using iTextSharp.text;
using iTextSharp.text.pdf;
using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using bangna_hospital.control;

namespace bangna_hospital.gui
{
    public partial class FrmOPD2CheckUP : Form
    {
        BangnaControl bc;
        OPDCheckUP opdc;
        Boolean pageLoad = false, keyDistrict = false;

        String id = "", HN="";
        public FrmOPD2CheckUP(BangnaControl c, String hn)
        {
            InitializeComponent();
            bc = c;
            this.HN = hn;
            //id = opdcid;
            initConfig();
        }
        public FrmOPD2CheckUP(String hn)
        {
            InitializeComponent();
            bc = new BangnaControl();
            this.HN = hn;
            //id = opdcid;
            initConfig();
        }
        private void initConfig()
        {
            opdc = new OPDCheckUP();
            setControl();
        }
        private void setControl()
        {
            if (HN.Equals(""))
            {
                return;
            }

            opdc = bc.bcDB.opdcDB.selectByHn(HN);
            if (!opdc.Id.Equals(""))
            {
                txtABOGroup.Text = opdc.ABOGroup;
                txtAddr1.Text = opdc.Addr1;
                txtAddr2.Text = opdc.Addr2;
                txtAge.Text = opdc.Age;
                //txtAllergisOther.Text = opdc.AllergicOther;
                txtBloodPressure.Text = opdc.BloodPressure;
                txtBreath.Text = opdc.Breath;
                
                txtHeight.Text = opdc.Height;
                //txtHisOther.Text = opdc.HistOther;
                txtHn.Text = opdc.HN;
                //txtOROther.Text = opdcOROther;
                txtPatientName.Text = opdc.Name;
                txtPhone.Text = opdc.Phone;
                txtPulse.Text = opdc.Pulse;
                //txtResult.Text = opdc.Result;
                txtRhgroup.Text = opdc.RhGroup;
                txtSex.Text = opdc.Sex;
                //txtSgotN.Text = opdc.SgotN;
                //txtSgotResult.Text = opdc.SgotResult;
                //txtSgotSuggest.Text = opdc.SgotSuggest;
                //txtSgotV.Text = opdc.SgotV;
                //txtSgptN.Text = opdc.SgptN;
                //txtSgptV.Text = opdc.SgptV;
                //txtSuggest.Text = opdc.Suggest;
                txtTempu.Text = opdc.Tempu;
                txtWeight.Text = opdc.Weight;
            }
            else
            {
                String date = "";
                date = System.DateTime.Today.Year + "-" + System.DateTime.Today.ToString("MM-dd");
                System.Data.DataTable dt = bc.bcDB.vsDB.selectVisitByHn2(HN, date);
                //DataTable dtor = bc.selectOPDViewOR(hn);
                if (dt.Rows.Count <= 0)
                {
                    clearControl();
                    return;
                }
                txtABOGroup.Text = dt.Rows[0]["mnc_blo_grp"].ToString();

                txtAddr1.Text = dt.Rows[0]["mnc_full_add"].ToString() != "" ? dt.Rows[0]["mnc_full_add"].ToString() : dt.Rows[0]["mnc_dom_add"].ToString() + " " + dt.Rows[0]["mnc_tum_dsc"].ToString() + " " + dt.Rows[0]["mnc_amp_dsc"].ToString() + " " + dt.Rows[0]["mnc_chw_dsc"].ToString() + " " + dt.Rows[0]["mnc_cur_poc"].ToString();
                txtAddr2.Text = "";
                txtAge.Text = dt.Rows[0]["MNC_AGE"].ToString();
                //txtAllergisOther.Text = dt.Rows[0][bc.opdcdb.opdc.AllergicOther].ToString();
                //txtBloodPressure.Text = dt.Rows[0][bc.opdcdb.opdc.BloodPressure].ToString();
                txtBreath.Text = dt.Rows[0]["mnc_breath"].ToString();
                txtPulse.Text = dt.Rows[0]["mnc_ratios"].ToString();

                txtHeight.Text = dt.Rows[0]["mnc_high"].ToString();
                //txtHisOther.Text = dt.Rows[0][bc.opdcdb.opdc.HistOther].ToString();
                txtHn.Text = dt.Rows[0]["MNC_HN_NO"].ToString();
                //txtOROther.Text = dt.Rows[0]["MNC_HN_NO"].ToString();
                txtPatientName.Text = dt.Rows[0]["prefix"].ToString() + " " + dt.Rows[0]["MNC_FNAME_T"].ToString() + " " + dt.Rows[0]["MNC_LNAME_T"].ToString();
                txtPhone.Text = dt.Rows[0]["mnc_cur_tel"].ToString();
                txtBloodPressure.Text = dt.Rows[0]["mnc_bp1_l"].ToString();
                //txtResult.Text = dt.Rows[0][bc.opdcdb.opdc.Result].ToString();
                txtRhgroup.Text = dt.Rows[0]["mnc_blo_rh"].ToString();
                txtSex.Text = dt.Rows[0]["mnc_sex"].ToString();

                //txtSuggest.Text = dt.Rows[0][bc.opdcdb.opdc.Suggest].ToString();
                txtTempu.Text = dt.Rows[0]["mnc_temp"].ToString();
                txtWeight.Text = dt.Rows[0]["mnc_weight"].ToString();
                txtVn.Text = dt.Rows[0]["MNC_VN_NO"].ToString() + "." + dt.Rows[0]["MNC_VN_SEQ"].ToString() + "." + dt.Rows[0]["MNC_VN_SUM"].ToString();

            }
        }
        private void clearControl()
        {
            txtABOGroup.Text = "";
            txtAddr1.Text = "";
            txtAddr2.Text = "";
            txtAge.Text = "";
            //txtAllergisOther.Text = "";
            txtBloodPressure.Text = "";
            txtBreath.Text = "";
            
            txtHeight.Text = "";
            //txtHisOther.Text = "";
            txtHn.Text = "";
            //txtOROther.Text = "";
            txtPatientName.Text = "";
            txtPhone.Text = "";
            txtPulse.Text = "";
            //txtResult.Text = "";
            txtRhgroup.Text = "";
            txtSex.Text = "";

            txtTempu.Text = "";
            txtWeight.Text = "";
        }
        private void getPDF()
        {
            System.Drawing.Font font = new System.Drawing.Font("Microsoft Sans Serif", 12);
            iTextSharp.text.pdf.BaseFont bfR, bfR1, bfRB;
            iTextSharp.text.BaseColor clrBlack = new iTextSharp.text.BaseColor(0, 0, 0);
            //MemoryStream ms = new MemoryStream();
            string myFont = Environment.CurrentDirectory + "\\THSarabun.ttf";
            string myFontB = Environment.CurrentDirectory + "\\THSarabun Bold.ttf";
            String name = "", doctor = "", fncd = "", birthday = "", dsDate = "", dsTime = "", an = "";

            decimal total = 0;

            bfR = BaseFont.CreateFont(myFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            bfR1 = BaseFont.CreateFont(myFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            bfRB = BaseFont.CreateFont(myFontB, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfR, 12, iTextSharp.text.Font.NORMAL, clrBlack);
            String patheName = Environment.CurrentDirectory + "\\checkup\\";
            if (!Directory.Exists(patheName)) { Directory.CreateDirectory(patheName); }
            String[] aa = dsDate.Split(',');
            if (aa.Length > 1)
            {
                dsDate = aa[0];
                an = aa[1];
            }
            String[] bb = dsDate.Split('*');
            if (bb.Length > 1)
            {
                dsDate = bb[0];
                dsTime = bb[1];
            }

            var logo = iTextSharp.text.Image.GetInstance(Environment.CurrentDirectory + "\\LOGO-BW-tran.jpg");
            logo.SetAbsolutePosition(30, PageSize.A4.Height - 90);
            logo.ScaleAbsoluteHeight(70);
            logo.ScaleAbsoluteWidth(70);

            FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");

            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 36, 36, 36, 36);
            try
            {

                FileStream output = new FileStream(patheName + HN + ".pdf", FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(doc, output);
                doc.Open();
                //PdfContentByte cb = writer.DirectContent;
                //ColumnText ct = new ColumnText(cb);
                //ct.Alignment = Element.ALIGN_JUSTIFIED;

                //Paragraph heading = new Paragraph("Chapter 1", fntHead);
                //heading.Leading = 30f;
                //doc.Add(heading);
                //Image L = Image.GetInstance(imagepath + "/l.gif");
                //logo.SetAbsolutePosition(doc.Left, doc.Top - 180);
                doc.Add(logo);

                //doc.Add(new Paragraph("Hello World", fntHead));

                Chunk c;
                String foobar = "Foobar Film Festival";
                //float width_helv = bfR.GetWidthPoint(foobar, 12);
                //c = new Chunk(foobar + ": " + width_helv, fntHead);
                //doc.Add(new Paragraph(c));

                //if (dt.Rows.Count > 24)
                //{
                //    doc.NewPage();
                //    doc.Add(new Paragraph(string.Format("This is a page {0}", 2)));
                //}
                int i = 0, r = 0, row2 = 0, rowEnd = 24;
                //r = dt.Rows.Count;
                int next = r / 24;
                int linenumber = 800, colCenter = 200, fontSize1=14, fontSize2=14;
                PdfContentByte canvas = writer.DirectContent;
                //canvas.SaveState();
                //canvas.SetLineWidth(0.05f);
                //canvas.MoveTo(400, 806);
                //canvas.LineTo(400, 626);
                //canvas.MoveTo(508.7f, 806);
                //canvas.LineTo(508.7f, 626);
                //canvas.MoveTo(280, 788);
                //canvas.LineTo(520, 788);
                //canvas.MoveTo(280, 752);
                //canvas.LineTo(520, 752);
                //canvas.MoveTo(280, 716);
                //canvas.LineTo(520, 716);
                //canvas.MoveTo(280, 680);
                //canvas.LineTo(520, 680);
                //canvas.MoveTo(280, 644);
                //canvas.LineTo(520, 644);
                //canvas.Stroke();
                //canvas.RestoreState();
                // Adding text with PdfContentByte.showTextAligned()
                canvas.BeginText();
                canvas.SetFontAndSize(bfR, 12);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "โรงพยาบาล บางนา5  55 หมู่4 ถนนเทพารักษ์ ตำบลบางพลีใหญ่ อำเภอบางพลี จังหวัด สมุทรปราการ 10540", 100, linenumber, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "55 หมู่4 ถนนเทพารักษ์ ตำบลบางพลีใหญ่ อำเภอบางพลี จังหวัด สมุทรปราการ 10540", 100, 780, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "BANGNA 5 GENERAL HOSPITAL  55 M.4 Theparuk Road, Bangplee, Samutprakan Thailand0", 100, linenumber - 20, 0);
                canvas.EndText();
                linenumber = 720;
                canvas.BeginText();
                canvas.SetFontAndSize(bfR, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "ใบรับรองแพทย์", PageSize.A4.Width / 2, linenumber+20, 0);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "MEDICAL CERTIFICATE", PageSize.A4.Width / 2, linenumber, 0);
                canvas.EndText();
                linenumber = 680;
                canvas.BeginText();
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "วันที่ตรวจ " + dtpDate.Value.Day.ToString() + " เดือน " + bc.getMonth(dtpDate.Value.Month.ToString("00")) + " พ.ศ. " + (dtpDate.Value.Year + 543), 375, linenumber + 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ข้าพเจ้า " , 80, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".......................................................................................................................................................................................", 110, linenumber-5 , 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtDoctorName.Text.Trim(), 113, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "แพทย์ปริญญาใบอนุญาตประกอบวิชาชีพเวชกรรม เลขที่ " , 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "................................................................................................................. ", 270, linenumber-3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtDoctorId.Text.Trim(), 273, linenumber , 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ได้ทำการตรวจร่างกาย " , 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...................................................................................................................................................................... " , 150, linenumber-5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtPatientName.Text.Trim(), 153, linenumber+2, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ปรากฏว่า ไม่เป็นผู้ทุพพลภาพ ไร้ความสามารถ จิตฟั่นเฟือน ไม่สมประกอบ ", 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "และปราศจากโรคเหล่านี้ ", 60, linenumber -= 20, 0);
                linenumber = 580;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "1.	โรคเรื้อนในระยะติดต่อหรือในระยะที่ปรากฏอาการเป็นที่รังเกียจแก่สังคม (Leprosy) ", 150, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "2.	วัณโรคปอดในระยะติดต่อ (Active pulmonary tuberculosis) ", 150, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "3.	โรคติดยาเสพติดให้โทษ (Drug addiction) ", 150, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "4.	โรคพิษสุราเรื้อรัง (Chronic alcoholism) ", 150, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "5.	โรคเท้าช้างในระยะที่ปรากฏอาการที่เป็นที่รังเกียจแก่สังคม (Filariasis) ", 150, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "6.	ซิฟิลิสในระยะที่ 2 (Syphilis Secondary)", 150, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "7.	โรคจิตฟั่นเฟือนหรือปัญญาอ่อน (Schizophrenia or Mental Retardation)", 150, linenumber -= 20, 0);

                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "เห็นว่า  ", 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "............................................................................................................................................................................................", 100, linenumber - 3, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, cboResult.Text.Trim(), colCenter, linenumber+5, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "ตรวจสมรรถภาพการทำงานของปอด  ", 60, linenumber -= 20, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLung.Text.Trim(), 160, linenumber, 0);

