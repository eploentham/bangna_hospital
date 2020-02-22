using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Command;
using C1.Win.C1Document;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using Ionic.Zip;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Path = System.IO.Path;

namespace bangna_hospital.gui
{
    public class FrmLabOutReceive1:Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;
        //FileSystemWatcher watcher;
        //ListBox listBox1;
        System.Windows.Forms.Timer timer;
        Boolean chkFileInnoTech = false, chkFileRIA = false;
        C1DockingTab tC1;
        C1DockingTabPage tabAuto, tabManual;
        Panel pnAuto, pnManual, panel1, pnLeft, pnRightTop, pnRightBotton;
        Label lbStep1, lbStep2, lbPttNmae, lbVisitStatus, lbOutLabDate, lbMessage;
        C1Button btnBrow, btnUpload;
        C1TextBox txtFilename, txtHn, txtVnAn, txtVsDate;
        C1FlexGrid grfVisit;
        RadioButton chkOPD, chkIPD;

        int colVsVsDate = 1, colVsVn = 2, colVsStatus = 3, colVsDept = 4, colVsPreno = 5, colVsAn = 6, colVsAndate = 7;
        
        Patient ptt;
        String preno = "";

        private C1.Win.C1Ribbon.C1StatusBar sb1;
        private C1.Win.C1Themes.C1ThemeController theme1;
        private System.Windows.Forms.ListBox listBox1, listBox2, listBox3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public FrmLabOutReceive1(BangnaControl bc)
        {
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            initCompoment();

            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            timer = new System.Windows.Forms.Timer();
            timer.Enabled = true;
            timer.Interval = bc.timerCheckLabOut * 1000;
            timer.Tick += Timer_Tick;
            this.Load += FrmLabOutReceive1_Load;
            btnUpload.Click += BtnUpload_Click;
            btnBrow.Click += BtnBrow_Click;
            txtHn.KeyUp += TxtHn_KeyUp;
            grfVisit.RowColChange += GrfVisit_RowColChange;
            chkOPD.Click += ChkOPD_Click;
            chkIPD.Click += ChkIPD_Click;
        }

