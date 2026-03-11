import re
from typing import Dict, List, Optional

class HospitalReceiptType:
    """ประเภทของใบเสร็จโรงพยาบาล"""
    RECEIPT_000_OPD = "RECEIPT_000_OPD"             # ใบเสร็จรูปแบบทั่วไป        STANDARD
    INVIOCE_000_IPD = "INVIOCE_000_IPD"             # ใบเสร็จรูปแบบโรงพยาบาลศูนย์การแพทย์       HOSPITAL_OFFICIAL
    RECEIPT_SIRIRAJ_000_OPD = "siriraj"             # ใบเสร็จโรงพยาบาลศิริราช
    RECEIPT_RAJAVITHI_000_OPD = "rajavithi"         # ใบเสร็จโรงพยาบาลราชวิถี
    PRAMONGKUTKLAO = "pramongkutklao"               # ใบเสร็จโรงพยาบาลพระมงกุฎเกล้า
    VAJIRA_RECEIPT_000_OPD = "vajira"               # ใบเสร็จโรงพยาบาลวชิระ
    SOMDET_CHAOPHRAYA = "somdet_chaophraya"         # สถาบันจิตเวชศาสตร์สมเด็จเจ้าพระยา
    CHULALONGKORN = "chulalongkorn"
    UNKNOWN = "UNKNOWN"

