#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
FTP Medical Document Processor - MSSQL + MySQL Version with Memory Stream
อ่านไฟล์จาก FTP server ลงใน memory stream โดยไม่เขียนไฟล์ลง disk
"""

import os
import re
import ftplib
from io import BytesIO
from typing import Optional, List, Tuple
import mysql.connector
from datetime import datetime
import hashlib
from typing import Optional, Dict, List
# สำหรับ PDF
try:
    import pdfplumber
except ImportError:
    print("กรุณาติดตั้ง: pip install pdfplumber --break-system-packages")

# สำหรับ OCR
try:
    import easyocr
    from PIL import Image
except ImportError:
    print("กรุณาติดตั้ง: pip install easyocr pillow --break-system-packages")

# สำหรับ MSSQL Database
try:
    import pyodbc
except ImportError:
    print("กรุณาติดตั้ง: pip install pyodbc --break-system-packages")
class HospitalReceiptType:
    """ประเภทของใบเสร็จโรงพยาบาล"""
    RECEIPT_000_OPD = "RECEIPT_000_OPD"  # ใบเสร็จรูปแบบทั่วไป        STANDARD
    INVIOCE_000_IPD = "INVIOCE_000_IPD"  # ใบเสร็จรูปแบบโรงพยาบาลศูนย์การแพทย์       HOSPITAL_OFFICIAL
    RECEIPT_SIRIRAJ_000_OPD = "siriraj"  # ใบเสร็จโรงพยาบาลศิริราช
    RECEIPT_RAJAVITHI_000_OPD = "rajavithi"  # ใบเสร็จโรงพยาบาลราชวิถี
    PRAMONGKUTKLAO = "pramongkutklao"  # ใบเสร็จโรงพยาบาลพระมงกุฎเกล้า
    VAJIRA_RECEIPT_000_OPD = "vajira"  # ใบเสร็จโรงพยาบาลวชิระ
    UNKNOWN = "UNKNOWN"
class FTPMedicalDocProcessorStream:
    """ประมวลผลเอกสารทางการแพทย์จาก FTP server - Memory Stream Version"""
    
    def __init__(self, ftp_host: str, ftp_user: str, ftp_pass: str,
                 db_server: str, db_user: str, db_pass: str, db_name: str,
                 db_driver: str = '{ODBC Driver 17 for SQL Server}',
                 mysql_server: str = None, mysql_user: str = None, 
                 mysql_pass: str = None, mysql_name: str = None):
        """
        Initialize FTP and Database connections
        
        Args:
            ftp_host, ftp_user, ftp_pass: FTP credentials
            db_server, db_user, db_pass, db_name, db_driver: MSSQL credentials
            mysql_server, mysql_user, mysql_pass, mysql_name: MySQL credentials (optional)
        """
        # FTP settings
        self.ftp_host = ftp_host
        self.ftp_user = ftp_user
        self.ftp_pass = ftp_pass
        
        # MSSQL settings
        self.db_server = db_server
        self.db_user = db_user
        self.db_pass = db_pass
        self.db_name = db_name
        self.db_driver = db_driver

        # MySQL settings - แก้ไขจาก db_name เป็น mysql_*
        self.mysql_server = mysql_server
        self.mysql_user = mysql_user
        self.mysql_pass = mysql_pass
        self.mysql_name = mysql_name
        
        # Initialize OCR reader
        print("กำลังโหลด EasyOCR...")
        self.reader = easyocr.Reader(['th', 'en'], gpu=False)
        print("โหลด EasyOCR เสร็จสิ้น")
        
    def connect_ftp(self) -> ftplib.FTP:
        """เชื่อมต่อ FTP server"""
        try:
            ftp = ftplib.FTP(self.ftp_host)
            ftp.login(self.ftp_user, self.ftp_pass)
            print(f"✓ เชื่อมต่อ FTP server: {self.ftp_host}")
            return ftp
        except Exception as e:
            print(f"✗ ไม่สามารถเชื่อมต่อ FTP: {e}")
            raise
    
    def list_files(self, ftp: ftplib.FTP, directory: str = '.', 
                   pattern: str = None) -> List[str]:
        """
        แสดงรายการไฟล์บน FTP server
        
        Args:
            ftp: FTP connection object
            directory: ไดเรกทอรีที่ต้องการดู (default: '.' = current)
            pattern: filter ชื่อไฟล์ (เช่น '*.pdf')
        
        Returns:
            List ของชื่อไฟล์
        """
        try:
            print(f"\nรายการไฟล์ใน: {directory}")
            print("="*70)
            
            # เปลี่ยนไปยัง directory
            if directory != '.':
                ftp.cwd(directory)
            
            # ดึงรายการไฟล์
            files = ftp.nlst()
            
            # Filter ตาม pattern (ถ้ามี)
            if pattern:
                import fnmatch
                files = [f for f in files if fnmatch.fnmatch(f, pattern)]
            
            if files:
                print(f"พบ {len(files)} ไฟล์:")
                for i, filename in enumerate(files, 1):
                    # ลองดึงขนาดไฟล์
                    try:
                        size = ftp.size(filename)
                        size_str = f"{size:,} bytes"
                        if size > 1024*1024:
                            size_str += f" ({size/1024/1024:.2f} MB)"
                        elif size > 1024:
                            size_str += f" ({size/1024:.2f} KB)"
                    except:
                        size_str = "N/A"
                    
                    print(f"  {i:3d}. {filename:<50} {size_str}")
            else:
                print("ไม่พบไฟล์")
            
            print("="*70)
            
            return files
            
        except Exception as e:
            print(f"✗ ไม่สามารถดูรายการไฟล์: {e}")
            return []
    def calculate_md5_from_stream(self, stream: BytesIO) -> str:
        """คำนวณ MD5 hash จาก BytesIO"""
        current_pos = stream.tell()
        stream.seek(0)
    
        md5_hash = hashlib.md5()
    
        while True:
            chunk = stream.read(8192)
            if not chunk:
                break
            md5_hash.update(chunk)
    
        stream.seek(current_pos)
        return md5_hash.hexdigest()
    def download_to_memory(self, ftp: ftplib.FTP, remote_path: str, 
                          chunk_size: int = 8192) -> Optional[BytesIO]:
        """
        ดาวน์โหลดไฟล์จาก FTP ลงใน memory stream (BytesIO)
        
        Args:
            ftp: FTP connection object
            remote_path: path ของไฟล์บน FTP server
            chunk_size: ขนาด buffer สำหรับ streaming (bytes)
        
        Returns:
            BytesIO object หรือ None ถ้าล้มเหลว
        """
        try:
            # สร้าง memory stream
            memory_stream = BytesIO()
            
            # ดึงขนาดไฟล์
            file_size = 0
            downloaded = 0
            
            try:
                file_size = ftp.size(remote_path)
            except:
                file_size = 0
            
            # Callback สำหรับ streaming
            def write_callback(data):
                nonlocal downloaded
                memory_stream.write(data)  # เขียนลง memory
                downloaded += len(data)
                
                # แสดง progress
                if file_size > 0:
                    percent = (downloaded / file_size) * 100
                    print(f"\rดาวน์โหลด: {downloaded:,} / {file_size:,} bytes ({percent:.1f}%)", 
                          end='', flush=True)
                else:
                    print(f"\rดาวน์โหลด: {downloaded:,} bytes", end='', flush=True)
            
            # Download แบบ streaming ลง memory
            print(f"\nกำลังดาวน์โหลด: {remote_path}")
            ftp.retrbinary(f'RETR {remote_path}', write_callback, blocksize=chunk_size)
            
            print()
            print(f"✓ ดาวน์โหลดเสร็จสิ้น: {downloaded:,} bytes (memory stream)")
            
            # Reset pointer ไปที่ต้น stream
            memory_stream.seek(0)
            
            return memory_stream
            
        except ftplib.error_perm as e:
            print(f"\n✗ ไม่มีสิทธิ์เข้าถึงไฟล์: {e}")
            return None
        except Exception as e:
            print(f"\n✗ ไม่สามารถดาวน์โหลดไฟล์: {e}")
            return None
    
    def detect_file_type_from_stream(self, stream: BytesIO) -> Optional[str]:
        """
        ตรวจสอบประเภทไฟล์จาก memory stream
        
        Args:
            stream: BytesIO object
            
        Returns:
            'pdf' หรือ 'image' หรือ None
        """
        try:
            # อ่าน header
            current_pos = stream.tell()
            stream.seek(0)
            header = stream.read(8)
            stream.seek(current_pos)  # คืน position
            
            if header.startswith(b'%PDF'):
                return 'pdf'
            elif header.startswith(b'\xff\xd8\xff'):  # JPEG
                return 'image'
            elif header.startswith(b'\x89PNG'):  # PNG
                return 'image'
            return None
        except Exception as e:
            print(f"✗ ไม่สามารถตรวจสอบประเภทไฟล์: {e}")
            return None
    
    def extract_text_from_pdf_stream(self, stream: BytesIO) -> str:
        """
        แยกข้อความจาก PDF stream
        
        Args:
            stream: BytesIO object ที่มี PDF data
            
        Returns:
            ข้อความที่แยกได้
        """
        text = ""
        try:
            # pdfplumber สามารถอ่านจาก BytesIO ได้โดยตรง
            with pdfplumber.open(stream) as pdf:
                print(f"PDF มี {len(pdf.pages)} หน้า")
                
                for page_num, page in enumerate(pdf.pages, 1):
                    page_text = page.extract_text()
                    if page_text:
                        text += f"\n{'='*60}\n"
                        text += f"หน้า {page_num}\n"
                        text += '='*60 + '\n'
                        text += page_text
            
            print(f"✓ แยกข้อความจาก PDF: {len(text)} ตัวอักษร")
            
        except Exception as e:
            print(f"✗ ไม่สามารถแยกข้อความจาก PDF: {e}")
        
        return text
    
    def extract_text_from_image_stream(self, stream: BytesIO) -> str:
        """
        แยกข้อความจาก image stream ด้วย OCR
        
        Args:
            stream: BytesIO object ที่มี image data
            
        Returns:
            ข้อความที่แยกได้
        """
        text = ""
        try:
            # PIL สามารถอ่านจาก BytesIO ได้โดยตรง
            stream.seek(0)
            image = Image.open(stream)
            
            print(f"Image: {image.size[0]} x {image.size[1]} pixels, {image.mode}")
            print("กำลัง OCR...")
            
            # EasyOCR รับ PIL Image ได้โดยตรง
            results = self.reader.readtext(image)
            
            # รวมข้อความ
            text_lines = [result[1] for result in results]
            text = "\n".join(text_lines)
            
            print(f"✓ OCR เสร็จสิ้น: {len(results)} บรรทัด, {len(text)} ตัวอักษร")
            
        except Exception as e:
            print(f"✗ ไม่สามารถ OCR image: {e}")
        
        return text
    
    def extract_thai_id(self, text: str) -> List[str]:
        """หาเลขบัตรประชาชนไทย 13 หลัก"""
        patterns = [    r'\b\d{1}[-\s]?\d{4}[-\s]?\d{5}[-\s]?\d{2}[-\s]?\d{1}\b',            r'\b\d{13}\b',        ]
        id_numbers = []
        for pattern in patterns:
            matches = re.findall(pattern, text)
            for match in matches:
                clean_id = re.sub(r'[-\s]', '', match)
                if len(clean_id) == 13 and clean_id.isdigit():
                    if clean_id not in id_numbers:
                        id_numbers.append(clean_id)
        
        return id_numbers
    
    def extract_hn_an(self, text: str) -> dict:
        """หา HN และ AN จากเอกสาร"""
        result = {
            'HN': [],
            'AN': [],
        }
        
        # Pattern สำหรับ HN
        hn_patterns = [
            r'HN\s*[:\s]\s*(\d+[-]?\d+)',
            r'Hospital\s*Number\s*[:\s]\s*(\d+)',
        ]
        
        # Pattern สำหรับ AN
        an_patterns = [
            r'AN\s*[:\s]\s*([A-Z0-9\-/]+)',
            r'Admission\s*Number\s*[:\s]\s*([A-Z0-9\-/]+)',
        ]
        
        # ค้นหา HN
        for pattern in hn_patterns:
            matches = re.findall(pattern, text, re.IGNORECASE)
            for match in matches:
                clean_hn = re.sub(r'[-\s]', '', match)
                if clean_hn and clean_hn not in result['HN']:
                    result['HN'].append(clean_hn)
        
        # ค้นหา AN
        for pattern in an_patterns:
            matches = re.findall(pattern, text, re.IGNORECASE)
            for match in matches:
                if match and match not in result['AN']:
                    result['AN'].append(match)
        
        return result
    
    def search_patient(self, pid: str) -> Optional[dict]:
        """ค้นหาข้อมูลผู้ป่วยจาก MSSQL database"""
        try:
            conn_str = (
                f'DRIVER={self.db_driver};'
                f'SERVER={self.db_server};'
                f'DATABASE={self.db_name};'
                f'UID={self.db_user};'
                f'PWD={self.db_pass};'
                f'TrustServerCertificate=yes;'
            )
            
            conn = pyodbc.connect(conn_str)
            cursor = conn.cursor()
            
            query = "SELECT * FROM patient_m01 WHERE PID = ?"
            cursor.execute(query, (pid,))
            
            columns = [column[0] for column in cursor.description]
            row = cursor.fetchone()
            
            result = None
            if row:
                result = dict(zip(columns, row))
                print(f"✓ พบข้อมูลผู้ป่วย PID: {pid}")
            else:
                print(f"✗ ไม่พบข้อมูลผู้ป่วย PID: {pid}")
            
            cursor.close()
            conn.close()
            
            return result
                
        except Exception as e:
            print(f"✗ ไม่สามารถค้นหาข้อมูลจาก database: {e}")
            return None
    
    def save_to_mysql(self, result_data: dict) -> bool:
        """
        บันทึกข้อมูลผลการประมวลผลลง MySQL database
        
        Args:
            result_data: dict ผลการประมวลผล
            
        Returns:
            True ถ้าบันทึกสำเร็จ
        """
        # ตรวจสอบว่าตั้งค่า MySQL หรือไม่
        if not self.mysql_server:
            print("✗ ไม่ได้ตั้งค่า MySQL connection")
            return False
        try:
            conn = mysql.connector.connect(     host=self.mysql_server,     user=self.mysql_user,       password=self.mysql_pass,   database=self.mysql_name)
            cursor = conn.cursor()
            # เตรียมข้อมูลสำหรับบันทึก
            # แปลง list เป็น string
            data = {
                'file_path': result_data.get('ftp_file_path', ''),
                'file_type': result_data.get('file_type', ''),
                'pid_list': ','.join(result_data.get('pid', [])),
                'hn_list': ','.join(result_data.get('HN', [])),
                'an_list': ','.join(result_data.get('AN', [])),
                'text_content': result_data.get('text', '')[:7000],  # จำกัดความยาว
                'memory_used': result_data.get('memory_used', 0),
                'time_start': result_data.get('time_start', 0),
                'time_end': result_data.get('time_end', 0),
                'ftp_file_path_archive': result_data.get('ftp_file_path_archive', ''),
                'md5': result_data.get('md5', ''),
                'what_image_is': result_data.get('what_image_is', '')
            }
            
            # INSERT query
            query = """
            INSERT INTO t_supra_image 
            (file_path, file_type, pid_list, hn_list, an_list, text_content, memory_used, time_start, time_end, ftp_file_path_archive, md5, what_image_is) 
            VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)
            """
            
            cursor.execute(query, (                data['file_path'],                data['file_type'],                data['pid_list'],                data['hn_list'],
                data['an_list'],                data['text_content'],                data['memory_used'],                data['time_start'],                data['time_end'],
                data['ftp_file_path_archive'],                data['md5'],                data['what_image_is']
            ))
            conn.commit()
            print(f"✓ บันทึกข้อมูลลง MySQL (table: t_supra_image, row_id: {cursor.lastrowid})")
            cursor.close()
            conn.close()
            return True
        except Exception as e:
            print(f"✗ ไม่สามารถบันทึกข้อมูลลง MySQL: {e}")
            return False
    
    def process_file_from_ftp(self, ftp_file_path: str) -> dict:
        """
        ประมวลผลไฟล์จาก FTP server โดยใช้ memory stream
        
        Args:
            ftp_file_path: path ของไฟล์บน FTP server
            
        Returns:
            dict ผลการประมวลผล
        """
        # แก้ไข: ตั้งชื่อ key ให้ตรงกัน
        result = {
            'ftp_file_path': ftp_file_path,            'file_type': None,            'text': '',            'pid': [],
            'HN': [],            'AN': [],            'patients': [],            'memory_used': 0,
            'time_start': '',            'time_end': '',            'ftp_file_path_archive': '',            'md5': None,
            'has_veshaphan': '',            'what_image_is': '',        }
        
        print(f"\n{'='*70}")
        print(f"ประมวลผล: {ftp_file_path}")
        print('='*70)
        
        try:
            # เชื่อมต่อ FTP
            ftp = self.connect_ftp()
            # Download ลง memory stream
            memory_stream = self.download_to_memory(ftp, ftp_file_path)
            if not memory_stream:
                ftp.quit()
                return result
            # เก็บขนาด memory ที่ใช้
            #result['time_start'] = __import__('datetime').datetime.now().strftime('%Y-%m-%d %H:%M:%S')
            result['time_start'] = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
            result['memory_used'] = memory_stream.getbuffer().nbytes
            result['md5'] = self.calculate_md5_from_stream(memory_stream)
            print(f"Memory used: {result['memory_used']:,} bytes")
            # ตรวจสอบประเภทไฟล์
            file_type = self.detect_file_type_from_stream(memory_stream)
            result['file_type'] = file_type
            if not file_type:
                print("✗ ไม่สามารถระบุประเภทไฟล์ได้")
                memory_stream.close()
                ftp.quit()
                return result
            
            print(f"ประเภทไฟล์: {file_type}")
            
            # แยกข้อความตามประเภทไฟล์
            if file_type == 'pdf':
                result['text'] = self.extract_text_from_pdf_stream(memory_stream)
            elif file_type == 'image':
                result['text'] = self.extract_text_from_image_stream(memory_stream)
            
            # ปิด memory stream
            memory_stream.close()
            result['time_end'] = datetime.now().strftime('%Y-%m-%d %H:%M:%S')

            # Move file to archive folder on FTP instead of deleting
            try:
                base_name = os.path.basename(ftp_file_path)
                timestamp = datetime.now().strftime('%Y%m%d%H%M%S')
                archive_dir = '/supra_output'
                # Ensure archive dir exists (ignore error if already exists)
                try:
                    ftp.mkd(archive_dir)
                except Exception:
                    pass
                new_path = f"{archive_dir}/{timestamp}_{base_name}"
                ftp.rename(ftp_file_path, new_path)
                print(f"✓ Moved FTP file to: {new_path}")
                # update saved file_path to the new location if desired
                result['ftp_file_path_archive'] = new_path
            except Exception as e:
                print(f"✗ ไม่สามารถย้ายไฟล์ไปยังโฟลเดอร์ archive: {e}")

            # ปิดการเชื่อมต่อ FTP
            ftp.quit()
            print("✓ ปิดการเชื่อมต่อ FTP")
            # ตรวจสอบประเภทใบเสร็จ
            what_image_is = self.detect_receipt_type(result['text'])
            result['what_image_is'] = what_image_is


            # หาข้อมูลที่ต้องการตามประเภทใบเสร็จ
            if what_image_is == HospitalReceiptType.HOSPITAL_OFFICIAL:
                # ใช้ method สำหรับใบเสร็จโรงพยาบาลศูนย์
                print(">>> ใช้ระบบประมวลผลสำหรับใบเสร็จโรงพยาบาลศูนย์การแพทย์ <<<\n")
                
                # แยกข้อมูลจากใบเสร็จรูปแบบโรงพยาบาล
                hospital_data = self.extract_hospital_official_receipt(result['text'])
                
                # เก็บข้อมูลลง result
                result['hospital_receipt_data'] = hospital_data
                result['HN'] = [hospital_data['hn']] if hospital_data['hn'] else []
                result['AN'] = []  # ใบเสร็จรูปแบบนี้ไม่มี AN
                
                # รายการค่าใช้จ่าย
                bill_items = hospital_data['bill_items']
                
            elif what_image_is == HospitalReceiptType.SIRIRAJ:
                # ใช้ method สำหรับใบเสร็จศิริราช
                print(">>> ใช้ระบบประมวลผลสำหรับใบเสร็จโรงพยาบาลศิริราช <<<\n")
                
                # แยกข้อมูลจากใบเสร็จศิริราช
                siriraj_data = self.extract_siriraj_receipt(result['text'])
                
                # เก็บข้อมูลลง result
                result['siriraj_receipt_data'] = siriraj_data
                result['HN'] = [siriraj_data['hn']] if siriraj_data['hn'] else []
                result['AN'] = []  # ใบเสร็จศิริราชไม่มี AN
                
                # รายการค่าใช้จ่าย
                bill_items = siriraj_data['bill_items']
            elif what_image_is == HospitalReceiptType.RAJAVITHI:
                # ใช้ method สำหรับใบเสร็จราชวิถี
                print(">>> ใช้ระบบประมวลผลสำหรับใบเสร็จโรงพยาบาลราชวิถี <<<\n")
                
                # แยกข้อมูลจากใบเสร็จราชวิถี
                rajavithi_data = self.extract_rajavithi_receipt(result['text'])
                
                # เก็บข้อมูลลง result
                result['rajavithi_receipt_data'] = rajavithi_data
                result['HN'] = [rajavithi_data['hn']] if rajavithi_data['hn'] else []
                result['AN'] = [rajavithi_data['an']] if rajavithi_data['an'] else []
                
                # รายการค่าใช้จ่าย
                bill_items = rajavithi_data['bill_items']
            elif what_image_is == HospitalReceiptType.PRAMONGKUTKLAO:
                # ใช้ method สำหรับใบเสร็จพระมงกุฎเกล้า
                print(">>> ใช้ระบบประมวลผลสำหรับใบเสร็จโรงพยาบาลพระมงกุฎเกล้า <<<\n")
                
                # แยกข้อมูลจากใบเสร็จพระมงกุฎเกล้า
                pmk_data = self.extract_pramongkutklao_receipt(result['text'])
                
                # เก็บข้อมูลลง result
                result['pramongkutklao_receipt_data'] = pmk_data
                result['HN'] = [pmk_data['hn']] if pmk_data['hn'] else []
                result['AN'] = []  # ใบเสร็จพระมงกุฎเกล้าไม่มี AN
                
                # รายการค่าใช้จ่าย
                bill_items = pmk_data['bill_items']
            elif what_image_is == HospitalReceiptType.VAJIRA_RECEIPT_000_OPD:
                # ใช้ method สำหรับใบเสร็จวชิรพยาบาล
                print(">>> ใช้ระบบประมวลผลสำหรับใบเสร็จโรงพยาบาลวชิรพยาบาล <<<\n")
                
                # แยกข้อมูลจากใบเสร็จวชิรพยาบาล
                vajira_data = self.extract_vajira_receipt(result['text'])
                
                # เก็บข้อมูลลง result
                result['vajira_receipt_data'] = vajira_data
                result['HN'] = [vajira_data['hn']] if vajira_data['hn'] else []
                result['AN'] = []  # ใบเสร็จวชิรพยาบาลไม่มี AN
                
                # รายการค่าใช้จ่าย
                bill_items = vajira_data['bill_items']
            else:
                # ใช้ method แบบเดิมสำหรับใบเสร็จทั่วไป
                print(">>> ใช้ระบบประมวลผลแบบมาตรฐาน <<<\n")
                
                id_numbers = self.extract_thai_id(result['text'])
                hn_an = self.extract_hn_an(result['text'])
                bill_items = self.extract_bill_items(result['text'])
                
                result['id_numbers'] = id_numbers
                result['HN'] = hn_an['HN']
                result['AN'] = hn_an['AN']
            
            # หาข้อมูลที่ต้องการ
            id_numbers = self.extract_thai_id(result['text'])
            hn_an = self.extract_hn_an(result['text'])
            bill_items = self.extract_bill_items(result['text'])
            #items.append({'item_no': item_no,'description': description,'amount': amount})
            # แสดงบิลไอเท็มและระบุว่ามี "เวชภัณฑ์" หรือไม่ในแต่ละรายการ
            if bill_items:
                print(f"\n✓ Bill items ({len(bill_items)}):")
                for it in bill_items:
                    flag = "YES" if it.get('is_veshaphan') else "no"
                    print(f"  - [{flag}] {it.get('description')} => {it.get('amount')}")
                    result['has_veshaphan'] = it.get('amount')
                print(f"\nHas any 'เวชภัณฑ์' item: {result['has_veshaphan']}")
            else:
                print("\n✗ ไม่พบรายการค่าใช้จ่าย (bill items)")
            
            result['pid'] = id_numbers
            result['HN'] = hn_an['HN']
            result['AN'] = hn_an['AN']
            
            # แสดงผลลัพธ์
            print(f"\n{'='*70}")
            print("ผลการประมวลผล")
            print('='*70)
            
            if id_numbers:
                print(f"\n✓ เลขบัตรประชาชน ({len(id_numbers)} รายการ):")
                for i, pid in enumerate(id_numbers, 1):
                    formatted_pid = f"{pid[0]}-{pid[1:5]}-{pid[5:10]}-{pid[10:12]}-{pid[12]}"
                    print(f"  {i}. {formatted_pid}")
                    
                    # ค้นหาใน database
                    patient = self.search_patient(pid)
                    if patient:
                        result['patients'].append(patient)
                        print(f"     HN: {patient.get('HN')}, Name: {patient.get('Name')}")
            else:
                print("\n✗ ไม่พบเลขบัตรประชาชน")
            
            # แก้ไข: ใช้ key ที่ถูกต้อง
            if result['HN']:
                print(f"\n✓ HN ({len(result['HN'])} รายการ): {', '.join(result['HN'])}")
            
            if result['AN']:
                print(f"✓ AN ({len(result['AN'])} รายการ): {', '.join(result['AN'])}")
            
            print(f"\n{'='*70}\n")
            
            # บันทึกลง MySQL - แก้ไข: ใช้ self.
            self.save_to_mysql(result)
            
        except Exception as e:
            print(f"\n✗ เกิดข้อผิดพลาด: {e}")
            import traceback
            traceback.print_exc()
        
        return result
    def detect_receipt_type(self, text: str) -> str:
        """
        ตรวจสอบประเภทของใบเสร็จ
        
        Args:
            text: ข้อความจาก OCR
            
        Returns:
            ประเภทของใบเสร็จ (HospitalReceiptType)
        """
        # คำสำคัญสำหรับใบเสร็จโรงพยาบาลศูนย์การแพทย์
        keywords_receipt_000_opd = [        'โรงพยาบาล',        'สมเด็จพระเทพรัตนราชสุดา',         'ใบเสร็จรับเงิน',  'คณะแพทยศาสตร์ มหาวิทยาลัยศรีนครินทรวิโรฒ'    ]
        keywords_invoice_000_ipd = [        'ศูนย์การ','ชูนย์การ',' ศูนย์การ',        'สมเด็จพระเทพรัตนราชสุดา', 'ใบสรุปค่ารักษา',  'คณะแพทยศาสตร์ มหาวิทยาลัยศรีนครินทรวิโรฒ'    ]
        siriraj_keywords = [            'คณะแพทยศาสตร์ศิริราชพยาบาล',            'ศิริราชพยาบาล',            'มหาวิทยาลัยมหิดล',            'เลขที่จ่ายยา',            'SM1-O-',            'ENT-O-'        ]
        rajavithi_keywords = [            'คณะแพทยศาสตร์ศิริราชพยาบาล',            'ศิริราชพยาบาล',            'มหาวิทยาลัยมหิดล',            'เลขที่จ่ายยา',            'SM1-O-',            'ENT-O-'        ]
        pramongkutklao_keywords = [            'โรงพยาบาลพระมงกุฎเกล้า',            'พระมงกุฎเกล้า',            'วงที่',            'ที่ในเสร็จต้นฉบับ'        ]
        vajira_keywords = [            'วชิรพยาบาล',            'วชิริมพราชา',            'คณะแพทยศาสตร์วชิรพยาบาล',            'ถนนบำราศเสน',            'เขตดุสิต'        ]
        # ตรวจสอบว่ามีคำสำคัญหรือไม่
        keyword_count_receipt_000_opd = 0
        keyword_count_invoice_000_ipd = 0
        keyword_count_receipt_siriraj_000_opd = 0
        keyword_count_receipt_rajavithi_000_opd = 0
        keyword_count_receipt_pramongkutklao_000_opd = 0
        keyword_count_receipt_vajira_000_opd = 0
        vajira_count = 0
        for keyword in keywords_receipt_000_opd:
            if keyword in text:
                keyword_count_receipt_000_opd += 1
        for keyword in keywords_invoice_000_ipd:
            if keyword in text:
                keyword_count_invoice_000_ipd += 1
        for keyword in siriraj_keywords:
            if keyword in text:
                keyword_count_receipt_siriraj_000_opd += 1
        for keyword in rajavithi_keywords:
            if keyword in text:
                keyword_count_receipt_rajavithi_000_opd += 1
        for keyword in pramongkutklao_keywords:
            if keyword in text:
                keyword_count_receipt_pramongkutklao_000_opd += 1
        for keyword in vajira_keywords:
            if keyword in text:
                vajira_count += 1
        
        # ถ้าพบคำสำคัญ 2 คำขึ้นไป แสดงว่าเป็นใบเสร็จโรงพยาบาลศูนย์
        if keyword_count_receipt_000_opd >= 2:
            return HospitalReceiptType.RECEIPT_000_OPD
        elif keyword_count_receipt_siriraj_000_opd >= 2:
            return HospitalReceiptType.RECEIPT_SIRIRAJ_000_OPD
        elif keyword_count_receipt_rajavithi_000_opd >= 2:
            return HospitalReceiptType.RECEIPT_RAJAVITHI_000_OPD
        elif keyword_count_receipt_pramongkutklao_000_opd >= 2:
            return HospitalReceiptType.PRAMONGKUTKLAO
        elif vajira_count >= 2:
            return HospitalReceiptType.VAJIRA
        elif keyword_count_invoice_000_ipd >= 2:
            return HospitalReceiptType.INVIOCE_000_IPD
        # ตรวจสอบรูปแบบเลขที่ใบเสร็จ (xxx/xx)
        receipt_pattern = r'\b\d{1,4}/\d{1,4}\b'
        if re.search(receipt_pattern, text) and 'ใบเสร็จรับเงิน' in text:
            return HospitalReceiptType.INVIOCE_000_IPD
        
        return HospitalReceiptType.RECEIPT_000_OPD

    def extract_hospital_official_receipt(self, text: str) -> Dict:
        """
        แยกข้อมูลจากใบเสร็จโรงพยาบาลศูนย์การแพทย์
        
        Args:
            text: ข้อความจาก OCR
            
        Returns:
            dict ข้อมูลที่แยกได้
        """
        print(f"\n{'='*70}")
        print("แยกข้อมูลจากใบเสร็จโรงพยาบาลศูนย์การแพทย์")
        print(f"{'='*70}\n")
        
        result = {
            'receipt_type': HospitalReceiptType.INVIOCE_000_IPD,
            'receipt_number': None,
            'receipt_date': None,
            'hospital_name': None,
            'hn': None,
            'patient_name': None,
            'total_amount': None,
            'receipt_doc_number': None,  # เลขเอกสารท้ายใบเสร็จ
            'bill_items': []
        }
        
        # 1. ชื่อโรงพยาบาล
        hospital_patterns = [
            r'(โรงพยาบาล[^\n]+)',
            r'(คณะแพทยศาสตร์[^\n]+)'
        ]
        for pattern in hospital_patterns:
            match = re.search(pattern, text)
            if match:
                result['hospital_name'] = match.group(1).strip()
                break
        
        # 2. เลขที่ใบเสร็จ (รูปแบบ xxx/xx)
        receipt_num_pattern = r'เลขที่[:\s]*([\d]+/[\d]+)'
        match = re.search(receipt_num_pattern, text)
        if match:
            result['receipt_number'] = match.group(1)
        else:
            # ลองหารูปแบบอื่น
            match = re.search(r'\b(\d{1,4}/\d{1,4})\b', text)
            if match:
                result['receipt_number'] = match.group(1)
        
        # 3. วันที่ (รูปแบบ: กุมภาพา 2568 11:22 น.)
        date_pattern = r'วันที่[:\s]*([^\n]+\d{2}:\d{2}[^\n]*)'
        match = re.search(date_pattern, text)
        if match:
            result['receipt_date'] = match.group(1).strip()
        
        # 4. HN
        hn_pattern = r'HN[:\s]*(\d{6,8})'
        match = re.search(hn_pattern, text)
        if match:
            result['hn'] = match.group(1)
        
        # 5. ชื่อผู้ป่วย
        name_patterns = [
            r'นาย\s+([^\n]+)',
            r'นาง\s+([^\n]+)',
            r'นางสาว\s+([^\n]+)',
            r'เด็กชาย\s+([^\n]+)',
            r'เด็กหญิง\s+([^\n]+)'
        ]
        for pattern in name_patterns:
            match = re.search(pattern, text)
            if match:
                name = match.group(1).strip()
                # ตัดข้อความที่ไม่เกี่ยวข้องออก
                name = re.split(r'[\d]{6,}', name)[0].strip()
                result['patient_name'] = name
                break
        
        # 6. จำนวนเงินรวม
        total_patterns = [
            r'รวมรากา[:\s]*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)',
            r'ยอดสุทธิ[:\s]*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)',
            r'จำนวนเงินรวม[:\s]*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)'
        ]
        for pattern in total_patterns:
            match = re.search(pattern, text)
            if match:
                amount_str = match.group(1).replace(',', '')
                try:
                    result['total_amount'] = float(amount_str)
                except ValueError:
                    pass
        
        # 7. เลขเอกสารท้ายใบเสร็จ (เช่น 1889100, 1898595)
        doc_num_pattern = r'\bถง\s+(\d{7,8})\b'
        match = re.search(doc_num_pattern, text)
        if match:
            result['receipt_doc_number'] = match.group(1)
        
        # 8. รายการค่าใช้จ่าย
        result['bill_items'] = self.extract_hospital_official_bill_items(text)
        
        # แสดงผลลัพธ์
        print(f"เลขที่ใบเสร็จ: {result['receipt_number']}")
        print(f"วันที่: {result['receipt_date']}")
        print(f"HN: {result['hn']}")
        print(f"ชื่อผู้ป่วย: {result['patient_name']}")
        print(f"จำนวนเงินรวม: {result['total_amount']:,.2f} บาท" if result['total_amount'] else "จำนวนเงินรวม: ไม่พบ")
        print(f"เลขเอกสาร: {result['receipt_doc_number']}")
        print(f"\n{'='*70}\n")
        
        return result
    def extract_siriraj_receipt(self, text: str) -> Dict:
        """
        แยกข้อมูลจากใบเสร็จโรงพยาบาลศิริราช
        
        Args:
            text: ข้อความจาก OCR
            
        Returns:
            dict ข้อมูลที่แยกได้
        """
        print(f"\n{'='*70}")
        print("แยกข้อมูลจากใบเสร็จโรงพยาบาลศิริราช")
        print(f"{'='*70}\n")
        
        result = {
            'receipt_type': HospitalReceiptType.SIRIRAJ,
            'receipt_number': None,
            'receipt_date': None,
            'hospital_name': 'โรงพยาบาลศิริราช',
            'hn': None,
            'prescription_no': None,  # เลขที่จ่ายยา (SM1-O-618, ENT-O-320)
            'patient_name': None,
            'total_amount': None,
            'claimable_amount': None,  # เบิกได้
            'excess_amount': None,  # เบิกได้เกินกระทรวงฯ
            'bill_items': []
        }
        
        # 1. เลขที่ใบเสร็จ (รูปแบบ: 916R-RO9-69004941)
        receipt_num_patterns = [
            r'เลขที่\s*(\d+R-[A-Z0-9-]+)',
            r'(\d{3,4}R-[A-Z0-9-]+)'
        ]
        for pattern in receipt_num_patterns:
            match = re.search(pattern, text)
            if match:
                result['receipt_number'] = match.group(1)
                break
        
        # 2. วันที่ (รูปแบบ: 2 ธันวาคม 2568)
        date_patterns = [
            r'วันที่\s+(\d{1,2}\s+[ก-๙]+\s+\d{4})',
            r'(\d{1,2}\s+ธันวาคม\s+\d{4})'
        ]
        for pattern in date_patterns:
            match = re.search(pattern, text)
            if match:
                result['receipt_date'] = match.group(1).strip()
                break
        
        # 3. HN (รูปแบบ: HN. 54-381483)
        hn_pattern = r'HN[.\s:]*(\d{2}-\d{6})'
        match = re.search(hn_pattern, text)
        if match:
            result['hn'] = match.group(1)
        
        # 4. เลขที่จ่ายยา (รูปแบบ: SM1-O-618, ENT-O-320)
        prescription_patterns = [
            r'เลขที่จ่ายยา[:\s]+([A-Z0-9-]+)',
            r'(SM1-O-\d+)',
            r'(ENT-O-\d+)',
            r'([A-Z]{2,3}\d*-O-\d+)'
        ]
        for pattern in prescription_patterns:
            match = re.search(pattern, text)
            if match:
                result['prescription_no'] = match.group(1)
                break
        
        # 5. ชื่อผู้ป่วย
        name_patterns = [
            r'ชื่อ\s+(นาง?|นางสาว|นาย|เด็กชาย|เด็กหญิง)\s+([^\n]+)',
            r'(นาง?|นางสาว|นาย)\s+([ก-๙]+\s+[ก-๙]+)'
        ]
        for pattern in name_patterns:
            match = re.search(pattern, text)
            if match:
                if match.lastindex >= 2:
                    result['patient_name'] = f"{match.group(1)} {match.group(2)}".strip()
                break
        
        # 6. จำนวนเงินรวม (รวมทั้งสิ้น)
        total_patterns = [
            r'รวมทั้งสิ้น\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)',
            r'QR Code\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)\s*บาท'
        ]
        for pattern in total_patterns:
            match = re.search(pattern, text)
            if match:
                amount_str = match.group(1).replace(',', '')
                try:
                    result['total_amount'] = float(amount_str)
                except ValueError:
                    pass
                break
        
        # 7. ชำระโดย และเบิกได้เกิน (ถ้ามี)
        payment_pattern = r'ชำระโดย\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)?'
        match = re.search(payment_pattern, text)
        if match:
            claimable = match.group(1).replace(',', '')
            try:
                result['claimable_amount'] = float(claimable)
            except ValueError:
                pass
            
            if match.group(2):
                excess = match.group(2).replace(',', '')
                try:
                    result['excess_amount'] = float(excess)
                except ValueError:
                    pass
        
        # 8. รายการค่าใช้จ่าย
        result['bill_items'] = self.extract_siriraj_bill_items(text)
        
        # แสดงผลลัพธ์
        print(f"เลขที่ใบเสร็จ: {result['receipt_number']}")
        print(f"วันที่: {result['receipt_date']}")
        print(f"HN: {result['hn']}")
        print(f"เลขที่จ่ายยา: {result['prescription_no']}")
        print(f"ชื่อผู้ป่วย: {result['patient_name']}")
        if result['total_amount']:
            print(f"จำนวนเงินรวม: {result['total_amount']:,.2f} บาท")
        if result['claimable_amount']:
            print(f"เบิกได้: {result['claimable_amount']:,.2f} บาท")
        if result['excess_amount']:
            print(f"เบิกได้เกิน: {result['excess_amount']:,.2f} บาท")
        print(f"\n{'='*70}\n")
        
        return result
    def extract_rajavithi_receipt(self, text: str) -> Dict:
        """
        แยกข้อมูลจากใบเสร็จโรงพยาบาลราชวิถี
        
        Args:
            text: ข้อความจาก OCR
            
        Returns:
            dict ข้อมูลที่แยกได้
        """
        print(f"\n{'='*70}")
        print("แยกข้อมูลจากใบเสร็จโรงพยาบาลราชวิถี")
        print(f"{'='*70}\n")
        
        result = {
            'receipt_type': HospitalReceiptType.RAJAVITHI,
            'receipt_number': None,
            'receipt_sub_number': None,  # เลขที่/เลขย่อย (82511/049)
            'ref_number': None,  # Ref.No.7403049/69
            'receipt_date': None,
            'hospital_name': 'โรงพยาบาลราชวิถี',
            'hn': None,
            'an': None,
            'patient_name': None,
            'payment_method': None,  # วิธีชำระเงิน (บัตรเครดิต/เงินสด)
            'total_amount': None,
            'claimable_amount': None,  # เบิกได้คงระบบ
            'bill_items': []
        }
        
        # 1. เลขที่ใบเสร็จ (รูปแบบ: 82511/049)
        receipt_num_patterns = [
            r'เลขที่\s*(\d{5})/(\d{3})',
            r'เลขที่[:\s]*(\d{5})',
            r'(\d{5})/(\d{3})'
        ]
        for pattern in receipt_num_patterns:
            match = re.search(pattern, text)
            if match:
                if match.lastindex >= 2:
                    result['receipt_number'] = match.group(1)
                    result['receipt_sub_number'] = match.group(2)
                    result['receipt_full_number'] = f"{match.group(1)}/{match.group(2)}"
                else:
                    result['receipt_number'] = match.group(1)
                break
        
        # 2. Ref.No. (รูปแบบ: Ref.No.7403049/69)
        ref_patterns = [
            r'Ref\.No\.(\d+/\d+)',
            r'Ref\.No\s*[.:]?\s*(\d+/\d+)'
        ]
        for pattern in ref_patterns:
            match = re.search(pattern, text)
            if match:
                result['ref_number'] = match.group(1)
                break
        
        # 3. วันที่ (รูปแบบ: 13 เดือน ธันวาคม พ.ศ. 2568)
        date_patterns = [
            r'วันที่\s+(\d{1,2})\s+เดือน\s+([ก-๙]+)\s+พ\.ศ\.\s+(\d{4})',
            r'(\d{1,2})\s+([ก-๙]+)\s+พ\.ศ\.\s+(\d{4})',
            r'(\d{1,2})\s+(ธันวาคม|มกราคม|กุมภาพันธ์|มีนาคม|เมษายน|พฤษภาคม|มิถุนายน|กรกฎาคม|สิงหาคม|กันยายน|ตุลาคม|พฤศจิกายน)\s+(\d{4})'
        ]
        for pattern in date_patterns:
            match = re.search(pattern, text)
            if match:
                if match.lastindex >= 3:
                    result['receipt_date'] = f"{match.group(1)} {match.group(2)} {match.group(3)}"
                break
        
        # 4. HN (รูปแบบ: 041330-68)
        hn_patterns = [
            r'HN[:\s]*(\d{6}-\d{2})',
            r'เลขประจำตัวผู้ป่วย[:\s]*(\d{6}-\d{2})',
            r'(\d{6}-\d{2})'
        ]
        for pattern in hn_patterns:
            match = re.search(pattern, text)
            if match:
                hn_candidate = match.group(1)
                # ตรวจสอบว่าไม่ใช่เลขบัตรประชาชน
                if not re.match(r'\d{13}', hn_candidate.replace('-', '')):
                    result['hn'] = hn_candidate
                    break
        
        # 5. AN (รูปแบบ: 230943) - สำหรับผู้ป่วยใน
        an_patterns = [
            r'AN[:\s]*(\d{6})',
            r'AN\s*[:]?\s*(\d{6})'
        ]
        for pattern in an_patterns:
            match = re.search(pattern, text)
            if match:
                result['an'] = match.group(1)
                break
        
        # 6. ชื่อผู้ป่วย (หาจากคำนำหน้า)
        name_patterns = [
            r'ชื่อ[:\s]+(นาย|นาง|นางสาว|เด็กชาย|เด็กหญิง|น\.ส\.|ด\.ช\.|ด\.ญ\.)\s*([ก-๙]+\s+[ก-๙]+)',
            r'(นาย|นาง|นางสาว)([ก-๙]+\s+[ก-๙]+)',
            r'ชื่อผู้ป่วย[:\s]+(นาย|นาง|นางสาว)\s*([ก-๙]+\s+[ก-๙]+)'
        ]
        for pattern in name_patterns:
            match = re.search(pattern, text)
            if match:
                if match.lastindex >= 2:
                    result['patient_name'] = f"{match.group(1)}{match.group(2)}".strip()
                    break
        
        # 7. วิธีชำระเงิน
        payment_patterns = [
            r'ชำระเป็น\s*(บัตรเครดิต|เงินสด|เงินโอน)',
            r'(บัตรเครดิต|เงินสด)'
        ]
        for pattern in payment_patterns:
            match = re.search(pattern, text)
            if match:
                result['payment_method'] = match.group(1)
                break
        
        # 8. จำนวนเงินรวม
        total_patterns = [
            r'สิทธิ์มีหนี้ส่งชอบ[า-๙]*\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)',
            r'หนี้มีส่งชอบ[า-๙]*\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)',
            r'รวมทั้งสิ้น\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)',
            r'ชำระเป็น\s+บัตรเครดิต\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)'
        ]
        for pattern in total_patterns:
            match = re.search(pattern, text)
            if match:
                amount_str = match.group(1).replace(',', '')
                try:
                    result['total_amount'] = float(amount_str)
                    break
                except ValueError:
                    pass
        
        # 9. เบิกได้คงระบบ (ถ้ามี)
        claimable_pattern = r'เบิกได้คงระบบ[ก-๙]*\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)'
        match = re.search(claimable_pattern, text)
        if match:
            claimable_str = match.group(1).replace(',', '')
            try:
                result['claimable_amount'] = float(claimable_str)
            except ValueError:
                pass
        
        # 10. รายการค่าใช้จ่าย
        result['bill_items'] = self.extract_rajavithi_bill_items(text)
        
        # แสดงผลลัพธ์
        print(f"เลขที่ใบเสร็จ: {result.get('receipt_full_number', result['receipt_number'])}")
        print(f"Ref.No.: {result['ref_number']}")
        print(f"วันที่: {result['receipt_date']}")
        print(f"HN: {result['hn']}")
        if result['an']:
            print(f"AN: {result['an']}")
        print(f"ชื่อผู้ป่วย: {result['patient_name']}")
        if result['payment_method']:
            print(f"วิธีชำระ: {result['payment_method']}")
        if result['total_amount']:
            print(f"จำนวนเงินรวม: {result['total_amount']:,.2f} บาท")
        if result['claimable_amount']:
            print(f"เบิกได้: {result['claimable_amount']:,.2f} บาท")
        print(f"\n{'='*70}\n")
        
        return result
    def extract_pramongkutklao_receipt(self, text: str) -> Dict:
        """
        แยกข้อมูลจากใบเสร็จโรงพยาบาลพระมงกุฎเกล้า
        
        Args:
            text: ข้อความจาก OCR
            
        Returns:
            dict ข้อมูลที่แยกได้
        """
        print(f"\n{'='*70}")
        print("แยกข้อมูลจากใบเสร็จโรงพยาบาลพระมงกุฎเกล้า")
        print(f"{'='*70}\n")
        
        result = {
            'receipt_type': HospitalReceiptType.PRAMONGKUTKLAO,
            'receipt_number': None,
            'receipt_sub_number': None,  # เลขที่/เลขย่อย (076757/69)
            'receipt_full_number': None,
            'vong_number': None,  # วงที่
            'doc_number': None,  # ที่ในเสร็จต้นฉบับ
            'sheet_number': None,  # แผ่นที่ 1/1
            'receipt_date': None,
            'hospital_name': 'โรงพยาบาลพระมงกุฎเกล้า',
            'hn': None,
            'patient_name': None,
            'total_amount': None,
            'bill_items': []
        }
        
        # 1. เลขที่ใบเสร็จ (รูปแบบ: 076757/69)
        receipt_num_patterns = [
            r'เลขที่\s*(\d{6})/(\d{2})',
            r'(\d{6})/(\d{2})\s*แผ่นที่'
        ]
        for pattern in receipt_num_patterns:
            match = re.search(pattern, text)
            if match:
                result['receipt_number'] = match.group(1)
                result['receipt_sub_number'] = match.group(2)
                result['receipt_full_number'] = f"{match.group(1)}/{match.group(2)}"
                break
        
        # 2. แผ่นที่ (รูปแบบ: แผ่นที่ 1/1)
        sheet_pattern = r'แผ่นที่\s*(\d+/\d+)'
        match = re.search(sheet_pattern, text)
        if match:
            result['sheet_number'] = match.group(1)
        
        # 3. วงที่ (รูปแบบ: 120959)
        vong_patterns = [
            r'วงที่\s*(\d{6})',
            r'วงที่[:\s]*(\d{6})'
        ]
        for pattern in vong_patterns:
            match = re.search(pattern, text)
            if match:
                result['vong_number'] = match.group(1)
                break
        
        # 4. ที่ในเสร็จต้นฉบับ (รูปแบบ: 32709404)
        doc_patterns = [
            r'ที่ในเสร็จต้นฉบับ\s*(\d{8})',
            r'ต้นฉบับ\s*(\d{8})'
        ]
        for pattern in doc_patterns:
            match = re.search(pattern, text)
            if match:
                result['doc_number'] = match.group(1)
                break
        
        # 5. วันที่ (รูปแบบ: 02 ธันวาคม 2568)
        date_patterns = [
            r'วันที่\s+(\d{1,2})\s+([ก-๙]+)\s+(\d{4})',
            r'(\d{1,2})\s+(ธันวาคม|มกราคม|กุมภาพันธ์|มีนาคม|เมษายน|พฤษภาคม|มิถุนายน|กรกฎาคม|สิงหาคม|กันยายน|ตุลาคม|พฤศจิกายน)\s+(\d{4})'
        ]
        for pattern in date_patterns:
            match = re.search(pattern, text)
            if match:
                if match.lastindex >= 3:
                    result['receipt_date'] = f"{match.group(1)} {match.group(2)} {match.group(3)}"
                break
        
        # 6. HN (รูปแบบ: 21695/68) - 5 หลัก/2 หลัก
        hn_patterns = [
            r'HN[:\s]*(\d{5}/\d{2})',
            r'HN[:\s]*(\d{5})/(\d{2})'
        ]
        for pattern in hn_patterns:
            match = re.search(pattern, text)
            if match:
                if '/' in match.group(0):
                    result['hn'] = match.group(1) if match.lastindex == 1 else f"{match.group(1)}/{match.group(2)}"
                else:
                    result['hn'] = match.group(1)
                break
        
        # 7. ชื่อผู้ป่วย (หาจากคำนำหน้า)
        name_patterns = [
            r'จาก\s+(นาย|นาง|นางสาว|เด็กชาย|เด็กหญิง)\s*([ก-๙]+\s+[ก-๙]+)',
            r'(นาย|นาง|นางสาว)([ก-๙]+\s+[ก-๙]+)',
            r'ชื่อผู้ป่วย[:\s]+(นาย|นาง|นางสาว)\s*([ก-๙]+\s+[ก-๙]+)'
        ]
        for pattern in name_patterns:
            match = re.search(pattern, text)
            if match:
                if match.lastindex >= 2:
                    result['patient_name'] = f"{match.group(1)}{match.group(2)}".strip()
                    break
        
        # 8. จำนวนเงินรวม
        total_patterns = [
            r'รวมทั้งสิ้น\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)',
            r'รวมเงิน\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)'
        ]
        for pattern in total_patterns:
            match = re.search(pattern, text)
            if match:
                amount_str = match.group(1).replace(',', '')
                try:
                    result['total_amount'] = float(amount_str)
                    break
                except ValueError:
                    pass
        
        # 9. รายการค่าใช้จ่าย
        result['bill_items'] = self.extract_pramongkutklao_bill_items(text)
        
        # แสดงผลลัพธ์
        print(f"เลขที่ใบเสร็จ: {result['receipt_full_number']}")
        if result['sheet_number']:
            print(f"แผ่นที่: {result['sheet_number']}")
        print(f"วงที่: {result['vong_number']}")
        print(f"ที่ในเสร็จต้นฉบับ: {result['doc_number']}")
        print(f"วันที่: {result['receipt_date']}")
        print(f"HN: {result['hn']}")
        print(f"ชื่อผู้ป่วย: {result['patient_name']}")
        if result['total_amount']:
            print(f"จำนวนเงินรวม: {result['total_amount']:,.2f} บาท")
        print(f"\n{'='*70}\n")
        
        return result
    def extract_vajira_receipt(self, text: str) -> Dict:
        """
        แยกข้อมูลจากใบเสร็จโรงพยาบาลวชิรพยาบาล
        รูปแบบคล้ายศิริราช Lab/VJR
        
        Args:
            text: ข้อความจาก OCR
            
        Returns:
            dict ข้อมูลที่แยกได้
        """
        print(f"\n{'='*70}")
        print("แยกข้อมูลจากใบเสร็จโรงพยาบาลวชิรพยาบาล")
        print(f"{'='*70}\n")
        
        result = {
            'receipt_type': HospitalReceiptType.VAJIRA,
            'receipt_number': None,
            'receipt_date': None,
            'hospital_name': 'โรงพยาบาลวชิรพยาบาล',
            'hn': None,
            'patient_name': None,
            'total_amount': None,
            'bill_items': [],
            # ข้อมูลเฉพาะวชิรพยาบาล (รูปแบบ Lab/VJR)
            'volume_sheet': None,  # ฉบับ/เล่ม ที่ปีงบ
            'page_number': None,  # หน้า
            'form_type': None,  # แบบ (AC 101 - VJR)
            'hospital_receipt_number': None,  # เลขที่โรงพยาบาล
            'cashier_receipt_number': None  # เลขที่ทำหน้าที่เก็บเงิน
        }
        
        # 1. เลขที่ใบเสร็จ (รูปแบบ: 121013 - 2203528/69)
        receipt_patterns = [
            r'เลขที่[:\s]*(\d{6}\s*-\s*\d{7}/\d{2})',
            r'(\d{6}\s*-\s*\d{7}/\d{2})'
        ]
        for pattern in receipt_patterns:
            match = re.search(pattern, text)
            if match:
                result['receipt_number'] = match.group(1).replace(' ', '')
                break
        
        # 2. ฉบับ/เล่ม ที่ปีงบ
        volume_patterns = [
            r'ฉบับ/เล่ม[:\s]+ที่ปีงบ[:\s]*([^\n]+)',
            r'ฉบับ/เล่ม ที่ปีงบ:\s*([^\n]+)'
        ]
        for pattern in volume_patterns:
            match = re.search(pattern, text)
            if match:
                result['volume_sheet'] = match.group(1).strip()
                break
        
        # 3. หน้า
        page_pattern = r'หน้า(\d+/\d+)'
        match = re.search(page_pattern, text)
        if match:
            result['page_number'] = match.group(1)
        
        # 4. วันที่ (รูปแบบ: 17/12/2568 06:54:41)
        date_patterns = [
            r'วันที่[:\s]*(\d{1,2}/\d{1,2}/\d{4}\s+\d{2}:\d{2}:\d{2})',
            r'(\d{1,2}/\d{1,2}/\d{4}\s+\d{2}:\d{2}:\d{2})'
        ]
        for pattern in date_patterns:
            match = re.search(pattern, text)
            if match:
                result['receipt_date'] = match.group(1)
                break
        
        # 5. เลขที่โรงพยาบาล (รูปแบบ: 8215/66)
        hospital_num_pattern = r'เลขที่โรงพยาบาล[:\s]*(\d{4}/\d{2})'
        match = re.search(hospital_num_pattern, text)
        if match:
            result['hospital_receipt_number'] = match.group(1)
        
        # 6. แบบ
        form_pattern = r'แบบ[:\s]*(AC\s*\d+\s*-\s*[A-Z]+)'
        match = re.search(form_pattern, text)
        if match:
            result['form_type'] = match.group(1).replace(' ', ' ')
        
        # 7. เลขที่ทำหน้าที่เก็บเงิน
        cashier_pattern = r'เลขที่ทำหน้าที่เก็บเงิน[:\s]*(\d+/\d+)'
        match = re.search(cashier_pattern, text)
        if match:
            result['cashier_receipt_number'] = match.group(1)
        
        # 8. ชื่อผู้ป่วย
        name_patterns = [
            r'ให้รับเงินค่ารักษาพยาบาลจาก\s*[:]\s*(นาย|นาง|นางสาว|เด็กชาย|เด็กหญิง)\s*([ก-๙]+\s+[ก-๙]+)',
            r'(นาย|นาง|นางสาว)([ก-๙]+\s+[ก-๙]+)'
        ]
        for pattern in name_patterns:
            match = re.search(pattern, text)
            if match:
                if match.lastindex >= 2:
                    result['patient_name'] = f"{match.group(1)}{match.group(2)}".strip()
                    break
        
        # 9. จำนวนเงินรวม
        total_patterns = [
            r'ของที่มีหนึ่งร้อยต่อยหนึ่งไปบากด้วยกัง\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)',
            r'รวม\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)\s*บาท',
            r'หนี้มีหนึ่งต่อหนึ่งไว้\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)'
        ]
        for pattern in total_patterns:
            match = re.search(pattern, text)
            if match:
                amount_str = match.group(1).replace(',', '')
                try:
                    result['total_amount'] = float(amount_str)
                    break
                except ValueError:
                    pass
        
        # 10. รายการค่าใช้จ่าย
        result['bill_items'] = self.extract_vajira_bill_items(text)
        
        # แสดงผลลัพธ์
        print(f"เลขที่ใบเสร็จ: {result['receipt_number']}")
        if result.get('volume_sheet'):
            print(f"ฉบับ/เล่ม: {result['volume_sheet']}")
        if result.get('page_number'):
            print(f"หน้า: {result['page_number']}")
        if result.get('form_type'):
            print(f"แบบ: {result['form_type']}")
        if result.get('hospital_receipt_number'):
            print(f"เลขที่โรงพยาบาล: {result['hospital_receipt_number']}")
        print(f"วันที่: {result['receipt_date']}")
        print(f"ชื่อผู้ป่วย: {result['patient_name']}")
        if result['total_amount']:
            print(f"จำนวนเงินรวม: {result['total_amount']:,.2f} บาท")
        if result.get('cashier_receipt_number'):
            print(f"เลขที่เก็บเงิน: {result['cashier_receipt_number']}")
        print(f"\n{'='*70}\n")
        
        return result
    def extract_hospital_official_bill_items(self, text: str) -> List[Dict]:
        """
        แยกรายการค่าใช้จ่ายจากใบเสร็จโรงพยาบาลศูนย์การแพทย์
        
        Args:
            text: ข้อความจาก OCR
            
        Returns:
            list of dict รายการค่าใช้จ่าย
        """
        print(f"{'='*70}")
        print("แยกรายการค่าใช้จ่าย (ใบเสร็จโรงพยาบาลศูนย์)")
        print(f"{'='*70}\n")
        
        items = []
        
        # คำสำคัญสำหรับรายการค่าใช้จ่าย
        known_items = [
            'ค่าห้อง', 'ค่าห้องและอาหาร', 'ค่าห้องพิเศษ',
            'ค่ายา', 'ยาและสารอาหาร', 'ยาที่นำไปใช้', 'ยาแผนไทย',
            'ยาในบัญชี', 'ยานอกบัญชี', 'ค่ายาในบัญชี', 'ค่ายานอกบัญชี',
            'เวชภัณฑ์', 'อุปกรณ์', 'อวัยวะเทียม',
            'บริการโลหิต', 'บริการพยาบาล', 'ค่าบริการ',
            'ตรวจวินิจฉัย', 'ตรวจรังสี', 'ตรวจเลือด', 'ตรวจวิเคราะห์',
            'ค่าตรวจ', 'เทคนิคการแพทย์', 'พาธิวิทยา', 'รังสีวิทยา',
            'ค่าตรววิเคราะห์', 'ทางเทคนิค',
            'หัตถการ', 'ผ่าตัด', 'ทำหัตถการ',
            'ทันตกรรม', 'กายภาพ', 'ฝังเข็ม',
            'การทางการพยาบาล', 'พยาบาล',
            'ธรรมเนียม', 'บริการอื่น'
        ]
        
        # คำที่บ่งบอกถึงเวชภัณฑ์
        veshaphan_keywords = ['เวชภัณฑ์', 'อุปกรณ์', 'อวัยวะเทียม']
        
        lines = text.split('\n')
        
        for line_num, line in enumerate(lines, 1):
            if not line.strip():
                continue
            
            # Pattern สำหรับรายการค่าใช้จ่าย
            patterns = [
                # รูปแบบ 1: ค่าตรวจวิเคราะห์ทางเทคนิค[35101, 35109, 31001] 460.00
                r'([ก-๙a-zA-Z\s]+)\[([\d,\s]+)\]\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)',
                # รูปแบบ 2: ค่ายาในบัญชี 32.00
                r'([ก-๙a-zA-Z\s]+?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)',
                # รูปแบบ 3: ค่ายาในบัญชีทางเทคนิค (ไม่มีในแบบมือ) 168.00
                r'([ก-๙a-zA-Z\s]+?)\(([^)]+)\)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)'
            ]
            
            matched = False
            for pattern_idx, pattern in enumerate(patterns):
                matches = re.finditer(pattern, line.strip())
                
                for match in matches:
                    if pattern_idx == 0:  # รูปแบบที่มี [รหัส]
                        description = match.group(1).strip()
                        codes = match.group(2).strip()
                        amount_str = match.group(3).replace(',', '')
                    elif pattern_idx == 2:  # รูปแบบที่มี (รายละเอียด)
                        description = match.group(1).strip()
                        detail = match.group(2).strip()
                        amount_str = match.group(3).replace(',', '')
                        description = f"{description} ({detail})"
                    else:  # รูปแบบทั่วไป
                        description = match.group(1).strip()
                        amount_str = match.group(2).replace(',', '')
                    
                    try:
                        amount = float(amount_str)
                        
                        # ข้ามจำนวนเงินที่ไม่สมเหตุสมผล
                        if amount <= 0 or amount > 1000000:
                            continue
                        
                        # ตรวจสอบว่าเป็นรายการที่ต้องการหรือไม่
                        is_valid = False
                        for known in known_items:
                            if known in description:
                                is_valid = True
                                break
                        
                        # ตรวจสอบว่าเป็นรายการเวชภัณฑ์หรือไม่
                        is_veshaphan = False
                        for vkw in veshaphan_keywords:
                            if vkw in description:
                                is_veshaphan = True
                                break
                        
                        if is_valid and not matched:
                            item_dict = {
                                'line_number': line_num,
                                'description': description,
                                'amount': amount,
                                'is_veshaphan': is_veshaphan
                            }
                            items.append(item_dict)
                            
                            flag = "[เวชภัณฑ์]" if is_veshaphan else ""
                            print(f"  ✓ บรรทัด {line_num:3d}: {description:<60} {amount:>10,.2f} {flag}")
                            
                            matched = True
                            break
                            
                    except (ValueError, IndexError):
                        continue
                
                if matched:
                    break
        
        print(f"\n{'='*70}")
        print(f"พบรายการค่าใช้จ่าย: {len(items)} รายการ")
        
        if items:
            total_amount = sum(item['amount'] for item in items)
            veshaphan_items = [item for item in items if item['is_veshaphan']]
            veshaphan_amount = sum(item['amount'] for item in veshaphan_items)
            
            print(f"จำนวนเงินรวม: {total_amount:,.2f} บาท")
            print(f"รายการเวชภัณฑ์: {len(veshaphan_items)} รายการ, จำนวนเงิน: {veshaphan_amount:,.2f} บาท")
        
        print(f"{'='*70}\n")
        
        return items
    def extract_bill_items(self, text: str) -> List[Dict]:
        """
        Extract รายการค่าใช้จ่ายทั้งหมด
        Args:
            text: ข้อความจาก OCR
        Returns:
            list of dict รายการค่าใช้จ่าย
        """
        print(f"extract_bill_items")
        items = []
        # Pattern สำหรับรายการค่าใช้จ่าย
        # Format: เลขที่ รายการ จำนวนเงิน
        patterns = [
            # รายการที่มีตัวเลขนำหน้า
            r'(\d+)\s+(.+?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)\s*(?:บาท)?',
            # รายการที่มีคำอธิบายยาว
            r'(\d+)\.\s*(.+?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)',
        ]
        # รายการที่มักปรากฏในใบเสร็จ
        known_items = ['ค่าห้อง','อวัยวะเทียม','ยาและสารอาหาร', 'ยาที่นำไปใช้','ยาแผนไทย', 'เวชภัณฑ์', 'บริการโลหิต',  'ตรวจวินิจฉัย','ตรวจรังสี', 'อุปกรณ์', 'หัตถการ', 'บริการพยาบาล','ทันตกรรม', 'กายภาพ', 'ฝังเข็ม', 'ธรรมเนียม', 'บริการอื่น' ]
        lines = text.split('\n')
        for line in lines:
            # ข้ามบรรทัดว่าง
            if not line.strip():
                continue
        
            # ลองหาด้วย pattern
            for pattern in patterns:
                matches = re.finditer(pattern, line)
                for match in matches:
                    item_no = match.group(1)
                    description = match.group(2).strip()
                    amount_str = match.group(3).replace(',', '')
                
                    try:
                        amount = float(amount_str)
                    
                        # ตรวจสอบว่าเป็นรายการที่ต้องการหรือไม่
                        is_valid = False
                        for known in known_items:
                            if known in description:
                                is_valid = True
                                break
                    
                        if is_valid and amount > 0:
                            items.append({
                                'item_no': item_no,
                                'description': description,
                                'amount': amount,
                                'is_veshaphan': is_veshaphan  # เพิ่ม flag
                            })
                            print(f"✓✓✓✓✓✓✓ is_veshaphan ✓✓✓✓✓✓✓✓✓✓✓✓✓✓✓✓✓✓✓✓✓")
                    except ValueError:
                        continue
    
        return items
def main():
    """ตัวอย่างการใช้งาน"""
    
    print("="*70)
    print("FTP Medical Document Processor - Memory Stream Version")
    print("="*70)
    
    # ตั้งค่า FTP server
    FTP_HOST = '172.25.10.3'
    FTP_USER = 'u_supra_temp'
    FTP_PASS = 'u_supra_temp'
    
    # ตั้งค่า MSSQL Database
    DB_SERVER = '172.25.10.5'
    DB_USER = 'ekapop'
    DB_PASS = 'Ekartc2c51*'
    DB_NAME = 'BNG5_DBMS_FRONT'
    DB_DRIVER = '{ODBC Driver 17 for SQL Server}'
    
    # ตั้งค่า MySQL Database
    MYSQL_SERVER = '172.25.10.20'
    MYSQL_USER = 'root'
    MYSQL_PASS = 'Bng5linux1*'
    MYSQL_NAME = 'bangna'
    
    # สร้าง processor
    processor = FTPMedicalDocProcessorStream(
        ftp_host=FTP_HOST,
        ftp_user=FTP_USER,
        ftp_pass=FTP_PASS,
        db_server=DB_SERVER,
        db_user=DB_USER,
        db_pass=DB_PASS,
        db_name=DB_NAME,
        db_driver=DB_DRIVER,
        mysql_server=MYSQL_SERVER,
        mysql_user=MYSQL_USER,
        mysql_pass=MYSQL_PASS,
        mysql_name=MYSQL_NAME
    )
    
    # แก้ไข: เก็บ FTP connection แล้วปิดหลังใช้
    ftp = processor.connect_ftp()
    list_files_ftp = processor.list_files(ftp, directory='/supra_temp')
    ftp.quit()
    
    # ประมวลผลแต่ละไฟล์
    for file_path in list_files_ftp:
        result = processor.process_file_from_ftp('/supra_temp/' + file_path)
        
        print(f"\nสรุป:")
        print(f"- Memory used: {result['memory_used']:,} bytes")
        print(f"- PIDs found: {len(result['pid'])}")  # แก้จาก 'pid'
        print(f"- Patients found: {len(result['patients'])}")
        print()


if __name__ == '__main__':
    main()