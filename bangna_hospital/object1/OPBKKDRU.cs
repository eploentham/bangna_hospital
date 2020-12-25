using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class OPBKKDRU
    {
        public String HCODE { get; set; }
        public String HN { get; set; }
        public String CLINIC { get; set; }
        public String PERSON_ID { get; set; }
        public String DATESERV { get; set; }
        public String DID { get; set; }
        public String DIDNAME { get; set; }
        public String DIDSTD { get; set; }
        public String TMTCODE { get; set; }
        public String AMOUNT { get; set; }
        public String DRUGPRICE { get; set; }
        public String PRICE_EXT { get; set; }
        public String DRUGCOST { get; set; }
        public String UNIT { get; set; }
        public String UNIT_PACK { get; set; }
        public String SEQ { get; set; }
        public String PROVIDER { get; set; }

        /*
        HCODE        รหัสสถานพยาบาล
        HN          หมายเลขประจำตัวผู้บริการ
        CLINIC      รหัสคลินิกที่รับบริการ                             รหัส5หลักตามภาคผนวก
        PERSON_ID   รหัสประจำตัวประชาชนตามสำนักทะเบียนราษฎร
        DATESERV    วันที่ที่รับบริการ บันทึก ปีในค่าเป็น ค.ศ                   รหัส5หลักตามภาคผนวก
        DID         รหัสยาที่หน่วยบริการใช้ (WorkingCode)             Working Code
        DIDNAME     ชื่อยาที่ใช้อยู่ปัจจุบันสัมพันธ์กับ DID
        DIDSTD      รหัสยาที่กำหนดเป็น 24 หลัก หากไม่ทราบรหัส ผู้ผลิตให้ใส่ 0 ต่อท้าย 5 หลัก ให้ครบ 24 หลัก          ใช้กรณีแพทย์แผนไทย
        TMTCODE     รหัสยา ตามมาตรฐาน (Thai Medicines Terminology - TMT)             ศูนย์พัฒนามาตรฐานระบบข้อมูลสุขภาพไทย (ศมสท) www.this.or.th
        AMOUNT      จำนวนยาที่จ่าย
        DRUGPRICE   ราคายาที่ขอเบิก                            (ไม่ใช่ราคาต่อหน่วย)
        PRICE_EXT   ราคายาส่วนที่เบิกไม่ได                       (ไม่ใช่ราคาต่อหน่วย)
        DRUGCOST    ราคาทุน
        UNIT        หน่วยนับของยาที่ใช้ในการจ่ายยา เช่น เม็ด ขวด
        UNIT_PACK   ขนาดบรรจุต่อหน่วยนับที่แสดงใน Field UNIT ใช้ สำหรับยาที่มีการแบ่งบรรจุเพื่อการจ่ายยาที่ เหมาะสม เช่น ยาน้ำ หรือ ยากลุ่มที่มีทะเบียนยา เดียวกันแต่มีขนาดบรรจุต่างกันของบางบริษัท             สามารถเป็นค่า NULL ได้ สำหรับยาที่ไม่จำเป็นต้อง มีข้อมูลใน Field นี้
        SEQ         รหัสการบริการที่กำหนดโดยโปรแกรม                 ใช้เชื่อมโยงกับแฟ้มOPD
        PROVIDER    เภสัชกรที่จ่ายยา ตามเลขที่ใบประกอบวิชาชีพ เวชกรรม               กรณีที่ไม่มีข้อมูล ให้เป็นค่าว่างหรือN/Aได

         */
    }
}
