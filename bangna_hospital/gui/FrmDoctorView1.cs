using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmDoctorView1:Form
    {
        BangnaControl bc;
        MainMenu menu;
        Login login;

        private Point _imageLocation = new Point(13, 5);
        private Point _imgHitArea = new Point(13, 2);
        Image CloseImage;
        C1FlexGrid grfQue, grfApm, grfFin, grfIPD;
        private C1.Win.C1Ribbon.C1StatusBar sb1;
        private C1.Win.C1Themes.C1ThemeController theme1;
        C1DockingTab tC1;
        C1DockingTabPage tabQue, tabApm, tabFinish, tabIPD;
        Label lbDtrName, lbTxtPttHn, lbTxtDate, lbPttName;
        C1TextBox txtPttHn;
        C1Button btnHnSearch;
        C1DateEdit txtDate;

        Panel panel1, pnHead, pnBotton, pnQue, pnApm, pnFinish, pnIPD;

        int colQueId = 1, colQueVnShow = 2, colQueHn = 3, colQuePttName = 4, colQueVsDate = 5, colQueVsTime = 6, colQueSex = 7, colQueAge = 8, colQuePaid = 9, colQueSymptom = 10, colQueHeight = 11, coolQueBw = 12, colQueBp = 13, colQuePulse = 14, colQyeTemp = 15, colQuePreNo = 16, colQueDsc = 17;
        int colApmId = 1, colApmVnShow = 2, colApmHn = 3, colApmPttName = 4, colApmDate = 5, colApmTime = 6, colApmSex = 7, colApmAge = 8, colApmDsc = 9, colApmRemark = 10, colApmDept = 11;
        int colFinId = 1, colFinVnShow = 2, colFinHn = 3, colFinPttName = 4, colFinDate = 5, colFinTime = 6, colFinSex = 7, colFinAge = 8, colFinDsc = 9, colFinRemark = 10, colFinDept = 11, colFinWrd=12, colFinAn=13;
        int colIPDId = 1, colIPDVnShow = 2, colIPDHn = 3, colIPDAn = 4, colIPDPttName = 5, colIPDDate = 6, colIPDTime = 7, colIPDSex = 8, colIPDAge = 9, colIPDDsc = 10, colIPDRemark = 11, colIPDDept = 12, colIPDWrd = 13;

        Boolean flagExit = false;
        Font fEdit, fEditB, fEditBig;
        System.Windows.Forms.Timer timer1;
        Form frmFlash;

        public FrmDoctorView1(BangnaControl bc, FrmSplash splash)
        {
            this.bc = bc;
            login = new Login(bc, splash);
            login.ShowDialog(this);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                bc.bcDB = new objdb.BangnaHospitalDB(bc.conn);
                bc.getInit();
            }).Start();
            splash.Dispose();
            if (login.LogonSuccessful.Equals("1"))
            {
                initConfig();
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    /* run your code here */

                }).Start();
            }
            else
            {
                Application.Exit();
            }
        }
        private void initConfig()
        {
            initComponent();
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize+3, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize+7, FontStyle.Regular);

            timer1 = new System.Windows.Forms.Timer();
            timer1.Enabled = true;
            timer1.Interval = bc.timerCheckLabOut * 1000;
            timer1.Tick += Timer1_Tick;
            timer1.Stop();

            this.Load += FrmDoctorView1_Load;
            txtPttHn.KeyUp += TxtPttHn_KeyUp;
            txtDate.DropDownClosed += TxtDate_DropDownClosed;
            tC1.Click += TC1_Click;
            btnHnSearch.Click += BtnHnSearch_Click;

            theme1.Theme = bc.iniC.themeApplication;
            txtDate.Value = System.DateTime.Now.Year + "-" + System.DateTime.Now.ToString("MM-dd");
            lbDtrName.Text = bc.user.fullname;

            initGrfQue();
            setGrfQue();
            initGrfApm();
            initGrfFinish();
            initGrfIPD();
            //lbDtrName.Font = fEditBig;
            //lbTxtPttHn.Font = fEditBig;
            //txtPttHn.Font = fEditBig;
        }

        private void BtnHnSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setSearch();
        }

        private void TC1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setRefreshTab();
        }
        private void TxtDate_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            setRefreshTab();
        }
        private void TxtPttHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                setSearch();
            }
        }

        private void initComponent()
        {
            int gapLine = 20, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;

            theme1 = new C1.Win.C1Themes.C1ThemeController();
            this.sb1 = new C1.Win.C1Ribbon.C1StatusBar();
            panel1 = new System.Windows.Forms.Panel();
            pnHead = new System.Windows.Forms.Panel();
            pnBotton = new System.Windows.Forms.Panel();
            pnQue = new System.Windows.Forms.Panel();
            pnApm = new System.Windows.Forms.Panel();
            pnFinish = new System.Windows.Forms.Panel();
            pnIPD = new System.Windows.Forms.Panel();

            theme1 = new C1.Win.C1Themes.C1ThemeController();
            tC1 = new C1.Win.C1Command.C1DockingTab();
            tabQue = new C1.Win.C1Command.C1DockingTabPage();
            tabApm = new C1.Win.C1Command.C1DockingTabPage();
            tabFinish = new C1.Win.C1Command.C1DockingTabPage();
            tabIPD = new C1.Win.C1Command.C1DockingTabPage();

            panel1.SuspendLayout();
            pnHead.SuspendLayout();
            pnBotton.SuspendLayout();
            pnQue.SuspendLayout();
            pnApm.SuspendLayout();
            pnFinish.SuspendLayout();
            pnIPD.SuspendLayout();
            tC1.SuspendLayout();
            tabQue.SuspendLayout();
            tabApm.SuspendLayout();
            tabFinish.SuspendLayout();
            tabIPD.SuspendLayout();

            this.SuspendLayout();

            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.TabIndex = 0;
            //panel1.BackColor = Color.Brown;
            this.sb1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.sb1.Location = new System.Drawing.Point(0, 620);
            this.sb1.Name = "sb1";
            this.sb1.Size = new System.Drawing.Size(956, 22);
            this.sb1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Blue;
            pnHead.Size = new System.Drawing.Size(scrW, 50);
            pnHead.BorderStyle = BorderStyle.Fixed3D;

            tabQue.Name = "tabQue";
            tabQue.TabIndex = 0;
            tabQue.Text = "Queue";
            tabQue.Font = fEditB;
            //tabQue.TabIndex = tC1.TabCount + 1;

            tabApm.Name = "tabApm";
            tabApm.TabIndex = 1;
            tabApm.Text = "Appointment";
            tabApm.Font = fEditB;

            tabFinish.Name = "tabFinish";
            tabFinish.TabIndex = 2;
            tabFinish.Text = "Finish";
            tabFinish.Font = fEditB;

            tabIPD.Name = "tabIPD";
            tabIPD.TabIndex = 3;
            tabIPD.Text = "IPD";
            tabIPD.Font = fEditB;

            tC1.Dock = System.Windows.Forms.DockStyle.Fill;
            tC1.HotTrack = true;
            tC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tC1.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            tC1.TabsShowFocusCues = true;
            tC1.Alignment = TabAlignment.Top;
            tC1.SelectedTabBold = true;
            tC1.Name = "tC1";
            tC1.Font = fEditB;
            tC1.CanCloseTabs = true;
            tC1.CanAutoHide = false;
            tC1.SelectedTabBold = true;
            //tC1.Location = new System.Drawing.Point(0, 0);
            //tC1.BackColor = Color.White;            

            pnHead.Dock = DockStyle.Top;
            pnBotton.Dock = DockStyle.Fill;            
            pnBotton.BorderStyle = BorderStyle.FixedSingle;
            pnQue.Dock = DockStyle.Fill;
            pnApm.Dock = DockStyle.Fill;
            pnFinish.Dock = DockStyle.Fill;
            pnIPD.Dock = DockStyle.Fill;

            setControlComponent();

            this.Controls.Add(panel1);
            this.Controls.Add(this.sb1);
            
            panel1.Controls.Add(pnBotton);
            panel1.Controls.Add(pnHead);
            pnBotton.Controls.Add(tC1);
            tC1.Controls.Add(tabQue);
            tC1.Controls.Add(tabApm);
            tC1.Controls.Add(tabFinish);
            tC1.Controls.Add(tabIPD);
            tabQue.Controls.Add(pnQue);
            tabApm.Controls.Add(pnApm);
            tabFinish.Controls.Add(pnFinish);
            tabIPD.Controls.Add(pnIPD);

            pnHead.Controls.Add(lbDtrName);
            pnHead.Controls.Add(txtPttHn);
            pnHead.Controls.Add(btnHnSearch);
            pnHead.Controls.Add(txtDate);
            pnHead.Controls.Add(lbTxtDate);
            pnHead.Controls.Add(lbTxtPttHn);
            //pnHead.Controls.Add(lbPttName);

            this.WindowState = FormWindowState.Maximized;

            //lbDtrName.ResumeLayout(false);
            //lbTxtPttHn.ResumeLayout(false);
            panel1.ResumeLayout(false);
            pnHead.ResumeLayout(false);
            pnBotton.ResumeLayout(false);
            pnQue.ResumeLayout(false);
            pnApm.ResumeLayout(false);
            pnFinish.ResumeLayout(false);
            pnIPD.ResumeLayout(false);
            tC1.ResumeLayout(false);
            tabQue.ResumeLayout(false);
            tabApm.ResumeLayout(false);
            tabFinish.ResumeLayout(false);
            tabIPD.ResumeLayout(false);
            this.ResumeLayout(false);

            pnQue.PerformLayout();
            pnApm.PerformLayout();
            
            tC1.PerformLayout();
            tabQue.PerformLayout();
            tabApm.PerformLayout();
            this.PerformLayout();
        }
        private void setControlComponent()
        {
            int gapLine = 10, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            lbDtrName = new Label();
            lbDtrName.Text = "...";
            lbDtrName.Font = fEditBig;
            lbDtrName.Location = new System.Drawing.Point(gapX, 5);
            lbDtrName.AutoSize = true;
            lbDtrName.Name = "lbDtrName";

            lbTxtPttHn = new Label();
            lbTxtPttHn.Text = "HN :";
            lbTxtPttHn.Font = fEditBig;
            size = bc.MeasureString(lbTxtPttHn);
            lbTxtPttHn.Location = new System.Drawing.Point(((scrW / 2) - size.Width)-60, lbDtrName.Location.Y);
            lbTxtPttHn.AutoSize = true;
            lbTxtPttHn.Name = "lbTxtPttHn";
            //lbDtrName.SuspendLayout();
            //lbTxtPttHn.SuspendLayout();

            txtPttHn = new C1TextBox();
            txtPttHn.Font = fEditBig;
            txtPttHn.Location = new System.Drawing.Point(lbTxtPttHn.Location.X + size.Width + 5, lbTxtPttHn.Location.Y);
            txtPttHn.Size = new Size(120, 20);

            btnHnSearch = new C1Button();
            btnHnSearch.Name = "btnHnSearch";
            btnHnSearch.Text = "...";
            btnHnSearch.Font = fEdit;
            //size = bc.MeasureString(btnHnSearch);
            btnHnSearch.Location = new System.Drawing.Point(txtPttHn.Location.X + txtPttHn .Width + 5, lbTxtPttHn.Location.Y);
            btnHnSearch.Size = new Size(30, lbTxtPttHn.Height);
            btnHnSearch.Font = fEdit;

            //lbPttName = new Label();
            //lbPttName.Text = "...";
            //lbPttName.Font = fEditBig;
            //lbPttName.Location = new System.Drawing.Point(btnHnSearch.Location.X + btnHnSearch.Width + 10, lbTxtPttHn.Location.Y);
            //lbPttName.AutoSize = true;
            //lbPttName.Name = "lbPttName";

            txtDate = new C1DateEdit();
            txtDate.AllowSpinLoop = false;
            txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            txtDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDate.CurrentTimeZone = false;
            txtDate.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            txtDate.DisplayFormat.FormatType = FormatTypeEnum.CustomFormat;
            txtDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtDate.EditFormat.CustomFormat = "dd/MM/yyyy";
            txtDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtDate.GMTOffset = System.TimeSpan.Parse("00:00:00");
            txtDate.ImagePadding = new System.Windows.Forms.Padding(0);
            //size = bc.MeasureString(lbtxtDateStart);
            txtDate.Location = new System.Drawing.Point(scrW - txtDate.Width + 5, lbTxtPttHn.Location.Y);
            txtDate.Name = "txtDateStart";
            txtDate.Size = new System.Drawing.Size(111, 20);
            txtDate.TabIndex = 12;
            txtDate.Tag = null;
            //theme1.SetTheme(this.txtDate, "(default)");
            txtDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            lbTxtDate = new Label();
            lbTxtDate.Text = "Date :";
            lbTxtDate.Font = fEdit;
            size = bc.MeasureString(lbTxtDate);
            lbTxtDate.Location = new System.Drawing.Point(txtDate .Location.X - size.Width -5, lbTxtPttHn.Location.Y);
            lbTxtDate.AutoSize = true;
            lbTxtDate.Name = "lbTxtPttHn";
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setRefreshTab();
        }
        private void setRefreshTab()
        {
            if (tC1.SelectedTab == tabApm)
            {
                setGrfApm();
            }
            else if (tC1.SelectedTab == tabQue)
            {
                setGrfQue();
            }
            else if (tC1.SelectedTab == tabFinish)
            {
                setGrfFinish();
            }
            else if (tC1.SelectedTab == tabIPD)
            {
                setGrfIPD();
            }
        }
        private void initGrfQue()
        {
            grfQue = new C1FlexGrid();
            grfQue.Font = fEdit;
            grfQue.Dock = System.Windows.Forms.DockStyle.Fill;
            grfQue.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            grfQue.DoubleClick += GrfQue_DoubleClick;

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnQue.Controls.Add(grfQue);
            grfQue.Rows[0].Visible = false;
            grfQue.Cols[0].Visible = false;
            theme1.SetTheme(grfQue, bc.iniC.themeApp);

        }

        private void GrfQue_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfQue == null) return;
            if (grfQue.Row <= 0) return;
            if (grfQue.Col <= 0) return;

            String vn = "", hn = "", vsdate = "", txt = "";
            vn = grfQue[grfQue.Row, colQueVnShow] != null ? grfQue[grfQue.Row, colQueVnShow].ToString() : "";
            hn = grfQue[grfQue.Row, colQueHn] != null ? grfQue[grfQue.Row, colQueHn].ToString() : "";
            txt = grfQue[grfQue.Row, colQuePttName] != null ? grfQue[grfQue.Row, colQuePttName].ToString() : "";
            openNewForm(hn, txt);
        }

        private void setGrfQue()
        {
            grfQue.Clear();
            grfQue.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfQue.Cols.Count = 18;
            //Column colpic1 = grf.Cols[colPic1];
            //colpic1.DataType = typeof(Image);
            //Column colpic2 = grf.Cols[colPic2];
            //colpic2.DataType = typeof(Image);
            //Column colpic3 = grf.Cols[colPic3];
            //colpic3.DataType = typeof(Image);
            //Column colpic4 = grf.Cols[colPic4];
            //colpic4.DataType = typeof(Image);
            grfQue.Cols[colQueVnShow].Caption = "VN";
            grfQue.Cols[colQueHn].Caption = "HN";
            grfQue.Cols[colQuePttName].Caption = "Patient Name";
            grfQue.Cols[colQueVsDate].Caption = "Date";
            grfQue.Cols[colQueVsTime].Caption = "Time";
            grfQue.Cols[colQueSex].Caption = "Sex";
            grfQue.Cols[colQueAge].Caption = "Age";
            grfQue.Cols[colQuePaid].Caption = "Paid";
            grfQue.Cols[colQueSymptom].Caption = "Symptom";
            grfQue.Cols[colQueHeight].Caption = "Height";
            grfQue.Cols[coolQueBw].Caption = "BW";
            grfQue.Cols[colQueBp].Caption = "BP";
            grfQue.Cols[colQuePulse].Caption = "Pulse";
            grfQue.Cols[colQyeTemp].Caption = "Temp";
            grfQue.Cols[colQueDsc].Caption = "Description";

            grfQue.Cols[colQueVnShow].Width = 80;
            grfQue.Cols[colQueHn].Width = 80;
            grfQue.Cols[colQuePttName].Width = 310;
            grfQue.Cols[colQueVsDate].Width = 110;
            grfQue.Cols[colQueVsTime].Width = 80;
            grfQue.Cols[colQueSex].Width = 60;
            grfQue.Cols[colQueAge].Width = 80;
            grfQue.Cols[colQuePaid].Width = 110;
            grfQue.Cols[colQueSymptom].Width = 300;
            grfQue.Cols[colQueHeight].Width = 60;
            grfQue.Cols[coolQueBw].Width = 60;
            grfQue.Cols[colQueBp].Width = 60;
            grfQue.Cols[colQuePulse].Width = 60;
            grfQue.Cols[colQyeTemp].Width = 60;
            grfQue.Cols[colQueDsc].Width = 300;
            grfQue.ShowCursor = true;
            //grf.Cols[colPic1].ImageAndText = true;
            //grf.Cols[colPic2].ImageAndText = true;
            //grf.Cols[colPic3].ImageAndText = true;
            //grf.Cols[colPic4].ImageAndText = true;
            //grf.Styles.Normal.ImageAlign = ImageAlignEnum.CenterTop;
            //grf.Styles.Normal.TextAlign = TextAlignEnum.CenterBottom;
            ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("&แก้ไข Image", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&Rotate Image", new EventHandler(ContextMenu_retate));
            //menuGw.MenuItems.Add("Delete Image", new EventHandler(ContextMenu_delete));
            grfQue.ContextMenu = menuGw;
            //foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            //{
            //menuGw.MenuItems.Add("&เลือกประเภทเอกสาร และUpload Image ["+dgs.doc_group_name+"]", new EventHandler(ContextMenu_upload));
            String date = "";
            DateTime dtt = new DateTime();
            //if (lDgss.Count <= 0) getlBsp();
            //new LogWriter("d", "FrmDoctorView1 setGrfQue 01 ");
            date = txtDate.Text;
            DataTable dt = new DataTable();
            //if (DateTime.TryParse(txtDate.Value.ToString(), out dtt))
            //{
            //    date = dtt.Year.ToString() + "-" + dtt.ToString("MM-dd");
            //}
            //if (date.Length <= 0)
            //{
            //    return;
            //}
            //this.Text = "Last Update 2020-02-06 Format Date " + System.DateTime.Now.ToString("dd-MM-yyyy") + " [" + date + "] hostFTP " + bc.iniC.hostFTP + " folderFTP " + bc.iniC.folderFTP;
            //new LogWriter("d", "FrmDoctorView1 setGrfQue 02 date "+ date);
            dt = bc.bcDB.vsDB.selectVisitByDtr(bc.user.staff_id, bc.datetoDB(date), "queue");
            //new LogWriter("d", "FrmDoctorView1 setGrfQue 03 ");
            int i = 1;
            grfQue.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    String status = "", vn = "";
                    Patient ptt = new Patient();
                    vn = row["MNC_VN_NO"].ToString() + "/" + row["MNC_VN_SEQ"].ToString() + "(" + row["MNC_VN_SUM"].ToString() + ")";
                    ptt.patient_birthday = bc.datetoDB(row["mnc_bday"].ToString());
                    grfQue[i, 0] = (i);
                    grfQue[i, colQueId] = vn;
                    grfQue[i, colQueVnShow] = vn;
                    grfQue[i, colQueVsDate] = bc.datetoShow(row["mnc_date"].ToString());
                    grfQue[i, colQueHn] = row["MNC_HN_NO"].ToString();
                    grfQue[i, colQuePttName] = row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfQue[i, colQueVsTime] = bc.FormatTime(row["mnc_time"].ToString());
                    //grfQue[i, colQueVnShow] = row["MNC_REQ_DAT"].ToString();
                    grfQue[i, colQueSex] = row["mnc_sex"].ToString();
                    grfQue[i, colQueAge] = ptt.AgeStringShort();
                    grfQue[i, colQuePaid] = row["MNC_FN_TYP_DSC"].ToString();
                    grfQue[i, colQueSymptom] = row["MNC_SHIF_MEMO"].ToString();
                    grfQue[i, colQueHeight] = row["mnc_high"].ToString();
                    grfQue[i, coolQueBw] = row["mnc_weight"].ToString();
                    grfQue[i, colQueBp] = row["mnc_bp1_l"].ToString();
                    grfQue[i, colQuePulse] = row["MNC_ratios"].ToString();
                    grfQue[i, colQyeTemp] = row["MNC_temp"].ToString();
                    grfQue[i, colQuePreNo] = row["MNC_pre_no"].ToString();
                    grfQue[i, colQueDsc] = row["MNC_ref_dsc"].ToString();
                    if ((i % 2) == 0)
                        grfQue.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);

                    i++;
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmDoctorView setGrfQue ex " + ex.Message);
                }
            }

            //addDevice.MenuItems.Add("", new EventHandler(ContextMenu_upload));
            //menuGw.MenuItems.Add(addDevice);
            //}


            //row1[colVSE2] = row[ic.ivfDB.pApmDB.pApm.e2].ToString().Equals("1") ? imgCorr : imgTran;
            grfQue.Cols[colQueId].Visible = false;
            grfQue.Cols[colQuePreNo].Visible = false;

            grfQue.Cols[colQueVnShow].AllowEditing = false;
            grfQue.Cols[colQueHn].AllowEditing = false;
            grfQue.Cols[colQuePttName].AllowEditing = false;
            grfQue.Cols[colQueVsDate].AllowEditing = false;
            grfQue.Cols[colQueVsTime].AllowEditing = false;
            grfQue.Cols[colQueSex].AllowEditing = false;
            grfQue.Cols[colQueAge].AllowEditing = false;
            grfQue.Cols[colQuePaid].AllowEditing = false;
            grfQue.Cols[colQueSymptom].AllowEditing = false;
            grfQue.Cols[colQueHeight].AllowEditing = false;
            grfQue.Cols[coolQueBw].AllowEditing = false;
            grfQue.Cols[colQueBp].AllowEditing = false;
            grfQue.Cols[colQuePulse].AllowEditing = false;
            grfQue.Cols[colQyeTemp].AllowEditing = false;
            grfQue.Cols[colQuePreNo].AllowEditing = false;
            grfQue.Cols[colQueDsc].AllowEditing = false;
            //grfQue.Cols[colQueVnShow].AllowEditing = false;
        }
        private void initGrfIPD()
        {
            grfIPD = new C1FlexGrid();
            grfIPD.Font = fEdit;
            grfIPD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfIPD.Location = new System.Drawing.Point(0, 0);
            grfIPD.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);

            grfIPD.DoubleClick += GrfIPD_DoubleClick;

            pnIPD.Controls.Add(grfIPD);
            grfIPD.Rows[0].Visible = false;
            grfIPD.Cols[0].Visible = false;
            theme1.SetTheme(grfIPD, bc.iniC.themeApp);
        }

        private void GrfIPD_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfIPD == null) return;
            if (grfIPD.Row <= 0) return;
            if (grfIPD.Col <= 0) return;

            String vn = "", hn = "", vsdate = "", txt = "";
            vn = grfIPD[grfIPD.Row, colFinVnShow] != null ? grfIPD[grfIPD.Row, colFinVnShow].ToString() : "";
            hn = grfIPD[grfIPD.Row, colFinHn] != null ? grfIPD[grfIPD.Row, colFinHn].ToString() : "";
            txt = grfIPD[grfIPD.Row, colFinPttName] != null ? grfIPD[grfIPD.Row, colFinPttName].ToString() : "";
            openNewForm(hn, txt);
        }
        private void setGrfIPD()
        {
            grfIPD.Clear();
            grfIPD.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfIPD.Cols.Count = 14;
            grfIPD.Cols[colIPDHn].Caption = "HN";
            grfIPD.Cols[colIPDVnShow].Caption = "VN";
            grfIPD.Cols[colIPDPttName].Caption = "Patient Name";
            grfIPD.Cols[colIPDDate].Caption = "Date";
            grfIPD.Cols[colIPDTime].Caption = "Time";
            grfIPD.Cols[colIPDSex].Caption = "Sex";
            grfIPD.Cols[colIPDAge].Caption = "Age";
            grfIPD.Cols[colIPDDsc].Caption = "Description";
            grfIPD.Cols[colIPDRemark].Caption = "Symptom";
            grfIPD.Cols[colIPDDept].Caption = "Dept";
            grfIPD.Cols[colIPDWrd].Caption = "Ward";
            grfIPD.Cols[colIPDAn].Caption = "AN";

            grfIPD.Cols[colIPDHn].Width = 80;
            grfIPD.Cols[colIPDVnShow].Width = 80;
            grfIPD.Cols[colIPDPttName].Width = 300;
            grfIPD.Cols[colIPDDate].Width = 100;
            grfIPD.Cols[colIPDTime].Width = 80;
            grfIPD.Cols[colIPDSex].Width = 80;
            grfIPD.Cols[colIPDAge].Width = 80;
            grfIPD.Cols[colIPDDsc].Width = 200;
            grfIPD.Cols[colIPDRemark].Width = 300;
            grfIPD.Cols[colIPDDept].Width = 170;
            grfIPD.Cols[colIPDWrd].Width = 110;
            grfIPD.Cols[colIPDAn].Width = 70;

            ContextMenu menuGw = new ContextMenu();
            grfIPD.ContextMenu = menuGw;
            String date = "";
            //if (lDgss.Count <= 0) getlBsp();
            date = txtDate.Text;
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectPttinWardByDtr(bc.user.staff_id);
            int i = 1;
            grfIPD.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    String status = "", vn = "";
                    Patient ptt = new Patient();
                    ptt.patient_birthday = bc.datetoDB(row["mnc_bday"].ToString());
                    vn = row["MNC_VN_NO"].ToString() + "/" + row["MNC_VN_SEQ"].ToString() + "(" + row["MNC_VN_SUM"].ToString() + ")";
                    grfIPD[i, 0] = (i);
                    grfIPD[i, colIPDHn] = row["MNC_HN_NO"].ToString();
                    grfIPD[i, colIPDVnShow] = vn;
                    grfIPD[i, colIPDPttName] = row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfIPD[i, colIPDDate] = bc.datetoShow(row["MNC_AD_DATE"].ToString());
                    grfIPD[i, colIPDTime] = bc.FormatTime(row["mnc_time"].ToString());
                    grfIPD[i, colIPDSex] = row["mnc_sex"].ToString();
                    grfIPD[i, colIPDAge] = ptt.AgeStringShort();
                    grfIPD[i, colIPDDsc] = row["MNC_FN_TYP_DSC"].ToString();
                    grfIPD[i, colIPDRemark] = row["MNC_SHIF_MEMO"].ToString();
                    grfIPD[i, colIPDDept] = row["MNC_MD_DEP_DSC1"].ToString();
                    grfIPD[i, colIPDWrd] = row["MNC_MD_DEP_DSC"].ToString();
                    grfIPD[i, colIPDAn] = row["mnc_an_no"].ToString();
                    //if ((i % 2) == 0)
                    //    grfIPD.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                    i++;
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmDoctorView setGrfApm ex " + ex.Message);
                }
            }
            grfIPD.Cols[colIPDId].Visible = false;

            grfIPD.Cols[colIPDHn].AllowEditing = false;
            grfIPD.Cols[colIPDVnShow].Visible = false;
            grfIPD.Cols[colIPDPttName].AllowEditing = false;
            grfIPD.Cols[colIPDDate].AllowEditing = false;
            grfIPD.Cols[colIPDTime].AllowEditing = false;
            grfIPD.Cols[colIPDSex].AllowEditing = false;
            grfIPD.Cols[colIPDAge].AllowEditing = false;
            grfIPD.Cols[colIPDDsc].AllowEditing = false;
            grfIPD.Cols[colIPDRemark].AllowEditing = false;
            grfIPD.Cols[colIPDDept].AllowEditing = false;
            //grfIPD.Cols[colIPDDsc].Visible = false;
        }
        private void initGrfFinish()
        {
            grfFin = new C1FlexGrid();
            grfFin.Font = fEdit;
            grfFin.Dock = System.Windows.Forms.DockStyle.Fill;
            grfFin.Location = new System.Drawing.Point(0, 0);
            grfFin.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);

            grfFin.DoubleClick += GrfFin_DoubleClick;

            pnFinish.Controls.Add(grfFin);
            grfFin.Rows[0].Visible = false;
            grfFin.Cols[0].Visible = false;
            theme1.SetTheme(grfFin, bc.iniC.themeApp);
        }

        private void GrfFin_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfFin == null) return;
            if (grfFin.Row <= 0) return;
            if (grfFin.Col <= 0) return;

            String vn = "", hn = "", vsdate = "", txt = "";
            vn = grfFin[grfFin.Row, colFinVnShow] != null ? grfFin[grfFin.Row, colFinVnShow].ToString() : "";
            hn = grfFin[grfFin.Row, colFinHn] != null ? grfFin[grfFin.Row, colFinHn].ToString() : "";
            txt = grfFin[grfFin.Row, colFinPttName] != null ? grfFin[grfFin.Row, colFinPttName].ToString() : "";
            openNewForm(hn, txt);
        }
        private void setGrfFinish()
        {
            grfFin.Clear();
            grfFin.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfFin.Cols.Count = 12;
            grfFin.Cols[colFinHn].Caption = "HN";
            grfFin.Cols[colFinVnShow].Caption = "VN";
            grfFin.Cols[colFinPttName].Caption = "Patient Name";
            grfFin.Cols[colFinDate].Caption = "Date";
            grfFin.Cols[colFinTime].Caption = "Time";
            grfFin.Cols[colFinSex].Caption = "Sex";
            grfFin.Cols[colFinAge].Caption = "Age";
            grfFin.Cols[colFinDsc].Caption = "Description";
            grfFin.Cols[colFinRemark].Caption = "Symptom";
            grfFin.Cols[colFinDept].Caption = "Dept";

            grfFin.Cols[colFinHn].Width = 80;
            grfFin.Cols[colFinVnShow].Width = 80;
            grfFin.Cols[colFinPttName].Width = 300;
            grfFin.Cols[colFinDate].Width = 100;
            grfFin.Cols[colFinTime].Width = 80;
            grfFin.Cols[colFinSex].Width = 80;
            grfFin.Cols[colFinAge].Width = 80;
            grfFin.Cols[colFinDsc].Width = 300;
            grfFin.Cols[colFinRemark].Width = 300;
            grfFin.Cols[colFinDept].Width = 110;

            ContextMenu menuGw = new ContextMenu();
            grfFin.ContextMenu = menuGw;
            String date = "";
            //if (lDgss.Count <= 0) getlBsp();
            date = txtDate.Text;
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectVisitByDtr(bc.user.staff_id, bc.datetoDB(date), "finish");
            int i = 1;
            grfFin.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    String status = "", vn = "";
                    Patient ptt = new Patient();
                    ptt.patient_birthday = bc.datetoDB(row["mnc_bday"].ToString());
                    vn = row["MNC_VN_NO"].ToString() + "/" + row["MNC_VN_SEQ"].ToString() + "(" + row["MNC_VN_SUM"].ToString() + ")";
                    grfFin[i, 0] = (i);
                    grfFin[i, colFinHn] = row["MNC_HN_NO"].ToString();
                    grfFin[i, colFinVnShow] = vn;
                    grfFin[i, colFinPttName] = row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfFin[i, colFinDate] = bc.datetoShow(row["mnc_date"].ToString());
                    grfFin[i, colFinTime] = bc.FormatTime(row["mnc_time"].ToString());
                    grfFin[i, colFinSex] = row["mnc_sex"].ToString();
                    grfFin[i, colFinAge] = ptt.AgeStringShort();
                    grfFin[i, colFinDsc] = row["MNC_ref_dsc"].ToString();
                    grfFin[i, colFinRemark] = row["MNC_SHIF_MEMO"].ToString();
                    grfFin[i, colFinDept] = row["MNC_MD_DEP_DSC1"].ToString();
                    //if ((i % 2) == 0)
                    //    grfFin.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                    i++;
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmDoctorView setGrfApm ex " + ex.Message);
                }
            }
            grfFin.Cols[colFinId].Visible = false;

            grfFin.Cols[colFinHn].AllowEditing = false;
            grfFin.Cols[colFinVnShow].Visible = false;
            grfFin.Cols[colFinPttName].AllowEditing = false;
            grfFin.Cols[colFinDate].AllowEditing = false;
            grfFin.Cols[colFinTime].AllowEditing = false;
            grfFin.Cols[colFinSex].AllowEditing = false;
            grfFin.Cols[colFinAge].AllowEditing = false;
            grfFin.Cols[colFinDsc].AllowEditing = false;
            grfFin.Cols[colFinRemark].AllowEditing = false;
            grfFin.Cols[colFinDept].AllowEditing = false;
        }
        private void initGrfApm()
        {
            grfApm = new C1FlexGrid();
            grfApm.Font = fEdit;
            grfApm.Dock = System.Windows.Forms.DockStyle.Fill;
            grfApm.Location = new System.Drawing.Point(0, 0);
            grfApm.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);

            grfApm.DoubleClick += GrfApm_DoubleClick;

            pnApm.Controls.Add(grfApm);
            grfApm.Rows[0].Visible = false;
            grfApm.Cols[0].Visible = false;
            theme1.SetTheme(grfApm, bc.iniC.themeApp);
        }

        private void GrfApm_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfApm == null) return;
            if (grfApm.Row <= 0) return;
            if (grfApm.Col <= 0) return;

            String vn = "", hn = "", vsdate = "", txt = "";
            vn = grfApm[grfApm.Row, colApmVnShow] != null ? grfApm[grfApm.Row, colApmVnShow].ToString() : "";
            hn = grfApm[grfApm.Row, colApmHn] != null ? grfApm[grfApm.Row, colApmHn].ToString() : "";
            txt = grfApm[grfApm.Row, colApmPttName] != null ? grfApm[grfApm.Row, colApmPttName].ToString() : "";
            openNewForm(hn, txt);
        }

        private void setGrfApm()
        {
            grfApm.Clear();
            grfApm.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfApm.Cols.Count = 12;
            grfApm.Cols[colApmHn].Caption = "HN";
            grfApm.Cols[colApmVnShow].Caption = "VN";
            grfApm.Cols[colApmPttName].Caption = "Patient Name";
            grfApm.Cols[colApmDate].Caption = "Date";
            grfApm.Cols[colApmTime].Caption = "Time";
            grfApm.Cols[colApmSex].Caption = "Sex";
            grfApm.Cols[colApmAge].Caption = "Age";
            grfApm.Cols[colApmDsc].Caption = "Appointment";
            grfApm.Cols[colApmRemark].Caption = "Remark";
            grfApm.Cols[colApmDept].Caption = "Dept";

            grfApm.Cols[colApmHn].Width = 80;
            grfApm.Cols[colApmVnShow].Width = 80;
            grfApm.Cols[colApmPttName].Width = 300;
            grfApm.Cols[colApmDate].Width = 100;
            grfApm.Cols[colApmTime].Width = 80;
            grfApm.Cols[colApmSex].Width = 80;
            grfApm.Cols[colApmAge].Width = 80;
            grfApm.Cols[colApmDsc].Width = 300;
            grfApm.Cols[colApmRemark].Width = 300;
            grfApm.Cols[colApmDept].Width = 110;

            ContextMenu menuGw = new ContextMenu();
            grfApm.ContextMenu = menuGw;
            String date = "";
            //if (lDgss.Count <= 0) getlBsp();
            date = txtDate.Text;
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectAppointmentByDtr(bc.user.staff_id, bc.datetoDB(date));
            int i = 1;
            grfApm.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    String status = "", vn = "";
                    Patient ptt = new Patient();
                    ptt.patient_birthday = bc.datetoDB(row["mnc_bday"].ToString());
                    vn = row["MNC_VN_NO"].ToString() + "/" + row["MNC_VN_SEQ"].ToString() + "(" + row["MNC_VN_SUM"].ToString() + ")";
                    grfApm[i, 0] = (i);
                    grfApm[i, colApmHn] = row["MNC_HN_NO"].ToString();
                    grfApm[i, colApmVnShow] = vn;
                    grfApm[i, colApmPttName] = row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfApm[i, colApmDate] = bc.datetoShow(row["mnc_app_dat"].ToString());
                    grfApm[i, colApmTime] = bc.FormatTime(row["mnc_app_tim"].ToString());
                    grfApm[i, colApmSex] = row["mnc_sex"].ToString();
                    grfApm[i, colApmAge] = ptt.AgeStringShort();
                    grfApm[i, colApmDsc] = row["mnc_app_dsc"].ToString();
                    grfApm[i, colApmRemark] = row["MNC_REM_MEMO"].ToString();
                    grfApm[i, colApmDept] = row["mnc_name"].ToString();
                    //if ((i % 2) == 0)
                    //    grfApm.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                    i++;
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmDoctorView setGrfApm ex " + ex.Message);
                }
            }
            grfApm.Cols[colApmId].Visible = false;

            grfApm.Cols[colApmHn].AllowEditing = false;
            grfApm.Cols[colApmVnShow].Visible = false;
            grfApm.Cols[colApmPttName].AllowEditing = false;
            grfApm.Cols[colApmDate].AllowEditing = false;
            grfApm.Cols[colApmTime].AllowEditing = false;
            grfApm.Cols[colApmSex].AllowEditing = false;
            grfApm.Cols[colApmAge].AllowEditing = false;
            grfApm.Cols[colApmDsc].AllowEditing = false;
            grfApm.Cols[colApmRemark].AllowEditing = false;
            grfApm.Cols[colApmDept].AllowEditing = false;
        }
        private void setSearch()
        {
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinet(txtPttHn.Text.Trim());
            showFormWaiting();
            String allergy = "";
            if (ptt.Name.Length <= 0)
            {
                frmFlash.Dispose();
                MessageBox.Show("ไม่พบ hn ในระบบ", "");
                return;
            }
            //lbPttName.Text = ptt.Name;
            openNewForm(txtPttHn.Text.Trim(), ptt.Name);
            
            frmFlash.Dispose();
        }
        private void openNewForm(String hn, String txt)
        {
            FrmScanView1 frm = new FrmScanView1(bc, hn,"hide");
            //frm.FormBorderStyle = FormBorderStyle.None;
            //AddNewTab(frm, txt);
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show(this);

            txtPttHn.Value = "";
        }
        public C1DockingTabPage AddNewTab(Form frm, String label)
        {
            frm.FormBorderStyle = FormBorderStyle.None;
            C1DockingTabPage tab = new C1DockingTabPage();
            tab.SuspendLayout();
            frm.TopLevel = false;
            tab.Width = tC1.Width - 10;
            tab.Height = tC1.Height - 35;
            tab.Name = frm.Name;

            frm.Parent = tab;
            frm.Dock = DockStyle.Fill;
            frm.Width = tab.Width;
            frm.Height = tab.Height;
            tab.Text = label;
            tab.TabIndex = tC1.TabCount + 1;
            
            //foreach (Control x in frm.Controls)
            //{
            //    if (x is DataGridView)
            //    {
            //        //x.Dock = DockStyle.Fill;
            //    }
            //}
            //tab.BackColor = System.Drawing.ColorTranslator.FromHtml("#1E1E1E");
            frm.Visible = true;

            //tC1.TabPages.Add(tab);
            tC1.TabPages.Insert(tC1.TabCount, tab);
            //frm.Location = new Point((tab.Width - frm.Width) / 2, (tab.Height - frm.Height) / 2);
            frm.Location = new Point(0, 0);
            tab.ResumeLayout();
            tab.Refresh();
            tab.Text = label;
            tab.Closing += Tab_Closing;

            theme1.SetTheme(tC1, bc.iniC.themeApplication);

            tC1.SelectedTab = tab;
            //theme1.SetTheme(tC1, "Office2010Blue");
            //theme1.SetTheme(tC1, "Office2010Green");
            return tab;
        }
        private void Tab_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //throw new NotImplementedException();

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
        private void FrmDoctorView1_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Size size = new Size();
            lbDtrName.Font = fEditBig;
            lbTxtPttHn.Font = fEditBig;
            size = bc.MeasureString(lbTxtPttHn);
            txtPttHn.Font = fEditBig;
            //lbPttName.Font = fEditBig;
            tC1.Font = fEdit;
            theme1.SetTheme(tC1, bc.iniC.themeDonor);
            txtPttHn.Location = new System.Drawing.Point(lbTxtPttHn.Location.X + size.Width + 5, lbTxtPttHn.Location.Y);
            sb1.Text = "Text";
            this.Text = "Last Update 2020-07-22 Format Date " + System.DateTime.Now.ToString("dd-MM-yyyy") + "hostFTP " + bc.iniC.hostFTP + " folderFTP " + bc.iniC.folderFTP;
            theme1.SetTheme(tC1, bc.iniC.themeApp);
            theme1.SetTheme(panel1, bc.iniC.themeApp);
            theme1.SetTheme(pnHead, bc.iniC.themeApp);
            theme1.SetTheme(pnBotton, bc.iniC.themeApp);
            timer1.Start();
        }
    }
}
