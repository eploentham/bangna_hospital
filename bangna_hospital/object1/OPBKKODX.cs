using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class OPBKKODX
    {
        public String HN { get; set; }
        public String DATEDX { get; set; }
        public String CLINIC { get; set; }
        public String DIAG { get; set; }
        public String DXTYPE { get; set; }
        public String DRDX { get; set; }
        public String PERSON_ID { get; set; }
        public String SEQ { get; set; }

        /*
        HN              หมายเลขประจำตัวผู้รับบริการ
        DATEDX          วันเดือนปีที่วินิจฉัยโรค บันทึก ป ในค่า ค.ศ            yyyymmdd
        CLINIC          รหัสคลินิกที่รับบริการ                             5 หลัก ตามภาคผนวก
        DIAG            วินิจฉัยโรค ตามรหัส ICD 10
        DXTYPE          ชนิดของโรค ระบุ
                            1 = Primary Diagnosis
                            2 = โรคร่วม (Co morbidity)
                            3 = โรคแทรกซ้อน (Complication)
                            4 = อื่นๆ (Others)
                            5 = สาเหตุภายนอก (External Cause)
        DRDX            แพทย์ผู้รักษา ตามเลขที่ใบประกอบวิชาชีพเวชกรรม           รหัส ว.
        PERSON_ID       รหัสประจำตัวประชาชน ตามสำนักทะเบียนราษฎร
        SEQ             รหัสการบริการที่กำหนดโดยโปรแกรมเรียงลำดับไม่ซ้ำกัน       ใช้เชื่อมโยงกับแฟ้ม OPD
         */
    }
}
