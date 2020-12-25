using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class OPBKKAER
    {
        public String HN { get; set; }
        public String DATEOPD { get; set; }
        public String AUTHAE { get; set; }
        public String AEDATE { get; set; }
        public String AETIME { get; set; }
        public String AETYPE { get; set; }
        public String REFER_NO { get; set; }
        public String REFMAINI { get; set; }
        public String IREFTYPE { get; set; }
        public String REFMAINO { get; set; }
        public String OREFTYPE { get; set; }
        public String UCAE { get; set; }
        public String EMTYPE { get; set; }
        public String SEQ { get; set; }

        /*
        
        HN              หมายเลขประจำตัวผู้รับบริการ
        DATEOPD         วันที่ที่รับบริการ บันทึก ปีในค่าเป็น ค.ศ.              yyyymmdd
        AUTHAE          Claim code (ที่ไดจากการแจง ที่ web สปสช.)
        AEDATE          วันที่เกิดอุบัติเหต                                yyyymmdd
        AETIME          เวลาที่เกิดอุบัติเหตุ
        AETYPE          สิทธิการรักษาอื่น กรณีอุบัติเหตุ
                        V = ใช้ พรบ. ผู้ประสบภัยจากรถ
                        O = ใช้ พรบ. กองทุนเงินทดแทน
                        B = ใช้ทั้ง พรบ. ผู้ประสบภัย และ พรบ. กองทุน
                        เงินทดแทน
        REFER_NO        เลขที่ใบส่งต่อ (เพิ่มเติม)                         **กรณี optype 0,1
        REFMAINI        รหัสโรงพยาบาลที่ส่งผู้ป่วยมารับการรักษา (Refer In)   **กรณี optype 0,1
        IREFTYPE        รหัสวัตถุประสงค์ของการรับ Refer                  เช่น 1100 = วินิจฉัย + รักษา
                        ตำแหน่งที่ 1 = วินิจฉัย
                        ตำแหน่งที่ 2 = รักษา
                        ตำแหน่งที่ 3 = รับไวรักษาต่อเนื่อง
                        ตำแหน่งที่ 4 = ตามความต้องการของผู้ป่วย
                        1 = มี 0 = ไมมี
        REFMAINO        รหัสโรงพยาบาลที่ส่งต่อไป (Refer Out)
        OREFTYPE        รหัสวัตถุประสงค์ของการ Refer ออก                เช่น 1100 = วินิจฉัย + รักษา
                        ตำแหน่งที่ 1 = วินิจฉัย
                        ตำแหน่งที่ 2 = รักษา
                        ตำแหน่งที่ 3 = รับไวรักษาต่อเนื่อง
                        ตำแหน่งที่ 4 = ตามความต้องการของผู้ป่วย
                        1 = มี 0 = ไมมี
        UCAE            รหัส บงบอกการรักษากรณีอุบัติเหตุ/ฉุกเฉิน             OPTYPE = 0,1 UCAE = null OPTYPE = 2,3 UCAE = A / E
                        A = อุบัติเหตุ ( Accident / Accident +
                        Emergency )
                        E = ฉุกเฉิน ( Emergency )
                        ว่าง = ไมเป็น A / E
        EMTYPE          รหัสข้อบ่งชี้ของกรณีฉุกเฉินตามเงื่อนไข
                        1 = ตองการรักษาเป็นการดวน
                        2 = ตองผ่าตัดด่วน
                        3 = โรคที่คณะกรรมการกำหนด
        SEQ             รหัสการบริการที่กำหนดโดยโปรแกรมเรียงลำดับ ไม่ซ้ำกัน (ถ้ามีจะต้องมีทุกแฟ้มไฟล์ที่เกี่ยวข้องกับ ผู้ป่วยนอก :OPD)       ใช้เชื่อมโยงกับแฟ้ม OPD
         */
    }
}
