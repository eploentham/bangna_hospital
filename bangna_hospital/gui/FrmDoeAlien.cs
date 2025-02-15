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
        C1FlexGrid grfOPD, grfVs, grfPDF, grfView, grfExcel;
        C1ThemeController theme1;
        C1FlexViewer fvCerti;
        String REGCODE = "";
        int colalcode = 1, colaltype = 2, colalprefix = 3, colalprefixen = 4, colalnameen = 5, colalsnamee=6, colalbdate=7, colalgender=8, colalnation=9, colalposid=10;
        int colvsdate = 1, colvshn =2, colvsalienname = 3, colvsalcode = 4, colvspre = 5;
        int colpdfname = 1, colpdfpath = 2;
        int colviewhn = 1, colviewname = 2, colviewvsdate = 3, colviewpreno = 4, colviewstatusdoe = 5;
        Boolean pageLoad = false;
        String FLAGTABDOE = "", HN="", VSDATE="", PRENO="", FILENAME="";
        Label lbLoading;
        MemoryStream STREAMCertiDOE;
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
            initFont();
            setEvent();
            setTheme();
            bc.bcDB.pttDB.setCboDeptOPD(cboDoeDeptNew, bc.iniC.station);
            initGrfOPD();
            initGrfVs();
            initGrfPDF();
            initGrfView();
            initLoading();
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
        }

        private void BtnDoeExcelOpen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xls;*.xlsx";
            ofd.Title = "Select Excel File";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //lbExcelFileName.Text = ofd.FileName;
                chkDoeExcel.Checked = true;
                C1XLBook _c1xl = new C1XLBook();
                _c1xl.Load(ofd.FileName);
                XLSheet sheet = _c1xl.Sheets[0];
                //grfOPD = new C1FlexGrid();
                //grfOPD.Font = fEdit;
                //grfOPD.Dock = System.Windows.Forms.DockStyle.Fill;
                grfOPD.Cols.Count = sheet.Columns.Count+1;
                grfOPD.Rows.Count = sheet.Rows.Count+1;
                //spExcel.Controls.Clear();
                //spExcel.Controls.Add(grfExcel);
                for (int i = 0; i < sheet.Rows.Count; i++)
                {
                    for (int j = 0; j < sheet.Columns.Count; j++)
                    {
                        grfOPD[i, j] = sheet[i, j].Text;
                    }
                }
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
            fvCerti.AutoScrollMargin = new System.Drawing.Size(0, 0);
            fvCerti.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            fvCerti.Dock = System.Windows.Forms.DockStyle.Fill;
            fvCerti.Location = new System.Drawing.Point(0, 0);
            fvCerti.Name = "fvCerti";
            fvCerti.Size = new System.Drawing.Size(1065, 790);
            fvCerti.TabIndex = 0;
            fvCerti.Ribbon.Minimized = true;
            panel3.Controls.Add(fvCerti);
            theme1.SetTheme(fvCerti, bc.iniC.themeApp);
            FtpClient ftpc = new FtpClient(bc.iniC.hostFTPCertMeddoe, bc.iniC.userFTPCertMeddoe, bc.iniC.passFTPCertMeddoe, false);
            MemoryStream streamCertiDoe = ftpc.download(filename);
            STREAMCertiDOE = new MemoryStream();
            streamCertiDoe.Position = 0;
            streamCertiDoe.CopyTo(STREAMCertiDOE, 2024);
            streamCertiDoe.Position = 0;
            STREAMCertiDOE.Position = 0;
            C1PdfDocumentSource pds = new C1PdfDocumentSource();
            pds.LoadFromStream(streamCertiDoe);
            fvCerti.DocumentSource = pds;
            FILENAME = filename;
            //ExtractImagesAndDecodeQRCode(file);

        }
        static void ExtractImagesAndDecodeQRCode(string pdfPath)
        {
            using (PdfReader pdfReader = new PdfReader(pdfPath))
            {
                using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
                {
                    for (int pageNumber = 1; pageNumber <= pdfDocument.GetNumberOfPages(); pageNumber++)
                    {
                        PdfPage page = pdfDocument.GetPage(pageNumber);
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
                        FtpClient ftpd = new FtpClient(bc.iniC.hostFTPCertMeddoe, bc.iniC.userFTPCertMeddoe, bc.iniC.passFTPCertMeddoe, false);
                        ftpd.delete(ftpfilename);
                        setGrfPDFFormFTP();
                        bc.bcDB.vsDB.updateVisitStatusDOEbyCertID(txtSendCertCertID.Text.Trim(), "2", "1618");
                        clearControlSendDOE();
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
        private void DoeAddVisit()
        {
            String err = "", compcode="", chkcomp="", prefix="";
            prefix = txtDoeAlprefixen.Text.Trim().Replace("MRS", "MRS.").Replace("MR", "MR.").Replace("MISS", "MISS.");
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetByPID(txtDoeAlcode.Text.Trim(), "pid");      //check ต่างด้าวให้ เปลี่ยนเป็น mnc_id_no 
            if (ptt.Hn.Length <= 0)
            {       //อยากจะทำ คือ ถ้าเคยมารักษาแล้ว ให้ใช้ HN เก่า

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
            if ((ptt.MNC_FNAME_T.Length <= 0) && (ptt.MNC_FNAME_E.Length <= 0))
            {
                lfSbMessage.Text = "ชื่อ ไม่ถูกต้อง";
                return;
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
            ptt.MNC_HN_YR = "";
            String re = bc.bcDB.pttDB.insertPatientStep1(ptt);
            if (long.TryParse(re, out long chk))
            {
                ptt = bc.bcDB.pttDB.selectPatinetByPID(txtDoeAlcode.Text.Trim(), "work_permit1");
                txtDoeHN.Value = ptt.Hn;
                txtDoevsdate.Value = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd");

                Visit vs = new Visit();
                vs.HN = txtDoeHN.Text.Trim();
                vs.PaidCode = txtDoePaidCode.Text.Trim();
                vs.symptom = txtDoeSymptom.Text.Trim();
                err = "01";
                vs.DeptCode = txtDoeDeptNew.Text.Trim();
                vs.remark = "doealien";
                err = "02";
                vs.VisitType = "P";//ใน source  Fieldนี้ MNC_PT_FLG
                vs.DoctorId = bc.iniC.dtrcode;      //IF CboDotCD.TEXT = '' THEN MNC_DOT_CD:= '00000'
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

                re = bc.bcDB.vsDB.insertVisit1(vs.HN, vs.PaidCode, vs.symptom, vs.DeptCode, vs.remark, vs.DoctorId, vs.VisitType, vs.compcode, vs.insurcode, "auto");
                if (int.TryParse(re, out int chk1))
                {
                    DataTable dtvs = bc.bcDB.vsDB.selectByPreno(vs.HN, txtDoevsdate.Text.Trim(),re);
                    if (dtvs != null)
                    {
                        foreach (DataRow dr in dtvs.Rows)
                        {
                            //และเข้าในว่า ต้องแก้ store procedure แก้ store procedure ต้องแก้โปรแกรมด้วยไหม ต้องหาคำตอบ
                            bc.bcDB.vsDB.updateVisitStatusDOE(vs.HN, txtDoevsdate.Text.Trim(), re, "1", txtDoeReqcode.Text.Trim(), "1618");
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

            pnDoeList.Controls.Add(grfOPD);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfOPD, bc.iniC.themegrfOpd);
        }

        private void GrfOPD_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfOPD.Row < 0) return;
            if (grfOPD[grfOPD.Row, colalcode] == null) return;
            if (chkDoeExcel.Checked)
            {

            }
            else
            {
                setDoeAienGrfOPDClick();
            }
        }
        private void setDoeAienGrfOPDClick()
        {
            showLbLoading();
            try
            {
                txtDoeAlcode.Value = grfOPD[grfOPD.Row, colalcode].ToString();
                txtDoeAltype.Value = grfOPD[grfOPD.Row, colaltype].ToString();
                txtDoeAlprefix.Value = grfOPD[grfOPD.Row, colalprefix].ToString();
                txtDoeAlprefixen.Value = grfOPD[grfOPD.Row, colalprefixen].ToString();
                txtDoeAlnameen.Value = grfOPD[grfOPD.Row, colalnameen].ToString();
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
            }
            catch(Exception ex)
            {
                //MessageBox.Show("error " + ex.Message, "");
            }
        }
        private void setControlSendDOE(String certid)
        {
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
            //txtSendCerthostname.Value = bc.iniC.hostname;
            txtSendCerthn.Value = HN;
            txtSendCertvsdate.Value = bc.datetoShow1(VSDATE);
            txtSendCertCertID.Value = vs.certi_id;
            txtSendCertalcode.Value = vs.ID;
            //txtSendCertprovcode.Text = bc.iniC.provcode;
            txtSendCerturlBangna.Text = bc.iniC.urlbangnadoeresult;
            txtSendCertdtrcode.Value = vs.MNC_DOT_CD;
            txtSendCertdtrName.Text = bc.selectDoctorName(vs.MNC_DOT_CD);
            lbSendCertPttName.Text = vs.PatientName;
            lfSbMessage.Text = "";
            txtSendCertCertID.Focus();
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
            showPDF(grfPDF[grfPDF.Row, colpdfpath].ToString());
            FILENAME = grfPDF[grfPDF.Row, colpdfpath].ToString();
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
            //spExcelMain.HeaderHeight = 0;
            if (FLAGTABDOE.Equals("namelist"))
            {//ดึงข้อมูลจาก DOE
                tabMain.SelectedTab = tabGetNameList;
                tabSendCert.TabVisible = false;
                tabDoeView.TabVisible = false;
                //tabExcel.TabVisible = false;
                setDoeAienGrfOPD();
            }
            else if (FLAGTABDOE.Equals("formfolder"))
            {//ดึง PDF จาก folder เพื่อดูว่ามี pdf ที่ยังไม่ได้ส่ง และทำการส่ง
                tabMain.SelectedTab = tabSendCert;
                tabGetNameList.TabVisible = false;
                tabDoeView.TabVisible = false;
                //tabExcel.TabVisible = false;
                setGrfPDFFormFTP();
            }
            else if (FLAGTABDOE.Equals("viewdoe"))
            {//ดึง PDF จาก folder เพื่อดูว่ามี pdf ที่ยังไม่ได้ส่ง และทำการส่ง
                tabMain.SelectedTab = tabDoeView;
                tabGetNameList.TabVisible = false;
                tabSendCert.TabVisible = false;
                //tabExcel.TabVisible = false;
                setGrfPDFFormFTP();
            }
            
            else
            {//ส่ง PDF จากการเลือกหน้า OPD โดยมี HN, cert_id เลือกมาเรียบร้อย
                tabGetNameList.TabVisible = true;
                tabDoeView.TabVisible = true;
                tabSendCert.TabVisible = true;
                //tabExcel.TabVisible = true;
                tabMain.SelectedTab = tabSendCert;
                setControlSendDOE("");
            }
            
        }
    }
}
