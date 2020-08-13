using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SplitContainer;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmDocGroupFmEdit:Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;

        private C1ThemeController theme1;
        C1FlexGrid grfImg, grfLeft;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1SplitterPanel scFmLeft;
        C1SplitterPanel scFmRight;        
        C1SplitContainer sCFm;

        Panel pnLeft, pnRight, pnRightTop, pnRightBotton;
        Label lbMlFmCode, lbMlFmCodeNew;
        C1TextBox txtMlFmCode, txtMlFmCodeNew;
        C1Button btnUpdate, btnFmCode;
        C1CheckBox chkLimit;

        int colLeftMlfmCode = 1, colLeftMlfmCnt = 2;
        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;

        List<listStream> lStream, lStreamPic;
        listStream strm;
        Form frmFlash;
        public FrmDocGroupFmEdit(BangnaControl bc)
        {
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            lStream = new List<listStream>();
            lStreamPic = new List<listStream>();
            strm = new listStream();

            InitComponent();
            setGrfLeft();
            this.Load += FrmDocGroupFmEdit_Load;
            btnUpdate.Click += BtnUpdate_Click;
            btnFmCode.Click += BtnFmCode_Click;
            txtMlFmCode.Enter += TxtMlFmCode_Enter;
            txtMlFmCodeNew.Enter += TxtMlFmCodeNew_Enter;
            txtMlFmCodeNew.EnabledChanged += TxtMlFmCodeNew_EnabledChanged;
        }

        private void TxtMlFmCodeNew_EnabledChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String[] txt = txtMlFmCodeNew.Text.Trim().Split('-');
            if (txt.Length >= 2)
            {
                String code = "";
                code = "FM" + txt[1] + "-" + txt[2];
            }
        }

        private void TxtMlFmCodeNew_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
        }

        private void TxtMlFmCode_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtMlFmCode.SelectAll();
            txtMlFmCode.Select(0, txtMlFmCode.Text.Length);
        }

        private void BtnFmCode_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (MessageBox.Show("ต้องการ บันทึกข้อมูล  ", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                saveFMCode();
            }
        }
        private void saveFMCode()
        {
            DocGroupFM fm = new DocGroupFM();
            DocGroupFM tmp = new DocGroupFM();
            tmp = bc.bcDB.dfmDB.selectByFMCode(txtMlFmCodeNew.Text.Trim());
            if (tmp.fm_id.Length <= 0)
            {
                fm.fm_id = "";
            }
            else
            {
                fm.fm_id = tmp.fm_id;
            }
            fm.fm_code = txtMlFmCodeNew.Text.Trim();
            fm.fm_name = "";
            fm.remark = "";
            fm.status_doc_adminsion = "0";
            fm.status_doc_medical = "0";
            fm.status_doc_nurse = "0";
            fm.status_doc_office = "0";
            fm.doc_group_sub_id = "";
            fm.doc_group_id = "";
            fm.active = "";
            bc.bcDB.dfmDB.insertDocGroupFMCode(fm, "");
        }
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if (MessageBox.Show("ต้องการ บันทึกข้อมูล \nFM code เดิม "+txtMlFmCode.Text+" Fm code ใหม่ "+txtMlFmCodeNew.Text, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
                String re = "";
                int chk = 0;
                re = bc.bcDB.dscDB.updateFmCodeByFmCodeLimit(txtMlFmCodeNew.Text.Trim(), txtMlFmCode.Text,"40");
                saveFMCode();
                setGrfLeft();
                grfImg.Rows.Count = 0;
                //txtMlFmCodeNew.Value = "";
            //}
        }

        private void setConponent()
        {
            int gapLine = 30, gapX = 20, gapY=20;
            Size size = new Size();

            initGrfLeft();
            initGrfImg();

            lbMlFmCode = new Label();
            lbMlFmCode.AutoSize = true;
            lbMlFmCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbMlFmCode.Font = fEdit;
            lbMlFmCode.ForeColor = System.Drawing.SystemColors.ControlText;
            lbMlFmCode.Location = new System.Drawing.Point(gapX, gapY);
            lbMlFmCode.Text = "Fm Code:";
            txtMlFmCode = new C1TextBox();
            txtMlFmCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtMlFmCode.Font = fEdit;
            txtMlFmCode.Width = 200;
            size = bc.MeasureString(lbMlFmCode);
            txtMlFmCode.Location = new System.Drawing.Point(lbMlFmCode.Location.X + size.Width + 15, lbMlFmCode.Location.Y);
            txtMlFmCode.Name = "txtMlFmCode";
            txtMlFmCode.ReadOnly = true;

            gapY += gapLine;
            lbMlFmCodeNew = new Label();
            lbMlFmCodeNew.AutoSize = true;
            lbMlFmCodeNew.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbMlFmCodeNew.Font = fEdit;
            lbMlFmCodeNew.ForeColor = System.Drawing.SystemColors.ControlText;
            lbMlFmCodeNew.Location = new System.Drawing.Point(gapX, gapY);
            lbMlFmCodeNew.Text = "Fm Code:";
            txtMlFmCodeNew = new C1TextBox();
            txtMlFmCodeNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtMlFmCodeNew.Width = 200;
            txtMlFmCodeNew.Font = fEdit;
            size = bc.MeasureString(lbMlFmCodeNew);
            txtMlFmCodeNew.Location = new System.Drawing.Point(lbMlFmCodeNew.Location.X + size.Width + 15, lbMlFmCodeNew.Location.Y);
            txtMlFmCodeNew.Name = "txtMlFmCodeNew";

            btnUpdate = new C1Button();
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Text = "Update Fm Code";
            btnUpdate.Font = this.fEdit;
            size = new Size(120, 40);
            btnUpdate.Size = size;
            btnUpdate.Location = new Point(txtMlFmCodeNew.Location.X + txtMlFmCodeNew.Width + 40, lbMlFmCodeNew.Location.Y);
            //btnPrn.Click += BtnPrn_Click;
            btnFmCode = new C1Button();
            btnFmCode.Name = "btnFmCode";
            btnFmCode.Text = "Fm Code";
            btnFmCode.Font = this.fEdit;
            size = new Size(120, 40);
            btnFmCode.Size = size;
            btnFmCode.Location = new Point(btnUpdate.Location.X + btnUpdate.Width + 40, lbMlFmCodeNew.Location.Y);

            gapY += gapLine;
            chkLimit = new C1CheckBox();
            chkLimit.Text = "Limit 40 ";
            chkLimit.Name = "chkLimit";
            chkLimit.Location = new Point(gapX, gapY);
            chkLimit.Checked = true;

            //pnRightTop.Controls.Add(btnUpdate);
            pnRightTop.Controls.Add(lbMlFmCode);
            pnRightTop.Controls.Add(txtMlFmCode);
            pnRightTop.Controls.Add(lbMlFmCodeNew);
            pnRightTop.Controls.Add(txtMlFmCodeNew);
            pnRightTop.Controls.Add(btnUpdate);
            pnRightTop.Controls.Add(chkLimit);
            pnRightTop.Controls.Add(btnFmCode);
            
        }
        private void initGrfLeft()
        {
            grfLeft = new C1FlexGrid();
            grfLeft.Font = fEdit;
            grfLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLeft.Location = new System.Drawing.Point(0, 0);

            grfLeft.Rows.Count = 1;
            grfLeft.Cols.Count = 8;
            grfLeft.Cols[colLeftMlfmCode].Caption = "FM Code";
            grfLeft.Cols[colLeftMlfmCnt].Caption = "Count";

            grfLeft.Cols[colLeftMlfmCode].Width = 400;
            grfLeft.Cols[colLeftMlfmCnt].Width = 200;
            grfLeft.Click += GrfLeft_Click;
            grfLeft.DoubleClick += GrfLeft_DoubleClick;

            grfLeft.Name = "grfLeft";
            pnLeft.Controls.Add(grfLeft);
        }
        private void GrfLeft_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfLeft.Row <= 0) return;
            if (grfLeft.Col <= 0) return;
            String mlfmcode = "", cnt = "";
            mlfmcode = grfLeft[grfLeft.Row, colLeftMlfmCode].ToString();
            cnt = grfLeft[grfLeft.Row, colLeftMlfmCnt].ToString();
            txtMlFmCode.Value = mlfmcode;
            txtMlFmCodeNew.Value = "";
        }
        private void GrfLeft_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfLeft.Row <= 0) return;
            if (grfLeft.Col <= 0) return;
            String mlfmcode = "", cnt = "";
            mlfmcode = grfLeft[grfLeft.Row, colLeftMlfmCode].ToString();
            cnt = grfLeft[grfLeft.Row, colLeftMlfmCnt].ToString();
            txtMlFmCode.Value = mlfmcode;
            txtMlFmCodeNew.Value = "";
            setGrfImg(mlfmcode);
        }
        private void setGrfImg(String fmcode)
        {
            showFormWaiting();
            DataTable dt = new DataTable();
            String limit = "";
            if (chkLimit.Checked)
            {
                dt = bc.bcDB.dscDB.selectByFmCode(fmcode,"40");
            }
            else
            {
                dt = bc.bcDB.dscDB.selectByFmCode(fmcode,"");
            }
            grfImg.Rows.Count = 0;

            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
            Boolean findTrue = false;
            int colcnt = 0, rowrun = -1;
            int cnt = 0;
            cnt = dt.Rows.Count / 2;
            ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_grfscan_print));
            menuGw.MenuItems.Add("ต้องการ Download ภาพนี้", new EventHandler(ContextMenu_grfImg_Download));
            grfImg.ContextMenu = menuGw;
            grfImg.Rows.Count = cnt + 1;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    if (findTrue) break;
                    colcnt++;
                    String dgssid = "", filename = "", ftphost = "", id = "", folderftp = "";
                    id = row1[bc.bcDB.dscDB.dsc.doc_scan_id].ToString();
                    dgssid = row1[bc.bcDB.dscDB.dsc.doc_group_sub_id].ToString();
                    filename = row1[bc.bcDB.dscDB.dsc.image_path].ToString();
                    ftphost = row1[bc.bcDB.dscDB.dsc.host_ftp].ToString();
                    folderftp = row1[bc.bcDB.dscDB.dsc.folder_ftp].ToString();

                    String err = "";
                    try
                    {
                        FtpWebRequest ftpRequest = null;
                        FtpWebResponse ftpResponse = null;
                        Stream ftpStream = null;
                        int bufferSize = 2048;
                        err = "00";
                        Row rowd;
                        if ((colcnt % 2) == 0)
                        {
                            rowd = grfImg.Rows[rowrun];
                        }
                        else
                        {
                            rowrun++;
                            rowd = grfImg.Rows[rowrun];
                        }
                        MemoryStream stream;
                        Image loadedImage, resizedImage;
                        stream = new MemoryStream();
                        //stream = ftp.download(folderftp + "//" + filename);

                        //loadedImage = Image.FromFile(filename);
                        err = "01";
                        Application.DoEvents();
                        ftpRequest = (FtpWebRequest)FtpWebRequest.Create(ftphost + "/" + folderftp + "/" + filename);
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
                            new LogWriter("e", "FrmScanView1 SetGrfScan try int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize); ex " + ex.Message + " " + err);
                        }
                        err = "03";

                        loadedImage = new Bitmap(stream);
                        err = "04";
                        int originalWidth = 0;
                        originalWidth = loadedImage.Width;
                        int newWidth = bc.imgScanWidth;
                        resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
                        //
                        err = "05";
                        if ((colcnt % 2) == 0)
                        {
                            rowd[colPic3] = resizedImage;       // + 0001
                            err = "061";       // + 0001
                            rowd[colPic4] = id;       // + 0001
                            err = "071";       // + 0001
                        }
                        else
                        {
                            err = "051";       // + 0001
                            rowd[colPic1] = resizedImage;       // + 0001
                            err = "06";       // + 0001
                            rowd[colPic2] = id;       // + 0001
                            err = "07";       // + 0001
                        }

                        strm = new listStream();
                        strm.id = id;
                        err = "08";
                        strm.stream = stream;
                        err = "09";
                        lStream.Add(strm);

                        Application.DoEvents();
                        err = "12";

                        if (colcnt == 50) GC.Collect();
                        if (colcnt == 100) GC.Collect();
                    }
                    catch (Exception ex)
                    {
                        String aaa = ex.Message + " " + err;
                        new LogWriter("e", "FrmDocGroupFmEdit setGrfImg ex " + ex.Message + " " + err + " colcnt " + colcnt);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            frmFlash.Dispose();
            grfImg.AutoSizeRows();
        }
        private void initGrfImg()
        {
            grfImg = new C1FlexGrid();
            grfImg.Font = fEdit;
            grfImg.Dock = System.Windows.Forms.DockStyle.Fill;
            grfImg.Location = new System.Drawing.Point(0, 0);
            grfImg.Rows[0].Visible = false;
            grfImg.Cols[0].Visible = false;
            grfImg.Rows.Count = 1;
            grfImg.Name = "grfImg";
            grfImg.Cols.Count = 5;
            Column colpic1 = grfImg.Cols[colPic1];
            colpic1.DataType = typeof(Image);
            Column colpic2 = grfImg.Cols[colPic2];
            colpic2.DataType = typeof(String);
            Column colpic3 = grfImg.Cols[colPic3];
            colpic3.DataType = typeof(Image);
            Column colpic4 = grfImg.Cols[colPic4];
            colpic4.DataType = typeof(String);
            grfImg.Cols[colPic1].Width = bc.grfImgWidth;
            grfImg.Cols[colPic2].Width = bc.grfImgWidth;
            grfImg.Cols[colPic3].Width = bc.grfImgWidth;
            grfImg.Cols[colPic4].Width = bc.grfImgWidth;
            grfImg.ShowCursor = true;
            grfImg.Cols[colPic2].Visible = false;
            grfImg.Cols[colPic3].Visible = true;
            grfImg.Cols[colPic4].Visible = false;
            grfImg.Cols[colPic1].AllowEditing = false;
            grfImg.Cols[colPic3].AllowEditing = false;

            grfImg.Name = "grfImg";
            pnRightBotton.Controls.Add(grfImg);
        }
        private void ContextMenu_grfImg_Download(object sender, System.EventArgs e)
        {
            String id = "", datetick = "", dsc_id="";
            if (grfImg.Col <= 0) return;
            if (grfImg.Row < 0) return;
            if (grfImg.Col == 1)
            {
                id = grfImg[grfImg.Row, colPic2].ToString();
            }
            else
            {
                id = grfImg[grfImg.Row, colPic4].ToString();
            }
            dsc_id = id;
            DocScan dsc = new DocScan();
            dsc = bc.bcDB.dscDB.selectByPk(dsc_id);
            Stream streamDownload = null;
            MemoryStream strm = null;
            foreach (listStream lstrmm in lStream)
            {
                if (lstrmm.id.Equals(id))
                {
                    strm = lstrmm.stream;
                    streamDownload = lstrmm.stream;
                    break;
                }
            }
            if (!Directory.Exists(bc.iniC.pathDownloadFile))
            {
                Directory.CreateDirectory(bc.iniC.pathDownloadFile);
            }
            datetick = DateTime.Now.Ticks.ToString();
            Image img = Image.FromStream(streamDownload);
            img.Save(bc.iniC.pathDownloadFile + "\\" + dsc.hn + "_" + datetick + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            bc.ExploreFile(bc.iniC.pathDownloadFile + "\\" + dsc.hn + "_" + datetick + ".jpg");
        }
        private void InitComponent()
        {
            sCFm = new C1SplitContainer();
            scFmLeft = new C1SplitterPanel();
            scFmRight = new C1SplitterPanel();
            pnLeft = new Panel();
            pnRight = new Panel();
            pnRightTop = new Panel();
            pnRightBotton = new Panel();

            sCFm.SuspendLayout();
            scFmLeft.SuspendLayout();
            scFmRight.SuspendLayout();
            pnLeft.SuspendLayout();
            pnRight.SuspendLayout();
            pnRightTop.SuspendLayout();
            pnRightBotton.SuspendLayout();

            pnLeft.Dock = DockStyle.Fill;
            pnRight.Dock = DockStyle.Fill;
            pnRightTop.Dock = DockStyle.Top;
            pnRightBotton.Dock = DockStyle.Fill;
            //pnRightTop.BackColor = Color.Red;

            sCFm.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCFm.Name = "sCFm";
            sCFm.Dock = System.Windows.Forms.DockStyle.Fill;
            //sCOrdDiag.Panels.Add(pnOrdDiagVal);
            sCFm.Panels.Add(scFmLeft);
            sCFm.Panels.Add(scFmRight);
            
            sCFm.HeaderHeight = 20;
            scFmLeft.Collapsible = true;
            scFmLeft.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            scFmLeft.Location = new System.Drawing.Point(0, 21);
            scFmLeft.Name = "scFmLeft";
            scFmRight.Collapsible = true;
            scFmRight.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            scFmRight.Location = new System.Drawing.Point(0, 21);
            scFmRight.Name = "scFmRight";

            scFmLeft.Controls.Add(pnLeft);
            scFmRight.Controls.Add(pnRight);
            pnRight.Controls.Add(pnRightBotton);
            pnRight.Controls.Add(pnRightTop);

            setConponent();

            sCFm.ResumeLayout(false);
            scFmLeft.ResumeLayout(false);
            scFmRight.ResumeLayout(false);
            pnLeft.ResumeLayout(false);
            pnRight.ResumeLayout(false);
            pnRightTop.ResumeLayout(false);
            pnRightBotton.ResumeLayout(false);

            pnRightTop.PerformLayout();
            pnRightBotton.PerformLayout();
            pnLeft.PerformLayout();
            pnRight.PerformLayout();
            sCFm.PerformLayout();
            scFmLeft.PerformLayout();
            scFmRight.PerformLayout();

            this.Controls.Add(sCFm);
        }
        private void setGrfLeft()
        {
            DataTable dt = new DataTable();
            grfLeft.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfLeft.Cols.Count = 3;
            grfLeft.Cols[colLeftMlfmCode].Caption = "FM Code";
            grfLeft.Cols[colLeftMlfmCnt].Caption = "Count";

            grfLeft.Cols[colLeftMlfmCode].Width = 220;
            grfLeft.Cols[colLeftMlfmCnt].Width = 60;

            grfLeft.ShowCursor = true;
            if (chkLimit.Checked)
            {
                dt = bc.bcDB.dscDB.selectGroupByMlFM("");
            }
            else
            {
                dt = bc.bcDB.dscDB.selectGroupByMlFM("");
            }
            int i = 1;
            grfLeft.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                grfLeft[i, 0] = (i);
                grfLeft[i, colLeftMlfmCode] = row["ml_fm"].ToString();
                grfLeft[i, colLeftMlfmCnt] = row["cnt"].ToString();
                i++;
            }
            //grfLeft.Cols[colHISEdit].Visible = false;
            grfLeft.Cols[colLeftMlfmCode].AllowEditing = false;
            grfLeft.Cols[colLeftMlfmCnt].AllowEditing = false;
        }
        private void showFormWaiting()
        {
            frmFlash = new Form();
            frmFlash.Size = new Size(300, 300);
            frmFlash.StartPosition = FormStartPosition.CenterScreen;
            C1PictureBox picFlash = new C1PictureBox();
            //Image img = new Image();
            picFlash.SuspendLayout();
            picFlash.Image = Resources.loading_transparent;
            picFlash.Width = 230;
            picFlash.Height = 230;
            picFlash.Location = new Point(30, 10);
            picFlash.SizeMode = PictureBoxSizeMode.StretchImage;
            frmFlash.Controls.Add(picFlash);
            picFlash.ResumeLayout();
            frmFlash.Show();
            Application.DoEvents();
        }
        class listStream
        {
            public String id = "";
            public MemoryStream stream;
        }
        private void FrmDocGroupFmEdit_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            scFmLeft.SizeRatio = 20;
            theme1 = new C1.Win.C1Themes.C1ThemeController();
            theme1.SetTheme(grfLeft, bc.iniC.themeApplication);
            theme1.SetTheme(sCFm, bc.iniC.themeApplication);
            theme1.SetTheme(pnLeft, bc.iniC.themeApplication);
            theme1.SetTheme(pnRight, bc.iniC.themeApplication);
            theme1.SetTheme(pnRightTop, bc.iniC.themeApp);
            theme1.SetTheme(btnUpdate, bc.iniC.themeApp);
            theme1.SetTheme(btnFmCode, bc.iniC.themeApp);
            theme1.SetTheme(chkLimit, bc.iniC.themeApp);
            foreach (Control c in pnRightTop.Controls)
            {
                theme1.SetTheme(c, bc.iniC.themeApp);
            }
        }
    }
}
