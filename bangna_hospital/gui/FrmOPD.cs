using AutocompleteMenuNS;
using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.C1Excel;
using C1.C1Pdf;
using C1.Win.BarCode;
using C1.Win.C1Document;
using C1.Win.C1Document.Export;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1Ribbon;
using C1.Win.C1SplitContainer;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using C1.Win.FlexReport;
using C1.Win.FlexViewer;
using C1.Win.TouchToolKit;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map.GeoData;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.VisualBasic;
using Mysqlx.Crud;
using Org.BouncyCastle.Ocsp;
using System;
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
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;
using static GrapeCity.ActiveReports.ReportsCore.Tools.DataSourceException;
using static iTextSharp.text.pdf.XfaXpathConstructor;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using Button = System.Windows.Forms.Button;

//using static bangna_hospital.object1.LabOutRIAaiJson;
//using static C1.Win.C1Preview.Strings;
using Column = C1.Win.C1FlexGrid.Column;
using File = System.IO.File;
using Font = System.Drawing.Font;
using Image = System.Drawing.Image;
using Item = bangna_hospital.object1.Item;
using Patient = bangna_hospital.object1.Patient;
using Row = C1.Win.C1FlexGrid.Row;
using TextBox = System.Windows.Forms.TextBox;
namespace bangna_hospital.gui
{
    public partial class FrmOPD : Form
    {
        BangnaControl bc;
        
        
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Patient PTT;
        Visit VS;
        PatientT07 APM;
        C1ThemeController theme1;
        C1FlexReport rptView;
        Boolean pageLoad = false, tabMedScanActiveNOtabOutLabActive=true, CHKLBAPMLISTCLICK=false, CHKLBAPMREMLISTClICK=false, CHKLBAPMLISTCLICK1 = false, CHKLBAPMREMLISTClICK1 = false;
        Image imgCorr, imgTran, resizedImage, IMG, IMGSTAFFNOTE;
        Timer timeOperList;
        String PRENO = "", VSDATE = "", HN="", VN="", DEPTNO="", HNmedscan="", DOCGRPID = "", DSCID = "", OUTLAB="", TC1Active="", TCFinishActive="", STATIONNAME="", STATUSQUICKORDER="";
        String TEMPLATESTAFFNOTE = "", SYMPTOMS="",QUEDEPT="", QUENO="", QUEFullname="", QUEHN="", QUESymptoms="", REQNOLAB="", REQNOXRAY="", REQDATELAB="", REQDATEXRAY="";
        Stream streamPrint, streamPrintL, streamPrintR, streamDownload;
        
        Form frmImg;
        C1PictureBox pic;
        C1FlexViewer fvCerti, fvTodayOutLab;
        C1BarCode qrcode;
        DataTable DTRPT, DTALLERGY, DTCHRONIC, DTLABGRPSEARCH, DTLABSEARCH, DTXRAYGRPSEARCH, DTXRAYSEARCH, DTPROCEDUREGRPSEARCH, DTPROCEDURESEARCH, DTDRUGGRPSEARCH, DTDRUGSEARCH;
        
        int originalHeight = 0, newHeight = 720, mouseWheel = 0;
        
        int ROWGrfOper = 0;

        Label lbDocAll, lbDocGrp1, lbDocGrp2, lbDocGrp3, lbDocGrp4, lbDocGrp5, lbDocGrp6, lbDocGrp7, lbDocGrp8, lbDocGrp9;
        Color colorLbDoc;
        SolidBrush brush ;
        listStream strm;
        List<listStream> lStream, lStreamPic;
        List<String> lApm;
        AutoCompleteStringCollection acmApmTime, autoLab, autoXray, autoProcedure, autoPhy, autoApm, autoDrug, autoCHECKUPDIAG, ACMDTR;
        string[] AUTOSymptom = { "07:00-08:00","08:00-09:00","08:00-15:00","09:00-10:00","09:00-15:00","10:00-11:00","10:00-15:00","11:00-12:00","12:00-13:00","13:00-14:00","14:00-15:00","15:00-16:00","16:00-17:00","17:00-18:00"
                ,"18:00-19:00","19:00-20:00","20:00-21:00"
        };
        String[] AUTOCHECKUPDIAG = { "สุขภาพสมบรูณ์แข็งแรง", "สุขภาพแข็งแรง", "ควรปรึกษาแพทย์" };
        Label lbLoading;
        FileInfo FILEL, FILER;
        DateTime RPTDTAPMSTART, RPTDTAPMEND;
        String RPTSECCODE = "", RPTDTRCODE = "", RPTDEPTCODE = "", RPTDEPTNAME = "";
        const string WIA_FORMAT_JPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
        const string WIA_FORMAT_PNG = "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}";
        const string WIA_FORMAT_BMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
        Boolean EDITDOE = false, TEMPCANCER=false, isCheckUPInitialized = false;
        ListBox lstAutoComplete;
        List<String> APMREM = new List<String>();
        HashSet<LabM01> HSLABM01 = new HashSet<LabM01>();
        HashSet<LabM01> HSLABM01C = new HashSet<LabM01>();
        HashSet<XrayM01> HSXRAYM01 = new HashSet<XrayM01>();
        HashSet<XrayM01> HSXRAYM01C = new HashSet<XrayM01>();
        HashSet<PatientM30> HSPROCEDUREM01 = new HashSet<PatientM30>();
        HashSet<PatientM30> HSPROCEDUREM01C = new HashSet<PatientM30>();
        LabM01 LAB = new LabM01();
        XrayM01 XRAY = new XrayM01();
        PatientM30 PROCEDURE = new PatientM30();
        // เพิ่มตัวแปรใน class FrmOPD
        private DataTable dtLabPrint;
        private int currentLabPrintRow = 0, pagecur=1;
        private const int MAX_LAB_ROWS_PER_PAGE = 10;
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmOPD(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        public FrmOPD(BangnaControl bc, String outlab)
        {
            this.bc = bc;
            this.OUTLAB = outlab;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            initFont();
            if (bc.iniC.hidecomponent.Equals("1"))
            {
                gbOperEar.Hide();
                gbOperEye.Hide();
                gbOperLung.Hide();
                lbOperLab.Hide();
                lbOperXray.Hide();
                lbOperDrug.Hide();
                lbOperProcedure.Hide();
            }
            sep = new C1SuperErrorProvider();
            stt = new C1SuperTooltip();
            theme1 = new C1ThemeController();
            imgCorr = Resources.red_checkmark_png_16;
            imgTran = Resources.DeleteTable_small;
            timeOperList = new Timer();
            timeOperList.Interval = 30000;
            timeOperList.Enabled = false;
            PTT = new Patient();
            VS = new Visit();
            APM = new PatientT07();
            acmApmTime = new AutoCompleteStringCollection();
            acmApmTime.AddRange(AUTOSymptom);
            autoLab = new AutoCompleteStringCollection();
            autoXray = new AutoCompleteStringCollection();
            autoProcedure = new AutoCompleteStringCollection();
            autoDrug = new AutoCompleteStringCollection();
            autoApm = new AutoCompleteStringCollection();
            autoApm = bc.bcDB.pm13DB.getlApm();
            autoCHECKUPDIAG = new AutoCompleteStringCollection();
            autoCHECKUPDIAG.AddRange(AUTOCHECKUPDIAG);
            ACMDTR = new AutoCompleteStringCollection();
            
            brush = new SolidBrush(Color.Black);

            txtPttApmDate.Value = DateTime.Now;
            txtApmMultiDate.Value = DateTime.Now;
            lStream = new List<listStream>();
            lApm = new List<String>();

            lfSbMessage.Text = bc.bcDB.pttDB.selectDeptIPDName(bc.iniC.station);
            //bc.bcDB.pm02DB.setCboPrefixT(cboCheckUPPrefixT, "");
            //bc.bcDB.pm02DB.setCboPrefixE(cboCheckUPPrefixE, "");
            //bc.setCboChckUP(cboCheckUPSelect);
            //bc.bcDB.pm04DB.setCboNation(cboCheckUPNat, "");
            bc.bcDB.pttDB.setCboDeptOPDNew(cboApmDept, "");
            bc.bcDB.pttDB.setCboDeptOPDNew(cboApmDept1, "");
            bc.bcDB.pttDB.setCboDeptOPDNew(cboRpt2, "");
            //bc.setCboAlienPosition(cboAlienPosition);
            //bc.setCboAlienCountry(cboCheckUPCountry);
            //bc.setCboSex(cboCheckUPSex);
            //bc.setCboSkintone(cboCheckUPskintone);
            //bc.bcDB.pm39DB.setCboPrefixT(cboCheckUPOrder, "3", "");//ใส่เพื่อให้ ดึงไม่ขึ้น
            bc.setCboNormal(cboOperEye);
            //bc.setCboNormal(cboCheckUpEye);
            bc.setCboNormal(cboOperLung);
            //bc.setCboNormal(cboCheckUpLung);
            bc.setCboOPDOper(cboOperCritiria);
            APMREM = bc.bcDB.pm13DB.getApmRemarkList();
            initControl();
            qrcode = new C1BarCode();
            qrcode.ForeColor = System.Drawing.Color.Black;
            qrcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            qrcode.Width = 40;
            qrcode.Height = 40;
            txtSBSearchHN.Visible = false;
            rb1.Visible = false;
            btnSBSearch.Visible = false;

            chlApmList.Hide();
            foreach (String txt in autoApm)            {               chlApmList.Items.Add(txt);            }
            txtSBSearchDate.Value = DateTime.Now;
            setEvent();
            setTheme();
            tabMedScan.Hide();
            tabMedScan.Visible = false;

            if (OUTLAB.Equals("outlab"))
            {
                tabOper.TabVisible = false;
                tabFinish.TabVisible = false;
                //tabLab.TabVisible = false;
                tabAppioment.TabVisible = false;
                tabCheckUP.TabVisible = false;
                //tabSearch.TabVisible = false;
            }
            txtApmDate.Value = DateTime.Now;            txtRptStartDate.Value = DateTime.Now;            txtRptEndDate.Value = DateTime.Now;
            clearControlCheckupSSO();
            chkApmDate.Checked = true;
            lstAutoComplete = new ListBox();            lstAutoComplete.Dock = DockStyle.Fill;
            pnInformation.Controls.Add(lstAutoComplete);
            pageLoad = false;
        }
        
        private void initControl()
        {
            initLoading();
            picL.Dock = DockStyle.Fill;            picL.SizeMode = PictureBoxSizeMode.StretchImage;            picR.Dock = DockStyle.Fill;
            picR.SizeMode = PictureBoxSizeMode.StretchImage;
            picL.Image = null;            picR.Image = null;

            picHisL.Dock = DockStyle.Fill;            picHisL.SizeMode = PictureBoxSizeMode.StretchImage;            picHisR.Dock = DockStyle.Fill;
            picHisR.SizeMode = PictureBoxSizeMode.StretchImage;

            picFinishL.Dock = DockStyle.Fill;            picFinishL.SizeMode = PictureBoxSizeMode.StretchImage;            picFinishR.Dock = DockStyle.Fill;
            picFinishR.SizeMode = PictureBoxSizeMode.StretchImage;

            picSrcL.Dock = DockStyle.Fill;            picSrcL.SizeMode = PictureBoxSizeMode.StretchImage;            picSrcR.Dock = DockStyle.Fill;
            picSrcR.SizeMode = PictureBoxSizeMode.StretchImage;

            picStaffNote.Dock = DockStyle.Fill;            picStaffNote.SizeMode = PictureBoxSizeMode.StretchImage;

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
            fvTodayOutLab = new C1FlexViewer();
            fvTodayOutLab.AutoScrollMargin = new System.Drawing.Size(0, 0);
            fvTodayOutLab.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            fvTodayOutLab.Dock = System.Windows.Forms.DockStyle.Fill;
            fvTodayOutLab.Location = new System.Drawing.Point(0, 0);
            fvTodayOutLab.Name = "fvTodayOutLab";
            fvTodayOutLab.Size = new System.Drawing.Size(1065, 790);
            fvTodayOutLab.TabIndex = 0;
            fvTodayOutLab.Ribbon.Minimized = true;
            spTodayOutLabView.Controls.Add(fvTodayOutLab);
            bc.bcDB.sumT04DB.setCboSummaryT04(cboOperRoom, bc.iniC.station, "");

            //initGrfCheckUPList();
            initGrfOperList();
            initGrfPttApm(ref grfPttApm,ref pnPttApm, "grfPttApm");
            //initGrfPttApm(ref grfApm, ref pnApm, "grfApm");
            initGrfOrder(ref grfOrder,ref pnOrder, "grfOrder");
            initGrfIPD();
            initGrfIPDScan();
            initGrfOPD();
            //initGrfOutLab();
            initGrfLab(ref grfLab,ref pnHistoryLab);
            initGrfHisOrder();
            initGrfXray(ref grfXray, ref pnHistoryXray);
            //initGrfSrc();
            //initGrfSrcVs();
            //initGrfSrcLab();
            //initGrfSrcXray();
            //initGrfSrcOrder();
            //initGrfProcedure();
            //initGrfSrcProcedure();
            //initGrfTodayOutLab();
            //initGrfOperFinish();
            //initGrfLab(ref grfOperFinishLab,ref pnFinishLab);
            //initGrfXray(ref grfOperFinishXray, ref pnFinishXray);
            //initGrfOperFinishDrug();
            //initGrfOperFinishProcedure();
            initGrfOrder(ref grfApmOrder, ref pnApmOrder, "grfApmOrder");
            initGrfRpt();
            initRptView();
            initGrfChkPackItems();
            //initGrfEKG();
            //initGrfDocOLD();
            //initGrfEST();
            //initGrfECHO();
            //initGrfHolter();
            initGrfCertMed();
            //initGrfMapPackage();
            //initGrfpackage();
            //initGrfMapPackageViewhelp();
            //initGrfMapPackageItmes();
            //initGrfOperLab();
            //initGrfLab(ref grfOperLab,ref pnPrenoLab);
            //initGrfXray(ref grfOperXray, ref pnPrenoXray);
            pnApmOrder.Hide();
        }
        private void setTheme()
        {
            //theme1.SetTheme(grfCheckUPList, "Violette");
            theme1.SetTheme(panel8, "Violette");
            theme1.SetTheme(chkCheckUpEdit, "Violette");
            theme1.SetTheme(chkCheckUPSelect, "Violette");
            theme1.SetTheme(groupBox4, "Violette");
            theme1.SetTheme(gbTrueStar, "Violette");
            theme1.SetTheme(groupBox4, "Violette");
            theme1.SetTheme(groupBox4, "Violette");
            theme1.SetTheme(btnCheckUPOrder, "Violette");
            theme1.SetTheme(btnCheckUPPrn7Disease, "Violette");
            theme1.SetTheme(chkCheckUPEditCert, "Violette");
            theme1.SetTheme(pnVitalSign, "Office2010Green");
            foreach (Control c in pnVitalSign.Controls)
            {
                if (c is C1TextBox) theme1.SetTheme(c, "Office2010Green");
                else if (c is Label) theme1.SetTheme(c, "Office2010Green");
            }
            lbSymptoms.ForeColor = Color.Red;

            theme1.SetTheme(fvCerti, bc.iniC.themeApp);
        }
        private void setEvent()
        {
            timeOperList.Tick += TimeOperList_Tick;

            tC1.SelectedTabChanged += TC1_SelectedTabChanged;            txtCheckUPDoctorId.KeyUp += TxtCheckUPDoctorId_KeyUp;
            btnCheckUPOrder.Click += BtnCheckUPOrder_Click;            btnCheckUPPrn7Disease.Click += BtnCheckUPPrn7Disease_Click;
            txtCheckUPNameT.KeyUp += TxtCheckUPNameT_KeyUp;            txtCheckUPSurNameT.KeyUp += TxtCheckUPSurNameT_KeyUp;
            txtCheckUPPassport.KeyUp += TxtCheckUPPassport_KeyUp;            txtCheckUPNameE.KeyUp += TxtCheckUPNameE_KeyUp;
            txtCheckUPSurNameE.KeyUp += TxtCheckUPSurNameE_KeyUp;            txtCheckUPMobile1.KeyUp += TxtCheckUPMobile1_KeyUp;
            txtCheckUPEmplyer.KeyUp += TxtCheckUPEmplyer_KeyUp;            txtCheckUPAddr1.KeyUp += TxtCheckUPAddr1_KeyUp;
            txtCheckUPAddr2.KeyUp += TxtCheckUPAddr2_KeyUp;            txtCheckUPPhone.KeyUp += TxtCheckUPPhone_KeyUp;
            txtCheckUPABOGroup.KeyUp += TxtCheckUPABOGroup_KeyUp;            txtCheckUPRhgroup.KeyUp += TxtCheckUPRhgroup_KeyUp;
            txtCheckUPTempu.KeyUp += TxtCheckUPTempu_KeyUp;            txtCheckUPWeight.KeyUp += TxtCheckUPWeight_KeyUp;
            txtCheckUPHeight.KeyUp += TxtCheckUPHeight_KeyUp;            txtCheckUPBp1L.KeyUp += TxtCheckUPBloodPressure_KeyUp;
            txtCheckUPHrate.KeyUp += TxtCheckUPPulse_KeyUp;            txtCheckUPRrate.KeyUp += TxtCheckUPBreath_KeyUp;
            txtOperDtr.KeyUp += TxtOperDtr_KeyUp;            txtOperDtr.KeyPress += TxtOperDtr_KeyPress;
            btnOperSaveDtr.Click += BtnOperSaveDtr_Click;            btnOperSaveVital.Click += BtnOperSaveVital_Click;
            txtOperTemp.KeyUp += TxtOperTemp_KeyUp;            txtOperHrate.KeyUp += TxtOperHrate_KeyUp;
            txtOperRrate.KeyUp += TxtOperRrate_KeyUp;            txtOperAbo.KeyUp += TxtOperAbo_KeyUp;
            txtOperRh.KeyUp += TxtOperRh_KeyUp;            txtOperBp1L.KeyUp += TxtBp1L_KeyUp;
            txtOperBp1R.KeyUp += TxtBp1R_KeyUp;            txtOperBp2L.KeyUp += TxtBp2L_KeyUp;
            txtOperBp2R.KeyUp += TxtBp2R_KeyUp;            txtOperAbc.KeyUp += TxtOperAbc_KeyUp;
            txtOperWt.KeyUp += TxtOperWt_KeyUp;            txtOperHt.KeyUp += TxtOperHt_KeyUp;
            txtOperHc.KeyUp += TxtOperHc_KeyUp;            txtOperCc.KeyUp += TxtOperCc_KeyUp;
            txtOperCcin.KeyUp += TxtOperCcin_KeyUp;            txtOperCcex.KeyUp += TxtOperCcex_KeyUp;

            tCOrder.SelectedTabChanged += TCOrder_SelectedTabChanged;            btnScanSaveImg.Click += BtnScanSaveImg_Click;
            //btnPrnStaffNote1.Click += BtnPrnStaffNote1_Click;
            btnOperClose.Click += BtnOperClose_Click;            btnSBSearch.Click += BtnSBSearch_Click;

            txtApmDtr.KeyUp += TxtApmDtr_KeyUp;            btnApmSave.Click += BtnApmSave_Click;
            txtPttApmDate.DropDownClosed += TxtPttApmDate_DropDownClosed;            txtPttApmDate.ValueChanged += TxtPttApmDate_ValueChanged;
            txtApmTime.KeyUp += TxtApmTime_KeyUp;            cboApmDept.DropDownClosed += CboApmDept_DropDownClosed;
            cboApmDept1.DropDownClosed += CboApmDept1_DropDownClosed;            txtApmDsc.KeyUp += TxtApmDsc_KeyUp;
            txtApmTel.KeyUp += TxtApmTel_KeyUp;            txtApmRemark.KeyUp += TxtApmRemark_KeyUp;
            btnApmNew.Click += BtnApmNew_Click;            btnApmPrint.Click += BtnApmPrint_Click;

            chkItemLab.Click += ChkItemLab_Click;            chkItemXray.Click += ChkItemXray_Click;
            chkItemProcedure.Click += ChkItemHotC_Click;            chkItemDrug.Click += ChkItemDrug_Click;

            txtItemCode.KeyUp += TxtItemCode_KeyUp;            txtSearchItem.KeyUp += TxtSearchItem_KeyUp;
            btnItemAdd.Click += BtnItemAdd_Click;            txtSearchItem.Enter += TxtSearchItem_Enter;

            txtSBSearchHN.KeyDown += TxtSBSearchHN_KeyDown;            txtSrcHn.KeyUp += TxtSrcHn_KeyUp;
            lbApmList.DoubleClick += LbApmList_DoubleClick;            lbApmRemList.DoubleClick += LbApmRemList_DoubleClick;
            //txtTodayOutLabStartDate.DropDownClosed += TxtTodayOutLabStartDate_DropDownClosed;

            txtSBSearchDate.DropDownClosed += TxtDateSearch_DropDownClosed;            btnOperItemSearch.Click += BtnOperItemSearch_Click;
            btnApmOrder.Click += BtnApmOrder_Click;            txtApmPlusDay.KeyPress += TxtApmPlusDay_KeyPress;
            txtApmPlusDay.KeyUp += TxtApmPlusDay_KeyUp;            lbApm7Week.Click += LbApm7Week_Click;
            lbApm14Week.Click += LbApm14Week_Click;            lbApm1Month.Click += LbApm1Month_Click;
            btnOrderSave.Click += BtnOrderSave_Click;            btnOrderSubmit.Click += BtnOrderSubmit_Click;
            txtApmDate.DropDownClosed += TxtApmDate_DropDownClosed;

            txtApmSrc.KeyPress += TxtApmSrc_KeyPress;            btnApmSearch.Click += BtnApmSearch_Click;
            btnApmExcel.Click += BtnApmExcel_Click;            btnRptPrint.Click += BtnRptPrint_Click;
            btnRpt1.Click += BtnRpt1_Click;            txtStaffNoteL.KeyUp += TxtStaffNoteL_KeyUp;
            btnStaffNote.Click += BtnStaffNote_Click;            txtStaffNoteR.KeyUp += TxtStaffNoteR_KeyUp;

            txtCheckUPHN.KeyUp += TxtCheckUPHN_KeyUp;            btnCheckUPPrn7Thai.Click += BtnCheckUPPrn7Thai_Click;
            btnPrnCertMed.Click += BtnPrnCertMed_Click;            btnCerti1.Click += BtnCerti1_Click;
            btnCerti2.Click += BtnCerti2_Click;            btnCertiView1.Click += BtnCertiView1_Click;
            btnCertiView2.Click += BtnCertiView2_Click;

            tCFinish.SelectedTabChanged += TCFinish_SelectedTabChanged;            btnCheckUPPrnDriver.Click += BtnCheckUPPrnDriver_Click;

            btnScanGetImg.Click += BtnScanGetImg_Click;            btnCheckUPSSoGetResult.Click += BtnCheckUPSSoGetResult_Click;
            btnCheckUPSSoPrint.Click += BtnCheckUPSSoPrint_Click;            btnOperPrnSticker.Click += BtnOperPrnSticker_Click;
            btnOperOpenSticker.Click += BtnOperOpenSticker_Click;

            btnPrnStaffNote.Click += BtnPrnStaffNote_Click;            btnPrnStaffNote1.Click += DropDownItem1_Click;
            btnPrnStaffNote2.Click += DropDownItem2_Click;            btnPrnStaffNote3.Click += DropDownItem3_Click;
            btnPrnStaffNote4.Click += DropDownItem4_Click;
            dropDownItem27.Click += DropDownItem27_Click;            dropDownItem5.Click += DropDownItem5_Click;
            dropDownItem6.Click += DropDownItem6_Click;            dropDownItem7.Click += DropDownItem7_Click;
            dropDownItem8.Click += DropDownItem8_Click;

            btnCheckUPAlienPrint.Click += BtnCheckUPAlienPrint_Click;            cboCheckUPSelect.SelectedItemChanged += CboCheckUPSelect_SelectedItemChanged;
            btnCheckUPAlienGetResult.Click += BtnCheckUPAlienGetResult_Click;            btnCheckUPSaveDtr.Click += BtnCheckUPSaveDtr_Click;
            btnCheckUPSaveVital.Click += BtnCheckUPSaveVital_Click;            btnSendDOE.Click += BtnSendDOE_Click;
            btnCheckUPFolder.Click += BtnCheckUPFolder_Click;            btnCheckUPPrnStaffNoteDoe.Click += BtnCheckUPPrnStaffNoteDoe_Click;
            btnCheckUPDoeView.Click += BtnCheckUPDoeView_Click;

            txtOperHN.KeyUp += TxtOperHN_KeyUp;            btnPrnLAB.Click += BtnPrnLAB_Click;
            btnPrnXray.Click += BtnPrnXray_Click;            btnCheckUPSaveStaffNote.Click += BtnCheckUPSaveStaffNote_Click;
            txtApmHn.KeyUp += TxtApmHn_KeyUp;            btnCheckUPExcelAlien.Click += BtnCheckUPExcelAlien_Click;
            txtApmVsNewHn.KeyUp += TxtApmVsNewHn_KeyUp;            txtApmVsNewHn.KeyPress += TxtApmVsNewHn_KeyPress;

            btnScanClearImg.Click += BtnScanClearImg_Click;            btnSrcEKGScanNew.Click += BtnSrcEKGScanNew_Click;
            btnSrcEKGScanSave.Click += BtnSrcEKGScanSave_Click;            btnSrcDocOLDNew.Click += BtnSrcDocOLDNew_Click;
            btnSrcDocOLDSave.Click += BtnSrcDocOLDSave_Click;            btnSrcECHOScanNew.Click += BtnSrcECHOScanNew_Click;
            btnSrcECHOScanSave.Click += BtnSrcECHOScanSave_Click;            btnSrcESTScanNew.Click += BtnSrcESTScanNew_Click;
            btnSrcESTScanSave.Click += BtnSrcESTScanSave_Click;            btnSrcHolterScanNew.Click += BtnSrcHolterScanNew_Click;
            btnSrcHolterScanSave.Click += BtnSrcHolterScanSave_Click;

            btnOperEarSave.Click += BtnOperEarSave_Click;            btnOperEyeSave.Click += BtnOperEyeSave_Click;
            btnOperSaveStaffNote.Click += BtnOperSaveStaffNote_Click;            btnOperLungSave.Click += BtnOperLungSave_Click;
            btnCertMedScan.Click += BtnCertMedScan_Click;            btnCertMedGet.Click += BtnCertMedGet_Click;
            btnCertMedUpload.Click += BtnCertMedUpload_Click;            btnCertMedUploadNew.Click += BtnCertMedUploadNew_Click;
            btnCheckUPChk1.Click += BtnCheckUPChk1_Click;            cboOperRoom.SelectedItemChanged += CboOperRoom_SelectedItemChanged;
            cboOperRoom.SelectedIndexChanged += CboOperRoom_SelectedIndexChanged;
            btnApmMulti.Click += BtnApmMulti_Click;            btnOperPrnQue.Click += BtnOperPrnQue_Click;
            txtMapPackageSearch.KeyUp += TxtMapPackageSearch_KeyUp;            txtMapPackagePackagesearch.KeyUp += TxtMapPackagePackagesearch_KeyUp;
            btnMapPackagepackageSave.Click += BtnMapPackagepackageSave_Click;            btnCheckUpEarSave.Click += BtnCheckUpEarSave_Click;
            btnCheckUpEyeSave.Click += BtnCheckUpEyeSave_Click;            btnCheckUpLungSave.Click += BtnCheckUpLungSave_Click;
            btnCheckUpCompPrint.Click += BtnCheckUpCompPrint_Click;            txtMapPackagepackageCode.KeyUp += TxtMapPackagepackageCode_KeyUp;
            chlApmList.LostFocus += ChlApmList_LostFocus;            chlApmList.Leave += ChlApmList_Leave;
            lbSymptoms.DoubleClick += LbSymptoms_DoubleClick;            btnSearchLab.Click += BtnSearchLab_Click;
            lbDoctor.DoubleClick += LbDoctor_DoubleClick;            cboOperCritiria.SelectedItemChanged += CboOperCritiria_SelectedItemChanged;
            txtOperCodeApprove.KeyUp += TxtOperCodeApprove_KeyUp;            cboApmDtr.SelectedItemChanged += CboApmDtr_SelectedItemChanged;
            btnOperDept.Click += BtnOperDept_Click;            btnOperConsult.Click += BtnOperConsult_Click;
            lbOperItem.DoubleClick += LbOperItem_DoubleClick;
            //btnOperPrnReq.Click += BtnOperPrnReq_Click;
        }

        private void BtnOperPrnReq_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if (PRENO.Equals(""))            {                lfSbMessage.Text = "ไม่พบ PRENO";                return;            }
            //if(btnOperPrnReq.Text.Equals("พิมพ์ใบสั่ง Lab"))            {                printLabReqNo();            }
            //else if (btnOperPrnReq.Text.Equals("พิมพ์ใบสั่ง Xray"))            {
            //}
        }
        private void printLabReqNo(String hn,String reqno, String reqdate)
        {
            dtLabPrint = bc.bcDB.labT02DB.selectbyHNReqNo(hn, reqdate, reqno);
            if (dtLabPrint.Rows.Count == 0)     {       lfSbMessage.Text = "ไม่พบข้อมูลใบสั่ง Lab";      return;            }
            // Reset counter
            currentLabPrintRow = 0;
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerA5;
            document.PrintPage += Document_PrintPageRequestLab;
            document.DefaultPageSettings.Landscape = false;
            document.Print();
            // Clear ข้อมูลหลังพิมพ์เสร็จ
            dtLabPrint = null;
        }

