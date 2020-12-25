using bangna_hospital.control;
using bangna_hospital.object1;
using C1.C1Pdf;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmOPBKKClaim:Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B;

        C1DockingTab tcMain, tcOPBKK;
        C1DockingTabPage tabOPBkk, tabOPBKKPtt, tabOPBKKINS, tabOPBKKPAT, tabOPBKKOPD, tabOPBKKODX, tabOPBKKOOP, tabOPBKKORF, tabOPBKKCHT, tabOPBKKCHA, tabOPBKKAER, tabOPBKKLABFU, tabOPBKKDRU, tabOPBKKCHAD, tabOrd;
        C1DockingTabPage tabMainPaidTyp, tabMainClinic;
        C1FlexGrid grfSelect, grfINS, grfPAT, grfOPD, grfODX, grfOOP, grfORF, grfCHT, grfCHA, grfAER, grfLABFU, grfDRU, grfCHAD, grfOrd, grfPaidTyp, grfClinic;
        
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1PdfDocument pdfDoc;
        C1ThemeController theme1;
        Label lbDateStart, lbDateEnd, lbtxtPaidType, lbLoading, lbtxtHospMain, lbtxtHCode;
        C1DateEdit txtDateStart, txtDateEnd;
        C1Button btnOPBKKSelect, btnOPBkkGen;
        C1ComboBox cboPaidType;
        C1TextBox txtPaidType, txtHospMain, txtHCode;
        Panel pnOPBKK;
        DataTable dtPtt = new DataTable();
        List<OPBKKINS> lINS = new List<OPBKKINS>();
        List<OPBKKPAT> lPAT = new List<OPBKKPAT>();
        List<OPBKKOPD> lOPD = new List<OPBKKOPD>();
        List<OPBKKORF> lORF = new List<OPBKKORF>();
        List<OPBKKODX> lODX = new List<OPBKKODX>();
        List<OPBKKOOP> lOOP = new List<OPBKKOOP>();
        List<OPBKKCHT> lCHT = new List<OPBKKCHT>();
        List<OPBKKCHA> lCHA = new List<OPBKKCHA>();
        List<OPBKKAER> lAER = new List<OPBKKAER>();
        List<OPBKKDRU> lDRU = new List<OPBKKDRU>();
        List<OPBKKLABFU> lLABFU = new List<OPBKKLABFU>();
        List<OPBKKCHAD> lCHAD = new List<OPBKKCHAD>();

        int colSelectHn = 1, colSelectName = 2, colSelectVsDate = 3, colSelectPaidType=4, colSelectSymptoms = 5, colSelectHnYear=6, colSelectPreno=7;
        int colOrdAddId = 1, colOrdAddNameT = 2, colOrdAddUnit = 3, colOrdAddQty = 4, colOrdAddDrugFr = 5, colOrdAddDrugIn = 6, colOrdDrugIn1 = 7, colOrdAddItemType = 8, colOrdAddRowNo = 9, colOrdAddFlag = 10;        // order add
        int colPaidTypId = 1, colPaidTypName = 2, colPaidTypIdOpBkkCode = 3, colPaidTypFNSYS=4, colPaidTypPTTYP=5, colPaidTypAccNo=6;
        int colClinicmddepno = 1, colClinicsecno = 2, colClinicdivno = 3, colClinictyppt = 4, colClinicdepdsc = 5, colClinicdpno = 7, colClinicopbkkcode = 6;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmOPBKKClaim(BangnaControl bc)
        {
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);
            theme1 = new C1.Win.C1Themes.C1ThemeController();

            initCompoment();
            setControl();

            this.Load += FrmOPBKKClaim_Load;
            btnOPBkkGen.Click += BtnOPBkkGen_Click;
        }

        private void BtnOPBkkGen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String datetick = "", pathfile = "";
            pathfile = bc.iniC.medicalrecordexportpath;
            datetick = DateTime.Now.Ticks.ToString();
            pathfile = pathfile + "\\" + datetick + "\\";
            if (!Directory.Exists(pathfile))
            {
                Directory.CreateDirectory(pathfile);
                Application.DoEvents();
            }
            setLbLoading("genINS");
            genINS();
            setLbLoading("genPAT");
            genPAT();
            setLbLoading("genOPD");
            genOPD();
            setLbLoading("genORF");
            genORF();
            setLbLoading("genODX");
            genODX();
            setLbLoading("genOOP");
            genOOP();
            setLbLoading("genCHT");
            genCHT();
            setLbLoading("genCHA");
            genCHA();
            setLbLoading("genAER");
            genAER();
            setLbLoading("genDRU");
            genDRU();
            setLbLoading("genLABFU");
            genLABFU();
            setLbLoading("genCHAD");
            genCHAD();

            setLbLoading("genTextINS");
            genTextINS(pathfile);
            setLbLoading("genTextPAT");
            genTextPAT(pathfile);
            setLbLoading("genTextOPD");
            genTextOPD(pathfile);
            setLbLoading("genTextORF");
            genTextORF(pathfile);
            setLbLoading("genTextODX");
            genTextODX(pathfile);
            setLbLoading("genTextOOP");
            genTextOOP(pathfile);
            setLbLoading("genTextCHT");
            genTextCHT(pathfile);
            setLbLoading("genTextCHA");
            genTextCHA(pathfile);
            setLbLoading("genTextAER");
            genTextAER(pathfile);
            setLbLoading("genTextDRU");
            genTextDRU(pathfile);
            setLbLoading("genTextLABFU");
            genTextLABFU(pathfile);
            setLbLoading("genTextCHAD");
            genTextCHAD(pathfile);
            
            Application.DoEvents();
            
            Process.Start("explorer.exe", pathfile);
        }

        private void initCompoment()
        {
            int gapLine = 25, gapX = 20, gapY = 20, xCol2 = 130, xCol1 = 80, xCol3 = 330, xCol4 = 640, xCol5 = 950;
            Size size = new Size();

            tcMain = new C1DockingTab();
            tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tcMain.Location = new System.Drawing.Point(0, 266);
            tcMain.Name = "tcMain";
            tcMain.Size = new System.Drawing.Size(669, 200);
            tcMain.TabIndex = 0;
            tcMain.TabsSpacing = 5;

            tabOPBkk = new C1DockingTabPage();
            tabOPBkk.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOPBkk.Size = new System.Drawing.Size(667, 175);
            tabOPBkk.TabIndex = 0;
            tabOPBkk.Text = "OPBkk Claim";
            tabOPBkk.Name = "tabStfNote";
            tcMain.Controls.Add(tabOPBkk);

            tabMainPaidTyp = new C1DockingTabPage();
            tabMainPaidTyp.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabMainPaidTyp.Size = new System.Drawing.Size(667, 175);
            tabMainPaidTyp.TabIndex = 1;
            tabMainPaidTyp.Text = "Paid Type สิทธิ";
            tabMainPaidTyp.Name = "tabMainPaidTyp";
            tcMain.Controls.Add(tabMainPaidTyp);
            grfPaidTyp = new C1FlexGrid();
            grfPaidTyp.Font = fEdit;
            grfPaidTyp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPaidTyp.Location = new System.Drawing.Point(0, 0);
            grfPaidTyp.Rows.Count = 1;
            tabMainPaidTyp.Controls.Add(grfPaidTyp);
            theme1.SetTheme(grfPaidTyp, "Office2010Red");

            tabMainClinic = new C1DockingTabPage();
            tabMainClinic.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabMainClinic.Size = new System.Drawing.Size(667, 175);
            tabMainClinic.TabIndex = 1;
            tabMainClinic.Text = "Clinic";
            tabMainClinic.Name = "tabMainClinic";
            tcMain.Controls.Add(tabMainClinic);
            grfClinic = new C1FlexGrid();
            grfClinic.Font = fEdit;
            grfClinic.Dock = System.Windows.Forms.DockStyle.Fill;
            grfClinic.Location = new System.Drawing.Point(0, 0);
            grfClinic.Rows.Count = 1;
            tabMainClinic.Controls.Add(grfClinic);
            theme1.SetTheme(grfClinic, "Office2010Red");

            tcOPBKK = new C1DockingTab();
            tcOPBKK.Dock = System.Windows.Forms.DockStyle.Fill;
            tcOPBKK.Name = "tcOPBKK";
            tabOPBKKPtt = new C1DockingTabPage();
            tabOPBKKPtt.TabIndex = 0;
            tabOPBKKPtt.Text = "Patient";
            tabOPBKKPtt.Name = "tabOPBKKPtt";
            tcOPBKK.Controls.Add(tabOPBKKPtt);

            pnOPBKK = new Panel();
            pnOPBKK.Controls.Add(tcOPBKK);

            lbDateStart = new Label();
            txtDateStart = new C1DateEdit();
            lbDateEnd = new Label();
            txtDateEnd = new C1DateEdit();
            btnOPBKKSelect = new C1Button();
            btnOPBkkGen = new C1Button();
            lbtxtPaidType = new Label();
            txtPaidType = new C1TextBox();
            lbtxtHospMain = new Label();
            txtHospMain = new C1TextBox();
            lbtxtHCode = new Label();
            txtHCode = new C1TextBox();

            //gapY += gapLine;
            bc.setControlLabel(ref lbDateStart, fEdit, "วันที่เริ่มต้น :", "lbDateStart", gapX , gapY);
            size = bc.MeasureString(lbDateStart);
            bc.setControlC1DateTimeEdit(ref txtDateStart, "txtDateStart", lbDateStart.Location.X + size.Width + 5, gapY);
            size = bc.MeasureString(lbDateStart);
            bc.setControlC1DateTimeEdit(ref txtDateStart, "txtDateStart", lbDateStart.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbDateEnd, fEdit, "วันที่เริ่มต้น :", "lbDateEnd", txtDateStart.Location.X + txtDateStart.Width+20, gapY);
            size = bc.MeasureString(lbDateEnd);
            bc.setControlC1DateTimeEdit(ref txtDateEnd, "txtDateEnd", lbDateEnd.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtPaidType, fEdit, "สิทธิ (xx,xx,xx,...) :", "lbtxtPaidType", txtDateEnd.Location.X + txtDateEnd.Width + 20, gapY);
            size = bc.MeasureString(lbtxtPaidType);
            bc.setControlC1TextBox(ref txtPaidType, fEdit, "txtPaidType",120, lbtxtPaidType.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtHospMain, fEdit, "HospMain :", "lbtxtHospMain", txtPaidType.Location.X + txtPaidType.Width + 25, gapY);
            size = bc.MeasureString(lbtxtHospMain);
            bc.setControlC1TextBox(ref txtHospMain, fEdit, "txtHospMain", 120, lbtxtHospMain.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtHCode, fEdit, "HCODE :", "lbtxtHCode", txtHospMain.Location.X + txtHospMain.Width + 20, gapY);
            size = bc.MeasureString(lbtxtHCode);
            bc.setControlC1TextBox(ref txtHCode, fEdit, "txtHCode", 120, lbtxtHCode.Location.X + size.Width + 5, gapY);

            bc.setControlC1Button(ref btnOPBKKSelect, fEdit, "ดึงข้อมูล", "btnOPBKKSelect", txtHCode.Location.X + txtHCode.Width + 20, gapY);
            bc.setControlC1Button(ref btnOPBkkGen, fEdit, "gen Text", "btnOPBkkGen", btnOPBKKSelect.Location.X + btnOPBKKSelect.Width + 20, gapY);

            btnOPBKKSelect.Click += BtnOPBKKSelect_Click;

            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);

            tabOPBkk.Controls.Add(lbDateStart);
            tabOPBkk.Controls.Add(txtDateStart);
            tabOPBkk.Controls.Add(lbDateEnd);
            tabOPBkk.Controls.Add(txtDateEnd);
            tabOPBkk.Controls.Add(btnOPBKKSelect);
            tabOPBkk.Controls.Add(btnOPBkkGen);
            tabOPBkk.Controls.Add(lbtxtPaidType);
            tabOPBkk.Controls.Add(txtPaidType);
            tabOPBkk.Controls.Add(lbtxtHospMain);
            tabOPBkk.Controls.Add(txtHospMain);
            tabOPBkk.Controls.Add(lbtxtHCode);
            tabOPBkk.Controls.Add(txtHCode);
            tabOPBkk.Controls.Add(pnOPBKK);

            initGrfSelect();
            initGrfOrd();

            this.Controls.Add(tcMain);

            theme1.SetTheme(this, bc.iniC.themeApp);
            theme1.SetTheme(tcMain, bc.iniC.themeApp);
            theme1.SetTheme(lbDateStart, bc.iniC.themeApp);
            theme1.SetTheme(txtDateStart, bc.iniC.themeApp);
            theme1.SetTheme(lbDateEnd, bc.iniC.themeApp);
            theme1.SetTheme(txtDateEnd, bc.iniC.themeApp);
            theme1.SetTheme(btnOPBKKSelect, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtPaidType, bc.iniC.themeApp);
            theme1.SetTheme(btnOPBkkGen, bc.iniC.themeApp);
            theme1.SetTheme(txtPaidType, bc.iniC.themeApp);
            theme1.SetTheme(tcOPBKK, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtHospMain, bc.iniC.themeApp);
            theme1.SetTheme(txtHospMain, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtHCode, bc.iniC.themeApp);
            theme1.SetTheme(txtHCode, bc.iniC.themeApp);
        }
        private void initCompomentOPBKK()
        {
            if (tabOPBKKINS != null) tabOPBKKINS.Dispose();
            if (tabOPBKKPAT != null) tabOPBKKPAT.Dispose();
            if (tabOPBKKOPD != null) tabOPBKKOPD.Dispose();
            if (tabOPBKKODX != null) tabOPBKKODX.Dispose();
            if (tabOPBKKOOP != null) tabOPBKKOOP.Dispose();
            if (tabOPBKKORF != null) tabOPBKKORF.Dispose();
            if (tabOPBKKCHT != null) tabOPBKKCHT.Dispose();
            if (tabOPBKKCHA != null) tabOPBKKCHA.Dispose();
            if (tabOPBKKAER != null) tabOPBKKAER.Dispose();
            if (tabOPBKKLABFU != null) tabOPBKKLABFU.Dispose();
            if (tabOPBKKDRU != null) tabOPBKKDRU.Dispose();
            if (tabOPBKKCHAD != null) tabOPBKKCHAD.Dispose();

            tabOPBKKINS = new C1DockingTabPage();
            tabOPBKKINS.TabIndex = 0;
            tabOPBKKINS.Text = "INS";
            tabOPBKKINS.Name = "tabOPBKKINS";
            tcOPBKK.Controls.Add(tabOPBKKINS);

            tabOPBKKPAT = new C1DockingTabPage();
            tabOPBKKPAT.TabIndex = 0;
            tabOPBKKPAT.Text = "PAT";
            tabOPBKKPAT.Name = "tabOPBKKPAT";
            tcOPBKK.Controls.Add(tabOPBKKPAT);

            tabOPBKKOPD = new C1DockingTabPage();
            tabOPBKKOPD.TabIndex = 0;
            tabOPBKKOPD.Text = "PAT";
            tabOPBKKOPD.Name = "tabOPBKKOPD";
            tcOPBKK.Controls.Add(tabOPBKKOPD);

            tabOPBKKODX = new C1DockingTabPage();
            tabOPBKKODX.TabIndex = 0;
            tabOPBKKODX.Text = "ODX";
            tabOPBKKODX.Name = "tabOPBKKODX";
            tcOPBKK.Controls.Add(tabOPBKKODX);

            tabOPBKKOOP = new C1DockingTabPage();
            tabOPBKKOOP.TabIndex = 0;
            tabOPBKKOOP.Text = "OOP";
            tabOPBKKOOP.Name = "tabOPBKKOOP";
            tcOPBKK.Controls.Add(tabOPBKKOOP);

            tabOPBKKORF = new C1DockingTabPage();
            tabOPBKKORF.TabIndex = 0;
            tabOPBKKORF.Text = "ORF";
            tabOPBKKORF.Name = "tabOPBKKORF";
            tcOPBKK.Controls.Add(tabOPBKKORF);

            tabOPBKKCHT = new C1DockingTabPage();
            tabOPBKKCHT.TabIndex = 0;
            tabOPBKKCHT.Text = "CHT";
            tabOPBKKCHT.Name = "tabOPBKKCHT";
            tcOPBKK.Controls.Add(tabOPBKKCHT);

            tabOPBKKCHA = new C1DockingTabPage();
            tabOPBKKCHA.TabIndex = 0;
            tabOPBKKCHA.Text = "CHA";
            tabOPBKKCHA.Name = "tabOPBKKCHA";
            tcOPBKK.Controls.Add(tabOPBKKCHA);

            tabOPBKKAER = new C1DockingTabPage();
            tabOPBKKAER.TabIndex = 0;
            tabOPBKKAER.Text = "AER";
            tabOPBKKAER.Name = "tabOPBKKAER";
            tcOPBKK.Controls.Add(tabOPBKKAER);

            tabOPBKKLABFU = new C1DockingTabPage();
            tabOPBKKLABFU.TabIndex = 0;
            tabOPBKKLABFU.Text = "LABFU";
            tabOPBKKLABFU.Name = "tabOPBKKLABFU";
            tcOPBKK.Controls.Add(tabOPBKKLABFU);

            tabOPBKKDRU = new C1DockingTabPage();
            tabOPBKKDRU.TabIndex = 0;
            tabOPBKKDRU.Text = "DRU";
            tabOPBKKDRU.Name = "tabOPBKKDRU";
            tcOPBKK.Controls.Add(tabOPBKKLABFU);

            tabOPBKKCHAD = new C1DockingTabPage();
            tabOPBKKCHAD.TabIndex = 0;
            tabOPBKKCHAD.Text = "CHAD";
            tabOPBKKCHAD.Name = "tabOPBKKCHAD";
            tcOPBKK.Controls.Add(tabOPBKKCHAD);


        }
        private void initGrfOrd()
        {
            if (tabOrd != null) tabOrd.Dispose();

            tabOrd = new C1DockingTabPage();
            tabOrd.TabIndex = 0;
            tabOrd.Text = "Order";
            tabOrd.Name = "tabOrd";
            tcOPBKK.Controls.Add(tabOrd);

            if (grfOrd != null) grfOrd.Dispose();

            grfOrd = new C1FlexGrid();
            grfOrd.Font = fEdit;
            grfOrd.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOrd.Location = new System.Drawing.Point(0, 0);
            grfOrd.Rows.Count = 1;
            tabOrd.Controls.Add(grfOrd);
            theme1.SetTheme(grfOrd, "Office2010Red");
        }
        private void BtnOPBKKSelect_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            dtPtt = new DataTable();
            String datestart = "", dateend = "", paidtype="";
            datestart = bc.datetoDB(txtDateStart.Text);
            dateend = bc.datetoDB(txtDateEnd.Text);
            //paidtype = txtPaidType.Text.Trim();
            String[] paid = txtPaidType.Text.Trim().Split(',');
            if (paid.Length > 0)
            {
                foreach(String txt in paid)
                {
                    paidtype += "'" + txt + "',";
                }
                if (paidtype.Length > 1)
                {
                    paidtype = paidtype.Substring(0, paidtype.Length - 1);
                }
            }
            else
            {
                paidtype = txtPaidType.Text.Trim();
            }
            grfSelect.Cols[colSelectHn].Caption = "HN";
            grfSelect.Cols[colSelectName].Caption = "Name";
            grfSelect.Cols[colSelectVsDate].Caption = "vsdate";
            grfSelect.Cols[colSelectPaidType].Caption = "สิทธิ";
            grfSelect.Cols[colSelectSymptoms].Caption = "อาการเบื้องต้น";
            grfSelect.Cols[colSelectHnYear].Caption = "hnyr";
            grfSelect.Cols[colSelectPreno].Caption = "preno";
            grfSelect.Cols[colSelectHn].Width = 80;
            grfSelect.Cols[colSelectName].Width = 300;
            grfSelect.Cols[colSelectVsDate].Width = 100;
            grfSelect.Cols[colSelectPaidType].Width = 100;
            grfSelect.Cols[colSelectSymptoms].Width = 300;
            grfSelect.Cols[colSelectHnYear].Width = 60;
            grfSelect.Cols[colSelectPreno].Width = 60;
            showLbLoading();
            dtPtt = bc.bcDB.vsDB.selectVisitByDatePaidType(paidtype, datestart, dateend);
            grfSelect.Cols.Count = 8;
            grfSelect.Rows.Count = 1;
            grfSelect.Rows.Count = dtPtt.Rows.Count+1;
            int i = 1;
            foreach (DataRow drow in dtPtt.Rows)
            {
                grfSelect[i, colSelectHn] = drow["MNC_HN_NO"].ToString();
                grfSelect[i, colSelectName] = drow["prefix"].ToString() + " " + drow["MNC_FNAME_T"].ToString() + " " + drow["MNC_LNAME_T"].ToString();
                grfSelect[i, colSelectVsDate] = drow["mnc_date"].ToString();
                grfSelect[i, colSelectPaidType] = drow["MNC_FN_TYP_DSC"].ToString();
                grfSelect[i, colSelectSymptoms] = drow["MNC_SHIF_MEMO"].ToString();
                grfSelect[i, colSelectHnYear] = drow["MNC_HN_YR"].ToString();
                grfSelect[i, colSelectPreno] = drow["mnc_pre_no"].ToString();
                grfSelect[i, 0] = i;
                i++;
            }
            grfSelect.Cols[colSelectHn].AllowEditing = false;
            grfSelect.Cols[colSelectName].AllowEditing = false;
            grfSelect.Cols[colSelectVsDate].AllowEditing = false;
            grfSelect.Cols[colSelectPaidType].AllowEditing = false;
            grfSelect.Cols[colSelectSymptoms].AllowEditing = false;
            grfSelect.Cols[colSelectHnYear].AllowEditing = false;
            grfSelect.Cols[colSelectPreno].AllowEditing = false;

            initCompomentOPBKK();
            initGrfTabOPBKK();

            hideLbLoading();
        }

        private void setControl()
        {
            txtDateStart.Value = DateTime.Now;
            txtDateEnd.Value = DateTime.Now;
            setGrfPaidType();
            setGrfClinic();
        }
        private void initGrfSelect()
        {
            grfSelect = new C1FlexGrid();
            grfSelect.Font = fEdit;
            grfSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSelect.Location = new System.Drawing.Point(0, 0);
            grfSelect.Rows.Count = 1;
            grfSelect.DoubleClick += GrfSelect_DoubleClick;
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            tabOPBKKPtt.Controls.Add(grfSelect);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfSelect, "Office2010Red");
        }
        private void initGrfTabOPBKK()
        {
            if (grfINS != null) grfINS.Dispose();
            if (grfPAT != null) grfPAT.Dispose();
            if (grfOPD != null) grfOPD.Dispose();
            if (grfODX != null) grfODX.Dispose();
            if (grfOOP != null) grfOOP.Dispose();
            if (grfORF != null) grfORF.Dispose();
            if (grfCHT != null) grfCHT.Dispose();
            if (grfCHA != null) grfCHA.Dispose();
            if (grfAER != null) grfAER.Dispose();
            if (grfLABFU != null) grfLABFU.Dispose();
            if (grfDRU != null) grfDRU.Dispose();
            if (grfCHAD != null) grfCHAD.Dispose();

            grfINS = new C1FlexGrid();
            grfINS.Font = fEdit;
            grfINS.Dock = System.Windows.Forms.DockStyle.Fill;
            grfINS.Location = new System.Drawing.Point(0, 0);
            grfINS.Rows.Count = 1;
            tabOPBKKINS.Controls.Add(grfINS);
            theme1.SetTheme(grfINS, "Office2010Red");

            grfPAT = new C1FlexGrid();
            grfPAT.Font = fEdit;
            grfPAT.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPAT.Location = new System.Drawing.Point(0, 0);
            grfPAT.Rows.Count = 1;
            tabOPBKKPAT.Controls.Add(grfPAT);
            theme1.SetTheme(grfPAT, "Office2010Red");

            grfOPD = new C1FlexGrid();
            grfOPD.Font = fEdit;
            grfOPD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPD.Location = new System.Drawing.Point(0, 0);
            grfOPD.Rows.Count = 1;
            tabOPBKKOPD.Controls.Add(grfOPD);
            theme1.SetTheme(grfOPD, "Office2010Red");

            grfODX = new C1FlexGrid();
            grfODX.Font = fEdit;
            grfODX.Dock = System.Windows.Forms.DockStyle.Fill;
            grfODX.Location = new System.Drawing.Point(0, 0);
            grfODX.Rows.Count = 1;
            tabOPBKKODX.Controls.Add(grfODX);
            theme1.SetTheme(grfODX, "Office2010Red");

            grfOOP = new C1FlexGrid();
            grfOOP.Font = fEdit;
            grfOOP.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOOP.Location = new System.Drawing.Point(0, 0);
            grfOOP.Rows.Count = 1;
            tabOPBKKOOP.Controls.Add(grfOOP);
            theme1.SetTheme(grfOOP, "Office2010Red");

            grfORF = new C1FlexGrid();
            grfORF.Font = fEdit;
            grfORF.Dock = System.Windows.Forms.DockStyle.Fill;
            grfORF.Location = new System.Drawing.Point(0, 0);
            grfORF.Rows.Count = 1;
            tabOPBKKORF.Controls.Add(grfORF);
            theme1.SetTheme(grfORF, "Office2010Red");

            grfCHT = new C1FlexGrid();
            grfCHT.Font = fEdit;
            grfCHT.Dock = System.Windows.Forms.DockStyle.Fill;
            grfCHT.Location = new System.Drawing.Point(0, 0);
            grfCHT.Rows.Count = 1;
            tabOPBKKCHT.Controls.Add(grfCHT);
            theme1.SetTheme(grfCHT, "Office2010Red");

            grfCHA = new C1FlexGrid();
            grfCHA.Font = fEdit;
            grfCHA.Dock = System.Windows.Forms.DockStyle.Fill;
            grfCHA.Location = new System.Drawing.Point(0, 0);
            grfCHA.Rows.Count = 1;
            tabOPBKKCHA.Controls.Add(grfCHA);
            theme1.SetTheme(grfCHA, "Office2010Red");

            grfAER = new C1FlexGrid();
            grfAER.Font = fEdit;
            grfAER.Dock = System.Windows.Forms.DockStyle.Fill;
            grfAER.Location = new System.Drawing.Point(0, 0);
            grfAER.Rows.Count = 1;
            tabOPBKKAER.Controls.Add(grfAER);
            theme1.SetTheme(grfAER, "Office2010Red");

            grfLABFU = new C1FlexGrid();
            grfLABFU.Font = fEdit;
            grfLABFU.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLABFU.Location = new System.Drawing.Point(0, 0);
            grfLABFU.Rows.Count = 1;
            tabOPBKKLABFU.Controls.Add(grfLABFU);
            theme1.SetTheme(grfLABFU, "Office2010Red");

            grfDRU = new C1FlexGrid();
            grfDRU.Font = fEdit;
            grfDRU.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDRU.Location = new System.Drawing.Point(0, 0);
            grfDRU.Rows.Count = 1;
            tabOPBKKDRU.Controls.Add(grfDRU);
            theme1.SetTheme(grfDRU, "Office2010Red");

            grfCHAD = new C1FlexGrid();
            grfCHAD.Font = fEdit;
            grfCHAD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfCHAD.Location = new System.Drawing.Point(0, 0);
            grfCHAD.Rows.Count = 1;
            tabOPBKKCHAD.Controls.Add(grfCHAD);
            theme1.SetTheme(grfCHAD, "Office2010Red");
        }
        private void GrfSelect_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                setGrfOrdItem();
            }
            catch(Exception ex)
            {
                MessageBox.Show("error "+ex.Message, "");
            }
        }
        private void setGrfOrdItem()
        {
            String hn = "", hnyr = "", vsdate = "", preno = "";
            DataTable dt = new DataTable();

            hn = grfSelect[grfSelect.Row, colSelectHn].ToString();
            hnyr = grfSelect[grfSelect.Row, colSelectHnYear].ToString();
            vsdate = grfSelect[grfSelect.Row, colSelectVsDate].ToString();
            preno = grfSelect[grfSelect.Row, colSelectPreno].ToString();

            dt = bc.bcDB.pharT02DB.selectReq(hnyr, hn, vsdate, preno);
            grfOrd.Rows.Count = 1;
            grfOrd.Cols.Count = 11;
            grfOrd.Rows.Count = dt.Rows.Count + 1;
            
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                grfOrd[i, colOrdAddId] = row1["MNC_ph_cd"].ToString();
                grfOrd[i, colOrdAddNameT] = row1["mnc_ph_tn"].ToString();
                grfOrd[i, colOrdAddItemType] = row1["MNC_ph_cd"].ToString();
                grfOrd[i, colOrdAddUnit] = row1["mnc_ph_unt_cd"].ToString();
                grfOrd[i, colOrdAddDrugFr] = row1["mnc_ph_dir_dsc"].ToString();
                grfOrd[i, colOrdAddDrugIn] = row1["MNC_PH_CAU_dsc"].ToString();
                grfOrd[i, colOrdAddQty] = row1["MNC_ph_qty"].ToString();
                grfOrd[i, colOrdAddFlag] = row1["mnc_ph_typ_flg"].ToString();      //drug=P,supp=O,lab, xray
                grfOrd[i, 0] = i;
                if (row1["mnc_ph_typ_flg"].ToString().Equals("P"))
                {
                    grfOrd.Rows[i].StyleDisplay.BackColor = Color.FromArgb(143, 200, 127);
                }
                else if (row1["mnc_ph_typ_flg"].ToString().Equals("O"))
                {
                    grfOrd.Rows[i].StyleDisplay.BackColor = Color.FromArgb(244, 222, 242);
                }
            }
            
        }
        private void setGrfPaidType()
        {
            DataTable dt = new DataTable();
            C1ComboBox cboMethod = new C1ComboBox();
            cboMethod.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
            bc.setCboOPBKKINSCL(cboMethod, "");

            dt = bc.bcDB.finM02DB.SelectAll();
            grfPaidTyp.Rows.Count = 1;
            grfPaidTyp.Cols.Count = 7;
            grfPaidTyp.Rows.Count = dt.Rows.Count + 1;
            grfPaidTyp.Cols[colPaidTypId].Caption = "ID";
            grfPaidTyp.Cols[colPaidTypName].Caption = "Paid Name";
            grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Caption = "OPBKK Code";
            grfPaidTyp.Cols[colPaidTypFNSYS].Caption = "FN SYS";
            grfPaidTyp.Cols[colPaidTypPTTYP].Caption = "PT TYP";
            grfPaidTyp.Cols[colPaidTypAccNo].Caption = "ACC NO";
            grfPaidTyp.Cols[colPaidTypId].Width = 80;
            grfPaidTyp.Cols[colPaidTypName].Width = 300;
            grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Width = 120;
            grfPaidTyp.Cols[colPaidTypFNSYS].Width = 80;
            grfPaidTyp.Cols[colPaidTypPTTYP].Width = 80;
            grfPaidTyp.Cols[colPaidTypAccNo].Width = 80;
            grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Editor = cboMethod;
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                grfPaidTyp[i, colPaidTypId] = row1["MNC_FN_TYP_CD"].ToString();
                grfPaidTyp[i, colPaidTypName] = row1["MNC_FN_TYP_DSC"].ToString();
                grfPaidTyp[i, colPaidTypIdOpBkkCode] = row1["opbkk_inscl"].ToString();
                grfPaidTyp[i, colPaidTypFNSYS] = row1["MNC_FN_STS"].ToString();
                grfPaidTyp[i, colPaidTypPTTYP] = row1["PTTYP"].ToString();
                grfPaidTyp[i, colPaidTypAccNo] = row1["MNC_ACCOUNT_NO"].ToString();
                grfPaidTyp[i, 0] = i;
                if (i % 2 == 0)
                    grfPaidTyp.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                
            }
            grfPaidTyp.Cols[colPaidTypId].AllowEditing = false;
            grfPaidTyp.Cols[colPaidTypName].AllowEditing = false;
            grfPaidTyp.Cols[colPaidTypFNSYS].AllowEditing = false;
            grfPaidTyp.Cols[colPaidTypPTTYP].AllowEditing = false;
            grfPaidTyp.Cols[colPaidTypAccNo].AllowEditing = false;
        }
        private void setGrfClinic()
        {
            DataTable dt = new DataTable();
            C1ComboBox cboMethod = new C1ComboBox();
            cboMethod.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
            bc.setCboOPBKKClinic(cboMethod, "");

            dt = bc.bcDB.pttM32DB.SelectAll();
            grfClinic.Rows.Count = 1;
            grfClinic.Cols.Count = 8;
            grfClinic.Rows.Count = dt.Rows.Count + 1;
            grfClinic.Cols[colClinicmddepno].Caption = "DEP NO";
            grfClinic.Cols[colClinicsecno].Caption = "SEC NO";
            grfClinic.Cols[colClinicdivno].Caption = "DIV NO";
            grfClinic.Cols[colClinictyppt].Caption = "TYP PT";
            grfClinic.Cols[colClinicdepdsc].Caption = "DEP DSC";
            grfClinic.Cols[colClinicdpno].Caption = "DP NO";
            grfClinic.Cols[colClinicopbkkcode].Caption = "OP BKK Clinic";
            grfClinic.Cols[colClinicmddepno].Width = 80;
            grfClinic.Cols[colClinicsecno].Width = 80;
            grfClinic.Cols[colClinicdivno].Width = 80;
            grfClinic.Cols[colClinictyppt].Width = 80;
            grfClinic.Cols[colClinicdepdsc].Width = 300;
            grfClinic.Cols[colClinicdpno].Width = 120;
            grfClinic.Cols[colClinicopbkkcode].Width = 200;
            grfClinic.Cols[colClinicopbkkcode].Editor = cboMethod;
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                grfClinic[i, colClinicmddepno] = row1["mnc_md_dep_no"].ToString();
                grfClinic[i, colClinicsecno] = row1["mnc_sec_no"].ToString();
                grfClinic[i, colClinicdivno] = row1["MNC_DIV_NO"].ToString();
                grfClinic[i, colClinictyppt] = row1["MNC_TYP_PT"].ToString();
                grfClinic[i, colClinicdepdsc] = row1["mnc_md_dep_dsc"].ToString();
                grfClinic[i, colClinicdpno] = row1["MNC_DP_NO"].ToString();
                grfClinic[i, colClinicopbkkcode] = row1["opbkk_clinic"].ToString();
                grfClinic[i, 0] = i;
                if (i % 2 == 0)
                    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);

            }
            grfClinic.Cols[colClinicmddepno].AllowEditing = false;
            grfClinic.Cols[colClinicsecno].AllowEditing = false;
            grfClinic.Cols[colClinicdivno].AllowEditing = false;
            grfClinic.Cols[colClinictyppt].AllowEditing = false;
            grfClinic.Cols[colClinicdepdsc].AllowEditing = false;
            grfClinic.Cols[colClinicdpno].AllowEditing = false;
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
        private void genINS()
        {
            String year = "", mm = "";
            if (dtPtt.Rows.Count > 0)
            {
                showLbLoading();
                lINS.Clear();
                foreach (DataRow drow in dtPtt.Rows)
                {
                    OPBKKINS ins = new OPBKKINS();
                    ins.HN = drow["mnc_hn_no"].ToString();
                    ins.INSCL = drow["mnc_fn_typ_cd"].ToString();
                    ins.SUBTYPE = bc.datetoDB(drow["mnc_date"].ToString());
                    ins.CID = "";
                    ins.DATEIN = "";                                                //  วันเดือนปีที่มีสิทธิ ปีมีค่าเป็น ค.ศ
                    ins.DATEEXP = "";                                               //  วันเดือนปีที่หมดสิทธิ ปีมีค่าเป็น ค.ศ.
                    ins.HOSPMAIN = txtHospMain.Text.Trim();
                    ins.HOSPSUB = "";
                    ins.GOVCODE = "";
                    ins.GOVNAME = "";
                    ins.PERMITNO = "";
                    ins.DOCNO = "";
                    ins.OWNRPID = "";
                    ins.OWNNAME = "";
                    lINS.Add(ins);
                }
                
                hideLbLoading();
            }
        }
        private void genPAT()
        {
            String year = "", mm = "";
            if (dtPtt.Rows.Count > 0)
            {
                showLbLoading();
                lPAT.Clear();
                foreach (DataRow drow in dtPtt.Rows)
                {
                    String prov = "", amphu = "";
                    prov = drow["MNC_CUR_CHW"] != null ? drow["MNC_CUR_CHW"].ToString().Substring(0, 2) : "";
                    amphu = drow["MNC_CUR_AMP"] != null ? drow["MNC_CUR_AMP"].ToString().Substring(0, 2) : "";
                    OPBKKPAT pat = new OPBKKPAT();
                    pat.HCODE = txtHCode.Text.Trim();
                    pat.HN = drow["mnc_hn_no"].ToString();
                    pat.CHANGWAT = prov;
                    pat.AMPHUR = amphu;
                    pat.DOB = bc.datetoDB(drow["MNC_BDAY"].ToString());
                    pat.SEX = drow["MNC_SEX"] != null ? drow["MNC_SEX"].ToString().Equals("M") ? "1" : "2":"";
                    pat.MARRIAGE = "";
                    pat.OCCUPA = "";
                    pat.NATION = drow["MNC_NAT_CD"] != null ? drow["MNC_NAT_CD"].ToString() : "";
                    pat.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                    pat.NAMEPAT = drow["prefix"].ToString()+" "+ drow["MNC_FNAME_T"].ToString()+" "+ drow["MNC_LNAME_T"].ToString();
                    pat.TITLE = drow["mnc_pfix_cdt"] != null ? drow["mnc_pfix_cdt"].ToString() : "";
                    pat.FNAME = drow["MNC_FNAME_T"].ToString();
                    pat.LNAME = drow["MNC_LNAME_T"].ToString();
                    pat.IDTYPE = "1";                       //  ประเภทบัตร   1 = บัตรประชาชน  2 = หนังสือเดินทาง 3 = หนังสือต่างด้าว 4 = หนังสือ / เอกสารอื่นๆ
                    lPAT.Add(pat);
                }

                hideLbLoading();
            }
        }
        private void genOPD()
        {
            String year = "", mm = "";
            if (dtPtt.Rows.Count > 0)
            {
                showLbLoading();
                lOPD.Clear();
                foreach (DataRow drow in dtPtt.Rows)
                {
                    OPBKKOPD opd = new OPBKKOPD();
                    opd.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                    opd.HN = drow["mnc_hn_no"].ToString();
                    opd.DATEOPD = bc.datetoDB(drow["MNC_date"].ToString());
                    opd.TIMEOPD = ("0000"+drow["MNC_time"].ToString()).Substring(4);
                    opd.SEQ = drow["mnc_pre_no"].ToString();
                    opd.UUC = "1";                                                       //      การใช้สิทธิ (เพิ่มเติม) 1 = ใช้สิทธิ 2 = ไมใช้สิทธ
                    opd.DETAIL = drow["MNC_SHIF_MEMO"].ToString();
                    opd.BTEMP = drow["mnc_temp"] != null ? drow["mnc_temp"].ToString() : "";
                    opd.SBP = drow["mnc_temp"] != null ? drow["mnc_temp"].ToString() : "";                //      ความดันโลหิตค่าตัวบน
                    opd.DBP = drow["mnc_temp"] != null ? drow["mnc_temp"].ToString() : "";                //      ความดันโลหิตค่าตัวล่าง
                    opd.PR = drow["mnc_temp"] != null ? drow["mnc_temp"].ToString() : "";                 //      อัตราการเต้นหัวใจ
                    opd.RR = drow["mnc_temp"] != null ? drow["mnc_temp"].ToString() : "";                 //      อัตราการหายใจ
                    opd.OPTYPE = "AE";          // ประเภทการให้บริการ 0 = Refer ในบัญชีเครือข่ายเดียวกัน 1 = Refer นอกบัญชีเครือข่าย 2 = AE ในบัญชีเครือข่าย 3 = AE นอกบัญชีเครือข่าย 4 = OP พิการ 5 = OP บัตรตัวเอง 6 = Clearing House ศบส 7 = OP อื่นๆ(Individual data) 8 = ผู้ป่วยกึ่ง OP / IP(NONI) 9 = บริการแพทย์แผนไทย
                    opd.TYPEIN = "";                    //ประเภทการมารับบริการ   1 = มารับบริการเอง 2 = มารับบริการตามนัดหมาย 3 = ได้รับการส่งต่อจากสถานพยาบาลอื่น 4 = ได้รับการส่งตัวจากบริการ EMS
                    opd.TYPEOUT = "";                   //สถานะผู้ป่วยเมื่อเสร็จสิ้นบริการ 1 = จำหน่ายกลับบ้าน 2 = รับไว้รักษาต่อIP 3 = Refer ต่อ 4 = เสียชีวิต 5 = เสียชีวิตก่อนมาถึง 6 = เสียชีวิตระหว่างส่งต่อไปยังที่อื่น 7 = ปฏิเสธการรักษา 8 = หนีกลับ
                    opd.CLAIM_CODE = "";                //รหัส ClaimCode เข้ารับการบริการ ที่ออกโดย สปสช.       *ถ้ามีการขอเบิกชดเชยค่าบริการต้องส่งแนบมาด้วย
                    lOPD.Add(opd);
                }
                hideLbLoading();
            }
        }
        private void genORF()
        {
            String year = "", mm = "";
            if (dtPtt.Rows.Count > 0)
            {
                showLbLoading();
                lORF.Clear();
                foreach (DataRow drow in dtPtt.Rows)
                {
                    OPBKKORF orf = new OPBKKORF();
                    orf.HN = drow["mnc_hn_no"].ToString();
                    orf.DATEOPD = bc.datetoDB(drow["MNC_date"].ToString());
                    orf.CLINIC = "";        //รหัสคลินิกที่รับบริการ
                    orf.REFER = "";
                    orf.REFERTYPE = "";
                    orf.REFERDATE = "";
                    orf.SEQ = drow["mnc_pre_no"].ToString();

                    lORF.Add(orf);
                }

                hideLbLoading();
            }
        }
        private void genODX()
        {
            String year = "", mm = "";
            if (dtPtt.Rows.Count > 0)
            {
                showLbLoading();
                lODX.Clear();
                foreach (DataRow drow in dtPtt.Rows)
                {
                    OPBKKODX ins = new OPBKKODX();
                    ins.HN = drow["mnc_hn_no"].ToString();
                    ins.DATEDX = "";
                    ins.CLINIC = "";
                    ins.DIAG = "";
                    ins.DXTYPE = "";
                    ins.DRDX = "";
                    ins.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                    ins.SEQ = drow["mnc_pre_no"].ToString();
                    lODX.Add(ins);
                }
                hideLbLoading();
            }
        }
        private void genOOP()
        {
            String year = "", mm = "";
            if (dtPtt.Rows.Count > 0)
            {
                showLbLoading();
                lOOP.Clear();
                foreach (DataRow drow in dtPtt.Rows)
                {
                    OPBKKOOP ins = new OPBKKOOP();
                    ins.HN = drow["mnc_hn_no"].ToString();
                    ins.DATEOPD = bc.datetoDB(drow["MNC_date"].ToString());
                    ins.CLINIC = "";
                    ins.OPER = "";
                    ins.SERVPRICE = "";
                    ins.DROPID = "";
                    ins.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                    ins.SEQ = drow["mnc_pre_no"].ToString();
                    lOOP.Add(ins);
                }
                hideLbLoading();
            }
        }
        private void genCHT()
        {
            String year = "", mm = "";
            if (dtPtt.Rows.Count > 0)
            {
                showLbLoading();
                lCHT.Clear();
                foreach (DataRow drow in dtPtt.Rows)
                {
                    OPBKKCHT ins = new OPBKKCHT();
                    ins.HN = drow["mnc_hn_no"].ToString();
                    ins.DATEOPD = bc.datetoDB(drow["MNC_date"].ToString());
                    ins.ACTUALCHT = "";
                    ins.TOTAL = "";
                    ins.PAID = "";
                    ins.PTTYPE = "";
                    ins.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                    ins.SEQ = drow["mnc_pre_no"].ToString();
                    ins.OPD_MEMO = "";
                    ins.INVOICE_NO = "";
                    ins.INVOICE_LT = "";
                    lCHT.Add(ins);
                }
                hideLbLoading();
            }
        }
        private void genCHA()
        {
            String year = "", mm = "";
            if (dtPtt.Rows.Count > 0)
            {
                showLbLoading();
                lCHA.Clear();
                foreach (DataRow drow in dtPtt.Rows)
                {
                    OPBKKCHA ins = new OPBKKCHA();
                    ins.HN = drow["mnc_hn_no"].ToString();
                    ins.DATEOPD = bc.datetoDB(drow["MNC_date"].ToString());
                    ins.CHRGITEM = "";
                    ins.AMOUNT = "";
                    ins.AMOUNT_EXT = "";
                    ins.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                    ins.SEQ = drow["mnc_pre_no"].ToString();
                    lCHA.Add(ins);
                }
                hideLbLoading();
            }
        }
        private void genAER()
        {
            String year = "", mm = "";
            if (dtPtt.Rows.Count > 0)
            {
                showLbLoading();
                lAER.Clear();
                foreach (DataRow drow in dtPtt.Rows)
                {
                    OPBKKAER ins = new OPBKKAER();
                    ins.HN = drow["mnc_hn_no"].ToString();
                    ins.DATEOPD = bc.datetoDB(drow["MNC_date"].ToString());
                    ins.AUTHAE = "";
                    ins.AEDATE = "";
                    ins.AETIME = "";
                    ins.AETYPE = "";
                    ins.REFER_NO = "";
                    ins.REFMAINI = "";
                    ins.IREFTYPE = "";
                    ins.REFMAINO = "";
                    ins.OREFTYPE = "";
                    ins.UCAE = "";
                    ins.EMTYPE = "";
                    ins.SEQ = drow["mnc_pre_no"].ToString();
                    lAER.Add(ins);
                }
                hideLbLoading();
            }
        }
        private void genDRU()
        {
            String year = "", mm = "";
            if (dtPtt.Rows.Count > 0)
            {
                showLbLoading();
                lDRU.Clear();
                foreach (DataRow drow in dtPtt.Rows)
                {
                    OPBKKDRU ins = new OPBKKDRU();
                    ins.HCODE = txtHCode.Text.Trim();
                    ins.HN = drow["mnc_hn_no"].ToString();
                    ins.CLINIC = "";
                    ins.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                    ins.DATESERV = "";
                    ins.DID = "";
                    ins.DIDNAME = "";
                    ins.DIDSTD = "";
                    ins.TMTCODE = "";
                    ins.AMOUNT = "";
                    ins.DRUGPRICE = "";
                    ins.PRICE_EXT = "";
                    ins.DRUGCOST = "";
                    ins.UNIT = "";
                    ins.UNIT_PACK = "";
                    ins.SEQ = drow["mnc_pre_no"].ToString();
                    ins.PROVIDER = "";
                    lDRU.Add(ins);
                }
                hideLbLoading();
            }
        }
        private void genLABFU()
        {
            String year = "", mm = "";
            if (dtPtt.Rows.Count > 0)
            {
                showLbLoading();
                lLABFU.Clear();
                foreach (DataRow drow in dtPtt.Rows)
                {
                    OPBKKLABFU ins = new OPBKKLABFU();
                    ins.HCODE = txtHCode.Text.Trim();
                    ins.HN = drow["mnc_hn_no"].ToString();
                    ins.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                    ins.DATESERV = "";
                    ins.SEQ = drow["mnc_pre_no"].ToString();
                    lLABFU.Add(ins);
                }
                hideLbLoading();
            }
        }
        private void genCHAD()
        {
            String year = "", mm = "";
            if (dtPtt.Rows.Count > 0)
            {
                showLbLoading();
                lCHAD.Clear();
                foreach (DataRow drow in dtPtt.Rows)
                {
                    OPBKKCHAD ins = new OPBKKCHAD();
                    ins.HN = drow["mnc_hn_no"].ToString();
                    ins.DATESERV = "";
                    ins.SEQ = drow["mnc_pre_no"].ToString();
                    ins.CLINIC = "";
                    ins.ITEMTYPE = "";
                    ins.ITEMCODE = "";
                    ins.ITEMSRC = "";
                    ins.QTY = "";
                    ins.AMOUNT = "";
                    ins.AMOUNT_EXT = "";
                    ins.PROVIDER = "";
                    ins.ADDON_DESC = "";
                    lCHAD.Add(ins);
                }
                hideLbLoading();
            }
        }
        private void genTextINS(String pathfile)
        {
            string fileName = "INS", separate = "|";
            fileName = pathfile + fileName + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach (OPBKKINS ins in lINS)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.INSCL + separate + ins.SUBTYPE + separate + ins.CID + separate
                        + ins.DATEIN + separate + ins.DATEEXP + separate + ins.HOSPMAIN + separate + ins.HOSPSUB + separate
                        + ins.GOVCODE + separate + ins.GOVNAME + separate + ins.PERMITNO + separate + ins.DOCNO + separate
                        + ins.OWNRPID + separate + ins.OWNNAME + separate;
                    writetext.WriteLine(txt);
                }
            }
            grfINS.DataSource = lINS;
        }
        private void genTextPAT(String pathfile)
        {
            string fileName = "PAT", separate = "|";
            fileName = pathfile + fileName + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach (OPBKKPAT ins in lPAT)
                {
                    String txt = "";
                    txt = ins.HCODE + separate + ins.HN + separate + ins.CHANGWAT + separate + ins.AMPHUR + separate
                        + ins.DOB + separate + ins.SEX + separate + ins.MARRIAGE + separate + ins.OCCUPA + separate
                        + ins.NATION + separate + ins.PERSON_ID + separate + ins.NAMEPAT + separate + ins.TITLE + separate
                        + ins.FNAME + separate + ins.LNAME + separate + ins.IDTYPE + separate;
                    writetext.WriteLine(txt);
                }
            }
            grfPAT.DataSource = lPAT;
        }
        private void genTextOPD(String pathfile)
        {
            string fileName = "OPD", separate = "|";
            fileName = pathfile + fileName + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach (OPBKKOPD ins in lOPD)
                {
                    String txt = "";
                    txt = ins.PERSON_ID + separate + ins.HN + separate + ins.DATEOPD + separate + ins.TIMEOPD + separate
                        + ins.SEQ + separate + ins.UUC + separate + ins.DETAIL + separate + ins.BTEMP + separate
                        + ins.SBP + separate + ins.DBP + separate + ins.PR + separate + ins.RR + separate
                        + ins.OPTYPE + separate + ins.TYPEIN + separate + ins.TYPEOUT + separate + ins.CLAIM_CODE + separate;
                    writetext.WriteLine(txt);
                }
            }
            grfOPD.DataSource = lOPD;
        }
        private void genTextORF(String pathfile)
        {
            string fileName = "ORF", separate = "|";
            fileName = pathfile + fileName + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach (OPBKKORF ins in lORF)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.DATEOPD + separate + ins.CLINIC + separate + ins.REFER + separate
                        + ins.REFERTYPE + separate + ins.REFERDATE + separate + ins.SEQ + separate;
                    writetext.WriteLine(txt);
                }
            }
            grfORF.DataSource = lORF;
        }
        private void genTextODX(String pathfile)
        {
            string fileName = "ODX", separate = "|";
            fileName = pathfile + fileName + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach (OPBKKODX ins in lODX)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.DATEDX + separate + ins.CLINIC + separate + ins.DIAG + separate
                        + ins.DXTYPE + separate + ins.DRDX + separate + ins.PERSON_ID + separate + ins.SEQ + separate;
                    writetext.WriteLine(txt);
                }
            }
            grfODX.DataSource = lODX;
        }
        private void genTextOOP(String pathfile)
        {
            string fileName = "OOP", separate = "|";
            fileName = pathfile + fileName + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach (OPBKKOOP ins in lOOP)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.DATEOPD + separate + ins.CLINIC + separate + ins.OPER + separate
                        + ins.SERVPRICE + separate + ins.DROPID + separate + ins.PERSON_ID + separate + ins.SEQ + separate;
                    writetext.WriteLine(txt);
                }
            }
            grfOOP.DataSource = lOOP;
        }
        private void genTextCHT(String pathfile)
        {
            string fileName = "CHT", separate = "|";
            fileName = pathfile + fileName + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach (OPBKKCHT ins in lCHT)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.DATEOPD + separate + ins.ACTUALCHT + separate + ins.TOTAL + separate
                        + ins.PAID + separate + ins.PTTYPE + separate + ins.PERSON_ID + separate + ins.SEQ + separate
                        +ins.OPD_MEMO + separate + ins.INVOICE_NO + separate + ins.INVOICE_LT + separate;
                    writetext.WriteLine(txt);
                }
            }
            grfCHT.DataSource = lCHT;
        }
        private void genTextCHA(String pathfile)
        {
            string fileName = "CHA", separate = "|";
            fileName = pathfile + fileName + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach (OPBKKCHA ins in lCHA)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.DATEOPD + separate + ins.CHRGITEM + separate + ins.AMOUNT + separate
                        + ins.AMOUNT_EXT + separate + ins.PERSON_ID + separate + ins.SEQ + separate;
                    writetext.WriteLine(txt);
                }
            }
            grfCHA.DataSource = lCHA;
        }
        private void genTextAER(String pathfile)
        {
            string fileName = "AER", separate = "|";
            fileName = pathfile + fileName + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach (OPBKKAER ins in lAER)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.DATEOPD + separate + ins.AUTHAE + separate + ins.AEDATE + separate
                        + ins.AETIME + separate + ins.AETYPE + separate + ins.REFER_NO + separate + ins.REFMAINI + separate
                        +ins.IREFTYPE + separate + ins.REFMAINO + separate + ins.OREFTYPE + separate + ins.UCAE + separate
                        +ins.EMTYPE + separate + ins.SEQ + separate;
                    writetext.WriteLine(txt);
                }
            }
            grfAER.DataSource = lAER;
        }
        private void genTextDRU(String pathfile)
        {
            string fileName = "DRU", separate = "|";
            fileName = pathfile + fileName + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach (OPBKKDRU ins in lDRU)
                {
                    String txt = "";
                    txt = ins.HCODE + separate + ins.HN + separate + ins.CLINIC + separate + ins.PERSON_ID + separate
                        + ins.DATESERV + separate + ins.DID + separate + ins.DIDNAME + separate + ins.DIDSTD + separate
                        + ins.TMTCODE + separate + ins.AMOUNT + separate + ins.DRUGPRICE + separate + ins.PRICE_EXT + separate
                        + ins.DRUGCOST + separate + ins.UNIT + separate + ins.UNIT_PACK + separate + ins.SEQ + separate
                        + ins.PROVIDER + separate;
                    writetext.WriteLine(txt);
                }
            }
            grfDRU.DataSource = lDRU;
        }
        private void genTextLABFU(String pathfile)
        {
            string fileName = "LABFU", separate = "|";
            fileName = pathfile + fileName + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach (OPBKKLABFU ins in lLABFU)
                {
                    String txt = "";
                    txt = ins.HCODE + separate + ins.HN + separate + ins.PERSON_ID + separate + ins.DATESERV + separate
                        + ins.SEQ + separate + ins.LABTEST + separate + ins.LABRESULT + separate;
                    writetext.WriteLine(txt);
                }
            }
        }
        private void genTextCHAD(String pathfile)
        {
            string fileName = "CHAD", separate = "|";
            fileName = pathfile + fileName + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach (OPBKKCHAD ins in lCHAD)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.DATESERV + separate + ins.SEQ + separate + ins.CLINIC + separate
                        + ins.ITEMTYPE + separate + ins.ITEMCODE + separate + ins.ITEMSRC + separate + ins.QTY + separate
                        + ins.AMOUNT + separate + ins.AMOUNT_EXT + separate + ins.PROVIDER + separate + ins.ADDON_DESC + separate;
                    writetext.WriteLine(txt);
                }
            }
            grfCHAD.DataSource = lCHAD;
        }
        private void FrmOPBKKClaim_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            int scrH = Screen.PrimaryScreen.Bounds.Height;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Maximized;

            grfSelect.Size = new Size(scrW -20, scrH - btnOPBKKSelect.Location.Y -140);
            grfSelect.Location = new Point(5, btnOPBKKSelect.Location.Y + 40);

            pnOPBKK.Location = new Point(5, btnOPBKKSelect.Location.Y + 40);
            pnOPBKK.Size = new Size(scrW - 20, scrH - btnOPBKKSelect.Location.Y - 140);

            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();

            this.Text = "Last Update 2020-12-25";
        }
    }
}
