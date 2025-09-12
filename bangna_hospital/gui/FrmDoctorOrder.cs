using AutocompleteMenuNS;
using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SplitContainer;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;
using static C1.Win.C1Editor.Strings.ToolStrips;
using Item = bangna_hospital.object1.Item;

namespace bangna_hospital.gui
{
    public partial class FrmDoctorOrder : Form
    {
        BangnaControl BC;
        Font fEdit, fEditB, fEdit3B, fEdit5B, fPrnBil, famtB, fEditS, famtB30;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1FlexGrid grfVS, grfLab, grfOrder, grfXray, grfProcedure, grfDrugSet, grfDrugAllergy, grfChronic, grfVital;
        //C1PdfDocument pdfDoc;
        C1ThemeController theme1;
        Label lbLoading;
        Patient PTT;
        Visit VS;
        AutoCompleteStringCollection AUTODrug, AUTOLab, AUTOXray, AUTOProcedure, acmApmTime, autoApm ;
        AutocompleteMenu AUTOChief, AUTOPyhsical, AUTODIAG, AUTODTRADVICE;
        String PRENO = "", VSDATE = "", HN = "", DTRCODE="";
        int colDrugOrderId = 1, colDrugOrderDate = 2, colDrugName = 3, colDrugOrderQty = 4, colDrugOrderFre = 5, colDrugOrderIn1 = 6, colDrugOrderMed = 7;
        int colLabDate = 1, colLabName = 2, colLabNameSub = 3, colLabResult = 4, colInterpret = 5, colNormal = 6, colUnit = 7;
        int colXrayDate = 1, colXrayCode = 2, colXrayName = 3, colXrayResult = 4;
        int colProcCode = 1, colProcName = 2, colProcReqDate = 3, colProcReqTime = 4;
        int colgrfOrderCode = 1, colgrfOrderName = 2, colgrfOrderStatus = 3, colgrfOrderQty = 4, colgrfOrderDrugUsing=5, colgrfOrderDrugFre = 6, colgrfOrderDrugPrecau = 7, colgrfOrderDrugIndica = 8, colgrfOrderDrugInterac = 9, colgrfOrderID = 10, colgrfOrderReqNO = 11, colgrfOrderReqDate = 12, colgrfOrdFlagSave = 13;
        int colgrfDrugSetItemCode = 1, colgrfDrugSetItemName = 2, colgrfDrugSetFreq = 3, colgrfDrugSetPrecau = 4, colgrfDrugSetInterac = 5, colgrfDrugSetItemStatus = 6, colgrfDrugSetItemQty = 7, colgrfDrugSetID = 8, colgrfDrugSetFlagSave = 9, colgrfDrugSetIndica = 10;
        int colgrfWeight=1, colgrfHeight = 2, colgrfTemp = 5, colgrfBSA = 3, colgrfBMI = 4, colgrfPulse = 6, colgrfRespiratory = 7, colgrfSystolic = 8, colgrfDiastolic = 9, colgrfMeanBP = 10, colgrfO2Sat = 11, colgrfGlucometer = 12;
        Boolean isLoad = false;
        MedicalDrawingForm drawingForm;
        C1TextBox rtbCheif, rtbPhysical, rtbDiag, rtbDtrAdvice;
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
            setEvent();
            setControlPnPateint();

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
        }
        private void initControl()
        {
            theme1 = new C1ThemeController();
            AUTODrug = new AutoCompleteStringCollection();
            AUTOLab = new AutoCompleteStringCollection();
            AUTOXray = new AutoCompleteStringCollection();
            AUTOProcedure = new AutoCompleteStringCollection();
            //AUTODrug = await Task.Run(() => BC.bcDB.pharM01DB.getlDrugAllTherd());
            
            AUTODrug = BC.bcDB.pharM01DB.getlDrugAll();
            AUTOLab = BC.bcDB.labM01DB.getlLabAll();
            AUTOXray = BC.bcDB.xrayM01DB.getlLabAll();
            AUTOProcedure = BC.bcDB.pm30DB.getlProcedureAll();
            BC.bcDB.drugSetDB.setCboDrugSet(cboDrugSetName, DTRCODE, "");
            pnDrugSet.Hide();

            initGrfDrugAllergy();
            initGrfChronic();
            initGrfDrugSet();
            initLoading();
            initRichTextChief();
            initRichTextPhysical();
            initRichTextDiag();
            initRichTextDtrAdvice();
            txtSearchItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchItem.AutoCompleteCustomSource = AUTODrug;

            initGrfOrder(ref grfOrder, ref pnOrder, "grfOrder");
            // ในฟอร์มหลัก (เช่น FrmDoctorOrder หรือฟอร์มที่มี pnPhysicalDraw)
            drawingForm = new MedicalDrawingForm(BC, DTRCODE, HN, VSDATE, PRENO,PTT);
            drawingForm.Dock = DockStyle.Fill;
            pnPhysicalDraw.Controls.Add(drawingForm);
            //drawingForm.DrawingBoxLoad();
        }
        private void setEvent()
        {
            chkItemLab.Click += ChkItemLab_Click;
            chkItemXray.Click += ChkItemXray_Click;
            chkItemProcedure.Click += ChkItemHotC_Click;
            chkItemDrug.Click += ChkItemDrug_Click;
            
            txtSearchItem.KeyUp += TxtSearchItem_KeyUp;
            txtSearchItem.Enter += TxtSearchItem_Enter;
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
            btnOrderSubmit.Click += BtnOrderSubmit_Click;
            btnOperItemSearch.Click += BtnOperItemSearch_Click;
            btnItemAdd.Click += BtnItemAdd_Click;
            btnPrnStaffNote.Click += BtnPrnStaffNote_Click;
            btnDrugSetAll.Click += BtnDrugSetAll_Click;
            cboDrugSetName.SelectedItemChanged += CboDrugSetName_SelectedItemChanged;

            txtUsing.KeyUp += TxtUsing_KeyUp;
            tabMain.SelectedTabChanged += TabMain_SelectedTabChanged;
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
                    rtbCheif.Text = dt.Rows[0]["chief_compliant"].ToString();
                    rtbPhysical.Text = dt.Rows[0]["physical_exam"].ToString();
                    rtbDiag.Text = dt.Rows[0]["diagnosis"].ToString();
                    rtbDtrAdvice.Text = dt.Rows[0]["doctor_advice"].ToString();
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
                rowa[colgrfOrderID] = "";
                rowa[colgrfOrderReqNO] = "";
                rowa[colgrfOrdFlagSave] = "0";
            }
        }
        private void CboDrugSetName_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (isLoad) return;
            if (cboDrugSetName.SelectedItem == null) return;
            pnDrugSet.Visible = true;
            setGrfDrugSet(((ComboBoxItem)(cboDrugSetName.SelectedItem)).Text);
        }
        private void initGrfDrugSet()
        {
            grfDrugSet = new C1FlexGrid();
            grfDrugSet.Font = fEdit;
            grfDrugSet.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDrugSet.Location = new System.Drawing.Point(0, 0);
            grfDrugSet.Cols.Count = 11;
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
            grfDrugSet.Cols[colgrfDrugSetFreq].Caption = "วิธีใช้ /ความถี่ในการใช้ยา";
            grfDrugSet.Cols[colgrfDrugSetPrecau].Caption = "ข้อบ่งชี้ /ข้อควรระวัง";
            grfDrugSet.Cols[colgrfDrugSetInterac].Caption = "ปฎิกิริยาต่อยาอื่น";
            grfDrugSet.Cols[colgrfDrugSetIndica].Caption = "ข้อบ่งชี้";
            grfDrugSet.Cols[colgrfDrugSetID].Visible = false;
            grfDrugSet.Cols[colgrfDrugSetFlagSave].Visible = false;
            grfDrugSet.Cols[colgrfDrugSetItemStatus].Visible = false;

            grfDrugSet.Cols[colgrfDrugSetItemCode].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetItemName].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetItemQty].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetFreq].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetPrecau].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetInterac].AllowEditing = false;
            grfDrugSet.Cols[colgrfDrugSetIndica].AllowEditing = false;

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
                rowa[colgrfDrugSetItemQty] = row1["qty"].ToString();
                rowa[colgrfDrugSetItemStatus] = row1["status_item"].ToString();
                rowa[colgrfDrugSetFreq] = row1["frequency"].ToString();
                rowa[colgrfDrugSetPrecau] = row1["precautions"].ToString();
                rowa[colgrfDrugSetInterac] = row1["interaction"].ToString();
                rowa[colgrfDrugSetIndica] = row1["indication"].ToString();
                rowa[colgrfDrugSetFlagSave] = "0";
                i++;
            }
            hideLbLoading();
        }
        private void BtnPrnStaffNote_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //printStaffNote();
        }
        private void BtnItemAdd_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfOrderItem();
        }
        private void BtnOrderSubmit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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
                    DataTable dtdrug = new DataTable();
                    DataTable dtlab = new DataTable();
                    DataTable dtxray = new DataTable();
                    DataTable dtprocedure = new DataTable();
                    dtdrug = BC.bcDB.pharT06DB.selectbyHNReqNo(txtPttHN.Text.Trim(), reqno[4], reqno[3]);
                    dtlab = BC.bcDB.labT02DB.selectbyHNReqNo(txtPttHN.Text.Trim(), reqno[4], reqno[0]);
                    dtxray = BC.bcDB.xrayT02DB.selectbyHNReqNo(txtPttHN.Text.Trim(), reqno[4], reqno[1]);
                    dtprocedure = BC.bcDB.pt16DB.selectbyHNReqNo(txtPttHN.Text.Trim(), reqno[4], reqno[2]);
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
                            new LogWriter("e", "FrmDoctorOrder BtnOrderSubmit_Click " + ex.Message);
                            BC.bcDB.insertLogPage(BC.userId, this.Name, "BtnOrderSubmit_Click ", ex.Message);
                        }
                    }
                }
            }
            hideLbLoading();
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
            {                if (txtSearchItem.Text.Trim().Length <= 0) return;                setOrderItem();                txtItemCode.Focus();            }
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
            txtSearchItem.AutoCompleteCustomSource = AUTODrug;
            cboDrugSetName.Show(); lbDrugSet.Show(); pnDrugSet.Show(); btnDrugSetAll.Show();
            txtDrugNum.Show(); txtDrugNumDay.Show(); txtDrugPerDay.Show(); clearControlOrder();
            txtSearchItem.Focus();
        }
        private void ChkItemDrug_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlCHkItemDrug();
        }
        private void ChkItemHotC_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteCustomSource = AUTOProcedure;
            cboDrugSetName.Hide(); lbDrugSet.Hide(); pnDrugSet.Hide(); btnDrugSetAll.Hide();
            txtDrugNum.Hide(); txtDrugNumDay.Hide(); txtDrugPerDay.Hide(); clearControlOrder();
            txtSearchItem.Focus();
        }
        private void ChkItemXray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteCustomSource = AUTOXray;
            cboDrugSetName.Hide(); lbDrugSet.Hide(); pnDrugSet.Hide(); btnDrugSetAll.Hide();
            txtDrugNum.Hide(); txtDrugNumDay.Hide(); txtDrugPerDay.Hide(); clearControlOrder();
            txtSearchItem.Focus();
        }
        private void ChkItemLab_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtSearchItem.AutoCompleteCustomSource = AUTOLab; cboDrugSetName.Hide(); lbDrugSet.Hide(); pnDrugSet.Hide();
            btnDrugSetAll.Hide(); txtDrugNum.Hide(); txtDrugNumDay.Hide(); txtDrugPerDay.Hide();
            clearControlOrder(); txtSearchItem.Focus();
        }
        private void clearControlOrder()
        {
            txtItemCode.Value = ""; txtSearchItem.Value = ""; lbItemName.Text = ""; txtIndication.Value = "";
            lbItemNameThai.Text = ""; lbTradeName.Text = ""; txtFrequency.Value = ""; txtPrecautions.Value = "";
            txtIndication.Value = ""; txtInteraction.Value = ""; lbStrength.Text = ""; txtItemQTY.Value = "";
            txtDrugNum.Value = ""; txtDrugPerDay.Value = ""; txtDrugNumDay.Value = "";
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
                drug = BC.bcDB.pharM01DB.SelectNameByPk1(code);
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
        private Boolean chkDupOrderItem(String code, String flag)
        {
            if (grfOrder == null) { return false; }
            if (grfOrder.Rows.Count <= 1) { return false; }
            foreach (Row row in grfOrder.Rows)
            {
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

            pnDiag.Controls.Add(rtbDiag);
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

            pnDtrAdvice.Controls.Add(rtbDtrAdvice);
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
            grf.Cols.Count = 14;
            grf.Cols[colgrfOrderCode].Width = 100;
            grf.Cols[colgrfOrderName].Width = 400;
            grf.Cols[colgrfOrderQty].Width = 70;
            grf.Cols[colgrfOrderDrugUsing].Width = 300;
            grf.Cols[colgrfOrderDrugFre].Width = 300;
            grf.Cols[colgrfOrderDrugPrecau].Width = 300;
            grf.Name = grfname;
            grf.ShowCursor = true;
            grf.Cols[colgrfOrderCode].Caption = "code";
            grf.Cols[colgrfOrderName].Caption = "name";
            grf.Cols[colgrfOrderQty].Caption = "qty";
            grf.Cols[colgrfOrderReqNO].Caption = "reqno";
            grf.Cols[colgrfOrderDrugUsing].Caption = "Using";
            grf.Cols[colgrfOrderDrugFre].Caption = "Frequency";
            grf.Cols[colgrfOrderDrugPrecau].Caption = "Precautions";
            grf.Cols[colgrfOrderDrugIndica].Caption = "indication";
            grf.Cols[colgrfOrderDrugInterac].Caption = "interaction";

            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grf.Cols[colgrfOrderCode].DataType = typeof(String);
            grf.Cols[colgrfOrderName].DataType = typeof(String);
            grf.Cols[colgrfOrderQty].DataType = typeof(String);
            grf.Cols[colgrfOrderDrugUsing].DataType = typeof(String);
            grf.Cols[colgrfOrderDrugFre].DataType = typeof(String);
            grf.Cols[colgrfOrderDrugPrecau].DataType = typeof(String);
            grf.Cols[colgrfOrderDrugIndica].DataType = typeof(String);
            grf.Cols[colgrfOrderDrugInterac].DataType = typeof(String);

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
            if (grfname.Equals("grfOrder"))
            {
                grf.Cols[colgrfOrderQty].Visible = true;
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
                id = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderID].ToString();
                txtOrderId.Value = id;
                txtItemCode.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderCode].ToString();
                lbItemName.Text = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderName].ToString();
                txtUsing.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugUsing].ToString();
                txtFrequency.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugFre].ToString();
                txtPrecautions.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugPrecau].ToString();
                txtInteraction.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugInterac].ToString();
                txtIndication.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderDrugIndica].ToString();
                txtItemQTY.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderQty].ToString();
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
                deleteGrfOrderTemp();
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
                    rowa[colgrfOrderID] = row1["id"].ToString();
                    rowa[colgrfOrderDrugFre] = row1["frequency"].ToString();
                    rowa[colgrfOrderDrugPrecau] = row1["precautions"].ToString();
                    rowa[colgrfOrderDrugIndica] = "";
                    rowa[colgrfOrderDrugInterac] = "";
                    rowa[colgrfOrderReqNO] = "";
                    rowa[colgrfOrdFlagSave] = "1";//ดึงข้อมูลจาก table temp_order 
                    rowa[0] = i.ToString();
                    rowa.StyleNew.BackColor = ColorTranslator.FromHtml(BC.iniC.grfRowColor);
                    i++;
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmOPD setGrfOrder " + ex.Message);
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
        private void FrmDoctorOrder_Load(object sender, EventArgs e)
        {
            this.Text = "last update 20250910";
            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            scMain.HeaderHeight = 0;
            c1SplitterPanel6.SizeRatio= 70;
            c1SplitterPanel2.SizeRatio = 30;
            spOrder.HeaderHeight = 0;
            this.ResumeLayout();
        }
    }
}
