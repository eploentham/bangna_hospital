using AutocompleteMenuNS;
using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1Document;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SplitContainer;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using C1.Win.FlexViewer;
using C1.Win.ImportServices.ReportingService4;
using GrapeCity.ActiveReports.Document.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static bangna_hospital.object1.LabOutRIAaiJson;
using static C1.Win.C1Preview.Strings;
using Column = C1.Win.C1FlexGrid.Column;
using Patient = bangna_hospital.object1.Patient;
using Row = C1.Win.C1FlexGrid.Row;

namespace bangna_hospital.gui
{
    public partial class FrmOPD : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Patient PTT;
        Visit VS;
        PatientT07 APM;
        C1ThemeController theme1;
        C1FlexGrid grfOperList, grfCheckUPList, grfPttApm, grfOrder, grfIPD, grfIPDScan, grfOPD, grfOutLab, grfHisOrder, grfLab, grfXray;
        Boolean pageLoad = false, tabMedScanActiveNOtabOutLabActive=true;
        Image imgCorr, imgTran;
        Timer timeOperList;
        String PRENO = "", VSDATE = "", HN="", DEPTNO="", HNmedscan="", DOCGRPID = "", DSCID = "";
        Stream streamPrint, streamPrintL, streamPrintR, streamDownload;
        Image resizedImage, IMG;
        Form frmImg;
        C1PictureBox pic;
        C1FlexViewer fvCerti;


        int originalHeight = 0, newHeight = 720, mouseWheel = 0, rowGrfOper = 0;
        
        int colgrfPttApmVsDate = 1, colgrfPttApmApmDate = 2, colgrfPttApmApmTime = 3, colgrfPttApmHN = 4, colgrfPttApmPttName = 5, colgrfPttApmDeptR = 6, colgrfPttApmDeptMake = 7, colgrfPttApmNote = 8, colgrfPttApmOrder = 9, colgrfPttApmDocYear = 10, colgrfPttApmDocNo = 11, colgrfPttApmDtrname = 12, colgrfPttApmPhone = 13, colgrfPttApmPaidName = 14, colgrfPttApmRemarkCall = 15, colgrfPttApmStatusRemarkCall = 16, colgrfPttApmRemarkCallDate = 17;
        int colGrfOrderCode = 1, colGrfOrderName = 2, colGrfOrderItemStatus=3;
        int colgrfCheckUPHn = 1, colgrfCheckUPFullNameT = 2, colgrfCheckUPSymtom = 3, colgrfCheckUPEmployer = 4, colgrfCheckUPVsDate = 5, colgrfCheckUPPreno = 6;
        int colgrfOperListHn = 1, colgrfOperListFullNameT = 2, colgrfOperListSymptoms = 3, colgrfOperListPaidName = 4, colgrfOperListPreno = 5, colgrfOperListVsDate = 6, colgrfOperListVsTime = 7, colgrfOperListActNo = 8, colgrfOperListDtrName=9, colgrfOperListLab=10, colgrfOperListXray=11;
        int colIPDDate = 1, colIPDDept = 2, colIPDAnShow = 4, colIPDStatus = 3, colIPDPreno = 5, colIPDVn = 6, colIPDAndate = 7, colIPDAnYr = 8, colIPDAn = 9, colIPDDtrName = 10;
        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
        int colVsVsDate = 1, colVsDept = 2, colVsVn = 3, colVsStatus = 4, colVsPreno = 5, colVsAn = 6, colVsAndate = 7, colVsPaidType = 8, colVsHigh = 9, colVsWeight = 10, colVsTemp = 11, colVscc = 12, colVsccin = 13, colVsccex = 14, colVsabc = 15, colVshc16 = 16, colVsbp1r = 17, colVsbp1l = 18, colVsbp2r = 19, colVsbp2l = 20, colVsVital = 21, colVsPres = 22, colVsRadios = 23, colVsBreath = 24, colVsVsDate1 = 25, colVsDtrName = 26;
        int colgrfOutLabDscHN = 1, colgrfOutLabDscName=2, colgrfOutLabDscVsDate=3, colgrfOutLabDscVN=4, colgrfOutLabDscId=5;
        int colOrderId = 1, colOrderDate = 2, colOrderName = 3, colOrderQty = 4, colOrderFre = 5, colOrderIn1 = 6, colOrderMed = 7;
        int colLabDate = 1, colLabName = 2, colLabNameSub = 3, colLabResult = 4, colInterpret = 5, colNormal = 6, colUnit = 7;
        int colXrayDate = 1, colXrayCode = 2, colXrayName = 3, colXrayResult = 4;

