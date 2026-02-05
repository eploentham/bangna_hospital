using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.C1Pdf;
using C1.C1Zip;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SplitContainer;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace bangna_hospital.gui
{
    public partial class FrmSSO : Form
    {
        BangnaControl bc;
        System.Drawing.Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt2, famt2B, famt4B, famt2BL, famt5, famt5B, famt5BL, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1FlexGrid grfSupra, grfRpt, grfSSOPimp, grfItems, grfSSOP, grfBillI, grfDisp, grfDispI, grfOPDx, grfOpServ, grfMakeTextSearch, grfMakeText;
        C1FlexGrid grfImage;
        Patient PTT;
        C1ThemeController theme1;
        int colgrfSupraHN = 1, colgrfSupraPname = 2, colgrfSupraVsDate = 3, colgrfSupraPreno = 4, colgrfSupraSymptoms = 5, colgrfSupraVNAN=6, colgrfSupraStatusAdmit=7;
        int colgrfSSOPimpchk = 1, colgrfSSOPimpVsDate = 2, colgrfSSOPimpPreno = 3, colgrfSSOPimpHn = 4, colgrfSSOPimpName = 5, colgrfSSOPimpSymptoms = 6, colgrfSSOPimpPaidName = 7, colgrfSSOPimppid = 8, colgrfSSOPimpVsTime = 9;
        int colgrfSSOPhn=1, colgrfSSOPname=2, colgrfSSOPvsdate=3, colgrfSSOPpreno = 4, colgrfSSOPid = 5, colgrfSSOPSymptoms=6;
        int colgrfBillIid=1, colgrfBillIsvdate = 2, colgrfBillIlccode = 3, colgrfBillIdesc = 4;
        int colgrfDispid = 1, colgrfDispprescdt = 2, colgrfDispprescb = 3, colgrfDispitemcnt = 4;
        int colgrfDispIid = 1, colgrfDispIdrgid = 2, colgrfDispIdfscode = 3, colgrfDispIdfstext = 4;
        int colgrfOPServid = 1, colgrfOPServsvid = 2, colgrfOPServcodeset = 3, colgrfOPServtypein = 4;
        int colgrfOPDxid = 1, colgrfOPDxsl = 2, colgrfOPDxcodeset = 3, colgrfOPDxcode = 4;
        int colgrfMakeTextSearchchk = 1,colgrfMakeTextSearchid=2, colgrfMakeTextSearchname = 3, colgrfMakeTextSearchhn = 4, colgrfMakeTextSearchvsdate = 5;
        int colgrfImageid=1, colgrfImageimgpath=2;

        Label lbLoading;
        Boolean pageLoad = false;
        TSupraDB tSupraDB;
        public FrmSSO(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            theme1 = new C1ThemeController();
            initFont();
            initLoading();
            initCombobox();
            initGrf();
            tSupraDB = new TSupraDB(bc.conn);
            tSupraDB.setCboHospital(cboSuprNewHospSupra, "");
            tSupraDB.setCboBranch(cboSuprNewHospHost, "");
            bc.setCboSupraReason(cboSupraNewReason);
            txtSupraNewDateInput.Value = DateTime.Now;
            txtSupraNewDateRefer.Value = DateTime.Now;
            PTT = new Patient();
            initEvent();
            txtSupraNewDtrCode.Text = "";
            txtSupraNewDtrNameT.Text = "";
            txtSuprNewDoc.Text = "";
        }
        private void initEvent()
        {
            txtSupraNewHn.KeyUp += TxtSupraNewHn_KeyUp;
            btnSupraNewSave.Click += BtnSupraNewSave_Click;
            btnSSOPSearch.Click += BtnSSOPSearch_Click;
            btnSSOPimport.Click += BtnSSOPimport_Click;
            tcSSOP.SelectedTabChanged += TcSSOP_SelectedTabChanged;
            btnSSOPSave.Click += BtnSSOPSave_Click;
            btnSSOPProcBillItemsGet.Click += BtnSSOPProcBillItemsGet_Click;
            btnSSOPProcBillItemsSave.Click += BtnSSOPProcBillItemsSave_Click;
            btnSSOPProcDispensingSave.Click += BtnSSOPProcDispensingSave_Click;
            btnSSOPProcDispenseditemSave.Click += BtnSSOPProcDispenseditemSave_Click;
            btnSSOPProcOPServicesSave.Click += BtnSSOPProcOPServicesSave_Click;
            btnSSOPProcOPDxSave.Click += BtnSSOPProcOPDxSave_Click;
            txtSSOPgentextSearch.KeyUp += TxtSSOPgentextSearch_KeyUp;
            btnSSOPgentextgentext.Click += BtnSSOPgentextgentext_Click;
            btnSSOPGen.Click += BtnSSOPGen_Click;
            btnSSOPVoid.Click += BtnSSOPVoid_Click;
            btnSSOPProcBillItemsNew.Click += BtnSSOPProcBillItemsNew_Click;
            btnSSOPProcDispensingNew.Click += BtnSSOPProcDispensingNew_Click;
            btnSSOPProcDispenseditemNew.Click += BtnSSOPProcDispenseditemNew_Click;
            btnSSOPProcOPServicesNew.Click += BtnSSOPProcOPServicesNew_Click;
            btnSSOPProcOPDxNew.Click += BtnSSOPProcOPDxNew_Click;
            btnSSOPItemVoid.Click += BtnSSOPItemVoid_Click;
            btnSSOPDispVoid.Click += BtnSSOPDispVoid_Click;
            btnSSOPDispItemVoid.Click += BtnSSOPDispItemVoid_Click;
            btnSSOPOPserVoid.Click += BtnSSOPOPserVoid_Click;
            btnSSOPOPDxVoid.Click += BtnSSOPOPDxVoid_Click;
            txtSSOPgrfsearch.KeyUp += TxtSSOPgrfsearch_KeyUp;
            txtSBSearchHN.KeyDown += TxtSBSearchHN_KeyDown;
            txtSupraNewDtrCode.KeyUp += TxtSupraNewDtrCode_KeyUp;
            btnSupraNewPrnRefer.Click += BtnSupraNewPrnRefer_Click;
            tabSupraMain.SelectedIndexChanged += TabSupraMain_SelectedIndexChanged;
            btnImageSave.Click += BtnImageSave_Click;
            txtImageExp.KeyPress += TxtImageExp_KeyPress;
            btnImageExpense.Click += BtnImageExpense_Click;
            btnSuprNewPayment.Click += BtnSuprNewPayment_Click;
            txtSupraExpense.KeyPress += TxtSupraExpense_KeyPress;
        }

        private void TxtSupraExpense_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') {                e.Handled = true;            }
        }

        private void BtnSuprNewPayment_Click(object sender, EventArgs e)
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
            saveExpenseSupra("SUPRA", txtSuprNewDoc.Text.Trim()+"/"+ txtSuprNewDocYear.Text.Trim(), txtSupraNewHn.Text.Trim(), lbSupraNewPttName.Text.Trim(), txtSupraExpense.Text.Trim());
        }

        private void BtnImageExpense_Click(object sender, EventArgs e)
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
            saveExpenseSupra("SUPRA_IMAGE", txtImageID.Text.Trim(), txtImageHn.Text.Trim(), lbImagePttNameT.Text.Trim(), txtImageExp.Text.Trim().Replace(",", ""));
        }
        private void saveExpenseSupra(String module, String supraid, String hn, String pttname, String expenseamt)
        {
            //throw new NotImplementedException();
            TExpenseDB expenseDB = new TExpenseDB(bc.conn);
            TExpense expense = new TExpense();
            expense.expense_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
            expense.expense_desc = "ค่าใช้จ่าย SUPRA no " + supraid + " hn " + hn + " " + pttname;
            expense.expense = decimal.Parse(expenseamt);
            expense.module_id = module;
            expense.table_name = "t_supra_image";
            expense.row_id = long.Parse(supraid);
            expense.user_req_id = bc.USERCONFIRMID;
            String re = expenseDB.Insert(expense);
            if (long.TryParse(re, out long chk))
            {
                lfSbMessage.Text = "บันทึกค่าใช้จ่ายเรียบร้อย";
            }
        }
        private void TxtImageExp_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            // Allow digits, control characters (backspace), one decimal point or comma
            char ch = e.KeyChar;
            if (char.IsControl(ch)) return;
            var c1tb = sender as C1.Win.C1Input.C1TextBox;
            string text = c1tb != null ? c1tb.Text : (sender as TextBox)?.Text ?? string.Empty;
            // only allow digits, dot or comma
            if (!char.IsDigit(ch) && ch != '.' && ch != ',')            {                e.Handled = true;                return;            }
            // prevent multiple dots or commas
            if ((ch == '.' || ch == ',') && text.IndexOf(ch) >= 0)      {                e.Handled = true;                return;            }
        }
        private void BtnImageSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            saveImageSupra();
        }
        private void saveImageSupra()
        {
            String path = "", filename = "", ext = "";
            try
            {
                String icd9 = "", icd10 = "", vsdate = "", discdate="", expense="";
                DateTime.TryParse(txtVsDate.Text, out DateTime dvsdate);
                DateTime.TryParse(txtDiscDate.Text, out DateTime ddiscdate);
                icd9 = txtImageICD9_1.Text.Trim();
                expense = txtImageExp.Text.Replace(",","");

                String re = tSupraDB.updateProcess(txtImageID.Text, icd9, "", expense, dvsdate.ToString("yyyy-MM-dd"),ddiscdate.ToString("yyyy-MM-dd"), txtImageHospitalCode.Text.Trim(), txtImageHn.Text.Trim(), "");
                if (re.Equals("1"))
                {
                    lfSbMessage.Text = "save OK";
                    clearControlImageSupra();
                    setGrfImage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "");
            }
        }
        private void clearControlImageSupra()
        {
            txtImagePID.Value = "";
            txtImageID.Value = "";
            txtImageHn.Value = "";
            lbImagePttNameT.Text = "";
            txtImageHospitalCode.Value = "";
            txtImageICD9_3.Value = "";
            txtVsDate.Value = DateTime.Now;
            txtDiscDate.Value = DateTime.Now;
            txtImageICD9_1.Value = "";
            txtImageICD9_2.Value = "";
            txtImageExp.Value = "0.00";
            c1TextBox6.Value = "";
            txtImageHospital.Value = "";
            txtImageHospitalCode.Value = "";
            picBoxSupra.Image = null;
        }
        private void TabSupraMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tabSupraMain.SelectedTab == tabImage)
            {
                setGrfImage();
            }
            else if (tabSupraMain.SelectedTab == tabOper)
            {

            }
        }
        private void BtnSupraNewPrnRefer_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            genCheckUPAlienPDF();
        }
        private void genCheckUPAlienPDF()
        {
            //new LogWriter("d", "FrmOPD genCheckUPAlienPDF ");
            String filename = "";
            int gapLine = 14, linenumber = 5, gapX = 40, gapY = 20, xCol1 = 20, xCol2 = 40, xCol3 = 100, xCol4 = 200, xCol5 = 250, xCol6 = 300, xCol7 = 450, xCol8 = 510;
            String certid = "";
            
            String patheName = Environment.CurrentDirectory + "\\supra\\";
            if (!Directory.Exists(patheName)) { Directory.CreateDirectory(patheName); }
            C1PdfDocument pdf = new C1PdfDocument();
            StringFormat _sfRight, _sfCenter, _sfLeft;
            _sfRight = new StringFormat();
            _sfCenter = new StringFormat();
            _sfLeft = new StringFormat();
            _sfRight.Alignment = StringAlignment.Far;
            _sfCenter.Alignment = StringAlignment.Center;
            _sfLeft.Alignment = StringAlignment.Near;
            pdf.FontType = FontTypeEnum.Embedded;
            pdf.PageSize = new SizeF(PageSize.A4.Width, PageSize.A4.Height);
            //new LogWriter("d", "FrmOPD genCheckUPAlienPDF 01");
            filename = txtSupraNewHn.Text.Trim() + "_supra_" + DateTime.Now.Year.ToString() + ".pdf";
            try
            {
                // Replace the line causing the error with the correct namespace
                System.Drawing.Image loadedImagelogo = Resources.LOGO_BW_tran;
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
                RectangleF recflogo = new RectangleF(15, 5, (int)newWidth, (int)newHeight);
                pdf.DrawImage(loadedImagelogo, recflogo);
                pdf.DrawString(" "+bc.iniC.hostname, fPDF, Brushes.Black, new PointF(recflogo.Width+20, linenumber += (gapLine - 5)), _sfLeft);
                pdf.DrawString(bc.iniC.hostaddresst, fPDF, Brushes.Black, new PointF(recflogo.Width + 20, linenumber += (gapLine)), _sfLeft);
                linenumber += gapLine; linenumber += gapLine; linenumber += gapLine; linenumber += gapLine;
                pdf.DrawString(" ใบรับรองการส่งตัวผู้ป่วยไปตรวจนอกสถานที่", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 80, linenumber += (gapLine - 5)), _sfLeft);
                pdf.DrawString("เลขที่ "+ txtSuprNewDoc.Text.Trim()+"/"+ txtSuprNewDocYear.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2+45, 50), _sfRight);
                pdf.DrawString("วันที่ "+DateTime.Now.ToString("dd MMM yyyy", new CultureInfo("th-TH")), fPDF, Brushes.Black, new PointF(xCol8, linenumber += (gapLine)), _sfRight);
                linenumber += gapLine;
                pdf.DrawString("รพ./สถานที่รับ", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine -10)), _sfLeft);
                linenumber += (gapLine-5);
                pdf.DrawString("ผู้ส่ง .....................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(" " + cboSuprNewHospHost.Text, fPDFl2, Brushes.Black, new PointF(xCol2 + 25, linenumber - 8), _sfLeft);
                pdf.DrawString("ผู้รับ .....................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(" " + cboSuprNewHospSupra.Text, fPDFl2, Brushes.Black, new PointF(xCol2 + 25, linenumber - 7), _sfLeft);
                linenumber += gapLine;
                pdf.DrawString("ขอส่งตัวผู้ป่วย", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                linenumber += (gapLine - 5);
                pdf.DrawString("ชื่อ.............................................................................................................................................เพศ..........................อายุ.....................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(PTT.Name+" ["+ PTT.MNC_ID_NO.Trim()+"]", fPDF, Brushes.Black, new PointF(xCol2 + 20, linenumber-3), _sfLeft);
                pdf.DrawString(lbSupraPttAge.Text, fPDF, Brushes.Black, new PointF(xCol2 + 360, linenumber - 3), _sfLeft);
                pdf.DrawString(PTT.MNC_SEX, fPDF, Brushes.Black, new PointF(xCol2 + 440, linenumber - 3), _sfLeft);
                pdf.DrawString("โรค 1. ..................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtSupraDiag1.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2+30, linenumber - 3), _sfLeft);
                pdf.DrawString("2. ..........................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtSupraDiag2.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 20, linenumber - 3), _sfLeft);
                pdf.DrawString("3. ..........................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtSupraDiag3.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 20, linenumber - 3), _sfLeft);
                pdf.DrawString("4. ..........................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtSupraDiag4.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 20, linenumber - 3), _sfLeft);
                pdf.DrawString("แพทย์ผู้ส่ง ...................................................................................................................................................................................", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(txtSupraNewDtrNameT.Text.Trim() + " [" + txtSupraNewDtrCode.Text.Trim()+"] ", fPDF, Brushes.Black, new PointF(xCol2 + 50, linenumber-3), _sfLeft);
                pdf.DrawString("เหตุผลในการส่งตัว ......................................................................................................................................................................", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(cboSupraNewReason.Text.Trim(), fPDFl2, Brushes.Black, new PointF(xCol2 + 90, linenumber - 3), _sfLeft);
                pdf.DrawString("วันที่ส่งตัว ....................................................................................................................................................................................", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString(((DateTime)(txtSupraNewDateRefer.Value)).ToString("dd MMM yyyy", new CultureInfo("th-TH")), fPDFl2, Brushes.Black, new PointF(xCol2 + 90, linenumber - 3), _sfLeft);
                linenumber += (gapLine + 5);
                pdf.DrawString("ข้อกำหนดและความต้องการของผู้ส่ง ", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString("เคสReferด่วน แผนกAdmission เป็นผู้ออกหนังสือส่งตัว ", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString("กรณีผู้ป่วยใน ผู้รับโปรดแจ้งผู้ส่งเป็นประจำทุกวัน ", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString("หมายเหตุ:  กรณีผู้ป่วยใช้สิทธิประกันสังคมทางโรงพยาบาล จะรับผิดชอบค่ารักษาตามสิทธิ ยกเว้นยานอกบัญชียาหลัก ", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                linenumber += (gapLine + 5);
                linenumber += (gapLine + 5);
                pdf.DrawString("ลงชื่อ ................................................................", fPDF, Brushes.Black, new PointF(xCol7-100, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString("(นายแพทย์อรรถสิทธ์  ทองปลาเค้า) ", fPDF, Brushes.Black, new PointF(xCol7, linenumber += (gapLine + 5)), _sfCenter);
                pdf.DrawString("ผู้อำนวยการโรงพยาบาลบางนา5 ", fPDF, Brushes.Black, new PointF(xCol7, linenumber += (gapLine + 5)), _sfCenter);
                linenumber += (gapLine + 5);
                pdf.DrawString("สำหรับผู้ประสานงานติดต่อ ", fPDF, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                //pdf.DrawString("ผู้ส่ง .......................................................", fPDF, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString("ชื่อ-นามสกุล "+ txtSupraNewSupraContactName.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                pdf.DrawString("ตำแหน่ง  เจ้าหน้าที่ประกันสังคม แผนก ประกันสังคม ", fPDF, Brushes.Black, new PointF(xCol4, linenumber), _sfLeft);

                pdf.Save(patheName + filename);
            }
            catch (Exception ex)
            {
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
        private void TxtSupraNewDtrCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode != Keys.Enter) return;
            string code = txtSupraNewDtrCode.Text?.Trim();
            if (string.IsNullOrEmpty(code)) return;

            try
            {
                txtSupraNewDtrNameT.Text = bc.selectDoctorName(txtSupraNewDtrCode.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "");
            }
        }

        private void TxtSBSearchHN_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if((e.KeyCode == Keys.Enter)&& txtSBSearchHN.Text.Length>3)
            {
                setGrfSupra(txtSBSearchHN.Text.Trim());
            }
        }

        private void initGrf()
        {
            //throw new NotImplementedException();
            initGrfSupra();             initGrfSSOP();              initGrfSSOPimp();               initGrfItems();
            initGrfBillItems();         initGrfDisp();              initGrfDispItems();             initGrfOpServ();
            initGrfOPDx();              initGrfMakeTextSearch();    initGrfMakeText();              initGrfImage();
        }
        private void initCombobox()
        {
            bc.setCboSSOPCompletion(cboSSOPOpServicescompletion, "");
            bc.setCboSSOPTFlag(cboSSOPProcTFlag, "");
            bc.setCboSSOPPayPlan(cboSSOPProcPayPlan, "");
            bc.setCboSSOPOtherPayPlan(cboSSOPProcOPayPlan, "");
            bc.setCboSSOPBillmuad(cboSSOPBillItemsbillmuad, "");
            bc.setCboSSOPClaimCat(cboSSOPBillItemsClaimcat, "");
            bc.setCboSSOPBenefitplan(cboSSOPDispBenefitplan, "");
            bc.setCboSSOPDispeStat(cboSSOPDispensingdispe, "");
            bc.setCboSSOPPrdCat(cboSSOPDispenseditemprdcat, "");
            bc.setCboSSOPClaimcont(cboSSOPDispenseditemclaimcont, "");
            bc.setCboSSOPClaimCat(cboSSOPDispenseditemclaimcat, "");
            bc.setCboSSOPPrdSeCode(cboSSOPDispenseditemprdsecode, "");
            bc.setCboSSOPClass(cboSSOPOPServicesclass, "");
            bc.setCboSSOPCareAccount(cboSSOPOpServicescareaccount, "");
            bc.setCboSSOPTypeIn(cboSSOPOpServicestypein, "");
            bc.setCboSSOPTypeOut(cboSSOPOpServicestypeout, "");
            bc.setCboSSOPCodeSet(cboSSOPOPServicescodeset, "");
            bc.setCboSSOPCompletion(cboSSOPOpServicescompletion, "");
            bc.setCboSSOPTypeServ(cboSSOPOpServicestypeserv, "");
            bc.setCboSSOPClinic(cboSSOPOPServicesclinic, "");
            bc.setCboSSOPClaimCat(cboSSOPOpServicesclaimcat, "");
            bc.setCboSSOPClass(cboSSOPOPDxclass, "");
            bc.setCboSSOPOPDxCodeSet(cboSSOPOPDxcodeset, "");
            bc.setCboSSOPSL(cboSSOPOPDxsl, "");
            bc.bcDB.pttDB.setCboDeptOPD(cboSSOPProcStation, bc.iniC.station);
            bc.setCboPaid(cboSupraPaid, "");
        }
        private void TxtSSOPgrfsearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(txtSSOPgrfsearch.Text.Length> 2)
            {
                grfSSOPimp.ApplySearch(txtSSOPgrfsearch.Text.Trim(), true, true, false);
            }
        }
        private void BtnSSOPOPDxVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.sSOPODDB.voidOPDx(txtSSOPOPDxid.Text.Trim());
        }

        private void BtnSSOPOPserVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.sSOPOSDB.voidOPService(txtSSOPOpServicesid.Text.Trim());
        }

        private void BtnSSOPDispItemVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.sSOPDIDB.voidDispensedItem(txtSSOPDispenseditemid.Text.Trim());
        }

        private void BtnSSOPDispVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.sSOPDDB.voidDispensing(txtSSOPDispID.Text.Trim());
        }

        private void BtnSSOPItemVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.ssopBillItemsDB.voidBillItem(txtSSOPBillItemsID.Text.Trim());
        }

        private void BtnSSOPProcOPDxNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlOPDx();
        }

        private void BtnSSOPProcOPServicesNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlOpServ();
        }

        private void BtnSSOPProcDispenseditemNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlDispI();
        }

        private void BtnSSOPProcDispensingNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlDispsing();
        }

        private void BtnSSOPProcBillItemsNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControlBillItems();
        }

        private void BtnSSOPVoid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.sSOPBillTranDB.updateStatusVoid(txtSSOPBilltransID.Text.Trim());
            setGrfSSOP();
        }

        private void BtnSSOPGen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String re = "";
            re = bc.bcDB.sSOPBillTranDB.updateStatusProcessMakeText(txtSSOPBilltransID.Text.Trim());
            foreach(Row rowa in grfSSOP.Rows)
            {
                if (rowa[colgrfSSOPid] != null && rowa[colgrfSSOPid].ToString().Equals(txtSSOPBilltransID.Text.Trim()))
                {
                    rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
                }
            }
        }

        private void BtnSSOPgentextgentext_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            genSSOPXML();
        }
        private String genSSOPXML()
        {
            List<SSOPBillTran> lbillt = new List<SSOPBillTran>();
            List<SSOPBillItems> lbillitems = new List<SSOPBillItems>();
            List<SSOPDispensing> ldisp = new List<SSOPDispensing>();
            List<SSOPDispenseditem> ldispitems = new List<SSOPDispenseditem>();
            List<SSOPOpservices> lopserv = new List<SSOPOpservices>();
            List<SSOPOpdx> lopdx = new List<SSOPOpdx>();
            String hcode =bc.iniC.ssoid,Hmain = "", sessionNo = "999999", pathFile = "";
            if (grfMakeText.Rows.Count > 0)
            {
                foreach (Row rowa in grfMakeText.Rows)
                {
                    if (rowa[colgrfMakeTextSearchchk] != null && rowa[colgrfMakeTextSearchchk].ToString().Equals("True"))
                    {
                        String id = rowa[colgrfMakeTextSearchid] != null ? rowa[colgrfMakeTextSearchid].ToString() : "";
                        SSOPBillTran billt = bc.bcDB.sSOPBillTranDB.selectByPk(id);
                        if (billt != null) { lbillt.Add(billt); }
                        List<SSOPBillItems> litems = bc.bcDB.ssopBillItemsDB.selectByBTransId(id);
                        if(litems != null && litems.Count > 0)
                        {
                            foreach (SSOPBillItems item in litems)                              {                                lbillitems.Add(item);              }
                        }
                        List<SSOPDispensing> ldisp1 = bc.bcDB.sSOPDDB.selectByBTransId(id);
                        if (ldisp1 != null && ldisp1.Count > 0)
                        {
                            foreach (SSOPDispensing item in ldisp1)                             {                                ldisp.Add(item);                   }
                        }
                        List<SSOPDispenseditem> ldispitems1 = bc.bcDB.sSOPDIDB.selectByBTransId(id);
                        if (ldispitems1 != null && ldispitems1.Count > 0)
                        {
                            foreach (SSOPDispenseditem item in ldispitems1)                     {                                ldispitems.Add(item);              }
                        }
                        List<SSOPOpservices> lopserv1 = bc.bcDB.sSOPOSDB.selectByBTransId(id);
                        if (lopserv1 != null && lopserv1.Count > 0)
                        {
                            foreach (SSOPOpservices item in lopserv1)                           {                                lopserv.Add(item);                 }
                        }
                        List<SSOPOpdx> lopdx1 = bc.bcDB.sSOPODDB.selectByBTransId(id);
                        if (lopdx1 != null && lopdx1.Count > 0)
                        {
                            foreach (SSOPOpdx item in lopdx1)                                   {                                lopdx.Add(item);                   }
                        }
                    }
                }
            }
            if (lbillt.Count <= 0)            {                MessageBox.Show("ไม่พบข้อมูล ", "");                return "";            }
            Hmain = bc.iniC.ssoid;
            sessionNo = "0000"+bc.bcDB.sSOPBillTranDB.selectMaxSessionID();
            sessionNo = sessionNo.Substring(sessionNo.Length - 4);
            pathFile = bc.iniC.ssopXmlPath + "\\" + sessionNo;
            txtSSOPgentextSessionid.Value = sessionNo;
            txtSSOPgentextHname.Value = bc.iniC.hostname;
            txtSSOPgentextHCODE.Value = hcode;
            if (!Directory.Exists(pathFile))            {                Directory.CreateDirectory(pathFile);            }
            String date1 = DateTime.Now.Year.ToString()+ DateTime.Now.ToString("MMdd");
            String senddate = DateTime.Now.Year.ToString() + DateTime.Now.ToString("-MM-dd")+"T" + DateTime.Now.ToString("HH:mm:ss");
            String filenamebillt = pathFile + "\\" + "BILLTRAN" + date1 + ".txt";
            String filenamedisp = pathFile + "\\" + "BILLDISP" + date1 + ".txt";
            String filenameopserv = pathFile + "\\" + "OPServices" + date1 + ".txt";
            SSOPxmlFile ssopxmlFile = new SSOPxmlFile();
            ssopxmlFile.genBillTrans(hcode, bc.iniC.hostname, senddate, sessionNo, lbillt, lbillitems, filenamebillt);
            ssopxmlFile.genDispensing(hcode, bc.iniC.hostname, senddate, sessionNo, ldisp, ldispitems, filenamedisp);
            ssopxmlFile.genOpService(hcode, bc.iniC.hostname, senddate, sessionNo, lopserv, lopdx, filenameopserv);
            foreach(SSOPBillTran billtran in lbillt)
            {
                String re = bc.bcDB.sSOPBillTranDB.updateStatusProcessMakeTextSend(billtran.billtran_id);
            }
            try
            {
                String fileZipName = "";
                C1ZipFile zip = new C1ZipFile(); //iniC.ssoid
                //fileZipName = Hmain+"AIPN"+ sessionNo;
                senddate = senddate.Replace("-", "").Replace(":", "");
                senddate = senddate.Replace("T", "-");
                fileZipName = bc.iniC.ssoid + "_SSOPBIL_" + sessionNo+"_01_"+ senddate;
                zip.Create(pathFile + "\\" + fileZipName + ".zip");
                foreach (String filename in Directory.GetFiles(pathFile))
                {
                    if (filename.IndexOf("-data") > 0)                  {                        continue;                    }
                    if (filename.IndexOf("-utf8") > 0)                  {                        continue;                    }
                    if (filename.IndexOf(".zip") > 0)                   {                        continue;                    }
                    if (filename.IndexOf("_temp.txt") > 0) { continue; }
                    zip.Entries.Add(filename);
                }
                zip.Close();
                Process.Start("explorer.exe", pathFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
            }
            return sessionNo;
        }
        private void TxtSSOPgentextSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //setGrfSSOP(txtSSOPgentextSearch.Text.Trim());
                setGrfMakeTextSearch(txtSSOPgentextSearch.Text.Trim());
            }
        }

        private void BtnSSOPProcOPDxSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SSOPOpdx sSOPOpdx = setSSOPOPDx();
            String re = bc.bcDB.sSOPODDB.insertData(sSOPOpdx);
        }
        private SSOPOpdx setSSOPOPDx()
        {
            SSOPOpdx sSOPOpdx = new SSOPOpdx();
            sSOPOpdx.opdx_id = txtSSOPOPDxid.Text.Trim();
            sSOPOpdx.class1 = cboSSOPOPDxclass.SelectedItem != null ? ((ComboBoxItem)cboSSOPOPDxclass.SelectedItem).Value : "";
            sSOPOpdx.svid = txtSSOPOPDxsvid.Text.Trim();
            sSOPOpdx.sl = cboSSOPOPDxsl.SelectedItem != null ? ((ComboBoxItem)cboSSOPOPDxsl.SelectedItem).Value : "";
            sSOPOpdx.codeset = cboSSOPOPDxcodeset.SelectedItem != null ? ((ComboBoxItem)cboSSOPOPDxcodeset.SelectedItem).Value : "";
            sSOPOpdx.code = txtSSOPOPDxcode.Text.Trim();
            sSOPOpdx.desc1 = txtSSOPOPDxdesc1.Text.Trim();
            sSOPOpdx.billtrans_id = "";
            sSOPOpdx.active = "1";
            return sSOPOpdx;
        }
        private void BtnSSOPProcOPServicesSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SSOPOpservices sSOPOpservices = setSSOPOpServices();
            String re = bc.bcDB.sSOPOSDB.insertData(sSOPOpservices);
        }
        private SSOPOpservices setSSOPOpServices()
        {
            SSOPOpservices sSOPOpservices = new SSOPOpservices();
            sSOPOpservices.opservices_id = txtSSOPOpServicesid.Text.Trim();
            sSOPOpservices.billtrans_id = "";
            sSOPOpservices.svid = txtSSOPOpServicessvid.Text.Trim();
            sSOPOpservices.class1 = cboSSOPOPServicesclass.SelectedItem != null ? ((ComboBoxItem)cboSSOPOPServicesclass.SelectedItem).Value : "";
            sSOPOpservices.invno = txtSSOPOpServicesinvno.Text.Trim();
            sSOPOpservices.hcode = "";
            sSOPOpservices.hn = txtSSOPProcHN.Text.Trim();
            sSOPOpservices.pid = txtSSOPProcPID.Text.Trim();
            sSOPOpservices.careaccount = cboSSOPOpServicescareaccount.SelectedItem != null ? ((ComboBoxItem)cboSSOPOpServicescareaccount.SelectedItem).Value : "";
            sSOPOpservices.typeserv = cboSSOPOpServicestypeserv.SelectedItem != null ? ((ComboBoxItem)cboSSOPOpServicestypeserv.SelectedItem).Value : "";
            sSOPOpservices.typein = cboSSOPOpServicestypein.SelectedItem != null ? ((ComboBoxItem)cboSSOPOpServicestypein.SelectedItem).Value : "";
            sSOPOpservices.typeout = cboSSOPOpServicestypeout.SelectedItem != null ? ((ComboBoxItem)cboSSOPOpServicestypeout.SelectedItem).Value : "";
            sSOPOpservices.dtappoint = bc.datetoDBCultureInfo(txtSSOPOPServicesdtappoint.Text.Trim());
            sSOPOpservices.svpid = txtSSOPOPServicessvpid.Text.Trim();
            sSOPOpservices.clinic = cboSSOPOPServicesclinic.SelectedItem != null ? ((ComboBoxItem)cboSSOPOPServicesclinic.SelectedItem).Value : "";
            sSOPOpservices.begdt = bc.datetoDBCultureInfo(txtSSOPOPServicesbegdt.Text.Trim());
            sSOPOpservices.enddt = bc.datetoDBCultureInfo(txtSSOPOPServicesenddt.Text.Trim());
            sSOPOpservices.lccode = txtSSOPOpServiceslccode.Text.Trim();
            sSOPOpservices.codeset = cboSSOPOPServicescodeset.SelectedItem != null ? ((ComboBoxItem)cboSSOPOPServicescodeset.SelectedItem).Value : "";
            sSOPOpservices.stdcode = txtSSOPOPServicestdcode.Text.Trim();
            sSOPOpservices.svcharge = txtSSOPOPServicessvcharge.Text.Trim();
            sSOPOpservices.completion = cboSSOPOpServicescompletion.SelectedItem != null ? ((ComboBoxItem)cboSSOPOpServicescompletion.SelectedItem).Value : "";
            sSOPOpservices.svtxcode = txtSSOPOpServicessvtxcode.Text.Trim();
            sSOPOpservices.claimcat = cboSSOPOpServicesclaimcat.SelectedItem != null ? ((ComboBoxItem)cboSSOPOpServicesclaimcat.SelectedItem).Value : "";
            return sSOPOpservices;
        }
        private void BtnSSOPProcDispenseditemSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SSOPDispenseditem sSOPDispenseditem = setSSOPDispenseditem();
            String re = bc.bcDB.sSOPDIDB.insertData(sSOPDispenseditem);
        }
        private SSOPDispenseditem setSSOPDispenseditem()
        {
            SSOPDispenseditem dispitem = new SSOPDispenseditem();
            dispitem.dispenseditem_id = txtSSOPDispenseditemid.Text.Trim();             dispitem.billtrans_id = "";                                         dispitem.dispid = "";
            dispitem.prdcat = cboSSOPDispenseditemprdcat.SelectedItem != null ? ((ComboBoxItem)cboSSOPDispenseditemprdcat.SelectedItem).Value : "";
            dispitem.hospdrgid = txtSSOPDispenseditemhospdrgid.Text.Trim();             dispitem.drgid = txtSSOPDispenseditemdrgid.Text.Trim();             dispitem.dfscode = txtSSOPDispenseditemdfscode.Text.Trim();
            dispitem.dfstext = txtSSOPDispenseditemdfstext.Text.Trim();                 dispitem.packsize = txtSSOPDispenseditempacksize.Text.Trim();       dispitem.sigcode = txtSSOPDispenseditemsigcode.Text.Trim();
            dispitem.sigtext = txtSSOPDispenseditemsigtext.Text.Trim();                 dispitem.quantity = txtSSOPDispenseditemquantity.Text.Trim();       dispitem.unitprice = txtSSOPDispenseditemunitprice.Text.Trim();
            dispitem.chargeamt = txtSSOPDispenseditemchargeamt.Text.Trim();             dispitem.reimbprice = txtSSOPDispenseditemreimbprice.Text.Trim();    dispitem.reimbamt = txtSSOPDispenseditemreimbamt.Text.Trim();
            dispitem.prdsecode = cboSSOPDispenseditemprdsecode.SelectedItem != null ? ((ComboBoxItem)cboSSOPDispenseditemprdsecode.SelectedItem).Value : "";
            dispitem.claimcont = cboSSOPDispenseditemclaimcont.SelectedItem != null ? ((ComboBoxItem)cboSSOPDispenseditemclaimcont.SelectedItem).Value : "";
            dispitem.claimcat = cboSSOPDispenseditemclaimcat.SelectedItem != null ? ((ComboBoxItem)cboSSOPDispenseditemclaimcat.SelectedItem).Value : "";
            dispitem.multidisp = txtSSOPDispenseditemmultidisp.Text.Trim();             dispitem.supplyfor = txtSSOPDispenseditemsupplyfor.Text.Trim();
            return dispitem;
        }
        private void BtnSSOPProcDispensingSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SSOPDispensing sSOPDispensing = setSSOPDispensing();
            String re = bc.bcDB.sSOPDDB.insertData(sSOPDispensing);
        }
        private SSOPDispensing setSSOPDispensing()
        {
            SSOPDispensing disp = new SSOPDispensing();
            disp.billdisp_id = txtSSOPDispID.Text.Trim();                                   disp.billtrans_id = "";                                     disp.hn = txtSSOPProcHN.Text.Trim();
            disp.pid = txtSSOPProcPID.Text.Trim();                                          disp.invno = txtSSOPDispensinginvno.Text.Trim();            disp.prescdt = bc.datetoDBCultureInfo(txtSSOPDispPrescdt.Text.Trim());
            disp.dispdt = bc.datetoDBCultureInfo(txtSSOPDispDispdt.Text.Trim());            disp.prescb = txtSSOPDispPrescb.Text.Trim();                disp.itemcnt = txtSSOPDispItemcnt.Text.Trim();
            disp.chargeamt = txtSSOPDispChargeamt.Text.Trim();                              disp.claimamt = txtSSOPDispClaimamt.Text.Trim();            disp.paid = txtSSOPDispPaid.Text.Trim();
            disp.otherpay = txtSSOPDispOtherpay.Text.Trim();
            disp.reimburser = txtSSOPDispReimburser.Text.Trim();
            disp.benefitplan = cboSSOPDispBenefitplan.SelectedItem != null ? ((ComboBoxItem)cboSSOPDispBenefitplan.SelectedItem).Value : "";
            disp.dispestat = cboSSOPDispensingdispe.SelectedItem != null ? ((ComboBoxItem)cboSSOPDispensingdispe.SelectedItem).Value : "";
            disp.svid = txtSSOPDispensingsvid.Text.Trim();                                  disp.daycover = txtSSOPDispensingdaycover.Text.Trim();      disp.dispid = txtSSOPDispensingdispid.Text.Trim();
            disp.providerid = txtSSOPDispensingproiderid.Text.Trim();

            return disp;
        }
        private void BtnSSOPProcBillItemsSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SSOPBillItems sSOPBillItems = setSSOPBillItems();
            String re = bc.bcDB.ssopBillItemsDB.insertData(sSOPBillItems);
        }
        private SSOPBillItems setSSOPBillItems()
        {
            SSOPBillItems sSOPBillItems = new SSOPBillItems();
            sSOPBillItems.billitems_id = txtSSOPBillItemsID.Text.Trim();
            sSOPBillItems.billtrans_id = "";
            sSOPBillItems.billmuad = cboSSOPBillItemsbillmuad.SelectedItem != null ? ((ComboBoxItem)cboSSOPBillItemsbillmuad.SelectedItem).Value : "";
            sSOPBillItems.claimcat = cboSSOPBillItemsClaimcat.SelectedItem != null ? ((ComboBoxItem)cboSSOPBillItemsClaimcat.SelectedItem).Value : "";
            sSOPBillItems.invno = txtSSOPBillItemsInvno.Text.Trim();
            sSOPBillItems.svdate = bc.datetoDBCultureInfo(txtSSOPBillItemssvdate.Text.Trim());
            sSOPBillItems.lccode = txtSSOPBillItemsLccode.Text.Trim();
            sSOPBillItems.stdcode = txtSSOPBillItemsstdcode.Text.Trim();
            sSOPBillItems.desc1 = txtSSOPBillItemsDesc1.Text.Trim();
            sSOPBillItems.qty = txtSSOPBillItemsQty.Text.Trim();
            sSOPBillItems.up1 = txtSSOPBillItemsUp1.Text.Trim();
            sSOPBillItems.chargeamt = txtSSOPBillItemsChargeamt.Text.Trim();
            sSOPBillItems.claimup = txtSSOPBillItemsClaimup.Text.Trim();
            sSOPBillItems.claimamount = txtSSOPBillItemsClaimamount.Text.Trim();
            sSOPBillItems.svrefid = txtSSOPBillItemssvrefid.Text.Trim();
            return sSOPBillItems;
        }
        private void BtnSSOPProcBillItemsGet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void BtnSSOPSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SSOPBillTran sSOPBillTran = setSSOPprocess();
            String re = bc.bcDB.sSOPBillTranDB.insertData(sSOPBillTran);
            if (int.Parse(re) > 0)            {                lfSbMessage.Text = "update ok";            }
            else            {                MessageBox.Show("insert error", "");            }
        }

        private void TcSSOP_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tcSSOP.SelectedTab == tabSSOPprocess)            {                setGrfSSOP();            }
            else if(tcSSOP.SelectedTab == tabGenText) { setGrfMakeText(); }
            else { }
        }
        private void BtnSSOPimport_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            showLbLoading();
            foreach(Row arow in grfItems.Rows)
            {
                String hn = arow[colgrfSSOPimpHn] != null ? arow[colgrfSSOPimpHn].ToString() : "";
                String preno = arow[colgrfSSOPimpPreno] != null ? arow[colgrfSSOPimpPreno].ToString() : "";
                String vsdate = arow[colgrfSSOPimpVsDate] != null ? arow[colgrfSSOPimpVsDate].ToString() : "";
                String name = arow[colgrfSSOPimpName] != null ? arow[colgrfSSOPimpName].ToString() : "";
                String pid = arow[colgrfSSOPimppid] != null ? arow[colgrfSSOPimppid].ToString() : "";
                String vstime = arow[colgrfSSOPimpVsTime] != null ? arow[colgrfSSOPimpVsTime].ToString() : "";
                if (int.TryParse(hn, out int i))
                {
                    DataTable dt = bc.bcDB.fint01DB.SelectByPreno(hn, vsdate, preno);
                    DataTable dtbillitems = bc.bcDB.fint01DB.SelectBillGroupSS1Bypreno(hn, vsdate, preno);
                    if(dtbillitems.Rows.Count <= 0){            MessageBox.Show("ไม่พบรายการ", "");              return;             }
                    String invno = "", amount="0", paid="0", secno="", docno="", docdate="", paidcode="";
                    if (dt.Rows.Count > 0)
                    {
                        String no = "000000"+ dt.Rows[0]["MNC_DOC_NO"].ToString();
                        no = no.Substring(no.Length - 6, 6);
                        String yr = "0000"+dt.Rows[0]["MNC_DOC_YR"].ToString();
                        yr = yr.Substring(2, 2);
                        invno = yr + no;//MNC_SUM_PRI
                        amount = dt.Rows[0]["MNC_SUM_PRI"].ToString();          paid = dt.Rows[0]["MNC_PAY_CASH"].ToString();       secno = dt.Rows[0]["MNC_SEC_NO"].ToString();
                        docno = dt.Rows[0]["MNC_DOC_NO"].ToString();            docdate = dt.Rows[0]["MNC_DOC_DAT"].ToString();
                        paidcode = dt.Rows[0]["MNC_FN_TYP_CD"].ToString();
                    }
                    secno = "147";
                    paid = "0.00";
                    
                    SSOPBillTran ssopbilltran = setSSOPBillTran(secno, vsdate+"T"+ vstime, invno, "", hn, preno, amount, paid
                        , pid, name, paidcode, "80", paid, "");
                    //setGrfSSOP(hn, preno, vsdate);
                    String rebillt = bc.bcDB.sSOPBillTranDB.insertData(ssopbilltran);
                    if (long.Parse(rebillt) > 0)
                    {
                        String svid = "", class1="EC";
                        // insert bill items
                        foreach (DataRow rowb in dtbillitems.Rows)
                        {
                            String ss1 = "00"+rowb["mnc_grp_ss1"].ToString();               ss1 = ss1.Substring(ss1.Length - 2, 2);
                            String svid1 = "000"+rowb["MNC_FN_NO"].ToString();              svid1 = svid1.Substring(svid1.Length - 3, 3);
                            String svid2 = "000" + rowb["MNC_FN_CD"].ToString();            svid2 = svid2.Substring(svid2.Length - 3, 3);
                            svid = ss1 + svid2 + svid1;
                            if (ss1.Equals("04"))         //drug  เอามาเป็นรายการสั่งยา
                            {
                                String reqdrugold = "", otherpay="0.00", reimburser="HP";
                                float amt = 0;
                                DataTable dtdrug = bc.bcDB.pharT06DB.selectBypreno(hn, preno, vsdate);
                                if (dtdrug.Rows.Count > 0)
                                {
                                    foreach (DataRow rowd in dtdrug.Rows)
                                    {
                                        SSOPBillItems p = new SSOPBillItems();
                                        p.billtrans_id = rebillt;                                   p.billitems_id = "";                            p.billmuad = ss1;
                                        p.claimcat = "OP1";                                         p.invno = invno;                                p.svdate = rowd["MNC_CFR_DAT"].ToString();
                                        p.lccode = rowd["MNC_PH_CD"].ToString();                    p.stdcode = "";                                 p.desc1 = rowd["MNC_PH_TN"].ToString();
                                        p.qty = rowd["MNC_PH_QTY_PAID"].ToString();
                                        p.up1 = rowd["MNC_PH_PRI"].ToString();
                                        p.chargeamt = rowd["MNC_PH_PRI"].ToString();
                                        p.claimup = rowd["MNC_PH_PRI"].ToString();
                                        p.claimamount = rowd["MNC_PH_PRI"].ToString();
                                        p.svrefid = invno;
                                        p.active = "1";
                                        p.billmuad = "3";
                                        String re1 = bc.bcDB.ssopBillItemsDB.insertData(p);
                                        String reqdrug = rowd["MNC_CFR_NO"].ToString() + "." + rowd["MNC_CFR_YR"].ToString();
                                        if(!reqdrugold.Equals(reqdrug))
                                        {
                                            reqdrugold = reqdrug;
                                            float.TryParse(rowd["MNC_PH_PRI"].ToString(), out float price);
                                            amt += price;
                                            SSOPDispensing disp = new SSOPDispensing();
                                            disp.dispid = reqdrug;                              disp.billdisp_id = "";                                  disp.billtrans_id = rebillt;
                                            disp.hn = hn;                                       disp.pid = pid;                                         disp.invno = invno;
                                            disp.prescdt = rowd["MNC_CFR_DAT"].ToString();      disp.dispdt = rowd["MNC_CFR_DAT"].ToString();           disp.prescb = "ว"+rowd["MNC_DOT_CD"].ToString();
                                            disp.itemcnt = dtdrug.Rows.Count.ToString();        disp.chargeamt = amt.ToString("#,###.00");              disp.claimamt = amt.ToString("#,###.00");
                                            disp.paid = amt.ToString("#,###.00");               disp.otherpay = otherpay;                               disp.reimburser = reimburser;
                                            disp.benefitplan = "";                              disp.dispestat = "1";                                   disp.svid = svid;
                                            disp.daycover = "7D";                               disp.providerid = bc.iniC.ssoid;
                                            String re3 = bc.bcDB.sSOPDDB.insertData(disp);
                                        }
                                        SSOPDispenseditem ditem = new SSOPDispenseditem();
                                        ditem.dispid = reqdrug;                                     ditem.prdcat = "1";                                     ditem.hospdrgid = rowd["MNC_PH_CD"].ToString();
                                        ditem.drgid = rowd["tmt_code"].ToString();                  ditem.dfscode = rowd["MNC_PH_DIR_CD"].ToString();       ditem.dfstext = rowd["MNC_PH_DIR_DSC"].ToString();
                                        ditem.packsize = rowd["MNC_PH_UNT_CD"].ToString();          ditem.sigcode = rowd["MNC_PH_TIM_CD"].ToString();       ditem.sigtext = rowd["MNC_PH_TIM_DSC"].ToString();
                                        ditem.quantity = rowd["MNC_PH_QTY_PAID"].ToString();        ditem.unitprice = rowd["MNC_PH_PRI"].ToString();        ditem.chargeamt = rowd["MNC_PH_PRI"].ToString();
                                        ditem.reimbamt = rowd["MNC_PH_PRI"].ToString();             ditem.reimbprice = rowd["MNC_PH_PRI"].ToString();       ditem.prdsecode = "0";
                                        ditem.claimcont = "OD";                                     ditem.claimcat = "OP1";                                 ditem.multidisp = "11";
                                        ditem.supplyfor = "7D";                                     ditem.billtrans_id = rebillt;
                                        ditem.sigtext = ditem.sigtext.Length<= 0 ? "/" : ditem.sigtext;
                                        ditem.dfstext = ditem.dfstext.Length <= 0 ? "/" : ditem.dfstext;
                                        String re2 = bc.bcDB.sSOPDIDB.insertData(ditem);
                                    }
                                }
                            }
                            else if(ss1.Equals("17")) //รายการตรวจ
                            {
                                SSOPOpservices p = new SSOPOpservices();
                                p.opservices_id = "";                               p.svid = svid;                              p.class1 = class1;
                                p.invno = invno;                                    p.hcode = bc.iniC.ssoid;                               p.hn = hn;
                                p.pid = pid;                                        p.careaccount = "1";                        p.typeserv = "01";
                                p.typein = "1";                                     p.typeout = "1";                            p.dtappoint = "";
                                p.svpid = "ว"+rowb["MNC_DOT_CD_DF"].ToString();   //  Doctor
                                p.clinic = "01";                                      p.begdt = docdate;                          p.enddt = docdate;
                                p.lccode = svid;                                    p.codeset = "";                             p.stdcode = "";
                                p.svcharge = rowb["MNC_FN_PAD"].ToString();         p.completion = "";                          p.svtxcode = "";
                                p.claimcat = "OP1";                                 p.active = "1";                             p.billtrans_id = rebillt;
                                String re1 = bc.bcDB.sSOPOSDB.insertData(p);
                            }
                            else
                            {
                                SSOPBillItems p = new SSOPBillItems();
                                p.billtrans_id = rebillt;                           p.billitems_id = "";                            p.billmuad = rowb["mnc_grp_ss1"].ToString();
                                p.claimcat = "OP1";                                 p.invno = invno;                                p.svdate = rowb["MNC_FN_DAT"].ToString();
                                p.lccode = rowb["MNC_FN_CD"].ToString();            p.stdcode = "";                                 p.desc1 = rowb["MNC_FN_DSCT"].ToString();
                                p.qty = "1";                                        p.up1 = rowb["MNC_FN_PAD"].ToString();          p.chargeamt = rowb["MNC_FN_PAD"].ToString();
                                p.claimup = rowb["MNC_FN_PAD"].ToString();          p.claimamount = rowb["MNC_FN_PAD"].ToString();  p.svrefid = "";
                                p.active = "1";
                                String re1 = bc.bcDB.ssopBillItemsDB.insertData(p);
                            }
                        }
                        // end insert bill items
                        // insert OPDx
                        DataTable dtopdx = bc.bcDB.pt09DB.SelectBypreno(hn, preno, vsdate);
                        if (dtopdx.Rows.Count > 0)
                        {
                            foreach (DataRow rowd in dtopdx.Rows)
                            {
                                SSOPOpdx p = new SSOPOpdx();
                                p.opdx_id = "";                                     p.svid = svid;                                  p.class1 = class1;
                                p.sl = rowd["MNC_DIA_FLG"].ToString();              p.codeset = rowd["MNC_DIA_CD"].ToString();      p.code = rowd["MNC_DIA_CD"].ToString();
                                p.desc1 = "";                                       p.billtrans_id = rebillt;
                                String re1 = bc.bcDB.sSOPODDB.insertData(p);
                            }
                        }
                        //MessageBox.Show("insert ok", "");
                        //bc.bcDB.sSOPBillTranDB.updateData(ssopbilltran);
                        //bc.bcDB.sSOPBillTranDB.deleteData(ssopbilltran);
                    }
                    else
                    {
                        MessageBox.Show("insert error", "");
                    }
                }
            }
            grfItems.Rows.Count = 1;
            hideLbLoading();
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
            fPDFl2 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 2, FontStyle.Regular);
            fPDFs6 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 6, FontStyle.Regular);
            fPDFs8 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 8, FontStyle.Regular);
        }
        private SSOPBillTran setSSOPBillTran(string station, string dtran, string invno, string billno, string hn, String preno, string amount, string paid, string pid, string name, string paidcode, string payplan, string claimamt, string otherpay)
        {
            SSOPBillTran sSOPBillTran = new SSOPBillTran();
            sSOPBillTran.hcode = bc.iniC.ssoid; 
            sSOPBillTran.hmain = paidcode.Equals("44")?"11592": paidcode.Equals("45")? "11772" : paidcode.Equals("46")?"24036": paidcode.Equals("47")? "11592" : paidcode.Equals("48")? "11772" : paidcode.Equals("49")? "24036" : "24036";
            sSOPBillTran.billtran_id = "";            sSOPBillTran.station = station ?? "";
            sSOPBillTran.authcode = "";            sSOPBillTran.dtran = dtran ?? "";
            sSOPBillTran.invno = invno ?? "";
            sSOPBillTran.billno = billno ?? "";            sSOPBillTran.hn = hn ?? "";
            sSOPBillTran.memberno = "";            sSOPBillTran.amount = amount ?? "";
            sSOPBillTran.paid = paid ?? "";            sSOPBillTran.vercode = "";
            sSOPBillTran.tflag = "A";/*Fix Aขอเบิก*/            sSOPBillTran.pid = pid ?? "";
            sSOPBillTran.name = name ?? "";
            sSOPBillTran.payplan = payplan ?? "";            sSOPBillTran.claimamt = claimamt ?? "";
            sSOPBillTran.otherpayplan = "";            sSOPBillTran.otherpay = otherpay ?? "";
            sSOPBillTran.active = "1";            sSOPBillTran.preno = preno;
            return sSOPBillTran;
        }
        private void BtnSSOPSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfSSOPimp();
        }
        private void BtnSupraNewSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TSupra supra = setSupra();
            String re1 = tSupraDB.insertOrUpdate(supra);
            String re = bc.bcDB.vsDB.updateStatusSupra(txtSupraNewHn.Text.Trim(), lbSupraVsDate.Text,lbSuprapreno.Text);
            if (re.Equals("1"))     {        lfSbMessage.Text = "update ok"; txtSuprNewDoc.Text = re1; txtSuprNewDocYear.Text = supra.SupraDate.Substring(2); }
            else            {                lfSbMessage.Text =("update error");            }
        }
        private TSupra setSupra()
        {
            DateTime dtInput = DateTime.Now;
            dtInput = ((DateTime)txtSupraNewDateInput.Value).Date;
            DateTime dtSupra = DateTime.Now;
            dtSupra = ((DateTime)txtSupraNewDateRefer.Value).Date;
            if(dtInput.Year < 2000)             {                dtInput = dtInput.AddYears(543);           }
            if (dtSupra.Year < 2000)            {                dtSupra = dtSupra.AddYears(543);           }
            TSupra tSupra = new TSupra();
            tSupra.Hn = txtSupraNewHn.Text.Trim();
            tSupra.PatId = lbSupraPID.Text;
            tSupra.PatName = lbSupraNewPttName.Text;
            tSupra.SupraDoc = "";
            tSupra.SupraDate = dtSupra.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            tSupra.InputDate = dtInput.ToString("yyyy-MM-dd", new CultureInfo("en-US"));  
            tSupra.BranchCode = cboSuprNewHospHost.SelectedItem != null ? ((ComboBoxItem)(cboSuprNewHospHost.SelectedItem)).Value : "";
            tSupra.HospId = cboSuprNewHospSupra.SelectedItem != null ? ((ComboBoxItem)(cboSuprNewHospSupra.SelectedItem)).Value : "";
            tSupra.Diseased1 = txtSupraDiag1.Text.Trim();
            tSupra.Diseased2 = txtSupraDiag2.Text.Trim();
            tSupra.Diseased3 = txtSupraDiag3.Text.Trim();
            tSupra.Diseased4 = txtSupraDiag4.Text.Trim();
            tSupra.DoctorName = txtSupraNewDtrNameT.Text;
            tSupra.PatStaff = txtSupraNewSupraContactName.Text.Trim();
            tSupra.SupraTypeId = cboSupraNewReason.Text.Trim();
            tSupra.PatAge = lbSupraPttAge.Text.Trim();
            tSupra.PaidTypeName = cboSupraPaid.SelectedItem != null ? cboSupraPaid.SelectedItem.ToString():"";
            tSupra.doctor_remark1 = "";
            tSupra.doctor_remark2 = "";
            tSupra.doctor_remark3 = "";
            return tSupra;
        }
        private void initGrfMakeText()
        {
            grfMakeText = new C1FlexGrid();
            grfMakeText.Font = fEdit;
            grfMakeText.Dock = System.Windows.Forms.DockStyle.Fill;
            grfMakeText.Location = new System.Drawing.Point(0, 0);
            grfMakeText.Rows.Count = 1;
            grfMakeText.Cols.Count = 6;

            grfMakeText.Cols[colgrfMakeTextSearchid].Width = 90;
            grfMakeText.Cols[colgrfMakeTextSearchname].Width = 300;
            grfMakeText.Cols[colgrfMakeTextSearchhn].Width = 100;
            grfMakeText.Cols[colgrfMakeTextSearchvsdate].Width = 100;
            grfMakeText.Cols[colgrfMakeTextSearchchk].Width = 50;
            grfMakeText.Cols[colgrfMakeTextSearchchk].DataType = typeof(bool);
            grfMakeText.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfMakeText.Cols[colgrfMakeTextSearchname].Caption = "name";
            grfMakeText.Cols[colgrfMakeTextSearchhn].Caption = "hn";
            grfMakeText.Cols[colgrfMakeTextSearchvsdate].Caption = "visitdate";
            grfMakeText.Cols[colgrfMakeTextSearchid].Visible = false;
            grfMakeText.Cols[colgrfMakeTextSearchname].AllowEditing = false;
            grfMakeText.Cols[colgrfMakeTextSearchhn].AllowEditing = false;
            grfMakeText.Cols[colgrfMakeTextSearchvsdate].AllowEditing = false;

            grfMakeText.DoubleClick += GrfMakeText_DoubleClick;
            pnSSOPGenTextMaketext.Controls.Add(grfMakeText);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfMakeTextSearch, bc.iniC.themegrfIpd);
        }
        private void GrfMakeText_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfMakeText.Row < 1) return;
            String id = grfMakeText[grfMakeText.Row, colgrfMakeTextSearchid].ToString();
            String re = bc.bcDB.sSOPBillTranDB.updateStatusProcessMakeTextPrepare(id);
            grfMakeText.Rows.Remove(grfMakeText.Row);
        }
        private void setGrfMakeText()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPBillTranDB.selectBystatusprocess("1");
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfMakeText.Rows.Count = 1;
            grfMakeText.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfMakeText.Rows[i];
                rowa[colgrfMakeTextSearchid] = row1["billtran_id"].ToString();
                rowa[colgrfMakeTextSearchname] = row1["name"].ToString();
                rowa[colgrfMakeTextSearchhn] = row1["hn"].ToString();
                rowa[colgrfMakeTextSearchvsdate] = row1["dtran"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initGrfMakeTextSearch()
        {
            grfMakeTextSearch = new C1FlexGrid();
            grfMakeTextSearch.Font = fEdit;
            grfMakeTextSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            grfMakeTextSearch.Location = new System.Drawing.Point(0, 0);
            grfMakeTextSearch.Rows.Count = 1;
            grfMakeTextSearch.Cols.Count = 6;

            grfMakeTextSearch.Cols[colgrfMakeTextSearchid].Width = 90;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchname].Width = 300;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchhn].Width = 100;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchvsdate].Width = 100;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchchk].Width = 50;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchchk].DataType = typeof(bool);
            grfMakeTextSearch.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchname].Caption = "name";
            grfMakeTextSearch.Cols[colgrfMakeTextSearchhn].Caption = "hn";
            grfMakeTextSearch.Cols[colgrfMakeTextSearchvsdate].Caption = "visitdate";
            grfMakeTextSearch.Cols[colgrfMakeTextSearchid].Visible = false;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchname].AllowEditing = false;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchhn].AllowEditing = false;
            grfMakeTextSearch.Cols[colgrfMakeTextSearchvsdate].AllowEditing = false;

            grfMakeTextSearch.DoubleClick += GrfMakeTextSearch_DoubleClick;
            pnGenTextSearch.Controls.Add(grfMakeTextSearch);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfMakeTextSearch, bc.iniC.themegrfIpd);
        }

        private void GrfMakeTextSearch_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfMakeTextSearch.Row < 1) return;
            
            Row rowa = grfMakeText.Rows.Add();
            rowa[colgrfMakeTextSearchchk] = true;
            rowa[colgrfMakeTextSearchid] = grfMakeTextSearch[grfMakeTextSearch.Row, colgrfMakeTextSearchid].ToString();
            rowa[colgrfMakeTextSearchname] = grfMakeTextSearch[grfMakeTextSearch.Row, colgrfMakeTextSearchname].ToString();
            rowa[colgrfMakeTextSearchhn] = grfMakeTextSearch[grfMakeTextSearch.Row, colgrfMakeTextSearchhn].ToString();
            rowa[colgrfMakeTextSearchvsdate] = grfMakeTextSearch[grfMakeTextSearch.Row, colgrfMakeTextSearchvsdate].ToString();
            
        }

        private void setGrfMakeTextSearch(String search)
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPBillTranDB.selectBySearch(search);
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfMakeTextSearch.Rows.Count = 1;
            grfMakeTextSearch.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfMakeTextSearch.Rows[i];
                rowa[colgrfMakeTextSearchid] = row1["billtran_id"].ToString();
                rowa[colgrfMakeTextSearchname] = row1["name"].ToString();
                rowa[colgrfMakeTextSearchhn] = row1["hn"].ToString();
                rowa[colgrfMakeTextSearchvsdate] = row1["dtran"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initGrfOPDx()
        {
            grfOPDx = new C1FlexGrid();
            grfOPDx.Font = fEdit;
            grfOPDx.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPDx.Location = new System.Drawing.Point(0, 0);
            grfOPDx.Rows.Count = 1;
            grfOPDx.Cols.Count = 5;

            grfOPDx.Cols[colgrfOPDxid].Width = 90;
            grfOPDx.Cols[colgrfOPDxsl].Width = 60;
            grfOPDx.Cols[colgrfOPDxcodeset].Width = 100;
            grfOPDx .Cols[colgrfOPDxcode].Width = 300;

            grfOPDx.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfOPDx .Cols[colgrfOPDxsl].Caption = "SL";
            grfOPDx.Cols[colgrfOPDxcodeset].Caption = "codeset";
            grfOPDx .Cols[colgrfOPDxcode].Caption = "code";
            grfOPDx .Cols[colgrfOPDxid].Visible = false;
            grfOPDx.Cols[colgrfOPDxsl].AllowEditing = false;
            grfOPDx.Cols[colgrfOPDxcodeset].AllowEditing = false;
            grfOPDx.Cols[colgrfOPDxcode].AllowEditing = false;

            grfOPDx.Click += GrfOPDx_Click;
            pnOPDx.Controls.Add(grfOPDx);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfOPDx, bc.iniC.themegrfIpd);
        }
        private void GrfOPDx_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfOPDx.Row < 1) return;
            String opdxid = grfOPDx[grfOPDx.Row, colgrfOPDxid] != null ? grfOPDx[grfOPDx.Row, colgrfOPDxid].ToString() : "";
            setControlOPDx(opdxid);
        }
        private void setControlOPDx(String opdxid)
        {
            clearControlOPDx();
            SSOPOpdx item = bc.bcDB.sSOPODDB.selectByPk(opdxid);
            if (item == null) return;
            txtSSOPOPDxid.Value = item.opdx_id;
            txtSSOPOPDxsvid.Value = item.svid;
            txtSSOPOPDxcode.Value = item.code;
            txtSSOPOPDxdesc1.Value = item.desc1;
            txtSSOPOPDxsvid.Value = item.svid;
            bc.setC1Combo(cboSSOPOPDxcodeset, item.codeset);
            bc.setC1Combo(cboSSOPOPDxsl, item.sl);
            bc.setC1Combo(cboSSOPOPDxclass, item.class1);
        }
        private void clearControlOPDx()
        {
            txtSSOPOPDxid.Value = "";
            txtSSOPOPDxsvid.Value = "";
            txtSSOPOPDxcode.Value = "";
            txtSSOPOPDxdesc1.Value = "";
            txtSSOPOPDxsvid.Value = "";
            cboSSOPOPDxcodeset.SelectedIndex = -1;
            cboSSOPOPDxsl.SelectedIndex = -1;
            cboSSOPOPDxclass.SelectedIndex = -1;
        }
        private void setGrfOPDx()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPODDB.selectByBilltransId(txtSSOPBilltransID.Text.Trim());
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfOPDx.Rows.Count = 1;
            grfOPDx.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfOPDx.Rows[i];
                rowa[colgrfOPDxid] = row1["opdx_id"].ToString();
                rowa[colgrfOPDxsl] = row1["sl"].ToString();
                rowa[colgrfOPDxcodeset] = row1["codeset"].ToString();
                rowa[colgrfOPDxcode] = row1["code"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initGrfOpServ()
        {
            grfOpServ = new C1FlexGrid();
            grfOpServ.Font = fEdit;
            grfOpServ.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOpServ.Location = new System.Drawing.Point(0, 0);
            grfOpServ.Rows.Count = 1;
            grfOpServ.Cols.Count = 5;

            grfOpServ.Cols[colgrfOPServid].Width = 90;
            grfOpServ.Cols[colgrfOPServsvid].Width = 100;
            grfOpServ.Cols[colgrfOPServcodeset].Width = 100;
            grfOpServ.Cols[colgrfOPServtypein].Width = 300;

            grfOpServ.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfOpServ.Cols[colgrfOPServsvid].Caption = "svid";
            grfOpServ.Cols[colgrfOPServcodeset].Caption = "codeset";
            grfOpServ.Cols[colgrfOPServtypein].Caption = "typein";
            grfOpServ.Cols[colgrfOPServid].Visible = false;
            grfOpServ.Cols[colgrfOPServsvid].AllowEditing = false;
            grfOpServ.Cols[colgrfOPServcodeset].AllowEditing = false;
            grfOpServ.Cols[colgrfOPServtypein].AllowEditing = false;

            grfOpServ.Click += GrfOpServ_Click;
            pnOpServ.Controls.Add(grfOpServ);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfOpServ, bc.iniC.themegrfIpd);
        }

        private void GrfOpServ_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfOpServ.Row < 1) return;
            String opservid = grfOpServ[grfOpServ.Row, colgrfOPServid] != null ? grfOpServ[grfOpServ.Row, colgrfOPServid].ToString() : "";
            setControlOpServ(opservid);
        }
        private void setControlOpServ(String opservid)
        {
            clearControlOpServ();
            SSOPOpservices item = bc.bcDB.sSOPOSDB.selectByPk(opservid);
            if (item == null) return;
            txtSSOPOpServicesid.Value = item.opservices_id;
            txtSSOPOpServicesinvno.Value = item.invno;
            txtSSOPOpServicessvid.Value = item.svid;
            txtSSOPOPServicesdtappoint.Value = item.dtappoint;
            txtSSOPOPServicessvpid.Value = item.svpid;
            txtSSOPOPServicestdcode.Value = item.stdcode;
            //txtSSOPOPServicesdtappoint.Value = item.dtappoint;
            txtSSOPOPServicesbegdt.Value = item.begdt;
            txtSSOPOPServicesenddt.Value = item.enddt;
            txtSSOPOpServiceslccode.Value = item.lccode;
            txtSSOPOpServicessvtxcode.Value = item.svtxcode;
            bc.setC1Combo(cboSSOPOPServicesclass, item.class1);
            bc.setC1Combo(cboSSOPOpServicescareaccount, item.careaccount);
            bc.setC1Combo(cboSSOPOpServicestypeserv, item.typeserv);
            bc.setC1Combo(cboSSOPOpServicestypein, item.typein);
            bc.setC1Combo(cboSSOPOpServicestypeout, item.typeout);
            bc.setC1Combo(cboSSOPOpServicescompletion, item.completion);
            bc.setC1Combo(cboSSOPOPServicesclinic, item.clinic);
            bc.setC1Combo(cboSSOPOpServicesclaimcat, item.claimcat);
            bc.setC1Combo(cboSSOPOPServicescodeset, item.codeset);
        }
        private void clearControlOpServ()
        {
            txtSSOPOpServicesid.Value = "";
            txtSSOPOpServicesinvno.Value = "";
            txtSSOPOpServicessvid.Value = "";
            txtSSOPOPServicesdtappoint.Value = "";
            txtSSOPOPServicessvpid.Value = "";
            txtSSOPOPServicestdcode.Value = "";
            //txtSSOPOPServicesdtappoint.Value = item.dtappoint;
            txtSSOPOPServicesbegdt.Value = "";
            txtSSOPOPServicesenddt.Value = "";
            txtSSOPOpServiceslccode.Value = "";
            txtSSOPOpServicessvtxcode.Value = "";
            cboSSOPOPServicesclass.SelectedIndex = -1;
            cboSSOPOpServicescareaccount.SelectedIndex = -1;
            cboSSOPOpServicestypeserv.SelectedIndex = -1;
            cboSSOPOpServicestypein.SelectedIndex = -1;
            cboSSOPOpServicestypeout.SelectedIndex = -1;
            cboSSOPOpServicescompletion.SelectedIndex = -1;
            cboSSOPOPServicesclinic.SelectedIndex = -1;
            cboSSOPOpServicesclaimcat.SelectedIndex = -1;
            cboSSOPOPServicescodeset.SelectedIndex = -1;
        }
        private void setGrfOpServ()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPOSDB.selectByBilltransId(txtSSOPBilltransID.Text.Trim());
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfOpServ.Rows.Count = 1;
            grfOpServ.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfOpServ.Rows[i];
                rowa[colgrfOPServid] = row1["opservices_id"].ToString();
                rowa[colgrfOPServsvid] = row1["svid"].ToString();
                rowa[colgrfOPServcodeset] = row1["codeset"].ToString();
                rowa[colgrfOPServtypein] = row1["typein"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initGrfDispItems()
        {
            grfDispI = new C1FlexGrid();
            grfDispI.Font = fEdit;
            grfDispI.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDispI.Location = new System.Drawing.Point(0, 0);
            grfDispI.Rows.Count = 1;
            grfDispI.Cols.Count = 5;

            grfDispI.Cols[colgrfDispIid].Width = 90;
            grfDispI.Cols[colgrfDispIdrgid].Width = 100;
            grfDispI.Cols[colgrfDispIdfscode].Width = 100;
            grfDispI.Cols[colgrfDispIdfstext].Width = 500;

            grfDispI.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfDispI.Cols[colgrfDispIdrgid].Caption = "drgid";
            grfDispI.Cols[colgrfDispIdfscode].Caption = "dfscode";
            grfDispI.Cols[colgrfDispIdfstext].Caption = "dfstext";
            grfDispI.Cols[colgrfDispIid].Visible = false;
            grfDispI.Cols[colgrfDispIdrgid].AllowEditing = false;
            grfDispI.Cols[colgrfDispIdfscode].AllowEditing = false;
            grfDispI.Cols[colgrfDispIdfstext].AllowEditing = false;

            grfDispI.Click += GrfDispI_Click;
            pnDispI.Controls.Add(grfDispI);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfDispI, bc.iniC.themegrfOpd);
        }

        private void GrfDispI_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfDispI.Row < 1) return;
            String dispIid = grfDispI[grfDispI.Row, colgrfDispIid] != null ? grfDispI[grfDispI.Row, colgrfDispIid].ToString() : "";
            setControlDispI(dispIid);
        }
        private void setControlDispI(String dispiid)
        {
            clearControlDispI();
            SSOPDispenseditem item = bc.bcDB.sSOPDIDB.selectByPk(dispiid);
            if (item == null) return;
            txtSSOPDispenseditemid.Value = item.dispenseditem_id;
            txtSSOPDispenseditemdispid.Value = item.dispid;
            txtSSOPDispenseditemhospdrgid.Value = item.hospdrgid;
            txtSSOPDispenseditemdrgid.Value = item.drgid;
            txtSSOPDispenseditemdfscode.Value = item.dfscode;
            txtSSOPDispenseditemdfstext.Value = item.dfstext;
            txtSSOPDispenseditempacksize.Value = item.packsize;
            txtSSOPDispenseditemsigcode.Value = item.sigcode;
            txtSSOPDispenseditemsigtext.Value = item.sigtext;
            txtSSOPDispenseditemquantity.Value = item.quantity;
            txtSSOPDispenseditemunitprice.Value = item.unitprice;
            txtSSOPDispenseditemchargeamt.Value = item.chargeamt;
            txtSSOPDispenseditemreimbamt.Value = item.reimbamt;
            txtSSOPDispenseditemreimbprice.Value = item.reimbprice;
            txtSSOPDispenseditemsupplyfor.Value = item.supplyfor;
            txtSSOPDispenseditemmultidisp.Value = item.multidisp;
            //txtSSOPDispenseditemhospdrgid.Value = item.claimcont;
            bc.setC1Combo(cboSSOPDispenseditemclaimcont, item.claimcont);
            bc.setC1Combo(cboSSOPDispenseditemclaimcat, item.claimcat);
            bc.setC1Combo(cboSSOPDispenseditemprdcat, item.prdcat);
            bc.setC1Combo(cboSSOPDispenseditemprdsecode, item.prdsecode);
        }
        private void clearControlDispI()
        {
            txtSSOPDispenseditemid.Value = "";            txtSSOPDispenseditemdispid.Value = "";
            txtSSOPDispenseditemhospdrgid.Value = "";            txtSSOPDispenseditemdrgid.Value = "";
            txtSSOPDispenseditemdfscode.Value = "";            txtSSOPDispenseditemdfstext.Value = "";
            txtSSOPDispenseditempacksize.Value = "";            txtSSOPDispenseditemsigcode.Value = "";
            txtSSOPDispenseditemsigtext.Value = "";            txtSSOPDispenseditemquantity.Value = "";
            txtSSOPDispenseditemunitprice.Value = "";            txtSSOPDispenseditemchargeamt.Value = "";
            txtSSOPDispenseditemreimbamt.Value = "";            txtSSOPDispenseditemreimbprice.Value = "";
            txtSSOPDispenseditemsupplyfor.Value = "";            txtSSOPDispenseditemmultidisp.Value = "";
        }
        private void setGrfDispItems()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPDIDB.selectByBilltransId(txtSSOPBilltransID.Text.Trim());
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfDispI.Rows.Count = 1;
            grfDispI.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfDispI.Rows[i];
                rowa[colgrfDispIid] = row1["dispenseditem_id"].ToString();
                rowa[colgrfDispIdrgid] = row1["drgid"].ToString();
                rowa[colgrfDispIdfscode] = row1["dfscode"].ToString();
                rowa[colgrfDispIdfstext] = row1["dfstext"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initGrfDisp()
        {
            grfDisp = new C1FlexGrid();
            grfDisp.Font = fEdit;
            grfDisp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDisp.Location = new System.Drawing.Point(0, 0);
            grfDisp.Rows.Count = 1;
            grfDisp.Cols.Count = 5;

            grfDisp.Cols[colgrfDispid].Width = 90;
            grfDisp.Cols[colgrfDispprescdt].Width = 100;
            grfDisp.Cols[colgrfDispprescb].Width = 100;
            grfDisp.Cols[colgrfDispitemcnt].Width = 100;

            grfDisp.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfDisp.Cols[colgrfDispprescdt].Caption = "prescdt";
            grfDisp.Cols[colgrfDispprescb].Caption = "prescb";
            grfDisp.Cols[colgrfDispitemcnt].Caption = "itemcnt";
            grfDisp.Cols[colgrfDispid].Visible = false;
            grfDisp.Cols[colgrfDispprescdt].AllowEditing = false;
            grfDisp.Cols[colgrfDispprescb].AllowEditing = false;
            grfDisp.Cols[colgrfDispitemcnt].AllowEditing = false;

            grfDisp.Click += GrfDisp_Click;
            pnDisp.Controls.Add(grfDisp);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfDisp, bc.iniC.themegrfIpd);
        }

        private void GrfDisp_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfDisp.Row < 1) return;
            String dispId = grfDisp[grfDisp.Row, colgrfDispid] != null ? grfDisp[grfDisp.Row, colgrfDispid].ToString() : "";
            setControlDispsing(dispId);
        }
        private void setGrfDisp()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPDDB.selectByBilltransId(txtSSOPBilltransID.Text.Trim());
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfDisp.Rows.Count = 1;
            grfDisp.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfDisp.Rows[i];
                rowa[colgrfDispid] = row1["billdisp_id"].ToString();
                rowa[colgrfDispprescdt] = row1["prescdt"].ToString();
                rowa[colgrfDispprescb] = row1["prescb"].ToString();
                rowa[colgrfDispitemcnt] = row1["itemcnt"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void setControlDispsing(String dispId)
        {
            clearControlDispsing();
            SSOPDispensing item = bc.bcDB.sSOPDDB.selectByPk(dispId);
            if (item != null)
            {
                txtSSOPDispID.Value = item.billdisp_id;
                txtSSOPDispPrescdt.Value = item.prescdt;
                txtSSOPDispDispdt.Value = item.dispdt;
                txtSSOPDispPrescb.Value = item.prescb;
                txtSSOPDispItemcnt.Value = item.itemcnt;
                txtSSOPDispChargeamt.Value = item.chargeamt;
                txtSSOPDispClaimamt.Value = item.claimamt;
                txtSSOPDispPaid.Value = item.paid;
                txtSSOPDispOtherpay.Value = item.otherpay;
                txtSSOPDispReimburser.Value = item.reimburser;
                txtSSOPDispensinginvno.Value = item.invno;
                txtSSOPDispensingdispid.Value = item.dispid;
                txtSSOPDispensingdaycover.Value = item.daycover;
                txtSSOPDispensingsvid.Value = item.svid;
                txtSSOPDispensingproiderid.Value = item.providerid;
                //txtSSOPDispensingdaycover.Value = item.claimamt;

                bc.setC1Combo(cboSSOPDispBenefitplan, item.benefitplan);
                bc.setC1Combo(cboSSOPDispensingdispe, item.dispestat);
            }
        }
        private void clearControlDispsing()
        {
            txtSSOPDispID.Value = "";            txtSSOPDispPrescdt.Value = "";
            txtSSOPDispDispdt.Value = "";            txtSSOPDispPrescb.Value = "";
            txtSSOPDispItemcnt.Value = "";            txtSSOPDispChargeamt.Value = "";
            txtSSOPDispClaimamt.Value = "";            txtSSOPDispPaid.Value = "";
            txtSSOPDispOtherpay.Value = "";            txtSSOPDispReimburser.Value = "";
            txtSSOPDispensinginvno.Value = "";            txtSSOPDispensingdispid.Value = "";
            txtSSOPDispensingdaycover.Value = "";            txtSSOPDispensingsvid.Value = "";
            txtSSOPDispensingproiderid.Value = "";            bc.setC1Combo(cboSSOPDispBenefitplan, "");
            bc.setC1Combo(cboSSOPDispensingdispe, "");
        }
        private void initGrfBillItems()
        {
            grfBillI = new C1FlexGrid();
            grfBillI.Font = fEdit;
            grfBillI.Dock = System.Windows.Forms.DockStyle.Fill;
            grfBillI.Location = new System.Drawing.Point(0, 0);
            grfBillI.Rows.Count = 1;
            grfBillI.Cols.Count = 5;

            grfBillI.Cols[colgrfBillIid].Width = 90;
            grfBillI.Cols[colgrfBillIsvdate].Width = 100;
            grfBillI.Cols[colgrfBillIlccode].Width = 100;
            grfBillI.Cols[colgrfBillIdesc].Width = 400;

            grfBillI.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfBillI.Cols[colgrfBillIsvdate].Caption = "visitdate";
            grfBillI.Cols[colgrfBillIlccode].Caption = "lccode";
            grfBillI.Cols[colgrfBillIdesc].Caption = "desc1";
            grfBillI.Cols[colgrfBillIlccode].DataType = typeof(string);
            grfBillI.Cols[colgrfBillIid].Visible = false;
            grfBillI.Cols[colgrfBillIsvdate].AllowEditing = false;
            grfBillI.Cols[colgrfBillIlccode].AllowEditing = false;
            grfBillI.Cols[colgrfBillIdesc].AllowEditing = false;

            grfBillI.Click += GrfBillI_Click;
            pnBillI.Controls.Add(grfBillI);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfBillI, bc.iniC.themeApp);
        }

        private void GrfBillI_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfBillI.Row < 1) return;
            String billiid = grfBillI[grfBillI.Row, colgrfBillIid] != null ? grfBillI[grfBillI.Row, colgrfBillIid].ToString() : "";
            setControlBillItems(billiid);
        }
        private void setGrfBillI()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.ssopBillItemsDB.selectByBilltransId(txtSSOPBilltransID.Text.Trim());
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfBillI.Rows.Count = 1;
            grfBillI.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfBillI.Rows[i];
                rowa[colgrfBillIid] = row1["billitems_id"].ToString();
                rowa[colgrfBillIsvdate] = row1["svdate"].ToString();
                rowa[colgrfBillIlccode] = row1["lccode"].ToString();
                rowa[colgrfBillIdesc] = row1["desc1"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void setControlBillItems(String billiid)
        {
            clearControlBillItems();
            SSOPBillItems item = bc.bcDB.ssopBillItemsDB.selectByPk(billiid);
            if (item != null)
            {
                txtSSOPBillItemsID.Value = item.billitems_id;
                txtSSOPBillItemssvdate.Value = item.svdate;
                txtSSOPBillItemssvrefid.Value = item.svrefid;
                txtSSOPBillItemsLccode.Value = item.lccode;
                txtSSOPBillItemsDesc1.Value = item.desc1;
                bc.setC1Combo(cboSSOPBillItemsClaimcat, item.claimcat);
                bc.setC1Combo(cboSSOPBillItemsbillmuad, item.billmuad);
                txtSSOPBillItemsQty.Value = item.qty;
                txtSSOPBillItemsUp1.Value = item.up1;
                txtSSOPBillItemsChargeamt.Value = item.chargeamt;
                txtSSOPBillItemsClaimup.Value = item.claimup;
                txtSSOPBillItemsClaimamount.Value = item.claimamount;
                txtSSOPBillItemsInvno.Value = item.invno;
                txtSSOPBillItemsstdcode.Value = item.stdcode;
            }

        }
        private void clearControlBillItems()
        {
            txtSSOPBillItemsID.Value = "";            txtSSOPBillItemssvdate.Value = "";
            txtSSOPBillItemssvrefid.Value = "";            txtSSOPBillItemsLccode.Value = "";
            txtSSOPBillItemsstdcode.Value = "";            txtSSOPBillItemsDesc1.Value = "";
            bc.setC1Combo(cboSSOPBillItemsClaimcat, "");            bc.setC1Combo(cboSSOPBillItemsbillmuad, "");
            txtSSOPBillItemsQty.Value = "";            txtSSOPBillItemsUp1.Value = "";
            txtSSOPBillItemsChargeamt.Value = "";            txtSSOPBillItemsClaimup.Value = "";
            txtSSOPBillItemsClaimamount.Value = "";            txtSSOPBillItemsInvno.Value = "";
        }
        private void initGrfSSOP()
        {
            grfSSOP = new C1FlexGrid();
            grfSSOP.Font = fEdit;
            grfSSOP.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSSOP.Location = new System.Drawing.Point(0, 0);
            grfSSOP.Rows.Count = 1;
            grfSSOP.Cols.Count = 7;

            grfSSOP.Cols[colgrfSSOPhn].Width = 90;
            grfSSOP.Cols[colgrfSSOPname].Width = 300;
            grfSSOP.Cols[colgrfSSOPvsdate].Width = 100;
            grfSSOP.Cols[colgrfSSOPpreno].Width = 70;
            grfSSOP.Cols[colgrfSSOPid].Width = 150;

            grfSSOP.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfSSOP.Cols[colgrfSSOPhn].Caption = "hn";
            grfSSOP.Cols[colgrfSSOPname].Caption = "name"; // ชื่อหัวคอลัมน์
            grfSSOP.Cols[colgrfSSOPvsdate].Caption = "date";
            grfSSOP.Cols[colgrfSSOPpreno].Caption = "preno";
            grfSSOP.Cols[colgrfSSOPid].Caption = "id";
            grfSSOP.Cols[colgrfSSOPSymptoms].Caption = "อาการ";

            grfSSOP.Cols[colgrfSSOPid].Visible = false;
            grfSSOP.Cols[colgrfSSOPhn].AllowEditing = false;
            grfSSOP.Cols[colgrfSSOPname].AllowEditing = false;
            grfSSOP.Cols[colgrfSSOPvsdate].AllowEditing = false;
            grfSSOP.Cols[colgrfSSOPpreno].AllowEditing = false;
            grfSSOP.Cols[colgrfSSOPSymptoms].AllowEditing = false;

            grfSSOP.Click += GrfSSOP_Click;
            pnSSOP.Controls.Add(grfSSOP);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfSSOP, bc.iniC.themegrfIpd);
        }

        private void GrfSSOP_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfSSOP.Row < 1) return;
            String billtranid = grfSSOP[grfSSOP.Row, colgrfSSOPid] != null ? grfSSOP[grfSSOP.Row, colgrfSSOPid].ToString() : "";
            setControlSSOPprocess(billtranid);
        }
        private void setGrfSSOP()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.sSOPBillTranDB.selectBystatusprocess("0");
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfSSOP.Rows.Count = 1;
            grfSSOP.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfSSOP.Rows[i];
                rowa[colgrfSSOPhn] = row1["hn"].ToString();
                rowa[colgrfSSOPname] = row1["name"].ToString();
                rowa[colgrfSSOPvsdate] = row1["dtran"].ToString();
                rowa[colgrfSSOPpreno] = row1["preno"].ToString();
                rowa[colgrfSSOPid] = row1["billtran_id"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void setControlSSOPprocess(String billtranid)
        {
            clearControlBillItems();
            clearControlDispsing();
            clearControlDispI();
            clearControlOpServ();
            clearControlOPDx();
            SSOPBillTran sSOPBillTran = bc.bcDB.sSOPBillTranDB.selectByPk(billtranid);
            DataTable dtbitems = bc.bcDB.ssopBillItemsDB.selectByBilltransId(billtranid);
            DataTable dtdisp = bc.bcDB.sSOPDDB.selectByBilltransId(billtranid);
            DataTable dtdispitems = bc.bcDB.sSOPDIDB.selectByBilltransId(billtranid);
            DataTable dtopdx = bc.bcDB.sSOPODDB.selectByBilltransId(billtranid);
            DataTable dtOpservices = bc.bcDB.sSOPOSDB.selectByBilltransId(billtranid);
            if (sSOPBillTran != null)
            {
                txtSSOPProcHN.Value = sSOPBillTran.hn;
                lbSSOPProcName.Text = sSOPBillTran.name;
                txtSSOPProcVsDate.Value = sSOPBillTran.dtran;
                txtSSOPProcPID.Value = sSOPBillTran.pid;
                txtSSOPProcmemberno.Value = sSOPBillTran.memberno;
                txtSSOPProcVercode.Value = sSOPBillTran.vercode;
                txtSSOPProcPaid.Value = sSOPBillTran.paid;
                txtSSOPProcInvno.Value = sSOPBillTran.invno;
                txtSSOPProcBillno.Value = sSOPBillTran.billno;
                txtSSOPProcClaimAmt.Value = sSOPBillTran.claimamt;

                txtSSOPProcAmount.Value = sSOPBillTran.amount;
                txtSSOPProcHCODE.Value = sSOPBillTran.hcode;
                txtSSOPProcOtherPay.Value = sSOPBillTran.otherpay;
                bc.setC1Combo(cboSSOPProcTFlag, sSOPBillTran.tflag);
                bc.setC1Combo(cboSSOPProcOPayPlan, sSOPBillTran.otherpayplan);
                bc.setC1Combo(cboSSOPProcPayPlan, sSOPBillTran.payplan);
                bc.setC1Combo(cboSSOPProcStation, sSOPBillTran.station);
                txtSSOPProcHMain.Value = sSOPBillTran.hmain;
                txtSSOPBilltransID.Value = sSOPBillTran.billtran_id;
                //setGrfSSOPimp(sSOPBillTran.hn, sSOPBillTran.preno, sSOPBillTran.dtran);
                setGrfBillI();
                setGrfDisp();
                setGrfDispItems();
                setGrfOpServ();
                setGrfOPDx();
            }
        }
        private SSOPBillTran setSSOPprocess()
        {
            SSOPBillTran sSOPBillTran = new SSOPBillTran();

            sSOPBillTran.hn = txtSSOPProcHN.Text;
            //lbSSOPProcName.Text = sSOPBillTran.name;
            sSOPBillTran.dtran = bc.datetoDBCultureInfo(txtSSOPProcVsDate.Text);
            sSOPBillTran.memberno = txtSSOPProcmemberno.Text;
            sSOPBillTran.paid = txtSSOPProcPaid.Text;
            sSOPBillTran.billno = txtSSOPProcBillno.Text;
            sSOPBillTran.claimamt = txtSSOPProcClaimAmt.Text;
            sSOPBillTran.vercode = txtSSOPProcVercode.Text;
            sSOPBillTran.paid = txtSSOPProcPaid.Text;
            sSOPBillTran.invno = txtSSOPProcInvno.Text;
            sSOPBillTran.pid = txtSSOPProcPID.Text.Trim();

            sSOPBillTran.amount = txtSSOPProcAmount.Text;
            sSOPBillTran.hcode = txtSSOPProcHCODE.Text;
            sSOPBillTran.otherpay = txtSSOPProcOtherPay.Text;
            sSOPBillTran.tflag = cboSSOPProcTFlag.SelectedItem == null ? "" : ((ComboBoxItem)cboSSOPProcTFlag.SelectedItem).Value.ToString();
            sSOPBillTran.otherpayplan = cboSSOPProcOPayPlan.SelectedItem == null ? "" : ((ComboBoxItem)cboSSOPProcOPayPlan.SelectedItem).Value.ToString();
            sSOPBillTran.payplan = cboSSOPProcPayPlan.SelectedItem == null ? "" : ((ComboBoxItem)cboSSOPProcPayPlan.SelectedItem).Value.ToString();
            sSOPBillTran.station = cboSSOPProcStation.SelectedItem == null ? "" : ((ComboBoxItem)cboSSOPProcStation.SelectedItem).Value.ToString();
            sSOPBillTran.hmain = txtSSOPProcHMain.Text;
            sSOPBillTran.billtran_id = txtSSOPBilltransID.Text;
            //setGrfSSOPimp(sSOPBillTran.hn, sSOPBillTran.preno, sSOPBillTran.dtran);
            return sSOPBillTran;
        }
        private void initGrfItems()
        {
            grfItems = new C1FlexGrid();
            grfItems.Font = fEdit;
            grfItems.Dock = System.Windows.Forms.DockStyle.Fill;
            grfItems.Location = new System.Drawing.Point(0, 0);
            grfItems.Rows.Count = 1;
            grfItems.Cols.Count = 10;

            grfItems.Cols[colgrfSSOPimpVsDate].Width = 90;
            grfItems.Cols[colgrfSSOPimpPreno].Width = 60;
            grfItems.Cols[colgrfSSOPimpHn].Width = 100;
            grfItems.Cols[colgrfSSOPimpSymptoms].Width = 300;
            grfItems.Cols[colgrfSSOPimpName].Width = 150;
            grfItems.Cols[colgrfSSOPimpPaidName].Width = 380;
            grfItems.Cols[colgrfSSOPimpchk].Width = 80;

            grfItems.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfItems.Cols[colgrfSSOPimpchk].DataType = typeof(bool);
            grfItems.Cols[colgrfSSOPimpchk].Caption = "Select"; // ชื่อหัวคอลัมน์
            grfItems.Cols[colgrfSSOPimpchk].TextAlign = TextAlignEnum.LeftCenter;
            grfItems.Cols[colgrfSSOPimpVsDate].Caption = "visitdate";
            grfItems.Cols[colgrfSSOPimpPreno].Caption = "preno";
            grfItems.Cols[colgrfSSOPimpHn].Caption = "HN";
            grfItems.Cols[colgrfSSOPimpchk].Visible = false;
            grfItems.Cols[colgrfSSOPimpVsDate].AllowEditing = false;
            grfItems.Cols[colgrfSSOPimpVsTime].AllowEditing = false;
            grfItems.Cols[colgrfSSOPimpPreno].AllowEditing = false;
            grfItems.Cols[colgrfSSOPimpHn].AllowEditing = false;
            grfItems.Cols[colgrfSSOPimpName].AllowEditing = false;
            grfItems.Cols[colgrfSSOPimpSymptoms].AllowEditing = false;
            grfItems.Cols[colgrfSSOPimpPaidName].AllowEditing = false;

            grfItems.Click += GrfItems_Click;
            grfItems.DoubleClick += GrfItems_DoubleClick;
            pnSSOPItems.Controls.Add(grfItems);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfItems, bc.iniC.themegrfIpd);
        }

        private void GrfItems_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void GrfItems_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void initGrfSSOPimp()
        {
            grfSSOPimp = new C1FlexGrid();
            grfSSOPimp.Font = fEdit;
            grfSSOPimp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSSOPimp.Location = new System.Drawing.Point(0, 0);
            grfSSOPimp.Rows.Count = 1;
            grfSSOPimp.Cols.Count = 10;

            grfSSOPimp.Cols[colgrfSSOPimpVsDate].Width = 90;
            grfSSOPimp.Cols[colgrfSSOPimpPreno].Width = 60;
            grfSSOPimp.Cols[colgrfSSOPimpHn].Width = 100;
            grfSSOPimp.Cols[colgrfSSOPimpSymptoms].Width = 300;
            grfSSOPimp.Cols[colgrfSSOPimpName].Width = 150;
            grfSSOPimp.Cols[colgrfSSOPimpPaidName].Width = 380;
            grfSSOPimp.Cols[colgrfSSOPimpchk].Width = 80;

            grfSSOPimp.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfSSOPimp.Cols[colgrfSSOPimpchk].DataType = typeof(bool);
            grfSSOPimp.Cols[colgrfSSOPimpchk].Caption = "Select"; // ชื่อหัวคอลัมน์
            grfSSOPimp.Cols[colgrfSSOPimpchk].TextAlign = TextAlignEnum.LeftCenter;
            grfSSOPimp.Cols[colgrfSSOPimpVsDate].Caption = "visitdate";
            grfSSOPimp.Cols[colgrfSSOPimpPreno].Caption = "preno";
            grfSSOPimp.Cols[colgrfSSOPimpHn].Caption = "HN";
            //grfSSOP.Cols[colgrfSSOPchk].Visible = false;
            grfSSOPimp.Cols[colgrfSSOPimpVsDate].AllowEditing = false;
            grfSSOPimp.Cols[colgrfSSOPimpVsTime].AllowEditing = false;
            grfSSOPimp.Cols[colgrfSSOPimpPreno].AllowEditing = false;
            grfSSOPimp.Cols[colgrfSSOPimpHn].AllowEditing = false;
            grfSSOPimp.Cols[colgrfSSOPimpName].AllowEditing = false;
            grfSSOPimp.Cols[colgrfSSOPimpSymptoms].AllowEditing = false;
            grfSSOPimp.Cols[colgrfSSOPimpPaidName].AllowEditing = false;

            grfSSOPimp.Click += GrfSSOPimp_Click;
            grfSSOPimp.DoubleClick += GrfSSOPimp_DoubleClick;
            pnSSOPimp.Controls.Add(grfSSOPimp);
            grfSSOPimp.SelectionMode = SelectionModeEnum.Row;
            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfSSOPimp, bc.iniC.themegrfIpd);
        }

        private void GrfSSOPimp_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfSSOPimp.Row < 1) return;
            if (!grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpchk].Equals("True"))
            {
                Row rowa = grfItems.Rows.Add();
                rowa[colgrfSSOPimpchk] = true;
                rowa[colgrfSSOPimpVsDate] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpVsDate].ToString();
                rowa[colgrfSSOPimpPreno] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpPreno].ToString();
                rowa[colgrfSSOPimpHn] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpHn].ToString();
                rowa[colgrfSSOPimpSymptoms] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpSymptoms].ToString();
                rowa[colgrfSSOPimpName] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpName].ToString();
                rowa[colgrfSSOPimpPaidName] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpPaidName].ToString();
                rowa[colgrfSSOPimppid] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimppid].ToString();
                rowa[colgrfSSOPimpVsTime] = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpVsTime].ToString();
            }
        }

        private void GrfSSOPimp_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfSSOPimp.Row < 1) return;
            String hn = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpHn] != null ? grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpHn].ToString():"";
            String preno = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpPreno] != null ? grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpPreno].ToString() : "";
            String vsdate = grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpVsDate] != null ? grfSSOPimp[grfSSOPimp.Row, colgrfSSOPimpVsDate].ToString() : "";
            //setGrfSSOP(hn, preno, vsdate);
        }
        private void setGrfSSOPimp()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectPtt(txtSSOPhn.Text.Trim(), txtSSOPPaidcode.Text.Trim(), bc.datetoDBCultureInfo(txtSSOPDateStart.Text),bc.datetoDBCultureInfo(txtSSOPDateend.Text));
            //MessageBox.Show("hn "+hn, "");
            int i = 1, j = 1;

            grfSSOPimp.Rows.Count = 1;
            grfSSOPimp.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfSSOPimp.Rows[i];
                rowa[colgrfSSOPimpchk] = false;
                rowa[colgrfSSOPimpVsDate] = row1["MNC_DATE"].ToString();
                rowa[colgrfSSOPimpPreno] = row1["mnc_pre_no"].ToString();
                rowa[colgrfSSOPimpHn] = row1["mnc_hn_no"].ToString();
                rowa[colgrfSSOPimpSymptoms] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colgrfSSOPimpName] = row1["pttfullname"].ToString();
                rowa[colgrfSSOPimpPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[colgrfSSOPimppid] = row1["mnc_id_no"].ToString();
                rowa[colgrfSSOPimpVsTime] = bc.showTime( row1["MNC_TIME"].ToString());
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void TxtSupraNewHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setControlSupra(txtSupraNewHn.Text.Trim());
            }
        }
        private void setControlSupra(String hn)
        {
            clearControlSupra();
            lbSupraVN.Text = "";
            PTT = bc.bcDB.pttDB.selectPatinetByHn(hn);
            if (PTT == null) return;
            txtSupraNewHn.Value = PTT.MNC_HN_NO;
            lbSupraNewPttName.Text = PTT.Name;
            lbSupraPttAge.Text = PTT.AgeStringOK1DOT();
            lbSupraPttAttachNote.Text = PTT.MNC_ATT_NOTE;
            setGrfSupra(hn);
        }
        private void clearControlSupra()
        {
            txtSupraNewHn.Value = "";
            lbSupraNewPttName.Text = "";
            lbSupraPttAge.Text = "";
            lbSupraPttAttachNote.Text = "";
            lbSupraVN.Text = "";
            grfSupra.Rows.Count = 1;
            txtSupraDiag1.Value = "";
            txtSupraDiag2.Value = "";
            txtSupraDiag3.Value = "";
            txtSupraDiag4.Value = "";
            txtSupraNewDtrNameT.Text = "";
            txtSupraNewDtrCode.Text = "";
            txtSuprNewDoc.Text = "";
            txtSuprNewDocYear.Text = "";
            lbSupraPttAttachNote.Text = "";
            txtSupraNewDateInput.Value = null;
            txtSupraNewDateRefer.Value = null;
            txtSupraExpense.Value = "";
            c1TextBox12.Value = "";
            c1TextBox13.Value = "";
            txtSupraRemark.Value = "";
            cboSupraPaid.SelectedIndex = 0;
            cboSuprNewHospHost.SelectedIndex = 0;
            cboSuprNewHospSupra.SelectedIndex = 0;
            cboSupraNewReason.SelectedIndex = 0;
            txtSupraNewSupraContactName.Value = "นาย วุฒินันท์  ธัมมาภิรักษ์กุล";
            txtSupraNewSupraContactTel.Value = "";
            txtSupraNewContactName.Value = "";
            txtSupraNewContactTel.Value = "";
            txtSupraContact.Value = "";
            lbSupraPID.Text = "";
        }
        private void initGrfImage()
        {
            grfImage = new C1FlexGrid();
            grfImage.Font = fEdit;
            grfImage.Dock = System.Windows.Forms.DockStyle.Fill;
            grfImage.Location = new System.Drawing.Point(0, 0);
            grfImage.Rows.Count = 1;
            grfImage.Cols.Count = 3;
            grfImage.Cols[colgrfImageid].Width = 90;
            grfImage.Cols[colgrfImageimgpath].Width = 200;
            grfImage.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfImage.Cols[colgrfImageid].Visible = true;
            grfImage.Cols[colgrfImageimgpath].Visible = true;
            grfImage.Cols[colgrfImageid].Caption = "";
            grfImage.Cols[colgrfImageimgpath].Caption = "";
            grfImage.Cols[colgrfImageid].AllowEditing = false;
            grfImage.Cols[colgrfImageimgpath].AllowEditing = false;
            grfImage.AfterRowColChange += GrfImage_AfterRowColChange;

            pnImageView.Controls.Add(grfImage);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfImage, bc.iniC.themegrfIpd);
        }
        private void GrfImage_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfImage.Row < 1) return;

            String imgpath = grfImage[grfImage.Row, colgrfImageimgpath] != null ? grfImage[grfImage.Row, colgrfImageimgpath].ToString() : "";
            String id = grfImage[grfImage.Row, colgrfImageid] != null ? grfImage[grfImage.Row, colgrfImageid].ToString() : "";
            txtImageID.Value = id;
            showImage(imgpath);
            setSupraImageText(id);

        }
        private void showImage(String imgpath)
        {
            try
            {
                picBoxSupra.Image = null;
                if (imgpath.Equals("")) return;
                String path = "";
                if (imgpath.IndexOf('/') >= 0)
                {
                    path = imgpath.Substring(1);
                }
                else
                {
                    path = imgpath;
                }
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
        private void setGrfImage()
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dtvs = tSupraDB.selectImage();
            int i = 1, j = 1;

            grfImage.Rows.Count = 1;
            grfImage.Rows.Count = dtvs.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dtvs.Rows)
            {
                //pB1.Value++;
                Row rowa = grfImage.Rows[i];
                rowa[colgrfImageid] = row1["id"].ToString();
                rowa[colgrfImageimgpath] = row1["ftp_file_path_archive"].ToString();
                if (row1["status_process"].ToString().Equals("1"))
                {
                    grfImage.Rows[i].StyleNew.BackColor = Color.LightGreen;
                }
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void clearPicBoxSupra()
        {
            var old = picBoxSupra.Image;
            picBoxSupra.Image = null;
            old?.Dispose();
        }
        private void setSupraImageText(String id)
        {
            DataTable dt = new DataTable();
            dt = tSupraDB.selectTextImageById(id);
            if (dt.Rows.Count > 0)
            {
                txtImageText.Text = dt.Rows[0]["text_content"].ToString();
                txtImagePID.Value = dt.Rows[0]["pid_list"]!=null? dt.Rows[0]["pid_list"].ToString():"";
                txtWhatImageIS.Value = dt.Rows[0]["what_image_is"] != null ? dt.Rows[0]["what_image_is"].ToString() : "";
                if(txtWhatImageIS.Text.Equals("RECEIPT_000_OPD"))
                {
                    setControlREceipt000OPD();
                }
                else if (txtWhatImageIS.Text.Equals("INVIOCE_000_IPD"))
                {
                    setControlINVOICE000IPD();
                }
                
            }
            else
            {
                txtImageText.Text = "";
            }
        }
        private void setControlREceipt000OPD()
        {
            String pttname = extractPttName( txtImageText.Text.Trim());
            String total = extractTotalReceipt000(txtImageText.Text.Trim());
            lbImagePttNameT.Text = pttname.Trim();
            txtImageExp.Value = total;
            String name = lbImagePttNameT.Text.Trim().Replace("นาย", "");
            name = name.Replace("นาง", "");
            name = name.Replace("น.ส.", "");
            String[] name1 = name.Trim().Split(' ');
            if(name1.Length>0)
            {
                DataTable ptt = bc.bcDB.pttDB.selectPatient1(name1[0], name1[1]);
                if (ptt.Rows.Count > 0)
                {
                    txtImageHn.Value = ptt.Rows[0]["MNC_HN_NO"].ToString();
                    txtImagePID.Value = ptt.Rows[0]["MNC_ID_NO"].ToString();
                }
            }
        }
        private void setControlINVOICE000IPD()
        {
            if (txtImagePID.Text.Length <= 0) {String pid = extractPID(txtImageText.Text.Trim()); txtImagePID.Value = pid; }
            Patient ptt = bc.bcDB.pttDB.selectPatinetByPID(txtImagePID.Text, "pid");
            if (ptt.Hn.Length <= 0) { ptt = bc.bcDB.pttDB.selectPatinetByPID(txtImagePID.Text, "password"); }
            //if (ptt.Hn.Length <= 0) { ptt = bc.bcDB.pttDB.selectPatinetByPID(txtImagePID.Text, "password"); }
            txtImageHn.Value = ptt.Hn;
            lbImagePttNameT.Text = ptt.Name;
            List<(string Code, string Description)> aa = ExtractIcd9CmEntries(txtImageText.Text.Trim());
            String veshaphan = extractVeshaphan(txtImageText.Text.Trim());
            String total = extractTotal(txtImageText.Text.Trim());
            String visitdate = extractVisitDate(txtImageText.Text.Trim());
            String hospital = extractHospital(txtImageText.Text.Trim());
            if (aa.Count > 0)
            {
                String icd9cmcode = "";
                String icd9cmdesc = "";
                foreach (var a in aa)
                {
                    icd9cmcode += a.Code + ",";
                    icd9cmdesc += a.Description + "; ";
                }
                if (icd9cmcode.EndsWith(","))
                {
                    icd9cmcode = icd9cmcode.Substring(0, icd9cmcode.Length - 1);
                }
                txtImageICD9_1.Value = icd9cmcode;
                txtImageICD9_2.Value = icd9cmdesc;
            }
            else
            {
                txtImageICD9_1.Value = "";
                txtImageICD9_2.Value = "";
            }
            c1TextBox6.Value = veshaphan;
            txtImageExp.Value = total;
            txtImageHospital.Value = hospital;
        }
        private String extractPID(String text)
        {
            String pid = "";//บัตรประชาชน  
            try
            {
                if (string.IsNullOrWhiteSpace(text)) return string.Empty;
                int start = text.IndexOf("บัตรประชาชน", StringComparison.OrdinalIgnoreCase);
                if (start >= 0)
                {
                    String slice = text.Substring(start);
                    String[] slice1 = slice.Split('\n');
                    if (slice1.Length > 0) { pid = slice1[0].Trim(); pid = (pid.Replace("บัตรประชาชน", "")).Trim(); }
                }
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = "extractPID " + ex.Message;
            }
            return pid;
        }
        private String extractPttName(string text)
        {
            String amt1 = "", hospcode = "";
            try
            {
                if (string.IsNullOrWhiteSpace(text)) return string.Empty;
                int start = text.IndexOf("นาย", StringComparison.OrdinalIgnoreCase);
                int start0 = text.IndexOf("นายก", StringComparison.OrdinalIgnoreCase);
                int start1 = text.IndexOf("นาง", StringComparison.OrdinalIgnoreCase);
                int start2 = text.IndexOf("น.ส.", StringComparison.OrdinalIgnoreCase);
                if(start0 >= 0) { int start000 = text.IndexOf("นาย", start0+4, StringComparison.OrdinalIgnoreCase); if (start000 > 0) start = start000; }
                if (start >= 0)
                {
                    //start += "นาย".Length;
                    String slice = text.Substring(start);
                    String[] slice1 = slice.Split('\n');
                    if(slice1.Length>0)                    {                        amt1 = slice1[0].Trim();                    }
                }
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = "extractHospital " + ex.Message;
            }
            return amt1;
        }
        private String extractHospital(string text)
        {
            String amt1 = "", hospcode="";
            try
            {
                if (string.IsNullOrWhiteSpace(text)) return string.Empty;
                int start = text.IndexOf("ศูนย์", StringComparison.OrdinalIgnoreCase);
                int start2 = text.IndexOf("ชูนย์การแพทย์", StringComparison.OrdinalIgnoreCase);
                if(start2>0)        {           start = start2; }       //ถือว่าเป็นคำผิด พลาด ให้ถือว่าถูกต้อง
                if (start < 0) { return string.Empty; }
                int start1 = text.IndexOf("ศูนย์การแพทย์สมเด็จพระเทพรัตนราชสุดา", StringComparison.OrdinalIgnoreCase);
                if (start1 == 0) 
                {
                    txtImageHospitalCode.Value = "000";
                    DataTable dthosp = tSupraDB.selectHospitalByCode(txtImageHospitalCode.Text.Trim());
                    if(dthosp.Rows.Count>0)
                    {
                        amt1 = dthosp.Rows[0]["hosp_name_t"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = "extractHospital " + ex.Message;
            }
            return amt1;
        }
        private String extractVeshaphan(string text)
        {
            float amt1 = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(text)) return string.Empty;       //เวชภณ
                // Find start token "icd-9cm"               เวชภณ\nเท์ที่ไม่ใช่ยา            เวชภัณฑ\nเท์ที่ไม่ใช่ยา
                int start = text.IndexOf("เวชภ", StringComparison.OrdinalIgnoreCase);//"เวชภัณ\nท์ที่ไม่ใช่ยา"
                if (start < 0) return string.Empty;
                // slice after token        เวชภัณ\nเท์ที่ไม่ใช่ยา
                int start1 = text.IndexOf("เวชภัณฑ์", StringComparison.OrdinalIgnoreCase);
                int start12 = text.IndexOf("เวชภัณฑ์ที่ไม่ใช่ยา", StringComparison.OrdinalIgnoreCase);
                int start13 = text.IndexOf("เวชภัณ\nท์ที่ไม่ใช่ยา", StringComparison.OrdinalIgnoreCase);
                int start14 = text.IndexOf("เวชภณ\nเท์ที่ไม่ใช่ยา", StringComparison.OrdinalIgnoreCase);
                int start15 = text.IndexOf("เวชภัณ\nเท์ที่ไม่ใช่ยา", StringComparison.OrdinalIgnoreCase);
                int start16 = text.IndexOf("เวชภัณฑ\nเท์ที่ไม่ใช่ยา", StringComparison.OrdinalIgnoreCase);
                int start2 = text.IndexOf("ไม่ใช่ยา", StringComparison.OrdinalIgnoreCase);
                if (start1 > 0 && start12 >= start1 && start2 > start12) start += "เวชภัณฑ์ที่ไม่ใช่ยา".Length;
                else if (start13 > 0) start += "เวชภัณ\nท์ที่ไม่ใช่ยา".Length;
                else if (start14 > 0) start += "เวชภณ\nเท์ที่ไม่ใช่ยา".Length;
                else if (start15 > 0) start += "เวชภัณ\nเท์ที่ไม่ใช่ยา".Length;
                else if (start16 > 0) start += "เวชภัณฑ\nเท์ที่ไม่ใช่ยา".Length;
                else start += "เวชภัณ".Length;
                string slice = text.Substring(start);
                if (start > 0) { string[] amt = slice.Split('\n'); if (!float.TryParse(amt[0], out amt1)) float.TryParse(amt[1], out amt1); }
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = "extractVeshaphan " + ex.Message;
            }
            return amt1.ToString("#,###.00");
        }
        private String extractTotalReceipt000(string text)
        {
            float amt1 = 0;
            String slice = "", text1 = "";
            try
            {
                if (string.IsNullOrWhiteSpace(text)) return string.Empty;
                // Find start token "icd-9cm" 
                int start = text.LastIndexOf("รับชำระเงิน", StringComparison.OrdinalIgnoreCase);//"เวชภัณ\nท์ที่ไม่ใช่ยา"
                int start1 = text.LastIndexOf("จำนวน", StringComparison.OrdinalIgnoreCase);
                int start2 = text.LastIndexOf("เงินทอน", StringComparison.OrdinalIgnoreCase);
                if (start>0 && start1 >0 && start2 >0)
                {
                    text1 = text.Substring(start, start2- start);
                    String[] slice1 = text1.Split('\n');
                    if (slice1.Length > 0)
                    {
                        slice = slice1[slice1.Length - 1];
                        start1 = slice.IndexOf("จำนวน", StringComparison.OrdinalIgnoreCase);
                        slice = (slice.Substring(start1+ "จำนวน".Length)).Trim();
                        amt1 = float.Parse(slice);
                    }
                }
                return amt1.ToString("#,###.00");
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = "extractTotal " + ex.Message;
            }
            return amt1.ToString("#,###.00");
        }
        private String extractTotal(string text)
        {
            float amt1 = 0;
            String slice = "", text1="";
            try
            {
                if (string.IsNullOrWhiteSpace(text)) return string.Empty;
                // Find start token "icd-9cm" 
                int start = text.LastIndexOf("รวม", StringComparison.OrdinalIgnoreCase);//"เวชภัณ\nท์ที่ไม่ใช่ยา"
                int start0 = text.IndexOf("รวม", StringComparison.OrdinalIgnoreCase);
                int start1 = text.LastIndexOf("รวมสุทธิ", StringComparison.OrdinalIgnoreCase);//
                int start2 = text.LastIndexOf("รวม\nเสทธิ", StringComparison.OrdinalIgnoreCase);//"รวม\nเสทธิ"
                int start3 = text.LastIndexOf("รวมสุทธ", StringComparison.OrdinalIgnoreCase);//รวมสุทธ
                //int start4 = text.LastIndexOf("รวมสุทธิ", StringComparison.OrdinalIgnoreCase);
                if (start < 0) return string.Empty;
                if(text.Length == (start1 + ("รวมสุทธิ").Length))
                {
                    text1 = text.Substring(0, start);
                    start = text1.LastIndexOf("รวม", StringComparison.OrdinalIgnoreCase);
                    slice = text1.Substring(start);
                }
                else if(start2 >0)       //"รวม\nเสทธิ"
                { text1 = text.Substring(0, start2+20); start = text1.LastIndexOf("รวม\nเสทธิ", StringComparison.OrdinalIgnoreCase); slice = text1.Substring(start); }
                else if (start3 > 0 && start > 0)
                {
                    start += "รวมสุทธ".Length;
                    slice = text.Substring(start);
                    //check ว่า slice มียอดเงินไหม
                    string[] amt = slice.Split('\n');
                    Boolean chk1=false, chk2=false;
                    if(amt.Length > 0) chk1 = float.TryParse(amt[0], out float amt00);
                    if (amt.Length > 1) chk2 = float.TryParse(amt[1], out float amt01);
                    if(chk1 == false && chk2 == false)
                    {
                        text1 = text.Substring(0, start- "รวมสุทธิ".Length);
                        start = text1.LastIndexOf("รวม", StringComparison.OrdinalIgnoreCase);
                        slice = text1.Substring(start);
                    }
                }
                else if (start1 < 0 && start > 0)
                {
                    slice = text.Substring(start);
                }
                else if (start1 > 0 && start > 0)
                {
                    slice = text.Substring(start);
                    if (slice.Length > 1)
                    {
                        string[] slice1 = slice.Split('\n');
                        if (!float.TryParse(slice1[1], out float amt2))
                        {
                            text1 = text.Substring(0, start);
                            start = text1.LastIndexOf("รวม", StringComparison.OrdinalIgnoreCase);
                            slice = text1.Substring(start);
                        }
                        else { slice = amt2.ToString()+"\n"; }      //หาtotalเจอ
                    }
                }
                
                else
                {
                    if (start1 > 0) start += "รวมสุทธิ".Length;
                    else start += "รวมสุทธิ".Length;
                    slice = text.Substring(start);
                }
                // slice after token
                if (start > 0) 
                {
                    float amt10 = 0, amt11=0, amt12 = 0, amt13 = 0;
                    string[] amt = slice.Split('\n'); //if (!float.TryParse(amt[0], out amt1)) if (!float.TryParse(amt[1], out amt1)) if ((amt.Length>2) &&(!float.TryParse(amt[2], out amt1))) float.TryParse(amt[3], out amt1);
                    //แอบcheck ว่ามีรวมอื่นๆอีกไหม คือมีหลายรวม ให้เอารวม มากสุด เพราะหาtotalเจอในตำแหน่งที่ผิด
                    float.TryParse(amt[0], out amt10);
                    if (amt.Length > 1) float.TryParse(amt[1], out amt11);
                    if (amt.Length > 2) float.TryParse(amt[2], out amt12);
                    if (amt.Length > 3) float.TryParse(amt[3], out amt13);
                    if(amt12> amt10 && amt12 > amt13) { amt1 = amt12; }
                    else if (amt11 > amt10 && amt11 > amt12) { amt1 = amt11; }
                    else if(amt13 > amt10 && amt13 > amt12) { amt1 = amt13; }
                    else if(amt10 > amt12 && amt10 > amt13) { amt1 = amt10; }
                }
                return amt1.ToString("#,###.00");
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = "extractTotal " + ex.Message;
            }
            return amt1.ToString("#,###.00");
        }
        private String extractVisitDate(string text)
        {
            String amt1 = "";
            try
            {
                if (string.IsNullOrWhiteSpace(text)) return string.Empty;
                // Find start token "icd-9cm" 
                int start = text.IndexOf("ตั้งแต่วันที่", StringComparison.OrdinalIgnoreCase);//ตังแต่วันที
                int start1 = text.IndexOf("ตั่งแต่วันที่", StringComparison.OrdinalIgnoreCase);//ตั้งแต่วันที
                int start2 = text.IndexOf("ตังแต่วันที่", StringComparison.OrdinalIgnoreCase);//ตั้งแต่วันที่
                int start3 = text.IndexOf("ตั้งแต่วันที", StringComparison.OrdinalIgnoreCase);
                int start4 = text.IndexOf("ตังแต่วันที", StringComparison.OrdinalIgnoreCase);
                if (start1 > 0) { start = start1; }//ถือว่าเป็นคำผิด พลาด ให้ถือว่าถูกต้อง
                else if (start2 > 0) { start = start2; }//ถือว่าเป็นคำผิด พลาด ให้ถือว่าถูกต้อง
                else if (start3 > 0) { start = start3; }
                else if (start4 > 0) { start = start4; }
                if (start < 0) return string.Empty;
                // slice after token
                string slice = text.Substring(start + "ตั้งแต่วันที่".Length);
                if (slice.IndexOf('\n') >= 0) 
                {
                    if(slice.IndexOf('\n') == 0) { slice = slice.Substring(0, 40); }
                    else slice = slice.Substring(0, slice.IndexOf('\n'));
                    if (slice.IndexOf("ถึง") > 0)
                    {
                        String slice1 = (slice.Substring(0, slice.IndexOf("ถึง"))).Trim();
                        String slicedisc = (slice.Substring(slice.IndexOf("ถึง"))).Trim();
                        slicedisc = slicedisc.Replace("ถึง", "");
                        if (slicedisc.IndexOf("รวม") > 0)
                        {
                            slicedisc = (slicedisc.Substring(0, slicedisc.IndexOf("รวม"))).Trim();
                            String[] slicedisc2 = slicedisc.Split(' ');
                            if (slicedisc2.Length > 0)
                            {
                                String dddisc2 = "", mmdisc2 = "", yeardisc2 = "";
                                String[] slicedisc3 = slicedisc2[0].Split('/');
                                if (slicedisc3.Length > 0)
                                {
                                    dddisc2 = slicedisc3[0];
                                    mmdisc2 = slicedisc3[1];
                                    yeardisc2 = slicedisc3[2];
                                    if (int.Parse(yeardisc2) > 2500) { yeardisc2 = (int.Parse(yeardisc2) - 543).ToString(); }
                                    DateTime dtdisc = new DateTime(int.Parse(yeardisc2), int.Parse(mmdisc2), int.Parse(dddisc2));
                                    txtDiscDate.Value = (DateTime)dtdisc;
                                }
                            }
                        }
                        txtVsDate.Value = fixDate(slice1);
                    }
                }
                else { slice = slice.Trim(); txtVsDate.Value = fixDate(slice); }
                string[] amt = slice.Split('\n');
                //if (amt.Length > 0) float.TryParse(amt[0], out amt1);
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = "extractTotal " + ex.Message;
            }
            return amt1;
        }
        private DateTime fixDate(String txt)
        {
            DateTime date = new DateTime();
            String[] slice2 = txt.Split(' ');
            if (slice2.Length > 0)
            {
                String dd = "", mm = "", year = "";
                String[] slice3 = slice2[0].Split('/');
                if (slice3.Length > 0)
                {
                    dd = slice3[0];
                    mm = slice3[1];
                    year = slice3[2];
                    if (int.Parse(year) > 2500) { year = (int.Parse(year) - 543).ToString(); }
                    date = new DateTime(int.Parse(year), int.Parse(mm), int.Parse(dd));
                }
                //txtVsDate.Value
            }
            return date;
        }
        private List<(string Code, string Description)> ExtractIcd9CmEntries(string text)
        {
            var result = new List<(string Code, string Description)>();
            if (string.IsNullOrWhiteSpace(text)) return result;

            // Find start token "icd-9cm"
            int start = text.IndexOf("icd-9cm", StringComparison.OrdinalIgnoreCase);
            int start1 = text.IndexOf("icd - 9cm", StringComparison.OrdinalIgnoreCase);
            int start2 = text.IndexOf("icd 9cm", StringComparison.OrdinalIgnoreCase);
            if (start1 > 0) { start = start1; }//ถือว่าเป็นคำผิด พลาด ให้ถือว่าถูกต้อง
            else if (start2 > 0) { start = start2; }//ถือว่าเป็นคำผิด พลาด ให้ถือว่าถูกต้อง
            if (start < 0) return result;
            // slice after token
            start += "icd-9cm".Length;
            string slice = text.Substring(start);

            // optional end markers commonly appear after the section; trim to first marker if found
            string[] endMarkers = new[] { "ที่", "จำนวนเงิน", "รวม", "เลขที่อ้างอิง" };
            int endIdx = -1;
            foreach (var m in endMarkers)
            {
                int i = slice.IndexOf(m, StringComparison.OrdinalIgnoreCase);
                if (i >= 0 && (endIdx < 0 || i < endIdx)) endIdx = i;
            }
            if (endIdx > 0) slice = slice.Substring(0, endIdx);

            // Pattern: code (3-4 digits) followed by description until next code or end
            var matches = Regex.Matches(slice, @"\b(\d{3,4})\b\s*([^\d]+?)(?=(?:\s*\b\d{3,4}\b)|\z)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            foreach (Match m in matches)
            {
                string code = m.Groups[1].Value.Trim();
                string desc = m.Groups[2].Value.Replace("\r", " ").Replace("\n", " ").Trim();
                // normalize multiple spaces
                desc = Regex.Replace(desc, @"\s{2,}", " ").Trim();
                if (!string.IsNullOrEmpty(code))
                    result.Add((code, desc));
            }
            return result;
        }
        private void initGrfSupra()
        {
            grfSupra = new C1FlexGrid();
            grfSupra.Font = fEdit;
            grfSupra.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSupra.Location = new System.Drawing.Point(0, 0);
            grfSupra.Rows.Count = 1;
            grfSupra.Cols.Count = 8;
            grfSupra.Cols[colgrfSSOPhn].Width = 90;
            grfSupra.Cols[colgrfSSOPname].Width = 200;
            grfSupra.Cols[colgrfSupraVsDate].Width = 90;
            grfSupra.Cols[colgrfSupraPreno].Width = 60;
            grfSupra.Cols[colgrfSupraSymptoms].Width = 200;
            grfSupra.Cols[colgrfSupraVNAN].Width = 100;
            grfSupra.Cols[colgrfSupraStatusAdmit].Width = 60;
            
            grfSupra.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfSupra.Cols[colgrfSupraHN].Visible = true;
            grfSupra.Cols[colgrfSupraPname].Visible = true;
            grfSupra.Cols[colgrfSupraVsDate].Caption = "visitdate";
            grfSupra.Cols[colgrfSupraPreno].Caption = "preno";
            grfSupra.Cols[colgrfSupraSymptoms].Caption = "Symptoms";
            grfSupra.Cols[colgrfSupraVNAN].Caption = "VNAN";
            grfSupra.Cols[colgrfSupraStatusAdmit].Caption = "status";
            grfSupra.Cols[colgrfSupraHN].AllowEditing = false;
            grfSupra.Cols[colgrfSupraPname].AllowEditing = false;
            grfSupra.Cols[colgrfSupraVsDate].AllowEditing = false;
            grfSupra.Cols[colgrfSupraPreno].AllowEditing = false;
            grfSupra.Cols[colgrfSupraSymptoms].AllowEditing = false;
            grfSupra.Cols[colgrfSupraVNAN].AllowEditing = false;
            grfSupra.Cols[colgrfSupraStatusAdmit].AllowEditing = false;

            grfSupra.AfterRowColChange += GrfSupra_AfterRowColChange;

            pnSupra.Controls.Add(grfSupra);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfSupra, bc.iniC.themegrfIpd);
        }

        private void GrfSupra_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfSupra.Row < 1) return;
            String hn = grfSupra[grfSupra.Row, colgrfSupraHN] != null ? grfSupra[grfSupra.Row, colgrfSupraHN].ToString() : "";
            String preno = grfSupra[grfSupra.Row, colgrfSupraPreno] != null ? grfSupra[grfSupra.Row, colgrfSupraPreno].ToString() : "";
            String vsdate = grfSupra[grfSupra.Row, colgrfSupraVsDate] != null ? grfSupra[grfSupra.Row, colgrfSupraVsDate].ToString() : "";
            setControlSupra(hn,preno,vsdate);
        }
        private void setGrfSupra(String hn)
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dtvs = bc.bcDB.vsDB.selectLikeVisitByHn(hn);
            int i = 1, j = 1;

            grfSupra.Rows.Count = 1;
            grfSupra.Rows.Count = dtvs.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dtvs.Rows)
            {
                //pB1.Value++;
                Row rowa = grfSupra.Rows[i];
                rowa[colgrfSupraHN] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfSupraPname] = row1["ptt_fullnamet"].ToString();
                rowa[colgrfSupraVsDate] = row1["MNC_DATE"].ToString();
                rowa[colgrfSupraPreno] = row1["mnc_pre_no"].ToString();
                rowa[colgrfSupraStatusAdmit] = row1["MNC_AN_NO"].ToString().Equals("0") ? "O" : "I";
                rowa[colgrfSupraSymptoms] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colgrfSupraVNAN] = row1["MNC_AN_NO"].ToString().Equals("0") ? row1["MNC_VN_NO"].ToString(): row1["MNC_AN_NO"].ToString();

                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void setControlSupra(String hn, String preno, String vsdate)
        {
            lbSupraPttAttachNote.Text = "";
            PTT = bc.bcDB.pttDB.selectPatinetByHn(hn);
            if (PTT == null) return;
            txtSupraNewHn.Value = PTT.MNC_HN_NO;
            lbSupraNewPttName.Text = PTT.Name;
            lbSupraPttAge.Text = PTT.AgeStringOK1DOT();
            lbSupraPttAttachNote.Text = PTT.MNC_ATT_NOTE;
            lbSupraVsDate.Text = vsdate;
            lbSuprapreno.Text = preno;
            lbSupraPID.Text = PTT.MNC_ID_NO;
            //setGrfSupraDetail(hn, preno, vsdate);
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
        private void FrmSSO_Load(object sender, EventArgs e)
        {
            this.Text = "last update 2025-12-18";
            scSupraNew.HeaderHeight = 0;
            spSSOPget.HeaderHeight = 0;
            c1SplitContainer1.HeaderHeight = 0;
            scGenText.HeaderHeight = 0;
            spImage.HeaderHeight = 0;
            tcSSOP.SelectedTab = tabSSOPimp;
            tabSSOPDetails.SelectedTab = tabSSOPBillItems;
            System.Drawing.Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
        }
    }
}