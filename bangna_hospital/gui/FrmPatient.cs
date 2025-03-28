using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Command;
using C1.Win.C1Document;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using C1.Win.FlexViewer;
using GrapeCity.ActiveReports.Document.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Column = C1.Win.C1FlexGrid.Column;
using Row = C1.Win.C1FlexGrid.Row;

namespace bangna_hospital.gui
{
    public partial class FrmPatient : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1FlexGrid grfVS, grfLab, grfOrder, grfXray, grfProcedure, grfDrugAllergy, grfChronic, grfIPDScan, grfOutLab, grfPttApm, grfApmOrder, grfDrugSet;
        Patient PTT;
        Visit VS;
        PatientT07 APM;
        C1ThemeController theme1;
        C1FlexViewer fvOutlab, fvCerti;
        AutoCompleteStringCollection autoDrug, autoLab, autoXray, autoProcedure, acmApmTime, autoApm;
        Color colorLbDoc;
        Boolean pageLoad = false, flagTabMedScanChange=false;
        String PRENO = "", VSDATE = "", HN = "", DEPTNO = "", DTRCODE = "", DOCGRPID = "", TABVSACTIVE="";
        string[] AUTOSymptom = { "07:00-08:00","08:00-09:00","08:00-15:00","09:00-10:00","09:00-15:00","10:00-11:00","10:00-15:00","11:00-12:00","12:00-13:00","13:00-14:00","14:00-15:00","15:00-16:00","16:00-17:00","17:00-18:00"
                ,"18:00-19:00","19:00-20:00","20:00-21:00"
        };
        int colVsVsDate = 1, colVsDept = 2, colVsVnShow = 3, colVsStatus = 4, colVsPreno = 5, colVsAn = 6, colVsAndate = 7, colVsPaidType = 8, colVsHigh = 9, colVsWeight = 10, colVsTemp = 11, colVscc = 12, colVsccin = 13, colVsccex = 14, colVsabc = 15, colVshc16 = 16, colVsbp1r = 17, colVsbp1l = 18, colVsbp2r = 19, colVsbp2l = 20, colVsVital = 21, colVsPres = 22, colVsRadios = 23, colVsBreath = 24, colVsVsDate1 = 25, colVsDtrName = 26, colVsVn = 27, colVsDeptName=28, colVsVsStatus=29;
        int colDrugOrderId = 1, colDrugOrderDate = 2, colDrugName = 3, colDrugOrderQty = 4, colDrugOrderFre = 5, colDrugOrderIn1 = 6, colDrugOrderMed = 7;
        int colLabDate = 1, colLabName = 2, colLabNameSub = 3, colLabResult = 4, colInterpret = 5, colNormal = 6, colUnit = 7;
        int colXrayDate = 1, colXrayCode = 2, colXrayName = 3, colXrayResult = 4;
        int colProcCode = 1, colProcName = 2, colProcReqDate = 3, colProcReqTime = 4;
        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
        int colgrfOutLabDscHN = 1, colgrfOutLabDscPttName = 2, colgrfOutLabDscVsDate = 3, colgrfOutLabDscVN = 4, colgrfOutLabDscId = 5, colgrfOutLablabcode = 6, colgrfOutLablabname = 7, colgrfOutLabApmDate = 8, colgrfOutLabApmDesc = 9, colgrfOutLabApmDtr = 10, colgrfOutLabApmReqNo = 11, colgrfOutLabApmReqDate = 12;
        int colgrfOrderCode = 1, colgrfOrderName = 2, colgrfOrderStatus = 3, colgrfOrderQty = 4, colgrfOrderDrugFre = 5, colgrfOrderDrugPrecau = 6, colgrfOrderDrugIndica = 7, colgrfOrderDrugInterac = 8, colgrfOrderID = 9, colgrfOrderReqNO = 10, colgrfOrderReqDate=11, colgrfOrdFlagSave = 12;
        int colgrfPttApmVsDate = 1, colgrfPttApmApmDateShow = 2, colgrfPttApmApmTime = 3, colgrfPttApmHN = 4, colgrfPttApmPttName = 5, colgrfPttApmDeptR = 6, colgrfPttApmDeptMake = 7, colgrfPttApmNote = 8, colgrfPttApmOrder = 9, colgrfPttApmDocYear = 10, colgrfPttApmDocNo = 11, colgrfPttApmDtrname = 12, colgrfPttApmPhone = 13, colgrfPttApmPaidName = 14, colgrfPttApmRemarkCall = 15, colgrfPttApmStatusRemarkCall = 16, colgrfPttApmRemarkCallDate = 17, colgrfPttApmApmDate1 = 18;

        int colgrfDrugSetItemCode = 1, colgrfDrugSetItemName = 2, colgrfDrugSetFreq = 3, colgrfDrugSetPrecau = 4, colgrfDrugSetInterac = 5, colgrfDrugSetItemStatus = 6, colgrfDrugSetItemQty = 7, colgrfDrugSetID = 8, colgrfDrugSetFlagSave = 9;

