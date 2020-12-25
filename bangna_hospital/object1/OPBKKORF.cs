using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class OPBKKORF
    {
        public String HN { get; set; }
        public String DATEOPD { get; set; }
        public String CLINIC { get; set; }
        public String REFER { get; set; }
        public String REFERTYPE { get; set; }
        public String REFERDATE { get; set; }
        public String SEQ { get; set; }

        /*
        HN          หมายเลขประจำตัวผู้รับบริการ
        DATEOPD     วันที่มารับบริการ บันทึก ป ในค่าเป็น ค.ศ.        รหัสคลินิกที่รับบริการ
        CLINIC      รหัสคลินิกที่รับบริการ                         5 หลัก ตามภาคผนวก
        REFER       รหัสสถานพยาบาลหรือคลินิกที่เกี่ยวข้องกับการส่งต่อ       (กรณี optype=0,1)
        REFERTYPE   ประเภทการส่งต่อ
                    (1 = รับเข้า, 2 = ส่งออก)
                    (กรณี optype=0,1) refertype = 1 เสม
        REFERDATE   วันที่ส่งต่อ
        SEQ         รหัสการบริการที่กำหนดโดยโปรแกรมเรียงลำดับไม่ซ้ำกัน       ใช้เชื่อมโยงกับแฟ้มOPD
         */
    }
}
