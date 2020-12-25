using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class OPBKKCHT
    {
        public String HN { get; set; }
        public String DATEOPD { get; set; }
        public String ACTUALCHT { get; set; }
        public String TOTAL { get; set; }
        public String PAID { get; set; }
        public String PTTYPE { get; set; }
        public String PERSON_ID { get; set; }
        public String SEQ { get; set; }
        public String OPD_MEMO { get; set; }
        public String INVOICE_NO { get; set; }
        public String INVOICE_LT { get; set; }

        /*
        
        HN          หมายเลขประจำตัวผู้รับบริการ
        DATEOPD     วันที่คิดค่ารักษา วันที่จำหน่าย หรือวันที่ผู้ป่วย  เปลี่ยนสิทธิการรักษา บันทึกปีในค่า ค.ศ     yyyymmdd
        ACTUALCHT   จำนวนเงินค่ารักษารวมที่เรียกเก็บทั้งหมด  รวมทั้งส่วนที่ไม่ได้เบิกจากสปสช.
        TOTAL       จำนวนเงินค่ารักษารวม เป็นบาท ที่เรียกเก็บ
        PAID        จำนวนเงินที่ผู้ป่วยจ่ายเอง  ในกรณีที่โรงพยาบาลไมไดรับเงินไว = 0
        PTTYPE      รหัสสิทธิการรักษา ถ้าชำระเงินเอง = 10
        PERSON_ID   รหัสประจำตัวประชาชนตามสำนักทะเบียนราษฎร
        SEQ         รหัสการบริการที่กำหนดโดยโปรแกรม  เรียงลำดับไม่ซ้ำกัน     ใช้เชื่อมโยงกับแฟ้ม OPD
        OPD_MEMO    รายละเอียดค่าบริการและการรักษาเพิ่มเติม    (ถ้ามี)       Free-text
        INVOICE_NO  เลขที่อ้างอิงใบแจ้งหนี้ของหน่วยบริการ                *Reference ID
        INVOICE_LT  เลขที่อ้างอิงชุดข้อมูลใบแจ้งหนี้ ของหน่วยบริการ



        PTTYPE
        รหัส สิทธิการรักษา                  รหัสสิทธิประโยชน์
        A2 ใช้สิทธิเบิกหน่วยงานต้นสังกัดราชการ      OFC
        A3 สิทธิลดหย่อนประเภท ก*               OFC
        A4 สิทธิลดหย่อนประเภท ข*               OFC
        A5 สิทธิลดหย่อนประเภท ค*               OFC
        A6 สิทธิลดหย่อนประเภท ง*               OFC
        A7 ผู้ประกันตนตามพรบ. ประกันสังคม        SSS
        AA เด็ก 0-12 ปี                       UCS
        AB ผู้มีรายได้น้อย                       UCS
        AC นักเรียน                           UCS
        AD ผู้พิการ                            UCS
        AE ทหารผ่านศึก                        UCS
        AF ภิกษุ/ผู้นำศาสนา                     UCS
        AG ผู้สูงอายุ                           UCS
        AH บัตรชั่วคราว                        UCS
        AI บัตรประกันสุขภาพ ประชาชนทั่วไป        UCS
        AJ บัตรประกันสุขภาพ อสม                UCS
        AK บัตรประกันสุขภาพ ผู้นำชุมชน            UCS
        UC บัตรประกันสุขภาพถ้วนหน้า              UCS
         */
    }
}
