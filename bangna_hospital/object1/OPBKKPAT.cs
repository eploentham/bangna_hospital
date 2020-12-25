using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class OPBKKPAT
    {
        public String HCODE { get; set; }
        public String HN { get; set; }
        public String CHANGWAT { get; set; }
        public String AMPHUR { get; set; }
        public String DOB { get; set; }
        public String SEX { get; set; }
        public String MARRIAGE { get; set; }
        public String OCCUPA { get; set; }
        public String NATION { get; set; }
        public String PERSON_ID { get; set; }
        public String NAMEPAT { get; set; }
        public String TITLE { get; set; }
        public String FNAME { get; set; }
        public String LNAME { get; set; }
        public String IDTYPE { get; set; }

        /*
         * 
         *  HCODE    HCODE
            HN          หมายเลขประจำตัวผู้รับบริการ
            CHANGWAT    ตามรหัสมหาดไทย                   มอก.1099-2535
            AMPHUR      ตามรหัสมหาดไทย                   มอก.1099-2535
            DOB         บันทึกวันเดือนปีเกิด ปี มีค่าเป็น ค.ศ
            SEX         รหัสเพศ
                        1 = เพศชาย
                        2 = เพศหญิง
            MARRIAGE    รหัสสถานภาพสมรส                  ปล่อยให้เป็นค่าว่างได
                        1 = โสด
                        2 = สมรส
                        3 = หม้าย
                        4 = หย่า
                        5 = แยกกันอยู่
                        6 = สมณะ
                        9 = ไม่ทราบ
            OCCUPA      อาชีพ
            NATION      สัญชาต
            PERSON_ID   รหัสประจำตัวประชาชน ตามสำนักทะเบียนราษฎร
            NAMEPAT     ชื่อ – สกุล ผู้ป่วย
            TITLE       คำนำหน้า
            FNAME       ชื่อ
            LNAME       นามสกุล
            IDTYPE      ประเภทบัตร
                        1 = บัตรประชาชน
                        2 = หนังสือเดินทาง
                        3 = หนังสือต่างด้าว
                        4 = หนังสือ / เอกสารอื่นๆ
         */
    }
}
