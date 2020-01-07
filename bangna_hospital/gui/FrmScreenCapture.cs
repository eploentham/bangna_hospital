using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1List;
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
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmScreenCapture : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;
        C1PictureBox picScr;
        C1FlexGrid grfView, grfDownload;
        C1List listView;
        private System.IO.FileSystemWatcher m_Watcher;
        List<String> lFile, lFilePrint;
        Patient ptt;
        MemoryStream streamPrint = null;

        int colUploadId = 1, colUploadName = 2, colUploadImg = 3, colUploadPath = 4;
        int cntPrint = 0, formwidth=0;
        Panel pnMain;
        Panel pnTop = new Panel();
        Panel pnBotom = new Panel();
        C1PictureBox picTop, picLeft, picRight;
        Form frmImg;
        //private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Panel pnBotton;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.Panel pnLeft;
        int screenWidth = 0;
        int screenHeight = 0;

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
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            pnMain = new Panel();
            picTop = new C1PictureBox();
            picLeft = new C1PictureBox();
            picRight = new C1PictureBox();
            screenWidth = Screen.PrimaryScreen.Bounds.Width;
            screenHeight = Screen.PrimaryScreen.Bounds.Height;

            lFile = new List<string>();
            lFilePrint = new List<string>();

            picScr = new C1PictureBox();
            picScr.Location = new System.Drawing.Point(0, 0);
            picScr.Dock = DockStyle.Fill;
            picScr.Name = "picScr";
            //picScr.Size = new System.Drawing.Size(screenWidth / 2, screenHeight);
            //picScr.Image = Resources.screen_first_l;
            picScr.SizeMode = PictureBoxSizeMode.StretchImage;
            pnPic.Controls.Add(picScr);
            
            this.Activated += FrmScreenCapture_Activated;
            this.FormClosed += FrmScreenCapture_FormClosed;
            txtHn.KeyUp += TxtHn_KeyUp;
            chkView.Click += ChkView_Click;
            chkUpload.Click += ChkUpload_Click;

            chkUpload.Checked = true;
            initGrfView();
            //FrmScreenCapturePrintMulti frm = new FrmScreenCapturePrintMulti();
            //frm.Show(this);
            initPrintMulti();

            if (File.Exists(bc.hn))
            {
                getListFile();
            }
            //MessageBox.Show("args "+bc.hn, "");
            //pnMain.Hide();
            //this.Width = this.Width - int.Parse(bc.iniC.imggridscanwidth);
            //this.Width = formwidth + int.Parse(bc.iniC.imggridscanwidth);
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
            initGrfDownload();
            setGrfUpload();
        }

        private void TxtHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatinet(txtHn.Text.Trim());
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

                System.Drawing.Image img = Image.FromStream(streamPrint);

                float newWidth = img.Width * 100 / img.HorizontalResolution;
                float newHeight = img.Height * 100 / img.VerticalResolution;

                float widthFactor = newWidth / e.MarginBounds.Width;
                float heightFactor = newHeight / e.MarginBounds.Height;

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
                e.Graphics.DrawImage(img, 0, 0, (int)newWidth, (int)newHeight);
                //}
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
            grfView = new C1FlexGrid();
            grfView.Font = fEdit;
            grfView.Dock = System.Windows.Forms.DockStyle.Fill;
            grfView.Location = new System.Drawing.Point(0, 0);
            
            grfView.Rows[0].Visible = false;
            grfView.Cols[0].Visible = false;
            pnView.Controls.Add(grfView);

            grfView.Rows.Count = 1;
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
                FrmScreenCaptureUpload frm = new FrmScreenCaptureUpload(bc, filename, txtHn.Text.Trim(), lbName.Text);
                frm.ShowDialog(this);
                getListFile();
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
                if (File.Exists(bc.hn))
                {
                    //path = bc.hn;
                    path = Path.GetDirectoryName(bc.hn);
                }
                else
                {
                    path = bc.iniC.pathScreenCaptureUpload;
                }
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
            }
            setListView();
        }
        private void setListView()
        {
            if (grfView.IsDisposed) return;
            if (grfView.Rows == null) return;
            grfView.Rows.Count = 0;
            Column colpic1 = grfView.Cols[colUploadImg];
            colpic1.DataType = typeof(Image);

            foreach (String file in lFile)
            {
                Row row = grfView.Rows.Add();
                row[colUploadPath] = file;
                string ext = Path.GetExtension(file);
                Image loadedImage, resizedImage;
                if (ext.ToLower().IndexOf("pdf") < 0)
                {
                    loadedImage = Image.FromFile(file);
                    int originalWidth = 0;
                    originalWidth = loadedImage.Width;
                    int newWidth = 280;
                    newWidth = bc.imggridscanwidth;
                    resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
                    row[colUploadImg] = resizedImage;
                    loadedImage.Dispose();
                }
                else
                {
                    row[colUploadImg] = Resources.pdf_symbol_300;
                }
            }
            grfView.AutoSizeCols();
            grfView.AutoSizeRows();
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
            frmImg.Location = new System.Drawing.Point(this.Location.X + this.Width + 20, this.Top);
            this.Text = "Last Update 2019-12-24 ProxyProxyType " + bc.iniC.ProxyProxyType;
        }
    }
}
