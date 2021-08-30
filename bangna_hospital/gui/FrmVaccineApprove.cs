using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.C1Pdf;
using C1.Win.BarCode;
using C1.Win.C1Command;
using C1.Win.C1Document;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmVaccineApprove:Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, fPrnBil, famtB, fEdits;

        C1DockingTab tcMain;
        C1DockingTabPage tabOP, tabOPBkk;

        C1FlexGrid grfSelect, grfImg;

        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1PdfDocument pdfDoc;
        C1ThemeController theme1;
        C1BarCode c1BarCode1;

        Label lbPID, lbName, lbDate, lbAddress, lbEmail, lbMobile, lbDose, lbID;
        C1TextBox txtPID, txtName, txtDate, txtAddress, txtEmail, txtMobile, txtDose, txtID;
        C1Button btnApprove, btnPrn, btnUnApprove;
        Panel pnOPBKK, pn;
        
        Boolean pageLoad = false;
        Form frm = new Form();

        int colPID = 1, colFirstname = 2, colLastname = 3, colDateOrder = 4, colAddress = 5, colEmail = 6, colMobile = 7, colDisease = 8, colDose = 8, colAllergy = 9, colStatusPay = 10, colID=11;
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);

        public FrmVaccineApprove(BangnaControl bc)
        {
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdits = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize-3, FontStyle.Regular);
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);
            fPrnBil = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            famtB = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
            theme1 = new C1.Win.C1Themes.C1ThemeController();
            pn = new Panel();

            initCompoment();
            setControl();
            

            this.Load += FrmVaccineApprove_Load;
            this.FormClosing += FrmVaccineApprove_FormClosing;
            grfSelect.DoubleClick += GrfSelect_DoubleClick;
        }

        private void FrmVaccineApprove_FormClosing(object sender, FormClosingEventArgs e)
        {
            //throw new NotImplementedException();
            if(frm != null)
            {
                frm.Dispose();
            }
        }

        private void BtnApprove_Click1(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String aaa = "";
        }

        private void setForm()
        {
            frm = new Form();
            int gapLine = 25, gapX = 20, gapY = 20, xCol2 = 130, xCol1 = 80, xCol3 = 470, xCol4 = 640, xCol5 = 950;
            Size size = new Size();

            //throw new NotImplementedException();

            frm.Size = new Size(1100, 800);
            frm.StartPosition = FormStartPosition.CenterScreen;
            
            pn.Dock = DockStyle.Fill;
            frm.Controls.Add(pn);
            
            grfImg = new C1FlexGrid();
            grfImg.Font = fEdit;
            grfImg.Dock = System.Windows.Forms.DockStyle.Bottom;
            grfImg.Location = new System.Drawing.Point(0, 0);
            grfImg.Rows.Count = 1;
            grfImg.Height = 540;
            grfImg.Rows.Count = 6;
            grfImg.Cols.Count = 15;
            grfImg.Cols[1].Caption = "";
            //grfINS.Cols[2].Caption = "เลขที่เอกสาร";
            //grfDITdc02.Cols[3].Caption = "Vendor";
            //grfDITdc02.Cols[4].Caption = "Price New";
            grfImg.Cols[1].Width = 120;
            grfImg.Cols[2].Width = 150;
            pn.Controls.Add(grfImg);

            lbID = new Label();
            lbPID = new Label();
            lbName = new Label();
            lbDate = new Label();
            lbAddress = new Label();
            lbEmail = new Label();
            lbMobile = new Label();
            lbDose = new Label();

            txtID = new C1TextBox();
            txtPID = new C1TextBox();
            txtName = new C1TextBox();
            txtDate = new C1TextBox();
            txtAddress = new C1TextBox();
            txtEmail = new C1TextBox();
            txtMobile = new C1TextBox();
            txtDose = new C1TextBox();

            bc.setControlLabel(ref lbID, fEdit, "เลขที่ใบจอง :", "lbID", gapX, gapY);
            size = bc.MeasureString(lbID);
            bc.setControlC1TextBox(ref txtID, fEdit, "txtID", 120, xCol2, lbID.Location.Y);
            txtID.Width = 120;

            bc.setControlLabel(ref lbPID, fEdit, "PID :", "lbPID", xCol3, lbID.Location.Y);
            size = bc.MeasureString(lbPID);
            bc.setControlC1TextBox(ref txtPID, fEdit, "txtPID", 120, lbPID.Location.X + size.Width + 15, lbID.Location.Y);
            txtPID.Width = 160;

            gapY += gapLine;
            bc.setControlLabel(ref lbName, fEdit, "ชื่อ นามสกุล :", "lbName", gapX, gapY);
            size = bc.MeasureString(lbName);
            bc.setControlC1TextBox(ref txtName, fEdit, "txtName", 120, xCol2, lbName.Location.Y);
            txtName.Width = 320;

            gapY += gapLine;
            bc.setControlLabel(ref lbDate, fEdit, "วันที่จอง :", "lbDate", gapX, gapY);
            size = bc.MeasureString(lbDate);
            bc.setControlC1TextBox(ref txtDate, fEdit, "txtDate", 120, xCol2, lbDate.Location.Y);
            txtDate.Width = 180;

            gapY += gapLine;
            bc.setControlLabel(ref lbAddress, fEdit, "ที่อยู่ :", "lbAddress", gapX, gapY);
            size = bc.MeasureString(lbDate);
            bc.setControlC1TextBox(ref txtAddress, fEdit, "txtAddress", 120, xCol2, lbAddress.Location.Y);
            txtAddress.Width = 620;

            gapY += gapLine;
            bc.setControlLabel(ref lbEmail, fEdit, "Email :", "lbEmail", gapX, gapY);
            size = bc.MeasureString(lbDate);
            bc.setControlC1TextBox(ref txtEmail, fEdit, "txtEmail", 120, xCol2, lbEmail.Location.Y);
            txtEmail.Width = 320;
            bc.setControlLabel(ref lbMobile, fEdit, "Mobile :", "lbMobile", xCol3, lbEmail.Location.Y);
            size = bc.MeasureString(lbPID);
            bc.setControlC1TextBox(ref txtMobile, fEdit, "txtMobile", 120, lbMobile.Location.X + size.Width + 15, lbMobile.Location.Y);
            txtMobile.Width = 200;

            gapY += gapLine;
            bc.setControlLabel(ref lbDose, fEdit, "Dose :", "lbDose", gapX, gapY);
            size = bc.MeasureString(lbDate);
            bc.setControlC1TextBox(ref txtDose, fEdit5B, "txtDose", 120, xCol2, lbDose.Location.Y);
            txtDose.Width = 300;

            gapY += gapLine + 40;
            bc.setControlC1Button(ref btnApprove, fEdit, "ตรวจสอบเรียบร้อย", "btnApprove", gapX, gapY);
            btnApprove.Width = 180;
            bc.setControlC1Button(ref btnPrn, fEdit, "พิมพ์ ใบจอง", "btnPrn", btnApprove.Location.X + btnApprove.Width + 40, gapY);
            btnPrn.Width = 120;

            gapY += gapLine + 40;
            bc.setControlC1Button(ref btnUnApprove, fEdit, "ตรวจสอบ ยังไม่เรียบร้อย", "btnUnApprove", gapX, gapY);
            btnUnApprove.Width = 180;

            c1BarCode1 = new C1.Win.BarCode.C1BarCode();
            c1BarCode1.AdditionalNumber = null;
            c1BarCode1.BackColor = System.Drawing.Color.White;
            c1BarCode1.CodeType = C1.BarCode.CodeType.QRCode;
            c1BarCode1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            c1BarCode1.ForeColor = System.Drawing.Color.Black;
            c1BarCode1.Location = new System.Drawing.Point(800, 60);
            c1BarCode1.MicroQRCodeOptions.EncodingCodePage = 65001;
            c1BarCode1.Name = "c1BarCode1";
            c1BarCode1.QuietZone.Bottom = 0D;
            c1BarCode1.QuietZone.Left = 0D;
            c1BarCode1.QuietZone.Right = 0D;
            c1BarCode1.QuietZone.Top = 0D;
            c1BarCode1.Size = new System.Drawing.Size(151, 150);
            c1BarCode1.TabIndex = 1;
            c1BarCode1.Text = txtID.Text;
            c1BarCode1.TextFixedLength = 0;
            c1BarCode1.WideToNarrowRatio = 2F;

            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ตรวจสอบเรียบร้อย", new EventHandler(ContextMenu_Approve));
            menuGw.MenuItems.Add("พิมพ์ ใบจอง", new EventHandler(ContextMenu_printReservel));
            menuGw.MenuItems.Add("ตรวจสอบ ยังไม่เรียบร้อย", new EventHandler(ContextMenu_UnApprove));
            frm.ContextMenu = menuGw;

            pn.Controls.Add(txtID);
            pn.Controls.Add(txtPID);
            pn.Controls.Add(lbID);
            pn.Controls.Add(lbPID);
            pn.Controls.Add(txtName);
            pn.Controls.Add(lbName);
            pn.Controls.Add(txtDate);
            pn.Controls.Add(lbDate);
            pn.Controls.Add(txtAddress);
            pn.Controls.Add(lbAddress);
            pn.Controls.Add(txtMobile);
            pn.Controls.Add(lbMobile);
            pn.Controls.Add(txtEmail);
            pn.Controls.Add(lbEmail);
            pn.Controls.Add(txtDose);
            pn.Controls.Add(lbDose);
            pn.Controls.Add(c1BarCode1);

        }
        private void ContextMenu_Approve(object sender, System.EventArgs e)
        {
            int chk = 0;
            String re = bc.bcDB.vaccDB.updateStatusPayment(txtID.Text.Trim());
            if (int.TryParse(re, out chk))
            {
                frm.Hide();
                setGrfClinic();
            }
        }
        private void ContextMenu_UnApprove(object sender, System.EventArgs e)
        {
            String aaa = "";
            int chk = 0;
            String re = bc.bcDB.vaccDB.updateStatusPaymentVoid(txtID.Text.Trim());
            if (int.TryParse(re, out chk))
            {
                frm.Hide();
                setGrfClinic();
            }
        }
        private void ContextMenu_printReservel(object sender, System.EventArgs e)
        {
            printReserveVaccinePDF();
        }
        private void GrfSelect_DoubleClick(object sender, EventArgs e)
        {
            if (pageLoad) return;
            String id = "";
            id = grfSelect[grfSelect.Row, colID] != null ? grfSelect[grfSelect.Row, colID].ToString():"";
            Vaccine vacc = new Vaccine();

            vacc = bc.bcDB.vaccDB.SelectByPk(id);

            txtID.Value = vacc.reserve_vaccine_id;
            txtPID.Value = vacc.pid;
            txtName.Value = vacc.firstname + "  " + vacc.lastname;
            txtDate.Value = vacc.date_create;
            txtAddress.Value = vacc.address;
            txtEmail.Value = vacc.email;
            txtMobile.Value = vacc.mobile;
            txtDose.Value = vacc.dose;
            string[] filePaths = Directory.GetFiles(System.IO.Path.GetDirectoryName(Application.ExecutablePath)+"\\slip\\", txtID.Text.Trim() + "*.jpg");
            grfImg.Clear();
            grfImg.Cols.Count = 2;
            grfImg.Rows.Count = 0;
            if (filePaths.Length > 0)
            {
                
                grfImg.Rows.Count = filePaths.Length+1;
                Column colpic1 = grfImg.Cols[1];
                colpic1.DataType = typeof(Image);
                int i = 1;
                foreach (String filename in filePaths)
                {
                    Row rowd;
                    rowd = grfImg.Rows[i];
                    Image loadedImage, resizedImage = null;
                    loadedImage = Image.FromFile(filename);
                    int originalWidth = 0;
                    originalWidth = loadedImage.Width;
                    int newWidth = bc.imgScanWidth;
                    newWidth = 400;
                    resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);

                    rowd[1] = resizedImage;
                    i++;
                }
            }
            grfImg.AutoSizeCols();
            grfImg.AutoSizeRows();
            frm.ShowDialog(this);
        }

        private void BtnUnApprove_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
        }

        private void BtnPrn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
        }
        private void printReserveVaccinePDF()
        {
            String pathFolder = "", filename = "", datetick = "";
            int gapLine = 20, gapLine1 = 15, gapX = 40, gapY = 20, xCol2 = 200, xCol1 = 160, xCol3 = 300, xCol4 = 390, xCol5 = 500;
            Size size = new Size();

            C1PdfDocument pdf = new C1PdfDocument();
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            StringFormat _sfRight, _sfRightCenter;
            //Font _fontTitle = new Font("Tahoma", 15, FontStyle.Bold);
            _sfRight = new StringFormat();
            _sfRight.Alignment = StringAlignment.Far;
            _sfRightCenter = new StringFormat();
            _sfRightCenter.Alignment = StringAlignment.Far;
            _sfRightCenter.LineAlignment = StringAlignment.Center;

            Font titleFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetitleFont, FontStyle.Bold);
            Font hdrFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizehdrFont, FontStyle.Regular);
            Font hdrFontB = new Font(bc.iniC.pdfFontName, 16, FontStyle.Bold);
            Font ftrFont = new Font(bc.iniC.pdfFontName, 8);
            Font txtFont = new Font(bc.iniC.pdfFontName, bc.pdfFontSizetxtFont, FontStyle.Regular);
            pdf.FontType = FontTypeEnum.Embedded;

            RectangleF rcPage = pdf.PageRectangle;
            rcPage = RectangleF.Empty;
            rcPage.Inflate(-72, -92);
            rcPage.Location = new PointF(rcPage.X, rcPage.Y + titleFont.SizeInPoints + 10);
            rcPage.Size = new SizeF(0, titleFont.SizeInPoints + 3);
            rcPage.Width = 110;

            Image loadedImage;
            loadedImage = Resources.LOGO_BW_tran;
            float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
            float newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

            float widthFactor = 4.8F;
            float heightFactor = 4.8F;

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

            RectangleF recf = new RectangleF(15, 15, (int)newWidth, (int)newHeight);
            pdf.DrawImage(loadedImage, recf);
            rcPage.X = gapX + recf.Width - 10;
            rcPage.Y = gapY;
            RectangleF rc = rcPage;

            string[] filePaths = Directory.GetFiles(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\slip\\", txtID.Text.Trim() + "*.jpg");
            if (filePaths.Length > 0)
            {
                int i = 1, xx=40;
                foreach (String filename1 in filePaths)
                {
                    Image loadedImage1, resizedImage1 = null;
                    loadedImage1 = Image.FromFile(filename1);
                    int originalWidth = 0;
                    originalWidth = loadedImage1.Width;
                    int newWidth1 = 200;

                    resizedImage1 = loadedImage1.GetThumbnailImage(newWidth1, (newWidth1 * loadedImage1.Height) / originalWidth, null, IntPtr.Zero);
                    RectangleF recf1 = new RectangleF(i==1?xx: newWidth1+10, 380, (int)newWidth1, (int)resizedImage1.Height);
                    pdf.DrawImage(loadedImage1, recf1);
                    i++;
                }
            }
            Image qrcode = c1BarCode1.Image;
            RectangleF recf2 = new RectangleF(350, 500, qrcode.Width, qrcode.Height);
            pdf.DrawImage(qrcode, recf2);

            size = bc.MeasureString(bc.iniC.hostname, titleFont);
            rcPage.Width = size.Width;
            pdf.DrawString(bc.iniC.hostname, titleFont, Brushes.Black, rcPage);
            gapY += gapLine;
            rcPage.Y = gapY;
            size = bc.MeasureString(bc.iniC.hostaddresst, hdrFont);
            rcPage.Width = size.Width;
            pdf.DrawString(bc.iniC.hostaddresst, hdrFont, Brushes.Black, rcPage);

            String dose = "", amt11 = "";
            int amt111 = 0;
            dose = txtDose.Text.Replace("จอง", "").Replace("เข็ม", "").Replace("3,300", "").Replace("1,650", "").Replace("4,950", "").Trim().Replace("6,600", "").Trim().Replace("8,250", "")
                .Replace("8,250", "").Replace("9,900", "").Replace("11,500", "").Replace("13,200", "").Replace("14,850", "").Replace("16,500", "").Replace("18,150", "").Replace("19,800", "").Trim();
            int.TryParse(dose, out amt111);
            amt111 *= 1650;

            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapX = xCol1;
            rc.X = (pdf.PageSize.Width/2)-15;
            rc.Y = gapY;
            pdf.DrawString("ใบจองวัคซีน", titleFont, Brushes.Black, rc);

            gapY += gapLine;
            gapY += gapLine;
            gapX = 30;
            rc.X = gapX;
            rc.Y = gapY;
            pdf.DrawString("ข้าพเจ้า ชื่อ-นามสกุล", txtFont, Brushes.Black, rc);
            rc.X = 110;
            rc.Y = rc.Y-4;
            pdf.DrawString(txtName.Text, hdrFont, Brushes.Black, rc);
            pdf.DrawLine(Pens.Gray, 100, gapY+15,380, gapY+15);
            rc.X = 400;
            rc.Y = gapY;
            pdf.DrawString("เลขที่ประชาชน", txtFont, Brushes.Black, rc);
            rc.X = 470;
            rc.Y = rc.Y - 4;
            pdf.DrawString(txtPID.Text, hdrFont, Brushes.Black, rc);
            pdf.DrawLine(Pens.Gray, 455, gapY + 15, 590, gapY + 15);

            gapY += gapLine;
            gapX = 30;
            rc.X = gapX;
            rc.Y = gapY;
            pdf.DrawString("ที่อยู่ปัจจุบัน", txtFont, Brushes.Black, rc);
            rc.X = 75;
            rc.Y = rc.Y - 4;
            pdf.DrawString(txtAddress.Text, hdrFont, Brushes.Black, rc);
            pdf.DrawLine(Pens.Gray, 70, gapY + 15, 400, gapY + 15);
            rc.X = 420;
            rc.Y = gapY;
            pdf.DrawString("เบอร์ที่ติดต่อได้", txtFont, Brushes.Black, rc);
            rc.X = 480;
            rc.Y = rc.Y - 4;
            pdf.DrawString(txtMobile.Text, hdrFont, Brushes.Black, rc);
            pdf.DrawLine(Pens.Gray, 470, gapY + 15, 590, gapY + 15);

            gapY += gapLine;
            gapX = 30;
            rc.X = gapX;
            rc.Y = gapY;
            rc.Width = 610;
            pdf.DrawString("ขอจองวัคซีนทางเลือก MODERNA และชำระเงิน  จำนวน             โดส   ราคา 1,650 บาทต่อโดส", txtFont, Brushes.Black, rc);
            rc.X = 19;
            rc.Y = rc.Y - 4;
            pdf.DrawString(dose, hdrFont, Brushes.Black, rc);
            pdf.DrawLine(Pens.Gray, 215, gapY + 15, 220, gapY + 15);

            gapY += gapLine;
            gapX = 30;
            rc.X = gapX;
            rc.Y = gapY;
            pdf.DrawString("ราคาดังกล่าวเป็นราคาที่ รวมค่าวัคซีน  ค่าประกัน  ค่าบริการสำหรับการฉีด ไม่รวมค่าแพทย์ ถ้าต้องการพบแพทย์  โดยชำระเงินเต็มจำนวน", txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = 30;
            rc.X = gapX;
            rc.Y = gapY;
            pdf.DrawString("โดสละ 1,650  รวม            โดส  เป็นเงิน                               บาท  ตามใบเจองเลขที่                            ", txtFont, Brushes.Black, rc);
            rc.X = 100;
            rc.Y = rc.Y - 4;
            pdf.DrawString(dose, hdrFont, Brushes.Black, rc);
            rc.X = 175;
            rc.Y = rc.Y;
            pdf.DrawString(amt111.ToString("#,###.00"), hdrFont, Brushes.Black, rc);
            rc.X = 310;
            rc.Y = rc.Y;
            pdf.DrawString(txtID.Text.Trim(), hdrFont, Brushes.Black, rc);
            pdf.DrawLine(Pens.Gray, 95, gapY + 15, 100, gapY + 15);
            pdf.DrawLine(Pens.Gray, 165, gapY + 15, 220, gapY + 15);
            pdf.DrawLine(Pens.Gray, 340, gapY + 15, 470, gapY + 15);

            gapY += gapLine;
            gapX = 30;
            rc.X = gapX;
            rc.Y = gapY;
            pdf.DrawString("หมายเหตุ  1.ทางโรงพยาบาลจะนัดรับวัคซีนหลังจาก โรงพยาบาลได้รับการจัดสรรจากหน่วยงานภาครัฐ  ตามที่อยู่และเบอร์โทรที่ได้ให้ไว้", txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = 30;
            rc.X = 67;
            rc.Y = gapY;
            pdf.DrawString("2.กรณีได้รับจัดสรรวัคซีนมาไม่เพียงพอต่อการจองที่โรงพยาบาลได้รับจองตามที่ได้รับจัดสรร  ทางโรงพยาบาลจะเรียงลำดับการเข้ารับวัคซีนตามลำดดับการจองก่อน-หลัง", txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = 30;
            rc.X = 67;
            rc.Y = gapY;
            pdf.DrawString("และจะคืนเงินมัดจำให้เต็มจำนวน  กรณีจองแล้วไม่ได้", txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = 30;
            rc.X = 67;
            rc.Y = gapY;
            pdf.DrawString("3.ทางโรงพยาบาลสงวนสิทธิ์ยกเลิกการของโดยไม่คืนเงินกรณีผู้จองไม่มารับวัคซีนตามช่วงเวลาที่กำหนด", txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = 30;
            rc.X = 67;
            rc.Y = gapY;
            pdf.DrawString("4.ห้ามนำวัคซีนไปขายต่อเพราะเป็นสินค้าควบคุมราคา", txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = 30;
            rc.X = 67;
            rc.Y = gapY;
            pdf.DrawString("5.ต้องมารับบริการฉีดวัคซีน ที่โรงพยาบาล บางนา5 เท่านั้น", txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapX = 30;
            rc.X = 370;
            rc.Y = gapY;
            pdf.DrawString("ผู้จอง", txtFont, Brushes.Black, rc);
            pdf.DrawLine(Pens.Gray, 400, gapY + 15, 500, gapY + 15);

            gapY += gapLine;
            gapX = 30;
            rc.X = 370;
            rc.Y = gapY;
            pdf.DrawString("ผู้รับจอง  on line", txtFont, Brushes.Black, rc);
            pdf.DrawLine(Pens.Gray, 400, gapY + 15, 500, gapY + 15);

            String txt = "";
            if (txtDate.Text.Trim().Length > 9)
            {
                txt = bc.datetoShow(txtDate.Text.Trim()) + " " + txtDate.Text.Substring(10).Trim();
            }
            else
            {
                txt = txtDate.Text.Trim();
            }
            gapY += gapLine;
            gapX = 30;
            rc.X = 370;
            rc.Y = gapY;
            pdf.DrawString("วันที่จอง   "+ txt, txtFont, Brushes.Black, rc);
            pdf.DrawLine(Pens.Gray, 400, gapY + 15, 500, gapY + 15);
            
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapY += gapLine;
            gapX = 30;
            rc.X = gapX;
            rc.Y = gapY;
            pdf.DrawString("สอบถามเพิ่มเติมโทร 02 138 1155-60 ต่อ 143   ", txtFont, Brushes.Black, rc);
            gapY += gapLine;
            gapX = 30;
            rc.X = gapX;
            rc.Y = gapY;
            pdf.DrawString("วันจันทร์ ถึง วันศุกร์ เวลา 8.00 - 16.00 น. (ปิดพักเที่ยง)   ", txtFont, Brushes.Black, rc);
            gapY += gapLine;
            gapX = 30;
            rc.X = gapX;
            rc.Y = gapY;
            pdf.DrawString("line @657bkkyq", txtFont, Brushes.Black, rc);


            RectangleF rcHdr = new RectangleF();
            rcHdr.Width = pdf.PageSize.Width-20;
            rcHdr.Height = pdf.PageSize.Height-20;
            rcHdr.X = 10;
            rcHdr.Y = 10;
            String PathName = "medical", fileName = "";
            datetick = DateTime.Now.Ticks.ToString();
            if (!Directory.Exists("report"))
            {
                Directory.CreateDirectory("report");
            }
            fileName = "report\\"+txtID.Text.Trim()+"_" + datetick + ".pdf";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                System.Threading.Thread.Sleep(100);
            }
            pdf.DrawRectangle(Pens.Black, rcHdr);       // ตาราง

            String path = Path.GetDirectoryName(Application.ExecutablePath);
            pdf.Save(path + "\\" + fileName);
            Process.Start(fileName);
        }

        private void printReserveVaccine()
        {
            PrintDocument document = new PrintDocument();
            document.PrintPage += Document_PrintPage_ReserveVaccine;
            document.PrinterSettings.PrinterName = bc.iniC.printerA4;
            document.DefaultPageSettings.Landscape = false;

            document.Print();
        }

        private void Document_PrintPage_ReserveVaccine(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                String amt = "", line = null, date = "", price = "", qty = "", price1 = "";
                Decimal amt1 = 0, voucamt = 0, discount = 0, total = 0, cash = 0;
                float yPos = 10, gap = 6, colName = 0, col2 = 25, col3 = 250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 820, col40 = 620;
                int count = 0, recx = 15, recy = 15, col2int = 0, yPosint = 0, col40int = 0;
                float yline = 40;

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
                Rectangle rec = new Rectangle(0, 0, 20, 20);
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
                String dose = "", amt11="";
                int amt111 = 0;
                dose = txtDose.Text.Replace("จอง", "").Replace("เข็ม", "").Replace("3,300", "").Replace("1,650", "").Replace("4,950", "").Trim().Replace("6,600", "").Trim().Replace("8,250", "")
                    .Replace("8,250", "").Replace("9,900", "").Replace("11,500", "").Replace("13,200", "").Replace("14,850", "").Replace("16,500", "").Replace("18,150", "").Replace("19,800", "").Trim();
                int.TryParse(dose, out amt111);
                amt111 *=  1650;

                System.Drawing.Image img = Resources.LOGO_BW_tran;

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
                        newWidth = newWidth / float.Parse("4.2");
                        newHeight = newHeight / float.Parse("4.2");
                    }
                    else
                    {
                        newWidth = newWidth / heightFactor;
                        newHeight = newHeight / heightFactor;
                    }
                }
                e.Graphics.DrawImage(img, 10, 20, (int)newWidth, (int)newHeight);
                e.Graphics.DrawString("โรงพยาบาล บางนา5", fEdits, Brushes.Black, 30, 60, flags);
                e.Graphics.DrawString("BANGNA5 GENERAL HOSPITAL", fEdits, Brushes.Black, 60, yPos, flags);

                yPos = 80;
                line = "ใบจองวัคซีน";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
                yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
                                                                    //e.Graphics.DrawString(line, fPrn, Brushes.Black, xOffset, yPos, new StringFormat());leftMargin
                e.Graphics.DrawString(line, famtB, Brushes.Black, avg-30, yPos, flags);

                yPos += yline;
                line = "ข้าพเจ้า ชื่อ-นามสกุล  __________________________________   เลขที่ประชาชน  ____________________";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos, flags);
                line = txtName.Text.Trim();
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2+150, yPos-3, flags);
                line = txtPID.Text.Trim();
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 650, yPos - 3, flags);

                yPos += yline;
                line = "ที่อยู่ปัจจุบัน  __________________________________________________________  เบอร์ที่ติดต่อได้  ____________________";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos, flags);
                line = txtAddress.Text.Trim();
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 100, yPos - 3, flags);
                line = txtMobile.Text.Trim();
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 740, yPos - 3, flags);

                yPos += yline;
                line = "ขอจองวัคซีนทางเลือก MODERNA และชำระเงิน  จำนวน  ___________ โดส   ราคา 1,650 บาทต่อโดส";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos, flags);
                line = dose;
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 380, yPos - 3, flags);

                yPos += yline;
                line = "ราคาดังกล่าวเป็นราคาที่ รวมค่าวัคซีน  ค่าประกัน  ค่าบริการสำหรับการฉีด ไม่รวมค่าแพทย์ ถ้าต้องการพบแพทย์  โดยชำระเงินเต็มจำนวน";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos, flags);

                yPos += yline;
                line = "โดสละ 1,650  รวม  ________ โดส  เป็นเงิน  __________________  บาท  ตามใบเสร็จรับเงินเลขที่  ________________________ ";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos, flags);
                line = dose;
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 140, yPos - 3, flags);
                
                e.Graphics.DrawString(amt111.ToString("#,###.00"), fEdit, Brushes.Black, col2 + 330, yPos - 3, flags);

                yPos += yline;
                line = "หมายเหตุ  1.ทางโรงพยาบาลจะนัดรับวัคซีนหลังจาก โรงพยาบาลได้รับการจัดสรรจากหน่วยงานภาครัฐ  ตามที่อยู่และเบอร์โทรที่ได้ให้ไว้";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos, flags);

                yPos += yline;
                line = "2.กรณีได้รับจัดสรรวัคซีนมาไม่เพียงพอต่อการจองที่โรงพยาบาลได้รับจองตามที่ได้รับจัดสรร  ทางโรงพยาบาลจะเรียงลำดับการเข้ารับวัคซีนตามลำดดับการจองก่อน-หลัง และจะคืนเงินมัดจำให้เต็มจำนวน  กรณีจองแล้วไม่ได้";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos, flags);

                yPos += yline;
                line = "3.ทางโรงพยาบาลสงวนสิทธิ์ยกเลิกการของโดยไม่คืนเงินกรณีผู้จองไม่มารับวัคซีนตามช่วงเวลาที่กำหนด";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos, flags);

                yPos += yline;
                line = "4.ห้ามนำวัคซีนไปขายต่อเพราะเป็นสินค้าควบคุมราคา";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos, flags);

                yPos += yline;
                line = "5.ต้องมารับบริการฉีดวัคซีน ที่โรงพยาบาล บางนา5 เท่านั้น";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos, flags);

                yPos += yline;
                line = "ผู้จอง  ________________________________________";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos, flags);
                line = txtName.Text.Trim();
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 640, yPos - 3, flags);
                
                yPos += yline;
                line = "ผู้รับจอง  on line ";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos, flags);

                yPos += yline;
                line = "วันที่จอง __________________________________ ";
                textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos, flags);
                if (txtDate.Text.Trim().Length > 9)
                {
                    line = bc.datetoShow(txtDate.Text.Trim()) + " " + txtDate.Text.Substring(10).Trim();
                }
                else
                {
                    line = txtDate.Text.Trim();
                }
                e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 640, yPos - 3, flags);
            }
            catch(Exception ex)
            {
                new LogWriter("e", " LabM01DB updateOPBKKCode error " + ex.InnerException);
            }
        }

        private void BtnApprove_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
        }

        private void initCompoment()
        {
            int gapLine = 25, gapX = 20, gapY = 20, xCol2 = 130, xCol1 = 80, xCol3 = 330, xCol4 = 640, xCol5 = 950;
            Size size = new Size();

            tcMain = new C1DockingTab();
            tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tcMain.Location = new System.Drawing.Point(0, 266);
            tcMain.Name = "tcMain";
            tcMain.Size = new System.Drawing.Size(669, 200);
            tcMain.TabIndex = 0;
            tcMain.TabsSpacing = 5;
            tcMain.SelectedTabChanged += TcMain_SelectedTabChanged;

            tabOPBkk = new C1DockingTabPage();
            tabOPBkk.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOPBkk.Size = new System.Drawing.Size(667, 175);
            tabOPBkk.TabIndex = 0;
            tabOPBkk.Text = "Vaccine";
            tabOPBkk.Name = "tabVaccine";
            tcMain.Controls.Add(tabOPBkk);

            tabOP = new C1DockingTabPage();
            tabOP.Dock = System.Windows.Forms.DockStyle.Fill;
            tabOP.Name = "tabOP";
            tabOP.Text = "OPBKK Main";
            tcMain.Controls.Add(tabOP);

            grfSelect = new C1FlexGrid();
            grfSelect.Font = fEdit;
            grfSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSelect.Location = new System.Drawing.Point(0, 0);
            grfSelect.Rows.Count = 1;
            grfSelect.Name = "grfSelect";
            tabOPBkk.Controls.Add(grfSelect);
            theme1.SetTheme(grfSelect, "Office2010Red");

            theme1.SetTheme(this, bc.iniC.themeApp);
            theme1.SetTheme(tcMain, bc.iniC.themeApp);
            this.Controls.Add(tcMain);
        }
        private void setControl()
        {
            setForm();
        }
        private void setGrfClinic()
        {
            pageLoad = true;
            DataTable dt = new DataTable();
            //C1ComboBox cboMethod = new C1ComboBox();
            //cboMethod.AutoCompleteMode = AutoCompleteMode.Suggest;
            //cboMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
            //bc.setCboOPBKKClinic(cboMethod, "");

            dt = bc.bcDB.vaccDB.SelectAll();
            grfSelect.Rows.Count = 1;
            grfSelect.Cols.Count = 12;
            grfSelect.Rows.Count = dt.Rows.Count + 1;
            grfSelect.Cols[colPID].Caption = "PID";
            grfSelect.Cols[colFirstname].Caption = "Firstname";
            grfSelect.Cols[colLastname].Caption = "Lastname";
            grfSelect.Cols[colDateOrder].Caption = "Date";
            grfSelect.Cols[colAddress].Caption = "Address";
            grfSelect.Cols[colEmail].Caption = "Email";
            grfSelect.Cols[colMobile].Caption = "Mobile";
            grfSelect.Cols[colDisease].Caption = "Disease";
            grfSelect.Cols[colDose].Caption = "Dose";
            grfSelect.Cols[colAllergy].Caption = "Allergy";
            grfSelect.Cols[colStatusPay].Caption = "Payment";
            grfSelect.Cols[colID].Caption = "ID";
            grfSelect.Cols[colPID].Width = 100;
            grfSelect.Cols[colFirstname].Width = 100;
            grfSelect.Cols[colLastname].Width = 100;
            grfSelect.Cols[colDateOrder].Width = 100;
            grfSelect.Cols[colAddress].Width = 100;
            grfSelect.Cols[colEmail].Width = 100;
            grfSelect.Cols[colMobile].Width = 100;
            grfSelect.Cols[colDisease].Width = 100;
            grfSelect.Cols[colDose].Width = 200;
            grfSelect.Cols[colAllergy].Width = 100;
            grfSelect.Cols[colStatusPay].Width = 100;
            
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                String inscl = "";
                
                grfSelect[i, colPID] = row1["pid"].ToString();
                grfSelect[i, colFirstname] = row1["firstname"].ToString();
                grfSelect[i, colLastname] = row1["lastname"].ToString();
                grfSelect[i, colDateOrder] = row1["date_create"].ToString();
                grfSelect[i, colAddress] = row1["address"].ToString();
                grfSelect[i, colEmail] = row1["email"].ToString();
                grfSelect[i, colMobile] = row1["mobile"].ToString();
                grfSelect[i, colDisease] = row1["disease"].ToString();
                grfSelect[i, colDose] = row1["dose"].ToString();
                grfSelect[i, colAllergy] = row1["drug_allergy"].ToString();
                grfSelect[i, colStatusPay] = row1["status_payment"].ToString();
                grfSelect[i, colID] = row1["reserve_vaccine_id"].ToString();
                grfSelect[i, 0] = i;
                if (i % 2 == 0)
                    grfSelect.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                if (row1["status_payment"].ToString().Equals("1")){
                    grfSelect.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.txtFocus);
                }
            }
            grfSelect.Cols[colPID].Visible = false;
            grfSelect.Cols[colFirstname].AllowEditing = false;
            grfSelect.Cols[colLastname].AllowEditing = false;
            grfSelect.Cols[colDateOrder].AllowEditing = false;
            grfSelect.Cols[colAddress].AllowEditing = false;
            grfSelect.Cols[colEmail].AllowEditing = false;
            grfSelect.Cols[colMobile].AllowEditing = false;
            grfSelect.Cols[colDisease].AllowEditing = false;
            grfSelect.Cols[colDose].AllowEditing = false;
            grfSelect.Cols[colAllergy].AllowEditing = false;
            grfSelect.Cols[colStatusPay].AllowEditing = false;
            grfSelect.Cols[colID].AllowEditing = false;
            //grfSelect.Cols[colClinicdpno].AllowEditing = false;
            pageLoad = false;
        }
        private void TcMain_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void FrmVaccineApprove_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            setGrfClinic();
            this.Text = "update 2021-07-19";
        }
    }
}
