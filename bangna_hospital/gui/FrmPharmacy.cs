using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using C1.Win.C1Tile;
using C1.Win.TouchToolKit;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using System.Printing;
using GrapeCity.Viewer.Common.Model;
using System.Net;
using C1.Win.C1Input;
using Column = C1.Win.C1FlexGrid.Column;
using Row = C1.Win.C1FlexGrid.Row;

namespace bangna_hospital.gui
{
    public partial class FrmPharmacy : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, fPrnBil, famtB, fEditS, famtB30;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        //C1PdfDocument pdfDoc;
        C1ThemeController theme1;
        Label lbLoading;
        Timer timeImgDrugINward;
        C1TileControl TileDrugINimg;
        PanelElement pnTopTilesLab;
        C1FlexGrid grfReport, grfOrder, grfOPDFinishList, grfOPDFinishReq, grfIPD, grfIPDScan;
        
        ImageElement imgTopTilesLab, imgFolder;
        TextElement txtTopTilesLab;
        Template tempFolder;
        Group grpDrug;
        Boolean pageLoad = false;
        Image imgCorr, imgTran, imgDrug, resizedImage, IMG;
        Color backColor, checkBackColor, hotBackColor, hotCheckBackColor, subgroupLineColor, subgroupTitleColor;
        AutoCompleteStringCollection autoLab, autoXray, autoProcedure, autoWard, autoDrug;
        Label lbDocAll, lbDocGrp1, lbDocGrp2, lbDocGrp3, lbDocGrp4, lbDocGrp5, lbDocGrp6, lbDocGrp7, lbDocGrp8, lbDocGrp9;
        Color colorLbDoc;
        C1PictureBox pic;

        int originalHeight = 0, newHeight = 720, mouseWheel = 0;
        int rowindexgrfOPDFinishList = 0, rowindexgrfOPDFinishReq= 0,TIMERCNT=0;
        String TC1ACTIVE = "", TCOPDACTIVE="", PRENO = "", VSDATE = "", HN = "", DEPTNO = "", DTRCODE = "", DOCGRPID = "", TABVSACTIVE = "", ITEMCODE="", DSCID="";
        float ImageScale = 1.0f;
        int colOPDFinishListHN = 1,colOPDFinishListPttName = 2, colOPDFinishListQueNo = 3, colOPDFinishListReqTime = 4, colOPDFinishListCFRTime = 5, colOPDFinishListVnno = 6, colOPDFinishListDeptName = 7, colOPDFinishListDocCD = 8, colOPDFinishListCFRYr = 9, colOPDFinishListCFRNo = 10, colOPDFinishListCFRDATE = 11, colOPDFinishListCFRSTS = 12, colOPDFinishListReqYr = 13, colOPDFinishListReqNo = 14, colOPDFinishListReqDate = 15, colOPDFinishListVsDate = 16, colOPDFinishListPreNo = 17, colOPDFinishListDtrcode = 18, colOPDFinishListDtrName = 19, colOPDFinishListUsrReq = 20, colOPDFinishListUsrPhar = 21, colOPDFinishListPttDOB = 22, colOPDFinishListPttSex = 23, colOPDFinishListPaidName = 24, colOPDFinishListCompName = 25, colOPDFinishListPttAttachNote = 26, colOPDFinishListFinNote = 27, colOPDFinishListDeptNo = 28, colOPDFinishListSecNo = 29;
        int colOPDFinishReqItemCode = 1, colOPDFinishReqItemName = 2, colOPDFinishReqDocCD = 3, colOPDFinishReqCFRYr = 4, colOPDFinishReqCFRNo = 5, colOPDFinishReqCFRDate = 6, colOPDFinishReqQty = 7, colOPDFinishReqDirDesc = 8, colOPDFinishReqPttName=9;
        int colIPDDate = 1, colIPDDept = 2, colIPDAnShow = 4, colIPDStatus = 3, colIPDPreno = 5, colIPDVn = 6, colIPDAndate = 7, colIPDAnYr = 8, colIPDAn = 9, colIPDDtrName = 10;
        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
        listStream strm;
        List<listStream> lStream, lStreamPic;

        Point pDown = Point.Empty;
        Rectangle rect = Rectangle.Empty;
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        DataTable DTSTCDRUG;
        DataRow DROWDRUG;
        FrmReportNew frmDrugStricker;
        Stream streamPrint;
        Form frmImg;

