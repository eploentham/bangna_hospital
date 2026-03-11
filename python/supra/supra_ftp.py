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
from typing import Optional, List, Tuple, Dict
import mysql.connector
from datetime import datetime
import hashlib
from supra_ftp_ftp import FTPMixin
from supra_ftp_parser import ParserMixin, HospitalReceiptType
from supra_ftp_ocr import OCRMixin

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
class FTPMedicalDocProcessorStream(FTPMixin, ParserMixin,OCRMixin):
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
                'what_image_is': result_data.get('what_image_is', ''),
                'branch': result_data.get('branch', '')
            }
            
            # INSERT query
            query = """
            INSERT INTO t_supra_image 
            (file_path, file_type, pid_list, hn_list, an_list, text_content, memory_used, time_start, time_end, ftp_file_path_archive, md5, what_image_is, branch_code) 
            VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)
            """
            
            cursor.execute(query, (                data['file_path'],                data['file_type'],                data['pid_list'],                data['hn_list'],
                data['an_list'],                data['text_content'],                data['memory_used'],                data['time_start'],                data['time_end'],
                data['ftp_file_path_archive'],                data['md5'],                data['what_image_is'], data['branch']
            ))
            conn.commit()
            print(f"✓ บันทึกข้อมูลลง MySQL (table: t_supra_image, row_id: {cursor.lastrowid})")
            cursor.close()
            conn.close()
            return True
        except Exception as e:
            print(f"✗ ไม่สามารถบันทึกข้อมูลลง MySQL: {e}")
            return False
    
    def process_file_from_ftp(self, ftp_file_path: str, branch: str = '') -> dict:
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
            'has_veshaphan': '',            'what_image_is': '','branch': branch,        }
        
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
            if what_image_is == HospitalReceiptType.INVIOCE_000_IPD:
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
                
            elif what_image_is == HospitalReceiptType.RECEIPT_SIRIRAJ_000_OPD:
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
            elif what_image_is == HospitalReceiptType.RECEIPT_RAJAVITHI_000_OPD:
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
            elif what_image_is == HospitalReceiptType.SOMDET_CHAOPHRAYA:
                somdet_data = self.extract_somdet_chaophraya_receipt(result['text'])
                result['somdet_receipt_data'] = somdet_data
                result['HN'] = [somdet_data['hn']] if somdet_data['hn'] else []
                bill_items = somdet_data['bill_items']
            elif what_image_is == HospitalReceiptType.CHULALONGKORN:
                chula_data = self.extract_chulalongkorn_receipt(result['text'])
                result['HN'] = [chula_data['hn']] if chula_data['hn'] else []
                bill_items = chula_data['bill_items']
            # แก้เป็น
            else:
                print(f"⚠ ยังไม่มีในระบบ: ประเภทใบเสร็จ '{what_image_is}'")
                print(f"⚠ กรุณาเพิ่ม HospitalReceiptType และ extract method สำหรับโรงพยาบาลนี้")
                result['what_image_is'] = what_image_is
                bill_items = []
            
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
    
def main():
    """ตัวอย่างการใช้งาน"""    
    print("="*70)
    print("FTP Medical Document Processor - Memory Stream Version")
    print("="*70)
    # ถามสาขาก่อน run
    print("\nสาขาที่รองรับ:")
    print("  1 = สาขาบางนา1")
    print("  2 = สาขาบางนา2")
    print("  5 = สาขาบางนา5")
    branch_input = input("\nรูปมาจากสาขาไหน (กรอกหมายเลข): ").strip()
    branch_map = {'1': 'บางนา1', '2': 'บางนา2', '5': 'บางนา5'}
    branch_name = branch_map.get(branch_input, branch_input)
    print(f"✓ สาขา: {branch_name}\n")

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
        ftp_host=FTP_HOST,        ftp_user=FTP_USER,        ftp_pass=FTP_PASS,
        db_server=DB_SERVER,        db_user=DB_USER,        db_pass=DB_PASS,
        db_name=DB_NAME,        db_driver=DB_DRIVER,        mysql_server=MYSQL_SERVER,
        mysql_user=MYSQL_USER,        mysql_pass=MYSQL_PASS,        mysql_name=MYSQL_NAME
    )
    
    # แก้ไข: เก็บ FTP connection แล้วปิดหลังใช้
    ftp = processor.connect_ftp()
    list_files_ftp = processor.list_files(ftp, directory='/supra_temp')
    ftp.quit()
    
    # ประมวลผลแต่ละไฟล์
    for file_path in list_files_ftp:
        result = processor.process_file_from_ftp('/supra_temp/' + file_path, branch=branch_name)
        print(f"\nสรุป:")
        print(f"- Memory used: {result['memory_used']:,} bytes")
        print(f"- PIDs found: {len(result['pid'])}")  # แก้จาก 'pid'
        print(f"- Patients found: {len(result['patients'])}")
        print()
if __name__ == '__main__':
    main()