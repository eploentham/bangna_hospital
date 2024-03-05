using bangna_hospital.control;
using bangna_hospital.object1;
using C1.C1Pdf;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.C1Document;
using System.Net;
using C1.Win.FlexViewer;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Linq;
using GrapeCity.ActiveReports.Extensibility.Drawing;

namespace bangna_hospital.gui
{
    public partial class FrmMedScanExport : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1ThemeController theme1;
        Label lbLoading;
        C1FlexGrid grfApm, grfRpt, grfOutLab, grfIPD, grfIPDScan;
        Label lbDocAll, lbDocGrp1, lbDocGrp2, lbDocGrp3, lbDocGrp4, lbDocGrp5, lbDocGrp6, lbDocGrp7, lbDocGrp8, lbDocGrp9;
        Color colorLbDoc;
        Stream streamPrint;
        C1FlexViewer fvCerti, fvTodayOutLab;
        C1PictureBox pic;
        listStream strm;
        String RPTNAME = "";

        DataTable DTRPT;
        int originalHeight = 0, newHeight = 720, mouseWheel = 0;
        int colgrfOutLabDscHN = 1, colgrfOutLabDscPttName = 2, colgrfOutLabDscVsDate = 3, colgrfOutLabDscVN = 4, colgrfOutLabDscId = 5, colgrfOutLablabcode = 6, colgrfOutLablabname = 7, colgrfOutLabApmDate = 8, colgrfOutLabApmDesc = 9, colgrfOutLabApmDtr = 10, colgrfOutLabApmReqNo = 11, colgrfOutLabApmReqDate = 12;
        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
        int colIPDDate = 1, colIPDDept = 2, colIPDAnShow = 4, colIPDStatus = 3, colIPDPreno = 5, colIPDVn = 6, colIPDAndate = 7, colIPDAnYr = 8, colIPDAn = 9, colIPDDtrName = 10;
        int colVsVsDate = 1, colVsDept = 2, colVsVn = 3, colVsStatus = 4, colVsPreno = 5, colVsAn = 6, colVsAndate = 7, colVsPaidType = 8, colVsHigh = 9, colVsWeight = 10, colVsTemp = 11, colVscc = 12, colVsccin = 13, colVsccex = 14, colVsabc = 15, colVshc16 = 16, colVsbp1r = 17, colVsbp1l = 18, colVsbp2r = 19, colVsbp2l = 20, colVsVital = 21, colVsPres = 22, colVsRadios = 23, colVsBreath = 24, colVsVsDate1 = 25, colVsDtrName = 26;
        String DOCGRPID = "", DSCID="", tC1Active = "", HNmedscan = "";
        Patient PTT;
        Visit VS;
        List<listStream> lStream;
        Image resizedImage, IMG;
        Boolean pageLoad = false, tabMedScanActiveNOtabOutLabActive = true;
        Form frmImg;
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmMedScanExport(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            theme1 = new C1ThemeController();
            initFont();
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);
            lStream = new List<listStream>();

            fvCerti = new C1FlexViewer();
            fvCerti.AutoScrollMargin = new System.Drawing.Size(0, 0);
            fvCerti.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            fvCerti.Dock = System.Windows.Forms.DockStyle.Fill;
            fvCerti.Location = new System.Drawing.Point(0, 0);
            fvCerti.Name = "fvCerti";
            fvCerti.Size = new System.Drawing.Size(1065, 790);
            fvCerti.TabIndex = 0;
            fvCerti.Ribbon.Minimized = true;
            spOutLabView.Controls.Add(fvCerti);
            imgScan1.Visible = false;           imgScan2.Visible = false;           imgScan3.Visible = false;           imgScan4.Visible = false;           imgScan5.Visible = false;
            imgScan6.Visible = false;           imgScan7.Visible = false;           imgScan8.Visible = false;           imgScan9.Visible = false;           imgScan10.Visible = false;
            imgScan11.Visible = false;          imgScan12.Visible = false;          imgScan13.Visible = false;          imgScan14.Visible = false;          imgScan15.Visible = false;

            imgLab1.Visible = false;            imgLab2.Visible = false;            imgLab3.Visible = false;            imgLab4.Visible = false;            imgLab5.Visible = false;
            imgLab6.Visible = false;            imgLab7.Visible = false;            imgLab8.Visible = false;            imgLab9.Visible = false;            imgLab10.Visible = false;
            imgLab11.Visible = false;           imgLab12.Visible = false;           imgLab13.Visible = false;           imgLab14.Visible = false;           imgLab15.Visible = false;

