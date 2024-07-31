using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.BarCode;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1Ribbon;
using C1.Win.C1Themes;
using GrapeCity.ActiveReports.Document.Section;
using GrapeCity.ActiveReports.Extensibility.Data.SchemaModel;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Ocsp;
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
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using Column = C1.Win.C1FlexGrid.Column;

namespace bangna_hospital.gui
{
    public partial class FrmScreenCapture : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famt1, famt5, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        C1PictureBox picScr;
        C1FlexGrid grfView, grfDownload, grfHn, grfDrugAllergy, grfChronic;
        C1List listView;
        private System.IO.FileSystemWatcher m_Watcher;
        List<String> lFile, lFilePrint;
        Patient ptt;
        MemoryStream streamPrint = null;
        int TIMERCNT = 0;
        int colUploadId = 1, colUploadName = 2, colUploadImg = 3, colUploadPath = 4, screenWidth = 0, screenHeight = 0, cntPrint = 0, formwidth = 0;
        int colHnHn = 1, colHnName = 2, colHnVn = 3, colHnVsDate = 4, colHnPreno = 5, colHnWrdNo = 6, colHnRoom = 7, colHnBed = 9, colHnSymptoms = 10;
        Panel pnMain;
        Panel pnTop = new Panel();
        Panel pnBotom = new Panel();
        C1PictureBox picTop, picLeft, picRight;
        Form frmImg;
        //private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Panel pnBotton;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.Panel pnLeft;
        C1BarCode qrcode;

        MedicalCertificate mcerti;
        DocScan dsc;
        Timer timer1;
        Boolean pageLoad = false, statusAutoSend = false, SoundPlay = false;
        Image imgPrint;
        String LINEERR = "", DEPTNO = "",HN = "", PRENO="", VSDATE="", DTRCODE="", CERTID="",AN="", ANPRNLAB="",ERRLINE="", REQNOPRNLAB="";
        Patient PTT;
        C1ThemeController theme1;
        SoundPlayer player;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmScreenCapture(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            formwidth = this.Width;
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            initFont();

            pnMain = new Panel();
            picTop = new C1PictureBox();
            picLeft = new C1PictureBox();
            picRight = new C1PictureBox();
            screenWidth = Screen.PrimaryScreen.Bounds.Width;
            screenHeight = Screen.PrimaryScreen.Bounds.Height;

            lFile = new List<string>();
            lFilePrint = new List<string>();

            timer1 = new Timer();
            timer1.Interval = 5000;
            timer1.Tick += Timer1_Tick;
            timer1.Enabled = true;
            theme1 = new C1ThemeController();
            PTT = new Patient();
            picScr = new C1PictureBox();
            picScr.Location = new System.Drawing.Point(0, 0);
            picScr.Dock = DockStyle.Fill;
            picScr.Name = "picScr";
            //picScr.Size = new System.Drawing.Size(screenWidth / 2, screenHeight);
            //picScr.Image = Resources.screen_first_l;
            picScr.SizeMode = PictureBoxSizeMode.StretchImage;
            pnPic.Controls.Add(picScr);
            player = new SoundPlayer();
            player.SoundLocation = "livechat-129007.wav";
            player.Load();
            qrcode = new C1BarCode();
            qrcode.ForeColor = System.Drawing.Color.Black;
            qrcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);

            this.Activated += FrmScreenCapture_Activated;
            this.FormClosed += FrmScreenCapture_FormClosed;
            txtHn.KeyUp += TxtHn_KeyUp;
            chkView.Click += ChkView_Click;
            chkUpload.Click += ChkUpload_Click;
            btnSearch.Click += BtnSearch_Click;
            txtCertID.KeyUp += TxtCertID_KeyUp;
            chkOPD.Click += ChkOPD_Click;
            chkIPD.Click += ChkIPD_Click;
            cboDept.SelectedIndexChanged += CboDept_SelectedIndexChanged;
            txtDtrCode.KeyUp += TxtDtrCode_KeyUp;
            btnPrint.Click += BtnPrint_Click;

            chkUpload.Checked = true;

            initGrfView();
            initGrfHn();
            initGrfDrugAllergy();
            if (bc.iniC.statusStation.Equals("IPD")) chkIPD.Checked = true;
            else chkOPD.Checked = true;
            if (chkIPD.Checked)
            {
                String[] deptno = bc.iniC.station.Split(',');
                foreach (String deptno1 in deptno)
                {
                    bc.bcDB.pttDB.setCboDeptIPDWdNo(cboDept, deptno1);
                    setGrfHn(deptno1);
                    break;
                }
            }
            else bc.bcDB.pttDB.setCboDeptOPD(cboDept, bc.iniC.station);

            //FrmScreenCapturePrintMulti frm = new FrmScreenCapturePrintMulti();
            //frm.Show(this);
            //initPrintMulti();
            rr1.Text = bc.iniC.pathScreenCaptureUpload;

            if (bc.iniC.statusScreenCaptureAutoSend.Equals("1"))
            {
                chkAutoSend.Checked = true;
                chkView.Checked = false;
                chkUpload.Checked = false;
            }

