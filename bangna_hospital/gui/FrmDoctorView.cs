using bangna_hospital.control;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using bangna_hospital.object1;
using C1.Win.C1Command;

namespace bangna_hospital.gui
{
    public partial class FrmDoctorView : Form
    {
        BangnaControl bc;
        Login login;

        private Point _imageLocation = new Point(13, 5);
        private Point _imgHitArea = new Point(13, 2);
        Image CloseImage;
        C1FlexGrid grfQue, grfApm;
        int colQueId = 1, colQueVnShow = 2, colQueHn = 3, colQuePttName = 4, colQueVsDate = 5, colQueVsTime = 6, colQueSex = 7, colQueAge = 8, colQuePaid = 9, colQueSymptom=10, colQueHeight=11, coolQueBw=12, colQueBp=13, colQuePulse=14, colQyeTemp=15, colQuePreNo=16, colQueDsc=17;
        int colApmId = 1, colApmHn = 2, colApmPttName = 3, colApmDate = 4, colApmTime = 5, colApmSex = 6, colApmAge = 7, colApmDsc = 8, colApmRemark = 9, colApmDept=10;

        Boolean flagExit = false;
        Font fEdit, fEditB, fEditBig;
        System.Windows.Forms.Timer timer1;
        public FrmDoctorView(BangnaControl bc, FrmSplash splash)
        {
            InitializeComponent();
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
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize+2, FontStyle.Regular);
            theme1.Theme = bc.iniC.themeApplication;
            txtDate.Value = System.DateTime.Now.Year + "-" + System.DateTime.Now.ToString("MM-dd");
            lbDtrName.Value = bc.user.fullname;
            txtDtrId.Value = bc.userId;
            lbDtrName.Font = fEdit;
            txtDate.Font = fEdit;
            c1Label3.Font = fEdit;
            c1Label2.Font = fEditBig;
            tC1.Font = fEditBig;
            txtHn.Font = fEdit;
            c1Label1.Font = fEdit;

            txtDate.DropDownClosed += TxtDate_DropDownClosed;
            tC1.Click += TC1_Click;
            btnHnSearch.Click += BtnHnSearch_Click;
            txtHn.KeyUp += TxtHn_KeyUp;

            timer1 = new System.Windows.Forms.Timer();
            int chk = 0;
            int.TryParse(bc.iniC.timerImgScanNew, out chk);
            timer1.Interval = chk*1000;
            timer1.Enabled = true;
            timer1.Tick += Timer1_Tick;
            timer1.Stop();

            initGrfQue();
            setGrfQue();
            initGrfApm();
        }