        public FrmPharmacy(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            initFont();
            initControl();
            setEvent();
        }
        private void initFont()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);
            fPrnBil = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            famtB = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
            fEditS = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            famtB30 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 30, FontStyle.Bold);
        }
        private void initControl()
        {
            theme1 = new C1.Win.C1Themes.C1ThemeController();
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            timeImgDrugINward = new Timer();
            timeImgDrugINward.Interval = 1000;
            timeImgDrugINward.Enabled = false;
            imgCorr = Resources.red_checkmark_png_16;
            imgTran = Resources.DeleteTable_small;

            backColor = Color.FromArgb(29, 29, 29);
            checkBackColor = Color.FromArgb(244, 179, 0);
            hotBackColor = Color.FromArgb(58, 58, 58);
            hotCheckBackColor = Color.FromArgb(246, 188, 35);
            subgroupLineColor = Color.FromArgb(38, 38, 38);
            subgroupTitleColor = Color.FromArgb(255, 200, 63);

            autoLab = new AutoCompleteStringCollection();
            autoXray = new AutoCompleteStringCollection();
            autoProcedure = new AutoCompleteStringCollection();
            autoDrug = new AutoCompleteStringCollection();
            frmDrugStricker = new FrmReportNew(bc, "drug_stricker");
            lStream = new List<listStream>();

            initTileImg();
            initGrfOPDFinishList();
            initGrfOPDFinishItem();
            initGrfIPD();
            initGrfIPDScan();
            initLoading();
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
                vn = row1["MNC_VN_NO"].ToString() + "." + row1["MNC_VN_SEQ"].ToString() + "." + row1["MNC_VN_SUM"].ToString();
                rowa[colIPDDate] = bc.datetoShow1(row1["mnc_date"].ToString());
                rowa[colIPDVn] = vn;
                rowa[colIPDStatus] = status;
                rowa[colIPDPreno] = row1["mnc_pre_no"].ToString();
                rowa[colIPDDept] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colIPDAnShow] = row1["mnc_an_no"].ToString() + "." + row1["mnc_an_yr"].ToString();
                rowa[colIPDAndate] = bc.datetoShow1(row1["mnc_ad_date"].ToString());
                rowa[colIPDAnYr] = row1["mnc_an_yr"].ToString();
                rowa[colIPDAn] = row1["mnc_an_no"].ToString();
                rowa[colIPDDtrName] = row1["dtr_name"].ToString();
                i++;
            }
        }
        private void setGrfIPDScan()
        {
            //Application.DoEvents();
            //ProgressBar pB1 = new ProgressBar();
            //pB1.Location = new System.Drawing.Point(20, 16);
            //pB1.Name = "pB1";
            //pB1.Size = new System.Drawing.Size(862, 23);
            //gbPtt.Controls.Add(pB1);
            //pB1.Left = txtHn.Left;
            //pB1.Show();
            showLbLoading();
            lStream.Clear();
            //clearGrf();
            String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "", anyr = "", vn1 = "";
            DataTable dtOrder = new DataTable();

            //new LogWriter("e", "FrmScanView1 setGrfScan 5 ");
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
            dt = bc.bcDB.dscDB.selectByAn(txtSBSearchHN.Text, an.Replace(".","/"));
            grfIPDScan.Rows.Count = 0;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    int cnt = 0;
                    cnt = dt.Rows.Count / 2;

                    grfIPDScan.Rows.Count = cnt + 1;

                    FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                    Boolean findTrue = false;
                    int colcnt = 0, rowrun = -1;
                    foreach (DataRow row1 in dt.Rows)
                    {
                        if (findTrue) break;
                        colcnt++;
                        String dgssid = "", filename = "", ftphost = "", id = "", folderftp = "";
                        id = row1[bc.bcDB.dscDB.dsc.doc_scan_id].ToString();
                        dgssid = row1[bc.bcDB.dscDB.dsc.doc_group_sub_id].ToString();
                        filename = row1[bc.bcDB.dscDB.dsc.image_path].ToString();
                        ftphost = row1[bc.bcDB.dscDB.dsc.host_ftp].ToString();
                        folderftp = row1[bc.bcDB.dscDB.dsc.folder_ftp].ToString();

                        //new Thread(() =>
                        //{
                        String err = "";
                        try
                        {
                            FtpWebRequest ftpRequest = null;
                            FtpWebResponse ftpResponse = null;
                            Stream ftpStream = null;
                            int bufferSize = 2048;
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
                            MemoryStream stream;
                            Image loadedImage, resizedImage;
                            stream = new MemoryStream();
                            //stream = ftp.download(folderftp + "//" + filename);

                            //loadedImage = Image.FromFile(filename);
                            err = "01";

                            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(ftphost + "/" + folderftp + "/" + filename);
                            ftpRequest.Credentials = new NetworkCredential(bc.iniC.userFTP, bc.iniC.passFTP);
                            ftpRequest.UseBinary = true;
                            ftpRequest.UsePassive = bc.ftpUsePassive;
                            ftpRequest.KeepAlive = true;
                            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                            ftpStream = ftpResponse.GetResponseStream();
                            err = "02";
                            byte[] byteBuffer = new byte[bufferSize];
                            int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                            try
                            {
                                while (bytesRead > 0)
                                {
                                    stream.Write(byteBuffer, 0, bytesRead);
                                    bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                new LogWriter("e", "FrmScanView1 SetGrfScan try int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize); ex " + ex.Message + " " + err);
                            }
                            err = "03";

                            loadedImage = new Bitmap(stream);
                            err = "04";
                            int originalWidth = 0;
                            originalWidth = loadedImage.Width;
                            int newWidth = bc.imgScanWidth;
                            resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
                            //
                            err = "05";
                            if ((colcnt % 2) == 0)
                            {

                                rowd[colPic3] = resizedImage;       // + 0001
                                err = "061";       // + 0001
                                rowd[colPic4] = id;       // + 0001
                                err = "071";       // + 0001
                            }
                            else
                            {

                                err = "051";       // + 0001
                                rowd[colPic1] = resizedImage;       // + 0001
                                err = "06";       // + 0001
                                rowd[colPic2] = id;       // + 0001
                                err = "07";       // + 0001
                            }

                            strm = new listStream();
                            strm.id = id;
                            strm.dgsid = row1[bc.bcDB.dscDB.dsc.doc_group_id].ToString();
                            err = "08";
                            strm.stream = stream;
                            err = "09";
                            lStream.Add(strm);

                            err = "12";

                            if (colcnt == 50) GC.Collect();
                            if (colcnt == 100) GC.Collect();
                        }
                        catch (Exception ex)
                        {
                            String aaa = ex.Message + " " + err;
                            new LogWriter("e", "FrmScanView1 SetGrfScan ex " + ex.Message + " " + err + " colcnt " + colcnt + " doc_scan_id " + id);
                        }

                    }
                    ftp = null;
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
        class listStream
        {
            public String id = "", dgsid = "";
            public MemoryStream stream;
        }
        private void initGrfOPDFinishItem()
        {
            grfOPDFinishReq = new C1FlexGrid();
            grfOPDFinishReq.Font = fEdit;
            grfOPDFinishReq.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPDFinishReq.Location = new System.Drawing.Point(0, 0);
            grfOPDFinishReq.Rows.Count = 1;
            grfOPDFinishReq.Cols.Count = 10;

            grfOPDFinishReq.Cols[colOPDFinishReqItemCode].Width = 80;
            grfOPDFinishReq.Cols[colOPDFinishReqItemName].Width = 300;
            grfOPDFinishReq.Cols[colOPDFinishReqDocCD].Width = 80;
            grfOPDFinishReq.Cols[colOPDFinishReqCFRYr].Width = 80;
            grfOPDFinishReq.Cols[colOPDFinishReqCFRNo].Width = 80;
            grfOPDFinishReq.Cols[colOPDFinishReqCFRDate].Width = 80;
            grfOPDFinishReq.Cols[colOPDFinishReqQty].Width = 80;
            grfOPDFinishReq.Cols[colOPDFinishReqDirDesc].Width = 500;

            grfOPDFinishReq.ShowCursor = true;
            grfOPDFinishReq.Cols[1].Caption = "-";

            grfOPDFinishReq.Cols[colOPDFinishReqDocCD].Visible = false;
            grfOPDFinishReq.Cols[colOPDFinishReqCFRYr].Visible = false;
            grfOPDFinishReq.Cols[colOPDFinishReqCFRNo].Visible = false;
            grfOPDFinishReq.Cols[colOPDFinishReqCFRDate].Visible = false;

            grfOPDFinishReq.Cols[colOPDFinishReqItemCode].AllowEditing = false;
            grfOPDFinishReq.Cols[colOPDFinishReqItemName].AllowEditing = false;
            grfOPDFinishReq.Cols[colOPDFinishReqQty].AllowEditing = false;
            grfOPDFinishReq.Cols[colOPDFinishReqDirDesc].AllowEditing = false;
            grfOPDFinishReq.Cols[colOPDFinishListPttName].AllowEditing = false;

            grfOPDFinishReq.AfterRowColChange += GrfOPDFinishReq_AfterRowColChange;
            pnOPDFinishReq.Controls.Add(grfOPDFinishReq);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfOPDFinishReq, "MacBlue");
        }
        private void GrfOPDFinishReq_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            try
            {
                if (rowindexgrfOPDFinishReq != ((C1FlexGrid)(sender)).Row) { rowindexgrfOPDFinishReq = ((C1FlexGrid)(sender)).Row; }
                else { return; }
                ITEMCODE = grfOPDFinishReq[grfOPDFinishReq.Row, colOPDFinishReqItemCode].ToString();
                rbItemName.Text = grfOPDFinishReq[grfOPDFinishReq.Row, colOPDFinishReqItemName].ToString();
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmPatient GrfOPD_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmPatient GrfOPD_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            finally
            {
                //frmFlash.Dispose();
                hideLbLoading();
            }
        }
        private void setGrfOPDFinishReq(String doccd, String cfryear, String cfrdate, String cfrno)
        {
            DTSTCDRUG = bc.bcDB.pharT06DB.selectByCFRNoDrug1(cfryear, cfrno, doccd, cfrdate);
            int i = 1, j = 1;
            grfOPDFinishReq.Rows.Count = 1; grfOPDFinishReq.Rows.Count = DTSTCDRUG.Rows.Count + 1;
            foreach (DataRow row1 in DTSTCDRUG.Rows)
            {
                try
                {
                    C1.Win.C1FlexGrid.Row rowa = grfOPDFinishReq.Rows[i];
                    rowa[colOPDFinishReqItemCode] = row1["MNC_PH_CD"].ToString();
                    rowa[colOPDFinishReqItemName] = row1["MNC_PH_TN"].ToString();
                    rowa[colOPDFinishReqDocCD] = row1["MNC_DOC_CD"].ToString();
                    rowa[colOPDFinishReqCFRYr] = row1["MNC_CFR_YR"].ToString();
                    rowa[colOPDFinishReqCFRNo] = row1["MNC_CFR_NO"].ToString();
                    rowa[colOPDFinishReqCFRDate] = row1["MNC_CFG_DAT"].ToString();
                    rowa[colOPDFinishReqQty] = row1["MNC_PH_QTY_PAID"].ToString();
                    rowa[colOPDFinishReqPttName] = row1["pttname"].ToString();
                    rowa[colOPDFinishReqDirDesc] = row1["drug_using"].ToString().Replace("/","").Trim();
                    i++;
                }
                catch(Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD setGrfOrder " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setGrfOrder ", ex.Message);
                }
            }
        }
        private void initGrfOPDFinishList()
        {
            grfOPDFinishList = new C1FlexGrid();
            grfOPDFinishList.Font = fEdit;
            grfOPDFinishList.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPDFinishList.Location = new System.Drawing.Point(0, 0);
            grfOPDFinishList.Rows.Count = 1;
            grfOPDFinishList.Cols.Count = 30;
            grfOPDFinishList.Cols[colOPDFinishListHN].Width = 80;
            grfOPDFinishList.Cols[colOPDFinishListPttName].Width = 300;
            grfOPDFinishList.Cols[colOPDFinishListQueNo].Width = 60;
            grfOPDFinishList.Cols[colOPDFinishListReqTime].Width = 70;
            grfOPDFinishList.Cols[colOPDFinishListCFRTime].Width = 70;
            grfOPDFinishList.Cols[colOPDFinishListVnno].Width = 60;
            grfOPDFinishList.Cols[colOPDFinishListDeptName].Width = 100;

            grfOPDFinishList.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfOPDFinishList.Cols[1].Caption = "-";

            grfOPDFinishList.Cols[colOPDFinishListDocCD].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListCFRYr].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListCFRNo].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListCFRDATE].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListCFRSTS].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListReqYr].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListReqNo].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListReqDate].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListVsDate].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListPreNo].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListDtrcode].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListPttDOB].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListPttSex].Visible = false;
            grfOPDFinishList.Cols[colOPDFinishListDeptNo].Visible = false;

            grfOPDFinishList.Cols[colOPDFinishListHN].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListPttName].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListQueNo].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListReqTime].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListCFRTime].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListVnno].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListUsrPhar].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListUsrReq].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListDtrName].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListPaidName].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListCompName].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListPttAttachNote].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListFinNote].AllowEditing = false;
            grfOPDFinishList.Cols[colOPDFinishListSecNo].AllowEditing = false;

            grfOPDFinishList.AfterRowColChange += GrfOPDFinishList_AfterRowColChange;
            pnOPDFinishList.Controls.Add(grfOPDFinishList);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfOPDFinishList, "Office2010Red");
        }
        private void GrfOPDFinishList_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            try
            {
                if (rowindexgrfOPDFinishList != ((C1FlexGrid)(sender)).Row) { rowindexgrfOPDFinishList = ((C1FlexGrid)(sender)).Row; }
                else { return; }
                String doccd = "", cfryear = "", cfrdate = "", cfrno = "";
                doccd = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListDocCD].ToString();
                cfryear = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListCFRYr].ToString();
                cfrdate = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListCFRDATE].ToString();
                cfrno = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListCFRNo].ToString();
                HN = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListHN].ToString();
                VSDATE = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListVsDate].ToString();
                PRENO = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListPreNo].ToString();
                rbPttName.Text = grfOPDFinishList[grfOPDFinishList.Row, colOPDFinishListPttName].ToString();

                clearControlReq();

                setGrfOPDFinishReq(doccd, cfryear, cfrdate, cfrno);
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmPatient GrfOPD_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmPatient GrfOPD_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            finally
            {
                //frmFlash.Dispose();
                hideLbLoading();
            }
        }
        private void setGrfOPDFinishList()
        {//ดึงจาก table temp_order
            DataTable dt = new DataTable();
            dt = bc.bcDB.pharT05DB.SelectByCFGCurrentDate();
            int i = 1, j = 1;
            grfOPDFinishList.Rows.Count = 1; grfOPDFinishList.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                try
                {
                    C1.Win.C1FlexGrid.Row rowa = grfOPDFinishList.Rows[i];
                    rowa[colOPDFinishListHN] = row1["MNC_HN_NO"].ToString();
                    rowa[colOPDFinishListPttName] = row1["PATIENTNAME"].ToString();
                    rowa[colOPDFinishListQueNo] = row1["MNC_QUE_NO"].ToString();
                    rowa[colOPDFinishListReqTime] = bc.showTime(row1["MNC_REQ_TIM"].ToString());
                    rowa[colOPDFinishListVnno] = row1["MNC_VN_NO"].ToString()+"."+ row1["MNC_VN_SEQ"].ToString()+"."+ row1["MNC_VN_SUM"].ToString();
                    
                    rowa[colOPDFinishListDeptName] = row1["MNC_SEC_DSC"].ToString();

                    rowa[colOPDFinishListDocCD] = row1["MNC_DOC_CD"].ToString();
                    rowa[colOPDFinishListCFRYr] = row1["MNC_CFR_YR"].ToString();
                    rowa[colOPDFinishListCFRNo] = row1["MNC_CFR_NO"].ToString();
                    rowa[colOPDFinishListCFRDATE] = row1["MNC_CFG_DAT"].ToString();
                    rowa[colOPDFinishListCFRTime] = bc.showTime(row1["MNC_CFR_TIME"].ToString());

                    rowa[colOPDFinishListReqNo] = row1["MNC_REQ_NO"].ToString();
                    rowa[colOPDFinishListVsDate] = row1["MNC_DATE"].ToString();
                    rowa[colOPDFinishListPreNo] = row1["MNC_PRE_NO"].ToString();
                    rowa[colOPDFinishListReqDate] = row1["MNC_REQ_DAT"].ToString();
                    rowa[colOPDFinishListDtrcode] = row1["MNC_ORD_DOT"].ToString();
                    rowa[colOPDFinishListDtrName] = row1["ORDDOTNAME"].ToString();
                    rowa[colOPDFinishListPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
                    rowa[colOPDFinishListCompName] = row1["MNC_COM_DSC"].ToString();
                    rowa[colOPDFinishListPttAttachNote] = row1["MNC_ATT_NOTE"].ToString();
                    rowa[colOPDFinishListFinNote] = row1["MNC_FIN_NOTE"].ToString();
                    rowa[colOPDFinishListSecNo] = row1["MNC_SEC_NO"].ToString();
                    //rowa[colgrfOrderReqNO] = "";
                    //rowa[colgrfOrdFlagSave] = "1";//ดึงข้อมูลจาก table temp_order 
                    rowa[0] = i.ToString();
                    //rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                    i++;
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD setGrfOrder " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setGrfOrder ", ex.Message);
                }
            }
        }
        private void clearControlReq()
        {
            ITEMCODE = "";
            rbItemName.Text = "";
            grfOPDFinishReq.Rows.Count = 1;
        }
        private void PicDrugIN_MouseUp(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            Rectangle iR = ImageArea(picDrugIN);
            rect = new Rectangle(pDown.X - iR.X, pDown.Y - iR.Y,
                                 e.X - pDown.X, e.Y - pDown.Y);
            Rectangle rectSrc = Scaled(rect, picDrugIN, true);
            Rectangle rectDest = new Rectangle(Point.Empty, rectSrc.Size);

            Bitmap bmp = new Bitmap(rectDest.Width, rectDest.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(picDrugIN.Image, rectDest, rectSrc, GraphicsUnit.Pixel);
            }
            picDrugIN.Image = bmp;
        }
        private void PicDrugIN_MouseMove(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            if (!e.Button.HasFlag(MouseButtons.Left)) return;

            rect = new Rectangle(pDown, new Size(e.X - pDown.X, e.Y - pDown.Y));
            using (Graphics g = picDrugIN.CreateGraphics())
            {
                picDrugIN.Refresh();
                g.DrawRectangle(Pens.Orange, rect);
            }
        }
        private void PicDrugIN_MouseDown(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            pDown = e.Location;
            picDrugIN.Refresh();
        }
        private void PicDrugIN_MouseWheel(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            //const float scale_per_delta = 0.1f / 120;

            //// Update the drawing based upon the mouse wheel scrolling.
            //ImageScale += e.Delta * scale_per_delta;
            //if (ImageScale < 0) ImageScale = 0;

            //// Size the image.
            //Size newsize = new Size((int)(imgDrug.Width * ImageScale), (int)(imgDrug.Height * ImageScale));
            //picDrugIN.Image = ZoomPicture(imgDrug, newsize);
        }
        Rectangle ImageArea(PictureBox pbox)
        {
            Size si = pbox.Image.Size;
            Size sp = pbox.ClientSize;

            if (pbox.SizeMode == PictureBoxSizeMode.StretchImage)
                return pbox.ClientRectangle;
            if (pbox.SizeMode == PictureBoxSizeMode.Normal ||
                pbox.SizeMode == PictureBoxSizeMode.AutoSize)
                return new Rectangle(Point.Empty, si);
            if (pbox.SizeMode == PictureBoxSizeMode.CenterImage)
                return new Rectangle(new Point((sp.Width - si.Width) / 2,
                                    (sp.Height - si.Height) / 2), si);

            //  PictureBoxSizeMode.Zoom
            float ri = 1f * si.Width / si.Height;
            float rp = 1f * sp.Width / sp.Height;
            if (rp > ri)
            {
                int width = si.Width * sp.Height / si.Height;
                int left = (sp.Width - width) / 2;
                return new Rectangle(left, 0, width, sp.Height);
            }
            else
            {
                int height = si.Height * sp.Width / si.Width;
                int top = (sp.Height - height) / 2;
                return new Rectangle(0, top, sp.Width, height);
            }
        }
        Rectangle Scaled(Rectangle rect, PictureBox pbox, bool scale)
        {
            float factor = GetFactor(pbox);
            if (!scale) factor = 1f / factor;
            return Rectangle.Round(new RectangleF(rect.X * factor, rect.Y * factor,
                                       rect.Width * factor, rect.Height * factor));
        }
        float GetFactor(PictureBox pBox)
        {
            if (pBox.Image == null) return 0;
            Size si = pBox.Image.Size;
            Size sp = pBox.ClientSize;
            float ri = 1f * si.Width / si.Height;
            float rp = 1f * sp.Width / sp.Height;
            float factor = 1f * pBox.Image.Width / pBox.ClientSize.Width;
            if (rp > ri) factor = 1f * pBox.Image.Height / pBox.ClientSize.Height;
            return factor;
        }
        private void setEvent()
        {
            timeImgDrugINward.Tick += TimeImgDrugINward_Tick;
            btnDrugINZoom.Click += BtnDrugINZoom_Click;
            txtSearchItem.KeyUp += TxtSearchItem_KeyUp;
            txtSearchItem.Enter += TxtSearchItem_Enter;
            txtItemCode.KeyUp += TxtItemCode_KeyUp;
            btnDrugINWard.Click += BtnDrugINWard_Click;
            tC1.SelectedTabChanged += TC1_SelectedTabChanged;
            tcOPD.SelectedTabChanged += TcOPD_SelectedTabChanged;

            picDrugIN.MouseWheel += PicDrugIN_MouseWheel;
            picDrugIN.MouseDown += PicDrugIN_MouseDown;
            picDrugIN.MouseMove += PicDrugIN_MouseMove;
            picDrugIN.MouseUp += PicDrugIN_MouseUp;

            btnPrint.Click += BtnPrint_Click;
            btnPrintItem.Click += BtnPrintItem_Click;
            rbTimerStart.Click += RbTimerStart_Click;
            txtSBSearchHN.KeyDown += TxtSBSearchHN_KeyDown;
            
        }
        private void TxtSBSearchHN_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                lfSbMessage.Text = txtSBSearchHN.Text;
                if (TC1ACTIVE.Equals(tabMedScan.Name))
                {
                    
                    Patient ptt = bc.bcDB.pttDB.selectPatinetByHn(txtSBSearchHN.Text);
                    rb1.Text = ptt.Name;
                    tC1.SelectedTab = tabMedScan;
                    setGrfIPD();
                }
            }
        }

        private void TxtSBSearchHN_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            
        }

        private void RbTimerStart_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (rbTimerStart.Text.Equals("Start"))
            {
                rbTimerStart.Text = "Stop";
                rbTimerStart.SmallImage = Resources.start128;
                timeImgDrugINward.Start();
            }
            else
            {
                rbTimerStart.Text = "Start";
                rbTimerStart.SmallImage = Resources.stop_red48;
                timeImgDrugINward.Stop();
            }
        }
        private void BtnPrintItem_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String printer = LocalPrintServer.GetDefaultPrintQueue().FullName;
            lfSbStatus.Text = bc.iniC.printerStickerDrug;
            if (!printer.Equals(bc.iniC.printerStickerDrug))
            {
                lfSbMessage.Text = "change Printer " + LocalPrintServer.GetDefaultPrintQueue().FullName+" to "+ bc.iniC.printerStickerDrug;
                SetDefaultPrinter(bc.iniC.printerStickerDrug);
            }
            setReportStricker(ref DTSTCDRUG, bc.iniC.statusPrintPreview.Equals("1") ? true: false) ;
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printStickerDrug();
        }
        private void printStickerDrug()
        {
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerStickerDrug;
            document.PrintPage += Document_PrintPage_Sticker_Drug;
            document.DefaultPageSettings.Landscape = false;
            if (int.TryParse("1", out int num))
            {
                document.PrinterSettings.Copies = short.Parse(num.ToString());
                foreach (DataRow drow in DTSTCDRUG.Rows)
                {
                    //if (!drow["MNC_PH_cd"].ToString().Equals("FA014")) continue;
                    DROWDRUG = drow;
                    for (int i = 0; i < num; i++) { document.Print(); }
                }
            }

            PrintDocument documentSum = new PrintDocument();
            documentSum.PrinterSettings.PrinterName = bc.iniC.printerStickerDrug;
            documentSum.PrintPage += Document_PrintPage_Sticker_DrugSum;
            documentSum.DefaultPageSettings.Landscape = false;
            num = 0;
            if (int.TryParse("1", out num))
            {
                documentSum.PrinterSettings.Copies = short.Parse(num.ToString());
                for (int i = 0; i < num; i++){ documentSum.Print(); }
            }
        }
        private void Document_PrintPage_Sticker_Drug(object sender, PrintPageEventArgs e)
        {
            float yPos = 10, ydate = 0, gapline = 17, col1 = 10, col2 = 60, col4 = 200;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps

            String text = "";
            if (DTSTCDRUG.Rows.Count > 0)
            {
                e.Graphics.DrawString(bc.iniC.hostname, fPrnBil, Brushes.Black, col2, yPos, flags);
                e.Graphics.DrawString(bc.iniC.hosttel, fPrnBil, Brushes.Black, col4, yPos, flags);
                yPos += gapline;
                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fPrnBil, Brushes.Black, col2, yPos, flags);
                e.Graphics.DrawString("HN " + HN, fEditB, Brushes.Black, col4, yPos, flags);
                yPos += gapline;
                e.Graphics.DrawString(rbPttName.Text, famtB, Brushes.Black, col1, yPos, flags);
                yPos += gapline + 15;
                e.Graphics.DrawString(DROWDRUG["MNC_PH_DIR_DSC"].ToString(), fEdit, Brushes.Black, col1, yPos, flags);
                yPos += gapline;                yPos += gapline;                yPos += gapline;                yPos += gapline;
                yPos += gapline - 5;
                e.Graphics.DrawString(DROWDRUG["MNC_PH_TN"].ToString() + " [" + DROWDRUG["MNC_PH_QTY_PAID"].ToString() + " " + DROWDRUG["MNC_PH_UNT_CD"].ToString() + "]", fEdit, Brushes.Black, col1, yPos, flags);
            }
        }
        private void Document_PrintPage_Sticker_DrugSum(object sender, PrintPageEventArgs e)
        {
            float yPos = 10, ydate = 0, gapline = 14, gapline1 = 17, col1 = 10, col2 = 60, col3 = 140, col4 = 200;

            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            StringFormat flagsr = new StringFormat(StringFormatFlags.DirectionRightToLeft);  //wraps
            String text = "", chk = "";
            if (DTSTCDRUG.Rows.Count > 0)
            {
                e.Graphics.DrawString(bc.iniC.hostname, fPrnBil, Brushes.Black, col2 + 15, yPos, flags);
                e.Graphics.DrawString(bc.iniC.hosttel, fPrnBil, Brushes.Black, col4, yPos, flags);
                yPos += gapline1;
                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fPrnBil, Brushes.Black, col2 + 15, yPos, flags);
                e.Graphics.DrawString("HN " + HN, fEditB, Brushes.Black, col4, yPos, flags);
                yPos += gapline1;
                
                e.Graphics.DrawString(rbPttName.Text, famtB, Brushes.Black, col1, yPos, flags);
                yPos += gapline;
                yPos += gapline;
                float price = 0, qty = 0, amt = 0, sum = 0;
                foreach (DataRow drow in DTSTCDRUG.Rows)
                {
                    text = drow["MNC_PH_cd"].ToString();
                    float.TryParse(drow["MNC_PH_QTY_PAID"].ToString(), out qty);
                    float.TryParse(drow["MNC_PH_PRI"].ToString(), out price);
                    amt = price * qty;
                    sum += amt;
                    e.Graphics.DrawString(drow["MNC_PH_TN"].ToString(), fEditS, Brushes.Black, col1, yPos, flags);
                    e.Graphics.DrawString(qty.ToString("###"), fEditS, Brushes.Black, col3 + 100, yPos, flags);
                    e.Graphics.DrawString(amt.ToString("#.00"), fEditS, Brushes.Black, col3 + 130, yPos, flags);
                    e.Graphics.DrawString(drow["MNC_PH_UNT_CD"].ToString(), fEditS, Brushes.Black, col4, yPos, flags);
                    yPos += gapline;
                    //yPos = 10;
                    //e.HasMorePages = true;
                }
                e.Graphics.DrawString("รวม. " + sum.ToString("#,00"), fEditB, Brushes.Black, col4 + 25, 48, flags);
            }
            //string chk1 = "";
        }
        private void setReportStricker(ref DataTable dtdrug, Boolean preview)
        {
            Boolean chk = false;
            FileInfo rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\drug_stricker.rdlx");
            if (!File.Exists(rptPath.FullName))
            {
                lfSbMessage.Text = "File report not found";
                return;
            }
            int i = 1;
            DateTime dt = DateTime.Now;
            if (dt.Year < 1900)
            {
                dt = dt.AddYears(543);
            }
            String datetime = "";
            datetime = dt.ToString("dd-MM")+"-"+ (dt.Year+543)+" "+ dt.ToString("HH:mm:ss");
            foreach (DataRow drow in dtdrug.Rows)
            {
                drow["hostname"] = bc.iniC.hostname;
                drow["tel"] = bc.iniC.hosttel;
                drow["drug_using"] = drow["drug_using"].ToString().Replace("/", "").Trim();
                drow["printdatetime"] = datetime;
                //drow["drugname_t"] = drow["drugname_t"].ToString().Trim()+" "+ drow["unitqty"].ToString().Trim();
            }
            frmDrugStricker.DT = dtdrug;
            if (preview)
            {
                frmDrugStricker.ShowDialog(this);
            }
            else
            {
                if (frmDrugStricker.PrintReport())
                {
                    if(dtdrug.Rows.Count>0)
                        bc.bcDB.pharT05DB.updateStatusPrintStrickered(dtdrug.Rows[0]["MNC_DOC_CD"].ToString(), dtdrug.Rows[0]["MNC_CFR_YR"].ToString(), dtdrug.Rows[0]["MNC_CFR_NO"].ToString(), dtdrug.Rows[0]["MNC_CFG_DAT"].ToString());
                }
            }
        }
        private void TcOPD_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tcOPD.SelectedTab == tabOPDQue) { TCOPDACTIVE = tabOPDQue.Name; }
            else if (tcOPD.SelectedTab == tabOPDFinish) { TCOPDACTIVE = tabOPDFinish.Name; setGrfOPDFinishList(); }
            else if (tcOPD.SelectedTab == tabOPDDispensing) { TCOPDACTIVE = tabOPDDispensing.Name; }
        }

        private void TC1_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tC1.SelectedTab == tabDrugWard) {TC1ACTIVE = tabDrugWard.Name;}
            else if (tC1.SelectedTab == tabMedScan) { TC1ACTIVE = tabMedScan.Name; }
        }

        private void BtnDrugINWard_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Form frm = new Form();
            C1FlexGrid grf = new C1FlexGrid();
            frm.Controls.Add(grf);
            frm.ShowDialog(this);
        }
        private void TxtItemCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setGrfOrderItem();
            }
        }
        private void setGrfOrderItem()
        {
            setGrfOrderItem(txtItemCode.Text.Trim(), lbItemName.Text, txtItemQTY.Text.Trim(), "drug");
            txtSearchItem.Value = "";
            txtSearchItem.Focus();
            //grfOrder.Rows.Add(rowitem);
        }
        private void setGrfOrderItem(String code, String name, String qty, String flag)
        {
            if (grfOrder == null) { return; }
            ////if(grfOrder.Row<=0) { return; }
            //Row rowitem = grfOrder.Rows.Add();
            //rowitem[colgrfOrderCode] = code;
            //rowitem[colgrfOrderName] = name;
            //rowitem[colgrfOrderQty] = qty;
            //rowitem[colgrfOrderStatus] = flag;
            //rowitem[colgrfOrderID] = "";
            //rowitem[colgrfOrderReqNO] = "";
            //rowitem[colgrfOrdFlagSave] = "0";//ต้องการ save ลง table temp_order
            txtSearchItem.Value = "";
            txtSearchItem.Focus();
            //grfOrder.Rows.Add(rowitem);
        }
        private void TxtSearchItem_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.Focus();
        }

        private void TxtSearchItem_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearchItem.Text.Trim().Length <= 0) return;
                setOrderItem();
                txtItemCode.Focus();
            }
        }
        private void setOrderItem()
        {
            String[] txt = txtSearchItem.Text.Split('#');
            if (txt.Length <= 1)
            {
                lfSbMessage.Text = "no item";
                txtItemCode.Value = "";
                lbItemName.Text = "";
                txtItemQTY.Value = "1";
                return;
            }
            String name = txt[0].Trim();
            String code = txt[1].Trim();
            PharmacyM01 drug = new PharmacyM01();
            String name1 = bc.bcDB.pharM01DB.SelectNameByPk(code);
            txtItemCode.Value = code;
            lbItemName.Text = name1;
            txtItemQTY.Visible = false;
            txtItemQTY.Value = "1";
        }
        private void BtnDrugINZoom_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //pnDrugINimg.Dock = DockStyle.None;
            picDrugIN.Image = ZoomPicture(imgDrug, new Size(2, 2));

        }
        Image ZoomPicture(Image img, Size size)
        {
            Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width * size.Width), Convert.ToInt32(img.Height * size.Height));
            Graphics gpu = Graphics.FromImage(bm);
            gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bm;
        }
        private void ChkDrugWardDrug_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoDrug;
            clearControlOrder();
        }
        private void clearControlOrder()
        {
            txtItemCode.Value = "";
            txtSearchItem.Value = "";
            lbItemName.Text = "";
            txtItemRemark.Value = "";
        }
        private void TimeImgDrugINward_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TIMERCNT++;
            if (TIMERCNT % 9==0)
            {
                if (TC1ACTIVE.Equals(tabDrugWard.Name)) { /*setDrugINimgList();*/ }
                else if (TC1ACTIVE.Equals(tabOPD.Name))
                {
                    if (TCOPDACTIVE.Equals(tabOPDFinish.Name))
                    {
                        setGrfOPDFinishList();
                    }
                }
            }
            else if (TIMERCNT % 2 == 0)
            {
                DTSTCDRUG = bc.bcDB.pharT06DB.selectByCFRNoDrug();
                if (DTSTCDRUG.Rows.Count > 0)
                {
                    DTSTCDRUG = bc.bcDB.pharT06DB.selectByCFRNoDrug1(DTSTCDRUG.Rows[0]["MNC_CFR_YR"].ToString(), DTSTCDRUG.Rows[0]["MNC_CFR_NO"].ToString(), DTSTCDRUG.Rows[0]["MNC_DOC_CD"].ToString(), DTSTCDRUG.Rows[0]["MNC_CFG_DAT"].ToString());
                    setReportStricker(ref DTSTCDRUG, false);
                }
            }
        }
        private void setDrugINimgList()
        {
            try
            {
                timeImgDrugINward.Stop();
                lfSbMessage.Text = "setDrugINimgList";
                showLbLoading();
                FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, "u_drugin", "u_drugin");
                String[] listFile = ftpc.directoryListSimple("drugin");
                grpDrug.Tiles.Clear();
                foreach (String filename in listFile)
                {
                    MemoryStream streamCertiDtr = ftpc.download(filename);
                    streamCertiDtr.Position = 0;
                    Image image = (Image)new Bitmap((Stream)streamCertiDtr);
                    int width = image.Width;
                    int imgScanWidth = this.bc.imgScanWidth;
                    Image thumbnailImage = image.GetThumbnailImage(210, 230 * image.Height / width, (Image.GetThumbnailImageAbort)null, IntPtr.Zero);

                    var tile = new Tile();
                    tile.HorizontalSize = 2;
                    tile.VerticalSize = 3;
                    tile.Image = thumbnailImage;
                    tile.Text = filename;
                    tile.Text1 = filename;
                    tile.Template = tempFolder;
                    //tileitemdrug.Text3 = dr["MNC_LB_TYP_CD"].ToString();
                    tile.BackColor = Color.IndianRed;
                    tile.Tag = image;
                    tile.Click += Tile_Click;
                    grpDrug.Tiles.Add(tile);
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                lfSbMessage.Text = "";
                hideLbLoading();
            }
            timeImgDrugINward.Start();
        }
        private void Tile_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((Tile)sender).Tag != null)
            {
                //((Tile)sender).Image = imgCorr;
                foreach (Tile tile in grpDrug.Tiles)
                {
                    ((Tile)sender).Checked = false;
                    ((Tile)sender).BackColor = Color.IndianRed;
                }
                imgDrug = (Image)((Tile)sender).Tag;
                pnDrugINimg.Dock = DockStyle.Fill;
                picDrugIN.Image = imgDrug;
                newHeight = 0;
                ((Tile)sender).Checked = true;
                ((Tile)sender).BackColor = Color.SteelBlue;
            }
            else
            {
                //((Tile)sender).Image = null;
            }
            //setGrfItem(((Tile)sender).Text1, ((Tile)sender).Text, "drug");
        }
        private void initTileImg()
        {
            TileDrugINimg = new C1TileControl();
            TileDrugINimg.Orientation = LayoutOrientation.Horizontal;
            //TileFoods.DefaultTemplate.Elements.Add(peOrd);
            TileDrugINimg.Dock = DockStyle.Fill;

            //TileFoods[i].Templates.Add(this.tempFlickr);
            //TileFoods = new C1TileControl();
            //TileFoods[i].Name = "tile" + i;
            //TileFoods[i].Dock = DockStyle.Fill;
            //TileFoods[i].BackColor = tilecolor;     // tile color
            //pnOrder.Controls.Add(TileFoods);                    
            TileDrugINimg.ScrollOffset = 0;
            TileDrugINimg.SurfaceContentAlignment = System.Drawing.ContentAlignment.TopLeft;
            TileDrugINimg.Padding = new System.Windows.Forms.Padding(0);
            TileDrugINimg.GroupPadding = new System.Windows.Forms.Padding(10);
            //TileDrugINimg.BackColor = backColor;
            TileDrugINimg.TileBackColor = backColor;
            TileDrugINimg.CheckBackColor = checkBackColor;
            TileDrugINimg.HotBackColor = hotBackColor;
            TileDrugINimg.HotCheckBackColor = hotCheckBackColor;

            grpDrug = new Group();
            TileDrugINimg.Groups.Add(grpDrug);
            TileCollection tiles = TileDrugINimg.Groups[0].Tiles;
            imgTopTilesLab = new C1.Win.C1Tile.ImageElement();
            imgTopTilesLab.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            txtTopTilesLab = new C1.Win.C1Tile.TextElement();
            imgFolder = new C1.Win.C1Tile.ImageElement();
            imgFolder.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);

            pnTopTilesLab = new C1.Win.C1Tile.PanelElement();
            pnTopTilesLab.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            pnTopTilesLab.Children.Add(imgTopTilesLab);
            pnTopTilesLab.Children.Add(txtTopTilesLab);
            pnTopTilesLab.ChildSpacing = 2;
            pnTopTilesLab.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);

            tempFolder = new C1.Win.C1Tile.Template();
            tempFolder.Elements.Add(pnTopTilesLab);
            //tempFolder.Elements.Add(imgFolder);

            pnDrugINimgList.Controls.Add(TileDrugINimg);
        }
        private void initLoading()
        {
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);
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
        private void FrmPharmacy_Load(object sender, EventArgs e)
        {
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            int scrH = Screen.PrimaryScreen.Bounds.Height;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Maximized;

            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            scDrugIN.HeaderHeight = 0;
            spOrder.HeaderHeight = 0;
            setDrugINimgList();
            
            timeImgDrugINward.Enabled = true;
            timeImgDrugINward.Stop();
            autoDrug = bc.bcDB.pharM01DB.getlDrugAll();            
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoDrug;
            pnDrugINimg.Dock = DockStyle.Fill;
            picDrugIN.Dock = DockStyle.Fill;
            picDrugIN.SizeMode = PictureBoxSizeMode.StretchImage;
            DEPTNO = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
            String stationname = bc.bcDB.pm32DB.getDeptName(bc.iniC.station);
            lfSbStation.Text = DEPTNO + "[" + bc.iniC.station + "]" + stationname;
            rgSbModule.Text = bc.iniC.hostDBMainHIS + " " + bc.iniC.nameDBMainHIS;
            this.Text = "Last Update 2024-03-07";
        }
    }
}
