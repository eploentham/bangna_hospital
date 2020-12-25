using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class OPBKKINS
    {
        public String HN { get; set; }
        public String INSCL { get; set; }
        public String SUBTYPE { get; set; }
        public String CID { get; set; }
        public String DATEIN { get; set; }
        public String DATEEXP { get; set; }
        public String HOSPMAIN { get; set; }
        public String HOSPSUB { get; set; }
        public String GOVCODE { get; set; }
        public String GOVNAME { get; set; }
        public String PERMITNO { get; set; }
        public String DOCNO { get; set; }
        public String OWNRPID { get; set; }
        public String OWNNAME { get; set; }

        /*
         * HN       หมายเลขประจำตัวผู้รับบริการ
         * INSCL    สิทธิการรักษาที่ใช้
                    UCS = สิทธิ UC
                    WEL = สิทธิ UC
                    OFC = ข้าราชการ
                    SSS = ประกันสังคม
                    LGO = อปท
                    SSI = ประกันสังคมทุพพลภาพ
        SUBTYPE     ระดับสิทธิของหลักประกัน
        CID         หมายเลขบัตรเพื่อตรวจสอบ
        DATEIN      วันเดือนปีที่มีสิทธิ ปีมีค่าเป็น ค.ศ.   yyyymmdd
        DATEEXP     วันนเดือนปีที่หมดสิทธิ ปีมีค่าเป็น ค.ศ.  yyyymmdd
        HOSPMAIN    รหัสหน่วยบริการประจำ
        HOSPSUB     รหัสหน่วยบริการปฐมภูม
        GOVCODE     รหัสหน่วยงานต้นสังกัดของผู้มีสิทธ                ปล่อยให้เป็นค่าว่างได
        GOVNAME     ชื่อหน่วยงานต้นสังกัดของผู้มีสิทธิ                 ปล่อยให้เป็นค่าว่างได
        PERMITNO    รหัส Claim Code/เลขอนุมัต                  ปล่อยให้เป็นค่าว่างได
        DOCNO       เลขที่หนังสือ                               ปล่อยให้เป็นค่าว่างได
        OWNRPID     เลขประจำตัวประชาชนของผู้มีสิทธิข้าราชการ/อปท       ปล่อยให้เป็นค่าว่างได
        OWNNAME     ชื่อ นามสกุลของผู้มีสิทธิข้าราชการ/อปท               ปล่อยให้เป็นค่าว่างได


        INSCL
        รหัส รายละเอียด
        UCS สิทธิหลักประกันสุขภาพ
        WEL สิทธิหลักประกันสุขภาพ (ยกเว้นการร่วมจ่าย)
        OFC สิทธิข้าราชการ
        SSS สิทธิประกันสังคม
        LGO สิทธิ อปท
        SSI สิทธิประกันสังคมทุพพลภาพ

         * */
    }
}
