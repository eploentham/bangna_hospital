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
        System.Drawing.Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt2, famt2B, famt4B, famt2BL, famt5, famt5B, famt5BL, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2;
        Font fStaffN, fStaffNs, fStaffNB;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Patient PTT;
        Visit VS;
        PatientT07 APM;
        C1ThemeController theme1;
        C1FlexGrid grfOperList, grfOperFinish, grfOperFinishDrug, grfOperFinishLab, grfOperFinishXray, grfOperFinishProcedure, grfCheckUPList, grfPttApm, grfOrder, grfIPD, grfIPDScan, grfOPD, grfOutLab, grfHisOrder, grfLab, grfXray, grfHisProcedure, grfSrc, grfSrcVs, grfSrcOrder, grfSrcLab, grfSrcXray, grfSrcProcedure, grfTodayOutLab, grfApmOrder;
        C1FlexGrid grfApm, grfRpt, grfChkPackItems, grfOrderPreno, grfEKG, grfDocOLD, grfEST, grfHolter, grfECHO, grfCertMed, grfApmMulti;
        C1FlexGrid grfMapPackage, grfPackage, grfMapPackageViewhelp, grfpackageitems, grfOperLab, grfOperXray, grfSearchLab;
        C1FlexReport rptView;
        Boolean pageLoad = false, tabMedScanActiveNOtabOutLabActive=true, CHKLBAPMLISTCLICK=false, CHKLBAPMREMLISTClICK=false, CHKLBAPMLISTCLICK1 = false, CHKLBAPMREMLISTClICK1 = false;
        Image imgCorr, imgTran, resizedImage, IMG, IMGSTAFFNOTE;
        Timer timeOperList;
        String PRENO = "", VSDATE = "", HN="", VN="", DEPTNO="", HNmedscan="", DOCGRPID = "", DSCID = "", OUTLAB="", TC1Active="", TCFinishActive="", STATIONNAME="", STATUSQUICKORDER="";
        String TEMPLATESTAFFNOTE = "", SYMPTOMS="",QUEDEPT="", QUENO="", QUEFullname="", QUEHN="", QUESymptoms="";
        Stream streamPrint, streamPrintL, streamPrintR, streamDownload;
        
        Form frmImg;
        C1PictureBox pic;
        C1FlexViewer fvCerti, fvTodayOutLab;
        C1BarCode qrcode;
        DataTable DTRPT, DTALLERGY, DTCHRONIC, DTLABGRPSEARCH, DTLABSEARCH, DTXRAYGRPSEARCH, DTXRAYSEARCH, DTPROCEDUREGRPSEARCH, DTPROCEDURESEARCH, DTDRUGGRPSEARCH, DTDRUGSEARCH;
        
        int originalHeight = 0, newHeight = 720, mouseWheel = 0;
        int colgrfSrcHn = 1, colgrfSrcFullNameT = 2, colgrfSrcPID = 3, colgrfSrcDOB = 4, colgrfSrcPttid = 5, colgrfSrcAge = 6, colgrfSrcVisitReleaseOPD = 7, colgrfSrcVisitReleaseIPD = 8, colgrfSrcVisitReleaseIPDDischarge = 9;
        int colgrfPttApmVsDate = 1, colgrfPttApmApmDateShow = 2, colgrfPttApmApmTime = 3, colgrfPttApmHN = 4, colgrfPttApmPttName = 5, colgrfPttApmDeptR = 6, colgrfPttApmDeptMake = 7, colgrfPttApmNote = 8, colgrfPttApmOrder = 9, colgrfPttApmDocYear = 10, colgrfPttApmDocNo = 11, colgrfPttApmDtrname = 12, colgrfPttApmPhone = 13, colgrfPttApmPaidName = 14, colgrfPttApmRemarkCall = 15, colgrfPttApmStatusRemarkCall = 16, colgrfPttApmRemarkCallDate = 17, colgrfPttApmApmDate1 = 18;
        int colgrfOrderCode = 1, colgrfOrderName = 2, colgrfOrderStatus=3, colgrfOrderQty=4, colgrfOrderID=5, colgrfOrderReqNO=6, colgrfOrdFlagSave=7, colgrfOrdStatusControl = 8, colgrfOrdControlYear = 9, colgrfOrdSupervisor = 10, colgrfOrdPassSupervisor = 11, colgrfOrdControlRemark = 12;
        int colgrfCheckUPHn = 1, colgrfCheckUPFullNameT = 2, colgrfCheckUPSymtom = 3, colgrfCheckUPEmployer = 4, colgrfCheckUPVsDate = 5, colgrfCheckUPPreno = 6;
        int colgrfOperListFullNameT = 1, colgrfOperListSymptoms = 2, colgrfOperListPaidName = 3, colgrfOperListPreno = 4, colgrfOperListVsDate = 5, colgrfOperListVsTime = 6,colgrfOperListHn = 7, colgrfOperListVN = 8, colgrfOperListActNo = 9, colgrfOperListDtrName=10, colgrfOperListLab=11, colgrfOperListXray=12;
        int colIPDDate = 1, colIPDDept = 2, colIPDAnShow = 4, colIPDStatus = 3, colIPDPreno = 5, colIPDVn = 6, colIPDAndate = 7, colIPDAnYr = 8, colIPDAn = 9, colIPDDtrName = 10;
        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
        int colVsVsDate = 1, colVsDept = 2, colVsVn = 3, colVsStatus = 4, colVsPreno = 5, colVsAn = 6, colVsAndate = 7, colVsPaidType = 8, colVsHigh = 9, colVsWeight = 10, colVsTemp = 11, colVscc = 12, colVsccin = 13, colVsccex = 14, colVsabc = 15, colVshc16 = 16, colVsbp1r = 17, colVsbp1l = 18, colVsbp2r = 19, colVsbp2l = 20, colVsVital = 21, colVsPres = 22, colVsRadios = 23, colVsBreath = 24, colVsVsDate1 = 25, colVsDtrName = 26;
        int colgrfOutLabDscHN = 1, colgrfOutLabDscPttName = 2, colgrfOutLabDscVsDate = 3, colgrfOutLabDscVN = 4, colgrfOutLabDscId = 5, colgrfOutLablabcode = 6, colgrfOutLablabname = 7, colgrfOutLabApmDate = 8, colgrfOutLabApmDesc = 9, colgrfOutLabApmDtr=10, colgrfOutLabApmReqNo=11, colgrfOutLabApmReqDate=12;
        int colOrderId = 1, colOrderDate = 2, colOrderName = 3, colOrderQty = 4, colOrderFre = 5, colOrderIn1 = 6, colOrderMed = 7;
        int colLabDate = 1, colLabName = 2, colLabNameSub = 3, colLabResult = 4, colInterpret = 5, colNormal = 6, colUnit = 7, colLabCode=8, colLabReqNo=9, colLabReqDate=10;
        int colXrayDate = 1, colXrayCode = 2, colXrayName = 3, colXrayResult = 4;
        int colHisProcCode = 1, colHisProcName = 2, colHisProcReqDate=3, colHisProcReqTime=4;
        int colChkPackItemsname=1, colChkPackItemflag = 2, colChkPackItemsitemcode = 3, colChkPackItemsPackcode=4;
        int colgrfMapPackageCompCode = 1, colgrfMapPackageCompName = 2;
        int colgrfPackageCode = 1, colgrfPackageName = 2, colgrfPackagePrice = 3, colgrfPackageType = 4, colgrfPackageCompCode = 5;
        int colgrfViewhelpPackCode = 1, colgrfViewhelpPackName = 2, colgrfViewhelpPttName = 3, colgrfViewhelpVsdate = 4, colgrfViewhelpPackType = 5;
        int colgrfpackageitemcode=1, colgrfpackageitemname=2, colgrfpackageitemprice=3, colgrfpackageitemtype=4;
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
        Boolean EDITDOE = false, TEMPCANCER=false;
        ListBox lstAutoComplete;
        List<String> APMREM = new List<String>();
        HashSet<LabM01> HSLABM01 = new HashSet<LabM01>();
        HashSet<LabM01> HSLABM01C = new HashSet<LabM01>();
        HashSet<XrayM01> HSXRAYM01 = new HashSet<XrayM01>();
        HashSet<XrayM01> HSXRAYM01C = new HashSet<XrayM01>();
        HashSet<XrayM01> HSPROCEDUREM01 = new HashSet<XrayM01>();
        HashSet<XrayM01> HSPROCEDUREM01C = new HashSet<XrayM01>();
        LabM01 LAB = new LabM01();
        XrayM01 XRAY = new XrayM01();
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
            bc.bcDB.pm02DB.setCboPrefixT(cboCheckUPPrefixT, "");
            bc.bcDB.pm02DB.setCboPrefixE(cboCheckUPPrefixE, "");
            bc.setCboChckUP(cboCheckUPSelect);
            bc.bcDB.pm04DB.setCboNation(cboCheckUPNat, "");
            bc.bcDB.pttDB.setCboDeptOPDNew(cboApmDept, "");
            bc.bcDB.pttDB.setCboDeptOPDNew(cboApmDept1, "");
            bc.bcDB.pttDB.setCboDeptOPDNew(cboRpt2, "");
            bc.setCboAlienPosition(cboAlienPosition);
            bc.setCboAlienCountry(cboCheckUPCountry);
            bc.setCboSex(cboCheckUPSex);
            bc.setCboSkintone(cboCheckUPskintone);
            bc.bcDB.pm39DB.setCboPrefixT(cboCheckUPOrder, "3", "");//ใส่เพื่อให้ ดึงไม่ขึ้น
            bc.setCboNormal(cboOperEye);
            bc.setCboNormal(cboCheckUpEye);
            bc.setCboNormal(cboOperLung);
            bc.setCboNormal(cboCheckUpLung);
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
            foreach (String txt in autoApm)
            {
                chlApmList.Items.Add(txt);
            }
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
            txtApmDate.Value = DateTime.Now;
            txtRptStartDate.Value = DateTime.Now;
            txtRptEndDate.Value = DateTime.Now;
            clearControlCheckupSSO();
            chkApmDate.Checked = true;
            lstAutoComplete = new ListBox();
            lstAutoComplete.Dock = DockStyle.Fill;
            pnInformation.Controls.Add(lstAutoComplete);
            pageLoad = false;
        }
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
            fPDFl2 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize+2, FontStyle.Regular);
            fPDFs6 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 6, FontStyle.Regular);
            fPDFs8 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 8, FontStyle.Regular);
            fStaffN = new System.Drawing.Font(bc.iniC.staffNoteFontName, bc.staffNoteFontSize, FontStyle.Regular);
            fStaffNs = new System.Drawing.Font(bc.iniC.staffNoteFontName, bc.staffNoteFontSize-2, FontStyle.Regular);
            fStaffNB = new System.Drawing.Font(bc.iniC.staffNoteFontName, bc.staffNoteFontSize+1, FontStyle.Bold);
        }
        private void initControl()
        {
            initLoading();
            picL.Dock = DockStyle.Fill;
            picL.SizeMode = PictureBoxSizeMode.StretchImage;
            picR.Dock = DockStyle.Fill;
            picR.SizeMode = PictureBoxSizeMode.StretchImage;
            picL.Image = null;
            picR.Image = null;

            picHisL.Dock = DockStyle.Fill;
            picHisL.SizeMode = PictureBoxSizeMode.StretchImage;
            picHisR.Dock = DockStyle.Fill;
            picHisR.SizeMode = PictureBoxSizeMode.StretchImage;

            picFinishL.Dock = DockStyle.Fill;
            picFinishL.SizeMode = PictureBoxSizeMode.StretchImage;
            picFinishR.Dock = DockStyle.Fill;
            picFinishR.SizeMode = PictureBoxSizeMode.StretchImage;

            picSrcL.Dock = DockStyle.Fill;
            picSrcL.SizeMode = PictureBoxSizeMode.StretchImage;
            picSrcR.Dock = DockStyle.Fill;
            picSrcR.SizeMode = PictureBoxSizeMode.StretchImage;

            picStaffNote.Dock = DockStyle.Fill;
            picStaffNote.SizeMode = PictureBoxSizeMode.StretchImage;

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

            initGrfCheckUPList();
            initGrfOperList();
            initGrfPttApm(ref grfPttApm,ref pnPttApm, "grfPttApm");
            initGrfPttApm(ref grfApm, ref pnApm, "grfApm");
            initGrfOrder(ref grfOrder,ref pnOrder, "grfOrder");
            initGrfIPD();
            initGrfIPDScan();
            initGrfOPD();
            initGrfOutLab();
            initGrfLab(ref grfLab,ref pnHistoryLab);
            initGrfHisOrder();
            initGrfXray(ref grfXray, ref pnHistoryXray);
            initGrfSrc();
            initGrfSrcVs();
            initGrfSrcLab();
            initGrfSrcXray();
            initGrfSrcOrder();
            initGrfProcedure();
            initGrfSrcProcedure();
            initGrfTodayOutLab();
            initGrfOperFinish();
            initGrfLab(ref grfOperFinishLab,ref pnFinishLab);
            initGrfXray(ref grfOperFinishXray, ref pnFinishXray);
            initGrfOperFinishDrug();
            initGrfOperFinishProcedure();
            initGrfOrder(ref grfApmOrder, ref pnApmOrder, "grfApmOrder");
            initGrfRpt();
            initRptView();
            initGrfChkPackItems();
            initGrfEKG();
            initGrfDocOLD();
            initGrfEST();
            initGrfECHO();
            initGrfHolter();
            initGrfCertMed();
            initGrfMapPackage();
            initGrfpackage();
            initGrfMapPackageViewhelp();
            initGrfMapPackageItmes();
            //initGrfOperLab();
            initGrfLab(ref grfOperLab, ref pnPrenoLab);
            initGrfXray(ref grfOperXray, ref pnPrenoXray);
            pnApmOrder.Hide();
        }
        private void setTheme()
        {
            theme1.SetTheme(grfCheckUPList, "Violette");
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

            tC1.SelectedTabChanged += TC1_SelectedTabChanged;
            txtCheckUPDoctorId.KeyUp += TxtCheckUPDoctorId_KeyUp;
            btnCheckUPOrder.Click += BtnCheckUPOrder_Click;
            btnCheckUPPrn7Disease.Click += BtnCheckUPPrn7Disease_Click;
            txtCheckUPNameT.KeyUp += TxtCheckUPNameT_KeyUp;
            txtCheckUPSurNameT.KeyUp += TxtCheckUPSurNameT_KeyUp;
            txtCheckUPPassport.KeyUp += TxtCheckUPPassport_KeyUp;
            txtCheckUPNameE.KeyUp += TxtCheckUPNameE_KeyUp;
            txtCheckUPSurNameE.KeyUp += TxtCheckUPSurNameE_KeyUp;
            txtCheckUPMobile1.KeyUp += TxtCheckUPMobile1_KeyUp;
            txtCheckUPEmplyer.KeyUp += TxtCheckUPEmplyer_KeyUp;
            txtCheckUPAddr1.KeyUp += TxtCheckUPAddr1_KeyUp;
            txtCheckUPAddr2.KeyUp += TxtCheckUPAddr2_KeyUp;
            txtCheckUPPhone.KeyUp += TxtCheckUPPhone_KeyUp;
            txtCheckUPABOGroup.KeyUp += TxtCheckUPABOGroup_KeyUp;
            txtCheckUPRhgroup.KeyUp += TxtCheckUPRhgroup_KeyUp;
            txtCheckUPTempu.KeyUp += TxtCheckUPTempu_KeyUp;
            txtCheckUPWeight.KeyUp += TxtCheckUPWeight_KeyUp;
            txtCheckUPHeight.KeyUp += TxtCheckUPHeight_KeyUp;
            txtCheckUPBp1L.KeyUp += TxtCheckUPBloodPressure_KeyUp;
            txtCheckUPHrate.KeyUp += TxtCheckUPPulse_KeyUp;
            txtCheckUPRrate.KeyUp += TxtCheckUPBreath_KeyUp;
            txtOperDtr.KeyUp += TxtOperDtr_KeyUp;
            txtOperDtr.KeyPress += TxtOperDtr_KeyPress;
            btnOperSaveDtr.Click += BtnOperSaveDtr_Click;
            btnOperSaveVital.Click += BtnOperSaveVital_Click;
            txtOperTemp.KeyUp += TxtOperTemp_KeyUp;
            txtOperHrate.KeyUp += TxtOperHrate_KeyUp;
            txtOperRrate.KeyUp += TxtOperRrate_KeyUp;
            txtOperAbo.KeyUp += TxtOperAbo_KeyUp;
            txtOperRh.KeyUp += TxtOperRh_KeyUp;
            txtOperBp1L.KeyUp += TxtBp1L_KeyUp;
            txtOperBp1R.KeyUp += TxtBp1R_KeyUp;
            txtOperBp2L.KeyUp += TxtBp2L_KeyUp;
            txtOperBp2R.KeyUp += TxtBp2R_KeyUp;
            txtOperAbc.KeyUp += TxtOperAbc_KeyUp;
            txtOperWt.KeyUp += TxtOperWt_KeyUp;
            txtOperHt.KeyUp += TxtOperHt_KeyUp;
            txtOperHc.KeyUp += TxtOperHc_KeyUp;
            txtOperCc.KeyUp += TxtOperCc_KeyUp;
            txtOperCcin.KeyUp += TxtOperCcin_KeyUp;
            txtOperCcex.KeyUp += TxtOperCcex_KeyUp;

            tCOrder.SelectedTabChanged += TCOrder_SelectedTabChanged;
            btnScanSaveImg.Click += BtnScanSaveImg_Click;
            //btnPrnStaffNote1.Click += BtnPrnStaffNote1_Click;
            btnOperClose.Click += BtnOperClose_Click;
            btnSBSearch.Click += BtnSBSearch_Click;

            txtApmDtr.KeyUp += TxtApmDtr_KeyUp;
            btnApmSave.Click += BtnApmSave_Click;
            txtPttApmDate.DropDownClosed += TxtPttApmDate_DropDownClosed;
            txtPttApmDate.ValueChanged += TxtPttApmDate_ValueChanged;
            txtApmTime.KeyUp += TxtApmTime_KeyUp;
            cboApmDept.DropDownClosed += CboApmDept_DropDownClosed;
            cboApmDept1.DropDownClosed += CboApmDept1_DropDownClosed;
            txtApmDsc.KeyUp += TxtApmDsc_KeyUp;
            txtApmTel.KeyUp += TxtApmTel_KeyUp;
            txtApmRemark.KeyUp += TxtApmRemark_KeyUp;
            btnApmNew.Click += BtnApmNew_Click;
            btnApmPrint.Click += BtnApmPrint_Click;

            chkItemLab.Click += ChkItemLab_Click;
            chkItemXray.Click += ChkItemXray_Click;
            chkItemProcedure.Click += ChkItemHotC_Click;
            chkItemDrug.Click += ChkItemDrug_Click;

            txtItemCode.KeyUp += TxtItemCode_KeyUp;
            txtSearchItem.KeyUp += TxtSearchItem_KeyUp;
            btnItemAdd.Click += BtnItemAdd_Click;
            txtSearchItem.Enter += TxtSearchItem_Enter;

            txtSBSearchHN.KeyDown += TxtSBSearchHN_KeyDown;

            txtSrcHn.KeyUp += TxtSrcHn_KeyUp;
            lbApmList.DoubleClick += LbApmList_DoubleClick;
            lbApmRemList.DoubleClick += LbApmRemList_DoubleClick;
            //txtTodayOutLabStartDate.DropDownClosed += TxtTodayOutLabStartDate_DropDownClosed;

            txtSBSearchDate.DropDownClosed += TxtDateSearch_DropDownClosed;

            btnOperItemSearch.Click += BtnOperItemSearch_Click;
            btnApmOrder.Click += BtnApmOrder_Click;
            txtApmPlusDay.KeyPress += TxtApmPlusDay_KeyPress;
            txtApmPlusDay.KeyUp += TxtApmPlusDay_KeyUp;
            lbApm7Week.Click += LbApm7Week_Click;
            lbApm14Week.Click += LbApm14Week_Click;
            lbApm1Month.Click += LbApm1Month_Click;
            btnOrderSave.Click += BtnOrderSave_Click;
            btnOrderSubmit.Click += BtnOrderSubmit_Click;
            txtApmDate.DropDownClosed += TxtApmDate_DropDownClosed;

            txtApmSrc.KeyPress += TxtApmSrc_KeyPress;
            btnApmSearch.Click += BtnApmSearch_Click;
            btnApmExcel.Click += BtnApmExcel_Click;
            btnRptPrint.Click += BtnRptPrint_Click;
            btnRpt1.Click += BtnRpt1_Click;

            txtStaffNoteL.KeyUp += TxtStaffNoteL_KeyUp;
            btnStaffNote.Click += BtnStaffNote_Click;
            txtStaffNoteR.KeyUp += TxtStaffNoteR_KeyUp;

            txtCheckUPHN.KeyUp += TxtCheckUPHN_KeyUp;
            btnCheckUPPrn7Thai.Click += BtnCheckUPPrn7Thai_Click;
            btnPrnCertMed.Click += BtnPrnCertMed_Click;
            btnCerti1.Click += BtnCerti1_Click;
            btnCerti2.Click += BtnCerti2_Click;
            btnCertiView1.Click += BtnCertiView1_Click;
            btnCertiView2.Click += BtnCertiView2_Click;

            tCFinish.SelectedTabChanged += TCFinish_SelectedTabChanged;
            btnCheckUPPrnDriver.Click += BtnCheckUPPrnDriver_Click;

            btnScanGetImg.Click += BtnScanGetImg_Click;
            btnCheckUPSSoGetResult.Click += BtnCheckUPSSoGetResult_Click;
            btnCheckUPSSoPrint.Click += BtnCheckUPSSoPrint_Click;
            btnOperPrnSticker.Click += BtnOperPrnSticker_Click;
            btnOperOpenSticker.Click += BtnOperOpenSticker_Click;

            btnPrnStaffNote.Click += BtnPrnStaffNote_Click;
            btnPrnStaffNote1.Click += DropDownItem1_Click;
            btnPrnStaffNote2.Click += DropDownItem2_Click;
            btnPrnStaffNote3.Click += DropDownItem3_Click;
            btnPrnStaffNote4.Click += DropDownItem4_Click;
            dropDownItem27.Click += DropDownItem27_Click;
            dropDownItem5.Click += DropDownItem5_Click;
            dropDownItem6.Click += DropDownItem6_Click;
            dropDownItem7.Click += DropDownItem7_Click;
            dropDownItem8.Click += DropDownItem8_Click;

            btnCheckUPAlienPrint.Click += BtnCheckUPAlienPrint_Click;
            cboCheckUPSelect.SelectedItemChanged += CboCheckUPSelect_SelectedItemChanged;
            btnCheckUPAlienGetResult.Click += BtnCheckUPAlienGetResult_Click;
            btnCheckUPSaveDtr.Click += BtnCheckUPSaveDtr_Click;
            btnCheckUPSaveVital.Click += BtnCheckUPSaveVital_Click;
            btnSendDOE.Click += BtnSendDOE_Click;
            btnCheckUPFolder.Click += BtnCheckUPFolder_Click;
            btnCheckUPPrnStaffNoteDoe.Click += BtnCheckUPPrnStaffNoteDoe_Click;
            btnCheckUPDoeView.Click += BtnCheckUPDoeView_Click;

            txtOperHN.KeyUp += TxtOperHN_KeyUp;
            btnPrnLAB.Click += BtnPrnLAB_Click;
            btnPrnXray.Click += BtnPrnXray_Click;
            btnCheckUPSaveStaffNote.Click += BtnCheckUPSaveStaffNote_Click;
            txtApmHn.KeyUp += TxtApmHn_KeyUp;
            btnCheckUPExcelAlien.Click += BtnCheckUPExcelAlien_Click;
            txtApmVsNewHn.KeyUp += TxtApmVsNewHn_KeyUp;
            txtApmVsNewHn.KeyPress += TxtApmVsNewHn_KeyPress;

            btnScanClearImg.Click += BtnScanClearImg_Click;
            btnSrcEKGScanNew.Click += BtnSrcEKGScanNew_Click;
            btnSrcEKGScanSave.Click += BtnSrcEKGScanSave_Click;
            btnSrcDocOLDNew.Click += BtnSrcDocOLDNew_Click;
            btnSrcDocOLDSave.Click += BtnSrcDocOLDSave_Click;
            btnSrcECHOScanNew.Click += BtnSrcECHOScanNew_Click;
            btnSrcECHOScanSave.Click += BtnSrcECHOScanSave_Click;
            btnSrcESTScanNew.Click += BtnSrcESTScanNew_Click;
            btnSrcESTScanSave.Click += BtnSrcESTScanSave_Click;
            btnSrcHolterScanNew.Click += BtnSrcHolterScanNew_Click;
            btnSrcHolterScanSave.Click += BtnSrcHolterScanSave_Click;

            btnOperEarSave.Click += BtnOperEarSave_Click;
            btnOperEyeSave.Click += BtnOperEyeSave_Click;
            btnOperSaveStaffNote.Click += BtnOperSaveStaffNote_Click;
            btnOperLungSave.Click += BtnOperLungSave_Click;
            btnCertMedScan.Click += BtnCertMedScan_Click;
            btnCertMedGet.Click += BtnCertMedGet_Click;
            btnCertMedUpload.Click += BtnCertMedUpload_Click;
            btnCertMedUploadNew.Click += BtnCertMedUploadNew_Click;
            btnCheckUPChk1.Click += BtnCheckUPChk1_Click;
            cboOperRoom.SelectedItemChanged += CboOperRoom_SelectedItemChanged;
            cboOperRoom.SelectedIndexChanged += CboOperRoom_SelectedIndexChanged;
            btnApmMulti.Click += BtnApmMulti_Click;
            btnOperPrnQue.Click += BtnOperPrnQue_Click;
            txtMapPackageSearch.KeyUp += TxtMapPackageSearch_KeyUp;
            txtMapPackagePackagesearch.KeyUp += TxtMapPackagePackagesearch_KeyUp;
            btnMapPackagepackageSave.Click += BtnMapPackagepackageSave_Click;
            btnCheckUpEarSave.Click += BtnCheckUpEarSave_Click;
            btnCheckUpEyeSave.Click += BtnCheckUpEyeSave_Click;
            btnCheckUpLungSave.Click += BtnCheckUpLungSave_Click;
            btnCheckUpCompPrint.Click += BtnCheckUpCompPrint_Click;
            txtMapPackagepackageCode.KeyUp += TxtMapPackagepackageCode_KeyUp;
            chlApmList.LostFocus += ChlApmList_LostFocus;
            chlApmList.Leave += ChlApmList_Leave;
            lbSymptoms.DoubleClick += LbSymptoms_DoubleClick;
            btnSearchLab.Click += BtnSearchLab_Click;
            lbDoctor.DoubleClick += LbDoctor_DoubleClick;
            cboOperCritiria.SelectedItemChanged += CboOperCritiria_SelectedItemChanged;
            txtOperCodeApprove.KeyUp += TxtOperCodeApprove_KeyUp;
            cboApmDtr.SelectedItemChanged += CboApmDtr_SelectedItemChanged;
            btnOperDept.Click += BtnOperDept_Click;
            btnOperConsult.Click += BtnOperConsult_Click;
            lbOperItem.DoubleClick += LbOperItem_DoubleClick;
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
        private void initGrfSearchLab()
        {
            grfSearchLab = new C1FlexGrid();
            grfSearchLab.Font = fEdit;
            grfSearchLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSearchLab.Location = new System.Drawing.Point(0, 0);
            grfSearchLab.Rows.Count = 1;
            grfSearchLab.Cols.Count = 9;
            grfSearchLab.Cols[colLabDate].Width = 100;
            grfSearchLab.Cols[colLabName].Width = 400;
            grfSearchLab.Cols[colLabCode].Width = 80;
            
            grfSearchLab.Cols[colLabDate].Caption = "Date";
            grfSearchLab.Cols[colLabName].Caption = "Lab Name";
            grfSearchLab.Cols[colLabCode].Caption = "code";
            grfSearchLab.Cols[colLabDate].AllowEditing = false;
            grfSearchLab.Cols[colLabName].AllowEditing = false;
            //grfSearchLab.ExtendLastCol = true;
            grfSearchLab.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            grfSearchLab.DoubleClick += GrfSearchLab_DoubleClick;
            pnSearchLab.Controls.Add(grfSearchLab);
        }

        private void GrfSearchLab_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(grfSearchLab == null)            {                return;            }
            if(grfSearchLab.Row < 1)            {                return;            }
            DateTime dtLab1, dtLab2;
            dtLab2 = DateTime.Now;
            if (chkSearchlab1Year.Checked) { dtLab1 = DateTime.Now.AddYears(-1); }
            else if (chkSearchlab6Month.Checked) { dtLab1 = DateTime.Now.AddMonths(-6); }
            else { dtLab1 = DateTime.Now; }
            String date1 = dtLab1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            String date2 = dtLab2.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            String lab = grfSearchLab[grfSearchLab.Row, colLabName].ToString();
            String[] labA = lab.Split('#');
            DataTable dt = bc.bcDB.labT05DB.selectbyLabCode(date1, date2,txtSrcHn.Text.Trim(),labA[1]);

            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Size = new Size(800, 400);
            frm.Text = "เลือก Lab";
            C1FlexGrid grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);
            grf.Rows.Count = 1;
            grf.Cols.Count = 4;
            grf.Cols[3].Width = 300;
            grf.Cols[1].Width = 100;
            grf.Cols[2].Width = 300;
            grf.Cols[1].Caption = "Date";
            grf.Cols[2].Caption = "Lab Name";
            grf.Cols[3].Caption = "code";
            grf.Cols[1].AllowEditing = false;
            grf.Cols[2].AllowEditing = false;
            grf.Cols[3].AllowEditing = false;
            grf.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            frm.Controls.Add(grf);
            int i = 1;
            grf.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow dr in dt.Rows)
            {
                Row row = grf.Rows[i];
                row[1] = dr["mnc_req_dat"].ToString();
                row[2] = dr["MNC_LB_DSC"].ToString();
                row[3] = dr["MNC_USR_FULL_usr"].ToString();
                i++;
            }
            frm.ShowDialog(this);
        }
        private void setGrfSearchLab(String hn)
        {
            //throw new NotImplementedException();
            DateTime  dtLab1, dtLab2;
            dtLab2 = DateTime.Now;
            if (chkSearchlab1Year.Checked)          {                dtLab1 = DateTime.Now.AddYears(-1);            }
            else if (chkSearchlab6Month.Checked)    {                dtLab1 = DateTime.Now.AddMonths(-6);           }
            else if (chkSearchlab1Jan69.Checked) { dtLab1 = new DateTime(2026, 1, 1); }
            else { dtLab1 = DateTime.Now; }
            String date1 = dtLab1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            String date2 = dtLab2.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            DataTable dt = bc.bcDB.labT05DB.selecCnttbyHn(date1, date2, hn,"");
            grfSearchLab.Rows.Count = 1; grfSearchLab.Rows.Count = dt.Rows.Count+1;
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                Row row = grfSearchLab.Rows[i];
                row[colLabDate] = dr["cnt"].ToString();
                row[colLabName] = dr["MNC_LB_DSC"].ToString()+"#"+ dr["MNC_LB_CD"].ToString();
                //row[colLabCode] = dr["MNC_LB_CD"].ToString();
                row[3] = dr["MNC_LB_PRI01"].ToString();
                i++;
            }
            //grfSearchLab.AutoSizeCols();
            grfSearchLab.Cols[colLabCode].Visible = false;
            grfSearchLab.Cols[7].Visible = false;
            grfSearchLab.Cols[6].Visible = false;
            grfSearchLab.Cols[5].Visible = false;
            grfSearchLab.Cols[4].Visible = false;
            grfSearchLab.Cols[3].Visible = true;
            grfSearchLab.Cols[colLabDate].Caption = "count";
            grfSearchLab.Cols[colLabCode].Width = 80;
            grfSearchLab.Cols[3].AllowEditing = false;
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
        private void initGrfApmMulti()
        {
            grfApmMulti = new C1FlexGrid();
            grfApmMulti.Font = fEdit;
            grfApmMulti.Dock = System.Windows.Forms.DockStyle.Fill;
            grfApmMulti.Location = new System.Drawing.Point(0, 0);
            grfApmMulti.Rows.Count = 1;
            grfApmMulti.Cols.Count = 5;
            grfApmMulti.Name = "grfApmMulti";

            grfApmMulti.Cols[1].Width = 100;            grfApmMulti.Cols[2].Width = 200;
            grfApmMulti.Cols[3].Width = 60;            grfApmMulti.Cols[4].Width = 500;

            grfApmMulti.ShowCursor = true;
            grfApmMulti.Cols[1].Caption = "date";            grfApmMulti.Cols[2].Caption = "นัดวันที่";
            grfApmMulti.Cols[3].Caption = "นัดเวลา";            grfApmMulti.Cols[4].Caption = "นัดตรวจที่แผนก";

            grfApmMulti.Cols[1].DataType = typeof(String);            grfApmMulti.Cols[2].DataType = typeof(String);            grfApmMulti.Cols[3].DataType = typeof(String);            grfApmMulti.Cols[4].DataType = typeof(String);

            grfApmMulti.Cols[1].TextAlign = TextAlignEnum.LeftCenter;            grfApmMulti.Cols[2].TextAlign = TextAlignEnum.LeftCenter;            grfApmMulti.Cols[3].TextAlign = TextAlignEnum.LeftCenter;            grfApmMulti.Cols[4].TextAlign = TextAlignEnum.LeftCenter;

            grfApmMulti.Cols[1].AllowEditing = false;            grfApmMulti.Cols[2].AllowEditing = false;            grfApmMulti.Cols[3].AllowEditing = false;            grfApmMulti.Cols[4].AllowEditing = false;
            pnApmMulti.Controls.Add(grfApmMulti);
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
        private void genCheckUPAlienPDF()
        {
            //new LogWriter("d", "FrmOPD genCheckUPAlienPDF ");
            String filename = "";
            int gapLine = 14, linenumber = 5, gapX = 40, gapY = 20, xCol1 = 20, xCol2 = 40, xCol3 = 100, xCol4 = 200, xCol5 = 250, xCol6 = 300, xCol7 = 450, xCol8 = 510;
            String certid = "";
            certid = chkCheckUPEditCert.Checked ? insertCertDoctorCheckUp("edit") : insertCertDoctorCheckUp("");
            //new LogWriter("d", "FrmOPD genCheckUPAlienPDF 00");
            bc.bcDB.vsDB.updateMedicalCertId(txtCheckUPHN.Text.Trim(), PRENO, VSDATE, certid);
            if (txtCheckUPDoctorId.Text.Trim().Equals("24738")) {
                bc.bcDB.tokenDB.updateUsed(txtCheckUPDoctorId.Text.Trim(), "cert_alien", certid);       // ทำเพื่อ จะได้ มีระบบ ลายเซ็นแพทย์ เป็นแบบรูปภาพ แล้วมีการเก็บ ลง ใน ระบบ
            }
            if (certid.Length > 3)            {                /*certid = certid.Replace("555", "");*/                certid = certid.Substring(3, 7);            }
            if (certid.Length < 3) { MessageBox.Show("ไม่พบ เลขที่ กรุณาตรวจสอบ hn visitdate preno ไม่ถูกต้อง", "");  return; }    //ต้องมี เพราะมี case ที่ไม่มี certid แต่มีการเรียกใช้งาน
            lfSbStatus.Text = certid;
            String patheName = Environment.CurrentDirectory + "\\cert_med\\";
            if (!Directory.Exists(patheName))            {                Directory.CreateDirectory(patheName);            }
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
            filename = certid + "_"+txtCheckUPHN.Text.Trim() + "_alien_" + DateTime.Now.Year.ToString() + ".pdf";
            try
            {
                qrcode.CodeType = C1.BarCode.CodeType.QRCode;
                qrcode.Text = txtCheckUPHN.Text.Trim() + " " +cboCheckUPPrefixT.Text +" "+ txtCheckUPNameT.Text.Trim()+" "+ txtCheckUPSurNameT.Text.Trim() + " " + DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year).ToString() + " " + certid;
                qrcode.AutoSize = false;
                qrcode.Width = 60;
                qrcode.Height = 60;
                System.Drawing.Image imgqrcode = qrcode.Image;
                // Replace the line causing the error with the correct namespace
                Image loadedImagelogo = Resources.LOGO_BW_tran;
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

                RectangleF recflogo = new RectangleF((PageSize.A4.Width / 2) - 38,5, (int)newWidth, (int)newHeight);
                pdf.DrawImage(loadedImagelogo, recflogo);
                linenumber += gapLine;                linenumber += gapLine;                linenumber += gapLine; linenumber += gapLine;
                pdf.DrawString("ใบรับรองแพทย์", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 38, linenumber += (gapLine-5)), _sfLeft);
                pdf.DrawString("เลขที่ "+ certid, fPDFl2, Brushes.Black, new PointF(xCol2+520, 50), _sfRight);
                pdf.DrawString("ตรวจสุขภาพคนต่างด้าว/แรงงานต่างด้าว", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 78, linenumber += (gapLine)), _sfLeft);
                if(EDITDOE) pdf.DrawString("วันที่ตรวจ " + bc.datetoShow1(VSDATE), fPDF, Brushes.Black, new PointF(PageSize.A4.Width - 120, linenumber), _sfLeft);
                else pdf.DrawString("วันที่ตรวจ " + DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year).ToString(), fPDF, Brushes.Black, new PointF(PageSize.A4.Width - 120, linenumber), _sfLeft);
                pdf.DrawString("1. รายละเอียด ประวัติส่วนตัวของผู้รับการตรวจสุขภาพ", fPDFl2, Brushes.Black, new PointF(xCol2 , linenumber += (gapLine +5)), _sfLeft);
                pdf.DrawString("1) ชื่อ-นามสกุล(นาย,นาง,นางสาว,เด็กชาย,เด็กหญิง) ...................................................................................................................................................." , fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString("ชื่อ-นามสกุล(ภาษาอังกฤษ) ............................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(cboCheckUPPrefixT.Text + " " + txtCheckUPNameT.Text.Trim() + " " + txtCheckUPSurNameT.Text.Trim() , fPDF, Brushes.Black, new PointF(xCol2 + 115, linenumber -3), _sfLeft);
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
                pdf.DrawString("2. รายละเอียด ข้อมูลนายจ้าง/สถานประกอบการ", fPDFl2, Brushes.Black, new PointF(xCol2 , linenumber += (gapLine + 5)), _sfLeft);
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
                pdf.DrawString("ผลการตรวจสุขภาพ", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2)-30, linenumber += (gapLine + 5)), _sfLeft);
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
                pdf.DrawString("สรุปผลตรวจ", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2)-30, linenumber += (gapLine + 5)), _sfLeft);
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
                pdf.DrawString("3.2 เป็นโรคไม่อนุญาตให้ทำงาน และไม่ให้การประกันสุขภาพ(ตามประกาศกระทรวงสาธารณสุขฯ)", fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber += (gapLine )), _sfLeft);
                int lineqrcode = linenumber;
                pdf.DrawString("แพทย์ผู้ตรวจ", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 100, linenumber += (gapLine + 5)), _sfLeft);
                linenumber += (gapLine + 15);
                int linesign = linenumber;
                pdf.DrawString("(.....................................................................................) ให้ประทับตรา", fPDF, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 100, linesign), _sfLeft);
                pdf.DrawString(txtCheckUPDoctorName.Text.Trim(), fPDF, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 90, linenumber - 3), _sfLeft);
                pdf.DrawString("(หมายเหตุ ใบรับรองแพทย์ฉบับนี้มีอายุ 90วัน นับตั้งแต่วันที่ตรวจร่างกาย ยกเว้น กรณีใช้สำหรับประกันสุขภาพมีอายุ 1 ปี)", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                RectangleF recfqrcode = new RectangleF(xCol2 + 465, lineqrcode-10, imgqrcode.Width, imgqrcode.Height+20);
                pdf.DrawImage(imgqrcode, recfqrcode);
                //ถ้าใน Folder sign มีรูป ให้แสดง ลงใน PDF
                //new LogWriter("d", "FrmOPD genCheckUPAlienPDF 02");
                String signFileName = Environment.CurrentDirectory + "\\sign\\sign_"+ txtCheckUPDoctorId.Text.Trim() + ".jpg";
                if (File.Exists(signFileName))
                {
                    //new LogWriter("d", "FrmOPD genCheckUPAlienPDF 03");
                    Image imgSign = null;                    imgSign = Image.FromFile(signFileName);
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
            catch (Exception ex) {
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
        private void BtnCheckUPSSoPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            genCheckUPSSOPDF1();
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

                Image loadedImage = Resources.LOGO_BW_tran;
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
                pdf.DrawString("ชื่อ-นามสกุล/Name " + cboCheckUPPrefixT.Text + " " + txtCheckUPNameT.Text.Trim() + " " + txtCheckUPSurNameT.Text.Trim()+" เลขที่บัตรประชาชน "+PTT.MNC_ID_NO + " HN " + PTT.MNC_HN_NO, fPDF, Brushes.Black, new PointF(xCol2+5, linenumber += gapLine), _sfLeft);
                pdf.DrawString("เลขที่ " + certid.Replace("555",""), fPDF, Brushes.Black, new PointF(PageSize.A4.Width-125, linenumber), _sfLeft);

                pdf.DrawString("ที่อยู่ " + txtCheckUPAddr1.Text.Trim()+" "+ txtCheckUPPhone.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol2+5, linenumber += (gapLine )), _sfLeft);
                pdf.DrawString("วันที่ตรวจ "+DateTime.Now.ToString("dd-MM-")+(DateTime.Now.Year+543).ToString(), fPDF, Brushes.Black, new PointF(PageSize.A4.Width-125, linenumber), _sfLeft);
                pdf.DrawString("ข้อมูลสุขภาพ (Health Data)", fPDFl2, Brushes.Black, new PointF(pointcenter - 40, linenumber += (gapLine+5)), _sfLeft);
                
                pdf.DrawRectangle(Pens.Black, xCol2, linenumber += (gapLine+5), PageSize.A4.Width - xCol2 -20, 510);
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
                pdf.DrawString("1. การคัดกรองการได้ยิน", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine-11)), _sfLeft);
                pdf.DrawString(cboCheckUPResFRT.Text, fPDF, Brushes.Black, new PointF(cboCheckUPResFRT.Text.Equals("Normal")?xCol4+5:xCol5+5, linenumber), _sfLeft);
                pdf.DrawString("6. การตรวจระดับน้ำตาลในเลือด", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber), _sfLeft);
                pdf.DrawString("    FBS", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber + 12), _sfLeft);
                pdf.DrawString(txtCheckUPResFBS.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 12, linenumber + 10), _sfLeft);
                pdf.DrawString(txtCheckUPResFBSStandard.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber + 12), _sfLeft);
                pdf.DrawString("   Finger Rub Test", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber + 12), _sfLeft);
                linenumber += (gapLine+13);
                pdf.DrawString("2. การตรวจเต้านมโดยแพทย์หรือ", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine-10)), _sfLeft);
                pdf.DrawString(cboCheckUPResChest.Text, fPDF, Brushes.Black, new PointF(cboCheckUPResChest.Text.Equals("Normal") ? xCol4 + 5 : xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("7. ตรวจหาเชื้อไวรัสตับอักเสบบี", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber), _sfLeft);
                pdf.DrawString("    HBsAg", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber + 12), _sfLeft);
                pdf.DrawString(txtCheckUPResHBsAg.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 12, linenumber + 10), _sfLeft);
                pdf.DrawString("   บุคลากรสาธารณสุข", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber + 12), _sfLeft);

                pdf.DrawString("3. การตรวจตาโดยความดูแลของจักษุแพทย์", fPDFs2, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine+18)), _sfLeft);
                pdf.DrawString("VA", fPDF, Brushes.Black, new PointF(xCol4 + 15, linenumber), _sfLeft);
                pdf.DrawString("Ph", fPDF, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("8. ตรวจการทำงานของไต", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber), _sfLeft);
                
                pdf.DrawString("  และการตรวจSnellen eye Chart", fPDF, Brushes.Black, new PointF(xCol2 + 12, linenumber += (gapLine-5)), _sfLeft);
                pdf.DrawString("R......./.......", fPDF, Brushes.Black, new PointF(xCol4 + 3, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResEyeVAR1.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber-2), _sfLeft);
                pdf.DrawString(txtCheckUPResEyeVAR2.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10 + 20, linenumber-2), _sfLeft);
                pdf.DrawString("L......./.......", fPDF, Brushes.Black, new PointF(xCol4 + 3, linenumber + 15), _sfLeft);
                pdf.DrawString(txtCheckUPResEyeVAL1.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 8, linenumber + 12), _sfLeft);
                pdf.DrawString(txtCheckUPResEyeVAL2.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 8 + 20, linenumber + 12), _sfLeft);
                pdf.DrawString("R...............", fPDF, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("  Serun Creatinine", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResCreatinine.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 12, cboCheckUPSex.Text.Equals("M") ? linenumber  : linenumber+15), _sfLeft);
                pdf.DrawString(cboCheckUPSex.Text.Equals("M") ? txtCheckUPResCreatinineStandard.Text.Length > 0 ? txtCheckUPResCreatinineStandard.Text.Trim() +" M": "0.8-1.3 mg/dl M" : "0.8-1.3 mg/dl M", fPDFs2, Brushes.Black, new PointF(xCol8 + 2, linenumber ), _sfLeft);
                pdf.DrawString(txtCheckUPResEyePhR.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol5 + 12, linenumber - 2), _sfLeft);
                pdf.DrawString(cboCheckUPSex.Text.Equals("F") ? txtCheckUPResCreatinineStandard.Text.Length > 0 ? txtCheckUPResCreatinineStandard.Text.Trim() + " F" : "0.6-1.1 mg/dl F" : "0.6-1.1 mg/dl F", fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber + 15), _sfLeft);
                pdf.DrawString("L...............", fPDF, Brushes.Black, new PointF(xCol5 + 5, linenumber + 15), _sfLeft);
                pdf.DrawString(txtCheckUPResEyePhL.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol5 + 12, linenumber+13), _sfLeft);

                linenumber += gapLine;
                pdf.DrawString("  การวัดความดันของเหลวภายในลูกตา", fPDF, Brushes.Black, new PointF(xCol2 + 12, linenumber += (gapLine-5)), _sfLeft);
                pdf.DrawString("R...............", fPDF, Brushes.Black, new PointF(xCol4 + 3, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResEyeVAR.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber - 2), _sfLeft);
                pdf.DrawString("L...............", fPDF, Brushes.Black, new PointF(xCol4 + 3, linenumber+15), _sfLeft);
                pdf.DrawString(txtCheckUPResEyeVAL.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber+12), _sfLeft);
                pdf.DrawString(txtCheckUPResEye.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol5 + 10, linenumber + 12), _sfLeft);
                pdf.DrawString("eGFR Crez", fPDF, Brushes.Black, new PointF(xCol6 + 12, linenumber + 10), _sfLeft);
                pdf.DrawString(txtCheckUPReseGFR.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 12, linenumber + 10), _sfLeft);
                pdf.DrawString(txtCheckUPReseGFRStandard.Text.Trim(), fPDFs6, Brushes.Black, new PointF(xCol8 + 2, linenumber + 15), _sfLeft);

                linenumber += gapLine;
                pdf.DrawString("4. ความสมบรูณ์ของเม็ดเลือด CBC", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine )), _sfLeft);
                pdf.DrawString("ค่าที่ตรวจได้", fPDF, Brushes.Black, new PointF(xCol4 + 5, linenumber ), _sfLeft);
                pdf.DrawString("ค่าปกติ", fPDF, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawString("9. การตรวจไขมันในเลือด", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber), _sfLeft);
                pdf.DrawString("Choleterol", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber += (gapLine-5)), _sfLeft);
                pdf.DrawString(txtCheckUPResCholes.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 12, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResCholesStandard.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber), _sfLeft);
                linenumber += (gapLine-5);
                pdf.DrawString("HDL Cholesterol", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber + 10), _sfLeft);
                pdf.DrawString(txtCheckUPResHDL.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol7 + 12, cboCheckUPSex.Text.Equals("M") ?  linenumber: linenumber + 15), _sfLeft);
                //pdf.DrawString(cboCheckUPSex.Text.Equals("M") ? txtCheckUPResHDLStandard.Text.Length > 0 ? txtCheckUPResHDLStandard.Text.Trim()+"M" : "0.8-1.4 mg/dl M" : "0.8-1.4 mg/dl M", fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber), _sfLeft);
                //pdf.DrawString(cboCheckUPSex.Text.Equals("F") ? txtCheckUPResHDLStandard.Text.Length > 0 ? txtCheckUPResHDLStandard.Text.Trim()+" F": "0.6-1.1 mg/dl F" : "0.6-1.1 mg/dl F", fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber + 15), _sfLeft);
                pdf.DrawString("M > 35 " , fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber), _sfLeft);
                pdf.DrawString("F > 44", fPDFs2, Brushes.Black, new PointF(xCol8 + 5, linenumber + 15), _sfLeft);
                pdf.DrawString("  ภาวะโลหิตจาง     Hb", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResHB.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, cboCheckUPSex.Text.Equals("M") ? linenumber: linenumber + 15), _sfLeft);
                pdf.DrawString(cboCheckUPSex.Text.Equals("M") ? txtCheckUPResHBStandard.Text.Length>0? txtCheckUPResHBStandard.Text.Trim() +" M": "14.1-18.1g/dl M" : "14.1-18.1g/dl M", fPDFs6, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                
                linenumber += (gapLine);
                pdf.DrawString(cboCheckUPSex.Text.Equals("F") ? txtCheckUPResHBStandard.Text .Length>0 ? txtCheckUPResHBStandard.Text.Trim() +" F": "12.1-16.1g/df F" : "12.1-16.1g/df F", fPDFs6, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                pdf.DrawLine(Pens.Black, xCol6, linenumber+15, 575, linenumber + 15);
                
                linenumber += gapLine;
                pdf.DrawString("  ความเข้มข้นของเลือด     HCT", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber), _sfLeft);
                pdf.DrawString(cboCheckUPSex.Text.Equals("M") ? txtCheckUPResHCTStandard.Text.Length > 0 ? txtCheckUPResHCTStandard.Text.Trim()+" M" : " 41-51% M" : " 41-51% M", fPDFs2, Brushes.Black, new PointF(xCol5 + 5, linenumber), _sfLeft);
                
                pdf.DrawString(txtCheckUPResHCT.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, cboCheckUPSex.Text.Equals("M") ? linenumber : linenumber+ gapLine), _sfLeft);
                pdf.DrawString("10. การตรวจปัสสาวะ Urine Analysis(UA)", fPDF, Brushes.Black, new PointF(xCol6 + 5, linenumber), _sfLeft);
                pdf.DrawString(cboCheckUPSex.Text.Equals("F") ? txtCheckUPResHCTStandard.Text.Length > 0 ? txtCheckUPResHCTStandard.Text.Trim()+" F" : " 37-47% F" : " 37-47% F", fPDFs2, Brushes.Black, new PointF(xCol5 + 5, linenumber += gapLine), _sfLeft);
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
                pdf.DrawString(txtCheckUPResWBCStandard.Text.Trim(), fPDFs6, Brushes.Black, new PointF(xCol5 + 2, linenumber+4), _sfLeft);
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
                pdf.DrawString(txtCheckUPResPlaCntStandard.Text.Trim(), fPDFs8, Brushes.Black, new PointF(xCol5 + 2, linenumber+4), _sfLeft);

                pdf.DrawString("  รูปร่างเม็ดเลือดแดง     RBC", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += gapLine), _sfLeft);
                pdf.DrawString(txtCheckUPResRBC.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol4 + 10, linenumber), _sfLeft);
                pdf.DrawString(txtCheckUPResRBCStandard.Text.Trim(), fPDFs6, Brushes.Black, new PointF(xCol5 + 5, linenumber + 4), _sfLeft);

                linenumber += gapLine;
                //pdf.DrawString("5. การถ่ายภาพรังสีทรวงอก ", fPDF, Brushes.Black, new PointF(xCol2 + 5, 630), _sfLeft);
                pdf.DrawString("5. การถ่ายภาพรังสีทรวงอก Chest X-ray", fPDF, Brushes.Black, new PointF(xCol2 + 5, 630 + gapLine-5), _sfLeft);
                pdf.DrawString(cboCheckUPResXray.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 220, 630 + gapLine-5), _sfLeft);
                //pdf.DrawString("11. การคัดกรองมะเร็งลำไส้ใหญ่ และลำไส้ตรง(Fit test)", fPDF, Brushes.Black, new PointF(xCol6 + 5, 630), _sfLeft);
                //pdf.DrawString("Result    " + cboCheckUPResMS017.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol6 + 50, 630 ), _sfLeft);
                pdf.DrawString("11. การคัดกรองมะเร็งลำไส้ใหญ่ และลำไส้ตรง(Fit test)         " + cboCheckUPResMS017.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol6 + 5, 630 + gapLine-5), _sfLeft);
                
                linenumber = 650;
                pdf.DrawString("สรุปผลตรวจ .....................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine - 2)), _sfLeft);
                pdf.DrawString(cboCheckUPRes.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 60, linenumber - 5), _sfLeft); 
                pdf.DrawString("คำแนะนำเพิ่มเติมในการดูแลสุขภาพ .................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2, linenumber += (gapLine-2)), _sfLeft);
                pdf.DrawString(cboCheckUPRes1.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol4 - 11, linenumber - 2), _sfLeft);
                pdf.DrawString(".............................................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 , linenumber += (gapLine-4)), _sfLeft);
                pdf.DrawString(cboCheckUPRes2.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol2 + 5, linenumber - 2), _sfLeft);
                pdf.DrawString("ผู้ประกันตนลงนาม ................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine-4)), _sfLeft);
                pdf.DrawString("ลงชื่อแพทย์ผู้ตรวจ ...............................................................................", fPDF, Brushes.Black, new PointF(xCol6 + 25, linenumber), _sfLeft);
                pdf.DrawString("            (.................................................................................)", fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber += gapLine), _sfLeft);
                RectangleF recf1 = new RectangleF(xCol2 + 85, linenumber - 4, 150, 20);
                pdf.DrawString(cboCheckUPPrefixT.Text + " " + txtCheckUPNameT.Text.Trim() + " " + txtCheckUPSurNameT.Text.Trim(), fPDF, Brushes.Black, recf1, _sfCenter);
                pdf.DrawString("            (...............................................................................)", fPDF, Brushes.Black, new PointF(xCol6 + 55, linenumber), _sfLeft);
                recf1 = new RectangleF(xCol6 + 105, linenumber - 4, 150, 20);
                pdf.DrawString(txtCheckUPDoctorName.Text.Trim(), fPDF, Brushes.Black, recf1, _sfCenter);
                int lineqrcode = linenumber+10;
                RectangleF recfqrcode = new RectangleF(xCol2+20, lineqrcode - 12, imgqrcode.Width+15, imgqrcode.Height+15);
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
        private void Document_PrintPage_StaffNote_Reserve(object sender, PrintPageEventArgs e)
        {
            String[] txtL = txtStaffNoteL.Text.Trim().Split('\n');
            String[] txtR = txtStaffNoteR.Text.Trim().Split('\n');
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);
            int line = 0, y = 0, i = 1, gapline=32;
            foreach (String txt1 in txtL)
            {
                y = 350 + line;
                if (i == 1) y = 350; else if(i == 2) y = 379; else if (i == 3) y = 408; else if (i == 4) y = 437;
                else if (i == 5) y = 467; else if (i == 6) y = 497; else if (i == 7) y = 520; else if (i == 8) y = 550;
                e.Graphics.DrawString(txt1.Replace("\r", ""), famt7, Brushes.Black, 50, y, flags);
                line += gapline; i++;
            }
            line = 0;            i = 1;
            foreach (String txt1 in txtR)
            {
                y = 270 + line;
                if (i == 1) y = 269; else if (i == 2) y = 297;    else if (i == 3) y = 325; else if (i == 4) y = 350; else if (i == 5) y = 380;
                else if (i == 6) y = 410; else if (i == 7) y = 438; else if (i == 8) y = 467; else if (i == 9) y = 495; else if (i == 10) y = 523; else if (i == 11) y = 550; 
                else if (i == 12) y = 580; else if (i == 13) y = 608; else if (i == 14) y = 637; else if (i == 15) y = 667; else if (i == 16) y = 696; else if (i == 17) y = 725;
                e.Graphics.DrawString(txt1.Replace("\r", ""), famt7, Brushes.Black, 620, y, flags);
                line += gapline; i++;
            }
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
        private void setGrfApm()
        {
            if (pageLoad) return;
            String deptcode = "", dtrcode = "";
            DataTable dtvs = new DataTable();
            if (chkApmDate.Checked)
            {
                DateTime.TryParse(txtApmDate.Value.ToString(), out DateTime dtapm);//txtApmDate
                if (dtapm.Year < 1900)                {                    dtapm.AddYears(543);                }
                deptcode = cboApmDept1.SelectedItem == null ? "" : ((ComboBoxItem)cboApmDept1.SelectedItem).Value.ToString();
                dtrcode = cboApmDtr.SelectedItem != null ? ((ComboBoxItem)cboApmDtr.SelectedItem).Value : "";
                dtvs = bc.bcDB.pt07DB.selectByDateDept(dtapm.Year.ToString() + "-" + dtapm.ToString("MM-dd"), deptcode, dtrcode, "");
            }
            else if (chkApmHn.Checked)
            {
                dtvs = bc.bcDB.pt07DB.selectByHnAll(txtApmHn.Text.Trim(), "desc");
            }
            HashSet<ComboBoxItem> lstdtr = new HashSet<ComboBoxItem>();
            grfApm.Rows.Count = 1; grfApm.Rows.Count = dtvs.Rows.Count + 1;
            cboApmDtr.Items.Clear();
            cboApmDtr.SelectedItem = null;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                if (dtrcode.Length <= 0)
                {
                    ComboBoxItem dtr = new ComboBoxItem();
                    dtr.Value = row1["MNC_DOT_CD"].ToString();
                    dtr.Text = row1["dtr_name"].ToString();
                    if (!lstdtr.Any(d => d.Value == dtr.Value)) { lstdtr.Add(dtr); cboApmDtr.Items.Add(dtr); }
                }
                Row rowa = grfApm.Rows[i];
                rowa[colgrfPttApmApmDateShow] = bc.datetoShowShort(row1["MNC_APP_DAT"].ToString());
                rowa[colgrfPttApmApmDateShow] = row1["MNC_APP_DAT"].ToString();
                rowa[colgrfPttApmApmTime] = bc.showTime(row1["MNC_APP_TIM"].ToString());
                rowa[colgrfPttApmDeptR] = row1["mnc_md_dep_dsc"].ToString();
                rowa[colgrfPttApmDeptMake] = bc.bcDB.pm32DB.getDeptNameOPD(row1["mnc_sec_no"].ToString());
                rowa[colgrfPttApmNote] = row1["MNC_APP_DSC"].ToString();
                rowa[colgrfPttApmOrder] = row1["MNC_REM_MEMO"].ToString();

                rowa[colgrfPttApmDocNo] = row1["MNC_DOC_NO"].ToString();
                rowa[colgrfPttApmDocYear] = row1["MNC_DOC_YR"].ToString();
                rowa[colgrfPttApmHN] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfPttApmPttName] = row1["ptt_fullnamet"].ToString();
                rowa[colgrfPttApmDtrname] = row1["dtr_name"].ToString();
                rowa[colgrfPttApmPhone] = row1["MNC_CUR_TEL"].ToString();
                rowa[colgrfPttApmPaidName] = row1["MNC_FN_TYP_DSC"].ToString();

                rowa[colgrfPttApmRemarkCall] = row1["remark_call"].ToString();
                rowa[colgrfPttApmRemarkCallDate] = row1["remark_call_date"].ToString();
                if (row1["status_remark_call"].ToString().Equals("1")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรเรียบร้อย รับสาย บุคคลอื่นเป็นคนรับ"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor); }
                else if (row1["status_remark_call"].ToString().Equals("2")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรเรียบร้อย ไม่รับสาย"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6"); }//#EBBDB6
                else if (row1["status_remark_call"].ToString().Equals("3")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรไม่ติด สายไม่ว่าง"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#CCCCFF"); }
                else if (row1["status_remark_call"].ToString().Equals("4")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรเรียบร้อย รับสาย แจ้งคนไข้ ครบถ้วน"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#9FE2BF"); }
                else if (row1["status_remark_call"].ToString().Equals("5")) { rowa[colgrfPttApmStatusRemarkCall] = "ไม่สามารถโทรได้ ไม่มีเบอร์โทร"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#FF7F50"); }
                else rowa[colgrfPttApmStatusRemarkCall] = "";

                rowa[0] = i.ToString();
                i++;
            }            
            lfSbMessage.Text = "พบ " + dtvs.Rows.Count + "รายการ";
        }
        private void BtnOrderSubmit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
            frm.ShowDialog();
            frm.Dispose();
            if (bc.USERCONFIRMID.Length <= 0)
            {
                lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            String re = "";
            re = bc.bcDB.vsDB.insertOrder(HN, VSDATE, PRENO, txtOperDtr.Text.Trim(), bc.USERCONFIRMID);
            if (re.Length>0)
            {
                String[] reqno = re.Split('#');
                if(reqno.Length > 2)
                {
                    DataTable dtlab = new DataTable();
                    DataTable dtxray = new DataTable();
                    DataTable dtprocedure = new DataTable();
                    DataTable dtdrug = new DataTable();
                    dtlab = bc.bcDB.labT02DB.selectbyHNReqNo(HN, VSDATE, reqno[0].Replace("lab=",""));
                    dtdrug = bc.bcDB.labT02DB.selectbyHNReqNo(HN, VSDATE, reqno[1].Replace("drug=", ""));
                    dtxray = bc.bcDB.xrayT02DB.selectbyHNReqNo(HN, VSDATE, reqno[3].Replace("xray=", ""));
                    dtprocedure = bc.bcDB.pt16DB.selectbyHNReqNo(HN, VSDATE, reqno[2].Replace("procedure=", ""));
                    dtlab.Merge(dtxray);
                    dtlab.Merge(dtprocedure);

                    int i = 1, j = 1;
                    grfOrder.Rows.Count = 1;
                    grfOrder.Rows.Count = dtlab.Rows.Count + 1;
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
                            rowa[colgrfOrderReqNO] = row1["req_no"].ToString();
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
            }
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
                if (int.TryParse(re, out int _))
                {
                    lfSbMessage.Text = "save OK";
                }
                else
                {
                    lfSbMessage.Text = re;
                }
            }
            setGrfOrderTemp();
            //if (grfOperFinishLab != null) setGrfLab(HN, VSDATE, PRENO, ref grfOperFinishLab);       //LAB
            //setGrfHisOrder(HN, VSDATE, PRENO, ref grfOperFinishDrug);                               //DRUG
            //if (grfOperFinishXray != null) setGrfXray(HN, VSDATE, PRENO, ref grfOperFinishXray);    //XRAY
            //if (TCFinishActive.Equals(tabFinishCertMed.Name)) setCertiMed();                        //cert med
            //setGrfHisProcedure(HN, VSDATE, PRENO, ref grfOperFinishProcedure);                      //Procedure
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
        private void setGrfSrc(String flagsearch)
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.pttDB.selectPatinetBySearch(txtSrcHn.Text.Trim());
            int i = 1, j = 1;
            grfSrc.Rows.Count = 1;
            grfSrc.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                try
                {
                    Patient ptt = new Patient();
                    Row rowa = grfSrc.Rows[i];
                    HN = row1["MNC_HN_NO"].ToString();
                    ptt.patient_birthday = row1["MNC_bday"].ToString();
                    rowa[colgrfSrcHn] = row1["MNC_HN_NO"].ToString();
                    rowa[colgrfSrcFullNameT] = row1["pttfullname"].ToString();
                    rowa[colgrfSrcPID] = row1["mnc_id_no"].ToString();
                    rowa[colgrfSrcDOB] = bc.datetoShowShort(row1["MNC_bday"].ToString());
                    rowa[colgrfSrcAge] = ptt.AgeStringShort1();
                    rowa[colgrfSrcVisitReleaseOPD] = bc.datetoShowShort(row1["MNC_LAST_CONT"].ToString());
                    rowa[colgrfSrcVisitReleaseIPD] = bc.datetoShowShort(row1["MNC_LAST_CONT_I"].ToString());
                    rowa[colgrfSrcVisitReleaseIPDDischarge] = bc.datetoShowShort(row1["ipd_discharge_release"].ToString());
                    rowa[colgrfSrcPttid] = "";
                    rowa[0] = i.ToString();
                    i++;
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD setGrfSrc " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setGrfSrc ", ex.Message);
                }
            }
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
        private void Document_PrintPageAppointment(object sender, PrintPageEventArgs e)
        {
            float yPos = 10, col1 = 0, col2 = 50, col21 = 180, col3 = 250, col4 = 450, col41 = 560, col5 = 300, line=25;
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
            e.Graphics.DrawString(bc.iniC.hostname, famt2B, Brushes.Black, centerpage - (textSize.Width/2), yPos, flags);
            
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText(bc.iniC.hostnamee, famt2, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(bc.iniC.hostnamee, famt2B, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            e.Graphics.DrawString("print date " + DateTime.Now, fEditS, Brushes.Black, col41 + 220, yPos, flagsR);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText("ใบนัดพบแพทย์ Appointment Note", famt2B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString("ใบนัดพบแพทย์ Appointment Note", famt4B, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            e.Graphics.DrawString("เลขที่: "+APM.MNC_DOC_YR.Substring(APM.MNC_DOC_YR.Length - 2) + "-" + APM.MNC_DOC_NO, famt2, Brushes.Black, col41+220, yPos, flagsR);
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
            if(APM.nationcode.Equals("01") || APM.nationcode.Equals("TH"))
            {
                e.Graphics.DrawString(bc.datetoShowTHMMM(APM.MNC_APP_DAT) + " " + APM.apm_time, famt2B, Brushes.Black, col21, yPos, flags);
            }
            else
            {
                e.Graphics.DrawString(bc.datetoShowEN(APM.MNC_APP_DAT) + " " + APM.apm_time, famt2B, Brushes.Black, col21, yPos, flags);
            }
            e.Graphics.DrawLine(blackPen, col21, yPos+line, col21+160, yPos + line);
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
                    String name = "", code="";
                    name = rowa[colgrfOrderName].ToString();
                    code = rowa[colgrfOrderCode].ToString();
                    if (name.Equals("name")) continue;
                    txt += code+" "+name + "\r\n";
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
            e.Graphics.DrawLine(blackPen, col4+60, yPos+22, col4+270, yPos+22);
            if(bc.cStf!= null) { e.Graphics.DrawString(bc.cStf.fullname, famt2, Brushes.Black, col4 + 60, yPos - 3, flags);     }

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("หมายเหตุ:", famt2, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString("เพื่อประโยชน์และความสะดวกของท่าน  กรุณามาให้ตรงตามวัน และเวลาที่แพทย์นัดทุกครั้ง", famt1, Brushes.Black, col21, yPos, flags);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("กรณีที่ไม่สามารถมาตรวจตามนัดได้  กรุณาโทรเพื่อแจ้งยกเลิกหรือเลื่อนนัดกับทางโรงพยาบาลทุกครั้ง", famt1, Brushes.Black, col21, yPos, flags);
            g.Dispose();
            Brush.Dispose();
            blackPen.Dispose();
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
        private void setGrfOrderLab() 
        {
            grfOperLab.Rows.Count = 1;
            grfOperLab.Cols.Count = 11;
            grfOperLab.Cols[colLabResult].Visible = false;
            grfOperLab.Cols[colInterpret].Visible = false;
            grfOperLab.Cols[colNormal].Visible = false;
            grfOperLab.Cols[colUnit].Visible = false;
            grfOperLab.Cols[colLabCode].Visible = false;
            grfOperLab.Cols[colLabReqDate].Visible = false;
            grfOperLab.Cols[colLabDate].Visible = false;
            grfOperLab.Cols[colLabNameSub].Visible = false;
            grfOperLab.Cols[colLabReqNo].AllowEditing = false;
            DataTable dt = new DataTable();
            dt = bc.bcDB.labT02DB.selectbyHN(txtOperHN.Text.Trim(), VSDATE, PRENO);
            if(dt.Rows.Count >= 0)
            {
                grfOperLab.Rows.Count = dt.Rows.Count + 1;
                int i = 1;
                foreach (DataRow row1 in dt.Rows)
                {
                    try
                    {
                        Row rowa = grfOperLab.Rows[i];
                        rowa[colLabCode] = row1["order_code"].ToString();
                        rowa[colLabName] = row1["order_name"].ToString();
                        //rowa[colLabNameSub] = row1["MNC_LABT2_STATUS"].ToString();
                        rowa[colLabReqNo] = row1["req_no"].ToString();
                        rowa[colLabReqDate] = row1["req_date"].ToString();
                        rowa[0] = i.ToString();
                        rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        lfSbMessage.Text = ex.Message;
                        new LogWriter("e", "FrmOPD setGrfOrderLab " + ex.Message);
                        bc.bcDB.insertLogPage(bc.userId, this.Name, "setGrfOrderLab ", ex.Message);
                    }
                }
            }
        }
        private void setGrfOrderTemp()
        {//ดึงจาก table temp_order
            lstAutoComplete.Items.Clear();
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectOrderTempByHN(txtOperHN.Text.Trim(), VSDATE, PRENO);
            int i = 1, j = 1;
            grfOrder.Rows.Count = 1;
            grfOrder.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                try
                {
                    Row rowa = grfOrder.Rows[i];
                    rowa[colgrfOrderCode] = row1["order_code"].ToString();
                    rowa[colgrfOrderName] = row1["order_name"].ToString();
                    rowa[colgrfOrderQty] = row1["qty"].ToString();
                    rowa[colgrfOrderStatus] = row1["flag"].ToString();
                    rowa[colgrfOrderID] = row1["id"].ToString();
                    rowa[colgrfOrderReqNO] = "";
                    rowa[colgrfOrdFlagSave] = "1";//ดึงข้อมูลจาก table temp_order 
                    rowa[colgrfOrdControlRemark] = row1["control_remark"].ToString();
                    rowa[colgrfOrdControlYear] = row1["control_year"].ToString();
                    rowa[colgrfOrdStatusControl] = row1["status_control"].ToString();
                    rowa[colgrfOrdPassSupervisor] = row1["pass_supervisor"].ToString();
                    rowa[colgrfOrdSupervisor] = row1["supervisor"].ToString();
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
            if (e.KeyCode == Keys.Enter)
            {
                //lbApmDtrName.Text = bc.selectDoctorName(txtApmDtr.Text.Trim());
                setApmCnt();
            }
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
                if (bc.USERCONFIRMID.Length <= 0)
                {
                    lfSbMessage.Text = "Password ไม่ถูกต้อง";
                    return;
                }
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
                year = VSDATE.Substring(0, 4);
                mon = VSDATE.Substring(5, 2);
                day = VSDATE.Substring(8, 2);
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
                if (File.Exists(filePathS))
                {
                    if (!IsFileLocked(filePathS))
                    {
                        File.Delete(filePathS);
                    }
                }
                err = "08";
                if (File.Exists(filePathR))
                {
                    if (!IsFileLocked(filePathR))
                    {
                        File.Delete(filePathR);
                    }
                }
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
                lfSbMessage.Text = err + " saveImgStaffNote " + ex.Message;
                new LogWriter("e", "FrmOPD saveImgStaffNote " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "saveImgStaffNote", ex.Message);
                if (bc.iniC.statusShowMessageError.Equals(""))
                {
                    MessageBox.Show("saveImgStaffNote " + ex.Message, "");
                }
            }
        }
        private bool IsFileLocked(string filePath)
        {
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
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
            if (template.Equals("checkup_doe"))
            {
                if (txtCheckUPHN.Text.Length <= 0) { return; }
            }
            else
            {
                if (txtOperHN.Text.Length <= 0) { return; }
            }
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
        private void Document_PrintPageStaffNote(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "", prndob = "";
            Decimal amt1 = 0, voucamt = 0, discount = 0, total = 0, cash = 0;
            float yPos = 10, yAdj=0, gap = 6, colName = 0, col2 = 5, col3 = 250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 820, col40 = 620;
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
            col2 = 65;            col3 = 320;            col4 = 880;            col40 = 650;
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
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col3 + 25, yPos , flags);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col4 + 30, yPos , flags);

            line = "ชื่อ " + PTT.Name;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col3, yPos + 20, flags);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col4+15, yPos + 20, flags);
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
                string txtchronic = "";int cnt = 0;float yPos1 = yPos+60;
                foreach (DataRow row in DTCHRONIC.Rows)
                {
                    txtchronic = "";
                    if (cnt == 0) { txtchronic = "โรคประจำตัว "+row["CHRONICCODE"].ToString() + " " + row["MNC_CRO_DESC"].ToString(); }
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
                e.Graphics.DrawString("[ ]ยังไม่พบ", fStaffN, Brushes.Black, col2+70, yPos + 60, flags);
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
            date = bc.datetoShow1(VS.VisitDate)+" "+bc.showTime(VS.VisitTime);
            line = "วันที่เวลา " + date;
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col3, yPos + 80, flags);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col4 + 15, yPos + 80, flags);
            if (VS.DoctorId.Equals("00000"))            {                line = "ชื่อแพทย์ " + VS.DoctorId + " แพทย์ไม่ระบุชื่อ" ;            }
            else            {                line = "ชื่อแพทย์ " + VS.DoctorId + " " + VS.DoctorName;            }
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col3, yPos + 100, flags);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col4 - 50, yPos + 120, flags);

            line = "อาการเบื้องต้น " + VS.symptom.Replace("\r\n","");
            if (VS.symptom.Length > 60) {line = VS.symptom.Substring(0, 60); line = line.Replace("\r\n", ","); e.Graphics.DrawString(line, fStaffNs, Brushes.Black, col2, yPos + 120, flags); e.Graphics.DrawString(line, fStaffNs, Brushes.Black, col40, yPos + 100, flags);            }
            else {e.Graphics.DrawString(line.Replace("เบื้องต้น",""), fStaffN, Brushes.Black, col2, yPos + 120, flags);                e.Graphics.DrawString(line, fStaffN, Brushes.Black, col40, yPos + 100, flags);            }
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
            e.Graphics.DrawString("Pain score "+txtOperPain.Text.Trim()+ " O₂" + txtOperO2.Text.Trim(), fStaffN, Brushes.Black, col2 + 360, yPos + 250, flags);

            if (TEMPLATESTAFFNOTE.Equals("0") || TEMPCANCER)
            {
                //e.Graphics.DrawString("อาการ " + VS.symptom, fStaffN, Brushes.Black, col3 + 40, yPos + 315, flags);
                //e.Graphics.DrawString("อาการ " + VS.symptom, fStaffN, Brushes.Black, col4 + 20, yPos + 350, flags);
                if (TEMPLATESTAFFNOTE.Equals("1"))
                {
                    //AC Doxorubicin-Cyclophosphamide
                    e.Graphics.DrawString("Premedication", fStaffNB, Brushes.Black, col40, yPos + 400, flags);
                    e.Graphics.DrawString("- Onsea 8 mg iv", fStaffN, Brushes.Black, col40+10, yPos + 420, flags);
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
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2+5, yPos + 730, flags);
            line = "ลงชื่อพยาบาล: _____________________________________";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fStaffN, Brushes.Black, col2+5, yPos + 750, flags);
            line = "FM-REC-002 (00 10/09/53)(1/1)";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            Font font1 = new Font(fStaffN.Name, fStaffN.Size-2, FontStyle.Regular);
            e.Graphics.DrawString(line, font1, Brushes.Black, col2, yPos + 770, flags);
            e.Graphics.DrawString(line, font1, Brushes.Black, col40, yPos + 770, flags);
            g.Dispose();
            Brush.Dispose();
            blackPen.Dispose();
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
                e.Graphics.DrawString("อายุ " + PTT.AgeStringOK1DOT() +" ["+PTT.patient_birthday+"]", fque, Brushes.Black, 0, yPos + 70, flags);
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
                if (DTCHRONIC== null) { DTCHRONIC = bc.bcDB.vsDB.SelectChronicByPID(PTT.idcard); }
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
                new LogWriter("e", this.Name+" Document_PrintPageQueDtr " +err + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "Document_PrintPageQueDtr", err+ex.Message);
            }
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
                setGrfOPD();
            }
            else if (tCOrder.SelectedTab == tabOrder)   {       setGrfOPD();    setGrfOrderLab();   setGrfOrderTemp();}
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
            if (e.KeyCode == Keys.Enter)
            {
                txtOperDtr.Focus();
            }
        }

        private void TxtOperCcin_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperCcex.Focus();
            }
        }

        private void TxtOperCc_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperCcin.Focus();
            }
        }

        private void TxtOperHc_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperCc.Focus();
            }
        }

        private void TxtOperHt_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperHc.Focus();
            }
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
            if (e.KeyCode == Keys.Enter)
            {
                txtOperWt.Focus();
            }
        }

        private void TxtBp2R_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperAbc.Focus();
            }
        }

        private void TxtBp2L_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperBp2R.Focus();
            }
        }

        private void TxtBp1R_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperWt.Focus();
            }
        }

        private void TxtBp1L_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperBp1R.Focus();
            }
        }

        private void TxtOperRh_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperBp1L.Focus();
            }
        }

        private void TxtOperAbo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperRh.Focus();
            }
        }

        private void TxtOperRrate_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperBp1L.Focus();
            }
        }

        private void TxtOperHrate_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperRrate.Focus();
            }
        }
        private void TxtOperTemp_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                txtOperHrate.Focus();
            }
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
            if(!TC1Active.Equals(tabOper.Name)) timeOperList.Stop();
            if (tC1.SelectedTab == tabCheckUP)
            {
                tabMedScanActiveNOtabOutLabActive = false;
                TC1Active = tabCheckUP.Name;
                setGrfCheckUPList("");
                txtSBSearchHN.Visible = false;
                btnSBSearch.Visible = false;
                btnScanSaveImg.Visible = true;
                btnScanGetImg.Visible = true;
                btnScanClearImg.Visible = true;
                btnOperClose.Visible = true;

                txtSBSearchDate.Visible = true;
                rb1.Visible = true;
                rb2.Visible = true;
                rgSbModule.Visible = true;
            }
            else if (tC1.SelectedTab == tabOper)
            {
                tabMedScanActiveNOtabOutLabActive = false;
                TC1Active = tabOper.Name;
                txtSBSearchHN.Visible = true;
                txtSBSearchDate.Visible = false;
                btnSBSearch.Visible = false;
                btnScanSaveImg.Visible = true;
                btnScanGetImg.Visible = true;
                btnScanClearImg.Visible = true;
                btnOperClose.Visible = true;

                rb1.Visible = true;
                rb2.Visible = true;
                rgSbModule.Visible = true;
                timeOperList.Start();
                setGrfOperList("");
            }
            else if (tC1.SelectedTab == tabFinish)
            {
                tabMedScanActiveNOtabOutLabActive = false;
                TC1Active = tabFinish.Name;
                txtSBSearchHN.Visible = false;
                txtSBSearchDate.Visible = false;
                btnSBSearch.Visible = false;
                btnScanSaveImg.Visible = false;
                btnScanGetImg.Visible = false;
                btnScanClearImg.Visible = false;
                btnOperClose.Visible = false;

                rb1.Visible = true;
                rb2.Visible = true;
                rgSbModule.Visible = true;
                setGrfOperFinish();
                tCFinish.SelectedTab = tabFinishStaffNote;
            }
            else if (tC1.SelectedTab == tabApm)
            {
                tabMedScanActiveNOtabOutLabActive = false;
                TC1Active = tabApm.Name;
                txtSBSearchHN.Visible = false;
                txtSBSearchDate.Visible = false;
                btnSBSearch.Visible = false;
                btnScanSaveImg.Visible = false;
                btnScanGetImg.Visible = false;
                btnScanClearImg.Visible = false;
                btnOperClose.Visible = false;

                rb1.Visible = true;
                rb2.Visible = true;
                rgSbModule.Visible = true;
            }
            else if (tC1.SelectedTab == tabMedScan)
            {
                tabMedScanActiveNOtabOutLabActive = true;
                TC1Active = tabMedScan.Name;
                txtSBSearchHN.Visible = true;
                txtSBSearchDate.Visible = false;
                btnSBSearch.Visible = true;
                btnScanSaveImg.Visible = false;
                btnScanGetImg.Visible = false;
                btnScanClearImg.Visible = false;
                btnOperClose.Visible = false;

                rb1.Visible = true;
                rb2.Visible = true;
                rgSbModule.Visible = true;
            }
            else if (tC1.SelectedTab == tabSearch)
            {
                tabMedScanActiveNOtabOutLabActive = false;
                TC1Active = tabSearch.Name;
                txtSrcHn.Focus();
                txtSBSearchHN.Visible = false;
                txtSBSearchDate.Visible = false;
                btnSBSearch.Visible = false;
                btnScanSaveImg.Visible = bc.iniC.authenedit.Equals("1") ? true : false;       //ต้องการให้ upload file scan ได้    68-12-01
                btnScanGetImg.Visible = bc.iniC.authenedit.Equals("1") ? true: false;       //ต้องการให้ upload file scan ได้      68-12-01
                btnScanClearImg.Visible = false;
                btnOperClose.Visible = false;

                rb1.Visible = true;
                rb2.Visible = true;
                rgSbModule.Visible = true;
            }
            else if (tC1.SelectedTab == tabOutLabDate)//date search
            {
                tabMedScanActiveNOtabOutLabActive = false;
                TC1Active = tabOutLabDate.Name;
                txtSBSearchHN.Visible = false;
                txtSBSearchDate.Visible = true;
                btnSBSearch.Visible = true;
                btnScanSaveImg.Visible = false;
                btnScanGetImg.Visible = false;
                btnScanClearImg.Visible = false;
                btnOperClose.Visible = false;

                rb1.Visible = true;
                rb2.Visible = true;
                rgSbModule.Visible = true;
            }
            else if (tC1.SelectedTab == tabOutlab)//hn search
            {
                setGrfTodayOutLab();
                tabMedScanActiveNOtabOutLabActive = false;
                TC1Active = tabOutlab.Name;
                txtSBSearchHN.Visible = true;
                txtSBSearchDate.Visible = false;
                btnSBSearch.Visible = true;
                btnScanSaveImg.Visible = false;
                btnScanGetImg.Visible = false;
                btnScanClearImg.Visible = false;
                btnOperClose.Visible = false;

                rb1.Visible = true;
                rb2.Visible = true;
                rgSbModule.Visible = true;
            }
            else if (tC1.SelectedTab == tabStaffNote)
            {
                tabMedScanActiveNOtabOutLabActive = false;
                TC1Active = tabStaffNote.Name;
                txtSBSearchHN.Visible = false;
                txtSBSearchDate.Visible = true;
                btnSBSearch.Visible = true;
                btnScanSaveImg.Visible = false;
                btnScanGetImg.Visible = false;
                btnScanClearImg.Visible = false;
                btnOperClose.Visible = false;

                rb1.Visible = true;
                rb2.Visible = true;
                rgSbModule.Visible = true;
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
            else
            {
                txtSBSearchHN.Visible = false;
                rb1.Visible = false;
                btnSBSearch.Visible = false;
            }
        }
        /*
         * grf company map package
         */
        private void initGrfMapPackage()
        {
            grfMapPackage = new C1FlexGrid();
            grfMapPackage.Font = fEdit;
            grfMapPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            grfMapPackage.Location = new System.Drawing.Point(0, 0);
            grfMapPackage.Rows.Count = 1;
            grfMapPackage.Cols.Count = 5;
            grfMapPackage.Cols[colgrfMapPackageCompCode].Width = 70;
            grfMapPackage.Cols[colgrfMapPackageCompName].Width = 300;
            grfMapPackage.ShowCursor = true;
            grfMapPackage.Cols[colgrfMapPackageCompCode].Caption = "CODE";
            grfMapPackage.Cols[colgrfMapPackageCompName].Caption = "Company Name";

            grfMapPackage.Cols[colgrfMapPackageCompCode].DataType = typeof(String);
            grfMapPackage.Cols[colgrfMapPackageCompName].DataType = typeof(String);

            grfMapPackage.Cols[colgrfMapPackageCompCode].TextAlign = TextAlignEnum.LeftCenter;
            grfMapPackage.Cols[colgrfMapPackageCompName].TextAlign = TextAlignEnum.LeftCenter;
            grfMapPackage.Cols[colgrfMapPackageCompCode].AllowEditing = false;
            grfMapPackage.Cols[colgrfMapPackageCompName].AllowEditing = false;
            pnMapPackageComp.Controls.Add(grfMapPackage);
            grfMapPackage.Click += GrfMapPackage_Click;
            theme1.SetTheme(grfMapPackage, bc.iniC.themeApp);
        }

        private void GrfMapPackage_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfMapPackage.Row <= 0) return;
            String compcode = grfMapPackage[grfMapPackage.Row, colgrfMapPackageCompCode].ToString();
            String compname = grfMapPackage[grfMapPackage.Row, colgrfMapPackageCompName].ToString();
            String txt = "";
            if (compname.Length > 7)            {                txt = compname.Substring(0, 6);            }
            else if (compname.Length > 5) { txt = compname.Substring(0, 4); }
            txtMapPackageCompCode.Value = compcode;
            txtMapPackageCompName.Value = compname;
            txtMapPackagePackagesearch.Value = txt;
        }

        private void setGrfMapPackage(String comcode)
        {
            DataTable dt = new DataTable();
            String vn = "", vsdate = "", an = "";
            grfMapPackage.Rows.Count = 1;
            dt = bc.bcDB.pm24DB.selectCustByName(comcode);
            int i = 0;
            grfMapPackage.Rows.Count = dt.Rows.Count + 1;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    Row rowa = grfMapPackage.Rows[i];
                    rowa[colgrfMapPackageCompCode] = row1["MNC_COM_CD"].ToString();
                    rowa[colgrfMapPackageCompName] = row1["MNC_COM_DSC"].ToString();
                    row1[0] = i;
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", this.Name+" setGrfMapPackage " + ex.Message);
            }
        }
        private void initGrfMapPackageItmes()
        {
            grfpackageitems = new C1FlexGrid();
            grfpackageitems.Font = fEdit;
            grfpackageitems.Dock = System.Windows.Forms.DockStyle.Fill;
            grfpackageitems.Location = new System.Drawing.Point(0, 0);
            grfpackageitems.Rows.Count = 1;
            grfpackageitems.Cols.Count = 6;
            grfpackageitems.Cols[colgrfPackageCode].Width = 70;
            grfpackageitems.Cols[colgrfPackageName].Width = 300;
            grfpackageitems.ShowCursor = true;
            grfpackageitems.Cols[colgrfPackageCode].Caption = "CODE";
            grfpackageitems.Cols[colgrfPackageName].Caption = "Package Name";
            grfpackageitems.Cols[colgrfPackageCode].DataType = typeof(String);
            grfpackageitems.Cols[colgrfPackageName].DataType = typeof(String);
            grfpackageitems.Cols[colgrfPackageCode].TextAlign = TextAlignEnum.LeftCenter;
            grfpackageitems.Cols[colgrfPackageName].TextAlign = TextAlignEnum.LeftCenter;
            grfpackageitems.Cols[colgrfPackageCode].AllowEditing = false;
            grfpackageitems.Cols[colgrfPackageName].AllowEditing = false;
            pnPackage.Controls.Add(grfpackageitems);//pnMapPackage
            theme1.SetTheme(grfpackageitems, bc.iniC.themeApp);
        }
        private void setGrfPackageItems(String packcode)
        {
            DataTable dt = new DataTable();
            String vn = "", vsdate = "", an = "";
            grfpackageitems.Rows.Count = 1;
            dt = bc.bcDB.pm40DB.selectByPackCode(packcode);
            int i = 0;
            grfpackageitems.Rows.Count = dt.Rows.Count + 1;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    Row rowa = grfpackageitems.Rows[i];
                    String type = row1["MNC_OPR_FLAG"].ToString(), name="";
                    name = type.Equals("L") ? row1["MNC_LB_DSC"].ToString() : type.Equals("O") ? row1["MNC_SR_DSC"].ToString()
                        : type.Equals("X") ? row1["MNC_XR_DSC"].ToString() : type.Equals("F") ? row1["MNC_DF_DSC"].ToString() : "";
                    rowa[colgrfPackageCode] = row1["MNC_OPR_CD"].ToString();
                    rowa[colgrfPackageName] = name;
                    //rowa[colgrfViewhelpPttName] = row1["fullnamet"].ToString();
                    //rowa[colgrfViewhelpVsdate] = row1["MNC_DATE"].ToString();
                    //rowa[colgrfViewhelpPackType] = row1["MNC_PAC_TYP"].ToString();
                    row1[0] = i;
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", this.Name + " setGrfMapPackageViewhelp " + ex.Message);
            }
        }
        private void initGrfMapPackageViewhelp()
        {
            grfMapPackageViewhelp = new C1FlexGrid();
            grfMapPackageViewhelp.Font = fEdit;
            grfMapPackageViewhelp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfMapPackageViewhelp.Location = new System.Drawing.Point(0, 0);
            grfMapPackageViewhelp.Rows.Count = 1;
            grfMapPackageViewhelp.Cols.Count = 6;
            grfMapPackageViewhelp.Cols[colgrfPackageCode].Width = 70;
            grfMapPackageViewhelp.Cols[colgrfPackageName].Width = 300;
            grfMapPackageViewhelp.ShowCursor = true;
            grfMapPackageViewhelp.Cols[colgrfPackageCode].Caption = "CODE";
            grfMapPackageViewhelp.Cols[colgrfPackageName].Caption = "Package Name";
            grfMapPackageViewhelp.Cols[colgrfPackageCode].DataType = typeof(String);
            grfMapPackageViewhelp.Cols[colgrfPackageName].DataType = typeof(String);
            grfMapPackageViewhelp.Cols[colgrfPackageCode].TextAlign = TextAlignEnum.LeftCenter;
            grfMapPackageViewhelp.Cols[colgrfPackageName].TextAlign = TextAlignEnum.LeftCenter;
            grfMapPackageViewhelp.Cols[colgrfPackageCode].AllowEditing = false;
            grfMapPackageViewhelp.Cols[colgrfPackageName].AllowEditing = false;
            pnMapPackageViewHelp.Controls.Add(grfMapPackageViewhelp);//pnMapPackage
            theme1.SetTheme(grfMapPackageViewhelp, bc.iniC.themeApp);
        }
        private void setGrfMapPackageViewhelp(String comcode)
        {
            DataTable dt = new DataTable();
            String vn = "", vsdate = "", an = "";
            grfMapPackageViewhelp.Rows.Count = 1;
            dt = bc.bcDB.pm39DB.selectViewHelpByCompCode(comcode);
            int i = 0;
            grfMapPackageViewhelp.Rows.Count = dt.Rows.Count + 1;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    Row rowa = grfMapPackageViewhelp.Rows[i];
                    rowa[colgrfPackageCode] = row1["MNC_PAC_CD"].ToString();
                    rowa[colgrfPackageName] = row1["MNC_PAC_DSC"].ToString();
                    rowa[colgrfViewhelpPttName] = row1["fullnamet"].ToString();
                    rowa[colgrfViewhelpVsdate] = row1["MNC_DATE"].ToString();
                    rowa[colgrfViewhelpPackType] = row1["MNC_PAC_TYP"].ToString();
                    row1[0] = i;
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", this.Name + " setGrfMapPackageViewhelp " + ex.Message);
            }
        }
        /*
         * grf package
         */
        private void initGrfpackage()
        {
            grfPackage = new C1FlexGrid();
            grfPackage.Font = fEdit;
            grfPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPackage.Location = new System.Drawing.Point(0, 0);
            grfPackage.Rows.Count = 1;
            grfPackage.Cols.Count = 6;
            grfPackage.Cols[colgrfPackageCode].Width = 70;
            grfPackage.Cols[colgrfPackageName].Width = 300;
            grfPackage.ShowCursor = true;
            grfPackage.Cols[colgrfMapPackageCompCode].Caption = "CODE";
            grfPackage.Cols[colgrfPackageName].Caption = "Package Name";
            grfPackage.Cols[colgrfPackageCompCode].Caption = "Comp Code";

            grfPackage.Cols[colgrfPackageCode].DataType = typeof(String);
            grfPackage.Cols[colgrfPackageName].DataType = typeof(String);
            grfPackage.Cols[colgrfPackageCompCode].DataType = typeof(String);

            grfPackage.Cols[colgrfPackageCode].TextAlign = TextAlignEnum.LeftCenter;
            grfPackage.Cols[colgrfPackageName].TextAlign = TextAlignEnum.LeftCenter;
            grfPackage.Cols[colgrfPackageCompCode].TextAlign = TextAlignEnum.CenterCenter;
            grfPackage.Cols[colgrfPackageCode].AllowEditing = false;
            grfPackage.Cols[colgrfPackageName].AllowEditing = false;
            grfPackage.Cols[colgrfPackageCompCode].AllowEditing = false;
            pnMapPackage.Controls.Add(grfPackage);//pnMapPackage
            grfPackage.Click += GrfPackage_Click;
            theme1.SetTheme(grfPackage, bc.iniC.themeApp);
        }

        private void GrfPackage_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfPackage.Row <= 0) return;
            txtMapPackagepackageCode.Value = grfPackage[grfPackage.Row,colgrfPackageCode]!=null? grfPackage[grfPackage.Row, colgrfPackageCode].ToString():"";
            txtMapPackagepackageName.Value = grfPackage[grfPackage.Row, colgrfPackageName] != null ? grfPackage[grfPackage.Row, colgrfPackageName].ToString() : "";
            txtMapPackagepackageType.Value = grfPackage[grfPackage.Row, colgrfPackageType] != null ? grfPackage[grfPackage.Row, colgrfPackageType].ToString() : "";
            setGrfPackageItems(txtMapPackagepackageCode.Text.Trim());


        }
        private void setGrfPackage(String comcode, String flagcode)
        {
            DataTable dt = new DataTable();
            String vn = "", vsdate = "", an = "";
            grfPackage.Rows.Count = 1;
            dt = flagcode.Equals("code") ? bc.bcDB.pm39DB.selectByCompCode(comcode) : bc.bcDB.pm39DB.selectByCompName(comcode);
            int i = 0;
            grfPackage.Rows.Count = dt.Rows.Count + 1;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    Row rowa = grfPackage.Rows[i];
                    rowa[colgrfPackageCode] = row1["MNC_PAC_CD"].ToString();
                    rowa[colgrfPackageName] = row1["MNC_PAC_DSC"].ToString();
                    rowa[colgrfPackageType] = row1["MNC_PAC_TYP"].ToString();
                    rowa[colgrfPackagePrice] = row1["MNC_PAC_PRI"].ToString();
                    rowa[colgrfPackageCompCode] = row1["MNC_COM_CD"].ToString();
                    row1[0] = i;
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", this.Name + " setGrfMapPackage " + ex.Message);
            }
        }
        private void initGrfSrcProcedure()
        {
            grfSrcProcedure = new C1FlexGrid();
            grfSrcProcedure.Font = fEdit;
            grfSrcProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSrcProcedure.Location = new System.Drawing.Point(0, 0);
            grfSrcProcedure.Rows.Count = 1;
            grfSrcProcedure.Cols.Count = 5;
            grfSrcProcedure.Cols[colHisProcCode].Width = 100;
            grfSrcProcedure.Cols[colHisProcName].Width = 200;
            grfSrcProcedure.Cols[colHisProcReqDate].Width = 100;
            grfSrcProcedure.Cols[colHisProcReqTime].Width = 100;

            grfSrcProcedure.ShowCursor = true;
            grfSrcProcedure.Cols[colHisProcCode].Caption = "CODE";
            grfSrcProcedure.Cols[colHisProcName].Caption = "Procedure Name";
            grfSrcProcedure.Cols[colHisProcReqDate].Caption = "req date";
            grfSrcProcedure.Cols[colHisProcReqTime].Caption = "req time";

            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grfSrcProcedure.Cols[colHisProcCode].DataType = typeof(String);
            grfSrcProcedure.Cols[colHisProcName].DataType = typeof(String);
            grfSrcProcedure.Cols[colHisProcReqDate].DataType = typeof(String);
            grfSrcProcedure.Cols[colHisProcReqTime].DataType = typeof(String);

            grfSrcProcedure.Cols[colHisProcCode].TextAlign = TextAlignEnum.CenterCenter;
            grfSrcProcedure.Cols[colHisProcName].TextAlign = TextAlignEnum.LeftCenter;
            grfSrcProcedure.Cols[colHisProcReqDate].TextAlign = TextAlignEnum.CenterCenter;
            grfSrcProcedure.Cols[colHisProcReqTime].TextAlign = TextAlignEnum.CenterCenter;

            grfSrcProcedure.Cols[colgrfOrderCode].Visible = true;
            grfSrcProcedure.Cols[colgrfOrderName].Visible = true;
            grfSrcProcedure.Cols[colHisProcReqTime].Visible = false;

            grfSrcProcedure.Cols[colHisProcCode].AllowEditing = false;
            grfSrcProcedure.Cols[colHisProcName].AllowEditing = false;
            grfSrcProcedure.Cols[colHisProcReqDate].AllowEditing = false;
            grfSrcProcedure.Cols[colHisProcReqTime].AllowEditing = false;

            pnSrcProcedure.Controls.Add(grfSrcProcedure);
            theme1.SetTheme(grfSrcProcedure, bc.iniC.themeApp);
        }
        private void setGrfSrcProcedure(String vsDate, String preno)
        {
            DataTable dt = new DataTable();
            String vn = "", vsdate = "", an = "";
            grfSrcProcedure.Rows.Count = 1;
            dt = bc.bcDB.pt16DB.SelectProcedureByVisit(HN, preno, vsDate);
            int i = 0;
            grfSrcProcedure.Rows.Count = dt.Rows.Count + 1;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    Row rowa = grfSrcProcedure.Rows[i];
                    //rowa[colHisProcReqTime] = row1["MNC_REQ_TIM"].ToString();
                    rowa[colHisProcReqDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
                    rowa[colHisProcName] = row1["MNC_SR_DSC"].ToString();
                    rowa[colHisProcCode] = row1["MNC_SR_CD"].ToString();
                    row1[0] = i;
                }
                if (dt.Rows.Count == 1)
                {
                    //setXrayResult();
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmScanView1 setGrfHisProcedure " + ex.Message);
            }
        }
        private void initGrfProcedure()
        {
            grfHisProcedure = new C1FlexGrid();
            grfHisProcedure.Font = fEdit;
            grfHisProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHisProcedure.Location = new System.Drawing.Point(0, 0);
            grfHisProcedure.Rows.Count = 1;
            grfHisProcedure.Cols.Count = 5;
            grfHisProcedure.Cols[colHisProcCode].Width = 100;
            grfHisProcedure.Cols[colHisProcName].Width = 200;
            grfHisProcedure.Cols[colHisProcReqDate].Width = 100;
            grfHisProcedure.Cols[colHisProcReqTime].Width = 200;

            grfHisProcedure.ShowCursor = true;
            grfHisProcedure.Cols[colHisProcCode].Caption = "CODE";
            grfHisProcedure.Cols[colHisProcName].Caption = "Procedure Name";
            grfHisProcedure.Cols[colHisProcReqDate].Caption = "req date";
            grfHisProcedure.Cols[colHisProcReqTime].Caption = "req time";

            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grfHisProcedure.Cols[colHisProcCode].DataType = typeof(String);
            grfHisProcedure.Cols[colHisProcName].DataType = typeof(String);
            grfHisProcedure.Cols[colHisProcReqDate].DataType = typeof(String);
            grfHisProcedure.Cols[colHisProcReqTime].DataType = typeof(String);

            grfHisProcedure.Cols[colHisProcCode].TextAlign = TextAlignEnum.CenterCenter;
            grfHisProcedure.Cols[colHisProcName].TextAlign = TextAlignEnum.LeftCenter;
            grfHisProcedure.Cols[colHisProcReqDate].TextAlign = TextAlignEnum.CenterCenter;
            grfHisProcedure.Cols[colHisProcReqTime].TextAlign = TextAlignEnum.CenterCenter;

            grfHisProcedure.Cols[colHisProcCode].Visible = true;
            grfHisProcedure.Cols[colHisProcName].Visible = true;
            grfHisProcedure.Cols[colHisProcReqTime].Visible = false;

            grfHisProcedure.Cols[colHisProcCode].AllowEditing = false;
            grfHisProcedure.Cols[colHisProcName].AllowEditing = false;
            grfHisProcedure.Cols[colHisProcReqDate].AllowEditing = false;
            grfHisProcedure.Cols[colHisProcReqTime].AllowEditing = false;

            pnHisProcedure.Controls.Add(grfHisProcedure);
            theme1.SetTheme(grfHisProcedure, bc.iniC.themeApp);
        }
        private void setGrfHisProcedure(String hn,String vsDate, String preno,ref C1FlexGrid grf)
        {
            DataTable dt = new DataTable();
            String vn = "", vsdate = "", an = "";
            grf.Rows.Count = 1;
            dt = bc.bcDB.pt16DB.SelectProcedureByVisit(hn, preno, vsDate);
            int i = 0;
            grf.Rows.Count = dt.Rows.Count + 1;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    Row rowa = grf.Rows[i];
                    //rowa[colHisProcReqTime] = row1["MNC_REQ_TIM"].ToString();
                    rowa[colHisProcReqDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
                    rowa[colHisProcName] = row1["MNC_SR_DSC"].ToString();
                    rowa[colHisProcCode] = row1["MNC_SR_CD"].ToString();
                    row1[0] = i;
                }
                if (dt.Rows.Count == 1)
                {
                    //setXrayResult();
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FRMOPD GrfSrcVs_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FRMOPD setGrfHisProcedure  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
        }
        private void initGrfSrc()
        {//tab ค้นหา
            grfSrc = new C1FlexGrid();
            grfSrc.Font = fEdit;
            grfSrc.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSrc.Location = new System.Drawing.Point(0, 0);
            grfSrc.Rows.Count = 1;
            grfSrc.Cols.Count = 10;
            grfSrc.Cols[colgrfSrcHn].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcFullNameT].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcPID].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcDOB].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcHn].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcFullNameT].TextAlign = TextAlignEnum.LeftCenter;
            grfSrc.Cols[colgrfSrcPID].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcDOB].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcAge].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcVisitReleaseIPD].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcHn].Width = 100;
            grfSrc.Cols[colgrfSrcFullNameT].Width = 300;
            grfSrc.Cols[colgrfSrcPID].Width = 150;
            grfSrc.Cols[colgrfSrcDOB].Width = 90;
            grfSrc.Cols[colgrfSrcPttid].Width = 60;
            grfSrc.Cols[colgrfSrcAge].Width = 90;
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].Width = 90;
            grfSrc.Cols[colgrfSrcVisitReleaseIPD].Width = 90;
            grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].Width = 90;
            grfSrc.ShowCursor = true;
            grfSrc.Cols[colgrfSrcHn].Caption = "hn";
            grfSrc.Cols[colgrfSrcFullNameT].Caption = "full name";
            grfSrc.Cols[colgrfSrcPID].Caption = "PID";
            grfSrc.Cols[colgrfSrcDOB].Caption = "DOB";
            grfSrc.Cols[colgrfSrcPttid].Caption = "";
            grfSrc.Cols[colgrfSrcAge].Caption = "AGE";
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].Caption = "มาล่าสุดOPD";
            grfSrc.Cols[colgrfSrcVisitReleaseIPD].Caption = "มาล่าสุดIPD";
            grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].Caption = "กลับบ้านIPD";

            grfSrc.Cols[colgrfSrcHn].Visible = true;
            grfSrc.Cols[colgrfSrcFullNameT].Visible = true;
            grfSrc.Cols[colgrfSrcPID].Visible = true;
            grfSrc.Cols[colgrfSrcDOB].Visible = true;
            grfSrc.Cols[colgrfSrcPttid].Visible = false;
            grfSrc.Cols[colgrfSrcAge].Visible = true;

            grfSrc.Cols[colgrfSrcHn].AllowEditing = false;
            grfSrc.Cols[colgrfSrcFullNameT].AllowEditing = false;
            grfSrc.Cols[colgrfSrcPID].AllowEditing = false;
            grfSrc.Cols[colgrfSrcDOB].AllowEditing = false;
            grfSrc.Cols[colgrfSrcAge].AllowEditing = false;
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].AllowEditing = false;
            grfSrc.Cols[colgrfSrcVisitReleaseIPD].AllowEditing = false;
            grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].AllowEditing = false;
            grfSrc.AllowFiltering = true;

            grfSrc.Click += GrfSrc_Click;
            //grfSrc.AutoSizeCols();
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnSrcGrf.Controls.Add(grfSrc);
            theme1.SetTheme(grfSrc, bc.iniC.themeApp);
        }
        private void GrfSrc_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfSrc.Row <= 0) return;
            if (grfSrc.Col <= 0) return;
            HN = grfSrc[grfSrc.Row, colgrfSrcHn] != null ? grfSrc[grfSrc.Row, colgrfSrcHn].ToString() : "";
            //PRENO = grfSrc[grfSrc.Row, colgrfSrcHn] != null ? grfSrc[grfSrc.Row, colgrfSrcHn].ToString() : "";
            //VSDATE = grfSrc[grfSrc.Row, colgrfSrc] != null ? grfSrc[grfSrc.Row, colgrfSrcHn].ToString() : "";

            setControlPatientByGrf(HN);
        }
        private void setControlPatientByGrf(String hn)
        {
            setGrfSrcVs(hn);
            lfSbStatus.Text = "";
            lfSbMessage.Text = "";
            lbSrcPreno.Value = "";
            lbSrcVsDate.Value = "";
            lbSrcVN.Value = "";
            setGrfDocOLD();
            setGrfEKG();
            setGrfEST();
            setGrfECHO();
            setGrfHolter();
            setGrfCertMed();
        }
        private void initGrfSrcVs()
        {//ใช้เหมือน grfOPD
            grfSrcVs = new C1FlexGrid();
            grfSrcVs.Font = fEdit;
            grfSrcVs.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSrcVs.Location = new System.Drawing.Point(0, 0);
            grfSrcVs.Rows.Count = 1;
            grfSrcVs.Cols.Count = 27;
            grfSrcVs.Cols[colVsVsDate].Width = 72;
            grfSrcVs.Cols[colVsVn].Width = 80;
            grfSrcVs.Cols[colVsDept].Width = 170;
            grfSrcVs.Cols[colVsPreno].Width = 100;
            grfSrcVs.Cols[colVsStatus].Width = 60;
            grfSrcVs.Cols[colVsDtrName].Width = 180;
            grfSrcVs.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfSrcVs.Cols[colVsVsDate].Caption = "Visit Date";
            grfSrcVs.Cols[colVsVn].Caption = "VN";
            grfSrcVs.Cols[colVsDept].Caption = "แผนก";
            grfSrcVs.Cols[colVsPreno].Caption = "";
            //grfOPD.Cols[colVsPreno].Visible = false;
            //grfOPD.Cols[colVsVn].Visible = true;
            //grfOPD.Cols[colVsAn].Visible = true;
            //grfOPD.Cols[colVsAndate].Visible = false;
            grfSrcVs.Rows[0].Visible = false;
            grfSrcVs.Cols[0].Visible = false;
            grfSrcVs.Cols[colVsVsDate].AllowEditing = false;
            grfSrcVs.Cols[colVsVn].AllowEditing = false;
            grfSrcVs.Cols[colVsDept].AllowEditing = false;
            grfSrcVs.Cols[colVsPreno].AllowEditing = false;

            grfSrcVs.Cols[colVsDtrName].AllowEditing = false;
            grfSrcVs.Cols[colVsPreno].Visible = false;
            grfSrcVs.Cols[colVsAn].Visible = false;
            grfSrcVs.Cols[colVsAndate].Visible = false;
            grfSrcVs.Cols[colVsVn].Visible = true;

            grfSrcVs.Cols[colVsbp2r].Visible = false;
            grfSrcVs.Cols[colVsbp2l].Visible = false;
            grfSrcVs.Cols[colVsbp1r].Visible = false;
            grfSrcVs.Cols[colVsbp1l].Visible = false;
            grfSrcVs.Cols[colVshc16].Visible = false;
            grfSrcVs.Cols[colVsabc].Visible = false;
            grfSrcVs.Cols[colVsccin].Visible = false;
            grfSrcVs.Cols[colVsccex].Visible = false;
            grfSrcVs.Cols[colVscc].Visible = false;
            grfSrcVs.Cols[colVsWeight].Visible = false;
            grfSrcVs.Cols[colVsHigh].Visible = false;
            grfSrcVs.Cols[colVsVital].Visible = false;
            grfSrcVs.Cols[colVsPres].Visible = false;
            grfSrcVs.Cols[colVsTemp].Visible = false;
            grfSrcVs.Cols[colVsPaidType].Visible = false;
            grfSrcVs.Cols[colVsRadios].Visible = false;
            grfSrcVs.Cols[colVsBreath].Visible = false;
            grfSrcVs.Cols[colVsStatus].Visible = false;
            grfSrcVs.Cols[colVsVsDate1].Visible = false;
            //FilterRow fr = new FilterRow(grfExpn);
            //grfOPD.AfterScroll += GrfOPD_AfterScroll;
            grfSrcVs.AfterRowColChange += GrfSrcVs_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            spSrcVs.Controls.Add(grfSrcVs);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfSrcVs, bc.iniC.themegrfOpd);
        }
        private void setGrfSrcVs(String hn)
        {
            grfSrcVs.Rows.Count = 1;
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectVisitByHn6(hn, "O");
            int i = 1, j = 1; grfSrcVs.Rows.Count = dt.Rows.Count+1;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfSrcVs.Rows[i];
                String status = "";
                status = row1["MNC_PAT_FLAG"] != null ? row1["MNC_PAT_FLAG"].ToString().Equals("O") ? "OPD" : "IPD" : "-";
                VN = row1["MNC_VN_NO"].ToString() + "." + row1["MNC_VN_SEQ"].ToString() + "." + row1["MNC_VN_SUM"].ToString();
                rowa[colVsVsDate] = bc.datetoShowShort(row1["mnc_date"].ToString());
                rowa[colVsVn] = VN;
                rowa[colVsStatus] = status;
                rowa[colVsPreno] = row1["mnc_pre_no"].ToString();
                rowa[colVsDept] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colVsAn] = row1["mnc_an_no"].ToString() + "." + row1["mnc_an_yr"].ToString();
                rowa[colVsAndate] = bc.datetoShow1(row1["mnc_ad_date"].ToString());
                rowa[colVsPaidType] = row1["MNC_FN_TYP_DSC"].ToString();
                
                rowa[colVsVsDate1] = row1["mnc_date"].ToString();
                rowa[colVsDtrName] = row1["dtr_name"].ToString();
                i++;
            }
        }
        private void GrfSrcVs_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            
            try
            {
                PRENO = grfSrcVs[grfSrcVs.Row, colVsPreno] != null ? grfSrcVs[grfSrcVs.Row, colVsPreno].ToString() : "";
                VSDATE = grfSrcVs[grfSrcVs.Row, colVsVsDate1] != null ? grfSrcVs[grfSrcVs.Row, colVsVsDate1].ToString() : "";
                VN = grfSrcVs[grfSrcVs.Row, colVsVn] != null ? grfSrcVs[grfSrcVs.Row, colVsVn].ToString() : "";
                //HN = grfSrcVs[grfSrcVs.Row, colVs] != null ? grfSrcVs[grfSrcVs.Row, colVsVn].ToString() : "";

                btnSrcEKGScanSave.Enabled = false;
                lbSrcPreno.Value = PRENO;
                lbSrcVsDate.Value = bc.datetoShow1(VSDATE);
                lbSrcVN.Value = VN;
                setSrcStaffNote(VSDATE, PRENO);
                setGrfSrcLab(VSDATE, PRENO);
                setGrfSrcOrder(VSDATE, PRENO);
                setGrfSrcXray(VSDATE, PRENO);
                setGrfSrcProcedure(VSDATE, PRENO);
                setControlSrcPttSrc(VN, PRENO, VSDATE);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FRMOPD GrfSrcVs_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FRMOPD GrfSrcVs_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            finally
            {
                //frmFlash.Dispose();
            }
        }
        private void initGrfSrcOrder()
        {
            grfSrcOrder = new C1FlexGrid();
            grfSrcOrder.Font = fEdit;
            grfSrcOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSrcOrder.Location = new System.Drawing.Point(0, 0);
            grfSrcOrder.Rows.Count = 1;
            grfSrcOrder.Cols.Count = 9;
            grfSrcOrder.Cols[colgrfOrderCode].Width = 100;
            grfSrcOrder.Cols[colgrfOrderName].Width = 200;

            grfSrcOrder.ShowCursor = true;
            grfSrcOrder.Cols[colgrfOrderCode].Caption = "code";
            grfSrcOrder.Cols[colgrfOrderName].Caption = "name";

            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grfSrcOrder.Cols[colgrfOrderCode].DataType = typeof(String);
            grfSrcOrder.Cols[colgrfOrderName].DataType = typeof(String);

            grfSrcOrder.Cols[colgrfOrderCode].TextAlign = TextAlignEnum.CenterCenter;
            grfSrcOrder.Cols[colgrfOrderName].TextAlign = TextAlignEnum.LeftCenter;

            grfSrcOrder.Cols[colgrfOrderCode].Visible = true;
            grfSrcOrder.Cols[colgrfOrderName].Visible = true;

            grfSrcOrder.Cols[colgrfOrderCode].AllowEditing = false;
            grfSrcOrder.Cols[colgrfOrderName].AllowEditing = false;

            pnSrcDrug.Controls.Add(grfSrcOrder);
            theme1.SetTheme(grfSrcOrder, bc.iniC.themeApp);
        }
        private void setGrfSrcOrder(String vsDate, String preno)
        {
            DataTable dtOrder = new DataTable();
            dtOrder = bc.bcDB.vsDB.selectDrugOPD(HN, preno, vsDate);
            grfSrcOrder.Rows.Count = 1;
            int i = 0;
            decimal aaa = 0;
            foreach (DataRow row1 in dtOrder.Rows)
            {
                i++;
                Row rowa = grfSrcOrder.Rows.Add();
                rowa[colOrderName] = row1["MNC_PH_TN"].ToString();
                rowa[colOrderMed] = "";
                rowa[colOrderQty] = row1["qty"].ToString();
                rowa[colOrderDate] = bc.datetoShow(row1["mnc_req_dat"]);
                rowa[colOrderFre] = row1["MNC_PH_DIR_DSC"].ToString();
                rowa[colOrderIn1] = row1["MNC_PH_CAU_dsc"].ToString();
                //row1[0] = (i - 2);
            }
        }
        private void initGrfSrcXray()
        {
            grfSrcXray = new C1FlexGrid();
            grfSrcXray.Font = fEdit;
            grfSrcXray.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSrcXray.Location = new System.Drawing.Point(0, 0);
            grfSrcXray.Cols.Count = 5;
            grfSrcXray.Cols[colXrayDate].Caption = "วันที่สั่ง";
            grfSrcXray.Cols[colXrayName].Caption = "ชื่อX-Ray";
            grfSrcXray.Cols[colXrayCode].Caption = "Code X-Ray";
            //grfXray.Cols[colXrayResult].Caption = "ผล X-Ray";

            grfSrcXray.Cols[colXrayDate].Width = 100;
            grfSrcXray.Cols[colXrayName].Width = 250;
            grfSrcXray.Cols[colXrayCode].Width = 100;
            grfSrcXray.Cols[colXrayResult].Width = 200;

            grfSrcXray.Cols[colXrayDate].AllowEditing = false;
            grfSrcXray.Cols[colXrayName].AllowEditing = false;
            grfSrcXray.Cols[colXrayCode].AllowEditing = false;
            grfSrcXray.Cols[colXrayResult].AllowEditing = false;

            grfSrcXray.Name = "grfSrcXray";
            grfSrcXray.Rows.Count = 1;
            pnSrcXray.Controls.Add(grfSrcXray);

            theme1.SetTheme(grfSrcXray, bc.iniC.themeApp);
        }
        private void setGrfSrcXray(String vsDate, String preno)
        {
            DataTable dt = new DataTable();
            String vn = "", vsdate = "", an = "";
            grfSrcXray.Rows.Count = 1;
            dt = bc.bcDB.vsDB.selectResultXraybyVN1(HN, preno, vsDate);
            int i = 0;
            grfSrcXray.Rows.Count = dt.Rows.Count + 1;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    Row rowa = grfSrcXray.Rows[i];
                    rowa[colXrayDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
                    rowa[colXrayName] = row1["MNC_XR_DSC"].ToString();
                    rowa[colXrayCode] = row1["MNC_XR_CD"].ToString();
                    row1[0] = i;
                }
                if (dt.Rows.Count == 1)
                {
                    //setXrayResult();
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FRMOPD setGrfSrcXray " + ex.Message);
            }
        }
        private void initGrfSrcLab()
        {
            grfSrcLab = new C1FlexGrid();
            grfSrcLab.Font = fEdit;
            grfSrcLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSrcLab.Location = new System.Drawing.Point(0, 0);
            grfSrcLab.Rows.Count = 1;
            grfSrcLab.Cols.Count = 6;

            grfSrcLab.Cols.Count = 8;
            grfSrcLab.Cols[colLabDate].Caption = "วันที่สั่ง";
            grfSrcLab.Cols[colLabName].Caption = "ชื่อLAB";
            grfSrcLab.Cols[colLabNameSub].Caption = "ชื่อLABย่อย";
            grfSrcLab.Cols[colLabResult].Caption = "ผลLAB";
            grfSrcLab.Cols[colInterpret].Caption = "แปรผล";
            grfSrcLab.Cols[colNormal].Caption = "Normal";
            grfSrcLab.Cols[colUnit].Caption = "Unit";
            grfSrcLab.Cols[colLabDate].Width = 100;
            grfSrcLab.Cols[colLabName].Width = 250;
            grfSrcLab.Cols[colLabNameSub].Width = 200;
            grfSrcLab.Cols[colInterpret].Width = 200;
            grfSrcLab.Cols[colNormal].Width = 200;
            grfSrcLab.Cols[colUnit].Width = 150;
            grfSrcLab.Cols[colLabResult].Width = 150;

            grfSrcLab.Cols[colLabName].AllowEditing = false;
            grfSrcLab.Cols[colInterpret].AllowEditing = false;
            grfSrcLab.Cols[colNormal].AllowEditing = false;

            pnSrcLab.Controls.Add(grfSrcLab);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfSrcLab, bc.iniC.themegrfOpd);
        }
        private void setGrfSrcLab(String vsDate, String preno)
        {
            DataTable dt = new DataTable();
            DateTime dtt = new DateTime();

            if (vsDate.Length <= 0)
            {
                return;
            }
            dt = bc.bcDB.vsDB.selectLabResultbyVN(HN, preno, vsDate);
            grfSrcLab.Rows.Count = 1; grfSrcLab.Rows.Count = dt.Rows.Count + 1;
            try
            {
                int i = 0, row = grfSrcLab.Rows.Count;
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    Row rowa = grfSrcLab.Rows[i];
                    rowa[colLabName] = row1["MNC_LB_DSC"].ToString();
                    rowa[colLabDate] = bc.datetoShow(row1["mnc_req_dat"].ToString());
                    rowa[colLabNameSub] = row1["mnc_res"].ToString();
                    rowa[colLabResult] = row1["MNC_RES_VALUE"].ToString();
                    rowa[colInterpret] = row1["MNC_STS"].ToString();
                    rowa[colNormal] = row1["MNC_LB_RES"].ToString();
                    rowa[colUnit] = row1["MNC_RES_UNT"].ToString();
                    row1[0] = i;
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPD setGrfSrcLab grfLab " + ex.Message);
            }
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
        private void initGrfDocOLD()
        {
            grfDocOLD = new C1FlexGrid();
            grfDocOLD.Font = fEdit;
            grfDocOLD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDocOLD.Location = new System.Drawing.Point(0, 0);
            grfDocOLD.Rows.Count = 1;
            grfDocOLD.Cols.Count = 8;

            grfDocOLD.Cols[colgrfOutLabDscHN].Width = 80;
            grfDocOLD.Cols[colgrfOutLabDscPttName].Width = 250;
            grfDocOLD.Cols[colgrfOutLabDscVsDate].Width = 100;
            grfDocOLD.Cols[colgrfOutLabDscVN].Width = 80;
            grfDocOLD.Cols[colgrfOutLablabcode].Width = 80;
            grfDocOLD.Cols[colgrfOutLablabname].Width = 250;

            grfDocOLD.Cols[colgrfOutLabDscHN].Caption = "HN";
            grfDocOLD.Cols[colgrfOutLabDscPttName].Caption = "Name";
            grfDocOLD.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
            grfDocOLD.Cols[colgrfOutLabDscVN].Caption = "VN";

            grfDocOLD.Cols[colgrfOutLabDscId].Visible = false;
            grfDocOLD.Cols[colgrfOutLabDscHN].AllowEditing = false;
            grfDocOLD.Cols[colgrfOutLabDscPttName].AllowEditing = false;
            grfDocOLD.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
            grfDocOLD.Cols[colgrfOutLabDscVN].AllowEditing = false;
            grfDocOLD.Cols[colgrfOutLablabcode].Visible = false;
            grfDocOLD.Cols[colgrfOutLablabname].Visible = false;
            grfDocOLD.DoubleClick += GrfDocOLD_DoubleClick;
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ ยกเลิก", new EventHandler(ContextMenu_voidDocOLD));
            grfDocOLD.ContextMenu = menuGw;
            pnDocOLD.Controls.Add(grfDocOLD);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfDocOLD, bc.iniC.themegrfOpd);
        }
        private void ContextMenu_voidDocOLD(object sender, System.EventArgs e)
        {
            if (grfDocOLD.Row <= 0) return;
            if (grfDocOLD.Col <= 0) return;
            String dscid = "";
            dscid = grfDocOLD[grfDocOLD.Row, colgrfOutLabDscId] != null ? grfDocOLD[grfDocOLD.Row, colgrfOutLabDscId].ToString() : "";
            if (dscid.Length <= 0)
            {
                lfSbMessage.Text = "ไม่พบข้อมูล EKG";
                return;
            }
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
            frm.ShowDialog();
            frm.Dispose();
            if (bc.USERCONFIRMID.Length <= 0)
            {
                lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            String re = bc.bcDB.dscDB.voidDocScan(dscid, bc.userId);
            setGrfDocOLD();
            //pnDocOLD.Controls.Clear();
        }
        private void GrfDocOLD_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfDocOLD.Row <= 0) return;
            if (grfDocOLD.Col <= 0) return;
            String hn = "", vn = "", vsDate = "", dscid = "";
            dscid = grfDocOLD[grfDocOLD.Row, colgrfOutLabDscId] != null ? grfDocOLD[grfDocOLD.Row, colgrfOutLabDscId].ToString() : "";
            try
            {
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
                    DocScan dsc = new DocScan();
                    dsc = bc.bcDB.dscDB.selectByPk(dscid);
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                    MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
                    if (dsc.image_path.ToLower().IndexOf("pdf") > 0)
                    {
                        pds.LoadFromStream(streamCertiDtr);
                        fv.DocumentSource = pds;
                    }
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD GrfDocOLD_DoubleClick " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfDocOLD_DoubleClick", ex.Message);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPD GrfDocOLD_DoubleClick " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfDocOLD_DoubleClick save  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
        }
        private void setGrfDocOLD()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.dscDB.selectByDocOLD(txtSrcHn.Text.Trim());
            grfDocOLD.Rows.Count = 1;grfDocOLD.Rows.Count = dt.Rows.Count + 1;int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                Row rowa = grfDocOLD.Rows[i];
                rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["visit_date"].ToString());
                rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
                rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
            }
        }
        private void initGrfEKG()
        {
            grfEKG = new C1FlexGrid();
            grfEKG.Font = fEdit;
            grfEKG.Dock = System.Windows.Forms.DockStyle.Fill;
            grfEKG.Location = new System.Drawing.Point(0, 0);
            grfEKG.Rows.Count = 1;
            grfEKG.Cols.Count = 8;

            grfEKG.Cols[colgrfOutLabDscHN].Width = 80;
            grfEKG.Cols[colgrfOutLabDscPttName].Width = 250;
            grfEKG.Cols[colgrfOutLabDscVsDate].Width = 100;
            grfEKG.Cols[colgrfOutLabDscVN].Width = 80;
            grfEKG.Cols[colgrfOutLablabcode].Width = 80;
            grfEKG.Cols[colgrfOutLablabname].Width = 250;

            grfEKG.Cols[colgrfOutLabDscHN].Caption = "HN";
            grfEKG.Cols[colgrfOutLabDscPttName].Caption = "Name";
            grfEKG.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
            grfEKG.Cols[colgrfOutLabDscVN].Caption = "VN";

            grfEKG.Cols[colgrfOutLabDscId].Visible = false;
            grfEKG.Cols[colgrfOutLabDscHN].AllowEditing = false;
            grfEKG.Cols[colgrfOutLabDscPttName].AllowEditing = false;
            grfEKG.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
            grfEKG.Cols[colgrfOutLabDscVN].AllowEditing = false;
            grfEKG.Cols[colgrfOutLablabcode].Visible = false;
            grfEKG.Cols[colgrfOutLablabname].Visible = false;
            grfEKG.DoubleClick += GrfEKG_DoubleClick;
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ ยกเลิก", new EventHandler(ContextMenu_voidEKG));
            grfEKG.ContextMenu = menuGw;
            pnEKG.Controls.Add(grfEKG);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfEKG, bc.iniC.themegrfOpd);
        }
        private void ContextMenu_voidEKG(object sender, System.EventArgs e)
        {
            if (grfEKG.Row <= 0) return;
            if (grfEKG.Col <= 0) return;
            String dscid = "";
            dscid = grfEKG[grfEKG.Row, colgrfOutLabDscId] != null ? grfEKG[grfEKG.Row, colgrfOutLabDscId].ToString() : "";
            if (dscid.Length <= 0)
            {
                lfSbMessage.Text = "ไม่พบข้อมูล EKG";
                return;
            }
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
            frm.ShowDialog();
            frm.Dispose();
            if (bc.USERCONFIRMID.Length <= 0)
            {
                lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            String re = bc.bcDB.dscDB.voidDocScan(dscid,bc.userId);
            setGrfEKG();
            //pnEKGView.Controls.Clear();
        }
        private void GrfEKG_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfEKG.Row <= 0) return;
            if (grfEKG.Col <= 0) return;
            String hn = "", vn = "", vsDate = "", dscid = "";
            dscid = grfEKG[grfEKG.Row, colgrfOutLabDscId] != null ? grfEKG[grfEKG.Row, colgrfOutLabDscId].ToString() : "";
            try
            {
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
                    DocScan dsc = new DocScan();
                    dsc = bc.bcDB.dscDB.selectByPk(dscid);
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                    MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
                    if (dsc.image_path.ToLower().IndexOf("pdf") > 0)
                    {
                        pds.LoadFromStream(streamCertiDtr);
                        fv.DocumentSource = pds;
                    }
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick", ex.Message);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick save  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
        }
        private void setGrfEKG()
        {
            
            DataTable dt = new DataTable();
            dt = bc.bcDB.dscDB.selectByEKG(txtSrcHn.Text.Trim());
            grfEKG.Rows.Count = 1;grfEKG.Rows.Count = dt.Rows.Count + 1;int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                Row rowa = grfEKG.Rows[i];
                rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["visit_date"].ToString());
                rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
                rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
            }
        }
        private void initGrfCertMed()
        {
            grfCertMed = new C1FlexGrid();
            grfCertMed.Font = fEdit;
            grfCertMed.Dock = System.Windows.Forms.DockStyle.Fill;
            grfCertMed.Location = new System.Drawing.Point(0, 0);
            grfCertMed.Rows.Count = 1;
            grfCertMed.Cols.Count = 8;

            grfCertMed.Cols[colgrfOutLabDscHN].Width = 80;
            grfCertMed.Cols[colgrfOutLabDscPttName].Width = 250;
            grfCertMed.Cols[colgrfOutLabDscVsDate].Width = 100;
            grfCertMed.Cols[colgrfOutLabDscVN].Width = 80;
            grfCertMed.Cols[colgrfOutLablabcode].Width = 80;
            grfCertMed.Cols[colgrfOutLablabname].Width = 250;

            grfCertMed.Cols[colgrfOutLabDscHN].Caption = "HN";
            grfCertMed.Cols[colgrfOutLabDscPttName].Caption = "Name";
            grfCertMed.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
            grfCertMed.Cols[colgrfOutLabDscVN].Caption = "VN";

            grfCertMed.Cols[colgrfOutLabDscId].Visible = false;
            grfCertMed.Cols[colgrfOutLabDscHN].AllowEditing = false;
            grfCertMed.Cols[colgrfOutLabDscPttName].AllowEditing = false;
            grfCertMed.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
            grfCertMed.Cols[colgrfOutLabDscVN].AllowEditing = false;
            grfCertMed.Cols[colgrfOutLablabcode].Visible = false;
            grfCertMed.Cols[colgrfOutLablabname].Visible = false;
            grfCertMed.Click += GrfCertMed_Click;
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("ต้องการ ยกเลิก", new EventHandler(GrfCertMed_DoubleClick));
            //grfCertMed.ContextMenu = menuGw;
            pnCertMed1.Controls.Add(grfCertMed);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfCertMed, bc.iniC.themegrfOpd);
        }

        private void GrfCertMed_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfCertMed.Row <= 0) return;
            if (grfCertMed.Col <= 0) return;
            String hn = "", vn = "", vsDate = "", dscid = "";
            dscid = grfCertMed[grfCertMed.Row, colgrfOutLabDscId] != null ? grfCertMed[grfCertMed.Row, colgrfOutLabDscId].ToString() : "";
            try
            {
                DocScan dsc = new DocScan();
                dsc = bc.bcDB.dscDB.selectByPk(dscid);
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
                    FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                    MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
                    txtdocNo.Value = dsc.doc_scan_id;
                    if (dsc.image_path.ToLower().IndexOf("pdf") > 0)
                    {
                        pds.LoadFromStream(streamCertiDtr);
                        fv.DocumentSource = pds;
                    }
                    else
                    {
                        var pdf = new C1PdfDocument();
                        Image img = Image.FromStream(streamCertiDtr);
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
                        //pdf.DrawImage(img, RectangleF.FromLTRB(0, 0, img.Width, img.Height));
                        string tempPdf = Path.GetTempFileName() + ".pdf";
                        pdf.Save(tempPdf);
                        pds.LoadFromFile(tempPdf);
                        fv.DocumentSource = pds;
                        //PictureBox pic = new PictureBox();
                        //pic.Image = Image.FromStream(streamCertiDtr);
                        //pic.SizeMode = PictureBoxSizeMode.Zoom;
                        //pic.Dock = DockStyle.Fill;
                        //pnCertMedView.Controls.Clear();
                        //pnCertMedView.Controls.Add(pic);
                    }
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD GrfCertMed_Click " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfCertMed_Click", ex.Message);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPD GrfCertMed_Click " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfCertMed_Click save  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
        }
        private void setGrfCertMed()
        {
            grfCertMed.Rows.Count = 1;
            DataTable dt = new DataTable();
            dt = bc.bcDB.dscDB.selectByCertMed(txtSrcHn.Text.Trim());
            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfCertMed.Rows.Add();
                rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["visit_date"].ToString());
                rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
                rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
            }
        }
        private void initGrfECHO()
        {
            grfECHO = new C1FlexGrid();
            grfECHO.Font = fEdit;
            grfECHO.Dock = System.Windows.Forms.DockStyle.Fill;
            grfECHO.Location = new System.Drawing.Point(0, 0);
            grfECHO.Rows.Count = 1;
            grfECHO.Cols.Count = 8;

            grfECHO.Cols[colgrfOutLabDscHN].Width = 80;
            grfECHO.Cols[colgrfOutLabDscPttName].Width = 250;
            grfECHO.Cols[colgrfOutLabDscVsDate].Width = 100;
            grfECHO.Cols[colgrfOutLabDscVN].Width = 80;
            grfECHO.Cols[colgrfOutLablabcode].Width = 80;
            grfECHO.Cols[colgrfOutLablabname].Width = 250;

            grfECHO.Cols[colgrfOutLabDscHN].Caption = "HN";
            grfECHO.Cols[colgrfOutLabDscPttName].Caption = "Name";
            grfECHO.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
            grfECHO.Cols[colgrfOutLabDscVN].Caption = "VN";

            grfECHO.Cols[colgrfOutLabDscId].Visible = false;
            grfECHO.Cols[colgrfOutLabDscHN].AllowEditing = false;
            grfECHO.Cols[colgrfOutLabDscPttName].AllowEditing = false;
            grfECHO.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
            grfECHO.Cols[colgrfOutLabDscVN].AllowEditing = false;
            grfECHO.Cols[colgrfOutLablabcode].Visible = false;
            grfECHO.Cols[colgrfOutLablabname].Visible = false;
            grfECHO.DoubleClick += GrfECHO_DoubleClick;
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ ยกเลิก", new EventHandler(ContextMenu_voidECHO));
            grfECHO.ContextMenu = menuGw;
            pnECHO.Controls.Add(grfECHO);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfECHO, bc.iniC.themegrfOpd);
        }
        private void ContextMenu_voidECHO(object sender, System.EventArgs e)
        {
            if (grfECHO.Row <= 0) return;
            if (grfECHO.Col <= 0) return;
            String dscid = "";
            dscid = grfECHO[grfECHO.Row, colgrfOutLabDscId] != null ? grfECHO[grfECHO.Row, colgrfOutLabDscId].ToString() : "";
            if (dscid.Length <= 0)
            {
                lfSbMessage.Text = "ไม่พบข้อมูล EKG";
                return;
            }
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
            frm.ShowDialog();
            frm.Dispose();
            if (bc.USERCONFIRMID.Length <= 0)
            {
                lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            String re = bc.bcDB.dscDB.voidDocScan(dscid, bc.userId);
            setGrfECHO();
            //pnEKGView.Controls.Clear();
        }
        private void GrfECHO_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfECHO.Row <= 0) return;
            if (grfECHO.Col <= 0) return;
            String hn = "", vn = "", vsDate = "", dscid = "";
            dscid = grfECHO[grfECHO.Row, colgrfOutLabDscId] != null ? grfECHO[grfECHO.Row, colgrfOutLabDscId].ToString() : "";
            try
            {
                pnECHOView.Controls.Clear();
                C1FlexViewer fv = new C1FlexViewer();
                fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
                fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
                fv.Dock = System.Windows.Forms.DockStyle.Fill;
                fv.Location = new System.Drawing.Point(0, 0);
                fv.Name = "fvSrcEKGScan";
                fv.Size = new System.Drawing.Size(1065, 790);
                fv.TabIndex = 0;
                fv.Ribbon.Minimized = true;
                pnECHOView.Controls.Add(fv);
                try
                {
                    DocScan dsc = new DocScan();
                    dsc = bc.bcDB.dscDB.selectByPk(dscid);
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                    MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
                    if (dsc.image_path.ToLower().IndexOf("pdf") > 0)
                    {
                        pds.LoadFromStream(streamCertiDtr);
                        fv.DocumentSource = pds;
                    }
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick", ex.Message);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick save  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
        }
        private void setGrfECHO()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.dscDB.selectByECHO(txtSrcHn.Text.Trim());
            grfECHO.Rows.Count = 1;grfECHO.Rows.Count = dt.Rows.Count + 1;int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                Row rowa = grfECHO.Rows[i];
                rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["visit_date"].ToString());
                rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
                rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
            }
        }
        private void initGrfEST()
        {
            grfEST = new C1FlexGrid();
            grfEST.Font = fEdit;
            grfEST.Dock = System.Windows.Forms.DockStyle.Fill;
            grfEST.Location = new System.Drawing.Point(0, 0);
            grfEST.Rows.Count = 1;
            grfEST.Cols.Count = 8;

            grfEST.Cols[colgrfOutLabDscHN].Width = 80;
            grfEST.Cols[colgrfOutLabDscPttName].Width = 250;
            grfEST.Cols[colgrfOutLabDscVsDate].Width = 100;
            grfEST.Cols[colgrfOutLabDscVN].Width = 80;
            grfEST.Cols[colgrfOutLablabcode].Width = 80;
            grfEST.Cols[colgrfOutLablabname].Width = 250;

            grfEST.Cols[colgrfOutLabDscHN].Caption = "HN";
            grfEST.Cols[colgrfOutLabDscPttName].Caption = "Name";
            grfEST.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
            grfEST.Cols[colgrfOutLabDscVN].Caption = "VN";

            grfEST.Cols[colgrfOutLabDscId].Visible = false;
            grfEST.Cols[colgrfOutLabDscHN].AllowEditing = false;
            grfEST.Cols[colgrfOutLabDscPttName].AllowEditing = false;
            grfEST.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
            grfEST.Cols[colgrfOutLabDscVN].AllowEditing = false;
            grfEST.Cols[colgrfOutLablabcode].Visible = false;
            grfEST.Cols[colgrfOutLablabname].Visible = false;
            grfEST.DoubleClick += GrfEST_DoubleClick;
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ ยกเลิก", new EventHandler(ContextMenu_voidEST));
            grfEST.ContextMenu = menuGw;
            pnEST.Controls.Add(grfEST);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfEST, bc.iniC.themegrfOpd);
        }
        private void ContextMenu_voidEST(object sender, System.EventArgs e)
        {
            if (grfEST.Row <= 0) return;
            if (grfEST.Col <= 0) return;
            String dscid = "";
            dscid = grfEST[grfEST.Row, colgrfOutLabDscId] != null ? grfEST[grfEST.Row, colgrfOutLabDscId].ToString() : "";
            if (dscid.Length <= 0)
            {
                lfSbMessage.Text = "ไม่พบข้อมูล EKG";
                return;
            }
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
            frm.ShowDialog();
            frm.Dispose();
            if (bc.USERCONFIRMID.Length <= 0)
            {
                lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            String re = bc.bcDB.dscDB.voidDocScan(dscid, bc.userId);
            setGrfEST();
            //pnEKGView.Controls.Clear();
        }
        private void GrfEST_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfEST.Row <= 0) return;
            if (grfEST.Col <= 0) return;
            String hn = "", vn = "", vsDate = "", dscid = "";
            dscid = grfEST[grfEST.Row, colgrfOutLabDscId] != null ? grfEST[grfEST.Row, colgrfOutLabDscId].ToString() : "";
            try
            {
                pnESTView.Controls.Clear();
                C1FlexViewer fv = new C1FlexViewer();
                fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
                fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
                fv.Dock = System.Windows.Forms.DockStyle.Fill;
                fv.Location = new System.Drawing.Point(0, 0);
                fv.Name = "fvSrcEKGScan";
                fv.Size = new System.Drawing.Size(1065, 790);
                fv.TabIndex = 0;
                fv.Ribbon.Minimized = true;
                pnESTView.Controls.Add(fv);
                try
                {
                    DocScan dsc = new DocScan();
                    dsc = bc.bcDB.dscDB.selectByPk(dscid);
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                    MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
                    if (dsc.image_path.ToLower().IndexOf("pdf") > 0)
                    {
                        pds.LoadFromStream(streamCertiDtr);
                        fv.DocumentSource = pds;
                    }
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick", ex.Message);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick save  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
        }
        private void setGrfEST()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.dscDB.selectByEST(txtSrcHn.Text.Trim());
            grfEST.Rows.Count = 1;grfEST.Rows.Count = dt.Rows.Count + 1;int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                Row rowa = grfEST.Rows[i];
                rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["visit_date"].ToString());
                rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
                rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
            }
        }
        private void initGrfHolter()
        {
            grfHolter = new C1FlexGrid();
            grfHolter.Font = fEdit;
            grfHolter.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHolter.Location = new System.Drawing.Point(0, 0);
            grfHolter.Rows.Count = 1;
            grfHolter.Cols.Count = 8;

            grfHolter.Cols[colgrfOutLabDscHN].Width = 80;
            grfHolter.Cols[colgrfOutLabDscPttName].Width = 250;
            grfHolter.Cols[colgrfOutLabDscVsDate].Width = 100;
            grfHolter.Cols[colgrfOutLabDscVN].Width = 80;
            grfHolter.Cols[colgrfOutLablabcode].Width = 80;
            grfHolter.Cols[colgrfOutLablabname].Width = 250;

            grfHolter.Cols[colgrfOutLabDscHN].Caption = "HN";
            grfHolter.Cols[colgrfOutLabDscPttName].Caption = "Name";
            grfHolter.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
            grfHolter.Cols[colgrfOutLabDscVN].Caption = "VN";

            grfHolter.Cols[colgrfOutLabDscId].Visible = false;
            grfHolter.Cols[colgrfOutLabDscHN].AllowEditing = false;
            grfEST.Cols[colgrfOutLabDscPttName].AllowEditing = false;
            grfHolter.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
            grfHolter.Cols[colgrfOutLabDscVN].AllowEditing = false;
            grfHolter.Cols[colgrfOutLablabcode].Visible = false;
            grfHolter.Cols[colgrfOutLablabname].Visible = false;
            grfHolter.DoubleClick += GrfHolter_DoubleClick;
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ ยกเลิก", new EventHandler(ContextMenu_voidHolter));
            grfHolter.ContextMenu = menuGw;
            pnHolter.Controls.Add(grfHolter);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfEST, bc.iniC.themegrfOpd);
        }
        private void ContextMenu_voidHolter(object sender, System.EventArgs e)
        {
            if (grfHolter.Row <= 0) return;
            if (grfHolter.Col <= 0) return;
            String dscid = "";
            dscid = grfHolter[grfHolter.Row, colgrfOutLabDscId] != null ? grfHolter[grfHolter.Row, colgrfOutLabDscId].ToString() : "";
            if (dscid.Length <= 0)
            {
                lfSbMessage.Text = "ไม่พบข้อมูล EKG";
                return;
            }
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
            frm.ShowDialog();
            frm.Dispose();
            if (bc.USERCONFIRMID.Length <= 0)
            {
                lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            String re = bc.bcDB.dscDB.voidDocScan(dscid, bc.userId);
            setGrfHolter();
            //pnEKGView.Controls.Clear();
        }
        private void GrfHolter_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfHolter.Row <= 0) return;
            if (grfHolter.Col <= 0) return;
            String hn = "", vn = "", vsDate = "", dscid = "";
            dscid = grfHolter[grfHolter.Row, colgrfOutLabDscId] != null ? grfHolter[grfHolter.Row, colgrfOutLabDscId].ToString() : "";
            try
            {
                pnHolterView.Controls.Clear();
                C1FlexViewer fv = new C1FlexViewer();
                fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
                fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
                fv.Dock = System.Windows.Forms.DockStyle.Fill;
                fv.Location = new System.Drawing.Point(0, 0);
                fv.Name = "fvSrcEKGScan";
                fv.Size = new System.Drawing.Size(1065, 790);
                fv.TabIndex = 0;
                fv.Ribbon.Minimized = true;
                pnHolterView.Controls.Add(fv);
                try
                {
                    DocScan dsc = new DocScan();
                    dsc = bc.bcDB.dscDB.selectByPk(dscid);
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                    MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
                    if (dsc.image_path.ToLower().IndexOf("pdf") > 0)
                    {
                        pds.LoadFromStream(streamCertiDtr);
                        fv.DocumentSource = pds;
                    }
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick", ex.Message);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick save  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
        }
        private void setGrfHolter()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.dscDB.selectByHolter(txtSrcHn.Text.Trim());
            grfHolter.Rows.Count = 1;grfHolter.Rows.Count = dt.Rows.Count + 1;int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                Row rowa = grfHolter.Rows[i];
                rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["visit_date"].ToString());
                rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
                rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
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
            grfIPD.Rows.Count = dt.Rows.Count+1;
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
            dt = bc.bcDB.dscDB.selectByAn(txtSBSearchHN.Text, an.Replace(".", "/"));
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
        private void initGrfHisOrder()
        {
            grfHisOrder = new C1FlexGrid();
            grfHisOrder.Font = fEdit;
            grfHisOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHisOrder.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grfHisOrder.Cols[colOrderId].Visible = false;
            grfHisOrder.Rows.Count = 1;
            grfHisOrder.Cols.Count = 8;
            grfHisOrder.Cols[colOrderName].Caption = "Drug Name";
            grfHisOrder.Cols[colOrderMed].Caption = "MED";
            grfHisOrder.Cols[colOrderQty].Caption = "QTY";
            grfHisOrder.Cols[colOrderDate].Caption = "Date";
            grfHisOrder.Cols[colOrderFre].Caption = "วิธีใช้";
            grfHisOrder.Cols[colOrderIn1].Caption = "ข้อควรระวัง";
            grfHisOrder.Cols[colOrderName].Width = 400;
            grfHisOrder.Cols[colOrderMed].Width = 200;
            grfHisOrder.Cols[colOrderQty].Width = 60;
            grfHisOrder.Cols[colOrderDate].Width = 90;
            grfHisOrder.Cols[colOrderFre].Width = 500;
            grfHisOrder.Cols[colOrderIn1].Width = 350;
            grfHisOrder.Cols[colOrderName].AllowEditing = false;
            grfHisOrder.Cols[colOrderQty].AllowEditing = false;
            grfHisOrder.Cols[colOrderMed].AllowEditing = false;
            grfHisOrder.Cols[colOrderFre].AllowEditing = false;
            grfHisOrder.Cols[colOrderIn1].AllowEditing = false;
            grfHisOrder.Cols[colOrderDate].AllowEditing = false;
            grfHisOrder.Name = "grfHisOrder";
            pnHistoryOrder.Controls.Add(grfHisOrder);
        }
        private void setGrfHisOrder(String hn,String vsDate, String preno,ref C1FlexGrid grf)
        {
            DataTable dtOrder = new DataTable();
            dtOrder = bc.bcDB.vsDB.selectDrugOPD(hn, preno, vsDate);
            grf.Rows.Count = 1;grf.Rows.Count = dtOrder.Rows.Count;  int i = 0;
            decimal aaa = 0;
            foreach (DataRow row1 in dtOrder.Rows)
            {
                i++;
                Row rowa = grf.Rows[i];
                rowa[colOrderName] = row1["MNC_PH_TN"].ToString();
                rowa[colOrderMed] = "";
                rowa[colOrderQty] = row1["qty"].ToString();
                rowa[colOrderDate] = bc.datetoShow(row1["mnc_req_dat"]);
                rowa[colOrderFre] = row1["MNC_PH_DIR_DSC"].ToString();
                rowa[colOrderIn1] = row1["MNC_PH_CAU_dsc"].ToString();
                //row1[0] = (i - 2);
            }
        }
        private void initGrfXray(ref C1FlexGrid grf,ref Panel pn)
        {
            grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);
            grf.Cols.Count = 5;
            grf.Cols[colXrayDate].Caption = "วันที่สั่ง";
            grf.Cols[colXrayName].Caption = "ชื่อX-Ray";
            grf.Cols[colXrayCode].Caption = "Code X-Ray";
            //grfXray.Cols[colXrayResult].Caption = "ผล X-Ray";

            grf.Cols[colXrayDate].Width = 100;
            grf.Cols[colXrayName].Width = 250;
            grf.Cols[colXrayCode].Width = 100;
            grf.Cols[colXrayResult].Width = 200;

            grf.Cols[colXrayDate].AllowEditing = false;
            grf.Cols[colXrayName].AllowEditing = false;
            grf.Cols[colXrayCode].AllowEditing = false;
            grf.Cols[colXrayResult].AllowEditing = false;

            grf.Name = "grfXray";
            grf.Rows.Count = 1;
            pn.Controls.Add(grf);

            theme1.SetTheme(grf, bc.iniC.themeApp);
        }
        private void setGrfXray(String hn, String vsDate, String preno,ref C1FlexGrid grf)
        {
            DataTable dt = new DataTable();
            String vn = "", vsdate = "", an = "";
            dt = bc.bcDB.vsDB.selectResultXraybyVN1(hn, preno, vsDate);
            int i = 0; grf.Rows.Count = 1;grf.Rows.Count = dt.Rows.Count+1;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    Row rowa = grf.Rows[i];
                    rowa[colXrayDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
                    rowa[colXrayName] = row1["MNC_XR_DSC"].ToString();
                    rowa[colXrayCode] = row1["MNC_XR_CD"].ToString();
                    row1[0] = i;
                }
                if (dt.Rows.Count == 1)
                {
                    //setXrayResult();
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmScanView1 setGrfXrayOPD " + ex.Message);
            }
        }
        private void initGrfLab(ref C1FlexGrid grf,ref Panel pn)
        {
            grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);
            grf.Rows.Count = 1;
            grf.Cols.Count = 6;

            grf.Cols.Count = 8;
            grf.Cols[colLabDate].Caption = "วันที่สั่ง";
            grf.Cols[colLabName].Caption = "ชื่อLAB";
            grf.Cols[colLabNameSub].Caption = "ชื่อLABย่อย";
            grf.Cols[colLabResult].Caption = "ผลLAB";
            grf.Cols[colInterpret].Caption = "แปรผล";
            grf.Cols[colNormal].Caption = "Normal";
            grf.Cols[colUnit].Caption = "Unit";
            grf.Cols[colLabDate].Width = 100;
            grf.Cols[colLabName].Width = 250;
            grf.Cols[colLabNameSub].Width = 200;
            grf.Cols[colInterpret].Width = 200;
            grf.Cols[colNormal].Width = 200;
            grf.Cols[colUnit].Width = 150;
            grf.Cols[colLabResult].Width = 150;

            grf.Cols[colLabName].AllowEditing = false;
            grf.Cols[colInterpret].AllowEditing = false;
            grf.Cols[colNormal].AllowEditing = false;

            pn.Controls.Add(grf);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grf, bc.iniC.themegrfOpd);
        }
        private void setGrfLab(String hn, String vsDate, String preno, ref C1FlexGrid grf)
        {
            DataTable dt = new DataTable();
            DateTime dtt = new DateTime();
            
            if (vsDate.Length <= 0)
            {
                return;
            }
            dt = bc.bcDB.vsDB.selectLabResultbyVN(hn, preno, vsDate);
            grf.Rows.Count = 1;grf.Rows.Count = dt.Rows.Count+1;
            try
            {
                int i = 0, row = grf.Rows.Count;
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    Row rowa = grf.Rows[i];
                    rowa[colLabName] = row1["MNC_LB_DSC"].ToString();
                    rowa[colLabDate] = bc.datetoShow(row1["mnc_req_dat"].ToString());
                    rowa[colLabNameSub] = row1["mnc_res"].ToString();
                    rowa[colLabResult] = row1["MNC_RES_VALUE"].ToString();
                    rowa[colInterpret] = row1["MNC_STS"].ToString();
                    rowa[colNormal] = row1["MNC_LB_RES"].ToString();
                    rowa[colUnit] = row1["MNC_RES_UNT"].ToString();
                    row1[0] = i;
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmScanView1 setGrfLab grfLab " + ex.Message);
            }
        }
        private void initGrfTodayOutLab()
        {
            grfTodayOutLab = new C1FlexGrid();
            grfTodayOutLab.Font = fEdit;
            grfTodayOutLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfTodayOutLab.Location = new System.Drawing.Point(0, 0);
            grfTodayOutLab.Rows.Count = 1;
            grfTodayOutLab.Cols.Count = 13;

            grfTodayOutLab.Cols[colgrfOutLabDscHN].Width = 80;
            grfTodayOutLab.Cols[colgrfOutLabDscPttName].Width = 220;
            grfTodayOutLab.Cols[colgrfOutLabDscVsDate].Width = 90;
            grfTodayOutLab.Cols[colgrfOutLabDscVN].Width = 80;
            grfTodayOutLab.Cols[colgrfOutLablabcode].Width = 80;
            grfTodayOutLab.Cols[colgrfOutLablabname].Width = 200;
            grfTodayOutLab.Cols[colgrfOutLabApmDesc].Width = 200;
            grfTodayOutLab.Cols[colgrfOutLabApmDtr].Width = 150;
            grfTodayOutLab.Cols[colgrfOutLabApmDate].Width = 100;

            grfTodayOutLab.Cols[colgrfOutLabDscHN].DataType = typeof(String);
            grfTodayOutLab.Cols[colgrfOutLabDscVsDate].DataType = typeof(String);
            grfTodayOutLab.Cols[colgrfOutLablabcode].DataType = typeof(String);
            grfTodayOutLab.Cols[colgrfOutLabApmDate].DataType = typeof(String);
            grfTodayOutLab.Cols[colgrfOutLabDscVsDate].DataType = typeof(String);
            grfTodayOutLab.Cols[colgrfOutLabDscHN].TextAlign = TextAlignEnum.CenterCenter;
            grfTodayOutLab.Cols[colgrfOutLabDscVsDate].TextAlign = TextAlignEnum.CenterCenter;
            grfTodayOutLab.Cols[colgrfOutLablabcode].TextAlign = TextAlignEnum.CenterCenter;
            grfTodayOutLab.Cols[colgrfOutLabApmDate].TextAlign = TextAlignEnum.CenterCenter;
            grfTodayOutLab.Cols[colgrfOutLabDscVsDate].TextAlign = TextAlignEnum.CenterCenter;

            grfTodayOutLab.Cols[colgrfOutLabDscHN].Caption = "HN";
            grfTodayOutLab.Cols[colgrfOutLabDscPttName].Caption = "Name";
            grfTodayOutLab.Cols[colgrfOutLabDscVsDate].Caption = "req Date";
            grfTodayOutLab.Cols[colgrfOutLabDscVN].Caption = "VN";
            grfTodayOutLab.Cols[colgrfOutLablabcode].Caption = "code";
            grfTodayOutLab.Cols[colgrfOutLablabname].Caption = "lab name";

            grfTodayOutLab.Cols[colgrfOutLabDscId].Visible = false;
            grfTodayOutLab.Cols[colgrfOutLabDscVN].Visible = false;
            grfTodayOutLab.Cols[colgrfOutLabApmReqNo].Visible = true;
            grfTodayOutLab.Cols[colgrfOutLabApmReqDate].Visible = false;

            grfTodayOutLab.Cols[colgrfOutLabApmReqNo].AllowEditing = false;
            grfTodayOutLab.Cols[colgrfOutLabDscHN].AllowEditing = false;
            grfTodayOutLab.Cols[colgrfOutLabDscPttName].AllowEditing = false;
            grfTodayOutLab.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
            grfTodayOutLab.Cols[colgrfOutLabDscVN].AllowEditing = false;
            grfTodayOutLab.Cols[colgrfOutLablabcode].AllowEditing = false;
            grfTodayOutLab.Cols[colgrfOutLablabname].AllowEditing = false;
            grfTodayOutLab.Cols[colgrfOutLabApmDate].AllowEditing = false;
            grfTodayOutLab.Cols[colgrfOutLabApmDesc].AllowEditing = false;
            grfTodayOutLab.Cols[colgrfOutLabApmDtr].AllowEditing = false;
            grfTodayOutLab.AfterRowColChange += GrfTodayOutLab_AfterRowColChange;

            pnTodayOutLabList.Controls.Add(grfTodayOutLab);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfTodayOutLab, bc.iniC.themegrfOpd);
        }

        private void GrfTodayOutLab_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            String reqno = "", reqdate="", hn="", dscid = "";
            try
            {
                reqno = grfTodayOutLab[grfTodayOutLab.Row, colgrfOutLabApmReqNo] != null ? grfTodayOutLab[grfTodayOutLab.Row, colgrfOutLabApmReqNo].ToString() : "";
                reqdate = grfTodayOutLab[grfTodayOutLab.Row, colgrfOutLabApmReqDate] != null ? grfTodayOutLab[grfTodayOutLab.Row, colgrfOutLabApmReqDate].ToString() : "";
                hn = grfTodayOutLab[grfTodayOutLab.Row, colgrfOutLabDscHN] != null ? grfTodayOutLab[grfTodayOutLab.Row, colgrfOutLabDscHN].ToString() : "";
                //dscid = grfOutLab[grfOutLab.Row, colVsPreno] != null ? grfOutLab[grfOutLab.Row, colgrfOutLabDscId].ToString() : "";
                DocScan dsc = new DocScan();
                dsc = bc.bcDB.dscDB.selectLabOutByHnReqDateReqNo1(hn, reqdate, reqno);
                if (dsc.doc_scan_id.Length <= 0)
                {
                    dsc = bc.bcDB.dscDB.selectLabOutByHnReqDateReqNoUnActive(hn, reqdate, reqno);
                }
                C1PdfDocumentSource pds = new C1PdfDocumentSource();
                
                FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
                pds.LoadFromStream(streamCertiDtr);
                fvTodayOutLab.DocumentSource = pds;
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FRMOPD GrfOPD_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FRMOPD GrfOPD_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            finally
            {
                //frmFlash.Dispose();
            }
        }
        private void setGrfTodayOutLab()
        {
            DateTime.TryParse(txtSBSearchDate.Text, out DateTime datestart);
            
            DataTable dt = new DataTable();
            dt = bc.bcDB.labT02DB.selectByTodayOutLab(datestart.Year.ToString() + "-" + datestart.ToString("MM-dd"));
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1; grfTodayOutLab.Rows.Count = 1;grfTodayOutLab.Rows.Count = dt.Rows.Count + 1;
            //grfTodayOutLab.Rows.Count = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfTodayOutLab.Rows[i];
                String status = "", vn = "";
                rowa[colgrfOutLabDscHN] = row1["MNC_HN_NO"].ToString();
                //rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
                rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["MNC_REQ_DAT"].ToString());
                rowa[colgrfOutLabDscPttName] = row1["pttfullname"].ToString();
                rowa[colgrfOutLablabcode] = row1["MNC_LB_CD"].ToString();
                rowa[colgrfOutLablabname] = row1["MNC_LB_DSC"].ToString();
                rowa[colgrfOutLabApmDate] = row1["MNC_APP_DAT"].ToString();
                rowa[colgrfOutLabApmDesc] = row1["MNC_APP_DSC"].ToString();
                rowa[colgrfOutLabApmDtr] = row1["dtr_name"].ToString();
                rowa[colgrfOutLabApmReqNo] = row1["MNC_REQ_NO"].ToString();
                rowa[colgrfOutLabApmReqDate] = row1["MNC_REQ_DAT"].ToString();
                i++;
            }
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
            catch(Exception ex)
            {
                new LogWriter("e", "FrmOPD GrfOutLab_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfOutLab_AfterRowColChange save  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
        }
        private void setGrfOutLab()
        {
            
            fvCerti.DocumentSource = null;
            DataTable dt = new DataTable();
            dt = bc.bcDB.dscDB.selectOutLabByHn(txtSBSearchHN.Text.Trim());
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1, row = grfOutLab.Rows.Count; grfOutLab.Rows.Count = 1;grfOutLab.Rows.Count = dt.Rows.Count+1;
            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfOutLab.Rows[i];
                String status = "", vn = "";
                rowa[colgrfOutLabDscHN] = row1["hn"].ToString();
                rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
                rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["date_req"].ToString());
                rowa[colgrfOutLabDscPttName] = row1["patient_fullname"].ToString();
                rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
                i++;
            }
        }
        private void initGrfOPD()
        {
            grfOPD = new C1FlexGrid();
            grfOPD.Font = fEdit;
            grfOPD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPD.Location = new System.Drawing.Point(0, 0);
            grfOPD.Rows.Count = 1;
            grfOPD.Cols.Count = 27;
            grfOPD.Cols[colVsVsDate].Width = 72;
            grfOPD.Cols[colVsVn].Width = 80;
            grfOPD.Cols[colVsDept].Width = 170;
            grfOPD.Cols[colVsPreno].Width = 100;
            grfOPD.Cols[colVsStatus].Width = 60;
            grfOPD.Cols[colVsDtrName].Width = 180;
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

            grfOPD.Cols[colVsDtrName].AllowEditing = false;
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
            grfOPD.Cols[colVsVsDate1].Visible = false;
            //FilterRow fr = new FilterRow(grfExpn);
            //grfOPD.AfterScroll += GrfOPD_AfterScroll;
            grfOPD.AfterRowColChange += GrfOPD_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            spHistoryVS.Controls.Add(grfOPD);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfOPD, bc.iniC.themegrfOpd);
        }
        private void GrfOPD_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            if (grfOPD.Row <= 0) return;
            try
            {
                PRENO = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                VSDATE = grfOPD[grfOPD.Row, colVsVsDate1] != null ? grfOPD[grfOPD.Row, colVsVsDate1].ToString() : "";
                setStaffNote(VSDATE, PRENO, picHisL,picHisR);
                setGrfLab(txtOperHN.Text.Trim(), VSDATE, PRENO, ref grfLab);
                setGrfHisOrder(txtOperHN.Text.Trim(), VSDATE, PRENO, ref grfHisOrder);
                setGrfXray(txtOperHN.Text.Trim(), VSDATE, PRENO, ref grfXray);
                setGrfHisProcedure(txtOperHN.Text.Trim(), VSDATE, PRENO, ref grfHisProcedure);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FRMOPD GrfOPD_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FRMOPD GrfOPD_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            finally
            {
                //frmFlash.Dispose();
            }
        }
        private void setGrfOPD()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectVisitByHn6(txtOperHN.Text, "O");
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1, row = grfOPD.Rows.Count; grfOPD.Rows.Count = 1;grfOPD.Rows.Count = dt.Rows.Count+1;

            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfOPD.Rows[i];
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
                
                rowa[colVsVsDate1] = row1["mnc_date"].ToString();
                rowa[colVsDtrName] = row1["dtr_name"].ToString();
                i++;
            }
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
        private void initGrfChkPackItems()
        {
            grfChkPackItems = new C1FlexGrid();
            grfChkPackItems.Font = fEdit;
            grfChkPackItems.Dock = System.Windows.Forms.DockStyle.Fill;
            grfChkPackItems.Location = new System.Drawing.Point(0, 0);
            grfChkPackItems.Rows.Count = 1;
            grfChkPackItems.Cols.Count = 5;
            grfChkPackItems.Cols[colChkPackItemsname].Width = 200;
            grfChkPackItems.Cols[colChkPackItemflag].Width = 30;
            grfChkPackItems.Cols[colChkPackItemsitemcode].Width = 60;
            grfChkPackItems.Cols[colChkPackItemsPackcode].Width = 60;
            grfChkPackItems.ShowCursor = true;
            grfChkPackItems.Cols[colChkPackItemsname].Caption = "name";
            grfChkPackItems.Cols[colChkPackItemflag].Caption = "flag";
            grfChkPackItems.Cols[colChkPackItemsitemcode].Caption = "code";
            grfChkPackItems.Cols[colChkPackItemsPackcode].Caption = "-";

            grfChkPackItems.Cols[colChkPackItemsitemcode].Visible = false;
            grfChkPackItems.Cols[colChkPackItemsname].AllowEditing = false;
            grfChkPackItems.Cols[colChkPackItemflag].AllowEditing = false;
            grfChkPackItems.Cols[colChkPackItemsPackcode].AllowEditing = false;

            grfChkPackItems.Rows.Count = 1;

            gbCheckUPPackage.Controls.Add(grfChkPackItems);
            theme1.SetTheme(grfChkPackItems, bc.iniC.themeApp);
        }
        private void setGrfChkPackItems(String packagecode)
        {
            if (pageLoad) return;
            pageLoad = true;
            if (packagecode.Length <= 0) pageLoad = false;  return;
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.pm40DB.selectByPackCode(packagecode);
            grfChkPackItems.Rows.Count = 1; grfChkPackItems.Rows.Count = dtvs.Rows.Count + 1; int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfChkPackItems.Rows[i];
                rowa[colChkPackItemsitemcode] = row1["MNC_OPR_CD"].ToString();
                rowa[colChkPackItemsname] = row1["MNC_OPR_FLAG"].ToString().Equals("F") ? row1["MNC_DF_DSC"].ToString() : row1["MNC_OPR_FLAG"].ToString().Equals("L") ? row1["MNC_LB_DSC"].ToString() : row1["MNC_OPR_FLAG"].ToString().Equals("X") ? row1["MNC_XR_DSC"].ToString(): row1["MNC_OPR_FLAG"].ToString().Equals("O") ? row1["MNC_SR_DSC"].ToString():"";
                rowa[colChkPackItemflag] = row1["MNC_OPR_FLAG"].ToString();
                rowa[colChkPackItemsPackcode] = row1["MNC_PAC_CD"].ToString();
                //rowa[colChkPackItemsPackcode] = row1["MNC_PAC_CD"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
            
            pageLoad = false;
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
            grfRpt.DoubleClick += GrfRpt_DoubleClick;
            //grfCheckUPList.AllowFiltering = true;
            grfRpt.Rows.Count = 3;
            Row rowa = grfRpt.Rows[1];
            Row row2 = grfRpt.Rows[2];
            rowa[1] ="รายงาน แพทย์นัด";
            row2[1] = "รายงาน จำนวนคนไข้ในแผนก";

            pnRptName.Controls.Add(grfRpt);
            theme1.SetTheme(grfRpt, bc.iniC.themeApp);
        }
        private void GrfRpt_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void initGrfOrder(ref C1FlexGrid grf, ref Panel pn, String grfname)
        {
            grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);
            grf.Rows.Count = 1;
            grf.Cols.Count = 13;
            grf.Cols[colgrfOrderCode].Width = 100;
            grf.Cols[colgrfOrderName].Width = 400;
            grf.Cols[colgrfOrderQty].Width = 70;
            grf.Name = grfname;
            grf.ShowCursor = true;
            grf.Cols[colgrfOrderCode].Caption = "code";
            grf.Cols[colgrfOrderName].Caption = "name";
            grf.Cols[colgrfOrderQty].Caption = "qty";
            grf.Cols[colgrfOrderReqNO].Caption = "reqno";

            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grf.Cols[colgrfOrderCode].DataType = typeof(String);
            grf.Cols[colgrfOrderName].DataType = typeof(String);
            grf.Cols[colgrfOrderQty].DataType = typeof(String);

            grf.Cols[colgrfOrderCode].TextAlign = TextAlignEnum.CenterCenter;
            grf.Cols[colgrfOrderName].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfOrderQty].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfOrderReqNO].TextAlign = TextAlignEnum.CenterCenter;

            grf.Cols[colgrfOrderCode].Visible = true;
            grf.Cols[colgrfOrderName].Visible = true;
            grf.Cols[colgrfOrderStatus].Visible = true;
            grf.Cols[colgrfOrderID].Visible = false;
            grf.Cols[colgrfOrdFlagSave].Visible = false;
            grf.Cols[colgrfOrdControlRemark].Visible = false;
            grf.Cols[colgrfOrdControlYear].Visible = false;
            grf.Cols[colgrfOrdStatusControl].Visible = false;
            grf.Cols[colgrfOrdPassSupervisor].Visible = false;
            grf.Cols[colgrfOrdSupervisor].Visible = false;
            if (grfname.Equals("grfOrder"))
            {
                grf.Cols[colgrfOrderQty].Visible = true;
            }
            else
            {
                grf.Cols[colgrfOrderQty].Visible = false;
            }
            grf.Cols[colgrfOrderCode].AllowEditing = false;
            grf.Cols[colgrfOrderName].AllowEditing = false;
            grf.Cols[colgrfOrderReqNO].AllowEditing = false;
            grf.DoubleClick += GrfOrder_DoubleClick;
            grf.Click += GrfOrder_Click1;
            grf.AllowSorting = AllowSortingEnum.None;
            pn.Controls.Add(grf);
            theme1.SetTheme(grf, bc.iniC.themeApp);
        }

        private void GrfOrder_Click1(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Name.Equals("grfOrder"))
            {
                String remark = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrdControlRemark]?.ToString() ?? "";
                lstAutoComplete.Items.Clear();
                // เพิ่มเฉพาะตอนที่มีค่า
                if (!string.IsNullOrWhiteSpace(remark))
                {
                    string[] lines = remark.Split(new[] { "\r\n", "\n", "\r" },  StringSplitOptions.RemoveEmptyEntries );
                    foreach (string line in lines)
                    {
                        string trimmedLine = line.Trim();
                        if (!string.IsNullOrWhiteSpace(trimmedLine))    {   lstAutoComplete.Items.Add($"{trimmedLine}");    }
                    }
                }
            }
        }

        private void GrfOrder_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row<=0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;

            if (((C1FlexGrid)sender).Name.Equals("grfApmOrder"))
            {
                String code = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderCode].ToString();
                String re = bc.bcDB.pt07DB.deleteOrderApm(txtApmDocYear.Text.Trim(), txtApmNO.Text.Trim(), code);
                ((C1FlexGrid)sender).Rows.Remove(((C1FlexGrid)sender).Row);
            }else if (((C1FlexGrid)sender).Name.Equals("grfOrder"))
            {
                String id = "";
                id = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderID]?.ToString()??"";
                ((C1FlexGrid)sender).Rows.Remove(((C1FlexGrid)sender).Row);
                lstAutoComplete.Items.Clear();
                if (id.Length > 0)
                {
                    String re = bc.bcDB.vsDB.deleteOrderTemp(id);
                    setGrfOrderTemp();
                }
            }
        }
        private void initGrfPttApm(ref C1FlexGrid grf, ref Panel pn, String grfname)
        {
            grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);
            grf.Rows.Count = 1;
            grf.Cols.Count = 19;
            grf.Name = grfname;

            grf.Cols[colgrfPttApmVsDate].Width = 100;
            grf.Cols[colgrfPttApmApmDateShow].Width = 100;
            grf.Cols[colgrfPttApmApmTime].Width = 60;
            grf.Cols[colgrfPttApmNote].Width = 500;
            grf.Cols[colgrfPttApmOrder].Width = 500;
            grf.Cols[colgrfPttApmHN].Width = 80;
            grf.Cols[colgrfPttApmPttName].Width = 250;
            grf.Cols[colgrfPttApmDeptR].Width = 120;
            grf.Cols[colgrfPttApmDeptMake].Width = 150;

            grf.ShowCursor = true;
            grf.Cols[colgrfPttApmVsDate].Caption = "date";
            grf.Cols[colgrfPttApmApmDateShow].Caption = "นัดวันที่";
            grf.Cols[colgrfPttApmApmTime].Caption = "นัดเวลา";
            grf.Cols[colgrfPttApmDeptR].Caption = "นัดตรวจที่แผนก";
            grf.Cols[colgrfPttApmDeptMake].Caption = "แผนกทำนัด";
            grf.Cols[colgrfPttApmNote].Caption = "รายละเอียด";
            grf.Cols[colgrfPttApmOrder].Caption = "Order";

            grf.Cols[colgrfPttApmApmDateShow].DataType = typeof(String);
            grf.Cols[colgrfPttApmApmTime].DataType = typeof(String);
            grf.Cols[colgrfPttApmDeptR].DataType = typeof(String);
            grf.Cols[colgrfPttApmNote].DataType = typeof(String);
            grf.Cols[colgrfPttApmOrder].DataType = typeof(String);
            grf.Cols[colgrfPttApmHN].DataType = typeof(String);
            grf.Cols[colgrfPttApmPttName].DataType = typeof(String);
            grf.Cols[colgrfPttApmDeptMake].DataType = typeof(String);

            grf.Cols[colgrfPttApmApmDateShow].TextAlign = TextAlignEnum.CenterCenter;
            grf.Cols[colgrfPttApmApmTime].TextAlign = TextAlignEnum.CenterCenter;
            grf.Cols[colgrfPttApmDeptR].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfPttApmDeptMake].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfPttApmNote].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfPttApmOrder].TextAlign = TextAlignEnum.LeftCenter;

            grf.Cols[colgrfPttApmVsDate].Visible = true;
            grf.Cols[colgrfPttApmApmDateShow].Visible = true;
            grf.Cols[colgrfPttApmDeptR].Visible = true;
            grf.Cols[colgrfPttApmNote].Visible = true;
            grf.Cols[colgrfPttApmDocNo].Visible = false;
            grf.Cols[colgrfPttApmDocYear].Visible = false;
            grf.Cols[colgrfPttApmVsDate].Visible = false;
            grf.Cols[colgrfPttApmHN].Visible = false;
            grf.Cols[colgrfPttApmPttName].Visible = true;
            grf.Cols[colgrfPttApmApmDate1].Visible = false;

            grf.Cols[colgrfPttApmVsDate].AllowEditing = false;
            grf.Cols[colgrfPttApmApmDateShow].AllowEditing = false;
            grf.Cols[colgrfPttApmDeptR].AllowEditing = false;
            grf.Cols[colgrfPttApmNote].AllowEditing = false;
            grf.Cols[colgrfPttApmApmTime].AllowEditing = false;
            grf.Cols[colgrfPttApmOrder].AllowEditing = false;
            grf.Cols[colgrfPttApmDeptMake].AllowEditing = false;
            
            if(grf.Name.Equals("grfPttApm")) grfPttApm.Click += GrfPttApm_Click;

            if (grf.Name.Equals("grfApm")) {
                ContextMenu menuGw = new ContextMenu();
                menuGw.MenuItems.Add("ต้องการออกvisit ตามนัด", new EventHandler(ContextMenu_VisitNew));
                menuGw.MenuItems.Add("แก้ไขนัด", new EventHandler(ContextMenu_EditAppoinment));
                grfApm.ContextMenu = menuGw;
                grfApm.DoubleClick += GrfApm_DoubleClick; 
            }
            else if (grf.Name.Equals("grfPttApm"))
            {
                ContextMenu menuGw = new ContextMenu();
                menuGw.MenuItems.Add("ต้องการยกเลิก ใบนัดพบแพพย์", new EventHandler(ContextMenu_VoidAppoinment));
                //menuGw.MenuItems.Add("Download Certificate Medical", new EventHandler(ContextMenu_CertiMedical_Download));
                grfPttApm.ContextMenu = menuGw;
            }
            pn.Controls.Add(grf);
            theme1.SetTheme(grf, bc.iniC.themeApp);
        }
        private void ContextMenu_EditAppoinment(object sender, System.EventArgs e)
        {
            String docyear = "", docno = "";
            try
            {
                PatientT07 apm = new PatientT07();
                docno = grfApm[grfApm.Row, colgrfPttApmDocNo].ToString();
                docyear = grfApm[grfApm.Row, colgrfPttApmDocYear].ToString();
                apm = bc.bcDB.pt07DB.selectAppointment(docyear, docno);
                FrmApmVisitNew frm = new FrmApmVisitNew(bc, apm,"edit");
                frm.ShowDialog(this);
                frm.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
        private void ContextMenu_VoidAppoinment(object sender, System.EventArgs e)
        {
            String docyear = "", docno = "";
            try
            {
                docno = grfPttApm[grfPttApm.Row, colgrfPttApmDocNo]?.ToString()??"";
                docyear = grfPttApm[grfPttApm.Row, colgrfPttApmDocYear]?.ToString()??"";
                if (MessageBox.Show("ต้องการยกเลิก ใบนัดพบแพพย์ เลขที่ "+ docyear.Substring(2) +"-"+ docno, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //docno = grfPttApm[grfPttApm.Row, colgrfPttApmDocNo].ToString();
                    String re = bc.bcDB.pt07DB.voidAppoinment(docyear, docno);
                    setGrfPttApm();
                    clearControlTabApm(false);
                }
            }
            catch(Exception ex)
            {

            }
        }
        private void ContextMenu_VisitNew(object sender, System.EventArgs e)
        {
            PatientT07 apm = new PatientT07();
            String docyear="", docno = "";
            docyear = grfApm[grfApm.Row, colgrfPttApmDocYear].ToString();
            docno = grfApm[grfApm.Row, colgrfPttApmDocNo].ToString();
            apm = bc.bcDB.pt07DB.selectAppointment(docyear, docno);
            FrmApmVisitNew frm = new FrmApmVisitNew(bc, apm);
            frm.ShowDialog(this);
            frm.Dispose();
        }
        private void GrfApm_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void GrfPttApm_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;
            if (((C1FlexGrid)sender).Name.Equals("grfPttApm"))
            {
                String apmno = "", apmyear = "";
                apmno = grfPttApm[grfPttApm.Row, colgrfPttApmDocNo].ToString();//colgrfPttApmDocNo
                apmyear = grfPttApm[grfPttApm.Row, colgrfPttApmDocYear].ToString();
                setControlApm(apmyear, apmno);
            }else if (((C1FlexGrid)sender).Name.Equals("grfApm"))
            {

            }
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
        private void setGrfPttApm()
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.pt07DB.selectByHnAll(txtOperHN.Text.Trim(), "desc");
            grfPttApm.Rows.Count = 1;
            grfPttApm.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1, row = grfPttApm.Rows.Count;
            String time = "";
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfPttApm.Rows[i];
                rowa[colgrfPttApmApmDateShow] = bc.datetoShow1(row1["MNC_APP_DAT"].ToString());
                rowa[colgrfPttApmApmTime] = bc.showTime(row1["MNC_APP_TIM"].ToString());
                rowa[colgrfPttApmDeptR] = row1["mnc_md_dep_dsc"].ToString();//นัดตรวจที่แผนก
                rowa[colgrfPttApmDeptMake] = bc.bcDB.pm32DB.getDeptNameOPD(row1["mnc_sec_no"].ToString());
                rowa[colgrfPttApmNote] = row1["MNC_APP_DSC"].ToString();
                rowa[colgrfPttApmOrder] = row1["MNC_REM_MEMO"].ToString();

                rowa[colgrfPttApmDocNo] = row1["MNC_DOC_NO"].ToString();
                rowa[colgrfPttApmDocYear] = row1["MNC_DOC_YR"].ToString();
                rowa[0] = i.ToString();
                i++;
            }
            if (tabApm.IsSelected)
            {
                txtApmDtr.Value = txtOperDtr.Text.Trim();
                lbApmDtrName.Text = lbOperDtrName.Text.Trim();
            }
        }
        private void initGrfOperFinish()
        {
            grfOperFinish = new C1FlexGrid();
            grfOperFinish.Font = fEdit;
            grfOperFinish.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOperFinish.Location = new System.Drawing.Point(0, 0);
            grfOperFinish.Rows.Count = 1;
            grfOperFinish.Cols.Count = 13;
            grfOperFinish.Cols[colgrfOperListHn].Width = 100;
            grfOperFinish.Cols[colgrfOperListFullNameT].Width = 200;
            grfOperFinish.Cols[colgrfOperListSymptoms].Width = 150;
            grfOperFinish.Cols[colgrfOperListPaidName].Width = 100;
            grfOperFinish.Cols[colgrfOperListPreno].Width = 100;
            grfOperFinish.Cols[colgrfOperListVsDate].Width = 100;
            grfOperFinish.Cols[colgrfOperListVsTime].Width = 70;
            grfOperFinish.Cols[colgrfOperListActNo].Width = 100;
            grfOperFinish.Cols[colgrfOperListDtrName].Width = 100;
            grfOperFinish.Cols[colgrfOperListLab].Width = 50;
            grfOperFinish.Cols[colgrfOperListXray].Width = 50;
            grfOperFinish.ShowCursor = true;
            grfOperFinish.Cols[colgrfOperListHn].Caption = "HN";
            grfOperFinish.Cols[colgrfOperListFullNameT].Caption = "ชื่อ-นามสกุล";
            grfOperFinish.Cols[colgrfOperListSymptoms].Caption = "อาการ";
            grfOperFinish.Cols[colgrfOperListPaidName].Caption = "สิทธิ";
            grfOperFinish.Cols[colgrfOperListPreno].Caption = "preno";
            grfOperFinish.Cols[colgrfOperListVsDate].Caption = "วันที่";
            grfOperFinish.Cols[colgrfOperListVsTime].Caption = "เวลา";
            grfOperFinish.Cols[colgrfOperListActNo].Caption = "สถานะ";
            grfOperFinish.Cols[colgrfOperListDtrName].Caption = "แพทย์";
            grfOperFinish.Cols[colgrfOperListLab].Caption = "lab";
            grfOperFinish.Cols[colgrfOperListXray].Caption = "xray";
            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grfOperFinish.Cols[colgrfOperListHn].DataType = typeof(String);
            grfOperFinish.Cols[colgrfOperListVsDate].DataType = typeof(String);
            grfOperFinish.Cols[colgrfOperListVsTime].DataType = typeof(String);
            grfOperFinish.Cols[colgrfOperListActNo].DataType = typeof(String);
            grfOperFinish.Cols[colgrfOperListDtrName].DataType = typeof(String);
            grfOperFinish.Cols[colgrfOperListLab].DataType = typeof(String);
            grfOperFinish.Cols[colgrfOperListXray].DataType = typeof(String);

            grfOperFinish.Cols[colgrfOperListHn].TextAlign = TextAlignEnum.CenterCenter;
            grfOperFinish.Cols[colgrfOperListVsTime].TextAlign = TextAlignEnum.CenterCenter;
            grfOperFinish.Cols[colgrfOperListActNo].TextAlign = TextAlignEnum.LeftCenter;
            grfOperFinish.Cols[colgrfOperListDtrName].TextAlign = TextAlignEnum.LeftCenter;
            grfOperFinish.Cols[colgrfOperListLab].TextAlign = TextAlignEnum.CenterCenter;
            grfOperFinish.Cols[colgrfOperListXray].TextAlign = TextAlignEnum.CenterCenter;

            grfOperFinish.Cols[colgrfOperListPreno].Visible = false;
            grfOperFinish.Cols[colgrfOperListVsDate].Visible = false;
            grfOperFinish.Cols[colgrfOperListHn].Visible = false;

            grfOperFinish.Cols[colgrfOperListHn].AllowEditing = false;
            grfOperFinish.Cols[colgrfOperListFullNameT].AllowEditing = false;
            grfOperFinish.Cols[colgrfOperListSymptoms].AllowEditing = false;
            grfOperFinish.Cols[colgrfOperListPaidName].AllowEditing = false;
            grfOperFinish.Cols[colgrfOperListPreno].AllowEditing = false;
            grfOperFinish.Cols[colgrfOperListVsDate].AllowEditing = false;
            grfOperFinish.Cols[colgrfOperListVsTime].AllowEditing = false;
            grfOperFinish.Cols[colgrfOperListActNo].AllowEditing = false;
            grfOperFinish.Cols[colgrfOperListDtrName].AllowEditing = false;
            grfOperFinish.Cols[colgrfOperListLab].AllowEditing = false;
            grfOperFinish.Cols[colgrfOperListXray].AllowEditing = false;

            grfOperFinish.AfterRowColChange += GrfOperFinish_AfterRowColChange;
            //grfCheckUPList.AllowFiltering = true;

            pnOperFinish.Controls.Add(grfOperFinish);
            theme1.SetTheme(grfOperFinish, bc.iniC.themeApp);
        }
        private void GrfOperFinish_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            String hn = "", preno = "", vsdate = "";
            hn = grfOperFinish[grfOperFinish.Row, colgrfOperListHn] != null ? grfOperFinish[grfOperFinish.Row, colgrfOperListHn].ToString() : "";
            preno = grfOperFinish[grfOperFinish.Row, colgrfOperListPreno] != null ? grfOperFinish[grfOperFinish.Row, colgrfOperListPreno].ToString() : "";
            vsdate = grfOperFinish[grfOperFinish.Row, colgrfOperListVsDate] != null ? grfOperFinish[grfOperFinish.Row, colgrfOperListVsDate].ToString() : "";
            setControlTabFinish(hn,vsdate, preno);
        }
        private void setControlTabFinish(String hn, String vsdate, String preno)
        {
            try
            {
                HN = hn;
                PRENO = preno;
                VSDATE = vsdate;
                setStaffNote(VSDATE, PRENO, picFinishL, picFinishR);
                if (grfOperFinishLab != null) setGrfLab(HN, VSDATE, PRENO, ref grfOperFinishLab);       //LAB
                setGrfHisOrder(HN, VSDATE, PRENO, ref grfOperFinishDrug);                               //DRUG
                if (grfOperFinishXray != null) setGrfXray(HN, VSDATE, PRENO, ref grfOperFinishXray);    //XRAY
                if (TCFinishActive.Equals(tabFinishCertMed.Name)) setCertiMed();                        //cert med
                setGrfHisProcedure(HN, VSDATE, PRENO, ref grfOperFinishProcedure);                      //Procedure
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FRMOPD GrfOperFinish_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FRMOPD GrfOperFinish_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            finally
            {
                //frmFlash.Dispose();
            }
        }
        private void setGrfOperFinish()
        {
            if (pageLoad) return;
            pageLoad = true;
            timeOperList.Enabled = false;
            DataTable dtvs = new DataTable();
            String deptno = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
            String vsdate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd");
            dtvs = bc.bcDB.vsDB.selectPttinDeptActNo110(deptno, bc.iniC.station, vsdate, vsdate);

            grfOperFinish.Rows.Count = 1;
            grfOperFinish.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1, row = grfOperFinish.Rows.Count;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfOperFinish.Rows[i];
                rowa[colgrfOperListHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfOperListFullNameT] = row1["ptt_fullnamet"].ToString();
                rowa[colgrfOperListSymptoms] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colgrfOperListPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[colgrfOperListPreno] = row1["MNC_PRE_NO"].ToString();

                rowa[colgrfOperListVsDate] = row1["MNC_DATE"].ToString();
                rowa[colgrfOperListVsTime] = bc.showTime(row1["MNC_TIME"].ToString());
                rowa[colgrfOperListActNo] = bc.adjustACTNO(row1["MNC_ACT_NO"].ToString());
                rowa[colgrfOperListDtrName] = row1["dtr_name"].ToString();
                if (row1["MNC_ACT_NO"].ToString().Equals("110")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor); }
                else if (row1["MNC_ACT_NO"].ToString().Equals("114")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6"); }

                rowa[0] = i.ToString();
                i++;
            }
            timeOperList.Enabled = true;
            pageLoad = false;
        }
        private void initGrfOperFinishDrug()
        {
            grfOperFinishDrug = new C1FlexGrid();
            grfOperFinishDrug.Font = fEdit;
            grfOperFinishDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOperFinishDrug.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grfOperFinishDrug.Cols[colOrderId].Visible = false;
            grfOperFinishDrug.Rows.Count = 1;
            grfOperFinishDrug.Cols.Count = 8;
            grfOperFinishDrug.Cols[colOrderName].Caption = "Drug Name";
            grfOperFinishDrug.Cols[colOrderMed].Caption = "MED";
            grfOperFinishDrug.Cols[colOrderQty].Caption = "QTY";
            grfOperFinishDrug.Cols[colOrderDate].Caption = "Date";
            grfOperFinishDrug.Cols[colOrderFre].Caption = "วิธีใช้";
            grfOperFinishDrug.Cols[colOrderIn1].Caption = "ข้อควรระวัง";
            grfOperFinishDrug.Cols[colOrderName].Width = 400;
            grfOperFinishDrug.Cols[colOrderMed].Width = 200;
            grfOperFinishDrug.Cols[colOrderQty].Width = 60;
            grfOperFinishDrug.Cols[colOrderDate].Width = 90;
            grfOperFinishDrug.Cols[colOrderFre].Width = 500;
            grfOperFinishDrug.Cols[colOrderIn1].Width = 350;
            grfOperFinishDrug.Cols[colOrderName].AllowEditing = false;
            grfOperFinishDrug.Cols[colOrderQty].AllowEditing = false;
            grfOperFinishDrug.Cols[colOrderMed].AllowEditing = false;
            grfOperFinishDrug.Cols[colOrderFre].AllowEditing = false;
            grfOperFinishDrug.Cols[colOrderIn1].AllowEditing = false;
            grfOperFinishDrug.Cols[colOrderDate].AllowEditing = false;
            grfOperFinishDrug.Name = "grfOperFinishDrug";
            pnFinishDrug.Controls.Add(grfOperFinishDrug);
        }
        private void initGrfOperFinishProcedure()
        {
            grfOperFinishProcedure = new C1FlexGrid();
            grfOperFinishProcedure.Font = fEdit;
            grfOperFinishProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOperFinishProcedure.Location = new System.Drawing.Point(0, 0);
            grfOperFinishProcedure.Rows.Count = 1;
            grfOperFinishProcedure.Cols.Count = 5;
            grfOperFinishProcedure.Cols[colHisProcCode].Width = 100;
            grfOperFinishProcedure.Cols[colHisProcName].Width = 200;
            grfOperFinishProcedure.Cols[colHisProcReqDate].Width = 100;
            grfOperFinishProcedure.Cols[colHisProcReqTime].Width = 100;

            grfOperFinishProcedure.ShowCursor = true;
            grfOperFinishProcedure.Cols[colHisProcCode].Caption = "CODE";
            grfOperFinishProcedure.Cols[colHisProcName].Caption = "Procedure Name";
            grfOperFinishProcedure.Cols[colHisProcReqDate].Caption = "req date";
            grfOperFinishProcedure.Cols[colHisProcReqTime].Caption = "req time";

            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grfOperFinishProcedure.Cols[colHisProcCode].DataType = typeof(String);
            grfOperFinishProcedure.Cols[colHisProcName].DataType = typeof(String);
            grfOperFinishProcedure.Cols[colHisProcReqDate].DataType = typeof(String);
            grfOperFinishProcedure.Cols[colHisProcReqTime].DataType = typeof(String);

            grfOperFinishProcedure.Cols[colHisProcCode].TextAlign = TextAlignEnum.CenterCenter;
            grfOperFinishProcedure.Cols[colHisProcName].TextAlign = TextAlignEnum.LeftCenter;
            grfOperFinishProcedure.Cols[colHisProcReqDate].TextAlign = TextAlignEnum.CenterCenter;
            grfOperFinishProcedure.Cols[colHisProcReqTime].TextAlign = TextAlignEnum.CenterCenter;

            grfOperFinishProcedure.Cols[colgrfOrderCode].Visible = true;
            grfOperFinishProcedure.Cols[colgrfOrderName].Visible = true;
            grfOperFinishProcedure.Cols[colHisProcReqTime].Visible = false;

            grfOperFinishProcedure.Cols[colHisProcCode].AllowEditing = false;
            grfOperFinishProcedure.Cols[colHisProcName].AllowEditing = false;
            grfOperFinishProcedure.Cols[colHisProcReqDate].AllowEditing = false;
            grfOperFinishProcedure.Cols[colHisProcReqTime].AllowEditing = false;

            pnFinishProcedure.Controls.Add(grfOperFinishProcedure);
            theme1.SetTheme(grfOperFinishProcedure, bc.iniC.themeApp);
        }
        private void initGrfOperList()
        {
            grfOperList = new C1FlexGrid();
            grfOperList.Font = fEdit;
            grfOperList.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOperList.Location = new System.Drawing.Point(0, 0);
            grfOperList.Rows.Count = 1;
            grfOperList.Cols.Count = 13;
            grfOperList.Cols[colgrfOperListHn].Width = 80;
            grfOperList.Cols[colgrfOperListVN].Width = 60;
            grfOperList.Cols[colgrfOperListFullNameT].Width = 200;
            grfOperList.Cols[colgrfOperListSymptoms].Width = 150;
            grfOperList.Cols[colgrfOperListPaidName].Width = 100;
            grfOperList.Cols[colgrfOperListPreno].Width = 100;
            grfOperList.Cols[colgrfOperListVsDate].Width = 100;
            grfOperList.Cols[colgrfOperListVsTime].Width = 70;
            grfOperList.Cols[colgrfOperListActNo].Width = 100;
            grfOperList.Cols[colgrfOperListDtrName].Width = 100;
            grfOperList.Cols[colgrfOperListLab].Width = 50;
            grfOperList.Cols[colgrfOperListXray].Width = 50;
            grfOperList.ShowCursor = true;
            grfOperList.Cols[colgrfOperListHn].Caption = "HN";
            grfOperList.Cols[colgrfOperListFullNameT].Caption = "ชื่อ-นามสกุล";
            grfOperList.Cols[colgrfOperListSymptoms].Caption = "อาการ";
            grfOperList.Cols[colgrfOperListPaidName].Caption = "สิทธิ";
            grfOperList.Cols[colgrfOperListPreno].Caption = "preno";
            grfOperList.Cols[colgrfOperListVsDate].Caption = "วันที่";
            grfOperList.Cols[colgrfOperListVsTime].Caption = "เวลา";
            grfOperList.Cols[colgrfOperListActNo].Caption = "สถานะ";
            grfOperList.Cols[colgrfOperListDtrName].Caption = "แพทย์";
            grfOperList.Cols[colgrfOperListLab].Caption = "lab";
            grfOperList.Cols[colgrfOperListXray].Caption = "xray";
            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grfOperList.Cols[colgrfOperListHn].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListVsDate].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListVsTime].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListActNo].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListDtrName].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListLab].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListXray].DataType = typeof(String);

            grfOperList.Cols[colgrfOperListHn].TextAlign = TextAlignEnum.CenterCenter;
            grfOperList.Cols[colgrfOperListVsTime].TextAlign = TextAlignEnum.CenterCenter;
            grfOperList.Cols[colgrfOperListActNo].TextAlign = TextAlignEnum.LeftCenter;
            grfOperList.Cols[colgrfOperListDtrName].TextAlign = TextAlignEnum.LeftCenter;
            grfOperList.Cols[colgrfOperListLab].TextAlign = TextAlignEnum.CenterCenter;
            grfOperList.Cols[colgrfOperListXray].TextAlign = TextAlignEnum.CenterCenter;

            grfOperList.Cols[colgrfOperListPreno].Visible = false;
            grfOperList.Cols[colgrfOperListVsDate].Visible = false;
            grfOperList.Cols[colgrfOperListHn].Visible = true;

            grfOperList.Cols[colgrfOperListHn].AllowEditing = false;
            grfOperList.Cols[colgrfOperListFullNameT].AllowEditing = false;
            grfOperList.Cols[colgrfOperListSymptoms].AllowEditing = false;
            grfOperList.Cols[colgrfOperListPaidName].AllowEditing = false;
            grfOperList.Cols[colgrfOperListPreno].AllowEditing = false;
            grfOperList.Cols[colgrfOperListVsDate].AllowEditing = false;
            grfOperList.Cols[colgrfOperListVsTime].AllowEditing = false;
            grfOperList.Cols[colgrfOperListActNo].AllowEditing = false;
            grfOperList.Cols[colgrfOperListDtrName].AllowEditing = false;
            grfOperList.Cols[colgrfOperListLab].AllowEditing = false;
            grfOperList.Cols[colgrfOperListXray].AllowEditing = false;
            grfOperList.Name = "grfOperList";
            grfOperList.AfterRowColChange += GrfOperList_AfterRowColChange;
            //grfCheckUPList.AllowFiltering = true;

            pnOperList.Controls.Add(grfOperList);
            theme1.SetTheme(grfOperList, bc.iniC.themeApp);
        }

        private void GrfOperList_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            if (grfOperList.Row <= 0) return;
            if (grfOperList.Col <= 0) return;
            if (grfOperList[grfOperList.Row, colgrfOperListPreno] == null) return;
            if (grfOperList.Row == ROWGrfOper) return;
            PRENO = grfOperList[grfOperList.Row, colgrfOperListPreno].ToString();
            VSDATE = grfOperList[grfOperList.Row, colgrfOperListVsDate].ToString();
            HN = grfOperList[grfOperList.Row, colgrfOperListHn].ToString();
            setControlOper(grfOperList.Name);
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
            lfSbStatus.Text = "";
            lfSbMessage.Text = "";
            lbOperCodeApprove.Visible = false;
            txtOperCodeApprove.Visible = false;
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
                    PRENO = VS.preno;
                    VSDATE = VS.VisitDate;
                    HN = VS.HN;
                    VN = VS.VN;
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
                txtOperLeftEar.Value = "";
                txtOperRightEar.Value = "";
                txtOperLeftEarOther.Value = "";
                txtOperRightEarOther.Value = "";
                txtOperLeftEye.Value = "";
                txtOperRightEye.Value = "";
                txtOperLeftEyePh.Value = "";
                txtOperRightEyePh.Value = "";
                cboOperEye.Value = "";
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

        private void setGrfOperList(String flagsearch)
        {
            if (pageLoad) return;
            if(grfOperList == null) return;
            if (grfOperList.Rows == null) return;
            pageLoad = true;
            timeOperList.Enabled = false;
            string criteria = ((ComboBoxItem)cboOperCritiria.SelectedItem).Value?.ToString() ?? "";
            DataTable dtvs = new DataTable();
            String deptno = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
            String vsdate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd");
            DateTime dateTime = DateTime.Now.AddDays(-1);
            //vsdate = dateTime.Year.ToString() + "-" + dateTime.ToString("MM-dd");
            if(bc.iniC.branchId.Equals("005"))
                dtvs = bc.bcDB.vsDB.selectPttinDeptActNo101(deptno,bc.iniC.station,vsdate, vsdate, txtOperHN.Text.Trim(), criteria.Length>0?criteria: flagsearch);
            else
                dtvs = bc.bcDB.vsDB.selectPttinDeptActNo101(deptno, "", vsdate, vsdate, txtOperHN.Text.Trim(), criteria.Length > 0 ? criteria : flagsearch);
            grfOperList.Rows.Count = 1; grfOperList.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1, row = grfOperList.Rows.Count;
            foreach (DataRow row1 in dtvs.Rows)
            {
                TimeSpan curtime = new TimeSpan();
                TimeSpan vstime = new TimeSpan();
                curtime = TimeSpan.Parse(row1["cur_time"].ToString());
                vstime = TimeSpan.Parse(bc.showTime(row1["MNC_TIME"].ToString()));
                Row rowa = grfOperList.Rows[i];
                rowa[colgrfOperListHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfOperListVN] = row1["MNC_VN_NO"].ToString()+"."+ row1["MNC_VN_SEQ"].ToString()+"."+ row1["MNC_VN_SUM"].ToString();
                rowa[colgrfOperListFullNameT] = row1["ptt_fullnamet"].ToString();
                rowa[colgrfOperListSymptoms] = row1["MNC_SHIF_MEMO"].ToString().Replace(Environment.NewLine,"");
                rowa[colgrfOperListPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[colgrfOperListPreno] = row1["MNC_PRE_NO"].ToString();

                rowa[colgrfOperListVsDate] = row1["MNC_DATE"].ToString();
                rowa[colgrfOperListVsTime] = bc.showTime(row1["MNC_TIME"].ToString());
                rowa[colgrfOperListActNo] = bc.adjustACTNO(row1["MNC_ACT_NO"].ToString());
                rowa[colgrfOperListDtrName] = row1["dtr_name"].ToString();
                if (row1["MNC_ACT_NO"].ToString().Equals("110")) 
                {
                    TimeSpan difference = curtime - vstime;
                    if (difference.TotalMinutes >= 5)
                    {
                        rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#DDA0DD"); // Lavender อ่อน
                    }
                    else if (difference.TotalMinutes >= 3)
                    {
                        rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#E6E6FA"); // Plum
                    }
                    else
                    {
                        rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                    }
                    CellNote note = new CellNote(rowa[colgrfOperListActNo].ToString() +Environment.NewLine+ "VN "+rowa[colgrfOperListVN].ToString() + Environment.NewLine +"แพทย์ "+ rowa[colgrfOperListDtrName].ToString() + Environment.NewLine + "เวลา " + rowa[colgrfOperListVsTime].ToString() + Environment.NewLine + "HN " + rowa[colgrfOperListHn].ToString() + Environment.NewLine + rowa[colgrfOperListSymptoms].ToString());
                    CellRange rg = grfOperList.GetCellRange(i, colgrfOperListFullNameT);
                    rg.UserData = note;
                }
                else if (row1["MNC_ACT_NO"].ToString().Equals("114")) 
                { 
                    rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
                    CellNote note = new CellNote(rowa[colgrfOperListActNo].ToString()+" "+ rowa[colgrfOperListVN].ToString());
                    CellRange rg = grfOperList.GetCellRange(i, colgrfOperListFullNameT);
                    rg.UserData = note;
                }
                rowa[0] = i.ToString();
                i++;
            }
            CellNoteManager mgr = new CellNoteManager(grfOperList);
            timeOperList.Enabled = true;
            pageLoad = false;
        }
        private void initGrfCheckUPList()
        {
            grfCheckUPList = new C1FlexGrid();
            grfCheckUPList.Font = fEdit;
            grfCheckUPList.Dock = System.Windows.Forms.DockStyle.Fill;
            grfCheckUPList.Location = new System.Drawing.Point(0, 0);
            grfCheckUPList.Rows.Count = 1;
            grfCheckUPList.Cols.Count = 7;
            grfCheckUPList.Cols[colgrfCheckUPHn].Width = 100;
            grfCheckUPList.Cols[colgrfCheckUPFullNameT].Width = 200;
            grfCheckUPList.Cols[colgrfCheckUPSymtom].Width = 150;
            grfCheckUPList.Cols[colgrfCheckUPEmployer].Width = 100;
            grfCheckUPList.ShowCursor = true;
            grfCheckUPList.Cols[colgrfCheckUPHn].Caption = "HN";
            grfCheckUPList.Cols[colgrfCheckUPFullNameT].Caption = "full name";
            grfCheckUPList.Cols[colgrfCheckUPSymtom].Caption = "อาการ";
            grfCheckUPList.Cols[colgrfCheckUPEmployer].Caption = "นายจ้าง";

            grfCheckUPList.Cols[colgrfCheckUPVsDate].Visible = false;
            grfCheckUPList.Cols[colgrfCheckUPPreno].Visible = false;

            grfCheckUPList.Cols[colgrfCheckUPHn].AllowEditing = false;
            grfCheckUPList.Cols[colgrfCheckUPFullNameT].AllowEditing = false;
            grfCheckUPList.Cols[colgrfCheckUPSymtom].AllowEditing = false;
            grfCheckUPList.Cols[colgrfCheckUPEmployer].AllowEditing = false;
            grfCheckUPList.AllowFiltering = true;

            grfCheckUPList.Click += GrfCheckUPList_Click;

            spCheckUpList.Controls.Add(grfCheckUPList);
            theme1.SetTheme(grfCheckUPList, bc.iniC.themeApp);
        }
        private void setGrfCheckUPList(String flagselect)
        {
            showLbLoading();
            DateTime datestar = DateTime.Now;
            DateTime dateend = DateTime.Now;
            if (datestar.Year < 1900)            {                datestar = datestar.AddYears(543);            }
            else if (dateend.Year < 1900)            {                dateend = dateend.AddYears(543);            }
            DataTable dtcheckup = new DataTable();
            if (flagselect.Length == 0)
            {
                dtcheckup = bc.bcDB.vsDB.selectPttinDeptOrderByNameT(bc.iniC.sectioncheckup, datestar.Year.ToString() + "-" + datestar.ToString("MM-dd"), dateend.Year.ToString() + "-" + dateend.ToString("MM-dd"));
            }
            else if (flagselect.Equals("sso"))
            {
                dtcheckup = bc.bcDB.vsDB.selectPttinCheckSSO(datestar.Year.ToString() + "-" + datestar.ToString("MM-dd"), dateend.Year.ToString() + "-" + dateend.ToString("MM-dd"));
            }
            else if (flagselect.Equals("doe"))
            {
                dtcheckup = bc.bcDB.vsDB.selectPttinCheckDOE(datestar.Year.ToString() + "-" + datestar.ToString("MM-dd"), dateend.Year.ToString() + "-" + dateend.ToString("MM-dd"));
            }
            else if (flagselect.Equals("doeonline"))
            {
                dtcheckup = bc.bcDB.vsDB.selectPttinCheckDOE(datestar.Year.ToString() + "-" + datestar.ToString("MM-dd"), dateend.Year.ToString() + "-" + dateend.ToString("MM-dd"));
            }
            int i = 1, j = 1, row = grfCheckUPList.Rows.Count;

            grfCheckUPList.Rows.Count = 1;
            grfCheckUPList.Rows.Count = dtcheckup.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dtcheckup.Rows)
            {
                //pB1.Value++;
                Row rowa = grfCheckUPList.Rows[i];
                rowa[colgrfCheckUPHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfCheckUPFullNameT] = row1["pttfullname"].ToString();
                rowa[colgrfCheckUPSymtom] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colgrfCheckUPEmployer] = "";
                rowa[colgrfCheckUPVsDate] = row1["MNC_DATE"].ToString();
                rowa[colgrfCheckUPPreno] = row1["MNC_PRE_NO"].ToString();
                rowa[0] = i.ToString();
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
            hideLbLoading();
        }
        private void GrfCheckUPList_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(grfCheckUPList==null) return;
            if(grfCheckUPList.Row<=0) return;
            if(grfCheckUPList.Col<=0) return;
            sep.Clear();
            SYMPTOMS = grfCheckUPList[grfCheckUPList.Row, colgrfCheckUPSymtom] != null ? grfCheckUPList[grfCheckUPList.Row, colgrfCheckUPSymtom].ToString() : "";
            setControlCheckUP(grfCheckUPList[grfCheckUPList.Row, colgrfCheckUPHn].ToString(), grfCheckUPList[grfCheckUPList.Row, colgrfCheckUPVsDate].ToString()
                , grfCheckUPList[grfCheckUPList.Row, colgrfCheckUPPreno].ToString());
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
                canvas.ShowTextAligned(Element.ALIGN_LEFT, "ส่วนที่ 1", 50, linenumber+=20 , 0);
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
                int year = DateTime.Now.Year<2500? DateTime.Now.Year+543: DateTime.Now.Year;

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
                canvas.ShowTextAligned(Element.ALIGN_LEFT, cboCheckUPPrefixT.Text +" "+txtCheckUPNameT.Text.Trim()+" "+ txtCheckUPSurNameT.Text.Trim(), 157, linenumber + 3, 0);

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
                if(chkCheckup62.Checked)
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
            Visit visit = new Visit();
            DataTable dtvs = new DataTable();
            VSDATE = vsdate;
            PRENO = preno;
            HN = hn;
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
            
            txtCheckUPHN.Value = PTT.MNC_HN_NO;
            txtCheckUPNameT.Value = PTT.MNC_FNAME_T;
            txtCheckUPSurNameT.Value = PTT.MNC_LNAME_T;
            txtCheckUPNameE.Value = PTT.MNC_FNAME_E;
            txtCheckUPSurNameE.Value = PTT.MNC_LNAME_E;
            txtCheckUPMobile1.Value = PTT.MNC_CUR_TEL;
            
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
            txtCheckUPABOGroup.Text = "";

            txtCheckUPAge.Text = "";
            txtCheckUPABOGroup.Text = "";
            txtCheckUPRrate.Text = "";
            txtCheckUPHrate.Text = "";
            txtCheckUPHeight.Text = "";

            txtCheckUPBp1L.Text = "";
            txtCheckUPRhgroup.Text = "";
            cboCheckUPSex.Text = "";
            txtCheckUPThaiHIV.Text = "";
            cboCheckUPThaiDiag.Text = "";
            txtCheckUPTempu.Text = "";
            txtCheckUPWeight.Text = "";
            txtCheckUPThaiSign1.Text = "";
            txtCheckUPThaiSign2.Text = "";
            txtCheckUPPttPID.Value = "";
            txtCheckUPDoctorId.Text = "";
            txtCheckUPDoctorName.Text = "";
            chkCheckUpEdit.Checked = false;
            txtCheckUPEmplyer.Text = "";
            txtCheckUPAddr1.Text = "";
            txtCheckUPAddr2.Text = "";
            txtCheckUPPhone.Text = "";
            txtCheckUPComp.Text = "";
            grfChkPackItems.Rows.Count = 1;
            clearControlCheckupSSO();
        }
        private void setControlTabOperVital(Visit vs)
        {
            txtOperHN.Value = vs.HN;
            lbOperPttNameT.Text = vs.PatientName;
            lboperAge.Text = "อายุ "+PTT.AgeStringOK1DOT();
            lboperVN.Text = "VN "+ vs.VN;
            txtOperTemp.Value = vs.temp;
            txtOperHrate.Value = vs.ratios;
            txtOperRrate.Value = vs.breath;
            //txtOperAbo.Value = vs.temp;
            txtOperRh.Value = "";
            txtOperBp1L.Value = vs.bp1l;
            txtOperBp1R.Value = vs.bp1r;
            txtOperBp2L.Value = vs.bp2l;
            txtOperBp2R.Value = vs.bp2r;
            txtOperAbc.Value = vs.abc;
            txtOperWt.Value = vs.weight;
            txtOperHt.Value = vs.high;
            txtOperHc.Value = vs.hc;
            txtOperCc.Value = vs.cc;
            txtOperCcin.Value = vs.ccin;
            txtOperCcex.Value = vs.ccex;
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
            txtOperHN.Value = "";
            lbOperPttNameT.Text = "";
            txtOperTemp.Value = "";
            txtOperHrate.Value = "";
            txtOperRrate.Value = "";
            txtOperAbo.Value = "";
            txtOperRh.Value = "";
            txtOperBp1L.Value = "";
            txtOperBp1R.Value = "";
            txtOperBp2L.Value = "";
            txtOperBp2R.Value = "";
            txtOperAbc.Value = "";
            txtOperWt.Value = "";
            txtOperHt.Value = "";
            txtOperHc.Value = "";
            txtOperCc.Value = "";
            txtOperCcin.Value = "";
            txtOperCcex.Value = "";
            txtOperDtr.Value = "";
            lbOperDtrName.Text = "";
            lbSymptoms.Text = "";
            txtOperBmi.Value = "";
            lboperAge.Text = "";
            lboperVN.Text = "";
        }
        private PatientT07 getApm()
        {
            if (VSDATE.Length <= 0) return null;
            if (PRENO.Length <= 0) return null;
            if (HN.Length <= 0) return null;
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
            txtItemCode.Value = "";
            txtSearchItem.Value = "";
            lbItemName.Text = "";
            txtItemRemark.Value = "";
        }
        private void clearControlTabApm(Boolean new1)
        {
            txtPttApmDate.Value = DateTime.Now;
            txtApmTime.Value = "";
            bc.setC1Combo(cboApmDept, "");
            txtApmDsc.Value = "";
            txtApmRemark.Value = "";
            txtApmDtr.Value = "";
            lbApmDtrName.Text = "";
            txtApmTel.Value = "";
            txtApmNO.Value = "";
            txtApmDocYear.Value = "";
            txtApmList.Value = "";
            grfApmOrder.Rows.Count = 1;
            if (!new1)
            {
                grfPttApm.Rows.Count = 1;
            }
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
                        xray.status_control = row["status_control"]?.ToString() ?? "".Trim();
                        HSPROCEDUREM01.Add(xray);
                        if (xray.status_control.Equals("1"))
                        {
                            HSPROCEDUREM01C.Add(xray);
                        }
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
            if (labItem.MNC_LB_CD != null && labItem.MNC_LB_CD.Length > 0)
            {
                LAB = labItem;
            }
            else
            {
                lfSbMessage.Text = "no item";                    txtItemCode.Value = "";                    lbItemName.Text = "";                    txtItemQTY.Value = "1";
                return;
            }
            //lab = bc.bcDB.labM01DB.SelectByPk(code);
            txtItemCode.Value = LAB.MNC_LB_CD;            lbItemName.Text = LAB.MNC_LB_DSC;            txtItemQTY.Visible = false;
            txtItemQTY.Value = "1";
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
                    DateTime dtLab1, dtLab2;
                    dtLab2 = DateTime.Now;
                    dtLab1 = new DateTime(2026, 1, 1);
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
            if((sender != null) && (sender.GetType() == typeof(Label)) && (((Label)sender).Name == lbOperItem.Name))
            {
                setGrfOrderItem();
                //ควร clear ข้อมูลด้วยเพราะ user อาจ enter ซ้ำ
                txtItemCode.Value = "";
                lbItemName.Text = "";
            }
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
            if ((sender != null) && (sender.GetType() == typeof(Label)) && (((Label)sender).Name == lbOperItem.Name))
            {
                setGrfOrderItem();
                //ควร clear ข้อมูลด้วยเพราะ user อาจ enter ซ้ำ
                txtItemCode.Value = "";
                lbItemName.Text = "";
            }
        }
        private void addItemProcedure()
        {
            setHSPROCEDUREM01();
            String name = "", code = "";
            PatientM30 pm30 = new PatientM30();
            String name1 = bc.bcDB.pm30DB.SelectNameByPk(code);
            txtItemCode.Value = code;
            lbItemName.Text = name1;
            txtItemQTY.Visible = false;
            txtItemQTY.Value = "1";
        }   
        private void addItemDRUG()
        {
            String name = "", code = "";
            PharmacyM01 drug = new PharmacyM01();
            String name1 = bc.bcDB.pharM01DB.SelectNameByPk(code);
            txtItemCode.Value = code;
            lbItemName.Text = name1;
            txtItemQTY.Visible = false;
            txtItemQTY.Value = "1";
        }
        private void setOrderItem(List<Item> items, object sender)
        {
            String name = "", code = "";
            lstAutoComplete.Items.Clear();
            lbOperCodeApprove.Visible = false;
            txtOperCodeApprove.Visible = false;
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
                addItemProcedure();
            }
            else if (chkItemDrug.Checked)
            {
                addItemDRUG();
            }
        }
        private void setHeaderHeight0()
        {
            spOper.HeaderHeight = 0;
            spCheckUP.HeaderHeight = 0;
            spScan.HeaderHeight = 0;
            scApm.HeaderHeight = 0;
            spOrder.HeaderHeight = 0;
            spOperList.SizeRatio = 25;
            spCheckUpList.SizeRatio = 25;
            spMedScan.HeaderHeight = 0;
            spHistory.HeaderHeight = 0;
            spHistoryVS.SizeRatio = 25;
            spOPDImgL.SizeRatio = 45;
            c1SplitContainer1.HeaderHeight = 0;
            spOutLab.HeaderHeight = 0;
            spOutLabList.SizeRatio = 25;
            spSearch.HeaderHeight = 0;
            spSrcStaffNote.HeaderHeight = 0;
            spTodayOutLab.HeaderHeight = 0;
            spTodayOutLabList.SizeRatio = 50;
            spOutLabList.SizeRatio = 50;
            spRpt.HeaderHeight = 0;
            spStaffNote.HeaderHeight = 0;
            spStaffNoteLeft.SizeRatio = 40;
            scFinish.HeaderHeight = 0;
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
            btnScanClearImg.Visible = false;
            btnScanGetImg.Visible = false;
            btnScanSaveImg.Visible = false;
            
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
            this.Text = "Last Update 2026-02-03-4 search staffnote";
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
            pnInformation.Width = tabOrder.Width - btnOrderSubmit.Left - btnOrderSubmit.Width - 15;
            pnInformation.Left = btnOrderSubmit.Left + btnOrderSubmit.Width + 10;
            txtCheckUPDate.Value = DateTime.Now;
            timeOperList.Enabled = true;
            lbSrcPreno.Value = "";
            lbSrcAge.Value = "";
            lbSrcVsDate.Value = "";
            lbSrcVN.Value = "";
            lvSrcPttName.Value = "";
            lbSrcHN.Value = "";
            lbPttFinNote.Text = "";
            lbDrugAllergy.Value = "";
            lbChronic.Value = "";
            lboperVN.Text = "";
            
            tCOrder.SelectedTab = tabScan;          //        เริ่มต้นที่ tab scan  เพราะ ถ้าเริ่มที่ tab history จะโหลดข้อมูลช้า
            timeOperList.Start();
        }
    }
}