        Label lbDocAll, lbDocGrp1, lbDocGrp2, lbDocGrp3, lbDocGrp4, lbDocGrp5, lbDocGrp6, lbDocGrp7, lbDocGrp8, lbDocGrp9;
        Color colorLbDoc;
        listStream strm;
        List<listStream> lStream, lStreamPic;
        AutoCompleteStringCollection acmApmTime, autoLab, autoXray, autoHotC, autoPhy;
        string[] AUTOSymptom = { "07:00-08:00","08:00-09:00","09:00-10:00","10:00-11:00","11:00-12:00","12:00-13:00","13:00-14:00","14:00-15:00","15:00-16:00","16:00-17:00","17:00-18:00"
                ,"18:00-19:00","19:00-20:00","20:00-21:00"
        };
        Label lbLoading;
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmOPD(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
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
            autoHotC = new AutoCompleteStringCollection();
            txtPttApmDate.Value = DateTime.Now;
            lStream = new List<listStream>();

            initLoading();
            lfSbMessage.Text = bc.bcDB.pttDB.selectDeptIPDName(bc.iniC.station);
            bc.bcDB.pm02DB.setCboPrefixT(cboCheckUPPrefixT, "");
            bc.bcDB.pm02DB.setCboPrefixE(cboCheckUPPrefixE, "");
            bc.setCboSex(cboCheckUPSex);
            bc.bcDB.pm04DB.setCboNation(cboCheckUPNat, "");
            bc.bcDB.pttDB.setCboDeptOPDNew(cboApmDept, "");

            initGrfCheckUPList();
            initGrfOperList();
            initGrfPttApm();
            initGrfOrder();
            initGrfIPD();
            initGrfIPDScan();
            initGrfOPD();
            initGrfOutLab();
            initGrfLab();
            initGrfHisOrder();

            theme1.SetTheme(grfCheckUPList, "Violette");
            theme1.SetTheme(panel8, "Violette");
            theme1.SetTheme(groupBox4, "Violette");
            theme1.SetTheme(gbTrueStar, "Violette");
            theme1.SetTheme(groupBox4, "Violette");
            theme1.SetTheme(groupBox4, "Violette");
            theme1.SetTheme(btnCheckUPOrder, "Violette");
            theme1.SetTheme(btnCheckUPPrn7Disease, "Violette");

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

            txtMedScanHN.Visible = false;
            rb1.Visible = false;
            btnMedScanSearch.Visible = false;

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

            setEvent();
            setTheme();
            tabMedScan.Hide();
            tabMedScan.Visible = false;
            pageLoad = false;
        }
        private void setTheme()
        {
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
            txtCheckUPBloodPressure.KeyUp += TxtCheckUPBloodPressure_KeyUp;
            txtCheckUPPulse.KeyUp += TxtCheckUPPulse_KeyUp;
            txtCheckUPBreath.KeyUp += TxtCheckUPBreath_KeyUp;
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
            btnPrnStaffNote.Click += BtnPrnStaffNote_Click;
            btnOperClose.Click += BtnOperClose_Click;
            btnMedScanSearch.Click += BtnMedScanSearch_Click;

            txtApmDtr.KeyUp += TxtApmDtr_KeyUp;
            btnApmSave.Click += BtnApmSave_Click;
            txtPttApmDate.DropDownClosed += TxtPttApmDate_DropDownClosed;
            txtApmTime.KeyUp += TxtApmTime_KeyUp;
            cboApmDept.DropDownClosed += CboApmDept_DropDownClosed;
            txtApmDsc.KeyUp += TxtApmDsc_KeyUp;
            txtApmTel.KeyUp += TxtApmTel_KeyUp;
            txtApmRemark.KeyUp += TxtApmRemark_KeyUp;
            btnApmNew.Click += BtnApmNew_Click;
            btnApmPrint.Click += BtnApmPrint_Click;

            chkItemLab.Click += ChkItemLab_Click;
            chkItemXray.Click += ChkItemXray_Click;
            chkItemPhy.Click += ChkItemPhy_Click;
            chkItemHotC.Click += ChkItemHotC_Click;

            txtItemCode.KeyUp += TxtItemCode_KeyUp;
            txtSearchItem.KeyUp += TxtSearchItem_KeyUp;
            btnItemAdd.Click += BtnItemAdd_Click;
            txtSearchItem.Enter += TxtSearchItem_Enter;

            txtMedScanHN.KeyUp += TxtMedScanHN_KeyUp;
        }

