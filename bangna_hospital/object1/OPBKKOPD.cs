using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class OPBKKOPD
    {
        public String PERSON_ID { get; set; }
        public String HN { get; set; }
        public String DATEOPD { get; set; }
        public String TIMEOPD { get; set; }
        public String SEQ { get; set; }
        public String UUC { get; set; }
        public String DETAIL { get; set; }
        public String BTEMP { get; set; }
        public String SBP { get; set; }
        public String DBP { get; set; }
        public String PR { get; set; }
        public String RR { get; set; }
        public String OPTYPE { get; set; }
        public String TYPEIN { get; set; }
        public String TYPEOUT { get; set; }
        public String CLAIM_CODE { get; set; }

        /*
         * 
        PERSON_ID       รหัสประจำตัวประชาชน ตามสำนักทะเบียนราษฎร์
        HN              หมายเลขประจำตัวผู้รับบริการ
        DATEOPD         วันที่ที่รับบริการ บันทึก ป ในค่าเป็น ค.ศ              yyyymmdd
        TIMEOPD         เวลาที่บันทึก เป็น ชั่วโมง นาท
        SEQ             รหัสการบริการที่กำหนดโดยโปรแกรมเรียงลำดับไม่ซ้ำกัน       Free-text
        UUC             การใช้สิทธิ (เพิ่มเติม) 1 = ใช้สิทธิ 2 = ไมใช้สิทธิ
        DETAIL          อาการสำคัญ           Free-text
        BTEMP           อุณหภูมิร่างกาย
        SBP             ความดันโลหิตค่าตัวบน
        DBP             ความดันโลหิตค่าตัวล่าง
        PR              อัตราการเต้นหัวใจ
        RR              อัตราการหายใจ
        OPTYPE          ประเภทการให้บริการ
                        0 = Refer ในบัญชีเครือข่ายเดียวกัน
                        1 = Refer นอกบัญชีเครือข่าย
                        2 = AE ในบัญชีเครือข่าย
                        3 = AE นอกบัญชีเครือข่าย
                        4 = OP พิการ
                        5 = OP บัตรตัวเอง
                        6 = Clearing House ศบส
                        7 = OP อื่นๆ (Individual data)
                        8 = ผู้ป่วยกึ่ง OP / IP (NONI)
                        9 = บริการแพทย์แผนไทย
        TYPEIN          ประเภทการมารับบริการ
                        1=มารับบริการเอง
                        2=มารับบริการตามนัดหมาย
                        3=ได้รับการส่งต่อจากสถานพยาบาลอื่น
                        4=ได้รับการส่งตัวจากบริการ EMS
        TYPEOUT         สถานะผู้ป่วยเมื่อเสร็จสิ้นบริการ
                        1=จำหน่ายกลับบ้าน
                        2=รับไว้รักษาต่อIP
                        3=Refer ต่อ
                        4=เสียชีวิต
                        5=เสียชีวิตก่อนมาถึง
                        6=เสียชีวิตระหว่างส่งต่อไปยังที่อื่น
                        7=ปฏิเสธการรักษา
                        8=หนีกลับ
        CLAIM_CODE      รหัส ClaimCode เข้ารับการบริการ ที่ออกโดย สปสช.
                            *ถ้ามีการขอเบิกชดเชยค่าบริการต้องส่งแนบมาด้วย
         */
    }
}
