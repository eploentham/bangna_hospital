using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.ImportServices.ReportingService4;
using C1.Win.FlexViewer;
using C1.Win.C1Document;
using System.IO;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using C1.C1Excel;
using bangna_hospital.Properties;
using C1.C1Pdf;
using System.Diagnostics;
using iTextSharp.text;
using Image = System.Drawing.Image;
using C1.Win.BarCode;
using GrapeCity.ActiveReports.Document.Section.Annotations;
using C1.Win.C1Command;
using iText.Kernel.Utils;

namespace bangna_hospital.gui
{
    public partial class FrmDoeAlien : Form
    {
        BangnaControl bc;
        System.Drawing.Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Patient PTT;
        Visit VS;
        C1FlexGrid grfOPD, grfVs, grfPDF, grfView, grfExcel, grfExcelVisit;
        C1ThemeController theme1;
        C1FlexViewer fvCerti;
        String REGCODE = "",FILENAMEEXCEL="";
        int colalcode = 1, colaltype = 2, colalprefix = 3, colalprefixen = 4, colalnameen = 5, colalsnamee=6, colalbdate=7, colalgender=8, colalnation=9, colalposid=10;
        int colvsdate = 1, colvshn =2, colvsalienname = 3, colvsalcode = 4, colvspre = 5, colvsheight=6, colvsweight=7, colvsbreath=8, colvshrate=9, colvsbp1l=10, colvsskintone=11, colvsnation=12, colvsposition=13,colvsdob=14, colvsgender=15, colvsempname=16, colvswkaddress=17;
        int colpdfname = 1, colpdfpath = 2;
        int colviewhn = 1, colviewname = 2, colviewvsdate = 3, colviewpreno = 4, colviewstatusdoe = 5;
        Boolean pageLoad = false, selectColAlCode=false, selectColAlName=false, selectColAlDOB = false, selectColAlNat = false, selectColAlAddr = false, selectColAlOwnName = false, selectColAlOwnAddr = false;
        Boolean selectColAlHeight = false, selectColAlwidth = false, selectColAlskin = false, clicktoDOE = false;
        String FLAGTABDOE = "", HN="", VSDATE="", PRENO="", FILENAME="";
        Label lbLoading;
        MemoryStream STREAMCertiDOE;
        List<DoeAlienList> DOEAR;
        C1PdfDocument pdfMERGE;
        public FrmDoeAlien(BangnaControl bc, String regcode, String fLAGTABDOE, String hn, String vsdate, String preno)
        {
            this.bc = bc;
            this.REGCODE = regcode;
            InitializeComponent();
            initConfig();
            FLAGTABDOE = fLAGTABDOE;
            HN = hn;
            VSDATE = vsdate;
            PRENO = preno;
        }
        private void initConfig()
        {
            pageLoad = true;
            VS = new Visit();
            PTT = new Patient();
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
            theme1 = new C1ThemeController();
            DOEAR = new List<DoeAlienList>();
            initFont();
            setEvent();
            setTheme();
            bc.bcDB.pttDB.setCboDeptOPD(cboDoeDeptNew, bc.iniC.station);
            initGrfOPD();
            initGrfVs();
            initGrfPDF();
            initGrfView();
            initLoading();
            initGrfExcelVisit();
            txtDoeView.Value = DateTime.Now;
            setControl();
            pageLoad = false;
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
        private void setControl()
        {
            txtDoetoken.Value = bc.iniC.doealientoken;
            txtDoeURLbangna.Value = bc.iniC.urlbangnadoe;
            txtDoeReqcode.Value = REGCODE;
            txtDoeDeptNew.Value = bc.iniC.station;
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
        private void setEvent()
        {
            btnDoeSave.Click += BtnDoeSave_Click;
            btnDoeGet.Click += BtnDoeGet_Click;
            txtDoePaidCode.KeyUp += TxtDoePaidCode_KeyUp;
            cboDoeDeptNew.SelectedItemChanged += CboDoeDeptNew_SelectedItemChanged;
            btnSendCertSend.Click += BtnSendCertSend_Click;
            btnSendCertgetPDF.Click += BtnSendCertgetPDF_Click;
            txtSendCertCertID.KeyUp += TxtSendCertCertID_KeyUp;
            txtDoeView.DropDownClosed += TxtDoeView_DropDownClosed;
            btnDoeExcelOpen.Click += BtnDoeExcelOpen_Click;
            btnCertSearch.Click += BtnCertSearch_Click;
            btnToken.Click += BtnToken_Click;
            label29.Click += Label29_Click;
            label32.Click += Label32_Click;
            label33.Click += Label33_Click;
            label34.Click += Label34_Click;
            label35.Click += Label35_Click;
            label37.Click += Label37_Click;
            label36.Click += Label36_Click;
            label38.Click += Label38_Click;
            label39.Click += Label39_Click;
            label40.Click += Label40_Click;
            btnDoeExcelCheck.Click += BtnDoeExcelCheck_Click;
            btnSendCertPend.Click += BtnSendCertPend_Click;
            btnDoeDoeCheck.Click += BtnDoeDoeCheck_Click;
            btnExcelVisit.Click += BtnExcelVisit_Click;
            txtSendCertdtrcode.KeyUp += TxtSendCertdtrcode_KeyUp;
            txtSendCertdtrcode.KeyPress += TxtSendCertdtrcode_KeyPress;
            btnExcelgenPDF.Click += BtnExcelgenPDF_Click;
            btnExcelSaveStaffNote.Click += BtnExcelSaveStaffNote_Click;
            txtCheckUPDoctorId.KeyUp += TxtCheckUPDoctorId_KeyUp;
            txtCheckUPDoctorId.KeyPress += TxtCheckUPDoctorId_KeyPress;
            btnExcelmergePDF.Click += BtnExcelmergePDF_Click;
        }

        private void BtnExcelmergePDF_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            mergePDF();
        }
        private void mergePDF()
        {
            if (pdfMERGE != null) { pdfMERGE.Save(bc.iniC.pathdoealiencert + "\\" + DateTime.Now.Ticks + ".PDF");  }
        }
        private void TxtCheckUPDoctorId_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtCheckUPDoctorId_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckUPDoctorName.Text = bc.selectDoctorName(txtCheckUPDoctorId.Text.Trim());
            }
        }
        private void BtnExcelSaveStaffNote_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            saveStaffNote();
        }
        private void saveStaffNote()
        {
            System.Drawing.Font fStaffN;
            fStaffN = new System.Drawing.Font(bc.iniC.staffNoteFontName, bc.staffNoteFontSize, FontStyle.Regular);
            foreach (Row alien in grfExcelVisit.Rows)
            {
                if (alien[colvsdate].ToString().Equals("date")) continue;
                Patient ptt = new Patient();
                Visit vs = new Visit();
                String packagecode = "", packagename = "", chiefcom = "ค่าใบรับรองแพทย์";
                ptt = bc.bcDB.pttDB.selectPatinetByHn(alien[colvshn].ToString());
                vs = bc.bcDB.vsDB.selectbyPreno(alien[colvshn].ToString(), alien[colvsdate].ToString(), alien[colvspre].ToString());
                DataTable dtpackage = new DataTable();
                dtpackage = bc.bcDB.pm39DB.selectByCompcode(bc.iniC.compcodedoe, ptt.MNC_SEX);
                if (dtpackage.Rows.Count > 0)
                {
                    packagecode = dtpackage.Rows[0]["MNC_PAC_CD"].ToString();                    packagename = dtpackage.Rows[0]["MNC_PAC_DSC"].ToString();
                }
                bc.genImgStaffNote(ptt, vs, fStaffN, packagecode, packagename, chiefcom);
            }
        }
        private void BtnExcelgenPDF_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            genCheckUPAlienPDF();
        }
        private void genCheckUPAlienPDF()
        {
            String filename = "";
            int gapLine = 14, linenumber = 5, gapX = 40, gapY = 20, xCol1 = 20, xCol2 = 40, xCol3 = 100, xCol4 = 200, xCol5 = 250, xCol6 = 300, xCol7 = 450, xCol8 = 510;
            pnPDFView.Controls.Clear();
            C1DockingTab tcHnLabOut = new C1DockingTab();
            tcHnLabOut.Dock = System.Windows.Forms.DockStyle.Fill;            tcHnLabOut.Location = new System.Drawing.Point(0, 266);            tcHnLabOut.Name = "tcHnLabOut";
            tcHnLabOut.Size = new System.Drawing.Size(669, 200);            tcHnLabOut.TabIndex = 0;            tcHnLabOut.TabsSpacing = 5;
            pnPDFView.Controls.Add(tcHnLabOut);
            tcHnLabOut.TabPages.Clear();
            theme1.SetTheme(tcHnLabOut, bc.iniC.themeApplication);
            int pagecnt = 0;
            foreach (Row alien in grfExcelVisit.Rows)
            {
                if (alien[colvsdate].ToString().Equals("date")) continue;
                linenumber = 5;
                String certid = "",alienname="";
                alienname = alien[colvsalienname].ToString();
                certid = bc.bcDB.mcertiDB.insertCertDoctor(txtCheckUPDoctorId.Text.Trim(), txtCheckUPDoctorName.Text.Trim(), alien[colvsdate].ToString(), alien[colvshn].ToString(), alienname, alien[colvspre].ToString(), bc.iniC.station);
                bc.bcDB.vsDB.updateMedicalCertId(alien[colvsalcode].ToString(), alien[colvspre].ToString(), alien[colvsdate].ToString(), certid);
                if (txtCheckUPDoctorId.Text.Trim().Equals("24738"))
                {
                    bc.bcDB.tokenDB.updateUsed(txtCheckUPDoctorId.Text.Trim(), "cert_alien", certid);       // ทำเพื่อ จะได้ มีระบบ ลายเซ็นแพทย์ เป็นแบบรูปภาพ แล้วมีการเก็บ ลง ใน ระบบ
                }
                if (certid.Length > 3) {                /*certid = certid.Replace("555", "");*/                certid = certid.Substring(3, 7); }
                if (certid.Length < 3) { MessageBox.Show("ไม่พบ เลขที่ กรุณาตรวจสอบ hn visitdate preno ไม่ถูกต้อง", ""); return; }    //ต้องมี เพราะมี case ที่ไม่มี certid แต่มีการเรียกใช้งาน
                lfSbMessage.Text = certid;
                String patheName = Environment.CurrentDirectory + "\\cert_med\\";
                if (!Directory.Exists(patheName)) { Directory.CreateDirectory(patheName); }
                C1PdfDocument pdf = new C1PdfDocument();
                pdfMERGE = new C1PdfDocument();
                StringFormat _sfRight, _sfCenter, _sfLeft;
                _sfRight = new StringFormat();                _sfCenter = new StringFormat();                _sfLeft = new StringFormat();                _sfRight.Alignment = StringAlignment.Far;                _sfCenter.Alignment = StringAlignment.Center;                _sfLeft.Alignment = StringAlignment.Near;
                pdf.FontType = FontTypeEnum.Embedded;
                pdfMERGE.FontType = FontTypeEnum.Embedded;
                filename = certid + "_" + alien[colvsalcode].ToString() + "_alien_" + DateTime.Now.Year.ToString() + ".pdf";
                try
                {
                    C1BarCode qrcode = new C1BarCode();
                    qrcode.CodeType = C1.BarCode.CodeType.QRCode;
                    qrcode.Text = alien[colvshn].ToString() + " " + alienname + " " + DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year).ToString() + " " + certid;
                    qrcode.AutoSize = false;                    qrcode.Width = 60;                    qrcode.Height = 60;
                    System.Drawing.Image imgqrcode = qrcode.Image;                    Image loadedImagelogo = Resources.LOGO_BW_tran;
                    float newWidth = loadedImagelogo.Width * 100 / loadedImagelogo.HorizontalResolution, newHeight = loadedImagelogo.Height * 100 / loadedImagelogo.VerticalResolution;
                    float widthFactor = 4.8F, heightFactor = 4.8F;
                    if (widthFactor > 1 | heightFactor > 1)
                    {
                        if (widthFactor > heightFactor) { widthFactor = 1; newWidth = newWidth / widthFactor; newHeight = newHeight / widthFactor; }
                        else { newWidth = newWidth / heightFactor;                            newHeight = newHeight / heightFactor; }
                    }
                    RectangleF recflogo = new RectangleF((PageSize.A4.Width / 2) - 38, 5, (int)newWidth, (int)newHeight);
                    pdf.DrawImage(loadedImagelogo, recflogo);
                    linenumber += gapLine; linenumber += gapLine; linenumber += gapLine; linenumber += gapLine;
                    pdf.DrawString("ใบรับรองแพทย์", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 38, linenumber += (gapLine - 5)), _sfLeft);
                    pdf.DrawString("เลขที่ " + certid, fPDFl2, Brushes.Black, new PointF(xCol2 + 520, 50), _sfRight);
                    pdf.DrawString("ตรวจสุขภาพคนต่างด้าว/แรงงานต่างด้าว", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 78, linenumber += (gapLine)), _sfLeft);
                    pdf.DrawString("วันที่ตรวจ " + DateTime.Now.ToString("dd-MM-") + (DateTime.Now.Year).ToString(), fPDF, Brushes.Black, new PointF(PageSize.A4.Width - 120, linenumber), _sfLeft);
                    pdf.DrawString("1. รายละเอียด ประวัติส่วนตัวของผู้รับการตรวจสุขภาพ", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString("1) ชื่อ-นามสกุล(นาย,นาง,นางสาว,เด็กชาย,เด็กหญิง) ....................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString("ชื่อ-นามสกุล(ภาษาอังกฤษ) ............................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString(alienname, fPDF, Brushes.Black, new PointF(xCol2 + 115, linenumber - 3), _sfLeft);
                    pdf.DrawString("เลขประจำตัวบุคคล .............................................  เลขที่ Passport .............................................................. อาชีพ ....................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString(alien[colvsalcode].ToString(), fPDF, Brushes.Black, new PointF(xCol2 + 85, linenumber - 3), _sfLeft);
                    pdf.DrawString("", fPDF, Brushes.Black, new PointF(xCol2 + 275, linenumber - 3), _sfLeft);
                    pdf.DrawString(alien[colvsposition].ToString(), fPDF, Brushes.Black, new PointF(xCol2 + 420, linenumber - 4), _sfLeft);
                    pdf.DrawString("วัน/เดือน/ปี เกิด .......................... เมืองที่เกิด ...................................... ประเทศ ...................................................  สัญชาติ ........................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString(alien[colvsdob].ToString(), fPDF, Brushes.Black, new PointF(xCol2 + 75, linenumber - 3), _sfLeft);
                    pdf.DrawString(alien[colvsnation].ToString(), fPDF, Brushes.Black, new PointF(xCol2 + 310, linenumber - 5), _sfLeft);
                    pdf.DrawString(alien[colvsnation].ToString(), fPDF, Brushes.Black, new PointF(xCol2 + 450, linenumber - 5), _sfLeft);

                    pdf.DrawString("2) ที่อยู่ปัจจุบัน ................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString(alien[colvswkaddress].ToString(), fPDF, Brushes.Black, new PointF(xCol2 + 70, linenumber - 3), _sfLeft);
                    pdf.DrawString("..........................................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);

                    pdf.DrawString("2. รายละเอียด ข้อมูลนายจ้าง/สถานประกอบการ", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                    //pdf.DrawString("ชื่อ-นามสกุล(นายจ้าง) ............................................................................. สถานประกอบการ .........................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString("ชื่อ-นามสกุล(นายจ้าง)/สถานประกอบการ ............................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString(alien[colvsempname].ToString(), fPDF, Brushes.Black, new PointF(xCol2 + 165, linenumber - 3), _sfLeft);
                    //pdf.DrawString(txtCheckUPComp.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 345, linenumber - 3), _sfLeft);
                    pdf.DrawString("ที่อยู่ ...................................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString(alien[colvswkaddress].ToString(), fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber - 5), _sfLeft);
                    pdf.DrawString("...........................................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString("", fPDF, Brushes.Black, new PointF(xCol2 + 15, linenumber - 3), _sfLeft);
                    pdf.DrawString("3. ข้อมูลแพทย์ผู้ตรวจ", fPDFl2, Brushes.Black, new PointF(xCol2, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString("นายแพทย์/แพทย์หญิง .....................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString(txtCheckUPDoctorName.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 95, linenumber - 3), _sfLeft);
                    pdf.DrawString("ใบอนุญาตประกอบวิชาชีพเวชกรรมเลขที่ .................................... สถานพยาบาล ..........................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString(txtCheckUPDoctorId.Text.Trim(), fPDF, Brushes.Black, new PointF(xCol2 + 165, linenumber - 3), _sfLeft);
                    pdf.DrawString(bc.iniC.hostname, fPDF, Brushes.Black, new PointF(xCol2 + 305, linenumber - 3), _sfLeft);
                    pdf.DrawString("ที่อยู่ ..................................................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString(bc.iniC.hostaddresst, fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber - 5), _sfLeft);
                    //linenumber += gapLine;
                    pdf.DrawString("ผลการตรวจสุขภาพ", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 30, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString("ส่วนสูง ................. ซ.ม. น้ำหนัก ............... ก.ก. สีผิว ........................... ความดันโลหิต ......................... มม.ปรอท ชีพจร .......................... ครั้ง/นาที", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString(alien[colvsheight].ToString(), fPDF, Brushes.Black, new PointF(xCol2 + 40, linenumber - 3), _sfLeft);
                    pdf.DrawString(alien[colvsweight].ToString(), fPDF, Brushes.Black, new PointF(xCol2 + 130, linenumber - 3), _sfLeft);
                    pdf.DrawString(alien[colvsskintone].ToString(), fPDF, Brushes.Black, new PointF(xCol2 + 200, linenumber - 5), _sfLeft);
                    pdf.DrawString(alien[colvsbp1l].ToString(), fPDF, Brushes.Black, new PointF(xCol2 + 325, linenumber - 3), _sfLeft);
                    pdf.DrawString(alien[colvshrate].ToString(), fPDF, Brushes.Black, new PointF(xCol2 + 455, linenumber - 3), _sfLeft);

                    pdf.DrawString("สภาพร่างกาย จิตใจทั่วไป ................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString("สุขภาพสมบูรณ์แข็งแรง", fPDF, Brushes.Black, new PointF(xCol2 + 105, linenumber - 3), _sfLeft);
                    pdf.DrawString("ผลการตรวจวัณโรค                                         ปกติ [  ]             ผิดปกติ/ให้รักษา [  ]                       ระยะอันตราย [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    int xxx = 0;
                    xxx = xCol2 + 224;
                    pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);
                    pdf.DrawString("ผลการตรวจโรคเรื้อน                                       ปกติ [  ]             ผิดปกติ/ให้รักษา [  ]                       ระยะติดต่อ/อาการเป็นที่รังเกียจ [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    xxx = xCol2 + 224;
                    pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);
                    pdf.DrawString("ผลการตรวจโรคเท้าช้าง                                    ปกติ [  ]             ผิดปกติ/ให้รักษา [  ]                       อาการเป็นที่รังเกียจ [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    xxx = xCol2 + 224;
                    pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);
                    pdf.DrawString("ผลการตรวจโรคซิฟิลิส                                      ปกติ [  ]             ผิดปกติ/ให้รักษา [  ]                       ระยะที่3 [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    xxx = xCol2 + 224;
                    pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);
                    pdf.DrawString("ผลการตรวจสารเสพติด                                     ปกติ [  ]             พบสารเสพติด   [  ]                       ให้ตรวจยืนยัน [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    xxx = xCol2 + 226;
                    pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);
                    pdf.DrawString("ผลการตรวจโรคอาการของโรคพิษสุราเรื้อรัง             ปกติ [  ]             ปรากฏอาการ   [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    xxx = xCol2 + 227;
                    pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);

                    pdf.DrawString("ผลการตรวจการตั้งครรภ์                                    ไม่ตั้งครรภ์ [  ]                                      ตั้งครรภ์ [  ]", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    xxx = xCol2 + 248;
                    if (alien[colvsgender].ToString().Equals("F"))
                    {
                        pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft);
                    }
                    pdf.DrawString("ผลการตรวจอื่นๆ (ถ้ามี) ..................................................................................................................................................................................................", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    //linenumber += gapLine;
                    pdf.DrawString("สรุปผลตรวจ", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 30, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString("1. [  ] สุขภาพสมบูรณ์ดี", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    xxx = xCol2 + 20; pdf.DrawString("/", famt7B, Brushes.Black, new PointF(xxx, linenumber - 8), _sfLeft); 
                    pdf.DrawString("2. [  ] ผ่านการตรวจสุขภาพ แต่ต้องให้การรักษา ควบคุม ติดตามอย่างต่อเนื่อง", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString("                     [  ] วัณโรค             [  ] โรคเรื้อน             [  ] โรคเท้าช้าง               [  ] โรคซิฟิลิส ", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString("3. [  ] ไม่ผ่านการตรวจสุขภาพเนื่องจาก ", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    pdf.DrawString("3.1 ร่างกายทุพพลภาพจนไม่สามารถประกอบการหาเลี้ยงชีพได้/จิตฟั่นเฟือน ไม่สมประกอบ", fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber += (gapLine)), _sfLeft);
                    pdf.DrawString("3.2 เป็นโรคไม่อนุญาตให้ทำงาน และไม่ให้การประกันสุขภาพ(ตามประกาศกระทรวงสาธารณสุขฯ)", fPDF, Brushes.Black, new PointF(xCol2 + 35, linenumber += (gapLine)), _sfLeft);
                    int lineqrcode = linenumber;
                    pdf.DrawString("แพทย์ผู้ตรวจ", fPDFl2, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 100, linenumber += (gapLine + 5)), _sfLeft);
                    linenumber += (gapLine + 15);
                    int linesign = linenumber;
                    pdf.DrawString("(.....................................................................................) ให้ประทับตรา", fPDF, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 100, linesign), _sfLeft);
                    pdf.DrawString(txtCheckUPDoctorName.Text.Trim(), fPDF, Brushes.Black, new PointF((PageSize.A4.Width / 2) - 90, linenumber - 3), _sfLeft);
                    pdf.DrawString("(หมายเหตุ ใบรับรองแพทย์ฉบับนี้มีอายุ 90วัน นับตั้งแต่วันที่ตรวจร่างกาย ยกเว้น กรณีใช้สำหรับประกันสุขภาพมีอายุ 1 ปี)", fPDF, Brushes.Black, new PointF(xCol2 + 5, linenumber += (gapLine + 5)), _sfLeft);
                    RectangleF recfqrcode = new RectangleF(xCol2 + 465, lineqrcode - 10, imgqrcode.Width, imgqrcode.Height + 20);
                    pdf.DrawImage(imgqrcode, recfqrcode);
                    //ถ้าใน Folder sign มีรูป ให้แสดง ลงใน PDF
                    String signFileName = Environment.CurrentDirectory + "\\sign\\sign_" + txtCheckUPDoctorId.Text.Trim() + ".jpg";
                    if (File.Exists(signFileName))
                    {
                        Image imgSign = null;
                        imgSign = Image.FromFile(signFileName);
                        float newWidthsign = imgSign.Width * 100 / imgSign.HorizontalResolution, newHeightsign = imgSign.Height * 100 / imgSign.VerticalResolution;
                        //float widthFactorsign = 60.8F, heightFactorsign = 60.8F;
                        RectangleF recfSign = new RectangleF(xCol4 + 75, linesign - 32, 90, 30);
                        pdf.DrawImage(imgSign, recfSign);
                        RectangleF recflogo1 = new RectangleF(xCol4 + 235, linesign - 72, (int)newWidth, (int)newHeight);
                        pdf.DrawImage(loadedImagelogo, recflogo1);
                    }
                    pdf.Save(patheName + filename);
                    MemoryStream ms = new MemoryStream();                    pdf.Save(ms);                    ms.Position = 0;
                    C1FlexViewer fvCerti = new C1FlexViewer();
                    fvCerti.AutoScrollMargin = new System.Drawing.Size(0, 0);                    fvCerti.AutoScrollMinSize = new System.Drawing.Size(0, 0);                    fvCerti.Dock = System.Windows.Forms.DockStyle.Fill;
                    fvCerti.Location = new System.Drawing.Point(0, 0);                    fvCerti.Name = "fvCerti"+ alien[colvsalcode].ToString();                    fvCerti.Size = new System.Drawing.Size(1065, 790);
                    fvCerti.TabIndex = 0;                    fvCerti.Ribbon.Minimized = true;
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    pds.LoadFromStream(ms);
                    fvCerti.DocumentSource = pds;
                    C1DockingTabPage tabHnLabOut = new C1DockingTabPage();
                    tabHnLabOut.Location = new System.Drawing.Point(1, 24);                    //tabScan.Name = "c1DockingTabPage1";
                    tabHnLabOut.Size = new System.Drawing.Size(667, 175);                    tabHnLabOut.TabIndex = 0;                    tabHnLabOut.Text = alien[colvsalcode].ToString();
                    tabHnLabOut.Name = "tabalien_" + alien[colvsalcode].ToString();                    tabHnLabOut.Controls.Add(fvCerti);
                    //tabHnLabOut.DoubleClick += TabHnLabOut_DoubleClick;
                    tcHnLabOut.TabPages.Add(tabHnLabOut);
                    if (File.Exists(signFileName))
                    {//online ให้ส่งไปที่ server FTP ถ้าเป็น offline ต้อง scan เลยไม่ต้อง
                        FtpClient ftpc = new FtpClient(bc.iniC.hostFTPCertMeddoe, bc.iniC.userFTPCertMeddoe, bc.iniC.passFTPCertMeddoe, false);
                        ftpc.upload(bc.iniC.folderFTPCertMeddoe + "/" + filename, ms);
                    }
                    if (pagecnt == 0) { pdfMERGE.NewPage(); }
                    pdfMERGE.Pages.Add(pdf.Pages[0]);
                    pagecnt++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    new LogWriter("e", "FrmOPD genCheckUPAlienPDF " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "genCheckUPAlienPDF ", ex.Message);
                }
                finally
                {
                    //pdf.Dispose();
                    //Process p = new Process();
                    //ProcessStartInfo s = new ProcessStartInfo(patheName + filename);
                    ////s.Arguments = "/c dir *.cs";
                    //p.StartInfo = s;

                    //p.Start();
                }
            }
        }
        private String insertCertDoctor(String hn, String preno, String vsdate, String pttname)
        {
            String certid = "";
            MedicalCertificate mcerti = new MedicalCertificate();
            mcerti.active = "1";
            mcerti.an = "";
            mcerti.certi_id = "";
            mcerti.certi_code = "";
            mcerti.dtr_code = txtCheckUPDoctorId.Text.Trim();
            mcerti.dtr_name_t = txtCheckUPDoctorName.Text;
            mcerti.status_ipd = "O";
            mcerti.visit_date = VSDATE;
            mcerti.visit_time = "";
            mcerti.remark = "";
            mcerti.line1 = "";
            mcerti.line2 = "";
            mcerti.line3 = "";
            mcerti.line4 = "";
            mcerti.hn = hn;
            mcerti.pre_no = PRENO;
            mcerti.ptt_name_e = pttname;
            mcerti.ptt_name_t = "";
            mcerti.doc_scan_id = "";
            mcerti.status_2nd_leaf = "1";
            mcerti.counter_name = bc.iniC.station;
            bc.bcDB.mcertiDB.insertMedicalCertificate(mcerti, "");
            
            certid = bc.bcDB.mcertiDB.selectCertIDByHn(hn, preno, vsdate);      //มีปัญหาเรื่องแก้ไขวันที่ ในการค้นหา
            
            return certid;
        }
        private void TxtSendCertdtrcode_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtSendCertdtrcode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                txtSendCertdtrName.Text = bc.selectDoctorName(txtSendCertdtrcode.Text.Trim());
            }
        }

        private async void BtnExcelVisit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (FILENAMEEXCEL.Length <= 0) { lbErr.Text = "ไม่พบ file Excel"; return; }
            C1XLBook _c1xl = new C1XLBook();
            try
            {
                int found = 0, notfound = 0;
                txtDoePaidCode.Value = "18";
                _c1xl.Load(FILENAMEEXCEL);
                XLSheet sheet = _c1xl.Sheets[0];
                for(int i=0; i<sheet.Rows.Count; i++)
                {
                    String alcode = sheet[i, int.Parse(txtExcelColAlCode.Text)-1].Text;
                    String width = sheet[i, int.Parse(txtExcelColAlWidth.Text)-1].Text;
                    String height = sheet[i, int.Parse(txtExcelColAlHeight.Text) - 1].Text;         //ความดันโลหิต
                    String breath = sheet[i, int.Parse(txtExcelColAlPresure.Text) - 1].Text;        //ชีพจร
                    String hrate = sheet[i, int.Parse(txtExcelColAlHRate.Text) - 1].Text;         
                    String skintone = sheet[i, int.Parse(txtExcelColAlSkin.Text) - 1].Text;
                    String owmemp = sheet[i, int.Parse(txtExcelColAlOwnName.Text) - 1].Text;
                    String owmaddr = sheet[i, int.Parse(txtExcelColAlAddr.Text) - 1].Text;
                    String nation = sheet[i, int.Parse(txtExcelColAlNat.Text) - 1].Text;
                    String position = sheet[i, int.Parse(txtExcelColAlPosition.Text) - 1].Text;
                    if (!long.TryParse(alcode, out long chk)) continue;
                    DoeAlienRequest doea = new DoeAlienRequest();
                    doea.reqcode = "";
                    doea.alcode = alcode;
                    if (alcode.Length <= 0)
                    {
                        return;
                    }
                    doea.token = txtDoetoken.Text.Trim();
                    var url = txtDoeURLbangna.Text.Trim();
                    String jsonEpi = JsonConvert.SerializeObject(doea, Formatting.Indented);
                    jsonEpi = jsonEpi.Replace("[" + Environment.NewLine + "    null" + Environment.NewLine + "  ]", "[]");

                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            //var content = new StringContent(jsonEpi, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = await httpClient.GetAsync(url + "?regcode=&alcode=" + alcode);
                            if (response.StatusCode.ToString().ToUpper().Equals("OK"))
                            {
                                String content1 = await response.Content.ReadAsStringAsync();
                                DoeAlientResponse doear = JsonConvert.DeserializeObject<DoeAlientResponse>(content1);
                                if (doear.statuscode.Equals("-100")) return;
                                foreach (var alien in doear.alienlist)
                                {
                                    if (alcode.Equals(alien.alcode))
                                    {
                                        found++;
                                        txtDoeAlcode.Value = alien.alcode;
                                        txtDoeAltype.Value = alien.altype;
                                        txtDoeAlprefix.Value = alien.alprefix;
                                        txtDoeAlprefixen.Value = alien.alprefixen;
                                        txtDoeAlnameen.Value = alien.alnameen;
                                        txtDoeAlsnameen.Value = alien.alsnameen;
                                        txtDoeAlbdate.Value = alien.albdate;
                                        txtDoeAlgender.Value = alien.algender;
                                        txtDoeAlnation.Value = alien.alnation;
                                        txtDoeAlposid.Value = position;
                                        txtDoeEmpname.Value = owmemp;
                                        txtDoeWkaddress.Value = owmaddr;
                                        Visit vs = DoeAddVisit();
                                        vs.high = height;
                                        vs.weight = width;
                                        vs.hrate = hrate;
                                        vs.breath = breath;
                                        vs.bp1l = "";
                                        vs.skin_tone = skintone;
                                        vs.VisitDate = DateTime.Now.Year.ToString()+"-"+DateTime.Now.ToString("MM-dd");
                                        bc.bcDB.vsDB.updateVitalSign1(vs, "1618");
                                        String re = bc.bcDB.pt16DB.insertStoreProcedure(vs.HN, vs.VisitDate, vs.preno, "1618", txtExcelProcedure.Text.Trim(), "1");

                                        sheet[i, int.Parse(txtExcelColAlHRate.Text) - 1 + 1].Value = vs.preno;
                                        sheet[i, int.Parse(txtExcelColAlHRate.Text) - 1 + 2].Value = vs.HN;
                                        Row rowa = grfExcelVisit.Rows.Add();
                                        rowa[colvsdate] = vs.VisitDate;
                                        rowa[colvshn] = vs.HN;
                                        rowa[colvspre] = vs.preno;
                                        rowa[colvsalienname] = alien.alprefixen+" "+ alien.alnameen+" "+ alien.alsnameen;
                                        rowa[colvsalcode] = alien.alcode;
                                        rowa[colvsheight] = vs.high;
                                        rowa[colvsweight] = vs.weight;
                                        rowa[colvsbreath] = vs.breath;
                                        rowa[colvshrate] = vs.hrate;
                                        rowa[colvsbp1l] = vs.bp1l;
                                        rowa[colvsskintone] = vs.skin_tone;
                                        rowa[colvsnation] = nation;
                                        rowa[colvsposition] = position;
                                        rowa[colvsdob] = alien.albdate;
                                        rowa[colvsgender] = alien.algender;
                                        rowa[colvsempname] = owmemp;
                                        rowa[colvswkaddress] = owmaddr;
                                    }
                                    else
                                    {
                                        notfound++;
                                    }
                                }
                            }
                            else
                            {
                                //MessageBox.Show("error send " + result.StatusCode, "");
                            }
                        }
                        txtDoeHN.Value = "";
                        txtDoepreno.Value = "";
                        //c1SplitterPanel3.SizeRatio = 0;
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("error " + ex.Message, "");
                    }
                }
                sheet = null;
            }
            catch (Exception ex)
            {

            }
            _c1xl.Dispose();
        }

        private async void BtnDoeDoeCheck_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                int found = 0, notfound = 0;                DOEAR.Clear();
                foreach (Row rowa in grfExcel.Rows)
                {
                    String alcode = rowa[int.Parse(txtExcelColAlCode.Text)] != null? rowa[ int.Parse(txtExcelColAlCode.Text) ].ToString():"";
                    if (!long.TryParse(alcode, out long chk)) continue;
                    DoeAlienRequest doea = new DoeAlienRequest();
                    doea.reqcode = "";                    doea.alcode = alcode;
                    if (alcode.Length <= 0)                    {                        return;                    }
                    doea.token = txtDoetoken.Text.Trim();
                    var url = txtDoeURLbangna.Text.Trim();
                    String jsonEpi = JsonConvert.SerializeObject(doea, Formatting.Indented);
                    jsonEpi = jsonEpi.Replace("[" + Environment.NewLine + "    null" + Environment.NewLine + "  ]", "[]");
                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            //var content = new StringContent(jsonEpi, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = await httpClient.GetAsync(url + "?regcode=&alcode=" + alcode);
                            if (response.StatusCode.ToString().ToUpper().Equals("OK"))
                            {
                                String content1 = await response.Content.ReadAsStringAsync();
                                DoeAlientResponse doear = JsonConvert.DeserializeObject<DoeAlientResponse>(content1);
                                if (doear.statuscode.Equals("-100")) return;
                                foreach (var alien in doear.alienlist)
                                {
                                    if (alcode.Equals(alien.alcode))
                                    {
                                        rowa[grfExcel.Cols.Count - 1] = alien.alcode;
                                        found++;
                                        rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                                        DOEAR.Add(alien);
                                    }
                                    else
                                    {
                                        rowa[grfExcel.Cols.Count - 1] = "not found";
                                        notfound++;
                                    }
                                }
                            }
                            else
                            {
                                //MessageBox.Show("error send " + result.StatusCode, "");
                            }
                        }
                        txtDoeHN.Value = "";                        txtDoepreno.Value = "";
                        //c1SplitterPanel3.SizeRatio = 0;
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("error " + ex.Message, "");
                    }
                    lfSbMessage.Text = "found "+found+" notfound "+notfound ;
                }
            }
            catch (Exception ex)            {            }
        }
        private void BtnSendCertPend_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(STREAMCertiDOE.Length <= 0)
            {
                lbErr.Text = "STREAMCertiDOE.Length <= 0";
                return;
            }
            STREAMCertiDOE.Position = 0;
            FtpClient ftpc = new FtpClient(bc.iniC.hostFTPCertMeddoe, bc.iniC.userFTPCertMeddoe, bc.iniC.passFTPCertMeddoe, false);
            var url = txtSendCerturlBangna.Text.Trim();
            String doefielname = "";
            doefielname = txtSendCertCertID.Text.Trim().Replace("555", "") + "_alien_" + txtSendCertalcode.Text.Trim() + ".pdf";
            Boolean chk = ftpc.upload(bc.iniC.folderFTPCertMeddoe + "_myanmar//" + doefielname, STREAMCertiDOE);
            FtpClient ftpd = new FtpClient(bc.iniC.hostFTPCertMeddoe, bc.iniC.userFTPCertMeddoe, bc.iniC.passFTPCertMeddoe, false);
            ftpd.delete(FILENAME);
            clearControlSendDOE();
            setGrfPDFFormFTP();
        }

        private void Label40_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            selectColAlskin = true;
            label40.ForeColor = Color.Red;
        }

        private void BtnDoeExcelCheck_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(FILENAMEEXCEL.Length <= 0)            { lbErr.Text = "ไม่พบ file Excel";                              return;            }
            C1XLBook _c1xl = new C1XLBook();
            try
            {
                int rowindex = int.Parse(txtExcelColAlRowStart.Text);   rowindex--;
                _c1xl.Load(FILENAMEEXCEL);
                XLSheet sheet = _c1xl.Sheets[0];
                String alcode = sheet[rowindex, int.Parse(txtExcelColAlCode.Text) - 1].Text;
                String alname = sheet[rowindex, int.Parse(txtExcelColAlName.Text) - 1].Text;
                String aldob = sheet[rowindex, int.Parse(txtExcelColAlDOB.Text) - 1].Text;
                String alnat = sheet[rowindex, int.Parse(txtExcelColAlNat.Text) - 1].Text;
                String aladdr = sheet[rowindex, int.Parse(txtExcelColAlAddr.Text) - 1].Text;
                String alheight = sheet[rowindex, int.Parse(txtExcelColAlHeight.Text) - 1].Text;
                String alwidth = sheet[rowindex, int.Parse(txtExcelColAlWidth.Text) - 1].Text;
                String alownname = sheet[rowindex, int.Parse(txtExcelColAlOwnName.Text) - 1].Text;
                String alownaddr = sheet[rowindex, int.Parse(txtExcelColAlOwnAddr.Text) - 1].Text;
                lfSbMessage.Text = "alcode " + alcode + " alname " + alname + " dob " + aldob + " สัญชาติ " + alnat + " alที่อยู่ " + aladdr + " ส่วนสูง " + alheight + " น้ำหนัก " + alwidth + " นายจ้าง " + alownname + " นายจ้างที่อยู่ " + alownaddr;
                sheet = null;
            }
            catch(Exception ex)
            {
                lbErr.Text = ex.Message;
            }
            _c1xl.Dispose();
        }
        private void Label39_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            selectColAlHeight = true;
            label39.ForeColor = Color.Red;
        }
        private void Label38_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            selectColAlwidth = true;
            label38.ForeColor = Color.Red;
        }

        private void Label36_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            selectColAlOwnAddr = true;
            label36.ForeColor = Color.Red;
        }

        private void Label37_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            selectColAlOwnName = true;
            label37.ForeColor = Color.Red;
        }

        private void Label35_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            selectColAlAddr = true;
            label35.ForeColor = Color.Red;
        }

        private void Label34_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            selectColAlNat = true;
            label34.ForeColor = Color.Red;
        }

        private void Label33_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            selectColAlDOB = true;
            label33.ForeColor = Color.Red;
        }

        private void Label32_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            selectColAlName = true;
            label32.ForeColor = Color.Red;
        }

        private void Label29_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            selectColAlCode = true;
            label29.ForeColor = Color.Red;
        }

        private void BtnToken_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            PatientM26 dtr = new PatientM26();
            dtr = bc.bcDB.pm26DB.selectByPk("24738");
            FrmToken frm = new FrmToken(bc, dtr);
            frm.ShowDialog(this);
        }
        private void BtnCertSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            panel10.Controls.Clear();
            if (txtSendCertalcode.Text.Trim().Length <= 0) { lfSbMessage.Text = "ไม่พบค่า alcode"; return; }
            ListBox lb = new ListBox();
            lb.Dock = DockStyle.Fill;
            lb.Font = fEdit;
            panel10.Controls.Add(lb);
            DataTable dt = bc.bcDB.vsDB.selectCertByPID(txtSendCertalcode.Text.Trim());
            foreach(DataRow dataRow in dt.Rows)
            {
                lb.Items.Add(dataRow["MNC_DATE"].ToString()+" "+dataRow["certi_id"].ToString());
            }
        }
        private void BtnDoeExcelOpen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xls;*.xlsx";
            ofd.Title = "Select Excel File";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                showLbLoading();
                //lbExcelFileName.Text = ofd.FileName;
                chkDoeExcel.Checked = true;
                pnExcel.Controls.Clear();
                C1XLBook _c1xl = new C1XLBook();
                FILENAMEEXCEL = ofd.FileName;
                _c1xl.Load(FILENAMEEXCEL);
                XLSheet sheet = _c1xl.Sheets[0];
                grfExcel = new C1FlexGrid();
                grfExcel.Font = fEdit;                grfExcel.Dock = System.Windows.Forms.DockStyle.Fill;                grfExcel.Cols.Count = sheet.Columns.Count+1;
                grfExcel.Rows.Count = sheet.Rows.Count+1;
                //spExcel.Controls.Clear();
                //spExcel.Controls.Add(grfExcel);
                for (int i = 0; i < sheet.Rows.Count; i++)
                {
                    for (int j = 0; j < sheet.Columns.Count; j++)
                    {
                        grfExcel[i, j+1] = sheet[i, j].Text;
                    }
                }
                grfExcel.Click += GrfExcel_Click;
                pnExcel.Controls.Add(grfExcel);
                c1SplitterPanel3.SizeRatio = 100;
                sheet = null;
                _c1xl.Dispose();
                hideLbLoading();
            }
        }
        private void GrfExcel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (selectColAlCode)
            {
                txtExcelColAlCode.Value = grfOPD.Col;
                selectColAlCode = false;
                label29.ForeColor = Color.Black;
            }
            if (selectColAlName)
            {
                txtExcelColAlName.Value = grfOPD.Col;
                selectColAlName = false;
                label32.ForeColor = Color.Black;
            }
            if (selectColAlDOB)
            {
                txtExcelColAlDOB.Value = grfOPD.Col;
                selectColAlDOB = false;
                label33.ForeColor = Color.Black;
            }
            if (selectColAlNat)
            {
                txtExcelColAlNat.Value = grfOPD.Col;
                selectColAlNat = false;
                label34.ForeColor = Color.Black;
            }
            if (selectColAlAddr)
            {
                txtExcelColAlAddr.Value = grfOPD.Col;
                selectColAlAddr = false;
                label35.ForeColor = Color.Black;
            }
            if (selectColAlOwnAddr)
            {
                txtExcelColAlOwnAddr.Value = grfOPD.Col;
                selectColAlOwnAddr = false;
                label36.ForeColor = Color.Black;
            }
            if (selectColAlOwnName)
            {
                txtExcelColAlOwnName.Value = grfOPD.Col;
                selectColAlOwnName = false;
                label37.ForeColor = Color.Black;
            }
            if (selectColAlwidth)
            {
                txtExcelColAlWidth.Value = grfOPD.Col;
                selectColAlwidth = false;
                label38.ForeColor = Color.Black;
            }
            if (selectColAlHeight)
            {
                txtExcelColAlHeight.Value = grfOPD.Col;
                selectColAlHeight = false;
                label39.ForeColor = Color.Black;
            }
            if (selectColAlskin)
            {
                txtExcelColAlSkin.Value = grfOPD.Col;
                selectColAlskin = false;
                label40.ForeColor = Color.Black;
            }
            if (grfOPD.Col == int.Parse(txtExcelColAlCode.Text.Trim()))
            {
                txtDoeAlcode.Value = grfOPD[grfOPD.Row, int.Parse(txtExcelColAlCode.Text.Trim())].ToString();
            }
        }

        private void TxtDoeView_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            setGrfView();
        }

        private void TxtSendCertCertID_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode==Keys.Enter)
            {
                setControlSendDOE("555"+txtSendCertCertID.Text.Trim());
            }
        }

        private void BtnSendCertgetPDF_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ListPDfinFolder();
        }
        private void ListPDfinFolder()
        {
            String folderPath = bc.iniC.pathdoealiencert;
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            // Get all files in the folder
            string[] files = Directory.GetFiles(folderPath);
            // Iterate through the files and process them
            foreach (string file in files)
            {
                Console.WriteLine("File: " + Path.GetFileName(file));
                FileInfo fi = new FileInfo(file);
                String[] filename = fi.FullName.Replace(fi.DirectoryName, "").Replace("\\", "").Replace(fi.Extension, "").Split('_');
                if (filename.Length > 1)
                {
                    foreach (String name in filename)
                    {
                        if (name.Equals(txtSendCertCertID.Text.Trim().Replace("555", "")))
                        {
                            showPDF(fi.FullName);
                        }
                    }
                }
            }
        }
        private void showPDF(String filename)
        {
            if(panel3.Controls.Count > 0) {panel3.Controls.Clear(); }
            fvCerti = new C1FlexViewer();
            fvCerti.AutoScrollMargin = new System.Drawing.Size(0, 0);            fvCerti.AutoScrollMinSize = new System.Drawing.Size(0, 0);            fvCerti.Dock = System.Windows.Forms.DockStyle.Fill;
            fvCerti.Location = new System.Drawing.Point(0, 0);            fvCerti.Name = "fvCerti";            fvCerti.Size = new System.Drawing.Size(1065, 790);
            fvCerti.TabIndex = 0;            fvCerti.Ribbon.Minimized = true;            panel3.Controls.Add(fvCerti);
            theme1.SetTheme(fvCerti, bc.iniC.themeApp);
            FtpClient ftpc = new FtpClient(bc.iniC.hostFTPCertMeddoe, bc.iniC.userFTPCertMeddoe, bc.iniC.passFTPCertMeddoe, false);
            MemoryStream streamCertiDoe = ftpc.download(filename);
            STREAMCertiDOE = new MemoryStream();
            streamCertiDoe.Position = 0;            streamCertiDoe.CopyTo(STREAMCertiDOE, 4096);            streamCertiDoe.Position = 0;            STREAMCertiDOE.Position = 0;
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            pds.LoadFromStream(streamCertiDoe);
            fvCerti.OperationError += FvCerti_OperationError;
            fvCerti.DocumentSource = pds;
            FILENAME = filename;
            //ExtractImagesAndDecodeQRCode(file);

        }

        private void FvCerti_OperationError(object sender, OperationErrorEventArgs e)
        {
            //throw new NotImplementedException();
            lbErr.Text = e.Exception.Message;
        }
        static void ExtractImagesAndDecodeQRCode(string pdfPath)
        {
            using (PdfReader pdfReader = new PdfReader(pdfPath))
            {
                using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
                {
                    for (int pageNumber = 1; pageNumber <= pdfDocument.GetNumberOfPages(); pageNumber++)
                    {
                        iText.Kernel.Pdf.PdfPage page = pdfDocument.GetPage(pageNumber);
                        IEventListener listener = new ImageRenderListener();
                        PdfCanvasProcessor processor = new PdfCanvasProcessor(listener);
                        processor.ProcessPageContent(page);
                    }
                }
            }
        }
        private void BtnSendCertSend_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SendCertFTP(FILENAME);
        }
        private async void SendCertFTP(String ftpfilename)
        {            
            lfSbMessage.Text = ftpfilename+" "+ STREAMCertiDOE.Length + " " + STREAMCertiDOE.Position;
            STREAMCertiDOE.Position = 0;
            if (STREAMCertiDOE.Length <= 0)
            {
                lbErr.Text = "STREAMCertiDOE.Length <= 0 SendCertFTP";
                return;
            }
            FtpClient ftpc = new FtpClient(bc.iniC.hostFTPbangnadoe, bc.iniC.userFTPbangnadoe, bc.iniC.passFTPbangnadoe, false);
            var url = txtSendCerturlBangna.Text.Trim();
            String doefielname = "";
            doefielname = txtSendCertCertID.Text.Trim().Replace("555", "") + "_alien_" + txtSendCertalcode.Text.Trim() + ".pdf";
            //MemoryStream streamCertiDoe = ftpc.download(file);
            Boolean chk = ftpc.upload(bc.iniC.folderFTPbangnadoe + "//certificate//" + doefielname, STREAMCertiDOE);
            if (chk)
            {
                //DoeAlienUpdateHealthCheckResult doeRes = new DoeAlienUpdateHealthCheckResult();
                //doeRes.alcode = txtSendCertalcode.Text.Trim();
                //doeRes.cert_id = txtSendCertCertID.Text.Trim();
                //doeRes.alchkhos = "";
                String status = chkSendCertStatus1.Checked ? "1" : chkSendCertStatus2.Checked ? "2" : chkSendCertStatus3.Checked ? "3" : "0";
                //doeRes.alchkdate = txtSendCertvsdate.Text.Trim();
                //doeRes.alchkprovid = "";
                //doeRes.licenseno = txtSendCertdtrcode.Text.Trim();
                //doeRes.chkname = txtSendCertdtrName.Text.Trim();
                //doeRes.chkposition = txtSendCertdtrposition.Text.Trim();
                //doeRes.alchkdesc = txtSendCertdesc.Text.Trim();
                //doeRes.alchkdoc = "";
                //String jsonEpi = JsonConvert.SerializeObject(doeRes);
                using (HttpClient httpClient = new HttpClient())
                {
                    //var content = new StringContent(jsonEpi, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.GetAsync(url + "?cert_id=" + txtSendCertCertID.Text.Trim().Replace("555", "") + "&alcodedoe=" + txtSendCertalcode.Text.Trim()
                        + "&chkstatus=" + status + "&chkdate=" + txtSendCertvsdate.Text.Trim() + "&dtrcode=" + txtSendCertdtrcode.Text.Trim()
                        + "&dtrname=" + txtSendCertdtrName.Text.Trim() + "&position=" + txtSendCertdtrposition.Text.Trim());
                    if (response.IsSuccessStatusCode)
                    {
                        // Handle success
                        //File.Delete(FILENAME);
                        //setGrfPDFFormFolder();
                        String content1 = await response.Content.ReadAsStringAsync();
                        DoeAlientResponse doear = JsonConvert.DeserializeObject<DoeAlientResponse>(content1);
                        if (int.Parse(doear.statuscode)<0)
                        {
                            lbErr.Text = "ไม่สามารถส่งไฟล์ไปยังเซิฟเวอร์ได้ "+ doear.statusdesc;
                            return;
                        }
                        FtpClient ftpd = new FtpClient(bc.iniC.hostFTPCertMeddoe, bc.iniC.userFTPCertMeddoe, bc.iniC.passFTPCertMeddoe, false);
                        ftpd.delete(ftpfilename);
                        
                        bc.bcDB.vsDB.updateVisitStatusDOEbyCertID(txtSendCertCertID.Text.Trim(), "2", "1618");
                        clearControlSendDOE();
                        setGrfPDFFormFTP();
                        lfSbMessage.Text = "Send OK " + response.IsSuccessStatusCode.ToString();
                    }
                    else
                    {
                        // Handle error
                    }
                }
            }
        }
        private async void SendCert()
        {
            if (File.Exists(FILENAME))
            {
                String doefielname = "";
                lfSbMessage.Text = "Send Start remote FTP "+ bc.iniC.folderFTPbangnadoe+" localfile "+ FILENAME;
                doefielname = txtSendCertCertID.Text.Trim().Replace("555", "") + "_alien_" + txtSendCertalcode.Text.Trim()+".pdf";
                FtpClient ftpc = new FtpClient(bc.iniC.hostFTPbangnadoe, bc.iniC.userFTPbangnadoe, bc.iniC.passFTPbangnadoe, false);
                //Boolean chk = ftpc.upload(bc.iniC.folderFTPbangnadoe + "//certificate//" + doefielname, FILENAME);
                Boolean chk = ftpc.upload(bc.iniC.folderFTPbangnadoe + "//certificate//" + doefielname, STREAMCertiDOE);
                if (chk)
                {
                    String err = "";
                    var url = txtSendCerturlBangna.Text.Trim();
                    //Visit vs = new Visit();
                    //vs = bc.bcDB.vsDB.selectbyPreno(HN, VSDATE, PRENO);
                    //if (vs == null) return;     //cert_id       alcodedoe   hostname    chkstatus   chkdate provid  dtrcode dtrname position    chkdesc
                    DoeAlienUpdateHealthCheckResult doeRes = new DoeAlienUpdateHealthCheckResult();
                    doeRes.alcode = txtSendCertalcode.Text.Trim();
                    doeRes.cert_id = txtSendCertCertID.Text.Trim();
                    doeRes.alchkhos = "";
                    String status = chkSendCertStatus1.Checked ? "1" : chkSendCertStatus2.Checked ? "2" : chkSendCertStatus3.Checked ? "3" : "0";
                    doeRes.alchkdate = txtSendCertvsdate.Text.Trim();
                    doeRes.alchkprovid = "";
                    doeRes.licenseno = txtSendCertdtrcode.Text.Trim();
                    doeRes.chkname = txtSendCertdtrName.Text.Trim();
                    doeRes.chkposition = txtSendCertdtrposition.Text.Trim();
                    doeRes.alchkdesc = txtSendCertdesc.Text.Trim();
                    doeRes.alchkdoc = "";
                    String jsonEpi = JsonConvert.SerializeObject(doeRes);
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var content = new StringContent(jsonEpi, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await httpClient.GetAsync(url + "?cert_id=" + txtSendCertCertID.Text.Trim().Replace("555", "") + "&alcodedoe=" + txtSendCertalcode.Text.Trim()
                            + "&chkstatus=" + status + "&chkdate=" + txtSendCertvsdate.Text.Trim() + "&dtrcode=" + txtSendCertdtrcode.Text.Trim()
                            + "&dtrname=" + txtSendCertdtrName.Text.Trim() + "&position=" + txtSendCertdtrposition.Text.Trim());
                        if (response.IsSuccessStatusCode)
                        {
                            // Handle success
                            File.Delete(FILENAME);
                            //setGrfPDFFormFolder();
                            setGrfPDFFormFTP();
                            clearControlSendDOE();
                            bc.bcDB.vsDB.updateVisitStatusDOEbyCertID(txtSendCertCertID.Text.Trim(), "2", "1618");
                            lfSbMessage.Text = "Send OK "+response.IsSuccessStatusCode.ToString();
                        }
                        else
                        {
                            // Handle error
                        }
                    }
                }
            }
        }
        private void CboDoeDeptNew_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            if (cboDoeDeptNew.SelectedItem != null)
            {
                txtDoeDeptNew.Value = ((ComboBoxItem)cboDoeDeptNew.SelectedItem).Value;
            }
        }
        private void TxtDoePaidCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(txtDoePaidCode.Text, out _))//ป้อนเป็น ตัวเลข
                {
                    lbPaidName.Text = bc.bcDB.finM02DB.getPaidName(txtDoePaidCode.Text.Trim());
                }
            }
        }
        private void BtnDoeGet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setDoeAienGrfOPD();
        }

        private void BtnDoeSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //Row rowa = grfVs.Rows.Add();
            //rowa[colvsdate] = txtDoevsdate.Text.Trim();
            DoeAddVisit();
        }
        private Visit DoeAddVisit()
        {
            Visit vs = new Visit();
            String err = "", compcode="", chkcomp="", prefix="", preno="";
            prefix = txtDoeAlprefixen.Text.Trim().Equals("MRS") ? "MRS." : txtDoeAlprefixen.Text.Trim().Equals("MR")?"MR.": txtDoeAlprefixen.Text.Trim().Equals("MISS")?"MISS.": txtDoeAlprefixen.Text.Trim();
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetByPID(txtDoeAlcode.Text.Trim(), "pid");      //check ต่างด้าวให้ เปลี่ยนเป็น mnc_id_no 
            if (ptt.Hn.Length <= 0)
            {       //อยากจะทำ คือ ถ้าเคยมารักษาแล้ว ให้ใช้ HN เก่า
                //return;
            }
            ptt.MNC_HN_NO = "";
            ptt.passport = "";
            ptt.MNC_HN_YR = "";
            ptt.MNC_PFIX_CDT = bc.bcDB.pm02DB.getCodeByName(prefix);
            ptt.MNC_PFIX_CDE = bc.bcDB.pm02DB.getCodeByName(prefix);
            err = "02";
            ptt.MNC_FNAME_T = txtDoeAlnameen.Text.Trim();
            ptt.MNC_LNAME_T = txtDoeAlsnameen.Text.Trim();
            ptt.MNC_FNAME_E = txtDoeAlnameen.Text.Trim();
            ptt.MNC_LNAME_E = txtDoeAlsnameen.Text.Trim();
            ptt.doe_position =txtDoeAlposid.Text.Trim();
            if ((ptt.MNC_FNAME_T.Length <= 0) && (ptt.MNC_FNAME_E.Length <= 0))
            {
                lfSbMessage.Text = "ชื่อ ไม่ถูกต้อง";
                return vs;
            }
            String comp1 = txtDoeEmpname.Text.Trim().Replace("บริษัท", "").Replace("จำกัด", "").Trim();
            chkcomp = bc.bcDB.pm24DB.selectCustByName1(comp1);
            if(chkcomp.Length <= 0)
            {
                chkcomp = bc.bcDB.pm24DB.selectCustByName1(txtDoeEmpname.Text.Trim());
            }
            if (chkcomp.Length <= 0)
            {
                String chkcomp1 = bc.bcDB.pm24DB.selectCustByName1(txtDoeEmpname.Text.Trim().Replace("บริษัท", "").Trim());
                if (chkcomp1.Length <= 0)
                {
                    PatientM24 comp = new PatientM24();
                    comp.MNC_COM_CD = "";
                    comp.MNC_COM_DSC = txtDoeEmpname.Text.Trim().Replace("บริษัท", "").Replace("จำกัด", "").Trim();
                    comp.MNC_COM_TEL = "";
                    comp.MNC_COM_ADD = txtDoeWkaddress.Text.Trim();
                    comp.MNC_COM_NAM = "";
                    comp.email = "";
                    comp.phone2 = "";
                    comp.status_insur = "0";
                    comp.insur1_code = "";
                    comp.insur2_code = "";
                    comp.MNC_COM_STS = "Y";
                    comp.MNC_ATT_NOTE = txtDoeBtname.Text.Trim();
                    String recomp = bc.bcDB.pm24DB.insertCompany(comp, "");
                    chkcomp = bc.bcDB.pm24DB.selectCustByName1(comp.MNC_COM_DSC);
                }
                else
                {
                    chkcomp = chkcomp1;
                }
            }
            String dob = bc.datetoDBCultureInfo(txtDoeAlbdate.Text);
            DateTime.TryParse(dob, out DateTime dob1);
            if (dob1.Year < 1900) { dob1 = dob1.AddYears(543); }
            ptt.MNC_BDAY = dob1.Year.ToString() + "-" + dob1.ToString("MM-dd");
            ptt.patient_birthday = dob1.Year.ToString() + "-" + dob1.ToString("MM-dd");
            String age= ptt.AgeStringOK1DOT();
            if(age.Length>0)
            {
                if (age.IndexOf(".") > 0) ptt.MNC_AGE = age.Substring(0, age.IndexOf("."));
                else ptt.MNC_AGE = age;
            }
            else
            {
                ptt.MNC_AGE = "0";
            }
            ptt.MNC_SEX = txtDoeAlgender.Text.Trim().Equals("1")?"M": txtDoeAlgender.Text.Trim().Equals("2")?"F":"";
            ptt.MNC_SS_NO = txtDoeAlcode.Text.Trim();
            ptt.WorkPermit1 = txtDoeAlcode.Text.Trim();
            ptt.MNC_NAT_CD = txtDoeAlnation.Text.Trim().Equals("M") ? "48" : txtDoeAlnation.Text.Trim().Equals("L") ? "56" : txtDoeAlnation.Text.Trim().Equals("C") ? "57" : txtDoeAlnation.Text.Trim().Equals("V") ? "46" : "97";
            ptt.MNC_ID_NAM = txtDoeAlcode.Text.Trim();
            ptt.MNC_ID_NO = txtDoeAlcode.Text.Trim();
            //ptt.MNC_AGE = "";
            ptt.MNC_ATT_NOTE = "";
            ptt.MNC_CUR_ADD = "";
            ptt.MNC_CUR_MOO = "";
            ptt.MNC_CUR_SOI = "";
            ptt.MNC_CUR_ROAD = "";
            ptt.MNC_CUR_TUM = "";
            ptt.MNC_CUR_AMP = "";
            ptt.MNC_CUR_CHW = "";
            ptt.MNC_CUR_POC = "";
            ptt.MNC_CUR_TEL = "";
            ptt.MNC_DOM_ADD = "";
            ptt.MNC_DOM_MOO = "";
            ptt.MNC_DOM_SOI = "";
            ptt.MNC_DOM_ROAD = "";
            ptt.MNC_DOM_TUM = "";
            ptt.MNC_DOM_AMP = "";
            ptt.MNC_DOM_CHW = "";
            ptt.MNC_DOM_POC = "";
            ptt.MNC_DOM_TEL = "";
            ptt.MNC_COM_CD = chkcomp;           //มีแจ้ง error ว่า save แล้ว บริษัทหาย ได้ลอง debug เช่น aIa ค้นไม่เจอ
            ptt.MNC_COM_CD2 = "";
            ptt.MNC_FN_TYP_CD = txtDoePaidCode.Text.Trim();
            ptt.remark1 = txtDoeEmpname.Text.Trim();
            ptt.remark2 = txtDoeWkaddress.Text.Trim();
            ptt.passport = "";
            ptt.MNC_HN_YR = DateTime.Now.Year.ToString();
            String re = bc.bcDB.pttDB.insertPatientStep1(ptt);
            if (long.TryParse(re, out long chk))
            {
                ptt = bc.bcDB.pttDB.selectPatinetByPID(txtDoeAlcode.Text.Trim(), "work_permit1");
                bc.bcDB.pttDB.updateDoePostion(ptt.Hn, txtDoeAlposid.Text.Trim());
                txtDoeHN.Value = ptt.Hn;
                txtDoevsdate.Value = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd");
                
                vs.HN = txtDoeHN.Text.Trim();
                vs.PaidCode = txtDoePaidCode.Text.Trim();
                vs.symptom = txtDoeSymptom.Text.Trim();
                err = "01";
                vs.DeptCode = txtDoeDeptNew.Text.Trim();
                vs.remark = "doealien";
                err = "02";
                vs.VisitType = "P";//ใน source  Fieldนี้ MNC_PT_FLG Package ให้เป็น P
                vs.DoctorId = bc.iniC.dtrcode;      //IF CboDotCD.TEXT = '' THEN MNC_DOT_CD:= '00000'
                if (vs.DoctorId.Length <= 0) vs.DoctorId = "00000";
                vs.VisitNote = "doealien";
                if (vs.PaidCode.Equals("02"))
                {//สิทธิ เงินสด ให้เอาชื่อบริษัทออก
                    vs.compcode = "";
                    vs.insurcode = "";
                }
                else
                {
                    vs.compcode = "";
                    vs.insurcode = "";
                }
                err = "03";
                //MNC_FIX_DOT_CD := edtDotcd2.TEXT  แพทย์เจ้าของไข้
                vs.DoctorOwn = "";
                vs.status_doe = "1";

                preno = bc.bcDB.vsDB.insertVisit1(vs.HN, vs.PaidCode, vs.symptom, vs.DeptCode, vs.remark, vs.DoctorId, vs.VisitType, vs.compcode, vs.insurcode, "auto");
                if (int.TryParse(preno, out int chk1))
                {
                    vs.preno = preno;
                    DataTable dtvs = bc.bcDB.vsDB.selectByPreno(vs.HN, txtDoevsdate.Text.Trim(), preno);
                    if (dtvs != null)
                    {
                        foreach (DataRow dr in dtvs.Rows)
                        {
                            //และเข้าในว่า ต้องแก้ store procedure แก้ store procedure ต้องแก้โปรแกรมด้วยไหม ต้องหาคำตอบ
                            bc.bcDB.vsDB.updateVisitStatusDOE(vs.HN, txtDoevsdate.Text.Trim(), preno, "1", txtDoeReqcode.Text.Trim(), "1618");
                            txtDoepreno.Value = dr["MNC_PRE_NO"].ToString();
                            Row rowa = grfVs.Rows.Add();
                            rowa[colvsdate] = dr["MNC_DATE"].ToString();
                            rowa[colvshn] = dr["MNC_HN_NO"].ToString();
                            rowa[colvspre] = dr["MNC_PRE_NO"].ToString();
                            rowa[colvsalienname] = dr["MNC_PRE_NO"].ToString();
                            rowa[colvsalcode] = dr["MNC_PRE_NO"].ToString();
                            break;
                        }
                    }
                }
            }
            return vs;
        }
        private void setTheme()
        {

        }
        private void initGrfView()
        {
            grfView = new C1FlexGrid();
            grfView.Font = fEdit;
            grfView.Dock = System.Windows.Forms.DockStyle.Fill;
            grfView.Location = new System.Drawing.Point(0, 0);
            grfView.Rows.Count = 1;
            grfView.Cols.Count = 6;
            grfView.Cols[colviewhn].Width = 80;
            grfView.Cols[colviewname].Width = 80;
            grfView.Cols[colviewvsdate].Width = 80;
            grfView.Cols[colviewpreno].Width = 80;
            grfView.Cols[colviewstatusdoe].Width = 80;

            grfView.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfView.Cols[colviewhn].Caption = "alcode";
            grfView.Cols[colviewname].Caption = "altype";
            grfView.Cols[colpdfpath].Caption = "altype";
            grfView.Cols[colpdfpath].Caption = "altype";
            grfView.Cols[colpdfpath].Caption = "altype";

            //grfOPD.Rows[0].Visible = false;
            //grfOPD.Cols[0].Visible = false;
            grfView.Cols[colpdfname].AllowEditing = false;
            grfView.Cols[colpdfpath].AllowEditing = false;

            grfView.AfterRowColChange += GrfView_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            panel9.Controls.Add(grfView);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfView, bc.iniC.themegrfOpd);
        }

        private void GrfView_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            //setGrfView();
        }
        private void setGrfView()
        {
            showLbLoading();
            lbErr.Text = "";
            DateTime.TryParse(txtDoeView.Value.ToString(), out DateTime datestart);
            if(datestart.Year < 1900)            {                datestart = datestart.AddYears(543);            }
            datestart = datestart.AddHours(7);
            DataTable dt = bc.bcDB.vsDB.selectByDateStatusDoe(datestart.Year+"-"+ datestart.ToString("MM-dd"));
            if(dt.Rows.Count <= 0)
            {
                hideLbLoading();
                return;
            }
            grfView.Rows.Count = 1;
            foreach (DataRow drow in dt.Rows)
            {
                //Console.WriteLine("File: " + Path.GetFileName(file));
                Row rowa = grfView.Rows.Add();
                rowa[colviewhn] = drow["MNC_HN_NO"].ToString();
                rowa[colviewname] = drow["MNC_FNAME_T"].ToString()+" "+ drow["MNC_LNAME_T"].ToString();
                rowa[colviewvsdate] = drow["MNC_DATE"].ToString();
                rowa[colviewpreno] = drow["MNC_PRE_NO"].ToString();
                rowa[colviewstatusdoe] = drow["status_alien_doe"].ToString();
            }
            panel5.Show();
            hideLbLoading();
        }
        private void initGrfExcelVisit()
        {
            grfExcelVisit = new C1FlexGrid();
            grfExcelVisit.Font = fEdit;
            grfExcelVisit.Dock = System.Windows.Forms.DockStyle.Fill;
            grfExcelVisit.Location = new System.Drawing.Point(0, 0);
            grfExcelVisit.Rows.Count = 1;
            grfExcelVisit.Cols.Count = 18;
            grfExcelVisit.Cols[colvsdate].Width = 80;
            grfExcelVisit.Cols[colvshn].Width = 90;
            grfExcelVisit.Cols[colvsalienname].Width = 200;
            grfExcelVisit.Cols[colvsalcode].Width = 100;
            grfExcelVisit.Cols[colvspre].Width = 100;

            grfExcelVisit.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfExcelVisit.Cols[colvsdate].Caption = "date";
            grfExcelVisit.Cols[colvshn].Caption = "hn";
            grfExcelVisit.Cols[colvsalienname].Caption = "name";
            grfExcelVisit.Cols[colvsalcode].Caption = "alcode";
            grfExcelVisit.Cols[colvspre].Caption = "alcode";

            //grfOPD.Rows[0].Visible = false;
            //grfOPD.Cols[0].Visible = false;
            grfExcelVisit.Cols[colvsdate].AllowEditing = false;
            grfExcelVisit.Cols[colvshn].AllowEditing = false;
            grfExcelVisit.Cols[colvsalienname].AllowEditing = false;
            grfExcelVisit.Cols[colvsalcode].AllowEditing = false;
            grfExcelVisit.Cols[colvspre].AllowEditing = false;

            pnExcelVisit.Controls.Add(grfExcelVisit);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfExcelVisit, bc.iniC.themegrfOpd);
        }
        private void initGrfVs()
        {
            grfVs = new C1FlexGrid();
            grfVs.Font = fEdit;
            grfVs.Dock = System.Windows.Forms.DockStyle.Fill;
            grfVs.Location = new System.Drawing.Point(0, 0);
            grfVs.Rows.Count = 1;
            grfVs.Cols.Count = 6;
            grfVs.Cols[colvsdate].Width = 80;
            grfVs.Cols[colvshn].Width = 90;
            grfVs.Cols[colvsalienname].Width = 200;
            grfVs.Cols[colvsalcode].Width = 100;
            grfVs.Cols[colvspre].Width = 100;

            grfVs.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfVs.Cols[colvsdate].Caption = "date";
            grfVs.Cols[colvshn].Caption = "hn";
            grfVs.Cols[colvsalienname].Caption = "name";
            grfVs.Cols[colvsalcode].Caption = "alcode";
            grfVs.Cols[colvspre].Caption = "alcode";

            //grfOPD.Rows[0].Visible = false;
            //grfOPD.Cols[0].Visible = false;
            grfVs.Cols[colvsdate].AllowEditing = false;
            grfVs.Cols[colvshn].AllowEditing = false;
            grfVs.Cols[colvsalienname].AllowEditing = false;
            grfVs.Cols[colvsalcode].AllowEditing = false;
            grfVs.Cols[colvspre].AllowEditing = false;

            grfVs.AfterRowColChange += GrfVs_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            pnVisit.Controls.Add(grfVs);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfVs, bc.iniC.themegrfOpd);
        }
        private void GrfVs_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void initGrfOPD()
        {
            grfOPD = new C1FlexGrid();
            grfOPD.Font = fEdit;
            grfOPD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPD.Location = new System.Drawing.Point(0, 0);
            grfOPD.Rows.Count = 1;
            grfOPD.Cols.Count = 11;
            grfOPD.Cols[colalcode].Width = 80;
            grfOPD.Cols[colaltype].Width = 80;
            grfOPD.Cols[colalprefix].Width = 60;
            grfOPD.Cols[colalprefixen].Width = 100;
            grfOPD.Cols[colalnameen].Width = 100;
            grfOPD.Cols[colalsnamee].Width = 180;
            grfOPD.Cols[colalbdate].Width = 100;
            grfOPD.Cols[colalgender].Width = 50;
            grfOPD.Cols[colalnation].Width = 50;
            grfOPD.Cols[colalposid].Width = 50;
            grfOPD.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfOPD.Cols[colalcode].Caption = "alcode";
            grfOPD.Cols[colaltype].Caption = "altype";
            grfOPD.Cols[colalprefix].Caption = "alprefix";
            grfOPD.Cols[colalprefixen].Caption = "prefixen";
            grfOPD.Cols[colalnameen].Caption = "name";
            grfOPD.Cols[colalsnamee].Caption = "surname";
            grfOPD.Cols[colalbdate].Caption = "dob";
            grfOPD.Cols[colalgender].Caption = "sex";
            grfOPD.Cols[colalnation].Caption = "nation";
            grfOPD.Cols[colalposid].Caption = "position";
            //grfOPD.Rows[0].Visible = false;
            //grfOPD.Cols[0].Visible = false;
            grfOPD.Cols[colalcode].AllowEditing = false;
            grfOPD.Cols[colaltype].AllowEditing = false;
            grfOPD.Cols[colalprefix].AllowEditing = false;
            grfOPD.Cols[colalprefixen].AllowEditing = false;

            grfOPD.Cols[colalnameen].AllowEditing = false;
            grfOPD.Cols[colalsnamee].AllowEditing = false;
            grfOPD.Cols[colalbdate].AllowEditing = false;
            grfOPD.Cols[colalgender].AllowEditing = false;
            grfOPD.Cols[colalnation].AllowEditing = false;
            grfOPD.Cols[colalposid].AllowEditing = false;

            //grfOPD.AfterRowColChange += GrfOPD_AfterRowColChange;
            grfOPD.Click += GrfOPD_Click;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //pnDoeList.Controls.Add(grfOPD);
            pnDoe.Controls.Add(grfOPD);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfOPD, bc.iniC.themegrfOpd);
        }

        private void GrfOPD_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfOPD.Row < 0) return;
            if (grfOPD[grfOPD.Row, colalcode] == null) return;
            setDoeAienGrfOPDClick();
        }
        private void setDoeAienGrfOPDClick()
        {
            showLbLoading();
            try
            {
                String name = grfOPD[grfOPD.Row, colalnameen]!=null? grfOPD[grfOPD.Row, colalnameen].ToString():"";
                name = (name.IndexOf("MRS") == 0) ? name.Replace("MRS", "") : name;
                name = (name.IndexOf("MR")==0) ? name.Replace("MR","") : name;
                name = (name.IndexOf(".") >= 0) ? name.Replace(".", "") : name;
                if (name.IndexOf(".") == 0) name = name.Replace(".", "").Trim();
                txtDoeAlcode.Value = grfOPD[grfOPD.Row, colalcode].ToString();
                txtDoeAltype.Value = grfOPD[grfOPD.Row, colaltype].ToString();
                txtDoeAlprefix.Value = grfOPD[grfOPD.Row, colalprefix].ToString();
                txtDoeAlprefixen.Value = grfOPD[grfOPD.Row, colalprefixen].ToString();
                txtDoeAlnameen.Value = name.Trim();
                txtDoeAlsnameen.Value = grfOPD[grfOPD.Row, colalsnamee].ToString();
                txtDoeAlbdate.Value = grfOPD[grfOPD.Row, colalbdate].ToString();
                txtDoeAlgender.Value = grfOPD[grfOPD.Row, colalgender].ToString();
                txtDoeAlnation.Value = grfOPD[grfOPD.Row, colalnation].ToString();
                txtDoeAlposid.Value = grfOPD[grfOPD.Row, colalposid].ToString();
                lbDoeAltypeName.Text = grfOPD[grfOPD.Row, colaltype].ToString().Equals("1") ? "ขึ้นทะเบียนคนต่างด้าวผิดกฏหมาย" : grfOPD[grfOPD.Row, colaltype].ToString().Equals("2") ? "ต่ออายุคนต่างด้าว" : "-";
                lbDoeAlgenderName.Text = grfOPD[grfOPD.Row, colalgender].ToString().Equals("1") ? "ชาย" : grfOPD[grfOPD.Row, colalgender].ToString().Equals("2") ? "หญิง" : "-";
                lbDoeAlposidName.Text = grfOPD[grfOPD.Row, colalposid].ToString().Equals("1") ? "กรรมกร" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("2") ? "ผู้รับใช้ในบ้าน" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("3") ? "ช่างเครื่องยนต์ในเรือประมงทะเล" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("4") ? " ผู้ประสานงานด้านภาษากัมพูชา ลาว เมียนมา หรือเวียดนาม" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("5") ? "งานขายของหน้าร้าน"
                    : grfOPD[grfOPD.Row, colalposid].ToString().Equals("6") ? "งานกสิกรรม" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("7") ? "งานเลี้ยงสัตว์" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("8") ? "งานป่าไม้" :
                    grfOPD[grfOPD.Row, colalposid].ToString().Equals("9") ? "งานประมง" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("10") ? "งานช่างก่ออิฐ" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("11") ? "งานช่างไม้" :
                    grfOPD[grfOPD.Row, colalposid].ToString().Equals("12") ? "งานช่างก่อสร้างอาคาร" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("13") ? "งานทำที่นอนหรือผ้าห่มนวม" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("14") ? "งานทำมีด" :
                    grfOPD[grfOPD.Row, colalposid].ToString().Equals("15") ? "งานทำรองเท้า" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("16") ? "งานทำหมวก" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("17") ? "งานประดิษฐ์เครื่องแต่งกาย" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("18") ? "งานปั้นหรือทำเครื่องปั้นดินเผา" : "-";
                lbDoeAlnationName.Text = grfOPD[grfOPD.Row, colalnation].ToString().Equals("M") ? "เมียนมา" : grfOPD[grfOPD.Row, colalnation].ToString().Equals("L") ? "ลาว" : grfOPD[grfOPD.Row, colalnation].ToString().Equals("C") ? "กัมพูชา" : grfOPD[grfOPD.Row, colalnation].ToString().Equals("V") ? "เวียดนาม" : "-";
            }
            catch (Exception ex)
            {
                //MessageBox.Show("error " + ex.Message, "");
                lfSbMessage.Text = ex.Message;
            }
            hideLbLoading();
        }
        private void GrfOPD_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            //if (grfOPD.Row < 0) return;
            //if (grfOPD[grfOPD.Row, colalcode] == null) return;
            //try
            //{
            //    txtDoeAlcode.Value = grfOPD[grfOPD.Row, colalcode].ToString();
            //    txtDoeAltype.Value = grfOPD[grfOPD.Row, colaltype].ToString();
            //    txtDoeAlprefix.Value = grfOPD[grfOPD.Row, colalprefix].ToString();
            //    txtDoeAlprefixen.Value = grfOPD[grfOPD.Row, colalprefixen].ToString();
            //    txtDoeAlnameen.Value = grfOPD[grfOPD.Row, colalnameen].ToString();
            //    txtDoeAlsnameen.Value = grfOPD[grfOPD.Row, colalsnamee].ToString();
            //    txtDoeAlbdate.Value = grfOPD[grfOPD.Row, colalbdate].ToString();
            //    txtDoeAlgender.Value = grfOPD[grfOPD.Row, colalgender].ToString();
            //    txtDoeAlnation.Value = grfOPD[grfOPD.Row, colalnation].ToString();
            //    txtDoeAlposid.Value = grfOPD[grfOPD.Row, colalposid].ToString();
            //    lbDoeAltypeName.Text = grfOPD[grfOPD.Row, colaltype].ToString().Equals("1") ? "ขึ้นทะเบียนคนต่างด้าวผิดกฏหมาย" : grfOPD[grfOPD.Row, colaltype].ToString().Equals("2")? "ต่ออายุคนต่างด้าว" : "-";
            //    lbDoeAlgenderName.Text = grfOPD[grfOPD.Row, colalgender].ToString().Equals("1") ? "ชาย" : grfOPD[grfOPD.Row, colalgender].ToString().Equals("2") ? "หญิง" : "-";
            //    lbDoeAlposidName.Text = grfOPD[grfOPD.Row, colalposid].ToString().Equals("1") ? "กรรมกร" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("2") ? "ผู้รับใช้ในบ้าน" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("3") ? "ช่างเครื่องยนต์ในเรือประมงทะเล" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("4") ? " ผู้ประสานงานด้านภาษากัมพูชา ลาว เมียนมา หรือเวียดนาม" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("5") ? "งานขายของหน้าร้าน" 
            //        : grfOPD[grfOPD.Row, colalposid].ToString().Equals("6") ? "งานกสิกรรม" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("7") ? "งานเลี้ยงสัตว์" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("8") ? "งานป่าไม้" :
            //        grfOPD[grfOPD.Row, colalposid].ToString().Equals("9") ? "งานประมง" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("10") ? "งานช่างก่ออิฐ" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("11") ? "งานช่างไม้" : 
            //        grfOPD[grfOPD.Row, colalposid].ToString().Equals("12") ? "งานช่างก่อสร้างอาคาร" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("13") ? "งานทำที่นอนหรือผ้าห่มนวม" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("14") ? "งานทำมีด" :
            //        grfOPD[grfOPD.Row, colalposid].ToString().Equals("15") ? "งานทำรองเท้า" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("16") ? "งานทำหมวก" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("17") ? "งานประดิษฐ์เครื่องแต่งกาย" : grfOPD[grfOPD.Row, colalposid].ToString().Equals("18") ? "งานปั้นหรือทำเครื่องปั้นดินเผา" : "-";
            //    lbDoeAlnationName.Text = grfOPD[grfOPD.Row, colalnation].ToString().Equals("M") ? "เมียนมา" : grfOPD[grfOPD.Row, colalnation].ToString().Equals("L") ? "ลาว" : grfOPD[grfOPD.Row, colalnation].ToString().Equals("C") ? "กัมพูชา" : grfOPD[grfOPD.Row, colalnation].ToString().Equals("V") ? "เวียดนาม" : "-";
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show("error " + ex.Message, "");
            //}
        }
        private async void setDoeAienGrfOPD()
        {
            grfOPD.DataSource = null;
            grfOPD.Rows.Count = 1;
            //if ((grfExcel!=null)&&(grfExcel.Rows.Count > 0)) grfExcel.Rows.Count = 0;
            DoeAlienRequest doea = new DoeAlienRequest();
            doea.reqcode = txtDoeReqcode.Text.Trim();
            doea.alcode = txtDoeAlcode.Text.Trim();
            if(txtDoeReqcode.Text.Trim().Length<=0 && txtDoeAlcode.Text.Trim().Length <= 0)
            {
                return;
            }
            doea.token = txtDoetoken.Text.Trim();
            var url = txtDoeURLbangna.Text.Trim();
            String jsonEpi = JsonConvert.SerializeObject(doea, Formatting.Indented);
            jsonEpi = jsonEpi.Replace("[" + Environment.NewLine + "    null" + Environment.NewLine + "  ]", "[]");
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    //var content = new StringContent(jsonEpi, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.GetAsync(url + "?regcode=" + txtDoeReqcode.Text.Trim() + "&alcode=" + txtDoeAlcode.Text.Trim());
                    if (response.StatusCode.ToString().ToUpper().Equals("OK"))
                    {
                        String content1 = await response.Content.ReadAsStringAsync();
                        DoeAlientResponse doear = JsonConvert.DeserializeObject<DoeAlientResponse>(content1);
                        if (doear.statuscode.Equals("-100")) return;
                        txtDoeEmpname.Value = doear.empname;
                        txtDoeWkaddress.Value = doear.wkaddress;
                        //txtDoeReqcode.Value = doear.reqcode;
                        txtDoeBtname.Value = doear.btname;
                        grfOPD.Rows.Count = doear.alienlist.Count + 1;
                        int i = 1, j = 1;
                        foreach (var alien in doear.alienlist)
                        {
                            Row rowa = grfOPD.Rows[i];
                            rowa[colalcode] = alien.alcode;
                            rowa[colaltype] = alien.altype;
                            rowa[colalprefix] = alien.alprefix;
                            rowa[colalprefixen] = alien.alprefixen;
                            rowa[colalnameen] = alien.alnameen;
                            rowa[colalsnamee] = alien.alsnameen;
                            rowa[colalbdate] = alien.albdate;
                            rowa[colalgender] = alien.algender;
                            rowa[colalnation] = alien.alnation;
                            rowa[colalposid] = alien.alposid;

                            rowa[0] = i.ToString();
                            i++;
                            //Console.WriteLine((alienlist)alien.alcode);
                        }
                        //Console.WriteLine(content);
                    }
                    else
                    {
                        //MessageBox.Show("error send " + result.StatusCode, "");
                    }
                }
                txtDoeHN.Value = "";
                txtDoepreno.Value = "";
                c1SplitterPanel3.SizeRatio = 0;
            }
            catch(Exception ex)
            {
                //MessageBox.Show("error " + ex.Message, "");
            }
        }
        private void setControlSendDOE(String certid)
        {
            //new LogWriter("d", "setControlSendDOE 00 ");
            Visit vs = new Visit();
            if (!FLAGTABDOE.Equals("formfolder"))
            {
                vs = bc.bcDB.vsDB.selectbyPreno(HN, VSDATE, PRENO);
                panel5.Hide();
            }
            else
            {
                vs = bc.bcDB.vsDB.selectbyCertID(certid);
                HN = vs.HN;
                VSDATE = vs.VisitDate;
                PRENO = vs.preno;
            }
            //new LogWriter("d", "setControlSendDOE 01 " );
            Patient ptt = bc.bcDB.pttDB.selectPatinetByHn(vs.HN);
            //txtSendCerthostname.Value = bc.iniC.hostname;
            txtSendCerthn.Value = HN;
            txtSendCertvsdate.Value = bc.datetoShow1(VSDATE);
            txtSendCertCertID.Value = vs.certi_id;
            txtSendCertalcode.Value = vs.ID;
            //txtSendCertprovcode.Text = bc.iniC.provcode;
            txtSendCerturlBangna.Text = bc.iniC.urlbangnadoeresult;
            txtSendCertdtrcode.Value = vs.MNC_DOT_CD;
            txtSendCertdtrName.Text = bc.selectDoctorName(vs.MNC_DOT_CD);
            //new LogWriter("d", "setControlSendDOE 02 ");
            lbSendCertPttName.Text = vs.PatientName;
            lfSbMessage.Text = "";
            lbSendCertNat.Text = bc.bcDB.pm04DB.getNationName(ptt.MNC_NAT_CD);
            txtSendCertCertID.Focus();
            panel10.Controls.Clear();
        }
        private void clearControlSendDOE()
        {
            txtSendCerthn.Value = "";
            txtSendCertvsdate.Value = "";
            txtSendCertCertID.Value = "";
            txtSendCertalcode.Value = "";
            //txtSendCertprovcode.Text = bc.iniC.provcode;
            txtSendCerturlBangna.Text = bc.iniC.urlbangnadoeresult;
            txtSendCertdtrcode.Value = "";
            txtSendCertdtrName.Text = "";
            lbSendCertPttName.Text = "";
            panel3.Controls.Clear();
        }
        private void initGrfPDF()
        {
            grfPDF = new C1FlexGrid();
            grfPDF.Font = fEdit;
            grfPDF.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPDF.Location = new System.Drawing.Point(0, 0);
            grfPDF.Rows.Count = 1;
            grfPDF.Cols.Count = 3;
            grfPDF.Cols[colpdfname].Width = 200;
            grfPDF.Cols[colpdfpath].Width = 90;
            
            grfPDF.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfPDF.Cols[colpdfname].Caption = "file name";
            grfPDF.Cols[colpdfpath].Caption = "hn";

            grfPDF.Cols[colpdfname].AllowEditing = false;
            grfPDF.Cols[colpdfpath].Visible = true;

            //grfPDF.AfterRowColChange += GrfPDF_AfterRowColChange;
            grfPDF.Click += GrfPDF_Click;
            panel5.Controls.Add(grfPDF);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfPDF, bc.iniC.themegrfOpd);
        }

        private void GrfPDF_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfPDF.Row < 0) return;
            if (grfPDF[grfPDF.Row, colpdfpath] == null) return;
            showLbLoading();
            
            if(bc.iniC.hostname.Equals("โรงพยาบาล บางนา2"))//เพราะ FTP ตอนสร้าง จะมี Folder ย่อย
            {
                lfSbMessage.Text = "cert_doe/" + grfPDF[grfPDF.Row, colpdfpath].ToString();
                showPDF("cert_doe/" + grfPDF[grfPDF.Row, colpdfpath].ToString());
                FILENAME = "cert_doe/" + grfPDF[grfPDF.Row, colpdfpath].ToString();
            }
            else
            {
                lfSbMessage.Text = grfPDF[grfPDF.Row, colpdfpath].ToString();
                showPDF(grfPDF[grfPDF.Row, colpdfpath].ToString());
                FILENAME = grfPDF[grfPDF.Row, colpdfpath].ToString();
            }
            hideLbLoading();
        }
        private void setGrfPDFFormFTP()
        {
            showLbLoading();
            lbErr.Text = bc.iniC.pathdoealiencert;
            int i = 0;
            FtpClient ftpc = new FtpClient(bc.iniC.hostFTPCertMeddoe, bc.iniC.userFTPCertMeddoe, bc.iniC.passFTPCertMeddoe, false);
            List<String> listFile = ftpc.directoryList(bc.iniC.folderFTPCertMeddoe);
            grfPDF.Rows.Count = 1;
            foreach (string file in listFile)
            {
                Console.WriteLine("File: " + Path.GetFileName(file));
                Row rowa = grfPDF.Rows.Add();
                rowa[colpdfname] = file;
                rowa[colpdfpath] = file;
                i++;
                rowa[0] = i;
            }
            panel5.Show();
            hideLbLoading();
        }
        private void setGrfPDFFormFolder()
        {
            showLbLoading();
            lbErr.Text = bc.iniC.pathdoealiencert;
            String folderPath = bc.iniC.pathdoealiencert;
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            string[] files = Directory.GetFiles(folderPath);
            grfPDF.Rows.Count = 1;
            foreach (string file in files)
            {
                Console.WriteLine("File: " + Path.GetFileName(file));
                FileInfo fi = new FileInfo(file);
                String[] filename = fi.FullName.Replace(fi.DirectoryName, "").Replace("\\", "").Replace(fi.Extension, "").Split('_');
                if (filename.Length > 1)
                {
                    Row rowa = grfPDF.Rows.Add();
                    rowa[colpdfname] = Path.GetFileName(filename[1]);
                    rowa[colpdfpath] = fi.FullName;
                }
            }
            panel5.Show();
            hideLbLoading();
        }
        private void FrmDoeAlien_Load(object sender, EventArgs e)
        {
            System.Drawing.Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            tabMain.ShowTabs = false;
            c1SplitContainer1.HeaderHeight = 0;
            spDoeList.HeaderHeight = 0;
            c1SplitterPanel3.SizeRatio = 0;
            //spExcelMain.HeaderHeight = 0;
            if (FLAGTABDOE.Equals("namelist"))
            {//ดึงข้อมูลจาก DOE
                tabMain.SelectedTab = tabGetNameList;
                tabGetNameList.TabVisible = true;
                tabSendCert.TabVisible = false;
                tabDoeView.TabVisible = false;
                tabExcel.TabVisible = false;
                setDoeAienGrfOPD();
            }
            else if (FLAGTABDOE.Equals("formfolder"))
            {//ดึง PDF จาก folder เพื่อดูว่ามี pdf ที่ยังไม่ได้ส่ง และทำการส่ง
                tabMain.SelectedTab = tabSendCert;
                tabSendCert.TabVisible = true;
                tabGetNameList.TabVisible = false;
                tabDoeView.TabVisible = false;
                tabExcel.TabVisible = false;
                setGrfPDFFormFTP();
            }
            else if (FLAGTABDOE.Equals("viewdoe"))
            {//ดึง PDF จาก folder เพื่อดูว่ามี pdf ที่ยังไม่ได้ส่ง และทำการส่ง
                tabMain.SelectedTab = tabDoeView;
                tabDoeView.TabVisible = true;
                tabGetNameList.TabVisible = false;
                tabSendCert.TabVisible = false;
                tabExcel.TabVisible = false;
                setGrfPDFFormFTP();
            }
            else if (FLAGTABDOE.Equals("excel"))
            {
                tabMain.SelectedTab = tabExcel;
                tabDoeView.TabVisible = false;
                tabGetNameList.TabVisible = false;
                tabSendCert.TabVisible = false;
                tabExcel.TabVisible = true;
                setGrfPDFFormFTP();
            }
            else
            {//ส่ง PDF จากการเลือกหน้า OPD โดยมี HN, cert_id เลือกมาเรียบร้อย
                tabGetNameList.TabVisible = true;
                tabDoeView.TabVisible = true;
                tabSendCert.TabVisible = true;
                tabExcel.TabVisible = true;
                tabMain.SelectedTab = tabSendCert;
                setControlSendDOE("");
            }
            
        }
    }
}