        private void Document_PrintPageRequestLab(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            // A5 Landscape size in points (1 inch = 72 points)
            const int A5_WIDTH = 595;   // 8.27 inches × 72 = 595 points
            const int A5_HEIGHT = 420, adjHeight=90;  // 5.83 inches × 72 = 420 points
            // Safe printable area (with margins)
            const int MARGIN_TOP = 20, MARGIN_BOTTOM = 20, MARGIN_LEFT = 20, MARGIN_RIGHT = 20, gapline=5;
            // Maximum printable height
            int maxPrintableHeight = A5_HEIGHT - MARGIN_BOTTOM; // 400 points
            // ✅ กำหนดค่าคงที่สำหรับกระดาษ A5 Landscape
            const int MAX_Y = A5_HEIGHT - MARGIN_BOTTOM + adjHeight; // 390 points
            if (dtLabPrint == null || dtLabPrint.Rows.Count == 0)   {       e.HasMorePages = false; lfSbMessage.Text = "ไม่พบข้อมูลใบสั่ง Lab"; return;     }
            String pttname = "", deptname = "", dtrname = "", vsdatetime = "", reqdatetime = "", compname = "", paidname = "", hn="", vn="";
            String userreq = "", line = null, prndate = "", price = "", qty = "", price1 = "";
            float colName = 10, col1 = 50, col2 = 250, col3 = 450, col4 = 550, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, col5 = 620;
            prndate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            if (currentLabPrintRow == 0 || currentLabPrintRow >= dtLabPrint.Rows.Count)
            {
                DataRow firstRow = dtLabPrint.Rows[0];
                pttname = firstRow["pttfullname"]?.ToString() ?? "";
                deptname = firstRow["MNC_SEC_NO"]?.ToString() ?? "";
                compname = firstRow["MNC_RES_MAS"]?.ToString() ?? "";
                dtrname = firstRow["dtr_name"]?.ToString() ?? "";
                paidname = firstRow["MNC_FN_TYP_CD"]?.ToString() ?? "";
                userreq = firstRow["MNC_USR_FULL_usr"]?.ToString() ?? "";
                vsdatetime = bc.datetoShow(firstRow["MNC_DATE"]?.ToString() ?? "") + " " + bc.showTime(firstRow["MNC_TIME"]?.ToString() ?? "");
                reqdatetime = bc.datetoShow(firstRow["req_date"]?.ToString() ?? "") + " " + bc.showTime(firstRow["MNC_REQ_TIM"]?.ToString() ?? "");
                hn = firstRow["MNC_HN_NO"]?.ToString() ?? "";
                vn = firstRow["MNC_VN_NO"]?.ToString() ?? "";

                deptname = bc.bcDB.pm32DB.getDeptNameOPD(deptname);
                compname = bc.bcDB.pm24DB.GetCompanyNameFast(compname);
                paidname = bc.bcDB.finM02DB.getPaidNameCopilot(paidname);
            }
            Graphics g = e.Graphics;
            float fontHeight = fPDF.GetHeight();
            int startX = 10; int startY = 10; int offset = 5;
            g.DrawString("โรงพยาบาล บางนา5", fPDF, Brushes.Black, colName, startY);
            g.DrawString("ชื่อผู้ตรวจ " + pttname, fPDF, Brushes.Black, col2, startY);
            g.DrawString("อายุ " + PTT.AgeStringOK1(), fPDF, Brushes.Black, col4, startY);

            offset += (int)fontHeight;
            g.DrawString("ใบสั่งทำการ LAB", fPDF, Brushes.Black, colName, startY + offset);
            g.DrawString("HN: " + hn, fPDF, Brushes.Black, col2, startY + offset);
            g.DrawString("VN: " + VS.VN, fPDF, Brushes.Black, col4, startY + offset);

            offset += (int)fontHeight + gapline;
            g.DrawString("เลขที่ใบสั่ง: " + REQNOLAB + "/" + REQDATELAB, fPDF, Brushes.Black, col2, startY + offset);
            g.DrawString("วันที่สั่ง: " + reqdatetime, fPDF, Brushes.Black, col4, startY + offset);

            offset += (int)fontHeight;
            g.DrawString("แผนก: " + deptname , fPDF, Brushes.Black, colName, startY + offset);
            g.DrawString("แพทย์: " + dtrname, fPDF, Brushes.Black, col2, startY + offset);
            g.DrawString("วันที่ตรวจ: " + vsdatetime, fPDF, Brushes.Black, col4, startY + offset);

            offset += (int)fontHeight;
            g.DrawString("บริษัท: " + compname, fPDF, Brushes.Black, colName, startY + offset);
            g.DrawString("ประเภทผู้ป่วย: " + paidname, fPDF, Brushes.Black, col2, startY + offset);
            g.DrawString("วันที่พิมพ์: " + prndate, fPDF, Brushes.Black, col4, startY + offset);

            // ✅ Header ตาราง
            offset += (int)fontHeight + gapline - 5;
            g.DrawString("..........................................................................................................................................................................................................................................", fPDF, Brushes.Black, startX, startY + offset);
            g.DrawString("STAT", fPDF, Brushes.Black, MARGIN_LEFT, startY + offset + 15);
            g.DrawString("DESCRIPTION", fPDF, Brushes.Black, col1+20, startY + offset + 15);
            g.DrawString("PRICE", fPDF, Brushes.Black, col4, startY + offset + 15);
            offset += (int)fontHeight + gapline - 5;
            g.DrawString("..........................................................................................................................................................................................................................................", fPDF, Brushes.Black, startX, startY + offset);
            // ✅ พิมพ์รายการ Lab (สูงสุด 9 รายการต่อหน้า)
            int rowsOnThisPage = 0;            int startRow = currentLabPrintRow;

            for (int i = startRow; i < dtLabPrint.Rows.Count && rowsOnThisPage < MAX_LAB_ROWS_PER_PAGE; i++)
            {
                DataRow arow = dtLabPrint.Rows[i];
                offset += (int)fontHeight + 5;
                String stat = "";
                String desc = (arow["order_name"]?.ToString() ?? "") + " " + (arow["order_code"]?.ToString() ?? "");
                float price2 = 0;
                float.TryParse(arow["MNC_LB_PRI"]?.ToString() ?? "0", out price2);
                g.DrawString(stat, fPDF, Brushes.Black, MARGIN_LEFT, startY + offset);
                g.DrawString(desc, fPDF, Brushes.Black, col1, startY + offset);
                g.DrawString(price2.ToString("#,###.00"), fPDF, Brushes.Black, col4, startY + offset);
                rowsOnThisPage++;                currentLabPrintRow++;
            }
            // ✅ Footer
            int pagecnt = 0;
            pagecnt = (dtLabPrint.Rows.Count / 10);
            if ((dtLabPrint.Rows.Count % 10) > 0) pagecnt++;
            offset += (int)fontHeight + 5;
            g.DrawString("...............................................................................................................................................................................................................................................", fPDF, Brushes.Black, startX, MAX_Y);
            g.DrawString("user/เจ้าหน้าที่ "+userreq, fPDF, Brushes.Black, col1, MAX_Y + 15);
            g.DrawString("total/รวมรายการ " + dtLabPrint.Rows.Count, fPDF, Brushes.Black, col3, MAX_Y + 15);
            g.DrawString("page " + pagecur + "/"+ pagecnt, fPDF, Brushes.Black, col5, MAX_Y + 15);
            g.DrawString("..................................................................................................................................................................................................................................................", fPDF, Brushes.Black, startX, MAX_Y + 25);
            g.DrawString("FM-NUR-212 (00-08/04/59)(1/1)", fPDFs2, Brushes.Black, startX, MAX_Y + 40);

            // ✅ ตรวจสอบว่ายังมีรายการเหลืออยู่หรือไม่
            if (currentLabPrintRow < dtLabPrint.Rows.Count)
            {
                pagecur++;
                e.HasMorePages = true;  // พิมพ์หน้าต่อไป
            }
            else
            {
                e.HasMorePages = false; // พิมพ์เสร็จแล้ว
                currentLabPrintRow = 0; // Reset counter
            }
        }
        private void printXrayReqNo(String hn, String reqno, String reqdate)
        {
            dtLabPrint = bc.bcDB.xrayT02DB.selectbyHNReqNo(hn, reqdate, reqno);
            if (dtLabPrint.Rows.Count == 0) { lfSbMessage.Text = "ไม่พบข้อมูลใบสั่ง Xray"; return; }
            // Reset counter
            currentLabPrintRow = 0;
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerA5;
            document.PrintPage += Document_PrintPageRequestXray;
            document.DefaultPageSettings.Landscape = false;
            document.Print();
            // Clear ข้อมูลหลังพิมพ์เสร็จ
            dtLabPrint = null;
        }
        private void Document_PrintPageRequestXray(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            // A5 Landscape size in points (1 inch = 72 points)
            const int A5_WIDTH = 595;   // 8.27 inches × 72 = 595 points
            const int A5_HEIGHT = 420, adjHeight = 90;  // 5.83 inches × 72 = 420 points
            // Safe printable area (with margins)
            const int MARGIN_TOP = 20, MARGIN_BOTTOM = 20, MARGIN_LEFT = 20, MARGIN_RIGHT = 20, gapline = 5;
            // Maximum printable height
            int maxPrintableHeight = A5_HEIGHT - MARGIN_BOTTOM; // 400 points
            // ✅ กำหนดค่าคงที่สำหรับกระดาษ A5 Landscape
            const int MAX_Y = A5_HEIGHT - MARGIN_BOTTOM + adjHeight; // 390 points
            if (dtLabPrint == null || dtLabPrint.Rows.Count == 0) { e.HasMorePages = false; lfSbMessage.Text = "ไม่พบข้อมูลใบสั่ง Xray"; return; }
            String pttname = "", deptname = "", dtrname = "", vsdatetime = "", reqdatetime = "", compname = "", paidname = "", hn = "", vn = "";
            String userreq = "", line = null, prndate = "", price = "", qty = "", price1 = "";
            float colName = 10, col1 = 50, col2 = 250, col3 = 450, col4 = 550, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, col5 = 620;
            prndate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            if (currentLabPrintRow == 0 || currentLabPrintRow >= dtLabPrint.Rows.Count)
            {
                DataRow firstRow = dtLabPrint.Rows[0];
                pttname = firstRow["pttfullname"]?.ToString() ?? "";
                deptname = firstRow["MNC_SEC_NO"]?.ToString() ?? "";
                compname = firstRow["MNC_RES_MAS"]?.ToString() ?? "";
                dtrname = firstRow["dtr_name"]?.ToString() ?? "";
                paidname = firstRow["MNC_FN_TYP_CD"]?.ToString() ?? "";
                userreq = firstRow["MNC_USR_FULL_usr"]?.ToString() ?? "";
                vsdatetime = bc.datetoShow2(firstRow["MNC_DATE"]?.ToString() ?? "") + " " + bc.showTime(firstRow["MNC_TIME"]?.ToString() ?? "");
                reqdatetime = bc.datetoShow2(firstRow["req_date"]?.ToString() ?? "") + " " + bc.showTime(firstRow["MNC_REQ_TIM"]?.ToString() ?? "");
                hn = firstRow["MNC_HN_NO"]?.ToString() ?? "";
                vn = firstRow["MNC_VN_NO"]?.ToString() ?? "";

                deptname = bc.bcDB.pm32DB.getDeptNameOPD(deptname);
                compname = bc.bcDB.pm24DB.GetCompanyNameFast(compname);
                paidname = bc.bcDB.finM02DB.getPaidNameCopilot(paidname);
            }
            Graphics g = e.Graphics;
            float fontHeight = fPDF.GetHeight();
            int startX = 10; int startY = 10; int offset = 5;
            g.DrawString("โรงพยาบาล บางนา5", fPDF, Brushes.Black, colName, startY);
            g.DrawString("ชื่อผู้ตรวจ " + pttname, fPDF, Brushes.Black, col2, startY);
            g.DrawString("อายุ " + PTT.AgeStringOK1(), fPDF, Brushes.Black, col4, startY);

            offset += (int)fontHeight;
            g.DrawString("ใบสั่งทำการ XRAY", fPDF, Brushes.Black, colName, startY + offset);
            g.DrawString("HN: " + hn, fPDF, Brushes.Black, col2, startY + offset);
            g.DrawString("VN: " + VS.VN, fPDF, Brushes.Black, col4, startY + offset);

            offset += (int)fontHeight + gapline;
            g.DrawString("เลขที่ใบสั่ง: " + REQNOXRAY + "/" + REQDATEXRAY, fPDF, Brushes.Black, col2, startY + offset);
            g.DrawString("วันที่สั่ง: " + reqdatetime, fPDF, Brushes.Black, col4, startY + offset);

            offset += (int)fontHeight;
            g.DrawString("แผนก: " + deptname, fPDF, Brushes.Black, colName, startY + offset);
            g.DrawString("แพทย์: " + dtrname, fPDF, Brushes.Black, col2, startY + offset);
            g.DrawString("วันที่ตรวจ: " + vsdatetime, fPDF, Brushes.Black, col4, startY + offset);

            offset += (int)fontHeight;
            g.DrawString("บริษัท: " + compname, fPDF, Brushes.Black, colName, startY + offset);
            g.DrawString("ประเภทผู้ป่วย: " + paidname, fPDF, Brushes.Black, col2, startY + offset);
            g.DrawString("วันที่พิมพ์: " + prndate, fPDF, Brushes.Black, col4, startY + offset);

            // ✅ Header ตาราง
            offset += (int)fontHeight + gapline - 5;
            g.DrawString("...............................................................................................................................................................................................................................................", fPDF, Brushes.Black, startX, startY + offset);
            g.DrawString("STAT", fPDF, Brushes.Black, MARGIN_LEFT, startY + offset + 15);
            g.DrawString("DESCRIPTION", fPDF, Brushes.Black, col1, startY + offset + 15);
            g.DrawString("PRICE", fPDF, Brushes.Black, col4, startY + offset + 15);
            offset += (int)fontHeight + gapline - 5;
            g.DrawString("...............................................................................................................................................................................................................................................", fPDF, Brushes.Black, startX, startY + offset);
            // ✅ พิมพ์รายการ Lab (สูงสุด 9 รายการต่อหน้า)
            int rowsOnThisPage = 0; int startRow = currentLabPrintRow;

            for (int i = startRow; i < dtLabPrint.Rows.Count && rowsOnThisPage < MAX_LAB_ROWS_PER_PAGE; i++)
            {
                DataRow arow = dtLabPrint.Rows[i];
                offset += (int)fontHeight + 5;
                String stat = "";
                String desc = (arow["order_name"]?.ToString() ?? "") + " " + (arow["order_code"]?.ToString() ?? "");
                float price2 = 0;
                float.TryParse(arow["MNC_XR_PRI"]?.ToString() ?? "0", out price2);
                g.DrawString(stat, fPDF, Brushes.Black, MARGIN_LEFT, startY + offset);
                g.DrawString(desc, fPDF, Brushes.Black, col1, startY + offset);
                g.DrawString(price2.ToString("#,###.00"), fPDF, Brushes.Black, col4, startY + offset);
                rowsOnThisPage++; currentLabPrintRow++;
            }
            int pagecnt = 0;
            pagecnt = (dtLabPrint.Rows.Count / 10);
            if ((dtLabPrint.Rows.Count % 10) > 0) pagecnt++;
            // ✅ Footer
            offset += (int)fontHeight + 5;
            g.DrawString("...............................................................................................................................................................................................................................................", fPDF, Brushes.Black, startX, MAX_Y);
            g.DrawString("user/เจ้าหน้าที่ " + userreq, fPDF, Brushes.Black, col1, MAX_Y + 15);
            g.DrawString("total/รวมรายการ " + dtLabPrint.Rows.Count, fPDF, Brushes.Black, col3, MAX_Y + 15);
            g.DrawString("page " + pagecur + "/" + pagecnt, fPDF, Brushes.Black, col5, MAX_Y + 15);
            g.DrawString("...............................................................................................................................................................................................................................................", fPDF, Brushes.Black, startX, MAX_Y + 25);
            g.DrawString("FM-XRT-040 (00-08/04/59)(1/1)", fPDFs2, Brushes.Black, startX, MAX_Y + 30);

            // ✅ ตรวจสอบว่ายังมีรายการเหลืออยู่หรือไม่
            if (currentLabPrintRow < dtLabPrint.Rows.Count)
            {
                pagecur++;
                e.HasMorePages = true;  // พิมพ์หน้าต่อไป
            }
            else
            {
                e.HasMorePages = false; // พิมพ์เสร็จแล้ว
                currentLabPrintRow = 0; // Reset counter
            }
        }
        private void LbOperItem_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.SuspendLayout();
            timeOperList.Enabled = false;
            FrmItemSearch frm = new FrmItemSearch(bc, chkItemDrug.Checked ? "DRUG":chkItemLab.Checked?"LAB":chkItemXray.Checked?"XRAY":chkItemProcedure.Checked? "PROCEDURE" : "");
            if (chkItemLab.Checked) { frm.DTLABSEARCH = DTLABSEARCH; frm.DTLABGRPSEARCH = DTDRUGGRPSEARCH; }
            else if (chkItemXray.Checked) { frm.DTXRAYSEARCH = DTXRAYSEARCH; frm.DTXRAYGRPSEARCH = DTXRAYGRPSEARCH; }
            else if (chkItemProcedure.Checked) { frm.DTPROCEDURESEARCH = DTPROCEDURESEARCH; frm.DTPROCEDUREGRPSEARCH = DTPROCEDUREGRPSEARCH; }
            else if (chkItemDrug.Checked) { frm.DTDRUGSEARCH = DTDRUGSEARCH; frm.DTDRUGGRPSEARCH = DTDRUGGRPSEARCH; }
            
