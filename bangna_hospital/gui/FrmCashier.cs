using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using C1.C1Pdf;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using GrapeCity.ActiveReports.Document.Section;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Document = iText.Layout.Document;
using Font = System.Drawing.Font;
using Row = C1.Win.C1FlexGrid.Row;

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
        C1FlexGrid grfOperList, grfFinish, grfInv, grfExp;
        DataTable DTINV = new DataTable();
        Timer timeOperList;
        Label lbLoading;
        TextBox txttaxid, txtName, txtAddr1, txtAddr2;
        String PRENO = "", VSDATE = "", HN = "", DEPTNO = "", DTRCODE = "", DOCGRPID = "", TABVSACTIVE = "", PTTNAME="", VSTIME="", DOCNO="", DOCDATE="";
        Boolean isLoad = false;
        TExpenseDB expenseDB;
        TSupraDB supraDB;
        int colgrfOperListHn = 1, colgrfOperListVn=2, colgrfOperListFullNameT = 3, colgrfOperListSymptoms = 4, colgrfOperListPaidName = 5, colgrfOperListPreno = 6, colgrfOperListVsDate = 7, colgrfOperListVsTime = 8, colgrfOperListActNo = 9, colgrfOperListDtrName = 10, colgrfOperListPha = 11, colgrfOperListLab = 12, colgrfOperListXray = 13;
        int colgrfFinishHn = 1, colgrfFinishFullNameT = 2, colgrfFinishPaidName = 3, colgrfFinishSymptoms = 4, colgrfFinishPreno = 5, colgrfFinishVsDate = 6, colgrfFinishVsTime = 7, colgrfFinishActNo = 8, colgrfFinishDocno = 9, colgrfFinishAN = 10;
        int colgrfExpid=1, colgrfExpDesc=2, colgrfExpAmount=3, colgrfExpModule=4, colgrfExpUser=5, colgrfExpDateReq=6;
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
            isLoad = true;
            theme1 = new C1ThemeController();
            timeOperList = new Timer();
            timeOperList.Interval = Math.Max(1000, bc.timerCheckLabOut * 1000); // enforce >= 1s
            timeOperList.Enabled = false;
            initFont();
            initLoading();
            setEvent(); setTheme();
            initGrfFinish();
            initGrfInv();
            initGrfOperList();
            initGrfExp();
            //timeOperList.Enabled = true;
            //timeOperList.Start();
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
            rgPrn.Click += RgPrn_Click;
            rgSearch.KeyPress += RgSearch_KeyPress;
            rgSearch.KeyUp += RgSearch_KeyUp;
            rgPrn1.Click += RgPrn1_Click;
            rgPrnReceipt.Click += RgPrnReceipt_Click;
            timeOperList.Tick += TimeOperList_Tick;
            btnExpenseSave.Click += BtnExpenseSave_Click;
            txtAmount.KeyPress += TxtAmount_KeyPress;
        }

        private void TxtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void BtnExpenseSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(txtExpenseID.Text.Length <= 0)   {                lfSbMessage.Text = "not id";                   return;                }
            FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
            frm.ShowDialog();
            frm.Dispose();
            if (bc.USERCONFIRMID.Length <= 0)   {                lfSbMessage.Text = "Password ไม่ถูกต้อง";         return;             }
            String re = expenseDB.updateAmountDraw(long.Parse("999"+txtExpenseID.Text.Trim()), txtAmount.Text.Trim().Replace(",",""), bc.USERCONFIRMID);
        }

        private void RgSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (TABVSACTIVE.Equals(tabFinish.Name))
            {
                lfSbMessage.Text = rgSearch.Text;
                grfFinish.ApplySearch(rgSearch.Text.Trim(), true, true, false);
            }
        }

        private void TimeOperList_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfOperList();
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

        private void RgSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            //if (e.KeyChar == (char)Keys.Enter)
            //{
            //    String txt = rgSearch.Text.Trim();
            //    if (txt.Length > 0)
            //    {
            //        if(tabMain.SelectedTab==tabFinish)
            //        {
                        
            //        }
            //    else
            //    {
                    
            //    }
            //}
        }
        private void RgPrn1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printReceipt("copy/สำเนา", bc.iniC.statusPrintPreview.Equals("1") ? "preview":"");
        }
        private void RgPrn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(bc.iniC.branchId.Equals("001"))
                printReceipt("original/ต้นฉบับ", bc.iniC.statusPrintPreview.Equals("1") ? "preview":"");
            else if (bc.iniC.branchId.Equals("005"))
                printReceiptBn5();
        }
        private void printReceiptBn5()
        {
            showLbLoading();
            String compname = "", compaddr1 = "", compaddr2 = "";
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetByHn(HN);
            DataTable dtvs = bc.bcDB.vsDB.selectCompByPreno(HN, VSDATE, PRENO);
            if(dtvs.Rows.Count > 0) { 
                compname = dtvs.Rows[0]["MNC_COM_DSC"] != null ? dtvs.Rows[0]["MNC_COM_PRF"].ToString()+" "+dtvs.Rows[0]["MNC_COM_DSC"].ToString() : "";
                compaddr1 = dtvs.Rows[0]["MNC_COM_ADD"] != null ? dtvs.Rows[0]["MNC_COM_ADD"].ToString() : "";
            }
            AutoCompleteStringCollection acb = new AutoCompleteStringCollection();
            acb = bc.bcDB.labM01DB.getlLabAll();
            Size proposedSize = new Size(100, 100);
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.WindowState = FormWindowState.Normal;
            frm.Size = new Size(800, 600);

            Button btntaxid = new Button();
            btntaxid.Text = "นิติบุคคล";
            Size textSize = TextRenderer.MeasureText(btntaxid.Text, btntaxid.Font, proposedSize, TextFormatFlags.Left);
            btntaxid.Location = new System.Drawing.Point(20, 0);
            btntaxid.Size = new Size(100, 30);
            btntaxid.Font = fEdit;
            btntaxid.Click += Btntaxid_Click;
            txttaxid = new TextBox();
            txttaxid.Name = "txttaxid";
            txttaxid.Font = fEdit;
            txttaxid.Location = btntaxid.Location;
            txttaxid.Left = btntaxid.Width + textSize.Width;
            txttaxid.Width = 300;
            txttaxid.Text = "";

            Label lbName = new Label();
            lbName.Text = "ชื่อบริษัท";
            lbName.Location = new System.Drawing.Point(20, 30);
            lbName.Font = fEdit;
            textSize = TextRenderer.MeasureText(lbName.Text, lbName.Font, proposedSize, TextFormatFlags.Left);
            txtName = new TextBox();
            txtName.Name = "txtName";
            txtName.Font = fEdit;
            txtName.Location = lbName.Location;
            txtName.Left = lbName.Width + textSize.Width+10;
            txtName.Width = 400;
            txtName.Text = compname;
            txtName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtName.AutoCompleteCustomSource = acb;

            Label lbAddr1 = new Label();
            lbAddr1.Text = "ที่อยู่บริษัท1";
            lbAddr1.Location = new System.Drawing.Point(20, 60);
            lbAddr1.Font = fEdit;
            textSize = TextRenderer.MeasureText(lbAddr1.Text, lbName.Font, proposedSize, TextFormatFlags.Left);
            txtAddr1 = new TextBox();
            txtAddr1.Name = "txtAddr1";
            txtAddr1.Font = fEdit;
            txtAddr1.Location = lbAddr1.Location;
            txtAddr1.Left = lbAddr1.Width + textSize.Width;
            txtAddr1.Width = 400;
            txtAddr1.Text = compaddr1;

            Label lbAddr2 = new Label();
            lbAddr2.Text = "ที่อยู่บริษัท2";
            lbAddr2.Location = new System.Drawing.Point(20, 90);
            lbAddr2.Font = fEdit;
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

            frm.Controls.Add(lbName);
            frm.Controls.Add(txtName);
            frm.Controls.Add(lbAddr1);
            frm.Controls.Add(lbAddr2);
            frm.Controls.Add(txtAddr1);
            frm.Controls.Add(txtAddr2);
            frm.Controls.Add(grf);
            frm.Controls.Add(btntaxid);
            frm.Controls.Add(txttaxid);
            theme1.SetTheme(frm, bc.iniC.themeApp);

            frm.ShowDialog(this);
            
            //DataTable dtinv = new DataTable();
            float total = setPrintReceipt("",DTINV);
            genPDFreceipt(DTINV, total);

            frm.Dispose();
            hideLbLoading();
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
                compname = txtName.Text.Trim();
                compaddr1 = "ที่อยู่ " + txtAddr1.Text.Trim();
                compaddr2 = " " + txtAddr2.Text.Trim();
                pdf.DrawString(txtName.Text.Trim() , fPDF, Brushes.Black, new PointF(xCol2-20, 35), _sfLeft);
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
                            pdf.DrawString(txtName.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 - 20, 35 + pageOffset), _sfLeft);
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
                //setGrfOperList();
                hideLbLoading();
                TABVSACTIVE = tabOper.Name;
            }
            else if (tabMain.SelectedTab == tabFinish)
            {
                TABVSACTIVE = tabFinish.Name;
                setLbLoading("กำลังโหลดข้อมูล ...");
                showLbLoading();
                setGrfFinish();
                hideLbLoading();

            }
            else if (tabMain.SelectedTab == tabExpense)
            {
                TABVSACTIVE = tabExpense.Name;
                setLbLoading("กำลังโหลดข้อมูล ...");
                showLbLoading();
                setGrfExp();
                hideLbLoading();
            }
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
        }
        private void setGrfOperList()
        {
            if (isLoad) return;
            if (grfOperList == null || grfOperList.IsDisposed || !grfOperList.IsHandleCreated) return;
            C1.Win.C1FlexGrid.RowCollection rows;
            try         {                rows = grfOperList.Rows;            }
            catch       {                return; /* rows not ready yet */           }

            if (rows == null) return;
            isLoad = true;
            try
            {
                timeOperList.Stop();
                showLbLoading();
                DataTable dtvs = new DataTable();
                String deptno = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
                String vsdate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd");
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
        private void initGrfInv()
        {
            grfInv = new C1FlexGrid();
            grfInv.Font = fEdit;
            grfInv.Dock = System.Windows.Forms.DockStyle.Fill;
            grfInv.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grfInv.Rows.Count = 1;
            grfInv.Cols.Count = 10;
            grfInv.Cols[colgrfFinishHn].DataType = typeof(string);
            grfInv.Cols[colgrfFinishHn].Caption = "MNC_DEF_CD";
            grfInv.Cols[colgrfFinishFullNameT].Caption = "MNC_DEF_DSC";
            grfInv.Cols[colgrfFinishSymptoms].Caption = "MNC_ORD_BY";
            grfInv.Cols[colgrfFinishPaidName].Caption = "MNC_AMT";
            grfInv.Cols[colgrfFinishPreno].Caption = "MNC_DEF_LEV";
            grfInv.Cols[colgrfFinishVsDate].Caption = "MNC_TOT_AMT";
            grfInv.Cols[colgrfFinishVsTime].Caption = "MNC_DIS_AMT";
            grfInv.Cols[colgrfFinishActNo].Caption = "";
            grfInv.Cols[colgrfFinishDocno].Caption = "MNC_DOC_NO";
            grfInv.Cols[colgrfFinishHn].Width = 90;
            grfInv.Cols[colgrfFinishFullNameT].Width = 300;
            grfInv.Cols[colgrfFinishSymptoms].Width = 100;
            grfInv.Cols[colgrfFinishPaidName].Width = 100;
            grfInv.Cols[colgrfFinishPreno].Width = 50;
            grfInv.Cols[colgrfFinishVsDate].Width = 100;
            grfInv.Cols[colgrfFinishVsTime].Width = 70;
            grfInv.Cols[colgrfFinishActNo].Width = 100;
            grfInv.Cols[colgrfFinishDocno].Width = 200;
            grfInv.Cols[colgrfFinishHn].AllowEditing = false;
            grfInv.Cols[colgrfFinishFullNameT].AllowEditing = false;
            grfInv.Cols[colgrfFinishSymptoms].AllowEditing = false;
            grfInv.Cols[colgrfFinishPaidName].AllowEditing = false;
            grfInv.Cols[colgrfFinishPreno].AllowEditing = false;
            grfInv.Cols[colgrfFinishVsDate].AllowEditing = false;
            grfInv.Cols[colgrfFinishVsTime].AllowEditing = false;
            grfInv.Cols[colgrfFinishActNo].AllowEditing = false;
            grfInv.Cols[colgrfFinishDocno].AllowEditing = false;
            grfInv.Cols[colgrfFinishPreno].Visible = false;
            grfInv.Name = "grfInv";
            pnGrfFinishInv.Controls.Add(grfInv);
            theme1.SetTheme(grfInv, bc.iniC.themegrfIpd);
        }
        private void setGrfInv(String docno, String docdate)
        {
            if (isLoad) return;
            setLbLoading("กำลังโหลดข้อมูล ...");
            showLbLoading();
            DTINV = bc.bcDB.tem02DB.SelectByDocno1(docno, docdate);
            //DTINV = bc.bcDB.tem01DB.se(docno, docdate);
            grfInv.Rows.Count = 1; grfInv.Rows.Count = DTINV.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in DTINV.Rows)
            {
                Row rowa = grfInv.Rows[i];
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
        private void initGrfFinish()
        {
            grfFinish = new C1FlexGrid();
            grfFinish.Font = fEdit;
            grfFinish.Dock = System.Windows.Forms.DockStyle.Fill;
            grfFinish.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grfFinish.Rows.Count = 1;
            grfFinish.Cols.Count = 11;
            grfFinish.Cols[colgrfFinishHn].Caption = "HN";
            grfFinish.Cols[colgrfFinishFullNameT].Caption = "Patient Name";
            grfFinish.Cols[colgrfFinishSymptoms].Caption = "อาการ";
            grfFinish.Cols[colgrfFinishPaidName].Caption = "สิทธิ";
            grfFinish.Cols[colgrfFinishPreno].Caption = "preno";
            grfFinish.Cols[colgrfFinishVsDate].Caption = "วันที่";
            grfFinish.Cols[colgrfFinishVsTime].Caption = "เวลา";
            grfFinish.Cols[colgrfFinishActNo].Caption = "Act No";
            grfFinish.Cols[colgrfFinishDocno].Caption = "Dtr Name";
            grfFinish.Cols[colgrfFinishHn].Width = 90;
            grfFinish.Cols[colgrfFinishFullNameT].Width = 200;
            grfFinish.Cols[colgrfFinishSymptoms].Width = 300;
            grfFinish.Cols[colgrfFinishPaidName].Width = 120;
            grfFinish.Cols[colgrfFinishPreno].Width = 50;
            grfFinish.Cols[colgrfFinishVsDate].Width = 100;
            grfFinish.Cols[colgrfFinishVsTime].Width = 70;
            grfFinish.Cols[colgrfFinishActNo].Width = 100;
            grfFinish.Cols[colgrfFinishDocno].Width = 200;
            grfFinish.Cols[colgrfFinishHn].AllowEditing = false;
            grfFinish.Cols[colgrfFinishFullNameT].AllowEditing = false;
            grfFinish.Cols[colgrfFinishSymptoms].AllowEditing = false;
            grfFinish.Cols[colgrfFinishPaidName].AllowEditing = false;
            grfFinish.Cols[colgrfFinishPreno].AllowEditing = false;
            grfFinish.Cols[colgrfFinishVsDate].AllowEditing = false;
            grfFinish.Cols[colgrfFinishVsTime].AllowEditing = false;
            grfFinish.Cols[colgrfFinishActNo].AllowEditing = false;
            grfFinish.Cols[colgrfFinishDocno].AllowEditing = false;
            grfFinish.Cols[colgrfFinishPreno].Visible = false;
            grfFinish.Name = "grfFinish";
            grfFinish.Click += GrfFinish_Click;
            pnGrfFinishView.Controls.Add(grfFinish);
            theme1.SetTheme(grfFinish, bc.iniC.themegrfIpd);
        }
        private void GrfFinish_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfFinish.Row <= 0) return;
            HN = grfFinish[grfFinish.Row, colgrfFinishHn].ToString();
            PRENO = grfFinish[grfFinish.Row, colgrfFinishPreno].ToString();
            PTTNAME = grfFinish[grfFinish.Row, colgrfFinishFullNameT].ToString();
            VSDATE = grfFinish[grfFinish.Row, colgrfFinishVsDate].ToString();
            VSTIME = grfFinish[grfFinish.Row, colgrfFinishVsTime].ToString();
            DOCDATE = grfFinish[grfFinish.Row, colgrfFinishVsDate].ToString();
            DOCNO = grfFinish[grfFinish.Row, colgrfFinishDocno].ToString();
            
            setGrfInv(DOCNO, DOCDATE);
        }
        private void setGrfFinish()
        {
            if (isLoad) return;
            DataTable dtvs = new DataTable();
            
            dtvs = bc.bcDB.fint01DB.SelectPaidOPDByDate(System.DateTime.Now.Year+"-"+DateTime.Now.ToString("MM-dd"));

            grfFinish.Rows.Count = 1; grfFinish.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfFinish.Rows[i];
                rowa[colgrfFinishHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfFinishFullNameT] = row1["pttfullname"].ToString();
                rowa[colgrfFinishSymptoms] = row1["MNC_DIA_DSC"].ToString();
                rowa[colgrfFinishPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[colgrfFinishPreno] = row1["MNC_PRE_NO"].ToString();
                rowa[colgrfFinishVsDate] = row1["MNC_DOC_DAT"].ToString();
                rowa[colgrfFinishVsTime] = row1["MNC_DOC_TIM"].ToString();
                rowa[colgrfFinishDocno] = row1["MNC_DOC_NO"].ToString();
                rowa[colgrfFinishAN] = row1["MNC_AN_NO"].ToString();
                rowa[0] = i.ToString();
                i++;
                if(row1["MNC_AN_NO"].ToString().Length > 1)
                {
                    rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
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
        private void FrmCashier_Load(object sender, EventArgs e)
        {
            this.Text = "Last Update 20251125  ";
            System.Drawing.Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            scFinish.HeaderHeight = 0;
            timeOperList.Start();// Start timer after controls are ready
        }
    }
}
