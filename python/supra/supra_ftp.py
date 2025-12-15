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
        # ตรวจสอบว่ามีคำสำคัญหรือไม่
        keyword_count_receipt_000_opd = 0
        keyword_count_invoice_000_ipd = 0
        for keyword in keywords_receipt_000_opd:
            if keyword in text:
                keyword_count_receipt_000_opd += 1
        for keyword in keywords_invoice_000_ipd:
            if keyword in text:
                keyword_count_invoice_000_ipd += 1
        # ถ้าพบคำสำคัญ 2 คำขึ้นไป แสดงว่าเป็นใบเสร็จโรงพยาบาลศูนย์
        if keyword_count_receipt_000_opd >= 2:
            return HospitalReceiptType.RECEIPT_000_OPD
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