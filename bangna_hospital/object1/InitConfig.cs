using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class InitConfig
    {
        public String hostDB = "", userDB = "", passDB = "", nameDB = "", portDB = "";
        public String hostDBMainHIS = "", userDBMainHIS = "", passDBMainHIS = "", nameDBMainHIS = "", portDBMainHIS = "";
        public String hostDBBACK = "", userDBBACK = "", passDBBACK = "", nameDBBACK = "", portDBBACK = "";
        public String hostDBPACs = "", userDBPACs = "", passDBPACs = "", nameDBPACs = "", portDBPACs = "";
        public String hostDBLabOut = "", userDBLabOut = "", passDBLabOut = "", nameDBLabOut = "", portDBLabOut = "";
        public String hostDBIm = "", userDBIm = "", passDBIm = "", nameDBIm = "", portDBIm = "";
        public String hostFTP = "", userFTP = "", passFTP = "", portFTP = "", folderFTP = "", usePassiveFTP = "", ProxyProxyType = "", ProxyHost = "", ProxyPort = "";
        public String hostFTPLabOut = "", userFTPLabOut = "", passFTPLabOut = "", portFTPLabOut = "", folderFTPLabOut = "", usePassiveFTPLabOut = "";
        public String hostFTPLabOutMedica = "", userFTPLabOutMedica = "", passFTPLabOutMedica = "", portFTPLabOutMedica = "", folderFTPLabOutMedica = "", usePassiveFTPLabOutMedica = "";
        public String hostDBOPBKK = "", userDBOPBKK = "", passDBOPBKK = "", nameDBOPBKK = "", portDBOPBKK = "";
        public String hostDBLogTask = "", userDBLogTask = "", passDBLogTask = "", nameDBLogTask = "", portDBLogTask = "";
        public String hostDBMySQL = "", userDBMySQL = "", passDBMySQL = "", nameDBMySQL = "", portDBMySQL = "";
        public String hostDBSsnData = "", userDBSsnData = "", passDBSsnData = "", nameDBSsnData = "", portDBSsnData = "";
        public String hostDBLinkLIS = "", nameDBLinkLIS = "", userDBLinkLIS = "", passDBLinkLIS = "", portDBLinkLIS = "";
        public String hostFTPCertMed = "", userFTPCertMed = "", passFTPCertMed = "", portFTPCertMed = "", folderFTPCertMed = "", usePassiveFTPCertMed = "";
        public String hostFTPDrugIn = "", userFTPDrugIn = "", passFTPDrugIn = "", portFTPDrugIn = "", folderFTPDrugIn = "", usePassiveFTPDrugIn = "" , EnableSsl="";
        public String hostFTPbangnadoe = "", userFTPbangnadoe = "", passFTPbangnadoe = "", portFTPbangnadoe = "", folderFTPbangnadoe = "", usePassiveFTPbangnadoe = "";
        public String hostFTPCertMeddoe = "", userFTPCertMeddoe = "", passFTPCertMeddoe = "", portFTPCertMeddoe = "", folderFTPCertMeddoe = "", usePassiveFTPCertMeddoe = "";

        public String grdViewFontSize = "", grdViewFontName = "", themeApplication = "", txtFocus = "", grfRowColor = "", pdfFontSize = "", pdfFontName = "", pdfFontSizetitleFont = "", pdfFontSizetxtFont = "", pdfFontSizehdrFont = "", pdfFontSizetxtFontB = "";
        public String email_form = "", email_auth_user = "", email_auth_pass = "", email_port = "", email_ssl = "";
        public String EmailFromAIPN = "", EmailToAIPN = "", EmailSubjectAIPN = "", EmailPortAIPN = "", EmailAuthUserAIPN = "", EmailAuthPassAIPN = "";

        public String sticker_donor_width = "", sticker_donor_height = "", sticker_donor_start_x = "", sticker_donor_start_y = "", sticker_donor_barcode_height = "", sticker_donor_barcode_gap_x = "", sticker_donor_barcode_gap_y = "", sticker_donor_gap = "";
        public String statusAppDonor = "", patientaddpanel1weight = "", barcode_width_minus = "", status_show_border = "";
        public String themeDonor = "", printerSticker = "", hostname = "", themeApp = "", ssoid = "", hostnamee = "", hostaddresst = "", hostaddresse = "";
        public String timerImgScanNew = "", pathImageScan = "", imggridscanwidth = "", txtSearchHnLenghtStart = "";
        public String pathLabOutReceiveInnoTech = "", pathLabOutBackupInnoTech = "", pathLabOutReceiveRIA = "", pathLabOutBackupRIA = "", pathLabOutBackupRIAZipExtract = "", pathLabOutBackupManual = "";
        public String printerA4 = "", programLoad = "", labOutOpenFileDialog = "", windows = "", grfScanWidth = "", imgScanWidth = "", pathScanStaffNote = "", pathScreenCaptureUpload = "", pathIniFile = "", statusShowPrintDialog = "";
        public String timerCheckLabOut = "", pathProgramCapture = "", pathTempScanAdd = "", station = "", pathDownloadFile = "", pathScreenCaptureSend = "", pacsServerIP = "", pacsServerPort = "", tabLabOutImageHeight = "", tabLabOutImageWidth = "";
        public String statusShowLabOutFrmLabOutReceiveView = "", pathLabOutReceiveMedica = "", pathLabOutBackupMedica = "", statusLabOutReceiveOnline = "", laboutMedicahosp_code = "", statusLabOutAutoPrint = "", printerLabOut = "";
        public String statusLabOutReceiveTabShow = "", branchId = "", pathline_bot_labout_urgent_bangna = "", grfImgWidth = "", scVssizeradio = "", laboutdateMedica = "", medicalrecordexportpath = "", themegrfOpd = "", themegrfIpd = "", statusoutlabMedica = "";
        public String imageCC_width = "", imageME_width = "", imageDiag_width = "", imageCC_Height = "", imageME_Height = "", imageDiag_Height = "";

        public String OPD_BTEMP = "", OPD_SBP = "", OPD_DBP = "", OPD_PR = "", OPD_RR = "", opbkkhcode = "";
        public String statusSmartCardNoDatabase = "", lab_code = "", printerStaffNote = "", printerLeter = "", printerA5 = "", printerQueue = "", pathSaveExcelNovel = "", statusSmartCardvaccine = "", statusPrintSticker = "";
        public String FrmSmartCardTabDefault = "", stickerPrintNumber = "", statusStation = "", paidcode = "", dtrcode = "", queFontName = "", queFontSize = "", hosttel = "", printerStickerDrug = "", printadjust = "";
        public String importMDBpaidcode = "", statusVisitBack = "", aipnXmlPath = "", aipnAuthorName = "", ssopXmlPath = "";
        public String grdQueFontSize = "", grdQueFontName = "", grdQueTodayFontSize = "", grdQueTodayFontName = "", grfRowHeight = "";
        public String pathLabOutReceiveATTA = "", pathLabOutBackupATTA = "", statusScreenCaptureUploadDoc = "", padYCertMed = "", statusScreenCaptureAutoSend = "", statusPrintPreview = "", nightTime = "", nightTimeOn = "", statusAutoPrintLabResult = "";
        public String statusdoctorold = "", linkmedicalscan = "", applicationrunnextrecord = "", statusdruginon = "", doealientoken="", urlbangnadoe="", provcode="", urlbangnadoeresult="";
        public String pathdoealiencert = "", pathlocalStaffNote="", compcodedoe="", staffNoteFontName="", staffNoteFontSize="", usersharepathstaffnote="", passwordsharepathstaffnote = "";
        public String pathapp = "", pathlocalEKG="",pathlocalDocOLD="", pathlocalEST = "", pathlocalECHO="", pathlocalHolter="", statusPasswordConfirm="", statusShowMessageError="", scannername="";
        public String deptphone = "";
    }
}
