using bangna_hospital.control;
using bangna_hospital.object1;
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
    public partial class FrmSSO : Form
    {
        BangnaControl bc;
        System.Drawing.Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1FlexGrid grfSupra, grfRpt;
        Patient PTT;
        C1ThemeController theme1;
        int colgrfSupraVsDate = 1, colgrfSupraPreno = 2, colgrfSupraSymptoms = 3, colgrfSupraVNAN=4, colgrfSupraStatusAdmit=5;

        Label lbLoading;
        Boolean pageLoad = false;
        public FrmSSO(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            theme1 = new C1ThemeController();

            initFont();
            initLoading();
            initGrfSupra();
            PTT = new Patient();
            

            txtSupraNewHn.KeyUp += TxtSupraNewHn_KeyUp;
            btnSuprNewSave.Click += BtnSuprNewSave_Click;
        }

        private void BtnSuprNewSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void initFont()
        {
            fEdit = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new System.Drawing.Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            famt1 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 1, FontStyle.Regular);
            famt5 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
            famt7 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Regular);
            famt7B = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
            famtB14 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 14, FontStyle.Bold);
            famtB30 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 30, FontStyle.Bold);
            ftotal = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 60, FontStyle.Bold);
            fPrnBil = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fque = new System.Drawing.Font(bc.iniC.queFontName, bc.queFontSize + 3, FontStyle.Bold);
            fqueB = new System.Drawing.Font(bc.iniC.queFontName, bc.queFontSize + 7, FontStyle.Bold);
            fEditS = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            fEditS1 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 1, FontStyle.Regular);

        }
        private void TxtSupraNewHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setControlSupra(txtSupraNewHn.Text);
            }
        }
        private void setControlSupra(String hn)
        {
            lbPttAttachNote.Text = "";
            lbVN.Text = "";
            PTT = bc.bcDB.pttDB.selectPatinetByHn(hn);
            if (PTT == null) return;
            txtSupraNewHn.Value = PTT.MNC_HN_NO;
            lbSupraNewPttName.Text = PTT.Name;
            lbPttAge.Text = PTT.AgeStringOK1DOT();
            lbPttAttachNote.Text = PTT.MNC_ATT_NOTE;
            setGrfSupra(hn);
        }
        private void initGrfSupra()
        {
            grfSupra = new C1FlexGrid();
            grfSupra.Font = fEdit;
            grfSupra.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSupra.Location = new System.Drawing.Point(0, 0);
            grfSupra.Rows.Count = 1;
            grfSupra.Cols.Count = 11;

            grfSupra.Cols[colgrfSupraVsDate].Width = 90;
            grfSupra.Cols[colgrfSupraPreno].Width = 60;
            grfSupra.Cols[colgrfSupraSymptoms].Width = 200;
            grfSupra.Cols[colgrfSupraVNAN].Width = 100;
            grfSupra.Cols[colgrfSupraStatusAdmit].Width = 60;
            
            grfSupra.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfSupra.Cols[colgrfSupraVsDate].Caption = "visitdate";
            grfSupra.Cols[colgrfSupraPreno].Caption = "preno";
            grfSupra.Cols[colgrfSupraSymptoms].Caption = "Symptoms";
            grfSupra.Cols[colgrfSupraVNAN].Caption = "VNAN";
            grfSupra.Cols[colgrfSupraStatusAdmit].Caption = "status";

            grfSupra.Cols[colgrfSupraVsDate].AllowEditing = false;
            grfSupra.Cols[colgrfSupraPreno].AllowEditing = false;
            grfSupra.Cols[colgrfSupraSymptoms].AllowEditing = false;
            grfSupra.Cols[colgrfSupraVNAN].AllowEditing = false;
            grfSupra.Cols[colgrfSupraStatusAdmit].AllowEditing = false;

            grfSupra.AfterRowColChange += GrfSupra_AfterRowColChange;

            pnSupra.Controls.Add(grfSupra);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfSupra, bc.iniC.themegrfIpd);
        }

        private void GrfSupra_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void setGrfSupra(String hn)
        {
            showLbLoading();
            DataTable dtvs = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dtvs = bc.bcDB.vsDB.selectVisitByHn(hn);
            int i = 1, j = 1, row = grfSupra.Rows.Count;

            grfSupra.Rows.Count = 1;
            grfSupra.Rows.Count = dtvs.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dtvs.Rows)
            {
                //pB1.Value++;
                Row rowa = grfSupra.Rows[i];

                rowa[colgrfSupraVsDate] = row1["MNC_DATE"].ToString();
                rowa[colgrfSupraPreno] = row1["mnc_pre_no"].ToString();
                rowa[colgrfSupraStatusAdmit] = row1["MNC_AN_NO"].ToString().Equals("0") ? "O" : "I";
                rowa[colgrfSupraSymptoms] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colgrfSupraVNAN] = row1["MNC_AN_NO"].ToString().Equals("0") ? row1["MNC_VN_NO"].ToString(): row1["MNC_AN_NO"].ToString();

                rowa[0] = i;
                i++;
            }
            hideLbLoading();
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
        private void setLbLoading(String txt)
        {
            lbLoading.Text = txt;
            Application.DoEvents();
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
        private void FrmSSO_Load(object sender, EventArgs e)
        {
            scSupraNew.HeaderHeight = 0;

            System.Drawing.Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
        }
    }
}