class ParserMixin:
    def detect_receipt_type(self, text: str) -> str:
        """
        ตรวจสอบประเภทของใบเสร็จ
        
        Args:
            text: ข้อความจาก OCR
            
        Returns:
            ประเภทของใบเสร็จ (HospitalReceiptType)
        """
        # คำสำคัญสำหรับใบเสร็จโรงพยาบาลศูนย์การแพทย์
        keywords_receipt_000_opd = [    'โรงพยาบาล',        'สมเด็จพระเทพรัตนราชสุดา',         'ใบเสร็จรับเงิน',  'คณะแพทยศาสตร์ มหาวิทยาลัยศรีนครินทรวิโรฒ'    ]
        keywords_invoice_000_ipd = [    'ศูนย์การ','ชูนย์การ',' ศูนย์การ',        'สมเด็จพระเทพรัตนราชสุดา', 'ใบสรุปค่ารักษา',  'คณะแพทยศาสตร์ มหาวิทยาลัยศรีนครินทรวิโรฒ'    ]
        siriraj_keywords = [            'คณะแพทยศาสตร์ศิริราชพยาบาล',            'ศิริราชพยาบาล',            'มหาวิทยาลัยมหิดล',            'เลขที่จ่ายยา',            'SM1-O-',            'ENT-O-'        ]
        rajavithi_000_keywords = [      'โรงพยาบาลราชวิถี',            'ราชวิถี',            'กรมการแพทย์',            'Ref.No.']
        pramongkutklao_keywords = [     'โรงพยาบาลพระมงกุฎเกล้า',            'พระมงกุฎเกล้า',            'วงที่',            'ที่ในเสร็จต้นฉบับ'        ]
        vajira_keywords = [             'วชิรพยาบาล',            'วชิริมพราชา',            'คณะแพทยศาสตร์วชิรพยาบาล',            'ถนนบำราศเสน',            'เขตดุสิต'        ]
        somdet_keywords = [    'สถาบันจิตเวชศาสตร์',    'สมเด็จเจ้าพระยา',    'กรมสุขภาพจิต',    'RX-RCPT-FORM-OPD']
        chula_keywords = [    'โรงพยาบาลจุฬาลงกรณ์',    'สภากาชาดไทย',    'KING CHULALONGKORN',    'THAI RED CROSS']


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
        for keyword in rajavithi_000_keywords:
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
        
        return HospitalReceiptType.UNKNOWN

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
        hospital_patterns = [            r'(โรงพยาบาล[^\n]+)',            r'(คณะแพทยศาสตร์[^\n]+)'        ]
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
        name_patterns = [   r'นาย\s+([^\n]+)', r'นาง\s+([^\n]+)',  r'นางสาว\s+([^\n]+)', r'เด็กชาย\s+([^\n]+)',  r'เด็กหญิง\s+([^\n]+)',r'น\.ส\.\s+([^\n]+)'    ]
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
            'receipt_type': HospitalReceiptType.RECEIPT_SIRIRAJ_000_OPD,
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
        receipt_num_patterns = [            r'เลขที่\s*(\d+R-[A-Z0-9-]+)',            r'(\d{3,4}R-[A-Z0-9-]+)'        ]
        for pattern in receipt_num_patterns:
            match = re.search(pattern, text)
            if match:
                result['receipt_number'] = match.group(1)
                break
        
        # 2. วันที่ (รูปแบบ: 2 ธันวาคม 2568)
        date_patterns = [            r'วันที่\s+(\d{1,2}\s+[ก-๙]+\s+\d{4})',            r'(\d{1,2}\s+ธันวาคม\s+\d{4})'        ]
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
        prescription_patterns = [  r'เลขที่จ่ายยา[:\s]+([A-Z0-9-]+)',            r'(SM1-O-\d+)',            r'(ENT-O-\d+)',            r'([A-Z]{2,3}\d*-O-\d+)'        ]
        for pattern in prescription_patterns:
            match = re.search(pattern, text)
            if match:
                result['prescription_no'] = match.group(1)
                break
        
        # 5. ชื่อผู้ป่วย
        name_patterns = [            r'ชื่อ\s+(นาง?|นางสาว|นาย|น\.ส\.|ด\.ช\.|ด\.ญ\.|เด็กชาย|เด็กหญิง)\s+([^\n]+)',       r'(นาง?|นางสาว|นาย)\s+([ก-๙]+\s+[ก-๙]+)'        ]
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
        receipt_num_patterns = [            r'เลขที่\s*(\d{5})/(\d{3})',            r'เลขที่[:\s]*(\d{5})',            r'(\d{5})/(\d{3})'        ]
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
        ref_patterns = [            r'Ref\.No\.(\d+/\d+)',            r'Ref\.No\s*[.:]?\s*(\d+/\d+)'        ]
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
        hn_patterns = [            r'HN[:\s]*(\d{6}-\d{2})',            r'เลขประจำตัวผู้ป่วย[:\s]*(\d{6}-\d{2})',            r'(\d{6}-\d{2})'        ]
        for pattern in hn_patterns:
            match = re.search(pattern, text)
            if match:
                hn_candidate = match.group(1)
                # ตรวจสอบว่าไม่ใช่เลขบัตรประชาชน
                if not re.match(r'\d{13}', hn_candidate.replace('-', '')):
                    result['hn'] = hn_candidate
                    break
        
        # 5. AN (รูปแบบ: 230943) - สำหรับผู้ป่วยใน
        an_patterns = [            r'AN[:\s]*(\d{6})',            r'AN\s*[:]?\s*(\d{6})'        ]
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
        payment_patterns = [            r'ชำระเป็น\s*(บัตรเครดิต|เงินสด|เงินโอน)',            r'(บัตรเครดิต|เงินสด)'        ]
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
        receipt_num_patterns = [            'เลขที่\s*(\d{6})\s*/\s*(\d{2})',            r'(\d{6})/(\d{2})\s*แผ่นที่'        ]
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
        vong_patterns = [            r'วงที่\s*(\d{6})',            r'วงที่[:\s]*(\d{6})'        ]
        for pattern in vong_patterns:
            match = re.search(pattern, text)
            if match:
                result['vong_number'] = match.group(1)
                break
        
        # 4. ที่ในเสร็จต้นฉบับ (รูปแบบ: 32709404)
        doc_patterns = [    r'ที่ในเสร็จต้นฉบับ\s*(\d{8})',    r'ต้นฉบับ\s*(\d{8})',    r'ที่ใบเสร็จคอมฯ\s*(\d{8})',    r'ที่ใบเสร็จคอม\s*(\d{8})']

        for pattern in doc_patterns:
            match = re.search(pattern, text)
            if match:
                result['doc_number'] = match.group(1)
                break
        
        # 5. วันที่ (รูปแบบ: 02 ธันวาคม 2568)
        date_patterns = [  r'วันที่\s+(\d{1,2})\s+([ก-๙]+)\s+(\d{4})',  r'(\d{1,2})\s+(ธันวาคม|มกราคม|กุมภาพันธ์|มีนาคม|เมษายน|พฤษภาคม|มิถุนายน|กรกฎาคม|สิงหาคม|กันยายน|ตุลาคม|พฤศจิกายน)\s+(\d{4})' ]
        for pattern in date_patterns:
            match = re.search(pattern, text)
            if match:
                if match.lastindex >= 3:
                    result['receipt_date'] = f"{match.group(1)} {match.group(2)} {match.group(3)}"
                break
        
        # 6. HN (รูปแบบ: 21695/68) - 5 หลัก/2 หลัก
        hn_patterns = [            r'HN[:\s]*(\d{5}/\d{2})',            r'HN[:\s]*(\d{5})/(\d{2})'        ]
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
        receipt_patterns = [            r'เลขที่[:\s]*(\d{6}\s*-\s*\d{7}/\d{2})',            r'(\d{6}\s*-\s*\d{7}/\d{2})'        ]
        for pattern in receipt_patterns:
            match = re.search(pattern, text)
            if match:
                result['receipt_number'] = match.group(1).replace(' ', '')
                break
        
        # 2. ฉบับ/เล่ม ที่ปีงบ
        volume_patterns = [            r'ฉบับ/เล่ม[:\s]+ที่ปีงบ[:\s]*([^\n]+)',            r'ฉบับ/เล่ม ที่ปีงบ:\s*([^\n]+)'        ]
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
        date_patterns = [     r'วันที่[:\s]*(\d{1,2}/\d{1,2}/\d{4}\s+\d{2}:\d{2}:\d{2})',      r'(\d{1,2}/\d{1,2}/\d{4}\s+\d{2}:\d{2}:\d{2})'     ]
        for pattern in date_patterns:
            match = re.search(pattern, text)
            if match:
                result['receipt_date'] = match.group(1)
                break
        
        # 5. เลขที่โรงพยาบาล (รูปแบบ: 8215/66)
        hospital_num_pattern = r'เลขที่โรงพยาบาล[:\s]*(\d{4,5}/\d{2})'
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
            'ค่าห้อง', 'ค่าห้องและอาหาร', 'ค่าห้องพิเศษ',            'ค่ายา', 'ยาและสารอาหาร', 'ยาที่นำไปใช้', 'ยาแผนไทย',
            'ยาในบัญชี', 'ยานอกบัญชี', 'ค่ายาในบัญชี', 'ค่ายานอกบัญชี',            'เวชภัณฑ์', 'อุปกรณ์', 'อวัยวะเทียม',
            'บริการโลหิต', 'บริการพยาบาล', 'ค่าบริการ',            'ตรวจวินิจฉัย', 'ตรวจรังสี', 'ตรวจเลือด', 'ตรวจวิเคราะห์',
            'ค่าตรวจ', 'เทคนิคการแพทย์', 'พาธิวิทยา', 'รังสีวิทยา',            'ค่าตรววิเคราะห์', 'ทางเทคนิค',
            'หัตถการ', 'ผ่าตัด', 'ทำหัตถการ',            'ทันตกรรม', 'กายภาพ', 'ฝังเข็ม',
            'การทางการพยาบาล', 'พยาบาล',            'ธรรมเนียม', 'บริการอื่น'
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
                        veshaphan_keywords = ['เวชภัณฑ์', 'อุปกรณ์', 'อวัยวะเทียม']
                        is_veshaphan = any(kw in description for kw in veshaphan_keywords)
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
    def extract_siriraj_bill_items(self, text: str) -> List[Dict]:
        items = self.extract_siriraj_receipt_items_01(text)  # format มีรหัส
        if items:
            return items
        items = self.extract_siriraj_receipt_items_02(text)  # Thai/English any desc
        if items:
            return items
        # fallback format ไม่มีรหัส
        items = []  # reset
        pattern = r'(ค่า[^\n\d]+?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)'
        matches = re.findall(pattern, text)
        for match in matches:
            description = match[0].strip()
            claimable = float(match[1].replace(',', ''))
            non_claimable = float(match[2].replace(',', ''))
            veshaphan_keywords = ['เวชภัณฑ์', 'อุปกรณ์', 'อวัยวะเทียม', 'ค่ายา']
            is_veshaphan = any(kw in description for kw in veshaphan_keywords)
            items.append({
                'description': description,
                'claimable_amount': claimable,
                'non_claimable_amount': non_claimable,
                'is_veshaphan': is_veshaphan
            })
        return items
    def extract_rajavithi_bill_items(self, text: str) -> List[Dict]:
        return self.extract_bill_items(text)

    def extract_pramongkutklao_bill_items(self, text: str) -> List[Dict]:
        """
        แยก bill items จากใบเสร็จพระมงกุฎเกล้า
        รูปแบบ: description จำนวน X หน่วย/กล่อง/BOT  [เบิกไม่ได้]  เบิกได้
        เช่น: OPTIVE FUSION UD EYE DROPS... จำนวน 2 กล่อง  940.00
        """
        items = []
        veshaphan_keywords = ['เวชภัณฑ์', 'อุปกรณ์', 'อวัยวะเทียม', 'ยา', 'DROPS', 'TABLET', 'CAPSULE']
        # description + จำนวน + unit + optional(non_claimable) + claimable
        pattern = r'([^\n]+?)\s+จำนวน\s+(\d+)\s+(หน่วย|กล่อง|BOT)\s+(?:(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)\s+)?(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)'
        for match in re.finditer(pattern, text):
            description = match.group(1).strip()
            qty = int(match.group(2))
            unit = match.group(3)
            non_claimable = float(match.group(4).replace(',', '')) if match.group(4) else 0.0
            claimable = float(match.group(5).replace(',', ''))
            items.append({
                'description': description,
                'qty': qty,
                'unit': unit,
                'non_claimable_amount': non_claimable,
                'claimable_amount': claimable,
                'is_veshaphan': any(kw in description for kw in veshaphan_keywords)
            })
        return items

    def extract_vajira_bill_items(self, text: str) -> List[Dict]:
        """
        แยก bill items จากใบเสร็จวชิรพยาบาล
        คอลัมน์: รายการ | จำนวน | ราคาต่อหน่วย | เบิกไม่ได้ | เบิกได้
        """
        items = []
        veshaphan_keywords = ['เวชภัณฑ์', 'อุปกรณ์', 'อวัยวะเทียม', 'ค่ายา', 'บัญชียา']

        # format sub-item: XXXXX ค่า... จำนวน ราคา/หน่วย เบิกไม่ได้ เบิกได้
        pattern_code = r'(\d{5})\s+([A-Za-zก-๙(][^\n\d]*?)\s+(\d+)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)'
        for match in re.finditer(pattern_code, text):
            description = match.group(2).strip()
            items.append({
                'code': match.group(1),
                'description': description,
                'qty': int(match.group(3)),
                'unit_price': float(match.group(4).replace(',', '')),
                'non_claimable_amount': float(match.group(5).replace(',', '')),
                'claimable_amount': float(match.group(6).replace(',', '')),
                'is_veshaphan': any(kw in description for kw in veshaphan_keywords)
            })

        # format category: ค่า... เบิกไม่ได้ เบิกได้
        pattern_cat = r'^(ค่า[^\n\d]+?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)$'
        for match in re.finditer(pattern_cat, text, re.MULTILINE):
            description = match.group(1).strip()
            items.append({
                'code': None,
                'description': description,
                'non_claimable_amount': float(match.group(2).replace(',', '')),
                'claimable_amount': float(match.group(3).replace(',', '')),
                'is_veshaphan': any(kw in description for kw in veshaphan_keywords)
            })

        return items
    def extract_siriraj_receipt_items_01(self, text: str) -> List[Dict]:
        """
        แยก bill items จากใบเสร็จศิริราช format 01
        รูปแบบ: XXXXX ค่า... | เบิกได้ | เบิกไม่ได้
        เช่น: 55020 ค่าบริการผู้ป่วยนอก  50.00  50.00
        """
        items = []
        pattern = r'(\d{5})\s+(ค่า[^\n\d]+?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)'
        matches = re.findall(pattern, text)
        for match in matches:
            code = match[0].strip()
            description = match[1].strip()
            claimable = float(match[2].replace(',', ''))
            non_claimable = float(match[3].replace(',', ''))
            veshaphan_keywords = ['เวชภัณฑ์', 'อุปกรณ์', 'อวัยวะเทียม', 'ค่ายา']
            is_veshaphan = any(kw in description for kw in veshaphan_keywords)
            items.append({
                'code': code,
                'description': description,
                'claimable_amount': claimable,
                'non_claimable_amount': non_claimable,
                'is_veshaphan': is_veshaphan
            })
        return items
    def extract_siriraj_receipt_items_02(self, text: str) -> List[Dict]:
        """
        แยก bill items จากใบเสร็จศิริราช format 02
        รูปแบบ: XXXXX description (Thai/English) | เบิกได้ | เบิกไม่ได้
        เช่น: 45901  Using Gd contrast medium  2,500.00  250.00
              45101  การตรวจสมองด้วยเครื่องเอ็มอาร์ไอ  8,000.00
        """
        items = []
        veshaphan_keywords = ['เวชภัณฑ์', 'อุปกรณ์', 'อวัยวะเทียม', 'ค่ายา']
        # เบิกไม่ได้ อาจว่างเปล่า → optional
        pattern = r'(\d{5})\s+([A-Za-zก-๙][^\n\d]+?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)(?:\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})))?'
        for match in re.finditer(pattern, text):
            code = match.group(1)
            description = match.group(2).strip()
            claimable = float(match.group(3).replace(',', ''))
            non_claimable = float(match.group(4).replace(',', '')) if match.group(4) else 0.0
            items.append({
                'code': code,
                'description': description,
                'claimable_amount': claimable,
                'non_claimable_amount': non_claimable,
                'is_veshaphan': any(kw in description for kw in veshaphan_keywords)
            })
        return items
    def extract_somdet_chaophraya_bill_items(self, text: str) -> List[Dict]:
        items = []
        veshaphan_keywords = ['เวชภัณฑ์', 'อุปกรณ์', 'อวัยวะเทียม', 'ค่ายา', 'ยา']
        # sub-items เริ่มด้วย - และมี amount ท้ายบรรทัด
        pattern = r'-\s+([^\n\d]+?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)$'
        for match in re.finditer(pattern, text, re.MULTILINE):
            description = match.group(1).strip()
            amount = float(match.group(2).replace(',', ''))
            is_non_claimable = 'เบิกไม่ได้' in description
            items.append({
                'description': description,
                'non_claimable_amount': amount if is_non_claimable else 0.0,
                'claimable_amount': 0.0 if is_non_claimable else amount,
                'is_veshaphan': any(kw in description for kw in veshaphan_keywords)
            })
        return items
    def extract_chulalongkorn_bill_items(self, text: str) -> List[Dict]:
        items = []
        veshaphan_keywords = ['เวชภัณฑ์', 'อุปกรณ์', 'อวัยวะเทียม', 'ค่ายา', 'ยา']
        # format: ลำดับ. [code] description  claimable  [non_claimable]
        pattern = r'^\s*\d+\.\s*(?:(\d{5})\s+)?(ค่า[^\n\d]+?)\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)(?:\s+(\d{1,3}(?:,\d{3})*(?:\.\d{2})))?$'
        for match in re.finditer(pattern, text, re.MULTILINE):
            code = match.group(1)
            description = match.group(2).strip()
            claimable = float(match.group(3).replace(',', ''))
            non_claimable = float(match.group(4).replace(',', '')) if match.group(4) else 0.0
            items.append({
                'code': code,
                'description': description,
                'claimable_amount': claimable,
                'non_claimable_amount': non_claimable,
                'is_veshaphan': any(kw in description for kw in veshaphan_keywords)
            })
        return items