                canvas.SetFontAndSize(bfR, fontSize1);
                if (!txtXRay.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจ X-Ray ปอด ", 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtXRay.Text, colCenter + 70, linenumber, 0);
                }
                if (!txtCBC.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจ CBC " , 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCBC.Text, colCenter + 70, linenumber, 0);
                }
                if (!txtAmp.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจหาสารเสพติดในปัสสาวะ (Amphetamine) ", 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter +70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtAmp.Text, colCenter+70, linenumber, 0);
                }
                if (!txtPrag.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจ Pragnancy ", 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtPrag.Text, colCenter + 70, linenumber, 0);
                }
                if (!txtHbsAg.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจ Hbs Ag ", 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtHbsAg.Text, colCenter + 70, linenumber, 0);
                }
                if (!txtUA.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจ UA ", 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtUA.Text, colCenter + 70, linenumber, 0);
                }
                if (!txtHIV.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจ Anti HIV ", 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtHIV.Text, colCenter + 70, linenumber, 0);
                }
                if (!txtEye.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจ วัดสายตา ", 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtEye.Text, colCenter + 70, linenumber, 0);
                }
                if (!txtEar.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจ การได้ยิน ", 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtEar.Text, colCenter + 70, linenumber, 0);
                }
                if (!txtVDRL.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจ VDRL  ", 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtVDRL.Text, colCenter + 70, linenumber, 0);
                }
                if (!txtBloodG.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจ กรุ๊ปเลือด  ", 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtBloodG.Text, colCenter + 70, linenumber, 0);
                }
                if (!txtFBS.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจหาน้ำตาลในเลือด (FBS)  ", 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtFBS.Text, colCenter + 70, linenumber, 0);
                }
                if (!txt1.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจ อุจจาระ  ", 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txt1.Text, colCenter + 70, linenumber, 0);
                }
                if (!txtLead.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจ Lead  ", 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLead.Text, colCenter + 70, linenumber, 0);
                }


