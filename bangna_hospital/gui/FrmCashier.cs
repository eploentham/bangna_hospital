using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using C1.C1Pdf;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
//using GrapeCity.ActiveReports.Document.Section;
//using GrapeCity.ActiveReports.PageReportModel;
using iText.Layout;
using iTextSharp.text;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static CSJ2K.j2k.codestream.HeaderInfo;
using DataRow = System.Data.DataRow;
using Document = iText.Layout.Document;
using Font = System.Drawing.Font;
using Image = System.Drawing.Image;
using Row = C1.Win.C1FlexGrid.Row;
using TextBox = System.Windows.Forms.TextBox;

namespace bangna_hospital.gui
{
    public partial class FrmCashier : Form
    {
        BangnaControl bc;
        System.Drawing.Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Patient PTT;
        Visit VS;
        C1ThemeController theme1;
        C1FlexGrid grfOperList, grfFinishOPD, grfFinishIPD, grfInv, grfInvIPD, grfExp, grfMasterDrug, grfMasterSimb2;
        DataTable DTINV = new DataTable(), DTPRINT, DTSIMB2;
        Timer timeOperList;
        Label lbLoading;
        TextBox txttaxid, txtCompName, txtAddr1, txtAddr2;
        String PRENO = "", VSDATE = "", HN = "", PAIDCODE="", ANNO="", CARDTXT = "", DTRCODE = "", DISCHARGEDATE = "", TABVSACTIVE = "", TABFINISHACTIVE = "", PTTNAME="", VSTIME="", DOCNO="", DOCDATE="", FNCODE="";
        Boolean FLAGNEWSIMB2 = false, isLoad = false;
        TExpenseDB expenseDB;
        TSupraDB supraDB;
        Simb2BillingGroupDB simb2DB;
        List<Simb2BillingGroup> listSimb2;
        int colgrfOperListHn = 1, colgrfOperListVn=2, colgrfOperListFullNameT = 3, colgrfOperListSymptoms = 4, colgrfOperListPaidName = 5, colgrfOperListPreno = 6, colgrfOperListVsDate = 7, colgrfOperListVsTime = 8, colgrfOperListActNo = 9, colgrfOperListDtrName = 10, colgrfOperListPha = 11, colgrfOperListLab = 12, colgrfOperListXray = 13;
        int colgrfFinishHn = 1, colgrfFinishFullNameT = 2, colgrfFinishPaidName = 3, colgrfFinishSymptoms = 4, colgrfFinishPreno = 5, colgrfFinishVsDate = 6, colgrfFinishVsTime = 7, colgrfFinishActNo = 8, colgrfFinishDocno = 9, colgrfFinishAN = 10, colgrfFinishPaidCode=11, colgrfFinishDocdate=12;
        int colgrfExpid=1, colgrfExpDesc=2, colgrfExpAmount=3, colgrfExpModule=4, colgrfExpUser=5, colgrfExpDateReq=6;
        int colgrfDrugCode = 1, colgrfDrugName = 2, colgrfDrugFnCd = 3, colgrfDrugFnName=4, colgrfDrugSimbcode = 5, colgrfDrugChargeCD = 6, colgrfDrugCodeBSG=7, colgrfDrugBillingSubGroupTH=8;
        int colgrfSimb2GID = 1, colgrfSimb2CodeBG = 2, colgrfSimb2BillGTH = 3, colgrfSimb2BillGEN = 4, colgrfSimb2CodeBSG = 5, colgrfSimb2CodeSubGTH = 6, colgrfSimb2CodeSubGEN=7;
        
        private int currentPrintRow = 0, pagecur = 1, currentPrintPage=0;
        private const int MAX_ROWS_PER_PAGE = 10;
        StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center };
        StringFormat sfRight = new StringFormat { Alignment = StringAlignment.Far };
        StringFormat sfLeft = new StringFormat { Alignment = StringAlignment.Near };
        // === FONTS ===
        PrivateFontCollection pfc = new PrivateFontCollection();
        Font fntTitle, fntNormal, fntNormal1, fntSubTitle, fntBold, fntSmall, fntStamp;

        float colDesc , colDescW, colAmount, colAmountW = 90f, colDiscount, colDiscountW = 90f, colNet, colNetW= 90f, rowH = 20f;
        float margin = 40f, logoW = 80f, logoH = 80f, imgRatio=0, boxRatio=0;
        
