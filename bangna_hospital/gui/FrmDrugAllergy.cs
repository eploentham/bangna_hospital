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
using System.Drawing.Imaging;
using System.Globalization;
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
        String TABACTIVE = "";
        C1FlexGrid grfLab;
        AutoCompleteStringCollection AUTODrug;
        int colLabDate = 1, colLabName = 2, colLabNameSub = 3, colLabResult = 4, colInterpret = 5, colNormal = 6, colUnit = 7;
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
            initGrfLab();
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
            tabMain.SelectedIndexChanged += TabMain_SelectedIndexChanged;
            txtLabOrdDate.KeyPress += TxtLabOrdDate_KeyPress;
        }

        private void TxtLabOrdDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (e.g., Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void initGrfLab()
        {
            grfLab = new C1FlexGrid();
            grfLab.Dock = DockStyle.Fill;
            grfLab.Font = fEdit;
            grfLab.Rows.Count = 1;
            grfLab.Cols.Count = 8;
            grfLab.Cols[colLabDate].Caption = "วันที่สั่ง";
            grfLab.Cols[colLabName].Caption = "ชื่อLAB";
            grfLab.Cols[colLabNameSub].Caption = "ชื่อLABย่อย";
            grfLab.Cols[colLabResult].Caption = "ผลLAB";
            grfLab.Cols[colInterpret].Caption = "แปรผล";
            grfLab.Cols[colNormal].Caption = "Normal";
            grfLab.Cols[colUnit].Caption = "Unit";
            grfLab.Cols[colLabDate].Width = 100;
            grfLab.Cols[colLabName].Width = 250;
            grfLab.Cols[colLabNameSub].Width = 200;
            grfLab.Cols[colInterpret].Width = 200;
            grfLab.Cols[colNormal].Width = 200;
            grfLab.Cols[colUnit].Width = 150;
            grfLab.Cols[colLabResult].Width = 150;
            panel5.Controls.Add(grfLab);
        }
        private void setGrfLab()
        {
            DateTime vsdate = new DateTime();
            DateTime vsdate1 = new DateTime();
            vsdate = DateTime.Now;
            vsdate1 = vsdate.AddDays(-int.Parse(txtLabOrdDate.Text.Trim()));
            if(vsdate.Year < 2000)              {                vsdate = vsdate.AddYears(543);         }
            if (vsdate1.Year < 2000)            {                vsdate1 = vsdate1.AddYears(543);       }
            DataTable dt = new DataTable();
            dt = BC.bcDB.vsDB.selectLabbyDate(txtPttHN.Text, vsdate1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), vsdate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            grfLab.Rows.Count = 1;
            grfLab.Rows.Count = dt.Rows.Count + 1;
            int i = 1;
            String labname = "", labnameold = "", reqno = "", reqnoold = "";
            foreach (DataRow row in dt.Rows)
            {
                Row row1 = grfLab.Rows[i];
                labname = row["MNC_LB_DSC"].ToString();
                reqno = row["mnc_req_no"].ToString();
                if (!labname.Equals(labnameold) || !reqno.Equals(reqnoold)) {   labnameold = labname;       reqnoold = reqno;   row1[colLabName] = row["MNC_LB_DSC"].ToString();   }
                else                {                    row1[colLabName] = "";                }
                row1[colLabDate] = row["mnc_req_dat"].ToString();
                //row1[colLabName] = row["MNC_LB_DSC"].ToString();
                row1[colLabNameSub] = row["MNC_RES"].ToString();
                row1[colLabResult] = row["MNC_RES_VALUE"].ToString();
                row1[colInterpret] = row["MNC_STS"].ToString();
                row1[colNormal] = row["MNC_LB_RES"].ToString();
                row1[colUnit] = row["MNC_RES_UNT"].ToString();
                i++;
            }
        }
        private void TabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TABACTIVE = tabMain.SelectedTab.Name;
            if(TABACTIVE.Equals(tabLabOrder.Name))              {                pnLabCond.Show(); setGrfLab();             }
            else                                                {                pnLabCond.Hide();                          }
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
