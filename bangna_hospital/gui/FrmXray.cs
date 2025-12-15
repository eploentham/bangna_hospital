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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmXray : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1ThemeController theme1;
        C1FlexGrid grfOperList;

        Image imgCorr, imgTran;
        Timer timeOperList;
        Patient PTT;
        Visit VS;
        Label lbLoading;

        int colgrfOperListHn = 1, colgrfOperListFullNameT = 2, colgrfOperListSymptoms = 3, colgrfOperListPaidName = 4, colgrfOperListPreno = 5, colgrfOperListVsDate = 6, colgrfOperListVsTime = 7, colgrfOperListActNo = 8, colgrfOperListDtrName = 9, colgrfOperListLab = 10, colgrfOperListXray = 11;

        String DEPTNO = "";
        Boolean pageLoad = false;
        public FrmXray(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            
            theme1 = new C1ThemeController();
            imgCorr = Resources.red_checkmark_png_16;
            imgTran = Resources.DeleteTable_small;

            timeOperList = new Timer();
            timeOperList.Interval = 30000;
            timeOperList.Enabled = false;
            PTT = new Patient();
            VS = new Visit();

            initFont();
            initLoading();
            initGrfOperList();
            pageLoad = false;
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
        private void initGrfOperList()
        {
            grfOperList = new C1FlexGrid();
            grfOperList.Font = fEdit;
            grfOperList.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOperList.Location = new System.Drawing.Point(0, 0);
            grfOperList.Rows.Count = 1;
            grfOperList.Cols.Count = 12;
            grfOperList.Cols[colgrfOperListHn].Width = 100;
            grfOperList.Cols[colgrfOperListFullNameT].Width = 200;
            grfOperList.Cols[colgrfOperListSymptoms].Width = 150;
            grfOperList.Cols[colgrfOperListPaidName].Width = 100;
            grfOperList.Cols[colgrfOperListPreno].Width = 100;
            grfOperList.Cols[colgrfOperListVsDate].Width = 100;
            grfOperList.Cols[colgrfOperListVsTime].Width = 70;
            grfOperList.Cols[colgrfOperListActNo].Width = 100;
            grfOperList.Cols[colgrfOperListDtrName].Width = 100;
            grfOperList.Cols[colgrfOperListLab].Width = 50;
            grfOperList.Cols[colgrfOperListXray].Width = 50;
            grfOperList.ShowCursor = true;
            grfOperList.Cols[colgrfOperListHn].Caption = "HN";
            grfOperList.Cols[colgrfOperListFullNameT].Caption = "ชื่อ-นามสกุล";
            grfOperList.Cols[colgrfOperListSymptoms].Caption = "อาการ";
            grfOperList.Cols[colgrfOperListPaidName].Caption = "สิทธิ";
            grfOperList.Cols[colgrfOperListPreno].Caption = "preno";
            grfOperList.Cols[colgrfOperListVsDate].Caption = "วันที่";
            grfOperList.Cols[colgrfOperListVsTime].Caption = "เวลา";
            grfOperList.Cols[colgrfOperListActNo].Caption = "สถานะ";
            grfOperList.Cols[colgrfOperListDtrName].Caption = "แพทย์";
            grfOperList.Cols[colgrfOperListLab].Caption = "lab";
            grfOperList.Cols[colgrfOperListXray].Caption = "xray";
            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grfOperList.Cols[colgrfOperListHn].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListVsDate].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListVsTime].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListActNo].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListDtrName].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListLab].DataType = typeof(String);
            grfOperList.Cols[colgrfOperListXray].DataType = typeof(String);

            grfOperList.Cols[colgrfOperListHn].TextAlign = TextAlignEnum.CenterCenter;
            grfOperList.Cols[colgrfOperListVsTime].TextAlign = TextAlignEnum.CenterCenter;
            grfOperList.Cols[colgrfOperListActNo].TextAlign = TextAlignEnum.LeftCenter;
            grfOperList.Cols[colgrfOperListDtrName].TextAlign = TextAlignEnum.LeftCenter;
            grfOperList.Cols[colgrfOperListLab].TextAlign = TextAlignEnum.CenterCenter;
            grfOperList.Cols[colgrfOperListXray].TextAlign = TextAlignEnum.CenterCenter;

            grfOperList.Cols[colgrfOperListPreno].Visible = false;
            grfOperList.Cols[colgrfOperListVsDate].Visible = false;
            grfOperList.Cols[colgrfOperListHn].Visible = false;

            grfOperList.Cols[colgrfOperListHn].AllowEditing = false;
            grfOperList.Cols[colgrfOperListFullNameT].AllowEditing = false;
            grfOperList.Cols[colgrfOperListSymptoms].AllowEditing = false;
            grfOperList.Cols[colgrfOperListPaidName].AllowEditing = false;
            grfOperList.Cols[colgrfOperListPreno].AllowEditing = false;
            grfOperList.Cols[colgrfOperListVsDate].AllowEditing = false;
            grfOperList.Cols[colgrfOperListVsTime].AllowEditing = false;
            grfOperList.Cols[colgrfOperListActNo].AllowEditing = false;
            grfOperList.Cols[colgrfOperListDtrName].AllowEditing = false;
            grfOperList.Cols[colgrfOperListLab].AllowEditing = false;
            grfOperList.Cols[colgrfOperListXray].AllowEditing = false;

            grfOperList.AfterRowColChange += GrfOperList_AfterRowColChange;
            //grfCheckUPList.AllowFiltering = true;

            spOperList.Controls.Add(grfOperList);
            theme1.SetTheme(grfOperList, bc.iniC.themeApp);
        }
        private void GrfOperList_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            //setControlOper();
        }
        private void setGrfOperList()
        {
            if (pageLoad) return;
            pageLoad = true;
            timeOperList.Enabled = false;
            DataTable dtvs = new DataTable();
            String deptno = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
            String vsdate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd");
            dtvs = bc.bcDB.vsDB.selectPttinDeptActNo101(deptno, bc.iniC.station, vsdate, vsdate,"","");

            grfOperList.Rows.Count = 1;
            grfOperList.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, row = grfOperList.Rows.Count;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfOperList.Rows[i];
                rowa[colgrfOperListHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfOperListFullNameT] = row1["ptt_fullnamet"].ToString();
                rowa[colgrfOperListSymptoms] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colgrfOperListPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[colgrfOperListPreno] = row1["MNC_PRE_NO"].ToString();

                rowa[colgrfOperListVsDate] = row1["MNC_DATE"].ToString();
                rowa[colgrfOperListVsTime] = bc.showTime(row1["MNC_TIME"].ToString());
                rowa[colgrfOperListActNo] = bc.adjustACTNO(row1["MNC_ACT_NO"].ToString());
                rowa[colgrfOperListDtrName] = row1["dtr_name"].ToString();
                if (row1["MNC_ACT_NO"].ToString().Equals("110")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor); }
                else if (row1["MNC_ACT_NO"].ToString().Equals("114")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6"); }

                rowa[0] = i.ToString();
                i++;
            }
            timeOperList.Enabled = true;
            pageLoad = false;
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
        private void setHeaderHeight0()
        {
            spOper.HeaderHeight = 0;
        }
        private void FrmXray_Load(object sender, EventArgs e)
        {
            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            setHeaderHeight0();
            setGrfOperList();
            timeOperList.Enabled = true;
            timeOperList.Start();
            DEPTNO = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
            String stationname = bc.bcDB.pm32DB.getDeptName(bc.iniC.station);
            lfSbStation.Text = DEPTNO + "[" + bc.iniC.station + "]" + stationname;
            rgSbModule.Text = bc.iniC.hostDBMainHIS + " " + bc.iniC.nameDBMainHIS;
            this.Text = "Last Update 2024-02-21";
        }
    }
}
