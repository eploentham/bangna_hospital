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
    public partial class FrmOPDLicenseDriver : Form
    {
        BangnaControl bc;
        OPDCheckUP opdc;
        int leftp = 0;
        public FrmOPDLicenseDriver(BangnaControl c)
        {
            InitializeComponent();
            bc = c;
            initConfig();
        }
        private void initConfig()
        {
            opdc = new OPDCheckUP();
            btnPrintLicenseDriver.Click += BtnPrintLicenseDriver_Click;
            txtHn.KeyUp += TxtHn_KeyUp;
            txtDoctorId.KeyUp += TxtDoctorId_KeyUp;
            chk1.CheckedChanged += Chk1_CheckedChanged;
            chk2.CheckedChanged += Chk2_CheckedChanged;
            chk3.CheckedChanged += Chk3_CheckedChanged;
            chk4.CheckedChanged += Chk4_CheckedChanged;

            setControl("");
        }

        private void Chk4_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chk4.Checked)
            {
                chk1.Checked = false;
                chk2.Checked = false;
                chk3.Checked = false;
                //chk5.Checked = false;
            }
        }

        private void Chk3_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chk3.Checked)
            {
                chk1.Checked = false;
                chk2.Checked = false;
                chk4.Checked = false;
                //chk5.Checked = false;
            }
        }

        private void Chk2_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chk2.Checked)
            {
                chk1.Checked = false;
                chk3.Checked = false;
                chk4.Checked = false;
                //chk5.Checked = false;
            }
        }

        private void Chk1_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chk1.Checked)
            {
                chk2.Checked = false;
                chk3.Checked = false;
                chk4.Checked = false;
                //chk5.Checked = false;
            }
        }

        private void TxtDoctorId_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtDoctorName.Text = bc.selectDoctorName(txtDoctorId.Text.Trim());
            }
        }

        private void TxtHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setControl(txtHn.Text);
            }
        }

        private void BtnPrintLicenseDriver_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            genPDF();
        }

        private void setControl(String hn)
        {
            if (hn.Equals(""))
            {
                return;
            }            
            String date = "";
            date = System.DateTime.Today.Year + "-" + System.DateTime.Today.ToString("MM-dd");
            System.Data.DataTable dt = bc.bcDB.vsDB.selectVisitByHn3(hn, date);
            //DataTable dtor = bc.selectOPDViewOR(hn);
            if (dt.Rows.Count <= 0)
            {
                clearControl();
                return;
            }
            txtABOGroup.Text = dt.Rows[0]["mnc_blo_grp"].ToString();
            txtId.Text = dt.Rows[0]["mnc_id_no"].ToString();
            txtAddr1.Text = dt.Rows[0]["mnc_full_add"].ToString() != "" ? dt.Rows[0]["mnc_full_add"].ToString() : dt.Rows[0]["mnc_dom_add"].ToString() + " ต." + dt.Rows[0]["mnc_tum_dsc"].ToString() + " อ." + dt.Rows[0]["mnc_amp_dsc"].ToString() + " จ." + dt.Rows[0]["mnc_chw_dsc"].ToString() + " " + dt.Rows[0]["mnc_cur_poc"].ToString();
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
            //txtPhone.Text = dt.Rows[0]["mnc_cur_tel"].ToString();
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
            txtVn.Text = dt1.Day.ToString("00") + "/" + dt1.Month.ToString("00") + "/" + (year + 543);
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
            //txtPhone.Text = "";
            txtPulse.Text = "";
            //txtResult.Text = "";
            txtRhgroup.Text = "";
            txtSex.Text = "";

            txtTempu.Text = "";
            txtWeight.Text = "";
        }
        private void genPDF()
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
                int linenumber = 820, colCenter = 200, fontSize0 = 8, fontSize1 = 14, fontSize2 = 16, fontSize3 = 18;
                PdfContentByte canvas = writer.DirectContent;

                canvas.BeginText();
                canvas.SetFontAndSize(bfR, 12);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "โรงพยาบาล บางนา5  55 หมู่4 ถนนเทพารักษ์ ตำบลบางพลีใหญ่ อำเภอบางพลี จังหวัด สมุทรปราการ 10540", 100, linenumber, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "55 หมู่4 ถนนเทพารักษ์ ตำบลบางพลีใหญ่ อำเภอบางพลี จังหวัด สมุทรปราการ 10540", 100, 780, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "BANGNA 5 GENERAL HOSPITAL  55 M.4 Theparuk Road, Bangplee, Samutprakan Thailand0", 100, linenumber-15, 0);
                canvas.EndText();
                linenumber = 720;
                canvas.BeginText();
                canvas.SetFontAndSize(bfR, fontSize3);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "ใบรับรองแพทย์", PageSize.A4.Width / 2, linenumber+40, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ส่วนที่ 1", 50, linenumber+=10, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ของผู้ขอรับใบรับรองสุขภาพ", 100, linenumber, 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ข้าพเจ้า นาย/นาง/นางสาว ", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "................................................................................................................................................................  ", 170, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtPatientName.Text.Trim(), 173, linenumber, 0);

                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "สถานที่อยู่ (ที่สามารถติดต่อได้)", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtAddr1.Text,172, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "................................................................................................................................................................  ", 170, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...................................................................................................................................................................................................................  ", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtAddr2.Text, 55, linenumber+5, 0);
                canvas.SetFontAndSize(bfR, fontSize1);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "หมายเลขบัตรประจำตัวประชาชน", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtId.Text, 172, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "................................................................... ", 170, linenumber - 5, 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ข้าพเจ้าขอใบรับรองสุขภาพ โดยมีประวัติสุขภาพดังนี้", 330, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "1. โรคประจำตัว", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ไม่มี     [  ] มี  ระบุ ........................................................................................................................", 200, linenumber, 0);
                
                if (chk1Normal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 205, linenumber, 0);
                }
                else if (chk1AbNormal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 248, linenumber, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txt1AbNormal.Text, 290, linenumber + 5, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "2. อุบัติเหตุ และ ผ่าตัด", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ไม่มี     [  ] มี  ระบุ ........................................................................................................................", 200, linenumber, 0);
                if (chk2Normal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 205, linenumber, 0);
                }
                else if (chk2AbNormal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 248, linenumber, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txt2AbNormal.Text, 290, linenumber + 5, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "3. เคยเข้ารับการรักษาในโรงพยาบาล", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ไม่มี     [  ] มี  ระบุ ........................................................................................................................", 200, linenumber, 0);
                if (chk3Normal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 205, linenumber, 0);
                }
                else if (chk3AbNormal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 248, linenumber, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txt3AbNormal.Text, 290, linenumber+5, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "4. ประวัติอื่นที่สำคัญ", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..................................................................................................................................................................................  ", 130, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtOther.Text, 133, linenumber, 0);
                
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ลงชื่อ .............................................................. วันที่ .............. เดือน .............................. พ.ศ. ...............", 150, linenumber -= 40, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, DateTime.Now.ToString("dd"), 340, linenumber + 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.getMonth(DateTime.Now.ToString("MM")), 400, linenumber + 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, DateTime.Now.ToString("yyyy"), 490, linenumber + 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "ในกรณีเด็กทีไม่สามารถรับรองตนเองได้ ให้ผู้ปกครองลงนามรับรองแทนได้", PageSize.A4.Width / 2, linenumber -= 20, 0);

                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ส่วนที่ 2   ของแพทย์", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "สถานที่ตรวจ", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "................................................................................................................  ", 100, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, TxtHospitalName.Text, 110, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "วันที่ ", 380, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..........", 398, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, DateTime.Now.ToString("dd"), 401, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, " เดือน", 420, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".........................", 445, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.getMonth(DateTime.Now.ToString("MM")), 448, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, " พ.ศ. ", 500, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "............", 520, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, DateTime.Now.ToString("yyyy"), 523, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(1) ข้าพเจ้า นายแพทย์/แพทย์หญิง", 40, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".....................................................................................................................  ", 175, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtDoctorName.Text, 178, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ใบอนุญาตประกอบวิชาชีพเวชกรรมเลขที่", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".....................................  ", 205, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtDoctorId.Text, 208, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "สถานพยาบาลชื่อ", 292, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".........................................................................................  ", 355, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, TxtHospitalName.Text, 358, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ที่อยู่", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "55 หมู่4 ถนนเทพารักษ์ ตำบลบางพลีใหญ่ อำเภอบางพลี จังหวัดสมุทรปราการ 10540", 77, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "....................................................................................................................................................................................................................  ", 72, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ได้ตรวจร่างกาย นาย/นาง/นางสาว", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtPatientName.Text, 187, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..................................................................................................................  ", 182, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "แล้วเมื่อ     วันที่", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..........", 120, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, DateTime.Now.ToString("dd"), 123, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, " เดือน", 142, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".........................", 167, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.getMonth(DateTime.Now.ToString("MM")), 170, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, " พ.ศ. ", 230, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "............", 250, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, DateTime.Now.ToString("yyyy"), 253, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, " มีรายละเอียดดังนี้ ", 290, linenumber, 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "น้ำหนักตัว", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtWeight.Text, 105, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "......................", 88, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "กก. ความสูง", 140, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtHeight.Text, 205, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "......................", 190, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "เซนติเมตร ความดันโลหิต", 240, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtBloodPressure.Text, 345, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "......................", 340, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "มม.ปรอท ชีพจร ", 400, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtPulse.Text, 480, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................", 468, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ครั้ง/นาที ", 520, linenumber, 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "สภาพร่างกายทั่วไปอยู่ในเกณฑ์  [  ] ปกติ   [  ] ผิดปกติ  ระบุ", 50, linenumber -= 20, 0);
                if (chkNormal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 173, linenumber, 0);
                }
                else if (chkAbNormal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 214, linenumber, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtAbnormal.Text, 287, linenumber, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "....................................................................................................................", 280, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ขอรับรองว่า บุคคลดังกล่าว ไม่เป็นผู้มีร่างกายทุพพลภาพจนไม่สามารถปฏิบัติหน้าที่ได้ ไม่ปรากฏอาการของโรคจิต ", 100, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "หรือจิตฟั่นเฟือน หรือปัญญาอ่อน ไม่ปรากฏอาการของการติดยาเสพติดให้โทษ และอาการของโรคพิษสุราเรื้อรัง และไม่", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ปรากฏอาการและอาการแสดงของโรคต่อไปนี้", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(1) โรคเรื้อนในระยะติดต่อ หรือในระยะที่ปรากฏอาการเป็นที่รังเกียจแก่สังคม", 80, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(2) วัณโรคในระยะอันตราย", 80, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(3) โรคเท้าช้างในระยะที่ปรากฏอาการเป็นที่รังเกียจแก่สังคม", 80, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(4) ถ้าจำเป็นต้องตรวจหาโรคที่เกี่ยวข้องกับการปฏิบัติงานของผูัรับการตรวจให้ระบุข้อนี้ ", 80, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".......................................................................", 400, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "สรุปความเห็นและข้อแนะนำของแพทย์ ", 100, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]  " + chk1.Text, 120, linenumber -= 20, 0);
                if (chk1.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 124, linenumber, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]  " + chk2.Text, 120, linenumber -= 20, 0);
                if (chk2.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 124, linenumber, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]  " + chk3.Text, 120, linenumber -= 20, 0);
                if (chk3.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 124, linenumber, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]  " + chk4.Text, 120, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...................................................................................", 178, linenumber - 5, 0);
                if (chk4.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 124, linenumber, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txt4Other.Text, 180, linenumber, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, ".......................................................................................................................................", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ลงชื่อ ", 270, linenumber -= 40, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...................................................................................", 290, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "แพทย์ผู้ตรวจร่างกาย", 480, linenumber, 0);
                //canvas.SetFontAndSize(bfRB, fontSize2);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, txtDoctorName.Text, 213, linenumber, 0);

                canvas.SetFontAndSize(bfR, fontSize0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "หมายเหตุ (1) ต้องเป็นแพทย์ซึ่งได้ขึ้นทะเบียนรับใบอนุญาตประกอบวิชาชีพเวชกรรม ", 50, linenumber -= 10, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(2) ให้แสดงว่าเป็นผู้มีร่างกายสมบูรณ์เพียงใด ใบรับรองแพทย์ฉบับนี้ให้ใช้ได้ 1 เดือน นับแต่วันที่ตรวจร่างกาย ", 72, linenumber -= 10, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(3) คำรับรองนี้เป็นการตรวจวินิจฉัยเบื้องต้น ", 72, linenumber -= 10, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "แบบฟอร์มนี้ได้รับการรับรองจากมติคณะกรรมการแพทยสภาในการประชุมครั้งที่ ค/2561 วันที่ 14 สิงหาคม 2561 ", 50, linenumber -= 10, 0);

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
        private void FrmOPDLicenseDriver_Load(object sender, EventArgs e)
        {

        }
    }
}