        private void BtnMedScanSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            HNmedscan = txtMedScanHN.Text.Trim();
            Patient ptt = bc.bcDB.pttDB.selectPatinetByHn(txtMedScanHN.Text);
            rb1.Text = ptt.Name;
            if (tabMedScanActiveNOtabOutLabActive)
            {
                tC1.SelectedTab = tabMedScan;
                setGrfVsIPD();
            }
            else
            {
                tC1.SelectedTab = tabOutlab;
                setGrfOutLab();
            }
        }

        private void TxtMedScanHN_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                
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
            String re = "";
            re = bc.bcDB.vsDB.updateStatusCloseVisit(HN, PRENO, VSDATE);
        }

        private void TimeOperList_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfOperList();
        }

        private void BtnApmPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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
            pdStaffNote.PrinterSettings.PrinterName = bc.iniC.printerStaffNote;
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
            Size textSize = TextRenderer.MeasureText("", fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
            StringFormat sfR2L = new StringFormat();
            float centerpage = e.PageSettings.PaperSize.Width / 2;
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText(bc.iniC.hostname, famt5, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(bc.iniC.hostname, famt5, Brushes.Black, centerpage - (textSize.Width/2), yPos, flags);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText(bc.iniC.hostnamee, famt5, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(bc.iniC.hostnamee, famt5, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText("ใบนัดพบแพทย์ Appointment Note", famt5, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString("ใบนัดพบแพทย์ Appointment Note", famt5, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            e.Graphics.DrawString("เลขที่:", famt5, Brushes.Black, col41+70, yPos, flags);
            e.Graphics.DrawString(APM.MNC_DOC_YR.Substring(APM.MNC_DOC_YR.Length-2) + "-"+APM.MNC_DOC_NO, famt5, Brushes.Black, col41+120, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("HN:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(HN, famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("แผนกที่นัด:", famt5, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(bc.bcDB.pm32DB.getDeptNameOPD(APM.MNC_SEC_NO), famt5, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Name/ชื่อผู้ป่วย:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(VS.PatientName, famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("วันที่พิมพ์:", famt5, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(DateTime.Now.ToString(), famt5, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Age/อายุ:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(PTT.AgeStringShort1(), famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("สิทธิ์การรักษา:", famt5, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(VS.PaidName, famt5, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Date/นัดมาวันที่:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(bc.datetoShow(APM.MNC_APP_DAT) +" "+APM.apm_time, famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("Dept/นัดตรวจที่แผนก:", famt5, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(bc.bcDB.pm32DB.getDeptNameOPD(APM.MNC_SECR_NO), famt5, Brushes.Black, col41+60, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Doctor/แพทย์:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(APM.doctor_name, famt5, Brushes.Black, col21, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("สิ่งที่ต้องเตรียม:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(APM.MNC_REM_MEMO, famt5, Brushes.Black, col21, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("เพื่อ:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(APM.MNC_APP_DSC, famt5, Brushes.Black, col21, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            //yPos = yPos + line;//ขึ้นบันทัดใหม่
            //yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("เบอร์โทรติดต่อ:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString("02-1381155-60", famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("ผู้บันทึก:", famt5, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString("---", famt5, Brushes.Black, col41, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("หมายเหตุ:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString("เพื่อประโยชน์และความสะดวกของท่าน  กรุณามาให้ตรงตามวัน และเวลาที่แพทย์นัดทุกครั้ง", famt1, Brushes.Black, col21, yPos, flags);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("กรณีที่ไม่สามารถมาตรวจตามนัดได้  กรุณาโทรเพื่อแจ้งยกเลิกหรือเลื่อนนัดกับทางโรงพยาบาลทุกครั้ง", famt1, Brushes.Black, col21, yPos, flags);
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
                txtApmTel.Focus();
            }
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
            if(grfOrder==null) { return; }
            if(grfOrder.Row==null) { return; }
            Row rowitem = grfOrder.Rows.Add();
            rowitem[colGrfOrderCode] = txtItemCode.Text.Trim();
            rowitem[colGrfOrderName] = lbItemName.Text;
            rowitem[colGrfOrderItemStatus] = chkItemLab.Checked ? "lab" : chkItemXray.Checked ? "xray" : chkItemHotC.Checked ? "hotc" : chkItemPhy.Checked ? "phy" : "";
            txtSearchItem.Value = "";
            txtSearchItem.Focus();
            //grfOrder.Rows.Add(rowitem);
        }
        private void TxtSearchItem_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                if (txtSearchItem.Text.Trim().Length <=0) return;
                setOrderItem();
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

        private void ChkItemHotC_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoHotC;
            clearControlOrder();
        }

        private void ChkItemPhy_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoPhy;
            clearControlOrder();
        }
        private void ChkItemXray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoXray;
            clearControlOrder();
        }
        private void ChkItemLab_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoLab;
            clearControlOrder();
        }
        private void BtnApmSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            PatientT07 apm = getApm();
            if(apm != null)
            {
                string re = bc.bcDB.pt07DB.insertPatientT07(apm);
                if (long.TryParse(re, out long chk))
                {
                    lfSbStatus.Text = txtApmNO.Text.Length>0 ? "update appointment OK" : "insert appointment OK";
                    setGrfPttApm();
                    if (txtApmNO.Text.Length <= 0)
                    {
                        txtApmNO.Value = re;//insert ถ้าแก้ไข  นัด ไม่ต้องใส่ค่า
                    }
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
                lbApmDtrName.Text = bc.selectDoctorName(txtApmDtr.Text.Trim());
            }
        }

        private void BtnPrnStaffNote_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printStaffNote();
        }

        private void BtnScanSaveImg_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            saveImgStaffNote();
        }
        private void printStaffNote()
        {
            PrintDocument documentStaffNote = new PrintDocument();
            documentStaffNote.PrinterSettings.PrinterName = bc.iniC.printerStaffNote;
            documentStaffNote.DefaultPageSettings.Landscape = true;
            documentStaffNote.PrintPage += Document_PrintPageStaffNote;

            documentStaffNote.Print();
        }
        private void Document_PrintPageStaffNote(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "", prndob = "";
            Decimal amt1 = 0, voucamt = 0, discount = 0, total = 0, cash = 0;
            float yPos = 10, gap = 6, colName = 0, col2 = 5, col3 = 250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 820, col40 = 620;
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
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            
            col2 = 65;
            col3 = 300;
            col4 = 870;
            col40 = 650;
            yPos = 15;
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());
            
            line = "5";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
            yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
            //e.Graphics.DrawString(line, fPrn, Brushes.Black, xOffset, yPos, new StringFormat());leftMargin
            e.Graphics.DrawString(line, famt7B, Brushes.Black, col3, yPos, flags);
            e.Graphics.DrawString(line, famt7B, Brushes.Black, col4, yPos, flags);
            line = "H.N. " + PTT.MNC_HN_NO + "     " + VS.VN;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 + 25, yPos + 5, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4 + 30, yPos + 5, flags);

            line = "ชื่อ " + PTT.Name;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3 + 20, yPos + 20, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4 + 10, yPos + 20, flags);
            line = "เลขที่บัตร " + PTT.MNC_ID_NO;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 40, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos + 40, flags);
            line = VS.PaidName;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 40, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 40, flags);

            line = VS.CompName;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 40, flags);

            line = "โรคประจำตัว        ไม่มี";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 60, flags);
            rec = new Rectangle(col2int + 75, 72, recx, recy);
            e.Graphics.DrawRectangle(blackPen, rec);

            line = prndob;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 60, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos + 60, flags);
            //line = lbPaidName.Text.Trim();
            //textSize = TextRenderer.MeasureText(line, fEdit, proposedSize, TextFormatFlags.RightToLeft);

            line = "มีโรค ระบุ";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 70, yPos + 80, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 67 - recx, 92, recx, recy));

            line = "วันที่เวลา " + date;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 80, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4, yPos + 80, flags);

            line = "โรคเรื้อรัง";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 100, flags);
            line = "ชื่อแพทย์ " + VS.DoctorId + " " + VS.DoctorName;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 100, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col4 - 50, yPos + 120, flags);

            line = "DR Time.                               ปิดใบยา";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col3, yPos + 120, flags);

            line = "อาการเบื้องต้น " + VS.symptom.Replace("\r\n","");
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 120, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 100, flags);

            line = "Temp" + VS.temp;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2, yPos + 140, flags);

            line = "H.Rate" + VS.ratios;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 80, yPos + 140, flags);
            line = "R.Rate" + VS.breath;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 160, yPos + 140, flags);
            line = "BP1" + VS.bp1l;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 240, yPos + 140, flags);
            line = "Time :";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 300, yPos + 140, flags);
            line = "BP2 " + VS.bp1r;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 380, yPos + 140, flags);
            line = "Time :";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 440, yPos + 140, flags);

            line = "Wt." + VS.weight;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2, yPos + 160, flags);
            line = "Ht." + VS.high;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 80, yPos + 160, flags);
            line = "BMI.";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 100, yPos + 160, flags);
            line = "CC." + VS.cc;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 180, yPos + 160, flags);
            line = "CC.IN" + VS.ccin;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 240, yPos + 160, flags);
            line = "CC.EX" + VS.ccex;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 300, yPos + 160, flags);
            line = "Ab.C" + VS.abc;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 400, yPos + 160, flags);
            line = "H.C." + VS.hc;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2 + 460, yPos + 160, flags);

            line = "Precaution (Med) _________________________________________ ";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 10, yPos + 220, flags);

            line = "แพ้ยา/อาหาร/อื่นๆ         ไม่มี";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 180, flags);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40, yPos + 180, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 123 - recx, yPosint + 180, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 120 - recx, yPosint + 180, recx, recy));
            line = "มี ระบุอาการ";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 20, yPos + 200, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 20 - recx, yPosint + 200, recx, recy));
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 15, yPos + 200, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 15 - recx, yPosint + 200, recx, recy));

            //line = "อาการเบื้อต้น  "+ txtSymptom.Text;
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 10, yPos + 220, flags);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 10, yPos + 220, flags);

            line = bc.bcDB.pm32DB.getDeptNameOPD(VS.DeptCode);
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 40, yPos + 260, flags);

            line = "Medication                       No Medication";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col40 + 50, yPos + 280, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 30 - recx - 5, yPosint + 280, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 120 - recx + 60, yPosint + 280, recx, recy));

            line = "อาการ" + VS.symptom;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col3 + 40, yPos + 315, flags);

            line = "อาการ"+ VS.symptom;
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 360, flags);

            //line = "สัมผัสผู้ป่วย ชื่อ";
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 430, flags);

            //line = "สัมผัสล่าสุด";
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 475, flags);

            //line = "คำแนะนำ       การออกกำลังกาย               การรับประทานอาหารที่ถูกสัดส่วน";
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 620, flags);
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 75 - recx, yPosint + 620, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 210 - recx, yPosint + 620, recx, recy));

            //line = "การตรวจสุขภาพประจำปี          การพบแพทย์เฉพาะทาง       อื่นๆ";
            //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.RightToLeft);
            //e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos + 640, flags);
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 35 - recx, yPosint + 640, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 190 - recx, yPosint + 640, recx, recy));
            //e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 3350 - recx, yPosint + 640, recx, recy));

            line = "ใบรับรองแพทย์             ไม่มี      มี             Consult      ไม่มี      มี __________________";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos + 660, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 170 - recx, yPosint + 660, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 215 - recx, yPosint + 660, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 345 - recx, yPosint + 660, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 385 - recx, yPosint + 660, recx, recy));

            line = "ชื่อผู้รับ _____________________________";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 680, flags);

            line = "Health Education :";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 730, flags);

            line = "ลงชื่อพยาบาล: _____________________________________";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEdit, Brushes.Black, col2, yPos + 750, flags);

            line = "FM-REC-002 (00 10/09/53)(1/1)";
            textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col2, yPos + 770, flags);
            e.Graphics.DrawString(line, fEditS, Brushes.Black, col40, yPos + 770, flags);
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
                picL.Image = Image.FromFile(file + preno1 + "R.JPG");
                picR.Image = Image.FromFile(file + preno1 + "S.JPG");
            }
            catch (Exception ex)
            {
                lfSbStatus.Text = ex.Message.ToString();
                lfSbMessage.Text = err + " getImgStaffNote " + ex.Message;
                new LogWriter("e", "FrmOPD getImgStaffNote " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "getImgStaffNote", ex.Message);
            }
        }
        private void saveImgStaffNote()
        {
            if (VSDATE.Length <= 0) return;
            if (PRENO.Length <= 0) return;
            if (HN.Length <= 0) return;
            String file = "", dd = "", mm = "", yy = "", err = "", preno1 = "";
            try
            {
                err = "00";

                String filenameS = "";
                filenameS = "000000" + PRENO;
                filenameS = filenameS.Substring(filenameS.Length - 6);

                String filenameR = "", path = bc.iniC.pathScanStaffNote, year ="", mon = "", day = "";
                year = VSDATE.Substring(0, 4);
                mon = VSDATE.Substring(5, 4);
                day = VSDATE.Substring(8, 4);
                path += year + "\\" + mon + "\\" + day + "\\";

                filenameR = "000000" + PRENO;
                filenameR = filenameR.Substring(filenameR.Length - 6);
                err = "07";
                //new LogWriter("e", "genImgStaffNote path filenameS " + path + filenameS);
                Image bitmap = picL.Image;
                bitmap.Save("\\\\"+path + filenameS + "S.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                //imgL.Save(filenameS + "R.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                err = "08";
                bitmap = picR.Image;
                //new LogWriter("e", "genImgStaffNote path filenameS " + path + filenameR);
                bitmap.Save(path + filenameR + "R.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                //imgR.Save(filenameR + "S.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                //lfSbStatus.Text = ex.Message.ToString();
                lfSbMessage.Text = err + " saveImgStaffNote " + ex.Message;
                new LogWriter("e", "FrmOPD saveImgStaffNote " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "saveImgStaffNote", ex.Message);
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
                txtOperBp2L.Focus();
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
                txtOperAbo.Focus();
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
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void BtnOperSaveVital_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Visit vs = new Visit();
            vs = setVisitVitalsign();
            vs.preno = PRENO;
            vs.VisitDate = VSDATE;
            vs.HN = HN;
            String re = bc.bcDB.vsDB.updateVitalSign(vs);
            if (long.TryParse(re, out long chk))
            {
                lfSbMessage.Text = "update vitalsign OK";
            }
        }
        private void BtnOperSaveDtr_Click(object sender, EventArgs e)     
        {
            //throw new NotImplementedException();
            String re = bc.bcDB.vsDB.updateDoctor(HN, PRENO, VSDATE, txtOperDtr.Text.Trim());
            String re1 = bc.bcDB.sumt03DB.insertSummaryT03(txtOperDtr.Text.Trim());
            String re2 = bc.bcDB.vsDB.updateActNoSendToDoctor110(HN, PRENO, VSDATE);
            if (long.TryParse(re, out long chk))
            {
                lfSbMessage.Text = "update Doctor OK";
                setGrfOperList();
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
                txtCheckUPBreath.Focus();
            }
        }

        private void TxtCheckUPBloodPressure_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPPulse.Focus();
            }
        }

        private void TxtCheckUPHeight_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPBloodPressure.Focus();
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
            if (tC1.SelectedTab == tabCheckUP)
            {
                setGrfCheckUPList();
            }
            else if (tC1.SelectedTab == tabMedScan)
            {
                tabMedScanActiveNOtabOutLabActive = true;
                txtMedScanHN.Visible = true;
                rb1.Visible = true;
                btnMedScanSearch.Visible = true;
            }
            else if (tC1.SelectedTab == tabOutlab)
            {
                tabMedScanActiveNOtabOutLabActive = false;
                txtMedScanHN.Visible = true;
                rb1.Visible = true;
                btnMedScanSearch.Visible = true;
            }
            else
            {
                txtMedScanHN.Visible = false;
                rb1.Visible = false;
                btnMedScanSearch.Visible = false;
            }
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

            setGrfScan();
        }
        private void setGrfVsIPD()
        {
            //grfIPD.Clear();
            grfIPD.Rows.Count = 1;
            grfIPD.Cols.Count = 11;

            grfIPD.Cols[colIPDDate].Width = 72;
            grfIPD.Cols[colIPDVn].Width = 80;
            grfIPD.Cols[colIPDDept].Width = 170;
            grfIPD.Cols[colIPDPreno].Width = 100;
            grfIPD.Cols[colIPDStatus].Width = 60;
            grfIPD.Cols[colIPDDtrName].Width = 180;
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
            grfIPD.Cols[colIPDDtrName].AllowEditing = false;

            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dt = bc.bcDB.vsDB.selectVisitIPDByHn5(txtMedScanHN.Text);
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
            grfIPD.Cols[colIPDAnShow].Visible = false;
            grfIPD.Cols[colIPDPreno].Visible = false;
            grfIPD.Cols[colIPDVn].Visible = false;
            grfIPD.Cols[colIPDStatus].Visible = false;
            grfIPD.Cols[colIPDAnYr].Visible = false;
            grfIPD.Cols[colIPDAn].Visible = false;
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
            label2.Text = "AN :";
            an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";
            
            vsDate = bc.datetoDB(vsDate);
            //setStaffNote(vsDate, preno);
            dt = bc.bcDB.dscDB.selectByAn(txtMedScanHN.Text, an);
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
        private void setGrfHisOrder(String vsDate, String preno)
        {
            DataTable dtOrder = new DataTable();
            dtOrder = bc.bcDB.vsDB.selectDrugOPD(txtOperHN.Text.Trim(), preno, vsDate);
            grfHisOrder.Rows.Count = 1;
            int i = 0;
            decimal aaa = 0;
            foreach (DataRow row1 in dtOrder.Rows)
            {
                i++;
                Row rowa = grfHisOrder.Rows.Add();
                rowa[colOrderName] = row1["MNC_PH_TN"].ToString();
                rowa[colOrderMed] = "";
                rowa[colOrderQty] = row1["qty"].ToString();
                rowa[colOrderDate] = bc.datetoShow(row1["mnc_req_dat"]);
                rowa[colOrderFre] = row1["MNC_PH_DIR_DSC"].ToString();
                rowa[colOrderIn1] = row1["MNC_PH_CAU_dsc"].ToString();
                //row1[0] = (i - 2);
            }
        }
        private void initGrfLab()
        {
            grfLab = new C1FlexGrid();
            grfLab.Font = fEdit;
            grfLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLab.Location = new System.Drawing.Point(0, 0);
            grfLab.Rows.Count = 1;
            grfLab.Cols.Count = 6;

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

            grfLab.Cols[colLabName].AllowEditing = false;
            grfLab.Cols[colInterpret].AllowEditing = false;
            grfLab.Cols[colNormal].AllowEditing = false;

            pnHistoryLab.Controls.Add(grfLab);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfOutLab, bc.iniC.themegrfOpd);
        }
        private void setGrfLab(String vsDate, String preno)
        {
            DataTable dt = new DataTable();
            DateTime dtt = new DateTime();
            
            if (vsDate.Length <= 0)
            {
                return;
            }
            dt = bc.bcDB.vsDB.selectLabResultbyVN(txtOperHN.Text.Trim(), preno, vsDate);

            grfLab.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfLab.Rows.Count = dt.Rows.Count + 1;
            
            //grfLab.Cols[colLabResult].Width = 200;
            int i = 0;
            decimal aaa = 0;
            //pB1.Maximum = dt.Rows.Count;
            try
            {
                String labname = "", labnameold = "", reqno = "", reqnoold = "";
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
            catch (Exception ex)
            {
                new LogWriter("e", "FrmScanView1 setGrfLab grfLab " + ex.Message);
            }

        }
        private void initGrfOutLab()
        {
            grfOutLab = new C1FlexGrid();
            grfOutLab.Font = fEdit;
            grfOutLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOutLab.Location = new System.Drawing.Point(0, 0);
            grfOutLab.Rows.Count = 1;
            grfOutLab.Cols.Count = 6;

            grfOutLab.Cols[colgrfOutLabDscHN].Width = 80;
            grfOutLab.Cols[colgrfOutLabDscName].Width = 250;
            grfOutLab.Cols[colgrfOutLabDscVsDate].Width = 100;
            grfOutLab.Cols[colgrfOutLabDscVN].Width = 80;

            grfOutLab.Cols[colgrfOutLabDscHN].Caption = "HN";
            grfOutLab.Cols[colgrfOutLabDscName].Caption = "Name";
            grfOutLab.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
            grfOutLab.Cols[colgrfOutLabDscVN].Caption = "VN";

            grfOutLab.Cols[colgrfOutLabDscId].Visible = false;
            grfOutLab.Cols[colgrfOutLabDscHN].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLabDscName].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLabDscVN].AllowEditing = false;
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
                new LogWriter("e", "FrmOPD BtnApmSave_Click " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnApmSave_Click save  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            
        }
        private void setGrfOutLab()
        {
            grfOutLab.Rows.Count = 1;
            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            // query นี้ ไปดึงข้อมูล จาก patient_t01_2 ซึ่งมีข้อมูลเยอะมาก  ตัดออกได้ไหม เพราะเป็น IPD       แก้แล้ว
            dt = bc.bcDB.dscDB.selectOutLabByHn(txtMedScanHN.Text.Trim());
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1, row = grfOutLab.Rows.Count;

            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfOutLab.Rows.Add();
                String status = "", vn = "";
                rowa[colgrfOutLabDscHN] = row1["hn"].ToString();
                rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
                rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["date_req"].ToString());
                rowa[colgrfOutLabDscName] = row1["patient_fullname"].ToString();
                rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
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
            String vsdate = "", preno = "";
            try
            {
                preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                vsdate = grfOPD[grfOPD.Row, colVsVsDate1] != null ? grfOPD[grfOPD.Row, colVsVsDate1].ToString() : "";
                setStaffNote(vsdate, preno);
                setGrfLab(vsdate, preno);
                setGrfHisOrder(vsdate, preno);
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
        private void setStaffNote(String vsDate, String preno)
        {
            String file = "", dd = "", mm = "", yy = "", err = "";
            picHisL.Image = null;
            picHisL.Image = null;
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
                    picHisL.Image = Image.FromFile(file + preno1 + "R.JPG");
                    picHisR.Image = Image.FromFile(file + preno1 + "S.JPG");
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
        private void setGrfOPD()
        {
            grfOPD.Rows.Count = 1;
            
            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            // query นี้ ไปดึงข้อมูล จาก patient_t01_2 ซึ่งมีข้อมูลเยอะมาก  ตัดออกได้ไหม เพราะเป็น IPD       แก้แล้ว

            dt = bc.bcDB.vsDB.selectVisitByHn6(txtOperHN.Text, "O");
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
                //rowa[colVsHigh] = row1["MNC_HIGH"].ToString();
                //rowa[colVsWeight] = row1["MNC_WEIGHT"].ToString();
                //rowa[colVscc] = row1["MNC_CC"].ToString();
                //rowa[colVsccex] = row1["MNC_CC_EX"].ToString();
                //rowa[colVsccin] = row1["MNC_CC_IN"].ToString();
                //rowa[colVsabc] = row1["MNC_ABC"].ToString();
                //rowa[colVshc16] = row1["MNC_HC"].ToString();
                //rowa[colVsbp1l] = row1["MNC_BP1_L"].ToString();
                //rowa[colVsbp1r] = row1["MNC_BP1_R"].ToString();
                //rowa[colVsbp2l] = row1["MNC_BP2_L"].ToString();
                //rowa[colVsbp2r] = row1["MNC_BP2_R"].ToString();
                //rowa[colVsTemp] = row1["MNC_TEMP"].ToString();
                //rowa[colVsVital] = row1["MNC_BREATH"].ToString();
                //rowa[colVsPres] = row1["MNC_CIR_HEAD"].ToString();
                rowa[colVsVsDate1] = row1["mnc_date"].ToString();
                rowa[colVsDtrName] = row1["dtr_name"].ToString();
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
            setGrfScan();
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
        private void initGrfOrder()
        {
            grfOrder = new C1FlexGrid();
            grfOrder.Font = fEdit;
            grfOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOrder.Location = new System.Drawing.Point(0, 0);
            grfOrder.Rows.Count = 1;
            grfOrder.Cols.Count = 9;
            grfOrder.Cols[colGrfOrderCode].Width = 100;
            grfOrder.Cols[colGrfOrderName].Width = 200;
            
            grfOrder.ShowCursor = true;
            grfOrder.Cols[colGrfOrderCode].Caption = "HN";
            grfOrder.Cols[colGrfOrderName].Caption = "full name";
            
            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grfOrder.Cols[colGrfOrderCode].DataType = typeof(String);
            grfOrder.Cols[colGrfOrderName].DataType = typeof(String);

            grfOrder.Cols[colGrfOrderCode].TextAlign = TextAlignEnum.CenterCenter;
            grfOrder.Cols[colGrfOrderName].TextAlign = TextAlignEnum.LeftCenter;

            grfOrder.Cols[colGrfOrderCode].Visible = true;
            grfOrder.Cols[colGrfOrderName].Visible = true;

            grfOrder.Cols[colGrfOrderCode].AllowEditing = false;
            grfOrder.Cols[colGrfOrderName].AllowEditing = false;

            grfOrder.RowColChange += GrfOrder_RowColChange;
            //grfCheckUPList.AllowFiltering = true;

            pnOrder.Controls.Add(grfOrder);
            theme1.SetTheme(grfOrder, bc.iniC.themeApp);
        }

        private void GrfOrder_RowColChange(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void initGrfPttApm()
        {
            grfPttApm = new C1FlexGrid();
            grfPttApm.Font = fEdit;
            grfPttApm.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPttApm.Location = new System.Drawing.Point(0, 0);
            grfPttApm.Rows.Count = 1;
            grfPttApm.Cols.Count = 18;

            grfPttApm.Cols[colgrfPttApmVsDate].Width = 100;
            grfPttApm.Cols[colgrfPttApmApmDate].Width = 100;
            grfPttApm.Cols[colgrfPttApmApmTime].Width = 60;
            grfPttApm.Cols[colgrfPttApmNote].Width = 500;
            grfPttApm.Cols[colgrfPttApmOrder].Width = 500;
            grfPttApm.Cols[colgrfPttApmHN].Width = 80;
            grfPttApm.Cols[colgrfPttApmPttName].Width = 250;
            grfPttApm.Cols[colgrfPttApmDeptR].Width = 120;
            grfPttApm.Cols[colgrfPttApmDeptMake].Width = 150;

            grfPttApm.ShowCursor = true;
            grfPttApm.Cols[colgrfPttApmVsDate].Caption = "date";
            grfPttApm.Cols[colgrfPttApmApmDate].Caption = "นัดวันที่";
            grfPttApm.Cols[colgrfPttApmApmTime].Caption = "นัดเวลา";
            grfPttApm.Cols[colgrfPttApmDeptR].Caption = "นัดตรวจที่แผนก";
            grfPttApm.Cols[colgrfPttApmDeptMake].Caption = "แผนกทำนัด";
            grfPttApm.Cols[colgrfPttApmNote].Caption = "รายละเอียด";
            grfPttApm.Cols[colgrfPttApmOrder].Caption = "Order";

            grfPttApm.Cols[colgrfPttApmApmDate].DataType = typeof(String);
            grfPttApm.Cols[colgrfPttApmApmTime].DataType = typeof(String);
            grfPttApm.Cols[colgrfPttApmDeptR].DataType = typeof(String);
            grfPttApm.Cols[colgrfPttApmNote].DataType = typeof(String);
            grfPttApm.Cols[colgrfPttApmOrder].DataType = typeof(String);
            grfPttApm.Cols[colgrfPttApmHN].DataType = typeof(String);
            grfPttApm.Cols[colgrfPttApmPttName].DataType = typeof(String);
            grfPttApm.Cols[colgrfPttApmDeptMake].DataType = typeof(String);

            grfPttApm.Cols[colgrfPttApmApmDate].TextAlign = TextAlignEnum.CenterCenter;
            grfPttApm.Cols[colgrfPttApmApmTime].TextAlign = TextAlignEnum.CenterCenter;
            grfPttApm.Cols[colgrfPttApmDeptR].TextAlign = TextAlignEnum.CenterCenter;
            grfPttApm.Cols[colgrfPttApmDeptMake].TextAlign = TextAlignEnum.CenterCenter;
            grfPttApm.Cols[colgrfPttApmNote].TextAlign = TextAlignEnum.LeftCenter;
            grfPttApm.Cols[colgrfPttApmOrder].TextAlign = TextAlignEnum.LeftCenter;

            grfPttApm.Cols[colgrfPttApmVsDate].Visible = true;
            grfPttApm.Cols[colgrfPttApmApmDate].Visible = true;
            grfPttApm.Cols[colgrfPttApmDeptR].Visible = true;
            grfPttApm.Cols[colgrfPttApmNote].Visible = true;
            grfPttApm.Cols[colgrfPttApmDocNo].Visible = false;
            grfPttApm.Cols[colgrfPttApmDocYear].Visible = false;
            grfPttApm.Cols[colgrfPttApmVsDate].Visible = false;
            grfPttApm.Cols[colgrfPttApmHN].Visible = false;
            grfPttApm.Cols[colgrfPttApmPttName].Visible = false;

            grfPttApm.Cols[colgrfPttApmVsDate].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmApmDate].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmDeptR].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmNote].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmApmTime].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmOrder].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmDeptMake].AllowEditing = false;
            grfPttApm.DoubleClick += GrfPttApm_DoubleClick;

            pnPttApm.Controls.Add(grfPttApm);
            theme1.SetTheme(grfPttApm, bc.iniC.themeApp);
        }

        private void GrfPttApm_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfPttApm.Row <= 0) return;
            if (grfPttApm.Col <= 0) return;
            String apmno = "", apmyear = "";
            apmno = grfPttApm[grfPttApm.Row, colgrfPttApmDocNo].ToString();
            apmyear = grfPttApm[grfPttApm.Row, colgrfPttApmDocYear].ToString();
            setControlApm(apmyear, apmno);
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
            txtApmTel.Value = apm.MNC_APP_TEL;
            txtApmRemark.Value = apm.MNC_DOT_CD;
            lbApmDtrName.Text = bc.selectDoctorName(apm.MNC_DOT_CD);
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
                rowa[colgrfPttApmApmDate] = bc.datetoShow1(row1["MNC_APP_DAT"].ToString());
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
        }
        private void initGrfOperList()
        {
            grfOperList = new C1FlexGrid();
            grfOperList.Font = fEdit;
            grfOperList.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOperList.Location = new System.Drawing.Point(0, 0);
            grfOperList.Rows.Count = 1;
            grfOperList.Cols.Count = 12;
            grfOperList.Cols[colgrfOperListHn].Width = 100;
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
            grfOperList.Cols[colgrfOperListHn].Visible = false;

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
            grfOperList.Click += GrfOperList_Click;
            grfOperList.RowColChange += GrfOperList_RowColChange;
            //grfCheckUPList.AllowFiltering = true;

            spOperList.Controls.Add(grfOperList);
            theme1.SetTheme(grfOperList, bc.iniC.themeApp);
        }
        private void setControlOper()
        {
            if (pageLoad) return;
            showLbLoading();
            lfSbMessage.Text = grfOperList.Row.ToString();
            picHisL.Image = null;
            picHisR.Image = null;
            if (grfOperList.Row <= 0) return;
            if (grfOperList.Col <= 0) return;
            if(grfOperList[grfOperList.Row, colgrfOperListPreno]==null) return;

            if (grfOperList.Row == rowGrfOper) return;
            lfSbStatus.Text = "";
            lfSbMessage.Text = "";
            rowGrfOper = grfOperList.Row;
            PRENO = grfOperList[grfOperList.Row, colgrfOperListPreno].ToString();
            VSDATE = grfOperList[grfOperList.Row, colgrfOperListVsDate].ToString();
            HN = grfOperList[grfOperList.Row, colgrfOperListHn].ToString();
            VS = bc.bcDB.vsDB.selectbyPreno(HN, VSDATE, PRENO);
            PTT = bc.bcDB.pttDB.selectPatinetByHn(HN);
            setControlTabOperVital(VS);
            getImgStaffNote();
            clearControlOrder();
            setGrfPttApm();
            setGrfOPD();
            setGrfOutLab();
            if (grfOrder != null) grfOrder.Rows.Count = 1;
            chkItemLab.Checked = true;
            HNmedscan = HN;
            rb1.Text = VS.PatientName;
            txtMedScanHN.Text = HNmedscan;
            //txtOperTemp.Focus();
            hideLbLoading();
        }
        private void GrfOperList_RowColChange(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlOper();
        }

        private void GrfOperList_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //setControlOper();
        }
        private void setGrfOperList()
        {
            if (pageLoad) return;
            pageLoad = true;
            timeOperList.Enabled = false;
            DataTable dtvs = new DataTable();
            String deptno = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
            String vsdate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd");
            dtvs = bc.bcDB.vsDB.selectPttinDept(deptno,bc.iniC.station,vsdate, vsdate);

            grfOperList.Rows.Count = 1;
            grfOperList.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1, row = grfOperList.Rows.Count;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfOperList.Rows[i];
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
        private void setGrfCheckUPList()
        {
            showLbLoading();
            DateTime datestar = DateTime.Now;
            DateTime dateend = DateTime.Now;
            if (datestar.Year < 1900)
            {
                datestar = datestar.AddYears(543);
            }
            if (dateend.Year < 1900)
            {
                dateend = dateend.AddYears(543);
            }
            DataTable dtcheckup = new DataTable();
            dtcheckup = bc.bcDB.vsDB.selectPttinDeptOrderByNameT("101", "147", datestar.Year.ToString() + "-" + datestar.ToString("MM-dd"), dateend.Year.ToString() + "-" + dateend.ToString("MM-dd"));

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

            setControlCheckUP(grfCheckUPList[grfCheckUPList.Row, colgrfCheckUPHn].ToString(), grfCheckUPList[grfCheckUPList.Row, colgrfCheckUPVsDate].ToString(), grfCheckUPList[grfCheckUPList.Row, colgrfCheckUPPreno].ToString());
        }
        private Visit setVisitVitalsign()
        {
            Visit vs = new Visit();
            vs.temp = txtOperTemp.Text;
            vs.ratios = txtOperHrate.Text;
            vs.breath = txtOperRrate.Text;
            //vs.abc = txtOperAbo.Text;
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
            
            return vs;
        }
        private void setControlCheckUP(String hn, String vsdate, String preno)
        {
            PTT = bc.bcDB.pttDB.selectPatinetByHn(hn);
            if(PTT==null) return;
            Visit visit = new Visit();
            DateTime.TryParse(PTT.MNC_BDAY, out DateTime dob);
            if (dob.Year < 1900)
            {
                dob = dob.AddYears(543);
            }
            txtCheckUPHN.Value = PTT.MNC_HN_NO;
            txtCheckUPHN.Value = PTT.MNC_HN_NO;
            txtCheckUPNameT.Value = PTT.MNC_HN_NO;
            txtCheckUPSurNameT.Value = PTT.MNC_HN_NO;
            txtCheckUPNameE.Value = PTT.MNC_HN_NO;
            txtCheckUPSurNameE.Value = PTT.MNC_HN_NO;
            txtCheckUPMobile1.Value = PTT.MNC_HN_NO;
            txtCheckUPAge.Value = PTT.AgeStringOK1DOT();
            txtCheckUPDOB.Value = dob;

            bc.setC1Combo(cboCheckUPPrefixT, PTT.MNC_PFIX_CDT);
            bc.setC1Combo(cboCheckUPSex, PTT.MNC_SEX);
            bc.setC1Combo(cboCheckUPNat, PTT.MNC_NAT_CD);

        }
        private void setControlTabOperVital(Visit vs)
        {
            txtOperHN.Value = vs.HN;
            lbOperPttNameT.Text = vs.PatientName;
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
        }
        private PatientT07 getApm()
        {
            if (VSDATE.Length <= 0) return null;
            if (PRENO.Length <= 0) return null;
            if (HN.Length <= 0) return null;
            if (VS == null) return null;
            
            DateTime.TryParse(txtPttApmDate.Text, out DateTime apmdate);
            if (apmdate == null) return null;
            PatientT07 apm = new PatientT07();
            apm.MNC_HN_NO = txtOperHN.Text.Trim();
            apm.MNC_HN_YR = VS.MNC_HN_YR;
            apm.MNC_DATE = VSDATE;
            apm.MNC_PRE_NO = PRENO;
            apm.MNC_DOC_YR = txtApmDocYear.Text.Trim();
            apm.MNC_DOC_NO = txtApmNO.Text.Trim();
            apm.MNC_TIME = VS.VisitTime;
            apm.MNC_APP_DAT = apmdate.Year+"-"+ apmdate.ToString("MM-dd");
            apm.MNC_APP_TIM = bc.bcDB.pt07DB.setAppTime(txtApmTime.Text);
            apm.MNC_APP_DSC = txtApmDsc.Text.Trim();
            apm.MNC_APP_STS = "";
            apm.MNC_APP_TEL = txtApmTel.Text;
            apm.MNC_DOT_CD = txtApmDtr.Text.Trim();
            apm.apm_time = txtApmTime.Text;
            apm.MNC_SEC_NO = bc.iniC.station;
            apm.MNC_DEP_NO = DEPTNO;
            apm.MNC_SECR_NO = cboApmDept.SelectedItem == null ? "" : ((ComboBoxItem)cboApmDept.SelectedItem).Value;
            apm.MNC_DEPR_NO = bc.bcDB.pm32DB.getDeptNoOPD(apm.MNC_SECR_NO);

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
            if(!new1)
            {
                grfPttApm.Rows.Count = 1;
            }
        }
        private void setOrderItem()
        {
            String[] txt = txtSearchItem.Text.Split('#');
            if (txt.Length < 1)
            {
                lfSbMessage.Text = "no item";
                txtItemCode.Value = "";
                lbItemName.Text = "";
                txtItemQTY.Text = "1";
                return;
            }
            String name = txt[0].Trim();
            String code = txt[1].Trim();
            if (chkItemLab.Checked)
            {
                LabM01 lab = new LabM01();
                lab = bc.bcDB.labM01DB.SelectByPk(code);
                txtItemCode.Value = lab.MNC_LB_CD;
                lbItemName.Text = lab.MNC_LB_DSC;
                txtItemQTY.Visible = false;
            }
            else if (chkItemXray.Checked)
            {
                XrayM01 xray = new XrayM01();
                xray = bc.bcDB.xrayM01DB.SelectByPk(code);
                txtItemCode.Value = xray.MNC_XR_CD;
                lbItemName.Text = xray.MNC_XR_DSC;
                txtItemQTY.Visible = false;
            }
            else if (chkItemHotC.Checked)
            {

            }
            else if (chkItemPhy.Checked)
            {

            }
        }
        private void FrmOPD_Load(object sender, EventArgs e)
        {
            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();

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

            txtApmTime.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtApmTime.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtApmTime.AutoCompleteCustomSource = acmApmTime;

            setGrfOperList();
            btnScanClearImg.Visible = false;
            btnScanGetImg.Visible = false;
            btnScanSaveImg.Visible = false;
            timeOperList.Enabled = true;
            timeOperList.Start();
            DEPTNO = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
            String stationname = bc.bcDB.pm32DB.getDeptName(bc.iniC.station);

            AutoCompleteStringCollection autoPaid = new AutoCompleteStringCollection();
            autoPaid = bc.bcDB.pm13DB.getlApm();
            txtApmDsc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtApmDsc.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtApmDsc.AutoCompleteCustomSource = autoPaid;
            
            autoLab = bc.bcDB.labM01DB.getlLabAll();
            autoXray = bc.bcDB.xrayM01DB.getlLabAll();

            lfSbStation.Text = DEPTNO+"[" +bc.iniC.station+"]"+ stationname;
            rgSbModule.Text = bc.iniC.hostDBMainHIS + " " + bc.iniC.nameDBMainHIS;
            this.Text = "Last Update 2024-01-26";
        }
    }
}
