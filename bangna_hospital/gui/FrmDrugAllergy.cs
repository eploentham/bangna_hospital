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
    public partial class FrmDrugAllergy : Form
    {
        BangnaControl BC;
        Font fEdit, fEditB, fEdit3B, fEdit5B, fPrnBil, famtB, fEditS, famtB30;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1FlexGrid grfDrugAllergy, grfDrugGrpAllergy;
        C1ThemeController theme1;
        Patient PTT;
        Boolean isLoad = false;
        AutoCompleteStringCollection AUTODrug;
        public FrmDrugAllergy(BangnaControl bc, Patient ptt)
        {
            this.BC = bc;
            this.PTT = ptt;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            isLoad = true;
            theme1 = new C1ThemeController();
            theme1.Theme = BC.iniC.themeApplication;
            theme1.SetTheme(this, BC.iniC.themeApplication);
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
            AUTODrug = new AutoCompleteStringCollection();
            AUTODrug = BC.bcDB.pharM01DB.setAUTODrug();
            c1SplitContainer1.HeaderHeight = 0;
            c1SplitContainer2.HeaderHeight = 0;
            initFont();
            initControl();
            setEvent();
            setControl();
            isLoad = false;
        }
        private void initFont()
        {
            fEdit = new Font(BC.iniC.grdViewFontName, BC.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(BC.iniC.grdViewFontName, BC.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new Font(BC.iniC.grdViewFontName, BC.grdViewFontSize + 3, FontStyle.Bold);
            fEdit5B = new Font(BC.iniC.grdViewFontName, BC.grdViewFontSize + 5, FontStyle.Bold);
            fPrnBil = new Font(BC.iniC.pdfFontName, BC.pdfFontSize, FontStyle.Regular);
            famtB = new Font(BC.iniC.pdfFontName, BC.pdfFontSize + 7, FontStyle.Bold);
            fEditS = new Font(BC.iniC.pdfFontName, BC.pdfFontSize - 2, FontStyle.Regular);
            famtB30 = new Font(BC.iniC.pdfFontName, BC.pdfFontSize + 30, FontStyle.Bold);
        }
        private void initControl()
        {
            txtPttHN.Value = PTT.Hn;
            lbPttNameT.Text = PTT.Name;
            txtDrugSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtDrugSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtDrugSearch.AutoCompleteCustomSource = AUTODrug;
            BC.bcDB.pharM14DB.setCboDrugGroup(cboDrugGrp,"");
            //initGrfDrugAllergy();
        }
        private void setEvent()
        {
            tabMain.SelectedTabChanged += TabMain_SelectedTabChanged;
            btnDrugSave.Click += BtnDrugSave_Click;
            btnDrugGrpSave.Click += BtnDrugGrpSave_Click; ;
            txtDrugSearch.KeyUp += TxtDrugSearch_KeyUp;
            txtItemCode.KeyUp += TxtItemCode_KeyUp;
        }

        private void TxtItemCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                
            }
        }

        private void TxtDrugSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            { if (txtDrugSearch.Text.Trim().Length <= 0) return; setOrderItem(); txtDrugAllergy.Focus(); }
        }

        private void BtnDrugGrpSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (cboDrugGrp.SelectedItem == null) return;
            if (txtPttHN.Text.Trim().Length <= 0) return;
            PatientT022 ptt022 = new PatientT022();
            ptt022.MNC_HN_NO = txtPttHN.Text.Trim();
            ptt022.MNC_PH_GRP_CD = ((ComboBoxItem)cboDrugGrp.SelectedItem).Value.ToString();
            ptt022.MNC_PH_MEMO = txtDrugGrpAllergy.Text.Trim();
            ptt022.MNC_PH_ALG_CD = cboDrugGrpSetName.SelectedItem == null ? "" : ((ComboBoxItem)cboDrugGrpSetName.SelectedItem).Value.ToString();
            ptt022.MNC_EMP_CD = BC.userId;
            ptt022.MNC_HN_YR = PTT.MNC_HN_YR;
            //ptt022.MNC_STAMP_TIM = DateTime.Now.ToString("HH:mm:ss");
            String re = BC.bcDB.ptt022DB.insertPatientT022(ptt022);
            setGrfDrugGrpAllergy();
        }

        private void BtnDrugSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtItemCode.Text.Trim().Length <= 0) return;
            if (txtPttHN.Text.Trim().Length <= 0) return;
            PatientT02 ptt02 = new PatientT02();
            ptt02.MNC_HN_NO = txtPttHN.Text.Trim();
            ptt02.MNC_PH_CD = txtItemCode.Text.Trim();
            ptt02.MNC_PH_MEMO = txtDrugAllergy.Text.Trim();
            ptt02.MNC_PH_ALG_CD = cboDrugSetName.SelectedItem == null ? "" : ((ComboBoxItem)cboDrugSetName.SelectedItem).Value.ToString();
            ptt02.MNC_EMP_CD = BC.userId;
            ptt02.MNC_HN_YR = PTT.MNC_HN_YR;
            //ptt02.MNC_STAMP_TIM = DateTime.Now.ToString("HH:mm:ss");
            String re = BC.bcDB.ptt02DB.insertPatientT02(ptt02);
            setGrfDrugAllergy();
        }

        private void TabMain_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (isLoad) return;
            if(tabMain.SelectedTab == tabDrugAllergy)
            {
                if (grfDrugAllergy == null)
                {
                    initGrfDrugAllergy();
                }
                setGrfDrugAllergy();
            }
            else if(tabMain.SelectedTab == tabDrugGrpAllergy)
            {
                if(grfDrugGrpAllergy == null)
                {
                    initGrfDrugGrpAllergy();
                }
                setGrfDrugGrpAllergy();
            }
        }
        private void setControl()
        {
            if (grfDrugAllergy == null)
            {
                initGrfDrugAllergy();
            }
            setGrfDrugAllergy();
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

            //grfDrugAllergy.Rows[0].Visible = false;
            //grfDrugAllergy.Cols[0].Visible = false;
            grfDrugAllergy.Cols[1].AllowEditing = false;
            grfDrugAllergy.Cols[2].AllowEditing = false;
            grfDrugAllergy.Cols[3].AllowEditing = false;
            grfDrugAllergy.Cols[1].Visible = true;

            pnDrugAllergy.Controls.Add(grfDrugAllergy);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            //theme1.SetTheme(grfDrugAllergy, "VS2013Dark");
            theme1.SetTheme(grfDrugAllergy, "ExpressionLight");
        }
        private void setGrfDrugAllergy()
        {
            //ใช้ database ใน object patient จะได้ไม่ต้องดึงข้อมูลหลายครั้ง  ลดการดึงข้อมูล
            DataTable dt = BC.bcDB.vsDB.selectDrugAllergy(txtPttHN.Text.Trim());
            grfDrugAllergy.Rows.Count = 1; grfDrugAllergy.Rows.Count = dt.Rows.Count + 1;
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfDrugAllergy.Rows[i];
                rowa[1] = row1["mnc_ph_tn"].ToString();
                rowa[3] = row1["MNC_PH_ALG_DSC"].ToString();
                rowa[2] = row1["MNC_PH_MEMO"].ToString();
                i++;
            }
        }
        private void initGrfDrugGrpAllergy()
        {
            grfDrugGrpAllergy = new C1FlexGrid();
            grfDrugGrpAllergy.Font = fEdit;
            grfDrugGrpAllergy.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDrugGrpAllergy.Location = new System.Drawing.Point(0, 0);
            grfDrugGrpAllergy.Rows.Count = 1;
            grfDrugGrpAllergy.Cols.Count = 4;
            grfDrugGrpAllergy.Cols[1].Width = 300;
            grfDrugGrpAllergy.Cols[2].Width = 300;
            grfDrugGrpAllergy.Cols[3].Width = 300;

            grfDrugGrpAllergy.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfDrugGrpAllergy.Cols[1].Caption = "drug allergy";
            grfDrugGrpAllergy.Cols[2].Caption = "-";
            grfDrugGrpAllergy.Cols[3].Caption = "-";

            //grfDrugGrpAllergy.Rows[0].Visible = false;
            //grfDrugGrpAllergy.Cols[0].Visible = false;
            grfDrugGrpAllergy.Cols[1].AllowEditing = false;
            grfDrugGrpAllergy.Cols[2].AllowEditing = false;
            grfDrugGrpAllergy.Cols[3].AllowEditing = false;
            grfDrugGrpAllergy.Cols[1].Visible = true;

            pnDrugGrpAllergy.Controls.Add(grfDrugGrpAllergy);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            //theme1.SetTheme(grfDrugAllergy, "VS2013Dark");
            theme1.SetTheme(grfDrugGrpAllergy, "ExpressionLight");
        }
        private void setGrfDrugGrpAllergy()
        {
            //ใช้ database ใน object patient จะได้ไม่ต้องดึงข้อมูลหลายครั้ง  ลดการดึงข้อมูล
            DataTable dt = BC.bcDB.vsDB.selectDrugGroupAllergy(txtPttHN.Text.Trim());
            grfDrugGrpAllergy.Rows.Count = 1; grfDrugGrpAllergy.Rows.Count = dt.Rows.Count + 1;
            //MessageBox.Show("01 ", "");
            int i = 1, j = 1;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfDrugGrpAllergy.Rows[i];
                rowa[2] = row1["MNC_PH_MEMO"].ToString();
                rowa[3] = row1["MNC_PH_ALG_DSC"].ToString();
                rowa[1] = row1["MNC_PH_GRP_DSC"].ToString();
                i++;
            }
        }
        private void setOrderItem()
        {
            String[] txt = txtDrugSearch.Text.Split('#');
            String name = txt[0].Trim();
            String code = txt[1].Trim();
            PharmacyM01 drug = new PharmacyM01();
            DataTable dtpmap = new DataTable();
            dtpmap = BC.bcDB.drugDB.selectPaidmapByCode(code);
            drug = BC.bcDB.pharM01DB.SelectNameByPk1(code);
            txtItemCode.Value = code;
            lbItemName.Text = drug.MNC_PH_TN;
            lbItemNameThai.Text = drug.MNC_PH_THAI;
            lbTradeName.Text = drug.MNC_PH_GN;
            
        }
        private void FrmDrugAllergy_Load(object sender, EventArgs e)
        {
            
        }
    }
}