        decimal totalNet = 0;
        String prndate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), PRINTORIGINAL="";
        Image logo1 = null, logo=null;
        FontFamily ff;
        AutoCompleteStringCollection acb;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern IntPtr GetFocus();
        public FrmCashier(BangnaControl bc)
        {
            this.bc = bc; this.PTT = null; InitializeComponent();
            initConfig();
        }
        public FrmCashier(BangnaControl bc, Patient ptt)
        {
            this.bc = bc; this.PTT = ptt; InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pfc.AddFontFile(@"THSarabunNew.ttf");
            ff = pfc.Families[0];
            fntTitle = new Font(ff, (16+3), FontStyle.Bold);
            fntSubTitle = new Font(ff, 14, FontStyle.Bold);
            fntNormal = new Font(ff, (13+1), FontStyle.Regular);
            fntNormal1 = new Font(ff, (13), FontStyle.Regular);
            fntBold = new Font(ff, (13+1), FontStyle.Bold);
            fntSmall = new Font(ff, 11, FontStyle.Regular);
            fntStamp = new Font(ff, 36, FontStyle.Bold);
            //new LogWriter("d", this.Name + " initConfig logo path " + Environment.CurrentDirectory + "\\LOGO-BW-tran.jpg"); 
            logo1 = System.Drawing.Image.FromFile(Environment.CurrentDirectory + "\\LOGO-BW-tran.jpg");
            logo = (System.Drawing.Image)logo1.Clone();
            logo1.Dispose();
            acb = new AutoCompleteStringCollection();
            imgRatio = (float)logo.Width / logo.Height;
            boxRatio = logoW / logoH;
            isLoad = true;
            theme1 = new C1ThemeController();
            timeOperList = new Timer();
            timeOperList.Interval = Math.Max(1000, bc.timerCheckLabOut * 1000);             // enforce >= 1s
            timeOperList.Enabled = false;
            initFont();
            initLoading();
            setEvent(); setTheme();
            initGrfFinish(ref grfFinishOPD, ref pnGrfFinishView);
            initGrfFinish(ref grfFinishIPD, ref pnGrfFinishViewIPD);
            initGrfInv(ref grfInv, ref pnGrfFinishInv);
            initGrfInv(ref grfInvIPD, ref pnGrfFinishInvIPD);
            initGrfOperList();
            initGrfExp();
            //timeOperList.Enabled = true;
            //timeOperList.Start();
            TABFINISHACTIVE = tabFinishOPD.Name;
            isLoad = false;
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
        private void initFont()
        {
            // ใช้ค่าจาก config และกำหนด fallback/default font name/size
            string grdFontName = string.IsNullOrEmpty(bc.iniC.grdViewFontName) ? "Tahoma" : bc.iniC.grdViewFontName;
            int grdFontSize = bc.grdViewFontSize > 0 ? bc.grdViewFontSize : 10;
            string pdfFontName = string.IsNullOrEmpty(bc.iniC.pdfFontName) ? "Tahoma" : bc.iniC.pdfFontName;
            int pdfFontSize = bc.pdfFontSize > 0 ? bc.pdfFontSize : 10;
            string queFontName = string.IsNullOrEmpty(bc.iniC.queFontName) ? "Tahoma" : bc.iniC.queFontName;
            int queFontSize = bc.queFontSize > 0 ? bc.queFontSize : 10;

            fEdit = new System.Drawing.Font(grdFontName, grdFontSize, FontStyle.Regular);
            fEditB = new System.Drawing.Font(grdFontName, grdFontSize, FontStyle.Bold);
            fEdit2 = new Font(grdFontName, grdFontSize + 2, FontStyle.Regular);
            fEdit2B = new Font(grdFontName, grdFontSize + 2, FontStyle.Bold);
            fEdit3B = new Font(grdFontName, grdFontSize + 3, FontStyle.Bold);
            fEdit5B = new Font(grdFontName, grdFontSize + 5, FontStyle.Bold);

            famt1 = new Font(pdfFontName, pdfFontSize + 1, FontStyle.Regular);
            famt5 = new Font(pdfFontName, pdfFontSize + 5, FontStyle.Regular);
            famt7B = new Font(pdfFontName, pdfFontSize + 7, FontStyle.Bold);
            famtB14 = new Font(pdfFontName, pdfFontSize + 14, FontStyle.Bold);
            famtB30 = new Font(pdfFontName, pdfFontSize + 30, FontStyle.Bold);
            ftotal = new Font(pdfFontName, pdfFontSize + 60, FontStyle.Bold);
            fPrnBil = new Font(pdfFontName, pdfFontSize, FontStyle.Regular);
            fEditS = new Font(pdfFontName, Math.Max(pdfFontSize - 2, 6), FontStyle.Regular);
            fEditS1 = new Font(pdfFontName, Math.Max(pdfFontSize - 1, 6), FontStyle.Regular);

            fPDF = new Font(pdfFontName, pdfFontSize, FontStyle.Regular);
            fPDFs2 = new Font(pdfFontName, Math.Max(pdfFontSize - 2, 6), FontStyle.Regular);
            fPDFl2 = new Font(pdfFontName, pdfFontSize + 2, FontStyle.Regular);
            fPDFs6 = new Font(pdfFontName, Math.Max(pdfFontSize - 6, 6), FontStyle.Regular);
            fPDFs8 = new Font(pdfFontName, Math.Max(pdfFontSize - 8, 6), FontStyle.Regular);

            fque = new Font(queFontName, queFontSize + 3, FontStyle.Bold);
            fqueB = new Font(queFontName, queFontSize + 7, FontStyle.Bold);
        }
        private void setTheme()
        {
            //theme1.SetTheme(pnPtt, "ExpressionLight");
            //foreach (Control c in pnPtt.Controls) if (c is C1TextBox) theme1.SetTheme(c, "ExpressionLight");
        }
        private void setEvent()
        {
            tabMain.SelectedTabChanged += TabMain_SelectedTabChanged;
            scFinish.SelectedTabChanged += ScFinish_SelectedTabChanged;
            rgPrn.Click += RgPrn_Click;
            rgSearch.KeyUp += RgSearch_KeyUp;
            rgPrn1.Click += RgPrn1_Click;
            rgPrnReceipt.Click += RgPrnReceipt_Click;
            timeOperList.Tick += TimeOperList_Tick;
            btnExpenseSave.Click += BtnExpenseSave_Click;
            txtAmount.KeyPress += TxtAmount_KeyPress;
            btnSimb2New.Click += BtnSimb2New_Click;
            btnSimb2Save.Click += BtnSimb2Save_Click;
            btnSimb2MapNew.Click += BtnSimb2MapNew_Click;
            BtnSimb2NewSave.Click += BtnSimb2NewSave_Click;
            chkMastersupply.Click += ChkMastersupply_Click;
            chkMasterDrug.Click += ChkMasterDrug_Click;
            chkMasterLab.Click += ChkMasterLab_Click;
            chkMasterXray.Click += ChkMasterXray_Click;
            chkMasterDf.Click += ChkMasterDf_Click;
            chkMasterProc.Click += ChkMasterProc_Click;
            chkMasterFncode.Click += ChkMasterFncode_Click;
            txtMasterSearch.KeyUp += TxtMasterSearch_KeyUp;
            rgPrnReceiptDetail.Click += RgPrnReceiptDetail_Click;
            ribbonDatePicker1.DropDownClosed += RibbonDatePicker1_DropDownClosed;
        }

        private void ScFinish_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TABFINISHACTIVE = scFinish.SelectedTab.Name;
            HN = "";
            PRENO = "";
            PTTNAME = "";
            VSDATE = "";
            VSTIME = "";
            DOCDATE = "";
            DOCNO = "";
            PAIDCODE = "";
            ANNO = "";
        }
        private void RibbonDatePicker1_DropDownClosed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (TABVSACTIVE.Equals(tabFinish.Name))
            {
                setGrfFinish(ribbonDatePicker1.Value.Year + "-" + ribbonDatePicker1.Value.ToString("MM-dd"));
            }
        }

        private void ShowDataTableInGrid(DataTable dt)
        {
            Form frmView = new Form();
            frmView.Text = "DTPRINT Data Viewer";
            frmView.Size = new Size(1000, 600);
            frmView.StartPosition = FormStartPosition.CenterParent;
            Panel pntop = new Panel();
            pntop.Dock = DockStyle.Top;
            pntop.Height = 40;
            Panel pngrf = new Panel();
            pngrf.Dock = DockStyle.Fill;
            ComboBox cboDiscount = new ComboBox();
            cboDiscount.Items.Add("ส่วนลดค่ายาทั้งหมด %");
            cboDiscount.Items.Add("ส่วนลดค่ายาทั้งหมด บาท");
            cboDiscount.Top = 10;
            Label lbDiscount = new Label();
            lbDiscount.Text = "ส่วนลด";
            lbDiscount.Top = 10;
            lbDiscount.Left = 50;
            cboDiscount.Left = 150;
            cboDiscount.Font = fEdit;
            lbDiscount.Font = fEdit;
            cboDiscount.Width = 200;
            TextBox txtDiscount = new TextBox();
            txtDiscount.Top = cboDiscount.Top;
            txtDiscount.Left = cboDiscount.Left + cboDiscount.Width + 10;
            txtDiscount.Font = fEdit;
            TextBox txtamt = new TextBox();
            txtamt.Top = cboDiscount.Top;
            txtamt.Left = txtDiscount.Left + txtDiscount.Width + 10;
            txtamt.Font = fEdit;
            pntop.Controls.Add(cboDiscount);
            pntop.Controls.Add(lbDiscount);
            pntop.Controls.Add(txtDiscount);
            pntop.Controls.Add(txtamt);

            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.DataSource = dt;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.ReadOnly = false;
            
            frmView.Controls.Add(pngrf);
            frmView.Controls.Add(pntop);
            pngrf.Controls.Add(dgv);
            dgv.DataBindingComplete += (s, e) =>
            {
                dgv.Columns["doc_no"].Visible = false;
                dgv.Columns["doc_date"].Visible = false;
                dgv.Columns["finance_code"].Visible = false;
                dgv.Columns["req_no"].Visible = false;
                dgv.Columns["req_date"].Visible = false;
                dgv.Columns["hn"].Visible = false;
                dgv.Columns["patient_name"].Visible = false;
                dgv.Columns["preno"].Visible = false;
                dgv.Columns["visitdate"].Visible = false;
                dgv.Columns["symptom"].Visible = false;
                dgv.Columns["vn_no"].Visible = false;
                dgv.Columns["vn_seq"].Visible = false;
                dgv.Columns["an_no"].Visible = false;
                dgv.Columns["an_year"].Visible = false;
                dgv.Columns["paid_name"].Visible = false;
                dgv.Columns["visit_time"].Visible = false;
                dgv.Columns["seq"].Visible = false;
                dgv.Columns["service_type"].Visible = true;
                dgv.Columns["item_code"].Visible = true;
                dgv.Columns["item_name"].Visible = true;
                dgv.Columns["item_name_e"].Visible = false;
                dgv.Columns["location_simb2"].Visible = false;
                dgv.Columns["location1"].Visible = false;
                dgv.Columns["ID"].Visible = false;
                dgv.Columns["CodeBG"].Visible = false;
                dgv.Columns["BillingGroupTH"].Visible = false;
                dgv.Columns["BillingGroupEN"].Visible = false;
                dgv.Columns["CodeBSG"].Visible = true;
                dgv.Columns["CreatedDate"].Visible = false;
                dgv.Columns["active"].Visible = false;
                dgv.Columns["status_master"].Visible = false;
                dgv.Columns["date_modi"].Visible = false;
                dgv.Columns["date_cancel"].Visible = false;
                dgv.Columns["unit"].Visible = false;
                dgv.Columns["CodeBSG1"].Visible = false;
                dgv.Columns["BillingSubGroupTH"].Visible = false;
                dgv.Columns["BillingSubGroupEN"].Visible = false;
                dgv.Columns["discount"].ReadOnly = false;
                dgv.Columns["item_name"].ReadOnly = true;
                dgv.Columns["qty"].ReadOnly = true;
                dgv.Columns["unit_price"].ReadOnly = true; 
                dgv.Columns["total_amount"].ReadOnly = true;
                dgv.Columns["ds_date"].ReadOnly = false;
            };
            txtDiscount.KeyUp +=(s,e)=>
            {
                if (cboDiscount.SelectedIndex == 0) // %
                {
                    if (decimal.TryParse(txtDiscount.Text.Trim(), out decimal discountPercent))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["service_type"].ToString().Equals("DRUG"))
                            {
                                decimal.TryParse(row["unit_price"].ToString(), out decimal unitprice);
                                decimal.TryParse(row["qty"].ToString(), out decimal qty);
                                decimal discountAmount = (unitprice * qty) * discountPercent / 100;
                                decimal total  = unitprice * qty;
                                row["discount"] = discountAmount;
                                row["total_amount"] = total - discountAmount;
                                
                            }
                        }
                    }
                }
                else if (cboDiscount.SelectedIndex == 1) // บาท
                {
                    if (decimal.TryParse(txtDiscount.Text.Trim(), out decimal discountBaht))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["service_type"].ToString().Equals("DRUG"))
                            {
                                decimal.TryParse(row["unit_price"].ToString(), out decimal unitprice);
                                decimal.TryParse(row["qty"].ToString(), out decimal qty);
                                decimal totalAmount = unitprice * qty;
                                row["discount"] = discountBaht;
                                row["total_amount"] = totalAmount - discountBaht;
                                
                            }
                        }
                    }
                }
            };
            decimal totalAmountSum = 0;
            foreach (DataRow dataRow in dt.Rows)
            {
                decimal amt = 0;
                decimal.TryParse(dataRow["total_amount"].ToString(), out amt);
                totalAmountSum += amt;
            }
            txtamt.Text = totalAmountSum.ToString("N2");
            // ✅ Event สำหรับอัพเดท total_amount เมื่อแก้ discount
            dgv.CellValueChanged += (s, ev) =>
            {
                if (ev.RowIndex >= 0 && ev.ColumnIndex >= 0)
                {
                    string colName = dgv.Columns[ev.ColumnIndex].Name;
                    if (colName == "discount")
                    {
                        DataGridViewRow row = dgv.Rows[ev.RowIndex];
                        decimal.TryParse(row.Cells["unit_price"].Value?.ToString(), out decimal unitPrice);
                        decimal.TryParse(row.Cells["qty"].Value?.ToString(), out decimal qty);
                        decimal.TryParse(row.Cells["discount"].Value?.ToString(), out decimal discount);

                        decimal total = (unitPrice * qty) - discount;
                        row.Cells["total_amount"].Value = total;
                    }
                }
            };
            frmView.ShowDialog(this);
            // ✅ บันทึกข้อมูลกลับเข้า DTPRINT หลังปิด dialog
            foreach (DataRow dtRow in dt.Rows)
            {
                foreach (DataRow printRow in DTPRINT.Rows)
                {
                    if (printRow["item_code"].ToString() == dtRow["item_code"].ToString() &&
                        printRow["seq"].ToString() == dtRow["seq"].ToString())
                    {
                        printRow["discount"] = dtRow["discount"];
                        printRow["total_amount"] = dtRow["total_amount"];
                        break;
                    }
                }
            }
            DTPRINT.AcceptChanges();
        }
        private void RgPrnReceiptDetail_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setLbLoading("กำลังโหลดข้อมูล ...");
            showLbLoading();
            try
            {
                lfSbMessage.Text = DOCNO;
                DTPRINT = bc.bcDB.getReceipt(DOCNO, DOCDATE);
                DISCHARGEDATE = bc.bcDB.vsDB.selectDSDateAN1(HN, VSDATE, PRENO);
                setLbLoading("กำลังโหลดข้อมูล ...setgrid");
                DateTime dsdate = new DateTime();
                if ((DTPRINT != null) && (DTPRINT.Rows.Count > 0))
                {
                    if (DTPRINT.Rows[0].Table.Columns.Contains("ds_date"))
                    {
                        object dsDateValue = DTPRINT.Rows[0]["ds_date"];
                        dsdate = (DateTime)dsDateValue;
                        DISCHARGEDATE = dsdate.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                    }
                    string usrno = "";
                    DataTable dtuft01 = bc.bcDB.fint01DB.SelectBillByDocno(DOCNO, DOCDATE);
                    if (dtuft01.Rows.Count > 0) usrno = dtuft01.Rows[0]["MNC_USR_CD"].ToString();
                    //if (!DTPRINT.Columns.Contains("amount")) DTPRINT.Columns.Add("amount", typeof(decimal));
                    ShowDataTableInGrid(DTPRINT); // ✅ แสดงข้อมูลใน grid
                    if (!DTPRINT.Columns.Contains("MNC_VN_NO")) DTPRINT.Columns.Add("MNC_VN_NO", typeof(string));
                    if (!DTPRINT.Columns.Contains("MNC_VN_SEQ")) DTPRINT.Columns.Add("MNC_VN_SEQ", typeof(string));
                    if (!DTPRINT.Columns.Contains("MNC_DATE")) DTPRINT.Columns.Add("MNC_DATE", typeof(DateTime));
                    if (!DTPRINT.Columns.Contains("MNC_FN_TYP_DSC")) DTPRINT.Columns.Add("MNC_FN_TYP_DSC", typeof(string));
                    if (!DTPRINT.Columns.Contains("MNC_SHIF_MEMO")) DTPRINT.Columns.Add("MNC_SHIF_MEMO", typeof(string));
                    if (!DTPRINT.Columns.Contains("MNC_TIME")) DTPRINT.Columns.Add("MNC_TIME", typeof(string));
                    if (!DTPRINT.Columns.Contains("MNC_PAY_CASH")) DTPRINT.Columns.Add("MNC_PAY_CASH", typeof(string));
                    if (!DTPRINT.Columns.Contains("MNC_NO")) DTPRINT.Columns.Add("MNC_NO", typeof(string));
                    if (!DTPRINT.Columns.Contains("MNC_DEF_DSC")) DTPRINT.Columns.Add("MNC_DEF_DSC", typeof(string));
                    if (!DTPRINT.Columns.Contains("MNC_FN_AMT")) DTPRINT.Columns.Add("MNC_FN_AMT", typeof(string));
                    if (!DTPRINT.Columns.Contains("MNC_FN_DIS")) DTPRINT.Columns.Add("MNC_FN_DIS", typeof(string));
                    if (!DTPRINT.Columns.Contains("MNC_SIMB_CD")) DTPRINT.Columns.Add("MNC_SIMB_CD", typeof(string));
                    if (!DTPRINT.Columns.Contains("MNC_DOC_NO")) DTPRINT.Columns.Add("MNC_DOC_NO", typeof(string));
                    if (!DTPRINT.Columns.Contains("MNC_DOC_DAT")) DTPRINT.Columns.Add("MNC_DOC_DAT", typeof(DateTime));
                    if (!DTPRINT.Columns.Contains("MNC_USR_CD")) DTPRINT.Columns.Add("MNC_USR_CD", typeof(string));
                    //if (!DTPRINT.Columns.Contains("discount")) DTPRINT.Columns.Add("discount", typeof(decimal));
                    //if (DTPRINT.Columns.Contains("unit_price"))
                    //{
                    //    int unitPriceIndex = DTPRINT.Columns["unit_price"].Ordinal;
                    //    DTPRINT.Columns["discount"].SetOrdinal(unitPriceIndex);
                    //}
                    foreach (DataRow row in DTPRINT.Rows)
                    {
                        row["MNC_VN_NO"] = VS.VN;
                        row["MNC_VN_SEQ"] = row["vn_seq"];
                        row["MNC_DATE"] = row["visitdate"];
                        row["MNC_FN_TYP_DSC"] = row["paid_name"];
                        row["MNC_SHIF_MEMO"] = row["symptom"];
                        row["MNC_TIME"] = row["visit_time"];
                        row["MNC_DOC_NO"] = row["doc_no"];
                        row["MNC_DOC_DAT"] = row["doc_date"];
                        row["MNC_DEF_DSC"] = row["BillingSubGroupTH"];
                        row["MNC_FN_AMT"] = row["total_amount"];
                        row["MNC_USR_CD"] = usrno;
                        //row["discount"] = 0;
                    }
                    DTPRINT.AcceptChanges();
                    //new LogWriter("d", "DTPRINT rows: " + DTPRINT.Rows.Count);
                    hideLbLoading();
                    printReceiptDetailSIMB2();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                hideLbLoading();
            }
        }
        private void ChkMasterFncode_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkMasterFncode.Checked) { setGrfMasterDrug(); }
        }

        private void TxtMasterSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            grfMasterDrug.ApplySearch(txtMasterSearch.Text.Trim(), true, true, false);
        }        

        private void ChkMasterProc_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkMasterProc.Checked)            {                setGrfMasterDrug(); }
        }

        private void ChkMasterDf_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkMasterDf.Checked)            {                setGrfMasterDrug(); }
        }

        private void ChkMasterXray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkMasterXray.Checked)            {                setGrfMasterDrug(); }
        }

        private void ChkMasterLab_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkMasterLab.Checked)            {                setGrfMasterDrug(); }
        }

        private void ChkMasterDrug_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkMasterDrug.Checked)            {                setGrfMasterDrug(); }
        }

        private void ChkMastersupply_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(chkMastersupply.Checked)            {                setGrfMasterDrug();            }
        }

        private void BtnSimb2NewSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtMasterSimb2CodeBGNew.Text.Trim().Length <= 0)        { lfSbMessage.Text = "กรุณาระบุรหัส new simb2";       return; }
            if (txtMasterSimb2BillGTHNew.Text.Trim().Length <= 0)       { lfSbMessage.Text = "กรุณาระบุชื่อกลุ่มใหม่ (ไทย)";       return; }
            if (txtMasterSimb2BillGENNew.Text.Trim().Length <= 0)       { lfSbMessage.Text = "กรุณาระบุชื่อกลุ่มใหม่ (อังกฤษ)";     return; }
            if (txtMasterSimb2CodeBSGNew.Text.Trim().Length <= 0)       { lfSbMessage.Text = "กรุณาระบุรหัสกลุ่มย่อย BSG";        return; }
            if (txtMasterSimb2CodeSubGTHNew.Text.Trim().Length <= 0)    { lfSbMessage.Text = "กรุณาระบุชื่อกลุ่มย่อยใหม่ (ไทย)";    return; }
            if (txtMasterSimb2CodeSubGENNew.Text.Trim().Length <= 0)    { lfSbMessage.Text = "กรุณาระบุชื่อกลุ่มย่อยใหม่ (อังกฤษ)";  return; }
            Simb2BillingGroup chk = simb2DB.GetByCodeBG(txtMasterSimb2CodeBGNew.Text.Trim());
            if (chk != null)    {                lfSbMessage.Text = "รหัส SIMB2 นี้มีอยู่แล้วในระบบ";                return;           }
            chk = simb2DB.GetByCodeBSG(txtMasterSimb2CodeBSGNew.Text.Trim()); 
            if (chk != null)    {                lfSbMessage.Text = "รหัสกลุ่มย่อย BSG นี้มีอยู่แล้วในระบบ";              return;       }
            Simb2BillingGroup obj = new Simb2BillingGroup();
            obj.ID = 0;
            obj.CodeBG = txtMasterSimb2CodeBGNew.Text.Trim();
            obj.BillingGroupTH = txtMasterSimb2BillGTHNew.Text.Trim();
            obj.BillingGroupEN = txtMasterSimb2BillGENNew.Text.Trim();
            obj.CodeBSG = txtMasterSimb2CodeBSGNew.Text.Trim();
            obj.BillingSubGroupTH = txtMasterSimb2CodeSubGTHNew.Text.Trim();
            obj.BillingSubGroupEN = txtMasterSimb2CodeSubGENNew.Text.Trim();
            String re = simb2DB.InsertOrUpdate(obj);
            if (long.TryParse(re, out long result) && result > 0)
            {
                lfSbMessage.Text = "Insert new SIMB2 สำเร็จ";
                listSimb2.Add(obj);
                setGrfMasterSimb2();
            }
            else
            {
                lfSbMessage.Text = re;
            }
            FLAGNEWSIMB2 = false;
            btnSimb2MapNew.Enabled = true;
            btnSimb2Save.Enabled = true;
        }

        private void BtnSimb2MapNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlTabMaster();
        }

        private void BtnSimb2Save_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtMasterDrugGenericCode.Text.Trim().Length <= 0 && chkMasterFncode.Checked==false)   { lfSbMessage.Text ="กรุณาระบุรหัสยา"; return;         }
            if (txtMasterSimb2CodeBSG.Text.Trim().Length <= 0)      { lfSbMessage.Text = "กรุณาระบุ SIMB2 code"; return; }
            String re = "";
            if (chkMasterDrug.Checked)      {   re = bc.bcDB.pharM01DB.UpdateCodeBSG(txtMasterDrugGenericCode.Text.Trim(), txtMasterFnCode.Text.Trim(), txtMasterSimb2CodeBSG.Text.Trim());      }
            else if (chkMasterLab.Checked)  {   re = bc.bcDB.labM01DB.UpdateCodeBSG(txtMasterDrugGenericCode.Text.Trim(), txtMasterFnCode.Text.Trim(), txtMasterSimb2CodeBSG.Text.Trim());       }
            else if (chkMasterXray.Checked) {   re = bc.bcDB.xrayM01DB.UpdateCodeBSG(txtMasterDrugGenericCode.Text.Trim(), txtMasterFnCode.Text.Trim(), txtMasterSimb2CodeBSG.Text.Trim());      }
            else if (chkMasterProc.Checked) {   re = bc.bcDB.pm30DB.UpdateCodeBSG(txtMasterDrugGenericCode.Text.Trim(), txtMasterFnCode.Text.Trim(), txtMasterSimb2CodeBSG.Text.Trim()); }
            else if (chkMasterFncode.Checked) { re = bc.bcDB.finM01DB.updateSCodeBSG(txtMasterFnCode.Text.Trim(), txtMasterSimb2CodeBSG.Text.Trim()); }
            else if (chkMasterDf.Checked)   {   re = bc.bcDB.finM01DB.updateSCodeBSG(txtMasterFnCode.Text.Trim(), txtMasterSimb2CodeBSG.Text.Trim());       }
            if (long.Parse(re) > 0)
            {
                lfSbMessage.Text = "Update SIMB2 code สำเร็จ";
                setGrfMasterDrug();
            }
            else            {                lfSbMessage.Text = re;            }
        }
        private void BtnSimb2New_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FLAGNEWSIMB2 = true;            btnSimb2MapNew.Enabled = false;            btnSimb2Save.Enabled = false;
        }

        private void TxtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') {        e.Handled = true;      }
        }
        private void BtnExpenseSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(txtExpenseID.Text.Length <= 0)   {                lfSbMessage.Text = "not id";               return;         }
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
            frm.ShowDialog();
            frm.Dispose();
            if (bc.USERCONFIRMID.Length <= 0)   {                lfSbMessage.Text = "Password ไม่ถูกต้อง";     return;         }
            String re = expenseDB.updateAmountDraw(long.Parse("999"+txtExpenseID.Text.Trim()), txtAmount.Text.Trim().Replace(",",""), bc.USERCONFIRMID);
        }
        private void RgSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (TABVSACTIVE.Equals(tabFinish.Name))
            {
                lfSbMessage.Text = rgSearch.Text;
                if (TABFINISHACTIVE.Equals(tabFinishOPD.Name))
                {
                    grfFinishOPD.ApplySearch(rgSearch.Text.Trim(), true, true, false);
                }else if (TABFINISHACTIVE.Equals(tabFinishIPD.Name))
                {
                    grfFinishIPD.ApplySearch(rgSearch.Text.Trim(), true, true, false);
                }
            }
            else if (TABVSACTIVE.Equals(tabOper.Name)) { lfSbMessage.Text = rgSearch.Text; grfOperList.ApplySearch(rgSearch.Text.Trim(), true, true, false); }
        }
        private void TimeOperList_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfOperList(ribbonDatePicker1.Value.Year + "-" + ribbonDatePicker1.Value.ToString("MM-dd"));
        }
        private void RgPrnReceipt_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (DTINV == null || DTINV.Rows.Count <= 0)
            {
                MessageBox.Show("กรุณาเลือกข้อมูลที่ต้องการพิมพ์");
                return;
            }
            printReceipt("original/ต้นฉบับ", bc.iniC.statusPrintPreview.Equals("1") ? "preview" : "");
            printReceipt("copy/สำเนา", bc.iniC.statusPrintPreview.Equals("1") ? "preview" : "");
        }
        private void RgPrn1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //printReceipt("copy/สำเนา", bc.iniC.statusPrintPreview.Equals("1") ? "preview":"");
            if (bc.iniC.branchId.Equals("001"))
                printReceipt("ใบเสร็จรับเงิน/Receipt (Copy สำเนา)", bc.iniC.statusPrintPreview.Equals("1") ? "preview" : "");
            else if (bc.iniC.branchId.Equals("005"))
            {
                PRINTORIGINAL = "ใบเสร็จรับเงิน/Receipt (Copy สำเนา)";
                printReceiptBn5();
            }
        }
        private void RgPrn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(TABVSACTIVE.Equals(tabFinish.Name))    
            {
                if (grfFinishOPD.Row <= 0) { MessageBox.Show("กรุณาเลือกข้อมูลที่ต้องการพิมพ์"); return; }
                if (bc.iniC.branchId.Equals("001"))
                    printReceipt("original/ต้นฉบับ", bc.iniC.statusPrintPreview.Equals("1") ? "preview" : "");
                else if (bc.iniC.branchId.Equals("005"))
                {
                    PRINTORIGINAL = "ใบเสร็จรับเงิน/Receipt (Original ต้นฉบับ)";
                    printReceiptBn5();//RgPrn_Click
                }
            }
            else if(TABVSACTIVE.Equals(tabOper.Name)) 
            {       
                if (grfOperList.Row <= 0) { MessageBox.Show("กรุณาเลือกข้อมูลที่ต้องการพิมพ์"); return; }
                if (bc.iniC.branchId.Equals("001"))
                    printReceipt("copy/สำเนา", bc.iniC.statusPrintPreview.Equals("1") ? "preview" : "");
                else if (bc.iniC.branchId.Equals("005"))
                {
                    PRINTORIGINAL = "ใบเสร็จรับเงิน/Receipt (Copy สำเนา)";
                    printReceiptBn5();  //RgPrn_Click
                }
            }
        }
        private void printReceiptBn5()
        {
            showLbLoading();
            String compname = "", compaddr1 = "", compaddr2 = "";
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetByHn(HN);
            DataTable dtvs = bc.bcDB.vsDB.selectCompByPreno(HN, VSDATE, PRENO);
            DataTable dtcard = bc.bcDB.fint01DB.SelectCardRemark(DOCNO, DOCDATE);
            DISCHARGEDATE = bc.bcDB.vsDB.selectDSDateAN1(HN, VSDATE, PRENO);
            if (dtvs.Rows.Count > 0) { 
                compname = dtvs.Rows[0]["MNC_COM_DSC"] != null ? dtvs.Rows[0]["MNC_COM_PRF"].ToString()+" "+dtvs.Rows[0]["MNC_COM_DSC"].ToString() : "";
                compaddr1 = dtvs.Rows[0]["MNC_COM_ADD"] != null ? dtvs.Rows[0]["MNC_COM_ADD"].ToString() : "";
            }
            if (dtcard.Rows.Count > 0)
            {
                CARDTXT = dtcard.Rows[0]["MNC_CARD_DSC"].ToString() +" number "+ dtcard.Rows[0]["MNC_CARD_NO"].ToString() + " code " + dtcard.Rows[0]["MNC_CARD_CFM_NO"].ToString() + " Transfer amount " + dtcard.Rows[0]["MNC_CARD_AMT"].ToString();
            }
            
            Size proposedSize = new Size(100, 100);
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.WindowState = FormWindowState.Normal;
            frm.Size = new Size(800, 600);

            Button btntaxid = new Button();            btntaxid.Text = "นิติบุคคล";
            Size textSize = TextRenderer.MeasureText(btntaxid.Text, btntaxid.Font, proposedSize, TextFormatFlags.Left);
            btntaxid.Location = new System.Drawing.Point(20, 0);            btntaxid.Size = new Size(100, 30);            btntaxid.Font = fEdit;            btntaxid.Click += Btntaxid_Click;
            txttaxid = new TextBox();
            txttaxid.Name = "txttaxid";
            txttaxid.Font = fEdit;
            txttaxid.Location = btntaxid.Location;
            txttaxid.Left = btntaxid.Width + textSize.Width;
            txttaxid.Width = 300;
            txttaxid.Text = "";

            Label lbName = new Label();            lbName.Text = "ชื่อบริษัท";            lbName.Location = new System.Drawing.Point(20, 30);            lbName.Font = fEdit;
            textSize = TextRenderer.MeasureText(lbName.Text, lbName.Font, proposedSize, TextFormatFlags.Left);
            txtCompName = new TextBox();
            txtCompName.Name = "txtCompName";
            txtCompName.Font = fEdit;
            txtCompName.Location = lbName.Location;
            txtCompName.Left = lbName.Width + textSize.Width+10;
            txtCompName.Width = 400;
            txtCompName.Text = compname;
            txtCompName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCompName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCompName.AutoCompleteCustomSource = acb;

            Label lbAddr1 = new Label();            lbAddr1.Text = "ที่อยู่บริษัท1";            lbAddr1.Location = new System.Drawing.Point(20, 60);            lbAddr1.Font = fEdit;
            textSize = TextRenderer.MeasureText(lbAddr1.Text, lbName.Font, proposedSize, TextFormatFlags.Left);
            txtAddr1 = new TextBox();
            txtAddr1.Name = "txtAddr1";
            txtAddr1.Font = fEdit;
            txtAddr1.Location = lbAddr1.Location;
            txtAddr1.Left = lbAddr1.Width + textSize.Width;
            txtAddr1.Width = 400;
            txtAddr1.Text = compaddr1;

            Label lbAddr2 = new Label();            lbAddr2.Text = "ที่อยู่บริษัท2";            lbAddr2.Location = new System.Drawing.Point(20, 90);            lbAddr2.Font = fEdit;
            textSize = TextRenderer.MeasureText(lbAddr1.Text, lbName.Font, proposedSize, TextFormatFlags.Left);
            txtAddr2 = new TextBox();
            txtAddr2.Name = "txtAddr2";
            txtAddr2.Font = fEdit;
            txtAddr2.Location = lbAddr2.Location;
            txtAddr2.Left = lbAddr2.Width + textSize.Width;
            txtAddr2.Width = 400;
            txtAddr2.Text = "";

            C1FlexGrid grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Bottom;
            grf.Location = new System.Drawing.Point(10, 110);
            grf.Height = 400;
            grf.Rows.Count = 1;
            grf.Cols.Count = 4;

            frm.Controls.Add(lbName);            frm.Controls.Add(txtCompName);            frm.Controls.Add(lbAddr1);
            frm.Controls.Add(lbAddr2);            frm.Controls.Add(txtAddr1);            frm.Controls.Add(txtAddr2);
            frm.Controls.Add(grf);            frm.Controls.Add(btntaxid);            frm.Controls.Add(txttaxid);
            theme1.SetTheme(frm, bc.iniC.themeApp);
            frm.ShowDialog(this);
            
            //DataTable dtinv = new DataTable();
            float total = setPrintReceipt("",DTINV);
            //genPDFreceipt(DTINV, total);

            frm.Dispose();
            hideLbLoading();
            printReceiptSIMB2();
            lfSbMessage.Text = "พิมพ์ ใบเสร็จรับเงิน OK";
        }
        private void genReceipt(DataTable dtinv, float total)
        {
            String filename = "";
            int gapLine = 15, linenumber = 5, gapX = 40, gapY = 20, xCol1 = 20, xCol2 = 40, xCol3 = 100, xCol4 = 200, xCol5 = 250, xCol6 = 300, xCol7 = 400, xCol8 = 440;
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 36, 36, 36, 36);
            StringFormat _sfRight, _sfCenter, _sfLeft;
            float pageWidth = 553, pageHeight = 797, discount = 0;
            //float cmToInch = 0.393701f;
            // 1 mm = 2.83465 points
            //ขนาดกระดาษต่อเนื่อง w 20.5 h 28 cm
            pageWidth = 210 * 2.83465f;   // = 581.1 points     ขยับค่า 210 ต้องปรับเยอะ กระเทือนเยอะ
            pageHeight = 280 * 2.83465f;  // = 793.7 points
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void genPDFreceipt(DataTable dtinv, float total)
        {
            String filename = "";
            int gapLine = 15, linenumber = 5, gapX = 40, gapY = 20, xCol1 = 20, xCol2 = 40, xCol3 = 100, xCol4 = 200, xCol5 = 250, xCol6 = 300, xCol7 = 400, xCol8 = 440;
            C1PdfDocument pdf = new C1PdfDocument();
            StringFormat _sfRight, _sfCenter, _sfLeft;
            float pageWidth = 553, pageHeight = 797, discount=0;
            //float cmToInch = 0.393701f;
            // 1 mm = 2.83465 points
            //ขนาดกระดาษต่อเนื่อง w 20.5 h 28 cm
            pageWidth = 210 * 2.83465f;   // = 581.1 points     ขยับค่า 210 ต้องปรับเยอะ กระเทือนเยอะ
            pageHeight = 280 * 2.83465f;  // = 793.7 points

            pdf.PageSize = new SizeF(pageWidth, pageHeight);
            //new LogWriter("d", this.Name + " genPDFreceipt 00");
            _sfRight = new StringFormat();
            _sfCenter = new StringFormat();
            _sfLeft = new StringFormat();
            _sfRight.Alignment = StringAlignment.Far;
            _sfCenter.Alignment = StringAlignment.Center;
            _sfLeft.Alignment = StringAlignment.Near;
            pdf.FontType = FontTypeEnum.Embedded;
            pdf.PageSize = C1PdfDocumentBase.ToPoints(new SizeF(pageWidth, pageHeight));
            filename = HN+ "_receipt_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".pdf";
            //new LogWriter("d", this.Name + " genPDFreceipt 01" );
            try
            {
                //pdf.DrawString("โรงพยาบาล บางนา5  55 หมู่4 ถนนเทพารักษ์ ตำบลบางพลีใหญ่ อำเภอบางพลี จังหวัด สมุทรปราการ 10540", fPDF, Brushes.Black, new PointF(recf.Width + 65, linenumber += gapLine), _sfLeft);   
                String compname = "", compaddr1 = "", compaddr2 = "", invno="", pttname="",hn="",vsdate="", diadesc="",vstime="", invdate="", sypmtoms="";
                compname = txtCompName.Text.Trim();
                compaddr1 = "ที่อยู่ " + txtAddr1.Text.Trim();
                compaddr2 = " " + txtAddr2.Text.Trim();
                pdf.DrawString(txtCompName.Text.Trim() , fPDF, Brushes.Black, new PointF(xCol2-20, 35), _sfLeft);
                pdf.DrawString(compaddr1, compaddr1.Length>45 ? fPDFs2 : fPDF, Brushes.Black, new PointF(xCol2-20, 50), _sfLeft);
                pdf.DrawString(compaddr2,compaddr2.Length >45 ? fPDFs2 : fPDF, Brushes.Black, new PointF(xCol2 - 20, 62), _sfLeft);
                pdf.DrawString("TAX ID: "+ txttaxid.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol2-20, 73), _sfLeft);
                pdf.DrawString(" ใบเสร็จรับเงิน ", fPDFl2, Brushes.Black, new PointF(xCol5 - 45, 60), _sfLeft);
                pdf.DrawString("Receipt ", fPDFl2, Brushes.Black, new PointF(xCol5 - 35, 70), _sfLeft);
                pdf.DrawString(DateTime.Now.ToString("dd-MM-yyyy HH:mm"), fPDFs2, Brushes.Black, new PointF(xCol7-10, 65), _sfLeft);
                if (dtinv.Rows.Count > 0)
                {
                    //new LogWriter("d", this.Name + " genPDFreceipt 02");
                    pttname = dtinv.Rows[0]["pttname"].ToString();
                    invno = dtinv.Rows[0]["inv_no"].ToString();
                    hn = dtinv.Rows[0]["hn"].ToString();
                    diadesc = dtinv.Rows[0]["desc1"].ToString();
                    vsdate = dtinv.Rows[0]["apm_date"].ToString();
                    vstime = dtinv.Rows[0]["apm_time"].ToString();
                    invdate = dtinv.Rows[0]["end_date"].ToString();
                    sypmtoms = dtinv.Rows[0]["symptoms"].ToString();
                    discount = dtinv.Rows[0]["discount"] != null ? float.Parse( dtinv.Rows[0]["discount"].ToString()) : 0;
                    pdf.DrawString(invno, fPDF, Brushes.Black, new PointF(xCol7+40, 45), _sfRight);
                    pdf.DrawString(pttname+" ["+hn+"]", fPDF, Brushes.Black, new PointF(xCol2+15, 85), _sfLeft);
                    pdf.DrawString(sypmtoms, fPDF, Brushes.Black, new PointF(xCol5+30, 85), _sfLeft);
                    pdf.DrawString(vsdate, fPDF, Brushes.Black, new PointF(xCol5+70, 96), _sfLeft);
                    pdf.DrawString((vstime), fPDF, Brushes.Black, new PointF(xCol7+15, 96), _sfLeft);
                    pdf.DrawString(invdate, fPDF, Brushes.Black, new PointF(xCol5-5, 108), _sfLeft);
                    linenumber += (130);
                    //new LogWriter("d", this.Name + " genPDFreceipt 03");
                    int i = 0;
                    int pageCounter = 1; // นับจำนวนหน้า (เริ่มที่ 1)
                    foreach (DataRow row in dtinv.Rows)
                    {
                        String amt = row["MNC_AMT"].ToString();
                        String desc = row["MNC_DEF_DSC"].ToString();
                        //String qty = row["MNC_QTY"].ToString();
                        String code = row["MNC_DEF_CD"].ToString();
                        float famt = 0;
                        float.TryParse(amt, out famt);
                        //pdf.DrawString(qty, fPDF, Brushes.Black, new PointF(xCol1, linenumber += (gapLine)), _sfLeft);
                        pdf.DrawString(code, fPDF, Brushes.Black, new PointF(xCol2-15, linenumber+=(gapLine)), _sfLeft);
                        pdf.DrawString(desc, fPDF, Brushes.Black, new PointF(xCol3-15, linenumber), _sfLeft);
                        pdf.DrawString(famt>0 ? famt.ToString("#,###.00"): "", fPDF, Brushes.Black, new PointF(xCol8, linenumber), _sfRight);
                        i++;
                        if(i%21 == 0)
                        {
                            pdf.NewPage();                              pageCounter++; // เพิ่มจำนวนหน้า
                            // ปรับตำแหน่งตามจำนวนหน้า
                            // วิธีการคำนวณ: baseOffset + (additionalOffset * จำนวนหน้าที่เพิ่ม)
                            int baseOffset = -40;          // <-- ค่าสำหรับหน้า 2 (ปรับจนหน้า 2 ตรง)
                            int additionalOffset = 20;     // <-- ค่าเพิ่มต่อหน้า สำหรับหน้า 3, 4, ... (เช่น -5 หรือ -10)
                            // สูตร: หน้า 2 = baseOffset, หน้า 3 = baseOffset + additionalOffset, หน้า 4 = baseOffset + (additionalOffset*2)
                            int pageOffset = baseOffset + (additionalOffset * (pageCounter - 2));
                            linenumber = 5 + pageOffset;
                            pdf.DrawString(txtCompName.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 - 20, 35 + pageOffset), _sfLeft);
                            pdf.DrawString(compaddr1, compaddr1.Length > 45 ? fPDFs2 : fPDF, Brushes.Black, new PointF(xCol2 - 20, 50 + pageOffset), _sfLeft);
                            pdf.DrawString(compaddr2, compaddr2.Length > 45 ? fPDFs2 : fPDF, Brushes.Black, new PointF(xCol2 - 20, 62 + pageOffset), _sfLeft);
                            pdf.DrawString("TAX ID: " + txttaxid.Text.Trim(), fPDFs2, Brushes.Black, new PointF(xCol2 - 20, 73 + pageOffset), _sfLeft);
                            pdf.DrawString(" ใบเสร็จรับเงิน ", fPDFl2, Brushes.Black, new PointF(xCol5 - 45, 60 + pageOffset), _sfLeft);
                            pdf.DrawString("Receipt ", fPDFl2, Brushes.Black, new PointF(xCol5 - 35, 70 + pageOffset), _sfLeft);
                            pdf.DrawString(DateTime.Now.ToString("dd-MM-yyyy HH:mm"), fPDFs2, Brushes.Black, new PointF(xCol7 - 5, 65 + pageOffset), _sfLeft);

                            pdf.DrawString(invno, fPDF, Brushes.Black, new PointF(xCol7 + 40, 45 + pageOffset), _sfRight);
                            pdf.DrawString(pttname + " [" + hn + "]", fPDF, Brushes.Black, new PointF(xCol2 + 15, 85 + pageOffset), _sfLeft);
                            pdf.DrawString(sypmtoms, fPDF, Brushes.Black, new PointF(xCol5 + 30, 85 + pageOffset), _sfLeft);
                            pdf.DrawString(vsdate, fPDF, Brushes.Black, new PointF(xCol5 + 70, 96 + pageOffset), _sfLeft);
                            pdf.DrawString((vstime), fPDF, Brushes.Black, new PointF(xCol7 + 15, 96 + pageOffset), _sfLeft);
                            pdf.DrawString(invdate, fPDF, Brushes.Black, new PointF(xCol5 - 5, 108 + pageOffset), _sfLeft);
                            linenumber += (130);
                        }
                    }
                }
                if(discount> 0)
                {
                    pdf.DrawString(total.ToString("#,###.00"), fPDF, Brushes.Black, new PointF(xCol8, 473), _sfRight);
                    pdf.DrawString(discount.ToString("#,###.00"), fPDF, Brushes.Black, new PointF(xCol8, 493), _sfRight);
                }
                pdf.DrawString((total- discount).ToString("#,###.00"), fPDF, Brushes.Black, new PointF(xCol8, 503), _sfRight);
                String patheName = Environment.CurrentDirectory + "\\receipt\\";
                if ((Environment.CurrentDirectory.ToLower().IndexOf("windows") >= 0) && ((Environment.CurrentDirectory.ToLower().IndexOf("c:") >= 0)))
                {
                    //new LogWriter("d", "genPDFreceipt Environment.CurrentDirectory " + Environment.CurrentDirectory);
                    patheName = bc.iniC.pathIniFile + "\\receipt\\";
                }
                if (!Directory.Exists(patheName))               {                    Directory.CreateDirectory(patheName);      }
                String pathFileName = patheName + filename;
                if (File.Exists(pathFileName))                  {                    File.Delete(pathFileName);                 }
                pdf.Save(pathFileName);
                if (File.Exists(pathFileName))
                {
                    lfSbMessage.Text = "สร้างใบเสร็จรับเงิน " + pathFileName + " เรียบร้อย";
                    var psi = new ProcessStartInfo { FileName = pathFileName, UseShellExecute = true };
                    Process.Start(psi);
                }
                else
                    lfSbMessage.Text = "สร้างใบเสร็จรับเงิน ไม่สำเร็จ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                new LogWriter("e", this.Name+ " genPDFreceipt " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "genPDFreceipt ", ex.Message);
            }
            finally
            {
                pdf.Dispose();
                
            }
        }
        private async void Btntaxid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //https://dataapi.moc.go.th/juristic?juristic_id=0105537004444
            if(txttaxid == null) { lfSbMessage.Text = "txttaxid is null";  return; }
            if (txttaxid.Text.Length <= 0) { lfSbMessage.Text = "กรุณากรอก เลขประจำตัวผู้เสียภาษี"; return; }
            using (var client = new System.Net.Http.HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var url = "https://dataapi.moc.go.th/juristic?juristic_id="+ txttaxid.Text.Trim();
                try
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string result = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(result, "API Result");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "API Error");
                }
            }
        }
        private float setPrintReceipt(String original, DataTable dtinv)
        {
            setPrintINV();
            //dtinv = new DataTable();
            dtinv = DTINV.Copy();
            if (dtinv == null || dtinv.Rows.Count <= 0) return 0;
            if (!dtinv.Columns.Contains("apmmake")) dtinv.Columns.Add("apmmake", typeof(String));
            if (!dtinv.Columns.Contains("phone")) dtinv.Columns.Add("phone", typeof(String));
            float total = 0;
            foreach (DataRow arow in dtinv.Rows)
            {
                float.TryParse(arow["amount"].ToString(), out float amt);
                total += amt;
                arow["amount"] = amt.ToString("#,##0.00");
                if (float.Parse(arow["amount"].ToString()) <= 0)                {                    arow.Delete();                }
                else                {                    arow["apmmake"] = original;                    arow["phone"] = "0";                }
            }
            dtinv.AcceptChanges();
            return total;
        }
        private void printReceipt(String original, String preview)
        {
            //DataTable dtinv = new DataTable();
            float total = setPrintReceipt(original, DTINV);
            
            FrmReportNew frm = new FrmReportNew(bc, "cashier_receipt", total.ToString("#,##0.00"), original,"");
            frm.DT = DTINV;
            if (preview.Equals("preview"))
            {
                frm.ShowDialog(this);
            }
            else
            {
                frm.PrintReport();
            }
        }
        private void setPrintINV()
        {
            if (DTINV == null) return;
            DataTable dttem01 = bc.bcDB.tem01DB.selectM01(DOCDATE,DOCNO);
            if(dttem01 == null || dttem01.Rows.Count <= 0) return;
            if(!DTINV.Columns.Contains("hn")) DTINV.Columns.Add("hn", typeof(String));
            if (!DTINV.Columns.Contains("desc1")) DTINV.Columns.Add("desc1", typeof(String));
            if (!DTINV.Columns.Contains("pttname")) DTINV.Columns.Add("pttname", typeof(String));
            if (!DTINV.Columns.Contains("inv_no")) DTINV.Columns.Add("inv_no", typeof(String));
            if (!DTINV.Columns.Contains("apm_date")) DTINV.Columns.Add("apm_date", typeof(String));
            if (!DTINV.Columns.Contains("end_date")) DTINV.Columns.Add("end_date", typeof(String));
            if (!DTINV.Columns.Contains("apm_time")) DTINV.Columns.Add("apm_time", typeof(String));
            if (!DTINV.Columns.Contains("amount")) DTINV.Columns.Add("amount", typeof(String));
            if (!DTINV.Columns.Contains("comp_name")) DTINV.Columns.Add("comp_name", typeof(String));
            if (!DTINV.Columns.Contains("comp_addr1")) DTINV.Columns.Add("comp_addr1", typeof(String));
            if (!DTINV.Columns.Contains("comp_addr2")) DTINV.Columns.Add("comp_addr2", typeof(String));
            if (!DTINV.Columns.Contains("symptoms")) DTINV.Columns.Add("symptoms", typeof(String));
            if (!DTINV.Columns.Contains("discount")) DTINV.Columns.Add("discount", typeof(String));
            string disc = DTINV.Columns.Contains("MNC_DIS_TOT") ? (DTINV.Columns["MNC_DIS_TOT"]?.ToString() ?? "") : "";
            new LogWriter("d"," discount "+disc);
            foreach (DataRow drow in DTINV.Rows)
            {
                Boolean chkname = false;
                String invno = dttem01.Rows[0]["MNC_DOC_NO2"].ToString();
                String[] invno1 = invno.Split('#');
                if(invno1.Length > 1)
                {
                    String[] txt = invno1[1].Split('/');
                    String invno2 = invno1[1].Length>2 ? invno1[1].Substring(invno1[1].Length-2) : invno1[1];
                    String invno3= invno1[1].Length > 2 ? invno1[1].Substring(0,invno1[1].Length - 4) : invno1[1];
                    invno = "BO"+ invno3 + invno2;
                    //การเงินให้แก้68-11-13
                    if (txt.Length > 1)
                    {
                        invno = "BO" + txt[1] + txt[0];
                    }
                }
                drow["hn"] = dttem01.Rows[0]["mnc_hn_no"] !=null ? dttem01.Rows[0]["mnc_hn_no"].ToString():"";
                drow["desc1"] = drow["MNC_DEF_CD"].ToString()+" "+drow["MNC_DEF_DSC"].ToString();
                drow["pttname"] = dttem01.Rows[0]["mnc_hn_name"] != null ? dttem01.Rows[0]["mnc_hn_name"].ToString() : "";
                //drow["inv_no"] = DOCNO;
                drow["apm_date"] = bc.datetoShow1(VSDATE);
                drow["end_date"] = bc.datetoShow1(VSDATE);
                drow["apm_time"] = bc.showTime(VSTIME);
                drow["amount"] = drow["MNC_AMT"].ToString();
                drow["discount"] = dttem01.Rows[0]["MNC_DIS_TOT"].ToString();
                drow["inv_no"] = invno;
                drow["comp_name"] = dttem01.Rows[0]["MNC_COM_NAME"] != null ? dttem01.Rows[0]["MNC_COM_NAME"].ToString():"";
                drow["comp_addr1"] = dttem01.Rows[0]["MNC_COM_addr"] != null ? dttem01.Rows[0]["MNC_COM_addr"].ToString():"";
                drow["symptoms"] = dttem01.Rows[0]["MNC_DIA_DSC"] != null ? dttem01.Rows[0]["MNC_DIA_DSC"].ToString() : "";
                //chkname = txtName.Text.Any(c => !Char.IsLetterOrDigit(c));
                //if (chkname)
                //{
                //    drow["patient_age"] = ptt.AgeString().Replace("Year", "ปี").Replace("Month", "เดือน").Replace("Days", "วัน").Replace("s", "");
                //}
                //else
                //{
                //    drow["patient_age"] = ptt.AgeString();
                //}
            }
        }
        private void TabMain_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tabMain.SelectedTab == tabOper)
            {
                setLbLoading("กำลังโหลดข้อมูล ...");
                showLbLoading();
                setGrfOperList(ribbonDatePicker1.Value.Year + "-" + ribbonDatePicker1.Value.ToString("MM-dd"));
                hideLbLoading();
                TABVSACTIVE = tabOper.Name;
            }
            else if (tabMain.SelectedTab == tabFinish)
            {
                TABVSACTIVE = tabFinish.Name;
                setLbLoading("กำลังโหลดข้อมูล ...");
                showLbLoading();
                setGrfFinish(ribbonDatePicker1.Value.Year+ "-" + ribbonDatePicker1.Value.ToString("MM-dd"));
                rgPrn.Visible = true;
                rgPrn1.Visible = true;
                rgPrnReceipt.Visible = true;
                hideLbLoading();

            }
            else if (tabMain.SelectedTab == tabExpense)
            {
                TABVSACTIVE = tabExpense.Name;
                setLbLoading("กำลังโหลดข้อมูล ...");
                showLbLoading();
                setGrfExp();
                rgPrn.Visible = false;
                rgPrn1.Visible = false;
                rgPrnReceipt.Visible = false;
                hideLbLoading();
            }
            else if(tabMain.SelectedTab == tabMaster)
            {
                TABVSACTIVE = tabMaster.Name;
                setLbLoading("กำลังโหลดข้อมูล ...");
                showLbLoading();
                initControlTabMaster();
                if (grfMasterDrug==null) initGrfMasterDrug();
                setGrfMasterDrug();
                if(grfMasterSimb2==null) initGrfMasterSimb2();
                setGrfMasterSimb2();
                hideLbLoading();
            }
        }
        private void initControlTabMaster()
        {
            simb2DB = new Simb2BillingGroupDB(bc.conn);
            listSimb2 = simb2DB.GetList();
        }
        private void clearControlTabMaster()
        {
            txtMasterSimb2id.Value = "";
            txtMasterDrugGenericCode.Value = "";
            txtMasterDrugGenricName.Value = "";
            txtMasterSimb2CodeBG.Value = "";
            txtMasterSimb2BillGTH.Value = "";
            txtMasterSimb2BillGEN.Value = "";
            txtMasterSimb2CodeBSG.Value = "";
            txtMasterSimb2CodeSubGTH.Value = "";
            txtMasterSimb2CodeSubGEN.Value = "";
            txtMasterFnCode.Value = "";
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
        private void initGrfMasterSimb2()
        {
            grfMasterSimb2 = new C1FlexGrid();
            grfMasterSimb2.Font = fEdit;
            grfMasterSimb2.Dock = System.Windows.Forms.DockStyle.Fill;
            grfMasterSimb2.Location = new System.Drawing.Point(0, 0);
            grfMasterSimb2.Rows.Count = 1;
            grfMasterSimb2.Cols.Count = 8;
            grfMasterSimb2.Cols[colgrfSimb2GID].Width = 60;
            grfMasterSimb2.Cols[colgrfSimb2CodeBG].Width = 60;
            grfMasterSimb2.Cols[colgrfSimb2BillGTH].Width = 300;
            grfMasterSimb2.Cols[colgrfSimb2BillGEN].Width = 300;
            grfMasterSimb2.Cols[colgrfSimb2CodeBSG].Width = 80;
            grfMasterSimb2.Cols[colgrfSimb2CodeSubGTH].Width = 300;
            grfMasterSimb2.Cols[colgrfSimb2CodeSubGEN].Width = 300;
            grfMasterSimb2.ShowCursor = true;
            grfMasterSimb2.Cols[colgrfSimb2GID].Caption = "รหัส";
            grfMasterSimb2.Cols[colgrfSimb2CodeBG].Caption = "รายการ";
            grfMasterSimb2.Cols[colgrfSimb2BillGTH].Caption = "BillG TH";
            grfMasterSimb2.Cols[colgrfSimb2BillGEN].Caption = "BillG EN";
            grfMasterSimb2.Cols[colgrfSimb2CodeBSG].Caption = "CodeBSG";
            grfMasterSimb2.Cols[colgrfSimb2CodeSubGTH].Caption = "SubG TH";
            grfMasterSimb2.Cols[colgrfSimb2CodeSubGEN].Caption = "SubG EN";
            grfMasterSimb2.Cols[colgrfSimb2CodeBG].TextAlign = TextAlignEnum.CenterCenter;
            grfMasterSimb2.Cols[colgrfSimb2BillGTH].TextAlign = TextAlignEnum.LeftCenter;
            grfMasterSimb2.Cols[colgrfSimb2BillGEN].TextAlign = TextAlignEnum.LeftCenter;
            grfMasterSimb2.Cols[colgrfSimb2CodeBG].TextAlign = TextAlignEnum.CenterCenter;
            grfMasterSimb2.Cols[colgrfSimb2CodeSubGTH].TextAlign = TextAlignEnum.LeftCenter;
            grfMasterSimb2.Cols[colgrfSimb2CodeSubGEN].TextAlign = TextAlignEnum.LeftCenter;

            grfMasterSimb2.Cols[colgrfSimb2GID].AllowEditing = false;
            grfMasterSimb2.Cols[colgrfSimb2CodeBG].AllowEditing = false;
            grfMasterSimb2.Cols[colgrfSimb2BillGTH].AllowEditing = false;
            grfMasterSimb2.Cols[colgrfSimb2BillGEN].AllowEditing = false;
            grfMasterSimb2.Cols[colgrfSimb2CodeBSG].AllowEditing = false;
            grfMasterSimb2.Cols[colgrfSimb2CodeSubGTH].AllowEditing = false;
            grfMasterSimb2.Cols[colgrfSimb2CodeSubGEN].AllowEditing = false;
            grfMasterSimb2.Cols[0].Width = 50;
            grfMasterSimb2.Cols[colgrfSimb2GID].Visible = false;
            pnMasterSimb2.Controls.Add(grfMasterSimb2);
            grfMasterSimb2.SelectionMode = SelectionModeEnum.Row;
            grfMasterSimb2.Click += GrfMasterSimb2_Click;
        }

        private void GrfMasterSimb2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfMasterSimb2.Row <= 0) return;
            if (FLAGNEWSIMB2)
            {
                txtMasterSimb2CodeBGNew.Value = grfMasterSimb2[grfMasterSimb2.Row, colgrfSimb2CodeBSG]?.ToString() ?? "";
                txtMasterSimb2BillGTHNew.Value = grfMasterSimb2[grfMasterSimb2.Row, colgrfSimb2CodeSubGTH]?.ToString() ?? "";
                txtMasterSimb2BillGENNew.Value = grfMasterSimb2[grfMasterSimb2.Row, colgrfSimb2CodeSubGEN]?.ToString() ?? "";
                txtMasterSimb2CodeBSGNew.Value = "";
                txtMasterSimb2CodeSubGTHNew.Value = "";
                txtMasterSimb2CodeSubGENNew.Value = "";
            }
            else
            {
                txtMasterSimb2id.Value = grfMasterSimb2[grfMasterSimb2.Row, colgrfSimb2GID]?.ToString() ?? "";
                txtMasterSimb2CodeBG.Value = grfMasterSimb2[grfMasterSimb2.Row, colgrfSimb2CodeBG]?.ToString() ?? "";
                txtMasterSimb2BillGTH.Value = grfMasterSimb2[grfMasterSimb2.Row, colgrfSimb2BillGEN]?.ToString() ?? "";
                txtMasterSimb2BillGEN.Value = grfMasterSimb2[grfMasterSimb2.Row, colgrfSimb2BillGTH]?.ToString() ?? "";
                txtMasterSimb2CodeBSG.Value = grfMasterSimb2[grfMasterSimb2.Row, colgrfSimb2CodeBSG]?.ToString() ?? "";
                txtMasterSimb2CodeSubGTH.Value = grfMasterSimb2[grfMasterSimb2.Row, colgrfSimb2CodeSubGTH]?.ToString() ?? "";
                txtMasterSimb2CodeSubGEN.Value = grfMasterSimb2[grfMasterSimb2.Row, colgrfSimb2CodeSubGEN]?.ToString() ?? "";
            }
        }
        private void setGrfMasterSimb2()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.pharM01DB.SelectSimbG2();
            grfMasterSimb2.Rows.Count = 1;
            if (dt.Rows.Count > 0)
            {
                grfMasterSimb2.Rows.Count = dt.Rows.Count + 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grfMasterSimb2[i + 1, colgrfSimb2GID] = dt.Rows[i]["ID"] != null ? dt.Rows[i]["ID"].ToString() : "";
                    grfMasterSimb2[i + 1, colgrfSimb2CodeBG] = dt.Rows[i]["CODEBG"] != null ? dt.Rows[i]["CODEBG"].ToString() : "";
                    grfMasterSimb2[i + 1, colgrfSimb2BillGTH] = dt.Rows[i]["BillingGroupTH"] != null ? dt.Rows[i]["BillingGroupTH"].ToString() : "";
                    grfMasterSimb2[i + 1, colgrfSimb2BillGEN] = dt.Rows[i]["BillingGroupEN"] != null ? dt.Rows[i]["BillingGroupEN"].ToString() : "";
                    grfMasterSimb2[i + 1, colgrfSimb2CodeBSG] = dt.Rows[i]["CODEBSG"] != null ? dt.Rows[i]["CODEBSG"].ToString() : "";
                    grfMasterSimb2[i + 1, colgrfSimb2CodeSubGTH] = dt.Rows[i]["BillingSubGroupTH"] != null ? dt.Rows[i]["BillingSubGroupTH"].ToString() : "";
                    grfMasterSimb2[i + 1, colgrfSimb2CodeSubGEN] = dt.Rows[i]["BillingSubGroupEN"] != null ? dt.Rows[i]["BillingSubGroupEN"].ToString() : "";
                    grfMasterSimb2[i + 1, 0] = (i + 1).ToString();
                }
            }
        }
        private void initGrfMasterDrug()
        {
            grfMasterDrug = new C1FlexGrid();
            grfMasterDrug.Font = fEdit;
            grfMasterDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            grfMasterDrug.Location = new System.Drawing.Point(0, 0);
            grfMasterDrug.Rows.Count = 1;
            grfMasterDrug.Cols.Count = 9;
            grfMasterDrug.Cols[0].Width = 50;
            grfMasterDrug.Cols[colgrfDrugCode].Width = 80;
            grfMasterDrug.Cols[colgrfDrugName].Width = 250;
            grfMasterDrug.Cols[colgrfDrugFnCd].Width = 60;
            grfMasterDrug.Cols[colgrfDrugFnName].Width = 200;
            grfMasterDrug.Cols[colgrfDrugSimbcode].Width = 60;
            grfMasterDrug.Cols[colgrfDrugChargeCD].Width = 40;
            grfMasterDrug.Cols[colgrfDrugCodeBSG].Width = 80;
            grfMasterDrug.Cols[colgrfDrugBillingSubGroupTH].Width = 200;
            grfMasterDrug.ShowCursor = true;
            grfMasterDrug.Cols[colgrfDrugCode].Caption = "รหัส";
            grfMasterDrug.Cols[colgrfDrugName].Caption = "DrugName";
            grfMasterDrug.Cols[colgrfDrugFnCd].Caption = "FnCd";
            grfMasterDrug.Cols[colgrfDrugFnName].Caption = "FnName";
            grfMasterDrug.Cols[colgrfDrugSimbcode].Caption = "simb";
            grfMasterDrug.Cols[colgrfDrugChargeCD].Caption = "ChargeCD";
            grfMasterDrug.Cols[colgrfDrugCodeBSG].Caption = "CodeBSG";
            grfMasterDrug.Cols[colgrfDrugBillingSubGroupTH].Caption = "BillingSubGroupTH";

            grfMasterDrug.Cols[colgrfDrugCode].DataType = typeof(String);
            grfMasterDrug.Cols[colgrfDrugName].DataType = typeof(String);
            grfMasterDrug.Cols[colgrfDrugCode].TextAlign = TextAlignEnum.CenterCenter;
            grfMasterDrug.Cols[colgrfDrugName].TextAlign = TextAlignEnum.LeftCenter;
            grfMasterDrug.Cols[colgrfDrugFnCd].TextAlign = TextAlignEnum.CenterCenter;
            grfMasterDrug.Cols[colgrfDrugFnName].TextAlign = TextAlignEnum.LeftCenter;
            grfMasterDrug.Cols[colgrfDrugSimbcode].TextAlign = TextAlignEnum.CenterCenter;
            grfMasterDrug.Cols[colgrfDrugChargeCD].TextAlign = TextAlignEnum.CenterCenter;
            grfMasterDrug.Cols[colgrfDrugCodeBSG].TextAlign = TextAlignEnum.CenterCenter;
            grfMasterDrug.Cols[colgrfDrugBillingSubGroupTH].TextAlign = TextAlignEnum.LeftCenter;
            grfMasterDrug.Cols[colgrfDrugCode].AllowEditing = false;
            grfMasterDrug.Cols[colgrfDrugName].AllowEditing = false;
            grfMasterDrug.Cols[colgrfDrugFnCd].AllowEditing = false;
            grfMasterDrug.Cols[colgrfDrugFnName].AllowEditing = false;
            grfMasterDrug.Cols[colgrfDrugChargeCD].AllowEditing = false;
            grfMasterDrug.Cols[colgrfDrugCodeBSG].AllowEditing = false;
            grfMasterDrug.Cols[colgrfDrugSimbcode].AllowEditing = false;
            grfMasterDrug.Cols[colgrfDrugBillingSubGroupTH].AllowEditing = false;
            grfMasterDrug.SelectionMode = SelectionModeEnum.Row;
            pnMasterDrugView.Controls.Add(grfMasterDrug);
            grfMasterDrug.SelectionMode = SelectionModeEnum.ListBox;
            grfMasterDrug.Click += GrfMasterDrug_Click;
        }
        private void GrfMasterDrug_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfMasterDrug.Row <= 0) return;
            String code = grfMasterDrug[grfMasterDrug.Row, colgrfDrugCode]?.ToString() ?? "";
            String name = grfMasterDrug[grfMasterDrug.Row, colgrfDrugName]?.ToString() ?? "";
            String fncode = grfMasterDrug[grfMasterDrug.Row, colgrfDrugFnCd]?.ToString() ?? "";
            var selectedRows = grfMasterDrug.Rows.Cast<Row>().Where(r => r.Index > 0 && r.Selected).ToList();
            if (selectedRows.Count > 0)
            {
                String code1 = "";
                foreach (var row in selectedRows)
                {
                    string drugCode = grfMasterDrug[row.Index, colgrfDrugCode]?.ToString() ?? "";
                    string drugName = grfMasterDrug[row.Index, colgrfDrugName]?.ToString() ?? "";
                    code1 += drugCode + ",";
                }
                txtMasterDrugGenericCode.Value = code1;
                txtMasterDrugGenricName.Value = name;
                txtMasterFnCode.Value = fncode;
                txtMasterSimb2BillGEN.Value = grfMasterDrug[grfMasterDrug.Row, colgrfDrugFnName]?.ToString() ?? "";
            }
        }
        private void setGrfMasterDrug()
        {
            DataTable dt = new DataTable();
            if(chkMasterDrug.Checked )          {   dt = bc.bcDB.pharM01DB.SelectDrugMap("drug");           }
            else if(chkMastersupply.Checked)    {   dt = bc.bcDB.pharM01DB.SelectDrugMap("supply");         }
            else if (chkMasterLab.Checked )     {   dt = bc.bcDB.labM01DB.SelectLabMap();                   }
            else if (chkMasterXray.Checked)     {   dt = bc.bcDB.xrayM01DB.SelectXrayMap();                 }
            else if (chkMasterProc.Checked)     {   dt = bc.bcDB.pm30DB.SelectProcedureMap();               }
            else if (chkMasterDf.Checked)       {   dt = bc.bcDB.finM01DB.SelectDfMap();                    }
            else if (chkMasterFncode.Checked)   {   dt = bc.bcDB.finM01DB.SelectCodeDSGMap(); }
            grfMasterDrug.Rows.Count = 1;
            if (dt.Rows.Count > 0)
            {
                grfMasterDrug.Rows.Count = dt.Rows.Count + 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grfMasterDrug[i + 1, colgrfDrugCode] = chkMasterDrug.Checked ? dt.Rows[i]["MNC_PH_CD"]?.ToString()?? "" : chkMastersupply.Checked ? dt.Rows[i]["MNC_PH_CD"]?.ToString()?? "" : chkMasterLab.Checked ? dt.Rows[i]["MNC_LB_CD"]?.ToString()?? "" : chkMasterXray.Checked ? dt.Rows[i]["MNC_XR_CD"]?.ToString()?? "" : chkMasterProc.Checked ? dt.Rows[i]["MNC_SR_CD"]?.ToString()?? "" : "";
                    grfMasterDrug[i + 1, colgrfDrugName] = chkMasterDrug.Checked ? dt.Rows[i]["MNC_PH_GN"]?.ToString()?? "" : chkMastersupply.Checked ? dt.Rows[i]["MNC_PH_TN"]?.ToString()?? "" : chkMasterLab.Checked ? dt.Rows[i]["MNC_LB_DSC"]?.ToString()?? "" : chkMasterXray.Checked ? dt.Rows[i]["MNC_XR_DSC"]?.ToString()?? "" : chkMasterProc.Checked ? dt.Rows[i]["MNC_SR_DSC"]?.ToString()?? "" : "";
                    grfMasterDrug[i + 1, colgrfDrugFnCd] = dt.Rows[i]["MNC_FN_CD"] != null ? dt.Rows[i]["MNC_FN_CD"].ToString() : "";
                    grfMasterDrug[i + 1, colgrfDrugFnName] = dt.Rows[i]["MNC_DEF_DSC"] != null ? dt.Rows[i]["MNC_DEF_DSC"].ToString() : "";
                    grfMasterDrug[i + 1, colgrfDrugCodeBSG] = dt.Rows[i]["CODEBSG"] != null ? dt.Rows[i]["CODEBSG"].ToString() : "";
                    grfMasterDrug[i + 1, colgrfDrugBillingSubGroupTH] = dt.Rows[i]["BillingSubGroupTH"] != null ? dt.Rows[i]["BillingSubGroupTH"].ToString() : "";
                    //grfMasterDrug[i + 1, colgrfSimb2CodeSubGEN] = dt.Rows[i]["BillingSubGroupEN"] != null ? dt.Rows[i]["BillingSubGroupEN"].ToString() : "";
                    grfMasterDrug[i + 1, colgrfDrugChargeCD] = dt.Rows[i]["MNC_CHARGE_CD"] != null ? dt.Rows[i]["MNC_CHARGE_CD"].ToString() : "";
                    grfMasterDrug[i + 1, colgrfDrugSimbcode] = dt.Rows[i]["MNC_SIMB_CD"] != null ? dt.Rows[i]["MNC_SIMB_CD"].ToString() : "";
                    grfMasterDrug[i+1,0] = (i + 1).ToString(); 
                }
            }
            if (txtMasterSearch.Text.Trim().Length > 0)
            {
                grfMasterDrug.ApplySearch(txtMasterSearch.Text.Trim(), true, true, false);
            }
        }
        private void initGrfExp()
        {
            grfExp = new C1FlexGrid();
            grfExp.Font = fEdit;
            grfExp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfExp.Location = new System.Drawing.Point(0, 0);
            grfExp.Rows.Count = 1;
            grfExp.Cols.Count = 7;
            grfExp.Cols[colgrfExpid].Width = 100;
            grfExp.Cols[colgrfExpDesc].Width = 450;
            grfExp.Cols[colgrfExpAmount].Width = 100;
            grfExp.Cols[colgrfExpModule].Width = 70;
            grfExp.Cols[colgrfExpUser].Width = 200;
            grfExp.Cols[colgrfExpDateReq].Width = 150;
            grfExp.ShowCursor = true;
            grfExp.Cols[colgrfExpid].Caption = "รหัส";
            grfExp.Cols[colgrfExpDesc].Caption = "รายการ";
            grfExp.Cols[colgrfExpAmount].Caption = "ยอดเงิน";
            grfExp.Cols[colgrfExpModule].Caption = "Module";
            grfExp.Cols[colgrfExpUser].Caption = "เจ้าหน้าที่";
            grfExp.Cols[colgrfExpDateReq].Caption = "วันที่ขอเบิก";
            grfExp.Cols[colgrfExpid].DataType = typeof(String);
            grfExp.Cols[colgrfExpDesc].DataType = typeof(String);
            grfExp.Cols[colgrfExpAmount].DataType = typeof(String);
            grfExp.Cols[colgrfExpModule].DataType = typeof(String);
            grfExp.Cols[colgrfExpUser].DataType = typeof(String);
            grfExp.Cols[colgrfExpDateReq].DataType = typeof(String);
            grfExp.Cols[colgrfExpid].TextAlign = TextAlignEnum.CenterCenter;
            grfExp.Cols[colgrfExpDesc].TextAlign = TextAlignEnum.LeftCenter;
            grfExp.Cols[colgrfExpAmount].TextAlign = TextAlignEnum.RightCenter;
            grfExp.Cols[colgrfExpModule].TextAlign = TextAlignEnum.CenterCenter;
            grfExp.Cols[colgrfExpUser].TextAlign = TextAlignEnum.LeftCenter;
            grfExp.Cols[colgrfExpDateReq].TextAlign = TextAlignEnum.LeftCenter;

            grfExp.Cols[colgrfExpid].AllowEditing = false;
            grfExp.Cols[colgrfExpDesc].AllowEditing = false;
            grfExp.Cols[colgrfExpAmount].AllowEditing = false;
            grfExp.Cols[colgrfExpModule].AllowEditing = false;
            grfExp.Cols[colgrfExpUser].AllowEditing = false;
            grfExp.Cols[colgrfExpDateReq].AllowEditing = false;
            grfExp.Click += GrfExp_Click;
            pnExpView.Controls.Add(grfExp);
        }
        private void GrfExp_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfExp.Row <= 0) return;
            String id = grfExp[grfExp.Row, colgrfExpid] != null ? grfExp[grfExp.Row, colgrfExpid].ToString() : "";
            if (id.Equals("")) return;
            if(supraDB == null) supraDB = new TSupraDB(bc.conn);
            picBoxSupra.Image = null;
            TExpense expense = new TExpense();
            expense = expenseDB.SelectByPk(long.Parse("999"+id));
            txtExpenseID.Value = expense.expense_id.ToString().Replace("999","");
            lbExpenseDesc.Text = expense.expense_desc;
            txtExpense.Value = expense.expense.ToString();
            txtAmount.Value = expense.expense.ToString();
            DataTable dtsupra = supraDB.selectImageByPk(expense.row_id.ToString());
            if(dtsupra.Rows.Count > 0)
            {
                String imageid = dtsupra.Rows[0]["ftp_file_path_archive"] != null ? dtsupra.Rows[0]["ftp_file_path_archive"].ToString() : "";
                showImage(imageid);
            }
        }
        private void setGrfExp()
        {
            DataTable dt = new DataTable();
            if(expenseDB == null)            expenseDB = new TExpenseDB(bc.conn);
            dt = expenseDB.SelectByDraw();
            grfExp.Rows.Count = 1;
            if (dt.Rows.Count > 0)
            {
                grfExp.Rows.Count = dt.Rows.Count + 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grfExp[i + 1, colgrfExpid] = dt.Rows[i]["expense_id"] != null ? dt.Rows[i]["expense_id"].ToString().Replace("999","") : "";
                    grfExp[i + 1, colgrfExpDesc] = dt.Rows[i]["expense_desc"] != null ? dt.Rows[i]["expense_desc"].ToString() : "";
                    float amt = 0;
                    float.TryParse(dt.Rows[i]["expense"] != null ? dt.Rows[i]["expense"].ToString() : "0", out amt);
                    grfExp[i + 1, colgrfExpAmount] = amt.ToString("#,##0.00");
                    grfExp[i + 1, colgrfExpModule] = dt.Rows[i]["module_id"] != null ? dt.Rows[i]["module_id"].ToString() : "";
                    grfExp[i + 1, colgrfExpUser] = dt.Rows[i]["user_req_id"] != null ? dt.Rows[i]["user_req_id"].ToString() : "";
                    grfExp[i + 1, colgrfExpDateReq] = dt.Rows[i]["date_req"] != null ? dt.Rows[i]["date_req"].ToString() : "";
                }
            }
        }
        private void showImage(String imgpath)
        {
            try
            {
                picBoxSupra.Image = null;
                if (imgpath.Equals("")) return;
                String path = "";
                if (imgpath.IndexOf('/') >= 0)                {                    path = imgpath.Substring(1);                }
                else                {                    path = imgpath;                }
                FtpClient ftpc = new FtpClient("ftp://172.25.10.3", "u_supra_temp", "u_supra_temp", true);
                using (MemoryStream streamCertiDtr = ftpc.download(imgpath))
                {
                    if (streamCertiDtr == null || streamCertiDtr.Length == 0) return;

                    streamCertiDtr.Position = 0;
                    using (var img = System.Drawing.Image.FromStream(streamCertiDtr))
                    {
                        // create a copy so we can dispose the stream and original image safely
                        var bmp = new Bitmap(img);
                        // update PictureBox (dispose previous image to avoid leaks)
                        var old = picBoxSupra.Image;
                        picBoxSupra.Dock = DockStyle.Fill;
                        picBoxSupra.SizeMode = PictureBoxSizeMode.StretchImage;
                        picBoxSupra.Image = bmp;
                        old?.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("showImage " + ex.Message, "");
            }
        }
        private void initGrfOperList()
        {
            grfOperList = new C1FlexGrid();
            grfOperList.Font = fEdit;
            grfOperList.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOperList.Location = new System.Drawing.Point(0, 0);
            grfOperList.Rows.Count = 1;
            grfOperList.Cols.Count = 14;
            grfOperList.Cols[colgrfOperListHn].Width = 100;
            grfOperList.Cols[colgrfOperListVn].Width = 70;
            grfOperList.Cols[colgrfOperListFullNameT].Width = 200;
            grfOperList.Cols[colgrfOperListSymptoms].Width = 150;
            grfOperList.Cols[colgrfOperListPaidName].Width = 100;
            grfOperList.Cols[colgrfOperListPreno].Width = 100;
            grfOperList.Cols[colgrfOperListVsDate].Width = 100;
            grfOperList.Cols[colgrfOperListVsTime].Width = 70;
            grfOperList.Cols[colgrfOperListActNo].Width = 100;
            grfOperList.Cols[colgrfOperListDtrName].Width = 100;
            grfOperList.Cols[colgrfOperListPha].Width = 40;
            grfOperList.Cols[colgrfOperListLab].Width = 40;
            grfOperList.Cols[colgrfOperListXray].Width = 40;
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
            grfOperList.Cols[colgrfOperListPha].Caption = "";
            grfOperList.Cols[colgrfOperListLab].Caption = "lab";
            grfOperList.Cols[colgrfOperListXray].Caption = "xray";
            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grfOperList.Cols[colgrfOperListHn].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListVsDate].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListVsTime].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListActNo].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListDtrName].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListPha].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListLab].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListXray].DataType = typeof(String);

            grfOperList.Cols[colgrfOperListHn].TextAlign = TextAlignEnum.CenterCenter;
            grfOperList.Cols[colgrfOperListVsTime].TextAlign = TextAlignEnum.CenterCenter;
            grfOperList.Cols[colgrfOperListActNo].TextAlign = TextAlignEnum.LeftCenter;
            grfOperList.Cols[colgrfOperListDtrName].TextAlign = TextAlignEnum.LeftCenter;
            grfOperList.Cols[colgrfOperListPha].TextAlign = TextAlignEnum.CenterCenter;
            grfOperList.Cols[colgrfOperListLab].TextAlign = TextAlignEnum.CenterCenter;
            grfOperList.Cols[colgrfOperListXray].TextAlign = TextAlignEnum.CenterCenter;

            grfOperList.Cols[colgrfOperListPreno].Visible = false;
            grfOperList.Cols[colgrfOperListVsDate].Visible = false;
            grfOperList.Cols[colgrfOperListHn].Visible = true;

            grfOperList.Cols[colgrfOperListHn].AllowEditing = false;
            grfOperList.Cols[colgrfOperListVn].AllowEditing = false;
            grfOperList.Cols[colgrfOperListFullNameT].AllowEditing = false;
            grfOperList.Cols[colgrfOperListSymptoms].AllowEditing = false;
            grfOperList.Cols[colgrfOperListPaidName].AllowEditing = false;
            grfOperList.Cols[colgrfOperListPreno].AllowEditing = false;
            grfOperList.Cols[colgrfOperListVsDate].AllowEditing = false;
            grfOperList.Cols[colgrfOperListVsTime].AllowEditing = false;
            grfOperList.Cols[colgrfOperListActNo].AllowEditing = false;
            grfOperList.Cols[colgrfOperListDtrName].AllowEditing = false;
            grfOperList.Cols[colgrfOperListPha].AllowEditing = false;
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
            if (isLoad) return;
            if (grfOperList.Row <= 0) return;
            if (grfOperList.Col <= 0) return;
            if (grfOperList[grfOperList.Row, colgrfOperListPreno] == null) return;
            //if (grfOperList.Row == ROWGrfOper) return;
            PRENO = grfOperList[grfOperList.Row, colgrfOperListPreno].ToString();
            VSDATE = grfOperList[grfOperList.Row, colgrfOperListVsDate].ToString();
            HN = grfOperList[grfOperList.Row, colgrfOperListHn].ToString();
            //setControlOper(grfOperList.Name);


            //if (grfFinish.Row <= 0) return;
            //HN = grfFinish[grfFinish.Row, colgrfFinishHn].ToString();
            //PRENO = grfFinish[grfFinish.Row, colgrfFinishPreno].ToString();
            PTTNAME = grfOperList[grfOperList.Row, colgrfOperListFullNameT].ToString();
            VSDATE = grfOperList[grfOperList.Row, colgrfOperListVsDate].ToString();
            VSTIME = grfOperList[grfOperList.Row, colgrfOperListVsTime].ToString();
            DOCDATE = grfOperList[grfOperList.Row, colgrfOperListVsDate].ToString();
            //DOCNO = grfOperList[grfOperList.Row, colgrfOperListDocno].ToString();
            //FNCODE = grfFinish[grfFinish.Row, colgrfDrugFnCd].ToString();

            setGrfInv(DOCNO, DOCDATE);
        }
        private void setGrfOperList(String date)
        {
            if (isLoad) return;
            if (grfOperList == null || grfOperList.IsDisposed || !grfOperList.IsHandleCreated) return;
            C1.Win.C1FlexGrid.RowCollection rows;
            try         {                rows = grfOperList.Rows;               }
            catch       {                return; /* rows not ready yet */       }

            if (rows == null) return;
            isLoad = true;
            try
            {
                timeOperList.Stop();
                showLbLoading();
                DataTable dtvs = new DataTable();
                String deptno = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
                String vsdate = date;
                DateTime dateTime = DateTime.Now.AddDays(-1);
                //vsdate = dateTime.Year.ToString() + "-" + dateTime.ToString("MM-dd");
                dtvs = bc.bcDB.vsDB.selectPttActNo131_600PharmacyOK("", "", vsdate, vsdate);

                grfOperList.Rows.Count = 1;
                grfOperList.Rows.Count = dtvs.Rows.Count + 1;
                int i = 1, j = 1, row = grfOperList.Rows.Count;
                foreach (DataRow row1 in dtvs.Rows)
                {
                    Row rowa = grfOperList.Rows[i];
                    rowa[colgrfOperListHn] = row1["MNC_HN_NO"].ToString();
                    rowa[colgrfOperListVn] = row1["MNC_VN_NO"].ToString() + "." + row1["MNC_VN_SEQ"].ToString() + "." + row1["MNC_VN_SUM"].ToString();
                    rowa[colgrfOperListFullNameT] = row1["ptt_fullnamet"].ToString();
                    rowa[colgrfOperListSymptoms] = row1["MNC_SHIF_MEMO"].ToString();
                    rowa[colgrfOperListPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
                    rowa[colgrfOperListPreno] = row1["MNC_PRE_NO"].ToString();
                    rowa[colgrfOperListLab] = row1["MNC_LAB_FLG"].ToString();
                    rowa[colgrfOperListPha] = row1["MNC_PHA_FLG"].ToString();
                    rowa[colgrfOperListXray] = row1["MNC_XRA_FLG"].ToString();

                    rowa[colgrfOperListVsDate] = row1["MNC_DATE"].ToString();
                    rowa[colgrfOperListVsTime] = bc.showTime(row1["MNC_TIME"].ToString());
                    rowa[colgrfOperListActNo] = bc.adjustACTNO(row1["MNC_ACT_NO"].ToString());
                    rowa[colgrfOperListDtrName] = row1["dtr_name"].ToString();
                    if (row1["MNC_ACT_NO"].ToString().Equals("110")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor); }
                    else if (row1["MNC_ACT_NO"].ToString().Equals("114")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6"); }
                    else if (row1["MNC_AN_NO"].ToString().Length > 0) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6"); rowa[colgrfOperListVn] = row1["MNC_AN_NO"].ToString(); }

                    rowa[0] = i.ToString();
                    i++;
                }
            }
            finally
            {
                hideLbLoading();
                if (timeOperList != null && timeOperList.Enabled) timeOperList.Start();
                isLoad = false;
            }
        }
        private void initGrfInv(ref C1FlexGrid grf, ref Panel pn)
        {
            grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grf.Rows.Count = 1;
            grf.Cols.Count = 10;
            grf.Cols[colgrfFinishHn].DataType = typeof(string);
            grf.Cols[colgrfFinishHn].Caption = "MNC_DEF_CD";
            grf.Cols[colgrfFinishFullNameT].Caption = "MNC_DEF_DSC";
            grf.Cols[colgrfFinishSymptoms].Caption = "MNC_ORD_BY";
            grf.Cols[colgrfFinishPaidName].Caption = "MNC_AMT";
            grf.Cols[colgrfFinishPreno].Caption = "MNC_DEF_LEV";
            grf.Cols[colgrfFinishVsDate].Caption = "MNC_TOT_AMT";
            grf.Cols[colgrfFinishVsTime].Caption = "MNC_DIS_AMT";
            grf.Cols[colgrfFinishActNo].Caption = "";
            grf.Cols[colgrfFinishDocno].Caption = "MNC_DOC_NO";
            grf.Cols[colgrfFinishHn].Width = 90;
            grf.Cols[colgrfFinishFullNameT].Width = 300;
            grf.Cols[colgrfFinishSymptoms].Width = 100;
            grf.Cols[colgrfFinishPaidName].Width = 100;
            grf.Cols[colgrfFinishPreno].Width = 50;
            grf.Cols[colgrfFinishVsDate].Width = 100;
            grf.Cols[colgrfFinishVsTime].Width = 70;
            grf.Cols[colgrfFinishActNo].Width = 100;
            grf.Cols[colgrfFinishDocno].Width = 200;
            grf.Cols[colgrfFinishHn].AllowEditing = false;
            grf.Cols[colgrfFinishFullNameT].AllowEditing = false;
            grf.Cols[colgrfFinishSymptoms].AllowEditing = false;
            grf.Cols[colgrfFinishPaidName].AllowEditing = false;
            grf.Cols[colgrfFinishPreno].AllowEditing = false;
            grf.Cols[colgrfFinishVsDate].AllowEditing = false;
            grf.Cols[colgrfFinishVsTime].AllowEditing = false;
            grf.Cols[colgrfFinishActNo].AllowEditing = false;
            grf.Cols[colgrfFinishDocno].AllowEditing = false;
            grf.Cols[colgrfFinishPreno].Visible = false;
            if (pn.Name.Equals("pnGrfFinishInvIPD"))
            {
                grf.Name = "grfInvIPD";
            }else if (pn.Name.Equals("pnGrfFinishView"))
            {
                grf.Name = "grfInv";
            }
            pn.Controls.Add(grf);
            theme1.SetTheme(grf, bc.iniC.themegrfIpd);
        }
        private void setGrfInv(String docno, String docdate)
        {
            if (isLoad) return;
            setLbLoading("กำลังโหลดข้อมูล ...");
            showLbLoading();
            PTT = bc.bcDB.pttDB.selectPatinetByHn(HN);
            VS = bc.bcDB.vsDB.selectbyPreno(HN, VSDATE, PRENO);
            setLbLoading("กำลังโหลดข้อมูล ...tem02");
            DTINV = bc.bcDB.tem02DB.SelectByDocno1(docno, docdate);
            setLbLoading("กำลังโหลดข้อมูล ...finance");
            DTSIMB2 = bc.bcDB.fint01DB.SelectItemsByDocNo(docno, docdate);
            if ((DTSIMB2.Rows.Count > 0) && VS.VN.Length<=0)
            {
                VS.VN = DTSIMB2.Rows[0]["MNC_VN_NO"].ToString()+"."+ DTSIMB2.Rows[0]["MNC_VN_SEQ"].ToString();
            }
            //DTINV = bc.bcDB.tem01DB.se(docno, docdate);
            if(TABFINISHACTIVE.Equals(tabFinishOPD.Name))
            {
                grfInv.Rows.Count = 1; grfInv.Rows.Count = DTINV.Rows.Count + 1;
            }else if(TABFINISHACTIVE.Equals(tabFinishIPD.Name))
            {
                grfInvIPD.Rows.Count = 1; grfInvIPD.Rows.Count = DTINV.Rows.Count + 1;
            }

            int i = 1, j = 1;
            setLbLoading("กำลังโหลดข้อมูล ...setgrid");
            foreach (DataRow row1 in DTINV.Rows)
            {
                Row rowa = null;
                if (TABFINISHACTIVE.Equals(tabFinishOPD.Name))                {                    rowa = grfInv.Rows[i];                }
                else if(TABFINISHACTIVE.Equals(tabFinishIPD.Name))                {                    rowa = grfInvIPD.Rows[i];                }
                    
                rowa[colgrfFinishHn] = row1["MNC_DEF_CD"].ToString();
                rowa[colgrfFinishFullNameT] = row1["MNC_DEF_DSC"].ToString();
                rowa[colgrfFinishSymptoms] = row1["MNC_ORD_BY"].ToString();
                rowa[colgrfFinishPaidName] = row1["MNC_AMT"].ToString();
                rowa[colgrfFinishPreno] = row1["MNC_DEF_LEV"].ToString();
                rowa[colgrfFinishVsDate] = row1["MNC_TOT_AMT"].ToString();
                rowa[colgrfFinishVsTime] = row1["MNC_DIS_AMT"].ToString();

                rowa[colgrfFinishDocno] = row1["MNC_DOC_NO"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
            lfSbMessage.Text = "พบ " + DTINV.Rows.Count + "รายการ";
            hideLbLoading();
        }
        private void initGrfFinish(ref C1FlexGrid grf,ref Panel pn)
        {            
            grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grf.Rows.Count = 1;
            grf.Cols.Count = 13;
            grf.Cols[colgrfFinishHn].Caption = "HN";
            grf.Cols[colgrfFinishFullNameT].Caption = "Patient Name";
            grf.Cols[colgrfFinishSymptoms].Caption = "อาการ";
            grf.Cols[colgrfFinishPaidName].Caption = "สิทธิ";
            grf.Cols[colgrfFinishPreno].Caption = "preno";
            grf.Cols[colgrfFinishVsDate].Caption = "วันที่";
            grf.Cols[colgrfFinishVsTime].Caption = "เวลา";
            grf.Cols[colgrfFinishActNo].Caption = "Act No";
            grf.Cols[colgrfFinishDocno].Caption = "Dtr Name";
            grf.Cols[colgrfFinishHn].Width = 90;
            grf.Cols[colgrfFinishFullNameT].Width = 200;
            grf.Cols[colgrfFinishSymptoms].Width = 300;
            grf.Cols[colgrfFinishPaidName].Width = 120;
            grf.Cols[colgrfFinishPreno].Width = 50;
            grf.Cols[colgrfFinishVsDate].Width = 100;
            grf.Cols[colgrfFinishVsTime].Width = 70;
            grf.Cols[colgrfFinishActNo].Width = 100;
            grf.Cols[colgrfFinishDocno].Width = 200;
            grf.Cols[colgrfFinishHn].AllowEditing = false;
            grf.Cols[colgrfFinishFullNameT].AllowEditing = false;
            grf.Cols[colgrfFinishSymptoms].AllowEditing = false;
            grf.Cols[colgrfFinishPaidName].AllowEditing = false;
            grf.Cols[colgrfFinishPreno].AllowEditing = false;
            grf.Cols[colgrfFinishVsDate].AllowEditing = false;
            grf.Cols[colgrfFinishVsTime].AllowEditing = false;
            grf.Cols[colgrfFinishActNo].AllowEditing = false;
            grf.Cols[colgrfFinishDocno].AllowEditing = false;
            grf.Cols[colgrfFinishDocdate].AllowEditing = false;
            grf.Cols[colgrfFinishPreno].Visible = false;
            grf.Cols[colgrfFinishPaidCode].Visible = false;
            grf.Dock = DockStyle.Fill;
            grf.Click += GrfFinish_Click;
            
            pn.Controls.Add(grf);
            if (pn.Name.Equals("pnGrfFinishViewIPD"))
            {
                grf.Name = "grfFinishOPD";
            }
            theme1.SetTheme(grf, bc.iniC.themegrfIpd);
            
        }
        private void GrfFinish_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(((C1FlexGrid)sender).Row <= 0) return;
            if (!(sender is C1FlexGrid grid) || grid.Row <= 0) return;
            //if (((C1FlexGrid)sender).Name.Equals(grfFinishOPD.Name))
            //{
                HN = grid[grid.Row, colgrfFinishHn].ToString();
                PRENO = grid[grid.Row, colgrfFinishPreno].ToString();
                PTTNAME = grid[grid.Row, colgrfFinishFullNameT].ToString();
                VSDATE = grid[grid.Row, colgrfFinishVsDate].ToString();
                VSTIME = grid[grid.Row, colgrfFinishVsTime].ToString();
                DOCDATE = grid[grid.Row, colgrfFinishDocdate].ToString();
                DOCNO = grid[grid.Row, colgrfFinishDocno].ToString();
                PAIDCODE = grid[grid.Row, colgrfFinishPaidName].ToString();
                ANNO = grid[grid.Row, colgrfFinishAN].ToString();
            //grid.Dispose();
                setGrfInv(DOCNO, DOCDATE);
            //}
            //else if (((C1FlexGrid)sender).Name.Equals(grfFinishIPD.Name))
            //{
            //    HN = grfFinishIPD[grfFinishIPD.Row, colgrfFinishHn].ToString();
            //    PRENO = grfFinishIPD[grfFinishIPD.Row, colgrfFinishPreno].ToString();
            //    PTTNAME = grfFinishIPD[grfFinishIPD.Row, colgrfFinishFullNameT].ToString();
            //    VSDATE = grfFinishIPD[grfFinishIPD.Row, colgrfFinishVsDate].ToString();
            //    VSTIME = grfFinishIPD[grfFinishIPD.Row, colgrfFinishVsTime].ToString();
            //    DOCDATE = grfFinishIPD[grfFinishIPD.Row, colgrfFinishDocdate].ToString();
            //    DOCNO = grfFinishIPD[grfFinishIPD.Row, colgrfFinishDocno].ToString();
            //    PAIDCODE = grfFinishIPD[grfFinishIPD.Row, colgrfFinishPaidName].ToString();
            //    ANNO = grfFinishIPD[grfFinishIPD.Row, colgrfFinishAN].ToString();

            //    setGrfInv(DOCNO, DOCDATE);
            //}
            
        }
        private void setGrfFinish(String date)
        {
            if (isLoad) return;
            DataTable dtvs = new DataTable();            
            dtvs = bc.bcDB.fint01DB.SelectPaidOPDByDate(date);
            grfFinishOPD.Rows.Count = 1; grfFinishOPD.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            int opdCount = 0, ipdCount = 0;
            int rowCount = dtvs.Rows.Count;
            for (int idx = 0; idx < rowCount; idx++)
            {
                DataRow row = dtvs.Rows[idx];
                string anNo = row["MNC_AN_NO"]?.ToString() ?? "";
                if (!anNo.Equals("0"))                    ipdCount++;
                else                    opdCount++;
            }
            grfFinishOPD.Rows.Count = opdCount + 1;
            grfFinishIPD.Rows.Count = ipdCount + 1;
            int opdIndex = 1, ipdIndex = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = null;
                if (!row1["MNC_AN_NO"].ToString().Equals("0"))                
                {                    
                    rowa = grfFinishIPD.Rows[ipdIndex];
                    rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#CCE5FF");
                    ipdIndex++;
                }
                else                {                    rowa = grfFinishOPD.Rows[opdIndex]; opdIndex++; }
                rowa[colgrfFinishHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfFinishFullNameT] = row1["pttfullname"].ToString();
                rowa[colgrfFinishSymptoms] = row1["MNC_DIA_DSC"].ToString();
                rowa[colgrfFinishPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[colgrfFinishPreno] = row1["MNC_PRE_NO"].ToString();
                rowa[colgrfFinishVsDate] = row1["MNC_DATE"].ToString();
                rowa[colgrfFinishVsTime] = row1["MNC_TIME"].ToString();
                rowa[colgrfFinishDocno] = row1["MNC_DOC_NO"].ToString();
                rowa[colgrfFinishDocdate] = row1["MNC_DOC_DAT"].ToString();
                rowa[colgrfFinishAN] = row1["MNC_AN_NO"].ToString();
                rowa[colgrfFinishPaidCode] = row1["MNC_FN_TYP_CD"].ToString();
                rowa[0] = i.ToString();
                i++;
                if(row1["MNC_AN_NO"].ToString().Length > 1)
                {
                    rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#CCE5FF");      //- เขียวอ่อน     •	#CCE5FF - ฟ้าอ่อน
                }
            }
            lfSbMessage.Text = "พบ " + dtvs.Rows.Count + "รายการ";
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.C | Keys.Control:
                    {
                        // Add logic here if needed, or explicitly break/return
                        Control ctl1 = new Control();
                        ctl1 = GetFocusedControl();
                        if (ctl1 is C1FlexGrid)
                        {
                            if (ctl1.Name.Equals("grfOperList"))
                            {
                                Clipboard.SetText(grfOperList[grfOperList.Row, grfOperList.Col].ToString());
                            }
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
        private void printReceiptDetailSIMB2()
        {
            if ((DTSIMB2 != null) && (DTSIMB2.Rows.Count == 0)) { lfSbMessage.Text = "ไม่พบข้อมูล"; return; }
            // Reset counter
            //new LogWriter("e", "printReceiptDetailSIMB2 ");
            currentPrintRow = 0; totalNet = 0;
            PRINTORIGINAL = "ใบแจ้งรายละเอียดค่ารักษาพยาบาล/Receipt Detail (Original ต้นฉบับ)";
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerA4;
            document.PrintPage += Document_PrintPageReceiptDetail;
            document.EndPrint += Document_ReceiptDetailEndPrint;
            document.BeginPrint += Document_ReceiptDetailBeginPrint;
            document.DefaultPageSettings.Landscape = false;
            //document.Print();
            // ── Preview ──────────────────────────────────────────────
            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = document;
                preview.Width = 900;
                preview.Height = 700;
                preview.Text = PRINTORIGINAL;
                preview.ShowDialog();   // แสดง Preview → กด Print ใน Dialog ได้เลย
            }
            PRINTORIGINAL = "ใบแจ้งรายละเอียดค่ารักษาพยาบาล/Receipt Detail (copy สำเนา)";
            //document.PrintPage += Document_PrintPageReceipt1;
            //document.Print();
            // Clear ข้อมูลหลังพิมพ์เสร็จ
            DTPRINT = null; DTSIMB2 = null;
        }
        private void Document_ReceiptDetailBeginPrint(object sender, PrintEventArgs e)
        {
            //throw new NotImplementedException();
            totalNet = 0;
        }
        private void Document_ReceiptDetailEndPrint(object sender, PrintEventArgs e)
        {
            //throw new NotImplementedException();
            totalNet = 0; currentPrintPage = 0;
        }
        private void Document_PrintPageReceiptDetail(object sender, PrintPageEventArgs e)
        {
            //ทำให้เหมือนเดิม Document_PrintPageReceipt1 แต่แยก header, table header, table rows, total+footer ออกเป็น method แยกกัน แล้วค่อยเรียกใช้ในนี้อีกที
            Graphics g = e.Graphics;
            float pageWidth = e.PageBounds.Width, pageHeight = e.PageBounds.Height, y = margin;
            colDesc = margin;
            colDescW = pageWidth - margin * 2 - 270;
            colAmount = colDesc + colDescW;
            colDiscount = colAmount + colAmountW;
            colNet = colDiscount + colDiscountW;
            currentPrintPage++;
            //totalNet = 0;
            // ... (fonts, brushes เหมือนเดิม)
            // ============================================================
            // HEADER — พิมพ์ทุกหน้า
            // ============================================================
            y = PrintReceiptHeader(g, pageWidth, margin, currentPrintPage);//Document_PrintPageReceiptDetail   ^ แยก header ออกเป็น method แล้ว return ค่า y หลัง header            
            // ============================================================
            // TABLE HEADER — พิมพ์ทุกหน้า
            // ============================================================
            y = PrintReceiptTableHeader(g, pageWidth, margin, y);
            // ============================================================
            // TABLE ROWS — วนต่อจาก currentPrintRow
            // ============================================================
            float footerHeight = 120f; // พื้นที่สำรองไว้สำหรับ total+footer (หน้าสุดท้าย)  // ลดลงจาก 200f
            float maxY = pageHeight - margin - footerHeight - 40;
            var allRows = DTPRINT.AsEnumerable().ToList(); // หรือแยก normal/doctor ตามเดิม
            while (currentPrintRow < allRows.Count)
            {
                DataRow row = allRows[currentPrintRow];
                DataRow rownext = null;
                if (currentPrintRow + 1 < allRows.Count)        {       rownext = allRows[currentPrintRow + 1];     }
                float rowH = MeasureRowHeight(g, row,"", pageWidth); // ความสูงแต่ละ row
                // ถ้า row นี้จะเกินหน้า
                if (y + rowH > maxY)
                {
                    PrintReceiptTotal(g, pageWidth, pageHeight, margin, 0, true);
                    PrintReceiptFooter(g, pageWidth, pageHeight, margin);
                    e.HasMorePages = true; return; /* หยุดแค่นี้ — event จะถูกเรียกอีกครั้งสำหรับหน้าถัดไป */
                }
                totalNet += PrintReceiptDetailRow(g, row,rownext, pageWidth, margin, y);
                y += rowH;
                currentPrintRow++;
            }
            y = PrintReceiptTotal(g, pageWidth, pageHeight, margin, totalNet, false); //Document_PrintPageReceiptDetail
            PrintReceiptFooter(g, pageWidth, pageHeight, margin);//Document_PrintPageReceiptDetail

            e.HasMorePages = false;

            // Reset สำหรับครั้งถัดไป
            currentPrintRow = 0;
            currentPrintPage = 0;
        }
        private void printReceiptSIMB2()
        {
            DTPRINT = DTSIMB2;
            if (DTPRINT.Rows.Count == 0) { lfSbMessage.Text = "ไม่พบข้อมูล"; return; }
            // Reset counter
            currentPrintRow = 0; totalNet = 0;
            PRINTORIGINAL = "ใบเสร็จรับเงิน/Receipt (Original ต้นฉบับ)";
            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrinterName = bc.iniC.printerA4;
            document.PrintPage += Document_PrintPageReceipt1;
            document.BeginPrint += Document_BeginPrintReceipt1;
            document.DefaultPageSettings.Landscape = false;
            if (bc.iniC.statusPrintPreview.Equals("1"))
            {
                using (PrintPreviewDialog preview = new PrintPreviewDialog())
                {
                    preview.Document = document;
                    preview.Width = 900;
                    preview.Height = 700;
                    preview.Text = PRINTORIGINAL;
                    preview.ShowDialog();   // แสดง Preview → กด Print ใน Dialog ได้เลย
                }
                PRINTORIGINAL = "ใบเสร็จรับเงิน/Receipt (copy สำเนา)";
                //document.PrintPage += Document_PrintPageReceipt1;
                //document.Print();
            }
            else
            {
                document.Print();
                PRINTORIGINAL = "ใบเสร็จรับเงิน/Receipt (copy สำเนา)";
                //document.PrintPage += Document_PrintPageReceipt1;
                document.Print();
            }
            // Clear ข้อมูลหลังพิมพ์เสร็จ
            DTPRINT = null; DTSIMB2 = null;
        }

        private void Document_BeginPrintReceipt1(object sender, PrintEventArgs e)
        {
            //throw new NotImplementedException();
            currentPrintRow = 0; totalNet = 0;
        }

        private void Document_PrintPageReceipt(object sender, PrintPageEventArgs e)
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
            if (DTPRINT == null || DTPRINT.Rows.Count == 0) { e.HasMorePages = false; lfSbMessage.Text = "ไม่พบข้อมูลใบสั่ง Xray"; return; }
            String pttname = "", deptname = "", dtrname = "", vsdatetime = "", reqdatetime = "", compname = "", paidname = "", hn = "", vn = "";
            String userreq = "", line = null, prndate = "", price = "", qty = "", price1 = "";
            float colName = 10, col1 = 50, col2 = 250, col3 = 450, col4 = 550, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, col5 = 620;
            prndate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            var logo1 = System.Drawing.Image.FromFile(Environment.CurrentDirectory + "\\LOGO-BW-tran.jpg");
            System.Drawing.Image logo = (System.Drawing.Image)logo1.Clone();
            logo1.Dispose();
            if (currentPrintRow == 0 || currentPrintRow >= DTPRINT.Rows.Count)
            {
                DataRow firstRow = DTPRINT.Rows[0];
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
            g.DrawImage(logo, colName, startY);
            g.DrawString("โรงพยาบาล บางนา5", fPDF, Brushes.Black, colName, startY);                               offset += (int)fontHeight;
            g.DrawString("Bangna5 General Hospital " + pttname, fPDF, Brushes.Black, col2, startY + offset);    offset += (int)fontHeight;
            g.DrawString(bc.iniC.hostaddresst, fPDF, Brushes.Black, col4, startY + offset);                     offset += (int)fontHeight;
            g.DrawString(bc.iniC.hostaddresse, fPDF, Brushes.Black, col4, startY + offset);                     offset += (int)fontHeight;

            offset += (int)fontHeight;
            g.DrawString("ใบเสร็จรับเงิน", fPDF, Brushes.Black, colName, startY + offset);                           offset += (int)fontHeight;
            g.DrawString("เลขประจำตัวผู้เสียภาษี Tax ID " , fPDF, Brushes.Black, col2, startY + offset);
            g.DrawString("VN: " + VS.VN, fPDF, Brushes.Black, col4, startY + offset);

            offset += (int)fontHeight + gapline;
            //g.DrawString("เลขที่ใบสั่ง: " + REQNOXRAY + "/" + REQDATEXRAY, fPDF, Brushes.Black, col2, startY + offset);
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
            int rowsOnThisPage = 0; int startRow = currentPrintRow;

            for (int i = startRow; i < DTPRINT.Rows.Count && rowsOnThisPage < MAX_ROWS_PER_PAGE; i++)
            {
                DataRow arow = DTPRINT.Rows[i];
                offset += (int)fontHeight + 5;
                String stat = "";
                String desc = (arow["order_name"]?.ToString() ?? "") + " " + (arow["order_code"]?.ToString() ?? "");
                float price2 = 0;
                float.TryParse(arow["MNC_XR_PRI"]?.ToString() ?? "0", out price2);
                g.DrawString(stat, fPDF, Brushes.Black, MARGIN_LEFT, startY + offset);
                g.DrawString(desc, fPDF, Brushes.Black, col1, startY + offset);
                g.DrawString(price2.ToString("#,###.00"), fPDF, Brushes.Black, col4, startY + offset);
                rowsOnThisPage++; currentPrintRow++;
            }
            int pagecnt = 0;
            pagecnt = (DTPRINT.Rows.Count / 10);
            if ((DTPRINT.Rows.Count % 10) > 0) pagecnt++;
            // ✅ Footer
            offset += (int)fontHeight + 5;
            g.DrawString("...............................................................................................................................................................................................................................................", fPDF, Brushes.Black, startX, MAX_Y);
            g.DrawString("user/เจ้าหน้าที่ " + userreq, fPDF, Brushes.Black, col1, MAX_Y + 15);
            g.DrawString("total/รวมรายการ " + DTPRINT.Rows.Count, fPDF, Brushes.Black, col3, MAX_Y + 15);
            g.DrawString("page " + pagecur + "/" + pagecnt, fPDF, Brushes.Black, col5, MAX_Y + 15);
            g.DrawString("...............................................................................................................................................................................................................................................", fPDF, Brushes.Black, startX, MAX_Y + 25);
            g.DrawString("FM-XRT-040 (00-08/04/59)(1/1)", fPDFs2, Brushes.Black, startX, MAX_Y + 30);

            // ✅ ตรวจสอบว่ายังมีรายการเหลืออยู่หรือไม่
            if (currentPrintRow < DTPRINT.Rows.Count)
            {
                pagecur++;
                e.HasMorePages = true;  // พิมพ์หน้าต่อไป
            }
            else
            {
                e.HasMorePages = false; // พิมพ์เสร็จแล้ว
                currentPrintRow = 0; // Reset counter
            }
        }
        private void Document_PrintPageReceipt1(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float pageWidth = e.PageBounds.Width, pageHeight = e.PageBounds.Height, y = margin;            
            colDesc = margin;
            colDescW = pageWidth - margin * 2 - 270;
            colAmount = colDesc + colDescW;
            colDiscount = colAmount + colAmountW;
            colNet = colDiscount + colDiscountW;
            totalNet = 0;
            currentPrintPage++;
            // ... (fonts, brushes เหมือนเดิม)
            // ============================================================
            // HEADER — พิมพ์ทุกหน้า
            // ============================================================
            y = PrintReceiptHeader(g, pageWidth, margin, currentPrintPage);//Document_PrintPageReceipt1   ^ แยก header ออกเป็น method แล้ว return ค่า y หลัง header            
            // ============================================================
            // TABLE HEADER — พิมพ์ทุกหน้า
            // ============================================================
            y = PrintReceiptTableHeader(g, pageWidth, margin, y);
            // ============================================================
            // TABLE ROWS — วนต่อจาก currentPrintRow
            // ============================================================
            float footerHeight = 120f; // พื้นที่สำรองไว้สำหรับ total+footer (หน้าสุดท้าย)
            float maxY = pageHeight - margin - footerHeight - 40;
            var allRows = DTPRINT.AsEnumerable().ToList(); // หรือแยก normal/doctor ตามเดิม
            while (currentPrintRow < allRows.Count)
            {
                DataRow row = allRows[currentPrintRow];
                float rowH = MeasureRowHeight(g, row,"", pageWidth); // ความสูงแต่ละ row
                // ถ้า row นี้จะเกินหน้า
                if (y + rowH > maxY)
                {
                    PrintReceiptTotal(g, pageWidth, pageHeight, margin, totalNet, true);
                    PrintReceiptFooter(g, pageWidth, pageHeight, margin);
                    e.HasMorePages = true;                    return; /* หยุดแค่นี้ — event จะถูกเรียกอีกครั้งสำหรับหน้าถัดไป */
                }
                totalNet += PrintReceiptRow(g, row, pageWidth, margin, y);
                y += rowH;
                currentPrintRow++;
            }
            y = PrintReceiptTotal(g, pageWidth, pageHeight, margin, totalNet, false);
            PrintReceiptFooter(g, pageWidth, pageHeight, margin);
            e.HasMorePages = false;
            // Reset สำหรับครั้งถัดไป
            currentPrintRow = 0;
            currentPrintPage = 0;
        }
        private float PrintReceiptTableHeader(Graphics g, float pageWidth, float margin, float y)
        {
            // พิมพ์ header ของตาราง เช่น "รายการ", "ราคา", "จำนวน" ฯลฯ
            // ใช้ g.DrawString(...) เพื่อพิมพ์ header
            // คำนวณตำแหน่ง x ของแต่ละ column ตามที่กำหนดไว้
            // Return ค่า y หลังจากพิมพ์ header เสร็จ (เพื่อใช้เป็นจุดเริ่มต้นของ rows)
            // ============================================================
            // TABLE HEADER
            // ============================================================
            float colDesc = margin, rowH = 20f, colDescW = pageWidth - margin * 2 - 270, colAmount = colDesc + colDescW;
            float colAmountW = 90f, colDiscountW = 90f, colNetW = 90f, colDiscount = colAmount + colAmountW, colNet = colDiscount + colDiscountW;
            // เส้นบน header
            g.DrawLine(Pens.Black, margin, y, pageWidth - margin, y);
            y += 2;
            //g.DrawRectangle(Pens.Black, margin, y, pageWidth - margin * 2, rowH);
            // วาดพื้นหลังสีจางๆ
            g.FillRectangle(new SolidBrush(Color.FromArgb(232, 232, 232)), margin, y-1, pageWidth - margin * 2, rowH*2);

            g.DrawString("รายการ", fntBold, Brushes.Black,              new RectangleF(colDesc, y, colDescW, rowH), sfCenter);
            g.DrawString("จำนวนเงิน", fntBold, Brushes.Black,            new RectangleF(colAmount, y, colAmountW, rowH), sfCenter);
            g.DrawString("ส่วนลด", fntBold, Brushes.Black,               new RectangleF(colDiscount, y, colDiscountW, rowH), sfCenter);
            g.DrawString("คงเหลือ", fntBold, Brushes.Black,              new RectangleF(colNet, y, colNetW, rowH), sfCenter);
            y += rowH;

            g.DrawString("Description", fntBold, Brushes.Black,         new RectangleF(colDesc, y, colDescW, rowH), sfCenter);
            g.DrawString("Amount", fntBold, Brushes.Black,              new RectangleF(colAmount, y, colAmountW, rowH), sfCenter);
            g.DrawString("Discount", fntBold, Brushes.Black,            new RectangleF(colDiscount, y, colDiscountW, rowH), sfCenter);
            g.DrawString("Net Amount", fntBold, Brushes.Black,          new RectangleF(colNet, y, colNetW, rowH), sfCenter);
            y += rowH;

            g.DrawLine(Pens.Black, margin, y, pageWidth - margin, y);
            
            y += 4;
            //return y + 20f; // ตัวอย่าง เพิ่มความสูง 20 หลัง header
            return y; // ตัวอย่าง เพิ่มความสูง 20 หลัง header
        }
        private decimal PrintReceiptDetailRow(Graphics g, DataRow row, DataRow rownext, float pageWidth, float margin, float y)
        {
            // พิมพ์ข้อมูลแต่ละ row ตาม column ต่างๆ
            // ใช้ row["COLUMN_NAME"] เพื่อดึงข้อมูล
            // ใช้ g.DrawString(...) เพื่อพิมพ์ข้อมูล
            // คำนวณตำแหน่ง x ของแต่ละ column ตามที่กำหนดไว้
            string space = "     ", desc1="", codebsg="", location="";
            string parent = row["service_type"]?.ToString() ?? "";
            string desc = parent.Equals("parent") ? row["item_name"]?.ToString() ?? "" :  row["MNC_DEF_DSC"]?.ToString() ?? "";
            if((rownext != null)&&parent.Equals("parent2")) {
                location = rownext["location_simb2"]?.ToString() ?? "";
                if (!location.Equals("")) desc1 = desc + " (" + location+")";
            }
            else            {                desc1 = row["item_name"]?.ToString() ?? "";            }
            codebsg = row["CodeBSG"]?.ToString() ?? "";
            if (codebsg.Equals("1.1"))
            {

            }
            if (codebsg.Equals("1.2"))
            {

            }
            //decimal amount = row["MNC_FN_AMT"] != DBNull.Value ? Convert.ToDecimal(row["MNC_FN_AMT"]) : 0;
            decimal amount = row["amount"] != DBNull.Value ? Convert.ToDecimal(row["amount"]) : 0;
            //decimal disc = row["MNC_FN_DIS"] != DBNull.Value ? Convert.ToDecimal(row["MNC_FN_DIS"]) : 0;
            decimal disc = row["discount"] != DBNull.Value ? Convert.ToDecimal(row["discount"]) : 0;
            decimal amt = amount - disc;
            if (parent.Equals("parent")) { desc = codebsg + " " + desc1; amount = 0; disc = 0; amt = 0; }
            else if (parent.Equals("parent1")) { desc = space + codebsg + " " + desc1; }
            else if (parent.Equals("parent2")) { desc = space + space + codebsg + " " + desc1; }
            else desc = space + space + space + desc1;
            
            //totalNet += net;MNC_SIMB_CD
            desc = row["MNC_SIMB_CD"]?.ToString() + "  " + desc;
            g.DrawString(desc, fntNormal, Brushes.Black, new RectangleF(colDesc + 4, y, colDescW - 4, rowH));
            if (amount != 0) g.DrawString(amount.ToString("#,##0.00"), fntNormal, Brushes.Black, new RectangleF(colAmount, y, colAmountW - 4, rowH), sfRight);
            if (disc != 0) g.DrawString(disc.ToString("#,##0.00"), fntNormal, Brushes.Black, new RectangleF(colDiscount, y, colDiscountW - 4, rowH), sfRight);
            if (amt > 0) g.DrawString(amt.ToString("#,##0.00"), fntNormal, Brushes.Black, new RectangleF(colNet, y, colNetW - 4, rowH), sfRight);
            //if (!parent.Equals("parent"))
            //{
            //    if ((!parent.Equals("parent1")))
            //    {
            //        if ((!parent.Equals("parent2")))
            //        {
            //            g.DrawString(totalnet.ToString("#,##0.00"), fntNormal, Brushes.Black, new RectangleF(colNet, y, colNetW - 4, rowH), sfRight);
            //        }
            //    }
            //}
            y += rowH;
            return amt;
        }
        private decimal PrintReceiptRow(Graphics g, DataRow row, float pageWidth, float margin, float y)
        {
            // พิมพ์ข้อมูลแต่ละ row ตาม column ต่างๆ
            // ใช้ row["COLUMN_NAME"] เพื่อดึงข้อมูล
            // ใช้ g.DrawString(...) เพื่อพิมพ์ข้อมูล
            // คำนวณตำแหน่ง x ของแต่ละ column ตามที่กำหนดไว้
            string desc = row["BillingSubGroupTH"]?.ToString() ?? "";
            decimal amount = row["MNC_FN_AMT"] != DBNull.Value ? Convert.ToDecimal(row["MNC_FN_AMT"]) : 0;
            decimal disc = row["MNC_FN_DIS"] != DBNull.Value ? Convert.ToDecimal(row["MNC_FN_DIS"]) : 0;
            decimal totalnet = amount - disc;
            //totalNet += net;MNC_SIMB_CD
            //desc = row["MNC_SIMB_CD"]?.ToString()+"  " + desc;
            String codebsg = row["CodeBSG"]?.ToString() ?? "";
            codebsg = codebsg + "          ";
            codebsg = codebsg.Substring(0, 10);
            desc = codebsg + "  " + desc;
            g.DrawString(desc, fntNormal, Brushes.Black, new RectangleF(colDesc + 4, y, colDescW - 4, rowH));
            if (amount != 0) g.DrawString(amount.ToString("#,##0.00"), fntNormal, Brushes.Black, new RectangleF(colAmount, y, colAmountW - 4, rowH), sfRight);
            if (disc != 0) g.DrawString(disc.ToString("#,##0.00"), fntNormal, Brushes.Black, new RectangleF(colDiscount, y, colDiscountW - 4, rowH), sfRight);
            g.DrawString(totalnet.ToString("#,##0.00"), fntNormal, Brushes.Black, new RectangleF(colNet, y, colNetW - 4, rowH), sfRight);
            y += rowH;
            return totalnet;
        }
        private float PrintReceiptTotal(Graphics g, float pageWidth, float pageHeight, float margin, Boolean hasmorepage)
        {
            float footerHeight = 160f; // ความสูงของ footer ทั้งหมด
            float y = pageHeight - margin - footerHeight+70, top = 0f;
            g.DrawLine(Pens.Black, margin, y, pageWidth - margin, y);
            return y + 20f; // ตัวอย่าง เพิ่มความสูง 20 หลัง total
        }
        private float PrintReceiptTotal(Graphics g, float pageWidth, float pageHeight, float margin, decimal totalNet, Boolean hasmorepage)
        {
            // พิมพ์ส่วน total เช่น "รวมทั้งสิ้น", "ส่วนลด", "ยอดสุทธิ" ฯลฯ
            // ใช้ g.DrawString(...) เพื่อพิมพ์ total
            // ============================================================
            // TOTAL
            // ============================================================
            // ดึงข้อมูล header จาก row แรก
            //decimal totalnet1 = totalNet;
            //new LogWriter("e", "PrintReceiptTotal totalNet " + totalNet);
            float footerHeight = 160f; // ความสูงของ footer ทั้งหมด
            float y = pageHeight - margin - footerHeight,top=0f;
            //top = (y + 8 + 36 +29) - (y);
            top = 8 + 36 + 29;
            DataRow hdr = DTPRINT.Rows[0];
            g.DrawLine(Pens.Black, margin, y, pageWidth - margin, y);
            y += 1;
            // ✅ วาดพื้นหลังสีจางๆ ให้ส่วน Total
            g.FillRectangle(new SolidBrush(Color.FromArgb(232, 232, 232)), margin, (y), pageWidth - (margin * 2), top-2);
            y += 6;
            decimal amount = hdr["MNC_PAY_CASH"] != DBNull.Value ? Convert.ToDecimal(hdr["MNC_PAY_CASH"]) : 0;
            if (amount <= 0) { if(hdr.Table.Columns.Contains("MNC_PAY_CARD")) amount = hdr["MNC_PAY_CARD"] != DBNull.Value ? Convert.ToDecimal(hdr["MNC_PAY_CARD"]) : 0; }
            if (PRINTORIGINAL.Equals("ใบแจ้งรายละเอียดค่ารักษาพยาบาล/Receipt Detail (Original ต้นฉบับ)"))
            {
                amount = totalNet;
            }
            decimal discount = 0;
            if (DTPRINT.Columns.Contains("MNC_DIS_TOT"))
            {
                discount = hdr["MNC_DIS_TOT"] != DBNull.Value ? Convert.ToDecimal(hdr["MNC_DIS_TOT"]) : 0;
            }
            string totalText = NumberToText(amount);
            string payMethod = "";
            //payMethod = hdr["MNC_NO"]?.ToString() ?? "";
            if (!string.IsNullOrEmpty(CARDTXT))            {                payMethod = CARDTXT;            }
            String txt = "จำนวนเงิน\nTotal Amount in letters", txttotalnet="", txtdiscount="";
            SizeF sz = g.MeasureString(txt, fntNormal, (int)colDescW);
            g.DrawString(txt, fntNormal, Brushes.Black,                new RectangleF(margin, y, sz.Width, sz.Height));
            if(hasmorepage) totalText = " (มีรายการต่อหน้าถัดไป)";
            g.DrawString($"{totalText}", fntNormal, Brushes.Black,                new RectangleF(margin + 124, y, colDescW - 124, 18));
            txt = "รวมทั้งสิ้น/Total";            sz = g.MeasureString(txt, fntNormal, (int)colDescW);
            g.DrawString(txt, fntBold, Brushes.Black,                new RectangleF(colAmount, y, colAmountW + colDiscountW, sz.Height), sfLeft);
            if (hasmorepage) txttotalnet = ""; else txttotalnet = totalNet.ToString("#,##0.00");     //มีต่อหน้าถัดไป
            g.DrawString(txttotalnet, fntBold, Brushes.Black,                new RectangleF(colNet, y, colNetW - 4, 18), sfRight);
            
            txt = "ส่วนลด/Discount"; sz = g.MeasureString(txt, fntNormal, (int)colDescW);
            g.DrawString(txt, fntBold, Brushes.Black, new RectangleF(colAmount, (y+20), colAmountW + colDiscountW, sz.Height), sfLeft);
            if(discount != 0) txtdiscount = discount.ToString("#,##0.00"); else txtdiscount = "";
            g.DrawString(txtdiscount, fntBold, Brushes.Black, new RectangleF(colNet, y+20, colNetW - 4, 18), sfRight);
            txt = "สุทธิ/nettotal"; sz = g.MeasureString(txt, fntNormal, (int)colDescW);
            float ynettotal = (y + 40);
            g.DrawString(txt, fntBold, Brushes.Black, new RectangleF(colAmount, ynettotal, colAmountW + colDiscountW, sz.Height), sfLeft);

            y += 36;
            txt = "ช่องทางชำระเงิน " + payMethod;            //sz = g.MeasureString(txt, fntSmall, (int)colDescW);
            float colH2 = 0;
            if (txt.Length > 150) { colH2 = MeasureRowWidth(g, txt, pageWidth, fntSmall);
                g.DrawString(txt, fntSmall, Brushes.Black, new RectangleF(margin, y + 12, colH2, sz.Height), sfLeft);
            }
            else { colH2 = MeasureRowWidth(g, txt, pageWidth, fntNormal);
                g.DrawString(txt, fntSmall, Brushes.Black, new RectangleF(margin, y + 12, colH2, sz.Height), sfLeft);
            }

            txttotalnet = amount.ToString("#,##0.00");
            g.DrawString(txttotalnet, fntBold, Brushes.Black, new RectangleF(colNet, ynettotal, colNetW - 4, sz.Height), sfRight);
            y += 29;
            g.DrawLine(Pens.Black, margin, y+2, pageWidth - margin, y);
            
            //y += 20;
            return y + 20f; // ตัวอย่าง เพิ่มความสูง 20 หลัง total
        }
        private void PrintReceiptFooter(Graphics g, float pageWidth, float pageHeight, float margin)
        {
            // พิมพ์ส่วน footer เช่น "ขอบคุณที่ใช้บริการ", "ติดต่อเราได้ที่..." ฯลฯ
            // ใช้ g.DrawString(...) เพื่อพิมพ์ footer
            // ============================================================
            // FOOTER
            // ============================================================
            float footerHeight = 80f; // ความสูงของ footer ทั้งหมด
            float y = pageHeight - margin - footerHeight;
            string footerTh = "เพื่อความปลอดภัยของผู้ป่วย โรงพยาบาลขอสงวนสิทธิ์ไม่รับเปลี่ยนหรือคืนยา";
            string footerEn = "For patient safety, The hospital reseves the right not to accept any exchange or return of medication";
            g.DrawString(footerTh, fntSmall, Brushes.Black, new RectangleF(margin, y, pageWidth - margin * 2 - 120, 16), sfCenter);
            y += 16;
            g.DrawString(footerEn, fntSmall, Brushes.Black, new RectangleF(margin, y, pageWidth - margin * 2 - 120, 16), sfCenter);
            y += 24;

            // Signature
            float sigX = pageWidth - margin - 130;
            g.DrawLine(Pens.Black, sigX, y, sigX + 120, y);
            y += 2;
            
            DataRow hdr = DTPRINT.Rows[0];
            //String aaa =  hdr["MNC_USR_CD"].ToString();
            string usrCode = GetColumnValue(hdr, "MNC_USR_CD");
            usrCode = bc.bcDB.stfDB.selectBycd(usrCode);
            float colH1 = MeasureRowWidth(g, usrCode, pageWidth, fntNormal);
            g.DrawString(usrCode, fntNormal, Brushes.Black, new RectangleF(sigX, y, colH1, 18), sfCenter);
            y += 18;
            colH1 = MeasureRowWidth(g, "เจ้าหน้าที่การเงิน / Cashier", pageWidth, fntSmall);
            g.DrawString("เจ้าหน้าที่การเงิน / Cashier", fntSmall, Brushes.Black, new RectangleF(sigX, y, colH1, 16), sfCenter);
        }
        // เพิ่ม method ช่วยใน FrmCashier class
        private string GetColumnValue(DataRow row, string columnName, string defaultValue = "")
        {
            if (row == null || row.Table == null)
                return defaultValue;

            if (!row.Table.Columns.Contains(columnName))
                return defaultValue;

            return row[columnName]?.ToString() ?? defaultValue;
        }
        private float MeasureRowHeight(Graphics g, DataRow row, String txt, float pageWidth)
        {
            float colDescW = pageWidth - 40f * 2 - 270; // เหมือนที่คำนวณใน handler
            //fntNormal = new Font(pfc.Families[0], 13);
            SizeF sz;
            if (row != null)            {                sz = g.MeasureString(row["MNC_DEF_DSC"]?.ToString(), fntNormal, (int)colDescW);            }
            else            {                sz = g.MeasureString(txt, fntNormal, (int)colDescW);            }
            //fntNormal.Dispose();
            return Math.Max(sz.Height, 20f);
        }
        private float MeasureRowWidth(Graphics g, String txt, float pageWidth, Font fnt)
        {
            float colDescW = pageWidth - 40f * 2 - 270; // เหมือนที่คำนวณใน handler
            //fntNormal = new Font(pfc.Families[0], 13);
            SizeF sz;
            sz = g.MeasureString(txt, fnt, (int)colDescW);
            //fntNormal.Dispose();
            return Math.Max(sz.Width, 20f);
        }
        private float PrintReceiptHeader(Graphics g, float pageWidth, float margin, int pageNo)
        {
            //new LogWriter("d", this.Name + " PrintReceiptHeader  ");
            float y = margin;
            DataRow hdr = DTPRINT.Rows[0];
            // ----------------------------------------
            // เลขหน้า (top right)
            // ----------------------------------------
            //new LogWriter("d", this.Name + " PrintReceiptHeader  aaa");
            int totalPages = CalcTotalPages1(g, pageWidth, margin);
            prndate = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
            g.DrawString($"{pageNo}/{totalPages}", fntNormal, Brushes.Black,  new RectangleF(pageWidth - margin - 50, y, 50, 18), sfRight);
            //new LogWriter("d", this.Name + " PrintReceiptHeader 00 ");
            if ((logo != null)  && (imgRatio > boxRatio)) /* รูปกว้างกว่า */{  float h = logoW / imgRatio; float yOffset = (logoH - h) / 2;    g.DrawImage(logo, margin, y + yOffset-10, logoW, h);   }
            else /* รูปสูงกว่าหรือพอดี */    {       float w = logoH * imgRatio;     float xOffset = (logoW - w) / 2;    g.DrawImage(logo, margin + xOffset, y, w, logoH);   }
            //new LogWriter("d", this.Name + " PrintReceiptHeader 01 ");
            //float cx = margin + 65;
            float cx = margin + 25;
            float cw = Math.Max(1f, pageWidth - margin * 2 - 65);
            g.DrawString(bc.iniC.hostname, fntTitle, Brushes.Black, new RectangleF(cx, y, cw, (rowH+2)), sfCenter);
            y += rowH;
            g.DrawString(bc.iniC.hostnamee, fntTitle, Brushes.Black, new RectangleF(cx, y, cw, (rowH+2)), sfCenter);
            y += (rowH+2);
            g.DrawString(bc.iniC.hostaddresst, fntSmall, Brushes.Black, new RectangleF(cx, y, cw, (rowH)), sfCenter);
            y += (rowH-2);
            g.DrawString(bc.iniC.hostaddresse, fntSmall, Brushes.Black, new RectangleF(cx, y, cw, (rowH-2)), sfCenter);
            y += rowH;
            g.DrawString("เลขประจำตัวผู้เสียภาษี", fntNormal, Brushes.Black, new RectangleF(pageWidth / 2, y, pageWidth / 2 - margin, rowH), sfRight);
            y += (rowH-2);
            g.DrawString("Tax ID 0105526004138", fntNormal, Brushes.Black, new RectangleF(pageWidth / 2, y, pageWidth / 2 - margin, rowH), sfRight);
            y += 4;
            //if (pageNo == 1)
            //{float rowH = MeasureRowHeight(g, row, pageWidth); // ความสูงแต่ละ row
            float rowH1 = MeasureRowHeight(g, null, PRINTORIGINAL, pageWidth); // ความสูงแต่ละ row
            float colH1 = MeasureRowWidth(g, PRINTORIGINAL, pageWidth, fntBold); // ความกว้างแต่ละ row
            RectangleF rcTitle = new RectangleF(margin, y-15, colH1+15, rowH1);
            g.DrawRectangle(Pens.Black, rcTitle.X, rcTitle.Y-5, rcTitle.Width, rcTitle.Height+5);
            g.DrawString(PRINTORIGINAL, fntBold, Brushes.Black, rcTitle, sfCenter);
            
            y += 32;
            if ((txtCompName != null) && (txtCompName.Text.Trim().Length > 0))
            {
                String addr = txttaxid.Text.Trim() + " "+txtCompName.Text.Trim();
                g.DrawString(addr, fntNormal, Brushes.Black, margin, y - 18, sfLeft);
                //g.DrawString(txtAddr1.Text.Trim(), fntNormal, Brushes.Black, margin, y - 20, sfLeft);
                g.DrawString(txtAddr1.Text.Trim(), fntNormal, Brushes.Black, margin, y, sfLeft);
                y += (rowH+2);
            }
            // ----------------------------------------
            // ข้อมูลผู้ป่วย — เฉพาะหน้าแรก
            // ----------------------------------------
            //string ptName = hdr["pttname"]?.ToString() ?? "";
            //string hnText = hdr["hn"]?.ToString() ?? "";
            //if(hdr["MNC_DOC_DAT"].ToString().Length<=0) return y;
            DateTime dtdocdate = new DateTime();
            DateTime.TryParse(hdr["MNC_DOC_DAT"]?.ToString(), out dtdocdate);
            if(dtdocdate.Year <= 1900) { dtdocdate = dtdocdate.AddYears(543); }
            //new LogWriter("d", this.Name + " PrintReceiptHeader 02 dtdocdate " + dtdocdate.ToString("dd-MM-yyyy"));
            string vnText = VS.VN;
            string docNo = "";
            if(PRINTORIGINAL.Equals("ใบเสร็จรับเงิน/Receipt (Original ต้นฉบับ)"))
            {
                docNo = "BO" + (dtdocdate.Year + 543) + "-" + hdr["MNC_DOC_NO"]?.ToString().PadLeft(6, '0');
            }
            else if(PRINTORIGINAL.Equals("ใบแจ้งรายละเอียดค่ารักษาพยาบาล/Receipt Detail (Original ต้นฉบับ)"))
            {
                String an = hdr["an_no"]?.ToString() ?? "";
                if (an.Length > 0)                {                    docNo = "PI" + (dtdocdate.Year + 543) + "-" + hdr["MNC_DOC_NO"]?.ToString().PadLeft(6, '0');                }
                else                {                    docNo = "PO" + (dtdocdate.Year + 543) + "-" + hdr["MNC_DOC_NO"]?.ToString().PadLeft(6, '0');                }
            }
            //docNo ="BO" + (dtdocdate.Year+543) + "-"+ hdr["MNC_DOC_NO"]?.ToString();
            string docDate = bc.datetoShow1(hdr["MNC_DOC_DAT"]?.ToString() ?? "");
            string visitDate = hdr["MNC_DATE"]?.ToString() ?? "";
            string discdate = "";
            if(hdr.Table.Columns.Contains("ds_date"))            {                discdate = hdr["ds_date"]?.ToString() ?? "";            }
            string rightType = hdr["MNC_FN_TYP_DSC"]?.ToString() ?? "";
            string symptoms = hdr["MNC_SHIF_MEMO"]?.ToString() ?? "";
            visitDate = bc.datetoShow1(visitDate) +" "+ bc.showTime(hdr["MNC_TIME"]?.ToString() ?? "");
            discdate = discdate.Length>0 ? bc.datetoShow1(discdate) : "";
            if (DISCHARGEDATE.Length > 0)
            {
                String[] dis = DISCHARGEDATE.Split(' ');
                if (dis.Length > 1)                {                    DISCHARGEDATE = bc.datetoShow1(dis[0]) + " " + dis[1];                }
                visitDate += " - " + bc.datetoShow1(DISCHARGEDATE);
            }
            //visitDate += discdate.Length>0 ? " - " + discdate : "";
            g.DrawString($"ชื่อ - นามสกุล / Patient name : {PTT.Name}", fntNormal, Brushes.Black, margin, y);
            g.DrawString($"เลขที่เอกสาร/Document No  {docNo}", fntNormal, Brushes.Black, new RectangleF(pageWidth / 2, y, pageWidth / 2 - margin, rowH), sfRight);  // ← sfRight หรือ sfLeft แล้วแต่ layout
            y += rowH;
            g.DrawString($"เลขประจำตัวผู้ป่วย / HN :  {PTT.Hn}      VN : {vnText}", fntNormal, Brushes.Black, margin, y);
            g.DrawString($"วันที่ / Date :  {docDate}", fntNormal, Brushes.Black,   new RectangleF(pageWidth / 2, y, pageWidth / 2 - margin, 18), sfRight);
            y += rowH;
            g.DrawString($"วันที่พบแพทย์ / Visit date :  {visitDate}", fntNormal, Brushes.Black, margin, y);
            g.DrawString($"วันที่พิมพ์ :  {prndate}", fntNormal, Brushes.Black, new RectangleF(pageWidth / 2, y, pageWidth / 2 - margin, 18), sfRight);
            y += rowH;
            g.DrawString($"สิทธิการรักษา:   {rightType}", fntNormal, Brushes.Black, margin, y);
            g.DrawString($"มารักษาด้วยอาการ :  {symptoms.Replace("\r\n"," ")}", fntNormal, Brushes.Black, new RectangleF(pageWidth / 2, (y-2), pageWidth / 2 - margin, (rowH+3)), sfRight);
            y += (rowH+5);
            //}
            //else
            //{
            //    // หน้า 2+ แสดงแค่ HN/ชื่อ ย่อ ๆ แทน
            //    g.DrawString($"HN: {PTT.Hn}  {PTT.Name}  (ต่อ)", fntNormal, Brushes.Black, margin, y);
            //    y += 22;
            //}
            //fntTitle.Dispose(); fntBold.Dispose(); fntNormal.Dispose(); fntSmall.Dispose();
            return y; // คืนค่า y ให้ handler ใช้ต่อ
        }
        private int CalcTotalPages(Graphics g, float pageWidth, float margin)
        {
            // ประมาณจำนวนหน้าจาก จำนวน rows ÷ rows ต่อหน้า
            float pageHeight = 1169f; // A4 ~1169 units
            float headerHeight = 300f;  // ความสูง header หน้าแรก (ปรับตามจริง)
            float headerNext = 300f;  // header หน้าถัดไป
            float footerHeight = 160f;
            //float rowH = 20f;   // ความสูงเฉลี่ยต่อ row
            int totalRows = DTPRINT.Rows.Count;
            float firstPageRows = (pageHeight - (margin * 2) - headerHeight - footerHeight) / rowH;
            if (totalRows <= (int)firstPageRows) return 1;
            float nextPageRows = (pageHeight - (margin * 2) - headerNext - footerHeight) / rowH;
            int remaining = totalRows - (int)firstPageRows;
            return 1 + (int)Math.Ceiling(remaining / nextPageRows);
        }
        private int CalcTotalPages1(Graphics g, float pageWidth, float margin)
        {
            float pageHeight = 1169f; // A4
            float headerHeight = 300f;
            float footerHeight = 160f; // ✅ ปรับให้ตรงกับ PrintReceiptTotal

            // หน้าที่ 1
            float y = margin + headerHeight;
            float maxY = pageHeight - margin - footerHeight - 40;
            int pageCount = 1;

            // วนลูปแต่ละ row เพื่อคำนวณความสูงจริง
            var allRows = DTPRINT.AsEnumerable().ToList();
            foreach (DataRow row in allRows)
            {
                float rowH = MeasureRowHeight(g, row, "", pageWidth); // ✅ วัดความสูงจริง

                // ถ้า row นี้จะเกินหน้า
                if (y + rowH > maxY)
                {
                    pageCount++;
                    y = margin + 250f; // header หน้าถัดไป (เล็กกว่าหน้าแรก)
                    maxY = pageHeight - margin - footerHeight - 40;
                }

                y += rowH;
            }

            return pageCount;
        }
        private string NumberToText(decimal amount)
        {
            if (amount == 0) return "ศูนย์บาทถ้วน";
            string[] ones = { "", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า" };
            string[] tens = { "", "สิบ", "ยี่สิบ", "สามสิบ", "สี่สิบ", "ห้าสิบ", "หกสิบ", "เจ็ดสิบ", "แปดสิบ", "เก้าสิบ" };
            long baht = (long)Math.Floor(amount);
            int satang = (int)Math.Round((amount - baht) * 100);
            string result = ConvertGroup(baht, ones, tens) + "บาท";
            result += (satang > 0)  ? ConvertGroup(satang, ones, tens) + "สตางค์" : "ถ้วน";
            return result;
        }
        private string ConvertGroup(long n, string[] ones, string[] tens)
        {
            if (n == 0) return "";
            if (n < 0) return "ลบ" + ConvertGroup(-n, ones, tens);
            string result = "";
            if (n >= 1_000_000_000_000)   // ล้านล้าน
            {
                result += ConvertGroup(n / 1_000_000_000_000, ones, tens) + "ล้านล้าน";
                n %= 1_000_000_000_000;
            }
            if (n >= 1_000_000_000)       // พันล้าน
            {
                result += ConvertGroup(n / 1_000_000_000, ones, tens) + "พันล้าน";
                n %= 1_000_000_000;
            }
            if (n >= 100_000_000)         // ร้อยล้าน
            {
                result += ConvertGroup(n / 100_000_000, ones, tens) + "ร้อยล้าน";
                n %= 100_000_000;
            }
            if (n >= 10_000_000)          // สิบล้าน
            {
                result += ConvertGroup(n / 10_000_000, ones, tens) + "สิบล้าน";
                n %= 10_000_000;
            }
            if (n >= 1_000_000)           // ล้าน
            {
                result += ConvertGroup(n / 1_000_000, ones, tens) + "ล้าน";
                n %= 1_000_000;
            }
            if (n >= 100_000)             // แสน
            {
                result += ones[n / 100_000] + "แสน";
                n %= 100_000;
            }
            if (n >= 10_000)              // หมื่น
            {
                result += ones[n / 10_000] + "หมื่น";
                n %= 10_000;
            }
            if (n >= 1_000)               // พัน
            {
                result += ones[n / 1_000] + "พัน";
                n %= 1_000;
            }
            if (n >= 100)                 // ร้อย
            {
                result += ones[n / 100] + "ร้อย";
                n %= 100;
            }
            if (n >= 10)                  // สิบ
            {
                // กรณีพิเศษ: 10 = "สิบ" ไม่ใช่ "หนึ่งสิบ"
                result += (n / 10 == 1) ? "สิบ" : tens[n / 10];
                n %= 10;
            }
            if (n > 0)                    // หน่วย
            {
                // กรณีพิเศษ: หลักหน่วยของสิบ = "เอ็ด" เช่น 21 = "ยี่สิบเอ็ด"
                bool hasExd = result.Contains("สิบ") && n == 1;
                result += hasExd ? "เอ็ด" : ones[n];
            }
            return result;
        }
        private void FrmCashier_Load(object sender, EventArgs e)
        {
            this.Text = "Last Update 20260409-1 ";
            System.Drawing.Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            scFinishOPD.HeaderHeight = 0;
            scFinishIPD.HeaderHeight = 0;
            ribbonDatePicker1.Value = DateTime.Now;
            if(bc.iniC.hidecomponent.Equals("1"))            {                ribbonDatePicker1.Visible = false;            }
            
            acb = bc.bcDB.pm24DB.getlPaid1(true);
            timeOperList.Start();// Start timer after controls are ready
        }
    }
}