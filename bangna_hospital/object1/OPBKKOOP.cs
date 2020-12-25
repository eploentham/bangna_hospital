using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class OPBKKOOP
    {
        public String HN { get; set; }
        public String DATEOPD { get; set; }
        public String CLINIC { get; set; }
        public String OPER { get; set; }
        public String SERVPRICE { get; set; }
        public String DROPID { get; set; }
        public String PERSON_ID { get; set; }
        public String SEQ { get; set; }

        /*
         
        HN          หมายเลขประจำตัวผู้รับบริการ
        DATEOPD     วันที่ที่รับบริการ บันทึก ป ในค่าเป็น ค.ศ.     yyyymmdd    
        CLINIC      รหัสคลินิกที่รับบริการ             5 หลัก ตามภาคผนวก
        OPER        รหัสหัตถการตาม ICD 9 CM
        SERVPRICE   ราคาค่าบริการหัตถการ ทศนิยม2ตำแหน่ง
        DROPID      แพทย์ผู้รักษา ตามเลขที่ใบประกอบวิชาชีพ เวชกรรม (สามารถใช้ชื่อ DROP หรือ DROPID ได้)
        PERSON_ID   รหัสประจำตัวประชาชนตามสำนักทะเบียนราษฎร
        SEQ         รหัสการบริการที่กำหนดโดยโปรแกรม เรียงลำดับไม่ซ้ำกัน          ใช้เชื่อมโยงกับแฟ้ม OPD
         */
    }
}
