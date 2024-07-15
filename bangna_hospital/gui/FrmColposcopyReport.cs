using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.C1Pdf;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmColposcopyReport : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit1, fEdit1B, fPrnBil, famtB14, fEditS, famtB10;

        C1ThemeController theme1;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1PdfDocument _c1pdf;
        RichTextBox rtbBcPreOpration;
        C1PictureBox page1Pic, page2Pic1, page2Pic2, page2Pic3, page2Pic4, page3Pic1, page3Pic2, page3Pic3, page3Pic4, page26Pic1, page26Pic2, page26Pic3, page26Pic4, page26Pic5, page26Pic6;

        Patient ptt;
        FrmDoctorDiag1 frmPage1Diag, frmPage2Diag, frmPage31Diag, frmPage32Diag, frmPage33Diag, frmPage34Diag, frmPage26Diag;

        ContextMenu menuPage1Pic, menuPage2Pic1, menuPage2Pic2, menuPage2Pic3, menuPage2Pic4, menuPage3Pic1, menuPage3Pic2, menuPage3Pic3, menuPage3Pic4, menuPage26Pic1, menuPage26Pic2, menuPage26Pic3, menuPage26Pic4, menuPage26Pic5, menuPage26Pic6;
        public FrmColposcopyReport(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit1 = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit1B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fPrnBil = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            famtB14 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 14, FontStyle.Bold);
            fEditS = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            famtB10 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 10, FontStyle.Bold);

            theme1 = new C1.Win.C1Themes.C1ThemeController();
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
            _c1pdf = new C1PdfDocument();
            _c1pdf.DocumentInfo.Producer = "C1Pdf";
            _c1pdf.Security.AllowCopyContent = true;
            _c1pdf.Security.AllowEditAnnotations = true;
            _c1pdf.Security.AllowEditContent = true;
            _c1pdf.Security.AllowPrint = true;

            initCompoment();
            btnSearch.Click += BtnSearch_Click;
            txtDtrCode.KeyUp += TxtDtrCode_KeyUp;
            btnpage1Prn.Click += Btnpage1Prn_Click;
            btnSave.Click += BtnSave_Click;
            btnpage2Prn.Click += Btnpage2Prn_Click;
            btnpage3Prn.Click += Btnpage3Prn_Click;
            btnpage26Prn.Click += Btnpage26Prn_Click;
        }

        private void Btnpage26Prn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printPage26();
        }
        private void printPage26()
        {
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerA4;

            document.PrintPage += Document_PrintPage26;
            document.DefaultPageSettings.Landscape = false;

            document.Print();
        }
        private void Document_PrintPage26(object sender, PrintPageEventArgs e)
        {
            String amt = "", line = null, date = "", txt = "";
            float yPos = 10, ydate = 0, gapline = 35;
            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);
            DateTime dtstart = new DateTime();
            int y = 0;
            //logo
            Image logoLeft;
            try
            {
                logoLeft = Resources.LOGO_BW_tran;
                float newWidth = logoLeft.Width * 100 / logoLeft.HorizontalResolution;
                float newHeight = logoLeft.Height * 100 / logoLeft.VerticalResolution;

                float widthFactor = 3.8F;
                float heightFactor = 3.8F;
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
                e.Graphics.DrawImage(logoLeft, recf);
                e.Graphics.DrawString(bc.iniC.hostname, fEditB, Brushes.Black, recf.Width + 20, 30, flags);
                e.Graphics.DrawString(bc.iniC.hostnamee, fEditB, Brushes.Black, recf.Width + 20, 60, flags);
                e.Graphics.DrawString(bc.iniC.hostaddresst, fEditS, Brushes.Black, 260, 30, flags);
                e.Graphics.DrawString(bc.iniC.hostaddresse, fEditS, Brushes.Black, 360, 60, flags);

                e.Graphics.DrawString("Colposcopy Report", famtB10, Brushes.Black, 300, 80, flags);

                e.Graphics.DrawString("Patient Name .......................................................................................  HN .........................", fEditB, Brushes.Black, 50, 140, flags);
                e.Graphics.DrawString(txtPttName.Text, fEditB, Brushes.Black, 170, 136, flags);
                e.Graphics.DrawString(txtHn.Text, fEditB, Brushes.Black, 660, 136, flags);
                Image img = bc.ResizeImage(page26Pic1.Image, 320, 200);
                newWidth = img.Width * 100 / img.HorizontalResolution;
                newHeight = img.Height * 100 / img.VerticalResolution;
                widthFactor = newWidth / e.MarginBounds.Width;
                heightFactor = newHeight / e.MarginBounds.Height;
                if (widthFactor > 1 | heightFactor > 1)
                {
                    if (widthFactor > heightFactor)
                    {
                        widthFactor = 1;
                        //newWidth = newWidth / widthFactor;
                        //newHeight = newHeight / widthFactor;
                        newWidth = newWidth / float.Parse("1");
                        newHeight = newHeight / float.Parse("1");
                    }
                    else
                    {
                        newWidth = newWidth / heightFactor;
                        newHeight = newHeight / heightFactor;
                    }
                }
                y = 180;
                e.Graphics.DrawImage(img, 60, y, (int)newWidth, (int)newHeight);
                if (page26Pic2.Image != null)
                {
                    img = bc.ResizeImage(page26Pic2.Image, 320, 200);
                    newWidth = img.Width * 100 / img.HorizontalResolution;
                    newHeight = img.Height * 100 / img.VerticalResolution;
                    widthFactor = newWidth / e.MarginBounds.Width;
                    heightFactor = newHeight / e.MarginBounds.Height;
                    if (widthFactor > 1 | heightFactor > 1)
                    {
                        if (widthFactor > heightFactor)
                        {
                            widthFactor = 1;
                            //newWidth = newWidth / widthFactor;
                            //newHeight = newHeight / widthFactor;
                            newWidth = newWidth / float.Parse("1");
                            newHeight = newHeight / float.Parse("1");
                        }
                        else
                        {
                            newWidth = newWidth / heightFactor;
                            newHeight = newHeight / heightFactor;
                        }
                    }
                    e.Graphics.DrawImage(img, 60, 400, (int)newWidth, (int)newHeight);
                }
                if (page26Pic3.Image != null)
                {
                    img = bc.ResizeImage(page26Pic3.Image, 320, 200);
                    newWidth = img.Width * 100 / img.HorizontalResolution;
                    newHeight = img.Height * 100 / img.VerticalResolution;
                    widthFactor = newWidth / e.MarginBounds.Width;
                    heightFactor = newHeight / e.MarginBounds.Height;
                    if (widthFactor > 1 | heightFactor > 1)
                    {
                        if (widthFactor > heightFactor)
                        {
                            widthFactor = 1;
                            //newWidth = newWidth / widthFactor;
                            //newHeight = newHeight / widthFactor;
                            newWidth = newWidth / float.Parse("1");
                            newHeight = newHeight / float.Parse("1");
                        }
                        else
                        {
                            newWidth = newWidth / heightFactor;
                            newHeight = newHeight / heightFactor;
                        }
                    }
                    e.Graphics.DrawImage(img, 400, 180, (int)newWidth, (int)newHeight);
                }
                if (page26Pic4.Image != null)
                {
                    img = bc.ResizeImage(page26Pic4.Image, 320, 200);
                    newWidth = img.Width * 100 / img.HorizontalResolution;
                    newHeight = img.Height * 100 / img.VerticalResolution;
                    widthFactor = newWidth / e.MarginBounds.Width;
                    heightFactor = newHeight / e.MarginBounds.Height;
                    if (widthFactor > 1 | heightFactor > 1)
                    {
                        if (widthFactor > heightFactor)
                        {
                            widthFactor = 1;
                            //newWidth = newWidth / widthFactor;
                            //newHeight = newHeight / widthFactor;
                            newWidth = newWidth / float.Parse("1");
                            newHeight = newHeight / float.Parse("1");
                        }
                        else
                        {
                            newWidth = newWidth / heightFactor;
                            newHeight = newHeight / heightFactor;
                        }
                    }
                    e.Graphics.DrawImage(img, 400, 400, (int)newWidth, (int)newHeight);
                }
                if (page26Pic5.Image != null)
                {
                    img = bc.ResizeImage(page26Pic5.Image, 320, 200);
                    newWidth = img.Width * 100 / img.HorizontalResolution;
                    newHeight = img.Height * 100 / img.VerticalResolution;
                    widthFactor = newWidth / e.MarginBounds.Width;
                    heightFactor = newHeight / e.MarginBounds.Height;
                    if (widthFactor > 1 | heightFactor > 1)
                    {
                        if (widthFactor > heightFactor)
                        {
                            widthFactor = 1;
                            //newWidth = newWidth / widthFactor;
                            //newHeight = newHeight / widthFactor;
                            newWidth = newWidth / float.Parse("1");
                            newHeight = newHeight / float.Parse("1");
                        }
                        else
                        {
                            newWidth = newWidth / heightFactor;
                            newHeight = newHeight / heightFactor;
                        }
                    }
                    e.Graphics.DrawImage(img, 60, 620, (int)newWidth, (int)newHeight);
                }
                if (page26Pic6.Image != null)
                {
                    img = bc.ResizeImage(page26Pic6.Image, 320, 200);
                    newWidth = img.Width * 100 / img.HorizontalResolution;
                    newHeight = img.Height * 100 / img.VerticalResolution;
                    widthFactor = newWidth / e.MarginBounds.Width;
                    heightFactor = newHeight / e.MarginBounds.Height;
                    if (widthFactor > 1 | heightFactor > 1)
                    {
                        if (widthFactor > heightFactor)
                        {
                            widthFactor = 1;
                            //newWidth = newWidth / widthFactor;
                            //newHeight = newHeight / widthFactor;
                            newWidth = newWidth / float.Parse("1");
                            newHeight = newHeight / float.Parse("1");
                        }
                        else
                        {
                            newWidth = newWidth / heightFactor;
                            newHeight = newHeight / heightFactor;
                        }
                    }
                    e.Graphics.DrawImage(img, 400, 620, (int)newWidth, (int)newHeight);
                }
                e.Graphics.DrawString("Comment .....................................................................................................................", fEditB, Brushes.Black, 50, 860, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 900, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 940, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 980, flags);

                e.Graphics.DrawString("Physician Name ..........................................", fEditB, Brushes.Black, 430, 1100, flags);
                e.Graphics.DrawString("Print Date ..........................................", fEditB, Brushes.Black, 120, 1100, flags);
                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), fEditB, Brushes.Black, 200, 1096, flags);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "Document_PrintPage " + ex.Message);
                MessageBox.Show("error " + ex.Message, "");
            }
        }
        private void Btnpage3Prn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printPage3();
        }
        private void printPage3()
        {
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerA4;

            document.PrintPage += Document_PrintPage3;
            document.DefaultPageSettings.Landscape = false;

            document.Print();
        }

        private void Document_PrintPage3(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            String amt = "", line = null, date = "", txt = "";
            float yPos = 10, ydate = 0, gapline = 35;
            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);
            DateTime dtstart = new DateTime();

            //logo
            Image logoLeft;
            try
            {
                logoLeft = Resources.LOGO_BW_tran;
                float newWidth = logoLeft.Width * 100 / logoLeft.HorizontalResolution;
                float newHeight = logoLeft.Height * 100 / logoLeft.VerticalResolution;

                float widthFactor = 3.8F;
                float heightFactor = 3.8F;
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
                e.Graphics.DrawImage(logoLeft, recf);
                e.Graphics.DrawString(bc.iniC.hostname, fEditB, Brushes.Black, recf.Width + 20, 30, flags);
                e.Graphics.DrawString(bc.iniC.hostnamee, fEditB, Brushes.Black, recf.Width + 20, 60, flags);
                e.Graphics.DrawString(bc.iniC.hostaddresst, fEditS, Brushes.Black, 260, 30, flags);
                e.Graphics.DrawString(bc.iniC.hostaddresse, fEditS, Brushes.Black, 360, 60, flags);

                e.Graphics.DrawString("Colposcopy Report", famtB14, Brushes.Black, 300, 80, flags);

                e.Graphics.DrawString("Patient Name .......................................................................................  HN .........................", fEditB, Brushes.Black, 50, 140, flags);
                e.Graphics.DrawString(txtPttName.Text, fEditB, Brushes.Black, 170, 136, flags);
                e.Graphics.DrawString(txtHn.Text, fEditB, Brushes.Black, 660, 136, flags);
                Image img = null;
                if (page3Pic1.Image != null)
                {
                    img = bc.ResizeImage(page3Pic1.Image, 320, 200);
                    newWidth = img.Width * 100 / img.HorizontalResolution;
                    newHeight = img.Height * 100 / img.VerticalResolution;
                    widthFactor = newWidth / e.MarginBounds.Width;
                    heightFactor = newHeight / e.MarginBounds.Height;
                    if (widthFactor > 1 | heightFactor > 1)
                    {
                        if (widthFactor > heightFactor)
                        {
                            widthFactor = 1;
                            //newWidth = newWidth / widthFactor;
                            //newHeight = newHeight / widthFactor;
                            newWidth = newWidth / float.Parse("1");
                            newHeight = newHeight / float.Parse("1");
                        }
                        else
                        {
                            newWidth = newWidth / heightFactor;
                            newHeight = newHeight / heightFactor;
                        }
                    }
                    e.Graphics.DrawImage(img, 60, 180, (int)newWidth, (int)newHeight);
                }

                if (page3Pic2.Image != null)
                {
                    img = bc.ResizeImage(page3Pic2.Image, 320, 200);
                    newWidth = img.Width * 100 / img.HorizontalResolution;
                    newHeight = img.Height * 100 / img.VerticalResolution;
                    widthFactor = newWidth / e.MarginBounds.Width;
                    heightFactor = newHeight / e.MarginBounds.Height;
                    if (widthFactor > 1 | heightFactor > 1)
                    {
                        if (widthFactor > heightFactor)
                        {
                            widthFactor = 1;
                            //newWidth = newWidth / widthFactor;
                            //newHeight = newHeight / widthFactor;
                            newWidth = newWidth / float.Parse("1");
                            newHeight = newHeight / float.Parse("1");
                        }
                        else
                        {
                            newWidth = newWidth / heightFactor;
                            newHeight = newHeight / heightFactor;
                        }
                    }
                    e.Graphics.DrawImage(img, 60, 400, (int)newWidth, (int)newHeight);
                }

                if (page3Pic3.Image != null)
                {
                    img = bc.ResizeImage(page3Pic3.Image, 320, 200);
                    newWidth = img.Width * 100 / img.HorizontalResolution;
                    newHeight = img.Height * 100 / img.VerticalResolution;
                    widthFactor = newWidth / e.MarginBounds.Width;
                    heightFactor = newHeight / e.MarginBounds.Height;
                    if (widthFactor > 1 | heightFactor > 1)
                    {
                        if (widthFactor > heightFactor)
                        {
                            widthFactor = 1;
                            //newWidth = newWidth / widthFactor;
                            //newHeight = newHeight / widthFactor;
                            newWidth = newWidth / float.Parse("1");
                            newHeight = newHeight / float.Parse("1");
                        }
                        else
                        {
                            newWidth = newWidth / heightFactor;
                            newHeight = newHeight / heightFactor;
                        }
                    }
                    e.Graphics.DrawImage(img, 60, 620, (int)newWidth, (int)newHeight);
                }
                if (page3Pic4.Image != null)
                {
                    img = bc.ResizeImage(page3Pic4.Image, 320, 200);
                    newWidth = img.Width * 100 / img.HorizontalResolution;
                    newHeight = img.Height * 100 / img.VerticalResolution;
                    widthFactor = newWidth / e.MarginBounds.Width;
                    heightFactor = newHeight / e.MarginBounds.Height;
                    if (widthFactor > 1 | heightFactor > 1)
                    {
                        if (widthFactor > heightFactor)
                        {
                            widthFactor = 1;
                            //newWidth = newWidth / widthFactor;
                            //newHeight = newHeight / widthFactor;
                            newWidth = newWidth / float.Parse("1");
                            newHeight = newHeight / float.Parse("1");
                        }
                        else
                        {
                            newWidth = newWidth / heightFactor;
                            newHeight = newHeight / heightFactor;
                        }
                    }
                    e.Graphics.DrawImage(img, 60, 840, (int)newWidth, (int)newHeight);
                }
                

                e.Graphics.DrawString("Comment ........................................................", fEditB, Brushes.Black, 400, 200, flags);
                e.Graphics.DrawString("................................................................", fEditB, Brushes.Black, 420, 240, flags);
                e.Graphics.DrawString("................................................................", fEditB, Brushes.Black, 420, 280, flags);
                e.Graphics.DrawString("................................................................", fEditB, Brushes.Black, 420, 320, flags);

                e.Graphics.DrawString("Comment ........................................................", fEditB, Brushes.Black, 400, 410, flags);
                e.Graphics.DrawString("................................................................", fEditB, Brushes.Black, 420, 450, flags);
                e.Graphics.DrawString("................................................................", fEditB, Brushes.Black, 420, 490, flags);
                e.Graphics.DrawString("................................................................", fEditB, Brushes.Black, 420, 530, flags);

                e.Graphics.DrawString("Comment ........................................................", fEditB, Brushes.Black, 400, 640, flags);
                e.Graphics.DrawString("................................................................", fEditB, Brushes.Black, 420, 680, flags);
                e.Graphics.DrawString("................................................................", fEditB, Brushes.Black, 420, 720, flags);
                e.Graphics.DrawString("................................................................", fEditB, Brushes.Black, 420, 760, flags);

                e.Graphics.DrawString("Comment ........................................................", fEditB, Brushes.Black, 400, 840, flags);
                e.Graphics.DrawString("................................................................", fEditB, Brushes.Black, 420, 880, flags);
                e.Graphics.DrawString("................................................................", fEditB, Brushes.Black, 420, 920, flags);
                e.Graphics.DrawString("................................................................", fEditB, Brushes.Black, 420, 960, flags);


                e.Graphics.DrawString("Physician Name ..........................................", fEditB, Brushes.Black, 450, 1020, flags);
                e.Graphics.DrawString("Print Date ..........................................", fEditB, Brushes.Black, 450, 1060, flags);
                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), fEditB, Brushes.Black, 540, 1056, flags);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "Document_PrintPage " + ex.Message);
                MessageBox.Show("error " + ex.Message, "");
            }
        }

        private void Btnpage2Prn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printPage2();
        }
        private void printPage2()
        {
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerA4;

            document.PrintPage += Document_PrintPage2;
            document.DefaultPageSettings.Landscape = false;

            document.Print();
        }

        private void Document_PrintPage2(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            String amt = "", line = null, date = "", txt = "";
            float yPos = 10, ydate = 0, gapline = 35;
            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);
            DateTime dtstart = new DateTime();
            int y = 0;
            //logo
            Image logoLeft;
            try
            {
                logoLeft = Resources.LOGO_BW_tran;
                float newWidth = logoLeft.Width * 100 / logoLeft.HorizontalResolution;
                float newHeight = logoLeft.Height * 100 / logoLeft.VerticalResolution;

                float widthFactor = 3.8F;
                float heightFactor = 3.8F;
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
                e.Graphics.DrawImage(logoLeft, recf);
                e.Graphics.DrawString(bc.iniC.hostname, fEditB, Brushes.Black, recf.Width + 20, 30, flags);
                e.Graphics.DrawString(bc.iniC.hostnamee, fEditB, Brushes.Black, recf.Width + 20, 60, flags);
                e.Graphics.DrawString(bc.iniC.hostaddresst, fEditS, Brushes.Black, 260, 30, flags);
                e.Graphics.DrawString(bc.iniC.hostaddresse, fEditS, Brushes.Black, 360, 60, flags);

                e.Graphics.DrawString("Colposcopy Report", famtB10, Brushes.Black, 300, 80, flags);

                e.Graphics.DrawString("Patient Name .......................................................................................  HN .........................", fEditB, Brushes.Black, 50, 140, flags);
                e.Graphics.DrawString(txtPttName.Text, fEditB, Brushes.Black, 170, 136, flags);
                e.Graphics.DrawString(txtHn.Text, fEditB, Brushes.Black, 660, 136, flags);
                Image img = bc.ResizeImage(page2Pic1.Image, 320, 200);
                newWidth = img.Width * 100 / img.HorizontalResolution;
                newHeight = img.Height * 100 / img.VerticalResolution;
                widthFactor = newWidth / e.MarginBounds.Width;
                heightFactor = newHeight / e.MarginBounds.Height;
                if (widthFactor > 1 | heightFactor > 1)
                {
                    if (widthFactor > heightFactor)
                    {
                        widthFactor = 1;
                        //newWidth = newWidth / widthFactor;
                        //newHeight = newHeight / widthFactor;
                        newWidth = newWidth / float.Parse("1");
                        newHeight = newHeight / float.Parse("1");
                    }
                    else
                    {
                        newWidth = newWidth / heightFactor;
                        newHeight = newHeight / heightFactor;
                    }
                }
                y = 180;
                e.Graphics.DrawImage(img, 60, y, (int)newWidth, (int)newHeight);
                if (page2Pic2.Image != null)
                {
                    img = bc.ResizeImage(page2Pic2.Image, 320, 200);
                    newWidth = img.Width * 100 / img.HorizontalResolution;
                    newHeight = img.Height * 100 / img.VerticalResolution;
                    widthFactor = newWidth / e.MarginBounds.Width;
                    heightFactor = newHeight / e.MarginBounds.Height;
                    if (widthFactor > 1 | heightFactor > 1)
                    {
                        if (widthFactor > heightFactor)
                        {
                            widthFactor = 1;
                            //newWidth = newWidth / widthFactor;
                            //newHeight = newHeight / widthFactor;
                            newWidth = newWidth / float.Parse("1");
                            newHeight = newHeight / float.Parse("1");
                        }
                        else
                        {
                            newWidth = newWidth / heightFactor;
                            newHeight = newHeight / heightFactor;
                        }
                    }
                    e.Graphics.DrawImage(img, 60, 400, (int)newWidth, (int)newHeight);
                }
                if (page2Pic3.Image != null)
                {
                    img = bc.ResizeImage(page2Pic3.Image, 320, 200);
                    newWidth = img.Width * 100 / img.HorizontalResolution;
                    newHeight = img.Height * 100 / img.VerticalResolution;
                    widthFactor = newWidth / e.MarginBounds.Width;
                    heightFactor = newHeight / e.MarginBounds.Height;
                    if (widthFactor > 1 | heightFactor > 1)
                    {
                        if (widthFactor > heightFactor)
                        {
                            widthFactor = 1;
                            //newWidth = newWidth / widthFactor;
                            //newHeight = newHeight / widthFactor;
                            newWidth = newWidth / float.Parse("1");
                            newHeight = newHeight / float.Parse("1");
                        }
                        else
                        {
                            newWidth = newWidth / heightFactor;
                            newHeight = newHeight / heightFactor;
                        }
                    }
                    e.Graphics.DrawImage(img, 400, 180, (int)newWidth, (int)newHeight);
                }
                if (page2Pic4.Image != null)
                {
                    img = bc.ResizeImage(page2Pic4.Image, 320, 200);
                    newWidth = img.Width * 100 / img.HorizontalResolution;
                    newHeight = img.Height * 100 / img.VerticalResolution;
                    widthFactor = newWidth / e.MarginBounds.Width;
                    heightFactor = newHeight / e.MarginBounds.Height;
                    if (widthFactor > 1 | heightFactor > 1)
                    {
                        if (widthFactor > heightFactor)
                        {
                            widthFactor = 1;
                            //newWidth = newWidth / widthFactor;
                            //newHeight = newHeight / widthFactor;
                            newWidth = newWidth / float.Parse("1");
                            newHeight = newHeight / float.Parse("1");
                        }
                        else
                        {
                            newWidth = newWidth / heightFactor;
                            newHeight = newHeight / heightFactor;
                        }
                    }
                    e.Graphics.DrawImage(img, 400, 400, (int)newWidth, (int)newHeight);
                }

                e.Graphics.DrawString("Comment .....................................................................................................................", fEditB, Brushes.Black, 50, 700, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 740, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 780, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 820, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 860, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 900, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 940, flags);

                e.Graphics.DrawString("Physician Name ..........................................", fEditB, Brushes.Black, 450, 1020, flags);
                e.Graphics.DrawString("Print Date ..........................................", fEditB, Brushes.Black, 450, 1060, flags);
                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), fEditB, Brushes.Black, 540, 1056, flags);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "Document_PrintPage " + ex.Message);
                MessageBox.Show("error " + ex.Message, "");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void Btnpage1Prn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printPage1();
        }
        private void printPage1()
        {
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerA4;

            document.PrintPage += Document_PrintPage;
            document.DefaultPageSettings.Landscape = false;

            document.Print();
        }

        private void Document_PrintPage(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            String amt = "", line = null, date = "", txt = "";
            float yPos = 10, ydate = 0, gapline = 35;
            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            Pen blackPen = new Pen(Color.Black, 1);
            Size proposedSize = new Size(100, 100);
            DateTime dtstart = new DateTime();

            //logo
            Image logoLeft;
            try
            {
                logoLeft = Resources.LOGO_BW_tran;
                float newWidth = logoLeft.Width * 100 / logoLeft.HorizontalResolution;
                float newHeight = logoLeft.Height * 100 / logoLeft.VerticalResolution;

                float widthFactor = 3.8F;
                float heightFactor = 3.8F;
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
                e.Graphics.DrawImage(logoLeft, recf);
                e.Graphics.DrawString(bc.iniC.hostname, fEditB, Brushes.Black, recf.Width + 20, 30, flags);
                e.Graphics.DrawString(bc.iniC.hostnamee, fEditB, Brushes.Black, recf.Width + 20, 60, flags);
                e.Graphics.DrawString(bc.iniC.hostaddresst, fEditS, Brushes.Black, 260, 30, flags);
                e.Graphics.DrawString( bc.iniC.hostaddresse, fEditS, Brushes.Black, 360, 60, flags);

                e.Graphics.DrawString("Colposcopy Report", famtB14, Brushes.Black, 300, 80, flags);

                e.Graphics.DrawString("Patient Name .......................................................................................  HN .........................", fEditB, Brushes.Black, 50, 140, flags);
                e.Graphics.DrawString(txtPttName.Text, fEditB, Brushes.Black, 170, 136, flags);
                e.Graphics.DrawString(txtHn.Text, fEditB, Brushes.Black, 660, 136, flags);
                Image img = bc.ResizeImage(page1Pic.Image,640,480);

                newWidth = img.Width * 100 / img.HorizontalResolution;
                newHeight = img.Height * 100 / img.VerticalResolution;

                widthFactor = newWidth / e.MarginBounds.Width;
                heightFactor = newHeight / e.MarginBounds.Height;

                if (widthFactor > 1 | heightFactor > 1)
                {
                    if (widthFactor > heightFactor)
                    {
                        widthFactor = 1;
                        //newWidth = newWidth / widthFactor;
                        //newHeight = newHeight / widthFactor;
                        newWidth = newWidth / float.Parse("1");
                        newHeight = newHeight / float.Parse("1");
                    }
                    else
                    {
                        newWidth = newWidth / heightFactor;
                        newHeight = newHeight / heightFactor;
                    }
                }
                e.Graphics.DrawImage(img, 60, 180, (int)newWidth, (int)newHeight);

                e.Graphics.DrawString("Comment .....................................................................................................................", fEditB, Brushes.Black, 50, 700, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 740, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 780, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 820, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 860, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 900, flags);
                e.Graphics.DrawString(".......................................................................................................................................", fEditB, Brushes.Black, 50, 940, flags);

                e.Graphics.DrawString("Physician Name ..........................................", fEditB, Brushes.Black, 450, 1020, flags);
                e.Graphics.DrawString("Print Date ..........................................", fEditB, Brushes.Black, 450, 1060, flags);
                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), fEditB, Brushes.Black, 540, 1056, flags);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "Document_PrintPage " + ex.Message);
                MessageBox.Show("error " + ex.Message, "");
            }
        }

        private void TxtDtrCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtDtrName.Value = bc.selectDoctorName(txtDtrCode.Text.Trim());
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmSearchHn frm = new FrmSearchHn(bc, FrmSearchHn.StatusConnection.host);
            frm.ShowDialog(this);
            String[] an = bc.sPtt.an.Split('/');
            
            txtHn.Value = bc.sPtt.Hn;
            txtPttName.Value = bc.sPtt.Name;
            txtVN.Value = bc.sPtt.vn;
            txtVisitDate.Value = bc.sPtt.visitDate;
            //txtPreNo.Value = bc.sPtt.preno;

            //txtAnDate.Value = bc.sPtt.anDate;
            //chkIPD.Checked = bc.sPtt.statusIPD.Equals("I") ? true : false;

            //if (chkIPD.Checked)
            //{
            //    txtVisitDate.Hide();
            //    txtAnDate.Show();
            //    label6.Text = "AN Date :";
            //}
            //else
            //{
            //    txtVisitDate.Show();
            //    txtAnDate.Hide();
            //    label6.Text = "Visit Date :";
            //}
        }

        private void initCompoment()
        {
            initPage1();
            initPage2();
            initPage3();
            initPage26();
            pnPage2Diag.Height = 600;
            pnPage26Diag.Height = 600;
        }
        private void initPage1()
        {
            page1Pic = new C1PictureBox();
            menuPage1Pic = new ContextMenu();
            frmPage1Diag = new FrmDoctorDiag1(bc, "Page1", "", "", "");
            
            page1Pic.Dock = DockStyle.Fill;
            page1Pic.BorderStyle = BorderStyle.FixedSingle;
            page1Pic.SizeMode = PictureBoxSizeMode.Zoom;

            MenuItem menuItemPage1Pic = new MenuItem();
            menuItemPage1Pic.Index = 0;
            menuItemPage1Pic.Text = "Brow รูป";
            menuItemPage1Pic.Click += MenuItemPage1Pic_Click;
            menuPage1Pic.MenuItems.Add(menuItemPage1Pic);

            frmPage1Diag.FormBorderStyle = FormBorderStyle.None;
            frmPage1Diag.TopLevel = false;
            frmPage1Diag.Dock = DockStyle.Fill;
            frmPage1Diag.AutoScroll = true;

            page1Pic.ContextMenu = menuPage1Pic;
            pnPage1Pic.Controls.Add(page1Pic);
            pnPage1Diag.Controls.Add(frmPage1Diag);

            frmPage1Diag.Show();
        }
        private void initPage2()
        {
            page2Pic1 = new C1PictureBox();
            page2Pic2 = new C1PictureBox();
            page2Pic3 = new C1PictureBox();
            page2Pic4 = new C1PictureBox();
            menuPage2Pic1 = new ContextMenu();
            menuPage2Pic2 = new ContextMenu();
            menuPage2Pic3 = new ContextMenu();
            menuPage2Pic4 = new ContextMenu();
            frmPage2Diag = new FrmDoctorDiag1(bc, "Page2", "", "", "");

            page2Pic1.Dock = DockStyle.Fill;
            page2Pic1.BorderStyle = BorderStyle.FixedSingle;
            page2Pic1.SizeMode = PictureBoxSizeMode.Zoom;

            page2Pic2.Dock = DockStyle.Fill;
            page2Pic2.BorderStyle = BorderStyle.FixedSingle;
            page2Pic2.SizeMode = PictureBoxSizeMode.Zoom;

            page2Pic3.Dock = DockStyle.Fill;
            page2Pic3.BorderStyle = BorderStyle.FixedSingle;
            page2Pic3.SizeMode = PictureBoxSizeMode.Zoom;

            page2Pic4.Dock = DockStyle.Fill;
            page2Pic4.BorderStyle = BorderStyle.FixedSingle;
            page2Pic4.SizeMode = PictureBoxSizeMode.Zoom;

            MenuItem menuItemPage2Pic1 = new MenuItem();
            menuItemPage2Pic1.Index = 0;
            menuItemPage2Pic1.Text = "Brow รูป";
            menuItemPage2Pic1.Click += MenuItemPage2Pic1_Click;
            menuPage2Pic1.MenuItems.Add(menuItemPage2Pic1);

            MenuItem menuItemPage2Pic2 = new MenuItem();
            menuItemPage2Pic2.Index = 0;
            menuItemPage2Pic2.Text = "Brow รูป";
            menuItemPage2Pic2.Click += MenuItemPage2Pic2_Click;
            menuPage2Pic2.MenuItems.Add(menuItemPage2Pic2);

            MenuItem menuItemPage2Pic3 = new MenuItem();
            menuItemPage2Pic3.Index = 0;
            menuItemPage2Pic3.Text = "Brow รูป";
            menuItemPage2Pic3.Click += MenuItemPage2Pic3_Click;
            menuPage2Pic3.MenuItems.Add(menuItemPage2Pic3);

            MenuItem menuItemPage2Pic4 = new MenuItem();
            menuItemPage2Pic4.Index = 0;
            menuItemPage2Pic4.Text = "Brow รูป";
            menuItemPage2Pic4.Click += MenuItemPage2Pic4_Click;
            menuPage2Pic4.MenuItems.Add(menuItemPage2Pic4);

            frmPage2Diag.FormBorderStyle = FormBorderStyle.None;
            frmPage2Diag.TopLevel = false;
            frmPage2Diag.Dock = DockStyle.Fill;
            frmPage2Diag.AutoScroll = true;

            page2Pic1.ContextMenu = menuPage2Pic1;
            pnPage2Pic1.Controls.Add(page2Pic1);
            
            page2Pic2.ContextMenu = menuPage2Pic2;
            pnPage2Pic2.Controls.Add(page2Pic2);

            page2Pic3.ContextMenu = menuPage2Pic3;
            pnPage2Pic3.Controls.Add(page2Pic3);

            page2Pic4.ContextMenu = menuPage2Pic4;
            pnPage2Pic4.Controls.Add(page2Pic4);

            pnPage2Diag.Controls.Add(frmPage2Diag);
            frmPage2Diag.Show();

        }
        private void initPage26()
        {
            page26Pic1 = new C1PictureBox();
            page26Pic2 = new C1PictureBox();
            page26Pic3 = new C1PictureBox();
            page26Pic4 = new C1PictureBox();
            page26Pic5 = new C1PictureBox();
            page26Pic6 = new C1PictureBox();
            menuPage26Pic1 = new ContextMenu();
            menuPage26Pic2 = new ContextMenu();
            menuPage26Pic3 = new ContextMenu();
            menuPage26Pic4 = new ContextMenu();
            menuPage26Pic5 = new ContextMenu();
            menuPage26Pic6 = new ContextMenu();
            frmPage26Diag = new FrmDoctorDiag1(bc, "Page26", "", "", "");

            page26Pic1.Dock = DockStyle.Fill;
            page26Pic1.BorderStyle = BorderStyle.FixedSingle;
            page26Pic1.SizeMode = PictureBoxSizeMode.Zoom;

            page26Pic2.Dock = DockStyle.Fill;
            page26Pic2.BorderStyle = BorderStyle.FixedSingle;
            page26Pic2.SizeMode = PictureBoxSizeMode.Zoom;

            page26Pic3.Dock = DockStyle.Fill;
            page26Pic3.BorderStyle = BorderStyle.FixedSingle;
            page26Pic3.SizeMode = PictureBoxSizeMode.Zoom;

            page26Pic4.Dock = DockStyle.Fill;
            page26Pic4.BorderStyle = BorderStyle.FixedSingle;
            page26Pic4.SizeMode = PictureBoxSizeMode.Zoom;

            page26Pic5.Dock = DockStyle.Fill;
            page26Pic5.BorderStyle = BorderStyle.FixedSingle;
            page26Pic5.SizeMode = PictureBoxSizeMode.Zoom;

            page26Pic6.Dock = DockStyle.Fill;
            page26Pic6.BorderStyle = BorderStyle.FixedSingle;
            page26Pic6.SizeMode = PictureBoxSizeMode.Zoom;

            MenuItem menuItemPage26Pic1 = new MenuItem();
            menuItemPage26Pic1.Index = 0;
            menuItemPage26Pic1.Text = "Brow รูป";
            menuItemPage26Pic1.Click += MenuItemPage26Pic1_Click;
            menuPage26Pic1.MenuItems.Add(menuItemPage26Pic1);

            MenuItem menuItemPage26Pic2 = new MenuItem();
            menuItemPage26Pic2.Index = 0;
            menuItemPage26Pic2.Text = "Brow รูป";
            menuItemPage26Pic2.Click += MenuItemPage26Pic2_Click;
            menuPage26Pic2.MenuItems.Add(menuItemPage26Pic2);

            MenuItem menuItemPage26Pic3 = new MenuItem();
            menuItemPage26Pic3.Index = 0;
            menuItemPage26Pic3.Text = "Brow รูป";
            menuItemPage26Pic3.Click += MenuItemPage26Pic3_Click;
            menuPage26Pic3.MenuItems.Add(menuItemPage26Pic3);

            MenuItem menuItemPage26Pic4 = new MenuItem();
            menuItemPage26Pic4.Index = 0;
            menuItemPage26Pic4.Text = "Brow รูป";
            menuItemPage26Pic4.Click += MenuItemPage26Pic4_Click;
            menuPage26Pic4.MenuItems.Add(menuItemPage26Pic4);

            MenuItem menuItemPage26Pic5 = new MenuItem();
            menuItemPage26Pic5.Index = 0;
            menuItemPage26Pic5.Text = "Brow รูป";
            menuItemPage26Pic5.Click += MenuItemPage26Pic5_Click;
            menuPage26Pic5.MenuItems.Add(menuItemPage26Pic5);

            MenuItem menuItemPage26Pic6 = new MenuItem();
            menuItemPage26Pic6.Index = 0;
            menuItemPage26Pic6.Text = "Brow รูป";
            menuItemPage26Pic6.Click += MenuItemPage26Pic6_Click;
            menuPage26Pic6.MenuItems.Add(menuItemPage26Pic6);

            frmPage26Diag.FormBorderStyle = FormBorderStyle.None;
            frmPage26Diag.TopLevel = false;
            frmPage26Diag.Dock = DockStyle.Fill;
            frmPage26Diag.AutoScroll = true;

            page26Pic1.ContextMenu = menuPage26Pic1;
            pnPage26Pic1.Controls.Add(page26Pic1);

            page26Pic2.ContextMenu = menuPage26Pic2;
            pnPage26Pic2.Controls.Add(page26Pic2);

            page26Pic3.ContextMenu = menuPage26Pic3;
            pnPage26Pic3.Controls.Add(page26Pic3);

            page26Pic4.ContextMenu = menuPage26Pic4;
            pnPage26Pic4.Controls.Add(page26Pic4);

            page26Pic5.ContextMenu = menuPage26Pic5;
            pnPage26Pic5.Controls.Add(page26Pic5);

            page26Pic6.ContextMenu = menuPage26Pic6;
            pnPage26Pic6.Controls.Add(page26Pic6);

            pnPage26Diag.Controls.Add(frmPage26Diag);
            frmPage26Diag.Show();
        }

        private void MenuItemPage26Pic6_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",                Title = "Browse Image File",                CheckFileExists = true,
                CheckPathExists = true,                DefaultExt = "jpg",                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,                RestoreDirectory = true,                ReadOnlyChecked = true,                ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK) page26Pic6.Load(openFileDialog1.FileName);
        }
        private void MenuItemPage26Pic5_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",                Title = "Browse Image File",                CheckFileExists = true,
                CheckPathExists = true,                DefaultExt = "jpg",                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,                RestoreDirectory = true,                ReadOnlyChecked = true,                ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK) page26Pic5.Load(openFileDialog1.FileName);
        }

        private void MenuItemPage26Pic4_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",                Title = "Browse Image File",                CheckFileExists = true,
                CheckPathExists = true,                DefaultExt = "jpg",                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,                RestoreDirectory = true,                ReadOnlyChecked = true,                ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK) page26Pic4.Load(openFileDialog1.FileName);
        }

        private void MenuItemPage26Pic3_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",                Title = "Browse Image File",                CheckFileExists = true,
                CheckPathExists = true,                DefaultExt = "jpg",                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,                RestoreDirectory = true,                ReadOnlyChecked = true,                ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK) page26Pic3.Load(openFileDialog1.FileName);
        }

        private void MenuItemPage26Pic2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",                Title = "Browse Image File",                CheckFileExists = true,
                CheckPathExists = true,                DefaultExt = "jpg",                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,                RestoreDirectory = true,                ReadOnlyChecked = true,                ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK) page26Pic2.Load(openFileDialog1.FileName);
        }

        private void MenuItemPage26Pic1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",                Title = "Browse Image File",                CheckFileExists = true,
                CheckPathExists = true,                DefaultExt = "jpg",                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,                RestoreDirectory = true,                ReadOnlyChecked = true,                ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)                page26Pic1.Load(openFileDialog1.FileName);
        }

        private void initPage3()
        {
            page3Pic1 = new C1PictureBox();
            page3Pic2 = new C1PictureBox();
            page3Pic3 = new C1PictureBox();
            page3Pic4 = new C1PictureBox();
            menuPage3Pic1 = new ContextMenu();
            menuPage3Pic2 = new ContextMenu();
            menuPage3Pic3 = new ContextMenu();
            menuPage3Pic4 = new ContextMenu();

            frmPage31Diag = new FrmDoctorDiag1(bc, "Page31", "", "", "");
            frmPage32Diag = new FrmDoctorDiag1(bc, "Page32", "", "", "");
            frmPage33Diag = new FrmDoctorDiag1(bc, "Page33", "", "", "");
            frmPage34Diag = new FrmDoctorDiag1(bc, "Page34", "", "", "");

            page3Pic1.Dock = DockStyle.Fill;
            page3Pic1.BorderStyle = BorderStyle.FixedSingle;
            page3Pic1.SizeMode = PictureBoxSizeMode.Zoom;

            page3Pic2.Dock = DockStyle.Fill;
            page3Pic2.BorderStyle = BorderStyle.FixedSingle;
            page3Pic2.SizeMode = PictureBoxSizeMode.Zoom;

            page3Pic3.Dock = DockStyle.Fill;
            page3Pic3.BorderStyle = BorderStyle.FixedSingle;
            page3Pic3.SizeMode = PictureBoxSizeMode.Zoom;

            page3Pic4.Dock = DockStyle.Fill;
            page3Pic4.BorderStyle = BorderStyle.FixedSingle;
            page3Pic4.SizeMode = PictureBoxSizeMode.Zoom;

            MenuItem menuItemPage3Pic1 = new MenuItem();
            menuItemPage3Pic1.Index = 0;
            menuItemPage3Pic1.Text = "Brow รูป";
            menuItemPage3Pic1.Click += MenuItemPage3Pic1_Click;
            menuPage3Pic1.MenuItems.Add(menuItemPage3Pic1);

            MenuItem menuItemPage3Pic2 = new MenuItem();
            menuItemPage3Pic2.Index = 0;
            menuItemPage3Pic2.Text = "Brow รูป";
            menuItemPage3Pic2.Click += MenuItemPage3Pic2_Click;
            menuPage3Pic2.MenuItems.Add(menuItemPage3Pic2);

            MenuItem menuItemPage3Pic3 = new MenuItem();
            menuItemPage3Pic3.Index = 0;
            menuItemPage3Pic3.Text = "Brow รูป";
            menuItemPage3Pic3.Click += MenuItemPage3Pic3_Click;
            menuPage3Pic3.MenuItems.Add(menuItemPage3Pic3);

            MenuItem menuItemPage3Pic4 = new MenuItem();
            menuItemPage3Pic4.Index = 0;
            menuItemPage3Pic4.Text = "Brow รูป";
            menuItemPage3Pic4.Click += MenuItemPage3Pic4_Click;
            menuPage3Pic4.MenuItems.Add(menuItemPage3Pic4);

            frmPage31Diag.FormBorderStyle = FormBorderStyle.None;
            frmPage31Diag.TopLevel = false;
            frmPage31Diag.Dock = DockStyle.Fill;
            frmPage31Diag.AutoScroll = true;

            frmPage32Diag.FormBorderStyle = FormBorderStyle.None;
            frmPage32Diag.TopLevel = false;
            frmPage32Diag.Dock = DockStyle.Fill;
            frmPage32Diag.AutoScroll = true;

            frmPage33Diag.FormBorderStyle = FormBorderStyle.None;
            frmPage33Diag.TopLevel = false;
            frmPage33Diag.Dock = DockStyle.Fill;
            frmPage33Diag.AutoScroll = true;

            frmPage34Diag.FormBorderStyle = FormBorderStyle.None;
            frmPage34Diag.TopLevel = false;
            frmPage34Diag.Dock = DockStyle.Fill;
            frmPage34Diag.AutoScroll = true;

            page3Pic1.ContextMenu = menuPage3Pic1;
            pnPage31Pic.Controls.Add(page3Pic1);

            page3Pic2.ContextMenu = menuPage3Pic2;
            pnPage32Pic.Controls.Add(page3Pic2);

            page3Pic3.ContextMenu = menuPage3Pic3;
            pnPage33Pic.Controls.Add(page3Pic3);

            page3Pic4.ContextMenu = menuPage3Pic4;
            pnPage34Pic.Controls.Add(page3Pic4);

            pnPage31Diag.Controls.Add(frmPage31Diag);
            frmPage31Diag.Show();

            pnPage32Diag.Controls.Add(frmPage32Diag);
            frmPage32Diag.Show();

            pnPage33Diag.Controls.Add(frmPage33Diag);
            frmPage33Diag.Show();

            pnPage34Diag.Controls.Add(frmPage34Diag);
            frmPage34Diag.Show();
        }

        private void MenuItemPage3Pic4_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Image File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "jpg",
                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                page3Pic4.Load(openFileDialog1.FileName);
            }
        }

        private void MenuItemPage3Pic3_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Image File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "jpg",
                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                page3Pic3.Load(openFileDialog1.FileName);
            }
        }

        private void MenuItemPage3Pic2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Image File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "jpg",
                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                page3Pic2.Load(openFileDialog1.FileName);
            }
        }

        private void MenuItemPage3Pic1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Image File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "jpg",
                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                page3Pic1.Load(openFileDialog1.FileName);
            }
        }

        private void MenuItemPage2Pic4_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Image File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "jpg",
                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                page2Pic4.Load(openFileDialog1.FileName);
            }
        }

        private void MenuItemPage2Pic3_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Image File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "jpg",
                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                page2Pic3.Load(openFileDialog1.FileName);
            }
        }

        private void MenuItemPage2Pic2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Image File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "jpg",
                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                page2Pic2.Load(openFileDialog1.FileName);
            }
        }

        private void MenuItemPage2Pic1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Image File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "jpg",
                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                page2Pic1.Load(openFileDialog1.FileName);
            }
        }

        private void MenuItemPage1Pic_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Image File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "jpg",
                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                page1Pic.Load(openFileDialog1.FileName);
            }
        }

        private void FrmColposcopyReport_Load(object sender, EventArgs e)
        {
            c1DockingTab1.SelectedTab = c1DockingTabPage1;
            this.Text = "last Update 2021-06-17,2024-06-25-1 bug";
        }
    }
}
