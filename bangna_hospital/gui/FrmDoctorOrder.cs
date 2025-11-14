using AutocompleteMenuNS;
using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using bangna_hospital.services;
using C1.C1Pdf;
using C1.Win.C1Document;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SplitContainer;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using C1.Win.FlexViewer;
using Microsoft.Web.WebView2.WinForms;
//using GrapeCity.ActiveReports.Document.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Button = System.Windows.Forms.Button;
using Font = System.Drawing.Font;
using Image = System.Drawing.Image;
using Item = bangna_hospital.object1.Item;
using Row = C1.Win.C1FlexGrid.Row;
using Task = System.Threading.Tasks.Task;
using TextBox = System.Windows.Forms.TextBox;
namespace bangna_hospital.gui
{
    public partial class FrmDoctorOrder : Form
    {
        BangnaControl BC;
        Font fEdit, fEditB, fEdit3B, fEdit5B, fPrnBil, famtB, fEditS, famtB30, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2, fPDFs4;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1FlexGrid grfVS, grfLab, grfOrder, grfXray, grfProcedure, grfDrugSet, grfDrugAllergy, grfChronic, grfVital;
        //C1PdfDocument pdfDoc;
        C1ThemeController theme1;
        Label lbLoading;
        Patient PTT;
        Visit VS;
        AutoCompleteStringCollection acmApmTime, autoApm, AUTOFRE, AUTOPROPER;
        AutocompleteMenu AUTOChief, AUTOPyhsical, AUTODIAG, AUTODTRADVICE;
        String PRENO = "", VSDATE = "", HN = "", DTRCODE="", PHYSICALEXAM="", PRESCRIPTIONID = "" ,LASTFOCUS="";
        int colDrugOrderId = 1, colDrugOrderDate = 2, colDrugName = 3, colDrugOrderQty = 4, colDrugOrderFre = 5, colDrugOrderIn1 = 6, colDrugOrderMed = 7;
        int colLabDate = 1, colLabName = 2, colLabNameSub = 3, colLabResult = 4, colInterpret = 5, colNormal = 6, colUnit = 7;
        int colXrayDate = 1, colXrayCode = 2, colXrayName = 3, colXrayResult = 4;
        int colProcCode = 1, colProcName = 2, colProcReqDate = 3, colProcReqTime = 4;
        int colgrfOrderCode = 1, colgrfOrderName = 2, colgrfOrderStatus = 3, colgrfOrderQty = 4, colgrfOrderDrugUsing=5, colgrfOrderDrugFre = 6, colgrfOrderDrugPrecau = 7, colgrfOrderDrugIndica = 8, colgrfOrderDrugProper = 9, colgrfOrderDrugInterac = 10, colgrfOrderDrugRemark = 11, colgrfOrderID = 12, colgrfOrderReqNO = 13, colgrfOrderReqDate = 14, colgrfOrdFlagSave = 15, colgrfOrderGeneric=16, colgrfOrderThai=17;
        int colgrfDrugSetItemCode = 1, colgrfDrugSetItemName = 2, colgrfDrugSetItemQty = 3, colgrfDrugSetUsing = 4, colgrfDrugSetFreq = 5, colgrfDrugSetPrecau = 6, colgrfDrugSetProper = 7, colgrfDrugSetInterac = 8, colgrfDrugSetItemStatus = 9, colgrfDrugSetID = 10, colgrfDrugSetFlagSave = 11, colgrfDrugSetIndica = 12;
        int colgrfWeight=1, colgrfHeight = 2, colgrfTemp = 5, colgrfBSA = 3, colgrfBMI = 4, colgrfPulse = 6, colgrfRespiratory = 7, colgrfSystolic = 8, colgrfDiastolic = 9, colgrfMeanBP = 10, colgrfO2Sat = 11, colgrfGlucometer = 12;
        int rowindexgrfVS;
        Boolean isLoad = false,_isInitialized = false;
        C1SplitContainer scAI;
        C1SplitterPanel spClaude, spGemeni;
        UCMedicalDrawingForm drawingForm;
        UCRicherTextBox rtbPhysical1;
        UCAppointment ucApm;
        UCVitalsign uCVitalsign;
        private ClaudeApiClient _claudeClient;
        WebView2 wvClaude, wvGemeni;
        Button btnClaudeSend, btnGemeniSend;
        C1TextBox rtbCheif, rtbPhysical, rtbDiag, rtbDtrAdvice, txtClaude, txtGemeni;
        Image imgPaidTrue, imgPaidFalse;
        DataTable DTDRUG;
        Panel pnSubmit, pnVitalSignUC;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern IntPtr GetFocus();
        public FrmDoctorOrder(BangnaControl bc, String dtrcode, String hn, String vsdate, String preno, Patient ptt)
        {
            this.SuspendLayout();
            // To reduce flickering, enable double buffering for the Form and key Panels/Controls.
            // Add this code to the constructor after InitializeComponent(), and for any custom panels.
            //this.SuspendLayout();
            
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            //this.UpdateStyles();
            //this.DoubleBuffered = true; // For Form
            InitializeComponent();
            // If you have custom panels, set DoubleBuffered for them as well:
            //SetDoubleBuffered(pnDrugAllergy);
            //SetDoubleBuffered(pnChronic);
            //SetDoubleBuffered(pnDrugSet);
            //SetDoubleBuffered(pnChief);
            //SetDoubleBuffered(pnPhysical);
            //SetDoubleBuffered(pnDiag);
            scMain.Hide();
            //this.Hide();
            this.DoubleBuffered = true;
            this.BC = bc;
            this.DTRCODE = dtrcode;
            this.HN = hn;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            this.PTT = ptt;
            initConfig();
        }
        // Utility method to set DoubleBuffered for Panels
        private void SetDoubleBuffered(Control control)
        {
            if (SystemInformation.TerminalServerSession) return;
            var prop = control.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (prop != null)
            {
                prop.SetValue(control, true, null);
            }
            //InitializeComponent();
            //this.ResumeLayout();
        }
        private void initConfig()
        {
            isLoad = true;
            initFont();
            initControl();
            initControlTabAI();
            setEvent();
            setControlPnPateint();
            pnSubmit = new Panel();            pnSubmit.Dock = DockStyle.Fill;            pnSubmit.Name = "pnSubmit";
            isLoad = false;
        }
        private void initFont()
        {
            fEdit = new Font(BC.iniC.grdViewFontName, BC.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(BC.iniC.grdViewFontName, BC.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new Font(BC.iniC.grdViewFontName, BC.grdViewFontSize + 3, FontStyle.Bold);
            fEdit5B = new Font(BC.iniC.grdViewFontName, BC.grdViewFontSize + 5, FontStyle.Bold);
            fPrnBil = new Font(BC.iniC.pdfFontName, BC.pdfFontSize, FontStyle.Regular);
            famtB = new Font(BC.iniC.pdfFontName, BC.pdfFontSize + 7, FontStyle.Bold);
            fEditS = new Font(BC.iniC.pdfFontName, BC.pdfFontSize - 2, FontStyle.Regular);
            famtB30 = new Font(BC.iniC.pdfFontName, BC.pdfFontSize + 30, FontStyle.Bold);
            fPDF = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize, FontStyle.Regular);
            fPDFs2 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 2, FontStyle.Regular);
            fPDFs4 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 4, FontStyle.Regular);
            fPDFs6 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 6, FontStyle.Regular);
            fPDFl2 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 2, FontStyle.Regular);
            fPDFs6 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 6, FontStyle.Regular);
            fPDFs8 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 8, FontStyle.Regular);
        }
        private void initControl()
        {
            theme1 = new C1ThemeController();
            BC.bcDB.pharM01DB.setAUTODrug();
            BC.bcDB.pm30DB.setAUTOProcedure();
            BC.bcDB.labM01DB.setAUTOLab();
            BC.bcDB.xrayM01DB.setAUTOXray();
            BC.bcDB.pharM01DB.setAUTOUsingDesc();
            BC.bcDB.pharM01DB.setAUTOFrequency();
            BC.bcDB.pharM01DB.getAUTOPrecautionDesc();
            BC.bcDB.pharM01DB.setAUTOIndicationDesc();
            //AUTOLab = new AutoCompleteStringCollection();
            //AUTOXray = new AutoCompleteStringCollection();
            //AUTOProcedure = new AutoCompleteStringCollection();
            //AUTOUSING = new AutoCompleteStringCollection();
            //AUTOFRE = new AutoCompleteStringCollection();
            //AUTOINDICA = new AutoCompleteStringCollection();
            //AUTOCUA = new AutoCompleteStringCollection();
            //AUTOPROPER = new AutoCompleteStringCollection();
            //AUTODrug = await Task.Run(() => BC.bcDB.pharM01DB.getlDrugAllTherd());

            //AUTOLab = BC.bcDB.labM01DB.getlLabAll();
            //AUTOXray = BC.bcDB.xrayM01DB.getlLabAll();
            //AUTOProcedure = BC.bcDB.pm30DB.getlProcedureAll();

            //AUTOUSING = BC.bcDB.pharM01DB.getAUTOUsingDesc();
            //AUTOFRE = BC.bcDB.pharM01DB.getAUTOFrequencyDesc();
            //AUTOCUA = BC.bcDB.pharM01DB.getAUTOPrecautionDesc();
            //AUTOINDICA = BC.bcDB.pharM01DB.setAUTOIndicationDesc();
            //AUTOPROPER = BC.bcDB.pharM01DB.getAUTOPropertiesDesc();

            //AUTOUSING1 = BC.bcDB.pharM01DB.getAUTOUsing();
            //AUTOFRE1 = BC.bcDB.pharM01DB.setAUTOFrequency();
            //AUTOCUA1 = BC.bcDB.pharM01DB.getAUTOPrecaution();
            //AUTOINDICA1 = BC.bcDB.pharM01DB.getAUTOIndication();
            //AUTOPROPER1 = BC.bcDB.pharM01DB.getAUTOProperties();

            BC.bcDB.drugSetDB.setCboDrugSet(cboDrugSetName, DTRCODE, "");
            pnDrugSet.Hide();

            initGrfDrugAllergy();
            initGrfChronic();
            initGrfDrugSet();
            initLoading();
            initRichTextChief();
            initClaudeClient();
            //initRichTextPhysical();
            rtbPhysical1 = new UCRicherTextBox(BC, DTRCODE, HN, VSDATE, PRENO, PTT, "doctor_order_physical_exam", PHYSICALEXAM);
            rtbPhysical1.Dock = DockStyle.Fill;
            pnPhysicalDesc.Controls.Add(rtbPhysical1);

            initRichTextDiag();
            initRichTextDtrAdvice();

            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = BC.bcDB.pharM01DB.AUTODrugTR;

            txtGeneric.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGeneric.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGeneric.AutoCompleteCustomSource = BC.bcDB.pharM01DB.AUTODrugGN;

            txtUsing.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtUsing.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtUsing.AutoCompleteCustomSource = BC.bcDB.pharM01DB.AUTOUSING;
            txtFrequency.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFrequency.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFrequency.AutoCompleteCustomSource = BC.bcDB.pharM01DB.AUTOFRE;
            txtPrecautions.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPrecautions.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPrecautions.AutoCompleteCustomSource = BC.bcDB.pharM01DB.AUTOCAU;
            txtIndication.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtIndication.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtIndication.AutoCompleteCustomSource = BC.bcDB.pharM01DB.AUTOINDICA;
            txtProperties.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtProperties.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtProperties.AutoCompleteCustomSource = AUTOPROPER;

            txtUsing1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtUsing1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtUsing1.AutoCompleteCustomSource =BC.bcDB.pharM01DB.AUTOUSING1;
            txtFrequency1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFrequency1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFrequency1.AutoCompleteCustomSource = BC.bcDB.pharM01DB.AUTOFRE1;
            txtPrecautions1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPrecautions1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPrecautions1.AutoCompleteCustomSource = BC.bcDB.pharM01DB.AUTOCAU1;
            txtIndication1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtIndication1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtIndication1.AutoCompleteCustomSource = BC.bcDB.pharM01DB.AUTOINDICA1;
            txtProperties1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtProperties1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtProperties1.AutoCompleteCustomSource = BC.bcDB.pharM01DB.AUTOPROPER1;
            imgPaidTrue = Resources.red_checkmark_png_16;
            imgPaidFalse = Resources.cross_small_24;

            initGrfOrder(ref grfOrder, ref pnOrder, "grfOrder");
            // ในฟอร์มหลัก (เช่น FrmDoctorOrder หรือฟอร์มที่มี pnPhysicalDraw)
            drawingForm = new UCMedicalDrawingForm(BC, DTRCODE, HN, VSDATE, PRENO,PTT);
            drawingForm.Dock = DockStyle.Fill;
            pnPhysicalDraw.Controls.Add(drawingForm);
            pnVitalSign.Hide();     //ทดลอง UCVitalsign
            pnVitalSignUC = new Panel();
            pnVitalSignUC.Hide();
            pnVitalSignUC.Dock = DockStyle.Fill;
            this.c1SplitterPanel6.Controls.Add(pnVitalSignUC);
            uCVitalsign = new UCVitalsign(BC, DTRCODE, HN, VSDATE, PRENO, PTT, VS, "doctor_order", ref lfSbMessage);
            //uCVitalsign.Hide();
            uCVitalsign.Dock = DockStyle.Fill;
            pnVitalSignUC.Controls.Add(uCVitalsign);
            //drawingForm.DrawingBoxLoad();
        }
        private void initClaudeClient()
        {
            String claudekey = BC.iniC.CLAUDEAPI_KEY;       //ใช้ใน ini file เพราะ ถ้าเก็บใน Properties เวลา up to github จะเห็น key claude จะยกเลิก key
            if ((claudekey == null) || (claudekey.Length <= 0))
            {
                lfSbMessage.Text = "ไม่พบ Key API ของ Claude";
                new LogWriter("e", this.Name + " BtnClaudeSend_Click " + lfSbMessage.Text);
                BC.bcDB.insertLogPage(BC.userId, this.Name, "BtnClaudeSend_Click ", lfSbMessage.Text);
                return;
            }
            var config = new ClaudeConfig
            {
                //ApiKey = Environment.GetEnvironmentVariable("CLAUDE_API_KEY"),
                //ApiKey = Properties.Settings.Default.CLAUDEAPI_KEY,
                ApiKey = claudekey,
                // Model ที่จะใช้
                DefaultModel = Properties.Settings.Default.CLAUDEAPI_MODEL,
                DefaultMaxTokens = 2048,
                EnableLogging = true
            };

            _claudeClient = new ClaudeApiClient(config);
        }
        private void setEvent()
        {
            chkItemLab.Click += ChkItemLab_Click;
            chkItemXray.Click += ChkItemXray_Click;
            chkItemProcedure.Click += ChkItemHotC_Click;
            chkItemDrug.Click += ChkItemDrug_Click;
            
            txtSearchItem.KeyUp += TxtSearchItem_KeyUp;
            txtSearchItem.Enter += TxtSearchItem_Enter;
            txtGeneric.KeyUp += TxtGeneric_KeyUp;
            txtItemCode.KeyUp += TxtItemCode_KeyUp;
            txtItemQTY.KeyUp += TxtItemQTY_KeyUp;

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
            btnOrderSave.Click += BtnOrderSave_Click;
            btnOrderBeforeSubmit.Click += BtnOrderBeforeSubmit;
            btnOperItemSearch.Click += BtnOperItemSearch_Click;
            btnItemAdd.Click += BtnItemAdd_Click;
            btnPrnStaffNote.Click += BtnPrnStaffNote_Click;
            btnDrugSetAll.Click += BtnDrugSetAll_Click;
            cboDrugSetName.SelectedItemChanged += CboDrugSetName_SelectedItemChanged;

            txtUsing.KeyUp += TxtUsing_KeyUp;
            tabMain.SelectedTabChanged += TabMain_SelectedTabChanged;
            btnBack.Click += BtnBack_Click;
            btnSubmit.Click += BtnSubmit_Click;
            chkDrugSet.Click += ChkDrugSet_Click;
            chkDrugOld.Click += ChkDrugOld_Click;
            this.FormClosing += FrmDoctorOrder_FormClosing;
        }

        private void FrmDoctorOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            //throw new NotImplementedException();
            //_claudeClient?.Dispose();
            //base.OnFormClosing(e);
        }

        private void ChkDrugOld_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlChkDrugSet();
        }
        private void ChkDrugSet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlChkDrugSet();
        }
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            submitOrder();
        }
        private void submitOrder()
        {
            this.SuspendLayout();
            FrmPasswordConfirm frm = new FrmPasswordConfirm(BC);
            frm.ShowDialog();
            if (BC.USERCONFIRMID.Length <= 0)
            {
                lfSbMessage.Text = "Password ไม่ถูกต้อง";
                return;
            }
            showLbLoading();
            String re = "";
            //insert ordre ใช้ procedure insertOrder
            re = BC.bcDB.vsDB.insertOrder(txtPttHN.Text.Trim(), VSDATE, PRENO, DTRCODE, BC.USERCONFIRMID);
            if (re.Length > 0)
            {
                String[] reqno = re.Split('#');
                if (reqno.Length > 2)
                {
                    MedicalPrescription mp = BC.bcDB.mpDB.selectByHn(HN, PRENO, VSDATE);
                    PRESCRIPTIONID = mp.prescription_id;
                    String re1 = BC.bcDB.vsDB.updateStatusCloseVisit(HN, PRENO, VSDATE, BC.USERCONFIRMID);
                    DTDRUG = new DataTable();
                    DataTable dtlab = new DataTable();
                    DataTable dtxray = new DataTable();
                    DataTable dtprocedure = new DataTable();
                    DTDRUG = BC.bcDB.pharT06DB.selectbyHNReqNo(txtPttHN.Text.Trim(), reqno[4], reqno[3]);
                    dtlab = BC.bcDB.labT02DB.selectbyHNReqNo(txtPttHN.Text.Trim(), reqno[4], reqno[0]);
                    dtxray = BC.bcDB.xrayT02DB.selectbyHNReqNo(txtPttHN.Text.Trim(), reqno[4], reqno[1]);
                    dtprocedure = BC.bcDB.pt16DB.selectbyHNReqNo(txtPttHN.Text.Trim(), reqno[4], reqno[2]);
                    dtlab.Merge(DTDRUG);
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
                            new LogWriter("e", "FrmDoctorOrder BtnOrderSubmit_Click " + ex.Message);
                            BC.bcDB.insertLogPage(BC.userId, this.Name, "BtnOrderSubmit_Click ", ex.Message);
                        }
                    }
                }
            }
            hideLbLoading();
            this.ResumeLayout();
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.SuspendLayout();
            pnSubmit.Visible = false;
            tabMain.Visible = true;
            this.ResumeLayout();
        }
        private void TabMain_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tabMain.SelectedTab == tabOrderDrug)
            {
                txtSearchItem.SelectAll();
                txtSearchItem.Focus();
            }            
            else if (tabMain.SelectedTab == tabDiag)
            {
                DataTable dt = new DataTable();
                dt = BC.bcDB.vsDB.selectChiefCompliant(HN, VSDATE, PRENO);
                if (dt.Rows.Count > 0)
                {
                    rtbCheif.Value = dt.Rows[0]["chief_compliant"].ToString();
                    rtbPhysical1.Rtf = dt.Rows[0]["physical_exam"].ToString();
                    rtbDiag.Value = dt.Rows[0]["diagnosis"].ToString();
                    rtbDtrAdvice.Value = dt.Rows[0]["doctor_advice"].ToString();
                    PHYSICALEXAM = dt.Rows[0]["physical_exam"].ToString();
                }
                drawingForm.DrawingBoxLoad();
                rtbCheif.Select();
                rtbCheif.Focus();
            }
        }
        private void TxtUsing_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(grfOrder.Row.ToString(), out int rowa))
                {
                    if (rowa > 0)
                    {
                        Row row = grfOrder.Rows[rowa];
                        row[colgrfOrderDrugUsing] = txtUsing.Text.Trim();
                    }
                    else
                    {
                        lfSbMessage.Text = "กรุณาเลือกแถวที่ต้องการแก้ไข";
                    }
                }
                else
                {
                    lfSbMessage.Text = "กรุณาเลือกแถวที่ต้องการแก้ไข";
                }
                txtFrequency.SelectAll(); txtFrequency.Focus();
            }
        }
        private void BtnDrugSetAll_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (cboDrugSetName.SelectedItem == null) return;
            if (grfDrugSet.Rows.Count <= 1) return;
            pnDrugSet.Visible = false;
            try
            {
                foreach (Row arow in grfDrugSet.Rows)
                {
                    if (arow[colgrfDrugSetItemCode].ToString().Equals("code")) continue;
                    Row rowa = grfOrder.Rows.Add();
                    rowa[colgrfOrderCode] = arow[colgrfDrugSetItemCode].ToString();
                    rowa[colgrfOrderName] = arow[colgrfDrugSetItemName].ToString();
                    rowa[colgrfOrderQty] = arow[colgrfDrugSetItemQty].ToString();
                    rowa[colgrfOrderStatus] = "drug";
                    rowa[colgrfOrderDrugFre] = arow[colgrfDrugSetFreq].ToString();
                    rowa[colgrfOrderDrugPrecau] = arow[colgrfDrugSetPrecau].ToString();
                    rowa[colgrfOrderDrugInterac] = arow[colgrfDrugSetInterac].ToString();
                    rowa[colgrfOrderDrugIndica] = arow[colgrfDrugSetIndica].ToString();
                    //rowa[colgrfOrderDrugUsing] = arow[colgrfDrugSetus].ToString();
                    rowa[colgrfOrderID] = "";
                    rowa[colgrfOrderReqNO] = "";
                    rowa[colgrfOrdFlagSave] = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BtnDrugSetAll_Click " + ex.Message, "");
            }
            pnDrugSet.Visible=true;
        }
        private void CboDrugSetName_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (isLoad) return;
            if (cboDrugSetName.SelectedItem == null) return;
            pnDrugSet.Visible = true;
            setGrfDrugSet(((ComboBoxItem)(cboDrugSetName.SelectedItem)).Text);
        }
        private void initControlTabAI()
        {
            pnAI.SuspendLayout();
            scAI = new C1SplitContainer();
            scAI.Dock = DockStyle.Fill;
            scAI.Name = "scAI";
            scAI.SuspendLayout();
            spClaude = new C1SplitterPanel();
            spClaude.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            //spClaude.Width = (this.Width / 2) - 5;
            spClaude.SizeRatio = 50;
            spClaude.TabIndex = 0;
            spClaude.Name = "spClaude";
            spClaude.Text = "💬 Claude";
            spGemeni = new C1SplitterPanel();
            spGemeni.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            //spGemeni.Width = (this.Width / 2) - 5;
            spGemeni.SizeRatio = 50;
            spGemeni.TabIndex = 1;
            spGemeni.Name = "spGemeni";
            spGemeni.Text= "💬 Gemini";
            scAI.Panels.Add(spClaude);
            scAI.Panels.Add(spGemeni);
            scAI.HeaderHeight = 30;
            scAI.ResumeLayout();
            SplitContainer scclaude = new SplitContainer();
            scclaude.Dock = DockStyle.Fill;
            scclaude.Orientation = Orientation.Horizontal;
            
            scclaude.Panel1.BackColor = Color.LightBlue;
            scclaude.Name = "scclaude";
            spClaude.Controls.Add(scclaude);
            SplitContainer scgemeni = new SplitContainer();
            scgemeni.Dock = DockStyle.Fill;
            scgemeni.Orientation = Orientation.Horizontal;
            scgemeni.Panel1.BackColor = ColorTranslator.FromHtml("#FCA5A5");
            
            scgemeni.Name = "scgemeni";
            Panel pnclaudechat = new Panel();
            pnclaudechat.Dock = DockStyle.Fill;
            //pnclaudechat.Width = 400;
            txtClaude = new C1TextBox();
            txtClaude.Dock = DockStyle.Fill;
            txtClaude.Multiline = true;
            txtClaude.Font = fEdit;
            txtClaude.BorderStyle = BorderStyle.None;
            txtClaude.BackColor = ColorTranslator.FromHtml("#93C5FD");
            txtClaude.Name = "txtClaude";
            pnclaudechat.Controls.Add(txtClaude);
            Panel pnclaudeSend = new Panel();
            pnclaudeSend.Dock = DockStyle.Right;
            pnclaudeSend.Width = 100;
            //pnclaudeSend.Height = 150;
            btnClaudeSend = new Button();
            btnClaudeSend.Dock = DockStyle.Fill;
            btnClaudeSend.Font = fEditB;
            btnClaudeSend.Text = "Send";
            btnClaudeSend.Click += BtnClaudeSend_Click;
            pnclaudeSend.Controls.Add(btnClaudeSend);
            scclaude.Panel1.Controls.Add(pnclaudechat);
            scclaude.Panel1.Controls.Add(pnclaudeSend);
            spGemeni.Controls.Add(scgemeni);
            scclaude.SplitterDistance = 10;
            Panel pngemenichat = new Panel();
            pngemenichat.Dock = DockStyle.Fill;
            //pngemenichat.Width = 400;
            txtGemeni = new C1TextBox();
            txtGemeni.Dock = DockStyle.Fill;
            txtGemeni.Multiline = true;
            txtGemeni.Font = fEdit;
            txtGemeni.BorderStyle = BorderStyle.None;
            txtGemeni.BackColor = ColorTranslator.FromHtml("#FCA5A5");
            txtGemeni.Name = "txtGemeni";
            pngemenichat.Controls.Add(txtGemeni);
            Panel pngemeniSend = new Panel();
            pngemeniSend.Dock = DockStyle.Right;
            pngemeniSend.Width = 100;
            btnGemeniSend = new Button();
            btnGemeniSend.Dock = DockStyle.Fill;
            btnGemeniSend.Font = fEditB;
            btnGemeniSend.Text = "Send";
            btnGemeniSend.Click += BtnGemeniSend_Click;
            pngemeniSend.Controls.Add(btnGemeniSend);
            scgemeni.Panel1.Controls.Add(pngemenichat);
            scgemeni.Panel1.Controls.Add(pngemeniSend);
            scgemeni.SplitterDistance = 10;
            wvClaude = new WebView2();
            wvClaude.Dock = DockStyle.Fill;
            //wvClaude.Source = new Uri("https://chat.openai.com/chat");
            initialWvClaudeAsync();
            scclaude.Panel2.Controls.Add(wvClaude);
            wvGemeni = new WebView2();
            wvGemeni.Dock = DockStyle.Fill;
            //wvGemeni.Source = new Uri("https://gemini.google.com/");
            scgemeni.Panel2.Controls.Add(wvGemeni);
            //scclaude.Panel1.Height = 100;

            pnAI.Controls.Add(scAI);
            pnAI.ResumeLayout();
            _isInitialized = true;
        }
        private async void initialWvClaudeAsync()
        {
            await wvClaude.EnsureCoreWebView2Async();
            // Now it's safe to use wvClaude.ExecuteScriptAsync and other methods
            await wvClaude.EnsureCoreWebView2Async(null);
            // โหลด HTML file
            String filename = Path.Combine(Application.StartupPath, "claude-chat.html");
            if (File.Exists(filename))            {                wvClaude.CoreWebView2.Navigate(filename);            }
            else            {                lfSbMessage.Text = "ไม่พบไฟล์ " + filename;            }
            var hasFunction = await CheckJavaScriptFunction(wvClaude, "addAssistantMessage");
            btnClaudeSend.Enabled = true; // Enable send button if you disabled it initially
        }
        private void BtnGemeniSend_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private async void BtnClaudeSend_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //String claudekey = Environment.GetEnvironmentVariable("CLAUDEAPI_KEY");
            var startTime = DateTime.Now;
            btnClaudeSend.Enabled = false; btnClaudeSend.Text = "waiting";
            String claudekey = Properties.Settings.Default.CLAUDEAPI_KEY;
            if (claudekey == null)
            {
                lfSbMessage.Text ="ไม่พบ Key API ของ Claude";
                new LogWriter("e", this.Name+" BtnClaudeSend_Click "+ lfSbMessage.Text);
                BC.bcDB.insertLogPage(BC.userId, this.Name, "BtnClaudeSend_Click ", lfSbMessage.Text);
                return;
            }
            if (txtClaude.Text.Length <= 0) return;
            try
            {
                // เรียก Claude
                ClaudeRequest request = new ClaudeRequest
                {
                    MaxTokens = 2048, Temperature = 0.7, Model = Properties.Settings.Default.CLAUDEAPI_MODEL,
                    Messages = new List<ClaudeMessage>{new ClaudeMessage{Role = "user", Content = txtClaude.Text.Trim() }}
                };
                var response = await _claudeClient.SendRequestAsync(request);
                // แสดงข้อความ Claude
                if (wvClaude.CoreWebView2 == null) { Console.WriteLine("WebView2 not ready!");  return;  }
                // 3. ตรวจสอบ function
                //var hasFunction = await CheckJavaScriptFunction(wvClaude, "addAssistantMessage");
                await wvClaude.ExecuteScriptAsync($"addUserMessage('{EscapeJS(txtClaude.Text.Trim())}', '{DateTime.Now:HH:mm}')");
                await wvClaude.ExecuteScriptAsync($"addAssistantMessage('{EscapeJS(response.Text)}', '{DateTime.Now:HH:mm}')");
                var responseTime = (int)(DateTime.Now - startTime).TotalMilliseconds;
                ClaudeChatLogger _logger = new ClaudeChatLogger("claude_chat_logs.db");
                await _logger.LogChatAsync(
                    userMessage: txtClaude.Text.Trim(),
                    response: response,
                    model: Properties.Settings.Default.CLAUDEAPI_MODEL,
                    systemPrompt: DTRCODE,
                    responseTimeMs: responseTime,
                    status: "success",              //logตรงนี้ คือ success
                    errorMessage: ""                //ไม่มี error
                );
                txtClaude.Clear();
            }
            catch (Exception ex)
            {
                new LogWriter("e", this.Name + " BtnClaudeSend_Click " + ex.Message);
                await wvClaude.ExecuteScriptAsync($"addErrorMessage('{EscapeJS(ex.Message)}')");
                lfSbMessage.Text = ex.Message;
            }
            finally { btnClaudeSend.Enabled = true; btnClaudeSend.Text = "Send"; }
            //await DebugAll();
        }
        private string EscapeJS(string text)
        {
            return text.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n");
        }
        public static async Task DebugAll()
        {
            var config = new ClaudeConfig
            {
                ApiKey = Properties.Settings.Default.CLAUDEAPI_KEY,
                //BaseUrl = "https://api.anthropic.com/v1",  // ตรวจสอบ
                EnableLogging = true
            };

            //Console.WriteLine("=== Configuration ===");
            //Console.WriteLine($"ApiKey: {config.ApiKey.Substring(0, 20)}...");
            //Console.WriteLine($"BaseUrl: '{config.BaseUrl}'");
            //Console.WriteLine($"Length: {config.BaseUrl.Length}");
            //Console.WriteLine($"Ends with /: {config.BaseUrl.EndsWith("/")}");
            //Console.WriteLine($"Ends with /v1: {config.BaseUrl.EndsWith("/v1")}");
            //Console.WriteLine("=====================\n");

            // ทดสอบ URL combination
            //var uri = new Uri(new Uri(config.BaseUrl), "/messages");
            //Console.WriteLine($"Result URL: {uri}");
            //Console.WriteLine($"Should be: https://api.anthropic.com/v1/messages");
            //Console.WriteLine($"Match: {uri.ToString() == "https://api.anthropic.com/v1/messages"}");
            //Console.WriteLine();
            Fix401ApiKey.CheckApiKey(config.ApiKey);
            await Fix401ApiKey.TestApiKey(config.ApiKey);
            using (var client = new ClaudeApiClient(config))
            {
                try
                {
                    var response = await client.SendMessageAsync("Hi");
                    Console.WriteLine($"✅ Success! Response: {response.Text}");
                }
                catch (ClaudeApiException ex)
                {
                    Console.WriteLine($"❌ Status: {ex.StatusCode}");
                    Console.WriteLine($"❌ Message: {ex.Message}");
                }
            }
        }
        public async Task<bool> CheckJavaScriptFunction(WebView2 wvClaude, string functionName)
        {
            try
            {
                if (wvClaude.CoreWebView2 == null)
                {
                    Console.WriteLine("[ERROR] CoreWebView2 is null");
                    return false;
                }
                var script = $"typeof {functionName}";
                var result = await wvClaude.ExecuteScriptAsync(script);
                Console.WriteLine($"[DEBUG] typeof {functionName} = {result}");
                return result.Contains("function");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Check function failed: {ex.Message}");
                new LogWriter("e", this.Name + " CheckJavaScriptFunction " + ex.Message);
                return false;
            }
        }
        private void initGrfDrugSet()
        {
            grfDrugSet = new C1FlexGrid();
            grfDrugSet.Font = fEdit;
            grfDrugSet.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDrugSet.Location = new System.Drawing.Point(0, 0);
            grfDrugSet.Cols.Count = 13;
            grfDrugSet.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);
            grfDrugSet.Cols[colgrfDrugSetItemCode].Width = 70;
            grfDrugSet.Cols[colgrfDrugSetItemName].Width = 250;
            grfDrugSet.Cols[colgrfDrugSetFreq].Width = 250;
            grfDrugSet.Cols[colgrfDrugSetPrecau].Width = 250;
            grfDrugSet.Cols[colgrfDrugSetInterac].Width = 250;
            grfDrugSet.Cols[colgrfDrugSetIndica].Width = 250;

            grfDrugSet.Cols[colgrfDrugSetItemCode].Caption = "code";
            grfDrugSet.Cols[colgrfDrugSetItemName].Caption = "Name";
            grfDrugSet.Cols[colgrfDrugSetUsing].Caption = "วิธีใช้ ";
            grfDrugSet.Cols[colgrfDrugSetFreq].Caption = "ความถี่ในการใช้ยา";
            grfDrugSet.Cols[colgrfDrugSetPrecau].Caption = "ข้อควรระวัง";
            grfDrugSet.Cols[colgrfDrugSetInterac].Caption = "ปฎิกิริยาต่อยาอื่น";
            grfDrugSet.Cols[colgrfDrugSetIndica].Caption = "ข้อบ่งชี้";
            grfDrugSet.Cols[colgrfDrugSetProper].Caption = "สรรพคุณ";
            grfDrugSet.Cols[colgrfDrugSetID].Visible = false;
            grfDrugSet.Cols[colgrfDrugSetFlagSave].Visible = false;
            grfDrugSet.Cols[colgrfDrugSetItemStatus].Visible = false;
            grfDrugSet.Cols[colgrfDrugSetUsing].Visible = false;
            grfDrugSet.Cols[colgrfDrugSetItemCode].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetItemName].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetItemQty].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetFreq].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetPrecau].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetInterac].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetIndica].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetProper].AllowEditing = false;
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
            showLbLoading();
            DataTable dt = new DataTable();
            dt = BC.bcDB.drugSetDB.selectDrugSet(DTRCODE, drugsetname);
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
                rowa[colgrfDrugSetItemQty] = row1["MNC_PH_QTY"].ToString();
                rowa[colgrfDrugSetPrecau] = row1["precautions"].ToString();
                rowa[colgrfDrugSetFreq] = row1["frequency"].ToString();
                rowa[colgrfDrugSetPrecau] = row1["precautions"].ToString();
                rowa[colgrfDrugSetInterac] = row1["interaction"].ToString();
                rowa[colgrfDrugSetIndica] = row1["indication"].ToString();
                rowa[colgrfDrugSetItemStatus] = "drug";
                rowa[colgrfDrugSetFlagSave] = "0";
                i++;
            }
            hideLbLoading();
        }
        private String insertPrescription()
        {
            String prescriptionid = "";
            MedicalPrescription mp = new MedicalPrescription();
            
            mp.active = "1";
            mp.an = "";
            mp.prescription_id = "";
            
            mp.dtr_code = DTRCODE;
            mp.dtr_name_t = "";
            mp.status_ipd =  "O" ;
            mp.visit_date = VSDATE;
            
            mp.remark = "";
            
            mp.hn = HN;
            mp.pre_no = PRENO;
            
            mp.ptt_name_t = lbPttNameT.Text;
            mp.doc_scan_id = "";
            
            mp.counter_name = BC.iniC.station;
            BC.bcDB.mpDB.insertMedicalPrescription(mp);
            MedicalPrescription mp1 = BC.bcDB.mpDB.selectByHn(HN, VSDATE, PRENO);

            return mp1.prescription_id;
        }
        private void convertgrfOrdertoDTDRUG()
        {
            DTDRUG = new DataTable();
            DTDRUG.Columns.Add("order_code");
            DTDRUG.Columns.Add("order_name");
            DTDRUG.Columns.Add("using1");
            DTDRUG.Columns.Add("frequency");
            DTDRUG.Columns.Add("precautions");
            DTDRUG.Columns.Add("indication");
            DTDRUG.Columns.Add("interaction");
            DTDRUG.Columns.Add("qty");
            DTDRUG.Columns.Add("MNC_PH_UNT_CD");
            foreach (Row row in grfOrder.Rows)
            {
                if (row[colgrfOrderCode]==null) continue;
                if (row[colgrfOrderCode].ToString().Equals("code")) continue;
                DataRow rowd = DTDRUG.NewRow();
                rowd["order_code"] = row[colgrfOrderCode].ToString();
                rowd["order_name"] = row[colgrfOrderName].ToString();
                rowd["using1"] = row[colgrfOrderDrugUsing]!=null? row[colgrfOrderDrugUsing].ToString():"";
                rowd["frequency"] = row[colgrfOrderDrugFre] != null ? row[colgrfOrderDrugFre].ToString():"";
                rowd["precautions"] = row[colgrfOrderDrugPrecau] != null ? row[colgrfOrderDrugPrecau].ToString() : "";
                rowd["indication"] = row[colgrfOrderDrugIndica] != null ? row[colgrfOrderDrugIndica].ToString() : "";
                rowd["interaction"] = row[colgrfOrderDrugInterac] != null ? row[colgrfOrderDrugInterac].ToString() : "";
                rowd["qty"] = row[colgrfOrderQty] != null ? row[colgrfOrderQty].ToString() : "";
                //Item itm = new Item();
                //itm = BC.bcDB.itemDB.selectItemByCode(row[colgrfOrderCode].ToString());
                //rowd["MNC_PH_UNT_CD"] = itm.MNC_PH_UNT_CD;
                rowd["MNC_PH_UNT_CD"] = "";
                DTDRUG.Rows.Add(rowd);
            }
            PRESCRIPTIONID = insertPrescription();
        }
        private MemoryStream printPrescription(String lang)
        {
            convertgrfOrdertoDTDRUG();      //mobup
            if (DTDRUG==null)  {                return null;            }
            if(DTDRUG.Rows.Count <= 0) { return null; }
            int gapLine = 14, linenumber = 5, gapX = 40, gapY = 20, xCol1 = 20, xCol2 = 20, xCol3 = 30, xCol4 = 110, xCol5 = 190, xCol6 = 270, xCol7 = 310, xCol8 = 400;
            C1PdfDocument pdf = new C1PdfDocument();
            StringFormat _sfRight, _sfCenter, _sfLeft;
            String dtrname = BC.selectDoctorName(DTRCODE);
            _sfRight = new StringFormat(); _sfCenter = new StringFormat(); _sfLeft = new StringFormat();
            Image logoLeft = Resources.LOGO_BW_tran;
            String patheName = Environment.CurrentDirectory + "\\prescription\\",filename = "";
            if (!Directory.Exists(patheName)) { Directory.CreateDirectory(patheName); }
            String prescriptionid = "", txt="";
            
            if (PRESCRIPTIONID.Length > 3)      {   prescriptionid = PRESCRIPTIONID.Substring(3, 7);      }
            else if (PRESCRIPTIONID.Length <=0) {   return null;            }
            filename = HN + "_prescription_" + prescriptionid + ".pdf";
            float targetWidth = 35;            float targetHeight = 35;
            float widthFactor = targetWidth / logoLeft.Width;
            float heightFactor = targetHeight / logoLeft.Height;
            float scaleFactor = Math.Min(widthFactor, heightFactor);
            float newWidth = logoLeft.Width * scaleFactor;
            float newHeight = logoLeft.Height * scaleFactor;
            float pageWidth = 553, pageheiht= 797;
            RectangleF recflogo = new RectangleF(10, 10, (int)newWidth, (int)newHeight);
            pdf.DocumentInfo.Producer = "pdf";
            pdf.Security.AllowCopyContent = true;
            pdf.Security.AllowEditAnnotations = true;
            pdf.Security.AllowEditContent = true;
            pdf.Security.AllowPrint = true;
            pdf.FontType = FontTypeEnum.Embedded;
            //pdf.PageSize = C1PdfDocumentBase.ToPoints(new SizeF(583, 827));     //A5
            pdf.PageSize = C1PdfDocumentBase.ToPoints(new SizeF(pageWidth, pageheiht));     //A5
            //pdf.PageSize = C1PdfDocumentBase.ToPoints(new SizeF(PageSize.A5.Width, PageSize.A5.Height));     //A5
            pdf.DrawImage(logoLeft, recflogo);
            pdf.DrawString(lang.Equals("th") ? " "+BC.iniC.hostname : BC.iniC.hostnamee, fPDFs2, Brushes.Black, new PointF(50, linenumber), _sfLeft);
            pdf.DrawString(lang.Equals("th") ? " " + BC.iniC.hostaddresst : BC.iniC.hostaddresse, fPDFs6, Brushes.Black, new PointF(50, linenumber+=gapLine), _sfLeft);
            linenumber += linenumber;
            pdf.DrawString(" ใบสั่งยา/Medical Prescription", fPDFs2, Brushes.Black, new PointF((pageWidth / 2) - 100, linenumber += (gapLine - 5)), _sfLeft);
            txt = "ชื่อ " + PTT.Name+" ["+PTT.Hn+"] " + " อายุ " + PTT.AgeStringShort1();
            pdf.DrawString(txt, fPDFs2, Brushes.Black, new PointF(20, linenumber += gapLine), _sfLeft);
            pdf.DrawString("สิทธิ " + VS.PaidName, fPDFs2, Brushes.Black, new PointF(xCol6, linenumber), _sfRight);
            txt = "แพทย์ผู้รักษา " + dtrname+" ["+DTRCODE+"]";
            pdf.DrawString(txt, fPDFs2, Brushes.Black, new PointF(20, linenumber += gapLine), _sfLeft);
            txt = "วันที่รักษา " + BC.datetoShow1(VS.VisitDate) + " " + BC.showTime(VS.VisitTime) + " อาการ "+ lbSymptoms.Text;
            pdf.DrawString(txt, fPDFs2, Brushes.Black, new PointF(20, linenumber += gapLine), _sfLeft);
            String txtallergy = "แพ้ยา : ";
            if (PTT.DRUGALLERGY.Rows.Count > 0)
            {
                foreach(DataRow row in PTT.DRUGALLERGY.Rows)
                {
                    txtallergy += " " + row["mnc_ph_tn"].ToString()+" " + row["MNC_PH_ALG_DSC"].ToString()+" " + row["MNC_PH_MEMO"].ToString();
                    pdf.DrawString(txtallergy, fPDFs2, Brushes.Black, new PointF(20, linenumber += gapLine), _sfLeft);
                }
            }
            int i = 1;
            foreach (DataRow row in DTDRUG.Rows)
            {
                //Header
                if ((i == 1)||(i%13==0))
                {
                    pdf.DrawString("", fPDFs4, Brushes.Black, new PointF(xCol2, linenumber += gapLine), _sfLeft);
                    pdf.DrawString("จำนวน", fPDFs4, Brushes.Black, new PointF(xCol7, linenumber), _sfRight);
                    pdf.DrawString("วิธีใช้/ความถี่", fPDFs4, Brushes.Black, new PointF(xCol3, linenumber), _sfLeft);
                    pdf.DrawString("ข้อบ่งชี้/ข้อควรระวัง", fPDFs4, Brushes.Black, new PointF(xCol5, linenumber), _sfLeft);
                }
                String txtcol1 ="", txtcol3="";
                String drugcode = row["order_code"].ToString(), drugname = row["order_name"].ToString(), using1 = row["using1"].ToString(), freq = row["frequency"].ToString(), precau = row["precautions"].ToString(), indica = row["indication"].ToString(), inter = row["interaction"].ToString(), qty = row["qty"].ToString(), unit = row["MNC_PH_UNT_CD"].ToString();
                txtcol1 = using1.Length > 0 ? using1 : freq;
                txtcol3 = precau.Length > 0 ? precau : indica;
                pdf.DrawString(i+" "+drugname + " [" + drugcode + "]", fPDFs4, Brushes.Black, new PointF(xCol2, linenumber += gapLine), _sfLeft);
                pdf.DrawString(qty+" ["+unit+"]", fPDFs4, Brushes.Black, new PointF(xCol7, linenumber), _sfRight);
                /*col1*/pdf.DrawString(txtcol1, fPDFs4, Brushes.Black, new PointF(xCol3, linenumber += (gapLine-5)), _sfLeft);
                /*col1*/pdf.DrawString(using1.Length > 0 ? freq : "", fPDFs4, Brushes.Black, new PointF(xCol4, linenumber), _sfLeft);
                /*col3*/pdf.DrawString(txtcol3, fPDFs4, Brushes.Black, new PointF(xCol5, linenumber), _sfLeft);
                /*col3*/pdf.DrawString(indica.Length > 0 ? indica : "", fPDFs4, Brushes.Black, new PointF(xCol6, linenumber), _sfLeft);
                pdf.DrawString(inter.Length <= 0 ? "interaction :" + inter : "interaction :", fPDFs4, Brushes.Black, new PointF(xCol3, linenumber += (gapLine - 5)), _sfLeft);
                //pdf.DrawString(qty + " " + unit, fPDFs4, Brushes.Black, new PointF(xCol8, linenumber), _sfRight);
                i++;
            }
            pdf.DrawString("total order drug "+(i-1)+" ", fPDFs4, Brushes.Black, new PointF(xCol6, pageheiht - (310)), _sfRight);
            pdf.DrawString("....................................................................", fPDFs4, Brushes.Black, new PointF(xCol6, pageheiht - (260)), _sfLeft);
            pdf.DrawString("แพทย์ผู้สั่งยา" + dtrname + " [" + DTRCODE + "]", fPDFs4, Brushes.Black, new PointF(xCol6, pageheiht -(240)), _sfLeft);

            pdf.Save(patheName + filename);
            MemoryStream ms = new MemoryStream(); pdf.Save(ms); ms.Position = 0;
            //pdfMERGE.Pages.Add(pdf.Pages[0]);
            pdf.Dispose();
            return ms;
        }
        private void setControlPnSubmit(MemoryStream ms)
        {
            tabMain.Hide();
            pnSubmit.Controls.Clear();
            pnSubmit.Hide();
            C1SplitContainer sC = new C1SplitContainer();
            sC.Dock = DockStyle.Fill;
            sC.Name = "sC";
            sC.SuspendLayout();
            C1SplitterPanel scCheif = new C1SplitterPanel();
            C1SplitterPanel scPhysical = new C1SplitterPanel();
            C1SplitterPanel scDiag = new C1SplitterPanel();
            C1SplitterPanel scDrug = new C1SplitterPanel();
            C1SplitterPanel scDtrAdvice = new C1SplitterPanel();
            scCheif.Collapsible = true;
            scCheif.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Top;
            scCheif.Location = new System.Drawing.Point(0, 21);
            scCheif.Name = "scStaffNote";
            scCheif.SizeRatio = 50;
            scDrug.Collapsible = true;
            scDrug.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            scDrug.Location = new System.Drawing.Point(0, 21);
            scDrug.Name = "scDrug";
            scDiag.Collapsible = true;
            scDiag.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Bottom;
            scDiag.Location = new System.Drawing.Point(0, 21);
            scDiag.Name = "scDiag";
            scDiag.SizeRatio = 50;
            scDtrAdvice.Collapsible = true;
            scDtrAdvice.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Bottom;
            scDtrAdvice.Location = new System.Drawing.Point(0, 21);
            scDtrAdvice.Name = "scDtrAdvice";
            scDtrAdvice.SizeRatio = 50;

            C1TextBox rtbCheif = new C1TextBox();
            rtbCheif.AcceptsTab = true;
            rtbCheif.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbCheif.Multiline = true;
            rtbCheif.Font = fEdit;
            rtbCheif.Location = new System.Drawing.Point(0, 51);
            rtbCheif.Name = "rtbCheif";
            rtbCheif.TabIndex = 0;
            scCheif.Controls.Add(rtbCheif);
            scPhysical.Collapsible = true;
            scPhysical.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Bottom;
            scPhysical.Location = new System.Drawing.Point(0, 21);
            scPhysical.Name = "scPhysical";
            scPhysical.SizeRatio = 50;
            UCRicherTextBox rtbPhysical1 = new UCRicherTextBox(BC, DTRCODE, HN, VSDATE, PRENO, PTT, "doctor_order_physical_exam", PHYSICALEXAM);
            rtbPhysical1.Dock = DockStyle.Fill;
            scPhysical.Controls.Add(rtbPhysical1);
            C1TextBox rtbDiag = new C1TextBox();
            rtbDiag.AcceptsTab = true;
            rtbDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbDiag.Multiline = true;
            rtbDiag.Font = fEdit;
            rtbDiag.Location = new System.Drawing.Point(0, 51);
            rtbDiag.Name = "rtbCheif";
            rtbDiag.TabIndex = 0;
            scDiag.Controls.Add(rtbDiag);
            C1TextBox rtbDtrAdvice = new C1TextBox();
            rtbDtrAdvice.AcceptsTab = true;
            rtbDtrAdvice.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbDtrAdvice.Multiline = true;
            rtbDtrAdvice.Font = fEdit;
            rtbDtrAdvice.Location = new System.Drawing.Point(0, 51);
            rtbDtrAdvice.Name = "rtbCheif";
            rtbDtrAdvice.TabIndex = 0;
            scDtrAdvice.Controls.Add(rtbDtrAdvice);

            sC.Panels.Add(scDrug);
            sC.Panels.Add(scDtrAdvice);
            sC.Panels.Add(scCheif);
            sC.Panels.Add(scDiag);
            sC.Panels.Add(scPhysical);
            

            sC.Name = "scOrdItem";
            C1FlexViewer drugview = new C1FlexViewer();
            drugview.Dock = DockStyle.Fill;
            drugview.Name = "drugview";
            drugview.Ribbon.Minimized = true;
            scDrug.Controls.Add(drugview);
            pnSubmit.Controls.Add(sC);
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            pds.LoadFromStream(ms);
            drugview.DocumentSource = pds;
            sC.ResumeLayout();
            Boolean found = false;
            foreach (Control ctl in pnMain.Controls)
            {
                if (ctl is Panel && ctl.Name == "pnSubmit")
                {
                    found = true;
                    break;
                }
            }
            if(found) { pnMain.Controls.RemoveByKey("pnSubmit"); }
            pnMain.Controls.Add(pnSubmit);
            pnSubmit.Show();
        }
        private void setControlChkDrugSet()
        {
            if (chkDrugOld.Checked)
            {
                txtDrugOldSearch.Visible = true;
                cboDrugSetName.Visible = false;
                btnDrugSetAll.Enabled = true;
                pnDrugSet.Visible = true;
                panel1.Visible = false;
                panel1.Dock = DockStyle.Left;
                panel1.Width = 190;
                panel1.BackColor = Color.AliceBlue;
                pnDrugSet.Dock = DockStyle.Right;
                if(grfVS==null) initGrfVS(); setGrfVS();
                panel1.Visible = true;
            }
            else if (chkDrugSet.Checked)
            {
                txtDrugOldSearch.Visible = false;
                cboDrugSetName.Visible = true;
                btnDrugSetAll.Enabled = true;
                panel1.Visible = false;
                pnDrugSet.Visible = true;
                pnDrugSet.Dock = DockStyle.Fill;
            }
            this.ResumeLayout();
        }
        private void initGrfVS()
        {
            grfVS = new C1FlexGrid();
            grfVS.Font = fEdit;
            grfVS.Dock = System.Windows.Forms.DockStyle.Fill;
            grfVS.Location = new System.Drawing.Point(0, 0);
            grfVS.Rows.Count = 1;
            grfVS.Cols.Count = 3;
            grfVS.Cols[1].Width = 95;
            grfVS.Cols[2].Width = 65;
            grfVS.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfVS.Cols[1].Caption = "-";

            grfVS.Rows[0].Visible = false;
            grfVS.Cols[0].Visible = false;
            grfVS.Cols[1].Visible = true;
            grfVS.Cols[1].AllowEditing = false;
            grfVS.Cols[2].AllowEditing = false;
            panel1.Controls.Add(grfVS);
            grfVS.AfterRowColChange += GrfVS_AfterRowColChange;

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfVS, "Office2010Red");
        }

        private void GrfVS_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfVS.Row < 0) return;
            if (grfVS.Rows.Count <= 1) return;
            if (grfVS.Row >= grfVS.Rows.Count) return;
            Row row = grfVS.Rows[grfVS.Row];
            if (row == null) return;
            grfDrugSet.Rows.Count = 1;
            String vn = row[2] != null ? row[2].ToString() : "";
            if (vn.Length <= 0) return;
            try
            {
                if (rowindexgrfVS != ((C1FlexGrid)(sender)).Row) { rowindexgrfVS = ((C1FlexGrid)(sender)).Row; }
                else { return; }
                String reqdate = BC.datetoDB(row[1].ToString());
                DataTable dt = BC.bcDB.pharT06DB.selectDrugbyHNReqDate(HN, reqdate);
                if (dt.Rows.Count > 0)
                {
                    grfDrugSet.Rows.Count = 1; grfDrugSet.Rows.Count = dt.Rows.Count + 1;
                    int i = 1;
                    foreach (DataRow row1 in dt.Rows)
                    {
                        Row rowa = grfDrugSet.Rows[i];
                        rowa[colgrfDrugSetID] = "drug_old";
                        rowa[colgrfDrugSetItemCode] = row1["MNC_PH_CD"].ToString();
                        rowa[colgrfDrugSetItemName] = row1["MNC_PH_TN"].ToString();
                        rowa[colgrfDrugSetItemQty] = row1["MNC_PH_QTY"].ToString();
                        rowa[colgrfDrugSetItemStatus] = "drug";
                        rowa[colgrfDrugSetFreq] = row1["frequency"].ToString();
                        rowa[colgrfDrugSetPrecau] = row1["precautions"].ToString();
                        rowa[colgrfDrugSetInterac] = row1["interaction"].ToString();
                        rowa[colgrfDrugSetIndica] = row1["indication"].ToString();
                        rowa[colgrfDrugSetFlagSave] = "1";
                        i++;
                    }
                }

            }
            catch (Exception ex)
            {
                new LogWriter("e", this.Name+" GrfVS_AfterRowColChange " + ex.Message);
                BC.bcDB.insertLogPage(BC.userId, this.Name, " GrfVS_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
        }

        private void setGrfVS()
        {
            //ใช้ database ใน object patient จะได้ไม่ต้องดึงข้อมูลหลายครั้ง  ลดการดึงข้อมูล
            DataTable dt = BC.bcDB.vsDB.selectVN_VisitAllByHn(HN);
            grfVS.Rows.Count = 1; grfVS.Rows.Count = dt.Rows.Count + 1;
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfVS.Rows[i];
                rowa[1] = BC.datetoShow1(row1["mnc_date"].ToString());
                rowa[2] = row1["MNC_VN_NO"].ToString() + "." + row1["MNC_VN_SEQ"].ToString() + "." + row1["MNC_VN_SUM"].ToString();
                i++;
            }
        }
        private void BtnPrnStaffNote_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            MemoryStream ms = printPrescription("th");
            setControlPnSubmit(ms);
        }
        private void BtnItemAdd_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfOrderItem();
        }
        private void BtnOrderBeforeSubmit(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            MemoryStream ms = printPrescription("th");
            setControlPnSubmit(ms);
            btnSubmit.Visible = true;
        }
        private void BtnOrderSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String txt = "", tick = "";
            tick = DateTime.Now.Ticks.ToString();
            showLbLoading();
            foreach (Row rowa in grfOrder.Rows)
            {
                String code = "", flag = "", name = "", qty = "", chk = "", using1="", freq = "", precau = "", id = "", interac="", indica="";
                code = rowa[colgrfOrderCode].ToString();
                if (code.Equals("code")) continue;
                chk = rowa[colgrfOrdFlagSave].ToString();
                //if (chk.Equals("1")) continue;// มี ข้อมูลใน table temp_order แล้วไม่ต้อง save เดียวจะ มี2record และในกรณีที่ submit ออก reqno เรียบร้อยแล้วจะได้ ไม่ซ้ำ
                id = rowa[colgrfOrderID].ToString();
                name = rowa[colgrfOrderName].ToString();
                qty = rowa[colgrfOrderQty].ToString();
                flag = rowa[colgrfOrderStatus].ToString();
                freq = rowa[colgrfOrderDrugFre] !=null ? rowa[colgrfOrderDrugFre].ToString():"";
                precau = rowa[colgrfOrderDrugPrecau] != null ? rowa[colgrfOrderDrugPrecau].ToString():"";
                interac = rowa[colgrfOrderDrugInterac] != null ? rowa[colgrfOrderDrugInterac].ToString():"";
                indica = rowa[colgrfOrderDrugIndica] != null ? rowa[colgrfOrderDrugIndica].ToString():"";
                using1 = rowa[colgrfOrderDrugUsing] != null ? rowa[colgrfOrderDrugUsing].ToString():"";
                String re = BC.bcDB.vsDB.insertOrderTemp(id, code, name, qty, using1, freq, precau, interac, indica, flag, txtPttHN.Text.Trim(), VSDATE, PRENO);
                if (int.TryParse(re, out int _))
                {

                }
            }
            setGrfOrderTemp();
            hideLbLoading();
        }
        private void TxtInteraction_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) 
            { 
                if (int.TryParse(grfOrder.Row.ToString(), out int rowa))
                {
                    if (rowa > 0)
                    {
                        Row row = grfOrder.Rows[rowa];
                        row[colgrfOrderDrugInterac] = txtInteraction.Text.Trim();
                    }
                    else
                    {
                        lfSbMessage.Text = "กรุณาเลือกแถวที่ต้องการแก้ไข";
                    }
                }
                else
                {
                    lfSbMessage.Text = "กรุณาเลือกแถวที่ต้องการแก้ไข";
                }
                txtSearchItem.SelectAll(); txtSearchItem.Focus(); 
            }
        }
        private void TxtIndication_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) 
            {
                if (int.TryParse(grfOrder.Row.ToString(), out int rowa))
                {
                    if (rowa > 0)
                    {
                        Row row = grfOrder.Rows[rowa];
                        row[colgrfOrderDrugIndica] = txtIndication.Text.Trim();
                    }
                    else
                    {
                        lfSbMessage.Text = "กรุณาเลือกแถวที่ต้องการแก้ไข";
                    }
                }
                else
                {
                    lfSbMessage.Text = "กรุณาเลือกแถวที่ต้องการแก้ไข";
                }
                txtInteraction.SelectAll(); txtInteraction.Focus(); 
            }
        }
        private void TxtPrecautions_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) 
            {
                if (int.TryParse(grfOrder.Row.ToString(), out int rowa))
                {
                    if (rowa > 0)
                    {
                        Row row = grfOrder.Rows[rowa];
                        row[colgrfOrderDrugPrecau] = txtPrecautions.Text.Trim();
                    }
                    else
                    {
                        lfSbMessage.Text = "กรุณาเลือกแถวที่ต้องการแก้ไข";
                    }
                }
                else
                {
                    lfSbMessage.Text = "กรุณาเลือกแถวที่ต้องการแก้ไข";
                }
                txtIndication.SelectAll(); txtIndication.Focus(); 
            }
        }
        private void TxtFrequency_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) 
            {
                if (int.TryParse(grfOrder.Row.ToString(), out int rowa))
                {
                    if (rowa > 0)
                    {
                        Row row = grfOrder.Rows[rowa];
                        row[colgrfOrderDrugFre] = txtFrequency.Text.Trim();
                    }
                    else
                    {
                        lfSbMessage.Text = "กรุณาเลือกแถวที่ต้องการแก้ไข";
                    }
                }
                else
                {
                    lfSbMessage.Text = "กรุณาเลือกแถวที่ต้องการแก้ไข";
                }
                txtPrecautions.SelectAll(); txtPrecautions.Focus();
            }
        }
        private void TxtNumDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) { e.Handled = true; }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) { e.Handled = true; }
        }
        private void TxtNumDay_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            setQty();
            if (e.KeyCode == Keys.Enter)
            {
                txtItemQTY.SelectAll();
                txtItemQTY.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtDrugPerDay.SelectAll();
                txtDrugPerDay.Focus();
            }
        }
        private void TxtPerDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) { e.Handled = true; }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) { e.Handled = true; }
        }
        private void TxtPerDay_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            setQty();
            if (e.KeyCode == Keys.Enter)
            {
                txtDrugNumDay.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtDrugNum.SelectAll();
                txtDrugNum.Focus();
            }
        }
        private void TxtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) { e.Handled = true; }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) { e.Handled = true; }
        }
        private void TxtNum_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            setQty();
            if (e.KeyCode == Keys.Enter) { txtDrugPerDay.Focus(); }
            else if (e.KeyCode == Keys.Left) { txtItemQTY.SelectAll(); txtItemQTY.Focus(); }
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
        private void TxtItemQTY_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Right)
            {
                txtDrugNum.SelectAll();
                txtDrugNum.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                setGrfOrderItem();
                clearControlOrder();
                txtSearchItem.SelectAll();
                txtSearchItem.Focus();
            }
        }
        private void TxtItemCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (chkItemDrug.Checked)                {                    txtItemQTY.SelectAll();                    txtItemQTY.Focus();                }
                else                {                    setGrfOrderItem();                }
            }
        }
        private void TxtGeneric_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            { if (txtGeneric.Text.Trim().Length <= 0) return; setOrderItem(sender); txtItemCode.Focus(); }
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
            {                if (txtSearchItem.Text.Trim().Length <= 0) return;                setOrderItem(sender);                txtItemCode.Focus();            }
        }
        private void BtnOperItemSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmItemSearch frm = new FrmItemSearch(BC);
            frm.ShowDialog();
            if ((BC.items != null) &&(BC.items.Count > 0))
            {
                foreach (Item item in BC.items)
                {                    setGrfOrderItem(item.code, item.name, item.qty, item.flag);                }
            }
        }
        private void setControlCHkItemDrug()
        {
            txtSearchItem.AutoCompleteCustomSource = BC.bcDB.pharM01DB.AUTODrugTR;
            cboDrugSetName.Show(); chkDrugSet.Show(); pnDrugSet.Show(); btnDrugSetAll.Show();
            txtDrugNum.Show(); txtDrugNumDay.Show(); txtDrugPerDay.Show();
            txtSearchItem.Focus(); chkDrugOld.Show();
            pnOrdDrug.Show(); pnDrugSet.Hide(); pnDrugSetTop.Show();
        }
        private void setControlChkItem(object sender)
        {
            if(sender==null)
            {
                lfSbMessage.Text = "no sender";
                return;
            }
            this.SuspendLayout();
            chkDrugOld.Visible = false;
            pnOrdDrug.Hide();
            pnDrugSet.Hide();
            pnDrugSetTop.Hide();
            if (((RadioButton)sender).Name.Equals("chkItemHotC", StringComparison.OrdinalIgnoreCase))
            {
                // This is the chkItemDrug checkbox
                txtSearchItem.AutoCompleteCustomSource = BC.bcDB.pm30DB.AUTOProcedure;
                cboDrugSetName.Hide(); chkDrugSet.Hide(); btnDrugSetAll.Hide();
                txtDrugNum.Hide(); txtDrugNumDay.Hide(); txtDrugPerDay.Hide(); 
                txtSearchItem.Focus();
            }
            else if(((RadioButton)sender).Name.Equals("chkItemDrug", StringComparison.OrdinalIgnoreCase))
            {
                setControlCHkItemDrug();
            }
            else if (((RadioButton)sender).Name.Equals("chkItemXray", StringComparison.OrdinalIgnoreCase))
            {
                txtSearchItem.AutoCompleteCustomSource = BC.bcDB.xrayM01DB.AUTOXray;
                cboDrugSetName.Hide(); chkDrugSet.Hide();  btnDrugSetAll.Hide();
                txtDrugNum.Hide(); txtDrugNumDay.Hide(); txtDrugPerDay.Hide();
                txtSearchItem.Focus();
            }
            else if (((RadioButton)sender).Name.Equals("chkItemLab", StringComparison.OrdinalIgnoreCase))
            {
                txtSearchItem.AutoCompleteCustomSource = BC.bcDB.labM01DB.AUTOLab; cboDrugSetName.Hide(); chkDrugSet.Hide(); pnDrugSet.Hide();
                btnDrugSetAll.Hide(); txtDrugNum.Hide(); txtDrugNumDay.Hide(); txtDrugPerDay.Hide();
                txtSearchItem.Focus();
            }
            clearControlOrder();
            this.ResumeLayout();
        }
        private void ChkItemDrug_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlChkItem(sender);
        }
        private void ChkItemHotC_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlChkItem(sender);
        }
        private void ChkItemXray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlChkItem(sender);
        }
        private void ChkItemLab_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlChkItem(sender);
        }
        private void clearControlOrder()
        {
            txtItemCode.Value = ""; txtSearchItem.Value = ""; lbItemName.Text = ""; txtIndication.Value = "";
            txtGeneric.Value = ""; lbThaiName.Text = ""; txtFrequency.Value = ""; txtPrecautions.Value = "";
            txtIndication.Value = ""; txtInteraction.Value = ""; lbStrength.Text = ""; txtItemQTY.Value = "";
            txtDrugNum.Value = ""; txtDrugPerDay.Value = ""; txtDrugNumDay.Value = "";
            picPaid1.Image = null;picPaid2.Image = null;picPaid3.Image = null;picPaid4.Image = null; txtUsing.Value = ""; txtInteraction.Value = ""; txtProperties.Value = "";
            txtUsing1.Value = ""; txtFrequency1.Value = ""; txtPrecautions1.Value = ""; txtIndication1.Value = ""; txtProperties1.Value = ""; txtInteraction1.Value = "";
        }
        private void setOrderItem(object sender)
        {
            if(sender==null)
            {
                lfSbMessage.Text = "no item";
                return;
            }
            String[] txt;
            if (((C1TextBox)sender).Name.Equals(txtSearchItem.Name))
            {
                txt = txtSearchItem.Text.Split('#');
            }
            else if (((C1TextBox)sender).Name.Equals(txtGeneric.Name))
            {
                txt = txtGeneric.Text.Split('#');
            }
            else
            {
                lfSbMessage.Text = "no item";
                return;
            }
            if ((txt==null)||(txt.Length <= 1))
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
                lab = BC.bcDB.labM01DB.SelectByPk(code);
                txtItemCode.Value = lab.MNC_LB_CD;
                lbItemName.Text = lab.MNC_LB_DSC;
                //txtItemQTY.Visible = false;
                txtItemQTY.Value = "1";
            }
            else if (chkItemXray.Checked)
            {
                XrayM01 xray = new XrayM01();
                xray = BC.bcDB.xrayM01DB.SelectByPk(code);
                txtItemCode.Value = xray.MNC_XR_CD;
                lbItemName.Text = xray.MNC_XR_DSC;
                //txtItemQTY.Visible = false;
                txtItemQTY.Value = "1";
            }
            else if (chkItemProcedure.Checked)
            {
                PatientM30 pm30 = new PatientM30();
                String name1 = BC.bcDB.pm30DB.SelectNameByPk(code);
                txtItemCode.Value = code;
                lbItemName.Text = name1;
                //txtItemQTY.Visible = false;
                txtItemQTY.Value = "1";
            }
            else if (chkItemDrug.Checked)
            {
                PharmacyM01 drug = new PharmacyM01();
                DataTable dtpmap = new DataTable();
                dtpmap = BC.bcDB.drugDB.selectPaidmapByCode(code);
                drug = BC.bcDB.pharM01DB.SelectNameByPk1(code);
                txtItemCode.Value = code;
                lbItemName.Text = drug.MNC_PH_TN;
                txtGeneric.Value = drug.MNC_PH_GN;
                lbThaiName.Text = drug.MNC_PH_THAI;
                txtUsing.Value = drug.using1;
                txtFrequency.Value = drug.frequency;
                txtPrecautions.Value = drug.precautions;
                txtIndication.Value = drug.indication;
                txtProperties.Value = drug.properties;
                lbStrength.Text = drug.MNC_PH_STRENGTH;
                txtItemQTY.Value = "1";
                picPaid1.Image = null; picPaid2.Image = null; picPaid3.Image = null; picPaid4.Image = null;
                if (dtpmap.Rows.Count > 0)
                {
                    foreach(DataRow row in dtpmap.Rows)
                    {
                        if (row["MNC_FN_TYP_CD"].ToString().Equals("1"))
                        {
                            picPaid1.Image = row["status_paid"].ToString().Equals("1") ? imgPaidTrue : row["status_paid"].ToString().Equals("0") ? imgPaidFalse : null;
                        }
                        else if (row["MNC_FN_TYP_CD"].ToString().Equals("2"))
                        {
                            picPaid2.Image = row["status_paid"].ToString().Equals("1") ? imgPaidTrue : row["status_paid"].ToString().Equals("0") ? imgPaidFalse : null;
                        }
                        else if (row["MNC_FN_TYP_CD"].ToString().Equals("3"))
                        {
                            picPaid3.Image = row["status_paid"].ToString().Equals("1") ? imgPaidTrue : row["status_paid"].ToString().Equals("0") ? imgPaidFalse : null;
                        }
                        else if (row["MNC_FN_TYP_CD"].ToString().Equals("4"))
                        {
                            picPaid4.Image = row["status_paid"].ToString().Equals("1") ? imgPaidTrue : row["status_paid"].ToString().Equals("0") ? imgPaidFalse : null;
                        }
                    }
                }
                else
                {
                    picPaid1.Image = imgPaidFalse;
                    picPaid2.Image = imgPaidFalse;
                    picPaid3.Image = imgPaidFalse;
                    picPaid4.Image = imgPaidFalse;
                }
            }
        }
        private void setGrfOrderItem()
        {
            if(chkDupOrderItem(txtItemCode.Text.Trim(), chkItemLab.Checked ? "lab" : chkItemXray.Checked ? "xray" : chkItemProcedure.Checked ? "procedure" : chkItemDrug.Checked ? "drug" : ""))
            {       //check duplicate
                lfSbMessage.Text = "รายการซ้ำ";
                txtSearchItem.SelectAll();
                txtSearchItem.Focus();
                return;
            }
            if (chkItemDrug.Checked)
            {
                setGrfOrderItemDrug(txtItemCode.Text.Trim(), lbItemName.Text, txtItemQTY.Text.Trim(), txtUsing.Text.Trim(), txtFrequency.Text.Trim(),txtIndication.Text.Trim(), txtPrecautions.Text.Trim(), txtInteraction.Text.Trim(), txtRemark.Text.Trim(), txtGeneric.Text.Trim(), lbThaiName.Text);
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
        private Boolean chkDupOrderItem(String code, String flag)
        {
            if (grfOrder == null) { return false; }
            if (grfOrder.Rows.Count <= 1) { return false; }
            foreach (Row row in grfOrder.Rows)
            {
                if (row[colgrfOrderCode]==null) continue;
                if (row[colgrfOrderCode].ToString().Equals("code")) continue;
                if (row[colgrfOrderCode].ToString().Equals(code) && row[colgrfOrderStatus].ToString().Equals(flag))
                {
                    return true;
                }
            }
            return false;
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
            rowitem[colgrfOrderDrugUsing] = "";
            rowitem[colgrfOrderDrugIndica] = "";
            rowitem[colgrfOrderDrugInterac] = "";
            rowitem[colgrfOrderID] = "";
            rowitem[colgrfOrderReqNO] = "";
            rowitem[colgrfOrdFlagSave] = "0";//ต้องการ save ลง table temp_order
            txtSearchItem.Value = "";
            txtSearchItem.Focus();
            //grfOrder.Rows.Add(rowitem);
        }
        private void setGrfOrderItemDrug(String code, String name, String qty, String using1, String drugfreq, String indica, String drugprecau, String interac, String remark, String generic, String thai)
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
            rowitem[colgrfOrderDrugUsing] = using1;
            rowitem[colgrfOrderDrugIndica] = indica;
            rowitem[colgrfOrderDrugInterac] = interac;
            rowitem[colgrfOrderID] = "";
            rowitem[colgrfOrderReqNO] = "";
            rowitem[colgrfOrdFlagSave] = "0";//ต้องการ save ลง table temp_order
            rowitem[colgrfOrderDrugRemark] = remark;
            rowitem[colgrfOrderGeneric] = generic;
            rowitem[colgrfOrderThai] = thai;
            txtSearchItem.Value = "";
            txtSearchItem.Focus();
            //grfOrder.Rows.Add(rowitem);
        }
        private void setControlTabOperVital(Visit vs)
        {
            //txtOperHN.Value = vs.HN;
            //lbOperPttNameT.Text = vs.PatientName;
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
            //txtOperDtr.Value = vs.DoctorId;
            //lbOperDtrName.Text = vs.DoctorName;
            lbSymptoms.Text = vs.symptom.Replace("\r\n", ",");
            lbSymptoms.Text = lbSymptoms.Text.Replace(",,,", "");
            lbSymptoms.Text = lbSymptoms.Text.Replace(",,", "");
            txtOperAbo.Value = "";
            txtOperBmi.Value = BC.calBMI(vs.weight, vs.high);
            lbPaidName.Text = "สิทธิ "+vs.PaidName;
        }
        private void clearControlTabOperVital()
        {
            //txtOperHN.Value = "";
            //lbOperPttNameT.Text = "";
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
            //txtOperDtr.Value = "";
            //lbOperDtrName.Text = "";
            lbSymptoms.Text = "";
            txtOperBmi.Value = "";
            txtOperO2.Value = "";
            txtOperPain.Value = "";
        }
        private void initRichTextChief()
        {
            rtbCheif = new C1TextBox();
            rtbCheif.AcceptsTab = true;
            ContextMenu menu = new ContextMenu();
            MenuItem mnuSave = new MenuItem("Save Chief Complaint");
            mnuSave.Click += MnuChiefSave_Click;
            menu.MenuItems.Add(mnuSave);
            rtbCheif.ContextMenu = menu;
            rtbCheif.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbCheif.Multiline = true;
            rtbCheif.Font = fEdit;
            rtbCheif.Location = new System.Drawing.Point(0, 51);
            rtbCheif.Name = "rtbCheif";
            rtbCheif.TabIndex = 0;
            AUTOChief = new AutocompleteMenuNS.AutocompleteMenu();
            AUTOChief.AllowsTabKey = true;
            AUTOChief.Font = new System.Drawing.Font(BC.iniC.grdViewFontName, 12F);
            AUTOChief.Items = new string[0];
            AUTOChief.SearchPattern = "[\\w\\.:=!<>]";
            AUTOChief.TargetControlWrapper = null;
            AUTOChief.SetAutocompleteItems(BC.bcDB.autoCompDB.BuildAutoCompleteLine1("doctor_chief", DTRCODE));
            AUTOChief.SetAutocompleteMenu(rtbCheif, AUTOChief);

            //rtbCheif.LoadFile(bc.ToStreamTxt(bc.preoperation), RichTextBoxStreamType.PlainText);

            pnChief.Controls.Add(rtbCheif);
        }

        private void MnuChiefSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = BC.bcDB.vsDB.updateChiefCompliant(txtPttHN.Text.Trim(), VSDATE, PRENO, rtbCheif.Text.Trim(), DTRCODE);
            if (re.Equals("1"))
            {
                lfSbMessage.Text = "Save Chief Complaint Complete";
            }
            else
            {
                lfSbMessage.Text = "Save Chief Complaint not complete";
            }
        }
        private void initRichTextPhysical()
        {
            rtbPhysical = new C1TextBox();
            rtbPhysical.AcceptsTab = true;
            ContextMenu menu = new ContextMenu();
            MenuItem mnuSave = new MenuItem("Save Physical Exam");
            mnuSave.Click += MnuPhysicalSave_Click;
            menu.MenuItems.Add(mnuSave);
            rtbPhysical.ContextMenu = menu;
            //this.rtbDocument.ContextMenuStrip = this.contextMenu;
            rtbPhysical.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbPhysical.Multiline = true;
            rtbPhysical.Font = fEdit;
            rtbPhysical.Location = new System.Drawing.Point(0, 51);
            rtbPhysical.Name = "rtbPhysical";
            rtbPhysical.TabIndex = 0;
            AUTOPyhsical = new AutocompleteMenuNS.AutocompleteMenu();
            AUTOPyhsical.AllowsTabKey = true;
            AUTOPyhsical.Font = new System.Drawing.Font(BC.iniC.grdViewFontName, 12F);
            AUTOPyhsical.Items = new string[0];
            AUTOPyhsical.SearchPattern = "[\\w\\.:=!<>]";
            AUTOPyhsical.TargetControlWrapper = null;
            AUTOPyhsical.SetAutocompleteItems(BC.bcDB.autoCompDB.BuildAutoCompleteLine1("doctor_physical", DTRCODE));
            AUTOPyhsical.SetAutocompleteMenu(rtbPhysical, AUTOPyhsical);
            //rtbCheif.LoadFile(bc.ToStreamTxt(bc.preoperation), RichTextBoxStreamType.PlainText);

            pnPhysicalDesc.Controls.Add(rtbPhysical);
        }

        private void MnuPhysicalSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = BC.bcDB.vsDB.updatePhysicalExam(txtPttHN.Text.Trim(), VSDATE, PRENO, rtbPhysical.Text.Trim(), DTRCODE);
            if (re.Equals("1"))
            {
                lfSbMessage.Text = "Save Physical Exam Complete";
            }
            else
            {
                lfSbMessage.Text = "Save Physical Exam not complete";
            }
        }

        private void initRichTextDiag()
        {
            rtbDiag = new C1TextBox();
            rtbDiag.AcceptsTab = true;
            ContextMenu menu = new ContextMenu();
            MenuItem mnuSave = new MenuItem("Save Diagnosis");
            mnuSave.Click += MnuDiagnosisSave_Click;
            menu.MenuItems.Add(mnuSave);
            rtbDiag.ContextMenu = menu;
            //this.rtbDocument.ContextMenuStrip = this.contextMenu;
            rtbDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbDiag.Multiline = true;
            rtbDiag.Font = fEdit;
            rtbDiag.Location = new System.Drawing.Point(0, 51);
            rtbDiag.Name = "rtbDiag";
            rtbDiag.TabIndex = 0;
            AUTODIAG = new AutocompleteMenuNS.AutocompleteMenu();
            AUTODIAG.AllowsTabKey = true;
            AUTODIAG.Font = new System.Drawing.Font(BC.iniC.grdViewFontName, 12F);
            AUTODIAG.Items = new string[0];
            AUTODIAG.SearchPattern = "[\\w\\.:=!<>]";
            AUTODIAG.TargetControlWrapper = null;
            AUTODIAG.SetAutocompleteItems(BC.bcDB.autoCompDB.BuildAutoCompleteLine1("doctor_diag", DTRCODE));
            AUTODIAG.SetAutocompleteMenu(rtbDiag, AUTODIAG);
            //rtbCheif.LoadFile(bc.ToStreamTxt(bc.preoperation), RichTextBoxStreamType.PlainText);

            pnDiag1.Controls.Add(rtbDiag);
        }

        private void MnuDiagnosisSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = BC.bcDB.vsDB.updateDiagnosis(txtPttHN.Text.Trim(), VSDATE, PRENO, rtbDiag.Text.Trim(), DTRCODE);
            if (re.Equals("1"))
            {
                lfSbMessage.Text = "Save Diagnosis Complete";
            }
            else
            {
                lfSbMessage.Text = "Save Diagnosis not complete";
            }
        }
        private void initRichTextDtrAdvice()
        {
            rtbDtrAdvice = new C1TextBox();
            rtbDtrAdvice.AcceptsTab = true;
            ContextMenu menu = new ContextMenu();
            MenuItem mnuSave = new MenuItem("Save Doctor Advice");
            mnuSave.Click += MnuDtrAdviceSave_Click;
            menu.MenuItems.Add(mnuSave);
            rtbDtrAdvice.ContextMenu = menu;
            rtbDtrAdvice.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbDtrAdvice.Multiline = true;
            rtbDtrAdvice.Font = fEdit;
            rtbDtrAdvice.Location = new System.Drawing.Point(0, 51);
            rtbDtrAdvice.Name = "rtbDtrAdvice";
            rtbDtrAdvice.TabIndex = 0;
            AUTODTRADVICE = new AutocompleteMenuNS.AutocompleteMenu();
            AUTODTRADVICE.AllowsTabKey = true;
            AUTODTRADVICE.Font = new System.Drawing.Font(BC.iniC.grdViewFontName, 12F);
            AUTODTRADVICE.Items = new string[0];
            AUTODTRADVICE.SearchPattern = "[\\w\\.:=!<>]";
            AUTODTRADVICE.TargetControlWrapper = null;
            AUTODTRADVICE.SetAutocompleteItems(BC.bcDB.autoCompDB.BuildAutoCompleteLine1("doctor_advice", DTRCODE));
            AUTODTRADVICE.SetAutocompleteMenu(rtbDtrAdvice, AUTOChief);

            //rtbCheif.LoadFile(bc.ToStreamTxt(bc.preoperation), RichTextBoxStreamType.PlainText);

            pnDtrAdvice1.Controls.Add(rtbDtrAdvice);
            
            ucApm = new UCAppointment(BC, DTRCODE, HN, VSDATE, PRENO, PTT, VS, "doctor_order", ref lfSbMessage);
            ucApm.Dock = DockStyle.Fill;
            pnDtrAdviceApm.Controls.Add(ucApm);
        }
        private void MnuDtrAdviceSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = BC.bcDB.vsDB.updateDoctorAdvice(txtPttHN.Text.Trim(), VSDATE, PRENO, rtbDtrAdvice.Text.Trim(), DTRCODE);
            if (re.Equals("1"))
            {
                lfSbMessage.Text = "Save Doctor Advice";
            }
            else
            {
                lfSbMessage.Text = "Save Doctor Advice not complete";
            }
        }
        private void initGrfOrder(ref C1FlexGrid grf, ref Panel pn, String grfname)
        {
            grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);
            grf.Rows.Count = 1;
            grf.Cols.Count = 18;
            grf.Cols[colgrfOrderCode].Width = 70;              grf.Cols[colgrfOrderName].Width = 300;       grf.Cols[colgrfOrderStatus].Width = 50;
            grf.Cols[colgrfOrderQty].Width = 70;                grf.Cols[colgrfOrderDrugUsing].Width = 300;
            grf.Cols[colgrfOrderDrugFre].Width = 300;           grf.Cols[colgrfOrderDrugPrecau].Width = 300;
            grf.Cols[colgrfOrderDrugIndica].Width = 300;        grf.Cols[colgrfOrderDrugInterac].Width = 300; grf.Cols[colgrfOrderDrugRemark].Width = 300;
            grf.Name = grfname;
            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grf.Cols[colgrfOrderCode].DataType = typeof(String);
            grf.Cols[colgrfOrderName].DataType = typeof(String);
            grf.Cols[colgrfOrderQty].DataType = typeof(String);
            grf.Cols[colgrfOrderDrugUsing].DataType = typeof(String);
            grf.Cols[colgrfOrderDrugFre].DataType = typeof(String);
            grf.Cols[colgrfOrderDrugPrecau].DataType = typeof(String);
            grf.Cols[colgrfOrderDrugIndica].DataType = typeof(String);
            grf.Cols[colgrfOrderDrugInterac].DataType = typeof(String);
            grf.Cols[colgrfOrderDrugRemark].DataType = typeof(String);
            grf.Cols[colgrfOrderCode].TextAlign = TextAlignEnum.CenterCenter;
            grf.Cols[colgrfOrderName].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfOrderQty].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfOrderReqNO].TextAlign = TextAlignEnum.CenterCenter;
            grf.Cols[colgrfOrderDrugUsing].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfOrderDrugFre].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfOrderDrugPrecau].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfOrderDrugIndica].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[colgrfOrderDrugInterac].TextAlign = TextAlignEnum.LeftCenter;

            grf.Cols[colgrfOrderCode].Visible = true;
            grf.Cols[colgrfOrderName].Visible = true;
            grf.Cols[colgrfOrderStatus].Visible = true;//production ค่อยเปลี่ยนเป็น false
            grf.Cols[colgrfOrderID].Visible = false;
            grf.Cols[colgrfOrdFlagSave].Visible = false;
            grf.Cols[colgrfOrderReqNO].Visible = false;
            grf.Cols[colgrfOrderReqDate].Visible = false;
            grf.Cols[colgrfOrderGeneric].Visible = false;
            grf.Cols[colgrfOrderThai].Visible = false;
            if (grfname.Equals("grfOrder"))
            {
                grf.Cols[colgrfOrderQty].Visible = true;
                grf.Cols[colgrfOrderCode].Caption = "รหัส";
                grf.Cols[colgrfOrderName].Caption = "ชื่อยา";
                grf.Cols[colgrfOrderQty].Caption = "qty";
                grf.Cols[colgrfOrderDrugUsing].Caption = "วิธีใช้";
                grf.Cols[colgrfOrderDrugFre].Caption = "ความถี่";
                grf.Cols[colgrfOrderDrugPrecau].Caption = "คำเตือน";
                grf.Cols[colgrfOrderDrugIndica].Caption = "ข้อบ่งชี้";
                grf.Cols[colgrfOrderDrugProper].Caption = "สรรพคุณ";
                grf.Cols[colgrfOrderDrugInterac].Caption = "interaction";
                grf.Cols[colgrfOrderDrugRemark].Caption = "หมายเหตุ";
                theme1.SetTheme(grf, "VS2013Red");
            }
            else
            {
                grf.Cols[colgrfOrderQty].Visible = false;
                theme1.SetTheme(grf, BC.iniC.themeApp);
            }
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_GrfOrder_Delete));
            grf.ContextMenu = menuGw;
            grf.Cols[colgrfOrderCode].AllowEditing = false;
            grf.Cols[colgrfOrderName].AllowEditing = false;
            grf.Cols[colgrfOrderReqNO].AllowEditing = false;
            grf.Cols[colgrfOrderDrugUsing].AllowEditing = false;
            grf.Cols[colgrfOrderDrugFre].AllowEditing = false;
            grf.Cols[colgrfOrderDrugPrecau].AllowEditing = false;
            grf.Cols[colgrfOrderDrugIndica].AllowEditing = false;
            grf.Cols[colgrfOrderDrugInterac].AllowEditing = false;
            grf.Cols[colgrfOrderDrugRemark].AllowEditing = false;
            grf.DoubleClick += GrfOrder_DoubleClick;
            grf.Click += Grf_Click;
            grf.AllowSorting = AllowSortingEnum.None;
            pn.Controls.Add(grf);
        }
        private void ContextMenu_GrfOrder_Delete(object sender, System.EventArgs e)
        {
            if(grfOrder.Row <= 0) return;
            if (grfOrder.Col <= 0) return;
            if (grfOrder.Name.Equals("grfOrder"))
            {
                deleteGrfOrderTemp();
            }
        }
        private void Grf_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;
            if (((C1FlexGrid)sender).Name.Equals("grfOrder"))
            {
                String id = "", flag = "", name = "", freq = "", precau = "", code = "";
                id = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderID] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderID].ToString() : "";
                txtOrderId.Value = id;
                txtItemCode.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderCode] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderCode].ToString() : "";
                lbItemName.Text = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderName] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderName].ToString() : "";
                txtUsing.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugUsing] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugUsing].ToString():"";
                txtFrequency.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugFre] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugFre].ToString():"";
                txtPrecautions.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugPrecau] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugPrecau].ToString():"";
                txtInteraction.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugInterac] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugInterac].ToString():"";
                txtIndication.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugIndica] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugIndica].ToString() : "";
                txtItemQTY.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderQty] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderQty].ToString() : "";
                txtGeneric.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderGeneric] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderGeneric].ToString() : "";
                lbThaiName.Text = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderThai] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderThai].ToString() : "";
                txtRowId.Value = ((C1FlexGrid)sender).Row.ToString();
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
                //deleteGrfOrderTemp();
            }
        }
        private void deleteGrfOrderTemp()
        {
            if (grfOrder.Row <= 0) return;
            if (grfOrder.Col <= 0) return;
            String id = "";
            id = grfOrder[grfOrder.Row, colgrfOrderID].ToString();
            String re = BC.bcDB.vsDB.deleteOrderTemp(id);
            setGrfOrderTemp();
        }
        private void setGrfOrder()
        {//ดึงจาก table pharmacy_t06, lab_t02, xray_t02, procedure_t16
            DataTable dt = new DataTable();
            dt = BC.bcDB.vsDB.selectOrderTempByHN(txtPttHN.Text.Trim(), VSDATE, PRENO);
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
                    new LogWriter("e", this.Name + " setGrfOrder " + ex.Message);
                    BC.bcDB.insertLogPage(BC.userId, this.Name, "setGrfOrder ", ex.Message);
                }
            }
        }
        private void setGrfOrderTemp()
        {//ดึงจาก table temp_order
            DataTable dt = new DataTable();
            dt = BC.bcDB.vsDB.selectOrderTempByHN(txtPttHN.Text.Trim(), VSDATE, PRENO);
            int i = 1, j = 1;
            grfOrder.Rows.Count = 1;
            grfOrder.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                try
                {
                    Row rowa = grfOrder.Rows[i];
                    rowa[colgrfOrderDrugUsing] = row1["using1"].ToString();
                    rowa[colgrfOrderDrugFre] = row1["frequency"].ToString();
                    rowa[colgrfOrderDrugPrecau] = row1["precautions"].ToString();
                    rowa[colgrfOrderDrugIndica] = row1["indication"].ToString();
                    rowa[colgrfOrderDrugInterac] = row1["interaction"].ToString();
                    rowa[colgrfOrderCode] = row1["order_code"].ToString();
                    rowa[colgrfOrderName] = row1["order_name"].ToString();
                    rowa[colgrfOrderQty] = row1["qty"].ToString();
                    rowa[colgrfOrderStatus] = row1["flag"].ToString();
                    rowa[colgrfOrderID] = row1["id"].ToString();
                    rowa[colgrfOrderReqNO] = "";
                    rowa[colgrfOrdFlagSave] = "1";//ดึงข้อมูลจาก table temp_order 
                    rowa[colgrfOrderGeneric] = "";
                    rowa[colgrfOrderThai] = "";
                    rowa[colgrfOrderReqDate] = "";
                    rowa[colgrfOrderDrugRemark] = "";
                    rowa[0] = i.ToString();
                    rowa.StyleNew.BackColor = ColorTranslator.FromHtml(BC.iniC.grfRowColor);
                    i++;
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD setGrfOrderTemp " + ex.Message);
                    BC.bcDB.insertLogPage(BC.userId, this.Name, "setGrfOrderTemp ", ex.Message);
                }
            }
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
            PTT.CHRONIC = BC.bcDB.vsDB.SelectChronicByPID(PTT.idcard);
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
            PTT.DRUGALLERGY = BC.bcDB.vsDB.selectDrugAllergy(txtPttHN.Text.Trim());
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
        private void setControlPnPateint()
        {
            //this.SuspendLayout();
            //PTT = bc.bcDB.pttDB.selectPatinetByHn(this.HN);
            PTT = new Patient();
            PTT = BC.bcDB.pttDB.selectPatinetVisitOPDByHn(HN, VSDATE, PRENO);
            if (PTT == null) return;
            if (PTT.Hn.Length <=0) return;
            VS = BC.bcDB.vsDB.selectbyPreno(HN, VSDATE, PRENO);
            txtPttHN.Value = PTT.Hn; lbPttNameT.Text = PTT.Name; HN = PTT.Hn;
            setGrfDrugAllergy(); setGrfChronic();
            //if (grfVS.Rows.Count > 2) grfVS.Select(1, 1);

            lbVN.Text = "...";
            
            lbPttAttachNote.Text = PTT.MNC_ATT_NOTE.Length <= 0 ? "..." : PTT.MNC_ATT_NOTE;
            lbPttFinNote.Text = PTT.MNC_FIN_NOTE.Length <= 0 ? "..." : PTT.MNC_FIN_NOTE;
            lbPttAge.Text = "age : " + PTT.AgeStringOK1DOT();
            lfSbComp.Text = "comp(" + PTT.comNameT + ")";
            lfSbInsur.Text = "insur[" + PTT.insurNameT + "]";
            rgSbHIV.Text = PTT.statusHIV.Length > 0 ? "(HP)" : "";
            rgSbHIV.ToolTip = PTT.statusHIV;
            rgSbAFB.Text = PTT.statusAFB.Length > 0 ? "[AF]" : "";
            rgSbAFB.ToolTip = PTT.statusAFB;

            setGrfOrderTemp();
            setControlCHkItemDrug();
            setControlTabOperVital(VS);
            //grfVS.Focus();
            //this.ResumeLayout();
        }
        private void clearControlPnPateint()
        {
            txtPttHN.Value = "";
            lbPttNameT.Text = "";
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D | Keys.Control:
                    {
                        // Add logic here if needed, or explicitly break/return
                        Control ctl1 = new Control();
                        ctl1 = GetFocusedControl();
                        if ((ctl1 is C1TextBox) && (ctl1.Name.Equals("txtDrugNum")))
                        {
                            txtDrugNum.SelectAll(); txtDrugNum.Focus();
                        }
                        break;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private Control GetFocusedControl()
        {
            Control focusedControl = null;
            // To get hold of the focused control:
            IntPtr focusedHandle = GetFocus();
            if (focusedHandle != IntPtr.Zero)
                // Note that if the focused Control is not a .Net control, then this will return null.
                focusedControl = Control.FromHandle(focusedHandle);
            return focusedControl;
        }
        private void AddCopyClaudetoClipboard()
        {
            var btnCopy = new Button
            {
                Text = "📋 Copy",
                Size = new Size(80, 30)
            };
            btnCopy.Click += (s, e) =>
            {
                if (!string.IsNullOrEmpty(txtClaude.SelectedText))
                {
                    Clipboard.SetText(txtClaude.SelectedText);
                    MessageBox.Show("คัดลอกแล้ว", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
        }
        private void ExportChatClaudeHistory()
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                DefaultExt = "txt",
                FileName = $"chat_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveDialog.FileName, txtClaude.Text);
                MessageBox.Show("บันทึกเรียบร้อย", "Success");
            }
        }
        public static class ChatColors
        {
            // User Message
            public static Color UserBackground = Color.FromArgb(33, 150, 243); // Blue
            public static Color UserText = Color.White;

            // Assistant Message
            public static Color AssistantBackground = Color.White;
            public static Color AssistantText = Color.Black;
            public static Color AssistantAccent = Color.FromArgb(33, 150, 243);

            // System
            public static Color SystemBackground = Color.FromArgb(245, 245, 245);
            public static Color SystemText = Color.Gray;

            // Error
            public static Color ErrorBackground = Color.FromArgb(255, 235, 238);
            public static Color ErrorText = Color.FromArgb(198, 40, 40);

            // App Background
            public static Color AppBackground = Color.FromArgb(240, 242, 245);
        }
        private void FrmDoctorOrder_Load(object sender, EventArgs e)
        {
            //this.SuspendLayout();
            this.Text = "last update 20251006";
            System.Drawing.Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            scMain.HeaderHeight = 0;
            
            c1SplitterPanel2.SizeRatio = 30;        //left
            c1SplitterPanel1.SizeRatio = 88;        
            c1SplitterPanel6.SizeRatio = 12;        //top
            c1SplitterPanel6.MinHeight = 40;
            c1SplitterPanel6.Height = 80;
            pnDtrAdvice.MinHeight = 100;
            pnDtrAdvice.Height = 150;
            pnChief.Height = 60;
            pnChief.SizeRatio = 20;
            pnPhysicalMain.SizeRatio = 35;
            pnDiag.SizeRatio = 35;
            pnPhysicalMain.Height = 120;
            pnDiag.Height = 120;
            spOrderItem.Height = 150;
            spOrderOrders.Height = 300;
            spOrder.HeaderHeight = 0;
            splitContainer1.SplitterDistance = 370; // <-- Set Panel1 width to 400 pixels (adjust as needed)
            btnSubmit.Visible = false;

            setControlChkDrugSet();
            scMain.Show();
            pnVitalSignUC.Show();
            this.ResumeLayout();
        }
    }
}
