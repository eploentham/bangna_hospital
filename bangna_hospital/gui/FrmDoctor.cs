using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using GrapeCity.ActiveReports.Document.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CSJ2K.j2k.codestream.HeaderInfo;

namespace bangna_hospital.gui
{
    public partial class FrmDoctor : Form
    {
        BangnaControl bc;
        Login login;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Patient PTT;
        Visit VS;
        PatientT07 APM;
        C1ThemeController theme1;

        private Point _imageLocation = new Point(13, 5);
        private Point _imgHitArea = new Point(13, 2);
        Image CloseImage;
        C1FlexGrid grfQue, grfApm, grfRpt, grfFin, grfIPD;
        Label lbLoading;

        int colQueId = 1, colQueVnShow = 2, colQueHn = 3, colQuePttName = 4, colQueVsDateShow = 5, colQueVsTime = 6, colQueSex = 7, colQueAge = 8, colQuePaid = 9, colQueSymptom = 10, colQueHeight = 11, coolQueBw = 12, colQueBp = 13, colQuePulse = 14, colQyeTemp = 15, colQuePreNo = 16, colQueDsc = 17, colQueVsDate = 18;
        int colApmId = 1, colApmVnShow = 2, colApmHn = 3, colApmPttName = 4, colApmDateShow = 5, colApmTime = 6, colApmSex = 7, colApmAge = 8, colApmDsc = 9, colApmRemark = 10, colApmDept = 11, colApmDate = 12, colApmPreNo = 13, colApmVsDate = 14;
        int colFinId = 1, colFinVnShow = 2, colFinHn = 3, colFinPttName = 4, colFinDateShow = 5, colFinTime = 6, colFinSex = 7, colFinAge = 8, colDFdtrcode=9, colDFDate = 10, colDFFNDate = 11, colFinPaidName = 12, colFinRemark = 13, colFinDept = 14, colFinWrd = 15, colFinAn = 16, colFinVsDate = 17, colFinPreno = 18, colFinSecNo=19, colFinDeptNo=20, colFinPATFLAG=21, colFinWdno = 21;
        int colIPDId = 1, colIPDVnShow = 2, colIPDHn = 3, colIPDAn = 4, colIPDPttName = 5, colIPDDate = 6, colIPDTime = 7, colIPDSex = 8, colIPDAge = 9, colIPDDsc = 10, colIPDRemark = 11, colIPDDept = 12, colIPDWrd = 13;

        int rowindexgrfRpt = 0;
        String DTRCODE = "", DEPTNO = "", TC1ACTIVE = "", RPTSELECT="";

