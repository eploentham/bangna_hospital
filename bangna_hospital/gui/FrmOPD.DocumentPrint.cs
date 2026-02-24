using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.C1Pdf;
using C1.Win.C1FlexGrid;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    partial class FrmOPD
    {
        System.Drawing.Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt2, famt2B, famt4B, famt2BL, famt5, famt5B, famt5BL, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2;
        System.Drawing.Font fStaffN, fStaffNs, fStaffNB;
        private void initFont()
        {
            fEdit = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            famt1 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 1, FontStyle.Regular);
            famt5 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
            famt5BL = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Underline);
            famt5B = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Bold);
            famt2 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 2, FontStyle.Regular);
            famt2B = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 2, FontStyle.Bold);
            famt2BL = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 2, FontStyle.Underline);
            famt4B = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 4, FontStyle.Bold);

            famt7 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Regular);
            famt7B = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
            famtB14 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 14, FontStyle.Bold);
            famtB30 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 30, FontStyle.Bold);
            ftotal = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 60, FontStyle.Bold);
            fPrnBil = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fque = new System.Drawing.Font(bc.iniC.queFontName, bc.queFontSize + 3, FontStyle.Bold);
            fqueB = new System.Drawing.Font(bc.iniC.queFontName, bc.queFontSize + 7, FontStyle.Bold);
            fEditS = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            fEditS1 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 1, FontStyle.Regular);

            fPDF = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fPDFs2 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            fPDFl2 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 2, FontStyle.Regular);
            fPDFs6 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 6, FontStyle.Regular);
            fPDFs8 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 8, FontStyle.Regular);
            fStaffN = new System.Drawing.Font(bc.iniC.staffNoteFontName, bc.staffNoteFontSize, FontStyle.Regular);
            fStaffNs = new System.Drawing.Font(bc.iniC.staffNoteFontName, bc.staffNoteFontSize - 2, FontStyle.Regular);
            fStaffNB = new System.Drawing.Font(bc.iniC.staffNoteFontName, bc.staffNoteFontSize + 1, FontStyle.Bold);
        }
        private void setGrfScanToPrint()
        {
            SetDefaultPrinter(bc.iniC.printerA4);
            System.Threading.Thread.Sleep(500);

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += Pd_PrintPageA4;
            //here to select the printer attached to user PC
            if (bc.iniC.statusShowPrintDialog.Equals("1"))
            {
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.Document = pd;
                DialogResult result = printDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pd.Print();     //this will trigger the Print Event handeler PrintPage
                }
            }
            else
            {
                pd.Print();
            }
        }
        private void Pd_PrintPageA4(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(streamPrint);

                float newWidth = img.Width * 100 / img.HorizontalResolution;
                float newHeight = img.Height * 100 / img.VerticalResolution;

                float widthFactor = newWidth / e.MarginBounds.Width;
                float heightFactor = newHeight / e.MarginBounds.Height;

                if (widthFactor > 1 | heightFactor > 1)
                {
                    if (widthFactor > heightFactor)
                    {
                        widthFactor = 1;
                        newWidth = newWidth / widthFactor;
                        newHeight = newHeight / widthFactor;
                        //newWidth = newWidth / 1.2;
                        //newHeight = newHeight / 1.2;
                    }
                    else
                    {
                        newWidth = newWidth / heightFactor;
                        newHeight = newHeight / heightFactor;
                    }
                }
                e.Graphics.DrawImage(img, 0, 0, (int)newWidth, (int)newHeight);
                //}
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmScanView1 Pd_PrintPageA4 error " + ex.Message);
            }
        }
        private void genDriverPDF()
        {
            System.Drawing.Font font = new System.Drawing.Font("Microsoft Sans Serif", 12);
            iTextSharp.text.pdf.BaseFont bfR, bfR1, bfRB;
            iTextSharp.text.BaseColor clrBlack = new iTextSharp.text.BaseColor(0, 0, 0);
            //MemoryStream ms = new MemoryStream();
            string myFont = Environment.CurrentDirectory + "\\THSarabunNew.ttf";
            string myFontB = Environment.CurrentDirectory + "\\THSarabunNew Bold.ttf";
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
            logo.SetAbsolutePosition(20, PageSize.A4.Height - 80);
            logo.ScaleAbsoluteHeight(60);
            logo.ScaleAbsoluteWidth(60);

            FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");

            Document doc = new iTextSharp.text.Document(PageSize.A4, 36, 36, 36, 36);
            try
            {

                FileStream output = new FileStream(Environment.CurrentDirectory + "\\" + txtCheckUPHN.Text.Trim() + "_driver.pdf", FileMode.Create);
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
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "BANGNA 5 GENERAL HOSPITAL  55 M.4 Theparuk Road, Bangplee, Samutprakan Thailand", 100, linenumber - 15, 0);
                canvas.EndText();
                linenumber = 720;
                canvas.BeginText();
                canvas.SetFontAndSize(bfR, fontSize3);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "ใบรับรองแพทย์  (สำหรับใบอนุญาตขับรถ)", PageSize.A4.Width / 2, linenumber + 50, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ส่วนที่ 1", 50, linenumber += 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ของผู้ขอรับใบรับรองสุขภาพ", 100, linenumber, 0);
                //linenumber = linenumber - 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ข้าพเจ้า นาย/นาง/นางสาว ", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".............................................................................................................................................................................  ", 170, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, cboCheckUPPrefixT.Text + " " + txtCheckUPNameT.Text.Trim() + " " + txtCheckUPSurNameT.Text.Trim(), 173, linenumber, 0);

                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "สถานที่อยู่ (ที่สามารถติดต่อได้)", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfRB, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPAddr1.Text, 172, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".............................................................................................................................................................................  ", 170, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..................................................................................................................................................................................................................................  ", 50, linenumber -= 26, 0);
                canvas.SetFontAndSize(bfRB, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPAddr2.Text, 56, linenumber + 3, 0);
                canvas.SetFontAndSize(bfR, fontSize1);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "หมายเลขบัตรประจำตัวประชาชน", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPPttPID.Text, 175, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "................................................................... ", 170, linenumber - 5, 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ข้าพเจ้าขอใบรับรองสุขภาพ โดยมีประวัติสุขภาพดังนี้", 330, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "1. โรคประจำตัว", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ไม่มี     [  ] มี  ระบุ ........................................................................................................................", 200, linenumber, 0);

                if (chkCheckUP1Normal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 205, linenumber, 0);
                }
                else if (chkCheckUP1AbNormal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 248, linenumber, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUP1AbNormal.Text, 290, linenumber + 5, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "2. อุบัติเหตุ และ ผ่าตัด", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ไม่มี     [  ] มี  ระบุ ........................................................................................................................", 200, linenumber, 0);
                if (chkCheckUP2Normal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 205, linenumber, 0);
                }
                else if (chkCheckUP2AbNormal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 248, linenumber, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txt2AbNormal.Text, 290, linenumber + 5, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "3. เคยเข้ารับการรักษาในโรงพยาบาล", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ไม่มี     [  ] มี  ระบุ ........................................................................................................................", 200, linenumber, 0);
                if (chkCheckUP3Normal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 205, linenumber, 0);
                }
                else if (chkCheckUP3AbNormal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 248, linenumber, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txt3AbNormal.Text, 290, linenumber + 5, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "4. โรคลมชัก", 50, linenumber -= 20, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "..................................................................................................................................................................................  ", 130, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ไม่มี     [  ] มี  ระบุ ........................................................................................................................", 200, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, txtOther.Text, 133, linenumber, 0);
                if (chkCheckUP4Normal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 205, linenumber, 0);
                }
                else if (chkCheckUP4AbNormal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 248, linenumber, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txt4AbNormal.Text, 290, linenumber + 5, 0);
                }

                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "5. ประวัติอื่นที่สำคัญ", 50, linenumber -= 20, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "..................................................................................................................................................................................  ", 130, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ] ไม่มี     [  ] มี  ระบุ ........................................................................................................................", 200, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, txtOther.Text, 133, linenumber, 0);
                if (chkCheckUP5Normal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 205, linenumber, 0);
                }
                else if (chkCheckUP5AbNormal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 248, linenumber, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUP5AbNormal.Text, 290, linenumber + 5, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "* ในกรณีมีโรคลมชัก  ให้แนบประวัติการรักษาจากแพทย์ผู้รักษาว่า ท่านปลอดภัยจากอาการชัก มากกว่า 1 ปี เพื่ออนุญาตให้ขับรถได้", 50, linenumber -= 20, 0);

                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ลงชื่อ .............................................................. วันที่ .............. เดือน .............................. พ.ศ. ...............", 150, linenumber -= 40, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, DateTime.Now.ToString("dd"), 340, linenumber + 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.getMonth(DateTime.Now.ToString("MM")), 400, linenumber + 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, (DateTime.Now.Year < 2500 ? DateTime.Now.Year + 543 : DateTime.Now.Year).ToString(), 490, linenumber + 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize1);
                //canvas.ShowTextAligned(Element.ALIGN_CENTER, "ในกรณีเด็กทีไม่สามารถรับรองตนเองได้ ให้ผู้ปกครองลงนามรับรองแทนได้", PageSize.A4.Width / 2, linenumber -= 20, 0);

                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ส่วนที่ 2   ของแพทย์", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "สถานที่ตรวจ", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "................................................................................................................  ", 100, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.iniC.hostname, 110, linenumber, 0);
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
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................", 520, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, (DateTime.Now.Year < 2500 ? DateTime.Now.Year + 543 : DateTime.Now.Year).ToString(), 523, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(1) ข้าพเจ้า นายแพทย์/แพทย์หญิง", 40, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".....................................................................................................................  ", 175, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPDoctorName.Text, 178, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ใบอนุญาตประกอบวิชาชีพเวชกรรมเลขที่", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".....................................  ", 205, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPDoctorId.Text, 208, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "สถานพยาบาลชื่อ", 292, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "............................................................................................  ", 355, linenumber - 5, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, bc.iniC.hostname, 358, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ที่อยู่", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "55 หมู่4 ถนนเทพารักษ์ ตำบลบางพลีใหญ่ อำเภอบางพลี จังหวัดสมุทรปราการ 10540", 77, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".........................................................................................................................................................................................................................  ", 72, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ได้ตรวจร่างกาย นาย/นาง/นางสาว", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, cboCheckUPPrefixT.Text + " " + txtCheckUPNameT.Text.Trim() + " " + txtCheckUPSurNameT.Text.Trim(), 187, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "..................................................................................................................  ", 182, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "เมื่อ     วันที่", 50, linenumber -= 20, 0);
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
                int year = DateTime.Now.Year < 2500 ? DateTime.Now.Year + 543 : DateTime.Now.Year;

                canvas.ShowTextAligned(Element.ALIGN_LEFT, (DateTime.Now.Year < 2500 ? DateTime.Now.Year + 543 : DateTime.Now.Year).ToString(), 253, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, " มีรายละเอียดดังนี้ ", 290, linenumber, 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "น้ำหนักตัว", 50, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPWeight.Text, 105, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "......................", 88, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "กก. ความสูง", 140, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPHeight.Text, 205, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "......................", 190, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "เซนติเมตร ความดันโลหิต", 240, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPBp1L.Text, 345, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "......................", 340, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "มม.ปรอท ชีพจร ", 400, linenumber, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPHrate.Text, 480, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................", 468, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ครั้ง/นาที ", 520, linenumber, 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "สภาพร่างกายทั่วไปอยู่ในเกณฑ์  [  ] ปกติ   [  ] ผิดปกติ  ระบุ", 50, linenumber -= 20, 0);
                if (chkCheckUPNormal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 173, linenumber, 0);
                }
                else if (chkCheckUPAbNormal.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 214, linenumber, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPAbnormal.Text, 287, linenumber, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".............................................................................................................................", 280, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ขอรับรองว่า บุคคลดังกล่าว ไม่เป็นผู้มีร่างกายทุพพลภาพจนไม่สามารถปฏิบัติหน้าที่ได้ ไม่ปรากฏอาการของโรคจิต ", 100, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "หรือจิตฟั่นเฟือน หรือปัญญาอ่อน ไม่ปรากฏอาการของการติดยาเสพติดให้โทษ และอาการของโรคพิษสุราเรื้อรัง และไม่", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ปรากฏอาการและอาการแสดงของโรคต่อไปนี้", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(1) โรคเรื้อนในระยะติดต่อ หรือในระยะที่ปรากฏอาการเป็นที่รังเกียจแก่สังคม", 80, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(2) วัณโรคในระยะอันตราย", 80, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(3) โรคเท้าช้างในระยะที่ปรากฏอาการเป็นที่รังเกียจแก่สังคม", 80, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(4) ถ้าจำเป็นต้องตรวจหาโรคที่เกี่ยวข้องกับการปฏิบัติงานของผู้รับการตรวจให้ระบุข้อนี้ ", 80, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".......................................................................", 400, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "สรุปความเห็นและข้อแนะนำของแพทย์ ", 100, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]  " + chkCheckUP1.Text, 120, linenumber -= 20, 0);
                if (chkCheckUP1.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 124, linenumber, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]  " + chkCheckUP2.Text, 120, linenumber -= 20, 0);
                if (chkCheckUP2.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 124, linenumber, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]  " + chkCheckUP3.Text, 120, linenumber -= 20, 0);
                if (chkCheckUP3.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 124, linenumber, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "[  ]  " + chkCheckUP4.Text, 120, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...................................................................................", 178, linenumber - 5, 0);
                if (chkCheckUP4.Checked)
                {
                    canvas.SetFontAndSize(bfRB, fontSize2);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "/ ", 124, linenumber, 0);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, txt4Other.Text, 180, linenumber, 0);
                }
                canvas.SetFontAndSize(bfR, fontSize1);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, ".......................................................................................................................................", 50, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ลงชื่อ ", 270, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "...................................................................................", 290, linenumber - 5, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "แพทย์ผู้ตรวจร่างกาย", 480, linenumber, 0);
                //canvas.SetFontAndSize(bfRB, fontSize2);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, txtDoctorName.Text, 213, linenumber, 0);

                canvas.SetFontAndSize(bfR, fontSize0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "หมายเหตุ (1) ต้องเป็นแพทย์ซึ่งได้ขึ้นทะเบียนรับใบอนุญาตประกอบวิชาชีพเวชกรรม ", 50, linenumber -= 10, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(2) ให้แสดงว่าเป็นผู้มีร่างกายสมบูรณ์เพียงใด ใบรับรองแพทย์ฉบับนี้ให้ใช้ได้ 1 เดือน นับแต่วันที่ตรวจร่างกาย ", 72, linenumber -= 10, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(3) คำรับรองนี้เป็นการตรวจวินิจฉัยเบื้องต้น และใบรับรองแพทย์นี้ ใช้สำหรับใบอนุญาตขับรถและปฎิบัติหน้าที่เป็นผู้ประจำรถ", 72, linenumber -= 10, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "แบบฟอร์มนี้ได้รับการรับรองจากมติคณะกรรมการแพทยสภาในการประชุมครั้งที่ 2/2564 วันที่ 4 กุมภาพันธ์ 2564 ", 50, linenumber -= 10, 0);

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
                ProcessStartInfo s = new ProcessStartInfo(Environment.CurrentDirectory + "\\" + txtCheckUPHN.Text.Trim() + "_driver.pdf");
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
        private void gen7ThaiPDF()
        {
            System.Drawing.Font font = new System.Drawing.Font("Microsoft Sans Serif", 12);
            iTextSharp.text.pdf.BaseFont bfR, bfR1, bfRB;
            iTextSharp.text.BaseColor clrBlack = new iTextSharp.text.BaseColor(0, 0, 0);
            //MemoryStream ms = new MemoryStream();
            string myFont = Environment.CurrentDirectory + "\\THSarabunNew.ttf";
            string myFontB = Environment.CurrentDirectory + "\\THSarabunNew Bold.ttf";
            String hn = "", name = "", doctor = "", fncd = "", birthday = "", dsDate = "", dsTime = "", an = "";

            decimal total = 0;

            bfR = BaseFont.CreateFont(myFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            bfR1 = BaseFont.CreateFont(myFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            bfRB = BaseFont.CreateFont(myFontB, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfR, 12, iTextSharp.text.Font.NORMAL, clrBlack);

            var logo = iTextSharp.text.Image.GetInstance(Environment.CurrentDirectory + "\\LOGO-BW-tran.jpg");
            logo.SetAbsolutePosition(10, PageSize.A4.Height - 90);
            logo.ScaleAbsoluteHeight(70);
            logo.ScaleAbsoluteWidth(70);
            int leftp = 0;
            FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");

            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 36, 36, 36, 36);
            try
            {

                FileStream output = new FileStream(Environment.CurrentDirectory + "\\" + txtCheckUPHN.Text.Trim() + "_7Thai.pdf", FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(doc, output);
                doc.Open();

                doc.Add(logo);

                //doc.Add(new Paragraph("Hello World", fntHead));

                Chunk c;
                String foobar = "";

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
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "MEDICAL CERTIFICATE", PageSize.A4.Width / 2, linenumber, 0);
                canvas.ShowTextAligned(Element.ALIGN_CENTER, "วันที่ตรวจ  " + txtCheckUPDate.Value.Day.ToString() + " " + bc.getMonth(txtCheckUPDate.Value.Month.ToString("00")) + " พ.ศ. " + (txtCheckUPDate.Value.Year + 543), (PageSize.A4.Width / 2) + 200, linenumber -= 20, 0);
                canvas.EndText();
                linenumber = 680;
                canvas.BeginText();

                canvas.SetFontAndSize(bfRB, fontSize2);

                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ข้าพเจ้า  ", 60, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPDoctorName.Text.Trim(), 107, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "....................................................................................................................................................................................................... ", 105, linenumber - 3, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "แพทย์ปริญญา ใบอนุญาตประกอบวิชาชีพเวชกรรมเลขที่  ", 60, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPDoctorId.Text.Trim(), 282, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................ ", 280, linenumber - 3, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ได้ทำการตรวจร่าางกาย ", 60, linenumber, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "................................................................................................................................................................................. ", 155, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, cboCheckUPPrefixT.Text + " " + txtCheckUPNameT.Text.Trim() + " " + txtCheckUPSurNameT.Text.Trim(), 157, linenumber + 3, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ปรากฏว่า ไม่เป็นผู้ทุพพลภาพ ไร้ความสามารถ จิตฟั่นเฟือน ไม่สมประกอบ ", 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfR, fontSize1);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "และปราศจากโรคเหล่านี้ ", 60, linenumber -= 20, 0);
                leftp = 60;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "1. โรคเรื้อนในระยะติดต่อหรือในระยะที่ปรากฏอาการเป็นที่รังเกียจแก่สังคม (Leprosy)", leftp, linenumber -= 20, 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "2. วัณโรคปอดในระยะติดต่อ (Active pulmonary tuberculosis)", leftp, linenumber -= 20, 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "3. โรคติดยาเสพติดให้โทษ (Drug addiction)", leftp, linenumber -= 20, 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "4. โรคพิษสุราเรื้อรัง (Chronic alcoholism)", leftp, linenumber -= 20, 0);

                canvas.ShowTextAligned(Element.ALIGN_LEFT, "5. โรคเท้าช้างในระยะที่ปรากฏอาการที่เป็นที่รังเกียจแก่สังคม (Filariasis)", leftp, linenumber -= 20, 0);
                if (chkCheckup62.Checked)
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "6. ซิฟิลิสในระยะที่ 2 (Secondary Syphilis)", leftp, linenumber -= 20, 0);
                else if (chkCheckup63.Checked)
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "6. ซิฟิลิสในระยะที่ 3 (Latent Syphilis)", leftp, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "7. โรคจิตฟั่นเฟือนหรือปัญญาอ่อน (Schizophrenia or Mental Retardation)", leftp, linenumber -= 20, 0);

                linenumber -= 20;
                canvas.SetFontAndSize(bfRB, fontSize2);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, txtPatientName.Text.Trim(), 153, linenumber + 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, label99.Text.Replace(":", ""), 60, linenumber -= 20, 0);
                canvas.SetFontAndSize(bfRB, fontSize2);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "................................................................................................................................................................................................... ", 90 - 3, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, cboCheckUPThaiDiag.Text.Trim(), 90, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผลการตรวจหาการติดเชื้อAnti HIV ", 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, ".................................................................................................................................................... ", 200, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPThaiHIV.Text.Trim(), 203, linenumber + 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "บันทึกสัญญาณชีพ ", 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................................................................................................................................................... ", 145, linenumber - 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPThaiSign1.Text.Trim(), 148, linenumber + 2, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "........................................................................................................................................................................... ", 145, linenumber -= 23, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPThaiSign2.Text.Trim(), 148, linenumber + 6, 0);
                //linenumber = 580;

                linenumber -= 20;

                canvas.EndText();

                canvas.BeginText();
                linenumber -= 20;
                linenumber -= 20;
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผู้เข้ารับการตรวจ ...................................................  ", 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "แพทย์ผู้ตรวจ ...................................................", (PageSize.A4.Width / 2) - 60, linenumber, 0);
                //canvas.ShowTextAligned(Element.ALIGN_LEFT, "(.....................................................)", 120, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "(........................................................................)", (PageSize.A4.Width / 2) - 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, txtCheckUPDoctorName.Text.Trim(), (PageSize.A4.Width / 2) - 50, linenumber + 3, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "วันที่ ..............................................................", (PageSize.A4.Width / 2) - 60, linenumber -= 20, 0);
                linenumber -= 20;
                linenumber -= 20;
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "มีอายุการใช้งาน 3 เดือน(VALID FOR THREE MONTHS)", 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ผ่านการรับรองมาตรฐาน ISO 9001:2000 ทุกหน่วยงาน", 60, linenumber -= 20, 0);
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "FM-NUR-001/3 (แก้ไขครั้งที่ 00 15/02/53)", 60, linenumber -= 20, 0);

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
                ProcessStartInfo s = new ProcessStartInfo(Environment.CurrentDirectory + "\\" + txtCheckUPHN.Text.Trim() + "_7Thai.pdf");
                //s.Arguments = "/c dir *.cs";
                p.StartInfo = s;

                p.Start();
            }
        }
        private void Document_PrintPageAppointment(object sender, PrintPageEventArgs e)
        {
            float yPos = 10, col1 = 0, col2 = 50, col21 = 180, col3 = 250, col4 = 450, col41 = 560, col5 = 300, line = 25;
            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            StringFormat flagsR = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            flags.Alignment = StringAlignment.Near;
            flagsR.Alignment = StringAlignment.Far;
            Size textSize = TextRenderer.MeasureText("", fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            float centerpage = e.PageSettings.PaperSize.Width / 2;
            //yPos = yPos + line;//ขึ้นบันทัดใหม่        ชื่อโรงพยาบาล ต่ำไป
            col2 = 20;
            textSize = TextRenderer.MeasureText(bc.iniC.hostname, famt2, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(bc.iniC.hostname, famt2B, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText(bc.iniC.hostnamee, famt2, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(bc.iniC.hostnamee, famt2B, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            e.Graphics.DrawString("print date " + DateTime.Now, fEditS, Brushes.Black, col41 + 220, yPos, flagsR);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText("ใบนัดพบแพทย์ Appointment Note", famt2B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString("ใบนัดพบแพทย์ Appointment Note", famt4B, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            e.Graphics.DrawString("เลขที่: " + APM.MNC_DOC_YR.Substring(APM.MNC_DOC_YR.Length - 2) + "-" + APM.MNC_DOC_NO, famt2, Brushes.Black, col41 + 220, yPos, flagsR);
            //e.Graphics.DrawString(APM.MNC_DOC_YR.Substring(APM.MNC_DOC_YR.Length-2) + "-"+APM.MNC_DOC_NO, famt5, Brushes.Black, col41+120, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("HN:", famt2B, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(HN, famt2B, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("แผนกที่นัด:", famt2B, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(bc.bcDB.pm32DB.getDeptNameOPD(APM.MNC_SEC_NO), famt2B, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Name/ชื่อผู้ป่วย:", famt2B, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(VS.PatientName, famt2B, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("วันที่พิมพ์:", famt2B, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(DateTime.Now.ToString(), famt2B, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Age/อายุ:", famt2B, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(PTT.AgeStringTHlong(), famt2B, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("สิทธิ์การรักษา:", famt2B, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(VS.PaidName, famt2B, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Date/นัดมาวันที่:", famt2B, Brushes.Black, col2, yPos, flags);
            if (APM.nationcode.Equals("01") || APM.nationcode.Equals("TH"))
            {
                e.Graphics.DrawString(bc.datetoShowTHMMM(APM.MNC_APP_DAT) + " " + APM.apm_time, famt2B, Brushes.Black, col21, yPos, flags);
            }
            else
            {
                e.Graphics.DrawString(bc.datetoShowEN(APM.MNC_APP_DAT) + " " + APM.apm_time, famt2B, Brushes.Black, col21, yPos, flags);
            }
            e.Graphics.DrawLine(blackPen, col21, yPos + line, col21 + 160, yPos + line);
            //e.Graphics.DrawString(bc.datetoShow(APM.MNC_APP_DAT) +" "+APM.apm_time, famt5, Brushes.Black, col21, yPos, flags);
            //e.Graphics.DrawString("Dept/นัดตรวจที่แผนก:", famt5B, Brushes.Black, col4, yPos, flags);
            //e.Graphics.DrawString(bc.bcDB.pm32DB.getDeptNameOPD(APM.MNC_SECR_NO), famt5, Brushes.Black, col41+60, yPos, flags);
            e.Graphics.DrawString("สิ่งที่ต้องเตรียม:", famt2B, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(APM.MNC_REM_MEMO, famt2, Brushes.Black, col41, yPos, flags);
            float ypos1 = yPos;
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Doctor/แพทย์:", famt2B, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(APM.doctor_name, famt2B, Brushes.Black, col21, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            //e.Graphics.DrawString("สิ่งที่ต้องเตรียม:", famt5B, Brushes.Black, col2, yPos, flags);
            //e.Graphics.DrawString(APM.MNC_REM_MEMO, famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("Dept/นัดตรวจที่แผนก:", famt2B, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(bc.bcDB.pm32DB.getDeptNameOPD(APM.MNC_SECR_NO), famt2B, Brushes.Black, col21, yPos, flags);
            //e.Graphics.DrawString("รายการตรวจ: ", famt2B, Brushes.Black, col4, yPos, flags);
            String txt = "";
            if (grfApmOrder.Rows.Count > 1)
            {
                foreach (Row rowa in grfApmOrder.Rows)
                {
                    String name = "", code = "";
                    name = rowa[colgrfOrderName].ToString();
                    code = rowa[colgrfOrderCode].ToString();
                    if (name.Equals("name")) continue;
                    txt += code + " " + name + "\r\n";
                }
                if (txt.Length > 1)
                {
                    txt = txt.Substring(0, txt.Length - 1);
                }
                e.Graphics.DrawString(txt, famt1, Brushes.Black, col41, ypos1, flags);
            }

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("เพื่อ:", famt2B, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(APM.MNC_APP_DSC, famt2B, Brushes.Black, col21, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            //Staff stf = new Staff();
            //stf = bc.bcDB.stfDB.selectByPasswordConfirm1("1618");
            //bc.cStf = stf;

            e.Graphics.DrawString("เบอร์โทรติดต่อ:", famt2, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(bc.iniC.deptphone, famt2, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("ผู้บันทึก:", famt2, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawLine(blackPen, col4 + 60, yPos + 22, col4 + 270, yPos + 22);
            if (bc.cStf != null) { e.Graphics.DrawString(bc.cStf.fullname, famt2, Brushes.Black, col4 + 60, yPos - 3, flags); }

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("หมายเหตุ:", famt2, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString("เพื่อประโยชน์และความสะดวกของท่าน  กรุณามาให้ตรงตามวัน และเวลาที่แพทย์นัดทุกครั้ง", famt1, Brushes.Black, col21, yPos, flags);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("กรณีที่ไม่สามารถมาตรวจตามนัดได้  กรุณาโทรเพื่อแจ้งยกเลิกหรือเลื่อนนัดกับทางโรงพยาบาลทุกครั้ง", famt1, Brushes.Black, col21, yPos, flags);
            g.Dispose();
            Brush.Dispose();
            blackPen.Dispose();
        }
        private void Document_PrintPageQueDtr(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "", err = "";
            Decimal amt1 = 0, voucamt = 0, discount = 0, total = 0, cash = 0;
            float yPos = 0, gap = 6, colName = 0, col2 = 5, col3 = 250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 820, col40 = 620;
            int count = 0, recx = 15, recy = 15, col2int = 0, yPosint = 0, col40int = 0;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            try
            {
                QUENO = bc.bcDB.vsDB.selectVisitQUE(VS.HN, DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd"), VS.preno);
                date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                Pen blackPen = new Pen(Color.Black, 1);
                Size proposedSize = new Size(100, 100);
                err = "01";
                StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
                Size textSize = TextRenderer.MeasureText(line, fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
                StringFormat sfR2L = new StringFormat();
                sfR2L.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                Int32 xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
                Int32 yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
                float marginR = e.MarginBounds.Right;
                float avg = marginR / 2;
                System.Drawing.Rectangle rec = new System.Drawing.Rectangle(0, 0, 20, 20);
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
                err = "02";
                e.Graphics.DrawString(QUEDEPT, fque, Brushes.Black, 0, yPos, flags);
                e.Graphics.DrawString(QUENO, ftotal, Brushes.Black, 180, yPos, flags);

                e.Graphics.DrawString("H.N. " + PTT.Hn, fqueB, Brushes.Black, 0, yPos + 25, flags);        //bc.pdfFontSize + 7        pdfFontName     FontStyle.Bold

                e.Graphics.DrawString(PTT.Name, fque, Brushes.Black, 0, yPos + 50, flags);
                err = "03";
                e.Graphics.DrawString("อายุ " + PTT.AgeStringOK1DOT() + " [" + PTT.patient_birthday + "]", fque, Brushes.Black, 0, yPos + 70, flags);
                e.Graphics.DrawString(VS.symptom, fque, Brushes.Black, 0, yPos + 95, flags);
                e.Graphics.DrawString(lbOperDtrName.Text.Trim(), fqueB, Brushes.Black, 0, yPos + 130, flags);

                err = "04";

                e.Graphics.DrawString("V/S _______", fEdit, Brushes.Black, 5, yPos + 180, flags);
                e.Graphics.DrawString("T _______'C   BP______mmHg", fEdit, Brushes.Black, 5, yPos + 200, flags);
                e.Graphics.DrawString("P______/mm   R______/mm", fEdit, Brushes.Black, 5, yPos + 220, flags);
                e.Graphics.DrawString("BW _______kgs.   HT______cms", fEdit, Brushes.Black, 5, yPos + 240, flags);
                err = "05";
                e.Graphics.DrawString("ประวัติการแพ้ยา", fque, Brushes.Black, 5, yPos + 260, flags);
                //สำคัญ ถ้าพิมพ์ ครั้งแรกจะโหลดข้อมูล ถ้าพิมพ์ ต่อจาก processอื่น พยายาม ไม่ดึง เพื่อความเร็ว และลด network
                if (DTCHRONIC == null) { DTCHRONIC = bc.bcDB.vsDB.SelectChronicByPID(PTT.idcard); }
                if (DTALLERGY == null) { DTALLERGY = bc.bcDB.vsDB.selectDrugAllergy(txtOperHN.Text.Trim()); }
                err = "051";
                if (DTCHRONIC.Rows.Count > 0)
                {
                    string txtchronic = ""; int cnt = 0; yPos += 280;
                    err = "052";
                    foreach (DataRow row in DTCHRONIC.Rows)
                    {
                        txtchronic = "";
                        if (cnt == 0) { txtchronic = "โรคประจำตัว " + row["CHRONICCODE"].ToString() + " " + row["MNC_CRO_DESC"].ToString(); }
                        else { txtchronic = row["CHRONICCODE"].ToString() + " " + row["MNC_CRO_DESC"].ToString(); yPos += 15; }
                        e.Graphics.DrawString(txtchronic, fque, Brushes.Black, 5, yPos, flags);
                        cnt++;
                    }
                }
                else
                {
                    err = "0521";
                    e.Graphics.DrawString("โรคประจำตัว _____________________", fEdit2, Brushes.Black, 5, yPos += 280, flags);
                }
                e.Graphics.DrawString("ขอใบรับรองแพทย์", fEdit, Brushes.Black, 5, yPos += 25, flags);
                e.Graphics.DrawString("___ไม่ขอ  ___ประกอบเบิก", fEdit, Brushes.Black, 5, yPos += 25, flags);
                e.Graphics.DrawString("___หยุดงาน  ___รับการตรวจจริง", fEdit, Brushes.Black, 5, yPos += 25, flags);
                err = "06";
                line = "print time  " + date;
                //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.Left);
                //e.Graphics.DrawString(line, famtB, Brushes.Black, 15, yPos + 185, flags);
                e.Graphics.DrawString(line, fque, Brushes.Black, 10, yPos += 25, flags);
            }
            catch (Exception ex)
            {
                lfSbStatus.Text = err + " Document_PrintPageQueDtr";
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", this.Name + " Document_PrintPageQueDtr " + err + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "Document_PrintPageQueDtr", err + ex.Message);
            }
        }
        private void Document_PrintPageStaffNote(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "", prndob = "";
            Decimal amt1 = 0, voucamt = 0, discount = 0, total = 0, cash = 0;
            float yPos = 10, yAdj = 0, gap = 6, colName = 0, col2 = 5, col3 = 250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 820, col40 = 620;
            int count = 0, recx = 15, recy = 15, col2int = 0, yPosint = 0, col40int = 0;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            prndob = "อายุ " + PTT.AgeStringShort1();

            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);

            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            Size textSize = TextRenderer.MeasureText(line, fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            sfR2L.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            Int32 xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            Int32 yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            float marginR = e.MarginBounds.Right;
            float avg = marginR / 2;
            System.Drawing.Rectangle rec = new System.Drawing.Rectangle(0, 0, 20, 20);
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            //new LogWriter("e", "FrmOPD Document_PrintPageStaffNote 00 ");
            col2 = 65; col3 = 320; col4 = 880; col40 = 650;
            yPos = float.Parse(bc.iniC.printYPOS);
            yAdj = float.Parse(bc.iniC.printadjustY);
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            //recx = 25;
            VS = bc.bcDB.vsDB.selectbyPreno(HN, VSDATE, PRENO);
            if (bc.iniC.branchId.Equals("001-1")) line = bc.iniC.hostname; else line = "5";
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fPrn, Brushes.Black, xOffset, yPos, new StringFormat());leftMargin
            if (bc.iniC.branchId.Equals("001-1"))
            {
                e.Graphics.DrawString(line, famt7B, Brushes.Black, col2, yPos - yAdj, flags);
                e.Graphics.DrawString(line, famt7B, Brushes.Black, col40, yPos - yAdj, flags);
            }
            else if (bc.iniC.branchId.Equals("001"))
            {
                line = "1";
                e.Graphics.DrawString(line, famt7B, Brushes.Black, col2, yPos - yAdj, flags);
                e.Graphics.DrawString(line, famt7B, Brushes.Black, col40, yPos - yAdj, flags);
            }
            else
            {
                e.Graphics.DrawString(line, famt7B, Brushes.Black, col3 - 15, yPos - yAdj + 15, flags);
                e.Graphics.DrawString(line, famt7B, Brushes.Black, col4, yPos - yAdj + 15, flags);
            }
            line = "H.N. " + PTT.MNC_HN_NO + "     " + VS.VN;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col3 + 25, yPos, flags);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col4 + 30, yPos, flags);

            line = "ชื่อ " + PTT.Name;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col3, yPos + 20, flags);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col4 + 15, yPos + 20, flags);
            line = "เลขที่บัตร " + PTT.MNC_ID_NO;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col3, yPos + 40, flags);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col4 + 15, yPos + 40, flags);
            String paid = VS.PaidName;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(paid, fStaffN, Brushes.Black, col2, yPos + 40, flags);
            e.Graphics.DrawString(paid, fStaffN, Brushes.Black, col40, yPos + 40, flags);
            String compname = "", allergy1 = "", allergy2 = "", chronic = "";
            compname = VS.CompName.Length > 48 ? VS.CompName.Substring(0, 48) + Environment.NewLine + VS.CompName.Substring(49) : VS.CompName;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(compname, fStaffN, Brushes.Black, col40, yPos + 60, flags);

            //dtallergy = bc.bcDB.vsDB.selectDrugAllergy(txtOperHN.Text.Trim());
            //dtchronic = bc.bcDB.vsDB.SelectChronicByPID(PTT.idcard);
            //สำคัญ ถ้าพิมพ์ ครั้งแรกจะโหลดข้อมูล ถ้าพิมพ์ ต่อจาก processอื่น พยายาม ไม่ดึง เพื่อความเร็ว และลด network
            if (DTCHRONIC == null) { DTCHRONIC = bc.bcDB.vsDB.SelectChronicByPID(PTT.idcard); }
            if (DTALLERGY == null) { DTALLERGY = bc.bcDB.vsDB.selectDrugAllergy(txtOperHN.Text.Trim()); }
            int i = 0;
            foreach (DataRow row in DTALLERGY.Rows)
            {
                allergy1 += row["MNC_ph_tn"].ToString() + " " + row["MNC_ph_memo"].ToString() + ", ";
                i++;
                if (i == 3) break;
            }
            i = 0;
            foreach (DataRow row in DTCHRONIC.Rows)
            {
                chronic += row["CHRONICCODE"].ToString() + " " + row["MNC_CRO_DESC"].ToString() + ",";
                i++;
                if (i == 3) break;
            }

            if (DTCHRONIC.Rows.Count > 0)
            {
                string txtchronic = ""; int cnt = 0; float yPos1 = yPos + 60;
                foreach (DataRow row in DTCHRONIC.Rows)
                {
                    txtchronic = "";
                    if (cnt == 0) { txtchronic = "โรคประจำตัว " + row["CHRONICCODE"].ToString() + " " + row["MNC_CRO_DESC"].ToString(); }
                    else { txtchronic = row["CHRONICCODE"].ToString() + " " + row["MNC_CRO_DESC"].ToString(); yPos1 += 13; }
                    e.Graphics.DrawString(txtchronic, fStaffN, Brushes.Black, col2, yPos1, flags);
                    cnt++;
                }
                //chronic = chronic.Substring(0, chronic.Length - 1);
                //e.Graphics.DrawString("โรคประจำตัว " + chronic.Replace(",",Environment.NewLine), fStaffN, Brushes.Black, col2, yPos + 60, flags);
                //rec = new System.Drawing.Rectangle(col2int + 82, 75, recx, recy);
                //e.Graphics.DrawRectangle(blackPen, rec);
            }
            else
            {
                e.Graphics.DrawString("โรคประจำตัว", fStaffN, Brushes.Black, col2, yPos + 60, flags);
                e.Graphics.DrawString("[ ]ยังไม่พบ", fStaffN, Brushes.Black, col2 + 70, yPos + 60, flags);
                e.Graphics.DrawString("โรคเรื้อรัง ไม่มีข้อมูล โรคเรื้อรัง", fStaffN, Brushes.Black, col2, yPos + 100, flags);
                e.Graphics.DrawString("[ ]มีโรค ระบุ", fStaffN, Brushes.Black, col2 + 70, yPos + 80, flags);
                //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(col2int + 67 - recx, 99, recx, recy));
            }
            if (DTALLERGY.Rows.Count > 0)
            {
                e.Graphics.DrawString("แพ้ยา/อาหาร/อื่นๆ  " + allergy1.Replace(",", Environment.NewLine), fStaffN, Brushes.Black, col2, yPos + 180, flags);
                e.Graphics.DrawString("แพ้ยา/อาหาร/อื่นๆ  " + allergy1.Replace(",", Environment.NewLine), fStaffN, Brushes.Black, col40, yPos + 180, flags);
            }
            else
            {
                allergy1 = "แพ้ยา/อาหาร/อื่นๆ ไม่มีข้อมูล การแพ้ยา ";
                e.Graphics.DrawString(allergy1, fStaffN, Brushes.Black, col2, yPos + 180, flags);
                e.Graphics.DrawString(allergy1, fStaffN, Brushes.Black, col40, yPos + 180, flags);
                //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(col2int + 207, yPosint + 183, recx, recy));
                //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(col40int + 213, yPosint + 183, recx, recy));
                line = "[ ]ไม่มี";
                e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2int + 208 + recx, yPos + 180, flags);
                e.Graphics.DrawString(line, fStaffN, Brushes.Black, col40 + 213 + recx, yPos + 180, flags);
                line = "[ ] มี ระบุอาการ";
                //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 211 + recx, yPos + 200, flags);
                //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(col2int + 207, yPosint + 203, recx, recy));
                e.Graphics.DrawString(line, fStaffN, Brushes.Black, col40 + 213 + recx, yPos + 200, flags);
                //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(col40int + 211, yPosint + 203, recx, recy));
            }
            //line = "แพ้ยา/อาหาร/อื่นๆ";
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);

            //if (allergy1.Length > 0)
            //{
            //    //e.Graphics.DrawString("/", fStaffN, Brushes.Black, col2int + 69 - recx, 99, flags);
            //}

            line = prndob;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col3, yPos + 60, flags);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col4 + 15, yPos + 60, flags);
            line = "DR Time.                  ปิดใบยา";
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col3 + 80, yPos + 60, flags);

            //date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            date = bc.datetoShow1(VS.VisitDate) + " " + bc.showTime(VS.VisitTime);
            line = "วันที่เวลา " + date;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col3, yPos + 80, flags);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col4 + 15, yPos + 80, flags);
            if (VS.DoctorId.Equals("00000")) { line = "ชื่อแพทย์ " + VS.DoctorId + " แพทย์ไม่ระบุชื่อ"; }
            else { line = "ชื่อแพทย์ " + VS.DoctorId + " " + VS.DoctorName; }
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col3, yPos + 100, flags);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col4 - 50, yPos + 120, flags);

            line = "อาการเบื้องต้น " + VS.symptom.Replace("\r\n", "");
            if (VS.symptom.Length > 60) { line = VS.symptom.Substring(0, 60); line = line.Replace("\r\n", ","); e.Graphics.DrawString(line, fStaffNs, Brushes.Black, col2, yPos + 120, flags); e.Graphics.DrawString(line, fStaffNs, Brushes.Black, col40, yPos + 100, flags); }
            else { e.Graphics.DrawString(line.Replace("เบื้องต้น", ""), fStaffN, Brushes.Black, col2, yPos + 120, flags); e.Graphics.DrawString(line, fStaffN, Brushes.Black, col40, yPos + 100, flags); }
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);fStaffNs

            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString("Temp " + VS.temp, fStaffN, Brushes.Black, col2, yPos + 140, flags);

            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString("H.Rate " + VS.ratios, fStaffN, Brushes.Black, col2 + 80, yPos + 140, flags);
            line = "R.Rate " + VS.breath;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 160, yPos + 140, flags);
            line = "BP1 " + VS.bp1l;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 240, yPos + 140, flags);
            line = "Time ";
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 320, yPos + 140, flags);
            line = "BP2 " + VS.bp1r;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 380, yPos + 140, flags);
            line = "Time ";
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 460, yPos + 140, flags);

            line = "Wt. " + VS.weight;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2, yPos + 160, flags);
            line = "Ht. " + VS.high;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 80, yPos + 160, flags);
            line = "BMI. ";
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 160, yPos + 160, flags);
            line = "CC. " + VS.cc;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 220, yPos + 160, flags);
            line = "CC.IN " + VS.ccin;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 290, yPos + 160, flags);
            line = "CC.EX " + VS.ccex;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 360, yPos + 160, flags);
            line = "Ab.C " + VS.abc;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 430, yPos + 160, flags);
            line = "H.C. " + VS.hc;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 500, yPos + 160, flags);

            line = "Precaution (Med) _________________________________________ ";
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fStaffN, Brushes.Black, col40 , yPos + 250, flags);
            //new LogWriter("e", "FrmOPD Document_PrintPageStaffNote 01 ");

            //line = "อาการเบื้อต้น  "+ txtSymptom.Text;
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 10, yPos + 220, flags);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 10, yPos + 220, flags);

            line = bc.bcDB.pm32DB.getDeptNameOPD(VS.DeptCode);
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col40 + 40, yPos + 260, flags);

            e.Graphics.DrawString("[  ]Medication                       [  ]No Medication", fStaffN, Brushes.Black, col40 + 50, yPos + 290, flags);
            e.Graphics.DrawString("Pain score " + txtOperPain.Text.Trim() + " O₂" + txtOperO2.Text.Trim(), fStaffN, Brushes.Black, col2 + 360, yPos + 250, flags);

            if (TEMPLATESTAFFNOTE.Equals("0") || TEMPCANCER)
            {
                //e.Graphics.DrawString("อาการ " + VS.symptom, fStaffN, Brushes.Black, col3 + 40, yPos + 315, flags);
                //e.Graphics.DrawString("อาการ " + VS.symptom, fStaffN, Brushes.Black, col4 + 20, yPos + 350, flags);
                if (TEMPLATESTAFFNOTE.Equals("1"))
                {
                    //AC Doxorubicin-Cyclophosphamide
                    e.Graphics.DrawString("Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 4 mg in 0.9% NaCl 10 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- Ativan  ( 0.5) 1 tab", fStaffN, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("Chemotherapy Order:", fStaffNB, Brushes.Black, col40, yPos + 480, flags);
                    e.Graphics.DrawString("-  Doxorubicin ......mg (60 mg/m2) in 0.9%NaCl 100 ml free flow", fStaffN, Brushes.Black, col40 + 10, yPos + 500, flags);
                    e.Graphics.DrawString("(Vesicant agent:extraprecaution for extravasation)", fStaffNB, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Cyclophosphamide ......mg (600mg/m2) in 0.9%NaCl100 mliv in 30 min", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("Home Medication", fStaffNB, Brushes.Black, col40, yPos + 560, flags);
                    e.Graphics.DrawString("-Lorazepam 1 mg po hs prn for insomnia", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("-Metoclopamide 1 tab potid ac", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("-Onsea (8 mg) 1 tab po bid am on day", fStaffN, Brushes.Black, col40 + 10, yPos + 620, flags);
                    e.Graphics.DrawString("-Dexa (4 mg) 1*2 pc on day", fStaffN, Brushes.Black, col40 + 10, yPos + 640, flags);
                    e.Graphics.DrawString("-Senokot 2 tabs pohsprn for constipation", fStaffN, Brushes.Black, col40 + 10, yPos + 660, flags);
                    e.Graphics.DrawString("-NSS 1 litre for กลั้วคอ ..................", fStaffN, Brushes.Black, col40 + 10, yPos + 680, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("2"))
                {
                    //Bleomycin
                    e.Graphics.DrawString("Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Metoclopamide 10 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- Bleomycin30 mg in0.9%NaCl 100 ml iv free flow", fStaffN, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("Home Medication", fStaffNB, Brushes.Black, col40, yPos + 480, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5mg po hs prn for insomnia *10tab", fStaffN, Brushes.Black, col40 + 10, yPos + 500, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab potid ac *10 tab", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- paracetamol 1 tab po prn q 6 hr for fever", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Others", fStaffN, Brushes.Black, col40, yPos + 560, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("3"))
                {
                    //Carboplatin
                    e.Graphics.DrawString("Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 20mg in 0.9% NaCl 10 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- Benadryl (25 mg) 1 tab po", fStaffN, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Chlorpheniramine 10 mg iv", fStaffN, Brushes.Black, col40, yPos + 480, flags);
                    e.Graphics.DrawString("Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 500, flags);
                    e.Graphics.DrawString("- Carboplatin  ..... mg (AUC5 or 7) in D5W 250ml iv in 1 hr", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("Monitor infusional Hypersensitivity reaction", fStaffN, Brushes.Black, col40 + 20, yPos + 540, flags);
                    e.Graphics.DrawString("Home Medication", fStaffNB, Brushes.Black, col40, yPos + 560, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5 mg po hs prn for insomnia..*10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab potid ac *15tab...", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("- Dexamethasone(4) 1*2 pc/6", fStaffN, Brushes.Black, col40 + 10, yPos + 620, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs poprn for constipation *20tab", fStaffN, Brushes.Black, col40 + 10, yPos + 640, flags);
                    e.Graphics.DrawString("- Onsea (8 mg) 1 tab poam on day2-4 *3tab", fStaffN, Brushes.Black, col40 + 10, yPos + 660, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("4"))
                {
                    //Carboplatin-Gemcitabine
                    e.Graphics.DrawString("Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 10 mg in 0.9%NaCl 10 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("Chemotherapy Order:", fStaffNB, Brushes.Black, col40, yPos + 460, flags);
                    e.Graphics.DrawString("- Carboplatin (AUC5) .............mg in 5%DW 100 ml iv in 1 hr", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("- Gemcitabine ....mg(1000-1250mg/m²) in 0.9%NaCl 100 ml iv in 30 min", fStaffN, Brushes.Black, col40 + 10, yPos + 500, flags);
                    e.Graphics.DrawString("Home Medication", fStaffNB, Brushes.Black, col40, yPos + 520, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5 mg po hs prn for insomnia..*10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac *15tab...", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Dexamethasone(0.5) 4*2pc day 2-4 *24tab", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs po  prn for constipation *20tab", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("- onsia (8) 1x1 ac d2-4 * 3tabs", fStaffN, Brushes.Black, col40 + 10, yPos + 620, flags);
                    e.Graphics.DrawString("- Others", fStaffN, Brushes.Black, col40 + 10, yPos + 640, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("5"))
                {
                    //Cisplatin -Gemcitabine
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 10 mg in 0.9%NaCl 10 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("🔲 0.9%NaCl 500 ml iv in 2 h before chemotherapy", fStaffN, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40, yPos + 470, flags);
                    e.Graphics.DrawString("- Cisplatin .... mg (25 mg/ m²) in NSS 100mlivin30 min day1, 8", fStaffN, Brushes.Black, col40 + 10, yPos + 500, flags);
                    e.Graphics.DrawString("- Gemcitabine .... mg(1000-1250mg/m²) in 0.9%NaCl 100 ml iv in 30 min day1,8", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 540, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5 mg po hs prn for insomnia..*10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac *15tab...", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Dexamethasone(0.5) 4*2pc day 2-4 *24tab", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs po  prn for constipation *20tab", fStaffN, Brushes.Black, col40 + 10, yPos + 620, flags);
                    e.Graphics.DrawString("- onsia (8) 1x1 ac d2-4 * 3tabs", fStaffN, Brushes.Black, col40 + 10, yPos + 640, flags);
                    e.Graphics.DrawString("- Others", fStaffN, Brushes.Black, col40 + 10, yPos + 660, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("6"))
                {
                    //Cisplatin
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsia 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 10 mg in 0.9%NaCl 10 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Cisplatin .... mg (50 mg/m²) in NSS 100mlivin30 min day1, 8 ,15", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 500, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5 mg po hs prn for insomnia..*10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac *15tab...", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Dexamethasone(4) 1*2 pc/6", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs po  prn for constipation *20tab", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Onsea (8 mg) 1 tab poam on day2-4 *3tab", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("7"))//
                {
                    //CMF
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 20 mg in 0.9%NaCl 20 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- 5FU ..... mg (600 mg/ m²) in 0.9%NaCl 100 ml iv in 10 min", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("- Methotrexate .... mg (40 mg/m²) in 0.9%NaCl 100 ml iv in 10 min", fStaffN, Brushes.Black, col40 + 10, yPos + 500, flags);
                    e.Graphics.DrawString("- Cyclophosphamide  .... mg (600mg/m² )0.9%NaCl 100 ml iv in 15 min", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 540, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5 mg po hs prn for insomnia..*10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac *15tab...", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Dexamethasone(0.5) 4*2pc day 2-4 *24tab", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs po  prn for constipation *20tab", fStaffN, Brushes.Black, col40 + 10, yPos + 620, flags);
                    e.Graphics.DrawString("- onsia (8) 1 tab po bid  ac day2-4 * 6tabs", fStaffN, Brushes.Black, col40 + 10, yPos + 620, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("8"))
                {
                    //CMV
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 4 mg in 0.9%NaCl 20 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- 5FU ..... mg (600 mg/ m²) in 0.9%NaCl 100 ml iv in 30 min", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("- Methotrexate .... mg (40 mg/m²) in 0.9%NaCl 100 ml iv in 30 min", fStaffN, Brushes.Black, col40 + 10, yPos + 500, flags);
                    e.Graphics.DrawString("- Cyclophosphamide(50 mg/ tab) .....................................................", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 540, flags);
                    e.Graphics.DrawString("- Lorazepam 1 mg po hs prn for insomnia", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab potid ac", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs pohsprn for constipation", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("9"))
                {
                    //Gemcitabine
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 20 mg in 0.9% NaCl 10 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- Benadryl (25 mg) 1 tab po", fStaffN, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Chlorpheniramine 10 mg iv", fStaffN, Brushes.Black, col40, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40, yPos + 480, flags);
                    e.Graphics.DrawString("- Docetaxel …….. mg (75 or 100 mg/m²) in 0.9%NaCl 250 ml iv in 1 hr", fStaffN, Brushes.Black, col40 + 10, yPos + 500, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 540, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5  mg po hs prn for insomnia *10 tabs", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac ..…  *15 tabs", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("-  Dexamethasone(0.5) 4*2pc day 2-4   *24tabs", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs pohs prn for constipation....   *20tabs", fStaffN, Brushes.Black, col40 + 10, yPos + 620, flags);
                    e.Graphics.DrawString("- Tramol 50 mg/cap 1 cap oral prn q6hr   * 20 tab", fStaffN, Brushes.Black, col40 + 10, yPos + 620, flags);
                    e.Graphics.DrawString("- Others", fStaffN, Brushes.Black, col40 + 10, yPos + 620, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("10"))
                {
                    //Docetaxel
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Gemcitabine .... mg(1000-1250mg/m²) in 0.9%NaCl 100 ml iv in 30 min", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("- Capecitabine ..................", fStaffN, Brushes.Black, col40 + 10, yPos + 500, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 520, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5 mg po hs prn for insomnia..*10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac ", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs po  prn for constipation *20tab", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("- Plasil 1×3 po ac.*20.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Immodium1 cap po prn*10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- ORS จิบ ", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- 20 % Urea cream apply b.i.d", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("11"))
                {
                    //Herceptin
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Chlorpheniramine 10 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- MaintenanceTrastuzumab ……..mg (6mg/kg/d) +NSS 250 ml iv in 60 min", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 500, flags);
                    e.Graphics.DrawString("- onsia 1 tab oral od ac", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Folic 1 tab pood pc", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Lorazepam 1 mg po hs prn for insomnia", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab potid ac", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Tramadol 1 cap po prn q 6 hr for pain", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs pohsprn for constipation", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Others  Tamoxifen (20) 1*1 OD", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("12"))
                {
                    //Herceptin2
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Chlorpheniramine 10 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- MaintenanceTrastuzumab .... mg (6mg/kg/d) +NSS 250 ml iv in 60 min", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 500, flags);
                    e.Graphics.DrawString("- onsia 1 tab oral od ac", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Folic 1 tab pood pc", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Lorazepam 1 mg po hs prn for insomnia", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab potid ac", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Tramadol 1 cap po prn q 6 hr for pain", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs pohsprn for constipation", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Others", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("13"))
                {
                    //ID
                    e.Graphics.DrawString("Next OPD .....................        [ ] Lab        [ ]  No Lab", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("          [ ] CBC        [ ] BUN        [ ] Cr        [ ] E’lyt        [ ] ALP               [ ] SGOT           [ ] SGPT", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("          [ ] FBS        [ ] Lipid      [ ] PO4       [ ] UA           [ ] Urine Phosphorus  [ ] Urine Cr ", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("          [ ] CD4        [ ] VL         [ ] VDRL      [ ] HbsAg        [ ] Anti- HCV         [ ] CXR", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("14"))
                {
                    //Infliximab
                    e.Graphics.DrawString("ให้ยาครั้งที่....................", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("[ ] Para( 500)", fStaffN, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("[ ] CPM 10 ml iv", fStaffN, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("..............................", fStaffN, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("-  Infliximab..........mg  in 0.9%NaCl 250  ml iv", fStaffN, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("20 ml/hr.       ปรับทุก 15 min", fStaffN, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("40 ml/hr.", fStaffN, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("80 ml/hr.", fStaffN, Brushes.Black, col40, yPos + 400, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("15"))
                {
                    //Irinotecan
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 10 mg in 0.9%NaCl 10 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Irinotecan .... mg (180 mg/m²) in 5%DW 500ml iv in 90 min", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 500, flags);
                    e.Graphics.DrawString("- onsia 1 tab oral od ac", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Folic 1 tab pood pc", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5 mg po hs prn for insomnia..*10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Tramadol 1 cap po prn q 6 hr for pain", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs po  prn for constipation", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Others", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("16"))
                {
                    //Mayo’s 5FU-low dose LV
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv for D1-5", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Leucovorin ...................... mg (20 mg/m²) iv push (พร้อม premed)", fStaffN, Brushes.Black, col40 + 10, yPos + 500, flags);
                    e.Graphics.DrawString("  5FU ...... mg (375 or 400 or 425 mg/m2) in 0.9%NaCl 100 ml iv in 10 min", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("  Repeat for total 0 4 or 0 5 days from ........... to .............", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 520, flags);
                    e.Graphics.DrawString("[ ] Lorazepam 0.5 mg po hs prn for insomnia..*10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("[ ] Metoclopamide 1 tab po tid ac ..........*15tab", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("[ ] NSS บ้วนปาก 1 ", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("[ ] ORS จิบ *", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("17"))
                {
                    //Paclitaxel
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 4 mg in 0.9%NaCl 10 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- Benadryl (25 mg) 1 tab po", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- Chlorpheniramine 10 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Paclitaxel....... mg (175 mg/m2) in 0.9%NaCl 250 ml iv in 1 hr", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("Record vital sign q 15 min x 4 after starting paclitaxel", fStaffN, Brushes.Black, col40 + 20, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 500, flags);
                    e.Graphics.DrawString("- Lorazepam 1 mg po hs prn for insomnia", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac - Others", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Tramadol1 cap po prn q 6 hr for pain", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs pohsprn for constipation", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- onsea (8 mg) 1x2 ac d2-4", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("- Others ", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("17"))
                {
                    //Paclitaxel-Carboplatin
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 20 mg in 0.9%NaCl 10 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- Benadryl (25 mg) 1 tab po", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- Chlorpheniramine 10 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Paclitaxel....... mg (175 mg/m²) in 0.9%NaCl 500 ml iv in 4 hr", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("Record vital sign q 15 min x 4 after starting Paclitaxel", fStaffN, Brushes.Black, col40 + 20, yPos + 480, flags);
                    e.Graphics.DrawString("- Carboplatin ....... mg (AUC 5-6) in D5W 500 ml iv in 1 hr", fStaffN, Brushes.Black, col40 + 10, yPos + 500, flags);
                    e.Graphics.DrawString("Monitor infusional Hypersensitivity reaction", fStaffN, Brushes.Black, col40 + 20, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 520, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5 mg po hs prn for insomnia .... *10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac * 15tab ....", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Dexamethasone(0.5) 4*2pc day 2-4 *24tab", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs po  prn for constipation *20tab", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("- Tramol(50)1cap oral prn pain q 4 hr *20 tabs", fStaffN, Brushes.Black, col40 + 10, yPos + 620, flags);
                    e.Graphics.DrawString("- Onsea or Zofran (8 mg) 1 tab bid on d2-4 *6tab", fStaffN, Brushes.Black, col40 + 10, yPos + 640, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("18"))
                {
                    //Single Agent Doxorubicin
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 20 mg in 0.9%NaCl 20 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Doxorubicin .... mg (60-75 mg/m²) in 0.9%NaCl 100 ml iv in 15 min", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("(Vesicant agent:extraprecaution for extravasation)", fStaffN, Brushes.Black, col40 + 20, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 500, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5 mg po hs prn for insomnia..*10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac -15tab ....", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Dexamethasone(0.5) 4*2pc day 2-4 *24tab", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs po  prn for constipation *20tab", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- onsia (8) 1x1 ac d2-4 * 3tabs", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("- Others", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("19"))
                {
                    //Single agent Gemcitabine
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Gemcitabine .... mg (1000-1250 mg/m²) in 0.9%NaCl 100 ml iv in 30 min", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 500, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5 mg po hs prn for insomnia..*10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs po  prn for constipation *20tab", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Plasil 1×3 po ac.*20.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Immodium1 cap po prn*10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("- ORS จิบ", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("- 20 % Urea cream apply b.i.d", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("20"))
                {
                    //TC
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 20 mg in 0.9%NaCl 10 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- Benadryl (25 mg) 1 tab po", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Chlorpheniramine 10 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Docetaxel .... mg (75 or 100 mg/m²) in 0.9%NaCl 250 ml iv in 1 hr", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("Record vital sign q 15 min x 4 after starting Paclitaxel", fStaffN, Brushes.Black, col40 + 20, yPos + 480, flags);
                    e.Graphics.DrawString("- Cyclophosphamide(600 mg/m²) in 0.9%NaCl 100 ml iv in 30 min", fStaffN, Brushes.Black, col40 + 10, yPos + 500, flags);
                    e.Graphics.DrawString("Monitor infusional Hypersensitivity reaction", fStaffN, Brushes.Black, col40 + 20, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 520, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5 mg po hs prn for insomnia*10.tab", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac ....  *15 tabs", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Dexamethasone(0.5) 4*2pc day 2-4   *24tabs", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs pohs prn for constipation....   *20tabs", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("- Tramol 50 mg/cap 1 cap oral prn q6hr   * 20 tab", fStaffN, Brushes.Black, col40 + 10, yPos + 620, flags);
                    e.Graphics.DrawString("- Others", fStaffN, Brushes.Black, col40 + 10, yPos + 640, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("21"))
                {
                    //Vinorebine
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 4mg in 0.9% NaCl 10 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Benadryl (25 mg) 1 tab po", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Vinorebine .... mg (25 mg/m²) in 0.9%NaCl 100 cc iv in 10 mins", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 500, flags);
                    e.Graphics.DrawString("- Lorazepam 1 mg po hs prn for insomnia", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs po  prn for constipation", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Onsia(8) 1*1 ac D2-4 * 3 tab", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("22"))
                {
                    //Weekly Paclitaxel
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 4 mg in 0.9%NaCl 10 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- Benadryl (25 mg) 1 tab po", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- Chlorpheniramine 10 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Paclitaxel....... mg (80 mg/m²) in 0.9%NaCl 250 ml iv in 1 hr", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("Record vital sign q 15 min x 4 after starting paclitaxel", fStaffN, Brushes.Black, col40 + 20, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 500, flags);
                    e.Graphics.DrawString("- Folic 1 tab pood pc", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Lorazepam 1 mg po hs prn for insomnia", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac - Others", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- Dexamethasone(0.5) 4*2pc day", fStaffN, Brushes.Black, col40 + 10, yPos + 580, flags);
                    e.Graphics.DrawString("- Tramadol1 cap po prn q 6 hr for pain", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs pohsprn for constipation", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("23"))
                {
                    //WeeklyPaclitaxel-Herceptin 
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 4 mg in 0.9%NaCl 10 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- Benadryl (25 mg) 1 tab po", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- Chlorpheniramine 10 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 460, flags);
                    e.Graphics.DrawString("- Taxol/Anzatax/ Intaxel/ Paclitaxel.... mg (80 mg/m2) in 0.9%NaCl 250 cc iv in 1hr", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("Record vital sign q 15 min x 4 after starting paclitaxel", fStaffN, Brushes.Black, col40 + 20, yPos + 480, flags);
                    e.Graphics.DrawString("Maintenance Trastuzumab....mg (6mg/kg/d) +NSS 100 ml iv in 60 min ", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 520, flags);
                    e.Graphics.DrawString("- Lorazepam 1 mg po hs prn for insomnia", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Tramadol1 cap po prn q 6 hr for pain", fStaffN, Brushes.Black, col40 + 10, yPos + 600, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs pohsprn for constipation", fStaffN, Brushes.Black, col40 + 10, yPos + 620, flags);
                }
                else if (TEMPLATESTAFFNOTE.Equals("24"))
                {
                    //XELOXC 
                    e.Graphics.DrawString("⬛ Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("- Dexamethasone 20 mg in 0.9%NaCl 20 ml iv", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("- CPM 1 amp IV", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("⬛ Chemotherapy Order:", fStaffNB, Brushes.Black, col40 + 10, yPos + 400, flags);
                    e.Graphics.DrawString("- Oxaliplatin .... mg (130 mg/m2) in D5W 500 ml iv in 2 hr.", fStaffN, Brushes.Black, col40 + 10, yPos + 420, flags);
                    e.Graphics.DrawString("(flush iv line with D5W before oxaliplatin Infusion)", fStaffN, Brushes.Black, col40 + 20, yPos + 480, flags);
                    e.Graphics.DrawString("Capecitabine .... mg ................", fStaffN, Brushes.Black, col40 + 10, yPos + 440, flags);
                    e.Graphics.DrawString("⬛ Home Medication", fStaffNB, Brushes.Black, col40, yPos + 460, flags);
                    e.Graphics.DrawString("- Lorazepam 0.5 mg po hs prn for insomnia #", fStaffN, Brushes.Black, col40 + 10, yPos + 480, flags);
                    e.Graphics.DrawString("- Metoclopamide 1 tab po tid ac  #", fStaffN, Brushes.Black, col40 + 10, yPos + 500, flags);
                    e.Graphics.DrawString("- Dexamethasone(4) 1*2 am bid day 2-4 *", fStaffN, Brushes.Black, col40 + 10, yPos + 520, flags);
                    e.Graphics.DrawString("- Senokot 2 tabs po  prn for constipation *", fStaffN, Brushes.Black, col40 + 10, yPos + 540, flags);
                    e.Graphics.DrawString("- onsia (8) 1 tab po bid on day 2-4 * ", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- Immodium1 cap po prn*", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- ORS จิบ", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                    e.Graphics.DrawString("- 20 % Urea cream apply b.i.d", fStaffN, Brushes.Black, col40 + 10, yPos + 560, flags);
                }
            }
            else if (TEMPLATESTAFFNOTE.Equals("checkup_doe"))
            {
                if (grfChkPackItems.Rows.Count > 0)
                {
                    float lineY = (yPos + 350);
                    if (cboCheckUPSelect.SelectedItem != null)
                    {
                        if (chkCheckUPSelect.Checked && ((ComboBoxItem)cboCheckUPSelect.SelectedItem).Value.ToString().Equals("doeonline"))
                        {
                            String txt1 = ((ComboBoxItem)cboCheckUPOrder.SelectedItem).Value.ToString();
                            String txt2 = cboCheckUPOrder.Text;
                            e.Graphics.DrawString(txt1 + " " + txt2, fStaffN, Brushes.Black, col40int + 20, lineY, flags);
                        }
                        foreach (Row row in grfChkPackItems.Rows)
                        {
                            if (row[colChkPackItemsname] == null) continue;
                            if (row[colChkPackItemsname].ToString().Length <= 0) continue;
                            if (row[colChkPackItemsname].ToString().Equals("name")) continue;
                            line = row[colChkPackItemsname].ToString();
                            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
                            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 20, lineY, flags);
                            lineY += 27;
                        }
                    }
                }
            }
            //ตามล่าม  พูดไทย X-ray Lab
            e.Graphics.DrawString("🔲ตามล่าม      🔲พูดไทยได้     🔲X-ray      🔲Lab        🔲EKG        🔲DTX", fStaffN, Brushes.Black, col40, yPos + 275, flags);
            //e.Graphics.DrawString("พูดไทยได้", fStaffNB, Brushes.Black, col40 + 40, yPos + 250, flags);
            //e.Graphics.DrawString("X-ray", fStaffNB, Brushes.Black, col40 + 100, yPos + 250, flags);
            //e.Graphics.DrawString("Lab", fStaffNB, Brushes.Black, col40 + 150, yPos + 250, flags);
            //e.Graphics.DrawString("EKG", fStaffNB, Brushes.Black, col40 + 200, yPos + 250, flags);
            //e.Graphics.DrawString("DTX", fStaffNB, Brushes.Black, col40 + 250, yPos + 250, flags);
            //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(col40int - 20, 250, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(col40int + 20, yPosint + 250, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(col40int + 80, yPosint + 250, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(col40int + 130, yPosint + 250, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(col40int + 180, yPosint + 250, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(col40int + 230, yPosint + 250, recx, recy));

            line = "ใบรับรองแพทย์             🔲ไม่มี      🔲มี          Consult      🔲ไม่มี      🔲มี __________________";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2, yPos + 640, flags);
            //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(183, yPosint + 640, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(235, yPosint + 640, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(357, yPosint + 640, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new System.Drawing.Rectangle(405, yPosint + 640, recx, recy));
            line = "ชื่อผู้รับ _____________________________";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2, yPos + 6650, flags);
            line = "Health Education :";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 5, yPos + 730, flags);
            line = "ลงชื่อพยาบาล: _____________________________________";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2 + 5, yPos + 750, flags);
            line = "FM-REC-002 (00 10/09/53)(1/1)";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            System.Drawing.Font font1 = new System.Drawing.Font(fStaffN.Name, fStaffN.Size - 2, FontStyle.Regular);
            e.Graphics.DrawString(line, font1, Brushes.Black, col2, yPos + 770, flags);
            e.Graphics.DrawString(line, font1, Brushes.Black, col40, yPos + 770, flags);
            g.Dispose();
            Brush.Dispose();
            blackPen.Dispose();
        }
        private void Document_PrintPage_StaffNote_Reserve(object sender, PrintPageEventArgs e)
        {
            String[] txtL = txtStaffNoteL.Text.Trim().Split('\n');
            String[] txtR = txtStaffNoteR.Text.Trim().Split('\n');
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);
            int line = 0, y = 0, i = 1, gapline = 32;
            foreach (String txt1 in txtL)
            {
                y = 350 + line;
                if (i == 1) y = 350;
                else if (i == 2) y = 379;
                else if (i == 3) y = 408;
                else if (i == 4) y = 437;
                else if (i == 5) y = 467; else if (i == 6) y = 497; else if (i == 7) y = 520; else if (i == 8) y = 550;
                e.Graphics.DrawString(txt1.Replace("\r", ""), famt7, Brushes.Black, 50, y, flags);
                line += gapline; i++;
            }
            line = 0; i = 1;
            foreach (String txt1 in txtR)
            {
                y = 270 + line;
                if (i == 1) y = 269;
                else if (i == 2) y = 297;
                else if (i == 3) y = 325;
                else if (i == 4) y = 350;
                else if (i == 5) y = 380;
                else if (i == 6) y = 410;
                else if (i == 7) y = 438;
                else if (i == 8) y = 467;
                else if (i == 9) y = 495;
                else if (i == 10) y = 523;
                else if (i == 11) y = 550;
                else if (i == 12) y = 580; else if (i == 13) y = 608; else if (i == 14) y = 637; else if (i == 15) y = 667; else if (i == 16) y = 696; else if (i == 17) y = 725;
                e.Graphics.DrawString(txt1.Replace("\r", ""), famt7, Brushes.Black, 620, y, flags);
                line += gapline; i++;
            }
        }
        private void genCheckUPAlienPDF()
        {
            //new LogWriter("d", "FrmOPD genCheckUPAlienPDF ");
            String filename = "";
            int gapLine = 14, linenumber = 5, gapX = 40, gapY = 20, xCol1 = 20, xCol2 = 40, xCol3 = 100, xCol4 = 200, xCol5 = 250, xCol6 = 300, xCol7 = 450, xCol8 = 510;
            String certid = "";
            certid = chkCheckUPEditCert.Checked ? insertCertDoctorCheckUp("edit") : insertCertDoctorCheckUp("");
            //new LogWriter("d", "FrmOPD genCheckUPAlienPDF 00");
            bc.bcDB.vsDB.updateMedicalCertId(txtCheckUPHN.Text.Trim(), PRENO, VSDATE, certid);
            if (txtCheckUPDoctorId.Text.Trim().Equals("24738"))
            {
                bc.bcDB.tokenDB.updateUsed(txtCheckUPDoctorId.Text.Trim(), "cert_alien", certid);       // ทำเพื่อ จะได้ มีระบบ ลายเซ็นแพทย์ เป็นแบบรูปภาพ แล้วมีการเก็บ ลง ใน ระบบ
            }
            if (certid.Length > 3) {                /*certid = certid.Replace("555", "");*/                certid = certid.Substring(3, 7); }
            if (certid.Length < 3) { MessageBox.Show("ไม่พบ เลขที่ กรุณาตรวจสอบ hn visitdate preno ไม่ถูกต้อง", ""); return; }    //ต้องมี เพราะมี case ที่ไม่มี certid แต่มีการเรียกใช้งาน
            lfSbStatus.Text = certid;
            String patheName = Environment.CurrentDirectory + "\\cert_med\\";
            if (!Directory.Exists(patheName)) { Directory.CreateDirectory(patheName); }
            C1PdfDocument pdf = new C1PdfDocument();
            StringFormat _sfRight, _sfCenter, _sfLeft;
            _sfRight = new StringFormat();
            _sfCenter = new StringFormat();
            _sfLeft = new StringFormat();
            _sfRight.Alignment = StringAlignment.Far;
            _sfCenter.Alignment = StringAlignment.Center;
            _sfLeft.Alignment = StringAlignment.Near;
            pdf.FontType = FontTypeEnum.Embedded;
            //new LogWriter("d", "FrmOPD genCheckUPAlienPDF 01");
            filename = certid + "_" + txtCheckUPHN.Text.Trim() + "_alien_" + DateTime.Now.Year.ToString() + ".pdf";
            try
            {
                qrcode.CodeType = C1.BarCode.CodeType.QRCode;
                qrcode.Text = txtCheckUPHN.Text.Trim() + " " + cboCheckUPPrefixT.Text + " " + txtCheckUPNameT.Text.Trim() + " " + txtCheckUPSurNameT.Text.Trim() + " " + DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year).ToString() + " " + certid;
                qrcode.AutoSize = false;
                qrcode.Width = 60;
                qrcode.Height = 60;
                System.Drawing.Image imgqrcode = qrcode.Image;
                // Replace the line causing the error with the correct namespace
                System.Drawing.Image loadedImagelogo = Resources.LOGO_BW_tran;
                float newWidth = loadedImagelogo.Width * 100 / loadedImagelogo.HorizontalResolution, newHeight = loadedImagelogo.Height * 100 / loadedImagelogo.VerticalResolution;
                float widthFactor = 4.8F, heightFactor = 4.8F;
                if (widthFactor > 1 | heightFactor > 1)
                {
                    if (widthFactor > heightFactor)
                    {
                        widthFactor = 1;
                        newWidth = newWidth / widthFactor;
                        newHeight = newHeight / widthFactor;
                        //newWidth = newWidth / 1.2;
                        //newHeight = newHeight / 1.2;
                    }
                    else
                    {
                        newWidth = newWidth / heightFactor;
                        newHeight = newHeight / heightFactor;
                    }
                }

                RectangleF recflogo = new RectangleF((PageSize.A4.Width / 2) - 38, 5, (int)newWidth, (int)newHeight);
                pdf.DrawImage(loadedImagelogo, recflogo);
                linenumber += gapLine; linenumber += gapLine; linenumber += gapLine; linenumber += gapLine;
                pdf.DrawString("ใบรับรองแพทย์", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 38, linenumber += (gapLine - 5)), _sfLeft);
                pdf.DrawString("เลขที่ " + certid, fPDFl2, Brushes.Black, new PointF(xCol2 + 520, 50), _sfRight);
                pdf.DrawString("ตรวจสุขภาพคนต่างด้าว/แรงงานต่างด้าว", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 78, linenumber += (gapLine)), _sfLeft);
                if (EDITDOE) pdf.DrawString("วันที่ตรวจ " + bc.datetoShow1(VSDATE), fPDF, Brushes.Black, new PointF(PageSize.A4.Width - 120, linenumber), _sfLeft);
                else pdf.DrawString("วันที่ตรวจ " + DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year).ToString(), fPDF, Brushes.Black, new PointF(PageSize.A4.Width - 120, linenumber), _sfLeft);
                pdf.DrawString("1. รายละเอียด ประวัติส่วนตัวของผู้รับการตรวจสุขภาพ", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString("1) ชื่อ-นามสกุล(นาย,นาง,นางสาว,เด็กชาย,เด็กหญิง) ....................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString("ชื่อ-นามสกุล(ภาษาอังกฤษ) ............................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(cboCheckUPPrefixT.Text + " " + txtCheckUPNameT.Text.Trim() + " " + txtCheckUPSurNameT.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 115, linenumber - 3), _sfLeft);
                pdf.DrawString("เลขประจำตัวบุคคล .............................................  เลขที่ Passport .............................................................. อาชีพ ....................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtCheckUPPttPID.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 85, linenumber - 3), _sfLeft);
                pdf.DrawString(txtCheckUPPassport.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 275, linenumber - 3), _sfLeft);
                pdf.DrawString(cboAlienPosition.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 420, linenumber - 4), _sfLeft);
                pdf.DrawString("วัน/เดือน/ปี เกิด .......................... เมืองที่เกิด ...................................... ประเทศ ...................................................  สัญชาติ ........................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtCheckUPDOB.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 75, linenumber - 3), _sfLeft);
                pdf.DrawString(cboCheckUPCountry.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 310, linenumber - 5), _sfLeft);
                pdf.DrawString(cboCheckUPNat.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 450, linenumber - 5), _sfLeft);

                //pdf.DrawString("2) ที่อยู่ปัจจุบัน อยู่บ้านเลขที่ ...................... หมู่ที่ ......... ตรอก .............. ซอย ...................... ถนน ............................ ตำบล/แขวง ...........................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);                
                //pdf.DrawString(txtCheckUPPttCurHomeNo.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 115, linenumber - 3), _sfLeft);
                //pdf.DrawString(txtCheckUPPttCurMoo.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 190, linenumber - 3), _sfLeft);
                //pdf.DrawString(txtCheckUPPttCurSoi.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 295, linenumber - 3), _sfLeft);
                //pdf.DrawString(txtCheckUPPttCurRoad.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 365, linenumber - 3), _sfLeft);
                //pdf.DrawString(txtCheckUPPttCurSearchTambon.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 485, linenumber - 3), _sfLeft);
                pdf.DrawString("2) ที่อยู่ปัจจุบัน ................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtCheckUPAddr3.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 85, linenumber - 3), _sfLeft);
                pdf.DrawString("..........................................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);

                //pdf.DrawString("อำเภอ/เขต ........................................ จังหวัด .......................................... รหัสไปรษญีย์ ............... โทร ...........................  มือถือ ..............................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                //pdf.DrawString(txtCheckUPPttCurAmp.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 55, linenumber - 3), _sfLeft);
                //pdf.DrawString(txtCheckUPPttCurChw.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 175, linenumber - 3), _sfLeft);
                //pdf.DrawString(txtCheckUPPttCurPostcode.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 325, linenumber - 3), _sfLeft);
                //pdf.DrawString(txtCheckUPMobile1.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 475, linenumber - 3), _sfLeft);
                pdf.DrawString("2. รายละเอียด ข้อมูลนายจ้าง/สถานประกอบการ", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                //pdf.DrawString("ชื่อ-นามสกุล(นายจ้าง) ............................................................................. สถานประกอบการ .........................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString("ชื่อ-นามสกุล(นายจ้าง)/สถานประกอบการ ............................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtCheckUPEmplyer.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 165, linenumber - 3), _sfLeft);
                //pdf.DrawString(txtCheckUPComp.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 345, linenumber - 3), _sfLeft);
                pdf.DrawString("ที่อยู่ ...................................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtCheckUPAddr1.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber - 5), _sfLeft);
                pdf.DrawString("...........................................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtCheckUPAddr2.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 15, linenumber - 3), _sfLeft);
                pdf.DrawString("3. ข้อมูลแพทย์ผู้ตรวจ", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString("นายแพทย์/แพทย์หญิง .....................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtCheckUPDoctorName.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 95, linenumber - 3), _sfLeft);
                pdf.DrawString("ใบอนุญาตประกอบวิชาชีพเวชกรรมเลขที่ .................................... สถานพยาบาล ..........................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtCheckUPDoctorId.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 165, linenumber - 3), _sfLeft);
                pdf.DrawString(bc.iniC.hostname, fPDF, Brushes.Black, new PointF(xCol2 + 305, linenumber - 3), _sfLeft);
                pdf.DrawString("ที่อยู่ ..................................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(bc.iniC.hostaddresst, fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber - 5), _sfLeft);
                //linenumber += gapLine;
                pdf.DrawString("ผลการตรวจสุขภาพ", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 30, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString("ส่วนสูง ................. ซ.ม. น้ำหนัก ............... ก.ก. สีผิว ........................... ความดันโลหิต ......................... มม.ปรอท ชีพจร .......................... ครั้ง/นาที", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtCheckUPHeight.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 40, linenumber - 3), _sfLeft);
                pdf.DrawString(txtCheckUPWeight.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 130, linenumber - 3), _sfLeft);
                pdf.DrawString(cboCheckUPskintone.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 200, linenumber - 5), _sfLeft);
                pdf.DrawString(txtCheckUPBp1L.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 325, linenumber - 3), _sfLeft);
                pdf.DrawString(txtCheckUPHrate.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 455, linenumber - 3), _sfLeft);

                pdf.DrawString("สภาพร่างกาย จิตใจทั่วไป ................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(cboCheckUPResult.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 105, linenumber - 3), _sfLeft);
                pdf.DrawString("ผลการตรวจวัณโรค                                         ปกติ [  ]             ผิดปกติ/ให้รักษา [  ]                       ระยะอันตราย [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                int xxx = 0;
                xxx = xCol2 + 708;
                if (chkAlienPulTuberNormal.Checked) xxx = xCol2 + 224;
                else if (chkAlienPulTuberAbNormal.Checked) xxx = xCol2 + 338;
                else if (chkAlienPulTuberDanger.Checked) xxx = xCol2 + 475;
                pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);
                pdf.DrawString("ผลการตรวจโรคเรื้อน                                       ปกติ [  ]             ผิดปกติ/ให้รักษา [  ]                       ระยะติดต่อ/อาการเป็นที่รังเกียจ [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                xxx = xCol2 + 708;
                if (chkAlienLeprosyNormal.Checked) xxx = xCol2 + 224;
                else if (chkAlienLeprosyAbNormal.Checked) xxx = xCol2 + 338;
                else if (chkAlienLeprosyDanger.Checked) xxx = xCol2 + 475;
                pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);
                pdf.DrawString("ผลการตรวจโรคเท้าช้าง                                    ปกติ [  ]             ผิดปกติ/ให้รักษา [  ]                       อาการเป็นที่รังเกียจ [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                xxx = xCol2 + 708;
                if (chkAlienFilariasisNormal.Checked) xxx = xCol2 + 224;
                else if (chkAlienFilariasisAbNormal.Checked) xxx = xCol2 + 338;
                else if (chkAlienFilariasisRepulsive.Checked) xxx = xCol2 + 475;
                pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);
                pdf.DrawString("ผลการตรวจโรคซิฟิลิส                                      ปกติ [  ]             ผิดปกติ/ให้รักษา [  ]                       ระยะที่3 [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                xxx = xCol2 + 708;
                if (chkAlienSyphilisNormal.Checked) xxx = xCol2 + 224;
                else if (chkAlienSyphilisAbNormal.Checked) xxx = xCol2 + 338;
                else if (chkAlienSyphilisStep3.Checked) xxx = xCol2 + 475;
                pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);
                pdf.DrawString("ผลการตรวจสารเสพติด                                     ปกติ [  ]             พบสารเสพติด   [  ]                       ให้ตรวจยืนยัน [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                xxx = xCol2 + 708;
                if (chkAlienDrugAddictionNormal.Checked) xxx = xCol2 + 226;
                else if (chkAlienDrugAddictionAbnormal.Checked) xxx = xCol2 + 338;
                else if (chkAlienDrugAddictionConfirm.Checked) xxx = xCol2 + 475;
                pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);
                pdf.DrawString("ผลการตรวจโรคอาการของโรคพิษสุราเรื้อรัง             ปกติ [  ]             ปรากฏอาการ   [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                xxx = xCol2 + 708;
                if (chkAlienChronicAlcoholismNormal.Checked) xxx = xCol2 + 227;
                else if (gbAlienChronicAlcoholismAppear.Checked) xxx = xCol2 + 338;
                pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);

                pdf.DrawString("ผลการตรวจการตั้งครรภ์                                    ไม่ตั้งครรภ์ [  ]                                      ตั้งครรภ์ [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                xxx = xCol2 + 708;

                if (chkAlienPregnantNo.Checked) xxx = xCol2 + 248;
                else if (chkAlienPregnantYes.Checked) xxx = xCol2 + 408;
                if (cboCheckUPSex.Text.Equals("F"))
                {
                    pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);
                }
                pdf.DrawString("ผลการตรวจอื่นๆ (ถ้ามี) ..................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtAlienOther.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 100, linenumber - 5), _sfLeft);
                //linenumber += gapLine;
                pdf.DrawString("สรุปผลตรวจ", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 30, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString("1. [  ] สุขภาพสมบูรณ์ดี", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                if (chkAlienGoodHealth.Checked) { xxx = xCol2 + 20; pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft); }
                pdf.DrawString("2. [  ] ผ่านการตรวจสุขภาพ แต่ต้องให้การรักษา ควบคุม ติดตามอย่างต่อเนื่อง", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                if (chkAlienConfirm.Checked) { xxx = xCol2 + 20; pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft); }
                pdf.DrawString("                     [  ] วัณโรค             [  ] โรคเรื้อน             [  ] โรคเท้าช้าง               [  ] โรคซิฟิลิส ", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                if (chkAlienConfirm.Checked && chkAlienConfirm1.Checked) { xxx = xCol2 + 72; pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft); }
                if (chkAlienConfirm.Checked && chkAlienConfirm2.Checked) { xxx = xCol2 + 152; pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft); }
                if (chkAlienConfirm.Checked && chkAlienConfirm3.Checked) { xxx = xCol2 + 238; pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft); }
                if (chkAlienConfirm.Checked && chkAlienConfirm4.Checked) { xxx = xCol2 + 338; pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft); }
                pdf.DrawString("3. [  ] ไม่ผ่านการตรวจสุขภาพเนื่องจาก ", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                if (chkAlienNoGoodHealth.Checked) { xxx = xCol2 + 20; pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft); }
                pdf.DrawString("3.1 ร่างกายทุพพลภาพจนไม่สามารถประกอบการหาเลี้ยงชีพได้/จิตฟั่นเฟือน ไม่สมประกอบ", fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber += (gapLine)), _sfLeft);
                pdf.DrawString("3.2 เป็นโรคไม่อนุญาตให้ทำงาน และไม่ให้การประกันสุขภาพ(ตามประกาศกระทรวงสาธารณสุขฯ)", fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber += (gapLine)), _sfLeft);
                int lineqrcode = linenumber;
                pdf.DrawString("แพทย์ผู้ตรวจ", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 100, linenumber += (gapLine + 5)), _sfLeft);
                linenumber += (gapLine + 15);
                int linesign = linenumber;
                pdf.DrawString("(.....................................................................................) ให้ประทับตรา", fPDF, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 100, linesign), _sfLeft);
                pdf.DrawString(txtCheckUPDoctorName.Text.Trim(), fPDF, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 90, linenumber - 3), _sfLeft);
                pdf.DrawString("(หมายเหตุ ใบรับรองแพทย์ฉบับนี้มีอายุ 90วัน นับตั้งแต่วันที่ตรวจร่างกาย ยกเว้น กรณีใช้สำหรับประกันสุขภาพมีอายุ 1 ปี)", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                RectangleF recfqrcode = new RectangleF(xCol2 + 465, lineqrcode - 10, imgqrcode.Width, imgqrcode.Height + 20);
                pdf.DrawImage(imgqrcode, recfqrcode);
                //ถ้าใน Folder sign มีรูป ให้แสดง ลงใน PDF
                //new LogWriter("d", "FrmOPD genCheckUPAlienPDF 02");
                String signFileName = Environment.CurrentDirectory + "\\sign\\sign_" + txtCheckUPDoctorId.Text.Trim() + ".jpg";
                if (File.Exists(signFileName))
                {
                    //new LogWriter("d", "FrmOPD genCheckUPAlienPDF 03");
                    System.Drawing.Image imgSign = null; imgSign = System.Drawing.Image.FromFile(signFileName);
                    float newWidthsign = imgSign.Width * 100 / imgSign.HorizontalResolution, newHeightsign = imgSign.Height * 100 / imgSign.VerticalResolution;
                    float widthFactorsign = 60.8F, heightFactorsign = 60.8F;
                    RectangleF recfSign = new RectangleF(xCol4 + 75, linesign - 32, 90, 30);
                    pdf.DrawImage(imgSign, recfSign);
                    //MemoryStream ms = new MemoryStream();                    pdf1.Save(ms);                    ms.Position = 0;
                    //if (txtCheckUPDoctorId.Text.Trim().Equals("24738"))
                    //{//online ให้ส่งไปที่ server FTP ถ้าเป็น offline ต้อง scan เลยไม่ต้อง
                    //    FtpClient ftpc = new FtpClient(bc.iniC.hostFTPCertMeddoe, bc.iniC.userFTPCertMeddoe, bc.iniC.passFTPCertMeddoe, false);
                    //    ftpc.upload(bc.iniC.folderFTPCertMeddoe + "/" + filename, ms);
                    //}
                    //new LogWriter("d", "FrmOPD genCheckUPAlienPDF 04");
                }
                //เอา logo ออก ตามคำสั่ง
                //RectangleF recflogo1 = new RectangleF(xCol4 + 235, linesign - 72, (int)newWidth, (int)newHeight);
                //pdf.DrawImage(loadedImagelogo, recflogo1);
                //new LogWriter("d", "FrmOPD genCheckUPAlienPDF 05 "+ patheName + filename);
                pdf.Save(patheName + filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                new LogWriter("e", "FrmOPD genCheckUPAlienPDF " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "genCheckUPAlienPDF ", ex.Message);
            }
            finally
            {
                pdf.Dispose();
                Process p = new Process();
                ProcessStartInfo s = new ProcessStartInfo(patheName + filename);
                //s.Arguments = "/c dir *.cs";
                p.StartInfo = s;

                p.Start();
            }
        }
        private void Document_PrintPage_tabStkSticker(object sender, PrintPageEventArgs e)
        {
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "";
            float yPos = 10, gap = 6, colName = 0, col2 = 5, col3 = 250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 820, col40 = 620;
            int count = 0, recx = 15, recy = 15, col2int = 0, yPosint = 0, col40int = 0;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);

            date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);

            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            Size textSize = TextRenderer.MeasureText(line, fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            sfR2L.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            Int32 xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            Int32 yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            float marginR = e.MarginBounds.Right;
            float avg = marginR / 2;
            System.Drawing.Rectangle rec = new System.Drawing.Rectangle(0, 0, 20, 20);
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            if (bc.iniC.windows.Equals("windowsxp"))
            {
                col2 = 65;
                col3 = 300;
                col4 = 870;
                col40 = 650;
                yPos = 15;
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
            }
            //Patient ptt = new Patient();

            line = "H.N. " + PTT.MNC_HN_NO + " " + VN;
            e.Graphics.DrawString(line, fPDF, Brushes.Black, 15, yPos + 5, flags);
            line = PTT.Name;
            e.Graphics.DrawString(line, fPDFs2, Brushes.Black, 15, yPos + 25, flags);
            //line = dt.Rows[0]["MNC_MD_DEP_DSC"].ToString() + " " + dt.Rows[0]["MNC_RM_NAM"].ToString() + " " + dt.Rows[0]["MNC_BD_NO"].ToString();//MNC_AN_NO
            line = PTT.AgeStringOK1();
            line = line.Replace("เดือน", "ด");
            line = line.Replace("วัน", "ว");
            e.Graphics.DrawString(line, fPDF, Brushes.Black, 15, yPos + 45, flags);
            line = STATIONNAME;
            e.Graphics.DrawString(line, fPDFs2, Brushes.Black, 115, yPos + 45, flags);

            line = "H.N. " + PTT.MNC_HN_NO + " " + VN;
            e.Graphics.DrawString(line, fPDF, Brushes.Black, 15, yPos + 65, flags);
            line = PTT.Name;
            e.Graphics.DrawString(line, fPDFs2, Brushes.Black, 15, yPos + 85, flags);
            //line = dt.Rows[0]["MNC_MD_DEP_DSC"].ToString() + " " + dt.Rows[0]["MNC_RM_NAM"].ToString() + " " + dt.Rows[0]["MNC_BD_NO"].ToString();
            line = PTT.AgeStringOK1();
            line = line.Replace("เดือน", "ด");
            line = line.Replace("วัน", "ว");
            e.Graphics.DrawString(line, fPDF, Brushes.Black, 15, yPos + 100, flags);

            line = STATIONNAME;
            e.Graphics.DrawString(line, fPDFs2, Brushes.Black, 115, yPos + 100, flags);

        }
        private void genCheckUPSSOPDF1()
        {
            String filename = "";
            int gapLine = 20, linenumber = 5, gapX = 40, gapY = 20, xCol1 = 20, xCol2 = 40, xCol3 = 100, xCol4 = 200, xCol5 = 250, xCol6 = 300, xCol7 = 450, xCol8 = 510;
            C1PdfDocument pdf = new C1PdfDocument();
            StringFormat _sfRight, _sfCenter, _sfLeft;
            _sfRight = new StringFormat();
            _sfCenter = new StringFormat();
            _sfLeft = new StringFormat();
            _sfRight.Alignment = StringAlignment.Far;
            _sfCenter.Alignment = StringAlignment.Center;
            _sfLeft.Alignment = StringAlignment.Near;
            pdf.FontType = FontTypeEnum.Embedded;
            filename = txtCheckUPHN.Text.Trim() + "_SSO_" + DateTime.Now.Year.ToString() + ".pdf";
            try
            {
                String certid = insertCertDoctorCheckUp("");        //สร้าง certid ใหม่
                bc.bcDB.vsDB.updateMedicalCertId(txtCheckUPHN.Text.Trim(), PRENO, VSDATE, certid);      //update certid ใน patient_t01
                qrcode.CodeType = C1.BarCode.CodeType.QRCode;
                qrcode.Text = txtCheckUPHN.Text.Trim() + " " + cboCheckUPPrefixT.Text + " " + txtCheckUPNameT.Text.Trim() + " " + txtCheckUPSurNameT.Text.Trim() + " " + DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year).ToString() + " " + certid;
                qrcode.AutoSize = false;
                qrcode.Width = 25;
                qrcode.Height = 25;
                System.Drawing.Image imgqrcode = qrcode.Image;

                System.Drawing.Image loadedImage = Resources.LOGO_BW_tran;
                float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution, newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;
                float widthFactor = 4.8F, heightFactor = 4.8F;
                {
                    if (widthFactor > heightFactor)
                    {
                        widthFactor = 1;
                        newWidth = newWidth / widthFactor;
                        newHeight = newHeight / widthFactor;
                        //newWidth = newWidth / 1.2;
                        //newHeight = newHeight / 1.2;
                    }
                    else
                    {
                        newWidth = newWidth / heightFactor;
                        newHeight = newHeight / heightFactor;
                    }
                }
                float pointcenter = (PageSize.A4.Width / 2);
                RectangleF recf = new RectangleF(55, 15, (int)newWidth, (int)newHeight);
                pdf.DrawImage(loadedImage, recf);
                pdf.DrawString("โรงพยาบาล บางนา5  55 หมู่4 ถนนเทพารักษ์ ตำบลบางพลีใหญ่ อำเภอบางพลี จังหวัด สมุทรปราการ 10540", fPDF, Brushes.Black, new PointF(recf.Width + 65, linenumber += gapLine), _sfLeft);
                pdf.DrawString("BANGNA 5 GENERAL HOSPITAL  55 M.4 Theparuk Road, Bangplee, Samutprakan Thailand", fPDF, Brushes.Black, new PointF(recf.Width + 65, linenumber += gapLine), _sfLeft);

                pdf.DrawString(" ใบรายงานผลตรวจสุขภาพ", fPDFl2, Brushes.Black, new PointF(pointcenter - 38, linenumber += (gapLine)), _sfLeft);
                pdf.DrawString("ชื่อ-นามสกุล/Name " + cboCheckUPPrefixT.Text + " " + txtCheckUPNameT.Text.Trim() + " " + txtCheckUPSurNameT.Text.Trim() + " เลขที่บัตรประชาชน " + PTT.MNC_ID_NO + " HN " + PTT.MNC_HN_NO, fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += gapLine), _sfLeft);
                pdf.DrawString("เลขที่ " + certid.Replace("555", ""), fPDF, Brushes.Black, new PointF(PageSize.A4.Width - 125, linenumber), _sfLeft);

                pdf.DrawString("ที่อยู่ " + txtCheckUPAddr1.Text.Trim() + " " + txtCheckUPPhone.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine)), _sfLeft);
                pdf.DrawString("วันที่ตรวจ " + DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year + 543).ToString(), fPDF, Brushes.Black, new PointF(PageSize.A4.Width - 125, linenumber), _sfLeft);
                pdf.DrawString("ข้อมูลสุขภาพ (Health Data)", fPDFl2, Brushes.Black, new PointF(pointcenter - 40, linenumber += (gapLine + 5)), _sfLeft);

                pdf.DrawRectangle(Pens.Black, xCol2, linenumber += (gapLine + 5), PageSize.A4.Width - xCol2 - 20, 510);
                pdf.DrawString("การตรวจร่างกายตามระบบ", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber), _sfLeft);
                pdf.DrawString("ผลตรวจ", fPDF, Brushes.Black, new PointF(xCol4 + 5, linenumber), _sfLeft);
                pdf.DrawString("ผลผิดปกติ", fPDF, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("การตรวจสารเคมีในเลือด", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber), _sfLeft);
                pdf.DrawString("ค่าที่ตรวจได้", fPDF, Brushes.Black, new PointF(xCol7 + 5, linenumber), _sfLeft);
                pdf.DrawString("ค่าปกติ", fPDF, Brushes.Black, new PointF(xCol8 + 12, linenumber), _sfLeft);

                pdf.DrawLine(Pens.Black, xCol4, linenumber, xCol4, 645);     //เส้นตั้ง col 1      cboCheckUPResChest
                pdf.DrawLine(Pens.Black, xCol5, linenumber, xCol5, 645);
                pdf.DrawLine(Pens.Black, xCol6, linenumber, xCol6, 665);
                pdf.DrawLine(Pens.Black, xCol7, linenumber, xCol7, 420);
                pdf.DrawLine(Pens.Black, xCol8, linenumber, xCol8, 420);

                pdf.DrawLine(Pens.Black, xCol2, linenumber + 30, 575, linenumber + 30);     //เส้นนอน header
                pdf.DrawLine(Pens.Black, xCol2, linenumber + 70, 575, linenumber + 70);     //เส้นนอน 1.
                pdf.DrawLine(Pens.Black, xCol2, linenumber + 110, 575, linenumber + 110);     //เส้นนอน 2.
                pdf.DrawLine(Pens.Black, xCol2, linenumber + 200, 575, linenumber + 200);   //เส้นนอน 3.
                pdf.DrawLine(Pens.Black, xCol2, linenumber + 230, xCol6, linenumber + 230);   //เส้นนอน 4.
                pdf.DrawLine(Pens.Black, xCol2, linenumber + 490, 575, linenumber + 490);   //เส้นนอน 5.

                linenumber += gapLine;
                pdf.DrawString("1. การคัดกรองการได้ยิน", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine - 11)), _sfLeft);
                pdf.DrawString(cboCheckUPResFRT.Text, fPDF, Brushes.Black, new PointF(cboCheckUPResFRT.Text.Equals("Normal") ? xCol4 + 5 : xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("6. การตรวจระดับน้ำตาลในเลือด", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber), _sfLeft);
                pdf.DrawString("    FBS", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber + 12), _sfLeft);
                pdf.DrawString(txtCheckUPResFBS.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 12, linenumber + 10), _sfLeft);
                pdf.DrawString(txtCheckUPResFBSStandard.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber + 12), _sfLeft);
                pdf.DrawString("   Finger Rub Test", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber + 12), _sfLeft);
                linenumber += (gapLine + 13);
                pdf.DrawString("2. การตรวจเต้านมโดยแพทย์หรือ", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine - 10)), _sfLeft);
                pdf.DrawString(cboCheckUPResChest.Text, fPDF, Brushes.Black, new PointF(cboCheckUPResChest.Text.Equals("Normal") ? xCol4 + 5 : xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("7. ตรวจหาเชื้อไวรัสตับอักเสบบี", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber), _sfLeft);
                pdf.DrawString("    HBsAg", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber + 12), _sfLeft);
                pdf.DrawString(txtCheckUPResHBsAg.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 12, linenumber + 10), _sfLeft);
                pdf.DrawString("   บุคลากรสาธารณสุข", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber + 12), _sfLeft);

                pdf.DrawString("3. การตรวจตาโดยความดูแลของจักษุแพทย์", fPDFs2, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 18)), _sfLeft);
                pdf.DrawString("VA", fPDF, Brushes.Black, new PointF(xCol4 + 15, linenumber), _sfLeft);
                pdf.DrawString("Ph", fPDF, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("8. ตรวจการทำงานของไต", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber), _sfLeft);

                pdf.DrawString("  และการตรวจSnellen eye Chart", fPDF, Brushes.Black, new PointF(xCol2 + 12, linenumber += (gapLine - 5)), _sfLeft);
                pdf.DrawString("R......./.......", fPDF, Brushes.Black, new PointF(xCol4 + 3, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResEyeVAR1.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber - 2), _sfLeft);
                pdf.DrawString(txtCheckUPResEyeVAR2.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10 + 20, linenumber - 2), _sfLeft);
                pdf.DrawString("L......./.......", fPDF, Brushes.Black, new PointF(xCol4 + 3, linenumber + 15), _sfLeft);
                pdf.DrawString(txtCheckUPResEyeVAL1.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 8, linenumber + 12), _sfLeft);
                pdf.DrawString(txtCheckUPResEyeVAL2.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 8 + 20, linenumber + 12), _sfLeft);
                pdf.DrawString("R...............", fPDF, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("  Serun Creatinine", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResCreatinine.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 12, cboCheckUPSex.Text.Equals("M") ? linenumber : linenumber + 15), _sfLeft);
                pdf.DrawString(cboCheckUPSex.Text.Equals("M") ? txtCheckUPResCreatinineStandard.Text.Length > 0 ? txtCheckUPResCreatinineStandard.Text.Trim() + " M" : "0.8-1.3 mg/dl M" : "0.8-1.3 mg/dl M", fPDFs2, Brushes.Black, new PointF(xCol8 + 2, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResEyePhR.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol5 + 12, linenumber - 2), _sfLeft);
                pdf.DrawString(cboCheckUPSex.Text.Equals("F") ? txtCheckUPResCreatinineStandard.Text.Length > 0 ? txtCheckUPResCreatinineStandard.Text.Trim() + " F" : "0.6-1.1 mg/dl F" : "0.6-1.1 mg/dl F", fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber + 15), _sfLeft);
                pdf.DrawString("L...............", fPDF, Brushes.Black, new PointF(xCol5 + 5, linenumber + 15), _sfLeft);
                pdf.DrawString(txtCheckUPResEyePhL.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol5 + 12, linenumber + 13), _sfLeft);

                linenumber += gapLine;
                pdf.DrawString("  การวัดความดันของเหลวภายในลูกตา", fPDF, Brushes.Black, new PointF(xCol2 + 12, linenumber += (gapLine - 5)), _sfLeft);
                pdf.DrawString("R...............", fPDF, Brushes.Black, new PointF(xCol4 + 3, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResEyeVAR.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber - 2), _sfLeft);
                pdf.DrawString("L...............", fPDF, Brushes.Black, new PointF(xCol4 + 3, linenumber + 15), _sfLeft);
                pdf.DrawString(txtCheckUPResEyeVAL.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber + 12), _sfLeft);
                pdf.DrawString(txtCheckUPResEye.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol5 + 10, linenumber + 12), _sfLeft);
                pdf.DrawString("eGFR Crez", fPDF, Brushes.Black, new PointF(xCol6 + 12, linenumber + 10), _sfLeft);
                pdf.DrawString(txtCheckUPReseGFR.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 12, linenumber + 10), _sfLeft);
                pdf.DrawString(txtCheckUPReseGFRStandard.Text.Trim(), fPDFs6, Brushes.Black, new PointF(xCol8 + 2, linenumber + 15), _sfLeft);

                linenumber += gapLine;
                pdf.DrawString("4. ความสมบรูณ์ของเม็ดเลือด CBC", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine)), _sfLeft);
                pdf.DrawString("ค่าที่ตรวจได้", fPDF, Brushes.Black, new PointF(xCol4 + 5, linenumber), _sfLeft);
                pdf.DrawString("ค่าปกติ", fPDF, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("9. การตรวจไขมันในเลือด", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber), _sfLeft);
                pdf.DrawString("Choleterol", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber += (gapLine - 5)), _sfLeft);
                pdf.DrawString(txtCheckUPResCholes.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 12, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResCholesStandard.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber), _sfLeft);
                linenumber += (gapLine - 5);
                pdf.DrawString("HDL Cholesterol", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber + 10), _sfLeft);
                pdf.DrawString(txtCheckUPResHDL.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 12, cboCheckUPSex.Text.Equals("M") ? linenumber : linenumber + 15), _sfLeft);
                //pdf.DrawString(cboCheckUPSex.Text.Equals("M") ? txtCheckUPResHDLStandard.Text.Length > 0 ? txtCheckUPResHDLStandard.Text.Trim()+"M" : "0.8-1.4 mg/dl M" : "0.8-1.4 mg/dl M", fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber), _sfLeft);
                //pdf.DrawString(cboCheckUPSex.Text.Equals("F") ? txtCheckUPResHDLStandard.Text.Length > 0 ? txtCheckUPResHDLStandard.Text.Trim()+" F": "0.6-1.1 mg/dl F" : "0.6-1.1 mg/dl F", fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber + 15), _sfLeft);
                pdf.DrawString("M > 35 ", fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber), _sfLeft);
                pdf.DrawString("F > 44", fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber + 15), _sfLeft);
                pdf.DrawString("  ภาวะโลหิตจาง     Hb", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResHB.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, cboCheckUPSex.Text.Equals("M") ? linenumber : linenumber + 15), _sfLeft);
                pdf.DrawString(cboCheckUPSex.Text.Equals("M") ? txtCheckUPResHBStandard.Text.Length > 0 ? txtCheckUPResHBStandard.Text.Trim() + " M" : "14.1-18.1g/dl M" : "14.1-18.1g/dl M", fPDFs6, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);

                linenumber += (gapLine);
                pdf.DrawString(cboCheckUPSex.Text.Equals("F") ? txtCheckUPResHBStandard.Text.Length > 0 ? txtCheckUPResHBStandard.Text.Trim() + " F" : "12.1-16.1g/df F" : "12.1-16.1g/df F", fPDFs6, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawLine(Pens.Black, xCol6, linenumber + 15, 575, linenumber + 15);

                linenumber += gapLine;
                pdf.DrawString("  ความเข้มข้นของเลือด     HCT", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber), _sfLeft);
                pdf.DrawString(cboCheckUPSex.Text.Equals("M") ? txtCheckUPResHCTStandard.Text.Length > 0 ? txtCheckUPResHCTStandard.Text.Trim() + " M" : " 41-51% M" : " 41-51% M", fPDFs2, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);

                pdf.DrawString(txtCheckUPResHCT.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, cboCheckUPSex.Text.Equals("M") ? linenumber : linenumber + gapLine), _sfLeft);
                pdf.DrawString("10. การตรวจปัสสาวะ Urine Analysis(UA)", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber), _sfLeft);
                pdf.DrawString(cboCheckUPSex.Text.Equals("F") ? txtCheckUPResHCTStandard.Text.Length > 0 ? txtCheckUPResHCTStandard.Text.Trim() + " F" : " 37-47% F" : " 37-47% F", fPDFs2, Brushes.Black, new PointF(xCol5 + 5, linenumber += gapLine), _sfLeft);
                pdf.DrawString("Color", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber), _sfLeft);
                pdf.DrawString(cboCheckUPResUAColor.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 10, linenumber), _sfLeft);

                pdf.DrawString("MCV", fPDF, Brushes.Black, new PointF(xCol2 + 105, linenumber += gapLine), _sfLeft);
                pdf.DrawString(txtCheckUPResMCV.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResMCVStandard.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("Appearance", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber), _sfLeft);
                pdf.DrawString(cboCheckUPResUAAppea.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 10, linenumber), _sfLeft);
                //pdf.DrawString("Pale Yellow", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber), _sfLeft);
                //pdf.DrawString("Yellow", fPDF, Brushes.Black, new PointF(xCol6 + 45, linenumber), _sfLeft);
                //pdf.DrawString("Amber", fPDF, Brushes.Black, new PointF(xCol6 + 65, linenumber), _sfLeft);

                pdf.DrawString("  จำนวนเม็ดเลือดขาว      WBC", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += gapLine), _sfLeft);
                pdf.DrawString(txtCheckUPResWBC.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResWBCStandard.Text.Trim(), fPDFs6, Brushes.Black, new PointF(xCol5 + 2, linenumber + 4), _sfLeft);
                pdf.DrawString("Protein (ng/dl)", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber), _sfLeft);
                pdf.DrawString(cboCheckUPResUAProtein.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 10, linenumber), _sfLeft);

                pdf.DrawString("Neutrophil", fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber += gapLine), _sfLeft);
                pdf.DrawString(txtCheckUPResNeu.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResNeuStandard.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("Glucose (mg/dl)", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber), _sfLeft);
                pdf.DrawString(cboCheckUPResUAGlucose.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 10, linenumber), _sfLeft);

                pdf.DrawString("Lymphocyte", fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber += gapLine), _sfLeft);
                pdf.DrawString(txtCheckUPResLym.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResLymStandard.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("Ketone", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber), _sfLeft);
                pdf.DrawString(cboCheckUPResUAKetone.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 10, linenumber), _sfLeft);

                pdf.DrawString("Monocyte", fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber += gapLine), _sfLeft);
                pdf.DrawString(txtCheckUPResMono.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResMonoStandard.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("WBC White blood cell", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResUAWBC.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 10, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResUAWBCStandard.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol8 + 2, linenumber), _sfLeft);

                pdf.DrawString("Eosinophil", fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber += gapLine), _sfLeft);
                pdf.DrawString(txtCheckUPResEos.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResEosStandard.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("RBC Red blood cell", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResUARBC.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 10, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResUARBCStandard.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol8 + 2, linenumber), _sfLeft);

                pdf.DrawString("Basophil", fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber += gapLine), _sfLeft);
                pdf.DrawString(txtCheckUPResBas.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResBasStandard.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("ผลการตรวจ UA", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber), _sfLeft);
                pdf.DrawString(cboCheckUPResUA.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 10, linenumber), _sfLeft);

                pdf.DrawString("  จำนวนเกล็ดเลือด     Platelets Count", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += gapLine), _sfLeft);
                pdf.DrawString(txtCheckUPResPlaCnt.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResPlaCntStandard.Text.Trim(), fPDFs8, Brushes.Black, new PointF(xCol5 + 2, linenumber + 4), _sfLeft);

                pdf.DrawString("  รูปร่างเม็ดเลือดแดง     RBC", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += gapLine), _sfLeft);
                pdf.DrawString(txtCheckUPResRBC.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResRBCStandard.Text.Trim(), fPDFs6, Brushes.Black, new PointF(xCol5 + 5, linenumber + 4), _sfLeft);

                linenumber += gapLine;
                //pdf.DrawString("5. การถ่ายภาพรังสีทรวงอก ", fPDF, Brushes.Black, new PointF(xCol2 + 5, 630), _sfLeft);
                pdf.DrawString("5. การถ่ายภาพรังสีทรวงอก Chest X-ray", fPDF, Brushes.Black, new PointF(xCol2 + 5, 630 + gapLine - 5), _sfLeft);
                pdf.DrawString(cboCheckUPResXray.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 220, 630 + gapLine - 5), _sfLeft);
                //pdf.DrawString("11. การคัดกรองมะเร็งลำไส้ใหญ่ และลำไส้ตรง(Fit test)", fPDF, Brushes.Black, new PointF(xCol6 + 5, 630), _sfLeft);
                //pdf.DrawString("Result    " + cboCheckUPResMS017.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol6 + 50, 630 ), _sfLeft);
                pdf.DrawString("11. การคัดกรองมะเร็งลำไส้ใหญ่ และลำไส้ตรง(Fit test)         " + cboCheckUPResMS017.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol6 + 5, 630 + gapLine - 5), _sfLeft);

                linenumber = 650;
                pdf.DrawString("สรุปผลตรวจ .....................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine - 2)), _sfLeft);
                pdf.DrawString(cboCheckUPRes.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 60, linenumber - 5), _sfLeft);
                pdf.DrawString("คำแนะนำเพิ่มเติมในการดูแลสุขภาพ .................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2, linenumber += (gapLine - 2)), _sfLeft);
                pdf.DrawString(cboCheckUPRes1.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol4 - 11, linenumber - 2), _sfLeft);
                pdf.DrawString(".............................................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2, linenumber += (gapLine - 4)), _sfLeft);
                pdf.DrawString(cboCheckUPRes2.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol2 + 5, linenumber - 2), _sfLeft);
                pdf.DrawString("ผู้ประกันตนลงนาม ................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine - 4)), _sfLeft);
                pdf.DrawString("ลงชื่อแพทย์ผู้ตรวจ ...............................................................................", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber), _sfLeft);
                pdf.DrawString("            (.................................................................................)", fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber += gapLine), _sfLeft);
                RectangleF recf1 = new RectangleF(xCol2 + 85, linenumber - 4, 150, 20);
                pdf.DrawString(cboCheckUPPrefixT.Text + " " + txtCheckUPNameT.Text.Trim() + " " + txtCheckUPSurNameT.Text.Trim(), fPDF, Brushes.Black, recf1, _sfCenter);
                pdf.DrawString("            (...............................................................................)", fPDF, Brushes.Black, new PointF(xCol6 + 55, linenumber), _sfLeft);
                recf1 = new RectangleF(xCol6 + 105, linenumber - 4, 150, 20);
                pdf.DrawString(txtCheckUPDoctorName.Text.Trim(), fPDF, Brushes.Black, recf1, _sfCenter);
                int lineqrcode = linenumber + 10;
                RectangleF recfqrcode = new RectangleF(xCol2 + 20, lineqrcode - 12, imgqrcode.Width + 15, imgqrcode.Height + 15);
                pdf.DrawImage(imgqrcode, recfqrcode);
                pdf.Save(filename);

                String patheName = Environment.CurrentDirectory + "\\cert_med\\";
                if ((Environment.CurrentDirectory.ToLower().IndexOf("windows") >= 0) && ((Environment.CurrentDirectory.ToLower().IndexOf("c:") >= 0)))
                {
                    new LogWriter("e", "FrmCertDoctorBn1 printCertDoctoriTextSharpThai Environment.CurrentDirectory " + Environment.CurrentDirectory);
                    patheName = bc.iniC.pathIniFile + "\\cert_med\\";
                }
                if (!Directory.Exists(patheName))
                {
                    Directory.CreateDirectory(patheName);
                }
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = "1100000007";
                dsc.hn = txtCheckUPHN.Text;

                dsc.an = "";
                dsc.vn = VN;

                dsc.visit_date = VSDATE;
                dsc.host_ftp = bc.iniC.hostFTP;
                //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                dsc.image_path = "";
                dsc.doc_group_sub_id = "1200000030";
                dsc.pre_no = PRENO;
                dsc.an_date = "";
                dsc.folder_ftp = bc.iniC.folderFTP;
                dsc.status_ipd = "O";
                dsc.row_no = "1";
                dsc.row_cnt = "1";
                dsc.status_ml = "2";
                dsc.ml_fm = "FM-MED-001";
                dsc.remark = "PDF";
                dsc.sort1 = "1";
                bc.bcDB.dscDB.voidDocScanByStatusCertMedical(txtCheckUPHN.Text, "FM-MED-001", VSDATE, PRENO, "");

                String reDocScanId = bc.bcDB.dscDB.insertScreenCapture(dsc, bc.userId);
                long chk = 0;
                if (long.TryParse(reDocScanId, out chk))
                {
                    dsc.image_path = txtCheckUPHN.Text.Replace("/", "-") + "//" + txtCheckUPHN.Text.Replace("/", "-") + "-" + reDocScanId + ".PDF";
                    String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, reDocScanId);
                    FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive, bc.iniC.ProxyProxyType, bc.iniC.ProxyHost, bc.iniC.ProxyPort);
                    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtCheckUPHN.Text.Replace("/", "-"));
                    ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                    if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename))
                    {
                        System.Threading.Thread.Sleep(200);
                    }
                }
                //if ((HN.Length > 0) && (PRENO.Length > 0))
                //{
                //    this.Dispose(true);
                //}
                //else
                //{
                //    Process p = new Process();
                //    ProcessStartInfo s = new ProcessStartInfo(filename);
                //    //s.Verb = "print";                           //ให้โปรแกรมเปิดไฟล์ด้วยคำสั่ง print
                //    //s.CreateNoWindow = true;                    //ให้โปรแกรมเปิดไฟล์ด้วยคำสั่ง print
                //    //s.WindowStyle = ProcessWindowStyle.Hidden;  //ให้โปรแกรมเปิดไฟล์ด้วยคำสั่ง print
                //    p.StartInfo = s;
                //    p.Start();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                new LogWriter("e", "FrmOPD genCheckUPSSOPDF1 " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "genCheckUPSSOPDF1 ", ex.Message);
            }
            finally
            {
                pdf.Dispose();
                Process p = new Process();
                ProcessStartInfo s = new ProcessStartInfo(Environment.CurrentDirectory + "\\" + filename);
                //s.Arguments = "/c dir *.cs";
                p.StartInfo = s;

                p.Start();
            }
        }
    }
}
