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

        C1DockingTab tcMain, tcOP, tcOPBKK, tcUcep, tcSSO;
        C1DockingTabPage tabOP,tabOPBkk, tabOPBKKPtt, tabOPBKKINS, tabOPBKKPAT, tabOPBKKOPD, tabOPBKKODX, tabOPBKKOOP, tabOPBKKORF, tabOPBKKCHT, tabOPBKKCHA, tabOPBKKAER, tabOPBKKLABFU, tabOPBKKDRU, tabOPBKKCHAD, tabOrd;
        C1DockingTabPage tabtcUcep, tabSSO, tabSSO1, tabUcepMain, tabUcepDrug, tabUcepCat;
        C1DockingTabPage tabOPBKKMainPaidTyp, tabOPBKKMainClinic, tabOPBKKMainCHRGITEM;
        C1FlexGrid grfSelect, grfINS, grfPAT, grfOPD, grfODX, grfOOP, grfORF, grfCHT, grfCHA, grfAER, grfLABFU, grfDRU, grfCHAD, grfOrd, grfPaidTyp, grfClinic, grfOPBKKMainCHRGITEM;
        C1FlexGrid grfUcepCat, grfUcepDrug, grfUcepSelect;

        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1PdfDocument pdfDoc;
        C1ThemeController theme1;
        Label lbDateStart, lbDateEnd, lbtxtPaidType, lbLoading, lbtxtHospMain, lbtxtHCode, lbtxtUcepDateStart, lbtxtUcepDateEnd, lbtxtUcepPaidType;
        C1DateEdit txtDateStart, txtDateEnd, txtUcepDateStart, txtUcepDateEnd;
        C1Button btnOPBKKSelect, btnOPBkkGen, btnUcepSelect, btnUcepTMTImport, btnUcepExcel;
        C1ComboBox cboPaidType;
        C1TextBox txtPaidType, txtHospMain, txtHCode, txtUcepPaidType;
        C1CheckBox chkUcepSelectAll;
        Panel pnOPBKK, pnUcepMainTop, pnUcepMainBotton;
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
        int colPaidTypId = 1, colPaidTypName = 2, colPaidTypIdOpBkkCode = 3, colPaidTypFNSYS=4, colPaidTypPTTYP=5, colPaidTypAccNo=6, colPaidTypflag=7;
        int colClinicmddepno = 1, colClinicsecno = 2, colClinicdivno = 3, colClinictyppt = 4, colClinicdepdsc = 5, colClinicdpno = 7, colClinicopbkkcode = 6, colClinicopbkkFlag=7;
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
        int colGrfUcepDrugDrugId = 1, colGrfUcepDrugDrugName = 2, colGrfUcepDrugOldCode=3, colGrfUcepDrugTmtCode = 4, colGrfUcepDrugFlag=5;
        int colGrfUcepPaidId = 1, colGrfUcepPaidName = 2, colGrfUcepPaidCatCode = 3, colGrfUcepPaidFalg = 4;
        int colGrfUcepSelectSelect=1, colGrfUcepSelectHn = 2, colGrfUcepSelectName = 3, colGrfUcepSelectVsDate = 4, colGrfUcepSelectPaidType = 5, colGrfUcepSelectSymptoms = 6, colGrfUcepSelectHnYear = 7, colGrfUcepSelectPreno = 8, colGrfUcepSelectStatusOPDIPD=9, colGrfUcepSelectAnNoShow=10, colGrfUcepSelectAnNo=11, colGrfUcepSelectAnYear=12, colGrfUcepSelectAnDate=13;

        Boolean pageLoad = false;
        String pathfile = "", fileNameINS = "INS", separate = "|", fileNamePAT = "PAT", fileNameOPD = "OPD", fileNameORF = "ORF", fileNameODX = "ODX", fileNameOOP = "OOP", fileNameCHT = "CHT";
        String fileNameCHA = "CHA", fileNameAER = "AER", fileNameDRU = "DRU", fileNameLABFU = "LABFU", fileNameCHAD = "CHAD";

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
            grfPaidTyp.CellChanged += GrfPaidTyp_CellChanged;
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
            grfClinic.CellChanged += GrfClinic_CellChanged;
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
            grfOPBKKMainCHRGITEM.CellChanged += GrfOPBKKMainCHRGITEM_CellChanged;
            tabOPBKKMainCHRGITEM.Controls.Add(grfOPBKKMainCHRGITEM);
            theme1.SetTheme(grfOPBKKMainCHRGITEM, "Office2010Red");
            ContextMenu menuGwCHRGITEM = new ContextMenu();
            menuGwCHRGITEM.MenuItems.Add("save CHRGITEM code", new EventHandler(ContextMenu_grfCHRGITEM_save));
            grfOPBKKMainCHRGITEM.ContextMenu = menuGwCHRGITEM;

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

            tcUcep.Controls.Add(tabUcepMain);
            tcUcep.Controls.Add(tabUcepDrug);
            tcUcep.Controls.Add(tabUcepCat);

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



            initCompomentTabUcep();
            //gapY += gapLine;
            bc.setControlLabel(ref lbDateStart, fEdit, "วันที่เริ่มต้น :", "lbDateStart", gapX , gapY);
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
            bc.setControlLabel(ref lbtxtHospMain, fEdit, "HospMain :", "lbtxtHospMain", txtPaidType.Location.X + txtPaidType.Width + 20, gapY);
            size = bc.MeasureString(lbtxtHospMain);
            bc.setControlC1TextBox(ref txtHospMain, fEdit, "txtHospMain", 80, lbtxtHospMain.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtHCode, fEdit, "HCODE :", "lbtxtHCode", txtHospMain.Location.X + txtHospMain.Width + 15, gapY);
            size = bc.MeasureString(lbtxtHCode);
            bc.setControlC1TextBox(ref txtHCode, fEdit, "txtHCode", 80, lbtxtHCode.Location.X + size.Width + 5, gapY);

            bc.setControlC1Button(ref btnOPBKKSelect, fEdit, "ดึงข้อมูล", "btnOPBKKSelect", txtHCode.Location.X + txtHCode.Width + 20, gapY);
            btnOPBKKSelect.Width = 70;
            bc.setControlC1Button(ref btnOPBkkGen, fEdit, "gen Text", "btnOPBkkGen", btnOPBKKSelect.Location.X + btnOPBKKSelect.Width + 20, gapY);
            btnOPBkkGen.Width = 80;
            btnOPBKKSelect.Click += BtnOPBKKSelect_Click;

            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);

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
            pnUcepMainTop.Dock = DockStyle.Top;
            pnUcepMainBotton.Dock = DockStyle.Fill;
            //pnUcepMainTop.BackColor = Color.Red;
            //pnUcepMainBotton.BackColor = Color.Blue;
            tabUcepMain.Controls.Add(pnUcepMainBotton);
            tabUcepMain.Controls.Add(pnUcepMainTop);

            btnUcepSelect = new C1Button();
            btnUcepTMTImport = new C1Button();
            btnUcepExcel = new C1Button();

            lbtxtUcepDateStart = new Label();
            txtUcepDateStart = new C1DateEdit();
            lbtxtUcepDateEnd = new Label();
            txtUcepDateEnd = new C1DateEdit();
            lbtxtUcepPaidType = new Label();
            txtUcepPaidType = new C1TextBox();
            chkUcepSelectAll = new C1CheckBox();

            bc.setControlLabel(ref lbtxtUcepDateStart, fEdit, "วันที่เริ่มต้น :", "lbtxtUcepDateStart", gapX, gapY);
            size = bc.MeasureString(lbtxtUcepDateStart);
            bc.setControlC1DateTimeEdit(ref txtUcepDateStart, "txtUcepDateStart", lbtxtUcepDateStart.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtUcepDateEnd, fEdit, "วันที่สิ้นสุด :", "lbtxtUcepDateEnd", txtUcepDateStart.Location.X + txtUcepDateStart.Width + 15, gapY);
            size = bc.MeasureString(lbtxtUcepDateEnd);
            bc.setControlC1DateTimeEdit(ref txtUcepDateEnd, "txtUcepDateEnd", lbtxtUcepDateEnd.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbtxtUcepPaidType, fEdit, "สิทธิ :", "lbtxtUcepPaidType", txtUcepDateEnd.Location.X+ txtUcepDateEnd.Width + 20, gapY);
            size = bc.MeasureString(lbtxtUcepPaidType);
            bc.setControlC1TextBox(ref txtUcepPaidType, fEdit, "", 120, lbtxtUcepPaidType.Location.X + size.Width + 5, gapY);

            //gapY += gapLine;
            bc.setControlC1Button(ref btnUcepSelect, fEdit, "ดึงข้อมูล", "btnUcepOk", txtUcepPaidType.Location.X+ txtUcepPaidType.Width+30, gapY);
            btnUcepSelect.Width = 70;
            bc.setControlC1CheckBox(ref chkUcepSelectAll, fEdit, "select All", "chkUcepSelectAll", btnUcepSelect.Location.X + btnUcepSelect.Width + 30, gapY);
            bc.setControlC1Button(ref btnUcepExcel, fEdit, "Export excel", "btnUcepExcel", chkUcepSelectAll.Location.X + chkUcepSelectAll.Width + 30, gapY);
            btnUcepExcel.Width = 110;
            bc.setControlC1Button(ref btnUcepTMTImport, fEdit, "Import TMT", "btnUcepTMTImport", btnUcepExcel.Location.X + btnUcepExcel.Width + 30, gapY);
            btnUcepTMTImport.Width = 100;

            initGrfUcepSelect();

            btnUcepSelect.Click += BtnUcepSelect_Click;
            btnUcepTMTImport.Click += BtnUcepTMTImport_Click;
            btnUcepExcel.Click += BtnUcepExcel_Click;
            chkUcepSelectAll.Click += ChkUcepSelectAll_Click;

            pnUcepMainTop.Controls.Add(lbtxtUcepPaidType);
            pnUcepMainTop.Controls.Add(txtUcepPaidType);
            pnUcepMainTop.Controls.Add(lbtxtUcepDateStart);
            pnUcepMainTop.Controls.Add(txtUcepDateStart);
            pnUcepMainTop.Controls.Add(lbtxtUcepDateEnd);
            pnUcepMainTop.Controls.Add(txtUcepDateEnd);
            pnUcepMainTop.Controls.Add(btnUcepSelect);
            pnUcepMainTop.Controls.Add(chkUcepSelectAll);
            pnUcepMainTop.Controls.Add(btnUcepTMTImport);
            pnUcepMainTop.Controls.Add(btnUcepExcel);

            theme1.SetTheme(pnUcepMainTop, bc.iniC.themeApp);
            //theme1.SetTheme(pnUcepMainBotton, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtUcepDateStart, bc.iniC.themeApp);
            theme1.SetTheme(txtUcepDateStart, bc.iniC.themeApp);
            theme1.SetTheme(lbtxtUcepDateEnd, bc.iniC.themeApp);
            theme1.SetTheme(txtUcepDateEnd, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepSelect, bc.iniC.themeApp);
            theme1.SetTheme(chkUcepSelectAll, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepTMTImport, bc.iniC.themeApp);
            theme1.SetTheme(btnUcepExcel, bc.iniC.themeApp);
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
            exportUcelExcel();
        }
        private void exportUcelImportTMTcode()
        {
            C1XLBook _c1xl = new C1XLBook();

            OpenFileDialog openFileDialog1;
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _c1xl.Load(openFileDialog1.FileName);
                var sheet = _c1xl.Sheets[0];
                int row = sheet.Rows.Count;
                for (int i = 0; i < row; i++)
                {
                    String drugcode = "", tmtcode = "";
                    drugcode = sheet[i, 0].Value.ToString();
                    tmtcode = sheet[i, 2].Value.ToString();
                    String re = bc.bcDB.pharM01DB.UpdateTMTCode(drugcode, tmtcode);
                }
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
            DateTime.TryParse(txtUcepDateStart.Text, out dtstart);
            DateTime.TryParse(txtUcepDateEnd.Text, out dtend);

            datestart = dtstart.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            dateend = dtend.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
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
            dtUcepPtt = bc.bcDB.vsDB.selectVisitByDatePaidType(paidtype, datestart, dateend);

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
                    if (row[colGrfUcepPaidFalg] == null) continue;
                    if (row[colGrfUcepPaidFalg].ToString().Equals("1"))
                    {
                        String id = "", tmtcode = "", divno = "", typpt = "", opbkkcode = "";
                        id = row[colGrfUcepPaidId].ToString();
                        tmtcode = row[colGrfUcepPaidCatCode].ToString();



                    }
                }
            }
            catch (Exception ex)
            {

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
                        id = row[colGrfUcepDrugDrugId].ToString();
                        tmtcode = row[colGrfUcepDrugTmtCode].ToString();
                        
                        

                    }
                }
            }
            catch (Exception ex)
            {

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

            }
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

        private void initGrfUcepCat()
        {
            grfUcepCat = new C1FlexGrid();
            grfUcepCat.Font = fEdit;
            grfUcepCat.Dock = System.Windows.Forms.DockStyle.Fill;
            grfUcepCat.Location = new System.Drawing.Point(0, 0);
            grfUcepCat.Rows.Count = 1;
            grfUcepCat.CellChanged += GrfUcepCat_CellChanged;

            ContextMenu menuGwPAT = new ContextMenu();
            menuGwPAT.MenuItems.Add("save Category 1-16", new EventHandler(ContextMenu_grfUcepCat_save));
            grfUcepCat.ContextMenu = menuGwPAT;
            grfUcepCat.CellChanged += GrfUcepCat_CellChanged;
            grfUcepCat.AfterFilter += GrfUcepCat_AfterFilter;

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
            grfUcepCat[e.Row, colGrfUcepPaidFalg] = "1";
        }

        private void BtnOPBKKSelect_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            dtPtt = new DataTable();
            String datestart = "", dateend = "", paidtype="";
            DateTime dtstart = new DateTime();
            DateTime dtend = new DateTime();
            DateTime.TryParse(txtDateStart.Text, out dtstart);
            DateTime.TryParse(txtDateEnd.Text, out dtend);

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
            txtUcepDateStart.Value = DateTime.Now;
            txtUcepDateEnd.Value = DateTime.Now;
            setGrfPaidType();
            setGrfClinic();
            setGrfUcepDrug();
            setGrfUcepCat();
            setGrfOPBKKMainCHRGITEM();
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

        private void ContextMenu_grfUcepSelect_ExportExcel(object sender, System.EventArgs e)
        {
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
            int row1 = 0;
            foreach (Row row in grfUcepSelect.Rows)
            {
                row1++;
                setLbLoading("Ucep gen  " + row1 + "/" + grfUcepSelect.Rows.Count);
                if (row[colGrfUcepSelectHn] == null) continue;
                if (row[colGrfUcepSelectSelect] == null) continue;
                if ((Boolean)row[colGrfUcepSelectSelect])
                {
                    String hn = "", hnyear = "", preno = "", vsdate = "", an="", anyear="", andate="", statusipd="";
                    hn = row[colGrfUcepSelectHn].ToString();
                    hnyear = row[colGrfUcepSelectHnYear].ToString();
                    preno = row[colGrfUcepSelectPreno].ToString();
                    vsdate = row[colGrfUcepSelectVsDate].ToString();
                    statusipd = row[colGrfUcepSelectStatusOPDIPD].ToString();
                    an = row[colGrfUcepSelectAnNo].ToString();
                    anyear = row[colGrfUcepSelectAnYear].ToString();
                    andate = bc.datetoDB(row[colGrfUcepSelectAnDate].ToString());
                    if (statusipd.Equals("OPD"))
                    {
                        DateTime vsdate1 = new DateTime();
                        if (DateTime.TryParse(vsdate, out vsdate1))
                        {
                            DataTable dtdrug = bc.bcDB.vsDB.selectDrugUcepByHn(hn, hnyear, vsdate, preno);
                            foreach (DataRow rowdrug in dtdrug.Rows)
                            {
                                String reqtime = "0000" + rowdrug["MNC_REQ_TIM"].ToString();
                                reqtime = reqtime.Substring(reqtime.Length - 4);
                                reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                                DataRow rowucep = dtUcepGen.Rows.Add();
                                rowucep["use_date"] = rowdrug["mnc_req_dat"].ToString() + " " + reqtime;
                                rowucep["tmt_code"] = rowdrug["tmt_code"].ToString();
                                rowucep["hosp_code"] = "";
                                rowucep["cat_1_16"] = rowdrug["mnc_grp_ss1"].ToString();
                                rowucep["mean"] = rowdrug["MNC_PH_TN"].ToString();
                                rowucep["unit"] = rowdrug["qty"].ToString();
                                rowucep["price_total"] = "";
                            }
                        }
                    }
                    else
                    {       //IPD
                        DateTime andate1 = new DateTime();
                        if (DateTime.TryParse(andate, out andate1))
                        {
                            DataTable dtdrug = bc.bcDB.vsDB.selectOrderDrugUcepIPDByHn(hn, hnyear, an, anyear);
                            foreach (DataRow rowdrug in dtdrug.Rows)
                            {
                                String reqtime = "0000" + rowdrug["MNC_REQ_TIM"].ToString();
                                reqtime = reqtime.Substring(reqtime.Length - 4);
                                reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                                DataRow rowucep = dtUcepGen.Rows.Add();
                                rowucep["use_date"] = rowdrug["mnc_req_dat"].ToString() + " " + reqtime;
                                rowucep["tmt_code"] = rowdrug["tmt_code"].ToString();
                                rowucep["hosp_code"] = "";
                                rowucep["cat_1_16"] = rowdrug["mnc_grp_ss1"].ToString();
                                rowucep["mean"] = rowdrug["MNC_PH_TN"].ToString();
                                rowucep["unit"] = rowdrug["qty"].ToString();
                                rowucep["price_total"] = "";
                            }
                            DataTable dtorder = bc.bcDB.vsDB.selectOrderHotChargeUcepIPDByHn(hn, hnyear, andate, an, anyear);
                            foreach (DataRow rowdrug in dtorder.Rows)
                            {
                                String reqtime = "0000" + rowdrug["mnc_stamp_tim"].ToString();
                                reqtime = reqtime.Substring(reqtime.Length - 4);
                                reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                                DataRow rowucep = dtUcepGen.Rows.Add();
                                rowucep["use_date"] = rowdrug["mnc_req_dat"].ToString() + " " + reqtime;
                                rowucep["tmt_code"] = rowdrug["mnc_sr_cd"].ToString();
                                rowucep["hosp_code"] = "";
                                rowucep["cat_1_16"] = "";
                                rowucep["mean"] = rowdrug["MNC_SR_DSC"].ToString();
                                rowucep["unit"] = rowdrug["qty"].ToString();
                                rowucep["price_total"] = "";
                            }
                        }
                    }
                    
                }
            }
            hideLbLoading();
            CreateExcelFile(dtUcepGen);
            pageLoad = false;
        }
        private string CreateExcelFile(DataTable dt)
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
            datetick = DateTime.Now.Ticks.ToString();
            pathfile = pathfile + "\\" + datetick + "\\";
            if (!Directory.Exists(pathfile))
            {
                Directory.CreateDirectory(pathfile);
                Application.DoEvents();
            }

            string filename = pathfile+"\\ucep.xls";
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
            sheet.Columns[0].Width = 3000;
            sheet.Columns[1].Width = 3000;
            sheet.Columns[2].Width = 3000;
            sheet.Columns[3].Width = 3000;
            sheet.Columns[4].Width = 3000;
            sheet.Columns[5].Width = 3000;
            sheet.Columns[6].Width = 3000;

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
            grfUcepCat.Cols.Count = 5;
            grfUcepCat.Rows.Count = dt.Rows.Count + 2;
            grfUcepCat.Cols[colGrfUcepPaidId].Caption = "ID";
            grfUcepCat.Cols[colGrfUcepPaidName].Caption = "Paid Name";
            grfUcepCat.Cols[colGrfUcepPaidCatCode].Caption = "CAT 1-16";
            
            grfUcepCat.Cols[colGrfUcepPaidId].Width = 80;
            grfUcepCat.Cols[colGrfUcepPaidName].Width = 500;
            grfUcepCat.Cols[colGrfUcepPaidCatCode].Width = 120;

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
                grfUcepCat[i, colGrfUcepPaidFalg] = "0";
                grfUcepCat[i, 0] = (i-1);
                if (i % 2 == 0)
                    grfUcepCat.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);

            }
            grfUcepCat.Cols[colGrfUcepPaidId].AllowEditing = false;
            grfUcepCat.Cols[colGrfUcepPaidName].AllowEditing = false;
            grfUcepCat.Cols[colGrfUcepDrugOldCode].AllowEditing = false;
            grfUcepCat.AllowFiltering = true;
            //for (int col = 0; col < dt.Columns.Count; ++col)
            //{
            grfUcepCat.Cols[colGrfUcepPaidId].DataType = dt.Columns["MNC_FN_CD"].DataType;
            //grfOrdDrug.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
            grfUcepCat.Cols[colGrfUcepPaidId].Name = dt.Columns["MNC_FN_CD"].ColumnName;
            grfUcepCat.Cols[colGrfUcepPaidName].DataType = dt.Columns["MNC_FN_DSCT"].DataType;
            grfUcepCat.Cols[colGrfUcepPaidName].Name = dt.Columns["MNC_FN_DSCT"].ColumnName;
            //}
            pageLoad = false;
        }
        private void setGrfUcepDrug()
        {
            pageLoad = true;
            DataTable dt = new DataTable();
            //C1ComboBox cboMethod = new C1ComboBox();
            //cboMethod.AutoCompleteMode = AutoCompleteMode.Suggest;
            //cboMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
            //bc.setCboOPBKKINSCL(cboMethod, "");

            dt = bc.bcDB.pharM01DB.SelectAll();
            grfUcepDrug.Rows.Count = 1;
            grfUcepDrug.Cols.Count = 6;
            grfUcepDrug.Rows.Count = dt.Rows.Count + 2;
            grfUcepDrug.Cols[colGrfUcepDrugDrugId].Caption = "ID";
            grfUcepDrug.Cols[colGrfUcepDrugDrugName].Caption = "Drug Name";
            grfUcepDrug.Cols[colGrfUcepDrugTmtCode].Caption = "TMT code";
            grfUcepDrug.Cols[colGrfUcepDrugOldCode].Caption = "OLD Code";
            grfUcepDrug.Cols[0].Width = 50;
            grfUcepDrug.Cols[colGrfUcepDrugDrugId].Width = 90;
            grfUcepDrug.Cols[colGrfUcepDrugDrugName].Width = 500;
            grfUcepDrug.Cols[colGrfUcepDrugTmtCode].Width = 120;
            grfUcepDrug.Cols[colGrfUcepDrugOldCode].Width = 100;
            
            //grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Editor = cboMethod;
            int i = 1;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                String inscl = "";
                grfUcepDrug[i, colGrfUcepDrugDrugId] = row1["MNC_ph_CD"].ToString();
                grfUcepDrug[i, colGrfUcepDrugDrugName] = row1["MNC_PH_TN"].ToString();
                grfUcepDrug[i, colGrfUcepDrugTmtCode] = row1["tmt_code"].ToString(); ;
                grfUcepDrug[i, colGrfUcepDrugOldCode] = row1["MNC_OLD_CD"].ToString();
                grfUcepDrug[i, colGrfUcepDrugFlag] = "0";
                grfUcepDrug[i, 0] = (i-1);
                if (i % 2 == 0)
                    grfUcepDrug.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);

            }
            grfUcepDrug.Cols[colGrfUcepDrugDrugId].AllowEditing = false;
            grfUcepDrug.Cols[colGrfUcepDrugDrugName].AllowEditing = false;
            grfUcepDrug.Cols[colGrfUcepDrugOldCode].AllowEditing = false;
            grfUcepDrug.AllowFiltering = true;

            grfUcepDrug.Cols[colGrfUcepDrugDrugId].DataType = dt.Columns["MNC_ph_CD"].DataType;
            grfUcepDrug.Cols[colGrfUcepDrugDrugId].Name = dt.Columns["MNC_ph_CD"].ColumnName;
            grfUcepDrug.Cols[colGrfUcepDrugDrugName].DataType = dt.Columns["MNC_PH_TN"].DataType;
            grfUcepDrug.Cols[colGrfUcepDrugDrugName].Name = dt.Columns["MNC_PH_TN"].ColumnName;
            grfUcepDrug.Cols[colGrfUcepDrugTmtCode].DataType = dt.Columns["tmt_code"].DataType;
            grfUcepDrug.Cols[colGrfUcepDrugTmtCode].Name = dt.Columns["tmt_code"].ColumnName;
            grfUcepDrug.Cols[colGrfUcepDrugOldCode].DataType = dt.Columns["MNC_OLD_CD"].DataType;
            grfUcepDrug.Cols[colGrfUcepDrugOldCode].Name = dt.Columns["MNC_OLD_CD"].ColumnName;

            pageLoad = false;
        }
        private void setGrfPaidType()
        {
            pageLoad = true;
            DataTable dt = new DataTable();
            C1ComboBox cboMethod = new C1ComboBox();
            cboMethod.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
            bc.setCboOPBKKINSCL(cboMethod, "");

            dt = bc.bcDB.finM02DB.SelectAll();
            grfPaidTyp.Rows.Count = 1;
            grfPaidTyp.Cols.Count = 8;
            grfPaidTyp.Rows.Count = dt.Rows.Count + 1;
            grfPaidTyp.Cols[colPaidTypId].Caption = "ID";
            grfPaidTyp.Cols[colPaidTypName].Caption = "Paid Name";
            grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Caption = "OPBKK Code";
            grfPaidTyp.Cols[colPaidTypFNSYS].Caption = "FN SYS";
            grfPaidTyp.Cols[colPaidTypPTTYP].Caption = "PT TYP";
            grfPaidTyp.Cols[colPaidTypAccNo].Caption = "ACC NO";
            grfPaidTyp.Cols[colPaidTypId].Width = 60;
            grfPaidTyp.Cols[colPaidTypName].Width = 300;
            grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Width = 300;
            grfPaidTyp.Cols[colPaidTypFNSYS].Width = 80;
            grfPaidTyp.Cols[colPaidTypPTTYP].Width = 80;
            grfPaidTyp.Cols[colPaidTypAccNo].Width = 80;
            grfPaidTyp.Cols[colPaidTypIdOpBkkCode].Editor = cboMethod;
            int i = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                String inscl = "";
                bc.opBKKINSCL.TryGetValue(row1["opbkk_inscl"].ToString(), out inscl);
                grfPaidTyp[i, colPaidTypId] = row1["MNC_FN_TYP_CD"].ToString();
                grfPaidTyp[i, colPaidTypName] = row1["MNC_FN_TYP_DSC"].ToString();
                grfPaidTyp[i, colPaidTypIdOpBkkCode] = inscl;
                grfPaidTyp[i, colPaidTypFNSYS] = row1["MNC_FN_STS"].ToString();
                grfPaidTyp[i, colPaidTypPTTYP] = row1["PTTYP"].ToString();
                grfPaidTyp[i, colPaidTypAccNo] = row1["MNC_ACCOUNT_NO"].ToString();
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
            grfClinic.Cols.Count = 8;
            grfClinic.Rows.Count = dt.Rows.Count + 1;
            grfClinic.Cols[colClinicmddepno].Caption = "DEP NO";
            grfClinic.Cols[colClinicsecno].Caption = "SEC NO";
            grfClinic.Cols[colClinicdivno].Caption = "DIV NO";
            grfClinic.Cols[colClinictyppt].Caption = "TYP PT";
            grfClinic.Cols[colClinicdepdsc].Caption = "DEP DSC";
            grfClinic.Cols[colClinicdpno].Caption = "DP NO";
            grfClinic.Cols[colClinicopbkkcode].Caption = "OP BKK Clinic";
            grfClinic.Cols[colClinicmddepno].Width = 60;
            grfClinic.Cols[colClinicsecno].Width = 60;
            grfClinic.Cols[colClinicdivno].Width = 60;
            grfClinic.Cols[colClinictyppt].Width = 60;
            grfClinic.Cols[colClinicdepdsc].Width = 300;
            grfClinic.Cols[colClinicdpno].Width = 80;
            grfClinic.Cols[colClinicopbkkcode].Width = 300;
            grfClinic.Cols[colClinicopbkkcode].Editor = cboMethod;
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
                            pat.NATION = drow["MNC_NAT_CD"] != null ? drow["MNC_NAT_CD"].ToString() : "";
                            pat.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                            pat.NAMEPAT = drow["prefix"].ToString() + " " + drow["MNC_FNAME_T"].ToString() + " " + drow["MNC_LNAME_T"].ToString();
                            pat.TITLE = drow["mnc_pfix_cdt"] != null ? drow["mnc_pfix_cdt"].ToString() : "";
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
                        String sbp = "", dbp;
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
                        OPBKKOPD opd = new OPBKKOPD();
                        opd.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                        opd.HN = drow["mnc_hn_no"].ToString();
                        opd.DATEOPD = date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        opd.TIMEOPD = ("0000" + drow["MNC_time"].ToString()).Substring(4);
                        opd.SEQ = drow["mnc_pre_no"].ToString();
                        opd.UUC = "1";                                                       //      การใช้สิทธิ (เพิ่มเติม) 1 = ใช้สิทธิ 2 = ไมใช้สิทธ
                        opd.DETAIL = drow["MNC_SHIF_MEMO"].ToString();
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
                        String hn = "", hnyear = "", preno = "", vsdate = "";
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
                        //vsdate = bc.datetoDB(drow["mnc_date"].ToString());
                        vsdate = vsdate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        new LogWriter("d", "FrmOPBKKClaim genODX vsdate " + vsdate);
                        DataTable dtlab = bc.bcDB.vsDB.selectICD10ByHn(hn, hnyear, vsdate, preno);
                        if (dtlab.Rows.Count > 0)
                        {
                            foreach (DataRow drowodx in dtlab.Rows)
                            {
                                if (drowodx["mnc_dia_cd"].ToString().Length > 0)
                                {
                                    DateTime actdate = new DateTime();
                                    new LogWriter("d", "FrmOPBKKClaim genODX drowodx ");
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
                                    ins.DATEDX = actdate.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                                    ins.CLINIC = "";
                                    ins.DIAG = drowodx["MNC_dia_cd"].ToString();
                                    ins.DXTYPE = drowodx["MNC_dia_flg"].ToString();
                                    ins.DRDX = drowodx["MNC_dot_cd"].ToString();
                                    ins.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                                    ins.SEQ = drow["mnc_pre_no"].ToString();
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
                                    ins.SEQ = drow["mnc_pre_no"].ToString();
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
                                    ins.DATEOPD = date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                                    ins.ACTUALCHT = drowlabfu["mnc_sum_pri"].ToString();
                                    ins.TOTAL = drowlabfu["mnc_sum_pri"].ToString();
                                    ins.PAID = drowlabfu["mnc_sum_pri"].ToString();
                                    ins.PTTYPE = "";
                                    ins.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                                    ins.SEQ = drow["mnc_pre_no"].ToString();
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
                                    ins.DATEOPD = date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                                    ins.CHRGITEM = "";
                                    ins.AMOUNT = drowlabfu["mnc_sum_pri"].ToString();
                                    ins.AMOUNT_EXT = drowlabfu["mnc_sum_pri"].ToString();
                                    ins.PERSON_ID = drow["MNC_ID_NO"] != null ? drow["MNC_ID_NO"].ToString() : "";
                                    ins.SEQ = drow["mnc_pre_no"].ToString();
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
                    foreach (DataRow drow in dtPtt.Rows)
                    {
                        row++;
                        setLbLoading("genDRU "+row +"/"+dtPtt.Rows.Count);
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
                                OPBKKDRU ins = new OPBKKDRU();
                                ins.HCODE = txtHCode.Text.Trim();
                                ins.HN = drow["mnc_hn_no"].ToString();
                                ins.CLINIC = "";
                                ins.PERSON_ID = drow["MNC_ID_Nam"] != null ? drow["MNC_ID_Nam"].ToString() : "";
                                ins.DATESERV = date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                                ins.DID = drowdrug["MNC_PH_CD"].ToString();
                                ins.DIDNAME = drowdrug["MNC_PH_TN"].ToString();
                                ins.DIDSTD = "";
                                ins.TMTCODE = drowdrug["tmt_code"].ToString();
                                ins.AMOUNT = drowdrug["qty"].ToString();        //จำนวนยาที่จ่าย
                                ins.DRUGPRICE = "";         // ราคายาที่ขอเบิก (ไม่ใช่ราคาต่อหน่วย)
                                ins.PRICE_EXT = "";         // ราคายาส่วนที่เบิกไม่ได้  (ไม่ใช่ราคาต่อหน่วย)
                                ins.DRUGCOST = "";
                                ins.UNIT = drowdrug["mnc_ph_unt_cd"].ToString();
                                ins.UNIT_PACK = "";
                                ins.SEQ = drow["mnc_pre_no"].ToString();
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
                                ins.DATESERV = date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                                ins.SEQ = drow["mnc_pre_no"].ToString();
                                ins.LABTEST = drowlabfu["MNC_RES"].ToString();
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
                                    ins.DATESERV = date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
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
            setGrfOPD();
        }
        private void genTextORF(String pathfile)
        {
            String fileNameORF1 = pathfile + fileNameORF + ".txt";
            using (StreamWriter writetext = new StreamWriter(fileNameORF1))
            {
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
                foreach (OPBKKCHAD ins in lCHAD)
                {
                    String txt = "";
                    txt = ins.HN + separate + ins.DATESERV + separate + ins.SEQ + separate + ins.CLINIC + separate
                        + ins.ITEMTYPE + separate + ins.ITEMCODE + separate + ins.ITEMSRC + separate + ins.QTY + separate
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

            this.Text = "Last Update 2020-01-25";
        }
    }
}