        Boolean flagExit = false;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        System.Windows.Forms.Timer timer1;
        public FrmDoctor(ref BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            login = new Login(ref bc);
            login.ShowDialog(this);
            if (login.LogonSuccessful.Equals("1"))
            {
                initConfig();
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    /* run your code here */
                    lbDtrName.BeginInvoke(new Action(() =>
                    {
                        //label1.Text = bc.user.fullname;
                    }));
                }).Start();
            }
            else
            {
                Application.Exit();
            }
        }
        private void initConfig()
        {
            stt = new C1SuperTooltip();
            theme1 = new C1ThemeController();
            theme1.Theme = bc.iniC.themeApplication;
            lbDtrName.Text = bc.user.fullname;
            DTRCODE = bc.user.username;//เปิดโปรแกรม login ด้วย แพทย์ ถือว่าเป็น แพทย์
            txtDate.Value = DateTime.Now;

            setEvent();


            initFont();
            initLoading();
            initGrfApm();
            initGrfQue();
            initGrfFinish();
            initGrfRpt();
            initGrfIPD();
            theme1.SetTheme(pnTop, "Office2010Green");
            tC1.SelectedTab = tabQue;
            TC1_SelectedTabChanged(null, null);

            lbDtrName.Text = bc.user.fullname;
            DTRCODE = bc.user.username;//เปิดโปรแกรม login ด้วย แพทย์ ถือว่าเป็น แพทย์
            
        }
        private void setEvent()
        {
            tC1.SelectedTabChanged += TC1_SelectedTabChanged;
            txtHN.KeyUp += TxtHN_KeyUp;
            txtHN.KeyPress += TxtHN_KeyPress;
            rbSbDrugSet.Click += RbSbDrugSet_Click;
            rpDrugSetNew.Click += RpDrugSetNew_Click;
            txtDate.DropDownClosed += TxtDate_DropDownClosed;
            btnDfDate.Click += BtnDfDate_Click;
            txtDate.ValueChanged += TxtDate_ValueChanged;
            btnRptPrint.Click += BtnRptPrint_Click;
        }
        private void BtnRptPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void TxtDate_ValueChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (TC1ACTIVE.Equals(tabFinish.Name))
            {
                setGrfFinish();
            }
        }

        private void BtnDfDate_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (TC1ACTIVE.Equals(tabFinish.Name) && grfFin.Rows.Count>0)
            {
                showLbLoading();
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmDoctor", "BtnDfDate_Click");
                int cnt = 0;
                foreach (C1.Win.C1FlexGrid.Row arow in grfFin.Rows)
                {
                    if (arow[colFinHn].ToString().Equals("HN")) continue;
                    DataTable dt = bc.bcDB.dfdDB.SelectDtrDFByPreno(arow[colFinHn].ToString(), arow[colFinVsDate].ToString(), arow[colFinPreno].ToString());
                    if(dt.Rows.Count> 0)
                    {
                        arow[colDFdtrcode] = dt.Rows[0]["MNC_DOT_CD_DF"].ToString();
                        arow[colDFDate] = dt.Rows[0]["DF_DATE"].ToString();
                        arow[colDFFNDate] = dt.Rows[0]["FN_DAT"].ToString();
                    }
                    else
                    {

                        arow.StyleNew.BackColor= ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                        cnt++;
                    }
                }
                lfSbMessage.Text = "ไม่พบ "+cnt.ToString();
                hideLbLoading();
            }
        }
        private void TxtDate_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            if (TC1ACTIVE.Equals(tabFinish.Name))
            {
                setGrfFinish();
            }
        }
        private void RpDrugSetNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmDoctorDrugSet1 frmDrugSet = new FrmDoctorDrugSet1(bc, DTRCODE);
            frmDrugSet.WindowState = FormWindowState.Normal;
            frmDrugSet.StartPosition = FormStartPosition.CenterScreen;
            frmDrugSet.Size = new Size(1600, 800);

            frmDrugSet.ShowDialog(this);
        }
        private void RbSbDrugSet_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmDoctorDrugSet frmDrugSet = new FrmDoctorDrugSet(bc, txtHN.Text.Trim());
            frmDrugSet.WindowState = FormWindowState.Normal;
            frmDrugSet.StartPosition = FormStartPosition.CenterScreen;
            frmDrugSet.Size = new Size(1200, 800);

            frmDrugSet.ShowDialog(this);
        }
        private void TxtHN_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private void TxtHN_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if(txtHN.Text.Length<=0) return;
                //if(txtHN.Text.)
                setSearch();
            }
        }
        private void setSearch()
        {
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetByHn(txtHN.Text.Trim());
            //showFormWaiting();
            String allergy = "";
            if (ptt.Name.Length <= 0)
            {
                //frmFlash.Dispose();
                MessageBox.Show("ไม่พบ hn ในระบบ", "");
                return;
            }
            //lbPttName.Text = ptt.Name;
            openNewForm(txtHN.Text.Trim(),"",ref ptt);

            //frmFlash.Dispose();
        }
        private void openNewForm(String hn,String txt,ref Patient ptt)
        {
            //showFormWaiting();
            FrmPatient frm;
            if (ptt != null)
            {
                frm = new FrmPatient(bc, DTRCODE,ref ptt);
            }
            else
            {
                frm = new FrmPatient(bc, DTRCODE);
            }
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show(this);

            txtHN.Value = "";
            //frmFlash.Dispose();
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
        private void TC1_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tC1.SelectedTab == tabQue){TC1ACTIVE = tabQue.Name; setGrfQue();}
            else if (tC1.SelectedTab == tabApm){TC1ACTIVE = tabApm.Name; setGrfApm();}
            else if (tC1.SelectedTab == tabIPD){TC1ACTIVE = tabIPD.Name; setGrfIPD();}
            else if (tC1.SelectedTab == tabFinish){stt.Show("ดึงข้อมูลตาม ว.แพทย์ของวันนี้??", tabFinish, 5); TC1ACTIVE = tabFinish.Name;setGrfFinish();
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmDoctor", "TC1_SelectedTabChanged");
            }
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
        private void initGrfIPD()
        {
            grfIPD = new C1FlexGrid();
            grfIPD.Font = fEdit;
            grfIPD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfIPD.Location = new System.Drawing.Point(0, 0);
            grfIPD.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);
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
            grfIPD.Cols[colIPDId].Visible = false;
            grfIPD.Cols[0].Visible = true;
            grfIPD.Rows[0].Visible = true;

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

            grfIPD.DoubleClick += GrfIPD_DoubleClick;

            pnIPD.Controls.Add(grfIPD);
            //grfIPD.Rows[0].Visible = false;
            //grfIPD.Cols[0].Visible = false;
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
            Patient ptt = bc.bcDB.pttDB.selectPatinetByHn(hn);
            openNewForm(hn, txt,ref ptt);
        }
        private void setGrfIPD()
        {
            //grfIPD.Clear();
            grfIPD.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
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
            
            //grfIPD.Cols[colIPDDsc].Visible = false;
        }
        private void initGrfFinish()
        {
            grfFin = new C1FlexGrid();
            grfFin.Font = fEdit;
            grfFin.Dock = System.Windows.Forms.DockStyle.Fill;
            grfFin.Location = new System.Drawing.Point(0, 0);
            grfFin.Rows.Count = 1;
            grfFin.Cols.Count = 23;
            grfFin.DoubleClick += GrfFin_DoubleClick;

            pnFinish.Controls.Add(grfFin);
            
            grfFin.Cols[colFinHn].Caption = "HN";
            grfFin.Cols[colFinVnShow].Caption = "VN/AN";
            grfFin.Cols[colFinPttName].Caption = "Patient Name";
            grfFin.Cols[colFinDateShow].Caption = "VSDate";
            grfFin.Cols[colFinTime].Caption = "Time";
            grfFin.Cols[colFinSex].Caption = "Sex";
            grfFin.Cols[colFinAge].Caption = "Age";
            grfFin.Cols[colFinPaidName].Caption = "Description";
            grfFin.Cols[colFinRemark].Caption = "Symptom";
            grfFin.Cols[colFinDept].Caption = "Dept";
            grfFin.Cols[colDFdtrcode].Caption = "dtrcode";
            grfFin.Cols[colDFDate].Caption = "df date";
            grfFin.Cols[colDFFNDate].Caption = "fn date";
            grfFin.Cols[colFinPATFLAG].Caption = "OPD/IPD";

            grfFin.Cols[colFinHn].Width = 80;
            grfFin.Cols[colFinVnShow].Width = 80;
            grfFin.Cols[colFinPttName].Width = 300;
            grfFin.Cols[colFinDateShow].Width = 90;
            grfFin.Cols[colFinTime].Width = 80;
            grfFin.Cols[colFinSex].Width = 80;
            grfFin.Cols[colFinAge].Width = 80;
            grfFin.Cols[colFinPaidName].Width = 100;
            grfFin.Cols[colFinRemark].Width = 300;
            grfFin.Cols[colFinDept].Width = 110;
            grfFin.Cols[colDFdtrcode].Width = 80;
            grfFin.Cols[colDFDate].Width = 90;
            grfFin.Cols[colDFFNDate].Width = 90;

            grfFin.Cols[colFinPreno].Visible = false;
            grfFin.Cols[colFinVsDate].Visible = false;
            grfFin.Cols[colFinAn].Visible = false;
            grfFin.Cols[colFinWdno].Visible = false;
            grfFin.Cols[colFinId].Visible = false;
            grfFin.Cols[colFinPaidName].Visible = true;
            grfFin.Cols[colFinDept].Visible = false;
            grfFin.Cols[colFinWrd].Visible = false;
            grfFin.Cols[colFinDeptNo].Visible = false;
            grfFin.Cols[colFinSecNo].Visible = false;
            grfFin.Cols[colFinPATFLAG].Visible = true;

            grfFin.Cols[colFinHn].AllowEditing = false;
            grfFin.Cols[colFinVnShow].AllowEditing = false;
            grfFin.Cols[colFinPttName].AllowEditing = false;
            grfFin.Cols[colFinDateShow].AllowEditing = false;
            grfFin.Cols[colFinTime].AllowEditing = false;
            grfFin.Cols[colFinSex].AllowEditing = false;
            grfFin.Cols[colFinAge].AllowEditing = false;
            grfFin.Cols[colFinPaidName].AllowEditing = false;
            grfFin.Cols[colFinRemark].AllowEditing = false;
            grfFin.Cols[colFinDept].AllowEditing = false;
            grfFin.Cols[colDFdtrcode].AllowEditing = false;
            grfFin.Cols[colDFDate].AllowEditing = false;
            grfFin.Cols[colDFFNDate].AllowEditing = false;

            grfFin.Cols[colFinSecNo].AllowEditing = false;
            grfFin.Cols[colFinDeptNo].AllowEditing = false;
            grfFin.Cols[colFinPATFLAG].AllowEditing = false;

            ContextMenu menuGw = new ContextMenu();
            grfFin.ContextMenu = menuGw;

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
            Patient ptt = bc.bcDB.pttDB.selectPatinetByHn(hn);
            openNewForm(hn, txt,ref ptt);
        }
        private void setGrfFinish()
        {
            //grfFin.Clear();
            showLbLoading();
            String date = "";
            DateTime.TryParse(txtDate.Text, out DateTime curdate);
            if (curdate.Year < 1900)
            {
                curdate = curdate.AddYears(543);
            }
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectVisitByDtr2(bc.user.staff_id, curdate.Year.ToString()+"-"+ curdate.ToString("MM-dd"));
            int i = 1, cnt=0;
            grfFin.Rows.Count = 1; grfFin.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    String status = "", vn = "";
                    Patient ptt = new Patient();
                    ptt.patient_birthday = row["mnc_bday"].ToString();
                    grfFin[i, 0] = (i);
                    grfFin[i, colFinHn] = row["MNC_HN_NO"].ToString();
                    grfFin[i, colFinVnShow] = row["MNC_VN_NO"].ToString() + "." + row["MNC_VN_SEQ"].ToString() + "." + row["MNC_VN_SUM"].ToString();
                    grfFin[i, colFinPttName] = row["pttfullname"].ToString();
                    grfFin[i, colFinDateShow] = bc.datetoShow(row["mnc_date"].ToString());
                    grfFin[i, colFinTime] = bc.showTime(row["mnc_time"].ToString());
                    grfFin[i, colFinSex] = row["mnc_sex"].ToString();
                    grfFin[i, colFinAge] = ptt.AgeStringOK1DOT();
                    grfFin[i, colFinPaidName] = row["MNC_FN_TYP_DSC"].ToString()+"["+ row["MNC_FN_TYP_CD"].ToString()+"]";
                    grfFin[i, colFinRemark] = row["MNC_SHIF_MEMO"].ToString();
                    //grfFin[i, colFinDept] = row["MNC_MD_DEP_DSC1"].ToString();

                    grfFin[i, colFinPATFLAG] = row["MNC_PAT_FLAG"].ToString();
                    grfFin[i, colFinDeptNo] = row["MNC_DEP_NO"].ToString();
                    grfFin[i, colFinSecNo] = row["MNC_SEC_NO"].ToString();
                    grfFin[i, colFinPreno] = row["MNC_PRE_NO"].ToString();
                    grfFin[i, colFinVsDate] = row["MNC_DATE"].ToString();
                    grfFin[i, colFinWdno] = row["MNC_WD_NO"].ToString();

                    grfFin[i, colDFdtrcode] = row["MNC_WD_NO"].ToString();
                    grfFin[i, colDFDate] = row["MNC_WD_NO"].ToString();
                    grfFin[i, colDFFNDate] = row["MNC_WD_NO"].ToString();
                    if (!row["MNC_VN_SEQ"].ToString().Equals("1"))
                    {
                        row.SetColumnError(colFinVnShow, row["MNC_VN_NO"].ToString() + "." + row["MNC_VN_SEQ"].ToString() + "." + row["MNC_VN_SUM"].ToString());

                    }
                    if (row["MNC_PAT_FLAG"].ToString().Equals("I")) 
                    {
                        grfFin.GetCellRange(i, colFinVnShow,i, colFinPttName).StyleNew.Font = fEditB; 
                        grfFin[i, colFinVnShow] = row["MNC_AN_NO"].ToString() + "." + row["MNC_AN_YR"].ToString();
                        grfFin[i, colFinPATFLAG] = "ADMIT";
                        CellNote note = new CellNote(row["MNC_VN_NO"].ToString() + "." + row["MNC_VN_SEQ"].ToString() + "." + row["MNC_VN_SUM"].ToString());
                        CellRange rg = grfFin.GetCellRange(i, colFinVnShow);
                        rg.UserData = note;
                    }
                    else { }
                    i++;
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmDoctor setGrfFinish " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setGrfFinish", ex.Message);
                }
            }
            CellNoteManager mgr = new CellNoteManager(grfFin);
            hideLbLoading();
        }
        private void initGrfQue()
        {
            grfQue = new C1FlexGrid();
            grfQue.Rows.Count = 1;
            grfQue.Cols.Count = 19;
            grfQue.Dock = DockStyle.Fill;
            grfQue.Font = fEdit;

            grfQue.Cols[colQueId].Caption = "que";
            grfQue.Cols[colQueVnShow].Caption = "VN";
            grfQue.Cols[colQueHn].Caption = "HN";
            grfQue.Cols[colQuePttName].Caption = "Patient Name";
            grfQue.Cols[colQueVsDateShow].Caption = "Date";
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

            grfQue.Cols[colQueId].Width = 50;
            grfQue.Cols[colQueVnShow].Width = 80;
            grfQue.Cols[colQueHn].Width = 80;
            grfQue.Cols[colQuePttName].Width = 310;
            grfQue.Cols[colQueVsDateShow].Width = 110;
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
            grfQue.Cols[colQueVsDate].Visible = false;
            grfQue.ShowCursor = true;

            grfQue.Cols[0].Visible = true;
            grfQue.Rows[0].Visible = true;
            //row1[colVSE2] = row[ic.ivfDB.pApmDB.pApm.e2].ToString().Equals("1") ? imgCorr : imgTran;
            grfQue.Cols[colQueId].Visible = true;//ต้องการแสดง que
            grfQue.Cols[colQuePreNo].Visible = false;

            grfQue.Cols[colQueId].AllowEditing = false;
            grfQue.Cols[colQueVnShow].AllowEditing = false;
            grfQue.Cols[colQueHn].AllowEditing = false;
            grfQue.Cols[colQuePttName].AllowEditing = false;
            grfQue.Cols[colQueVsDateShow].AllowEditing = false;
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
            pnQue.Controls.Add(grfQue);
            theme1.SetTheme(grfQue, "Office2010Red");
            grfQue.DoubleClick += GrfQue_DoubleClick;
        }
        private void GrfQue_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;
            String vsdate = "", preno = "", hn = "", name = "";
            hn = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQueHn] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQueHn].ToString() : "";
            vsdate = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQueVsDate] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQueVsDate].ToString() : "";
            preno = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQuePreNo] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQuePreNo].ToString() : "";
            name = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQuePttName] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQuePttName].ToString() : "";
            Patient ptt = bc.bcDB.pttDB.selectPatinetByHn(hn);
            bc.bcDB.sumt03DB.updateSumVnAdd(DTRCODE, hn, preno, vsdate);
            openNewForm(hn, name,ref ptt);
        }
        private void setGrfQue()
        {
            DataTable  dt = bc.bcDB.vsDB.selectVisitByDtr(bc.user.staff_id,"", "queue");
            //new LogWriter("d", "FrmDoctorView1 setGrfQue 03 ");
            int i = 1;
            grfQue.Rows.Count = 1; grfQue.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    String status = "", vn = "";
                    Patient ptt = new Patient();
                    vn = row["MNC_VN_NO"].ToString() + "." + row["MNC_VN_SEQ"].ToString() + "." + row["MNC_VN_SUM"].ToString();
                    ptt.patient_birthday = row["mnc_bday"].ToString();
                    grfQue[i, 0] = (i);
                    grfQue[i, colQueId] = row["MNC_QUE_NO"].ToString();
                    grfQue[i, colQueVnShow] = vn;
                    grfQue[i, colQueVsDateShow] = bc.datetoShow(row["mnc_date"].ToString());
                    grfQue[i, colQueHn] = row["MNC_HN_NO"].ToString();
                    grfQue[i, colQuePttName] = row["pttfullname"].ToString();
                    grfQue[i, colQueVsTime] = bc.showTime(row["mnc_time"].ToString());
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
                    grfQue[i, colQueVsDate] = row["mnc_date"].ToString();
                    if ((i % 2) == 0) grfQue.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                    i++;
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmDoctorView setGrfQue ex " + ex.Message);
                }
            }
        }
        private void initGrfApm()
        {
            grfApm = new C1FlexGrid();
            grfApm.Rows.Count = 1;
            grfApm.Dock = DockStyle.Fill;
            grfApm.Font = fEdit;
            //grfQue.Rows.Count = 1;
            grfApm.Cols.Count = 15;
            grfApm.Cols[colApmHn].Caption = "HN";
            grfApm.Cols[colApmVnShow].Caption = "VN";
            grfApm.Cols[colApmPttName].Caption = "Patient Name";
            grfApm.Cols[colApmDateShow].Caption = "Date";
            grfApm.Cols[colApmTime].Caption = "Time";
            grfApm.Cols[colApmSex].Caption = "Sex";
            grfApm.Cols[colApmAge].Caption = "Age";
            grfApm.Cols[colApmDsc].Caption = "Appointment";
            grfApm.Cols[colApmRemark].Caption = "Remark";
            grfApm.Cols[colApmDept].Caption = "Dept";

            grfApm.Cols[colApmHn].Width = 80;
            grfApm.Cols[colApmVnShow].Width = 80;
            grfApm.Cols[colApmPttName].Width = 300;
            grfApm.Cols[colApmDateShow].Width = 100;
            grfApm.Cols[colApmTime].Width = 80;
            grfApm.Cols[colApmSex].Width = 80;
            grfApm.Cols[colApmAge].Width = 80;
            grfApm.Cols[colApmDsc].Width = 300;
            grfApm.Cols[colApmRemark].Width = 300;
            grfApm.Cols[colApmDept].Width = 110;

            grfApm.Cols[colApmId].Visible = false;
            grfApm.Cols[0].Visible = true;
            grfApm.Rows[0].Visible = true;
            grfApm.Cols[colApmDate].Visible = false;
            grfApm.Cols[colApmPreNo].Visible = false;
            grfApm.Cols[colApmVsDate].Visible = false;
            grfApm.Cols[colApmHn].AllowEditing = false;
            grfApm.Cols[colApmVnShow].Visible = false;
            grfApm.Cols[colApmPttName].AllowEditing = false;
            grfApm.Cols[colApmDateShow].AllowEditing = false;
            grfApm.Cols[colApmTime].AllowEditing = false;
            grfApm.Cols[colApmSex].AllowEditing = false;
            grfApm.Cols[colApmAge].AllowEditing = false;
            grfApm.Cols[colApmDsc].AllowEditing = false;
            grfApm.Cols[colApmRemark].AllowEditing = false;
            grfApm.Cols[colApmDept].AllowEditing = false;
            pnApm.Controls.Add(grfApm);
            theme1.SetTheme(grfApm, "Office2010Blue");
            grfApm.DoubleClick += GrfApm_DoubleClick;
        }
        private void GrfApm_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;
            String vsdate = "", preno = "", hn = "", name="";
            hn = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colApmHn] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colApmHn].ToString() : "";
            vsdate = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colApmVsDate] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colApmVsDate].ToString() : "";
            preno = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colApmPreNo] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colApmPreNo].ToString() : "";
            name = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colApmPttName] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colApmPttName].ToString() : "";
            Patient ptt = bc.bcDB.pttDB.selectPatinetByHn(hn);
            openNewForm(hn, name,ref ptt);
        }
        private void setGrfApm()
        {
            DateTime.TryParse(txtDate.Text, out DateTime dtdate);
            if (dtdate.Year < 1900)
            {
                dtdate = dtdate.AddYears(543);
            }
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectAppointmentByDtr(bc.user.staff_id, dtdate.Year.ToString() + "-" + dtdate.ToString("MM-dd"));
            int i = 1;
            grfApm.Rows.Count = 1; grfApm.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    String status = "", vn = "";
                    Patient ptt = new Patient();
                    ptt.patient_birthday = row["mnc_bday"].ToString();
                    vn = row["MNC_VN_NO"].ToString() + "/" + row["MNC_VN_SEQ"].ToString() + "(" + row["MNC_VN_SUM"].ToString() + ")";
                    grfApm[i, 0] = (i);
                    grfApm[i, colApmHn] = row["MNC_HN_NO"].ToString();
                    grfApm[i, colApmVnShow] = vn;
                    grfApm[i, colApmPttName] = row["pttfullname"].ToString();
                    grfApm[i, colApmDateShow] = bc.datetoShow(row["mnc_app_dat"].ToString());
                    grfApm[i, colApmTime] = bc.FormatTime(row["mnc_app_tim"].ToString());
                    grfApm[i, colApmSex] = row["mnc_sex"].ToString();
                    grfApm[i, colApmAge] = ptt.AgeStringShort();
                    grfApm[i, colApmDsc] = row["mnc_app_dsc"].ToString();
                    grfApm[i, colApmRemark] = row["MNC_REM_MEMO"].ToString();
                    grfApm[i, colApmDept] = row["mnc_name"].ToString();
                    grfApm[i, colApmDate] = row["mnc_app_dat"].ToString();
                    grfApm[i, colApmPreNo] = row["MNC_PRE_NO"].ToString();
                    grfApm[i, colApmVsDate] = row["MNC_DATE"].ToString();
                    i++;
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmDoctorView setGrfApm ex " + ex.Message);
                }
            }
        }
        private void initGrfRpt()
        {
            grfRpt = new C1FlexGrid();
            grfRpt.Font = fEdit;
            grfRpt.Dock = System.Windows.Forms.DockStyle.Fill;
            grfRpt.Location = new System.Drawing.Point(0, 0);
            grfRpt.Rows.Count = 1;
            grfRpt.Cols.Count = 2;
            grfRpt.Cols[1].Width = 300;

            grfRpt.ShowCursor = true;
            grfRpt.Cols[1].Caption = "HN";

            grfRpt.Cols[1].DataType = typeof(String);
            grfRpt.Cols[1].TextAlign = TextAlignEnum.LeftCenter;
            grfRpt.Cols[1].Visible = true;
            grfRpt.Cols[1].AllowEditing = false;
            grfRpt.AfterRowColChange += GrfRpt_AfterRowColChange;
            grfRpt.Rows.Count = 3;
            grfRpt.Rows[1][1] = "รายงาน แพทย์นัด";
            grfRpt.Rows[2][1] = "รายงาน รายได้แพทย์";
            pnRptName.Controls.Add(grfRpt);
            theme1.SetTheme(grfRpt, bc.iniC.themeApp);
        }

        private void GrfRpt_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            try
            {
                if (rowindexgrfRpt != ((C1FlexGrid)(sender)).Row) { rowindexgrfRpt = ((C1FlexGrid)(sender)).Row; }
                else { return; }
                if (grfRpt.Row ==1) RPTSELECT = "01";
                else if (grfRpt.Row == 2) RPTSELECT = "02";
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmDoctor GrfRpt_AfterRowColChange " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmDoctor GrfRpt_AfterRowColChange  ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
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
        private void FrmDoctor_Load(object sender, EventArgs e)
        {
            String stationname = bc.bcDB.pm32DB.getDeptName(bc.iniC.station);
            DEPTNO = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            
            spDoctor.HeaderHeight = 0;
            spTop.SizeRatio = 5;
            spRpt.HeaderHeight = 0;
            lfSbStation.Text = DEPTNO + "[" + bc.iniC.station + "]" + stationname;
            rgSbModule.Text = bc.iniC.hostDBMainHIS + " " + bc.iniC.nameDBMainHIS;
            //theme1.SetTheme(this, "Office2010Blue");
            this.Text = "Last Update 2024-03-21";
            lfSbLastUpdate.Text = "Update 2567-03-21-1";
            lfSbMessage.Text = "";
            bc.bcDB.insertLogPage(bc.userId, this.Name, "FrmDoctor_Load", "Application Doctor Start");
        }
    }
}
