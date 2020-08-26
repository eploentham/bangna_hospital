using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Command;
using C1.Win.C1Document;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.FlexViewer;
using Ionic.Zip;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Path = System.IO.Path;

namespace bangna_hospital.gui
{
    /*
     * 63-04-02     0004
     * 63-03-28     0003
     */
    public class FrmLabOutReceive1:Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;
        //FileSystemWatcher watcher;
        //ListBox listBox1;
        System.Windows.Forms.Timer timer;
        Boolean chkFileInnoTech = false, chkFileRIA = false;
        C1DockingTab tC1;
        C1DockingTabPage tabLabAuto, tabManual, tabMed;
        Panel pnAuto, pnManual, panel1, pnLeft, pnRightTop, pnRightBotton, pnMed, pnMedMachine, pnLabComp;
        Label lbStep1, lbStep2, lbPttNmae, lbVisitStatus, lbOutLabDate, lbMessage, lbMedStep1, lbMedStep2, lbMedPttNmae, lbMedVisitStatus, lbMedOutLabDate, lbMedMessage, lbMedicaManualDate;
        C1Button btnBrow, btnUpload, btnMedBrow, btnMedUpload, btnMedicaManual, btnLabTest;
        C1TextBox txtFilename, txtHn, txtVnAn, txtVsDate, txtMedFilename, txtMedHn, txtMedVnAn, txtMedVsDate, txtMedicaManualDate;
        C1FlexGrid grfVisit, grfMedVisit;
        RadioButton chkOPD, chkIPD, chkMedOPD, chkMedIPD, chkMedHolter, chkMedCarilo, chkMedEcho, chkMedEndoscope, chkLabComp1, chkLabComp2;
        C1FlexViewer labOutView, medView;

        int colVsVsDate = 1, colVsVn = 2, colVsStatus = 3, colVsDept = 4, colVsPreno = 5, colVsAn = 6, colVsAndate = 7;
        
        Patient ptt;
        String preno = "", compName="";

        private C1.Win.C1Ribbon.C1StatusBar sb1;
        private C1.Win.C1Themes.C1ThemeController theme1;
        private System.Windows.Forms.ListBox listBox1, listBox2, listBox3;
        private System.Windows.Forms.SplitContainer splitContainer1;

