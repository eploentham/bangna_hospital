using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmWard : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Patient PTT;
        Visit VS;
        C1ThemeController theme1;
        C1FlexGrid grfOperList, grfOutLab, grfIPD, grfIPDScan, grfSrc;
        Image imgCorr, imgTran;
        Timer timeOperList;
        Boolean pageLoad = false;
        Color colorLbDoc;
        listStream strm;
        List<listStream> lStream, lStreamPic;
        Label lbLoading;
        Label lbDocAll, lbDocGrp1, lbDocGrp2, lbDocGrp3, lbDocGrp4, lbDocGrp5, lbDocGrp6, lbDocGrp7, lbDocGrp8, lbDocGrp9;
        String PRENO = "", VSDATE = "", HN = "", DEPTNO = "", HNmedscan = "", DSCID = "", OUTLAB = "";
        int colgrfOutLabDscHN = 1, colgrfOutLabDscPttName = 2, colgrfOutLabDscVsDate = 3, colgrfOutLabDscVN = 4, colgrfOutLabDscId = 5, colgrfOutLablabcode = 6, colgrfOutLablabname = 7, colgrfOutLabApmDate = 8, colgrfOutLabApmDesc = 9, colgrfOutLabApmDtr = 10, colgrfOutLabApmReqNo = 11, colgrfOutLabApmReqDate = 12;
        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
        int colgrfSrcHn = 1, colgrfSrcFullNameT = 2, colgrfSrcPID = 3, colgrfSrcDOB = 4, colgrfSrcPttid = 5, colgrfSrcAge = 6, colgrfSrcVisitReleaseOPD = 7, colgrfSrcVisitReleaseIPD = 8, colgrfSrcVisitReleaseIPDDischarge = 9;
        int colIPDDate = 1, colIPDDept = 2, colIPDAnShow = 4, colIPDStatus = 3, colIPDPreno = 5, colIPDVn = 6, colIPDAndate = 7, colIPDAnYr = 8, colIPDAn = 9, colIPDDtrName = 10;
        public FrmWard(BangnaControl bc, String outlab)
        {
            this.bc = bc;
            this.OUTLAB = outlab;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            initFont();
            theme1 = new C1ThemeController();
            imgCorr = Resources.red_checkmark_png_16;
            imgTran = Resources.DeleteTable_small;
            timeOperList = new Timer();
            timeOperList.Interval = 30000;
            timeOperList.Enabled = false;
            PTT = new Patient();
            VS = new Visit();
            lStream = new List<listStream>();
            initControl();
            if (OUTLAB.Equals("outlab"))
            {
                TabNew.TabVisible = false;
                tabPttinWard.TabVisible = false;

            }
            btnSBSearch.Click += BtnSBSearch_Click;
            pageLoad = false;
        }

        private void BtnSBSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if (tC1Active.Equals(tabMedScan.Name))
            //{
            //    HNmedscan = txtSBSearchHN.Text.Trim();
            //    Patient ptt = bc.bcDB.pttDB.selectPatinetByHn(txtSBSearchHN.Text);
            //    rb1.Text = ptt.Name;
            //    if (tabMedScanActiveNOtabOutLabActive)
            //    {
            //        tC1.SelectedTab = tabMedScan;
            //        setGrfIPD();
            //    }
            //    else
            //    {
            //        tC1.SelectedTab = tabOutlab;
            //        fvCerti.DocumentSource = null;
            //        setGrfOutLab();
            //    }
            //}
            //else if (tC1Active.Equals(tabOutlab.Name))
            //{
            //    setGrfOutLab();
            //}
        }

        private void initFont()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            famt1 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 1, FontStyle.Regular);
            famt5 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
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
        private void initControl()
        {
            initLoading();
            initGrfOutLab();
            initGrfIPD();
            initGrfIPDScan();
            initGrfSrc();
        }
        private void initGrfOutLab()
        {
            grfOutLab = new C1FlexGrid();
            grfOutLab.Font = fEdit;
            grfOutLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOutLab.Location = new System.Drawing.Point(0, 0);
            grfOutLab.Rows.Count = 1;
            grfOutLab.Cols.Count = 8;

            grfOutLab.Cols[colgrfOutLabDscHN].Width = 80;
            grfOutLab.Cols[colgrfOutLabDscPttName].Width = 250;
            grfOutLab.Cols[colgrfOutLabDscVsDate].Width = 100;
            grfOutLab.Cols[colgrfOutLabDscVN].Width = 80;
            grfOutLab.Cols[colgrfOutLablabcode].Width = 80;
            grfOutLab.Cols[colgrfOutLablabname].Width = 250;

            grfOutLab.Cols[colgrfOutLabDscHN].Caption = "HN";
            grfOutLab.Cols[colgrfOutLabDscPttName].Caption = "Name";
            grfOutLab.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
            grfOutLab.Cols[colgrfOutLabDscVN].Caption = "VN";
            grfOutLab.Cols[colgrfOutLablabcode].Caption = "code";
            grfOutLab.Cols[colgrfOutLablabname].Caption = "lab name";

            grfOutLab.Cols[colgrfOutLabDscId].Visible = false;
            grfOutLab.Cols[colgrfOutLabDscHN].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLabDscPttName].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLabDscVN].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLablabcode].AllowEditing = false;
            grfOutLab.Cols[colgrfOutLablabname].AllowEditing = false;
            grfOutLab.AfterRowColChange += GrfOutLab_AfterRowColChange;

            spOutLabList.Controls.Add(grfOutLab);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfOutLab, bc.iniC.themegrfOpd);
        }
        private void initGrfIPD()
        {
            grfIPD = new C1FlexGrid();
            grfIPD.Font = fEdit;
            grfIPD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfIPD.Location = new System.Drawing.Point(0, 0);
            grfIPD.Rows.Count = 1;
            grfIPD.Cols.Count = 11;

            grfIPD.Cols[colIPDDate].Width = 90;
            grfIPD.Cols[colIPDVn].Width = 80;
            grfIPD.Cols[colIPDDept].Width = 170;
            grfIPD.Cols[colIPDPreno].Width = 100;
            grfIPD.Cols[colIPDStatus].Width = 60;
            grfIPD.Cols[colIPDDtrName].Width = 180;
            grfIPD.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfIPD.Cols[colIPDDate].Caption = "Visit Date";
            grfIPD.Cols[colIPDVn].Caption = "VN";
            grfIPD.Cols[colIPDDept].Caption = "อาการ";
            grfIPD.Cols[colIPDAnShow].Caption = "AN";

            grfIPD.Cols[colIPDPreno].Visible = false;
            grfIPD.Cols[colIPDVn].Visible = true;
            grfIPD.Cols[colIPDAnShow].Visible = true;
            grfIPD.Cols[colIPDAndate].Visible = false;
            grfIPD.Cols[colIPDPreno].Visible = false;
            grfIPD.Cols[colIPDVn].Visible = false;
            grfIPD.Cols[colIPDStatus].Visible = false;
            grfIPD.Cols[colIPDAnYr].Visible = false;
            grfIPD.Cols[colIPDAn].Visible = false;
            //grfIPD.Rows[0].Visible = false;
            //grfIPD.Cols[0].Visible = false;
            grfIPD.Cols[colIPDDate].AllowEditing = false;
            grfIPD.Cols[colIPDVn].AllowEditing = false;
            grfIPD.Cols[colIPDDept].AllowEditing = false;
            grfIPD.Cols[colIPDPreno].AllowEditing = false;
            grfIPD.Cols[colIPDDtrName].AllowEditing = false;
            //FilterRow fr = new FilterRow(grfExpn);

            grfIPD.AfterRowColChange += GrfIPD_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            spMedScanIPD.Controls.Add(grfIPD);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfIPD, bc.iniC.themegrfIpd);
        }

        private void GrfIPD_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;

            String an = "", vsDate = "", preno = "";

            an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";
            vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
            preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";

            setGrfIPDScan();
            lfSbMessage.Text = an;
        }
        private void setGrfIPDScan()
        {
            //Application.DoEvents();
            //ProgressBar pB1 = new ProgressBar();
            //pB1.Location = new System.Drawing.Point(20, 16);
            //pB1.Name = "pB1";
            //pB1.Size = new System.Drawing.Size(862, 23);
            //gbPtt.Controls.Add(pB1);
            //pB1.Left = txtHn.Left;
            //pB1.Show();
            showLbLoading();
            lStream.Clear();
            //clearGrf();
            String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "", anyr = "", vn1 = "";
            DataTable dtOrder = new DataTable();

            //new LogWriter("e", "FrmScanView1 setGrfScan 5 ");
            GC.Collect();


            DataTable dt = new DataTable();
            statusOPD = grfIPD[grfIPD.Row, colIPDStatus] != null ? grfIPD[grfIPD.Row, colIPDStatus].ToString() : "";
            preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
            vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";

            an = grfIPD[grfIPD.Row, colIPDAn] != null ? grfIPD[grfIPD.Row, colIPDAn].ToString() : "";
            anDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
            anyr = grfIPD[grfIPD.Row, colIPDAnYr] != null ? grfIPD[grfIPD.Row, colIPDAnYr].ToString() : "";
            //label2.Text = "AN :";
            an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";

            vsDate = bc.datetoDB(vsDate);
            //setStaffNote(vsDate, preno);
            dt = bc.bcDB.dscDB.selectByAn(txtSBSearchHN.Text, an);
            grfIPDScan.Rows.Count = 0;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    int cnt = 0;
                    cnt = dt.Rows.Count / 2;

                    grfIPDScan.Rows.Count = cnt + 1;

                    FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                    Boolean findTrue = false;
                    int colcnt = 0, rowrun = -1;
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

                        //new Thread(() =>
                        //{
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
                                rowd = grfIPDScan.Rows[rowrun];
                            }
                            else
                            {
                                rowrun++;
                                rowd = grfIPDScan.Rows[rowrun];
                                Application.DoEvents();
                            }
                            MemoryStream stream;
                            Image loadedImage, resizedImage;
                            stream = new MemoryStream();
                            //stream = ftp.download(folderftp + "//" + filename);

                            //loadedImage = Image.FromFile(filename);
                            err = "01";

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
                            strm.dgsid = row1[bc.bcDB.dscDB.dsc.doc_group_id].ToString();
                            err = "08";
                            strm.stream = stream;
                            err = "09";
                            lStream.Add(strm);

                            err = "12";

                            if (colcnt == 50) GC.Collect();
                            if (colcnt == 100) GC.Collect();
                        }
                        catch (Exception ex)
                        {
                            String aaa = ex.Message + " " + err;
                            new LogWriter("e", "FrmScanView1 SetGrfScan ex " + ex.Message + " " + err + " colcnt " + colcnt + " doc_scan_id " + id);
                        }

                    }
                    ftp = null;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("" + ex.Message, "");
                    new LogWriter("e", "FrmScanView1 SetGrfScan if (dt.Rows.Count > 0) ex " + ex.Message);
                }
            }
            grfIPDScan.AutoSizeCols();
            grfIPDScan.AutoSizeRows();
            hideLbLoading();
        }
        private void initGrfIPDScan()
        {
            Panel pnScanTop = new Panel();
            Panel pnScan = new Panel();

            pnScanTop.Dock = DockStyle.Top;
            pnScanTop.Height = 30;
            pnScan.Dock = DockStyle.Fill;

            grfIPDScan = new C1FlexGrid();
            grfIPDScan.Font = fEdit;
            grfIPDScan.Dock = System.Windows.Forms.DockStyle.Fill;
            grfIPDScan.Location = new System.Drawing.Point(0, 0);
            grfIPDScan.Rows[0].Visible = false;
            grfIPDScan.Cols[0].Visible = false;
            grfIPDScan.Rows.Count = 1;
            grfIPDScan.Name = "grfIPDScan";
            grfIPDScan.Cols.Count = 5;
            Column colpic1 = grfIPDScan.Cols[colPic1];
            colpic1.DataType = typeof(Image);
            Column colpic2 = grfIPDScan.Cols[colPic2];
            colpic2.DataType = typeof(String);
            Column colpic3 = grfIPDScan.Cols[colPic3];
            colpic3.DataType = typeof(Image);
            Column colpic4 = grfIPDScan.Cols[colPic4];
            colpic4.DataType = typeof(String);
            grfIPDScan.Cols[colPic1].Width = bc.grfScanWidth;
            grfIPDScan.Cols[colPic2].Width = bc.grfScanWidth;
            grfIPDScan.Cols[colPic3].Width = bc.grfScanWidth;
            grfIPDScan.Cols[colPic4].Width = bc.grfScanWidth;
            grfIPDScan.ShowCursor = true;
            grfIPDScan.Cols[colPic2].Visible = false;
            grfIPDScan.Cols[colPic3].Visible = true;
            grfIPDScan.Cols[colPic4].Visible = false;
            grfIPDScan.Cols[colPic1].AllowEditing = false;
            grfIPDScan.Cols[colPic3].AllowEditing = false;
            grfIPDScan.DoubleClick += GrfIPDScan_DoubleClick;
            lbDocAll = new Label();
            bc.setControlLabel(ref lbDocAll, fEditB, "All", "lbDocAll", 20, 5);
            lbDocAll.ForeColor = Color.Red;
            lbDocAll.Click += LbDocAll_Click;
            pnScanTop.Controls.Add(lbDocAll);
            int i = 0, width1 = 0;
            colorLbDoc = lbDocAll.ForeColor;
            if (bc.bcDB.dgsDB.lDgs.Count <= 0) bc.bcDB.dgsDB.getlDgs();
            foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            {
                i++;
                if (i == 1)
                {
                    lbDocGrp1 = new Label();
                    bc.setControlLabel(ref lbDocGrp1, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp1.Click += LbDocAll_Click;
                    lbDocGrp1.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp1);
                }
                else if (i == 2)
                {
                    lbDocGrp2 = new Label();
                    bc.setControlLabel(ref lbDocGrp2, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp2.Click += LbDocAll_Click;
                    lbDocGrp2.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp2);
                }
                else if (i == 3)
                {
                    lbDocGrp3 = new Label();
                    bc.setControlLabel(ref lbDocGrp3, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp3.Click += LbDocAll_Click;
                    lbDocGrp3.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp3);
                }
                else if (i == 4)
                {
                    lbDocGrp4 = new Label();
                    bc.setControlLabel(ref lbDocGrp4, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp4.Click += LbDocAll_Click;
                    lbDocGrp4.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp4);
                }
                else if (i == 5)
                {
                    lbDocGrp5 = new Label();
                    bc.setControlLabel(ref lbDocGrp5, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp5.Click += LbDocAll_Click;
                    lbDocGrp5.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp5);
                }
                else if (i == 6)
                {
                    lbDocGrp6 = new Label();
                    bc.setControlLabel(ref lbDocGrp6, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp6.Click += LbDocAll_Click;
                    lbDocGrp6.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp6);
                }
                else if (i == 7)
                {
                    lbDocGrp7 = new Label();
                    bc.setControlLabel(ref lbDocGrp7, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp7.Click += LbDocAll_Click;
                    lbDocGrp7.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp7);
                }
                else if (i == 8)
                {
                    lbDocGrp8 = new Label();
                    bc.setControlLabel(ref lbDocGrp8, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp8.Click += LbDocAll_Click;
                    lbDocGrp8.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp8);
                }
                else if (i == 9)
                {
                    lbDocGrp9 = new Label();
                    bc.setControlLabel(ref lbDocGrp9, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
                    width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
                    if ((i == 1) || (i == 2) || (i == 7))
                    {
                        width1 += 40;
                    }
                    if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
                    {
                        width1 += 20;
                    }
                    lbDocGrp9.Click += LbDocAll_Click;
                    lbDocGrp9.ForeColor = Color.Black;
                    pnScanTop.Controls.Add(lbDocGrp9);
                }
            }
            pnMedScan.Controls.Add(pnScan);
            pnMedScan.Controls.Add(pnScanTop);
            pnScan.Controls.Add(grfIPDScan);
            //initGrfPrn();
            //initGrfHn();
        }
        private void initGrfSrc()
        {//tab ค้นหา
            grfSrc = new C1FlexGrid();
            grfSrc.Font = fEdit;
            grfSrc.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSrc.Location = new System.Drawing.Point(0, 0);
            grfSrc.Rows.Count = 1;
            grfSrc.Cols.Count = 10;
            grfSrc.Cols[colgrfSrcHn].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcFullNameT].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcPID].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcDOB].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcHn].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcFullNameT].TextAlign = TextAlignEnum.LeftCenter;
            grfSrc.Cols[colgrfSrcPID].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcDOB].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcAge].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcVisitReleaseIPD].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcHn].Width = 100;
            grfSrc.Cols[colgrfSrcFullNameT].Width = 300;
            grfSrc.Cols[colgrfSrcPID].Width = 150;
            grfSrc.Cols[colgrfSrcDOB].Width = 90;
            grfSrc.Cols[colgrfSrcPttid].Width = 60;
            grfSrc.Cols[colgrfSrcAge].Width = 90;
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].Width = 90;
            grfSrc.Cols[colgrfSrcVisitReleaseIPD].Width = 90;
            grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].Width = 90;
            grfSrc.ShowCursor = true;
            grfSrc.Cols[colgrfSrcHn].Caption = "hn";
            grfSrc.Cols[colgrfSrcFullNameT].Caption = "full name";
            grfSrc.Cols[colgrfSrcPID].Caption = "PID";
            grfSrc.Cols[colgrfSrcDOB].Caption = "DOB";
            grfSrc.Cols[colgrfSrcPttid].Caption = "";
            grfSrc.Cols[colgrfSrcAge].Caption = "AGE";
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].Caption = "มาล่าสุดOPD";
            grfSrc.Cols[colgrfSrcVisitReleaseIPD].Caption = "มาล่าสุดIPD";
            grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].Caption = "กลับบ้านIPD";

            grfSrc.Cols[colgrfSrcHn].Visible = true;
            grfSrc.Cols[colgrfSrcFullNameT].Visible = true;
            grfSrc.Cols[colgrfSrcPID].Visible = true;
            grfSrc.Cols[colgrfSrcDOB].Visible = true;
            grfSrc.Cols[colgrfSrcPttid].Visible = false;
            grfSrc.Cols[colgrfSrcAge].Visible = true;

            grfSrc.Cols[colgrfSrcHn].AllowEditing = false;
            grfSrc.Cols[colgrfSrcFullNameT].AllowEditing = false;
            grfSrc.Cols[colgrfSrcPID].AllowEditing = false;
            grfSrc.Cols[colgrfSrcDOB].AllowEditing = false;
            grfSrc.Cols[colgrfSrcAge].AllowEditing = false;
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].AllowEditing = false;
            grfSrc.Cols[colgrfSrcVisitReleaseIPD].AllowEditing = false;
            grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].AllowEditing = false;
            grfSrc.AllowFiltering = true;

            grfSrc.Click += GrfSrc_Click;
            //grfSrc.AutoSizeCols();
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnSrcGrf.Controls.Add(grfSrc);
            theme1.SetTheme(grfSrc, bc.iniC.themeApp);
        }

        private void GrfSrc_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void LbDocAll_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void GrfIPDScan_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void GrfOutLab_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();

        }
        class listStream
        {
            public String id = "", dgsid = "";
            public MemoryStream stream;
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
        private void FrmWard_Load(object sender, EventArgs e)
        {

        }
    }
}
