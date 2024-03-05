using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmOrder : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1FlexGrid grfDrugAllergy, grfChronic, grfOrder, grfItems;

        Patient PTT;
        Visit VS;
        String PRENO = "", VSDATE = "", HN = "", DEPTNO = "", DTRCODE = "";
        Boolean pageLoad = false;
        C1ThemeController theme1;
        Label lbLoading;
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmOrder(BangnaControl bc, String dtrcode, ref Patient ptt, String vsdate, String preno)
        {
            this.bc = bc;
            this.PTT = ptt;
            this.DTRCODE = dtrcode;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            theme1 = new C1ThemeController();
            initFont();
            initControl();

            setControlPnPateint();
            setEvent();
            setTheme();
            pageLoad = false;
        }
        private void initControl()
        {
            initLoading();
            initGrfDrugAllergy();
            initGrfChronic();
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
        private void setTheme()
        {
            //theme1.SetTheme(pnPtt, "ExpressionLight");
            //foreach (Control c in pnPtt.Controls) if (c is C1TextBox) theme1.SetTheme(c, "ExpressionLight");
        }
        private void setEvent()
        {
            
        }
        private void initGrfChronic()
        {
            grfChronic = new C1FlexGrid();
            grfChronic.Font = fEdit;
            grfChronic.Dock = System.Windows.Forms.DockStyle.Fill;
            grfChronic.Location = new System.Drawing.Point(0, 0);
            grfChronic.Rows.Count = 1;
            grfChronic.Cols.Count = 2;
            grfChronic.Cols[1].Width = 300;

            grfChronic.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfChronic.Cols[1].Caption = "-";

            grfChronic.Rows[0].Visible = false;
            grfChronic.Cols[0].Visible = false;
            grfChronic.Cols[1].Visible = true;
            grfChronic.Cols[1].AllowEditing = false;

            pnChronic.Controls.Add(grfChronic);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfChronic, "Office2010Red");
        }
        private void setGrfChronic()
        {
            grfChronic.Rows.Count = 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in PTT.CHRONIC.Rows)
            {
                //pB1.Value++;
                Row rowa = grfChronic.Rows.Add();
                rowa[1] = row1["MNC_CRO_DESC"].ToString();

            }
        }
        private void initGrfDrugAllergy()
        {
            grfDrugAllergy = new C1FlexGrid();
            grfDrugAllergy.Font = fEdit;
            grfDrugAllergy.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDrugAllergy.Location = new System.Drawing.Point(0, 0);
            grfDrugAllergy.Rows.Count = 1;
            grfDrugAllergy.Cols.Count = 4;
            grfDrugAllergy.Cols[1].Width = 300;
            grfDrugAllergy.Cols[2].Width = 300;
            grfDrugAllergy.Cols[3].Width = 300;

            grfDrugAllergy.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfDrugAllergy.Cols[1].Caption = "drug allergy";
            grfDrugAllergy.Cols[2].Caption = "-";
            grfDrugAllergy.Cols[3].Caption = "-";

            grfDrugAllergy.Rows[0].Visible = false;
            grfDrugAllergy.Cols[0].Visible = false;
            grfDrugAllergy.Cols[1].AllowEditing = false;
            grfDrugAllergy.Cols[2].AllowEditing = false;
            grfDrugAllergy.Cols[3].AllowEditing = false;
            grfDrugAllergy.Cols[1].Visible = true;

            pnDrugAllergy.Controls.Add(grfDrugAllergy);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfDrugAllergy, "VS2013Dark");
        }
        private void setGrfDrugAllergy()
        {
            grfDrugAllergy.Rows.Count = 1;
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1;
            foreach (DataRow row1 in PTT.DRUGALLERGY.Rows)
            {
                //pB1.Value++;
                Row rowa = grfDrugAllergy.Rows.Add();
                rowa[1] = row1["mnc_ph_tn"].ToString();
                rowa[3] = row1["MNC_PH_ALG_DSC"].ToString();
                rowa[2] = row1["MNC_PH_MEMO"].ToString();
            }
        }
        private void setControlPnPateint()
        {
            //PTT = bc.bcDB.pttDB.selectPatinetByHn(this.HN);
            txtPttHN.Value = PTT.Hn;
            lbPttNameT.Text = PTT.Name;

            setGrfDrugAllergy();
            setGrfChronic();
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
        private void setLbLoading(String txt)
        {
            lbLoading.Text = txt;
            Application.DoEvents();
        }
        private void hideLbLoading()
        {
            lbLoading.Hide();
            Application.DoEvents();
        }
        private void FrmOrder_Load(object sender, EventArgs e)
        {
            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            DEPTNO = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
            String stationname = bc.bcDB.pm32DB.getDeptName(bc.iniC.station);

            lfSbStation.Text = DEPTNO + "[" + bc.iniC.station + "]" + stationname;
            rgSbModule.Text = bc.iniC.hostDBMainHIS + " " + bc.iniC.nameDBMainHIS;
            this.Text = "Last Update 2024-02-26";
        }
    }
}
