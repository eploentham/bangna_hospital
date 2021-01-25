using AutocompleteMenuNS;
using bangna_hospital.control;
using bangna_hospital.FlexGrid;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.C1Pdf;
using C1.Win.C1Command;
using C1.Win.C1Document;
using C1.Win.C1Document.Export;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1Ribbon;
using C1.Win.C1SplitContainer;
using C1.Win.FlexViewer;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    /*
     * 62-11-18     0001    รูปใน scanView เรียงไม่ถูกต้อง
     */
    public partial class FrmScanView1 : Form
    {
        BangnaControl bc;

        Font fEdit, fEditB, fEdit3B, fEdit5B;
        C1DockingTab tcDtr, tcVs, tcHnLabOut, tcMac, tcPrnEmail;
        C1DockingTabPage tabStfNote, tabOrder, tabScan, tabLab, tabXray, tablabOut, tabOPD, tabIPD, tabPrn, tabMac, tabHnLabOut, tabPic, tabOrdAdd, tabPrnEmailDrug, tabPrnEmailLab, tabPrnEmailXray, tabPrnEmailSummary,  tabPrnEmailOther;
        C1FlexGrid grfOrder, grfScan, grfLab, grfXray, grfPrn, grfHn, grfPic, grfIPD, grfOPD;
        C1FlexGrid grfOrdDrug, grfOrdSup, grfOrdLab, grfOrdXray, grfOrdOR, grfOrdItem, grfPrnEmailImg;
        C1FlexViewer labOutView, fvPrnEmailSummary, fvPrnEmailDrug, fvPrnEmailLab, fvPrnEmailXray, fvPrnEmailOther;
        List<C1DockingTabPage> tabHnLabOutR;
        Panel pnOrdSearchDrug, pnOrdSearchSup, pnOrdSearchLab, pnOrdSearchXray, pnOrdSearchOR, pnOrdItem, pnscOrdItem, pnOrdDiagVal, pnPrnEmail, pnPrnEmailGrfPrn;
        Label lbPttVitalSigns, lbPttPressure, lbPttTemp, lbPttWeight, lbPttHigh, lbPttBloodGroup, lbPttCC, lbPttCCin, lbPttCCex, lbPttAbc, lbPttHC, lbPttBp1, lbPttBp2, lbPttHrate, lbPttLRate;
        Label lbPttSymptom, lbPttVsDate, lbPaidType, lbLoading, lbDocAll, lbDocGrp1, lbDocGrp2, lbDocGrp3, lbDocGrp4, lbDocGrp5, lbDocGrp6, lbDocGrp7, lbDocGrp8;
        
        C1TextBox txtItmRowNo, txtItmId, txtItmName, txtItmQty, txtItmFre, txtItmIn1, txtItmIn2, txtPrnEmailTo, txtPrnEmailSubject, txtPrnEmailBody, txtItmFlag;
        C1Button btnItmSend, btnItmDrugSet, btnItmSave, btnPrnFile, btnPrn, btnSearch;
        C1SplitterPanel scOrdItem = new C1.Win.C1SplitContainer.C1SplitterPanel();
        C1SplitterPanel scOrdItemGrf = new C1.Win.C1SplitContainer.C1SplitterPanel();
        C1SplitContainer sCOrdItem = new C1.Win.C1SplitContainer.C1SplitContainer();
        C1DateEdit txtPrnDateStart, txtPrnDateEnd;

        C1Button btnDocOk, btnDocExport, btnPrnEmailImgBrow, btnPrnEmailImgOpen, btnPrnEmailImgSend;
        Label lbDocGrp, lbDocSubGrp, lbDocAn, lbPrnDateStart, lbPrnDateEnd, label11, labe2, lbtxtPrnEmailTo, lbtxtPrnEmailSubject, lbtxtPrnEmailBody;
        C1ComboBox cboDocGrp, cboDocSubGrp;

        int colVsVsDate = 1, colVsDept = 2, colVsVn = 3, colVsStatus = 4, colVsPreno = 5, colVsAn = 6, colVsAndate = 7, colVsPaidType=8, colVsHigh=9, colVsWeight=10, colVsTemp=11, colVscc=12, colVsccin=13, colVsccex=14, colVsabc=15, colVshc16=16, colVsbp1r=17, colVsbp1l=18, colVsbp2r=19, colVsbp2l=20, colVsVital=21, colVsPres=22, colVsRadios=23, colVsBreath=24;
        int colIPDDate = 1, colIPDDept = 2, colIPDAnShow = 4, colIPDStatus = 3, colIPDPreno = 5, colIPDVn = 6, colIPDAndate = 7, colIPDAnYr = 8, colIPDAn = 9;
        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
        int colOrderId = 1, colOrderDate = 2, colOrderName = 3, colOrderQty = 4, colOrderFre=5, colOrderIn1=6, colOrderMed = 7;
        int colLabDate = 1, colLabName = 2, colLabNameSub = 3, colLabResult = 4, colInterpret = 5, colNormal = 6, colUnit = 7;
        int colXrayDate = 1, colXrayCode = 2, colXrayName = 3, colXrayResult = 4;
        int colLabOutDateReq = 1, colLabOutHN = 2, colLabOutFullName = 3, colLabOutVN = 4, colLabOutDateReceive = 5, colLabOutReqNo = 6, colLabOutId = 7;
        int colDrugAllCode = 1, colDrugAllName = 2, colDrugAllDsc = 3, colDrugAllAlg=4;
        int colOrdDrugId = 1, colOrdDrugNameT = 2, colOrdDrugUnit = 3, colOrdDrugtypcd = 4;
        int colOrdXrayId = 1, colOrdXrayName = 2, colOrdXrayUnit = 3, colXraytypcd = 4, colXraygrpcd = 5, colXraygrpdsc = 6;
        int colOrdLabId = 1, colOrdLabName = 2, colOrdlabUnit = 3, colLabtypcd = 4, colLabgrpcd = 5, colLabgrpdsc = 6;

        int colOrdAddId = 1, colOrdAddNameT = 2, colOrdAddUnit = 3, colOrdAddQty=4, colOrdAddDrugFr=5, colOrdAddDrugIn=6, colOrdDrugIn1=7, colOrdAddItemType=8, colOrdAddRowNo=9, colOrdAddFlag=10;        // order add

        int colPrnlabHn = 1, colPrnlabName = 2, colPrnlabVN = 3, colPrnlabAN = 4, colPrnlabReqDate = 5, colPrnlabReqNo = 6;
        int colPrnSSOchk = 1, colPrnSSOVn = 2, colPrnSSOvsDate = 3, colPrnSSODesc = 4, colPrnSSOpreno = 5;
        int colPrnEmailDocCD = 1, colPrnEmailDocYr = 2, colPrnEmailDocNo = 3, colPrnEmailDocDat = 4, colPrnEmailDate = 5, colPrnEmailPaidName=6, colPrnEmailStatusOPD=7, colPrnEmailAn=8, colPrnEmailAnYr=9;
        int colPrnEmailImgimage = 1, colPrnEmailImgFilename = 2;

        int newHeight = 720;
        int mouseWheel = 0;
        int originalHeight = 0;
        ArrayList array1 = new ArrayList();
        List<listStream> lStream, lStreamPic;
        listStream strm;
        Image resizedImage, img, imgLR;
        C1PictureBox pic, picL, picR;
        private RadioButton chkPrnAll, chkPrnCri, chkPrnLab, chkXray, chkPrnSSO, chkPrnEmail, chkPrnFAX;
        C1TextBox txtPrnCri;
        C1CheckBox chkPrnSSOall, chkPrnEmailSummary, chkPrnEmailDrug, chkPrnEmailLab, chkPrnEmailXray;
        //FlowLayoutPanel fpL, fpR;
        //SplitContainer sct;
        C1SplitContainer sct, sCPrn, sCOrdDiag;
        C1SplitterPanel cspL, cspR, cspPrnLeft, cspPrnRight, scOrdDiag1, scOrdDiag2, scOrdDiag3;
        RichTextBox rtb;
        //VScrollBar vScroller;
        //int y = 0;
        Form frmImg;
        String dsc_id = "", hn = "", flagShowBtnSearch="", preno="",vsDate = "", docgrpid="", emailSummary="", emailDrug="", emailLab="", emailXray="";
        //Timer timer1;
        Patient ptt;
        Stream streamPrint, streamPrintL, streamPrintR, streamDownload;
        Form frmFlash;
        String grfActive = "", txtChronic = "";
        DataTable dtchronic = new DataTable();
        Color colorLbDoc;
        Boolean pageLoadGrfPrn = false;
        AutocompleteMenu acmEmailCsh;

        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        //[STAThread]
        //private void txtStatus(String msg)
        //{
        //    txt.Invoke(new EventHandler(delegate
        //    {
        //        txt.Value = msg;
        //    }));
        //}
        Boolean flagTabOutlabLoad = false, flagTabDtrOrdLoad=false, flagTabOrderLoad=false, flagTabPrn = false, flagTabPic = false, flagtabScan=false;
        Boolean flagTabPrnEmailSummary = false, flagTabPrnEmailDrug = false, flagTabPrnEmailLab = false, flagTabPrnEmailXray = false, flagTabPrnEmailOther = false;
        public FrmScanView1(BangnaControl bc,String flagShoSearch)
        {
            InitializeComponent();
            this.bc = bc;
            this.flagShowBtnSearch = flagShoSearch;
            initConfig();
        }
        public FrmScanView1(BangnaControl bc, String hn, String flagShowBtnSearch)
        {
            //new LogWriter("d", "FrmScanView1 InitializeComponent start");
            InitializeComponent();
            //new LogWriter("d", "FrmScanView1 InitializeComponent end");
            //MessageBox.Show("22", "");
            this.bc = bc;
            this.hn = hn;
            this.flagShowBtnSearch = flagShowBtnSearch;
            //new LogWriter("d", "FrmScanView1 initConfig start");
            initConfig();
            //new LogWriter("d", "FrmScanView1 initConfig end");
        }
        private void initConfig()
        {
            //this.FormBorderStyle = FormBorderStyle.None;

            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize+3, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            array1 = new ArrayList();
            lStream = new List<listStream>();
            lStreamPic = new List<listStream>();
            strm = new listStream();
            tabHnLabOutR = new List<C1DockingTabPage>();

            ptt = new Patient();
            //timer1 = new Timer();
            //int chk = 0;
            //int.TryParse(bc.iniC.timerImgScanNew, out chk);
            //timer1.Interval = chk;
            //timer1.Enabled = true;
            //timer1.Tick += Timer1_Tick;
            //timer1.Stop();
            txtName.Font = fEdit3B;
            lbDrugAllergy.Font = fEdit3B;

            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);

            //theme1.SetTheme(sb1, bc.iniC.themeApplication);
            theme1.SetTheme(gbPtt, bc.iniC.themeApplication);
            theme1.SetTheme(panel2, bc.iniC.themeApplication);
            theme1.SetTheme(panel3, bc.iniC.themeApplication);
            theme1.SetTheme(sC1, bc.iniC.themeApplication);
            foreach (Control con in gbPtt.Controls)
            {
                if (con is C1PictureBox) continue;
                //if (con.Name.Equals("lbAge")) continue;
                theme1.SetTheme(con, bc.iniC.themeApplication);
            }
            //theme1.SetTheme(lbAge, bc.iniC.themeApplication);
            //foreach (Control con in grfScan.Controls)
            //{
            //    theme1.SetTheme(con, "ExpressionDark");
            //}

            txtHn.Value = hn;
            //ptt = bc.bcDB.pttDB.selectPatinet(txtHn.Text.Trim());
            //txtName.Value = ptt.Name;
            //String allergy = "";
            //DataTable dt = new DataTable();
            //dt = bc.bcDB.vsDB.selectDrugAllergy(txtHn.Text.Trim());
            //foreach (DataRow row in dt.Rows)
            //{
            //    allergy += row["MNC_ph_tn"].ToString() + " " + row["MNC_ph_memo"].ToString() + ", ";
            //}
            //lbDrugAllergy.Text = "";
            //if (allergy.Length > 0)
            //{
            //    lbDrugAllergy.Text = "แพ้ยา " + allergy;
            //}
            //else
            //{
            //    lbDrugAllergy.Text = "ไม่มีข้อมูล การแพ้ยา ";
            //}
            //lbAge.Text = "อายุ "+ptt.AgeStringShort();
            btnHn.Click += BtnHn_Click;

            
            //btnRefresh.Click += BtnRefresh_Click;
            txtHn.KeyUp += TxtHn_KeyUp;
            //picExit.Click += PicExit_Click;
            //tcDtr.SelectedTabChanged += TcDtr_SelectedTabChanged;
            //sC1.TabIndexChanged += SC1_TabIndexChanged;
            //tcDtr.TabClick += TcDtr_TabClick;
            
            //MessageBox.Show("111", "");
            initTabDtr();
            initTabVS();
            initGrfOPD();
            initGrfIPD();
            
            //initGrfPicture();
            //initTabPrn();
            //initGrf();

            setPicStaffNote();
            setControlHN();
            //theme1.SetTheme(tcDtr, theme1.Theme);
            //MessageBox.Show("222", "");
            //setTabMachineResult();

            tabHnLabOut.DoubleClick += TabHnLabOut_DoubleClick;
        }

        private void TabHnLabOut_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmWaiting frmW = new FrmWaiting();
            frmW.StartPosition = FormStartPosition.CenterScreen;
            frmW.Show(this);

            setTabHnLabOut();

            frmW.Dispose();
        }

        private void BtnItmDrugSet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmDoctorDrugSet frmDrugSet = new FrmDoctorDrugSet(bc, txtHn.Text.Trim());
            frmDrugSet.WindowState = FormWindowState.Normal;
            frmDrugSet.StartPosition = FormStartPosition.CenterScreen;
            frmDrugSet.Size = new Size(1200, 800);

            //C1FlexViewer flexv = new C1FlexViewer();
            //flexv.AutoScrollMargin = new System.Drawing.Size(0, 0);
            //flexv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            //flexv.Dock = System.Windows.Forms.DockStyle.Fill;
            //flexv.Location = new System.Drawing.Point(0, 0);
            //flexv.Name = "flexv";
            //flexv.Size = new System.Drawing.Size(1065, 790);
            //flexv.TabIndex = 0;
            //C1PdfDocumentSource pds = new C1PdfDocumentSource();
            //MemoryStream stream = new MemoryStream();
            //FileStream file = new FileStream("d:\\picture\\ออกตรวจ_26052563.pdf", FileMode.Open, FileAccess.Read);
            //file.CopyTo(stream);
            //stream.Seek(0, SeekOrigin.Begin);
            //pds.LoadFromStream(stream);
            //flexv.DocumentSource = pds;
            //frmDrugSet.Controls.Add(flexv);
            
            frmDrugSet.ShowDialog(this);
        }
        private void PicExit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Close();
        }
        private void initTabDtr()
        {
            tcDtr = new C1DockingTab();
            tcDtr.Dock = System.Windows.Forms.DockStyle.Fill;
            tcDtr.Location = new System.Drawing.Point(0, 266);
            tcDtr.Name = "tcDtr";
            tcDtr.Size = new System.Drawing.Size(669, 200);
            tcDtr.TabIndex = 0;
            tcDtr.TabsSpacing = 5;
            tcDtr.TabClick += TcDtr_TabClick;

            tabStfNote = new C1DockingTabPage();
            tabStfNote.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabStfNote.Size = new System.Drawing.Size(667, 175);
            tabStfNote.TabIndex = 0;
            tabStfNote.Text = "ใบยา / Staff's Note";
            tabStfNote.Name = "tabStfNote";
            tcDtr.Controls.Add(tabStfNote);
            
            tabOrder = new C1DockingTabPage();
            tabOrder.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOrder.Size = new System.Drawing.Size(667, 175);
            tabOrder.TabIndex = 0;
            tabOrder.Text = "ประวัติการสั่งการ";
            tabOrder.Name = "tabOrder";
            tcDtr.Controls.Add(tabOrder);

            tabScan = new C1DockingTabPage();
            tabScan.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabScan.Size = new System.Drawing.Size(667, 175);
            tabScan.TabIndex = 0;
            tabScan.Text = "เวชระเบียน Scan";
            tabScan.Name = "tabPageScan";

            tcDtr.Controls.Add(tabScan);

            tabLab = new C1DockingTabPage();
            tabLab.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabLab.Size = new System.Drawing.Size(667, 175);
            tabLab.TabIndex = 0;
            tabLab.Text = "ผล LAB";
            tabLab.Name = "tabPageLab";

            tcDtr.Controls.Add(tabLab);

            tabXray = new C1DockingTabPage();
            tabXray.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabXray.Size = new System.Drawing.Size(667, 175);
            tabXray.TabIndex = 0;
            tabXray.Text = "ผล x-Ray";
            tabXray.Name = "tabPageXray";

            tcDtr.Controls.Add(tabXray);

            tabPrn = new C1DockingTabPage();
            tabPrn.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabPrn.Size = new System.Drawing.Size(667, 175);
            tabPrn.TabIndex = 0;
            tabPrn.Text = "Print Email FAX";
            tabPrn.Name = "tabPrn";
            tcDtr.Controls.Add(tabPrn);

            tabMac = new C1DockingTabPage();
            tabMac.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabMac.Size = new System.Drawing.Size(667, 175);
            tabMac.TabIndex = 0;
            //tabHn.Text = "Hn เวชระเบียน";
            tabMac.Text = "Machine Result";
            tabMac.Name = "tabHn";
            tcDtr.Controls.Add(tabMac);

            tabHnLabOut = new C1DockingTabPage();
            tabHnLabOut.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabHnLabOut.Size = new System.Drawing.Size(667, 175);
            tabHnLabOut.TabIndex = 0;
            tabHnLabOut.Text = "Hn Out Lab";
            tabHnLabOut.Name = "tabHnLabOut";
            tcDtr.Controls.Add(tabHnLabOut);

            //Panel pntabHnLabOut = new Panel();
            //pntabHnLabOut.Dock = DockStyle.Fill;
            //tabHnLabOut.Controls.Add(pntabHnLabOut);

            tcHnLabOut = new C1DockingTab();
            tcHnLabOut.Dock = System.Windows.Forms.DockStyle.Fill;
            tcHnLabOut.Location = new System.Drawing.Point(0, 266);
            tcHnLabOut.Name = "tcHnLabOut";
            tcHnLabOut.Size = new System.Drawing.Size(669, 200);
            tcHnLabOut.TabIndex = 0;
            tcHnLabOut.TabsSpacing = 5;
            tcHnLabOut.DoubleClick += TcHnLabOut_DoubleClick;
            tabHnLabOut.Controls.Add(tcHnLabOut);
            theme1.SetTheme(tcHnLabOut, bc.iniC.themeApplication);

            tcMac = new C1DockingTab();
            tcMac.Dock = System.Windows.Forms.DockStyle.Fill;
            tcMac.Location = new System.Drawing.Point(0, 266);
            tcMac.Name = "tcMac";
            tcMac.Size = new System.Drawing.Size(669, 200);
            tcMac.TabIndex = 0;
            tcMac.TabsSpacing = 5;
            tcMac.DoubleClick += TcMac_DoubleClick;
            tabMac.Controls.Add(tcMac);
            theme1.SetTheme(tcMac, bc.iniC.themeApplication);

            tabPic = new C1DockingTabPage();
            tabPic.Location = new Point(1, 24);
            tabPic.Size = new Size(667, 175);
            tabPic.TabIndex = 0;
            tabPic.Text = "Picture";
            tabPic.Name = "tabPic";
            tcDtr.Controls.Add(tabPic);

            tabOrdAdd = new C1DockingTabPage();
            tabOrdAdd.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOrdAdd.Size = new System.Drawing.Size(667, 175);
            tabOrdAdd.TabIndex = 0;
            tabOrdAdd.Text = "สั่งยา & Diagnose";
            tabOrdAdd.Name = "tabOrdAdd";
            tcDtr.Controls.Add(tabOrdAdd);
            panel3.Controls.Add(tcDtr);
            theme1.SetTheme(tcDtr, bc.iniC.themeApplication);
        }

        private void TcMac_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void TcHnLabOut_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if (tcHnLabOut.SelectedTab == tabImage)
            //{

            //}
            String filename = "";
            //((C1DockingTab)sender).SelectedTab.Name 
            if (((C1DockingTab)sender).SelectedTab == null) return;
            filename = ((C1DockingTab)sender).SelectedTab.Name;
            String[] txt1 = filename.Split('_');
            if (txt1.Length > 1)
            {
                filename = "report\\" + txt1[1] + ".pdf";
                if (File.Exists(filename))
                {
                    //bool isExists = System.IO.File.Exists(filename);
                    //if (isExists)
                    System.Diagnostics.Process.Start(filename);
                }
                else
                {
                    MessageBox.Show("ไม่พบ File Out Lab", "");
                }
            }
        }
        private void initTabVS()
        {
            tcVs = new C1DockingTab();
            tcVs.Dock = System.Windows.Forms.DockStyle.Fill;
            tcVs.Location = new System.Drawing.Point(0, 266);
            tcVs.Name = "c1DockingTab1";
            //tcVs.Size = new System.Drawing.Size(669, 200);
            tcVs.TabIndex = 0;
            tcVs.TabsSpacing = 5;
            panel2.Controls.Add(tcVs);
            theme1.SetTheme(tcVs, bc.iniC.themeApplication);

            tabOPD = new C1DockingTabPage();
            tabOPD.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            //tabOPD.Size = new System.Drawing.Size(667, 175);
            tabOPD.TabIndex = 0;
            tabOPD.Text = "OPD";
            tabOPD.Name = "tabOPD";
            tcVs.Controls.Add(tabOPD);

            tabIPD = new C1DockingTabPage();
            tabIPD.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            //tabIPD.Size = new System.Drawing.Size(667, 175);
            tabIPD.TabIndex = 0;
            tabIPD.Text = "IPD";
            tabIPD.Name = "tabIPD";
            tcVs.Controls.Add(tabIPD);

        }
        private void initGrfOrderLabXray()
        {
            grfOrder = new C1FlexGrid();
            grfOrder.Font = fEdit;
            grfOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOrder.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grfOrder.Cols[colOrderId].Visible = false;
            grfOrder.Rows.Count = 1;
            grfOrder.Cols.Count = 8;
            grfOrder.Cols[colOrderName].Caption = "ชื่อ";
            grfOrder.Cols[colOrderMed].Caption = "-";
            grfOrder.Cols[colOrderQty].Caption = "QTY";
            grfOrder.Cols[colOrderName].Width = 300;
            grfOrder.Cols[colOrderMed].Width = 200;
            grfOrder.Cols[colOrderQty].Width = 60;
            grfOrder.Name = "grfOrder";
            ContextMenu menuGwOrder = new ContextMenu();
            menuGwOrder.MenuItems.Add("ต้องการ Print ", new EventHandler(ContextMenu_print_Order));
            menuGwOrder.MenuItems.Add("ต้องการ Export PDF ", new EventHandler(ContextMenu_print_Order_export));
            grfOrder.ContextMenu = menuGwOrder;

            grfLab = new C1FlexGrid();
            grfLab.Font = fEdit;
            grfLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLab.Location = new System.Drawing.Point(0, 0);
            //grfLab.Rows[0].Visible = false;
            //grfLab.Cols[0].Visible = false;
            grfLab.Name = "grfLab";
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ Print ", new EventHandler(ContextMenu_print_lab));
            menuGw.MenuItems.Add("ต้องการ Export PDF ", new EventHandler(ContextMenu_print_lab_export));
            grfLab.ContextMenu = menuGw;
            grfLab.Rows.Count = 1;

            grfXray = new C1FlexGrid();
            grfXray.Font = fEdit;
            grfXray.Dock = System.Windows.Forms.DockStyle.Top;
            grfXray.Location = new System.Drawing.Point(0, 0);
            grfXray.Height = 120;
            grfXray.Click += GrfXray_Click;
            //grfLab.Cols[0].Visible = false;
            grfXray.Name = "grfXray";
            grfXray.Rows.Count = 1;
            ContextMenu menuGwX = new ContextMenu();
            menuGwX.MenuItems.Add("เปิด PACs infinitt", new EventHandler(ContextMenu_xray_infinitt));
            menuGwX.MenuItems.Add("พิมพ์ผล Xray", new EventHandler(ContextMenu_xray_result_print));
            menuGwX.MenuItems.Add("พิมพ์ผล Export PDF", new EventHandler(ContextMenu_xray_result_print_export));

            grfXray.ContextMenu = menuGwX;

            rtb = new RichTextBox();
            //rtb.Location = new Point(20, 20);
            rtb.Dock = DockStyle.Fill;
            //rtb.Height = 200;
            // Set background and foreground  
            rtb.BackColor = Color.Gray;
            rtb.ForeColor = Color.Black;
            rtb.Text = "";
            rtb.Name = "rtb";
            rtb.Font = fEdit3B;

            theme1.SetTheme(grfOrder, bc.iniC.themeApp);
            theme1.SetTheme(grfLab, bc.iniC.themeApp);
            theme1.SetTheme(grfXray, bc.iniC.themeApp);

            tabOrder.Controls.Add(grfOrder);
            tabLab.Controls.Add(grfLab);
            tabXray.Controls.Add(rtb);
            tabXray.Controls.Add(grfXray);
        }
        private void initGrfScan()
        {
            Panel pnScanTop = new Panel();
            Panel pnScan = new Panel();

            pnScanTop.Dock = DockStyle.Top;
            pnScanTop.Height = 30;
            pnScan.Dock = DockStyle.Fill;

            grfScan = new C1FlexGrid();
            grfScan.Font = fEdit;
            grfScan.Dock = System.Windows.Forms.DockStyle.Fill;
            grfScan.Location = new System.Drawing.Point(0, 0);
            grfScan.Rows[0].Visible = false;
            grfScan.Cols[0].Visible = false;
            grfScan.Rows.Count = 1;
            grfScan.Name = "grfScan";
            grfScan.Cols.Count = 5;
            Column colpic1 = grfScan.Cols[colPic1];
            colpic1.DataType = typeof(Image);
            Column colpic2 = grfScan.Cols[colPic2];
            colpic2.DataType = typeof(String);
            Column colpic3 = grfScan.Cols[colPic3];
            colpic3.DataType = typeof(Image);
            Column colpic4 = grfScan.Cols[colPic4];
            colpic4.DataType = typeof(String);
            grfScan.Cols[colPic1].Width = bc.grfScanWidth;
            grfScan.Cols[colPic2].Width = bc.grfScanWidth;
            grfScan.Cols[colPic3].Width = bc.grfScanWidth;
            grfScan.Cols[colPic4].Width = bc.grfScanWidth;
            grfScan.ShowCursor = true;
            grfScan.Cols[colPic2].Visible = false;
            grfScan.Cols[colPic3].Visible = true;
            grfScan.Cols[colPic4].Visible = false;
            grfScan.Cols[colPic1].AllowEditing = false;
            grfScan.Cols[colPic3].AllowEditing = false;
            grfScan.DoubleClick += Grf_DoubleClick;

            lbDocAll = new Label();
            bc.setControlLabel(ref lbDocAll, fEditB, "All", "lbDocAll", 20, 5);
            lbDocGrp1 = new Label();
            bc.setControlLabel(ref lbDocGrp1, fEditB, "DISCHARGE", "lbDocGrp1", lbDocAll.Width + 20, 5);
            lbDocGrp2 = new Label();
            bc.setControlLabel(ref lbDocGrp2, fEditB, "ADMISSION", "lbDocGrp2", lbDocGrp1.Location.X + lbDocGrp1.Width + 20, 5);
            lbDocGrp3 = new Label();
            bc.setControlLabel(ref lbDocGrp3, fEditB, "ORDER", "lbDocGrp3", lbDocGrp2.Location.X + lbDocGrp2.Width + 20, 5);
            lbDocGrp4 = new Label();
            bc.setControlLabel(ref lbDocGrp4, fEditB, "OPERATIVE", "lbDocGrp4", lbDocGrp3.Location.X + lbDocGrp3.Width + 50, 5);
            lbDocGrp5 = new Label();
            bc.setControlLabel(ref lbDocGrp5, fEditB, "INVESTIGATION", "lbDocGrp5", lbDocGrp4.Location.X + lbDocGrp4.Width + 40, 5);
            lbDocGrp6 = new Label();
            bc.setControlLabel(ref lbDocGrp6, fEditB, "NURSE", "lbDocGrp6", lbDocGrp5.Location.X + lbDocGrp5.Width + 60, 5);
            lbDocGrp7 = new Label();
            bc.setControlLabel(ref lbDocGrp7, fEditB, "MEDICATION", "lbDocGrp7", lbDocGrp6.Location.X + lbDocGrp6.Width + 40, 5);
            lbDocGrp8 = new Label();
            bc.setControlLabel(ref lbDocGrp8, fEditB, "OTHER", "lbDocGrp8", lbDocGrp7.Location.X + lbDocGrp7.Width + 40, 5);
            lbDocAll.Click += LbDocAll_Click;
            lbDocGrp1.Click += LbDocAll_Click;
            lbDocGrp2.Click += LbDocAll_Click;
            lbDocGrp3.Click += LbDocAll_Click;
            lbDocGrp4.Click += LbDocAll_Click;
            lbDocGrp5.Click += LbDocAll_Click;
            lbDocGrp6.Click += LbDocAll_Click;
            lbDocGrp7.Click += LbDocAll_Click;
            lbDocGrp8.Click += LbDocAll_Click;
            //grfScan.AutoSizeRows();
            //grfScan.AutoSizeCols();
            //tabScan.Controls.Add(grfScan);

            //theme1.SetTheme(grfOrder, "Office2016Black");

            //theme1.SetTheme(grfLab, "Office2016Black");
            //theme1.SetTheme(grfXray, "Office2016Black");
            colorLbDoc = lbDocAll.ForeColor;
            docgrpid = "1100000099";
            setForColorLbDocGrp();
            lbDocAll.ForeColor = Color.Red;
            pnScanTop.Controls.Add(lbDocAll);
            pnScanTop.Controls.Add(lbDocGrp1);
            pnScanTop.Controls.Add(lbDocGrp2);
            pnScanTop.Controls.Add(lbDocGrp3);
            pnScanTop.Controls.Add(lbDocGrp4);
            pnScanTop.Controls.Add(lbDocGrp5);
            pnScanTop.Controls.Add(lbDocGrp6);
            pnScanTop.Controls.Add(lbDocGrp7);
            pnScanTop.Controls.Add(lbDocGrp8);
            tabScan.Controls.Add(pnScan);
            tabScan.Controls.Add(pnScanTop);
            pnScan.Controls.Add(grfScan);
            //initGrfPrn();
            //initGrfHn();
        }
        private void setForColorLbDocGrp()
        {
            lbDocAll.ForeColor = colorLbDoc;
            lbDocGrp1.ForeColor = colorLbDoc;
            lbDocGrp2.ForeColor = colorLbDoc;
            lbDocGrp3.ForeColor = colorLbDoc;
            lbDocGrp4.ForeColor = colorLbDoc;
            lbDocGrp5.ForeColor = colorLbDoc;
            lbDocGrp6.ForeColor = colorLbDoc;
            lbDocGrp7.ForeColor = colorLbDoc;
            lbDocGrp8.ForeColor = colorLbDoc;
        }
        private void LbDocAll_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setForColorLbDocGrp();
            ((Label)sender).ForeColor = Color.Red;
            if (((Label)sender).Name.Equals("lbDocAll"))
            {
                docgrpid = "1100000099";
            }
            else if (((Label)sender).Name.Equals("lbDocGrp1"))
            {
                docgrpid = "1100000000";
            }
            else if (((Label)sender).Name.Equals("lbDocGrp2"))
            {
                docgrpid = "1100000001";
            }
            else if (((Label)sender).Name.Equals("lbDocGrp3"))
            {
                docgrpid = "1100000002";
            }
            else if (((Label)sender).Name.Equals("lbDocGrp4"))
            {
                docgrpid = "1100000003";
            }
            else if (((Label)sender).Name.Equals("lbDocGrp5"))
            {
                docgrpid = "1100000004";
            }
            else if (((Label)sender).Name.Equals("lbDocGrp6"))
            {
                docgrpid = "1100000005";
            }
            else if (((Label)sender).Name.Equals("lbDocGrp7"))
            {
                docgrpid = "1100000006";
            }
            else if (((Label)sender).Name.Equals("lbDocGrp8"))
            {
                docgrpid = "1100000007";
            }
            setGrfScan();
        }

        private void ContextMenu_print_Order(object sender, System.EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = setPrintOrder();
            FrmReport frm = new FrmReport(bc, this, "pharmacy_result", dt);
        }
        private void ContextMenu_print_Order_export(object sender, System.EventArgs e)
        {
            showLbLoading();

            DataTable dt = new DataTable();
            dt = setPrintOrder();

            bc.exportResultPharmacy(dt, txtHn.Text.Trim(), txtVN.Text.Trim(),"open");
            hideLbLoading();
        }
        private void setTabPrnEmailOrder()
        {
            String filename = "";
            DataTable dt = new DataTable();
            dt = setPrintOrder();
            if (dt.Rows.Count <= 0) return;
            filename = bc.exportResultPharmacy(dt, txtHn.Text.Trim(), txtVN.Text.Trim(), "");
            if (File.Exists(filename))
            {
                C1PdfDocumentSource pds = new C1PdfDocumentSource();
                emailDrug = filename;
                pds.LoadFromFile(filename);
                fvPrnEmailDrug.DocumentSource = pds;
            }
        }
        private DataTable setPrintOrder()
        {
            DataTable dt = new DataTable();
            //if (grfOrdLab == null) return dt;
            //if (grfOrdLab.Row <= 1) return dt;
            //if (grfOrdLab.Col <= 0) return dt;
            String an = "", vn = "", vsdate = "", xraycode = "", txt1 = "", reqdate = "", reqno = "", dtrname = "", ordname = "", orddetail = "", dtrxrname = "", resdate = "", pttcompname = "", paidname = "", depname = "";

            DataTable dtOrder = new DataTable();
            dtOrder = getDataTableOrder("print");

            dtOrder.Columns.Add("patient_name", typeof(String));
            dtOrder.Columns.Add("patient_hn", typeof(String));
            dtOrder.Columns.Add("patient_age", typeof(String));
            dtOrder.Columns.Add("request_no", typeof(String));
            dtOrder.Columns.Add("patient_vn", typeof(String));
            dtOrder.Columns.Add("doctor", typeof(String));
            dtOrder.Columns.Add("result_date", typeof(String));
            dtOrder.Columns.Add("print_date", typeof(String));
            //dt.Columns.Add("user_lab", typeof(String));
            //dt.Columns.Add("user_check", typeof(String));
            //dt.Columns.Add("user_report", typeof(String));
            dtOrder.Columns.Add("patient_dep", typeof(String));
            dtOrder.Columns.Add("patient_company", typeof(String));
            //dt.Columns.Add("ptt_department", typeof(String));
            dtOrder.Columns.Add("patient_type", typeof(String));
            dtOrder.Columns.Add("mnc_lb_dsc", typeof(String));
            dtOrder.Columns.Add("mnc_lb_grp_cd", typeof(String));
            dtOrder.Columns.Add("sort1", typeof(String));
            //dtOrder.Columns.Add("request_no", typeof(String));
            foreach (DataRow drow in dtOrder.Rows)
            {
                Boolean chkname = false;
                chkname = txtName.Text.Any(c => !Char.IsLetterOrDigit(c));
                if (chkname)
                {
                    drow["patient_age"] = ptt.AgeString().Replace("Year", "ปี").Replace("Month", "เดือน").Replace("Days", "วัน").Replace("s", "");
                }
                else
                {
                    drow["patient_age"] = ptt.AgeString();
                }
                drow["patient_name"] = ptt.Name;
                drow["patient_hn"] = ptt.Hn;
                drow["patient_company"] = "";
                //drow["patient_age"] = ptt.Name;
                drow["patient_vn"] = txtVN.Text;
                //drow["patient_dep"] = ptt.Name;
                drow["patient_type"] = "";
                //drow["request_no"] = drow["MNC_REQ_NO"].ToString() + "/" + bc.datetoShow(drow["mnc_req_dat"].ToString());
                drow["doctor"] = "";
                drow["request_no"] = drow["MNC_REQ_NO"].ToString()+"/"+ drow["MNC_REQ_yr"].ToString();
                drow["patient_visitdate"] = bc.datetoShow(drow["patient_visitdate"].ToString());
                drow["request_date"] = bc.datetoShow(drow["request_date"].ToString());
                //drow["print_date"] = bc.datetoShow(dtreq.Rows[0]["MNC_STAMP_DAT"].ToString());
                //drow["user_lab"] = drow["user_lab"].ToString() + " [ทน." + drow["MNC_USR_NAME_result"].ToString() + "]";
                //drow["user_report"] = drow["user_report"].ToString() + " [ทน." + drow["MNC_USR_NAME_report"].ToString() + "]";
                //drow["user_check"] = drow["user_check"].ToString() + " [ทน." + drow["MNC_USR_NAME_approve"].ToString() + "]";
                //drow["patient_dep"] = dtreq.Rows[0]["MNC_REQ_DEP"].ToString().Equals("101") ? "OPD1" : depname.Equals("107") ? "OPD2" : depname.Equals("103") ? "OPD3" :
                //    depname.Equals("104") ? "ER" : depname.Equals("106") ? "WARD6" : depname.Equals("108") ? "WARD5W" : depname.Equals("109") ? "ล้างไต" :
                //    depname.Equals("105") ? "WARD5M" : depname.Equals("113") ? "ICU" : depname.Equals("114") ? "NS/LR" : depname.Equals("115") ? "ทันตกรรม" : depname.Equals("116") ? "CCU" : depname;
                //drow["mnc_lb_dsc"] = dtreq.Rows[0]["MNC_LB_DSC"].ToString();
                //drow["mnc_lb_grp_cd"] = dtreq.Rows[0]["MNC_LB_TYP_DSC"].ToString();
                //if (drow["MNC_RES_VALUE"].ToString().Equals("-"))
                //{
                //    drow["MNC_RES_UNT"] = "";
                //}
                //drow["MNC_RES_UNT"] = drow["MNC_RES_UNT"].ToString().Replace("0.00-0.00", "").Replace("0.00 - 0.00", "").Replace("0.00", "");
                //drow["sort1"] = drow["mnc_req_dat"].ToString().Replace("-", "").Replace("-", "") + drow["MNC_REQ_NO"].ToString();
            }
            foreach (DataRow drow in dt.Rows)
            {
                //MessageBox.Show(drow["sort1"].ToString(), "");
                if (drow["sort1"] == null)
                {
                    MessageBox.Show("11", "");
                }

                if (drow["sort1"].ToString().Equals(""))
                {
                    MessageBox.Show("22", "");
                }
            }
            return dtOrder;
        }
        private void setTabPrnEmailXray()
        {
            String datetick = "", pathFolder = "", filename="", ext = "";
            datetick = DateTime.Now.Ticks.ToString();
            pathFolder = bc.iniC.medicalrecordexportpath + "\\" + txtHn.Text.Trim() + "\\" + datetick;
            DataTable dt = new DataTable();
            dt = setPrintXray();
            if (dt.Rows.Count <= 0) return;
            filename = bc.exportResultXray(dt, txtHn.Text.Trim(), txtVN.Text.Trim(), "", pathFolder);
            if (ext.Equals(".jpg"))
            {
                filename = filename.Replace(".jpg", ".pdf");
            }
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            if (File.Exists(filename))
            {
                emailXray = filename;
                pds.LoadFromFile(filename);
                fvPrnEmailXray.DocumentSource = pds;
            }
        }
        private void ContextMenu_xray_result_print(object sender, System.EventArgs e)
        {

            //MessageBox.Show("reqdate ", reqdate);
            //MessageBox.Show("reqno ", reqno);
            //MessageBox.Show("dtrname ", dtrname);
            DataTable dt = new DataTable();
            dt = setPrintXray();
            FrmReport frm = new FrmReport(bc, this, "xray_result", dt);
        }
        private void ContextMenu_xray_result_print_export(object sender, System.EventArgs e)
        {
            showLbLoading();
            String datetick = "", pathFolder = "";
            datetick = DateTime.Now.Ticks.ToString();
            pathFolder = bc.iniC.medicalrecordexportpath + "\\" + txtHn.Text.Trim() + "\\" + datetick;
            DataTable dt = new DataTable();
            dt = setPrintXray();

            bc.exportResultXray(dt, txtHn.Text.Trim(), txtVN.Text.Trim(),"open", pathFolder);
        }
        private DataTable setPrintXraySSO(String vn, String preno, String vsdate)
        {
            DataTable dt = new DataTable();
            //if (grfXray == null) return dt;
            //if (grfXray.Row <= 0) return dt;
            //if (grfXray.Col <= 0) return dt;
            String an = "", xraycode = "", txt1 = "", reqdate = "", reqno = "", dtrname = "", ordname = "", orddetail = "", dtrxrname = "", resdate = "", pttcompname = "", paidname = "", depname = "";
            //MessageBox.Show("10 ", "");
            Boolean flagPACsPlus = false;
            //DataTable dt = new DataTable();
            DataTable dtreq = new DataTable();
            DataTable dtpacsplus = new DataTable();
            //MessageBox.Show("11 ", "");
            
            //vsdate = grfXray[grfXray.Row, colXrayDate] != null ? grfXray[grfXray.Row, colXrayDate].ToString() : "";
            //preno = txtVN.Text.Trim();
            //xraycode = grfXray[grfXray.Row, colXrayCode] != null ? grfXray[grfXray.Row, colXrayCode].ToString() : "";
            //vsdate = bc.datetoDB(vsdate);
            //dt = bc.bcDB.vsDB.selectResultXraybyVN1(txtHn.Text, preno, vsdate);
            //if (dt.Rows.Count <= 0)
            //{
            //    //MessageBox.Show("bc.bcDB.vsDB.selectResultXraybyVN1 ", "");
            //    dtpacsplus = bc.bcDB.xrDB.selectResultXrayPACsPlusbyVN1(txtHn.Text, this.preno, vsdate, xraycode);
            //    flagPACsPlus = (dtpacsplus.Rows.Count > 0) ? true : false;
            //}
            //else
            //{
            //    //MessageBox.Show("else bc.bcDB.vsDB.selectResultXraybyVN1 ", "");
            //    resdate = dt.Rows[0]["mnc_stamp_dat"].ToString();
            //    dtrxrname = dt.Rows[0]["dtr_name"].ToString();
            //}
            dtreq = bc.bcDB.vsDB.selectRequestXraybyVN2(txtHn.Text, preno, vsdate);
            //new LogWriter("d", "GrfXray_Click 00 "+ vsdate);
            
            if (dtreq.Rows.Count > 0)
            {
                reqdate = dtreq.Rows[0]["MNC_REQ_DAT"].ToString();
                reqno = dtreq.Rows[0]["MNC_REQ_NO"].ToString() + "/" + dtreq.Rows[0]["MNC_REQ_YR"].ToString();
                dtrname = dtreq.Rows[0]["dtr_name"].ToString() + "[" + dtreq.Rows[0]["mnc_dot_cd"].ToString() + "]";
                ordname = dtreq.Rows[0]["MNC_XR_DSC"].ToString();
                pttcompname = dtreq.Rows[0]["MNC_COM_DSC"].ToString();
                paidname = dtreq.Rows[0]["mnc_fn_typ_dsc"].ToString();
                depname = dtreq.Rows[0]["MNC_REQ_DEP"].ToString();

                foreach(DataRow drow in dtreq.Rows)
                {
                    xraycode = drow["mnc_xr_cd"] != null ? drow["mnc_xr_cd"].ToString() : "";
                    dt = bc.bcDB.vsDB.selectResultXraybyVN1(txtHn.Text, preno, vsdate, xraycode);

                    if (dt.Rows.Count <= 0)
                    {
                        String txt = "", studydesc = "", studydescold = "";
                        dtpacsplus = bc.bcDB.xrDB.selectResultXrayPACsPlusbyVN1(txtHn.Text, preno, vsdate, xraycode);
                        foreach (DataRow row in dtpacsplus.Rows)
                        {
                            dtrxrname = row["readingdr"].ToString();
                            studydesc = row["studydesc"].ToString();
                            txt += row["interpretation"].ToString();
                        }
                        dt.Rows.Add(dt.NewRow());
                        txt1 = txt;
                    }
                    else
                    {
                        txt1 = dt.Rows[0]["mnc_xr_dsc"].ToString();
                    }
                }
                flagPACsPlus = (dtpacsplus.Rows.Count > 0) ? true : false;
            }
            //if (flagPACsPlus)
            //{
            //    dt.Rows.Add(dt.NewRow());
            //    String txt = "", studydesc = "", studydescold = "";
            //    foreach (DataRow row in dtpacsplus.Rows)
            //    {
            //        resdate = row["readingdate"].ToString();
            //        if (resdate.Length >= 8)
            //        {
            //            resdate = resdate.Substring(0, 4) + "-" + resdate.Substring(4, 2) + "-" + resdate.Substring(resdate.Length - 2);
            //        }
            //        dtrxrname = row["readingdr"].ToString();
            //        studydesc = row["studydesc"].ToString();
            //        //studydescold = !studydesc.Equals(studydescold) ? row["studydesc"].ToString() : studydescold;
            //        if (!studydescold.Equals(studydesc))
            //        {
            //            txt += Environment.NewLine + Environment.NewLine + studydesc + Environment.NewLine + row["interpretation"].ToString();
            //            studydescold = studydesc;
            //        }
            //        else
            //        {
            //            txt += row["interpretation"].ToString();
            //        }
            //    }
            //    txt1 = txt;
            //}
            
            //MessageBox.Show("20 ", "");
            dt.Columns.Add("ptt_name", typeof(String));
            dt.Columns.Add("ptt_hn", typeof(String));
            dt.Columns.Add("ptt_age", typeof(String));
            dt.Columns.Add("xry_requestno", typeof(String));
            dt.Columns.Add("ptt_vn", typeof(String));
            dt.Columns.Add("xry_doctor", typeof(String));
            dt.Columns.Add("xry_request_date", typeof(String));
            dt.Columns.Add("xry_print", typeof(String));
            dt.Columns.Add("xry_orderdetail", typeof(String));
            dt.Columns.Add("xry_result", typeof(String));
            dt.Columns.Add("xry_conslution", typeof(String));
            dt.Columns.Add("xry_xraydoctor", typeof(String));
            dt.Columns.Add("ptt_comp", typeof(String));
            dt.Columns.Add("ptt_department", typeof(String));
            dt.Columns.Add("ptt_type", typeof(String));
            dt.Columns.Add("xry_ordername", typeof(String));
            dt.Columns.Add("xry_ordertetail", typeof(String));
            dt.Columns.Add("xry_result_date", typeof(String));
            foreach (DataRow drow in dt.Rows)
            {
                Boolean chkname = false;
                chkname = txtName.Text.Any(c => !Char.IsLetterOrDigit(c));
                if (chkname)
                {
                    drow["ptt_age"] = ptt.AgeString().Replace("Year", "ปี").Replace("Month", "เดือน").Replace("Days", "วัน").Replace("s", "");
                }
                else
                {
                    drow["ptt_age"] = ptt.AgeString();
                }
                drow["ptt_name"] = txtName.Text.Trim();
                drow["ptt_hn"] = txtHn.Text;

                drow["ptt_hn"] = txtHn.Text;
                //drow["xray_requestno"] = txt1;
                if (chkIPD.Checked)
                {
                    drow["ptt_vn"] = txtVN.Text;
                    if (flagPACsPlus)
                    {
                        drow["xry_result"] = txt1;
                        drow["xry_request_date"] = bc.datetoShow(reqdate);
                        drow["xry_requestno"] = reqno;
                        drow["xry_doctor"] = dtrname;
                        drow["xry_ordername"] = ordname;
                        drow["xry_xraydoctor"] = dtrxrname;
                        //drow["xry_xraydoctor"] = dtrxrname;
                        drow["xry_result_date"] = bc.datetoShow(resdate);
                    }
                    else
                    {
                        drow["xry_result"] = drow["mnc_xr_dsc"].ToString();//MNC_REQ_DAT
                        drow["xry_request_date"] = drow["MNC_REQ_DAT"].ToString();
                        drow["xry_requestno"] = drow["MNC_REQ_NO"].ToString();
                        drow["xry_doctor"] = drow["dtr_name"].ToString();
                        drow["xry_xraydoctor"] = drow["dtr_name_result"].ToString();
                        drow["xry_result_date"] = drow["mnc_stamp_dat"].ToString();
                    }
                }
                else
                {
                    drow["ptt_vn"] = txtVN.Text;
                    drow["xry_result"] = txt1;
                    drow["xry_request_date"] = bc.datetoShow(reqdate);
                    drow["xry_requestno"] = reqno;
                    drow["xry_doctor"] = dtrname;
                    drow["xry_ordername"] = ordname;
                    drow["xry_xraydoctor"] = dtrxrname;
                    //drow["xry_xraydoctor"] = dtrxrname;
                    drow["xry_result_date"] = bc.datetoShow(resdate);
                    //drow["ptt_comp"] = pttcompname;
                    //drow["ptt_type"] = paidname;
                }
                drow["ptt_comp"] = pttcompname;
                drow["ptt_type"] = paidname;
                drow["ptt_department"] = depname.Equals("101") ? "OPD1" : depname.Equals("107") ? "OPD2" : depname.Equals("103") ? "OPD3" :
                    depname.Equals("104") ? "ER" : depname.Equals("106") ? "WARD6" : depname.Equals("108") ? "WARD5W" : depname.Equals("109") ? "ล้างไต" :
                    depname.Equals("105") ? "WARD5M" : depname.Equals("113") ? "ICU" : depname.Equals("114") ? "NS/LR" : depname.Equals("115") ? "ทันตกรรม" : depname.Equals("116") ? "CCU" : depname;
            }
            return dt;
        }
        private DataTable setPrintXray()
        {
            DataTable dt = new DataTable();
            if (grfXray == null) return dt;
            if (grfXray.Row <= 0) return dt;
            if (grfXray.Col <= 0) return dt;
            String an = "", vn = "", vsdate = "", xraycode = "", txt1 = "", reqdate = "", reqno = "", dtrname = "", ordname = "", orddetail = "", dtrxrname = "", resdate = "", pttcompname = "", paidname = "", depname = "";
            //MessageBox.Show("10 ", "");
            Boolean flagPACsPlus = false;
            //DataTable dt = new DataTable();
            DataTable dtreq = new DataTable();
            DataTable dtpacsplus = new DataTable();
            //MessageBox.Show("11 ", "");
            if (chkIPD.Checked)
            {
                //an = grfXray[grfXray.Row, colIPDAnShow] != null ? grfXray[grfXray.Row, colIPDAnShow].ToString() : "";
                vsdate = grfXray[grfXray.Row, colXrayDate] != null ? grfXray[grfXray.Row, colXrayDate].ToString() : "";
                xraycode = grfXray[grfXray.Row, colXrayCode] != null ? grfXray[grfXray.Row, colXrayCode].ToString() : "";
                vsdate = bc.datetoDB(vsdate);
                String[] an1 = txtVN.Text.Trim().Split('/');
                if (an1.Length > 0)
                {
                    dt = bc.bcDB.vsDB.selectResultXraybyAN1(txtHn.Text, an1[0], an1[1], xraycode);
                    if (dt.Rows.Count > 0)
                    {
                        vsdate = dt.Rows[0]["mnc_date"].ToString();
                        vsdate = bc.datetoDB(vsdate);
                    }
                    else
                    {
                        dtpacsplus = bc.bcDB.xrDB.selectResultXrayPACsPlusbyVN1(txtHn.Text, this.preno, vsdate, xraycode);
                        flagPACsPlus = (dtpacsplus.Rows.Count > 0) ? true : false;
                    }
                }
                //  ต้องเปลี่ยน vs date เป็น แบบนี้
                vsdate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                vsdate = bc.datetoDB(vsdate);
                //ต้องถึงตาม OPD เพราะ IPD ยาก
                dtreq = bc.bcDB.vsDB.selectRequestXraybyVN1(txtHn.Text, vsdate, xraycode);
            }
            else
            {
                vsdate = grfXray[grfXray.Row, colXrayDate] != null ? grfXray[grfXray.Row, colXrayDate].ToString() : "";
                //preno = txtVN.Text.Trim();
                xraycode = grfXray[grfXray.Row, colXrayCode] != null ? grfXray[grfXray.Row, colXrayCode].ToString() : "";
                vsdate = bc.datetoDB(vsdate);
                dt = bc.bcDB.vsDB.selectResultXraybyVN1(txtHn.Text, this.preno, vsdate, xraycode);
                if (dt.Rows.Count <= 0)
                {
                    //MessageBox.Show("bc.bcDB.vsDB.selectResultXraybyVN1 ", "");
                    dtpacsplus = bc.bcDB.xrDB.selectResultXrayPACsPlusbyVN1(txtHn.Text, this.preno, vsdate, xraycode);
                    flagPACsPlus = (dtpacsplus.Rows.Count > 0) ? true : false;
                }
                else
                {
                    //MessageBox.Show("else bc.bcDB.vsDB.selectResultXraybyVN1 ", "");
                    resdate = dt.Rows[0]["mnc_stamp_dat"].ToString();
                    dtrxrname = dt.Rows[0]["dtr_name"].ToString();
                }
                dtreq = bc.bcDB.vsDB.selectRequestXraybyVN1(txtHn.Text, this.preno, vsdate, xraycode);
                //new LogWriter("d", "GrfXray_Click 00 "+ vsdate);
            }
            
            if (dtreq.Rows.Count > 0)
            {
                reqdate = dtreq.Rows[0]["MNC_REQ_DAT"].ToString();
                reqno = dtreq.Rows[0]["MNC_REQ_NO"].ToString() + "/" + dtreq.Rows[0]["MNC_REQ_YR"].ToString();
                dtrname = dtreq.Rows[0]["dtr_name"].ToString() + "[" + dtreq.Rows[0]["mnc_dot_cd"].ToString() + "]";
                ordname = dtreq.Rows[0]["MNC_XR_DSC"].ToString();
                pttcompname = dtreq.Rows[0]["MNC_COM_DSC"].ToString();
                paidname = dtreq.Rows[0]["mnc_fn_typ_dsc"].ToString();
                depname = dtreq.Rows[0]["MNC_REQ_DEP"].ToString();
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    txt1 += dt.Rows[0]["MNC_XR_DSC"].ToString();
                }
                //reqdate = dt.Rows[0]["MNC_REQ_DAT"].ToString();
                //reqno = dt.Rows[0]["MNC_REQ_NO"].ToString();
                //dtrname = dt.Rows[0]["dtr_name"].ToString();
            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                if (flagPACsPlus)
                {
                    String txt = "", studydesc = "", studydescold = "";
                    foreach (DataRow row in dtpacsplus.Rows)
                    {
                        resdate = row["readingdate"].ToString();
                        if (resdate.Length >= 8)
                        {
                            resdate = resdate.Substring(0, 4) + "-" + resdate.Substring(4, 2) + "-" + resdate.Substring(resdate.Length - 2);
                        }
                        dtrxrname = row["readingdr"].ToString();
                        studydesc = row["studydesc"].ToString();
                        //studydescold = !studydesc.Equals(studydescold) ? row["studydesc"].ToString() : studydescold;
                        if (!studydescold.Equals(studydesc))
                        {
                            txt += Environment.NewLine + Environment.NewLine + studydesc + Environment.NewLine + row["interpretation"].ToString();
                            studydescold = studydesc;
                        }
                        else
                        {
                            txt += row["interpretation"].ToString();
                        }
                    }
                    txt1 = txt;
                }
            }
            //MessageBox.Show("20 ", "");
            dt.Columns.Add("ptt_name", typeof(String));
            dt.Columns.Add("ptt_hn", typeof(String));
            dt.Columns.Add("ptt_age", typeof(String));
            dt.Columns.Add("xry_requestno", typeof(String));
            dt.Columns.Add("ptt_vn", typeof(String));
            dt.Columns.Add("xry_doctor", typeof(String));
            dt.Columns.Add("xry_request_date", typeof(String));
            dt.Columns.Add("xry_print", typeof(String));
            dt.Columns.Add("xry_orderdetail", typeof(String));
            dt.Columns.Add("xry_result", typeof(String));
            dt.Columns.Add("xry_conslution", typeof(String));
            dt.Columns.Add("xry_xraydoctor", typeof(String));
            dt.Columns.Add("ptt_comp", typeof(String));
            dt.Columns.Add("ptt_department", typeof(String));
            dt.Columns.Add("ptt_type", typeof(String));
            dt.Columns.Add("xry_ordername", typeof(String));
            dt.Columns.Add("xry_ordertetail", typeof(String));
            dt.Columns.Add("xry_result_date", typeof(String));
            foreach (DataRow drow in dt.Rows)
            {
                Boolean chkname = false;
                chkname = txtName.Text.Any(c => !Char.IsLetterOrDigit(c));
                if (chkname)
                {
                    drow["ptt_age"] = ptt.AgeString().Replace("Year", "ปี").Replace("Month", "เดือน").Replace("Days", "วัน").Replace("s", "");
                }
                else
                {
                    drow["ptt_age"] = ptt.AgeString();
                }
                drow["ptt_name"] = txtName.Text.Trim();
                drow["ptt_hn"] = txtHn.Text;

                drow["ptt_hn"] = txtHn.Text;
                //drow["xray_requestno"] = txt1;
                if (chkIPD.Checked)
                {
                    drow["ptt_vn"] = txtVN.Text;
                    if (flagPACsPlus)
                    {
                        drow["xry_result"] = txt1;
                        drow["xry_request_date"] = bc.datetoShow(reqdate);
                        drow["xry_requestno"] = reqno;
                        drow["xry_doctor"] = dtrname;
                        drow["xry_ordername"] = ordname;
                        drow["xry_xraydoctor"] = dtrxrname;
                        //drow["xry_xraydoctor"] = dtrxrname;
                        drow["xry_result_date"] = bc.datetoShow(resdate);
                    }
                    else
                    {
                        drow["xry_result"] = drow["mnc_xr_dsc"].ToString();//MNC_REQ_DAT
                        drow["xry_request_date"] = drow["MNC_REQ_DAT"].ToString();
                        drow["xry_requestno"] = drow["MNC_REQ_NO"].ToString();
                        drow["xry_doctor"] = drow["dtr_name"].ToString();
                        drow["xry_xraydoctor"] = drow["dtr_name_result"].ToString();
                        drow["xry_result_date"] = drow["mnc_stamp_dat"].ToString();
                    }
                }
                else
                {
                    drow["ptt_vn"] = txtVN.Text;
                    drow["xry_result"] = txt1;
                    drow["xry_request_date"] = bc.datetoShow(reqdate);
                    drow["xry_requestno"] = reqno;
                    drow["xry_doctor"] = dtrname;
                    drow["xry_ordername"] = ordname;
                    drow["xry_xraydoctor"] = dtrxrname;
                    //drow["xry_xraydoctor"] = dtrxrname;
                    drow["xry_result_date"] = bc.datetoShow(resdate);
                    //drow["ptt_comp"] = pttcompname;
                    //drow["ptt_type"] = paidname;
                }

                drow["ptt_comp"] = pttcompname;
                drow["ptt_type"] = paidname;
                drow["ptt_department"] = depname.Equals("101") ? "OPD1" : depname.Equals("107") ? "OPD2" : depname.Equals("103") ? "OPD3" :
                    depname.Equals("104") ? "ER" : depname.Equals("106") ? "WARD6" : depname.Equals("108") ? "WARD5W" : depname.Equals("109") ? "ล้างไต" :
                    depname.Equals("105") ? "WARD5M" : depname.Equals("113") ? "ICU" : depname.Equals("114") ? "NS/LR" : depname.Equals("115") ? "ทันตกรรม" : depname.Equals("116") ? "CCU" : depname;
            }
            return dt;
        }
        private void ContextMenu_xray_infinitt(object sender, System.EventArgs e)
        {
            if (grfOrdXray == null) return;
            if (grfOrdXray.Row <= 1) return;
            if (grfOrdXray.Col <= 0) return;
            String address = "";

            address = "http://172.25.10.9/pkg_pacs/external_interface.aspx?TYPE=W&LID=itadmin&LPW=itadmin&PID=" + txtHn.Text.Trim();
            System.Diagnostics.Process.Start("iexplore", address);
        }
        private void GrfXray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfXray == null) return;
            if (grfXray.Row <= 0) return;
            setXrayResult();
        }
        private void setXrayResult()
        {
            String an = "", vn = "", vsdate = "", xraycode = "";
            Boolean flagPACsPlus = false;
            DataTable dt = new DataTable();
            DataTable dtpacsplus = new DataTable();
            if (chkIPD.Checked)
            {
                //an = grfXray[grfXray.Row, colIPDAnShow] != null ? grfXray[grfXray.Row, colIPDAnShow].ToString() : "";
                vsdate = grfXray[grfXray.Row, colXrayDate] != null ? grfXray[grfXray.Row, colXrayDate].ToString() : "";
                xraycode = grfXray[grfXray.Row, colXrayCode] != null ? grfXray[grfXray.Row, colXrayCode].ToString() : "";
                vsdate = bc.datetoDB(vsdate);
                String[] an1 = txtVN.Text.Trim().Split('/');
                if (an1.Length > 0)
                {
                    dt = bc.bcDB.vsDB.selectResultXraybyAN1(txtHn.Text, an1[0], an1[1], xraycode);
                    if (dt.Rows.Count <= 0)
                    {
                        dtpacsplus = bc.bcDB.xrDB.selectResultXrayPACsPlusbyVN1(txtHn.Text, this.preno, vsdate, xraycode);
                        flagPACsPlus = (dtpacsplus.Rows.Count > 0) ? true : false;
                    }
                }
                //dt = bc.bcDB.vsDB.selectResultXraybyVN1(txtHn.Text, preno, vsdate, xraycode);
            }
            else
            {
                vsdate = grfXray[grfXray.Row, colXrayDate] != null ? grfXray[grfXray.Row, colXrayDate].ToString() : "";
                //preno = txtVN.Text.Trim();
                xraycode = grfXray[grfXray.Row, colXrayCode] != null ? grfXray[grfXray.Row, colXrayCode].ToString() : "";
                vsdate = bc.datetoDB(vsdate);
                dt = bc.bcDB.vsDB.selectResultXraybyVN1(txtHn.Text, this.preno, vsdate, xraycode);
                if (dt.Rows.Count <= 0)
                {
                    //MessageBox.Show("11 ", "");
                    dtpacsplus = bc.bcDB.xrDB.selectResultXrayPACsPlusbyVN1(txtHn.Text, this.preno, vsdate, xraycode);
                    flagPACsPlus = (dtpacsplus.Rows.Count > 0) ? true : false;
                }
                //new LogWriter("d", "GrfXray_Click 00 "+ vsdate);
            }
            //new LogWriter("d", "GrfXray_Click 01");
            if (dt.Rows.Count > 0)
            {
                //MessageBox.Show("22 ", "");
                //new LogWriter("d", "GrfXray_Click 02");
                //rtb.Text = dt.Rows[0]["MNC_XR_DSC"].ToString();
                String txt = "";
                foreach (DataRow row in dt.Rows)
                {
                    txt += Environment.NewLine + row["MNC_XR_DSC"].ToString();
                }
                rtb.Text = txt;
                //Application.DoEvents();
                //new LogWriter("d", "GrfXray_Click 02 "+ dt.Rows[0]["MNC_XR_DSC"].ToString());
            }
            else if (flagPACsPlus)
            {
                //MessageBox.Show("33 ", "");
                //new LogWriter("d", "GrfXray_Click 03");
                rtb.Text = "";
                String txt = "", studydesc="", studydescold = "";
                foreach (DataRow row in dtpacsplus.Rows)
                {
                    studydesc = row["studydesc"].ToString();
                    //studydescold = !studydesc.Equals(studydescold) ? row["studydesc"].ToString() : studydescold;
                    if (!studydescold.Equals(studydesc))
                    {
                        txt += Environment.NewLine + Environment.NewLine + studydesc + Environment.NewLine + row["interpretation"].ToString();
                        studydescold = studydesc;
                    }
                    else
                    {
                        txt += row["interpretation"].ToString();
                    }
                }
                rtb.Text = txt;
            }
            //new LogWriter("d", "GrfXray_Click 04");
        }
        private void TcDtr_TabClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
            setActive();
        }
        private void setActive()
        {
            //String vsDate = "";
            int sizeradio = bc.scVssizeradio;
            //preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
            //vsDate = grfOPD[grfOPD.Row, colVsVsDate] != null ? grfOPD[grfOPD.Row, colVsVsDate].ToString() : "";
            //vsDate = bc.datetoDB(vsDate);
            if (tcDtr.SelectedTab == tabScan)
            {
                scVs.SizeRatio = sizeradio;
                if (!flagtabScan)
                {
                    initGrfScan();
                }
                flagtabScan = true;
                setGrfScan();
            }
            else if (tcDtr.SelectedTab == tabOrder)
            {
                scVs.SizeRatio = sizeradio;
                if (!flagTabOrderLoad)
                {
                    initGrfOrderLabXray();
                }
                flagTabOrderLoad = true;
                tabOrderActive();
            }
            else if (tcDtr.SelectedTab == tabStfNote)
            {
                scVs.SizeRatio = sizeradio;
                if (!chkIPD.Checked)
                {
                    preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                    vsDate = grfOPD[grfOPD.Row, colVsVsDate] != null ? grfOPD[grfOPD.Row, colVsVsDate].ToString() : "";
                    if (vsDate.Length >= 8)
                    {
                        String yy = "";
                        yy = vsDate.Substring(vsDate.Length - 2);
                        int chkyy = 0;
                        int.TryParse(yy, out chkyy);
                        chkyy = (chkyy > 60) ? chkyy += 2500 : chkyy += 2000;
                        //new LogWriter("d", "FrmScanView setActive chkyy " + chkyy);
                        vsDate = vsDate.Substring(0,vsDate.Length - 2) + chkyy;
                        //new LogWriter("d", "FrmScanView setActive vsDate " + vsDate);
                        vsDate = bc.datetoDB(vsDate);
                    }
                    else
                    {
                        vsDate = bc.datetoDB(vsDate);
                    }
                }
                else
                {
                    preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
                    vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                    vsDate = bc.datetoDB(vsDate);
                }
                //new LogWriter("d", "FrmScanView setActive colVsVsDate " + grfOPD[grfOPD.Row, colVsVsDate].ToString()+" "+);
                setStaffNote(vsDate, preno);
            }
            else if (tcDtr.SelectedTab == tabLab)
            {
                scVs.SizeRatio = sizeradio;
                if(!flagTabOrderLoad)
                {
                    initGrfOrderLabXray();
                }
                flagTabOrderLoad = true;
                
                setGrfLab();
                
            }
            else if (tcDtr.SelectedTab == tabXray)
            {
                scVs.SizeRatio = sizeradio;
                if (!flagTabOrderLoad)
                {
                    initGrfOrderLabXray();
                }
                flagTabOrderLoad = true;
                
                setGrfXray(grfOPD.Row);
                
            }
            //else if (tcDtr.SelectedTab == tabScan)
            //{
            //    scVs.SizeRatio = sizeradio;
            //    if (!flagtabScan)
            //    {
            //        initGrf();
            //    }
            //    flagtabScan = true;
            //}
            else if (tcDtr.SelectedTab == tabOrdAdd)
            {
                scVs.SizeRatio = 1;
                if (!flagTabDtrOrdLoad)
                {
                    tabOrdAdd.Hide();
                    showLbLoading();
                    //Application.DoEvents();
                    initTabOrdAdd();
                    initGrfOrdDrug();
                    initGrfOrdSup();
                    initGrfOrdLab();
                    initGrfOrdXray();
                    initGrfOrdOR();
                    initGrfOrdItem();

                    setGrfOrdDrug();
                    setGrfOrdSup();
                    setGrfOrdLab();
                    setGrfOrdXray();
                    setGrfOrdItem();
                    tabOrdAdd.Show();
                }
                flagTabDtrOrdLoad = true;
                hideLbLoading();
            }
            else if(tcDtr.SelectedTab == tabHnLabOut)
            {
                if (!flagTabOutlabLoad)
                {
                    FrmWaiting frmW = new FrmWaiting();
                    frmW.StartPosition = FormStartPosition.CenterScreen;
                    frmW.Show(this);

                    setTabHnLabOut();

                    frmW.Dispose();
                }
                flagTabOutlabLoad = true;
            }
            //else if (tcDtr.SelectedTab == tabPic)
            //{
            //    setTabGrfPicPatient();
            //}
            else if (tcDtr.SelectedTab == tabMac)
            {
                setTabMachineResult();
            }
            else if (tcDtr.SelectedTab == tabPrn)
            {
                if (!flagTabPrn)
                {
                    initTabPrn();
                }
                
                if (!chkIPD.Checked )
                {
                    if (grfOPD.Row > 1)
                    {
                        preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                        vsDate = grfOPD[grfOPD.Row, colVsVsDate] != null ? grfOPD[grfOPD.Row, colVsVsDate].ToString() : "";
                        vsDate = bc.datetoDB(vsDate);
                    }
                }
                else
                {
                    if (grfOPD.Row > 1)
                    {
                        preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
                        vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                        vsDate = bc.datetoDB(vsDate);
                    }
                }
                ChkPrnEmail_CheckedChanged(null, null);
                flagTabPrn = true;
            }
            else if (tcDtr.SelectedTab == tabPic)
            {
                if (!flagTabPic)
                {
                    initGrfPicture();
                }
                flagTabPic = true;
                setTabGrfPicPatient();
            }
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
        private DataTable getDataTableOrder(String flag)
        {
            String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "", anyr = "", vn1 = "";
            DataTable dtOrder = new DataTable();
            if (chkIPD.Checked)
            {
                statusOPD = grfIPD[grfIPD.Row, colIPDStatus] != null ? grfIPD[grfIPD.Row, colIPDStatus].ToString() : "";
                preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
                vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";

                //chkIPD.Checked = true;
                an = grfIPD[grfIPD.Row, colIPDAn] != null ? grfIPD[grfIPD.Row, colIPDAn].ToString() : "";
                anDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                anyr = grfIPD[grfIPD.Row, colIPDAnYr] != null ? grfIPD[grfIPD.Row, colIPDAnYr].ToString() : "";
                //txtVN.Value = an;
                label2.Text = "AN :";
                if (flag.Equals("print"))
                {
                    dtOrder = bc.bcDB.vsDB.selectDrugIPDPrint(txtHn.Text, an, anyr);
                }
                else
                {
                    dtOrder = bc.bcDB.vsDB.selectDrugIPD(txtHn.Text, an, anyr);
                }
            }
            else
            {
                statusOPD = grfOPD[grfOPD.Row, colVsStatus] != null ? grfOPD[grfOPD.Row, colVsStatus].ToString() : "";
                preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                vsDate = grfOPD[grfOPD.Row, colVsVsDate] != null ? grfOPD[grfOPD.Row, colVsVsDate].ToString() : "";
                //vsDate = bc.datetoDB(vsDate);

                //chkIPD.Checked = false;
                vn = grfOPD[grfOPD.Row, colVsVn] != null ? grfOPD[grfOPD.Row, colVsVn].ToString() : "";
                txtVN.Value = vn;
                label2.Text = "VN :";
                if (vn.IndexOf("(") > 0)
                {
                    vn1 = vn.Substring(0, vn.IndexOf("("));
                }
                if (vn.IndexOf("/") > 0)
                {
                    vn1 = vn.Substring(0, vn.IndexOf("/"));
                }
                vsDate = bc.datetoDB(vsDate);
                if (flag.Equals("print"))
                {
                    dtOrder = bc.bcDB.vsDB.selectDrugOPDPrint(txtHn.Text, vn1, preno, vsDate);
                }
                else
                {
                    dtOrder = bc.bcDB.vsDB.selectDrugOPD(txtHn.Text, vn1, preno, vsDate);
                }
            }
            return dtOrder;
        }
        private void tabOrderActive()
        {
            if (grfIPD.Row == null) return;
            //FrmWaiting frmW = new FrmWaiting();
            //frmW.StartPosition = FormStartPosition.CenterScreen;
            //frmW.Show(this);
            showLbLoading();
            try
            {
                DataTable dtOrder = new DataTable();
                dtOrder = getDataTableOrder("");
                setGrfOrder(dtOrder);
            }
            catch(Exception ex)
            {

            }
            hideLbLoading();
            //frmW.Dispose();
        }        

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // ...
            if (keyData == (Keys.Escape))
            {
                //appExit();
                //if (MessageBox.Show("ต้องการออกจากโปรแกรม1", "ออกจากโปรแกรม", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                //{
                //frmmain.Show();
                Close();
                //    return true;
                //}
            }
            //else
            //{
            //    switch (keyData)
            //    {
            //        case Keys.K | Keys.Control:
            //            if (flagShowTitle)
            //                flagShowTitle = false;
            //            else
            //                flagShowTitle = true;
            //            setTitle(flagShowTitle);
            //            return true;
            //        case Keys.X | Keys.Control:
            //            //frmmain.Show();
            //            Close();
            //            return true;
            //    }
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void setPicStaffNote()
        {

            //pnL = new Panel();
            //pnL.Dock = DockStyle.Left;
            //pnL.Width = tabScan.Width / 2;
            //tabScan.Controls.Add(pnL);
            //pnR = new Panel();
            //pnR.Dock = DockStyle.Fill;
            //pnR.Width = tabScan.Width / 2;
            //tabScan.Controls.Add(pnR);
            //sct = new SplitContainer();
            sct = new C1SplitContainer();
            sct.Dock = DockStyle.Fill;
            //sct.Dock = System.Windows.Forms.DockStyle.Fill;
            sct.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            theme1.SetTheme(sct, bc.iniC.themeApplication);
            tabStfNote.Controls.Add(sct);

            //fpL = new FlowLayoutPanel();
            //fpL.Dock = DockStyle.Fill;
            //fpL.AutoScroll = true;
            //sct.Panels.Controls.Add(fpL);
            //tabScan.Controls.Add(fpL);
            //fpR = new FlowLayoutPanel();
            //fpR.Dock = DockStyle.Fill;
            //fpR.AutoScroll = true;
            //tabScan.Controls.Add(fpR);
            //sct.Panel2.Controls.Add(fpR);
            cspL = new C1SplitterPanel();
            cspL.Collapsible = true;
            //cspL.Controls.Add(this.panel1);
            cspL.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            cspL.Height = 629;
            cspL.Location = new System.Drawing.Point(0, 21);
            cspL.Name = "cspL";
            cspL.Size = new System.Drawing.Size(600, 608);
            cspL.SizeRatio = 49.855D;
            cspL.TabIndex = 0;
            cspL.Text = "Panel 1";
            cspL.Width = 600;

            cspR = new C1SplitterPanel();
            //cspR.Controls.Add(this.panel2);
            cspR.Height = 629;
            cspR.Location = new System.Drawing.Point(494, 21);
            cspR.Name = "cspR";
            cspR.Size = new System.Drawing.Size(600, 608);
            cspR.TabIndex = 1;
            cspR.Text = "Panel 2";

            sct.Panels.Add(cspL);
            sct.Panels.Add(cspR);

            picL = new C1PictureBox();
            picL = new C1PictureBox();
            picL.Dock = DockStyle.Fill;
            picL.SizeMode = PictureBoxSizeMode.StretchImage;
            //picL.SizeMode = PictureBoxSizeMode.StretchImage;
            
            picR = new C1PictureBox();
            picR = new C1PictureBox();
            picR.Dock = DockStyle.Fill;
            picR.SizeMode = PictureBoxSizeMode.StretchImage;
            //picR.SizeMode = PictureBoxSizeMode.StretchImage;
            cspL.Controls.Add(picL);
            cspR.Controls.Add(picR);
            //fpR.Controls.Add(picR);
        }
        private void Grf_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //MessageBox.Show("Row " + ((C1FlexGrid)sender).Row+"\n grf name "+ ((C1FlexGrid)sender).Name, "Col "+((C1FlexGrid)sender).Col+" id "+ ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row,colPic2].ToString());
            if (((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colPic2] == null) return;
            if (((C1FlexGrid)sender).Row < 0) return;
            String id = "";
            ((C1FlexGrid)sender).AutoSizeCols();
            ((C1FlexGrid)sender).AutoSizeRows();
            if (((C1FlexGrid)sender).Col == 1)
            {
                id = grfScan[grfScan.Row, colPic2].ToString();
            }
            else
            {
                id = grfScan[grfScan.Row, colPic4].ToString();
            }
            //id = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colPic2].ToString();
            dsc_id = id;
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
                img = Image.FromStream(strm);
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
                originalWidth = img.Width;
                originalHeight = img.Height;
                //resizedImage = img.GetThumbnailImage(newWidth, (newWidth * img.Height) / originalWidth, null, IntPtr.Zero);
                resizedImage = img.GetThumbnailImage((newHeight * img.Width) / originalHeight, newHeight, null, IntPtr.Zero);
                pic.Image = resizedImage;
                frmImg.Controls.Add(pn);
                pn.Controls.Add(pic);
                //pn.Controls.Add(vScroller);
                ContextMenu menuGw = new ContextMenu();
                menuGw.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_print));
                menuGw.MenuItems.Add("ต้องการ ลบข้อมูลนี้", new EventHandler(ContextMenu_Delete));
                mouseWheel = 0;
                pic.MouseWheel += Pic_MouseWheel;
                pic.ContextMenu = menuGw;
                //vScroller.Scroll += VScroller_Scroll;
                //pic.Paint += Pic_Paint;
                //vScroller.Hide();
                frmImg.ShowDialog(this);
            }
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
        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                //if (File.Exists(this.ImagePath))
                //{
                //Load the image from the file
                //Stream streamPrint = null;
                System.Drawing.Image img = Image.FromStream(streamPrint);
                //Adjust the size of the image to the page to print the full image without loosing any part of it
                Rectangle m = e.MarginBounds;
                if ((double)img.Width / (double)img.Height > (double)m.Width / (double)m.Height) // image is wider
                {
                    m.Height = (int)((double)img.Height / (double)img.Width * (double)m.Width);
                }
                else
                {
                    m.Width = (int)((double)img.Width / (double)img.Height * (double)m.Height);
                }
                //pd.DefaultPageSettings.Landscape = m.Width > m.Height;
                //Putting image in center of page.

                e.Graphics.DrawString("print date " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), fEdit, Brushes.Black, 30, 30);
                e.Graphics.DrawString("doc scan id " + dsc_id, fEdit, Brushes.Black, 30, 50);
                e.Graphics.DrawImage(img, m);
                //}
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmScanView1 Pd_PrintPage error " + ex.Message);
            }
        }
        private void ContextMenu_Delete(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("ต้องการ ลบข้อมูลนี้ ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                int chk = 0;
                String re = bc.bcDB.dscDB.voidDocScan(dsc_id, "");
                if (int.TryParse(re, out chk))
                {
                    frmImg.Dispose();
                    setGrfVsIPD();
                    grfScan.Rows.Count = 0;
                    //clearGrf();
                }
            }
        }
        //private void Pic_Paint(object sender, PaintEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    //pBox = sender as PictureBox;
        //    e.Graphics.DrawImage(pic.Image, e.ClipRectangle, pic.Image.Height, y, e.ClipRectangle.Width,
        //      e.ClipRectangle.Height, GraphicsUnit.Pixel);
        //}

        //private void VScroller_Scroll(object sender, ScrollEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    //Graphics g = pic.CreateGraphics();
        //    //g.DrawImage(pic.Image, newRectangle(0, 0, pic.Height, vScroller.Value));
        //    y = (sender as VScrollBar).Value;
        //    pic.Refresh();
        //}

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
            resizedImage = img.GetThumbnailImage((newHeight * img.Width) / originalHeight, newHeight, null, IntPtr.Zero);
            pic.Image = resizedImage;
            //if(resizedImage.Height > frmImg.Height)
            //{
            //    vScroller.Show();
            //}
            //else
            //{
            //    vScroller.Hide();
            //    //Graphics g = pictureBox1.CreateGraphics();
            //    //g.DrawImage(pictureBox1.Image, newRectangle(0, 0, pictureBox1.Height, vScroller.Value));
            //}
        }

        private void TxtHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setControlHN();
            }
        }
        private void setControlHN()
        {
            ptt = bc.bcDB.pttDB.selectPatinet(txtHn.Text.Trim());
            txtName.Value = ptt.Name;
            tcDtr.SelectedTab = tabStfNote;
            //lbAge.Text = "อายุ " + ptt.AgeStringShort();
            String allergy = "";
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectDrugAllergy(txtHn.Text.Trim());
            dtchronic = bc.bcDB.vsDB.SelectChronicByPID(ptt.idcard);
            foreach (DataRow row in dt.Rows)
            {
                allergy += row["MNC_ph_tn"].ToString() + " " + row["MNC_ph_memo"].ToString() + ", ";
            }
            lbDrugAllergy.Text = "";
            if (allergy.Length > 0)
            {
                lbDrugAllergy.Text = "แพ้ยา " + allergy;
            }
            else
            {
                lbDrugAllergy.Text = "ไม่มีข้อมูล การแพ้ยา ";
            }
            
            foreach (DataRow row in dtchronic.Rows)
            {
                txtChronic += row["CHRONICCODE"].ToString() + " " + row["MNC_CRO_DESC"].ToString() +",";
            }
            lbAge.Text = "อายุ " + ptt.AgeStringShort();

            picL.Image = null;
            picR.Image = null;
            setGrfVsIPD();
            setGrfVsOPD();
            //setTabHnLabOut();
            //setTabGrfPicPatient();
            grfOPD.Focus();
            if (grfOPD.Rows.Count > 1)
            {
                grfOPD.Row = 1;
                grfOPD.Col = 1;
                //setGrfScan(grfOPD.Row, "OPD");
                String symptom = "", paidtype="", high="", weight="", cc="", ccin="", ccex="", abc="", hc="", bp1l="", bp1r="", bp2l="",bp2r="", temp="", vitalsign="", pres="", breath="", radios="";
                preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                vsDate = grfOPD[grfOPD.Row, colVsVsDate] != null ? grfOPD[grfOPD.Row, colVsVsDate].ToString() : "";
                symptom = grfOPD[grfOPD.Row, colVsDept] != null ? grfOPD[grfOPD.Row, colVsDept].ToString() : "";
                paidtype = grfOPD[grfOPD.Row, colVsPaidType] != null ? grfOPD[grfOPD.Row, colVsPaidType].ToString() : "";

                high = grfOPD[grfOPD.Row, colVsHigh] != null ? grfOPD[grfOPD.Row, colVsHigh].ToString() : "";
                weight = grfOPD[grfOPD.Row, colVsWeight] != null ? grfOPD[grfOPD.Row, colVsWeight].ToString() : "";
                cc = grfOPD[grfOPD.Row, colVscc] != null ? grfOPD[grfOPD.Row, colVscc].ToString() : "";
                ccin = grfOPD[grfOPD.Row, colVsccin] != null ? grfOPD[grfOPD.Row, colVsccin].ToString() : "";
                ccex = grfOPD[grfOPD.Row, colVsccex] != null ? grfOPD[grfOPD.Row, colVsccex].ToString() : "";
                abc = grfOPD[grfOPD.Row, colVsabc] != null ? grfOPD[grfOPD.Row, colVsabc].ToString() : "";
                hc = grfOPD[grfOPD.Row, colVshc16] != null ? grfOPD[grfOPD.Row, colVshc16].ToString() : "";
                bp1l = grfOPD[grfOPD.Row, colVsbp1l] != null ? grfOPD[grfOPD.Row, colVsbp1l].ToString() : "";
                bp1r = grfOPD[grfOPD.Row, colVsbp1r] != null ? grfOPD[grfOPD.Row, colVsbp1r].ToString() : "";
                bp2l = grfOPD[grfOPD.Row, colVsbp2l] != null ? grfOPD[grfOPD.Row, colVsbp2l].ToString() : "";
                bp2r = grfOPD[grfOPD.Row, colVsbp2r] != null ? grfOPD[grfOPD.Row, colVsbp2r].ToString() : "";
                temp = grfOPD[grfOPD.Row, colVsTemp] != null ? grfOPD[grfOPD.Row, colVsTemp].ToString() : "";
                vitalsign = grfOPD[grfOPD.Row, colVsVital] != null ? grfOPD[grfOPD.Row, colVsVital].ToString() : "";
                pres = grfOPD[grfOPD.Row, colVsPres] != null ? grfOPD[grfOPD.Row, colVsPres].ToString() : "";
                radios = grfOPD[grfOPD.Row, colVsRadios] != null ? grfOPD[grfOPD.Row, colVsRadios].ToString() : "";
                breath = grfOPD[grfOPD.Row, colVsBreath] != null ? grfOPD[grfOPD.Row, colVsBreath].ToString() : "";
                vsDate = bc.datetoDB(vsDate);
                bc.preno = preno;
                bc.vsdate = vsDate;
                bc.hn = txtHn.Text.Trim();
                txtVN.Value = grfOPD[grfOPD.Row, colVsVn] != null ? grfOPD[grfOPD.Row, colVsVn].ToString() : "";
                setStaffNote(vsDate, preno);
                //setControlPnOrdDiagVal(vitalsign, pres, temp, weight, high, "", cc, ccin, ccex, abc, hc, bp1l, bp2l, radios, breath, symptom, vsDate, paidtype);
            }
            setControlGbPtt();
        }
        private void setTabGrfPicPatient()
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = this.bc.bcDB.dscDB.selectPicByHn(txtHn.Text.Trim());
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_grfPic_print));
            contextMenu.MenuItems.Add("ต้องการ Download ภาพนี้", new EventHandler(ContextMenu_grfPic_download));
            grfPic.ContextMenu = contextMenu;
            grfPic.Rows.Count = 0;
            if (dataTable2.Rows.Count > 0)
            {
                try
                {
                    grfPic.Rows.Count = (dataTable2.Rows.Count / 2) + 1;
                    FtpClient ftpClient = new FtpClient(this.bc.iniC.hostFTP, this.bc.iniC.userFTP, this.bc.iniC.passFTP);
                    bool flag = false;
                    int num1 = 0;
                    int num2 = -1;
                    foreach (DataRow row1 in (InternalDataCollectionBase)dataTable2.Rows)
                    {
                        if (!flag)
                        {
                            ++num1;
                            string str1 = "";
                            string str2 = "";
                            string str3 = row1[this.bc.bcDB.dscDB.dsc.doc_scan_id].ToString();
                            str1 = row1[this.bc.bcDB.dscDB.dsc.doc_group_sub_id].ToString();
                            string str4 = row1[this.bc.bcDB.dscDB.dsc.image_path].ToString();
                            string str5 = row1[this.bc.bcDB.dscDB.dsc.host_ftp].ToString();
                            string str6 = row1[this.bc.bcDB.dscDB.dsc.folder_ftp].ToString();
                            try
                            {
                                int count1 = 2048;
                                str2 = "00";
                                Row row2;
                                if (num1 % 2 == 0)
                                {
                                    row2 = grfPic.Rows[num2];
                                }
                                else
                                {
                                    ++num2;
                                    row2 = grfPic.Rows[num2];
                                }
                                MemoryStream memoryStream = new MemoryStream();
                                str2 = "01";
                                Application.DoEvents();
                                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(str5 + "/" + str6 + "/" + str4);
                                ftpWebRequest.Credentials = (ICredentials)new NetworkCredential(this.bc.iniC.userFTP, this.bc.iniC.passFTP);
                                ftpWebRequest.UseBinary = true;
                                ftpWebRequest.UsePassive = this.bc.ftpUsePassive;
                                ftpWebRequest.KeepAlive = true;
                                ftpWebRequest.Method = "RETR";
                                Stream responseStream = ftpWebRequest.GetResponse().GetResponseStream();
                                str2 = "02";
                                byte[] buffer = new byte[count1];
                                int count2 = responseStream.Read(buffer, 0, count1);
                                try
                                {
                                    for (; count2 > 0; count2 = responseStream.Read(buffer, 0, count1))
                                        memoryStream.Write(buffer, 0, count2);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                    LogWriter logWriter = new LogWriter("e", "FrmScanView1 SetGrfScan try int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize); ex " + ex.Message + " " + str2);
                                }
                                str2 = "03";
                                Image image = (Image)new Bitmap((Stream)memoryStream);
                                str2 = "04";
                                int width = image.Width;
                                int imgScanWidth = this.bc.imgScanWidth;
                                Image thumbnailImage = image.GetThumbnailImage(imgScanWidth, imgScanWidth * image.Height / width, (Image.GetThumbnailImageAbort)null, IntPtr.Zero);
                                str2 = "05";
                                if (num1 % 2 == 0)
                                {
                                    row2[colPic3]= thumbnailImage;
                                    str2 = "061";
                                    row2[colPic4] = str3;
                                    str2 = "071";
                                }
                                else
                                {
                                    str2 = "051";
                                    row2[colPic1] = thumbnailImage;
                                    str2 = "06";
                                    row2[colPic2] = str3;
                                    str2 = "07";
                                }
                                this.strm = new FrmScanView1.listStream();
                                this.strm.id = str3;
                                str2 = "08";
                                this.strm.stream = memoryStream;
                                str2 = "09";
                                lStreamPic.Add(strm);
                                Application.DoEvents();
                                str2 = "12";
                                if (num1 == 50)
                                    GC.Collect();
                                if (num1 == 100)
                                    GC.Collect();
                            }
                            catch (Exception ex)
                            {
                                string str7 = ex.Message + " " + str2;
                                LogWriter logWriter = new LogWriter("e", "FrmScanView1 setGrfPic ex " + ex.Message + " " + str2 + " colcnt " + num1.ToString() + " HN " + ((Control)this.txtHn).Text + " ");
                            }
                        }
                        else
                            break;
                    }
                }
                catch (Exception ex)
                {
                    LogWriter logWriter = new LogWriter("e", "FrmScanView1 setGrfPic if (dt.Rows.Count > 0) ex " + ex.Message);
                }
            }
            grfPic.AutoSizeRows();
        }
        private void ContextMenu_grfPic_download(object sender, EventArgs e)
        {
            if (grfPic.Col <= 0 || (grfPic.Row < 0))
                return;
            string str1 = grfPic.Col != 1 ? grfPic[grfPic.Row, colPic4].ToString() : grfPic[grfPic.Row, colPic2].ToString();
            this.dsc_id = str1;
            Stream stream = (Stream)null;
            MemoryStream memoryStream = (MemoryStream)null;
            foreach (FrmScanView1.listStream listStream in lStreamPic)
            {
                if (listStream.id.Equals(str1))
                {
                    memoryStream = listStream.stream;
                    stream = (Stream)listStream.stream;
                    break;
                }
            }
            try
            {
                if (!Directory.Exists(this.bc.iniC.pathDownloadFile))
                    Directory.CreateDirectory(this.bc.iniC.pathDownloadFile);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("path" + this.bc.iniC.pathDownloadFile + " error " + ex.Message, "");
            }
            string str2 = DateTime.Now.Ticks.ToString();
            Image.FromStream(stream).Save(this.bc.iniC.pathDownloadFile + "\\" + ((Control)this.txtHn).Text.Trim() + "_" + str2 + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            this.bc.ExploreFile(this.bc.iniC.pathDownloadFile + "\\" + ((Control)this.txtHn).Text.Trim() + "_" + str2 + ".jpg");
        }
        private void ContextMenu_grfPic_print(object sender, EventArgs e)
        {
            if ((grfPic.Col <= 0 ) || (grfPic.Row < 0))
                return;
            //grfFMCode[e.NewRange.r1, colID] != null ? grfFMCode[e.NewRange.r1, colID].ToString() : "";
            string str = grfPic.Col != 1 ? grfPic[grfPic.Row, colPic4].ToString() : grfPic[grfPic.Row, colPic2].ToString();
            this.dsc_id = str;
            MemoryStream memoryStream = (MemoryStream)null;
            foreach (FrmScanView1.listStream listStream in lStreamPic)
            {
                if (listStream.id.Equals(str))
                {
                    memoryStream = listStream.stream;
                    this.streamPrint = (Stream)listStream.stream;
                    break;
                }
            }
            FrmScanView1.SetDefaultPrinter(this.bc.iniC.printerA4);
            Thread.Sleep(500);
            PrintPic(this.streamPrint);
        }
        public void PrintPic(Stream streamPic)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintController = (PrintController)new StandardPrintController();
                pd.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                pd.PrinterSettings.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                pd.PrintPage += (PrintPageEventHandler)((sndr, args) =>
                {
                    Image image = Image.FromStream(streamPic);
                    Rectangle marginBounds = args.MarginBounds;
                    if ((double)image.Width / (double)image.Height > (double)marginBounds.Width / (double)marginBounds.Height)
                        marginBounds.Height = (int)((double)image.Height / (double)image.Width * (double)marginBounds.Width);
                    else
                        marginBounds.Width = (int)((double)image.Width / (double)image.Height * (double)marginBounds.Height);
                    pd.DefaultPageSettings.Landscape = marginBounds.Width > marginBounds.Height;
                    marginBounds.Y = (((PrintDocument)sndr).DefaultPageSettings.PaperSize.Height - marginBounds.Height) / 2;
                    marginBounds.X = (((PrintDocument)sndr).DefaultPageSettings.PaperSize.Width - marginBounds.Width) / 2;
                    args.Graphics.DrawImage(image, marginBounds);
                });
                pd.Print();
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }
        private void setTabHnLabOut()
        {
            //ProgressBar pB1 = new ProgressBar();
            //pB1.Location = new System.Drawing.Point(20, 16);
            //pB1.Name = "pB1";
            //pB1.Size = new System.Drawing.Size(862, 23);
            //gbPtt.Controls.Add(pB1);
            //pB1.Left = txtHn.Left;
            //pB1.Show();
            setHeaderDisable();

            if (txtHn.Text.Trim().Length <= 0)
            {
                setHeaderEnable();
                return;
            }
            tcHnLabOut.TabPages.Clear();
            DataTable dtlaboutOld = new DataTable();
            DataTable dtlaboutdsc = new DataTable();
            dtlaboutOld = bc.bcDB.labexDB.selectByHn(txtHn.Text.Trim());
            dtlaboutdsc = bc.bcDB.dscDB.selectLabOutByDateReq("","",txtHn.Text.Trim(), "daterequest");
            if (dtlaboutOld.Rows.Count > 0)
            {
                //pB1.Maximum = dtlaboutOld.Rows.Count;
                //new LogWriter("w", "setTabHnLabOut");
                tcHnLabOut.TabPages.Clear();
                int k = 0;
                foreach(DataRow row in dtlaboutOld.Rows)
                {
                    try
                    {
                        k++;
                        //pB1.Value = k;
                        String vn = "", preno = "", vsdate = "", an = "", labexid = "", yearid = "", filename = "", filename1 = "", datetick = "";
                        datetick = DateTime.Now.Ticks.ToString();
                        labexid = dtlaboutOld.Rows[0][bc.bcDB.labexDB.labex.Id].ToString();
                        filename = labexid + "_" + k.ToString() + ".pdf";
                        yearid = dtlaboutOld.Rows[0][bc.bcDB.labexDB.labex.YearId].ToString();
                        vn = dtlaboutOld.Rows[0][bc.bcDB.labexDB.labex.Vn].ToString();
                        vn = vn.Replace("/", ".").Replace("(", ".").Replace(")", "");
                                                
                        //for (int i = 0; i < 6; i++)
                        //{
                        MemoryStream stream;

                        if (!Directory.Exists("report"))
                        {
                            Directory.CreateDirectory("report");
                        }

                        FtpClient ftpc = new FtpClient(bc.iniC.hostFTPLabOut, bc.iniC.userFTPLabOut, bc.iniC.passFTPLabOut, bc.ftpUsePassiveLabOut);
                        stream = ftpc.download(bc.iniC.folderFTPLabOut + "//" + yearid + "//" + filename);
                        if (stream == null)
                        {
                            setHeaderEnable();
                            continue;
                        }
                        if (stream.Length == 0)
                        {
                            setHeaderEnable();
                            continue;
                        }
                        C1DockingTabPage tabHnLabOut = new C1DockingTabPage();
                        tabHnLabOut.Location = new System.Drawing.Point(1, 24);
                        //tabScan.Name = "c1DockingTabPage1";
                        tabHnLabOut.Size = new System.Drawing.Size(667, 175);
                        tabHnLabOut.TabIndex = 0;
                        tabHnLabOut.Text = bc.datetoShow(row["labex_date"].ToString());
                        tabHnLabOut.Name = "tabHnLabOut_" + datetick;
                        //tabHnLabOut.DoubleClick += TabHnLabOut_DoubleClick;
                        tcHnLabOut.TabPages.Add(tabHnLabOut);

                        stream.Seek(0, SeekOrigin.Begin);
                        
                        //tcHnLabOut.Controls.Add(tablabOut);
                        if (bc.iniC.windows.Equals("windowsxp"))
                        {
                            var fileStream = new FileStream("report\\" + datetick + ".pdf", FileMode.Create, FileAccess.Write);
                            stream.CopyTo(fileStream);
                            fileStream.Flush();
                            fileStream.Dispose();
                            Application.DoEvents();
                            Thread.Sleep(50);

                            string currentDirectory = Directory.GetCurrentDirectory();
                            WebBrowser webBrowser1;
                            webBrowser1 = new System.Windows.Forms.WebBrowser();
                            //webBrowser1.Enabled = true;
                            //webBrowser1.Location = new System.Drawing.Point(192, 0);
                            webBrowser1.Name = "webBrowser1";
                            //axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(Resources.GetObject("axAcroPDF1.OcxState")));
                            //webBrowser1.Size = new System.Drawing.Size(192, 192);
                            webBrowser1.Dock = DockStyle.Fill;
                            //axAcroPDF1.TabIndex = 7;
                            String file1 = "";
                            file1 = currentDirectory + "\\report\\" + datetick + ".pdf";
                            //new LogWriter("d", file1);
                            if (!File.Exists(file1))
                            {
                                MessageBox.Show("ไม่พบ File " + file1, "");
                            }
                            webBrowser1.Navigate(file1);
                            tabHnLabOut.Controls.Add(webBrowser1);
                        }
                        else
                        {
                            labOutView = new C1FlexViewer();
                            labOutView.AutoScrollMargin = new System.Drawing.Size(0, 0);
                            labOutView.AutoScrollMinSize = new System.Drawing.Size(0, 0);
                            labOutView.Dock = System.Windows.Forms.DockStyle.Fill;
                            labOutView.Location = new System.Drawing.Point(0, 0);
                            labOutView.Name = "c1FlexViewer1" + k.ToString();
                            labOutView.Size = new System.Drawing.Size(1065, 790);
                            labOutView.TabIndex = 0;
                            labOutView.Ribbon.Minimized = true;
                            tabHnLabOut.Controls.Add(labOutView);


                            C1PdfDocumentSource pds = new C1PdfDocumentSource();

                            pds.LoadFromStream(stream);

                            //pds.LoadFromFile(filename1);

                            labOutView.DocumentSource = pds;
                        }
                    }
                    catch(Exception ex)
                    {
                        new LogWriter("e", "FrmScanView1 setTabHnLabOut " + ex.Message+" hn "+txtHn.Text);
                    }
                    //}
                }
            }
            else
            {
                tabHnLabOutR.Clear();
            }
            if (dtlaboutdsc.Rows.Count > 0)
            {
                int k = 0;
                foreach (DataRow rowdsc in dtlaboutdsc.Rows)
                {
                    try
                    {
                        String ext1 = "";
                        ext1 = Path.GetExtension(rowdsc[bc.bcDB.dscDB.dsc.image_path].ToString());
                        if (ext1.Length <= 0) continue;
                        k++;
                        //pB1.Value = k;
                        String vn = "", preno = "", vsdate = "", an = "", labexid = "", yearid = "", filename = "", filename1 = "", datetick = "";
                        datetick = DateTime.Now.Ticks.ToString();
                        //labexid = dt.Rows[0][bc.bcDB.labexDB.labex.Id].ToString();
                        //filename = labexid + "_" + k.ToString() + ".pdf";
                        //yearid = dt.Rows[0][bc.bcDB.labexDB.labex.YearId].ToString();
                        //vn = dt.Rows[0][bc.bcDB.labexDB.labex.Vn].ToString();
                        //vn = vn.Replace("/", ".").Replace("(", ".").Replace(")", "");

                        C1DockingTabPage tabHnLabOut = new C1DockingTabPage();
                        //tabHnLabOut.Location = new System.Drawing.Point(1, 24);
                        //tabScan.Name = "c1DockingTabPage1";
                        tabHnLabOut.Size = new System.Drawing.Size(667, 175);
                        tabHnLabOut.TabIndex = 0;
                        //tabHnLabOut.Text = bc.datetoShow(rowdsc["date_create"].ToString());
                        if (rowdsc["ml_fm"].ToString().Equals("FM-LAB-997"))
                        {
                            tabHnLabOut.Text = "Patho " + bc.datetoShow(rowdsc["date_create"].ToString());
                        }
                        else if (rowdsc["ml_fm"].ToString().Equals("FM-LAB-998"))
                        {
                            tabHnLabOut.Text = "Clinical " + bc.datetoShow(rowdsc["date_create"].ToString());
                        }
                        else
                        {
                            tabHnLabOut.Text = rowdsc["ml_fm"].ToString();
                        }
                        tabHnLabOut.Name = "tabHnLabOut_" + datetick;
                        //tabHnLabOut.DoubleClick += TabHnLabOut_DoubleClick;
                        
                        //for (int i = 0; i < 6; i++)
                        //{
                        MemoryStream stream;

                        if (!Directory.Exists("report"))
                        {
                            Directory.CreateDirectory("report");
                        }

                        FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                        stream = ftpc.download(rowdsc[bc.bcDB.dscDB.dsc.folder_ftp] + "/" + rowdsc[bc.bcDB.dscDB.dsc.image_path].ToString());
                        if (stream == null)
                        {
                            setHeaderEnable();
                            tabHnLabOut.Dispose();
                            continue;
                        }
                        if (stream.Length == 0)
                        {
                            setHeaderEnable();
                            tabHnLabOut.Dispose();
                            continue;
                        }
                        stream.Seek(0, SeekOrigin.Begin);
                        String ext = Path.GetExtension(rowdsc[bc.bcDB.dscDB.dsc.image_path].ToString());

                        Application.DoEvents();
                        
                        tcHnLabOut.TabPages.Add(tabHnLabOut);
                        //tcHnLabOut.Controls.Add(tablabOut);
                        if (bc.iniC.windows.Equals("windowsxp"))
                        {
                            var fileStream = new FileStream("report\\" + datetick + ext, FileMode.Create, FileAccess.Write);
                            stream.CopyTo(fileStream);
                            Thread.Sleep(50);
                            fileStream.Flush();
                            fileStream.Dispose();
                            if (ext.Equals(".jpg"))
                            {
                                C1PictureBox labOutView = new C1PictureBox();
                                Image img = Image.FromStream(stream);
                                labOutView.Dock = DockStyle.None;
                                labOutView.SizeMode = PictureBoxSizeMode.StretchImage;
                                labOutView.Image = img;
                                labOutView.Size = new Size(bc.tabLabOutImageWidth, bc.tabLabOutImageHeight);
                                ContextMenu menuGw = new ContextMenu();
                                menuGw.MenuItems.Add("Print Out Lab", new EventHandler(ContextMenu_LabOut_Print));
                                labOutView.ContextMenu = menuGw;
                                streamPrint = stream;
                                tabHnLabOut.Controls.Add(labOutView);
                            }
                            else
                            {
                                string currentDirectory = Directory.GetCurrentDirectory();
                                WebBrowser webBrowser1;
                                webBrowser1 = new System.Windows.Forms.WebBrowser();
                                //webBrowser1.Enabled = true;
                                //webBrowser1.Location = new System.Drawing.Point(192, 0);
                                webBrowser1.Name = "webBrowser1";
                                //axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(Resources.GetObject("axAcroPDF1.OcxState")));
                                //webBrowser1.Size = new System.Drawing.Size(192, 192);
                                webBrowser1.Dock = DockStyle.Fill;
                                //axAcroPDF1.TabIndex = 7;
                                String file1 = "";
                                file1 = currentDirectory + "\\report\\" + datetick + ext;
                                //new LogWriter("d", file1);
                                if (!File.Exists(file1))
                                {
                                    MessageBox.Show("ไม่พบ File " + file1, "");
                                }
                                webBrowser1.Navigate(file1);
                                tabHnLabOut.Controls.Add(webBrowser1);
                            }
                            
                        }
                        else
                        {
                            if (ext.Equals(".jpg"))
                            {
                                C1PictureBox labOutView = new C1PictureBox();
                                Image img = Image.FromStream(stream);
                                //Image resizedImage = null;
                                //int originalWidth = 0;
                                //int newHeight = 300;
                                //int newWidth = 1200;
                                //originalHeight = 0;
                                //originalWidth = img.Width;
                                //originalHeight = img.Height;
                                ////resizedImage = img.GetThumbnailImage((newHeight * img.Width) / originalHeight, newHeight, null, IntPtr.Zero);
                                //Image resizedImage = img.GetThumbnailImage(newWidth, (newWidth * img.Height) / originalWidth, null, IntPtr.Zero);
                                labOutView.Dock = DockStyle.None;
                                labOutView.SizeMode = PictureBoxSizeMode.StretchImage;
                                labOutView.Image = img;
                                labOutView.Size = new Size(bc.tabLabOutImageWidth, bc.tabLabOutImageHeight);

                                tabHnLabOut.Controls.Add(labOutView);
                            }
                            else
                            {
                                labOutView = new C1FlexViewer();
                                labOutView.AutoScrollMargin = new System.Drawing.Size(0, 0);
                                labOutView.AutoScrollMinSize = new System.Drawing.Size(0, 0);
                                labOutView.Dock = System.Windows.Forms.DockStyle.Fill;
                                labOutView.Location = new System.Drawing.Point(0, 0);
                                labOutView.Name = "c1FlexViewer1" + k.ToString();
                                labOutView.Size = new System.Drawing.Size(1065, 790);
                                labOutView.TabIndex = 0;
                                labOutView.Ribbon.Minimized = true;
                                tabHnLabOut.Controls.Add(labOutView);

                                ContextMenu menuGw = new ContextMenu();
                                menuGw.MenuItems.Add("Export Out Lab", new EventHandler(ContextMenu_LabOut_export_outlab));
                                labOutView.ContextMenu = menuGw;

                                C1PdfDocumentSource pds = new C1PdfDocumentSource();
                                pds.LoadFromStream(stream);
                                streamPrint = stream;
                                //pds.LoadFromFile(filename1);

                                labOutView.DocumentSource = pds;
                                //labOutView.Ribbon.Minimized = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        new LogWriter("e", "FrmScanView1 setTabHnLabOut "+ex.Message);
                    }
                }
            }
            setHeaderEnable();
        }
        private void ContextMenu_LabOut_export_outlab(object sender, System.EventArgs e)
        {
            String datetick = DateTime.Now.Ticks.ToString();
            streamPrint.Seek(0, SeekOrigin.Begin);
            //streamPrint.CopyTo
            var fileStream = new FileStream(bc.iniC.medicalrecordexportpath+"\\outlab_"+txtHn.Text.Trim()+"_" + datetick + ".pdf", FileMode.Create, FileAccess.Write);
            streamPrint.CopyTo(fileStream);
            fileStream.Flush();
            fileStream.Dispose();
            Application.DoEvents();
            Thread.Sleep(200);
            string filePath = bc.iniC.medicalrecordexportpath + "\\outlab_" + txtHn.Text.Trim() + "_" + datetick + ".pdf";
            if (!File.Exists(filePath))
            {
                return;
            }

            // combine the arguments together
            // it doesn't matter if there is a space after ','
            string argument = "/select, \"" + filePath + "\"";

            System.Diagnostics.Process.Start("explorer.exe", argument);
        }
        private void ContextMenu_LabOut_Print(object sender, System.EventArgs e)
        {
            SetDefaultPrinter(bc.iniC.printerA4);
            setGrfScanToPrint();
        }
        private void setTabMachineResult()
        {
            if (txtHn.Text.Trim().Length <= 0)
            {
                return;
            }
            tcMac.TabPages.Clear();
            DataTable dtlaboutdsc = new DataTable();            
            dtlaboutdsc = bc.bcDB.dscDB.selectMedResultByDateReq("", "", txtHn.Text.Trim());
            if (dtlaboutdsc.Rows.Count > 0)
            {
                int k = 0;
                foreach (DataRow rowdsc in dtlaboutdsc.Rows)
                {
                    try
                    {
                        k++;
                        //pB1.Value = k;
                        String vn = "", preno = "", vsdate = "", an = "", labexid = "", yearid = "", filename = "", filename1 = "", datetick = "";
                        datetick = DateTime.Now.Ticks.ToString();                        

                        C1DockingTabPage tabHnLabOut = new C1DockingTabPage();                        
                        tabHnLabOut.Size = new System.Drawing.Size(667, 175);
                        tabHnLabOut.TabIndex = 0;
                        //tabHnLabOut.Text = bc.datetoShow(rowdsc["date_create"].ToString());
                        
                        tabHnLabOut.Text = rowdsc["ml_fm"].ToString();
                        
                        tabHnLabOut.Name = "tabHnLabOut_" + datetick;
                        //tabHnLabOut.DoubleClick += TabHnLabOut_DoubleClick;
                        tcMac.TabPages.Add(tabHnLabOut);
                        //for (int i = 0; i < 6; i++)
                        //{
                        MemoryStream stream;

                        if (!Directory.Exists("report"))
                        {
                            Directory.CreateDirectory("report");
                        }

                        FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                        stream = ftpc.download(rowdsc[bc.bcDB.dscDB.dsc.folder_ftp] + "/" + rowdsc[bc.bcDB.dscDB.dsc.image_path].ToString());
                        if (stream == null)
                        {
                            continue;
                        }
                        if (stream.Length == 0)
                        {
                            continue;
                        }
                        stream.Seek(0, SeekOrigin.Begin);
                        var fileStream = new FileStream("report\\" + datetick + ".pdf", FileMode.Create, FileAccess.Write);
                        stream.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Dispose();
                        Application.DoEvents();
                        Thread.Sleep(200);

                        //tcHnLabOut.Controls.Add(tablabOut);
                        if (bc.iniC.windows.Equals("windowsxp"))
                        {
                            string currentDirectory = Directory.GetCurrentDirectory();
                            WebBrowser webBrowser1;
                            webBrowser1 = new System.Windows.Forms.WebBrowser();
                            //webBrowser1.Enabled = true;
                            //webBrowser1.Location = new System.Drawing.Point(192, 0);
                            webBrowser1.Name = "webBrowser1";
                            //axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(Resources.GetObject("axAcroPDF1.OcxState")));
                            //webBrowser1.Size = new System.Drawing.Size(192, 192);
                            webBrowser1.Dock = DockStyle.Fill;
                            //axAcroPDF1.TabIndex = 7;
                            String file1 = "";
                            file1 = currentDirectory + "\\report\\" + datetick + ".pdf";
                            //new LogWriter("d", file1);
                            if (!File.Exists(file1))
                            {
                                MessageBox.Show("ไม่พบ File " + file1, "");
                            }
                            webBrowser1.Navigate(file1);
                            tabHnLabOut.Controls.Add(webBrowser1);
                        }
                        else
                        {
                            labOutView = new C1FlexViewer();
                            labOutView.AutoScrollMargin = new System.Drawing.Size(0, 0);
                            labOutView.AutoScrollMinSize = new System.Drawing.Size(0, 0);
                            labOutView.Dock = System.Windows.Forms.DockStyle.Fill;
                            labOutView.Location = new System.Drawing.Point(0, 0);
                            labOutView.Name = "c1FlexViewer1" + k.ToString();
                            labOutView.Size = new System.Drawing.Size(1065, 790);
                            labOutView.TabIndex = 0;
                            labOutView.Ribbon.Minimized = true;
                            tabHnLabOut.Controls.Add(labOutView);

                            C1PdfDocumentSource pds = new C1PdfDocumentSource();

                            pds.LoadFromStream(stream);

                            //pds.LoadFromFile(filename1);
                            labOutView.Ribbon.Minimized = true;
                            labOutView.DocumentSource = pds;
                        }
                    }
                    catch (Exception ex)
                    {
                        new LogWriter("e", "FrmScanView1 setTabHnLabOut " + ex.Message);
                    }
                }
            }
        }
        private void setHeaderEnable(ProgressBar pB1)
        {
            pB1.Dispose();
            txtVN.Show();
            txtHn.Show();
            txtName.Show();
            //label1.Show();
            //cboDgs.Show();
            
            //btnHn.Show();
            //txt.Show();
            //label6.Show();
            chkIPD.Show();
            //grf1.AutoSizeRows();
            //grf1.AutoSizeRows();
            panel2.Enabled = true;
        }
        private void setHeaderEnable()
        {
            
            txtVN.Show();
            //txtHn.Show();
            //txtName.Show();
            
            chkIPD.Show();
            
            panel2.Enabled = true;
        }
        private void setHeaderDisable()
        {
            txtVN.Hide();
            //txtHn.Hide();
            //txtName.Hide();
            //label1.Hide();
            //cboDgs.Hide();
            
            btnHn.Hide();
            //txt.Hide();
            //label6.Show();
            chkIPD.Hide();
            //grf1.AutoSizeRows();
            //grf1.AutoSizeRows();
            panel2.Enabled = false;
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            grfIPD.AutoSizeCols();
            grfIPD.AutoSizeRows();
        }
        private void BtnHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmSearchHn frm = new FrmSearchHn(bc, FrmSearchHn.StatusConnection.host);
            frm.ShowDialog(this);
            txtHn.Value = bc.sPtt.Hn;
            txtName.Value = bc.sPtt.Name;
            setGrfVsIPD();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void showFormWaiting()
        {
            frmFlash = new Form();
            frmFlash.Size = new Size(300, 300);
            frmFlash.StartPosition = FormStartPosition.CenterScreen;
            C1PictureBox picFlash = new C1PictureBox();
            //Image img = new Image();
            picFlash.SuspendLayout();
            picFlash.Image = Resources.loading_transparent;
            picFlash.Width = 230;
            picFlash.Height = 230;
            picFlash.Location = new Point(30, 10);
            picFlash.SizeMode = PictureBoxSizeMode.StretchImage;
            frmFlash.Controls.Add(picFlash);
            picFlash.ResumeLayout();
            frmFlash.Show();
            Application.DoEvents();
        }
        private void initGrfOPD()
        {
            grfOPD = new C1FlexGrid();
            grfOPD.Font = fEdit;
            grfOPD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPD.Location = new System.Drawing.Point(0, 0);
            grfOPD.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);
            //grfOPD.AfterScroll += GrfOPD_AfterScroll;
            grfOPD.AfterRowColChange += GrfOPD_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการลบข้อมูลมั้งหมด ของ รายการนี้", new EventHandler(ContextMenu_delete_opd_all));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            grfOPD.ContextMenu = menuGw;

            tabOPD.Controls.Add(grfOPD);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfOPD, bc.iniC.themegrfOpd);
        }
                
        private void ContextMenu_delete_opd_all(object sender, System.EventArgs e)
        {
            String id = "", vn="";
            vn = grfOPD[grfOPD.Row, colVsVn].ToString();
            if (MessageBox.Show("ต้องการลบข้อมูล ทั้งหมดของ VN "+ vn, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                int chk = 0;
                String re = bc.bcDB.dscDB.voidDocScanVN(vn, "");
                if (int.TryParse(re, out chk))
                {
                    setGrfLab();
                    setGrfXray(grfOPD.Row);
                    setGrfScan();
                    if (!bc.iniC.windows.Equals("windowsxp"))
                    {
                        //setTabLabOut(grfOPD.Row, "OPD", bc.iniC.windows);
                    }
                }
            }
        }
        private void GrfOPD_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 !=1) return;

            if (txtHn.Text.Equals("")) return;
            grfActive = "grfOPD";
            chkIPD.Checked = false;
            label2.Text = "VN :";
            if(rtb !=null)
                rtb.Text = "";
            //txt.Value = "";
            //new LogWriter("d", "FrmScanView1 GrfOPD_AfterRowColChange 01 setGrfLab");
            try
            {
                txtVN.Value = grfOPD[grfOPD.Row, colVsVn] != null ? grfOPD[grfOPD.Row, colVsVn].ToString() : "";
                this.preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                setActive();
                
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmScanView1 GrfOPD_AfterRowColChange "+ex.Message);
            }
            finally
            {
                //frmFlash.Dispose();
            }
            
            grfOPD.Focus();
        }
        private void initGrfPicture()
        {
            Size size = new Size();
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            grfPic = new C1FlexGrid();
            grfPic.Font = fEdit;
            grfPic.Dock = DockStyle.Fill;
            grfPic.Location = new Point(0, 0);
            grfPic.Rows.Count = 1;
            grfPic.Name = "grfScan";
            grfPic.Cols.Count = 5;
            Column colpic1 = grfPic.Cols[colPic1];
            colpic1.DataType = typeof(Image);
            Column colpic2 = grfPic.Cols[colPic2];
            colpic2.DataType = typeof(String);
            Column colpic3 = grfPic.Cols[colPic3];
            colpic3.DataType = typeof(Image);
            Column colpic4 = grfPic.Cols[colPic4];
            colpic4.DataType = typeof(String);
            grfPic.Cols[colPic1].Width = bc.grfScanWidth;
            grfPic.Cols[colPic2].Width = bc.grfScanWidth;
            grfPic.Cols[colPic3].Width = bc.grfScanWidth;
            grfPic.Cols[colPic4].Width = bc.grfScanWidth;
            grfPic.ShowCursor = true;
            grfPic.Cols[colPic2].Visible = false;
            grfPic.Cols[colPic3].Visible = true;
            grfPic.Cols[colPic4].Visible = false;
            grfPic.Cols[colPic1].AllowEditing = false;
            grfPic.Cols[colPic3].AllowEditing = false;
            grfPic.DoubleClick += GrfPic_DoubleClick;
            panel.Controls.Add(grfPic);
            tabPic.Controls.Add(panel);
            theme1.SetTheme(grfPic, bc.iniC.themeApp);
            theme1.SetTheme(panel, bc.iniC.themeApp);
        }

        private void GrfPic_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void initTabOrdAdd()
        {
            C1DockingTab tcOrdSearch;
            C1DockingTabPage tabOrdSearchDrug, tabOrdSearchSup, tabOrdSearchLab, tabOrdSearchXray, tabOrdSearchOR;
            Panel pnOrdLeft = new Panel();
            Panel pnOrdRight = new Panel();
            pnOrdLeft.Dock = DockStyle.Fill;
            pnOrdRight.Dock = DockStyle.Fill;
            Panel pnOrdDrugSarch = new Panel();
            Panel pnOrdDrugAdd = new Panel();
            Panel pnOrdDiag1 = new Panel();
            Panel pnOrdDiag2 = new Panel();
            Panel pnOrdDiag3 = new Panel();
            pnOrdDiagVal = new Panel();

            pnOrdDrugSarch.Dock = DockStyle.Fill;
            pnOrdDrugAdd.Dock = DockStyle.Fill;
            pnOrdDiag1.Dock = DockStyle.Fill;
            pnOrdDiag2.Dock = DockStyle.Fill;
            pnOrdDiag3.Dock = DockStyle.Fill;
            pnOrdDiagVal.Dock = DockStyle.Fill;

            pnOrdSearchDrug = new Panel();
            pnOrdSearchSup = new Panel();
            pnOrdSearchLab = new Panel();
            pnOrdSearchXray = new Panel();
            pnOrdSearchOR = new Panel();
            pnOrdItem = new Panel();
            pnOrdSearchDrug.Dock = DockStyle.Fill;
            pnOrdSearchSup.Dock = DockStyle.Fill;
            pnOrdSearchLab.Dock = DockStyle.Fill;
            pnOrdSearchXray.Dock = DockStyle.Fill;
            pnOrdSearchOR.Dock = DockStyle.Fill;
            pnOrdItem.Dock = DockStyle.Fill;

            C1SplitterPanel scOrdLeft = new C1.Win.C1SplitContainer.C1SplitterPanel();
            C1SplitterPanel scOrdRight = new C1.Win.C1SplitContainer.C1SplitterPanel();
            C1SplitContainer sCOrdAdd = new C1.Win.C1SplitContainer.C1SplitContainer();

            C1SplitterPanel scOrdDrugAdd = new C1.Win.C1SplitContainer.C1SplitterPanel();
            C1SplitterPanel scOrdDrugSearch = new C1.Win.C1SplitContainer.C1SplitterPanel();
            C1SplitContainer sCOrdDrug = new C1.Win.C1SplitContainer.C1SplitContainer();

            scOrdDiag1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            scOrdDiag2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            scOrdDiag3 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            sCOrdDiag = new C1.Win.C1SplitContainer.C1SplitContainer();
            
            tcOrdSearch = new C1DockingTab();
            tabOrdSearchDrug = new C1DockingTabPage();
            tabOrdSearchSup = new C1DockingTabPage();
            tabOrdSearchLab = new C1DockingTabPage();
            tabOrdSearchXray = new C1DockingTabPage();
            tabOrdSearchOR = new C1DockingTabPage();

            tcOrdSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            tcOrdSearch.Location = new System.Drawing.Point(0, 266);
            tcOrdSearch.Name = "tabOrdSearch";
            tcOrdSearch.Size = new System.Drawing.Size(669, 200);
            tcOrdSearch.TabIndex = 0;
            tcOrdSearch.TabsSpacing = 5;
            tcOrdSearch.Alignment = TabAlignment.Left;
            tcOrdSearch.TabClick += TabOrdSearch_TabClick;
            tcOrdSearch.Font = fEdit;
            //tcDtr.SelectedTabChanged += TcDtr_SelectedTabChanged1;
            pnOrdDrugSarch.Controls.Add(tcOrdSearch);
            theme1.SetTheme(tcOrdSearch, bc.iniC.themeApplication);
            
            tabOrdSearchDrug.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOrdSearchDrug.Size = new System.Drawing.Size(667, 175);
            tabOrdSearchDrug.TabIndex = 0;
            tabOrdSearchDrug.Text = "Drug List";
            tabOrdSearchDrug.Name = "tabOrdSearchDrug";
            tabOrdSearchDrug.Controls.Add(pnOrdSearchDrug);
            tabOrdSearchDrug.Font = fEdit;
            
            tabOrdSearchSup.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOrdSearchSup.Size = new System.Drawing.Size(667, 175);
            tabOrdSearchSup.TabIndex = 0;
            tabOrdSearchSup.Text = "Supply List";
            tabOrdSearchSup.Name = "tabOrdSearchSup";
            tabOrdSearchSup.Controls.Add(pnOrdSearchSup);
            
            tabOrdSearchLab.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOrdSearchLab.Size = new System.Drawing.Size(667, 175);
            tabOrdSearchLab.TabIndex = 0;
            tabOrdSearchLab.Text = "LAB List";
            tabOrdSearchLab.Name = "tabOrdSearchLab";
            tabOrdSearchLab.Controls.Add(pnOrdSearchLab);
            
            tabOrdSearchXray.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOrdSearchXray.Size = new System.Drawing.Size(667, 175);
            tabOrdSearchXray.TabIndex = 0;
            tabOrdSearchXray.Text = "X-Ray List";
            tabOrdSearchXray.Name = "tabOrdSearchXray";
            tabOrdSearchXray.Controls.Add(pnOrdSearchXray);
            
            tabOrdSearchOR.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOrdSearchOR.Size = new System.Drawing.Size(667, 175);
            tabOrdSearchOR.TabIndex = 0;
            tabOrdSearchOR.Text = "OR List";
            tabOrdSearchOR.Name = "tabOrdSearchOR";
            tabOrdSearchOR.Controls.Add(pnOrdSearchOR);
            
            tcOrdSearch.Controls.Add(tabOrdSearchDrug);
            tcOrdSearch.Controls.Add(tabOrdSearchSup);
            tcOrdSearch.Controls.Add(tabOrdSearchLab);
            tcOrdSearch.Controls.Add(tabOrdSearchXray);
            tcOrdSearch.Controls.Add(tabOrdSearchOR);

            tcOrdSearch.SuspendLayout();
            sCOrdAdd.SuspendLayout();
            scOrdLeft.SuspendLayout();
            scOrdRight.SuspendLayout();
            pnOrdLeft.SuspendLayout();
            pnOrdRight.SuspendLayout();
            sCOrdDrug.SuspendLayout();
            scOrdDrugSearch.SuspendLayout();
            scOrdDrugAdd.SuspendLayout();
            sCOrdDiag.SuspendLayout();
            scOrdDiag1.SuspendLayout();
            scOrdDiag2.SuspendLayout();
            scOrdDiag3.SuspendLayout();
            pnOrdDrugSarch.SuspendLayout();
            pnOrdDrugAdd.SuspendLayout();
            pnOrdItem.SuspendLayout();
            pnOrdSearchDrug.SuspendLayout();
            pnOrdSearchSup.SuspendLayout();
            pnOrdSearchLab.SuspendLayout();
            pnOrdSearchXray.SuspendLayout();
            pnOrdSearchOR.SuspendLayout();
            pnOrdDiag1.SuspendLayout();
            pnOrdDiag2.SuspendLayout();
            pnOrdDiag3.SuspendLayout();
            pnOrdDiagVal.SuspendLayout();

            sCOrdAdd.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCOrdAdd.Name = "sCOrdAdd";
            sCOrdAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            sCOrdAdd.Panels.Add(scOrdLeft);
            sCOrdAdd.Panels.Add(scOrdRight);
            sCOrdAdd.HeaderHeight = 20;

            scOrdLeft.Collapsible = true;
            scOrdLeft.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            scOrdLeft.Location = new System.Drawing.Point(0, 21);
            scOrdLeft.Name = "scOrdLeft";
            scOrdLeft.Controls.Add(pnOrdLeft);
            //scOrdLeft.HeaderHeight = 10;
            scOrdRight.Collapsible = true;
            scOrdRight.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            scOrdRight.Location = new System.Drawing.Point(0, 21);
            scOrdRight.Name = "scOrdRight";
            //scOrdRight.HeaderHeight = 10;
            scOrdRight.Controls.Add(pnOrdRight);
            pnOrdRight.Controls.Add(sCOrdDrug);

            tabOrdAdd.Controls.Add(sCOrdAdd);

            sCOrdDrug.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCOrdDrug.Name = "sCOrdDrug";
            sCOrdDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            sCOrdDrug.Panels.Add(scOrdDrugSearch);
            sCOrdDrug.Panels.Add(scOrdDrugAdd);
            sCOrdDrug.HeaderHeight = 20;
            scOrdDrugSearch.Collapsible = true;
            scOrdDrugSearch.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Top;
            scOrdDrugSearch.Location = new System.Drawing.Point(0, 21);
            scOrdDrugSearch.Name = "scOrdDrugSearch";
            scOrdDrugSearch.Controls.Add(pnOrdDrugSarch);
            //scOrdLeft.HeaderHeight = 10;
            scOrdDrugAdd.Collapsible = true;
            scOrdDrugAdd.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Bottom;
            scOrdDrugAdd.Location = new System.Drawing.Point(0, 21);
            scOrdDrugAdd.Name = "scOrdDrugAdd";
            //scOrdRight.HeaderHeight = 10;
            scOrdDrugAdd.Controls.Add(pnOrdItem);

            pnOrdLeft.Controls.Add(sCOrdDiag);
            sCOrdDiag.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCOrdDiag.Name = "sCOrdDiag";
            sCOrdDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            //sCOrdDiag.Panels.Add(pnOrdDiagVal);
            sCOrdDiag.Panels.Add(scOrdDiag1);
            sCOrdDiag.Panels.Add(scOrdDiag2);
            sCOrdDiag.Panels.Add(scOrdDiag3);
            sCOrdDiag.HeaderHeight = 20;
            scOrdDiag1.Collapsible = true;
            scOrdDiag1.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Top;
            scOrdDiag1.Location = new System.Drawing.Point(0, 21);
            scOrdDiag1.Name = "scOrdDiag1";
            pnOrdDiagVal.Name = "pnOrdDiagVal";
            pnOrdDiagVal.Height = 100;
            pnOrdDiagVal.Dock = DockStyle.Top;
            pnOrdDiag1.Dock = DockStyle.Fill;
            //pnOrdDiagVal.BackColor = Color.Red;
            //pnOrdDiagVal.BackColor = Color.Red;
            //pnOrdDiag1.Height = 80;
            //scOrdDiag1.ClientSize = new Size(20, 200);
            //scOrdDiag2.ClientSize = new Size(20, 600);
            //scOrdDiag3.ClientSize = new Size(20, 80);
            //scOrdDiag1.Height = 200;
            scOrdDiag1.Controls.Add(pnOrdDiag1);
            scOrdDiag1.Controls.Add(pnOrdDiagVal);
            //scOrdLeft.HeaderHeight = 10;
            scOrdDiag2.Collapsible = true;
            scOrdDiag2.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Top;
            scOrdDiag2.Location = new System.Drawing.Point(0, 21);
            scOrdDiag2.Name = "scOrdDiag2";
            //scOrdRight.HeaderHeight = 10;
            //pnOrdDiag2.Height = 900;
            //pnOrdDiag3.Height = 200;
            scOrdDiag2.Controls.Add(pnOrdDiag2);
            //scOrdDiag2.Height = 900;
            //scOrdDiag2.ClientSize = new Size(20, 600);
            //scOrdDiag3.Height = 200;
            scOrdDiag3.Collapsible = true;
            scOrdDiag3.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Bottom;
            scOrdDiag3.Location = new System.Drawing.Point(0, 21);
            scOrdDiag3.Name = "scOrdDiag3";
            //scOrdDiag3.ClientSize = new Size(20, 80);
            //scOrdRight.HeaderHeight = 10;
            scOrdDiag3.Controls.Add(pnOrdDiag3);
            initComTabVital();
            Visit vs = new Visit();
            vs = bc.bcDB.vsDB.selectVisit(txtHn.Text.Trim(), vsDate, preno);
            setControlPnOrdDiagVal(vs.bp1l, vs.breath, vs.temp, vs.weight, vs.high,"",vs.cc, vs.ccin, vs.ccex,"","", vs.bp1l, vs.bp2l,"","",vs.symptom, vsDate,vs.PaidName);

            FrmDoctorDiag1 frmDtrDiag1 = new FrmDoctorDiag1(bc, "cc", txtHn.Text.Trim());
            frmDtrDiag1.FormBorderStyle = FormBorderStyle.None;
            frmDtrDiag1.TopLevel = false;
            frmDtrDiag1.Dock = DockStyle.Fill;
            frmDtrDiag1.AutoScroll = true;
            pnOrdDiag1.Controls.Add(frmDtrDiag1);
            FrmDoctorDiag1 frmDtrDiag2 = new FrmDoctorDiag1(bc, "me", txtHn.Text.Trim());
            frmDtrDiag2.FormBorderStyle = FormBorderStyle.None;
            frmDtrDiag2.TopLevel = false;
            frmDtrDiag2.Dock = DockStyle.Fill;
            frmDtrDiag2.AutoScroll = true;
            pnOrdDiag2.Controls.Add(frmDtrDiag2);
            FrmDoctorDiag1 frmDtrDiag3 = new FrmDoctorDiag1(bc, "diag", txtHn.Text.Trim());
            
            frmDtrDiag3.FormBorderStyle = FormBorderStyle.None;
            frmDtrDiag3.TopLevel = false;
            frmDtrDiag3.Dock = DockStyle.Fill;
            frmDtrDiag3.AutoScroll = true;
            pnOrdDiag3.Controls.Add(frmDtrDiag3);
            frmDtrDiag1.Show();
            frmDtrDiag2.Show();
            frmDtrDiag3.Show();
            //pnOrdDiag1.BackColor = Color.Red;
            scOrdLeft.SizeRatio = 20;

            tcOrdSearch.ResumeLayout(false);
            pnOrdLeft.ResumeLayout(false);
            pnOrdRight.ResumeLayout(false);
            pnOrdItem.ResumeLayout(false);
            pnOrdDrugSarch.ResumeLayout(false);
            pnOrdDrugAdd.ResumeLayout(false);
            scOrdLeft.ResumeLayout(false);
            scOrdRight.ResumeLayout(false);
            sCOrdAdd.ResumeLayout(false);
            sCOrdDrug.ResumeLayout(false);
            scOrdDrugSearch.ResumeLayout(false);
            scOrdDrugAdd.ResumeLayout(false);
            sCOrdDiag.ResumeLayout(false);
            scOrdDiag1.ResumeLayout(false);
            scOrdDiag2.ResumeLayout(false);
            scOrdDiag3.ResumeLayout(false);
            pnOrdSearchDrug.ResumeLayout(false);
            pnOrdSearchSup.ResumeLayout(false);
            pnOrdSearchLab.ResumeLayout(false);
            pnOrdSearchXray.ResumeLayout(false);
            pnOrdSearchOR.ResumeLayout(false);
            pnOrdDiag1.ResumeLayout(false);
            pnOrdDiag2.ResumeLayout(false);
            pnOrdDiag3.ResumeLayout(false);
            pnOrdDiagVal.ResumeLayout(false);

            tcOrdSearch.PerformLayout();
            pnOrdLeft.PerformLayout();
            pnOrdRight.PerformLayout();
            pnOrdDrugSarch.PerformLayout();
            pnOrdDrugAdd.PerformLayout();
            pnOrdItem.PerformLayout();
            scOrdLeft.PerformLayout();
            scOrdRight.PerformLayout();
            sCOrdAdd.PerformLayout();
            sCOrdDrug.PerformLayout();
            scOrdDrugSearch.PerformLayout();
            scOrdDrugAdd.PerformLayout();
            sCOrdDiag.PerformLayout();
            scOrdDiag1.PerformLayout();
            scOrdDiag2.PerformLayout();
            scOrdDiag3.PerformLayout();
            pnOrdSearchDrug.PerformLayout();
            pnOrdSearchSup.PerformLayout();
            pnOrdSearchLab.PerformLayout();
            pnOrdSearchXray.PerformLayout();
            pnOrdSearchOR.PerformLayout();
            pnOrdDiag1.PerformLayout();
            pnOrdDiag2.PerformLayout();
            pnOrdDiag3.PerformLayout();
            pnOrdDiagVal.PerformLayout();

            pnOrdDiag1.Height = 900;
            //scOrdDiag1.ClientSize = new Size(20, 100);
            //scOrdDiag2.ClientSize = new Size(20, 600);
            //scOrdDiag3.ClientSize = new Size(20, 100);
            scOrdDiag1.SizeRatio = 25;
            scOrdDiag2.SizeRatio = 70;
            scOrdDiag3.SizeRatio = 5;
            //theme1.SetTheme(tabOrdSearchDrug, "ExpressionDark");
            //tabOrdSearchDrug.tabc
        }
        private void initComTabVital()
        {
            int gapLine = 20, gapX = 20, gapY=20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            lbPttVitalSigns = new Label();
            lbPttVitalSigns.Text = "Vital Sign :";
            lbPttVitalSigns.Font = fEdit;
            size = bc.MeasureString(lbPttVitalSigns);
            lbPttVitalSigns.Location = new System.Drawing.Point(gapX, gapY);
            lbPttVitalSigns.AutoSize = true;
            lbPttVitalSigns.Name = "lbPttVitalSigns";

            lbPttPressure = new Label();
            lbPttPressure.Text = "Pressure :";
            lbPttPressure.Font = fEdit;
            size = bc.MeasureString(lbPttPressure);
            lbPttPressure.Location = new System.Drawing.Point(lbPttVitalSigns.Location.X+ size.Width + 20, gapY);
            lbPttPressure.AutoSize = true;
            lbPttPressure.Name = "lbPttPressure";

            lbPttTemp = new Label();
            lbPttTemp.Text = "Temp :";
            lbPttTemp.Font = fEdit;
            size = bc.MeasureString(lbPttTemp);
            lbPttTemp.Location = new System.Drawing.Point(lbPttPressure.Location.X + size.Width + 20, gapY);
            lbPttTemp.AutoSize = true;
            lbPttTemp.Name = "lbPttTemp";

            lbPttWeight = new Label();
            lbPttWeight.Text = "Weight :";
            lbPttWeight.Font = fEdit;
            size = bc.MeasureString(lbPttWeight);
            lbPttWeight.Location = new System.Drawing.Point(lbPttTemp.Location.X + size.Width + 20, gapY);
            lbPttWeight.AutoSize = true;
            lbPttWeight.Name = "lbPttWeight";

            lbPttHigh = new Label();
            lbPttHigh.Text = "High :";
            lbPttHigh.Font = fEdit;
            size = bc.MeasureString(lbPttWeight);
            lbPttHigh.Location = new System.Drawing.Point(lbPttWeight.Location.X + size.Width + 20, gapY);
            lbPttHigh.AutoSize = true;
            lbPttHigh.Name = "lbPttHigh";

            lbPttBloodGroup = new Label();
            lbPttBloodGroup.Text = "BloodGroup :";
            lbPttBloodGroup.Font = fEdit;
            size = bc.MeasureString(lbPttBloodGroup);
            lbPttBloodGroup.Location = new System.Drawing.Point(lbPttHigh.Location.X + size.Width + 20, gapY);
            lbPttBloodGroup.AutoSize = true;
            lbPttBloodGroup.Name = "lbPttBloodGroup";

            gapY += gapLine;
            lbPttCC = new Label();
            lbPttCC.Text = "CC :";
            lbPttCC.Font = fEdit;
            size = bc.MeasureString(lbPttCC);
            lbPttCC.Location = new System.Drawing.Point(gapX, gapY);
            lbPttCC.AutoSize = true;
            lbPttCC.Name = "lbPttCC";

            lbPttCCin = new Label();
            lbPttCCin.Text = "CC in :";
            lbPttCCin.Font = fEdit;
            size = bc.MeasureString(lbPttCCin);
            lbPttCCin.Location = new System.Drawing.Point(lbPttCC.Location.X + size.Width + 20, gapY);
            lbPttCCin.AutoSize = true;
            lbPttCCin.Name = "lbPttCCin";

            lbPttCCex = new Label();
            lbPttCCex.Text = "CC ex :";
            lbPttCCex.Font = fEdit;
            size = bc.MeasureString(lbPttCCex);
            lbPttCCex.Location = new System.Drawing.Point(lbPttCCin.Location.X + size.Width + 20, gapY);
            lbPttCCex.AutoSize = true;
            lbPttCCex.Name = "lbPttCCex";

            lbPttAbc = new Label();
            lbPttAbc.Text = "Abc :";
            lbPttAbc.Font = fEdit;
            size = bc.MeasureString(lbPttAbc);
            lbPttAbc.Location = new System.Drawing.Point(lbPttCCex.Location.X + size.Width + 20, gapY);
            lbPttAbc.AutoSize = true;
            lbPttAbc.Name = "lbPttAbc";

            lbPttHC = new Label();
            lbPttHC.Text = "HC :";
            lbPttHC.Font = fEdit;
            size = bc.MeasureString(lbPttHC);
            lbPttHC.Location = new System.Drawing.Point(lbPttAbc.Location.X + size.Width + 20, gapY);
            lbPttHC.AutoSize = true;
            lbPttHC.Name = "lbPttHC";

            lbPttBp1 = new Label();
            lbPttBp1.Text = "Bp 1 :";
            lbPttBp1.Font = fEdit;
            size = bc.MeasureString(lbPttBp1);
            lbPttBp1.Location = new System.Drawing.Point(lbPttHC.Location.X + size.Width + 20, gapY);
            lbPttBp1.AutoSize = true;
            lbPttBp1.Name = "lbPttBp1";

            lbPttBp2 = new Label();
            lbPttBp2.Text = "Bp 2 :";
            lbPttBp2.Font = fEdit;
            size = bc.MeasureString(lbPttBp2);
            lbPttBp2.Location = new System.Drawing.Point(lbPttBp1.Location.X + size.Width + 20, gapY);
            lbPttBp2.AutoSize = true;
            lbPttBp2.Name = "lbPttBp2";

            lbPttHrate = new Label();
            lbPttHrate.Text = "H rate :";
            lbPttHrate.Font = fEdit;
            size = bc.MeasureString(lbPttHrate);
            lbPttHrate.Location = new System.Drawing.Point(lbPttBp2.Location.X + size.Width + 20, gapY);
            lbPttHrate.AutoSize = true;
            lbPttHrate.Name = "lbPttHrate";

            lbPttLRate = new Label();
            lbPttLRate.Text = "L rate :";
            lbPttLRate.Font = fEdit;
            size = bc.MeasureString(lbPttLRate);
            lbPttLRate.Location = new System.Drawing.Point(lbPttHrate.Location.X + size.Width + 20, gapY);
            lbPttLRate.AutoSize = true;
            lbPttLRate.Name = "lbPttLRate";

            gapY += gapLine;
            lbPttSymptom = new Label();
            lbPttSymptom.Text = "Symptom :";
            lbPttSymptom.Font = fEdit;
            size = bc.MeasureString(lbPttSymptom);
            lbPttSymptom.Location = new System.Drawing.Point(gapX, gapY);
            lbPttSymptom.AutoSize = true;
            lbPttSymptom.Name = "lbPttSymptom";

            lbPttVsDate = new Label();
            lbPttVsDate.Text = "Visit Date :";
            lbPttVsDate.Font = fEdit;
            size = bc.MeasureString(lbPttVsDate);
            lbPttVsDate.Location = new System.Drawing.Point(lbPttSymptom.Location.X + size.Width + 20, gapY);
            lbPttVsDate.AutoSize = true;
            lbPttVsDate.Name = "lbPttVsDate";
            
            lbPaidType = new Label();
            lbPaidType.Text = "สิทธิ :";
            lbPaidType.Font = fEdit;
            size = bc.MeasureString(lbPaidType);
            lbPaidType.Location = new System.Drawing.Point(lbPttSymptom.Location.X + size.Width + 20, gapY);
            lbPaidType.AutoSize = true;
            lbPaidType.Name = "lbPaidType";

            pnOrdDiagVal.Controls.Add(lbPttVitalSigns);
            pnOrdDiagVal.Controls.Add(lbPttPressure);
            pnOrdDiagVal.Controls.Add(lbPttTemp);
            pnOrdDiagVal.Controls.Add(lbPttWeight);
            pnOrdDiagVal.Controls.Add(lbPttHigh);
            pnOrdDiagVal.Controls.Add(lbPttBloodGroup);
            pnOrdDiagVal.Controls.Add(lbPttCC);
            pnOrdDiagVal.Controls.Add(lbPttCCin);
            pnOrdDiagVal.Controls.Add(lbPttCCex);
            pnOrdDiagVal.Controls.Add(lbPttAbc);
            pnOrdDiagVal.Controls.Add(lbPttHC);
            pnOrdDiagVal.Controls.Add(lbPttBp1);
            pnOrdDiagVal.Controls.Add(lbPttBp2);
            pnOrdDiagVal.Controls.Add(lbPttHrate);
            pnOrdDiagVal.Controls.Add(lbPttLRate);
            pnOrdDiagVal.Controls.Add(lbPttSymptom);
            pnOrdDiagVal.Controls.Add(lbPttVsDate);
            pnOrdDiagVal.Controls.Add(lbPaidType);
        }
        private void setControlPnOrdDiagVal(String vitalSign, String pressure, String temp, String weight, String high, String bloodgroup, String cc, String ccin, String ccex
            , String abc, String hc, String bp1, String bp2, String hrate, String lrate, String symptom, String vsdate, String paidtype)
        {
            Size size = new Size();

            lbPttVitalSigns.Text = !vitalSign.Equals("") ? "Vital Sign : "+ vitalSign: "Vital Sign : ";
            lbPttPressure.Text = !pressure.Equals("") ? "Pressure : " + pressure: "Pressure : ";
            lbPttTemp.Text = !temp.Equals("") ? "Temp : " + temp: "Temp : ";
            lbPttWeight.Text = !weight.Equals("") ? "Weight : " + weight: "Weight : " ;
            lbPttHigh.Text = !high.Equals("") ? "High : "+ high: "High : " ;
            lbPttBloodGroup.Text = !bloodgroup.Equals("") ? "BloodGroup : "+ bloodgroup: "BloodGroup : " ;
            lbPttCC.Text = !cc.Equals("") ? "CC : "+ cc: "CC : " ;
            lbPttCCin.Text = !ccin.Equals("") ? "CC in : " + ccin: "CC in :" ;
            lbPttCCex.Text = !ccex.Equals("") ? "CC ex : " + ccex: "CC ex : " ;
            lbPttAbc.Text = !abc.Equals("") ? "Abc :" + abc: "Abc :";
            lbPttHC.Text = !hc.Equals("") ? "HC : ": "HC : " + hc;
            lbPttBp1.Text = !bp1.Equals("") ? "BP1 : " + bp1: "BP1 :";
            lbPttBp2.Text = !bp2.Equals("") ? "BP2 : "+ bp2: "BP2 :";
            lbPttHrate.Text = !hrate.Equals("") ? "H.Rate : "+ hrate: "H.Rate :";
            lbPttLRate.Text = !lrate.Equals("") ? "R.Rate : " + lrate: "R.Rate :" ;
            lbPttSymptom.Text = !symptom.Equals("") ? "Symptom : " + symptom: "Symptom :";
            lbPttVsDate.Text = !vsdate.Equals("") ? "Visit Date : "  + vsdate: "Visit Date :";
            lbPaidType.Text = !paidtype.Equals("") ? "สิทธิ : " + paidtype: "สิทธิ :";

            size = bc.MeasureString(lbPttVitalSigns);
            lbPttVitalSigns.Location = new System.Drawing.Point(lbPttVitalSigns.Location.X, lbPttVitalSigns.Location.Y);
            size = bc.MeasureString(lbPttVitalSigns);
            lbPttPressure.Location = new System.Drawing.Point(lbPttVitalSigns.Location.X + size.Width + 20, lbPttVitalSigns.Location.Y);
            size = bc.MeasureString(lbPttPressure);
            lbPttTemp.Location = new System.Drawing.Point(lbPttPressure.Location.X + size.Width + 20, lbPttVitalSigns.Location.Y);
            size = bc.MeasureString(lbPttTemp);
            lbPttWeight.Location = new System.Drawing.Point(lbPttTemp.Location.X + size.Width + 20, lbPttVitalSigns.Location.Y);
            size = bc.MeasureString(lbPttWeight);
            lbPttHigh.Location = new System.Drawing.Point(lbPttWeight.Location.X + size.Width + 20, lbPttVitalSigns.Location.Y);
            size = bc.MeasureString(lbPttHigh);
            lbPttBloodGroup.Location = new System.Drawing.Point(lbPttHigh.Location.X + size.Width + 20, lbPttVitalSigns.Location.Y);


            size = bc.MeasureString(lbPttCC);
            lbPttCC.Location = new System.Drawing.Point(lbPttCC.Location.X, lbPttCC.Location.Y);
            size = bc.MeasureString(lbPttCC);
            lbPttCCin.Location = new System.Drawing.Point(lbPttCC.Location.X + size.Width + 20, lbPttCC.Location.Y);
            size = bc.MeasureString(lbPttCCin);
            lbPttCCex.Location = new System.Drawing.Point(lbPttCCin.Location.X + size.Width + 20, lbPttCC.Location.Y);
            size = bc.MeasureString(lbPttCCex);
            lbPttAbc.Location = new System.Drawing.Point(lbPttCCex.Location.X + size.Width + 20, lbPttCC.Location.Y);
            size = bc.MeasureString(lbPttAbc);
            lbPttHC.Location = new System.Drawing.Point(lbPttAbc.Location.X + size.Width + 20, lbPttCC.Location.Y);
            size = bc.MeasureString(lbPttHC);
            lbPttBp1.Location = new System.Drawing.Point(lbPttHC.Location.X + size.Width + 20, lbPttCC.Location.Y);
            size = bc.MeasureString(lbPttBp1);
            lbPttBp2.Location = new System.Drawing.Point(lbPttBp1.Location.X + size.Width + 20, lbPttCC.Location.Y);
            size = bc.MeasureString(lbPttBp2);
            lbPttHrate.Location = new System.Drawing.Point(lbPttBp2.Location.X + size.Width + 20, lbPttCC.Location.Y);
            size = bc.MeasureString(lbPttHrate);
            lbPttLRate.Location = new System.Drawing.Point(lbPttHrate.Location.X + size.Width + 20, lbPttCC.Location.Y);

            size = bc.MeasureString(lbPttSymptom);
            lbPttSymptom.Location = new System.Drawing.Point(lbPttSymptom.Location.X, lbPttSymptom.Location.Y);
            size = bc.MeasureString(lbPttSymptom);
            lbPttVsDate.Location = new System.Drawing.Point(lbPttSymptom.Location.X + size.Width + 20, lbPttSymptom.Location.Y);
            size = bc.MeasureString(lbPttVsDate);
            lbPaidType.Location = new System.Drawing.Point(lbPttVsDate.Location.X + size.Width + 20, lbPttSymptom.Location.Y);
        }

        private void TabOrdSearch_TabClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void initGrfIPD()
        {
            grfIPD = new C1FlexGrid();
            grfIPD.Font = fEdit;
            grfIPD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfIPD.Location = new System.Drawing.Point(0, 0);
            grfIPD.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);

            grfIPD.AfterRowColChange += GrfIPD_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("&ต้องการลบข้อมูลมั้งหมด ของ รายการนี้", new EventHandler(ContextMenu_delete_ipd_all));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            grfIPD.ContextMenu = menuGw;

            tabIPD.Controls.Add(grfIPD);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfIPD, bc.iniC.themegrfIpd);
        }
        private void ContextMenu_delete_ipd_all(object sender, System.EventArgs e)
        {
            String id = "", vn = "";
            vn = grfIPD[grfIPD.Row, colIPDAnShow].ToString();
            if (MessageBox.Show("ต้องการลบข้อมูล ทั้งหมดของ AN " + vn, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                int chk = 0;
                String re = bc.bcDB.dscDB.voidDocScanAN(vn, "");
                if (int.TryParse(re, out chk))
                {
                    setGrfLab();
                    setGrfXrayIPD(grfIPD.Row);
                    setGrfScan();
                    //setTabLabOut(grfIPD.Row, "IPD", bc.iniC.windows);
                }
            }
        }
        private void initTabPrn()
        {
            //new LogWriter("e", "initTabPrn 01");
            int y1 = 20, x = 20, gapLine = 70, gapX = 20, gapY=0, col2=90, col3=600,col4=655;
            Size size1 = new Size();
            Size size = new Size();
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            
            GroupBox groupBox1 = new GroupBox();
            
            pnPrnEmailGrfPrn = new Panel();
            tcPrnEmail = new C1DockingTab();
            tabPrnEmailDrug = new C1DockingTabPage();
            tabPrnEmailLab = new C1DockingTabPage();
            tabPrnEmailXray = new C1DockingTabPage();
            tabPrnEmailSummary = new C1DockingTabPage();
            tabPrnEmailOther = new C1DockingTabPage();
            sCPrn = new C1.Win.C1SplitContainer.C1SplitContainer();
            cspPrnLeft = new C1.Win.C1SplitContainer.C1SplitterPanel();
            cspPrnRight = new C1.Win.C1SplitContainer.C1SplitterPanel();
            
            groupBox1.SuspendLayout();
            pnPrnEmailGrfPrn.SuspendLayout();
            tcPrnEmail.SuspendLayout();
            tabPrnEmailDrug.SuspendLayout();
            tabPrnEmailLab.SuspendLayout();
            tabPrnEmailXray.SuspendLayout();
            tabPrnEmailSummary.SuspendLayout();
            tabPrnEmailOther.SuspendLayout();
            sCPrn.SuspendLayout();
            cspPrnLeft.SuspendLayout();
            cspPrnRight.SuspendLayout();

            groupBox1.Size = new Size(tabPrn.Width -20, tabPrn.Height - 20);
            groupBox1.Location = new Point(10, 5);
            chkPrnAll = new RadioButton();
            bc.setControlRadioBox(ref chkPrnAll, fEdit, "พิมพ์ ทั้งหมด", "chkPrnAll", x, y1);

            chkPrnCri = new RadioButton();
            Size size2 = bc.MeasureString(chkPrnCri);
            //chkPrnCri.Location = new Point(x + size2.Width + 10, y1);
            bc.setControlRadioBox(ref chkPrnCri, fEdit, "พิมพ์ ตามเงื่อนไข", "chkPrnCri", x + chkPrnAll.Width + 10, y1);

            chkPrnLab = new RadioButton();
            size2 = bc.MeasureString(chkPrnCri);
            bc.setControlRadioBox(ref chkPrnLab, fEdit, "lab ตามเงื่อนไข", "chkPrnLab", chkPrnCri.Location.X + size2.Width + 120, y1);
            //chkPrnLab.CheckedChanged += ChkPrnLab_CheckedChanged;
            chkPrnLab.Click += ChkPrnLab_Click;

            txtPrnCri = new C1TextBox();
            txtPrnCri.Text = "";
            txtPrnCri.Name = "chkPrchkPrnCrinAll";
            txtPrnCri.Font = fEdit;
            Size size3 = bc.MeasureString(chkPrnCri);
            txtPrnCri.Location = new Point(chkPrnLab.Location.X + size3.Width + 40, y1);

            chkPrnSSO = new RadioButton();
            size2 = bc.MeasureString(chkPrnLab);
            bc.setControlRadioBox(ref chkPrnSSO, fEdit, "export ปกส", "chkPrnSSO", txtPrnCri.Location.X + txtPrnCri.Width + 20, y1);
            chkPrnSSO.Click += ChkPrnSSO_Click;

            chkPrnSSOall = new C1CheckBox();
            size2 = bc.MeasureString(chkPrnSSO);
            bc.setControlC1CheckBox(ref chkPrnSSOall, fEdit, "all", "chkPrnSSOall", chkPrnSSO.Location.X + size2.Width + 20, y1);
            chkPrnSSOall.CheckedChanged += ChkPrnSSOall_CheckedChanged;

            btnSearch = new C1Button();
            bc.setControlC1Button(ref btnSearch, fEdit, "...", "btnSearch", chkPrnSSOall.Location.X + chkPrnSSOall.Width + 20, y1 - 10);
            btnSearch.Width = 30;
            btnSearch.Click += BtnSearch_Click;

            btnPrn = new C1Button();
            bc.setControlC1Button(ref btnPrn, fEdit, "Print", "btnPrn", btnSearch.Location.X + btnSearch.Width + 40, y1 - 10);
            btnPrn.Click += BtnPrn_Click;

            chkPrnEmail = new RadioButton();
            size2 = bc.MeasureString(chkPrnLab);
            bc.setControlRadioBox(ref chkPrnEmail, fEdit, "Email ", "chkPrnEmail", btnPrn.Location.X + btnPrn.Width + 20, y1);
            //chkPrnEmail.CheckedChanged += ChkPrnEmail_CheckedChanged;
            chkPrnEmail.Click += ChkPrnEmail_Click;

            label11 = new Label();
            label11.Font = this.fEdit;
            int y2 = y1 + 25;
            label11.Location = new Point(this.chkPrnCri.Location.X, y2);
            label11.Text = "ตัวอย่าง 1-30 หรือ 1,2,3,4,5,6,7";
            Size size4 = this.bc.MeasureString((Control)label11);
            label11.Size = size4;
            labe2 = new Label();
            labe2.Font = this.fEdit;
            y2 = y1 + 25;
            labe2.Location = new Point(this.chkPrnLab.Location.X, y2);
            labe2.Text = "ตัวอย่าง SE161,SE165";
            size4 = this.bc.MeasureString((Control)labe2);
            labe2.Size = size4;
            lbPrnDateStart = new Label();
            txtPrnDateStart = new C1DateEdit();
            lbPrnDateEnd = new Label();
            txtPrnDateEnd = new C1DateEdit();

            bc.setControlLabel(ref lbPrnDateStart, fEdit, "วันที่เริ่มต้น :", "lbPrnDateStart", labe2.Location.X + size4.Width + 20, labe2.Location.Y);
            size4 = this.bc.MeasureString((Control)lbPrnDateStart);
            bc.setControlC1DateTimeEdit(ref txtPrnDateStart, "txtPrnDateStart", lbPrnDateStart.Location.X + size4.Width + 5, labe2.Location.Y);
            bc.setControlLabel(ref lbPrnDateEnd, fEdit, "วันที่สิ้นสุด :", "lbPrnDateEnd", txtPrnDateStart.Location.X + txtPrnDateStart.Width + 20, labe2.Location.Y);
            size4 = this.bc.MeasureString((Control)lbPrnDateEnd);
            bc.setControlC1DateTimeEdit(ref txtPrnDateEnd, "txtPrnDateEnd", lbPrnDateEnd.Location.X + size4.Width + 5, labe2.Location.Y);
            txtPrnDateStart.Value = System.DateTime.Now;
            txtPrnDateEnd.Value = System.DateTime.Now;

            lbtxtPrnEmailTo = new Label();
            bc.setControlLabel(ref lbtxtPrnEmailTo, fEdit, "TO :", "lbtxtPrnEmailTo", x, label11.Location.Y);
            txtPrnEmailTo = new C1TextBox();
            size4 = this.bc.MeasureString(lbtxtPrnEmailTo);
            bc.setControlC1TextBox(ref txtPrnEmailTo, fEdit, "txtPrnEmailTo", 250, col2, lbtxtPrnEmailTo.Location.Y);

            lbtxtPrnEmailBody = new Label();
            bc.setControlLabel(ref lbtxtPrnEmailBody, fEdit, "Body :", "lbtxtPrnEmailBody", col3, label11.Location.Y);
            txtPrnEmailBody = new C1TextBox();
            size4 = this.bc.MeasureString(lbtxtPrnEmailTo);
            bc.setControlC1TextBox(ref txtPrnEmailBody, fEdit, "txtPrnEmailBody", 450, col4, lbtxtPrnEmailTo.Location.Y);
            txtPrnEmailBody.Multiline = true;
            txtPrnEmailBody.Height = 70;

            
            
            //pnPrnEmail.BackColor = Color.Red;
            


            //gapLine += 60;
            gapY += gapLine;
            lbDocGrp = new Label();
            bc.setControlLabel(ref lbDocGrp, fEdit, "กลุ่มเอกสารหลัก : ", "lbDocGrp", gapX, gapY);
            cboDocGrp = new C1ComboBox();
            cboDocGrp.Font = fEdit;
            cboDocGrp.Name = "cboDocGrp";
            size = bc.MeasureString(lbDocGrp);
            cboDocGrp.Location = new System.Drawing.Point(lbDocGrp.Location.X + size.Width + 5, lbDocGrp.Location.Y);
            bc.bcDB.dgsDB.setCboDgs(cboDocGrp, "");

            lbDocSubGrp = new Label();
            bc.setControlLabel(ref lbDocSubGrp, fEdit, "กลุ่มเอกสาร : ", "lbDocSubGrp", cboDocGrp.Location.X + cboDocGrp.Width + 20, lbDocGrp.Location.Y);
            cboDocSubGrp = new C1ComboBox();
            cboDocSubGrp.Font = fEdit;
            cboDocSubGrp.Name = "cboDocSubGrp";
            size = bc.MeasureString(lbDocSubGrp);
            cboDocSubGrp.Location = new System.Drawing.Point(lbDocSubGrp.Location.X + size.Width + 5, lbDocGrp.Location.Y);

            lbtxtPrnEmailSubject = new Label();
            bc.setControlLabel(ref lbtxtPrnEmailSubject, fEdit, "Subject :", "lbtxtPrnEmailSubject", x, gapY);
            txtPrnEmailSubject = new C1TextBox();
            size4 = this.bc.MeasureString(lbtxtPrnEmailSubject);
            bc.setControlC1TextBox(ref txtPrnEmailSubject, fEdit, "txtPrnEmailSubject", 500, col2, lbtxtPrnEmailSubject.Location.Y);

            btnDocOk = new C1Button();
            btnDocOk.Name = "btnDocOk";
            btnDocOk.Text = "...";
            btnDocOk.Font = this.fEdit;
            size3 = new Size(40, 30);
            btnDocOk.Size = size3;
            btnDocOk.Location = new Point(cboDocSubGrp.Location.X + cboDocSubGrp.Width + 20, lbDocGrp.Location.Y);
            btnDocOk.Click += BtnDocOk_Click;
            btnDocExport = new C1Button();
            btnDocExport.Name = "btnDocExport";
            btnDocExport.Text = "export";
            btnDocExport.Font = this.fEdit;
            size3 = new Size(70, 30);
            btnDocExport.Size = size3;
            btnDocExport.Location = new Point(btnDocOk.Location.X + btnDocOk.Width + 20, lbDocGrp.Location.Y);
            btnDocExport.Click += BtnDocExport_Click;

            lbDocAn = new Label();
            bc.setControlLabel(ref lbDocAn, fEdit, "doc an ", "lbDocAn", btnDocExport.Location.X + btnDocExport.Width + 20, lbDocGrp.Location.Y);

            theme1.SetTheme(btnDocOk, this.bc.iniC.themeApplication);
            theme1.SetTheme(btnDocExport, this.bc.iniC.themeApplication);

            int y3 = 20;

            
            pnPrnEmailGrfPrn.Dock = DockStyle.Fill;
            //pnPrnEmailGrfPrn.BackColor = Color.Red;
            //pnPrn.Height = groupBox1.Height - 120;
            

            grfPrn = new C1FlexGrid();
            grfPrn.Font = this.fEdit;
            //grfPrn.Dock = DockStyle.Bottom;
            grfPrn.Dock = DockStyle.Fill;
            grfPrn.Location = new Point(0, 0);
            grfPrn.Rows.Count = 1;
            //grfPrn.Height = groupBox1.Height - 120;
            grfPrn.MouseClick += GrfPrn_MouseClick;
            grfPrn.AfterRowColChange += GrfPrn_AfterRowColChange;
            pnPrnEmailGrfPrn.Controls.Add(grfPrn);

            
            tcPrnEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            tcPrnEmail.Location = new System.Drawing.Point(0, 266);
            tcPrnEmail.Name = "tcPrnEmail";
            tcPrnEmail.Size = new System.Drawing.Size(669, 200);
            tcPrnEmail.TabIndex = 0;
            tcPrnEmail.TabsSpacing = 5;
            tcPrnEmail.Font = fEdit;

            
            tabPrnEmailDrug.Location = new System.Drawing.Point(1, 24);
            tabPrnEmailDrug.Size = new System.Drawing.Size(667, 175);
            tabPrnEmailDrug.TabIndex = 1;
            tabPrnEmailDrug.Text = "รายการยา";
            tabPrnEmailDrug.Name = "tabPrnEmailDrug";
            
            tabPrnEmailLab.Location = new System.Drawing.Point(1, 24);
            tabPrnEmailLab.Size = new System.Drawing.Size(667, 175);
            tabPrnEmailLab.TabIndex = 2;
            tabPrnEmailLab.Text = "ผล LAB";
            tabPrnEmailLab.Name = "tabPrnEmailLab";

            tabPrnEmailXray.Location = new System.Drawing.Point(1, 24);
            tabPrnEmailXray.Size = new System.Drawing.Size(667, 175);
            tabPrnEmailXray.TabIndex = 3;
            tabPrnEmailXray.Text = "ผล Xray";
            tabPrnEmailXray.Name = "tabPrnEmailXray";
            
            tabPrnEmailSummary.Location = new System.Drawing.Point(1, 24);
            tabPrnEmailSummary.Size = new System.Drawing.Size(667, 175);
            tabPrnEmailSummary.TabIndex = 0;
            tabPrnEmailSummary.Text = "ใบงบสรุป";
            tabPrnEmailSummary.Name = "tabPrnEmailSummary";

            tabPrnEmailOther.Location = new System.Drawing.Point(1, 24);
            tabPrnEmailOther.Size = new System.Drawing.Size(667, 175);
            tabPrnEmailOther.TabIndex = 0;
            tabPrnEmailOther.Text = "Other";
            tabPrnEmailOther.Name = "tabPrnEmailOther";
            tcPrnEmail.Controls.Add(tabPrnEmailSummary);
            tcPrnEmail.Controls.Add(tabPrnEmailDrug);
            tcPrnEmail.Controls.Add(tabPrnEmailLab);
            tcPrnEmail.Controls.Add(tabPrnEmailXray);
            tcPrnEmail.Controls.Add(tabPrnEmailOther);

            sCPrn.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCPrn.Name = "sCPrn";
            sCPrn.Dock = System.Windows.Forms.DockStyle.Bottom;
            sCPrn.Height = groupBox1.Height - 120;
            sCPrn.Panels.Add(cspPrnLeft);
            sCPrn.Panels.Add(cspPrnRight);
            sCPrn.HeaderHeight = 20;

            cspPrnLeft.Collapsible = true;
            cspPrnLeft.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            cspPrnLeft.Location = new System.Drawing.Point(0, 21);
            cspPrnLeft.Name = "cspPrnLeft";
            cspPrnLeft.Controls.Add(pnPrnEmailGrfPrn);
            //scOrdLeft.HeaderHeight = 10;
            cspPrnRight.Collapsible = true;
            cspPrnRight.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            cspPrnRight.Location = new System.Drawing.Point(0, 21);
            cspPrnRight.Name = "cspPrnRight";
            //scOrdRight.HeaderHeight = 10;
            cspPrnRight.Controls.Add(tcPrnEmail);


            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(labe2);
            groupBox1.Controls.Add(lbPrnDateStart);
            groupBox1.Controls.Add(lbPrnDateEnd);
            groupBox1.Controls.Add(txtPrnDateStart);
            groupBox1.Controls.Add(txtPrnDateEnd);
            groupBox1.Controls.Add(btnPrn);
            groupBox1.Controls.Add(txtPrnCri);
            groupBox1.Controls.Add(chkPrnCri);
            groupBox1.Controls.Add(chkPrnLab);
            groupBox1.Controls.Add(chkPrnAll);
            groupBox1.Controls.Add(lbDocGrp);
            groupBox1.Controls.Add(cboDocGrp);
            groupBox1.Controls.Add(lbDocSubGrp);
            groupBox1.Controls.Add(cboDocSubGrp);
            //groupBox1.Controls.Add(grfPrn);
            groupBox1.Controls.Add(sCPrn);
            groupBox1.Controls.Add(btnDocOk);
            groupBox1.Controls.Add(btnDocExport);
            groupBox1.Controls.Add(lbDocAn);
            groupBox1.Controls.Add(chkPrnSSO);
            groupBox1.Controls.Add(chkPrnSSOall);
            groupBox1.Controls.Add(btnSearch);
            groupBox1.Controls.Add(chkPrnEmail);
            groupBox1.Controls.Add(lbtxtPrnEmailSubject);
            groupBox1.Controls.Add(lbtxtPrnEmailTo);
            groupBox1.Controls.Add(txtPrnEmailSubject);
            groupBox1.Controls.Add(txtPrnEmailTo);
            groupBox1.Controls.Add(lbtxtPrnEmailBody);
            groupBox1.Controls.Add(txtPrnEmailBody);
            
            
            panel.Controls.Add(groupBox1);
            tabPrn.Controls.Add(panel);
            theme1.SetTheme(btnPrn, this.bc.iniC.themeApp);
            theme1.SetTheme(panel, this.bc.iniC.themeApp);
            theme1.SetTheme(groupBox1, this.bc.iniC.themeApp);
            theme1.SetTheme(chkPrnSSOall, this.bc.iniC.themeApp);
            theme1.SetTheme(btnSearch, this.bc.iniC.themeApp);
            //theme1.SetTheme(chkPrnEmailDrug, this.bc.iniC.themeApp);
            //theme1.SetTheme(chkPrnEmailDrug, this.bc.iniC.themeApp);
            //theme1.SetTheme(chkPrnEmailDrug, this.bc.iniC.themeApp);

            //new LogWriter("e", "initTabPrn 02");
            lbtxtPrnEmailTo.Visible = false;
            lbtxtPrnEmailSubject.Visible = false;
            txtPrnEmailTo.Visible = false;
            txtPrnEmailSubject.Visible = false;
            lbtxtPrnEmailBody.Visible = false;
            txtPrnEmailBody.Visible = false;
            //pnPrnEmail.Visible = false;

            
            groupBox1.ResumeLayout(false);
            pnPrnEmailGrfPrn.ResumeLayout(false);
            tcPrnEmail.ResumeLayout(false);
            tabPrnEmailDrug.ResumeLayout(false);
            tabPrnEmailLab.ResumeLayout(false);
            tabPrnEmailXray.ResumeLayout(false);
            tabPrnEmailSummary.ResumeLayout(false);
            sCPrn.ResumeLayout(false);
            cspPrnLeft.ResumeLayout(false);
            cspPrnRight.ResumeLayout(false);

            
            groupBox1.PerformLayout();
            pnPrnEmailGrfPrn.PerformLayout();
            tcPrnEmail.PerformLayout();
            tabPrnEmailDrug.PerformLayout();
            tabPrnEmailLab.PerformLayout();
            tabPrnEmailXray.PerformLayout();
            tabPrnEmailSummary.PerformLayout();
            sCPrn.PerformLayout();
            cspPrnLeft.PerformLayout();
            cspPrnRight.PerformLayout();
        }

        private void ChkPrnLab_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtPrnDateStart.Visible = true;
            txtPrnDateEnd.Visible = true;
            lbPrnDateEnd.Visible = true;
            lbPrnDateStart.Visible = true;
            labe2.Visible = true;
            lbDocGrp.Visible = true;
            cboDocGrp.Visible = true;
            lbDocSubGrp.Visible = true;
            cboDocSubGrp.Visible = true;
            btnDocOk.Visible = true;
            btnDocExport.Visible = true;
            lbDocAn.Visible = true;
            btnPrn.Visible = true;

            lbtxtPrnEmailTo.Visible = false;
            lbtxtPrnEmailSubject.Visible = false;
            //pnPrnEmail.Visible = flag;
            txtPrnEmailTo.Visible = false;
            txtPrnEmailSubject.Visible = false;
            txtPrnEmailBody.Visible = false;
            lbtxtPrnEmailBody.Visible = false;
            chkPrnSSOall.Visible = false;
            //btnPrn.Visible = false;
            btnSearch.Visible = false;
        }

        private void ChkPrnSSO_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            label11.Visible = false;
            labe2.Visible = false;
            lbPrnDateStart.Visible = true;
            txtPrnDateStart.Visible = true;
            lbPrnDateEnd.Visible = true;
            txtPrnDateEnd.Visible = true;
            lbDocGrp.Visible = false;
            cboDocGrp.Visible = false;
            lbDocSubGrp.Visible = false;
            cboDocSubGrp.Visible = false;
            btnDocOk.Visible = false;
            btnDocExport.Visible = false;
            lbDocAn.Visible = false;
            btnPrn.Visible = false;
            btnSearch.Visible = true;
            chkPrnSSOall.Visible = true;
            //lbtxtPrnEmailTo.Visible = flag;
            //lbtxtPrnEmailSubject.Visible = flag;
            //txtPrnEmailTo.Visible = flag;
            //txtPrnEmailSubject.Visible = flag;

            lbtxtPrnEmailTo.Visible = false;
            lbtxtPrnEmailSubject.Visible = false;
            //pnPrnEmail.Visible = flag;
            txtPrnEmailTo.Visible = false;
            txtPrnEmailSubject.Visible = false;
            txtPrnEmailBody.Visible = false;
            lbtxtPrnEmailBody.Visible = false;
            btnPrn.Visible = true;
        }

        private void ChkPrnEmail_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            cspPrnLeft.Width = 300;

            setShowTabPrnLine2(chkPrnEmail.Checked);
            setGrfPrnEmail();
            if (fvPrnEmailSummary != null) fvPrnEmailSummary.DocumentSource = null;
            if (fvPrnEmailDrug != null) fvPrnEmailDrug.DocumentSource = null;
            if (fvPrnEmailLab != null) fvPrnEmailLab.DocumentSource = null;
            if (fvPrnEmailXray != null) fvPrnEmailXray.DocumentSource = null;
        }

        private void GrfPrn_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfPrn.Row <= 0) return;
            if (pageLoadGrfPrn) return;
            if (chkPrnSSO.Checked && (grfPrn.Col == colPrnSSOchk))
            {
                if (grfPrn[grfPrn.Row, colPrnSSOchk] == null) return;
                if (grfPrn[e.NewRange.BottomRow, colPrnSSOchk].ToString().Equals("False"))
                {
                    grfPrn.Rows[e.NewRange.BottomRow].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                }
                else if (grfPrn[e.NewRange.BottomRow, colPrnSSOchk].ToString().Equals("True"))
                {
                    grfPrn.Rows[e.NewRange.BottomRow].StyleNew.BackColor = Color.White; 
                }
            }
            else if (chkPrnEmail.Checked)
            {
                if (fvPrnEmailSummary !=null) fvPrnEmailSummary.DocumentSource = null;
                if (fvPrnEmailDrug != null) fvPrnEmailDrug.DocumentSource = null;
                if (fvPrnEmailLab != null) fvPrnEmailLab.DocumentSource = null;
                if (fvPrnEmailXray != null) fvPrnEmailXray.DocumentSource = null;
                
                setActiveTabPrnEmail();
            }
        }
        private void setActiveTabPrnEmail()
        {
            showLbLoading();
            if (!flagTabPrnEmailSummary)
            {

                bc.setControlC1FlexViewer(ref fvPrnEmailSummary, "fvPrnEmailSummary");
                tabPrnEmailSummary.Controls.Add(fvPrnEmailSummary);
                bc.setControlC1FlexViewer(ref fvPrnEmailDrug, "fvPrnEmailDrug");
                tabPrnEmailDrug.Controls.Add(fvPrnEmailDrug);
                bc.setControlC1FlexViewer(ref fvPrnEmailLab, "fvPrnEmailLab");
                tabPrnEmailDrug.Controls.Add(fvPrnEmailLab);
                bc.setControlC1FlexViewer(ref fvPrnEmailXray, "fvPrnEmailXray");
                tabPrnEmailXray.Controls.Add(fvPrnEmailXray);
                flagTabPrnEmailSummary = true;
                initTabPrnEmailOther();
            }
            setTabPrnEmailSummary1();
            setTabPrnEmailOrder();
            setTabPrnEmailLab();
            setTabPrnEmailXray();
            if (tcPrnEmail.SelectedTab == tabPrnEmailSummary)
            {
                
                
                
            }
            else if (tcPrnEmail.SelectedTab == tabPrnEmailDrug)
            {
                if (!flagTabPrnEmailDrug)
                {
                    //bc.setControlC1FlexViewer(ref fvPrnEmailDrug, "fvPrnEmailDrug");
                    //tabPrnEmailDrug.Controls.Add(fvPrnEmailDrug);
                    flagTabPrnEmailDrug = true;
                }
                //setTabPrnEmailSummary1();
                //setTabPrnEmailOrder();
                //setTabPrnEmailLab();
                //setTabPrnEmailXray();
            }
            else if (tcPrnEmail.SelectedTab == tabPrnEmailLab)
            {
                if (!flagTabPrnEmailLab)
                {
                    //bc.setControlC1FlexViewer(ref fvPrnEmailLab, "fvPrnEmailLab");
                    //tabPrnEmailLab.Controls.Add(fvPrnEmailLab);
                    flagTabPrnEmailLab = true;
                }
                //setTabPrnEmailSummary1();
                //setTabPrnEmailOrder();
                //setTabPrnEmailLab();
                //setTabPrnEmailXray();
            }
            else if (tcPrnEmail.SelectedTab == tabPrnEmailXray)
            {
                if (!flagTabPrnEmailXray)
                {
                    //bc.setControlC1FlexViewer(ref fvPrnEmailXray, "fvPrnEmailXray");
                    //tabPrnEmailXray.Controls.Add(fvPrnEmailXray);
                    flagTabPrnEmailXray = true;
                }
                //setTabPrnEmailSummary1();
                //setTabPrnEmailOrder();
                //setTabPrnEmailLab();
                //setTabPrnEmailXray();
            }
            else if (tcPrnEmail.SelectedTab == tabPrnEmailOther)
            {
                if (!flagTabPrnEmailOther)
                {
                    //bc.setControlC1FlexViewer(ref fvPrnEmailOther, "fvPrnEmailOther");
                    //tabPrnEmailOther.Controls.Add(fvPrnEmailOther);
                    flagTabPrnEmailOther = true;
                    initTabPrnEmailOther();
                }
            }
            hideLbLoading();
        }
        private void initTabPrnEmailOther()
        {
            int gapLine = 16, gapX = 20, gapY = 20, xCol2 = 130, xCol1 = 20, xCol3 = 300, xCol4 = 390, xCol5 = 1030;
            //pnPrnEmail = new Panel();
            //
            
            //pnPrnEmail.Dock = DockStyle.Fill;
            //pnPrnEmail.Location = new Point(txtPrnEmailBody.Location.X + txtPrnEmailBody.Width, chkPrnEmail.Location.Y - 5);
            //pnPrnEmail.SuspendLayout();
            //pnPrnEmail.BackColor = Color.Red;

            btnPrnEmailImgOpen = new C1Button();
            bc.setControlC1Button(ref btnPrnEmailImgOpen, fEdit, "1. ดึงรูป Scan", "btnPrnEmailImpOpen", 5, 10);
            btnPrnEmailImgOpen.Size = new Size(130, 40);
            btnPrnEmailImgOpen.Click += BtnPrnEmailImgOpen_Click;

            btnPrnEmailImgBrow = new C1Button();
            bc.setControlC1Button(ref btnPrnEmailImgBrow, fEdit, "2. รูป เพิ่มเติม", "btnPrnEmailImgBrow", btnPrnEmailImgOpen.Location.X + btnPrnEmailImgOpen.Size.Width + 20, 10);
            btnPrnEmailImgBrow.Image = Resources.folder;
            btnPrnEmailImgBrow.Size = new Size(130, 40);
            btnPrnEmailImgBrow.Click += BtnPrnEmailImgBrow_Click;
            
            btnPrnEmailImgSend = new C1Button();
            bc.setControlC1Button(ref btnPrnEmailImgSend, fEdit, "ส่ง Email", "btnPrnEmailImgSend", btnPrnEmailImgBrow.Location.X + btnPrnEmailImgBrow.Size.Width + 20, 10);
            btnPrnEmailImgSend.Size = new Size(130, 40);
            btnPrnEmailImgSend.Image = Resources.Email_icon_24;
            btnPrnEmailImgSend.Click += BtnPrnEmailImgSend_Click;

            chkPrnEmailSummary = new C1CheckBox();
            Size size2 = bc.MeasureString(chkPrnLab);
            bc.setControlC1CheckBox(ref chkPrnEmailSummary, fEdit, "งบสรุป ", "chkPrnEmailSummary", btnPrnEmailImgSend.Location.X+ btnPrnEmailImgSend.Width+20, 5);
            chkPrnEmailDrug = new C1CheckBox();
            size2 = bc.MeasureString(chkPrnEmailSummary);
            bc.setControlC1CheckBox(ref chkPrnEmailDrug, fEdit, "ยา ", "chkPrnEmailDrug", chkPrnEmailSummary.Location.X + size2.Width + 30, chkPrnEmailSummary.Location.Y);
            chkPrnEmailLab = new C1CheckBox();
            size2 = bc.MeasureString(chkPrnEmailDrug);
            bc.setControlC1CheckBox(ref chkPrnEmailLab, fEdit, "LAB ", "chkPrnEmailLab", chkPrnEmailDrug.Location.X + size2.Width + 30, chkPrnEmailSummary.Location.Y);
            chkPrnEmailXray = new C1CheckBox();
            size2 = bc.MeasureString(chkPrnEmailLab);
            bc.setControlC1CheckBox(ref chkPrnEmailXray, fEdit, "Xray ", "chkPrnEmailXray", chkPrnEmailLab.Location.X + size2.Width + 30, chkPrnEmailSummary.Location.Y);
            
            gapY += gapLine;
            gapY += gapLine;
            //gapY += gapLine;
            grfPrnEmailImg = new C1FlexGrid();
            grfPrnEmailImg.Font = this.fEdit;
            //grfPrn.Dock = DockStyle.Bottom;
            grfPrnEmailImg.Dock = DockStyle.Fill;
            grfPrnEmailImg.Location = new Point(5, gapY);
            grfPrnEmailImg.Size = new Size(tabPrnEmailOther.Size.Width - 10, tabPrnEmailOther.Size.Height - gapY);
            grfPrnEmailImg.Rows.Count = 1;

            grfPrnEmailImg.Rows.Count = 1;
            grfPrnEmailImg.Cols.Count = 3;
            grfPrnEmailImg.Cols[colPrnEmailImgimage].Caption = "";
            grfPrnEmailImg.Cols[colPrnEmailImgFilename].Caption = "";
            grfPrnEmailImg.Cols[colPrnEmailImgimage].Width = 100;
            grfPrnEmailImg.Cols[colPrnEmailImgFilename].Width = 60;

            Column colpic1 = grfPrnEmailImg.Cols[colPrnEmailImgimage];
            colpic1.DataType = typeof(Image);

            grfPrnEmailImg.Cols[colPrnEmailImgimage].AllowEditing = false;
            grfPrnEmailImg.Cols[colPrnEmailImgFilename].AllowEditing = false;
            grfPrnEmailImg.Cols[colPrnEmailImgFilename].Visible = false;
            //grfPrn.Height = groupBox1.Height - 120;

            tabPrnEmailOther.Controls.Add(btnPrnEmailImgBrow);
            tabPrnEmailOther.Controls.Add(btnPrnEmailImgOpen);
            tabPrnEmailOther.Controls.Add(btnPrnEmailImgSend);
            tabPrnEmailOther.Controls.Add(grfPrnEmailImg);

            tabPrnEmailOther.Controls.Add(chkPrnEmailSummary);
            tabPrnEmailOther.Controls.Add(chkPrnEmailDrug);
            tabPrnEmailOther.Controls.Add(chkPrnEmailLab);
            tabPrnEmailOther.Controls.Add(chkPrnEmailXray);
            //tabPrnEmailOther.Controls.Add(pnPrnEmail);

            theme1.SetTheme(btnPrnEmailImgBrow, this.bc.iniC.themeApp);
            theme1.SetTheme(btnPrnEmailImgOpen, this.bc.iniC.themeApp);
            theme1.SetTheme(btnPrnEmailImgSend, this.bc.iniC.themeApp);
            //theme1.SetTheme(chkPrnEmailSummary, this.bc.iniC.themeApp);
            //theme1.SetTheme(chkPrnEmailDrug, this.bc.iniC.themeApp);
            //theme1.SetTheme(chkPrnEmailLab, this.bc.iniC.themeApp);
            //theme1.SetTheme(chkPrnEmailXray, this.bc.iniC.themeApp);

            //pnPrnEmail.ResumeLayout(false);
            //pnPrnEmail.PerformLayout();
        }

        private void BtnPrnEmailImgSend_Click(object sender, EventArgs e)
        {
            showLbLoading();
            long emailsize = 0;
            String pathFolder = "", datetick = "";
            datetick = DateTime.Now.Ticks.ToString();
            pathFolder = bc.iniC.medicalrecordexportpath + "\\" + datetick;
            if (Directory.Exists(pathFolder))
            {
                Directory.Delete(pathFolder, true);
                Thread.Sleep(100);
                Application.DoEvents();
            }
            Directory.CreateDirectory(pathFolder);
            //throw new NotImplementedException();
            lbLoading.Text = "กรุณารอซักครู่ config Email";
            Application.DoEvents();
            C1PdfDocument pdfdoc = new C1PdfDocument();
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(bc.iniC.email_form);
            mail.To.Add(txtPrnEmailTo.Text);
            mail.Subject = txtPrnEmailSubject.Text;
            mail.Body = txtPrnEmailBody.Text;
            mail.IsBodyHtml = true;
            SmtpClient SmtpServer;
            SmtpServer = new SmtpClient("smtp.gmail.com");
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(txtPrnEmailBody.Text, null, "text/html");
            mail.AlternateViews.Add(htmlView);
            List<LinkedResource> theEmailImage1 = new List<LinkedResource>();
            foreach (LinkedResource linkimg in theEmailImage1)
            {
                htmlView.LinkedResources.Add(linkimg);
            }
            SmtpServer.Port = int.Parse(bc.iniC.email_port);
            SmtpServer.Credentials = new System.Net.NetworkCredential(bc.iniC.email_auth_user, bc.iniC.email_auth_pass);
            //SmtpServer.UseDefaultCredentials = true;
            SmtpServer.EnableSsl = Boolean.Parse(bc.iniC.email_ssl);
            lbLoading.Size = new Size(400, 60);
            lbLoading.Text = "กรุณารอซักครู่ attach file ...";
            Application.DoEvents();
            
            if (File.Exists(emailSummary))
            {
                mail.Attachments.Add(new System.Net.Mail.Attachment(emailSummary));
                emailsize = (new FileInfo(emailSummary)).Length;
                //pds.LoadFromFile(emailSummary);
            }
            if (File.Exists(emailDrug))
            {
                mail.Attachments.Add(new System.Net.Mail.Attachment(emailDrug));
                emailsize += (new FileInfo(emailDrug)).Length;
            }
            if (File.Exists(emailLab))
            {
                mail.Attachments.Add(new System.Net.Mail.Attachment(emailLab));
                emailsize += (new FileInfo(emailLab)).Length;
            }
            if (File.Exists(emailXray))
            {
                mail.Attachments.Add(new System.Net.Mail.Attachment(emailXray));
                emailsize =+ (new FileInfo(emailXray)).Length;
            }
            

            foreach (Row row in grfPrnEmailImg.Rows)
            {
                String filename = "";
                filename = row[colPrnEmailImgFilename].ToString();
                if (File.Exists(filename))
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(filename);
                    mail.Attachments.Add(attachment);
                    emailsize = +(new FileInfo(filename)).Length;
                }
            }

            if (emailsize >= 1024) emailsize = emailsize / 1024;     // kb
            if (emailsize >= 1024) emailsize = emailsize / 1024;     // mb

            lbLoading.Text = "กรุณารอซักครู่ send email ... emailsize";
            lbLoading.Size = new Size(300, 60);
            SmtpServer.Send(mail);
            hideLbLoading();
        }

        private void BtnPrnEmailImgOpen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String filename = "";
            DirectoryInfo d = new DirectoryInfo(bc.iniC.pathImageScan);
            FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files
            foreach (FileInfo file in Files)
            {
                if (ImageExtensions.Contains(Path.GetExtension(file.FullName).ToUpperInvariant()))
                {
                    Image loadedImage, resizedImage;
                    int originalWidth = 0, newWidth=0;
                    loadedImage = Image.FromFile(file.FullName);
                    originalWidth = loadedImage.Width;
                    newWidth = 600;
                    
                    Row row = grfPrnEmailImg.Rows.Add();
                    resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
                    row[colPrnEmailImgimage] = resizedImage;
                    row[colPrnEmailImgFilename] = file.FullName;
                }
            }
            grfPrnEmailImg.AutoSizeCols();
            grfPrnEmailImg.AutoSizeRows();
        }

        private void BtnPrnEmailImgBrow_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfPrnEmailImg();
        }
        private void setGrfPrnEmailImg()
        {

        }
        private void setTabPrnEmailSummary2()
        {
            int gapLine = 16, gapX = 20, gapY = 60, xCol2 = 130, xCol1 = 20, xCol3 = 300, xCol4 = 390, xCol5 = 1030;
            Size size = new Size();
            String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "", anyr = "", vn1 = "", pathFolder = "", datetick = "", filename = "";
            DataTable dt = new DataTable();

            if (chkIPD.Checked)
            {
                an = grfIPD[grfIPD.Row, colIPDAn] != null ? grfIPD[grfIPD.Row, colIPDAn].ToString() : "";
                anDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                anyr = grfIPD[grfIPD.Row, colIPDAnYr] != null ? grfIPD[grfIPD.Row, colIPDAnYr].ToString() : "";
            }
            else
            {
                preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                vsDate = grfPrn[grfPrn.Row, colPrnEmailDocDat] != null ? grfPrn[grfPrn.Row, colPrnEmailDocDat].ToString() : "";
                vn = grfOPD[grfOPD.Row, colVsVn] != null ? grfOPD[grfOPD.Row, colVsVn].ToString() : "";
            }
            vn1 = vn.IndexOf("(") > 0 ? vn.Substring(0, vn.IndexOf("(")) : "";
            dt = bc.bcDB.tem01DB.selectSummaryOPD(txtHn.Text.Trim(), ptt.hnyr, vn1, vsDate);
            if (dt.Rows.Count <= 0) return;

            iTextSharp.text.pdf.BaseFont bfR, bfR1;
            iTextSharp.text.BaseColor clrBlack = new iTextSharp.text.BaseColor(0, 0, 0);

        }
        private void setTabPrnEmailSummary1()
        {
            int gapLine = 20, gapX = 40, gapY = 20, xCol2 = 130, xCol1 = 20, xCol3 = 300, xCol4 = 390, xCol5 = 1030;
            Size size = new Size();

            String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", doccd = "", docno = "", anyr = "", vn1 = "", pathFolder = "", datetick = "", filename = "", docyr="",amt2 = "";
            
            DataTable dt = new DataTable();

            //if (chkIPD.Checked)
            //{
            //    an = grfIPD[grfIPD.Row, colIPDAn] != null ? grfIPD[grfIPD.Row, colIPDAn].ToString() : "";
            //    anDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
            //    anyr = grfIPD[grfIPD.Row, colIPDAnYr] != null ? grfIPD[grfIPD.Row, colIPDAnYr].ToString() : "";
            //    vn1 = vn.IndexOf("(") > 0 ? vn.Substring(0, vn.IndexOf("(")) : "";
            //    dt = bc.bcDB.tem01DB.selectSummaryIPD(txtHn.Text.Trim(), ptt.hnyr, an, anyr);
            //}
            //else
            //{
            //    preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
            //    vsDate = grfPrn[grfPrn.Row, colPrnEmailDocDat] != null ? grfPrn[grfPrn.Row, colPrnEmailDocDat].ToString() : "";
            //    vn = grfOPD[grfOPD.Row, colVsVn] != null ? grfOPD[grfOPD.Row, colVsVn].ToString() : "";
            //    vn1 = vn.IndexOf("(") > 0 ? vn.Substring(0, vn.IndexOf("(")) : "";
            //    dt = bc.bcDB.tem01DB.selectSummaryOPD(txtHn.Text.Trim(), ptt.hnyr, vn1, vsDate);
            //}
            statusOPD = grfPrn[grfPrn.Row, colPrnEmailStatusOPD] != null ? grfPrn[grfPrn.Row, colPrnEmailStatusOPD].ToString() : "";
            doccd = grfPrn[grfPrn.Row, colPrnEmailDocCD] != null ? grfPrn[grfPrn.Row, colPrnEmailDocCD].ToString() : "";
            docno = grfPrn[grfPrn.Row, colPrnEmailDocNo] != null ? grfPrn[grfPrn.Row, colPrnEmailDocNo].ToString() : "";
            docyr = grfPrn[grfPrn.Row, colPrnEmailDocYr] != null ? grfPrn[grfPrn.Row, colPrnEmailDocYr].ToString() : "";
            if (statusOPD.Length <= 0) return;
            if (statusOPD.Equals("FIO"))
            {
                preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                vsDate = grfPrn[grfPrn.Row, colPrnEmailDocDat] != null ? grfPrn[grfPrn.Row, colPrnEmailDocDat].ToString() : "";
                vn = grfOPD[grfOPD.Row, colVsVn] != null ? grfOPD[grfOPD.Row, colVsVn].ToString() : "";
                vn1 = vn.IndexOf("(") > 0 ? vn.Substring(0, vn.IndexOf("(")) : "";
                dt = bc.bcDB.tem01DB.selectSummaryOPD(txtHn.Text.Trim(), ptt.hnyr, vn1, vsDate);
            }
            else
            {
                an = grfPrn[grfPrn.Row, colPrnEmailAn] != null ? grfPrn[grfPrn.Row, colPrnEmailAn].ToString() : "";
                anyr = grfPrn[grfPrn.Row, colPrnEmailAnYr] != null ? grfPrn[grfPrn.Row, colPrnEmailAnYr].ToString() : "";
                
                dt = bc.bcDB.tem01DB.selectSummaryIPD(txtHn.Text.Trim(), ptt.hnyr, an, anyr, doccd, docno, docyr);
            }
            if (dt.Rows.Count <= 0) return;

            C1PdfDocument pdf = new C1PdfDocument();
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            StringFormat _sfRight, _sfRightCenter;
            //Font _fontTitle = new Font("Tahoma", 15, FontStyle.Bold);
            _sfRight = new StringFormat();
            _sfRight.Alignment = StringAlignment.Far;
            _sfRightCenter = new StringFormat();
            _sfRightCenter.Alignment = StringAlignment.Far;
            _sfRightCenter.LineAlignment = StringAlignment.Center;

            //RectangleF rc = GetPageRect(pdf);
            Font titleFont = new Font(bc.iniC.pdfFontName, 18, FontStyle.Bold);
            Font hdrFont = new Font(bc.iniC.pdfFontName, 14, FontStyle.Regular);
            Font hdrFontB = new Font(bc.iniC.pdfFontName, 16, FontStyle.Bold);
            Font ftrFont = new Font(bc.iniC.pdfFontName, 8);
            Font txtFont = new Font(bc.iniC.pdfFontName, 10, FontStyle.Regular);
            //pdf.Clear();
            pdf.FontType = FontTypeEnum.Embedded;
            
            newPagePDFSummaryBorder(pdf, dt, titleFont, hdrFont, ftrFont, txtFont, false);

            //get page rectangle, discount margins
            RectangleF rcPage = pdf.PageRectangle;
            rcPage = RectangleF.Empty;
            rcPage.Inflate(-72, -92);
            rcPage.Location = new PointF(rcPage.X, rcPage.Y + titleFont.SizeInPoints + 10);
            rcPage.Size = new SizeF(0, titleFont.SizeInPoints + 3);
            rcPage.Width = 110;

            ////loop through selected categories
            //int page = 0;

            ////add page break, update page counter
            ////if (page > 0) pdf.NewPage();
            //page++;
            //Image loadedImage;
            //loadedImage = Resources.LOGO_BW_tran;
            //float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
            //float newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

            //float widthFactor = 4.8F;
            //float heightFactor = 4.8F;

            //if (widthFactor > 1 | heightFactor > 1)
            //{
            //    if (widthFactor > heightFactor)
            //    {
            //        widthFactor = 1;
            //        newWidth = newWidth / widthFactor;
            //        newHeight = newHeight / widthFactor;
            //        //newWidth = newWidth / 1.2;
            //        //newHeight = newHeight / 1.2;
            //    }
            //    else
            //    {
            //        newWidth = newWidth / heightFactor;
            //        newHeight = newHeight / heightFactor;
            //    }
            //}

            //RectangleF recf = new RectangleF(15, 15, (int)newWidth, (int)newHeight);
            //pdf.DrawImage(loadedImage, recf);

            //rcPage.X = gapX+50;
            //rcPage.Y = 20;
            ////rcPage.Width = 500;
            //pdf.DrawString(bc.iniC.hostname, titleFont, Brushes.Black, rcPage);
            //rcPage.X = gapX + 180;
            //rcPage.Width = 500;
            //rcPage.Y = rcPage.Y + 7;
            //pdf.DrawString(bc.iniC.hostaddresst, ftrFont, Brushes.Black, rcPage);
            //gapY += gapLine;
            //rcPage.X = gapX+50;
            //rcPage.Y = gapY;
            //rcPage.Width = 500;
            //pdf.DrawString(bc.iniC.hostnamee , titleFont, Brushes.Black, rcPage);
            //rcPage.X = gapX + 230;
            //rcPage.Y = rcPage.Y + 7;
            //pdf.DrawString( bc.iniC.hostaddresse, ftrFont, Brushes.Black, rcPage);
            ////gapY += gapLine;
            ////rcPage.X = gapX+50;
            ////rcPage.Y = gapY;
            ////rcPage.Width = 100;
            ////pdf.DrawString("HN  "+txtHn.Text.Trim(), txtFont, Brushes.Black, rcPage);
            ////rcPage.X = gapX + 185;
            ////rcPage.Width = 310;
            ////pdf.DrawString("เลขที่/No.  ", txtFont, Brushes.Black, rcPage, _sfRight);
            //gapY += gapLine;
            ////rcPage.X = gapX + 50;
            ////rcPage.Y = gapY;
            ////rcPage.Width = 100;
            ////pdf.DrawString("HN  " + txtHn.Text.Trim(), txtFont, Brushes.Black, rcPage);

            //rcPage.X = gapX + 180;
            //rcPage.Y = gapY;
            //pdf.DrawString("เลขที่ประจำตัวผู้เสียภาษีอากร.  0105526004138", hdrFont, Brushes.Black, rcPage);
            //gapY += gapLine;
            //rcPage.X = gapX + 50;
            //rcPage.Y = gapY;
            //pdf.DrawString("HN  " + txtHn.Text.Trim(), txtFont, Brushes.Black, rcPage);
            //rcPage.X = gapX + 180;
            //rcPage.Y = gapY;

            //pdf.DrawString("TAX I.D. NO.  0105526004138", hdrFont, Brushes.Black, rcPage);
            //rcPage.X = gapX + 185;
            //rcPage.Width = 310;
            //pdf.DrawString("เลขที่/No.  ", txtFont, Brushes.Black, rcPage, _sfRight);

            //gapY += gapLine+5;
            //rcPage.X = gapX+180;
            //rcPage.Y = gapY;
            //rcPage.Width = 270;
            //pdf.DrawString("ใบงบหน้าสรุปรายการค่ารักษาพยาบาล", titleFont, Brushes.Black, rcPage);
            //rcPage.X = gapX + 260;
            //pdf.DrawString("วันที่/Date  "+bc.datetoShow(dt.Rows[0]["doc_dat"].ToString()), txtFont, Brushes.Black, rcPage, _sfRight);


            gapY += gapLine;
            gapY += gapLine;
            //RectangleF rcHdr = new RectangleF();
            //rcHdr.Width = pdf.PageSize.Width - gapX -30;
            //rcHdr.Height = 500;
            //rcHdr.X = gapX;
            //rcHdr.Y = gapY;
            ////rcHdr.Location
            //pdf.DrawRectangle(Pens.Black, rcHdr);       // ตารางใหญ่

            //pdf.DrawLine(Pens.Black, rcHdr.X, rcHdr.Y + 50, rcHdr.X + rcHdr.Width, rcHdr.Y + 50);
            //pdf.DrawLine(Pens.Black, rcHdr.X, rcHdr.Y + 80, rcHdr.X + rcHdr.Width, rcHdr.Y + 80);
            //pdf.DrawLine(Pens.Black, rcHdr.X + rcHdr.Width - 90, rcHdr.Y + 50, rcHdr.X + rcHdr.Width - 90, rcHdr.Y + rcHdr.Height);     //เส้นตั้ง จำนวนเงิน
            //pdf.DrawLine(Pens.Black, rcHdr.X + 230, rcHdr.Y, rcHdr.X + 230, rcHdr.Y + 50);      //  เส้นตั้ง ชื่อ - นามสกุล
            //float xxx = rcHdr.X + rcHdr.Width - 90;
            //float yyy = rcHdr.Y + rcHdr.Height;

            //gapY += 510;
            //rcHdr.Width = rcHdr.X + rcHdr.Width - 230;
            //rcHdr.Height = 30;
            //rcHdr.X = gapX;
            //rcHdr.Y = gapY;
            //pdf.DrawRectangle(Pens.Black, rcHdr);       // ตารางจำนวนเงิน ตัวอักษร

            //rcHdr.Width =  500 - xxx + gapX + 42;
            //rcHdr.Height = 75;
            //rcHdr.X = xxx;
            //rcHdr.Y = yyy;
            //pdf.DrawRectangle(Pens.Black, rcHdr);       // ตารางรวมเงิน ด้านล่าง
            String space2 = "    ", space3 = "      ", space4 = "        ", space5 = "          ", space="";
            int rowline = 0, i=0;
            rowline = 205;
            foreach (DataRow drow in dt.Rows)
            {
                i++;
                if ((i % 24)==0)
                {
                    //newPagePDFBorder(pdf, true);
                    newPagePDFSummaryBorder(pdf, dt, titleFont, hdrFont, ftrFont, txtFont, true);
                    rowline = 205;
                    i = 0;
                }
                rowline += gapLine -3;
                rcPage.X = gapX + 10;
                rcPage.Y = rowline;
                rcPage.Width = 300;
                space = drow["MNC_def_lev"].ToString().Equals("1") ? "" : drow["MNC_def_lev"].ToString().Equals("2") ? space2 : drow["MNC_def_lev"].ToString().Equals("3") ? space3 : drow["MNC_def_lev"].ToString().Equals("4") ? space4 : drow["MNC_def_lev"].ToString().Equals("5") ? space5 : "";
                String txt = "";
                txt = space + drow["MNC_DEF_CD"].ToString() + " " + drow["MNC_DEF_DSC"].ToString();
                pdf.DrawString(txt, txtFont, Brushes.Black, rcPage);

                float amt = 0;
                if(float.TryParse(drow["MNC_amt"].ToString(), out amt) && (amt > 0))
                {
                    rcPage.X = 470;
                    rcPage.Width = 100;
                    pdf.DrawString(amt.ToString("#,###.00"), txtFont, Brushes.Black, rcPage, _sfRight);
                }
                //newPagePDFBorder(pdf);
            }
            decimal amt1 = 0;
            decimal.TryParse(dt.Rows[0]["mnc_sum_amt"].ToString(), out amt1);
            amt2 = bc.ThaiBahtText(amt1.ToString(), false);
            rcPage.X = 470;
            rcPage.Y = 690;
            rcPage.Width = 100;
            pdf.DrawString(amt1.ToString("#,###.00"), txtFont, Brushes.Black, rcPage, _sfRight);
            rcPage.X = gapX + 10;
            rcPage.Y = 650;
            rcPage.Width = 500;
            pdf.DrawString("(#"+amt2+"#)", txtFont, Brushes.Black, rcPage);

            datetick = DateTime.Now.Ticks.ToString();
            //pathFolder = "D:\\" + datetick;
            pathFolder = bc.iniC.medicalrecordexportpath + "\\" + datetick;
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
            filename = pathFolder + "\\" + txtHn.Text.TrimEnd() + "_summary.pdf";
            emailSummary = filename;
            pdf.Save(filename);
            pdf.Clear();
            pdf.Dispose();
            //Application.DoEvents();
            //Thread.Sleep(200);
            //MemoryStream stream = new MemoryStream();
            //pdf.Save(stream);
            //stream.Seek(0, SeekOrigin.Begin);
            
            pds.LoadFromFile(filename);
            //pds.LoadFromFile("d:\\ct_part5.pdf");
            //Application.DoEvents();
            //Thread.Sleep(200);
            fvPrnEmailSummary.DocumentSource = pds;
            
            //System.Diagnostics.Process.Start("explorer.exe", pathFolder);
        }
        private void newPagePDFSummaryBorder(C1PdfDocument pdf, DataTable dt, Font titleFont, Font hdrFont, Font ftrFont, Font txtFont, Boolean loopfor)
        {
            newPagePDFBorder(pdf, loopfor);
            newPagePDFHeaderPage(pdf, dt, titleFont, hdrFont, ftrFont, txtFont);
        }
        private void newPagePDFBorder(C1PdfDocument pdf, Boolean loopfor)
        {
            int gapLine = 20, gapX = 40, gapY = 135, xCol2 = 130, xCol1 = 20, xCol3 = 300, xCol4 = 390, xCol5 = 1030;
            Size size = new Size();
            //if(pdf.Pages.Count>1) pdf.NewPage();
            //if ((loopfor) && (pdf.CurrentPage != 0)) pdf.NewPage();
            if ((loopfor)) pdf.NewPage();
            RectangleF rcHdr = new RectangleF();
            rcHdr.Width = 542;
            rcHdr.Height = 500;
            rcHdr.X = gapX;
            rcHdr.Y = gapY;
            //rcHdr.Location
            pdf.DrawRectangle(Pens.Black, rcHdr);       // ตารางใหญ่

            pdf.DrawLine(Pens.Black, rcHdr.X, rcHdr.Y + 50, rcHdr.X + rcHdr.Width, rcHdr.Y + 50);
            pdf.DrawLine(Pens.Black, rcHdr.X, rcHdr.Y + 80, rcHdr.X + rcHdr.Width, rcHdr.Y + 80);
            pdf.DrawLine(Pens.Black, rcHdr.X + rcHdr.Width - 90, rcHdr.Y + 50, rcHdr.X + rcHdr.Width - 90, rcHdr.Y + rcHdr.Height);     //เส้นตั้ง จำนวนเงิน
            pdf.DrawLine(Pens.Black, rcHdr.X + 260, rcHdr.Y, rcHdr.X + 260, rcHdr.Y + 50);      //  เส้นตั้ง ชื่อ - นามสกุล
            float xxx = rcHdr.X + rcHdr.Width - 90;
            float yyy = rcHdr.Y + rcHdr.Height;

            gapY += 510;
            rcHdr.Width = rcHdr.X + rcHdr.Width - 230;
            rcHdr.Height = 30;
            rcHdr.X = gapX;
            rcHdr.Y = gapY;
            pdf.DrawRectangle(Pens.Black, rcHdr);       // ตารางจำนวนเงิน ตัวอักษร

            pdf.DrawLine(Pens.Black, rcHdr.X + 452, rcHdr.Y + 15, rcHdr.X + 542, rcHdr.Y + 15);       //เส้นแบ่ง รวมเงิน
            pdf.DrawLine(Pens.Black, rcHdr.X + 452, rcHdr.Y + 40, rcHdr.X + 542, rcHdr.Y + 40);       //เส้นแบ่ง รวมเงิน

            rcHdr.Width = 500 - xxx + gapX + 42;
            rcHdr.Height = 75;
            rcHdr.X = xxx;
            rcHdr.Y = yyy;
            pdf.DrawRectangle(Pens.Black, rcHdr);       // ตารางรวมเงิน ด้านล่าง


        }
        private void newPagePDFHeaderPage(C1PdfDocument pdf, DataTable dt, Font titleFont,Font hdrFont, Font ftrFont,Font txtFont)
        {
            int gapLine = 20, gapX = 40, gapY = 20, xCol2 = 130, xCol1 = 20, xCol3 = 300, xCol4 = 390, xCol5 = 1030;
            String amt2 = "", compname="", paidname="", paidcode="", disdate="", admdate="", sickness="";
            Size size = new Size();
            Visit vs = new Visit();

            StringFormat _sfRight, _sfRightCenter;
            //Font _fontTitle = new Font("Tahoma", 15, FontStyle.Bold);
            _sfRight = new StringFormat();
            _sfRight.Alignment = StringAlignment.Far;
            _sfRightCenter = new StringFormat();
            _sfRightCenter.Alignment = StringAlignment.Far;
            _sfRightCenter.LineAlignment = StringAlignment.Center;

            RectangleF rcPage = pdf.PageRectangle;
            rcPage = RectangleF.Empty;
            rcPage.Inflate(-72, -92);
            rcPage.Location = new PointF(rcPage.X, rcPage.Y + titleFont.SizeInPoints + 10);
            rcPage.Size = new SizeF(0, titleFont.SizeInPoints + 3);
            rcPage.Width = 110;

            vs = bc.bcDB.vsDB.selectVisit(txtHn.Text.Trim(), vsDate, preno);

            compname = dt.Rows[0]["mnc_com_name"].ToString();
            paidcode = dt.Rows[0]["mnc_fn_typ_cd"].ToString();
            paidname = dt.Rows[0]["mnc_fn_typ_dsc"].ToString();

            disdate = dt.Rows[0]["mnc_dis_dat"].ToString();
            admdate = dt.Rows[0]["mnc_adm_dat"].ToString();
            sickness = dt.Rows[0]["MNC_DIA_DSC"].ToString();

            //loop through selected categories
            int page = 0;

            //add page break, update page counter
            //if (page > 0) pdf.NewPage();
            page++;
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

            rcPage.X = gapX + 50;
            rcPage.Y = 20;
            //rcPage.Width = 500;
            pdf.DrawString(bc.iniC.hostname, titleFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 180;
            rcPage.Width = 500;
            rcPage.Y = rcPage.Y + 7;
            pdf.DrawString(bc.iniC.hostaddresst, ftrFont, Brushes.Black, rcPage);
            gapY += gapLine;
            rcPage.X = gapX + 50;
            rcPage.Y = gapY;
            rcPage.Width = 500;
            pdf.DrawString(bc.iniC.hostnamee, titleFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 230;
            rcPage.Y = rcPage.Y + 7;
            pdf.DrawString(bc.iniC.hostaddresse, ftrFont, Brushes.Black, rcPage);
            //gapY += gapLine;
            //rcPage.X = gapX+50;
            //rcPage.Y = gapY;
            //rcPage.Width = 100;
            //pdf.DrawString("HN  "+txtHn.Text.Trim(), txtFont, Brushes.Black, rcPage);
            //rcPage.X = gapX + 185;
            //rcPage.Width = 310;
            //pdf.DrawString("เลขที่/No.  ", txtFont, Brushes.Black, rcPage, _sfRight);
            gapY += gapLine;
            //rcPage.X = gapX + 50;
            //rcPage.Y = gapY;
            //rcPage.Width = 100;
            //pdf.DrawString("HN  " + txtHn.Text.Trim(), txtFont, Brushes.Black, rcPage);

            rcPage.X = gapX + 180;
            rcPage.Y = gapY;
            pdf.DrawString("เลขที่ประจำตัวผู้เสียภาษีอากร.  0105526004138", hdrFont, Brushes.Black, rcPage);
            gapY += gapLine;
            rcPage.X = gapX + 50;
            rcPage.Y = gapY;
            pdf.DrawString("HN  " + txtHn.Text.Trim(), txtFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 180;
            rcPage.Y = gapY;

            pdf.DrawString("TAX I.D. NO.  0105526004138", hdrFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 185;
            rcPage.Width = 310;
            pdf.DrawString("เลขที่/No.  ", txtFont, Brushes.Black, rcPage, _sfRight);

            gapY += gapLine + 5;
            rcPage.X = gapX + 180;
            rcPage.Y = gapY;
            rcPage.Width = 270;
            pdf.DrawString("ใบงบหน้าสรุปรายการค่ารักษาพยาบาล", titleFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 260;
            pdf.DrawString("วันที่/Date  " + bc.datetoShow(dt.Rows[0]["doc_dat"].ToString()), txtFont, Brushes.Black, rcPage, _sfRight);

            gapY += gapLine;
            gapY += 13;
            rcPage.X = gapX + 10;
            rcPage.Y = gapY;
            pdf.DrawString("ชื่อ/Name  " + txtName.Text.Trim(), txtFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 265;
            pdf.DrawString("ชึ่งป่วยเป็นโรค/Sickness  " + sickness, txtFont, Brushes.Black, rcPage);

            gapY += gapLine - 5;
            rcPage.X = gapX + 10;
            rcPage.Y = gapY;
            
            pdf.DrawString("บริษัท/Company  " + compname, txtFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 265;
            pdf.DrawString("มาขอรับการรักษาพยาบาลเมื่อวันที่/Admission Date  " + bc.datetoShow(admdate), txtFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 460;
            pdf.DrawString("เวลา/Time  " + vs.VisitTime, txtFont, Brushes.Black, rcPage);

            gapY += gapLine - 5;
            rcPage.X = gapX + 10;
            rcPage.Y = gapY;
            pdf.DrawString("เลขที่ผู้ประสพอัรตราย/Injury No.  " , txtFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 265;
            pdf.DrawString("ถึงวันที่/TO  " + bc.datetoShow(disdate), txtFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 850;
            pdf.DrawString("ดังรายการต่อไปนี้  " , txtFont, Brushes.Black, rcPage);

            gapY += gapLine -7;
            rcPage.X = gapX + 210;
            rcPage.Y = gapY;
            pdf.DrawString("รายการ", titleFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 470;
            pdf.DrawString("จำนวนเงิน", titleFont, Brushes.Black, rcPage);

            gapY += gapLine-10;
            rcPage.X = gapX + 190;
            rcPage.Y = gapY;
            pdf.DrawString("DESCRIPTION", titleFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 470;
            pdf.DrawString("AMOUNT" , titleFont, Brushes.Black, rcPage);

            //decimal amt = 0;
            //decimal.TryParse(dt.Rows[0]["mnc_sum_amt"].ToString(), out amt);
            //amt2 = bc.NumberToCurrencyTextThaiBaht(amt, MidpointRounding.AwayFromZero);
            //amt2 = bc.ThaiBahtText(amt.ToString(), false);
            gapY += 455;
            rcPage.X = gapX + 20;
            rcPage.Y = gapY;
            rcPage.Width = 400;
            //pdf.DrawString(amt2, txtFont, Brushes.Black, rcPage);

            rcPage.X = gapX + 380;
            rcPage.Y = gapY+30;
            pdf.DrawString("รวมเป็นเงินทั้งสิ้น", hdrFont, Brushes.Black, rcPage);

            gapY += gapLine;
            rcPage.X = gapX + 410;
            rcPage.Y = gapY + 25;
            pdf.DrawString("TOTAL", hdrFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 400;
            rcPage.Width = 130;
            //pdf.DrawString(amt.ToString("#,###.00"), txtFont, Brushes.Black, rcPage, _sfRight);

            gapY += gapLine-10;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            rcPage.Width = 200;
            pdf.DrawString("หมายเหตุ/Remarks", hdrFont, Brushes.Black, rcPage);

            gapY += gapLine - 5;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            rcPage.Width = 400;
            pdf.DrawString("โปรดกรุณาจ่ายเช็คขีดคร่อมในนาม ยริษัท โรงพยาบาลบางนา จำกัด  เท่านั้น ", txtFont, Brushes.Black, rcPage);
            gapY += gapLine - 7;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            pdf.DrawString("กรณีชำระเงินด้วย เช็ค ใยเสร็จรบเงินนี้จะสมบรูณ์เมื่อบริษัทฯ ได้เรียกเก็บเงินตาม เช็ค เรียบร้อยแล้ว ", txtFont, Brushes.Black, rcPage);
            gapY += gapLine - 7;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            pdf.DrawString("ใบเสร็จรับเงินทุกฉบับจะต้องปรากฎลายเซ็นผู้รับเงินและประทับตราบริษัท ", txtFont, Brushes.Black, rcPage);
            gapY += gapLine - 7;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            pdf.DrawString("Pleasepay by crossed cheque on behalf of BANGNA GENERAL HOSPITAL CO.,LTD. ", txtFont, Brushes.Black, rcPage);

            gapY += gapLine -7;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            pdf.DrawString("Payment by cheque not valid intil the cheque in honoured ", txtFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 400;
            pdf.DrawString("............................................................................* ", txtFont, Brushes.Black, rcPage);
            gapY += gapLine - 7;
            rcPage.X = gapX;
            rcPage.Y = gapY;
            pdf.DrawString("Every receipt must appear cashier signature and company seal ", txtFont, Brushes.Black, rcPage);
            rcPage.X = gapX + 450;
            pdf.DrawString("ผู้รับเงิน/ผู้จัดทำ ", txtFont, Brushes.Black, rcPage);
            
            gapY += gapLine;
            rcPage.X = gapX +440;
            rcPage.Y = gapY-5;
            pdf.DrawString("RECEIVER/PREPARED BY", txtFont, Brushes.Black, rcPage);

        }
        private void setTabPrnEmailSummary()
        {
            int gapLine = 16, gapX = 20, gapY = 60, xCol2 = 130, xCol1 = 20, xCol3 = 300, xCol4 = 390, xCol5 = 1030;
            Size size = new Size();

            String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "", anyr = "", vn1 = "", pathFolder = "", datetick = "";
            DataTable dt = new DataTable();
            C1PdfDocument pdf = new C1PdfDocument();
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
                        
            if (chkIPD.Checked)
            {
                an = grfIPD[grfIPD.Row, colIPDAn] != null ? grfIPD[grfIPD.Row, colIPDAn].ToString() : "";
                anDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                anyr = grfIPD[grfIPD.Row, colIPDAnYr] != null ? grfIPD[grfIPD.Row, colIPDAnYr].ToString() : "";
            }
            else
            {
                preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                vsDate = grfPrn[grfPrn.Row, colPrnEmailDocDat] != null ? grfPrn[grfPrn.Row, colPrnEmailDocDat].ToString() : "";
                vn = grfOPD[grfOPD.Row, colVsVn] != null ? grfOPD[grfOPD.Row, colVsVn].ToString() : "";
            }
            vn1 = vn.IndexOf("(") > 0 ? vn.Substring(0, vn.IndexOf("(")) : "";
            dt = bc.bcDB.tem01DB.selectSummaryOPD(txtHn.Text.Trim(), ptt.hnyr, vn1, vsDate);
            //RectangleF rc = GetPageRect(pdf);
            Font titleFont = new Font(bc.iniC.pdfFontName, 18, FontStyle.Bold);
            Font hdrFont = new Font(bc.iniC.pdfFontName, 16, FontStyle.Bold);
            Font ftrFont = new Font(bc.iniC.pdfFontName, 8);
            Font txtFont = new Font(bc.iniC.pdfFontName, 10, FontStyle.Regular);


            //get page rectangle, discount margins
            RectangleF rcPage = pdf.PageRectangle;
            rcPage.Inflate(-72, -92);

            //loop through selected categories
            int page = 0;

            //add page break, update page counter
            if (page > 0) pdf.NewPage();
            page++;
            gapY += gapLine;
            rcPage.X = gapX + 160;
            rcPage.Y = gapY;
            pdf.DrawString("ใบงบหน้าสรุป", titleFont, Brushes.Blue, rcPage);


            //pdf.Clear();
            //pdf.DocumentInfo.Producer = "ComponentOne C1Pdf";
            //pdf.Security.AllowCopyContent = true;
            //pdf.Security.AllowEditAnnotations = true;
            //pdf.Security.AllowEditContent = true;
            //pdf.Security.AllowPrint = true;
            //pdf.FontType = FontTypeEnum.TrueType;

            //pdf.DocumentInfo.Title = "ใบงบหน้าสรุปรายการค่ารักษาพยาบาล " + txtHn.Text.Trim();
            //gapY += gapLine;
            //rc.X = gapX+160;
            //rc.Y = gapY;
            //rc.Height = 120;
            //RectangleF rcPage = pdf.PageRectangle;

            //rcPage.Inflate(-72, -92);
            //RectangleF rcRows = new RectangleF();

            //rcRows = RectangleF.Empty;
            //rcRows.Location = new PointF(rcPage.X, rcPage.Y + titleFont.SizeInPoints + 10);
            //rcRows.Size = new SizeF(0, titleFont.SizeInPoints + 3);

            //rcRows.Width = 210;      // Product Name            

            //rcRows.X = rcRows.X + rcRows.Width + 8;

            ////rc.Location = new PointF(rc.X, rc.Y + titleFont.SizeInPoints + 10);
            ////rc.Size = new SizeF(0, titleFont.SizeInPoints + 3);
            ////pdf.DrawString("ใบงบหน้าสรุปรายการค่ารักษาพยาบาล ", titleFont, Brushes.Black, rcRows);      //New PointF(30, 447)
            ////pdf.DrawString("ใบงบหน้าสรุปรายการค่ารักษาพยาบาล ", titleFont, Brushes.Black, new PointF(gapX + 160, gapY));
            //pdf.DrawString("ใบงบหน้าสรุป", titleFont, Brushes.Blue, rcPage);

            datetick = DateTime.Now.Ticks.ToString();
            pathFolder = bc.iniC.medicalrecordexportpath + "\\" + datetick;
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
            //MemoryStream stream = new MemoryStream();
            ////String path = Path.GetDirectoryName(Application.ExecutablePath);
            pdf.Save(pathFolder + "\\"+txtHn.Text.Trim()+"_summary.pdf");
            //pdf.Save(stream);
            //stream.Seek(0, SeekOrigin.Begin);
            //pds.LoadFromStream(stream);
            fvPrnEmailSummary.DocumentSource = pds;
            Process.Start("explorer.exe", pathFolder);
        }
        internal RectangleF GetPageRect(C1PdfDocument pdf)
        {
            RectangleF rcPage = pdf.PageRectangle;
            rcPage.Inflate(-72, -72);
            return rcPage;
        }
        private void GrfPrn_MouseClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfPrn.Row <= 0) return;
            if (chkPrnSSO.Checked)
            {
                //if(grfPrn.Col == colPrnSSOchk)
                //{
                //    if(grfPrn[grfPrn.Row, colPrnSSOchk].ToString() == "False")
                //    {
                //        grfPrn[grfPrn.Row, colPrnSSOchk] = true;
                //    }
                //    else if (grfPrn[grfPrn.Row, colPrnSSOchk].ToString() == "True")
                //    {
                //        grfPrn[grfPrn.Row, colPrnSSOchk] = false;
                //    }
                //}
            }
        }

        private void ChkPrnLab_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //txtPrnDateStart.Visible = chkPrnLab.Checked;
            //txtPrnDateEnd.Visible = chkPrnLab.Checked;
            //lbPrnDateEnd.Visible = chkPrnLab.Checked;
            //lbPrnDateStart.Visible = chkPrnLab.Checked;
        }
        
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkPrnSSO.Checked)
            {
                setGrfPrnSSO_OPD();
            }
        }
        private void setGrfPrnEmail()
        {
            pageLoadGrfPrn = true;
            grfPrn.Clear();
            grfPrn.Rows.Count = 1;
            DataTable dt = new DataTable();

            grfPrn.Cols.Count = 10;
            
            grfPrn.Cols[colPrnEmailPaidName].Caption = "paid";
            grfPrn.Cols[colPrnEmailDocNo].Caption = "NO";
            grfPrn.Cols[colPrnEmailDocDat].Caption = "prn date";
            grfPrn.Cols[colPrnEmailDate].Caption = "vs date";

            grfPrn.Cols[colPrnEmailPaidName].Width = 100;
            grfPrn.Cols[colPrnEmailDocNo].Width = 60;
            grfPrn.Cols[colPrnEmailDocDat].Width = 100;
            grfPrn.Cols[colPrnEmailDate].Width = 100;
            grfPrn.Cols[colPrnEmailDocCD].Width = 60;
            grfPrn.Cols[colPrnEmailDocYr].Width = 60;
            dt = bc.bcDB.vsDB.selectFinance(txtHn.Text.Trim(), ptt.hnyr, preno, vsDate);
            grfPrn.Rows.Count = dt.Rows.Count + 1;
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                i++;
                grfPrn[i, 0] = (i);
                grfPrn[i, colPrnEmailPaidName] = row["mnc_fn_typ_cd"].ToString();
                grfPrn[i, colPrnEmailDocNo] = row["mnc_doc_no"].ToString();
                grfPrn[i, colPrnEmailDocDat] = row["doc_date"].ToString();
                grfPrn[i, colPrnEmailDate] = row["vs_date"].ToString();
                grfPrn[i, colPrnEmailDocCD] = row["mnc_doc_cd"].ToString();
                grfPrn[i, colPrnEmailDocYr] = row["mnc_doc_yr"].ToString();
                grfPrn[i, colPrnEmailStatusOPD] = row["mnc_job_cd"].ToString();
                grfPrn[i, colPrnEmailAn] = row["mnc_an_no"].ToString();
                grfPrn[i, colPrnEmailAnYr] = row["mnc_an_yr"].ToString();
            }
            CellNoteManager mgr = new CellNoteManager(grfPrn);
            grfPrn.Cols[colPrnEmailPaidName].AllowEditing = false;
            grfPrn.Cols[colPrnEmailDocNo].AllowEditing = false;
            grfPrn.Cols[colPrnEmailDocDat].AllowEditing = false;
            grfPrn.Cols[colPrnEmailDate].AllowEditing = false;
            grfPrn.Cols[colPrnEmailDocCD].AllowEditing = false;
            grfPrn.Cols[colPrnEmailDocYr].AllowEditing = false;
            grfPrn.Cols[colPrnEmailStatusOPD].AllowEditing = false;
            grfPrn.Cols[colPrnEmailAn].AllowEditing = false;
            grfPrn.Cols[colPrnEmailAnYr].AllowEditing = false;
            pageLoadGrfPrn = false;
        }
        private void setShowTabPrnLine2(Boolean flag)
        {
            label11.Visible = !flag;
            labe2.Visible = !flag;
            lbPrnDateStart.Visible = !flag;
            txtPrnDateStart.Visible = !flag;
            lbPrnDateEnd.Visible = !flag;
            txtPrnDateEnd.Visible = !flag;
            lbDocGrp.Visible = !flag;
            cboDocGrp.Visible = !flag;
            lbDocSubGrp.Visible = !flag;
            cboDocSubGrp.Visible = !flag;
            btnDocOk.Visible = !flag;
            btnDocExport.Visible = !flag;
            lbDocAn.Visible = !flag;
            btnPrn.Visible = !flag;
            btnSearch.Visible = !flag;
            chkPrnSSOall.Visible = !flag;
            //lbtxtPrnEmailTo.Visible = flag;
            //lbtxtPrnEmailSubject.Visible = flag;
            //txtPrnEmailTo.Visible = flag;
            //txtPrnEmailSubject.Visible = flag;

            lbtxtPrnEmailTo.Visible = flag;
            lbtxtPrnEmailSubject.Visible = flag;
            //pnPrnEmail.Visible = flag;
            txtPrnEmailTo.Visible = flag;
            txtPrnEmailSubject.Visible = flag;
            txtPrnEmailBody.Visible = flag;
            lbtxtPrnEmailBody.Visible = flag;
            //btnPrn.Visible = flag;
            //lbtxtPrnEmailSubject.Visible = flag;
            //txtPrnEmailTo.Visible = flag;
            //txtPrnEmailSubject.Visible = flag;
            //btnPrn.Visible = true;
            //btnPrn.Text = chkPrnEmail.Checked ? "Send" : "Print";
        }
        private void ChkPrnSSOall_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkPrnSSO.Checked)
            {
                txtPrnDateStart.Visible = !chkPrnSSOall.Checked;
                txtPrnDateEnd.Visible = !chkPrnSSOall.Checked;
                lbPrnDateEnd.Visible = !chkPrnSSOall.Checked;
                lbPrnDateStart.Visible = !chkPrnSSOall.Checked;
            }
        }
        private void ChkPrnEmail_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkPrnEmail.Checked)
            {
                cspPrnLeft.Width = 300;
                
                setShowTabPrnLine2(chkPrnEmail.Checked);
                setGrfPrnEmail();
                if (fvPrnEmailSummary != null) fvPrnEmailSummary.DocumentSource = null;
                if (fvPrnEmailDrug != null) fvPrnEmailDrug.DocumentSource = null;
                if (fvPrnEmailLab != null) fvPrnEmailLab.DocumentSource = null;
                if (fvPrnEmailXray != null) fvPrnEmailXray.DocumentSource = null;
            }
            else
            {
                cspPrnLeft.Width = tabPrn.Width - 0;
                setShowTabPrnLine2(chkPrnEmail.Checked);
            }
        }
        private void BtnDocExport_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            showLbLoading();
            if (chkIPD.Checked)
            {
                DataTable dt = new DataTable();
                String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "", anyr = "", vn1 = "", err="", datetick="", pathFolder="";
                datetick = DateTime.Now.Ticks.ToString();
                pathFolder = bc.iniC.medicalrecordexportpath + "\\" + txtHn.Text.Trim() + "\\" + datetick;
                statusOPD = grfIPD[grfIPD.Row, colIPDStatus] != null ? grfIPD[grfIPD.Row, colIPDStatus].ToString() : "";
                preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
                vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";

                //chkIPD.Checked = true;
                an = grfIPD[grfIPD.Row, colIPDAn] != null ? grfIPD[grfIPD.Row, colIPDAn].ToString() : "";
                anDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                anyr = grfIPD[grfIPD.Row, colIPDAnYr] != null ? grfIPD[grfIPD.Row, colIPDAnYr].ToString() : "";
                label2.Text = "AN :";
                an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";
                docgrpid = cboDocGrp.SelectedItem != null ? cboDocGrp.SelectedItem.ToString() : "1100000099";
                docgrpid = cboDocGrp.SelectedItem == null ? "1100000099" : ((ComboBoxItem)cboDocGrp.SelectedItem).Value;
                if (docgrpid.Equals("1100000099"))
                {
                    dt = bc.bcDB.dscDB.selectByAn(txtHn.Text, an);
                }
                else
                {
                    dt = bc.bcDB.dscDB.selectByAnDocGrp(txtHn.Text, an, docgrpid);
                }
                try
                {
                    if (Directory.Exists(pathFolder))
                    {
                        Directory.Delete(pathFolder, true);
                        Thread.Sleep(100);
                        Application.DoEvents();
                    }
                    Thread.Sleep(100);
                    //if (Directory.Exists(bc.iniC.medicalrecordexportpath))
                    //{
                    //    Directory.Delete(bc.iniC.medicalrecordexportpath, true);
                    //    Thread.Sleep(200);
                    //    Application.DoEvents();
                    //}
                }
                catch (Exception ex)
                {

                }
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        //if (Directory.Exists(pathFolder))
                        //{
                        //    Directory.Delete(pathFolder, true);
                        //}
                        //Thread.Sleep(100);
                        //if (Directory.Exists(bc.iniC.medicalrecordexportpath))
                        //{
                        //    Directory.Delete(bc.iniC.medicalrecordexportpath , true);
                        //}
                        C1PdfDocument pdfdoc = new C1PdfDocument();
                        Directory.CreateDirectory(pathFolder);
                        Thread.Sleep(200);
                        FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                        err = "00";
                        foreach (DataRow row1 in dt.Rows)
                        {
                            Image loadedImage, resizedImage = null;
                            String dscid = "", filename = "", ftphost = "", id = "", folderftp = "";
                            dscid = row1[bc.bcDB.dscDB.dsc.doc_scan_id].ToString();
                            filename = row1[bc.bcDB.dscDB.dsc.image_path].ToString();
                            ftphost = row1[bc.bcDB.dscDB.dsc.host_ftp].ToString();
                            folderftp = row1[bc.bcDB.dscDB.dsc.folder_ftp].ToString();
                            MemoryStream stream;
                            stream = ftp.download(folderftp + "//" + filename);
                            stream.Position = 0;
                            loadedImage = Image.FromStream(stream);
                            loadedImage.Save(pathFolder + "\\" + txtHn.Text.Trim() + "_" + dscid + "_1.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
                            float newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

                            float widthFactor = 1.5F;
                            float heightFactor = 1.5F;

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
                            RectangleF recf = new RectangleF(10, 10, (int)newWidth, (int)newHeight);
                            
                            pdfdoc.DrawImage(loadedImage, recf);
                            pdfdoc.NewPage();
                        }
                        //  result lab
                        DataTable dtLab = new DataTable();
                        dtLab = setPrintLab();
                        if (dtLab.Rows.Count > 0)
                        {
                            String filename = bc.exportResultLab(dtLab, txtHn.Text.Trim(), txtVN.Text.Trim(), "", pathFolder);
                            Application.DoEvents();
                            for (int i = 1; i <= 30; i++)
                            {
                                if (File.Exists(filename.Replace(".jpg","") + "_page" + i + ".jpg"))
                                {
                                    Image loadedImage, resizedImage = null;
                                    loadedImage = Image.FromFile(filename.Replace(".jpg", "") + "_page" + i + ".jpg");
                                    float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
                                    float newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

                                    float widthFactor = 1.5F;
                                    float heightFactor = 1.5F;

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

                                    RectangleF recf = new RectangleF(5, 5, (int)newWidth, (int)newHeight);
                                    pdfdoc.DrawImage(loadedImage, recf);
                                    pdfdoc.NewPage();
                                }
                            }
                        }

                        // result xray
                        DataTable dtXray = new DataTable();
                        dtXray = setPrintXray();
                        if (dtXray.Rows.Count > 0)
                        {
                            if((dtXray.Rows[0]["xry_result"] != null) && (!dtXray.Rows[0]["xry_result"].ToString().Equals("")))
                            {
                                String filename = bc.exportResultXray(dtXray, txtHn.Text.Trim(), txtVN.Text.Trim(), "", pathFolder);
                                Application.DoEvents();
                                for (int i = 1; i <= 30; i++)
                                {
                                    if (File.Exists(filename + "_page" + i + ".jpg"))
                                    {
                                        Image loadedImage, resizedImage = null;
                                        loadedImage = Image.FromFile(filename + "_page" + i + ".jpg");
                                        float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
                                        float newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

                                        float widthFactor = 1.5F;
                                        float heightFactor = 1.5F;

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

                                        RectangleF recf = new RectangleF(5, 5, (int)newWidth, (int)newHeight);
                                        pdfdoc.DrawImage(loadedImage, recf);
                                        pdfdoc.NewPage();
                                    }
                                }
                            }
                            
                        }
                        //      lab out
                        DataTable dtlaboutdsc = new DataTable();
                        //dtlaboutdsc = bc.bcDB.dscDB.selectLabOutByAn(txtHn.Text.Trim(), an);
                        dtlaboutdsc = bc.bcDB.dscDB.selectLabOutByDateReq("","",txtHn.Text.Trim(), "");
                        //dtlaboutdsc = bc.bcDB.dscDB.selectLabOutByDateReq2("", "", txtHn.Text.Trim(), "");
                        List<String> lAn = new List<string>();
                        foreach (DataRow rowdsc in dtlaboutdsc.Rows)
                        {
                            String an1 = "";
                            an1 = rowdsc[bc.bcDB.dscDB.dsc.an] != null ? rowdsc[bc.bcDB.dscDB.dsc.an].ToString() : "";
                            if (lAn.Count == 0)
                            {
                                lAn.Add(an1);
                            }
                            foreach (String an2 in lAn)
                            {
                                if (!an2.Equals(an1))
                                {
                                    lAn.Add(an2);
                                }
                            }
                        }
                        if (lAn.Count == 0)
                        {

                        }
                        foreach (DataRow rowdsc in dtlaboutdsc.Rows)
                        {
                            try
                            {
                                String dscid = "", filename = "", ftphost = "", id = "", folderftp = "", an1="", pid="";
                                an1 = rowdsc[bc.bcDB.dscDB.dsc.an] != null ? rowdsc[bc.bcDB.dscDB.dsc.an].ToString() : "";
                                dscid = rowdsc[bc.bcDB.dscDB.dsc.doc_scan_id].ToString();
                                pid = rowdsc[bc.bcDB.dscDB.dsc.an] != null ? rowdsc["mnc_id_nam"].ToString() : "";
                                
                                MemoryStream stream;
                                stream = ftp.download(rowdsc[bc.bcDB.dscDB.dsc.folder_ftp] + "/" + rowdsc[bc.bcDB.dscDB.dsc.image_path].ToString());
                                String ext = Path.GetExtension(rowdsc[bc.bcDB.dscDB.dsc.image_path].ToString());
                                if (ext.Equals(".jpg"))
                                {
                                    Image loadedImage, resizedImage = null;
                                    loadedImage = Image.FromStream(stream);
                                    loadedImage.Save(pathFolder + "\\" + txtHn.Text.Trim() + "_outlab_" + dscid + ext, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    Application.DoEvents();
                                    if (File.Exists(pathFolder + "\\" + txtHn.Text.Trim() + "_outlab_" + dscid + ext))
                                    {
                                        float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
                                        float newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

                                        float widthFactor = 1.5F;
                                        float heightFactor = 1.5F;

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
                                        RectangleF recf = new RectangleF(5, 5, (int)newWidth, (int)newHeight);
                                        pdfdoc.DrawImage(loadedImage, recf);
                                        pdfdoc.NewPage();
                                    }
                                }
                                else
                                {
                                    //var fileStream = new FileStream(pathFolder + "\\" + txtHn.Text.Trim() + "_outlab_" + dscid + ext, FileMode.Create, FileAccess.Write);
                                    stream.Position = 0;
                                    //stream.CopyTo(fileStream);
                                    //fileStream.Dispose();
                                    //Application.DoEvents();

                                    C1PdfDocumentSource pdf = new C1PdfDocumentSource();
                                    
                                    var exporter = pdf.SupportedExportProviders[4].NewExporter();
                                    exporter.ShowOptions = false;
                                    exporter.FileName = pathFolder + "\\" + txtHn.Text.Trim() + "_outlab_" + dscid;
                                    
                                    pdf.LoadFromStream(stream);
                                    pdf.Export(exporter);
                                    Application.DoEvents();
                                    for(int i = 1; i <= 30; i++)
                                    {
                                        if (File.Exists(exporter.FileName + "_page"+i+".jpg"))
                                        {
                                            Image loadedImage, resizedImage = null;
                                            loadedImage = Image.FromFile(exporter.FileName + "_page" + i + ".jpg");
                                            float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
                                            float newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

                                            float widthFactor = 1.5F;
                                            float heightFactor = 1.5F;

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
                                            RectangleF recf = new RectangleF(5, 5, (int)newWidth, (int)newHeight);
                                            pdfdoc.DrawString("ID "+ pid, fEdit5B, SystemBrushes.WindowText, recf);
                                            recf = new RectangleF(35, 35, (int)newWidth, (int)newHeight);
                                            pdfdoc.DrawImage(loadedImage, recf);
                                            pdfdoc.NewPage();
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                new LogWriter("e", "FrmScanView1 BtnDocExport_Click foreach (DataRow rowdsc in dtlaboutdsc.Rows) " + ex.Message);
                            }
                        }
                        Thread.Sleep(200);
                        Application.DoEvents();

                        pdfdoc.Save(pathFolder + "\\" +txtHn.Text+".pdf");
                        //MessageBox.Show("1111", "");
                        Process.Start("explorer.exe", pathFolder);
                    }
                    catch (Exception ex)
                    {
                        String aaa = ex.Message + " " + err;
                        new LogWriter("e", "FrmScanView1 BtnDocExport_Click if (dt.Rows.Count > 0) ex " + ex.Message + " " + err + " colcnt " + " HN " + txtHn.Text + " " + an);
                    }
                }
            }
            hideLbLoading();
        }

        private void BtnDocOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void BtnPrn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (this.chkPrnCri.Checked)
            {
                setPrnCri();
            }
            else if (this.chkPrnLab.Checked)
            {
                showLbLoading();
                setPrnLabCri();
                String pathFolder = setExportOutLabtoFolder();
                setFileOutLab(pathFolder);
                Process.Start("explorer.exe", pathFolder);
                hideLbLoading();
            }
            else if (this.chkPrnSSO.Checked)
            {
                showLbLoading();
                MessageBox.Show("11", "");
                String pathFolder = setExportSSOtoFolder();

                Process.Start("explorer.exe", pathFolder);
                hideLbLoading();
            }
        }
        private String setExportSSOtoFolder()
        {
            String pathFolder = "", datetick = "";

            datetick = DateTime.Now.Ticks.ToString();
            pathFolder = bc.iniC.medicalrecordexportpath + "\\" + datetick;
            if (Directory.Exists(pathFolder))
            {
                Directory.Delete(pathFolder, true);
                Thread.Sleep(100);
                Application.DoEvents();
            }
            Directory.CreateDirectory(pathFolder);
            C1PdfDocument pdfdocR = new C1PdfDocument();
            C1PdfDocument pdfdocS = new C1PdfDocument();
            C1PdfDocument pdfdocL = new C1PdfDocument();
            C1PdfDocument pdfdocX = new C1PdfDocument();
            int rowi = 0,rowcnt=0;
            foreach (Row row in grfPrn.Rows)
            {
                if (row[colPrnSSOchk].ToString().Equals("True"))
                {
                    rowcnt++;
                }
            }
            foreach (Row row in grfPrn.Rows)
            {
                if (row[colPrnSSOchk].ToString().Equals("True"))
                {
                    rowi++;
                    try
                    {
                        Image stffnoteR, stffnoteS;
                        String preno1 = "", file = "", dd = "", mm = "", yy = "", filename = "", vsdate = "",vn = "", preno = "";
                        int chk = 0;
                        vn = row[colPrnSSOVn].ToString();
                        preno = row[colPrnSSOpreno].ToString();
                        vsdate = row[colPrnSSOvsDate].ToString();
                        dd = vsdate.Substring(0, 2);
                        mm = vsdate.Substring(3, 2);
                        yy = "20" + vsdate.Substring(vsdate.Length - 2, 2);
                        preno1 = preno;
                        int.TryParse(yy, out chk);
                        if (chk > 2500)
                            chk -= 543;
                        file = "\\\\" + bc.iniC.pathScanStaffNote + chk + "\\" + mm + "\\" + dd + "\\";
                        preno1 = "000000" + preno1;
                        preno1 = preno1.Substring(preno1.Length - 6);
                        stffnoteR = Image.FromFile(file + preno1 + "R.JPG");
                        stffnoteS = Image.FromFile(file + preno1 + "S.JPG");
                        filename = "\\" + txtHn.Text.Trim() + "_" + yy + mm + dd + "_" + preno1;
                        stffnoteR.Save(pathFolder + filename + "_R.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        stffnoteS.Save(pathFolder + filename + "_S.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        new LogWriter("d", "FrmScanView1 setExportSSOtoFolder hn "+txtHn.Text.Trim()+" vsdate "+yy+mm+dd+" preno "+preno1+ " rowi " + rowi+" rowcnt "+ rowcnt);

                        Image loadedImage, resizedImage = null;
                        loadedImage = stffnoteR;
                        float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
                        float newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

                        float widthFactor = 1.5F;
                        float heightFactor = 1.5F;

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

                        RectangleF recf = new RectangleF(5, 5, (int)newWidth, (int)newHeight);
                        pdfdocR.DrawImage(loadedImage, recf);
                        if (rowi < rowcnt)
                        {
                            pdfdocR.NewPage();
                        }
                        loadedImage = stffnoteS;
                        newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
                        newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

                        widthFactor = 1.5F;
                        heightFactor = 1.5F;

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
                        pdfdocS.DrawImage(loadedImage, recf);
                        if (rowi < rowcnt)
                        {
                            pdfdocS.NewPage();
                        }

                        //LAB
                        DataTable dtLab = new DataTable();                        
                        dtLab = setPrintLabPrnSSO(vn, preno, chk+"-"+mm+"-"+dd);
                        if (dtLab.Rows.Count > 0)
                        {
                            filename = bc.exportResultLab(dtLab, txtHn.Text.Trim(), vn, "", pathFolder);
                            Application.DoEvents();
                            for (int i = 1; i <= 30; i++)
                            {
                                if (File.Exists(filename.Replace(".jpg", "") + "_page" + i + ".jpg"))
                                {
                                    loadedImage = null;
                                    resizedImage = null;
                                    loadedImage = Image.FromFile(filename.Replace(".jpg", "") + "_page" + i + ".jpg");
                                    newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
                                    newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

                                    widthFactor = 1.5F;
                                    heightFactor = 1.5F;

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

                                    recf = new RectangleF(5, 5, (int)newWidth, (int)newHeight);
                                    pdfdocL.DrawImage(loadedImage, recf);
                                    pdfdocL.NewPage();
                                }
                            }
                            //Thread.Sleep(200);
                            //Application.DoEvents();
                            //pdfdocL.Save(pathFolder + "\\" + txtHn.Text + "_L.pdf");
                        }

                        //Xray
                        DataTable dtXray = new DataTable();
                        dtXray = setPrintXraySSO(vn, preno, chk + "-" + mm + "-" + dd);
                        if (dtXray.Rows.Count > 0)
                        {
                            if ((dtXray.Rows[0]["xry_result"] != null) && (!dtXray.Rows[0]["xry_result"].ToString().Equals("")))
                            {
                                filename = bc.exportResultXray(dtXray, txtHn.Text.Trim(), vn, "", pathFolder);
                                Application.DoEvents();
                                for (int i = 1; i <= 30; i++)
                                {
                                    if (File.Exists(filename.Replace(".jpg", "") + "_page" + i + ".jpg"))
                                    {
                                        loadedImage = null;
                                        resizedImage = null;
                                        loadedImage = Image.FromFile(filename.Replace(".jpg", "") + "_page" + i + ".jpg");
                                        newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
                                        newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

                                        widthFactor = 1.5F;
                                        heightFactor = 1.5F;

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
                                        recf = new RectangleF(5, 5, (int)newWidth, (int)newHeight);
                                        pdfdocX.DrawImage(loadedImage, recf);
                                        pdfdocX.NewPage();
                                    }
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        new LogWriter("e", "FrmScanView1 setExportSSOtoFolder error "+ex.Message);
                    }                    
                }
            }
            Thread.Sleep(200);
            Application.DoEvents();
            pdfdocR.Save(pathFolder + "\\" + bc.iniC.ssoid+"_" +ptt.idcard + "_MED.pdf");
            pdfdocS.Save(pathFolder + "\\" + bc.iniC.ssoid + "_" + ptt.idcard + "_MAIN.pdf");
            pdfdocL.Save(pathFolder + "\\" + bc.iniC.ssoid + "_" + ptt.idcard + "_LAB.pdf");
            pdfdocX.Save(pathFolder + "\\" + bc.iniC.ssoid + "_" + ptt.idcard + "_XRAY.pdf");

            return pathFolder;
        }
        private String setExportOutLabtoFolder()
        {
            String pathFolder = "", datetick = "";

            datetick = DateTime.Now.Ticks.ToString();
            pathFolder = bc.iniC.medicalrecordexportpath + "\\" + datetick;
            if (Directory.Exists(pathFolder))
            {
                Directory.Delete(pathFolder, true);
                Thread.Sleep(100);
                Application.DoEvents();
            }
            Directory.CreateDirectory(pathFolder);
            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
            foreach (Row row in grfPrn.Rows)
            {
                String hn = "", reqdate = "", reqno = "", filename = "";
                if (hn.Equals("HN")) continue;
                hn = row[colPrnlabHn] != null ? row[colPrnlabHn].ToString() : "";
                reqdate = row[colPrnlabReqDate] != null ? row[colPrnlabReqDate].ToString() : "";
                reqno = row[colPrnlabReqNo] != null ? row[colPrnlabReqNo].ToString() : "";
                DataTable dt = new DataTable();
                //reqdate = bc.datetoDB(reqdate);
                dt = bc.bcDB.dscDB.selectLabOutByHnReqDateReqNo(hn, reqdate, reqno);
                int i = 0;
                foreach (DataRow drow in dt.Rows)
                {
                    try
                    {
                        i++;
                        String dscid = "", image_path = "", ftphost = "", id = "", folderftp = "", ext = "";
                        folderftp = drow[bc.bcDB.dscDB.dsc.folder_ftp].ToString();
                        image_path = drow[bc.bcDB.dscDB.dsc.image_path].ToString();
                        ext = Path.GetExtension(image_path);
                        filename = hn + "_" + datetick + "_" + i + ext;
                        MemoryStream stream;
                        stream = ftp.download(folderftp + "//" + image_path);
                        stream.Position = 0;
                        FileStream filestream = new FileStream(pathFolder + "\\" + filename, FileMode.Create, FileAccess.Write);
                        stream.WriteTo(filestream);
                        filestream.Close();
                        stream.Close();
                    }
                    catch (Exception ex)
                    {
                        string ex1 = ex.Message;
                    }

                }
            }
            return pathFolder;
        }
        private void setFileOutLab(String pathFolder)
        {
            List<String> filePaths = new List<String>();
            DirectoryInfo d = new DirectoryInfo(pathFolder);
            FileInfo[] dirs = d.GetFiles("*.*");
            C1PdfDocument pdfdoc = new C1PdfDocument();
            int i1 = 0;
            foreach (FileInfo diNext in dirs)
            {
                String filename = diNext.FullName;
                try
                {
                    String dscid = "", ftphost = "", id = "", folderftp = "", an1 = "";

                    String ext = Path.GetExtension(filename);
                    if (ext.Equals(".jpg"))
                    {
                        Image loadedImage, resizedImage = null;
                        loadedImage = Image.FromFile(filename);
                        loadedImage.Save(pathFolder + "\\" + txtHn.Text.Trim() + "_outlab_" + dscid + ext, System.Drawing.Imaging.ImageFormat.Jpeg);
                        Application.DoEvents();
                        if (File.Exists(pathFolder + "\\" + txtHn.Text.Trim() + "_outlab_" + dscid + ext))
                        {
                            float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
                            float newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

                            float widthFactor = 1.5F;
                            float heightFactor = 1.5F;

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
                            RectangleF recf = new RectangleF(5, 5, (int)newWidth, (int)newHeight);
                            pdfdoc.DrawImage(loadedImage, recf);
                            pdfdoc.NewPage();
                        }
                    }
                    else
                    {
                        C1PdfDocumentSource pdf = new C1PdfDocumentSource();
                        i1++;
                        var exporter = pdf.SupportedExportProviders[4].NewExporter();
                        exporter.ShowOptions = false;
                        exporter.FileName = pathFolder + "\\" + txtHn.Text.Trim() + "_outlab_" + i1;

                        pdf.LoadFromFile(filename);
                        pdf.Export(exporter);
                        Application.DoEvents();
                        Thread.Sleep(50);
                        for (int i = 1; i <= 40; i++)
                        {
                            if (File.Exists(exporter.FileName + "_page" + i + ".jpg"))
                            {
                                Image loadedImage, resizedImage = null;
                                loadedImage = Image.FromFile(exporter.FileName + "_page" + i + ".jpg");
                                float newWidth = loadedImage.Width * 100 / loadedImage.HorizontalResolution;
                                float newHeight = loadedImage.Height * 100 / loadedImage.VerticalResolution;

                                float widthFactor = 1.5F;
                                float heightFactor = 1.5F;

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

                                RectangleF recf = new RectangleF(5, 5, (int)newWidth, (int)newHeight);
                                pdfdoc.DrawImage(loadedImage, recf);
                                pdfdoc.NewPage();
                            }
                        }
                    }
                    Thread.Sleep(50);
                    Application.DoEvents();
                    
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmScanView1 BtnDocExport_Click foreach (DataRow rowdsc in dtlaboutdsc.Rows) " + ex.Message);
                }
            }
            Thread.Sleep(50);
            Application.DoEvents();
            pdfdoc.Save(pathFolder + "\\" + txtHn.Text + "_outlab_all.pdf");
        }
        private void setPrnLabCri()
        {
            grfPrn.Rows.Count = 1;
            DataTable dt = new DataTable();
            String datestart = "", dateend = "", lbcode="";
            String[] lbcode1 = txtPrnCri.Text.Trim().Split(',');
            for (int j = 0; j < lbcode1.Length; j++)
            {
                lbcode1[j] = "'" + lbcode1[j].Trim() + "'";
                lbcode += lbcode1[j] + ",";
            }
            if (lbcode.Length > 0)
            {
                if (lbcode.Substring(lbcode.Length - 1).Equals(","))
                {
                    lbcode = lbcode.Substring(0, lbcode.Length - 1);
                }
            }
            DateTime dtstart = new DateTime();
            DateTime dtend = new DateTime();
            DateTime.TryParse(txtPrnDateStart.Text, out dtstart);
            DateTime.TryParse(txtPrnDateEnd.Text, out dtend);
            
            //txtPrnDateEnd.CultureInfo = new CultureInfo("en-US");
            //txtPrnDateEnd.Culture = new CultureInfo("en-US");
            datestart = dtstart.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            dateend = dtend.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            new LogWriter("d", "FrmScanView1 setPrnLabCri  datestart "+ datestart + " txtPrnDateStart " + txtPrnDateStart.Text + " dateend "+ dateend+ " txtPrnDateEnd "+ txtPrnDateEnd.Text);
            datestart = bc.datetoDB(txtPrnDateStart.Text);
            dateend = bc.datetoDB(txtPrnDateEnd.Text);
            datestart = txtPrnDateStart.Text.Substring(txtPrnDateStart.Text.Length - 4) +"-"+ txtPrnDateStart.Text.Substring(3, 2) + "-" + txtPrnDateStart.Text.Substring(0, 2);
            dateend = txtPrnDateEnd.Text.Substring(txtPrnDateEnd.Text.Length - 4) + "-" + txtPrnDateEnd.Text.Substring(3, 2) + "-" + txtPrnDateEnd.Text.Substring(0, 2);
            new LogWriter("d", "FrmScanView1 setPrnLabCri 33  datestart " + datestart + " txtPrnDateStart " + txtPrnDateStart.Text + " dateend " + dateend + " txtPrnDateEnd " + txtPrnDateEnd.Text);
            dt = bc.bcDB.labDB.SelectHnLabOut(datestart, dateend, lbcode);
            
            grfPrn.Cols.Count = 7;
            grfPrn.Rows.Count = dt.Rows.Count+1;
            grfPrn.Cols[colPrnlabHn].Caption = "HN";
            grfPrn.Cols[colPrnlabName].Caption = "Patient Name";
            grfPrn.Cols[colPrnlabVN].Caption = "VN";
            grfPrn.Cols[colPrnlabAN].Caption = "AN";
            grfPrn.Cols[colPrnlabReqDate].Caption = "req date";
            grfPrn.Cols[colPrnlabReqNo].Caption = "req no";
            grfPrn.Cols[colPrnlabHn].Width = 100;
            grfPrn.Cols[colPrnlabName].Width = 200;
            grfPrn.Cols[colPrnlabVN].Width = 100;
            grfPrn.Cols[colPrnlabAN].Width = 100;
            grfPrn.Cols[colPrnlabReqDate].Width = 100;
            grfPrn.Cols[colPrnlabReqNo].Width = 100;
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                i++;
                grfPrn[i, 0] = (i);
                grfPrn[i, colPrnlabHn] = row["mnc_hn_no"].ToString();
                grfPrn[i, colPrnlabName] = row["mnc_patname"].ToString();
                grfPrn[i, colPrnlabVN] = row["mnc_vn_no"].ToString()+"."+ row["mnc_vn_seq"].ToString()+"."+ row["mnc_vn_sum"].ToString();
                grfPrn[i, colPrnlabAN] = row["MNC_AN_NO"].ToString()+"/"+ row["MNC_AN_YR"].ToString();
                grfPrn[i, colPrnlabReqDate] = row["mnc_req_dat"].ToString();
                grfPrn[i, colPrnlabReqNo] = row["mnc_req_no"].ToString();
            }

            //grfPrn[i, colLabNameSub] = row1["mnc_res"].ToString();
            CellNoteManager mgr = new CellNoteManager(grfPrn);
            grfPrn.Cols[colLabName].AllowEditing = false;
            
        }
        private void setPrnCri()
        {
            string sort1 = "";
            string[] strArray1 = ((Control)this.txtPrnCri).Text.Trim().Split('-');
            string[] strArray2 = ((Control)this.txtPrnCri).Text.Trim().Split(',');
            if (strArray1.Length != 0 && !strArray1[0].Equals(((Control)this.txtPrnCri).Text.Trim()))
            {
                int result1 = 0;
                int result2 = 0;
                int.TryParse(strArray1[0], out result1);
                int.TryParse(strArray1[1], out result2);
                for (int index = result1; index <= result2; ++index)
                    sort1 = sort1 + index.ToString() + ",";
            }
            else if (strArray2.Length > 0)
            {
                foreach (string str in strArray2)
                {
                    if (str.Length > 0)
                        sort1 = sort1 + str + ",";
                }
            }
            if (sort1.Length > 1)
                sort1 = sort1.Substring(0, sort1.Length - 1);
            new LogWriter("d", "FrmScanView1 BtnPrn_Click  ");
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = bc.bcDB.dscDB.selectBySortID(txtHn.Text.Trim(), txtVN.Text.Trim(), sort1);
            if (dataTable2.Rows.Count > 0)
            {
                new LogWriter("d", "FrmScanView1 BtnPrn_Click  dt.Rows.Count");
                streamPrint = null;
                foreach (DataRow row in dataTable2.Rows)
                {
                    foreach (FrmScanView1.listStream listStream in this.lStream)
                    {
                        if (listStream.id.Equals(row[bc.bcDB.dscDB.dsc.doc_scan_id].ToString()))
                        {
                            LogWriter logWriter3 = new LogWriter("d", "FrmScanView1 BtnPrn_Click  foreach (DataRow drow in dt.Rows)");
                            streamPrint = listStream.stream;
                            setGrfScanToPrint();
                        }
                    }
                }
            }
        }
        private void initGrfOrdItem()
        {
            int gapY = 30, gapX = 20, gapLine=0, gapColName=120;
            Size size = new Size();
            
            Label lbItmId, lbItmName, lbItmQty, lbItmFre, lbItmIn1, lbItmIn2;
            pnscOrdItem = new Panel();
            pnscOrdItem.Dock = DockStyle.Fill;
            
            sCOrdItem.SuspendLayout();
            scOrdItem.SuspendLayout();
            scOrdItemGrf.SuspendLayout();

            grfOrdItem = new C1FlexGrid();
            grfOrdItem.Font = fEdit;
            grfOrdItem.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOrdItem.Location = new System.Drawing.Point(0, 0);
            grfOrdItem.Rows.Count = 1;
            grfOrdItem.Cols.Count = 11;
            grfOrdItem.Name = "grfOrdItem";
            grfOrdItem.MouseClick += GrfOrdItem_MouseClick;
            grfOrdItem.Cols[colOrdAddNameT].Caption = "ชื่อ";
            grfOrdItem.Cols[colOrdAddQty].Caption = "QTY";
            grfOrdItem.Cols[colOrdAddItemType].Caption = "หน่วย";
            grfOrdItem.Cols[colOrdAddUnit].Caption = "หน่วย";
            grfOrdItem.Cols[colOrdAddDrugFr].Caption = "ความถี่";
            grfOrdItem.Cols[colOrdAddDrugIn].Caption = "ข้อความระวัง";
            grfOrdItem.Cols[colOrdAddNameT].Width = 300;
            grfOrdItem.Cols[colOrdAddQty].Width = 50;
            grfOrdItem.Cols[colOrdAddItemType].Width = 80;
            grfOrdItem.Cols[colOrdAddUnit].Width = 80;
            grfOrdItem.Cols[colOrdAddDrugFr].Width = 200;
            grfOrdItem.Cols[colOrdAddDrugIn].Width = 200;

            grfOrdItem.Cols[colOrdAddUnit].Visible = false;
            grfOrdItem.Cols[colOrdAddFlag].Visible = false;
            //grfOrdItem.Cols[colOrdDrugId].Visible = false;
            grfOrdItem.Cols[colOrdAddNameT].AllowEditing = false;
            grfOrdItem.Cols[colOrdAddQty].AllowEditing = false;
            grfOrdItem.Cols[colOrdAddItemType].AllowEditing = false;
            grfOrdItem.Cols[colOrdAddUnit].AllowEditing = false;
            grfOrdItem.Cols[colOrdAddDrugFr].AllowEditing = false;
            grfOrdItem.Cols[colOrdAddDrugIn].AllowEditing = false;
            //grfOrdItem.DoubleClick += GrfOrdItem_DoubleClick1;
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการลบข้อมูล รายการนี้", new EventHandler(ContextMenu_delete_ord_item));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            grfOrdItem.ContextMenu = menuGw;

            scOrdItem.Collapsible = true;
            scOrdItem.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            scOrdItem.Location = new System.Drawing.Point(0, 21);
            scOrdItem.Name = "scOrdItem";
            scOrdItem.Controls.Add(pnscOrdItem);
            scOrdItemGrf.Collapsible = true;
            scOrdItemGrf.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            scOrdItemGrf.Location = new System.Drawing.Point(0, 21);
            scOrdItemGrf.Name = "scOrdItemGrf";
            scOrdItemGrf.Controls.Add(grfOrdItem);
            sCOrdItem.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCOrdItem.Name = "sCOrdItem";
            sCOrdItem.Dock = System.Windows.Forms.DockStyle.Fill;
            sCOrdItem.Panels.Add(scOrdItem);
            sCOrdItem.Panels.Add(scOrdItemGrf);
            sCOrdItem.HeaderHeight = 20;

            txtItmFlag = new C1TextBox();
            bc.setControlC1TextBox(ref txtItmFlag, fEdit, "txtItmFlag", 20, 0, 0);
            txtItmFlag.Hide();

            lbItmId = new Label();
            lbItmId.Text = "รหัส";
            lbItmId.Font = fEdit;
            lbItmId.Location = new System.Drawing.Point(gapX, 5);
            lbItmId.AutoSize = true;
            lbItmId.Name = "lbItmId";
            txtItmId = new C1TextBox();
            //txtItmId.Font = fEdit;
            //txtItmId.Name = "txtItmId";
            //txtItmId.Location = new System.Drawing.Point(gapColName, lbItmId.Location.Y);
            //txtItmId.Size = new Size(120, 20);      //txtItmRowNo
            bc.setControlC1TextBox(ref txtItmId, fEdit, "txtItmId", 120, gapColName, lbItmId.Location.Y);

            txtItmRowNo = new C1TextBox();
            txtItmRowNo.Font = fEdit;
            txtItmRowNo.Name = "txtItmRowNo";
            txtItmRowNo.Location = new System.Drawing.Point(gapColName, lbItmId.Location.Y);
            txtItmRowNo.Size = new Size(120, 20);      //txtItmRowNo
            txtItmRowNo.Hide();
            gapLine += gapY;
            lbItmName = new Label();
            lbItmName.Text = "ชื่อ";
            lbItmName.Font = fEdit;
            lbItmName.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmName.AutoSize = true;
            lbItmName.Name = "lbItmName";
            txtItmName = new C1TextBox();
            txtItmName.Font = fEdit;
            txtItmName.Name = "txtItmName";
            txtItmName.Location = new System.Drawing.Point(gapColName, lbItmName.Location.Y);
            txtItmName.Size = new Size(300, 20);
            txtItmName.LostFocus += TxtItmName_LostFocus;

            gapLine += gapY;
            lbItmQty = new Label();
            lbItmQty.Text = "QTY";
            lbItmQty.Font = fEdit;
            lbItmQty.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmQty.AutoSize = true;
            lbItmQty.Name = "lbItmQty";
            txtItmQty = new C1TextBox();
            txtItmQty.Font = fEdit;
            txtItmQty.Name = "txtItmQty";
            txtItmQty.Location = new System.Drawing.Point(gapColName, lbItmQty.Location.Y);
            txtItmQty.Size = new Size(120, 20);
            txtItmQty.LostFocus += TxtItmQty_LostFocus;

            gapLine += gapY;
            lbItmFre = new Label();
            lbItmFre.Text = "วิธีใช้";
            lbItmFre.Font = fEdit;
            lbItmFre.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmFre.AutoSize = true;
            lbItmFre.Name = "lbItmFre";
            txtItmFre = new C1TextBox();
            txtItmFre.Font = fEdit;
            txtItmFre.Name = "txtItmFre";
            txtItmFre.Location = new System.Drawing.Point(gapColName, lbItmFre.Location.Y);
            txtItmFre.Size = new Size(300, 20);
            txtItmFre.LostFocus += TxtItmFre_LostFocus;

            gapLine += gapY;
            lbItmIn1 = new Label();
            lbItmIn1.Text = "ข้อควรระวัง1";
            lbItmIn1.Font = fEdit;
            lbItmIn1.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmIn1.AutoSize = true;
            lbItmIn1.Name = "lbItmIn1";
            txtItmIn1 = new C1TextBox();
            txtItmIn1.Font = fEdit;
            txtItmIn1.Name = "txtItmIn1";
            txtItmIn1.Location = new System.Drawing.Point(gapColName, lbItmIn1.Location.Y);
            txtItmIn1.Size = new Size(300, 20);
            txtItmIn1.LostFocus += TxtItmIn1_LostFocus;

            gapLine += gapY;
            lbItmIn2 = new Label();
            lbItmIn2.Text = "ข้อควรระวัง2";
            lbItmIn2.Font = fEdit;
            lbItmIn2.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmIn2.AutoSize = true;
            lbItmIn2.Name = "lbItmIn2";
            txtItmIn2 = new C1TextBox();
            txtItmIn2.Font = fEdit;
            txtItmIn2.Name = "txtItmIn2";
            txtItmIn2.Location = new System.Drawing.Point(gapColName, lbItmIn2.Location.Y);
            txtItmIn2.Size = new Size(300, 20);
            btnItmSend = new C1Button();
            btnItmSend.Text = "ส่งไปห้องยา";
            btnItmSend.Name = "btnItmSend";
            btnItmSend.Location = new Point(txtItmIn2.Width -btnItmSend.Width + 100, txtItmIn2.Location.Y + txtItmIn2.Height + 20);
            btnItmSend.Size = new Size(70, 50);
            btnItmSend.Click += BtnItmSend_Click;

            btnItmDrugSet = new C1Button();
            btnItmDrugSet.Text = "ยา ชุด";
            btnItmDrugSet.Name = "btnItmDrugSet";
            btnItmDrugSet.Location = new Point(btnItmSend.Location.X - btnItmSend.Width - 40, btnItmSend.Location.Y);
            btnItmDrugSet.Size = new Size(70, 50);
            btnItmDrugSet.Click += BtnItmDrugSet_Click;
            btnItmSave = new C1Button();
            btnItmSave.Text = "save";
            btnItmSave.Name = "btnItmSave";
            btnItmSave.Location = new Point(btnItmDrugSet.Location.X - btnItmDrugSet.Width - 40, btnItmSend.Location.Y);
            btnItmSave.Size = new Size(70, 50);
            btnItmSave.Click += BtnItmSave_Click;

            pnscOrdItem.BackColor = this.BackColor;
            pnscOrdItem.Controls.Add(lbItmId);
            pnscOrdItem.Controls.Add(txtItmId); 
            pnscOrdItem.Controls.Add(txtItmRowNo);
            pnscOrdItem.Controls.Add(lbItmName);
            pnscOrdItem.Controls.Add(txtItmName);
            pnscOrdItem.Controls.Add(lbItmQty);
            pnscOrdItem.Controls.Add(txtItmQty);
            pnscOrdItem.Controls.Add(lbItmFre);
            pnscOrdItem.Controls.Add(txtItmFre);
            pnscOrdItem.Controls.Add(lbItmIn1);
            pnscOrdItem.Controls.Add(txtItmIn1);
            pnscOrdItem.Controls.Add(lbItmIn2);
            pnscOrdItem.Controls.Add(txtItmIn2);
            pnscOrdItem.Controls.Add(btnItmSend);
            pnscOrdItem.Controls.Add(btnItmDrugSet);
            pnscOrdItem.Controls.Add(btnItmSave);
            pnscOrdItem.Controls.Add(txtItmFlag);
            scOrdItem.SizeRatio = 30;
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnOrdItem.Controls.Add(sCOrdItem);
            pnOrdItem.BackColor = this.BackColor;

            scOrdItemGrf.ResumeLayout(false);
            scOrdItem.ResumeLayout(false);
            sCOrdItem.ResumeLayout(false);
            scOrdItemGrf.PerformLayout();
            scOrdItem.PerformLayout();
            sCOrdItem.PerformLayout();
            theme1.SetTheme(grfOrdItem, bc.iniC.themeApp);
            //theme1.SetTheme(pnscOrdItem, "Office2016Colorful");
            theme1.SetTheme(pnscOrdItem, "VS2013Purple");
        }

        private void BtnItmSend_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            drawImageMedicalRecord();
        }

        private void ContextMenu_delete_ord_item(object sender, System.EventArgs e)
        {
            grfOrdItem.Rows.Remove(grfOrdItem.Row);
        }
        private void GrfOrdItem_MouseClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            txtItmRowNo.Value = grfOrdItem.Row.ToString();
            txtItmId.Value = grfOrdItem[grfOrdItem.Row, colOrdAddId].ToString();
            txtItmName.Value = grfOrdItem[grfOrdItem.Row, colOrdAddNameT].ToString();
            txtItmFre.Value = grfOrdItem[grfOrdItem.Row, colOrdAddDrugFr].ToString();
            txtItmIn1.Value = grfOrdItem[grfOrdItem.Row, colOrdAddDrugIn].ToString();
            txtItmQty.Value = grfOrdItem[grfOrdItem.Row, colOrdAddQty].ToString();
        }

        private void TxtItmIn1_LostFocus(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int row = 0;
            if (int.TryParse(txtItmRowNo.Text, out row))
            {
                if (grfOrdItem.Rows.Count == row)
                {
                    grfOrdItem[row - 1, colOrdAddDrugIn] = txtItmIn1.Text.Trim();
                }
                else
                {
                    grfOrdItem[row, colOrdAddDrugIn] = txtItmIn1.Text.Trim();
                }
            }
        }

        private void TxtItmFre_LostFocus(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int row = 0;
            if (int.TryParse(txtItmRowNo.Text, out row))
            {
                
                    if (grfOrdItem.Rows.Count == row)
                    {
                        grfOrdItem[row-1, colOrdAddDrugFr] = txtItmFre.Text.Trim();
                    }
                    else
                    {
                        grfOrdItem[row, colOrdAddDrugFr] = txtItmFre.Text.Trim();
                    }
            }
        }

        private void TxtItmQty_LostFocus(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int row = 0;
            if (int.TryParse(txtItmRowNo.Text, out row))
            {
                //if (grfOrdItem.Rows.Count == 2)
                //{
                //    grfOrdItem[row-1, colOrdAddQty] = txtItmQty.Text.Trim();
                //}
                //else
                //{
                    if (grfOrdItem.Rows.Count == row)
                    {
                        grfOrdItem[row-1, colOrdAddQty] = txtItmQty.Text.Trim();
                    }
                    else
                    {
                        grfOrdItem[row, colOrdAddQty] = txtItmQty.Text.Trim();
                    }
                //}
            }
        }

        private void TxtItmName_LostFocus(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int row = 0;
            if(int.TryParse(txtItmRowNo.Text, out row))
            {
                if (grfOrdItem.Rows.Count == 2)
                {
                    grfOrdItem[row - 1, colOrdAddNameT] = txtItmName.Text.Trim();
                }
                else
                {
                    grfOrdItem[row, colOrdAddNameT] = txtItmName.Text.Trim();
                }
            }
        }
        private PharmacyT01 setPharmacyT01(String hn, string hnyear,String vsDate, String preno)
        {
            PharmacyT01 phart01 = new PharmacyT01();
            phart01.MNC_DOC_CD = "ROS";
            phart01.MNC_REQ_NO = "";
            phart01.MNC_REQ_YR = (DateTime.Now.Year + 543).ToString();
            phart01.MNC_REQ_DAT = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd");
            phart01.MNC_HN_NO = hn;
            phart01.MNC_HN_YR = hnyear;
            phart01.MNC_AN_NO = "";
            phart01.MNC_AN_YR = "";
            phart01.MNC_BD_NO = "";
            phart01.MNC_CAL_NO = "";
            phart01.MNC_CANCEL_STS = "";
            phart01.MNC_CFM_DOT = "";
            phart01.MNC_COM_CD = "";
            phart01.MNC_DATE = vsDate;
            phart01.MNC_DEPC_NO = "";
            phart01.MNC_DEP_NO = "";
            phart01.MNC_DOC_CD = "";
            phart01.MNC_DOT_CD = bc.user.username;
            phart01.MNC_EMPC_CD = "";
            phart01.MNC_EMPR_CD = bc.user.username;
            phart01.MNC_FN_TYP_CD = "";
            phart01.MNC_ORD_DOT = "";
            phart01.MNC_PAC_CD = "";
            phart01.MNC_PAC_TYP = "";
            phart01.MNC_PHA_STS = "";
            phart01.MNC_PH_REM = "";
            phart01.MNC_PRE_NO = preno;
            phart01.MNC_PRE_SEQ = "";
            phart01.MNC_REQ_COUNT = "0";
            phart01.MNC_REQ_STS = "";
            phart01.MNC_REQ_TIM = "";
            phart01.MNC_REQ_TYP = "";
            phart01.MNC_RM_NAM = "";
            phart01.MNC_SECC_NO = "";
            phart01.MNC_SEC_NO = "";
            phart01.MNC_SUM_COS = "1";
            phart01.MNC_SUM_PRI = "1";
            phart01.MNC_STAMP_DAT = "";
            phart01.MNC_STAMP_TIM = "";
            phart01.MNC_TIME = "";
            phart01.MNC_USE_LOG = "";
            phart01.MNC_USR_ADD = bc.user.username;
            phart01.MNC_USR_UPD = bc.user.username;
            phart01.MNC_WD_NO = "";

            return phart01;
        }
        private LabT01 setLabT01(String hn, string hnyear, String vsDate, String preno)
        {
            LabT01 labT01 = new LabT01();
            labT01.MNC_REQ_YR = (DateTime.Now.Year + 543).ToString();
            labT01.MNC_REQ_NO = "";
            labT01.MNC_REQ_DAT = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd");
            labT01.MNC_REQ_DEP = "";
            labT01.MNC_REQ_STS = "";
            labT01.MNC_REQ_TIM = "";
            labT01.MNC_HN_YR = hnyear;
            labT01.MNC_HN_NO = hn;
            labT01.MNC_AN_YR = "";
            labT01.MNC_AN_NO = "";
            labT01.MNC_PRE_NO = preno;
            labT01.MNC_DATE = vsDate;
            labT01.MNC_TIME = "";
            labT01.MNC_DOT_CD = bc.user.username;
            labT01.MNC_WD_NO = "";
            labT01.MNC_RM_NAM = "";
            labT01.MNC_BD_NO = "";
            labT01.MNC_FN_TYP_CD = "";
            labT01.MNC_COM_CD = "";
            labT01.MNC_REM = "";
            labT01.MNC_LB_STS = "";
            labT01.MNC_CAL_NO = "";
            labT01.MNC_EMPR_CD = "";
            labT01.MNC_EMPC_CD = "";
            labT01.MNC_ORD_DOT = "";
            labT01.MNC_CFM_DOT = "";
            labT01.MNC_DOC_YR = "";
            labT01.MNC_DOC_NO = "";
            labT01.MNC_DOC_DAT = "";
            labT01.MNC_DOC_CD = "";
            labT01.MNC_SPC_SEND_DAT = "";
            labT01.MNC_SPC_SEND_TM = "";
            labT01.MNC_SPC_TYP = "";
            labT01.MNC_REMARK = "";
            labT01.MNC_STAMP_DAT = "";
            labT01.MNC_STAMP_TIM = "";
            labT01.MNC_CANCEL_STS = "";
            labT01.MNC_PAC_CD = "";
            labT01.MNC_PAC_TYP = "";
            labT01.MNC_DANGER_FLG = "";
            labT01.MNC_DIET_FLG = "";
            labT01.MNC_MED_FLG = "";
            labT01.MNC_LAB_FN_TYP_CD = "";
            labT01.MNC_IP_ADD1 = "";
            labT01.MNC_IP_ADD2 = "";
            labT01.MNC_IP_ADD3 = "";
            labT01.MNC_IP_ADD4 = "";
            labT01.MNC_PATNAME = "";
            labT01.MNC_LOAD_STS = "";
            labT01.MNC_IP_REC = "";

            return labT01;
        }
        private XrayT01 setXrayT01(String hn, string hnyear, String vsDate, String preno)
        {
            XrayT01 xrayT01 = new XrayT01();
            xrayT01.MNC_REQ_YR = (DateTime.Now.Year + 543).ToString();
            xrayT01.MNC_REQ_NO = "";
            xrayT01.MNC_REQ_DAT = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd");
            xrayT01.MNC_REQ_DEP = "";
            xrayT01.MNC_REQ_STS = "";
            xrayT01.MNC_REQ_TIM = "";
            xrayT01.MNC_HN_YR = hnyear;
            xrayT01.MNC_HN_NO = hn;
            xrayT01.MNC_AN_YR = "";
            xrayT01.MNC_AN_NO = "";
            xrayT01.MNC_PRE_NO = preno;
            xrayT01.MNC_DATE = vsDate;
            xrayT01.MNC_TIME = "";
            xrayT01.MNC_DOT_CD = "";
            xrayT01.MNC_WD_NO = "";
            xrayT01.MNC_RM_NAM = "";
            xrayT01.MNC_BD_NO = "";
            xrayT01.MNC_FN_TYP_CD = "";
            xrayT01.MNC_COM_CD = "";
            xrayT01.MNC_REM = "";
            xrayT01.MNC_XR_STS = "";
            xrayT01.MNC_CAL_NO = "";
            xrayT01.MNC_EMPR_CD = bc.user.username;
            xrayT01.MNC_EMPC_CD = bc.user.username;
            xrayT01.MNC_ORD_DOT = "";
            xrayT01.MNC_CFM_DOT = "";
            xrayT01.MNC_DOC_YR = "";
            xrayT01.MNC_DOC_NO = "";
            xrayT01.MNC_DOC_DAT = "";
            xrayT01.MNC_DOC_CD = "";
            xrayT01.MNC_STAMP_DAT = "";
            xrayT01.MNC_STAMP_TIM = "";
            xrayT01.MNC_CANCEL_STS = "";
            xrayT01.MNC_PAC_CD = "";
            xrayT01.MNC_PAC_TYP = "";
            xrayT01.status_pacs = "";
            return xrayT01;
        }
        private void savePharT01()
        {
            PharmacyT01 phart01 = new PharmacyT01();
            XrayT01 xrayT01 = new XrayT01();
            LabT01 labT01 = new LabT01();
            //new LogWriter("d", "PharmacyT02 insertPharmacyT01 hn " + phart01.MNC_HN_NO+" hnyr "+ phart01.MNC_HN_YR+" date "+ phart01.MNC_DATE+" preno "+ phart01.MNC_PRE_NO+ "select * from PHARMACY_T01 where MNC_HN_NO = '" + phart01.MNC_HN_NO + "' and mnc_req_dat = '"+ vsDate + "' and MNC_PRE_NO = '" + phart01.MNC_PRE_NO + "'");
            //หาก่อนว่า มี drug lab xray หรือไม่
            String itmflag1 = "", flagDrug="", flagLab="", flagXray="", reDrug="", reLab="", reXray="";

            foreach (Row rowdrug in grfOrdItem.Rows)
            {
                if (rowdrug[colOrdAddId] == null) continue;
                itmflag1 = rowdrug[colOrdAddFlag].ToString();
                if((itmflag1.Equals("P")) || (itmflag1.Equals("O")))
                {
                    flagDrug = "1";
                }
                if (itmflag1.Equals("X"))
                {
                    flagXray = "1";
                }
                if (itmflag1.Equals("L"))
                {
                    flagLab = "1";
                }
            }
            if (flagDrug.Equals("1"))
            {
                phart01 = setPharmacyT01(ptt.Hn, ptt.hnyr, vsDate, preno);
                reDrug = bc.bcDB.pharT01DB.insertPharmacyT01(phart01);
            }
            if (flagXray.Equals("1"))
            {
                xrayT01 = setXrayT01(ptt.Hn, ptt.hnyr, vsDate, preno);
                reXray = bc.bcDB.xrayT01DB.insertXrayT01(xrayT01);
            }
            if (flagLab.Equals("1"))
            {
                labT01 = setLabT01(ptt.Hn, ptt.hnyr, vsDate, preno);
                reLab = bc.bcDB.labT01DB.insertLabT01(labT01);
            }
            //new LogWriter("d", "PharmacyT02 insertPharmacyT01 re " + re);
            int chk = 0;
            if(int.TryParse(reDrug, out chk))
            {
                bc.bcDB.pharT02DB.deleteReqNo(phart01.MNC_REQ_YR, reDrug);
                bc.bcDB.xrayT02DB.deleteReqNo(xrayT01.MNC_REQ_YR, reXray);
                bc.bcDB.labT02DB.deleteReqNo(labT01.MNC_REQ_YR, reLab);
                foreach (Row rowdrug in grfOrdItem.Rows)
                {
                    String itmcode = "", drugFr = "", drugIn = "", untcode = "", qty = "", itmflag="";
                    if (rowdrug[colOrdAddId] == null) continue;
                    itmcode = rowdrug[colOrdAddId].ToString();
                    drugFr = rowdrug[colOrdAddDrugFr].ToString();
                    drugIn = rowdrug[colOrdAddDrugIn].ToString();
                    untcode = rowdrug[colOrdAddUnit] !=null ? rowdrug[colOrdAddUnit].ToString() : "";
                    qty = rowdrug[colOrdAddQty].ToString();
                    itmflag = rowdrug[colOrdAddFlag].ToString();
                    if ((itmflag.Equals("P")) || (itmflag.Equals("O")))
                    {
                        PharmacyT02 phart02 = new PharmacyT02();
                        phart02.MNC_CANCEL_STS = "";
                        phart02.MNC_DOC_CD = "ROS";
                        phart02.MNC_FN_CD = "";
                        phart02.MNC_ORD_NO = "";
                        phart02.MNC_PAY_FLAG = "";
                        phart02.MNC_PHA_HID = "";
                        phart02.MNC_PH_CAU = drugIn;
                        phart02.MNC_PH_CD = itmcode;
                        phart02.MNC_PH_COS = "";
                        phart02.MNC_PH_DIR_CD = "";
                        phart02.MNC_PH_DIR_DSC = drugFr;
                        phart02.MNC_PH_DIR_TXT = "";
                        phart02.MNC_PH_FLG = "";
                        phart02.MNC_PH_FRE_CD = "";
                        phart02.MNC_PH_IND = "";
                        phart02.MNC_PH_PRI = "0";
                        phart02.MNC_PH_QTY = qty;
                        phart02.MNC_PH_REM = "";
                        phart02.MNC_PH_RFN = "";
                        phart02.MNC_PH_STS = "";
                        phart02.MNC_PH_TIM_CD = "";
                        phart02.MNC_PH_UNTF_QTY = "";
                        phart02.MNC_PH_UNT_CD = untcode;
                        phart02.MNC_REQ_NO = reDrug;
                        phart02.MNC_REQ_YR = phart01.MNC_REQ_YR;
                        phart02.MNC_STAMP_DAT = "";
                        phart02.MNC_STAMP_TIM = "";
                        phart02.MNC_SUP_STS = "";
                        phart02.MNC_USR_ADD = bc.user.username;
                        phart02.MNC_USR_UPD = bc.user.username;

                        bc.bcDB.pharT02DB.insertPharmacyT02(phart02, "");
                    }
                    else if (itmflag.Equals("L"))
                    {
                        LabT02 labT02 = new LabT02();
                        labT02.MNC_REQ_YR = "";
                        labT02.MNC_REQ_NO = "";
                        labT02.MNC_REQ_DAT = "";
                        labT02.MNC_LB_CD = "";
                        labT02.MNC_REQ_STS = "";
                        labT02.MNC_LB_RMK = "";
                        labT02.MNC_LB_COS = "";
                        labT02.MNC_LB_PRI = "";
                        labT02.MNC_LB_RFN = "";
                        labT02.MNC_SPC_SEND_DAT = "";
                        labT02.MNC_SPC_SEND_TM = "";
                        labT02.MNC_SPC_TYP = "";
                        labT02.MNC_RESULT_DAT = "";
                        labT02.MNC_RESULT_TIM = "";
                        labT02.MNC_STAMP_DAT = "";
                        labT02.MNC_STAMP_TIM = "";
                        labT02.MNC_USR_RESULT = "";
                        labT02.MNC_USR_RESULT_REPORT = "";
                        labT02.MNC_USR_RESULT_APPROVE = "";
                        labT02.MNC_CANCEL_STS = "";
                        labT02.MNC_USR_UPD = "";
                        labT02.MNC_SND_OUT_STS = "";
                        labT02.MNC_LB_STS = "";
                        bc.bcDB.labT02DB.insertLabT02(labT02,"");
                    }
                    else if (itmflag.Equals("X"))
                    {
                        XrayT02 xrayt02 = new XrayT02();
                        xrayt02.MNC_REQ_YR = "";
                        xrayt02.MNC_REQ_NO = "";
                        xrayt02.MNC_REQ_DAT = "";
                        xrayt02.MNC_REQ_STS = "";
                        xrayt02.MNC_XR_CD = "";
                        xrayt02.MNC_XR_RMK = "";
                        xrayt02.MNC_XR_COS = "";
                        xrayt02.MNC_XR_PRI = "";
                        xrayt02.MNC_XR_RFN = "";
                        xrayt02.MNC_XR_PRI_R = "";
                        xrayt02.MNC_FLG_K = "";
                        xrayt02.MNC_STAMP_DAT = "";
                        xrayt02.MNC_STAMP_TIM = "";
                        xrayt02.MNC_XR_COS_R = "";
                        xrayt02.MNC_DOT_CD_DF = "";
                        xrayt02.MNC_DOT_GRP_CD = "";
                        xrayt02.MNC_ACT_DAT = "";
                        xrayt02.MNC_ACT_TIM = "";
                        xrayt02.MNC_CANCEL_STS = "";
                        xrayt02.MNC_USR_UPD = "";
                        xrayt02.MNC_SND_OUT_STS = "";
                        xrayt02.MNC_XR_STS = "";
                        xrayt02.status_pacs = "";
                        bc.bcDB.xrayT02DB.insertXrayT02(xrayt02, "");
                    }
                }
            }
        }
        private void drawImageMedicalRecord()
        {
            String filename = "";
            //filename = txtHn.Text.Trim()+"_"+
            //if (File.Exists("medicalrecord.jpg"))
            //new LogWriter("d", "FrmScanView1 drawImageMedicalRecord 00 start");
            {
                int gapLine = 18, gapX = 15, gapY = 10, col1=130, col11=300, col12 = 400, col2=530, temp=0,col3=1000, col31=1200, col4=1338, col5=1320, col6=1570;
                String mlfm = "", pathFolder = "", datetick = "";
                C1PictureBox pic = new C1PictureBox();
                C1PdfDocument _c1pdf;
                C1PdfDocumentSource pdf = new C1PdfDocumentSource();
                MemoryStream streamCC, streamME, streamDiag, streamCC1 = new MemoryStream(), streamME1 = new MemoryStream(), streamDiag1 = new MemoryStream();
                FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                DocScan docCC = new DocScan();
                DocScan docME = new DocScan();
                DocScan docDiag = new DocScan();

                //mlfm = "FM-MED-900";
                //mlfm = "FM-MED-901";
                //mlfm = "FM-MED-902";
                datetick = DateTime.Now.Ticks.ToString();
                pathFolder = bc.iniC.medicalrecordexportpath + "\\";
                if (!Directory.Exists(pathFolder))
                {
                    Directory.CreateDirectory(pathFolder);
                }

                docCC = bc.bcDB.dscDB.selectByStatusMedicalExamination(hn, "FM-MED-900", vsDate, preno);    //cc
                docME = bc.bcDB.dscDB.selectByStatusMedicalExamination(hn, "FM-MED-901", vsDate, preno);
                docDiag = bc.bcDB.dscDB.selectByStatusMedicalExamination(hn, "FM-MED-902", vsDate, preno);

                Visit vs = new Visit();
                Pen pen = new Pen(Color.Black);
                pen.Width = 2;
                vs = bc.bcDB.vsDB.selectVisit(txtHn.Text.Trim(), vsDate, preno);
                //new LogWriter("d", "FrmScanView1 drawImageMedicalRecord 01");
                Image img = Image.FromFile("medicalrecord.jpg");
                Graphics graphicImage = Graphics.FromImage(img);
                Font font = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
                Font fontB = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Bold);
                Font font5 = new Font(bc.iniC.pdfFontName, 14+24, FontStyle.Bold);
                int height = 20, branch=0;

                _c1pdf = new C1.C1Pdf.C1PdfDocument();
                _c1pdf.DocumentInfo.Producer = "C1Pdf";
                _c1pdf.Security.AllowCopyContent = true;
                _c1pdf.Security.AllowEditAnnotations = true;
                _c1pdf.Security.AllowEditContent = true;
                _c1pdf.Security.AllowPrint = true;
                _c1pdf.DocumentInfo.Title = " ";

                streamCC = ftp.download(bc.iniC.folderFTP + "//" + docCC.image_path);
                streamME = ftp.download(bc.iniC.folderFTP + "//" + docME.image_path);
                streamDiag = ftp.download(bc.iniC.folderFTP + "//" + docDiag.image_path);
                streamCC.Position = 0;
                streamDiag.Position = 0;
                streamME.Position = 0;
                StreamReader reader;
                RectangleF rc;
                Image impCC =null, impME = null, impDiag = null;
                String txt = "";
                if (streamCC.Length > 0)
                {
                    reader = new StreamReader(streamCC, System.Text.Encoding.UTF8, true);
                    reader.BaseStream.Position = 0;
                    rc = new RectangleF(0, 0, 200, 200);
                    var exporter = pdf.SupportedExportProviders[4].NewExporter();
                    exporter.ShowOptions = false;
                    filename = txtHn.Text.Trim() + "_cc_" + preno;
                    exporter.FileName = pathFolder + datetick + "_" + filename;

                    _c1pdf.FontType = FontTypeEnum.Embedded;
                    _c1pdf.DrawStringRtf(reader.ReadToEnd(), font, Brushes.White, rc);
                    _c1pdf.SaveAllImagesAsJpeg = true;
                    _c1pdf.Save(streamCC1);
                    //_c1pdf.Save(pathFolder + datetick + "_cc.pdf");
                    //_c1pdf.Save("aaaaa.pdf");
                    streamCC1.Position = 0;
                    pdf.LoadFromStream(streamCC1);
                    //pdf.LoadFromFile(pathFolder + datetick + "_cc.pdf");
                    pdf.Export(exporter);


                    //RichTextBox rtf = new RichTextBox();
                    //reader.BaseStream.Position = 0;
                    //rtf.LoadFile(reader.ReadToEnd(), RichTextBoxStreamType.RichText);
                    //rtf.Size = new Size(bc.imageCC_width, bc.imageCC_Height);
                    //rtf.Location = new Point(0, 0);
                    //rtf.SaveFile("", RichTextBoxStreamType.)








                    Application.DoEvents();
                    filename = filename + "_page1.jpg";
                    impCC = Image.FromFile(pathFolder + datetick + "_" + filename);
                    Bitmap bmpImage = new Bitmap(impCC);
                    bmpImage = bmpImage.Clone(new Rectangle(0, 0, bc.imageCC_width, bc.imageCC_Height), bmpImage.PixelFormat);
                    impCC = bmpImage;
                    txt = "Chief Compliant";
                    Size size11 = bc.MeasureString(txt, font);
                    Graphics graphicImageCC = Graphics.FromImage(impCC);
                    
                    RectangleF recf11 = new RectangleF(0, 0, size11.Width, size11.Height);
                    graphicImageCC.DrawString(txt, font, SystemBrushes.WindowText, recf11);
                    
                    
                    if (streamME.Length > 0)
                    {
                        reader = new StreamReader(streamME, System.Text.Encoding.UTF8, true);
                        reader.BaseStream.Position = 0;
                        filename = txtHn.Text.Trim() + "_me_" + preno;
                        exporter.FileName = pathFolder + datetick + "_" + filename;
                        _c1pdf.Clear();
                        _c1pdf.DrawStringRtf(reader.ReadToEnd(), font, Brushes.White, rc);
                        _c1pdf.Save(streamME1);
                        streamME1.Position = 0;
                        pdf.LoadFromStream(streamME1);
                        pdf.Export(exporter);
                        Application.DoEvents();
                        filename = filename + "_page1.jpg";
                        impME = Image.FromFile(pathFolder + datetick + "_" + filename);
                        bmpImage = new Bitmap(impME);
                        bmpImage = bmpImage.Clone(new Rectangle(0, 0, bc.imageME_width, bc.imageME_Height+600), bmpImage.PixelFormat);
                        bmpImage = bc.ResizeImage(bmpImage, bc.imageME_width, bc.imageME_Height);
                        impME = bmpImage;
                        txt = "Physical Exam";
                        Size size1 = bc.MeasureString(txt, font);
                        Graphics graphicImageME = Graphics.FromImage(impME);
                        RectangleF recf1 = new RectangleF(0, 0, size1.Width, size1.Height);
                        graphicImageME.DrawString(txt, font, SystemBrushes.WindowText, recf1);
                    }
                    if (streamDiag.Length > 0)
                    {
                        reader = new StreamReader(streamDiag, System.Text.Encoding.UTF8, true);
                        reader.BaseStream.Position = 0;
                        filename = txtHn.Text.Trim() + "_diag_" + preno;
                        exporter.FileName = pathFolder + datetick + "_" + filename;
                        _c1pdf.Clear();
                        _c1pdf.DrawStringRtf(reader.ReadToEnd(), font, Brushes.White, rc);
                        _c1pdf.Save(streamDiag1);
                        streamDiag1.Position = 0;
                        pdf.LoadFromStream(streamDiag1);
                        pdf.Export(exporter);
                        Application.DoEvents();
                        filename = filename + "_page1.jpg";
                        impDiag = Image.FromFile(pathFolder + datetick + "_" + filename);
                        bmpImage = new Bitmap(impDiag);
                        bmpImage = bmpImage.Clone(new Rectangle(0, 0, bc.imageDiag_width, bc.imageDiag_Height), bmpImage.PixelFormat);
                        impDiag = bmpImage;
                        txt = "Diagnosis";
                        Size size1 = bc.MeasureString(txt, font);
                        Graphics graphicImageME = Graphics.FromImage(impDiag);
                        RectangleF recf1 = new RectangleF(0, 0, size1.Width, size1.Height);
                        graphicImageME.DrawString(txt, font, SystemBrushes.WindowText, recf1);
                    }
                }
                gapLine = int.Parse(font.Size.ToString())+4;
                txt = "HN " + txtHn.Text.Trim() + " ชื่อ-นามสกุล " + txtName.Text.Trim();
                Size size = TextRenderer.MeasureText(txt, font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.SingleLine | TextFormatFlags.NoClipping | TextFormatFlags.PreserveGraphicsClipping);
                RectangleF recf = new RectangleF(0, 0, size.Width, size.Height);
                graphicImage.DrawString("0,0 " + font.Name+" "+ font.Size, font, SystemBrushes.WindowText, recf);
                gapY += gapLine;
                
                txt = int.Parse(bc.iniC.branchId).ToString();
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(470, gapY-12, size.Width+10, size.Height+20);
                graphicImage.DrawString(txt, font5, SystemBrushes.WindowText, recf);
                recf = new RectangleF(col4, gapY-12, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font5, SystemBrushes.WindowText, recf);

                gapY += gapLine;
                txt = "H.N. "+txtHn.Text.Trim() +"     V.N. "+txtVN.Text.Trim();
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col2, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                recf = new RectangleF(col5, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);

                gapY += gapLine;
                txt = vs.PaidName;
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col1, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                recf = new RectangleF(col3, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = "ชื่อ "+txtName.Text;
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col2, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                recf = new RectangleF(col5, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);

                //gapY += gapLine;
                gapY += gapLine;
                
                txt = "เลขที่บัตร " + vs.ID;
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col2, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                recf = new RectangleF(col5, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);

                gapY += gapLine;
                txt = "โรคประจำตัว";
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col1, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);

                Rectangle rec = new Rectangle(col11, gapY+3, 20, 20);
                graphicImage.DrawRectangle(pen, rec);
                txt = "ไม่มี";
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col11 + 25, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = "อายุ " + ptt.AgeString() + " (" + bc.datetoShow(ptt.patient_birthday) + ")";
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col2, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                recf = new RectangleF(col5, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);

                gapY += gapLine;
                rec = new Rectangle(col11, gapY + 3, 20, 20);
                graphicImage.DrawRectangle(pen, rec);
                txt = "มีโรค  ระบุ";
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col11 + 25, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = "วันที่เวลา " + System.DateTime.Now.ToString("dd/MM/yyyy hh:MM");
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col2, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                recf = new RectangleF(col5, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);

                gapY += gapLine;
                txt = "โรคเรื้อรัง";
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col1, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);

                temp = size.Width;
                txt = lbChronic1.Text.Replace("Chronic : ","");
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col1 + temp, gapY, size.Width + temp + 10, size.Height);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                gapY += gapLine;
                txt = "ชื่อแพทย์ " + bc.user.fullname + "("+bc.user.username+")";
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col1, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                recf = new RectangleF(col3, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);

                //gapY += gapLine;
                txt = "DR Time.                        ปิดใบยา";
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col2, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);

                gapY += gapLine;
                txt = lbPttTemp.Text;
                //size = bc.MeasureString(txt, font);
                recf = new RectangleF(col1, gapY, 120, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = lbPttHrate.Text;
                recf = new RectangleF(col1 + 120, gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = lbPttLRate.Text;
                recf = new RectangleF(col1 + (120*2), gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = lbPttBp1.Text;
                recf = new RectangleF(col1 + (120*3), gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = "Time ";
                recf = new RectangleF(col1 + (120 * 4), gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = lbPttBp2.Text;
                recf = new RectangleF(col1 + (120 * 5), gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = "Time ";
                recf = new RectangleF(col1 + (120 * 6), gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                gapY += gapLine;
                txt = lbPttWeight.Text;
                recf = new RectangleF(col1, gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = lbPttHigh.Text;
                recf = new RectangleF(col1+120, gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = "BMI ";
                recf = new RectangleF(col1 + (120 * 2), gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = lbPttCC.Text;
                recf = new RectangleF(col1 + (120 * 3), gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = lbPttCCin.Text;
                recf = new RectangleF(col1 + (120 * 4), gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = lbPttCCex.Text;
                recf = new RectangleF(col1 + (120 * 5), gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = "Ab.C ";
                recf = new RectangleF(col1 + 650, gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                txt = "H.C. ";
                recf = new RectangleF(col1 + 750, gapY, 100, 30);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                //gapY += gapLine;
                //size = bc.MeasureString(txt, font);
                //recf = new RectangleF(gapX, gapY, size.Width, height);
                //graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                gapY += gapLine;
                txt = "แพ้ยา/อาหาร/อื่นๆ";
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col1, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                recf = new RectangleF(col3, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                rec = new Rectangle(col11, gapY + 3, 20, 20);
                graphicImage.DrawRectangle(pen, rec);
                rec = new Rectangle(col31, gapY + 3, 20, 20);
                graphicImage.DrawRectangle(pen, rec);
                txt = "ไม่มี";
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col11 + 25, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                recf = new RectangleF(col31 + 25, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                gapY += gapLine;
                rec = new Rectangle(col11, gapY + 3, 20, 20);
                graphicImage.DrawRectangle(pen, rec);
                rec = new Rectangle(col31, gapY + 3, 20, 20);
                graphicImage.DrawRectangle(pen, rec);
                txt = "มี ระบุอาการ";
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col11 + 25, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                recf = new RectangleF(col31 + 25, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);

                gapY += gapLine;
                txt = "อาการเบื้องต้น "+vs.symptom;
                size = bc.MeasureString(txt, font);
                recf = new RectangleF(col1, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                recf = new RectangleF(col3, gapY, size.Width + 10, size.Height + 20);
                graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                gapY += gapLine;
                gapY += gapLine;
                temp = gapY;
                recf = new RectangleF(col1 - 20, gapY, bc.imageCC_width, bc.imageCC_Height);
                if (impCC != null)
                {
                    graphicImage.DrawImage(impCC, recf);
                }
                gapY += (gapLine + (impCC != null ? impCC.Height : 0));
                recf = new RectangleF(col1 - 21, gapY, bc.imageME_width, bc.imageME_Height);
                if (impME != null)
                {
                    graphicImage.DrawImage(impME, recf);
                }
                gapY += (gapLine + (impME != null ? impME.Height : 0));
                recf = new RectangleF(col1 - 21, gapY, bc.imageDiag_width, bc.imageDiag_Height);
                if (impDiag != null)
                {
                    graphicImage.DrawImage(impDiag, recf);
                }
                //gapY += gapLine;
                int i = 1;
                //new LogWriter("d", "FrmScanView1 drawImageMedicalRecord 99");
                gapLine = 30;
                gapY = temp;
                foreach (Row row in grfOrdItem.Rows)
                {
                    if (row[colOrdAddId] == null) continue;
                    gapY += gapLine;
                    txt = i+". "+ row[colOrdAddId].ToString()+" "+ row[colOrdAddNameT].ToString();
                    recf = new RectangleF(col3, gapY, 800, 30);
                    graphicImage.DrawString(txt, fontB, SystemBrushes.WindowText, recf);

                    txt = row[colOrdAddQty].ToString()+" "+ row[colOrdAddUnit].ToString();
                    recf = new RectangleF(col6, gapY, 200, 30);
                    graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                    gapY += gapLine;
                    //txt = row[colOrdAddDrugFr].ToString() + " " + row[colOrdAddDrugIn].ToString();
                    txt = row[colOrdAddDrugFr].ToString();
                    recf = new RectangleF(col3+10, gapY, 750, 30);
                    graphicImage.DrawString(txt, font, SystemBrushes.WindowText, recf);
                    
                    i++;
                }
                img.Save(pathFolder + datetick +"_" + txtHn.Text.Trim() +"_"+ vsDate + "_" + preno + "medicalrecord_1.jpg");
            }
            
        }
        private void BtnItmSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            MessageBox.Show("BtnItmSave_Click  preno "+preno +" hnyr " + ptt.hnyr, "");

            savePharT01();
        }
        private void initGrfOrdDrug()
        {
            grfOrdDrug = new C1FlexGrid();
            grfOrdDrug.Font = fEdit;
            grfOrdDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOrdDrug.Location = new System.Drawing.Point(0, 0);
            grfOrdDrug.Rows.Count = 1;
            grfOrdDrug.DoubleClick += GrfOrdDrug_DoubleClick;
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnOrdSearchDrug.Controls.Add(grfOrdDrug);

            theme1.SetTheme(grfOrdDrug, "Office2010Red");
        }
        private void GrfOrdDrug_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setOrdDrugItem();
        }
        private void setOrdDrugItem()
        {
            if (grfOrdDrug == null) return;
            if (grfOrdDrug.Row <= 1) return;
            if (grfOrdDrug.Col <= 0) return;
            String code = "";
            DataTable dt = new DataTable();
            code = grfOrdDrug[grfOrdDrug.Row, colOrdDrugId].ToString();
            dt = bc.bcDB.drugDB.selectDrugByCode(code);
            Row rowdrug = grfOrdItem.Rows.Add();
            rowdrug[colOrdAddId] = dt.Rows[0]["MNC_ph_cd"].ToString();
            rowdrug[colOrdAddNameT] = dt.Rows[0]["MNC_ph_tn"].ToString();
            rowdrug[colOrdAddItemType] = dt.Rows[0]["mnc_ph_typ_cd"].ToString();
            rowdrug[colOrdAddUnit] = dt.Rows[0]["mnc_ph_unt_cd"].ToString();
            rowdrug[colOrdAddDrugFr] = dt.Rows[0]["MNC_ph_dir_dsc"].ToString();
            rowdrug[colOrdAddDrugIn] = dt.Rows[0]["MNC_ph_cau_dsc"].ToString();
            rowdrug[colOrdAddQty] = "";
            rowdrug[colOrdAddFlag] = dt.Rows[0]["mnc_ph_typ_flg"].ToString();       //drug=P,supp=O,lab=xray=
            rowdrug[0] = (grfOrdItem.Rows.Count-1);
            //rowdrug.StyleDisplay.BackColor = Color.FromArgb(143, 200, 127);
            rowdrug.StyleNew.BackColor = Color.FromArgb(253, 233, 233);

            //C1TextBox txtItmId = (C1TextBox)this.Controls["txtItmId"];
            //C1TextBox lbItmName = (C1TextBox)this.Controls["lbItmName"];
            txtItmRowNo.Value = grfOrdItem.Rows.Count;
            txtItmId.Value = dt.Rows[0]["MNC_ph_cd"].ToString();
            txtItmName.Value = dt.Rows[0]["MNC_ph_tn"].ToString();
            txtItmFre.Value = dt.Rows[0]["MNC_ph_dir_dsc"].ToString();
            txtItmIn1.Value = dt.Rows[0]["MNC_ph_cau_dsc"].ToString();
            txtItmQty.Value = "1";
            txtItmFlag.Value = dt.Rows[0]["mnc_ph_typ_flg"].ToString();
            txtItmFre.Show();
            txtItmIn1.Show();
            txtItmIn2.Show();
        }
        private void setGrfOrdItem()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.pharT02DB.selectReq(ptt.hnyr, txtHn.Text.Trim(), vsDate, preno);
            grfOrdItem.Rows.Count = 1;
            grfOrdItem.Rows.Count = dt.Rows.Count+1;
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                grfOrdItem[i, colOrdAddId] = row1["MNC_ph_cd"].ToString();
                grfOrdItem[i, colOrdAddNameT] = row1["mnc_ph_tn"].ToString();
                grfOrdItem[i, colOrdAddItemType] = row1["MNC_ph_cd"].ToString();
                grfOrdItem[i, colOrdAddUnit] = row1["mnc_ph_unt_cd"].ToString();
                grfOrdItem[i, colOrdAddDrugFr] = row1["mnc_ph_dir_dsc"].ToString();
                grfOrdItem[i, colOrdAddDrugIn] = row1["MNC_PH_CAU_dsc"].ToString();
                grfOrdItem[i, colOrdAddQty] = row1["MNC_ph_qty"].ToString();
                grfOrdItem[i, colOrdAddFlag] = row1["mnc_ph_typ_flg"].ToString();      //drug=P,supp=O,lab, xray
                grfOrdItem[i, 0] = i;
                if (row1["mnc_ph_typ_flg"].ToString().Equals("P"))
                {
                    grfOrdItem.Rows[i].StyleDisplay.BackColor = Color.FromArgb(143, 200, 127);
                }
                else if (row1["mnc_ph_typ_flg"].ToString().Equals("O"))
                {
                    grfOrdItem.Rows[i].StyleDisplay.BackColor = Color.FromArgb(244, 222, 242);
                }
            }
        }
        private void setGrfOrdDrug()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.drugDB.selectDrugAll();

            grfOrdDrug.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfOrdDrug.Rows.Count = dt.Rows.Count + 1;
            grfOrdDrug.Cols.Count = dt.Columns.Count + 1;
            grfOrdDrug.Cols[colOrdDrugId].Caption = "code";
            grfOrdDrug.Cols[colOrdDrugNameT].Caption = "ชื่อ ยา";
            grfOrdDrug.Cols[colOrdDrugtypcd].Caption = " typ cd";
            grfOrdDrug.Cols[colOrdDrugUnit].Caption = "unit";
            grfOrdDrug.Cols[colOrdAddDrugFr].Caption = "วิธีใช้";
            grfOrdDrug.Cols[colOrdAddDrugIn].Caption = "ข้อควรระวัง";
            grfOrdDrug.Cols[colOrdAddQty].Caption = "qty";

            grfOrdDrug.Cols[colOrdDrugId].Width = 100;
            grfOrdDrug.Cols[colOrdDrugNameT].Width = 350;
            grfOrdDrug.Cols[colOrdDrugtypcd].Width = 100;
            grfOrdDrug.Cols[colOrdDrugUnit].Width = 200;
            grfOrdDrug.Cols[colOrdAddDrugFr].Width = 300;
            grfOrdDrug.Cols[colOrdAddDrugIn].Width = 300;
            grfOrdDrug.Cols[colOrdAddQty].Width = 60;
            int i = 0;
            decimal aaa = 0;
            for (int col = 0; col < dt.Columns.Count; ++col)
            {
                grfOrdDrug.Cols[col + 1].DataType = dt.Columns[col].DataType;
                //grfOrdDrug.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
                grfOrdDrug.Cols[col + 1].Name = dt.Columns[col].ColumnName;
            }
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                if (i == 1) continue;
                grfOrdDrug[i, colOrdDrugId] = row1["MNC_ph_cd"].ToString();
                grfOrdDrug[i, colOrdDrugNameT] = row1["MNC_ph_tn"].ToString();
                grfOrdDrug[i, colOrdDrugtypcd] = row1["MNC_ph_typ_cd"].ToString();
                grfOrdDrug[i, colOrdDrugUnit] = row1["mnc_ph_unt_cd"].ToString();
                grfOrdDrug[i, colOrdAddDrugFr] = row1["MNC_ph_dir_dsc"].ToString();
                grfOrdDrug[i, colOrdAddDrugIn] = row1["MNC_ph_cau_dsc"].ToString();
                grfOrdDrug[i, colOrdAddQty] = "";
                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfOrdDrug);
            //grfOrdDrug.Cols[colXrayResult].Visible = false;
            grfOrdDrug.Cols[colOrdDrugId].AllowEditing = false;
            grfOrdDrug.Cols[colOrdDrugNameT].AllowEditing = false;
            grfOrdDrug.Cols[colOrdDrugtypcd].AllowEditing = false;
            grfOrdDrug.Cols[colOrdDrugUnit].AllowEditing = false;
            FilterRow fr = new FilterRow(grfOrdDrug);
            grfOrdDrug.AllowFiltering = true;
            grfOrdDrug.AfterFilter += GrfOrdDrug_AfterFilter;
            //}).Start();
        }
        private void GrfOrdDrug_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            for (int col = grfOrdDrug.Cols.Fixed; col < grfOrdDrug.Cols.Count; ++col)
            {
                var filter = grfOrdDrug.Cols[col].ActiveFilter;
            }
        }

        private void initGrfOrdSup()
        {
            grfOrdSup = new C1FlexGrid();
            grfOrdSup.Font = fEdit;
            grfOrdSup.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOrdSup.Location = new System.Drawing.Point(0, 0);
            grfOrdSup.Rows.Count = 1;
            grfOrdSup.DoubleClick += GrfOrdSup_DoubleClick;
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnOrdSearchSup.Controls.Add(grfOrdSup);

            theme1.SetTheme(grfOrdSup, "ShinyBlue");

        }

        private void GrfOrdSup_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setOrdSupItem();
        }
        private void setOrdSupItem()
        {
            if (grfOrdSup == null) return;
            if (grfOrdSup.Row <= 1) return;
            if (grfOrdSup.Col <= 0) return;
            String code = "";
            DataTable dt = new DataTable();
            code = grfOrdSup[grfOrdSup.Row, colOrdDrugId].ToString();
            dt = bc.bcDB.drugDB.selectDrugByCode(code);
            Row rowdrug = grfOrdItem.Rows.Add();
            rowdrug[colOrdAddId] = dt.Rows[0]["MNC_ph_cd"].ToString();
            rowdrug[colOrdAddNameT] = dt.Rows[0]["MNC_ph_tn"].ToString();
            rowdrug[colOrdAddItemType] = dt.Rows[0]["mnc_ph_typ_cd"].ToString();
            rowdrug[colOrdAddUnit] = dt.Rows[0]["mnc_ph_unt_cd"].ToString();
            rowdrug[colOrdAddFlag] = dt.Rows[0]["mnc_ph_typ_flg"].ToString();       //drug=P,supp=O,lab=Lxray=X
            rowdrug[colOrdAddDrugFr] = "";
            rowdrug[colOrdAddDrugIn] = "";
            rowdrug[colOrdAddQty] = "";
            rowdrug[0] = (grfOrdItem.Rows.Count - 1);
            rowdrug.StyleNew.BackColor = Color.FromArgb(218, 237, 255);     //Color.FromArgb(244, 222, 242);        Color.FromArgb(253, 233, 233);      Color.FromArgb(244, 252, 232);      Color.FromArgb(218, 237, 255);      Color.FromArgb(255, 255, 231);      Color.FromArgb(224, 224, 224);

            txtItmRowNo.Value = grfOrdItem.Rows.Count;
            txtItmId.Value = dt.Rows[0]["MNC_ph_cd"].ToString();
            txtItmName.Value = dt.Rows[0]["MNC_ph_tn"].ToString();
            txtItmFre.Value = "";
            txtItmIn1.Value = "";
            txtItmQty.Value = "0";
            txtItmFlag.Value = dt.Rows[0]["mnc_ph_typ_flg"].ToString();

            txtItmFre.Hide();
            txtItmIn1.Hide();
            txtItmIn2.Hide();
        }
        private void setGrfOrdSup()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.drugDB.selectSupplyAll();

            grfOrdSup.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfOrdSup.Rows.Count = dt.Rows.Count + 1;
            grfOrdSup.Cols.Count = dt.Columns.Count + 1;
            grfOrdSup.Cols[colOrdDrugId].Caption = "วันที่สั่ง";
            grfOrdSup.Cols[colOrdDrugNameT].Caption = "ชื่อ";
            grfOrdSup.Cols[colOrdDrugtypcd].Caption = "Code ";
            grfOrdSup.Cols[colOrdDrugUnit].Caption = "หน่วย";

            grfOrdSup.Cols[colOrdDrugId].Width = 100;
            grfOrdSup.Cols[colOrdDrugNameT].Width = 350;
            grfOrdSup.Cols[colOrdDrugtypcd].Width = 100;
            grfOrdSup.Cols[colOrdDrugUnit].Width = 200;

            int i = 0;
            decimal aaa = 0;
            for (int col = 0; col < dt.Columns.Count; ++col)
            {
                grfOrdSup.Cols[col + 1].DataType = dt.Columns[col].DataType;
                grfOrdSup.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
                grfOrdSup.Cols[col + 1].Name = dt.Columns[col].ColumnName;
            }
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                if (i == 1) continue;
                grfOrdSup[i, colOrdDrugId] = row1["MNC_ph_cd"].ToString();
                grfOrdSup[i, colOrdDrugNameT] = row1["MNC_ph_tn"].ToString();
                grfOrdSup[i, colOrdDrugtypcd] = row1["MNC_ph_gn"].ToString();
                grfOrdSup[i, colOrdDrugUnit] = row1["mnc_ph_unt_cd"].ToString();

                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfOrdSup);
            //grfOrdDrug.Cols[colXrayResult].Visible = false;
            grfOrdSup.Cols[colOrdDrugId].AllowEditing = false;
            grfOrdSup.Cols[colOrdDrugNameT].AllowEditing = false;
            grfOrdSup.Cols[colOrdDrugtypcd].AllowEditing = false;
            grfOrdSup.Cols[colOrdDrugUnit].AllowEditing = false;
            FilterRow fr = new FilterRow(grfOrdSup);
            grfOrdSup.AllowFiltering = true;
            grfOrdSup.AfterFilter += GrfOrdSup_AfterFilter;
            //}).Start();
        }

        private void GrfOrdSup_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            for (int col = grfOrdSup.Cols.Fixed; col < grfOrdSup.Cols.Count; ++col)
            {
                var filter = grfOrdSup.Cols[col].ActiveFilter;
            }
        }

        private void initGrfOrdLab()
        {
            grfOrdLab = new C1FlexGrid();
            grfOrdLab.Font = fEdit;
            grfOrdLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOrdLab.Location = new System.Drawing.Point(0, 0);
            grfOrdLab.Rows.Count = 1;
            grfOrdLab.DoubleClick += GrfOrdLab_DoubleClick;
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnOrdSearchLab.Controls.Add(grfOrdLab);

            theme1.SetTheme(grfOrdLab, "RainerOrange");

        }

        private void GrfOrdLab_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setOrdLabItem();
        }
        private void setOrdLabItem()
        {
            if (grfOrdLab == null) return;
            if (grfOrdLab.Row <= 1) return;
            if (grfOrdLab.Col <= 0) return;

            String code = "";
            DataTable dt = new DataTable();
            code = grfOrdLab[grfOrdLab.Row, colOrdDrugId].ToString();
            dt = bc.bcDB.labDB.selectLabByCode(code);
            Row rowdrug = grfOrdItem.Rows.Add();
            rowdrug[colOrdAddId] = dt.Rows[0]["MNC_lb_cd"].ToString();
            rowdrug[colOrdAddNameT] = dt.Rows[0]["mnc_lb_dsc"].ToString();
            rowdrug[colOrdAddFlag] = "L";
            rowdrug[colOrdAddDrugFr] = "";
            rowdrug[colOrdAddDrugIn] = "";
            rowdrug[colOrdAddQty] = "";

            rowdrug[0] = (grfOrdItem.Rows.Count - 1);
            //rowdrug.StyleDisplay.BackColor = Color.FromArgb(253, 233, 233);
            rowdrug.StyleNew.BackColor = Color.FromArgb(255, 255, 231);
            //rowdrug[colOrdDrugNameE] = dt.Rows[0]["MNC_ph_gn"].ToString();
            //rowdrug[colOrdDrugUnit] = dt.Rows[0]["mnc_ph_unt_cd"].ToString();
            //C1TextBox txtItmId = (C1TextBox)this.Controls["txtItmId"];
            //C1TextBox lbItmName = (C1TextBox)this.Controls["lbItmName"];
            txtItmId.Value = dt.Rows[0]["MNC_lb_cd"].ToString();
            txtItmName.Value = dt.Rows[0]["mnc_lb_dsc"].ToString();
            txtItmFre.Hide();
            txtItmIn1.Hide();
            txtItmIn2.Hide();
        }
        private void setGrfOrdLab()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.labDB.selectLabAll();

            grfOrdLab.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfOrdLab.Rows.Count = dt.Rows.Count + 1;
            grfOrdLab.Cols.Count = dt.Columns.Count + 1;
            grfOrdLab.Cols[colOrdLabId].Caption = "code";

            grfOrdLab.Cols[colOrdLabName].Caption = "ชื่อ LAB";
            grfOrdLab.Cols[colLabtypcd].Caption = "ประเภท";
            grfOrdLab.Cols[colOrdlabUnit].Caption = "หน่วย";
            grfOrdLab.Cols[colLabgrpdsc].Caption = "กลุ่ม";
            //grfOrdLab.Cols[colOrdDrugUnit].Caption = "หน่วย";

            grfOrdLab.Cols[colOrdDrugId].Width = 100;
            grfOrdLab.Cols[colOrdLabName].Width = 350;
            grfOrdLab.Cols[colOrdDrugtypcd].Width = 100;
            grfOrdLab.Cols[colOrdDrugUnit].Width = 200;
            grfOrdLab.Cols[colLabgrpdsc].Width = 300;
            int i = 0;
            decimal aaa = 0;
            for (int col = 0; col < dt.Columns.Count; ++col)
            {
                grfOrdLab.Cols[col + 1].DataType = dt.Columns[col].DataType;
                //grfOrdLab.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
                grfOrdLab.Cols[col + 1].Name = dt.Columns[col].ColumnName;
            }
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                if (i == 1) continue;
                grfOrdLab[i, colOrdLabId] = row1["MNC_lb_cd"].ToString();
                grfOrdLab[i, colOrdLabName] = row1["MNC_lb_dsc"].ToString();
                grfOrdLab[i, colOrdDrugtypcd] = row1["MNC_LB_TYP_DSC"].ToString();
                grfOrdLab[i, colLabgrpdsc] = row1["MNC_LB_GRP_DSC"].ToString();
                grfOrdLab[i, colOrdlabUnit] = "";

                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfOrdLab);
            //grfOrdDrug.Cols[colXrayResult].Visible = false;
            grfOrdLab.Cols[colOrdLabId].AllowEditing = false;
            grfOrdLab.Cols[colOrdLabName].AllowEditing = false;
            grfOrdLab.Cols[colOrdDrugtypcd].AllowEditing = false;
            grfOrdLab.Cols[colLabgrpdsc].AllowEditing = false;
            grfOrdLab.Cols[colOrdlabUnit].AllowEditing = false;
            FilterRow fr = new FilterRow(grfOrdLab);
            grfOrdLab.AllowFiltering = true;
            grfOrdLab.AfterFilter += GrfOrdLab_AfterFilter;
            //}).Start();
        }
        private void GrfOrdLab_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            for (int col = grfOrdLab.Cols.Fixed; col < grfOrdLab.Cols.Count; ++col)
            {
                var filter = grfOrdLab.Cols[col].ActiveFilter;
            }
        }

        private void initGrfOrdXray()
        {
            grfOrdXray = new C1FlexGrid();
            grfOrdXray.Font = fEdit;
            grfOrdXray.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOrdXray.Location = new System.Drawing.Point(0, 0);
            grfOrdXray.Rows.Count = 1;
            grfOrdXray.DoubleClick += GrfOrdXray_DoubleClick;
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("เปิด PACs infinitt", new EventHandler(ContextMenu_xray_infinitt));
            
            //grfOrdXray.ContextMenu = menuGw;
            pnOrdSearchXray.Controls.Add(grfOrdXray);

            theme1.SetTheme(grfOrdXray, "Office2016Colorful");

        }
        
        private void GrfOrdXray_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setOrdXrayItem();
        }
        private void setOrdXrayItem()
        {
            if (grfOrdXray == null) return;
            if (grfOrdXray.Row <= 1) return;
            if (grfOrdXray.Col <= 0) return;
            String code = "";
            DataTable dt = new DataTable();
            code = grfOrdXray[grfOrdXray.Row, colOrdDrugId].ToString();
            dt = bc.bcDB.xrDB.selectXrayByCode(code);
            Row rowdrug = grfOrdItem.Rows.Add();
            rowdrug[colOrdAddId] = dt.Rows[0]["MNC_xr_cd"].ToString();
            rowdrug[colOrdAddNameT] = dt.Rows[0]["mnc_xr_dsc"].ToString();
            rowdrug[colOrdAddFlag] = "X";       //drug=P,supp=O,lab=Lxray=X
            rowdrug[colOrdAddDrugFr] = "";
            rowdrug[colOrdAddDrugIn] = "";
            rowdrug[colOrdAddQty] = "";
            rowdrug[0] = (grfOrdItem.Rows.Count - 1);
            rowdrug.StyleNew.BackColor = Color.FromArgb(240, 240, 240);
            //rowdrug[colOrdDrugNameE] = dt.Rows[0]["MNC_ph_gn"].ToString();
            //rowdrug[colOrdDrugUnit] = dt.Rows[0]["mnc_ph_unt_cd"].ToString();
            //C1TextBox txtItmId = (C1TextBox)this.Controls["txtItmId"];
            //C1TextBox lbItmName = (C1TextBox)this.Controls["lbItmName"];
            txtItmId.Value = dt.Rows[0]["MNC_xr_cd"].ToString();
            txtItmName.Value = dt.Rows[0]["mnc_xr_dsc"].ToString();
        }
        private void setGrfOrdXray()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.xrDB.selectXrayAll();

            grfOrdXray.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfOrdXray.Rows.Count = dt.Rows.Count + 1;
            grfOrdXray.Cols.Count = 7;
            grfOrdXray.Cols[colOrdXrayId].Caption = "Code";
            grfOrdXray.Cols[colOrdXrayName].Caption = "Xray Description";
            grfOrdXray.Cols[colXraytypcd].Caption = "typ cd";
            grfOrdXray.Cols[colXraygrpcd].Caption = "grp cd";
            grfOrdXray.Cols[colXraygrpdsc].Caption = "Grp Description";

            grfOrdXray.Cols[colOrdXrayId].Width = 100;
            grfOrdXray.Cols[colOrdXrayName].Width = 350;
            grfOrdXray.Cols[colXraytypcd].Width = 100;
            grfOrdXray.Cols[colXraygrpcd].Width = 100;
            grfOrdXray.Cols[colXraygrpdsc].Width = 200;

            int i = 0;
            decimal aaa = 0;
            for (int col = 0; col < dt.Columns.Count; ++col)
            {
                grfOrdXray.Cols[col + 1].DataType = dt.Columns[col].DataType;
                grfOrdXray.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
                grfOrdXray.Cols[col + 1].Name = dt.Columns[col].ColumnName;
            }
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                if (i == 1) continue;
                grfOrdXray[i, colOrdXrayId] = row1["mnc_xr_cd"].ToString();
                grfOrdXray[i, colOrdXrayName] = row1["mnc_xr_dsc"].ToString();
                grfOrdXray[i, colXraytypcd] = row1["mnc_xr_typ_cd"].ToString();
                grfOrdXray[i, colXraygrpcd] = row1["MNC_XR_GRP_CD"].ToString();
                grfOrdXray[i, colXraygrpdsc] = row1["MNC_XR_GRP_DSC"].ToString();

                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfOrdXray);
            //grfOrdDrug.Cols[colXrayResult].Visible = false;
            grfOrdXray.Cols[colOrdXrayId].AllowEditing = false;
            grfOrdXray.Cols[colOrdXrayName].AllowEditing = false;
            grfOrdXray.Cols[colXraytypcd].AllowEditing = false;
            grfOrdXray.Cols[colXraygrpcd].AllowEditing = false;
            grfOrdXray.Cols[colXraygrpdsc].AllowEditing = false;
            FilterRow fr = new FilterRow(grfOrdXray);
            grfOrdXray.AllowFiltering = true;
            grfOrdXray.AfterFilter += GrfOrdXray_AfterFilter;
            //}).Start();
        }

        private void GrfOrdXray_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            for (int col = grfOrdXray.Cols.Fixed; col < grfOrdXray.Cols.Count; ++col)
            {
                var filter = grfOrdXray.Cols[col].ActiveFilter;
            }
        }

        private void initGrfOrdOR()
        {
            grfOrdOR = new C1FlexGrid();
            grfOrdOR.Font = fEdit;
            grfOrdOR.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOrdOR.Location = new System.Drawing.Point(0, 0);
            grfOrdOR.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnOrdSearchOR.Controls.Add(grfOrdOR);

            theme1.SetTheme(grfOrdOR, "GreenHouse");

        }

        private void initGrfHn()
        {
            grfHn = new C1FlexGrid();
            grfHn.Font = fEdit;
            grfHn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHn.Location = new System.Drawing.Point(0, 0);
            grfHn.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            tabMac.Controls.Add(grfHn);

            theme1.SetTheme(grfHn, bc.iniC.themeApp);

        }
        private void setGrHn()
        {
            //new Thread(() =>
            //{
            try
            {
                DataTable dt = new DataTable();
                String vn = "", preno = "", vsdate = "", an = "";

                Application.DoEvents();
                if (txtHn.Text.Length <= 0) return;
                dt = bc.bcDB.vsDB.selectDrugAllergy(txtHn.Text.Trim());
                //grfHn.Rows.Count = 1;
                //grfLab.Cols[colOrderId].Visible = false;
                grfHn.Rows.Count = 4;
                grfHn.Rows.Count = dt.Rows.Count + 1;
                grfHn.Cols.Count = 6;
                grfHn.Cols[colDrugAllCode].Caption = "Code";
                grfHn.Cols[colDrugAllName].Caption = "Drug Name";
                grfHn.Cols[colDrugAllDsc].Caption = "Drug Allergy";
                grfHn.Cols[colDrugAllAlg].Caption = "Desc";

                grfHn.Cols[colDrugAllCode].Width = 100;
                grfHn.Cols[colDrugAllName].Width = 250;
                grfHn.Cols[colDrugAllDsc].Width = 300;
                grfHn.Cols[colDrugAllAlg].Width = 200;

                int i = 0;
                decimal aaa = 0;
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    grfHn[i, colDrugAllCode] = row1["MNC_ph_cd"].ToString();
                    grfHn[i, colDrugAllName] = row1["MNC_ph_tn"].ToString();
                    grfHn[i, colDrugAllDsc] = row1["MNC_ph_memo"].ToString();
                    grfHn[i, colDrugAllAlg] = row1["MNC_PH_ALG_DSC"].ToString();

                    //row1[0] = (i - 2);
                }
                CellNoteManager mgr = new CellNoteManager(grfXray);
                grfHn.Cols[colDrugAllCode].AllowEditing = false;
                grfHn.Cols[colDrugAllName].AllowEditing = false;
                grfHn.Cols[colDrugAllDsc].AllowEditing = false;
                grfHn.Cols[colDrugAllAlg].AllowEditing = false;
            }
            catch(Exception ex)
            {
                new LogWriter("e", ex.Message);
            }
            
            //}).Start();
        }
        private void setGrfOrder(DataTable dtOrder)
        {
            //new Thread(() =>
            //{
            //DataTable dtOrder = new DataTable();
            String vsdate = "";

            //Application.DoEvents();

            //if (txtHn.Text.Length <= 0) return;
            //dt = bc.bcDB.vsDB.selectDrugAllergy(txtHn.Text.Trim());
            //grfHn.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfOrder.Cols.Count = 8;
            grfOrder.Rows.Count = 1;
            grfOrder.Rows.Count = dtOrder.Rows.Count + 1;
            //grfOrder.Cols.Count = 6;
            grfOrder.Cols[colOrderName].Caption = "Drug Name";
            grfOrder.Cols[colOrderMed].Caption = "MED";
            grfOrder.Cols[colOrderQty].Caption = "QTY";
            grfOrder.Cols[colOrderDate].Caption = "Date";
            grfOrder.Cols[colOrderFre].Caption = "วิธีใช้";
            grfOrder.Cols[colOrderIn1].Caption = "ข้อควรระวัง";

            grfOrder.Cols[colOrderName].Width = 400;
            grfOrder.Cols[colOrderMed].Width = 200;
            grfOrder.Cols[colOrderQty].Width = 80;
            grfOrder.Cols[colOrderDate].Width = 90;
            grfOrder.Cols[colOrderFre].Width = 300;
            grfOrder.Cols[colOrderIn1].Width = 300;

            int i = 0;
            decimal aaa = 0;
            foreach (DataRow row1 in dtOrder.Rows)
            {
                i++;
                grfOrder[i, colOrderName] = row1["MNC_PH_TN"].ToString();
                grfOrder[i, colOrderMed] = "";
                grfOrder[i, colOrderQty] = row1["qty"].ToString();
                grfOrder[i, colOrderDate] = bc.datetoShow(row1["mnc_req_dat"]);
                grfOrder[i, colOrderFre] = row1["MNC_PH_DIR_DSC"].ToString();
                grfOrder[i, colOrderIn1] = row1["MNC_PH_CAU_dsc"].ToString();

                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfOrder);
            grfOrder.Cols[colOrderName].AllowEditing = false;
            grfOrder.Cols[colOrderQty].AllowEditing = false;
            grfOrder.Cols[colOrderMed].AllowEditing = false;
            grfOrder.Cols[colOrderFre].AllowEditing = false;
            grfOrder.Cols[colOrderIn1].AllowEditing = false;
            grfOrder.Cols[colOrderId].Visible = false;
            
            grfOrder.Cols[colOrderDate].AllowEditing = false;
            
            //}).Start();
        }
        private void setGrOrder(String vn, String preno, String an, String anyr, String flagOPD)
        {
            //new Thread(() =>
            //{
            DataTable dtOrder = new DataTable();
            String vsdate = "";
            String statusOPD = "", vsDate = "", anDate = "", hn = "", vn1 = "";

            Application.DoEvents();
            if (flagOPD.Equals("OPD"))
            {
                statusOPD = grfOPD[grfOPD.Row, colVsStatus] != null ? grfOPD[grfOPD.Row, colVsStatus].ToString() : "";
                preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                vsDate = grfOPD[grfOPD.Row, colVsVsDate] != null ? grfOPD[grfOPD.Row, colVsVsDate].ToString() : "";
                //vsDate = bc.datetoDB(vsDate);

                chkIPD.Checked = false;
                vn = grfOPD[grfOPD.Row, colVsVn] != null ? grfOPD[grfOPD.Row, colVsVn].ToString() : "";
                txtVN.Value = vn;
                label2.Text = "VN :";
                if (vn.IndexOf("(") > 0)
                {
                    vn1 = vn.Substring(0, vn.IndexOf("("));
                }
                if (vn.IndexOf("/") > 0)
                {
                    vn1 = vn.Substring(0, vn.IndexOf("/"));
                }
                vsDate = bc.datetoDB(vsDate);
                dtOrder = bc.bcDB.vsDB.selectDrugOPD(txtHn.Text, vn, preno, vsDate);
            }
            else
            {
                statusOPD = grfIPD[grfIPD.Row, colIPDStatus] != null ? grfIPD[grfIPD.Row, colIPDStatus].ToString() : "";
                preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
                vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";

                chkIPD.Checked = true;
                an = grfIPD[grfIPD.Row, colIPDAn] != null ? grfIPD[grfIPD.Row, colIPDAn].ToString() : "";
                anDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                anyr = grfIPD[grfIPD.Row, colIPDAnYr] != null ? grfIPD[grfIPD.Row, colIPDAnYr].ToString() : "";
                txtVN.Value = an;
                label2.Text = "AN :";
                dtOrder = bc.bcDB.vsDB.selectDrugIPD(txtHn.Text, an, anyr);
            }
                //if (txtHn.Text.Length <= 0) return;
                //dt = bc.bcDB.vsDB.selectDrugAllergy(txtHn.Text.Trim());
                //grfHn.Rows.Count = 1;
                //grfLab.Cols[colOrderId].Visible = false;
            grfOrder.Rows.Count = 1;
            grfOrder.Rows.Count = dtOrder.Rows.Count + 1;
            //grfOrder.Cols.Count = 6;
            grfOrder.Cols[colOrderName].Caption = "Drug Name";
            grfOrder.Cols[colOrderMed].Caption = "MED";
            grfOrder.Cols[colOrderQty].Caption = "QTY";
            //grfOrder.Cols[colDrugAllAlg].Caption = "Desc";

            grfOrder.Cols[colOrderName].Width = 300;
            grfOrder.Cols[colOrderMed].Width = 200;
            grfOrder.Cols[colOrderQty].Width = 80;
            //grfOrder.Cols[colDrugAllAlg].Width = 200;

            int i = 0;
            decimal aaa = 0;
            foreach (DataRow row1 in dtOrder.Rows)
            {
                i++;
                grfOrder[i, colOrderName] = row1["MNC_PH_TN"].ToString();
                grfOrder[i, colOrderMed] = "";
                grfOrder[i, colOrderQty] = row1["qty"].ToString();
                //grfOrder[i, colDrugAllAlg] = row1["MNC_PH_ALG_DSC"].ToString();

                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfOrder);
            grfOrder.Cols[colOrderName].AllowEditing = false;
            grfOrder.Cols[colOrderQty].AllowEditing = false;
            grfOrder.Cols[colOrderMed].AllowEditing = false;
            grfOrder.Cols[colOrderId].Visible = false;
            //}).Start();
        }
        private void GrfHn_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void setStaffNote(String vsDate, String preno)
        {
            String file = "", dd = "", mm = "", yy = "";
            Image stffnoteR, stffnoteS;
            if (vsDate.Length > 8)
            {
                String preno1 = preno;
                try
                {
                    imgLR = null;
                    picL.Image = null;
                    picR.Image = null;
                    int chk = 0;
                    dd = vsDate.Substring(vsDate.Length - 2);
                    mm = vsDate.Substring(5, 2);
                    yy = vsDate.Substring(0,4);
                    int.TryParse(yy, out chk);
                    if (chk > 2500)
                        chk -= 543;
                    file = "\\\\"+bc.iniC.pathScanStaffNote + chk + "\\" + mm + "\\" + dd + "\\";
                    preno1 = "000000" + preno1;
                    preno1 = preno1.Substring(preno1.Length - 6);
                    //new LogWriter("e", "FrmScanView1 setStaffNote file  " + file + preno1+" hn "+txtHn.Text+" yy "+yy);
                    stffnoteR = Image.FromFile(file + preno1 + "R.JPG");
                    stffnoteS = Image.FromFile(file + preno1 + "S.JPG");
                    picL.Image = stffnoteS;
                    picR.Image = stffnoteR;
                    ContextMenu menuGwL = new ContextMenu();
                    menuGwL.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_print_staffnote_L));
                    menuGwL.MenuItems.Add("ต้องการ Print ภาพนี้ L R", new EventHandler(ContextMenu_print_staffnote_LR));
                    menuGwL.MenuItems.Add("ต้องการ Download ภาพนี้ L", new EventHandler(ContextMenu_export_staffnote_L));
                    ContextMenu menuGwR = new ContextMenu();
                    menuGwR.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_print_staffnote_R));
                    menuGwR.MenuItems.Add("ต้องการ Download ภาพนี้ R", new EventHandler(ContextMenu_export_staffnote_R));
                    picL.ContextMenu = menuGwL;
                    picR.ContextMenu = menuGwR;

                }
                catch (Exception ex)
                {
                    //MessageBox.Show("ไม่พบ StaffNote ในระบบ " + ex.Message, "");
                    new LogWriter("e", "FrmScanView1 setStaffNote ex  " + ex.Message+ " InnerException " + ex.InnerException+" file  " + file + preno1+" hn "+txtHn.Text+" yy "+yy);
                }
            }
        }
        private void ContextMenu_print_staffnote_L(object sender, System.EventArgs e)
        {
            setGrfScanToPrintStaffNoteL();
        }
        private void ContextMenu_print_staffnote_LR(object sender, System.EventArgs e)
        {
            setGrfScanToPrintStaffNoteLR();
        }
        private void ContextMenu_print_staffnote_R(object sender, System.EventArgs e)
        {
            setGrfScanToPrintStaffNoteR();
        }
        private void ContextMenu_export_staffnote_R(object sender, System.EventArgs e)
        {
            String datetick = DateTime.Now.Ticks.ToString();
            String filename = bc.iniC.medicalrecordexportpath + "\\staff_note_"+txtHn.Text.Trim()+""+txtVN.Text.Trim().Replace("/", "_")+".jpg";
            picR.Image.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            //string filePath = bc.iniC.medicalrecordexportpath + "\\resultresult_xray_lab_" + hn + "_" + vn.Replace("/", "_") + ".pdf";
            if (!File.Exists(filename))
            {
                return;
            }

            // combine the arguments together
            // it doesn't matter if there is a space after ','
            string argument = "/select, \"" + filename + "\"";

            System.Diagnostics.Process.Start("explorer.exe", argument);
        }
        private void ContextMenu_export_staffnote_L(object sender, System.EventArgs e)
        {
            //setGrfScanToPrintStaffNoteL();
            String datetick = DateTime.Now.Ticks.ToString();
            String filename = bc.iniC.medicalrecordexportpath + "\\doctor_order_" + txtHn.Text.Trim() + "" + txtVN.Text.Trim().Replace("/", "_") + ".jpg";
            picL.Image.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            //string filePath = bc.iniC.medicalrecordexportpath + "\\resultresult_xray_lab_" + hn + "_" + vn.Replace("/", "_") + ".pdf";
            if (!File.Exists(filename))
            {
                return;
            }

            // combine the arguments together
            // it doesn't matter if there is a space after ','
            string argument = "/select, \"" + filename + "\"";

            System.Diagnostics.Process.Start("explorer.exe", argument);
        }
        private void setGrfScanToPrintStaffNoteR()
        {
            SetDefaultPrinter(bc.iniC.printerA4);
            System.Threading.Thread.Sleep(100);

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += Pd_PrintPageA4_staffnote_R;
            //here to select the printer attached to user PC
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = pd;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                pd.Print();//this will trigger the Print Event handeler PrintPage
            }
        }
        private void setGrfScanToPrintStaffNoteLR()
        {
            SetDefaultPrinter(bc.iniC.printerA4);
            System.Threading.Thread.Sleep(100);

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += Pd_PrintPageA4_staffnote_LR;
            //here to select the printer attached to user PC
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = pd;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                pd.Print();//this will trigger the Print Event handeler PrintPage
            }
        }
        private void setGrfScanToPrintStaffNoteL()
        {
            SetDefaultPrinter(bc.iniC.printerA4);
            System.Threading.Thread.Sleep(100);

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += Pd_PrintPageA4_staffnote_L;
            //here to select the printer attached to user PC
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = pd;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                pd.Print();//this will trigger the Print Event handeler PrintPage
            }
        }
        private void Pd_PrintPageA4_staffnote_R(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                float newWidth = picR.Image.Width * 100 / picR.Image.HorizontalResolution;
                float newHeight = picR.Image.Height * 100 / picR.Image.VerticalResolution;

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
                e.Graphics.DrawImage(picR.Image, 0, 0, (int)newWidth, (int)newHeight);
                //}
            }
            catch (Exception)
            {

            }
        }
        private void Pd_PrintPageA4_staffnote_L(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                float newWidth = picL.Image.Width * 100 / picL.Image.HorizontalResolution;
                float newHeight = picL.Image.Height * 100 / picL.Image.VerticalResolution;

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
                e.Graphics.DrawImage(picL.Image, 0, 0, (int)newWidth, (int)newHeight);
                //}
            }
            catch (Exception)
            {

            }
        }
        private void Pd_PrintPageA4_staffnote_LR(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                Image imgL = null;
                Image imgR = null;

                imgL = bc.ResizeImagetoA4Lan(picL.Image);
                imgR = bc.ResizeImagetoA4Lan(picR.Image);
                e.Graphics.DrawImage(imgR, 60, 20, imgL.Width, imgL.Height);
                e.Graphics.DrawImage(imgL, 60, imgL.Height + 60, imgR.Width, imgR.Height);
            }
            catch (Exception ex)
            {

            }
        }
        private void GrfIPD_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            grfActive = "grfIPD";
            String an = "", vsDate="", preno="";
            chkIPD.Checked = true;
            if(rtb!=null)
                rtb.Text = "";
            label2.Text = "AN :";

            an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";
            vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
            preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
            //txt.Value = an;
            txtVN.Value = an;
            if (grfIPD.Row != 0)
            {
                if (e.NewRange.r1 == e.OldRange.r1) return;
            }
            
            if (txtHn.Text.Equals("")) return;

            setActive();
            grfIPD.Focus();
            //if (tcDtr.SelectedTab == tabOrder)
            //{
            //    tabOrderActive();
            //}
            //else if (tcDtr.SelectedTab == tabXray)
            //{
            //    setGrfXray(e.NewRange.r1);
            //}
            //else if (tcDtr.SelectedTab == tabStfNote)
            //{
            //    setStaffNote(vsDate, preno);
            //}
            //else if (tcDtr.SelectedTab == tabLab)
            //{
            //    setGrfLab(e.NewRange.r1, "IPD");
            //}
            //else if (tcDtr.SelectedTab == tabScan)
            //{
            //    setGrfScan(e.NewRange.r1, "IPD");
            //}
            //setTabLabOut(e.NewRange.r1, "IPD", bc.iniC.windows);
        }
        private void setTabLabOut(int row, String flagOPD, String flagWindows)
        {
            //int i1 = 0;
            foreach(Control obj in tcDtr.Controls)
            {
                if(obj is C1DockingTabPage)
                {
                    if (obj.Name.Equals("tablabOutOld0")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld1")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld2")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld3")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld4")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld5")) tcDtr.Controls.Remove(obj);
                }
            }
            foreach (Control obj in tcDtr.Controls)
            {
                if (obj is C1DockingTabPage)
                {
                    if (obj.Name.Equals("tablabOutOld0")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld1")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld2")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld3")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld4")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld5")) tcDtr.Controls.Remove(obj);
                }
            }
            String vn = "", preno = "", vsdate = "", an = "";
            if (flagOPD.Equals("OPD"))
            {
                vn = grfOPD[row, colVsVn] != null ? grfOPD[row, colVsVn].ToString() : "";
                preno = grfOPD[row, colVsPreno] != null ? grfOPD[row, colVsPreno].ToString() : "";
                vsdate = grfOPD[row, colVsVsDate] != null ? grfOPD[row, colVsVsDate].ToString() : "";
                //an = grfOPD[row, colIPDAnShow] != null ? grfOPD[row, colIPDAnShow].ToString() : "";
            }
            else
            {
                vn = grfIPD[row, colIPDVn] != null ? grfIPD[row, colIPDVn].ToString() : "";
                preno = grfIPD[row, colIPDPreno] != null ? grfIPD[row, colIPDPreno].ToString() : "";
                vsdate = grfIPD[row, colIPDDate] != null ? grfIPD[row, colIPDDate].ToString() : "";
                an = grfIPD[row, colIPDAnShow] != null ? grfIPD[row, colIPDAnShow].ToString() : "";
            }
            
            vn = vn.Replace("/", ".").Replace("(", ".").Replace(")", "");
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt = bc.bcDB.labexDB.selectByVn(txtHn.Text.Trim(), vn);
            if (!flagWindows.Equals("windowsxp"))
            {
                if (dt.Rows.Count > 0)
                {
                    String labexid = "", yearid = "";
                    labexid = dt.Rows[0][bc.bcDB.labexDB.labex.Id].ToString();
                    yearid = dt.Rows[0][bc.bcDB.labexDB.labex.YearId].ToString();
                    for (int i = 0; i < 6; i++)
                    {
                        MemoryStream stream;
                        String filename = "", filename1 = "";
                        filename = labexid + "_" + (i + 1).ToString() + ".pdf";
                        FtpClient ftpc = new FtpClient(bc.iniC.hostFTPLabOut, bc.iniC.userFTPLabOut, bc.iniC.passFTPLabOut, bc.ftpUsePassiveLabOut);
                        stream = ftpc.download(bc.iniC.folderFTPLabOut + "//" + yearid + "//" + filename);
                        if (stream == null) return;
                        if (stream.Length == 0) return;

                        tablabOut = new C1DockingTabPage();
                        tablabOut.Location = new System.Drawing.Point(1, 24);
                        //tabScan.Name = "c1DockingTabPage1";
                        tablabOut.Size = new System.Drawing.Size(667, 175);
                        tablabOut.TabIndex = 0;
                        tablabOut.Text = "OUT LAB";
                        tablabOut.Name = "tablabOutOld" + i.ToString();

                        tcDtr.Controls.Add(tablabOut);

                        labOutView = new C1FlexViewer();
                        labOutView.AutoScrollMargin = new System.Drawing.Size(0, 0);
                        labOutView.AutoScrollMinSize = new System.Drawing.Size(0, 0);
                        labOutView.Dock = System.Windows.Forms.DockStyle.Fill;
                        labOutView.Location = new System.Drawing.Point(0, 0);
                        labOutView.Name = "c1FlexViewer1" + i.ToString();
                        labOutView.Size = new System.Drawing.Size(1065, 790);
                        labOutView.TabIndex = 0;

                        tablabOut.Controls.Add(labOutView);

                        C1PdfDocumentSource pds = new C1PdfDocumentSource();

                        pds.LoadFromStream(stream);
                        //pds.LoadFromFile(filename1);

                        labOutView.DocumentSource = pds;
                    }
                }
            }
            else
            {
                tablabOut = new C1DockingTabPage();
                tablabOut.Location = new System.Drawing.Point(1, 24);
                //tabScan.Name = "c1DockingTabPage1";
                tablabOut.Size = new System.Drawing.Size(667, 175);
                tablabOut.TabIndex = 0;
                tablabOut.Text = "OUT LAB";
                tablabOut.Name = "tablabOutOld";
                tcDtr.Controls.Add(tablabOut);
                Panel pnlabout = new Panel();
                pnlabout.Dock = DockStyle.Fill;
                tablabOut.Controls.Add(pnlabout);
                C1FlexGrid grflabout = new C1FlexGrid();
                grflabout.Dock = DockStyle.Fill;
                grflabout.Cols[colLabOutDateReq].Caption = "Date Req";
                grflabout.Cols[colLabOutHN].Caption = "HN";
                grflabout.Cols[colLabOutFullName].Caption = "Name";
                grflabout.Cols[colLabOutVN].Caption = "Date Rec";
                grflabout.Cols[colLabOutDateReceive].Caption = "Req No";
                grflabout.Cols[colLabOutReqNo].Caption = "VN";
                grflabout.Cols[colLabOutId].Caption = "id";
                grflabout.Cols[colLabOutDateReq].Width = 100;
                grflabout.Cols[colLabOutHN].Width = 80;
                grflabout.Cols[colLabOutFullName].Width = 300;
                grflabout.Cols[colLabOutVN].Width = 100;
                grflabout.Cols[colLabOutDateReceive].Width = 80;
                grflabout.Cols[colLabOutReqNo].Width = 80;

                pnlabout.Controls.Add(grflabout);
            }
            dt1 = bc.bcDB.labexDB.selectByVn(txtHn.Text.Trim(), vn);
        }
        private void setGrfXrayIPD(int row)
        {
            //new Thread(() =>
            //{
            DataTable dt = new DataTable();
            String vn = "", preno = "", vsdate = "", an = "";
            vn = grfIPD[row, colIPDVn] != null ? grfIPD[row, colIPDVn].ToString() : "";
            preno = grfIPD[row, colIPDPreno] != null ? grfIPD[row, colIPDPreno].ToString() : "";
            vsdate = grfIPD[row, colIPDDate] != null ? grfIPD[row, colIPDDate].ToString() : "";
            an = grfIPD[row, colIPDAnShow] != null ? grfIPD[row, colIPDAnShow].ToString() : "";
            if (vn.IndexOf("(") > 0)
            {
                vn = vn.Substring(0, vn.IndexOf("("));
            }
            if (vn.IndexOf("/") > 0)
            {
                vn = vn.Substring(0, vn.IndexOf("/"));
            }
            //Application.DoEvents();
            if (an.Length > 0)
            {
                String[] an1 = an.Split('/');
                if (an1.Length > 0)
                {
                    dt = bc.bcDB.vsDB.selectResultXraybyAN(txtHn.Text, an1[0], an1[1]);
                }
            }
            else
            {
                //dt = bc.bcDB.vsDB.selectLabbyVN(vsdate, vsdate, txtHn.Text, vn, preno);
            }
            grfXray.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfXray.Rows.Count = dt.Rows.Count + 1;
            grfXray.Cols.Count = 5;
            grfXray.Cols[colXrayDate].Caption = "วันที่สั่ง";
            grfXray.Cols[colXrayName].Caption = "ชื่อX-Ray";
            grfXray.Cols[colXrayCode].Caption = "Code X-Ray";
            grfXray.Cols[colXrayResult].Caption = "ผล X-Ray";

            grfXray.Cols[colXrayDate].Width = 100;
            grfXray.Cols[colXrayName].Width = 250;
            grfXray.Cols[colXrayCode].Width = 100;
            grfXray.Cols[colXrayResult].Width = 200;
            
            int i = 0;
            decimal aaa = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                grfXray[i, colXrayDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
                grfXray[i, colXrayName] = row1["MNC_XR_DSC"].ToString();
                grfXray[i, colXrayCode] = row1["MNC_XR_CD"].ToString();
                //grfXray[i, colXrayResult] = row1["result"].ToString();
                
                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfXray);
            grfXray.Cols[colXrayResult].Visible = false;
            grfXray.Cols[colXrayDate].AllowEditing = false;
            grfXray.Cols[colXrayName].AllowEditing = false;
            grfXray.Cols[colXrayCode].AllowEditing = false;
            grfXray.Cols[colXrayResult].AllowEditing = false;
            //}).Start();
        }
        private void setGrfXray(int row)
        {
            //ProgressBar pB1 = new ProgressBar();
            //pB1.Location = new System.Drawing.Point(20, 16);
            //pB1.Name = "pB1";
            //pB1.Size = new System.Drawing.Size(862, 23);
            //gbPtt.Controls.Add(pB1);
            //pB1.Left = txtHn.Left;
            //pB1.Show();
            setHeaderDisable();
            DataTable dt = new DataTable();
            String vn = "", preno = "", vsdate = "", an = "";
            if (!chkIPD.Checked)
            {
                vn = grfOPD[row, colVsVn] != null ? grfOPD[row, colVsVn].ToString() : "";
                preno = grfOPD[row, colVsPreno] != null ? grfOPD[row, colVsPreno].ToString() : "";
                vsdate = grfOPD[row, colVsVsDate] != null ? grfOPD[row, colVsVsDate].ToString() : "";
                an = grfOPD[row, colVsAn] != null ? grfOPD[row, colVsAn].ToString() : "";
                vsdate = bc.datetoDB(vsdate);
                if (vn.IndexOf("(") > 0)
                {
                    vn = vn.Substring(0, vn.IndexOf("("));
                }
                if (vn.IndexOf("/") > 0)
                {
                    vn = vn.Substring(0, vn.IndexOf("/"));
                }
                dt = bc.bcDB.vsDB.selectResultXraybyVN1(txtHn.Text, preno, vsdate);
            }
            else
            {
                vn = grfIPD[grfIPD.Row, colIPDVn] != null ? grfIPD[grfIPD.Row, colIPDVn].ToString() : "";
                preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
                vsdate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";
                vsdate = bc.datetoDB(vsdate);
                dt = bc.bcDB.vsDB.selectResultXraybyDate(txtHn.Text, vsdate);
                //if (an.Length > 0)
                //{
                //    String[] an1 = an.Split('/');
                //    if (an1.Length > 0)
                //    {
                //        //dt = bc.bcDB.vsDB.selectResultLabbyAN(txtHn.Text, an1[0], an1[1]);
                //        dt = bc.bcDB.vsDB.selectResultXraybyAN(txtHn.Text, an1[0], an1[1]);
                        
                //    }
                //}
            }
            
            //new LogWriter("e", "FrmScanView1 setGrfXrayOPD vsdate " + vsdate);
            
            //Application.DoEvents();
            //if (an.Length > 0)
            //{
            //    String[] an1 = an.Split('/');
            //    if (an1.Length > 0)
            //    {
            //        dt = bc.bcDB.vsDB.selectResultXraybyAN(txtHn.Text, an1[0], an1[1]);
            //    }
            //}
            //else
            //{
                
            //}
            grfXray.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfXray.Rows.Count = dt.Rows.Count + 1;
            grfXray.Cols.Count = 5;
            grfXray.Cols[colXrayDate].Caption = "วันที่สั่ง";
            grfXray.Cols[colXrayName].Caption = "ชื่อX-Ray";
            grfXray.Cols[colXrayCode].Caption = "Code X-Ray";
            //grfXray.Cols[colXrayResult].Caption = "ผล X-Ray";

            grfXray.Cols[colXrayDate].Width = 100;
            grfXray.Cols[colXrayName].Width = 250;
            grfXray.Cols[colXrayCode].Width = 100;
            grfXray.Cols[colXrayResult].Width = 200;

            int i = 0;
            decimal aaa = 0;
            //pB1.Maximum = dt.Rows.Count;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    //pB1.Value = i;
                    grfXray[i, colXrayDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
                    grfXray[i, colXrayName] = row1["MNC_XR_DSC"].ToString();
                    grfXray[i, colXrayCode] = row1["MNC_XR_CD"].ToString();
                    //grfXray[i, colXrayResult] = row1["result"].ToString();

                    //row1[0] = (i - 2);
                }
                if (dt.Rows.Count == 1)
                {
                    setXrayResult();
                }
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmScanView1 setGrfXrayOPD " + ex.Message);
            }
            
            CellNoteManager mgr = new CellNoteManager(grfXray);
            grfXray.Cols[colXrayDate].AllowEditing = false;
            grfXray.Cols[colXrayName].AllowEditing = false;
            grfXray.Cols[colXrayCode].AllowEditing = false;
            grfXray.Cols[colXrayResult].AllowEditing = false;
            //if (dt.Rows.Count > 0)
            //{
            //    setXrayResult();
            //}
            //}).Start();

            setHeaderEnable();
        }
        private void setGrfLab()
        {
            //ProgressBar pB1 = new ProgressBar();
            //pB1.Location = new System.Drawing.Point(20, 16);
            //pB1.Name = "pB1";
            //pB1.Size = new System.Drawing.Size(862, 23);
            //gbPtt.Controls.Add(pB1);
            //pB1.Left = txtHn.Left;
            //pB1.Show();
            showLbLoading();
            
            setHeaderDisable();

            DataTable dt = new DataTable();
            String vn = "", preno = "", vsdate="", an="";
            DateTime dtt = new DateTime();
            if (!chkIPD.Checked)
            {
                vn = grfOPD[grfOPD.Row, colVsVn] != null ? grfOPD[grfOPD.Row, colVsVn].ToString() : "";
                preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                vsdate = grfOPD[grfOPD.Row, colVsVsDate] != null ? grfOPD[grfOPD.Row, colVsVsDate].ToString() : "";
                an = grfOPD[grfOPD.Row, colVsAn] != null ? grfOPD[grfOPD.Row, colVsAn].ToString() : "";
                vsdate = bc.datetoDB(vsdate);
                //if (DateTime.TryParse(vsdate, out dtt))
                //{
                //    vsdate = dtt.Year.ToString() + "-" + dtt.ToString("MM-dd");
                //}
                if (vsdate.Length <= 0)
                {
                    return;
                }
                if (vn.IndexOf("(") > 0)
                {
                    vn = vn.Substring(0, vn.IndexOf("("));
                }
                if (vn.IndexOf("/") > 0)
                {
                    vn = vn.Substring(0, vn.IndexOf("/"));
                }
                dt = bc.bcDB.vsDB.selectLabbyVN1(vsdate, vsdate, txtHn.Text, vn, preno);
            }
            else
            {
                vn = grfIPD[grfIPD.Row, colIPDVn] != null ? grfIPD[grfIPD.Row, colIPDVn].ToString() : "";
                preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
                vsdate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";
                if (an.Length > 0)
                {
                    String[] an1 = an.Split('/');
                    if (an1.Length > 0)
                    {
                        dt = bc.bcDB.vsDB.selectResultLabbyAN(txtHn.Text, an1[0], an1[1]);
                    }
                }
            }
            
            //Application.DoEvents();
                
            grfLab.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfLab.Rows.Count = dt.Rows.Count + 1;
            grfLab.Cols.Count = 8;
            grfLab.Cols[colLabDate].Caption = "วันที่สั่ง";
            grfLab.Cols[colLabName].Caption = "ชื่อLAB";
            grfLab.Cols[colLabNameSub].Caption = "ชื่อLABย่อย";
            grfLab.Cols[colLabResult].Caption = "ผลLAB";
            grfLab.Cols[colInterpret].Caption = "แปรผล";
            grfLab.Cols[colNormal].Caption = "Normal";
            grfLab.Cols[colUnit].Caption = "Unit";
            grfLab.Cols[colLabDate].Width = 100;
            grfLab.Cols[colLabName].Width = 250;
            grfLab.Cols[colLabNameSub].Width = 200;
            grfLab.Cols[colInterpret].Width = 200;
            grfLab.Cols[colNormal].Width = 200;
            grfLab.Cols[colUnit].Width = 150;
            grfLab.Cols[colLabResult].Width = 150;
            //grfLab.Cols[colLabResult].Width = 200;
            int i = 0;
            decimal aaa = 0;
            //pB1.Maximum = dt.Rows.Count;
            try
            {
                String labname = "", labnameold = "", reqno="", reqnoold="";
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    //pB1.Value = i;
                    labname = row1["MNC_LB_DSC"].ToString();
                    reqno = row1["mnc_req_no"].ToString();
                    if (!labname.Equals(labnameold) || !reqno.Equals(reqnoold))
                    {
                        labnameold = labname;
                        reqnoold = reqno;
                        grfLab[i, colLabName] = row1["MNC_LB_DSC"].ToString();
                    }
                    else
                    {
                        grfLab[i, colLabName] = "";
                    }
                    grfLab[i, colLabDate] = bc.datetoShow(row1["mnc_req_dat"].ToString());
                    
                    grfLab[i, colLabNameSub] = row1["mnc_res"].ToString();
                    grfLab[i, colLabResult] = row1["MNC_RES_VALUE"].ToString();
                    grfLab[i, colInterpret] = row1["MNC_STS"].ToString();
                    grfLab[i, colNormal] = row1["MNC_LB_RES"].ToString();
                    grfLab[i, colUnit] = row1["MNC_RES_UNT"].ToString();
                    //row1[0] = (i - 2);
                }
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmScanView1 setGrfLab grfLab " + ex.Message);
            }
            
            CellNoteManager mgr = new CellNoteManager(grfLab);
            grfLab.Cols[colLabName].AllowEditing = false;
            grfLab.Cols[colInterpret].AllowEditing = false;
            grfLab.Cols[colNormal].AllowEditing = false;
            //}).Start();

            setHeaderEnable();
            hideLbLoading();
        }
        private void setGrfScan()
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
            setHeaderDisable();
            lStream.Clear();
            //clearGrf();
            String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "", anyr="", vn1="";
            DataTable dtOrder = new DataTable();
            
            //new LogWriter("e", "FrmScanView1 setGrfScan 5 ");
            GC.Collect();
            //Application.DoEvents();
            DataTable dt = new DataTable();
            if (!chkIPD.Checked)
            {
                statusOPD = grfOPD[grfOPD.Row, colVsStatus] != null ? grfOPD[grfOPD.Row, colVsStatus].ToString() : "";
                preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                vsDate = grfOPD[grfOPD.Row, colVsVsDate] != null ? grfOPD[grfOPD.Row, colVsVsDate].ToString() : "";
                if (vsDate.Length > 4)
                {
                    vsDate = vsDate.Substring(0, 6)+"20"+ vsDate.Substring(vsDate.Length-2);
                    vsDate = bc.datetoDB(vsDate);
                }
                //vsDate = bc.datetoDB(vsDate);
                chkIPD.Checked = false;
                vn = grfOPD[grfOPD.Row, colVsVn] != null ? grfOPD[grfOPD.Row, colVsVn].ToString() : "";
                label2.Text = "VN :";
                txtVN.Value = vn;
                dt = bc.bcDB.dscDB.selectByVnDocScan(txtHn.Text, vn, vsDate);
            }
            else
            {
                statusOPD = grfIPD[grfIPD.Row, colIPDStatus] != null ? grfIPD[grfIPD.Row, colIPDStatus].ToString() : "";
                preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
                vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";

                chkIPD.Checked = true;
                an = grfIPD[grfIPD.Row, colIPDAn] != null ? grfIPD[grfIPD.Row, colIPDAn].ToString() : "";
                anDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                anyr = grfIPD[grfIPD.Row, colIPDAnYr] != null ? grfIPD[grfIPD.Row, colIPDAnYr].ToString() : "";
                label2.Text = "AN :";
                an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";
                if (docgrpid.Equals("1100000099"))
                {
                    dt = bc.bcDB.dscDB.selectByAn(txtHn.Text, an);
                }
                else
                {
                    dt = bc.bcDB.dscDB.selectByAnDocGrp(txtHn.Text, an, docgrpid);
                }                
                //if (dt.Rows.Count == 0)
                //{
                //    vn = grfIPD[row, colIPDVn] != null ? grfIPD[row, colIPDVn].ToString() : "";
                //    dt = bc.bcDB.dscDB.selectByVn(txtHn.Text, vn, bc.datetoDB(vsDate));
                //}
            }
            vsDate = bc.datetoDB(vsDate);
            //setStaffNote(vsDate, preno);

            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_grfscan_print));
            menuGw.MenuItems.Add("ต้องการ Print ภาพทั้งหมด", new EventHandler(ContextMenu_grfscan_print_all));
            menuGw.MenuItems.Add("ต้องการ Download ภาพนี้", new EventHandler(ContextMenu_grfscan_Download));
            menuGw.MenuItems.Add("ต้องการ แก้ไข ภาพนี้", new EventHandler(ContextMenu_grfscan_Edit));
            grfScan.ContextMenu = menuGw;
            //new LogWriter("e", "FrmScanView1 setGrfScan 6 ");
            grfScan.Rows.Count = 0;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    int cnt = 0;
                    cnt = dt.Rows.Count / 2;

                    grfScan.Rows.Count = cnt + 1;
                    //foreach (Row row1 in grfScan.Rows)
                    //{
                    //    row1.Height = 100;
                    //}
                    //for(int k=0; k < grfScan.Rows.Count; k++)
                    //{
                    //    grfScan.Rows[k].Height = 200;
                    //}
                    //pB1.Value = 0;
                    //pB1.Minimum = 0;
                    //pB1.Maximum = dt.Rows.Count;
                    //MemoryStream stream;
                    //Image loadedImage, resizedImage;
                    //if (pB1.Value == 0)
                    //{
                    lbCnt.Text = "["+dt.Rows[0][bc.bcDB.dscDB.dsc.pic_before_scan_cnt].ToString()+"]"+ "[" + dt.Rows[0][bc.bcDB.dscDB.dsc.row_cnt].ToString() + "]";
                    //}
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
                                rowd = grfScan.Rows[rowrun];
                            }
                            else
                            {
                                rowrun++;
                                rowd = grfScan.Rows[rowrun];
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
                            //if (id.Length != 10)
                            //{
                            //    string aa = "";
                            //}
                            //if (id.Equals("1000309865"))
                            //{
                            //    string aa = "";
                            //}
                            //if (stream.Length <= 0)
                            //{
                            //    string aa = "";
                            //}
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
                                //err = "051";                      //  621118  -0001
                                //rowd[colPic1] = resizedImage;     //  621118  -0001
                                //err = "06";               //  621118  -0001
                                //rowd[colPic2] = id;           //  621118  -0001
                                //err = "07";//  621118  -0001
                                rowd[colPic3] = resizedImage;       // + 0001
                                err = "061";       // + 0001
                                rowd[colPic4] = id;       // + 0001
                                err = "071";       // + 0001
                            }
                            else
                            {
                                //err = "052 " + colPic3 + " cnt " + grfScan.Cols.Count;            //  621118  -0001
                                //rowd[colPic3] = resizedImage;         //  621118  -0001
                                //err = "061";          //  621118  -0001
                                //rowd[colPic4] = id;           //  621118  -0001
                                //err = "071";          //  621118  -0001
                                err = "051";       // + 0001
                                rowd[colPic1] = resizedImage;       // + 0001
                                err = "06";       // + 0001
                                rowd[colPic2] = id;       // + 0001
                                err = "07";       // + 0001
                            }

                            strm = new listStream();
                            strm.id = id;
                            err = "08";
                            strm.stream = stream;
                            err = "09";
                            lStream.Add(strm);

                            //grf1.AutoSizeRows();
                            //err = "10";
                            //grf1.AutoSizeCols();
                            //err = "11";
                            //loadedImage.Dispose();
                            //resizedImage.Dispose();
                            //stream.Dispose();
                            //Application.DoEvents();
                            err = "12";
                            //findTrue = true;
                            //break;
                            //GC.Collect();
                            if(colcnt==50) GC.Collect();
                            if (colcnt == 100) GC.Collect();
                        }
                        catch (Exception ex)
                        {
                            String aaa = ex.Message + " " + err;
                            new LogWriter("e", "FrmScanView1 SetGrfScan ex " + ex.Message+" "+ err+ " colcnt " + colcnt+" HN "+ txtHn.Text+" "+ an+" doc_scan_id "+id);
                        }
                    //}).Start();

                    //pB1.Value++;
                        //Application.DoEvents();
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("" + ex.Message, "");
                    new LogWriter("e", "FrmScanView1 SetGrfScan if (dt.Rows.Count > 0) ex " + ex.Message);
                }
            }
            //new LogWriter("e", "FrmScanView1 setGrfScan 10 ");
            setHeaderEnable();
            //setControlGbPtt();
            grfScan.AutoSizeCols();
            grfScan.AutoSizeRows();
            hideLbLoading();
        }
        private void ContextMenu_grfscan_Download(object sender, System.EventArgs e)
        {
            String id = "", datetick="";
            if (grfScan.Col <= 0) return;
            if (grfScan.Row < 0) return;
            if (grfScan.Col == 1)
            {
                id = grfScan[grfScan.Row, colPic2].ToString();
            }
            else
            {
                id = grfScan[grfScan.Row, colPic4].ToString();
            }
            dsc_id = id;
            Stream streamDownload = null;
            MemoryStream strm = null;
            foreach (listStream lstrmm in lStream)
            {
                if (lstrmm.id.Equals(id))
                {
                    strm = lstrmm.stream;
                    streamDownload = lstrmm.stream;
                    break;
                }
            }
            if (!Directory.Exists(bc.iniC.pathDownloadFile))
            {
                Directory.CreateDirectory(bc.iniC.pathDownloadFile);
            }
            datetick = DateTime.Now.Ticks.ToString();
            Image img = Image.FromStream(streamDownload);
            img.Save(bc.iniC.pathDownloadFile+"\\"+txtHn.Text.Trim()+"_"+ datetick+".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            bc.ExploreFile(bc.iniC.pathDownloadFile + "\\" + txtHn.Text.Trim() + "_" + datetick + ".jpg");
        }
        private void ContextMenu_grfscan_Edit(object sender, System.EventArgs e)
        {
            String id = "", datetick = "";
            if (grfScan.Col <= 0) return;
            if (grfScan.Row < 0) return;
            if (grfScan.Col == 1)
            {
                id = grfScan[grfScan.Row, colPic2] != null ? grfScan[grfScan.Row, colPic2].ToString(): "";
            }
            else
            {
                id = grfScan[grfScan.Row, colPic4] !=  null ? grfScan[grfScan.Row, colPic4].ToString() : "";
            }
            if (id.Length <= 0) return;
            dsc_id = id;
            Stream streamDownload = null;
            MemoryStream strm = null;
            foreach (listStream lstrmm in lStream)
            {
                if (lstrmm.id.Equals(id))
                {
                    strm = lstrmm.stream;
                    streamDownload = lstrmm.stream;
                    break;
                }
            }
            if (!Directory.Exists(bc.iniC.pathDownloadFile))
            {
                Directory.CreateDirectory(bc.iniC.pathDownloadFile);
            }
            datetick = DateTime.Now.Ticks.ToString();
            Image img = Image.FromStream(streamDownload);
            FrmScanViewEdit frm = new FrmScanViewEdit(bc, txtHn.Text, txtVN.Text, txtName.Text, img, dsc_id, chkIPD.Checked ? "OPD":"IPD");
            frm.ShowDialog(this);
        }
        private void ContextMenu_grfscan_print(object sender, System.EventArgs e)
        {
            String id = "";
            if (grfScan.Col <= 0) return;
            if (grfScan.Row < 0) return;
            if(grfScan.Col == 1)
            {
                id = grfScan[grfScan.Row, colPic2].ToString();
            }
            else
            {
                id = grfScan[grfScan.Row, colPic4].ToString();
            }
            dsc_id = id;
            MemoryStream strm = null;
            foreach (listStream lstrmm in lStream)
            {
                if (lstrmm.id.Equals(id))
                {
                    strm = lstrmm.stream;
                    streamPrint = lstrmm.stream;
                    break;
                }
            }
            setGrfScanToPrint();
            //MessageBox.Show("row "+ grfScan.Row+"\n"+"col "+grfScan.Col+"\n ", "");

        }
        private void ContextMenu_grfscan_print_all(object sender, System.EventArgs e)
        {
            //FrmWaiting frmW = new FrmWaiting();
            //frmW.StartPosition = FormStartPosition.CenterScreen;
            //frmW.ShowDialog(this);

            int i = 0;
            //foreach(Row row in grfScan.Rows)
            //{
            //    String idLeft = "", idRight="";
                //if (i==0)
                //{
                //    if (row[colPic4] == null) continue;
                //    idLeft = row[colPic2].ToString();
                //    i = 1;
                //}
                //else
                //{
                //    if (row[colPic4] == null) continue;
                //    idLeft = row[colPic4].ToString();
                //    i = 0;
                //}
                //if (row[colPic2] == null) continue;
                //idLeft = row[colPic2].ToString();
                //if (row[colPic4] == null) continue;
                //idRight = row[colPic4].ToString();

                //dsc_id = idLeft;
                //MemoryStream strm = null;
            foreach (listStream lstrmm in lStream)
            {
                //if (lstrmm.id.Equals(idLeft))
                //{
                    //strm = lstrmm.stream;
                streamPrint = lstrmm.stream;
                setGrfScanToPrint();
                //break;
                //}
            }
                

                //dsc_id = idRight;
                //MemoryStream strmR = null;
                //foreach (listStream lstrmm in lStream)
                //{
                //    if (lstrmm.id.Equals(idRight))
                //    {
                //        strmR = lstrmm.stream;
                //        streamPrint = lstrmm.stream;
                //        break;
                //    }
                //}
                //setGrfScanToPrint();

            //}

            //frmW.Dispose();
        }
        private void setTabPrnEmailLab()
        {
            String datetick = "", pathFolder = "", filename="", ext="";
            datetick = DateTime.Now.Ticks.ToString();
            pathFolder = bc.iniC.medicalrecordexportpath + "\\" + txtHn.Text.Trim() + "\\" + datetick;
            DataTable dt = new DataTable();
            dt = setPrintLab();
            if (dt.Rows.Count <= 0) return;
            filename = bc.exportResultLab(dt, txtHn.Text.Trim(), txtVN.Text.Trim(), "", pathFolder);
            ext = Path.GetExtension(filename);
            if (ext.Equals(".jpg"))
            {
                filename = filename.Replace(".jpg", ".pdf");
            }
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            if (File.Exists(filename))
            {
                emailLab = filename;
                pds.LoadFromFile(filename);
                fvPrnEmailLab.DocumentSource = pds;
            }
        }
        private void ContextMenu_print_lab(object sender, System.EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = setPrintLab();
            FrmReport frm = new FrmReport(bc, this, "lab_result", dt);
        }
        private void ContextMenu_print_lab_export(object sender, System.EventArgs e)
        {
            showLbLoading();
            String  datetick = "", pathFolder = "";
            datetick = DateTime.Now.Ticks.ToString();
            pathFolder = bc.iniC.medicalrecordexportpath + "\\" + txtHn.Text.Trim() + "\\" + datetick;

            DataTable dt = new DataTable();
            dt = setPrintLab();

            bc.exportResultLab(dt, txtHn.Text.Trim(), txtVN.Text.Trim(),"open", pathFolder);
            hideLbLoading();
        }
        private DataTable setPrintLabPrnSSO(String vn,String preno, String vsdate)
        {
            DataTable dt = new DataTable();
            DataTable dtreq = new DataTable();
            String an = "", xraycode = "", txt1 = "", reqdate = "", reqno = "", dtrname = "", ordname = "", orddetail = "", dtrxrname = "", resdate = "", pttcompname = "", paidname = "", depname = "";
                        
            if (vsdate.Length <= 0)
            {
                return dt;
            }
            if (vn.IndexOf("(") > 0)
            {
                vn = vn.Substring(0, vn.IndexOf("("));
            }
            if (vn.IndexOf("/") > 0)
            {
                vn = vn.Substring(0, vn.IndexOf("/"));
            }
            dt = bc.bcDB.vsDB.selectLabbyVN1(vsdate, vsdate, txtHn.Text.Trim(), vn, preno);
            dtreq = bc.bcDB.vsDB.selectLabRequestbyVN1(vsdate, vsdate, txtHn.Text.Trim(), preno);
            new LogWriter("d", "FrmScanView1 setPrintLabPrnSSO preno " + preno+ " vsdate "+ vsdate +" hn "+txtHn.Text.Trim()+" vn "+vn);

            dt.Columns.Add("patient_name", typeof(String));
            dt.Columns.Add("patient_hn", typeof(String));
            dt.Columns.Add("patient_age", typeof(String));
            dt.Columns.Add("request_no", typeof(String));
            dt.Columns.Add("patient_vn", typeof(String));
            dt.Columns.Add("doctor", typeof(String));
            dt.Columns.Add("result_date", typeof(String));
            dt.Columns.Add("print_date", typeof(String));
            //dt.Columns.Add("user_lab", typeof(String));
            //dt.Columns.Add("user_check", typeof(String));
            //dt.Columns.Add("user_report", typeof(String));
            dt.Columns.Add("patient_dep", typeof(String));
            dt.Columns.Add("patient_company", typeof(String));
            //dt.Columns.Add("ptt_department", typeof(String));
            dt.Columns.Add("patient_type", typeof(String));
            dt.Columns.Add("mnc_lb_dsc", typeof(String));
            dt.Columns.Add("mnc_lb_grp_cd", typeof(String));
            dt.Columns.Add("sort1", typeof(String));
            foreach (DataRow drow in dt.Rows)
            {
                Boolean chkname = false;
                chkname = txtName.Text.Any(c => !Char.IsLetterOrDigit(c));
                if (chkname)
                {
                    drow["patient_age"] = ptt.AgeString().Replace("Year", "ปี").Replace("Month", "เดือน").Replace("Days", "วัน").Replace("s", "");
                }
                else
                {
                    drow["patient_age"] = ptt.AgeString();
                }
                drow["patient_name"] = ptt.Name;
                drow["patient_hn"] = ptt.Hn;
                drow["patient_company"] = dtreq.Rows[0]["MNC_COM_DSC"].ToString();
                //drow["patient_age"] = ptt.Name;
                drow["patient_vn"] = txtVN.Text;
                //drow["patient_dep"] = ptt.Name;
                drow["patient_type"] = dtreq.Rows[0]["MNC_FN_TYP_DSC"].ToString();
                drow["request_no"] = drow["MNC_REQ_NO"].ToString() + "/" + bc.datetoShow(drow["mnc_req_dat"].ToString());
                drow["doctor"] = dtreq.Rows[0]["dtr_name"].ToString() + "[" + dtreq.Rows[0]["mnc_dot_cd"].ToString() + "]";
                drow["result_date"] = bc.datetoShow(dtreq.Rows[0]["mnc_req_dat"].ToString());
                drow["print_date"] = bc.datetoShow(dtreq.Rows[0]["MNC_STAMP_DAT"].ToString());
                drow["user_lab"] = drow["user_lab"].ToString() + " [ทน." + drow["MNC_USR_NAME_result"].ToString() + "]";
                drow["user_report"] = drow["user_report"].ToString() + " [ทน." + drow["MNC_USR_NAME_report"].ToString() + "]";
                drow["user_check"] = drow["user_check"].ToString() + " [ทน." + drow["MNC_USR_NAME_approve"].ToString() + "]";
                if (bc.iniC.branchId.Equals("005"))
                {
                    drow["patient_dep"] = dtreq.Rows[0]["MNC_REQ_DEP"].ToString().Equals("101") ? "OPD1" : depname.Equals("107") ? "OPD2" : depname.Equals("103") ? "OPD3" :
                    depname.Equals("104") ? "ER" : depname.Equals("106") ? "WARD6" : depname.Equals("108") ? "WARD5W" : depname.Equals("109") ? "ล้างไต" :
                    depname.Equals("105") ? "WARD5M" : depname.Equals("113") ? "ICU" : depname.Equals("114") ? "NS/LR" : depname.Equals("115") ? "ทันตกรรม" : depname.Equals("116") ? "CCU" : depname;
                }
                else if (bc.iniC.branchId.Equals("002"))
                {
                    drow["patient_dep"] = dtreq.Rows[0]["MNC_REQ_DEP"].ToString().Equals("101") ? "OPD1" : depname.Equals("107") ? "OPD2" : depname.Equals("103") ? "OPD3" :
                    depname.Equals("104") ? "ER" : depname.Equals("106") ? "WARD6" : depname.Equals("108") ? "WARD5W" : depname.Equals("109") ? "ล้างไต" :
                    depname.Equals("105") ? "WARD5M" : depname.Equals("113") ? "ICU" : depname.Equals("114") ? "NS/LR" : depname.Equals("115") ? "ทันตกรรม" : depname.Equals("116") ? "CCU" : depname;
                }
                else if (bc.iniC.branchId.Equals("001"))
                {
                    drow["patient_dep"] = dtreq.Rows[0]["MNC_REQ_DEP"].ToString().Equals("101") ? "OPD1" : depname.Equals("107") ? "OPD2" : depname.Equals("103") ? "OPD3" :
                    depname.Equals("104") ? "ER" : depname.Equals("106") ? "WARD6" : depname.Equals("108") ? "WARD5W" : depname.Equals("109") ? "ล้างไต" :
                    depname.Equals("105") ? "WARD5M" : depname.Equals("113") ? "ICU" : depname.Equals("114") ? "NS/LR" : depname.Equals("115") ? "ทันตกรรม" : depname.Equals("116") ? "CCU" : depname;
                }

                drow["mnc_lb_dsc"] = dtreq.Rows[0]["MNC_LB_DSC"].ToString();
                drow["mnc_lb_grp_cd"] = dtreq.Rows[0]["MNC_LB_TYP_DSC"].ToString();
                if (drow["MNC_RES_VALUE"].ToString().Equals("-"))
                {
                    drow["MNC_RES_UNT"] = "";
                }
                drow["MNC_RES_UNT"] = drow["MNC_RES_UNT"].ToString().Replace("0.00-0.00", "").Replace("0.00 - 0.00", "").Replace("0.00", "");
                drow["sort1"] = drow["mnc_req_dat"].ToString().Replace("-", "").Replace("-", "") + drow["MNC_REQ_NO"].ToString();
            }
            foreach (DataRow drow in dt.Rows)
            {
                //MessageBox.Show(drow["sort1"].ToString(), "");
                if (drow["sort1"] == null)
                {
                    //MessageBox.Show("11", "");
                }

                if (drow["sort1"].ToString().Equals(""))
                {
                    //MessageBox.Show("22", "");
                }
            }
            return dt;
        }
        private DataTable setPrintLab()
        {
            DataTable dt = new DataTable();
            //if (grfOrdLab == null) return dt;
            //if (grfOrdLab.Row <= 1) return dt;
            //if (grfOrdLab.Col <= 0) return dt;
            String an = "", vn = "", vsdate = "", xraycode = "", txt1 = "", reqdate = "", reqno = "", dtrname = "", ordname = "", orddetail = "", dtrxrname = "", resdate = "", pttcompname = "", paidname = "", depname = "";
            
            DataTable dtreq = new DataTable();
            if (chkIPD.Checked)
            {
                vn = grfIPD[grfIPD.Row, colIPDVn] != null ? grfIPD[grfIPD.Row, colIPDVn].ToString() : "";
                preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
                vsdate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";
                if (an.Length > 0)
                {
                    String[] an1 = an.Split('/');
                    if (an1.Length > 0)
                    {
                        dt = bc.bcDB.vsDB.selectResultLabbyAN(txtHn.Text, an1[0], an1[1]);
                        dtreq = bc.bcDB.vsDB.selectRequestLabbyAN(txtHn.Text, an1[0], an1[1]);
                    }
                }
            }
            else
            {
                vn = grfOPD[grfOPD.Row, colVsVn] != null ? grfOPD[grfOPD.Row, colVsVn].ToString() : "";
                preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                vsdate = grfOPD[grfOPD.Row, colVsVsDate] != null ? grfOPD[grfOPD.Row, colVsVsDate].ToString() : "";
                an = grfOPD[grfOPD.Row, colVsAn] != null ? grfOPD[grfOPD.Row, colVsAn].ToString() : "";
                vsdate = bc.datetoDB(vsdate);
                if (vsdate.Length <= 0)
                {
                    return dt;
                }
                if (vn.IndexOf("(") > 0)
                {
                    vn = vn.Substring(0, vn.IndexOf("("));
                }
                if (vn.IndexOf("/") > 0)
                {
                    vn = vn.Substring(0, vn.IndexOf("/"));
                }
                dt = bc.bcDB.vsDB.selectLabbyVN1(vsdate, vsdate, txtHn.Text, vn, preno);
                dtreq = bc.bcDB.vsDB.selectLabRequestbyVN1(vsdate, vsdate, txtHn.Text, preno);
            }

            //if (dtreq.Rows.Count > 0)
            //{
            //    reqdate = dtreq.Rows[0]["MNC_REQ_DAT"].ToString();
            //    reqno = dtreq.Rows[0]["MNC_REQ_NO"].ToString() + "/" + dtreq.Rows[0]["MNC_REQ_YR"].ToString();
            //    dtrname = dtreq.Rows[0]["dtr_name"].ToString() + "[" + dtreq.Rows[0]["mnc_dot_cd"].ToString() + "]";
            //    ordname = dtreq.Rows[0]["MNC_XR_DSC"].ToString();
            //    pttcompname = dtreq.Rows[0]["MNC_COM_DSC"].ToString();
            //    paidname = dtreq.Rows[0]["mnc_fn_typ_dsc"].ToString();
            //    depname = dtreq.Rows[0]["MNC_REQ_DEP"].ToString();
            //}
            dt.Columns.Add("patient_name", typeof(String));
            dt.Columns.Add("patient_hn", typeof(String));
            dt.Columns.Add("patient_age", typeof(String));
            dt.Columns.Add("request_no", typeof(String));
            dt.Columns.Add("patient_vn", typeof(String));
            dt.Columns.Add("doctor", typeof(String));
            dt.Columns.Add("result_date", typeof(String));
            dt.Columns.Add("print_date", typeof(String));
            //dt.Columns.Add("user_lab", typeof(String));
            //dt.Columns.Add("user_check", typeof(String));
            //dt.Columns.Add("user_report", typeof(String));
            dt.Columns.Add("patient_dep", typeof(String));
            dt.Columns.Add("patient_company", typeof(String));
            //dt.Columns.Add("ptt_department", typeof(String));
            dt.Columns.Add("patient_type", typeof(String));
            dt.Columns.Add("mnc_lb_dsc", typeof(String));
            dt.Columns.Add("mnc_lb_grp_cd", typeof(String));
            dt.Columns.Add("sort1", typeof(String));
            //dt.Columns.Add("xry_result_date", typeof(String));
            foreach (DataRow drow in dt.Rows)
            {
                Boolean chkname = false;
                chkname = txtName.Text.Any(c => !Char.IsLetterOrDigit(c));
                if (chkname)
                {
                    drow["patient_age"] = ptt.AgeString().Replace("Year", "ปี").Replace("Month", "เดือน").Replace("Days", "วัน").Replace("s", "");
                }
                else
                {
                    drow["patient_age"] = ptt.AgeString();
                }
                drow["patient_name"] = ptt.Name;
                drow["patient_hn"] = ptt.Hn;
                drow["patient_company"] = dtreq.Rows[0]["MNC_COM_DSC"].ToString();
                //drow["patient_age"] = ptt.Name;
                drow["patient_vn"] = txtVN.Text;
                //drow["patient_dep"] = ptt.Name;
                drow["patient_type"] = dtreq.Rows[0]["MNC_FN_TYP_DSC"].ToString();
                drow["request_no"] = drow["MNC_REQ_NO"].ToString() + "/" + bc.datetoShow(drow["mnc_req_dat"].ToString());
                drow["doctor"] = dtreq.Rows[0]["dtr_name"].ToString() + "[" + dtreq.Rows[0]["mnc_dot_cd"].ToString() + "]";
                drow["result_date"] = bc.datetoShow(dtreq.Rows[0]["mnc_req_dat"].ToString());
                drow["print_date"] = bc.datetoShow(dtreq.Rows[0]["MNC_STAMP_DAT"].ToString());
                drow["user_lab"] = drow["user_lab"].ToString()+ " [ทน." + drow["MNC_USR_NAME_result"].ToString()+"]";
                drow["user_report"] = drow["user_report"].ToString() + " [ทน." + drow["MNC_USR_NAME_report"].ToString() + "]";
                drow["user_check"] = drow["user_check"].ToString() + " [ทน." + drow["MNC_USR_NAME_approve"].ToString() + "]";
                drow["patient_dep"] = dtreq.Rows[0]["MNC_REQ_DEP"].ToString().Equals("101") ? "OPD1" : depname.Equals("107") ? "OPD2" : depname.Equals("103") ? "OPD3" :
                    depname.Equals("104") ? "ER" : depname.Equals("106") ? "WARD6" : depname.Equals("108") ? "WARD5W" : depname.Equals("109") ? "ล้างไต" :
                    depname.Equals("105") ? "WARD5M" : depname.Equals("113") ? "ICU" : depname.Equals("114") ? "NS/LR" : depname.Equals("115") ? "ทันตกรรม" : depname.Equals("116") ? "CCU" : depname;
                drow["mnc_lb_dsc"] = dtreq.Rows[0]["MNC_LB_DSC"].ToString();
                drow["mnc_lb_grp_cd"] = dtreq.Rows[0]["MNC_LB_TYP_DSC"].ToString();
                if (drow["MNC_RES_VALUE"].ToString().Equals("-"))
                {
                    drow["MNC_RES_UNT"] = "";
                }
                drow["MNC_RES_UNT"] = drow["MNC_RES_UNT"].ToString().Replace("0.00-0.00", "").Replace("0.00 - 0.00", "").Replace("0.00", "");
                drow["sort1"] = drow["mnc_req_dat"].ToString().Replace("-","").Replace("-", "") + drow["MNC_REQ_NO"].ToString();
            }
            foreach (DataRow drow in dt.Rows)
            {
                //MessageBox.Show(drow["sort1"].ToString(), "");
                if (drow["sort1"] == null)
                {
                    //MessageBox.Show("11", "");
                }

                if (drow["sort1"].ToString().Equals(""))
                {
                    //MessageBox.Show("22", "");
                }
            }
            return dt;
        }
        private void setGrfVsOPD()
        {
            //ProgressBar pB1 = new ProgressBar();
            //pB1.Location = new System.Drawing.Point(20, 16);
            //pB1.Name = "pB1";
            //pB1.Size = new System.Drawing.Size(862, 23);
            //gbPtt.Controls.Add(pB1);
            //pB1.Left = txtHn.Left;
            //pB1.Show();
            setHeaderDisable();

            //if (cspL.Controls.Count > 0)
            //{
            //    cspL.Controls.Clear();
            //}
            //if (cspR.Controls.Count > 0)
            //{
            //    cspR.Controls.Clear();
            //}
            //cspL.Controls.Add(picL);
            //cspR.Controls.Add(picR);

            //grfOPD.Clear();
            grfOPD.Rows.Count = 1;
            grfOPD.Cols.Count = 25;

            //C1TextBox text = new C1TextBox();
            //grfVs.Cols[colVsVsDate].Editor = text;
            //grfVs.Cols[colVsVn].Editor = text;
            //grfVs.Cols[colVsDept].Editor = text;
            //grfVs.Cols[colVsPreno].Editor = text;

            grfOPD.Cols[colVsVsDate].Width = 80;
            grfOPD.Cols[colVsVn].Width = 80;
            grfOPD.Cols[colVsDept].Width = 240;
            grfOPD.Cols[colVsPreno].Width = 100;
            grfOPD.Cols[colVsStatus].Width = 60;
            grfOPD.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfOPD.Cols[colVsVsDate].Caption = "Visit Date";
            grfOPD.Cols[colVsVn].Caption = "VN";
            grfOPD.Cols[colVsDept].Caption = "แผนก";
            grfOPD.Cols[colVsPreno].Caption = "";
            //grfOPD.Cols[colVsPreno].Visible = false;
            //grfOPD.Cols[colVsVn].Visible = true;
            //grfOPD.Cols[colVsAn].Visible = true;
            //grfOPD.Cols[colVsAndate].Visible = false;
            grfOPD.Rows[0].Visible = false;
            grfOPD.Cols[0].Visible = false;
            grfOPD.Cols[colVsVsDate].AllowEditing = false;
            grfOPD.Cols[colVsVn].AllowEditing = false;
            grfOPD.Cols[colVsDept].AllowEditing = false;
            grfOPD.Cols[colVsPreno].AllowEditing = false;

            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            // query นี้ ไปดึงข้อมูล จาก patient_t01_2 ซึ่งมีข้อมูลเยอะมาก  ตัดออกได้ไหม เพราะเป็น IPD       แก้แล้ว
            dt = bc.bcDB.vsDB.selectVisitByHn6(txtHn.Text,"O");
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1, row = grfOPD.Rows.Count;
            //txtVN.Value = dt.Rows.Count;
            //txtName.Value = ""; 
            //txt.Value = "";
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfOPD.Rows.Add();
                String status = "", vn = "";

                status = row1["MNC_PAT_FLAG"] != null ? row1["MNC_PAT_FLAG"].ToString().Equals("O") ? "OPD" : "IPD" : "-";
                vn = row1["MNC_VN_NO"].ToString() + "/" + row1["MNC_VN_SEQ"].ToString() + "(" + row1["MNC_VN_SUM"].ToString() + ")";
                rowa[colVsVsDate] = bc.datetoShowShort(row1["mnc_date"].ToString());
                rowa[colVsVn] = vn;
                rowa[colVsStatus] = status;
                rowa[colVsPreno] = row1["mnc_pre_no"].ToString();
                rowa[colVsDept] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colVsAn] = row1["mnc_an_no"].ToString() + "/" + row1["mnc_an_yr"].ToString();
                rowa[colVsAndate] = bc.datetoShow1(row1["mnc_ad_date"].ToString());
                rowa[colVsPaidType] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[colVsHigh] = row1["MNC_HIGH"].ToString();
                rowa[colVsWeight] = row1["MNC_WEIGHT"].ToString();
                rowa[colVscc] = row1["MNC_CC"].ToString();
                rowa[colVsccex] = row1["MNC_CC_EX"].ToString();
                rowa[colVsccin] = row1["MNC_CC_IN"].ToString();
                rowa[colVsabc] = row1["MNC_ABC"].ToString();
                rowa[colVshc16] = row1["MNC_HC"].ToString();
                rowa[colVsbp1l] = row1["MNC_BP1_L"].ToString();
                rowa[colVsbp1r] = row1["MNC_BP1_R"].ToString();
                rowa[colVsbp2l] = row1["MNC_BP2_L"].ToString();
                rowa[colVsbp2r] = row1["MNC_BP2_R"].ToString();
                rowa[colVsTemp] = row1["MNC_TEMP"].ToString();
                rowa[colVsVital] = row1["MNC_BREATH"].ToString();
                rowa[colVsPres] = row1["MNC_CIR_HEAD"].ToString();
            }
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("&ยกเลิก รูปภาพนี้", new EventHandler(ContextMenu_Void));
            //menuGw.MenuItems.Add("&Update ข้อมูล", new EventHandler(ContextMenu_Update));
            //foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            //{
            //    menuGw.MenuItems.Add("&เลือกประเภทเอกสาร และUpload Image [" + dgs.doc_group_name + "]", new EventHandler(ContextMenu_upload));
            //}
            //grfVs.ContextMenu = menuGw;
            grfOPD.Cols[colVsPreno].Visible = false;
            grfOPD.Cols[colVsAn].Visible = false;
            grfOPD.Cols[colVsAndate].Visible = false;
            grfOPD.Cols[colVsVn].Visible = false;

            grfOPD.Cols[colVsbp2r].Visible = false;
            grfOPD.Cols[colVsbp2l].Visible = false;
            grfOPD.Cols[colVsbp1r].Visible = false;
            grfOPD.Cols[colVsbp1l].Visible = false;
            grfOPD.Cols[colVshc16].Visible = false;
            grfOPD.Cols[colVsabc].Visible = false;
            grfOPD.Cols[colVsccin].Visible = false;
            grfOPD.Cols[colVsccex].Visible = false;
            grfOPD.Cols[colVscc].Visible = false;
            grfOPD.Cols[colVsWeight].Visible = false;
            grfOPD.Cols[colVsHigh].Visible = false;
            grfOPD.Cols[colVsVital].Visible = false;
            grfOPD.Cols[colVsPres].Visible = false;
            grfOPD.Cols[colVsTemp].Visible = false;
            grfOPD.Cols[colVsPaidType].Visible = false;
            grfOPD.Cols[colVsRadios].Visible = false;
            grfOPD.Cols[colVsBreath].Visible = false;
            grfOPD.Cols[colVsStatus].Visible = false;
            //row1[colVSE2] = row[ic.ivfDB.pApmDB.pApm.e2].ToString().Equals("1") ? imgCorr : imgTran;
            //grfVs.AutoSizeCols();
            //grfVs.AutoSizeRows();
            //grfVs.Refresh();
            //theme1.SetTheme(grfVs, "ExpressionDark");
            setHeaderEnable();
        }
        private void setGrfVsIPD()
        {
            //ProgressBar pB1 = new ProgressBar();
            //pB1.Location = new System.Drawing.Point(20, 16);
            //pB1.Name = "pB1";
            //pB1.Size = new System.Drawing.Size(862, 23);
            //gbPtt.Controls.Add(pB1);
            //pB1.Left = txtHn.Left;
            //pB1.Show();
            setHeaderDisable();

            //grfIPD.Clear();
            grfIPD.Rows.Count = 1;
            grfIPD.Cols.Count = 10;
            
            //C1TextBox text = new C1TextBox();
            //grfVs.Cols[colVsVsDate].Editor = text;
            //grfVs.Cols[colVsVn].Editor = text;
            //grfVs.Cols[colVsDept].Editor = text;
            //grfVs.Cols[colVsPreno].Editor = text;

            grfIPD.Cols[colIPDDate].Width = 100;
            grfIPD.Cols[colIPDVn].Width = 80;
            grfIPD.Cols[colIPDDept].Width = 240;
            grfIPD.Cols[colIPDPreno].Width = 100;
            grfIPD.Cols[colIPDStatus].Width = 60;
            grfIPD.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfIPD.Cols[colIPDDate].Caption = "Visit Date";
            grfIPD.Cols[colIPDVn].Caption = "VN";
            grfIPD.Cols[colIPDDept].Caption = "แผนก";
            grfIPD.Cols[colIPDPreno].Caption = "";
            grfIPD.Cols[colIPDPreno].Visible = false;
            grfIPD.Cols[colIPDVn].Visible = true;
            grfIPD.Cols[colIPDAnShow].Visible = true;
            grfIPD.Cols[colIPDAndate].Visible = false;
            grfIPD.Rows[0].Visible = false;
            grfIPD.Cols[0].Visible = false;
            grfIPD.Cols[colIPDDate].AllowEditing = false;
            grfIPD.Cols[colIPDVn].AllowEditing = false;
            grfIPD.Cols[colIPDDept].AllowEditing = false;
            grfIPD.Cols[colIPDPreno].AllowEditing = false;

            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dt = bc.bcDB.vsDB.selectVisitIPDByHn5(txtHn.Text);
            int i = 0, j = 1, row = grfIPD.Rows.Count;
            //txtVN.Value = dt.Rows.Count;
            //txtName.Value = "";
            //txt.Value = "";
            grfIPD.Rows.Count = 0;
            grfIPD.Rows.Count = dt.Rows.Count;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfIPD.Rows[i];
                String status = "", vn = "";

                //status = row1["MNC_PAT_FLAG"] != null ? row1["MNC_PAT_FLAG"].ToString().Equals("O") ? "OPD" : "IPD" : "-";
                status = "IPD";
                vn = row1["MNC_VN_NO"].ToString() + "/" + row1["MNC_VN_SEQ"].ToString() + "(" + row1["MNC_VN_SUM"].ToString() + ")" ;
                rowa[colIPDDate] = bc.datetoShow1(row1["mnc_date"].ToString());
                rowa[colIPDVn] = vn;
                rowa[colIPDStatus] = status;
                rowa[colIPDPreno] = row1["mnc_pre_no"].ToString();
                rowa[colIPDDept] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colIPDAnShow] = row1["mnc_an_no"].ToString()+"/"+ row1["mnc_an_yr"].ToString();
                rowa[colIPDAndate] = bc.datetoShow1(row1["mnc_ad_date"].ToString());
                rowa[colIPDAnYr] = row1["mnc_an_yr"].ToString();
                rowa[colIPDAn] = row1["mnc_an_no"].ToString();
                i++;
            }
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("&ยกเลิก รูปภาพนี้", new EventHandler(ContextMenu_Void));
            //menuGw.MenuItems.Add("&Update ข้อมูล", new EventHandler(ContextMenu_Update));
            //foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            //{
            //    menuGw.MenuItems.Add("&เลือกประเภทเอกสาร และUpload Image [" + dgs.doc_group_name + "]", new EventHandler(ContextMenu_upload));
            //}
            //grfVs.ContextMenu = menuGw;
            grfIPD.Cols[colIPDAnShow].Visible = false;
            grfIPD.Cols[colIPDPreno].Visible = false;
            grfIPD.Cols[colIPDVn].Visible = false;
            grfIPD.Cols[colIPDStatus].Visible = false;
            grfIPD.Cols[colIPDAnYr].Visible = false;
            grfIPD.Cols[colIPDAn].Visible = false;
            setHeaderEnable();
        }
        private void setGrfPrnSSO_OPD()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectVisitByHn6(txtHn.Text, "O");
            grfPrn.Rows.Count = 1;
            grfPrn.Cols.Count = 6;
            grfPrn.Cols[colPrnSSOVn].Width = 80;
            grfPrn.Cols[colPrnSSOvsDate].Width = 100;
            grfPrn.Cols[colPrnSSODesc].Width = 300;
            grfPrn.Cols[colPrnSSOpreno].Width = 80;
            //grfPrn.Cols[colVsVsDate].Width = 80;
            grfPrn.Cols[colPrnSSOVn].Caption = "VN";
            grfPrn.Cols[colPrnSSOvsDate].Caption = "Date";
            grfPrn.Cols[colPrnSSODesc].Caption = "Desc";
            grfPrn.Cols[colPrnSSOpreno].Caption = "preno";
            grfPrn.Cols[colPrnSSOchk].Caption = "check";
            Column colChk = grfPrn.Cols[colPrnSSOchk];
            colChk.DataType = typeof(Boolean);
            //grfPrn.Cols[colVsVn].Caption = "VN";
            int i = 0;
            grfPrn.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                String status = "", vn = "";
                vn = row1["MNC_VN_NO"].ToString() + "/" + row1["MNC_VN_SEQ"].ToString() + "(" + row1["MNC_VN_SUM"].ToString() + ")";
                grfPrn[i, 0] = i;
                grfPrn[i, colPrnSSOchk] = false;
                grfPrn[i, colPrnSSOvsDate] = bc.datetoShowShort(row1["mnc_date"].ToString());
                grfPrn[i, colPrnSSOVn] = vn;
                grfPrn[i, colPrnSSOpreno] = row1["mnc_pre_no"].ToString();
                grfPrn[i, colPrnSSODesc] = row1["MNC_SHIF_MEMO"].ToString();
            }

            grfPrn.Cols[colPrnSSOVn].AllowEditing = false;
            grfPrn.Cols[colPrnSSODesc].AllowEditing = false;
            grfPrn.Cols[colPrnSSOpreno].AllowEditing = false;
            grfPrn.Cols[colPrnSSOchk].AllowEditing = true;
            //grfPrn.Cols[colVsVsDate].AllowEditing = false;
        }
        private void ContextMenu_Void(object sender, System.EventArgs e)
        {
            
        }
        class listStream
        {
            public String id = "";
            public MemoryStream stream;
        }
        private void setControlGbPtt()
        {
            int widthL = 0, widthR = 0;
            Size size = new Size();
            widthL = tabStfNote.Width / 2;
            widthR = widthL + 5;
            //sct.SplitterDistance = fpL.Width;
            //cspL.Width = widthL;        //panel รูปใบยาซ้าย
            //cspL.Width = 240;
            //cspL.Width = 1300;
            tcVs.Width = 100;
            sC1.HeaderHeight = 0;
            scVs.Width = 240;
            scScan.Width = 1700;
            //panel2.Width = 240;
            panel2.Width = 240;
            //sct.Panel1.Width = fpL.Width;
            //sct.Panel2.Width = fpR.Width;
            sct.HeaderHeight = 0;
            tabOPD.Width = 240;
            gbPtt.Height = 45;
            tcDtr.Width = 1700;
            scVs.SizeRatio  = bc.scVssizeradio;       // set ขนาด หน้าจอ ซ้ายกับขวา ให้ set ตรงนี้
            Application.DoEvents();
            //size = bc.MeasureString(txtName);
            //txtName.Size = size;
            if (!flagShowBtnSearch.Equals("show"))
            {
                btnHn.Hide();
            }
            txtHn.Width = 90;
            txtName.Location = new Point(txtHn.Width + 50, txtHn.Location.Y);
            lbAge.Location = new Point(txtName.Location.X + txtName.Width + 5, txtName.Location.Y);
            size = bc.MeasureString(lbAge);
            //label1.Location = new Point(lbAge.Location.X + size.Width + 10, txtName.Location.Y);
            //size = bc.MeasureString(label1);
            //cboDgs.Location = new Point(label1.Location.X + size.Width + 10, txtName.Location.Y);
            label2.Location = new Point(lbAge.Location.X + size.Width +5, txtName.Location.Y);
            size = bc.MeasureString(label2);
            txtVN.Location = new Point(label2.Location.X + size.Width +5, txtName.Location.Y);
            size = bc.MeasureString(txtVN);
            chkIPD.Location = new Point(txtVN.Location.X + txtVN.Width + 10, txtName.Location.Y - 5);
            //txt.Location = new Point(chkIPD.Location.X + chkIPD.Width + 10, txtName.Location.Y);
            lbCnt.Location = new Point(chkIPD.Location.X + chkIPD.Width + 10, txtName.Location.Y);
            size = bc.MeasureString(lbCnt);
            lbDrugAllergy.Location = new Point(lbCnt.Location.X + size.Width + 10, txtName.Location.Y);
            
            size = bc.MeasureString(lbDrugAllergy);
            lbChronic1.AutoSize = true;
            lbChronic1.Location = new System.Drawing.Point(lbDrugAllergy.Location.X + size.Width + 10, txtName.Location.Y);
            lbChronic1.Text = "Chronic : " + txtChronic;
        }
        private void FrmScanView1_Load(object sender, EventArgs e)
        {
            //Point poigtt = new Point();
            //poigtt.X = gbPtt.Width - picExit.Width - 10;
            //poigtt.Y = 10;
            //picExit.Location = poigtt;
            this.Text = "Last Update 2020-12-16 windows "+bc.iniC.windows+" dd "+DateTime.Now.ToString("dd")+" mm "+DateTime.Now.ToString("MM")+" year "+DateTime.Now.Year;
            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height/2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            setControlGbPtt();
            //scOrdDiag1.ClientSize = new Size(20, 100);
            //scOrdDiag2.ClientSize = new Size(20, 600);
            //btnItmSend.Location = new Point(180, 180);
        }
    }
}