            frm.DTLABGRPSEARCH = DTLABGRPSEARCH;
            frm.ShowDialog();
            if ((bc.items != null) && (bc.items.Count > 0))
            {
                //txtSearchItem.Text = item.name + "#" + item.code;
                setOrderItem(bc.items, lbOperItem);
                //setGrfOrderItem(item.code, item.name, item.qty, item.flag); }
                txtItemCode.Focus();
            }
            if (chkItemLab.Checked) {       DTLABSEARCH = frm.DTLABSEARCH;  DTLABGRPSEARCH = frm.DTLABGRPSEARCH;    }
            else if (chkItemXray.Checked) { DTXRAYSEARCH = frm.DTXRAYSEARCH; DTXRAYGRPSEARCH = frm.DTXRAYGRPSEARCH; }
            else if (chkItemProcedure.Checked) { DTPROCEDURESEARCH = frm.DTPROCEDURESEARCH; DTPROCEDUREGRPSEARCH = frm.DTPROCEDUREGRPSEARCH; }
            else if (chkItemDrug.Checked) { DTDRUGSEARCH = frm.DTDRUGSEARCH; DTDRUGGRPSEARCH = frm.DTDRUGGRPSEARCH; }
            frm.Dispose();
            this.ResumeLayout();
            timeOperList.Enabled = true;
        }
        private void BtnOperConsult_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            timeOperList.Enabled = false;
            //Consult โปรแกรมเดิม เป็นการออก vn ใหม่ ไปที่แผนกที่ต้องการ เช่นคนไข้มาหาหมอ ปวดท้อง แล้วต้องไปพบหมอสูติ จะเป็นการออกใบยาใบที่2
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Size = new Size(600, 400);
            frm.Text = "เลือกแผนกเพื่อส่งปรึกษา";
            Label lbdept = new Label();
            lbdept.Font = fEdit;
            lbdept.AutoSize = true;
            lbdept.Left = 20;
            lbdept.Top = 20;
            lbdept.Text = "เลือกแผนกเพื่อส่งปรึกษา";
            Size textSize = TextRenderer.MeasureText(lbdept.Text, lbdept.Font);
            C1ComboBox cboDept = new C1ComboBox();
            cboDept.Font = fEdit;
            cboDept.Left = lbdept.Left + textSize.Width + 20;
            cboDept.Top = lbdept.Top;
            bc.bcDB.pm32DB.setC1ComboDeptOPD(cboDept, "");
            Label lbSypm = new Label();
            lbSypm.Font = fEdit;
            lbSypm.AutoSize = true;
            lbSypm.Left = 20;
            lbSypm.Top = lbdept.Top + 40;
            lbSypm.Text = "อาการ";
            textSize = TextRenderer.MeasureText(lbSypm.Text, lbSypm.Font);
            TextBox txtSymp = new TextBox();
            txtSymp.Font = fEdit;
            txtSymp.Left = lbSypm.Left + textSize.Width + 20;
            txtSymp.Top = lbSypm.Top;
            txtSymp.Width = 300;
            Button btnsave = new Button();
            btnsave.Font = fEdit;
            btnsave.Text = "save";
            btnsave.Height = 40;
            btnsave.Left = frm.Width - 20 - btnsave.Width;
            btnsave.Top = frm.Height - 80;
            btnsave.Click += (s1, e1) =>
            {
                FrmPasswordConfirm frmPwd = new FrmPasswordConfirm(bc);
                frmPwd.ShowDialog(this);
                if (bc.USERCONFIRMID.Length <= 0)         { return; }
                if (cboDept.SelectedItem == null)   {   MessageBox.Show("กรุณาเลือกแผนก");    return;     }
                String seccode = ((ComboBoxItem)cboDept.SelectedItem).Value, deptname = ((ComboBoxItem)cboDept.SelectedItem).Text, symptom = txtSymp.Text, preno="";
                if (symptom.Equals(""))             {   MessageBox.Show("กรุณาระบุอาการ");    return;     }
                //String chk = bc.bcDB.vsDB.insertConsultOPD(txtOperHN.Text, PRENO, VSDATE, deptcode, deptname, symptom);
                Visit vs = new Visit();
                vs = bc.bcDB.vsDB.selectbyPreno(txtOperHN.Text, VSDATE, PRENO);
                preno = bc.bcDB.vsDB.insertVisit1(HN, VS.PaidCode, txtSymp.Text.Trim(), seccode, "opd consult", vs.DoctorId, vs.VisitType, vs.compcode, vs.insurcode, bc.USERCONFIRMID);
                frm.Close();
            };
            frm.Controls.Add(lbdept);
            frm.Controls.Add(cboDept);
            frm.Controls.Add(btnsave);
            frm.Controls.Add(lbSypm);
            frm.Controls.Add(txtSymp);
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void BtnOperDept_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            timeOperList.Enabled = false;
            String deptcode="", deptname="", seccode="";
            Panel pntop = new Panel();
            pntop.Dock = DockStyle.Top;
            pntop.Height = 40;
            Panel pngrf = new Panel();
            pngrf.Dock = DockStyle.Fill;
            Label lbDeptname = new Label();
            lbDeptname.Text = "ส่งไปแผนก ";
            lbDeptname.Font = fEdit2B;
            lbDeptname.AutoSize = true;
            Button btndept = new Button();
            btndept.Text = "save";
            btndept.Font = fEdit;
            btndept.Height = 40;
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Size = new Size(600, 400);
            frm.Text = "เลือกแผนก";
            C1FlexGrid grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);
            grf.Rows.Count = 1;
            grf.Cols.Count = 4;
            grf.Cols[1].Width = 400;
            grf.Cols[2].Width = 60;
            grf.Cols[3].Width = 60;
            grf.Cols[1].Caption = "Name";
            grf.Cols[2].Caption = "dept";
            grf.Cols[3].Caption = "sec";
            grf.Cols[1].AllowEditing = false;
            grf.Cols[2].AllowEditing = false;
            grf.Cols[3].AllowEditing = false;
            DataTable dt = bc.bcDB.pm32DB.selectDeptOPD();
            
            pntop.Controls.Add(btndept);
            pngrf.Controls.Add(grf);
            pntop.Controls.Add(lbDeptname);
            frm.Controls.Add(pngrf);
            frm.Controls.Add(pntop);
            btndept.Left = frm.Width - 20 - btndept.Width;
            int i = 1; grf.Rows.Count = dt.Rows.Count + 2;
            foreach (DataRow dr in dt.Rows)
            {
                Row row = grf.Rows[i];
                row[0] = (i);
                row[1] = dr["MNC_MD_DEP_DSC"].ToString();
                row[2] = dr["MNC_MD_DEP_NO"].ToString();
                row[3] = dr["MNC_SEC_NO"].ToString();
                i++;
            }
            grf.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            grf.Click += (s1, e1) =>
            {
                if (grf.Row < 1) { return; }
                deptcode = "";                deptname = "";                seccode = "";                lbDeptname.Text = "ส่งไปแผนก ";
            };
            grf.DoubleClick += (s1, e1) =>
            {
                if (grf.Row < 1) { return; }
                deptcode = grf[grf.Row, 2].ToString();      deptname = grf[grf.Row, 1].ToString();      seccode = grf[grf.Row, 3].ToString();
                lbDeptname.Text = "ต้องการส่งไปแผนก "+deptname;
            };
            btndept.Click += (s1, e1) =>
            {
                if (grf.Row < 1) { return; }
                if(deptcode.Equals(""))         {           MessageBox.Show("กรุณาเลือกแผนก");        return;             }
                if(seccode.Equals(""))          {           MessageBox.Show("ไม่พบรหัสแผนก");         return;             }
                String chk = bc.bcDB.vsDB.updateChangeDept(txtOperHN.Text, PRENO, VSDATE, deptcode, seccode);
                frm.Dispose();
            };
            frm.ShowDialog(this);
            timeOperList.Enabled = true;
        }
        private void CboApmDtr_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(cboApmDtr.SelectedItem != null)
            {
                String dtrcode = cboApmDtr.SelectedItem!=null?((ComboBoxItem)cboApmDtr.SelectedItem).Value:"";
                if(dtrcode.Length>0)
                {
                    setGrfApm();
                }
            }
        }

        private void TxtOperCodeApprove_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                String code = txtOperCodeApprove.Text.Trim();
                if(!code.Equals(""))
                {
                    String chk = bc.selectDoctorName(code.Trim());
                    if(chk.Length<=0)
                    {
                        MessageBox.Show("ไม่พบรหัสแพทย์ " + code);
                        txtOperCodeApprove.Focus();
                    }
                }
            }
        }

        private void CboOperCritiria_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if(pageLoad)            {                return; }
            setGrfOperList(DEPTNO);
        }

        private void LbDoctor_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pageLoad = true;
            timeOperList.Enabled = false;
            FrmOpdRoomDoctor frm = new FrmOpdRoomDoctor(bc, DEPTNO);
            frm.ShowDialog(this);
            timeOperList.Enabled = true;
            pageLoad = false;
        }

        private void BtnSearchLab_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String hn = txtSrcHn.Text.Trim();
            String lab = txtSearchLabLab.Text.Trim();
            if (!hn.Equals(""))
            {
                if (grfSearchLab == null)                    {                        initGrfSearchLab();                    }
                setGrfSearchLab(hn);
            }
        }
        private void LbSymptoms_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Size = new Size(600, 300);
            frm.Text = "เลือกอาการ";
            TextBox txt = new TextBox();
            txt.Multiline = false;
            txt.Size = new Size(550, 30);
            txt.Location = new Point(20, 20);
            txt.Text = lbSymptoms.Text;
            txt.Font = fEdit;
            Label lb = new Label();
            lb.Location = new Point(20, 60);
            Button btn = new Button();
            btn.Text = "ตกลง";
            btn.Location = new Point(150, 200);
            btn.Font = fEdit;
            btn.Size = new Size(100, 35);
            btn.Click += (s, ee) =>
            {
                lbSymptoms.Text = txt.Text;
                String re = bc.bcDB.vsDB.updateSymptoms(HN, PRENO, VSDATE, lbSymptoms.Text.Trim(),"");
                if(int.Parse(re) > 0)   {       lfSbMessage.Text = "บันทึกอาการสำเร็จ";    }       else    {   lfSbMessage.Text = "บันทึกอาการไม่สำเร็จ";  }
                frm.Close();                frm.Dispose();
            };
            frm.Controls.Add(txt);            frm.Controls.Add(lb);            frm.Controls.Add(btn);            frm.ShowDialog(this);
        }
        private void TxtPttApmDate_ValueChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtApmTime.Focus();
            setApmCnt();
        }

        private void TxtMapPackagepackageCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                String txt = txtMapPackagepackageCode.Text.Trim();
                if (!txt.Equals(""))
                {
                    setGrfPackage(txt,"code");
                }
            }
        }

        private void BtnCheckUpCompPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void BtnCheckUpLungSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = bc.bcDB.vsDB.updateLung(txtCheckUPHN.Text.Trim(), PRENO, VSDATE, txtCheckUpLung.Text.Trim(), cboCheckUpLung.Text.Trim());
        }

        private void BtnCheckUpEyeSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtCheckUPHN.Text.Length <= 0) { lfSbMessage.Text = "กรุณาใส่ HN"; return; }
            String re = bc.bcDB.vsDB.updateEye(txtCheckUPHN.Text.Trim(), PRENO, VSDATE, txtCheckUpLeftEye.Text.Trim(), txtCheckUpRightEye.Text.Trim(), txtOperLeftEyePh.Text.Trim(), txtCheckUpRightEyePh.Text.Trim(), cboOperEye.Text.Trim());
        }

        private void BtnCheckUpEarSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtCheckUPHN.Text.Length <= 0) { lfSbMessage.Text = "กรุณาใส่ HN"; return; }
            String rear = chkOperLeftEarNormal.Checked ? "1" : "0";
            String lear = chkOperRightEarNormal.Checked ? "1" : "0";
            String re = bc.bcDB.vsDB.updateEar(txtCheckUPHN.Text.Trim(), PRENO, VSDATE, txtCheckUpLeftEar.Text.Trim(), txtCheckUpRightEar.Text.Trim(), rear, lear, txtCheckUpLeftEarOther.Text.Trim(), txtOperRightEarOther.Text.Trim());
        }

        private void BtnMapPackagepackageSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtMapPackageCompName.Text.Length <= 0) { lfSbMessage.Text = "กรุณาเลือกรายการแพ็คเกจ"; return; }
            if (txtMapPackagepackageCode.Text.Length <= 0) { lfSbMessage.Text = "กรุณาเลือกรายการบริการ"; return; }
            if (txtMapPackageCompCode.Text.Length <= 0) { lfSbMessage.Text = "รหัสPackage ไม่ถูกต้อง"; return; }
            String re = bc.bcDB.pm39DB.updateCompCode(txtMapPackagepackageCode.Text.Trim(), txtMapPackagepackageType.Text.Trim(), txtMapPackageCompCode.Text.Trim(), bc.userId);
            if(int.Parse(re) > 0)
            {
                lfSbMessage.Text = "บันทึกรายการสำเร็จ";
                setGrfPackage(txtMapPackagePackagesearch.Text.Trim(),"");
            }
            else
            {
                lfSbMessage.Text = "บันทึกรายการไม่สำเร็จ";
            }
        }
        private void TxtMapPackagePackagesearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                String txt = txtMapPackagePackagesearch.Text.Trim();
                if (!txt.Equals(""))
                {
                    setGrfPackage(txt,"");
                    setGrfMapPackageViewhelp(txtMapPackageCompCode.Text.Trim());
                    c1Label9.Value = "Package ที่เคยเลือกของ " + txtMapPackageCompName.Text.Trim();
                }
            }
        }
        private void TxtMapPackageSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                String txt = txtMapPackageSearch.Text.Trim();
                if (!txt.Equals(""))
                {
                    setGrfMapPackage(txt);
                }
            }
        }
        private void BtnOperPrnQue_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtOperHN.Text.Equals(""))            {                lfSbMessage.Text = "กรุณาใส่ HN";                return;            }
            printQueueDtr();
        }

        private void BtnApmMulti_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(cboApmMulti.Text.Equals("")) {                lfSbMessage.Text = "รายการไม่ได้เลือก";                return;}
            if(txtApmDtr.Text.Equals(""))   {                lfSbMessage.Text = "กรุณาใส่รหัสแพทย์"; txtApmDtr.Focus(); return; }
            if(lbApmDtrName.Text.Equals("")){                lfSbMessage.Text = "กรุณาใส่ชื่อแพทย์ให้ถูกต้อง";                return; }
            String[] apm = cboApmMulti.Text.Split('#');     //ทำนัดใหม่ทุกๆ 1 วัน#จำนวน 7 นัด
            if (apm.Length>1)
            {
                String apmdesc = apm[0].Replace("ทำนัดใหม่ทุกๆ", "").Replace("วัน", "").Trim();
                String apmcnt = apm[1].Replace("จำนวน", "").Replace("นัด", "").Trim();
                int apmcnt1=0, apmperiod=0, apmlimit=0, limit=0;
                DateTime dtampstart = new DateTime();                DateTime dtamp = new DateTime();
                if (!DateTime.TryParse(txtApmMultiDate.Text, out dtampstart)) { lfSbMessage.Text = "วันที่ไม่ถูกต้อง"; return; }
                if (!int.TryParse(apmcnt, out apmcnt1)) { lfSbMessage.Text = "จำนวน นัด ไม่ถูกต้อง"; return; }
                if (!int.TryParse(apmdesc, out apmperiod)) { lfSbMessage.Text = "ระยะห่าง วัน ไม่ถูกต้อง"; return; }
                if (!int.TryParse(lbDtrApmLimit.Text.Trim().Replace("limit", "").Trim(), out apmlimit)) { apmlimit = 50; }
                apmlimit = 5;       //debug
                String dtrcode = txtApmDtr.Text.Trim();
                if (grfApmMulti == null){                    initGrfApmMulti();                }
                grfApmMulti.Rows.Count = apmcnt1+1;
                for (int i = 1; i <= apmcnt1; i++)
                {
                    dtamp = dtampstart.AddDays(apmperiod * i); if (dtamp.Year < 2000) { dtamp = dtamp.AddYears(543); }
                    String cnt = bc.bcDB.pt07DB.countAppointmentByDtrDate(dtrcode, dtamp.ToString("yyyy-MM-dd"),"1");
                    limit = bc.bcDB.pt07DB.countApmByCount(cnt);
                    if(apmlimit>0 && limit>=apmlimit)
                    {
                        for(int j=1;j<=7;j++)
                        {
                            dtamp = dtamp.AddDays(1);
                            cnt = bc.bcDB.pt07DB.countAppointmentByDtrDate(dtrcode, dtamp.ToString("yyyy-MM-dd"), "1");
                            limit = bc.bcDB.pt07DB.countApmByCount(cnt);
                            if(limit < apmlimit){               break;                            }
                        }
                        //lfSbMessage.Text = "แพทย์ "+ lbApmDtrName.Text + " มีนัดเต็มวันที่ "+ dtamp.ToString("dd-MM-yyyy") + " กรุณาเลือกวันอื่น";
                        //return;
                    }
                    Row arow = grfApmMulti.Rows[i];
                    arow[0] = (i).ToString();
                    arow[1] = dtamp.ToString("dd-MM-yyyy");
                    arow[2] = cnt;
                    arow[3] = dtrcode;
                    arow[4] = lbApmDtrName.Text;
                }
            }
        }
        
        private void CboOperRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            String[] roomcode = cboOperRoom.Text.Split('#');
            if (roomcode.Length > 1)
            {
                String[] dtr = roomcode[1].Split('[');
                if(dtr.Length>1)
                {
                    txtOperDtr.Value = dtr[0];
                }
                //txtOperDtr.Value = roomcode[1];
                lbOperDtrName.Text = roomcode[0];
            }
        }
        private void CboOperRoom_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if(pageLoad) return;
            //String[] roomcode = cboOperRoom.Text.Split('#');
            //if (roomcode.Length > 1)
            //{
            //    txtOperDtr.Value = roomcode[1];
            //    lbOperDtrName.Text = roomcode[0];
            //}
        }
        private void BtnCheckUPChk1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //check ว่า symptomมีคำว่า สุขภาพ หรือไม่         selectVisitByHn2        "and t01.MNC_STS <> 'C' and t01.mnc_shif_memo like '%สุขภาพ%'" +
            sep.Clear();
            if (SYMPTOMS.IndexOf("สุขภาพ")<0)            {                lfSbMessage.Text = ("ไม่มีคำว่า สุขภาพ ในอาการสำคัญ"); sep.SetError(btnCheckUPChk1, "ไม่มีคำว่า สุขภาพ ในอาการสำคัญ");  return;            }
            FrmOPD2CheckUP frm = new FrmOPD2CheckUP(bc, HN);
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void BtnCertMedUploadNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            saveCertMed("new");
        }

        private void BtnCertMedUpload_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            saveCertMed("");
        }
        private void saveCertMed(String flagnew)
        {
            String folderPath = bc.iniC.pathImageScan;
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            string[] files = Directory.GetFiles(folderPath);
            foreach (String file in files)
            {
                string ext = Path.GetExtension(file);
                String dgssname = "", vn = "", an = "";
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_group_id = "1100000007";
                dsc.hn = txtSrcHn.Text;
                dsc.visit_date = VSDATE;
                dsc.pre_no = PRENO;
                dsc.ml_fm = "";
                //comment ไว้ ยังทำไม่เวร็จ
                //DocScan dsc1 = bc.bcDB.dscDB.selectByVn
                //    dsc.visit_date = this.dsc.visit_date;
                //    dsc.pre_no = this.dsc.pre_no;
                if(flagnew.Equals("new"))
                {
                    dsc.doc_scan_id = "";
                    dsc.vn = VN;
                    dsc.status_ipd = "O";
                    dsc.row_no = "0";
                    dsc.row_cnt = "";
                    dsc.status_ml = "";
                }
                else
                {
                    DocScan dsc1 = bc.bcDB.dscDB.selectByPk(txtdocNo.Text.Trim());
                    bc.bcDB.dscDB.voidDocScanCertMed(txtdocNo.Text.Trim(), "screencaptureupload");
                    dsc.pre_no = dsc1.pre_no;
                    dsc.visit_date = dsc1.visit_date;
                    dsc.vn = dsc1.vn;
                    dsc.status_ipd = dsc1.status_ipd;
                    dsc.row_no = dsc1.row_no;
                    dsc.row_cnt = dsc1.row_cnt;
                    dsc.status_ml = dsc1.status_ml;
                }
                dsc.host_ftp = bc.iniC.hostFTP;
                ////dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                //dsc.image_path = "";
                dsc.doc_group_sub_id = "1200000030";

                //dsc.an_date = "";
                dsc.folder_ftp = bc.iniC.folderFTP;
                

                String re = bc.bcDB.dscDB.insertScreenCapture(dsc, bc.userId);

                ////sB11.Text = " filename " + filename + " bc.iniC.folderFTP " + bc.iniC.folderFTP + "//" + dsc.image_path;
                long chk = 0;
                if (long.TryParse(re, out chk))
                {
                    //dsc.image_path = txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "-" + re + ext;         //+1
                    dsc.image_path = txtSrcHn.Text.Replace("/", "-") + "//" + txtSrcHn.Text.Replace("/", "-") + "-" + re + ext;
                    String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                    //    if (CERTID.Length > 0) { String re2 = bc.bcDB.mcertiDB.updateDocScanIdScanUploadByPk("555" + CERTID, re); }
                    //    //MessageBox.Show("filename" + filename + "\n bc.iniC.folderFTP " + bc.iniC.folderFTP + "//" + dsc.image_path, "");
                    //    //FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                    FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive, bc.iniC.ProxyProxyType, bc.iniC.ProxyHost, bc.iniC.ProxyPort);
                    //    //MessageBox.Show("HN "+ txtHn.Text.Replace("/", "-"), "");
                    //ftp.createDirectory(txtHn.Text);
                    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtSrcHn.Text.Replace("/", "-"));
                    //    //MessageBox.Show("222", "");
                    ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                    //    //MessageBox.Show("333", "");

                    if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, file))
                    {
                        File.Delete(file);
                        //        System.Threading.Thread.Sleep(200);
                        //        this.Dispose();
                    }
                }
                break;      //ใช้ break ดีกว่าเพื่อไม่ให้วนลูป ถ้ามี file มากกว่า 1 file
            }
            setGrfCertMed();
        }
        private void BtnCertMedGet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            getScanCertMed();
        }
        private void getScanCertMed()
        {
            String folderPath = bc.iniC.pathImageScan;
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            string[] files = Directory.GetFiles(folderPath);
            foreach (String file in files)
            {
                pnCertMedView.Controls.Clear();
                try
                {
                    C1FlexViewer fv = new C1FlexViewer();
                    fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
                    fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
                    fv.Dock = System.Windows.Forms.DockStyle.Fill;
                    fv.Location = new System.Drawing.Point(0, 0);
                    fv.Name = "fvSrcCertMEdScan";
                    fv.Size = new System.Drawing.Size(1065, 790);
                    fv.TabIndex = 0;
                    fv.Ribbon.Minimized = true;
                    pnCertMedView.Controls.Add(fv);
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();

                    var pdf = new C1PdfDocument();
                    Image img = Image.FromFile(file);
                    // Replace this line in GrfCertMed_DoubleClick:
                    // pdf.DrawImage(img, RectangleF.FromLTRB(0, 0, img.Width, img.Height));

                    // With the following code to fit the image to the A4 page size:

                    var pageWidth = 595f;  // A4 width in points (8.27 inch * 72)
                    var pageHeight = 842f; // A4 height in points (11.69 inch * 72)
                    float imgAspect = (float)img.Width / img.Height;
                    float pageAspect = pageWidth / pageHeight;
                    float drawWidth, drawHeight, offsetX, offsetY;

                    if (imgAspect > pageAspect)
                    {
                        // Image is wider relative to page
                        drawWidth = pageWidth;
                        drawHeight = pageWidth / imgAspect;
                        offsetX = 0;
                        offsetY = (pageHeight - drawHeight) / 2;
                    }
                    else
                    {
                        // Image is taller relative to page
                        drawHeight = pageHeight - 40;
                        drawWidth = pageHeight * imgAspect;
                        offsetX = (((pageWidth - drawWidth) / 2));
                        //offsetX += (float)(0.6);
                        offsetY = (float)0.6;
                    }
                    pdf.DrawImage(img, new RectangleF(offsetX, offsetY, drawWidth, drawHeight));
                    //img.Dispose();
                    //pdf.DrawImage(img, RectangleF.FromLTRB(0, 0, img.Width, img.Height));
                    string tempPdf = Path.GetTempFileName() + ".pdf";
                    pdf.Save(tempPdf);
                    pds.LoadFromFile(tempPdf);
                    fv.DocumentSource = pds;
                    img.Dispose();
                    break;
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD BtnCertMedGet_Click " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnCertMedGet_Click", ex.Message);
                }
                continue;
            }
        }
        private void BtnCertMedScan_Click(object sender, EventArgs e)
        {
            // Replace the abstract 'CommonDialog' with a concrete implementation.  
            new LogWriter("d", "FrmOPD BtnCertMedScan_Click 00");
            var dialog = new WIA.CommonDialog(); // Ensure the WIA namespace is correctly referenced.  
            //DeviceManager deviceManager = new DeviceManager();
            //DeviceInfo availableScanner = null;
            //Device device = null;
            
            //foreach (DeviceInfo info in deviceManager.DeviceInfos)
            //{
            //    if (info.Type == WiaDeviceType.ScannerDeviceType)
            //    {
            //        // แสดงชื่อ scanner
            //        string scannerName = info.Properties["Name"].get_Value().ToString();
            //        availableScanner = info;
            //        if (scannerName.Equals(""))
            //        {
            //            device = info.Connect();
            //            var item = device.Items[1];
            //            var imgFile = (ImageFile)dialog.ShowTransfer(item, WIA_FORMAT_JPEG, false);
            //            String folderPath = bc.iniC.pathImageScan;
            //    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            //    String filename = folderPath + "//" + txtSrcHn.Text.Replace("/", "-") + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            //    imgFile.SaveFile(filename);
            //    getScanCertMed();
            //            break;
            //        }
            //    }
            //}

            var device = dialog.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, false);
            //foreach(WIA.Device device in dialog)
            //{
            //    new LogWriter("d", "FrmOPD BtnCertMedScan_Click 00.1 " + di.Name);
            //}
            new LogWriter("d", "FrmOPD BtnCertMedScan_Click 01");
            if (device != null)
            {
                new LogWriter("d", "FrmOPD BtnCertMedScan_Click 02");
                var item = device.Items[1];
                new LogWriter("d", "FrmOPD BtnCertMedScan_Click 03");
                var imgFile = (ImageFile)dialog.ShowTransfer(item, WIA_FORMAT_JPEG, false);
                
                String folderPath = bc.iniC.pathImageScan;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                String filename = folderPath + "//" + txtSrcHn.Text.Replace("/", "-") + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                imgFile.SaveFile(filename);
                getScanCertMed();
            }
        }
        private void BtnOperLungSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = bc.bcDB.vsDB.updateLung(txtOperHN.Text.Trim(), PRENO, VSDATE, txtOperLung.Text.Trim(), cboOperLung.Text.Trim());
        }
        private void BtnOperSaveStaffNote_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String packname = "", packcode = "";
            if((VS.preno==null) && (VS.VisitDate == null)){                lfSbStatus.Text = "preno null vsdate null";                return;            }
            if ((VS.preno.Length == 0) && (VS.VisitDate.Length == 0)) { lfSbStatus.Text = "preno.len = 0 vsdate.len = 0"; return; }
            bc.genImgStaffNote(PTT, VS, fStaffN, packcode, packname, "");
            lfSbStatus.Text = "บันทึกใบยาเรียบร้อย";
        }
        private void BtnOperEyeSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = bc.bcDB.vsDB.updateEye(txtOperHN.Text.Trim(), PRENO, VSDATE, txtOperLeftEye.Text.Trim(), txtOperRightEye.Text.Trim(), txtOperLeftEyePh.Text.Trim(), txtOperRightEyePh.Text.Trim(), cboOperEye.Text.Trim());
        }
        private void BtnOperEarSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String rear = chkOperLeftEarNormal.Checked ? "1" : "0";
            String lear = chkOperRightEarNormal.Checked ? "1" : "0";
            String re = bc.bcDB.vsDB.updateEar(txtOperHN.Text.Trim(), PRENO, VSDATE, txtOperLeftEar.Text.Trim(), txtOperRightEar.Text.Trim(), rear, lear, txtOperLeftEarOther.Text.Trim(), txtOperRightEarOther.Text.Trim());
        }
        private void BtnSrcHolterScanSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DocScan dsc = new DocScan();
            dsc.active = "1";
            dsc.doc_scan_id = "";
            dsc.doc_group_id = "1100000007";
            dsc.hn = txtSrcHn.Text;
            dsc.an = "";
            dsc.vn = lbSrcVN.Text;
            dsc.visit_date = lbSrcVsDate.Text;
            dsc.host_ftp = bc.iniC.hostFTP;
            //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
            dsc.image_path = "";
            dsc.doc_group_sub_id = "1200000042";
            dsc.pre_no = PRENO;
            dsc.an_date = "";
            dsc.folder_ftp = bc.iniC.folderFTP;
            dsc.status_ipd = "O";
            dsc.row_no = "1";
            dsc.row_cnt = "1";
            dsc.status_ml = "2";
            dsc.ml_fm = "FM-MED-002";
            dsc.remark = "PDF";
            dsc.sort1 = "1";

            String reDocScanId = bc.bcDB.dscDB.insertEKG(dsc, bc.userId);
            long chk = 0;
            if (long.TryParse(reDocScanId, out chk))
            {
                String folderPath = bc.iniC.pathlocalHolter;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                String[] files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    foreach (String file1 in files)
                    {
                        dsc.image_path = txtSrcHn.Text.Replace("/", "-") + "//" + txtSrcHn.Text.Replace("/", "-") + "-" + reDocScanId + ".PDF";
                        String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, reDocScanId);
                        FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive, bc.iniC.ProxyProxyType, bc.iniC.ProxyHost, bc.iniC.ProxyPort);
                        ftp.createDirectory(bc.iniC.folderFTP + "//" + txtSrcHn.Text.Replace("/", "-"));
                        ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                        if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, file1))
                        {
                            File.Delete(file1);
                            break;
                        }
                    }
                }
            }
            setGrfHolter();
        }

        private void BtnSrcHolterScanNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pnHolterView.Controls.Clear();
            C1FlexViewer fv = new C1FlexViewer();
            fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
            fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            fv.Dock = System.Windows.Forms.DockStyle.Fill;
            fv.Location = new System.Drawing.Point(0, 0);
            fv.Name = "fvSrcHolterScan";
            fv.Size = new System.Drawing.Size(1065, 790);
            fv.TabIndex = 0;
            fv.Ribbon.Minimized = true;
            pnHolterView.Controls.Add(fv);
            try
            {
                C1PdfDocumentSource pds = new C1PdfDocumentSource();
                String folderPath = bc.iniC.pathlocalHolter;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                String[] files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    foreach (String file1 in files)
                    {
                        pds.LoadFromFile(file1);
                        fv.DocumentSource = pds;
                        btnSrcEKGScanSave.Enabled = true;
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", "FrmOPD BtnSrcHolterScanNew_Click " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnSrcHolterScanNew_Click", ex.Message);
            }
        }

        private void BtnSrcESTScanSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DocScan dsc = new DocScan();
            dsc.active = "1";
            dsc.doc_scan_id = "";
            dsc.doc_group_id = "1100000007";
            dsc.hn = txtSrcHn.Text;
            dsc.an = "";
            dsc.vn = lbSrcVN.Text;
            dsc.visit_date = lbSrcVsDate.Text;
            dsc.host_ftp = bc.iniC.hostFTP;
            //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
            dsc.image_path = "";
            dsc.doc_group_sub_id = "1200000040";
            dsc.pre_no = PRENO;
            dsc.an_date = "";
            dsc.folder_ftp = bc.iniC.folderFTP;
            dsc.status_ipd = "O";
            dsc.row_no = "1";
            dsc.row_cnt = "1";
            dsc.status_ml = "2";
            dsc.ml_fm = "FM-MED-002";
            dsc.remark = "PDF";
            dsc.sort1 = "1";

            String reDocScanId = bc.bcDB.dscDB.insertEKG(dsc, bc.userId);
            long chk = 0;
            if (long.TryParse(reDocScanId, out chk))
            {
                String folderPath = bc.iniC.pathlocalEST;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                String[] files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    foreach (String file1 in files)
                    {
                        dsc.image_path = txtSrcHn.Text.Replace("/", "-") + "//" + txtSrcHn.Text.Replace("/", "-") + "-" + reDocScanId + ".PDF";
                        String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, reDocScanId);
                        FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive, bc.iniC.ProxyProxyType, bc.iniC.ProxyHost, bc.iniC.ProxyPort);
                        ftp.createDirectory(bc.iniC.folderFTP + "//" + txtSrcHn.Text.Replace("/", "-"));
                        ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                        if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, file1))
                        {
                            File.Delete(file1);
                            break;
                        }
                    }
                }
            }
            setGrfEST();
        }
        private void BtnSrcESTScanNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pnESTView.Controls.Clear();
            C1FlexViewer fv = new C1FlexViewer();
            fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
            fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            fv.Dock = System.Windows.Forms.DockStyle.Fill;
            fv.Location = new System.Drawing.Point(0, 0);
            fv.Name = "fvSrcESTScan";
            fv.Size = new System.Drawing.Size(1065, 790);
            fv.TabIndex = 0;
            fv.Ribbon.Minimized = true;
            pnESTView.Controls.Add(fv);
            try
            {
                C1PdfDocumentSource pds = new C1PdfDocumentSource();
                String folderPath = bc.iniC.pathlocalEST;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                String[] files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    foreach (String file1 in files)
                    {
                        pds.LoadFromFile(file1);
                        fv.DocumentSource = pds;
                        btnSrcEKGScanSave.Enabled = true;
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", "FrmOPD BtnSrcESTScanNew_Click " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnSrcESTScanNew_Click", ex.Message);
            }
        }
        private void BtnSrcECHOScanSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DocScan dsc = new DocScan();
            dsc.active = "1";
            dsc.doc_scan_id = "";
            dsc.doc_group_id = "1100000007";
            dsc.hn = txtSrcHn.Text;
            dsc.an = "";
            dsc.vn = lbSrcVN.Text;
            dsc.visit_date = lbSrcVsDate.Text;
            dsc.host_ftp = bc.iniC.hostFTP;
            //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
            dsc.image_path = "";
            dsc.doc_group_sub_id = "1200000041";
            dsc.pre_no = PRENO;
            dsc.an_date = "";
            dsc.folder_ftp = bc.iniC.folderFTP;
            dsc.status_ipd = "O";
            dsc.row_no = "1";
            dsc.row_cnt = "1";
            dsc.status_ml = "2";
            dsc.ml_fm = "FM-MED-002";
            dsc.remark = "PDF";
            dsc.sort1 = "1";

            String reDocScanId = bc.bcDB.dscDB.insertEKG(dsc, bc.userId);
            long chk = 0;
            if (long.TryParse(reDocScanId, out chk))
            {
                String folderPath = bc.iniC.pathlocalECHO;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                String[] files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    foreach (String file1 in files)
                    {
                        dsc.image_path = txtSrcHn.Text.Replace("/", "-") + "//" + txtSrcHn.Text.Replace("/", "-") + "-" + reDocScanId + ".PDF";
                        String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, reDocScanId);
                        FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive, bc.iniC.ProxyProxyType, bc.iniC.ProxyHost, bc.iniC.ProxyPort);
                        ftp.createDirectory(bc.iniC.folderFTP + "//" + txtSrcHn.Text.Replace("/", "-"));
                        ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                        if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, file1))
                        {
                            File.Delete(file1);
                            break;
                        }
                    }
                }
            }
            setGrfECHO();
        }
        private void BtnSrcECHOScanNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pnECHOView.Controls.Clear();
            C1FlexViewer fv = new C1FlexViewer();
            fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
            fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            fv.Dock = System.Windows.Forms.DockStyle.Fill;
            fv.Location = new System.Drawing.Point(0, 0);
            fv.Name = "fvSrcECHOScan";
            fv.Size = new System.Drawing.Size(1065, 790);
            fv.TabIndex = 0;
            fv.Ribbon.Minimized = true;
            pnECHOView.Controls.Add(fv);
            try
            {
                C1PdfDocumentSource pds = new C1PdfDocumentSource();
                String folderPath = bc.iniC.pathlocalECHO;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                String[] files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    foreach (String file1 in files)
                    {
                        pds.LoadFromFile(file1);
                        fv.DocumentSource = pds;
                        btnSrcEKGScanSave.Enabled = true;
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", "FrmOPD BtnSrcECHOScanNew_Click " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnSrcECHOScanNew_Click", ex.Message);
            }
        }

        private void BtnSrcDocOLDSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DocScan dsc = new DocScan();
            dsc.active = "1";
            dsc.doc_scan_id = "";
            dsc.doc_group_id = "1100000007";
            dsc.hn = txtSrcHn.Text;
            dsc.an = "";
            dsc.vn = lbSrcVN.Text;
            dsc.visit_date = lbSrcVsDate.Text;
            dsc.host_ftp = bc.iniC.hostFTP;
            //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
            dsc.image_path = "";
            dsc.doc_group_sub_id = "1200000039";
            dsc.pre_no = PRENO;
            dsc.an_date = "";
            dsc.folder_ftp = bc.iniC.folderFTP;
            dsc.status_ipd = "O";
            dsc.row_no = "1";
            dsc.row_cnt = "1";
            dsc.status_ml = "2";
            dsc.ml_fm = "FM-MED-003";
            dsc.remark = "PDF";
            dsc.sort1 = "1";

            String reDocScanId = bc.bcDB.dscDB.insertEKG(dsc, bc.userId);       //ใช้ insertEKG แต่ให้ เป็น doc_group_sub_id = "1200000039" เพื่อให้แยกจาก EKG
            long chk = 0;
            if (long.TryParse(reDocScanId, out chk))
            {
                String folderPath = bc.iniC.pathlocalDocOLD;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                String[] files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    foreach (String file1 in files)
                    {
                        dsc.image_path = txtSrcHn.Text.Replace("/", "-") + "//" + txtSrcHn.Text.Replace("/", "-") + "-" + reDocScanId + ".PDF";
                        String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, reDocScanId);
                        FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive, bc.iniC.ProxyProxyType, bc.iniC.ProxyHost, bc.iniC.ProxyPort);
                        ftp.createDirectory(bc.iniC.folderFTP + "//" + txtSrcHn.Text.Replace("/", "-"));
                        ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                        if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, file1))
                        {
                            File.Delete(file1);
                            break;
                        }
                    }
                }
            }
            setGrfDocOLD();
        }

        private void BtnSrcDocOLDNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pnDocOLDView.Controls.Clear();
            C1FlexViewer fv = new C1FlexViewer();
            fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
            fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            fv.Dock = System.Windows.Forms.DockStyle.Fill;
            fv.Location = new System.Drawing.Point(0, 0);
            fv.Name = "fvSrcDocOLDScan";
            fv.Size = new System.Drawing.Size(1065, 790);
            fv.TabIndex = 0;
            fv.Ribbon.Minimized = true;
            pnDocOLDView.Controls.Add(fv);
            try
            {
                C1PdfDocumentSource pds = new C1PdfDocumentSource();
                String folderPath = bc.iniC.pathlocalDocOLD;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                String[] files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    foreach (String file1 in files)
                    {
                        pds.LoadFromFile(file1);
                        fv.DocumentSource = pds;
                        btnSrcEKGScanSave.Enabled = true;
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", "FrmOPD BtnSrcDocOLDNew_Click " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnSrcDocOLDNew_Click", ex.Message);
            }
        }

        private void BtnSrcEKGScanSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DocScan dsc = new DocScan();
            dsc.active = "1";
            dsc.doc_scan_id = "";
            dsc.doc_group_id = "1100000007";
            dsc.hn = txtSrcHn.Text;
            dsc.an = "";
            dsc.vn = lbSrcVN.Text;
            dsc.visit_date = lbSrcVsDate.Text;
            dsc.host_ftp = bc.iniC.hostFTP;
            //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
            dsc.image_path = "";
            dsc.doc_group_sub_id = "1200000038";
            dsc.pre_no = PRENO;
            dsc.an_date = "";
            dsc.folder_ftp = bc.iniC.folderFTP;
            dsc.status_ipd = "O";
            dsc.row_no = "1";
            dsc.row_cnt = "1";
            dsc.status_ml = "2";
            dsc.ml_fm = "FM-MED-002";
            dsc.remark = "PDF";
            dsc.sort1 = "1";
            
            String reDocScanId = bc.bcDB.dscDB.insertEKG(dsc, bc.userId);
            long chk = 0;
            if (long.TryParse(reDocScanId, out chk))
            {
                String folderPath = bc.iniC.pathlocalEKG;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                String[] files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    foreach (String file1 in files)
                    {
                        dsc.image_path = txtSrcHn.Text.Replace("/", "-") + "//" + txtSrcHn.Text.Replace("/", "-") + "-" + reDocScanId + ".PDF";
                        String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, reDocScanId);
                        FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive, bc.iniC.ProxyProxyType, bc.iniC.ProxyHost, bc.iniC.ProxyPort);
                        ftp.createDirectory(bc.iniC.folderFTP + "//" + txtSrcHn.Text.Replace("/", "-"));
                        ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                        if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, file1))
                        {
                            File.Delete(file1);
                            break;
                        }
                    }
                }
            }
            setGrfEKG();
        }
        private void BtnSrcEKGScanNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pnEKGView.Controls.Clear();
            C1FlexViewer fv = new C1FlexViewer();
            fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
            fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            fv.Dock = System.Windows.Forms.DockStyle.Fill;
            fv.Location = new System.Drawing.Point(0, 0);
            fv.Name = "fvSrcEKGScan";
            fv.Size = new System.Drawing.Size(1065, 790);
            fv.TabIndex = 0;
            fv.Ribbon.Minimized = true;
            pnEKGView.Controls.Add(fv);
            try
            {
                C1PdfDocumentSource pds = new C1PdfDocumentSource();
                String folderPath = bc.iniC.pathlocalEKG;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                String[] files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    foreach (String file1 in files)
                    {
                        pds.LoadFromFile(file1);
                        fv.DocumentSource = pds;
                        btnSrcEKGScanSave.Enabled = true;
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", "FrmOPD BtnSrcEKGScanNew_Click " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnSrcEKGScanNew_Click", ex.Message);
            }
        }

        private void BtnScanClearImg_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                String folderPath = bc.iniC.pathlocalStaffNote;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                string[] files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    foreach (String file1 in files)
                    {
                        File.Delete(file1);
                    }
                }
            }
            catch(Exception ex)
            {
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", "FrmOPD BtnScanClearImg_Click " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnScanClearImg_Click", ex.Message);
            }
        }

        private void TxtApmVsNewHn_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as C1TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private void TxtApmVsNewHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                chkApmVsNew.Checked = true;
                PatientT07 apm = new PatientT07();
                apm = bc.bcDB.pt07DB.selectAppointmentToday(txtApmVsNewHn.Text.Trim());
                if(apm.MNC_HN_NO.Length<=0) { apm.MNC_HN_NO = txtApmVsNewHn.Text.Trim(); }
                //if(apm.MNC_HN_NO.Length<=0)                {                    lfSbMessage.Text = "ไม่พบ รายการนี้";                    return;                }
                FrmApmVisitNew frm = new FrmApmVisitNew(bc, apm);
                frm.ShowDialog(this);
                frm.Dispose();
            }
        }
        private void BtnCheckUPExcelAlien_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmDoeAlien frm = new FrmDoeAlien(bc, txtCheckUPRegcode.Text.Trim(), "excel", txtCheckUPHN.Text.Trim(), VSDATE, PRENO);
            frm.ShowDialog(this);
            frm.Dispose();
        }
        private void TxtApmHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //HN = txtApmHn.Text.Trim();
                setGrfApm();
            }
        }
        private void BtnCheckUPSaveStaffNote_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String packname = "", packcode="";
            if (grfChkPackItems.Rows.Count > 0)
            {
                if (chkCheckUPSelect.Checked && ((ComboBoxItem)cboCheckUPSelect.SelectedItem).Value.ToString().Equals("doeonline"))
                {
                    packcode = ((ComboBoxItem)cboCheckUPOrder.SelectedItem).Value.ToString();
                    String txt2 = cboCheckUPOrder.Text;
                }
                foreach (Row row in grfChkPackItems.Rows)
                {
                    if (row[colChkPackItemsname] == null) continue;
                    if (row[colChkPackItemsname].ToString().Length <= 0) continue;
                    if (row[colChkPackItemsname].ToString().Equals("name")) continue;
                    packname = row[colChkPackItemsname].ToString();
                }
            }
            bc.genImgStaffNote(PTT, VS, fStaffN, packcode, packname, "ค่าใบรับรองแพทย์");
            lfSbStatus.Text = "บันทึกใบยาเรียบร้อย";
        }
        private void BtnPrnXray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void BtnPrnLAB_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void TxtOperHN_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //แก้ให้ สามารถ ค้นหาโดยใช้ ชื่อ pid ได้   68-12-01
                HN = txtOperHN.Text.Trim();
                setControlOper(txtOperHN.Name);
            }
        }
        private void BtnCheckUPDoeView_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmDoeAlien frm = new FrmDoeAlien(bc, txtCheckUPRegcode.Text.Trim(), "viewdoe", txtCheckUPHN.Text.Trim(), VSDATE, PRENO);
            frm.ShowDialog(this);
            frm.Dispose();
        }
        private void BtnCheckUPPrnStaffNoteDoe_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printStaffNote("checkup_doe");
            txtOperRemark.Focus();
        }
        private void BtnCheckUPFolder_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmDoeAlien frm = new FrmDoeAlien(bc, txtCheckUPRegcode.Text.Trim(), "formfolder", txtCheckUPHN.Text.Trim(), VSDATE, PRENO);
            frm.ShowDialog(this);
            frm.Dispose();
        }
        private void BtnSendDOE_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmDoeAlien frm = new FrmDoeAlien(bc, txtCheckUPRegcode.Text.Trim(), "senddoe",txtCheckUPHN.Text.Trim(), VSDATE, PRENO);
            frm.ShowDialog(this);
        }
        private void BtnCheckUPSaveVital_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Visit vs = new Visit();
            vs = setCheckUPVitalsign();
            vs.preno = PRENO;
            vs.VisitDate = VSDATE;
            vs.HN = txtCheckUPHN.Text.Trim();
            String re = bc.bcDB.vsDB.updateVitalSign(vs, "1618");
            String re1 = bc.bcDB.vsDB.updateActNo113(txtCheckUPHN.Text.Trim(), PRENO, VSDATE);
            if (long.TryParse(re, out long chk))
            {
                lfSbMessage.Text = "update vitalsign OK";
            }
        }
        
        private Visit setCheckUPVitalsign()
        {
            Visit vs = new Visit();
            vs.temp = txtCheckUPTempu.Text;
            vs.weight = txtCheckUPWeight.Text;
            vs.high = txtCheckUPHeight.Text;
            vs.hrate = txtCheckUPHrate.Text.Trim();
            vs.rrate = txtCheckUPRrate.Text.Trim();
            vs.bp1l = txtCheckUPBp1L.Text;
            vs.abc = txtCheckUPABOGroup.Text;
            
            //vs.bp1l = "0";
            vs.bp1r = "0";
            vs.bp2l = "0";
            vs.bp2r = "0";
            //vs.abc = "0";
            
            vs.hc = "0";
            vs.cc = "0";
            vs.ccin ="0";
            vs.ccex = "0";

            return vs;
        }
        private void BtnCheckUPSaveDtr_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = bc.bcDB.vsDB.updateDoctor(txtCheckUPHN.Text.Trim(), PRENO, VSDATE, txtCheckUPDoctorId.Text.Trim(), "1618");
            String re1 = bc.bcDB.sumt03DB.insertSummaryT03(txtCheckUPDoctorId.Text.Trim(), bc.iniC.station);
            String re2 = bc.bcDB.vsDB.updateActNoSendToDoctor110(txtCheckUPHN.Text.Trim(), PRENO, VSDATE);
            if (long.TryParse(re, out long chk))
            {
                lfSbMessage.Text = "update Doctor OK";
            }
        }
        private void BtnCheckUPAlienGetResult_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmDoeAlien frm = new FrmDoeAlien(bc, txtCheckUPRegcode.Text.Trim(),"namelist","","","");
            frm.ShowDialog(this);
            frm.Dispose();
        }
        private void CboCheckUPSelect_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(pageLoad) return;
            if(chkCheckUPSelect.Checked)
            {
                if(((ComboBoxItem)cboCheckUPSelect.SelectedItem).Value.ToString().Equals("doeonline")) bc.bcDB.pm39DB.setCboPrefixT(cboCheckUPOrder,bc.iniC.compcodedoe,"");
                setGrfCheckUPList(cboCheckUPSelect.SelectedItem == null ? "" : ((ComboBoxItem)cboCheckUPSelect.SelectedItem).Value.ToString());
            }
        }
        private void BtnCheckUPAlienPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            genCheckUPAlienPDF();
        }
        private String insertCertDoctorCheckUp(String flagedit)
        {
            String certid = "";
            MedicalCertificate mcerti = new MedicalCertificate();
            mcerti.active = "1";
            mcerti.an = "";
            mcerti.certi_id = "";
            mcerti.certi_code = "";
            mcerti.dtr_code = txtCheckUPDoctorId.Text.Trim();
            mcerti.dtr_name_t = txtCheckUPDoctorName.Text;
            mcerti.status_ipd = "O";
            mcerti.visit_date = VSDATE;
            mcerti.visit_time = "";
            mcerti.remark = "";
            mcerti.line1 = "";
            mcerti.line2 = "";
            mcerti.line3 = "";
            mcerti.line4 = "";
            mcerti.hn = txtCheckUPHN.Text;
            mcerti.pre_no = PRENO;
            mcerti.ptt_name_e = cboCheckUPPrefixT.Text + " " + txtCheckUPNameT.Text.Trim() + " " + txtCheckUPSurNameT.Text.Trim();
            mcerti.ptt_name_t = "";
            mcerti.doc_scan_id = "";
            mcerti.status_2nd_leaf = "1";
            mcerti.counter_name = bc.iniC.station;
            bc.bcDB.mcertiDB.insertMedicalCertificate(mcerti, "");
            if (flagedit.Equals("edit"))
            {
                certid = bc.bcDB.mcertiDB.selectCertIDByEditVisitDate(txtCheckUPHN.Text.Trim());
            }
            else
            {
                certid = bc.bcDB.mcertiDB.selectCertIDByHn(txtCheckUPHN.Text.Trim(), PRENO, VSDATE);      //มีปัญหาเรื่องแก้ไขวันที่ ในการค้นหา
            }
            return certid;
        }
        
        private void DropDownItem8_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TEMPCANCER = true;            printStaffNoteTemplate("8");
        }

        private void DropDownItem7_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TEMPCANCER = true;            printStaffNoteTemplate("7");
        }

        private void DropDownItem6_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TEMPCANCER = true;            printStaffNoteTemplate("6");
        }

        private void DropDownItem5_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TEMPCANCER = true;            printStaffNoteTemplate("5");
        }

        private void DropDownItem27_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TEMPCANCER = true;            printStaffNoteTemplate("27");
        }
        private void DropDownItem4_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TEMPCANCER = true;            printStaffNoteTemplate("4");
        }

        private void DropDownItem3_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TEMPCANCER = true;            printStaffNoteTemplate("3");
        }
        private void DropDownItem2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TEMPCANCER = true;            printStaffNoteTemplate("2");
        }
        private void DropDownItem1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TEMPCANCER = true;            printStaffNoteTemplate("1");
        }
        private void BtnPrnStaffNote_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //MessageBox.Show("11111", "");
            TEMPCANCER = false;            printStaffNoteTemplate("0");
        }
        private void printStaffNoteTemplate(String temp)
        {
            DTCHRONIC = bc.bcDB.vsDB.SelectChronicByPID(PTT.idcard);
            DTALLERGY = bc.bcDB.vsDB.selectDrugAllergy(txtOperHN.Text.Trim());
            //user แจ้งให้ save doctor ด้วย 68-10-21
            saveDoctor();       //ด้วย 68-10-21
            saveVitalSign();    //ด้วย 66-01-23
            printStaffNote(temp);
            if (bc.iniC.statusPrintQue.Equals("1")) { printQueueDtr(); }
            setGrfOperList("");
        }
        private void BtnOperOpenSticker_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Form frm = new FrmSmartCard(bc, HN, "sticker");
            frm.ShowDialog();
        }
        private void BtnOperPrnSticker_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printTabStkSticker();
        }
        private void printTabStkSticker()
        {
            PrintDocument document = new PrintDocument();            
            document.PrinterSettings.PrinterName = bc.iniC.printerSticker;
            document.PrintPage += Document_PrintPage_tabStkSticker;
            document.DefaultPageSettings.Landscape = false;
            int num = 0;
            if (int.TryParse(txtOperSticker.Text.Trim(), out num))
            {
                document.PrinterSettings.Copies = short.Parse(num.ToString());
                //for(int i = 0; i < num; i++)
                //{
                document.Print();
                //}
            }
            else
            {
                sep.SetError(txtOperSticker, "");
            }
        }
        
        private void BtnCheckUPSSoPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            genCheckUPSSOPDF1();
        }
        
        private void BtnCheckUPSSoGetResult_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            showLbLoading();
            clearControlCheckupSSO();
            DataTable dtres = new DataTable();
            String reqdate = "", reqno = "";
            dtres = bc.bcDB.labT05DB.selectResultCheckSSObyLabCode(txtCheckUPHN.Text.Trim(), "HE001");
            if (dtres.Rows.Count > 0)
            {
                foreach(DataRow drow in dtres.Rows)
                {
                    String labsubcode = "", standard="", unit="", name="";
                    labsubcode = drow["MNC_RES"] !=null? drow["MNC_RES"].ToString():"";
                    standard = drow["MNC_LB_RES"] != null ? drow["MNC_LB_RES"].ToString() : "";
                    unit = drow["MNC_RES_UNT"] != null ? drow["MNC_RES_UNT"].ToString() : "";
                    name = drow["MNC_RES"] != null ? drow["MNC_RES"].ToString() : "";
                    standard = standard.Replace(name, "").Trim().Replace("[","").Replace("]", "").Trim()+ unit.Trim();
                    if (labsubcode.ToLower().Equals("hb"))
                    {
                        txtCheckUPResHB.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResHBStandard.Text = standard;
                    }
                    else if (labsubcode.ToLower().Equals("hct"))
                    {
                        txtCheckUPResHCT.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResHCTStandard.Text = standard;
                    }
                    else if (labsubcode.Equals("Neutrophil"))
                    {
                        txtCheckUPResNeu.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResNeuStandard.Text = standard;
                    }
                    else if (labsubcode.Equals("Lymphocyte"))
                    {
                        txtCheckUPResLym.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResLymStandard.Text = standard;
                    }
                    else if (labsubcode.Equals("Monocyte"))
                    {
                        txtCheckUPResMono.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResMonoStandard.Text = standard;
                    }
                    else if (labsubcode.Equals("Eosinophil"))
                    {
                        txtCheckUPResEos.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResEosStandard.Text = standard;
                    }
                    else if (labsubcode.Equals("Basophil"))
                    {
                        txtCheckUPResBas.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResBasStandard.Text = standard;
                    }
                    else if (labsubcode.Equals("MCV"))
                    {
                        txtCheckUPResMCV.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResMCVStandard.Text = standard;
                    }
                    else if (labsubcode.Equals("WBC  count"))
                    {
                        txtCheckUPResWBC.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResWBCStandard.Text = standard;
                    }
                    else if (labsubcode.Equals("RBC  count"))
                    {
                        txtCheckUPResRBC.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResRBCStandard.Text = standard;
                    }
                    else if (labsubcode.Equals("PLT. count"))
                    {
                        txtCheckUPResPlaCnt.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResPlaCntStandard.Text = standard;
                    }
                }
            }
            dtres = bc.bcDB.labT05DB.selectResultCheckSSObyLabCode(txtCheckUPHN.Text.Trim(), "CH002");
            if (dtres.Rows.Count > 0)
            {
                foreach (DataRow drow in dtres.Rows)
                {
                    String labsubcode = "", standard = "", unit = "", name = "";
                    labsubcode = drow["MNC_RES"] != null ? drow["MNC_RES"].ToString() : "";
                    standard = drow["MNC_LB_RES"] != null ? drow["MNC_LB_RES"].ToString() : "";
                    unit = drow["MNC_RES_UNT"] != null ? drow["MNC_RES_UNT"].ToString() : "";
                    name = drow["MNC_RES"] != null ? drow["MNC_RES"].ToString() : "";
                    standard = standard.Replace(name, "").Trim().Replace("[", "").Replace("]", "").Trim() + unit.Trim();
                    txtCheckUPResFBS.Text = drow["MNC_RES_VALUE"].ToString();
                    txtCheckUPResFBSStandard.Text = standard;
                }
            }
            dtres = bc.bcDB.labT05DB.selectResultCheckSSObyLabCode(txtCheckUPHN.Text.Trim(), "SE038");
            if (dtres.Rows.Count > 0)
            {
                foreach (DataRow drow in dtres.Rows)
                {
                    String labsubcode = "";
                    labsubcode = drow["MNC_RES"] != null ? drow["MNC_RES"].ToString() : "";
                    if (labsubcode.ToLower().Equals("hbsag"))
                    {
                        txtCheckUPResHBsAg.Text = drow["MNC_RES_VALUE"].ToString();
                    }
                }
            }
            dtres = bc.bcDB.labT05DB.selectResultCheckSSObyLabCode(txtCheckUPHN.Text.Trim(), "CH004");
            if (dtres.Rows.Count > 0)
            {
                foreach (DataRow drow in dtres.Rows)
                {
                    String labsubcode = "", standard = "", unit = "", name = "";
                    labsubcode = drow["MNC_RES"] != null ? drow["MNC_RES"].ToString() : "";
                    standard = drow["MNC_LB_RES"] != null ? drow["MNC_LB_RES"].ToString() : "";
                    unit = drow["MNC_RES_UNT"] != null ? drow["MNC_RES_UNT"].ToString() : "";
                    name = drow["MNC_RES"] != null ? drow["MNC_RES"].ToString() : "";
                    standard = standard.Replace(name, "").Trim().Replace("[", "").Replace("]", "").Trim() + unit.Trim();
                    if (labsubcode.ToUpper().IndexOf("CREATININE")>=0)
                    {
                        txtCheckUPResCreatinine.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResCreatinineStandard.Text = standard;
                    }
                    else if (labsubcode.ToLower().IndexOf("egfr")>=0)
                    {
                        txtCheckUPReseGFR.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPReseGFRStandard.Text = standard;
                    }
                }
            }
            dtres = bc.bcDB.labT05DB.selectResultCheckSSObyLabCode(txtCheckUPHN.Text.Trim(), "CH006");
            if (dtres.Rows.Count > 0)
            {
                foreach (DataRow drow in dtres.Rows)
                {
                    String labsubcode = "", standard = "", unit = "", name = "";
                    labsubcode = drow["MNC_RES"] != null ? drow["MNC_RES"].ToString() : "";
                    standard = drow["MNC_LB_RES"] != null ? drow["MNC_LB_RES"].ToString() : "";
                    unit = drow["MNC_RES_UNT"] != null ? drow["MNC_RES_UNT"].ToString() : "";
                    name = drow["MNC_RES"] != null ? drow["MNC_RES"].ToString() : "";
                    standard = standard.Replace(name, "").Trim().Replace("[", "").Replace("]", "").Trim() + unit.Trim();
                    txtCheckUPResCholes.Text = drow["MNC_RES_VALUE"].ToString();
                    txtCheckUPResCholesStandard.Text = standard;
                }
            }
            dtres = bc.bcDB.labT05DB.selectResultCheckSSObyLabCode(txtCheckUPHN.Text.Trim(), "CH008");
            if (dtres.Rows.Count > 0)
            {
                foreach (DataRow drow in dtres.Rows)
                {
                    String labsubcode = "", standard = "", unit = "", name = "";
                    labsubcode = drow["MNC_RES"] != null ? drow["MNC_RES"].ToString() : "";
                    standard = drow["MNC_LB_RES"] != null ? drow["MNC_LB_RES"].ToString() : "";
                    unit = drow["MNC_RES_UNT"] != null ? drow["MNC_RES_UNT"].ToString() : "";
                    name = drow["MNC_RES"] != null ? drow["MNC_RES"].ToString() : "";
                    standard = standard.Replace(name, "").Trim().Replace("[", "").Replace("]", "").Trim() + unit.Trim();
                    txtCheckUPResHDL.Text = drow["MNC_RES_VALUE"].ToString();
                    txtCheckUPResHDLStandard.Text = standard;
                }
            }
            dtres = bc.bcDB.labT05DB.selectResultCheckSSObyLabCode(txtCheckUPHN.Text.Trim(), "MS001");
            if (dtres.Rows.Count > 0)
            {
                foreach (DataRow drow in dtres.Rows)
                {
                    String labsubcode = "", standard = "", unit = "", name = "";
                    labsubcode = drow["MNC_RES"] != null ? drow["MNC_RES"].ToString() : "";
                    standard = drow["MNC_LB_RES"] != null ? drow["MNC_LB_RES"].ToString() : "";
                    unit = drow["MNC_RES_UNT"] != null ? drow["MNC_RES_UNT"].ToString() : "";
                    name = drow["MNC_RES"] != null ? drow["MNC_RES"].ToString() : "";
                    standard = standard.Replace(name, "").Trim().Replace("[", "").Replace("]", "").Trim() + unit.Trim();
                    if (labsubcode.ToLower().Equals("color"))
                    {
                        cboCheckUPResUAColor.Text = drow["MNC_RES_VALUE"].ToString();
                    }
                    else if (labsubcode.ToLower().Equals("appearance"))
                    {
                        cboCheckUPResUAAppea.Text = drow["MNC_RES_VALUE"].ToString();
                    }
                    else if (labsubcode.ToLower().Equals("glucose"))
                    {
                        cboCheckUPResUAGlucose.Text = drow["MNC_RES_VALUE"].ToString();
                    }
                    else if (labsubcode.ToLower().Equals("protein"))
                    {
                        cboCheckUPResUAProtein.Text = drow["MNC_RES_VALUE"].ToString();
                    }
                    else if (labsubcode.ToLower().IndexOf("ketone")>=0)
                    {
                        cboCheckUPResUAKetone.Text = drow["MNC_RES_VALUE"].ToString();
                    }
                    else if (labsubcode.ToUpper().Equals("WBC"))
                    {
                        txtCheckUPResUAWBC.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResUAWBCStandard.Text = standard;
                    }
                    else if (labsubcode.ToUpper().Equals("RBC"))
                    {
                        txtCheckUPResUARBC.Text = drow["MNC_RES_VALUE"].ToString();
                        txtCheckUPResUARBCStandard.Text = standard;
                    }
                }
                dtres = bc.bcDB.labT05DB.selectResultCheckSSObyLabCode(txtCheckUPHN.Text.Trim(), "MS017");
                if (dtres.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtres.Rows)
                    {
                        String labsubcode = "";
                        labsubcode = drow["MNC_RES"] != null ? drow["MNC_RES"].ToString() : "";
                        cboCheckUPResMS017.Text = drow["MNC_RES_VALUE"].ToString();
                    }
                }
            }
            hideLbLoading();
        }
        private void clearControlCheckupSSO()
        {
            txtCheckUPResFBS.Text = "";
            txtCheckUPResHBsAg.Text = "";
            txtCheckUPResCreatinine.Text = "";
            txtCheckUPReseGFR.Text = "";
            txtCheckUPResCholes.Text = "";
            txtCheckUPResHDL.Text = "";
            cboCheckUPResUAColor.Text = "";
            cboCheckUPResUAAppea.Text = "";
            cboCheckUPResUAGlucose.Text = "";
            cboCheckUPResUAProtein.Text = "";
            cboCheckUPResUAKetone.Text = "";
            txtCheckUPResUAWBC.Text = "";
            txtCheckUPResUARBC.Text = "";
            cboCheckUPResMS017.Text = "";
            txtCheckUPResMCV.Text = "";
            txtCheckUPResWBC.Text = "";
            txtCheckUPResEyeVAR1.Text = "";
            txtCheckUPResEyeVAL1.Text = "";
            txtCheckUPResEyeVAR2.Text = "";
            txtCheckUPResEyeVAL2.Text = "";
            txtCheckUPResEyeVAR.Text = "";
            txtCheckUPResEyeVAL.Text = "";
            txtCheckUPResEyePhR.Text = "";
            txtCheckUPResEyePhL.Text = "";
            cboCheckUPResFRT.Text = "";
            cboCheckUPResChest.Text = "";
            txtCheckUPResEye.Text = "";
            txtCheckUPResHCT.Text = "";
            txtCheckUPResHB.Text = "";
            txtCheckUPResNeu.Text = "";
            txtCheckUPResLym.Text = "";
            txtCheckUPResMono.Text = "";
            txtCheckUPResEos.Text = "";
            txtCheckUPResBas.Text = "";
            txtCheckUPResPlaCnt.Text = "";
            txtCheckUPResRBC.Text = "";
            cboCheckUPResXray.Text = "";
            txtCheckUPResHBStandard.Text = "";
            txtCheckUPResHCTStandard.Text = "";
            txtCheckUPResNeuStandard.Text = "";
            txtCheckUPResLymStandard.Text = "";
            txtCheckUPResMonoStandard.Text = "";
            txtCheckUPResEosStandard.Text = "";
            txtCheckUPResBasStandard.Text = "";
            txtCheckUPResMCVStandard.Text = "";
            txtCheckUPResWBCStandard.Text = "";
            txtCheckUPResRBCStandard.Text = "";
            txtCheckUPResPlaCntStandard.Text = "";
            txtCheckUPResHDLStandard.Text = "";
            txtCheckUPResCholesStandard.Text = "";
            txtCheckUPReseGFRStandard.Text = "";
            txtCheckUPResCreatinineStandard.Text = "";
            txtCheckUPResFBSStandard.Text = "";
            txtCheckUPResUAWBCStandard.Text = "";
            txtCheckUPResUARBCStandard.Text = "";
        }
        private void BtnScanGetImg_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tC1.SelectedTab == tabOper)
            {
                setImageStaffNote(picL, picR);
            }
            else if (tC1.SelectedTab == tabSearch)
            {
                setImageStaffNote(picSrcL, picSrcR);
            }
        }
        private void setImageStaffNote(C1PictureBox picl, C1PictureBox picr)
        {
            String dd = "", mm = "", yy = "", err = "", preno1 = "";
            if (tC1.SelectedTab == tabCheckUP)
            {
                FrmStaffNote frm = new FrmStaffNote(bc, txtCheckUPHN.Text.Trim(), cboCheckUPPrefixT.Text.Trim() + " " + txtCheckUPNameT.Text.Trim() + " " + txtCheckUPSurNameT.Text.Trim(), VSDATE, PRENO, "checkup");
                frm.ShowDialog(this);
                frm.Dispose();
            }
            else
            {
                try
                {
                    Boolean chkL = false, chkR = false;
                    picl.Image = null;
                    picr.Image = null;
                    String folderPath = bc.iniC.pathlocalStaffNote;
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                    string[] files = Directory.GetFiles(folderPath);
                    foreach (String file in files)
                    {
                        if (picl.Image == null)
                        {
                            FILEL = new FileInfo(file);
                            using (FileStream fsR = new FileStream(FILEL.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                using (Image imgl = Image.FromStream(fsR))
                                {
                                    picl.Image = (Image)imgl.Clone();
                                }
                            }
                            chkL = true;
                            continue;
                        }
                        if (picr.Image == null)
                        {
                            FILER = new FileInfo(file);
                            using (FileStream fsR = new FileStream(FILER.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                using (Image imgr = Image.FromStream(fsR))
                                {
                                    picr.Image = (Image)imgr.Clone();
                                }
                            }
                            chkR = true;
                            continue;
                        }
                    }
                    if (chkL && chkR)
                        stt.Show("<p>" + FILER.FullName + " ✅</p>", picL, 2000);
                    err = "00";
                }
                catch (Exception ex)
                {
                    lfSbStatus.Text = ex.Message.ToString();
                    lfSbMessage.Text = err + " setImageStaffNote " + ex.Message;
                    new LogWriter("e", "FrmOPD setImageStaffNote " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setImageStaffNote", ex.Message);
                }
            }
        }
        private void BtnCheckUPPrnDriver_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            genDriverPDF();
        }

        private void TCFinish_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tCFinish.SelectedTab == tabFinishCertMed)
            {
                TCFinishActive = tabFinishCertMed.Name;
            }
        }
        private void BtnCertiView2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void BtnCertiView1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void BtnCerti2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (bc.iniC.hostname.Equals("โรงพยาบาล บางนา5"))
            {
                FrmCertDoctor frm = new FrmCertDoctor(bc, "", HN, VSDATE, PRENO, "2NFLEAF");
                frm.ShowDialog(this);

                if (frm.streamCertiDtr != null)
                {
                    pnCertiMed.Controls.Clear();
                    pnCertiMed.Controls.Add(fvCerti);
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    pds.LoadFromStream(frm.streamCertiDtr);
                    fvCerti.DocumentSource = pds;
                    btnCerti1.Enabled = true;

                    if (!bc.iniC.statusLabOutAutoPrint.Equals("1")) return;
                    frm.streamCertiDtr.Position = 0;
                    setLbLoading("กำลังสั่งพิมพ์ ...");
                    showLbLoading();
                    SetDefaultPrinter(bc.iniC.printerLabOut);
                    pds.LoadFromStream(frm.streamCertiDtr);
                    pds.Print();
                    hideLbLoading();
                    //new LogWriter("d", "FrmScanView1 BtnCertiNew_Click Print Done ");
                }
            }
            else if (bc.iniC.hostname.Equals("โรงพยาบาล บางนา1"))
            {
                FrmCertDoctorBn1 frm = new FrmCertDoctorBn1(bc, "'", HN, VSDATE, PRENO, "2NFLEAF");
                frm.ShowDialog(this);

                if (frm.streamCertiDtr != null)
                {
                    pnCertiMed.Controls.Clear();
                    pnCertiMed.Controls.Add(fvCerti);
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    pds.LoadFromStream(frm.streamCertiDtr);
                    fvCerti.DocumentSource = pds;
                    btnCerti1.Enabled = true;

                    if (!bc.iniC.statusLabOutAutoPrint.Equals("1")) return;
                    frm.streamCertiDtr.Position = 0;
                    setLbLoading("กำลังสั่งพิมพ์ ...");
                    showLbLoading();
                    SetDefaultPrinter(bc.iniC.printerLabOut);
                    pds.LoadFromStream(frm.streamCertiDtr);
                    pds.Print();
                    hideLbLoading();
                    //new LogWriter("d", "FrmScanView1 BtnCertiNew_Click Print Done ");
                }
            }
        }
        private void BtnCerti1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (bc.iniC.hostname.Equals("โรงพยาบาล บางนา5"))
            {
                FrmCertDoctor frm = new FrmCertDoctor(bc, "", HN, VSDATE, PRENO);
                frm.ShowDialog(this);

                if (frm.streamCertiDtr != null)
                {
                    pnCertiMed.Controls.Clear();
                    pnCertiMed.Controls.Add(fvCerti);
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    pds.LoadFromStream(frm.streamCertiDtr);
                    fvCerti.DocumentSource = pds;
                    btnCerti1.Enabled = true;

                    if (!bc.iniC.statusLabOutAutoPrint.Equals("1")) return;
                    frm.streamCertiDtr.Position = 0;
                    setLbLoading("กำลังสั่งพิมพ์ ...");
                    showLbLoading();
                    SetDefaultPrinter(bc.iniC.printerLabOut);
                    pds.LoadFromStream(frm.streamCertiDtr);
                    pds.Print();
                    hideLbLoading();
                    //new LogWriter("d", "FrmScanView1 BtnCertiNew_Click Print Done ");
                }
            }
            else if (bc.iniC.hostname.Equals("โรงพยาบาล บางนา1"))
            {
                FrmCertDoctorBn1 frm = new FrmCertDoctorBn1(bc, "", HN, VSDATE, PRENO);
                frm.ShowDialog(this);

                if (frm.streamCertiDtr != null)
                {
                    pnCertiMed.Controls.Clear();
                    pnCertiMed.Controls.Add(fvCerti);
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    pds.LoadFromStream(frm.streamCertiDtr);
                    fvCerti.DocumentSource = pds;
                    btnCerti1.Enabled = true;

                    if (!bc.iniC.statusLabOutAutoPrint.Equals("1")) return;
                    frm.streamCertiDtr.Position = 0;
                    setLbLoading("กำลังสั่งพิมพ์ ...");
                    showLbLoading();
                    SetDefaultPrinter(bc.iniC.printerLabOut);
                    pds.LoadFromStream(frm.streamCertiDtr);
                    pds.Print();
                    hideLbLoading();
                    //new LogWriter("d", "FrmScanView1 BtnCertiNew_Click Print Done ");
                }
            }
        }

        private void BtnPrnCertMed_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtOperHN.Text.Length <= 0) { lfSbMessage.Text = "กรุณาเลือกคนไข้"; return; }
            if (txtOperDtr.Text.Length <= 0) { lfSbMessage.Text = "กรุณาระบุ แพทย์"; return; }
            FrmCertDoctor frm = new FrmCertDoctor(bc, txtOperDtr.Text.Trim(), txtOperHN.Text.Trim(), VSDATE, PRENO);
            frm.ShowDialog(this);
        }
        private void setCertiMed()
        {
            String docscanid = "", cert2ndleaf = "";
            docscanid = bc.bcDB.mcertiDB.selectDocScanIDByHn(HN, PRENO, VSDATE);
            DocScan dsc = new DocScan();
            dsc = bc.bcDB.dscDB.selectByPk(docscanid);
            btnCertiView1.Enabled = false;
            cert2ndleaf = bc.bcDB.mcertiDB.selectCertIDByHn2ndLeaf(HN, PRENO, VSDATE);
            //CERTI2NDLEAF = cert2ndleaf;
            btnCerti2.Enabled = dsc.doc_scan_id.Length > 0 ? true : false;
            btnCertiView1.Enabled = cert2ndleaf.Length > 0 ? true : false;
            FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
            MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
            pnCertiMed.Controls.Clear();
            if (dsc.image_path.ToLower().IndexOf("pdf") > 0)
            {
                C1PdfDocumentSource pdsMedCerti = new C1PdfDocumentSource();
                pdsMedCerti.LoadFromStream(streamCertiDtr);
                fvCerti.DocumentSource = pdsMedCerti;
                pnCertiMed.Controls.Add(fvCerti);
            }
            else
            {
                C1PictureBox certiView = new C1PictureBox();
                if ((streamCertiDtr == null) || (streamCertiDtr.Length == 0))
                {
                    return;
                }
                streamCertiDtr.Position = 0;
                Image img = Image.FromStream(streamCertiDtr);
                certiView.Dock = DockStyle.Fill;
                certiView.SizeMode = PictureBoxSizeMode.StretchImage;
                certiView.Image = img;
                certiView.Size = new Size(1000, 850);
                //certiView.Location = new System.Drawing.Point(10, btnCertiNew.Top + btnCertiNew.Height - 25);
                ContextMenu menuGw = new ContextMenu();
                //menuGw.MenuItems.Add("Print Certificate Medical", new EventHandler(ContextMenu_CertiMedical_Print));
                //menuGw.MenuItems.Add("Download Certificate Medical", new EventHandler(ContextMenu_CertiMedical_Download));
                certiView.ContextMenu = menuGw;
                streamPrint = streamCertiDtr;
                pnCertiMed.Controls.Add(certiView);
            }
        }
        private void BtnCheckUPPrn7Thai_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            gen7ThaiPDF();
        }

        private void TxtCheckUPHN_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setControlCheckUP(txtCheckUPHN.Text,"","");
            }
        }
        private void TxtStaffNoteR_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            paintStaffNote();
        }
        private void TxtStaffNoteL_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            paintStaffNote();
        }
        private void paintStaffNote()
        {
            String[] txt = txtStaffNoteR.Text.Trim().Split('\n');
            Image aaa = (Image)IMGSTAFFNOTE.Clone();
            int line = 0, y = 0, i = 0;
            if (txt.Length > 0)
            {
                using (var gr = Graphics.FromImage(aaa))
                {
                    foreach (String txt1 in txt)
                    {
                        y = 440 + line;
                        if (i == 4) y -= 6;
                        else if (i == 6) y -= 6;
                        else if (i == 7) y -= 2;
                        else if (i == 8) y -= 2;
                        else if (i == 8) y -= 2;
                        else if (i == 9) y -= 8; else if (i == 10) y -= 10; else if (i == 11) y -= 10; else if (i == 12) y -= 12; else if (i == 13) y -= 14; else if (i == 14) y -= 14; else if (i == 15) y -= 16; else if (i == 16) y -= 16;
                        gr.DrawString(txt1.Replace("\r", ""), famt7, brush, 980, y);
                        line += 45; i++;
                    }
                }
            }
            line = 0; y = 0; i = 0;
            txt = txtStaffNoteL.Text.Trim().Split('\n');
            if (txt.Length > 0)
            {
                using (var gr = Graphics.FromImage(aaa))
                {
                    foreach (String txt1 in txt)
                    {
                        y = 570 + line;
                        if (i == 4) y -= 6; if (i == 6) y -= 6; if (i == 7) y -= 2; if (i == 8) y -= 2;
                        gr.DrawString(txt1.Replace("\r", ""), famt7, brush, 110, y);
                        line += 45; i++;
                    }
                }
            }
            picStaffNote.Image = aaa;
        }
        private void BtnStaffNote_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerStaffNote;
            document.DefaultPageSettings.PaperSize = new PaperSize("A4", 826, 1169);
            document.PrintPage += Document_PrintPage_StaffNote_Reserve;
            document.DefaultPageSettings.Landscape = true;            document.Print();            document.Dispose();
        }
        
        private void BtnRpt1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DateTime.TryParse(txtRptStartDate.Value.ToString(), out DateTime dtapmstart);//txtApmDate
            if (dtapmstart.Year < 1900)            {                dtapmstart.AddYears(543);            }
            DateTime.TryParse(txtRptEndDate.Value.ToString(), out DateTime dtapmend);//txtApmDate
            if (dtapmend.Year < 1900)            {                dtapmend.AddYears(543);            }
            bc.bcDB.pt07DB.setCboTumbonName(cboRpt1, dtapmstart.Year.ToString() + "-" + dtapmstart.ToString("MM-dd"), dtapmend.Year.ToString() + "-" + dtapmend.ToString("MM-dd"), "");
        }
        private void BtnRptPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(grfRpt.Rows.Count <= 0)            {                lfSbMessage.Text = "ไม่พบข้อมูล";                return;            }
            if(grfRpt.Rows ==null)            {                lfSbMessage.Text = "ไม่พบข้อมูล";                return;            }
            if (grfRpt.Row <= 0) { lfSbMessage.Text = "กรุณาเลือกข้อมูลที่ต้องการพิมพ์"; return; }
            if (grfRpt.Row == 1)
            {
                genReportApmDoctor();
            }
            else if(grfRpt.Row == 2){
                genReportPatientinDept();
            }
        }
        private void genReportPatientinDept()
        {
            setVARRPT();
            //String date = RPTDTAPMSTART.Year.ToString() + "-" + RPTDTAPMSTART.ToString("MM-dd");
            DTRPT = bc.bcDB.vsDB.selectPttinDeptActNo110(RPTDEPTCODE, RPTSECCODE, RPTDTAPMSTART.Year.ToString() + "-" + RPTDTAPMSTART.ToString("MM-dd"), RPTDTAPMEND.Year.ToString() + "-" + RPTDTAPMEND.ToString("MM-dd"));
            int i = 1;
            foreach (DataRow drow in DTRPT.Rows)
            {
                drow["row_number"] = i++;
                drow["hn"] = drow["MNC_HN_NO"].ToString();
                drow["apm_time"] = bc.showTime(drow["MNC_TIME"].ToString());
                drow["apm_date"] = bc.datetoShow1(drow["MNC_DATE"].ToString());
                drow["apmdept"] = drow["MNC_SEC_NO"].ToString().Equals("302") ? "OPD3" : drow["MNC_SEC_NO"].ToString().Equals("131") ? "OPD2": drow["MNC_SEC_NO"].ToString().Equals("111") ?"OPD1": drow["MNC_SEC_NO"].ToString().Equals("123") ?"OPD4": drow["MNC_SEC_NO"].ToString().Equals("156") ?"OPD5": drow["MNC_SEC_NO"].ToString();
                drow["pttname"] = drow["ptt_fullnamet"].ToString();
                drow["paidname"] = drow["MNC_FN_TYP_DSC"].ToString().Replace("ประกันสังคมอิสระ (บ.5)", "ปกต บ.5").Replace("ประกันสังคมอิสระ (บ.2)", "ปกต บ.2").Replace("ประกันสังคมอิสระ (บ.1)", "ปกต บ.1").Replace("ประกันสังคม (บ.5)", "ปกส บ.5").Replace("ประกันสังคม (บ.2)", "ปกส บ.2").Replace("ประกันสังคม (บ.1)", "ปกส บ.1");
                drow["dtrname"] = drow["dtr_name"].ToString().Replace("นพ. พิพัฒน์ชัย อรุณธรรมคุณ", "นพ. พิพัฒน์ชัย อรุณธรรม.");
                drow["desc1"] = drow["MNC_SHIF_MEMO"].ToString().Replace("\r\n", ",");
                drow["apmmake"] = drow["MNC_COM_DSC"].ToString();
                drow["apmmake"] = drow["MNC_COM_DSC"].ToString();
            }
            FileInfo rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\ptt_visit_dept_date.rdlx");
            if (RPTDTRCODE.Length > 0)
            {
                //rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\appointment_date_doctor.rdlx");
                rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\ptt_visit_dept_date.rdlx");
            }
            if (!System.IO.File.Exists(rptPath.FullName)) { lfSbMessage.Text = "File report not found"; return; }
            PageReport definition = new PageReport(rptPath);
            PageDocument runtime = new GrapeCity.ActiveReports.Document.PageDocument(definition);

            runtime.Parameters["line1"].CurrentValue = bc.iniC.hostname;
            runtime.Parameters["line2"].CurrentValue = "รายงานคนไข้ตามแผนก "+ cboRpt2.Text+ " ประจำวันที่ " + RPTDTAPMSTART.ToString("dd-MM-yyyy") + " ถึงวันที่ " + RPTDTAPMEND.ToString("dd-MM-yyyy");
            runtime.Parameters["line3"].CurrentValue = (RPTDTRCODE.Length > 0) ? "แพทย์ " + cboRpt1.Text : "";

            runtime.LocateDataSource += Runtime_LocateDataSource;
            arvMain.LoadDocument(runtime);
        }
        private void setVARRPT()
        {
            DateTime.TryParse(txtRptStartDate.Value.ToString(), out RPTDTAPMSTART);//txtApmDate
            if (RPTDTAPMSTART.Year < 1900) { RPTDTAPMSTART.AddYears(543); }
            DateTime.TryParse(txtRptEndDate.Value.ToString(), out RPTDTAPMEND);//txtApmDate
            if (RPTDTAPMEND.Year < 1900) { RPTDTAPMEND.AddYears(543); }

            RPTSECCODE = cboRpt2.SelectedItem == null ? "" : ((ComboBoxItem)cboRpt2.SelectedItem).Value.ToString();
            RPTDEPTCODE = bc.bcDB.pm32DB.selectDeptOPDBySecNO(RPTSECCODE);
            RPTDTRCODE = cboRpt1.SelectedItem == null ? "" : ((ComboBoxItem)cboRpt1.SelectedItem).Value.ToString();
        }
        private void genReportApmDoctor()
        {
            setVARRPT();
            
            DTRPT = bc.bcDB.pt07DB.selectByDate1(RPTDTAPMSTART.Year.ToString() + "-" + RPTDTAPMSTART.ToString("MM-dd"), RPTDTAPMEND.Year.ToString() + "-" + RPTDTAPMEND.ToString("MM-dd"), RPTDTRCODE, RPTDEPTCODE, cboRpt2.Text, "");
            int i = 1;
            foreach (DataRow drow in DTRPT.Rows)
            {
                drow["row_number"] = i++;
                drow["hn"] = drow["MNC_HN_NO"].ToString();
                drow["apm_time"] = bc.showTime(drow["apm_time"].ToString());
                drow["apm_date"] = bc.datetoShow1(drow["apm_date"].ToString());
                drow["apmdept"] = drow["apmdept"].ToString().Replace("กุมารเวช", "").Replace("เวชปฎิบัติทั่วไป", "").Replace("ศัลยกรรมกระดูก", "").Replace("อายุรกรรมทั่วไป", "").Replace("อายุรกรรมโรคหัวใจ", "").Replace("สูตินารีเวช", "");//นพ. พิพัฒน์ชัย อรุณธรรมคุณ เวชปฎิบัติทั่วไป (OPD3)
                drow["apmmake"] = drow["apmmake"].ToString().Replace("กุมารเวช", "").Replace("เวชปฎิบัติทั่วไป", "").Replace("ศัลยกรรมกระดูก", "").Replace("อายุรกรรมทั่วไป", "").Replace("อายุรกรรมโรคหัวใจ", "").Replace("สูตินารีเวช", "");
                drow["paidname"] = drow["paidname"].ToString().Replace("ประกันสังคมอิสระ (บ.5)", "ปกต บ.5").Replace("ประกันสังคมอิสระ (บ.2)", "ปกต บ.2").Replace("ประกันสังคมอิสระ (บ.1)", "ปกต บ.1").Replace("ประกันสังคม (บ.5)", "ปกส บ.5").Replace("ประกันสังคม (บ.2)", "ปกส บ.2").Replace("ประกันสังคม (บ.1)", "ปกส บ.1");
                drow["dtrname"] = drow["dtrname"].ToString().Replace("นพ. พิพัฒน์ชัย อรุณธรรมคุณ", "นพ. พิพัฒน์ชัย อรุณธรรม.");
                drow["desc1"] = drow["desc1"].ToString().Replace("\r\n", ",");
            }
            FileInfo rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\appointment_date.rdlx");
            if (RPTDTRCODE.Length > 0)
            {
                //rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\appointment_date_doctor.rdlx");
                rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\appointment_date.rdlx");
            }
            if (!System.IO.File.Exists(rptPath.FullName)) { lfSbMessage.Text = "File report not found"; return; }
            PageReport definition = new PageReport(rptPath);
            PageDocument runtime = new GrapeCity.ActiveReports.Document.PageDocument(definition);

            runtime.Parameters["line1"].CurrentValue = bc.iniC.hostname;
            runtime.Parameters["line2"].CurrentValue = "รายงานแพทย์นัด ประจำวันที่ " + RPTDTAPMSTART.ToString("dd-MM-yyyy") + " ถึงวันที่ " + RPTDTAPMEND.ToString("dd-MM-yyyy");
            runtime.Parameters["line3"].CurrentValue = (RPTDTRCODE.Length > 0) ? "แพทย์ " + cboRpt1.Text : "";

            runtime.LocateDataSource += Runtime_LocateDataSource;
            arvMain.LoadDocument(runtime);
        }
        private void Runtime_LocateDataSource(object sender, LocateDataSourceEventArgs args)
        {
            //throw new NotImplementedException();
            args.Data = DTRPT;
        }
        private void BtnApmExcel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //grfApm.SaveExcel("appointment.xlsx", "appointment", FileFlags.None);
            //System.Diagnostics.Process.Start("appointment.xlsx");
            String filenam = "";
            filenam = "app_"+DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd")+".xls";
            if (System.IO.File.Exists(bc.iniC.pathDownloadFile + "\\" + filenam))
            {
                lfSbMessage.Text = "พบ File "+ filenam + " กรุณาลบ File นี้ก่อน";
                //return;
            }
            //SaveFileDialog dlg = new SaveFileDialog();
            //dlg.DefaultExt = "xls";
            //dlg.Filter = "Excel |*.xls";
            //dlg.InitialDirectory = bc.iniC.pathDownloadFile;
            //dlg.FileName = "*.xls";
            //if (dlg.ShowDialog() != DialogResult.OK)
            //    return;

            // clear book
            C1XLBook _book = new C1XLBook();
            //_book.Clear();
            //_book.Sheets.Clear();

            // copy grids to book sheets
            //foreach (TabPage pg in _tab.TabPages)
            //{
            //    C1FlexGrid grid = pg.Controls[0] as C1FlexGrid;
            _book.Sheets.Remove("Sheet1");
            XLSheet sheet = _book.Sheets.Add("Sheet1");
            bc.SaveSheet(grfApm, sheet, _book, false);
            //}

            // save selected sheet index
            _book.Sheets.SelectedIndex = 0;

            // save the book
            _book.Save(bc.iniC.pathDownloadFile+"\\"+ filenam);
            System.Diagnostics.Process.Start(bc.iniC.pathDownloadFile + "\\" + filenam);
        }
        private void BtnApmSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
        }
        private void TxtApmSrc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            grfApm.ApplySearch(txtApmSrc.Text.Trim(), true, true, false);
        }
        private void TxtApmDate_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            setGrfApm();
        }
        private void CboApmDept1_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            setGrfApm();
        }
        
        private void BtnOrderSubmit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            timeOperList.Stop();
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
            frm.ShowDialog();
            frm.Dispose();
            if (bc.USERCONFIRMID.Length <= 0)       {           lfSbMessage.Text = "Password ไม่ถูกต้อง";                return;            }
            String re = "";
            re = bc.bcDB.vsDB.insertOrder(HN, VSDATE, PRENO, txtOperDtr.Text.Trim(), bc.USERCONFIRMID);
            if (re.Length>0)
            {
                String[] reqno = re.Split('#');
                if(reqno.Length > 2)
                {
                    DataTable dtlab = new DataTable(), dtxray = new DataTable(), dtprocedure = new DataTable(), dtdrug = new DataTable();
                    dtlab = bc.bcDB.labT02DB.selectbyHNReqNo(HN, VSDATE, reqno[0].Replace("lab=",""));
                    //dtdrug = bc.bcDB.drugDB.selectbyHNReqNo(HN, VSDATE, reqno[1].Replace("drug=", ""));
                    dtxray = bc.bcDB.xrayT02DB.selectbyHNReqNo(HN, VSDATE, reqno[3].Replace("xray=", ""));
                    dtprocedure = bc.bcDB.pt16DB.selectbyHNReqNo(HN, VSDATE, reqno[2].Replace("procedure=", ""));
                    dtlab.Merge(dtxray);
                    dtlab.Merge(dtprocedure);
                    setGrfOrderLab(); setGrfOrderXray(); setGrfPrenoProc();
                    int i = 1, j = 1;
                    grfOrder.Rows.Count = 1;                    grfOrder.Rows.Count = dtlab.Rows.Count + 1;
                    //pB1.Maximum = dt.Rows.Count;
                    foreach (DataRow row1 in dtlab.Rows)
                    {
                        try
                        {
                            Row rowa = grfOrder.Rows[i];
                            rowa[colgrfOrderCode] = row1["order_code"].ToString();
                            rowa[colgrfOrderName] = row1["order_name"].ToString();
                            rowa[colgrfOrderQty] = row1["qty"].ToString();
                            rowa[colgrfOrderStatus] = row1["flag"].ToString();
                            rowa[colgrfOrderID] = "";
                            rowa[colgrfOrderReqNO] = row1["req_no"]?.ToString()??"";
                            rowa[colgrfOrdFlagSave] = "1";
                            rowa[0] = i.ToString();
                            if (row1["flag"].ToString().Equals("drug")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#9FE2BF"); }
                            else if (row1["flag"].ToString().Equals("lab")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6"); }
                            else if (row1["flag"].ToString().Equals("xray")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#CCCCFF"); }
                            else if (row1["flag"].ToString().Equals("procedure")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#FF7F50"); }
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
                else                {                    lfSbMessage.Text = re;                }
            }
            timeOperList.Start();
        }
        private void BtnOrderSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String txt = "", tick="";
            tick = DateTime.Now.Ticks.ToString();
            foreach(Row rowa in grfOrder.Rows)
            {
                String code = "", flag = "", name="", qty="", chk="", idtemp="", interac = "", indica = "", freq = "", precau = "", statuscontrol="", supervisor="", passsupervisor="", controlyear="", controlremark="";
                idtemp = rowa[colgrfOrderID]?.ToString()??"";
                code = rowa[colgrfOrderCode]?.ToString()??"";
                if (code.Equals("code")) continue;
                chk = rowa[colgrfOrdFlagSave].ToString();
                if (chk.Equals("1")) continue;// มี ข้อมูลใน table temp_order แล้วไม่ต้อง save เดียวจะ มี2record และในกรณีที่ submit ออก reqno เรียบร้อยแล้วจะได้ ไม่ซ้ำ
                name = rowa[colgrfOrderName].ToString();
                qty = rowa[colgrfOrderQty].ToString();
                flag = rowa[colgrfOrderStatus].ToString();
                statuscontrol = rowa[colgrfOrdStatusControl]?.ToString()??"";
                supervisor = rowa[colgrfOrdSupervisor]?.ToString() ?? "";
                passsupervisor = rowa[colgrfOrdPassSupervisor]?.ToString() ?? "";
                controlyear = rowa[colgrfOrdControlYear]?.ToString() ?? "";
                controlremark = rowa[colgrfOrdControlRemark]?.ToString() ?? "";
                String re = bc.bcDB.vsDB.insertOrderTemp(idtemp, code, name, qty,"","","","","", flag, txtOperHN.Text.Trim(), VSDATE, PRENO, statuscontrol, controlyear, controlremark, supervisor, passsupervisor);
                if (int.TryParse(re, out int _))        {           lfSbMessage.Text = "save OK";           }
                else                                    {                    lfSbMessage.Text = re;         }
            }
            setGrfOrderTemp();
        }
        private void LbApm1Month_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtApmPlusDay.Value = "28";
            calApmDate();
        }

        private void LbApm14Week_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtApmPlusDay.Value = "14";
            calApmDate();
        }

        private void LbApm7Week_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtApmPlusDay.Value = "7";
            calApmDate();
        }

        private void TxtApmPlusDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as C1TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private void TxtApmPlusDay_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            calApmDate();
        }
        private void calApmDate()
        {
            if(txtApmPlusDay.Text.Length<=0) return;
            DateTime dtcal = DateTime.Now;
            if (dtcal.Year > 2500)
            {
                dtcal.AddYears(-543);
            }
            dtcal = dtcal.AddDays(int.Parse(txtApmPlusDay.Text));
            txtPttApmDate.Value = dtcal.Year.ToString() + "-" + dtcal.ToString("MM-dd");
        }
        private void BtnApmOrder_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmItemSearch frm = new FrmItemSearch(bc);
            frm.ShowDialog();
            if ((bc.items != null) && (bc.items.Count > 0))
            {
                pnApmOrder.Show();
                foreach(Item item in bc.items)
                {
                    Row rowa = grfApmOrder.Rows.Add();
                    rowa[colgrfOrderCode] = item.code;
                    rowa[colgrfOrderName] = item.name;
                    rowa[colgrfOrderStatus] = item.flag;
                    rowa[colgrfOrderQty] = "1";
                    rowa[0] = grfApmOrder.Rows.Count-1;
                }
            }
        }
        private void BtnOperItemSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmItemSearch frm = new FrmItemSearch(bc);
            frm.ShowDialog();
            if ((bc.items != null)  && (bc.items.Count > 0))
            {
                foreach(Item item in bc.items)
                {
                    setGrfOrderItem(item.code, item.name, item.qty, item.flag);
                }
            }
        }
        private void TxtDateSearch_DropDownClosed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfTodayOutLab();
        }
        private void LbApmList_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            CHKLBAPMLISTCLICK = true; CHKLBAPMREMLISTClICK = false;
            //if (CHKLBAPMLISTCLICK) CHKLBAPMLISTCLICK1 = true;
            if (chlApmList.Visible)
            {
                chlApmList.Hide();
                String txt = "";
                foreach(var chk in chlApmList.Items)
                {
                    if (chk.Selected)       {       txt += chk.Value.ToString()+"\r\n";         }
                }
                if (txtApmList.Text.Trim().Length > 0) txtApmList.Value += "\r\n" + txt; else txtApmList.Value += txt;
                txtApmList.ScrollBars = ScrollBars.Both;
            }
            else if (!chlApmList.Visible)
            {
                chlApmList.Items.Clear();
                foreach (String txt in autoApm)     {       chlApmList.Items.Add(txt);      }
                chlApmList.Top = cboApmDept.Top;            chlApmList.Left = cboApmDept.Left;              chlApmList.Show();
                foreach (var chk in chlApmList.Items)
                {
                    if (chk.Selected)   {   chk.Selected = false;   }
                }
            }
        }
        private void LbApmRemList_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            CHKLBAPMLISTCLICK = false; CHKLBAPMREMLISTClICK = true;
            //if(CHKLBAPMREMLISTClICK) CHKLBAPMREMLISTClICK1 = true;
            if (chlApmList.Visible)
            {
                chlApmList.Hide();
                String txt = "";
                foreach (var chk in chlApmList.Items)
                {
                    if (chk.Selected)       {           txt += chk.Value.ToString() + ";";      }
                }
                txtApmRemark.Value = txt;
                //txtApmRemList.ScrollBars = ScrollBars.Both;
            }
            else if (!chlApmList.Visible)
            {
                chlApmList.Items.Clear();
                foreach (String txt in APMREM) { chlApmList.Items.Add(txt); }
                chlApmList.Top = cboApmDept.Top;                chlApmList.Left = cboApmDept.Left;                chlApmList.Show();
                foreach (var chk in chlApmList.Items)
                {
                    if (chk.Selected)           {           chk.Selected = false;           }
                }
            }
        }
        private void ChlApmList_Leave(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (CHKLBAPMLISTCLICK)
            {
                chlApmList.Hide();
                String txt = "";
                foreach (var chk in chlApmList.Items)
                {
                    if (chk.Selected)
                    {
                        txt += chk.Value.ToString() + "\r\n";
                    }
                }
                //txtApmList.Value += "\r\n" + txt;
                if (txtApmList.Text.Trim().Length > 0) txtApmList.Value += "\r\n" + txt; else txtApmList.Value += txt;
                txtApmList.ScrollBars = ScrollBars.Both;
            }
            else if (CHKLBAPMREMLISTClICK)
            {
                chlApmList.Hide();
                String txt = "";
                foreach (var chk in chlApmList.Items)
                {
                    if (chk.Selected)
                    {
                        txt += chk.Value.ToString() + ";";
                    }
                }
                txtApmRemark.Value = txt;
            }
        }
        private void ChlApmList_LostFocus(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
        }
        private void TxtSrcHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if((e.KeyCode == Keys.Enter) || (txtSrcHn.Text.Length > 3))
            {
                setGrfSrc1();
                if(grfSearchLab !=null && grfSearchLab.Rows.Count > 1)                {                    grfSearchLab.Rows.Count = 1;                }
            }
        }
        private void setGrfSrc1()
        {
            showLbLoading();
            bool ispid = false, isname = false, isNum = false, isLettersOnly = false;
            long chkpid = 0;
            isNum = long.TryParse(txtSrcHn.Text.Trim(), out chkpid);
            isLettersOnly = !string.IsNullOrWhiteSpace(txtSrcHn.Text) && Regex.IsMatch(txtSrcHn.Text.Trim(), @"^[\p{L}\s]+$");
            if(isLettersOnly) setGrfSrc("search");
            else setGrfSrc("");
            if (isNum) setControlSrcPttSrc("","","");
            pnCertMedView.Controls.Clear();
            hideLbLoading();
        }
        private void setControlSrcPttSrc(String vn, String preno, String vsdate)
        {
            //แก้ไข ให้สามารถค้นหา จาก PID name ได้ 681202
            if (txtSrcHn.Text.Trim().Length <= 0) return;
            Patient ptt = new Patient();
            bool ispid = false, isname = false, isNum = false, isLettersOnly = false;
            long chkpid = 0;
            isNum = long.TryParse(txtOperHN.Text.Trim(), out chkpid);
            isLettersOnly = !string.IsNullOrWhiteSpace(txtSrcHn.Text) && Regex.IsMatch(txtSrcHn.Text.Trim(), @"^[\p{L}\s]+$");
            ptt = bc.bcDB.pttDB.selectPatinetByHn(HN);
            if (ptt == null) return;
            lvSrcPttName.Value = ptt.Name;
            lbPttFinNote.Text = ptt.MNC_FIN_NOTE.Length <= 0 ? "..." : ptt.MNC_FIN_NOTE;
            lbSrcHN.Value = ptt.Hn;
            DataTable dtallergy = new DataTable();
            DataTable dtchronic = new DataTable();
            String allergy = "", txtChronic="";
            dtallergy = bc.bcDB.vsDB.selectDrugAllergy(ptt.Hn);
            dtchronic = bc.bcDB.vsDB.SelectChronicByPID(ptt.idcard);
            foreach (DataRow row in dtallergy.Rows)
            {
                allergy += row["MNC_ph_tn"].ToString() + " " + row["MNC_ph_memo"].ToString() + ", ";
            }
            lbDrugAllergy.Value = "";
            lbChronic.Value = "";
            if (allergy.Length > 0)
            {
                lbDrugAllergy.Value = "แพ้ยา " + allergy;
            }
            else
            {
                lbDrugAllergy.Value = "ไม่มีข้อมูล การแพ้ยา ";
            }
            foreach (DataRow row in dtchronic.Rows)
            {
                txtChronic += row["CHRONICCODE"].ToString() + " " + row["MNC_CRO_DESC"].ToString() + ",";
            }
            lbChronic.Value = txtChronic.Length > 0 ? "โรคเรื้อรัง " + txtChronic : "ไม่มีข้อมูล โรคเรื้อรัง";
            lbSrcAge.Value = "อายุ " + ptt.AgeStringShort();
            lbSrcPreno.Value = preno;
            lbSrcVsDate.Value = vsdate;
            lbSrcVN.Value = vn;
            btnSrcEKGScanSave.Enabled = false;
        }
        
        private void BtnSBSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setSBSearch();
        }
        private void setSBSearch()
        {
            if (TC1Active.Equals(tabMedScan.Name))
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
                    tC1.SelectedTab = tabOutlab;
                    fvCerti.DocumentSource = null;
                    setGrfOutLab();
                }
            }
            else if (TC1Active.Equals(tabOutlab.Name))
            {
                Patient ptt = bc.bcDB.pttDB.selectPatinetByHn(txtSBSearchHN.Text);
                rb1.Text = ptt.Name;
                setGrfOutLab();
            }
            else if (TC1Active.Equals(tabOper.Name))
            {
                grfOperList.ApplySearch(txtSBSearchHN.Text.Trim(), true, true, false);
            }
        }
        private void TxtMedScanHN_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            
        }
        
        private void TxtSBSearchHN_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setSBSearch();
            }
        }
        private void BtnMedScan_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (HN.Length <= 0) return;
            if (tabMedScan.Visible)
            {
                tabMedScan.Hide();
            }
            else
            {
                tabMedScan.Show();
            }
        }
        private void BtnOperClose_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (bc.iniC.statusPasswordConfirm.Equals("1"))
            {
                FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
                frm.ShowDialog();
                frm.Dispose();
                if (bc.USERCONFIRMID.Length <= 0)       {                    lfSbMessage.Text = "Password ไม่ถูกต้อง";                    return;          }
            }
            String re = bc.bcDB.vsDB.updateStatusCloseVisit(HN, PRENO, VSDATE, bc.USERCONFIRMID);
            if(int.TryParse(re, out int _))             {                lfSbMessage.Text = "ปิด visit OK";                setGrfOperList("");            }
            else            {                lfSbMessage.Text = re;            }
        }
        private void TimeOperList_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfOperList("");
        }
        private void BtnApmPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (bc.cStf == null)
            {
                FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
                frm.ShowDialog();
                frm.Dispose();
                if (bc.USERCONFIRMID.Length <= 0)
                {
                    lfSbMessage.Text = "Password ไม่ถูกต้อง";
                    return;
                }
            }
            printAppointment();
        }
        private void printAppointment()
        {
            if (txtApmNO.Text.Trim().Length <= 0)
            {
                lfSbMessage.Text = "ไม่พบ เลขที่นัด";
                return;
            }
            APM = bc.bcDB.pt07DB.selectAppointment(txtApmDocYear.Text.Trim(), txtApmNO.Text.Trim());
            if (APM.MNC_APP_NO.Length <= 0)
            {
                lfSbMessage.Text = "ค้นหา นัด ไม่พบ";
                return;
            }
            PrintDocument pdStaffNote = new PrintDocument();
            pdStaffNote.PrinterSettings.PrinterName = bc.iniC.printerA5;
            pdStaffNote.DefaultPageSettings.PaperSize = new PaperSize("A4", 826, 1169);
            pdStaffNote.DefaultPageSettings.Landscape = false;
            pdStaffNote.PrintPage += Document_PrintPageAppointment;

            pdStaffNote.Print();
            pdStaffNote.Dispose();

            lfSbMessage.Text = "พิมพ์ นัด OK";
        }
        
        private void BtnApmNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlTabApm(true);
        }
        private void TxtApmRemark_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                txtApmDtr.Focus();
            }
        }

        private void TxtApmTel_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                txtApmRemark.Focus();
            }
        }

        private void TxtApmDsc_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                if (txtApmList.Text.Length > 0)
                {
                    txtApmList.Value += "\r\n" + txtApmDsc.Text.Trim();
                }
                else
                {
                    txtApmList.Value += txtApmDsc.Text.Trim();
                }
                txtApmTel.Focus();
            }
        }
        private void setTxtApmList()
        {
            lApm.Add(txtApmDtr.Text.Trim());
        }
        private void CboApmDept_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            txtApmDsc.Focus();
        }

        private void TxtApmTime_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                cboApmDept.Focus();
            }
        }

        private void TxtPttApmDate_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            txtApmTime.Focus();
            setApmCnt();
        }

        private void TxtSearchItem_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.Focus();
        }

        private void BtnItemAdd_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfOrderItem();
        }
        private void setGrfOrderItem()
        {
            setGrfOrderItem(txtItemCode.Text.Trim(), lbItemName.Text, txtItemQTY.Text.Trim()
                , chkItemLab.Checked ? "lab" : chkItemXray.Checked ? "xray" : chkItemProcedure.Checked ? "procedure" : chkItemDrug.Checked ? "drug" : "");
            txtSearchItem.Value = "";
            txtSearchItem.Focus();
            //grfOrder.Rows.Add(rowitem);
        }
        private void setGrfOrderItem(String code, String name, String qty, String flag)
        {
            if (grfOrder == null) { return; }
            //if(grfOrder.Row<=0) { return; }
            Row rowitem = grfOrder.Rows.Add();
            rowitem[colgrfOrderCode] = code;
            rowitem[colgrfOrderName] = name;
            rowitem[colgrfOrderQty] = qty;
            rowitem[colgrfOrderStatus] = flag;
            rowitem[colgrfOrderID] = "";
            rowitem[colgrfOrderReqNO] = "";
            rowitem[colgrfOrdFlagSave] = "0";//ต้องการ save ลง table temp_order
            rowitem[colgrfOrdControlRemark] = LAB.control_remark??"";
            rowitem[colgrfOrdControlYear] = LAB.control_year ?? "";
            rowitem[colgrfOrdPassSupervisor] = LAB.passsupervisor ?? "";
            rowitem[colgrfOrdSupervisor] = LAB.control_supervisor ?? "";
            rowitem[colgrfOrdStatusControl] = LAB.status_control ?? "";

            txtSearchItem.Value = "";
            txtSearchItem.Focus();
            //grfOrder.Rows.Add(rowitem);
        }
        
        private void setGrfOrderTemp()
        {//ดึงจาก table temp_order
            lstAutoComplete.Items.Clear();
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectOrderTempByHN(txtOperHN.Text.Trim(), VSDATE, PRENO);
            int i = 1, j = 1;
            grfOrder.Rows.Count = 1;            grfOrder.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                try
                {
                    Row rowa = grfOrder.Rows[i];
                    rowa[colgrfOrderCode] = row1["order_code"].ToString();
                    rowa[colgrfOrderName] = row1["order_name"]?.ToString()??"";
                    rowa[colgrfOrderQty] = row1["qty"].ToString();
                    rowa[colgrfOrderStatus] = row1["flag"]?.ToString()??"";
                    rowa[colgrfOrderID] = row1["id"].ToString();
                    rowa[colgrfOrderReqNO] = "";
                    rowa[colgrfOrdFlagSave] = "1";//ดึงข้อมูลจาก table temp_order 
                    rowa[colgrfOrdControlRemark] = row1["control_remark"]?.ToString()??"";
                    rowa[colgrfOrdControlYear] = row1["control_year"]?.ToString() ?? "";
                    rowa[colgrfOrdStatusControl] = row1["status_control"]?.ToString() ?? "";
                    rowa[colgrfOrdPassSupervisor] = row1["pass_supervisor"]?.ToString() ?? "";
                    rowa[colgrfOrdSupervisor] = row1["supervisor"]?.ToString() ?? "";
                    rowa[0] = i.ToString();
                    rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
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
        private void TxtSearchItem_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                if (txtSearchItem.Text.Trim().Length <=0) return;
                String[] txt = txtSearchItem.Text.Trim().Split('#');
                if (txt.Length < 2) { lfSbMessage.Text = "ไม่พบcode"; return; }
                timeOperList.Stop();
                List<Item> items = new List<Item>();
                Item item = new Item();
                item.code = txt[1];
                item.name = txt[0];
                items.Add(item);
                setOrderItem(items, txtSearchItem);
                timeOperList.Start();
                txtItemCode.Focus();
            }
        }
        private void TxtItemCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setGrfOrderItem();
            }
        }
        private void ChkItemDrug_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoDrug;
            clearControlOrder();
        }
        private void ChkItemHotC_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoProcedure;
            clearControlOrder();
            txtSearchItem.Focus();
        }
        private void ChkItemXray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoXray;
            clearControlOrder();
            txtSearchItem.Focus();
        }
        private void ChkItemLab_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoLab;
            clearControlOrder();
            txtSearchItem.Focus();
        }
        private void BtnApmSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            /*
             * Head_data.MNC_DOC_YR    := yr;
               Head_data.MNC_DOC_NO    := no;
               Head_data.MNC_DEP_NO    := STRTOINTDEF(cboDepno.TEXT,0);
               Head_data.MNC_SEC_NO    := STRTOINTDEF(cboSecno.TEXT,0);
               Head_data.MNC_DEPR_NO   := StrToIntDef(dep_cd,0);
               Head_data.MNC_SECR_NO   := StrToIntDef(Sec_cd,0);
               Head_data.MNC_APP_TIM   := appts;
               Head_data.MNC_APP_NO    := Queno;
               Head_data.MNC_AGE       := 0;
               Head_data.MNC_APP_DAT   := APP_DAT;
               Head_data.MNC_APP_BY    := '';
               Head_data.MNC_STS       := FLG;
               Head_data.MNC_APP_OR_FLG  := '';
               Head_data.MNC_APP_ADM_FLG := '';
               Head_data.MNC_SEND_CARD   := '';
               Head_data.IOSTS           := 'I';
               Head_data.MNC_APP_STS     := '';
               Head_data.MNC_SEX         := '';
               Head_data.MNC_EMPR_CD     := empcode;
               Head_data.MNC_DOT_CD      := cboDotcd.TEXT;
               Head_data.MNC_APP_TEL     := '';
               Head_data.MNC_NAME        := edtName.Text;
               Head_data.MNC_APP_ADD     := '';
               Head_data.MNC_APP_TIM_E   := appte;
               Head_data.MNC_APP_TYP     := apptime_typ;
            RdoAppTyp คือ 0=รักษาต่อเนื่อง follow up,1=ทำการ,2=ฟังผล result
            chkPaid คือ ชำระเงินครบแล้ว
            
                if (RdoAppTyp.ItemIndex=0) and (chkPaid.Checked=False) then Flg:='1';
              if (RdoAppTyp.ItemIndex=1) and (chkPaid.Checked=False) then Flg:='2';
              if (RdoAppTyp.ItemIndex=2) and (chkPaid.Checked=False) then Flg:='3';
              if (RdoAppTyp.ItemIndex=0) and (chkPaid.Checked=True) then Flg:='4';
              if (RdoAppTyp.ItemIndex=1) and (chkPaid.Checked=True) then Flg:='5';
              if (RdoAppTyp.ItemIndex=2) and (chkPaid.Checked=True) then Flg:='6';
              if (Flg='4') or (Flg='5') or (Flg='6') then Rem := '' //'*** ชำระเงินครบแล้ว ***'
             */
            lfSbMessage.Text = "";
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);            frm.ShowDialog();            frm.Dispose();
            if (bc.USERCONFIRMID.Length <= 0)            {                lfSbMessage.Text = "Password ไม่ถูกต้อง";                return;            }
            int limit = 0, cnt = 0;
            String cnts = "";
            lbDtrApmLimit.Text = lbDtrApmLimit.Text.Replace("limit ", "");
            if(!int.TryParse(lbDtrApmLimit.Text, out limit)) { lfSbMessage.Text = "limit no set"; return; }
            DateTime.TryParse(txtPttApmDate.Text, out DateTime apmdate);
            if (apmdate == null) { lfSbMessage.Text = "error txtPttApmDate.Text"; return; }
            if (apmdate.Year < 1900) apmdate = apmdate.AddYears(543);
            String date1 = apmdate.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
            cnts = bc.bcDB.pt07DB.countAppointmentByDtrDate(txtApmDtr.Text.Trim(), date1, "1");
            if(cnts.Length<=0)            { cnts = "0"; }
            if (cnts.Length>5)            {                cnts = cnts.Replace("[", "").Replace("]", "").Replace("->", "").Replace(";", ""); cnts = cnts.Replace(date1, "");         }
            
            if (!int.TryParse(cnts, out cnt)) { lfSbMessage.Text = "count no set"; return; }
            if(cnt >= limit)            {                lfSbMessage.Text = "วันที่ "+ date1+"แพทย์มีนัดเต็มแล้ว กรุณาเลือกวันอื่น";                return;            }
            PatientT07 apm = getApm();
            if (apm != null)
            {
                string re = bc.bcDB.pt07DB.insertPatientT07(apm);
                if (long.TryParse(re, out long chk))
                {
                    txtApmNO.Value = apm.MNC_DOC_NO.Length > 3 ? txtApmNO.Text.Trim() : re;
                    txtApmDocYear.Value = apm.MNC_DOC_YR;
                    if (grfApmOrder.Rows.Count > 1)
                    {
                        foreach(Row rowa in grfApmOrder.Rows)
                        {
                            String ordercode = rowa[colgrfOrderCode].ToString(); if (ordercode.Equals("code")) continue;
                            String ordername = rowa[colgrfOrderName].ToString();
                            String flag = rowa[colgrfOrderStatus].ToString();//O hotcharge, X xray, L lab
                            flag = flag.Equals("lab") ? "L" : flag.Equals("xray") ? "X" : flag.Equals("procedure") ? "O" : flag;

                            String re1 = bc.bcDB.pt07DB.insertPatientT073(txtApmNO.Text.Trim(), txtApmDocYear.Text.Trim(), ordercode, "", flag);
                            if (!long.TryParse(re1, out long chk1))
                            {
                                new LogWriter("e", "FrmOPD BtnApmSave_Click " + re1);
                                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnApmSave_Click save  ", re1);
                                lfSbMessage.Text = re1;
                            }
                        }
                    }
                    lfSbStatus.Text = txtApmNO.Text.Length>0 ? "update appointment OK" : "insert appointment OK";
                    setGrfPttApm();
                    if (txtApmNO.Text.Length <= 0)
                    {
                        txtApmNO.Value = re;//insert ถ้าแก้ไข  นัด ไม่ต้องใส่ค่า
                    }
                    btnApmPrint.Focus();
                }
                else
                {
                    new LogWriter("e", "FrmOPD BtnApmSave_Click " + re);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnApmSave_Click save  ", re);
                    lfSbMessage.Text = re;
                }
            }
            else
            {
                lfSbMessage.Text = "ไม่พบ visit";
            }
        }

        private void TxtApmDtr_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                setApmCnt();            }
        }
        private void setApmCnt()
        {
            try
            {
                PatientM26 dtr = new PatientM26();
                dtr = bc.bcDB.pm26DB.selectByPk(txtApmDtr.Text.Trim());
                lbApmDtrName.Text = dtr.dtrname;
                lbDtrApmLimit.Text = "limit " + dtr.MNC_APP_NO;
                DateTime.TryParse(txtPttApmDate.Text, out DateTime apmdate);
                if (apmdate == null) { lfSbMessage.Text = "error txtPttApmDate.Text"; return; }
                if (apmdate.Year < 1900) apmdate = apmdate.AddYears(543);
                lbDtrApmCnt.Text = "cnt " + bc.bcDB.pt07DB.countAppointmentByDtrDate(txtApmDtr.Text.Trim(), apmdate.Year + "-" + apmdate.ToString("MM-dd"), "7");
            }
            catch(Exception ex)
            {
                lfSbMessage.Text = ex.Message;
                return;
            }
        }
        private void BtnPrnStaffNote1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printStaffNote("1");
        }

        private void BtnScanSaveImg_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (bc.iniC.statusPasswordConfirmLow.Equals("1"))
            {
                FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
                frm.ShowDialog();
                frm.Dispose();
                if (bc.USERCONFIRMID.Length <= 0)       {           lfSbMessage.Text = "Password ไม่ถูกต้อง";          return;                }
            }
            saveImgStaffNote();
        }
        private void saveImgStaffNote()
        {
            if (VSDATE.Length <= 0) return;
            if (PRENO.Length <= 0) return;
            if (HN.Length <= 0) return;
            String file = "", dd = "", mm = "", yy = "", err = "", preno1 = "";
            try
            {
                Boolean chkL = false, chkR = false;
                err = "00";
                lfSbMessage.Text =VSDATE+ " " + PRENO+" ["+bc.user.staff_fname_t+" " + bc.user.staff_lname_t+"]";
                String filenameS = "";
                filenameS = "000000" + PRENO;
                filenameS = filenameS.Substring(filenameS.Length - 6);

                String filenameR = "", path = bc.iniC.pathScanStaffNote, path1 = bc.iniC.pathScanStaffNote, year = "", mon = "", day = "";
                year = VSDATE.Substring(0, 4);                mon = VSDATE.Substring(5, 2);                day = VSDATE.Substring(8, 2);
                path += year + "\\" + mon + "\\" + day + "\\";
                //path1 += year + "\" + mon + "\" + day + "\";

                filenameR = "000000" + PRENO;
                filenameR = filenameR.Substring(filenameR.Length - 6);
                err = "07";
                //new LogWriter("e", "genImgStaffNote path filenameS " + path + filenameS);
                //Image bitmap = picL.Image;
                //bitmap.Save("\\\\" + path + filenameS + "S.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                //imgL.Save(filenameS + "R.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                //err = "08";
                //bitmap = picR.Image;
                //new LogWriter("e", "genImgStaffNote path filenameS " + path + filenameR);
                //bitmap.Save("\\\\" + path + filenameR + "R.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                //imgR.Save(filenameR + "S.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);

                string filePathS = "\\\\" + path + filenameS + "S.JPG";     //R คือ ขวางขวา S คือ ขวางซ้าย
                string filePathR = "\\\\" + path + filenameR + "R.JPG";
                if (File.Exists(filePathS))     {           if (!IsFileLocked(filePathS))           {               File.Delete(filePathS);         }                }
                err = "08";
                if (File.Exists(filePathR))         {               if (!IsFileLocked(filePathR))           {       File.Delete(filePathR);         }                }
                err = "09";
                //path = path.Replace("\\\\", "\\");
                //using (new NetworkConnection(@"\\" + path, new NetworkCredential(bc.iniC.usersharepathstaffnote, bc.iniC.passwordsharepathstaffnote)))
                //{
                //    // ดำเนินการกับไฟล์ใน shared path
                //    //File.Copy(@"\\shared\path\file.txt", @"C:\local\file.txt");
                //    File.Move(FILEL.FullName, filePathS);
                //    File.Move(FILER.FullName, filePathR);
                //}
                //File.Move(FILEL.FullName, filePathS);
                //File.Move(FILER.FullName, filePathR);
                if (!File.Exists(filePathS))
                {
                    File.Move(FILEL.FullName, filePathS);                    chkR = true;
                }
                else
                {
                    //using (FileStream fsR = new FileStream(filePathR, FileMode.Open, FileAccess.Read, FileShare.Read))
                    //{
                    //    using (Image imgr = Image.FromStream(fsR))
                    //    {
                    //        picR.Image = (Image)imgr.Clone();
                    //    }
                    //}
                }
                err = "10";
                if (!File.Exists(filePathR))
                {
                    File.Move(FILER.FullName, filePathR);                    chkL = true;
                }
                else
                {
                    //using (FileStream fsS = new FileStream(filePathS, FileMode.Open, FileAccess.Read, FileShare.Read))
                    //{
                    //    using (Image imgl = Image.FromStream(fsS))
                    //    {
                    //        picL.Image = (Image)imgl.Clone();
                    //    }
                    //}
                }
                err = "11";
                String folderPath = bc.iniC.pathlocalStaffNote;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                string[] files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    foreach (String file1 in files)
                    {
                        File.Delete(file1);
                    }
                }
                err = "12";
                String re = bc.bcDB.vsDB.updateActNo111(HN, PRENO, VSDATE);
                if (chkL && chkR)
                    stt.Show("<p>" + FILER.FullName + " ✅</p>", picL, 2000);
            }
            catch (Exception ex)
            {
                //lfSbStatus.Text = ex.Message.ToString();
                MessageBox.Show("saveImgStaffNote " + ex.Message, "");      //user ไม่ต้องการเห็น error
                lfSbMessage.Text = err + " saveImgStaffNote " + ex.Message;
                new LogWriter("e", "FrmOPD saveImgStaffNote " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "saveImgStaffNote", ex.Message);
                if (bc.iniC.statusShowMessageError.Equals("1"))     {       MessageBox.Show("saveImgStaffNote " + ex.Message, "");      }
            }
        }
        private bool IsFileLocked(string filePath)
        {
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))    {       stream.Close(); }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }
        private void printStaffNote(String template)
        {
            TEMPLATESTAFFNOTE = template;
            if (template.Equals("checkup_doe"))            {                if (txtCheckUPHN.Text.Length <= 0) { return; }            }
            else            {                if (txtOperHN.Text.Length <= 0) { return; }            }
            //new LogWriter("e", "FrmOPD printStaffNote 00");
            PrintDocument documentStaffNote = new PrintDocument();
            documentStaffNote.PrinterSettings.PrinterName = bc.iniC.printerStaffNote;
            documentStaffNote.DefaultPageSettings.Landscape = true;
            if (TEMPLATESTAFFNOTE.Equals("0"))
            {
                //new LogWriter("e", "FrmOPD printStaffNote 01");
                documentStaffNote.PrintPage += Document_PrintPageStaffNote;
            }
            else if (TEMPLATESTAFFNOTE.Equals("checkup_doe"))
            {
                //new LogWriter("e", "FrmOPD printStaffNote 02");
                documentStaffNote.PrintPage += Document_PrintPageStaffNote;
            }
            else
            {
                //new LogWriter("e", "FrmOPD printStaffNote 02");
                documentStaffNote.PrintPage += Document_PrintPageStaffNote;
            }
            documentStaffNote.Print();
        }
        
        private void printQueueDtr()
        {
            try
            {
                PrintDocument document = new PrintDocument();
                document.PrinterSettings.PrinterName = bc.iniC.printerQueue;
                document.PrintPage += Document_PrintPageQueDtr;
                document.DefaultPageSettings.Landscape = false;
                document.Print();
            }
            catch (Exception ex)            {            }
        }
        
        private void TCOrder_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            btnScanClearImg.Visible = false;
            btnScanGetImg.Visible = false;
            btnScanSaveImg.Visible = false;
            if (tCOrder.SelectedTab == tabScan)
            {
                btnScanClearImg.Visible = true;
                btnScanGetImg.Visible = true;
                btnScanSaveImg.Visible = true;
                getImgStaffNote();
            }
            else if (tCOrder.SelectedTab == tabApm)
            {
                clearControlTabApm(false);
                setGrfPttApm();
            }
            else if (tCOrder.SelectedTab == tabHistory)
            {
                initGrfProcedure(); setGrfOPD();
            }
            else if (tCOrder.SelectedTab == tabOrder)   { initGrfPrenoProcedure(); setGrfOPD();    setGrfOrderLab();   setGrfOrderTemp(); setGrfOrderXray(); setGrfPrenoProc(); }
        }
        private void getImgStaffNote()
        {
            if (VSDATE.Length <= 0) return;
            if (PRENO.Length <= 0) return;
            if (HN.Length <= 0) return;
            String file = "", dd = "", mm = "", yy = "", err = "", preno1="";
            try
            {
                err = "00";
                picL.Image = null;
                picR.Image = null;
                int chk = 0;
                err = "01";
                dd = VSDATE.Substring(VSDATE.Length - 2);
                mm = VSDATE.Substring(5, 2);
                yy = VSDATE.Substring(0, 4);
                err = "02";
                int.TryParse(yy, out chk);
                if (chk > 2500)
                    chk -= 543;
                file = "\\\\" + bc.iniC.pathScanStaffNote + chk + "\\" + mm + "\\" + dd + "\\";
                preno1 = "000000" + PRENO;
                err = "03";
                preno1 = preno1.Substring(preno1.Length - 6);
                err = "04";
                //new LogWriter("e", "FrmScanView1 setStaffNote file  " + file + preno1+" hn "+txtHn.Text+" yy "+yy);
                //picL.Image = Image.FromFile(file + preno1 + "S.JPG");
                Boolean chkL=false, chkR=false;
                if (File.Exists(file + preno1 + "S.JPG"))
                {
                    // ✅ สร้าง Image ใหม่แต่ละครั้ง
                    using (Image original = Image.FromFile(file + preno1 + "S.JPG"))
                    {
                        // Clone เพื่อไม่ lock file และแยก reference
                        picL.Image = (Image)original.Clone();
                    }
                    chkL = true;
                }
                //picR.Image = Image.FromFile(file + preno1 + "R.JPG");
                if (File.Exists(file + preno1 + "R.JPG"))
                {
                    // ✅ สร้าง Image ใหม่แต่ละครั้ง
                    using (Image original = Image.FromFile(file + preno1 + "R.JPG"))
                    {
                        // Clone เพื่อไม่ lock file และแยก reference
                        picR.Image = (Image)original.Clone();
                    }
                    chkR = true;
                }
                //stt.SetToolTip(picL, file + preno1 + "S.JPG");
                //stt.Show(file + preno1 + "S.JPG", picL, 2);
                if(chkL && chkR)
                    stt.Show("<p>" + file + preno1 + "S.JPG" + " ✅</p>", picL,2000);
            }
            catch (Exception ex)
            {
                lfSbStatus.Text = ex.Message.ToString();
                lfSbMessage.Text = err + " getImgStaffNote " + ex.Message;
                new LogWriter("e", "FrmOPD getImgStaffNote " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "getImgStaffNote", ex.Message);
            }
        }
        
        private void TxtOperCcex_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperDtr.Focus();            }
        }

        private void TxtOperCcin_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperCcex.Focus();            }
        }

        private void TxtOperCc_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperCcin.Focus();            }
        }

        private void TxtOperHc_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperCc.Focus();            }
        }

        private void TxtOperHt_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperHc.Focus();            }
        }

        private void TxtOperWt_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperHt.Focus();
            }
        }

        private void TxtOperAbc_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperWt.Focus();            }
        }

        private void TxtBp2R_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperAbc.Focus();            }
        }

        private void TxtBp2L_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperBp2R.Focus();            }
        }

        private void TxtBp1R_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperWt.Focus();            }
        }

        private void TxtBp1L_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperBp1R.Focus();            }
        }

        private void TxtOperRh_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperBp1L.Focus();            }
        }

        private void TxtOperAbo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperRh.Focus();            }
        }

        private void TxtOperRrate_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperBp1L.Focus();            }
        }

        private void TxtOperHrate_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtOperRrate.Focus();            }
        }
        private void TxtOperTemp_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)            {                txtOperHrate.Focus();            }
        }

        private void TxtOperDtr_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as C1TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private void BtnOperSaveVital_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            saveVitalSign();
        }
        private void saveVitalSign()
        {
            if (bc.iniC.statusPasswordConfirmLow.Equals("1"))
            {
                FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
                frm.ShowDialog();
                frm.Dispose();
                if (bc.USERCONFIRMID.Length <= 0)
                {
                    lfSbMessage.Text = "Password ไม่ถูกต้อง";
                    return;
                }
            }
            Visit vs = new Visit();
            vs = setVisitVitalsign(tabOper.Name);
            vs.preno = PRENO;
            vs.VisitDate = VSDATE;
            vs.HN = HN;
            String re = bc.bcDB.vsDB.updateVitalSign(vs, bc.USERCONFIRMID);
            String re1 = bc.bcDB.vsDB.updateActNo113(HN, PRENO, VSDATE);
            if (long.TryParse(re, out long chk))
            {
                lfSbMessage.Text = "update vitalsign OK";
            }
        }
        private void BtnOperSaveDtr_Click(object sender, EventArgs e)     
        {
            //throw new NotImplementedException();
            if (bc.iniC.statusPasswordConfirmLow.Equals("1"))
            {
                FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
                frm.ShowDialog();
                frm.Dispose();
                if (bc.USERCONFIRMID.Length <= 0)
                {
                    lfSbMessage.Text = "Password ไม่ถูกต้อง";
                    return;
                }
            }
            saveDoctor();
        }
        private void saveDoctor()
        {
            String re = bc.bcDB.vsDB.updateDoctor(HN, PRENO, VSDATE, txtOperDtr.Text.Trim(), bc.USERCONFIRMID);
            String re1 = bc.bcDB.sumt03DB.insertSummaryT03(txtOperDtr.Text.Trim(), bc.iniC.station);
            String re2 = bc.bcDB.vsDB.updateActNoSendToDoctor110(HN, PRENO, VSDATE);
            if (long.TryParse(re, out long chk))
            {
                lfSbMessage.Text = "update Doctor OK";
                setGrfOperList("");
            }
        }
        private void TxtOperDtr_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                lbOperDtrName.Text = bc.selectDoctorName(txtOperDtr.Text.Trim());
            }
        }
        private void TxtCheckUPBreath_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void TxtCheckUPPulse_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPRrate.Focus();
            }
        }

        private void TxtCheckUPBloodPressure_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPHrate.Focus();
            }
        }

        private void TxtCheckUPHeight_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPBp1L.Focus();
            }
        }

        private void TxtCheckUPWeight_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPHeight.Focus();
            }
        }

        private void TxtCheckUPTempu_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPWeight.Focus();
            }
        }

        private void TxtCheckUPRhgroup_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPTempu.Focus();
            }
        }

        private void TxtCheckUPABOGroup_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPRhgroup.Focus();
            }
        }

        private void TxtCheckUPPhone_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPABOGroup.Focus();
            }
        }

        private void TxtCheckUPAddr2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPPhone.Focus();
            }
        }

        private void TxtCheckUPAddr1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPAddr2.Focus();
            }
        }

        private void TxtCheckUPEmplyer_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPAddr1.Focus();
            }
        }

        private void TxtCheckUPMobile1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPEmplyer.Focus();
            }
        }

        private void TxtCheckUPSurNameE_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPMobile1.Focus();
            }
        }

        private void TxtCheckUPNameE_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPSurNameE.Focus();
            }
        }

        private void TxtCheckUPPassport_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPNameE.Focus();
            }
        }

        private void TxtCheckUPSurNameT_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPPassport.Focus();
            }
        }

        private void TxtCheckUPNameT_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPSurNameT.Focus();
            }
        }

        private void BtnCheckUPPrn7Disease_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void BtnCheckUPOrder_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkCheckUPSelect.Checked && ((ComboBoxItem)cboCheckUPSelect.SelectedItem).Value.ToString().Equals("doeonline") && !STATUSQUICKORDER.Equals("1"))
            {
                FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
                frm.ShowDialog();
                frm.Dispose();
                if (bc.USERCONFIRMID.Length <= 0)
                {
                    lfSbMessage.Text = "Password ไม่ถูกต้อง";
                    return;
                }
                foreach(Row dataRow in grfChkPackItems.Rows)
                {
                    String flagItem =  dataRow[colChkPackItemflag].ToString();
                    String itemcode = dataRow[colChkPackItemsitemcode].ToString();
                    String packcode = dataRow[colChkPackItemsPackcode].ToString();
                    if (flagItem.Equals("L"))
                    {
                        String re = bc.bcDB.labT01DB.insertCOVID(PTT.MNC_HN_NO, VSDATE, PRENO, itemcode, "1618");
                        if (long.TryParse(re,out long chk1))
                        {
                            bc.bcDB.vsDB.updateStatusCloseVisitLab(PTT.MNC_HN_NO, PTT.MNC_HN_YR, PRENO, VSDATE);
                        }
                    }
                    else if (flagItem.Equals("O"))
                    {

                    }
                    else if (flagItem.Equals("X"))
                    {
                        String reXray = "";
                        XrayT01 xrayT01 = new XrayT01();
                        xrayT01 = bc.setXrayT01(PTT.MNC_HN_NO, PTT.MNC_HN_YR, VSDATE, PRENO, txtCheckUPDoctorId.Text.Trim());
                        reXray = bc.bcDB.xrayT01DB.insertXrayT01(xrayT01);
                        int chk = 0;
                        if (int.TryParse(reXray, out chk))
                        {
                            
                        }
                    }
                    else if (flagItem.Equals("F"))
                    {

                    }
                }
                bc.bcDB.vsDB.updateStatusQuickOrder(PTT.MNC_HN_NO, VSDATE, PRENO);
            }
        }
        private void TxtCheckUPDoctorId_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPDoctorName.Text = bc.selectDoctorName(txtCheckUPDoctorId.Text.Trim());
            }
        }
        private void TC1_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //ถ้าไม่ได้เปิด tab oper ให้ stop timer
            if (!TC1Active.Equals(tabOper.Name)) timeOperList.Stop();
            if (tC1.SelectedTab == tabCheckUP)
            {
                // ✅ Lazy Loading - init ครั้งแรกที่เข้า tab จริงๆ
                if (grfCheckUPList==null) { initGrfCheckUPList(); }
                if (!isCheckUPInitialized)
                {
                    showLbLoading();
                    setLbLoading("กำลังโหลด CheckUP...");
                    // Init ComboBox
                    bc.bcDB.pm02DB.setCboPrefixT_Fast(cboCheckUPPrefixT, "");
                    bc.bcDB.pm02DB.setCboPrefixE_Fast(cboCheckUPPrefixE, "");
                    bc.setCboChckUP(cboCheckUPSelect);
                    bc.bcDB.pm04DB.setCboNation(cboCheckUPNat, "");
                    bc.setCboAlienPosition(cboAlienPosition);
                    bc.setCboAlienCountry(cboCheckUPCountry);
                    bc.setCboSex(cboCheckUPSex);
                    bc.setCboSkintone(cboCheckUPskintone);
                    bc.bcDB.pm39DB.setCboPrefixT(cboCheckUPOrder, "3", "");//ใส่เพื่อให้ ดึงไม่ขึ้น
                    bc.setCboNormal(cboCheckUpEye);
                    bc.setCboNormal(cboCheckUpLung);
                    isCheckUPInitialized = true;
                    hideLbLoading();
                }
                tabMedScanActiveNOtabOutLabActive = false;
                TC1Active = tabCheckUP.Name;
                setGrfCheckUPList("");
                txtSBSearchHN.Visible = false;                btnSBSearch.Visible = false;                btnScanSaveImg.Visible = true;
                btnScanGetImg.Visible = true;                btnScanClearImg.Visible = true;                btnOperClose.Visible = true;

                txtSBSearchDate.Visible = true;                rb1.Visible = true;                rb2.Visible = true;
                rgSbModule.Visible = true;
            }
            else if (tC1.SelectedTab == tabOper)
            {
                if (grfOutLab == null) initGrfOutLab();
                tabMedScanActiveNOtabOutLabActive = false;                TC1Active = tabOper.Name;                txtSBSearchHN.Visible = true;
                txtSBSearchDate.Visible = false;                btnSBSearch.Visible = false;                btnScanSaveImg.Visible = true;
                btnScanGetImg.Visible = true;                btnScanClearImg.Visible = true;                btnOperClose.Visible = true;

                rb1.Visible = true;                rb2.Visible = true;                rgSbModule.Visible = true;
                setGrfOperList("");
                timeOperList.Start();
            }
            else if (tC1.SelectedTab == tabFinish)
            {
                if(grfOperFinish==null) { initGrfOperFinish(); }
                if(grfOperFinishDrug==null) { initGrfOperFinishDrug(); }
                if(grfOperFinishProcedure==null) { initGrfOperFinishProcedure(); }
                if(grfOperFinishLab==null) { initGrfLab(ref grfOperFinishLab,ref pnFinishLab); }
                if(grfOperFinishXray==null) { initGrfXray(ref grfOperFinishXray, ref pnFinishXray); }
                tabMedScanActiveNOtabOutLabActive = false;                TC1Active = tabFinish.Name;                txtSBSearchHN.Visible = false;
                txtSBSearchDate.Visible = false;                btnSBSearch.Visible = false;                btnScanSaveImg.Visible = false;
                btnScanGetImg.Visible = false;                btnScanClearImg.Visible = false;                btnOperClose.Visible = false;

                rb1.Visible = true;                rb2.Visible = true;                rgSbModule.Visible = true;
                setGrfOperFinish();
                tCFinish.SelectedTab = tabFinishStaffNote;
            }
            else if (tC1.SelectedTab == tabApm)
            {
                //if(grfPttApm==null) { initGrfPttApm(ref grfPttApm, ref pnPttApm, "grfPttApm"); }
                if(grfApm==null) { initGrfPttApm(ref grfApm, ref pnApm, "grfApm"); }
                tabMedScanActiveNOtabOutLabActive = false;                TC1Active = tabApm.Name;                txtSBSearchHN.Visible = false;
                txtSBSearchDate.Visible = false;                btnSBSearch.Visible = false;                btnScanSaveImg.Visible = false;
                btnScanGetImg.Visible = false;                btnScanClearImg.Visible = false;                btnOperClose.Visible = false;

                rb1.Visible = true;                rb2.Visible = true;                rgSbModule.Visible = true;
            }
            else if (tC1.SelectedTab == tabMedScan)
            {
                tabMedScanActiveNOtabOutLabActive = true;                TC1Active = tabMedScan.Name;                txtSBSearchHN.Visible = true;
                txtSBSearchDate.Visible = false;                btnSBSearch.Visible = true;                btnScanSaveImg.Visible = false;
                btnScanGetImg.Visible = false;                btnScanClearImg.Visible = false;                btnOperClose.Visible = false;

                rb1.Visible = true;                rb2.Visible = true;                rgSbModule.Visible = true;
            }
            else if (tC1.SelectedTab == tabSearch)
            {
                tabMedScanActiveNOtabOutLabActive = false;
                TC1Active = tabSearch.Name;
                if(grfSrc==null) { initGrfSrc(); }
                if (grfEKG==null)    {   initGrfEKG();}
                if (grfECHO == null) { initGrfECHO(); }
                if (grfEST == null) { initGrfEST(); }
                if (grfHolter == null) { initGrfHolter(); }
                if (grfDocOLD == null) { initGrfDocOLD(); }
                if(grfSrcXray==null) { initGrfSrcXray(); }
                if(grfSrcLab==null) { initGrfSrcLab(); }
                if(grfSrcVs==null) { initGrfSrcVs(); }
                if(grfSrcProcedure==null) { initGrfSrcProcedure(); }
                if (grfSrcOrder == null) { initGrfSrcOrder(); }
                if(grfSrc ==null) { initGrfSrc(); }
                txtSrcHn.Focus();
                txtSBSearchHN.Visible = false;                txtSBSearchDate.Visible = false;                btnSBSearch.Visible = false;
                btnScanSaveImg.Visible = bc.iniC.authenedit.Equals("1") ? true : false;       //ต้องการให้ upload file scan ได้    68-12-01
                btnScanGetImg.Visible = bc.iniC.authenedit.Equals("1") ? true: false;       //ต้องการให้ upload file scan ได้      68-12-01
                btnScanClearImg.Visible = false;                btnOperClose.Visible = false;

                rb1.Visible = true;                rb2.Visible = true;                rgSbModule.Visible = true;
            }
            else if (tC1.SelectedTab == tabOutLabDate)//date search
            {
                if(grfOutLab==null) initGrfOutLab();
                tabMedScanActiveNOtabOutLabActive = false;                TC1Active = tabOutLabDate.Name;                txtSBSearchHN.Visible = false;
                txtSBSearchDate.Visible = true;                btnSBSearch.Visible = true;                btnScanSaveImg.Visible = false;
                btnScanGetImg.Visible = false;                btnScanClearImg.Visible = false;                btnOperClose.Visible = false;

                rb1.Visible = true;                rb2.Visible = true;                rgSbModule.Visible = true;
            }
            else if (tC1.SelectedTab == tabOutlab)//hn search
            {
                if(grfTodayOutLab==null) initGrfTodayOutLab();
                setGrfTodayOutLab();
                tabMedScanActiveNOtabOutLabActive = false;                TC1Active = tabOutlab.Name;                txtSBSearchHN.Visible = true;
                txtSBSearchDate.Visible = false;                btnSBSearch.Visible = true;                btnScanSaveImg.Visible = false;
                btnScanGetImg.Visible = false;                btnScanClearImg.Visible = false;                btnOperClose.Visible = false;

                rb1.Visible = true;                rb2.Visible = true;                rgSbModule.Visible = true;
            }
            else if (tC1.SelectedTab == tabStaffNote)
            {
                tabMedScanActiveNOtabOutLabActive = false;                TC1Active = tabStaffNote.Name;                txtSBSearchHN.Visible = false;
                txtSBSearchDate.Visible = true;                btnSBSearch.Visible = true;                btnScanSaveImg.Visible = false;
                btnScanGetImg.Visible = false;                btnScanClearImg.Visible = false;                btnOperClose.Visible = false;

                rb1.Visible = true;                rb2.Visible = true;                rgSbModule.Visible = true;
                if(picStaffNote.Image!=null) picStaffNote.Image.Dispose();
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\temp_med\\medicalrecord.jpg"))
                {
                    IMGSTAFFNOTE = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "\\temp_med\\medicalrecord.jpg");
                    
                    if(IMGSTAFFNOTE.Width< IMGSTAFFNOTE.Height)
                    {
                        IMGSTAFFNOTE = bc.RotateImage90(IMGSTAFFNOTE);
                        picStaffNote.Image = IMGSTAFFNOTE;
                    }
                }
            }
            else if (tC1.SelectedTab == tabMapPackage)
            {
                if(grfMapPackage==null) { initGrfMapPackage(); }
                if(grfMapPackageViewhelp==null) { initGrfMapPackageViewhelp(); }
                if(grfPackage==null) { initGrfpackage(); }
                if(grfpackageitems==null) { initGrfMapPackageItmes(); }
            }
            else
            {
                txtSBSearchHN.Visible = false;                rb1.Visible = false;                btnSBSearch.Visible = false;
            }
        }
        /*
         * grf company map package
         */
        
        
        private void setControlPatientByGrf(String hn)
        {
            setGrfSrcVs(hn);
            lfSbStatus.Text = "";            lfSbMessage.Text = "";            lbSrcPreno.Value = "";            lbSrcVsDate.Value = "";            lbSrcVN.Value = "";
            setGrfDocOLD();            setGrfEKG();            setGrfEST();            setGrfECHO();            setGrfHolter();            setGrfCertMed();
        }
        
        private void setSrcStaffNote(String vsDate, String preno)
        {
            String file = "", dd = "", mm = "", yy = "", err = "";
            picSrcL.Image = null;
            picSrcR.Image = null;
            if (vsDate.Length > 8)
            {
                String preno1 = preno;
                try
                {
                    err = "00";
                    //imgLR = null;
                    int chk = 0;
                    err = "01";
                    dd = vsDate.Substring(vsDate.Length - 2);
                    mm = vsDate.Substring(5, 2);
                    yy = vsDate.Substring(0, 4);
                    err = "02";
                    int.TryParse(yy, out chk);
                    if (chk > 2500)
                        chk -= 543;
                    file = "\\\\" + bc.iniC.pathScanStaffNote + chk + "\\" + mm + "\\" + dd + "\\";
                    preno1 = "000000" + preno1;
                    err = "03";
                    preno1 = preno1.Substring(preno1.Length - 6);
                    err = "04";
                    //new LogWriter("e", "FrmScanView1 setStaffNote file  " + file + preno1+" hn "+txtHn.Text+" yy "+yy);
                    //stffnoteR = Image.FromFile(file + preno1 + "R.JPG");
                    //stffnoteS = Image.FromFile(file + preno1 + "S.JPG");
                    picSrcL.Image = Image.FromFile(file + preno1 + "S.JPG");
                    picSrcR.Image = Image.FromFile(file + preno1 + "R.JPG");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("ไม่พบ StaffNote ในระบบ " + ex.Message, "");
                    //lfSbStatus.Text = ex.Message.ToString();
                    lfSbMessage.Text = err + " setSrcStaffNote " + ex.Message;
                    new LogWriter("e", "FrmOPD setSrcStaffNote " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setSrcStaffNote", ex.Message);

                }
            }
        }
        
        
        class listStream
        {
            public String id = "", dgsid = "";
            public MemoryStream stream;
        }
        
        private void setStaffNote(String vsDate, String preno, C1PictureBox picL, C1PictureBox picR)
        {
            String file = "", dd = "", mm = "", yy = "", err = "";
            picL.Image = null;
            picR.Image = null;
            if (vsDate.Length > 8)
            {
                String preno1 = preno;
                try
                {
                    err = "00";
                    //imgLR = null;
                    int chk = 0;
                    err = "01";
                    dd = vsDate.Substring(vsDate.Length - 2);
                    mm = vsDate.Substring(5, 2);
                    yy = vsDate.Substring(0, 4);
                    err = "02";
                    int.TryParse(yy, out chk);
                    if (chk > 2500)
                        chk -= 543;
                    file = "\\\\" + bc.iniC.pathScanStaffNote + chk + "\\" + mm + "\\" + dd + "\\";
                    preno1 = "000000" + preno1;
                    err = "03";
                    preno1 = preno1.Substring(preno1.Length - 6);
                    err = "04";
                    //new LogWriter("e", "FrmScanView1 setStaffNote file  " + file + preno1+" hn "+txtHn.Text+" yy "+yy);
                    //stffnoteR = Image.FromFile(file + preno1 + "R.JPG");
                    //stffnoteS = Image.FromFile(file + preno1 + "S.JPG");
                    //picL.Image = Image.FromFile(file + preno1 + "S.JPG");
                    if (File.Exists(file + preno1 + "S.JPG"))
                    {
                        // ✅ สร้าง Image ใหม่แต่ละครั้ง
                        using (Image original = Image.FromFile(file + preno1 + "S.JPG"))
                        {
                            // Clone เพื่อไม่ lock file และแยก reference
                            picL.Image = (Image)original.Clone();
                        }
                    }
                    //picR.Image = Image.FromFile(file + preno1 + "R.JPG");
                    if (File.Exists(file + preno1 + "R.JPG"))
                    {
                        // ✅ สร้าง Image ใหม่แต่ละครั้ง
                        using (Image original = Image.FromFile(file + preno1 + "R.JPG"))
                        {
                            // Clone เพื่อไม่ lock file และแยก reference
                            picR.Image = (Image)original.Clone();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("ไม่พบ StaffNote ในระบบ " + ex.Message, "");
                    //lfSbStatus.Text = ex.Message.ToString();
                    lfSbMessage.Text = err + " setStaffNote " + ex.Message;
                    new LogWriter("e", "FrmOPD setStaffNote " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setStaffNote", ex.Message);

                }
            }
        }
        
        private void setForColorLbDocGrp(object sender)
        {
            lbDocAll.ForeColor = Color.Black;

            lbDocGrp1.ForeColor = Color.Black;            lbDocGrp2.ForeColor = Color.Black;            lbDocGrp3.ForeColor = Color.Black;
            lbDocGrp4.ForeColor = Color.Black;            lbDocGrp5.ForeColor = Color.Black;            lbDocGrp6.ForeColor = Color.Black;
            lbDocGrp7.ForeColor = Color.Black;            lbDocGrp8.ForeColor = Color.Black;            lbDocGrp9.ForeColor = Color.Black;
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
        
        
        private void initLoading()
        {
            lbLoading = new Label();            lbLoading.Font = fEdit5B;            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;            lbLoading.AutoSize = false;            lbLoading.Size = new Size(300, 60);
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
        
        private void initRptView()
        {
            
        }
        private void setControlApm(String apmyear, String apmno)
        {
            PatientT07 apm = new PatientT07();
            apm = bc.bcDB.pt07DB.selectAppointment(apmyear, apmno);
            txtApmTime.Value = apm.apm_time;
            txtPttApmDate.Value = apm.MNC_APP_DAT;
            txtApmNO.Value = apm.MNC_DOC_NO;
            txtApmDocYear.Value = apm.MNC_DOC_YR;
            txtApmDtr.Value = apm.MNC_DOT_CD;
            //txtPttApmDate.Value = apm.MNC_DOT_CD;
            //cboApmDept.Value =  apm.MNC_SECR_NO;
            bc.setC1Combo(cboApmDept, apm.MNC_SECR_NO);
            txtApmDsc.Value = apm.MNC_APP_DSC;
            txtApmList.Value = apm.MNC_APP_DSC;
            txtApmTel.Value = apm.MNC_APP_TEL;
            txtApmRemark.Value = apm.MNC_DOT_CD;
            lbApmDtrName.Text = bc.selectDoctorName(apm.MNC_DOT_CD);
            DataTable dt = new DataTable();
            dt = bc.bcDB.pt07DB.selectAppointmentOrder(txtApmDocYear.Text, txtApmNO.Text);
            grfApmOrder.Rows.Count = 1;grfApmOrder.Rows.Count = dt.Rows.Count + 1;int i = 0;
            foreach (DataRow item in dt.Rows)
            {
                i++;
                Row rowa = grfApmOrder.Rows[i];
                String flag = "", name="", code="";
                code = item["MNC_OPR_CD"].ToString();
                flag = item["MNC_OPR_FLAG"].ToString();
                if (flag.Equals("O"))
                {
                    String chk = "";
                    chk = bc.bcDB.pm30DB.SelectNameByPk(code);
                    name = chk;
                }
                else if (flag.Equals("L"))
                {
                    LabM01 lab = new LabM01();
                    lab = bc.bcDB.labM01DB.SelectByPk(code);
                    name = lab.MNC_LB_DSC;
                }
                else if (flag.Equals("X"))
                {
                    XrayM01 xray = new XrayM01();
                    xray = bc.bcDB.xrayM01DB.SelectByPk(code);
                    name = xray.MNC_XR_DSC;
                }
                rowa[colgrfOrderCode] = item["MNC_OPR_CD"].ToString();
                rowa[colgrfOrderName] = name;
                rowa[colgrfOrderStatus] = flag;
                rowa[colgrfOrderQty] = "1";
                rowa[0] = grfApmOrder.Rows.Count - 1;
            }
            pnApmOrder.Visible = (grfApmOrder.Rows.Count > 1) ? true : false;
        }
        
        
        

        private void setControlOper(String sendername)
        {
            if (pageLoad) return ;
            timeOperList.Enabled = false;
            bool ispid = false, isname = false, isNum = false, isLettersOnly=false;
            long chkpid = 0;
            lfSbMessage.Text = grfOperList.Row.ToString();
            picHisL.Image = null;            picHisR.Image = null;            picL.Image = null;            picR.Image = null;
            isNum = long.TryParse(HN, out chkpid);
            showLbLoading();
            lfSbStatus.Text = "";            lfSbMessage.Text = "";            lbOperCodeApprove.Visible = false;            txtOperCodeApprove.Visible = false;
            //clearControlTabOperVital();
            if (sendername.Equals(grfOperList.Name)){   ROWGrfOper = grfOperList.Row;   VS = bc.bcDB.vsDB.selectbyPreno(HN, VSDATE, PRENO); VN = VS.VN; }
            else if (sendername.Equals(txtOperHN.Name))
            {
                //แก้ให้ สามารถ ค้นหาโดยใช้ ชื่อ pid ได้   68-12-01
                String[] chkvndot = txtOperHN.Text.Trim().Split('.');
                string[] chkvn = txtOperHN.Text.Trim().Split('/');
                //if((chkvn.Length <= 1) && (chkvndot.Length > 1))  { chkvn = new string[2]; chkvn[0]=chkvn[0].Trim();   chkvn[1]= (chkvn.Length > 1) ? chkvn[1].Trim() : "";    }
                if ((chkvn.Length <= 1) && (chkvndot.Length > 1))   {   chkvn = chkvndot.Select(s => s?.Trim() ?? "").ToArray();    }
                if (chkvn.Length > 1)
                {       //ค้นหาจาก vn
                    VS = bc.bcDB.vsDB.selectByvn(chkvn[0], chkvn[1]);
                    PRENO = VS.preno;                    VSDATE = VS.VisitDate;                    HN = VS.HN;                    VN = VS.VN;
                    isNum = true;       //ให้ค่า isNum = true ไปเลยจะได้แสดงผล
                }
                else
                {
                    //long chkpid = 0;
                    //bool ispid = false;
                    isNum = long.TryParse(txtOperHN.Text.Trim(), out chkpid);
                    //if (isNum)                    {                        if (txtOperHN.Text.Length == 13) { ispid = true; }                    }
                    //else
                    //{
                    isLettersOnly = !string.IsNullOrWhiteSpace(txtOperHN.Text) && Regex.IsMatch(txtOperHN.Text.Trim(), @"^[\p{L}\s]+$");
                    //    if (isLettersOnly)                      {           isname = true;          }
                    //}
                    DataTable dtvs = bc.bcDB.vsDB.selectByvsdateAllVoid(HN);        // เอาที่ ยกเลิก มาแสดงด้วย
                    //ROWGrfOper = 0;
                    if (dtvs.Rows.Count == 1)
                    {
                        VS = bc.bcDB.vsDB.selectbyPreno(HN, dtvs.Rows[0]["MNC_PRE_NO"].ToString());
                        PRENO = VS.preno;
                        VSDATE = VS.VisitDate;
                    }
                    else if (dtvs.Rows.Count > 1)
                    {
                        Form frm = new Form();
                        Panel pn = new Panel();
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.Location = new Point(txtOperHN.Location.X + 10+ pnOperList.Width, txtOperHN.Location.Y + txtOperHN.Width+3);
                        frm.Size = new Size(600, 400);
                        pn.Dock = DockStyle.Fill;
                        frm.Controls.Add(pn);
                        C1FlexGrid grfOperVnCheck = new C1FlexGrid();
                        grfOperVnCheck.Font = fEdit;
                        grfOperVnCheck.Dock = System.Windows.Forms.DockStyle.Fill;
                        grfOperVnCheck.Location = new System.Drawing.Point(0, 0);
                        grfOperVnCheck.Rows.Count = 1;                        grfOperVnCheck.Cols.Count = 4;
                        grfOperVnCheck.Cols[0].Width = 60;                        grfOperVnCheck.Cols[1].Width = 80;                        grfOperVnCheck.Cols[2].Width = 400;
                        grfOperVnCheck.ShowCursor = true;
                        grfOperVnCheck.Cols[0].Caption = "";            grfOperVnCheck.Cols[1].Caption = "VN";          grfOperVnCheck.Cols[2].Caption = "อาการ";
                        grfOperVnCheck.Cols[3].Caption = "preno";
                        grfOperVnCheck.Cols[3].Visible = false;
                        grfOperVnCheck.Cols[0].AllowEditing = false;    grfOperVnCheck.Cols[1].AllowEditing = false;        grfOperVnCheck.Cols[2].AllowEditing = false;
                        grfOperVnCheck.Rows.Count = dtvs.Rows.Count + 1;
                        int i = 1;
                        foreach(DataRow arow in dtvs.Rows)
                        {
                            Row rowa = grfOperVnCheck.Rows[i];
                            rowa[0] = i;
                            rowa[1] = arow["MNC_VN_NO"].ToString()+"."+ arow["MNC_VN_SEQ"].ToString()+"."+ arow["MNC_VN_SUM"].ToString();
                            rowa[2] = arow["MNC_SHIF_MEMO"].ToString();
                            rowa[3] = arow["MNC_PRE_NO"].ToString();
                            i++;
                        }
                        pn.Controls.Add(grfOperVnCheck);
                        grfOperVnCheck.DoubleClick += (s, e) =>
                        {
                            if (grfOperVnCheck.Row <= 0) return;
                            if (grfOperVnCheck[grfOperVnCheck.Row, 3] == null) return;
                            PRENO = grfOperVnCheck[grfOperVnCheck.Row, 3].ToString();
                            VS = bc.bcDB.vsDB.selectbyPreno(HN, PRENO);
                            VN = VS.VN;
                            VSDATE = VS.VisitDate;
                            frm.Close();
                        };
                        frm.ShowDialog();
                        lfSbMessage.Text = "พบข้อมูล มีใบยามากกว่า 1 ใบ กรุณาป้อนเป็นเลขที่ใบยา";
                        isNum = true;
                        //hideLbLoading();
                    }
                    setGrfOperList("search");
                }
            }
            setLbLoading("โหลดข้อมูล 01");
            if (isNum)
            {
                tCOrder.SelectedTab = tabScan;          //        เริ่มต้นที่ tab scan  เพราะ ถ้าเริ่มที่ tab history จะโหลดข้อมูลช้า
                PTT = bc.bcDB.pttDB.selectPatinetByHn(HN);
                if (VS.HN.Length <= 0) { lfSbMessage.Text = "ไม่พบ visit";  clearControlTabOperVital(); hideLbLoading(); return; }
                setLbLoading("โหลดข้อมูล 01 vitalsign");
                setControlTabOperVital(VS);
                setLbLoading("โหลดข้อมูล 02 staffnote");
                getImgStaffNote();
                setLbLoading("โหลดข้อมูล 02-1");
                clearControlOrder();
                setLbLoading("โหลดข้อมูล 02-2");
                if (tCOrder.SelectedTab == tabApm) { setGrfPttApm(); }
                //setGrfPttApm();               //ไม่รู้ เป็นสาเหตุ ทำให้ช้า
                setLbLoading("โหลดข้อมูล 03 outlab");
                if (tCOrder.SelectedTab == tabHistory) { setGrfOPD(); }
                setGrfOutLab();
                if (tCOrder.SelectedTab == tabOrder) {  setLbLoading("โหลดข้อมูล 04 order"); setGrfOrderTemp(); setGrfOrderLab(); }
                setLbLoading("โหลดข้อมูล 05");
                txtOperLeftEar.Value = "";                txtOperRightEar.Value = "";                txtOperLeftEarOther.Value = "";
                txtOperRightEarOther.Value = "";                txtOperLeftEye.Value = "";                txtOperRightEye.Value = "";
                txtOperLeftEyePh.Value = "";                txtOperRightEyePh.Value = "";                cboOperEye.Value = "";
                if (VSDATE.Length > 0)
                {
                    DataTable dt011 = bc.bcDB.vsDB.selectT011(HN, VSDATE, PRENO);   //select by HN, VSDATE, PRENO
                    if (dt011.Rows.Count > 0)
                    {
                        txtOperLeftEar.Value = dt011.Rows[0]["left_ear"].ToString();
                        txtOperRightEar.Value = dt011.Rows[0]["right_ear"].ToString();
                        txtOperLeftEarOther.Value = dt011.Rows[0]["left_ear_other"].ToString();
                        txtOperRightEarOther.Value = dt011.Rows[0]["right_ear_other"].ToString();
                        chkOperRightEarNormal.Checked = dt011.Rows[0]["right_ear_normal"].ToString().Equals("1") ? true : false;
                        chkOperLeftEarNormal.Checked = dt011.Rows[0]["left_ear_normal"].ToString().Equals("1") ? true : false;
                        chkOperRightEarAbNormal.Checked = dt011.Rows[0]["right_ear_normal"].ToString().Equals("0") ? true : false;
                        chkOperLeftEarAbNormal.Checked = dt011.Rows[0]["left_ear_normal"].ToString().Equals("0") ? true : false;

                        txtOperLeftEye.Value = dt011.Rows[0]["left_eye"].ToString();
                        txtOperRightEye.Value = dt011.Rows[0]["right_eye"].ToString();
                        txtOperLeftEyePh.Value = dt011.Rows[0]["left_eye_ph"].ToString();
                        txtOperRightEyePh.Value = dt011.Rows[0]["right_eye_ph"].ToString();
                        bc.setC1ComboByName(cboOperEye, dt011.Rows[0]["eye_normal"].ToString());
                        bc.setC1ComboByName(cboOperLung, dt011.Rows[0]["lung_normal"].ToString());
                        txtOperLung.Value = dt011.Rows[0]["lung_value"].ToString();
                        bc.setC1ComboByNameIndexOf(cboOperRoom, VS.DoctorId);
                    }
                }
                //if (grfOrder != null) grfOrder.Rows.Count = 1;
                setLbLoading("โหลดข้อมูล 06");
                chkItemLab.Checked = true;
                ChkItemLab_Click(null, null);
                HNmedscan = HN;
                rb1.Text = VS.PatientName;
                txtSBSearchHN.Text = HNmedscan;
                setLbLoading("โหลดข้อมูล 07");
                //if (bc.iniC.statusbypasserror.Equals("1"))                {                    lbFindPaidSSO.Text = "";                }
                //else
                //    lbFindPaidSSO.Text = bc.checkPaidSSO(PTT.MNC_ID_NO, lbLoading);
                lbFindPaidSSO.Text = bc.checkPaidSSO(PTT.MNC_ID_NO, lbLoading);
            }
            else
            {

            }

            //txtOperTemp.Focus();
            lbLoading.Text = "กรุณารอซักครู่ ...";
            hideLbLoading();
            timeOperList.Enabled = true;
        }

        private void GrfOperVnCheck_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        
        
        private Visit setVisitVitalsign(String tabname)
        {
            Visit vs = new Visit();
            if (tabname.Equals(tabOper.Name))
            {
                vs.temp = txtOperTemp.Text;
                vs.ratios = txtOperHrate.Text;
                vs.breath = txtOperRrate.Text;
                vs.hrate = txtOperHrate.Text;
                vs.rrate = txtOperRh.Text;
                vs.bp1l = txtOperBp1L.Text;
                vs.bp1r = txtOperBp1R.Text;
                vs.bp2l = txtOperBp2L.Text;
                vs.bp2r = txtOperBp2R.Text;
                vs.abc = txtOperAbc.Text;
                vs.weight = txtOperWt.Text;
                vs.high = txtOperHt.Text;
                vs.hc = txtOperHc.Text;
                vs.cc = txtOperCc.Text;
                vs.ccin = txtOperCcin.Text;
                vs.ccex = txtOperCcex.Text;
            }
            else if (tabname.Equals(tabCheckUP.Name))
            {
                vs.temp = txtCheckUPTempu.Text;
                vs.ratios = txtOperHrate.Text;
                vs.breath = txtOperRrate.Text;
                //vs.abc = txtOperAbo.Text;
                vs.rrate = txtCheckUPRrate.Text;
                vs.bp1l = txtCheckUPBp1L.Text;
                vs.bp1r = "";
                vs.bp2l = "";
                vs.bp2r = "";
                vs.abc = txtOperAbc.Text;
                vs.weight = txtCheckUPWeight.Text;
                vs.high = txtCheckUPHeight.Text;
                vs.hc = "";
                vs.cc = "";
                vs.ccin = "";
                vs.ccex = "";
            }
            
            return vs;
        }
        
        private void setControlCheckUP(String hn, String vsdate, String preno)
        {
            if (hn == null) { return; }
            String hn1 = hn;
            var allowedChk = new HashSet<string> { "12", "15", "18" };
            DataTable dtchk = bc.bcDB.vsDB.selectVisitByHn7(hn);   //เพื่อ check 
            EDITDOE = chkCheckUpEdit.Checked;
            if (dtchk.Rows.Count <= 0)
            {
                if (chkCheckUpEdit.Checked)
                {   //ต้องการแก้ไข ข้อมูลเก่า
                    dtchk = bc.bcDB.vsDB.selectVisitByCertId1(hn);
                    if (dtchk.Rows.Count > 0) { hn1 = dtchk.Rows[0]["MNC_HN_NO"].ToString(); }
                    else { hn1 = ""; }
                }
                else { return; }
            }
            clearControlCheckUP();
            PTT = bc.bcDB.pttDB.selectPatinetByHn(hn1);
            //new LogWriter("d", "FrmOPD setControlCheckUP " + hn);
            if (PTT == null) { lfSbMessage.Text = "ไม่พบข้อมูล"; return; }
            if (PTT.MNC_HN_NO.Length <= 0) { lfSbMessage.Text = "ไม่พบข้อมูล"; return; }
            Visit visit = new Visit();            DataTable dtvs = new DataTable();
            VSDATE = vsdate;            PRENO = preno;            HN = hn;
            //new LogWriter("d", "FrmOPD setControlCheckUP 001");
            if ((chkCheckUPSelect.Checked && ((ComboBoxItem)cboCheckUPSelect.SelectedItem).Value.ToString().Equals("doeonline")) || (EDITDOE))
            {       //ให้แน่ใจว่า มาจาก ตรวจสุขภาพต่างด้าว
                if (EDITDOE)
                {//ต้องการแก้ไข ข้อมูลเก่า
                    dtvs = bc.bcDB.vsDB.selectByvsdateDOECertId(hn);
                }
                else
                {
                    dtvs = bc.bcDB.vsDB.selectByvsdateDOE(hn);
                }
                if (dtvs.Rows.Count > 0)
                {
                    VS = new Visit();
                    //new LogWriter("d", "FrmOPD setControlCheckUP 01");
                    VSDATE = dtvs.Rows[0]["MNC_DATE"].ToString();
                    PRENO = dtvs.Rows[0]["MNC_PRE_NO"].ToString();
                    STATUSQUICKORDER = dtvs.Rows[0]["status_quick_order"].ToString();
                    VS.VN = dtvs.Rows[0]["MNC_VN_NO"].ToString()+"."+ dtvs.Rows[0]["MNC_VN_SEQ"].ToString();
                    VN = VS.VN;
                    VS.VisitDate = VSDATE;
                    VS.preno = PRENO;
                    VS.HN = dtvs.Rows[0]["MNC_HN_NO"].ToString();
                    VS.skin_tone = dtvs.Rows[0]["skin_tone"].ToString();
                    bc.setC1Combo(cboCheckUPskintone, VS.skin_tone);
                    //new LogWriter("d", "FrmOPD setControlCheckUP 02");
                }
            }
            else
            {
                VN = dtchk.Rows[0]["mnc_vn_no"].ToString()+"/"+ dtchk.Rows[0]["mnc_vn_seq"].ToString()+"("+ dtchk.Rows[0]["mnc_vn_sum"].ToString()+")";
                VSDATE = dtchk.Rows[0]["mnc_date"].ToString();
                PRENO = dtchk.Rows[0]["mnc_pre_no"].ToString();
            }
            DateTime.TryParse(PTT.MNC_BDAY, out DateTime dob);
            if (dob.Year < 1900)            {                dob = dob.AddYears(543);            }
            
            txtCheckUPHN.Value = PTT.MNC_HN_NO;             txtCheckUPNameT.Value = PTT.MNC_FNAME_T;             txtCheckUPSurNameT.Value = PTT.MNC_LNAME_T;
            txtCheckUPNameE.Value = PTT.MNC_FNAME_E;        txtCheckUPSurNameE.Value = PTT.MNC_LNAME_E;         txtCheckUPMobile1.Value = PTT.MNC_CUR_TEL;
            txtCheckUPAge.Value = PTT.AgeStringOK1DOT();
            txtCheckUPDOB.Value = dob;
            bc.setC1Combo(cboCheckUPPrefixT, PTT.MNC_PFIX_CDT);
            bc.setC1Combo(cboCheckUPSex, PTT.MNC_SEX);
            bc.setC1Combo(cboCheckUPNat, PTT.MNC_NAT_CD);
            txtCheckUPABOGroup.Text = dtchk.Rows[0]["mnc_blo_grp"].ToString();
            //txtThaiAge.Text = dt.Rows[0]["MNC_AGE"].ToString();
            txtCheckUPRrate.Text = dtchk.Rows[0]["mnc_breath"].ToString();
            txtCheckUPHrate.Text = dtchk.Rows[0]["mnc_ratios"].ToString();

            txtCheckUPHeight.Text = dtchk.Rows[0]["mnc_high"].ToString();
            txtCheckUPBp1L.Text = dtchk.Rows[0]["mnc_bp1_l"].ToString();
            txtCheckUPRhgroup.Text = dtchk.Rows[0]["mnc_blo_rh"].ToString();
            //cboCheckUPSex.Text = dt.Rows[0]["mnc_sex"].ToString();

            txtCheckUPTempu.Text = dtchk.Rows[0]["mnc_temp"].ToString();
            txtCheckUPWeight.Text = dtchk.Rows[0]["mnc_weight"].ToString();
            txtCheckUPThaiSign1.Text = "อัตราการเต้นของหัวใจ " + txtCheckUPHrate.Text.Trim() + " ครั้ง/นาที " + "  อัตราการหายใจ " + txtCheckUPRrate.Text.Trim() + " ครั้ง/นาที " + " ความดันโลหิต " + txtCheckUPBp1L.Text + " มม.ปรอท";
            txtCheckUPThaiSign2.Text = "น้ำหนัก " + txtCheckUPWeight.Text.Trim() + " กก. " + "  ส่วนสูง " + txtCheckUPHeight.Text.Trim() + " ซม." ;

            txtCheckUPPttPID.Value = PTT.MNC_ID_NO;
            String moo = "",soi = "", road="", tambon="", amphur = "", prov = "", poc="";
            moo = PTT.MNC_CUR_MOO.Length > 0 ? "หมู่ "+ PTT.MNC_CUR_MOO : "";
            soi = PTT.MNC_CUR_SOI.Length > 0 ? " ซอย " + PTT.MNC_CUR_SOI : "";
            road = PTT.MNC_CUR_ROAD.Length > 0 ? " ถนน " + PTT.MNC_CUR_ROAD : "";
            tambon = PTT.MNC_CUR_TUM.Length > 0 ? bc.bcDB.pm07DB.getTumbonAmphurProvName(PTT.MNC_CUR_TUM) : "";
            
            poc = PTT.MNC_CUR_POC.Length > 0 ? " รหัสไปรษณีย์ " + PTT.MNC_CUR_POC : "";
            txtCheckUPAddr1.Text = PTT.MNC_CUR_ADD + " " + moo + soi + road + " " + tambon +" "+ poc;
            txtCheckUPAddr3.Text = PTT.MNC_CUR_ADD + " " + moo + soi + road + " " + tambon + " " + poc;//ที่อยู่ลูกจ้าง
            //คุยกับอิวปชส ให้เอา ที่อยู่ต่างด้าวกับที่อยู่นายจ้างเป็นที่อยู่เดียวกัน
            DataTable dtpackage = new DataTable();
            var code = (dtchk != null && dtchk.Rows.Count > 0)    ? Convert.ToString(dtchk.Rows[0]["MNC_FN_TYP_CD"]).Trim()    : string.Empty;
            //if (PTT.MNC_SEX.Equals("")) cboCheckUPSex.Text = "";
            String packagecode = "";        //ต้องหา package code ให้ได้ จะได้รู้ว่ารายการตรวจอะไรบ้าง
            if ((chkCheckUPSelect.Checked && ((ComboBoxItem)cboCheckUPSelect.SelectedItem).Value.ToString().Equals("doeonline")) || (EDITDOE))
            {
                txtCheckUPEmplyer.Text = PTT.remark1;
                txtCheckUPAddr1.Text = PTT.remark2;
                txtCheckUPAddr3.Text = PTT.remark2;//ที่อยู่ลูกจ้าง
                txtCheckUPComp.Text = PTT.comNameT;
                if (PTT.ref1.Length > 0) { txtCheckUPPttPID.Value = PTT.ref1; }
                if (PTT.MNC_ID_NO.Length<=1)                {                    txtCheckUPPttPID.Value = PTT.ref1;                }
                dtpackage = bc.bcDB.pm39DB.selectByCompcode(bc.iniC.compcodedoe, PTT.MNC_SEX);
                if (dtpackage.Rows.Count > 0)
                {
                    packagecode = dtpackage.Rows[0]["MNC_PAC_CD"].ToString();
                    setGrfChkPackItems(packagecode);
                    bc.setC1Combo(cboCheckUPOrder, packagecode);
                }
                //cboAlienPosition
                bc.setC1Combo(cboAlienPosition, PTT.doe_position);
            }
            else if(allowedChk.Contains(code))      //checkup
            {
                DataTable dtpm24 = new DataTable();
                dtpm24 = bc.bcDB.pm24DB.selectCustByCode(PTT.MNC_COM_CD2);
                if(dtpm24.Rows.Count>0)
                {
                    txtCheckUPComp.Text = dtpm24.Rows[0]["MNC_COM_CD"].ToString();
                    txtCheckUPEmplyer.Text = dtpm24.Rows[0]["MNC_COM_DSC"].ToString();
                    String addr = "";
                    addr = dtpm24.Rows[0]["MNC_COM_ADD"].ToString() ;
                    txtCheckUPAddr1.Text = addr;
                }
                dtpackage = bc.bcDB.pm39DB.selectByCompcode(dtchk.Rows[0]["MNC_RES_MAS"].ToString());       //บริษัท ใน patient_t01 ใช้ MNC_RES_MAS
                packagecode = dtpackage.Rows.Count>0 ? dtpackage.Rows[0]["MNC_PAC_CD"].ToString():"";
                setGrfChkPackItems(packagecode);
                bc.setC1Combo(cboCheckUPOrder, packagecode);
                if (dtpackage.Rows.Count > 0)
                {
                    txtCheckUPEmplyer.Text = dtpackage.Rows[0]["MNC_COM_DSC"].ToString();
                    txtCheckUPComp.Text = dtpackage.Rows[0]["MNC_COM_CD"].ToString();
                    txtCheckUPAddr1.Text = dtpackage.Rows[0]["MNC_COM_ADD"].ToString();
                }
            }
            txtCheckUPDoctorId.Text = dtchk.Rows[0]["doctor_id"].ToString();
            txtCheckUPDoctorName.Text = dtchk.Rows[0]["dtr_name"].ToString();
        }
        private void clearControlCheckUP()
        {
            txtCheckUPABOGroup.Text = "";            txtCheckUPAge.Text = "";            txtCheckUPABOGroup.Text = "";            txtCheckUPRrate.Text = "";
            txtCheckUPHrate.Text = "";            txtCheckUPHeight.Text = "";            txtCheckUPBp1L.Text = "";            txtCheckUPRhgroup.Text = "";
            cboCheckUPSex.Text = "";            txtCheckUPThaiHIV.Text = "";            cboCheckUPThaiDiag.Text = "";            txtCheckUPTempu.Text = "";
            txtCheckUPWeight.Text = "";            txtCheckUPThaiSign1.Text = "";            txtCheckUPThaiSign2.Text = "";            txtCheckUPPttPID.Value = "";
            txtCheckUPDoctorId.Text = "";            txtCheckUPDoctorName.Text = "";            chkCheckUpEdit.Checked = false;            txtCheckUPEmplyer.Text = "";
            txtCheckUPAddr1.Text = "";            txtCheckUPAddr2.Text = "";            txtCheckUPPhone.Text = "";            txtCheckUPComp.Text = "";
            grfChkPackItems.Rows.Count = 1;
            clearControlCheckupSSO();
        }
        private void setControlTabOperVital(Visit vs)
        {
            txtOperHN.Value = vs.HN;
            lbOperPttNameT.Text = vs.PatientName;
            lboperAge.Text = "อายุ "+PTT.AgeStringOK1();
            lboperVN.Text = "VN "+ vs.VN;            txtOperTemp.Value = vs.temp;            txtOperHrate.Value = vs.ratios;
            txtOperRrate.Value = vs.breath;            //txtOperAbo.Value = vs.temp;
            txtOperRh.Value = "";            txtOperBp1L.Value = vs.bp1l;            txtOperBp1R.Value = vs.bp1r;
            txtOperBp2L.Value = vs.bp2l;            txtOperBp2R.Value = vs.bp2r;            txtOperAbc.Value = vs.abc;
            txtOperWt.Value = vs.weight;            txtOperHt.Value = vs.high;            txtOperHc.Value = vs.hc;
            txtOperCc.Value = vs.cc;            txtOperCcin.Value = vs.ccin;            txtOperCcex.Value = vs.ccex;
            txtOperDtr.Value = vs.DoctorId;
            lbOperDtrName.Text = vs.DoctorName;
            lbSymptoms.Text = vs.symptom.Replace("\r\n",",");
            lbSymptoms.Text = lbSymptoms.Text.Replace(",,,", "");
            lbSymptoms.Text = lbSymptoms.Text.Replace(",,", "");
            txtOperAbo.Value = "";
            txtOperBmi.Value = bc.calBMI(vs.weight, vs.high);
        }
        private void clearControlTabOperVital()
        {
            txtOperHN.Value = "";            lbOperPttNameT.Text = "";            txtOperTemp.Value = "";            txtOperHrate.Value = "";
            txtOperRrate.Value = "";            txtOperAbo.Value = "";            txtOperRh.Value = "";            txtOperBp1L.Value = "";
            txtOperBp1R.Value = "";            txtOperBp2L.Value = "";            txtOperBp2R.Value = "";            txtOperAbc.Value = "";
            txtOperWt.Value = "";            txtOperHt.Value = "";            txtOperHc.Value = "";            txtOperCc.Value = "";
            txtOperCcin.Value = "";            txtOperCcex.Value = "";            txtOperDtr.Value = "";            lbOperDtrName.Text = "";
            lbSymptoms.Text = "";            txtOperBmi.Value = "";            lboperAge.Text = "";            lboperVN.Text = "";
        }
        private PatientT07 getApm()
        {
            if (VSDATE.Length <= 0) return null;            if (PRENO.Length <= 0) return null;            if (HN.Length <= 0) return null;
            if (VS == null) return null;
            //String deptcode = cboApmDept.Text;
            //String stationname = bc.bcDB.pttDB.selectDeptOPD(deptcode);
            DateTime.TryParse(txtPttApmDate.Text, out DateTime apmdate);
            if (apmdate == null) return null;
            if (apmdate.Year < 1900) apmdate = apmdate.AddYears(543);
            PatientT07 apm = new PatientT07();
            apm.MNC_HN_NO = txtOperHN.Text.Trim();
            apm.MNC_HN_YR = VS.MNC_HN_YR;
            apm.MNC_DATE = VSDATE;
            apm.MNC_PRE_NO = PRENO;
            apm.MNC_DOC_YR = txtApmNO.Text.Trim().Length==0?(DateTime.Now.Year+543).ToString(): txtApmDocYear.Text.Trim();
            apm.MNC_DOC_NO = txtApmNO.Text.Trim();
            apm.MNC_TIME = VS.VisitTime;
            apm.MNC_APP_DAT = apmdate.Year+"-"+ apmdate.ToString("MM-dd");
            apm.MNC_APP_TIM = bc.bcDB.pt07DB.setAppTime(txtApmTime.Text);
            //queno := RunNumByDate_notrn('OPD','QAP','Que Appoint',formatdatetime('eeee',DtpApp.Date),APP_DAT,Datamodule1.Rquery2)
            apm.MNC_APP_NO = txtApmNO.Text.Trim();              //เลขที่นัดหมาย patient_t07.mnc_doc_no
            apm.MNC_APP_DSC = txtApmList.Text.Length > 0 ? txtApmList.Text.Trim():txtApmDsc.Text.Trim();
            apm.MNC_APP_STS = "";
            apm.MNC_APP_TEL = txtApmTel.Text;
            apm.MNC_DOT_CD = txtApmDtr.Text.Trim();
            apm.apm_time = txtApmTime.Text.Trim();
            //Test ที่บางนา1 ดูแล้ว ลงสลับกัน เลยแก้ไขใหม่
            //apm.MNC_SEC_NO = bc.iniC.station;                   //แผนกที่นัด
            //apm.MNC_DEP_NO = DEPTNO;
            //apm.MNC_SECR_NO = cboApmDept.SelectedItem == null ? "" : ((ComboBoxItem)cboApmDept.SelectedItem).Value; //นัดหมายที่แผนกใด
            //apm.MNC_DEPR_NO = bc.bcDB.pm32DB.getDeptNoOPD(apm.MNC_SECR_NO); //นัดหมายที่แผนกใด
            String[] stationarr = bc.iniC.station.Split(',');
            //แผนกที่นัด
            if (stationarr.Length>1)            {                apm.MNC_SEC_NO = stationarr[0].Trim();             }
            else            {                apm.MNC_SEC_NO = bc.iniC.station.Trim();                               }
            //apm.MNC_SECR_NO = bc.iniC.station;                   //แผนกที่ทำนัด
            apm.MNC_DEPR_NO = DEPTNO;
            apm.MNC_SEC_NO = cboApmDept.SelectedItem == null ? "" : ((ComboBoxItem)cboApmDept.SelectedItem).Value; //นัดหมายที่แผนกใด
            apm.MNC_DEP_NO = bc.bcDB.pm32DB.getDeptNoOPD(apm.MNC_SECR_NO); //นัดหมายที่แผนกใด

            apm.MNC_NAME = cboApmDept.Text;
            apm.MNC_EMPR_CD = bc.USERCONFIRMID;
            apm.MNC_STS = "3";
            apm.MNC_APP_ADM_FLG = chkApmAdmit.Checked ? "Y":"";
            apm.MNC_APP_OR_FLG = chkApmOR.Checked ? "Y":"";
            String time2 = "";
            String[] time1 = apm.apm_time.Split('-');
            if(time1.Length > 1)            {                time2 = time1[1].Trim().Replace(":", "");                apm.MNC_APP_TIM_E = time1[1];            }
            else            {                time2 = apm.apm_time.Trim().Replace(":", "");            }
            int time3 = 0;
            int.TryParse(time2.Trim().Replace(":", ""), out time3);
            apm.MNC_APP_TIM_E = time3.ToString();
            //apm.MNC_SEC_NO = cboApmDept.SelectedItem == null ? "" : ((ComboBoxItem)cboApmDept.SelectedItem).Value;
            return apm;
        }
        private void clearControlOrder()
        {
            txtItemCode.Value = "";            txtSearchItem.Value = "";            lbItemName.Text = "";            txtItemRemark.Value = "";
        }
        private void clearControlTabApm(Boolean new1)
        {
            txtPttApmDate.Value = DateTime.Now;            txtApmTime.Value = "";            bc.setC1Combo(cboApmDept, "");
            txtApmDsc.Value = "";            txtApmRemark.Value = "";            txtApmDtr.Value = "";
            lbApmDtrName.Text = "";            txtApmTel.Value = "";            txtApmNO.Value = "";
            txtApmDocYear.Value = "";            txtApmList.Value = "";            grfApmOrder.Rows.Count = 1;
            if (!new1)            {                grfPttApm.Rows.Count = 1;            }
        }
        private void setHSLABM01()
        {
            if (HSLABM01.Count <= 0)
            {
                DataTable dt = bc.bcDB.labM01DB.SelectAllSearch();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        LabM01 lab = new LabM01();
                        lab.MNC_LB_CD = row["MNC_LB_CD"].ToString().Trim();
                        lab.MNC_LB_DSC = row["MNC_LB_DSC"].ToString().Trim();
                        lab.control_year = row["control_year"].ToString().Trim();
                        lab.control_supervisor = row["control_supervisor"].ToString().Trim();
                        lab.control_paid_code = row["control_paid_code"].ToString().Trim();
                        lab.control_remark = row["control_remark"].ToString().Trim();
                        lab.status_control = row["status_control"].ToString().Trim();
                        HSLABM01.Add(lab);
                        if (lab.status_control.Equals("1"))
                        {
                            HSLABM01C.Add(lab);
                        }
                    }
                }
            }
        }
        private void setHSXRAYM01()
        {
            if (HSXRAYM01.Count <= 0)
            {
                DataTable dt = bc.bcDB.xrayM01DB.SelectAll();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        XrayM01 xray = new XrayM01();
                        xray.MNC_XR_CD = row["MNC_XR_CD"]?.ToString() ?? "".Trim();
                        xray.MNC_XR_DSC = row["MNC_XR_DSC"]?.ToString() ?? "".Trim();
                        xray.control_supervisor = row["control_supervisor"]?.ToString() ?? "".Trim();
                        xray.control_paid_code = row["control_paid_code"]?.ToString() ?? "".Trim();
                        xray.control_remark = row["control_remark"]?.ToString() ?? "".Trim();
                        xray.status_control = row["status_control"]?.ToString()??"".Trim();
                        HSXRAYM01.Add(xray);
                        if (xray.status_control.Equals("1"))
                        {
                            HSXRAYM01C.Add(xray);
                        }
                    }
                }
            }
        }
        private void setHSPROCEDUREM01()
        {
            if (HSPROCEDUREM01.Count <= 0)
            {
                DataTable dt = bc.bcDB.pm30DB.SelectAll();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        PatientM30 xray = new PatientM30();
                        xray.mnc_sr_cd = row["MNC_SR_CD"]?.ToString() ?? "".Trim();
                        xray.mnc_sr_dsc = row["MNC_SR_DSC"]?.ToString() ?? "".Trim();
                        xray.control_supervisor = row["control_supervisor"]?.ToString() ?? "".Trim();
                        xray.control_paid_code = row["control_paid_code"]?.ToString() ?? "".Trim();
                        xray.control_remark = row["control_remark"]?.ToString() ?? "".Trim();
                        xray.status_control = row["status_control"]?.ToString() ?? "".Trim();
                        HSPROCEDUREM01.Add(xray);
                        if (xray.status_control.Equals("1"))                        {                            HSPROCEDUREM01C.Add(xray);                        }
                    }
                }
            }
        }
        private void addItemLAB(String itemcode, object sender)
        {
            //String name = "", code = "";
            setHSLABM01();
            //String[] txt = itemname.Split('#');
            //var labItem1 = HSLABM01.FirstOrDefault(d => d.MNC_LB_CD == txtSearchItem.Text.Trim().ToUpper());
            var labItem = HSLABM01.FirstOrDefault(d => d.MNC_LB_CD.Equals(itemcode.Trim().ToUpper(), StringComparison.OrdinalIgnoreCase));
            if (labItem == null) { lfSbMessage.Text = "no item"; return; }
            if (labItem.MNC_LB_CD != null && labItem.MNC_LB_CD.Length > 0)            {                LAB = labItem;            }
            else
            {
                lfSbMessage.Text = "no item";                    txtItemCode.Value = "";                    lbItemName.Text = "";                    txtItemQTY.Value = "1";
                return;
            }
            //lab = bc.bcDB.labM01DB.SelectByPk(code);
            txtItemCode.Value = LAB.MNC_LB_CD;            lbItemName.Text = LAB.MNC_LB_DSC;            txtItemQTY.Visible = false;            txtItemQTY.Value = "1";
            if (LAB.status_control.Equals("1"))
            {
                if (LAB.control_paid_code.Split(',').Contains("'" + VS.PaidCode + "'"))
                {
                    List<String> txt = new List<String>();
                    txt.Add("lab ถูกควบคุมการสั่ง เนื่องจากเป็นสิทธิ์ " + bc.bcDB.finM02DB.getPaidName(VS.PaidCode) + " ตามที่กำหนด ");
                    //lstAutoComplete.Items.Add("lab นี้ถูกจำกัดสิทธิ์ " + bc.bcDB.finM02DB.getPaidName(VS.PaidCode) + " ตามที่กำหนด ");
                    if (LAB.control_remark.Length > 0)
                        txt.Add(LAB.control_remark);
                        //lstAutoComplete.Items.Add(LAB.control_remark);
                    //lfSbMessage.Text = "lab นี้ไม่สามารถสั่งได้ เนื่องจากเป็นสิทธิ์ " + bc.bcDB.pm08DB.getPaidDscByCode(VS.PaidCode) + " ตามที่กำหนด ";
                    //return;
                    DateTime dtLab1, dtLab2;                    dtLab2 = DateTime.Now;                    dtLab1 = new DateTime(2026, 1, 1);
                    String date1 = dtLab1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                    String date2 = dtLab2.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                    DataTable dtlabcon = bc.bcDB.labT05DB.selecControltbyHn(date1, date2, txtOperHN.Text.Trim(), LAB.MNC_LB_CD);

                    //lstAutoComplete.Items.Add(LAB.MNC_LB_DSC + " สามารถสั่งได้ " + LAB.control_year + " ครั้งต่อปี ");
                    txt.Add(LAB.MNC_LB_DSC + " สามารถสั่งได้ " + LAB.control_year + " ครั้งต่อปี ");
                    if (dtlabcon.Rows.Count > 0)
                    {
                        foreach (DataRow arow in dtlabcon.Rows)
                        {
                            txt.Add(" สั่งไปแล้ววันที่ " + bc.datetoShow(arow["req_date"]?.ToString()??"") + " เลขที่ " + arow["req_no"].ToString());
                            //lstAutoComplete.Items.Add(" สั่งไปแล้ววันที่ " + bc.datetoShow(arow["req_date"].ToString()) + " เลขที่ " + arow["req_no"].ToString());
                        }
                        if (LAB.control_supervisor.Equals("1") && dtlabcon.Rows.Count >= Convert.ToInt32(LAB.control_year))//1=ขออนุมัติ
                        {
                            lbOperCodeApprove.Visible = true;               txtOperCodeApprove.Visible = true;              txtOperCodeApprove.Focus();
                        }
                        else if (LAB.control_supervisor.Equals("2"))//2=เฉพาะแพทย์GI
                        {
                            //เวชปฎิบัติทั่วไป (OPD3)        2=เฉพาะแพทย์GI
                            PatientM26 dtr = bc.bcDB.pm26DB.selectByPk(txtOperDtr.Text.Trim());
                            String sec = "";
                            sec = bc.bcDB.pm32DB.getDeptNoOPD(dtr.MNC_SEC_NO);
                            if (sec.IndexOf("เวชปฎิบัติทั่วไป") >= 0){lfSbMessage.Text = "lab นี้ไม่สามารถสั่งได้ เนื่องจากเป็นสิทธิ์ แพทย์ GI ตามที่กำหนด ";       return; }
                        }
                    }
                    else { txt.Add(" ยังไม่เคยสั่ง " + LAB.MNC_LB_DSC + "☕️☕️☕️"); }
                    lstAutoComplete.Items.AddRange(txt.ToArray());
                    LAB.control_remark = string.Join("\n", txt);
                }
            }
            if((sender != null) && (sender.GetType() == typeof(Label)) && (((Label)sender).Name == lbOperItem.Name))    {       setGrfOrderItem();  }
        }
        private void addItemXRAY(String itemcode, object sender)
        {
            String name = "", code = "";
            setHSXRAYM01();
            var xrayItem = HSXRAYM01.FirstOrDefault(d => d.MNC_XR_CD.Equals(itemcode.Trim().ToUpper(), StringComparison.OrdinalIgnoreCase));
            if (xrayItem == null) { lfSbMessage.Text = "no item"; return; }
            if (xrayItem.MNC_XR_CD != null && xrayItem.MNC_XR_CD.Length > 0)
            {
                XRAY = xrayItem;
            }
            else
            {
                lfSbMessage.Text = "no item"; txtItemCode.Value = ""; lbItemName.Text = ""; txtItemQTY.Value = "1";
                return;
            }
            //XrayM01 xray = new XrayM01();
            //xray = bc.bcDB.xrayM01DB.SelectByPk(itemcode);
            txtItemCode.Value = XRAY.MNC_XR_CD;
            lbItemName.Text = XRAY.MNC_XR_DSC;
            txtItemQTY.Visible = false;
            txtItemQTY.Value = "1";
            if ((sender != null) && (sender.GetType() == typeof(Label)) && (((Label)sender).Name == lbOperItem.Name))   {       setGrfOrderItem();  }
        }
        private void addItemProcedure(String itemcode, object sender)
        {
            setHSPROCEDUREM01();
            var labItem = HSPROCEDUREM01.FirstOrDefault(d => d.mnc_sr_cd.Equals(itemcode.Trim().ToUpper(), StringComparison.OrdinalIgnoreCase));
            if (labItem == null) { lfSbMessage.Text = "no item"; return; }
            if (labItem.mnc_sr_cd != null && labItem.mnc_sr_cd.Length > 0)      {                PROCEDURE = labItem;            }
            else
            {
                lfSbMessage.Text = "no item"; txtItemCode.Value = ""; lbItemName.Text = ""; txtItemQTY.Value = "1";
                return;
            }
            //String name = "", code = "";
            //PatientM30 pm30 = new PatientM30();
            //String name1 = bc.bcDB.pm30DB.SelectNameByPk(code);
            txtItemCode.Value = PROCEDURE.mnc_sr_cd;
            lbItemName.Text = PROCEDURE.mnc_sr_dsc;
            txtItemQTY.Visible = false;
            txtItemQTY.Value = "1";
            if ((sender != null) && (sender.GetType() == typeof(Label)) && (((Label)sender).Name == lbOperItem.Name))   {       setGrfOrderItem();  }
        }   
        private void addItemDRUG()
        {
            String name = "", code = "";
            PharmacyM01 drug = new PharmacyM01();
            String name1 = bc.bcDB.pharM01DB.SelectNameByPk(code);
            txtItemCode.Value = code;            lbItemName.Text = name1;            txtItemQTY.Visible = false;            txtItemQTY.Value = "1";
        }
        private void checkDupItem(List<Item> items)
        {
            Boolean dup = false;
            List<Item> temp = new List<Item>();
            foreach (Item item in items)
            {
                foreach(Row arow in grfOrder.Rows)
                {
                    if (arow[colgrfOrderCode].ToString().Equals(item.code, StringComparison.OrdinalIgnoreCase))
                    {
                        temp.Add(item);
                    }
                }
            }
            if(temp.Count > 0)
            {
                foreach(Item item in temp)
                {
                    items.Remove(item);
                }
            }
        }
        private void setOrderItem(List<Item> items, object sender)
        {
            String name = "", code = "";
            lstAutoComplete.Items.Clear();
            lbOperCodeApprove.Visible = false;
            txtOperCodeApprove.Visible = false;
            checkDupItem(items);
            if (chkItemLab.Checked)
            {
                foreach (Item item in items)                {                    addItemLAB(item.code, sender);                }
            }
            else if (chkItemXray.Checked)
            {
                foreach (Item item in items) { addItemXRAY(item.code, sender); }
            }
            else if (chkItemProcedure.Checked)
            {
                foreach (Item item in items) { addItemProcedure(item.code, sender); }
            }
            else if (chkItemDrug.Checked)
            {
                addItemDRUG();
            }
            txtItemCode.Value = "";
            lbItemName.Text = "";
        }
        private void setHeaderHeight0()
        {
            spOper.HeaderHeight = 0;            spCheckUP.HeaderHeight = 0;            spScan.HeaderHeight = 0;            scApm.HeaderHeight = 0;
            spOrder.HeaderHeight = 0;            spOperList.SizeRatio = 25;            spCheckUpList.SizeRatio = 25;            spMedScan.HeaderHeight = 0;
            spHistory.HeaderHeight = 0;            spHistoryVS.SizeRatio = 25;            spOPDImgL.SizeRatio = 45;            c1SplitContainer1.HeaderHeight = 0;
            spOutLab.HeaderHeight = 0;            spOutLabList.SizeRatio = 25;            spSearch.HeaderHeight = 0;            spSrcStaffNote.HeaderHeight = 0;
            spTodayOutLab.HeaderHeight = 0;            spTodayOutLabList.SizeRatio = 50;            spOutLabList.SizeRatio = 50;            spRpt.HeaderHeight = 0;
            spStaffNote.HeaderHeight = 0;            spStaffNoteLeft.SizeRatio = 40;            scFinish.HeaderHeight = 0;
        }
        private void FrmOPD_Load(object sender, EventArgs e)
        {
            System.Drawing.Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            setHeaderHeight0();
            txtApmTime.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtApmTime.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtApmTime.AutoCompleteCustomSource = acmApmTime;

            //txtCheckUPThaiDiag.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //txtCheckUPThaiDiag.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txtCheckUPThaiDiag.AutoCompleteCustomSource = autoCHECKUPDIAG;
            cboOperCritiria.SelectedIndex = 1;
            //setGrfOperList("");
            btnScanClearImg.Visible = false;            btnScanGetImg.Visible = false;            btnScanSaveImg.Visible = false;
            
            DEPTNO = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
            STATIONNAME = bc.bcDB.pm32DB.getDeptName(bc.iniC.station);
            
            txtApmDsc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtApmDsc.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtApmDsc.AutoCompleteCustomSource = autoApm;
            
            autoLab = bc.bcDB.labM01DB.getlLabAll();
            autoXray = bc.bcDB.xrayM01DB.getlLabAll();
            autoProcedure = bc.bcDB.pm30DB.getlProcedureAll();
            autoDrug = bc.bcDB.pharM01DB.setAUTODrug();
            ACMDTR = bc.bcDB.pm26DB.setAUTODTR();
            chkItemLab.Checked = true;
            ChkItemLab_Click(null, null);

            lfSbStation.Text = DEPTNO+"[" +bc.iniC.station+"]"+ STATIONNAME;
            rgSbModule.Text = bc.iniC.hostDBMainHIS + " " + bc.iniC.nameDBMainHIS;
            this.Text = "Last Update 2026-02-19 search staffnote";
            QUEDEPT = STATIONNAME;
            lfSbMessage.Text = "";
            //btnPrnStaffNote.Left = pnVitalSign.Width - btnPrnCertMed.Width - 10;
            btnPrnStaffNote.Left = txtOperRemark.Width + 105;
            btnPrnStaffNote.Top = txtOperRemark.Top - 35;
            btnOperPrnQue.Left = btnPrnStaffNote.Left + btnPrnStaffNote.Width +2;
            btnOperPrnQue.Top = btnPrnStaffNote.Top + 8;
            btnPrnCertMed.Left = btnOperPrnQue.Location.X+ btnOperPrnQue.Width+5;
            btnPrnCertMed.Top = btnPrnStaffNote.Top ;
            btnOperPrnSticker.Left = pnVitalSign.Width - btnOperPrnSticker.Width - 10;
            btnOperPrnSticker.Top = btnOperPrnSticker.Top + 10;

            txtOperSticker.Value = 4;
            txtOperSticker.Left = btnOperPrnSticker.Left - txtOperSticker.Width - 10;
            txtOperSticker.Top = btnOperPrnSticker.Top + 3;
            btnOperOpenSticker.Left = txtOperSticker.Left - btnOperOpenSticker.Width - 10;
            btnOperOpenSticker.Top = btnOperPrnSticker.Top;

            txtApmDtr.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtApmDtr.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtApmDtr.AutoCompleteCustomSource = ACMDTR;
            pnInformation.Width = tabOrder.Width - txtOperCodeApprove.Left - txtOperCodeApprove.Width - 15;
            pnInformation.Left = txtOperCodeApprove.Left + txtOperCodeApprove.Width + 10;
            txtCheckUPDate.Value = DateTime.Now;
            timeOperList.Enabled = true;
            lbSrcPreno.Value = "";            lbSrcAge.Value = "";            lbSrcVsDate.Value = "";            lbSrcVN.Value = "";
            lvSrcPttName.Value = "";            lbSrcHN.Value = "";            lbPttFinNote.Text = "";            lbDrugAllergy.Value = "";
            lbChronic.Value = "";            lboperVN.Text = "";
            
            tCOrder.SelectedTab = tabScan;          //        เริ่มต้นที่ tab scan  เพราะ ถ้าเริ่มที่ tab history จะโหลดข้อมูลช้า
            timeOperList.Start();
        }
    }
}