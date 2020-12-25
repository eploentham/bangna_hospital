using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class OPBKKCHAD
    {
        public String HN { get; set; }
        public String DATESERV { get; set; }
        public String SEQ { get; set; }
        public String CLINIC { get; set; }
        public String ITEMTYPE { get; set; }
        public String ITEMCODE { get; set; }
        public String ITEMSRC { get; set; }
        public String QTY { get; set; }
        public String AMOUNT { get; set; }
        public String AMOUNT_EXT { get; set; }
        public String PROVIDER { get; set; }
        public String ADDON_DESC { get; set; }

        /*
        HN              หมายเลขประจำตัวผู้รับบริการ
        DATESERV        วันที่คิดค่ารักษา บันทึก ปีในค่าเป็น ค.ศ
        SEQ             รหัสการบริการที่กำหนดโดยโปรแกรม
        CLINIC          รหัสคลินิกที่ให้บริการ
        ITEMTYPE        ชนิดของบริการที่คิดค่ารักษา ตามรหัสที่กำหนด
                            02 = ค่าอวัยวะเทียมและเครื่องช่วยผู้พิการ
                            05 = เวชภัณฑ์ที่ไม่ใช่ยา
                            06 = บริการโลหิตและส่วนประกอบของโลหิต
                            07 = ตรวจวินิจฉัยทางเทคนิคการแพทย์
                            และพยาธิวิทยา
                            08 = ตรวจวินิจฉัยทางรังสีวิทยา
                            09 = ตรวจวินิจฉัยด้วยวิธีพิเศษอื่นๆ
                            10 = อุปกรณ์และเครื่องมือทางการแพทย์
                            11 = ทำหัตถการและวิสัญญี
                            12 = ค่าบริการทางการพยาบาล
                            13 = ค่าบริการทางทันตกรรม
                            14 = ค่าบริการทางกายภาพบำบัดและเวชกรรมฟื้นฟู
                            16 = ค่าบริการอื่นๆที่ไม่เกี่ยวกับการรักษาพยาบาลโดยตรง
        ITEMCODE            รหัสรายการ ตามแฟ้ม master ที่ได้ส่ง
                                ให้กับสปสช.ผ่านทาง OPBKKClaim Client
                                หรือ รหัสของกรมบัญชีกลาง/รหัสที่สปสช. กำหนด
        ITEMSRC             ประเภทรหัส:
                                1 = รหัสหน่วยบริการ
                                2 =รหัสกรมบัญชีกลาง/รหัสที่สปสช.กำหนด
        QTY                 จำนวนรายการ
        AMOUNT              จำนวนเงิน คารักษาของบริการรายการนั้นเป็น บาท ในส่วนที่เบิกได
        AMOUNT_EXT          จำนวนเงิน คารักษาของบริการรายการนั้นเป็น บาท ในส่วนที่เบิกไม่ได
        PROVIDER            ผู้ให้บริการที่เกี่ยวข้อง ตามเลขที่ใบประกอบวิชาชีพ เวชกรรม
                                *PROVIDER รหัสผู้ให้บริการ กรณีที่ไม่มีข้อมูลให้ละเป็ นค่าว่างได้
        ADDON_DESC          รายละเอียดเพิ่มเติม
                                - กรณี Itemtype=13 (ค่าบริการทันตกรรม) * ต้องระบุ
                                หากเป็น Itemcode ที่เกี่ยวกับการ ขูด อุด ถอน ต้องระบุ ซี่ฟันด้วย คั้น
                                ด้วยเครื่องหมาย , เช่น 32,33,35
                                - แก้ปัญหา dbf ไม่รองรับชื่อฟิลด์ addon_detail

         */
    }
}