        int rowindexgrfVs = 0, rowindexgrfOutLab=0;
        List<listStream> lStream, lStreamPic;
        listStream strm;
        Stream streamPrint;
        Label lbLoading;
        Label lbDocAll, lbDocGrp1, lbDocGrp2, lbDocGrp3, lbDocGrp4, lbDocGrp5, lbDocGrp6, lbDocGrp7, lbDocGrp8, lbDocGrp9;
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmPatient(BangnaControl bc, String dtrcode,ref Patient ptt)
        {
            this.bc = bc;            this.PTT = ptt;            this.DTRCODE = dtrcode;            InitializeComponent();
            initConfig();
        }
        public FrmPatient(BangnaControl bc, String hn)
        {
            this.bc = bc;            this.HN = hn;
            InitializeComponent();            initConfig();
        }
        public FrmPatient(BangnaControl bc, String hn, String vsdate, String preno)
        {
            this.bc = bc;            this.HN = hn;            this.VSDATE = vsdate;            this.PRENO = preno;
            InitializeComponent();            initConfig();
        }
        public FrmPatient(BangnaControl bc, String hn, String vsdate, String preno, String dtrcode)
        {
            this.bc = bc;            this.HN = hn;            this.VSDATE = vsdate;            this.PRENO = preno;
            InitializeComponent();            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            theme1 = new C1ThemeController();            lStream = new List<listStream>();
            initFont();            initControl();
            
            setEvent();            setTheme();

            setControlPnPateint();
            pageLoad = false;
        }
        private void initControl()
        {
            acmApmTime = new AutoCompleteStringCollection();
            acmApmTime.AddRange(AUTOSymptom);

            autoDrug = new AutoCompleteStringCollection();
            autoLab = new AutoCompleteStringCollection();
            autoXray = new AutoCompleteStringCollection();
            autoProcedure = new AutoCompleteStringCollection();
            autoDrug = bc.bcDB.pharM01DB.getlDrugAll();
            autoLab = bc.bcDB.labM01DB.getlLabAll();
            autoXray = bc.bcDB.xrayM01DB.getlLabAll();
            autoProcedure = bc.bcDB.pm30DB.getlProcedureAll();
            autoApm = new AutoCompleteStringCollection();
            autoApm = bc.bcDB.pm13DB.getlApm();

            initLoading();
            initGrfVS();
            initGrfLab(ref grfLab,ref pnLab);
            initGrfXray(ref grfXray,ref pnXray);
            initGrfProcedure();            initGrfOrder();            initGrfDrugAllergy();            initGrfChronic();
            initGrfIPDScan();            initGrfOutLab();
            initGrfOrder(ref grfOrder, ref pnOrder, "grfOrder");
            initGrfPttApm(ref grfPttApm, ref pnPttApm, "grfPttApm");
            initGrfOrder(ref grfApmOrder, ref pnApmOrder, "grfApmOrder");
            initGrfDrugSet();

            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = autoDrug;
            txtApmTime.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtApmTime.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtApmTime.AutoCompleteCustomSource = acmApmTime;
            txtApmDsc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtApmDsc.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtApmDsc.AutoCompleteCustomSource = autoApm;
            chlApmList.Hide();
            foreach (String txt in autoApm)
            {
                chlApmList.Items.Add(txt);
            }
            //txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;

            fvOutlab = new C1FlexViewer();
            fvOutlab.AutoScrollMargin = new System.Drawing.Size(0, 0);
            fvOutlab.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            fvOutlab.Dock = System.Windows.Forms.DockStyle.Fill;
            fvOutlab.Location = new System.Drawing.Point(0, 0);
            fvOutlab.Name = "fvOutlab";
            fvOutlab.Size = new System.Drawing.Size(1065, 790);
            fvOutlab.TabIndex = 0;
            fvOutlab.Ribbon.Minimized = true;
            pnOutLab.Controls.Add(fvOutlab);

            fvCerti = new C1FlexViewer();
            fvCerti.AutoScrollMargin = new System.Drawing.Size(0, 0);
            fvCerti.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            fvCerti.Dock = System.Windows.Forms.DockStyle.Fill;
            fvCerti.Location = new System.Drawing.Point(0, 0);
            fvCerti.Name = "fvCerti";
            fvCerti.Size = new System.Drawing.Size(1065, 790);
            fvCerti.TabIndex = 0;
            fvCerti.Ribbon.Minimized = true;
            pnCertiMed.Controls.Add(fvCerti);
            rgSb1.Text = "";            lfSbComp.Text = "";            lfSbInsur.Text = "";            lfSbStation.Text = "";            lfSbMessage.Text = "";
            bc.bcDB.drugSetDB.setCboDgs(cboDrugSetName, DTRCODE, "");
            pnDrugSet.Hide();
            bc.bcDB.pttDB.setCboDeptOPDNew(cboApmDept, "");
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
        private void setTheme()
        {
            theme1.SetTheme(pnPtt, "ExpressionLight");
            foreach (Control c in pnPtt.Controls) if (c is C1TextBox) theme1.SetTheme(c, "ExpressionLight");
        }
        private void setEvent()
        {
            tabVS.SelectedTabChanged += TabVS_SelectedTabChanged;
            btnCerti1.Click += BtnCerti1_Click;
            btnCerti2.Click += BtnCerti2_Click;
            btnCertiView2.Click += BtnCertiView2_Click;
            btnCertiView1.Click += BtnCertiView1_Click;

            txtSearchItem.KeyUp += TxtSearchItem_KeyUp;
            txtSearchItem.Enter += TxtSearchItem_Enter;
            txtItemCode.KeyUp += TxtItemCode_KeyUp;
            txtItemQTY.KeyUp += TxtItemQTY_KeyUp;

            chkItemLab.Click += ChkItemLab_Click;
            chkItemXray.Click += ChkItemXray_Click;
            chkItemProcedure.Click += ChkItemHotC_Click;
            chkItemDrug.Click += ChkItemDrug_Click;
            btnItemAdd.Click += BtnItemAdd_Click;
            btnOrderSave.Click += BtnOrderSave_Click;
            btnOperItemSearch.Click += BtnOperItemSearch_Click;
            btnOrderSubmit.Click += BtnOrderSubmit_Click;
            lbApmList.DoubleClick += LbApmList_DoubleClick;
            btnApmOrder.Click += BtnApmOrder_Click;
            btnApmSave.Click += BtnApmSave_Click;
            btnApmNew.Click += BtnApmNew_Click;
            btnApmPrint.Click += BtnApmPrint_Click;

            txtApmRemark.KeyUp += TxtApmRemark_KeyUp;
            txtApmTel.KeyUp += TxtApmTel_KeyUp;
            txtApmDsc.KeyUp += TxtApmDsc_KeyUp;
            cboApmDept.DropDownClosed += CboApmDept_DropDownClosed;
            txtApmTime.KeyUp += TxtApmTime_KeyUp;
            txtPttApmDate.DropDownClosed += TxtPttApmDate_DropDownClosed;

            lbApm7Week.Click += LbApm7Week_Click;
            lbApm14Week.Click += LbApm14Week_Click;
            lbApm1Month.Click += LbApm1Month_Click;
            txtApmPlusDay.KeyPress += TxtApmPlusDay_KeyPress;
            txtApmPlusDay.KeyUp += TxtApmPlusDay_KeyUp;
            cboDrugSetName.SelectedIndexChanged += CboDrugSetName_SelectedIndexChanged;
            btnDrugSetAll.Click += BtnDrugSetAll_Click;

            txtDrugNum.KeyUp += TxtNum_KeyUp;
            txtDrugNum.KeyPress += TxtNum_KeyPress;
            txtDrugPerDay.KeyUp += TxtPerDay_KeyUp;
            txtDrugPerDay.KeyPress += TxtPerDay_KeyPress;
            txtDrugNumDay.KeyUp += TxtNumDay_KeyUp;
            txtDrugNumDay.KeyPress += TxtNumDay_KeyPress;

            txtFrequency.KeyUp += TxtFrequency_KeyUp;
            txtPrecautions.KeyUp += TxtPrecautions_KeyUp;
            txtIndication.KeyUp += TxtIndication_KeyUp;
            txtInteraction.KeyUp += TxtInteraction_KeyUp;
            btnPrnStaffNote.Click += BtnPrnStaffNote_Click;
            btnPrnSticker.Click += BtnPrnSticker_Click;

            txtPttHN.KeyUp += TxtPttHN_KeyUp;
            picL.DoubleClick += PicL_DoubleClick;
            picR.DoubleClick += PicL_DoubleClick;
        }

        private void PicL_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(picL.Image == null) { return; }
            showStaffNote();
        }
        private void showStaffNote()
        {
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.WindowState = FormWindowState.Maximized;
            PictureBox picl = new PictureBox();
            picl.Dock = DockStyle.Left;
            picl.Image = picL.Image;
            picl.SizeMode = PictureBoxSizeMode.StretchImage;
            picl.Top = 0;
            PictureBox picr = new PictureBox();
            picr.Dock = DockStyle.Right;
            picr.Image = picR.Image;
            picr.SizeMode = PictureBoxSizeMode.StretchImage;
            picr.Top = 0;
            frm.Controls.Add(picr);
            frm.Controls.Add(picl);
            Rectangle screenRect = Screen.GetBounds(Bounds);
            picr.Width = screenRect.Width / 2;
            picl.Width = screenRect.Width / 2;
            frm.ShowDialog();
        }
        private void TxtPttHN_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //ไม่อยากให้แก้ไข HN แต่ถ้าเป็น โปรแกรม run มาแต่แรก ก็OK
                if (bc.iniC.programLoad.Equals("ScanView"))
                {
                    this.HN = txtPttHN.Text.Trim();
                    PTT = bc.bcDB.pttDB.selectPatinetByHn(this.HN);
                    setControlPnPateint();
                }
            }
        }
        private void BtnPrnSticker_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Form frm = new FrmSmartCard(bc,HN,"sticker");
            frm.ShowDialog();
        }

        private void BtnPrnStaffNote_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printStaffNote();
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
            float marginR = e.MarginBounds.Right, avg = marginR / 2;
            
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            col2int = int.Parse(col2.ToString());
            yPosint = int.Parse(yPos.ToString());
            col40int = int.Parse(col40.ToString());

            col2 = 65;            col3 = 300;            col4 = 870;            col40 = 650;
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
            
            //textSize = TextRenderer.MeasureText(line, famt7B, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString("H.N. " + PTT.MNC_HN_NO + "     " + VS.VN, fEdit, Brushes.Black, col3 + 25, yPos + 5, flags);
            e.Graphics.DrawString("H.N. " + PTT.MNC_HN_NO + "     " + VS.VN, fEdit, Brushes.Black, col4 + 30, yPos + 5, flags);

            e.Graphics.DrawString("ชื่อ " + PTT.Name, fEdit, Brushes.Black, col3 + 20, yPos + 20, flags);
            e.Graphics.DrawString("ชื่อ " + PTT.Name, fEdit, Brushes.Black, col4 + 10, yPos + 20, flags);
            e.Graphics.DrawString("เลขที่บัตร " + PTT.MNC_ID_NO, fEdit, Brushes.Black, col3, yPos + 40, flags);
            e.Graphics.DrawString("เลขที่บัตร " + PTT.MNC_ID_NO, fEdit, Brushes.Black, col4, yPos + 40, flags);
            
            e.Graphics.DrawString(VS.PaidName, fEdit, Brushes.Black, col2, yPos + 40, flags);
            e.Graphics.DrawString(VS.PaidName, fEdit, Brushes.Black, col40, yPos + 40, flags);
            e.Graphics.DrawString(VS.CompName, fEdit, Brushes.Black, col40, yPos + 40, flags);

            e.Graphics.DrawString("โรคประจำตัว        ไม่มี", fEdit, Brushes.Black, col2, yPos + 60, flags);
            rec = new Rectangle(col2int + 75, 72, recx, recy);
            e.Graphics.DrawRectangle(blackPen, rec);

            e.Graphics.DrawString(prndob, fEdit, Brushes.Black, col3, yPos + 60, flags);
            e.Graphics.DrawString(prndob, fEdit, Brushes.Black, col4, yPos + 60, flags);

            e.Graphics.DrawString("มีโรค ระบุ", fEdit, Brushes.Black, col2 + 70, yPos + 80, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 67 - recx, 92, recx, recy));

            e.Graphics.DrawString("วันที่เวลา " + date, fEdit, Brushes.Black, col3, yPos + 80, flags);
            e.Graphics.DrawString("วันที่เวลา " + date, fEdit, Brushes.Black, col4, yPos + 80, flags);
            e.Graphics.DrawString("โรคเรื้อรัง", fEdit, Brushes.Black, col2, yPos + 100, flags);
            
            e.Graphics.DrawString("ชื่อแพทย์ " + VS.DoctorId + " " + VS.DoctorName, fEdit, Brushes.Black, col3, yPos + 100, flags);
            e.Graphics.DrawString("ชื่อแพทย์ " + VS.DoctorId + " " + VS.DoctorName, fEdit, Brushes.Black, col4 - 50, yPos + 120, flags);
            e.Graphics.DrawString("DR Time.                               ปิดใบยา", fEdit, Brushes.Black, col3, yPos + 120, flags);

            e.Graphics.DrawString("อาการเบื้องต้น " + VS.symptom.Replace("\r\n", ""), fEdit, Brushes.Black, col2, yPos + 120, flags);
            e.Graphics.DrawString("อาการเบื้องต้น " + VS.symptom.Replace("\r\n", ""), fEdit, Brushes.Black, col40, yPos + 100, flags);

            e.Graphics.DrawString("Temp" + VS.temp, fEditS, Brushes.Black, col2, yPos + 140, flags);
            e.Graphics.DrawString("H.Rate" + VS.ratios, fEditS, Brushes.Black, col2 + 80, yPos + 140, flags);
            e.Graphics.DrawString("R.Rate" + VS.breath, fEditS, Brushes.Black, col2 + 160, yPos + 140, flags);
            
            e.Graphics.DrawString("BP1" + VS.bp1l, fEditS, Brushes.Black, col2 + 240, yPos + 140, flags);
            e.Graphics.DrawString("Time :", fEditS, Brushes.Black, col2 + 300, yPos + 140, flags);
            e.Graphics.DrawString("BP2 " + VS.bp1r, fEditS, Brushes.Black, col2 + 380, yPos + 140, flags);
            e.Graphics.DrawString("Time :", fEditS, Brushes.Black, col2 + 440, yPos + 140, flags);
            e.Graphics.DrawString("Wt." + VS.weight, fEditS, Brushes.Black, col2, yPos + 160, flags);
            
            e.Graphics.DrawString("Ht." + VS.high, fEditS, Brushes.Black, col2 + 80, yPos + 160, flags);
            e.Graphics.DrawString("BMI.", fEditS, Brushes.Black, col2 + 100, yPos + 160, flags);
            e.Graphics.DrawString("CC." + VS.cc, fEditS, Brushes.Black, col2 + 180, yPos + 160, flags);
            e.Graphics.DrawString("CC.IN" + VS.ccin, fEditS, Brushes.Black, col2 + 240, yPos + 160, flags);
            
            e.Graphics.DrawString("CC.EX" + VS.ccex, fEditS, Brushes.Black, col2 + 300, yPos + 160, flags);
            e.Graphics.DrawString("Ab.C" + VS.abc, fEditS, Brushes.Black, col2 + 400, yPos + 160, flags);
            
            e.Graphics.DrawString("H.C." + VS.hc, fEditS, Brushes.Black, col2 + 460, yPos + 160, flags);
            e.Graphics.DrawString("Precaution (Med) _________________________________________ ", fEdit, Brushes.Black, col40 + 10, yPos + 220, flags);
            
            e.Graphics.DrawString("แพ้ยา/อาหาร/อื่นๆ         ไม่มี", fEdit, Brushes.Black, col2, yPos + 180, flags);
            e.Graphics.DrawString("แพ้ยา/อาหาร/อื่นๆ         ไม่มี", fEdit, Brushes.Black, col40, yPos + 180, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 123 - recx, yPosint + 180, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 120 - recx, yPosint + 180, recx, recy));
            
            e.Graphics.DrawString("มี ระบุอาการ", fEdit, Brushes.Black, col2 + 20, yPos + 200, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 20 - recx, yPosint + 200, recx, recy));
            e.Graphics.DrawString("มี ระบุอาการ", fEdit, Brushes.Black, col40 + 15, yPos + 200, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 15 - recx, yPosint + 200, recx, recy));

            e.Graphics.DrawString(bc.bcDB.pm32DB.getDeptNameOPD(VS.DeptCode), fEdit, Brushes.Black, col40 + 40, yPos + 260, flags);

            e.Graphics.DrawString("Medication                       No Medication", fEdit, Brushes.Black, col40 + 50, yPos + 280, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 30 - recx - 5, yPosint + 280, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col40int + 120 - recx + 60, yPosint + 280, recx, recy));

            e.Graphics.DrawString("อาการ" + VS.symptom, fEditB, Brushes.Black, col3 + 40, yPos + 315, flags);
            e.Graphics.DrawString("อาการ" + VS.symptom, fEditB, Brushes.Black, col2 + 20, yPos + 360, flags);

            e.Graphics.DrawString("ใบรับรองแพทย์             ไม่มี      มี             Consult      ไม่มี      มี __________________", fEdit, Brushes.Black, col2 + 40, yPos + 660, flags);
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 170 - recx, yPosint + 660, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 215 - recx, yPosint + 660, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 345 - recx, yPosint + 660, recx, recy));
            e.Graphics.DrawRectangle(blackPen, new Rectangle(col2int + 385 - recx, yPosint + 660, recx, recy));

            e.Graphics.DrawString("ชื่อผู้รับ _____________________________", fEdit, Brushes.Black, col2, yPos + 680, flags);
            e.Graphics.DrawString("Health Education :", fEdit, Brushes.Black, col2, yPos + 730, flags);
            e.Graphics.DrawString("ลงชื่อพยาบาล: _____________________________________", fEdit, Brushes.Black, col2, yPos + 750, flags);
            e.Graphics.DrawString("FM-REC-002 (00 10/09/53)(1/1)", fEditS, Brushes.Black, col2, yPos + 770, flags);
            e.Graphics.DrawString("FM-REC-002 (00 10/09/53)(1/1)", fEditS, Brushes.Black, col40, yPos + 770, flags);
        }
        private void TxtItemQTY_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode== Keys.Right)
            {
                txtDrugNum.SelectAll();
                txtDrugNum.Focus();
            }else if (e.KeyCode == Keys.Enter)
            {
                setGrfOrderItem();
                clearControlOrder();
                txtSearchItem.SelectAll();
                txtSearchItem.Focus();
            }
        }

        private void TxtInteraction_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtSearchItem.SelectAll();                txtSearchItem.Focus();            }
        }

        private void TxtIndication_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtInteraction.SelectAll();                txtInteraction.Focus();            }
        }

        private void TxtPrecautions_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtIndication.SelectAll();                txtIndication.Focus();            }
        }

        private void TxtFrequency_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPrecautions.SelectAll();                txtPrecautions.Focus();            }
        }

        private void TxtNumDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))            {                e.Handled = true;            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))            {                e.Handled = true;            }
        }
        private void TxtNumDay_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            setQty();
            if (e.KeyCode == Keys.Enter)
            {
                txtItemQTY.SelectAll();
                txtItemQTY.Focus();
            }else if (e.KeyCode == Keys.Left)
            {
                txtDrugPerDay.SelectAll();
                txtDrugPerDay.Focus();
            }
        }
        private void TxtPerDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))            {                e.Handled = true;            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))            {                e.Handled = true;            }
        }
        private void TxtPerDay_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            setQty();
            if (e.KeyCode == Keys.Enter)
            {
                txtDrugNumDay.Focus();
            }else if (e.KeyCode == Keys.Left)
            {
                txtDrugNum.SelectAll();
                txtDrugNum.Focus();
            }
        }
        private void TxtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))            {                e.Handled = true;            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))            {                e.Handled = true;            }
        }
        private void TxtNum_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            setQty();
            if (e.KeyCode == Keys.Enter)            {                txtDrugPerDay.Focus();            }
            else if (e.KeyCode == Keys.Left)            {                txtItemQTY.SelectAll();                txtItemQTY.Focus();            }
        }
        private void setQty()
        {
            try
            {
                int num = 0, perday = 0, numday = 0, qty = 0;
                int.TryParse(txtDrugNum.Text, out num);
                int.TryParse(txtDrugPerDay.Text, out perday);
                int.TryParse(txtDrugNumDay.Text, out numday);

                qty = num * perday * numday;
                txtItemQTY.Value = qty;
            }
            catch (Exception ex)
            {

            }
        }
        private void BtnDrugSetAll_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            foreach(Row arow in grfDrugSet.Rows)
            {
                if (arow[colgrfDrugSetItemCode].ToString().Equals("code")) continue;
                Row rowa = grfOrder.Rows.Add();
                rowa[colgrfOrderCode] = arow[colgrfDrugSetItemCode].ToString();
                rowa[colgrfOrderName] = arow[colgrfDrugSetItemName].ToString();
                rowa[colgrfOrderQty] = arow[colgrfDrugSetItemQty].ToString();
                rowa[colgrfOrderStatus] = "drug";
                rowa[colgrfOrderDrugFre] = arow[colgrfDrugSetFreq].ToString();
                rowa[colgrfOrderDrugPrecau] = arow[colgrfDrugSetPrecau].ToString();
                rowa[colgrfOrderDrugInterac] = "";
                rowa[colgrfOrderID] = "";
                rowa[colgrfOrderReqNO] = "";
                rowa[colgrfOrdFlagSave] = "0";
            }
        }

        private void CboDrugSetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            setGrfDrugSet(cboDrugSetName.Text.Trim());
        }

        private void TxtApmPlusDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))            {                e.Handled = true;            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as C1TextBox).Text.IndexOf('.') > -1))            {                e.Handled = true;            }
        }

        private void TxtApmPlusDay_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            calApmDate();
        }
        private void calApmDate()
        {
            if (txtApmPlusDay.Text.Length <= 0) return;
            DateTime dtcal = DateTime.Now;
            if (dtcal.Year > 2500)            {                dtcal.AddYears(-543);            }
            dtcal = dtcal.AddDays(int.Parse(txtApmPlusDay.Text));
            txtPttApmDate.Value = dtcal.Year.ToString() + "-" + dtcal.ToString("MM-dd");
        }
        private void LbApm1Month_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtApmPlusDay.Value = "28";            calApmDate();
        }

        private void LbApm14Week_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtApmPlusDay.Value = "14";            calApmDate();
        }

        private void LbApm7Week_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtApmPlusDay.Value = "7";            calApmDate();
        }
        private void TxtPttApmDate_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            txtApmTime.Focus();
        }

        private void TxtApmTime_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                cboApmDept.Focus();            }
        }

        private void CboApmDept_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            txtApmDsc.Focus();
        }

        private void TxtApmDsc_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
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

        private void TxtApmTel_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtApmRemark.Focus();            }
        }

        private void TxtApmRemark_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtApmDtr.Focus();            }
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
            float yPos = 10, col1 = 0, col2 = 50, col21 = 180, col3 = 250, col4 = 450, col41 = 560, col5 = 300, line = 25;
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
            e.Graphics.DrawString(bc.iniC.hostname, famt5, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText(bc.iniC.hostnamee, famt5, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString(bc.iniC.hostnamee, famt5, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            yPos = yPos + line;//ขึ้นบันทัดใหม่
            textSize = TextRenderer.MeasureText("ใบนัดพบแพทย์ Appointment Note", famt5, proposedSize, TextFormatFlags.RightToLeft);
            e.Graphics.DrawString("ใบนัดพบแพทย์ Appointment Note", famt5, Brushes.Black, centerpage - (textSize.Width / 2), yPos, flags);
            e.Graphics.DrawString("เลขที่:", famt5, Brushes.Black, col41 + 70, yPos, flags);
            e.Graphics.DrawString(APM.MNC_DOC_YR.Substring(APM.MNC_DOC_YR.Length - 2) + "-" + APM.MNC_DOC_NO, famt5, Brushes.Black, col41 + 120, yPos, flags);

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
            e.Graphics.DrawString(bc.datetoShow(APM.MNC_APP_DAT) + " " + APM.apm_time, famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("Dept/นัดตรวจที่แผนก:", famt5, Brushes.Black, col4, yPos, flags);
            e.Graphics.DrawString(bc.bcDB.pm32DB.getDeptNameOPD(APM.MNC_SECR_NO), famt5, Brushes.Black, col41 + 60, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("Doctor/แพทย์:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(APM.doctor_name, famt5, Brushes.Black, col21, yPos, flags);

            yPos = yPos + line;//ขึ้นบันทัดใหม่
            e.Graphics.DrawString("สิ่งที่ต้องเตรียม:", famt5, Brushes.Black, col2, yPos, flags);
            e.Graphics.DrawString(APM.MNC_REM_MEMO, famt5, Brushes.Black, col21, yPos, flags);
            e.Graphics.DrawString("รายการตรวจ: ", famt5, Brushes.Black, col4, yPos, flags);
            String txt = "";
            if (grfApmOrder.Rows.Count > 1)
            {
                foreach (Row rowa in grfApmOrder.Rows)
                {
                    String name = "";
                    name = rowa[colgrfOrderName].ToString();
                    if (name.Equals("name")) continue;
                    txt += name + "\r\n";
                }
                if (txt.Length > 1)
                {
                    txt = txt.Substring(0, txt.Length - 1);
                }
                e.Graphics.DrawString(txt, famt1, Brushes.Black, col41, yPos, flags);
            }

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
        private void clearControlOrder()
        {
            txtItemCode.Value = "";            txtSearchItem.Value = "";            lbItemName.Text = "";            txtIndication.Value = "";
            lbItemNameThai.Text = "";            lbTradeName.Text = "";            txtFrequency.Value = "";            txtPrecautions.Value = "";
            txtIndication.Value = "";            txtInteraction.Value = "";            lbStrength.Text = "";            txtItemQTY.Value = "";
            txtDrugNum.Value = "";            txtDrugPerDay.Value = "";            txtDrugNumDay.Value = "";
        }
        private void clearControlTabApm(Boolean new1)
        {
            txtPttApmDate.Value = DateTime.Now;
            txtApmTime.Value = "";            bc.setC1Combo(cboApmDept, "");            txtApmDsc.Value = "";            txtApmRemark.Value = "";
            txtApmDtr.Value = "";            lbApmDtrName.Text = "";            txtApmTel.Value = "";            txtApmNO.Value = "";
            txtApmList.Value = "";            grfApmOrder.Rows.Count = 1;
            if (!new1)            {                grfPttApm.Rows.Count = 1;            }
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
            apm.MNC_HN_NO = txtPttHN.Text.Trim();
            apm.MNC_HN_YR = VS.MNC_HN_YR;
            apm.MNC_DATE = VSDATE;
            apm.MNC_PRE_NO = PRENO;
            apm.MNC_DOC_YR = txtApmDocYear.Text.Trim();
            apm.MNC_DOC_NO = txtApmNO.Text.Trim();
            apm.MNC_TIME = VS.VisitTime;
            apm.MNC_APP_DAT = apmdate.Year + "-" + apmdate.ToString("MM-dd");
            apm.MNC_APP_TIM = bc.bcDB.pt07DB.setAppTime(txtApmTime.Text);
            //apm.MNC_APP_DSC = txtApmDsc.Text.Trim();
            apm.MNC_APP_DSC = txtApmList.Text.Trim();
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
        private void BtnApmSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
            frm.ShowDialog();
            if (bc.USERCONFIRMID.Length <= 0)
            {
                lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            PatientT07 apm = getApm();
            if (apm != null)
            {
                string re = bc.bcDB.pt07DB.insertPatientT07(apm);
                if (long.TryParse(re, out long chk))
                {
                    txtApmNO.Value = re;
                    txtApmDocYear.Value = (DateTime.Now.Year + 543).ToString();
                    if (grfApmOrder.Rows.Count > 1)
                    {
                        foreach (Row rowa in grfApmOrder.Rows)
                        {
                            String ordercode = rowa[colgrfOrderCode].ToString(); if (ordercode.Equals("code")) continue;
                            String ordername = rowa[colgrfOrderName].ToString();
                            String flag = rowa[colgrfOrderStatus].ToString();//O hotcharge, X xray, L lab
                            flag = flag.Equals("lab") ? "L" : flag.Equals("xray") ? "X" : flag.Equals("procedure") ? "O" : "";
                            String re1 = bc.bcDB.pt07DB.insertPatientT073(re, txtApmDocYear.Text.Trim(), ordercode, "", flag);
                            if (!long.TryParse(re1, out long chk1))
                            {
                                new LogWriter("e", "FrmOPD BtnApmSave_Click " + re1);
                                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnApmSave_Click save  ", re1);
                                lfSbMessage.Text = re1;
                            }
                        }
                    }
                    lfSbMessage.Text = txtApmNO.Text.Length > 0 ? "update appointment OK" : "insert appointment OK";
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

        private void BtnApmOrder_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmItemSearch frm = new FrmItemSearch(bc);
            frm.ShowDialog();
            if ((bc.items != null) && (bc.items.Count > 0))
            {
                pnApmOrder.Show();
                foreach (Item item in bc.items)
                {
                    Row rowa = grfApmOrder.Rows.Add();
                    rowa[colgrfOrderCode] = item.code;
                    rowa[colgrfOrderName] = item.name;
                    rowa[colgrfOrderStatus] = item.flag;
                    rowa[colgrfOrderQty] = "1";
                    rowa[0] = grfApmOrder.Rows.Count - 1;
                }
            }
        }
        private void BtnOrderSubmit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
            frm.ShowDialog();
            if (bc.USERCONFIRMID.Length <= 0)
            {
                lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            String re = "";
            re = bc.bcDB.vsDB.insertOrder(txtPttHN.Text.Trim(), VSDATE, PRENO, DTRCODE, bc.USERCONFIRMID);
            if (re.Length > 0)
            {
                String[] reqno = re.Split('#');
                if (reqno.Length > 2)
                {
                    DataTable dtdrug = new DataTable();
                    DataTable dtlab = new DataTable();
                    DataTable dtxray = new DataTable();
                    DataTable dtprocedure = new DataTable();
                    dtdrug = bc.bcDB.pharT06DB.selectbyHNReqNo(txtPttHN.Text.Trim(), reqno[4], reqno[3]);
                    dtlab = bc.bcDB.labT02DB.selectbyHNReqNo(txtPttHN.Text.Trim(), reqno[4], reqno[0]);
                    dtxray = bc.bcDB.xrayT02DB.selectbyHNReqNo(txtPttHN.Text.Trim(), reqno[4], reqno[1]);
                    dtprocedure = bc.bcDB.pt16DB.selectbyHNReqNo(txtPttHN.Text.Trim(), reqno[4], reqno[2]);
                    dtlab.Merge(dtdrug);
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
        private void BtnOperItemSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmItemSearch frm = new FrmItemSearch(bc);
            frm.ShowDialog();
            if (bc.items.Count > 0)
            {
                foreach (Item item in bc.items)
                {
                    setGrfOrderItem(item.code, item.name, item.qty, item.flag);
                }
            }
        }

        private void BtnOrderSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String txt = "", tick = "";
            tick = DateTime.Now.Ticks.ToString();
            foreach (Row rowa in grfOrder.Rows)
            {
                String code = "", flag = "", name = "", qty = "", chk = "", freq="", precau="", id="";
                code = rowa[colgrfOrderCode].ToString();
                if (code.Equals("code")) continue;
                chk = rowa[colgrfOrdFlagSave].ToString();
                if (chk.Equals("1")) continue;// มี ข้อมูลใน table temp_order แล้วไม่ต้อง save เดียวจะ มี2record และในกรณีที่ submit ออก reqno เรียบร้อยแล้วจะได้ ไม่ซ้ำ
                id = rowa[colgrfOrderID].ToString();
                name = rowa[colgrfOrderName].ToString();
                qty = rowa[colgrfOrderQty].ToString();
                flag = rowa[colgrfOrderStatus].ToString();
                freq = rowa[colgrfOrderDrugFre].ToString();
                precau = rowa[colgrfOrderDrugPrecau].ToString();
                String re = bc.bcDB.vsDB.insertOrderTemp(id, code, name, qty, freq, precau, flag, txtPttHN.Text.Trim(), VSDATE, PRENO);
                if (int.TryParse(re, out int _))
                {

                }
            }
            setGrfOrder();
        }
        private void BtnItemAdd_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfOrderItem();
        }
        private void ChkItemDrug_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteCustomSource = autoDrug;
            cboDrugSetName.Show();            lbDrugSet.Show();            pnDrugSet.Show();            btnDrugSetAll.Show();
            txtDrugNum.Show();            txtDrugNumDay.Show();            txtDrugPerDay.Show();            clearControlOrder();
            txtSearchItem.Focus();
        }
        private void ChkItemHotC_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteCustomSource = autoProcedure;
            cboDrugSetName.Hide();            lbDrugSet.Hide();            pnDrugSet.Hide();            btnDrugSetAll.Hide();
            txtDrugNum.Hide();            txtDrugNumDay.Hide();            txtDrugPerDay.Hide();            clearControlOrder();
            txtSearchItem.Focus();
        }
        private void ChkItemXray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteCustomSource = autoXray;
            cboDrugSetName.Hide();            lbDrugSet.Hide();            pnDrugSet.Hide();            btnDrugSetAll.Hide();
            txtDrugNum.Hide();            txtDrugNumDay.Hide();            txtDrugPerDay.Hide();            clearControlOrder();
            txtSearchItem.Focus();
        }
        private void ChkItemLab_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteCustomSource = autoLab;            cboDrugSetName.Hide();            lbDrugSet.Hide();            pnDrugSet.Hide();
            btnDrugSetAll.Hide();            txtDrugNum.Hide();            txtDrugNumDay.Hide();            txtDrugPerDay.Hide();
            clearControlOrder();            txtSearchItem.Focus();
        }
        private void TxtItemCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (chkItemDrug.Checked)
                {
                    txtItemQTY.SelectAll();
                    txtItemQTY.Focus();
                }
                else
                {
                    setGrfOrderItem();
                }
            }
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
        private void setGrfOrderItem()
        {
            if (chkItemDrug.Checked)
            {
                setGrfOrderItemDrug(txtItemCode.Text.Trim(), lbItemName.Text, txtItemQTY.Text.Trim(), txtFrequency.Text.Trim(), txtPrecautions.Text.Trim());
            }
            else
            {
                setGrfOrderItem(txtItemCode.Text.Trim(), lbItemName.Text, txtItemQTY.Text.Trim()
                , chkItemLab.Checked ? "lab" : chkItemXray.Checked ? "xray" : chkItemProcedure.Checked ? "procedure" : chkItemDrug.Checked ? "drug" : "");
            }
            if (!chkItemDrug.Checked)
            {
                txtSearchItem.Value = "";
                txtSearchItem.Focus();
            }
            else
            {
                txtItemQTY.SelectAll();
                txtItemQTY.Focus();
            }
            //grfOrder.Rows.Add(rowitem);
        }
        private void setGrfOrderItem(String code, String name, String qty, String flag)
        {
            if (grfOrder == null) { return; }
            ////if(grfOrder.Row<=0) { return; }
            Row rowitem = grfOrder.Rows.Add();
            rowitem[colgrfOrderCode] = code;
            rowitem[colgrfOrderName] = name;
            rowitem[colgrfOrderQty] = qty;
            rowitem[colgrfOrderStatus] = flag;
            rowitem[colgrfOrderDrugFre] = "";
            rowitem[colgrfOrderDrugPrecau] = "";
            rowitem[colgrfOrderID] = "";
            rowitem[colgrfOrderReqNO] = "";
            rowitem[colgrfOrdFlagSave] = "0";//ต้องการ save ลง table temp_order
            txtSearchItem.Value = "";
            txtSearchItem.Focus();
            //grfOrder.Rows.Add(rowitem);
        }
        private void setGrfOrderItemDrug(String code, String name, String qty, String drugfreq, String drugprecau)
        {
            if (grfOrder == null) { return; }
            ////if(grfOrder.Row<=0) { return; }
            Row rowitem = grfOrder.Rows.Add();
            rowitem[colgrfOrderCode] = code;
            rowitem[colgrfOrderName] = name;
            rowitem[colgrfOrderQty] = qty;
            rowitem[colgrfOrderStatus] = "drug";
            rowitem[colgrfOrderDrugFre] = drugfreq;
            rowitem[colgrfOrderDrugPrecau] = drugprecau;
            rowitem[colgrfOrderID] = "";
            rowitem[colgrfOrderReqNO] = "";
            rowitem[colgrfOrdFlagSave] = "0";//ต้องการ save ลง table temp_order
            txtSearchItem.Value = "";
            txtSearchItem.Focus();
            //grfOrder.Rows.Add(rowitem);
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
            if (chkItemLab.Checked)
            {
                LabM01 lab = new LabM01();
                lab = bc.bcDB.labM01DB.SelectByPk(code);
                txtItemCode.Value = lab.MNC_LB_CD;
                lbItemName.Text = lab.MNC_LB_DSC;
                //txtItemQTY.Visible = false;
                txtItemQTY.Value = "1";
            }
            else if (chkItemXray.Checked)
            {
                XrayM01 xray = new XrayM01();
                xray = bc.bcDB.xrayM01DB.SelectByPk(code);
                txtItemCode.Value = xray.MNC_XR_CD;
                lbItemName.Text = xray.MNC_XR_DSC;
                //txtItemQTY.Visible = false;
                txtItemQTY.Value = "1";
            }
            else if (chkItemProcedure.Checked)
            {
                PatientM30 pm30 = new PatientM30();
                String name1 = bc.bcDB.pm30DB.SelectNameByPk(code);
                txtItemCode.Value = code;
                lbItemName.Text = name1;
                //txtItemQTY.Visible = false;
                txtItemQTY.Value = "1";
            }
            else if (chkItemDrug.Checked)
            {
                PharmacyM01 drug = new PharmacyM01();
                drug = bc.bcDB.pharM01DB.SelectNameByPk1(code);
                txtItemCode.Value = code;
                lbItemName.Text = drug.MNC_PH_TN;
                lbItemNameThai.Text = drug.MNC_PH_THAI;
                lbTradeName.Text = drug.MNC_PH_GN;
                txtFrequency.Value = drug.frequency;
                txtPrecautions.Value = drug.precautions;
                txtIndication.Value = drug.indication;
                lbStrength.Text = drug.MNC_PH_STRENGTH;
                txtItemQTY.Value = "1";
            }
        }
        private void BtnCertiView2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String cert2ndleaf = bc.bcDB.mcertiDB.selectCertIDByHn2ndLeaf(HN, PRENO, VSDATE);
            if (cert2ndleaf.Length > 0)
            {
                pnCertiMed.Controls.Clear();
                pnCertiMed.Controls.Add(fvCerti);
                MedicalCertificate certi = new MedicalCertificate();
                certi = bc.bcDB.mcertiDB.selectByPk(cert2ndleaf);
                DocScan dsc = new DocScan();
                dsc = bc.bcDB.dscDB.selectByPk(certi.doc_scan_id);
                FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
                C1PdfDocumentSource pdsMedCerti = new C1PdfDocumentSource();
                pdsMedCerti.LoadFromStream(streamCertiDtr);
                fvCerti.DocumentSource = pdsMedCerti;
            }
        }
        private void BtnCerti2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (bc.iniC.hostname.Equals("โรงพยาบาล บางนา5"))
            {
                FrmCertDoctor frm = new FrmCertDoctor(bc, DTRCODE, txtPttHN.Text.Trim(), VSDATE, PRENO, "2NFLEAF");
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
                FrmCertDoctorBn1 frm = new FrmCertDoctorBn1(bc, DTRCODE, txtPttHN.Text.Trim(), VSDATE, PRENO, "2NFLEAF");
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
        private void BtnCertiView1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String cert2ndleaf = bc.bcDB.mcertiDB.selectCertIDByHn2ndLeaf(HN, PRENO, VSDATE);
            if (cert2ndleaf.Length > 0)
            {
                pnCertiMed.Controls.Clear();
                pnCertiMed.Controls.Add(fvCerti);
                MedicalCertificate certi = new MedicalCertificate();
                certi = bc.bcDB.mcertiDB.selectByPk(cert2ndleaf);
                DocScan dsc = new DocScan();
                dsc = bc.bcDB.dscDB.selectByPk(certi.doc_scan_id);
                FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
                C1PdfDocumentSource pdsMedCerti = new C1PdfDocumentSource();
                pdsMedCerti.LoadFromStream(streamCertiDtr);
                fvCerti.DocumentSource = pdsMedCerti;
            }
        }
        private void BtnCerti1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (bc.iniC.hostname.Equals("โรงพยาบาล บางนา5"))
            {
                FrmCertDoctor frm = new FrmCertDoctor(bc, DTRCODE, txtPttHN.Text.Trim(), VSDATE, PRENO);
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
                FrmCertDoctorBn1 frm = new FrmCertDoctorBn1(bc, DTRCODE, txtPttHN.Text.Trim(), VSDATE, PRENO);
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
        private void setCertiMed()
        {
            String docscanid = "", cert2ndleaf = "";
            docscanid = bc.bcDB.mcertiDB.selectDocScanIDByHn(txtPttHN.Text.Trim(), PRENO, VSDATE);
            DocScan dsc = new DocScan();
            dsc = bc.bcDB.dscDB.selectByPk(docscanid);
            btnCertiView1.Enabled = false;
            cert2ndleaf = bc.bcDB.mcertiDB.selectCertIDByHn2ndLeaf(txtPttHN.Text.Trim(), PRENO, VSDATE);
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
        private void TabVS_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tabVS.SelectedTab == tabMedScan)
            {
                TABVSACTIVE = tabMedScan.Name;
                if (bc.iniC.linkmedicalscan.Length <= 0)
                {
                    if (flagTabMedScanChange)
                    {
                        setGrfIPDScan();
                        flagTabMedScanChange = false;
                    }
                }
                else
                {
                    try
                    {
                        string url = bc.iniC.linkmedicalscan + HN + "?userid=" + DTRCODE;
                        //System.Diagnostics.Process.Start("open", url);
                        url = url.Replace("&", "^&");
                        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                    }
                    catch (Exception ex) 
                    {
                        lfSbMessage.Text = ex.Message;
                        new LogWriter("e", this.Name+" TabVS_SelectedTabChanged " + ex.Message);
                        bc.bcDB.insertLogPage(bc.userId, this.Name, "TabVS_SelectedTabChanged ", ex.Message);
                    }                    
                }
                
            }
            else if (tabVS.SelectedTab == tabOrder){TABVSACTIVE = tabOrder.Name; }
            else if (tabVS.SelectedTab == tabOutLab) 
            { 
                TABVSACTIVE = tabOutLab.Name;
                setGrfOutLab();
            }
            else if (tabVS.SelectedTab == tabStaffNote) { TABVSACTIVE = tabStaffNote.Name; }
            else if (tabVS.SelectedTab == tabApm) 
            { 
                TABVSACTIVE = tabApm.Name;
                setGrfPttApm();
            }
            else if (tabVS.SelectedTab == tabCerti) { TABVSACTIVE = tabCerti.Name; }
            else if (tabVS.SelectedTab == tabOrderNew) { TABVSACTIVE = tabOrderNew.Name; }
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
        private void initGrfDrugSet()
        {
            grfDrugSet = new C1FlexGrid();
            grfDrugSet.Font = fEdit;
            grfDrugSet.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDrugSet.Location = new System.Drawing.Point(0, 0);
            grfDrugSet.Cols.Count = 10;
            grfDrugSet.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);
            grfDrugSet.Cols[colgrfDrugSetItemCode].Width = 70;
            grfDrugSet.Cols[colgrfDrugSetItemName].Width = 250;
            grfDrugSet.Cols[colgrfDrugSetFreq].Width = 250;
            grfDrugSet.Cols[colgrfDrugSetPrecau].Width = 250;
            grfDrugSet.Cols[colgrfDrugSetInterac].Width = 250;

            grfDrugSet.Cols[colgrfDrugSetItemCode].Caption = "code";
            grfDrugSet.Cols[colgrfDrugSetItemName].Caption = "Name";
            grfDrugSet.Cols[colgrfDrugSetFreq].Caption = "วิธีใช้ /ความถี่ในการใช้ยา";
            grfDrugSet.Cols[colgrfDrugSetPrecau].Caption = "ข้อบ่งชี้ /ข้อควรระวัง";
            grfDrugSet.Cols[colgrfDrugSetInterac].Caption = "ปฎิกิริยาต่อยาอื่น";
            grfDrugSet.Cols[colgrfDrugSetID].Visible = false;
            grfDrugSet.Cols[colgrfDrugSetFlagSave].Visible = false;
            grfDrugSet.Cols[colgrfDrugSetItemStatus].Visible = false;

            grfDrugSet.Cols[colgrfDrugSetItemCode].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetItemName].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetItemQty].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetFreq].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetPrecau].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetInterac].AllowEditing = false;

            grfDrugSet.DoubleClick += GrfDrugSet_DoubleClick;

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnDrugSet.Controls.Add(grfDrugSet);
            theme1.SetTheme(grfDrugSet, "VS2013Purple");
        }
        private void GrfDrugSet_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void setGrfDrugSet(String drugsetname)
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.drugSetDB.selectDrugSet(DTRCODE, drugsetname);
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1;
            grfDrugSet.Rows.Count = 1; grfDrugSet.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfDrugSet.Rows[i];
                String status = "", vn = "";
                rowa[colgrfDrugSetID] = row1["drug_set_id"].ToString();
                rowa[colgrfDrugSetItemCode] = row1["item_code"].ToString();
                rowa[colgrfDrugSetItemName] = row1["item_name"].ToString();
                rowa[colgrfDrugSetItemQty] = row1["qty"].ToString();
                rowa[colgrfDrugSetItemStatus] = row1["status_item"].ToString();
                rowa[colgrfDrugSetFreq] = row1["frequency"].ToString();
                rowa[colgrfDrugSetPrecau] = row1["precautions"].ToString();
                rowa[colgrfDrugSetFlagSave] = "0";
                i++;
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
            grf.Cols[colgrfPttApmDtrname].Caption = "แพทย์นัด";
            grf.Cols[colgrfPttApmPhone].Caption = "เบอร์ติดต่อ";
            grf.Cols[colgrfPttApmPaidName].Caption = "สิทธิการรักษา";

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
            grf.Cols[colgrfPttApmPttName].Visible = false;
            grf.Cols[colgrfPttApmRemarkCall].Visible = false;
            grf.Cols[colgrfPttApmStatusRemarkCall].Visible = false;
            grf.Cols[colgrfPttApmRemarkCallDate].Visible = false;

            grf.Cols[colgrfPttApmVsDate].AllowEditing = false;
            grf.Cols[colgrfPttApmApmDateShow].AllowEditing = false;
            grf.Cols[colgrfPttApmDeptR].AllowEditing = false;
            grf.Cols[colgrfPttApmNote].AllowEditing = false;
            grf.Cols[colgrfPttApmApmTime].AllowEditing = false;
            grf.Cols[colgrfPttApmOrder].AllowEditing = false;
            grf.Cols[colgrfPttApmDeptMake].AllowEditing = false;
            grf.Cols[colgrfPttApmDtrname].AllowEditing = false;
            grf.Cols[colgrfPttApmPhone].AllowEditing = false;
            grf.Cols[colgrfPttApmPaidName].AllowEditing = false;

            grfPttApm.Click += GrfPttApm_Click;

            pn.Controls.Add(grf);
            theme1.SetTheme(grf, bc.iniC.themeApp);
        }
        private void GrfPttApm_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;
            if (((C1FlexGrid)sender).Name.Equals("grfPttApm"))
            {
                String apmno = "", apmyear = "";
                apmno = grfPttApm[grfPttApm.Row, colgrfPttApmDocNo].ToString();
                apmyear = grfPttApm[grfPttApm.Row, colgrfPttApmDocYear].ToString();
                setControlApm(apmyear, apmno);
            }
            else if (((C1FlexGrid)sender).Name.Equals("grfApm"))
            {

            }
        }
        private void setGrfPttApm()
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.pt07DB.selectByHnAll(txtPttHN.Text.Trim(), "desc");
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
            if (apm.MNC_APP_DSC.IndexOf("\r\n")>0){
                txtApmList.Show();
            }
            else
            {
                txtApmList.Hide();
            }
            txtApmTel.Value = apm.MNC_APP_TEL;
            txtApmRemark.Value = apm.MNC_DOT_CD;
            lbApmDtrName.Text = bc.selectDoctorName(apm.MNC_DOT_CD);
            bc.setC1Combo(cboApmDept, apm.MNC_SECR_NO);
            DataTable dt = new DataTable();
            dt = bc.bcDB.pt07DB.selectAppointmentOrder(txtApmDocYear.Text, txtApmNO.Text);
            grfApmOrder.Rows.Count = 1;
            foreach (DataRow item in dt.Rows)
            {
                Row rowa = grfApmOrder.Rows.Add();
                String flag = "", name = "", code = "";
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
        private void LbApmList_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chlApmList.Visible == true)
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
                txtApmList.Value += "\r\n" + txt;
                txtApmList.ScrollBars = ScrollBars.Both;
            }
            else if (chlApmList.Visible == false)
            {
                chlApmList.Top = cboApmDept.Top;
                chlApmList.Left = cboApmDept.Left;
                chlApmList.Show();
                foreach (var chk in chlApmList.Items)
                {
                    if (chk.Selected)
                    {
                        chk.Selected = false;
                    }
                }
            }
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
            grf.Cols[colgrfOrderDrugFre].Width = 300;
            grf.Cols[colgrfOrderDrugPrecau].Width = 300;
            grf.Name = grfname;
            grf.ShowCursor = true;
            grf.Cols[colgrfOrderCode].Caption = "code";
            grf.Cols[colgrfOrderName].Caption = "name";
            grf.Cols[colgrfOrderQty].Caption = "qty";
            grf.Cols[colgrfOrderReqNO].Caption = "reqno";
            grf.Cols[colgrfOrderDrugFre].Caption = "Frequency";
            grf.Cols[colgrfOrderDrugPrecau].Caption = "Precautions";
            grf.Cols[colgrfOrderDrugIndica].Caption = "indication";
            grf.Cols[colgrfOrderDrugInterac].Caption = "interaction";

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
            grf.Cols[colgrfOrderStatus].Visible = true;//production ค่อยเปลี่ยนเป็น false
            grf.Cols[colgrfOrderID].Visible = false;
            grf.Cols[colgrfOrdFlagSave].Visible = false;
            if (grfname.Equals("grfOrder"))
            {
                grf.Cols[colgrfOrderQty].Visible = true;
                theme1.SetTheme(grf, "VS2013Red");
            }
            else
            {
                grf.Cols[colgrfOrderQty].Visible = false;
                theme1.SetTheme(grf, bc.iniC.themeApp);
            }
            grf.Cols[colgrfOrderCode].AllowEditing = false;
            grf.Cols[colgrfOrderName].AllowEditing = false;
            grf.Cols[colgrfOrderReqNO].AllowEditing = false;
            grf.Cols[colgrfOrderDrugFre].AllowEditing = false;
            grf.Cols[colgrfOrderDrugPrecau].AllowEditing = false;
            grf.Cols[colgrfOrderDrugIndica].AllowEditing = false;
            grf.Cols[colgrfOrderDrugInterac].AllowEditing = false;
            grf.DoubleClick += GrfOrder_DoubleClick;
            grf.Click += Grf_Click;
            grf.AllowSorting = AllowSortingEnum.None;
            pn.Controls.Add(grf);
        }
        private void Grf_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;
            if (((C1FlexGrid)sender).Name.Equals("grfOrder"))
            {
                String id = "", flag = "", name = "", freq = "", precau = "", code = "";
                id = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderID].ToString();
                txtOrderId.Value = id;
                txtItemCode.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderCode].ToString();
                lbItemName.Text = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderName].ToString();
                txtFrequency.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugFre].ToString();
                txtPrecautions.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugPrecau].ToString();
                txtItemQTY.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderQty].ToString();
            }
        }
        private void GrfOrder_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;

            if (((C1FlexGrid)sender).Name.Equals("grfApmOrder"))
            {
                ((C1FlexGrid)sender).Rows.Remove(((C1FlexGrid)sender).Row);
            }
            else if (((C1FlexGrid)sender).Name.Equals("grfOrder"))
            {
                String id = "";
                id = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderID].ToString();
                String re = bc.bcDB.vsDB.deleteOrderTemp(id);
                setGrfOrder();
            }
        }
        private void setGrfOrder()
        {//ดึงจาก table temp_order
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectOrderTempByHN(txtPttHN.Text.Trim(), VSDATE, PRENO);
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
                    rowa[colgrfOrderDrugFre] = row1["frequency"].ToString();
                    rowa[colgrfOrderDrugPrecau] = row1["precautions"].ToString();
                    rowa[colgrfOrderReqNO] = "";
                    rowa[colgrfOrdFlagSave] = "1";//ดึงข้อมูลจาก table temp_order 
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
            grfOutLab.Cols[colgrfOutLabDscVsDate].Caption = "Req Date";
            grfOutLab.Cols[colgrfOutLabDscVN].Caption = "VN";
            grfOutLab.Cols[colgrfOutLablabcode].Caption = "code";
            grfOutLab.Cols[colgrfOutLablabname].Caption = "lab name";

            grfOutLab.Cols[colgrfOutLabDscId].Visible = false;
            grfOutLab.Cols[colgrfOutLabDscHN].Visible = false;
            grfOutLab.Cols[colgrfOutLabDscPttName].Visible = false;
            grfOutLab.Cols[colgrfOutLablabcode].Visible = false;
            grfOutLab.Cols[colgrfOutLablabname].Visible = false;

            grfOutLab.Cols[colgrfOutLabDscHN].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLabDscPttName].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLabDscVN].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLablabcode].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLablabname].AllowEditing = false;
            grfOutLab.AfterRowColChange += GrfOutLab_AfterRowColChange;
            grfOutLab.SelectionMode = SelectionModeEnum.Row;
            pnOutLabList.Controls.Add(grfOutLab);

            theme1.SetTheme(grfOutLab, "ExpressionDark");
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
                if (rowindexgrfOutLab != ((C1FlexGrid)(sender)).Row) { rowindexgrfOutLab = ((C1FlexGrid)(sender)).Row; }
                showLbLoading();
                fvOutlab.DocumentSource = null;//rowindexgrfOutLab=0;
                dscid = grfOutLab[grfOutLab.Row, colVsPreno] != null ? grfOutLab[grfOutLab.Row, colgrfOutLabDscId].ToString() : "";
                C1PdfDocumentSource pds = new C1PdfDocumentSource();
                DocScan dsc = new DocScan();
                dsc = bc.bcDB.dscDB.selectByPk(dscid);
                //FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                FtpClient ftpc = new FtpClient(bc.iniC.hostFTPLabOut, bc.iniC.userFTPLabOut, bc.iniC.passFTPLabOut, bc.ftpUsePassive);
                pds.LoadFromStream(ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString()));
                fvOutlab.DocumentSource = pds;
                ftpc = null;
                hideLbLoading();
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
            fvOutlab.DocumentSource = null;
            DataTable dt = new DataTable();
            dt = bc.bcDB.dscDB.selectOutLabByHn(txtPttHN.Text.Trim());
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1;
            grfOutLab.Rows.Count = 1; grfOutLab.Rows.Count = dt.Rows.Count+1;
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
        class listStream
        {
            public String id = "", dgsid = "";
            public MemoryStream stream;
        }
        private void GrfIPDScan_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void setGrfIPDScan()
        {
            lbLoading.Text = "กรุณารอซักครู่ ... ";
            showLbLoading();
            lStream.Clear();
            String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "", anyr = "", vn1 = "";
            DataTable dtOrder = new DataTable();
            //new LogWriter("e", "FrmScanView1 setGrfScan 5 ");
            GC.Collect();
            DataTable dt = new DataTable();
            statusOPD = grfVS[grfVS.Row, colVsStatus] != null ? grfVS[grfVS.Row, colVsStatus].ToString() : "";
            preno = grfVS[grfVS.Row, colVsPreno] != null ? grfVS[grfVS.Row, colVsPreno].ToString() : "";
            vsDate = grfVS[grfVS.Row, colVsVsDate1] != null ? grfVS[grfVS.Row, colVsVsDate1].ToString() : "";
            an = grfVS[grfVS.Row, colVsAn] != null ? grfVS[grfVS.Row, colVsAn].ToString() : "";
            vsDate = bc.datetoDB(vsDate);
            //setStaffNote(vsDate, preno);
            dt = bc.bcDB.dscDB.selectByAn(txtPttHN.Text, an.Replace(".","/"));
            grfIPDScan.Rows.Count = 0;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    grfIPDScan.Rows.Count = (dt.Rows.Count / 2) + 1;
                    FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                    Boolean findTrue = false;                    int colcnt = 0, rowrun = -1;
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
                        String err = "";
                        try
                        {
                            err = "00";                            Row rowd;
                            if ((colcnt % 2) == 0){rowd = grfIPDScan.Rows[rowrun];}
                            else{rowrun++;rowd = grfIPDScan.Rows[rowrun];}
                            Image loadedImage = new Bitmap(ftpc.download4K(folderftp + "//" + filename));
                            int newWidth = bc.imgScanWidth;
                            Image resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / loadedImage.Width, null, IntPtr.Zero);
                            err = "05";
                            if ((colcnt % 2) == 0)
                            {
                                rowd[colPic3] = resizedImage;       // + 0001
                                rowd[colPic4] = id;       // + 0001
                            }
                            else
                            {
                                rowd[colPic1] = resizedImage;       // + 0001
                                rowd[colPic2] = id;       // + 0001
                            }
                            if (colcnt == 50) GC.Collect();
                            if (colcnt == 100) GC.Collect();
                            lbLoading.Text = "กรุณารอซักครู่ ... " + (colcnt - 1) + "/" + dt.Rows.Count;
                            if (colcnt % 9 == 0) Application.DoEvents();
                        }
                        catch (Exception ex)
                        {
                            String aaa = ex.Message + " " + err;
                            new LogWriter("e", "FrmScanView1 SetGrfScan ex " + ex.Message + " " + err + " colcnt " + colcnt + " doc_scan_id " + id);
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
        private void initGrfChronic()
        {
            grfChronic = new C1FlexGrid();
            grfChronic.Font = fEdit;
            grfChronic.Dock = System.Windows.Forms.DockStyle.Fill;
            grfChronic.Location = new System.Drawing.Point(0, 0);
            grfChronic.Rows.Count = 1;
            grfChronic.Cols.Count = 2;
            grfChronic.Cols[1].Width = 300;

            grfChronic.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfChronic.Cols[1].Caption = "-";

            grfChronic.Rows[0].Visible = false;
            grfChronic.Cols[0].Visible = false;
            grfChronic.Cols[1].Visible = true;
            grfChronic.Cols[1].AllowEditing = false;

            pnChronic.Controls.Add(grfChronic);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfChronic, "Office2010Red");
        }
        private void setGrfChronic()
        {
            //ใช้ database ใน object patient จะได้ไม่ต้องดึงข้อมูลหลายครั้ง  ลดการดึงข้อมูล
            PTT.CHRONIC = bc.bcDB.vsDB.SelectChronicByPID(PTT.idcard);
            grfChronic.Rows.Count = 1; grfChronic.Rows.Count = PTT.CHRONIC.Rows.Count + 1;
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1;
            foreach (DataRow row1 in PTT.CHRONIC.Rows)
            {
                //pB1.Value++;
                Row rowa = grfChronic.Rows[i];
                rowa[1] = row1["MNC_CRO_DESC"].ToString();
                i++;
            }
        }
        private void initGrfDrugAllergy()
        {
            grfDrugAllergy = new C1FlexGrid();
            grfDrugAllergy.Font = fEdit;
            grfDrugAllergy.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDrugAllergy.Location = new System.Drawing.Point(0, 0);
            grfDrugAllergy.Rows.Count = 1;
            grfDrugAllergy.Cols.Count = 4;
            grfDrugAllergy.Cols[1].Width = 300;
            grfDrugAllergy.Cols[2].Width = 300;
            grfDrugAllergy.Cols[3].Width = 300;

            grfDrugAllergy.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfDrugAllergy.Cols[1].Caption = "drug allergy";
            grfDrugAllergy.Cols[2].Caption = "-";
            grfDrugAllergy.Cols[3].Caption = "-";

            grfDrugAllergy.Rows[0].Visible = false;
            grfDrugAllergy.Cols[0].Visible = false;
            grfDrugAllergy.Cols[1].AllowEditing = false;
            grfDrugAllergy.Cols[2].AllowEditing = false;
            grfDrugAllergy.Cols[3].AllowEditing = false;
            grfDrugAllergy.Cols[1].Visible = true;
            
            pnDrugAllergy.Controls.Add(grfDrugAllergy);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            //theme1.SetTheme(grfDrugAllergy, "VS2013Dark");
            theme1.SetTheme(grfDrugAllergy, "ExpressionLight");
        }
        private void setGrfDrugAllergy()
        {
            //ใช้ database ใน object patient จะได้ไม่ต้องดึงข้อมูลหลายครั้ง  ลดการดึงข้อมูล
            PTT.DRUGALLERGY = bc.bcDB.vsDB.selectDrugAllergy(txtPttHN.Text.Trim());
            grfDrugAllergy.Rows.Count = 1; grfDrugAllergy.Rows.Count = PTT.DRUGALLERGY.Rows.Count + 1;
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1;
            foreach (DataRow row1 in PTT.DRUGALLERGY.Rows)
            {
                //pB1.Value++;
                Row rowa = grfDrugAllergy.Rows[i];
                rowa[1] = row1["mnc_ph_tn"].ToString();
                rowa[3] = row1["MNC_PH_ALG_DSC"].ToString();
                rowa[2] = row1["MNC_PH_MEMO"].ToString();
                i++;
            }
        }
        private void initGrfVS()
        {
            grfVS = new C1FlexGrid();
            grfVS.Font = fEdit;
            grfVS.Dock = System.Windows.Forms.DockStyle.Fill;
            grfVS.Location = new System.Drawing.Point(0, 0);
            grfVS.Rows.Count = 1;
            grfVS.Cols.Count = 30;
            grfVS.Cols[colVsVsDate].Width = 72;
            grfVS.Cols[colVsVnShow].Width = 80;
            grfVS.Cols[colVsDept].Width = 170;
            grfVS.Cols[colVsPreno].Width = 100;
            grfVS.Cols[colVsStatus].Width = 60;
            grfVS.Cols[colVsDtrName].Width = 180;
            grfVS.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfVS.Cols[colVsVsDate].Caption = "Visit Date";
            grfVS.Cols[colVsVnShow].Caption = "VN/AN";
            grfVS.Cols[colVsDept].Caption = "แผนก";
            grfVS.Cols[colVsPreno].Caption = "";
            //grfOPD.Cols[colVsPreno].Visible = false;
            //grfOPD.Cols[colVsVn].Visible = true;
            //grfOPD.Cols[colVsAn].Visible = true;
            //grfOPD.Cols[colVsAndate].Visible = false;
            grfVS.Rows[0].Visible = false;
            grfVS.Cols[0].Visible = false;
            grfVS.Cols[colVsVsDate].AllowEditing = false;
            grfVS.Cols[colVsVnShow].AllowEditing = false;
            grfVS.Cols[colVsDept].AllowEditing = false;
            grfVS.Cols[colVsPreno].AllowEditing = false;
            grfVS.Cols[colVsDtrName].AllowEditing = false;
            grfVS.Cols[colVsDeptName].AllowEditing = false;

            grfVS.Cols[colVsPreno].Visible = false;
            grfVS.Cols[colVsAn].Visible = false;
            grfVS.Cols[colVsAndate].Visible = false;
            grfVS.Cols[colVsVn].Visible = false;
            grfVS.Cols[colVsVnShow].Visible = true;

            grfVS.Cols[colVsbp2r].Visible = false;
            grfVS.Cols[colVsbp2l].Visible = false;
            grfVS.Cols[colVsbp1r].Visible = false;
            grfVS.Cols[colVsbp1l].Visible = false;
            grfVS.Cols[colVshc16].Visible = false;
            grfVS.Cols[colVsabc].Visible = false;
            grfVS.Cols[colVsccin].Visible = false;
            grfVS.Cols[colVsccex].Visible = false;
            grfVS.Cols[colVscc].Visible = false;
            grfVS.Cols[colVsWeight].Visible = false;
            grfVS.Cols[colVsHigh].Visible = false;
            grfVS.Cols[colVsVital].Visible = false;
            grfVS.Cols[colVsPres].Visible = false;
            grfVS.Cols[colVsTemp].Visible = false;
            grfVS.Cols[colVsPaidType].Visible = false;
            grfVS.Cols[colVsRadios].Visible = false;
            grfVS.Cols[colVsBreath].Visible = false;
            grfVS.Cols[colVsStatus].Visible = false;
            grfVS.Cols[colVsVsDate1].Visible = false;//colVsVsStatus
            grfVS.Cols[colVsVsStatus].Visible = false;
            //FilterRow fr = new FilterRow(grfExpn);
            //grfOPD.AfterScroll += GrfOPD_AfterScroll;
            grfVS.AfterRowColChange += GrfVS_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            pnVs.Controls.Add(grfVS);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfVS, "Office2010Black");
        }
        private void GrfVS_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
            try
            {
                if(rowindexgrfVs != ((C1FlexGrid)(sender)).Row) { rowindexgrfVs = ((C1FlexGrid)(sender)).Row;}
                else { return;}
                showLbLoading();
                PRENO = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colVsPreno] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colVsPreno].ToString() : "";
                VSDATE = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colVsVsDate1] != null ? ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colVsVsDate1].ToString() : "";
                //VS = bc.bcDB.vsDB.selectbyPreno(txtPttHN.Text.Trim(), VSDATE, PRENO);setGrfOrder()
                lfSbMessage.Text = ((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colVsVsStatus].ToString();
                lbVN.Text= "VN:"+((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colVsVn].ToString();
                if(((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colVsStatus].ToString().Equals("I")) { lbVN.Text += " AN:"+((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colVsAn].ToString(); }
                tabVS.TabPages[tabOrderNew.Name].TabVisible = (((C1FlexGrid)(sender))[((C1FlexGrid)(sender)).Row, colVsVsStatus].ToString().Equals("F")) ? false : true;
                flagTabMedScanChange = true;
                if (TABVSACTIVE.Equals("tabMedScan")){setGrfIPDScan();}
                else if (TABVSACTIVE.Equals("tabStaffNote")) {setStaffNote(VSDATE, PRENO, ref picL, ref picR);}
                else if (TABVSACTIVE.Equals("tabOrder")) {
                    setGrfLab(txtPttHN.Text.Trim(), VSDATE, PRENO, ref grfLab);
                    setGrfOrder(txtPttHN.Text.Trim(), VSDATE, PRENO, ref grfOrder);
                    setGrfXray(txtPttHN.Text.Trim(), VSDATE, PRENO, ref grfXray);
                    setGrfProcedure(txtPttHN.Text.Trim(), VSDATE, PRENO, ref grfProcedure);
                    setGrfOrder();
                }
                else if (TABVSACTIVE.Equals("tabCerti")) { setCertiMed(); }
                else if (TABVSACTIVE.Equals(tabOrderNew.Name)) { setGrfOrder(); }
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
        private void setStaffNote(String vsDate, String preno, ref C1PictureBox picL, ref C1PictureBox picR)
        {
            String file = "", dd = "", mm = "", yy = "", err = "";
            picL.Image = null;
            picR.Image = null;
            picR.Dock = DockStyle.Fill;
            picL.Dock = DockStyle.Fill;
            picR.SizeMode = PictureBoxSizeMode.StretchImage;
            picL.SizeMode = PictureBoxSizeMode.StretchImage;
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
                    //picL.Image = Image.FromFile(file + preno1 + "R.JPG");     //หมอแจ้งว่า สลับกันกับโปรแกรมเก่า หมอผู้หญิง
                    //picR.Image = Image.FromFile(file + preno1 + "S.JPG");     //หมอแจ้งว่า สลับกันกับโปรแกรมเก่า หมอผู้หญิง
                    picL.Image = Image.FromFile(file + preno1 + "S.JPG");
                    picR.Image = Image.FromFile(file + preno1 + "R.JPG");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("ไม่พบ StaffNote ในระบบ " + ex.Message, "");
                    //lfSbStatus.Text = ex.Message.ToString();
                    lfSbMessage.Text = err + " setStaffNote " + ex.Message;
                    new LogWriter("e", this.Name + " setStaffNote " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setStaffNote", ex.Message);
                }
            }
        }
        private void setGrfVS()
        {
            //new LogWriter("d", this.Name+" setGrfVS " );
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectVisitByHn3(txtPttHN.Text);
            grfVS.Rows.Count = 1; grfVS.Rows.Count = dt.Rows.Count+1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfVS.Rows[i];
                String status = "", vn = "", an="";
                status = row1["MNC_PAT_FLAG"].ToString();
                vn = row1["MNC_VN_NO"].ToString() + "." + row1["MNC_VN_SEQ"].ToString() + "." + row1["MNC_VN_SUM"].ToString();
                rowa[colVsVsDate] = bc.datetoShowShort(row1["mnc_date"].ToString());
                rowa[colVsVnShow] = status.Equals("I") ? row1["mnc_an_no"].ToString() + "." + row1["mnc_an_yr"].ToString(): vn;
                rowa[colVsStatus] = status;
                rowa[colVsPreno] = row1["mnc_pre_no"].ToString();
                rowa[colVsDept] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colVsAn] = row1["mnc_an_no"].ToString() + "." + row1["mnc_an_yr"].ToString();
                rowa[colVsAndate] = bc.datetoShow1(row1["mnc_ad_date"].ToString());
                rowa[colVsPaidType] = row1["MNC_FN_TYP_DSC"].ToString();

                rowa[colVsVsDate1] = row1["mnc_date"].ToString();
                rowa[colVsDtrName] = row1["dtr_name"].ToString();
                rowa[colVsVn] = vn;
                rowa[colVsDeptName] = row1["MNC_MD_DEP_DSC"].ToString();
                rowa[colVsVsStatus] = row1["MNC_STS"].ToString();
                if (status.Equals("I")){rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#CCCCFF"); }
                i++;
            }
        }
        private void initGrfLab(ref C1FlexGrid grf, ref Panel pn)
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
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("Compare LAB", new EventHandler(ContextMenu_Compare_Lab));
            grfLab.ContextMenu = menuGw;

            grfLab.Cols[colLabName].AllowEditing = false;
            grfLab.Cols[colInterpret].AllowEditing = false;
            grfLab.Cols[colNormal].AllowEditing = false;

            pnLab.Controls.Add(grfLab);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfLab, "Office2010Barbie");
        }
        private void ContextMenu_Compare_Lab(object sender, System.EventArgs e)
        {
            FrmLabCompare frm;
            if (!grfVS[grfVS.Row, colVsStatus].ToString().Equals("I"))
            {
                frm = new FrmLabCompare(bc, txtPttHN.Text, VSDATE, PRENO, "");
            }
            else
            {
                String an = "";
                an = grfVS[grfVS.Row, colVsAn].ToString();
                frm = new FrmLabCompare(bc, txtPttHN.Text, "", "", an);
            }
            frm.ShowDialog(this);
            //frm.Show(this);
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
            grf.Rows.Count = 1; grf.Rows.Count = dt.Rows.Count + 1;
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
        private void initGrfXray(ref C1FlexGrid grf, ref Panel pn)
        {
            grfXray = new C1FlexGrid();
            grfXray.Font = fEdit;
            grfXray.Dock = System.Windows.Forms.DockStyle.Fill;
            grfXray.Location = new System.Drawing.Point(0, 0);
            grfXray.Cols.Count = 5;
            grfXray.Cols[colXrayDate].Caption = "วันที่สั่ง";
            grfXray.Cols[colXrayName].Caption = "ชื่อX-Ray";
            grfXray.Cols[colXrayCode].Caption = "Code X-Ray";
            //grfXray.Cols[colXrayResult].Caption = "ผล X-Ray";

            grfXray.Cols[colXrayDate].Width = 100;
            grfXray.Cols[colXrayName].Width = 250;
            grfXray.Cols[colXrayCode].Width = 100;
            grfXray.Cols[colXrayResult].Width = 200;

            grfXray.Cols[colXrayDate].AllowEditing = false;
            grfXray.Cols[colXrayName].AllowEditing = false;
            grfXray.Cols[colXrayCode].AllowEditing = false;
            grfXray.Cols[colXrayResult].AllowEditing = false;
            ContextMenu menuGwX = new ContextMenu();
            menuGwX.MenuItems.Add("เปิด PACs infinitt", new EventHandler(ContextMenu_xray_infinitt));

            grfXray.ContextMenu = menuGwX;

            grfXray.Name = "grfXray";
            grfXray.Rows.Count = 1;
            pnXray.Controls.Add(grfXray);

            theme1.SetTheme(grfXray, "MacBlue");
        }
        private void ContextMenu_xray_infinitt(object sender, System.EventArgs e)
        {
            //if (grfXray == null) return;
            //if (grfXray.Row <= 1) return;
            //if (grfXray.Col <= 0) return;
            String address = "";

            address = "http://172.25.10.9/pkg_pacs/external_interface.aspx?TYPE=W&LID=itadmin&LPW=itadmin&PID=" + txtPttHN.Text.Trim();
            System.Diagnostics.Process.Start("iexplore", address);
        }
        private void setGrfXray(String hn, String vsDate, String preno, ref C1FlexGrid grf)
        {
            DataTable dt = new DataTable();
            String vn = "", vsdate = "", an = "";
            dt = bc.bcDB.vsDB.selectResultXraybyVN1(hn, preno, vsDate);
            grf.Rows.Count = 1; grf.Rows.Count = dt.Rows.Count + 1;
            int i = 0;
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
        private void initGrfOrder()
        {
            grfOrder = new C1FlexGrid();
            grfOrder.Font = fEdit;
            grfOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOrder.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grfOrder.Cols[colDrugOrderId].Visible = false;
            grfOrder.Rows.Count = 1;
            grfOrder.Cols.Count = 8;
            grfOrder.Cols[colDrugName].Caption = "Drug Name";
            grfOrder.Cols[colDrugOrderMed].Caption = "MED";
            grfOrder.Cols[colDrugOrderQty].Caption = "QTY";
            grfOrder.Cols[colDrugOrderDate].Caption = "Date";
            grfOrder.Cols[colDrugOrderFre].Caption = "วิธีใช้";
            grfOrder.Cols[colDrugOrderIn1].Caption = "ข้อควรระวัง";
            grfOrder.Cols[colDrugName].Width = 400;
            grfOrder.Cols[colDrugOrderMed].Width = 200;
            grfOrder.Cols[colDrugOrderQty].Width = 60;
            grfOrder.Cols[colDrugOrderDate].Width = 90;
            grfOrder.Cols[colDrugOrderFre].Width = 500;
            grfOrder.Cols[colDrugOrderIn1].Width = 350;
            grfOrder.Cols[colDrugName].AllowEditing = false;
            grfOrder.Cols[colDrugOrderQty].AllowEditing = false;
            grfOrder.Cols[colDrugOrderMed].AllowEditing = false;
            grfOrder.Cols[colDrugOrderFre].AllowEditing = false;
            grfOrder.Cols[colDrugOrderIn1].AllowEditing = false;
            grfOrder.Cols[colDrugOrderDate].AllowEditing = false;
            grfOrder.Name = "grfOrder";
            pnDrug.Controls.Add(grfOrder);
            theme1.SetTheme(grfOrder, "ExpressionLight");
            //theme1.SetTheme(grfOrder, "VS2013Red");
        }
        private void setGrfOrder(String hn, String vsDate, String preno, ref C1FlexGrid grf)
        {
            DataTable dtOrder = new DataTable();
            dtOrder = bc.bcDB.vsDB.selectDrugOPD(hn, preno, vsDate);
            grf.Rows.Count = 1; grf.Rows.Count = dtOrder.Rows.Count + 1;
            int i = 0;
            decimal aaa = 0;
            foreach (DataRow row1 in dtOrder.Rows)
            {
                i++;
                Row rowa = grf.Rows[i];
                rowa[colDrugName] = row1["MNC_PH_TN"].ToString();
                rowa[colDrugOrderMed] = "";
                rowa[colDrugOrderQty] = row1["qty"].ToString();
                rowa[colDrugOrderDate] = bc.datetoShow(row1["mnc_req_dat"]);
                rowa[colDrugOrderFre] = row1["MNC_PH_DIR_DSC"].ToString();
                rowa[colDrugOrderIn1] = row1["MNC_PH_CAU_dsc"].ToString();
                //row1[0] = (i - 2);
            }
        }
        private void initGrfProcedure()
        {
            grfProcedure = new C1FlexGrid();
            grfProcedure.Font = fEdit;
            grfProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
            grfProcedure.Location = new System.Drawing.Point(0, 0);
            grfProcedure.Rows.Count = 1;
            grfProcedure.Cols.Count = 5;
            grfProcedure.Cols[colProcCode].Width = 100;
            grfProcedure.Cols[colProcName].Width = 200;
            grfProcedure.Cols[colProcReqDate].Width = 100;
            grfProcedure.Cols[colProcReqTime].Width = 200;

            grfProcedure.ShowCursor = true;
            grfProcedure.Cols[colProcCode].Caption = "CODE";
            grfProcedure.Cols[colProcName].Caption = "Procedure Name";
            grfProcedure.Cols[colProcReqDate].Caption = "req date";
            grfProcedure.Cols[colProcReqTime].Caption = "req time";

            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grfProcedure.Cols[colProcCode].DataType = typeof(String);
            grfProcedure.Cols[colProcName].DataType = typeof(String);
            grfProcedure.Cols[colProcReqDate].DataType = typeof(String);
            grfProcedure.Cols[colProcReqTime].DataType = typeof(String);

            grfProcedure.Cols[colProcCode].TextAlign = TextAlignEnum.CenterCenter;
            grfProcedure.Cols[colProcName].TextAlign = TextAlignEnum.LeftCenter;
            grfProcedure.Cols[colProcReqDate].TextAlign = TextAlignEnum.CenterCenter;
            grfProcedure.Cols[colProcReqTime].TextAlign = TextAlignEnum.CenterCenter;

            grfProcedure.Cols[colProcCode].Visible = true;
            grfProcedure.Cols[colProcName].Visible = true;
            grfProcedure.Cols[colProcReqTime].Visible = false;

            grfProcedure.Cols[colProcCode].AllowEditing = false;
            grfProcedure.Cols[colProcName].AllowEditing = false;
            grfProcedure.Cols[colProcReqDate].AllowEditing = false;
            grfProcedure.Cols[colProcReqTime].AllowEditing = false;

            pnProcedure.Controls.Add(grfProcedure);
            theme1.SetTheme(grfProcedure, "Office2010Red");
        }
        private void setGrfProcedure(String hn, String vsDate, String preno, ref C1FlexGrid grf)
        {
            DataTable dt = new DataTable();
            String vn = "", vsdate = "", an = "";
            dt = bc.bcDB.pt16DB.SelectProcedureByVisit(hn, preno, vsDate);
            grf.Rows.Count = 1; grf.Rows.Count = dt.Rows.Count + 1;
            int i = 0;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    Row rowa = grf.Rows[i];
                    //rowa[colHisProcReqTime] = row1["MNC_REQ_TIM"].ToString();
                    rowa[colProcReqDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
                    rowa[colProcName] = row1["MNC_SR_DSC"].ToString();
                    rowa[colProcCode] = row1["MNC_SR_CD"].ToString();
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
        private void setControlPnPateint()
        {
            //PTT = bc.bcDB.pttDB.selectPatinetByHn(this.HN);
            if(PTT==null) return;
            txtPttHN.Value = PTT.Hn;            lbPttNameT.Text = PTT.Name;            HN = PTT.Hn;
            setGrfVS();            setGrfDrugAllergy();            setGrfChronic();            /*setGrfOutLab();        setGrfPttApm();  comment เพราะหมอแจ้งว่าช้า เอาไปไว้ที่ TabVS_SelectedTabChanged */
            if (grfVS.Rows.Count>2) grfVS.Select(1, 1);

            TABVSACTIVE = tabStaffNote.Name;
            tabVS.SelectedTab = tabStaffNote;
            TabVS_SelectedTabChanged(null, null);
            lbVN.Text = "...";
            if (grfVS.Rows.Count > 1)
            {
                PRENO = grfVS[grfVS.Row, colVsPreno] != null ? grfVS[grfVS.Row, colVsPreno].ToString() : "";
                VSDATE = grfVS[grfVS.Row, colVsVsDate1] != null ? grfVS[grfVS.Row, colVsVsDate1].ToString() : "";
                setStaffNote(VSDATE, PRENO, ref picL, ref picR);
                tabVS.TabPages[tabOrderNew.Name].TabVisible = (grfVS[grfVS.Row, colVsVsStatus].ToString().Equals("F")) ? false : true;
            }
            lbPttAttachNote.Text = PTT.MNC_ATT_NOTE.Length <= 0 ? "..." : PTT.MNC_ATT_NOTE;
            lbPttFinNote.Text = PTT.MNC_FIN_NOTE.Length<=0 ? "..." : PTT.MNC_FIN_NOTE;
            lbPttAge.Text = "age : "+PTT.AgeStringOK1DOT();
            lfSbComp.Text = "comp("+PTT.comNameT+")";
            lfSbInsur.Text = "insur["+PTT.insurNameT+"]";
            rgSbHIV.Text = PTT.statusHIV.Length > 0 ? "(HP)" : "";
            rgSbHIV.ToolTip = PTT.statusHIV;
            rgSbAFB.Text = PTT.statusAFB.Length > 0 ? "[AF]" : "";
            rgSbAFB.ToolTip = PTT.statusAFB;
            
            grfVS.Focus();
        }
        private void clearControlPnPateint()
        {
            txtPttHN.Value = "";
            lbPttNameT.Text = "";
        }
        private void showLbLoading()
        {
            lbLoading.Show();
            lbLoading.BringToFront();
            Application.DoEvents();
        }
        private void setLbLoading(String txt)
        {
            lbLoading.Text = txt;
            Application.DoEvents();
        }
        private void hideLbLoading()
        {
            lbLoading.Hide();
            Application.DoEvents();
        }
        private void setControlTabOrderNew(String flag)
        {
            clearControlOrder();
            if(flag.Equals("drug")) chkItemDrug.Checked = true;
            else if (flag.Equals("lab")) chkItemLab.Checked = true;
            else if (flag.Equals("xray")) chkItemXray.Checked = true;
            else if (flag.Equals("procedure")) chkItemProcedure.Checked = true;
            txtSearchItem.SelectAll();
            txtSearchItem.Focus();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // ...
            if (keyData == (Keys.Escape))
            {
                if (chlApmList.Visible) { chlApmList.Hide(); }
            }
            else if (keyData == (Keys.Home))
            {
                pnVs.Focus();
                grfVS.Focus();
            }
            else if (keyData == (Keys.F1)) {tabVS.SelectedTab = tabOrderNew; }
            else if (keyData == (Keys.F2))
            {
                tabVS.SelectedTab = tabOrderNew;
                setControlTabOrderNew("lab");
            }
            else if (keyData == (Keys.F3))
            {
                tabVS.SelectedTab = tabOrderNew;
                setControlTabOrderNew("xray");
            }
            else if (keyData == (Keys.F4))
            {
                tabVS.SelectedTab = tabOrderNew;
                setControlTabOrderNew("procedure");
            }
            else if (keyData == (Keys.F5))
            {
                tabVS.SelectedTab = tabOrderNew;
                setControlTabOrderNew("drug");
            }
            else if (keyData == (Keys.F6))
            {
                tabVS.SelectedTab = tabOrderNew;
                clearControlOrder();
                chkItemDrug.Checked = true;
                cboDrugSetName.SelectAll();
                cboDrugSetName.Focus();
            }
            else if (keyData == (Keys.F7)){tabVS.SelectedTab = tabStaffNote;}
            else if (keyData == (Keys.F8)) {tabVS.SelectedTab = tabOrder; }
            else if (keyData == (Keys.F9)) {tabVS.SelectedTab = tabApm;   }
            else if (keyData == (Keys.F10)){tabVS.SelectedTab = tabMedScan;}
            else if (keyData == (Keys.F11)){tabVS.SelectedTab = tabOutLab;}
            else if (keyData == (Keys.F12)){tabVS.SelectedTab = tabCerti;}
            else if (keyData == (Keys.End))
            {

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void FrmPatient_Load(object sender, EventArgs e)
        {
            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            DEPTNO = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
            String stationname = bc.bcDB.pm32DB.getDeptName(bc.iniC.station);
            spMain.HeaderHeight = 0;
            spPtt2.HeaderHeight = 20;
            spScan.HeaderHeight = 0;
            scApm.HeaderHeight = 0;
            spOrder.HeaderHeight = 0;

            lfSbStation.Text = DEPTNO + "[" + bc.iniC.station + "]" + stationname;
            rgSbModule.Text = bc.iniC.hostDBMainHIS + " " + bc.iniC.nameDBMainHIS;
            this.Text = "Last Update 2024-07-26 บาง1link staffnote สลับหน้า ";
            lbVN.Left = lbPttAge.Left + 120;
            chkItemDrug.Checked = true;
            ChkItemDrug_Click(null, null);
            if (bc.iniC.programLoad.Equals("ScanView"))
            {
                txtPttHN.ReadOnly = false;
                txtPttHN.Focus();
            }
            else
            {
                txtPttHN.ReadOnly= true;
            }
        }
    }
}
