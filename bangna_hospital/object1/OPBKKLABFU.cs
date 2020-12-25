using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class OPBKKLABFU
    {
        public String HCODE { get; set; }
        public String HN { get; set; }
        public String PERSON_ID { get; set; }
        public String DATESERV { get; set; }
        public String SEQ { get; set; }
        public String LABTEST { get; set; }
        public String LABRESULT { get; set; }

        /*
        HCODE           HCODE
        HN              หมายเลขประจำตัวผู้บริการ
        PERSON_ID       รหัสประจำตัวประชาชนตามสำนักทะเบียนราษฎร
        DATESERV        วันที่ที่รับบริการ บันทึก ปีในค่าเป็น ค.ศ                       yyyymmdd
        SEQ             รหัสการบริการที่กำหนดโดยโปรแกรม                         ใช้เชื่อมโยงกับแฟ้มOPD
        LABTEST         รหัสการตรวจทางห้องปฏิบัติการ
                            01=ตรวจน้ำตาลในเลือด จากหลอดเลือดดำ หลังอดอาหาร
                            02=ตรวจน้ำตาลในเลือด จากหลอดเลือดดำ โดยไม่อดอาหาร
                            03=ตรวจน้ำตาลในเลือด จากเส้นเลือดฝอย หลังอดอาหาร
                            04=ตรวจน้ำตาลในเลือด จากเส้นเลือดฝอย โดยไม่อดอาหาร
                            05=ตรวจ HbA1C
                            06=ตรวจ Triglyceride
                            07=ตรวจ Total Cholesterol
                            08=ตรวจ HDL Cholesterol
                            09=ตรวจ LDL Cholesterol
                            10=ตรวจ BUN ในเลือด
                            11=ตรวจ Creatinine ในเลือด
                            12=ตรวจโปรตีน microalbumin ในปัสสาวะ
                            (ใน field ผลการตรวจใส่ค่า 0=negative, 1=trace, 2=positive)
                            13=ตรวจ CREATININE ในปัสสาวะ
                            14=ตรวจโปรตีน macroalbumin ในปัสสาวะ
                            (ใน field ผลการตรวจใส่ค่า 0=negative, 1=trace, 2=positive)
                            15=ตรวจหาค่า eGFR (ใช้สูตร CKD-EPI formula)
                            16=ตรวจ Hb
                            17=ตรวจ UPCR (Urine protein creatinine ratio)
                            18=ตรวจ K (กรณี CKD stage 3 ขึ้นไป หรือได้ยา ACEI//ARBs)
                            19=ตรวจ Bicarb (กรณี CKD stage 3 ขึ้นไป)
                            20=ตรวจ phosphate (กรณี CKD stage 3 ขึ้นไป)
                            21=ตรวจ PTH (กรณี CKD stage 3 ขึ้นไป)
        LABRESULT           ผลของการตรวจทางห้องปฏิบัติการ               ถ้า Result = 0 อนุญาต ให้เป็นค่าว่างได้

         */
    }
}