            imgXray1.Visible = false;           imgXray2.Visible = false;            imgXray3.Visible = false;         imgXray4.Visible = false;        imgXray5.Visible = false;
            imgXray6.Visible = false;           imgXray7.Visible = false;            imgXray8.Visible = false;         imgXray9.Visible = false;        imgXray10.Visible = false;
            imgXray11.Visible = false;          imgXray12.Visible = false;        imgXray13.Visible = false;      imgXray14.Visible = false;        imgXray15.Visible = false;
            bc.bcDB.pttDB.setCboDeptOPDNew(cboRpt2, "");
            initGrfRpt();
            initRptView();
            initGrfOutLab();
            initGrfIPD();
            initGrfIPDScan();
            setEvent();
            pageLoad = false;
        }
        private void initGrfIPDScan()
        {
            Panel pnScanTop = new Panel();
            Panel pnScan = new Panel();

            pnScanTop.Dock = DockStyle.Top;
            pnScanTop.Height = 30;
            pnScan.Dock = DockStyle.Fill;

            grfIPDScan = new C1FlexGrid();
            grfIPDScan.Font = fEdit;
            grfIPDScan.Dock = System.Windows.Forms.DockStyle.Fill;
            grfIPDScan.Location = new System.Drawing.Point(0, 0);
            grfIPDScan.Rows[0].Visible = false;
            grfIPDScan.Cols[0].Visible = false;
            grfIPDScan.Rows.Count = 1;
            grfIPDScan.Name = "grfIPDScan";
            grfIPDScan.Cols.Count = 5;
            Column colpic1 = grfIPDScan.Cols[colPic1];
            colpic1.DataType = typeof(Image);
            Column colpic2 = grfIPDScan.Cols[colPic2];
            colpic2.DataType = typeof(String);
            Column colpic3 = grfIPDScan.Cols[colPic3];
            colpic3.DataType = typeof(Image);
            Column colpic4 = grfIPDScan.Cols[colPic4];
            colpic4.DataType = typeof(String);
            grfIPDScan.Cols[colPic1].Width = bc.grfScanWidth;
            grfIPDScan.Cols[colPic2].Width = bc.grfScanWidth;
            grfIPDScan.Cols[colPic3].Width = bc.grfScanWidth;
            grfIPDScan.Cols[colPic4].Width = bc.grfScanWidth;
            grfIPDScan.ShowCursor = true;
            grfIPDScan.Cols[colPic2].Visible = false;
            grfIPDScan.Cols[colPic3].Visible = true;
            grfIPDScan.Cols[colPic4].Visible = false;
            grfIPDScan.Cols[colPic1].AllowEditing = false;
            grfIPDScan.Cols[colPic3].AllowEditing = false;
            grfIPDScan.DoubleClick += GrfIPDScan_DoubleClick;
            lbDocAll = new Label();
            bc.setControlLabel(ref lbDocAll, fEditB, "All", "lbDocAll", 20, 5);
            lbDocAll.ForeColor = Color.Red;
            lbDocAll.Click += LbDocAll_Click;
            pnScanTop.Controls.Add(lbDocAll);
            int i = 0, width1 = 0;
            colorLbDoc = lbDocAll.ForeColor;
            if (bc.bcDB.dgsDB.lDgs.Count <= 0) bc.bcDB.dgsDB.getlDgs();
            foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            {
                i++;
                if (i == 1)
                {
                    lbDocGrp1 = new Label();
                    bc.setControlLabel(ref lbDocGrp1, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp1.Click += LbDocAll_Click;
                    lbDocGrp1.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp1);
                }
                else if (i == 2)
                {
                    lbDocGrp2 = new Label();
                    bc.setControlLabel(ref lbDocGrp2, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp2.Click += LbDocAll_Click;
                    lbDocGrp2.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp2);
                }
                else if (i == 3)
                {
                    lbDocGrp3 = new Label();
                    bc.setControlLabel(ref lbDocGrp3, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp3.Click += LbDocAll_Click;
                    lbDocGrp3.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp3);
                }
                else if (i == 4)
                {
                    lbDocGrp4 = new Label();
                    bc.setControlLabel(ref lbDocGrp4, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp4.Click += LbDocAll_Click;
                    lbDocGrp4.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp4);
                }
                else if (i == 5)
                {
                    lbDocGrp5 = new Label();
                    bc.setControlLabel(ref lbDocGrp5, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp5.Click += LbDocAll_Click;
                    lbDocGrp5.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp5);
                }
                else if (i == 6)
                {
                    lbDocGrp6 = new Label();
                    bc.setControlLabel(ref lbDocGrp6, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp6.Click += LbDocAll_Click;
                    lbDocGrp6.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp6);
                }
                else if (i == 7)
                {
                    lbDocGrp7 = new Label();
                    bc.setControlLabel(ref lbDocGrp7, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp7.Click += LbDocAll_Click;
                    lbDocGrp7.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp7);
                }
                else if (i == 8)
                {
                    lbDocGrp8 = new Label();
                    bc.setControlLabel(ref lbDocGrp8, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp8.Click += LbDocAll_Click;
                    lbDocGrp8.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp8);
                }
                else if (i == 9)
                {
                    lbDocGrp9 = new Label();
                    bc.setControlLabel(ref lbDocGrp9, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp9.Click += LbDocAll_Click;
                    lbDocGrp9.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp9);
                }
            }
            pnMedScan.Controls.Add(pnScan);
            pnMedScan.Controls.Add(pnScanTop);
            pnScan.Controls.Add(grfIPDScan);
            //initGrfPrn();
            //initGrfHn();
        }
        private void setForColorLbDocGrp(object sender)
        {
            lbDocAll.ForeColor = Color.Black;

            lbDocGrp1.ForeColor = Color.Black;
            lbDocGrp2.ForeColor = Color.Black;
            lbDocGrp3.ForeColor = Color.Black;
            lbDocGrp4.ForeColor = Color.Black;
            lbDocGrp5.ForeColor = Color.Black;
            lbDocGrp6.ForeColor = Color.Black;
            lbDocGrp7.ForeColor = Color.Black;
            lbDocGrp8.ForeColor = Color.Black;
            lbDocGrp9.ForeColor = Color.Black;
        }
        private void LbDocAll_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //grfIPDScan.ContextMenu.MenuItems.Clear();

            setForColorLbDocGrp(sender);
            ((Label)sender).ForeColor = Color.Red;
            if (((Label)sender).Name.Equals("lbDocAll"))
            {
                DOCGRPID = "1100000099";
            }
            else if (((Label)sender).Name.Equals("lbDocGrp1"))
            {
                DOCGRPID = "1100000000";//DISCHARGE
            }
            else if (((Label)sender).Name.Equals("lbDocGrp2"))
            {
                DOCGRPID = "1100000001";//ADMISSION
            }
            else if (((Label)sender).Name.Equals("lbDocGrp3"))
            {
                DOCGRPID = "1100000002";//ORDER
            }
            else if (((Label)sender).Name.Equals("lbDocGrp4"))
            {
                DOCGRPID = "1100000003";//OPERATIVE
            }
            else if (((Label)sender).Name.Equals("lbDocGrp5"))
            {
                DOCGRPID = "1100000004";//INVESTIGATION
            }
            else if (((Label)sender).Name.Equals("lbDocGrp6"))
            {
                DOCGRPID = "1100000005";//NURSE
            }
            else if (((Label)sender).Name.Equals("lbDocGrp7"))
            {
                DOCGRPID = "1100000006";//MEDICATION
            }
            else if (((Label)sender).Name.Equals("lbDocGrp8"))
            {
                DOCGRPID = "1100000007";//OTHER
            }
            else if (((Label)sender).Name.Equals("lbDocGrp9"))
            {
                DOCGRPID = "1100000008";//GRAPHIC SHEET
            }
            setGrfIPDScan();
        }
        private void GrfIPDScan_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colPic2] == null) return;
            if (((C1FlexGrid)sender).Row < 0) return;
            String id = "";
            ((C1FlexGrid)sender).AutoSizeCols();
            ((C1FlexGrid)sender).AutoSizeRows();
            if (((C1FlexGrid)sender).Col == 1)
            {
                id = grfIPDScan[grfIPDScan.Row, colPic2].ToString();
            }
            else
            {
                id = grfIPDScan[grfIPDScan.Row, colPic4] != null ? grfIPDScan[grfIPDScan.Row, colPic4].ToString() : "";
            }
            //id = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colPic2].ToString();
            if (id.Equals("")) return;
            DSCID = id;
            MemoryStream strm = null;
            foreach (listStream lstrmm in lStream)
            {
                if (lstrmm.id.Equals(id))
                {
                    strm = lstrmm.stream;
                    break;
                }
            }
            if (strm != null)
            {
                streamPrint = strm;
                IMG = Image.FromStream(strm);
                frmImg = new Form();
                FlowLayoutPanel pn = new FlowLayoutPanel();
                //vScroller = new VScrollBar();
                //vScroller.Height = frmImg.Height;
                //vScroller.Width = 15;
                //vScroller.Dock = DockStyle.Right;
                frmImg.WindowState = FormWindowState.Normal;
                frmImg.StartPosition = FormStartPosition.CenterScreen;
                frmImg.Size = new Size(1024, 764);
                frmImg.AutoScroll = true;
                pn.Dock = DockStyle.Fill;
                pn.AutoScroll = true;
                pic = new C1PictureBox();
                pic.Dock = DockStyle.Fill;
                pic.SizeMode = PictureBoxSizeMode.AutoSize;
                //int newWidth = 440;
                int originalWidth = 0;

                originalHeight = 0;
                originalWidth = IMG.Width;
                originalHeight = IMG.Height;
                //resizedImage = img.GetThumbnailImage(newWidth, (newWidth * img.Height) / originalWidth, null, IntPtr.Zero);
                resizedImage = IMG.GetThumbnailImage((newHeight * IMG.Width) / originalHeight, newHeight, null, IntPtr.Zero);
                pic.Image = resizedImage;
                frmImg.Controls.Add(pn);
                pn.Controls.Add(pic);
                //pn.Controls.Add(vScroller);
                ContextMenu menuGw = new ContextMenu();
                menuGw.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_print));

                mouseWheel = 0;
                pic.MouseWheel += Pic_MouseWheel;
                pic.ContextMenu = menuGw;
                //vScroller.Scroll += VScroller_Scroll;
                //pic.Paint += Pic_Paint;
                //vScroller.Hide();
                frmImg.ShowDialog(this);
            }
        }
        private void Pic_MouseWheel(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();

            int numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            if (e.Delta < 0)
            {
                newHeight += SystemInformation.MouseWheelScrollLines * 10;
                this.Text = e.Y.ToString();
            }
            else
            {
                newHeight -= SystemInformation.MouseWheelScrollLines * 10;
            }
            resizedImage = IMG.GetThumbnailImage((newHeight * IMG.Width) / originalHeight, newHeight, null, IntPtr.Zero);
            pic.Image = resizedImage;
        }
        private void ContextMenu_print(object sender, System.EventArgs e)
        {
            setGrfScanToPrint();
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
                System.Drawing.Image img = Image.FromStream(streamPrint);

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
        private void initGrfIPD()
        {
            grfIPD = new C1FlexGrid();
            grfIPD.Font = fEdit;
            grfIPD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfIPD.Location = new System.Drawing.Point(0, 0);
            grfIPD.Rows.Count = 1;
            grfIPD.Cols.Count = 11;

            grfIPD.Cols[colIPDDate].Width = 90;
            grfIPD.Cols[colIPDVn].Width = 80;
            grfIPD.Cols[colIPDDept].Width = 170;
            grfIPD.Cols[colIPDPreno].Width = 100;
            grfIPD.Cols[colIPDStatus].Width = 60;
            grfIPD.Cols[colIPDDtrName].Width = 180;
            grfIPD.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfIPD.Cols[colIPDDate].Caption = "Visit Date";
            grfIPD.Cols[colIPDVn].Caption = "VN";
            grfIPD.Cols[colIPDDept].Caption = "อาการ";
            grfIPD.Cols[colIPDAnShow].Caption = "AN";

            grfIPD.Cols[colIPDPreno].Visible = false;
            grfIPD.Cols[colIPDVn].Visible = true;
            grfIPD.Cols[colIPDAnShow].Visible = true;
            grfIPD.Cols[colIPDAndate].Visible = false;
            grfIPD.Cols[colIPDPreno].Visible = false;
            grfIPD.Cols[colIPDVn].Visible = false;
            grfIPD.Cols[colIPDStatus].Visible = false;
            grfIPD.Cols[colIPDAnYr].Visible = false;
            grfIPD.Cols[colIPDAn].Visible = false;
            //grfIPD.Rows[0].Visible = false;
            //grfIPD.Cols[0].Visible = false;
            grfIPD.Cols[colIPDDate].AllowEditing = false;
            grfIPD.Cols[colIPDVn].AllowEditing = false;
            grfIPD.Cols[colIPDDept].AllowEditing = false;
            grfIPD.Cols[colIPDPreno].AllowEditing = false;
            grfIPD.Cols[colIPDDtrName].AllowEditing = false;
            //FilterRow fr = new FilterRow(grfExpn);

            grfIPD.AfterRowColChange += GrfIPD_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            spMedScanIPD.Controls.Add(grfIPD);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfIPD, bc.iniC.themegrfIpd);
        }
        private void GrfIPD_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;

            String an = "", vsDate = "", preno = "";

            an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";
            vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
            preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";

            setGrfIPDScan();
            lfSbMessage.Text = an;
        }
        private void setGrfIPD()
        {
            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dt = bc.bcDB.vsDB.selectVisitIPDByHn5(txtSBSearchHN.Text);
            int i = 1, j = 1, row = grfIPD.Rows.Count;
            //txtVN.Value = dt.Rows.Count;
            //txtName.Value = "";
            //txt.Value = "";
            grfIPD.Rows.Count = 1;
            grfIPD.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfIPD.Rows[i];
                String status = "", vn = "";

                //status = row1["MNC_PAT_FLAG"] != null ? row1["MNC_PAT_FLAG"].ToString().Equals("O") ? "OPD" : "IPD" : "-";
                status = "IPD";
                vn = row1["MNC_VN_NO"].ToString() + "/" + row1["MNC_VN_SEQ"].ToString() + "(" + row1["MNC_VN_SUM"].ToString() + ")";
                rowa[colIPDDate] = bc.datetoShow1(row1["mnc_date"].ToString());
                rowa[colIPDVn] = vn;
                rowa[colIPDStatus] = status;
                rowa[colIPDPreno] = row1["mnc_pre_no"].ToString();
                rowa[colIPDDept] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colIPDAnShow] = row1["mnc_an_no"].ToString() + "/" + row1["mnc_an_yr"].ToString();
                rowa[colIPDAndate] = bc.datetoShow1(row1["mnc_ad_date"].ToString());
                rowa[colIPDAnYr] = row1["mnc_an_yr"].ToString();
                rowa[colIPDAn] = row1["mnc_an_no"].ToString();
                rowa[colIPDDtrName] = row1["dtr_name"].ToString();
                i++;
            }
        }
        private void setGrfIPDScan()
        {
            showLbLoading();
            lStream.Clear();
            String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "", anyr = "", vn1 = "";
            DataTable dtOrder = new DataTable();
            GC.Collect();
            DataTable dt = new DataTable();
            statusOPD = grfIPD[grfIPD.Row, colIPDStatus] != null ? grfIPD[grfIPD.Row, colIPDStatus].ToString() : "";
            preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
            vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";

            an = grfIPD[grfIPD.Row, colIPDAn] != null ? grfIPD[grfIPD.Row, colIPDAn].ToString() : "";
            anDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
            anyr = grfIPD[grfIPD.Row, colIPDAnYr] != null ? grfIPD[grfIPD.Row, colIPDAnYr].ToString() : "";
            //label2.Text = "AN :";
            an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";

            vsDate = bc.datetoDB(vsDate);
            //setStaffNote(vsDate, preno);
            dt = bc.bcDB.dscDB.selectByAn(txtSBSearchHN.Text, an);
            grfIPDScan.Rows.Count = 0;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    int cnt = 0;
                    cnt = dt.Rows.Count / 2;
                    grfIPDScan.Rows.Count = cnt + 1;
                    FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                    Boolean findTrue = false;
                    int colcnt = 0, rowrun = -1;
                    StringBuilder sbdscid = new StringBuilder();
                    StringBuilder sbfilename = new StringBuilder();
                    StringBuilder sbftphost = new StringBuilder();
                    StringBuilder sbfolderftp = new StringBuilder();
                    foreach (DataRow row1 in dt.Rows)
                    {
                        if (findTrue) break;
                        colcnt++;
                        sbdscid.Clear();
                        sbdscid.Append(row1[bc.bcDB.dscDB.dsc.doc_scan_id].ToString());
                        sbfilename.Clear();
                        sbfilename.Append(row1[bc.bcDB.dscDB.dsc.image_path].ToString());
                        sbftphost.Clear();
                        sbftphost.Append(row1[bc.bcDB.dscDB.dsc.host_ftp].ToString());
                        sbfolderftp.Clear();
                        sbfolderftp.Append(row1[bc.bcDB.dscDB.dsc.folder_ftp].ToString());
                        String err = "";
                        try
                        {
                            err = "00";
                            Row rowd;
                            if ((colcnt % 2) == 0)
                            {
                                rowd = grfIPDScan.Rows[rowrun];
                            }
                            else
                            {
                                rowrun++;
                                rowd = grfIPDScan.Rows[rowrun];
                                Application.DoEvents();
                            }
                            MemoryStream stream = ftpc.download4K(sbfolderftp.ToString() + "/" + sbfilename.ToString());
                            err = "01";
                            Image loadedImage = new Bitmap(stream);
                            int originalWidth = 0;
                            originalWidth = loadedImage.Width;
                            int newWidth = bc.imgScanWidth;
                            //Image resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
                            err = "05";
                            if ((colcnt % 2) == 0)
                            {
                                rowd[colPic3] = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);       // + 0001
                                err = "061";       // + 0001
                                rowd[colPic4] = sbdscid.ToString();       // + 0001
                                err = "071";       // + 0001
                            }
                            else
                            {
                                err = "051";       // + 0001
                                rowd[colPic1] = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);       // + 0001
                                err = "06";       // + 0001
                                rowd[colPic2] = sbdscid.ToString();       // + 0001
                                err = "07";       // + 0001
                            }
                            strm = new listStream();
                            strm.id = sbdscid.ToString();
                            strm.dgsid = row1[bc.bcDB.dscDB.dsc.doc_group_id].ToString();
                            strm.stream = stream;
                            lStream.Add(strm);

                            if (colcnt == 50) GC.Collect();
                            if (colcnt == 100) GC.Collect();
                        }
                        catch (Exception ex)
                        {
                            String aaa = ex.Message + " " + err;
                            new LogWriter("e", "FrmScanView1 SetGrfScan ex " + ex.Message + " " + err + " colcnt " + colcnt + " doc_scan_id " + sbdscid.ToString());
                        }
                    }
                    ftpc = null;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("" + ex.Message, "");
                    new LogWriter("e", "FrmScanView1 SetGrfScan if (dt.Rows.Count > 0) ex " + ex.Message);
                }
            }
            grfIPDScan.AutoSizeCols();
            grfIPDScan.AutoSizeRows();
            hideLbLoading();
        }
        private void initGrfOutLab()
        {
            grfOutLab = new C1FlexGrid();
            grfOutLab.Font = fEdit;
            grfOutLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOutLab.Location = new System.Drawing.Point(0, 0);
            grfOutLab.Rows.Count = 1;
            grfOutLab.Cols.Count = 8;

            grfOutLab.Cols[colgrfOutLabDscHN].Width = 80;
            grfOutLab.Cols[colgrfOutLabDscPttName].Width = 250;
            grfOutLab.Cols[colgrfOutLabDscVsDate].Width = 100;
            grfOutLab.Cols[colgrfOutLabDscVN].Width = 80;
            grfOutLab.Cols[colgrfOutLablabcode].Width = 80;
            grfOutLab.Cols[colgrfOutLablabname].Width = 250;

            grfOutLab.Cols[colgrfOutLabDscHN].Caption = "HN";
            grfOutLab.Cols[colgrfOutLabDscPttName].Caption = "Name";
            grfOutLab.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
            grfOutLab.Cols[colgrfOutLabDscVN].Caption = "VN";
            grfOutLab.Cols[colgrfOutLablabcode].Caption = "code";
            grfOutLab.Cols[colgrfOutLablabname].Caption = "lab name";

            grfOutLab.Cols[colgrfOutLabDscId].Visible = false;
            grfOutLab.Cols[colgrfOutLabDscHN].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLabDscPttName].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLabDscVN].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLablabcode].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLablabname].AllowEditing = false;
            grfOutLab.AfterRowColChange += GrfOutLab_AfterRowColChange;

            spOutLabList.Controls.Add(grfOutLab);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfOutLab, bc.iniC.themegrfOpd);
        }
        private void GrfOutLab_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            String dscid = "";
            try
            {
                fvCerti.DocumentSource = null;
                dscid = grfOutLab[grfOutLab.Row, colVsPreno] != null ? grfOutLab[grfOutLab.Row, colgrfOutLabDscId].ToString() : "";
                C1PdfDocumentSource pds = new C1PdfDocumentSource();
                DocScan dsc = new DocScan();
                dsc = bc.bcDB.dscDB.selectByPk(dscid);
                FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
                pds.LoadFromStream(streamCertiDtr);
                fvCerti.DocumentSource = pds;
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPD BtnApmSave_Click " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnApmSave_Click save  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
        }
        private void setGrfOutLab()
        {
            grfOutLab.Rows.Count = 1;
            fvCerti.DocumentSource = null;
            DataTable dt = new DataTable();
            dt = bc.bcDB.dscDB.selectOutLabByHn(txtSBSearchHN.Text.Trim());
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1, row = grfOutLab.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfOutLab.Rows.Add();
                String status = "", vn = "";
                rowa[colgrfOutLabDscHN] = row1["hn"].ToString();
                rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
                rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["date_req"].ToString());
                rowa[colgrfOutLabDscPttName] = row1["patient_fullname"].ToString();
                rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
            }
        }
        private void initRptView()
        {

        }
        private void initGrfRpt()
        {
            grfRpt = new C1FlexGrid();
            grfRpt.Font = fEdit;
            grfRpt.Dock = System.Windows.Forms.DockStyle.Fill;
            grfRpt.Location = new System.Drawing.Point(0, 0);
            grfRpt.Rows.Count = 1;
            grfRpt.Cols.Count = 2;
            grfRpt.Cols[1].Width = 300;

            grfRpt.ShowCursor = true;
            grfRpt.Cols[1].Caption = "HN";

            grfRpt.Cols[1].DataType = typeof(String);
            grfRpt.Cols[1].TextAlign = TextAlignEnum.LeftCenter;
            grfRpt.Cols[1].Visible = true;
            grfRpt.Cols[1].AllowEditing = false;
            grfRpt.Click += GrfRpt_Click;
            //grfCheckUPList.AllowFiltering = true;
            grfRpt.Rows.Count = 2;
            Row rowa = grfRpt.Rows[1];
            rowa[1] = "รายงาน แพทย์นัด";
            rowa[0] = "01";
            pnRptName.Controls.Add(grfRpt);
            theme1.SetTheme(grfRpt, bc.iniC.themeApp);
        }

        private void GrfRpt_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfRpt.Rows.Count == 0) return;
            if (grfRpt.Col == 1)//ให้เป็น รายงานตัวที่ 5 ไปก่อน  เพราะทำรายงานตัวนี้ก่อน
            {
                RPTNAME = "01";
                setControlRpt01();
            }
        }
        private void setControlRpt01()
        {
            pnRptCriDate.Visible = true;
            lbRptStartDate.Visible = true;
            lbRptEndDate.Visible = true;
            txtRptStartDate.Visible = true;
            txtRptEndDate.Visible = true;
            btnRpt1.Visible = true;
            cboRpt1.Visible = true;
            lbRpt2.Visible = false;
            cboRpt2.Visible = false;
        }
        private void setEvent()
        {
            btnRptPrint.Click += BtnRptPrint_Click;
            btnRpt1.Click += BtnRpt1_Click;
            btnSBSearch.Click += BtnSBSearch_Click;
            tC1.SelectedTabChanged += TC1_SelectedTabChanged;

            txtHn1.KeyUp += TxtHn1_KeyUp;
            txtHn2.KeyUp += TxtHn1_KeyUp;
            txtHn3.KeyUp += TxtHn1_KeyUp;
            txtHn4.KeyUp += TxtHn1_KeyUp;
            txtHn5.KeyUp += TxtHn1_KeyUp;

            txtHn6.KeyUp += TxtHn1_KeyUp;
            txtHn7.KeyUp += TxtHn1_KeyUp;
            txtHn8.KeyUp += TxtHn1_KeyUp;
            txtHn9.KeyUp += TxtHn1_KeyUp;
            txtHn10.KeyUp += TxtHn1_KeyUp;

            txtHn12.KeyUp += TxtHn1_KeyUp;
            txtHn13.KeyUp += TxtHn1_KeyUp;
            txtHn14.KeyUp += TxtHn1_KeyUp;
            txtHn15.KeyUp += TxtHn1_KeyUp;
            txtHn11.KeyUp += TxtHn1_KeyUp;

            lbPttName1.Click += LbPttName1_Click;
            lbPttName2.Click += LbPttName1_Click;
            lbPttName3.Click += LbPttName1_Click;
            lbPttName4.Click += LbPttName1_Click;
            lbPttName5.Click += LbPttName1_Click;

            lbPttName6.Click += LbPttName1_Click;
            lbPttName7.Click += LbPttName1_Click;
            lbPttName8.Click += LbPttName1_Click;
            lbPttName9.Click += LbPttName1_Click;
            lbPttName10.Click += LbPttName1_Click;

            lbPttName11.Click += LbPttName1_Click;
            lbPttName12.Click += LbPttName1_Click;
            lbPttName13.Click += LbPttName1_Click;
            lbPttName14.Click += LbPttName1_Click;
            lbPttName15.Click += LbPttName1_Click;

            btnExp1.Click += BtnExp1_Click;
            btnExp2.Click += BtnExp1_Click;
            btnExp3.Click += BtnExp1_Click;
            btnExp4.Click += BtnExp1_Click;
            btnExp5.Click += BtnExp1_Click;

            btnExp6.Click += BtnExp1_Click;
            btnExp7.Click += BtnExp1_Click;
            btnExp8.Click += BtnExp1_Click;
            btnExp9.Click += BtnExp1_Click;
            btnExp10.Click += BtnExp1_Click;

            btnExp11.Click += BtnExp1_Click;
            btnExp12.Click += BtnExp1_Click;
            btnExp13.Click += BtnExp1_Click;
            btnExp14.Click += BtnExp1_Click;
            btnExp15.Click += BtnExp1_Click;

            btnExpAll.Click += BtnExpAll_Click;

            txtAn1.KeyUp += TxtAn1_KeyUp;
            txtAn2.KeyUp += TxtAn1_KeyUp;
            txtAn3.KeyUp += TxtAn1_KeyUp;
            txtAn4.KeyUp += TxtAn1_KeyUp;
            txtAn5.KeyUp += TxtAn1_KeyUp;

            txtAn6.KeyUp += TxtAn1_KeyUp;
            txtAn7.KeyUp += TxtAn1_KeyUp;
            txtAn8.KeyUp += TxtAn1_KeyUp;
            txtAn9.KeyUp += TxtAn1_KeyUp;
            txtAn10.KeyUp += TxtAn1_KeyUp;

            txtAn11.KeyUp += TxtAn1_KeyUp;
            txtAn12.KeyUp += TxtAn1_KeyUp;
            txtAn13.KeyUp += TxtAn1_KeyUp;
            txtAn14.KeyUp += TxtAn1_KeyUp;
            txtAn15.KeyUp += TxtAn1_KeyUp;
        }

        private void TxtAn1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode != Keys.Enter) return;
        }
        private void TC1_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tC1.SelectedTab == tabMedScan)
            {
                tabMedScanActiveNOtabOutLabActive = true;
                tC1Active = tabMedScan.Name;
                txtSBSearchHN.Visible = true;
                btnSBSearch.Visible = true;
            }
            else if (tC1.SelectedTab == tabSearch)
            {
                tabMedScanActiveNOtabOutLabActive = false;
                tC1Active = tabSearch.Name;
                txtSrcHn.Focus();
                txtSBSearchHN.Visible = false;
                btnSBSearch.Visible = false;
            }
            else if (tC1.SelectedTab == tabOutLab)//hn search
            {
                tabMedScanActiveNOtabOutLabActive = false;
                tC1Active = tabOutLab.Name;
                txtSBSearchHN.Visible = true;
                
                btnSBSearch.Visible = true;
            }
            else
            {
                txtSBSearchHN.Visible = false;
                btnSBSearch.Visible = false;
            }
        }
        private void BtnSBSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tC1Active.Equals(tabMedScan.Name))
            {
                HNmedscan = txtSBSearchHN.Text.Trim();
                Patient ptt = bc.bcDB.pttDB.selectPatinetByHn(txtSBSearchHN.Text);
                rb1.Text = ptt.Name;
                if (tabMedScanActiveNOtabOutLabActive)
                {
                    tC1.SelectedTab = tabMedScan;
                    setGrfIPD();
                }
                else
                {
                    tC1.SelectedTab = tabOutLab;
                    fvCerti.DocumentSource = null;
                    setGrfOutLab();
                }
            }
            else if (tC1Active.Equals(tabOutLab.Name))
            {
                setGrfOutLab();
            }
        }
        private void BtnRpt1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DateTime.TryParse(txtRptStartDate.Value.ToString(), out DateTime dtapmstart);//txtApmDate
            if (dtapmstart.Year < 1900)
            {
                dtapmstart.AddYears(543);
            }
            DateTime.TryParse(txtRptEndDate.Value.ToString(), out DateTime dtapmend);//txtApmDate
            if (dtapmend.Year < 1900)
            {
                dtapmend.AddYears(543);
            }
            bc.bcDB.pt07DB.setCboTumbonName(cboRpt1, dtapmstart.Year.ToString() + "-" + dtapmstart.ToString("MM-dd"), dtapmend.Year.ToString() + "-" + dtapmend.ToString("MM-dd"), "");
        }
        private void setReport01()
        {

        }
        private void BtnRptPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DateTime.TryParse(txtRptStartDate.Value.ToString(), out DateTime dtapmstart);//txtApmDate
            if (dtapmstart.Year < 1900)
            {
                dtapmstart.AddYears(543);
            }
            DateTime.TryParse(txtRptEndDate.Value.ToString(), out DateTime dtapmend);//txtApmDate
            if (dtapmend.Year < 1900)
            {
                dtapmend.AddYears(543);
            }
            String seccode = "", dtrcode = "", deptcode = "", deptname = "";
            seccode = cboRpt2.SelectedItem == null ? "" : ((ComboBoxItem)cboRpt2.SelectedItem).Value.ToString();
            deptcode = bc.bcDB.pm32DB.selectDeptOPDBySecNO(seccode);
            dtrcode = cboRpt1.SelectedItem == null ? "" : ((ComboBoxItem)cboRpt1.SelectedItem).Value.ToString();
            DTRPT = bc.bcDB.pt07DB.selectByDate1(dtapmstart.Year.ToString() + "-" + dtapmstart.ToString("MM-dd"), dtapmend.Year.ToString() + "-" + dtapmend.ToString("MM-dd"), dtrcode, deptcode, cboRpt2.Text, "");
            int i = 1;
            foreach (DataRow drow in DTRPT.Rows)
            {
                drow["row_number"] = i++;
                drow["apm_time"] = bc.showTime(drow["apm_time"].ToString());
                drow["apm_date"] = bc.datetoShow1(drow["apm_date"].ToString());
                drow["apmdept"] = drow["apmdept"].ToString().Replace("กุมารเวช", "").Replace("เวชปฎิบัติทั่วไป", "").Replace("ศัลยกรรมกระดูก", "").Replace("อายุรกรรมทั่วไป", "").Replace("อายุรกรรมโรคหัวใจ", "").Replace("สูตินารีเวช", "");//นพ. พิพัฒน์ชัย อรุณธรรมคุณ เวชปฎิบัติทั่วไป (OPD3)
                drow["apmmake"] = drow["apmmake"].ToString().Replace("กุมารเวช", "").Replace("เวชปฎิบัติทั่วไป", "").Replace("ศัลยกรรมกระดูก", "").Replace("อายุรกรรมทั่วไป", "").Replace("อายุรกรรมโรคหัวใจ", "").Replace("สูตินารีเวช", "");
                drow["paidname"] = drow["paidname"].ToString().Replace("ประกันสังคมอิสระ (บ.5)", "ปกต บ.5").Replace("ประกันสังคมอิสระ (บ.2)", "ปกต บ.2").Replace("ประกันสังคมอิสระ (บ.1)", "ปกต บ.1").Replace("ประกันสังคม (บ.5)", "ปกส บ.5").Replace("ประกันสังคม (บ.2)", "ปกส บ.2").Replace("ประกันสังคม (บ.1)", "ปกส บ.1");
                drow["dtrname"] = drow["dtrname"].ToString().Replace("นพ. พิพัฒน์ชัย อรุณธรรมคุณ", "นพ. พิพัฒน์ชัย อรุณธรรม.");
                drow["desc1"] = drow["desc1"].ToString().Replace("\r\n", ",");
            }
            FileInfo rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\appointment_date.rdlx");
            if (dtrcode.Length > 0)
            {
                //rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\appointment_date_doctor.rdlx");
                rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\appointment_date.rdlx");
            }
            if (!System.IO.File.Exists(rptPath.FullName))
            {
                lfSbMessage.Text = "File report not found";
                return;
            }
            PageReport definition = new PageReport(rptPath);
            PageDocument runtime = new GrapeCity.ActiveReports.Document.PageDocument(definition);

            runtime.Parameters["line1"].CurrentValue = bc.iniC.hostname;
            runtime.Parameters["line2"].CurrentValue = "รายงานแพทย์นัด ประจำวันที่ " + dtapmstart.ToString("dd-MM-yyyy") + " ถึงวันที่ " + dtapmend.ToString("dd-MM-yyyy");
            runtime.Parameters["line3"].CurrentValue = (dtrcode.Length > 0) ? "แพทย์ " + cboRpt1.Text : "";

            runtime.LocateDataSource += Runtime_LocateDataSource;
            arvMain.LoadDocument(runtime);
        }
        private void Runtime_LocateDataSource(object sender, LocateDataSourceEventArgs args)
        {
            //throw new NotImplementedException();
            args.Data = DTRPT;
        }
        private void BtnExpAll_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String hn = "", an = "", err = "", folderName = "";
            Boolean flagStaffnote = false, flagOutlab = false;
            if (chkExp1.Checked) { hn = txtHn1.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab1.Checked ? true : false, imgStaffNote1.Checked ? true : false); }
            if (chkExp2.Checked) { hn = txtHn2.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab2.Checked ? true : false, imgStaffNote2.Checked ? true : false); }
            if (chkExp3.Checked) { hn = txtHn3.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab3.Checked ? true : false, imgStaffNote3.Checked ? true : false); }
            if (chkExp4.Checked) { hn = txtHn4.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab4.Checked ? true : false, imgStaffNote4.Checked ? true : false); }
            if (chkExp5.Checked) { hn = txtHn5.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab5.Checked ? true : false, imgStaffNote5.Checked ? true : false); }

            if (chkExp6.Checked) { hn = txtHn6.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab6.Checked ? true : false, imgStaffNote6.Checked ? true : false); }
            if (chkExp7.Checked) { hn = txtHn7.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab7.Checked ? true : false, imgStaffNote7.Checked ? true : false); }
            if (chkExp8.Checked) { hn = txtHn8.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab8.Checked ? true : false, imgStaffNote8.Checked ? true : false); }
            if (chkExp9.Checked) { hn = txtHn9.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab9.Checked ? true : false, imgStaffNote9.Checked ? true : false); }
            if (chkExp10.Checked) { hn = txtHn10.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab10.Checked ? true : false, imgStaffNote10.Checked ? true : false); }

            if (chkExp11.Checked) { hn = txtHn11.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab11.Checked ? true : false, imgStaffNote11.Checked ? true : false); }
            if (chkExp12.Checked) { hn = txtHn12.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab12.Checked ? true : false, imgStaffNote12.Checked ? true : false); }
            if (chkExp13.Checked) { hn = txtHn13.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab13.Checked ? true : false, imgStaffNote13.Checked ? true : false); }
            if (chkExp14.Checked) { hn = txtHn14.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab14.Checked ? true : false, imgStaffNote14.Checked ? true : false); }
            if (chkExp15.Checked) { hn = txtHn15.Text.Trim(); an = txtAn1.Text.Trim(); genPDF(hn, an, imgOutlab15.Checked ? true : false, imgStaffNote15.Checked ? true : false); }
        }
        class listStream
        {
            public String id = "", dgsid = "";
            public MemoryStream stream;
        }
        private void genPDF(String hn, String an, Boolean flagOutlab, Boolean flagStaffnote)
        {
            List<listStream> lStream;
            lStream = new List<listStream>();
            String err = "", folderName = "";
            showLbLoading();
            lbLoading.BringToFront();
            try
            {
                lbLoading.Text = "กรุณารอซักครู่ ดึงข้อมูล FTP";
                FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                DataTable dtdsc = new DataTable();
                DataTable dtoutlab = new DataTable();
                Boolean findTrue = false;
                listStream strm;
                Patient ptt = new Patient();
                Image picL = null, picR = null;
                err = "00";
                ptt = bc.bcDB.pttDB.selectPatinetByHn(hn);
                if (!Directory.Exists(bc.iniC.pathDownloadFile))
                {
                    Directory.CreateDirectory(bc.iniC.pathDownloadFile);
                }
                folderName = hn + "_" + an.Replace(".", "_") + "_" + DateTime.Now.Ticks.ToString();
                if (!Directory.Exists(bc.iniC.pathDownloadFile + "\\" + folderName))
                {
                    Directory.CreateDirectory(bc.iniC.pathDownloadFile + "\\" + folderName);
                }
                else
                {
                    string[] files = Directory.GetFiles(bc.iniC.pathDownloadFile + "\\" + folderName);
                    foreach (string file in files)
                    {
                        System.IO.File.Delete(file);
                    }
                }
                Application.DoEvents();
                
                dtdsc = bc.bcDB.dscDB.selectByAn(hn, an.Replace(".", "/"));
                if (dtdsc.Rows.Count > 0)
                {
                    foreach (DataRow row1 in dtdsc.Rows)
                    {
                        MemoryStream streamCertiDtr = ftpc.download(row1[bc.bcDB.dscDB.dsc.folder_ftp].ToString() + "/" + row1[bc.bcDB.dscDB.dsc.image_path].ToString());
                        strm = new listStream();
                        strm.id = row1[bc.bcDB.dscDB.dsc.doc_scan_id].ToString();
                        strm.stream = streamCertiDtr;
                        lStream.Add(strm);
                    }
                }
                if (flagOutlab)
                {
                    dtoutlab = bc.bcDB.dscDB.selectOutLabByAn(hn, an.Replace(".", "/"));
                    if (dtoutlab.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dtoutlab.Rows)
                        {
                            MemoryStream streamCertiDtr = ftpc.download(row1[bc.bcDB.dscDB.dsc.folder_ftp].ToString() + "/" + row1[bc.bcDB.dscDB.dsc.image_path].ToString());
                            File.WriteAllBytes(bc.iniC.pathDownloadFile + "\\" + folderName + "\\" + hn + "_" + an.Replace(".", "-") + "_outlab" + row1[bc.bcDB.dscDB.dsc.doc_scan_id].ToString() + ".pdf", streamCertiDtr.ToArray());
                        }
                    }
                }
                //Stream streamDownload = null;
                //MemoryStream strm = null;
                C1PdfDocument _c1pdf = new C1.C1Pdf.C1PdfDocument();
                _c1pdf.DocumentInfo.Producer = "C1Pdf";
                _c1pdf.Security.AllowCopyContent = true;
                _c1pdf.Security.AllowEditAnnotations = true;
                _c1pdf.Security.AllowEditContent = true;
                _c1pdf.Security.AllowPrint = true;
                _c1pdf.SaveAllImagesAsJpeg = true;
                _c1pdf.DocumentInfo.Title = "bangna5";
                int i = 1, newWidth1 = 210;
                lbLoading.Text = "กรุณารอซักครู่ กำลังเตรียมข้อมูล ";
                StringBuilder sberr = new StringBuilder();
                err = "01";
                sberr.Append("01");
                if (flagStaffnote)
                {
                    String preno = "", vsdate = "", vn = "", preno1="",file = "", dd = "", mm = "", yy = "";
                    preno = dtdsc.Rows[0][8] !=null ? dtdsc.Rows[0][8].ToString():"";
                    vsdate = dtdsc.Rows[0][9] != null ? dtdsc.Rows[0][9].ToString() : "";
                    dd = vsdate.Substring(vsdate.Length - 2);
                    mm = vsdate.Substring(5, 2);
                    yy = vsdate.Substring(0, 4);
                    int.TryParse(yy, out int chk);
                    if (chk > 2500)
                        chk -= 543;
                    file = "\\\\" + bc.iniC.pathScanStaffNote + chk + "\\" + mm + "\\" + dd + "\\";
                    preno1 = "000000" + preno;
                    err = "03";
                    preno1 = preno1.Substring(preno1.Length - 6);
                    picL = Image.FromFile(file + preno1 + "R.JPG");
                    picR = Image.FromFile(file + preno1 + "S.JPG");
                    _c1pdf.DrawImage(bc.ResizeImagetoA42((Bitmap)picL, 600,800),  new RectangleF(5, 5, 600,800));
                    _c1pdf.NewPage();
                    _c1pdf.DrawImage(bc.ResizeImagetoA42((Bitmap)picR, 600, 800), new RectangleF(5, 5, 600, 800));
                    _c1pdf.NewPage();
                }
                foreach (listStream lstrmm in lStream)
                {
                    //MemoryStream strm = lstrmm.stream.CopyTo(strm);
                    //strm.
                    //strm.Seek(0, SeekOrigin.Begin);
                    //Application.DoEvents();
                    err = "02";
                    //sberr.("02");
                    //Thread.Sleep(100);
                    //streamDownload = lstrmm.stream;
                    //RectangleF rc;
                    //Image img = Image.FromStream(lstrmm.stream);
                    err = "03";
                    //Image img1 = bc.ResizeImagetoA41(Image.FromStream(lstrmm.stream), 900);
                    err = "04";
                    //rc = new RectangleF(10, 10, 590, 770);
                    //_c1pdf.DrawImage(img1, rc);
                    _c1pdf.DrawImage(bc.ResizeImagetoA41(Image.FromStream(lstrmm.stream), 900), new RectangleF(10, 10, 590, 770));
                    err = "05";
                    if (i < lStream.Count)
                        _c1pdf.NewPage();
                    //img.Save(bc.iniC.pathDownloadFile + "\\" + folderName + "\\" + txtHn.Text.Trim() + "_" + txtVN.Text.Trim().Replace("/", "-")+"_"+i + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    //img1.Save(bc.iniC.pathDownloadFile + "\\" + folderName + "\\" + txtHn.Text.Trim() + "_" + txtVN.Text.Trim().Replace("/", "-") + "_" + i + "_re.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    //resizedImage.Save(bc.iniC.pathDownloadFile + "\\" + folderName + "\\" + txtHn.Text.Trim() + "_" + txtVN.Text.Trim().Replace("/", "-") + "_" + i + "_re.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    using (var fileStream = new FileStream(bc.iniC.pathDownloadFile + "\\" + folderName + "\\" + hn + "_" + an.Replace("/", "-") + "_" + i + ".jpg", FileMode.Create, FileAccess.Write))
                    {
                        lstrmm.stream.Position = 0;
                        lstrmm.stream.CopyTo(fileStream);
                    }
                    //strm.Dispose();
                    err = "06";
                    //img.Dispose();
                    //img1.Dispose();
                    i++;
                    lbLoading.Text = "กรุณารอซักครู่ ... " + (i - 1) + "/" + lStream.Count;
                    if(i%9 == 0) Application.DoEvents();
                }
                DataTable dtLab = new DataTable();//LAB
                DataTable dtXray = new DataTable();//Xray
                String[] an1 = an.Split('.');
                if (an1.Length >= 2)
                {
                    dtLab = bc.bcDB.vsDB.selectLabbyAN(hn, an1[0], an1[1]);
                    dtXray = bc.bcDB.vsDB.selectResultXraybyAN(hn, an1[0], an1[1]);
                }
                if (dtLab.Rows.Count > 0)
                {
                    lbLoading.Text = "กรุณารอซักครู่ มี lab ... ";
                    foreach (DataRow drow in dtLab.Rows)
                    {
                        drow["patient_name"] = ptt.Name;
                        drow["patient_age"] = ptt.AgeStringOK1DOT();
                        drow["patient_hn"] = hn;
                        drow["patient_vn"] = an;
                        drow["patient_type"] = drow["MNC_FN_TYP_DSC"].ToString();
                        drow["request_no"] = drow["MNC_REQ_NO"].ToString() + "/" + bc.datetoShow(drow["mnc_req_dat"].ToString());
                        drow["doctor"] = drow["dtr_name"].ToString() + "[" + drow["mnc_dot_cd"].ToString() + "]";
                        //drow["result_date"] = bc.datetoShow(dtreq.Rows[0]["mnc_req_dat"].ToString());
                        drow["result_date"] = bc.datetoShow(drow["MNC_RESULT_DAT"].ToString()) + " " + bc.showTime(drow["MNC_RESULT_TIM"].ToString());
                        drow["print_date"] = bc.datetoShow(drow["MNC_STAMP_DAT"].ToString());
                        drow["user_lab"] = drow["user_lab"].ToString() + " [ทน." + drow["MNC_USR_NAME_result"].ToString() + "]";
                        drow["user_report"] = drow["user_report"].ToString() + " [ทน." + drow["MNC_USR_NAME_report"].ToString() + "]";
                        drow["user_check"] = drow["user_check"].ToString() + " [ทน." + drow["MNC_USR_NAME_approve"].ToString() + "]";
                    }
                    Application.DoEvents();
                    FrmReportNew frm = new FrmReportNew(bc, "lab_result_3");
                    frm.DT = dtLab;
                    //frm.ShowDialog(this);
                    //return;
                    frm.ExportReport(bc.iniC.pathDownloadFile + "\\" + folderName + "\\" + hn + "_" + an.Replace(".", "-") + "_lab.pdf");
                }
                //if (dtXray.Rows.Count > 0)
                //{
                //    lbLoading.Text = "กรุณารอซักครู่ มี xray ... ";
                //    foreach (DataRow drow in dtXray.Rows)
                //    {
                //        drow["patient_name"] = ptt.Name;
                //        drow["patient_age"] = ptt.AgeStringOK1DOT();
                //        drow["patient_hn"] = hn;
                //        drow["patient_vn"] = an;
                //        //drow["patient_type"] = drow["MNC_FN_TYP_DSC"].ToString();
                //        drow["request_no"] = drow["MNC_REQ_NO"].ToString() + "/" + bc.datetoShow(drow["mnc_req_dat"].ToString());
                //        drow["doctor"] = drow["dtr_name_result"].ToString() + "[" + drow["mnc_dot_df_cd"].ToString() + "]";
                //        //drow["result_date"] = bc.datetoShow(dtreq.Rows[0]["mnc_req_dat"].ToString());
                //        drow["result_date"] = bc.datetoShow(drow["MNC_STAMP_DAT"].ToString()) + " " + bc.showTime(drow["MNC_STAMP_TIM"].ToString());
                //        drow["print_date"] = bc.datetoShow(drow["MNC_STAMP_DAT"].ToString());
                //        drow["patient_dep"] = bc.bcDB.pm32DB.getDeptNameIPD(drow["MNC_REQ_DEP"].ToString());
                //        drow["sort1"] = drow["mnc_req_dat"].ToString().Replace("-", "").Replace("-", "") + drow["MNC_REQ_NO"].ToString();

                //        drow["patient_dep"] = bc.bcDB.pm32DB.getDeptNameIPD(drow["MNC_REQ_DEP"].ToString());
                //        FrmReportNew frm = new FrmReportNew(bc, "xray_result");
                //        frm.DT = dtXray;
                //        //frm.ShowDialog(this);
                //        //return;
                //        frm.ExportReport(bc.iniC.pathDownloadFile + "\\" + folderName + "\\" + hn + "_" + an.Replace(".", "-") + "_xray.pdf");
                //    }
                //}
                _c1pdf.DocumentInfo.Title = "bangna5";
                _c1pdf.ImageQuality = ImageQualityEnum.High;
                _c1pdf.Save(bc.iniC.pathDownloadFile + "\\" + folderName + "\\" + hn + "_" + an.Replace(".", "-") + ".pdf");
                //_c1pdf.Dispose();
                lbLoading.Text = "กำลัง export รวม File ... ";
                Application.DoEvents();
                System.Threading.Thread.Sleep(1000);

                if (System.IO.File.Exists(bc.iniC.pathDownloadFile + "\\" + folderName + "\\" + hn + "_" + an.Replace(".", "-") + "_lab.pdf"))
                {
                    String[] files;
                    if (flagOutlab)
                    {
                        List<string> listfileoutlab = new List<string>();
                        string[] filePaths = Directory.GetFiles(bc.iniC.pathDownloadFile + "\\" + folderName + "\\", "*.pdf", SearchOption.AllDirectories);
                        int ii = 0;
                        foreach (string filePath in filePaths)
                        {
                            if (filePath.IndexOf("outlab") > 0)
                            {
                                ii++;
                                listfileoutlab.Add(filePath);
                            }
                        }
                        files = new string[2 + listfileoutlab.Count];
                        ii = 2;
                        foreach (string filePath in listfileoutlab)
                        {
                            files[ii] = filePath;
                        }
                    }
                    else
                    {
                        files = new string[2];
                        files[0] = bc.iniC.pathDownloadFile + "\\" + folderName + "\\" + hn + "_" + an.Replace(".", "-") + ".pdf";
                        files[1] = bc.iniC.pathDownloadFile + "\\" + folderName + "\\" + hn + "_" + an.Replace(".", "-") + "_lab.pdf";
                    }
                    PdfControl pdfc = new PdfControl();
                    //pdfc.MergeFileslab(bc.iniC.pathDownloadFile + "\\" + folderName + "\\" + txtHn.Text.Trim() + "_" + txtVN.Text.Trim().Replace("/", "-") + "_all.pdf",files);
                    pdfc.MergeFileslab2(bc.iniC.pathDownloadFile + "\\" + folderName + "\\" + hn + "_" + an.Replace(".", "-") + "_all.pdf", files);
                    pdfc = null;
                }
                Process.Start("explorer.exe", bc.iniC.pathDownloadFile + "\\" + folderName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("error " + ex.Message, "err " + err);
            }
            hideLbLoading();
        }
        private void BtnExp1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String hn = "", an = "", err = "", folderName = "";
            Boolean flagStaffnote = false, flagOutlab = false, flagXray=false;
            
            if (((C1Button)sender).Name.Equals("btnExp1")) { hn = txtHn1.Text.Trim(); an = txtAn1.Text.Trim(); flagOutlab = imgOutlab1.Checked ? true : false; flagStaffnote = imgStaffNote1.Checked ? true : false; }
            else if (((C1Button)sender).Name.Equals("btnExp2")) { hn = txtHn2.Text.Trim(); an = txtAn2.Text.Trim(); flagOutlab = imgOutlab2.Checked ? true : false; flagStaffnote = imgStaffNote2.Checked ? true : false; }
            else if (((C1Button)sender).Name.Equals("btnExp3")) { hn = txtHn3.Text.Trim(); an = txtAn3.Text.Trim(); flagOutlab = imgOutlab3.Checked ? true : false; flagStaffnote = imgStaffNote3.Checked ? true : false; }
            else if (((C1Button)sender).Name.Equals("btnExp4")) { hn = txtHn4.Text.Trim(); an = txtAn4.Text.Trim(); flagOutlab = imgOutlab4.Checked ? true : false; flagStaffnote = imgStaffNote4.Checked ? true : false; }
            else if (((C1Button)sender).Name.Equals("btnExp5")) { hn = txtHn5.Text.Trim(); an = txtAn5.Text.Trim(); flagOutlab = imgOutlab5.Checked ? true : false; flagStaffnote = imgStaffNote5.Checked ? true : false; }

            else if (((C1Button)sender).Name.Equals("btnExp6")) { hn = txtHn6.Text.Trim(); an = txtAn6.Text.Trim(); flagOutlab = imgOutlab6.Checked ? true : false; flagStaffnote = imgStaffNote6.Checked ? true : false; }
            else if (((C1Button)sender).Name.Equals("btnExp7")) { hn = txtHn7.Text.Trim(); an = txtAn7.Text.Trim(); flagOutlab = imgOutlab7.Checked ? true : false; flagStaffnote = imgStaffNote7.Checked ? true : false; }
            else if (((C1Button)sender).Name.Equals("btnExp8")) { hn = txtHn8.Text.Trim(); an = txtAn8.Text.Trim(); flagOutlab = imgOutlab8.Checked ? true : false; flagStaffnote = imgStaffNote8.Checked ? true : false; }
            else if (((C1Button)sender).Name.Equals("btnExp9")) { hn = txtHn9.Text.Trim(); an = txtAn9.Text.Trim(); flagOutlab = imgOutlab9.Checked ? true : false; flagStaffnote = imgStaffNote9.Checked ? true : false; }
            else if (((C1Button)sender).Name.Equals("btnExp10")) { hn = txtHn10.Text.Trim(); an = txtAn10.Text.Trim(); flagOutlab = imgOutlab10.Checked ? true : false; flagStaffnote = imgStaffNote10.Checked ? true : false; }

            else if (((C1Button)sender).Name.Equals("btnExp11")) { hn = txtHn11.Text.Trim(); an = txtAn11.Text.Trim(); flagOutlab = imgOutlab11.Checked ? true : false; flagStaffnote = imgStaffNote11.Checked ? true : false; }
            else if (((C1Button)sender).Name.Equals("btnExp12")) { hn = txtHn12.Text.Trim(); an = txtAn12.Text.Trim(); flagOutlab = imgOutlab12.Checked ? true : false; flagStaffnote = imgStaffNote12.Checked ? true : false; }
            else if (((C1Button)sender).Name.Equals("btnExp13")) { hn = txtHn13.Text.Trim(); an = txtAn13.Text.Trim(); flagOutlab = imgOutlab13.Checked ? true : false; flagStaffnote = imgStaffNote13.Checked ? true : false; }
            else if (((C1Button)sender).Name.Equals("btnExp14")) { hn = txtHn14.Text.Trim(); an = txtAn14.Text.Trim(); flagOutlab = imgOutlab14.Checked ? true : false; flagStaffnote = imgStaffNote14.Checked ? true : false; }
            else if (((C1Button)sender).Name.Equals("btnExp15")) { hn = txtHn15.Text.Trim(); an = txtAn15.Text.Trim(); flagOutlab = imgOutlab15.Checked ? true : false; flagStaffnote = imgStaffNote15.Checked ? true : false; }
            //dsc_id = id;
            genPDF(hn, an, flagOutlab, flagStaffnote);
        }
        private void LbPttName1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String hn = "", pttname = "";
            if (((Label)sender).Name.Equals("lbPttName1")) hn = txtHn1.Text.Trim();
            else if (((Label)sender).Name.Equals("lbPttName5")) hn = txtHn5.Text.Trim();
            else if (((Label)sender).Name.Equals("lbPttName2")) hn = txtHn2.Text.Trim();
            else if (((Label)sender).Name.Equals("lbPttName3")) hn = txtHn3.Text.Trim();
            else if (((Label)sender).Name.Equals("lbPttName4")) hn = txtHn4.Text.Trim();

            else if (((Label)sender).Name.Equals("lbPttName6")) hn = txtHn6.Text.Trim();
            else if (((Label)sender).Name.Equals("lbPttName7")) hn = txtHn7.Text.Trim();
            else if (((Label)sender).Name.Equals("lbPttName8")) hn = txtHn8.Text.Trim();
            else if (((Label)sender).Name.Equals("lbPttName9")) hn = txtHn9.Text.Trim();
            else if (((Label)sender).Name.Equals("lbPttName10")) hn = txtHn10.Text.Trim();

            else if (((Label)sender).Name.Equals("lbPttName11")) hn = txtHn11.Text.Trim();
            else if (((Label)sender).Name.Equals("lbPttName12")) hn = txtHn12.Text.Trim();
            else if (((Label)sender).Name.Equals("lbPttName13")) hn = txtHn13.Text.Trim();
            else if (((Label)sender).Name.Equals("lbPttName14")) hn = txtHn14.Text.Trim();
            else if (((Label)sender).Name.Equals("lbPttName15")) hn = txtHn15.Text.Trim();

            pttname = ((Label)sender).Text;

            Label lbname = new Label();
            lbname.Text = hn + " " + pttname;
            lbname.Font = fEdit;
            C1FlexGrid grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Rows.Count = 1;
            grf.Cols.Count = 5;

            Form frm = new Form();
            frm.Size = new Size(400, 400);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Controls.Add(grf);
            frm.Controls.Add(lbname);
            lbname.Top = 5;
            lbname.Left = 10;
            lbname.AutoSize = true;
            grf.Top = 25;
            grf.Left = 10;
            grf.Cols[1].AllowEditing = false;
            grf.Cols[2].AllowEditing = false;
            grf.Cols[3].Visible = false;
            grf.Cols[4].Visible = false;
            grf.Size = new Size(380, 300);
            grf.Click += Grf_Click;
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectPttIPDANbyHN(hn);
            grf.Rows.Count = dt.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow dr in dt.Rows)
            {
                Row rowa = grf.Rows[i];
                rowa[1] = dr["mnc_an_no"].ToString() + "." + dr["mnc_an_yr"].ToString();
                rowa[2] = bc.datetoShow1(dr["mnc_ad_date"].ToString());
                rowa[0] = i.ToString();
                rowa[3] = ((Label)sender).Name;
                rowa[4] = hn;
                i++;
            }
            frm.ShowDialog(this);
            
        }
        private void Grf_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if ((C1FlexGrid)sender == null) return;
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;
            Boolean chkdsc = false, chklab = false, chkxray = false;
            String con = "", anno = "", hn = "";
            con = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, 3].ToString();
            anno = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, 1].ToString();
            hn = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, 4].ToString();
            DataTable dtdsc = new DataTable();
            dtdsc = bc.bcDB.dscDB.selectByAn(hn, anno.Replace(".", "/"));
            if (dtdsc.Rows.Count > 0) { chkdsc = true; } else { chkdsc = false; }
            DataTable dtlab = new DataTable();
            DataTable dtxray = new DataTable();
            String[] an1 = anno.Split('.');
            dtlab = bc.bcDB.vsDB.selectLabbyAN(hn, an1[0], an1[1]);
            dtxray = bc.bcDB.vsDB.selectResultXraybyAN(hn, an1[0], an1[1]);
            if (dtlab.Rows.Count > 0) { chklab = true; } else { chklab = false; }
            if (dtxray.Rows.Count > 0) { chkxray = true; } else { chkxray = false; }
            if (con.Equals("lbPttName1")) { txtAn1.Value = anno; chkExp1.Checked = true; imgScan1.Visible = chkdsc; imgLab1.Visible = chkdsc;imgXray1.Visible = chkxray; }
            else if (con.Equals("lbPttName2")) { txtAn2.Value = anno; chkExp2.Checked = true; imgScan2.Visible = chkdsc; imgLab2.Visible = chkdsc; imgXray2.Visible = chkxray; }
            else if (con.Equals("lbPttName3")) { txtAn3.Value = anno; chkExp3.Checked = true; imgScan3.Visible = chkdsc; imgLab3.Visible = chkdsc; imgXray3.Visible = chkxray; }
            else if (con.Equals("lbPttName4")) { txtAn4.Value = anno; chkExp4.Checked = true; imgScan4.Visible = chkdsc; imgLab4.Visible = chkdsc; imgXray4.Visible = chkxray; }
            else if (con.Equals("lbPttName5")) { txtAn5.Value = anno; chkExp5.Checked = true; imgScan5.Visible = chkdsc; imgLab5.Visible = chkdsc; imgXray5.Visible = chkxray; }

            else if (con.Equals("lbPttName6")) { txtAn6.Value = anno; chkExp6.Checked = true; imgScan6.Visible = chkdsc; imgLab6.Visible = chkdsc; imgXray6.Visible = chkxray; }
            else if (con.Equals("lbPttName7")) { txtAn7.Value = anno; chkExp7.Checked = true; imgScan7.Visible = chkdsc; imgLab7.Visible = chkdsc; imgXray7.Visible = chkxray; }
            else if (con.Equals("lbPttName8")) { txtAn8.Value = anno; chkExp8.Checked = true; imgScan8.Visible = chkdsc; imgLab8.Visible = chkdsc; imgXray8.Visible = chkxray; }
            else if (con.Equals("lbPttName9")) { txtAn9.Value = anno; chkExp9.Checked = true; imgScan9.Visible = chkdsc; imgLab9.Visible = chkdsc; imgXray9.Visible = chkxray; }
            else if (con.Equals("lbPttName10")) { txtAn10.Value = anno; chkExp10.Checked = true; imgScan10.Visible = chkdsc; imgLab10.Visible = chkdsc; imgXray10.Visible = chkxray; }

            else if (con.Equals("lbPttName11")) { txtAn11.Value = anno; chkExp11.Checked = true; imgScan11.Visible = chkdsc; imgLab11.Visible = chkdsc; imgXray11.Visible = chkxray; }
            else if (con.Equals("lbPttName12")) { txtAn12.Value = anno; chkExp12.Checked = true; imgScan12.Visible = chkdsc; imgLab12.Visible = chkdsc; imgXray12.Visible = chkxray; }
            else if (con.Equals("lbPttName13")) { txtAn13.Value = anno; chkExp13.Checked = true; imgScan13.Visible = chkdsc; imgLab13.Visible = chkdsc; imgXray13.Visible = chkxray; }
            else if (con.Equals("lbPttName14")) { txtAn14.Value = anno; chkExp14.Checked = true; imgScan14.Visible = chkdsc; imgLab14.Visible = chkdsc; imgXray14.Visible = chkxray; }
            else if (con.Equals("lbPttName15")) { txtAn15.Value = anno; chkExp15.Checked = true; imgScan15.Visible = chkdsc; imgLab15.Visible = chkdsc; imgXray15.Visible = chkxray; }
        }
        private void TxtHn1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode != Keys.Enter) return;            
            String hn = "", pttname = "";
            hn = ((C1TextBox)sender).Text;
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetByHn(hn);
            if (((C1TextBox)sender).Name.Equals("txtHn1")) lbPttName1.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn2")) lbPttName2.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn3")) lbPttName3.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn4")) lbPttName4.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn5")) lbPttName5.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn6")) lbPttName6.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn7")) lbPttName7.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn8")) lbPttName8.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn9")) lbPttName9.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn10")) lbPttName10.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn12")) lbPttName12.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn13")) lbPttName13.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn14")) lbPttName14.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn15")) lbPttName15.Text = ptt.Name;
            else if (((C1TextBox)sender).Name.Equals("txtHn11")) lbPttName11.Text = ptt.Name;

        }
        private void setLbLoading(String txt)
        {
            lbLoading.Text = txt;
            Application.DoEvents();
        }
        private void showLbLoading()
        {
            lbLoading.Show();
            lbLoading.BringToFront();
            Application.DoEvents();
        }
        private void hideLbLoading()
        {
            lbLoading.Hide();
            Application.DoEvents();
        }
        private void initFont()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            famt1 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 1, FontStyle.Regular);
            famt5 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
            famt7B = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
            famtB14 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 14, FontStyle.Bold);
            famtB30 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 30, FontStyle.Bold);
            ftotal = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 60, FontStyle.Bold);
            fPrnBil = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fque = new Font(bc.iniC.queFontName, bc.queFontSize + 3, FontStyle.Bold);
            fqueB = new Font(bc.iniC.queFontName, bc.queFontSize + 7, FontStyle.Bold);
            fEditS = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            fEditS1 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 1, FontStyle.Regular);

        }
        private void FrmMedScanExport_Load(object sender, EventArgs e)
        {
            this.Text = "last Update 2024-02-21";
            lfsbLastUpdate.Text = "last Update 2024-02-21";
            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            spMedScan.HeaderHeight = 0;
            spOutLab.HeaderHeight = 0;
            spSearch.HeaderHeight = 0;
            spRpt.HeaderHeight = 0;
            txtRptStartDate.Value = DateTime.Now;
            txtRptEndDate.Value = DateTime.Now;

            rgSbModule.Text = bc.iniC.hostDBMainHIS + " " + bc.iniC.nameDBMainHIS;
        }
    }
}
