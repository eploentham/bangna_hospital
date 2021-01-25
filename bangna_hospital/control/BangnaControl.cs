using bangna_hospital.objdb;
using bangna_hospital.object1;
using C1.Win.C1Document;
using C1.Win.C1Input;
using C1.Win.FlexViewer;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.control
{
    public class BangnaControl
    {
        public InitConfig iniC;
        private IniFile iniF;
        public ConnectDB conn;

        public String theme = "", userId = "", hn="", vn="", preno="", appName = "", vsdate="", operative_note_precidures_1, operative_note_finding_1, operative_note_hn, operative_note_id;
        public Color cTxtFocus;
        public Staff user;
        public Staff sStf, cStf;
        public int grdViewFontSize = 0, imggridscanwidth=0, pdfFontSize=0, pdfFontSizetitleFont = 0, pdfFontSizetxtFont = 0, pdfFontSizehdrFont = 0, pdfFontSizetxtFontB=0;

        public BangnaHospitalDB bcDB;

        public Patient sPtt;
        public Boolean ftpUsePassive = false, ftpUsePassiveLabOut = false;
        public int grfScanWidth = 0, imgScanWidth = 0, txtSearchHnLenghtStart=0, timerCheckLabOut=0, tabLabOutImageHeight = 0, tabLabOutImageWidth = 0, grfImgWidth = 0, scVssizeradio=0, imageCC_width = 0, imageME_width = 0, imageDiag_width = 0, imageCC_Height = 0, imageME_Height = 0, imageDiag_Height = 0;
        public String[] preoperation, postoperation, operation, fining, procidures;
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        public Dictionary<String, String> opBKKINSCL = new Dictionary<String, String>() { { "UCS", "สิทธิหลักประกันสุขภาพ" },{ "WEL", "สิทธิหลักประกันสุขภาพ (ยกเว้นการร่วมจ่าย)" }, { "OFC", "สิทธิข้าราชการ" }, { "SSS", "สิทธิประกันสังคม" }, { "LGO", "สิทธิ อปท" }, { "SSI", "สิทธิประกันสังคมทุพพลภาพ" } };
        public Dictionary<String, String> opBKKClinic = new Dictionary<String, String>();
        public Dictionary<String, String> opBKKCHRGITEM_CODEA = new Dictionary<String, String>();
        public BangnaControl()
        {
            initConfig();
        }
        private void initConfig()
        {
            //MessageBox.Show("h1111n ", "");
            try
            {
                appName = System.AppDomain.CurrentDomain.FriendlyName;
                appName = appName.ToLower().Replace(".exe", "");
                if (System.IO.File.Exists(Environment.CurrentDirectory + "\\" + appName + ".ini"))
                {
                    appName = Environment.CurrentDirectory + "\\" + appName + ".ini";
                }
                else
                {
                    appName = Environment.CurrentDirectory + "\\" + Application.ProductName + ".ini";
                }
                if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("bangna_hospital_scan_capture"))
                {
                    appName = "c:\\bangna_hospital.ini";
                }
                //MessageBox.Show("h1111n ", "");
                iniF = new IniFile(appName);
                iniC = new InitConfig();
                cTxtFocus = ColorTranslator.FromHtml(iniC.txtFocus);
                user = new Staff();
                sPtt = new Patient();
                sStf = new Staff();
                cStf = new Staff();

                GetConfig();
                conn = new ConnectDB(iniC);
                bcDB = new BangnaHospitalDB(conn);
                initOPBKKClinic();
                initOPBKKCHRGITEM();
            }
            catch(Exception ex)
            {
                new LogWriter("e", "BangnaControl initConfig ");
                MessageBox.Show("error "+ex.Message, "");
            }
            
        }
        public void getInit()
        {
            //bcDB.sexDB.getlSex();
            //cop = ivfDB.copDB.selectByCode1("001");
        }
        public void GetConfig()
        {
            //MessageBox.Show("hn " , "");
            iniC.hostDB = iniF.getIni("connection", "hostDB");
            iniC.nameDB = iniF.getIni("connection", "nameDB");
            iniC.userDB = iniF.getIni("connection", "userDB");
            iniC.passDB = iniF.getIni("connection", "passDB");
            iniC.portDB = iniF.getIni("connection", "portDB");

            iniC.hostDBMainHIS = iniF.getIni("connection", "hostDBMainHIS");
            iniC.nameDBMainHIS = iniF.getIni("connection", "nameDBMainHIS");
            iniC.userDBMainHIS = iniF.getIni("connection", "userDBMainHIS");
            iniC.passDBMainHIS = iniF.getIni("connection", "passDBMainHIS");
            iniC.portDBMainHIS = iniF.getIni("connection", "portDBMainHIS");

            iniC.hostDBPACs = iniF.getIni("connection", "hostDBPACs");
            iniC.nameDBPACs = iniF.getIni("connection", "nameDBPACs");
            iniC.userDBPACs = iniF.getIni("connection", "userDBPACs");
            iniC.passDBPACs = iniF.getIni("connection", "passDBPACs");
            iniC.portDBPACs = iniF.getIni("connection", "portDBPACs");

            iniC.hostDBLabOut = iniF.getIni("connection", "hostDBLabOut");
            iniC.nameDBLabOut = iniF.getIni("connection", "nameDBLabOut");
            iniC.userDBLabOut = iniF.getIni("connection", "userDBLabOut");
            iniC.passDBLabOut = iniF.getIni("connection", "passDBLabOut");
            iniC.portDBLabOut = iniF.getIni("connection", "portDBLabOut");

            iniC.hostFTP = iniF.getIni("ftp", "hostFTP");
            iniC.userFTP = iniF.getIni("ftp", "userFTP");
            iniC.passFTP = iniF.getIni("ftp", "passFTP");
            iniC.portFTP = iniF.getIni("ftp", "portFTP");
            iniC.folderFTP = iniF.getIni("ftp", "folderFTP");
            iniC.usePassiveFTP = iniF.getIni("ftp", "usePassiveFTP");
            iniC.ProxyHost = iniF.getIni("ftp", "ProxyHost");
            iniC.ProxyPort = iniF.getIni("ftp", "ProxyPort");
            iniC.ProxyProxyType = iniF.getIni("ftp", "ProxyProxyType");

            iniC.hostFTPLabOut = iniF.getIni("ftp", "hostFTPLabOut");
            iniC.userFTPLabOut = iniF.getIni("ftp", "userFTPLabOut");
            iniC.passFTPLabOut = iniF.getIni("ftp", "passFTPLabOut");
            iniC.portFTPLabOut = iniF.getIni("ftp", "portFTPLabOut");
            iniC.folderFTPLabOut = iniF.getIni("ftp", "folderFTPLabOut");
            iniC.usePassiveFTPLabOut = iniF.getIni("ftp", "usePassiveFTPLabOut");

            iniC.hostFTPLabOutMedica = iniF.getIni("ftp", "hostFTPLabOut_MADICA");
            iniC.userFTPLabOutMedica = iniF.getIni("ftp", "userFTPLabOut_MADICA");
            iniC.passFTPLabOutMedica = iniF.getIni("ftp", "passFTPLabOut_MADICA");
            iniC.portFTPLabOutMedica = iniF.getIni("ftp", "portFTPLabOut_MADICA");
            iniC.folderFTPLabOutMedica = iniF.getIni("ftp", "folderFTPLabOut_MADICA");
            iniC.usePassiveFTPLabOutMedica = iniF.getIni("ftp", "usePassiveFTPLabOut_MADICA");

            iniC.grdViewFontSize = iniF.getIni("app", "grdViewFontSize");
            iniC.grdViewFontName = iniF.getIni("app", "grdViewFontName");
            iniC.pdfFontSize = iniF.getIni("app", "pdfFontSize");
            iniC.pdfFontName = iniF.getIni("app", "pdfFontName");
            iniC.pdfFontSizetitleFont = iniF.getIni("app", "pdfFontSizetitleFont");
            iniC.pdfFontSizetxtFont = iniF.getIni("app", "pdfFontSizetxtFont");
            iniC.pdfFontSizehdrFont = iniF.getIni("app", "pdfFontSizehdrFont");
            iniC.pdfFontSizetxtFontB = iniF.getIni("app", "pdfFontSizetxtFontB");

            iniC.txtFocus = iniF.getIni("app", "txtFocus");
            iniC.grfRowColor = iniF.getIni("app", "grfRowColor");
            iniC.statusAppDonor = iniF.getIni("app", "statusAppDonor");
            iniC.themeApplication = iniF.getIni("app", "themeApplication");
            iniC.themeDonor = iniF.getIni("app", "themeDonor");
            iniC.printerSticker = iniF.getIni("app", "printerSticker");
            iniC.timerImgScanNew = iniF.getIni("app", "timerImgScanNew");
            iniC.pathImageScan = iniF.getIni("app", "pathImageScan");
            iniC.imggridscanwidth = iniF.getIni("app", "imggridscanwidth");
            iniC.hostname = iniF.getIni("app", "hostname");
            iniC.printerA4 = iniF.getIni("app", "printerA4");
            iniC.programLoad = iniF.getIni("app", "programLoad");
            iniC.labOutOpenFileDialog = iniF.getIni("app", "labOutOpenFileDialog");
            iniC.windows = iniF.getIni("app", "windows");
            iniC.grfScanWidth = iniF.getIni("app", "grfScanWidth");
            iniC.imgScanWidth = iniF.getIni("app", "imgScanWidth");
            iniC.pathScanStaffNote = iniF.getIni("app", "pathScanStaffNote");
            iniC.pathScreenCaptureUpload = iniF.getIni("app", "pathScreenCaptureUpload");
            iniC.pathIniFile = iniF.getIni("app", "pathIniFile");
            iniC.statusShowPrintDialog = iniF.getIni("app", "statusShowPrintDialog");
            iniC.txtSearchHnLenghtStart = iniF.getIni("app", "txtSearchHnLenghtStart");
            iniC.pathLabOutReceiveInnoTech = iniF.getIni("app", "pathLabOutReceiveInnoTech");
            iniC.pathLabOutBackupInnoTech = iniF.getIni("app", "pathLabOutBackupInnoTech");
            iniC.timerCheckLabOut = iniF.getIni("app", "timerCheckLabOut");
            iniC.pathTempScanAdd = iniF.getIni("app", "pathTempScanAdd");
            iniC.themeApp = iniF.getIni("app", "themeApp");
            iniC.station = iniF.getIni("app", "station");
            iniC.pathLabOutReceiveRIA = iniF.getIni("app", "pathLabOutReceiveRIA");
            iniC.pathLabOutBackupRIA = iniF.getIni("app", "pathLabOutBackupRIA");
            iniC.pathLabOutBackupRIAZipExtract = iniF.getIni("app", "pathLabOutBackupRIAZipExtract");
            iniC.pathLabOutBackupManual = iniF.getIni("app", "pathLabOutBackupManual");
            iniC.pathDownloadFile = iniF.getIni("app", "pathDownloadFile");
            iniC.pathScreenCaptureSend = iniF.getIni("app", "pathScreenCaptureSend");
            iniC.pacsServerIP = iniF.getIni("app", "pacsServerIP");
            iniC.pacsServerPort = iniF.getIni("app", "pacsServerPort");
            iniC.tabLabOutImageHeight = iniF.getIni("app", "tabLabOutImageHeight");
            iniC.tabLabOutImageWidth = iniF.getIni("app", "tabLabOutImageWidth");
            iniC.statusShowLabOutFrmLabOutReceiveView = iniF.getIni("app", "statusShowLabOutFrmLabOutReceiveView");
            iniC.pathLabOutReceiveMedica = iniF.getIni("app", "pathLabOutReceiveMedica");
            iniC.pathLabOutBackupMedica = iniF.getIni("app", "pathLabOutBackupMedica");
            iniC.statusLabOutReceiveOnline = iniF.getIni("app", "statusLabOutReceiveOnline");
            iniC.laboutMedicahosp_code = iniF.getIni("app", "laboutMedicahosp_code");
            iniC.statusLabOutAutoPrint = iniF.getIni("app", "statusLabOutAutoPrint");
            iniC.printerLabOut = iniF.getIni("app", "printerLabOut");
            iniC.statusLabOutReceiveTabShow = iniF.getIni("app", "statusLabOutReceiveTabShow");
            iniC.branchId = iniF.getIni("app", "branchId");
            iniC.pathline_bot_labout_urgent_bangna = iniF.getIni("app", "pathline_bot_labout_urgent_bangna");
            iniC.grfImgWidth = iniF.getIni("app", "grfImgWidth");
            iniC.scVssizeradio = iniF.getIni("app", "scVssizeradio");
            iniC.laboutdateMedica = iniF.getIni("app", "laboutdateMedica");
            iniC.medicalrecordexportpath = iniF.getIni("app", "medicalrecordexportpath");
            iniC.themegrfOpd = iniF.getIni("app", "themegrfOpd");
            iniC.themegrfIpd = iniF.getIni("app", "themegrfIpd");
            iniC.statusoutlabMedica = iniF.getIni("app", "statusoutlabMedica");
            iniC.ssoid = iniF.getIni("app", "ssoid");
            
            iniC.hostnamee = iniF.getIni("app", "hostnamee");
            iniC.hostaddresst = iniF.getIni("app", "hostaddresst");
            iniC.hostaddresse = iniF.getIni("app", "hostaddresse");

            iniC.imageCC_width = iniF.getIni("app", "imageCC_width");
            iniC.imageME_width = iniF.getIni("app", "imageME_width");
            iniC.imageDiag_width = iniF.getIni("app", "imageDiag_width");
            iniC.imageCC_Height = iniF.getIni("app", "imageCC_Height");
            iniC.imageME_Height = iniF.getIni("app", "imageME_Height");
            iniC.imageDiag_Height = iniF.getIni("app", "imageDiag_Height");

            iniC.email_form = iniF.getIni("email", "email_form");
            iniC.email_auth_user = iniF.getIni("email", "email_auth_user");
            iniC.email_auth_pass = iniF.getIni("email", "email_auth_pass");
            iniC.email_port = iniF.getIni("email", "email_port");
            iniC.email_ssl = iniF.getIni("email", "email_ssl");

            iniC.OPD_BTEMP = iniF.getIni("OPBKKClaim", "OPD_BTEMP");
            iniC.OPD_SBP = iniF.getIni("OPBKKClaim", "OPD_SBP");//      ความดันโลหิตค่าตัวบน
            iniC.OPD_DBP = iniF.getIni("OPBKKClaim", "OPD_DBP");//      ความดันโลหิตค่าตัวล่าง
            iniC.OPD_PR = iniF.getIni("OPBKKClaim", "OPD_PR");//      อัตราการเต้นหัวใจ
            iniC.OPD_RR = iniF.getIni("OPBKKClaim", "OPD_RR");//      อัตราการหายใจ

            iniC.OPD_BTEMP = iniC.OPD_BTEMP == null ? "mnc_temp" : iniC.OPD_BTEMP.Equals("") ? "mnc_temp" : iniC.OPD_BTEMP;
            iniC.OPD_SBP = iniC.OPD_SBP == null ? "mnc_temp" : iniC.OPD_SBP.Equals("") ? "mnc_temp" : iniC.OPD_SBP;
            iniC.OPD_DBP = iniC.OPD_DBP == null ? "mnc_temp" : iniC.OPD_DBP.Equals("") ? "mnc_temp" : iniC.OPD_DBP;
            iniC.OPD_PR = iniC.OPD_PR == null ? "mnc_temp" : iniC.OPD_PR.Equals("") ? "mnc_temp" : iniC.OPD_PR;
            iniC.OPD_RR = iniC.OPD_RR == null ? "mnc_temp" : iniC.OPD_RR.Equals("") ? "mnc_temp" : iniC.OPD_RR;

            iniC.themeApplication = iniC.themeApplication == null ? "Office2007Blue" : iniC.themeApplication.Equals("") ? "Office2007Blue" : iniC.themeApplication;
            iniC.timerImgScanNew = iniC.timerImgScanNew == null ? "2" : iniC.timerImgScanNew.Equals("") ? "0" : iniC.timerImgScanNew;
            iniC.pathImageScan = iniC.pathImageScan == null ? "d:\\images" : iniC.pathImageScan.Equals("") ? "d:\\images" : iniC.pathImageScan;
            iniC.imggridscanwidth = iniC.imggridscanwidth == null ? "380" : iniC.imggridscanwidth.Equals("") ? "380" : iniC.imggridscanwidth;
            iniC.themeApp = iniC.themeApp == null ? "Office2007Blue" : iniC.themeApp.Equals("") ? "Office2007Blue" : iniC.themeApp;
            iniC.pathScreenCaptureSend = iniC.pathScreenCaptureSend == null ? "C:\\capture" : iniC.pathScreenCaptureSend.Equals("") ? "C:\\capture" : iniC.pathScreenCaptureSend;

            //iniC.pathImgScanNew = iniC.pathImgScanNew == null ? "d:\\images" : iniC.pathImgScanNew.Equals("") ? "d:\\images" : iniC.pathImgScanNew;
            iniC.folderFTP = iniC.folderFTP == null ? "images_medical_record" : iniC.folderFTP.Equals("") ? "images_medical_record" : iniC.folderFTP;
            iniC.grdViewFontName = iniC.grdViewFontName.Equals("") ? "Microsoft Sans Serif" : iniC.grdViewFontName;
            iniC.pdfFontName = iniC.pdfFontName.Equals("") ? iniC.grdViewFontName : iniC.pdfFontName;
            iniC.pdfFontSize = iniC.pdfFontSize.Equals("") ? iniC.grdViewFontSize : iniC.pdfFontSize;
            iniC.pdfFontSizetitleFont = iniC.pdfFontSizetitleFont.Equals("") ? iniC.pdfFontSize : iniC.pdfFontSizetitleFont;
            iniC.pdfFontSizetxtFont = iniC.pdfFontSizetxtFont.Equals("") ? iniC.pdfFontSize : iniC.pdfFontSizetxtFont;
            iniC.pdfFontSizehdrFont = iniC.pdfFontSizehdrFont.Equals("") ? iniC.pdfFontSize : iniC.pdfFontSizehdrFont;
            iniC.pdfFontSizetxtFontB = iniC.pdfFontSizetxtFontB.Equals("") ? iniC.pdfFontSize : iniC.pdfFontSizetxtFontB;

            iniC.hostname = iniC.hostname == null ? "โรงพยาบาล" : iniC.hostname.Equals("") ? "โรงพยาบาล" : iniC.hostname;
            iniC.usePassiveFTP = iniC.usePassiveFTP == null ? "false" : iniC.usePassiveFTP.Equals("") ? "false" : iniC.usePassiveFTP;
            iniC.usePassiveFTPLabOut = iniC.usePassiveFTPLabOut == null ? "false" : iniC.usePassiveFTPLabOut.Equals("") ? "false" : iniC.usePassiveFTPLabOut;
            iniC.labOutOpenFileDialog = iniC.labOutOpenFileDialog == null ? "" : iniC.labOutOpenFileDialog.Equals("") ? "" : iniC.labOutOpenFileDialog;
            iniC.windows = iniC.windows == null ? "" : iniC.windows.Equals("") ? "" : iniC.windows;
            iniC.imgScanWidth = iniC.imgScanWidth == null ? "380" : iniC.imgScanWidth.Equals("") ? "380" : iniC.imgScanWidth;
            iniC.pathScanStaffNote = iniC.pathScanStaffNote == null ? "172.25.10.5" : iniC.pathScanStaffNote.Equals("") ? "172.25.10.5" : iniC.pathScanStaffNote;
            iniC.ProxyProxyType = iniC.ProxyProxyType == null ? "0" : iniC.ProxyProxyType.Equals("") ? "0" : iniC.ProxyProxyType;
            iniC.ProxyPort = iniC.ProxyPort == null ? "0" : iniC.ProxyPort.Equals("") ? "0" : iniC.ProxyPort;
            iniC.pathIniFile = iniC.pathIniFile == null ? "" : iniC.pathIniFile.Equals("") ? "" : iniC.pathIniFile;
            iniC.statusShowPrintDialog = iniC.statusShowPrintDialog == null ? "0" : iniC.statusShowPrintDialog.Equals("") ? "0" : iniC.statusShowPrintDialog;
            iniC.timerCheckLabOut = iniC.timerCheckLabOut == null ? "0" : iniC.timerCheckLabOut.Equals("") ? "0" : iniC.timerCheckLabOut;
            iniC.station = iniC.station == null ? "0" : iniC.station.Equals("") ? "0" : iniC.station;
            iniC.pathDownloadFile = iniC.pathDownloadFile == null ? "" : iniC.pathDownloadFile.Equals("") ? "" : iniC.pathDownloadFile;
            iniC.pacsServerIP = iniC.pacsServerIP == null ? "" : iniC.pacsServerIP.Equals("") ? "" : iniC.pacsServerIP;
            iniC.pacsServerPort = iniC.pacsServerPort == null ? "" : iniC.pacsServerPort.Equals("") ? "" : iniC.pacsServerPort;
            iniC.tabLabOutImageHeight = iniC.tabLabOutImageHeight == null ? "842" : iniC.tabLabOutImageHeight.Equals("") ? "842" : iniC.tabLabOutImageHeight;
            iniC.tabLabOutImageWidth = iniC.tabLabOutImageWidth == null ? "595" : iniC.tabLabOutImageWidth.Equals("") ? "595" : iniC.tabLabOutImageWidth;
            iniC.statusShowLabOutFrmLabOutReceiveView = iniC.statusShowLabOutFrmLabOutReceiveView == null ? "windows10" : iniC.statusShowLabOutFrmLabOutReceiveView.Equals("") ? "windows10" : iniC.statusShowLabOutFrmLabOutReceiveView;
            iniC.pathLabOutReceiveMedica = iniC.pathLabOutReceiveMedica == null ? "c:\\medica\\result" : iniC.pathLabOutReceiveMedica.Equals("") ? "c:\\medica\\result" : iniC.pathLabOutReceiveMedica;
            iniC.pathLabOutBackupMedica = iniC.pathLabOutBackupMedica == null ? "c:\\medica\\backup" : iniC.pathLabOutBackupMedica.Equals("") ? "c:\\medica\\backup" : iniC.pathLabOutBackupMedica;
            iniC.statusLabOutReceiveOnline = iniC.statusLabOutReceiveOnline == null ? "0" : iniC.statusLabOutReceiveOnline.Equals("") ? "0" : iniC.statusLabOutReceiveOnline;
            iniC.statusLabOutAutoPrint = iniC.statusLabOutAutoPrint == null ? "0" : iniC.statusLabOutAutoPrint.Equals("") ? "0" : iniC.statusLabOutAutoPrint;
            iniC.printerLabOut = iniC.printerLabOut == null ? "" : iniC.printerLabOut.Equals("") ? "" : iniC.printerLabOut;
            iniC.statusLabOutReceiveTabShow = iniC.statusLabOutReceiveTabShow == null ? "1" : iniC.statusLabOutReceiveTabShow.Equals("") ? "1" : iniC.statusLabOutReceiveTabShow;
            iniC.branchId = iniC.branchId == null ? "005" : iniC.branchId.Equals("") ? "005" : iniC.branchId;
            iniC.pathline_bot_labout_urgent_bangna = iniC.pathline_bot_labout_urgent_bangna == null ? "c:\\python\\line_bot_labout_urgent_bangna.py" : iniC.pathline_bot_labout_urgent_bangna.Equals("") ? "c:\\python\\line_bot_labout_urgent_bangna.py" : iniC.pathline_bot_labout_urgent_bangna;
            iniC.grfImgWidth = iniC.grfImgWidth == null ? "500" : iniC.grfImgWidth.Equals("") ? "500" : iniC.grfImgWidth;
            iniC.scVssizeradio = iniC.scVssizeradio == null ? "20" : iniC.scVssizeradio.Equals("") ? "20" : iniC.scVssizeradio;
            iniC.laboutdateMedica = iniC.laboutdateMedica == null ? "" : iniC.laboutdateMedica.Equals("") ? "" : iniC.laboutdateMedica;
            iniC.medicalrecordexportpath = iniC.medicalrecordexportpath == null ? "c:\\exportpath" : iniC.medicalrecordexportpath.Equals("") ? "" : iniC.medicalrecordexportpath;
            iniC.themegrfOpd = iniC.themegrfOpd == null ? "Office2016Colorful" : iniC.themegrfOpd.Equals("") ? "Office2016Colorful" : iniC.themegrfOpd;
            iniC.themegrfIpd = iniC.themegrfIpd == null ? "Office2007Black" : iniC.themegrfIpd.Equals("") ? "Office2007Black" : iniC.themegrfIpd;
            iniC.statusoutlabMedica = iniC.statusoutlabMedica == null ? "1" : iniC.statusoutlabMedica.Equals("") ? "1" : iniC.statusoutlabMedica;
            iniC.pdfFontName = iniC.pdfFontName == null ? iniC.grdViewFontName : iniC.pdfFontName.Equals("") ? iniC.grdViewFontName : iniC.pdfFontName;
            iniC.hostnamee = iniC.hostnamee == null ? "" : iniC.hostnamee.Equals("") ? "" : iniC.hostnamee;
            iniC.email_auth_pass = iniC.email_auth_pass == null ? "" : iniC.email_auth_pass.Equals("") ? "" : iniC.email_auth_pass;
            iniC.email_auth_user = iniC.email_auth_user == null ? "" : iniC.email_auth_user.Equals("") ? "" : iniC.email_auth_user;
            iniC.email_form = iniC.email_form == null ? "" : iniC.email_form.Equals("") ? "" : iniC.email_form;
            iniC.email_port = iniC.email_port == null ? "" : iniC.email_port.Equals("") ? "" : iniC.email_port;
            iniC.email_ssl = iniC.email_ssl == null ? "" : iniC.email_ssl.Equals("") ? "" : iniC.email_ssl;
            iniC.imageCC_width = iniC.imageCC_width == null ? "800" : iniC.imageCC_width.Equals("") ? "800" : iniC.imageCC_width;
            iniC.imageME_width = iniC.imageME_width == null ? "800" : iniC.imageME_width.Equals("") ? "800" : iniC.imageME_width;
            iniC.imageDiag_width = iniC.imageDiag_width == null ? "800" : iniC.imageDiag_width.Equals("") ? "800" : iniC.imageDiag_width;
            iniC.imageCC_Height = iniC.imageCC_Height == null ? "400" : iniC.imageCC_Height.Equals("") ? "400" : iniC.imageCC_Height;
            iniC.imageME_Height = iniC.imageME_Height == null ? "400" : iniC.imageME_Height.Equals("") ? "400" : iniC.imageME_Height;
            iniC.imageDiag_Height = iniC.imageDiag_Height == null ? "400" : iniC.imageDiag_Height.Equals("") ? "400" : iniC.imageDiag_Height;

            int.TryParse(iniC.grdViewFontSize, out grdViewFontSize);
            int.TryParse(iniC.pdfFontSize, out pdfFontSize);
            int.TryParse(iniC.pdfFontSizehdrFont, out pdfFontSizehdrFont);
            int.TryParse(iniC.pdfFontSizetitleFont, out pdfFontSizetitleFont);
            int.TryParse(iniC.pdfFontSizetxtFont, out pdfFontSizetxtFont);
            int.TryParse(iniC.pdfFontSizetxtFontB, out pdfFontSizetxtFontB);

            int.TryParse(iniC.imggridscanwidth, out imggridscanwidth);
            Boolean.TryParse(iniC.usePassiveFTP, out ftpUsePassive);
            Boolean.TryParse(iniC.usePassiveFTPLabOut, out ftpUsePassiveLabOut);
            int.TryParse(iniC.grfScanWidth, out grfScanWidth);
            int.TryParse(iniC.timerCheckLabOut, out timerCheckLabOut);

            int.TryParse(iniC.imggridscanwidth, out imggridscanwidth);
            int.TryParse(iniC.imgScanWidth, out imgScanWidth);
            int.TryParse(iniC.txtSearchHnLenghtStart, out txtSearchHnLenghtStart);
            int.TryParse(iniC.tabLabOutImageHeight, out tabLabOutImageHeight);
            int.TryParse(iniC.tabLabOutImageWidth, out tabLabOutImageWidth);
            int.TryParse(iniC.grfImgWidth, out grfImgWidth);
            int.TryParse(iniC.scVssizeradio, out scVssizeradio);
            int.TryParse(iniC.imageCC_width, out imageCC_width);
            int.TryParse(iniC.imageME_width, out imageME_width);
            int.TryParse(iniC.imageDiag_width, out imageDiag_width);
            int.TryParse(iniC.imageCC_Height, out imageCC_Height);
            int.TryParse(iniC.imageME_Height, out imageME_Height);
            int.TryParse(iniC.imageDiag_Height, out imageDiag_Height);
        }
        public void setC1Combo(C1ComboBox c, String data)
        {
            if (c.Items.Count == 0) return;
            c.SelectedIndex = c.SelectedItem == null ? 0 : c.SelectedIndex;
            c.SelectedIndex = 0;
            foreach (ComboBoxItem item in c.Items)
            {
                if (item.Value.Equals(data))
                {
                    c.SelectedItem = item;
                    break;
                }
            }
        }
        public Image RotateImage(Image img)
        {
            var bmp = new Bitmap(img);

            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Color.White);
                gfx.DrawImage(img, 0, 0, img.Width, img.Height);
            }

            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            //bmp.Dispose();
            return bmp;
        }
        public Stream ToStream(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
        public String datetoShow(Object dt)
        {
            DateTime dt1 = new DateTime();
            //MySqlDateTime dtm = new MySqlDateTime();
            String re = "";
            if (iniC.windows.Equals("windowsxp"))
            {
                if (DateTime.TryParse(dt.ToString(), out dt1))
                {
                    if (dt1.Year > 2500)
                    {
                        re = dt1.ToString("dd-MM") + "-" + (dt1.Year - 543).ToString();
                    }
                    else if (dt1.Year < 1500)
                    {
                        re = dt1.ToString("dd-MM")+ "-"+ (dt1.Year + 543).ToString();
                    }
                    else
                    {
                        re = dt1.ToString("dd-MM") + "-" + dt1.Year.ToString();
                    }
                }
                //re = dt.ToString();
            }
            else
            {
                if (dt != null)
                {
                    if (DateTime.TryParse(dt.ToString(), out dt1))
                    {
                        re = dt1.ToString("dd-MM-yyyy");
                    }
                }
            }
            return re;
        }
        public String datetoShow1(String dt)
        {
            DateTime dt1 = new DateTime();
            //MySqlDateTime dtm = new MySqlDateTime();
            String re = "", year1 = "", mm = "", dd = "";
            if (iniC.windows.Equals("windowsxp"))
            {
                if (dt.Length >= 10)
                {
                    year1 = dt.Substring(0, 4);
                    mm = dt.Substring(5, 2);
                    dd = dt.Substring(8, 2);
                    re = dd + "-" + mm + "-" + year1;
                }
                else
                {
                    re = "";
                }
                //re = dt.ToString();
            }
            else
            {
                if (dt != null)
                {
                    if (DateTime.TryParse(dt.ToString(), out dt1))
                    {
                        re = dt1.ToString("dd-MM-yyyy");
                    }
                }
            }
            return re;
        }
        public String datetoShowShort(String dt)
        {
            DateTime dt1 = new DateTime();
            //MySqlDateTime dtm = new MySqlDateTime();
            String re = "", year1 = "", mm = "", dd = "";
            if (iniC.windows.Equals("windowsxp"))
            {
                if (dt.Length >= 10)
                {
                    year1 = dt.Substring(0, 4);
                    mm = dt.Substring(5, 2);
                    dd = dt.Substring(8, 2);
                    re = dd + "-" + mm + "-" + year1;
                }
                else
                {
                    re = "";
                }
                //re = dt.ToString();
            }
            else
            {
                if (dt != null)
                {
                    if (DateTime.TryParse(dt.ToString(), out dt1))
                    {
                        re = dt1.ToString("dd-MM-yy");
                    }
                }
            }
            return re;
        }
        public String datetoDB(String dt)
        {
            DateTime dt1 = new DateTime();
            String re = "", year1="",mm="",dd="";
            int year = 0, mon=0, day=0;
            //new LogWriter("d", "datetoDB 01" );
            if (iniC.windows.Equals("windowsxp"))
            {
                //new LogWriter("d", "datetoDB 02 iniC.windowsxp ");
                //if (DateTime.TryParse(dt, out dt1))
                //{
                //    //new LogWriter("d", "datetoDB 02 iniC.windowsxp DateTime.TryParse(dt, out dt1) true dt1.Year"+ dt1.Year);
                //    if (dt1.Year > 2500)
                //    {
                //        re = (dt1.Year - 543).ToString() + "-" + dt1.ToString("MM-dd");
                //    }
                //    else if (dt1.Year < 1500)
                //    {
                //        re = (dt1.Year + 543).ToString() + "-" + dt1.ToString("MM-dd");
                //    }
                //    else
                //    {
                //        re = dt1.Year.ToString() + "-" + dt1.ToString("MM-dd");
                //    }                    
                //}
                //else
                //{
                    //new LogWriter("d", "datetoDB 02 iniC.windowsxp DateTime.TryParse(dt, out dt1) false ");
                    if (dt.Length >= 10)
                    {
                        year1 = dt.Substring(6, 4);
                        mm = dt.Substring(3, 2);
                        dd = dt.Substring(0, 2);
                        int.TryParse(mm, out mon);
                        if (mon > 12)
                        {
                            mm = dt.Substring(0, 2);
                            dd = dt.Substring(3, 2);
                            int.TryParse(mm, out mon);
                        }
                        //new LogWriter("d", "datetoDB year1 " + year1);
                        if (int.TryParse(year1, out year))
                        {
                            //new LogWriter("d", "datetoDB year1 int.TryParse(year1, out year) " + year);
                            if (year <= 1500)
                            {
                                year = year + 543;
                            }
                            else if (year >= 2500)
                            {
                                year = year - 543;
                            }
                        }
                        else
                        {
                            //new LogWriter("d", "datetoDB year1 else int.TryParse(year1, out year) ");
                        }
                        re = year.ToString() + "-" + mm + "-" + dd;
                    }
                    else
                    {

                    }
                //}
                //re = dt1.ToString("yyyy-MM-dd");
            }
            else
            {
                //new LogWriter("d", "datetoDB 03 iniC.windows 10 ");
                if (dt != null)
                {
                    if (!dt.Equals(""))
                    {
                        // Thread แบบนี้ ทำให้ โปรแกรม ที่ไปลงที Xtrim ไม่เอา date ผิด
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us")
                        {
                            DateTimeFormat =
                            {
                                DateSeparator = "-"
                            }
                        };
                        if (DateTime.TryParse(dt, out dt1))
                        {
                            
                            if (dt1.Year > 2500)
                            {
                                re = (dt1.Year -543).ToString() + "-" + dt1.ToString("MM-dd");
                            }
                            else
                            {
                                re = dt1.Year.ToString() + "-" + dt1.ToString("MM-dd");
                            }
                        }
                        else
                        {
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH")
                            {
                                DateTimeFormat =
                            {
                                DateSeparator = "-"
                            }
                            };
                            if (DateTime.TryParse(dt, out dt1))
                            {
                                re = dt1.ToString("yyyy-MM-dd");
                            }
                        }
                        //dt1 = DateTime.Parse(dt.ToString());

                    }
                }
            }
            
            return re;
        }
        private void initOPBKKClinic()
        {
            opBKKClinic.Add("00", "หน่วยงานระดับสถานีอนามัย");
            opBKKClinic.Add("01", "อายุรกรรม");
            opBKKClinic.Add("02", "ศัลยกรรม");
            opBKKClinic.Add("03", "สูติกรรม");
            opBKKClinic.Add("04", "นรีเวชกรรม");
            opBKKClinic.Add("05", "กุมารเวชกรรม");
            opBKKClinic.Add("06", "โสต ศอ นาสิก");
            opBKKClinic.Add("07", "จักษุวิทยา");
            opBKKClinic.Add("08", "ศัลยกรรมออร์โธปิดิกส");
            opBKKClinic.Add("09", "จิตเวช");
            opBKKClinic.Add("10", "รังสีวิทยา");
            opBKKClinic.Add("11", "ทันตกรรม");
            opBKKClinic.Add("12", "เวชศาสตร์ฉุกเฉินและนิติเวช");
            opBKKClinic.Add("13", "เวชกรรมฟื้นฟู");
            opBKKClinic.Add("14", "แพทย์แผนไทย");
            opBKKClinic.Add("15", "PCU ใน รพ.");
            opBKKClinic.Add("16", "เวชกรรมปฎิบัติทั่วไป");
            opBKKClinic.Add("17", "เวชศาสสตร์ครอบครัวและชุมชน");
            opBKKClinic.Add("18", "อาชีวคลินิก");
            opBKKClinic.Add("19", "วิสัญญีวิทยา(คลินิกระงับปวด)");
            opBKKClinic.Add("20", "ศัลยกรรมประสาท");
            opBKKClinic.Add("21", "อาชีวเวชรกรรม");
            opBKKClinic.Add("22", "เวชกรรมสังคม");
            opBKKClinic.Add("23", "พยาธิวิทยากายวิภาค");
            opBKKClinic.Add("24", "พยาธิวิทยาคลินิค");
            opBKKClinic.Add("25", "แพทย์ทางเลือก");
            opBKKClinic.Add("99", "อื่นๆ");
            //opBKKClinic.Add("00", "111111");
        }
        private void initOPBKKCHRGITEM()
        {
            opBKKCHRGITEM_CODEA.Add("21", "ค่าอวัยวะเทียมและเครื่องช่วยผู้พิการ");
            opBKKCHRGITEM_CODEA.Add("31", "ค่ายาและสารอาหารทางเส้นเลือด");
            opBKKCHRGITEM_CODEA.Add("51", "เวชภัณฑ์ที่ไม่ใช่ยา");
            opBKKCHRGITEM_CODEA.Add("61", "บริการโลหิตและส่วนประกอบของโลหิต");
            opBKKCHRGITEM_CODEA.Add("71", "ตรวจวินิจฉัยทางเทคนิคการแพทย์และพยาธิวิทยา");
            opBKKCHRGITEM_CODEA.Add("81", "ตรวจวินิจฉัยและรักษาทางรังสีวิทยา");
            opBKKCHRGITEM_CODEA.Add("91", "ตรวจวินิจฉัยโดยวิธีพิเศษอื่น ๆ");
            opBKKCHRGITEM_CODEA.Add("A1", "อุปกรณ์ของใช้และเครื่องมือทางการแพทย");
            opBKKCHRGITEM_CODEA.Add("B1", "ทำหัตถการ และบริการวิสัญญ");
            opBKKCHRGITEM_CODEA.Add("C1", "ค่าบริการทางการพยาบาล");
            opBKKCHRGITEM_CODEA.Add("D1", "บริการทางทันตกรรม");
            opBKKCHRGITEM_CODEA.Add("E1", "บริการทางกายภาพบำบัด และเวชกรรมฟื้นฟู");
            opBKKCHRGITEM_CODEA.Add("J1", "ค่าบริการอื่นๆที่ไม่เกี่ยวกับการรักษาพยาบาลโดยตรง");
            opBKKCHRGITEM_CODEA.Add("H1", "ค่าธรรมเนียมบุคลากรทางการแพทย");
            //opBKKClinic.Add("14", "แพทย์แผนไทย");
            //opBKKClinic.Add("15", "PCU ใน รพ.");
            //opBKKClinic.Add("16", "เวชกรรมปฎิบัติทั่วไป");
            //opBKKClinic.Add("17", "เวชศาสสตร์ครอบครัวและชุมชน");
            //opBKKClinic.Add("18", "อาชีวคลินิก");
            //opBKKClinic.Add("19", "วิสัญญีวิทยา(คลินิกระงับปวด)");
            //opBKKClinic.Add("20", "ศัลยกรรมประสาท");
            //opBKKClinic.Add("21", "อาชีวเวชรกรรม");
            //opBKKClinic.Add("22", "เวชกรรมสังคม");
            //opBKKClinic.Add("23", "พยาธิวิทยากายวิภาค");
            //opBKKClinic.Add("24", "พยาธิวิทยาคลินิค");
            //opBKKClinic.Add("25", "แพทย์ทางเลือก");
            //opBKKClinic.Add("99", "อื่นๆ");
            //opBKKClinic.Add("00", "111111");
        }
        public void setCboOPBKKINSCL(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            
            item = new ComboBoxItem();
            item.Value = "UCS";
            item.Text = "สิทธิหลักประกันสุขภาพ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "WEL";
            item.Text = "สิทธิหลักประกันสุขภาพ (ยกเว้นการร่วมจ่าย)";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "OFC";
            item.Text = "สิทธิข้าราชการ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "SSS";
            item.Text = "สิทธิประกันสังคม";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "LGO";
            item.Text = "สิทธิ อปท";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "SSI";
            item.Text = "สิทธิประกันสังคมทุพพลภาพ";
            c.Items.Add(item);
        }
        public void setCboOPBKKCHRGITEM_CODEA(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();

            foreach (KeyValuePair<string, string> entry in opBKKCHRGITEM_CODEA)
            {
                item = new ComboBoxItem();
                item.Value = entry.Key;
                item.Text = entry.Value;
                c.Items.Add(item);
            }
        }
        public void setCboOPBKKClinic(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();

            foreach (KeyValuePair<string, string> entry in opBKKClinic)
            {
                item = new ComboBoxItem();
                item.Value = entry.Key;
                item.Text = entry.Value;
                c.Items.Add(item);
            }

            //item = new ComboBoxItem();
            //item.Value = "00";
            //item.Text = "หน่วยงานระดับสถานีอนามัย";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "01";
            //item.Text = "อายุรกรรม";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "02";
            //item.Text = "ศัลยกรรม";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "03";
            //item.Text = "สูติกรรม";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "04";
            //item.Text = "นรีเวชกรรม";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "05";
            //item.Text = "กุมารเวชกรรม";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "06";
            //item.Text = "โสต ศอ นาสิก";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "07";
            //item.Text = "จักษุวิทยา";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "08";
            //item.Text = "ศัลยกรรมออร์โธปิดิกส";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "09";
            //item.Text = "จิตเวช";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "10";
            //item.Text = "รังสีวิทยา";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "11";
            //item.Text = "ทันตกรรม";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "12";
            //item.Text = "เวชศาสตร์ฉุกเฉินและนิติเวช";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "13";
            //item.Text = "เวชกรรมฟื้นฟู";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "14";
            //item.Text = "แพทย์แผนไทย";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "15";
            //item.Text = "PCU ใน รพ.";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "16";
            //item.Text = "เวชกรรมปฎิบัติทั่วไป";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "17";
            //item.Text = "เวชศาสสตร์ครอบครัวและชุมชน";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "18";
            //item.Text = "อาชีวคลินิก";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "19";
            //item.Text = "วิสัญญีวิทยา(คลินิกระงับปวด)";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "20";
            //item.Text = "ศัลยกรรมประสาท";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "21";
            //item.Text = "อาชีวเวชรกรรม";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "22";
            //item.Text = "เวชกรรมสังคม";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "23";
            //item.Text = "พยาธิวิทยากายวิภาค";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "24";
            //item.Text = "พยาธิวิทยาคลินิค";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "25";
            //item.Text = "แพทย์ทางเลือก";
            //c.Items.Add(item);

            //item = new ComboBoxItem();
            //item.Value = "99";
            //item.Text = "อื่นๆ";
            //c.Items.Add(item);
        }
        public String getMonth(String monthId)
        {
            if (monthId == "01")
            {
                return "มกราคม";
            }
            else if (monthId == "02")
            {
                return "กุมภาพันธ์";
            }
            else if (monthId == "03")
            {
                return "มีนาคม";
            }
            else if (monthId == "04")
            {
                return "เมษายน";
            }
            else if (monthId == "05")
            {
                return "พฤษภาคม";
            }
            else if (monthId == "06")
            {
                return "มิถุนายน";
            }
            else if (monthId == "07")
            {
                return "กรกฎาคม";
            }
            else if (monthId == "08")
            {
                return "สิงหาคม";
            }
            else if (monthId == "09")
            {
                return "กันยายน";
            }
            else if (monthId == "10")
            {
                return "ตุลาคม";
            }
            else if (monthId == "11")
            {
                return "พฤศจิกายน";
            }
            else if (monthId == "12")
            {
                return "ธันวาคม";
            }
            else
            {
                return "";
            }
        }
        public ComboBox setCboMonth(ComboBox c)
        {
            c.Items.Clear();
            var items = new[]{
                new{Text = "มกราคม", Value="01"},
                new{Text = "กุมภาพันธ์", Value="02"},
                new{Text = "มีนาคม", Value="03"},
                new{Text = "เมษายน", Value="04"},
                new{Text = "พฤษภาคม", Value="05"},
                new{Text = "มิถุนายน", Value="06"},
                new{Text = "กรกฎาคม", Value="07"},
                new{Text = "สิงหาคม", Value="08"},
                new{Text = "กันยายน", Value="09"},
                new{Text = "ตุลาคม", Value="10"},
                new{Text = "พฤศจิกายน", Value="11"},
                new{Text = "ธันวาคม", Value="12"}
            };
            c.DataSource = items;
            c.DisplayMember = "Text";

            c.ValueMember = "Value";
            c.SelectedIndex = c.FindStringExact(getMonth(System.DateTime.Now.Month.ToString("00")));
            return c;
        }
        public void setCboMOdality(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectAll();
            int i = 0;
            
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "CR";
            item.Text = "Computed Radiography";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "CT";
            item.Text = "Computed Tomography";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "MR";
            item.Text = "Magnetic Resonance";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "NR";
            item.Text = "Nuclear Medicine";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "UA";
            item.Text = "Ultrasound";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "OT";
            item.Text = "Other";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "BI";
            item.Text = "Biomagnetic Imaging";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "CD";
            item.Text = "Color Flow Doppler";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "DD";
            item.Text = "Duplex Doppler";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "DG";
            item.Text = "Diaphanography";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "ES";
            item.Text = "Endoscopy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "LS";
            item.Text = "Laser Surface Scan";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "PT";
            item.Text = "Positron Emission Tomography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RG";
            item.Text = "Radiographic Imaging";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "ST";
            item.Text = "Single-photon Emission Computed Tomography (SPECT)";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "TG";
            item.Text = "Thermography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "XA";
            item.Text = "X-Ray Angiography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RF";
            item.Text = "Radio Fluoroscopy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTIMAGE";
            item.Text = "Radiotherapy Image";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTDOSE";
            item.Text = "Radiotherapy Dose";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTSTRUCT";
            item.Text = "RadioTherapy Structure Set";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTPLAN";
            item.Text = "Radiotherapy Plan";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTRECORD";
            item.Text = "Radiotherapy Treatment Record";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "HC";
            item.Text = "Hard Copy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "DX";
            item.Text = "Digital Radiography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "MG";
            item.Text = "Mammography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "IO";
            item.Text = "Intra-oral Radiography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "PX";
            item.Text = "Panoramic X-Ray";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "GM";
            item.Text = "General Microscopy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SM";
            item.Text = "Slide Microscopy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "XC";
            item.Text = "External-camera Photography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "PR";
            item.Text = "Presentation State";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "AU";
            item.Text = "Audio";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "ECG";
            item.Text = "Electrocardiography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "EPS";
            item.Text = "Cardiac Electrophysiology";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "HD";
            item.Text = "Hemodynamic Waveform";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SR";
            item.Text = "Structured Report Document";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "IVUS";
            item.Text = "Intravascular Ultrasound";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "OP";
            item.Text = "Ophthalmic Photography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SMR";
            item.Text = "Stereometric Relationship";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SC";
            item.Text = "Secondary Capture";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SD";
            item.Text = "Scanned Document";
            c.Items.Add(item);
        }
        public ComboBox setCboYear(ComboBox c)
        {
            c.Items.Clear();
            c.Items.Add(System.DateTime.Now.Year + 543);
            c.Items.Add(System.DateTime.Now.Year + 543 - 1);
            c.Items.Add(System.DateTime.Now.Year + 543 - 2);
            c.SelectedIndex = c.FindStringExact(String.Concat(System.DateTime.Now.Year + 543));
            return c;
        }
        public ComboBox setCboPeriod(ComboBox c)
        {
            c.Items.Clear();
            c.Items.Add(1);
            c.Items.Add(2);

            c.SelectedIndex = 0;
            return c;
        }
        public String shortPaidName(String name)
        {
            if (name == "ประกันสังคม (บ.1)")
            {
                return "ปกส(บ.1)";
            }
            else if (name == "ประกันสังคม (บ.2)")
            {
                return "ปกส(บ.2)";
            }
            else if (name == "ประกันสังคม (บ.5)")
            {
                return "ปกส(บ.5)";
            }
            else if (name == "ประกันสังคมอิสระ (บ.1)")
            {
                return "ปกต(บ.1)";
            }
            else if (name == "ประกันสังคมอิสระ (บ.5)")
            {
                return "ปกต(บ.5)";
            }
            else if (name == "ตรวจสุขภาพ (เงินสด)")
            {
                return "ตส(เงินสด)";
            }
            else if (name == "ตรวจสุขภาพ (บริษัท)")
            {
                return "ตส(บริษัท)";
            }
            else if (name == "ตรวจสุขภาพ (PACKAGE)")
            {
                return "ตส(PACKAGE)";
            }
            else if (name == "ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ")
            {
                return "ลูกหนี้(ปากน้ำ)";
            }
            else if (name == "ลูกหนี้บางนา 1")
            {
                return "ลูกหนี้(บ.1)";
            }
            else if (name == "บริษัทประกัน")
            {
                return "บ.ประกัน";
            }
            else if (name == "ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปู่เจ้า")
            {
                return "ลูกหนี้(ปู่เจ้า)";
            }
            else
            {
                return name;
            }
        }
        public String dateDBtoShowShort(String dt)
        {
            if (dt != "")
            {
                if (Int16.Parse(dt.Substring(0, 4)) < 1957)
                {
                    return dt.Substring(8, 2) + "-" + dt.Substring(5, 2) + "-" + String.Concat(Int16.Parse(dt.Substring(0, 4)) + 543);
                }
                else
                {
                    return dt.Substring(8, 2) + "-" + dt.Substring(5, 2) + "-" + String.Concat(Int16.Parse(dt.Substring(0, 4)) + 543).Substring(2);
                }

            }
            else
            {
                return dt;
            }
        }
        public String FormatTime(String t)
        {
            String aa = "";
            aa = "0000" + t;
            if (aa.Length >= 4)
            {
                aa = aa.Substring(aa.Length - 4, 2) + ":" + aa.Substring(aa.Length - 2);
            }
            return aa;
        }
        public String dateDBtoShowShort1(String dt)
        {
            if (dt != "")
            {
                if (Int16.Parse(dt.Substring(0, 4)) < 1957)
                {
                    return dt.Substring(8, 2) + "-" + dt.Substring(5, 2) + "-" + String.Concat(Int16.Parse(dt.Substring(0, 4)) + 543);
                }
                else
                {
                    return dt.Substring(8, 2) + "-" + dt.Substring(5, 2) + "-" + String.Concat(Int16.Parse(dt.Substring(0, 4)) + 543).Substring(2);
                }

            }
            else
            {
                return dt;
            }
        }
        public Size MeasureString(Control c)
        {
            return TextRenderer.MeasureText(c.Text, c.Font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.SingleLine | TextFormatFlags.NoClipping | TextFormatFlags.PreserveGraphicsClipping);
        }
        public Size MeasureString(String txt, Font font)
        {
            return TextRenderer.MeasureText(txt, font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.SingleLine | TextFormatFlags.NoClipping | TextFormatFlags.PreserveGraphicsClipping);
        }
        public Bitmap ResizeImage(Image image, int width, int height)
        {            
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public Bitmap ResizeImagetoA4(Image image)
        {
            /*
             * 
             */
            float tgtWidthMM = 210;  //A4 paper size
            float tgtHeightMM = 297;
            float tgtWidthInches = tgtWidthMM / 25.4f;
            float tgtHeightInches = tgtHeightMM / 25.4f;
            float srcWidthPx = image.Width;
            float srcHeightPx = image.Height;
            float dpiX = srcWidthPx / tgtWidthInches;
            float dpiY = srcHeightPx / tgtHeightInches;

            var destRect = new Rectangle(0, 0, 210, 297);
            var destImage = new Bitmap(210, 297);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public Bitmap ResizeImagetoA4Lan(Image image)
        {
            /*
             * 
             */
            int ww = 697, hh= 610;

            Image aaa = RotateImage(image);
            float tgtWidthMM = 297;  //A4 paper size
            float tgtHeightMM = 210;
            float tgtWidthInches = tgtWidthMM / 25.4f;
            float tgtHeightInches = tgtHeightMM / 25.4f;
            //float srcWidthPx = aaa.Width;
            //float srcHeightPx = aaa.Height;
            float srcWidthPx = aaa.Height;
            float srcHeightPx = aaa.Width;
            float dpiX = srcWidthPx / tgtWidthInches;
            float dpiY = srcHeightPx / tgtHeightInches;

            var destRect = new Rectangle(0, 0, ww, hh-100);
            var destImage = new Bitmap(ww , hh- 100);
            destImage.SetResolution(aaa.HorizontalResolution, aaa.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(aaa, destRect, 0, 0, aaa.Width, aaa.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public bool ExploreFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }
            //Clean up file path so it can be navigated OK
            filePath = System.IO.Path.GetFullPath(filePath);
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            return true;
        }
        public void serverPACsInfinittStart(TcpListener tcpServerListener, String ipaddress, String port)
        {
            try
            {
                IPAddress ipad = IPAddress.Parse(ipaddress);
                tcpServerListener = new TcpListener(ipad, int.Parse(port));

                tcpServerListener.Start();

                Socket socket = tcpServerListener.AcceptSocket();
                byte[] b = new byte[100];
                int k = socket.Receive(b);

            }
            catch(Exception ex)
            {

            }
        }
        public PACsMSH genPAsMSH(String reqdept, String MessageType)
        {
            PACsMSH msh = new PACsMSH();
            msh.FieldSeparator = "";
            msh.EncodingCharacters = "^~\\&";
            msh.SendingApplication = "HIS";
            msh.SendingFacility = reqdept;
            msh.ReceivingApplication = "ThaiGL";
            msh.ReceivingFacility = "";
            msh.DateTimeOfMessage = DateTime.Now.Year+DateTime.Now.ToString("MMddHHmmss");
            msh.Security = "";
            //msh.MessageType = "ADT^A08";
            msh.MessageType = MessageType;
            msh.MessageControlID = "331950";
            msh.ProcessingID = "P";
            msh.VersionID = "2.3";
            msh.SequenceNumber = "";
            msh.ContinuationPointer = "";
            msh.AcceptAcknowledgementType = "";
            msh.ApplicationAcknowledgementType = "";
            msh.CountryCode = "";
            msh.CharacterSet = "";
            msh.PrincipalLanguageOfMessage = "";
            msh.AlternateCharacterSetHandlingScheme = "UNICODE UTF-8";
            
            return msh;
        }
        public PACsEVN genPACsEVN()
        {
            PACsEVN evn = new PACsEVN();
            evn.EventTypeCode = "";
            evn.RecordedDateTime = DateTime.Now.Year + DateTime.Now.ToString("MMddHHmmss");
            evn.DateTimePlannedEvent = "";
            evn.EventReasonCode = "";
            evn.OperatorID = "";
            evn.EventOccurred = DateTime.Now.Year + DateTime.Now.ToString("MMddHHmmss");

            return evn;
        }
        public PACsPID genPACsPID(String hn, String pttprefix, String pttfirstname, String pttlastame, String dob, String sex, String nation)
        {
            PACsPID pid = new PACsPID();
            pid = new PACsPID();
            pid.SetID = "";
            pid.PatientID = "";
            pid.PatientIdentifierList = hn;
            pid.AlternatePatientID = "";
            pid.PatientName = pttprefix+"^"+ pttfirstname+"^^^"+ pttlastame;
            pid.MothersMaidenName = "";
            pid.DateTimeOfBirth = dob;
            pid.Sex = sex;
            pid.PatientAlias = "";
            pid.Race = "";
            pid.PatientAddress = "";
            pid.CountyCode = "";
            pid.HomePhoneNumber = "";
            pid.BusinessPhoneNumber = "";
            pid.PrimaryLanguage = "";
            pid.MaritalStatus = "";
            pid.Religion = "";
            pid.PatientAccountNumber = "";
            pid.SSNNumber = "";
            pid.DriversLicenseNumber = "";
            pid.MothersIdentifier = "";
            pid.EthnicGroup = "";
            pid.BirthPlace = "";
            pid.MultipleBirthIndicator = "";
            pid.BirthOrder = "";
            pid.Citizenship = "";
            pid.VeteransMilitaryStatus = "";
            pid.Nationality = nation;
            pid.PatientDeathDateAndTime = "";
            pid.PatientDeathIndicator = "";

            return pid;
        }
        public PACsPV1 genPV1(String opdtype, String depcode, String depname)
        {
            PACsPV1 pv1 = new PACsPV1();
            pv1.SetID = "";
            pv1.PatientClass = opdtype;
            pv1.AssignedPatientLocation = depcode+"^"+ depname;
            pv1.AdmissionType = "";
            pv1.PreadmitNumber = "";
            pv1.PriorPatientLocation = "";
            pv1.AttendingDoctor = "";
            pv1.ReferringDoctor = "";
            pv1.ConsultingDoctor = "";
            pv1.HospitalService = "";
            pv1.TemporaryLocation = "";
            pv1.PreadmitTestIndicator = "";
            pv1.ReadmissionIndicator = "";
            pv1.AdmitSource = "";
            pv1.AmbulatoryStatus = "";
            pv1.VIPIndicator = "";
            pv1.AdmittingDoctor = "";
            pv1.PatientType = "";
            pv1.VisitNumber = "";
            pv1.FinancialClass = "";
            pv1.ChargePriceIndicator = "";
            pv1.CourtesyCode = "";
            pv1.CreditRating = "";
            pv1.ContractCode = "";
            pv1.ContractEffectiveDate = "";
            pv1.ContractAmount = "";
            pv1.ContractPeriod = "";
            pv1.InterestCode = "";
            pv1.TransferToBadDebtCode = "";
            pv1.TransferToBadDebtDate = "";
            pv1.BadDebtAgencyCode = "";
            pv1.BadDebtTransferAmount = "";
            pv1.BadDebtRecoveryAmount = "";
            pv1.DeleteAccountIndicator = "";
            pv1.DeleteAccountDate = "";
            pv1.DischargeDisposition = "";
            pv1.DischargedToLocation = "";
            pv1.DietType = "";
            pv1.ServicingFacility = "";
            pv1.BedStatus = "";
            pv1.AccountStatus = "";
            pv1.PendingLocation = "";
            pv1.PriorTemporaryLocation = "";
            pv1.AdmitDateTime = "";
            pv1.DischargeDateTime = "";
            pv1.CurrentPatientBalance = "";
            pv1.TotalCharges = "";
            pv1.TotalAdjustments = "";
            pv1.TotalPayments = "";
            pv1.AlternateVisitID = "";
            pv1.VisitIndicator = "";
            pv1.OtherHealthcareProvider = "";

            return pv1;
        }
        public String PACsADT(PACsMSH msh, PACsEVN evn, PACsPID pid, PACsPV1 pv1)
        {
            String txt = "", MSH="", separate="|", EVN="", PID="", PV1="";
            txt = "\x0b";

            MSH = "MSH" + separate + msh.EncodingCharacters + separate + msh.SendingApplication + separate + msh.SendingFacility + separate + msh.ReceivingApplication
                + separate + msh.ReceivingFacility + separate + msh.DateTimeOfMessage + separate + msh.Security + separate + msh.MessageType
                + separate + msh.MessageControlID + separate + msh.ProcessingID + separate + msh.VersionID + separate + msh.SequenceNumber
                + separate + msh.ContinuationPointer + separate + msh.AcceptAcknowledgementType + separate + msh.ApplicationAcknowledgementType + separate + msh.CountryCode
                + separate + msh.CharacterSet + separate + msh.PrincipalLanguageOfMessage + separate + msh.AlternateCharacterSetHandlingScheme;

            EVN = "EVN" + separate + evn.EventTypeCode + separate + evn.RecordedDateTime + separate + evn.DateTimePlannedEvent + separate + evn.EventReasonCode
                + separate + evn.OperatorID + separate + evn.EventOccurred;

            PID = "PID" + separate + pid.SetID + separate + pid.PatientID + separate + pid.PatientIdentifierList + separate + pid.AlternatePatientID
                + pid.PatientName + separate + pid.MothersMaidenName + separate + pid.DateTimeOfBirth + separate + pid.Sex
                + pid.PatientAlias + separate + pid.Race + separate + pid.PatientAddress + separate + pid.CountyCode
                + pid.HomePhoneNumber + separate + pid.BusinessPhoneNumber + separate + pid.PrimaryLanguage + separate + pid.MaritalStatus
                + pid.Religion + separate + pid.PatientAccountNumber + separate + pid.SSNNumber + separate + pid.DriversLicenseNumber
                + pid.MothersIdentifier + separate + pid.EthnicGroup + separate + pid.BirthPlace + separate + pid.MultipleBirthIndicator
                + pid.BirthOrder + separate + pid.Citizenship + separate + pid.VeteransMilitaryStatus + separate + pid.Nationality
                + pid.PatientDeathDateAndTime + separate + pid.PatientDeathIndicator;

            PV1 = "PV1" + separate + pv1.SetID + separate + pv1.PatientClass + separate + pv1.AssignedPatientLocation + separate + pv1.AdmissionType
                + separate + pv1.PreadmitNumber + separate + pv1.PriorPatientLocation + separate + pv1.AttendingDoctor + separate + pv1.ReferringDoctor
                + separate + pv1.ConsultingDoctor + separate + pv1.HospitalService + separate + pv1.TemporaryLocation + separate + pv1.PreadmitTestIndicator
                + separate + pv1.ReadmissionIndicator + separate + pv1.AdmitSource + separate + pv1.AmbulatoryStatus + separate + pv1.VIPIndicator
                + separate + pv1.AdmittingDoctor + separate + pv1.PatientType + separate + pv1.VisitNumber + separate + pv1.FinancialClass
                + separate + pv1.ChargePriceIndicator + separate + pv1.CourtesyCode + separate + pv1.CreditRating + separate + pv1.ContractCode
                + separate + pv1.ContractEffectiveDate + separate + pv1.ContractAmount + separate + pv1.ContractPeriod + separate + pv1.InterestCode
                + separate + pv1.TransferToBadDebtCode + separate + pv1.TransferToBadDebtDate + separate + pv1.BadDebtAgencyCode + separate + pv1.BadDebtTransferAmount
                + separate + pv1.BadDebtRecoveryAmount + separate + pv1.DeleteAccountIndicator + separate + pv1.DeleteAccountDate + separate + pv1.DischargeDisposition
                + separate + pv1.DischargedToLocation + separate + pv1.DietType + separate + pv1.ServicingFacility + separate + pv1.BedStatus
                + separate + pv1.AccountStatus + separate + pv1.PendingLocation + separate + pv1.PriorTemporaryLocation + separate + pv1.AdmitDateTime
                + separate + pv1.DischargeDateTime + separate + pv1.CurrentPatientBalance + separate + pv1.TotalCharges + separate + pv1.TotalAdjustments
                + separate + pv1.TotalPayments + separate + pv1.AlternateVisitID + separate + pv1.VisitIndicator + separate + pv1.OtherHealthcareProvider;
            txt = txt + MSH + Environment.NewLine + EVN + Environment.NewLine + PID + Environment.NewLine + PV1;
            return txt;
        }
        public String genADT(String reqdept, String hn, String pttprefix, String pttfirstname, String pttlastame, String dob, String sex, String nation, String opdtype, String depcode, String depname)
        {
            String txt = "";
            PACsMSH msh = new PACsMSH();
            PACsPID pid = new PACsPID();
            PACsEVN evn = new PACsEVN();
            PACsPV1 pv1 = new PACsPV1();
            msh = genPAsMSH(reqdept, "ADT^A08");
            pid = genPACsPID(hn, pttprefix, pttfirstname, pttlastame, dob, sex, nation);
            pv1 = genPV1(opdtype, depcode, depname);
            evn = genPACsEVN();

            txt = PACsADT(msh, evn, pid, pv1);
            return txt;
        }
        public PACsORC genORC(String xrayyear, String reqno, String xraycode, String userid, String username)
        {
            String code = "";
            code = bcDB.xrDB.selectPACsInfinittCode(xraycode);
            PACsORC orc = new PACsORC();
            orc.OrderControl = "NW";
            orc.PlacerOrderNumber = xrayyear + reqno + code;
            orc.FillerOrderNumber = "";
            orc.PlacerGroupNumber = "";
            orc.OrderStatus = "SC";
            orc.ResponseFlag = "";
            orc.QuantityTiming = "";
            orc.Parent = "";

            orc.DateTimeOfTransaction = DateTime.Now.Year + DateTime.Now.ToString("MMddHHmmss");
            orc.EnteredBy = userid+"^"+ username;
            orc.VerifiedBy = "";
            orc.OrderingProvider = userid + "^" + username;
            orc.EnterersLocation = "";
            orc.CallBackPhoneNumber = "";
            orc.OrderEffectiveDateTime = DateTime.Now.Year + DateTime.Now.ToString("MMddHHmmss");
            orc.OrderControlCodeReason = "";

            orc.EnteringOrganization = "";
            orc.EnteringDevice = "";
            orc.ActionBy = "";
            orc.AdvancedBeneficiaryNoticeCode = "";
            orc.OrderingFacilityName = "";
            orc.OrderingFacilityAddress = "";
            orc.OrderingFacilityPhoneNumber = "";
            orc.OrderingProviderAddress = "";
            return orc;
        }
        public PACsOBR genOBR(String xrayyear, String reqno, String xraycode,String xrayname, String xraytype, String userid, String username)
        {
            PACsOBR obr = new PACsOBR();
            obr.SetID = "";
            obr.PlacerOrderNumber = "";
            obr.FillerOrderNumber = "";
            String code = "";
            code = bcDB.xrDB.selectPACsInfinittCode(xraycode);
            obr.UniversalServiceID = code + "^"+ xrayname;
            obr.Priority = "";
            obr.RequestedDateTime = "";
            obr.ObservationDateTime = "";
            obr.ObservationEndDateTime = "";

            obr.CollectionVolume = "";
            obr.CollectorIdentifier = "";
            obr.SpecimenActionCode = "";
            obr.DangerCode = "";
            obr.RelevantClinicalInfo = "";
            obr.SpecimenReceivedDateTime = "";
            obr.SpecimenSource = "";
            obr.OrderingProvider = userid + "^" + username;

            obr.OrderCallbackPhoneNumber = "";
            obr.PlacerField1 = xrayyear + reqno + code;
            obr.PlacerField2 = "RP"+ xrayyear + reqno + code;
            obr.FillerField1 = "SS" + xrayyear + reqno + code;
            obr.FillerField2 = "";
            obr.ResultsRptStatusChngDateTime = "";
            obr.ChargeToPractice = "";
            obr.DiagnosticServSectID = xraytype;
            obr.ResultStatus = xraytype;
            obr.ParentResult = "";
            obr.QuantityTiming = "";
            obr.ResultCopiesTo = "";

            obr.ParentNumber = "";
            obr.TransportationMode = "";
            obr.ReasonForStudy = "";
            obr.PrincipalResultInterpreter = "";
            obr.AssistantResultInterpreter = "";
            obr.Technician = "";
            obr.Transcriptionist = "";
            obr.ScheduledDateTime = "";
            obr.NumberOfSampleContainers = "";
            obr.TransportLogisticsOfCollectedSample = "";
            obr.CollectorsComment = "";
            obr.TransportArrangementResponsibility = "";
            obr.TransportArranged = "";
            obr.EscortRequired = "A";
            obr.PlannedPatientTransportComment = "";
            obr.ProcedureCode = "";
            obr.ProcedureCodeModifier = code + "^" + xrayname;
            return obr;
        }
        public PACsZDS genZDS(String xrayyear, String reqno, String xraycode, String modality)
        {
            String code = "";
            code = bcDB.xrDB.selectPACsInfinittCode(xraycode);
            PACsZDS zds = new PACsZDS();
            zds.ZDS_Field1 = "1.2.410.2000010.66.101." + xrayyear + reqno + code;
            zds.ZDS_Field2 ="";
            zds.ZDS_Field3 ="";
            zds.ZDS_Field4 ="";
            zds.ZDS_Field5 ="";
            zds.ZDS_Field6 ="";
            zds.ZDS_Field7 ="";
            zds.ZDS_Field8 ="";
            zds.ZDS_Field9 ="";
            zds.ZDS_Field10 = "";

            return zds;
        }
        public String PACsORM(PACsMSH msh, PACsPID pid, PACsPV1 pv1, PACsORC orc, PACsOBR obr, PACsZDS zds, String comment)
        {
            String txt = "", MSH = "", separate = "|", PID = "", PV1 = "", ORC = "", OBR = "", ZDS = "", NTE="";
            txt = "\x0b";

            MSH = "MSH" + separate + msh.EncodingCharacters + separate + msh.SendingApplication + separate + msh.SendingFacility + separate + msh.ReceivingApplication
                + separate + msh.ReceivingFacility + separate + msh.DateTimeOfMessage + separate + msh.Security + separate + msh.MessageType
                + separate + msh.MessageControlID + separate + msh.ProcessingID + separate + msh.VersionID + separate + msh.SequenceNumber
                + separate + msh.ContinuationPointer + separate + msh.AcceptAcknowledgementType + separate + msh.ApplicationAcknowledgementType + separate + msh.CountryCode
                + separate + msh.CharacterSet + separate + msh.PrincipalLanguageOfMessage + separate + msh.AlternateCharacterSetHandlingScheme;
            
            PID = "PID" + separate + pid.SetID + separate + pid.PatientID + separate + pid.PatientIdentifierList + separate + pid.AlternatePatientID
                + pid.PatientName + separate + pid.MothersMaidenName + separate + pid.DateTimeOfBirth + separate + pid.Sex
                + pid.PatientAlias + separate + pid.Race + separate + pid.PatientAddress + separate + pid.CountyCode
                + pid.HomePhoneNumber + separate + pid.BusinessPhoneNumber + separate + pid.PrimaryLanguage + separate + pid.MaritalStatus
                + pid.Religion + separate + pid.PatientAccountNumber + separate + pid.SSNNumber + separate + pid.DriversLicenseNumber
                + pid.MothersIdentifier + separate + pid.EthnicGroup + separate + pid.BirthPlace + separate + pid.MultipleBirthIndicator
                + pid.BirthOrder + separate + pid.Citizenship + separate + pid.VeteransMilitaryStatus + separate + pid.Nationality
                + pid.PatientDeathDateAndTime + separate + pid.PatientDeathIndicator;

            PV1 = "PV1" + separate + pv1.SetID + separate + pv1.PatientClass + separate + pv1.AssignedPatientLocation + separate + pv1.AdmissionType
                + separate + pv1.PreadmitNumber + separate + pv1.PriorPatientLocation + separate + pv1.AttendingDoctor + separate + pv1.ReferringDoctor
                + separate + pv1.ConsultingDoctor + separate + pv1.HospitalService + separate + pv1.TemporaryLocation + separate + pv1.PreadmitTestIndicator
                + separate + pv1.ReadmissionIndicator + separate + pv1.AdmitSource + separate + pv1.AmbulatoryStatus + separate + pv1.VIPIndicator
                + separate + pv1.AdmittingDoctor + separate + pv1.PatientType + separate + pv1.VisitNumber + separate + pv1.FinancialClass
                + separate + pv1.ChargePriceIndicator + separate + pv1.CourtesyCode + separate + pv1.CreditRating + separate + pv1.ContractCode
                + separate + pv1.ContractEffectiveDate + separate + pv1.ContractAmount + separate + pv1.ContractPeriod + separate + pv1.InterestCode
                + separate + pv1.TransferToBadDebtCode + separate + pv1.TransferToBadDebtDate + separate + pv1.BadDebtAgencyCode + separate + pv1.BadDebtTransferAmount
                + separate + pv1.BadDebtRecoveryAmount + separate + pv1.DeleteAccountIndicator + separate + pv1.DeleteAccountDate + separate + pv1.DischargeDisposition
                + separate + pv1.DischargedToLocation + separate + pv1.DietType + separate + pv1.ServicingFacility + separate + pv1.BedStatus
                + separate + pv1.AccountStatus + separate + pv1.PendingLocation + separate + pv1.PriorTemporaryLocation + separate + pv1.AdmitDateTime
                + separate + pv1.DischargeDateTime + separate + pv1.CurrentPatientBalance + separate + pv1.TotalCharges + separate + pv1.TotalAdjustments
                + separate + pv1.TotalPayments + separate + pv1.AlternateVisitID + separate + pv1.VisitIndicator + separate + pv1.OtherHealthcareProvider;

            ORC = "ORC" + separate + orc.OrderControl + separate + orc.PlacerOrderNumber + separate + orc.FillerOrderNumber + separate + orc.PlacerGroupNumber + separate
                + orc.OrderStatus + separate + orc.ResponseFlag + separate + orc.QuantityTiming + separate + orc.Parent + separate
                + orc.DateTimeOfTransaction + separate + orc.EnteredBy + separate + orc.VerifiedBy + separate + orc.OrderingProvider + separate
                + orc.EnterersLocation + separate + orc.CallBackPhoneNumber + separate + orc.OrderEffectiveDateTime + separate + orc.OrderControlCodeReason + separate
                + orc.EnteringOrganization + separate + orc.EnteringDevice + separate + orc.ActionBy + separate + orc.AdvancedBeneficiaryNoticeCode + separate
                + orc.OrderingFacilityName + separate + orc.OrderingFacilityAddress + separate + orc.OrderingFacilityPhoneNumber + separate + orc.OrderingProviderAddress;

            OBR = "OBR" + separate + obr.SetID + separate + obr.PlacerOrderNumber + separate + obr.FillerOrderNumber + separate + obr.UniversalServiceID + separate
                + obr.Priority + separate + obr.RequestedDateTime + separate + obr.ObservationDateTime + separate + obr.ObservationEndDateTime + separate
                + obr.CollectionVolume + separate + obr.CollectorIdentifier + separate + obr.SpecimenActionCode + separate + obr.DangerCode + separate
                + obr.RelevantClinicalInfo + separate + obr.SpecimenReceivedDateTime + separate + obr.SpecimenSource + separate + obr.OrderingProvider + separate
                + obr.OrderCallbackPhoneNumber + separate + obr.PlacerField1 + separate + obr.PlacerField2 + separate + obr.FillerField1 + separate
                + obr.FillerField2 + separate + obr.ResultsRptStatusChngDateTime + separate + obr.ChargeToPractice + separate + obr.DiagnosticServSectID + separate
                + obr.ResultStatus + separate + obr.ParentResult + separate + obr.QuantityTiming + separate + obr.ResultCopiesTo + separate
                + obr.ParentNumber + separate + obr.TransportationMode + separate + obr.ReasonForStudy + separate + obr.PrincipalResultInterpreter + separate
                + obr.AssistantResultInterpreter + separate + obr.Technician + separate + obr.Transcriptionist + separate + obr.ScheduledDateTime + separate
                + obr.NumberOfSampleContainers + separate + obr.TransportLogisticsOfCollectedSample + separate + obr.CollectorsComment + separate + obr.TransportArrangementResponsibility + separate
                + obr.TransportArranged + separate + obr.EscortRequired + separate + obr.PlannedPatientTransportComment + separate + obr.ProcedureCode
                + obr.ProcedureCodeModifier;

            ZDS = "ZDS" + separate + zds.ZDS_Field1 + separate + zds.ZDS_Field2 + separate + zds.ZDS_Field3 + separate + zds.ZDS_Field4 + separate
                + zds.ZDS_Field5 + separate + zds.ZDS_Field6 + separate + zds.ZDS_Field7 + separate + zds.ZDS_Field8 + separate
                + zds.ZDS_Field9 + separate + zds.ZDS_Field10;
            NTE = "NTE" + separate +"1"+ separate+ comment;
            txt = txt + MSH + Environment.NewLine + PID + Environment.NewLine + PV1 + Environment.NewLine + ORC + Environment.NewLine + OBR + Environment.NewLine + ZDS + Environment.NewLine+ NTE+ "\x0c";
            return txt;
        }
        public String genORM(String reqdept, String hn, String pttprefix, String pttfirstname, String pttlastame, String dob, String sex, String nation
            , String xrayyear, String reqno, String xraycode, String xrayname, String xraytype, String userid, String username, String modality
            , String opdtype, String depcode, String depname, String comment)
        {
            String txt = "";
            PACsMSH msh = new PACsMSH();
            PACsPID pid = new PACsPID();
            PACsPV1 pv1 = new PACsPV1();

            PACsORC orc = new PACsORC();
            PACsOBR obr = new PACsOBR();
            PACsZDS zds = new PACsZDS();
            msh = genPAsMSH(reqdept, "ORM^O01");
            pid = genPACsPID(hn, pttprefix, pttfirstname, pttlastame, dob, sex, nation);
            pv1 = genPV1(opdtype, depcode, depname);
            orc = genORC(xrayyear, reqno, xraycode, userid, username);
            obr = genOBR(xrayyear, reqno, xraycode, xrayname, xraytype, userid, username);
            zds = genZDS(xrayyear, reqno, xraycode, modality);
            txt = PACsORM(msh, pid, pv1, orc, obr, zds, comment);
            return txt;
        }
        public void setControlLabel(ref Label lb, Font fEdit, String text, String name, int x, int y)
        {
            lb.Text = text;
            lb.Font = fEdit;
            lb.Location = new System.Drawing.Point(x, y);
            lb.AutoSize = true;
            lb.Name = name;
        }
        public void setControlC1TextBox(ref C1TextBox txt, Font fEdit, String name, int width, int x, int y)
        {
            //txt = new C1TextBox();
            txt.Font = fEdit;
            txt.Location = new System.Drawing.Point(x, y);
            txt.Size = new Size(width, 30);
            txt.Name = name;
        }
        public void setControlRadioBox(ref RadioButton chk, Font fEdit, String text, String name, int x, int y)
        {
            chk.Checked = false;
            chk.Name = name;
            chk.Text = text;
            chk.Font = fEdit;
            Size size = MeasureString(chk);
            chk.Width = size.Width + 20;
            chk.Location = new Point(x, y);
        }
        public void setControlC1CheckBox(ref C1CheckBox chk, Font fEdit, String text, String name, int x, int y)
        {
            chk.BackColor = System.Drawing.Color.Transparent;
            chk.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            chk.BorderStyle = System.Windows.Forms.BorderStyle.None;
            chk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            chk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            chk.Location = new System.Drawing.Point(x, y);
            chk.Name = name;
            chk.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            chk.Text = text;
            chk.Value = text;
            chk.Font = fEdit;
            Size size = MeasureString(chk);
            chk.Width = size.Width + 30;
            chk.TabIndex = 0;
            //theme1.SetTheme(this.chkVoid, "(default)");
            chk.UseVisualStyleBackColor = true;
            chk.Value = null;
            chk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            
        }
        public void setControlCheckBox(ref CheckBox chk, Font fEdit, String text, String name, int x, int y)
        {
            chk.BackColor = System.Drawing.Color.Transparent;
            //chk.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            //chk.BorderStyle = System.Windows.Forms.BorderStyle.None;
            chk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            //chk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            chk.Location = new System.Drawing.Point(x, y);
            chk.Name = name;
            chk.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            chk.Text = text;
            //chk.Value = text;
            Size size = MeasureString(chk);
            chk.Width = size.Width;
            chk.TabIndex = 0;
            //theme1.SetTheme(this.chkVoid, "(default)");
            chk.UseVisualStyleBackColor = true;
            //chk.Value = null;
            //chk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            chk.Font = fEdit;
        }
        public void setControlC1Button(ref C1Button btn, Font fEdit, String text, String name, int x, int y)
        {
            btn = new C1Button();
            btn.Name = name;
            btn.Text = text;
            btn.Font = fEdit;
            btn.Location = new System.Drawing.Point(x, y);
            btn.Size = new Size(MeasureString(btn).Width + 50, 30);
            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextAlign = ContentAlignment.MiddleRight;
            btn.Font = fEdit;
        }
        public void setControlC1ComboBox(ref C1ComboBox cbo, String name, int width, int x, int y)
        {
            cbo.AllowSpinLoop = false;
            cbo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            cbo.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            cbo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            cbo.GapHeight = 0;
            cbo.ImagePadding = new System.Windows.Forms.Padding(0);
            cbo.ItemsDisplayMember = "";
            cbo.ItemsValueMember = "";
            cbo.Location = new System.Drawing.Point(x, y);
            cbo.Name = name;
            cbo.Size = new System.Drawing.Size(65, 20);
            cbo.Style.DropDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            cbo.Style.DropDownBorderColor = System.Drawing.Color.Gainsboro;
            cbo.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            cbo.TabIndex = 538;
            cbo.Tag = null;
            cbo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            cbo.Size = new Size(width, 30);
        }
        public void setControlC1DateTimeEdit(ref C1DateEdit txt, String name, int x, int y)
        {
            txt.AllowSpinLoop = false;
            txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txt.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            txt.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txt.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txt.CurrentTimeZone = false;
            txt.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            txt.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txt.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txt.EditFormat.CustomFormat = "dd/MM/yyyy";
            txt.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txt.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txt.GMTOffset = System.TimeSpan.Parse("00:00:00");
            txt.ImagePadding = new System.Windows.Forms.Padding(0);
            //txt.Culture = 1033;     // English US

            txt.Location = new System.Drawing.Point(x, y);
            txt.Name = name;
            txt.Size = new System.Drawing.Size(111, 20);
            txt.TabIndex = 12;
            txt.Tag = null;
            txt.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
        }
        public void setControlC1FlexViewer(ref C1FlexViewer fvPrnEmailSummary, String name)
        {
            fvPrnEmailSummary = new C1FlexViewer();
            fvPrnEmailSummary.AutoScrollMargin = new System.Drawing.Size(0, 0);
            fvPrnEmailSummary.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            fvPrnEmailSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            fvPrnEmailSummary.Location = new System.Drawing.Point(0, 0);
            fvPrnEmailSummary.Name = name;
            fvPrnEmailSummary.Size = new System.Drawing.Size(1065, 790);
            fvPrnEmailSummary.TabIndex = 0;
            fvPrnEmailSummary.Ribbon.Minimized = true;
        }
        public Boolean readOperation()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\opration.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                operation = File.ReadAllLines(filename);
            }
            return chk;
        }
        public Boolean writeOperation()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\opration.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                File.WriteAllLines(filename, operation, Encoding.UTF8);
            }
            else
            {
                File.CreateText(filename);
            }
            return chk;
        }
        public Boolean readPostOperation()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\postopration.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                postoperation = File.ReadAllLines(filename);
            }            
            return chk;
        }
        public Boolean writePostOperation()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\postopration.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                File.WriteAllLines(filename, postoperation, Encoding.UTF8);
            }
            else
            {
                File.CreateText(filename);
            }
            return chk;
        }
        public Boolean readPreOperation()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\preopration.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                preoperation = File.ReadAllLines(filename);
                
            }
            return chk;
        }
        public Boolean writePreOperation()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\preopration.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                File.WriteAllLines(filename, preoperation, Encoding.UTF8);
            }
            else
            {
                File.CreateText(filename);
            }
            return chk;
        }
        public Boolean readFinding()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\finding.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                fining = File.ReadAllLines(filename);
            }
            return chk;
        }
        public Boolean readProcidures()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\procidures.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                procidures = File.ReadAllLines(filename);
            }
            return chk;
        }
        public Stream ToStream(String str)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public Stream ToStreamTxt(String[] str)
        {
            if (str == null) return null;
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            foreach(String txt in str)
            {
                writer.Write(txt+ Environment.NewLine);
            }
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public String exportResultXray(DataTable dt, String hn, String vn, String flagOpenFile, String pathFolder)
        {
            //ReportDocument rpt;
            //CrystalReportViewer crv = new CrystalReportViewer();
            //rpt = new ReportDocument();
            //rpt.Load("xray_result.rpt");

            //crv.ReportSource = rpt;

            //crv.Refresh();
            ////rpt.Load(Application.StartupPath + "\\lab_opu_embryo_dev.rpt");
            ////rd.Load("StudentReg.rpt");
            //rpt.SetDataSource(dt);
            //rpt.SetParameterValue("line1", iniC.hostname);
            ////crv.ReportSource = rd;
            ////crv.Refresh();
            ////if (!Directory.Exists(iniC.medicalrecordexportpath))
            ////{
            ////    Directory.CreateDirectory(iniC.medicalrecordexportpath);
            ////}
            ////if (!Directory.Exists(iniC.medicalrecordexportpath + "\\" + hn))
            ////{
            ////    Directory.CreateDirectory(pathFolder);
            ////}
            //string filePath = pathFolder + "\\result_xray_" + hn + "_" + vn.Replace("/", "_") + ".pdf";
            //if (File.Exists(filePath))
            //    File.Delete(filePath);

            //ExportOptions CrExportOptions;
            //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            //CrDiskFileDestinationOptions.DiskFileName = filePath;
            //CrExportOptions = rpt.ExportOptions;
            //{
            //    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //    CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //}
            //rpt.Export();

            //if (!File.Exists(filePath))
            //{
            //    return "";
            //}

            //C1PdfDocumentSource pdf = new C1PdfDocumentSource();
            String filename = "", ext = "";

            //filename = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileNameWithoutExtension(filePath) + ".jpg";
            //var exporter = pdf.SupportedExportProviders[4].NewExporter();
            //exporter.ShowOptions = false;
            //exporter.FileName = filename;

            //pdf.LoadFromFile(filePath);
            //pdf.Export(exporter);

            //if (flagOpenFile.Equals("open"))
            //{
            //    // combine the arguments together
            //    // it doesn't matter if there is a space after ','
            //    string argument = "/select, \"" + filePath + "\"";

            //    System.Diagnostics.Process.Start("explorer.exe", argument);
            //}
            return filename;
        }
        public String exportResultLab(DataTable dt, String hn, String vn, String flagOpenFile, String pathFolder)
        {
            //ReportDocument rpt;
            //CrystalReportViewer crv = new CrystalReportViewer();
            //rpt = new ReportDocument();
            //rpt.Load("lab_result.rpt");
            //crv.ReportSource = rpt;

            //crv.Refresh();
            ////rpt.Load(Application.StartupPath + "\\lab_opu_embryo_dev.rpt");
            ////rd.Load("StudentReg.rpt");
            //rpt.SetDataSource(dt);
            //rpt.SetParameterValue("line1", iniC.hostname);
            ////crv.ReportSource = rd;
            ////crv.Refresh();
            //if (!Directory.Exists(pathFolder))
            //{
            //    Directory.CreateDirectory(pathFolder);
            //}
            ////if (!Directory.Exists(iniC.medicalrecordexportpath+"\\"+hn))
            ////{
            ////    Directory.CreateDirectory(iniC.medicalrecordexportpath + "\\" + hn);
            ////}
            //string filePath = pathFolder + "\\result_lab_" + hn + "_" + vn.Replace("/", "_") + ".pdf";
            //if (File.Exists(filePath))
            //    File.Delete(filePath);

            //ExportOptions CrExportOptions;
            //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            //CrDiskFileDestinationOptions.DiskFileName = filePath;
            //CrExportOptions = rpt.ExportOptions;
            //{
            //    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //    CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //}
            //rpt.Export();
            ////frmW.Dispose();

            //if (!File.Exists(filePath))
            //{
            //    return "";
            //}

            //C1PdfDocumentSource pdf = new C1PdfDocumentSource();
            String filename = "", ext = "";

            //filename = Path.GetDirectoryName(filePath)+"\\"+ Path.GetFileNameWithoutExtension(filePath)+".jpg";
            //var exporter = pdf.SupportedExportProviders[4].NewExporter();
            //exporter.ShowOptions = false;
            //exporter.FileName = filename;

            //pdf.LoadFromFile(filePath);
            //pdf.Export(exporter);

            //if (flagOpenFile.Equals("open"))
            //{
            //    // combine the arguments together
            //    // it doesn't matter if there is a space after ','
            //    string argument = "/select, \"" + filePath + "\"";

            //    System.Diagnostics.Process.Start("explorer.exe", argument);
            //}
            return filename;
        }
        public String exportResultPharmacy(DataTable dt, String hn, String vn, String flagExpoler)
        {
            //ReportDocument rpt;
            //CrystalReportViewer crv = new CrystalReportViewer();
            //rpt = new ReportDocument();
            //rpt.Load("pharmacy_result.rpt");
            //crv.ReportSource = rpt;

            //crv.Refresh();
            ////rpt.Load(Application.StartupPath + "\\lab_opu_embryo_dev.rpt");
            ////rd.Load("StudentReg.rpt");
            //rpt.SetDataSource(dt);
            //rpt.SetParameterValue("line1", iniC.hostname);
            ////crv.ReportSource = rd;
            ////crv.Refresh();
            String filename = "";
            //filename = iniC.medicalrecordexportpath + "\\result_pharmacy_" + hn + "_" + vn.Replace("/", "_") + ".pdf";
            //if (!Directory.Exists(iniC.medicalrecordexportpath))
            //{
            //    Directory.CreateDirectory(iniC.medicalrecordexportpath);
            //}
            //if (File.Exists(filename))
            //    File.Delete(filename);

            //ExportOptions CrExportOptions;
            //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            //CrDiskFileDestinationOptions.DiskFileName = filename;
            //CrExportOptions = rpt.ExportOptions;
            //{
            //    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //    CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //}
            //rpt.Export();
            ////frmW.Dispose();

            string filePath = filename;
            //if (!File.Exists(filePath))
            //{
            //    return "";
            //}

            //// combine the arguments together
            //// it doesn't matter if there is a space after ','
            //if (flagExpoler.Equals("open"))
            //{
            //    string argument = "/select, \"" + filePath + "\"";
            //    System.Diagnostics.Process.Start("explorer.exe", argument);
            //}
            return filePath;
        }
        public string NumberToText(long number)
        {
            StringBuilder wordNumber = new StringBuilder();

            string[] powers = new string[] { "Thousand ", "Million ", "Billion " };
            string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string[] ones = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                                       "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

            if (number == 0) { return "Zero"; }
            if (number < 0)
            {
                wordNumber.Append("Negative ");
                number = -number;
            }

            long[] groupedNumber = new long[] { 0, 0, 0, 0 };
            int groupIndex = 0;

            while (number > 0)
            {
                groupedNumber[groupIndex++] = number % 1000;
                number /= 1000;
            }

            for (int i = 3; i >= 0; i--)
            {
                long group = groupedNumber[i];

                if (group >= 100)
                {
                    wordNumber.Append(ones[group / 100 - 1] + " Hundred ");
                    group %= 100;

                    if (group == 0 && i > 0)
                        wordNumber.Append(powers[i - 1]);
                }

                if (group >= 20)
                {
                    if ((group % 10) != 0)
                        wordNumber.Append(tens[group / 10 - 2] + " " + ones[group % 10 - 1] + " ");
                    else
                        wordNumber.Append(tens[group / 10 - 2] + " ");
                }
                else if (group > 0)
                    wordNumber.Append(ones[group - 1] + " ");

                if (group != 0 && i > 0)
                    wordNumber.Append(powers[i - 1]);
            }

            return wordNumber.ToString().Trim();
        }
        public string NumberToCurrencyTextThaiBaht(decimal number, MidpointRounding midpointRounding)
        {
            // Round the value just in case the decimal value is longer than two digits
            number = decimal.Round(number, 2, midpointRounding);

            string wordNumber = string.Empty;

            // Divide the number into the whole and fractional part strings
            string[] arrNumber = number.ToString().Split('.');

            // Get the whole number text
            long wholePart = long.Parse(arrNumber[0]);
            string strWholePart = NumberToText(wholePart);

            // For amounts of zero dollars show 'No Dollars...' instead of 'Zero Dollars...'
            //wordNumber = (wholePart == 0 ? "No" : strWholePart) + (wholePart == 1 ? " Dollar and " : " Dollars and ");
            wordNumber = (wholePart == 0 ? "No" : strWholePart) + (wholePart == 1 ? " Baht " : " Baht ");

            // If the array has more than one element then there is a fractional part otherwise there isn't
            // just add 'No Cents' to the end
            //if (arrNumber.Length > 1)
            //{
            //    // If the length of the fractional element is only 1, add a 0 so that the text returned isn't,
            //    // 'One', 'Two', etc but 'Ten', 'Twenty', etc.
            //    long fractionPart = long.Parse((arrNumber[1].Length == 1 ? arrNumber[1] + "0" : arrNumber[1]));
            //    string strFarctionPart = NumberToText(fractionPart);

            //    wordNumber += (fractionPart == 0 ? " No" : strFarctionPart) + (fractionPart == 1 ? " Cent" : " Cents");
            //}
            //else
            //    wordNumber += "No Cents";

            return wordNumber;
        }
        public string ThaiBahtText(string strNumber, bool IsTrillion = false)
        {
            string BahtText = "";
            string strTrillion = "";
            string[] strThaiNumber = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] strThaiPos = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };

            decimal decNumber = 0;
            decimal.TryParse(strNumber, out decNumber);

            if (decNumber == 0)
            {
                return "ศูนย์บาทถ้วน";
            }

            strNumber = decNumber.ToString("0.00");
            string strInteger = strNumber.Split('.')[0];
            string strSatang = strNumber.Split('.')[1];

            if (strInteger.Length > 13)
                throw new Exception("รองรับตัวเลขได้เพียง ล้านล้าน เท่านั้น!");

            bool _IsTrillion = strInteger.Length > 7;
            if (_IsTrillion)
            {
                strTrillion = strInteger.Substring(0, strInteger.Length - 6);
                BahtText = ThaiBahtText(strTrillion, _IsTrillion);
                strInteger = strInteger.Substring(strTrillion.Length);
            }

            int strLength = strInteger.Length;
            for (int i = 0; i < strInteger.Length; i++)
            {
                string number = strInteger.Substring(i, 1);
                if (number != "0")
                {
                    if (i == strLength - 1 && number == "1" && strLength != 1)
                    {
                        BahtText += "เอ็ด";
                    }
                    else if (i == strLength - 2 && number == "2" && strLength != 1)
                    {
                        BahtText += "ยี่";
                    }
                    else if (i != strLength - 2 || number != "1")
                    {
                        BahtText += strThaiNumber[int.Parse(number)];
                    }

                    BahtText += strThaiPos[(strLength - i) - 1];
                }
            }

            if (IsTrillion)
            {
                return BahtText + "ล้าน";
            }

            if (strInteger != "0")
            {
                BahtText += "บาท";
            }

            if (strSatang == "00")
            {
                BahtText += "ถ้วน";
            }
            else
            {
                strLength = strSatang.Length;
                for (int i = 0; i < strSatang.Length; i++)
                {
                    string number = strSatang.Substring(i, 1);
                    if (number != "0")
                    {
                        if (i == strLength - 1 && number == "1" && strSatang[0].ToString() != "0")
                        {
                            BahtText += "เอ็ด";
                        }
                        else if (i == strLength - 2 && number == "2" && strSatang[0].ToString() != "0")
                        {
                            BahtText += "ยี่";
                        }
                        else if (i != strLength - 2 || number != "1")
                        {
                            BahtText += strThaiNumber[int.Parse(number)];
                        }

                        BahtText += strThaiPos[(strLength - i) - 1];
                    }
                }
                BahtText += "สตางค์";
            }
            return BahtText;
        }
        public String selectDoctorName(String doctorId)
        {
            DataTable dt = new DataTable();
            String sql = "", chk = "-";
            sql = "Select  patient_m02.MNC_PFIX_DSC as prefix,patient_m26.MNC_DOT_FNAME as Fname,patient_m26.MNC_DOT_LNAME as Lname,patient_m26.MNC_DOT_FNAME_e,patient_m26.MNC_DOT_LNAME_e  " +
                "From  patient_m26  " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                "where patient_m26.MNC_DOT_CD = '" + doctorId + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0]["prefix"].ToString() + " " + dt.Rows[0]["Fname"].ToString() + " " + dt.Rows[0]["Lname"].ToString();
            }
            return chk;
        }
        public String selectDoctorNameE(String doctorId)
        {
            DataTable dt = new DataTable();
            String sql = "", chk = "-";
            sql = "Select  patient_m02.MNC_PFIX_DSC as prefix,patient_m26.MNC_DOT_FNAME as Fname,patient_m26.MNC_DOT_LNAME as Lname,patient_m26.MNC_DOT_FNAME_e,patient_m26.MNC_DOT_LNAME_e  " +
                "From  patient_m26  " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                "where patient_m26.MNC_DOT_CD = '" + doctorId + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                chk = "DR. " + dt.Rows[0]["MNC_DOT_FNAME_e"].ToString() + " " + dt.Rows[0]["MNC_DOT_LNAME_e"].ToString();
            }
            return chk;
        }
        public String datetoShow2(String date)
        {
            String dd = "", mm = "", yyyy = "", re = "";
            int dd1 = 0, mm1 = 0, yyyy1 = 0;
            re = date;
            if (date.Length >= 12)
            {
                dd = date.Substring(0, 2);
                mm = date.Substring(4, 2);
                yyyy = date.Substring(yyyy.Length - 4);
                int.TryParse(dd, out dd1);
                int.TryParse(mm, out mm1);
                int.TryParse(yyyy, out yyyy1);
                if (yyyy1 > 2500) yyyy1 = yyyy1 - 543;
                DateTime date1 = new DateTime(yyyy1, mm1, dd1);
                re = date1.ToString("dd") + "/" + date1.ToString("MMM") + "/" + date1.ToString("yyyy");
            }

            return re;
        }
    }
}