                linenumber -= 5;
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "บันทึกสัญญาณชีพ  ", 60, linenumber -= 20, 0);
                String aaaa = "H.Rate: ครั้ง/min  BP:  mmHg ";
                if (!txtPulse.Text.Equals(""))
                {
                    //String[] aaa = txtPulse.Text.Split('/');
                    aaaa = "H.Rate: " + txtPulse.Text + " ครั้ง/min R.Rate: " + txtBreath.Text + " ครั้ง/min  BP: " + txtBloodPressure.Text + " mmHg ";
                }
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..................................................................................................................", colCenter + 70, linenumber - 3, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, aaaa, colCenter + 70, linenumber+2, 0);
                
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "WT: " + txtWeight.Text + " Kgs  HT:  " + txtHeight.Text + " Cms", colCenter + 70, (linenumber -= 20)+2, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);


                if (!txtT1.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, cboL1.Text.Trim(), 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtT1.Text, colCenter + 70, linenumber+1, 0);
                }
                if (!txtT2.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtL2.Text.Trim(), 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtT2.Text, colCenter + 70, linenumber+1, 0);
                }
                if (!txtT3.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtL3.Text.Trim(), 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtT3.Text, colCenter + 70, linenumber+1, 0);
                }
                if (!txtT4.Text.Equals(""))
                {
                    canvas.SetFontAndSize(bfR, fontSize1);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtL4.Text.Trim(), 60, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................", colCenter + 70, linenumber - 3, 0);
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtT4.Text, colCenter + 70, linenumber+1, 0);
                }

                linenumber = 100;
                canvas.SetFontAndSize(bfR, 12);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "มีอายุการใช้งาน 3 เดือน (VALID FOR THREE MONTHS)  ", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ลายมือชื่อ ....................................................... ", 370, linenumber-3, 0);
                canvas.SetFontAndSize(bfR, 12);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผ่านการรับรองมาตรฐาน  ", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "Signature  " + txtDoctorName.Text.Trim() +" [ว."+txtDoctorId.Text.Trim()+"]", 370, linenumber, 0);
                canvas.SetFontAndSize(bfR, 12);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ISO 9001:2000 ทุกหน่วยงาน  ", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(แพทย์อาชีวะเวชศาสตร์)  ", 420, linenumber, 0);
                canvas.SetFontAndSize(bfR, 12);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "FM-NUR-001/3 (แก้ไขครั้งที่ 00 15/02/53)  ", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "Attending physician  ", 420, linenumber, 0);
                canvas.EndText();

                //canvas.BeginText();
                //canvas.SetFontAndSize(bfR, 16);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "วัน/เวลา", 50, 620, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "รายการ", 250, 620, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "จำนวน", 405, 620, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "ราคา", 460, 620, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "รวมราคา", 510, 620, 0);
                ////canvas.ShowTextAlignedKerned(Element.ALIGN_LEFT, "ชื่อแพทย์ผู้รักษา "+ 60, 660, 644, 0);
                ////canvas.ShowTextAlignedKerned(Element.ALIGN_LEFT, "ชื่อแพทย์ผู้รักษา " + 60, 640, 644, 0);
                //canvas.EndText();

                //canvas.MoveTo(520, 640);//vertical Amount
                //canvas.LineTo(520, 110);
                canvas.Stroke();
                canvas.RestoreState();
                //pB1.Maximum = dt.Rows.Count;

            }
            catch (Exception ex)
            {
                //Log(ex.Message);
            }
            finally
            {
                doc.Close();
                Process p = new Process();
                ProcessStartInfo s = new ProcessStartInfo(patheName + HN + ".pdf");
                //s.Arguments = "/c dir *.cs";
                p.StartInfo = s;
                //p.StartInfo.Arguments = "/c dir *.cs";
                //p.StartInfo.UseShellExecute = false;
                //p.StartInfo.RedirectStandardOutput = true;
                p.Start();

                //string output = p.StandardOutput.ReadToEnd();
                //p.WaitForExit();
                //Application.Exit();
            }
        }

        private void btnPrintOPD2_Click(object sender, EventArgs e)
        {
            getPDF();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            setControl();
        }

        private void cboXRay_Click(object sender, EventArgs e)
        {
            
        }

        private void cboCBC_Click(object sender, EventArgs e)
        {
            
        }

        private void cboAmp_Click(object sender, EventArgs e)
        {
            
        }

        private void cboPrag_Click(object sender, EventArgs e)
        {
            
        }

        private void cboXRay_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtXRay.Text = cboXRay.Text;
        }

        private void cboCBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCBC.Text = cboCBC.Text;
        }

        private void cboAmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAmp.Text = cboAmp.Text;
        }

        private void cboPrag_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPrag.Text = cboPrag.Text;
        }

        private void cboHbsAg_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHbsAg.Text = cboHbsAg.Text;
        }

        private void cboUA_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUA.Text = cboUA.Text;
        }

        private void cboHIV_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHIV.Text = cboHIV.Text;
        }

        private void cboEye_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEye.Text = cboEye.Text;
        }

        private void cboEar_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEar.Text = cboEar.Text;
        }

        private void cboVDRL_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtVDRL.Text = cboVDRL.Text;
        }

        private void cboFBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFBS.Text = cboFBS.Text;
        }

        private void cbo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt1.Text = cbo1.Text;
        }

        private void cboLead_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtLead.Text = cboLead.Text;
        }

        private void btnXRay_Click(object sender, EventArgs e)
        {
            txtXRay.Text = cboXRay.Items[0].ToString();
        }

        private void btnCBC_Click(object sender, EventArgs e)
        {
            txtCBC.Text = cboCBC.Items[0].ToString();
        }

        private void btnAmp_Click(object sender, EventArgs e)
        {
            txtAmp.Text = cboAmp.Items[0].ToString();
        }

        private void btnPrag_Click(object sender, EventArgs e)
        {
            txtPrag.Text = cboPrag.Items[0].ToString();
        }

        private void btnHbsAg_Click(object sender, EventArgs e)
        {
            txtHbsAg.Text = cboHbsAg.Items[0].ToString();
        }

        private void btnUA_Click(object sender, EventArgs e)
        {
            txtUA.Text = cboUA.Items[0].ToString();
        }

        private void btnHIV_Click(object sender, EventArgs e)
        {
            txtHIV.Text = cboHIV.Items[0].ToString();
        }

        private void btnEye_Click(object sender, EventArgs e)
        {
            txtEye.Text = cboEye.Items[0].ToString();
        }

        private void btnEar_Click(object sender, EventArgs e)
        {
            txtEar.Text = cboEar.Items[0].ToString();
        }

        private void btnVDRL_Click(object sender, EventArgs e)
        {
            txtVDRL.Text = cboVDRL.Items[0].ToString();
        }

        private void btnFBS_Click(object sender, EventArgs e)
        {
            txtFBS.Text = cboFBS.Items[0].ToString();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txt1.Text = cbo1.Items[0].ToString();
        }

        private void btnLead_Click(object sender, EventArgs e)
        {
            txtLead.Text = cboLead.Items[0].ToString();
        }

        private void cboBloodG_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBloodG.Text = cboBloodG.Text;
        }
        private void txtDoctorId_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDoctorName.Text = bc.selectDoctorName(txtDoctorId.Text.Trim());
            }
        }

        private void txtHn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setControl();
            }
        }
        private void FrmOPD2CheckUP_Load(object sender, EventArgs e)
        {
            this.Text = "ตรวจสุขภาพ (Check Up) 2024-06-17 09:09";
        }
    }
}
