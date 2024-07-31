using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SplitContainer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmNurseScanView : Form
    {
        BangnaControl bc;
        MainMenu menu;

        Panel pnLabOut, pnXray, pnXrayL, pnXrayR;
        C1SplitterPanel spPttinWrd, spPttL, spPttR, spLabOutR, spXray, spItem;
        C1DockingTab tC1;
        C1DockingTabPage tabApm, tabHn,tabPttinWrd, tabLabOutRec, tabLabOutReq;
        C1FlexGrid grfHn, grfQue, grfApm, grfPttinWrd, grfLabOutFinish, grfXrayFinish, grfLabOutRes, grfLabOutReq;
        Font fEdit, fEditB, fEditBig;
        int colDateReq = 1, colHN = 2, colFullName = 3, colVN = 4, colDateReceive = 5, colReqNo = 6, colId = 7;
        //int colQueId = 1, colQueVnShow = 2, colQueHn = 3, colQuePttName = 4, colQueVsDate = 5, colQueVsTime = 6, colQueSex = 7, colQueAge = 8, colQuePaid = 9, colQueSymptom = 10, colQueHeight = 11, coolQueBw = 12, colQueBp = 13, colQuePulse = 14, colQyeTemp = 15, colQuePreNo = 16, colQueDsc = 17;
        int colApmId = 1, colApmHn = 2, colApmPttName = 3, colApmDate = 4, colApmTime = 5, colApmSex = 6, colApmAge = 7, colApmDsc = 8, colApmRemark = 9, colApmDept = 10, colApmVsDate=11;
        int colLabOutHn = 1, colLabOutPttName = 2, colLabOutWard = 3, colLabOutRoom = 4, colLabOutDtrName = 5, colLabOutRemark = 6, colLabOutBefore = 7;
        int colLabOutFDateReq = 1, colLabOutFHN = 2, colLabOutFFullName = 3, colLabOutFVN = 4, colLabOutFDateReceive = 5, colLabOutFReqNo = 6, colLabOutFId = 7;
        //int colAppmHn=1, colApp
        System.Windows.Forms.Timer timer;
        String HNinWrd = "";
        Form frmFlash;
        //FrmWaiting1 frmFlash;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmNurseScanView(BangnaControl bc)
        {
            showFormWaiting();
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

            chkHn.Checked = true;
            btnOk.Click += BtnOk_Click;
            txtHn.KeyUp += TxtHn_KeyUp;
            chkHn.Click += ChkHn_Click;
            chkDoctor.Click += ChkDoctor_Click;
            chkDateReq.Click += ChkDateReq_Click;
            chkDateLabOut.Click += ChkDateLabOut_Click;
            btnCap.Click += BtnCap_Click;
            chkDateApm.Click += ChkDateApm_Click;

            txtDateStart.Value = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd");
            txtDateEnd.Value = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd");
            initTab();
            setGrfPttinWrd();
            setGrfLabOutFinish(HNinWrd);
            setGrfXrayFinish(HNinWrd);

            timer = new System.Windows.Forms.Timer();
            timer.Enabled = true;
            timer.Interval = bc.timerCheckLabOut * 1000;
            timer.Tick += Timer_Tick;

            theme1.Theme = bc.iniC.themeApplication;
            setTheme();
            //initGrf();
            //initGrfApm();
        }

        private void ChkDateApm_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setCheckHn();
        }

        private void BtnCap_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmCapture frm = new FrmCapture(bc, this);
            frm.Show();
            this.Hide();
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
        private void Timer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfPttinWrd();
            setGrfLabOutFinish(HNinWrd);
        }

        private void ChkDateLabOut_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setCheckHn();
        }

        private void ChkDateReq_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setCheckHn();
        }

        private void setTheme()
        {
            theme1.SetTheme(this, theme1.Theme);
            //theme1.SetTheme(panel1, theme1.Theme);
            theme1.SetTheme(panel2, theme1.Theme);
            theme1.SetTheme(panel1, bc.iniC.themeDonor);
            theme1.SetTheme(pnHead, bc.iniC.themeDonor);
            foreach(Control c in pnHead.Controls)
            {
                theme1.SetTheme(c, bc.iniC.themeDonor);
            }
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
                c1Label4.Visible = true;
                txtDateEnd.Visible = true;
            }
            else if (chkDateReq.Checked)
            {
                txtDateStart.Visible = true;
                txtDateEnd.Visible = true;
                c1Label4.Visible = true;
            }
            else if (chkDateLabOut.Checked)
            {
                txtDateStart.Visible = true;
                txtDateEnd.Visible = true;
                c1Label4.Visible = true;
            }
            else if (chkDateApm.Checked)
            {
                txtDateStart.Visible = true;
                txtDateEnd.Visible = false;
                c1Label4.Visible = false;
            }
            else
            {
                txtDateEnd.Visible = false;
                c1Label4.Visible = false;
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
            showFormWaiting();
            //tC1.TabPages.Clear();
            if (chkHn.Checked)
            {
                String allergy = "";
                Patient ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatient(txtHn.Text.Trim());
                
                if (ptt.Name.Length <= 0)
                {
                    frmFlash.Dispose();
                    MessageBox.Show("ไม่พบ hn ในระบบ", "");
                    return;
                }
                lbName.Text = ptt.Name;
                openNewForm(txtHn.Text.Trim(), ptt.Name);
            }
            else if (chkDateLabOut.Checked)
            {
                setGrfLabOutRes();
                tC1.SelectedTab = tabLabOutRec;
                //setGrfApm();
            }
            else if (chkDateReq.Checked)
            {
                setGrfLabOutReq();
                tC1.SelectedTab = tabLabOutReq;
            }
            else if (chkDateApm.Checked)
            {
                setGrfApmDept();
            }
            else
            {
                setGrfApm();
            }
            frmFlash.Dispose();
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
            tC1.CanCloseTabs = true;
            tC1.CanAutoHide = false;
            tC1.ShowCaption = true;
            tC1.SelectedTabBold = true;
            theme1.SetTheme(this.tC1, bc.iniC.themeApplication);
            panel2.Controls.Add(tC1);
            initTabLabOut();
            //initTabXray();
            initGrfDateLabRec();
            initGrfDateLabReq();
        }
        private void initGrfDateLabReq()
        {
            tabLabOutReq = new C1.Win.C1Command.C1DockingTabPage();
            tabLabOutReq.CaptionVisible = true;
            //tabQue.Controls.Add(this.pnQue);
            tabLabOutReq.Location = new System.Drawing.Point(1, 1);
            tabLabOutReq.Name = "tabLabOutRec";
            tabLabOutReq.Size = new System.Drawing.Size(1010, 575);
            tabLabOutReq.TabIndex = 0;
            tabLabOutReq.Text = "ค้นหารายการ Out Lab ตามวันที่ request ";
            tC1.Controls.Add(tabLabOutReq);
            Panel pnLabOutReq = new Panel();
            pnLabOutReq.Dock = DockStyle.Fill;
            tabLabOutReq.Controls.Add(pnLabOutReq);
            //pnApm.Controls.Add(tabApm);

            grfLabOutReq = new C1FlexGrid();
            grfLabOutReq.Font = fEdit;
            grfLabOutReq.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLabOutReq.Location = new System.Drawing.Point(0, 0);
            grfLabOutReq.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);

            grfLabOutReq.DoubleClick += GrfLabOutReq_DoubleClick;

            pnLabOutReq.Controls.Add(grfLabOutReq);
            grfLabOutReq.Rows[0].Visible = false;
            grfLabOutReq.Cols[0].Visible = false;
            tC1.SelectedTab = tabLabOutReq;
            theme1.SetTheme(grfLabOutReq, bc.iniC.themeApp);
        }

        private void GrfLabOutReq_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfLabOutReq == null) return;
            if (grfLabOutReq.Row <= 0) return;
            if (grfLabOutReq.Col <= 0) return;

            String hn = "", pttname = "";
            hn = grfLabOutReq[grfLabOutReq.Row, colLabOutFHN].ToString();
            pttname = grfLabOutReq[grfLabOutReq.Row, colLabOutFFullName].ToString();
            openNewForm(hn, pttname);
        }
        private void setGrfLabOutReq()
        {
            String datestart = "", dateend = "";
            DataTable dt = new DataTable();
            datestart = bc.datetoDB(txtDateStart.Text);
            dateend = bc.datetoDB(txtDateEnd.Text);
            grfLabOutReq.Clear();
            grfLabOutReq.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfLabOutReq.Cols.Count = 8;
            grfLabOutReq.Cols[colLabOutFDateReq].Caption = "Date Req";
            grfLabOutReq.Cols[colLabOutFHN].Caption = "HN";
            grfLabOutReq.Cols[colLabOutFFullName].Caption = "Name";
            grfLabOutReq.Cols[colLabOutFVN].Caption = "VN";
            grfLabOutReq.Cols[colLabOutFDateReceive].Caption = "Date Rec";
            grfLabOutReq.Cols[colLabOutFReqNo].Caption = "Req No";
            grfLabOutReq.Cols[colLabOutFId].Caption = "id";

            grfLabOutReq.Cols[colLabOutFDateReq].Width = 100;
            grfLabOutReq.Cols[colLabOutFHN].Width = 80;
            grfLabOutReq.Cols[colLabOutFFullName].Width = 300;
            grfLabOutReq.Cols[colLabOutFVN].Width = 100;
            grfLabOutReq.Cols[colLabOutFDateReceive].Width = 100;
            grfLabOutReq.Cols[colLabOutFReqNo].Width = 80;

            dt = bc.bcDB.dscDB.selectLabOutByDateReq(datestart, dateend, txtHn.Text.Trim(), "daterequest");

            //grfLabOutReq.Cols[colHnPrnStaffNote].Width = 60;

            //if (datestart.Length <= 0 && dateend.Length <= 0)
            //{
            //    MessageBox.Show("วันทีเริ่มต้น ไม่มีค่า", "");
            //    return;
            //}
            grfLabOutReq.ShowCursor = true;
            ContextMenu menuGw = new ContextMenu();
            grfLabOutReq.ContextMenu = menuGw;
            int i = 1;
            grfLabOutReq.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    grfLabOutReq[i, 0] = (i);
                    grfLabOutReq[i, colLabOutFHN] = row["hn"].ToString();
                    grfLabOutReq[i, colLabOutFFullName] = row["patient_fullname"].ToString();//row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfLabOutReq[i, colLabOutFDateReceive] = bc.datetoShow(row["date_create"].ToString());
                    grfLabOutReq[i, colLabOutFDateReq] = bc.datetoShow(row["date_req"].ToString());
                    grfLabOutReq[i, colLabOutFReqNo] = row["req_id"].ToString();
                    grfLabOutReq[i, colLabOutFVN] = row["vn"].ToString();
                    grfLabOutReq[i, colLabOutFId] = row["doc_scan_id"].ToString();
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmNurseScanView setGrfLabOutRec ex " + ex.Message);
                }
                i++;
            }
            grfLabOutReq.Cols[colId].Visible = false;
            grfLabOutReq.Cols[colLabOutFHN].AllowEditing = false;
            grfLabOutReq.Cols[colLabOutFFullName].AllowEditing = false;
            grfLabOutReq.Cols[colLabOutFDateReceive].AllowEditing = false;
            grfLabOutReq.Cols[colLabOutFDateReq].AllowEditing = false;
            grfLabOutReq.Cols[colLabOutFReqNo].AllowEditing = false;
            grfLabOutReq.Cols[colLabOutFVN].AllowEditing = false;
        }
        private void initGrfDateLabRec()
        {
            tabLabOutRec = new C1.Win.C1Command.C1DockingTabPage();
            tabLabOutRec.CaptionVisible = true;
            //tabQue.Controls.Add(this.pnQue);
            tabLabOutRec.Location = new System.Drawing.Point(1, 1);
            tabLabOutRec.Name = "tabLabOutRec";
            tabLabOutRec.Size = new System.Drawing.Size(1010, 575);
            tabLabOutRec.TabIndex = 0;
            tabLabOutRec.Text = "ค้นหารายการ Out Lab ตามวันที่รับผล " ;
            tC1.Controls.Add(tabLabOutRec);
            Panel pnLabOutRec = new Panel();
            pnLabOutRec.Dock = DockStyle.Fill;
            tabLabOutRec.Controls.Add(pnLabOutRec);
            //pnApm.Controls.Add(tabApm);

            grfLabOutRes = new C1FlexGrid();
            grfLabOutRes.Font = fEdit;
            grfLabOutRes.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLabOutRes.Location = new System.Drawing.Point(0, 0);
            grfLabOutRes.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);

            grfLabOutRes.DoubleClick += GrfLabOutRec_DoubleClick;

            pnLabOutRec.Controls.Add(grfLabOutRes);
            grfLabOutRes.Rows[0].Visible = false;
            grfLabOutRes.Cols[0].Visible = false;
            tC1.SelectedTab = tabLabOutRec;
            theme1.SetTheme(grfLabOutRes, bc.iniC.themeApp);
        }

        private void GrfLabOutRec_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfLabOutRes == null) return;
            if (grfLabOutRes.Row <= 0) return;
            if (grfLabOutRes.Col <= 0) return;

            showFormWaiting();
            String hn = "", pttname = "";
            hn = grfLabOutRes[grfLabOutRes.Row, colLabOutFHN].ToString();
            pttname = grfLabOutRes[grfLabOutRes.Row, colLabOutFFullName].ToString();
            openNewForm(hn, pttname);
            frmFlash.Dispose();
        }
        private void setGrfLabOutRes()
        {
            String datestart = "", dateend = "";
            DataTable dt = new DataTable();
            datestart = bc.datetoDB(txtDateStart.Text);
            dateend = bc.datetoDB(txtDateEnd.Text);
            this.Text = this.Text + " date start " + datestart + " date end " + dateend+ " windows " + bc.iniC.windows;
            grfLabOutRes.Clear();
            grfLabOutRes.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfLabOutRes.Cols.Count = 8;
            grfLabOutRes.Cols[colLabOutFDateReq].Caption = "Date Req";
            grfLabOutRes.Cols[colLabOutFHN].Caption = "HN";
            grfLabOutRes.Cols[colLabOutFFullName].Caption = "Name";
            grfLabOutRes.Cols[colLabOutFVN].Caption = "VN";
            grfLabOutRes.Cols[colLabOutFDateReceive].Caption = "Date Rec";
            grfLabOutRes.Cols[colLabOutFReqNo].Caption = "Req No";
            grfLabOutRes.Cols[colLabOutFId].Caption = "id";

            grfLabOutRes.Cols[colLabOutFDateReq].Width = 100;
            grfLabOutRes.Cols[colLabOutFHN].Width = 80;
            grfLabOutRes.Cols[colLabOutFFullName].Width = 300;
            grfLabOutRes.Cols[colLabOutFVN].Width = 100;
            grfLabOutRes.Cols[colLabOutFDateReceive].Width = 100;
            grfLabOutRes.Cols[colLabOutFReqNo].Width = 80;

            dt = bc.bcDB.dscDB.selectLabOutByDateReq(datestart, dateend, txtHn.Text.Trim(), "datecreate");

            //grfLabOutRec.Cols[colHnPrnStaffNote].Width = 60;

            //if (datestart.Length <= 0 && dateend.Length <= 0)
            //{
            //    MessageBox.Show("วันทีเริ่มต้น ไม่มีค่า", "");
            //    return;
            //}
            grfLabOutRes.ShowCursor = true;
            ContextMenu menuGw = new ContextMenu();
            grfLabOutRes.ContextMenu = menuGw;
            int i = 1;
            grfLabOutRes.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    grfLabOutRes[i, 0] = (i);
                    grfLabOutRes[i, colLabOutFHN] = row["hn"].ToString();
                    grfLabOutRes[i, colLabOutFFullName] = row["patient_fullname"].ToString();//row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfLabOutRes[i, colLabOutFDateReceive] = bc.datetoShow(row["date_create"].ToString());
                    grfLabOutRes[i, colLabOutFDateReq] = bc.datetoShow(row["date_req"].ToString());
                    grfLabOutRes[i, colLabOutFReqNo] = row["req_id"].ToString();
                    grfLabOutRes[i, colLabOutFVN] = row["vn"].ToString();
                    grfLabOutRes[i, colLabOutFId] = row["doc_scan_id"].ToString();
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmNurseScanView setGrfLabOutRec ex " + ex.Message);
                }
                i++;
            }
            grfLabOutRes.Cols[colId].Visible = false;
            grfLabOutRes.Cols[colLabOutFHN].AllowEditing = false;
            grfLabOutRes.Cols[colLabOutFFullName].AllowEditing = false;
            grfLabOutRes.Cols[colLabOutFDateReceive].AllowEditing = false;
            grfLabOutRes.Cols[colLabOutFDateReq].AllowEditing = false;
            grfLabOutRes.Cols[colLabOutFReqNo].AllowEditing = false;
            grfLabOutRes.Cols[colLabOutFVN].AllowEditing = false;
        }
        private void initGrfApm(String name)
        {
            tabApm = new C1.Win.C1Command.C1DockingTabPage();
            tabApm.CaptionVisible = true;
            //tabQue.Controls.Add(this.pnQue);
            tabApm.Location = new System.Drawing.Point(1, 1);
            tabApm.Name = "tabApm";
            tabApm.Size = new System.Drawing.Size(1010, 575);
            tabApm.TabIndex = 0;
            if (name.Length > 0)
            {
                tabApm.Text = "นัดของ " + name;
            }
            else
            {
                tabApm.Text = " ";
            }
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
            tC1.SelectedTab = tabApm;
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
            ptt = bc.bcDB.pttDB.selectPatient(hn);
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
            FrmScanView1 frm = new FrmScanView1(bc, hn, "hide");
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
        private void initTabLabOut()
        {
            tabPttinWrd = new C1.Win.C1Command.C1DockingTabPage();
            tabPttinWrd.CaptionVisible = true;
            //tabQue.Controls.Add(this.pnQue);
            tabPttinWrd.Location = new System.Drawing.Point(1, 1);
            tabPttinWrd.Name = "tabLabOut";
            
            tabPttinWrd.Size = new System.Drawing.Size(1010, 575);
            tabPttinWrd.TabIndex = 0;
            tabPttinWrd.Text = "Out LAB / Xray";
            tC1.Controls.Add(tabPttinWrd);
            C1SplitContainer scPttinWrd = new C1SplitContainer();
            scPttinWrd.Height = 565;
            scPttinWrd.Location = new System.Drawing.Point(0, 183);
            scPttinWrd.Name = "scPttinWrd";
            scPttinWrd.Size = new System.Drawing.Size(895, 544);
            scPttinWrd.TabIndex = 1;
            scPttinWrd.Text = "";
            scPttinWrd.Dock = DockStyle.Fill;
            C1SplitContainer scPttL = new C1SplitContainer();
            scPttL.Height = 565;
            scPttL.Location = new System.Drawing.Point(0, 183);
            scPttL.Name = "scPttL";
            scPttL.Size = new System.Drawing.Size(895, 544);
            scPttL.TabIndex = 1;
            scPttL.Text = "";
            scPttL.Dock = DockStyle.Fill;
            C1SplitContainer scPttR = new C1SplitContainer();
            scPttR.Height = 565;
            scPttR.Location = new System.Drawing.Point(0, 183);
            scPttR.Name = "scPttR";
            scPttR.Size = new System.Drawing.Size(895, 544);
            scPttR.TabIndex = 1;
            scPttR.Text = "";
            scPttR.Dock = DockStyle.Fill;

            //spLabOut.Dock = PanelDockStyle
            tabPttinWrd.Controls.Add(scPttinWrd);

            pnLabOut = new Panel();
            pnLabOut.Dock = DockStyle.Fill;
            pnLabOut.Size = new Size(this.Width / 2, this.Height);
            //tabLabOut.Controls.Add(pnLabOut);

            spPttinWrd = new C1SplitterPanel();
            spPttinWrd.Dock = PanelDockStyle.Top;
            Size pnlsize = new Size(pnLabOut.Width / 2, pnLabOut.Height);
            spPttinWrd.Size = pnlsize;
            spPttinWrd.Location = new System.Drawing.Point(0, 0);

            spItem = new C1SplitterPanel();
            //spItem.Dock = PanelDockStyle.Left;
            //spItem.Size = pnlsize;
            spItem.Location = new System.Drawing.Point(0, 0);

            //pnLabOut.Controls.Add(spPttinWrd);
            spPttL = new C1SplitterPanel();
            spPttL.Size = pnlsize;
            spPttL.Dock = PanelDockStyle.Left;
            spPttR = new C1SplitterPanel();
            //pnLabOutR.Dock = DockStyle.Right;
            spPttR.Size = pnlsize;

            spLabOutR = new C1SplitterPanel();
            spLabOutR.Dock = PanelDockStyle.Top;
            spLabOutR.Size = pnlsize;
            spLabOutR.Location = new System.Drawing.Point(0, 0);
            spXray = new C1SplitterPanel();
            
            //spLabOutR.Dock = PanelDockStyle.Top;
            spXray.Size = pnlsize;
            spXray.Location = new System.Drawing.Point(0, 0);

            //pnLabOut.Controls.Add(pnLabOutR);
            //scPttinWrd.Panels.Add(spPttinWrd);
            scPttinWrd.Panels.Add(spPttL);
            scPttinWrd.Panels.Add(spPttR);
            spPttR.Controls.Add(scPttR);
            spPttL.Controls.Add(scPttL);
            scPttR.SplitterWidth = 4;
            scPttR.HeaderHeight = 20;
            spLabOutR.Text = "Out Lab";
            spXray.Text = "Xray";
            scPttR.Panels.Add(spLabOutR);
            scPttR.Panels.Add(spXray);

            scPttL.SplitterWidth = 4;
            scPttL.HeaderHeight = 20;
            if (bc.iniC.station.Equals("101"))      //OPD1
            {
                spPttinWrd.Text = "Patient in OPD1";
            }
            else if (bc.iniC.station.Equals("107"))      //OPD2
            {
                spPttinWrd.Text = "Patient in OPD2";
            }
            else if (bc.iniC.station.Equals("103"))      //OPD3
            {
                spPttinWrd.Text = "Patient in OPD3";
            }
            else
            {
                spPttinWrd.Text = "Patient in Ward";
            }
            
            spItem.Text = "รายการ";
            scPttL.Panels.Add(spPttinWrd);
            scPttL.Panels.Add(spItem);


            grfPttinWrd = new C1FlexGrid();
            grfPttinWrd.Font = fEdit;
            grfPttinWrd.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPttinWrd.Location = new System.Drawing.Point(0, 0);
            grfLabOutFinish = new C1FlexGrid();
            grfLabOutFinish.Font = fEdit;
            grfLabOutFinish.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLabOutFinish.Location = new System.Drawing.Point(0, 0);
            //FilterRow fr = new FilterRow(grfExpn);
            grfXrayFinish = new C1FlexGrid();
            grfXrayFinish.Font = fEdit;
            grfXrayFinish.Dock = System.Windows.Forms.DockStyle.Fill;
            grfXrayFinish.Location = new System.Drawing.Point(0, 0);

            grfPttinWrd.DoubleClick += GrfPttinWrd_DoubleClick;
            grfLabOutFinish.DoubleClick += GrfLabOutFinish_DoubleClick;
            grfLabOutFinish.DoubleClick += GrfXrayFinish_DoubleClick;
            //grfHn.Click += GrfHn_Click;

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            spPttinWrd.Controls.Add(grfPttinWrd);
            spLabOutR.Controls.Add(grfLabOutFinish);
            spXray.Controls.Add(grfXrayFinish);

            //grfLabOutWait.Rows[0].Visible = false;
            //grfLabOutWait.Cols[0].Visible = false;
            theme1.SetTheme(grfPttinWrd, bc.iniC.themeApp);
            theme1.SetTheme(grfLabOutFinish, bc.iniC.themeApp);
        }

        private void GrfPttinWrd_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfPttinWrd == null) return;
            if (grfPttinWrd.Row <= 0) return;
            if (grfPttinWrd.Col <= 0) return;

            String hn = "", pttname = "";
            hn = grfPttinWrd[grfPttinWrd.Row, colLabOutHn].ToString();
            pttname = grfPttinWrd[grfPttinWrd.Row, colLabOutPttName].ToString();
            openNewForm(hn, pttname);
        }

        private void GrfXrayFinish_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void GrfLabOutFinish_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfLabOutFinish == null) return;
            if (grfLabOutFinish.Row <= 0) return;
            if (grfLabOutFinish.Col <= 0) return;

            String hn = "", pttname = "";
            hn = grfLabOutFinish[grfLabOutFinish.Row, colLabOutFHN].ToString();
            pttname = grfLabOutFinish[grfLabOutFinish.Row, colLabOutFFullName].ToString();
            openNewForm(hn, pttname);
        }
        private void setGrfPttinWrd()
        {
            //grfPttinWrd.Clear();
            grfPttinWrd.Font = fEdit;
            grfPttinWrd.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfPttinWrd.Cols.Count = 8;
            grfPttinWrd.Cols[colLabOutHn].Caption = "HN";
            grfPttinWrd.Cols[colLabOutPttName].Caption = "Patient Name";
            grfPttinWrd.Cols[colLabOutWard].Caption = "Ward";
            grfPttinWrd.Cols[colLabOutRoom].Caption = "Room";
            grfPttinWrd.Cols[colLabOutDtrName].Caption = "Doctor";
            grfPttinWrd.Cols[colLabOutRemark].Caption = "";
            grfPttinWrd.Cols[colLabOutBefore].Caption = "Before";
            //grfLabOutWait.Cols[colApmRemark].Caption = "Remark";
            //grfLabOutWait.Cols[colApmDept].Caption = "Dept";

            grfPttinWrd.Cols[colLabOutHn].Width = 90;
            grfPttinWrd.Cols[colLabOutPttName].Width = 250;
            grfPttinWrd.Cols[colLabOutWard].Width = 80;
            grfPttinWrd.Cols[colLabOutRoom].Width = 40;
            grfPttinWrd.Cols[colLabOutDtrName].Width = 200;
            grfPttinWrd.Cols[colLabOutRemark].Width = 200;
            grfPttinWrd.Cols[colLabOutBefore].Width = 300;
            //grfLabOutWait.Cols[colApmRemark].Width = 300;
            //grfLabOutWait.Cols[colApmDept].Width = 110;

            ContextMenu menuGw = new ContextMenu();
            grfPttinWrd.ContextMenu = menuGw;
            DataTable dt = new DataTable();
            HNinWrd = "";
            if (bc.iniC.station.Equals("100"))
            {
                return;
            }
            if (bc.iniC.station.Equals("101"))      //OPD1
            {
                dt = bc.bcDB.vsDB.selectPttinOPD(bc.iniC.station, bc.datetoDB(txtDateStart.Text));
            }
            else if (bc.iniC.station.Equals("107"))      //OPD2
            {
                dt = bc.bcDB.vsDB.selectPttinOPD(bc.iniC.station, bc.datetoDB(txtDateStart.Text));
            }
            else if (bc.iniC.station.Equals("103"))      //OPD3
            {
                dt = bc.bcDB.vsDB.selectPttinOPD(bc.iniC.station, bc.datetoDB(txtDateStart.Text));
            }
            else
            {
                dt = bc.bcDB.vsDB.selectPttinWard(bc.iniC.station);
            }
            //}
            int i = 1;
            grfPttinWrd.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    //Patient ptt = new Patient();
                    //ptt.patient_birthday = bc.datetoDB(row["mnc_bday"].ToString());
                    DataTable dtitm = new DataTable();
                    if (bc.iniC.station.Equals("101") || bc.iniC.station.Equals("107") || bc.iniC.station.Equals("103"))
                    {
                        dtitm = bc.bcDB.vsDB.selectLabOutbyVN(row["mnc_date"].ToString(), row["MNC_HN_NO"].ToString(), row["mnc_pre_no"].ToString());
                    }
                    else
                    {
                        dtitm = bc.bcDB.vsDB.selectLabOutbyVN(row["mnc_date"].ToString(), row["MNC_HN_NO"].ToString(), row["mnc_pre_no"].ToString());
                    }
                    //dtitm = bc.bcDB.vsDB.selectLabOutbyVN(row["mnc_date"].ToString(), row["MNC_HN_NO"].ToString(), row["mnc_pre_no"].ToString());
                    grfPttinWrd[i, 0] = (i);
                    grfPttinWrd[i, colLabOutHn] = row["MNC_HN_NO"].ToString();
                    grfPttinWrd[i, colLabOutPttName] = row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfPttinWrd[i, colLabOutWard] = row["mnc_md_dep_dsc"].ToString();
                    grfPttinWrd[i, colLabOutRoom] = row["mnc_rm_nam"].ToString();
                    grfPttinWrd[i, colLabOutDtrName] = row["mnc_pfix_dsc"].ToString() + " " + row["MNC_DOT_FNAME"].ToString() + " " + row["MNC_DOT_LNAME"].ToString();
                    grfPttinWrd[i, colLabOutRemark] = row["MNC_shif_memo"].ToString();
                    grfPttinWrd[i, colLabOutBefore] = row["before"].ToString();
                    HNinWrd += "'"+row["MNC_HN_NO"].ToString()+"',";
                    //grfLabOutWait[i, colApmRemark] = row["MNC_REM_MEMO"].ToString();
                    //grfLabOutWait[i, colApmDept] = row["mnc_name"].ToString();
                    //grfLabOutWait[i, colApmId] = row["mnc_name"].ToString();
                    //if ((i % 2) == 0)
                    //    grfLabOutWait.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                    String txt1 = "", txt2 = "";
                    if (dtitm.Rows.Count > 0)
                    {
                        foreach (DataRow vsrow1 in dtitm.Rows)
                        {
                            DataTable dtlabout = new DataTable();
                            dtlabout = bc.bcDB.laboDB.selectByHnDateReqLabCode(row["MNC_HN_NO"].ToString(), row["mnc_date"].ToString(), vsrow1["MNC_LB_CD"].ToString());
                            if (dtlabout.Rows.Count > 0)
                            {
                                if (dtlabout.Rows[0]["status_result"].ToString().Equals("1"))
                                {
                                    txt1 += "มี  " + vsrow1["MNC_LB_DSC"].ToString() + " -> OK " + Environment.NewLine;
                                }
                                else
                                {
                                    txt1 += "มี  " + vsrow1["MNC_LB_DSC"].ToString() + " -> waiting " + vsrow1["MNC_sch_act"].ToString() + Environment.NewLine;
                                }
                            }
                            else
                            {
                                txt1 += "มี  " + vsrow1["MNC_LB_DSC"].ToString() + " -> waiting " + vsrow1["MNC_sch_act"].ToString() + Environment.NewLine;
                            }
                            CellNote1 note1 = new CellNote1(txt1);
                            CellRange rg2 = grfPttinWrd.GetCellRange(i, colLabOutHn);
                            rg2.UserData = note1;
                        }
                        //txt2 = "มี outlab";
                        
                    }
                    i++;
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmNurseScanView setGrfPttinWrd ex " + ex.Message);
                }
            }
            //grfLabOutWait.Cols[colApmId].Visible = false;
            CellNoteManager1 mgr = new CellNoteManager1(grfPttinWrd);
            grfPttinWrd.Cols[colLabOutHn].AllowEditing = false;
            grfPttinWrd.Cols[colLabOutPttName].AllowEditing = false;
            grfPttinWrd.Cols[colLabOutWard].AllowEditing = false;
            grfPttinWrd.Cols[colLabOutRoom].AllowEditing = false;
            grfPttinWrd.Cols[colLabOutDtrName].AllowEditing = false;
            grfPttinWrd.Cols[colLabOutRemark].AllowEditing = false;
            grfPttinWrd.Cols[colLabOutBefore].AllowEditing = false;
            //grfLabOutWait.Cols[colApmRemark].AllowEditing = false;
            //grfLabOutWait.Cols[colApmDept].AllowEditing = false;
        }
        private void setGrfLabOutFinish(String hn)
        {
            grfLabOutFinish.Rows.Count = 1;
            String hn1 = "";
            if (hn.Length <= 0) return;
            hn1 = hn.Substring(hn.Length-1, 1);
            if (hn1.IndexOf(",")>=0)
            {
                hn1 = hn.Substring(0, hn.Length-1);
            }
            DataTable dt = new DataTable();            

            grfLabOutFinish.Clear();
            grfLabOutFinish.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfLabOutFinish.Cols.Count = 8;
            grfLabOutFinish.Cols[colLabOutFDateReq].Caption = "Date Req";
            grfLabOutFinish.Cols[colLabOutFHN].Caption = "HN";
            grfLabOutFinish.Cols[colLabOutFFullName].Caption = "Name";
            grfLabOutFinish.Cols[colLabOutFVN].Caption = "VN";
            grfLabOutFinish.Cols[colLabOutFDateReceive].Caption = "Date Result";
            grfLabOutFinish.Cols[colLabOutFReqNo].Caption = "Req No";
            grfLabOutFinish.Cols[colLabOutFId].Caption = "id";

            grfLabOutFinish.Cols[colLabOutFDateReq].Width = 100;
            grfLabOutFinish.Cols[colLabOutFHN].Width = 80;
            grfLabOutFinish.Cols[colLabOutFFullName].Width = 300;
            grfLabOutFinish.Cols[colLabOutFVN].Width = 100;
            grfLabOutFinish.Cols[colLabOutFDateReceive].Width = 100;
            grfLabOutFinish.Cols[colLabOutFReqNo].Width = 80;
            
            dt = bc.bcDB.dscDB.selectLabOutByHn(hn1);
            
            //grfLabOutFinish.Cols[colHnPrnStaffNote].Width = 60;

            //if (datestart.Length <= 0 && dateend.Length <= 0)
            //{
            //    MessageBox.Show("วันทีเริ่มต้น ไม่มีค่า", "");
            //    return;
            //}
            grfLabOutFinish.ShowCursor = true;
            ContextMenu menuGw = new ContextMenu();
            grfLabOutFinish.ContextMenu = menuGw;
            int i = 1;
            grfLabOutFinish.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    grfLabOutFinish[i, 0] = (i);
                    grfLabOutFinish[i, colLabOutFHN] = row["hn"].ToString();
                    grfLabOutFinish[i, colLabOutFFullName] = row["patient_fullname"].ToString();//row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfLabOutFinish[i, colLabOutFDateReceive] = bc.datetoShow(row["date_create"].ToString());
                    grfLabOutFinish[i, colLabOutFDateReq] = bc.datetoShow(row["date_req"].ToString());
                    grfLabOutFinish[i, colLabOutFReqNo] = row["req_id"].ToString();
                    grfLabOutFinish[i, colLabOutFVN] = row["vn"].ToString();
                    grfLabOutFinish[i, colLabOutFId] = row["doc_scan_id"].ToString();
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmLabOutReceiveView setGrf ex " + ex.Message);
                }
                i++;
            }
            grfLabOutFinish.Cols[colId].Visible = false;
            grfLabOutFinish.Cols[colLabOutFHN].AllowEditing = false;
            grfLabOutFinish.Cols[colLabOutFFullName].AllowEditing = false;
            grfLabOutFinish.Cols[colLabOutFDateReceive].AllowEditing = false;
            grfLabOutFinish.Cols[colLabOutFDateReq].AllowEditing = false;
            grfLabOutFinish.Cols[colLabOutFReqNo].AllowEditing = false;
            grfLabOutFinish.Cols[colLabOutFVN].AllowEditing = false;
        }
        private void setGrfXrayFinish(String hn)
        {
            String hn1 = "",hn3="";
            grfXrayFinish.Rows.Count = 1;
            if (hn.Length <= 0) return;
            hn1 = hn.Substring(hn.Length - 1, 1);
            if (hn1.IndexOf(",") >= 0)
            {
                hn1 = hn.Substring(0, hn.Length - 1);
            }
            //String[] hn2 = hn1.Split(',');
            //if (hn2.Length > 0)
            //{
            //    foreach(String hn4 in hn2)
            //    {
            //        hn3 += "'" + hn4+"',";
            //    }
            //}
            if (hn3.Length <= 0) return;
            hn1 = hn3.Substring(hn3.Length - 1, 1);
            if (hn1.IndexOf(",") >= 0)
            {
                hn1 = hn3.Substring(0, hn3.Length - 1);
            }
            DataTable dt = new DataTable();

            grfXrayFinish.Clear();
            grfXrayFinish.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfXrayFinish.Cols.Count = 8;
            grfXrayFinish.Cols[colLabOutFDateReq].Caption = "Date Req";
            grfXrayFinish.Cols[colLabOutFHN].Caption = "HN";
            grfXrayFinish.Cols[colLabOutFFullName].Caption = "Name";
            grfXrayFinish.Cols[colLabOutFVN].Caption = "VN";
            grfXrayFinish.Cols[colLabOutFDateReceive].Caption = "Date Result";
            grfXrayFinish.Cols[colLabOutFReqNo].Caption = "Req No";
            grfXrayFinish.Cols[colLabOutFId].Caption = "id";

            grfXrayFinish.Cols[colLabOutFDateReq].Width = 100;
            grfXrayFinish.Cols[colLabOutFHN].Width = 80;
            grfXrayFinish.Cols[colLabOutFFullName].Width = 300;
            grfXrayFinish.Cols[colLabOutFVN].Width = 100;
            grfXrayFinish.Cols[colLabOutFDateReceive].Width = 100;
            grfXrayFinish.Cols[colLabOutFReqNo].Width = 80;

            dt = bc.bcDB.rpttcDB.selectResultByHn(hn1);
            //grfXrayFinish.Cols[colHnPrnStaffNote].Width = 60;

            //if (datestart.Length <= 0 && dateend.Length <= 0)
            //{
            //    MessageBox.Show("วันทีเริ่มต้น ไม่มีค่า", "");
            //    return;
            //}
            grfXrayFinish.ShowCursor = true;
            ContextMenu menuGw = new ContextMenu();
            grfXrayFinish.ContextMenu = menuGw;
            int i = 1;
            grfXrayFinish.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    grfXrayFinish[i, 0] = (i);
                    grfXrayFinish[i, colLabOutFHN] = row["PID"].ToString();
                    grfXrayFinish[i, colLabOutFFullName] = row["pkname"].ToString();//row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfXrayFinish[i, colLabOutFDateReceive] = bc.datetoShow(row["confirmdate"].ToString());
                    grfXrayFinish[i, colLabOutFDateReq] = bc.datetoShow(row["studydate"].ToString());
                    grfXrayFinish[i, colLabOutFReqNo] = row["accessnum"].ToString();
                    grfXrayFinish[i, colLabOutFVN] = row["accessnum"].ToString();
                    grfXrayFinish[i, colLabOutFId] = row["studykey"].ToString()+"."+ row["histno"].ToString()+"."+row["recno"].ToString();
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmNurseScanView setGrfXrayFinish ex " + ex.Message);
                }
                i++;
            }
            grfXrayFinish.Cols[colId].Visible = false;
            grfXrayFinish.Cols[colLabOutFHN].AllowEditing = false;
            grfXrayFinish.Cols[colLabOutFFullName].AllowEditing = false;
            grfXrayFinish.Cols[colLabOutFDateReceive].AllowEditing = false;
            grfXrayFinish.Cols[colLabOutFDateReq].AllowEditing = false;
            grfXrayFinish.Cols[colLabOutFReqNo].AllowEditing = false;
            grfXrayFinish.Cols[colLabOutFVN].AllowEditing = false;
        }
        private void initTabXray()
        {
            pnXray = new Panel();
            pnXray.Dock = DockStyle.Fill;
            pnXray.Size = new Size(this.Width / 2, this.Height);

            pnXrayL = new Panel();
            pnXrayL.Dock = DockStyle.Left;
            Size pnlsize = new Size(pnXray.Width / 2, pnXray.Height);
            pnXrayL.Size = pnlsize;
            pnXrayL.Location = new System.Drawing.Point(0, 0);
            pnXray.Controls.Add(pnXrayL);
            pnXrayR = new Panel();
            pnXrayR.Dock = DockStyle.Right;
            pnXrayR.Size = pnlsize;
            pnXray.Controls.Add(pnXrayR);
            grfXrayFinish = new C1FlexGrid();
            grfXrayFinish.Font = fEdit;
            grfXrayFinish.Dock = System.Windows.Forms.DockStyle.Fill;
            grfXrayFinish.Location = new System.Drawing.Point(0, 0);
            //FilterRow fr = new FilterRow(grfExpn);
            
            grfXrayFinish.DoubleClick += GrfXrayFinish_DoubleClick;
            //grfHn.Click += GrfHn_Click;

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            
            pnXrayR.Controls.Add(grfXrayFinish);

            //grfLabOutWait.Rows[0].Visible = false;
            //grfLabOutWait.Cols[0].Visible = false;
            theme1.SetTheme(grfXrayFinish, bc.iniC.themeApp);
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

            pnHn.Controls.Add(grfHn);
            grfHn.Rows[0].Visible = false;
            grfHn.Cols[0].Visible = false;
            theme1.SetTheme(grfHn, "Office2010Blue");

        }
        private void GrfHn_DoubleClick(object sender, EventArgs e)
        {
            //if (grfHn == null) return;
            //if (grfHn.Row <= 0) return;
            //if (grfHn.Col <= 0) return;

            //String hn = "", pttname = "";
            //hn = grfHn[grfHn.Row, colQueHn].ToString();
            //Patient ptt = new Patient();
            //ptt = bc.bcDB.pttDB.selectPatinet(hn);
            ////hn = txtHn.Text;
            //if (ptt.Name.Length <= 0)
            //{
            //    MessageBox.Show("ไม่พบ hn ในระบบ", "");
            //    return;
            //}
            //openNewForm(hn, ptt.Name);
        }
        private void setGrfApmDept()
        {
            if (grfApm != null)
            {
                grfApm.Dispose();
                grfApm = null;
            }
            if (grfHn != null)
            {
                grfHn.Dispose();
                grfHn = null;
            }
            //if (txtHn.Text.Trim().Length <= 0)
            //{
            //    MessageBox.Show("ไม่พบ รหัสแพทย์", "");
            //    return;
            //}
            try
            {
                String datestart = "", dateend = "", hn = "", txt = "", err = "";
                datestart = bc.datetoDB(txtDateStart.Text);
                new LogWriter("d", "FrmNurseScanView setGrfApmDept datestart" + datestart);
                //dateend = bc.datetoDB(txtDateEnd.Text);
                //Staff stf = bc.bcDB.stfDB.selectByLogin(txtHn.Text);
                //if (stf.fullname.Length <= 0)
                //{
                //    MessageBox.Show("ไม่พบ รหัสแพทย์ ในระบบ", "");
                //    return;
                //}
                err = "01";
                //lbName.Text = stf.fullname;
                initGrfApm("วันที่นัด "+ txtDateStart.Text);
                grfApm.Clear();
                grfApm.Font = fEdit;
                grfApm.Rows.Count = 1;
                //grfQue.Rows.Count = 1;
                grfApm.Cols.Count = 12;
                grfApm.Cols[colApmHn].Caption = "HN";
                grfApm.Cols[colApmPttName].Caption = "Patient Name";
                grfApm.Cols[colApmDate].Caption = "Date";
                grfApm.Cols[colApmTime].Caption = "Time";
                grfApm.Cols[colApmSex].Caption = "Sex";
                grfApm.Cols[colApmAge].Caption = "Age";
                grfApm.Cols[colApmDsc].Caption = "Appointment";
                grfApm.Cols[colApmRemark].Caption = "Remark";
                grfApm.Cols[colApmDept].Caption = "Dept";
                grfApm.Cols[colApmVsDate].Caption = "วันที่ทำนัด";

                grfApm.Cols[colApmHn].Width = 80;
                grfApm.Cols[colApmPttName].Width = 300;
                grfApm.Cols[colApmDate].Width = 100;
                grfApm.Cols[colApmTime].Width = 80;
                grfApm.Cols[colApmSex].Width = 80;
                grfApm.Cols[colApmAge].Width = 80;
                grfApm.Cols[colApmDsc].Width = 300;
                grfApm.Cols[colApmRemark].Width = 300;
                grfApm.Cols[colApmDept].Width = 110;
                grfApm.Cols[colApmVsDate].Width = 110;
                err = "02";
                ContextMenu menuGw = new ContextMenu();
                grfApm.ContextMenu = menuGw;
                //String date = "";
                //if (lDgss.Count <= 0) getlBsp();
                //date = txtDate.Text;
                DataTable dt = new DataTable();
                //if (chkHn.Checked)
                //{
                //    dt = bc.bcDB.vsDB.selectVisitByHn(txtHn.Text.Trim(), bc.datetoDB(datestart), bc.datetoDB(dateend));
                //}
                //else
                //{
                //new LogWriter("e", "FrmNurseScanView setGrfApm datestart " + datestart);
                err = "03 datestart " + datestart;
                dt = bc.bcDB.vsDB.selectAppointmentByDepNo(bc.iniC.station, datestart);
                //}
                int i = 1;
                grfApm.Rows.Count = dt.Rows.Count + 1;
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        if (row["MNC_HN_NO"].ToString().Equals("5201798"))
                        {
                            String sql = "";
                        }
                        err = "01 mnc_date "+row["mnc_date"].ToString();
                        DataTable dtapm = new DataTable();
                        DataTable dtitm= new DataTable();
                        dtitm = bc.bcDB.vsDB.selectLabOutbyVN(row["mnc_date"].ToString(), row["MNC_HN_NO"].ToString(), row["mnc_pre_no"].ToString());
                        dtapm = bc.bcDB.vsDB.selectAppointmentByHnDate(row["MNC_HN_NO"].ToString(), row["mnc_date"].ToString());
                        Patient ptt = new Patient();
                        ptt.patient_birthday = row["mnc_bday"].ToString();
                        grfApm[i, 0] = (i);
                        //grfApm[i, colApmHn] = row["MNC_HN_NO"].ToString();
                        grfApm[i, colApmHn] = row["MNC_HN_NO"].ToString();
                        grfApm[i, colApmPttName] = row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                        grfApm[i, colApmDate] = bc.datetoShow(row["mnc_app_dat"].ToString());
                        grfApm[i, colApmTime] = bc.FormatTime(row["mnc_app_tim"].ToString());
                        grfApm[i, colApmSex] = row["mnc_sex"].ToString();
                        grfApm[i, colApmAge] = ptt.AgeStringShort();
                        grfApm[i, colApmDsc] = row["mnc_app_dsc"].ToString();
                        grfApm[i, colApmRemark] = row["MNC_REM_MEMO"].ToString();
                        grfApm[i, colApmDept] = row["mnc_name"].ToString();
                        grfApm[i, colApmVsDate] = bc.datetoShow(row["mnc_date"].ToString());
                        err = "02";
                        String txt1 = "", txt2="";
                        txt1 = "วันที่ทำนัด " + bc.datetoShow(dtapm.Rows[0]["mnc_date"].ToString()) + Environment.NewLine;
                        if (dtitm.Rows.Count > 0)
                        {
                            foreach (DataRow vsrow1 in dtitm.Rows)
                            {
                                DataTable dtlabout = new DataTable();
                                dtlabout = bc.bcDB.laboDB.selectByHnDateReqLabCode(row["MNC_HN_NO"].ToString(),row["mnc_date"].ToString(), vsrow1["MNC_LB_CD"].ToString());
                                if (dtlabout.Rows.Count > 0)
                                {
                                    if (dtlabout.Rows[0]["status_result"].ToString().Equals("1"))
                                    {
                                        txt1 += "      " + vsrow1["MNC_LB_DSC"].ToString() + " -> OK " + Environment.NewLine;
                                    }
                                }
                            }
                            txt2 = "มี outlab";
                            CellNote note2 = new CellNote(txt2);
                            CellRange rg2 = grfApm.GetCellRange(i, colApmSex);
                            rg2.UserData = note2;
                        }
                        err = "03";
                        if (dtapm.Rows.Count > 0)
                        {
                            String txt22 = "";
                            foreach (DataRow drow in dtapm.Rows)
                            {
                                txt1 += "   วันที่นัด " + bc.datetoShow(drow["mnc_app_dat"].ToString()) + " " + drow["mnc_app_dsc"].ToString() + Environment.NewLine;
                                DataTable dtvs = new DataTable();
                                dtvs = bc.bcDB.vsDB.selectLabOutbyVN(drow["mnc_app_dat"].ToString(), drow["MNC_HN_NO"].ToString());
                                if (dtvs.Rows.Count > 0)
                                {
                                    foreach(DataRow vsrow in dtvs.Rows)
                                    {
                                        DataTable dtlabout = new DataTable();
                                        dtlabout = bc.bcDB.laboDB.selectByHnDateReqLabCode(drow["MNC_HN_NO"].ToString(), drow["mnc_app_dat"].ToString(), vsrow["MNC_LB_CD"].ToString());
                                        if (dtlabout.Rows.Count > 0)
                                        {
                                            if (vsrow["status_result"].ToString().Equals("1"))
                                            {
                                                txt1 += "      " + vsrow["MNC_LB_DSC"].ToString() + " -> OK " + Environment.NewLine;
                                            }
                                            else
                                            {
                                                txt1 += "      " + vsrow["MNC_LB_DSC"].ToString() + " -> waiting " + Environment.NewLine;
                                                grfApm.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                                            }
                                        }
                                        else
                                        {
                                            grfApm.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                                            txt1 += "      " + vsrow["MNC_LB_DSC"].ToString() + " -> waiting "+ vsrow["MNC_sch_act"].ToString() + Environment.NewLine;
                                        }
                                    }
                                    txt22 = "มี outlab";
                                    CellNote note22 = new CellNote(txt22);
                                    CellRange rg22 = grfApm.GetCellRange(i, colApmSex);
                                    rg22.UserData = note22;
                                    //grfApm.GetCellRange(i, colApmSex).UserData = new CellNote(txt2);
                                }
                                else
                                {
                                    
                                }
                            }
                        }
                        CellNote1 note = new CellNote1(txt1);
                        CellRange rg = grfApm.GetCellRange(i, colApmHn);
                        rg.UserData = note;
                        
                        //if ((i % 2) == 0)
                        //    grfApm.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        new LogWriter("e", "FrmNurseScanView setGrfApm ex " + ex.Message+" err "+err);
                    }
                }
                CellNoteManager mgr = new CellNoteManager(grfApm);
                CellNoteManager1 mgr1 = new CellNoteManager1(grfApm);
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
                grfApm.Cols[colApmVsDate].AllowEditing = false;
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmNurseScanView setGrfApm ex " + ex.Message);
            }

        }
        private void setGrfApm()
        {
            if (grfApm != null)
            {
                grfApm.Dispose();
                grfApm = null;
            }
            if (grfHn != null)
            {
                grfHn.Dispose();
                grfHn = null;
            }
            if (txtHn.Text.Trim().Length <= 0)
            {
                MessageBox.Show("ไม่พบ รหัสแพทย์", "");
                return;
            }
            try
            {
                String datestart = "", dateend = "", hn = "", txt = "", err="";
                datestart = bc.datetoDB(txtDateStart.Text);
                dateend = bc.datetoDB(txtDateEnd.Text);
                Staff stf = bc.bcDB.stfDB.selectByLogin(txtHn.Text);
                if (stf.fullname.Length <= 0)
                {
                    MessageBox.Show("ไม่พบ รหัสแพทย์ ในระบบ", "");
                    return;
                }
                err = "01";
                lbName.Text = stf.fullname;
                initGrfApm(stf.fullname);
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
                err = "02";
                ContextMenu menuGw = new ContextMenu();
                grfApm.ContextMenu = menuGw;
                //String date = "";
                //if (lDgss.Count <= 0) getlBsp();
                //date = txtDate.Text;
                DataTable dt = new DataTable();
                //if (chkHn.Checked)
                //{
                //    dt = bc.bcDB.vsDB.selectVisitByHn(txtHn.Text.Trim(), bc.datetoDB(datestart), bc.datetoDB(dateend));
                //}
                //else
                //{
                //new LogWriter("e", "FrmNurseScanView setGrfApm datestart " + datestart);
                err = "03 datestart "+ datestart;
                dt = bc.bcDB.vsDB.selectAppointmentByDtr(txtHn.Text.Trim(), datestart);
                //}
                int i = 1;
                grfApm.Rows.Count = dt.Rows.Count + 1;
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        Patient ptt = new Patient();
                        ptt.patient_birthday = bc.datetoDB(row["mnc_bday"].ToString());
                        grfApm[i, 0] = (i);
                        //grfApm[i, colApmHn] = row["MNC_HN_NO"].ToString();
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
                        new LogWriter("e", "FrmNurseScanView setGrfApm ex " + ex.Message);
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
            catch(Exception ex)
            {
                new LogWriter("e", "FrmNurseScanView setGrfApm ex " +ex.Message);
            }
            
        }
        //private void setGrfHn()
        //{
        //    if (grfApm != null)
        //    {
        //        grfApm.Dispose();
        //        grfApm = null;
        //    }
        //    if (grfHn != null)
        //    {
        //        grfHn.Dispose();
        //        grfHn = null;
        //    }
        //    if (txtHn.Text.Trim().Length <= 0)
        //    {
        //        MessageBox.Show("ไม่พบ รหัสแพทย์", "");
        //        return;
        //    }
        //    //initGrfHn();
        //    String datestart = "", dateend = "", hn = "", txt = "";
        //    datestart = bc.datetoDB(txtDateStart.Text);
        //    dateend = bc.datetoDB(txtDateEnd.Text);
        //    Patient ptt1 = bc.bcDB.vsDB.selectPatientName(txtHn.Text);
        //    if (ptt1.Name.Length <= 0)
        //    {
        //        MessageBox.Show("ไม่พบ ข้อมูลคนไข้ ในระบบ", "");
        //        return;
        //    }
        //    lbName.Text = ptt1.Name;
        //    initGrfHn();
        //    grfHn.Clear();
        //    grfHn.Font = fEdit;
        //    grfHn.Rows.Count = 1;
        //    //grfQue.Rows.Count = 1;
        //    grfHn.Cols.Count = 18;
        //    grfHn.Cols[colQueVnShow].Caption = "VN";
        //    grfHn.Cols[colQueHn].Caption = "HN";
        //    grfHn.Cols[colQuePttName].Caption = "Patient Name";
        //    grfHn.Cols[colQueVsDate].Caption = "Date";
        //    grfHn.Cols[colQueVsTime].Caption = "Time";
        //    grfHn.Cols[colQueSex].Caption = "Sex";
        //    grfHn.Cols[colQueAge].Caption = "Age";
        //    grfHn.Cols[colQuePaid].Caption = "Paid";
        //    grfHn.Cols[colQueSymptom].Caption = "Symptom";
        //    grfHn.Cols[colQueHeight].Caption = "Height";
        //    grfHn.Cols[coolQueBw].Caption = "BW";
        //    grfHn.Cols[colQueBp].Caption = "BP";
        //    grfHn.Cols[colQuePulse].Caption = "Pulse";
        //    grfHn.Cols[colQyeTemp].Caption = "Temp";
        //    grfHn.Cols[colQueDsc].Caption = "Description";

        //    grfHn.Cols[colQueVnShow].Width = 80;
        //    grfHn.Cols[colQueHn].Width = 80;
        //    grfHn.Cols[colQuePttName].Width = 310;
        //    grfHn.Cols[colQueVsDate].Width = 110;
        //    grfHn.Cols[colQueVsTime].Width = 80;
        //    grfHn.Cols[colQueSex].Width = 60;
        //    grfHn.Cols[colQueAge].Width = 80;
        //    grfHn.Cols[colQuePaid].Width = 110;
        //    grfHn.Cols[colQueSymptom].Width = 300;
        //    grfHn.Cols[colQueHeight].Width = 60;
        //    grfHn.Cols[coolQueBw].Width = 60;
        //    grfHn.Cols[colQueBp].Width = 60;
        //    grfHn.Cols[colQuePulse].Width = 60;
        //    grfHn.Cols[colQyeTemp].Width = 60;
        //    grfHn.Cols[colQueDsc].Width = 300;

        //    ContextMenu menuGw = new ContextMenu();
        //    grfHn.ContextMenu = menuGw;
        //    //String date = "";
        //    //if (lDgss.Count <= 0) getlBsp();
        //    //date = txtDate.Text;
        //    DataTable dt = new DataTable();
        //    //if (chkHn.Checked)
        //    //{
        //    //    dt = bc.bcDB.vsDB.selectVisitByHn(txtHn.Text.Trim(), bc.datetoDB(datestart), bc.datetoDB(dateend));
        //    //}
        //    //else
        //    //{
        //    dt = bc.bcDB.vsDB.selectVisitByHn(txtHn.Text.Trim(), bc.datetoDB(datestart), bc.datetoDB(dateend));
        //    //}
        //    int i = 1;
        //    grfHn.Rows.Count = dt.Rows.Count + 1;
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        try
        //        {
        //            String status = "", vn = "";
        //            //Patient ptt = new Patient();
        //            //vn = row["MNC_VN_NO"].ToString() + "/" + row["MNC_VN_SEQ"].ToString() + "(" + row["MNC_VN_SUM"].ToString() + ")";
        //            //ptt.patient_birthday = bc.datetoDB(row["mnc_bday"].ToString());
        //            grfHn[i, 0] = (i);
        //            grfHn[i, colQueId] = vn;
        //            grfHn[i, colQueVnShow] = vn;
        //            grfHn[i, colQueVsDate] = bc.datetoShow(row["mnc_date"].ToString());
        //            grfHn[i, colQueHn] = row["MNC_HN_NO"].ToString();
        //            grfHn[i, colQuePttName] = row["prefix"].ToString() + "" + row["MNC_FNAME_T"].ToString() + "" + row["MNC_LNAME_T"].ToString();
        //            grfHn[i, colQueVsTime] = bc.FormatTime(row["mnc_time"].ToString());
        //            //grfHn[i, colQueVnShow] = row["MNC_REQ_DAT"].ToString();
        //            grfHn[i, colQueSex] = row["mnc_sex"].ToString();
        //            //grfHn[i, colQueAge] = ptt.AgeStringShort();
        //            grfHn[i, colQuePaid] = row["MNC_FN_TYP_DSC"].ToString();
        //            grfHn[i, colQueSymptom] = row["MNC_SHIF_MEMO"].ToString();
        //            grfHn[i, colQueHeight] = row["mnc_high"].ToString();
        //            grfHn[i, coolQueBw] = row["mnc_weight"].ToString();
        //            grfHn[i, colQueBp] = row["mnc_bp1_l"].ToString();
        //            grfHn[i, colQuePulse] = row["MNC_ratios"].ToString();
        //            grfHn[i, colQyeTemp] = row["MNC_temp"].ToString();
        //            grfHn[i, colQuePreNo] = row["MNC_pre_no"].ToString();
        //            grfHn[i, colQueDsc] = row["MNC_ref_dsc"].ToString();
        //            if ((i % 2) == 0)
        //                grfHn.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);

        //            i++;
        //        }
        //        catch (Exception ex)
        //        {
        //            new LogWriter("e", "FrmDoctorView setGrfQue ex " + ex.Message);
        //        }
        //    }
        //    grfHn.Cols[colApmId].Visible = false;

        //    grfHn.Cols[colApmHn].AllowEditing = false;
        //    grfHn.Cols[colApmPttName].AllowEditing = false;
        //    grfHn.Cols[colApmDate].AllowEditing = false;
        //    grfHn.Cols[colApmTime].AllowEditing = false;
        //    grfHn.Cols[colApmSex].AllowEditing = false;
        //    grfHn.Cols[colApmAge].AllowEditing = false;
        //    grfHn.Cols[colApmDsc].AllowEditing = false;
        //    grfHn.Cols[colApmRemark].AllowEditing = false;
        //    grfHn.Cols[colApmDept].AllowEditing = false;
        //}
        private void FrmNurseScanView_Load(object sender, EventArgs e)
        {
            this.Text = "Last Update 2020-09-16 date "+DateTime.Now.Day.ToString()+" month ";
            pnLabOut.Size = new Size(this.Width / 2, this.Height);
            Size pnlsize = new Size(pnLabOut.Width / 2, pnLabOut.Height);
            spPttinWrd.Size = pnlsize;
            spLabOutR.Size = pnlsize;
            tC1.SelectedTab = tabPttinWrd;
            if (bc.iniC.station.Equals("101") || bc.iniC.station.Equals("107") || bc.iniC.station.Equals("103"))
            {
                tC1.TabPages[0].Hide();
            }
            //pnXray.Size = new Size(this.Width / 2, this.Height);
            //pnXrayL.Size = pnlsize;
            //pnXrayR.Size = pnlsize;
            frmFlash.Dispose();
        }
    }
}
