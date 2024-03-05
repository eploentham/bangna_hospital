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
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using GrapeCity.ActiveReports.Extensions;

namespace bangna_hospital.gui
{
    public partial class FrmDoctorView : Form
    {
        BangnaControl bc;
        Login login;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Patient PTT;
        Visit VS;
        PatientT07 APM;
        C1ThemeController theme1;
        C1FlexGrid grfRpt;

        private Point _imageLocation = new Point(13, 5);
        private Point _imgHitArea = new Point(13, 2);
        Image CloseImage;
        C1FlexGrid grfQue, grfApm;
        Label lbLoading;

        int colQueId = 1, colQueVnShow = 2, colQueHn = 3, colQuePttName = 4, colQueVsDateShow = 5, colQueVsTime = 6, colQueSex = 7, colQueAge = 8, colQuePaid = 9, colQueSymptom = 10, colQueHeight = 11, coolQueBw = 12, colQueBp = 13, colQuePulse = 14, colQyeTemp = 15, colQuePreNo = 16, colQueDsc = 17, colQueVsDate = 18;
        int colApmId = 1, colApmVnShow = 2, colApmHn = 3, colApmPttName = 4, colApmDateShow = 5, colApmTime = 6, colApmSex = 7, colApmAge = 8, colApmDsc = 9, colApmRemark = 10, colApmDept = 11, colApmDate = 12, colApmPreNo = 13, colApmVsDate=14;
        int colFinId = 1, colFinVnShow = 2, colFinHn = 3, colFinPttName = 4, colFinDate = 5, colFinTime = 6, colFinSex = 7, colFinAge = 8, colFinDsc = 9, colFinRemark = 10, colFinDept = 11, colFinWrd = 12, colFinAn = 13;
        int colIPDId = 1, colIPDVnShow = 2, colIPDHn = 3, colIPDAn = 4, colIPDPttName = 5, colIPDDate = 6, colIPDTime = 7, colIPDSex = 8, colIPDAge = 9, colIPDDsc = 10, colIPDRemark = 11, colIPDDept = 12, colIPDWrd = 13;

        String DTRCODE = "";

        Boolean flagExit = false;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        System.Windows.Forms.Timer timer1;
        public FrmDoctorView(BangnaControl bc, FrmSplash splash)
        {
            InitializeComponent();
            this.bc = bc;
            login = new Login(bc, splash);
            login.ShowDialog(this);
            if (login.LogonSuccessful.Equals("1"))
            {
                initConfig();
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    /* run your code here */
                    lbDtrName.Text = bc.user.fullname;
                }).Start();
            }
            else
            {
                Application.Exit();
            }
        }
        private void initConfig()
        {
            theme1 = new C1ThemeController();
            theme1.Theme = bc.iniC.themeApplication;

            tC1.SelectedTabChanged += TC1_SelectedTabChanged;
            initLoading();
            initGrfApm();
            initGrfQue();
            initGrfRpt();
        }
        private void TC1_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tC1.SelectedTab == tabQue)
            {
                setGrfQue();
            }
            else if(tC1.SelectedTab == tabApm){
                setGrfApm();
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
        private void initGrfQue()
        {
            grfQue = new C1FlexGrid();
            grfQue.Rows.Count = 1;
            grfQue.Cols.Count = 19;

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
            grfQue.Cols[colQueId].Visible = false;
            grfQue.Cols[colQuePreNo].Visible = false;

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

            grfQue.DoubleClick += GrfQue_DoubleClick;
        }

        private void GrfQue_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;
            String vsdate = "", preno = "", hn = "";
            hn = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQueHn] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQueHn].ToString() : "";
            vsdate = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQueVsDate] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQueVsDate].ToString() : "";
            preno = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQuePreNo] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQuePreNo].ToString() : "";

        }
        private void setGrfQue()
        {
            DateTime.TryParse(txtPttApmDate.Text, out DateTime dtdate);
            if (dtdate.Year < 1900)
            {
                dtdate.AddYears(543);
            }
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectVisitByDtr(bc.user.staff_id, dtdate.Year.ToString() + "-" + dtdate.ToString("MM-dd"), "queue");
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
                    grfQue[i, colQueId] = vn;
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

            grfApm.DoubleClick += GrfApm_DoubleClick;
        }

        private void GrfApm_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;
            String vsdate = "", preno = "", hn = "";
            hn = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colApmHn] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colApmHn].ToString() : "";
            vsdate = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colApmVsDate] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colApmVsDate].ToString() : "";
            preno = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQuePreNo] != null ? ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colQuePreNo].ToString() : "";

        }

        private void setGrfApm()
        {
            DateTime.TryParse(txtPttApmDate.Text, out DateTime dtdate);
            if (dtdate.Year < 1900)
            {
                dtdate.AddYears(543);
            }
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectAppointmentByDtr(bc.user.staff_id, dtdate.Year.ToString()+"-"+ dtdate.ToString("MM-dd"));
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
            grfRpt.DoubleClick += GrfRpt_DoubleClick1;
            //grfCheckUPList.AllowFiltering = true;
            grfRpt.Rows.Count = 2;
            Row rowa = grfRpt.Rows[1];
            rowa[1] = "รายงาน แพทย์นัด";

            pnRptName.Controls.Add(grfRpt);
            theme1.SetTheme(grfRpt, bc.iniC.themeApp);
        }

        private void GrfRpt_DoubleClick1(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

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
        private void FrmDoctorView_Load(object sender, EventArgs e)
        {
            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();

            c1SplitContainer1.HeaderHeight = 0;
        }
    }
}
