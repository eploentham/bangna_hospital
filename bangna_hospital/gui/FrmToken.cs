using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace bangna_hospital.gui
{
    public partial class FrmToken : Form
    {
        BangnaControl bc;
        System.Drawing.Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2;
        C1FlexGrid grfUnUsed, grfUsed;
        Font fStaffN;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1ThemeController theme1;
        PatientM26 DTR;

        int colUsedID = 1, colUsedToken = 2, colUsedDateExpire = 3, colUsedDateCreate = 4, colUsedNumberDaysExpire = 6, colUsedTokenType = 7, colUsedActive = 8;
        Boolean pageLoad = false;
        public FrmToken(BangnaControl bc, PatientM26 dtr)
        {
            this.bc = bc;
            this.DTR = dtr;
            InitializeComponent(); 
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
            initFont();
            setTheme();
            bc.setCboTokenType(cboTokenType);
            initGrfUnUsed();
            initGrfUsed();

            btnTokenGen.Click += BtnTokenGen_Click;
            tCMain.SelectedTabChanged += TCMain_SelectedTabChanged;
            pageLoad = false;
        }
        private void TCMain_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(tCMain.SelectedTab == tabToken)            {                setGrfUnUsed();            }
            else if (tCMain.SelectedTab == tabView)            {                setGrfUsed();            }
        }
        private void initGrfUsed()
        {
            grfUsed = new C1FlexGrid();
            grfUsed.Font = fEdit;
            grfUsed.Dock = System.Windows.Forms.DockStyle.Fill;
            grfUsed.Location = new System.Drawing.Point(0, 0);
            grfUsed.Rows.Count = 1;
            grfUsed.Cols.Count = 9;
            grfUsed.Cols[colUsedID].Width = 100;
            grfUsed.Cols[colUsedToken].Width = 100;
            grfUsed.Cols[colUsedDateExpire].Width = 100;
            grfUsed.Cols[colUsedDateCreate].Width = 100;
            grfUsed.Cols[colUsedNumberDaysExpire].Width = 80;
            grfUsed.Cols[colUsedTokenType].Width = 100;
            grfUsed.ShowCursor = true;
            grfUsed.Cols[colUsedID].Caption = "id";
            grfUsed.Cols[colUsedToken].Caption = "token";
            grfUsed.Cols[colUsedDateExpire].Caption = "DateExpire";
            grfUsed.Cols[colUsedDateCreate].Caption = "DateCreate";
            grfUsed.Cols[colUsedNumberDaysExpire].Caption = "days";
            grfUsed.Cols[colUsedTokenType].Caption = "Type";

            grfUsed.Cols[colUsedID].Visible = false;

            grfUsed.Cols[colUsedToken].AllowEditing = false;
            grfUsed.Cols[colUsedDateExpire].AllowEditing = false;
            grfUsed.Cols[colUsedDateCreate].AllowEditing = false;
            grfUsed.Cols[colUsedNumberDaysExpire].AllowEditing = false;
            grfUsed.Cols[colUsedTokenType].AllowEditing = false;
            grfUsed.AllowFiltering = true;

            //grfUsed.Click += GrfCheckUPList_Click;

            panel4.Controls.Add(grfUsed);
            theme1.SetTheme(grfUsed, bc.iniC.themeApp);
        }
        private void setGrfUsed()
        {
            //showLbLoading();
            DataTable dtcheckup = new DataTable();
            dtcheckup = bc.bcDB.tokenDB.SelectUsedByUsername(DTR.MNC_DOT_CD);
            int i = 1, j = 1;
            grfUsed.Rows.Count = 1;
            grfUsed.Rows.Count = dtcheckup.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dtcheckup.Rows)
            {
                //pB1.Value++;
                String[] tokensplit = row1["token_text"].ToString().Split('.');
                Row rowa = grfUsed.Rows[i];
                rowa[colUsedID] = row1["token_id"].ToString();
                if (tokensplit.Length > 2) { rowa[colUsedToken] = tokensplit[2]; }
                else { rowa[colUsedToken] = row1["token_text"].ToString(); }
                rowa[colUsedDateExpire] = row1["date_expire"].ToString();
                rowa[colUsedDateCreate] = row1["date_create"].ToString();
                rowa[colUsedNumberDaysExpire] = row1["number_days_expire"].ToString();
                rowa[colUsedTokenType] = row1["token_type"].ToString();
                rowa[0] = i.ToString();
                i++;
            }
            //hideLbLoading();
        }
        private void initGrfUnUsed()
        {
            grfUnUsed = new C1FlexGrid();
            grfUnUsed.Font = fEdit;
            grfUnUsed.Dock = System.Windows.Forms.DockStyle.Fill;
            grfUnUsed.Location = new System.Drawing.Point(0, 0);
            grfUnUsed.Rows.Count = 1;
            grfUnUsed.Cols.Count = 9;
            grfUnUsed.Cols[colUsedID].Width = 100;
            grfUnUsed.Cols[colUsedToken].Width = 100;
            grfUnUsed.Cols[colUsedDateExpire].Width = 100;
            grfUnUsed.Cols[colUsedDateCreate].Width = 100;
            grfUnUsed.Cols[colUsedNumberDaysExpire].Width = 80;
            grfUnUsed.Cols[colUsedTokenType].Width = 100;
            grfUnUsed.ShowCursor = true;
            grfUnUsed.Cols[colUsedID].Caption = "id";
            grfUnUsed.Cols[colUsedToken].Caption = "token";
            grfUnUsed.Cols[colUsedDateExpire].Caption = "DateExpire";
            grfUnUsed.Cols[colUsedDateCreate].Caption = "DateCreate";
            grfUnUsed.Cols[colUsedNumberDaysExpire].Caption = "days";
            grfUnUsed.Cols[colUsedTokenType].Caption = "Type";

            grfUnUsed.Cols[colUsedID].Visible = false;

            grfUnUsed.Cols[colUsedToken].AllowEditing = false;
            grfUnUsed.Cols[colUsedDateExpire].AllowEditing = false;
            grfUnUsed.Cols[colUsedDateCreate].AllowEditing = false;
            grfUnUsed.Cols[colUsedNumberDaysExpire].AllowEditing = false;
            grfUnUsed.Cols[colUsedTokenType].AllowEditing = false;
            grfUnUsed.AllowFiltering = true;

            //grfUsed.Click += GrfCheckUPList_Click;

            panel3.Controls.Add(grfUnUsed);
            theme1.SetTheme(grfUnUsed, bc.iniC.themeApp);
        }
        private void setGrfUnUsed()
        {
            //showLbLoading();
            DataTable dtcheckup = new DataTable();
            dtcheckup = bc.bcDB.tokenDB.SelectUnUsedByUsername(DTR.MNC_DOT_CD);
            int i = 1, j = 1;
            grfUnUsed.Rows.Count = 1;
            grfUnUsed.Rows.Count = dtcheckup.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dtcheckup.Rows)
            {
                //pB1.Value++;
                String[] tokensplit = row1["token_text"].ToString().Split('.');
                Row rowa = grfUnUsed.Rows[i];
                rowa[colUsedID] = row1["token_id"].ToString();
                if (tokensplit.Length > 2)                {                    rowa[colUsedToken] = tokensplit[2];                }
                else                {                    rowa[colUsedToken] = row1["token_text"].ToString();                }
                rowa[colUsedDateExpire] = row1["date_expire"].ToString();
                rowa[colUsedDateCreate] = row1["date_create"].ToString();
                rowa[colUsedNumberDaysExpire] = row1["number_days_expire"].ToString();
                rowa[colUsedTokenType] = row1["token_type"].ToString();
                rowa[0] = i.ToString();
                i++;
            }
            //hideLbLoading();
        }
        private void BtnTokenGen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            btnTokenGen.Enabled = false;
            String tokentype = ((ComboBoxItem)cboTokenType.SelectedItem).Value.ToString();
            
            for (int i = 0; i < int.Parse(txtTokenQyt.Text); i++)
            {
                //String secretKey = DTR.MNC_DOT_CD + DTR.dtrname + cboTokenType.Text.Trim() + "bangna_hospital_checkup" + DateTime.Now.Ticks.ToString();
                String secretKey =  DateTime.Now.Ticks.ToString();
                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var tokenService = new TokenService(signinKey.ToString());
                var token = tokenService.GenerateToken(DTR.MNC_DOT_CD, DTR.dtrname);
                Console.WriteLine("Generated Token: " + token);
                DateTime dt = DateTime.UtcNow.AddYears(1);
                Token t = new Token();
                t.token_id = "";
                t.token_module_name = "bangna_hospital";
                t.date_expire = dt.Year.ToString() + "-" + dt.ToString("MM-dd");
                t.user_name = DTR.MNC_DOT_CD;
                t.secret_key = secretKey;
                t.token_text = token;
                t.number_days_expire = "365";
                t.token_type = tokentype;
                String re = bc.bcDB.tokenDB.insert(t);
                if (int.TryParse(re, out int chk))
                {
                    Console.WriteLine("Generated Token: " + token);
                }
                else
                {
                    Console.WriteLine("Error Generated Token: " + token);
                }
            }
            setGrfUnUsed();
            btnTokenGen.Enabled = true;
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

            fPDF = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fPDFs2 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            fPDFl2 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 2, FontStyle.Regular);
            fPDFs6 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 6, FontStyle.Regular);
            fPDFs8 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 8, FontStyle.Regular);
            fStaffN = new System.Drawing.Font(bc.iniC.staffNoteFontName, bc.staffNoteFontSize, FontStyle.Regular);

        }
        private void setTheme()
        {
            theme1 = bc.theme1;
            theme1.SetTheme(panel1, "Violette");
        }
        private void FrmToken_Load(object sender, EventArgs e)
        {
            lbTokenDtrName.Text = DTR.dtrname;
            lbViewDtrName.Text = DTR.dtrname;
            setGrfUnUsed();
        }
    }
}
