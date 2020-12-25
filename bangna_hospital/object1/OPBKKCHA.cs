using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class OPBKKCHA
    {
        public String HN { get; set; }
        public String DATEOPD { get; set; }
        public String CHRGITEM { get; set; }
        public String AMOUNT { get; set; }
        public String AMOUNT_EXT { get; set; }
        public String PERSON_ID { get; set; }
        public String SEQ { get; set; }

        /*
         
        HN          หมายเลขประจำตัวผู้รับบริการ
        DATEOPD     วันที่คิดค่ารักษา บันทึก ปีในค่าเป็น ค.ศ.
        CHRGITEM    ชนิดของบริการที่คิดค่ารักษา ตามรหัสที่กำหนด           ใช้รหัสแบบละเอียด  ตามภาคผนวก
        AMOUNT      จำนวนเงิน คารักษาของบริการรายการนั้น   เป็นบาท ในส่วนที่เบิกได้
        AMOUNT_EXT  จำนวนเงิน คารักษาของบริการรายการนั้น  เป็นบาท ในส่วนที่เบิกไม่ได้
        PERSON_ID   รหัสประจำตัวประชาชนตามสำนักทะเบียนราษฎร
        SEQ         รหัสการบริการที่กำหนดโดยโปรแกรม  เรียงลำดับไม่ซ้ำกัน     ใช้เชื่อมโยงกับแฟ้ม OPD
         */
    }
}