        Stream streamPrint;
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
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
            if (bc.iniC.statusLabOutReceiveOnline.Equals("1"))
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
                //tabLabAuto.Hide();
            }

            this.Load += FrmLabOutReceive1_Load;
            btnUpload.Click += BtnUpload_Click;
            btnBrow.Click += BtnBrow_Click;
            txtHn.KeyUp += TxtHn_KeyUp;
            grfVisit.RowColChange += GrfVisit_RowColChange;
            chkOPD.Click += ChkOPD_Click;
            chkIPD.Click += ChkIPD_Click;

            btnMedBrow.Click += BtnMedBrow_Click;
            btnMedUpload.Click += BtnMedUpload_Click;
            txtMedHn.KeyUp += TxtMedHn_KeyUp;
            grfMedVisit.RowColChange += GrfMedVisit_RowColChange;
            chkMedOPD.Click += ChkMedOPD_Click;
            chkMedIPD.Click += ChkMedIPD_Click;
            btnMedicaManual.Click += BtnMedicaManual_Click;
            btnLabTest.Click += BtnLabTest_Click;
        }
        private void BtnLabTest_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String hn = "";
            hn = txtHn.Text.Trim();
            //streamPrint = ftp.download(bc.iniC.folderFTP + "//" + dsc.image_path);
            //printLabOut();
            //chkAttendUrgent(dsc, streamPrint);
        }

        private void BtnMedicaManual_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkLabComp1.Checked)
            {
                tC1.SelectedTab = tabLabAuto;
                getFileinFolderMedica(txtMedicaManualDate.Text.Trim());
                uploadFiletoServerMedicaOnLine(txtMedicaManualDate.Text.Trim());
            }
        }

        private void ChkMedIPD_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfMed();
        }

        private void ChkMedOPD_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfMed();
        }

        private void GrfMedVisit_RowColChange(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfMedVisit.Row <= 0) return;
            if (grfMedVisit.Col <= 0) return;
            if (grfMedVisit[grfMedVisit.Row, colVsVn] == null) return;
                        
            if (grfMedVisit[grfMedVisit.Row, colVsStatus].ToString().Trim().Equals("OPD"))
            {
                chkMedOPD.Checked = true;
            }
            else
            {
                chkMedIPD.Checked = true;
            }
            if (chkMedOPD.Checked)
            {
                txtMedVnAn.Value = grfMedVisit[grfMedVisit.Row, colVsVn].ToString().Trim();
                //chkMedOPD.Checked = true;
            }
            else if (chkMedIPD.Checked)
            {
                txtMedVnAn.Value = grfMedVisit[grfMedVisit.Row, colVsAn].ToString().Trim();
                //chkMedIPD.Checked = true;
            }
            else
            {

            }
            preno = grfMedVisit[grfMedVisit.Row, colVsPreno].ToString().Trim();
            txtMedVsDate.Value = grfMedVisit[grfMedVisit.Row, colVsVsDate].ToString().Trim();
        }

        private void TxtMedHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setControlHNMed();
                setGrfMed();
            }
        }

        private void BtnMedUpload_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (MessageBox.Show("ต้องการ Upload MED ช้อมูล \n" + txtMedHn.Text + " " + lbMedPttNmae.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                uploadFiletoServerMedCarilo();
            }
        }

        private void BtnMedBrow_Click(object sender, EventArgs e)
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
                lbMedMessage.Text = "";
                txtMedHn.Value = "";
                txtMedVnAn.Value = "";
                txtMedVsDate.Value = "";
                preno = "";
                grfVisit.Rows.Count = 1;
                lbMedOutLabDate.Text = "";
                lbMedOutLabDate.Text = "";
                txtMedFilename.Value = ofd.FileName;
                int pagesToScan = 2;
                string strText = "", outPath = "";
                PdfReader reader = new PdfReader(ofd.FileName);
                try
                {
                    //pds.LoadFromFile(filename1);
                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    pds.LoadFromFile(ofd.FileName);
                    if (bc.iniC.windows.Equals("windowsxp"))
                    {
                        string currentDirectory = Directory.GetCurrentDirectory();
                        String datetick = DateTime.Now.Ticks.ToString();
                        WebBrowser webBrowser1;
                        webBrowser1 = new System.Windows.Forms.WebBrowser();
                        //webBrowser1.Enabled = true;
                        //webBrowser1.Location = new System.Drawing.Point(192, 0);
                        webBrowser1.Name = "webBrowser1";
                        //axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(Resources.GetObject("axAcroPDF1.OcxState")));
                        //webBrowser1.Size = new System.Drawing.Size(192, 192);
                        webBrowser1.Dock = DockStyle.Fill;
                        webBrowser1.Dock = System.Windows.Forms.DockStyle.None;
                        webBrowser1.Size = medView.Size;
                        webBrowser1.Location = medView.Location;
                        //axAcroPDF1.TabIndex = 7;
                        String file1 = "";
                        file1 = ofd.FileName;
                        //new LogWriter("d", file1);
                        if (!File.Exists(file1))
                        {
                            MessageBox.Show("ไม่พบ File " + file1, "");
                        }
                        medView.Hide();
                        pnMed.Controls.Add(webBrowser1);
                        webBrowser1.Navigate(file1);
                        //tabHnLabOut.Controls.Add(webBrowser1);
                    }
                    else
                    {
                        medView.DocumentSource = pds;
                    }
                        
                    Application.DoEvents();
                    String pathname = "", filename="", pttname1="";
                    pathname = Path.GetDirectoryName(txtMedFilename.Text.Trim());
                    filename = Path.GetFileNameWithoutExtension(txtMedFilename.Text.Trim());
                    String lastFolderName = Path.GetFileName(pathname.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                    String[] pttname2 = lastFolderName.Split('_');
                    if (pttname2.Length >= 4)
                    {
                        pttname1 = pttname2[1] + " " + pttname2[0];
                        txtMedHn.Value = pttname2[3].Replace("_", "");
                        lbMedPttNmae.Text = pttname1;
                        chkMedOPD.Checked = true;
                        txtMedVsDate.Value = filename;
                        //setGrf();
                    }
                    pagesToScan = pds.PageCount;
                    for (int page = 1; page <= pagesToScan; page++) //(int page = 1; page <= reader.NumberOfPages; page++) <- for scanning all the pages in A PDF
                    {
                        try
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

                                int indexpttname = line.LastIndexOf("Q-Stress Final Report");
                                if (indexpttname >= 0)
                                {
                                    chkMedCarilo.Checked = true;
                                    chkMedEcho.Checked = false;
                                    chkMedEndoscope.Checked = false;
                                    chkMedHolter.Checked = false;
                                }
                                int indexendoscop = line.LastIndexOf("Endoscope");
                                if (indexendoscop >= 0)
                                {
                                    chkMedCarilo.Checked = false;
                                    chkMedEcho.Checked = false;
                                    chkMedEndoscope.Checked = true;
                                    chkMedHolter.Checked = false;
                                    int indexhn = line.LastIndexOf("HN");
                                    if (indexhn < 0)
                                    {
                                        indexhn = line.LastIndexOf("H N");
                                    }
                                    if (indexhn >= 0)
                                    {
                                        txtMedHn.Value = line.Replace("H", "").Replace("N", "").Replace(":", "").Trim();
                                        setControlHNMed();
                                        setGrfMed();
                                        //Patient ptt = new Patient();
                                        //ptt = bc.bcDB.pttDB.selectPatinet(txtMedHn.Text.Trim());
                                        //lbPttNmae.Text = ptt.Name;
                                    }
                                }
                                int indexeholter = line.LastIndexOf("HOLTER");
                                if (indexeholter >= 0)
                                {
                                    chkMedCarilo.Checked = false;
                                    chkMedEcho.Checked = false;
                                    chkMedEndoscope.Checked = false;
                                    chkMedHolter.Checked = true;
                                    int indexhn = line.LastIndexOf("ID");
                                    if (indexhn >= 0)
                                    {
                                        txtMedHn.Value = line.Replace("ID", "").Replace(":", "").Trim();
                                        setControlHNMed();
                                        setGrfMed();
                                        //Patient ptt = new Patient();
                                        //ptt = bc.bcDB.pttDB.selectPatinet(txtMedHn.Text.Trim());
                                        //lbPttNmae.Text = ptt.Name;
                                    }
                                    String hn = "";
                                    if (lines.Length > 9)
                                    {
                                        int chk = 0;
                                        String[] txtchk = lines[6].Split(' ');
                                        if (txtchk.Length > 1)
                                        {
                                            if (int.TryParse(txtchk[0], out chk))
                                            {
                                                hn = txtchk[0];
                                                txtMedHn.Value = hn;
                                                setControlHNMed();
                                                setGrfMed();
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (int.TryParse(lines[6], out chk))
                                            {
                                                hn = lines[6];
                                                txtMedHn.Value = hn;
                                                setControlHNMed();
                                                setGrfMed();
                                                break;
                                            }
                                            else
                                            {
                                                if (int.TryParse(lines[7], out chk))
                                                {
                                                    hn = lines[7];
                                                    txtMedHn.Value = hn;
                                                    setControlHNMed();
                                                    setGrfMed();
                                                    break;
                                                }
                                            }
                                        }
                                        
                                    }
                                }
                                
                                //}
                            }
                            
                        }
                        catch(Exception ex)
                        {

                        }
                        
                    }
                    pds.Dispose();
                    reader.Close();
                    reader.Dispose();
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "BtnMedBrow_Click " + ex.Message);
                    lbMedMessage.Text = "error " + ex.Message;
                    Console.Write(ex);
                }
                finally
                {
                    //reader.Close();
                    //reader.Dispose();
                }
            }
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
        private void setGrfMed()
        {
            setGrfVsHMed();
            setGrfVsOPDMed();
        }
        private void setControlHN()
        {
            ptt = bc.bcDB.pttDB.selectPatinet(txtHn.Text.Trim());
            lbPttNmae.Text = ptt.Name +" Age "+ptt.AgeStringShort();
        }
        private void setControlHNMed()
        {
            ptt = bc.bcDB.pttDB.selectPatinet(txtMedHn.Text.Trim());
            lbMedPttNmae.Text = ptt.Name + " Age " + ptt.AgeStringShort();
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
            tabLabAuto = new C1.Win.C1Command.C1DockingTabPage();
            tabManual = new C1.Win.C1Command.C1DockingTabPage();
            tabMed = new C1.Win.C1Command.C1DockingTabPage();
            pnAuto = new Panel();
            pnManual = new Panel();
            pnMed = new Panel();
            pnMedMachine = new Panel();
            pnLabComp = new Panel();
            labOutView = new C1FlexViewer();
            medView = new C1FlexViewer();

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
            pnMed.SuspendLayout();
            pnMedMachine.SuspendLayout();
            pnLabComp.SuspendLayout();
            tC1.SuspendLayout();
            tabLabAuto.SuspendLayout();
            tabManual.SuspendLayout();
            tabMed.SuspendLayout();
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
            tC1.Name = "tC1";

            pnAuto.Dock = DockStyle.Fill;
            pnManual.Dock = DockStyle.Fill;
            pnMed.Dock = DockStyle.Fill;
            pnMedMachine.Dock = DockStyle.None;
            pnLabComp.Dock = DockStyle.None;

            tabLabAuto.Name = "tabAuto";
            tabLabAuto.TabIndex = 0;
            tabLabAuto.Text = "Lab Auto Result";

            tabManual.Name = "tabManual";
            tabManual.TabIndex = 0;
            tabManual.Text = "Lab Manual Result";

            tabMed.Name = "tabManual";
            tabMed.TabIndex = 0;
            tabMed.Text = "Medical Result";

            setTabLabManual();
            setTabMed();

            this.theme1.Theme = "BeigeOne";

            this.Controls.Add(panel1);
            this.Controls.Add(this.sb1);
            panel1.Controls.Add(tC1);
            tC1.Controls.Add(tabLabAuto);
            tC1.Controls.Add(tabManual);
            tC1.Controls.Add(tabMed);
            tabLabAuto.Controls.Add(pnAuto);
            tabManual.Controls.Add(pnManual);
            tabMed.Controls.Add(pnMed);
            pnMed.Controls.Add(pnMedMachine);
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
            pnManual.Controls.Add(labOutView);
            pnManual.Controls.Add(lbMedicaManualDate);
            pnManual.Controls.Add(txtMedicaManualDate);
            pnManual.Controls.Add(btnMedicaManual);
            pnManual.Controls.Add(btnLabTest);
            pnManual.Controls.Add(pnLabComp);
            pnLabComp.Controls.Add(chkLabComp1);
            pnLabComp.Controls.Add(chkLabComp2);

            pnMed.Controls.Add(lbMedStep1);
            pnMed.Controls.Add(btnMedBrow);
            pnMed.Controls.Add(txtMedFilename);
            pnMed.Controls.Add(lbMedStep2);
            pnMed.Controls.Add(txtMedHn);
            pnMed.Controls.Add(lbMedPttNmae);
            pnMed.Controls.Add(lbMedOutLabDate);
            pnMed.Controls.Add(lbMedVisitStatus);
            pnMed.Controls.Add(chkMedOPD);
            pnMed.Controls.Add(chkMedIPD);
            pnMed.Controls.Add(txtMedVnAn);
            pnMed.Controls.Add(txtMedVsDate);
            pnMed.Controls.Add(btnMedUpload);
            pnMed.Controls.Add(lbMedMessage);
            pnMed.Controls.Add(grfMedVisit);
            pnMed.Controls.Add(medView);

            pnMedMachine.Controls.Add(chkMedHolter);
            pnMedMachine.Controls.Add(chkMedCarilo);
            pnMedMachine.Controls.Add(chkMedEcho);
            pnMedMachine.Controls.Add(chkMedEndoscope);

            this.WindowState = FormWindowState.Maximized;

            panel1.ResumeLayout(false);
            pnAuto.ResumeLayout(false);
            pnManual.ResumeLayout(false);
            pnMed.ResumeLayout(false);
            pnMedMachine.ResumeLayout(false);
            pnLabComp.ResumeLayout(false);
            tC1.ResumeLayout(false);
            tabLabAuto.ResumeLayout(false);
            tabManual.ResumeLayout(false);
            tabMed.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            panel1.PerformLayout();
            pnAuto.PerformLayout();
            pnManual.PerformLayout();
            pnMed.PerformLayout();
            pnMedMachine.PerformLayout();
            pnLabComp.PerformLayout();
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.PerformLayout();
            splitContainer1.PerformLayout();
            tabMed.PerformLayout();
            tabLabAuto.PerformLayout();
            tC1.PerformLayout();
            this.PerformLayout();
        }
        private void setTabLabManual()
        {
            int gapLine = 20, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;
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
            txtFilename.Size = new Size(400, btnBrow.Height + 5);

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
            btnUpload.Location = new System.Drawing.Point(txtVsDate.Location.X + txtVsDate.Width + 10, chkOPD.Location.Y);
            btnUpload.Size = new Size(size.Width + 15, lbStep1.Height);
            btnUpload.Font = fEdit;
            lbMessage = new Label();
            lbMessage.Text = "";
            lbMessage.Font = fEdit;
            lbMessage.Location = new System.Drawing.Point(btnUpload.Location.X + btnUpload.Width + 10, chkOPD.Location.Y);
            lbMessage.AutoSize = true;
            lbMedicaManualDate = new Label();
            lbMedicaManualDate.Text = "Date  Manual ";
            lbMedicaManualDate.Font = fEdit;
            lbMedicaManualDate.Location = new System.Drawing.Point(lbMessage.Location.X + 100, chkOPD.Location.Y);
            lbMedicaManualDate.AutoSize = true;
            lbMedicaManualDate.Name = "lbMedicaManualDate";
            size = bc.MeasureString(lbMedicaManualDate);
            txtMedicaManualDate = new C1TextBox();
            txtMedicaManualDate.Font = fEdit;
            txtMedicaManualDate.Location = new System.Drawing.Point(lbMedicaManualDate.Location.X + size.Width + 15, chkOPD.Location.Y);
            txtMedicaManualDate.Size = new Size(80, btnBrow.Height + 5);
            txtMedicaManualDate.Name = "";
            btnMedicaManual = new C1Button();
            btnMedicaManual.Name = "btnMedicaManual";
            btnMedicaManual.Text = "Upload Manual";
            size = bc.MeasureString(btnMedicaManual);
            btnMedicaManual.Font = fEdit;
            btnMedicaManual.Location = new System.Drawing.Point(txtMedicaManualDate.Location.X+ txtMedicaManualDate.Width + 15, chkOPD.Location.Y);
            btnMedicaManual.Size = new Size(size.Width + 15, lbStep1.Height);
            btnMedicaManual.Font = fEdit;
            btnLabTest = new C1Button();
            btnLabTest.Name = "btnLabTest";
            btnLabTest.Text = "Test Manual";
            size = bc.MeasureString(btnLabTest);
            btnLabTest.Font = fEdit;
            btnLabTest.Location = new System.Drawing.Point(btnMedicaManual.Location.X + btnMedicaManual.Width + 10, chkOPD.Location.Y);
            btnLabTest.Size = new Size(size.Width + 15, lbStep1.Height);
            btnLabTest.Font = fEdit;

            grfVisit = new C1FlexGrid();
            grfVisit.Font = fEdit;
            grfVisit.Location = new System.Drawing.Point(gapX, gapLine + chkOPD.Location.Y + 10);
            grfVisit.Size = new Size(900, 300);

            labOutView.AutoScrollMargin = new System.Drawing.Size(0, 0);
            labOutView.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            labOutView.Dock = System.Windows.Forms.DockStyle.None;
            labOutView.Size = new System.Drawing.Size(900, Screen.PrimaryScreen.Bounds.Height - (gapLine + chkOPD.Location.Y + 10) - grfVisit.Height - 160);
            labOutView.Location = new System.Drawing.Point(gapX, gapLine + grfVisit.Location.Y + grfVisit.Height);
            labOutView.Name = "labOutView";
            //labOutView.
            chkLabComp1 = new RadioButton();
            chkLabComp1.Text = "Medica";
            chkLabComp1.Font = fEdit;
            chkLabComp1.Location = new System.Drawing.Point(5, 5);
            chkLabComp1.AutoSize = true;
            size = bc.MeasureString(chkLabComp1);
            chkLabComp2 = new RadioButton();
            chkLabComp2.Text = "GM";
            chkLabComp2.Font = fEdit;
            chkLabComp2.Location = new System.Drawing.Point(chkLabComp1.Location.X + size.Width + 20, chkLabComp1.Location.Y);
            chkLabComp2.AutoSize = true;

            pnLabComp.Location = new System.Drawing.Point(lbOutLabDate.Location.X + lbOutLabDate.Width + 50, lbOutLabDate.Location.Y);
            pnLabComp.Size = new System.Drawing.Size(250, 30);
            pnLabComp.BorderStyle = BorderStyle.FixedSingle;
            
            labOutView.TabIndex = 0;
        }
        private void setTabMed()
        {
            int gapLine = 20, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;

            lbMedStep1 = new Label();
            lbMedStep1.Text = "Step 1 Browe File PDF ";
            lbMedStep1.Font = fEdit;
            lbMedStep1.Location = new System.Drawing.Point(gapX, 10);
            lbMedStep1.AutoSize = true;
            size = bc.MeasureString(lbMedStep1);
            btnMedBrow = new C1Button();
            btnMedBrow.Name = "btnMedBrow";
            btnMedBrow.Text = "...";
            btnMedBrow.Font = fEdit;
            btnMedBrow.Location = new System.Drawing.Point(gapX + size.Width, lbMedStep1.Location.Y);
            btnMedBrow.Size = new Size(30, lbMedStep1.Height);
            btnMedBrow.Font = fEdit;

            txtMedFilename = new C1TextBox();
            txtMedFilename.Font = fEdit;
            txtMedFilename.Location = new System.Drawing.Point(btnMedBrow.Location.X + btnMedBrow.Width + 5, btnMedBrow.Location.Y);
            txtMedFilename.Size = new Size(600, btnMedBrow.Height + 5);

            lbMedStep2 = new Label();
            lbMedStep2.Text = "Step 2 HN :";
            lbMedStep2.Font = fEdit;
            lbMedStep2.Location = new System.Drawing.Point(gapX, gapLine + txtMedFilename.Height);
            lbMedStep2.AutoSize = true;
            size = bc.MeasureString(lbMedStep2);
            txtMedHn = new C1TextBox();
            txtMedHn.Font = fEdit;
            txtMedHn.Location = new System.Drawing.Point(gapX + size.Width + 5, lbMedStep2.Location.Y);
            txtMedHn.Size = new Size(80, btnMedBrow.Height + 5);

            lbMedPttNmae = new Label();
            lbMedPttNmae.Text = "xxxx";
            lbMedPttNmae.Font = fEdit;
            lbMedPttNmae.Location = new System.Drawing.Point(txtHn.Location.X + txtHn.Width + 5, lbStep2.Location.Y);
            lbMedPttNmae.AutoSize = true;
            lbMedOutLabDate = new Label();
            lbMedOutLabDate.Text = "dd-mm-yyyy";
            lbMedOutLabDate.Font = fEdit;
            lbMedOutLabDate.Location = new System.Drawing.Point(txtMedHn.Location.X + txtMedHn.Width + 250, lbMedStep2.Location.Y);
            lbMedOutLabDate.AutoSize = true;

            lbMedVisitStatus = new Label();
            lbMedVisitStatus.Text = "สถานะ visit";
            lbMedVisitStatus.Font = fEdit;
            lbMedVisitStatus.Location = new System.Drawing.Point(gapX, gapLine + txtMedHn.Location.Y + txtMedHn.Height);
            lbMedVisitStatus.AutoSize = true;
            size = bc.MeasureString(lbMedVisitStatus);
            chkMedOPD = new RadioButton();
            chkMedOPD.Text = "OPD";
            chkMedOPD.Font = fEdit;
            chkMedOPD.Location = new System.Drawing.Point(lbMedVisitStatus.Location.X + size.Width + 5, lbMedVisitStatus.Location.Y);
            chkMedOPD.AutoSize = true;
            size = bc.MeasureString(chkMedOPD);
            chkMedIPD = new RadioButton();
            chkMedIPD.Text = "IPD";
            chkMedIPD.Font = fEdit;
            chkMedIPD.Location = new System.Drawing.Point(chkMedOPD.Location.X + size.Width + 25, chkMedOPD.Location.Y);
            chkMedIPD.AutoSize = true;
            txtMedVnAn = new C1TextBox();
            txtMedVnAn.Font = fEdit;
            txtMedVnAn.Location = new System.Drawing.Point(chkMedIPD.Location.X + size.Width + 25, chkMedOPD.Location.Y);
            txtMedVnAn.Size = new Size(80, btnMedBrow.Height + 5);
            txtMedVsDate = new C1TextBox();
            txtMedVsDate.Font = fEdit;
            txtMedVsDate.Location = new System.Drawing.Point(txtMedVnAn.Location.X + txtMedVnAn.Width + 25, txtMedVnAn.Location.Y);
            txtMedVsDate.Size = new Size(80, btnMedBrow.Height + 5);
            btnMedUpload = new C1Button();
            btnMedUpload.Name = "btnMedUpload";
            btnMedUpload.Text = "Upload";
            size = bc.MeasureString(btnMedUpload);
            btnMedUpload.Font = fEdit;
            btnMedUpload.Location = new System.Drawing.Point(txtMedVsDate.Location.X + txtMedVsDate.Width + 10, chkMedOPD.Location.Y);
            btnMedUpload.Size = new Size(size.Width + 15, lbMedStep1.Height);
            btnMedUpload.Font = fEdit;
            lbMedMessage = new Label();
            lbMedMessage.Text = "";
            lbMedMessage.Font = fEdit;
            lbMedMessage.Location = new System.Drawing.Point(btnMedUpload.Location.X + btnMedUpload.Width + 10, chkMedOPD.Location.Y);
            lbMedMessage.AutoSize = true;

            grfMedVisit = new C1FlexGrid();
            grfMedVisit.Font = fEdit;
            grfMedVisit.Location = new System.Drawing.Point(gapX, gapLine + chkMedOPD.Location.Y + 10);
            grfMedVisit.Size = new Size(900, 300);

            chkMedHolter = new RadioButton();
            chkMedHolter.Text = "Holter";
            chkMedHolter.Font = fEdit;
            chkMedHolter.Location = new System.Drawing.Point(5, 5);
            chkMedHolter.AutoSize = true;
            size = bc.MeasureString(chkMedHolter);
            chkMedCarilo = new RadioButton();
            chkMedCarilo.Text = "Carilo";
            chkMedCarilo.Font = fEdit;
            chkMedCarilo.Location = new System.Drawing.Point(chkMedHolter.Location.X + size.Width + 20, chkMedHolter.Location.Y);
            chkMedCarilo.AutoSize = true;
            size = bc.MeasureString(chkMedCarilo);
            chkMedEcho = new RadioButton();
            chkMedEcho.Text = "Echo";
            chkMedEcho.Font = fEdit;
            chkMedEcho.Location = new System.Drawing.Point(chkMedCarilo.Location.X + size.Width + 20, chkMedHolter.Location.Y);
            chkMedEcho.AutoSize = true;
            size = bc.MeasureString(chkMedEcho);
            chkMedEndoscope = new RadioButton();
            chkMedEndoscope.Text = "Endoscope";
            chkMedEndoscope.Font = fEdit;
            chkMedEndoscope.Location = new System.Drawing.Point(chkMedEcho.Location.X + size.Width + 20, chkMedHolter.Location.Y);
            chkMedEndoscope.AutoSize = true;

            pnMedMachine.Location = new System.Drawing.Point(lbMedOutLabDate.Location.X + lbMedOutLabDate.Width + 50, lbMedOutLabDate.Location.Y);
            pnMedMachine.Size = new System.Drawing.Size(250, 30);
            pnMedMachine.BorderStyle = BorderStyle.FixedSingle;

            medView.AutoScrollMargin = new System.Drawing.Size(0, 0);
            medView.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            medView.Dock = System.Windows.Forms.DockStyle.None;
            medView.Size = new System.Drawing.Size(900, Screen.PrimaryScreen.Bounds.Height - (gapLine + chkMedOPD.Location.Y + 10 ) - grfMedVisit.Height-160);
            medView.Location = new System.Drawing.Point(gapX, gapLine + grfMedVisit.Location.Y + grfMedVisit.Height);
            medView.Name = "medView";
            medView.TabIndex = 0;

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
            DataTable dt = new DataTable();
            String yy = "", mm = "", dd = "", reqid = "", vn = "", filename1 = "", filename2 = "", year1 = "", ext = "";
            int year2 = 0;
            int.TryParse(year1, out year2);
            //yy = filename2.Substring(filename2.Length - 5, 2);
            mm = txtVsDate.Text.Substring(3, 2);
            dd = txtVsDate.Text.Substring(0, 2);
            year1 = txtVsDate.Text.Substring(txtVsDate.Text.Length - 4, 4);
            dt = bc.bcDB.vsDB.SelectHnLabOut1(txtHn.Text.Trim(), year1 + "-" + mm + "-" + dd);
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("ไม่พบ Request ใน HIS " + txtVsDate.Text.Trim(), "");
            }
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
        private String setOutLab(String outlabfilename)
        {
            DataTable dt = new DataTable();
            String reqid = "";
            lbMessage.Text = "";
            txtHn.Value = "";
            txtVnAn.Value = "";
            txtVsDate.Value = "";
            preno = "";
            grfVisit.Rows.Count = 1;
            lbOutLabDate.Text = "";
            lbOutLabDate.Text = "";
            txtFilename.Value = outlabfilename;
            int pagesToScan = 2;
            string strText = "", outPath = "";
            PdfReader reader = new PdfReader(outlabfilename);
            try
            {
                C1PdfDocumentSource pds = new C1PdfDocumentSource();
                pds.LoadFromFile(outlabfilename);
                //pds.LoadFromFile(filename1);
                labOutView.DocumentSource = pds;
                pagesToScan = pds.PageCount;
                Application.DoEvents();
                for (int page = 1; page <= pagesToScan; page++) //(int page = 1; page <= reader.NumberOfPages; page++) <- for scanning all the pages in A PDF
                {
                    try
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
                            int indexcompn = line.LastIndexOf("Genome-Molecule Laboratory Co.,Ltd.");
                            if (indexcompn >= 0)
                            {
                                chkLabComp1.Checked = false;
                                chkLabComp2.Checked = true;     //GN
                            }

                            int indexpttname = line.LastIndexOf("PATIENT NAME");
                            if (indexpttname >= 0)
                            {
                                var pttname = line.Substring(indexpttname, (line.Length - indexpttname));
                                lbPttNmae.Text = pttname.Replace("PATIENT NAME", "").Trim();
                                chkLabComp1.Checked = true;    //medica 
                                chkLabComp2.Checked = false;
                            }
                            int indexhn = line.LastIndexOf("HN");
                            if (indexhn >= 0)
                            {
                                var ptthn = line.Substring(indexhn, (line.Length - indexhn));
                                String temphn = ptthn.Replace("HN", "").Replace(":", "").Trim();
                                if (temphn.Length >= 7)
                                {
                                    //txtHn.Value = txtHn.Text.Substring(0, 7);
                                    int chk = 0;
                                    String txt1 = "";
                                    txt1 = temphn.Trim();
                                    if(int.TryParse(txt1, out chk))
                                    {
                                        txtHn.Value = txt1;     // hn lenght = 8
                                    }
                                    else
                                    {
                                        txtHn.Value = temphn.Substring(0, 7);// hn lenght = 7
                                    }
                                }
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
                            int indexoutlaRequest = line.LastIndexOf("REQUEST");
                            if (indexoutlaRequest >= 0)
                            {
                                var pttname = line.Substring(indexoutlaRequest, (line.Length - indexoutlaRequest));
                                String txt = "", yy = "", mm = "", dd = "", year1="";
                                txt = pttname.Replace("REQUEST", "").Trim();
                                DateTime dtt1 = new DateTime();
                                //int.TryParse(year1, out year2);
                                yy = txt.Substring(txt.Length - 5, 2);
                                mm = txt.Substring(txt.Length - 7, 2);
                                dd = txt.Substring(txt.Length - 9, 2);
                                year1 = "20" + yy;
                                if (!DateTime.TryParse(year1 + "-" + mm + "-" + dd, out dtt1))
                                {
                                    new LogWriter("e", "Filename ชื่อ File ไม่สามารถหา date ได้ " + txt + " date " + year1 + "-" + mm + "-" + dd);
                                    continue;
                                }
                                reqid = txt.Substring(txt.Length - 3);
                                
                                dt = bc.bcDB.vsDB.SelectHnLabOut(reqid, year1 + "-" + mm + "-" + dd);
                                if (dt.Rows.Count <= 0)
                                {

                                }
                                txtVsDate.Value = "";
                            }
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        lbMessage.Text = "error " + ex.Message;
                    }

                }

            }
            catch (Exception ex)
            {
                new LogWriter("e", "setOutLab " + ex.Message);
                lbMessage.Text = "error " + ex.Message;
                Console.Write(ex);
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
            if (txtHn.Text.Length >= 7)
            {
                setControlHN();
                setGrf();
            }
            return reqid;
        }
        private void BtnBrow_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            //String cmd = "", args = "";
            //cmd = bc.iniC.pathline_bot_labout_urgent_bangna;
            //args = "1000000982";
            //ProcessStartInfo start = new ProcessStartInfo();
            //start.FileName = "python.exe";
            //start.Arguments = string.Format("{0} {1}", cmd, args);
            //start.UseShellExecute = true;
            //start.RedirectStandardOutput = true;
            //new LogWriter("d", "chkAttendUrgent 02 cmd " + cmd);
            //using (Process process = Process.Start(start))
            //{
            //    //using (StreamReader reader = process.StandardOutput)
            //    //{
            //    //    string result = reader.ReadToEnd();
            //    //    Console.Write(result);
            //    //}
            //}


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
                setOutLab(ofd.FileName);

                //Stream fs = File.OpenRead(@ofd.FileName);
                //chkAttendUrgent(null, fs);
            }
        }
        private void printLabOut()
        {
            if (!bc.iniC.statusLabOutAutoPrint.Equals("1")) return;
            SetDefaultPrinter(bc.iniC.printerLabOut);

            C1PdfDocumentSource pds = new C1PdfDocumentSource();

            //mstream.Seek(0, SeekOrigin.Begin);
            pds.LoadFromStream(streamPrint);
            pds.Print();
            //System.Threading.Thread.Sleep(200);
            ////streamPrint = mstream;
            //PrintDocument pd = new PrintDocument();
            //pd.PrintPage += Pd_PrintPage;
            ////here to select the printer attached to user PC
            
            //pd.Print();
            
        }        

        private void Timer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //new LogWriter("d", "Timer_Tick 01");
            //timer.Stop();
            listBox2.Items.Clear();
            Application.DoEvents();
            getFileinFolderInnoTech();
            listBox2.Items.Add("Check Innotect ");
            Application.DoEvents();
            Thread.Sleep(200);
            //new LogWriter("d", "Timer_Tick 02");
            getFileinFolderRIA();
            listBox2.Items.Add("Check RIA ");
            Application.DoEvents();
            Thread.Sleep(200);
            //new LogWriter("d", "Timer_Tick 03");
            if (chkFileInnoTech)
            {
                uploadFiletoServerInnoTech();
            }
            if (chkFileRIA)
            {
                uploadFiletoServerRIA();
            }
            String chkdate = "";
            chkdate = "2020-08-18";
            getFileinFolderMedica(chkdate);
            uploadFiletoServerMedicaOnLine(chkdate);
            Thread.Sleep(200);
            listBox2.Items.Add("Check ");
            Application.DoEvents();
            Thread.Sleep(200);
            //timer.Start();
        }
        private void getFileinFolderRIA()
        {
            DirectoryInfo d = new DirectoryInfo(bc.iniC.pathLabOutReceiveRIA);         
            FileInfo[] Files = d.GetFiles("*.zip"); //Getting Text files
            string str = "";
            //new LogWriter("d", "Timer_Tick 03");
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
                //new LogWriter("d", "uploadFiletoServerRIA zipFilename "+ zipFilename);
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
                //List<RIA_Patient> listRiaAtach = new List<RIA_Patient>();       //+0004
                RIA_Patient aaa = new RIA_Patient();       //-0004
                aaa.attach = "";       //-0004

                ZipFile zf = new ZipFile(zipFilename);
                zf.ExtractAll(pathbackup);
                DirectoryInfo dZip = new DirectoryInfo(pathbackup);
                FileInfo[] filesZip = dZip.GetFiles("*.*");
                //new LogWriter("d", "uploadFiletoServerRIA foreach (String zipFilename in filePaths) ");
                LabOutRIAResult objRiaR = null;
                foreach (FileInfo file in filesZip)
                {
                    ext = Path.GetExtension(file.FullName);
                    if (ext.ToLower().Equals(".json"))
                    {
                        using (StreamReader file1 = File.OpenText(file.FullName))
                        using (JsonTextReader reader = new JsonTextReader(file1))
                        {
                            try
                            {
                                //var ojbJson = JsonConvert.DeserializeObject<LabOutRIAResult>(reader);
                                LabOutRIAResult riaRes = (LabOutRIAResult)new JsonSerializer().Deserialize(reader, typeof(LabOutRIAResult));
                                //LabOutRIAResult riaRes = JsonConvert.DeserializeObject<LabOutRIAResult>(reader);
                                filename2 = riaRes.orderdetail.ref_no;
                                objRiaR = riaRes;
                            }
                            catch(Exception ex)
                            {

                            }
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
                    //MessageBox.Show("aaaaaa", "");
                    new LogWriter("e", "Filename ไม่พบข้อมูล reqid " + zipFilename);
                    continue;
                }
                dZip = null;
                zf.Dispose();
                reqid = filename2.Substring(filename2.Length - 3);
                yy = filename2.Substring(filename2.Length - 5, 2);
                mm = filename2.Substring(filename2.Length - 7, 2);
                dd = filename2.Substring(filename2.Length - 9, 2);
                year1 = "20" + yy;
                DateTime dttest = new DateTime();
                if(!DateTime.TryParse(year1 + "-" + mm + "-" + dd, out dttest))
                {
                    listBox2.Items.Add("Filename refid ไม่ถูกต้อง " + zipFilename);
                    Application.DoEvents();
                    String datetick = "", fileerr="", extfileerr = "";
                    fileerr = Path.GetFileName(zipFilename);
                    extfileerr = Path.GetExtension(zipFilename);
                    new LogWriter("e", "Filename refid ไม่ถูกต้อง หา req date ไม่ได้ " + reqid + " " + year1 + "-" + mm + "-" + dd+" ria filename "+ fileerr);

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
                        File.Move(zipFilename, bc.iniC.pathLabOutBackupRIA + "\\" + pathname + "\\err_" + filename2 + "_หา req date ไม่ได้" + "_" + datetick+"_"+ fileerr + extfileerr);
                    }
                    else
                    {
                        File.Move(zipFilename, bc.iniC.pathLabOutBackupRIA + "\\err_" + filename2 + "_หา req date ไม่ได้" + "_" + datetick + "_" + fileerr + extfileerr);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                DataTable dt = new DataTable();
                dt = bc.bcDB.vsDB.SelectHnLabOut(reqid, year1 + "-" + mm + "-" + dd);
                if (dt.Rows.Count <= 0)
                {
                    listBox2.Items.Add("Filename ไม่พบข้อมูล HIS " + zipFilename);
                    Application.DoEvents();
                    String datetick = "", fileerr = "", extfileerr = "";
                    fileerr = Path.GetFileName(zipFilename);
                    extfileerr = Path.GetExtension(zipFilename);
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
                        File.Move(zipFilename, bc.iniC.pathLabOutBackupRIA + "\\" + pathname + "\\err_" + filename2 + "_" + datetick + extfileerr);
                    }
                    else
                    {
                        File.Move(zipFilename, bc.iniC.pathLabOutBackupRIA + "\\err_" + filename2 + "_" + datetick + extfileerr);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                listBox2.Items.Add("พบข้อมูล HIS " + dt.Rows[0]["mnc_hn_no"].ToString());
                Application.DoEvents();
                if (!dt.Rows[0]["mnc_hn_no"].ToString().Equals(aaa.hn_customer))
                {
                    //MessageBox.Show("aaaaaa", "");
                    new LogWriter("e", "uploadFiletoServerRIA if (!dt.Rows[0]['mnc_hn_no'].ToString().Equals(aaa.hn_customer)) ");
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
                dsc.status_record = "2";        // status ria
                dsc.comp_labout_id = "1040000001";
                String re = bc.bcDB.dscDB.insertLabOut(dsc, bc.userId);
                String reattch = "";         //+0003
                Dictionary<string, string> dict = new Dictionary<string, string>();//+0004
                Dictionary<string, string> dict1 = new Dictionary<string, string>();//+0004
                
                Application.DoEvents();

                if (objRiaR != null)
                {
                    listBox2.Items.Add("มี Attach File ");
                    foreach (LabOutRIALabs ddd in objRiaR.labs)
                    {
                        foreach (Object att in ddd.attach)
                        {
                            if ((att != null) && (att.ToString().Length > 0))
                            {
                                listBox2.Items.Add("มี Attach File ");
                                String reattch1 = bc.bcDB.dscDB.insertLabOut(dsc, bc.userId);
                                dict.Add(reattch1, pathbackup+"\\"+att.ToString());
                                dict1.Add(reattch1, "");
                            }
                        }
                    }
                }

                if (re.Length <= 0)
                {
                    listBox2.Items.Add("ไม่ได้เลขที่ " + zipFilename);
                    Application.DoEvents();
                    String datetick = "", fileerr = "", extfileerr = "";
                    fileerr = Path.GetFileName(zipFilename);
                    extfileerr = Path.GetExtension(zipFilename);
                    new LogWriter("e", "ไม่ได้เลขที่ " + zipFilename);
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();

                    Thread.Sleep(200);
                    if (pathname.Length > 0)
                    {
                        File.Move(zipFilename, pathbackup + "\\err_" + filename2 + "_" + datetick + "_"+ fileerr + extfileerr);
                    }
                    else
                    {
                        File.Move(zipFilename, pathbackup + "\\err_" + filename2 + "_" + datetick + "_" + fileerr + extfileerr);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                listBox2.Items.Add("ได้เลขที่ " + re);
                Application.DoEvents();
                String extori = Path.GetExtension(filenamePDF);
                dsc.doc_scan_id = re;
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString() + "//" + dt.Rows[0]["mnc_hn_no"].ToString() + "_" + re + extori;
                
                vn = dsc.vn.Replace("/", "_").Replace("(", "_").Replace(")", "");

                dsc.ml_fm = "FM-LAB-996";       //RIA
                dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "-" + vn + "-" + re + extori;         //+1

                String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                String image_path_attach = "";
                
                if (dict.Count > 0)         //+0004
                {
                    foreach (KeyValuePair<string, string> pair1 in dict)
                    {
                        String extattach = Path.GetExtension(pair1.Value);
                        image_path_attach = dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "//" + dt.Rows[0]["mnc_hn_no"].ToString().Replace("/", "-") + "-" + vn + "-" + pair1.Key + extattach;         //+0003
                        String re1attach = bc.bcDB.dscDB.updateImagepath(image_path_attach, pair1.Key);
                        dict1[pair1.Key] = pair1.Value + "#" + image_path_attach;
                        listBox2.Items.Add("มี Attach File 1 " + image_path_attach);
                    }
                }

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
                String filenamezip = "";
                if(dsc.image_path.Length > 4)
                {
                    filenamezip = dsc.image_path.Substring(0,dsc.image_path.Length-4)+".zip";
                }

                if (ftp.upload(bc.iniC.folderFTP + "//" + filenamezip, zipFilename))
                {
                    Thread.Sleep(200);
                    String ext1 = "", ext2="";
                    ext1 = Path.GetExtension(filenamePDF);
                    ext2 = Path.GetExtension(dsc.image_path);
                    dsc.image_path = dsc.image_path.Replace(ext2, ext1);
                    if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filenamePDF))
                    {
                        if (dict.Count > 0)         //+0004
                        {
                            foreach (KeyValuePair<string, string> pair in dict1)
                            {
                                Thread.Sleep(200);         //+0003
                                String[] eee = pair.Value.Split('#');
                                if (eee.Length > 0)
                                {
                                    ftp.upload(bc.iniC.folderFTP + "//" + eee[1], eee[0]);         //+0004
                                    listBox2.Items.Add("มี Attach File Upload " + eee[0]);
                                }
                            }
                        }
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
                    bc.bcDB.laboDB.updateStatusResult(dsc.hn, dsc.date_req, dsc.req_id, "");        //630223
                    streamPrint = ftp.download(bc.iniC.folderFTP + "//" + dsc.image_path);
                    printLabOut();
                    chkAttendUrgent(dsc, streamPrint);
                    Application.DoEvents();
                    Thread.Sleep(1000 * 20);
                }
                else
                {
                    listBox2.Items.Add("FTP upload no success ");
                    Application.DoEvents();
                    new LogWriter("e", "FTP upload no success");
                }
            }
            timer.Start();
        }
        private void chkAttendUrgent(DocScan dsc, Stream stream)
        {
            try
            {
                stream.Position = 0;
                PdfReader reader = new PdfReader(stream);
                for (int page1 = 1; page1 <= reader.NumberOfPages; page1++)
                {
                    string strText = "";
                    ITextExtractionStrategy its = new LocationTextExtractionStrategy();
                    strText = PdfTextExtractor.GetTextFromPage(reader, page1, its);
                    Boolean chkUrgent = false;
                    String txtchktrue = "";
                    //แจ้งแพทย์ด่วน
                    //แจจงแพทยษดมวน แจจงแพทยษดมวน  แจจงแพทยนดดวน
                    if (strText.IndexOf("แจจงแพทยษดมวน") >= 0)
                    {
                        chkUrgent = true;
                        txtchktrue = "แจจงแพทยษดมวน";
                    }
                    if (strText.IndexOf("แจ้งแพทย์ด่วน") >= 0)
                    {
                        chkUrgent = true;
                        txtchktrue = "แจ้งแพทย์ด่วน";
                    }
                    if (strText.IndexOf("แจจงแพทยนดดวน") >= 0)
                    {
                        chkUrgent = true;
                        txtchktrue = "แจจงแพทยนดดวน";
                    }
                    if (strText.IndexOf("แจจงแพ") >= 0)
                    {
                        chkUrgent = true;
                        txtchktrue = "แจจงแพ";
                    }
                    //if (strText.IndexOf("แพทย") >= 0)
                    //{
                    //    chkUrgent = true;
                    //}
                    if (!chkUrgent)
                    {
                        string[] lines = strText.Split('\n');
                        foreach (String str in lines)
                        {
                            if (str.IndexOf("แจ้งแพทย์ด่วน") >= 0)
                            {
                                chkUrgent = true;
                                txtchktrue = "foreach แจ้งแพทย์ด่วน";
                            }
                            else if (str.IndexOf("แจจงแพทยษดมวน") >= 0)
                            {
                                chkUrgent = true;
                                txtchktrue = "foreach แจจงแพทยษดมวน";
                            }
                            else if (str.IndexOf("แจจงแพทยนดดวน") >= 0)
                            {
                                chkUrgent = true;
                                txtchktrue = "foreach แจจงแพทยนดดวน";
                            }
                        }
                    }
                    if (chkUrgent)
                    {
                        bc.bcDB.laboDB.updateStatusUrgentBydscid(dsc.hn, dsc.date_req, dsc.req_id);
                        new LogWriter("d", "chkAttendUrgent 01 hn "+ dsc.hn+ " doc_scan_id " + dsc.doc_scan_id);
                        String cmd = "", args = "";
                        cmd = bc.iniC.pathline_bot_labout_urgent_bangna;
                        if (!File.Exists(cmd))
                        {
                            new LogWriter("d", "chkAttendUrgent 01 hn " + cmd + " not found ");
                            return;
                        }
                        args = dsc.doc_scan_id;
                        ProcessStartInfo start = new ProcessStartInfo();
                        start.FileName = "python.exe";
                        start.Arguments = string.Format("{0} {1}", cmd, args);
                        start.UseShellExecute = false;
                        start.RedirectStandardOutput = true;
                        new LogWriter("d", "chkAttendUrgent 02 cmd "+ cmd+ " txtchktrue " + txtchktrue);
                        using (Process process = Process.Start(start))
                        {
                            //using (StreamReader reader = process.StandardOutput)
                            //{
                            //    string result = reader.ReadToEnd();
                            //    Console.Write(result);
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "chkAttendUrgent " + ex.Message);
            }
        }
        private Boolean getFileinFolderMedica(String manualDate)
        {
            timer.Stop();
            Boolean chk = false;
            List<LabOutMedicaResult> list = null;
            Stream stream = null;
            String currDate = DateTime.Now.Year.ToString()+"-"+DateTime.Now.ToString("MM-dd");
            if (manualDate.Length > 0)
            {
                currDate = manualDate;
            }            
            //currDate = "2020-07-24";
            listBox2.Items.Add("Check Medica date "+currDate+ " hosp_code=" + bc.iniC.laboutMedicahosp_code);
            Application.DoEvents();
            try
            {
                //String page = "http://119.59.102.111/app/getlist.php?date="+ currDate + "&hosp_code=CT-MD0166";
                String page = "http://119.59.102.111/app/getlist.php?date=" + currDate + "&hosp_code=" + bc.iniC.laboutMedicahosp_code;
                //WebClient webClient = new WebClient();
                var http = (HttpWebRequest)WebRequest.Create(new Uri(page));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";
                var response = http.GetResponse();
                Application.DoEvents();
                Thread.Sleep(100);
                stream = response.GetResponseStream();
                Application.DoEvents();
                Thread.Sleep(100);
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                dynamic obj = JsonConvert.DeserializeObject(content);
                Application.DoEvents();
                Thread.Sleep(100);
                //var list = JsonConvert.DeserializeObject<List<LabOutMedicaResult>>(content);
                list = JsonConvert.DeserializeObject<List<LabOutMedicaResult>>(content);
            }
            catch(Exception ex)
            {
                new LogWriter("e", "cannot connect to host medica " + ex.Message);
            }
            
            if (list == null)
            {
                chk = false;
                return chk;
            }
            listBox2.Items.Add("Check Medica lab Count " + list.Count);
            Application.DoEvents();
            foreach (LabOutMedicaResult lab in list)
            {
                chk = false;
                String datetick = "", path="", reqid = "";
                path = bc.iniC.folderFTPLabOutMedica + "//";
                //if (lab.labno.Equals("200714864"))
                //{
                //    string aaa1 = "";
                //}
                //else
                //{
                //    continue;
                //}
                //path = bc.iniC.hostFTPLabOutMedica + "medicalab/www/app/pdf/";
                String address = path+lab.labno+".pdf";
                Boolean chkFileExit = false;
                listBox2.Items.Add("Check Medica lab " + lab.labno+ " bc.iniC.hostFTPLabOutMedica "+ bc.iniC.hostFTPLabOutMedica+ " bc.iniC.userFTPLabOutMedica " + bc.iniC.userFTPLabOutMedica+ " bc.iniC.passFTPLabOutMedica " + bc.iniC.passFTPLabOutMedica+ " bc.ftpUsePassiveLabOut " + bc.ftpUsePassiveLabOut);
                Application.DoEvents();
                Thread.Sleep(200);
                FtpClient ftp = new FtpClient(bc.iniC.hostFTPLabOutMedica, bc.iniC.userFTPLabOutMedica, bc.iniC.passFTPLabOutMedica, bc.ftpUsePassiveLabOut);
                MemoryStream streamresult = ftp.download(address);
                if (streamresult == null)
                {
                    continue;
                }
                if (streamresult.Length == 0)
                {
                    continue;
                }
                
                streamresult.Seek(0, SeekOrigin.Begin);
                listBox2.Items.Add("Check Medica ftp " + lab.labno);
                Application.DoEvents();
                datetick = DateTime.Now.Ticks.ToString();
                if (!Directory.Exists(bc.iniC.pathLabOutReceiveMedica+"\\"+ currDate))
                {
                    Directory.CreateDirectory(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate);
                    listBox2.Items.Add("CreateDirectory " + bc.iniC.pathLabOutReceiveMedica + "\\" + currDate);
                    new LogWriter("d", "CreateDirectory " + bc.iniC.pathLabOutReceiveMedica + "\\" + currDate);
                }
                const Int32 BufferSize = 128;
                if(!File.Exists(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate + "\\list.txt"))
                {
                    using (StreamWriter w = File.AppendText(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate + "\\list.txt"))
                    {
                        w.WriteLine("");
                    }
                }
                using (var fileStream = File.OpenRead(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate + "\\list.txt"))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.Equals(lab.labno))
                        {
                            chkFileExit = true;
                            break;
                        }
                    }
                    // Process line
                }
                new LogWriter("d", "PdfReader 00 chkFileExit"+ chkFileExit);
                if (chkFileExit) continue;
                //if (!File.Exists(bc.iniC.pathLabOutReceiveMedica + "\\" + lab.labno + ".pdf"))
                //{
                MemoryStream aaa = new MemoryStream();
                streamresult.CopyTo(aaa);
                aaa.Position = 0;
                int pagesToScan = 2;
                //PdfReader reader = new PdfReader(streamresult);
                try
                {
                    new LogWriter("d", "PdfReader 01 ");
                    PdfReader reader = new PdfReader(aaa);
                    for (int page1 = 1; page1 <= reader.NumberOfPages; page1++)
                    {
                        string strText = "";
                        if (page1 > 1) break;
                        chk = false;
                        ITextExtractionStrategy its = new LocationTextExtractionStrategy();
                        strText = PdfTextExtractor.GetTextFromPage(reader, page1, its);
                        string[] lines = strText.Split('\n');
                        new LogWriter("d", "PdfReader 02 lines.Length " + lines.Length);
                        if (lines.Length >= 4)
                        {
                            String txt = "";
                            txt = lines[4];
                            String[] txt1 = txt.Split(' ');
                            String txt2 = "";
                            if (txt1.Length >= 1)
                            {
                                txt2 = txt1[txt1.Length - 1];
                            }
                            long reqid1 = 0;
                            if(long.TryParse(txt2.Trim(), out reqid1))
                            {
                                reqid = txt2.ToString();
                                chk = true;
                            }
                            else
                            {
                                //ตัวรองสุดท้าย ว่า ใช่ไหม
                                if (txt1.Length >= 2)
                                {
                                    txt2 = txt1[txt1.Length - 2];
                                }
                                long reqid2 = 0;
                                if (long.TryParse(txt2.Trim(), out reqid2))
                                {
                                    reqid = txt2.ToString();
                                    chk = true;
                                }
                                else
                                {
                                    //ตัวถัดมา ว่า ใช่ไหม
                                    if (txt1.Length >= 3)
                                    {
                                        txt2 = txt1[txt1.Length - 3];
                                    }
                                    long reqid3 = 0;
                                    if (long.TryParse(txt2.Trim(), out reqid3))
                                    {
                                        reqid = txt2.ToString();
                                        chk = true;
                                    }
                                    //ตัวถัดมา ว่า ใช่ไหม
                                    if (txt1.Length >= 4)
                                    {
                                        txt2 = txt1[txt1.Length - 4];
                                    }
                                    long reqid4 = 0;
                                    if (long.TryParse(txt2.Trim(), out reqid4))
                                    {
                                        reqid = txt2.ToString();
                                        chk = true;
                                    }
                                    //ตัวถัดมา ว่า ใช่ไหม
                                    if (txt1.Length >= 5)
                                    {
                                        txt2 = txt1[txt1.Length - 5];
                                    }
                                    long reqid5 = 0;
                                    if (long.TryParse(txt2.Trim(), out reqid5))
                                    {
                                        reqid = txt2.ToString();
                                        chk = true;
                                    }
                                }
                                if (!chk)
                                {
                                    foreach(String chkrequest in lines)
                                    {
                                        if (chkrequest.ToUpper().IndexOf("REQUEST")>=0)
                                        {
                                            long reqid6 = 0;
                                            if (long.TryParse(chkrequest.Trim().Replace("REQUEST", ""), out reqid6))
                                            {
                                                reqid = chkrequest.ToString();
                                                chk = true;
                                                break;
                                            }
                                            //String bbb = "";
                                            //reqid = chkrequest.Replace("REQUEST", "");
                                            //chk = true;
                                            //new LogWriter("d", "foreach(String chkrequest in lines) reqid " + reqid);
                                            
                                        }
                                    }
                                }
                            }
                            if (!chk)
                            {
                                txt = lines[5];
                                txt1 = txt.Split(' ');
                                txt2 = "";
                                if (txt1.Length >= 1)
                                {
                                    txt2 = txt1[txt1.Length - 1];
                                }
                                reqid1 = 0;
                                if (long.TryParse(txt2.Trim(), out reqid1))
                                {
                                    //reqid = reqid1.ToString();        //มี bug ถ้ามี ) นำหน้า
                                    reqid = txt2.Trim();
                                    chk = true;
                                }
                                else
                                {
                                    txt2 = "";
                                    txt2 = txt1[1];
                                    if (long.TryParse(txt2.Trim(), out reqid1))
                                    {
                                        reqid = txt2.Trim();
                                        chk = true;
                                    }
                                    else
                                    {
                                        txt2 = "";
                                        txt2 = txt1[2];
                                        if (long.TryParse(txt2.Trim(), out reqid1))
                                        {
                                            reqid = txt2.Trim();
                                            chk = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    reader.Close();
                    listBox2.Items.Add("Check Medica chk " + chk);
                    //streamresult.Position = 0;
                    if (chk == false)
                    {
                        if(!File.Exists(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate + "\\" + lab.labno + ".pdf"))
                        {
                            var fileStream = new FileStream(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate + "\\" + lab.labno + ".pdf", FileMode.Create, FileAccess.Write);
                            streamresult.Position = 0;
                            streamresult.CopyTo(fileStream);
                            fileStream.Flush();
                            fileStream.Dispose();
                            listBox2.Items.Add("Check Medica write file " + lab.labno);
                            Application.DoEvents();
                        }
                        else
                        {
                            var fileStream = new FileStream(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate + "\\" + lab.labno + "_"+ datetick + ".pdf", FileMode.Create, FileAccess.Write);
                            streamresult.Position = 0;
                            streamresult.CopyTo(fileStream);
                            fileStream.Flush();
                            fileStream.Dispose();
                            listBox2.Items.Add("Check Medica write file " + lab.labno + "_" + datetick);
                            Application.DoEvents();
                        }
                        using (StreamWriter w = File.AppendText(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate + "\\list.txt"))
                        {
                            w.WriteLine(lab.labno);
                        }
                        Application.DoEvents();
                        Thread.Sleep(200);
                        listBox2.Items.Add("Check Medica ไม่มี req id ");
                        Application.DoEvents();
                    }
                    else
                    {
                        Boolean chkfile = false;
                        using (StreamReader sr1 = File.OpenText(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate + "\\list.txt"))
                        {
                            String s = "";
                            while ((s = sr1.ReadLine()) != null)
                            {
                                listBox2.Items.Add("Check file FTP reqid "+ reqid +" s "+ s);
                                Application.DoEvents();
                                Thread.Sleep(100);
                                if (s.Equals(reqid))
                                {
                                    chkfile = true;
                                    listBox2.Items.Add("Check Medica พบ file FTP เรียบร้อย  ");
                                    Application.DoEvents();
                                    Thread.Sleep(100);
                                }
                            }
                        }
                        if (!File.Exists(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate + "\\" + reqid + ".pdf") && !chkfile)
                        {
                            streamresult.Position = 0;
                            var fileStream = new FileStream(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate + "\\" + reqid + ".pdf", FileMode.Create, FileAccess.Write);
                            streamresult.CopyTo(fileStream);
                            Thread.Sleep(200);
                            Application.DoEvents();
                            fileStream.Flush();
                            fileStream.Dispose();
                            listBox2.Items.Add("Check Medica write file " + reqid);
                            Application.DoEvents();
                            using (StreamWriter w = File.AppendText(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate + "\\list.txt"))
                            {
                                w.WriteLine(reqid);
                            }
                        }
                        listBox2.Items.Add("Check Medica req id " + reqid);
                    }
                    Application.DoEvents();

                }
                catch (Exception ex)
                {
                    new LogWriter("e", "getFileinFolderMedica " + ex.Message);
                }

                //}
                if (chkFileExit) continue;
                
                streamresult.Close();
                Application.DoEvents();
                Thread.Sleep(200);
            }
            stream.Close();
            timer.Start();
            return chk;
            //MemoryStream stream = ftp.download("");
        }
        private void getFileinFolderInnoTech()
        {
            listBox3.Items.Clear();     //listBox1
            //new LogWriter("d", "getFileinFolderInnoTech "+ bc.iniC.pathLabOutReceiveInnoTech);
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
            Thread.Sleep(500);
            //new LogWriter("e", "uploadFiletoServerInnoTech 00");
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
            //new LogWriter("d", "uploadFiletoServerInnoTech 01");
            String dgssid = "";
            dgssid = bc.bcDB.dgssDB.getIdDgss("Document Other");
            DocGroupSubScan dgss = new DocGroupSubScan();
            dgss = bc.bcDB.dgssDB.selectByPk(dgssid);
            foreach (String filename in filePaths)
            {
                listBox2.Items.Add("พบ file " + filename);
                //new LogWriter("d", "uploadFiletoServerInnoTech file " + filename);
                Application.DoEvents();
                int year2 = 0;
                String yy = "", mm = "", dd = "", reqid = "", vn = "", filename1 = "", filename2 = "", year1 = "", ext = "";
                String pathname = "", tmp = "";
                //tmp = bc.iniC.pathLabOutReceiveInnoTech.Replace("\\\\", "\\");
                filename1 = Path.GetFileName(filename);
                //new LogWriter("d", "uploadFiletoServerInnoTech file " + filename+" 999");
                pathname = filename.Replace(filename1, "").Replace(bc.iniC.pathLabOutReceiveInnoTech, "").Replace("\\", "");                
                pathname = pathname.Replace("\\", "");
                //pathname = Path.GetDirectoryName(filename);
                //pathname = pathname.Replace(bc.iniC.pathLabOutReceiveInnoTech,"").Replace("\\", "");
                //String[] txt = filename1.Split('_');
                //if (txt.Length > 1)
                //{
                //    filename2 = txt[0];
                //}
                //else
                //{
                //    filename2 = filename1.Replace(".pdf", "");
                //}
                ext = Path.GetExtension(filename1);
                filename2 = Path.GetFileNameWithoutExtension(filename);
                //new LogWriter("d", "uploadFiletoServerInnoTech file " + filename+" 000");
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
                //new LogWriter("d", "uploadFiletoServerInnoTech file " + filename + " 001");
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
                //new LogWriter("d", "uploadFiletoServerInnoTech 01 000");
                DateTime dtt1 = new DateTime();
                int.TryParse(year1, out year2);
                if (filename2.IndexOf("(")>0)
                {
                    String aaa = "";
                    aaa = filename2.Substring(0, filename2.IndexOf("("));
                    aaa = aaa.Replace("_", "").Replace("-", "");
                    yy = aaa.Substring(aaa.Length - 5, 2);
                    mm = aaa.Substring(aaa.Length - 7, 2);
                    dd = aaa.Substring(aaa.Length - 9, 2);
                }
                else
                {
                    //filename2 = filename2.Replace("_", "").Replace("-", "");
                    yy = filename2.Substring(filename2.Length - 5, 2);
                    mm = filename2.Substring(filename2.Length - 7, 2);
                    dd = filename2.Substring(filename2.Length - 9, 2);
                }
                
                year1 = "20" + yy;
                if (!DateTime.TryParse(year1 + "-" + mm + "-" + dd, out dtt1))
                {
                    String datetick = "";
                    new LogWriter("e", "Filename ชื่อ File ไม่สามารถหา date ได้ " + filename + " date "+ year1 + "-" + mm + "-" + dd);
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
                if (filename2.IndexOf("(") > 0)
                {
                    String aaa = "";
                    aaa = filename2.Substring(0, filename2.IndexOf("("));
                    aaa = aaa.Replace("_", "").Replace("-", "");
                    reqid = aaa.Substring(aaa.Length - 3);
                }
                else
                {
                    reqid = filename2.Substring(filename2.Length - 3);
                }
                listBox2.Items.Add("filename2 " + filename2 + " reqid " + reqid + " " + year1 + "-" + mm + "-" + dd);
                Application.DoEvents();
                //new LogWriter("e", "uploadFiletoServerInnoTech 01 001");
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
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\" + pathname + "\\err_select_HIS_not_found_" + reqid+"_"+ year1 + "-" + mm + "-" + dd + "_" + filename2 + "_" + datetick + ext);
                    }
                    else
                    {
                        File.Move(filename, bc.iniC.pathLabOutBackupInnoTech + "\\err_select_HIS_not_found_" + reqid + "_" + year1 + "-" + mm + "-" + dd + "_" + filename2 + "_" + datetick + ext);
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                //new LogWriter("d", "uploadFiletoServerInnoTech 01 002");
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
                //new LogWriter("e", "uploadFiletoServerInnoTech 02");
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
                dsc.comp_labout_id = "1040000000";
                String re = bc.bcDB.dscDB.insertLabOut(dsc, bc.userId);
                //new LogWriter("e", "uploadFiletoServerInnoTech 03");
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
                dsc.doc_scan_id = re;
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
                    bc.bcDB.laboDB.updateStatusResult(dsc.hn, dsc.date_req, dsc.req_id, "");        //630223
                    Application.DoEvents();
                    streamPrint = ftp.download(bc.iniC.folderFTP + "//" + dsc.image_path);
                    chkAttendUrgent(dsc, streamPrint);
                    printLabOut();
                    Thread.Sleep(1000 * 30);
                }
                else
                {
                    listBox2.Items.Add("FTP upload no success ");
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
            Thread.Sleep(200);

            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
            //String[] filePaths = Directory.GetFiles(bc.iniC.pathLabOutReceive, "*.*", SearchOption.TopDirectoryOnly);
            List<String> filePaths = new List<String>();
            listBox2.Items.Add("Check Medica date " );
            Application.DoEvents();
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
                    MessageBox.Show("ไม่พบ Request ใน HIS "+ txtVsDate.Text.Trim(), "");
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
                if (chkLabComp1.Checked)
                {
                    dsc.status_record = "2";        // medica
                    dsc.ml_fm = "FM-LAB-995";
                    dsc.comp_labout_id = "1040000002";
                }
                else if (chkLabComp2.Checked)
                {
                    dsc.status_record = "2";        // gm
                    dsc.ml_fm = "FM-LAB-994";
                    dsc.comp_labout_id = "1040000003";
                }
                
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
                    Thread.Sleep(500);
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
                lbMessage.Text = "ได้เลขที่ " + re+" Update Ok";
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
                    lbMessage.Text = "ได้เลขที่ "+" [Update Ok] Upload OK";
                    listBox2.Items.Add("FTP upload success ");
                    Application.DoEvents();
                    Thread.Sleep(500);
                    String datetick = "";
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupManual))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupManual);
                        Thread.Sleep(500);
                    }
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
                    bc.bcDB.laboDB.updateStatusResult(dsc.hn, dsc.date_req, dsc.req_id, "");        //630223
                    Application.DoEvents();
                    lbMessage.Text = "Upload เรียบร้อย";
                    Thread.Sleep(500);
                }
                else
                {
                    listBox2.Items.Add("FTP upload success ");
                    Application.DoEvents();
                    new LogWriter("e", "FTP upload no success");
                }
            }
            //timer.Start();
        }
        private void uploadFiletoServerMedicaOnLine(String manualDate)
        {
            String currDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd");
            timer.Stop();
            if (manualDate.Length > 0)
            {
                currDate = manualDate;
            }
            //listBox2.Items.Clear();     //listBox3
            Application.DoEvents();
            Thread.Sleep(200);
            //currDate = "2020-06-24";
            if (!Directory.Exists(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate))
            {
                Directory.CreateDirectory(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate);
            }
            //new LogWriter("e", "uploadFiletoServerInnoTech 00");
            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
            //String[] filePaths = Directory.GetFiles(bc.iniC.pathLabOutReceive, "*.*", SearchOption.TopDirectoryOnly);
            List<String> filePaths = new List<String>();
            //currDate = "2020-06-19";
            DirectoryInfo dirs = new DirectoryInfo(bc.iniC.pathLabOutReceiveMedica + "\\" + currDate);
            listBox2.Items.Add("uploadFiletoServerMedicaOnLine currDate " + currDate + " hosp_code=" + bc.iniC.laboutMedicahosp_code+ " filePaths " + filePaths.Count);
            listBox2.Items.Add("filePaths  " + bc.iniC.pathLabOutReceiveMedica + "\\" + currDate);
            Application.DoEvents();
            FileInfo[] Files = dirs.GetFiles("*.pdf");
            foreach (FileInfo file in Files)
            {                
                filePaths.Add(file.FullName);                
            }
            //new LogWriter("d", "uploadFiletoServerInnoTech 01");
            String dgssid = "";
            dgssid = bc.bcDB.dgssDB.getIdDgss("Document Other");
            DocGroupSubScan dgss = new DocGroupSubScan();
            dgss = bc.bcDB.dgssDB.selectByPk(dgssid);
            foreach (String filename in filePaths)
            {
                listBox2.Items.Add("พบ file " + filename);
                //new LogWriter("d", "uploadFiletoServerInnoTech file " + filename);
                Application.DoEvents();
                int year2 = 0;
                //continue;
                String yy = "", mm = "", dd = "", reqid = "", vn = "", filename1 = "", filename2 = "", year1 = "", ext = "";
                String pathname = "", tmp = "";
                //tmp = bc.iniC.pathLabOutReceiveInnoTech.Replace("\\\\", "\\");
                filename1 = Path.GetFileName(filename);
                //new LogWriter("d", "uploadFiletoServerInnoTech file " + filename+" 999");
                //pathname = filename.Replace(filename1, "").Replace(bc.iniC.pathLabOutReceiveMedica, "").Replace("\\", "");
                //pathname = pathname.Replace("\\", "");
                pathname = Path.GetDirectoryName(filename);
                
                filename2 = Path.GetFileNameWithoutExtension(filename);
                ext = Path.GetExtension(filename1);
                //new LogWriter("d", "uploadFiletoServerInnoTech file " + filename+" 000");
                if (filename2.Replace(".pdf", "").Length < 10)
                {
                    String datetick = "";
                    new LogWriter("e", "madical online Filename ไม่ถูก FORMAT" + filename);
                    listBox2.Items.Add("Filename ไม่ถูก FORMAT " + filename);
                    //MessageBox.Show("Filename ไม่ถูก FORMAT", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupMedica))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupMedica);
                    }
                    if (pathname.Length > 0)
                    {
                        if (!Directory.Exists(bc.iniC.pathLabOutBackupMedica + "\\Backup" ))
                        {
                            Directory.CreateDirectory(bc.iniC.pathLabOutBackupMedica + "\\Backup");
                        }
                    }
                    Thread.Sleep(200);
                    
                    File.Move(filename, bc.iniC.pathLabOutBackupMedica + "\\backup\\err_not_correct_FORMAT" + filename2 + "_" + datetick + ext);
                    
                    Thread.Sleep(500);
                    continue;
                }
                //new LogWriter("d", "uploadFiletoServerInnoTech file " + filename + " 001");
                if (filename2.Length < 10)
                {
                    String datetick = "";
                    new LogWriter("e", "Filename ชื่อ File สั้นไป " + filename);
                    listBox2.Items.Add("Filename ชื่อ File สั้นไป " + filename);
                    //MessageBox.Show("Filename ไม่ถูก FORMAT", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupMedica))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupMedica);
                    }
                    
                    Thread.Sleep(200);
                    
                    File.Move(filename, bc.iniC.pathLabOutBackupMedica + "\\err_File_short" + filename2 + "_" + datetick + ext);
                    
                    Thread.Sleep(500);
                    continue;
                }
                //new LogWriter("d", "uploadFiletoServerInnoTech 01 000");
                DateTime dtt1 = new DateTime();
                int.TryParse(year1, out year2);
                yy = filename2.Substring(filename2.Length - 5, 2);
                mm = filename2.Substring(filename2.Length - 7, 2);
                dd = filename2.Substring(filename2.Length - 9, 2);
                year1 = "20" + yy;
                if (!DateTime.TryParse(year1 + "-" + mm + "-" + dd, out dtt1))
                {
                    String datetick = "";
                    new LogWriter("e", "Filename ชื่อ File ไม่สามารถหา date ได้ " + filename + " date " + year1 + "-" + mm + "-" + dd);
                    listBox2.Items.Add("Filename ชื่อ File ไม่สามารถหา date ได้ " + filename);
                    //MessageBox.Show("Filename ไม่ถูก FORMAT", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupMedica))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupMedica);
                    }
                    
                    Thread.Sleep(200);
                    
                    File.Move(filename, bc.iniC.pathLabOutBackupMedica + "\\err_File_no_found_date" + filename2 + "_" + datetick + ext);
                    
                    Thread.Sleep(500);
                    continue;
                }
                reqid = filename2.Substring(filename2.Length - 3);
                //new LogWriter("e", "uploadFiletoServerInnoTech 01 001");
                DataTable dt = new DataTable();
                dt = bc.bcDB.vsDB.SelectHnLabOut(reqid, year1 + "-" + mm + "-" + dd);
                if (dt.Rows.Count <= 0)
                {
                    listBox2.Items.Add("Filename ไม่พบข้อมูล HIS " + filename + " reqid " + reqid + " " + year1 + "-" + mm + "-" + dd);
                    Application.DoEvents();
                    String datetick = "";
                    new LogWriter("e", "Filename ไม่พบข้อมูล HIS" + filename + " reqid " + reqid + " " + year1 + "-" + mm + "-" + dd);
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupMedica))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupMedica);
                    }
                    
                    Thread.Sleep(200);
                    
                    File.Move(filename, bc.iniC.pathLabOutBackupMedica + "\\err_not_found_HIS" + filename2 + "_" + datetick + ext);
                    
                    Thread.Sleep(500);
                    continue;
                }
                //new LogWriter("d", "uploadFiletoServerInnoTech 01 002");
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
                //new LogWriter("e", "uploadFiletoServerInnoTech 02");
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
                                
                dsc.ml_fm = "FM-LAB-995";
                dsc.comp_labout_id = "1040000002";
                dsc.patient_fullname = dt.Rows[0]["mnc_patname"].ToString();
                dsc.status_record = "2";
                String re = bc.bcDB.dscDB.insertLabOut(dsc, bc.userId);
                //new LogWriter("e", "uploadFiletoServerInnoTech 03");
                if (re.Length <= 0)
                {
                    listBox2.Items.Add("ไม่ได้เลขที่ " + filename);
                    Application.DoEvents();
                    String datetick = "";
                    new LogWriter("e", "ไม่ได้เลขที่ " + filename);
                    //MessageBox.Show("Filename ไม่พบข้อมูล HIS", "");
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupMedica))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupMedica);
                    }
                    
                    Thread.Sleep(200);
                    
                    File.Move(filename, bc.iniC.pathLabOutBackupMedica + "\\err_no_req_id" + filename2 + "_" + datetick + ext);
                    
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
                    Thread.Sleep(200);
                    String datetick = "";
                    datetick = DateTime.Now.Ticks.ToString();
                    if (!Directory.Exists(bc.iniC.pathLabOutBackupMedica))
                    {
                        Directory.CreateDirectory(bc.iniC.pathLabOutBackupMedica);
                    }
                    Thread.Sleep(500);
                    File.Move(filename, bc.iniC.pathLabOutBackupMedica + "\\" + filename2 + "_" + datetick + ext);
                    
                    listBox1.BeginUpdate();     //listBox2
                    listBox1.Items.Add(filename + " -> " + bc.iniC.hostFTP + "//" + bc.iniC.folderFTP + "//" + dsc.image_path);     //listBox2
                    listBox1.EndUpdate();     //listBox2
                    bc.bcDB.laboDB.updateStatusResult(dsc.hn, dsc.date_req, dsc.req_id, "");        //630223
                    Application.DoEvents();
                    streamPrint = ftp.download(bc.iniC.folderFTP + "//" + dsc.image_path);
                    printLabOut();
                    Thread.Sleep(1000);
                }
                else
                {
                    listBox2.Items.Add("FTP upload no success ");
                    Application.DoEvents();
                    new LogWriter("e", "FTP upload no success");
                }
            }
            timer.Start();
        }
        private void uploadFiletoServerMedCarilo()
        {
            if (txtMedVsDate.Text.Length <= 0)
            {
                MessageBox.Show("วันที่ ไม่มีค่า", "");
                return;
            }
            if (txtMedVnAn.Text.Length <= 0)
            {
                MessageBox.Show("VN An ไม่มีค่า", "");
                return;
            }
            timer.Stop();
            lbMedMessage.Text = "เตรียม Upload";
            listBox2.Items.Clear();     //listBox3
            Application.DoEvents();
            Thread.Sleep(1000);

            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
            //String[] filePaths = Directory.GetFiles(bc.iniC.pathLabOutReceive, "*.*", SearchOption.TopDirectoryOnly);
            List<String> filePaths = new List<String>();

            filePaths.Add(txtMedFilename.Text);
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
                String pathname1 = "", filename11 = "", pttname1 = "";
                pathname1 = Path.GetDirectoryName(txtMedFilename.Text.Trim());
                filename11 = Path.GetFileNameWithoutExtension(txtMedFilename.Text.Trim());
                String lastFolderName = Path.GetFileName(pathname1.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                String[] pttname2 = lastFolderName.Split('_');

                filename2 = txtMedHn.Text.Trim();

                ext = Path.GetExtension(filename);
                
                DateTime dtt1 = new DateTime();
                int.TryParse(year1, out year2);
                //yy = filename2.Substring(filename2.Length - 5, 2);
                mm = txtMedVsDate.Text.Substring(3, 2);
                dd = txtMedVsDate.Text.Substring(0, 2);
                year1 = txtMedVsDate.Text.Substring(txtMedVsDate.Text.Length - 4, 4);
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
                
                reqid = "";
                listBox2.Items.Add("พบข้อมูล HIS " + txtMedHn.Text);
                Application.DoEvents();
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = dgss.doc_group_id;
                dsc.hn = txtMedHn.Text.Trim();
                //dsc.vn = dt.Rows[0]["MNC_VN_NO"].ToString() + "/" + dt.Rows[0]["MNC_VN_SEQ"].ToString() + "(" + dt.Rows[0]["MNC_VN_SUM"].ToString() + ")";
                if (chkMedIPD.Checked)
                {
                    dsc.an = txtMedVnAn.Text.Trim();
                    if (dsc.an.Equals("/"))
                    {
                        dsc.an = "";
                    }
                    //dsc.an_date
                }
                else
                {
                    dsc.vn = txtMedVnAn.Text.Trim();
                    dsc.an = "";
                }                
                //dsc.an = dt.Rows[0]["MNC_AN_NO"].ToString() + "/" + dt.Rows[0]["MNC_AN_YR"].ToString();
                dsc.visit_date = bc.datetoDB(txtMedVsDate.Text.Trim());
                dsc.host_ftp = bc.iniC.hostFTP;
                //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                dsc.image_path = "";
                dsc.doc_group_sub_id = dgssid;
                //dsc.pre_no = dt.Rows[0]["mnc_pre_no"].ToString();
                dsc.pre_no = preno;

                dsc.folder_ftp = bc.iniC.folderFTP;
                //    dsc.status_ipd = chkIPD.Checked ? "I" : "O";
                dsc.row_no = "1";
                dsc.row_cnt = "1";
                dsc.status_version = "2";
                dsc.req_id = reqid;
                DateTime dtt = new DateTime();

                dsc.date_req = dsc.visit_date;
                if (dsc.an.Length > 0)
                {
                    dsc.status_ipd = "1";
                }
                else
                {
                    dsc.status_ipd = "0";
                }
                //dsc.ml_fm = "FM-LAB-999";
                if (chkMedCarilo.Checked)
                {
                    dsc.ml_fm = "FM-MED-999";       //Med Carilo
                }
                else if (chkMedEcho.Checked)
                {
                    dsc.ml_fm = "FM-MED-998";       //Med Echo
                }
                else if (chkMedEndoscope.Checked)
                {
                    dsc.ml_fm = "FM-MED-997";       //Med Carilo
                }
                else if (chkMedHolter.Checked)
                {
                    dsc.ml_fm = "FM-MED-996";       //Med Holter
                }

                dsc.patient_fullname = lbMedPttNmae.Text.Trim();
                dsc.status_record = "3";
                String re = bc.bcDB.dscDB.insertMed(dsc, bc.userId);
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
                lbMedMessage.Text = "ได้เลขที่ " + re;
                listBox2.Items.Add("ได้เลขที่ " + re);
                Application.DoEvents();
                //dsc.image_path = dt.Rows[0]["mnc_hn_no"].ToString() + "//" + dt.Rows[0]["mnc_hn_no"].ToString() + "_" + re + ext;
                dsc.image_path = txtMedHn.Text.Trim() + "//" + txtMedHn.Text.Trim() + "_" + re + ext;
                //    if (chkIPD.Checked)
                //    {
                //        vn = txtAN.Text.Replace("/", "_").Replace("(", "_").Replace(")", "");
                //    }
                //    else
                //    {
                vn = dsc.vn.Replace("/", "_").Replace("(", "_").Replace(")", "");

                //dsc.ml_fm = "FM-MED-999";       //Med Carilo

                //    }
                //    //dsc.image_path = txtHn.Text.Replace("/", "-") + "-" + vn + "//" + txtHn.Text.Replace("/", "-") + "-" + vn + "-" + re + ext;       //-1
                dsc.image_path = txtMedHn.Text.Trim().Replace("/", "-") + "//" + txtMedHn.Text.Trim().Replace("/", "-") + "-" + vn + "-" + re + ext;         //+1                

                String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                listBox2.Items.Add("updateImagepath " + dsc.image_path);
                Application.DoEvents();
                //    //MessageBox.Show("111", "");

                ftp.createDirectory(bc.iniC.folderFTP + "//" + txtMedHn.Text.Trim().Replace("/", "-"));       // สร้าง Folder HN
                //    ftp.createDirectory(bc.iniC.folderFTP + "//" + txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + vn);
                //    //MessageBox.Show("222", "");
                ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                //    //MessageBox.Show("333", "");
                Thread.Sleep(200);
                if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename))
                {
                    listBox2.Items.Add("FTP upload success ");
                    Application.DoEvents();
                    //Thread.Sleep(1000);
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
                    catch (Exception ex)
                    {
                        MessageBox.Show("upload file ผล สำเร็จ \nแต่ไม่สามารถ move file ได้\n" + "ex " + ex.Message+"\n"+ bc.iniC.pathLabOutBackupManual + "\\" + filename2 + "_" + datetick + ext, "");
                    }

                    listBox1.BeginUpdate();     //listBox2
                    listBox1.Items.Add(filename + " -> " + bc.iniC.hostFTP + "//" + bc.iniC.folderFTP + "//" + dsc.image_path);     //listBox2
                    listBox1.EndUpdate();     //listBox2
                    //bc.bcDB.laboDB.updateStatusResult(dsc.hn, dsc.date_req, dsc.req_id, "");        //630223
                    Application.DoEvents();
                    lbMedMessage.Text = "Upload เรียบร้อย";
                    Thread.Sleep(1000);
                }
                else
                {
                    listBox2.Items.Add("FTP upload success ");
                    Application.DoEvents();
                    new LogWriter("e", "FTP upload no success");
                }
            }
            //timer.Start();
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
        private void setGrfVsOPDMed()
        {
            //grfVisit.Clear();
            grfMedVisit.Cols.Count = 10;
            //grfVisit.Rows.Count = 0;
            grfMedVisit.Rows.Count = 1;
            grfMedVisit.Cols[colVsVsDate].Width = 100;
            grfMedVisit.Cols[colVsVn].Width = 80;
            grfMedVisit.Cols[colVsDept].Width = 240;
            grfMedVisit.Cols[colVsPreno].Width = 100;
            grfMedVisit.Cols[colVsStatus].Width = 60;
            grfMedVisit.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfMedVisit.Cols[colVsVsDate].Caption = "Visit Date";
            grfMedVisit.Cols[colVsVn].Caption = "VN";
            grfMedVisit.Cols[colVsDept].Caption = "แผนก";
            grfMedVisit.Cols[colVsPreno].Caption = "";
            grfMedVisit.Cols[colVsAn].Caption = "AN";
            grfMedVisit.Cols[colVsPreno].Visible = false;
            grfMedVisit.Cols[colVsVn].Visible = true;
            grfMedVisit.Cols[colVsAn].Visible = true;
            grfMedVisit.Cols[colVsAndate].Visible = false;
            //grfVisit.Rows[0].Visible = false;
            grfMedVisit.Cols[0].Visible = false;
            grfMedVisit.Cols[colVsVsDate].AllowEditing = false;
            grfMedVisit.Cols[colVsVn].AllowEditing = false;
            grfMedVisit.Cols[colVsDept].AllowEditing = false;
            grfMedVisit.Cols[colVsPreno].AllowEditing = false;

            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dt = bc.bcDB.vsDB.selectVisitByHn4(txtMedHn.Text, "O");
            int i = 1, j = 1;
            j = dt.Rows.Count + 1;
            grfMedVisit.Rows.Count = j;

            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfMedVisit.Rows[i];
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
        private void setGrfVsHMed()
        {
            grfMedVisit.Clear();
            grfMedVisit.Rows.Count = 1;
            grfMedVisit.Cols.Count = 10;

            grfMedVisit.Cols[colVsVsDate].Width = 100;
            grfMedVisit.Cols[colVsVn].Width = 80;
            grfMedVisit.Cols[colVsDept].Width = 240;
            grfMedVisit.Cols[colVsPreno].Width = 100;
            grfMedVisit.Cols[colVsStatus].Width = 60;
            grfMedVisit.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfMedVisit.Cols[colVsVsDate].Caption = "Visit Date";
            grfMedVisit.Cols[colVsVn].Caption = "VN";
            grfMedVisit.Cols[colVsDept].Caption = "แผนก";
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
            this.Text = "Last Update 2020-08-26-1 แก้ online Medica, '_,-' innotech, auto print  bc.timerCheckLabOut " + bc.timerCheckLabOut+" status online "+bc.iniC.statusLabOutReceiveOnline+" status autoprint "+ bc.iniC.statusLabOutAutoPrint+" laboutmedicacode "+bc.iniC.laboutMedicahosp_code;
            if (bc.iniC.statusLabOutReceiveOnline.Equals("1"))
            {
                tC1.ShowTabs = true;
                tC1.SelectedTab = tabLabAuto;
            }
            else
            {
                //tabLabAuto.Hide();
                tC1.ShowTabs = false;
                if (bc.iniC.statusLabOutReceiveTabShow.Equals("2"))
                {
                    tC1.SelectedTab = tabManual;
                }
                else if (bc.iniC.statusLabOutReceiveTabShow.Equals("3"))
                {
                    tC1.SelectedTab = tabMed;
                }
                else if (bc.iniC.statusLabOutReceiveTabShow.Equals("ๅ"))
                {
                    tC1.SelectedTab = tabLabAuto;
                }
                labOutView.Ribbon.Minimized = true;
                medView.Ribbon.Minimized = true;
            }
        }
    }
}
