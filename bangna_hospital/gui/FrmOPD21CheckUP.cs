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
    public partial class FrmOPD21CheckUP : Form
    {
        BangnaControl bc;
        OPDCheckUP opdc;
        String truestar = "";
        int leftp = 0;
        public FrmOPD21CheckUP(BangnaControl c, String truestar)
        {
            InitializeComponent();
            bc = c;
            this.truestar = truestar;
            initConfig();
        }
        private void initConfig()
        {
            opdc = new OPDCheckUP();
            setControl("");
        }

        private void setControl(String hn)
        {
            gbTrueStar.Visible = false;
            if (truestar.Length > 0)
            {
                gbTrueStar.Visible = true;
            }
            if (hn.Equals(""))
            {
                return;
            }
            
            //opdc = bc.opdcdb.selectByHn(hn);
            //if (!opdc.Id.Equals(""))
            //{
            //    txtABOGroup.Text = opdc.ABOGroup;
            //    txtAddr1.Text = opdc.Addr1;
            //    txtAddr2.Text = opdc.Addr2;
            //    txtAge.Text = opdc.Age;
            //    //txtAllergisOther.Text = opdc.AllergicOther;
            //    txtBloodPressure.Text = opdc.BloodPressure;
            //    txtBreath.Text = opdc.Breath;

            //    txtHeight.Text = opdc.Height;
            //    //txtHisOther.Text = opdc.HistOther;
            //    txtHn.Text = opdc.HN;
            //    //txtOROther.Text = opdcOROther;
            //    txtPatientName.Text = opdc.Name;
            //    txtPhone.Text = opdc.Phone;
            //    txtPulse.Text = opdc.Pulse;
            //    //txtResult.Text = opdc.Result;
            //    txtRhgroup.Text = opdc.RhGroup;
            //    txtSex.Text = opdc.Sex;
            //    //txtSgotN.Text = opdc.SgotN;
            //    //txtSgotResult.Text = opdc.SgotResult;
            //    //txtSgotSuggest.Text = opdc.SgotSuggest;
            //    //txtSgotV.Text = opdc.SgotV;
            //    //txtSgptN.Text = opdc.SgptN;
            //    //txtSgptV.Text = opdc.SgptV;
            //    //txtSuggest.Text = opdc.Suggest;
            //    txtTempu.Text = opdc.Tempu;
            //    txtWeight.Text = opdc.Weight;
            //}
            //else
            //{
                String date = "";
                date = System.DateTime.Today.Year + "-" + System.DateTime.Today.ToString("MM-dd");
                System.Data.DataTable dt = bc.bcDB.vsDB.selectVisitByHn2(hn, date);
                //DataTable dtor = bc.selectOPDViewOR(hn);
                if (dt.Rows.Count <= 0)
                {
                    clearControl();
                    return;
                }
                txtABOGroup.Text = dt.Rows[0]["mnc_blo_grp"].ToString();

                txtAddr1.Text = dt.Rows[0]["mnc_full_add"].ToString() != "" ? dt.Rows[0]["mnc_full_add"].ToString() : dt.Rows[0]["mnc_dom_add"].ToString() + " " + dt.Rows[0]["mnc_tum_dsc"].ToString() + " " + dt.Rows[0]["mnc_amp_dsc"].ToString() + " " + dt.Rows[0]["mnc_chw_dsc"].ToString() + " " + dt.Rows[0]["mnc_cur_poc"].ToString();
                txtAddr2.Text = "";
                txtId.Text = dt.Rows[0]["mnc_id_no"].ToString();
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
            //txtVn.Text = dt.Rows[0]["MNC_VN_NO"].ToString() + "." + dt.Rows[0]["MNC_VN_SEQ"].ToString() + "." + dt.Rows[0]["MNC_VN_SUM"].ToString();
            DateTime dt1 = new DateTime();
            int year = 0;
            dt1 = DateTime.Parse(dt.Rows[0]["mnc_bday"].ToString());
            year = dt1.Year;
            txtDOB.Text = dt1.Day.ToString("00")+"/"+ dt1.Month.ToString("00")+"/" + (year + 543);
            txtAge.Text = String.Concat(System.DateTime.Now.Year - year);

            //}
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
            String hn = "", name = "", doctor = "", fncd = "", birthday = "", dsDate = "", dsTime = "", an = "";

            decimal total = 0;

            bfR = BaseFont.CreateFont(myFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            bfR1 = BaseFont.CreateFont(myFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            bfRB = BaseFont.CreateFont(myFontB, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfR, 12, iTextSharp.text.Font.NORMAL, clrBlack);

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
            logo.SetAbsolutePosition(10, PageSize.A4.Height - 90);
            logo.ScaleAbsoluteHeight(70);
            logo.ScaleAbsoluteWidth(70);

            FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");

            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 36, 36, 36, 36);
            try
            {

                FileStream output = new FileStream(Environment.CurrentDirectory + "\\" + txtHn.Text.Trim() + ".pdf", FileMode.Create);
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
                int linenumber = 800, colCenter = 200, fontSize1 = 14, fontSize2 = 14;
                PdfContentByte canvas = writer.DirectContent;
                
                canvas.BeginText();
                canvas.SetFontAndSize(bfR, 12);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "โรงพยาบาล บางนา5  55 หมู่4 ถนนเทพารักษ์ ตำบลบางพลีใหญ่ อำเภอบางพลี จังหวัด สมุทรปราการ 10540", 100, linenumber, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "55 หมู่4 ถนนเทพารักษ์ ตำบลบางพลีใหญ่ อำเภอบางพลี จังหวัด สมุทรปราการ 10540", 100, 780, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "BANGNA 5 GENERAL HOSPITAL  55 M.4 Theparuk Road, Bangplee, Samutprakan Thailand0", 100, linenumber - 20, 0);
                canvas.EndText();
                linenumber = 720;
                canvas.BeginText();
                canvas.SetFontAndSize(bfR, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "ใบรับรองแพทย์", PageSize.A4.Width / 2, linenumber + 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "การตรวจสุขภาพแรงงานต่างด้าว", PageSize.A4.Width / 2, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "สถานที่ตรวจ  โรงพยาบาลบางนา 5", PageSize.A4.Width / 2, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "วันที่ตรวจ  " + dtpDate.Value.Day.ToString() + " " + bc.getMonth(dtpDate.Value.Month.ToString("00")) + " พ.ศ. " + (dtpDate.Value.Year + 543), PageSize.A4.Width / 2, linenumber -= 20, 0);
                canvas.EndText();
                linenumber = 660;
                canvas.BeginText();
                canvas.SetFontAndSize(bfRB, fontSize2);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "วันที่ตรวจ " + dtpDate.Value.Day.ToString() + " เดือน " + bc.cf.getMonth(dtpDate.Value.Month.ToString("00")) + " พ.ศ. " + (dtpDate.Value.Year + 543), 400, linenumber + 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ชื่อ ", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................................................................................................  ", 75, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtPatientName.Text.Trim(), 93, linenumber, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "เพศ  [  ]ชาย   [  ]หญิง     สถานภาพ  [  ]โสด   [  ]สมรส   [  ]อื่นๆ ", 300, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "เพศ ", 355, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]ชาย   [  ]หญิง  ", 380, linenumber, 0);
                if (txtSex.Text.ToUpper().Equals("M"))
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 384, linenumber, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                }
                else
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 420, linenumber, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                }
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "เลขที่บัตรประจำตัวบุคลผู้ไม่มีสัญชาติไทย ", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR1, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".......................................................... ", 218, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtId.Text.Trim(), 221, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "เลข Passport ", 355, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "............................................................. ", 410, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtPassport.Text.Trim(), 413, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "สัญชาติ ", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...................................................... ", 90, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, cboNation.Text.Trim(), 93, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "เชื้อชาติ ", 220, linenumber , 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................................ ", 253, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, cboRace.Text.Trim(), 253, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "สถานภาพ ", 385, linenumber , 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...................................................... ",425, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, cboMar.Text.Trim(), 428, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "วัน/เดือน/ปี เกิด ", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................ ", 122, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.datetoShow2(txtDOB.Text), 125, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "อายุ ", 220, linenumber , 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "................................. ", 240, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtAge.Text+" ปี", 243, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ชื่อนายจ้าง (บริษัท) ", 320, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................... ", 395, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, cboCompany.Text, 418, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ที่อยู่ของนายจ้าง ", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR1, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................................................................................................................................................................... ", 125, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtAddr1.Text, 128, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "....................................................................................................................................................................................................................... ", 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtAddr2.Text, 63, linenumber+3, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ที่อยู่ต่างประเทศ ", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................................................................................................................................................................... ", 125, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtAddress.Text, 128, linenumber, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจสุขภาพ ", (PageSize.A4.Width / 2)-30, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ความสูง ", 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".............. ", 93, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtHeight.Text, 96, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ชม.   น้ำหนัก ", 125, linenumber , 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "................... ", 180, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtWeight.Text, 183, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "กก.   ความดันโลหิต ", 225, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..................... ", 303, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtBloodPressure.Text, 306, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "มม.ปรอท   ชีพจร ", 350, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "................... ", 420, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtPulse.Text, 423, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ครั้ง / นาที ", 465, linenumber, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfRB, fontSize2);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, txtPatientName.Text.Trim(), 153, linenumber + 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ปรากฏว่า ไม่เป็นผู้ทุพพลภาพ ไร้ความสามารถ จิตฟั่นเฟือน ไม่สมประกอบ และปราศจากโรคเหล่านี้ ", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "และปราศจากโรคเหล่านี้ ", 60, linenumber -= 20, 0);
                //linenumber = 580;
                if (truestar.Equals("truestar"))
                {
                    leftp = 60;
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "1.	โรคเรื้อนในระยะติดต่อหรือในระยะที่ปรากฏอาการเป็นที่รังเกียจแก่สังคม (Leprosy) ", leftp, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ปกติ    [  ] ผิดปกติ    [  ] ให้ตรวจยืนยันรักษา", 380, linenumber, 0);
                    if (chk1Normal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 384, linenumber, 0);
                    }
                    else if (chk1AbNormal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 428, linenumber, 0);
                    }
                    else if (chk1Repeat.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 482, linenumber, 0);
                    }
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "2.	วัณโรคปอดในระยะติดต่อ (Active pulmonary tuberculosis) ", leftp, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ปกติ    [  ] ผิดปกติ    [  ] ให้ตรวจยืนยันรักษา", 380, linenumber, 0);
                    if (chk2Normal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 384, linenumber, 0);
                    }
                    else if (chk2AbNormal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 428, linenumber, 0);
                    }
                    else if (chk2Repeat.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 482, linenumber, 0);
                    }
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "3.	โรคติดยาเสพติดให้โทษ (Drug addiction) ", leftp, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ปกติ    [  ] ผิดปกติ    [  ] ให้ตรวจยืนยันรักษา", 380, linenumber, 0);
                    if (chk3Normal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 384, linenumber, 0);
                    }
                    else if (chk3AbNormal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 428, linenumber, 0);
                    }
                    else if (chk3Repeat.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 482, linenumber, 0);
                    }
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "4.	โรคพิษสุราเรื้อรัง (Chronic alcoholism) ", leftp, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ปกติ    [  ] ผิดปกติ", 380, linenumber, 0);
                    if (chk4Normal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 384, linenumber, 0);
                    }
                    else if (chk4AbNormal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 428, linenumber, 0);
                    }
                    //else if (chk4Repeat.Checked)
                    //{
                    //    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 482, linenumber, 0);
                    //}
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "5.	โรคเท้าช้างในระยะที่ปรากฏอาการที่เป็นที่รังเกียจแก่สังคม (Filariasis) ", leftp, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ปกติ    [  ] ผิดปกติ    [  ] ให้ตรวจยืนยันรักษา", 380, linenumber, 0);
                    if (chk5Normal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 384, linenumber, 0);
                    }
                    else if (chk5AbNormal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 428, linenumber, 0);
                    }
                    else if (chk5Repeat.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 482, linenumber, 0);
                    }
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "6.	ซิฟิลิสในระยะที่ 3 (Syphilis Latent)", leftp, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ปกติ    [  ] ผิดปกติ    [  ] ให้ตรวจยืนยันรักษา", 380, linenumber, 0);
                    if (chk6Normal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 384, linenumber, 0);
                    }
                    else if (chk6AbNormal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 428, linenumber, 0);
                    }
                    else if (chk6Repeat.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 482, linenumber, 0);
                    }
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "7.	โรคจิตฟั่นเฟือนหรือปัญญาอ่อน (Schizophrenia or Mental Retardation)", leftp, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ปกติ    [  ] ผิดปกติ", 380, linenumber, 0);
                    if (chk7Normal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 384, linenumber, 0);
                    }
                    else if (chk7AbNormal.Checked)
                    {
                        canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 428, linenumber, 0);
                    }
                    //else if (chk7Repeat.Checked)
                    //{
                    //    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 482, linenumber, 0);
                    //}
                }
                else
                {
                    leftp = 150;
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "1.	โรคเรื้อนในระยะติดต่อหรือในระยะที่ปรากฏอาการเป็นที่รังเกียจแก่สังคม (Leprosy) ", leftp, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "2.	วัณโรคปอดในระยะติดต่อ (Active pulmonary tuberculosis) ", leftp, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "3.	โรคติดยาเสพติดให้โทษ (Drug addiction) ", leftp, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "4.	โรคพิษสุราเรื้อรัง (Chronic alcoholism) ", leftp, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "5.	โรคเท้าช้างในระยะที่ปรากฏอาการที่เป็นที่รังเกียจแก่สังคม (Filariasis) ", leftp, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "6.	ซิฟิลิสในระยะที่ 3 (Syphilis Latent)", leftp, linenumber -= 20, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "7.	โรคจิตฟั่นเฟือนหรือปัญญาอ่อน (Schizophrenia or Mental Retardation)", leftp, linenumber -= 20, 0);
                }
                
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "หมายเหตุ : สำหรับสตรี", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจตั้งครรภ์ [  ]ไม่พบการตั้งครรภ์  [  ]พบการตั้งครรภ์", 170, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                if (chkPregOn.Checked)
                {                    
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 252, linenumber, 0);                    
                }else if (chkPregOff.Checked){                    
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 335, linenumber, 0);                    
                }
                //canvas.SetFontAndSize(bfR, fontSize1);
                //canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "สรุปผลการตรวจ", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] สุขภาพสมบูรณ์ดี", 200, linenumber -= 20, 0);
                if (chk1.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 204, linenumber, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                }
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ผ่านการตรวจสุขภาพ แต่ต้องติดตามผลการตรวจยืนยัน และให้การรักษา", 200, linenumber -= 20, 0);
                if (chk2.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 204, linenumber, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                }
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ไม่ผ่านเพราะ....................................................................", 200, linenumber -= 20, 0);
                if (chk3.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 204, linenumber, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                }
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] สุขภาพไม่สมบูรณ์แข็งแรง ที่เป็นอุปสรรคต่อการทำงาน", 200, linenumber -= 20, 0);
                if (chk4.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 204, linenumber, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                }
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] เป็นโรคต้องห้ามมิให้ทำงาน (ตามประกาศกระทรวงสาธารณสุข)", 200, linenumber -= 20, 0);
                if (chk5.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 204, linenumber, 0);
                    canvas.SetFontAndSize(bfR, fontSize1);
                }

                canvas.EndText();

                //canvas.BeginText();
                //canvas.SaveState();
                canvas.SetLineWidth(0.05f);
                canvas.MoveTo(80, 140);//vertical
                canvas.LineTo(80, 225);
                canvas.MoveTo(80, 140);//Hericental
                canvas.LineTo(150, 140);
                canvas.MoveTo(80, 225);//Hericental
                canvas.LineTo(150, 225);
                canvas.MoveTo(150, 140);//vertical
                canvas.LineTo(150, 225);

                //canvas.MoveTo(560, 640);//vertical
                //canvas.LineTo(560, 110);
                //canvas.Stroke();
                //canvas.RestoreState();

                canvas.BeginText();
                linenumber -= 20;
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผู้เข้ารับการตรวจ ...................................................  ", 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "แพทย์ผู้ตรวจ ...................................................", colCenter+180, linenumber , 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(.....................................................)", 120, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(.........................................................)", colCenter + 220, linenumber , 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtDoctorName.Text.Trim(), colCenter + 225, linenumber+3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ใบรับรองแพทย์ฉบับนี้ให้ใช้ได้ 60 วันนับแต่วันที่ตรวจร่างกาย", 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "วันที่ .........................................................", colCenter + 200, linenumber, 0);
                //canvas.SetFontAndSize(bfRB, fontSize2);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, cboCompany.Text.Trim(), colCenter, linenumber + 5, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "ตรวจสมรรถภาพการทำงานของปอด  ", 60, linenumber -= 20, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, txtLung.Text.Trim(), 160, linenumber, 0);




                //linenumber -= 5;
                //canvas.SetFontAndSize(bfR, fontSize1);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "บันทึกสัญญาณชีพ  ", 60, linenumber -= 20, 0);
                //String aaaa = "H.Rate: ครั้ง/min  BP:  mmHg ";
                //if (!txtPulse.Text.Equals(""))
                //{
                //    //String[] aaa = txtPulse.Text.Split('/');
                //    aaaa = "H.Rate: " + txtPulse.Text + " ครั้ง/min R.Rate: " + txtBreath.Text + " ครั้ง/min  BP: " + txtBloodPressure.Text + " mmHg ";
                //}
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............................................................................................", colCenter + 70, linenumber - 3, 0);
                //canvas.SetFontAndSize(bfRB, fontSize2);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, aaaa, colCenter + 70, linenumber + 2, 0);

                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "WT: " + txtWeight.Text + " Kgs  HT:  " + txtHeight.Text + " Cms", colCenter + 70, (linenumber -= 20) + 2, 0);
                //canvas.SetFontAndSize(bfR, fontSize1);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "...............................................................................................", colCenter + 70, linenumber - 3, 0);


                //linenumber = 100;
                ////canvas.SetFontAndSize(bfR, 12);
                ////canvas.ShowTextAligned(Element.ALIGN_LEFT, "มีอายุการใช้งาน 3 เดือน (VALID FOR THREE MONTHS)  ", 60, linenumber -= 20, 0);
                //canvas.SetFontAndSize(bfR, fontSize1);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "ลายมือชื่อ ....................................................... ", 370, linenumber - 3, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "Signature  " + txtDoctorName.Text.Trim(), 370, linenumber, 0);
                ////canvas.SetFontAndSize(bfR, 12);
                ////canvas.ShowTextAligned(Element.ALIGN_LEFT, "ISO 9001:2000 ทุกหน่วยงาน  ", 60, linenumber -= 20, 0);
                ////canvas.SetFontAndSize(bfR, fontSize1);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "(แพทย์ผู้ตรวจ)  ", 420, linenumber, 0);

                canvas.EndText();

                canvas.Stroke();
                //canvas.RestoreState();
                //pB1.Maximum = dt.Rows.Count;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                doc.Close();
                Process p = new Process();
                ProcessStartInfo s = new ProcessStartInfo(Environment.CurrentDirectory + "\\" + txtHn.Text.Trim() + ".pdf");
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

        private void txtHn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setControl(txtHn.Text);
            }
        }

        private void txtDoctorId_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDoctorName.Text = bc.selectDoctorName(txtDoctorId.Text.Trim());
            }
        }

        private void chkPragOn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPregOn.Checked)
            {
                chkPregOff.Checked = false;
            }
        }

        private void chkPragOff_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPregOff.Checked)
            {
                chkPregOn.Checked = false;
            }
        }

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked)
            {
                chk2.Checked = false;
                chk3.Checked = false;
                chk4.Checked = false;
                chk5.Checked = false;
            }
        }

        private void chk2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk2.Checked)
            {
                chk1.Checked = false;
                chk3.Checked = false;
                chk4.Checked = false;
                chk5.Checked = false;
            }
        }

        private void chk3_CheckedChanged(object sender, EventArgs e)
        {
            if (chk3.Checked)
            {
                chk1.Checked = false;
                chk2.Checked = false;
                chk4.Checked = false;
                chk5.Checked = false;
            }
        }

        private void chk4_CheckedChanged(object sender, EventArgs e)
        {
            if (chk4.Checked)
            {
                chk1.Checked = false;
                chk2.Checked = false;
                chk3.Checked = false;
                chk5.Checked = false;
            }
        }

        private void chk5_CheckedChanged(object sender, EventArgs e)
        {
            if (chk5.Checked)
            {
                chk1.Checked = false;
                chk2.Checked = false;
                chk3.Checked = false;
                chk4.Checked = false;
            }
        }
        
        private void cboNation_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboRace.Text = cboNation.Text;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
        private void FrmOPD21CheckUP_Load(object sender, EventArgs e)
        {

        }
    }
}
