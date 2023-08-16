using bangna_hospital.control;
using bangna_hospital.object1;
using C1.C1Excel;
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
using System.Globalization;
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

        C1DockingTab tcMain, tcOP, tcOPBKK, tcUcep, tcSSO, tcDIT, tcCIPN, tcSSOP;
        C1DockingTabPage tabtcUcep, tabOPBkk, tabSSO, tabtcCIPN, tabtcSSOP;
        C1DockingTabPage tabOP,tabOPBKKPtt, tabOPBKKINS, tabOPBKKPAT, tabOPBKKOPD, tabOPBKKODX, tabOPBKKOOP, tabOPBKKORF, tabOPBKKCHT, tabOPBKKCHA, tabOPBKKAER, tabOPBKKLABFU, tabOPBKKDRU, tabOPBKKCHAD, tabOrd;
        C1DockingTabPage tabSSO1, tabUcepMain, tabUcepDrug, tabUcepCat, tabUcepLab, tabUcepXray, tabUcepHotCharge, tabUcepDIT, tabDITdc01, tabDITdc02, tabDITSupp, tabDITService;
        C1DockingTabPage tabOPBKKMainPaidTyp, tabOPBKKMainClinic, tabOPBKKMainCHRGITEM, tabOPBKKMainTmtCode;
        C1DockingTabPage tabCIPN, tabCIPNDrug, tabCIPNLab, tabCIPNItem, tabCIPNcsv;
        C1FlexGrid grfSelect, grfINS, grfPAT, grfOPD, grfODX, grfOOP, grfORF, grfCHT, grfCHA, grfAER, grfLABFU, grfDRU, grfCHAD, grfOrd, grfPaidTyp, grfClinic, grfOPBKKMainCHRGITEM, grfOPBKKMainTmtCode;
        C1FlexGrid grfUcepCat, grfUcepDrug, grfUcepSelect, grfUcepLab, grfUcepXray, grfUcepHotcharge, grfDITdc01, grfDITdc02, grfDITsupp, grfDITservice, grfUcepHnPaidType, grfUcepHnPaidTypeDetail;
        C1FlexGrid grfCipnDrug, grfCipnCsv, grfSSOP;

        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1PdfDocument pdfDoc;
        C1ThemeController theme1;
        Label lbDateStart, lbDateEnd, lbtxtPaidType, lbLoading, lbtxtHospMain, lbtxtHCode, lbtxtUcepDateStart, lbtxtUcepDateEnd, lbtxtUcepPaidType, lbtxtUcepHn, lbSSOPYear, lbSSOPMonth;
        C1DateEdit txtDateStart, txtDateEnd, txtUcepDateStart, txtUcepDateEnd;
        C1Button btnOPBKKSelect, btnOPBkkGen, btnUcepSelect, btnUcepTMTImport, btnUcepExcel, btnUcepDITdc01Open, btnUcepDITdc02Open, btnUcepDITsuppOpen, btnUcepDITserviceOpen, btnUcepDITdc01Read, btnUcepDITdc02Read, btnUcepDITsuppRead, btnUcepDITserviceRead, btnUcepDITdc01Write, btnUcepDITdc02Write;
        C1Button btnSSOPSearch, btnSSOPGen;
        C1ComboBox cboPaidType, cboSSOPMonth, cboSSOPYear;
        C1TextBox txtPaidType, txtHospMain, txtHCode, txtUcepHn, txtUcepPaidType, txtDITdc01StartRow, txtDITdc01CostNew, txtDITdc01PriceNew, txtDITdc01Filename, txtDITdc01Top200, txtDITdc02StartRow, txtDITdc02PriceNew, txtDITdc02Filename;
        Label lbtxtDITdc01StartRow, lbtxtDITdc01CostNew, lbtxtDITdc01PriceNew, lbtxtDITdc01Top200, lbtxtDITdc02StartRow, lbtxtDITdc02PriceNew, lbtxtDITsuppStartRow, lbtxtDITsuppCostNew, lbtxtDITsuppPriceNew, lbtxtDITserviceStartRow, lbtxtDITserviceCostNew, lbtxtDITservicePriceNew, lbtxtDITserviceFilename;
        C1TextBox txtDITsuppStartRow, txtDITsuppCostNew, txtDITsuppPriceNew, txtDITsuppFilename, txtDITserviceStartRow, txtDITserviceCostNew, txtDITservicePriceNew, txtDITserviceFilename;
        C1CheckBox chkUcepSelectAll, chkUcepAllVisit;
        Panel pnOPBKK, pnUcepMainTop, pnUcepMainBotton, pnDITdc01Top, pnDITdc02Top, pnDITsuppTop, pnDITserviceTop, pnDITdc01Botton, pnDITdc02Botton, pnDITsuppBotton, pnDITserviceBotton, pnSSOPTop, pnSSOPBotton;
        RadioButton chkOPBKKSelectVsDate, chkOPBKKSelectDiscDate;
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
        int colPaidTypId = 1, colPaidTypName = 2, colPaidTypIdOpBkkCode = 3, colPaidTypFNSYS=4, colPaidTypPTTYP=5, colPaidTypAccNo=6, colPaidTypflag=7, colPaidTypSSOPclaimCat = 8;
        int colClinicmddepno = 1, colClinicsecno = 2, colClinicdivno = 3, colClinictyppt = 4, colClinicdepdsc = 5, colClinicdpno = 7, colClinicopbkkcode = 6, colClinicopbkkFlag=7, colClinicopbkkSSOP = 8;
        int colCHRGITEMFNcode = 1, colCHRGITEMFNname = 2, colCHRGITEMFNGRPcode = 3, colCHRGITEMFNDEFcode = 4, colCHRGITEMsimbcode=5, colCHRGITEMchrcode=6, colCHRGITEMchrsubcode=7, colCHRGITEMcode = 8, colClinicCHRGITEMFlag = 9;

        int colINShn = 1, colINSinscl = 2, colINSsubtype = 3, colINScid = 4, colINSdatein = 5, colINSdateexp = 6, colINShospmain = 7, colINShospsub = 8, colINSgovcode = 8, colINSgovname = 9, colINSpermitno = 10, colINSdocno = 11, colINSownrpid = 12, colINSownname = 13, colINSflag=14;
        int colPAThcode = 1, colPAThn = 2, colPATchangwat = 3, colPATamphur = 4, colPATdob = 5, colPATsex = 6, colPATmarriage = 7, colPAToccupa = 8, colPATnation = 9, colPATpersonid = 10, colPATnamepat = 11, colPATtitle = 12, colPATfname = 13, colPATlname = 14, colPATidtype = 15, colPATflag=16;
        int colOPDpersonid = 1, colOPDhn = 2, colOPDdateopd = 3, colOPDtimeopd = 4, colOPDseq = 5, colOPDuuc = 6, colOPDdetail = 7, colOPDbtemp = 8, colOPDsbp = 9, colOPDdbp = 10, colOPDpr = 11, colOPDrr = 12, colOPDoptype = 13, colOPDtypein = 14, colOPDtypeout = 15, colOPDclaimcode = 16, colOPDflag=17;
        int colORFhn = 1, colORFdateopd = 2, colORFclinic = 3, colORFrefer = 4, colORFrefertype = 5, colORFreferdate = 6, colORFseq = 7, colORFflag=8;
        int colODXhn = 1, colODXdatedx = 2, colODXclinic = 3, colODXdiag = 4, colODXdxtype = 5, colODXdrdx = 6, colODXpersonid = 7, colODXseq = 8, colODXflag=9;
        int colOOPhn = 1, colOOPdateopd = 2, colOOPclinic = 3, colOOPoper = 4, colOOPservprice = 5, colOOPdropid = 6, colOOPpersonid = 7, colOOPseq=8, colOOPflag=9;
        int colCHThn=1,colCHTdateopd=2, colCHTactualcht=3, colCHTtotal=4, colCHTpaid=5, colCHTpttype=6, colCHTpersonid=7, colCHTseq=8, colCHTopd_memo=9, colCHTinvoiceno=10, colCHTinvoicelt=11,colCHTflag=12;
        int colCHAhn = 1, colCHAdateopd = 2, colCHAchrgitem = 3, colCHAamount = 4, colCHAamountext = 5, colCHApersonid = 6, colCHAseq = 7, colCHAflag=8;
        int colAERhn = 1, colAERdateopd = 2, colAERauthae = 3, colAERaedate = 4, colAERaetime = 5, colAERaetype = 6, colAERreferno = 7, colAERrefmaini = 8, colAERireftype=9, colAERrefmainno=10, colAERoreftype=11, colAERucae=12, colAERemtype=13, colAERseq=14, colAERflag=15;
        int colDRUhcode = 1, colDRUhn = 2, colDRUclinic = 3, colDRUpersonid = 4, colDRUdateserv = 5, colDRUdid = 6, colDRUdidname = 7, colDRUdidstd = 8, colDRUtmtcode = 9, colDRUamount = 10, colDRUdrugprice = 11, colDRUpriceext = 12, colDRUdrugcost = 13, colDRUunit = 14, colDRUunitpack = 15, colDRUseq = 16, colDRUprovider = 17, colDRUflag=8;
        int colLABFUhcode = 1, colLABFUhn = 2, colLABFUpersonid = 3, colLABFUdateserv = 4, colLABFUseq = 5, colLABFUlabtest = 6, colLABFUlabresult = 7, colLABFUflag=8;
        int colCHADhn = 1, colCHADdateserv = 2, colCHADseq = 3, colCHADclinic = 4, colCHADitemtype = 5, colCHADitemcode = 6, colCHADitemsrc = 7, colCHADqty = 8, colCHADamount = 9, colCHADamountext = 10, colCHADprovider=11, colCHADaddon_desc=12, colCHADflag=13;
        int colGrfUcepDrugDrugId = 1, colGrfUcepDrugDrugName = 2, colGrfUcepDrugOldCode=3, colGrfUcepDrugTmtCode = 4, colGrfUcepDrugPrice1=5, colGrfUcepDrugPrice2=6, colGrfUcepDrugFlag=7, colGrfUcepDrugOpbkkCode = 8, colGrfUcepDrugStatusDrug=9;
        int colGrfUcepPaidId = 1, colGrfUcepPaidName = 2, colGrfUcepPaidCatCode = 3, colGrfUcepPaiducepcode=4, colGrfUcepPaidFlag = 5, colGrfUcepPaidSSOP = 6;
        int colGrfUcepLabId = 1, colGrfUcepLabName = 2, colGrfUcepLabUcepCode = 3, colGrfUcepLabFlag = 4;
        int colGrfUcepXrayId = 1, colGrfUcepXrayName = 2, colGrfUcepXrayUcepCode = 3, colGrfUcepXrayFlag = 4;
        int colGrfUcepHotChargeId = 1, colGrfUcepHotChargeName = 2, colGrfUcepHotChargeUcepCode = 3, colGrfUcepHotChargeDitCode = 4, colGrfUcepHotChargeFlag = 5;
        int colGrfUcepSelectSelect=1, colGrfUcepSelectHn = 2, colGrfUcepSelectName = 3, colGrfUcepSelectVsDate = 4, colGrfUcepSelectPaidType = 5, colGrfUcepSelectSymptoms = 6, colGrfUcepSelectHnYear = 7, colGrfUcepSelectPreno = 8, colGrfUcepSelectStatusOPDIPD=9, colGrfUcepSelectAnNoShow=10, colGrfUcepSelectAnNo=11, colGrfUcepSelectAnYear=12, colGrfUcepSelectAnDate=13;

        Boolean pageLoad = false;
        String pathfile = "", fileNameINS = "INS", separate = "|", fileNamePAT = "PAT", fileNameOPD = "OPD", fileNameORF = "ORF", fileNameODX = "ODX", fileNameOOP = "OOP", fileNameCHT = "CHT";
        String fileNameCHA = "CHA", fileNameAER = "AER", fileNameDRU = "DRU", fileNameLABFU = "LABFU", fileNameCHAD = "CHAD", flagpaidtype="";

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

            //hideLbLoading();
        }

        private void BtnOPBkkGen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (txtHCode.Text.Length <= 0)
            {
                MessageBox.Show("รหัส HCODE ว่าง", "");
                return;
            }
            //if (txtHospMain.Text.Length <= 0)
            //{
            //    MessageBox.Show("รหัส HospMain ว่าง", "");
            //    return;
            //}
            String datetick = "";
            pathfile = bc.iniC.medicalrecordexportpath;
            datetick = DateTime.Now.Ticks.ToString();
            pathfile = pathfile + "\\" + datetick + "\\";
            if (!Directory.Exists(pathfile))
            {
                Directory.CreateDirectory(pathfile);
                Application.DoEvents();
            }
            setLbLoading("genINS");
            genINS("");
            setLbLoading("genPAT");
            genPAT("");
            setLbLoading("genOPD");
            genOPD("");
            setLbLoading("genORF");
            genORF("");
            setLbLoading("genODX");
            genODX("");
            setLbLoading("genOOP");
            genOOP("");
            setLbLoading("genCHT");
            genCHT("");
            setLbLoading("genCHA");
            genCHA("");
            setLbLoading("genAER");
            genAER("");
            setLbLoading("genDRU");
            genDRU("");
            setLbLoading("genLABFU");
            genLABFU("");
            setLbLoading("genCHAD");
            genCHAD("");

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
            tcMain.SelectedTabChanged += TcMain_SelectedTabChanged;

            tabOPBkk = new C1DockingTabPage();
            tabOPBkk.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOPBkk.Size = new System.Drawing.Size(667, 175);
            tabOPBkk.TabIndex = 0;
            tabOPBkk.Text = "OPBkk Claim";
            tabOPBkk.Name = "tabOPBkk";
            tcMain.Controls.Add(tabOPBkk);

            tcOP = new C1DockingTab();
            tcOP.Dock = System.Windows.Forms.DockStyle.Fill;
            tcOP.Name = "tcOP";

            tabOP = new C1DockingTabPage();
            tabOP.Dock = System.Windows.Forms.DockStyle.Fill;
            tabOP.Name = "tabOP";
            tabOP.Text = "OPBKK Main";
            tabOPBkk.Controls.Add(tcOP);
            tcOP.Controls.Add(tabOP);

            tabtcUcep = new C1DockingTabPage();
            tabtcUcep.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabtcUcep.Size = new System.Drawing.Size(667, 175);
            tabtcUcep.TabIndex = 0;
            tabtcUcep.Text = "UCEP";
            tabtcUcep.Name = "tabtcUcep";
            tcMain.Controls.Add(tabtcUcep);

            tabSSO = new C1DockingTabPage();
            tabSSO.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabSSO.Size = new System.Drawing.Size(667, 175);
            tabSSO.TabIndex = 0;
            tabSSO.Text = "ประกันสังคม";
            tabSSO.Name = "tabSSO";
            tcMain.Controls.Add(tabSSO);

            tabOPBKKMainPaidTyp = new C1DockingTabPage();
            tabOPBKKMainPaidTyp.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOPBKKMainPaidTyp.Size = new System.Drawing.Size(667, 175);
            tabOPBKKMainPaidTyp.TabIndex = 1;
            tabOPBKKMainPaidTyp.Text = "Paid Type สิทธิ INSCL";
            tabOPBKKMainPaidTyp.Name = "tabMainPaidTyp";
            tcOP.Controls.Add(tabOPBKKMainPaidTyp);
            grfPaidTyp = new C1FlexGrid();
            grfPaidTyp.Font = fEdit;
            grfPaidTyp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPaidTyp.Location = new System.Drawing.Point(0, 0);
            grfPaidTyp.Rows.Count = 1;
            grfPaidTyp.Name = "grfPaidTyp";
            grfPaidTyp.CellChanged += GrfPaidTyp_CellChanged;
            grfPaidTyp.AfterEdit += GrfPaidTyp_AfterEdit;
            tabOPBKKMainPaidTyp.Controls.Add(grfPaidTyp);
            theme1.SetTheme(grfPaidTyp, "Office2010Red");
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("save สิทธิ Paid Type", new EventHandler(ContextMenu_grfPaidTyp_save));
            grfPaidTyp.ContextMenu = menuGw;

            tabOPBKKMainClinic = new C1DockingTabPage();
            tabOPBKKMainClinic.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOPBKKMainClinic.Size = new System.Drawing.Size(667, 175);
            tabOPBKKMainClinic.TabIndex = 1;
            tabOPBKKMainClinic.Text = "Clinic";
            tabOPBKKMainClinic.Name = "tabMainClinic";
            tcOP.Controls.Add(tabOPBKKMainClinic);
            grfClinic = new C1FlexGrid();
            grfClinic.Font = fEdit;
            grfClinic.Dock = System.Windows.Forms.DockStyle.Fill;
            grfClinic.Location = new System.Drawing.Point(0, 0);
            grfClinic.Rows.Count = 1;
            grfClinic.Name = "grfClinic";
            grfClinic.CellChanged += GrfClinic_CellChanged;
            grfClinic.AfterEdit += GrfClinic_AfterEdit;
            tabOPBKKMainClinic.Controls.Add(grfClinic);
            theme1.SetTheme(grfClinic, "Office2010Red");
            ContextMenu menuGwClinic = new ContextMenu();
            menuGwClinic.MenuItems.Add("save Clinic code", new EventHandler(ContextMenu_grfClinic_save));
            grfClinic.ContextMenu = menuGwClinic;

            tabOPBKKMainCHRGITEM = new C1DockingTabPage();
            tabOPBKKMainCHRGITEM.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOPBKKMainCHRGITEM.Size = new System.Drawing.Size(667, 175);
            tabOPBKKMainCHRGITEM.TabIndex = 1;
            tabOPBKKMainCHRGITEM.Text = "CHRGITEM";
            tabOPBKKMainCHRGITEM.Name = "tabOPBKKMainCHRGITEM";
            tcOP.Controls.Add(tabOPBKKMainCHRGITEM);
            grfOPBKKMainCHRGITEM = new C1FlexGrid();
            grfOPBKKMainCHRGITEM.Font = fEdit;
            grfOPBKKMainCHRGITEM.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPBKKMainCHRGITEM.Location = new System.Drawing.Point(0, 0);
            grfOPBKKMainCHRGITEM.Rows.Count = 1;
            grfOPBKKMainCHRGITEM.Name = "grfOPBKKMainCHRGITEM";
            grfOPBKKMainCHRGITEM.CellChanged += GrfOPBKKMainCHRGITEM_CellChanged;
            tabOPBKKMainCHRGITEM.Controls.Add(grfOPBKKMainCHRGITEM);
            //tabOPBKKMainTmtCode
            tabOPBKKMainTmtCode = new C1DockingTabPage();
            tabOPBKKMainTmtCode.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOPBKKMainTmtCode.Size = new System.Drawing.Size(667, 175);
            tabOPBKKMainTmtCode.TabIndex = 1;
            tabOPBKKMainTmtCode.Text = "Drug TMTcode";
            tabOPBKKMainTmtCode.Name = "tabOPBKKMainTmtCode";
            tcOP.Controls.Add(tabOPBKKMainTmtCode);
            grfOPBKKMainTmtCode = new C1FlexGrid();
            grfOPBKKMainTmtCode.Font = fEdit;
            grfOPBKKMainTmtCode.Name = "grfOPBKKMainTmtCode";
            grfOPBKKMainTmtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPBKKMainTmtCode.Location = new System.Drawing.Point(0, 0);
            grfOPBKKMainTmtCode.Rows.Count = 1;
            grfOPBKKMainTmtCode.CellChanged += GrfOPBKKMainTmtCode_CellChanged;
            tabOPBKKMainTmtCode.Controls.Add(grfOPBKKMainTmtCode);

            theme1.SetTheme(grfOPBKKMainCHRGITEM, "Office2010Red");
            theme1.SetTheme(grfOPBKKMainTmtCode, "Office2010Red");
            ContextMenu menuGwCHRGITEM = new ContextMenu();
            menuGwCHRGITEM.MenuItems.Add("save CHRGITEM code", new EventHandler(ContextMenu_grfCHRGITEM_save));
            grfOPBKKMainCHRGITEM.ContextMenu = menuGwCHRGITEM;

            ContextMenu menuGwtmtcode = new ContextMenu();
            menuGwtmtcode.MenuItems.Add("save OPBKK code", new EventHandler(ContextMenu_grfOPBkkMain_save_OPBKKCode));
            menuGwtmtcode.MenuItems.Add("save เวชภัณฑ์ code", new EventHandler(ContextMenu_grfOPBkkMain_save_tmtcode));
            menuGwtmtcode.MenuItems.Add("Import TMT code", new EventHandler(ContextMenu_grfOPBkkMainTmtCode_import));

            grfOPBKKMainTmtCode.ContextMenu = menuGwtmtcode;

            tcOPBKK = new C1DockingTab();
            tcOPBKK.Dock = System.Windows.Forms.DockStyle.Fill;
            tcOPBKK.Name = "tcOPBKK";
            tabOPBKKPtt = new C1DockingTabPage();
            tabOPBKKPtt.TabIndex = 0;
            tabOPBKKPtt.Text = "Patient";
            tabOPBKKPtt.Name = "tabOPBKKPtt";
            tcOPBKK.Controls.Add(tabOPBKKPtt);

            tcSSO = new C1DockingTab();
            tcSSO.Dock = System.Windows.Forms.DockStyle.Fill;
            tcSSO.Name = "tcSSO";
            tcSSO.Text = "Print";
            tabSSO.Controls.Add(tcSSO);

            tabSSO1 = new C1DockingTabPage();
            tabSSO1.TabIndex = 0;
            tabSSO1.Text = "Print";
            tabSSO1.Name = "tabSSO1";
            tcSSO.Controls.Add(tabSSO1);

            tcUcep = new C1DockingTab();
            tcUcep.Dock = System.Windows.Forms.DockStyle.Fill;
            tcUcep.Name = "tcUcep";
            tcUcep.Text = "UCEP";
            tabtcUcep.Controls.Add(tcUcep);

            tabUcepMain = new C1DockingTabPage();
            tabUcepMain.TabIndex = 0;
            tabUcepMain.Text = "Main";
            tabUcepMain.Name = "tabUcep1";

            tabUcepDrug = new C1DockingTabPage();
            tabUcepDrug.TabIndex = 0;
            tabUcepDrug.Text = "Drug TMT";
            tabUcepDrug.Name = "tabUcepDrug";

            tabUcepCat = new C1DockingTabPage();
            tabUcepCat.TabIndex = 0;
            tabUcepCat.Text = "Category 1-16";
            tabUcepCat.Name = "tabUcepCat";

            tabUcepLab = new C1DockingTabPage();
            tabUcepLab.TabIndex = 0;
            tabUcepLab.Text = "Ucep LAB";
            tabUcepLab.Name = "tabUcepLab";

            tabUcepXray = new C1DockingTabPage();
            tabUcepXray.TabIndex = 0;
            tabUcepXray.Text = "Ucep Xray";
            tabUcepXray.Name = "tabUcepXray";

            tabUcepHotCharge = new C1DockingTabPage();
            tabUcepHotCharge.TabIndex = 0;
            tabUcepHotCharge.Text = "Ucep Hot Charge";
            tabUcepHotCharge.Name = "tabUcepHotCharge";

            //tabUcepDIT
            tabUcepDIT = new C1DockingTabPage();
            tabUcepDIT.TabIndex = 0;
            tabUcepDIT.Text = "DIT";
            tabUcepDIT.Name = "tabUcepDIT";

            tcDIT = new C1DockingTab();
            tcDIT.Dock = System.Windows.Forms.DockStyle.Fill;
            tcDIT.Name = "tcDIT";
            tcDIT.Text = "DIT";
            tabUcepDIT.Controls.Add(tcDIT);

            tabDITdc01 = new C1DockingTabPage();
            tabDITdc01.TabIndex = 0;
            tabDITdc01.Text = "dc 01";
            tabDITdc01.Name = "tabDITdc01";

            tabDITdc02 = new C1DockingTabPage();
            tabDITdc02.TabIndex = 0;
            tabDITdc02.Text = "dc 02";
            tabDITdc02.Name = "tabDITdc02";

            tabDITSupp = new C1DockingTabPage();
            tabDITSupp.TabIndex = 0;
            tabDITSupp.Text = "Supplies";
            tabDITSupp.Name = "tabDITSupp";

            tabDITService = new C1DockingTabPage();
            tabDITService.TabIndex = 0;
            tabDITService.Text = "Service";
            tabDITService.Name = "tabDITService";

            tabtcCIPN = new C1DockingTabPage();
            tabtcCIPN.TabIndex = 0;
            tabtcCIPN.Text = "CIPN";
            tabtcCIPN.Name = "tabtcCIPN";
            tcMain.Controls.Add(tabtcCIPN);

            tcCIPN = new C1DockingTab();
            tcCIPN.Dock = System.Windows.Forms.DockStyle.Fill;
            tcCIPN.Name = "tcDtcCIPNIT";
            tcCIPN.Text = "CIPN";
            tabtcCIPN.Controls.Add(tcCIPN);

            tabCIPN = new C1DockingTabPage();
            tabCIPN.TabIndex = 0;
            tabCIPN.Text = "CIPN";
            tabCIPN.Name = "tabCIPN";
            tcCIPN.Controls.Add(tabCIPN);

            tabCIPNDrug = new C1DockingTabPage();
            tabCIPNDrug.TabIndex = 0;
            tabCIPNDrug.Text = "DRUG catalog";
            tabCIPNDrug.Name = "tabCIPNDrug";
            tcCIPN.Controls.Add(tabCIPNDrug);

            tabCIPNLab = new C1DockingTabPage();
            tabCIPNLab.TabIndex = 0;
            tabCIPNLab.Text = "LAB catalog";
            tabCIPNLab.Name = "tabCIPNLab";
            tcCIPN.Controls.Add(tabCIPNLab);

            tabCIPNItem = new C1DockingTabPage();
            tabCIPNItem.TabIndex = 0;
            tabCIPNItem.Text = "Item catalog";
            tabCIPNItem.Name = "tabCIPNItem";
            tcCIPN.Controls.Add(tabCIPNItem);

            tabCIPNcsv = new C1DockingTabPage();
            tabCIPNcsv.TabIndex = 0;
            tabCIPNcsv.Text = "CSV";
            tabCIPNcsv.Name = "tabCIPNcsv";
            tcCIPN.Controls.Add(tabCIPNcsv);

            tabtcSSOP = new C1DockingTabPage();
            tabtcSSOP.TabIndex = 0;
            tabtcSSOP.Text = "SSOP";
            tabtcSSOP.Name = "tabtcSSOP";
            tcMain.Controls.Add(tabtcSSOP);

            tcUcep.Controls.Add(tabUcepMain);
            tcUcep.Controls.Add(tabUcepDrug);
            tcUcep.Controls.Add(tabUcepCat);
            tcUcep.Controls.Add(tabUcepLab);
            tcUcep.Controls.Add(tabUcepXray);
            tcUcep.Controls.Add(tabUcepHotCharge);
            tcUcep.Controls.Add(tabUcepDIT);

            tcDIT.Controls.Add(tabDITdc01);
            tcDIT.Controls.Add(tabDITdc02);
            tcDIT.Controls.Add(tabDITSupp);
            tcDIT.Controls.Add(tabDITService);

            pnOPBKK = new Panel();
            pnOPBKK.Controls.Add(tcOPBKK);
            Panel pnchkOPBKKSelectDiscDate = new Panel();
            pnchkOPBKKSelectDiscDate.Size = new Size(180, 60);

            pnSSOPTop = new Panel();
            
            pnSSOPTop.Dock = DockStyle.Top;
            pnSSOPTop.Height = 80;
            pnSSOPBotton = new Panel();
            tabtcSSOP.Controls.Add(pnSSOPBotton);
            tabtcSSOP.Controls.Add(pnSSOPTop);
            pnSSOPBotton.Dock = DockStyle.Fill;
            pnSSOPBotton.Height = 80;
            
            bc.setControlC1Button(ref btnSSOPSearch, fEdit, "ดึงข้อมูล", "btnSSOPSearch", 270, gapY);
            btnSSOPSearch.Width = 70;
            bc.setControlC1Button(ref btnSSOPGen, fEdit, "gen Text", "btnSSOPGen", 400, gapY);
            btnSSOPGen.Width = 70;
            pnSSOPTop.Controls.Add(btnSSOPSearch);
            pnSSOPTop.Controls.Add(btnSSOPGen);
            cboSSOPYear = new C1ComboBox();
            cboSSOPMonth = new C1ComboBox();
            lbSSOPYear = new Label();
            lbSSOPMonth = new Label();
            bc.setControlC1ComboBox(ref cboSSOPYear, "cboSSOPYear", 70, 40, gapY);
            bc.setControlC1ComboBox(ref cboSSOPMonth, "cboSSOPMonth", 90, 170, gapY);
            cboSSOPMonth.Font = fEdit;
            bc.setControlLabel(ref lbSSOPYear, fEdit,"ปี :", "lbSSOPYear", 10, gapY);
            bc.setControlLabel(ref lbSSOPMonth, fEdit,"เดือน :", "lbSSOPMonth", 120, gapY);
            pnSSOPTop.Controls.Add(cboSSOPYear);
            pnSSOPTop.Controls.Add(cboSSOPMonth);
            pnSSOPTop.Controls.Add(lbSSOPYear);
            pnSSOPTop.Controls.Add(lbSSOPMonth);
            bc.setCboYear(cboSSOPYear);
            bc.setCboMonth(cboSSOPMonth);
            btnSSOPSearch.Click += BtnSSOPSearch_Click;
            btnSSOPGen.Click += BtnSSOPGen_Click;

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
            lbLoading = new Label();

            chkOPBKKSelectDiscDate = new RadioButton();
            chkOPBKKSelectVsDate = new RadioButton();
            pnchkOPBKKSelectDiscDate.Controls.Add(chkOPBKKSelectVsDate);
            pnchkOPBKKSelectDiscDate.Controls.Add(chkOPBKKSelectDiscDate);

            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);
            //showLbLoading();

            initCompomentTabUcep();
            //gapY += gapLine;
            bc.setControlRadioBox(ref chkOPBKKSelectVsDate, fEdit, "ตาม visit date", "chkOPBKKSelectVsDate", 10, 5);
            bc.setControlRadioBox(ref chkOPBKKSelectDiscDate, fEdit, "ตาม discharge date", "chkOPBKKSelectDiscDate", 10, 25);

            bc.setControlLabel(ref lbDateStart, fEdit, "วันที่เริ่มต้น :", "lbDateStart", pnchkOPBKKSelectDiscDate.Location.X + pnchkOPBKKSelectDiscDate.Width + 10, gapY);
            size = bc.MeasureString(lbDateStart);
            bc.setControlC1DateTimeEdit(ref txtDateStart, "txtDateStart", lbDateStart.Location.X + size.Width + 5, gapY);
            size = bc.MeasureString(lbDateStart);
            //bc.setControlC1DateTimeEdit(ref txtDateStart, "txtDateStart", lbDateStart.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbDateEnd, fEdit, "วันที่สิ้นสุด :", "lbDateEnd", txtDateStart.Location.X + txtDateStart.Width+15, gapY);
            size = bc.MeasureString(lbDateEnd);
            bc.setControlC1DateTimeEdit(ref txtDateEnd, "txtDateEnd", lbDateEnd.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtPaidType, fEdit, "สิทธิ (xx,...) :", "lbtxtPaidType", txtDateEnd.Location.X + txtDateEnd.Width + 15, gapY);
            size = bc.MeasureString(lbtxtPaidType);
            bc.setControlC1TextBox(ref txtPaidType, fEdit, "txtPaidType",120, lbtxtPaidType.Location.X + size.Width + 5, gapY);
            //bc.setControlLabel(ref lbtxtHospMain, fEdit, "HospMain :", "lbtxtHospMain", txtPaidType.Location.X + txtPaidType.Width + 20, gapY);
            //size = bc.MeasureString(lbtxtHospMain);
            //bc.setControlC1TextBox(ref txtHospMain, fEdit, "txtHospMain", 80, lbtxtHospMain.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtHCode, fEdit, "HCODE :", "lbtxtHCode", txtPaidType.Location.X + txtPaidType.Width + 15, gapY);
            size = bc.MeasureString(lbtxtHCode);
            bc.setControlC1TextBox(ref txtHCode, fEdit, "txtHCode", 80, lbtxtHCode.Location.X + size.Width + 5, gapY);

            bc.setControlC1Button(ref btnOPBKKSelect, fEdit, "ดึงข้อมูล", "btnOPBKKSelect", txtHCode.Location.X + txtHCode.Width + 20, gapY);
            btnOPBKKSelect.Width = 70;
            bc.setControlC1Button(ref btnOPBkkGen, fEdit, "gen Text", "btnOPBkkGen", btnOPBKKSelect.Location.X + btnOPBKKSelect.Width + 20, gapY);
            btnOPBkkGen.Width = 80;
            btnOPBKKSelect.Click += BtnOPBKKSelect_Click;

            tabOP.Controls.Add(pnchkOPBKKSelectDiscDate);
            tabOP.Controls.Add(lbDateStart);
            tabOP.Controls.Add(txtDateStart);
            tabOP.Controls.Add(lbDateEnd);
            tabOP.Controls.Add(txtDateEnd);
            tabOP.Controls.Add(btnOPBKKSelect);
            tabOP.Controls.Add(btnOPBkkGen);
            tabOP.Controls.Add(lbtxtPaidType);
            tabOP.Controls.Add(txtPaidType);
            tabOP.Controls.Add(lbtxtHospMain);
            tabOP.Controls.Add(txtHospMain);
            tabOP.Controls.Add(lbtxtHCode);
            tabOP.Controls.Add(txtHCode);
            tabOP.Controls.Add(pnOPBKK);
            
            initGrfSelect();
            initGrfOrd();
            initGrfUcepDrug();
            initGrfUcepCat();
            initGrfUcepLab();
            initGrfUcepXray();
            initGrfUcepHotcharge();

            initGrfCipnDrug();
            initGrfCipnCSV();
            initGrfSSOP();

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
            theme1.SetTheme(tcOP, bc.iniC.themeApp);
            theme1.SetTheme(tcSSO, bc.iniC.themeApp);
            theme1.SetTheme(tcUcep, bc.iniC.themeApp);
            theme1.SetTheme(tcDIT, bc.iniC.themeApp);
            theme1.SetTheme(tcCIPN, bc.iniC.themeDonor);
        }

        private void BtnSSOPGen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                String HCODE = "", sessionNo = "", pathFile = "", XMLHeader = "<?xml version=\"1.0\" encoding=\"windows-874\"?>", FooterXML = "";
                String genDT = "", SPLIT = "|";
                StringBuilder XMLBillTran = new StringBuilder();
                StringBuilder XMLBillItem = new StringBuilder();

                DataTable dtssop = new DataTable();
                DataTable dtitem = new DataTable();
                DataTable dtdrug = new DataTable();
                DateTime dtstart = new DateTime();
                DateTime dtend = new DateTime();
                dtstart = DateTime.Parse((int.Parse(cboSSOPYear.Text) - 543).ToString() + "-" + ((ComboBoxItem)cboSSOPMonth.SelectedItem).Value + "-" + "01");
                dtstart = dtstart.Year < 2000 ? dtstart.AddYears(543) : dtstart;

                dtend = DateTime.Parse((int.Parse(cboSSOPYear.Text) - 543).ToString() + "-" + ((ComboBoxItem)cboSSOPMonth.SelectedItem).Value + "-" + "01");
                dtend = dtend.Year < 2000 ? dtend.AddYears(543) : dtend;
                dtend = dtend.AddMonths(1);
                dtend = dtend.AddDays(-1);

                dtssop = bc.bcDB.vsDB.selectSSOPBillTran(dtstart.Year.ToString() + "-" + dtstart.ToString("MM-dd"), dtend.Year.ToString() + "-" + dtend.ToString("MM-dd"));
                if (dtssop.Rows.Count <= 0)
                {
                    MessageBox.Show("ไม่พบข้อมูล ", "");
                    return ;
                }
                HCODE = bc.iniC.ssoid;
                sessionNo = "";
                
                XMLHeader = "<?xml version=\"1.0\" encoding=\"windows-874\"?>" + Environment.NewLine;
                XMLHeader += "<ClaimRec System=\"OP\" PayPlan=\"SS\" Version=\"0.93\">" + Environment.NewLine;
                XMLHeader += "<HCODE>"+ HCODE+"</HCODE>" + Environment.NewLine;
                XMLHeader += "<HNAME>" + bc.iniC.hostname + "</HNAME>" + Environment.NewLine;
                XMLHeader += "<DATETIME>" + genDT + "</DATETIME>" + Environment.NewLine;
                XMLHeader += "<SESSNO>" + sessionNo + "</SESSNO>" + Environment.NewLine;
                XMLHeader += "<RECCOUNT>" + dtssop.Rows.Count + "</RECCOUNT>" + Environment.NewLine;
                dtssop.Columns.Add("invno", typeof(String));
                XMLBillTran.Append("<BILLTRAN>" + Environment.NewLine);
                String authcode = "", memberno = "", paid = "0.00", payplan = "80", otherpay = "0.00", vercode = "", tflag = "A";
                foreach (DataRow rowSSOP in dtssop.Rows)
                {
                    String station = "", dttran = "", hcode = "", invno = "", billno = "", hn = "", amount = "", pid = "", name = "", hmain = "", claimamt = "", otherpayplan = "", docno="";
                    float amt1 = 0;
                    docno = "000000000"+rowSSOP["mnc_doc_no"].ToString();
                    docno = docno.Substring(docno.Length - 9);
                    //station = rowSSOP["mnc_sec_no"].ToString();
                    //authcode = "";
                    //dttran = rowSSOP["mnc_date"].ToString();
                    invno = rowSSOP["mnc_doc_yr"].ToString() + docno;
                    rowSSOP["invno"] = invno;
                    billno = "";
                    //hn = rowSSOP["MNC_HN_NO"].ToString();
                    //memberno = "";
                    float.TryParse(rowSSOP["mnc_sum_pri"].ToString(), out amt1);
                    amount = amt1.ToString("0.00");
                    //paid = "0.00";
                    //vercode = "";
                    //tflag = "A";
                    //pid = rowSSOP["mnc_id_no"].ToString();
                    name = rowSSOP["MNC_PFIX_DSC"].ToString()+" "+ rowSSOP["MNC_FNAME_T"].ToString()+" "+ rowSSOP["MNC_LNAME_T"].ToString();
                    hmain = rowSSOP["MNC_FN_TYP_CD"].ToString().Equals("46") ? "24638" : rowSSOP["MNC_FN_TYP_CD"].ToString().Equals("45") ? "11772" : rowSSOP["MNC_FN_TYP_CD"].ToString().Equals("44") ? "1506" : "";
                    //payplan = "80";
                    claimamt = amt1.ToString("0.00");
                    otherpayplan = "";
                    //otherpay = "0.00";

                    //dtitem = bc.bcDB.vsDB.selectFinT03(rowSSOP["MNC_HN_NO"].ToString(), rowSSOP["MNC_PRE_NO"].ToString(), rowSSOP["mnc_date"].ToString(), rowSSOP["mnc_doc_no"].ToString());

                    XMLBillTran.Append(rowSSOP["mnc_sec_no"].ToString() + SPLIT + authcode + SPLIT + rowSSOP["mnc_date"].ToString() + SPLIT + HCODE + SPLIT + invno + SPLIT + billno + SPLIT + rowSSOP["MNC_HN_NO"].ToString() + SPLIT + memberno
                        + SPLIT + amount + SPLIT + paid + SPLIT + vercode + SPLIT + tflag + SPLIT + rowSSOP["mnc_id_no"].ToString() + SPLIT + name + SPLIT + hmain
                        + SPLIT + payplan + SPLIT + claimamt + SPLIT + otherpayplan + SPLIT + otherpay + Environment.NewLine);
                }
                XMLBillTran.Append("</BILLTRAN>" + Environment.NewLine);
                XMLBillTran.Append("<BillItems>" + Environment.NewLine);
                
                dtitem = bc.bcDB.vsDB.selectSSOPFinT03(dtstart.Year.ToString() + "-" + dtstart.ToString("MM-dd"), dtend.Year.ToString() + "-" + dtend.ToString("MM-dd"));
                foreach (DataRow rowSSOP in dtssop.Rows)
                {
                    DataRow[] results = dtitem.Select("MNC_HN_NO = "+ rowSSOP["MNC_HN_NO"].ToString()+ " and mnc_doc_no = " + rowSSOP["mnc_doc_no"].ToString());
                    String svrefID = "";
                    foreach (DataRow items in results)
                    {
                        svrefID = items["MNC_FN_DAT"].ToString().Replace("-","").Replace("-", "") + items["MNC_FN_NO"].ToString();
                        XMLBillTran.Append(rowSSOP["invno"].ToString() + SPLIT + items["mnc_doc_dat"].ToString() + SPLIT + items["ssop_billmuad"].ToString()
                            + SPLIT + items["mnc_fn_cd"].ToString() + SPLIT + items["ucep_code"].ToString() + SPLIT + items["MNC_FN_DSCT"].ToString()
                            + SPLIT + items["MNC_QTY"].ToString() + SPLIT + items["MNC_FN_AMT"].ToString() + SPLIT + items["MNC_FN_AMT"].ToString()
                            + SPLIT + items["MNC_FN_AMT"].ToString() + SPLIT + svrefID + SPLIT + items["ssop_claimcat"].ToString() + Environment.NewLine);
                    }
                }
                XMLBillTran.Append("</BillItems>" + Environment.NewLine);

                String fileNameBillTran = "BILLTRAN", yyyymmdd="", foldername="", ext=".TXT";
                foldername = (int.Parse(cboSSOPYear.Text) - 543).ToString()+ ((ComboBoxItem)cboSSOPMonth.SelectedItem).Value;
                yyyymmdd = DateTime.Now.Year>2500 ? (DateTime.Now.Year - 543).ToString() : DateTime.Now.Year.ToString();
                yyyymmdd += DateTime.Now.ToString("MMdd");
                fileNameBillTran += yyyymmdd + ext;
                pathFile = bc.iniC.ssopXmlPath + "\\" + foldername;

                String md5aipn = bc.ComputeMD5(XMLBillTran.ToString());

                byte[] wind874Bytes = Encoding.UTF8.GetBytes(XMLBillTran.ToString());
                byte[] win874BytesAipn = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(874), wind874Bytes);

                String md5win874 = bc.ComputeMD5Bytes(win874BytesAipn);
                byte[] md5aipnWin874Bytes = Encoding.GetEncoding(874).GetBytes(md5win874);
                byte[] win874BytesHeader = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(874), Encoding.UTF8.GetBytes(XMLHeader));

                byte[] md5aipnBytes = md5aipnWin874Bytes.ToArray();
                byte[] win874BytesFooter1 = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(874), Encoding.UTF8.GetBytes("<?EndNote HMAC=\""));
                byte[] win874BytesFooter2 = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(874), Encoding.UTF8.GetBytes("\" ?>"));
                byte[] win874BytesFooter = new byte[win874BytesFooter1.Length + md5aipnBytes.Length + win874BytesFooter2.Length];

                Buffer.BlockCopy(win874BytesFooter1, 0, win874BytesFooter, 0, win874BytesFooter1.Length);
                Buffer.BlockCopy(md5aipnBytes, 0, win874BytesFooter, win874BytesFooter1.Length, md5aipnBytes.Length);
                Buffer.BlockCopy(win874BytesFooter2, 0, win874BytesFooter, win874BytesFooter1.Length + md5aipnBytes.Length, win874BytesFooter2.Length);

                byte[] rv1 = new byte[win874BytesHeader.Length + win874BytesAipn.Length + win874BytesFooter.Length];
                Buffer.BlockCopy(win874BytesHeader, 0, rv1, 0, win874BytesHeader.Length);
                Buffer.BlockCopy(win874BytesAipn, 0, rv1, win874BytesHeader.Length, win874BytesAipn.Length);
                Buffer.BlockCopy(win874BytesFooter, 0, rv1, win874BytesHeader.Length + win874BytesAipn.Length, win874BytesFooter.Length);

                if (!Directory.Exists(pathFile))
                {
                    Directory.CreateDirectory(pathFile);
                }
                else
                {
                    Directory.Delete(pathFile, true);
                }
                Boolean chk = bc.ByteArrayToFile(pathFile + "\\" + fileNameBillTran, rv1);


                dtdrug = bc.bcDB.vsDB.selectSSOPPharT06(dtstart.Year.ToString() + "-" + dtstart.ToString("MM-dd"), dtend.Year.ToString() + "-" + dtend.ToString("MM-dd"));
                foreach (DataRow rowSSOP in dtssop.Rows)
                {
                    DataRow[] results = dtitem.Select("MNC_HN_NO = " + rowSSOP["MNC_HN_NO"].ToString() + " and mnc_doc_no = " + rowSSOP["mnc_doc_no"].ToString());
                    String svID = "";
                    foreach (DataRow drug in results)
                    {

                    }
                }


                Process.Start("explorer.exe", pathFile);
            }
            catch(Exception ex)
            {
                String err = "";
            }
            
        }

        private void BtnSSOPSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                DataTable dt = new DataTable();
                DateTime dtstart = new DateTime();
                DateTime dtend = new DateTime();
                dtstart = DateTime.Parse((int.Parse(cboSSOPYear.Text) - 543).ToString() + "-" + ((ComboBoxItem)cboSSOPMonth.SelectedItem).Value + "-" + "01");
                dtstart = dtstart.Year < 2000 ? dtstart.AddYears(543) : dtstart;

                dtend = DateTime.Parse((int.Parse(cboSSOPYear.Text) - 543).ToString() + "-" + ((ComboBoxItem)cboSSOPMonth.SelectedItem).Value + "-" + "01");
                dtend = dtend.Year < 2000 ? dtend.AddYears(543) : dtend;
                dtend = dtend.AddMonths(1);
                dtend = dtend.AddDays(-1);

                dt = bc.bcDB.vsDB.selectSSOPBillTran(dtstart.Year.ToString() + "-" + dtstart.ToString("MM-dd"), dtend.Year.ToString() + "-" + dtend.ToString("MM-dd"));
                grfSSOP.DataSource = dt;

            }
            catch (Exception ex)
            {

            }
        }

        private void GrfPaidTyp_AfterEdit(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            if (grfPaidTyp[e.Row, e.Col] == null) return;
            if (e.Col == colPaidTypSSOPclaimCat)
            {
                String paidid = "", ssopclaimcat = "";
                paidid = grfPaidTyp[e.Row, colPaidTypId].ToString();
                ssopclaimcat = grfPaidTyp[e.Row, colPaidTypSSOPclaimCat].ToString();
                if (!ssopclaimcat.Equals(""))
                {
                    if (bc.ssopClaimCat.ContainsValue(ssopclaimcat))
                    {
                        var myKey = bc.ssopClaimCat.FirstOrDefault(x => x.Value == ssopclaimcat).Key;
                        String re1 = bc.bcDB.finM02DB.updateSSOPClaimCatCode(paidid, myKey);
                    }
                }
            }
        }

        private void GrfClinic_AfterEdit(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            if (grfClinic[e.Row, e.Col] == null) return;
            grfClinic.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            if (e.Col == colClinicopbkkSSOP)
            {
                String depno = "", secno = "", divno = "", typpt = "", ssopcode = "";
                depno = grfClinic[e.Row, colClinicmddepno].ToString();
                secno = grfClinic[e.Row, colClinicsecno].ToString();
                divno = grfClinic[e.Row, colClinicdivno].ToString();
                typpt = grfClinic[e.Row, colClinictyppt].ToString();
                ssopcode = grfClinic[e.Row, e.Col].ToString();
                String re = bc.bcDB.pttM32DB.updateSSOPCode(depno, secno, divno, typpt, ssopcode);
            }
        }

        private void GrfOPBKKMainTmtCode_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfOPBKKMainTmtCode.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfOPBKKMainTmtCode[grfOPBKKMainTmtCode.Row, colGrfUcepDrugFlag] = "1";
        }
        private void TcMain_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(tcMain.SelectedTab== tabtcUcep)
            {
                showLbLoading();
                setGrfUcepDrug(grfUcepDrug);
                setGrfUcepCat();
                setGrfUcepHotCharge();
                setGrfUcepLAB();
                setGrfUcepXray();
                hideLbLoading();
            }
            else if (tcMain.SelectedTab == tabtcCIPN)
            {
                showLbLoading();
                setGrfUcepDrug(grfCipnDrug);
                setGrfCipnCVS();
                hideLbLoading();
            }
        }

        private void GrfOPBKKMainCHRGITEM_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void initCompomentTabUcep()
        {
            int gapLine = 25, gapX = 20, gapY = 20, xCol2 = 130, xCol1 = 80, xCol3 = 330, xCol4 = 640, xCol5 = 950;
            Size size = new Size();

            pnUcepMainTop = new Panel();
            pnUcepMainBotton = new Panel();
            pnDITdc01Top = new Panel();
            pnDITdc02Top = new Panel();
            pnDITsuppTop = new Panel();
            pnDITserviceTop = new Panel();
            pnDITdc01Botton = new Panel();
            pnDITdc02Botton = new Panel();
            pnDITsuppBotton = new Panel();
            pnDITserviceBotton = new Panel();

            pnUcepMainTop.Dock = DockStyle.Top;
            pnUcepMainBotton.Dock = DockStyle.Fill;

            pnDITdc01Top.Dock = DockStyle.Top;
            pnDITdc01Botton.Dock = DockStyle.Fill;
            //pnDITdc01Botton.BackColor = Color.Red;
            //pnDITdc01Top.BackColor = Color.Red;

            pnDITdc02Top.Dock = DockStyle.Top;
            pnDITdc02Botton.Dock = DockStyle.Fill;

            pnDITsuppTop.Dock = DockStyle.Top;
            pnDITsuppBotton.Dock = DockStyle.Fill;

            pnDITserviceTop.Dock = DockStyle.Top;
            pnDITserviceBotton.Dock = DockStyle.Fill;

            //pnUcepMainTop.BackColor = Color.Red;
            //pnUcepMainBotton.BackColor = Color.Blue;
            tabUcepMain.Controls.Add(pnUcepMainBotton);
            tabUcepMain.Controls.Add(pnUcepMainTop);

            tabDITdc01.Controls.Add(pnDITdc01Botton);
            tabDITdc01.Controls.Add(pnDITdc01Top);

            tabDITdc02.Controls.Add(pnDITdc02Botton);
            tabDITdc02.Controls.Add(pnDITdc02Top);

            tabDITSupp.Controls.Add(pnDITsuppBotton);
            tabDITSupp.Controls.Add(pnDITsuppTop);

            tabDITService.Controls.Add(pnDITserviceBotton);
            tabDITService.Controls.Add(pnDITserviceTop);

            btnUcepSelect = new C1Button();
            btnUcepTMTImport = new C1Button();
            btnUcepExcel = new C1Button();
            btnUcepDITdc01Read = new C1Button();
            btnUcepDITdc01Write = new C1Button();
            btnUcepDITdc02Write = new C1Button();

            lbtxtUcepDateStart = new Label();
            txtUcepDateStart = new C1DateEdit();
            lbtxtUcepDateEnd = new Label();
            txtUcepDateEnd = new C1DateEdit();
            lbtxtUcepPaidType = new Label();
            txtUcepPaidType = new C1TextBox();
            chkUcepSelectAll = new C1CheckBox();
            chkUcepAllVisit = new C1CheckBox();
            lbtxtDITdc01StartRow = new Label();
            txtDITdc01StartRow = new C1TextBox();
            lbtxtDITdc01CostNew = new Label();
            txtDITdc01CostNew = new C1TextBox();
            lbtxtDITdc01PriceNew = new Label();
            txtDITdc01PriceNew = new C1TextBox();
            txtDITdc01Filename = new C1TextBox();
            lbtxtDITdc01Top200 = new Label();
            txtDITdc01Top200 = new C1TextBox();
            lbtxtDITdc02StartRow = new Label();
            txtDITdc02StartRow = new C1TextBox();
            lbtxtDITdc02PriceNew = new Label();
            txtDITdc02PriceNew = new C1TextBox();
            txtDITdc02Filename = new C1TextBox();

            lbtxtDITsuppStartRow = new Label();
            txtDITsuppStartRow = new C1TextBox();
            lbtxtDITsuppCostNew = new Label();
            txtDITsuppCostNew = new C1TextBox();
            lbtxtDITsuppPriceNew = new Label();
            txtDITsuppPriceNew = new C1TextBox();
            txtDITsuppFilename = new C1TextBox();

            lbtxtDITserviceStartRow = new Label();
            txtDITserviceStartRow = new C1TextBox();
            lbtxtDITserviceCostNew = new Label();
            txtDITserviceCostNew = new C1TextBox();
            lbtxtDITservicePriceNew = new Label();
            txtDITservicePriceNew = new C1TextBox();
            txtDITserviceFilename = new C1TextBox();
            lbtxtUcepHn = new Label();
            txtUcepHn = new C1TextBox();

            bc.setControlLabel(ref lbtxtUcepDateStart, fEdit, "วันที่visit เริ่มต้น :", "lbtxtUcepDateStart", gapX, gapY);
            size = bc.MeasureString(lbtxtUcepDateStart);
            bc.setControlC1DateTimeEdit(ref txtUcepDateStart, "txtUcepDateStart", lbtxtUcepDateStart.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtUcepDateEnd, fEdit, "วันที่visit สิ้นสุด :", "lbtxtUcepDateEnd", txtUcepDateStart.Location.X + txtUcepDateStart.Width + 15, gapY);
            size = bc.MeasureString(lbtxtUcepDateEnd);
            bc.setControlC1DateTimeEdit(ref txtUcepDateEnd, "txtUcepDateEnd", lbtxtUcepDateEnd.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtUcepPaidType, fEdit, "สิทธิเข้า(ปชส) :", "lbtxtUcepPaidType", txtUcepDateEnd.Location.X+ txtUcepDateEnd.Width + 20, gapY);
            size = bc.MeasureString(lbtxtUcepPaidType);
            bc.setControlC1TextBox(ref txtUcepPaidType, fEdit, "txtUcepPaidType", 120, lbtxtUcepPaidType.Location.X + size.Width + 5, gapY);

            bc.setControlLabel(ref lbtxtUcepHn, fEdit, "HN :", "lbtxtUcepHn", txtUcepDateEnd.Location.X + txtUcepDateEnd.Width + 20, gapY+30);
            size = bc.MeasureString(lbtxtUcepHn);
            bc.setControlC1TextBox(ref txtUcepHn, fEdit, "txtUcepHn", 120, txtUcepPaidType.Location.X , lbtxtUcepHn.Location.Y);
            bc.setControlC1CheckBox(ref chkUcepAllVisit, fEdit, "ทุกใบยา", "chkUcepAllVisit", txtUcepPaidType.Location.X  + 120, lbtxtUcepHn.Location.Y);
            chkUcepAllVisit.Height = 30;

            //gapY += gapLine;chkUcepAllVisit
            bc.setControlC1Button(ref btnUcepSelect, fEdit, "ดึงข้อมูล", "btnUcepOk", txtUcepPaidType.Location.X+ txtUcepPaidType.Width+30, gapY);
            btnUcepSelect.Width = 70;
            bc.setControlC1CheckBox(ref chkUcepSelectAll, fEdit, "select All", "chkUcepSelectAll", btnUcepSelect.Location.X + btnUcepSelect.Width + 30, gapY);
            bc.setControlC1Button(ref btnUcepExcel, fEdit, "Export excel", "btnUcepExcel", chkUcepSelectAll.Location.X + chkUcepSelectAll.Width + 30, gapY);
            btnUcepExcel.Width = 110;
            bc.setControlC1Button(ref btnUcepTMTImport, fEdit, "Import TMT", "btnUcepTMTImport", btnUcepExcel.Location.X + btnUcepExcel.Width + 30, gapY);
            btnUcepTMTImport.Width = 100;

            bc.setControlC1Button(ref btnUcepDITdc01Open, fEdit, "Open Excel", "btnUcepDITdc01Open", gapX, gapY);
            btnUcepTMTImport.Width = 100;
            bc.setControlLabel(ref lbtxtDITdc01StartRow, fEdit, "row start :", "lbtxtDITdc01StartRow", btnUcepDITdc01Open.Location.X + btnUcepDITdc01Open.Width + 20, gapY);
            size = bc.MeasureString(lbtxtDITdc01StartRow);
            bc.setControlC1TextBox(ref txtDITdc01StartRow, fEdit, "txtDITdc01StartRow", 40, lbtxtDITdc01StartRow.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtDITdc01CostNew, fEdit, "col cost :", "lbtxtDITdc01CostNew", txtDITdc01StartRow.Location.X + txtDITdc01StartRow.Width + 20, gapY);
            size = bc.MeasureString(lbtxtDITdc01CostNew);
            bc.setControlC1TextBox(ref txtDITdc01CostNew, fEdit, "txtDITdc01CostNew", 40, lbtxtDITdc01CostNew.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtDITdc01PriceNew, fEdit, "col price :", "lbtxtDITdc01PriceNew", txtDITdc01CostNew.Location.X + txtDITdc01CostNew.Width + 20, gapY);
            size = bc.MeasureString(lbtxtDITdc01PriceNew);
            bc.setControlC1TextBox(ref txtDITdc01PriceNew, fEdit, "txtDITdc01PriceNew", 40, lbtxtDITdc01PriceNew.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtDITdc01Top200, fEdit, "TOP 200 :", "lbtxtDITdc01Top200", txtDITdc01PriceNew.Location.X + txtDITdc01PriceNew.Width + 20, gapY);
            size = bc.MeasureString(lbtxtDITdc01Top200);
            bc.setControlC1TextBox(ref txtDITdc01Top200, fEdit, "txtDITdc01Top200", 40, lbtxtDITdc01Top200.Location.X + size.Width + 5, gapY);
            bc.setControlC1Button(ref btnUcepDITdc01Read, fEdit, "Read Excel", "btnUcepDITdc01Read", txtDITdc01Top200.Location.X + txtDITdc01Top200.Width+20, gapY);
            btnUcepDITdc01Read.Width = 100;
            bc.setControlC1Button(ref btnUcepDITdc01Write, fEdit, "Write Excel", "btnUcepDITdc01Write", btnUcepDITdc01Read.Location.X + btnUcepDITdc01Read.Width + 20, gapY);
            btnUcepDITdc01Write.Width = 100;
            bc.setControlC1TextBox(ref txtDITdc01Filename, fEdit, "txtDITdc01Filename", 500, gapX, gapY+gapLine+10);

            bc.setControlC1Button(ref btnUcepDITdc02Open, fEdit, "Open Excel", "btnUcepDITdc02Open", gapX, gapY);
            btnUcepTMTImport.Width = 100;
            bc.setControlLabel(ref lbtxtDITdc02StartRow, fEdit, "row start :", "lbtxtDITdc02StartRow", btnUcepDITdc02Open.Location.X + btnUcepDITdc02Open.Width + 20, gapY);
            size = bc.MeasureString(lbtxtDITdc02StartRow);
            bc.setControlC1TextBox(ref txtDITdc02StartRow, fEdit, "txtDITdc02StartRow", 40, lbtxtDITdc02StartRow.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtDITdc02PriceNew, fEdit, "col price :", "lbtxtDITdc02PriceNew", txtDITdc02StartRow.Location.X + txtDITdc02StartRow.Width + 20, gapY);
            size = bc.MeasureString(lbtxtDITdc02PriceNew);
            bc.setControlC1TextBox(ref txtDITdc02PriceNew, fEdit, "txtDITdc02PriceNew", 40, lbtxtDITdc02PriceNew.Location.X + size.Width + 5, gapY);
            bc.setControlC1Button(ref btnUcepDITdc02Read, fEdit, "Read Excel", "btnUcepDITdc02Read", txtDITdc02PriceNew.Location.X + txtDITdc02PriceNew.Width + 20, gapY);
            btnUcepDITdc02Read.Width = 100;
            bc.setControlC1Button(ref btnUcepDITdc02Write, fEdit, "Write Excel", "btnUcepDITdc02Write", btnUcepDITdc02Read.Location.X + btnUcepDITdc02Read.Width + 20, gapY);
            btnUcepDITdc02Write.Width = 100;
            bc.setControlC1TextBox(ref txtDITdc02Filename, fEdit, "txtDITdc02Filename", 500, gapX, gapY + gapLine + 10);
            

            bc.setControlC1Button(ref btnUcepDITsuppOpen, fEdit, "Open Excel", "btnUcepDITsuppOpen", gapX, gapY);
            btnUcepTMTImport.Width = 100;
            bc.setControlLabel(ref lbtxtDITsuppStartRow, fEdit, "row start :", "lbtxtDITsuppStartRow", btnUcepDITsuppOpen.Location.X + btnUcepDITsuppOpen.Width + 20, gapY);
            size = bc.MeasureString(lbtxtDITsuppStartRow);
            bc.setControlC1TextBox(ref txtDITsuppStartRow, fEdit, "txtDITsuppStartRow", 40, lbtxtDITsuppStartRow.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtDITsuppCostNew, fEdit, "col cost :", "lbtxtDITsuppCostNew", txtDITsuppStartRow.Location.X + txtDITsuppStartRow.Width + 20, gapY);
            size = bc.MeasureString(lbtxtDITsuppCostNew);
            bc.setControlC1TextBox(ref txtDITsuppCostNew, fEdit, "txtDITsuppCostNew", 40, lbtxtDITsuppCostNew.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtDITsuppPriceNew, fEdit, "col price :", "lbtxtDITsuppPriceNew", txtDITsuppCostNew.Location.X + txtDITsuppCostNew.Width + 20, gapY);
            size = bc.MeasureString(lbtxtDITsuppPriceNew);
            bc.setControlC1TextBox(ref txtDITsuppPriceNew, fEdit, "txtDITsuppPriceNew", 40, lbtxtDITsuppPriceNew.Location.X + size.Width + 5, gapY);
            bc.setControlC1Button(ref btnUcepDITsuppRead, fEdit, "Read Excel", "btnUcepDITsuppRead", txtDITsuppPriceNew.Location.X + txtDITsuppPriceNew.Width + 20, gapY);
            btnUcepDITsuppRead.Width = 100;
            bc.setControlC1TextBox(ref txtDITsuppFilename, fEdit, "txtDITsuppFilename", 500, gapX, gapY + gapLine + 10);

            bc.setControlC1Button(ref btnUcepDITserviceOpen, fEdit, "Open Excel", "btnUcepDITdcserviceOpen", gapX, gapY);
            btnUcepTMTImport.Width = 100;
            bc.setControlLabel(ref lbtxtDITserviceStartRow, fEdit, "row start :", "lbtxtDITserviceStartRow", btnUcepDITserviceOpen.Location.X + btnUcepDITserviceOpen.Width + 20, gapY);
            size = bc.MeasureString(lbtxtDITserviceStartRow);
            bc.setControlC1TextBox(ref txtDITserviceStartRow, fEdit, "txtDITserviceStartRow", 40, lbtxtDITserviceStartRow.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtDITserviceCostNew, fEdit, "col cost :", "lbtxtDITserviceCostNew", txtDITserviceStartRow.Location.X + txtDITserviceStartRow.Width + 20, gapY);
            size = bc.MeasureString(lbtxtDITserviceCostNew);
            bc.setControlC1TextBox(ref txtDITserviceCostNew, fEdit, "txtDITserviceCostNew", 40, lbtxtDITserviceCostNew.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtDITservicePriceNew, fEdit, "col price :", "lbtxtDITservicePriceNew", txtDITserviceCostNew.Location.X + txtDITserviceCostNew.Width + 20, gapY);
            size = bc.MeasureString(lbtxtDITservicePriceNew);
            bc.setControlC1TextBox(ref txtDITservicePriceNew, fEdit, "txtDITservicePriceNew", 40, lbtxtDITservicePriceNew.Location.X + size.Width + 5, gapY);
            bc.setControlC1Button(ref btnUcepDITserviceRead, fEdit, "Read Excel", "btnUcepDITserviceRead", txtDITservicePriceNew.Location.X + txtDITservicePriceNew.Width + 20, gapY);
            btnUcepDITserviceRead.Width = 100;
            bc.setControlC1TextBox(ref txtDITserviceFilename, fEdit, "txtDITserviceFilename", 500, gapX, gapY + gapLine + 10);

            initGrfUcepSelect();
            initGrfDITdc01();
            initGrfDITdc02();
            initGrfDITsupp();
            initGrfDITservice();

            btnUcepSelect.Click += BtnUcepSelect_Click;
            btnUcepTMTImport.Click += BtnUcepTMTImport_Click;
            btnUcepExcel.Click += BtnUcepExcel_Click;
            chkUcepSelectAll.Click += ChkUcepSelectAll_Click;

            btnUcepDITdc01Open.Click += BtnUcepDITdc01Open_Click;
            btnUcepDITdc01Read.Click += BtnUcepDITdc01Read_Click;
            btnUcepDITdc01Write.Click += BtnUcepDITdc01Write_Click;
            btnUcepDITdc02Open.Click += BtnUcepDITdc02Open_Click;
            btnUcepDITdc02Read.Click += BtnUcepDITdc02Read_Click;
            btnUcepDITsuppOpen.Click += BtnUcepDITsuppOpen_Click;
            btnUcepDITsuppRead.Click += BtnUcepDITsuppRead_Click;
            btnUcepDITserviceOpen.Click += BtnUcepDITserviceOpen_Click;
            btnUcepDITserviceRead.Click += BtnUcepDITserviceRead_Click;
            btnUcepDITdc02Write.Click += BtnUcepDITdc02Write_Click;

            pnUcepMainTop.Controls.Add(lbtxtUcepPaidType);
            pnUcepMainTop.Controls.Add(txtUcepPaidType);
            pnUcepMainTop.Controls.Add(lbtxtUcepDateStart);
            pnUcepMainTop.Controls.Add(txtUcepDateStart);
            pnUcepMainTop.Controls.Add(lbtxtUcepDateEnd);
            pnUcepMainTop.Controls.Add(txtUcepDateEnd);
            pnUcepMainTop.Controls.Add(btnUcepSelect);
            pnUcepMainTop.Controls.Add(chkUcepSelectAll); //chkUcepAllVisit
            pnUcepMainTop.Controls.Add(chkUcepAllVisit);
            pnUcepMainTop.Controls.Add(btnUcepTMTImport);
            pnUcepMainTop.Controls.Add(btnUcepExcel);
            pnUcepMainTop.Controls.Add(lbtxtUcepHn);
            pnUcepMainTop.Controls.Add(txtUcepHn);

            pnDITdc01Top.Controls.Add(btnUcepDITdc01Open);
            pnDITdc01Top.Controls.Add(lbtxtDITdc01StartRow);
            pnDITdc01Top.Controls.Add(txtDITdc01StartRow);
            pnDITdc01Top.Controls.Add(lbtxtDITdc01CostNew);
            pnDITdc01Top.Controls.Add(txtDITdc01CostNew);
            pnDITdc01Top.Controls.Add(lbtxtDITdc01PriceNew);
            pnDITdc01Top.Controls.Add(txtDITdc01PriceNew);
            pnDITdc01Top.Controls.Add(txtDITdc01Filename);
            pnDITdc01Top.Controls.Add(lbtxtDITdc01Top200);
            pnDITdc01Top.Controls.Add(txtDITdc01Top200);
            pnDITdc01Top.Controls.Add(btnUcepDITdc01Read);
            pnDITdc01Top.Controls.Add(btnUcepDITdc01Write);


            pnDITdc02Top.Controls.Add(btnUcepDITdc02Open);
            pnDITdc02Top.Controls.Add(lbtxtDITdc02StartRow);
            pnDITdc02Top.Controls.Add(txtDITdc02StartRow);
            pnDITdc02Top.Controls.Add(lbtxtDITdc02PriceNew);
            pnDITdc02Top.Controls.Add(txtDITdc02PriceNew);
            pnDITdc02Top.Controls.Add(btnUcepDITdc02Read);
            pnDITdc02Top.Controls.Add(txtDITdc02Filename);
            pnDITdc02Top.Controls.Add(btnUcepDITdc02Write);

            pnDITsuppTop.Controls.Add(btnUcepDITsuppOpen);
            pnDITsuppTop.Controls.Add(lbtxtDITsuppStartRow);
            pnDITsuppTop.Controls.Add(txtDITsuppStartRow);
            pnDITsuppTop.Controls.Add(lbtxtDITsuppCostNew);
            pnDITsuppTop.Controls.Add(txtDITsuppCostNew);
            pnDITsuppTop.Controls.Add(lbtxtDITsuppPriceNew);
            pnDITsuppTop.Controls.Add(txtDITsuppPriceNew);
            pnDITsuppTop.Controls.Add(txtDITsuppFilename);
            pnDITsuppTop.Controls.Add(btnUcepDITsuppRead);

            pnDITserviceTop.Controls.Add(btnUcepDITserviceOpen);
            pnDITserviceTop.Controls.Add(lbtxtDITserviceStartRow);
            pnDITserviceTop.Controls.Add(txtDITserviceStartRow);
            pnDITserviceTop.Controls.Add(lbtxtDITserviceCostNew);
            pnDITserviceTop.Controls.Add(txtDITserviceCostNew);
            pnDITserviceTop.Controls.Add(lbtxtDITservicePriceNew);
            pnDITserviceTop.Controls.Add(txtDITservicePriceNew);
            pnDITserviceTop.Controls.Add(txtDITserviceFilename);
            pnDITserviceTop.Controls.Add(btnUcepDITserviceRead);

            theme1.SetTheme(pnUcepMainTop, bc.iniC.themeApp);
            //theme1.SetTheme(pnUcepMainBotton, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtUcepDateStart, bc.iniC.themeApp);
            theme1.SetTheme(txtUcepDateStart, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtUcepDateEnd, bc.iniC.themeApp);
            theme1.SetTheme(txtUcepDateEnd, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepSelect, bc.iniC.themeApp);
            theme1.SetTheme(chkUcepSelectAll, bc.iniC.themeApp);
            theme1.SetTheme(chkUcepAllVisit, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepTMTImport, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepExcel, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtUcepHn, bc.iniC.themeApp);
            theme1.SetTheme(txtUcepHn, bc.iniC.themeApp);

            theme1.SetTheme(lbtxtDITdc01StartRow, bc.iniC.themeApp);
            theme1.SetTheme(txtDITdc01StartRow, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtDITdc01CostNew, bc.iniC.themeApp);
            theme1.SetTheme(txtDITdc01CostNew, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtDITdc01PriceNew, bc.iniC.themeApp);
            theme1.SetTheme(txtDITdc01PriceNew, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtDITdc01Top200, bc.iniC.themeApp);
            theme1.SetTheme(txtDITdc01Top200, bc.iniC.themeApp);
            theme1.SetTheme(txtDITdc01Filename, bc.iniC.themeApp);

            theme1.SetTheme(lbtxtDITdc02StartRow, bc.iniC.themeApp);
            theme1.SetTheme(txtDITdc02StartRow, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtDITdc02PriceNew, bc.iniC.themeApp);
            theme1.SetTheme(txtDITdc02PriceNew, bc.iniC.themeApp);
            theme1.SetTheme(txtDITdc02Filename, bc.iniC.themeApp);

            theme1.SetTheme(lbtxtDITsuppStartRow, bc.iniC.themeApp);
            theme1.SetTheme(txtDITsuppStartRow, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtDITsuppCostNew, bc.iniC.themeApp);
            theme1.SetTheme(txtDITsuppCostNew, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtDITsuppPriceNew, bc.iniC.themeApp);
            theme1.SetTheme(txtDITsuppPriceNew, bc.iniC.themeApp);
            theme1.SetTheme(txtDITsuppFilename, bc.iniC.themeApp);

            theme1.SetTheme(lbtxtDITserviceStartRow, bc.iniC.themeApp);
            theme1.SetTheme(txtDITserviceStartRow, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtDITserviceCostNew, bc.iniC.themeApp);
            theme1.SetTheme(txtDITserviceCostNew, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtDITservicePriceNew, bc.iniC.themeApp);
            theme1.SetTheme(txtDITservicePriceNew, bc.iniC.themeApp);
            theme1.SetTheme(txtDITserviceFilename, bc.iniC.themeApp);

            theme1.SetTheme(btnUcepDITdc01Open, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepDITdc02Open, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepDITsuppOpen, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepDITserviceOpen, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepDITdc01Read, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepDITdc01Write, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepDITdc02Write, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepDITdc02Read, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepDITsuppRead, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepDITserviceRead, bc.iniC.themeApp);

            chkUcepAllVisit.Checked = true;
        }

        private void BtnUcepDITdc02Write_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1XLBook _c1xl = new C1XLBook();

            if (File.Exists(txtDITdc02Filename.Text))
            {
                showLbLoading();
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode openFileDialog1.FileName " + txtDITdc02Filename.Text);
                _c1xl.Load(txtDITdc02Filename.Text);
                var sheet = _c1xl.Sheets[0];
                int rowcnt = sheet.Rows.Count;
                int rowstart = 0, colcost = 0, colprice = 0, coltop200 = 0, row = 0, indexrow = 0;
                //String aaa = bc.convertExcelColName(txtDITdc01CostNew.Text);
                int.TryParse(txtDITserviceStartRow.Text, out rowstart);
                int.TryParse(bc.convertExcelColName(txtDITdc02PriceNew.Text), out colprice);
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode open Sheet success row count " + rowcnt);

                row = rowstart;
                for (int i = 1; i < rowcnt; i++)
                {
                    String drugname = "", tmtcode = "", vendor = "", price = "";
                    if (sheet[row, 0].Value == null) continue;

                    tmtcode = sheet[row, 0].Value != null ? sheet[row, 0].Value.ToString() : "";
                    price = bc.bcDB.pharM01DB.SelectPriceByTmtCode(tmtcode);
                    String[] cost = price.Split('#');
                    if (cost.Length > 1)
                    {
                        sheet[row, colcost].Value = cost[1];
                        sheet[row, colprice].Value = cost[0];
                    }
                    setLbLoading(i + "/" + rowcnt);
                    row++;
                    //String re = bc.bcDB.pharM01DB.UpdateTMTCode(drugcode, tmtcode);
                }
            }
            _c1xl.Save(txtDITdc02Filename.Text);
            _c1xl.Dispose();
            hideLbLoading();
            BtnUcepDITdc02Read_Click(null, null);
        }

        private void BtnUcepDITdc01Write_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1XLBook _c1xl = new C1XLBook();

            if (File.Exists(txtDITdc01Filename.Text))
            {
                showLbLoading();
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode openFileDialog1.FileName " + txtDITdc01Filename.Text);
                _c1xl.Load(txtDITdc01Filename.Text);
                var sheet = _c1xl.Sheets[0];
                int rowcnt = sheet.Rows.Count;
                int rowstart = 0, colcost = 0, colprice = 0, coltop200 = 0, row = 0, indexrow=0;
                //String aaa = bc.convertExcelColName(txtDITdc01CostNew.Text);
                int.TryParse(txtDITserviceStartRow.Text, out rowstart);
                int.TryParse(bc.convertExcelColName(txtDITdc01CostNew.Text), out colcost);
                int.TryParse(bc.convertExcelColName(txtDITdc01PriceNew.Text), out colprice);
                int.TryParse(bc.convertExcelColName(txtDITdc01Top200.Text), out coltop200);
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode open Sheet success row count " + rowcnt);
                

                row = rowstart;
                for (int i = 1; i < rowcnt; i++)
                {
                    String drugname = "", tmtcode = "", vendor = "", price = "";
                    if (sheet[row, 0].Value == null) continue;
                    
                    tmtcode = sheet[row, 0].Value != null ? sheet[row, 0].Value.ToString() : "";
                    price = bc.bcDB.pharM01DB.SelectPriceByTmtCode(tmtcode);
                    String[] cost = price.Split('#');
                    if (cost.Length > 1)
                    {
                        sheet[row, colcost].Value = cost[1];
                        sheet[row, colprice].Value = cost[0];
                    }
                    setLbLoading(i + "/" + rowcnt);
                    row++;
                    //String re = bc.bcDB.pharM01DB.UpdateTMTCode(drugcode, tmtcode);
                }
                setLbLoading("กำลังดึง TOP 200");
                DataTable dt200 = new DataTable();
                dt200 = bc.bcDB.pharT01DB.selectTop200();
                dt200.DefaultView.Sort = "qty desc";
                dt200 = dt200.DefaultView.ToTable();
                foreach (DataRow drow in dt200.Rows)
                {
                    indexrow++;
                    String tmtcode = "", tmtcodeexcel="";
                    tmtcode = drow["tmt_code"].ToString();
                    for (int i = rowstart; i < rowcnt; i++)
                    {
                        tmtcodeexcel = sheet[i, 0].Value != null ? sheet[i, 0].Value.ToString() : "";
                        if (tmtcodeexcel.Equals(tmtcode))
                        {
                            setLbLoading("TOP "+ indexrow + " / 200");
                            sheet[i, coltop200].Value = indexrow;
                            break;
                        }
                    }

                    //    if (sheet[row, 0].Value == null) continue;
                    //tmtcode = sheet[row, 0].Value != null ? sheet[row, 0].Value.ToString() : "";
                    //sheet[row, colcost].Value = cost[1];
                }
            }
            _c1xl.Save(txtDITdc01Filename.Text);
            _c1xl.Dispose();
            hideLbLoading();
            BtnUcepDITdc01Read_Click(null, null);
        }

        private void BtnUcepDITserviceRead_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1XLBook _c1xl = new C1XLBook();

            if (File.Exists(txtDITserviceFilename.Text))
            {
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode openFileDialog1.FileName " + txtDITdc01Filename.Text);
                _c1xl.Load(txtDITserviceFilename.Text);
                var sheet = _c1xl.Sheets[0];
                int rowcnt = sheet.Rows.Count;
                int rowstart = 0, colcost = 0, colprice = 0, coltop200 = 0, row = 0;
                //String aaa = bc.convertExcelColName(txtDITdc01CostNew.Text);
                int.TryParse(txtDITserviceStartRow.Text, out rowstart);
                int.TryParse(bc.convertExcelColName(txtDITserviceCostNew.Text), out colcost);
                int.TryParse(bc.convertExcelColName(txtDITservicePriceNew.Text), out colprice);
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode open Sheet success row count " + rowcnt);
                grfDITservice.Rows.Count = 1;
                grfDITservice.Rows.Count = rowcnt - rowstart + 1;
                grfDITservice.Cols.Count = 6;
                grfDITservice.Cols[1].Caption = "TMT Code";
                grfDITservice.Cols[2].Caption = "Name";
                grfDITservice.Cols[3].Caption = "Unit";
                grfDITservice.Cols[4].Caption = "Cost New";
                grfDITservice.Cols[5].Caption = "Price New";

                grfDITservice.Cols[1].Width = 80;
                grfDITservice.Cols[2].Width = 400;
                grfDITservice.Cols[3].Width = 300;
                grfDITservice.Cols[4].Width = 80;
                grfDITservice.Cols[5].Width = 80;

                row = rowstart;
                for (int i = 1; i < rowcnt; i++)
                {
                    String drugname = "", tmtcode = "", vendor = "", cost = "", price = "", top = "";
                    if (sheet[row, 0].Value == null) continue;
                    //if (sheet[i, 2].Value == null) continue;
                    drugname = sheet[row, 1].Value != null ? sheet[row, 1].Value.ToString() : "";
                    tmtcode = sheet[row, 0].Value != null ? sheet[row, 0].Value.ToString() : "";
                    vendor = sheet[row, 2].Value != null ? sheet[row, 2].Value.ToString() : "";
                    cost = sheet[row, colcost].Value != null ? sheet[row, colcost].Value.ToString() : "";
                    price = sheet[row, colprice].Value != null ? sheet[row, colprice].Value.ToString() : "";
                    grfDITservice[i, 0] = i;
                    grfDITservice[i, 1] = tmtcode;
                    grfDITservice[i, 2] = drugname;
                    grfDITservice[i, 3] = vendor;
                    grfDITservice[i, 4] = cost;
                    grfDITservice[i, 5] = price;
                    row++;
                    //String re = bc.bcDB.pharM01DB.UpdateTMTCode(drugcode, tmtcode);
                }
            }
            _c1xl.Dispose();
        }

        private void BtnUcepDITsuppRead_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1XLBook _c1xl = new C1XLBook();

            if (File.Exists(txtDITsuppFilename.Text))
            {
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode openFileDialog1.FileName " + txtDITsuppFilename.Text);
                _c1xl.Load(txtDITsuppFilename.Text);
                var sheet = _c1xl.Sheets[0];
                int rowcnt = sheet.Rows.Count;
                int rowstart = 0, colcost = 0, colprice = 0, coltop200 = 0, row = 0;
                //String aaa = bc.convertExcelColName(txtDITdc01CostNew.Text);
                int.TryParse(txtDITsuppStartRow.Text, out rowstart);
                int.TryParse(bc.convertExcelColName(txtDITsuppCostNew.Text), out colcost);
                int.TryParse(bc.convertExcelColName(txtDITsuppPriceNew.Text), out colprice);
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode open Sheet success row count " + rowcnt);
                grfDITsupp.Rows.Count = 1;
                grfDITsupp.Rows.Count = rowcnt - rowstart + 1;
                grfDITsupp.Cols.Count = 6;
                grfDITsupp.Cols[1].Caption = "TMT Code";
                grfDITsupp.Cols[2].Caption = "Name";
                grfDITsupp.Cols[3].Caption = "Unit";
                grfDITsupp.Cols[4].Caption = "Cost New";
                grfDITsupp.Cols[5].Caption = "Price New";
                
                grfDITsupp.Cols[1].Width = 80;
                grfDITsupp.Cols[2].Width = 400;
                grfDITsupp.Cols[3].Width = 300;
                grfDITsupp.Cols[4].Width = 80;
                grfDITsupp.Cols[5].Width = 80;
                
                row = rowstart;
                for (int i = 1; i < rowcnt; i++)
                {
                    String drugname = "", tmtcode = "", vendor = "", cost = "", price = "", top = "";
                    if (sheet[row, 0].Value == null) continue;
                    //if (sheet[i, 2].Value == null) continue;
                    drugname = sheet[row, 1].Value != null ? sheet[row, 1].Value.ToString() : "";
                    tmtcode = sheet[row, 0].Value != null ? sheet[row, 0].Value.ToString() : "";
                    vendor = sheet[row, 2].Value != null ? sheet[row, 2].Value.ToString() : "";
                    cost = sheet[row, colcost].Value != null ? sheet[row, colcost].Value.ToString() : "";
                    price = sheet[row, colprice].Value != null ? sheet[row, colprice].Value.ToString() : "";
                    grfDITsupp[i, 0] = i;
                    grfDITsupp[i, 1] = tmtcode;
                    grfDITsupp[i, 2] = drugname;
                    grfDITsupp[i, 3] = vendor;
                    grfDITsupp[i, 4] = cost;
                    grfDITsupp[i, 5] = price;
                    row++;
                    //String re = bc.bcDB.pharM01DB.UpdateTMTCode(drugcode, tmtcode);
                }
            }
            _c1xl.Dispose();
        }

        private void BtnUcepDITdc02Read_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1XLBook _c1xl = new C1XLBook();

            if (File.Exists(txtDITdc02Filename.Text))
            {
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode openFileDialog1.FileName " + txtDITdc02Filename.Text);
                _c1xl.Load(txtDITdc02Filename.Text);
                var sheet = _c1xl.Sheets[0];
                int rowcnt = sheet.Rows.Count;
                int rowstart = 0, colcost = 0, colprice = 0, coltop200 = 0, row = 0;
                //String aaa = bc.convertExcelColName(txtDITdc01CostNew.Text);
                int.TryParse(txtDITdc02StartRow.Text, out rowstart);
                int.TryParse(bc.convertExcelColName(txtDITdc02PriceNew.Text), out colprice);
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode open Sheet success row count " + rowcnt);
                grfDITdc02.Rows.Count = 1;
                grfDITdc02.Rows.Count = rowcnt - rowstart +1;
                grfDITdc02.Cols.Count = 7;
                grfDITdc02.Cols[1].Caption = "TMT Code";
                grfDITdc02.Cols[2].Caption = "Name";
                grfDITdc02.Cols[3].Caption = "Vendor";
                grfDITdc02.Cols[4].Caption = "Price New";
                grfDITdc02.Cols[1].Width = 80;
                grfDITdc02.Cols[2].Width = 400;
                grfDITdc02.Cols[3].Width = 300;
                grfDITdc02.Cols[4].Width = 80;
                
                row = rowstart;
                for (int i = 1; i < rowcnt; i++)
                {
                    String drugname = "", tmtcode = "", vendor = "", cost = "", price = "", top = "";
                    if (sheet[row, 0].Value == null) continue;
                    //if (sheet[i, 2].Value == null) continue;
                    drugname = sheet[row, 1].Value != null ? sheet[row, 1].Value.ToString() : "";
                    tmtcode = sheet[row, 0].Value != null ? sheet[row, 0].Value.ToString() : "";
                    vendor = sheet[row, 2].Value != null ? sheet[row, 2].Value.ToString() : "";
                    price = sheet[row, colprice].Value != null ? sheet[row, colprice].Value.ToString() : "";
                    grfDITdc02[i, 0] = i;
                    grfDITdc02[i, 1] = tmtcode;
                    grfDITdc02[i, 2] = drugname;
                    grfDITdc02[i, 3] = vendor;
                    grfDITdc02[i, 4] = price;
                    row++;
                    //String re = bc.bcDB.pharM01DB.UpdateTMTCode(drugcode, tmtcode);
                }
            }
            _c1xl.Dispose();
        }

        private void BtnUcepDITdc01Read_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            C1XLBook _c1xl = new C1XLBook();
            
            if (File.Exists(txtDITdc01Filename.Text))
            {
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode openFileDialog1.FileName " + txtDITdc01Filename.Text);
                _c1xl.Load(txtDITdc01Filename.Text);
                var sheet = _c1xl.Sheets[0];
                int rowcnt = sheet.Rows.Count;
                int rowstart = 0, colcost=0, colprice=0, coltop200=0, row=0;
                //String aaa = bc.convertExcelColName(txtDITdc01CostNew.Text);
                int.TryParse(txtDITdc01StartRow.Text, out rowstart);
                int.TryParse(bc.convertExcelColName(txtDITdc01CostNew.Text), out colcost);
                int.TryParse(bc.convertExcelColName(txtDITdc01PriceNew.Text), out colprice);
                int.TryParse(bc.convertExcelColName(txtDITdc01Top200.Text), out coltop200);
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode open Sheet success row count " + rowcnt);
                grfDITdc01.Rows.Count = 1;
                grfDITdc01.Rows.Count = rowcnt - rowstart + 1;
                grfDITdc01.Cols.Count = 9;
                grfDITdc01.Cols[1].Caption = "TMT Code";
                grfDITdc01.Cols[2].Caption = "Name";
                grfDITdc01.Cols[3].Caption = "Vendor";
                grfDITdc01.Cols[4].Caption = "Cost New";
                grfDITdc01.Cols[5].Caption = "Price New";
                grfDITdc01.Cols[6].Caption = "TOP 200";
                grfDITdc01.Cols[7].Caption = "hos code";
                grfDITdc01.Cols[8].Caption = "hos old code";
                grfDITdc01.Cols[0].Width = 70;
                grfDITdc01.Cols[1].Width = 80;
                grfDITdc01.Cols[2].Width = 400;
                grfDITdc01.Cols[3].Width = 300;
                grfDITdc01.Cols[4].Width = 80;
                grfDITdc01.Cols[5].Width = 80;
                grfDITdc01.Cols[6].Width = 80;
                grfDITdc01.Cols[7].Width = 80;
                grfDITdc01.Cols[8].Width = 80;
                grfDITdc01.AllowFiltering = true;
                row = rowstart;
                for (int i = 1; i < rowcnt; i++)
                {
                    String drugname = "", tmtcode = "", vendor="", cost="", price="", top="";
                    if (sheet[row, 0].Value == null) continue;
                    //if (sheet[i, 2].Value == null) continue;
                    drugname = sheet[row, 1].Value != null ? sheet[row, 1].Value.ToString() : "";
                    tmtcode = sheet[row, 0].Value != null ? sheet[row, 0].Value.ToString() : "";
                    vendor = sheet[row, 2].Value != null ? sheet[row, 2].Value.ToString() : "";
                    cost = sheet[row, colcost].Value != null ? sheet[row, colcost].Value.ToString() : "";
                    price = sheet[row, colprice].Value != null ? sheet[row, colprice].Value.ToString() : "";
                    top = sheet[row, coltop200].Value != null ? sheet[row, coltop200].Value.ToString() : "";
                    grfDITdc01[i, 0] = i;
                    grfDITdc01[i, 1] = tmtcode;
                    grfDITdc01[i, 2] = drugname;
                    grfDITdc01[i, 3] = vendor;
                    grfDITdc01[i, 4] = cost;
                    grfDITdc01[i, 5] = price;
                    grfDITdc01[i, 6] = top;
                    row++;
                    //String re = bc.bcDB.pharM01DB.UpdateTMTCode(drugcode, tmtcode);
                }
            }
            _c1xl.Dispose();
        }

        private void BtnUcepDITserviceOpen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd;
            ofd = new System.Windows.Forms.OpenFileDialog();
            //ofd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            ofd.Filter = "Excel (*.xls, *.xlsx)|*.xls;*.xlsx";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtDITserviceFilename.Value = ofd.FileName;
            }
        }

        private void BtnUcepDITsuppOpen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd;
            ofd = new System.Windows.Forms.OpenFileDialog();
            //ofd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            ofd.Filter = "Excel (*.xls, *.xlsx)|*.xls;*.xlsx";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtDITsuppFilename.Value = ofd.FileName;
            }
        }

        private void BtnUcepDITdc02Open_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd;
            ofd = new System.Windows.Forms.OpenFileDialog();
            //ofd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            ofd.Filter = "Excel (*.xls, *.xlsx)|*.xls;*.xlsx";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtDITdc02Filename.Value = ofd.FileName;
            }
        }

        private void BtnUcepDITdc01Open_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            OpenFileDialog ofd;
            ofd = new System.Windows.Forms.OpenFileDialog();
            //ofd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            ofd.Filter = "Excel (*.xls, *.xlsx)|*.xls;*.xlsx";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode openFileDialog1.FileName " + ofd.FileName);
                //_c1xl.Load(ofd.FileName);
                //var sheet = _c1xl.Sheets[0];
                //int row = sheet.Rows.Count;
                //new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode open Sheet success row count " + row);
                //for (int i = 0; i < row; i++)
                //{
                //    String drugcode = "", tmtcode = "";
                //    if (sheet[i, 0].Value == null) continue;
                //    if (sheet[i, 2].Value == null) continue;
                //    drugcode = sheet[i, 0].Value.ToString();
                //    tmtcode = sheet[i, 2].Value.ToString();
                //    String re = bc.bcDB.pharM01DB.UpdateTMTCode(drugcode, tmtcode);
                //}
                //grfDITdc01.DataSource = null;
                //grfDITdc01.LoadExcel(ofd.FileName);
                txtDITdc01Filename.Value = ofd.FileName;
            }
        }

        private void ChkUcepSelectAll_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Boolean checkAll = false;
            checkAll = chkUcepSelectAll.Checked ? true : false;
            foreach(Row row in grfUcepSelect.Rows)
            {
                if (row[colGrfUcepSelectHn]==null) continue;
                if (!row[colGrfUcepSelectHn].ToString().Equals("HN"))
                {
                    row[colGrfUcepSelectSelect] = checkAll;
                }
            }
        }
        private void BtnUcepExcel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            flagpaidtype = "";
            exportUcelExcel();
        }
        private void exportUcelImportTMTcode()
        {
            //ต้องการ update tmt code ทั้ง 2 field จากการ import excel
            C1XLBook _c1xl = new C1XLBook();

            OpenFileDialog openFileDialog1;
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                showLbLoading();
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode openFileDialog1.FileName "+ openFileDialog1.FileName);
                _c1xl.Load(openFileDialog1.FileName);
                var sheet = _c1xl.Sheets[0];
                int row = sheet.Rows.Count;
                new LogWriter("d", "FrmOPBKKClaim exportUcelImportTMTcode open Sheet success row count "+ row);
                for (int i = 0; i < row; i++)
                {
                    String drugcode = "", tmtcode = "";
                    if (sheet[i, 0].Value == null) continue;
                    if (sheet[i, 2].Value == null) continue;
                    drugcode = sheet[i, 0].Value.ToString();
                    tmtcode = sheet[i, 2].Value.ToString();
                    String re = bc.bcDB.pharM01DB.UpdateTMTCode(drugcode, tmtcode);
                    setLbLoading(i+" / rowcount "+ row);
                }
                hideLbLoading();
            }
        }
        private void BtnUcepTMTImport_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            exportUcelImportTMTcode();
        }

        private void BtnUcepSelect_Click(object sender, EventArgs e)
        {
            pageLoad = true;
            //throw new NotImplementedException();
            DataTable dtUcepPtt = new DataTable();

            String datestart = "", dateend = "", paidtype = "";
            DateTime dtstart = new DateTime();
            DateTime dtend = new DateTime();
            
            DateTime.TryParse(txtUcepDateStart.Value.ToString(), out dtstart);
            DateTime.TryParse(txtUcepDateEnd.Value.ToString(), out dtend);

            if(dtstart.Year < 2000)
            {
                datestart = (dtstart.Year+543) + dtstart.ToString("-MM-dd", new CultureInfo("en-US"));
                dateend = (dtend.Year+543) + dtend.ToString("-MM-dd", new CultureInfo("en-US"));
            }
            else
            {
                datestart = dtstart.Year + dtstart.ToString("-MM-dd", new CultureInfo("en-US"));
                dateend = dtend.Year + dtend.ToString("-MM-dd", new CultureInfo("en-US"));
            }
            
            new LogWriter("d", "FrmOPBKKClaim BtnUcepOk_Click datestart " + datestart + " datestart " + datestart);

            String[] paid = txtUcepPaidType.Text.Trim().Split(',');
            if (paid.Length > 0)
            {
                foreach (String txt in paid)
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
            Column colChk = grfUcepSelect.Cols[colGrfUcepSelectSelect];
            colChk.DataType = typeof(Boolean);
            grfUcepSelect.Cols.Count = 14;
            grfUcepSelect.Rows.Count = 1;
            grfUcepSelect.Cols[colGrfUcepSelectHn].Caption = "HN";
            grfUcepSelect.Cols[colGrfUcepSelectName].Caption = "Name";
            grfUcepSelect.Cols[colGrfUcepSelectVsDate].Caption = "vsdate";
            grfUcepSelect.Cols[colGrfUcepSelectPaidType].Caption = "สิทธิ";
            grfUcepSelect.Cols[colGrfUcepSelectSymptoms].Caption = "อาการเบื้องต้น";
            grfUcepSelect.Cols[colGrfUcepSelectHnYear].Caption = "hnyr";
            grfUcepSelect.Cols[colGrfUcepSelectPreno].Caption = "preno";
            grfUcepSelect.Cols[colGrfUcepSelectAnNo].Caption = "AN NO";
            grfUcepSelect.Cols[colGrfUcepSelectAnYear].Caption = "AN year";
            grfUcepSelect.Cols[colGrfUcepSelectAnDate].Caption = "AN date";
            grfUcepSelect.Cols[colGrfUcepSelectAnNoShow].Caption = "AN NO";
            grfUcepSelect.Cols[colGrfUcepSelectSelect].Width = 60;
            grfUcepSelect.Cols[colGrfUcepSelectHn].Width = 90;
            grfUcepSelect.Cols[colGrfUcepSelectName].Width = 300;
            grfUcepSelect.Cols[colGrfUcepSelectVsDate].Width = 100;
            grfUcepSelect.Cols[colGrfUcepSelectPaidType].Width = 100;
            grfUcepSelect.Cols[colGrfUcepSelectSymptoms].Width = 300;
            grfUcepSelect.Cols[colGrfUcepSelectHnYear].Width = 60;
            grfUcepSelect.Cols[colGrfUcepSelectPreno].Width = 60;
            grfUcepSelect.Cols[colGrfUcepSelectAnNo].Width = 60;
            grfUcepSelect.Cols[colGrfUcepSelectAnYear].Width = 60;
            grfUcepSelect.Cols[colGrfUcepSelectAnDate].Width = 100;
            grfUcepSelect.Cols[colGrfUcepSelectAnNoShow].Width = 100;
            showLbLoading();
            dtUcepPtt = bc.bcDB.vsDB.selectVisitByDatePaidType(txtUcepHn.Text.Trim(), paidtype, datestart, dateend);

            grfUcepSelect.Rows.Count = dtUcepPtt.Rows.Count + 1;
            int i = 1;
            foreach (DataRow drow in dtUcepPtt.Rows)
            {
                
                grfUcepSelect[i, colGrfUcepSelectHn] = drow["MNC_HN_NO"].ToString();
                grfUcepSelect[i, colGrfUcepSelectName] = drow["prefix"].ToString() + " " + drow["MNC_FNAME_T"].ToString() + " " + drow["MNC_LNAME_T"].ToString();
                grfUcepSelect[i, colGrfUcepSelectVsDate] = drow["mnc_date"].ToString();
                grfUcepSelect[i, colGrfUcepSelectPaidType] = drow["MNC_FN_TYP_DSC"].ToString();
                grfUcepSelect[i, colGrfUcepSelectSymptoms] = drow["MNC_SHIF_MEMO"].ToString();
                grfUcepSelect[i, colGrfUcepSelectHnYear] = drow["MNC_HN_YR"].ToString();
                grfUcepSelect[i, colGrfUcepSelectPreno] = drow["mnc_pre_no"].ToString();
                grfUcepSelect[i, colGrfUcepSelectStatusOPDIPD] = drow["MNC_PAT_FLAG"].ToString().Equals("O") ? "OPD" : "IPD";
                grfUcepSelect[i, colGrfUcepSelectAnNo] = drow["MNC_AN_NO"] != null ? drow["MNC_AN_NO"].ToString() : "";
                grfUcepSelect[i, colGrfUcepSelectAnYear] = drow["MNC_AN_YR"] != null ? drow["MNC_AN_YR"].ToString() : "";
                grfUcepSelect[i, colGrfUcepSelectAnDate] = drow["MNC_AD_DATE"] != null ? bc.datetoShow(drow["MNC_AD_DATE"].ToString()) : "";
                
                grfUcepSelect[i, colGrfUcepSelectSelect] = false;
                if (drow["MNC_PAT_FLAG"].ToString().Equals("I"))
                {
                    grfUcepSelect.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                    grfUcepSelect[i, colGrfUcepSelectAnNoShow] = drow["MNC_AN_NO"] != null ? drow["MNC_AN_NO"].ToString() + "/" + drow["MNC_AN_YR"].ToString() : "";
                }
                else
                {
                    grfUcepSelect[i, colGrfUcepSelectAnNoShow] = "";
                }
                grfUcepSelect[i, 0] = i;
                i++;

            }
            grfUcepSelect.Cols[colGrfUcepSelectAnNo].Visible = false;
            grfUcepSelect.Cols[colGrfUcepSelectAnYear].Visible = false;

            grfUcepSelect.Cols[colGrfUcepSelectHn].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUcepSelectName].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUcepSelectVsDate].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUcepSelectPaidType].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUcepSelectSymptoms].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUcepSelectHnYear].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUcepSelectPreno].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUcepSelectAnNoShow].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUcepSelectAnDate].AllowEditing = false;
            grfUcepSelect.AllowFiltering = true;
            grfUcepSelect.Cols[colGrfUcepSelectHn].DataType = dtUcepPtt.Columns["MNC_HN_NO"].DataType;
            grfUcepSelect.Cols[colGrfUcepSelectHn].Name = dtUcepPtt.Columns["MNC_HN_NO"].ColumnName;
            grfUcepSelect.Cols[colGrfUcepSelectName].DataType = dtUcepPtt.Columns["MNC_FNAME_T"].DataType;
            grfUcepSelect.Cols[colGrfUcepSelectName].Name = dtUcepPtt.Columns["MNC_FNAME_T"].ColumnName;

            hideLbLoading();
            pageLoad = false;
        }

        private void GrfClinic_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfClinic.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfClinic[e.Row, colClinicopbkkFlag] = "1";
        }

        private void GrfPaidTyp_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfPaidTyp.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfPaidTyp[e.Row, colPaidTypflag] = "1";
        }
        private void ContextMenu_grfUcepCat_save(object sender, System.EventArgs e)
        {
            try
            {
                foreach (Row row in grfUcepCat.Rows)
                {
                    if (row[colGrfUcepPaidFlag] == null) continue;
                    if (row[colGrfUcepPaidFlag].ToString().Equals("1"))
                    {
                        String id = "", tmtcode = "", divno = "", typpt = "", ucepcode = "";
                        id = row[colGrfUcepPaidId].ToString();
                        if (id.Length <= 0) continue;
                        //tmtcode = row[colGrfUcepPaidId].ToString();
                        ucepcode = row[colGrfUcepPaiducepcode] != null ? row[colGrfUcepPaiducepcode].ToString() : "";
                        String re = bc.bcDB.finM01DB.updateUcepcode(id, ucepcode);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ContextMenu_grfUcepHotcharge_save(object sender, System.EventArgs e)
        {
            try
            {
                foreach (Row row in grfUcepHotcharge.Rows)
                {
                    if (row[colGrfUcepHotChargeFlag] == null) continue;
                    if (row[colGrfUcepHotChargeFlag].ToString().Equals("1"))
                    {
                        if (row[colGrfUcepHotChargeId] == null) continue;
                        String id = "", tmtcode = "", ditcode = "", typpt = "", opbkkcode = "";
                        id = row[colGrfUcepHotChargeId] != null ? row[colGrfUcepHotChargeId].ToString():"";
                        if (id.Length <= 0) continue;
                        tmtcode = row[colGrfUcepHotChargeUcepCode] != null ? row[colGrfUcepHotChargeUcepCode].ToString() : "";
                        ditcode = row[colGrfUcepHotChargeDitCode] != null ? row[colGrfUcepHotChargeDitCode].ToString() : "";
                        String re = bc.bcDB.pttM30DB.updateOPBKKCode(id, tmtcode, ditcode);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ContextMenu_grfUcepXray_save(object sender, System.EventArgs e)
        {
            try
            {
                foreach (Row row in grfUcepXray.Rows)
                {
                    if (row[colGrfUcepXrayFlag] == null) continue;
                    if (row[colGrfUcepXrayFlag].ToString().Equals("1"))
                    {
                        String id = "", tmtcode = "", divno = "", typpt = "", opbkkcode = "";
                        id = row[colGrfUcepXrayId] != null ? row[colGrfUcepXrayId].ToString():"";
                        if (id.Length <= 0) continue;
                        tmtcode = row[colGrfUcepXrayUcepCode] != null ? row[colGrfUcepXrayUcepCode].ToString():"";
                        if (tmtcode.Length <= 0) continue;       //64-04-07
                        String re = bc.bcDB.xrayM01DB.updateOPBKKCode(id, tmtcode);
                    }
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfUcepXray_save " + ex.Message);
            }
        }
        private void ContextMenu_grfUcepLab_save(object sender, System.EventArgs e)
        {
            try
            {
                foreach (Row row in grfUcepLab.Rows)
                {
                    if (row[colGrfUcepLabFlag] == null) continue;
                    if (row[colGrfUcepLabFlag].ToString().Equals("1"))
                    {
                        String id = "", tmtcode = "", divno = "", typpt = "", opbkkcode = "";
                        id = row[colGrfUcepLabId] != null ?  row[colGrfUcepLabId].ToString() : "";
                        if (id.Length <= 0) continue;
                        tmtcode = row[colGrfUcepLabUcepCode] != null ? row[colGrfUcepLabUcepCode].ToString():"";
                        if (tmtcode.Length <= 0) continue;       //64-04-07
                        String re = bc.bcDB.labM01DB.updateOPBKKCode(id, tmtcode);
                    }
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfUcepLab_save " + ex.Message);
            }
        }
        private void ContextMenu_grfUcepDrug_save(object sender, System.EventArgs e)
        {
            try
            {
                foreach (Row row in grfUcepDrug.Rows)
                {
                    if (row[colGrfUcepDrugFlag] == null) continue;
                    if (row[colGrfUcepDrugFlag].ToString().Equals("1"))
                    {
                        String id = "", tmtcode = "", divno = "", typpt = "", opbkkcode = "";
                        id = row[colGrfUcepDrugDrugId] != null ? row[colGrfUcepDrugDrugId].ToString():"";
                        tmtcode = row[colGrfUcepDrugTmtCode] != null ? row[colGrfUcepDrugTmtCode].ToString():"";
                        if (id.Length <= 0) continue;       //64-04-07
                        if (tmtcode.Length <= 0) continue;       //64-04-07
                        String re = bc.bcDB.pharM01DB.UpdateTMTCode(id, tmtcode);
                    }
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfUcepDrug_save " + ex.Message);
            }
        }
        private void ContextMenu_grfINS_save(object sender, System.EventArgs e)
        {
            try
            {
                if (File.Exists(fileNameINS))
                {
                    File.Delete(fileNameINS);
                    genINS("txt");
                    genTextINS(pathfile);
                }
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfINS_save " + ex.Message);
            }
        }
        private void ContextMenu_grfPAT_save(object sender, System.EventArgs e)
        {
            try
            {
                if (File.Exists(fileNamePAT))
                {
                    File.Delete(fileNamePAT);
                    genPAT("txt");
                    genTextPAT(pathfile);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfPAT_save " + ex.Message);
            }
        }
        private void ContextMenu_grfOPD_save(object sender, System.EventArgs e)
        {
            try
            {
                if (File.Exists(fileNameOPD))
                {
                    File.Delete(fileNameOPD);
                    genOPD("txt");
                    genTextOPD(pathfile);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfOPD_save " + ex.Message);
            }
        }
        private void ContextMenu_grfODX_save(object sender, System.EventArgs e)
        {
            try
            {
                if (File.Exists(fileNameODX))
                {
                    File.Delete(fileNameODX);
                    genODX("txt");
                    genTextODX(pathfile);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfODX_save " + ex.Message);
            }
        }
        private void ContextMenu_grfOOP_save(object sender, System.EventArgs e)
        {
            try
            {
                if (File.Exists(fileNameOOP))
                {
                    File.Delete(fileNameOOP);
                    genOOP("txt");
                    genTextOOP(pathfile);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfOOP_save " + ex.Message);
            }
        }
        private void ContextMenu_grfORF_save(object sender, System.EventArgs e)
        {
            try
            {
                if (File.Exists(fileNameORF))
                {
                    File.Delete(fileNameORF);
                    genORF("txt");
                    genTextORF(pathfile);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfORF_save " + ex.Message);
            }
        }
        private void ContextMenu_grfCHT_save(object sender, System.EventArgs e)
        {
            try
            {
                if (File.Exists(fileNameCHT))
                {
                    File.Delete(fileNameCHT);
                    genCHT("txt");
                    genTextCHT(pathfile);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfCHT_save " + ex.Message);
            }
        }
        private void ContextMenu_grfCHA_save(object sender, System.EventArgs e)
        {
            try
            {
                if (File.Exists(fileNameCHA))
                {
                    File.Delete(fileNameCHA);
                    genCHA("txt");
                    genTextCHA(pathfile);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfCHA_save " + ex.Message);
            }
        }
        private void ContextMenu_grfAER_save(object sender, System.EventArgs e)
        {
            try
            {
                if (File.Exists(fileNameAER))
                {
                    File.Delete(fileNameAER);
                    genAER("txt");
                    genTextAER(pathfile);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfAER_save " + ex.Message);
            }
        }
        private void ContextMenu_grfLABFU_save(object sender, System.EventArgs e)
        {
            try
            {
                if (File.Exists(fileNameLABFU))
                {
                    File.Delete(fileNameLABFU);
                    genLABFU("txt");
                    genTextLABFU(pathfile);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfLABFU_save " + ex.Message);
            }
        }
        private void ContextMenu_grfDRU_save(object sender, System.EventArgs e)
        {
            try
            {
                if (File.Exists(fileNameDRU))
                {
                    File.Delete(fileNameDRU);
                    genDRU("txt");
                    genTextDRU(pathfile);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfDRU_save " + ex.Message);
            }
        }
        private void ContextMenu_grfCHAD_save(object sender, System.EventArgs e)
        {
            try
            {
                if (File.Exists(fileNameCHAD))
                {
                    File.Delete(fileNameCHAD);
                    genCHAD("txt");
                    genTextCHAD(pathfile);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfCHAD_save " + ex.Message);
            }
        }
        private void ContextMenu_grfOPBkkMain_save_tmtcode(object sender, System.EventArgs e)
        {
            //ต้องการ update รหัส เวชภัณฑ์
            foreach (Row row in grfOPBKKMainTmtCode.Rows)
            {
                if (row[colGrfUcepDrugFlag] == null) continue;
                if (row[colGrfUcepDrugFlag].ToString().Length <= 0) continue;
                if (row[colGrfUcepDrugFlag].ToString().Equals("0")) continue;

                if (row[colGrfUcepDrugFlag].ToString().Equals("1") && row[colGrfUcepDrugStatusDrug].ToString().Equals("O"))
                {
                    String drugid = "", opbkkcode = "";
                    drugid = row[colGrfUcepDrugDrugId] != null ? row[colGrfUcepDrugDrugId].ToString() : "";
                    opbkkcode = row[colGrfUcepDrugTmtCode] != null ? row[colGrfUcepDrugTmtCode].ToString() : "";
                    String re = bc.bcDB.pharM01DB.UpdateTMTCodeOPBKK(drugid, opbkkcode);
                }
            }
        }
        private void ContextMenu_grfOPBkkMain_save_OPBKKCode(object sender, System.EventArgs e)
        {
            foreach (Row row in grfOPBKKMainTmtCode.Rows)
            {
                if (row[colGrfUcepDrugFlag] == null) continue;
                if (row[colGrfUcepDrugFlag].ToString().Length<=0) continue;
                if (row[colGrfUcepDrugFlag].ToString().Equals("0")) continue;

                if (row[colGrfUcepDrugFlag].ToString().Equals("1"))
                {
                    String drugid = "", opbkkcode="";
                    drugid = row[colGrfUcepDrugDrugId] != null ? row[colGrfUcepDrugDrugId].ToString() : "";
                    opbkkcode = row[colGrfUcepDrugOpbkkCode] != null ? row[colGrfUcepDrugOpbkkCode].ToString() : "";
                    String re = bc.bcDB.pharM01DB.UpdateOPBKKCode(drugid, opbkkcode);
                //    secno = row[colClinicsecno].ToString();
                //    divno = row[colClinicdivno].ToString();
                //    typpt = row[colClinictyppt].ToString();
                //    opbkkcode = row[colCHRGITEMcode].ToString();
                //    if (bc.opBKKClinic.ContainsValue(opbkkcode))
                //    {
                //        var myKey = bc.opBKKClinic.FirstOrDefault(x => x.Value == opbkkcode).Key;
                //        String re = bc.bcDB.pttM32DB.updateOPBKKCode(depno, secno, divno, typpt, myKey);
                //    }
                }

            }
        }
        private void ContextMenu_grfOPBkkMainTmtCode_import(object sender, System.EventArgs e)
        {
            exportUcelImportTMTcode();
        }
        private void ContextMenu_grfCHRGITEM_save(object sender, System.EventArgs e)
        {
            foreach (Row row in grfOPBKKMainCHRGITEM.Rows)
            {
                if (row[colClinicCHRGITEMFlag] == null) continue;
                if (row[colClinicCHRGITEMFlag].ToString().Equals("1"))
                {
                    String depno = "", secno = "", divno = "", typpt = "", opbkkcode = "";
                    depno = row[colCHRGITEMFNcode].ToString();
                    secno = row[colClinicsecno].ToString();
                    divno = row[colClinicdivno].ToString();
                    typpt = row[colClinictyppt].ToString();
                    opbkkcode = row[colCHRGITEMcode].ToString();
                    if (bc.opBKKClinic.ContainsValue(opbkkcode))
                    {
                        var myKey = bc.opBKKClinic.FirstOrDefault(x => x.Value == opbkkcode).Key;
                        String re = bc.bcDB.pttM32DB.updateOPBKKCode(depno, secno, divno, typpt, myKey);
                    }

                }
            }
        }
        private void ContextMenu_grfClinic_save(object sender, System.EventArgs e)
        {
            foreach (Row row in grfClinic.Rows)
            {
                if (row[colClinicopbkkFlag] == null) continue;
                if (row[colClinicopbkkFlag].ToString().Equals("1"))
                {
                    String depno = "", secno="", divno="", typpt="", opbkkcode = "";
                    depno = row[colClinicmddepno].ToString();
                    secno = row[colClinicsecno].ToString();
                    divno = row[colClinicdivno].ToString();
                    typpt = row[colClinictyppt].ToString();
                    opbkkcode = row[colClinicopbkkcode].ToString();
                    if (bc.opBKKClinic.ContainsValue(opbkkcode))
                    {
                        var myKey = bc.opBKKClinic.FirstOrDefault(x => x.Value == opbkkcode).Key;
                        String re = bc.bcDB.pttM32DB.updateOPBKKCode(depno, secno, divno, typpt, myKey);
                    }

                }
            }
        }
        private void ContextMenu_grfPaidTyp_save(object sender, System.EventArgs e)
        {
            foreach(Row row in grfPaidTyp.Rows)
            {
                if (row[colPaidTypflag] == null) continue;
                if (row[colPaidTypflag].ToString().Equals("1"))
                {
                    String paidtypid = "", opbkkcode = "";
                    paidtypid = row[colPaidTypId].ToString();
                    opbkkcode = row[colPaidTypIdOpBkkCode].ToString();
                    if (bc.opBKKINSCL.ContainsValue(opbkkcode))
                    {
                        var myKey = bc.opBKKINSCL.FirstOrDefault(x => x.Value == opbkkcode).Key;
                        String re = bc.bcDB.finM02DB.updateOPBKKCode(paidtypid, myKey);
                    }
                }
            }
        }
        private void ContextMenu_grfDc01_getHoscode(object sender, System.EventArgs e)
        {
            showLbLoading();
            int i = 0;
            foreach(Row row in grfDITdc01.Rows)
            {
                String tmtcode = "", hoscode="";
                tmtcode = row[1] != null ? row[1].ToString() : "";
                if (tmtcode.Length > 0)
                {
                    hoscode = bc.bcDB.pharM01DB.SelectHosCodeByTmtCode(tmtcode);
                    String[] cost = hoscode.Split('#');
                    if (cost.Length > 1)
                    {
                        row[7] = cost[0];
                        row[8] = cost[1];
                    }
                }
                i++;
                setLbLoading(i+"/" +grfDITdc01.Rows.Count);
            }
            hideLbLoading();
        }
        private void ContextMenu_grfDc01_exportExcel(object sender, System.EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "xls";
            dlg.FileName = "*.xls";
            dlg.Filter = "Excel Files | *.xls";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            // save grid as sheet in the book
            FileFlags flags = FileFlags.IncludeFixedCells;
            grfDITdc01.SaveGrid(dlg.FileName, FileFormatEnum.Excel, flags);
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
            tabOPBKKOPD.Text = "OPD";
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
            tcOPBKK.Controls.Add(tabOPBKKDRU);

            tabOPBKKCHAD = new C1DockingTabPage();
            tabOPBKKCHAD.TabIndex = 0;
            tabOPBKKCHAD.Text = "CHAD";
            tabOPBKKCHAD.Name = "tabOPBKKCHAD";
            tcOPBKK.Controls.Add(tabOPBKKCHAD);

        }
        private void initGrfSSOP()
        {
            grfSSOP = new C1FlexGrid();
            grfSSOP.Font = fEdit;
            grfSSOP.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSSOP.Location = new System.Drawing.Point(0, 0);
            grfSSOP.Rows.Count = 1;
            grfSSOP.Name = "grfSSOP";
            //grfUcepCat.CellChanged += GrfUcepCat_CellChanged;

            ContextMenu menuGwPAT = new ContextMenu();
            menuGwPAT.MenuItems.Add("save Category 1-16", new EventHandler(ContextMenu_grfUcepCat_save));
            grfSSOP.ContextMenu = menuGwPAT;

            FlexGrid.FilterRow fr = new FlexGrid.FilterRow(grfSSOP);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnSSOPBotton.Controls.Add(grfSSOP);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfSSOP, "Office2010Red");
        }

        private void GrfSSOP_CellChanged(object sender, RowColEventArgs e)
        {
            throw new NotImplementedException();
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
            grfOrd.Name = "grfOrd";
            grfOrd.Rows.Count = 1;
            tabOrd.Controls.Add(grfOrd);
            theme1.SetTheme(grfOrd, "Office2010Red");
        }
        private void initGrfCipnDrug()
        {
            grfCipnDrug = new C1FlexGrid();
            grfCipnDrug.Font = fEdit;
            grfCipnDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            grfCipnDrug.Location = new System.Drawing.Point(0, 0);
            grfCipnDrug.Rows.Count = 1;
            grfCipnDrug.Name = "grfCipnDrug";
            grfCipnDrug.CellChanged += GrfCipnDrug_CellChanged;
            grfCipnDrug.AfterFilter += GrfCipnDrug_AfterFilter;

            ContextMenu menuGwPAT = new ContextMenu();
            menuGwPAT.MenuItems.Add("save TMT code ", new EventHandler(ContextMenu_grfCipnDrug_save));
            grfCipnDrug.ContextMenu = menuGwPAT;

            FlexGrid.FilterRow fr = new FlexGrid.FilterRow(grfUcepDrug);

            tabCIPNDrug.Controls.Add(grfCipnDrug);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfCipnDrug, "Office2010Red");
        }
        private void initGrfCipnCSV()
        {
            grfCipnCsv = new C1FlexGrid();
            grfCipnCsv.Font = fEdit;
            grfCipnCsv.Dock = System.Windows.Forms.DockStyle.Fill;
            grfCipnCsv.Location = new System.Drawing.Point(0, 0);
            grfCipnCsv.Rows.Count = 1;
            grfCipnCsv.Name = "grfCipnCsv";
            grfCipnCsv.CellChanged += GrfCipnCsv_CellChanged;
            grfCipnCsv.AfterFilter += GrfCipnCsv_AfterFilter;

            ContextMenu menuGwPAT = new ContextMenu();
            menuGwPAT.MenuItems.Add("save CVS code ", new EventHandler(ContextMenu_grfCipncsv_save));
            grfCipnDrug.ContextMenu = menuGwPAT;

            FlexGrid.FilterRow fr = new FlexGrid.FilterRow(grfCipnCsv);

            tabCIPNcsv.Controls.Add(grfCipnCsv);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfCipnCsv, "Office2010Red");
        }

        private void GrfCipnCsv_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
        }

        private void GrfCipnCsv_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;

        }
        private void ContextMenu_grfCipncsv_save(object sender, System.EventArgs e)
        {

        }
        private void ContextMenu_grfCipnDrug_save(object sender, System.EventArgs e)
        {
            try
            {
                foreach (Row row in grfUcepDrug.Rows)
                {
                    if (row[colGrfUcepDrugFlag] == null) continue;
                    if (row[colGrfUcepDrugFlag].ToString().Equals("1"))
                    {
                        String id = "", tmtcode = "", divno = "", typpt = "", opbkkcode = "";
                        id = row[colGrfUcepDrugDrugId]!= null ? row[colGrfUcepDrugDrugId].ToString():"";
                        tmtcode = row[colGrfUcepDrugTmtCode] != null ? row[colGrfUcepDrugTmtCode].ToString():"";
                        String re = bc.bcDB.pharM01DB.UpdateTMTCodeCIPN(id, tmtcode);       //64-04-07
                    }
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmOPBKKClaim ContextMenu_grfCipnDrug_save " + ex.Message);
            }
        }
        private void GrfCipnDrug_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void GrfCipnDrug_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void GrfUcepDrug_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfUcepDrug.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfUcepDrug[e.Row, colGrfUcepDrugFlag] = "1";
        }
        private void initGrfUcepDrug()
        {
            grfUcepDrug = new C1FlexGrid();
            grfUcepDrug.Font = fEdit;
            grfUcepDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            grfUcepDrug.Location = new System.Drawing.Point(0, 0);
            grfUcepDrug.Rows.Count = 1;
            grfUcepDrug.Name = "grfUcepDrug";
            grfUcepDrug.CellChanged += GrfUcepDrug_CellChanged;
            grfUcepDrug.AfterFilter += GrfUcepDrug_AfterFilter;

            ContextMenu menuGwPAT = new ContextMenu();
            menuGwPAT.MenuItems.Add("save TMT code ", new EventHandler(ContextMenu_grfUcepDrug_save));
            grfUcepDrug.ContextMenu = menuGwPAT;

            FlexGrid.FilterRow fr = new FlexGrid.FilterRow(grfUcepDrug);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            tabUcepDrug.Controls.Add(grfUcepDrug);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfUcepDrug, "Office2010Red");
        }

        private void GrfUcepDrug_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            for (int col = grfUcepDrug.Cols.Fixed; col < grfUcepDrug.Cols.Count; ++col)
            {
                var filter = grfUcepDrug.Cols[col].ActiveFilter;
            }
        }
        private void initGrfUcepLab()
        {
            grfUcepLab = new C1FlexGrid();
            grfUcepLab.Font = fEdit;
            grfUcepLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfUcepLab.Location = new System.Drawing.Point(0, 0);
            grfUcepLab.Rows.Count = 1;
            grfUcepLab.Name = "grfUcepLab";
            grfUcepLab.CellChanged += GrfUcepLab_CellChanged;
            grfUcepLab.AfterFilter += GrfUcepLab_AfterFilter;

            ContextMenu menuGwPAT = new ContextMenu();
            menuGwPAT.MenuItems.Add("save Lab Ucep code ", new EventHandler(ContextMenu_grfUcepLab_save));
            grfUcepLab.ContextMenu = menuGwPAT;

            FlexGrid.FilterRow fr = new FlexGrid.FilterRow(grfUcepLab);

            //grfUcepLab.AfterRowColChange += GrfUcepLab_AfterFilter;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            tabUcepLab.Controls.Add(grfUcepLab);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfUcepLab, "Office2010Red");
        }

        private void GrfUcepLab_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            for (int col = grfUcepLab.Cols.Fixed; col < grfUcepLab.Cols.Count; ++col)
            {
                var filter = grfUcepLab.Cols[col].ActiveFilter;
            }
        }

        private void GrfUcepLab_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfUcepLab.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfUcepLab[e.Row, colGrfUcepLabFlag] = "1";
        }
        private void initGrfUcepXray()
        {
            grfUcepXray = new C1FlexGrid();
            grfUcepXray.Font = fEdit;
            grfUcepXray.Dock = System.Windows.Forms.DockStyle.Fill;
            grfUcepXray.Location = new System.Drawing.Point(0, 0);
            grfUcepXray.Rows.Count = 1;
            grfUcepXray.Name = "grfUcepXray";
            grfUcepXray.CellChanged += GrfUcepXray_CellChanged;
            grfUcepXray.AfterFilter += GrfUcepXray_AfterFilter;

            ContextMenu menuGwPAT = new ContextMenu();
            menuGwPAT.MenuItems.Add("save Xray Ucep code ", new EventHandler(ContextMenu_grfUcepXray_save));
            grfUcepXray.ContextMenu = menuGwPAT;

            FlexGrid.FilterRow fr = new FlexGrid.FilterRow(grfUcepXray);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            tabUcepXray.Controls.Add(grfUcepXray);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfUcepXray, "Office2010Red");
        }

        private void GrfUcepXray_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void GrfUcepXray_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfUcepXray.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfUcepXray[e.Row, colGrfUcepXrayFlag] = "1";
        }
        private void initGrfDITdc01()
        {
            grfDITdc01 = new C1FlexGrid();
            grfDITdc01.Font = fEdit;
            grfDITdc01.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDITdc01.Location = new System.Drawing.Point(0, 0);
            grfDITdc01.Rows.Count = 1;
            grfDITdc01.Name = "grfDITdc01";
            ContextMenu menuGwPAT = new ContextMenu();
            menuGwPAT.MenuItems.Add("get Hos Code ", new EventHandler(ContextMenu_grfDc01_getHoscode));
            menuGwPAT.MenuItems.Add("Export to Excel  ", new EventHandler(ContextMenu_grfDc01_exportExcel));
            grfDITdc01.ContextMenu = menuGwPAT;

            pnDITdc01Botton.Controls.Add(grfDITdc01);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfDITdc01, "Office2010Red");
        }
        private void initGrfDITdc02()
        {
            grfDITdc02 = new C1FlexGrid();
            grfDITdc02.Font = fEdit;
            grfDITdc02.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDITdc02.Location = new System.Drawing.Point(0, 0);
            grfDITdc02.Rows.Count = 1;

            pnDITdc02Botton.Controls.Add(grfDITdc02);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfDITdc02, "Office2010Red");
        }
        private void initGrfDITsupp()
        {
            grfDITsupp = new C1FlexGrid();
            grfDITsupp.Font = fEdit;
            grfDITsupp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDITsupp.Location = new System.Drawing.Point(0, 0);
            grfDITsupp.Rows.Count = 1;
            grfDITsupp.Name = "grfDITsupp";

            pnDITsuppBotton.Controls.Add(grfDITsupp);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfDITsupp, "Office2010Red");
        }
        private void initGrfDITservice()
        {
            grfDITservice = new C1FlexGrid();
            grfDITservice.Font = fEdit;
            grfDITservice.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDITservice.Location = new System.Drawing.Point(0, 0);
            grfDITservice.Rows.Count = 1;
            grfDITservice.Name = "grfDITservice";
            pnDITserviceBotton.Controls.Add(grfDITservice);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfDITservice, "Office2010Red");
        }
        private void initGrfUcepHotcharge()
        {
            grfUcepHotcharge = new C1FlexGrid();
            grfUcepHotcharge.Font = fEdit;
            grfUcepHotcharge.Dock = System.Windows.Forms.DockStyle.Fill;
            grfUcepHotcharge.Location = new System.Drawing.Point(0, 0);
            grfUcepHotcharge.Rows.Count = 1;
            grfUcepHotcharge.Name = "grfUcepHotcharge";
            grfUcepHotcharge.CellChanged += GrfUcepHotcharge_CellChanged;
            grfUcepHotcharge.AfterFilter += GrfUcepHotcharge_AfterFilter;

            ContextMenu menuGwPAT = new ContextMenu();
            menuGwPAT.MenuItems.Add("save Hot Charge Ucep code ", new EventHandler(ContextMenu_grfUcepHotcharge_save));
            grfUcepHotcharge.ContextMenu = menuGwPAT;

            FlexGrid.FilterRow fr = new FlexGrid.FilterRow(grfUcepHotcharge);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            tabUcepHotCharge.Controls.Add(grfUcepHotcharge);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfUcepHotcharge, "Office2010Red");
        }

        private void GrfUcepHotcharge_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            for (int col = grfUcepHotcharge.Cols.Fixed; col < grfUcepHotcharge.Cols.Count; ++col)
            {
                var filter = grfUcepHotcharge.Cols[col].ActiveFilter;
            }
        }

        private void GrfUcepHotcharge_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfUcepHotcharge.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfUcepHotcharge[e.Row, colGrfUcepHotChargeFlag] = "1";
        }

        private void initGrfUcepCat()
        {
            grfUcepCat = new C1FlexGrid();
            grfUcepCat.Font = fEdit;
            grfUcepCat.Dock = System.Windows.Forms.DockStyle.Fill;
            grfUcepCat.Location = new System.Drawing.Point(0, 0);
            grfUcepCat.Rows.Count = 1;
            grfUcepCat.Name = "grfUcepCat";
            //grfUcepCat.CellChanged += GrfUcepCat_CellChanged;

            ContextMenu menuGwPAT = new ContextMenu();
            menuGwPAT.MenuItems.Add("save Category 1-16", new EventHandler(ContextMenu_grfUcepCat_save));
            grfUcepCat.ContextMenu = menuGwPAT;
            grfUcepCat.CellChanged += GrfUcepCat_CellChanged;
            grfUcepCat.AfterFilter += GrfUcepCat_AfterFilter;
            grfUcepCat.AfterEdit += GrfUcepCat_AfterEdit;

            FlexGrid.FilterRow fr = new FlexGrid.FilterRow(grfUcepCat);
            
            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            tabUcepCat.Controls.Add(grfUcepCat);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfUcepCat, "Office2010Red");
        }

        private void GrfUcepCat_AfterEdit(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            if (grfUcepCat[e.Row, e.Col] == null) return;
            grfUcepCat.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            if(e.Col== colGrfUcepPaidSSOP)
            {
                String id = "", tmtcode = "", divno = "", typpt = "", ssopid = "";
                id = grfUcepCat[e.Row, colGrfUcepPaidId].ToString();
                if (id.Length <= 0) return;
                ssopid = grfUcepCat[e.Row, e.Col].ToString();
                String re = bc.bcDB.finM01DB.updateSSOPcode(id, ssopid);
            }
        }

        private void GrfUcepCat_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            for (int col = grfUcepCat.Cols.Fixed; col < grfUcepCat.Cols.Count; ++col)
            {
                var filter = grfUcepCat.Cols[col].ActiveFilter;
            }
        }

        private void GrfUcepCat_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfUcepCat.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfUcepCat[e.Row, colGrfUcepPaidFlag] = "1";
        }

        private void BtnOPBKKSelect_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            dtPtt = new DataTable();
            String datestart = "", dateend = "", paidtype="";
            DateTime dtstart = new DateTime();
            DateTime dtend = new DateTime();
            DateTime.TryParse(txtDateStart.Value.ToString(), out dtstart);
            DateTime.TryParse(txtDateEnd.Value.ToString(), out dtend);

            datestart = dtstart.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            dateend = dtend.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            new LogWriter("d", "FrmOPBKKClaim BtnOPBKKSelect_Click datestart "+ datestart + " datestart "+ datestart);
            //datestart = bc.datetoDB(txtDateStart.Text);
            //dateend = bc.datetoDB(txtDateEnd.Text);
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
            dtPtt = bc.bcDB.vsDB.selectVisitByDatePaidType("",paidtype, datestart, dateend);
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
            txtUcepDateStart.Value = DateTime.Now;
            txtUcepDateEnd.Value = DateTime.Now;
            setGrfPaidType();
            setGrfClinic();
            setGrfUcepDrug(grfOPBKKMainTmtCode);
            setGrfOPBKKMainCHRGITEM();

            txtDITdc01StartRow.Value = "19";
            txtDITdc01CostNew.Value = "E";
            txtDITdc01PriceNew.Value = "G";
            txtDITdc01Top200.Value = "H";

            txtDITdc02StartRow.Value = "19";
            txtDITdc02PriceNew.Value = "D";

            txtDITsuppStartRow.Value = "18";
            txtDITsuppCostNew.Value = "E";
            txtDITsuppPriceNew.Value = "G";

            txtDITserviceStartRow.Value = "18";
            txtDITserviceCostNew.Value = "D";
            txtDITservicePriceNew.Value = "E";

            txtHCode.Value = bc.iniC.opbkkhcode;

        }
        private void initGrfUcepSelect()
        {
            grfUcepSelect = new C1FlexGrid();
            grfUcepSelect.Font = fEdit;
            grfUcepSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            grfUcepSelect.Location = new System.Drawing.Point(0, 0);
            grfUcepSelect.Rows.Count = 2;
            //grfSelect.DoubleClick += GrfSelect_DoubleClick;
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ดูข้อมูล รับชำระ", new EventHandler(ContextMenu_grfUcepSelect_viewPaidType));
            menuGw.MenuItems.Add("Export Excel", new EventHandler(ContextMenu_grfUcepSelect_ExportExcel));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            grfUcepSelect.ContextMenu = menuGw;
            //grfUcepSelect.AfterFilter += GrfUcepSelect_AfterFilter;
            //FlexGrid.FilterRow fr = new FlexGrid.FilterRow(grfUcepSelect);

            pnUcepMainBotton.Controls.Add(grfUcepSelect);

            //theme1.SetTheme(grfSelect, bc.theme);
            theme1.SetTheme(grfUcepSelect, "Office2010Red");
        }

        //private void GrfUcepSelect_AfterFilter(object sender, EventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    for (int col = grfUcepSelect.Cols.Fixed; col < grfUcepSelect.Cols.Count; ++col)
        //    {
        //        var filter = grfUcepSelect.Cols[col].ActiveFilter;
        //    }
        //}
        private void ContextMenu_grfUcepSelect_viewPaidType(object sender, System.EventArgs e)
        {
            Form frm = new Form();
            frm.Size = new Size(900, 800);
            frm.StartPosition = FormStartPosition.CenterScreen;
            Panel pn = new Panel();
            pn.Dock = DockStyle.Fill;
            frm.Controls.Add(pn);
            string currentDirectory = Directory.GetCurrentDirectory();      //grfUcepHnPaidType
            grfUcepHnPaidType = new C1FlexGrid();
            grfUcepHnPaidType.Font = fEdit;
            grfUcepHnPaidType.Dock = System.Windows.Forms.DockStyle.Top;
            grfUcepHnPaidType.Location = new System.Drawing.Point(0, 0);
            grfUcepHnPaidType.Rows.Count = 1;
            grfUcepHnPaidType.Height = 120;
            grfUcepHnPaidType.Rows.Count = 6;
            grfUcepHnPaidType.Cols.Count = 15;
            grfUcepHnPaidType.Cols[1].Caption = "วันที่รับชำระ";
            grfUcepHnPaidType.Cols[2].Caption = "เลขที่เอกสาร";
            //grfDITdc02.Cols[3].Caption = "Vendor";
            //grfDITdc02.Cols[4].Caption = "Price New";
            grfUcepHnPaidType.Cols[1].Width = 120;
            grfUcepHnPaidType.Cols[2].Width = 150;
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("Export Excel", new EventHandler(ContextMenu_grfUcepSelect_PaidType_ExportExcel));
            grfUcepHnPaidType.ContextMenu = menuGw;
            //grfDITdc02.Cols[3].Width = 300;
            //grfDITdc02.Cols[4].Width = 80;

            grfUcepHnPaidTypeDetail = new C1FlexGrid();
            grfUcepHnPaidTypeDetail.Font = fEdit;
            grfUcepHnPaidTypeDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            grfUcepHnPaidTypeDetail.Location = new System.Drawing.Point(0, 0);
            grfUcepHnPaidTypeDetail.Rows.Count = 3;
            //grfUcepHnPaidTypeDetail.Height = 200;
            //grfDITdc02.Rows.Count = rowcnt - rowstart + 1;
            grfUcepHnPaidTypeDetail.Cols.Count = 7;
            grfUcepHnPaidTypeDetail.Cols[1].Caption = "รหัส";
            grfUcepHnPaidTypeDetail.Cols[2].Caption = "รหัสย่อย";
            grfUcepHnPaidTypeDetail.Cols[3].Caption = "รายการ";
            grfUcepHnPaidTypeDetail.Cols[4].Caption = "จำนวนเงิน";
            grfUcepHnPaidTypeDetail.Cols[4].Caption = "จำนวนเงิน";
            //grfDITdc02.Cols[3].Caption = "Vendor";
            //grfDITdc02.Cols[4].Caption = "Price New";
            grfUcepHnPaidTypeDetail.Cols[1].Width = 150;
            grfUcepHnPaidTypeDetail.Cols[2].Width = 150;
            grfUcepHnPaidTypeDetail.Cols[3].Width = 100;

            grfUcepHnPaidType.DoubleClick += GrfUcepHnPaidType_DoubleClick;

            pn.Controls.Add(grfUcepHnPaidTypeDetail);
            pn.Controls.Add(grfUcepHnPaidType);
            String preno = "", vsdate = "", hnyear="", hn="";
            hnyear = grfUcepSelect[grfUcepSelect.Row, colGrfUcepSelectHnYear] != null ? grfUcepSelect[grfUcepSelect.Row, colGrfUcepSelectHnYear].ToString():"";
            preno = grfUcepSelect[grfUcepSelect.Row, colGrfUcepSelectPreno] != null ? grfUcepSelect[grfUcepSelect.Row, colGrfUcepSelectPreno].ToString():"";
            vsdate = grfUcepSelect[grfUcepSelect.Row, colGrfUcepSelectVsDate] != null ? grfUcepSelect[grfUcepSelect.Row, colGrfUcepSelectVsDate].ToString():"";
            hn = grfUcepSelect[grfUcepSelect.Row, colGrfUcepSelectHn] != null ? grfUcepSelect[grfUcepSelect.Row, colGrfUcepSelectHn].ToString() : "";

            DateTime vsdate1 = new DateTime();
            DateTime.TryParse(vsdate, out vsdate1);
            if (vsdate1.Year > 2500)
            {
                vsdate1 = vsdate1.AddYears(-543);
            }
            else if (vsdate1.Year < 2000)
            {
                vsdate1 = vsdate1.AddYears(543);
            }
            vsdate = vsdate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            DataTable dtUcepPtt = new DataTable();
            if(grfUcepSelect[grfUcepSelect.Row, colGrfUcepSelectStatusOPDIPD].ToString().Equals("OPD"))
            {
                dtUcepPtt = bc.bcDB.vsDB.selectPaidByHnGrpPaidTypeOPD(hn, hnyear, vsdate, preno);
            }
            else
            {
                dtUcepPtt = bc.bcDB.vsDB.selectPaidByHnGrpPaidTypeIPD(hn, hnyear, vsdate, preno);
            }
            grfUcepHnPaidType.Rows.Count = dtUcepPtt.Rows.Count +1;
            int i = 1;
            foreach (DataRow drow in dtUcepPtt.Rows)
            {//finance_t01  mnc_doc_cd                  IN1 = BI; IN2 = PI; ON1 = BO; ON2 = PO; ON3 = PO;IN3 = BI
                String doc = "", docno="";
                decimal total = 0;
                docno = "000000"+drow["MNC_doc_no"].ToString();
                docno = docno.Substring(docno.Length - 6);
                doc = drow["mnc_doc_cd"].ToString().Equals("IN1") ? "BI" : drow["mnc_doc_cd"].ToString().Equals("IN2") ? "PI" : drow["mnc_doc_cd"].ToString().Equals("ON1") ? "BO" : drow["mnc_doc_cd"].ToString().Equals("ON2") ? "PO" : drow["mnc_doc_cd"].ToString().Equals("ON3") ? "PO" : "";
                decimal.TryParse(drow["mnc_sum_pri"].ToString(), out total);
                grfUcepHnPaidType[i, 2] = doc + drow["MNC_doc_yr"].ToString()+""+ docno;
                grfUcepHnPaidType[i, 1] = bc.datetoShow(drow["mnc_doc_dat"].ToString());
                grfUcepHnPaidType[i, 3] = total.ToString("#,###.00");
                grfUcepHnPaidType[i, 4] = drow["MNC_DOC_CD"].ToString();
                grfUcepHnPaidType[i, 5] = drow["MNC_DOC_YR"].ToString();
                grfUcepHnPaidType[i, 6] = drow["MNC_DOC_NO"].ToString();
                grfUcepHnPaidType[i, 7] = drow["MNC_DOC_DAT"].ToString();
                grfUcepHnPaidType[i, 8] = hn;
                grfUcepHnPaidType[i, 9] = hnyear;
                grfUcepHnPaidType[i, 10] = vsdate;
                grfUcepHnPaidType[i, 11] = preno;
                grfUcepHnPaidType[i, 12] = drow["MNC_an_no"] != null ? drow["MNC_an_no"].ToString() : "";
                grfUcepHnPaidType[i, 13] = drow["MNC_an_yr"] != null ? drow["MNC_an_yr"].ToString() : "";
                grfUcepHnPaidType[i, 14] = drow["MNC_ad_dat"] != null ? drow["MNC_ad_dat"].ToString() : "";
                i++;
            }

            grfUcepHnPaidType.Cols[1].AllowEditing = false;
            grfUcepHnPaidType.Cols[2].AllowEditing = false;
            grfUcepHnPaidType.Cols[3].AllowEditing = false;
            grfUcepHnPaidType.Cols[4].AllowEditing = false;
            grfUcepHnPaidType.Cols[5].AllowEditing = false;
            grfUcepHnPaidType.Cols[6].AllowEditing = false;
            grfUcepHnPaidType.Cols[7].AllowEditing = false;
            grfUcepHnPaidType.Cols[8].AllowEditing = false;
            grfUcepHnPaidType.Cols[9].AllowEditing = false;
            grfUcepHnPaidType.Cols[10].AllowEditing = false;
            grfUcepHnPaidType.Cols[11].AllowEditing = false;
            grfUcepHnPaidType.Cols[12].AllowEditing = false;
            grfUcepHnPaidType.Cols[13].AllowEditing = false;
            grfUcepHnPaidType.Cols[14].AllowEditing = false;
            theme1.SetTheme(grfDITdc02, "Office2010Red");
            theme1.SetTheme(grfUcepHnPaidTypeDetail, bc.iniC.themeApp);
            frm.ShowDialog(this);
        }
        private void ContextMenu_grfUcepSelect_PaidType_ExportExcel(object sender, System.EventArgs e)
        {
            flagpaidtype = grfUcepHnPaidType[grfUcepHnPaidType.Row, 4].ToString() 
                + "#" + grfUcepHnPaidType[grfUcepHnPaidType.Row, 5].ToString() 
                + "#" + grfUcepHnPaidType[grfUcepHnPaidType.Row, 6].ToString()
                + "#" + grfUcepHnPaidType[grfUcepHnPaidType.Row, 7].ToString() 
                + "#" + grfUcepHnPaidType[grfUcepHnPaidType.Row, 8].ToString() 
                + "#" + grfUcepHnPaidType[grfUcepHnPaidType.Row, 9].ToString() 
                + "#" + grfUcepHnPaidType[grfUcepHnPaidType.Row, 10].ToString() 
                + "#" + grfUcepHnPaidType[grfUcepHnPaidType.Row, 11].ToString()
                + "#" + grfUcepHnPaidType[grfUcepHnPaidType.Row, 12].ToString()
                + "#" + grfUcepHnPaidType[grfUcepHnPaidType.Row, 13].ToString()
                + "#" + grfUcepHnPaidType[grfUcepHnPaidType.Row, 14].ToString();
            exportUcelExcel();
        }
        private void GrfUcepHnPaidType_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfUcepHnPaidType.Row < 0) return;
            if (grfUcepHnPaidType.Col < 0) return;
            pageLoad = true;
            String preno = "", vsdate = "", hnyear = "", hn = "", doccd="", doc_year="", docno="", docdate="";
            hnyear = grfUcepHnPaidType[grfUcepHnPaidType.Row, 9] != null ? grfUcepHnPaidType[grfUcepHnPaidType.Row, 9].ToString() : "";
            preno = grfUcepHnPaidType[grfUcepHnPaidType.Row, 11] != null ? grfUcepHnPaidType[grfUcepHnPaidType.Row, 11].ToString() : "";
            vsdate = grfUcepHnPaidType[grfUcepHnPaidType.Row, 10] != null ? grfUcepHnPaidType[grfUcepHnPaidType.Row, 10].ToString() : "";
            hn = grfUcepHnPaidType[grfUcepHnPaidType.Row, 8] != null ? grfUcepHnPaidType[grfUcepHnPaidType.Row, 8].ToString() : "";
            doccd = grfUcepHnPaidType[grfUcepHnPaidType.Row, 4] != null ? grfUcepHnPaidType[grfUcepHnPaidType.Row, 4].ToString() : "";
            doc_year = grfUcepHnPaidType[grfUcepHnPaidType.Row, 5] != null ? grfUcepHnPaidType[grfUcepHnPaidType.Row, 5].ToString() : "";
            docno = grfUcepHnPaidType[grfUcepHnPaidType.Row, 5] != null ? grfUcepHnPaidType[grfUcepHnPaidType.Row, 6].ToString() : "";
            docdate = grfUcepHnPaidType[grfUcepHnPaidType.Row, 7] != null ? grfUcepHnPaidType[grfUcepHnPaidType.Row, 7].ToString() : "";

            DataTable dtUcepPtt = new DataTable();
            int i = 1;
            grfUcepHnPaidTypeDetail.Cols.Count = 7;
            dtUcepPtt = bc.bcDB.vsDB.selectPaidByHnGrpPaidTypeDetail(hn, hnyear, vsdate, preno, doccd, doc_year, docno, docdate);
            grfUcepHnPaidTypeDetail.Rows.Count = 1;
            grfUcepHnPaidTypeDetail.Rows.Count = dtUcepPtt.Rows.Count+1;
            grfUcepHnPaidTypeDetail.Cols[1].Width = 50;
            grfUcepHnPaidTypeDetail.Cols[2].Width = 50;
            grfUcepHnPaidTypeDetail.Cols[3].Width = 300;
            grfUcepHnPaidTypeDetail.Cols[4].Width = 80;
            grfUcepHnPaidTypeDetail.Cols[5].Width = 80;
            grfUcepHnPaidTypeDetail.Cols[6].Width = 100;
            foreach (DataRow drow in dtUcepPtt.Rows)
            {
                decimal total = 0, dis=0;
                //decimal.TryParse(drow["mnc_fn_amt"].ToString(), out total);
                decimal.TryParse(drow["mnc_fn_pad"].ToString(), out total);
                decimal.TryParse(drow["mnc_fn_dis"].ToString(), out dis);
                grfUcepHnPaidTypeDetail[i, 1] = drow["MNC_CHARGE_CD"].ToString();
                grfUcepHnPaidTypeDetail[i, 2] = drow["MNC_SUB_CHARGE_CD"].ToString();
                grfUcepHnPaidTypeDetail[i, 3] = drow["MNC_FN_DSCT"].ToString();
                grfUcepHnPaidTypeDetail[i, 4] = total.ToString("#,###.00");
                grfUcepHnPaidTypeDetail[i, 5] = dis.ToString("#,##0.00");
                grfUcepHnPaidTypeDetail[i, 6] = drow["MNC_SIMB_CD"].ToString();
                i++;
            }
            grfUcepHnPaidTypeDetail.Cols[1].AllowEditing = false;
            grfUcepHnPaidTypeDetail.Cols[2].AllowEditing = false;
            grfUcepHnPaidTypeDetail.Cols[3].AllowEditing = false;
            grfUcepHnPaidTypeDetail.Cols[4].AllowEditing = false;
            grfUcepHnPaidTypeDetail.Cols[5].AllowEditing = false;
            grfUcepHnPaidTypeDetail.Cols[6].AllowEditing = false;
        }

        private void ContextMenu_grfUcepSelect_ExportExcel(object sender, System.EventArgs e)
        {
            flagpaidtype = "";
            exportUcelExcel();
        }
        private void exportUcelExcel()
        {
            pageLoad = true;
            showLbLoading();
            DataTable dtUcepGen = new DataTable();
            dtUcepGen.Columns.Add("use_date", typeof(String));
            dtUcepGen.Columns.Add("tmt_code", typeof(String));
            dtUcepGen.Columns.Add("hosp_code", typeof(String));
            dtUcepGen.Columns.Add("cat_1_16", typeof(String));
            dtUcepGen.Columns.Add("mean", typeof(String));
            dtUcepGen.Columns.Add("unit", typeof(String));
            dtUcepGen.Columns.Add("price_total", typeof(String));
            dtUcepGen.Columns.Add("hosp_code1", typeof(String));
            int row1 = 0;
            String hn = "", hnyear = "", preno = "", vsdate = "", an = "", anyear = "", andate = "", statusipd = "", doccd = "", docyr = "", docno = "", docdate = "";
            String[] wheresplit = flagpaidtype.Split('#');
            if (wheresplit.Length > 9)
            {
                //IPD
                doccd = wheresplit[0];
                docyr = wheresplit[1];
                docno = wheresplit[2];
                docdate = wheresplit[3];
                hn = wheresplit[4];
                hnyear = wheresplit[5];
                vsdate = wheresplit[6];
                preno = wheresplit[7];
                an = wheresplit[8];
                anyear = wheresplit[9];
                andate = wheresplit[10];

                DataTable dtdrug = bc.bcDB.vsDB.selectOrderDrugUcepIPDByHn(hn, hnyear, an, anyear);     //drug
                foreach (DataRow rowdrug in dtdrug.Rows)
                {
                    float qty = 0, price = 0;
                    float.TryParse(rowdrug["qty"].ToString(), out qty);
                    float.TryParse(rowdrug["MNC_PH_PRI"].ToString(), out price);
                    String reqtime = "0000" + rowdrug["MNC_REQ_TIM"].ToString();
                    reqtime = reqtime.Substring(reqtime.Length - 4);
                    reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                    DataRow rowucep = dtUcepGen.Rows.Add();
                    rowucep["use_date"] = rowdrug["mnc_req_dat"].ToString() + " " + reqtime;
                    rowucep["tmt_code"] = rowdrug["tmt_code"].ToString();
                    rowucep["hosp_code"] = rowdrug["MNC_PH_CD"].ToString();
                    rowucep["cat_1_16"] = rowdrug["mnc_grp_ss1"].ToString();
                    rowucep["mean"] = rowdrug["MNC_PH_TN"].ToString();
                    rowucep["unit"] = rowdrug["qty"].ToString();
                    rowucep["price_total"] = (qty * price);
                    rowucep["hosp_code1"] = rowdrug["MNC_fn_CD"].ToString();//mnc_grp_ss1
                }
                //new LogWriter("d", "FrmOPBKKClaim exportUcelExcel anyear " + anyear+" an "+an+ " andate " + andate+ " hn " + hn + " hnyear " + hnyear);
                DataTable dtorder = bc.bcDB.vsDB.selectOrderHotChargeUcepIPDByHn(hn, hnyear, an, anyear);       //hotcharge
                //new LogWriter("d", "FrmOPBKKClaim exportUcelExcel dtorder.Rows.Count " + dtorder.Rows.Count);
                foreach (DataRow rowdrug in dtorder.Rows)
                {
                    float qty = 0, price = 0;
                    float.TryParse(rowdrug["qty"].ToString(), out qty);
                    float.TryParse(rowdrug["MNC_SR_PRI"].ToString(), out price);
                    String reqtime = "0000" + rowdrug["mnc_stamp_tim"].ToString();
                    reqtime = reqtime.Substring(reqtime.Length - 4);
                    reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                    DataRow rowucep = dtUcepGen.Rows.Add();
                    rowucep["use_date"] = rowdrug["mnc_req_dat"].ToString() + " " + reqtime;
                    rowucep["tmt_code"] = rowdrug["ucep_code"].ToString();
                    rowucep["hosp_code"] = rowdrug["mnc_sr_cd"].ToString();
                    rowucep["cat_1_16"] = "";
                    rowucep["mean"] = rowdrug["MNC_SR_DSC"].ToString();
                    rowucep["unit"] = rowdrug["qty"].ToString();
                    rowucep["price_total"] = (qty * price);
                    rowucep["hosp_code1"] = rowdrug["MNC_fn_CD"].ToString();
                    //new LogWriter("d", "FrmOPBKKClaim exportUcelExcel rowucep['tmt_code'] " + rowucep["tmt_code"].ToString());
                }
                DataTable dtfin = bc.bcDB.vsDB.selectOrderFinanceUcepIPDByHn(hn, hnyear, an, anyear);
                //new LogWriter("d", "FrmOPBKKClaim exportUcelExcel dtorder.Rows.Count " + dtorder.Rows.Count);
                foreach (DataRow rowdrug in dtfin.Rows)
                {
                    float qty = 0, price = 0;
                    //float.TryParse(rowdrug["qty"].ToString(), out qty);
                    float.TryParse(rowdrug["MNC_FN_AMT"].ToString(), out price);
                    String reqtime = "0000" + rowdrug["MNC_FN_TIME"].ToString();
                    reqtime = reqtime.Substring(reqtime.Length - 4);
                    reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                    DataRow rowucep = dtUcepGen.Rows.Add();
                    rowucep["use_date"] = rowdrug["MNC_FN_DAT"].ToString() + " " + reqtime;
                    rowucep["tmt_code"] = rowdrug["ucep_code"].ToString();
                    rowucep["hosp_code"] = rowdrug["mnc_charge_cd"].ToString() + rowdrug["mnc_sub_charge_cd"].ToString();
                    rowucep["cat_1_16"] = "";
                    rowucep["mean"] = rowdrug["MNC_FN_DSCT"].ToString();
                    rowucep["unit"] = "1";
                    rowucep["price_total"] = (1 * price);
                    rowucep["hosp_code1"] = rowdrug["MNC_fn_CD"].ToString();
                    //new LogWriter("d", "FrmOPBKKClaim exportUcelExcel rowucep['tmt_code'] " + rowucep["tmt_code"].ToString());
                }
                DataTable dtlab = bc.bcDB.vsDB.selectResultLabUcepbyAN(hn, an, anyear);
                foreach (DataRow rowlab in dtlab.Rows)
                {
                    String reqtime = "0000" + rowlab["mnc_stamp_tim"].ToString();
                    reqtime = reqtime.Substring(reqtime.Length - 4);
                    reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                    DataRow rowucep = dtUcepGen.Rows.Add();
                    rowucep["use_date"] = rowlab["mnc_req_dat"].ToString() + " " + reqtime;
                    rowucep["tmt_code"] = rowlab["ucep_code"].ToString();
                    rowucep["hosp_code"] = rowlab["mnc_lb_cd"].ToString();
                    rowucep["cat_1_16"] = "";
                    rowucep["mean"] = rowlab["MNC_LB_DSC"].ToString();
                    rowucep["unit"] = "1";
                    rowucep["price_total"] = rowlab["MNC_LB_PRI"].ToString();
                    rowucep["hosp_code1"] = rowlab["MNC_fn_cd"].ToString();
                }
                DataTable dtxray = bc.bcDB.vsDB.selectResultXraybyAN(hn, an, anyear);
                foreach (DataRow rowxray in dtxray.Rows)
                {
                    String reqtime = "0000" + rowxray["mnc_stamp_tim"].ToString();
                    reqtime = reqtime.Substring(reqtime.Length - 4);
                    reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                    DataRow rowucep = dtUcepGen.Rows.Add();
                    rowucep["use_date"] = rowxray["mnc_req_dat"].ToString() + " " + reqtime;
                    rowucep["tmt_code"] = rowxray["ucep_code"].ToString();
                    rowucep["hosp_code"] = rowxray["MNC_XR_CD"].ToString();
                    rowucep["cat_1_16"] = "";
                    rowucep["mean"] = rowxray["MNC_XR_DSC"].ToString();
                    rowucep["unit"] = "1";
                    rowucep["price_total"] = rowxray["MNC_XR_PRI_R"].ToString();
                    rowucep["hosp_code1"] = rowxray["MNC_fn_cd"].ToString();
                }

                //statusipd = row[colGrfUcepSelectStatusOPDIPD].ToString();
                //an = row[colGrfUcepSelectAnNo].ToString();
                //anyear = row[colGrfUcepSelectAnYear].ToString();
                //andate = bc.datetoDB(row[colGrfUcepSelectAnDate].ToString());
            }
            else
            {
                foreach (Row row in grfUcepSelect.Rows)
                {
                    row1++;
                    setLbLoading("Ucep gen  " + row1 + "/" + grfUcepSelect.Rows.Count);
                    if (row[colGrfUcepSelectHn] == null) continue;
                    if (row[colGrfUcepSelectSelect] == null) continue;
                    if ((Boolean)row[colGrfUcepSelectSelect])
                    {
                        //String hn = "", hnyear = "", preno = "", vsdate = "", an = "", anyear = "", andate = "", statusipd = "";
                        hn = row[colGrfUcepSelectHn].ToString();
                        hnyear = row[colGrfUcepSelectHnYear].ToString();
                        preno = row[colGrfUcepSelectPreno].ToString();
                        vsdate = row[colGrfUcepSelectVsDate].ToString();
                        statusipd = row[colGrfUcepSelectStatusOPDIPD].ToString();
                        an = row[colGrfUcepSelectAnNo].ToString();
                        anyear = row[colGrfUcepSelectAnYear].ToString();
                        andate = bc.datetoDB(row[colGrfUcepSelectAnDate].ToString());
                        //new LogWriter("d", "FrmOPBKKClaim exportUcelExcel andate0 " + andate);
                        if (statusipd.Equals("OPD"))
                        {
                            DateTime vsdate1 = new DateTime();
                            if (DateTime.TryParse(vsdate, out vsdate1))
                            {
                                DataTable dtdrug = bc.bcDB.vsDB.selectDrugUcepByHn(hn, hnyear, vsdate, preno);
                                foreach (DataRow rowdrug in dtdrug.Rows)
                                {
                                    float qty = 0, price = 0;
                                    float.TryParse(rowdrug["qty"].ToString(), out qty);
                                    float.TryParse(rowdrug["MNC_PH_PRI"].ToString(), out price);
                                    String reqtime = "0000" + rowdrug["MNC_REQ_TIM"].ToString();
                                    reqtime = reqtime.Substring(reqtime.Length - 4);
                                    reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                                    DataRow rowucep = dtUcepGen.Rows.Add();
                                    rowucep["use_date"] = rowdrug["mnc_req_dat"].ToString() + " " + reqtime;
                                    rowucep["tmt_code"] = rowdrug["tmt_code"].ToString();
                                    rowucep["hosp_code"] = rowdrug["MNC_PH_CD"].ToString();
                                    rowucep["cat_1_16"] = rowdrug["mnc_grp_ss1"].ToString();
                                    rowucep["mean"] = rowdrug["MNC_PH_TN"].ToString();
                                    rowucep["unit"] = rowdrug["qty"].ToString();
                                    rowucep["price_total"] = qty * price;
                                    rowucep["hosp_code1"] = rowdrug["MNC_fn_CD"].ToString();
                                }
                                //DataTable dtorder = bc.bcDB.vsDB.selectOrderHotChargeUcepIPDByHn(hn, hnyear, an, anyear);
                                DataTable dtorder = bc.bcDB.vsDB.selectOrderHotChargeUcepOPDByHn(hn, hnyear, vsdate, preno);
                                foreach (DataRow rowdrug in dtorder.Rows)
                                {
                                    float qty = 0, price = 0;
                                    float.TryParse(rowdrug["qty"].ToString(), out qty);
                                    float.TryParse(rowdrug["MNC_SR_PRI"].ToString(), out price);
                                    String reqtime = "0000" + rowdrug["mnc_stamp_tim"].ToString();
                                    reqtime = reqtime.Substring(reqtime.Length - 4);
                                    reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                                    DataRow rowucep = dtUcepGen.Rows.Add();
                                    rowucep["use_date"] = rowdrug["mnc_req_dat"].ToString() + " " + reqtime;
                                    rowucep["tmt_code"] = rowdrug["ucep_code"].ToString();
                                    rowucep["hosp_code"] = rowdrug["mnc_sr_cd"].ToString();
                                    rowucep["cat_1_16"] = "";
                                    rowucep["mean"] = rowdrug["MNC_SR_DSC"].ToString();
                                    rowucep["unit"] = rowdrug["qty"].ToString();
                                    rowucep["price_total"] = (qty * price);
                                }
                                DataTable dtlab = bc.bcDB.vsDB.selectResultLabUcepbyPreno(hn, vsdate, preno);
                                foreach (DataRow rowlab in dtlab.Rows)
                                {
                                    String reqtime = "0000" + rowlab["mnc_stamp_tim"].ToString();
                                    reqtime = reqtime.Substring(reqtime.Length - 4);
                                    reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                                    DataRow rowucep = dtUcepGen.Rows.Add();
                                    rowucep["use_date"] = rowlab["mnc_req_dat"].ToString() + " " + reqtime;
                                    rowucep["tmt_code"] = rowlab["ucep_code"].ToString();
                                    rowucep["hosp_code"] = rowlab["mnc_lb_cd"].ToString();
                                    rowucep["cat_1_16"] = "";
                                    rowucep["mean"] = rowlab["MNC_LB_DSC"].ToString();
                                    rowucep["unit"] = "1";
                                    rowucep["price_total"] = rowlab["MNC_LB_PRI"].ToString();
                                    rowucep["hosp_code1"] = rowlab["MNC_fn_cd"].ToString();
                                }
                                DataTable dtxray = bc.bcDB.vsDB.selectResultXraybyPreno(hn, vsdate, preno);
                                foreach (DataRow rowxray in dtxray.Rows)
                                {
                                    String reqtime = "0000" + rowxray["mnc_stamp_tim"].ToString();
                                    reqtime = reqtime.Substring(reqtime.Length - 4);
                                    reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                                    DataRow rowucep = dtUcepGen.Rows.Add();
                                    rowucep["use_date"] = rowxray["mnc_req_dat"].ToString() + " " + reqtime;
                                    rowucep["tmt_code"] = rowxray["ucep_code"].ToString();
                                    rowucep["hosp_code"] = rowxray["MNC_XR_CD"].ToString();
                                    rowucep["cat_1_16"] = "";
                                    rowucep["mean"] = rowxray["MNC_XR_DSC"].ToString();
                                    rowucep["unit"] = "1";
                                    rowucep["price_total"] = rowxray["MNC_XR_PRI"].ToString();
                                    rowucep["hosp_code1"] = rowxray["MNC_fn_cd"].ToString();
                                }
                            }
                        }
                        else
                        {       //IPD
                            DateTime andate1 = new DateTime();
                            DateTime date = new DateTime();
                            //DateTime.TryParse(andate, out date);
                            //new LogWriter("d", "FrmOPBKKClaim exportUcelExcel andate " + andate);
                            //andate1.AddYears(int.Parse(row[colGrfUcepSelectAnDate].ToString().Substring(row[colGrfUcepSelectAnDate].ToString().Length - 4)));
                            //andate1.AddMonths(int.Parse(row[colGrfUcepSelectAnDate].ToString().Substring(3,2)));
                            //andate1.AddDays(int.Parse(row[colGrfUcepSelectAnDate].ToString().Substring(0,2)));

                            //if (DateTime.TryParse(andate, out andate1))
                            //{
                            //if (andate1.Year > 2500)
                            //{
                            //    andate1 = andate1.AddYears(-543);
                            //}
                            //else if (andate1.Year < 2000)
                            //{
                            //    andate1 = andate1.AddYears(543);
                            //}
                            //new LogWriter("d", "FrmOPBKKClaim exportUcelExcel andate1 " + andate1.ToString("yyyy-MM-dd", new CultureInfo("en-US")));
                            //andate = andate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                            DataTable dtdrug = new DataTable();
                            if (chkUcepAllVisit.Checked)
                            {
                                dtdrug = bc.bcDB.vsDB.selectDrugUcepByHn(hn, hnyear, vsdate, preno);
                            }
                            else
                            {
                                dtdrug = bc.bcDB.vsDB.selectOrderDrugUcepIPDByHn(hn, hnyear, an, anyear);
                            }
                            foreach (DataRow rowdrug in dtdrug.Rows)
                            {
                                float qty = 0, price = 0;
                                float.TryParse(rowdrug["qty"].ToString(), out qty);
                                float.TryParse(rowdrug["MNC_PH_PRI"].ToString(), out price);
                                String reqtime = "0000" + rowdrug["MNC_REQ_TIM"].ToString();
                                reqtime = reqtime.Substring(reqtime.Length - 4);
                                reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                                DataRow rowucep = dtUcepGen.Rows.Add();
                                rowucep["use_date"] = rowdrug["mnc_req_dat"].ToString() + " " + reqtime;
                                rowucep["tmt_code"] = rowdrug["tmt_code"].ToString();
                                rowucep["hosp_code"] = rowdrug["MNC_PH_CD"].ToString();
                                rowucep["cat_1_16"] = rowdrug["mnc_grp_ss1"].ToString();
                                rowucep["mean"] = rowdrug["MNC_PH_TN"].ToString();
                                rowucep["unit"] = rowdrug["qty"].ToString();
                                rowucep["price_total"] = (qty * price);
                                rowucep["hosp_code1"] = rowdrug["MNC_fn_CD"].ToString();//mnc_grp_ss1
                            }
                            //new LogWriter("d", "FrmOPBKKClaim exportUcelExcel anyear " + anyear+" an "+an+ " andate " + andate+ " hn " + hn + " hnyear " + hnyear);
                            DataTable dtorder = bc.bcDB.vsDB.selectOrderHotChargeUcepIPDByHn(hn, hnyear, an, anyear);
                            //new LogWriter("d", "FrmOPBKKClaim exportUcelExcel dtorder.Rows.Count " + dtorder.Rows.Count);
                            foreach (DataRow rowdrug in dtorder.Rows)
                            {
                                float qty = 0, price = 0;
                                float.TryParse(rowdrug["qty"].ToString(), out qty);
                                float.TryParse(rowdrug["MNC_SR_PRI"].ToString(), out price);
                                String reqtime = "0000" + rowdrug["mnc_stamp_tim"].ToString();
                                reqtime = reqtime.Substring(reqtime.Length - 4);
                                reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                                DataRow rowucep = dtUcepGen.Rows.Add();
                                rowucep["use_date"] = rowdrug["mnc_req_dat"].ToString() + " " + reqtime;
                                rowucep["tmt_code"] = rowdrug["ucep_code"].ToString();
                                rowucep["hosp_code"] = rowdrug["mnc_sr_cd"].ToString();
                                rowucep["cat_1_16"] = "";
                                rowucep["mean"] = rowdrug["MNC_SR_DSC"].ToString();
                                rowucep["unit"] = rowdrug["qty"].ToString();
                                rowucep["price_total"] = (qty * price);
                                //rowucep["hosp_code1"] = rowdrug["MNC_fn_CD"].ToString();
                                //new LogWriter("d", "FrmOPBKKClaim exportUcelExcel rowucep['tmt_code'] " + rowucep["tmt_code"].ToString());
                            }
                            DataTable dtfin = bc.bcDB.vsDB.selectOrderFinanceUcepIPDByHn(hn, hnyear, an, anyear);
                            //new LogWriter("d", "FrmOPBKKClaim exportUcelExcel dtorder.Rows.Count " + dtorder.Rows.Count);
                            foreach (DataRow rowdrug in dtfin.Rows)
                            {
                                float qty = 0, price = 0;
                                //float.TryParse(rowdrug["qty"].ToString(), out qty);
                                float.TryParse(rowdrug["MNC_FN_AMT"].ToString(), out price);
                                String reqtime = "0000" + rowdrug["MNC_FN_TIME"].ToString();
                                reqtime = reqtime.Substring(reqtime.Length - 4);
                                reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                                DataRow rowucep = dtUcepGen.Rows.Add();
                                rowucep["use_date"] = rowdrug["MNC_FN_DAT"].ToString() + " " + reqtime;
                                rowucep["tmt_code"] = rowdrug["ucep_code"].ToString();
                                rowucep["hosp_code"] = rowdrug["mnc_charge_cd"].ToString() + rowdrug["mnc_sub_charge_cd"].ToString();
                                rowucep["cat_1_16"] = "";
                                rowucep["mean"] = rowdrug["MNC_FN_DSCT"].ToString();
                                rowucep["unit"] = "1";
                                rowucep["price_total"] = (1 * price);
                                rowucep["hosp_code1"] = rowdrug["MNC_fn_CD"].ToString();
                                //new LogWriter("d", "FrmOPBKKClaim exportUcelExcel rowucep['tmt_code'] " + rowucep["tmt_code"].ToString());
                            }
                            DataTable dtlabipd = new DataTable();
                            if (chkUcepAllVisit.Checked)
                            {
                                dtlabipd = bc.bcDB.vsDB.selectResultLabUcepbyPreno(hn, vsdate, preno);
                            }
                            else
                            {
                                dtlabipd = bc.bcDB.vsDB.selectResultLabUcepbyAN(hn, an, anyear);
                            }
                            foreach (DataRow rowlab in dtlabipd.Rows)
                            {
                                String reqtime = "0000" + rowlab["mnc_stamp_tim"].ToString();
                                reqtime = reqtime.Substring(reqtime.Length - 4);
                                reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                                DataRow rowucep = dtUcepGen.Rows.Add();
                                rowucep["use_date"] = rowlab["mnc_req_dat"].ToString() + " " + reqtime;
                                rowucep["tmt_code"] = rowlab["ucep_code"].ToString();
                                rowucep["hosp_code"] = rowlab["mnc_lb_cd"].ToString();
                                rowucep["cat_1_16"] = "";
                                rowucep["mean"] = rowlab["MNC_LB_DSC"].ToString();
                                rowucep["unit"] = "1";
                                rowucep["price_total"] = rowlab["MNC_LB_PRI"].ToString();
                                rowucep["hosp_code1"] = rowlab["MNC_fn_cd"].ToString();
                            }
                            DataTable dtxray = new DataTable();
                            if (chkUcepAllVisit.Checked)
                            {
                                dtxray = bc.bcDB.vsDB.selectResultXraybyPreno(hn, vsdate, preno);
                            }
                            else
                            {
                                dtxray = bc.bcDB.vsDB.selectResultXraybyAN(hn, an, anyear);
                            }
                            foreach (DataRow rowxray in dtxray.Rows)
                            {
                                String reqtime = "0000" + rowxray["mnc_stamp_tim"].ToString();
                                reqtime = reqtime.Substring(reqtime.Length - 4);
                                reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                                DataRow rowucep = dtUcepGen.Rows.Add();
                                rowucep["use_date"] = rowxray["mnc_req_dat"].ToString() + " " + reqtime;
                                rowucep["tmt_code"] = rowxray["ucep_code"].ToString();
                                rowucep["hosp_code"] = rowxray["MNC_XR_CD"].ToString();
                                rowucep["cat_1_16"] = "";
                                rowucep["mean"] = rowxray["MNC_XR_DSC"].ToString();
                                rowucep["unit"] = "1";
                                rowucep["price_total"] = rowxray["MNC_XR_PRI_R"].ToString();
                                rowucep["hosp_code1"] = rowxray["MNC_fn_cd"].ToString();
                            }
                            //}
                        }
                    }
                }
            }
            
            hideLbLoading();
            CreateExcelFile(dtUcepGen, hn);
            pageLoad = false;
        }
        private string CreateExcelFile(DataTable dt, String hn)
        {
            //clear Excel book, remove the single blank sheet
            C1.C1Excel.C1XLBook _c1xl = new C1.C1Excel.C1XLBook();
            XLStyle _styTitle;
            XLStyle _styHeader;
            XLStyle _styMoney;
            XLStyle _styOrder;
            _c1xl.Clear();
            _c1xl.Sheets.Clear();
            _c1xl.DefaultFont = new Font("Tahoma", 8);

            //create Excel styles
            _styTitle = new XLStyle(_c1xl);
            _styHeader = new XLStyle(_c1xl);
            _styMoney = new XLStyle(_c1xl);
            _styOrder = new XLStyle(_c1xl);

            //set up styles
            _styTitle.Font = new Font(_c1xl.DefaultFont.Name, 15, FontStyle.Bold);
            _styTitle.ForeColor = Color.Blue;
            _styHeader.Font = new Font(_c1xl.DefaultFont, FontStyle.Bold);
            _styHeader.ForeColor = Color.White;
            _styHeader.BackColor = Color.DarkGray;
            _styMoney.Format = XLStyle.FormatDotNetToXL("c");
            _styOrder.Font = _styHeader.Font;
            _styOrder.ForeColor = Color.Red;

            //create report with one sheet per category
            //DataTable dt = GetCategories();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    CreateSheet(_c1xl,dr);
            //}
            CreateSheet(_c1xl, _styTitle, _styHeader, _styOrder, _styMoney, dt);
            //save xls file
            String datetick = "";
            pathfile = bc.iniC.medicalrecordexportpath;
            if (!Directory.Exists(pathfile))
            {
                Directory.CreateDirectory(pathfile);
                Application.DoEvents();
            }
            datetick = DateTime.Now.Ticks.ToString();
            if (hn.Length > 0)
            {
                pathfile = pathfile + "\\" + hn + "\\";
                if (!Directory.Exists(pathfile))
                {
                    Directory.CreateDirectory(pathfile);
                    Application.DoEvents();
                }
                System.IO.DirectoryInfo di = new DirectoryInfo(pathfile);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }

            string filename = pathfile+ "\\fs_template.xls";
            _c1xl.Save(filename);
            Process.Start("explorer.exe", pathfile);
            return filename;
        }
        private void CreateSheet(C1XLBook _c1xl, XLStyle _styTitle, XLStyle _styHeader, XLStyle _styOrder, XLStyle _styMoney, DataTable dt)
        {
            //get current category name
            string catName = "ucep";

            //add a new worksheet to the workbook 
            //('/' is invalid in sheet names, so replace it with '+')
            string sheetName = catName.Replace("/", " + ");
            XLSheet sheet = _c1xl.Sheets.Add(sheetName);

            //add title to worksheet
            sheet[0, 0].Value = catName;
            sheet.Rows[0].Style = _styTitle;

            // set column widths (in twips)
            sheet.Columns[0].Width = 2000;
            sheet.Columns[1].Width = 2000;
            sheet.Columns[2].Width = 1500;
            sheet.Columns[3].Width = 1500;
            sheet.Columns[4].Width = 4000;
            sheet.Columns[5].Width = 1000;
            sheet.Columns[6].Width = 1000;
            sheet.Columns[7].Width = 1500;

            //add column headers
            int row = 0;
            sheet.Rows[row].Style = _styHeader;

            sheet[row, 0].Value = "use date";
            sheet[row, 1].Value = "Feeschedule/TMT code";
            sheet[row, 2].Value = "Hospital code";
            sheet[row, 3].Value = "category 1-16";
            sheet[row, 4].Value = "mean";
            sheet[row, 5].Value = "unit";
            sheet[row, 6].Value = "price_total";
            sheet[row, 7].Value = "hospital code1";

            //loop through products in this category
            //DataRow[] products = dr.GetChildRows("Categories_Products");
            foreach (DataRow drow in dt.Rows)
            {
                row++;
                sheet[row, 0].Value = drow["use_date"].ToString();
                sheet[row, 1].Value = drow["tmt_code"].ToString();
                sheet[row, 2].Value = drow["hosp_code"].ToString();
                sheet[row, 3].Value = drow["cat_1_16"].ToString();
                sheet[row, 4].Value = drow["mean"].ToString();
                sheet[row, 5].Value = drow["unit"].ToString();
                sheet[row, 6].Value = drow["price_total"].ToString();
                sheet[row, 7].Value = drow["hosp_code1"].ToString();
            }
            //if (products.Length == 0)
            //{
            //    row++;
            //    sheet[row, 1].Value = "No products in this category";
            //}
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
            if (grfPAT != null) grfINS.Dispose();
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
            grfINS.CellChanged += GrfINS_CellChanged;
            tabOPBKKINS.Controls.Add(grfINS);
            theme1.SetTheme(grfINS, "Office2010Red");
            ContextMenu menuGwINS = new ContextMenu();
            menuGwINS.MenuItems.Add("save INS", new EventHandler(ContextMenu_grfINS_save));
            grfINS.ContextMenu = menuGwINS;

            grfPAT = new C1FlexGrid();
            grfPAT.Font = fEdit;
            grfPAT.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPAT.Location = new System.Drawing.Point(0, 0);
            grfPAT.Rows.Count = 1;
            tabOPBKKPAT.Controls.Add(grfPAT);
            theme1.SetTheme(grfPAT, "Office2010Red");
            grfPAT.CellChanged += GrfPAT_CellChanged;
            ContextMenu menuGwPAT = new ContextMenu();
            menuGwPAT.MenuItems.Add("save PAT", new EventHandler(ContextMenu_grfPAT_save));
            grfPAT.ContextMenu = menuGwPAT;

            grfOPD = new C1FlexGrid();
            grfOPD.Font = fEdit;
            grfOPD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPD.Location = new System.Drawing.Point(0, 0);
            grfOPD.Rows.Count = 1;
            tabOPBKKOPD.Controls.Add(grfOPD);
            theme1.SetTheme(grfOPD, "Office2010Red");
            grfOPD.CellChanged += GrfOPD_CellChanged;
            ContextMenu menuGwOPD = new ContextMenu();
            menuGwOPD.MenuItems.Add("save OPD", new EventHandler(ContextMenu_grfOPD_save));
            grfOPD.ContextMenu = menuGwOPD;

            grfODX = new C1FlexGrid();
            grfODX.Font = fEdit;
            grfODX.Dock = System.Windows.Forms.DockStyle.Fill;
            grfODX.Location = new System.Drawing.Point(0, 0);
            grfODX.Rows.Count = 1;
            tabOPBKKODX.Controls.Add(grfODX);
            theme1.SetTheme(grfODX, "Office2010Red");
            grfODX.CellChanged += GrfODX_CellChanged;
            ContextMenu menuGwODX = new ContextMenu();
            menuGwODX.MenuItems.Add("save ODX", new EventHandler(ContextMenu_grfODX_save));
            grfODX.ContextMenu = menuGwODX;

            grfOOP = new C1FlexGrid();
            grfOOP.Font = fEdit;
            grfOOP.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOOP.Location = new System.Drawing.Point(0, 0);
            grfOOP.Rows.Count = 1;
            tabOPBKKOOP.Controls.Add(grfOOP);
            theme1.SetTheme(grfOOP, "Office2010Red");
            grfOOP.CellChanged += GrfOOP_CellChanged;
            ContextMenu menuGwOOP = new ContextMenu();
            menuGwOOP.MenuItems.Add("save ODX", new EventHandler(ContextMenu_grfOOP_save));
            grfOOP.ContextMenu = menuGwOOP;

            grfORF = new C1FlexGrid();
            grfORF.Font = fEdit;
            grfORF.Dock = System.Windows.Forms.DockStyle.Fill;
            grfORF.Location = new System.Drawing.Point(0, 0);
            grfORF.Rows.Count = 1;
            tabOPBKKORF.Controls.Add(grfORF);
            theme1.SetTheme(grfORF, "Office2010Red");
            grfORF.CellChanged += GrfORF_CellChanged;
            ContextMenu menuGwORF = new ContextMenu();
            menuGwORF.MenuItems.Add("save ORF", new EventHandler(ContextMenu_grfORF_save));
            grfORF.ContextMenu = menuGwORF;

            grfCHT = new C1FlexGrid();
            grfCHT.Font = fEdit;
            grfCHT.Dock = System.Windows.Forms.DockStyle.Fill;
            grfCHT.Location = new System.Drawing.Point(0, 0);
            grfCHT.Rows.Count = 1;
            tabOPBKKCHT.Controls.Add(grfCHT);
            theme1.SetTheme(grfCHT, "Office2010Red");
            grfCHT.CellChanged += GrfCHT_CellChanged;
            ContextMenu menuGwCHT = new ContextMenu();
            menuGwCHT.MenuItems.Add("save CHT", new EventHandler(ContextMenu_grfCHT_save));
            grfCHT.ContextMenu = menuGwCHT;

            grfCHA = new C1FlexGrid();
            grfCHA.Font = fEdit;
            grfCHA.Dock = System.Windows.Forms.DockStyle.Fill;
            grfCHA.Location = new System.Drawing.Point(0, 0);
            grfCHA.Rows.Count = 1;
            tabOPBKKCHA.Controls.Add(grfCHA);
            theme1.SetTheme(grfCHA, "Office2010Red");
            grfCHA.CellChanged += GrfCHA_CellChanged;
            ContextMenu menuGwCHA = new ContextMenu();
            menuGwCHA.MenuItems.Add("save CHA", new EventHandler(ContextMenu_grfCHA_save));
            grfCHA.ContextMenu = menuGwCHA;

            grfAER = new C1FlexGrid();
            grfAER.Font = fEdit;
            grfAER.Dock = System.Windows.Forms.DockStyle.Fill;
            grfAER.Location = new System.Drawing.Point(0, 0);
            grfAER.Rows.Count = 1;
            tabOPBKKAER.Controls.Add(grfAER);
            theme1.SetTheme(grfAER, "Office2010Red");
            grfAER.CellChanged += GrfAER_CellChanged;
            ContextMenu menuGwAER = new ContextMenu();
            menuGwAER.MenuItems.Add("save AER", new EventHandler(ContextMenu_grfAER_save));
            grfAER.ContextMenu = menuGwAER;

            grfLABFU = new C1FlexGrid();
            grfLABFU.Font = fEdit;
            grfLABFU.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLABFU.Location = new System.Drawing.Point(0, 0);
            grfLABFU.Rows.Count = 1;
            tabOPBKKLABFU.Controls.Add(grfLABFU);
            theme1.SetTheme(grfLABFU, "Office2010Red");
            grfLABFU.CellChanged += GrfLABFU_CellChanged;
            ContextMenu menuGwLABFU = new ContextMenu();
            menuGwLABFU.MenuItems.Add("save LABFU", new EventHandler(ContextMenu_grfLABFU_save));
            grfLABFU.ContextMenu = menuGwLABFU;

            grfDRU = new C1FlexGrid();
            grfDRU.Font = fEdit;
            grfDRU.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDRU.Location = new System.Drawing.Point(0, 0);
            grfDRU.Rows.Count = 1;
            tabOPBKKDRU.Controls.Add(grfDRU);
            theme1.SetTheme(grfDRU, "Office2010Red");
            grfDRU.CellChanged += GrfDRU_CellChanged;
            ContextMenu menuGwDRU = new ContextMenu();
            menuGwDRU.MenuItems.Add("save DRU", new EventHandler(ContextMenu_grfDRU_save));
            grfDRU.ContextMenu = menuGwDRU;

            grfCHAD = new C1FlexGrid();
            grfCHAD.Font = fEdit;
            grfCHAD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfCHAD.Location = new System.Drawing.Point(0, 0);
            grfCHAD.Rows.Count = 1;
            tabOPBKKCHAD.Controls.Add(grfCHAD);
            theme1.SetTheme(grfCHAD, "Office2010Red");
            grfCHAD.CellChanged += GrfCHAD_CellChanged;
            ContextMenu menuGwCHAD = new ContextMenu();
            menuGwCHAD.MenuItems.Add("save CHAD", new EventHandler(ContextMenu_grfCHAD_save));
            grfCHAD.ContextMenu = menuGwCHAD;
        }

        private void GrfCHAD_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfCHAD.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfCHAD[e.Row, grfCHAD.Cols.Count - 1] = "1";
        }

        private void GrfDRU_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfDRU.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfDRU[e.Row, grfDRU.Cols.Count - 1] = "1";
        }

        private void GrfLABFU_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfLABFU.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfLABFU[e.Row, grfLABFU.Cols.Count - 1] = "1";
        }

        private void GrfAER_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfAER.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfAER[e.Row, grfAER.Cols.Count - 1] = "1";
        }

        private void GrfCHA_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfCHA.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfCHA[e.Row, grfCHA.Cols.Count - 1] = "1";
        }

        private void GrfCHT_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfCHT.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfCHT[e.Row, grfCHT.Cols.Count - 1] = "1";
        }

        private void GrfORF_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfORF.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfORF[e.Row, grfORF.Cols.Count - 1] = "1";
        }

        private void GrfOOP_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfOOP.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfOOP[e.Row, grfOOP.Cols.Count - 1] = "1";
        }

        private void GrfODX_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfODX.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfODX[e.Row, grfODX.Cols.Count - 1] = "1";
        }

        private void GrfOPD_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfOPD.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfOPD[e.Row, grfOPD.Cols.Count - 1] = "1";
        }

        private void GrfPAT_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfPAT.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfPAT[e.Row, grfPAT.Cols.Count - 1] = "1";
        }

        private void GrfINS_CellChanged(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            grfINS.Rows[e.Row].StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
            grfINS[e.Row, grfINS.Cols.Count-1] = "1";
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
        private void setGrfUcepXray()
        {
            pageLoad = true;
            DataTable dt = new DataTable();
            //C1ComboBox cboMethod = new C1ComboBox();
            //cboMethod.AutoCompleteMode = AutoCompleteMode.Suggest;
            //cboMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
            //bc.setCboOPBKKINSCL(cboMethod, "");

            dt = bc.bcDB.xrayM01DB.SelectAll();
            grfUcepXray.Rows.Count = 1;
            grfUcepXray.Cols.Count = 5;
            grfUcepXray.Rows.Count = dt.Rows.Count + 2;
            grfUcepXray.Cols[colGrfUcepXrayId].Caption = "ID";
            grfUcepXray.Cols[colGrfUcepXrayName].Caption = "LAB Name";
            grfUcepXray.Cols[colGrfUcepXrayUcepCode].Caption = "Ucep code";

            grfUcepXray.Cols[colGrfUcepLabId].Width = 80;
            grfUcepXray.Cols[colGrfUcepXrayName].Width = 500;
            grfUcepXray.Cols[colGrfUcepXrayUcepCode].Width = 120;
            
            //grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Editor = cboMethod;
            int i = 1;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                String inscl = "";
                grfUcepXray[i, colGrfUcepXrayId] = row1["MNC_XR_CD"].ToString();
                grfUcepXray[i, colGrfUcepXrayName] = row1["MNC_XR_DSC"].ToString();
                grfUcepXray[i, colGrfUcepXrayUcepCode] = row1["ucep_code"].ToString();
                grfUcepXray[i, colGrfUcepXrayFlag] = "0";
                grfUcepXray[i, 0] = (i - 1);
                if (i % 2 == 0)
                    grfUcepLab.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);

            }
            grfUcepXray.Cols[colGrfUcepXrayFlag].Visible = false;
            grfUcepXray.Cols[colGrfUcepXrayId].AllowEditing = false;
            grfUcepXray.Cols[colGrfUcepXrayName].AllowEditing = false;
            grfUcepXray.Cols[colGrfUcepXrayUcepCode].AllowEditing = true;
            grfUcepXray.AllowFiltering = true;
            //for (int col = 0; col < dt.Columns.Count; ++col)
            //{
            //grfUcepHotcharge.Cols[colGrfUcepPaidId].DataType = dt.Columns["MNC_FN_CD"].DataType;
            ////grfOrdDrug.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
            //grfUcepHotcharge.Cols[colGrfUcepPaidId].Name = dt.Columns["MNC_FN_CD"].ColumnName;
            //grfUcepHotcharge.Cols[colGrfUcepPaidName].DataType = dt.Columns["MNC_FN_DSCT"].DataType;
            //grfUcepHotcharge.Cols[colGrfUcepPaidName].Name = dt.Columns["MNC_FN_DSCT"].ColumnName;
            //}
            pageLoad = false;
        }
        private void setGrfUcepLAB()
        {
            pageLoad = true;
            DataTable dt = new DataTable();
            //C1ComboBox cboMethod = new C1ComboBox();
            //cboMethod.AutoCompleteMode = AutoCompleteMode.Suggest;
            //cboMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
            //bc.setCboOPBKKINSCL(cboMethod, "");

            dt = bc.bcDB.labM01DB.SelectAll();
            grfUcepLab.Rows.Count = 1;
            grfUcepLab.Cols.Count = 5;
            grfUcepLab.Rows.Count = dt.Rows.Count + 2;
            grfUcepLab.Cols[colGrfUcepLabId].Caption = "ID";
            grfUcepLab.Cols[colGrfUcepLabName].Caption = "LAB Name";
            grfUcepLab.Cols[colGrfUcepLabUcepCode].Caption = "Ucep code";

            grfUcepLab.Cols[colGrfUcepLabId].Width = 80;
            grfUcepLab.Cols[colGrfUcepLabName].Width = 500;
            grfUcepLab.Cols[colGrfUcepLabUcepCode].Width = 120;

            //grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Editor = cboMethod;
            int i = 1;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                String inscl = "";
                grfUcepLab[i, colGrfUcepLabId] = row1["MNC_LB_CD"].ToString();
                grfUcepLab[i, colGrfUcepLabName] = row1["MNC_LB_DSC"].ToString();
                grfUcepLab[i, colGrfUcepLabUcepCode] = row1["ucep_code"].ToString();
                grfUcepLab[i, colGrfUcepLabFlag] = "0";
                grfUcepLab[i, 0] = (i - 1);
                if (i % 2 == 0)
                    grfUcepLab.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);

            }
            grfUcepLab.Cols[colGrfUcepLabFlag].Visible = false;
            grfUcepLab.Cols[colGrfUcepLabId].AllowEditing = false;
            grfUcepLab.Cols[colGrfUcepLabName].AllowEditing = false;
            grfUcepLab.Cols[colGrfUcepLabUcepCode].AllowEditing = true;
            grfUcepLab.AllowFiltering = true;
            for (int col = 0; col < dt.Columns.Count; ++col)
            {
                grfUcepLab.Cols[colGrfUcepLabId].DataType = dt.Columns["MNC_LB_CD"].DataType;
                //grfOrdDrug.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
                grfUcepLab.Cols[colGrfUcepLabId].Name = dt.Columns["MNC_LB_CD"].ColumnName;
                grfUcepLab.Cols[colGrfUcepLabName].DataType = dt.Columns["MNC_LB_DSC"].DataType;
                grfUcepLab.Cols[colGrfUcepLabName].Name = dt.Columns["MNC_LB_DSC"].ColumnName;
            }
            pageLoad = false;
        }
        private void setGrfCipnCVS()
        {
            pageLoad = true;
            DataTable dt = new DataTable();
            //C1ComboBox cboMethod = new C1ComboBox();
            //cboMethod.AutoCompleteMode = AutoCompleteMode.Suggest;
            //cboMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
            //bc.setCboOPBKKINSCL(cboMethod, "");

            dt = bc.bcDB.pttM30DB.SelectAll();
            grfCipnCsv.Rows.Count = 1;
            grfCipnCsv.Cols.Count = 7;
            grfCipnCsv.Rows.Count = dt.Rows.Count + 2;
            grfCipnCsv.Cols[colGrfUcepHotChargeId].Caption = "ID";
            grfCipnCsv.Cols[colGrfUcepHotChargeName].Caption = "Hot Charge Name";
            grfCipnCsv.Cols[colGrfUcepHotChargeUcepCode].Caption = "Ucep code";
            grfCipnCsv.Cols[colGrfUcepHotChargeDitCode].Caption = "CSV code";

            grfCipnCsv.Cols[colGrfUcepHotChargeId].Width = 80;
            grfCipnCsv.Cols[colGrfUcepHotChargeName].Width = 500;
            grfCipnCsv.Cols[colGrfUcepHotChargeUcepCode].Width = 120;
            grfCipnCsv.Cols[colGrfUcepHotChargeDitCode].Width = 120;

            //grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Editor = cboMethod;
            int i = 1;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                String inscl = "";
                grfCipnCsv[i, colGrfUcepHotChargeId] = row1["mnc_sr_cd"].ToString();
                grfCipnCsv[i, colGrfUcepHotChargeName] = row1["mnc_sr_dsc"].ToString();
                grfCipnCsv[i, colGrfUcepHotChargeUcepCode] = row1["ucep_code"].ToString();
                grfCipnCsv[i, colGrfUcepHotChargeDitCode] = row1["dit_code"].ToString();
                grfCipnCsv[i, colGrfUcepHotChargeFlag] = "0";
                grfCipnCsv[i, 0] = (i - 1);
                if (i % 2 == 0)
                    grfCipnCsv.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);

            }
            grfCipnCsv.Cols[colGrfUcepHotChargeFlag].Visible = false;
            grfCipnCsv.Cols[colGrfUcepHotChargeUcepCode].Visible = false;
            grfCipnCsv.Cols[colGrfUcepHotChargeDitCode].Visible = false;

            grfCipnCsv.Cols[colGrfUcepHotChargeId].AllowEditing = false;
            grfCipnCsv.Cols[colGrfUcepHotChargeName].AllowEditing = false;
            grfCipnCsv.Cols[colGrfUcepHotChargeUcepCode].AllowEditing = true;
            grfCipnCsv.Cols[colGrfUcepHotChargeDitCode].AllowEditing = true;
            grfCipnCsv.AllowFiltering = true;
            //for (int col = 0; col < dt.Columns.Count; ++col)
            //{
            //grfUcepHotcharge.Cols[colGrfUcepPaidId].DataType = dt.Columns["MNC_FN_CD"].DataType;
            ////grfOrdDrug.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
            //grfUcepHotcharge.Cols[colGrfUcepPaidId].Name = dt.Columns["MNC_FN_CD"].ColumnName;
            //grfUcepHotcharge.Cols[colGrfUcepPaidName].DataType = dt.Columns["MNC_FN_DSCT"].DataType;
            //grfUcepHotcharge.Cols[colGrfUcepPaidName].Name = dt.Columns["MNC_FN_DSCT"].ColumnName;
            //}
            pageLoad = false;
        }
        private void setGrfUcepHotCharge()
        {
            pageLoad = true;
            DataTable dt = new DataTable();
            //C1ComboBox cboMethod = new C1ComboBox();
            //cboMethod.AutoCompleteMode = AutoCompleteMode.Suggest;
            //cboMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
            //bc.setCboOPBKKINSCL(cboMethod, "");

            dt = bc.bcDB.pttM30DB.SelectAll();
            grfUcepHotcharge.Rows.Count = 1;
            grfUcepHotcharge.Cols.Count = 6;
            grfUcepHotcharge.Rows.Count = dt.Rows.Count + 2;
            grfUcepHotcharge.Cols[colGrfUcepHotChargeId].Caption = "ID";
            grfUcepHotcharge.Cols[colGrfUcepHotChargeName].Caption = "Hot Charge Name";
            grfUcepHotcharge.Cols[colGrfUcepHotChargeUcepCode].Caption = "Ucep code";
            grfUcepHotcharge.Cols[colGrfUcepHotChargeDitCode].Caption = "DIT code";

            grfUcepHotcharge.Cols[colGrfUcepHotChargeId].Width = 80;
            grfUcepHotcharge.Cols[colGrfUcepHotChargeName].Width = 500;
            grfUcepHotcharge.Cols[colGrfUcepHotChargeUcepCode].Width = 120;
            grfUcepHotcharge.Cols[colGrfUcepHotChargeDitCode].Width = 120;

            //grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Editor = cboMethod;
            int i = 1;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                String inscl = "";
                grfUcepHotcharge[i, colGrfUcepHotChargeId] = row1["mnc_sr_cd"].ToString();
                grfUcepHotcharge[i, colGrfUcepHotChargeName] = row1["mnc_sr_dsc"].ToString();
                grfUcepHotcharge[i, colGrfUcepHotChargeUcepCode] = row1["ucep_code"].ToString();
                grfUcepHotcharge[i, colGrfUcepHotChargeDitCode] = row1["dit_code"].ToString();
                grfUcepHotcharge[i, colGrfUcepHotChargeFlag] = "0";
                grfUcepHotcharge[i, 0] = (i - 1);
                if (i % 2 == 0)
                    grfUcepHotcharge.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);

            }
            grfUcepHotcharge.Cols[colGrfUcepHotChargeFlag].Visible = false;
            grfUcepHotcharge.Cols[colGrfUcepHotChargeId].AllowEditing = false;
            grfUcepHotcharge.Cols[colGrfUcepHotChargeName].AllowEditing = false;
            grfUcepHotcharge.Cols[colGrfUcepHotChargeUcepCode].AllowEditing = true;
            grfUcepHotcharge.Cols[colGrfUcepHotChargeDitCode].AllowEditing = true;
            grfUcepHotcharge.AllowFiltering = true;
            //for (int col = 0; col < dt.Columns.Count; ++col)
            //{
                grfUcepHotcharge.Cols[colGrfUcepHotChargeId].DataType = dt.Columns["MNC_SR_CD"].DataType;
                grfUcepHotcharge.Cols[colGrfUcepHotChargeId].Name = dt.Columns["MNC_SR_CD"].ColumnName;
                //grfOrdDrug.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
                grfUcepHotcharge.Cols[colGrfUcepHotChargeName].DataType = dt.Columns["MNC_SR_DSC"].DataType;
                grfUcepHotcharge.Cols[colGrfUcepHotChargeName].Name = dt.Columns["MNC_SR_DSC"].ColumnName;
                //grfUcepHotcharge.Cols[colGrfUcepHotChargeUcepCode].DataType = dt.Columns["ucep_code"].DataType;
                //grfUcepHotcharge.Cols[colGrfUcepHotChargeDitCode].Name = dt.Columns["dit_code"].ColumnName;
            //}
            pageLoad = false;
        }
        private void setGrfUcepCat()
        {
            pageLoad = true;
            DataTable dt = new DataTable();
            //C1ComboBox cboMethod = new C1ComboBox();
            //cboMethod.AutoCompleteMode = AutoCompleteMode.Suggest;
            //cboMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
            //bc.setCboOPBKKINSCL(cboMethod, "");

            dt = bc.bcDB.finM01DB.SelectAll();
            grfUcepCat.Rows.Count = 1;
            grfUcepCat.Cols.Count = 7;
            grfUcepCat.Rows.Count = dt.Rows.Count + 2;
            grfUcepCat.Cols[colGrfUcepPaidId].Caption = "ID";
            grfUcepCat.Cols[colGrfUcepPaidName].Caption = "Paid Name";
            grfUcepCat.Cols[colGrfUcepPaidCatCode].Caption = "CAT 1-16";
            grfUcepCat.Cols[colGrfUcepPaiducepcode].Caption = "ucep code";
            grfUcepCat.Cols[colGrfUcepPaidSSOP].Caption = "SSOP";

            grfUcepCat.Cols[colGrfUcepPaidId].Width = 80;
            grfUcepCat.Cols[colGrfUcepPaidName].Width = 500;
            grfUcepCat.Cols[colGrfUcepPaidCatCode].Width = 120;
            grfUcepCat.Cols[colGrfUcepPaidSSOP].Width = 80;

            //grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Editor = cboMethod;
            int i = 1;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                String inscl = "";
                grfUcepCat[i, colGrfUcepPaidId] = row1["MNC_FN_CD"].ToString();
                grfUcepCat[i, colGrfUcepPaidName] = row1["MNC_FN_DSCT"].ToString();
                grfUcepCat[i, colGrfUcepPaidCatCode] = row1["mnc_grp_ss1"].ToString();
                grfUcepCat[i, colGrfUcepPaiducepcode] = row1["ucep_code"].ToString();
                grfUcepCat[i, colGrfUcepPaidSSOP] = row1["ssop_billmuad"].ToString();
                grfUcepCat[i, colGrfUcepPaidFlag] = "0";
                grfUcepCat[i, 0] = (i-1);
                if (i % 2 == 0)
                    grfUcepCat.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);

            }
            grfUcepCat.Cols[colGrfUcepPaidFlag].Visible = false;

            grfUcepCat.Cols[colGrfUcepPaidId].AllowEditing = false;
            grfUcepCat.Cols[colGrfUcepPaidName].AllowEditing = false;
            grfUcepCat.Cols[colGrfUcepDrugOldCode].AllowEditing = false;
            grfUcepCat.Cols[colGrfUcepPaidSSOP].AllowEditing = true;
            grfUcepCat.AllowFiltering = true;
            //for (int col = 0; col < dt.Columns.Count; ++col)
            //{
            grfUcepCat.Cols[colGrfUcepPaidId].DataType = dt.Columns["MNC_FN_CD"].DataType;
            //grfOrdDrug.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
            grfUcepCat.Cols[colGrfUcepPaidId].Name = dt.Columns["MNC_FN_CD"].ColumnName;
            grfUcepCat.Cols[colGrfUcepPaidName].DataType = dt.Columns["MNC_FN_DSCT"].DataType;
            grfUcepCat.Cols[colGrfUcepPaidName].Name = dt.Columns["MNC_FN_DSCT"].ColumnName;
            grfUcepCat.Cols[colGrfUcepPaidSSOP].Name = dt.Columns["ssop_billmuad"].ColumnName;
            //}
            pageLoad = false;
        }
        private void setGrfUcepDrug(C1FlexGrid grf)
        {
            pageLoad = true;
            DataTable dt = new DataTable();
            //C1ComboBox cboMethod = new C1ComboBox();
            //cboMethod.AutoCompleteMode = AutoCompleteMode.Suggest;
            //cboMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
            //bc.setCboOPBKKINSCL(cboMethod, "");

            dt = bc.bcDB.pharM01DB.SelectAll();
            grf.Rows.Count = 1;
            grf.Cols.Count = 10;
            grf.Rows.Count = dt.Rows.Count + 2;
            grf.Cols[colGrfUcepDrugDrugId].Caption = "ID";
            grf.Cols[colGrfUcepDrugDrugName].Caption = "Drug Name";
            grf.Cols[colGrfUcepDrugTmtCode].Caption = "TMT code";
            grf.Cols[colGrfUcepDrugOldCode].Caption = "OLD Code";
            grf.Cols[colGrfUcepDrugPrice1].Caption = "Price1";
            grf.Cols[colGrfUcepDrugPrice2].Caption = "Price2";
            if (grf.Name.Equals("grfOPBKKMainTmtCode"))
            {
                grf.Cols[colGrfUcepDrugOpbkkCode].Caption = "OPBKK tmt code";
            }
            else if (grf.Name.Equals("grfCipnDrug"))
            {
                grf.Cols[colGrfUcepDrugOpbkkCode].Caption = "CIPN tmt code";
            }
            else
            {
                grf.Cols[colGrfUcepDrugOpbkkCode].Caption = "UCEP tmt code";
            }

            grf.Cols[0].Width = 50;
            grf.Cols[colGrfUcepDrugDrugId].Width = 90;
            grf.Cols[colGrfUcepDrugDrugName].Width = 500;
            grf.Cols[colGrfUcepDrugTmtCode].Width = 120;
            grf.Cols[colGrfUcepDrugOldCode].Width = 100;
            grf.Cols[colGrfUcepDrugPrice1].Width = 100;
            grf.Cols[colGrfUcepDrugPrice2].Width = 100;
            grf.Cols[colGrfUcepDrugOpbkkCode].Width = 100;

            //grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Editor = cboMethod;
            int i = 1;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                String inscl = "";
                grf[i, colGrfUcepDrugDrugId] = row1["MNC_ph_CD"].ToString();
                grf[i, colGrfUcepDrugDrugName] = row1["MNC_PH_TN"].ToString();
                if (grf.Name.Equals("grfOPBKKMainTmtCode"))
                {
                    grf[i, colGrfUcepDrugTmtCode] = row1["tmt_code_opbkk"].ToString(); ;
                }
                else if (grf.Name.Equals("grfCipnDrug"))
                {
                    grf[i, colGrfUcepDrugTmtCode] = row1["tmt_code_cipn"].ToString(); ;
                }
                else
                {
                    grf[i, colGrfUcepDrugTmtCode] = row1["tmt_code"].ToString(); ;
                }
                grf[i, colGrfUcepDrugOldCode] = row1["MNC_OLD_CD"].ToString();
                grf[i, colGrfUcepDrugPrice1] = row1["MNC_ph_pri01"].ToString();
                grf[i, colGrfUcepDrugPrice2] = row1["MNC_ph_pri02"].ToString();
                grf[i, colGrfUcepDrugOpbkkCode] = row1["opbkk_code"].ToString();
                grf[i, colGrfUcepDrugFlag] = "0";
                grf[i, colGrfUcepDrugStatusDrug] = row1["mnc_ph_typ_flg"].ToString();
                grf[i, 0] = (i-1);
                if (i % 2 == 0)
                    grf.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);

            }
            grf.Cols[colGrfUcepDrugDrugId].AllowEditing = false;
            grf.Cols[colGrfUcepDrugDrugName].AllowEditing = false;
            grf.Cols[colGrfUcepDrugOldCode].AllowEditing = false;
            grf.Cols[colGrfUcepDrugPrice1].AllowEditing = false;
            grf.Cols[colGrfUcepDrugPrice2].AllowEditing = false;
            grf.Cols[colGrfUcepDrugOpbkkCode].AllowEditing = true;
            grf.Cols[colGrfUcepDrugStatusDrug].AllowEditing = false;
            grf.AllowFiltering = true;

            grf.Cols[colGrfUcepDrugDrugId].DataType = dt.Columns["MNC_ph_CD"].DataType;
            grf.Cols[colGrfUcepDrugDrugId].Name = dt.Columns["MNC_ph_CD"].ColumnName;
            grf.Cols[colGrfUcepDrugDrugName].DataType = dt.Columns["MNC_PH_TN"].DataType;
            grf.Cols[colGrfUcepDrugDrugName].Name = dt.Columns["MNC_PH_TN"].ColumnName;
            grf.Cols[colGrfUcepDrugTmtCode].DataType = dt.Columns["tmt_code"].DataType;
            grf.Cols[colGrfUcepDrugTmtCode].Name = dt.Columns["tmt_code"].ColumnName;
            grf.Cols[colGrfUcepDrugOldCode].DataType = dt.Columns["MNC_OLD_CD"].DataType;
            grf.Cols[colGrfUcepDrugOldCode].Name = dt.Columns["MNC_OLD_CD"].ColumnName;

            grf.Cols[colGrfUcepDrugOpbkkCode].DataType = dt.Columns["opbkk_code"].DataType;
            grf.Cols[colGrfUcepDrugOpbkkCode].Name = dt.Columns["opbkk_code"].ColumnName;

            pageLoad = false;
        }
        private void setGrfPaidType()
        {
            pageLoad = true;
            DataTable dt = new DataTable();
            C1ComboBox cboOPBKK = new C1ComboBox();
            cboOPBKK.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboOPBKK.AutoCompleteSource = AutoCompleteSource.ListItems;
            bc.setCboOPBKKINSCL(cboOPBKK, "");

            C1ComboBox cboSSOP = new C1ComboBox();
            cboSSOP.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboSSOP.AutoCompleteSource = AutoCompleteSource.ListItems;
            bc.setCboSSOPClaimCat(cboSSOP, "");

            dt = bc.bcDB.finM02DB.SelectAll();
            grfPaidTyp.Rows.Count = 1;
            grfPaidTyp.Cols.Count = 9;
            grfPaidTyp.Rows.Count = dt.Rows.Count + 1;
            grfPaidTyp.Cols[colPaidTypId].Caption = "ID";
            grfPaidTyp.Cols[colPaidTypName].Caption = "Paid Name";
            grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Caption = "OPBKK Code";
            grfPaidTyp.Cols[colPaidTypFNSYS].Caption = "FN SYS";
            grfPaidTyp.Cols[colPaidTypPTTYP].Caption = "PT TYP";
            grfPaidTyp.Cols[colPaidTypAccNo].Caption = "ACC NO";
            grfPaidTyp.Cols[colPaidTypSSOPclaimCat].Caption = "SSOP ClaimCat";
            grfPaidTyp.Cols[colPaidTypId].Width = 60;
            grfPaidTyp.Cols[colPaidTypName].Width = 300;
            grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Width = 300;
            grfPaidTyp.Cols[colPaidTypFNSYS].Width = 80;
            grfPaidTyp.Cols[colPaidTypPTTYP].Width = 80;
            grfPaidTyp.Cols[colPaidTypAccNo].Width = 80;
            grfPaidTyp.Cols[colPaidTypSSOPclaimCat].Width = 150;
            grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Editor = cboOPBKK;
            grfPaidTyp.Cols[colPaidTypSSOPclaimCat].Editor = cboSSOP;
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                String inscl = "", ssopclaimcat="";
                bc.opBKKINSCL.TryGetValue(row1["opbkk_inscl"].ToString(), out inscl);
                bc.ssopClaimCat.TryGetValue(row1["ssop_claimcat"].ToString(), out ssopclaimcat);
                grfPaidTyp[i, colPaidTypId] = row1["MNC_FN_TYP_CD"].ToString();
                grfPaidTyp[i, colPaidTypName] = row1["MNC_FN_TYP_DSC"].ToString();
                grfPaidTyp[i, colPaidTypIdOpBkkCode] = inscl;
                grfPaidTyp[i, colPaidTypFNSYS] = row1["MNC_FN_STS"].ToString();
                grfPaidTyp[i, colPaidTypPTTYP] = row1["PTTYP"].ToString();
                grfPaidTyp[i, colPaidTypAccNo] = row1["MNC_ACCOUNT_NO"].ToString();
                grfPaidTyp[i, colPaidTypSSOPclaimCat] = ssopclaimcat;
                grfPaidTyp[i, colPaidTypflag] = "0";
                grfPaidTyp[i, 0] = i;
                if (i % 2 == 0)
                    grfPaidTyp.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                
            }
            grfPaidTyp.Cols[colPaidTypflag].Visible = false;
            grfPaidTyp.Cols[colPaidTypId].AllowEditing = false;
            grfPaidTyp.Cols[colPaidTypName].AllowEditing = false;
            grfPaidTyp.Cols[colPaidTypFNSYS].AllowEditing = false;
            grfPaidTyp.Cols[colPaidTypPTTYP].AllowEditing = false;
            grfPaidTyp.Cols[colPaidTypAccNo].AllowEditing = false;
            pageLoad = false;
        }
        private void setGrfClinic()
        {
            pageLoad = true;
            DataTable dt = new DataTable();
            C1ComboBox cboMethod = new C1ComboBox();
            cboMethod.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
            bc.setCboOPBKKClinic(cboMethod, "");

            dt = bc.bcDB.pttM32DB.SelectAll();
            grfClinic.Rows.Count = 1;
            grfClinic.Cols.Count = 9;
            grfClinic.Rows.Count = dt.Rows.Count + 1;
            grfClinic.Cols[colClinicmddepno].Caption = "DEP NO";
            grfClinic.Cols[colClinicsecno].Caption = "SEC NO";
            grfClinic.Cols[colClinicdivno].Caption = "DIV NO";
            grfClinic.Cols[colClinictyppt].Caption = "TYP PT";
            grfClinic.Cols[colClinicdepdsc].Caption = "DEP DSC";
            grfClinic.Cols[colClinicdpno].Caption = "DP NO";
            grfClinic.Cols[colClinicopbkkcode].Caption = "OP BKK Clinic";
            grfClinic.Cols[colClinicopbkkSSOP].Caption = "SSOP Clinic";
            grfClinic.Cols[colClinicmddepno].Width = 60;
            grfClinic.Cols[colClinicsecno].Width = 60;
            grfClinic.Cols[colClinicdivno].Width = 60;
            grfClinic.Cols[colClinictyppt].Width = 60;
            grfClinic.Cols[colClinicdepdsc].Width = 300;
            grfClinic.Cols[colClinicdpno].Width = 80;
            grfClinic.Cols[colClinicopbkkcode].Width = 300;
            grfClinic.Cols[colClinicopbkkcode].Editor = cboMethod;
            grfClinic.Cols[colClinicopbkkSSOP].Width = 150;
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                String inscl = "";
                bc.opBKKClinic.TryGetValue(row1["opbkk_clinic"].ToString(), out inscl);
                grfClinic[i, colClinicmddepno] = row1["mnc_md_dep_no"].ToString();
                grfClinic[i, colClinicsecno] = row1["mnc_sec_no"].ToString();
                grfClinic[i, colClinicdivno] = row1["MNC_DIV_NO"].ToString();
                grfClinic[i, colClinictyppt] = row1["MNC_TYP_PT"].ToString();
                grfClinic[i, colClinicdepdsc] = row1["mnc_md_dep_dsc"].ToString();
                grfClinic[i, colClinicdpno] = row1["MNC_DP_NO"].ToString();
                grfClinic[i, colClinicopbkkcode] = inscl;
                grfClinic[i, colClinicopbkkFlag] = "0";
                grfClinic[i, colClinicopbkkSSOP] = row1["ssop_clinic"].ToString();
                grfClinic[i, 0] = i;
                if (i % 2 == 0)
                    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            grfClinic.Cols[colClinicopbkkFlag].Visible = false;
            grfClinic.Cols[colClinicmddepno].AllowEditing = false;
            grfClinic.Cols[colClinicsecno].AllowEditing = false;
            grfClinic.Cols[colClinicdivno].AllowEditing = false;
            grfClinic.Cols[colClinictyppt].AllowEditing = false;
            grfClinic.Cols[colClinicdepdsc].AllowEditing = false;
            grfClinic.Cols[colClinicdpno].AllowEditing = false;
            grfClinic.Cols[colClinicopbkkSSOP].AllowEditing = true;
            pageLoad = false;
        }
        private void setGrfOPBKKMainCHRGITEM()
        {
            pageLoad = true;
            DataTable dt = new DataTable();
            C1ComboBox cboMethod = new C1ComboBox();
            cboMethod.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
            bc.setCboOPBKKCHRGITEM_CODEA(cboMethod, "");

            dt = bc.bcDB.finM01DB.SelectAllOPBKK();
            grfOPBKKMainCHRGITEM.Rows.Count = 1;
            grfOPBKKMainCHRGITEM.Cols.Count = 10;
            grfOPBKKMainCHRGITEM.Rows.Count = dt.Rows.Count + 1;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMFNcode].Caption = "FN CODE";
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMFNname].Caption = "FN Name";
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMFNGRPcode].Caption = "GRP ";
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMFNDEFcode].Caption = "DEF ";
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMcode].Caption = "CHRGITEM";
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMsimbcode].Caption = "SIMB code";
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMchrcode].Caption = "charge code";
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMchrsubcode].Caption = "sub charge code";

            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMFNcode].Width = 60;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMFNname].Width = 250;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMFNGRPcode].Width = 250;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMFNDEFcode].Width = 250;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMcode].Width = 250;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMsimbcode].Width = 90;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMchrcode].Width = 110;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMchrsubcode].Width = 110;

            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMcode].Editor = cboMethod;
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                String inscl = "";
                bc.opBKKClinic.TryGetValue(row1["chrgitem_code"].ToString(), out inscl);
                grfOPBKKMainCHRGITEM[i, colCHRGITEMFNcode] = row1["mnc_fn_cd"].ToString();
                grfOPBKKMainCHRGITEM[i, colCHRGITEMFNname] = row1["mnc_fn_dsct"].ToString();
                grfOPBKKMainCHRGITEM[i, colCHRGITEMFNGRPcode] = row1["MNC_FN_GRP_DSC"].ToString();
                grfOPBKKMainCHRGITEM[i, colCHRGITEMFNDEFcode] = row1["MNC_DEF_DSC"].ToString();
                grfOPBKKMainCHRGITEM[i, colCHRGITEMsimbcode] = row1["mnc_simb_cd"].ToString();
                grfOPBKKMainCHRGITEM[i, colCHRGITEMchrcode] = row1["mnc_charge_cd"].ToString();
                grfOPBKKMainCHRGITEM[i, colCHRGITEMchrsubcode] = row1["mnc_sub_charge_cd"].ToString();

                grfOPBKKMainCHRGITEM[i, colCHRGITEMcode] = inscl;
                grfOPBKKMainCHRGITEM[i, colClinicCHRGITEMFlag] = "0";
                grfOPBKKMainCHRGITEM[i, 0] = i;
                if (i % 2 == 0)
                    grfOPBKKMainCHRGITEM.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            grfOPBKKMainCHRGITEM.Cols[colClinicCHRGITEMFlag].Visible = false;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMFNcode].AllowEditing = false;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMFNname].AllowEditing = false;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMFNGRPcode].AllowEditing = false;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMFNDEFcode].AllowEditing = false;
            //grfOPBKKMainCHRGITEM.Cols[colCHRGITEMcode].AllowEditing = false;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMsimbcode].AllowEditing = false;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMchrcode].AllowEditing = false;
            grfOPBKKMainCHRGITEM.Cols[colCHRGITEMchrsubcode].AllowEditing = false;
            pageLoad = false;
        }
        private void setGrfINS()
        {
            pageLoad = true;
            grfINS.DataSource = null;
            grfINS.Rows.Count = 1;
            grfINS.Cols.Count = 15;
            grfINS.Rows.Count = lINS.Count+1;
            grfINS.Cols[colINShn].Caption = "HN";
            grfINS.Cols[colINSinscl].Caption = "INSCL";
            grfINS.Cols[colINSsubtype].Caption = "sub type";
            grfINS.Cols[colINScid].Caption = "CID";
            grfINS.Cols[colINSdatein].Caption = "date in";
            grfINS.Cols[colINSdateexp].Caption = "date exp";
            grfINS.Cols[colINShospmain].Caption = "hosp main";
            grfINS.Cols[colINShospsub].Caption = "hosp sub";
            grfINS.Cols[colINSgovcode].Caption = "gov code";
            grfINS.Cols[colINSgovname].Caption = "gov name";
            grfINS.Cols[colINSpermitno].Caption = "permit no";
            grfINS.Cols[colINSdocno].Caption = "doc no";
            grfINS.Cols[colINSownrpid].Caption = "ownrpid";
            grfINS.Cols[colINSownname].Caption = "ownname";

            grfINS.Cols[colINShn].Width = 60;
            grfINS.Cols[colINSinscl].Width = 60;
            grfINS.Cols[colINSsubtype].Width = 60;
            grfINS.Cols[colINScid].Width = 60;
            grfINS.Cols[colINSdatein].Width = 300;
            grfINS.Cols[colINSdateexp].Width = 80;
            grfINS.Cols[colINShospmain].Width = 300;
            grfINS.Cols[colINShospsub].Width = 300;
            grfINS.Cols[colINSgovcode].Width = 300;
            grfINS.Cols[colINSgovname].Width = 300;
            grfINS.Cols[colINSpermitno].Width = 300;
            grfINS.Cols[colINSdocno].Width = 300;
            grfINS.Cols[colINSownrpid].Width = 300;
            grfINS.Cols[colINSownname].Width = 300;

            int i = 0;
            foreach (OPBKKINS ins in lINS)
            {
                i++;
                //if (i == 1) continue;
                grfINS[i, colINShn] = ins.HN;
                grfINS[i, colINSinscl] = ins.INSCL;
                grfINS[i, colINSsubtype] = ins.SUBTYPE;
                grfINS[i, colINScid] = ins.CID;
                grfINS[i, colINSdatein] = ins.DATEIN;
                grfINS[i, colINSdateexp] = ins.DATEEXP;
                grfINS[i, colINShospmain] = ins.HOSPMAIN;
                grfINS[i, colINShospsub] = ins.HOSPSUB;
                grfINS[i, colINSgovcode] = ins.GOVCODE;
                grfINS[i, colINSgovname] = ins.GOVNAME;
                grfINS[i, colINSpermitno] = ins.PERMITNO;
                grfINS[i, colINSdocno] = ins.DOCNO;
                grfINS[i, colINSownrpid] = ins.OWNRPID;
                grfINS[i, colINSownname] = ins.OWNNAME;
                grfINS[i, colINSflag] = "0";
                grfINS[i, 0] = i;
                //if (i % 2 == 0)
                //    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            pageLoad = false;
        }
        private void setGrfPAT()
        {
            pageLoad = true;
            grfPAT.DataSource = null;
            grfPAT.Rows.Count = 1;
            grfPAT.Cols.Count = 17;
            grfPAT.Rows.Count = lPAT.Count + 1;
            grfPAT.Cols[colPAThcode].Caption = "HCODE";
            grfPAT.Cols[colPAThn].Caption = "HN";
            grfPAT.Cols[colPATchangwat].Caption = "changwat";
            grfPAT.Cols[colPATamphur].Caption = "amphur";
            grfPAT.Cols[colPATdob].Caption = "DOB";
            grfPAT.Cols[colPATsex].Caption = "SEX";
            grfPAT.Cols[colPATmarriage].Caption = "marriage";
            grfPAT.Cols[colPAToccupa].Caption = "occupa";
            grfPAT.Cols[colPATnation].Caption = "nation";
            grfPAT.Cols[colPATpersonid].Caption = "personid";
            grfPAT.Cols[colPATnamepat].Caption = "namepat";
            grfPAT.Cols[colPATtitle].Caption = "title";
            grfPAT.Cols[colPATfname].Caption = "fname";
            grfPAT.Cols[colPATlname].Caption = "lname";
            grfPAT.Cols[colPATidtype].Caption = "idtype";

            grfPAT.Cols[colPAThcode].Width = 60;
            grfPAT.Cols[colPAThn].Width = 60;
            grfPAT.Cols[colPATchangwat].Width = 60;
            grfPAT.Cols[colPATamphur].Width = 60;
            grfPAT.Cols[colPATdob].Width = 300;
            grfPAT.Cols[colPATsex].Width = 80;
            grfPAT.Cols[colPATmarriage].Width = 300;
            grfPAT.Cols[colPAToccupa].Width = 300;
            grfPAT.Cols[colPATnation].Width = 300;
            grfPAT.Cols[colPATpersonid].Width = 300;
            grfPAT.Cols[colPATnamepat].Width = 300;
            grfPAT.Cols[colPATtitle].Width = 300;
            grfPAT.Cols[colPATfname].Width = 300;
            grfPAT.Cols[colPATlname].Width = 300;
            grfPAT.Cols[colPATidtype].Width = 300;

            int i = 0;
            foreach (OPBKKPAT pat in lPAT)
            {
                i++;
                //if (i == 1) continue;
                grfPAT[i, colPAThcode] = pat.HCODE;
                grfPAT[i, colPAThn] = pat.HN;
                grfPAT[i, colPATchangwat] = pat.CHANGWAT;
                grfPAT[i, colPATamphur] = pat.AMPHUR;
                grfPAT[i, colPATdob] = pat.DOB;
                grfPAT[i, colPATsex] = pat.SEX;
                grfPAT[i, colPATmarriage] = pat.MARRIAGE;
                grfPAT[i, colPAToccupa] = pat.OCCUPA;
                grfPAT[i, colPATnation] = pat.NATION;
                grfPAT[i, colPATpersonid] = pat.PERSON_ID;
                grfPAT[i, colPATnamepat] = pat.NAMEPAT;
                grfPAT[i, colPATtitle] = pat.TITLE;
                grfPAT[i, colPATfname] = pat.FNAME;
                grfPAT[i, colPATlname] = pat.LNAME;
                grfPAT[i, colPATidtype] = pat.IDTYPE;
                grfPAT[i, colPATflag] = "0";
                grfPAT[i, 0] = i;
                //if (i % 2 == 0)
                //    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            pageLoad = false;
        }
        private void setGrfOPD()
        {
            pageLoad = true;
            grfOPD.DataSource = null;
            grfOPD.Rows.Count = 1;
            grfOPD.Cols.Count = 18;
            grfOPD.Rows.Count = lOPD.Count + 1;
            grfOPD.Cols[colOPDpersonid].Caption = "ID";
            grfOPD.Cols[colOPDhn].Caption = "HN";
            grfOPD.Cols[colOPDdateopd].Caption = "date";
            grfOPD.Cols[colOPDtimeopd].Caption = "time";
            grfOPD.Cols[colOPDseq].Caption = "seq";
            grfOPD.Cols[colOPDuuc].Caption = "UUC";
            grfOPD.Cols[colOPDdetail].Caption = "detail";
            grfOPD.Cols[colOPDbtemp].Caption = "temp";
            grfOPD.Cols[colOPDsbp].Caption = "SBP";
            grfOPD.Cols[colOPDdbp].Caption = "DBP";
            grfOPD.Cols[colOPDpr].Caption = "PR";
            grfOPD.Cols[colOPDrr].Caption = "RR";
            grfOPD.Cols[colOPDoptype].Caption = "OPTYPE";
            grfOPD.Cols[colOPDtypein].Caption = "TYPEIN";
            grfOPD.Cols[colOPDtypeout].Caption = "TYPEOUT";
            grfOPD.Cols[colOPDclaimcode].Caption = "claimcode";

            grfOPD.Cols[colOPDpersonid].Width = 60;
            grfOPD.Cols[colOPDhn].Width = 60;
            grfOPD.Cols[colOPDdateopd].Width = 60;
            grfOPD.Cols[colOPDtimeopd].Width = 60;
            grfOPD.Cols[colOPDseq].Width = 300;
            grfOPD.Cols[colOPDuuc].Width = 80;
            grfOPD.Cols[colOPDdetail].Width = 300;
            grfOPD.Cols[colOPDbtemp].Width = 300;
            grfOPD.Cols[colOPDsbp].Width = 300;
            grfOPD.Cols[colOPDdbp].Width = 300;
            grfOPD.Cols[colOPDpr].Width = 300;
            grfOPD.Cols[colOPDrr].Width = 300;
            grfOPD.Cols[colOPDoptype].Width = 300;
            grfOPD.Cols[colOPDtypein].Width = 300;
            grfOPD.Cols[colOPDtypeout].Width = 300;
            grfOPD.Cols[colOPDclaimcode].Width = 300;

            int i = 0;
            foreach (OPBKKOPD ins in lOPD)
            {
                i++;
                //if (i == 1) continue;
                grfOPD[i, colOPDpersonid] = ins.PERSON_ID;
                grfOPD[i, colOPDhn] = ins.HN;
                grfOPD[i, colOPDdateopd] = ins.DATEOPD;
                grfOPD[i, colOPDtimeopd] = ins.TIMEOPD;
                grfOPD[i, colOPDseq] = ins.SEQ;
                grfOPD[i, colOPDuuc] = ins.UUC;
                grfOPD[i, colOPDdetail] = ins.DETAIL;
                grfOPD[i, colOPDbtemp] = ins.BTEMP;
                grfOPD[i, colOPDsbp] = ins.SBP;
                grfOPD[i, colOPDdbp] = ins.DBP;
                grfOPD[i, colOPDpr] = ins.PR;
                grfOPD[i, colOPDrr] = ins.RR;
                grfOPD[i, colOPDoptype] = ins.OPTYPE;
                grfOPD[i, colOPDtypein] = ins.TYPEIN;
                grfOPD[i, colOPDtypeout] = ins.TYPEOUT;
                grfOPD[i, colOPDclaimcode] = ins.CLAIM_CODE;
                grfOPD[i, colOPDflag] = "0";
                grfOPD[i, 0] = i;
                //if (i % 2 == 0)
                //    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            pageLoad = false;
        }
        private void setGrfORF()
        {
            pageLoad = true;
            grfORF.DataSource = null;
            grfORF.Rows.Count = 1;
            grfORF.Cols.Count = 9;
            grfORF.Rows.Count = lORF.Count + 1;
            grfORF.Cols[colORFhn].Caption = "ID";
            grfORF.Cols[colORFdateopd].Caption = "HN";
            grfORF.Cols[colORFclinic].Caption = "date";
            grfORF.Cols[colORFrefer].Caption = "time";
            grfORF.Cols[colORFrefertype].Caption = "seq";
            grfORF.Cols[colORFreferdate].Caption = "UUC";
            grfORF.Cols[colORFseq].Caption = "detail";

            grfORF.Cols[colORFhn].Width = 60;
            grfORF.Cols[colORFdateopd].Width = 60;
            grfORF.Cols[colORFclinic].Width = 60;
            grfORF.Cols[colORFrefer].Width = 60;
            grfORF.Cols[colORFrefertype].Width = 300;
            grfORF.Cols[colORFreferdate].Width = 80;
            grfORF.Cols[colORFseq].Width = 300;

            int i = 0;
            foreach (OPBKKORF ins in lORF)
            {
                i++;
                //if (i == 1) continue;
                grfORF[i, colORFhn] = ins.HN;
                grfORF[i, colORFdateopd] = ins.DATEOPD;
                grfORF[i, colORFclinic] = ins.CLINIC;
                grfORF[i, colORFrefer] = ins.REFER;
                grfORF[i, colORFrefertype] = ins.REFERTYPE;
                grfORF[i, colORFreferdate] = ins.REFERDATE;
                grfORF[i, colORFseq] = ins.SEQ;
                
                grfORF[i, colORFflag] = "0";
                grfORF[i, 0] = i;
                //if (i % 2 == 0)
                //    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            pageLoad = false;
        }
        private void setGrfODX()
        {
            pageLoad = true;
            grfODX.DataSource = null;
            grfODX.Rows.Count = 1;
            grfODX.Cols.Count = 10;
            grfODX.Rows.Count = lODX.Count + 1;
            grfODX.Cols[colODXhn].Caption = "HN";
            grfODX.Cols[colODXdatedx].Caption = "date";
            grfODX.Cols[colODXclinic].Caption = "clinic";
            grfODX.Cols[colODXdiag].Caption = "diag";
            grfODX.Cols[colODXdxtype].Caption = "dxtype";
            grfODX.Cols[colODXdrdx].Caption = "drdx";
            grfODX.Cols[colODXpersonid].Caption = "PID";
            grfODX.Cols[colODXseq].Caption = "SEQ";

            grfODX.Cols[colODXhn].Width = 60;
            grfODX.Cols[colODXdatedx].Width = 60;
            grfODX.Cols[colODXclinic].Width = 60;
            grfODX.Cols[colODXdiag].Width = 60;
            grfODX.Cols[colODXdxtype].Width = 300;
            grfODX.Cols[colODXdrdx].Width = 80;
            grfODX.Cols[colODXpersonid].Width = 300;
            grfODX.Cols[colODXseq].Width = 300;

            int i = 0;
            foreach (OPBKKODX ins in lODX)
            {
                i++;
                //if (i == 1) continue;
                grfODX[i, colODXhn] = ins.HN;
                grfODX[i, colODXdatedx] = ins.DATEDX;
                grfODX[i, colODXclinic] = ins.CLINIC;
                grfODX[i, colODXdiag] = ins.DIAG;
                grfODX[i, colODXdxtype] = ins.DXTYPE;
                grfODX[i, colODXdrdx] = ins.DRDX;
                grfODX[i, colODXpersonid] = ins.PERSON_ID;
                grfODX[i, colODXseq] = ins.SEQ;

                grfODX[i, colODXflag] = "0";
                grfODX[i, 0] = i;
                //if (i % 2 == 0)
                //    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            pageLoad = false;
        }
        private void setGrfOOP()
        {
            pageLoad = true;
            grfOOP.DataSource = null;
            grfOOP.Rows.Count = 1;
            grfOOP.Cols.Count = 10;
            grfOOP.Rows.Count = lOOP.Count + 1;
            grfOOP.Cols[colOOPhn].Caption = "HN";
            grfOOP.Cols[colOOPdateopd].Caption = "date";
            grfOOP.Cols[colOOPclinic].Caption = "clinic";
            grfOOP.Cols[colOOPoper].Caption = "oper";
            grfOOP.Cols[colOOPservprice].Caption = "servprice";
            grfOOP.Cols[colOOPdropid].Caption = "dropid";
            grfOOP.Cols[colOOPpersonid].Caption = "pid";
            grfOOP.Cols[colOOPseq].Caption = "SEQ";

            grfOOP.Cols[colOOPhn].Width = 60;
            grfOOP.Cols[colOOPdateopd].Width = 60;
            grfOOP.Cols[colOOPclinic].Width = 60;
            grfOOP.Cols[colOOPoper].Width = 60;
            grfOOP.Cols[colOOPservprice].Width = 300;
            grfOOP.Cols[colOOPdropid].Width = 80;
            grfOOP.Cols[colOOPpersonid].Width = 300;
            grfOOP.Cols[colOOPseq].Width = 300;

            int i = 0;
            foreach (OPBKKOOP ins in lOOP)
            {
                i++;
                //if (i == 1) continue;
                grfOOP[i, colOOPhn] = ins.HN;
                grfOOP[i, colOOPdateopd] = ins.DATEOPD;
                grfOOP[i, colOOPclinic] = ins.CLINIC;
                grfOOP[i, colOOPoper] = ins.OPER;
                grfOOP[i, colOOPservprice] = ins.SERVPRICE;
                grfOOP[i, colOOPdropid] = ins.DROPID;
                grfOOP[i, colOOPpersonid] = ins.PERSON_ID;
                grfOOP[i, colOOPseq] = ins.SEQ;

                grfOOP[i, colOOPflag] = "0";
                grfOOP[i, 0] = i;
                //if (i % 2 == 0)
                //    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            pageLoad = false;
        }
        private void setGrfCHT()
        {
            pageLoad = true;
            grfCHT.DataSource = null;
            grfCHT.Rows.Count = 1;
            grfCHT.Cols.Count = 13;
            grfCHT.Rows.Count = lCHT.Count + 1;
            grfCHT.Cols[colCHThn].Caption = "HN";
            grfCHT.Cols[colCHTdateopd].Caption = "date";
            grfCHT.Cols[colCHTactualcht].Caption = "actualcht";
            grfCHT.Cols[colCHTtotal].Caption = "total";
            grfCHT.Cols[colCHTpaid].Caption = "paid";
            grfCHT.Cols[colCHTpttype].Caption = "pttype";
            grfCHT.Cols[colCHTpersonid].Caption = "pid";
            grfCHT.Cols[colCHTseq].Caption = "SEQ";
            grfCHT.Cols[colCHTopd_memo].Caption = "MEMO";
            grfCHT.Cols[colCHTinvoiceno].Caption = "invoiceno";
            grfCHT.Cols[colCHTinvoicelt].Caption = "invoicelt";

            grfCHT.Cols[colCHThn].Width = 60;
            grfCHT.Cols[colCHTdateopd].Width = 60;
            grfCHT.Cols[colCHTactualcht].Width = 60;
            grfCHT.Cols[colCHTtotal].Width = 60;
            grfCHT.Cols[colCHTpaid].Width = 300;
            grfCHT.Cols[colCHTpttype].Width = 80;
            grfCHT.Cols[colOOPpersonid].Width = 300;
            grfCHT.Cols[colCHTpersonid].Width = 300;
            grfCHT.Cols[colCHTseq].Width = 300;
            grfCHT.Cols[colCHTopd_memo].Width = 300;
            grfCHT.Cols[colCHTinvoiceno].Width = 300;
            grfCHT.Cols[colCHTinvoicelt].Width = 300;

            int i = 0;
            foreach (OPBKKCHT ins in lCHT)
            {
                i++;
                //if (i == 1) continue;
                grfCHT[i, colCHThn] = ins.HN;
                grfCHT[i, colCHTdateopd] = ins.DATEOPD;
                grfCHT[i, colCHTactualcht] = ins.ACTUALCHT;
                grfCHT[i, colCHTtotal] = ins.TOTAL;
                grfCHT[i, colCHTpaid] = ins.PAID;
                grfCHT[i, colCHTpttype] = ins.PTTYPE;
                grfCHT[i, colCHTpersonid] = ins.PERSON_ID;
                grfCHT[i, colCHTseq] = ins.SEQ;
                grfCHT[i, colCHTopd_memo] = ins.OPD_MEMO;
                grfCHT[i, colCHTinvoiceno] = ins.INVOICE_NO;
                grfCHT[i, colCHTinvoicelt] = ins.INVOICE_LT;

                grfCHT[i, colCHTflag] = "0";
                grfCHT[i, 0] = i;
                //if (i % 2 == 0)
                //    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            pageLoad = false;
        }
        private void setGrfCHA()
        {
            pageLoad = true;
            grfCHA.DataSource = null;
            grfCHA.Rows.Count = 1;
            grfCHA.Cols.Count = 9;
            grfCHA.Rows.Count = lCHA.Count + 1;
            grfCHA.Cols[colCHAhn].Caption = "HN";
            grfCHA.Cols[colCHAdateopd].Caption = "date";
            grfCHA.Cols[colCHAchrgitem].Caption = "chrgitem";
            grfCHA.Cols[colCHAamount].Caption = "amount";
            grfCHA.Cols[colCHAamountext].Caption = "amountext";
            grfCHA.Cols[colCHApersonid].Caption = "PID";
            grfCHA.Cols[colCHAseq].Caption = "SEQ";

            grfCHA.Cols[colCHAhn].Width = 60;
            grfCHA.Cols[colCHAdateopd].Width = 60;
            grfCHA.Cols[colCHAchrgitem].Width = 60;
            grfCHA.Cols[colCHAamount].Width = 60;
            grfCHA.Cols[colCHAamountext].Width = 300;
            grfCHA.Cols[colCHApersonid].Width = 80;
            grfCHA.Cols[colCHAseq].Width = 300;

            int i = 0;
            foreach (OPBKKCHA ins in lCHA)
            {
                i++;
                //if (i == 1) continue;
                grfCHA[i, colCHAhn] = ins.HN;
                grfCHA[i, colCHAdateopd] = ins.DATEOPD;
                grfCHA[i, colCHAchrgitem] = ins.CHRGITEM;
                grfCHA[i, colCHAamount] = ins.AMOUNT;
                grfCHA[i, colCHAamountext] = ins.AMOUNT_EXT;
                grfCHA[i, colCHApersonid] = ins.PERSON_ID;
                grfCHA[i, colCHAseq] = ins.SEQ;

                grfCHA[i, colCHAflag] = "0";
                grfCHA[i, 0] = i;
                //if (i % 2 == 0)
                //    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            pageLoad = false;
        }
        private void setGrfAER()
        {
            pageLoad = true;
            grfAER.DataSource = null;
            grfAER.Rows.Count = 1;
            grfAER.Cols.Count = 16;
            grfAER.Rows.Count = lAER.Count + 1;
            grfAER.Cols[colAERhn].Caption = "HN";
            grfAER.Cols[colAERdateopd].Caption = "date";
            grfAER.Cols[colAERauthae].Caption = "authae";
            grfAER.Cols[colAERaedate].Caption = "aedate";
            grfAER.Cols[colAERaetime].Caption = "aetime";
            grfAER.Cols[colAERaetype].Caption = "aetype";
            grfAER.Cols[colAERreferno].Caption = "referno";
            grfAER.Cols[colAERrefmaini].Caption = "refmaini";
            grfAER.Cols[colAERireftype].Caption = "ireftype";
            grfAER.Cols[colAERrefmainno].Caption = "refmainno";
            grfAER.Cols[colAERoreftype].Caption = "reftype";
            grfAER.Cols[colAERucae].Caption = "ucase";
            grfAER.Cols[colAERemtype].Caption = "emtype";
            grfAER.Cols[colAERseq].Caption = "SEQ";

            grfAER.Cols[colAERhn].Width = 60;
            grfAER.Cols[colAERdateopd].Width = 60;
            grfAER.Cols[colAERauthae].Width = 60;
            grfAER.Cols[colAERaedate].Width = 60;
            grfAER.Cols[colAERaetime].Width = 300;
            grfAER.Cols[colAERaetype].Width = 80;
            grfAER.Cols[colAERreferno].Width = 300;
            grfAER.Cols[colAERrefmaini].Width = 300;
            grfAER.Cols[colAERireftype].Width = 300;
            grfAER.Cols[colAERrefmainno].Width = 300;
            grfAER.Cols[colAERoreftype].Width = 300;
            grfAER.Cols[colAERucae].Width = 300;
            grfAER.Cols[colAERemtype].Width = 300;
            grfAER.Cols[colAERseq].Width = 300;

            int i = 0;
            foreach (OPBKKAER ins in lAER)
            {
                i++;
                //if (i == 1) continue;
                grfAER[i, colAERhn] = ins.HN;
                grfAER[i, colAERdateopd] = ins.DATEOPD;
                grfAER[i, colAERauthae] = ins.AUTHAE;
                grfAER[i, colAERaedate] = ins.AEDATE;
                grfAER[i, colAERaetime] = ins.AETIME;
                grfAER[i, colAERaetype] = ins.AETYPE;
                grfAER[i, colAERreferno] = ins.REFER_NO;
                grfAER[i, colAERrefmaini] = ins.REFMAINI;
                grfAER[i, colAERireftype] = ins.IREFTYPE;
                grfAER[i, colAERrefmainno] = ins.REFMAINO;
                grfAER[i, colAERoreftype] = ins.OREFTYPE;
                grfAER[i, colAERucae] = ins.UCAE;
                grfAER[i, colAERemtype] = ins.EMTYPE;
                grfAER[i, colAERseq] = ins.SEQ;
                
                grfAER[i, colAERflag] = "0";
                grfAER[i, 0] = i;
                //if (i % 2 == 0)
                //    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            pageLoad = false;
        }
        private void setGrfDRU()
        {
            pageLoad = true;
            grfDRU.DataSource = null;
            grfDRU.Rows.Count = 1;
            grfDRU.Cols.Count = 19;
            grfDRU.Rows.Count = lDRU.Count + 1;
            grfDRU.Cols[colDRUhcode].Caption = "hcode";
            grfDRU.Cols[colDRUhn].Caption = "HN";
            grfDRU.Cols[colDRUclinic].Caption = "clinic";
            grfDRU.Cols[colDRUpersonid].Caption = "pid";
            grfDRU.Cols[colDRUdateserv].Caption = "date";
            grfDRU.Cols[colDRUdid].Caption = "did";
            grfDRU.Cols[colDRUdidname].Caption = "didname";
            grfDRU.Cols[colDRUdidstd].Caption = "didstd";
            grfDRU.Cols[colDRUtmtcode].Caption = "tmtcode";
            grfDRU.Cols[colDRUamount].Caption = "amount";
            grfDRU.Cols[colDRUdrugprice].Caption = "price";
            grfDRU.Cols[colDRUpriceext].Caption = "price ext";
            grfDRU.Cols[colDRUdrugcost].Caption = "cost";
            grfDRU.Cols[colDRUunit].Caption = "unit";
            grfDRU.Cols[colDRUunitpack].Caption = "pack";
            grfDRU.Cols[colDRUseq].Caption = "SEQ";
            grfDRU.Cols[colDRUprovider].Caption = "provider";

            grfDRU.Cols[colDRUhcode].Width = 60;
            grfDRU.Cols[colDRUhn].Width = 80;
            grfDRU.Cols[colDRUclinic].Width = 60;
            grfDRU.Cols[colDRUpersonid].Width = 130;
            grfDRU.Cols[colDRUdateserv].Width = 100;
            grfDRU.Cols[colDRUdid].Width = 80;
            grfDRU.Cols[colDRUdidname].Width = 300;
            grfDRU.Cols[colDRUdidstd].Width = 100;
            grfDRU.Cols[colDRUtmtcode].Width = 100;
            grfDRU.Cols[colDRUamount].Width = 100;
            grfDRU.Cols[colDRUdrugprice].Width = 100;
            grfDRU.Cols[colDRUpriceext].Width = 100;
            grfDRU.Cols[colDRUdrugcost].Width = 100;
            grfDRU.Cols[colDRUunit].Width = 100;
            grfDRU.Cols[colDRUunitpack].Width = 100;
            grfDRU.Cols[colDRUseq].Width = 80;
            grfDRU.Cols[colDRUprovider].Width = 100;

            int i = 0;
            foreach (OPBKKDRU ins in lDRU)
            {
                i++;
                //if (i == 1) continue;
                grfDRU[i, colDRUhcode] = ins.HCODE;
                grfDRU[i, colDRUhn] = ins.HN;
                grfDRU[i, colDRUclinic] = ins.CLINIC;
                grfDRU[i, colDRUpersonid] = ins.PERSON_ID;
                grfDRU[i, colDRUdateserv] = ins.DATESERV;
                grfDRU[i, colDRUdid] = ins.DID;
                grfDRU[i, colDRUdidname] = ins.DIDNAME;
                grfDRU[i, colDRUdidstd] = ins.DIDSTD;
                grfDRU[i, colDRUtmtcode] = ins.TMTCODE;
                grfDRU[i, colDRUamount] = ins.AMOUNT;
                grfDRU[i, colDRUdrugprice] = ins.DRUGPRICE;
                grfDRU[i, colDRUpriceext] = ins.PRICE_EXT;
                grfDRU[i, colDRUdrugcost] = ins.DRUGCOST;
                grfDRU[i, colDRUunit] = ins.UNIT;
                grfDRU[i, colDRUunitpack] = ins.UNIT_PACK;
                grfDRU[i, colDRUseq] = ins.SEQ;
                grfDRU[i, colDRUprovider] = ins.PROVIDER;

                grfDRU[i, colDRUflag] = "0";
                grfDRU[i, 0] = i;
                //if (i % 2 == 0)
                //    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            pageLoad = false;
        }
        private void setGrfLABFU()
        {
            pageLoad = true;
            grfLABFU.DataSource = null;
            grfLABFU.Rows.Count = 1;
            grfLABFU.Cols.Count = 9;
            grfLABFU.Rows.Count = lLABFU.Count + 1;
            grfLABFU.Cols[colLABFUhcode].Caption = "hcode";
            grfLABFU.Cols[colLABFUhn].Caption = "HN";
            grfLABFU.Cols[colLABFUpersonid].Caption = "PID";
            grfLABFU.Cols[colLABFUdateserv].Caption = "date";
            grfLABFU.Cols[colLABFUseq].Caption = "SEQ";
            grfLABFU.Cols[colLABFUlabtest].Caption = "labtest";
            grfLABFU.Cols[colLABFUlabresult].Caption = "result";

            grfLABFU.Cols[colLABFUhcode].Width = 60;
            grfLABFU.Cols[colLABFUhn].Width = 80;
            grfLABFU.Cols[colLABFUpersonid].Width = 120;
            grfLABFU.Cols[colLABFUdateserv].Width = 100;
            grfLABFU.Cols[colLABFUseq].Width = 60;
            grfLABFU.Cols[colLABFUlabtest].Width = 80;
            grfLABFU.Cols[colLABFUlabresult].Width = 300;

            int i = 0;
            foreach (OPBKKLABFU ins in lLABFU)
            {
                i++;
                //if (i == 1) continue;
                grfLABFU[i, colLABFUhcode] = ins.HCODE;
                grfLABFU[i, colLABFUhn] = ins.HN;
                grfLABFU[i, colLABFUpersonid] = ins.PERSON_ID;
                grfLABFU[i, colLABFUdateserv] = ins.DATESERV;
                grfLABFU[i, colLABFUseq] = ins.SEQ;
                grfLABFU[i, colLABFUlabtest] = ins.LABTEST;
                grfLABFU[i, colLABFUlabresult] = ins.LABRESULT;

                grfLABFU[i, colLABFUflag] = "0";
                grfLABFU[i, 0] = i;
                //if (i % 2 == 0)
                //    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            pageLoad = false;
        }
        private void setGrfCHAD()
        {
            pageLoad = true;
            grfCHAD.DataSource = null;
            grfCHAD.Rows.Count = 1;
            grfCHAD.Cols.Count = 14;
            grfCHAD.Rows.Count = lCHAD.Count + 1;
            grfCHAD.Cols[colCHADhn].Caption = "HN";
            grfCHAD.Cols[colCHADdateserv].Caption = "date";
            grfCHAD.Cols[colCHADseq].Caption = "seq";
            grfCHAD.Cols[colCHADclinic].Caption = "clinic";
            grfCHAD.Cols[colCHADitemtype].Caption = "itm type";
            grfCHAD.Cols[colCHADitemcode].Caption = "itm code";
            grfCHAD.Cols[colCHADitemsrc].Caption = "itm src";
            grfCHAD.Cols[colCHADprovider].Caption = "provider";
            grfCHAD.Cols[colCHADqty].Caption = "qty";
            grfCHAD.Cols[colCHADamount].Caption = "amount";
            grfCHAD.Cols[colCHADamountext].Caption = "amt text";
            grfCHAD.Cols[colCHADaddon_desc].Caption = "addon";

            grfCHAD.Cols[colCHADhn].Width = 60;
            grfCHAD.Cols[colCHADdateserv].Width = 60;
            grfCHAD.Cols[colCHADseq].Width = 60;
            grfCHAD.Cols[colCHADclinic].Width = 60;
            grfCHAD.Cols[colCHADitemtype].Width = 300;
            grfCHAD.Cols[colCHADitemcode].Width = 80;
            grfCHAD.Cols[colCHADitemsrc].Width = 300;
            grfCHAD.Cols[colCHADprovider].Width = 300;
            grfCHAD.Cols[colCHADqty].Width = 300;
            grfCHAD.Cols[colCHADamount].Width = 300;
            grfCHAD.Cols[colCHADamountext].Width = 300;
            grfCHAD.Cols[colCHADaddon_desc].Width = 300;

            int i = 0;
            foreach (OPBKKCHAD ins in lCHAD)
            {
                i++;
                //if (i == 1) continue;
                grfCHAD[i, colCHADhn] = ins.HN;
                grfCHAD[i, colCHADdateserv] = ins.DATESERV;
                grfCHAD[i, colCHADseq] = ins.SEQ;
                grfCHAD[i, colCHADclinic] = ins.CLINIC;
                grfCHAD[i, colCHADitemtype] = ins.ITEMTYPE;
                grfCHAD[i, colCHADitemcode] = ins.ITEMCODE;
                grfCHAD[i, colCHADitemsrc] = ins.ITEMSRC;
                grfCHAD[i, colCHADqty] = ins.QTY;
                grfCHAD[i, colCHADamount] = ins.AMOUNT;
                grfCHAD[i, colCHADamountext] = ins.AMOUNT_EXT;
                grfCHAD[i, colCHADprovider] = ins.PROVIDER;
                grfCHAD[i, colCHADaddon_desc] = ins.ADDON_DESC;


                grfCHAD[i, colCHADflag] = "0";
                grfCHAD[i, 0] = i;
                //if (i % 2 == 0)
                //    grfClinic.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            }
            pageLoad = false;
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
        private void genINS(String flag)
        {
            showLbLoading();
            if (flag.Equals("txt"))
            {
                int i = 0;
                foreach (Row row in grfINS.Rows)
                {
                    if (row[colINSflag] == null) continue;
                    if (row[colINSflag].ToString().Equals("1"))
                    {
                        OPBKKINS ins = new OPBKKINS();
                        ins.HN = row[colINShn].ToString();
                        ins.INSCL = row[colINSinscl].ToString();
                        ins.SUBTYPE = row[colINSsubtype].ToString();
                        ins.CID = row[colINScid].ToString();
                        ins.DATEIN = row[colINSdatein].ToString();                                                //  วันเดือนปีที่มีสิทธิ ปีมีค่าเป็น ค.ศ
                        ins.DATEEXP = row[colINSdateexp].ToString();                                               //  วันเดือนปีที่หมดสิทธิ ปีมีค่าเป็น ค.ศ.
                        ins.HOSPMAIN = txtHospMain.Text.Trim();
                        ins.HOSPSUB = row[colINShospsub].ToString();
                        ins.GOVCODE = row[colINSgovcode].ToString();
                        ins.GOVNAME = row[colINSgovname].ToString();
                        ins.PERMITNO = row[colINSpermitno].ToString();
                        ins.DOCNO = row[colINSdocno].ToString();
                        ins.OWNRPID = row[colINSownrpid].ToString();
                        ins.OWNNAME = row[colINSownname].ToString();
                        lINS.RemoveAt(i);
                        lINS.Insert(i,ins);
                    }
                    i++;
                }
            }
            else
            {
                if (dtPtt.Rows.Count > 0)
                {
                    lINS.Clear();
                    foreach (DataRow drow in dtPtt.Rows)
                    {
                        OPBKKINS ins = new OPBKKINS();
                        ins.HN = drow["mnc_hn_no"].ToString();
                        ins.INSCL = drow["opbkk_inscl"].ToString();
                        ins.SUBTYPE = "00";
                        ins.CID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
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
                }
            }
            hideLbLoading();
        }
        private void genPAT(String flag)
        {
            showLbLoading();
            if (flag.Equals("txt"))
            {
                int i = 0;
                foreach (Row row in grfPAT.Rows)
                {
                    if (row[colPATflag] == null) continue;
                    if (row[colPATflag].ToString().Equals("1"))
                    {
                        OPBKKPAT pat = new OPBKKPAT();
                        pat.HCODE = txtHCode.Text.Trim();
                        pat.HN = row[colPAThn].ToString();
                        pat.CHANGWAT = row[colPATchangwat].ToString();
                        pat.AMPHUR = row[colPATamphur].ToString();
                        pat.DOB = row[colPATdob].ToString();
                        pat.SEX = row[colPATsex].ToString();
                        pat.MARRIAGE = row[colPATmarriage].ToString();
                        pat.OCCUPA = row[colPAToccupa].ToString();
                        pat.NATION = row[colPATnation].ToString();
                        pat.PERSON_ID = row[colPATpersonid].ToString();
                        pat.NAMEPAT = row[colPATnamepat].ToString();
                        pat.TITLE = row[colPATtitle].ToString();
                        pat.FNAME = row[colPATfname].ToString();
                        pat.LNAME = row[colPATlname].ToString();
                        pat.IDTYPE = "1";                       //  ประเภทบัตร   1 = บัตรประชาชน  2 = หนังสือเดินทาง 3 = หนังสือต่างด้าว 4 = หนังสือ / เอกสารอื่นๆ
                        lINS.RemoveAt(i);
                        lPAT.Add(pat);
                    }
                    i++;
                }
            }
            else
            {
                if (dtPtt.Rows.Count > 0)
                {
                    lPAT.Clear();
                    foreach (DataRow drow in dtPtt.Rows)
                    {
                        try
                        {
                            DateTime date = new DateTime();
                            DateTime.TryParse(drow["mnc_bday"].ToString(), out date);
                            if (date.Year > 2500)
                            {
                                date = date.AddYears(-543);
                            }
                            else if (date.Year < 2000)
                            {
                                date = date.AddYears(543);
                            }
                            String prov = "", amphu = "";
                            prov = drow["MNC_CUR_CHW"] != null ? drow["MNC_CUR_CHW"].ToString() : "";
                            amphu = drow["MNC_CUR_AMP"] != null ? drow["MNC_CUR_AMP"].ToString() : "";
                            prov = prov.Length >= 2 ? prov.Substring(0, 2) : "";
                            amphu = amphu.Length >= 2 ? amphu.Substring(0, 2) : "";
                            OPBKKPAT pat = new OPBKKPAT();
                            pat.HCODE = txtHCode.Text.Trim();
                            pat.HN = drow["mnc_hn_no"].ToString();
                            pat.CHANGWAT = prov;
                            pat.AMPHUR = amphu;
                            pat.DOB = date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                            pat.SEX = drow["MNC_SEX"] != null ? drow["MNC_SEX"].ToString().Equals("M") ? "1" : "2" : "";
                            pat.MARRIAGE = drow["MNC_STATUS"].ToString();
                            pat.OCCUPA = "";
                            //pat.NATION = drow["MNC_NAT_CD"] != null ? drow["MNC_NAT_CD"].ToString() : "";
                            pat.NATION = "099";
                            pat.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                            pat.NAMEPAT = drow["MNC_FNAME_T"].ToString() + " " + drow["MNC_LNAME_T"].ToString();
                            pat.TITLE = drow["prefix"] != null ? drow["prefix"].ToString() : "";
                            pat.FNAME = drow["MNC_FNAME_T"].ToString();
                            pat.LNAME = drow["MNC_LNAME_T"].ToString();
                            pat.IDTYPE = "1";                       //  ประเภทบัตร   1 = บัตรประชาชน  2 = หนังสือเดินทาง 3 = หนังสือต่างด้าว 4 = หนังสือ / เอกสารอื่นๆ
                            lPAT.Add(pat);
                        }
                        catch(Exception ex)
                        {
                            String aaa = "";
                        }
                        
                    }
                }
            }
            hideLbLoading();
        }
        private void genOPD(String flag)
        {
            showLbLoading();
            if (flag.Equals("txt"))
            {
                int i = 0;
                foreach (Row row in grfOPD.Rows)
                {
                    if (row[colOPDflag] == null) continue;
                    if (row[colOPDflag].ToString().Equals("1"))
                    {
                        OPBKKOPD opd = new OPBKKOPD();
                        opd.PERSON_ID = row[colOPDpersonid].ToString();
                        opd.HN = row[colOPDhn].ToString();
                        opd.DATEOPD = row[colOPDdateopd].ToString();
                        opd.TIMEOPD = row[colOPDtimeopd].ToString();
                        opd.SEQ = row[colOPDseq].ToString();
                        opd.UUC = "1";                                                       //      การใช้สิทธิ (เพิ่มเติม) 1 = ใช้สิทธิ 2 = ไมใช้สิทธ
                        opd.DETAIL = row[colOPDdetail].ToString();
                        opd.BTEMP = row[colOPDbtemp].ToString();
                        opd.SBP = row[colOPDsbp].ToString();                //      ความดันโลหิตค่าตัวบน
                        opd.DBP = row[colOPDdbp].ToString();                //      ความดันโลหิตค่าตัวล่าง
                        opd.PR = row[colOPDpr].ToString();                 //      อัตราการเต้นหัวใจ
                        opd.RR = row[colOPDrr].ToString();                 //      อัตราการหายใจ
                        opd.OPTYPE = "AE";          // ประเภทการให้บริการ 0 = Refer ในบัญชีเครือข่ายเดียวกัน 1 = Refer นอกบัญชีเครือข่าย 2 = AE ในบัญชีเครือข่าย 3 = AE นอกบัญชีเครือข่าย 4 = OP พิการ 5 = OP บัตรตัวเอง 6 = Clearing House ศบส 7 = OP อื่นๆ(Individual data) 8 = ผู้ป่วยกึ่ง OP / IP(NONI) 9 = บริการแพทย์แผนไทย
                        opd.TYPEIN = "";                    //ประเภทการมารับบริการ   1 = มารับบริการเอง 2 = มารับบริการตามนัดหมาย 3 = ได้รับการส่งต่อจากสถานพยาบาลอื่น 4 = ได้รับการส่งตัวจากบริการ EMS
                        opd.TYPEOUT = "";                   //สถานะผู้ป่วยเมื่อเสร็จสิ้นบริการ 1 = จำหน่ายกลับบ้าน 2 = รับไว้รักษาต่อIP 3 = Refer ต่อ 4 = เสียชีวิต 5 = เสียชีวิตก่อนมาถึง 6 = เสียชีวิตระหว่างส่งต่อไปยังที่อื่น 7 = ปฏิเสธการรักษา 8 = หนีกลับ
                        opd.CLAIM_CODE = "";                //รหัส ClaimCode เข้ารับการบริการ ที่ออกโดย สปสช.       *ถ้ามีการขอเบิกชดเชยค่าบริการต้องส่งแนบมาด้วย
                        lOPD.RemoveAt(i);
                        lOPD.Add(opd);
                    }
                    i++;
                }
            }
            else
            {
                if (dtPtt.Rows.Count > 0)
                {
                    lOPD.Clear();
                    foreach (DataRow drow in dtPtt.Rows)
                    {
                        String sbp = "", dbp="",seq="";
                        sbp = drow[bc.iniC.OPD_SBP] != null ? drow[bc.iniC.OPD_SBP].ToString() : "";
                        dbp = drow[bc.iniC.OPD_DBP] != null ? drow[bc.iniC.OPD_DBP].ToString() : "";
                        if (bc.iniC.branchId.Equals("005"))
                        {
                            if (sbp.IndexOf('/') >= 0)
                            {
                                String[] sbp1 = sbp.Split('/');
                                sbp = sbp1[0];
                                dbp = sbp1[1];
                            }
                        }
                        
                        DateTime date = new DateTime();
                        DateTime.TryParse(drow["mnc_date"].ToString(), out date);
                        if (date.Year > 2500)
                        {
                            date = date.AddYears(-543);
                        }
                        else if (date.Year < 2000)
                        {
                            date = date.AddYears(543);
                        }
                        seq = date.ToString("yyyyMMdd", new CultureInfo("en-US")) + drow["mnc_pre_no"].ToString();
                        OPBKKOPD opd = new OPBKKOPD();
                        opd.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                        opd.HN = drow["mnc_hn_no"].ToString();
                        opd.DATEOPD = date.ToString("yyyyMMdd", new CultureInfo("en-US"));
                        opd.TIMEOPD = ("0000" + drow["MNC_time"].ToString()).Substring(4);
                        opd.SEQ = seq;
                        opd.UUC = "1";                                                       //      การใช้สิทธิ (เพิ่มเติม) 1 = ใช้สิทธิ 2 = ไมใช้สิทธ
                        opd.DETAIL = drow["MNC_SHIF_MEMO"].ToString().Replace("\r\n","");
                        opd.BTEMP = drow[bc.iniC.OPD_BTEMP] != null ? drow[bc.iniC.OPD_BTEMP].ToString() : "";
                        opd.SBP = sbp;    //      ความดันโลหิตค่าตัวบน
                        opd.DBP = dbp;    //      ความดันโลหิตค่าตัวล่าง
                        opd.PR = drow[bc.iniC.OPD_PR] != null ? drow[bc.iniC.OPD_PR].ToString() : "";     //      อัตราการเต้นหัวใจ
                        opd.RR = drow[bc.iniC.OPD_RR] != null ? drow[bc.iniC.OPD_RR].ToString() : "";     //      อัตราการหายใจ
                        opd.OPTYPE = "AE";          // ประเภทการให้บริการ 0 = Refer ในบัญชีเครือข่ายเดียวกัน 1 = Refer นอกบัญชีเครือข่าย 2 = AE ในบัญชีเครือข่าย 3 = AE นอกบัญชีเครือข่าย 4 = OP พิการ 5 = OP บัตรตัวเอง 6 = Clearing House ศบส 7 = OP อื่นๆ(Individual data) 8 = ผู้ป่วยกึ่ง OP / IP(NONI) 9 = บริการแพทย์แผนไทย
                        opd.TYPEIN = "";                    //ประเภทการมารับบริการ   1 = มารับบริการเอง 2 = มารับบริการตามนัดหมาย 3 = ได้รับการส่งต่อจากสถานพยาบาลอื่น 4 = ได้รับการส่งตัวจากบริการ EMS
                        opd.TYPEOUT = "";                   //สถานะผู้ป่วยเมื่อเสร็จสิ้นบริการ 1 = จำหน่ายกลับบ้าน 2 = รับไว้รักษาต่อIP 3 = Refer ต่อ 4 = เสียชีวิต 5 = เสียชีวิตก่อนมาถึง 6 = เสียชีวิตระหว่างส่งต่อไปยังที่อื่น 7 = ปฏิเสธการรักษา 8 = หนีกลับ
                        opd.CLAIM_CODE = "";                //รหัส ClaimCode เข้ารับการบริการ ที่ออกโดย สปสช.       *ถ้ามีการขอเบิกชดเชยค่าบริการต้องส่งแนบมาด้วย
                        lOPD.Add(opd);
                    }
                }
            }
            hideLbLoading();
        }
        private void genORF(String flag)
        {
            //แฟ้มข้อมูลที่ 4 มาตรฐานแฟ้มข้อมูลผู้ป่ วยนอกที่ต้องส่งต่อ (ORF)
            showLbLoading();
            if (flag.Equals("txt"))
            {
                int i = 0;
                foreach (Row row in grfORF.Rows)
                {
                    if (row[colORFflag] == null) continue;
                    if (row[colORFflag].ToString().Equals("1"))
                    {
                        OPBKKORF orf = new OPBKKORF();
                        orf.HN = row[colORFhn].ToString();
                        orf.DATEOPD = row[colORFdateopd].ToString();
                        orf.CLINIC = row[colORFclinic].ToString();        //รหัสคลินิกที่รับบริการ
                        orf.REFER = row[colORFrefer].ToString();
                        orf.REFERTYPE = row[colORFrefertype].ToString();
                        orf.REFERDATE = row[colORFreferdate].ToString();
                        orf.SEQ = row[colORFseq].ToString();
                        lORF.RemoveAt(i);
                        lORF.Add(orf);
                    }
                    i++;
                }
            }
            else
            {
                if (dtPtt.Rows.Count > 0)
                {
                    lORF.Clear();
                    foreach (DataRow drow in dtPtt.Rows)
                    {
                        String seq = "";
                        DateTime date = new DateTime();
                        DateTime.TryParse(drow["mnc_date"].ToString(), out date);
                        if (date.Year > 2500)
                        {
                            date = date.AddYears(-543);
                        }
                        else if (date.Year < 2000)
                        {
                            date = date.AddYears(543);
                        }
                        seq = date.ToString("yyyyMMdd", new CultureInfo("en-US")) + drow["mnc_pre_no"].ToString();
                        OPBKKORF orf = new OPBKKORF();
                        orf.HN = drow["mnc_hn_no"].ToString();
                        orf.DATEOPD = date.ToString("yyyyMMdd", new CultureInfo("en-US"));
                        orf.CLINIC = "";        //รหัสคลินิกที่รับบริการ
                        orf.REFER = "";
                        orf.REFERTYPE = "";
                        orf.REFERDATE = "";
                        orf.SEQ = seq;
                        lORF.Add(orf);
                    }
                }
            }
            hideLbLoading();
        }
        private void genODX(String flag)
        {
            //แฟ้มข้อมูลที่ 5 มาตรฐานแฟ้มข้อมูลวินิจฉัยโรคผู้ป่วยนอก (ODX)    ICD10
            showLbLoading();
            if (flag.Equals("txt"))
            {
                int i = 0;
                foreach (Row row in grfODX.Rows)
                {
                    if (row[colODXflag] == null) continue;
                    if (row[colODXflag].ToString().Equals("1"))
                    {
                        OPBKKODX ins = new OPBKKODX();
                        ins.HN = row[colODXhn].ToString();
                        ins.DATEDX = row[colODXdatedx].ToString();
                        ins.CLINIC = row[colODXclinic].ToString();
                        ins.DIAG = row[colODXdiag].ToString();
                        ins.DXTYPE = row[colODXdxtype].ToString();
                        ins.DRDX = row[colODXdrdx].ToString();
                        ins.PERSON_ID = row[colODXpersonid].ToString();
                        ins.SEQ = row[colODXseq].ToString();
                        lODX.RemoveAt(i);
                        lODX.Add(ins);
                    }
                    i++;
                }
            }
            else
            {
                if (dtPtt.Rows.Count > 0)
                {
                    lODX.Clear();
                    foreach (DataRow drow in dtPtt.Rows)
                    {
                        //dttime.ToString("MMM-dd-yyyy", new CultureInfo("en-US"));
                        String hn = "", hnyear = "", preno = "", vsdate = "",seq="";
                        hn = drow["mnc_hn_no"].ToString();
                        hnyear = drow["mnc_hn_yr"].ToString();
                        preno = drow["mnc_pre_no"].ToString();
                        DateTime vsdate1 = new DateTime();
                        DateTime.TryParse(drow["mnc_date"].ToString(), out vsdate1);
                        if (vsdate1.Year > 2500)
                        {
                            vsdate1 = vsdate1.AddYears(-543);
                        }
                        else if (vsdate1.Year < 2000)
                        {
                            vsdate1 = vsdate1.AddYears(543);
                        }
                        seq = vsdate1.ToString("yyyyMMdd", new CultureInfo("en-US")) + drow["mnc_pre_no"].ToString();
                        vsdate = vsdate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        //new LogWriter("d", "FrmOPBKKClaim genODX vsdate " + vsdate);
                        DataTable dtlab = bc.bcDB.vsDB.selectICD10ByHn(hn, hnyear, vsdate, preno);
                        if (dtlab.Rows.Count > 0)
                        {
                            foreach (DataRow drowodx in dtlab.Rows)
                            {
                                if (drowodx["mnc_dia_cd"].ToString().Length > 0)
                                {
                                    DateTime actdate = new DateTime();
                                    //new LogWriter("d", "FrmOPBKKClaim genODX drowodx ");
                                    DateTime.TryParse(drowodx["mnc_act_dat"].ToString(), out actdate);
                                    if (actdate.Year > 2500)
                                    {
                                        actdate = actdate.AddYears(-543);
                                    }
                                    else if (actdate.Year < 2000)
                                    {
                                        actdate = actdate.AddYears(543);
                                    }
                                    OPBKKODX ins = new OPBKKODX();
                                    ins.HN = drow["mnc_hn_no"].ToString();
                                    ins.DATEDX = actdate.ToString("yyyyMMdd", new CultureInfo("en-US"));
                                    ins.CLINIC = "";
                                    ins.DIAG = drowodx["MNC_dia_cd"].ToString();
                                    ins.DXTYPE = drowodx["MNC_dia_flg"].ToString();
                                    ins.DRDX = drowodx["MNC_dot_cd"].ToString();
                                    ins.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                                    ins.SEQ = seq;
                                    lODX.Add(ins);
                                }
                            }
                        }
                    }
                }
            }
            hideLbLoading();
        }
        private void genOOP(String flag)
        {
            //แฟ้มข้อมูลที่ 6 มาตรฐานแฟ้มข้อมูลหัตถการผู้ป่วยนอก (OOP)      ICD9
            showLbLoading();
            if (flag.Equals("txt"))
            {
                int i = 0;
                foreach (Row row in grfOOP.Rows)
                {
                    if (row[colOOPflag] == null) continue;
                    if (row[colOOPflag].ToString().Equals("1"))
                    {
                        OPBKKOOP ins = new OPBKKOOP();
                        ins.HN = row[colOOPhn].ToString();
                        ins.DATEOPD = row[colOOPdateopd].ToString();
                        ins.CLINIC = row[colOOPclinic].ToString();
                        ins.OPER = row[colOOPoper].ToString();
                        ins.SERVPRICE = row[colOOPservprice].ToString();
                        ins.DROPID = row[colOOPdropid].ToString();
                        ins.PERSON_ID = row[colOOPpersonid].ToString();
                        ins.SEQ = row[colOOPseq].ToString();
                        lOOP.RemoveAt(i);
                        lOOP.Add(ins);
                    }
                    i++;
                }
            }
            else
            {
                if (dtPtt.Rows.Count > 0)
                {
                    lOOP.Clear();
                    foreach (DataRow drow in dtPtt.Rows)
                    {
                        String hn = "", hnyear = "", preno = "", vsdate = "",seq="";
                        hn = drow["mnc_hn_no"].ToString();
                        hnyear = drow["mnc_hn_yr"].ToString();
                        preno = drow["mnc_pre_no"].ToString();
                        //vsdate = bc.datetoDB(drow["mnc_date"].ToString());
                        DateTime vsdate1 = new DateTime();
                        DateTime.TryParse(drow["mnc_date"].ToString(), out vsdate1);
                        if (vsdate1.Year > 2500)
                        {
                            vsdate1 = vsdate1.AddYears(-543);
                        }
                        else if (vsdate1.Year < 2000)
                        {
                            vsdate1 = vsdate1.AddYears(543);
                        }
                        seq = vsdate1.ToString("yyyyMMdd", new CultureInfo("en-US")) + drow["mnc_pre_no"].ToString();
                        vsdate = vsdate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        DataTable dtoop = bc.bcDB.vsDB.selectICD9ByHn(hn, hnyear, vsdate, preno);
                        if (dtoop.Rows.Count > 0)
                        {
                            foreach (DataRow drowoop in dtoop.Rows)
                            {
                                if (drowoop["mnc_diaor_cd"].ToString().Length > 0)
                                {
                                    DateTime date = new DateTime();
                                    DateTime.TryParse(drowoop["mnc_act_dat"].ToString(), out date);
                                    if (date.Year > 2500)
                                    {
                                        date = date.AddYears(-543);
                                    }
                                    else if (date.Year < 2000)
                                    {
                                        date = date.AddYears(543);
                                    }
                                    new LogWriter("d", "FrmOPBKKClaim genOOP drowoop[mnc_act_dat].ToString() " + drowoop["mnc_act_dat"].ToString() + " date " + date);
                                    OPBKKOOP ins = new OPBKKOOP();
                                    ins.HN = drow["mnc_hn_no"].ToString();
                                    ins.DATEOPD = vsdate;
                                    ins.CLINIC = "";
                                    ins.OPER = drowoop["MNC_diaor_cd"].ToString();
                                    ins.SERVPRICE = "";
                                    ins.DROPID = drow["MNC_dot_cd"].ToString();
                                    ins.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                                    ins.SEQ = seq;
                                    lOOP.Add(ins);
                                }
                            }
                        }
                    }
                }
            }
            hideLbLoading();
        }
        private void genCHT(String flag)
        {
            //แฟ้มข้อมูลที่ 7 มาตรฐานแฟ้มข้อมูลการเงิน(แบบสรุป)(CHT)
            showLbLoading();
            if (flag.Equals("txt"))
            {
                int i = 0;
                foreach (Row row in grfCHT.Rows)
                {
                    if (row[colCHTflag] == null) continue;
                    if (row[colCHTflag].ToString().Equals("1"))
                    {
                        OPBKKCHT ins = new OPBKKCHT();
                        ins.HN = row[colCHThn].ToString();
                        ins.DATEOPD = row[colCHTdateopd].ToString();
                        ins.ACTUALCHT = row[colCHTactualcht].ToString();
                        ins.TOTAL = row[colCHTtotal].ToString();
                        ins.PAID = row[colCHTpaid].ToString();
                        ins.PTTYPE = row[colCHTpttype].ToString();
                        ins.PERSON_ID = row[colCHTpersonid].ToString();
                        ins.SEQ = row[colCHTseq].ToString();
                        ins.OPD_MEMO = row[colCHTopd_memo].ToString();
                        ins.INVOICE_NO = row[colCHTinvoiceno].ToString();
                        ins.INVOICE_LT = row[colCHTinvoicelt].ToString();
                        lCHT.RemoveAt(i);
                        lCHT.Add(ins);
                    }
                    i++;
                }
            }
            else
            {
                int row = 0;
                if (dtPtt.Rows.Count > 0)
                {
                    lCHT.Clear();
                    pageLoad = true;
                    foreach (DataRow drow in dtPtt.Rows)
                    {
                        row++;
                        setLbLoading("genCHT " + row + "/" + dtPtt.Rows.Count);
                        String hn = "", hnyear = "", preno = "", vsdate = "",seq="";
                        hn = drow["mnc_hn_no"].ToString();
                        hnyear = drow["mnc_hn_yr"].ToString();
                        preno = drow["mnc_pre_no"].ToString();
                        //vsdate = bc.datetoDB(drow["mnc_date"].ToString());
                        DateTime vsdate1 = new DateTime();
                        DateTime.TryParse(drow["mnc_date"].ToString(), out vsdate1);
                        if (vsdate1.Year > 2500)
                        {
                            vsdate1 = vsdate1.AddYears(-543);
                        }
                        else if (vsdate1.Year < 2000)
                        {
                            vsdate1 = vsdate1.AddYears(543);
                        }
                        seq = vsdate1.ToString("yyyyMMdd", new CultureInfo("en-US")) + drow["mnc_pre_no"].ToString();
                        vsdate = vsdate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        DataTable dtcht = bc.bcDB.vsDB.selectPaidByHnOPBKK(hn, hnyear, vsdate, preno);
                        if (dtcht.Rows.Count > 0)
                        {
                            foreach (DataRow drowlabfu in dtcht.Rows)
                            {
                                float sumpri = 0;
                                float.TryParse(drowlabfu["mnc_sum_pri"].ToString(), out sumpri);
                                if (sumpri > 0)
                                {
                                    DateTime date = new DateTime();
                                    DateTime.TryParse(drowlabfu["mnc_doc_dat"].ToString(), out date);
                                    if (date.Year > 2500)
                                    {
                                        date = date.AddYears(-543);
                                    }
                                    else if (date.Year < 2000)
                                    {
                                        date = date.AddYears(543);
                                    }
                                    OPBKKCHT ins = new OPBKKCHT();
                                    ins.HN = drow["mnc_hn_no"].ToString();
                                    ins.DATEOPD = date.ToString("yyyyMMdd", new CultureInfo("en-US"));
                                    ins.ACTUALCHT = drowlabfu["mnc_sum_pri"].ToString();
                                    ins.TOTAL = drowlabfu["mnc_sum_pri"].ToString();
                                    //ins.PAID = drowlabfu["mnc_sum_pri"].ToString();
                                    ins.PAID = "0";
                                    ins.PTTYPE = "UC";
                                    ins.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                                    ins.SEQ = seq;
                                    ins.OPD_MEMO = "";
                                    ins.INVOICE_NO = drowlabfu["mnc_doc_yr"].ToString() + "" + drowlabfu["mnc_doc_no"].ToString();
                                    ins.INVOICE_LT = "";
                                    lCHT.Add(ins);
                                }
                            }
                        }
                    }
                    pageLoad = false;
                }
            }
            hideLbLoading();
        }
        private void genCHA(String flag)
        {
            showLbLoading();
            if (flag.Equals("txt"))
            {
                int i = 0;
                foreach (Row row in grfCHA.Rows)
                {
                    if (row[colCHAflag] == null) continue;
                    if (row[colCHAflag].ToString().Equals("1"))
                    {
                        OPBKKCHA ins = new OPBKKCHA();
                        ins.HN = row[colCHAhn].ToString();
                        ins.DATEOPD = row[colCHAdateopd].ToString();
                        ins.CHRGITEM = row[colCHAchrgitem].ToString();
                        ins.AMOUNT = row[colCHAamount].ToString();
                        ins.AMOUNT_EXT = row[colCHAamountext].ToString();
                        ins.PERSON_ID = row[colCHApersonid].ToString();
                        ins.SEQ = row[colCHAseq].ToString();
                        lCHA.RemoveAt(i);
                        lCHA.Add(ins);
                    }
                }
            }
            else
            {
                int row = 0;
                if (dtPtt.Rows.Count > 0)
                {
                    lCHA.Clear();
                    pageLoad = true;
                    foreach (DataRow drow in dtPtt.Rows)
                    {
                        row++;
                        setLbLoading("genCHA " + row + "/" + dtPtt.Rows.Count);
                        String hn = "", hnyear = "", preno = "", vsdate = "", seq="";
                        hn = drow["mnc_hn_no"].ToString();
                        hnyear = drow["mnc_hn_yr"].ToString();
                        preno = drow["mnc_pre_no"].ToString();
                        //vsdate = bc.datetoDB(drow["mnc_date"].ToString());
                        DateTime vsdate1 = new DateTime();
                        DateTime.TryParse(drow["mnc_date"].ToString(), out vsdate1);
                        if (vsdate1.Year > 2500)
                        {
                            vsdate1 = vsdate1.AddYears(-543);
                        }
                        else if (vsdate1.Year < 2000)
                        {
                            vsdate1 = vsdate1.AddYears(543);
                        }
                        seq = vsdate1.ToString("yyyyMMdd", new CultureInfo("en-US")) + drow["mnc_pre_no"].ToString();
                        vsdate = vsdate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        DataTable dtcha = bc.bcDB.vsDB.selectPaidByHnOPBKKCHA(hn, hnyear, vsdate, preno);
                        if (dtcha.Rows.Count > 0)
                        {
                            foreach (DataRow drowlabfu in dtcha.Rows)
                            {
                                float sumpri = 0;
                                float.TryParse(drowlabfu["mnc_sum_pri"].ToString(), out sumpri);
                                if (sumpri > 0)
                                {
                                    DateTime date = new DateTime();
                                    DateTime.TryParse(drowlabfu["mnc_doc_dat"].ToString(), out date);
                                    if (date.Year > 2500)
                                    {
                                        date = date.AddYears(-543);
                                    }
                                    else if (date.Year < 2000)
                                    {
                                        date = date.AddYears(543);
                                    }
                                    OPBKKCHA ins = new OPBKKCHA();
                                    ins.HN = drow["mnc_hn_no"].ToString();
                                    ins.DATEOPD = date.ToString("yyyyMMdd", new CultureInfo("en-US"));
                                    ins.CHRGITEM = "";
                                    ins.AMOUNT = drowlabfu["mnc_sum_pri"].ToString();
                                    ins.AMOUNT_EXT = drowlabfu["mnc_sum_pri"].ToString();
                                    ins.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                                    ins.SEQ = seq;
                                    lCHA.Add(ins);
                                }
                            }
                        }



                        
                    }
                    pageLoad = false;
                }
            }
            hideLbLoading();
        }
        private void genAER(String flag)
        {
            showLbLoading();
            if (flag.Equals("txt"))
            {
                int i = 0;
                foreach (Row row in grfAER.Rows)
                {
                    if (row[colAERflag] == null) continue;
                    if (row[colAERflag].ToString().Equals("1"))
                    {
                        OPBKKAER ins = new OPBKKAER();
                        ins.HN = row[colAERhn].ToString();
                        ins.DATEOPD = row[colAERdateopd].ToString();
                        ins.AUTHAE = row[colAERauthae].ToString();
                        ins.AEDATE = row[colAERaedate].ToString();
                        ins.AETIME = row[colAERaetime].ToString();
                        ins.AETYPE = row[colAERaetype].ToString();
                        ins.REFER_NO = row[colAERreferno].ToString();
                        ins.REFMAINI = row[colAERrefmaini].ToString();
                        ins.IREFTYPE = row[colAERireftype].ToString();
                        ins.REFMAINO = row[colAERrefmainno].ToString();
                        ins.OREFTYPE = row[colAERoreftype].ToString();
                        ins.UCAE = row[colAERucae].ToString();
                        ins.EMTYPE = row[colAERemtype].ToString();
                        ins.SEQ = row[colAERseq].ToString();
                        lAER.RemoveAt(i);
                        lAER.Add(ins);
                    }
                    i++;
                }
            }
            else
            {
                if (dtPtt.Rows.Count > 0)
                {
                    lAER.Clear();
                    foreach (DataRow drow in dtPtt.Rows)
                    {
                        String seq = "";
                        DateTime date = new DateTime();
                        DateTime.TryParse(drow["mnc_date"].ToString(), out date);
                        if (date.Year > 2500)
                        {
                            date = date.AddYears(-543);
                        }
                        else if (date.Year < 2000)
                        {
                            date = date.AddYears(543);
                        }
                        seq = date.ToString("yyyyMMdd", new CultureInfo("en-US")) + drow["mnc_pre_no"].ToString();
                        OPBKKAER ins = new OPBKKAER();
                        ins.HN = drow["mnc_hn_no"].ToString();
                        ins.DATEOPD = date.ToString("yyyyMMdd", new CultureInfo("en-US"));
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
                        ins.SEQ = seq;
                        lAER.Add(ins);
                    }
                }
            }
            hideLbLoading();
        }
        private void genDRU(String flag)
        {
            showLbLoading();
            if (flag.Equals("txt"))
            {
                int i = 0;
                foreach (Row row in grfDRU.Rows)
                {
                    if (row[colDRUflag] == null) continue;
                    if (row[colDRUflag].ToString().Equals("1"))
                    {
                        OPBKKDRU ins = new OPBKKDRU();
                        ins.HCODE = txtHCode.Text.Trim();
                        ins.HN = row[colDRUhn].ToString();
                        ins.CLINIC = row[colDRUclinic].ToString();
                        ins.PERSON_ID = row[colDRUpersonid].ToString();
                        ins.DATESERV = row[colDRUdateserv].ToString();
                        ins.DID = row[colDRUdid].ToString();
                        ins.DIDNAME = row[colDRUdidname].ToString();
                        ins.DIDSTD = row[colDRUdidstd].ToString();
                        ins.TMTCODE = row[colDRUtmtcode].ToString();
                        ins.AMOUNT = row[colDRUamount].ToString();
                        ins.DRUGPRICE = row[colDRUdrugprice].ToString();
                        ins.PRICE_EXT = row[colDRUpriceext].ToString();
                        ins.DRUGCOST = row[colDRUdrugcost].ToString();
                        ins.UNIT = row[colDRUunit].ToString();
                        ins.UNIT_PACK = row[colDRUunitpack].ToString();
                        ins.SEQ = row[colDRUseq].ToString();
                        ins.PROVIDER = row[colDRUprovider].ToString();
                        lDRU.RemoveAt(i);
                        lDRU.Add(ins);
                    }
                    i++;
                }
            }
            else
            {
                int row = 0;
                if (dtPtt.Rows.Count > 0)
                {
                    lDRU.Clear();
                    pageLoad = true;
                    bc.bcDB.opbkkDrugCatDB.getlopbkkDrugCat();
                    foreach (DataRow drow in dtPtt.Rows)
                    {
                        row++;
                        setLbLoading("genDRU "+row +"/"+dtPtt.Rows.Count);
                        String hn = "", hnyear = "", preno = "", vsdate = "",seq="";
                        hn = drow["mnc_hn_no"].ToString();
                        hnyear = drow["mnc_hn_yr"].ToString();
                        preno = drow["mnc_pre_no"].ToString();
                        //vsdate = bc.datetoDB(drow["mnc_date"].ToString());
                        DateTime vsdate1 = new DateTime();
                        DateTime.TryParse(drow["mnc_date"].ToString(), out vsdate1);
                        if (vsdate1.Year > 2500)
                        {
                            vsdate1 = vsdate1.AddYears(-543);
                        }
                        else if (vsdate1.Year < 2000)
                        {
                            vsdate1 = vsdate1.AddYears(543);
                        }
                        seq = vsdate1.ToString("yyyyMMdd", new CultureInfo("en-US")) + drow["mnc_pre_no"].ToString();
                        vsdate = vsdate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        DataTable dtdrug = bc.bcDB.vsDB.selectDrugOPBKKByHn(hn, hnyear, vsdate, preno);
                        if (dtdrug.Rows.Count > 0)
                        {
                            foreach (DataRow drowdrug in dtdrug.Rows)
                            {
                                DateTime date = new DateTime();
                                DateTime.TryParse(drowdrug["mnc_req_dat"].ToString(), out date);
                                if (date.Year > 2500)
                                {
                                    date = date.AddYears(-543);
                                }
                                else if (date.Year < 2000)
                                {
                                    date = date.AddYears(543);
                                }
                                String tmtcode = "";
                                tmtcode = bc.bcDB.opbkkDrugCatDB.getTmtCodeBydrugcode(drowdrug["MNC_PH_CD"].ToString());
                                OPBKKDRU ins = new OPBKKDRU();
                                ins.HCODE = txtHCode.Text.Trim();
                                ins.HN = drow["mnc_hn_no"].ToString();
                                ins.CLINIC = "";
                                ins.PERSON_ID = drow["MNC_ID_Nam"] != null ? drow["MNC_ID_Nam"].ToString() : "";
                                ins.DATESERV = date.ToString("yyyyMMdd", new CultureInfo("en-US"));
                                ins.DID = drowdrug["MNC_PH_CD"].ToString();
                                ins.DIDNAME = drowdrug["MNC_PH_TN"].ToString();
                                ins.DIDSTD = "";
                                ins.TMTCODE = tmtcode;
                                ins.AMOUNT = drowdrug["qty"].ToString();        //จำนวนยาที่จ่าย
                                ins.DRUGPRICE = "";         // ราคายาที่ขอเบิก (ไม่ใช่ราคาต่อหน่วย)
                                ins.PRICE_EXT = "";         // ราคายาส่วนที่เบิกไม่ได้  (ไม่ใช่ราคาต่อหน่วย)
                                ins.DRUGCOST = "";
                                ins.UNIT = drowdrug["mnc_ph_unt_cd"].ToString();
                                ins.UNIT_PACK = "";
                                ins.SEQ = seq;
                                ins.PROVIDER = drowdrug["mnc_usr_add"].ToString();      //เภสัชกรที่จ่ายยา ตามเลขที่ใบประกอบวิชาชีพเวชกรรม
                                lDRU.Add(ins);
                            }
                        }
                    }
                    pageLoad = false;
                }

                //String datestart = "", dateend = "", paidtype = "";
                //datestart = bc.datetoDB(txtDateStart.Text);
                //dateend = bc.datetoDB(txtDateEnd.Text);
                //String[] paid = txtPaidType.Text.Trim().Split(',');
                //if (paid.Length > 0)
                //{
                //    foreach (String txt in paid)
                //    {
                //        paidtype += "'" + txt + "',";
                //    }
                //    if (paidtype.Length > 1)
                //    {
                //        paidtype = paidtype.Substring(0, paidtype.Length - 1);
                //    }
                //}
                //else
                //{
                //    paidtype = txtPaidType.Text.Trim();
                //}
                //DataTable dtdru = bc.bcDB.vsDB.selectDrugOPBKK(paidtype, datestart, dateend);
                //if (dtdru.Rows.Count > 0)
                //{
                //    lDRU.Clear();
                //    foreach (DataRow drow in dtdru.Rows)
                //    {
                //        OPBKKDRU ins = new OPBKKDRU();
                //        ins.HCODE = txtHCode.Text.Trim();
                //        ins.HN = drow["mnc_hn_no"].ToString();
                //        ins.CLINIC = "";
                //        ins.PERSON_ID = drow["MNC_ID_Nam"] != null ? drow["MNC_ID_Nam"].ToString() : "";
                //        ins.DATESERV = drow["mnc_req_dat"].ToString();
                //        ins.DID = drow["MNC_PH_CD"].ToString();
                //        ins.DIDNAME = drow["MNC_PH_TN"].ToString();
                //        ins.DIDSTD = "";
                //        ins.TMTCODE = "";
                //        ins.AMOUNT = drow["qty"].ToString();        //จำนวนยาที่จ่าย
                //        ins.DRUGPRICE = "";         // ราคายาที่ขอเบิก (ไม่ใช่ราคาต่อหน่วย)
                //        ins.PRICE_EXT = "";         // ราคายาส่วนที่เบิกไม่ได้  (ไม่ใช่ราคาต่อหน่วย)
                //        ins.DRUGCOST = "";
                //        ins.UNIT = "";
                //        ins.UNIT_PACK = "";
                //        ins.SEQ = drow["mnc_pre_no"].ToString();
                //        ins.PROVIDER = "";      //เภสัชกรที่จ่ายยา ตามเลขที่ใบประกอบวิชาชีพเวชกรรม
                //        lDRU.Add(ins);
                //    }
                //}








            }
            hideLbLoading();
        }
        private void genLABFU(String flag)
        {
            showLbLoading();
            if (flag.Equals("txt"))
            {
                int i = 0;
                foreach (Row row in grfLABFU.Rows)
                {
                    if (row[colLABFUflag] == null) continue;
                    if (row[colLABFUflag].ToString().Equals("1"))
                    {
                        OPBKKLABFU ins = new OPBKKLABFU();
                        ins.HCODE = txtHCode.Text.Trim();
                        ins.HN = row[colLABFUhn].ToString();
                        ins.PERSON_ID = row[colLABFUpersonid].ToString();
                        ins.DATESERV = row[colLABFUdateserv].ToString();
                        ins.SEQ = row[colLABFUseq].ToString();
                        ins.LABTEST = row[colLABFUlabtest].ToString();
                        ins.LABRESULT = row[colLABFUlabresult].ToString();
                        lLABFU.RemoveAt(i);
                        lLABFU.Add(ins);
                    }
                    i++;
                }
            }
            else
            {
                String datestart = "", dateend = "", paidtype = "";
                datestart = bc.datetoDB(txtDateStart.Text);
                dateend = bc.datetoDB(txtDateEnd.Text);
                String[] paid = txtPaidType.Text.Trim().Split(',');
                if (paid.Length > 0)
                {
                    foreach (String txt in paid)
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
                //DataTable dtlab = bc.bcDB.vsDB.selectResultLabByDate(paidtype, datestart, dateend);
                int row = 0;
                if (dtPtt.Rows.Count > 0)
                {
                    lLABFU.Clear();
                    pageLoad = true;
                    foreach (DataRow drow in dtPtt.Rows)
                    {
                        row++;
                        setLbLoading("genLABFU " + row + "/" + dtPtt.Rows.Count);
                        String hn = "", hnyear = "", preno = "", vsdate = "",seq="";
                        hn = drow["mnc_hn_no"].ToString();
                        hnyear = drow["mnc_hn_yr"].ToString();
                        preno = drow["mnc_pre_no"].ToString();
                        //vsdate = bc.datetoDB(drow["mnc_date"].ToString());
                        DateTime vsdate1 = new DateTime();
                        DateTime.TryParse(drow["mnc_date"].ToString(), out vsdate1);
                        if (vsdate1.Year > 2500)
                        {
                            vsdate1 = vsdate1.AddYears(-543);
                        }
                        else if (vsdate1.Year < 2000)
                        {
                            vsdate1 = vsdate1.AddYears(543);
                        }
                        seq = vsdate1.ToString("yyyyMMdd", new CultureInfo("en-US")) + drow["mnc_pre_no"].ToString();
                        vsdate = vsdate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        DataTable dtlab = bc.bcDB.vsDB.selectResultLabByHnOPBKK(hn, hnyear, vsdate, preno);
                        if (dtlab.Rows.Count > 0)
                        {
                            foreach (DataRow drowlabfu in dtlab.Rows)
                            {
                                DateTime date = new DateTime();
                                DateTime.TryParse(drowlabfu["mnc_req_dat"].ToString(), out date);
                                if (date.Year > 2500)
                                {
                                    date = date.AddYears(-543);
                                }
                                else if (date.Year < 2000)
                                {
                                    date = date.AddYears(543);
                                }
                                OPBKKLABFU ins = new OPBKKLABFU();
                                ins.HCODE = txtHCode.Text.Trim();
                                ins.HN = drow["mnc_hn_no"].ToString();
                                ins.PERSON_ID = drow["MNC_ID_nam"] != null ? drow["MNC_ID_nam"].ToString() : "";
                                ins.DATESERV = date.ToString("yyyyMMdd", new CultureInfo("en-US"));
                                ins.SEQ = seq;
                                ins.LABTEST = drowlabfu["MNC_RES"].ToString();
                                //ins.LABTEST = "01";     // fix for test
                                ins.LABRESULT = drowlabfu["MNC_RES_VALUE"].ToString();
                                lLABFU.Add(ins);
                            }
                        }
                    }
                    pageLoad = false;
                }
            }
            hideLbLoading();
        }
        private void genCHAD(String flag)
        {
            showLbLoading();
            if (flag.Equals("txt"))
            {
                int i = 0;
                foreach (Row row in grfCHAD.Rows)
                {
                    if (row[colCHADflag] == null) continue;
                    if (row[colCHADflag].ToString().Equals("1"))
                    {
                        OPBKKCHAD ins = new OPBKKCHAD();
                        ins.HN = row[colCHADhn].ToString();
                        ins.DATESERV = row[colCHADdateserv].ToString();
                        ins.SEQ = row[colCHADseq].ToString();
                        ins.CLINIC = row[colCHADclinic].ToString();
                        ins.ITEMTYPE = row[colCHADitemtype].ToString();
                        ins.ITEMCODE = row[colCHADitemcode].ToString();
                        ins.ITEMSRC = row[colCHADitemsrc].ToString();
                        ins.QTY = row[colCHADqty].ToString();
                        ins.AMOUNT = row[colCHADamount].ToString();
                        ins.AMOUNT_EXT = row[colCHADamountext].ToString();
                        ins.PROVIDER = row[colCHADprovider].ToString();
                        ins.ADDON_DESC = row[colCHADaddon_desc].ToString();
                        lCHAD.RemoveAt(i);
                        lCHAD.Add(ins);
                    }
                    i++;
                }
            }
            else
            {
                int row = 0;
                if (dtPtt.Rows.Count > 0)
                {
                    lCHAD.Clear();
                    pageLoad = true;
                    foreach (DataRow drow in dtPtt.Rows)
                    {
                        row++;
                        setLbLoading("genCHAD " + row + "/" + dtPtt.Rows.Count);
                        String hn = "", hnyear = "", preno = "", vsdate = "";
                        hn = drow["mnc_hn_no"].ToString();
                        hnyear = drow["mnc_hn_yr"].ToString();
                        preno = drow["mnc_pre_no"].ToString();
                        //vsdate = bc.datetoDB(drow["mnc_date"].ToString());
                        DateTime vsdate1 = new DateTime();
                        DateTime.TryParse(drow["mnc_date"].ToString(), out vsdate1);
                        if (vsdate1.Year > 2500)
                        {
                            vsdate1 = vsdate1.AddYears(-543);
                        }
                        else if (vsdate1.Year < 2000)
                        {
                            vsdate1 = vsdate1.AddYears(543);
                        }
                        //vsdate = bc.datetoDB(drow["mnc_date"].ToString());
                        vsdate = vsdate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        DataTable dtchad = bc.bcDB.vsDB.selectPaidByHnOPBKKCHA(hn, hnyear, vsdate, preno);
                        if (dtchad.Rows.Count > 0)
                        {
                            foreach (DataRow drowlabfu in dtchad.Rows)
                            {
                                float sumpri = 0;
                                float.TryParse(drowlabfu["mnc_sum_pri"].ToString(), out sumpri);
                                if (sumpri > 0)
                                {
                                    DateTime date = new DateTime();
                                    DateTime.TryParse(drowlabfu["mnc_doc_dat"].ToString(), out date);
                                    if (date.Year > 2500)
                                    {
                                        date = date.AddYears(-543);
                                    }
                                    else if (date.Year < 2000)
                                    {
                                        date = date.AddYears(543);
                                    }
                                    OPBKKCHAD ins = new OPBKKCHAD();
                                    ins.HN = drow["mnc_hn_no"].ToString();
                                    ins.DATESERV = date.ToString("yyyyMMdd", new CultureInfo("en-US"));
                                    ins.SEQ = drow["mnc_pre_no"].ToString();
                                    ins.CLINIC = "";
                                    ins.ITEMTYPE = drowlabfu["mnc_fn_cd"].ToString();
                                    ins.ITEMCODE = drowlabfu["mnc_fn_cd"].ToString();
                                    ins.ITEMSRC = drowlabfu["mnc_fn_cd"].ToString();
                                    ins.QTY = "";
                                    ins.AMOUNT = drowlabfu["mnc_sum_pri"].ToString();
                                    ins.AMOUNT_EXT = drowlabfu["mnc_sum_pri"].ToString();
                                    ins.PROVIDER = "";
                                    ins.ADDON_DESC = "";
                                    lCHAD.Add(ins);
                                }
                            }
                        }
                    }
                    pageLoad = false;
                }
            }
            hideLbLoading();
        }
        private void genTextINS(String pathfile)
        {
            String fileNameINS1 = pathfile + fileNameINS + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileNameINS1))
            {
                //HN|INSCL|SUBTYPE|CID|DATEIN|DATEEXP|HOSPMAIN|HOSPSUB|GOVCODE|GOVNAME|PERMITNO|DOCNO|OWNRPID|OWNNAME|
                writetext.WriteLine("HN|INSCL|SUBTYPE|CID|DATEIN|DATEEXP|HOSPMAIN|HOSPSUB|GOVCODE|GOVNAME|PERMITNO|DOCNO|OWNRPID|OWNNAME|");
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
            setGrfINS();
        }
        private void genTextPAT(String pathfile)
        {
            String fileNamePAT1 = pathfile + fileNamePAT + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileNamePAT1))
            {
                writetext.WriteLine("HCODE|HN|CHANGWAT|AMPHUR|DOB|SEX|MARRIAGE|OCCUPA|NATION|PERSON_ID|NAMEPAT|TITLE|FNAME|LNAME|IDTYPE|");
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
            setGrfPAT();
        }
        private void genTextOPD(String pathfile)
        {
            String fileNameOPD1 = pathfile + fileNameOPD + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileNameOPD1))
            {
                //PERSON_ID|HN|DATEOPD|TIMEOPD|SEQ|UUC|DETAIL|BTEMP|SBP|DBP|PR|RR|OPTYPE|TYPEIN|TYPEOUT|
                writetext.WriteLine("PERSON_ID|HN|DATEOPD|TIMEOPD|SEQ|UUC|DETAIL|BTEMP|SBP|DBP|PR|RR|OPTYPE|TYPEIN|TYPEOUT|");
                foreach (OPBKKOPD ins in lOPD)
                {
                    String txt = "";
                    //txt = ins.PERSON_ID + separate + ins.HN + separate + ins.DATEOPD + separate + ins.TIMEOPD + separate
                    //    + ins.SEQ + separate + ins.UUC + separate + ins.DETAIL + separate + ins.BTEMP + separate
                    //    + ins.SBP + separate + ins.DBP + separate + ins.PR + separate + ins.RR + separate
                    //    + ins.OPTYPE + separate + ins.TYPEIN + separate + ins.TYPEOUT + separate + ins.CLAIM_CODE + separate;
                    txt = ins.PERSON_ID + separate + ins.HN + separate + ins.DATEOPD + separate + ins.TIMEOPD + separate
                        + ins.SEQ + separate + ins.UUC + separate + ins.DETAIL + separate + ins.BTEMP + separate
                        + ins.SBP + separate + ins.DBP + separate + ins.PR + separate + ins.RR + separate
                        + ins.OPTYPE + separate + ins.TYPEIN + separate + ins.TYPEOUT + separate;
                    writetext.WriteLine(txt);
                }
            }
            setGrfOPD();
        }
        private void genTextORF(String pathfile)
        {
            String fileNameORF1 = pathfile + fileNameORF + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileNameORF1))
            {
                writetext.WriteLine("HN|DATEOPD|CLINIC|REFER|REFERTYPE|REFERDATE|SEQ|");
                foreach (OPBKKORF ins in lORF)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.DATEOPD + separate + ins.CLINIC + separate + ins.REFER + separate
                        + ins.REFERTYPE + separate + ins.REFERDATE + separate + ins.SEQ + separate;
                    writetext.WriteLine(txt);
                }
            }
            setGrfORF();
        }
        private void genTextODX(String pathfile)
        {
            String fileNameODX1 = pathfile + fileNameODX + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileNameODX1))
            {
                writetext.WriteLine("HN|DATEDX|CLINIC|DIAG|DXTYPE|DRDX|PERSON_ID|SEQ|");
                foreach (OPBKKODX ins in lODX)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.DATEDX + separate + ins.CLINIC + separate + ins.DIAG + separate
                        + ins.DXTYPE + separate + ins.DRDX + separate + ins.PERSON_ID + separate + ins.SEQ + separate;
                    writetext.WriteLine(txt);
                }
            }
            setGrfODX();
        }
        private void genTextOOP(String pathfile)
        {
            String fileNameOOP1 = pathfile + fileNameOOP + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileNameOOP1))
            {
                writetext.WriteLine("HN|DATEOPD|CLINIC|OPER|SERVPRICE|DROPID|PERSON_ID|SEQ|");
                foreach (OPBKKOOP ins in lOOP)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.DATEOPD + separate + ins.CLINIC + separate + ins.OPER + separate
                        + ins.SERVPRICE + separate + ins.DROPID + separate + ins.PERSON_ID + separate + ins.SEQ + separate;
                    writetext.WriteLine(txt);
                }
            }
            setGrfOOP();
        }
        private void genTextCHT(String pathfile)
        {
            String fileNameCHT1 = pathfile + fileNameCHT + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileNameCHT1))
            {
                //HN|DATEOPD|ACTUALCHT|TOTAL|PAID|PTTYPE|PERSON_ID|SEQ|OPD_MEMO|INVOICE_NO|INVOICE_LT|
                writetext.WriteLine("HN|DATEOPD|ACTUALCHT|TOTAL|PAID|PTTYPE|PERSON_ID|SEQ|OPD_MEMO|INVOICE_NO|INVOICE_LT|");
                foreach (OPBKKCHT ins in lCHT)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.DATEOPD + separate + ins.ACTUALCHT + separate + ins.TOTAL + separate
                        + ins.PAID + separate + ins.PTTYPE + separate + ins.PERSON_ID + separate + ins.SEQ + separate
                        +ins.OPD_MEMO + separate + ins.INVOICE_NO + separate + ins.INVOICE_LT + separate;
                    writetext.WriteLine(txt);
                }
            }
            setGrfCHT();
        }
        private void genTextCHA(String pathfile)
        {
            String fileNameCHA1 = pathfile + fileNameCHA + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileNameCHA1))
            {
                writetext.WriteLine("HN|DATEOPD|CHRGITEM|AMOUNT|AMOUNT_EXT|PERSON_ID|SEQ|");
                foreach (OPBKKCHA ins in lCHA)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.DATEOPD + separate + ins.CHRGITEM + separate + ins.AMOUNT + separate
                        + ins.AMOUNT_EXT + separate + ins.PERSON_ID + separate + ins.SEQ + separate;
                    writetext.WriteLine(txt);
                }
            }
            setGrfCHA();
        }
        private void genTextAER(String pathfile)
        {
            String fileNameAER1 = pathfile + fileNameAER + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileNameAER1))
            {
                writetext.WriteLine("HN|DATEOPD|AUTHAE|AEDATE|AETIME|AETYPE|REFER_NO|REFMAINI|IREFTYPE|REFMAINO|OREFTYPE|UCAE|EMTYPE|SEQ|");
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
            setGrfAER();
        }
        private void genTextDRU(String pathfile)
        {
            String fileNameDRU1 = pathfile + fileNameDRU + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileNameDRU1))
            {
                writetext.WriteLine("HCODE|HN|CLINIC|PERSON_ID|DATESERV|DID|DIDNAME|DIDSTD|TMTCODE|AMOUNT|DRUGPRICE|PRICE_EXT|DRUGCOST|UNIT|UNIT_PACK|SEQ|PROVIDER|");
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
            setGrfDRU();
        }
        private void genTextLABFU(String pathfile)
        {
            String fileNameLABFU1 = pathfile + fileNameLABFU + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileNameLABFU1))
            {
                //HCODE|HN|PERSON_ID|DATESERV|SEQ|LABTEST|LABRESULT|
                writetext.WriteLine("HCODE|HN|PERSON_ID|DATESERV|SEQ|LABTEST|LABRESULT|");
                foreach (OPBKKLABFU ins in lLABFU)
                {
                    String txt = "";
                    txt = ins.HCODE + separate + ins.HN + separate + ins.PERSON_ID + separate + ins.DATESERV + separate
                        + ins.SEQ + separate + ins.LABTEST + separate + ins.LABRESULT + separate;
                    writetext.WriteLine(txt);
                }
            }
            setGrfLABFU();
        }
        private void genTextCHAD(String pathfile)
        {
            String fileNameCHAD1 = pathfile + fileNameCHAD + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileNameCHAD1))
            {
                writetext.WriteLine("HN|DATESERVE|SEQ|CLINIC|ITEMTYPE|ITEMCODE|QTY|AMOUNT|AMOUNT_EXT|PROVIDER|ADDON_DETAIL|");
                foreach (OPBKKCHAD ins in lCHAD)
                {
                    String txt = "";
                    //txt = ins.HN + separate + ins.DATESERV + separate + ins.SEQ + separate + ins.CLINIC + separate
                    //    + ins.ITEMTYPE + separate + ins.ITEMCODE + separate + ins.ITEMSRC + separate + ins.QTY + separate
                    //    + ins.AMOUNT + separate + ins.AMOUNT_EXT + separate + ins.PROVIDER + separate + ins.ADDON_DESC + separate;
                    txt = ins.HN + separate + ins.DATESERV + separate + ins.SEQ + separate + ins.CLINIC + separate
                        + ins.ITEMTYPE + separate + ins.ITEMCODE + separate + ins.QTY + separate
                        + ins.AMOUNT + separate + ins.AMOUNT_EXT + separate + ins.PROVIDER + separate + ins.ADDON_DESC + separate;
                    writetext.WriteLine(txt);
                }
            }
            setGrfCHAD();
        }
        private void FrmOPBKKClaim_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            int scrH = Screen.PrimaryScreen.Bounds.Height;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Maximized;
            pnUcepMainTop.Size = new Size(scrW, 80);

            grfSelect.Size = new Size(scrW -20, scrH - btnOPBKKSelect.Location.Y -140);
            grfSelect.Location = new Point(5, btnOPBKKSelect.Location.Y + 40);

            pnOPBKK.Location = new Point(5, btnOPBKKSelect.Location.Y + 40);
            pnOPBKK.Size = new Size(scrW - 20, scrH - btnOPBKKSelect.Location.Y - 160);

            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();

            //  แก้เรื่อง ข้อมูลที่ double

            //this.Text = "Last Update 2020-04-09";
            this.Text = "Last Update 2023-04-12 ucep lab xray drug IPD ให้ดึงตามใบยา";
        }
    }
}
