﻿using System;
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
        public String hostDBPACs = "", userDBPACs = "", passDBPACs = "", nameDBPACs = "", portDBPACs = "";
        public String hostDBLabOut = "", userDBLabOut = "", passDBLabOut = "", nameDBLabOut = "", portDBLabOut = "";
        public String hostDBIm = "", userDBIm = "", passDBIm = "", nameDBIm = "", portDBIm = "";
        public String hostFTP = "", userFTP = "", passFTP = "", portFTP = "", folderFTP = "", usePassiveFTP = "", ProxyProxyType="", ProxyHost="", ProxyPort="";
        public String hostFTPLabOut = "", userFTPLabOut = "", passFTPLabOut = "", portFTPLabOut = "", folderFTPLabOut = "", usePassiveFTPLabOut = "";
        public String hostFTPLabOutMedica = "", userFTPLabOutMedica = "", passFTPLabOutMedica = "", portFTPLabOutMedica = "", folderFTPLabOutMedica = "", usePassiveFTPLabOutMedica = "";
        public String hostDBOPBKK = "", userDBOPBKK = "", passDBOPBKK = "", nameDBOPBKK = "", portDBOPBKK = "";

        public String grdViewFontSize = "", grdViewFontName = "", themeApplication = "", txtFocus = "", grfRowColor = "", pdfFontSize="", pdfFontName = "", pdfFontSizetitleFont = "", pdfFontSizetxtFont = "", pdfFontSizehdrFont = "", pdfFontSizetxtFontB="";
        public String email_form = "", email_auth_user = "", email_auth_pass = "", email_port = "", email_ssl = "";

        public String sticker_donor_width = "", sticker_donor_height = "", sticker_donor_start_x = "", sticker_donor_start_y = "", sticker_donor_barcode_height = "", sticker_donor_barcode_gap_x = "", sticker_donor_barcode_gap_y = "", sticker_donor_gap="";
        public String statusAppDonor = "", patientaddpanel1weight="", barcode_width_minus="", status_show_border="";
        public String themeDonor = "", printerSticker="", hostname="", themeApp="", ssoid="", hostnamee="", hostaddresst="", hostaddresse="";
        public String timerImgScanNew = "", pathImageScan = "", imggridscanwidth = "", txtSearchHnLenghtStart = "";
        public String pathLabOutReceiveInnoTech="", pathLabOutBackupInnoTech="", pathLabOutReceiveRIA = "", pathLabOutBackupRIA = "", pathLabOutBackupRIAZipExtract="", pathLabOutBackupManual="";
        public String printerA4 = "", programLoad="", labOutOpenFileDialog="", windows="", grfScanWidth="", imgScanWidth = "", pathScanStaffNote="", pathScreenCaptureUpload = "", pathIniFile="", statusShowPrintDialog="";
        public String timerCheckLabOut = "", pathProgramCapture = "", pathTempScanAdd="", station="", pathDownloadFile="", pathScreenCaptureSend="", pacsServerIP="", pacsServerPort="", tabLabOutImageHeight="", tabLabOutImageWidth="";
        public String statusShowLabOutFrmLabOutReceiveView = "", pathLabOutReceiveMedica="", pathLabOutBackupMedica="", statusLabOutReceiveOnline="", laboutMedicahosp_code="", statusLabOutAutoPrint="", printerLabOut="";
        public String statusLabOutReceiveTabShow = "", branchId="", pathline_bot_labout_urgent_bangna="", grfImgWidth = "", scVssizeradio="", laboutdateMedica="", medicalrecordexportpath="", themegrfOpd="", themegrfIpd = "", statusoutlabMedica="";
        public String imageCC_width = "", imageME_width = "", imageDiag_width = "", imageCC_Height = "", imageME_Height = "", imageDiag_Height = "";

        public String OPD_BTEMP = "", OPD_SBP = "", OPD_DBP = "", OPD_PR = "", OPD_RR = "", opbkkhcode = "";
    }
}