        private void ChkIPD_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrf();
        }

        private void ChkOPD_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrf();
        }
        private void setGrf()
        {
            setGrfVsH();
            setGrfVsOPD();
        }
        private void setControlHN()
        {
            ptt = bc.bcDB.pttDB.selectPatinet(txtHn.Text.Trim());
            lbPttNmae.Text = ptt.Name +" Age "+ptt.AgeStringShort();
        }
        private void initCompoment()
        {
            int gapLine = 20, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;

            this.sb1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.theme1 = new C1.Win.C1Themes.C1ThemeController();
            tC1 = new C1.Win.C1Command.C1DockingTab();
            tabAuto = new C1.Win.C1Command.C1DockingTabPage();
            tabManual = new C1.Win.C1Command.C1DockingTabPage();
            pnAuto = new Panel();
            pnManual = new Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            pnAuto.SuspendLayout();
            pnManual.SuspendLayout();
            tC1.SuspendLayout();
            tabAuto.SuspendLayout();
            tabManual.SuspendLayout();
            this.SuspendLayout();

            this.sb1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.sb1.Location = new System.Drawing.Point(0, 620);
            this.sb1.Name = "sb1";
            this.sb1.Size = new System.Drawing.Size(956, 22);
            this.sb1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Custom;

            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(956, 620);
            this.panel1.TabIndex = 0;

            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(467, 450);
            this.listBox1.TabIndex = 0;
            this.listBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(0, 0);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(329, 355);
            this.listBox3.TabIndex = 0;
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(0, 0);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(329, 300);
            this.listBox2.TabIndex = 0;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 467;
            this.splitContainer1.TabIndex = 0;

            tC1.Dock = System.Windows.Forms.DockStyle.Fill;
            tC1.HotTrack = true;
            tC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tC1.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            tC1.TabsShowFocusCues = true;
            tC1.Alignment = TabAlignment.Bottom;
            tC1.SelectedTabBold = true;

            pnAuto.Dock = DockStyle.Fill;
            pnManual.Dock = DockStyle.Fill;

            tabAuto.Name = "tabAuto";
            tabAuto.TabIndex = 0;
            tabAuto.Text = "Auto";

            tabManual.Name = "tabManual";
            tabManual.TabIndex = 0;
            tabManual.Text = "Manual";

            lbStep1 = new Label();
            lbStep1.Text = "Step 1 Browe File PDF ";
            lbStep1.Font = fEdit;
            lbStep1.Location = new System.Drawing.Point(gapX, 10);
            lbStep1.AutoSize = true;
            size = bc.MeasureString(lbStep1);
            btnBrow = new C1Button();
            btnBrow.Name = "btnBrow";
            btnBrow.Text = "...";
            btnBrow.Font = fEdit;
            btnBrow.Location = new System.Drawing.Point(gapX + size.Width, lbStep1.Location.Y);
            btnBrow.Size = new Size(30, lbStep1.Height);
            btnBrow.Font = fEdit;
            
            txtFilename = new C1TextBox();
            txtFilename.Font = fEdit;
            txtFilename.Location = new System.Drawing.Point(btnBrow.Location.X + btnBrow.Width + 5, btnBrow.Location.Y);
            txtFilename.Size = new Size(400, btnBrow.Height+5);

            lbStep2 = new Label();
            lbStep2.Text = "Step 2 HN :";
            lbStep2.Font = fEdit;
            lbStep2.Location = new System.Drawing.Point(gapX, gapLine + txtFilename.Height);
            lbStep2.AutoSize = true;
            size = bc.MeasureString(lbStep2);
            txtHn = new C1TextBox();
            txtHn.Font = fEdit;
            txtHn.Location = new System.Drawing.Point(gapX + size.Width + 5, lbStep2.Location.Y);
            txtHn.Size = new Size(80, btnBrow.Height + 5);
            
            lbPttNmae = new Label();
            lbPttNmae.Text = "xxxx";
            lbPttNmae.Font = fEdit;
            lbPttNmae.Location = new System.Drawing.Point(txtHn.Location.X + txtHn.Width + 5, lbStep2.Location.Y);
            lbPttNmae.AutoSize = true;
            lbOutLabDate = new Label();
            lbOutLabDate.Text = "xxxx";
            lbOutLabDate.Font = fEdit;
            lbOutLabDate.Location = new System.Drawing.Point(txtHn.Location.X + txtHn.Width + 250, lbStep2.Location.Y);
            lbOutLabDate.AutoSize = true;

            lbVisitStatus = new Label();
            lbVisitStatus.Text = "สถานะ visit";
            lbVisitStatus.Font = fEdit;
            lbVisitStatus.Location = new System.Drawing.Point(gapX, gapLine + txtHn.Location.Y + txtHn.Height);
            lbVisitStatus.AutoSize = true;
            size = bc.MeasureString(lbVisitStatus);
            chkOPD = new RadioButton();
            chkOPD.Text = "OPD";
            chkOPD.Font = fEdit;
            chkOPD.Location = new System.Drawing.Point(lbVisitStatus.Location.X + size.Width + 5, lbVisitStatus.Location.Y);
            chkOPD.AutoSize = true;
            size = bc.MeasureString(chkOPD);
            chkIPD = new RadioButton();
            chkIPD.Text = "IPD";
            chkIPD.Font = fEdit;
            chkIPD.Location = new System.Drawing.Point(chkOPD.Location.X + size.Width + 25, chkOPD.Location.Y);
            chkIPD.AutoSize = true;
            txtVnAn = new C1TextBox();
            txtVnAn.Font = fEdit;
            txtVnAn.Location = new System.Drawing.Point(chkIPD.Location.X + size.Width + 25, chkOPD.Location.Y);
            txtVnAn.Size = new Size(80, btnBrow.Height + 5);
            txtVsDate = new C1TextBox();
            txtVsDate.Font = fEdit;
            txtVsDate.Location = new System.Drawing.Point(txtVnAn.Location.X + txtVnAn.Width + 25, txtVnAn.Location.Y);
            txtVsDate.Size = new Size(80, btnBrow.Height + 5);
            btnUpload = new C1Button();
            btnUpload.Name = "btnUpload";
            btnUpload.Text = "Upload";
            size = bc.MeasureString(btnUpload);
            btnUpload.Font = fEdit;
            btnUpload.Location = new System.Drawing.Point(txtVsDate.Location.X+ txtVsDate.Width+10, chkOPD.Location.Y);
            btnUpload.Size = new Size(size.Width+15, lbStep1.Height);
            btnUpload.Font = fEdit;
            lbMessage = new Label();
            lbMessage.Text = "";
            lbMessage.Font = fEdit;
            lbMessage.Location = new System.Drawing.Point(btnUpload.Location.X + btnUpload.Width + 10, chkOPD.Location.Y);
            lbMessage.AutoSize = true;

            grfVisit = new C1FlexGrid();
            grfVisit.Font = fEdit;
            grfVisit.Location = new System.Drawing.Point(gapX, gapLine + chkOPD.Location.Y+10);
            grfVisit.Size = new Size(900, 300);
            

            this.theme1.Theme = "BeigeOne";

            this.Controls.Add(panel1);
            this.Controls.Add(this.sb1);
            panel1.Controls.Add(tC1);
            tC1.Controls.Add(tabAuto);
            tC1.Controls.Add(tabManual);
            tabAuto.Controls.Add(pnAuto);
            tabManual.Controls.Add(pnManual);
            pnAuto.Controls.Add(splitContainer1);
            splitContainer1.Panel1.Controls.Add(listBox1);
            splitContainer1.Panel2.Controls.Add(listBox2);
            splitContainer1.Panel2.Controls.Add(listBox3);
            pnManual.Controls.Add(lbStep1);
            pnManual.Controls.Add(btnBrow);
            pnManual.Controls.Add(txtFilename);
            pnManual.Controls.Add(lbStep2);
            pnManual.Controls.Add(txtHn);
            pnManual.Controls.Add(lbPttNmae);
            pnManual.Controls.Add(lbOutLabDate);
            pnManual.Controls.Add(lbVisitStatus);
            pnManual.Controls.Add(chkOPD);
            pnManual.Controls.Add(chkIPD);
            pnManual.Controls.Add(txtVnAn);
            pnManual.Controls.Add(txtVsDate);
            pnManual.Controls.Add(btnUpload);
            pnManual.Controls.Add(lbMessage);
            pnManual.Controls.Add(grfVisit);

            this.WindowState = FormWindowState.Maximized;

            panel1.ResumeLayout(false);
            pnAuto.ResumeLayout(false);
            pnManual.ResumeLayout(false);
            tC1.ResumeLayout(false);
            tabAuto.ResumeLayout(false);
            tabManual.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (MessageBox.Show("ต้องการ Upload ช้อมูล \n" + txtHn.Text+" " + lbPttNmae.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                uploadFiletoServerMedica();
            }
        }

        private void GrfVisit_RowColChange(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfVisit.Row <= 0) return;
            if (grfVisit.Col <= 0) return;
            if (grfVisit[grfVisit.Row, colVsVn] == null) return;

            if (chkOPD.Checked)
            {
                txtVnAn.Value = grfVisit[grfVisit.Row, colVsVn].ToString().Trim();
            }
            else if (chkIPD.Checked)
            {
                txtVnAn.Value = grfVisit[grfVisit.Row, colVsAn].ToString().Trim();
            }
            preno = grfVisit[grfVisit.Row, colVsPreno].ToString().Trim();
            txtVsDate.Value = grfVisit[grfVisit.Row, colVsVsDate].ToString().Trim();
        }

        private void TxtHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                setControlHN();
                setGrf();
            }
        }

        private void BtnBrow_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd;
            ofd = new OpenFileDialog();
            ofd.Title = "Browse PDF Files";
            ofd.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.ReadOnlyChecked = true;
            ofd.ShowReadOnly = true;
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lbMessage.Text = "";
                txtHn.Value = "";
                txtVnAn.Value = "";
                txtVsDate.Value = "";
                preno = "";
                grfVisit.Rows.Count = 1;
                lbOutLabDate.Text = "";
                lbOutLabDate.Text = "";
                txtFilename.Value = ofd.FileName;
                int pagesToScan = 2;
                string strText = "", outPath="";
                PdfReader reader = new PdfReader(ofd.FileName);
                try
                {
                    for (int page = 1; page <= pagesToScan; page++) //(int page = 1; page <= reader.NumberOfPages; page++) <- for scanning all the pages in A PDF
                    {
                        ITextExtractionStrategy its = new LocationTextExtractionStrategy();
                        strText = PdfTextExtractor.GetTextFromPage(reader, page, its);

                        //strText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(strText)));
                        //creating the string array and storing the PDF line by line
                        string[] lines = strText.Split('\n');
                        foreach (string line in lines)
                        {
                            //Creating and appending to a text file
                            //using (StreamWriter file = new StreamWriter(outPath, true))
                            //{
                                // file.WriteLine(line);

                            int indexpttname = line.LastIndexOf("PATIENT NAME");
                            if (indexpttname >= 0)
                            {
                                var pttname = line.Substring(indexpttname, (line.Length - indexpttname));
                                lbPttNmae.Text = pttname.Replace("PATIENT NAME", "").Trim();
                            }
                            int indexhn = line.LastIndexOf("HN");
                            if (indexhn >= 0)
                            {
                                var pttname = line.Substring(indexhn, (line.Length - indexhn));
                                txtHn.Value = pttname.Replace("HN", "").Trim();
                            }
                            int indexoutlabdate = line.LastIndexOf("REGISTERED DATE");
                            if (indexoutlabdate >= 0)
                            {
                                var pttname = line.Substring(indexoutlabdate, (line.Length - indexoutlabdate));
                                String txt = "";
                                txt = pttname.Replace("REGISTERED DATE", "").Trim();
                                if (lbOutLabDate.Text.Length <= 0)
                                {
                                    lbOutLabDate.Text = txt;
                                }
                            }
                            else
                            {
                                indexoutlabdate = line.LastIndexOf("OPD1");
                                if (indexoutlabdate >= 0)
                                {
                                    var pttname = line.Substring(indexoutlabdate, (line.Length - indexoutlabdate));
                                    String txt = "";
                                    txt = pttname.Replace("OPD1", "").Trim();
                                    if (lbOutLabDate.Text.Length <= 0)
                                    {
                                        lbOutLabDate.Text = txt;
                                    }
                                }
                            }

                            //}
                        }
                    }
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "BtnBrow_Click " + ex.Message);
                    lbMessage.Text = "error " + ex.Message;
                    Console.Write(ex);
                }
                finally
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //new LogWriter("d", "Timer_Tick 01");
            getFileinFolderInnoTech();
            //new LogWriter("d", "Timer_Tick 02");
            getFileinFolderRIA();
            //new LogWriter("d", "Timer_Tick 03");
            if (chkFileInnoTech)
            {
                uploadFiletoServerInnoTech();
            }
            if (chkFileRIA)
            {
                uploadFiletoServerRIA();
            }
        }
        private void getFileinFolderRIA()
        {
            DirectoryInfo d = new DirectoryInfo(bc.iniC.pathLabOutReceiveRIA);         
            FileInfo[] Files = d.GetFiles("*.zip"); //Getting Text files
            string str = "";
            new LogWriter("d", "Timer_Tick 03");
            foreach (FileInfo file in Files)
            {
                //str = str + ", " + file.Name;
                listBox3.BeginUpdate();     //listBox1
                listBox3.Items.Add(file.FullName);     //listBox1
                listBox3.EndUpdate();     //listBox1
                chkFileRIA = true;
            }
            //}
        }
        private void uploadFiletoServerRIA()
        {
            timer.Stop();
            listBox2.Items.Clear();     //listBox3
            Application.DoEvents();
            Thread.Sleep(1000);
            //new LogWriter("d", "uploadFiletoServerRIA 01");
            if (!Directory.Exists(bc.iniC.pathLabOutBackupRIAZipExtract))
            {
                Directory.CreateDirectory(bc.iniC.pathLabOutBackupRIAZipExtract);
            }
            if (!Directory.Exists(bc.iniC.pathLabOutBackupRIA))
            {
                Directory.CreateDirectory(bc.iniC.pathLabOutBackupRIA);
            }
            //new LogWriter("d", "uploadFiletoServerRIA 02");
            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
            //String[] filePaths = Directory.GetFiles(bc.iniC.pathLabOutReceive, "*.*", SearchOption.TopDirectoryOnly);
            List<String> filePaths = new List<String>();
            DirectoryInfo dir = new DirectoryInfo(bc.iniC.pathLabOutReceiveRIA);
            FileInfo[] Files = dir.GetFiles("*.zip"); //Getting Text files
            foreach (FileInfo file in Files)
            {
                //Console.WriteLine("The number of files in {0} is {1}", diNext, diNext.GetFiles().Length);
                string str = "";
                filePaths.Add(file.FullName);
            }
            //new LogWriter("d", "uploadFiletoServerRIA 03");
            String dgssid = "";
            dgssid = bc.bcDB.dgssDB.getIdDgss("Document Other");
            DocGroupSubScan dgss = new DocGroupSubScan();
            dgss = bc.bcDB.dgssDB.selectByPk(dgssid);
            foreach (String zipFilename in filePaths)
            {
                listBox2.Items.Add("พบ file " + zipFilename);
                new LogWriter("d", "uploadFiletoServerRIA zipFilename "+ zipFilename);
                Application.DoEvents();
                int year2 = 0;
                String yy = "", mm = "", dd = "", reqid = "", vn = "", datetick1 = "", pathbackup = "", year1 = "", ext = "", pathname = "", tmp = "", filename2 = "", hn = "", filenamePDF = "";
                var fileStreamJson = new MemoryStream();
                var fileStreamPDF = new MemoryStream();
                //Json

                datetick1 = DateTime.Now.Ticks.ToString();
                pathbackup = bc.iniC.pathLabOutBackupRIA + "\\" + datetick1;
                if (!Directory.Exists(pathbackup))
                {
                    Directory.CreateDirectory(pathbackup);
                }
                //using (var zip = ZipFile.Read(filename))
                //{
                //    foreach (var entry in zip)
                //    {
                //        ext = Path.GetExtension(entry.FileName);
                //        if (ext.ToLower().Equals(".json"))
                //        {
                //            entry.Extract(fileStreamJson);
                //            fileStreamJson.Seek(0, SeekOrigin.Begin);
                //            StreamReader reader = new StreamReader(fileStreamJson, System.Text.Encoding.UTF8, true);
                //            using (JsonTextReader readerJson = new JsonTextReader(reader))
                //            {
                //                //JObject o2 = (JObject)JToken.ReadFrom(readerJson);
                //                //RIA_Patient account = JsonConvert.DeserializeObject<RIA_Patient>(readerJson);
                //                string jsonData1 = @"{'idcard': '', 'hn': '', title:'', fname:'', lname:'', sex:'', dob:''}";
                //                JsonSerializer serializer = new JsonSerializer();
                //                var movie2 = serializer.Deserialize(readerJson);
                //                JObject o2 = (JObject)JToken.ReadFrom(readerJson);
                //                dynamic obj = o2["patient"];
                //                //dynamic obj = JsonConvert.DeserializeObject(movie2.ToString());
                //                //var jsonData = JObject.Parse(movie2.ToString());
                //                //RIA_Patient account = (RIA_Patient)jsonData[0]["hn"].ToString();
                //                //RIA_Patient rptt = new RIA_Patient();
                //                //rptt = (RIA_Patient)movie2[0];
                //            }
                //        }
                //        else if (ext.ToLower().Equals(".pdf"))
                //        {
                //            entry.Extract(fileStreamPDF);
                //            fileStreamPDF.Seek(0, SeekOrigin.Begin);

                //        }
                //    }
                //}
                RIA_Patient aaa = new RIA_Patient();
                ZipFile zf = new ZipFile(zipFilename);
                zf.ExtractAll(pathbackup);
                DirectoryInfo dZip = new DirectoryInfo(pathbackup);
                FileInfo[] filesZip = dZip.GetFiles("*.*");
                //new LogWriter("d", "uploadFiletoServerRIA foreach (String zipFilename in filePaths) ");
                foreach (FileInfo file in filesZip)
                {
                    ext = Path.GetExtension(file.FullName);
                    if (ext.ToLower().Equals(".json"))
                    {
                        using (StreamReader file1 = File.OpenText(file.FullName))
                        using (JsonTextReader reader = new JsonTextReader(file1))
                        {
                            JObject o2 = (JObject)JToken.ReadFrom(reader);

                            dynamic objPtt = o2["patient"];
                            dynamic objOrd = o2["orderdetail"];
                            aaa.hn = objPtt.hn;
                            aaa.idcard = objPtt.idcard;
                            aaa.fname = objPtt.fname;
                            aaa.lname = objPtt.lname;
                            aaa.ref_no = objOrd.ref_no;
                            aaa.order_number = objOrd.order_number;
                            aaa.ln = objOrd.ln;
                            aaa.hn_customer = objOrd.hn_customer;
                            aaa.status = objOrd.status;
                            aaa.comment_order = objOrd.comment_order;
                            aaa.comment_patient = objOrd.comment_patient;
                            aaa.ward_customer = objOrd.ward_customer;
                            aaa.doctor = objOrd.doctor;
                            aaa.time_register = objOrd.time_register;
                            aaa.ward_customer = objOrd.ward_customer;
                            filename2 = aaa.ref_no;
                            //aaa = (RIA_Patient)o2["patient"];
                            //IList<JToken> results = o2["patient"]["fname"].Children().ToList();
                        }
                    }
                    else if (ext.ToLower().Equals(".pdf"))
                    {
                        filenamePDF = file.FullName;
                    }
                }
                filesZip = null;
                if (filename2.Length <= 0)
                {
                    MessageBox.Show("aaaaaa", "");
                    return;
                }
                dZip = null;
                zf.Dispose();
                reqid = filename2.Substring(filename2.Length - 3);
                yy = filename2.Substring(filename2.Length - 5, 2);
                mm = filename2.Substring(filename2.Length - 7, 2);
                dd = filename2.Substring(filename2.Length - 9, 2);
                year1 = "20" + yy;
                DataTable dt = new DataTable();
                dt = bc.bcDB.vsDB.SelectHnLabOut(reqid, year1 + "-" + mm + "-" + dd);
                if (dt.Rows.Count <= 0)
                {
                    listBox2.Items.Add("Filename ไม่พบข้อมูล HIS " + zipFilename);
                    Application.DoEvents();
                    String datetick = "";
                    new LogWriter("e", "Filename ไม่พบข้อมูล HIS  reqid " + reqid +" "+ year1 + "-" + mm + "-" + dd);
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupRIA))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupRIA);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupRIA + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupRIA + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(zipFilename, bc.iniC.pathLabOutBackupRIA + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(zipFilename, bc.iniC.pathLabOutBackupRIA + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                listBox2.Items.Add("พบข้อมูล HIS " + dt.Rows[0]["mnc_hn_no"].ToString());
                Application.DoEvents();
                if (!dt.Rows[0]["mnc_hn_no"].ToString().Equals(aaa.hn_customer))
                {
                    MessageBox.Show("aaaaaa", "");
                }
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = dgss.doc_group_id;
                dsc.hn = dt.Rows[0]["mnc_hn_no"].ToString();
                dsc.vn = dt.Rows[0]["MNC_VN_NO"].ToString() + "/" + dt.Rows[0]["MNC_VN_SEQ"].ToString() + "(" + dt.Rows[0]["MNC_VN_SUM"].ToString() + ")";
                dsc.an = dt.Rows[0]["MNC_AN_NO"].ToString() + "/" + dt.Rows[0]["MNC_AN_YR"].ToString();
                
                dsc.an = dsc.an.Equals("/") ? "" : dsc.an;
                dsc.visit_date = "";
                dsc.host_ftp = bc.iniC.hostFTP;
                //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                dsc.image_path = "";
                dsc.doc_group_sub_id = dgssid;
                dsc.pre_no = dt.Rows[0]["mnc_pre_no"].ToString();
                //dsc.an = "";
                //DateTime dt = new DateTime();

                //    dsc.an_date = (DateTime.TryParse(txtAnDate.Text, out dt)) ? bc.datetoDB(txtAnDate.Text) : "";
                //    if (dsc.an_date.Equals("1-01-01"))
                //    {
                //        dsc.an_date = "";
                //    }
                dsc.folder_ftp = bc.iniC.folderFTP;
                //    dsc.status_ipd = chkIPD.Checked ? "I" : "O";
                dsc.row_no = "1";
                dsc.row_cnt = "1";
                dsc.status_version = "2";
                dsc.req_id = dt.Rows[0]["mnc_req_no"].ToString();
                DateTime dtt = new DateTime();
                if (DateTime.TryParse(dt.Rows[0]["mnc_req_dat"].ToString(), out dtt))
                {
                    dsc.date_req = dtt.Year.ToString() + "-" + dtt.ToString("MM-dd");
                }
                else
                {
                    dsc.date_req = "";
                }
                dsc.status_ipd = dsc.an.Length > 0 ? "1" : "0";
                //dsc.ml_fm = "FM-LAB-999";

                dsc.ml_fm = "FM-LAB-996";       //RIA

                dsc.patient_fullname = dt.Rows[0]["mnc_patname"].ToString();
                dsc.status_record = "2";
                String re = bc.bcDB.dscDB.insertLabOut(dsc, bc.userId);
                if (re.Length <= 0)
                {
                    listBox2.Items.Add("ไม่ได้เลขที่ " + zipFilename);
                    Application.DoEvents();
                    String datetick = "";
                    new LogWriter("e", "ไม่ได้เลขที่ " + zipFilename);
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();

                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(zipFilename, pathbackup + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(zipFilename, pathbackup + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                listBox2.Items.Add("ได้เลขที่ " + re);
                Application.DoEvents();
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString() + "//" + dt.Rows[0]["mnc_hn_no"].ToString() + "_" + re + ext;
                
                vn = dsc.vn.Replace("/", "_").Replace("(", "_").Replace(")", "");

                dsc.ml_fm = "FM-LAB-996";       //RIA
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "-" + vn + "-" + re + ext;         //+1                

                String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                listBox2.Items.Add("updateImagepath " + dsc.image_path);
                Application.DoEvents();
                //    //MessageBox.Show("111", "");

                ftp.createDirectory(bc.iniC.folderFTP + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-"));       // สร้าง Folder HN
                //    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn);
                //    //MessageBox.Show("222", "");
                Thread.Sleep(200);
                ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                //    //MessageBox.Show("333", "");
                Thread.Sleep(200);
                if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, zipFilename))
                {
                    Thread.Sleep(200);
                    if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filenamePDF))
                    {
                        listBox2.Items.Add("FTP upload success ");
                        Application.DoEvents();
                        Thread.Sleep(1000);
                        String datetick = "";
                        datetick = DateTime.Now.Ticks.ToString();
                        
                        Thread.Sleep(1000);
                        if (File.Exists(zipFilename))
                        {
                            //dZip.MoveTo("");
                            try
                            {
                                File.Move(zipFilename, pathbackup + "\\" + filename2 + "_" + datetick + ".zip");
                            }
                            catch (Exception ex)
                            {
                                String aaaa = "";
                            }

                        }
                    }
                    //else
                    //{
                    //    File.Move(zipFilename, pathbackup + "\\" + filename2 + "_" + datetick + ".zip");
                    //}
                    listBox1.BeginUpdate();     //listBox2
                    listBox1.Items.Add(zipFilename + " -> " + bc.iniC.hostFTP + "//" + bc.iniC.folderFTP + "//" + dsc.image_path);     //listBox2
                    listBox1.EndUpdate();     //listBox2
                    Application.DoEvents();
                    Thread.Sleep(1000 * 60);
                }
                else
                {
                    listBox2.Items.Add("FTP upload success ");
                    Application.DoEvents();
                    new LogWriter("e", "FTP upload no success");
                }
            }
            timer.Start();
        }
        private void getFileinFolderInnoTech()
        {
            listBox3.Items.Clear();     //listBox1
            new LogWriter("d", "getFileinFolderInnoTech "+ bc.iniC.pathLabOutReceiveInnoTech);
            DirectoryInfo d = new DirectoryInfo(bc.iniC.pathLabOutReceiveInnoTech);//Assuming Test is your Folder
            DirectoryInfo[] dirs = d.GetDirectories();
            foreach (DirectoryInfo diNext in dirs)
            {
                Console.WriteLine("The number of files in {0} is {1}", diNext, diNext.GetFiles().Length);
                FileInfo[] Files = diNext.GetFiles("*.*"); //Getting Text files
                string str = "";
                foreach (FileInfo file in Files)
                {
                    //str = str + ", " + file.Name;
                    listBox3.BeginUpdate();     //listBox1
                    listBox3.Items.Add(file.FullName);     //listBox1
                    listBox3.EndUpdate();     //listBox1
                    chkFileInnoTech = true;
                }
            }
        }
        private void uploadFiletoServerInnoTech()
        {
            timer.Stop();
            listBox2.Items.Clear();     //listBox3
            Application.DoEvents();
            Thread.Sleep(1000);

            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
            //String[] filePaths = Directory.GetFiles(bc.iniC.pathLabOutReceive, "*.*", SearchOption.TopDirectoryOnly);
            List<String> filePaths = new List<String>();
            DirectoryInfo d = new DirectoryInfo(bc.iniC.pathLabOutReceiveInnoTech);//Assuming Test is your Folder
            DirectoryInfo[] dirs = d.GetDirectories();
            foreach (DirectoryInfo diNext in dirs)
            {
                Console.WriteLine("The number of files in {0} is {1}", diNext, diNext.GetFiles().Length);
                FileInfo[] Files = diNext.GetFiles("*.*"); //Getting Text files
                string str = "";
                foreach (FileInfo file in Files)
                {
                    filePaths.Add(file.FullName);
                }
            }
            //new LogWriter("d", "uploadFiletoServerInnoTech 01" );
            String dgssid = "";
            dgssid = bc.bcDB.dgssDB.getIdDgss("Document Other");
            DocGroupSubScan dgss = new DocGroupSubScan();
            dgss = bc.bcDB.dgssDB.selectByPk(dgssid);
            foreach (String filename in filePaths)
            {
                listBox2.Items.Add("พบ file " + filename);
                Application.DoEvents();
                int year2 = 0;
                String yy = "", mm = "", dd = "", reqid = "", vn = "", filename1 = "", filename2 = "", year1 = "", ext = "";
                String pathname = "", tmp = "";
                //tmp = bc.iniC.pathLabOutReceiveInnoTech.Replace("\\\\", "\\");
                filename1 = Path.GetFileName(filename);
                pathname = filename.Replace(filename1, "").Replace(tmp, "").Replace("\\", "");
                pathname = pathname.Replace("\\", "");
                String[] txt = filename1.Split('_');
                if (txt.Length > 1)
                {
                    filename2 = txt[0];
                }
                else
                {
                    filename2 = filename1.Replace(".pdf", "");
                }
                ext = Path.GetExtension(filename1);

                if (filename2.Replace(".pdf", "").Length < 10)
                {
                    String datetick = "";
                    new LogWriter("e", "Filename ไม่ถูก FORMAT");
                    listBox2.Items.Add("Filename ไม่ถูก FORMAT " + filename);
                    //MessageBox.Show("Filename ไม่ถูก FORMAT", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                if (filename2.Length <= 10)
                {
                    String datetick = "";
                    new LogWriter("e", "Filename ชื่อ File สั้นไป " + filename);
                    listBox2.Items.Add("Filename ชื่อ File สั้นไป " + filename);
                    //MessageBox.Show("Filename ไม่ถูก FORMAT", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                DateTime dtt1 = new DateTime();
                int.TryParse(year1, out year2);
                yy = filename2.Substring(filename2.Length - 5, 2);
                mm = filename2.Substring(filename2.Length - 7, 2);
                dd = filename2.Substring(filename2.Length - 9, 2);
                year1 = "20" + yy;
                if (!DateTime.TryParse(year1 + "-" + mm + "-" + dd, out dtt1))
                {
                    String datetick = "";
                    new LogWriter("e", "Filename ชื่อ File ไม่สามารถหา date ได้ " + filename);
                    listBox2.Items.Add("Filename ชื่อ File ไม่สามารถหา date ได้ " + filename);
                    //MessageBox.Show("Filename ไม่ถูก FORMAT", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                reqid = filename2.Substring(filename2.Length - 3);

                DataTable dt = new DataTable();
                dt = bc.bcDB.vsDB.SelectHnLabOut(reqid, year1 + "-" + mm + "-" + dd);
                if (dt.Rows.Count <= 0)
                {
                    listBox2.Items.Add("Filename ไม่พบข้อมูล HIS " + filename+ " reqid " + reqid+" "+ year1 + "-" + mm + "-" + dd);
                    Application.DoEvents();
                    String datetick = "";
                    new LogWriter("e", "Filename ไม่พบข้อมูล HIS");
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                listBox2.Items.Add("พบข้อมูล HIS " + dt.Rows[0]["mnc_hn_no"].ToString());
                Application.DoEvents();
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = dgss.doc_group_id;
                dsc.hn = dt.Rows[0]["mnc_hn_no"].ToString();
                dsc.vn = dt.Rows[0]["MNC_VN_NO"].ToString() + "/" + dt.Rows[0]["MNC_VN_SEQ"].ToString() + "(" + dt.Rows[0]["MNC_VN_SUM"].ToString() + ")";
                dsc.an = dt.Rows[0]["MNC_AN_NO"].ToString() + "/" + dt.Rows[0]["MNC_AN_YR"].ToString();
                if (dsc.an.Equals("/"))
                {
                    dsc.an = "";
                }
                dsc.visit_date = "";
                dsc.host_ftp = bc.iniC.hostFTP;
                //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                dsc.image_path = "";
                dsc.doc_group_sub_id = dgssid;
                dsc.pre_no = dt.Rows[0]["mnc_pre_no"].ToString();
                //dsc.an = "";
                //DateTime dt = new DateTime();

                //    dsc.an_date = (DateTime.TryParse(txtAnDate.Text, out dt)) ? bc.datetoDB(txtAnDate.Text) : "";
                //    if (dsc.an_date.Equals("1-01-01"))
                //    {
                //        dsc.an_date = "";
                //    }
                dsc.folder_ftp = bc.iniC.folderFTP;
                //    dsc.status_ipd = chkIPD.Checked ? "I" : "O";
                dsc.row_no = "1";
                dsc.row_cnt = "1";
                dsc.status_version = "2";
                dsc.req_id = dt.Rows[0]["mnc_req_no"].ToString();
                DateTime dtt = new DateTime();
                if (DateTime.TryParse(dt.Rows[0]["mnc_req_dat"].ToString(), out dtt))
                {
                    dsc.date_req = dtt.Year.ToString() + "-" + dtt.ToString("MM-dd");
                }
                else
                {
                    dsc.date_req = "";
                }
                if (dsc.an.Length > 0)
                {
                    dsc.status_ipd = "1";
                }
                else
                {
                    dsc.status_ipd = "0";
                }
                //dsc.ml_fm = "FM-LAB-999";
                if (pathname.Equals("ClinicalReport"))
                {
                    dsc.ml_fm = "FM-LAB-998";
                }
                else
                {
                    dsc.ml_fm = "FM-LAB-997";       //PathoReport
                }
                dsc.patient_fullname = dt.Rows[0]["mnc_patname"].ToString();
                dsc.status_record = "2";
                String re = bc.bcDB.dscDB.insertLabOut(dsc, bc.userId);
                if (re.Length <= 0)
                {
                    listBox2.Items.Add("ไม่ได้เลขที่ " + filename);
                    Application.DoEvents();
                    String datetick = "";
                    new LogWriter("e", "ไม่ได้เลขที่ " + filename);
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                listBox2.Items.Add("ได้เลขที่ " + re);
                Application.DoEvents();
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString() + "//" + dt.Rows[0]["mnc_hn_no"].ToString() + "_" + re + ext;
                //    if (chkIPD.Checked)
                //    {
                //        vn = txtAN.Text.Replace("/", "_").Replace("(", "_").Replace(")", "");
                //    }
                //    else
                //    {
                vn = dsc.vn.Replace("/", "_").Replace("(", "_").Replace(")", "");
                if (pathname.Equals("ClinicalReport"))
                {
                    dsc.ml_fm = "FM-LAB-998";
                }
                else
                {
                    dsc.ml_fm = "FM-LAB-997";       //PathoReport
                }
                //    }
                //    //dsc.image_path = txtHn.Text.Replace("/", "-") + "-" + vn + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "-" + re + ext;       //-1
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "-" + vn + "-" + re + ext;         //+1                

                String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                listBox2.Items.Add("updateImagepath " + dsc.image_path);
                Application.DoEvents();
                //    //MessageBox.Show("111", "");

                ftp.createDirectory(bc.iniC.folderFTP + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-"));       // สร้าง Folder HN
                //    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn);
                //    //MessageBox.Show("222", "");
                ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                //    //MessageBox.Show("333", "");
                Thread.Sleep(200);
                if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename))
                {
                    listBox2.Items.Add("FTP upload success ");
                    Application.DoEvents();
                    Thread.Sleep(1000);
                    String datetick = "";
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(1000);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + filename2 + "_" + datetick + ext);
                    }
                    listBox1.BeginUpdate();     //listBox2
                    listBox1.Items.Add(filename + " -> " + bc.iniC.hostFTP + "//" + bc.iniC.folderFTP + "//" + dsc.image_path);     //listBox2
                    listBox1.EndUpdate();     //listBox2
                    Application.DoEvents();
                    Thread.Sleep(1000 * 60);
                }
                else
                {
                    listBox2.Items.Add("FTP upload success ");
                    Application.DoEvents();
                    new LogWriter("e", "FTP upload no success");
                }
            }
            timer.Start();
        }
        private void uploadFiletoServerMedica()
        {
            if (txtVsDate.Text.Length <= 0)
            {
                MessageBox.Show("วันที่ ไม่มีค่า", "");
                return;
            }
            if (txtVnAn.Text.Length <= 0)
            {
                MessageBox.Show("VN An ไม่มีค่า", "");
                return;
            }
            timer.Stop();
            lbMessage.Text = "เตรียม Upload";
            listBox2.Items.Clear();     //listBox3
            Application.DoEvents();
            Thread.Sleep(1000);

            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
            //String[] filePaths = Directory.GetFiles(bc.iniC.pathLabOutReceive, "*.*", SearchOption.TopDirectoryOnly);
            List<String> filePaths = new List<String>();
            
            filePaths.Add(txtFilename.Text);
            //new LogWriter("d", "uploadFiletoServerMedica 01");
            String dgssid = "";
            dgssid = bc.bcDB.dgssDB.getIdDgss("Document Other");
            DocGroupSubScan dgss = new DocGroupSubScan();
            dgss = bc.bcDB.dgssDB.selectByPk(dgssid);
            foreach (String filename in filePaths)
            {
                listBox2.Items.Add("พบ file " + filename);
                Application.DoEvents();
                int year2 = 0;
                String yy = "", mm = "", dd = "", reqid = "", vn = "", filename1 = "", filename2 = "", year1 = "", ext = "";
                String pathname = "", tmp = "";
                //tmp = bc.iniC.pathLabOutReceiveInnoTech.Replace("\\\\", "\\");
                filename1 = Path.GetFileName(filename);
                pathname = Path.GetDirectoryName(filename);
                //pathname = filename.Replace(filename1, "").Replace(tmp, "").Replace("\\", "");
                //pathname = pathname.Replace("\\", "");
                String[] txt = filename1.Split('_');
                if (txt.Length > 1)
                {
                    filename2 = txt[0];
                }
                else
                {
                    filename2 = filename1.Replace(".pdf", "");
                }
                ext = Path.GetExtension(filename1);
                
                
                DateTime dtt1 = new DateTime();
                int.TryParse(year1, out year2);
                //yy = filename2.Substring(filename2.Length - 5, 2);
                mm = txtVsDate.Text.Substring(3, 2);
                dd = txtVsDate.Text.Substring(0, 2);
                year1 = txtVsDate.Text.Substring(txtVsDate.Text.Length-4, 4);
                if (!DateTime.TryParse(year1 + "-" + mm + "-" + dd, out dtt1))
                {
                    String datetick = "";
                    new LogWriter("e", "Filename ชื่อ File ไม่สามารถหา date ได้ " + filename);
                    listBox2.Items.Add("Filename ชื่อ File ไม่สามารถหา date ได้ " + filename);
                    //MessageBox.Show("Filename ไม่ถูก FORMAT", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                reqid = "";

                DataTable dt = new DataTable();
                dt = bc.bcDB.vsDB.SelectHnLabOut1(txtHn.Text.Trim(), year1 + "-" + mm + "-" + dd);
                if (dt.Rows.Count <= 0)
                {
                    listBox2.Items.Add("Filename ไม่พบข้อมูล HIS " + filename);
                    Application.DoEvents();
                    String datetick = "";
                    new LogWriter("e", "Filename ไม่พบข้อมูล HIS"+ txtHn.Text.Trim()  +" "+ year1 + "-" + mm + "-" + dd);
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupManual))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupManual);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupManual + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupManual + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupManual + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupManual + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                reqid = dt.Rows[0]["mnc_req_no"].ToString();
                listBox2.Items.Add("พบข้อมูล HIS " + dt.Rows[0]["mnc_hn_no"].ToString());
                Application.DoEvents();
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = dgss.doc_group_id;
                dsc.hn = dt.Rows[0]["mnc_hn_no"].ToString();
                dsc.vn = dt.Rows[0]["MNC_VN_NO"].ToString() + "/" + dt.Rows[0]["MNC_VN_SEQ"].ToString() + "(" + dt.Rows[0]["MNC_VN_SUM"].ToString() + ")";
                dsc.an = dt.Rows[0]["MNC_AN_NO"].ToString() + "/" + dt.Rows[0]["MNC_AN_YR"].ToString();
                if (dsc.an.Equals("/"))
                {
                    dsc.an = "";
                }
                dsc.visit_date = "";
                dsc.host_ftp = bc.iniC.hostFTP;
                //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                dsc.image_path = "";
                dsc.doc_group_sub_id = dgssid;
                dsc.pre_no = dt.Rows[0]["mnc_pre_no"].ToString();
                //dsc.an = "";
                //DateTime dt = new DateTime();

                //    dsc.an_date = (DateTime.TryParse(txtAnDate.Text, out dt)) ? bc.datetoDB(txtAnDate.Text) : "";
                //    if (dsc.an_date.Equals("1-01-01"))
                //    {
                //        dsc.an_date = "";
                //    }
                dsc.folder_ftp = bc.iniC.folderFTP;
                //    dsc.status_ipd = chkIPD.Checked ? "I" : "O";
                dsc.row_no = "1";
                dsc.row_cnt = "1";
                dsc.status_version = "2";
                dsc.req_id = dt.Rows[0]["mnc_req_no"].ToString();
                DateTime dtt = new DateTime();
                if (DateTime.TryParse(dt.Rows[0]["mnc_req_dat"].ToString(), out dtt))
                {
                    dsc.date_req = dtt.Year.ToString() + "-" + dtt.ToString("MM-dd");
                }
                else
                {
                    dsc.date_req = "";
                }
                if (dsc.an.Length > 0)
                {
                    dsc.status_ipd = "1";
                }
                else
                {
                    dsc.status_ipd = "0";
                }
                //dsc.ml_fm = "FM-LAB-999";
                
                dsc.ml_fm = "FM-LAB-995";       //PathoReport
                
                dsc.patient_fullname = dt.Rows[0]["mnc_patname"].ToString();
                dsc.status_record = "2";
                String re = bc.bcDB.dscDB.insertLabOut(dsc, bc.userId);
                if (re.Length <= 0)
                {
                    listBox2.Items.Add("ไม่ได้เลขที่ " + filename);
                    Application.DoEvents();
                    String datetick = "";
                    new LogWriter("e", "ไม่ได้เลขที่ " + filename);
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname);
                        }
                    }
                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\err_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                lbMessage.Text = "ได้เลขที่ " + re;
                listBox2.Items.Add("ได้เลขที่ " + re);
                Application.DoEvents();
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString() + "//" + dt.Rows[0]["mnc_hn_no"].ToString() + "_" + re + ext;
                //    if (chkIPD.Checked)
                //    {
                //        vn = txtAN.Text.Replace("/", "_").Replace("(", "_").Replace(")", "");
                //    }
                //    else
                //    {
                vn = dsc.vn.Replace("/", "_").Replace("(", "_").Replace(")", "");
                
                dsc.ml_fm = "FM-LAB-995";       //Medica
                
                //    }
                //    //dsc.image_path = txtHn.Text.Replace("/", "-") + "-" + vn + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "-" + re + ext;       //-1
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "-" + vn + "-" + re + ext;         //+1                

                String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                listBox2.Items.Add("updateImagepath " + dsc.image_path);
                Application.DoEvents();
                //    //MessageBox.Show("111", "");

                ftp.createDirectory(bc.iniC.folderFTP + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-"));       // สร้าง Folder HN
                //    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn);
                //    //MessageBox.Show("222", "");
                ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                //    //MessageBox.Show("333", "");
                Thread.Sleep(200);
                if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename))
                {
                    listBox2.Items.Add("FTP upload success ");
                    Application.DoEvents();
                    Thread.Sleep(1000);
                    String datetick = "";
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupManual))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupManual);
                    }
                    Thread.Sleep(1000);
                    try
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupManual + "\\" + filename2 + "_" + datetick + ext);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถ move file ได้\n"+"ex "+ex.Message, "");
                    }
                    
                    
                    listBox1.BeginUpdate();     //listBox2
                    listBox1.Items.Add(filename + " -> " + bc.iniC.hostFTP + "//" + bc.iniC.folderFTP + "//" + dsc.image_path);     //listBox2
                    listBox1.EndUpdate();     //listBox2
                    Application.DoEvents();
                    lbMessage.Text = "Upload เรียบร้อย";
                    Thread.Sleep(2000);
                }
                else
                {
                    listBox2.Items.Add("FTP upload success ");
                    Application.DoEvents();
                    new LogWriter("e", "FTP upload no success");
                }
            }
            timer.Start();
        }
        private void setGrfVsOPD()
        {
            //grfVisit.Clear();
            grfVisit.Cols.Count = 10;
            //grfVisit.Rows.Count = 0;
            grfVisit.Rows.Count = 1;
            grfVisit.Cols[colVsVsDate].Width = 100;
            grfVisit.Cols[colVsVn].Width = 80;
            grfVisit.Cols[colVsDept].Width = 240;
            grfVisit.Cols[colVsPreno].Width = 100;
            grfVisit.Cols[colVsStatus].Width = 60;
            grfVisit.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfVisit.Cols[colVsVsDate].Caption = "Visit Date";
            grfVisit.Cols[colVsVn].Caption = "VN";
            grfVisit.Cols[colVsDept].Caption = "แผนก";
            grfVisit.Cols[colVsPreno].Caption = "";
            grfVisit.Cols[colVsAn].Caption = "AN";
            grfVisit.Cols[colVsPreno].Visible = false;
            grfVisit.Cols[colVsVn].Visible = true;
            grfVisit.Cols[colVsAn].Visible = true;
            grfVisit.Cols[colVsAndate].Visible = false;
            //grfVisit.Rows[0].Visible = false;
            grfVisit.Cols[0].Visible = false;
            grfVisit.Cols[colVsVsDate].AllowEditing = false;
            grfVisit.Cols[colVsVn].AllowEditing = false;
            grfVisit.Cols[colVsDept].AllowEditing = false;
            grfVisit.Cols[colVsPreno].AllowEditing = false;

            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dt = bc.bcDB.vsDB.selectVisitByHn4(txtHn.Text, "O");
            int i = 1, j = 1;
            
            grfVisit.Rows.Count = dt.Rows.Count+1;

            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfVisit.Rows[i];
                String status = "", vn = "";

                status = row1["MNC_PAT_FLAG"] != null ? row1["MNC_PAT_FLAG"].ToString().Equals("O") ? "OPD" : "IPD" : "-";
                vn = row1["MNC_VN_NO"].ToString() + "/" + row1["MNC_VN_SEQ"].ToString() + "(" + row1["MNC_VN_SUM"].ToString() + ")";
                rowa[colVsVsDate] = bc.datetoShow(row1["mnc_date"]);
                rowa[colVsVn] = vn;
                rowa[colVsStatus] = status;
                rowa[colVsPreno] = row1["mnc_pre_no"].ToString();
                rowa[colVsDept] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colVsAn] = row1["mnc_an_no"].ToString() + "/" + row1["mnc_an_yr"].ToString();
                rowa[colVsAndate] = bc.datetoShow(row1["mnc_ad_date"].ToString());
                i++;
            }
            
        }
        
        private void setGrfVsH()
        {
            grfVisit.Clear();
            grfVisit.Rows.Count = 1;
            grfVisit.Cols.Count = 10;

            grfVisit.Cols[colVsVsDate].Width = 100;
            grfVisit.Cols[colVsVn].Width = 80;
            grfVisit.Cols[colVsDept].Width = 240;
            grfVisit.Cols[colVsPreno].Width = 100;
            grfVisit.Cols[colVsStatus].Width = 60;
            grfVisit.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfVisit.Cols[colVsVsDate].Caption = "Visit Date";
            grfVisit.Cols[colVsVn].Caption = "VN";
            grfVisit.Cols[colVsDept].Caption = "แผนก";
        }
        private void FrmLabOutReceive1_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.Text = "Last Update 2020-02-21 bc.timerCheckLabOut " + bc.timerCheckLabOut;
        }
    }
}
