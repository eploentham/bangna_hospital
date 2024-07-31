using AutocompleteMenuNS;
using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.C1Pdf;
using C1.Win.BarCode;
using C1.Win.C1FlexGrid;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmCertDoctor : Form
    {
        BangnaControl bc;
        System.Drawing.Font fEdit, fEditB, fEditBig, ffB;
        Color bg, fc, color;
        Label lbLoading;
        Boolean pageLoad = false;
        Patient ptt;
        C1PdfDocument _c1pdf;
        C1BarCode qrcode;
        C1FlexGrid grfHn;
        int colHnHn = 1, colHnName = 2, colHnVn = 3, colHnVsDate=4, colHnPreno=5;
        String HN = "", PRENO="", VSDATE="", DTRCODE="", CERTID="",AN="";
        public MemoryStream streamCertiDtr;
        AutocompleteMenu acmLine1, acmLine2, acmLine3, acmLine4;
        string[] AUTOLINE1 = { "ไข้หวัด", "คออักเสบ(Acute Pharyngitis)", "ไขมันในเลือดสูง", "ไข้หวัดใหญ่ สายพันธุ์บี(B)", "กล้ามเนื้อไหล่อักเสบ", "ท้องเสีย", "คออักเสบ", "ไข้หวัดใหญ่ สายพันธุ์เอ(A)", "ปวดท้อง", "หูอักเสบ", "เบาหวาน", "ไข้ และคออักเสบ", "หอบหืด และไขมันในเลือดสูง", "นิ่วทางเดินปัสสสวะ"
            , "กล้ามเนื้ออักเสบ", "วิงเวียน", "กรดไหลย้อน", "ติดเชื้อทางเดินปัสสาวะ", "กล้ามเนื้อหลังอักเสบ", "ปวดศรีษะ"
        , "กล้ามเนื้อบริเวณไหล่ขวา และสะบักหลังอักเสบเรื้อรัง", "กระเพาะปัสสาวะอักเสบ", "โรคกรดไหลย้อน", "น้ำมูก", "ติดเชื้อโควิด-19", "ริดสีดวงทวาร", "ภูมิแพ้", "กล้ามเนื้อต้นขาขวาอักเสบ", "เอ็นข้อมือขวาอักเสบ", "เอ็นข้อมือซ้ายอักเสบ", "กล้ามเนื้อบริเวณไหล่ซ้าย และสะบักหลังอักเสบเรื้อรัง", "กล้ามเนื้อบริเวณไหล่ขวา และสะบักหลังอักเสบ"
            , "กล้ามเนื้อบริเวณไหล่ซ้าย และสะบักหลังอักเสบ", "กล้ามเนื้อไหล่ขวาอักเสบ","กล้ามเนื้อต้นขาซ้ายอักเสบ","ติดตามอาการ ","ได้รับบาดเจ็บที่หัวเข่าซ้าย","ข้อสะโพกขวา","ชาที่ขา","ติดตามอาการผ่าตัด ","ดามกระดูกที่หัวไหล่ขวา","กระดูกสันหลัง","แผลที่แขนซ้าย"
            , "กล้ามเนื้อไหล่ซ้ายอักเสบ", "ความดันโลหิตสูง", "ไข้หวัด(common cold)", "ปวดศีรษะ", "จมูกหัก", "กระเพาะอักเสบ  ", "เข่าซ้ายอักเสบ", "เข่าขวาอักเสบ", "อาการหลังติดเชื้อไข้หวัดใหญ่", "โรคเก๊าต์", "ขาบวม", "เวียนศีรษะ", "ปวดหลัง", "หลอดลมอักเสบ", "ลำไส้อักเสบ", "รูขุมขนอักเสบ", "ปวดกราม"
            , "จุกแน่นท้อง","ผื่นผิวหนังอักเสบ", "ผื่นผิวหนังอักเสบ บริเวณขาขวา(สงสัยงูสวัด)", "ผื่นผิวหนังอักเสบ บริเวณขาซ้าย(สงสัยงูสวัด)", "ผื่นผิวหนังอักเสบ บริเวณแขนขวา(สงสัยงูสวัด)", "ผื่นผิวหนังอักเสบ บริเวณแขนซ้าย(สงสัยงูสวัด)", "ผื่นงูสวัด บริเวณ", "common cold", "กระเพาะอักเสบ และท้องเสีย ", "นิ่วไต"
            , "ตาซ้ายอักเสบ (สงสัยสิ่งแปลกปลอมเข้าตา)", "ตาขวาอักเสบ (สงสัยสิ่งแปลกปลอมเข้าตา)", "ต่อมทอนซิลอักเสบ(Tonsilitis)", " แผลถลอกบริเวณ", "แผลฉีกขาดบริเวณ", "ฝีบริเวณ", "ไมเกรน", "เล็บขบ นิ้วโป้งเท้าซ้าย", "เล็บขบ นิ้วโป้งเท้าขวา", "ขาชา", "ขาขวาชา", "ขาซ้ายชา", "ติดเชื้อพยาธิ ในทางเดินอาหาร"
            , "ตับอักเสบ", "เอ็นอักเสบที่มือซ้าย", "เอ็นอักเสบที่มือขวา", "เอ็นอักเสบที่เท้าซ้าย", "เอ็นอักเสบที่เท้าขวา", "เอ็นอักเสบที่ข้อเท้าซ้าย", "เอ็นอักเสบที่ข้อเท้าขวา", "เล็บขบนิ้วโป้งซ้าย", "เล็บขบนิ้วโป้งขวา", "เส้นเอ็นหัวไหล่ซ้ายได้รับบาดเจ็บ"
            , "ได้รับบาดเจ็บที่หัวไหล่ซ้าย", "เอ็นอักเสบที่เท้า", "ลำไส้ใหญ่อักเสบ", "กระดูกหัวไหล่ขวาแตกหัก", "นัดติดตามอาการ(ได้รับบาดเจ้บที่ )", "อ่อนเพลีย", "หมอนรองกระดูกสันหลัง ระคายเส้นประสาท", "หมอนรองกระดูกสันหลัง", "กล้ามเนื้อแขนขวาอักเสบ", "กล้ามเนื้อแขนซ้ายอักเสบ", "กล้ามเนื้อแขน"
            , "กล้ามเนื้อ", "ไข้หวัดเฉียบพลัน", "ความดันโลหิตสูง ไขมันในเลือดสูงและโลหิตจาง", "กล้ามเนื้อเเขนซ้ายอักเสบ", "กล้ามเนื้อเเขนขวาอักเสบ","ปวดหัว จากเครียดสะสม","ความดันโลหิตสูงและปวดเข่า","ตรวจหลังแท้งบุตร","ผลตรวจสุขภาพ ผิดปกติ","นัดติดตามอาการ และนัดอัลตราซาวน์ช่องท้อง"
            ,"ไข้หวัด (นัดติดตามอาการและนัดเจาะเลือด)","เชื้อราหน้าอกและแผ่นหลัง","ภูมิแพ้อากาศและไข้หวัดแทรก","ภูมิแพ้อากาศ","ไข้หวัดคออักเสบเรื้อรัง","เอ็นยึดหัวแม่มือซ้าย","เอ็นยึดหัวแม่มือขวา","เอ็นกล้ามเนื้อเข่าซ้ายอักเสบ","เอ็นกล้ามเนื้อเข่าขวาอักเสบ","เท้าขวาช้ำบวมอักเสบ","เท้าซ้ายช้ำบวมอักเสบ","กระเพาะลำไส้อักเสบจากอาหาร"
            ,"เนื้อเยื่อข้อมือขวาบาดเจ็บ","เนื้อเยื่อข้อมือซ้ายบาดเจ็บ","ไอเรื้อรัง","ปวดหัวเครียดและไข้หวัดแทรก","ภาวะลำไส้อักเสบ","ไข้หวัดคออักเสบกึ่งเรื้อรัง","โรคกระเพาะอักเสบเฉียบพลัน","เอ็นไหล่อักเสบเรื้อรัง","เอ็นไหล่ซ้ายอักเสบเรื้อรัง","เอ็นไหล่ขวาอักเสบเรื้อรัง"
            ,"งูสวัด","โลหิตจาง","ทอนซิลอักเสบ","เนื้องอกมดลูก","ปรึกษาเรื่องซีด","ไขมันในเลือดสูง ความดันโลหิตสูง","ภูมิแพ้จมูก","ภูมิแพ้กำเริบ","ปัสสาวะติดเชื้อ","หวัดธรรมดา","ข้อมือขวาเอ็นอักเสบ","ข้อมือซ้ายเอ็นอักเสบ","ปวดเข่าซ้ายจากเอ็นไขว้หน้าเข่าซ้ายบาดเจ็บ","ปวดเข่าขวาจากเอ็นไขว้หน้าเข่าขวาบาดเจ็บ"
            ,"common cold","ปรึกษาเรื่อง","มาปรึกษาเรื่องความดันโลหิต","กระดูกสันหลังคด","มาปรึกษาเรื่อง","ฝากครรภ์","มาฟังผลเลือด","ปวดกล้ามเนื้อ","ปวดกล้ามเนื้อ ต้นคอ","ปวดหลังและไหล่ซ้าย","ปวดหลังและไหล่ขวา","เชื้อราที่ก้น","อาหารเป็นพิษ","ตกขาว ปัสสาวะติดเชื้อ","ตกขาว","ปัสสาวะติดเชื้อ"
            ,"ตากุ้งยิง","หวัดทั่วไป","ปวดมือขวา","ปวดมือซ้าย","มาตรวจเรื่องผลตรวจรังสีทรวงอก ผิดปกติ","ไข้ และประวัติสัมผัสคนไข้ ไข้หวัดใหญ่","ไข้หวัดใหญ่","ปวดศรีษะจากกล้ามเนื้อต้นคออักเสบ","ปวดศีรษะจากกล้ามเนื้ออักเสบ","กล้ามเนื้อเคล็ดที่หลัง คอ และไหล่","ผื่นคันที่รักแร้","กล้ามเนื้อเคล็ดที่ศอกซ้าย"
            ,"กล้ามเนื้ออักเสบบริเวณคอ","กล้ามเนื้ออักเสบบริเวณ","เสียงดังในหูซ้าย","ปวดข้อเท้า","เลือดกำเดาไหล","เยื่อบุจมูกอักเสบเรื้อรัง","ผื่นคัน","อวัยวะเพศอักเสบ","ปวดศีรษะ ไมเกรน","ปวดเข่า","ปวดเมื่อยกล้ามเนื้อแขน","ตาปลา","ปัสสาวะบ่อย","ก้อนหูขวา","ก้อนหูซ้าย","กระดูกงอกในหูขวา","กระดูกงอกในหูซ้าย","ผื่นคันผิวหนัง"
            ,"ข้อเท้าอักเสบ","ข้อเท้าขวาอักเสบ","ข้อเท้าซ้ายอักเสบ","ปวดเข่าขวา","ปวดเข่าซ้าย","ปวดเก๊าท์","ไซนัสอักเสบ","แผลติดเชื้อบริเวณขาขวา","แผลติดเชื้อบริเวณขาซ้าย","ก้อนที่เต้านม","เวียนศีรษะ บ้านหมุน","หอบหืด","ก้อนบริเวณนิ้วกลางเท้าซ้าย","ก้อนบริเวณนิ้วกลางเท้าขวา","ข้อเท้าพลิกอักเสบ","ทางเดินปัสสาวะอักเสบ"
            ,"ปวดไหล่ขวา (กระดูกไหปลาร้าด้านขวาหัก)","ปวดไหล่ซ้าย (กระดูกไหปลาร้าด้านซ้ายหัก)","ปรึกษาผลตรวจสุขภาพ","ก้อนไขมันที่หน้าท้อง","ระดับน้ำตาลในเลือดสูง","สุกใส","ปรึกษาอาการ","ปวดข้อเท้าซ้าย","ปวดข้อเท้าซขวา","ปวดกล้ามเนื้อ แขน","ตาอักเสบ","ถุงน้ำรังไข่","เยื่อบุตาอักเสบ","ก้อนเนื้อ","ก้อนเนื้อนิ้ว"
            ,"แผลปากช่องคลอด","แผล","เศษฝุ่นเข้าตาข้างขวา","เศษฝุ่นเข้าตาข้างซ้าย","กระดูกข้อเท้าหัก","กระดูกข้อเท้าซ้ายหัก","กระดูกข้อเท้าขวาหัก","ข้อเข่าอักเสบ","นัดติดตามอาการ","ติดตามอาการผ่าตัด","เป็นเก๊าท์","ฝากครรภ์   สัปดาห์","ปวดขา","แท้งบุตร","ติดตามอาการ","อาการเหน็บชา","ตาอักเสบข้างขวา"
        ,"ตาอักเสบข้างซ้าย","โรคเบาหวาน","โรคไตเสื่อม","เส้นเอ็นข้อเท้าซ้ายอักเสบ","เส้นเอ็นข้อเท้าขวาอักเสบ","กระดูกสะบักขวาร้าว","กระดูกสะบักซ้ายร้าว รักษาต่อเนื่อง","พังผืดฝ่าเท้าขวาอักเสบ","ริดสีดวงอักเสบ","ปวดเมื่อยตามตัว","เป็นไข้","ท่อปัสสาวะอักเสบ","ตกขาวร่วมกับกระเพาะปัสสาวะอักเสบ","เก๊าท์","เก๊าท์ และ ไขมันในเลือดสูง "
        ,"พังผืดฝ่าเท้าอักเสบ","กล้ามเนื้อเคล็ด","หัวเข่าขวาอักเสบ","หัวเข่าซ้ายอักเสบ","ตรวจสุขภาพประกันสังคม","ซิฟิลิส","โรคกระเพาะอาหาร","ผื่นลมพิษ","ลมพิษ","ระคายเคืองตา(สงสัยตาแห้ง)","ผิวหนังอักเสบ","ภาวะลองโควิด","ไข้ปวดตามตัว","กระดูกข้อศอกซ้ายหัก","กระดูกข้อศอกขวาหัก","ก้อนเนื้อบริเวณหลัง","แผลริมอ่อน"
        ,"ตรวจติดตามเลือดออกผิดปกติ","ไข้และปวดศรีษะ","กล้ามเนื้ออักเสบบริเวณขาสองข้าง","เอ็นฝ่ามืออักเสบ","มือขวาชา","มือซ้ายชา","ประจำเดือนผิดปกติ","เหงือกอัเสบ","กล้ามเนื้อแขนขาอักเสบ","กระเพาะอาหารอักเสบ","เหงือกอักเสบ","โควิด-19","เอ็นยึดนิ้วหัวแม่มือขวา","เอ็นยึดนิ้วหัวแม่มือซ้าย","กล้ามเนื้อต้นคออักเสบ","ติ่งเนื้อหัวเหน่า"
        ,"ความดันโลหิตสูงและเก๊าท์","ไข้หนาวสั่น","โลหิตจางและเวียนศรีษะ","เวียนศรีษะ","กระดูกไหปลาร้าขวาหัก","กระดูกไหปลาร้าซ้ายหัก","ปรึกษาอาการโรคหยุดหายใจขณะหลับ","ปวดบริเวณ","ปวดบริเวณกรามด้านขวา","แผลบริเวณทวารหนัก","เวียนศีรษะและระดับไขมันในเลือดสูง","ขี้หูอุดตัน","กล้ามเนื้อขาอักเสบ","เชื้อราที่ท้อง แขน"
        ,"เชื้อราที่ตัว","รังแค่ที่หน้า ศรีษะ","แผลเป็นที่หน้าอก","แผลที่ทวารหนักอักเสบ","ผมร่วงเป็นวง","ติดเชื้อที่หลังคอ","สิวที่ตัว","เชื้อราที่ตัวแขน","ด่างขาวที่อก เท้า","ชั้นไขมันอักเสบที่ขา","อัณฑะอักเสบ","ลมพิษเรื้อรัง","ก้อนที่หัวใจ","กระดูกสันหลัง ทับเส้นประสาท","อุบัติเหตุ เส้นเอ็นหัวไหล่ซ้ายได้รับบาดเจ็บ","อุบัติเหตุ เส้นเอ็นหัวไหล่ขวาได้รับบาดเจ็บ"
        ,"กระดูกสันหลังเสื่อม","สิ้นสุดการรักษา ได้รับบาดเจ็บที่","ปวดท้องประจำเดือน","หูดปากช่องคลอด","เลือดออกผิดปกติ","ปรึกษาเรื่องมีบุตรยาก","ติดตามอาการ เรื่องโรคประจำตัว","ติดเชื้อระบบทางเดินอาหาร","ปวดข้อมือ","เปลือกตาอักเสบ","ภูมิแพ้ทางเดินหายใจ","นัดฉีดยา","โรคหิด scabies","ก้อนที่","ท้องผูก","สะเก็ดเงิน"
        ,"มาเจาะเลือดตามนัด","ไอ มีน้ำมูก","เส้นเอ็นมืออักเสบ","จากกล้ามเนื้ออักเสบ","ปวดตาข้างซ้าย ถูกทำร้ายร่างกาย","หวัด เจ็บคอ","เจ็บคอ","ริมฝีปากบนบวม","เอ็นอักเสบที่นิ้วโป้งมือซ้าย","ไอเสมหะ","ตรวจหัวใจ","เส้นเลือดหัวใจตีบ","หัวใจเต้นผิดจังหวะ","มะเร็งเต้านม","มะเร็งปอด","มะเร็งตับ","มะเร็งลำไส้"
        ,"มะเร็งหลอดอาหาร","มะเร็งกระเพาะอาหาร","มะเร็งโพรงจมูก","มารับการให้ยาเคมีบำบัดจริง","ลิ้นหัวใจรั่ว","ผนังกั้นหัวใจรั่ว","ไขมันในเลือด","ติดตามอาการ เรื่องระบบท่อทางเดืนน้ำดีอุดตัน","ติดตามอาการ ก้อนเต้านม ทั้ง 2 ข้าง","นิ่วในถุงน้ำดี","กระดูกฝ่าเท้าซ้ายหัก","ถุงน้ำรังไข่ซ้าย","ก้อนเต้านม","ไวรัสตับอักเสบบี","ปวดท้องน้อย"
        ,"โรคมือเท้าปาก","ไทรอยด์เป็นพิษ","เยื่อบุตาถลอก จากสารเคมีกระเด็นเข้าตา","โรคตากุ้งยิง","โรคกระจกตาอักเสบ","โรคต้อลมและตาแห้ง","โรคม่านตาอักเสบและความดันตาสูง","เนื้องอกที่เยื่อบุตาขวา","ยิงเลเซอร์ตา","ตาแห้ง","ติดเชื้อไวรัสทางเดินหายใจส่วนบน","สงสัยการติดเชื้อไวรัส ยังระบุสาเหตุไม่ชัดเจน","รับวัคซีน คอตีบไอกรนบาดทะยักโปลิโอเข็ม5+ไข้หวัดใหญ่"
        ,"คอหอยอักเสบ","นิ่วในกระเพาะปัสสาวะ","ตรวจหัวใจ","ตรวจหัวใจ  ติด Holter 24 hr.","แผลร้อนใน","หูน้ำหนวก2ข้าง","โรคข้ออักเสบรูมาตอยด์","พาหะไวรัสตับอักเสบบี","นอนไม่หลับ","มาตรวจเรื่องผลตรวจรังสีทรวงอกผิดปกติ","ไขมันเกาะตับ","ชามือสองข้าง","ตากุ้งยิง เปลือกตาซ้าย","น้ำในหูชั้นกลางข้างขวา"
        ,"ลิ่มเลือดที่ปอดอุดตัน","ถ่ายอุจจาระเหลว","ม่านตาอักเสบตาขวา","ผึ้งต่อยเท้าขวา","ข้อเท้าซ้น","ต่อมลูกหมากโต","หูขวาอักเสบ","ไข้หวัดทั่วไป","โรค SLE","โรคข้อกระดูกสันหลังติดยึด","แผลถลอกเข่าซ้าย","ข้ออักเสบจากเชื้อไวรัส","แผลฉีกขาดที่","ล้างเเผลต่อเนื่อง","การติดเชื้อทางเดินหายใจส่วนบน","เส้นเลือดหัวใจตีบ"
        ,"ก้อนถุงน้ำข้อมือซ้าย","ก้อนถุงน้ำข้อมือขวา","สงสัยตับอักเสบจากสุรา","เส้นเลือดหัวใจตีบ (TVD)","เหงือกอักเสบติดเชื้อ จากการมีฟันคุด","ฟันผุทะลุโพรงประสาทฟัน ต้องถอนฟัน","พบฟันสึกที่ฟันกรามใหญ่ล่าง","พบฟันสึกที่ฟัน","28 Irreversible pulpitis with chronic apical periodonitis"
        ,"ฟันแตก","ริดสีดวงจมูก","ปรึกษาเรื่องติดเชื้อหูดที่ปากมดลูก","ปรึกษาเรื่องติดเชื้อ","มีบุตรยาก","ถุงน้ำรังไข่","เส้นเลือดสมองตีบ","การอักเสบในกระพุังแก้ม","มาตรวจตามนัด ทอมซิลอักเสบ","มาตรวจตามนัด","มาตรวจตามนัดเรื่องโลหิตจาง","ติดตามอาการ หลังผ่าตัดไส้ติ่ง","โรคเยื่อบุตาอักเสบ","ภาวะตาแห้ง","โรคต้อลมอักเสบ"
        ,"เศษเหล็กติดที่กระจกตาข้างซ้าย","เศษเหล็กติดที่กระจกตาข้างขวา","18 ฟันคุดไม่มีคู่สบ","38 เหงือกอักเสบรอบฟันคุด,28 ฟันคุดไม่มีคู่สบ","22,32 โรคปรืทันต์อักเสบ","38 เหงือกอักเสบติดเชื้อรอบฟันคุด","วัณโรคปอด ได้รับการรักษาเกิน 2 สัปดาห์แล้ว สามารถทำงานได้ตามปกติ ","วัณโรคปอด","โรคลมชัก"
        ,"ช่องหน้าม่านตาแคบ ยิงเลเซอร์ตาตามนัด","ช่องหน้าม่านตาแคบ","ไข้เลือดออก","16 ติดตามอาการหลังถอนฟัน","ไทรอยด์ต่ำ(หลังกลืนแร่)","มาตรวจตามนัดเรื่องลำไส้อักเสบ","ฉีควัคซีนพิษสุนัขบ้า เข็มที่1","ฉีควัคซีนพิษสุนัขบ้า เข็มที่2","ฉีควัคซีนพิษสุนัขบ้า","34 โรคปริทันต์อักเสบรุนแรง, เหงือกอักเสบทั้งปาก"
        ,"ไอเรื้อรัง","ตัดไหม","อาการใจสั่น","ท้องเสียเรื้อรัง","ฉีดสีเส้นเลือดหัวใจ","#48 Pericoronitis","ข้ออักเสบเรื้อรัง","เกร็ดเลือดต่ำ","ลิ่มเลือดอุดตันที่เส้นเลือดตา","เเพ้ท้อง (ตั้งครรภ์)","สุนัขกัด","บาดเเผลฉีกขาด","ฉีดวัคซีนไข้หวัดใหญ่","ฉีดวัคซีน","ตั้งครรภ์","แมวกัด","ฝีข้างทวาร","37 โรคปริทันต์อักเสบ"
        ,"เหงือกอักเสบ, 27 Pa ฟันผุ","วุ้นในตาเสื่อม","มะเร็งท่อทางเดินน้ำดี","ตาแดง เยื่อบุตาอักเสบทั้งสองข้าง","ตาแดง","เศษสิ่งแปลกปลอมในกระจกตาซ้าย","ตับอ่อนอักเสบ","ฟันคุดทำความสะอาดไม่ถึง","ต้อกระจกระยะเริ่มแรก และมีสายตายาวตามอายุ","ปวดท้องเรื้อรัง","มะเร็งทางเดินน้ำดี","มะเร็งตับอ่อน","มะเร็งลำไส้ตรง"
        ,"seminoma","เนื้องอกจิสต์ (GIST)","มะเร็งลิ้น","กระจกตาดำถลอก ตาซ้าย","กระจกตาดำถลอก ตาขวา","ต้อกระจก","สิ่งแปลกปลอมที่กระจกตาดำซ้าย","สิ่งแปลกปลอมที่กระจกตาดำขวา","เยื่อบุตาอักเสบ ไวรัสตาแดง","สารเคมีกระเด็นเข้าตา กระจกตาดำถลอกตาขวา","มาตรวจเบาหวานจอตา  (ปกติ)","เปลือกตาอักเสบ กุ้งยิง"
        ,"จอประสาทตาเสื่อม","เบาหวานขึ้นจอตา","กระจกตาอักเสบแห้ง","จอประสาทตาบวม ตาซ้าย","จอประสาทตาบวม ตาขวา","เบาหวานขึ้นจอตา   มารับการยิงเลเซอร์จอตา","โรคหนังแข็ง","โรคความดันโลหิตสูง","โรคปอดอักเสบจากแพ้ภูมิตนเอง","โรคกระดูกสันหลังติดยึดชนิดข้อระยางค์เด่น","โรคโลหิตจาง","ข้ออักเสบรูมาตอยด์กำเริบ"
        ,"โรคเส้นเลือดอักเสบ IgA","โรคแพ้ภูมิตนเอง","อุบัติเหตุจราจร ","กล้ามเนื้อเข่าอักเสบ","หกล้มกระดูก","กล้ามเนื้อต้นคออักเสบ","รักษาต่อเนื่อง","แผลเย็บ","เศษเหล็กเข้าตา","แสงควันเชื่อมเข้าตา","ล้างแผลต่อเนื่อง","อุบัติเหตุ"};

        string[] AUTOLINE2 = { "นัดติดตามอาการ และนัดเจาะเลือด", "นัดติดตามอาการ", "นัดตรวจอัลตราซาวน์ช่องท้อง", "นัดพบแพทย์เฉพาะทาง", "นัดติดตามอาการ และนัดฉีดยาฆ่าเชื้อ", "นัดติดตามอาการ และนัดเจาะเลือดเช็คค่าตับและตับอ่อน", "หากไม่ดีขึ้น แนะนำให้มาโรงพยาบาลอีกครั้ง", "แนะนำให้มาโรงพยาบาลหากไม่ดีขึ้น"
                , "หากรับประทานยาไม่ดีขึ้นหรือมีอาการผิดปกติ แนะนำพบแพทย์", "ผู้ป่วยตรวจพบเชื้อโควิด ด้วยวิธี ATK","ล้างแผลต่อเนื่องห้ามแผลโดนน้ำ","นัดฉีดยาฆ่าเชื้อ","ล้างแผลต่อเนื่อง ห้ามแผลโดนน้ำ","ไขมัน","ความดัน","ผลอ่านเอ็กซ์เรย์ปอด พบลักษณะ หลอดลมอักเสบ","ปวดหัวมาก จากความเครียด","ท้องเสีย"
                ,"ผลอ่านเอ๊กซ์เรย์พบลักษณะรอยบริเวณปอดด้านล่างเล็กน้อย","นัดฟังผลเอ๊กซ์เรย์ปอดและนัดส่งเสมหะ","นัดฟังผลเอ๊กซ์เรย์","เส้นเอ็นอักเสบ","มาตรวจจริงที่โรงพยาบาล","นัดติดตามอาการและนัดเจาะเลือด","ต้องได้รับการตรวจเพิ่มเติม","ต้องตรวจเพิ่มเติมปอดมีรอยโรค","นัดส่งเสมหะ","แนะนำพบทันตกรรม","ถูกทำร้ายร่างกาย"
        ,"เหงือกอักเสบทั้งปาก","ได้รับการถอนฟัน 2 ซี่","พบลิ่มเลือดขนาดใหญ่ ทำการล้างแผลและจ่ายยาแก้ปวดเพิ่ม","ได้รับการถอนฟันและจ่ายยา","28 ฟันคุดไม่มีคู่สบ","ส่งปรึกษาแพทย์เฉพาะทางทางเดินอาหาร","ได้รับการถอนฟันแบบธรรมดา และจ่ายยา","แนะนำ ถอนฟัน , ขูดหินปูน , อุดฟัน","มารับการตรวจ , x-ray 1 film , จ่ายยา "
        ,"ตรวจ , จ่ายยา "};
        string[] AUTOLINE3 = { "หากมีอาการผิดปกติแนะนำพบแพทย์", "นัดติดตามอาการ", "นัดพบแพทย์เฉพาะทาง", "ไม่มีอาการ ไอเรื้อรัง เบื่ออาหาร ไข้ต่ำๆตอนกลางคืน น้ำหนักตัวลดผิดปกติ", "ผล เอ็กซเรย์ปอด มีลักษณะรอยโรคเดิม", "หากรับประทานยาไม่ดีขึ้นหรือมีอาการผิดปกติแนะนำพบแพทย์", "ขูดหินปูนทั้งปาก"
        ,"ได้รับการถอนฟัน 2 ซี่","แนะนำถอนฟัน"};
        string[] methods = { "Equals()", "GetHashCode()", "GetType()", "ToString()" };
        string[] snippets = { "if(^)\n{\n}", "if(^)\n{\n}\nelse\n{\n}", "for(^;;)\n{\n}", "while(^)\n{\n}", "do${\n^}while();", "switch(^)\n{\n\tcase : break;\n}" };
        string[] declarationSnippets = {
               "public class ^\n{\n}", "private class ^\n{\n}", "internal class ^\n{\n}",
               "public struct ^\n{\n}", "private struct ^\n{\n}", "internal struct ^\n{\n}",
               "public void ^()\n{\n}", "private void ^()\n{\n}", "internal void ^()\n{\n}", "protected void ^()\n{\n}",
               "public ^{ get; set; }", "private ^{ get; set; }", "internal ^{ get; set; }", "protected ^{ get; set; }"
               };

        public FrmCertDoctor(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        public FrmCertDoctor(BangnaControl bc, String hn, String vsdate, String preno)
        {
            this.bc = bc;
            this.HN = hn;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            InitializeComponent();
            initConfig();
        }
        public FrmCertDoctor(BangnaControl bc, String dtrCode, String hn, String vsdate, String preno)
        {
            this.bc = bc;
            this.HN = hn;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            this.DTRCODE = dtrCode;
            InitializeComponent();
            initConfig();
        }
        public FrmCertDoctor(BangnaControl bc, String dtrCode, String hn, String vsdate, String preno, String certid)
        {
            this.bc = bc;
            this.HN = hn;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            this.DTRCODE = dtrCode;
            this.CERTID = certid;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            fEdit = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEditBig = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 7, FontStyle.Regular);
            rb1.Text = bc.iniC.grdViewFontName;
            rb2.Text = "bc.iniC.grdViewFontName "+VSDATE+" "+PRENO;
            rb3.Text = bc.iniC.hostFTP + " " + bc.iniC.folderFTP;
            qrcode = new C1BarCode();
            qrcode.ForeColor = System.Drawing.Color.Black;
            qrcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            if (bc.iniC.statusStation.Equals("IPD")) chkIPD.Checked = true;
            else chkOPD.Checked = true;
            if (chkIPD.Checked) bc.bcDB.pttDB.setCboDeptIPDWdNo(cboDept, bc.iniC.station);
            else bc.bcDB.pttDB.setCboDeptOPD(cboDept, bc.iniC.station);
            
            ptt = new Patient();

            btnPrintCert.Click += BtnPrintCert_Click;
            btnPrintCertE.Click += BtnPrintCertE_Click;
            cboDept.SelectedIndexChanged += CboDept_SelectedIndexChanged;
            txtDtrCode.KeyUp += TxtDtrCode_KeyUp;
            txtLine1.KeyUp += TxtLine1_KeyUp;
            txtLine2.KeyUp += TxtLine2_KeyUp;
            txtLine3.KeyUp += TxtLine3_KeyUp;
            txtChk3NumDays.KeyPress += TxtChk3NumDays_KeyPress;
            txtChk3NumDays.KeyUp += TxtChk3NumDays_KeyUp;
            
            chk2.Click += Chk2_Click;
            chk3.Click += Chk3_Click;
            chk4.Click += Chk4_Click;
            chk1.Click += Chk1_Click;
            txtChk3DateStart.DropDownClosed += TxtChk3DateStart_DropDownClosed;
            chkOPD.Click += ChkOPD_Click;
            chkIPD.Click += ChkIPD_Click;

            initGrfDtrCert();
            initAutoComTxtLine1();
            BuildAutocompleteMenuLine1();
            initAutoComTxtLine2();
            BuildAutocompleteMenuLine2();
            initAutoComTxtLine3();
            BuildAutocompleteMenuLine3();
            setControlForm();
            setGrfHn(bc.iniC.station);
            lb2nfleaf.Visible = CERTID.Equals("2NFLEAF") ?true:false;
            
            pageLoad = false;
        }

        private void ChkIPD_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pageLoad = true;
            cboDept.Clear();
            cboDept.Items.Clear();
            bc.bcDB.pttDB.setCboDeptIPDWdNo(cboDept, bc.iniC.station);
            pageLoad = false;
        }

        private void ChkOPD_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pageLoad = true;
            cboDept.Clear();
            cboDept.Items.Clear();
            bc.bcDB.pttDB.setCboDeptOPD(cboDept, bc.iniC.station);
            pageLoad = false;
        }

        private void TxtChk3NumDays_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            setTxtChk3DateEnd(txtChk3NumDays.Text.Trim());
        }

        private void TxtChk3DateStart_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            setTxtChk3DateEnd(txtChk3NumDays.Text.Trim());
        }

        private void BtnPrintCertE_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (lbDtrName.Text.Length > 0)
            {
                printCertDoctoriTextSharpEnglish();
            }
            else
            {
                lb1.Text = "ไม่พบรหัสแพทย์";
            }
        }
        private void TxtChk3NumDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            //setTxtChk3DateEnd(txtChk3NumDays.Text.Trim());
        }
        private void setTxtChk3DateEnd(String numsday)
        {
            if (numsday.Equals(""))
            {
                txtChk3DateEnd.Value = null;
                return;
            }
            int numday = 0;
            if (!int.TryParse(numsday, out numday))
            {
                txtChk3DateEnd.Value = null;
                return;
            }
            numday -= 1;        // แก้ให้ ตามHRบริษัทต้องการ
            DateTime date = new DateTime();
            //DateTime.TryParse(txtVsDate.Value.ToString(), out date);
            DateTime.TryParse(txtChk3DateStart.Value.ToString(), out date);
            date = date.AddDays(numday);
            if (date.Year < 2000)
            {
                date = date.AddYears(543);
            }
            //txtChk3DateStart.Value = txtVsDate.Text;
            if (txtChk3DateEnd.CultureInfo.Name.IndexOf("th") >= 0)
            {
                txtChk3DateEnd.Value = date;
            }
            else
            {
                txtChk3DateEnd.Value = date;
            }
            //txtChk3DateEnd.Value = date;
        }
        private void setControlForm()
        {
            if (HN.Length > 0)
            {
                setControl(HN, VSDATE, PRENO);
                panel4.Hide();
                this.Width = panel2.Width + 30;
                panel1.Width = panel2.Width + 20;
            }
        }
        private void initAutoComTxtLine3()
        {
            acmLine3 = new AutocompleteMenuNS.AutocompleteMenu();
            acmLine3.AllowsTabKey = true;
            acmLine3.Font = new System.Drawing.Font(bc.iniC.grdViewFontName, 12F);
            acmLine3.Items = new string[0];
            acmLine3.SearchPattern = "[\\w\\.:=!<>]";
            acmLine3.TargetControlWrapper = null;

            acmLine3.SetAutocompleteMenu(txtLine3, acmLine3);
        }
        private void initAutoComTxtLine2()
        {
            acmLine2 = new AutocompleteMenuNS.AutocompleteMenu();
            acmLine2.AllowsTabKey = true;
            acmLine2.Font = new System.Drawing.Font(bc.iniC.grdViewFontName, 12F);
            acmLine2.Items = new string[0];
            acmLine2.SearchPattern = "[\\w\\.:=!<>]";
            acmLine2.TargetControlWrapper = null;

            acmLine2.SetAutocompleteMenu(txtLine2, acmLine2);
        }
        private void initAutoComTxtLine1()
        {
            acmLine1 = new AutocompleteMenuNS.AutocompleteMenu();
            acmLine1.AllowsTabKey = true;
            acmLine1.Font = new System.Drawing.Font(bc.iniC.grdViewFontName, 12F);
            acmLine1.Items = new string[0];
            acmLine1.SearchPattern = "[\\w\\.:=!<>]";
            acmLine1.TargetControlWrapper = null;

            acmLine1.SetAutocompleteMenu(txtLine1, acmLine1);
        }
        private void BuildAutocompleteMenuLine2()
        {
            var items = new List<AutocompleteItem>();
            if (bc.postoperation != null)
            {
                foreach (var item in bc.postoperation)
                    items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            }            
            foreach (var item in AUTOLINE2)
                items.Add(new AutocompleteItem(item));
            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            //set as autocomplete source
            acmLine2.SetAutocompleteItems(items);
        }
        private void BuildAutocompleteMenuLine3()
        {
            var items = new List<AutocompleteItem>();
            if (bc.postoperation != null)
            {
                foreach (var item in bc.postoperation)
                    items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            }
            foreach (var item in AUTOLINE3)
                items.Add(new AutocompleteItem(item));
            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            //set as autocomplete source
            acmLine3.SetAutocompleteItems(items);
        }
        private void BuildAutocompleteMenuLine1()
        {
            var items = new List<AutocompleteItem>();
            if (bc.postoperation != null)
            {
                foreach (var item in bc.postoperation)
                    items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            }

            foreach (var item in declarationSnippets)
                items.Add(new DeclarationSnippet(item) { ImageIndex = 0 });
            foreach (var item in AUTOLINE2)
                items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2 });
            foreach (var item in AUTOLINE1)
                items.Add(new AutocompleteItem(item));

            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            //set as autocomplete source
            acmLine1.SetAutocompleteItems(items);
        }
        private void TxtLine3_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtLine4.Focus();
            }
        }

        private void TxtLine2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtLine3.Focus();
            }
        }

        private void TxtLine1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtLine2.Focus();
            }
        }

        private void Chk1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlDateClear();
        }

        private void Chk4_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlDateClear();
            txtChk4Date.Value = txtVsDate.Value;
            txtChk4Time.Text = txtVsTime.Text;
        }

        private void Chk3_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlDateClear();
            txtChk3DateStart.Value = txtVsDate.Value;
            setTxtChk3DateEnd(txtChk3NumDays.Text);
        }

        private void Chk2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlDateClear();
            txtChk2DateStart.Value = txtVsDate.Value;
            if (chkIPD.Checked)
            {
                txtChk2DateEnd.Value = DateTime.Now;
            }
            else
            {
                txtChk2DateEnd.Value = txtVsDate.Value;
            }
        }
        private void setControlDateClear()
        {
            //txtChk2DateStart.Clear();
            //txtChk2DateEnd.Clear();
            //txtChk3DateStart.Clear();
            //txtChk3DateEnd.Clear();
            //txtChk4Date.Clear();
        }
        private void TxtDtrCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                lbDtrName.Text = bc.selectDoctorName(txtDtrCode.Text.Trim());
            }
        }

        private void CboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            String deptid = "";
            deptid = ((ComboBoxItem)cboDept.SelectedItem).Value;
            setGrfHn(deptid);
        }
        private void setGrfHn(String wardid)
        {
            DataTable dt = new DataTable();
            if (chkIPD.Checked)
            {
                dt = bc.bcDB.pttDB.selectPatientinWardIPD(wardid);
            }
            else
            {
                DateTime dtstart1 = DateTime.Now;
                String deptid = bc.bcDB.pttDB.selectDeptIdOPDBySecId(wardid);
                if (dtstart1.Year > 2500)
                {

                }
                dt = bc.bcDB.vsDB.selectPttHiinDept1(deptid, wardid, dtstart1.Year + "-"+dtstart1.ToString("MM-dd"), dtstart1.Year + "-" + dtstart1.ToString("MM-dd"));
            }

            grfHn.Rows.Count = 1;
            grfHn.Rows.Count = dt.Rows.Count + 1;
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                grfHn[i, colHnHn] = row1["MNC_HN_NO"].ToString();
                grfHn[i, colHnName] = row1["prefix"].ToString() + " " + row1["MNC_FNAME_T"].ToString() + " " + row1["MNC_LNAME_T"].ToString();
                grfHn[i, colHnPreno] = row1["MNC_PRE_NO"].ToString();
                if (chkIPD.Checked)
                {
                    grfHn[i, colHnVsDate] = row1["MNC_AD_DATE"].ToString();
                    grfHn[i, colHnVn] = row1["an_no"].ToString();
                }
                else
                {
                    grfHn[i, colHnVsDate] = row1["MNC_DATE"].ToString();
                    grfHn[i, colHnVn] = "";
                }
                grfHn[i, 0] = i;
                if (i % 2 == 0)
                {
                    grfHn.Rows[i].StyleDisplay.BackColor = Color.FromArgb(143, 200, 127);
                }
                else
                {
                    grfHn.Rows[i].StyleDisplay.BackColor = Color.Cornsilk;
                }
            }
        }
        private void BtnPrintCert_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (lbDtrName.Text.Length > 0)
            {
                printCertDoctoriTextSharpThai();
            }
            else
            {
                lb1.Text = "ไม่พบรหัสแพทย์";
            }
        }
        private String insertCertDoctor()
        {
            String certid = "";
            MedicalCertificate mcerti = new MedicalCertificate();
            mcerti.active = "1";
            mcerti.an = chkIPD.Checked ? AN : "";
            mcerti.certi_id = "";
            mcerti.certi_code = "";
            mcerti.dtr_code = txtDtrCode.Text.Trim();
            mcerti.dtr_name_t = lbDtrName.Text;
            mcerti.status_ipd = chkOPD.Checked ? "O": "I";
            mcerti.visit_date = VSDATE;
            mcerti.visit_time = txtVsTime.Text;
            mcerti.remark = "";
            mcerti.line1 = txtLine1.Text;
            mcerti.line2 = txtLine2.Text;
            mcerti.line3 = txtLine3.Text;
            mcerti.line4 = txtLine4.Text;
            mcerti.hn = txtHn.Text;
            mcerti.pre_no = PRENO;
            mcerti.ptt_name_e = txtNameE.Text;
            mcerti.ptt_name_t = txtNameT.Text;
            mcerti.doc_scan_id = "";
            mcerti.status_2nd_leaf = (CERTID.Equals("2NFLEAF")) ? "2" : "1";
            mcerti.counter_name = bc.iniC.station;
            bc.bcDB.mcertiDB.insertMedicalCertificate(mcerti, "");
            if (CERTID.Equals("2NFLEAF"))
            {
                if (chkOPD.Checked)
                {
                    certid = bc.bcDB.mcertiDB.selectCertIDByHn2ndLeaf(txtHn.Text, PRENO, VSDATE);
                }
                else
                {
                    certid = bc.bcDB.mcertiDB.selectCertIDByHn2ndLeafAN(AN);
                }
            }
            else
            {
                if (chkOPD.Checked)
                {
                    certid = bc.bcDB.mcertiDB.selectCertIDByHn(txtHn.Text, PRENO, VSDATE);
                }
                else
                {
                    certid = bc.bcDB.mcertiDB.selectCertIDByAn(AN);
                }
            }
            return certid;
        }
        private void printCertDoctoriTextSharpThai()
        {
            String certid = "";
            certid = insertCertDoctor();
            if (certid.Length > 3)
            {
                //certid = certid.Replace("555", "");
                certid = certid.Substring(3, 7);
            }

            String patheName = Environment.CurrentDirectory + "\\cert_med\\";
            if (!Directory.Exists(patheName))
            {
                Directory.CreateDirectory(patheName);
            }

            System.Drawing.Font fontMS12 = new System.Drawing.Font("Microsoft Sans Serif", 12);
            BaseFont bfR, bfR1, bfRB;
            BaseColor clrBlack = new iTextSharp.text.BaseColor(0, 0, 0);
            string myFont = Environment.CurrentDirectory + "\\THSarabunNew.ttf";
            string myFontB = Environment.CurrentDirectory + "\\THSarabunNew Bold.ttf";
            String filename = patheName + txtHn.Text.Trim() + "_" + VSDATE + "_" + PRENO + ".pdf";
            filename = (chkOPD.Checked) ? patheName + txtHn.Text.Trim() + "_" + VSDATE + "_" + PRENO + ".pdf" : patheName + txtHn.Text.Trim() + "_"+AN + ".pdf";

            bfR = BaseFont.CreateFont(myFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            bfR1 = BaseFont.CreateFont(myFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            bfRB = BaseFont.CreateFont(myFontB, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfR, 12, iTextSharp.text.Font.NORMAL, clrBlack);

            var logo = iTextSharp.text.Image.GetInstance(Environment.CurrentDirectory + "\\LOGO-BW-tran.jpg");
            logo.SetAbsolutePosition(20, PageSize.A4.Height - 60);
            logo.ScaleAbsoluteHeight(50);
            logo.ScaleAbsoluteWidth(50);

            FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 36, 36, 36, 36);
            FileStream output = null;
            PdfWriter writer = null;
            try
            {
                String vstime = "", vsDateTH="", docscanid = "";
                docscanid = bc.bcDB.mcertiDB.selectDocScanIDByHn(txtHn.Text.Trim(), PRENO, VSDATE);
                DateTime vsdat1 = new DateTime();
                vstime = "0000" + ptt.visitTime;
                vstime = vstime.Substring(vstime.Length - 4);
                vstime = vstime.Substring(0, 2) + ":" + vstime.Substring(vstime.Length - 2, 2);
                DateTime.TryParse(ptt.visitDate, out vsdat1);
                if (vsdat1.Year < 2000)
                {
                    vsdat1 = vsdat1.AddYears(543);
                }
                vsDateTH = vsdat1.ToString("dd-MM-")+(vsdat1.Year+543).ToString();
                qrcode.CodeType = C1.BarCode.CodeType.QRCode;
                qrcode.Text = txtHn.Text.Trim()+" "+txtNameT.Text.Trim() + " " + vsDateTH+" "+ certid;
                System.Drawing.Image imgqrcode = qrcode.Image;
                var imgqrcode1 = iTextSharp.text.Image.GetInstance(imgqrcode,BaseColor.WHITE);

                output = new FileStream(filename, FileMode.Create);
                writer = PdfWriter.GetInstance(doc, output);
                doc.Open();
                doc.Add(logo);
                int linenumber = 820, colCenter = 200, fontSize0 = 8, fontSize1 = 16, fontSize2 = 18, fontSize20 = 20, fontSize4 = 22, fontSize26 = 26;
                PdfContentByte canvas = writer.DirectContent;
                linenumber = bc.padYCertMed > 0 ? bc.padYCertMed : 820;
                canvas.BeginText();
                canvas.SetFontAndSize(bfRB, fontSize20);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.iniC.hostname, 80, linenumber, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "55 หมู่4 ถนนเทพารักษ์ ตำบลบางพลีใหญ่ อำเภอบางพลี จังหวัด สมุทรปราการ 10540", 100, 780, 0);
                canvas.SetFontAndSize(bfRB, fontSize20);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.iniC.hostnamee, 80, linenumber - 15, 0);
                canvas.SetFontAndSize(bfR, 12);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.iniC.hostaddresst, 80, linenumber - 30, 0);
                canvas.EndText();
                linenumber = 720;

                //imgqrcode1.SetAbsolutePosition(530, linenumber + 30);
                //imgqrcode1.ScaleAbsoluteHeight(60);
                //imgqrcode1.ScaleAbsoluteWidth(60);
                //doc.Add(imgqrcode1);

                canvas.BeginText();
                canvas.SetFontAndSize(bfR, fontSize26);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "ใบรับรองแพทย์", PageSize.A4.Width / 2, linenumber + 40, 0);
                canvas.SetFontAndSize(bfR, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "เลขที่ " + certid, 530, linenumber + 40, 0);

                canvas.SetFontAndSize(bfR, fontSize1);
                linenumber += 10;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ข้าพเจ้า", 50, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............................................................................................", 85, linenumber-2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "แพทย์แผนปัจจุบันชั้นหนึ่งสาขาเวชกรรมเลขที่", 335, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............", 528, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, lbDtrName.Text.Trim(), 90, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtDtrCode.Text.Trim(), 535, linenumber + 3, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ขอรับรองว่า", 35, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................................................................................................................................................................", 90, linenumber - 2, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, txtNameT.Text.Trim()+" HN "+ txtHn.Text.Trim(), 93, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtNameT.Text.Trim() , 93, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, " HN " + txtHn.Text.Trim(), 335, linenumber + 3, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ได้รับการตรวจโรคจากโรงพยาบาลนี้เมื่อ วันที่", 35, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "............................................................................................", 225, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, vsDateTH, 230, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "เวลามาตรวจ", 470, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............", 528, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "น. ", 570, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, vstime, 535, linenumber + 3, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ปรากฏว่าป่วยเป็นโรค", 35, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".........................................................................................................................................................................", 130, linenumber - 2, 0);
                canvas.SetFontAndSize(bfRB, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLine1.Text.Trim(), 133, linenumber + 3, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..............................................................................................................................................................................................................", 35, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLine2.Text.Trim(), 40, linenumber + 3, 0);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..............................................................................................................................................................................................................", 35, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLine3.Text.Trim(), 40, linenumber + 3, 0);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..............................................................................................................................................................................................................", 35, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLine4.Text.Trim(), 40, linenumber + 3, 0);

                //linenumber -= 20;
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]   " + chk1.Text, 50, linenumber, 0);
                if (chk1.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/", 54, linenumber+2, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                }
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]   " + chk2.Text, 50, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................", 245, linenumber - 2, 0);
                
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ถึงวันที่", 360, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................", 395, linenumber - 2, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, ptt.visitDate, 400, linenumber + 3, 0);
                if (chk2.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/", 54, linenumber + 2, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                    DateTime dateend = new DateTime();
                    DateTime datestart = new DateTime();

                    DateTime.TryParse(txtChk2DateStart.Value.ToString(), out datestart);
                    DateTime.TryParse(txtChk2DateEnd.Value.ToString(), out dateend);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, datestart.ToString("dd-MM-") + (datestart.Year + 543).ToString(), 250, linenumber + 3, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, dateend.ToString("dd-MM-") + (dateend.Year + 543).ToString(), 400, linenumber + 3, 0);
                }//txtChk3NumDays
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]   " + chk3.Text, 50, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "............", 240, linenumber - 2, 0);
                
                canvas.ShowTextAligned(Element.ALIGN_LEFT, label6.Text.Trim(), 275, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................  ถึงวันที่  ..............................", 345, linenumber - 2, 0);
                
                if (chk3.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/", 54, linenumber + 2, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);

                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtChk3NumDays.Text.Trim(), 250, linenumber + 3, 0);
                    //canvas.ShowTextAligned(Element.ALIGN_LEFT, vsDateTH, 355, linenumber + 3, 0);
                    DateTime dateend = new DateTime();
                    DateTime datestart = new DateTime();
                    
                    DateTime.TryParse(txtChk3DateStart.Value.ToString(), out datestart);
                    datestart = new DateTime(datestart.Year, datestart.Month, datestart.Day, 0, 0, 0);
                    DateTime.TryParse(txtChk3DateEnd.Value.ToString(), out dateend);
                    dateend = new DateTime(dateend.Year, dateend.Month, dateend.Day, 0, 0, 0);

                    canvas.ShowTextAligned(Element.ALIGN_LEFT, datestart.ToString("dd-MM-") + (datestart.Year + 543).ToString(), 355, linenumber + 3, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, dateend.ToString("dd-MM-") + (dateend.Year + 543).ToString(), 495, linenumber + 3, 0);
                }
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]   " + chk4.Text, 50, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................  เวลา", 185, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................", 320, linenumber - 2, 0);
                if (chk4.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/", 54, linenumber + 2, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                    //txtChk4Date
                    DateTime date = new DateTime();
                    DateTime.TryParse(txtChk4Date.Text, out date);
                    if (date.Year < 2000)
                    {
                        date = date.AddYears(543);
                    }
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, date.ToString("dd-MM-yyyy"), 190, linenumber + 3, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtChk4Time.Text.Trim(), 325, linenumber + 3, 0);
                }
                //linenumber -= 20;
                linenumber -= 20;
                canvas.SetFontAndSize(bfRB, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(กรณีที่4. ใช้รับรองว่ามารับการตรวจรักษาจริงเท่านั้น  มิใช่เป็นใบรับรองแพทย์ลาป่วย)" , 30, linenumber, 0);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "***  เอกสารนี้ไม่สามารถใช้ประกอบการดำเนินคดีได้", 30, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);

                //linenumber -= 20;
                //linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ลงชื่อ", 380, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............................................................", 405, linenumber - 2, 0);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............................................................", 50, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(                                                      )", 385, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..................................................................", 395, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, lbDtrName.Text.Trim()+" ["+txtDtrCode.Text.Trim()+"]", 398, linenumber + 3, 0);

                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผู้รับเอกสารใบรับรองแพทย์", 75, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "แพทย์ผู้ตรวจโรค", 440, linenumber, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfR, 12);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "FM-MED-001 (01-05/02/59)(1/1)", 50, linenumber+6, 0);

                //logo.SetAbsolutePosition(310, linenumber+3);
                //logo.SetAbsolutePosition(240, linenumber + 3);
                //logo.ScaleAbsoluteHeight(60);
                //logo.ScaleAbsoluteWidth(60);
                imgqrcode1.SetAbsolutePosition(240, linenumber + 3);
                imgqrcode1.ScaleAbsoluteHeight(50);
                imgqrcode1.ScaleAbsoluteWidth(50);
                doc.Add(imgqrcode1);
                //imgqrcode1.SetAbsolutePosition(240, linenumber + 3);
                //imgqrcode1.SetAbsolutePosition(315, linenumber + 12);
                //imgqrcode1.ScaleAbsoluteHeight(50);
                //imgqrcode1.ScaleAbsoluteWidth(50);
                logo.SetAbsolutePosition(315, linenumber + 12);
                logo.ScaleAbsoluteHeight(60);
                logo.ScaleAbsoluteWidth(60);
                //doc.Add(logo);  //เอาออก มีแจ้งว่า ประกันไม่จ่าย

                canvas.EndText();
                canvas.Stroke();
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmCertDoctor printCertDoctoriTextSharpThai "+ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                doc.Close();
                output.Close();
                writer.Close();
                
                byte[] data = File.ReadAllBytes(filename);
                this.streamCertiDtr = new MemoryStream(data);
                this.streamCertiDtr.Position = 0;
                Application.DoEvents();
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = "1100000007";
                dsc.hn = txtHn.Text;
                
                dsc.an = chkIPD.Checked ? AN : "";
                dsc.vn = ptt.vn;
                
                dsc.visit_date = VSDATE;
                dsc.host_ftp = bc.iniC.hostFTP;
                //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                dsc.image_path = "";
                dsc.doc_group_sub_id = "1200000030";
                dsc.pre_no = PRENO;
                dsc.an_date = "";
                dsc.folder_ftp = bc.iniC.folderFTP;
                dsc.status_ipd = chkOPD.Checked ? "O": "I";
                dsc.row_no = "1";
                dsc.row_cnt = "1";
                dsc.status_ml = "2";
                dsc.ml_fm = "FM-MED-001";
                dsc.remark = "PDF";
                dsc.sort1 = CERTID.Equals("2NFLEAF") ? "2" : "1";
                if (chkOPD.Checked)
                {
                    if (CERTID.Equals("2NFLEAF"))
                    {
                        bc.bcDB.dscDB.voidDocScanByStatusCertMedical2NFLEAF(txtHn.Text, "FM-MED-001", VSDATE, PRENO, "");
                    }
                    else
                    {
                        bc.bcDB.dscDB.voidDocScanByStatusCertMedical(txtHn.Text, "FM-MED-001", VSDATE, PRENO, "");
                    }
                }
                else
                {

                }
                String reDocScanId = bc.bcDB.dscDB.insertScreenCapture(dsc, bc.userId);
                long chk = 0;
                if (long.TryParse(reDocScanId, out chk))
                {
                    dsc.image_path = txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + reDocScanId + ".PDF";
                    String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, reDocScanId);
                    FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive, bc.iniC.ProxyProxyType, bc.iniC.ProxyHost, bc.iniC.ProxyPort);
                    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-"));
                    ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                    if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename))
                    {
                        //File.Delete(filename);
                        //MedicalCertificate mcerti = new MedicalCertificate();
                        //mcerti.active = "1";
                        //mcerti.an = "";
                        //mcerti.certi_id = "";
                        //mcerti.certi_code = "";
                        //mcerti.dtr_code = txtDtrCode.Text.Trim();
                        //mcerti.dtr_name_t = lbDtrName.Text;
                        //mcerti.status_ipd = "O";
                        //mcerti.visit_date = VSDATE;
                        //mcerti.visit_time = txtVsTime.Text;
                        //mcerti.remark = "";
                        //mcerti.line1 = txtLine1.Text;
                        //mcerti.line2 = txtLine2.Text;
                        //mcerti.line3 = txtLine3.Text;
                        //mcerti.line4 = txtLine4.Text;
                        //mcerti.hn = txtHn.Text;
                        //mcerti.pre_no = PRENO;
                        //mcerti.ptt_name_e = txtNameE.Text;
                        //mcerti.ptt_name_t = txtNameT.Text;
                        //mcerti.doc_scan_id = reDocScanId;
                        //bc.bcDB.mcertiDB.insertMedicalCertificate(mcerti, "");
                        //String certid = bc.bcDB.mcertiDB.selectCertIDByHn(txtHn.Text, PRENO, VSDATE);
                        bc.bcDB.mcertiDB.updateDocScanIdByPk("555" + certid, reDocScanId);
                        if (chkOPD.Checked)
                        {
                            if (CERTID.Equals("2NFLEAF"))
                            {
                                bc.bcDB.vsDB.updateMedicalCertId2NFLEAF(txtHn.Text, PRENO, VSDATE, "555" + certid);
                            }
                            else
                            {
                                bc.bcDB.vsDB.updateMedicalCertId(txtHn.Text, PRENO, VSDATE, "555" + certid);
                            }
                        }
                        else
                        {

                        }
                        System.Threading.Thread.Sleep(200);
                    }
                }
                if ((HN.Length > 0) && (PRENO.Length > 0))
                {
                    this.Dispose(true);
                }
                else
                {
                    Process p = new Process();
                    ProcessStartInfo s = new ProcessStartInfo(filename);
                    p.StartInfo = s;
                    p.Start();
                }
            }
        }
        private void printCertDoctoriTextSharpEnglish()
        {
            String certid = "";
            certid = insertCertDoctor();
            certid = certid.Replace("555", "");

            String patheName = Environment.CurrentDirectory + "\\cert_med\\";
            if (!Directory.Exists(patheName))
            {
                Directory.CreateDirectory(patheName);
            }

            String dtrNameEng = "", pttNameEng="";
            dtrNameEng = bc.selectDoctorNameE(txtDtrCode.Text.Trim());
            //Patient ptt = bc.bcDB.pttDB.selectPatinetByHn(txtHn.Text.Trim());
            //pttNameEng = " "+ptt.MNC_FNAME_E + " " + ptt.MNC_LNAME_E;
            pttNameEng = txtNameE.Text.Trim();

            System.Drawing.Font fontMS12 = new System.Drawing.Font("Microsoft Sans Serif", 12);
            BaseFont bfR, bfR1, bfRB;
            BaseColor clrBlack = new iTextSharp.text.BaseColor(0, 0, 0);
            string myFont = Environment.CurrentDirectory + "\\THSarabunNew.ttf";
            string myFontB = Environment.CurrentDirectory + "\\THSarabunNew Bold.ttf";
            String filename = patheName + txtHn.Text.Trim() + "_" + VSDATE + "_" + PRENO + ".pdf";
            filename = (chkOPD.Checked) ? patheName + txtHn.Text.Trim() + "_" + VSDATE + "_" + PRENO + ".pdf" : patheName + txtHn.Text.Trim() + "_" + AN + ".pdf";

            bfR = BaseFont.CreateFont(myFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            bfR1 = BaseFont.CreateFont(myFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            bfRB = BaseFont.CreateFont(myFontB, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfR, 12, iTextSharp.text.Font.NORMAL, clrBlack);

            var logo = iTextSharp.text.Image.GetInstance(Environment.CurrentDirectory + "\\LOGO-BW-tran.jpg");
            logo.SetAbsolutePosition(20, PageSize.A4.Height - 60);
            logo.ScaleAbsoluteHeight(50);
            logo.ScaleAbsoluteWidth(50);

            FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 36, 36, 36, 36);
            FileStream output = null;
            PdfWriter writer = null;
            try
            {
                String vstime = "", vsDateTH = "", docscanid = "";
                docscanid = bc.bcDB.mcertiDB.selectDocScanIDByHn(txtHn.Text.Trim(), PRENO, VSDATE);
                DateTime vsdat1 = new DateTime();
                vstime = "0000" + ptt.visitTime;
                vstime = vstime.Substring(vstime.Length - 4);
                vstime = vstime.Substring(0, 2) + ":" + vstime.Substring(vstime.Length - 2, 2);
                DateTime.TryParse(ptt.visitDate, out vsdat1);
                if (vsdat1.Year < 2000)
                {
                    vsdat1 = vsdat1.AddYears(543);
                }
                vsDateTH = vsdat1.ToString("dd-MM-")+vsdat1.Year;
                qrcode.CodeType = C1.BarCode.CodeType.QRCode;
                qrcode.Text = txtHn.Text.Trim() + " " + txtNameT.Text.Trim() + " " + vsDateTH + " " + certid;
                System.Drawing.Image imgqrcode = qrcode.Image;
                var imgqrcode1 = iTextSharp.text.Image.GetInstance(imgqrcode, BaseColor.WHITE);

                output = new FileStream(filename, FileMode.Create);
                writer = PdfWriter.GetInstance(doc, output);
                doc.Open();
                doc.Add(logo);
                int linenumber = 820, colCenter = 200, fontSize0 = 8, fontSize1 = 16, fontSize2 = 18, fontSize20 = 20, fontSize4 = 22, fontSize26 = 26;
                PdfContentByte canvas = writer.DirectContent;
                linenumber = bc.padYCertMed > 0 ? bc.padYCertMed : 820;
                canvas.BeginText();
                canvas.SetFontAndSize(bfRB, fontSize20);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.iniC.hostname, 80, linenumber, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "55 หมู่4 ถนนเทพารักษ์ ตำบลบางพลีใหญ่ อำเภอบางพลี จังหวัด สมุทรปราการ 10540", 100, 780, 0);
                canvas.SetFontAndSize(bfRB, fontSize20);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.iniC.hostnamee, 80, linenumber - 15, 0);
                canvas.SetFontAndSize(bfR, 12);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.iniC.hostaddresst, 80, linenumber - 30, 0);
                canvas.EndText();
                linenumber = 720;
                canvas.BeginText();
                canvas.SetFontAndSize(bfR, fontSize26);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "Medical Certificate", PageSize.A4.Width / 2, linenumber + 40, 0);
                canvas.SetFontAndSize(bfR, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "NO. " + certid, 530, linenumber + 40, 0);

                canvas.SetFontAndSize(bfR, fontSize1);
                linenumber += 10;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "I", 50, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".............................................................................................................................", 60, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "Medical doctor license No.", 390, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............", 528, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, dtrNameEng, 70, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtDtrCode.Text.Trim(), 535, linenumber + 3, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "I hereby certfy that", 35, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".......................................................................................................................................................................", 132, linenumber - 2, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, pttNameEng + " HN " + txtHn.Text.Trim(), 140, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, pttNameEng , 140, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, " HN " + txtHn.Text.Trim(), 390, linenumber + 3, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "Received an examination from this hospital on", 35, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................", 265, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, vsDateTH, 270, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "examination time", 440, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............", 528, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "น. ", 570, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, vstime, 535, linenumber + 3, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "Diagnosis", 35, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..........................................................................................................................................................................................", 85, linenumber - 2, 0);
                canvas.SetFontAndSize(bfRB, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLine1.Text.Trim(), 103, linenumber + 3, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..............................................................................................................................................................................................................", 35, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLine2.Text.Trim(), 40, linenumber + 3, 0);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..............................................................................................................................................................................................................", 35, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLine3.Text.Trim(), 40, linenumber + 3, 0);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..............................................................................................................................................................................................................", 35, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLine4.Text.Trim(), 40, linenumber + 3, 0);

                //linenumber -= 20;
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]   for reimbursement", 50, linenumber, 0);
                if (chk1.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/", 54, linenumber + 2, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                }
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]   date of treatment", 50, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "....................................................................", 165, linenumber - 2, 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "until", 350, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................", 380, linenumber - 2, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, ptt.visitDate, 400, linenumber + 3, 0);
                if (chk2.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/", 54, linenumber + 2, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                    DateTime dateend = new DateTime();
                    DateTime datestart = new DateTime();

                    DateTime.TryParse(txtChk2DateStart.Value.ToString(), out datestart);
                    DateTime.TryParse(txtChk2DateEnd.Value.ToString(), out dateend);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, datestart.ToString("dd-MM-") + (datestart.Year).ToString(), 200, linenumber + 3, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, dateend.ToString("dd-MM-") + (dateend.Year).ToString(), 400, linenumber + 3, 0);
                }//txtChk3NumDays
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]   recommended period of absense from duty", 50, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "............", 290, linenumber - 2, 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "days  from", 325, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..............................  until  ..............................", 385, linenumber - 2, 0);

                if (chk3.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/", 54, linenumber + 2, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);

                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtChk3NumDays.Text.Trim(), 300, linenumber + 3, 0);
                    //canvas.ShowTextAligned(Element.ALIGN_LEFT, vsDateTH, 355, linenumber + 3, 0);
                    DateTime dateend = new DateTime();
                    DateTime datestart = new DateTime();

                    DateTime.TryParse(txtChk3DateStart.Value.ToString(), out datestart);
                    DateTime.TryParse(txtChk3DateEnd.Value.ToString(), out dateend);

                    canvas.ShowTextAligned(Element.ALIGN_LEFT, datestart.ToString("dd-MM-") + (datestart.Year ).ToString(), 395, linenumber + 3, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, dateend.ToString("dd-MM-") + (dateend.Year ).ToString(), 510, linenumber + 3, 0);
                }
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]   date of treatment" , 50, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................  time", 165, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................", 305, linenumber - 2, 0);
                if (chk4.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/", 54, linenumber + 2, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                    //txtChk4Date
                    DateTime date = new DateTime();
                    DateTime.TryParse(txtChk4Date.Text, out date);
                    if (date.Year < 2000)
                    {
                        date = date.AddYears(543);
                    }
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, date.ToString("dd-MM-yyyy"), 180, linenumber + 3, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtChk4Time.Text.Trim(), 325, linenumber + 3, 0);
                }
                //linenumber -= 20;
                linenumber -= 20;
                canvas.SetFontAndSize(bfRB, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(Case 4. Used only to certify that he/she has actually come for treatment. not to be used for sick leave)", 30, linenumber, 0);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "***  This document cannot be used for litigation", 30, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);

                //linenumber -= 20;
                //linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "sign", 380, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............................................................", 405, linenumber - 2, 0);
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............................................................", 50, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(                                                      )", 385, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..................................................................", 395, linenumber - 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, dtrNameEng + " [" + txtDtrCode.Text.Trim() + "]", 398, linenumber + 3, 0);

                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "signature of applicant", 75, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "signature of medical officer", 420, linenumber, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfR, 12);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "FM-MED-001 (01-05/02/59)(1/1)", 50, linenumber + 2, 0);

                //logo.SetAbsolutePosition(310, linenumber+3);
                imgqrcode1.SetAbsolutePosition(240, linenumber + 3);
                imgqrcode1.ScaleAbsoluteHeight(50);
                imgqrcode1.ScaleAbsoluteWidth(50);
                doc.Add(imgqrcode1);
                //imgqrcode1.SetAbsolutePosition(240, linenumber+3);
                logo.SetAbsolutePosition(305, linenumber + 12);
                logo.ScaleAbsoluteHeight(60);
                logo.ScaleAbsoluteWidth(60);
                doc.Add(logo);

                canvas.EndText();
                canvas.Stroke();
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmCertDoctor printCertDoctoriTextSharpEnglish " + ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                doc.Close();
                output.Close();
                writer.Close();

                byte[] data = File.ReadAllBytes(filename);
                streamCertiDtr = new MemoryStream(data);
                streamCertiDtr.Position = 0;
                Application.DoEvents();
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = "1100000006";
                dsc.hn = txtHn.Text;

                dsc.an = "";
                dsc.vn = ptt.vn;

                dsc.visit_date = VSDATE;
                dsc.host_ftp = bc.iniC.hostFTP;
                //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                dsc.image_path = "";
                dsc.doc_group_sub_id = "1200000030";
                dsc.pre_no = PRENO;
                dsc.an_date = "";
                dsc.folder_ftp = bc.iniC.folderFTP;
                dsc.status_ipd = chkOPD.Checked ? "O":"I";
                dsc.row_no = "1";
                dsc.row_cnt = "1";
                dsc.status_ml = "2";
                dsc.ml_fm = "FM-MED-001";
                dsc.remark = "PDF";
                bc.bcDB.dscDB.voidDocScanByStatusCertMedical(txtHn.Text, "FM-MED-001", VSDATE, PRENO, "");
                String reDocScanId = bc.bcDB.dscDB.insertScreenCapture(dsc, bc.userId);
                long chk = 0;
                if (long.TryParse(reDocScanId, out chk))
                {
                    dsc.image_path = txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + reDocScanId + ".PDF";
                    String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, reDocScanId);
                    FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive, bc.iniC.ProxyProxyType, bc.iniC.ProxyHost, bc.iniC.ProxyPort);
                    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-"));
                    ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                    if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename))
                    {
                        //File.Delete(filename);
                        //MedicalCertificate mcerti = new MedicalCertificate();
                        //mcerti.active = "1";
                        //mcerti.an = "";
                        //mcerti.certi_id = "";
                        //mcerti.certi_code = "";
                        //mcerti.dtr_code = txtDtrCode.Text.Trim();
                        //mcerti.dtr_name_t = lbDtrName.Text;
                        //mcerti.status_ipd = "O";
                        //mcerti.visit_date = VSDATE;
                        //mcerti.visit_time = txtVsTime.Text;
                        //mcerti.remark = "";
                        //mcerti.line1 = txtLine1.Text;
                        //mcerti.line2 = txtLine2.Text;
                        //mcerti.line3 = txtLine3.Text;
                        //mcerti.line4 = txtLine4.Text;
                        //mcerti.hn = txtHn.Text;
                        //mcerti.pre_no = PRENO;
                        //mcerti.ptt_name_e = txtNameE.Text;
                        //mcerti.ptt_name_t = txtNameT.Text;
                        //mcerti.doc_scan_id = reDocScanId;
                        //bc.bcDB.mcertiDB.insertMedicalCertificate(mcerti, "");
                        //String certid = bc.bcDB.mcertiDB.selectCertIDByHn(txtHn.Text, PRENO, VSDATE);
                        bc.bcDB.mcertiDB.updateDocScanIdByPk("555" + certid, reDocScanId);
                        bc.bcDB.vsDB.updateMedicalCertId(txtHn.Text, PRENO, VSDATE, "555" + certid);
                        System.Threading.Thread.Sleep(200);
                    }
                }
                if ((HN.Length > 0) && (PRENO.Length > 0))
                {
                    this.Dispose(true);
                }
                else
                {
                    Process p = new Process();
                    ProcessStartInfo s = new ProcessStartInfo(filename);
                    p.StartInfo = s;
                    p.Start();
                }
            }
        }
        internal RectangleF GetPageRect()
        {
            RectangleF rcPage = _c1pdf.PageRectangle;
            rcPage.Inflate(-72, -72);
            return rcPage;
        }
        private void initGrfDtrCert()
        {
            grfHn = new C1FlexGrid();
            grfHn.Font = fEdit;
            grfHn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHn.Location = new System.Drawing.Point(0, 0);

            pnPttinDept.Controls.Add(grfHn);

            grfHn.Rows.Count = 1;
            grfHn.Cols.Count = 6;
            grfHn.Cols[colHnHn].Caption = "HN";
            grfHn.Cols[colHnName].Caption = "Name";
            grfHn.Cols[colHnVn].Caption = "VN";

            grfHn.Cols[colHnHn].Width = 90;
            grfHn.Cols[colHnName].Width = 250;
            grfHn.Cols[colHnVn].Width = 100;

            grfHn.Cols[colHnHn].AllowEditing = false;
            grfHn.Cols[colHnName].AllowEditing = false;
            grfHn.Cols[colHnVn].AllowEditing = false;
            grfHn.Cols[colHnHn].Visible = true;
            grfHn.Cols[colHnName].Visible = true;
            grfHn.Cols[colHnVn].Visible = true;
            grfHn.Cols[colHnVsDate].Visible = true;
            grfHn.Cols[colHnPreno].Visible = false;

            grfHn.Click += GrfHn_Click;
        }

        private void GrfHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfHn.Row <= 0) return;
            if (grfHn.Col <= 0) return;
            AN = grfHn[grfHn.Row, colHnVn].ToString();
            setControl(grfHn[grfHn.Row, colHnHn].ToString(), grfHn[grfHn.Row, colHnVsDate].ToString(), grfHn[grfHn.Row, colHnPreno].ToString());
        }
        private void setControl(String hn, String vsdate, String preno)
        {
            String vstime = "";

            ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetVisitOPDByHn(hn, vsdate, preno);
            txtHn.Text = hn;
            txtNameT.Text = ptt.Name;
            txtNameE.Text = ptt.MNC_FNAME_E + " " + ptt.MNC_LNAME_E;
            txtDOB.Text = ptt.patient_birthday;

            txtVsDate.Value = ptt.visitDate;
            
            vstime = "0000"+ptt.visitTime;
            vstime = vstime.Substring(vstime.Length - 4);
            vstime = vstime.Substring(0, 2)+":"+vstime.Substring(vstime.Length - 2, 2);
            txtVsTime.Text = vstime;

            txtLine1.Text = "";
            txtLine2.Text = "";
            txtLine3.Text = "";
            txtLine4.Text = "";

            chk1.Checked = false;
            chk2.Checked = false;
            chk3.Checked = false;
            chk4.Checked = false;

            txtChk3NumDays.Text = "1";
            txtChk4Time.Text = vstime;
            //txtDtrCode.Text = ptt.dtrcode;
            
            txtDtrCode.Text = (DTRCODE.Length == 0) ? ptt.dtrcode : DTRCODE;
            if (chkIPD.Checked)
            {
                DataTable dt = new DataTable();
                String[] an = AN.Split('.');
                if (an.Length > 1)
                {
                    dt = bc.bcDB.vsDB.selectPttIPD(an[0], an[1]);
                    String dtrcodeS = dt.Rows[0]["MNC_DOT_CD_S"].ToString();
                    String dtrcodeR = dt.Rows[0]["MNC_DOT_CD_R"].ToString();
                    txtDtrCode.Text = dtrcodeR;
                    lbDtrName.Text = bc.selectDoctorName(txtDtrCode.Text.Trim());
                }
            }
            lbDtrName.Text = bc.selectDoctorName(txtDtrCode.Text.Trim());
            setControlDateClear();
        }
        class DeclarationSnippet : SnippetAutocompleteItem
        {
            public static string RegexSpecSymbolsPattern = @"[\^\$\[\]\(\)\.\\\*\+\|\?\{\}]";

            public DeclarationSnippet(string snippet)
                : base(snippet)
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var pattern = Regex.Replace(fragmentText, RegexSpecSymbolsPattern, "\\$0");
                if (Regex.IsMatch(Text, "\\b" + pattern, RegexOptions.IgnoreCase))
                    return CompareResult.Visible;
                return CompareResult.Hidden;
            }
        }
        /// <summary>
        /// Divides numbers and words: "123AND456" -> "123 AND 456"
        /// Or "i=2" -> "i = 2"
        /// </summary>
        class InsertSpaceSnippet : AutocompleteItem
        {
            string pattern;

            public InsertSpaceSnippet(string pattern)
                : base("")
            {
                this.pattern = pattern;
            }

            public InsertSpaceSnippet()
                : this(@"^(\d+)([a-zA-Z_]+)(\d*)$")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                if (Regex.IsMatch(fragmentText, pattern))
                {
                    Text = InsertSpaces(fragmentText);
                    if (Text != fragmentText)
                        return CompareResult.Visible;
                }
                return CompareResult.Hidden;
            }

            public string InsertSpaces(string fragment)
            {
                var m = Regex.Match(fragment, pattern);
                if (m.Groups[1].Value == "" && m.Groups[3].Value == "")
                    return fragment;
                return (m.Groups[1].Value + " " + m.Groups[2].Value + " " + m.Groups[3].Value).Trim();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return Text;
                }
            }
        }

        /// <summary>
        /// Inerts line break after '}'
        /// </summary>
        class InsertEnterSnippet : AutocompleteItem
        {
            int enterPlace = 0;

            public InsertEnterSnippet()
                : base("[Line break]")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var tb = Parent.TargetControlWrapper;

                var text = tb.Text;
                for (int i = Parent.Fragment.Start - 1; i >= 0; i--)
                {
                    if (text[i] == '\n')
                        break;
                    if (text[i] == '}')
                    {
                        enterPlace = i;
                        return CompareResult.Visible;
                    }
                }

                return CompareResult.Hidden;
            }

            public override string GetTextForReplace()
            {
                var tb = Parent.TargetControlWrapper;

                //insert line break
                tb.SelectionStart = enterPlace + 1;
                tb.SelectedText = "\n";
                Parent.Fragment.Start += 1;
                Parent.Fragment.End += 1;
                return Parent.Fragment.Text;
            }

            public override string ToolTipTitle
            {
                get
                {
                    return "Insert line break after '}'";
                }
            }
        }
        private void FrmCertDoctor_Load(object sender, EventArgs e)
        {
            this.Text = "Last Update 2024-01-11 fix big font ";
        }
    }
}