            //MessageBox.Show("args "+bc.hn, "");
            //pnMain.Hide();
            //this.Width = this.Width - int.Parse(bc.iniC.imggridscanwidth);
            //this.Width = formwidth + int.Parse(bc.iniC.imggridscanwidth);
            pageLoad = false;
        }
        private void initFont()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            famt = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            famt1 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 1, FontStyle.Regular);
            famt5 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
            famt7 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Regular);
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
        private void playSoundStop()
        {
            player.Stop();
            SoundPlay = false;
            //player.Play();
        }
        private void playSound()
        {
            SoundPlay = true;
            player.Play();      //play one time
            //player.Play();
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtPrnHN.Text.Length > 0)
            {
                printOrderSheet();
            }
            else
            {
                rl1.Text = "ไม่พบรหัสแพทย์";
            }
        }

        private void TxtDtrCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                lbDtrName.Text = bc.selectDoctorName(txtDtrCode.Text.Trim());
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TIMERCNT++;
            if (bc.iniC.statusScreenCaptureAutoSend.Equals("1") && chkAutoSend.Checked)
            {
                timer1.Stop();
                sendFTPCertMed();
                getListFile();
                timer1.Start();
            }
            else
            {
                getListFile();
            }
            if (TIMERCNT % 4 == 0)
            {
                if (bc.iniC.statusAutoPrintLabResult.Equals("1"))
                {
                    timer1.Stop();
                    try
                    {
                        printLabResult();
                    }
                    catch (Exception ex)
                    {
                        new LogWriter("e", "FrmScreenCapture Timer1_Tick printLabResult errline "+ERRLINE+ " ex.Message" + ex.Message);
                    }
                    timer1.Start();
                }
                TIMERCNT = 0;
            }
        }
        private void printLabResult()
        {
            Patient ptt = new Patient();
            DataTable dtRes = new DataTable();
            DataTable dtReq = new DataTable();
            String[] deptno = bc.iniC.station.Split(',');
            ERRLINE = "";
            foreach (String deptno1 in deptno)
            {
                dtReq = bc.bcDB.labT05DB.selectRequestLabNotPrnbyDeptNo(deptno1);   //ต้องดึง lab_t01.mnc_req_sts = 'O' เพราะใน lab_t02 mnc_req_sts มีทั้ง O, Q
                if (dtReq.Rows.Count > 0)
                {
                    ERRLINE = "01";
                    String reqdate = "", hn = "", depname = "", vnno = "";
                    reqdate = dtReq.Rows[0]["MNC_REQ_DAT"].ToString();
                    REQNOPRNLAB = dtReq.Rows[0]["MNC_REQ_NO"].ToString();
                    hn = dtReq.Rows[0]["MNC_HN_NO"].ToString();
                    ANPRNLAB = dtReq.Rows[0]["MNC_AN_NO"].ToString() + "." + dtReq.Rows[0]["MNC_AN_YR"].ToString();
                    vnno = dtReq.Rows[0]["MNC_VN_NO"].ToString() + "." + dtReq.Rows[0]["MNC_VN_SEQ"].ToString() + "." + dtReq.Rows[0]["MNC_VN_SUM"].ToString();
                    dtRes = bc.bcDB.labT05DB.selectResultbyReqNo(reqdate, REQNOPRNLAB);
                    if (dtRes.Rows.Count > 0)
                    {
                        ERRLINE = "02";
                        ptt = bc.bcDB.pttDB.selectPatinetByHn(hn);
                        dtRes.Columns.Add("patient_name", typeof(String));
                        dtRes.Columns.Add("patient_hn", typeof(String));
                        dtRes.Columns.Add("patient_age", typeof(String));
                        dtRes.Columns.Add("request_no", typeof(String));
                        dtRes.Columns.Add("patient_vn", typeof(String));
                        dtRes.Columns.Add("doctor", typeof(String));
                        dtRes.Columns.Add("result_date", typeof(String));
                        dtRes.Columns.Add("print_date", typeof(String));
                        dtRes.Columns.Add("patient_dep", typeof(String));
                        dtRes.Columns.Add("patient_company", typeof(String));
                        //dt.Columns.Add("ptt_department", typeof(String));
                        dtRes.Columns.Add("patient_type", typeof(String));
                        //dtRes.Columns.Add("mnc_lb_dsc", typeof(String));
                        //dtRes.Columns.Add("mnc_lb_grp_cd", typeof(String));
                        dtRes.Columns.Add("sort1", typeof(String));
                        //dtRes.Columns.Add("hostname", typeof(String));
                        ERRLINE = "03";
                        Boolean chkresultnull=false;
                        foreach (DataRow drow in dtRes.Rows)
                        {
                            //drow["patient_age"] = ptt.AgeStringOK1DOT();
                            drow["patient_age"] = ptt.AgeStringOK1();
                            depname = dtRes.Rows[0]["MNC_REQ_DEP"].ToString();
                            drow["patient_name"] = ptt.Name;
                            drow["patient_hn"] = ptt.Hn;
                            drow["patient_company"] = "";           //ไม่ต้องพิมพ์ ชื่อบริษัท ลองดู
                            ERRLINE = "031";
                            drow["patient_vn"] = bc.iniC.statusStation.Equals("OPD") ? vnno : ANPRNLAB;
                            ERRLINE = "0310";
                            new LogWriter("d", "FrmScreenCapture printLabResult hn " + hn + " reqdate " + reqdate + " REQNOPRNLAB " + REQNOPRNLAB);
                            if(!chkresultnull) chkresultnull = drow["MNC_RES_VALUE"] == null ? true : false;        // check เพราะ มีค่าว่าง เมื่อ วันเวลา status labt02.mnc_req_sts = 'O' ค่าผล ก็ยังว่าง  ปล่อยให้พิมพ์ แต่ไม่ต้อง update status status_print_result_no
                            else if (!chkresultnull) chkresultnull = drow["MNC_RES_VALUE"].ToString().Equals("") ? true : false;        // check เพราะ มีค่าว่าง เมื่อ วันเวลา status labt02.mnc_req_sts = 'O' ค่าผล ก็ยังว่าง  ปล่อยให้พิมพ์ แต่ไม่ต้อง update status status_print_result_no
                            new LogWriter("d", "FrmScreenCapture printLabResult drow['mnc_lb_res'] " + drow["mnc_lb_res"] + " drow['mnc_lb_res'].ToString() " + drow["mnc_lb_res"].ToString());
                            ERRLINE = "032";
                            drow["patient_type"] = dtRes.Rows[0]["MNC_FN_TYP_DSC"].ToString();
                            drow["request_no"] = drow["MNC_REQ_NO"].ToString() + "/" + bc.datetoShow(drow["mnc_req_dat"].ToString());
                            ERRLINE = "033";
                            drow["doctor"] = dtRes.Rows[0]["dtr_name"].ToString() + "[" + dtRes.Rows[0]["mnc_dot_cd"].ToString() + "]";
                            ERRLINE = "04";
                            drow["result_date"] = bc.datetoShow(dtRes.Rows[0]["mnc_req_dat"].ToString());
                            drow["print_date"] = bc.datetoShow(dtRes.Rows[0]["MNC_RESULT_DAT"].ToString()) + " " + bc.FormatTime(dtRes.Rows[0]["MNC_RESULT_TIM"].ToString());
                            drow["user_lab"] = drow["user_lab"].ToString() + " [ทน." + drow["MNC_USR_NAME_result"].ToString() + "]";
                            drow["user_report"] = drow["user_report"].ToString() + " [ทน." + drow["MNC_USR_NAME_report"].ToString() + "]";
                            drow["user_check"] = drow["user_check"].ToString() + " [ทน." + drow["MNC_USR_NAME_approve"].ToString() + "]";
                            drow["patient_dep"] = depname.Equals("101") ? "OPD1" : depname.Equals("107") ? "OPD2" : depname.Equals("103") ? "OPD3" :
                            depname.Equals("104") ? "ER" : depname.Equals("106") ? "WARD6" : depname.Equals("108") ? "WARD5W" : depname.Equals("109") ? "ล้างไต" :
                                depname.Equals("105") ? "WARD5M" : depname.Equals("113") ? "ICU" : depname.Equals("114") ? "NS/LR" : depname.Equals("115") ? "ทันตกรรม" : depname.Equals("116") ? "CCU" : depname;
                            ERRLINE = "05";
                            drow["mnc_lb_dsc"] = drow["MNC_LB_DSC"].ToString();
                            drow["mnc_lb_grp_cd"] = drow["MNC_LB_TYP_DSC"].ToString();
                            drow["hostname"] = bc.iniC.hostname;
                            //if (drow["MNC_RES_VALUE"].ToString().Equals("-"))                                drow["MNC_RES_UNT"] = "";
                            drow["MNC_RES_UNT"] = drow["MNC_RES_UNT"].ToString().Replace("0.00-0.00", "").Replace("0.00 - 0.00", "").Replace("0.00", "");
                            drow["sort1"] = drow["mnc_req_dat"].ToString().Replace("-", "").Replace("-", "") + drow["MNC_REQ_NO"].ToString();
                            //drow["MNC_RES"]=ชื่อlabลูก
                            //drow["mnc_lb_res"]REFERRENCE RANGE
                            //drow["mnc_lb_res"] = drow["mnc_lb_res"].ToString().Replace(drow["MNC_RES"].ToString(), "");      //lab ต้องการให้แสดงค่าตัวเลข
                            drow["mnc_lb_res"] = bc.fixLabRef(drow["MNC_LB_CD"].ToString(), drow["MNC_RES"].ToString(), drow["mnc_lb_res"].ToString(), drow["MNC_RES"].ToString());      // REFERRENCE RANGE
                            drow["MNC_RES_UNT"] = bc.fixLabUnit(drow["MNC_LB_CD"].ToString(), drow["MNC_RES"].ToString().Trim(), drow["MNC_RES_UNT"].ToString().Trim(), drow["mnc_lb_res"].ToString());
                            if (drow["mnc_lb_res"].Equals("0 - 0"))
                            {
                                drow["mnc_lb_res"] = drow["MNC_RES_UNT"].ToString();
                                drow["MNC_RES_UNT"] = "";
                            }
                            ERRLINE = "06";
                        }
                        //printerA5
                        playSound();
                        rr1.Text = bc.iniC.printerA5;
                        SetDefaultPrinter(bc.iniC.printerA5);
                        FrmReportNew frm = new FrmReportNew(bc, "lab_result_4");
                        frm.DT = dtRes;
                        if (bc.iniC.statusPrintPreview.Equals("1"))                            frm.ShowDialog(this);                        
                        else                            frm.PrintReport();                       
                        frm.Dispose();
                        bc.bcDB.labT01DB.updateStatusPrintResult(REQNOPRNLAB, reqdate);
                        new LogWriter("d", "FrmScreenCapture printLabResult  " + ptt.Hn+" an "+ ANPRNLAB);
                    }
                }
            }
            dtRes.Dispose();
            dtReq.Dispose();
        }
        private void CboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            String deptid = "";
            deptid = ((ComboBoxItem)cboDept.SelectedItem).Value;
            setGrfHn(deptid);
        }
        private void ChkIPD_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pageLoad = true;
            cboDept.Clear();
            cboDept.Items.Clear();
            bc.bcDB.pttDB.setCboDeptIPDWdNo(cboDept, bc.iniC.station);
            pageLoad = false;
        }

        private void ChkOPD_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pageLoad = true;
            cboDept.Clear();
            cboDept.Items.Clear();
            bc.bcDB.pttDB.setCboDeptOPD(cboDept, bc.iniC.station);
            pageLoad = false;
        }
        private void printOrderSheet()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = bc.iniC.printerLabOut;     //ต้องใช้อันนี้ เพราะ printerA4 ถูกใช้ไปแล้ว drugin

                PageSettings pg = new System.Drawing.Printing.PageSettings();
                pg.PaperSize = new PaperSize("A4", 827, 1169);
                pd.DefaultPageSettings = pg;
                pd.DefaultPageSettings.Landscape = false;
                pd.PrintPage += Pd_PrintPage;
                pd.Print();
                pd.Dispose();
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmScreenCapture printOrderSheet ex.Message " + ex.Message);
            }
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            float yPos = 10, ydate = 0, gapline = 17, col1 = 10, col2 = 60, col4 = 200;
            int recx = 15, recy = 15;
            Graphics g = e.Graphics;
            Pen blackPen = new Pen(Color.Black, 1);
            Image logo;
            logo = Resources.LOGO_BW_tran;
            SolidBrush Brush = new SolidBrush(Color.Black);
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
            float newWidth = logo.Width * 100 / logo.HorizontalResolution;
            float newHeight = logo.Height * 100 / logo.VerticalResolution;

            float widthFactor = 5.8F;
            float heightFactor = 5.8F;
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

            DocScan dsc = new DocScan();
            //new LogWriter("d", "BtnUpload_Click dsc.vn " + dsc.vn + " dsc.an " + dsc.an);
            dsc.active = "1";
            dsc.doc_scan_id = "";
            dsc.doc_group_id = "1100000007";
            dsc.hn = txtPrnHN.Text;

            dsc.an = AN;
            dsc.vn = "";

            dsc.visit_date = "";
            dsc.pre_no = "";
            dsc.ml_fm = "FM-MED-003";
            
            dsc.host_ftp = bc.iniC.hostFTP;
            //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
            dsc.image_path = "";
            dsc.doc_group_sub_id = "1200000005";

            dsc.an_date = "";
            dsc.folder_ftp = bc.iniC.folderFTP;
            dsc.status_ipd = "I";
            dsc.row_no = "1";
            dsc.row_cnt = "1";
            dsc.status_ml = "2";
            String re = bc.bcDB.dscDB.insertScreenCapture(dsc, bc.userId);
            long chk = 0;
            if (long.TryParse(re, out chk))
            {
                bc.bcDB.dscDB.voidDocScanCertMed(re, "screencapture_ordersheet");      //ต้องการ ไม่ให้แสดงผล คือ ไม่ให้ select ขึ้น ต้องการแค่ เลขที่ doc_scan เท่านั้น
                qrcode.CodeType = C1.BarCode.CodeType.QRCode;
                qrcode.Text = txtPrnHN.Text.Trim() + "#AN " + AN+"#docid "+ re;
            }
            RectangleF recfqrcode = new RectangleF(20, 20, e.MarginBounds.Width-62-30, e.MarginBounds.Height-62-60);
            //e.Graphics.DrawImage(qrcode.Image, recfqrcode);

            RectangleF recf = new RectangleF(15, 15, (int)newWidth, (int)newHeight);
            e.Graphics.DrawImage(logo, recf);
            
            e.Graphics.DrawString(bc.iniC.hostname, famt1, Brushes.Black, 70, 10, flags);
            e.Graphics.DrawString("Doctor Order Sheet", famt7, Brushes.Black, 300, 10, flags);
            e.Graphics.DrawString("print date "+DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), famt, Brushes.Black, 590, 15, flags);

            e.Graphics.DrawString("HN "+txtPrnHN.Text+"   "+ txtNameT.Text+"   age "+ptt.AgeStringOK1DOT()+"   AN "+AN, famt7, Brushes.Black, 63, 40, flags);
            e.Graphics.DrawString("Department ................. ", famt7, Brushes.Black, 40, 70, flags);
            e.Graphics.DrawString("Ward ................. ", famt7, Brushes.Black, 250, 70, flags);
            e.Graphics.DrawString(txtWrdRoom.Text.Trim(), famt1, Brushes.Black, 300, 68, flags);
            e.Graphics.DrawString("Attending Physical ................................... ", famt7, Brushes.Black, 400, 102, flags);
            e.Graphics.DrawString(lbDtrName.Text, famt7, Brushes.Black, 570, 92, flags);
            e.Graphics.DrawString("อาการ "+txtSymptoms.Text.Replace(Environment.NewLine,""), fEditS1, Brushes.Black, 40, 102, flags);
            //DataTable DRUGALLERGY = bc.bcDB.vsDB.selectDrugAllergy(txtPrnHN.Text.Trim());
            String drugallergy = grfDrugAllergy.Rows.Count == 1 ? "ไม่พบแพ้ยา" : "";
            if(grfDrugAllergy.Rows.Count> 1)
            {
                int i = 0;
                drugallergy = "";
                foreach(C1.Win.C1FlexGrid.Row drow in grfDrugAllergy.Rows)
                {
                    if (drow[2].ToString().Trim().Equals("-")) continue;
                    drugallergy += drow[1].ToString().Trim()+" "+ drow[2].ToString().Trim() + Environment.NewLine;
                    i++;
                }
            }
            e.Graphics.DrawString("แพ้ยา " + drugallergy, famt1, Brushes.Black, 420, 70, flags);

            e.Graphics.DrawRectangle(blackPen, 40, 130,740, 1000);
            e.Graphics.DrawLine(blackPen, 400, 130, 400, 800);      //เส้นvertical
            e.Graphics.DrawLine(blackPen, 100, 130, 100, 800);      //เส้นvertical
            e.Graphics.DrawLine(blackPen, 40, 165, 780, 165);
            e.Graphics.DrawLine(blackPen, 460, 130, 460, 800);      //เส้นvertical

            e.Graphics.DrawString("Order for One Day", famt1, Brushes.Black, 140, 132, flags);
            e.Graphics.DrawString("datetime", famt, Brushes.Black, 40, 132, flags);
            e.Graphics.DrawString("datetime", famt, Brushes.Black, 402, 132, flags);
            e.Graphics.DrawLine(blackPen, 40, 800, 780, 800);
            e.Graphics.DrawString("Order for Continuation", famt1, Brushes.Black, 510, 132, flags);
            e.Graphics.DrawString("บันทึกอาการและความก้าวหน้า Progess Note of Multidisciplnary Team(ตามมาตรฐาน S.O.A.P)", famt1, Brushes.Black, 40, 800, flags);
            e.Graphics.DrawString("FM-MED-003 (00-01/02/61) (1/1)", famt, Brushes.Black, 50, 1120, flags);

            if (chkOrdSheetPreOp.Checked)
            {
                e.Graphics.DrawString("Pre-Op order for .........................", famt, Brushes.Black, 140, 172, flags);
                e.Graphics.DrawString("-NPO", famt, Brushes.Black, 110, 192, flags);
                e.Graphics.DrawString("-set OR วันที่ ....................", famt, Brushes.Black, 110, 212, flags);
                
                e.Graphics.DrawRectangle(blackPen, new Rectangle(110, 234, recx, recy));
                e.Graphics.DrawString(" 5% D/N/2 1000 ml", famt, Brushes.Black, 130, 232, flags);
                e.Graphics.DrawString(" IV drip 100 ml/hr", famt, Brushes.Black, 130, 252, flags);

                e.Graphics.DrawRectangle(blackPen, new Rectangle(110, 273, recx, recy));
                e.Graphics.DrawString(" NSS 1000 ml", famt, Brushes.Black, 130, 272, flags);

                e.Graphics.DrawString(" IV drip 100 ml/hr", famt, Brushes.Black, 130, 292, flags);
                e.Graphics.DrawString("-prep skin abdomen & perinium", famt, Brushes.Black, 110, 312, flags);
                e.Graphics.DrawRectangle(blackPen, new Rectangle(110, 335, recx, recy));
                e.Graphics.DrawString("bowel preparation", famt, Brushes.Black, 130, 332, flags);
                e.Graphics.DrawString("-Cefazolin 1 mg to OR", famt, Brushes.Black, 110, 352, flags);
                e.Graphics.DrawString("-Retain Foley's cath ใน OR", famt, Brushes.Black, 110, 372, flags);
                e.Graphics.DrawRectangle(blackPen, new Rectangle(110, 394, recx, recy));
                e.Graphics.DrawString("CBC, Electrolyte, Creatinine, Anti-HIV", famt, Brushes.Black, 130, 392, flags);
                e.Graphics.DrawString("Chest X-ray, EKG", famt, Brushes.Black, 130, 412, flags);
                e.Graphics.DrawRectangle(blackPen, new Rectangle(110, 434, recx, recy));
                e.Graphics.DrawString("CBC, VDRL, Anti-HIV", famt, Brushes.Black, 130, 432, flags);
                e.Graphics.DrawString("-G/M for PRC 2 units", famt, Brushes.Black, 110, 452, flags);
            }
            else if (chkOrdSheetPreOpDi.Checked)
            {
                e.Graphics.DrawString("Pre-Op order for dilatation & curettage", famt, Brushes.Black, 140, 172, flags);
                e.Graphics.DrawString("-NPO", famt, Brushes.Black, 110, 192, flags);
                e.Graphics.DrawString("-5%D/N/2 1000 ml IV drip 100 ml./hr.", famt, Brushes.Black, 110, 212, flags);
                e.Graphics.DrawString("-Pethidine .................... mg to LR", famt, Brushes.Black, 110, 232, flags);
                e.Graphics.DrawString("-Valium ......................... mg to LR", famt, Brushes.Black, 110, 252, flags);
                e.Graphics.DrawString("-ให้ผู้ป่วยปัสสาวะทิ้งก่อนทำหัตถการ", famt, Brushes.Black, 110, 272, flags);
            }
            else if (chkOrdSheetPostOp.Checked)
            {
                e.Graphics.DrawString("Post-Op order for .........................", famt, Brushes.Black, 240, 172, flags);
                e.Graphics.DrawString("-Record V/S q 15 min x IV", famt, Brushes.Black, 110, 192, flags);
                e.Graphics.DrawString("-NPO", famt, Brushes.Black, 470, 192, flags);
                e.Graphics.DrawString("then           q30 min x II", famt, Brushes.Black, 110, 212, flags);
                e.Graphics.DrawString("-Record V/S", famt, Brushes.Black, 470, 212, flags);
                e.Graphics.DrawString("then           q1 hr.until stable", famt, Brushes.Black, 110, 232, flags);
                e.Graphics.DrawString("-routine post -op care", famt, Brushes.Black, 110, 252, flags);
                e.Graphics.DrawString("MED", famt, Brushes.Black, 500, 252, flags);
                e.Graphics.DrawString("-5% DN/2 100 ml./hr", famt, Brushes.Black, 110, 272, flags);
                e.Graphics.DrawString("-Amoxicillin(500)    # 40", famt, Brushes.Black, 470, 272, flags);
                e.Graphics.DrawString("IV drip 100 ml./hr", famt, Brushes.Black, 130, 292, flags);
                e.Graphics.DrawString("2 x 2 tab ⊙ Pc", famt, Brushes.Black, 490, 292, flags);
                e.Graphics.DrawRectangle(blackPen, new Rectangle(110, 312, recx, recy));
                e.Graphics.DrawString("Add Oxytocin 10 units ใน IV ขวดแรก", famt, Brushes.Black, 130, 312, flags);
                e.Graphics.DrawString("start หลัง cefazolin ครบ 3 doses", famt, Brushes.Black, 490, 312, flags);
                e.Graphics.DrawRectangle(blackPen, new Rectangle(110, 332, recx, recy));
                e.Graphics.DrawString("observe uterine contraction and vaginal bleeding", famt, Brushes.Black, 130, 332, flags);
                e.Graphics.DrawString("-paracetamol (500)", famt, Brushes.Black, 470, 332, flags);
                e.Graphics.DrawString("-Cefazolin 1 gm IV q 6 hr x III", famt, Brushes.Black, 110, 352, flags);
                e.Graphics.DrawString("1 tab ⊙ prn q 6 hr.    # 20", famt, Brushes.Black, 490, 352, flags);
                e.Graphics.DrawString("-Pethidine 50 mg", famt, Brushes.Black, 110, 372, flags);
                e.Graphics.DrawString("-lbuprofen (400)    # 30", famt, Brushes.Black, 470, 372, flags);
                e.Graphics.DrawString("IV prn q 6 hr", famt, Brushes.Black, 110, 392, flags);
                e.Graphics.DrawString("1 x 3 ⊙ pc", famt, Brushes.Black, 490, 392, flags);
                e.Graphics.DrawString("-Plasil 1 amp", famt, Brushes.Black, 110, 412, flags);
                e.Graphics.DrawString("IV prn q 6 hr", famt, Brushes.Black, 110, 432, flags);
                e.Graphics.DrawString("-Senokot", famt, Brushes.Black, 470, 432, flags);
                e.Graphics.DrawString("-IV หมด off ได้พร้อม Foley's cath", famt, Brushes.Black, 110, 452, flags);
                e.Graphics.DrawString("2 tab ⊙ hs.", famt, Brushes.Black, 490, 452, flags);
                e.Graphics.DrawString("-จิบน้ำ มื้อ ...................", famt, Brushes.Black, 110, 472, flags);
                e.Graphics.DrawString("คืนก่อน discharge ถ้ายังไม่จ่าย", famt, Brushes.Black, 490, 472, flags);
                e.Graphics.DrawString("Liquid diet มื้อ ....................", famt, Brushes.Black, 130, 492, flags);
                e.Graphics.DrawString("Soft diet มื้อ .................", famt, Brushes.Black, 130, 512, flags);
                e.Graphics.DrawString("Regular diet เริ่ม มื้อ .................. ", famt, Brushes.Black, 130, 532, flags);
                e.Graphics.DrawString("-ถ้าอาการดี plan discharge วันที่ ..................", famt, Brushes.Black, 110, 552, flags);
                e.Graphics.DrawString("F/U วันที่ .....................", famt, Brushes.Black, 130, 572, flags);
            }
            else if (chkOrdSheetPostOpDi.Checked)
            {
                e.Graphics.DrawString("Post-Op order for Dilatation & curettage", famt, Brushes.Black, 140, 172, flags);
                e.Graphics.DrawString("-รู้สึกตัวดี", famt, Brushes.Black, 110, 192, flags);
                e.Graphics.DrawString("-Regular diet.", famt, Brushes.Black, 470, 192, flags);
                e.Graphics.DrawString("-IV หมด Off", famt, Brushes.Black, 110, 212, flags);
                e.Graphics.DrawString("-record V/s.", famt, Brushes.Black, 470, 212, flags);
                e.Graphics.DrawString("-Observe Vaginal bleeding", famt, Brushes.Black, 110, 232, flags);
                e.Graphics.DrawString("MED", famt, Brushes.Black, 500, 232, flags);
                e.Graphics.DrawString("-อาการดีให้ Discharge", famt, Brushes.Black, 110, 252, flags);
                e.Graphics.DrawString("-Doxycyeline (100) # 14", famt, Brushes.Black, 470, 252, flags);
                e.Graphics.DrawString("-วันที่ .........................", famt, Brushes.Black, 110, 272, flags);
                e.Graphics.DrawString(" 1 x 2 ⊙ pc", famt, Brushes.Black, 470, 272, flags);
                e.Graphics.DrawString("-F/U วันที่ .........................", famt, Brushes.Black, 110, 292, flags);
                e.Graphics.DrawString("-lbuprofen  (400) # 20", famt, Brushes.Black, 470, 292, flags);
                e.Graphics.DrawString(" 1 x 3 ⊙ pc", famt, Brushes.Black, 470, 312, flags);
            }

            g.Dispose();
            blackPen.Dispose();
            logo.Dispose();
        }

        private void TxtCertID_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                mcerti = new MedicalCertificate();
                mcerti = bc.bcDB.mcertiDB.selectByPk("555" + txtCertID.Text.Trim());
                if (mcerti.certi_id.Length > 0)
                {
                    lbName.Text = mcerti.ptt_name_t;
                    dsc = new DocScan();
                    dsc = bc.bcDB.dscDB.selectByPk(mcerti.doc_scan_id);
                    txtVN.Value = dsc.vn;
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmSearchHn frm = new FrmSearchHn(bc, FrmSearchHn.StatusConnection.host);
            frm.ShowDialog(this);
            String[] an = bc.sPtt.an.Split('/');
            //if (an.Length > 1)
            //{
            //    txtAN.Value = an[0];
            //    txtAnCnt.Value = an[1];
            //}
            //else
            //{
            clearText();
            if (bc.sPtt.an.Replace("/","").Length > 0)
            {
                lbVn.Text = "AN :";
                txtVN.Value = bc.sPtt.an.Trim();
                txtHn.Value = bc.sPtt.Hn.Trim();
                lbName.Text = bc.sPtt.Name;
            }
            else if (bc.sPtt.vn.Length > 0)
            {
                lbVn.Text = "VN :";
                txtVN.Value = bc.sPtt.vn.Trim();
                txtHn.Value = bc.sPtt.Hn.Trim();
                lbName.Text = bc.sPtt.Name;
            }
            else
            {
                MessageBox.Show("ไม่พบ ข้อมูล an หรือ vn", "");
                return;
            }
            //txtAN.Value = bc.sPtt.an;
            //txtAnCnt.Value = "";
            ////}
            //txtHn.Value = bc.sPtt.Hn;
            //txtName.Value = bc.sPtt.Name;
            //txtVN.Value = bc.sPtt.vn;
            //txtVisitDate.Value = bc.sPtt.visitDate;
            //txtPreNo.Value = bc.sPtt.preno;

            //txtAnDate.Value = bc.sPtt.anDate;
            //chkIPD.Checked = bc.sPtt.statusIPD.Equals("I") ? true : false;

            //if (chkIPD.Checked)
            //{
            //    txtVisitDate.Hide();
            //    txtAnDate.Show();
            //    label6.Text = "AN Date :";
            //}
            //else
            //{
            //    txtVisitDate.Show();
            //    txtAnDate.Hide();
            //    label6.Text = "Visit Date :";
            //}
        }
        private void clearText()
        {
            lbVn.Text = "";
            txtVN.Value = "";
            txtHn.Value = "";
            lbName.Text = "";
        }
        private void ChkUpload_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            initGrfView();
            getListFile();
        }

        private void ChkView_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //initGrfDownload();
            //setGrfUpload();
        }

        private void TxtHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatient(txtHn.Text.Trim());
                lbName.Text = ptt.Name;
            }
        }

        private void FrmScreenCapture_Activated(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //getListFile();
        }

        private void FrmScreenCapture_FormClosed(object sender, FormClosedEventArgs e)
        {
            //throw new NotImplementedException();
            //m_Watcher.Dispose();
        }
        private void initGrfHn()
        {
            grfHn = new C1FlexGrid();
            grfHn.Font = fEdit;
            grfHn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHn.Location = new System.Drawing.Point(0, 0);

            pnPttinDept.Controls.Add(grfHn);

            grfHn.Rows.Count = 1;
            grfHn.Cols.Count = 11;
            grfHn.Cols[colHnHn].Caption = "HN";
            grfHn.Cols[colHnName].Caption = "Name";
            grfHn.Cols[colHnVn].Caption = "VN";

            grfHn.Cols[colHnHn].Width = 90;
            grfHn.Cols[colHnName].Width = 250;
            grfHn.Cols[colHnVn].Width = 100;

            grfHn.Cols[colHnHn].AllowEditing = false;
            grfHn.Cols[colHnName].AllowEditing = false;
            grfHn.Cols[colHnVn].AllowEditing = false;
            grfHn.Cols[colHnHn].Visible = true;
            grfHn.Cols[colHnName].Visible = true;
            grfHn.Cols[colHnVn].Visible = true;
            grfHn.Cols[colHnVsDate].Visible = true;
            grfHn.Cols[colHnPreno].Visible = false;
            grfHn.Cols[colHnWrdNo].Visible = false;
            grfHn.Cols[colHnRoom].Visible = false;
            grfHn.Cols[colHnBed].Visible = false;
            grfHn.Cols[colHnSymptoms].Visible = false;

            grfHn.Click += GrfHn_Click;
        }
        private void GrfHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfHn.Row <= 0) return;
            if (grfHn.Col <= 0) return;
            AN = grfHn[grfHn.Row, colHnVn].ToString();
            setControl(grfHn[grfHn.Row, colHnHn].ToString(), grfHn[grfHn.Row, colHnVsDate].ToString(), grfHn[grfHn.Row, colHnPreno].ToString()
                , cboDept.Text+ " "+ grfHn[grfHn.Row, colHnRoom].ToString() + " " + grfHn[grfHn.Row, colHnBed].ToString(), grfHn[grfHn.Row, colHnSymptoms].ToString());
        }
        private void setGrfHn(String wardid)
        {
            DataTable dt = new DataTable();
            if (chkIPD.Checked)
            {
                dt = bc.bcDB.pttDB.selectPatientinWardIPD(wardid);
            }
            else
            {
                DateTime dtstart1 = DateTime.Now;
                String deptid = bc.bcDB.pttDB.selectDeptIdOPDBySecId(wardid);
                if (dtstart1.Year > 2500)
                {

                }
                dt = bc.bcDB.vsDB.selectPttHiinDept1(deptid, wardid, dtstart1.Year + "-" + dtstart1.ToString("MM-dd"), dtstart1.Year + "-" + dtstart1.ToString("MM-dd"));
            }

            grfHn.Rows.Count = 1;
            grfHn.Rows.Count = dt.Rows.Count + 1;
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                grfHn[i, colHnHn] = row1["MNC_HN_NO"].ToString();
                grfHn[i, colHnName] = row1["prefix"].ToString() + " " + row1["MNC_FNAME_T"].ToString() + " " + row1["MNC_LNAME_T"].ToString();
                grfHn[i, colHnPreno] = row1["MNC_PRE_NO"].ToString();
                if (chkIPD.Checked)
                {
                    grfHn[i, colHnVsDate] = row1["MNC_AD_DATE"].ToString();
                    grfHn[i, colHnVn] = row1["an_no"].ToString();
                    grfHn[i, colHnWrdNo] = row1["MNC_WD_NO"].ToString();
                    grfHn[i, colHnRoom] = row1["MNC_RM_NAM"].ToString();
                    grfHn[i, colHnBed] = row1["MNC_BD_NO"].ToString();
                    grfHn[i, colHnSymptoms] = row1["MNC_SHIF_MEMO"].ToString();
                }
                else
                {
                    grfHn[i, colHnVsDate] = row1["MNC_DATE"].ToString();
                    grfHn[i, colHnVn] = "";
                }
                grfHn[i, 0] = i;
                if (i % 2 == 0)
                {
                    grfHn.Rows[i].StyleDisplay.BackColor = Color.FromArgb(143, 200, 127);
                }
                else
                {
                    grfHn.Rows[i].StyleDisplay.BackColor = Color.Cornsilk;
                }
            }
        }
        private void setControl(String hn, String vsdate, String preno, String wrd, String symptoms)
        {
            String vstime = "";

            ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetVisitOPDByHn(hn, vsdate, preno);
            txtPrnHN.Text = hn;
            txtNameT.Text = ptt.Name;
            txtNameE.Text = ptt.MNC_FNAME_E + " " + ptt.MNC_LNAME_E;
            txtDOB.Text = ptt.patient_birthday;
            txtWrdRoom.Text = wrd;
            txtSymptoms.Text = symptoms;
            setGrfDrugAllergy();
            txtDtrCode.Text = (DTRCODE.Length == 0) ? ptt.dtrcode : DTRCODE;
            if (chkIPD.Checked)
            {
                DataTable dt = new DataTable();
                String[] an = AN.Split('.');
                if (an.Length > 1)
                {
                    dt = bc.bcDB.vsDB.selectPttIPD(an[0], an[1]);
                    String dtrcodeS = dt.Rows[0]["MNC_DOT_CD_S"].ToString();
                    String dtrcodeR = dt.Rows[0]["MNC_DOT_CD_R"].ToString();
                    txtDtrCode.Text = dtrcodeR;
                    lbDtrName.Text = bc.selectDoctorName(txtDtrCode.Text.Trim());
                }
            }
            else
            {
                lbDtrName.Text = bc.selectDoctorName(txtDtrCode.Text.Trim());
            }
            setControlDateClear();
        }
        private void setControlDateClear()
        {
            //txtChk2DateStart.Clear();
            //txtChk2DateEnd.Clear();
            //txtChk3DateStart.Clear();
            //txtChk3DateEnd.Clear();
            //txtChk4Date.Clear();
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
            grfDrugAllergy.Rows.Count = 1;
            //ใช้ database ใน object patient จะได้ไม่ต้องดึงข้อมูลหลายครั้ง  ลดการดึงข้อมูล
            PTT.DRUGALLERGY = bc.bcDB.vsDB.selectDrugAllergy(txtPrnHN.Text.Trim());
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1;
            foreach (DataRow row1 in PTT.DRUGALLERGY.Rows)
            {
                //pB1.Value++;
                C1.Win.C1FlexGrid.Row rowa = grfDrugAllergy.Rows.Add();
                rowa[1] = row1["mnc_ph_tn"].ToString();
                rowa[3] = row1["MNC_PH_ALG_DSC"].ToString();
                rowa[2] = row1["MNC_PH_MEMO"].ToString();
            }
        }
        private void initGrfDownload()
        {
            if(grfView != null)
            {
                grfView.Dispose();
            }
            grfDownload = new C1FlexGrid();
            grfDownload.Font = fEdit;
            grfDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDownload.Location = new System.Drawing.Point(0, 0);

            grfDownload.Rows[0].Visible = false;
            grfDownload.Cols[0].Visible = false;
            pnView.Controls.Add(grfDownload);

            grfDownload.Rows.Count = 1;
            grfDownload.Cols.Count = 5;
            grfDownload.Cols[1].Width = this.Width - 50;
            grfDownload.DoubleClick += GrfDownload_DoubleClick;
            
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ Download ภาพนี้", new EventHandler(ContextMenu_View_Download));
            menuGw.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_View_Print));
            menuGw.MenuItems.Add("ต้องการ Print ภาพนี้ แบบ 3 ภาพ ภาพที่ 1", new EventHandler(ContextMenu_View_Print_multi));
            menuGw.MenuItems.Add("ต้องการ Print ภาพนี้ แบบ 3 ภาพ ภาพที่ 2", new EventHandler(ContextMenu_View_Print_multi));
            menuGw.MenuItems.Add("ต้องการ Print ภาพนี้ แบบ 3 ภาพ ภาพที่ 3", new EventHandler(ContextMenu_View_Print_multi));
            menuGw.MenuItems.Add("ต้องการ ลบข้อมูลนี้", new EventHandler(ContextMenu_View_Delete));
            //mouseWheel = 0;
            //pic.MouseWheel += Pic_MouseWheel;
            grfDownload.ContextMenu = menuGw;

            grfDownload.Cols[colUploadId].Visible = false;
            grfDownload.Cols[colUploadName].Visible = false;
            grfDownload.Cols[colUploadPath].Visible = false;
            grfDownload.Cols[colUploadImg].AllowEditing = false;
            //grfUpload.Cols[2].Visible = false;
        }

        private void GrfDownload_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfDownload.Row < 0) return;
            if (grfDownload.Col < 0) return;
            String id = "", filename="";
            id = grfDownload[grfDownload.Row, colUploadId].ToString();
            filename = grfDownload[grfDownload.Row, colUploadPath].ToString();
            string ext = Path.GetExtension(filename);
            if (ext.ToLower().IndexOf("pdf") > 0)
            {
                if (File.Exists(filename))
                {
                    System.Diagnostics.Process.Start(filename);
                }
            }
            else
            {

            }
        }
        private void sendFTPCertMed()
        {
            string[] files = Directory.GetFiles(bc.iniC.pathScreenCaptureUpload);
            //new LogWriter("d", "FrmScreenCapture sendFTPCertMed start files " + files.Length.ToString());
            foreach (string filename in files)
            {
                try
                {
                    String filename1 = Path.GetFileName(filename);
                    if (filename1.IndexOf(".db") > 0)
                    {
                        File.Delete(filename);
                        continue;
                    }
                    Bitmap img = (Bitmap)Image.FromFile(filename);
                    //new LogWriter("d", "FrmScreenCapture sendFTPCertMed start filename " + filename);
                    IBarcodeReader reader = new BarcodeReader();
                    var result = reader.Decode(img);//สามารถอ่านรูป ว่าเป็น ใบรับรองแพทย์ ได้
                    if (result != null)
                    {
                        String textqrcode = result.Text;
                        String[] txtqrcode = textqrcode.Split(' ');
                        if (txtqrcode.Length > 0)
                        {
                            txtHn.Value = txtqrcode[0];
                            String certid = "";
                            certid = txtqrcode[txtqrcode.Length - 1];
                            mcerti = new MedicalCertificate();
                            mcerti = bc.bcDB.mcertiDB.selectByPk("555" + certid);
                            lbName.Text = mcerti.ptt_name_t;
                            DocScan dsc = bc.bcDB.dscDB.selectByPk(mcerti.doc_scan_id);
                            if (mcerti.visit_date.Equals(""))
                            {
                                bc.bcDB.mcertiDB.updateVsDateByPk("555" + certid, dsc.visit_date);  // มี bug ทำให้ต้อง update vsdate ในกรณี vsdate เป้นค่าว่าง เวลาค้นหา จะได้ค้นหาพบ
                            }
                            txtVN.Value = dsc.vn;
                            txtCertID.Value = certid;
                            img.Dispose();
                            uploadFileCertMedTOdocscan(mcerti, filename, dsc.vn, dsc.visit_date, dsc.pre_no);
                            new LogWriter("d", "FrmScreenCapture sendFTPCertMed mcerti "+ mcerti);
                            //File.Delete(filename);
                        }
                    }
                    else if (img.Height <= 2000)//น่าจะเป็นใบรับรองแพทย์
                    {
                        new LogWriter("d", "FrmScreenCapture sendFTPCertMed img.Height <= 2000 ");
                        FtpClient ftp = new FtpClient(bc.iniC.hostFTPCertMed, bc.iniC.userFTPCertMed, bc.iniC.passFTPCertMed, bc.ftpUsePassiveLabOut);
                        if (ftp.upload("//cert_med//" + filename1, filename))
                        {
                            //new LogWriter("d", "FrmScreenCapture sendFTPCertMed cert_med File.Delete(filename); " + filename);
                            img.Dispose();
                            File.Delete(filename);
                        }
                    }
                    else if (img.Height > 3000)//น่าจะเป็น Order Sheet
                    {
                        //new LogWriter("d", "FrmScreenCapture sendFTPCertMed img.Height > 3000 ");
                        imgPrint = (Image)img.Clone();
                        if (bc.iniC.statusLabOutAutoPrint.Equals("1")) printDrugIn();
                        FtpClient ftp = new FtpClient(bc.iniC.hostFTPDrugIn, bc.iniC.userFTPDrugIn, bc.iniC.passFTPDrugIn, bc.ftpUsePassiveLabOut);
                        if (ftp.upload("//drugin//" + filename1, filename))
                        {
                            //new LogWriter("d", "FrmScreenCapture sendFTPCertMed drugin File.Delete(filename); " + filename);
                            img.Dispose();
                            imgPrint.Dispose();
                            File.Delete(filename);
                        }
                        
                    }
                    else
                    {
                        new LogWriter("d", "FrmScreenCapture sendFTPCertMed else ");
                        FtpClient ftp = new FtpClient(bc.iniC.hostFTPCertMed, bc.iniC.userFTPCertMed, bc.iniC.passFTPCertMed, bc.ftpUsePassiveLabOut);
                        if (ftp.upload("//cert_med//" + filename1, filename))
                        {
                            img.Dispose();
                            File.Delete(filename);
                        }
                    }
                    reader = null;
                    result = null;
                }
                catch (Exception ex)
                {
                    c1StatusBar1.Text = ex.Message;
                    new LogWriter("e", "FrmScreenCapture sendFTPCertMed ex.Message " + ex.Message);
                    Application.Restart();
                }
            }
        }
        private void printDrugIn()
        {
            String err = "00";
            try
            {
                //new LogWriter("d", "FrmScreenCapture printDrugIn ");
                SetDefaultPrinter(bc.iniC.printerA4);
                err = "01";
                PrintDocument pd = new PrintDocument();
                err = "02";
                pd.DefaultPageSettings.Landscape = false;
                err = "03";
                pd.PrintPage += Pd_PrintPageDrugIn;
                pd.Print();
                pd.Dispose();
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmScreenCapture  printDrugIn(Bitmap img) "+ err +" lineerr "+LINEERR+" : " + ex.Message);
            }
        }
        private void Pd_PrintPageDrugIn(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            //e.PageSettings.Landscape = false;
            //new LogWriter("d", "FrmScreenCapture Pd_PrintPageDrugIn e.PageSettings.Margins.Top  " + e.PageSettings.Margins.Top + " e.PageSettings.Margins.Top  " + e.PageSettings.Margins.Top);
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Left = 0;
            
            RectangleF bounds = e.PageSettings.PrintableArea;
            LINEERR = "Pd_PrintPageDrugIn 00";
            //new LogWriter("d", "FrmScreenCapture Pd_PrintPageDrugIn bounds.Height " + bounds.Height + " bounds.Width " + bounds.Width);
            Rectangle m = e.MarginBounds;
            //new LogWriter("d", "FrmScreenCapture Pd_PrintPageDrugIn m.Height " + m.Height+ " m.Width " + m.Width);
            if ((double)imgPrint.Width / (double)imgPrint.Height > (double)m.Width / (double)m.Height) // image is wider
            {
                m.Height = (int)((double)imgPrint.Height / (double)imgPrint.Width * (double)m.Width);
                //new LogWriter("d", "FrmScreenCapture Pd_PrintPageDrugIn > m.Height " + m.Height);
            }
            else
            {
                m.Width = (int)((double)imgPrint.Width / (double)imgPrint.Height * (double)m.Height);
                //new LogWriter("d", "FrmScreenCapture Pd_PrintPageDrugIn <= m.Width " + m.Width);
            }
            LINEERR = "Pd_PrintPageDrugIn 01";
            m.Height = (int)bounds.Height - 5;
            m.Width = (int)bounds.Width - 5;
            m.X = 10; m.Y=10;
            //pd.DefaultPageSettings.Landscape = m.Width > m.Height;
            //Putting image in center of page.

            //e.Graphics.DrawString("print date " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), fEdit, Brushes.Black, 30, 30);
            //e.Graphics.DrawString("doc scan id " + dsc_id, fEdit, Brushes.Black, 30, 50);
            //e.Graphics.DrawImage(imgPrint, m);
            LINEERR = "Pd_PrintPageDrugIn 02";
            e.Graphics.DrawImage(imgPrint, m);
            //e.Graphics.DrawImage(imgPrint, e.MarginBounds);
            imgPrint.Dispose();
        }

        private void uploadFileCertMedTOdocscan(MedicalCertificate mcerti,String filename, String vn, String vsdate, String preno)
        {
            FileInfo fileInfo = new FileInfo(filename);

            DocScan dsc = new DocScan();
            //new LogWriter("d", "BtnUpload_Click dsc.vn " + dsc.vn + " dsc.an " + dsc.an);
            dsc.active = "1";
            dsc.doc_scan_id = "";
            dsc.doc_group_id = "1100000007";
            dsc.hn = txtHn.Text;
            
            dsc.an = mcerti.an;
            dsc.vn = vn;
            
            dsc.visit_date = vsdate;
            dsc.pre_no = preno;
            dsc.ml_fm = "FM-MED-001";
            bc.bcDB.dscDB.voidDocScanCertMed(mcerti.doc_scan_id, "screencapture");
            dsc.host_ftp = bc.iniC.hostFTP;
            //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
            dsc.image_path = "";
            dsc.doc_group_sub_id = "1200000030";

            dsc.an_date = "";
            dsc.folder_ftp = bc.iniC.folderFTP;
            dsc.status_ipd = "O";
            dsc.row_no = "1";
            dsc.row_cnt = "1";
            dsc.status_ml = "2";
            String re = bc.bcDB.dscDB.insertScreenCapture(dsc, bc.userId);

            long chk = 0;
            if (long.TryParse(re, out chk))
            {
                dsc.image_path = txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + re + fileInfo.Extension;
                String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive, bc.iniC.ProxyProxyType, bc.iniC.ProxyHost, bc.iniC.ProxyPort);                
                ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-"));
                ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename))
                {
                    File.Delete(filename);
                    System.Threading.Thread.Sleep(500);
                }
            }
        }
        private void initPrintMulti()
        {
            frmImg = new Form();
            frmImg.WindowState = FormWindowState.Normal;
            
            frmImg.Size = new Size(1024, 764);
            //frmImg.AutoScroll = true;
            //frmImg.SuspendLayout();
            //this.Location.
            //pnMain = new Panefl();
            //pnMain.Location = new System.Drawing.Point(formwidth + 40, 0);
            //pnMain.Size = new System.Drawing.Size(int.Parse(bc.iniC.imggridscanwidth), this.Height);
            //pnMain.Name = "pnMain";
            //pnMain.BackColor = Color.Red;
            //pnMain.Dock = DockStyle.Fill;
            //pnMain.BorderStyle = BorderStyle.Fixed3D;

            //frmImg.Show();
            //frmImg.Text = "aaa";
            //Button btn = new Button();
            //btn.Size = new Size(100, 100);
            //btn.Location = new Point(10, 10);
            //pnMain.Controls.Add(btn);
            //frmImg.Controls.Add(pnMain);
            //pnTop.Location = new System.Drawing.Point(formwidth + 40, 0);
            //pnTop.Size = new System.Drawing.Size(int.Parse(bc.iniC.imggridscanwidth) / 2, this.Height);
            //pnTop.Name = "pnTop";
            //pnTop.Height = frmImg.Height / 2;
            //pnTop.Dock = DockStyle.Top;
            //pnTop.BorderStyle = BorderStyle.Fixed3D;
            //pnTop.BackColor = Color.Red;

            //pnBotom.Location = new System.Drawing.Point(formwidth + 40, 0);
            //pnBotom.Size = new System.Drawing.Size(int.Parse(bc.iniC.imggridscanwidth) / 2, this.Height);
            //pnBotom.Name = "pnBotom";
            //pnBotom.Dock = DockStyle.Fill;
            //pnBotom.BackColor = Color.Green;
            //pnBotom.Height = frmImg.Height / 2;

            //pnMain.Controls.Add(pnTop);
            //pnMain.Controls.Add(pnBotom);






            //pnBotom.Controls.Add(picRight);
            //((System.ComponentModel.ISupportInitialize)(frmImg.pnMain)).EndInit();
            //frmImg.ResumeLayout(false);

            pnTop = new System.Windows.Forms.Panel();
            pnBotton = new System.Windows.Forms.Panel();
            pnLeft = new System.Windows.Forms.Panel();
            pnRight = new System.Windows.Forms.Panel();
            //panel2.SuspendLayout();
            //SuspendLayout();
            // 
            // panel1
            // 
            pnTop.BackColor = System.Drawing.SystemColors.ActiveCaption;
            pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            pnTop.Location = new System.Drawing.Point(0, 0);
            pnTop.Name = "pnTop";
            pnTop.Size = new System.Drawing.Size(frmImg.Width, frmImg.Height/2);
            pnTop.TabIndex = 0;
            // 
            // panel2
            // 
            pnBotton.Controls.Add(this.pnRight);
            pnBotton.Controls.Add(this.pnLeft);
            pnBotton.Dock = System.Windows.Forms.DockStyle.Fill;
            pnBotton.Location = new System.Drawing.Point(0, 418);
            pnBotton.Name = "panel2";
            pnBotton.Size = new System.Drawing.Size(1045, 401);
            pnBotton.TabIndex = 0;
            // 
            // panel3
            // 
            pnLeft.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            pnLeft.Dock = System.Windows.Forms.DockStyle.Left;
            pnLeft.Location = new System.Drawing.Point(0, 0);
            pnLeft.Name = "panel3";
            pnLeft.Size = new System.Drawing.Size(pnBotton.Width/2, pnBotton.Height);
            pnLeft.TabIndex = 0;
            // 
            // panel4
            // 
            pnRight.BackColor = System.Drawing.SystemColors.AppWorkspace;
            pnRight.Dock = System.Windows.Forms.DockStyle.Fill;
            pnRight.Location = new System.Drawing.Point(515, 0);
            pnRight.Name = "panel4";
            pnRight.Size = new System.Drawing.Size(pnBotton.Width/2, pnBotton.Height);
            pnRight.TabIndex = 1;
            // 
            // FrmScreenCapturePrintMulti
            // 
            frmImg.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            frmImg.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            frmImg.ClientSize = new System.Drawing.Size(1045, 819);
            frmImg.Controls.Add(pnBotton);
            frmImg.Controls.Add(pnTop);
            frmImg.Name = "FrmScreenCapturePrintMulti";
            frmImg.Text = "FrmScreenCapturePrintMulti";
            panel2.ResumeLayout(false);
            frmImg.ResumeLayout(false);

            picTop.Location = new System.Drawing.Point(0, 0);
            picTop.Dock = DockStyle.Fill;
            picTop.Name = "picTop";
            picTop.SizeMode = PictureBoxSizeMode.StretchImage;
            pnTop.Controls.Add(picTop);

            picLeft.Location = new System.Drawing.Point(0, 0);
            picLeft.Dock = DockStyle.Fill;
            picLeft.Name = "picLeft";
            picLeft.SizeMode = PictureBoxSizeMode.StretchImage;
            pnLeft.Controls.Add(picLeft);

            picRight.Location = new System.Drawing.Point(0, 0);
            picRight.Dock = DockStyle.Fill;
            picRight.Name = "picRight";
            picRight.SizeMode = PictureBoxSizeMode.StretchImage;
            pnRight.Controls.Add(picRight);

            //frmImg.Show();

        }
        private void ContextMenu_View_Print_multi(object sender, System.EventArgs e)
        {
            String id = "", filename = "", txtmenu="";
            if (grfDownload.Col <= 0) return;
            if (grfDownload.Row < 0) return;
            id = grfDownload[grfDownload.Row, colUploadId].ToString();
            filename = grfDownload[grfDownload.Row, colUploadPath].ToString();
            txtmenu = sender.ToString();
            MenuItem menu = (MenuItem)sender;
            //lFilePrint[cntPrint] = filename;
            lFilePrint.Add(filename);

            FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
            streamPrint = ftpc.download(filename.Replace(bc.iniC.hostFTP, ""));
            streamPrint.Position = 0;
            Image resizedImage;
            Image img = Image.FromStream(streamPrint);
            int originalWidth = 0, originalHeight = 0;
            int newWidth = bc.grfScanWidth;

            originalHeight = 0;
            originalWidth = img.Width;
            originalHeight = img.Height;
            //resizedImage = img.GetThumbnailImage(newWidth, (newWidth * img.Height) / originalWidth, null, IntPtr.Zero);
            resizedImage = img.GetThumbnailImage(newWidth, (newWidth * img.Height) / originalWidth, null, IntPtr.Zero);
            if (menu.Text.Equals("ต้องการ Print ภาพนี้ แบบ 3 ภาพ ภาพที่ 1"))
            {
                picTop.Image = img;
            }
            else if (menu.Text.Equals("ต้องการ Print ภาพนี้ แบบ 3 ภาพ ภาพที่ 2"))
            {
                picLeft.Image = img;
            }
            else if (menu.Text.Equals("ต้องการ Print ภาพนี้ แบบ 3 ภาพ ภาพที่ 3"))
            {
                picRight.Image = img;
            }
            cntPrint++;
            if (frmImg == null)
            {
                frmImg.Show(this);
            }
            else
            {
                frmImg.Hide();
                frmImg.Show();
                frmImg.Location = new System.Drawing.Point(this.Location.X + this.Width + 20, screenHeight - frmImg.Height-40);
            }
            //
            //this.Width = formwidth + int.Parse(bc.iniC.imggridscanwidth);
        }
        private void ContextMenu_View_Download(object sender, System.EventArgs e)
        {
            String id = "", datetick = "", filename = "";
            Stream streamDownload = null;
            if (grfDownload.Col <= 0) return;
            if (grfDownload.Row < 0) return;
            id = grfDownload[grfDownload.Row, colUploadId].ToString();
            filename = grfDownload[grfDownload.Row, colUploadPath].ToString();
            FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
            streamDownload = ftpc.download(filename.Replace(bc.iniC.hostFTP, ""));

            streamDownload.Position = 0;
            datetick = DateTime.Now.Ticks.ToString();
            Image img = Image.FromStream(streamDownload);
            if (!Directory.Exists(bc.iniC.pathDownloadFile))
            {
                Directory.CreateDirectory(bc.iniC.pathDownloadFile);
            }
            img.Save(bc.iniC.pathDownloadFile + "\\" + txtHn.Text.Trim() + "_" + datetick + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            bc.ExploreFile(bc.iniC.pathDownloadFile + "\\" + txtHn.Text.Trim() + "_" + datetick + ".jpg");
        }
        private void ContextMenu_View_Print(object sender, System.EventArgs e)
        {
            String id = "", filename="";
            if (grfDownload.Col <= 0) return;
            if (grfDownload.Row < 0) return;
            id = grfDownload[grfDownload.Row, colUploadId].ToString();
            filename = grfDownload[grfDownload.Row, colUploadPath].ToString();
            //MessageBox.Show("bc.iniC.hostFTP "+ bc.iniC.hostFTP+ "\nbc.iniC.userFTP "+ bc.iniC.userFTP+ "\nfilename "+ filename+"\n filename Replase "+ filename.Replace(bc.iniC.hostFTP,""), "");
            FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
            streamPrint = ftpc.download(filename.Replace(bc.iniC.hostFTP, ""));

            String ext = Path.GetExtension(filename);
            if (ext.ToLower().IndexOf("pdf") >= 0)
            {
                if (!Directory.Exists("report"))
                {
                    Directory.CreateDirectory("report");
                }
                
                String datetick = "", filename1="";
                datetick = DateTime.Now.Ticks.ToString();
                filename1 = "report\\" + datetick + ".pdf";
                FileStream filestr = new FileStream(filename1, FileMode.Create);
                streamPrint.Position = 0;
                streamPrint.CopyTo(filestr);
                filestr.Close();
                Process p = new Process();
                p.StartInfo.FileName = filename1;
                p.Start();
            }
            else
            {
                setGrfUploadToPrint();
            }
                
        }
        private void setGrfUploadToPrint()
        {
            SetDefaultPrinter(bc.iniC.printerA4);
            System.Threading.Thread.Sleep(500);

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += Pd_PrintPageA4;
            //here to select the printer attached to user PC
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = pd;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                pd.Print();//this will trigger the Print Event handeler PrintPage
            }
        }
        private void Pd_PrintPageA4(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                Image img = Image.FromStream(streamPrint);
                Image aaa = bc.ResizeImagetoA4Lan(img);
                Rectangle m = e.MarginBounds;
                //m.Width = aaa.Width;
                //m.Height = aaa.Height;
                if ((double)aaa.Width / (double)aaa.Height > (double)m.Width / (double)m.Height) // image is wider
                {
                    m.Height = (int)((double)aaa.Height / (double)aaa.Width * (double)m.Width);
                }
                else
                {
                    m.Width = (int)((double)aaa.Width / (double)aaa.Height * (double)m.Height);
                }
                //e.Graphics.DrawImage(img, m);
                //}
                e.Graphics.DrawImage(aaa,m);
            }
            catch (Exception)
            {

            }
        }
        private void ContextMenu_View_Delete(object sender, System.EventArgs e)
        {

        }
        private void setGrfUpload()
        {
            grfDownload.Cols.Count = 5;
            grfDownload.Rows.Count = 0;
            Column colpic1 = grfDownload.Cols[colUploadImg];
            colpic1.DataType = typeof(Image);


            grfDownload.ShowCursor = true;
            grfDownload.Cols[colUploadId].Visible = false;
            grfDownload.Cols[colUploadName].Visible = false;
            grfDownload.Cols[colUploadPath].Visible = false;

            ProgressBar pB1 = new ProgressBar();
            pB1.Minimum = 0;
            pB1.Location = new Point(20, 5);
            pB1.Width = panel2.Width-60;
            panel2.Controls.Add(pB1);
            label1.Hide();
            lbName.Hide();
            txtHn.Hide();
            chkUpload.Hide();
            chkView.Hide();

            DataTable dt = new DataTable();
            dt = bc.bcDB.dscDB.selectByHnDeptUS(txtHn.Text);
            if (dt.Rows.Count > 0)
            {
                pB1.Maximum = dt.Rows.Count;
                grfDownload.Rows.Count = dt.Rows.Count;
                String err = "", filename = "";
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        String ftphost = "", folderftp = "";
                        filename = dt.Rows[i][bc.bcDB.dscDB.dsc.host_ftp].ToString() + "//" + dt.Rows[i][bc.bcDB.dscDB.dsc.folder_ftp].ToString() + "//" + dt.Rows[i][bc.bcDB.dscDB.dsc.image_path].ToString();
                        string ext = Path.GetExtension(filename);

                        grfDownload[i, colUploadId] = dt.Rows[i][bc.bcDB.dscDB.dsc.doc_scan_id].ToString();
                        grfDownload[i, colUploadPath] = filename;
                        FtpWebRequest ftpRequest = null;
                        FtpWebResponse ftpResponse = null;
                        Stream ftpStream = null;
                        int bufferSize = 2048;
                        MemoryStream stream;
                        Image loadedImage, resizedImage;
                        stream = new MemoryStream();
                        Application.DoEvents();
                        ftpRequest = (FtpWebRequest)FtpWebRequest.Create(filename);
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
                        }
                        err = "03";

                        if (ext.ToLower().IndexOf("pdf") > 0)
                        {
                            grfDownload[i, colUploadImg] = Resources.pdf_symbol_300;
                        }
                        else
                        {
                            grfDownload.Cols[0].Width = bc.imggridscanwidth;
                            loadedImage = new Bitmap(stream);
                            err = "04";
                            int originalWidth = 0;
                            originalWidth = loadedImage.Width;
                            int newWidth = bc.imggridscanwidth;
                            resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
                            grfDownload[i, colUploadImg] = resizedImage;
                        }
                    }
                    catch(Exception ex)
                    {
                        //MessageBox.Show("err " + err + " filename " + filename + "\n "+ex.Message, "");
                    }
                    Application.DoEvents();
                    pB1.Value = i;
                }
                grfDownload.AutoSizeCols();
                grfDownload.AutoSizeRows();
            }
            pB1.Dispose();
            label1.Show();
            lbName.Show();
            txtHn.Show();
            chkUpload.Show();
            chkView.Show();
        }
        private void initGrfView()
        {
            if (grfDownload != null)
            {
                grfDownload.Dispose();
            }
            if (grfView != null)
            {
                grfView.Dispose();
            }
            grfView = new C1FlexGrid();
            grfView.Font = fEdit;
            grfView.Dock = System.Windows.Forms.DockStyle.Fill;
            grfView.Location = new System.Drawing.Point(0, 0);
            
            grfView.Rows[0].Visible = false;
            grfView.Cols[0].Visible = false;
            pnView.Controls.Add(grfView);

            grfView.Rows.Count = 2;
            grfView.Cols.Count = 5;
            grfView.Cols[colUploadImg].Width = this.Width-50;

            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ Upload ภาพนี้", new EventHandler(ContextMenu_UpLoad));
            menuGw.MenuItems.Add("ต้องการ ลบข้อมูลนี้", new EventHandler(ContextMenu_Delete));
            //mouseWheel = 0;
            //pic.MouseWheel += Pic_MouseWheel;
            grfView.ContextMenu = menuGw;
            grfView.Cols[colUploadId].Visible = false;
            grfView.Cols[colUploadName].Visible = false;
            grfView.Cols[colUploadPath].Visible = false;
            grfView.Cols[colUploadImg].AllowEditing = false;
        }
        private void ContextMenu_UpLoad(object sender, System.EventArgs e)
        {
            String filename = "";
            if (txtHn.Text.Length == 0)
            {
                MessageBox.Show("ไม่พบ HN", "");
                return;
            }
            if (lbName.Text.Length == 0)
            {
                MessageBox.Show("ไม่พบ ชื่อ คนไข้", "");
                return;
            }
            filename = grfView[grfView.Row, colUploadPath].ToString();
            if (File.Exists(filename))
            {
                if (dsc.doc_scan_id.Length > 0)
                {
                    FrmScreenCaptureUpload frm = new FrmScreenCaptureUpload(bc, filename, txtHn.Text.Trim(), lbName.Text, txtVN.Text.Trim(), lbVn.Text.Trim(), dsc);
                    frm.ShowDialog(this);
                }
                else
                {
                    FrmScreenCaptureUpload frm = new FrmScreenCaptureUpload(bc, filename, txtHn.Text.Trim(), lbName.Text, txtVN.Text.Trim(), lbVn.Text.Trim());
                    frm.ShowDialog(this);
                }
                
                Application.Exit();
                //getListFile();
            }
            else
            {
                MessageBox.Show("ไม่พบ File Upload", "");
            }
        }
        private void ContextMenu_Delete(object sender, System.EventArgs e)
        {
            
        }
        private void getListFile()
        {
            if (grfView == null) return;
            String path = "";
            if (bc.hn.Length > 0)
            {
                if (File.Exists(bc.hn)) path = Path.GetDirectoryName(bc.hn);
                else    path = bc.iniC.pathScreenCaptureUpload;
            }
            else
            {
                path = bc.iniC.pathScreenCaptureUpload;
            }
            //MessageBox.Show("path "+ path, "");
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] Files = dir.GetFiles("*.*");
            string str = "";
            //grfView.Rows.Count = 4;
            //int i = 0;
            lFile.Clear();
            foreach (FileInfo file in Files)
            {
                if (bc.hn.Length > 0)
                {
                    lFile.Add(bc.hn);
                    break;
                }
                string ext = Path.GetExtension(file.FullName);
                if(ext.ToLower().Equals(".pdf") || ext.ToLower().Equals(".jpg"))
                {
                    lFile.Add(file.FullName);
                }
                if (Files.Length == 1 || ext.ToLower().Equals(".jpg"))
                {
                    String textqrcode = FindQrCodeInImage((Bitmap)Image.FromFile(file.FullName));
                    if (textqrcode != null && textqrcode.Length > 0)
                    {
                        String[] txtqrcode = textqrcode.Split(' ');
                        if (txtqrcode.Length > 0)
                        {
                            txtHn.Value = txtqrcode[0];
                            String certid = "";
                            certid = txtqrcode[txtqrcode.Length-1];
                            mcerti = new MedicalCertificate();
                            mcerti = bc.bcDB.mcertiDB.selectByPk("555"+certid);
                            lbName.Text = mcerti.ptt_name_t;
                            dsc = new DocScan();
                            dsc = bc.bcDB.dscDB.selectByPk(mcerti.doc_scan_id);
                            txtVN.Value = dsc.vn;
                            txtCertID.Value = certid;
                        }
                    }
                }
            }
            setListView();
        }
        private void setListView()
        {
            if (grfView.IsDisposed) return;
            if (grfView.Rows == null) return;
            grfView.Rows.Count = lFile.Count+1;
            Column colpic1 = grfView.Cols[colUploadImg];
            colpic1.DataType = typeof(Image);
            int i = 0;
            foreach (String file in lFile)
            {
                try
                {
                    i++;
                    //Row row = grfView.Rows.Add();
                    //row[colUploadPath] = file;
                    grfView.SetData(i, colUploadPath, file);
                    string ext = Path.GetExtension(file);
                    Image loadedImage, resizedImage;
                    if (ext.ToLower().IndexOf("pdf") < 0)
                    {
                        if (File.Exists(file))
                        {
                            loadedImage = Image.FromFile(file);
                            int originalWidth = 0;
                            originalWidth = loadedImage.Width;
                            int newWidth = 280;
                            newWidth = bc.imggridscanwidth;
                            resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
                            //row[colUploadImg] = resizedImage;
                            grfView.SetCellImage(i, colUploadImg, resizedImage);
                            //loadedImage.Dispose();
                        }
                    }
                    else
                    {
                        //row[colUploadImg] = Resources.pdf_symbol_300;
                    }
                }
                catch (Exception ex)
                {
                    new LogWriter("e", file + " " + ex.Message);
                    MessageBox.Show("err "+ file +" "+ ex.Message,"");
                }
            }
            grfView.Refresh();
            grfView.AutoSizeCols();
            grfView.AutoSizeRows();
        }
        private string FindQrCodeInImage(Bitmap bmp)
        {
            //decode the bitmap and try to find a qr code
            var source = new BitmapLuminanceSource(bmp);
            var bitmap = new BinaryBitmap(new HybridBinarizer(source));
            var result = new MultiFormatReader().decode(bitmap);

            //no qr code found in bitmap
            if (result == null)
            {
                rl1.Text ="No QR Code found!";
                return null;
            }
            return result.Text;
        }
        private void BtnCapture_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.Hide();
            System.Threading.Thread.Sleep(500);
            Application.DoEvents();
            FullScreenshot(Application.StartupPath, "capture.jpg", ImageFormat.Jpeg);
            Application.DoEvents();
            this.Show();
            //WindowScreenshotWithoutClass(Application.StartupPath, "capture1.jpg", ImageFormat.Jpeg);
            //CaptureScreenToFile("capture2.jpg", ImageFormat.Jpeg);
        }
        private void WindowScreenshotWithoutClass(String filepath, String filename, ImageFormat format)
        {
            Rectangle bounds = this.Bounds;

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }

                string fullpath = filepath + "\\" + filename;

                bitmap.Save(fullpath, format);
            }
        }
        private void FullScreenshot(String filepath, String filename, ImageFormat format)
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }

                string fullpath = filepath + "\\" + filename;

                bitmap.Save(fullpath, format);
            }
        }
        public Image CaptureScreen()
        {
            return CaptureWindow(User32.GetDesktopWindow());
        }
        public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);
        }
        public void CaptureScreenToFile(string filename, ImageFormat format)
        {
            Image img = CaptureScreen();
            img.Save(filename, format);
        }
        public Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up 
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
            return img;
        }
        private void FrmScreenCapture_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.Manual;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new System.Drawing.Point(5, screenHeight - this.Height - 40);
            //frmImg.Location = new System.Drawing.Point(this.Location.X + this.Width + 20, this.Top);
            this.Text = "Last Update 2024-07-24 bug vsdate, Notify Sound, Blood Bank ไม่ต้องพิมพ์, Auto Print Lab " ;
            //getListFile();
            txtCertID.Focus();
            String stationname ="";
            String[] deptno = bc.iniC.station.Split(',');
            foreach(String deptno1 in deptno)
            {
                stationname += bc.bcDB.pm32DB.getDeptNameOPD1(deptno1);
                DEPTNO = bc.bcDB.pm32DB.getDeptNoOPD(deptno1);
            }
            rl1.Text = "hostFTPDrugIn " + bc.iniC.hostFTPDrugIn+ " printerA4 " +bc.iniC.printerA4;
            rl2.Text = bc.iniC.statusStation + " printerA5 " + bc.iniC.printerA5;
            lfSbStation.Text = DEPTNO + "[" + bc.iniC.station + "]" + stationname;
            rgSbModule.Text = bc.iniC.hostDBMainHIS + " " + bc.iniC.nameDBMainHIS;
        }
    }
}