        private void TxtHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                setSearch();
            }
        }
        private void setSearch()
        {
            String hn = "", pttname = "";
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinet(txtHn.Text.Trim());
            hn = txtHn.Text;
            openNewForm(hn, ptt.Name);
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

        private void TxtDate_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
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
        }
        private void initGrfApm()
        {
            grfApm = new C1FlexGrid();
            grfApm.Font = fEdit;
            grfApm.Dock = System.Windows.Forms.DockStyle.Fill;
            grfApm.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            grfApm.DoubleClick += GrfApm_DoubleClick;

            pnApm.Controls.Add(grfApm);
            grfApm.Rows[0].Visible = false;
            grfApm.Cols[0].Visible = false;
            theme1.SetTheme(grfApm, "Office2010Blue");
        }
        private void setGrfApm()
        {
            grfApm.Clear();
            grfApm.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfApm.Cols.Count = 11;
            grfApm.Cols[colApmHn].Caption = "HN";
            grfApm.Cols[colApmPttName].Caption = "Patient Name";
            grfApm.Cols[colApmDate].Caption = "Date";
            grfApm.Cols[colApmTime].Caption = "Time";
            grfApm.Cols[colApmSex].Caption = "Sex";
            grfApm.Cols[colApmAge].Caption = "Age";
            grfApm.Cols[colApmDsc].Caption = "Appointment";
            grfApm.Cols[colApmRemark].Caption = "Remark";
            grfApm.Cols[colApmDept].Caption = "Dept";

            grfApm.Cols[colApmHn].Width = 80;
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
                    Patient ptt = new Patient();
                    ptt.patient_birthday = bc.datetoDB(row["mnc_bday"].ToString());
                    grfApm[i, 0] = (i);
                    grfApm[i, colApmHn] = row["MNC_HN_NO"].ToString();
                    grfApm[i, colApmHn] = row["MNC_HN_NO"].ToString();
                    grfApm[i, colApmPttName] = row["prefix"].ToString() + "" + row["MNC_FNAME_T"].ToString() + "" + row["MNC_LNAME_T"].ToString();
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
                catch(Exception ex)
                {
                    new LogWriter("e", "FrmDoctorView setGrfApm ex " + ex.Message);
                }
            }
            grfApm.Cols[colApmId].Visible = false;

            grfApm.Cols[colApmHn].AllowEditing = false;
            grfApm.Cols[colApmPttName].AllowEditing = false;
            grfApm.Cols[colApmDate].AllowEditing = false;
            grfApm.Cols[colApmTime].AllowEditing = false;
            grfApm.Cols[colApmSex].AllowEditing = false;
            grfApm.Cols[colApmAge].AllowEditing = false;
            grfApm.Cols[colApmDsc].AllowEditing = false;
            grfApm.Cols[colApmRemark].AllowEditing = false;
            grfApm.Cols[colApmDept].AllowEditing = false;
        }

        private void GrfApm_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

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
            //theme1.SetTheme(grf, "Office2010Blue");

        }

        private void GrfQue_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfQue == null) return;
            if (grfQue.Row <= 0) return;
            if (grfQue.Col <= 0) return;

            String vn = "", hn="", vsdate="", txt="";
            vn = grfQue[grfQue.Row, colQueVnShow] != null ? grfQue[grfQue.Row, colQueVnShow].ToString() : "";
            hn = grfQue[grfQue.Row, colQueHn] != null ? grfQue[grfQue.Row, colQueHn].ToString() : "";
            txt = grfQue[grfQue.Row, colQuePttName] != null ? grfQue[grfQue.Row, colQuePttName].ToString() : "";
            openNewForm(hn, txt);

        }
        private void openNewForm(String hn, String txt)
        {
            FrmScanView1 frm = new FrmScanView1(bc, hn);
            frm.FormBorderStyle = FormBorderStyle.None;
            AddNewTab(frm, txt);
        }
        public C1DockingTabPage AddNewTab(Form frm, String label)
        {            
            frm.FormBorderStyle = FormBorderStyle.None;
            C1DockingTabPage tab = new C1DockingTabPage();
            //tab.SuspendLayout();
            frm.TopLevel = false;
            tab.Width = tC1.Width - 10;
            tab.Height = tC1.Height - 35;
            tab.Name = frm.Name;

            frm.Parent = tab;
            frm.Dock = DockStyle.Fill;
            frm.Width = tab.Width;
            frm.Height = tab.Height;
            frm.WindowState = FormWindowState.Maximized;
            tab.Text = label;
            //foreach (Control x in frm.Controls)
            //{
            //    if (x is DataGridView)
            //    {
            //        //x.Dock = DockStyle.Fill;
            //    }
            //}
            //tab.BackColor = System.Drawing.ColorTranslator.FromHtml("#1E1E1E");
            frm.Visible = true;

            tC1.TabPages.Add(tab);

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

        private void Tab_Closing(object sender, CancelEventArgs e)
        {
            //throw new NotImplementedException();

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
            date = txtDate.Text;
            DataTable dt = new DataTable();
            if(DateTime.TryParse(txtDate.Value.ToString(), out dtt))
            {
                date = dtt.Year.ToString() + "-" + dtt.ToString("MM-dd");
            }
            if (date.Length <= 0)
            {
                return;
            }
            this.Text = "Last Update 2020-02-06 Format Date " + System.DateTime.Now.ToString("dd-MM-yyyy") +" ["+ date + "] hostFTP " + bc.iniC.hostFTP + " folderFTP " + bc.iniC.folderFTP;
            dt = bc.bcDB.vsDB.selectVisitByDtr(bc.user.staff_id, date);
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
                    grfQue[i, colQuePttName] = row["prefix"].ToString() + "" + row["MNC_FNAME_T"].ToString() + "" + row["MNC_LNAME_T"].ToString();
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
                catch(Exception ex)
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
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setRefreshTab();
        }

        private void FrmDoctorView_Load(object sender, EventArgs e)
        {
            tC1.SelectedTab = tabQue;
            timer1.Start();
            this.Text = "Last Update 2020-02-07 Format Date "+ System.DateTime.Now.ToString("dd-MM-yyyy") + "hostFTP " + bc.iniC.hostFTP + " folderFTP " + bc.iniC.folderFTP;
        }
    }
}
