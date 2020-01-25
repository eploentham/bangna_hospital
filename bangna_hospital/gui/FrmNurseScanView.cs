using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmNurseScanView : Form
    {
        BangnaControl bc;
        MainMenu menu;

        C1DockingTab tC1;
        C1DockingTabPage tabApm, tabHn;
        C1FlexGrid grfHn, grfQue, grfApm;
        Font fEdit, fEditB, fEditBig;
        int colDateReq = 1, colHN = 2, colFullName = 3, colVN = 4, colDateReceive = 5, colReqNo = 6, colId = 7;
        int colQueId = 1, colQueVnShow = 2, colQueHn = 3, colQuePttName = 4, colQueVsDate = 5, colQueVsTime = 6, colQueSex = 7, colQueAge = 8, colQuePaid = 9, colQueSymptom = 10, colQueHeight = 11, coolQueBw = 12, colQueBp = 13, colQuePulse = 14, colQyeTemp = 15, colQuePreNo = 16, colQueDsc = 17;
        int colApmId = 1, colApmHn = 2, colApmPttName = 3, colApmDate = 4, colApmTime = 5, colApmSex = 6, colApmAge = 7, colApmDsc = 8, colApmRemark = 9, colApmDept = 10;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmNurseScanView(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            menu = null;
            initConfig();
        }
        public FrmNurseScanView(BangnaControl bc, MainMenu m)
        {
            InitializeComponent();
            this.bc = bc;
            menu = m;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);

            chkDoctor.Checked = true;
            btnOk.Click += BtnOk_Click;
            txtHn.KeyUp += TxtHn_KeyUp;
            chkHn.Click += ChkHn_Click;
            chkDoctor.Click += ChkDoctor_Click;
            txtDateStart.Value = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd");
            txtDateEnd.Value = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd");
            initTab();

            theme1.Theme = bc.iniC.themeApplication;
            setTheme();
            //initGrf();
            //initGrfApm();
        }
        private void setTheme()
        {
            theme1.SetTheme(this, theme1.Theme);
            theme1.SetTheme(panel1, theme1.Theme);
            theme1.SetTheme(panel2, theme1.Theme);
            theme1.SetTheme(panel3, theme1.Theme);
        }
        private void ChkDoctor_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setCheckHn();
        }

        private void ChkHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setCheckHn();
        }
        private void setCheckHn()
        {
            if (chkHn.Checked)
            {
                txtDateEnd.Enabled = true;
            }
            else
            {
                txtDateEnd.Enabled = false;
            }
        }
        private void TxtHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                setGrf();
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrf();
        }
        private void setGrf()
        {
            tC1.TabPages.Clear();
            if (chkHn.Checked)
            {
                setGrfHn();
            }
            else
            {
                setGrfApm();
            }
        }
        private void initTab()
        {
            tC1 = new C1.Win.C1Command.C1DockingTab();
            tC1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            tC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tC1.CanCloseTabs = true;
            tC1.CanMoveTabs = true;
            tC1.Name = "tC1";
            tC1.SelectedTabBold = true;
            tC1.Dock = System.Windows.Forms.DockStyle.Fill;
            tC1.TabLayout = C1.Win.C1Command.ButtonLayoutEnum.TextOnLeft;
            tC1.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            tC1.TabsShowFocusCues = true;
            tC1.TabsSpacing = 2;
            theme1.SetTheme(this.tC1, bc.iniC.themeApplication);
            panel2.Controls.Add(tC1);
        }
        private void initGrfApm()
        {
            tabApm = new C1.Win.C1Command.C1DockingTabPage();
            tabApm.CaptionVisible = true;
            //tabQue.Controls.Add(this.pnQue);
            tabApm.Location = new System.Drawing.Point(1, 1);
            tabApm.Name = "tabApm";
            tabApm.Size = new System.Drawing.Size(1010, 575);
            tabApm.TabIndex = 0;
            tabApm.Text = "Apointment";
            tC1.Controls.Add(tabApm);
            Panel pnApm = new Panel();
            pnApm.Dock = DockStyle.Fill;
            tabApm.Controls.Add(pnApm);
            //pnApm.Controls.Add(tabApm);

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

        private void GrfApm_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfApm == null) return;
            if (grfApm.Row <= 0) return;
            if (grfApm.Col <= 0) return;

            String hn = "", pttname = "";
            hn = grfApm[grfApm.Row, colApmHn].ToString();
            //DocScan dsc = new DocScan();
            //dsc = bc.bcDB.dscDB.selectByPk(hn);
            //C1DockingTabPage tab = new C1.Win.C1Command.C1DockingTabPage();
            //tab.CaptionVisible = true;
            //tabQue.Controls.Add(this.pnQue);
            //tab.Location = new System.Drawing.Point(1, 1);
            //tab.Name = "tab";
            //tab.Size = new System.Drawing.Size(1010, 575);
            //tab.TabIndex = 0;
            //tab.Text = "HN ";
            //tC1.Controls.Add(tab);
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinet(hn);
            //hn = txtHn.Text;
            if (ptt.Name.Length <= 0)
            {
                MessageBox.Show("ไม่พบ hn ในระบบ", "");
                return;
            }
            openNewForm(hn, ptt.Name);
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
        private void initGrfHn()
        {
            tabHn = new C1.Win.C1Command.C1DockingTabPage();
            tabHn.CaptionVisible = true;
            //tabQue.Controls.Add(this.pnQue);
            tabHn.Location = new System.Drawing.Point(1, 1);
            tabHn.Name = "tabHn";
            tabHn.Size = new System.Drawing.Size(1010, 575);
            tabHn.TabIndex = 0;
            tabHn.Text = "HN ";
            tC1.Controls.Add(tabHn);
            Panel pnHn = new Panel();
            pnHn.Dock = DockStyle.Fill;
            tabHn.Controls.Add(pnHn);
            pnHn.Controls.Add(grfHn);

            grfHn = new C1FlexGrid();
            grfHn.Font = fEdit;
            grfHn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHn.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            grfHn.DoubleClick += GrfHn_DoubleClick;
            //grfHn.Click += GrfHn_Click;

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            //panel2.Controls.Add(grfHn);
            grfHn.Rows[0].Visible = false;
            grfHn.Cols[0].Visible = false;
            theme1.SetTheme(grfHn, "Office2010Blue");

        }
        private void GrfHn_DoubleClick(object sender, EventArgs e)
        {

        }
        private void setGrfApm()
        {
            if (grfApm != null) grfApm.Dispose();
            if (grfHn != null) grfHn.Dispose();
            if (txtHn.Text.Trim().Length <= 0)
            {
                MessageBox.Show("ไม่พบ รหัสแพทย์", "");
                return;
            }
            String datestart = "", dateend = "", hn = "", txt = "";
            datestart = bc.datetoDB(txtDateStart.Text);
            dateend = bc.datetoDB(txtDateEnd.Text);
            Staff stf = bc.bcDB.stfDB.selectByLogin(txtHn.Text);
            if (stf.fullname.Length <= 0)
            {
                MessageBox.Show("ไม่พบ รหัสแพทย์ ในระบบ", "");
                return;
            }
            lbName.Text = stf.fullname;
            initGrfApm();
            grfApm.Clear();
            grfApm.Font = fEdit;
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
            //String date = "";
            //if (lDgss.Count <= 0) getlBsp();
            //date = txtDate.Text;
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectAppointmentByDtr(txtHn.Text.Trim(), bc.datetoDB(datestart));
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
                    grfApm[i, colApmPttName] = row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfApm[i, colApmDate] = bc.datetoShow(row["mnc_app_dat"].ToString());
                    grfApm[i, colApmTime] = bc.FormatTime(row["mnc_app_tim"].ToString());
                    grfApm[i, colApmSex] = row["mnc_sex"].ToString();
                    grfApm[i, colApmAge] = ptt.AgeStringShort();
                    grfApm[i, colApmDsc] = row["mnc_app_dsc"].ToString();
                    grfApm[i, colApmRemark] = row["MNC_REM_MEMO"].ToString();
                    grfApm[i, colApmDept] = row["mnc_name"].ToString();
                    //grfApm[i, colApmId] = row["mnc_name"].ToString();
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
            grfApm.Cols[colApmPttName].AllowEditing = false;
            grfApm.Cols[colApmDate].AllowEditing = false;
            grfApm.Cols[colApmTime].AllowEditing = false;
            grfApm.Cols[colApmSex].AllowEditing = false;
            grfApm.Cols[colApmAge].AllowEditing = false;
            grfApm.Cols[colApmDsc].AllowEditing = false;
            grfApm.Cols[colApmRemark].AllowEditing = false;
            grfApm.Cols[colApmDept].AllowEditing = false;
        }
        private void setGrfHn()
        {
            if (grfApm != null) grfApm.Dispose();
            if (grfHn != null) grfHn.Dispose();
            initGrfHn();
        }
        private void FrmNurseScanView_Load(object sender, EventArgs e)
        {

        }
    }
}
