using AutocompleteMenuNS;
using bangna_hospital.control;
using bangna_hospital.FlexGrid;
using bangna_hospital.Models;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.C1Excel;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using C1.Win.C1Tile;
using C1.Win.ImportServices.ReportingService4;
using GrapeCity.Viewer.Common.Model;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static CSJ2K.j2k.codestream.HeaderInfo;

namespace bangna_hospital.gui
{
    /*
     * ในหน้าจอนี้
     * จะมีการ set Control อยู่ 2 Method
     * setControl
     * setControlPatientByGrf
     * ควรทำ ให้ 2 Method มีการทำงานที่เหมือนกัน
     */
    public partial class FrmReception : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famtB, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, fque, fqueB;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        Patient ptt;
        C1FlexGrid grfSrc, grfPttComp, grfPttVs, grfPttApm, grfRptReport, grfRptData, grfCust1, grfVsPttVisit, grfToday, grfApm, grfWard, grfSSO;
        C1ThemeController theme1;
        //ThaiNationalIDCardReader idcard;
        PersonalPhoto PHOTO;
        Label lbLoading;

        int colgrfSrcHn = 1, colgrfSrcFullNameT = 2, colgrfSrcPID = 3, colgrfSrcDOB = 4, colgrfSrcPttid=5, colgrfSrcAge=6, colgrfSrcVisitReleaseOPD=7, colgrfSrcVisitReleaseIPD = 8,colgrfSrcVisitReleaseIPDDischarge=9;
        int celgrfPttCompCode = 1, colgrfPttCompNameT = 2, colgrfPttCompid = 3;
        int colgrfPttVsStatusVisit=1 ,colgrfPttVsVsDateShow = 2, colgrfPttVsVsTime=3,colgrfPttVsDiscDateShow = 4, colgrfPttVsDiscTime=5, colgrfPttVsHn=6, colgrfPttVsFullNameT=7, colgrfPttVsPaid=8, colgrfPttVsPreno=9, colgrfPttVsDept=10, colgrfPttVsStatusOPD=11, colgrfPttVsSymptom=12, colgrfPttVsVN=13, colgrfPttVsAN=14, colgrfPttVsQue=15, colgrfPttVsRemark = 16, colgrfPttVsActno=17, colgrfPttVsActno101 = 18, colgrfPttVsActno110 = 19, colgrfPttVsActno111 = 20, colgrfPttVsActno113 = 21, colgrfPttVsActno131 = 22, colgrfPttVsActno500 = 23, colgrfPttVsActno610 = 24, colgrfPttVsVsDate1=25, colgrfPttVsLimitCreditNo = 26, colgrfPttVsdtrName=27;
        int colgrfPttApmVsDate = 1, colgrfPttApmApmDateShow = 2, colgrfPttApmApmTime = 3, colgrfPttApmHN=4, colgrfPttApmPttName=5, colgrfPttApmDeptR = 6, colgrfPttApmDeptMake=7, colgrfPttApmNote = 8, colgrfPttApmDtrname=9, colgrfPttApmOrder=10, colgrfPttApmDocYear=11, colgrfPttApmDocNo=12, colgrfPttApmPhone=13, colgrfPttApmPaidName=14, colgrfPttApmRemarkCall=15, colgrfPttApmStatusRemarkCall=16, colgrfPttApmRemarkCallDate=17, colgrfPttApmApmDate=18;
        int colgrfRptReportCode = 1, colgrfRptReportName = 2;
        int colgrfRptDatadailydeptDate = 1,colgrfRptDatadailydeptTime = 2, colgrfRptQueno=3,colgrfRptDatadailydeptHn = 4, colgrfRptDatadailydeptName = 5, colgrfRptDatadailydeptMobile = 6, colgrfRptDatadailyDrugSet=7, colgrfRptDatadailyXray = 8, colgrfRptDatadailyAuthen = 9, colgrfRptDatadailyPicKYC = 10, colgrfRptDatadailyPicFoodsdaily = 11;
        int colgrfCustCode=1, colgrfCustName=2;
        int colgrfWardName = 1, colgrfWardRoom = 2, colgrfWardBed = 3, colgrfWardHn = 4, colgrfWardPttName = 5, colgrfWardSymptoms = 6, colgrfWardDays=7, colgrfWardDtrName=8;
        int colgrfSSOSocialNo=1, colgrfSSOCard=2, colgrfSSOTitle=3, colgrfSSOFirstName=4, colgrfSSOLastName=5, colgrfSSOFullName=6, colgrfSSOPrakan=7, colgrfSSOPrang=8, colgrfSSOStartDate=9, colgrfSSOEndDate=10, colgrfSSOdob=11, colgrfSSOUploadDate=12;
        int colgrfSearchPttHN = 1, colgrfSearchPttName = 2, colgrfSearchPttVNShow = 3, colgrfSearchPttSymptom = 4, colgrfSearchPttpreno = 5, colgrfSearchPttVsDate = 6;
        int rowindexgrfsearchptt = 0;
        String rptCode = "", PRENO="", VSDATE="", CHWCODE="", AMPCODE="", TAMBONCODE="", QUENO="", QUEFullname="", QUEHN="", QUESymptoms="", QUEDEPT="", TABTCACTIVE="";
        Boolean pageLoad = false, findCust=false, findInsur=false, wanttoSave = false, wanttoVisit = false, wantEditVisit = false, haveEditPtt=false, flagTamboxOK = false, FLAGPTTNEW=false;
        Boolean FLAGPTTINPUT = false/* เป็น สถานะ กำลังป้อนข้อมูลคนไข้ อาจกด tab ptt, tab vs กลับไปกลับมาเพื่อดูข้อมูล ก่อนsave */;
        Image imgCorr, imgTran;
        C1TileControl TileFoods;
        PanelElement peOrd;
        ImageElement ieOrd;
        TextElement teOrd;
        DataTable dtDeptOPD = null;
        DataTable dtDeptIPD = null;//cboPttPaid

        AutocompleteMenu acmSymptom, acmLine2, acmLine3, acmLine4;
        string[] AUTOSymptom = { "ไข้หวัด", "ล้างแผล", "ล้างแผล(อุบัติเหตุจราจร)"
        , "นัดติดตามอาการ","นัดติดตามดูอาการ", "นัดติดตามดูอาการ(อุบัติเหตุจราจร)", "นัดแพทย์นิติเวช","นัดทำMammogram","นัดล้างแผล ถูกทำร้ายร้างกาย","นัดยิงเลเซอร์ตา","นัดเจาะเลือดก่อนพบแพทย์","นัดส่งเสมหะ+CXR ตอนเช้าพบแพทย์ตอนเย็น 17.00 น."
        , "ซื้อยา","ถอนฟัน","ปัสสาวะขัด","ผื่นที่หน้า","ตาบวม","ตกหลังคา","ท้องเสีย","ไอ เจ็บหน้าอก","ถูกทำร้ายร่างกาย","ตาแดง","หน้าบวม","หูอื้อ","หกล้ม","ส่องกล้อง","ขูดหินปูน","ก้อนที่เต้านม","ฝากครรภ์(ครั้งแรก)"
        , "ปวดหู", "ปวดท้อง", "ปวดขา","ปวดสะโพก" ,"ปวดศีรษะ", "ปวดไหล่", "ปวดแขน","ปวดท้องน้อย","ปวดท้อง ปัสสาวะขัด", "ปวดหลัง", "ปวดขา","ปวดเท้า","ปวดเข่า","ปวดเเข่า (เล่นกีฬา)","ปวดตามข้อ","ปวดฟัน"
                ,"ปวดท้องประจำเดือน","ปวดข้อเท้า","ปวดต้นคอ","ปวดเมื่อยตามร่างกาย","ปวดข้อมือ"
        , "ไข้ ไอ","ไข้ ท้องเสีย แน่นหน้าอก อ่อนเพลีย","แน่นหน้าอก"
        , "กายภาพบำบัด", "พบแพทย์","ผื่นคันตามร่างกาย","เจาะเลือดHIV","ชักเกร็ง","ประจำเดือนผิดปกติ","หกล้ม","หายใจขัด","อุดฟัน","อาเจียนเป็นเลือด ท้องเสีย","แผลที่ริมฝีปาก"
        , "เจ็บคอ","เจ็บคอ ไอ","เจ็บคอ คอบวม","เจ็บเต้านม","เจ็บซี่โครง","เจ็บคอ ATK+","เจ็บตา","เหล็กบาดนิ้วมือ(ขณะทำงาน)","เศษเหล็กเข้าตา(ขณะทำงาน)"
        , "เครื่องปั้มงานทับ","ตกบันได ที่บ้าน","ยาหม่องเข้าตา(โดนเพื่อนป้ายตา)","อาเจียน","ไข้  ไข้เลือดออก","คัดจมูก","เหล็กหล่นทับนิ้วเท้า","กระจกบาดขา","แมวข่วน","ฉีดวัคซีน","เวียนศีรษะ หน้ามืด","นอนไม่หลับ","ผื่นคันตามร่างกาย","ตรวจพิเศษลำไส้ใหญ่","สุนัขกัด"
        , "ทำMRCP","ทำMRI","ทำMammogram"
        , "ตรวจสุขภาพ","ตรวจสุขภาพบริษัท","ตรวจสุขภาพแรงงานต่างด้าว","ตรวจสุขภาพเข้างาน"
        , "ปรึกษาแก้จมูก","ปรึกษาอาการ"
        };
        ComboBoxItem itemVsType;
        AutoCompleteStringCollection autoInsur, autoComp;
        public FrmReception(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);
            fque = new Font(bc.iniC.queFontName, bc.queFontSize + 3, FontStyle.Bold);
            fqueB = new Font(bc.iniC.queFontName, bc.queFontSize + 7, FontStyle.Bold);
            ftotal = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 20, FontStyle.Bold);

            theme1 = new C1ThemeController();
            imgCorr = Resources.red_checkmark_png_16;
            imgTran = Resources.DeleteTable_small;
            ptt = new Patient();

            stt = new C1SuperTooltip();
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            itemVsType = new ComboBoxItem();
            itemVsType.Value = "N";
            itemVsType.Text = "ตรวจปกติ Normal";
            this.Controls.Add(lbLoading);

            bc.bcDB.pm02DB.setCboPrefixT(cboPttPrefixT, "");
            bc.bcDB.pm02DB.setCboPrefixE(cboPttPrefixE, "");

            bc.bcDB.pm03DB.setCboEdu(cboPttEdu, "");
            bc.bcDB.pm04DB.setCboNation(cboPttNat, "");
            bc.bcDB.pm05DB.setCboRel(cboPttRel, "");
            bc.bcDB.pm06DB.setCboRace(cboPttRace, "");
            bc.setCboMarri(cboPttMarri, "");

            if (bc.iniC.statusStation.Equals("OPD"))
            {
                bc.bcDB.pttDB.setCboDeptOPD(cboRptDept, bc.iniC.station);
            }
            else
            {
                bc.bcDB.pttDB.setCboDeptIPD(cboRptDept, bc.iniC.station);
            }
            bc.setCboSex(cboPttSex);
            //Tab Visit
            bc.setCboVisitType(cboVsType);                  //ประเภทการตรวจ
            bc.bcDB.pttDB.setCboDeptOPDNew(cboVsDept, "");     //ส่งตัวไป
            //bc.bcDB.finM02DB.setCboPaidName(cboVsPaid, "'"); //รักษาด้วยสิทธิ
            //bc.bcDB.finM02DB.setCboPaidName(cboPttPaid, "'"); //สิทธิประจำตัว
            bc.bcDB.finM02DB.setCboPaidName(cboApmPaid, "'"); //สิทธิ
            txtRptDateStart.Value = DateTime.Now;
            txtRptDateEnd.Value = DateTime.Now;
            //dtDeptOPD = bc.bcDB.pm32DB.selectDeptOPDAll();
            //dtDeptIPD = bc.bcDB.pm32DB.selectDeptIPDAll();
            //bc.bcDB.pm09DB.setCboProvince(cboPttRefProv, "");
            //bc.bcDB.pm09DB.setCboProvince(cboPttRefProv, "");
            cboVsDtr.Items.Add("");
            setEvent();
            setCboTheme();
            setTabIndex();

            setTheme(bc.iniC.themeApp);
            initGrfSrc();
            initGrfPttVs();
            initGrfPttApm();
            initGrfRptReport();
            initGrfRptData();
            initTileImg();
            //initGrfCust();
            initGrfVsPttVisit();
            initGrfToday();
            initGrfApm();
            initGrfWard();
            initGrfSSO();

            chkApmDate.Checked = true;
            txtApmDate.Value = DateTime.Now;
            setTabApmCheck();

            initAutoComTxtSymptom();
            BuildAutocompleteMenuSymptom();

            int nres = 0;
            byte[] _lic = String2Byte(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\RDNIDLib.DLD");
            
            nres = RDNID.openNIDLibRD(_lic);
            if (nres != 0)
            {
                String m;
                m = String.Format(" error no {0} ", nres);
                MessageBox.Show(m);
            }
            byte[] Licinfo = new byte[1024];

            RDNID.getLicenseInfoRD(Licinfo);
            //m_lblDLDInfo.Text = aByteToString(Licinfo);

            byte[] Softinfo = new byte[1024];
            RDNID.getSoftwareInfoRD(Softinfo);
            ListCardReader();
            
            pageLoad = false;
        }
        private void setTabApmCheck()
        {
            if(chkApmDate.Checked)
            {
                txtApmDate.Enabled = true;
                txtApmHn.Enabled = false;
            }
            else
            {
                txtApmDate.Enabled = false;
                txtApmHn.Enabled = true;
                txtApmHn.Focus();
            }
        }
        private void BuildAutocompleteMenuSymptom()
        {
            var items = new List<AutocompleteItem>();
            if (bc.postoperation != null)
            {
                foreach (var item in bc.postoperation)
                    items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            }

            foreach (var item in AUTOSymptom)
                items.Add(new AutocompleteItem(item));

            //set as autocomplete source
            acmSymptom.SetAutocompleteItems(items);
        }
        private void initAutoComTxtSymptom()
        {
            acmSymptom = new AutocompleteMenuNS.AutocompleteMenu();
            acmSymptom.AllowsTabKey = true;
            acmSymptom.Font = new System.Drawing.Font(bc.iniC.grdViewFontName, 12F);
            acmSymptom.Items = new string[0];
            acmSymptom.SearchPattern = "[\\w\\.:=!<>]";
            acmSymptom.TargetControlWrapper = null;

            acmSymptom.SetAutocompleteMenu(txtVsSymptom, acmSymptom);
        }
        private void setAutoComplete()
        {
            txtVsSymptom.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtVsSymptom.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            col.Add("ไข้หวัด");
            col.Add("ตรวจสุขภาพ");
            txtVsSymptom.AutoCompleteCustomSource = col;
        }
        private void setEvent()
        {
            btnSrcNew.Click += BtnSrcNew_Click;
            txtSrcHn.KeyUp += TxtSrcHn_KeyUp;
            btnSrcCardRead.Click += BtnSrcCardRead_Click;

            cboPttPrefixT.DropDownClosed += CboPttPrefixT_DropDownClosed;
            txtPttNameT.KeyUp += TxtPttNameT_KeyUp;
            txtPttSurNameT.KeyUp += TxtPttSurNameT_KeyUp;
            txtPttNameE.KeyUp += TxtPttNameE_KeyUp;
            txtPttSurNameE.KeyUp += TxtPttSurNameE_KeyUp;
            txtPttPID.KeyUp += TxtPttPID_KeyUp;
            txtPttPID.LostFocus += TxtPttPID_LostFocus;
            txtPttSsn.KeyUp += TxtPttSsn_KeyUp;
            txtPttPassport.KeyUp += TxtPttPassport_KeyUp;
            txtPttPassportOld.KeyUp += TxtPttPassportOld_KeyUp;
            txtPttMobile1.KeyUp += TxtPttMobile1_KeyUp;
            txtPttMobile2.KeyUp += TxtPttMobile2_KeyUp;
            txtPttEmail.KeyUp += TxtPttEmail_KeyUp;
            txtPttIDHomeNo.KeyUp += TxtPttIDHomeNo_KeyUp;
            txtPttIDMoo.KeyUp += TxtPttIDMoo_KeyUp;
            txtPttIDSoi.KeyUp += TxtPttIDSoi_KeyUp;
            txtPttIDRoad.KeyUp += TxtPttIDRoad_KeyUp;
            
            txtPttCurHomeNo.KeyUp += TxtPttCurHomeNo_KeyUp;
            txtPttCurMoo.KeyUp += TxtPttCurMoo_KeyUp;
            txtPttCurSoi.KeyUp += TxtPttCurSoi_KeyUp;
            txtPttCurRoad.KeyUp += TxtPttCurRoad_KeyUp;
            btnRptOk.Click += BtnRptOk_Click;
            btnRptExcel.Click += BtnRptExcel_Click;
            txtPttRefHomeNo.KeyUp += TxtPttRefHomeNo_KeyUp;
            txtPttRefMoo.KeyUp += TxtPttRefMoo_KeyUp;
            txtPttRefSoi.KeyUp += TxtPttRefSoi_KeyUp;
            txtPttRefRoad.KeyUp += TxtPttRefRoad_KeyUp;
            txtPttRefContact1Name.KeyUp += TxtPttRefContact1Name_KeyUp;
            txtPttRefContact1Mobile.KeyUp += TxtPttRefContact1Mobile_KeyUp;
            txtPttRefContact2Name.KeyUp += TxtPttRefContact2Name_KeyUp;
            txtPttRefContact2Mobile.KeyUp += TxtPttRefContact2Mobile_KeyUp;
            
            cboPttNat.DropDownClosed += CboPttNat_DropDownClosed;
            cboPttNat.KeyUp += CboPttNat_KeyUp;
            cboPttRel.DropDownClosed += CboPttRel_DropDownClosed;
            cboPttEdu.DropDownClosed += CboPttEdu_DropDownClosed;
            cboPttMarri.DropDownClosed += CboPttMarri_DropDownClosed;
            txtPttAttchNote.KeyUp += TxtPttAttchNote_KeyUp;
            txtPttRemark1.KeyUp += TxtPttRemark1_KeyUp;
            btnPttCardRead.Click += BtnPttCardRead_Click;
            btnPttLIC.Click += BtnPttLIC_Click;
            btnPttIdCopyto.Click += BtnPttIdCopyto_Click;
            btnPttCurCopyto.Click += BtnPttCurCopyto_Click;
            txtPttHn.KeyUp += TxtPttHn_KeyUp;
            cboTheme.SelectedIndexChanged += CboTheme_SelectedIndexChanged;
            txtPttwp1.KeyUp += TxtPttwp1_KeyUp;
            txtPttwp2.KeyUp += TxtPttwp2_KeyUp;
            txtPttwp3.KeyUp += TxtPttwp3_KeyUp;
            txtPttDOB.DropDownClosed += TxtPttDOB_DropDownClosed;
            txtPttUser.KeyUp += TxtPttUser_KeyUp;
            txtPttIDPostcode.KeyUp += TxtPttIDPostcode_KeyUp;
            txtPttCurPostcode.KeyUp += TxtPttCurPostcode_KeyUp;
            txtPttRefPostcode.KeyUp += TxtPttRefPostcode_KeyUp;
            txtPttRef1.KeyUp += TxtPttRef1_KeyUp;
            
            txtPttNickName.KeyUp += TxtPttNickName_KeyUp;

            btnPttSave.Click += BtnPttSave_Click;
            btnDocSearch.Click += BtnDocSearch_Click;
            btnPttVisit.Click += BtnPttVisit_Click;
            txtPttIdSearchTambon.KeyUp += TxtPttIdSearchTambon_KeyUp;
            txtPttCurSearchTambon.KeyUp += TxtPttCurSearchTambon_KeyUp;
            txtPttRefSearchTambon.KeyUp += TxtPttRefSearchTambon_KeyUp;
            lbPID.Click += LbPID_Click;
            btnPrnQue.Click += BtnPrnQue_Click;

            btnVsSave.Click += BtnVsSave_Click;
            
            cboVsDept.DropDownClosed += CboVsDept_DropDownClosed;
            txtVsSymptom.KeyUp += TxtVsSymptom_KeyUp;
            txtVsRemark.KeyUp += TxtVsRemark_KeyUp;
            txtVsNote.KeyUp += TxtVsNote_KeyUp;
            cboVsDtr.DropDownClosed += CboVsDtr_DropDownClosed;
            cboVsType.DropDownClosed += CboVsType_DropDownClosed;
            txtVsUser.KeyUp += TxtVsUser_KeyUp;
            txtPttDOBDD.KeyUp += TxtPttDOBDD_KeyUp;
            txtPttDOBMM.KeyUp += TxtPttDOBMM_KeyUp;
            txtPttDOBYear.KeyUp += TxtPttDOBYear_KeyUp;
            txtPttDOBDD.KeyPress += TxtPttDOBDD_KeyPress;
            txtPttDOBMM.KeyPress += TxtPttDOBMM_KeyPress;
            txtPttDOBYear.KeyPress += TxtPttDOBYear_KeyPress;
            btnPttClearData.Click += BtnPttClearData_Click;
            txtVsPaidCode.KeyUp += TxtVsPaidCode_KeyUp;

            chkApmDate.Click += ChkApmDate_Click;
            chkApmHn.Click += ChkApmHn_Click;
            btnApmSearch.Click += BtnApmSearch_Click;
            txtApmHn.KeyPress += TxtApmHn_KeyPress;
            cboApmPaid.DropDownClosed += CboApmPaid_DropDownClosed;
            
            tC.SelectedTabChanged += TC_SelectedTabChanged;
            txtSSOsearch.KeyUp += TxtSSOsearch_KeyUp;
            txtVsInsur.KeyUp += TxtVsInsur_KeyUp;
            txtVsComp.KeyUp += TxtVsComp_KeyUp;
            txtPttCompCode.KeyUp += TxtPttCompCode_KeyUp;
            txtPttCompCode.Enter += TxtPttCompCode_Enter;
            txtPttInsur.KeyUp += TxtPttInsur_KeyUp;
            txtPttInsur.Enter += TxtPttInsur_Enter;
            
            txtApmHn.KeyUp += TxtApmHn_KeyUp;
            txtApmSrc.KeyPress += TxtApmSrc_KeyPress;
            txtWardSrc.KeyPress += TxtWardSrc_KeyPress;
            txtPttPaid.KeyUp += TxtPttPaid_KeyUp;
            btnPttPaid.Click += BtnPttPaid_Click;
            btnPttPaid.MouseHover += BtnPttPaid_MouseHover;
            btnApmExcel.Click += BtnApmExcel_Click;
            btnPttInsur.Click += BtnPttInsur_Click;
            btnPttInsur.MouseHover += BtnPttInsur_MouseHover;
            btnPttComp.Click += BtnPttComp_Click;
            btnPttComp.MouseHover += BtnPttComp_MouseHover;

            rbDateSearch.DropDownClosed += RbDateSearch_DropDownClosed;
            rbDateSearch.ValueChanged += RbDateSearch_ValueChanged;
            btnPttInsurCopyto.Click += BtnPttInsurCopyto_Click;
            btnVsPaid.Click += BtnVsPaid_Click;
            btnPttSsnCopy.Click += BtnPttSsnCopy_Click;

            btnAlienGenVisitAll.Click += BtnAlienGenVisitAll_Click;
            rbTxtHnSearch.KeyPress += RbTxtHnSearch_KeyPress;
            lbalcode.DoubleClick += Lbalcode_DoubleClick;
            btnSrcAlien.Click += BtnSrcAlien_Click;
        }

        private void BtnSrcAlien_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            chkAlien(txtSrcHn.Text.Trim());
            if (txtPttPID.Text.Trim().Length > 0) { tC.SelectedTab = tabPtt; setControlTabPTT(); }
        }

        private void Lbalcode_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            chkAlien(txtPttwp1.Text.Trim());
        }
        private async void chkAlien(String alcode)
        {
            Boolean chk = false;
            DoeAlienList doealien = await bc.GetDoeAien(alcode);
            if (doealien.alcode == null) { lfSbMessage.Text = "ไม่พบข้อมูล จากAPIกรมการจัดหางาน"; return; }
            txtPttNameT.Value = doealien.alnameen;
            txtPttNameE.Value = doealien.alnameen;
            txtPttPID.Value = doealien.alcode;
            txtPttwp1.Value = doealien.alcode;
            String[] dob1 = doealien.albdate.Split(new char[] { '-' });
            txtPttDOB.Value = bc.datetoDBCultureInfo(dob1[2] + "-" + dob1[1] + "-" + dob1[0]);
            DateTime ddchk = DateTime.Parse(bc.datetoDBCultureInfo(dob1[2] + "-" + dob1[1] + "-" + dob1[0]));
            if (ddchk.Year < 1900) { ddchk = ddchk.AddYears(543); }
            String dob = bc.datetoDBCultureInfo(dob1[2] + "-" + dob1[1] + "-" + dob1[0]);
            if (dob.Length >= 10)
            {
                txtPttDOBDD.Value = ddchk.Day;
                txtPttDOBMM.Value = ddchk.Month;
                txtPttDOBYear.Value = ddchk.Year;
                Patient pttage = new Patient();
                pttage.patient_birthday = dob;
                txtPttAge.Value = pttage.AgeStringShort1();
            }
            bc.setC1ComboByName(cboPttPrefixT, doealien.alprefixen.Replace("MRS", "MRS.").Replace("MR", "MR.").Replace("MISS", "MISS."));
            bc.setC1Combo(cboPttSex, doealien.algender.Replace("1", "M").Replace("2", "F"));
            //cboPttNat
            bc.setC1Combo(cboPttNat, doealien.alnation.Replace("M", "48").Replace("L", "56").Replace("V", "46").Replace("C", "57"));
            txtPttCompCode.Value = doealien.empname;
            String[] addr = doealien.wkaddress.Split(new char[] { ' ' });
            if (addr.Length > 0)
            {
                txtPttIDHomeNo.Value = addr[0];
                txtPttIDPostcode.Value = addr[addr.Length - 1];
                int i = 0;
                foreach (String txt in addr)
                {
                    if (txt.IndexOf("Moo") > 0)
                    {
                        txtPttIDMoo.Value = txt.Replace("Moo", "").Trim();
                    }
                    if (txt.IndexOf("ซอย") >= 0)
                    {
                        String aa = "", bb = "", cc = "", dd = "";
                        if (addr.Length > 4)
                        {
                            aa = addr[i + 1];
                            bb = addr[i + 2];
                            cc = addr[i + 3];
                            dd = addr[i + 4];
                        }
                        if ((dd.IndexOf("แขวง") >= 0) || (dd.IndexOf("ตำบล") >= 0))
                        {
                            txtPttIDSoi.Value = txt.Trim() + aa + bb + cc;
                        }
                        else
                        {
                            txtPttIDSoi.Value = txt.Trim() + aa + bb + cc + dd;
                        }
                    }
                    if (txt.IndexOf("แขวง") >= 0)
                    {
                        txtPttIdSearchTambon.Value = txt.Replace("แขวง", "").Trim();
                    }
                    if (txt.IndexOf("ตำบล") >= 0)
                    {
                        txtPttIdSearchTambon.Value = txt.Replace("ตำบล", "").Trim();
                    }
                    if (txt.IndexOf("เขต") >= 0)
                    {
                        txtPttIdAmp.Value = txt.Trim();
                    }
                    if (txt.IndexOf("อำเภอ") >= 0)
                    {
                        txtPttIdAmp.Value = txt.Replace("อำเภอ", "").Trim();
                    }
                    if (txt.IndexOf("จังหวัด") >= 0)
                    {
                        String provname = txt.Replace("จังหวัด", "").Trim();
                        //String provcode = bc.bcDB.pm09DB.getProvName(provname);
                        txtPttIdChw.Value = txt.Replace("จังหวัด", "").Trim();
                    }
                    i++;
                }
            }
            txtPttCurHomeNo.Value = txtPttIDHomeNo.Text;
            txtPttCurMoo.Value = txtPttIDMoo.Text;
            txtPttCurSoi.Value = txtPttIDSoi.Text;
            txtPttCurRoad.Value = txtPttIDRoad.Text;
            txtPttCurPostcode.Value = txtPttIDPostcode.Text;
            txtPttCurSearchTambon.Value = txtPttIdSearchTambon.Text;
            txtPttCurAmp.Value = txtPttIdAmp.Text;
            txtPttCurChw.Value = txtPttIdChw.Text;
            checkPaidSSO(txtPttwp1.Text.Trim());
            chk = true;
            //return chk;
        }
        private void RbTxtHnSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            //grfApm.ApplySearch(txtApmSrc.Text.Trim(), true, true, false);
            if (tC.SelectedTab == tabToday)
            {
                grfToday.ApplySearch(rbTxtHnSearch.Text.Trim(), true, true, false);
            }
            else if (tC.SelectedTab == tabApm)
            {
                //grfApm.ApplySearch(rbTxtHnSearch.Text.Trim(), true, true, false);
            }
            else if (tC.SelectedTab == tabWard)
            {
                //grfWard.ApplySearch(rbTxtHnSearch.Text.Trim(), true, true, false);
            }
        }
        private void BtnAlienGenVisitAll_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(chkAlien1.Checked)
            {
                genVisitAlien(txtAlienFullName1.Text.Trim(), txtAlienAddr1.Text.Trim(), txtAlienAddr21.Text.Trim(), txtAlienPID1.Text.Trim(), txtAliendob1.Text.Trim(), txtAlienNat1.Text.Trim(), txtAlienHn1, lbAlienpreno1);
            }
            if (chkAlien2.Checked)
            {
                genVisitAlien(txtAlienFullName2.Text.Trim(), txtAlienAddr2.Text.Trim(), txtAlienAddr22.Text.Trim(), txtAlienPID2.Text.Trim(), txtAliendob2.Text.Trim(), txtAlienNat2.Text.Trim(), txtAlienHn2, lbAlienpreno2);
            }
            if (chkAlien3.Checked)
            {
                genVisitAlien(txtAlienFullName3.Text.Trim(), txtAlienAddr3.Text.Trim(), txtAlienAddr23.Text.Trim(), txtAlienPID3.Text.Trim(), txtAliendob3.Text.Trim(), txtAlienNat3.Text.Trim(), txtAlienHn3, lbAlienpreno3);
            }
            if (chkAlien4.Checked)
            {
                genVisitAlien(txtAlienFullName4.Text.Trim(), txtAlienAddr4.Text.Trim(), txtAlienAddr24.Text.Trim(), txtAlienPID4.Text.Trim(), txtAliendob4.Text.Trim(), txtAlienNat4.Text.Trim(), txtAlienHn4, lbAlienpreno4);
            }
            if (chkAlien5.Checked)
            {
                genVisitAlien(txtAlienFullName5.Text.Trim(), txtAlienAddr5.Text.Trim(), txtAlienAddr25.Text.Trim(), txtAlienPID5.Text.Trim(), txtAliendob5.Text.Trim(), txtAlienNat5.Text.Trim(), txtAlienHn5, lbAlienpreno5);
            }
            if (chkAlien6.Checked)
            {
                genVisitAlien(txtAlienFullName6.Text.Trim(), txtAlienAddr6.Text.Trim(), txtAlienAddr26.Text.Trim(), txtAlienPID6.Text.Trim(), txtAliendob6.Text.Trim(), txtAlienNat6.Text.Trim(), txtAlienHn6, lbAlienpreno6);
            }
            if (chkAlien7.Checked)
            {
                genVisitAlien(txtAlienFullName7.Text.Trim(), txtAlienAddr7.Text.Trim(), txtAlienAddr27.Text.Trim(), txtAlienPID7.Text.Trim(), txtAliendob7.Text.Trim(), txtAlienNat7.Text.Trim(), txtAlienHn7, lbAlienpreno7);
            }
            if (chkAlien8.Checked)
            {
                genVisitAlien(txtAlienFullName8.Text.Trim(), txtAlienAddr8.Text.Trim(), txtAlienAddr28.Text.Trim(), txtAlienPID8.Text.Trim(), txtAliendob8.Text.Trim(), txtAlienNat8.Text.Trim(), txtAlienHn8, lbAlienpreno8);
            }
            if (chkAlien9.Checked)
            {
                genVisitAlien(txtAlienFullName9.Text.Trim(), txtAlienAddr9.Text.Trim(), txtAlienAddr29.Text.Trim(), txtAlienPID9.Text.Trim(), txtAliendob9.Text.Trim(), txtAlienNat9.Text.Trim(), txtAlienHn9, lbAlienpreno9);
            }
            if (chkAlien10.Checked)
            {
                genVisitAlien(txtAlienFullName10.Text.Trim(), txtAlienAddr10.Text.Trim(), txtAlienAddr210.Text.Trim(), txtAlienPID10.Text.Trim(), txtAliendob10.Text.Trim(), txtAlienNat10.Text.Trim(), txtAlienHn10, lbAlienpreno10);
            }
            if (chkAlien11.Checked)
            {
                genVisitAlien(txtAlienFullName11.Text.Trim(), txtAlienAddr11.Text.Trim(), txtAlienAddr211.Text.Trim(), txtAlienPID11.Text.Trim(), txtAliendob11.Text.Trim(), txtAlienNat11.Text.Trim(), txtAlienHn11, lbAlienpreno11);
            }
            if (chkAlien12.Checked)
            {
                genVisitAlien(txtAlienFullName12.Text.Trim(), txtAlienAddr12.Text.Trim(), txtAlienAddr212.Text.Trim(), txtAlienPID12.Text.Trim(), txtAliendob12.Text.Trim(), txtAlienNat12.Text.Trim(), txtAlienHn12, lbAlienpreno10);
            }
            if (chkAlien13.Checked)
            {
                genVisitAlien(txtAlienFullName13.Text.Trim(), txtAlienAddr13.Text.Trim(), txtAlienAddr213.Text.Trim(), txtAlienPID13.Text.Trim(), txtAliendob13.Text.Trim(), txtAlienNat13.Text.Trim(), txtAlienHn13, lbAlienpreno13);
            }
        }
        private void genVisitAlien(String fullname, String addr1, String addr2, String alienpid, String dob, String natcode, C1TextBox txthn, Label lbpreno)
        {
            //throw new NotImplementedException();
            String err = "", sex = "";
            Patient ptt = new Patient();
            ptt.MNC_HN_NO = "";
            ptt.passport = "";
            ptt.MNC_HN_YR = "";
            ptt.MNC_PFIX_CDT = bc.bcDB.pm02DB.getCodeByNameE(fullname);
            ptt.MNC_PFIX_CDE = bc.bcDB.pm02DB.getCodeByNameE(fullname);
            err = "02";
            ptt.MNC_FNAME_T = fullname;
            ptt.MNC_LNAME_T = fullname;
            ptt.MNC_FNAME_E = fullname;
            ptt.MNC_LNAME_E = fullname;
            if ((ptt.MNC_FNAME_T.Length <= 0) && (ptt.MNC_FNAME_E.Length <= 0))
            {
                lfSbMessage.Text = "ชื่อ ไม่ถูกต้อง";
                return;
            }

            DateTime.TryParse(dob, out DateTime dob1);
            if (dob1.Year < 1900) { dob1 = dob1.AddYears(543); }
            ptt.MNC_BDAY = dob1.Year.ToString() + "-" + dob1.ToString("MM-dd");
            ptt.patient_birthday = dob1.Year.ToString() + "-" + dob1.ToString("MM-dd");
            String age = ptt.AgeStringOK1DOT();
            if (age.Length > 0)
            {
                if (age.IndexOf(".") > 0) ptt.MNC_AGE = age.Substring(0, age.IndexOf("."));
                else ptt.MNC_AGE = age;
            }
            else
            {
                ptt.MNC_AGE = "0";
            }
            ptt.MNC_SEX = sex;
            ptt.MNC_SS_NO = alienpid;
            ptt.WorkPermit1 = alienpid;
            ptt.MNC_NAT_CD = natcode.Trim().Equals("M") ? "48" : natcode.Trim().Equals("L") ? "56" : natcode.Trim().Equals("C") ? "57" : natcode.Trim().Equals("V") ? "46" : "97";
            ptt.MNC_ID_NAM = alienpid;
            ptt.MNC_ID_NO = alienpid;
            //ptt.MNC_AGE = "";
            ptt.MNC_ATT_NOTE = "";
            ptt.MNC_CUR_ADD = "";
            ptt.MNC_CUR_MOO = "";
            ptt.MNC_CUR_SOI = "";
            ptt.MNC_CUR_ROAD = "";
            ptt.MNC_CUR_TUM = "";
            ptt.MNC_CUR_AMP = "";
            ptt.MNC_CUR_CHW = "";
            ptt.MNC_CUR_POC = "";
            ptt.MNC_CUR_TEL = "";
            ptt.MNC_DOM_ADD = "";
            ptt.MNC_DOM_MOO = "";
            ptt.MNC_DOM_SOI = "";
            ptt.MNC_DOM_ROAD = "";
            ptt.MNC_DOM_TUM = "";
            ptt.MNC_DOM_AMP = "";
            ptt.MNC_DOM_CHW = "";
            ptt.MNC_DOM_POC = "";
            ptt.MNC_DOM_TEL = "";
            ptt.MNC_COM_CD = "";           //มีแจ้ง error ว่า save แล้ว บริษัทหาย ได้ลอง debug เช่น aIa ค้นไม่เจอ
            ptt.MNC_COM_CD2 = "";
            ptt.MNC_FN_TYP_CD = "18";
            ptt.remark1 = "alien doe";
            ptt.passport = "";
            ptt.MNC_HN_YR = "";
            String re = bc.bcDB.pttDB.insertPatientStep1(ptt);
            if (long.TryParse(re, out long chk))
            {
                ptt = bc.bcDB.pttDB.selectPatinetByPID(alienpid, "work_permit1");
                txthn.Value = ptt.Hn;
                
            }
        }
        private void BtnPttSsnCopy_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtPttSsn.Value = txtPttPID.Text.Trim();
        }
        private void BtnVsPaid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setFormPaidList(((C1Button)sender).Name);
        }
        private void CboPttNat_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            //lfSbMessage.Text = e.KeyData.ToString();
            if (e.KeyCode == Keys.Enter)
            {
                pageLoad = true;
                bc.setC1Combo(cboPttNat, cboPttNat.Text);
                pageLoad = false;
                cboPttRel.Focus();
            }
        }

        private void BtnPttInsurCopyto_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtPttCompCode.Value = txtPttInsur.Text.Trim();
        }

        private void RbDateSearch_ValueChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfToday();
        }
        private void RbDateSearch_DropDownClosed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //setGrfToday();
        }
        private void TC_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (pageLoad) return;
            rbTxtHnSearch.Visible = false;
            rbDateSearch.Visible = false;
            //TABTCACTIVE = ((C1DockingTabPage)sender).Name;
            if (tC.SelectedTab == tabSrc)
            {
                TABTCACTIVE = tabSrc.Name;
                txtSrcHn.SelectAll();
                txtSrcHn.Focus();
            }
            else if (tC.SelectedTab == tabPtt)
            {
                TABTCACTIVE = tabSrc.Name;
                setGrfPttVs();
                setGrfPttApm();
            }
            else if (tC.SelectedTab == tabVs)
            {
                TABTCACTIVE = tabSrc.Name;
            }
            else if (tC.SelectedTab == tabToday)
            {
                TABTCACTIVE = tabSrc.Name;
                rbTxtHnSearch.Visible = true;
                rbDateSearch.Visible = true;
            }
            else if (tC.SelectedTab == tabApm)
            {
                TABTCACTIVE = tabSrc.Name;
            }
            else if (tC.SelectedTab == tabRpt)
            {
                TABTCACTIVE = tabSrc.Name;
            }
            else if (tC.SelectedTab == tabDoc)
            {
                TABTCACTIVE = tabSrc.Name;
            }
            else if (tC.SelectedTab == tabWard)
            {
                TABTCACTIVE = tabSrc.Name;
            }
            else if (tC.SelectedTab == tabSSO)
            {
                TABTCACTIVE = tabSrc.Name;
            }
            
            setTabActive();
        }
        private void setTabActive()
        {
            wantEditVisit = false;
            VSDATE = "";
            PRENO = "";
            lfSbStatus.Text = "";
            lfSbMessage.Text = "";
            if (tC.SelectedTab == tabToday)
            {
                setGrfToday();
            }
            else if (tC.SelectedTab == tabApm)
            {
                setGrfApm();
            }
            else if (tC.SelectedTab == tabSrc)
            {
                txtSrcHn.Focus();
            }
            else if (tC.SelectedTab == tabVs)
            {
                VSDATE = "";
                PRENO = "";
                btnVsSave.Text = "ส่งตัว";
                setCboVsType();
            }
            else if (tC.SelectedTab == tabWard)
            {
                VSDATE = "";
                PRENO = "";
                setGrfWard();
            }
            else if (tC.SelectedTab == tabSSO)
            {
                txtSSOsearch.Focus();
            }
        }
        private void TxtPttRef1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) {                txtPttwp3.SelectAll();                txtPttwp3.Focus();            }
        }
        private void TxtPttPassportOld_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter){                txtPttMobile1.SelectAll();                txtPttMobile1.Focus();            }
        }

        private void BtnApmExcel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String filenam = "";
            filenam = "app_" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd") + ".xls";
            if (File.Exists(bc.iniC.pathDownloadFile + "\\" + filenam))
            {
                lfSbMessage.Text = "พบ File " + filenam + " กรุณาลบ File นี้ก่อน";
                //return;
            }
            C1XLBook _book = new C1XLBook();
            _book.Sheets.Remove("Sheet1");
            XLSheet sheet = _book.Sheets.Add("Sheet1");
            bc.SaveSheet(grfApm, sheet, _book, false);

            _book.Sheets.SelectedIndex = 0;

            // save the book
            _book.Save(bc.iniC.pathDownloadFile + "\\" + filenam);
            System.Diagnostics.Process.Start(bc.iniC.pathDownloadFile + "\\" + filenam);
        }
        private void BtnPttPaid_MouseHover(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            stt.Show("<p> Double Click เพื่อดู สิทธิประจำตัว ทั้งหมด </p>", btnPttPaid, 1000);
        }
        private void BtnPttPaid_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setFormPaidList(((C1Button)sender).Name);
        }
        private void setFormPaidList(String btnname)
        {
            int i = 1;
            C1FlexGrid grf = new C1FlexGrid();
            grf.Font = fEdit;
            grf.Dock = System.Windows.Forms.DockStyle.Fill;
            grf.Location = new System.Drawing.Point(0, 0);
            grf.Cols.Count = 5;
            grf.Cols[1].Width = 40;
            grf.Cols[2].Width = 320;
            grf.Cols[3].Width = 80;
            grf.Cols[1].DataType = typeof(String);
            grf.Cols[2].DataType = typeof(String);
            grf.Cols[1].TextAlign = TextAlignEnum.CenterCenter;
            grf.Cols[2].TextAlign = TextAlignEnum.LeftCenter;
            grf.Cols[1].AllowEditing = false;
            grf.Cols[2].AllowEditing = false;
            grf.Cols[3].AllowEditing = false;
            grf.Rows[0].Visible = false;
            grf.Rows[4].Visible = false;
            grf.DoubleClick += GrfPttPaid_DoubleClick;
            theme1.SetTheme(grf, "Office2010Blue");
            DataTable dt = new DataTable();
            dt = bc.bcDB.finM02DB.SelectAll();
            grf.Rows.Count = 1; grf.Rows.Count = dt.Rows.Count + 1;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow drow in dt.Rows)
                {
                    Row rowa = grf.Rows[i];
                    rowa[1] = drow["MNC_FN_TYP_CD"].ToString();
                    rowa[2] = drow["MNC_FN_TYP_DSC"].ToString();
                    rowa[3] = drow["MNC_FN_STS"].ToString();
                    rowa[0] = i.ToString();
                    rowa[4] = btnname;
                    i++;
                }
            }
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Size = new Size(700, 400);
            frm.Controls.Add(grf);
            frm.ShowDialog();
        }
        private void GrfPttPaid_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, 4].ToString().Equals("btnPttPaid"))
            {
                txtPttPaid.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, 2].ToString();
            }
            else
            {
                txtVsPaidCode.Value = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, 2].ToString();
            }
        }

        private void TxtPttPaid_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode== Keys.Enter)
            {
                if (int.TryParse(txtPttPaid.Text, out _))//ป้อนเป็น ตัวเลข
                {
                    txtPttPaid.Value = bc.bcDB.finM02DB.getPaidName(txtPttPaid.Text.Trim());
                }
                txtPttInsur.SelectAll();
                txtPttInsur.Focus();
            }
        }

        private void TxtWardSrc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            grfWard.ApplySearch(txtWardSrc.Text.Trim(), true, true, false);
        }

        private void TxtApmSrc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            grfApm.ApplySearch(txtApmSrc.Text.Trim(), true, true, false);
        }

        private void TxtApmHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode== Keys.Enter)            {                setGrfApm();            }
        }

        private void TxtVsComp_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(txtVsComp.Text.Trim(), out _))//ป้อนเป็น ตัวเลข
                {
                    txtVsComp.Value = bc.bcDB.pm24DB.getPaidName(txtVsComp.Text.Trim());
                }
            }
            else
            {

            }
        }

        private void TxtVsInsur_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(txtVsInsur.Text, out _))//ป้อนเป็น ตัวเลข
                {
                    txtVsInsur.Value = bc.bcDB.pm24DB.getPaidName(txtVsInsur.Text.Trim());
                }
            }
            else
            {

            }
        }

        private void TxtSSOsearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if ((e.KeyCode == Keys.Enter) || (txtSSOsearch.Text.Length > 4))
            {
                setGrfSSO();
            }
            else
            {

            }
        }
        private void setGrfSSO()
        {
            showLbLoading();
            DataTable dt = new DataTable();
            dt = bc.bcDB.prakM01DB.selectSSOBySearch(txtSSOsearch.Text.Trim());
            int i = 1, j = 1;
            grfSSO.Rows.Count = 1; grfSSO.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                try
                {
                    Row rowa = grfSSO.Rows[i];
                    rowa[colgrfSSOSocialNo] = row1["SocialID"].ToString();
                    rowa[colgrfSSOCard] = row1["Social_Card_no"].ToString();
                    //rowa[colgrfSSOTitle] = row1["mnc_id_no"].ToString();
                    rowa[colgrfSSOFullName] = row1["FullName"].ToString();
                    rowa[colgrfSSOStartDate] = row1["StartDate"].ToString();
                    rowa[colgrfSSOEndDate] = row1["EndDate"].ToString();
                    rowa[colgrfSSOUploadDate] = row1["UploadDate"].ToString();
                    rowa[colgrfSrcPttid] = "";
                    rowa[0] = i.ToString();
                    i++;
                }
                catch (Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmReception setGrfSrc " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setGrfSrc ", ex.Message);
                }
            }
            hideLbLoading();
        }
        private void BtnPrnQue_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            printQueue();
        }
        private void LbPID_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Clipboard.SetText(txtPttPID.Text.Trim());
            stt.Show("<p> Copy to clipboard เรียบร้อย</p>", lbPID,1000);
        }

        private void TxtPttRefPostcode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttRefChw.Value = bc.bcDB.pm07DB.getChwNameByPostCode(txtPttRefPostcode.Text.Trim());
                txtPttRefAmp.Value = bc.bcDB.pm07DB.getAmpNameByPostCode(txtPttRefPostcode.Text.Trim());
                txtPttRefSearchTambon.Value = bc.bcDB.pm07DB.getTambonNameByPostCode(txtPttRefPostcode.Text.Trim());
            }
        }
        private void TxtPttCurPostcode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttCurChw.Value = bc.bcDB.pm07DB.getChwNameByPostCode(txtPttCurPostcode.Text.Trim());
                txtPttCurAmp.Value = bc.bcDB.pm07DB.getAmpNameByPostCode(txtPttCurPostcode.Text.Trim());
                txtPttCurSearchTambon.Value = bc.bcDB.pm07DB.getTambonNameByPostCode(txtPttCurPostcode.Text.Trim());
            }
        }
        private void TxtPttIDPostcode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttIdChw.Value = bc.bcDB.pm07DB.getChwNameByPostCode(txtPttIDPostcode.Text.Trim());
                txtPttIdAmp.Value = bc.bcDB.pm07DB.getAmpNameByPostCode(txtPttIDPostcode.Text.Trim());
                txtPttIdSearchTambon.Value = bc.bcDB.pm07DB.getTambonNameByPostCode(txtPttIDPostcode.Text.Trim());
            }
        }
        private void TxtPttRefSearchTambon_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (txtPttRefSearchTambon.Text.Trim().Length > 0)
                {
                    String[] txttambox = txtPttRefSearchTambon.Text.Trim().Split(',');
                    if (txttambox.Length > 0)
                    {
                        CHWCODE = bc.bcDB.pm07DB.getChwCode(txttambox[2]);
                        AMPCODE = bc.bcDB.pm07DB.getAmpCode(txttambox[1]);
                        TAMBONCODE = bc.bcDB.pm07DB.getTambonCode(txttambox[0]);
                        txtPttRefSearchTambon.Value = txttambox[0].Trim();
                        txtPttRefAmp.Value = txttambox[1].Trim();
                        txtPttRefChw.Value = txttambox[2].Trim();
                        txtPttRefPostcode.Value = bc.bcDB.pm07DB.getPostCode(txttambox[2]);
                    }
                }
            }
        }
        private void TxtPttCurSearchTambon_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (txtPttCurSearchTambon.Text.Trim().Length > 0)
                {
                    String[] txttambox = txtPttCurSearchTambon.Text.Trim().Split(',');
                    if (txttambox.Length > 0)
                    {
                        CHWCODE = bc.bcDB.pm07DB.getChwCode(txttambox[2]);
                        AMPCODE = bc.bcDB.pm07DB.getAmpCode(txttambox[1]);
                        TAMBONCODE = bc.bcDB.pm07DB.getTambonCode(txttambox[0]);
                        txtPttCurSearchTambon.Value = txttambox[0].Trim();
                        txtPttCurAmp.Value = txttambox[1].Trim();
                        txtPttCurChw.Value = txttambox[2].Trim();
                        //txtPttCurPostcode.Value = bc.bcDB.pm07DB.getPostCode(txttambox[2]);
                        txtPttCurPostcode.Value = txttambox[3].Trim();
                    }
                }
            }
        }
        private void TxtPttIdSearchTambon_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (txtPttIdSearchTambon.Text.Trim().Length > 0)
                {
                    String[] txttambox = txtPttIdSearchTambon.Text.Trim().Split(',');
                    if(txttambox.Length > 0)
                    {
                        CHWCODE = bc.bcDB.pm07DB.getChwCode(txttambox[2]);
                        AMPCODE = bc.bcDB.pm07DB.getAmpCode(txttambox[1]);
                        TAMBONCODE = bc.bcDB.pm07DB.getTambonCode(txttambox[0]);
                        txtPttIdSearchTambon.Value = txttambox[0].Trim();
                        txtPttIdAmp.Value = txttambox[1].Trim();
                        txtPttIdChw.Value = txttambox[2].Trim();
                        //txtPttIDPostcode.Value = bc.bcDB.pm07DB.getPostCode(txttambox[2]);
                        txtPttIDPostcode.Value = txttambox[3].Trim();
                    }
                }
            }
        }
        private void CboApmPaid_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            //pageLoad=true;
            setGrfApm();
            //pageLoad =false;
        }
        private void TxtVsPaidCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //bc.setC1Combo(cboVsPaid, txtVsPaidCode.Text.Trim());
                if (int.TryParse(txtVsPaidCode.Text, out _))//ป้อนเป็น ตัวเลข
                {
                    txtVsPaidCode.Value = bc.bcDB.finM02DB.getPaidName(txtVsPaidCode.Text.Trim());
                }
                //โปรแกรม ช่วยตรวจสอบ และแสดง message
                String chk = bc.bcDB.finM02DB.getPaidCode(txtVsPaidCode.Text.Trim());
                lfSbMessage.Text = "เลือกสิทธิการรักษา "+chk;
            }
        }
        private void BtnPttClearData_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControl();            clearControlTabVisit();            clearTxtUser();
        }
        private void TxtApmHn_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))            {                e.Handled = true;            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))            {                e.Handled = true;            }
        }
        private void TxtPttDOBYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))            {                e.Handled = true;            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))            {                e.Handled = true;            }
        }
        private void TxtPttDOBMM_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            lfSbMessage.Text = "";
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))            {                e.Handled = true;            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))            {                e.Handled = true;            }
        }
        private void TxtPttDOBDD_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            lfSbMessage.Text = "";
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))            {                e.Handled = true;            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))            {                e.Handled = true;            }
        }
        private void BtnApmSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfApm();
        }
        private void ChkApmHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setTabApmCheck();
        }
        private void ChkApmDate_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setTabApmCheck();
        }
        private void BtnSrcCardRead_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            clearControl();
            int ret = 0;        // ทำเพื่อ รองรับ  smartcartd แบบอ่านเอง ไม่ผ่าน DLLของ rd-comp
            ret = ReadCardThaiID();
            if (ret != 0) { ReadCard(); txtSrcHn.Focus(); return; }
            setControlTabPTT();
        }
        private void setControlTabPTT()
        {
            Patient ptt = new Patient();
            try
            {
                clearControl();
                ptt = bc.bcDB.pttDB.selectPatinetByPID(txtPttPID.Text.Trim(), "pid");
                if (ptt.MNC_HN_NO.Length <= 0) FLAGPTTNEW = true; else FLAGPTTNEW = false;
            }
            catch (Exception ex) { lfSbMessage.Text = ex.Message; new LogWriter("e", "FrmReception BtnSrcCardRead_Click " + ex.Message); bc.bcDB.insertLogPage(bc.userId, this.Name, "setPttAge", ex.Message); }
            if (setControl(ptt, "smartcard")) tC.SelectedTab = tabPtt;
        }
        private void BtnPttInsur_MouseHover(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            stt.Show("<p> Double Click เพื่อ เพิ่มข้อมูลประกัน </p>", btnPttInsur, 1000);
        }
        private void BtnPttInsur_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmCompAdd frm = new FrmCompAdd(bc, txtPttInsur.Text.Trim());
            frm.ShowDialog(this);
            autoInsur = bc.bcDB.pm24DB.setAutoInsur(true);
            autoComp = bc.bcDB.pm24DB.getlPaid1(true);
            txtVsInsur.AutoCompleteCustomSource = autoInsur;
            txtVsComp.AutoCompleteCustomSource = autoComp;
            txtPttInsur.AutoCompleteCustomSource = autoInsur;
            txtPttCompCode.AutoCompleteCustomSource = autoComp;
            txtPttInsur.Value = bc.COMPNAME;
        }
        private void BtnPttComp_MouseHover(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            stt.Show("<p> Double Click เพื่อ เพิ่มข้อมูลบริษัท </p>", btnPttInsur, 1000);
        }
        private void BtnPttComp_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmCompAdd frm = new FrmCompAdd(bc, txtPttCompCode.Text.Trim());
            frm.ShowDialog(this);
            autoInsur = bc.bcDB.pm24DB.setAutoInsur(true);
            autoComp = bc.bcDB.pm24DB.getlPaid1(true);
            txtVsInsur.AutoCompleteCustomSource = autoInsur;
            txtVsComp.AutoCompleteCustomSource = autoComp;
            txtPttInsur.AutoCompleteCustomSource = autoInsur;
            txtPttCompCode.AutoCompleteCustomSource = autoComp;
            txtPttCompCode.Value= bc.COMPNAME;
        }
        private void TxtPttNickName_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                cboPttPrefixT.Focus();            }
        }
        private void TxtPttInsur_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            findCust = false;            findInsur = true;
        }
        private void TxtPttCompCode_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            findCust = true;            findInsur = false;
        }
        private void TxtPttDOBYear_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                setPttAge();                txtPttAge.Focus();
            }
            else if (txtPttDOBYear.Text.Trim().Length >= 4)
            {
                setPttAge();                txtPttNickName.SelectAll();                txtPttNickName.Focus();
            }
        }
        private void setPttAge()
        {
            String err = "";
            lfSbStatus.Text = "";
            lfSbMessage.Text = "";
            try
            {
                DateTime dtdob = new DateTime();
                String dob = "";
                int.TryParse(txtPttDOBYear.Text.Trim(), out int year);
                if ((year <2500) && (year>DateTime.Now.Year))
                {
                    lfSbMessage.Text = "ปีเกิด มากกว่าปีปัจจุบัน";
                    txtPttDOBYear.Focus();
                    return;
                }
                if ((DateTime.Now.Year - year) >120)
                {
                    lfSbMessage.Text = "อายุมากกว่า 120 ปี";
                    txtPttDOBYear.Focus();
                    return;
                }
                dob = txtPttDOBYear.Text.Trim() + "-" + txtPttDOBMM.Text.Trim() + "-" + txtPttDOBDD.Text.Trim();
                DateTime.TryParse(dob, out dtdob);
                if (dtdob.Year > 2450)
                {
                    dtdob = dtdob.AddYears(-543);
                }
                else if (dtdob.Year < 1900)
                {
                    dtdob = dtdob.AddYears(543);
                }
                err = "01";
                if (ptt == null) ptt = new Patient();
                txtPttDOB.Value = dtdob;
                ptt.patient_birthday = dtdob.Year.ToString() + "-" + dtdob.ToString("MM-dd");
                txtPttAge.Value = ptt.AgeStringShort1();
            }
            catch(Exception ex)
            {
                lfSbStatus.Text = err+ "setPttAge ";
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", "FrmReception setPttAge " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "setPttAge", ex.Message);
            }
        }
        private void TxtPttDOBMM_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(txtPttDOBMM.Text.Trim(), out int chk))
                {
                    if (chk < 13) txtPttDOBYear.Focus();
                    else
                    {
                        lfSbMessage.Text = "เดือนเกิด ไม่ถูกต้อง";
                        txtPttDOBMM.Focus();
                    }
                }
            }
            else if (txtPttDOBMM.Text.Trim().Length >= 2)
            {
                if (int.TryParse(txtPttDOBMM.Text.Trim(), out int chk))
                {
                    if (chk < 13) txtPttDOBYear.Focus();
                    else
                    {
                        lfSbMessage.Text = "เดือนเกิด ไม่ถูกต้อง";
                        txtPttDOBMM.Focus();
                    }
                }
            }
        }
        private void TxtPttDOBDD_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if ((e.KeyCode == Keys.Enter) && (txtPttDOBDD.Text.Length==2))
            {
                if (int.TryParse(txtPttDOBDD.Text.Trim(), out int chk))
                {
                    if (chk < 32) txtPttDOBMM.Focus();
                    else
                    {
                        lfSbMessage.Text = "วันเกิด ไม่ถูกต้อง";
                        txtPttDOBDD.Focus();
                    }
                }
            }
            else if (txtPttDOBDD.Text.Trim().Length >= 2)
            {
                if(int.TryParse(txtPttDOBDD.Text.Trim(), out int chk))
                {
                    if (chk < 32) txtPttDOBMM.Focus();
                    else
                    {
                        lfSbMessage.Text = "วันเกิด ไม่ถูกต้อง";
                        txtPttDOBDD.Focus();
                    }
                }
            }
        }
        private void setCboVsType()
        {
            if ((pageLoad==false) && (cboVsType.SelectedIndex <= 0))
            {
                cboVsType.SelectedIndex = 1;
                if(cboVsType.Text.Length==0) cboVsType.Text = "ตรวจปกติ Normal";
            }
        }
        private void setPttnameTabVs(String symptoms)
        {
            txtVsHN.Value = txtPttHn.Text.Trim();
            lbVsPttNameT.Text = cboPttPrefixT.Text + " " + txtPttNameT.Text.Trim() + " " + txtPttSurNameT.Text.Trim();
            lbVsPttNameE.Text = cboPttPrefixE.Text.Trim() + " " + txtPttNameE.Text.Trim() + " " + txtPttSurNameE.Text.Trim();
            lbVsPaidNameT.Text = txtPttPaid.Text.Trim();
            picVsPtt.Image = m_picPhoto.Image != null ? m_picPhoto.Image: null;
            txtVsPttAttchNote.Value = txtPttAttchNote.Text;
            txtVsPttRemark1.Value = txtPttRemark1.Text;
            txtVsPttRemark2.Value = txtPttRemark2.Text;
            txtVsSymptom.Value = symptoms;
            //ComboBoxItem item = new ComboBoxItem();
            //if(cboPttPaid.SelectedItem != null){
            //    item = (ComboBoxItem)cboPttPaid.SelectedItem;

            //}
            txtVsPaidCode.Value = txtPttPaid.Text;
            txtVsComp.Value = txtPttCompCode.Text;
            txtVsInsur.Value = txtPttInsur.Text;
            setCboVsType();
            setGrfVsPttVisit();
        }
        private void BtnPttVisit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setTabVisit();
        }
        private void setTabVisit()
        {
            String err = "";
            try
            {
                wantEditVisit = false;
                VSDATE = "";
                PRENO = "";
                err = "01";
                PatientT07 pt07 = new PatientT07();
                String symptoms = "";
                pt07 = bc.bcDB.pt07DB.selectAppointmentDate(txtPttHn.Text.Trim(), DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd"));
                if (pt07 != null)
                {
                    if (pt07.MNC_DOC_NO.Length > 0)
                    {
                        lbVsStatus.Text = "วันนี้ คนไข้มีนัด " + pt07.MNC_APP_DSC.Replace("\r\n", "") + " [" + bc.bcDB.pm32DB.getDeptName(pt07.MNC_SEC_NO) + "]";
                        //txtVsPaidCode.Value = bc.bcDB.pm32DB.getDeptName(pt07.MNC_SEC_NO);
                        bc.setC1Combo(cboVsDept, bc.adjustSecNoOPD(pt07.MNC_SECR_NO));
                        symptoms = pt07.MNC_APP_DSC.Replace("\r\n", "");
                    }
                }
                else
                {
                    lbVsStatus.Text = "";
                }
                setPttnameTabVs(symptoms);
                err = "02";
                btnVsSave.Text = "ส่งตัว";
                tC.SelectedTab = tabVs;
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = "err " + err + " " + ex.Message;
                new LogWriter("e", "FrmReception BtnPttVisit_Click err " + err + " " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPttVisit_Click ", "err " + err + " " + ex.Message);
            }
        }
        private void TxtPttPID_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //ถ้าเป็นคนไข้ใหม่ ต้องค้น PID อีกครั้ง
                //2567-03-14  ไม่ต้อง check PID PID สามารถ ว่างได้ เพราะ ถ้าเป็นคนไข้ ต่างด้าว จะไม่มี PID
                //if (FLAGPTTNEW)
                //{
                //    Patient ptt = bc.bcDB.pttDB.selectPatinetByPID(txtPttPID.Text.Trim(), "pid");
                //    if (ptt.Hn.Length > 0)
                //    {
                //        if (MessageBox.Show("PID มีข้อมูลแล้ว\nHN " + ptt.Hn + " " + ptt.Name + "\n ต้องการดึงข้อมูลไหม?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //        {
                //            setControlTabPateint(ptt);
                //        }
                //        else
                //        {
                //            txtPttPID.Focus();
                //        }
                //    }
                //    else
                //    {
                //        lfSbMessage.Text = "ไม่พบ PID";
                //        txtPttSsn.Focus();
                //    }
                //}
                txtPttSsn.Value = txtPttPID.Text; txtPttSsn.Focus();
                
            }
        }
        private void TxtPttPID_LostFocus(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if(txtPttPID.Text.Trim().Length > 0)
            //{
            //    Patient ptt = bc.bcDB.pttDB.selectPatinetByPID(txtPttPID.Text.Trim(), "pid");
            //    if (ptt.Hn.Length > 0)
            //    {
            //        if (MessageBox.Show("PID มีข้อมูลแล้ว\nHN " + ptt.Hn + " " + ptt.Name + "\n ต้องการดึงข้อมูลไหม?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            setControlTabPateint(ptt);
            //        }
            //        else
            //        {
            //            txtPttPID.Focus();
            //        }
            //    }
            //    else
            //    {
            //        lfSbMessage.Text = "ไม่พบ PID";
            //    }
                
            //}
        }
        private void TxtVsUser_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                lbVsUser.Text = bc.bcDB.stfDB.selectByPassword(txtPttUser.Text.Trim());
                if (lbVsUser.Text.Length > 0)
                {
                    if (wanttoVisit)
                    {
                        BtnVsSave_Click(null, null);
                    }
                }
            }
        }
        private void CboVsType_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            txtVsUser.SelectAll();
            txtVsUser.Focus();
        }

        private void CboVsDtr_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            cboVsType.Focus();
        }

        private void TxtVsNote_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                txtVsUser.SelectAll();
                txtVsUser.Focus();
            }
        }

        private void TxtVsRemark_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtVsNote.SelectAll();
                txtVsNote.Focus();
            }
        }
        private void TxtVsSymptom_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtVsRemark.SelectAll();
                txtVsRemark.Focus();
            }
        }

        private void CboVsDept_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            txtVsSymptom.SelectAll();
            txtVsSymptom.Focus();
        }

        private void CboVsPaid_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            cboVsDept.Focus();
        }
        private void TxtPttUser_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                lbPttUser.Text = bc.bcDB.stfDB.selectByPassword(txtPttUser.Text.Trim());
                if(lbPttUser.Text.Length > 0)
                {
                    if (wanttoSave)
                    {
                        BtnPttSave_Click(null, null);
                    }
                }
            }
        }
        private void TxtPttDOB_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            DateTime dob = new DateTime();
            DateTime.TryParse(txtPttDOB.Value.ToString(), out dob);
            Patient ptt = new Patient();
            if (dob.Year < 1900)
            {
                dob.AddYears(543);
            }
            ptt.patient_birthday = dob.Year + "-" + dob.ToString("MM-dd");
            txtPttAge.Value = ptt.AgeStringShort1();
            cboPttPrefixT.Focus();
        }

        private void TxtPttwp3_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttEmail.SelectAll();
                txtPttEmail.Focus();
            }
        }

        private void TxtPttwp2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttMobile2.SelectAll();
                txtPttMobile2.Focus();
            }
        }

        private void TxtPttwp1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPttwp2.SelectAll();
                txtPttwp2.Focus();
            }
        }
        private void BtnVsSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String err = "", re="";
            try
            {
                lfSbMessage.Text = "";
                lfSbStatus.Text = "";
                if (ptt == null)
                {
                    MessageBox.Show("error patient", "");
                    return;
                }
                //ตรวจสอบ patient admit     select mnc_an_no,mnc_an_yr from patient_t08 Where (mnc_hn_no = '+inttostr(pat_no)' and (mnc_ds_cd is null or mnc_ds_cd = '''' ))';
                //ตรวจสอบ ค้างชำระ  SELECT_PATT01_NOTSEL sts := DataModule1.Qp_t01.FieldByName('MNC_STS').Asstring;  if (pre<>'') and (sts<>'F') then 
                //if ((sts = 'B') or (sts = 'N') or (sts = 'P') or (sts = 'U') or (sts='R') or (sts='A') or (sts='S') ) then
                //ถ้า หมอ มีค่าว่าง IF CboDotCD.TEXT = '' THEN  MNC_DOT_CD:= '00000'
                if(haveEditPtt)
                {
                    if (!setPattient()) return;
                    err = "01";
                    re = bc.bcDB.pttDB.insertPatientStep1(ptt);
                    if (int.TryParse(re, out int chk2))
                    {

                    }
                }
                err = "011";
                Visit vs = setVisit();
                err = "02";
                if (vs.HN.Length < 5) {                     lfSbStatus.Text = "HN";                 return;                }
                if (vs.symptom.Length <= 0) {               lfSbStatus.Text = "อาการ";             txtVsSymptom.Focus();                    return;                }
                if (vs.DeptCode.Length <= 0)
                {
                    lfSbStatus.Text = "ส่งตัวไป";
                    cboVsDept.Focus();
                    return;
                }
                if (vs.PaidCode.Length <= 1)
                {
                    lfSbStatus.Text = "รักษาด้วยสิทธิ";
                    txtVsPaidCode.Focus();
                    return;
                }
                //ต้อง check เพราะมีการป้อน สิทธิ ที่ไม่มี เช่น -, **  โปรแกรมก็ให้ผ่าน
                String chk1 = "";
                chk1 = bc.bcDB.finM02DB.getPaidName(vs.PaidCode);
                if (chk1.Length <= 0)
                {
                    lfSbStatus.Text = "รักษาด้วยสิทธิ";
                    txtVsPaidCode.Focus();
                    return;
                }
                if (vs.VisitType.Length <= 0)
                {
                    lfSbStatus.Text = "ประเภทการตรวจ";
                    cboVsType.Focus();
                    return;
                }
                if (txtVsUser.Text.Length == 0)
                {
                    wanttoVisit = true;
                    lfSbStatus.Text = "กรุณาป้อน รหัสผู้ส่งตัว";
                    txtVsUser.Focus();
                    return;
                }
                err = "03";
                if (wantEditVisit)
                {
                    String deptno = bc.bcDB.pm32DB.selectDeptOPDBySecNO(vs.DeptCode);
                    re = bc.bcDB.vsDB.updateVisit(vs.HN, VSDATE, PRENO, vs.PaidCode, vs.symptom, deptno, vs.DeptCode, vs.VisitType, vs.remark, vs.compcode, vs.insurcode);
                    if (chkVsStatusDOE.Checked)
                    {
                        bc.bcDB.vsDB.updateVisitStatusDOE(vs.HN, VSDATE, PRENO, "1", "", "");
                    }
                }
                else
                {
                    re = bc.bcDB.vsDB.insertVisit1(vs.HN, vs.PaidCode, vs.symptom, vs.DeptCode, vs.remark, vs.DoctorId, vs.VisitType, vs.compcode, vs.insurcode, txtVsUser.Text.Trim());
                    if (chkVsStatusDOE.Checked)
                    {
                        bc.bcDB.vsDB.updateVisitStatusDOE(vs.HN, DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd"), re, "1", "", "");
                    }
                }
                //และเข้าในว่า ต้องแก้ store procedure แก้ store procedure ต้องแก้โปรแกรมด้วยไหม ต้องหาคำตอบ
                
                err = "04";
                QUENO = bc.bcDB.vsDB.selectVisitQUE(vs.HN, DateTime.Now.Year.ToString()+"-"+DateTime.Now.ToString("MM-dd"),re);
                QUEFullname = lbVsPttNameT.Text;
                QUEHN = vs.HN;
                QUESymptoms = vs.symptom;
                QUEDEPT = bc.bcDB.pm32DB.getDeptNameOPD(vs.DeptCode);
                if (int.TryParse(re, out int chk))
                {
                    if (chkPrnQue.Checked)
                    {
                        printQueue();
                    }
                    lfSbMessage.Text = wantEditVisit ? "แก้ไข visit เรียบร้อย" : "ส่ง visit เรียบร้อย";
                    lfSbStatus.Text = "OK";
                    setGrfVsPttVisit();
                    clearControlTabVisit();
                }
                else
                {
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnVsSave_Click ", re);
                    lfSbMessage.Text = re;
                }
            }
            catch(Exception ex)
            {
                lfSbMessage.Text = "err " + err + " " + ex.Message;
                new LogWriter("e", "FrmReception BtnVsSave_Click err " + err + " " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnVsSave_Click ", "err " + err + " " + ex.Message);
            }
        }
        private void BtnDocSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setTiltImg(txtDocHn.Text.Trim());
        }
        private Boolean checkVisit()
        {
            if(txtVsPaidCode.Text.Length<=0)
            {
                lfSbMessage.Text = "กรุณาเลือก รักษาด้วยสิทธิ ";
                tC.SelectedTab = tabVs;
                return false;
            }
            if (cboVsDept.SelectedItem == null)
            {
                lfSbMessage.Text = "กรุณาเลือก ส่งตัวไป ";
                tC.SelectedTab = tabVs;
                return false;
            }
            if (cboVsType.SelectedItem == null)
            {
                lfSbMessage.Text = "กรุณาเลือก ประเภทการตรวจ ";
                tC.SelectedTab = tabVs;
                return false;
            }
            return true;
        }
        private Visit setVisit()
        {
            String err = "";
            Visit vs = new Visit();
            if (!checkVisit()) return vs;
            try
            {
                vs.HN = txtVsHN.Text.Trim();
                vs.PaidCode = bc.bcDB.finM02DB.getPaidCode(txtVsPaidCode.Text.Trim());
                vs.symptom = txtVsSymptom.Text.Trim();
                err = "01";
                vs.DeptCode = cboVsDept.SelectedItem == null ? "" : ((ComboBoxItem)cboVsDept.SelectedItem).Value;
                vs.remark = txtVsRemark.Text.Trim();
                err = "02";
                vs.VisitType = cboVsType.SelectedItem == null ? "" : ((ComboBoxItem)cboVsType.SelectedItem).Value;//ใน source  Fieldนี้ MNC_PT_FLG
                vs.DoctorId = cboVsDtr.SelectedItem == null ? "" : cboVsDtr.SelectedItem.Equals("") ? "" : ((ComboBoxItem)cboVsDtr.SelectedItem).Value;
                vs.DoctorId = vs.DoctorId.Length == 0 ? "00000" : vs.DoctorId;      //IF CboDotCD.TEXT = '' THEN MNC_DOT_CD:= '00000'
                vs.VisitNote = txtVsNote.Text.Trim();
                if (vs.PaidCode.Equals("02"))
                {//สิทธิ เงินสด ให้เอาชื่อบริษัทออก
                    vs.compcode = "";
                    vs.insurcode = "";
                }
                else
                {
                    vs.compcode = bc.bcDB.pm24DB.getPaidCode(txtVsComp.Text.Trim());
                    vs.insurcode = bc.bcDB.pm24DB.getPaidCode(txtVsInsur.Text.Trim());
                }
                err = "03";
                //MNC_FIX_DOT_CD := edtDotcd2.TEXT  แพทย์เจ้าของไข้
                vs.DoctorOwn = "";
                vs.status_doe = chkVsStatusDOE.Checked ? "1" : "0";
                lfSbMessage.Text = vs.VisitType;
                //IF MNC_DOT_CD = '00000' THEN         MNC_REQ_DOT_STS:= 'O'          else MNC_REQ_DOT_STS:= 'R';
                //if (cboType.text = 'A') or (cboType.text = 'F') then Appointment
                ////RUN Query ของแผนกการตรวจ
                //S:= ' SELECT *  FROM SUMMARY_T02 ';
                //S:= S + ' WHERE ((MNC_SUM_DAT = :M_SUM_DAT) ';
                //S:= S + ' AND (MNC_DEP_NO = :M_DEP_NO) ';
                //S:= S + ' AND (MNC_SEC_NO = :M_SEC_NO)) ';
                //PATIENT_SEND.MNC_QUE_NO := FIELDBYNAME('MNC_SUM_VN_ADD').ASINTEGER + 1;
                //PATIENT_SEND.MNC_PRE_NO := RunNumByDate_notrn('OPD', 'ODR', 'RUN VN SYSTEM', FORMATDATETIME('EEEE', DATE), PATIENT_SEND.MNC_DATE, datamodule1.RQuery);
            }
            catch (Exception ex)
            {
                lfSbStatus.Text = ex.Message.ToString();
                lfSbMessage.Text = err + " setVisit " + ex.Message;
                new LogWriter("e", "FrmReception setVisit " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "setVisit", ex.Message);
            }
            return vs;
        }
        private Boolean setPattient()
        {
            String err = "00";
            try
            {
                DateTime dob = new DateTime();
                DateTime.TryParse(txtPttDOB.Value.ToString(), out dob);
                if (dob.Year < 1900)                {                    dob = dob.AddYears(543);                }
                if(dob.Year < 1900)
                {
                    lfSbMessage.Text = "วัน เดือน ปี เกิด ไม่ถูกต้อง";
                    txtPttDOBDD.Focus();
                    tC.SelectedTab = tabPtt;
                    return false;
                }
                if (dob.Year >2450)                {                    dob = dob.AddYears(-543);                }
                err = "01";
                ptt = new Patient();
                ptt.MNC_HN_NO = txtPttHn.Text.Trim();
                ptt.passport = txtPttPassport.Text.Trim();
                ptt.MNC_HN_YR = "";
                ptt.MNC_PFIX_CDT = cboPttPrefixT.SelectedItem == null ? "" : ((ComboBoxItem)cboPttPrefixT.SelectedItem).Value;
                ptt.MNC_PFIX_CDE = cboPttPrefixE.SelectedItem == null ? "" : ((ComboBoxItem)cboPttPrefixE.SelectedItem).Value;
                err = "02";
                ptt.MNC_FNAME_T = txtPttNameT.Text.Trim();
                ptt.MNC_LNAME_T = txtPttSurNameT.Text.Trim();
                ptt.MNC_FNAME_E = txtPttNameE.Text.Trim();
                ptt.MNC_LNAME_E = txtPttSurNameE.Text.Trim();
                if ((ptt.MNC_FNAME_T.Length <= 0) && (ptt.MNC_FNAME_E.Length <= 0))
                {
                    lfSbMessage.Text = "ชื่อ ไม่ถูกต้อง";
                    txtPttNameT.Focus();
                    tC.SelectedTab = tabPtt;
                    return false;
                }
                ptt.MNC_AGE = "";
                ptt.MNC_BDAY = dob.Year.ToString() + "-" + dob.ToString("MM-dd");
                ptt.MNC_ID_NO = txtPttPID.Text.Trim();
                if (ptt.MNC_ID_NO.Length <= 0)
                {
                    lfSbMessage.Text = "PID ไม่ถูกต้อง";
                    txtPttPID.Focus();
                    tC.SelectedTab = tabPtt;
                    //return false;
                }
                if (ptt.MNC_PFIX_CDT.Length <= 0)
                {
                    lfSbMessage.Text = "คำนำหน้าชื่อ ไม่ถูกต้อง";
                    cboPttPrefixT.Focus();
                    tC.SelectedTab = tabPtt;
                    return false;
                }
                ptt.MNC_SS_NO = txtPttSsn.Text.Trim();
                ptt.MNC_SEX = cboPttSex.Text;
                ptt.MNC_FULL_ADD = "";
                ptt.MNC_STAMP_DAT = "";
                ptt.MNC_STAMP_TIM = "";
                err = "03";
                //dfm.doc_group_sub_id = cboDocGroupSubName.SelectedItem == null ? "" : ((ComboBoxItem)cboDocGroupSubName.SelectedItem).Value;
                ptt.MNC_CUR_ADD = txtPttCurHomeNo.Text.Trim();
                ptt.MNC_CUR_MOO = txtPttCurMoo.Text.Trim();
                ptt.MNC_CUR_SOI = txtPttCurSoi.Text.Trim();
                ptt.MNC_CUR_ROAD = txtPttCurRoad.Text.Trim();
                ptt.MNC_CUR_TUM = bc.bcDB.pm07DB.getTambonCode(txtPttCurSearchTambon.Text.Trim());
                ptt.MNC_CUR_AMP = bc.bcDB.pm07DB.getAmpCode(txtPttCurAmp.Text.Trim());
                ptt.MNC_CUR_CHW = bc.bcDB.pm07DB.getChwCode(txtPttCurChw.Text.Trim());
                ptt.MNC_CUR_POC = txtPttCurPostcode.Text.Length <= 5 ? txtPttCurPostcode.Text.Trim() : txtPttCurPostcode.Text.Substring(0, 5);
                ptt.MNC_CUR_TEL = txtPttMobile1.Text.Trim();
                err = "04";
                ptt.MNC_DOM_ADD = txtPttIDHomeNo.Text.Trim();
                ptt.MNC_DOM_MOO = txtPttIDMoo.Text.Trim();
                ptt.MNC_DOM_SOI = txtPttIDSoi.Text.Trim();
                ptt.MNC_DOM_ROAD = txtPttIDRoad.Text.Trim();
                ptt.MNC_DOM_TUM = bc.bcDB.pm07DB.getTambonCode(txtPttIdSearchTambon.Text.Trim());
                ptt.MNC_DOM_AMP = bc.bcDB.pm07DB.getAmpCode(txtPttIdAmp.Text.Trim());
                ptt.MNC_DOM_CHW = bc.bcDB.pm07DB.getChwCode(txtPttIdChw.Text.Trim());
                if (txtPttIdChw.Text.Equals("กรุงเทพมหานคร")) txtPttIdChw.Value = "กรุงเทพ ฯ";
                ptt.MNC_DOM_CHW = bc.bcDB.pm07DB.getChwCode(txtPttIdChw.Text.Trim());
                ptt.MNC_DOM_POC = txtPttIDPostcode.Text.Length <= 5 ? txtPttIDPostcode.Text.Trim() : txtPttIDPostcode.Text.Substring(0, 5);
                ptt.MNC_DOM_TEL = "";
                err = "05";
                ptt.MNC_REF_NAME = txtPttRefContact1Name.Text.Trim();
                ptt.MNC_REF_ADD = txtPttRefHomeNo.Text.Trim();
                ptt.MNC_REF_MOO = txtPttRefMoo.Text.Trim();
                ptt.MNC_REF_SOI = txtPttRefSoi.Text.Trim();
                ptt.MNC_REF_ROAD = txtPttRefRoad.Text.Trim();
                ptt.MNC_REF_TUM = bc.bcDB.pm07DB.getTambonCode(txtPttRefSearchTambon.Text.Trim());
                ptt.MNC_REF_AMP = bc.bcDB.pm07DB.getAmpCode(txtPttRefAmp.Text.Trim());
                ptt.MNC_REF_CHW = bc.bcDB.pm07DB.getChwCode(txtPttRefChw.Text.Trim());
                ptt.MNC_REF_POC = txtPttRefPostcode.Text.Length <= 5 ? txtPttRefPostcode.Text.Trim() : txtPttRefPostcode.Text.Substring(0, 5);
                ptt.MNC_REF_TEL = txtPttRefContact1Mobile.Text.Trim();
                ptt.MNC_REF_REL = txtPttRefContact1Rel.Text.Trim();
                err = "06";
                ptt.MNC_COM_CD = bc.bcDB.pm24DB.getPaidCodeCopilot(txtPttInsur.Text.Trim());           //มีแจ้ง error ว่า save แล้ว บริษัทหาย ได้ลอง debug เช่น aIa ค้นไม่เจอ
                ptt.MNC_COM_CD2 = bc.bcDB.pm24DB.getPaidCodeCopilot(txtPttCompCode.Text.Trim());
                ptt.WorkPermit1 = txtPttwp1.Text.Trim();
                ptt.WorkPermit2 = txtPttwp2.Text.Trim();
                ptt.WorkPermit3 = txtPttwp3.Text.Trim();
                ptt.MNC_FN_TYP_CD =bc.bcDB.finM02DB.getPaidCode( txtPttPaid.Text.Trim());
                ptt.MNC_ATT_NOTE = txtPttAttchNote.Text.Trim();
                ptt.remark1 = txtPttRemark1.Text.Trim();
                ptt.remark2 = txtPttRemark2.Text.Trim();
                err = "07";
                ptt.MNC_NAT_CD = cboPttNat.SelectedItem == null ? "" : ((ComboBoxItem)cboPttNat.SelectedItem).Value;
                ptt.MNC_NATI_CD = cboPttRace.SelectedItem == null ? "" : ((ComboBoxItem)cboPttRace.SelectedItem).Value;
                ptt.MNC_REL_CD = cboPttRel.SelectedItem == null ? "" : ((ComboBoxItem)cboPttRel.SelectedItem).Value;
                ptt.MNC_STATUS = cboPttMarri.SelectedItem == null ? "" : ((ComboBoxItem)cboPttMarri.SelectedItem).Value;//สถานะการสมรส
                ptt.MNC_EDU_CD = cboPttEdu.SelectedItem == null ? "" : ((ComboBoxItem)cboPttEdu.SelectedItem).Value;

                ptt.ref1 = txtPttRef1.Text.Trim();
                ptt.passportold = txtPttPassportOld.Text.Trim();
            }
            catch(Exception  ex)
            {
                lfSbStatus.Text = ex.Message.ToString();
                lfSbMessage.Text = err+ " setPattient " + ex.Message;
                new LogWriter("e", "FrmReception setPattient " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "setPattient", ex.Message);
                return false;
            }
            return true;
        }
        private void initTileImg()
        {
            TileFoods = new C1TileControl();
            TileFoods.Orientation = LayoutOrientation.Vertical;

            peOrd = new C1.Win.C1Tile.PanelElement();
            ieOrd = new C1.Win.C1Tile.ImageElement();
            peOrd.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            peOrd.Children.Add(ieOrd);
            TileFoods.DefaultTemplate.Elements.Add(peOrd);
            TileFoods.Dock = DockStyle.Fill;

            //TileFoods[i].Templates.Add(this.tempFlickr);
            //TileFoods = new C1TileControl();
            //TileFoods[i].Name = "tile" + i;
            //TileFoods[i].Dock = DockStyle.Fill;
            //TileFoods[i].BackColor = tilecolor;     // tile color
            //pnOrder.Controls.Add(TileFoods);                    
            TileFoods.ScrollOffset = 0;
            TileFoods.SurfaceContentAlignment = System.Drawing.ContentAlignment.TopLeft;
            TileFoods.Padding = new System.Windows.Forms.Padding(0);
            TileFoods.GroupPadding = new System.Windows.Forms.Padding(10);

            pnDocView.Controls.Add(TileFoods);
        }
        private void setTiltImg(String hn)
        {
            DataTable dtrow = new DataTable();
            //lFoot = new List<FoodsTopping>();
            TileFoods.BeginUpdate();
            dtrow = bc.bcDB.dscDB.selectStatus4ByHn(hn);
            //lFoot = mposC.mposDB.footpDB.getlFooSpecByFooId(fooId);
            //lFoos = lfooC1;
            Group gr1 = new Group();
            TileFoods.Groups.Add(gr1);
            TileCollection tiles = TileFoods.Groups[0].Tiles;
            tiles.Clear(true);
            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
            //foreach (DataRow drow in dt.Rows)
            foreach (DataRow foos in dtrow.Rows)
            {
                var tile = new Tile();
                tile.HorizontalSize = 5;
                tile.VerticalSize = 4;
                //tile.Template = tempSpec;
                tile.Text = foos["row_no"].ToString();
                //tile.Text1 = "ราคา " + foo1.foods_price;
                tile.Tag = foos;
                tile.Name = foos["doc_scan_id"].ToString();
                //tile.Click += TileTopping_Click;
                tile.Image = null;
                try
                {
                    //tile.Image = null;
                    MemoryStream stream = new MemoryStream();
                    Image loadedImage = null, resizedImage;
                    if (foos["image_path"].ToString().Equals("")) continue;
                    stream = ftp.download(foos[bc.bcDB.dscDB.dsc.folder_ftp].ToString() + "//" + foos["image_path"].ToString());
                    if (stream.Length == 0) continue;
                    loadedImage = new Bitmap(stream);
                    int newWidth = 540;
                    resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / loadedImage.Width, null, IntPtr.Zero);
                    tile.Image = resizedImage;
                    
                    tiles.Add(tile);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("" + ex.Message, "showImg");
                    new LogWriter("e", "FrmReception setTiltImg " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setTiltImg", ex.Message);
                    lfSbMessage.Text = ex.Message;
                }
                //foos.statusUs = "";
            }
            TileFoods.EndUpdate();
        }
        private void BtnPttSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            lfSbMessage.Text = "";
            lfSbStatus.Text = "";
            if (txtPttUser.Text.Length == 0)
            {
                wanttoSave = true;
                txtPttUser.Focus();
                return;
            }
            
            if (!setPattient()) return;
            String re = bc.bcDB.pttDB.insertPatientStep1(ptt);
            if(long.TryParse(re, out long chk))
            {//save รูปบัตรประชาชน    m_picPhoto
                if (m_picPhoto.Image != null)
                {
                    try
                    {
                        MemoryStream ms = new MemoryStream();
                        m_picPhoto.Image.Save(ms, ImageFormat.Bmp);     // save to stream
                        byte[] photo_aray = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(photo_aray, 0, photo_aray.Length);      // convert to bytearray
                        String re1 = bc.bcDB.pttDB.insertPatientImage(ptt.MNC_HN_NO, ptt.MNC_HN_YR, photo_aray);
                        if (long.TryParse(re1, out chk))
                        {

                        }
                        else
                        {
                            bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPttSave_Click insertPatientImage", re);
                            lfSbMessage.Text = re;
                        }
                    }
                    catch(Exception ex)
                    {
                        new LogWriter("e", "FrmReception BtnPttSave_Click " + ex.Message);
                        bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPttSave_Click save image ", ex.Message);
                        lfSbMessage.Text = ex.Message;
                    }
                }
                if (txtPttPID.Text.Trim().Length <= 0)
                {
                    ptt = bc.bcDB.pttDB.selectPatinetByPID(txtPttNameT.Text.Trim()+","+ ptt.MNC_BDAY, "nopid_newhn");
                }
                else
                {
                    ptt = bc.bcDB.pttDB.selectPatinetByPID(txtPttPID.Text.Trim(), "pid");
                }
                txtPttHn.Value = ptt.Hn;
                lfSbStatus.Text = "update OK";
                lbPttUser.Text = "";
                txtPttUser.Value = "";
            }
            else
            {
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPttSave_Click ", re);
                lfSbMessage.Text = re;
            }
        }
        private void CboTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pageLoad = true;
            setTheme(cboTheme.Text);
            pageLoad = false;
        }
        private void TxtPttHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void BtnPttCurCopyto_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            showLbLoading();
            pageLoad = true;
            txtPttRefHomeNo.Value = txtPttIDHomeNo.Text.Trim();
            txtPttRefMoo.Value = txtPttCurMoo.Text.Trim();
            txtPttRefSoi.Value = txtPttCurSoi.Text.Trim();
            txtPttRefRoad.Value = txtPttCurRoad.Text.Trim();
            txtPttRefPostcode.Value = txtPttCurPostcode.Text.Trim();
            txtPttRefChw.Value = txtPttCurChw.Text.Trim();
            txtPttRefAmp.Value = txtPttCurAmp.Text.Trim();
            txtPttRefSearchTambon.Value = txtPttCurSearchTambon.Text.Trim();
            pageLoad = false;
            hideLbLoading();
        }

        private void BtnPttIdCopyto_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            showLbLoading();
            pageLoad = true;
            txtPttCurHomeNo.Value = txtPttIDHomeNo.Text.Trim();
            txtPttCurMoo.Value = txtPttIDMoo.Text.Trim();
            txtPttCurSoi.Value = txtPttIDSoi.Text.Trim();
            txtPttCurRoad.Value = txtPttIDRoad.Text.Trim();
            txtPttCurPostcode.Value = txtPttIDPostcode.Text.Trim();

            txtPttCurChw.Value = txtPttIdChw.Text.Trim();
            txtPttCurAmp.Value = txtPttIdAmp.Text.Trim();
            txtPttCurSearchTambon.Value = txtPttIdSearchTambon.Text.Trim();
            //bc.cloneComboBox(cboPttIDTambon,ref cboPttCurTambon);
            //bc.cloneComboBox(cboPttIDAmphur,ref cboPttCurAmphur);
            //bc.cloneComboBox(cboPttIDProv,ref cboPttCurProv);
            //cboPttCurProv.SelectedIndex = cboPttIDProv.SelectedIndex;
            //cboPttCurProv.Text = cboPttIDProv.Text;
            pageLoad = false;
            hideLbLoading();
        }

        private void BtnPttLIC_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            string StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            byte[] _lic = String2Byte(StartupPath + "\\RDNIDLib.DLD");
            int res = RDNID.updateLicenseFileRD(_lic);
            if (res != 0)
            {
                string s = string.Format("Error : {0}", res);
                MessageBox.Show(s);
            }
        }
        private void checkPttInWard()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = bc.bcDB.vsDB.selectPttinWard1(txtPttHn.Text.Trim());
                if(dt.Rows.Count > 0)
                {
                    lbPttinWard.Text = "คนไข้นอนอยู่ "+dt.Rows[0]["MNC_MD_DEP_DSC"].ToString()+" admit วันที่ "+ dt.Rows[0]["MNC_AD_DATE"].ToString();//MNC_AD_DATE
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmReception checkPaidSSO " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "checkPaidSSO ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
        }
        private void checkPaidSSO(String pid)
        {
            try
            {
                setLbLoading("patient sso01");
                PrakunM01 pkunM01 = new PrakunM01();
                pkunM01 = bc.bcDB.prakM01DB.selectByPID(pid);
                if(pkunM01.Social_Card_no.Equals(""))
                {
                    pkunM01 = bc.bcDB.prakM01DB.selectByCardNo(pid);
                    if (pkunM01.Social_Card_no.Equals(""))
                    {
                        pkunM01 = bc.bcDB.prakM01DB.selectOne();
                        lbFindPaidSSO.Text = "ไม่พบ สิทธิ จาก database HIS->" + pkunM01.UploadDate;
                    }
                    else
                    {
                        String txt = pkunM01.PrakanCode.Equals("2210028") ? "สิทธิ บางนา 1" : pkunM01.PrakanCode.Equals("2211006") ? "สิทธิ บางนา 2" : pkunM01.PrakanCode.Equals("2211041") ? "สิทธิ บางนา 5" : "สิทธิ  ที่อื่น";
                        lbFindPaidSSO.Text = "พบ " + txt + " เริ่ม[" + pkunM01.StartDate + "] สิ้นสุด[" + pkunM01.EndDate + "] จาก database HIS->" + pkunM01.UploadDate;

                        if (pkunM01.PrakanCode.Equals("2211041")) txtPttPaid.Value = bc.bcDB.finM02DB.getPaidName("46");
                        else if (pkunM01.PrakanCode.Equals("2211006")) txtPttPaid.Value = bc.bcDB.finM02DB.getPaidName("45");
                        else if (pkunM01.PrakanCode.Equals("2210028")) txtPttPaid.Value = bc.bcDB.finM02DB.getPaidName("44");
                    }
                        
                }
                else
                {
                    //abel1.Text = prak.PrakanCode.Equals("2210028") ? "สิทธิ บางนา 1" : prak.PrakanCode.Equals("2211006") ? "สิทธิ บางนา 2" : prak.PrakanCode.Equals("2211041") ? "สิทธิ บางนา 5" : "สิทธิ  ที่อื่น";
                    String txt = pkunM01.PrakanCode.Equals("2210028") ? "สิทธิ บางนา 1" : pkunM01.PrakanCode.Equals("2211006") ? "สิทธิ บางนา 2" : pkunM01.PrakanCode.Equals("2211041") ? "สิทธิ บางนา 5" : "สิทธิ  ที่อื่น";
                    lbFindPaidSSO.Text = "พบ "+txt + " เริ่ม["+ pkunM01.StartDate+"] สิ้นสุด["+ pkunM01.EndDate+"] จาก database HIS->"+ pkunM01.UploadDate;

                    if (pkunM01.PrakanCode.Equals("2211041")) txtPttPaid.Value = bc.bcDB.finM02DB.getPaidName("46");
                    else if (pkunM01.PrakanCode.Equals("2211006")) txtPttPaid.Value = bc.bcDB.finM02DB.getPaidName("45");
                    else if (pkunM01.PrakanCode.Equals("2210028")) txtPttPaid.Value = bc.bcDB.finM02DB.getPaidName("44");
                    
                    //cboPttPaid.SelectedItem = item;
                }
                setLbLoading("patient sso02");
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmReception checkPaidSSO " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "checkPaidSSO ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
        }
        private void BtnPttCardRead_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int re = ReadCard();
            if(re != 0)
            {
                tC.SelectedTab = tabSrc;
                return;
            }
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetByPID(txtPttPID.Text.Trim(),"pid");//read smart card มาแล้ว ก็ได้ pid  ถ้าพบ ก็ควร update ข้อมูลจากบัตรส่ง database
            setControl(ptt,"smartcard");
        }
        private Boolean setControl(Patient ptt, String flag)
        {
            //ถ้ามาจาก smartcard ก็ควรผ่าน เพื่อเป้นการ update ข้อมูล ลง database
            String err = "";
            try
            {
                showLbLoading();
                //clearControl();
                lbPttinWard.Text = "";
                lbFindPaidSSO.Text = "";
                QUENO = "";
                txtPttHn.Value = ptt.MNC_HN_NO;
                setLbLoading("patient 01");
                if (!flag.Equals("smartcard"))
                {
                    //ควรแก้ ให้ไปใช้  setControlTabPateint เพราะจะได้ใช้ method เดียวกัน
                    //read smart card มาแล้ว ก็ได้ pid  ถ้าพบ ก็ควร update ข้อมูลจากบัตรส่ง database
                    txtPttNameT.Value = ptt.MNC_FNAME_T;
                    txtPttSurNameT.Value = ptt.MNC_LNAME_T;
                    txtPttNameE.Value = ptt.MNC_FNAME_E;
                    txtPttSurNameE.Value = ptt.MNC_LNAME_E;
                    err = "01";
                    DateTime dtime = new DateTime();
                    DateTime.TryParse(ptt.MNC_BDAY, out dtime);
                    txtPttDOB.Value = dtime;
                    txtPttPID.Value = ptt.MNC_ID_NO;
                    bc.setC1Combo(cboPttPrefixT, ptt.MNC_PFIX_CDT);
                    txtPttIDHomeNo.Value = ptt.MNC_DOM_ADD;
                    txtPttIDSoi.Value = ptt.MNC_DOM_SOI;
                    err = "02";
                    //txtPttIDRoad.Value = ptt.MNC_DOM_;
                }
                //ในส่วนตรงนี้ ใน smart card ไม่น่าจะมี ก็ให้เอาข้อมูลจาก  database มาใส่ได้เลย
                txtPttSsn.Value = ptt.MNC_SS_NO;
                txtPttPassport.Value = ptt.passport;
                setLbLoading("patient 02");

                err = "03";
                txtPttwp2.Value = ptt.WorkPermit2;
                txtPttwp3.Value = ptt.WorkPermit3;
                txtPttAttchNote.Value = ptt.MNC_ATT_NOTE;
                //txtPttCompCode.Value = ptt.MNC_COM_CD;
                //txtPttInsur.Value = ptt.MNC_COM_CD2;
                txtPttCompCode.Value = ptt.comNameT;
                txtPttInsur.Value = ptt.insurNameT;
                txtPttRefContact1Mobile.Value = ptt.MNC_REF_TEL;
                txtPttMobile1.Value = ptt.MNC_CUR_TEL;
                txtPttwp1.Value = ptt.WorkPermit1;
                txtPttwp2.Value = ptt.WorkPermit2;
                txtPttwp3.Value = ptt.WorkPermit3;
                txtPttRemark1.Value = ptt.remark1;
                txtPttRemark2.Value = ptt.remark2;
                txtPttAttchNote.Value = ptt.MNC_ATT_NOTE;
                txtPttPaid.Value = bc.bcDB.finM02DB.getPaidName(ptt.MNC_FN_TYP_CD);

                txtPttCurHomeNo.Value = ptt.MNC_CUR_ADD;
                txtPttCurMoo.Value = ptt.MNC_CUR_MOO;
                txtPttCurSoi.Value = ptt.MNC_CUR_SOI;
                txtPttCurRoad.Value = ptt.MNC_CUR_ROAD;
                txtPttCurPostcode.Value = ptt.MNC_CUR_POC;
                txtPttCurChw.Value = bc.bcDB.pm07DB.getTambonName(ptt.MNC_CUR_TUM);
                txtPttCurAmp.Value = bc.bcDB.pm07DB.getAmpName(ptt.MNC_CUR_AMP);
                txtPttCurChw.Value = bc.bcDB.pm07DB.getChwName(ptt.MNC_CUR_CHW);

                txtPttRefHomeNo.Value = ptt.MNC_REF_ADD;
                txtPttRefMoo.Value = ptt.MNC_REF_MOO;
                txtPttRefSoi.Value = ptt.MNC_REF_SOI;
                txtPttRefRoad.Value = ptt.MNC_REF_ROAD;
                txtPttRefPostcode.Value = ptt.MNC_REF_POC;
                txtPttRefSearchTambon.Value = bc.bcDB.pm07DB.getTambonName(ptt.MNC_REF_TUM);
                txtPttRefAmp.Value = bc.bcDB.pm07DB.getAmpName(ptt.MNC_REF_AMP);
                txtPttRefChw.Value = bc.bcDB.pm07DB.getChwName(ptt.MNC_REF_CHW);
                txtPttRefContact1Mobile.Value = ptt.MNC_REF_TEL;
                txtPttRefContact1Rel.Value = ptt.MNC_REF_REL;
                txtPttRefContact2Name.Value = "";
                txtPttRefContact1Name.Value = "";
                txtPttRef1.Value = ptt.ref1;
                txtPttPassportOld.Value = ptt.passportold;
                setLbLoading("patient 03");
                err = "04";
                setGrfPttVs();
                err = "05";
                setGrfPttApm();
                err = "06";
                checkPaidSSO(ptt.MNC_ID_NO);
                checkPttInWard();
            }
            catch(Exception ex)
            {
                lfSbMessage.Text = "setControl "+ err+" " + ex.Message;
                new LogWriter("e", "FrmReception setControl " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "setControl ", ex.Message);
            }
            hideLbLoading();
            return true;
        }
        private int ReadCardThaiID()
        {
            new LogWriter("e", "FrmReception ReadCardThaiID " );
            int ret = 0;
            ThaiNationalIDCardReader idreader = new ThaiNationalIDCardReader();
            Personal personal = new Personal();
            PHOTO = new PersonalPhoto(personal);
            new LogWriter("e", "FrmReception ReadCardThaiID 01");
            PHOTO = idreader.GetPersonalPhoto();
            new LogWriter("e", "FrmReception ReadCardThaiID 02");
            if (PHOTO.Photo.Length <=0)
            {
                personal = idreader.GetPersonal();
                if (personal.CitizenID.Length <= 0) { ret = 100; }
            }
            txtPttPID.Value = PHOTO.CitizenID;
            setControlCitizen(PHOTO.ThaiPersonalInfo.Prefix, PHOTO.ThaiPersonalInfo.FirstName, PHOTO.EnglishPersonalInfo.FirstName, PHOTO.ThaiPersonalInfo.LastName, PHOTO.EnglishPersonalInfo.LastName
                , PHOTO.AddressInfo.HouseNo, PHOTO.AddressInfo.Lane, "", "", PHOTO.AddressInfo.Road, PHOTO.AddressInfo.SubDistrict, PHOTO.AddressInfo.District, PHOTO.AddressInfo.Province, PHOTO.Sex, PHOTO.dobYYYY+""+ PHOTO.dobMM+""+ PHOTO.dobDD
                ,PHOTO.GetPhotoAsImage());
            return 0;
        }
        private void setControlCitizen(String prefix, String namet, String namee, String surnamet, String surnamee, String homeno, String moo, String trok, String soi, String road, String tombon, String amphoe, String province, String sex, String dobddMMyyyy, Image img)
        {
            String err = "", provincename1="", provid="", amprid="", districtid="", poc="", dob="";
            txtPttNameT.Value = namet.Trim();
            txtPttSurNameT.Value = surnamet.Trim();
            txtPttNameE.Value = namee.Trim();
            txtPttSurNameE.Value = surnamee.Trim();
            m_picPhoto.Image = null;
            //m_txtBrithDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.BIRTH_DATE]);
            string _yyyy = "", _mm = "", _dd = "";

            String addr = homeno + " " + moo + " " + trok + " " + soi + " " + road + " " + tombon + " " + amphoe + " " + province;
            err = "07";
            txtPttIDHomeNo.Value = homeno.Trim();
            txtPttIDMoo.Value = moo.Trim().Replace("หมู่ที่", "").Trim();
            txtPttIDSoi.Value = soi.Trim();
            txtPttIDRoad.Value = road.Trim();
            //txtPttIDHomeNo.Value = fields[(int)NID_FIELD.HOME_NO].Trim();

            prefix = bc.bcDB.pttDB.selectProfixId(prefix.Trim());
            provincename1 = province.Trim().Replace("จังหวัด", "");
            provid = bc.bcDB.pttDB.selectProvinceId(provincename1);
            amprid = bc.bcDB.pttDB.selectAmphurId(provid, amphoe.Trim().Replace("อำเภอ", ""));
            districtid = bc.bcDB.pttDB.selectDistrictId(provid, amprid, tombon.Trim().Replace("ตำบล", ""));
            poc = bc.bcDB.pttDB.selectPOCId(provid, amprid, districtid);
            err = "08";
            txtPttIdChw.Value = bc.bcDB.pm07DB.getChwName(provid);
            txtPttIdAmp.Value = bc.bcDB.pm07DB.getAmpName(amprid);
            txtPttIdSearchTambon.Value = bc.bcDB.pm07DB.getTambonName(districtid);
            txtPttIDPostcode.Value = bc.bcDB.pm07DB.getPostCode(txtPttIdSearchTambon.Text);
            //bc.setC1Combo(cboPttIDProv, provid);
            //bc.bcDB.pm08DB.setCboAmphurByProvCode(cboPttIDAmphur, provid, amprid);
            //bc.bcDB.pm07DB.setCboTambonByAmphrCode(cboPttIDTambon, amprid, districtid);
            txtPttIDPostcode.Value = poc;
            bc.setC1Combo(cboPttPrefixT, prefix);
            bc.setC1Combo(cboPttSex, sex.Trim() == "1" ? "M" : "F");
            //cboPttSex.Text = fields[(int)NID_FIELD.GENDER].Trim() == "1" ? "M" : "F";
            //m_txtIssueDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.ISSUE_DATE]);
            //m_txtExpiryDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.EXPIRY_DATE]);
            //if ("99999999" == m_txtExpiryDate.Text)
            //    m_txtExpiryDate.Text = "99999999 ตลอดชีพ";
            //m_txtIssueNum.Text = fields[(int)NID_FIELD.ISSUE_NUM];

            try
            {
                _yyyy = dobddMMyyyy.Substring(0, 4);
                _mm = dobddMMyyyy.Substring(4, 2);
                _dd = dobddMMyyyy.Substring(6, 2);
                dob = _yyyy + "-" + _mm + "-" + _dd;
                DateTime dtime = new DateTime();
                DateTime.TryParse(dob, out dtime);
                if (dtime.Year > 2450)
                {
                    dtime = dtime.AddYears(-543);
                }
                txtPttDOBDD.Value = _dd;
                txtPttDOBMM.Value = _mm;
                txtPttDOB.Value = dtime;
                txtPttDOBYear.Value = (dtime.Year + 543);
                setPttAge();
                Bitmap MyImage = new Bitmap(img, m_picPhoto.Width - 2, m_picPhoto.Height - 2);
                m_picPhoto.Image = (Image)MyImage;
            }
            catch (Exception ex)
            {
                //sep.SetError(fields[(int)NID_FIELD.BIRTH_DATE], "");
                lfSbMessage.Text = dobddMMyyyy + " " + ex.Message;
            }
        }
        protected int ReadCard()
        {
            //clearPatient();
            String err = "";
            pageLoad = true;
            showLbLoading();
            //if (bc.iniC.statusSmartCardNoDatabase.Equals("1"))
            //{
            //    bc.bcDB.insertLogPage(bc.userId, this.Name, "ReadCard", "read card start");
            //}
            try
            {
                setLbLoading("อ่านcard");
                String strTerminal = m_ListReaderCard.GetItemText(m_ListReaderCard.SelectedItem);
                err = "01";
                IntPtr obj = selectReader(strTerminal);
                err = "02";
                Int32 nInsertCard = 0;
                nInsertCard = RDNID.connectCardRD(obj);
                if (nInsertCard != 0)
                {
                    String m;
                    m = String.Format(" error no {0} ", nInsertCard);
                    MessageBox.Show(m);

                    RDNID.disconnectCardRD(obj);
                    RDNID.deselectReaderRD(obj);
                    hideLbLoading();
                    lfSbMessage.Text = "Read card error nInsertCard != 0";
                    return nInsertCard;
                }
                err = "03";
                //BindDataToScreen();
                byte[] id = new byte[30];
                int res = RDNID.getNIDNumberRD(obj, id);
                if (res != DefineConstants.NID_SUCCESS)
                {
                    hideLbLoading();
                    lfSbMessage.Text = "Read card error res != DefineConstants.NID_SUCCESS getNIDNumberRD";
                    return res;
                }
                String NIDNum = aByteToString(id);
                err = "04";
                byte[] data = new byte[1024];
                res = RDNID.getNIDTextRD(obj, data, data.Length);
                if (res != DefineConstants.NID_SUCCESS)
                {
                    hideLbLoading();
                    lfSbMessage.Text = "Read card error res != DefineConstants.NID_SUCCESS getNIDTextRD";
                    return res;
                }
                err = "05";
                String NIDData = aByteToString(data);
                if (NIDData == "")
                {
                    lfSbMessage.Text = "Read card error (NIDData == '')";
                    MessageBox.Show("Read Text error");
                }
                else
                {
                    setLbLoading("รับข้อมูลcard");
                    String day = "", month = "", year = "", prefix = "", provid = "", provincename1 = "", amprid = "", districtid = "", poc = "", dob = "";
                    string[] fields = NIDData.Split('#');

                    txtPttPID.Value = NIDNum;                             // or use m_txtID.Text = fields[(int)NID_FIELD.NID_Number];
                    err = "06";
                    String fullname = fields[(int)NID_FIELD.TITLE_T] + " " +
                                        fields[(int)NID_FIELD.NAME_T] + " " +
                                        fields[(int)NID_FIELD.MIDNAME_T] + " " +
                                        fields[(int)NID_FIELD.SURNAME_T];
                    txtPttNameT.Value = fields[(int)NID_FIELD.NAME_T].Trim();
                    txtPttSurNameT.Value = fields[(int)NID_FIELD.SURNAME_T].Trim();
                    txtPttNameE.Value = fields[(int)NID_FIELD.NAME_E].Trim();
                    txtPttSurNameE.Value = fields[(int)NID_FIELD.SURNAME_E].Trim();

                    //m_txtBrithDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.BIRTH_DATE]);
                    string _yyyy = "", _mm = "", _dd = "";

                    String addr = fields[(int)NID_FIELD.HOME_NO] + " " + fields[(int)NID_FIELD.MOO] + " " + fields[(int)NID_FIELD.TROK] + " " + fields[(int)NID_FIELD.SOI] + " " + fields[(int)NID_FIELD.ROAD] + " " + fields[(int)NID_FIELD.TUMBON] + " " + fields[(int)NID_FIELD.AMPHOE] + " " + fields[(int)NID_FIELD.PROVINCE];
                    err = "07";
                    txtPttIDHomeNo.Value = fields[(int)NID_FIELD.HOME_NO].Trim();
                    txtPttIDMoo.Value = fields[(int)NID_FIELD.MOO].Trim().Replace("หมู่ที่", "").Trim();
                    txtPttIDSoi.Value = fields[(int)NID_FIELD.SOI].Trim();
                    txtPttIDRoad.Value = fields[(int)NID_FIELD.ROAD].Trim();
                    //txtPttIDHomeNo.Value = fields[(int)NID_FIELD.HOME_NO].Trim();

                    prefix = bc.bcDB.pttDB.selectProfixId(fields[(int)NID_FIELD.TITLE_T].Trim());
                    provincename1 = fields[(int)NID_FIELD.PROVINCE].Trim().Replace("จังหวัด", "");
                    provid = bc.bcDB.pttDB.selectProvinceId(provincename1);
                    amprid = bc.bcDB.pttDB.selectAmphurId(provid, fields[(int)NID_FIELD.AMPHOE].Trim().Replace("อำเภอ", ""));
                    districtid = bc.bcDB.pttDB.selectDistrictId(provid, amprid, fields[(int)NID_FIELD.TUMBON].Trim().Replace("ตำบล", ""));
                    poc = bc.bcDB.pttDB.selectPOCId(provid, amprid, districtid);
                    err = "08";
                    txtPttIdChw.Value = bc.bcDB.pm07DB.getChwName(provid);
                    txtPttIdAmp.Value = bc.bcDB.pm07DB.getAmpName(amprid);
                    txtPttIdSearchTambon.Value = bc.bcDB.pm07DB.getTambonName(districtid);
                    txtPttIDPostcode.Value = bc.bcDB.pm07DB.getPostCode(txtPttIdSearchTambon.Text);
                    //bc.setC1Combo(cboPttIDProv, provid);
                    //bc.bcDB.pm08DB.setCboAmphurByProvCode(cboPttIDAmphur, provid, amprid);
                    //bc.bcDB.pm07DB.setCboTambonByAmphrCode(cboPttIDTambon, amprid, districtid);
                    txtPttIDPostcode.Value = poc;
                    bc.setC1Combo(cboPttPrefixT, prefix);
                    bc.setC1Combo(cboPttSex, fields[(int)NID_FIELD.GENDER].Trim() == "1" ? "M" : "F");
                    //cboPttSex.Text = fields[(int)NID_FIELD.GENDER].Trim() == "1" ? "M" : "F";
                    //m_txtIssueDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.ISSUE_DATE]);
                    //m_txtExpiryDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.EXPIRY_DATE]);
                    //if ("99999999" == m_txtExpiryDate.Text)
                    //    m_txtExpiryDate.Text = "99999999 ตลอดชีพ";
                    //m_txtIssueNum.Text = fields[(int)NID_FIELD.ISSUE_NUM];

                    try
                    {
                        _yyyy = fields[(int)NID_FIELD.BIRTH_DATE].Substring(0, 4);
                        _mm = fields[(int)NID_FIELD.BIRTH_DATE].Substring(4, 2);
                        _dd = fields[(int)NID_FIELD.BIRTH_DATE].Substring(6, 2);
                        dob = _yyyy + "-" + _mm + "-" + _dd;
                        DateTime dtime = new DateTime();
                        DateTime.TryParse(dob, out dtime);
                        if (dtime.Year > 2450)
                        {
                            dtime = dtime.AddYears(-543);
                        }
                        txtPttDOBDD.Value = _dd;
                        txtPttDOBMM.Value = _mm;
                        txtPttDOB.Value = dtime;
                        txtPttDOBYear.Value = (dtime.Year+543);
                        setPttAge();
                    }
                    catch (Exception ex)
                    {
                        //sep.SetError(fields[(int)NID_FIELD.BIRTH_DATE], "");
                        lfSbMessage.Text = fields[(int)NID_FIELD.BIRTH_DATE] + " " + ex.Message;
                    }
                }
                setLbLoading("ดึงรูปจากcard");
                err = "09";
                byte[] NIDPicture = new byte[1024 * 5];
                int imgsize = NIDPicture.Length;
                res = RDNID.getNIDPhotoRD(obj, NIDPicture, out imgsize);
                if (res != DefineConstants.NID_SUCCESS)
                {
                    hideLbLoading();
                    return res;
                }
                byte[] byteImage = NIDPicture;
                if (byteImage == null)
                {
                    lfSbMessage.Text = "Read Photo error";
                    MessageBox.Show("Read Photo error");
                }
                else
                {
                    //m_picPhoto
                    Image img = Image.FromStream(new MemoryStream(byteImage));
                    //m_picPhoto.Image = img;
                    Bitmap MyImage = new Bitmap(img, m_picPhoto.Width - 2, m_picPhoto.Height - 2);
                    m_picPhoto.Image = (Image)MyImage;
                }
                err = "10";
                RDNID.disconnectCardRD(obj);
                RDNID.deselectReaderRD(obj);
                lfSbMessage.Text = "read card OK";
            }
            catch(Exception ex)
            {
                lfSbMessage.Text = "ReadCard err " + err+" "+ex.Message;
                new LogWriter("e", "FrmReception ReadCard err " + err + " " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "ReadCard ", "err " + err + " " + ex.Message);
            }
            lfSbMessage.Text = "Read Card OK";
            hideLbLoading();
            pageLoad = false;
            return 0;
        }
        enum NID_FIELD
        {
            NID_Number,   //1234567890123#

            TITLE_T,    //Thai title#
            NAME_T,     //Thai name#
            MIDNAME_T,  //Thai mid name#
            SURNAME_T,  //Thai surname#

            TITLE_E,    //Eng title#
            NAME_E,     //Eng name#
            MIDNAME_E,  //Eng mid name#
            SURNAME_E,  //Eng surname#

            HOME_NO,    //12/34#
            MOO,        //10#
            TROK,       //ตรอกxxx#
            SOI,        //ซอยxxx#
            ROAD,       //ถนนxxx#
            TUMBON,     //ตำบลxxx#
            AMPHOE,     //อำเภอxxx#
            PROVINCE,   //จังหวัดxxx#

            GENDER,     //1#			//1=male,2=female

            BIRTH_DATE, //25200131#	    //YYYYMMDD 
            ISSUE_PLACE,//xxxxxxx#      //
            ISSUE_DATE, //25580131#     //YYYYMMDD 
            EXPIRY_DATE,//25680130      //YYYYMMDD 
            ISSUE_NUM,  //12345678901234 //14-Char
            END
        };
        static byte[] String2Byte(string s)
        {
            // Create two different encodings.
            Encoding ascii = Encoding.GetEncoding(874);
            Encoding unicode = Encoding.Unicode;

            // Convert the string into a byte array.
            byte[] unicodeBytes = unicode.GetBytes(s);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            return asciiBytes;
        }
        public IntPtr selectReader(String reader)
        {
            IntPtr mCard = (IntPtr)0;
            byte[] _reader = String2Byte(reader);
            IntPtr res = (IntPtr)RDNID.selectReaderRD(_reader);
            if ((Int64)res > 0)
                mCard = (IntPtr)res;
            return mCard;
        }
        static string aByteToString(byte[] b)
        {
            Encoding ut = Encoding.GetEncoding(874); // 874 for Thai langauge
            int i;
            for (i = 0; b[i] != 0; i++) ;

            string s = ut.GetString(b);
            s = s.Substring(0, i);
            return s;
        }
        private void ListCardReader()
        {
            try
            {
                byte[] szReaders = new byte[1024 * 2];
                int size = szReaders.Length;
                int numreader = RDNID.getReaderListRD(szReaders, size);
                if (numreader <= 0)
                    return;
                String s = aByteToString(szReaders);
                String[] readlist = s.Split(';');
                if (readlist != null)
                {
                    for (int i = 0; i < readlist.Length; i++)
                        m_ListReaderCard.Items.Add(readlist[i]);
                    m_ListReaderCard.SelectedIndex = 0;
                }
            }
            catch(Exception ex)
            {

            }
        }
        private void TxtPttRemark1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttRemark2.SelectAll();                txtPttRemark2.Focus();            }
        }
        private void TxtPttAttchNote_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttRemark1.SelectAll();                txtPttRemark1.Focus();            }
        }
        private void CboPttMarri_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            cboPttRace.Focus();
        }

        private void CboPttEdu_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            cboPttMarri.Focus();
        }

        private void CboPttRel_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            cboPttEdu.Focus();
        }

        private void CboPttNat_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            cboPttRel.Focus();
        }

        private void TxtPttInsur_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //cboPttNat.Focus();
                if (int.TryParse(txtPttInsur.Text.Trim(), out _))//ป้อนเป็น ตัวเลข
                {
                    txtPttInsur.Value = bc.bcDB.pm24DB.getPaidName(txtPttInsur.Text.Trim());
                }
                txtPttCompCode.SelectAll();
                txtPttCompCode.Focus();
            }
            else if (txtPttInsur.Text.Trim().Length > 3) { findCust = false; findInsur = true; }
            else { findCust = false; findInsur = true;}
        }
        private void TxtPttRefContact2Mobile_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) { txtPttInsur.SelectAll(); txtPttInsur.Focus(); }
        }

        private void TxtPttRefContact2Name_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter){ txtPttRefContact2Mobile.SelectAll(); txtPttRefContact2Mobile.Focus(); }
        }

        private void TxtPttRefContact1Mobile_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) { txtPttRefContact2Name.SelectAll(); txtPttRefContact2Name.Focus();}
        }

        private void TxtPttRefContact1Name_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) { txtPttRefContact1Mobile.SelectAll(); txtPttRefContact1Mobile.Focus(); }
        }

        private void CboPttRefTambon_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();            
            txtPttRefContact1Name.SelectAll();            txtPttRefContact1Name.Focus();
        }

        private void TxtPttRefRoad_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) { txtPttRefSearchTambon.Focus(); }
        }

        private void TxtPttRefSoi_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) { txtPttRefRoad.SelectAll(); txtPttRefRoad.Focus(); }
        }

        private void TxtPttRefMoo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) { txtPttRefSoi.SelectAll(); txtPttRefSoi.Focus();}
        }

        private void TxtPttRefHomeNo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter){ txtPttRefMoo.SelectAll(); txtPttRefMoo.Focus();}
        }

        private void CboPttCurTambon_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            txtPttRefHomeNo.SelectAll();            txtPttRefHomeNo.Focus();
        }

        private void BtnRptExcel_Click(object sender, EventArgs e)
        {
            DateTime datestart = new DateTime();
            DateTime dateend = new DateTime();
            DateTime.TryParse(txtRptDateStart.Text, out datestart);
            DateTime.TryParse(txtRptDateEnd.Text, out dateend);
            //throw new NotImplementedException();
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "xls";
            dlg.FileName = "*.xls";
            dlg.Filter = "Excel Files | *.xls";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            C1XLBook _book = new C1XLBook();
            XLSheet sheet = _book.Sheets.Add(datestart.ToString("dd-MM-yyyy") + "-" + dateend.ToString("dd-MM-yyyy"));
            bc.SaveSheet(grfRptData, sheet, _book, false);
            //ic.SaveSheet(grfCld, sheet, _book, false);
            //}

            // save selected sheet index
            _book.Sheets.SelectedIndex = 1;

            // save the book
            _book.Save(dlg.FileName);

            //String filename = "";
            //filename = Path.GetDirectoryName(Application.ExecutablePath) + "\\doc-nurse-dotm\\กอร์ดอน-pop.docm";
            if (File.Exists(dlg.FileName))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = dlg.FileName,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            else
            {
                MessageBox.Show("File not found", "");
            }
        }

        private void BtnRptOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
            if (rptCode.Length <= 0)
            {
                lfSbMessage.Text = rptCode+" no select report";
                return;
            }
            if (rptCode.Equals("reportdailydept"))
            {
                setGrfRptReportDailyDept();
            }
            else if (rptCode.Equals("reportdailydeptHI"))
            {
                setGrfRptReportDailyDeptHi();
            }
            else if (rptCode.Equals("reportdailydeptHIATK"))
            {
                setGrfRptReportDailyDeptHiATK();
            }
        }
        private void setGrfRptReportDailyDept()
        {
            DateTime datestart = new DateTime();
            DateTime dateend = new DateTime();
            DataTable dt = new DataTable();
            String deptid = "";
            try
            {
                showLbLoading();
                deptid = bc.bcDB.pttDB.selectDeptIdOPDBySecId(((ComboBoxItem)cboRptDept.SelectedItem).Value);
                DateTime.TryParse(txtRptDateStart.Text, out datestart);
                DateTime.TryParse(txtRptDateEnd.Text, out dateend);
                lfSbMessage.Text = datestart.ToString("yyyy-MM-dd") + " " + dateend.ToString("yyyy-MM-dd") + " " + ((ComboBoxItem)cboRptDept.SelectedItem).Value;
                dt = bc.bcDB.vsDB.selectPttHiinDept(deptid, ((ComboBoxItem)cboRptDept.SelectedItem).Value, datestart.ToString("yyyy-MM-dd"), dateend.ToString("yyyy-MM-dd"));
                int i = 1, j = 1;
                grfRptData.Cols.Count = 12;
                grfRptData.Rows.Count = 1;
                grfRptData.Cols[colgrfRptDatadailydeptDate].Width = 110;
                grfRptData.Cols[colgrfRptDatadailydeptTime].Width = 50;
                grfRptData.Cols[colgrfRptDatadailydeptHn].Width = 90;
                grfRptData.Cols[colgrfRptDatadailydeptName].Width = 280;
                grfRptData.Cols[colgrfRptDatadailydeptMobile].Width = 120;
                grfRptData.Cols[colgrfRptDatadailyDrugSet].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyAuthen].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyPicKYC].Width = 50;
                grfRptData.Cols[colgrfRptQueno].Width = 50;
                
                grfRptData.Cols[colgrfRptDatadailyPicKYC].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyPicFoodsdaily].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyXray].Width = 50;

                grfRptData.Cols[colgrfRptDatadailydeptDate].Caption = "date";
                grfRptData.Cols[colgrfRptDatadailydeptTime].Caption = "time";
                grfRptData.Cols[colgrfRptDatadailydeptHn].Caption = "hn";
                grfRptData.Cols[colgrfRptDatadailydeptName].Caption = "name";
                grfRptData.Cols[colgrfRptDatadailydeptMobile].Caption = "mobile";
                //grfRptData.Cols[colgrfRptDatadailyDrugSet].Caption = "set";
                //grfRptData.Cols[colgrfRptDatadailyAuthen].Caption = "authen";
                //grfRptData.Cols[colgrfRptDatadailyPicFoods].Caption = "Foods";
                //grfRptData.Cols[colgrfRptDatadailyPicFoodsdaily].Caption = "Foods1";
                //grfRptData.Cols[colgrfRptDatadailyXray].Caption = "xray";

                grfRptData.Rows.Count = dt.Rows.Count + 1;
                //Column colAuthen = grfRptData.Cols[colgrfRptDatadailyAuthen];
                //colAuthen.DataType = typeof(Image);
                //Column colPic = grfRptData.Cols[colgrfRptDatadailyPic];
                //colPic.DataType = typeof(Image);
                //pB1.Maximum = dt.Rows.Count;
                foreach (DataRow row1 in dt.Rows)
                {
                    //pB1.Value++;
                    Row rowa = grfRptData.Rows[i];
                    rowa[colgrfRptDatadailydeptDate] = row1["MNC_date"].ToString();
                    rowa[colgrfRptDatadailydeptTime] = row1["MNC_time"].ToString();
                    rowa[colgrfRptDatadailydeptHn] = row1["mnc_hn_no"].ToString();
                    rowa[colgrfRptDatadailydeptName] = row1["prefix"].ToString() + " " + row1["MNC_FNAME_T"].ToString() + " " + row1["MNC_LNAME_T"].ToString();
                    rowa[colgrfRptDatadailydeptMobile] = row1["mnc_cur_tel"].ToString();
                    //rowa[colgrfSrcPttid] = "";
                    //row1[colgrfRptDatadailyAuthen] = row1["status_authen"] != null ? row1["status_authen"].ToString().Equals("1") ? imgCorr : imgTran : imgTran;
                    //row1[colgrfRptDatadailyPic] = row1["status_pic_kyc"] != null ?  row1["status_pic_kyc"].ToString().Equals("1") ? imgCorr : imgTran : imgTran;
                    //row1[colgrfRptDatadailyAuthen] = "";
                    //row1[colgrfRptDatadailyPic] = "";
                    rowa[colgrfRptQueno] = row1["queue_seq"].ToString();
                    rowa[0] = i.ToString();
                    i++;
                }
            }
            catch(Exception ex)
            {
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", "FrmReception setGrfRptReportDailyDept ");
            }
            finally
            {
                hideLbLoading();
            }
        }
        private void setGrfRptReportDailyDeptHi()
        {
            DateTime datestart = new DateTime();
            DateTime dateend = new DateTime();
            DataTable dt = new DataTable();
            String deptid = "";
            try
            {
                showLbLoading();
                deptid = bc.bcDB.pttDB.selectDeptIdOPDBySecId(((ComboBoxItem)cboRptDept.SelectedItem).Value);
                DateTime.TryParse(txtRptDateStart.Text, out datestart);
                DateTime.TryParse(txtRptDateEnd.Text, out dateend);
                lfSbMessage.Text = datestart.ToString("yyyy-MM-dd") + " " + dateend.ToString("yyyy-MM-dd") + " " + ((ComboBoxItem)cboRptDept.SelectedItem).Value;
                dt = bc.bcDB.vsDB.selectPttHiinDept(deptid, ((ComboBoxItem)cboRptDept.SelectedItem).Value, datestart.ToString("yyyy-MM-dd"), dateend.ToString("yyyy-MM-dd"));
                int i = 1, j = 1;
                long chkxray = 0;
                grfRptData.Cols.Count = 12;
                grfRptData.Rows.Count = 1;
                grfRptData.Cols[colgrfRptDatadailydeptDate].Width = 110;
                grfRptData.Cols[colgrfRptDatadailydeptTime].Width = 50;
                grfRptData.Cols[colgrfRptDatadailydeptHn].Width = 90;
                grfRptData.Cols[colgrfRptDatadailydeptName].Width = 280;
                grfRptData.Cols[colgrfRptDatadailydeptMobile].Width = 120;
                grfRptData.Cols[colgrfRptDatadailyDrugSet].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyAuthen].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyPicKYC].Width = 50;
                grfRptData.Cols[colgrfRptQueno].Width = 50;

                grfRptData.Cols[colgrfRptDatadailyPicKYC].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyPicFoodsdaily].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyXray].Width = 50;

                grfRptData.Cols[colgrfRptDatadailydeptDate].Caption = "date";
                grfRptData.Cols[colgrfRptDatadailydeptTime].Caption = "time";
                grfRptData.Cols[colgrfRptDatadailydeptHn].Caption = "hn";
                grfRptData.Cols[colgrfRptDatadailydeptName].Caption = "name";
                grfRptData.Cols[colgrfRptDatadailydeptMobile].Caption = "mobile";
                grfRptData.Cols[colgrfRptDatadailyDrugSet].Caption = "set";
                grfRptData.Cols[colgrfRptDatadailyAuthen].Caption = "Foods";
                grfRptData.Cols[colgrfRptDatadailyPicKYC].Caption = "KYC";
                grfRptData.Cols[colgrfRptDatadailyPicFoodsdaily].Caption = "Foods1";
                grfRptData.Cols[colgrfRptDatadailyXray].Caption = "xray";

                grfRptData.Rows.Count = dt.Rows.Count + 1;
                Column colAuthen = grfRptData.Cols[colgrfRptDatadailyAuthen];
                colAuthen.DataType = typeof(Image);
                Column colPic = grfRptData.Cols[colgrfRptDatadailyPicKYC];
                colPic.DataType = typeof(Image);
                Column colPic1 = grfRptData.Cols[colgrfRptDatadailyXray];
                colPic1.DataType = typeof(Image);
                //pB1.Maximum = dt.Rows.Count;
                foreach (DataRow row1 in dt.Rows)
                {
                    //pB1.Value++;
                    Row rowa = grfRptData.Rows[i];
                    rowa[colgrfRptDatadailydeptDate] = row1["MNC_date"].ToString();
                    rowa[colgrfRptDatadailydeptTime] = row1["MNC_time"].ToString();
                    rowa[colgrfRptDatadailydeptHn] = row1["mnc_hn_no"].ToString();
                    rowa[colgrfRptDatadailydeptName] = row1["prefix"].ToString() + " " + row1["MNC_FNAME_T"].ToString() + " " + row1["MNC_LNAME_T"].ToString();
                    rowa[colgrfRptDatadailydeptMobile] = row1["mnc_cur_tel"].ToString();
                    //rowa[colgrfSrcPttid] = "";
                    rowa[colgrfRptDatadailyAuthen] = row1["status_authen"] != null ? row1["status_authen"].ToString().Equals("1") ? imgCorr : imgTran : imgTran;        //รูปถ่ายอาหาร รับอาหาร
                    rowa[colgrfRptDatadailyPicKYC] = row1["status_pic_kyc"] != null ? row1["status_pic_kyc"].ToString().Equals("1") ? imgCorr : imgTran : imgTran;         //รูปถ่ายคนไข้ KYC
                    //long.TryParse(row1["req_no_xray"].ToString(), out chkxray);
                    rowa[colgrfRptDatadailyXray] = long.TryParse(row1["req_no_xray"].ToString(), out chkxray) ? imgCorr : imgTran;
                    rowa[colgrfRptDatadailyDrugSet] = row1["drug_set"].ToString();
                    rowa[colgrfRptQueno] = row1["queue_seq"].ToString();
                    rowa[0] = i.ToString();
                    i++;
                }
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", "FrmReception setGrfRptReportDailyDept ");
            }
            finally
            {
                hideLbLoading();
            }
        }
        private void setGrfRptReportDailyDeptHiATK()
        {
            DateTime datestart = new DateTime();
            DateTime dateend = new DateTime();
            DataTable dt = new DataTable();
            String deptid = "";
            try
            {
                showLbLoading();
                deptid = bc.bcDB.pttDB.selectDeptIdOPDBySecId(((ComboBoxItem)cboRptDept.SelectedItem).Value);
                DateTime.TryParse(txtRptDateStart.Text, out datestart);
                DateTime.TryParse(txtRptDateEnd.Text, out dateend);
                lfSbMessage.Text = datestart.ToString("yyyy-MM-dd") + " " + dateend.ToString("yyyy-MM-dd") + " " + ((ComboBoxItem)cboRptDept.SelectedItem).Value;
                dt = bc.bcDB.vsDB.selectPttHiinDeptPaidCode(deptid, ((ComboBoxItem)cboRptDept.SelectedItem).Value, datestart.ToString("yyyy-MM-dd"), dateend.ToString("yyyy-MM-dd"), txtPaidCode.Text.Trim());
                int i = 1, j = 1;
                long chkxray = 0;
                grfRptData.Cols.Count = 12;
                grfRptData.Rows.Count = 1;
                grfRptData.Cols[colgrfRptDatadailydeptDate].Width = 110;
                grfRptData.Cols[colgrfRptDatadailydeptTime].Width = 50;
                grfRptData.Cols[colgrfRptDatadailydeptHn].Width = 90;
                grfRptData.Cols[colgrfRptDatadailydeptName].Width = 280;
                grfRptData.Cols[colgrfRptDatadailydeptMobile].Width = 120;
                grfRptData.Cols[colgrfRptDatadailyDrugSet].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyAuthen].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyPicKYC].Width = 50;
                grfRptData.Cols[colgrfRptQueno].Width = 50;

                grfRptData.Cols[colgrfRptDatadailyPicKYC].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyPicFoodsdaily].Width = 50;
                grfRptData.Cols[colgrfRptDatadailyXray].Width = 50;

                grfRptData.Cols[colgrfRptDatadailydeptDate].Caption = "date";
                grfRptData.Cols[colgrfRptDatadailydeptTime].Caption = "time";
                grfRptData.Cols[colgrfRptDatadailydeptHn].Caption = "hn";
                grfRptData.Cols[colgrfRptDatadailydeptName].Caption = "name";
                grfRptData.Cols[colgrfRptDatadailydeptMobile].Caption = "mobile";
                grfRptData.Cols[colgrfRptDatadailyDrugSet].Caption = "set";
                grfRptData.Cols[colgrfRptDatadailyAuthen].Caption = "Foods";
                grfRptData.Cols[colgrfRptDatadailyPicKYC].Caption = "KYC";
                grfRptData.Cols[colgrfRptDatadailyPicFoodsdaily].Caption = "Foods1";
                grfRptData.Cols[colgrfRptDatadailyXray].Caption = "xray";

                grfRptData.Rows.Count = dt.Rows.Count + 1;
                Column colAuthen = grfRptData.Cols[colgrfRptDatadailyAuthen];
                colAuthen.DataType = typeof(Image);
                Column colPic = grfRptData.Cols[colgrfRptDatadailyPicKYC];
                colPic.DataType = typeof(Image);
                Column colPic1 = grfRptData.Cols[colgrfRptDatadailyXray];
                colPic1.DataType = typeof(Image);
                //pB1.Maximum = dt.Rows.Count;
                foreach (DataRow row1 in dt.Rows)
                {
                    //pB1.Value++;
                    Row rowa = grfRptData.Rows[i];
                    rowa[colgrfRptDatadailydeptDate] = row1["MNC_date"].ToString();
                    rowa[colgrfRptDatadailydeptTime] = row1["MNC_time"].ToString();
                    rowa[colgrfRptDatadailydeptHn] = row1["mnc_hn_no"].ToString();
                    rowa[colgrfRptDatadailydeptName] = row1["prefix"].ToString() + " " + row1["MNC_FNAME_T"].ToString() + " " + row1["MNC_LNAME_T"].ToString();
                    rowa[colgrfRptDatadailydeptMobile] = row1["mnc_cur_tel"].ToString();
                    //rowa[colgrfSrcPttid] = "";
                    //rowa[colgrfRptDatadailyAuthen] = row1["status_authen"] != null ? row1["status_authen"].ToString().Equals("1") ? imgCorr : imgTran : imgTran;        //รูปถ่ายอาหาร รับอาหาร
                    //rowa[colgrfRptDatadailyPicKYC] = row1["status_pic_kyc"] != null ? row1["status_pic_kyc"].ToString().Equals("1") ? imgCorr : imgTran : imgTran;         //รูปถ่ายคนไข้ KYC
                    //long.TryParse(row1["req_no_xray"].ToString(), out chkxray);
                    //rowa[colgrfRptDatadailyXray] = long.TryParse(row1["req_no_xray"].ToString(), out chkxray) ? imgCorr : imgTran;
                    //rowa[colgrfRptDatadailyDrugSet] = row1["drug_set"].ToString();
                    //rowa[colgrfRptQueno] = row1["queue_seq"].ToString();
                    rowa[0] = i.ToString();
                    i++;
                }
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = ex.Message;
                new LogWriter("e", "FrmReception setGrfRptReportDailyDept ");
            }
            finally
            {
                hideLbLoading();
            }
        }
        private void TxtPttCurRoad_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter){ txtPttCurSearchTambon.Focus(); }
        }

        private void TxtPttCurSoi_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) { txtPttCurRoad.SelectAll(); txtPttCurRoad.Focus(); }
        }

        private void TxtPttCurMoo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter) {                txtPttCurSoi.SelectAll();                txtPttCurSoi.Focus();            }
        }

        private void TxtPttCurHomeNo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttCurMoo.SelectAll();                txtPttCurMoo.Focus();            }
        }
        private void TxtPttIDRoad_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            
        }

        private void TxtPttIDSoi_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttIDRoad.SelectAll();                txtPttIDRoad.Focus();            }
        }

        private void TxtPttIDMoo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttIDSoi.SelectAll();                txtPttIDSoi.Focus();            }
        }

        private void TxtPttIDHomeNo_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttIDMoo.SelectAll();                txtPttIDMoo.Focus();            }
        }

        private void TxtPttEmail_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttIDHomeNo.SelectAll();                txtPttIDHomeNo.Focus();            }
        }

        private void TxtPttMobile2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttRef1.SelectAll();                txtPttRef1.Focus();            }
        }

        private void TxtPttMobile1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttMobile2.SelectAll();                txtPttMobile2.Focus();            }
        }

        private void TxtPttPassport_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttPassportOld.SelectAll();                txtPttPassportOld.Focus();            }
        }

        private void TxtPttSsn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttPassport.SelectAll();                txtPttPassport.Focus();            }
        }
        private void TxtPttSurNameE_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttPID.SelectAll();                txtPttPID.Focus();            }
        }
        private void TxtPttNameE_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttSurNameE.SelectAll();                txtPttSurNameE.Focus();            }
        }
        private void TxtPttSurNameT_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttNameE.SelectAll();                txtPttNameE.Focus();            }
        }
        private void CboPttPrefixT_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            //throw new NotImplementedException();
            if(pageLoad) { return; }
            String chk = cboPttPrefixT.SelectedItem == null ? "" : ((ComboBoxItem)cboPttPrefixT.SelectedItem).Value;
            String sex = "";
            //if (chk.Equals("01")) { sex = "M"; bc.setC1Combo(cboPttSex, sex); }
            //else if (chk.Equals("02")) { sex = "F"; bc.setC1Combo(cboPttSex, sex); }
            //else if (chk.Equals("03")) { sex = "F"; bc.setC1Combo(cboPttSex, sex); }
            //else if (chk.Equals("04")) { sex = "M"; bc.setC1Combo(cboPttSex, sex); }
            //else if (chk.Equals("05")) { sex = "F"; bc.setC1Combo(cboPttSex, sex); }
            //else if (chk.Equals("11")) { sex = "M"; bc.setC1Combo(cboPttSex, sex); }
            //else if (chk.Equals("13")) { sex = "F"; bc.setC1Combo(cboPttSex, sex); }
            //else if (chk.Equals("12")) { sex = "F"; bc.setC1Combo(cboPttSex, sex); }
            //else if (chk.Equals("I1")) { sex = "M"; bc.setC1Combo(cboPttSex, sex); }
            //else if (chk.Equals("I2")) { sex = "F"; bc.setC1Combo(cboPttSex, sex); }
            //else if (chk.Equals("I3")) { sex = "F"; bc.setC1Combo(cboPttSex, sex); }
            sex = bc.bcDB.pm02DB.getSexByCode(chk);
            bc.setC1Combo(cboPttSex, sex);
            txtPttNameT.SelectAll();            txtPttNameT.Focus();
        }
        private void TxtPttNameT_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)            {                txtPttSurNameT.SelectAll();                txtPttSurNameT.Focus();            }
        }
        private void TxtPttCompCode_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(txtPttCompCode.Text.Trim(), out _))//ป้อนเป็น ตัวเลข
                {
                    txtPttCompCode.Value = bc.bcDB.pm24DB.getPaidName(txtPttCompCode.Text.Trim());
                }
                txtPttAttchNote.SelectAll();
                txtPttAttchNote.Focus();
            }
            else if (txtPttCompCode.Text.Trim().Length > 3)
            {
                findCust = true;
                findInsur = false;
                //setGrfCust(txtPttCompCode.Text.Trim());
            }
            else
            {
                findCust = true;
                findInsur = false;
            }
        }
        private void TxtSrcHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if ((e.KeyCode == Keys.Enter) || (txtSrcHn.Text.Length > 3))
            {
                setGrfSrc1();
            }
            else
            {

            }
        }
        private void setGrfSrc1()
        {
            showLbLoading();
            setGrfSrc();
            hideLbLoading();
            if(grfSrc.Rows.Count == 2) 
            {
                if (grfSrc == null) return;
                if (grfSrc.Row <= 0) return;
                if (grfSrc.Col <= 0) return;
                //setControlPatientByGrf(grfSrc.Row);  //comment เพราะถ้า search ไม่เจอ น่าจะ ค้าง แสดงว่า เจออะไร หรือไม่พบ เพราะ ตอน key key ไป8ตัวอักษร เจอของคนอื่น แล้วเอามาแสดง ในcase ที่ค้นไม่เจอ
            }
        }
        private void BtnSrcNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FLAGPTTNEW = true;
            setControlPatientNew();

        }
        private void setControlPatientNew()
        {
            clearControl();            tC.SelectedTab = tabPtt;            txtPttPID.SelectAll();            txtPttPID.Focus();
        }
        private void setCboTheme()
        {
            cboTheme.Items.Clear();
            String[] themes = C1ThemeController.GetThemes();
            foreach (String txt in themes)            {                cboTheme.Items.Add(txt);            }
            cboTheme.SelectedText = "Office2010Red";
            
        }
        private void setTheme(String theme)
        {
            theme1.SetTheme(this,theme);/*pnSrcTop*/            theme1.SetTheme(groupBox1, "Office2010Green");            theme1.SetTheme(groupBox2, "BeigeOne");            theme1.SetTheme(groupBox3, "Office2010Barbie");
            //theme1.SetTheme(groupBox5, bc.iniC.themeApp);Office2007Blue
            //theme1.SetTheme(groupBox6, bc.iniC.themeApp);Office2010Green
            //theme1.SetTheme(groupBox4, "Office2010Barbie");//RainerOrange
            //theme1.SetTheme(pnPttComp, bc.iniC.themeApp);Office2010Silver
            //theme1.SetTheme(groupBox6, bc.iniC.themeApp);c1StatusBar1
            theme1.SetTheme(sCPtt, theme);            theme1.SetTheme(c1StatusBar1, bc.iniC.themeApp);
            foreach (Control c in pnPttPatient.Controls) if (c is C1TextBox) { theme1.SetTheme(c, bc.iniC.themeApp); } else if (c is C1Button) { theme1.SetTheme(c, bc.iniC.themeApp); }
            foreach (Control c in groupBox1.Controls){                if (c is C1TextBox) theme1.SetTheme(c, "Office2010Green");                else if(c is Label) theme1.SetTheme(c, "Office2010Green");}
            foreach (Control c in groupBox2.Controls){                if (c is C1TextBox) theme1.SetTheme(c, "BeigeOne");                else if (c is Label) theme1.SetTheme(c, "BeigeOne");}
            foreach (Control c in groupBox3.Controls){                if (c is C1TextBox) theme1.SetTheme(c, "Office2010Barbie");                else if (c is Label) theme1.SetTheme(c, "Office2010Barbie");}
            foreach (Control c in groupBox5.Controls){                if (c is C1TextBox)theme1.SetTheme(c, bc.iniC.themeApp);}
            foreach (Control c in groupBox6.Controls){                if (c is C1TextBox) theme1.SetTheme(c, bc.iniC.themeApp);            }
            theme1.SetTheme(btnPttSave, bc.iniC.themeApp);            theme1.SetTheme(btnPttVisit, bc.iniC.themeApp);            theme1.SetTheme(btnPttCardRead, bc.iniC.themeApp);
            theme1.SetTheme(btnPttIdCopyto, bc.iniC.themeApp);            theme1.SetTheme(btnPttCurCopyto, bc.iniC.themeApp);

            foreach (Control c in pnVsVisit.Controls){                if (c is C1TextBox)                {                    theme1.SetTheme(c, "RainerOrange");                }}
            theme1.SetTheme(btnVsSave, "RainerOrange");
            //theme1.SetTheme(btnPttPaid, bc.iniC.themeApp);
        }
        private void clearControl()
        {
            String err = "";
            try
            {
                txtPttHn.Value = "";                txtPttDOB.Value = "";                cboPttPrefixT.SelectedIndex = 0;                txtPttNameT.Value = "";
                txtPttSurNameT.Value = "";                txtPttAge.Value = "";                cboPttPrefixE.SelectedIndex = 0;                txtPttNameE.Value = "";
                txtPttSurNameE.Value = "";                txtPttPID.Value = "";                txtPttSsn.Value = "";                txtPttPassport.Value = "";
                txtPttMobile1.Value = "";                txtPttMobile2.Value = "";                txtPttEmail.Value = "";                txtPttIDHomeNo.Value = "";
                txtPttIDMoo.Value = "";                txtPttIDSoi.Value = "";                txtPttIDRoad.Value = "";                txtPttIDPostcode.Value = "";

                txtPttIdSearchTambon.Value = "";                txtPttIdAmp.Value = "";                txtPttIdChw.Value = "";                txtPttCurHomeNo.Value = "";
                txtPttCurMoo.Value = "";                txtPttCurSoi.Value = "";                txtPttCurRoad.Value = "";                txtPttCurSearchTambon.Value = "";
                txtPttCurAmp.Value = "";                txtPttCurChw.Value = "";                txtPttCurPostcode.Value = "";                txtPttRefHomeNo.Value = "";
                txtPttRefMoo.Value = "";                txtPttRefSoi.Value = "";                txtPttRefRoad.Value = "";                txtPttRefSearchTambon.Value = "";
                txtPttRefAmp.Value = "";                txtPttRefChw.Value = "";                txtPttRefPostcode.Value = "";                txtPttRefContact1Name.Value = "";
                txtPttRefContact2Name.Value = "";                txtPttRefContact1Mobile.Value = "";                txtPttRefContact2Mobile.Value = "";                txtPttRefContact1Rel.Value = "";
                txtPttRefContact2Rel.Value = "";                txtPttCompCode.Value = "";
                txtPttDOBDD.Value = ""; txtPttDOBMM.Value = ""; txtPttDOBYear.Value = "";
                //lbPttCompNameT.Text = "";
                cboPttNat.SelectedIndex = 0;
                cboPttRace.SelectedIndex = 0;   //เชื้อชาติ
                cboPttEdu.SelectedIndex = 0;
                cboPttRel.SelectedIndex = 0;    //ศาสนา
                cboPttMarri.SelectedIndex = 0;                txtPttAttchNote.Value = "";                txtPttRemark1.Value = "";                txtPttRemark2.Value = "";
                lbFindPaidSSO.Text = "";                txtPttIdSearchTambon.Value = "";

                if (grfPttComp != null) grfPttComp.Rows.Count = 1;
                if (grfPttVs != null) grfPttVs.Rows.Count = 1;
                if (grfPttApm != null) grfPttApm.Rows.Count = 1;
                if (grfVsPttVisit != null) grfVsPttVisit.Rows.Count = 1;

                txtPttwp1.Value = "";                txtPttwp2.Value = "";                txtPttwp3.Value = "";                txtPttInsur.Value = "";
                //lbPttInsurNameT.Text = "";
                m_picPhoto.Image = null;

                txtVsHN.Value = "";                lbVsPttNameT.Text = "";                lbVsPttNameE.Text = "";                txtVsPaidCode.Value = "";
                cboVsDept.SelectedIndex = 0;                txtVsRemark.Value = "";                txtVsNote.Value = "";                cboVsDtr.SelectedIndex = 0;
                
                cboVsType.SelectedItem = itemVsType;                
                //cboVsType.Text = "";
                CHWCODE = ""; AMPCODE = ""; TAMBONCODE = ""; VSDATE = ""; PRENO = ""; QUENO = "";
                
                lfSbStatus.Text = "";                lfSbMessage.Text = "";

                picVsPtt.Image = null;                txtVsPttAttchNote.Value = "";                txtVsPttRemark1.Value = "";                txtVsPttRemark2.Value = "";
                lbVsPaidNameT.Text = "...";                lbVsStatus.Text = "";                lbPttinWard.Text = "";                clearTxtUser();
                txtPttRef1.Value = ""; txtPttPassportOld.Value = "";
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmReception clearControl " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "clearControl ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
        }
        private void clearControlTabVisit()
        {
            txtVsHN.Value = "";
            lbVsPttNameT.Text = "";
            lbVsPttNameE.Text = "";
            txtVsPaidCode.Value = "";
            cboVsDept.SelectedIndex = 0;
            txtVsRemark.Value = "";
            txtVsNote.Value = "";
            cboVsDtr.SelectedIndex = 0;
            
            cboVsType.SelectedItem = itemVsType;
            txtVsSymptom.Value = "";

            picVsPtt.Image = null;
            txtVsPttAttchNote.Value = "";
            txtVsPttRemark1.Value = "";
            txtVsPttRemark2.Value = "";
            lbVsPaidNameT.Text = "...";
            lbVsStatus.Text = "";
            txtVsInsur.Value = "";
            txtVsComp.Value = "";
            chkVsStatusDOE.Checked = false;


            clearTxtUser();
        }
        private void initGrfSSO()
        {
            grfSSO = new C1FlexGrid();
            grfSSO.Font = fEdit;
            grfSSO.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSSO.Location = new System.Drawing.Point(0, 0);
            grfSSO.Rows.Count = 1;
            grfSSO.Cols.Count = 14;

            grfSSO.Cols[colgrfSSOSocialNo].Width = 130;
            grfSSO.Cols[colgrfSSOCard].Width = 100;
            grfSSO.Cols[colgrfSSOTitle].Width = 80;
            grfSSO.Cols[colgrfSSOFirstName].Width = 200;
            grfSSO.Cols[colgrfSSOLastName].Width = 200;
            grfSSO.Cols[colgrfSSOFullName].Width = 200;
            grfSSO.Cols[colgrfSSOPrakan].Width = 80;
            grfSSO.Cols[colgrfSSOPrang].Width = 50;
            grfSSO.Cols[colgrfSSOStartDate].Width = 100;
            grfSSO.Cols[colgrfSSOEndDate].Width = 100;
            grfSSO.Cols[colgrfSSOdob].Width = 100;
            grfSSO.Cols[colgrfSSOUploadDate].Width = 100;

            grfSSO.ShowCursor = true;
            grfSSO.Cols[colgrfSSOSocialNo].Caption = "ID";
            grfSSO.Cols[colgrfSSOCard].Caption = "card";
            grfSSO.Cols[colgrfSSOTitle].Caption = "Title";
            grfSSO.Cols[colgrfSSOFirstName].Caption = "Firstname";
            grfSSO.Cols[colgrfSSOLastName].Caption = "Lastname";
            grfSSO.Cols[colgrfSSOFullName].Caption = "Fullname";
            grfSSO.Cols[colgrfSSOPrakan].Caption = "prakan";
            grfSSO.Cols[colgrfSSOPrang].Caption = "prang";
            grfSSO.Cols[colgrfSSOStartDate].Caption = "startdate";
            grfSSO.Cols[colgrfSSOEndDate].Caption = "endate";
            grfSSO.Cols[colgrfSSOdob].Caption = "DOB";
            grfSSO.Cols[colgrfSSOUploadDate].Caption = "uploaddate";

            grfSSO.Cols[colgrfSSOSocialNo].DataType = typeof(String);
            grfSSO.Cols[colgrfSSOCard].DataType = typeof(String);
            grfSSO.Cols[colgrfSSOTitle].DataType = typeof(String);
            grfSSO.Cols[colgrfSSOFirstName].DataType = typeof(String);
            grfSSO.Cols[colgrfSSOLastName].DataType = typeof(String);
            grfSSO.Cols[colgrfSSOFullName].DataType = typeof(String);
            grfSSO.Cols[colgrfSSOPrakan].DataType = typeof(String);
            grfSSO.Cols[colgrfSSOPrang].DataType = typeof(String);
            grfSSO.Cols[colgrfSSOStartDate].DataType = typeof(String);
            grfSSO.Cols[colgrfSSOEndDate].DataType = typeof(String);
            grfSSO.Cols[colgrfSSOdob].DataType = typeof(String);
            grfSSO.Cols[colgrfSSOUploadDate].DataType = typeof(String);

            grfSSO.Cols[colgrfSSOSocialNo].TextAlign = TextAlignEnum.LeftCenter;
            grfSSO.Cols[colgrfSSOCard].TextAlign = TextAlignEnum.LeftCenter;
            grfSSO.Cols[colgrfSSOTitle].TextAlign = TextAlignEnum.LeftCenter;
            grfSSO.Cols[colgrfSSOFirstName].TextAlign = TextAlignEnum.LeftCenter;
            grfSSO.Cols[colgrfSSOLastName].TextAlign = TextAlignEnum.LeftCenter;
            grfSSO.Cols[colgrfSSOFullName].TextAlign = TextAlignEnum.LeftCenter;
            grfSSO.Cols[colgrfSSOPrakan].TextAlign = TextAlignEnum.CenterCenter;
            grfSSO.Cols[colgrfSSOPrang].TextAlign = TextAlignEnum.CenterCenter;
            grfSSO.Cols[colgrfSSOStartDate].TextAlign = TextAlignEnum.CenterCenter;
            grfSSO.Cols[colgrfSSOEndDate].TextAlign = TextAlignEnum.CenterCenter;
            grfSSO.Cols[colgrfSSOdob].TextAlign = TextAlignEnum.CenterCenter;
            grfSSO.Cols[colgrfSSOUploadDate].TextAlign = TextAlignEnum.CenterCenter;

            grfSSO.Cols[colgrfSSOdob].Visible = false;
            grfSSO.Cols[colgrfSSOPrang].Visible = false;
            grfSSO.Cols[colgrfSSOPrakan].Visible = false;
            grfSSO.Cols[colgrfSSOLastName].Visible = false;
            grfSSO.Cols[colgrfSSOFirstName].Visible = false;
            grfSSO.Cols[colgrfSSOTitle].Visible = false;

            grfSSO.Cols[colgrfSSOSocialNo].AllowEditing = false;
            grfSSO.Cols[colgrfSSOCard].AllowEditing = false;
            grfSSO.Cols[colgrfSSOTitle].AllowEditing = false;
            grfSSO.Cols[colgrfSSOFirstName].AllowEditing = false;
            grfSSO.Cols[colgrfSSOLastName].AllowEditing = false;
            grfSSO.Cols[colgrfSSOFullName].AllowEditing = false;
            grfSSO.Cols[colgrfSSOPrakan].AllowEditing = false;
            grfSSO.Cols[colgrfSSOPrang].AllowEditing = false;
            grfSSO.Cols[colgrfSSOStartDate].AllowEditing = false;
            grfSSO.Cols[colgrfSSOEndDate].AllowEditing = false;
            grfSSO.Cols[colgrfSSOdob].AllowEditing = false;
            grfSSO.Cols[colgrfSSOUploadDate].AllowEditing = false;

            //grfApm.Cols[colgrfPttApmStatusRemarkCall].AllowFiltering = AllowFiltering.ByValue;
            grfSSO.AllowFiltering = true;

            pnSSO.Controls.Add(grfSSO);
            theme1.SetTheme(grfSSO, "Office2010Blue");
        }
        private void initGrfWard()
        {
            grfWard = new C1FlexGrid();
            grfWard.Font = fEdit;
            grfWard.Dock = System.Windows.Forms.DockStyle.Fill;
            grfWard.Location = new System.Drawing.Point(0, 0);
            grfWard.Rows.Count = 1;
            grfWard.Cols.Count = 9;

            grfWard.Cols[colgrfWardName].Width = 100;
            grfWard.Cols[colgrfWardRoom].Width = 100;
            grfWard.Cols[colgrfWardBed].Width = 60;
            grfWard.Cols[colgrfWardHn].Width = 100;
            grfWard.Cols[colgrfWardPttName].Width = 200;
            grfWard.Cols[colgrfWardSymptoms].Width = 300;
            grfWard.Cols[colgrfWardDays].Width = 50;
            grfWard.Cols[colgrfWardDtrName].Width = 300;

            grfWard.ShowCursor = true;
            grfWard.Cols[colgrfWardName].Caption = "ward";
            grfWard.Cols[colgrfWardRoom].Caption = "room";
            grfWard.Cols[colgrfWardBed].Caption = "bed";
            grfWard.Cols[colgrfWardHn].Caption = "HN";
            grfWard.Cols[colgrfWardPttName].Caption = "ชื่อ-นามสกุล";
            grfWard.Cols[colgrfWardSymptoms].Caption = "อาการ";
            grfWard.Cols[colgrfWardDays].Caption = "days";

            grfWard.Cols[colgrfWardName].DataType = typeof(String);
            grfWard.Cols[colgrfWardRoom].DataType = typeof(String);
            grfWard.Cols[colgrfWardBed].DataType = typeof(String);
            grfWard.Cols[colgrfWardHn].DataType = typeof(String);
            grfWard.Cols[colgrfWardPttName].DataType = typeof(String);
            grfWard.Cols[colgrfWardSymptoms].DataType = typeof(String);

            grfWard.Cols[colgrfWardName].TextAlign = TextAlignEnum.LeftCenter;
            grfWard.Cols[colgrfWardRoom].TextAlign = TextAlignEnum.CenterCenter;
            grfWard.Cols[colgrfWardBed].TextAlign = TextAlignEnum.CenterCenter;
            grfWard.Cols[colgrfWardHn].TextAlign = TextAlignEnum.CenterCenter;
            grfWard.Cols[colgrfWardPttName].TextAlign = TextAlignEnum.LeftCenter;
            grfWard.Cols[colgrfWardSymptoms].TextAlign = TextAlignEnum.LeftCenter;
            grfWard.Cols[colgrfWardDays].TextAlign = TextAlignEnum.CenterCenter;

            grfWard.Cols[colgrfWardName].Visible = true;
            grfWard.Cols[colgrfWardRoom].Visible = true;
            grfWard.Cols[colgrfWardBed].Visible = true;
            grfWard.Cols[colgrfWardHn].Visible = true;
            grfWard.Cols[colgrfWardPttName].Visible = true;
            grfWard.Cols[colgrfWardSymptoms].Visible = true;

            grfWard.Cols[colgrfWardName].AllowEditing = false;
            grfWard.Cols[colgrfWardRoom].AllowEditing = false;
            grfWard.Cols[colgrfWardBed].AllowEditing = false;
            grfWard.Cols[colgrfWardHn].AllowEditing = false;
            grfWard.Cols[colgrfWardPttName].AllowEditing = false;
            grfWard.Cols[colgrfWardSymptoms].AllowEditing = false;
            grfWard.Cols[colgrfWardDays].AllowEditing = false;

            //grfApm.Cols[colgrfPttApmStatusRemarkCall].AllowFiltering = AllowFiltering.ByValue;
            grfWard.AllowFiltering = true;
            grfWard.KeyDown += GrfWard_KeyDown;
            pnWard.Controls.Add(grfWard);
            theme1.SetTheme(grfWard, "Office2010Blue");
        }
        private void GrfWard_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                String txt = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, ((C1FlexGrid)sender).Col].ToString();
                Clipboard.SetText(txt);
            }
        }
        private void setGrfWard()
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.vsDB.selectPttinWard();
            grfWard.Rows.Count = 1;
            grfWard.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1, row = grfWard.Rows.Count;
            String time = "";
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfWard.Rows[i];
                rowa[colgrfWardName] = row1["MNC_MD_DEP_DSC"].ToString();
                rowa[colgrfWardRoom] = row1["MNC_RM_NAM"].ToString();
                rowa[colgrfWardBed] = row1["MNC_BD_NO"].ToString();
                rowa[colgrfWardHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfWardPttName] = row1["pttfullname"].ToString();
                rowa[colgrfWardSymptoms] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colgrfWardDays] = row1["days"].ToString();
                rowa[colgrfWardDtrName] = "แพทย์ "+row1["MNC_DOT_FNAME"].ToString()+" "+ row1["MNC_DOT_LNAME"].ToString();

                rowa[0] = i.ToString();
                i++;
            }
        }
        private void initGrfVsPttVisit()
        {
            grfVsPttVisit = new C1FlexGrid();
            grfVsPttVisit.Font = fEdit;
            grfVsPttVisit.Dock = System.Windows.Forms.DockStyle.Fill;
            grfVsPttVisit.Location = new System.Drawing.Point(0, 0);
            grfVsPttVisit.Rows.Count = 1;
            grfVsPttVisit.Cols.Count = 26;
            grfVsPttVisit.Cols[colgrfPttVsStatusVisit].Width = 50;
            grfVsPttVisit.Cols[colgrfPttVsVsDateShow].Width = 100;
            grfVsPttVisit.Cols[colgrfPttVsVsTime].Width = 60;
            grfVsPttVisit.Cols[colgrfPttVsHn].Width = 100;
            grfVsPttVisit.Cols[colgrfPttVsFullNameT].Width = 250;
            grfVsPttVisit.Cols[colgrfPttVsPreno].Width = 150;
            grfVsPttVisit.Cols[colgrfPttVsDept].Width = 150;
            grfVsPttVisit.Cols[colgrfPttVsStatusOPD].Width = 50;
            grfVsPttVisit.Cols[colgrfPttVsSymptom].Width = 300;
            grfVsPttVisit.Cols[colgrfPttVsAN].Width = 60;
            grfVsPttVisit.Cols[colgrfPttVsVN].Width = 60;
            grfVsPttVisit.Cols[colgrfPttVsQue].Width = 40;
            grfVsPttVisit.Cols[colgrfPttVsActno].Width = 40;
            grfVsPttVisit.ShowCursor = true;
            grfVsPttVisit.Cols[colgrfPttVsStatusVisit].Caption = "statusvs";
            grfVsPttVisit.Cols[colgrfPttVsVsDateShow].Caption = "date";
            grfVsPttVisit.Cols[colgrfPttVsVsTime].Caption = "time";
            grfVsPttVisit.Cols[colgrfPttVsHn].Caption = "hn";
            grfVsPttVisit.Cols[colgrfPttVsFullNameT].Caption = "ชื่อ-นามสกุล";
            grfVsPttVisit.Cols[colgrfPttVsPreno].Caption = "preno";
            grfVsPttVisit.Cols[colgrfPttVsDept].Caption = "ส่งตัวไป";
            grfVsPttVisit.Cols[colgrfPttVsStatusOPD].Caption = "opd";
            grfVsPttVisit.Cols[colgrfPttVsSymptom].Caption = "Symptom";
            grfVsPttVisit.Cols[colgrfPttVsAN].Caption = "VN/AN";
            grfVsPttVisit.Cols[colgrfPttVsVN].Caption = "VN";
            grfVsPttVisit.Cols[colgrfPttVsPaid].Caption = "รักษาด้วยสิทธิ";
            grfVsPttVisit.Cols[colgrfPttVsQue].Caption = "que";

            grfVsPttVisit.Cols[colgrfPttVsSymptom].DataType = typeof(String);
            grfVsPttVisit.Cols[colgrfPttVsSymptom].TextAlign = TextAlignEnum.LeftCenter;
            grfVsPttVisit.Cols[colgrfPttVsHn].DataType = typeof(String);
            grfVsPttVisit.Cols[colgrfPttVsHn].TextAlign = TextAlignEnum.CenterCenter;
            grfVsPttVisit.Cols[colgrfPttVsAN].DataType = typeof(String);
            grfVsPttVisit.Cols[colgrfPttVsAN].TextAlign = TextAlignEnum.CenterCenter;

            grfVsPttVisit.Cols[colgrfPttVsVsDateShow].Visible = true;
            grfVsPttVisit.Cols[colgrfPttVsVsTime].Visible = false;
            grfVsPttVisit.Cols[colgrfPttVsDiscDateShow].Visible = false;
            grfVsPttVisit.Cols[colgrfPttVsDiscTime].Visible = false;
            grfVsPttVisit.Cols[colgrfPttVsHn].Visible = false;
            grfVsPttVisit.Cols[colgrfPttVsPreno].Visible = false;
            grfVsPttVisit.Cols[colgrfPttVsDept].Visible = true;
            grfVsPttVisit.Cols[colgrfPttVsStatusOPD].Visible = false;
            grfVsPttVisit.Cols[colgrfPttVsSymptom].Visible = true;
            grfVsPttVisit.Cols[colgrfPttVsPaid].Visible = true;
            grfVsPttVisit.Cols[colgrfPttVsFullNameT].Visible = false;
            grfVsPttVisit.Cols[colgrfPttVsVN].Visible = false;
            grfVsPttVisit.Cols[colgrfPttVsVN].Visible = false;
            grfVsPttVisit.Cols[colgrfPttVsActno].Visible = true;

            grfVsPttVisit.Cols[colgrfPttVsVsDateShow].AllowEditing = false;
            grfVsPttVisit.Cols[colgrfPttVsHn].AllowEditing = false;
            grfVsPttVisit.Cols[colgrfPttVsPreno].AllowEditing = false;
            grfVsPttVisit.Cols[colgrfPttVsDept].AllowEditing = false;
            grfVsPttVisit.Cols[colgrfPttVsStatusOPD].AllowEditing = false;
            grfVsPttVisit.Cols[colgrfPttVsSymptom].AllowEditing = false;
            grfVsPttVisit.Cols[colgrfPttVsAN].AllowEditing = false;
            grfVsPttVisit.Cols[colgrfPttVsVN].AllowEditing = false;
            grfVsPttVisit.Cols[colgrfPttVsFullNameT].AllowEditing = false;
            grfVsPttVisit.Cols[colgrfPttVsQue].AllowEditing = false;
            grfVsPttVisit.Cols[colgrfPttVsActno].AllowEditing = false;

            pnVsPttVisit.Controls.Add(grfVsPttVisit);
            grfVsPttVisit.DoubleClick += GrfVsPttVisit_DoubleClick;
            grfVsPttVisit.KeyDown += GrfVsPttVisit_KeyDown;
            theme1.SetTheme(grfVsPttVisit, bc.iniC.themeApp);
        }

        private void GrfVsPttVisit_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfVsPttVisit.Row <= 0) return;
            if (grfVsPttVisit.Col <= 0) return;
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                String txt = grfVsPttVisit[grfVsPttVisit.Row, grfVsPttVisit.Col].ToString();
                Clipboard.SetText(txt);
            }
        }
        private void GrfVsPttVisit_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfVsPttVisit.Row <= 0) return;
            if (grfVsPttVisit.Col <= 0) return;
            String hn = "";
            wantEditVisit = true;
            hn = grfVsPttVisit[grfVsPttVisit.Row, colgrfPttVsHn].ToString();
            VSDATE = bc.datetoDB(grfVsPttVisit[grfVsPttVisit.Row, colgrfPttVsVsDate1].ToString());
            PRENO = grfVsPttVisit[grfVsPttVisit.Row, colgrfPttVsPreno].ToString();
            setControlTabVs(hn, VSDATE, PRENO);
            btnVsSave.Text = "แก้ไข visit";
        }
        private void setGrfVsPttVisit()
        {
            if (txtPttHn.Text.Length <= 0) return;
            DateTime datestart = DateTime.Now;
            
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.vsDB.selectVisitByHn(txtPttHn.Text.Trim(), datestart.Year.ToString()+ datestart.ToString("-MM-dd"), datestart.Year.ToString() + datestart.ToString("-MM-dd"));
            grfVsPttVisit.Rows.Count = 1;
            grfVsPttVisit.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfVsPttVisit.Rows[i];
                rowa[colgrfPttVsVsDateShow] = bc.datetoShow1(row1["mnc_date"].ToString());
                rowa[colgrfPttVsHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfPttVsFullNameT] = row1["ptt_fullnamet"].ToString();
                rowa[colgrfPttVsPaid] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[colgrfPttVsPreno] = row1["mnc_pre_no"].ToString();
                rowa[colgrfPttVsDept] = row1["MNC_AN_NO"].ToString().Equals("0") ? row1["dept_opd"].ToString(): row1["dept_ipd"].ToString();
                rowa[colgrfPttVsStatusOPD] = row1["mnc_sts_flg"].ToString();
                rowa[colgrfPttVsSymptom] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colgrfPttVsAN] = row1["MNC_AN_NO"].ToString().Equals("0") ? row1["MNC_VN_NO"].ToString() + "." + row1["MNC_VN_SEQ"].ToString() + "." + row1["MNC_VN_SUM"].ToString() : row1["MNC_AN_NO"].ToString() + "." + row1["MNC_AN_YR"].ToString();
                rowa[colgrfPttVsQue] = row1["MNC_QUE_NO"].ToString();
                rowa[colgrfPttVsActno] = bc.adjustACTNO(row1["MNC_ACT_NO"].ToString());
                rowa[colgrfPttVsVsDate1] = row1["mnc_date"].ToString();
                rowa[0] = i.ToString();
                i++;
            }
        }
        private void initGrfToday()
        {
            grfToday = new C1FlexGrid();
            grfToday.Font = fEdit;
            grfToday.Dock = System.Windows.Forms.DockStyle.Fill;
            grfToday.Location = new System.Drawing.Point(0, 0);
            grfToday.Rows.Count = 1;
            grfToday.Cols.Count = 26;

            grfToday.Cols[colgrfPttVsVsDateShow].Width = 100;
            grfToday.Cols[colgrfPttVsVsTime].Width = 60;
            grfToday.Cols[colgrfPttVsHn].Width = 100;
            grfToday.Cols[colgrfPttVsFullNameT].Width = 250;
            grfToday.Cols[colgrfPttVsPreno].Width = 150;
            grfToday.Cols[colgrfPttVsDept].Width = 100;
            grfToday.Cols[colgrfPttVsStatusOPD].Width = 50;
            grfToday.Cols[colgrfPttVsSymptom].Width = 150;
            grfToday.Cols[colgrfPttVsAN].Width = 80;
            grfToday.Cols[colgrfPttVsQue].Width = 40;
            grfToday.Cols[colgrfPttVsActno].Width = 70;
            grfToday.Cols[colgrfPttVsActno101].Width = 70;
            grfToday.Cols[colgrfPttVsActno110].Width = 60;
            grfToday.Cols[colgrfPttVsActno111].Width = 60;
            grfToday.Cols[colgrfPttVsActno113].Width = 60;
            grfToday.Cols[colgrfPttVsActno131].Width = 60;
            grfToday.Cols[colgrfPttVsActno500].Width = 60;
            grfToday.Cols[colgrfPttVsActno610].Width = 60;
            
            grfToday.ShowCursor = true;
            grfToday.Cols[colgrfPttVsVsDateShow].Caption = "date";
            grfToday.Cols[colgrfPttVsVsTime].Caption = "time";
            grfToday.Cols[colgrfPttVsHn].Caption = "hn";
            grfToday.Cols[colgrfPttVsFullNameT].Caption = "ชื่อ-นามสกุล";
            grfToday.Cols[colgrfPttVsPreno].Caption = "preno";
            grfToday.Cols[colgrfPttVsDept].Caption = "ส่งตัวไป";
            grfToday.Cols[colgrfPttVsStatusOPD].Caption = "opd";
            grfToday.Cols[colgrfPttVsSymptom].Caption = "Symptom";
            grfToday.Cols[colgrfPttVsVN].Caption = "VN";
            grfToday.Cols[colgrfPttVsAN].Caption = "VN/AN";
            grfToday.Cols[colgrfPttVsPaid].Caption = "รักษาด้วยสิทธิ";
            grfToday.Cols[colgrfPttVsQue].Caption = "que";
            grfToday.Cols[colgrfPttVsActno].Caption = "สถานะ";
            grfToday.Cols[colgrfPttVsActno101].Caption = "ส่งตัว";
            grfToday.Cols[colgrfPttVsActno110].Caption = "พบแพทย์";
            grfToday.Cols[colgrfPttVsActno111].Caption = "Scan ใบยา";
            grfToday.Cols[colgrfPttVsActno113].Caption = "vitalsign";
            grfToday.Cols[colgrfPttVsActno131].Caption = "ปิดการรักษา";
            grfToday.Cols[colgrfPttVsActno500].Caption = "รอรับยา";
            grfToday.Cols[colgrfPttVsActno610].Caption = "รับชำระเงินแล้ว";
            grfToday.Cols[colgrfPttVsRemark].Caption = "หมายเหตุ";
            grfToday.Cols[colgrfPttVsActno500].Caption = "500";
            grfToday.Cols[colgrfPttVsActno610].Caption = "610";
            grfToday.Cols[colgrfPttVsVsDate1].Caption = "VsDate1";

            grfToday.Cols[colgrfPttVsHn].DataType = typeof(String);
            grfToday.Cols[colgrfPttVsHn].TextAlign = TextAlignEnum.CenterCenter;
            grfToday.Cols[colgrfPttVsSymptom].DataType = typeof(String);
            grfToday.Cols[colgrfPttVsSymptom].TextAlign = TextAlignEnum.LeftCenter;
            grfToday.Cols[colgrfPttVsVsTime].DataType = typeof(String);
            grfToday.Cols[colgrfPttVsVsTime].TextAlign = TextAlignEnum.CenterCenter;
            grfToday.Cols[colgrfPttVsQue].DataType = typeof(String);
            grfToday.Cols[colgrfPttVsQue].TextAlign = TextAlignEnum.CenterCenter;
            grfToday.Cols[colgrfPttVsAN].DataType = typeof(String);
            grfToday.Cols[colgrfPttVsAN].TextAlign = TextAlignEnum.CenterCenter;
            grfToday.Cols[colgrfPttVsActno].DataType = typeof(String);
            grfToday.Cols[colgrfPttVsActno].TextAlign = TextAlignEnum.CenterCenter;
            grfToday.Cols[colgrfPttVsActno101].DataType = typeof(String);
            grfToday.Cols[colgrfPttVsActno101].TextAlign = TextAlignEnum.CenterCenter;
            grfToday.Cols[colgrfPttVsActno110].DataType = typeof(String);
            grfToday.Cols[colgrfPttVsActno110].TextAlign = TextAlignEnum.CenterCenter;
            grfToday.Cols[colgrfPttVsActno111].DataType = typeof(String);
            grfToday.Cols[colgrfPttVsActno111].TextAlign = TextAlignEnum.CenterCenter;
            grfToday.Cols[colgrfPttVsActno113].DataType = typeof(String);
            grfToday.Cols[colgrfPttVsActno113].TextAlign = TextAlignEnum.CenterCenter;
            grfToday.Cols[colgrfPttVsActno131].DataType = typeof(String);
            grfToday.Cols[colgrfPttVsActno131].TextAlign = TextAlignEnum.CenterCenter;
            grfToday.Cols[colgrfPttVsActno500].DataType = typeof(String);
            grfToday.Cols[colgrfPttVsActno500].TextAlign = TextAlignEnum.CenterCenter;
            grfToday.Cols[colgrfPttVsActno610].DataType = typeof(String);
            grfToday.Cols[colgrfPttVsActno610].TextAlign = TextAlignEnum.CenterCenter;

            grfToday.Cols[colgrfPttVsVsDateShow].Visible = true;
            grfToday.Cols[colgrfPttVsDiscDateShow].Visible = false;
            grfToday.Cols[colgrfPttVsDiscTime].Visible = false;
            grfToday.Cols[colgrfPttVsHn].Visible = true;
            grfToday.Cols[colgrfPttVsPreno].Visible = false;
            grfToday.Cols[colgrfPttVsDept].Visible = true;
            grfToday.Cols[colgrfPttVsStatusOPD].Visible = false;
            grfToday.Cols[colgrfPttVsSymptom].Visible = true;
            grfToday.Cols[colgrfPttVsPaid].Visible = true;
            grfToday.Cols[colgrfPttVsVN].Visible = false;
            grfToday.Cols[colgrfPttVsActno101].Visible = false;
            grfToday.Cols[colgrfPttVsActno101].Visible = false;
            grfToday.Cols[colgrfPttVsVsDate1].Visible = false;
            
            grfToday.Cols[colgrfPttVsVsDateShow].AllowEditing = false;
            grfToday.Cols[colgrfPttVsVsTime].AllowEditing = false;
            grfToday.Cols[colgrfPttVsHn].AllowEditing = true;
            grfToday.Cols[colgrfPttVsPreno].AllowEditing = false;
            grfToday.Cols[colgrfPttVsDept].AllowEditing = false;
            grfToday.Cols[colgrfPttVsStatusOPD].AllowEditing = false;
            grfToday.Cols[colgrfPttVsSymptom].AllowEditing = false;
            grfToday.Cols[colgrfPttVsAN].AllowEditing = false;
            grfToday.Cols[colgrfPttVsVN].AllowEditing = false;
            grfToday.Cols[colgrfPttVsFullNameT].AllowEditing = false;
            grfToday.Cols[colgrfPttVsPaid].AllowEditing = false;
            grfToday.Cols[colgrfPttVsQue].AllowEditing = false;
            grfToday.Cols[colgrfPttVsActno110].AllowEditing = false;
            grfToday.Cols[colgrfPttVsActno111].AllowEditing = false;
            grfToday.Cols[colgrfPttVsActno113].AllowEditing = false;
            grfToday.Cols[colgrfPttVsActno131].AllowEditing = false;
            grfToday.Cols[colgrfPttVsActno500].AllowEditing = false;
            grfToday.Cols[colgrfPttVsActno610].AllowEditing = false;
            grfToday.Cols[colgrfPttVsActno].AllowEditing = false;
            grfToday.Cols[colgrfPttVsRemark].AllowEditing = false;
            grfToday.AllowFiltering = true;
            grfToday.DoubleClick += GrfToday_DoubleClick;
            grfToday.KeyDown += GrfToday_KeyDown;
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ดูประวัติ Patient", new EventHandler(ContextMenu_PatientHistory));
            grfToday.ContextMenu = menuGw;
            pnTabTotay.Controls.Add(grfToday);
            theme1.SetTheme(grfToday, bc.iniC.themeApp);
        }
        private void ContextMenu_PatientHistory(object sender, System.EventArgs e)
        {
            if (grfToday.Row <= 0) return;
            if (grfToday.Col <= 0) return;
            Patient ptt = bc.bcDB.pttDB.selectPatinetByHn(grfToday[grfToday.Row, colgrfPttVsHn].ToString());
            openNewForm(grfToday[grfToday.Row, colgrfPttVsHn].ToString(), grfToday[grfToday.Row, colgrfPttVsFullNameT].ToString(), ref ptt);
        }
        private void GrfToday_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfToday.Row <= 0) return;
            if (grfToday.Col <= 0) return;
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                String txt = grfToday[grfToday.Row, grfToday.Col].ToString();
                Clipboard.SetText(txt);
            }
        }

        private void GrfToday_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfToday.Row <= 0) return;
            if (grfToday.Col <= 0) return;
            String err = "", hn="";
            try
            {
                wantEditVisit = false;
                VSDATE = "";
                PRENO = "";
                err = "01";
                hn = grfToday[grfToday.Row, colgrfPttVsHn].ToString();
                VSDATE = bc.datetoDB(grfToday[grfToday.Row, colgrfPttVsVsDateShow].ToString());
                PRENO = grfToday[grfToday.Row, colgrfPttVsPreno].ToString();
                ptt = bc.bcDB.pttDB.selectPatinetByHn(hn);
                setControlTabPateint(ptt);

                wantEditVisit = true;
                setControlTabVs(hn, VSDATE, PRENO);
                btnVsSave.Text = "แก้ไข visit";
                tC.SelectedTab = tabVs;
            }
            catch (Exception ex)
            {
                lfSbMessage.Text = "err " + err + " " + ex.Message;
                new LogWriter("e", "FrmReception BtnPttVisit_Click err " + err + " " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPttVisit_Click ", "err " + err + " " + ex.Message);
            }
        }
        private void setGrfToday()
        {
            
            DateTime datestart = rbDateSearch.Value;
            String time="";
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.vsDB.selectVisitByHn("", datestart.Year.ToString() + datestart.ToString("-MM-dd"), datestart.Year.ToString() + datestart.ToString("-MM-dd"));
            grfToday.Rows.Count = 1;
            grfToday.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfToday.Rows[i];
                rowa[colgrfPttVsVsDateShow] = bc.datetoShow1(row1["mnc_date"].ToString());
                rowa[colgrfPttVsVsTime] = bc.showTime(row1["MNC_TIME"].ToString());
                rowa[colgrfPttVsHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfPttVsFullNameT] = row1["ptt_fullnamet"].ToString();
                rowa[colgrfPttVsPaid] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[colgrfPttVsPreno] = row1["mnc_pre_no"].ToString();
                rowa[colgrfPttVsDept] = row1["MNC_AN_NO"].ToString().Equals("0") ? row1["dept_opd"].ToString(): row1["dept_ipd"].ToString();
                rowa[colgrfPttVsStatusOPD] = row1["mnc_sts_flg"].ToString();
                rowa[colgrfPttVsSymptom] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colgrfPttVsAN] = row1["MNC_AN_NO"].ToString().Equals("0") ? row1["MNC_VN_NO"].ToString() + "." + row1["MNC_VN_SEQ"].ToString()+ "." + row1["MNC_VN_SUM"].ToString() : row1["MNC_AN_NO"].ToString() + "." + row1["MNC_AN_YR"].ToString();
                rowa[colgrfPttVsQue] = row1["MNC_QUE_NO"].ToString();
                rowa[colgrfPttVsActno] = bc.adjustACTNO(row1["MNC_ACT_NO"].ToString());
                rowa[colgrfPttVsActno110] = bc.showTime(row1["act_no_110"].ToString());
                rowa[colgrfPttVsActno111] = bc.showTime(row1["act_no_111"].ToString());
                rowa[colgrfPttVsActno113] = bc.showTime(row1["act_no_113"].ToString());
                rowa[colgrfPttVsActno131] = bc.showTime(row1["act_no_131"].ToString());
                rowa[colgrfPttVsActno500] = bc.showTime(row1["act_no_500"].ToString());
                rowa[colgrfPttVsActno610] = bc.showTime(row1["act_no_610"].ToString());
                rowa[colgrfPttVsRemark] = row1["MNC_REF_DSC"].ToString();
                rowa[colgrfPttVsVsDate1] = row1["mnc_date"].ToString();
                rowa[0] = i.ToString();
                if (!row1["MNC_AN_NO"].ToString().Equals("0")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);}
                else if (row1["MNC_ACT_NO"].ToString().Equals("101")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#FFC5C5"); }//ส่งตัว
                else if (row1["MNC_ACT_NO"].ToString().Equals("110")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#61A3BA"); }//พบแพทย์
                else if (row1["MNC_ACT_NO"].ToString().Equals("111")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#ADBC9F"); }//Scan ใบยา
                else if (row1["MNC_ACT_NO"].ToString().Equals("310")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#FFBB64"); }//รับทำการ ห้องยา
                else if (row1["MNC_ACT_NO"].ToString().Equals("500")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#FFB996"); }//รอรับยา
                else if (row1["MNC_ACT_NO"].ToString().Equals("610")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EEE7DA"); }//รับชำระเงินแล้ว
                i++;
            }
        }
        private void openNewForm(String hn, String txt, ref Patient ptt)
        {
            //showFormWaiting();
            FrmPatient frm;
            if (ptt != null)
            {
                frm = new FrmPatient(bc, "", ref ptt);
                frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog(this);
            }
        }
        private void initGrfApm()
        {
            grfApm = new C1FlexGrid();
            grfApm.Font = fEdit;
            grfApm.Dock = System.Windows.Forms.DockStyle.Fill;
            grfApm.Location = new System.Drawing.Point(0, 0);
            grfApm.Rows.Count = 1;
            grfApm.Cols.Count = 19;

            grfApm.Cols[colgrfPttApmVsDate].Width = 100;
            grfApm.Cols[colgrfPttApmApmDateShow].Width = 90;
            grfApm.Cols[colgrfPttApmApmTime].Width = 60;
            grfApm.Cols[colgrfPttApmNote].Width = 400;
            grfApm.Cols[colgrfPttApmOrder].Width = 400;
            grfApm.Cols[colgrfPttApmHN].Width = 80;
            grfApm.Cols[colgrfPttApmPttName].Width = 250;
            grfApm.Cols[colgrfPttApmDeptR].Width = 200;
            grfApm.Cols[colgrfPttApmDtrname].Width = 200;
            grfApm.Cols[colgrfPttApmPhone].Width = 200;
            grfApm.Cols[colgrfPttApmRemarkCall].Width = 200;
            grfApm.Cols[colgrfPttApmStatusRemarkCall].Width = 200;
            grfApm.Cols[colgrfPttApmRemarkCallDate].Width = 140;

            grfApm.ShowCursor = true;
            grfApm.Cols[colgrfPttApmVsDate].Caption = "date";
            grfApm.Cols[colgrfPttApmApmDateShow].Caption = "นัดวันที่";
            grfApm.Cols[colgrfPttApmApmTime].Caption = "นัดเวลา";
            grfApm.Cols[colgrfPttApmDeptR].Caption = "นัดตรวจที่";
            grfApm.Cols[colgrfPttApmDeptMake].Caption = "แผนกทำนัด";
            grfApm.Cols[colgrfPttApmNote].Caption = "รายละเอียด";
            grfApm.Cols[colgrfPttApmOrder].Caption = "Order";

            grfApm.Cols[colgrfPttApmApmDateShow].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmApmTime].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmDeptR].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmNote].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmOrder].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmHN].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmPttName].DataType = typeof(String);
            grfApm.Cols[colgrfPttApmPhone].DataType = typeof(String);

            grfApm.Cols[colgrfPttApmApmDateShow].TextAlign = TextAlignEnum.CenterCenter;
            grfApm.Cols[colgrfPttApmApmTime].TextAlign = TextAlignEnum.CenterCenter;
            grfApm.Cols[colgrfPttApmDeptR].TextAlign = TextAlignEnum.LeftCenter;
            grfApm.Cols[colgrfPttApmNote].TextAlign = TextAlignEnum.LeftCenter;
            grfApm.Cols[colgrfPttApmOrder].TextAlign = TextAlignEnum.LeftCenter;
            grfApm.Cols[colgrfPttApmPhone].TextAlign = TextAlignEnum.LeftCenter;

            grfApm.Cols[colgrfPttApmVsDate].Visible = true;
            grfApm.Cols[colgrfPttApmApmDateShow].Visible = true;
            grfApm.Cols[colgrfPttApmDeptR].Visible = true;
            grfApm.Cols[colgrfPttApmNote].Visible = true;
            grfApm.Cols[colgrfPttApmDocNo].Visible = false;
            grfApm.Cols[colgrfPttApmDocYear].Visible = false;
            grfApm.Cols[colgrfPttApmVsDate].Visible = false;
            grfApm.Cols[colgrfPttApmHN].Visible = true;
            grfApm.Cols[colgrfPttApmPttName].Visible = true;
            grfApm.Cols[colgrfPttApmApmDate].Visible = true;

            grfApm.Cols[colgrfPttApmVsDate].AllowEditing = false;
            grfApm.Cols[colgrfPttApmApmDateShow].AllowEditing = false;
            grfApm.Cols[colgrfPttApmDeptR].AllowEditing = false;
            grfApm.Cols[colgrfPttApmNote].AllowEditing = false;
            grfApm.Cols[colgrfPttApmApmTime].AllowEditing = false;
            grfApm.Cols[colgrfPttApmOrder].AllowEditing = false;
            grfApm.Cols[colgrfPttApmRemarkCall].AllowEditing = false;
            grfApm.Cols[colgrfPttApmStatusRemarkCall].AllowEditing = false;
            grfApm.Cols[colgrfPttApmRemarkCallDate].AllowEditing = false;
            grfApm.Cols[colgrfPttApmDeptMake].AllowEditing = false;
            grfApm.Cols[colgrfPttApmPttName].AllowEditing = false;

            //grfApm.Cols[colgrfPttApmStatusRemarkCall].AllowFiltering = AllowFiltering.ByValue;
            grfApm.AllowFiltering = true;
            grfApm.DoubleClick += GrfApm_DoubleClick;
            grfApm.KeyDown += GrfApm_KeyDown;
            
            pnApm.Controls.Add(grfApm);
            theme1.SetTheme(grfApm, bc.iniC.themeApp);
        }
        private void GrfApm_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfApm.Row <= 0) return;
            if (grfApm.Col <= 0) return;
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                String txt = grfApm[grfApm.Row, grfApm.Col].ToString();
                Clipboard.SetText(txt);
            }
        }

        private void initGrfPttApm()
        {
            grfPttApm = new C1FlexGrid();
            grfPttApm.Font = fEdit;
            grfPttApm.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPttApm.Location = new System.Drawing.Point(0, 0);
            grfPttApm.Rows.Count = 1;
            grfPttApm.Cols.Count = 19;
            
            grfPttApm.Cols[colgrfPttApmVsDate].Width = 100;
            grfPttApm.Cols[colgrfPttApmApmDateShow].Width = 90;
            grfPttApm.Cols[colgrfPttApmApmTime].Width = 60;
            grfPttApm.Cols[colgrfPttApmNote].Width = 500;
            grfPttApm.Cols[colgrfPttApmOrder].Width = 500;
            grfPttApm.Cols[colgrfPttApmHN].Width = 80;
            grfPttApm.Cols[colgrfPttApmPttName].Width = 250;

            grfPttApm.ShowCursor = true;
            grfPttApm.Cols[colgrfPttApmVsDate].Caption = "date";
            grfPttApm.Cols[colgrfPttApmApmDateShow].Caption = "นัดวันที่";
            grfPttApm.Cols[colgrfPttApmApmTime].Caption = "นัดเวลา";
            grfPttApm.Cols[colgrfPttApmDeptR].Caption = "นัดตรวจที่";
            grfPttApm.Cols[colgrfPttApmDeptMake].Caption = "แผนกทำนัด";
            grfPttApm.Cols[colgrfPttApmNote].Caption = "รายละเอียด";
            grfPttApm.Cols[colgrfPttApmOrder].Caption = "Order";

            grfPttApm.Cols[colgrfPttApmPhone].Caption = "Phone";
            grfPttApm.Cols[colgrfPttApmPaidName].Caption = "Paid";
            grfPttApm.Cols[colgrfPttApmDtrname].Caption = "Doctor";

            grfPttApm.Cols[colgrfPttApmApmDateShow].DataType = typeof(String);
            grfPttApm.Cols[colgrfPttApmApmTime].DataType = typeof(String);
            grfPttApm.Cols[colgrfPttApmDeptR].DataType = typeof(String);
            grfPttApm.Cols[colgrfPttApmNote].DataType = typeof(String);
            grfPttApm.Cols[colgrfPttApmOrder].DataType = typeof(String);
            grfPttApm.Cols[colgrfPttApmHN].DataType = typeof(String);
            grfPttApm.Cols[colgrfPttApmPttName].DataType = typeof(String);

            grfPttApm.Cols[colgrfPttApmApmDateShow].TextAlign = TextAlignEnum.CenterCenter;
            grfPttApm.Cols[colgrfPttApmApmTime].TextAlign = TextAlignEnum.CenterCenter;
            grfPttApm.Cols[colgrfPttApmDeptR].TextAlign = TextAlignEnum.LeftCenter;
            grfPttApm.Cols[colgrfPttApmNote].TextAlign = TextAlignEnum.LeftCenter;
            grfPttApm.Cols[colgrfPttApmOrder].TextAlign = TextAlignEnum.LeftCenter;

            grfPttApm.Cols[colgrfPttApmVsDate].Visible = true;
            grfPttApm.Cols[colgrfPttApmApmDateShow].Visible = true;
            grfPttApm.Cols[colgrfPttApmDeptR].Visible = true;
            grfPttApm.Cols[colgrfPttApmNote].Visible = true;
            grfPttApm.Cols[colgrfPttApmDocNo].Visible = false;
            grfPttApm.Cols[colgrfPttApmDocYear].Visible = false;
            grfPttApm.Cols[colgrfPttApmVsDate].Visible = false;
            grfPttApm.Cols[colgrfPttApmHN].Visible = false;
            grfPttApm.Cols[colgrfPttApmPttName].Visible = true;
            grfPttApm.Cols[colgrfPttApmApmDate].Visible = false;

            grfPttApm.Cols[colgrfPttApmDocNo].Visible = false;
            grfPttApm.Cols[colgrfPttApmDocYear].Visible = false;
            grfPttApm.Cols[colgrfPttApmRemarkCall].Visible = false;
            grfPttApm.Cols[colgrfPttApmStatusRemarkCall].Visible = false;
            grfPttApm.Cols[colgrfPttApmRemarkCallDate].Visible = false;

            grfPttApm.Cols[colgrfPttApmVsDate].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmApmDateShow].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmDeptR].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmNote].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmApmTime].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmOrder].AllowEditing = false;
            grfPttApm.Cols[colgrfPttApmDeptMake].AllowEditing = false;
            grfPttApm.KeyDown += GrfPttApm_KeyDown;

            pnPttApm.Controls.Add(grfPttApm);
            theme1.SetTheme(grfPttApm, bc.iniC.themeApp);
        }

        private void GrfPttApm_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfPttApm.Row <= 0) return;
            if (grfPttApm.Col <= 0) return;
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                String txt = grfPttApm[grfPttApm.Row, grfPttApm.Col].ToString();
                Clipboard.SetText(txt);
            }
        }

        private void initGrfPttVs()
        {
            grfPttVs = new C1FlexGrid();
            grfPttVs.Font = fEdit;
            grfPttVs.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPttVs.Location = new System.Drawing.Point(0, 0);
            grfPttVs.Rows.Count = 1;
            grfPttVs.Cols.Count = 28;
            grfPttVs.Cols[colgrfPttVsStatusVisit].Width = 25;
            grfPttVs.Cols[colgrfPttVsVsDateShow].Width = 90;
            grfPttVs.Cols[colgrfPttVsVsTime].Width = 50;
            grfPttVs.Cols[colgrfPttVsDiscDateShow].Width = 90;
            grfPttVs.Cols[colgrfPttVsDiscTime].Width = 50;
            grfPttVs.Cols[colgrfPttVsHn].Width = 100;
            grfPttVs.Cols[colgrfPttVsFullNameT].Width = 250;
            grfPttVs.Cols[colgrfPttVsPreno].Width = 150;
            grfPttVs.Cols[colgrfPttVsDept].Width = 150;
            grfPttVs.Cols[colgrfPttVsStatusOPD].Width = 50;
            grfPttVs.Cols[colgrfPttVsSymptom].Width = 150;
            grfPttVs.Cols[colgrfPttVsAN].Width = 80;
            grfPttVs.Cols[colgrfPttVsActno].Width = 80;
            grfPttVs.Cols[colgrfPttVsRemark].Width = 300;
            grfPttVs.Cols[colgrfPttVsdtrName].Width = 300;
            grfPttVs.ShowCursor = true;
            grfPttVs.Cols[colgrfPttVsVsDateShow].Caption = "date(เข้า)";
            grfPttVs.Cols[colgrfPttVsVsTime].Caption = "time(เข้า)";
            grfPttVs.Cols[colgrfPttVsDiscDateShow].Caption = "date(ออก)";
            grfPttVs.Cols[colgrfPttVsDiscTime].Caption = "time(ออก)";
            grfPttVs.Cols[colgrfPttVsHn].Caption = "hn";
            grfPttVs.Cols[colgrfPttVsFullNameT].Caption = "ชื่อ-นามสกุล";
            grfPttVs.Cols[colgrfPttVsPreno].Caption = "preno";
            grfPttVs.Cols[colgrfPttVsDept].Caption = "ส่งตัวไป";
            grfPttVs.Cols[colgrfPttVsStatusOPD].Caption = "opd";
            grfPttVs.Cols[colgrfPttVsSymptom].Caption = "Symptom";
            grfPttVs.Cols[colgrfPttVsVN].Caption = "VN";
            grfPttVs.Cols[colgrfPttVsAN].Caption = "VN/AN";
            grfPttVs.Cols[colgrfPttVsPaid].Caption = "รักษาด้วยสิทธิ";
            grfPttVs.Cols[colgrfPttVsActno].Caption = "";
            grfPttVs.Cols[colgrfPttVsRemark].Caption = "หมายเหตุ";
            grfPttVs.Cols[colgrfPttVsdtrName].Caption = "แพทย์";
            grfPttVs.Cols[colgrfPttVsRemark].DataType = typeof(String);
            grfPttVs.Cols[colgrfPttVsVsTime].TextAlign = TextAlignEnum.CenterCenter;
            grfPttVs.Cols[colgrfPttVsPaid].TextAlign = TextAlignEnum.CenterCenter;
            grfPttVs.Cols[colgrfPttVsSymptom].TextAlign = TextAlignEnum.LeftCenter;
            grfPttVs.Cols[colgrfPttVsRemark].TextAlign = TextAlignEnum.LeftCenter;
            grfPttVs.Cols[colgrfPttVsVsDateShow].TextAlign = TextAlignEnum.CenterCenter;

            grfPttVs.Cols[colgrfPttVsVsDateShow].Visible = true;
            grfPttVs.Cols[colgrfPttVsHn].Visible = false;
            grfPttVs.Cols[colgrfPttVsPreno].Visible = false;
            grfPttVs.Cols[colgrfPttVsDept].Visible = true;
            grfPttVs.Cols[colgrfPttVsStatusOPD].Visible = false;
            grfPttVs.Cols[colgrfPttVsSymptom].Visible = true;
            grfPttVs.Cols[colgrfPttVsPaid].Visible = true;
            grfPttVs.Cols[colgrfPttVsFullNameT].Visible = false;
            grfPttVs.Cols[colgrfPttVsVN].Visible = false;
            grfPttVs.Cols[colgrfPttVsQue].Visible = false;
            grfPttVs.Cols[colgrfPttVsActno].Visible = false;
            grfPttVs.Cols[colgrfPttVsVsDate1].Visible = false;
            grfPttVs.Cols[colgrfPttVsActno101].Visible = false;
            grfPttVs.Cols[colgrfPttVsActno110].Visible = false;
            grfPttVs.Cols[colgrfPttVsActno111].Visible = false;
            grfPttVs.Cols[colgrfPttVsActno113].Visible = false;
            grfPttVs.Cols[colgrfPttVsActno131].Visible = false;
            grfPttVs.Cols[colgrfPttVsActno500].Visible = false;
            grfPttVs.Cols[colgrfPttVsActno610].Visible = false;
            grfPttVs.Cols[colgrfPttVsLimitCreditNo].Visible = false;

            grfPttVs.Cols[colgrfPttVsVsDateShow].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsVsTime].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsHn].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsPreno].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsDept].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsStatusOPD].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsSymptom].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsAN].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsVN].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsFullNameT].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsActno].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsPaid].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsRemark].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsDiscDateShow].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsDiscTime].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsStatusVisit].AllowEditing = false;
            grfPttVs.Cols[colgrfPttVsdtrName].AllowEditing = false;

            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ตรวจสถานะ visit", new EventHandler(ContextMenu_GrfPttVsStatusVisit));
            menuGw.MenuItems.Add("ข้อมูลวงเงินผู้ป่วย", new EventHandler(ContextMenu_GrfPttVsRew));
            grfPttVs.ContextMenu = menuGw;

            grfPttVs.DoubleClick += GrfPttVs_DoubleClick;
            grfPttVs.KeyDown += GrfPttVs_KeyDown;
            pnPttVs.Controls.Add(grfPttVs);
            theme1.SetTheme(grfPttVs, bc.iniC.themeApp);
        }

        private void GrfPttVs_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfPttVs.Row <= 0) return;
            if (grfPttVs.Col <= 0) return;
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                String txt = grfPttVs[grfPttVs.Row, grfPttVs.Col].ToString();
                Clipboard.SetText(txt);
            }
        }
        private void ContextMenu_GrfPttVsStatusVisit(object sender, System.EventArgs e)
        {
            
        }
        private void GrfPttVs_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if (grfPttVs.Row <= 0) return;
            //if (grfPttVs.Col <= 0) return;
            //String hn = "", vsdate = "", preno = "";
            //hn = grfPttVs[grfPttVs.Row, colgrfPttVsHn].ToString();
            //vsdate = grfPttVs[grfPttVs.Row, colgrfPttVsVsDate].ToString();
            //preno = grfPttVs[grfPttVs.Row, colgrfPttVsPreno].ToString();
            //setControlTabVs(hn, vsdate, preno);
            if (grfPttVs.Row <= 0) return;
            if (grfPttVs.Col <= 0) return;
            String hn = grfPttVs[grfPttVs.Row, colgrfPttVsHn].ToString();
            String preno = grfPttVs[grfPttVs.Row, colgrfPttVsPreno].ToString();
            String vsdate = grfPttVs[grfPttVs.Row, colgrfPttVsVsDate1].ToString();
            String name = grfPttVs[grfPttVs.Row, colgrfPttVsFullNameT].ToString();
            String symptoms = grfPttVs[grfPttVs.Row, colgrfPttVsSymptom].ToString();
            String remark = grfPttVs[grfPttVs.Row, colgrfPttVsRemark].ToString();
            String padname = grfPttVs[grfPttVs.Row, colgrfPttVsPaid].ToString();
            String vn = grfPttVs[grfPttVs.Row, colgrfPttVsAN].ToString();
            String deptname = grfPttVs[grfPttVs.Row, colgrfPttVsDept].ToString();
            FrmReceptionStatusVisit frm = new FrmReceptionStatusVisit(bc, "visit", hn, name, preno, bc.datetoDB(vsdate), symptoms.Replace("\r\n",""), txtPttCompCode.Text, txtPttInsur.Text, remark, padname,vn, deptname);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
            setGrfPttVs();
        }
        private void setGrfPttVs()
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.vsDB.selectVisitByHn(txtPttHn.Text.Trim());
            grfPttVs.Rows.Count = 1; grfPttVs.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfPttVs.Rows[i];
                //rowa[colgrfPttVsStatusVisit] = row1["MNC_PAT_FLAG"].ToString().Equals("I")?"A" : row1["MNC_ARV_TIME"].ToString().Equals("O") ? "O" : "";//ปชส ต้องการ ให้ admit แสดงเป็น A ถ้า OPD ไม่ต้องแสดงMNC_ARV_TIME
                rowa[colgrfPttVsStatusVisit] = row1["MNC_ADM_FLG"].ToString().Equals("A") ? "A" : row1["MNC_ARV_TIME"].ToString().Length>1 ? "O": "";//patient_t01 A = admin O = observe
                rowa[colgrfPttVsVsDateShow] = bc.datetoShowShort(row1["mnc_date"].ToString());
                rowa[colgrfPttVsHn] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfPttVsFullNameT] = row1["ptt_fullnamet"].ToString();
                rowa[colgrfPttVsPaid] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[colgrfPttVsPreno] = row1["mnc_pre_no"].ToString();
                rowa[colgrfPttVsDept] = row1["MNC_AN_NO"].ToString().Equals("0") ? row1["dept_opd"].ToString() : row1["dept_ipd"].ToString();
                rowa[colgrfPttVsStatusOPD] = row1["mnc_sts_flg"].ToString();
                rowa[colgrfPttVsSymptom] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colgrfPttVsAN] = row1["MNC_AN_NO"].ToString().Equals("0") ? row1["MNC_VN_NO"].ToString()+"."+row1["MNC_VN_SEQ"].ToString() + "." + row1["MNC_VN_SUM"].ToString() : row1["MNC_AN_NO"].ToString() + "." + row1["MNC_AN_YR"].ToString();
                rowa[colgrfPttVsQue] = row1["MNC_QUE_NO"].ToString();
                rowa[colgrfPttVsRemark] = row1["MNC_REF_DSC"].ToString();
                rowa[colgrfPttVsVsTime] = bc.showTime(row1["MNC_TIME"].ToString());
                rowa[colgrfPttVsVsDate1] = row1["mnc_date"].ToString();
                rowa[colgrfPttVsDiscDateShow] = row1["MNC_AN_NO"].ToString().Equals("0") ? bc.datetoShowShort(row1["MNC_OUT_DAT"].ToString()) : bc.datetoShowShort(row1["MNC_DS_DATE"].ToString());
                rowa[colgrfPttVsDiscTime] = row1["MNC_AN_NO"].ToString().Equals("0") ? bc.showTime(row1["MNC_OUT_TIM"].ToString()) : bc.showTime(row1["MNC_DS_TIME"].ToString());
                rowa[colgrfPttVsLimitCreditNo] = row1["limit_credit_no"].ToString();
                rowa[colgrfPttVsdtrName] = row1["MNC_AN_NO"].ToString().Equals("0") ? row1["dtr_name"].ToString() : row1["dtr_nameipd"].ToString();
                if (!row1["MNC_AN_NO"].ToString().Equals("0")) {rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);}
                if (row1["limit_credit_no"].ToString().Length > 0)
                {
                    String limitcredit = row1["limit_credit_no"].ToString();
                    CellNote note = new CellNote("มีวงเงิน " + row1["limit_credit_no"].ToString());
                    CellRange rg = grfPttVs.GetCellRange(i, colgrfPttVsPaid);
                    rg.UserData = note;
                }
                rowa[0] = i.ToString();
                i++;
            }
            CellNoteManager mgr = new CellNoteManager(grfPttVs);
        }
        private void setControlTabVs(String hn,String vsdate, String preno)
        {
            //ต้องการ ให้แก้ไข dept ที่ส่งไปแล้ว หรือแก้ไขอาการ
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectByPreno(hn, vsdate, preno);
            if (dt.Rows.Count > 0)
            {
                txtVsPaidCode.Value = "";
                cboVsDept.SelectedIndex = 0;
                txtVsPaidCode.Value = bc.bcDB.finM02DB.getPaidName(dt.Rows[0]["MNC_FN_TYP_CD"].ToString());
                bc.setC1Combo(cboVsDept, dt.Rows[0]["MNC_SEC_NO"].ToString());
                bc.setC1Combo(cboVsType, dt.Rows[0]["MNC_PT_FLG"].ToString());
                //txtVsRemark.Value = dt.Rows[0]["MNC_HN_NO"].ToString();
                //txtVsNote.Value = dt.Rows[0]["MNC_HN_NO"].ToString();
                lbVsPttNameT.Text = ptt.Name;
                txtVsHN.Value = dt.Rows[0]["MNC_HN_NO"].ToString();
                txtVsSymptom.Value = dt.Rows[0]["MNC_SHIF_MEMO"].ToString();
                lbVsStatus.Text = "VN "+ dt.Rows[0]["MNC_VN_NO"].ToString()+"."+ dt.Rows[0]["MNC_VN_SEQ"].ToString() + "." + dt.Rows[0]["MNC_VN_SUM"].ToString() + bc.adjustACTNO(dt.Rows[0]["MNC_ACT_NO"].ToString());
                txtVsComp.Value = txtPttCompCode.Text;
                txtVsInsur.Value = txtPttInsur.Text;
                chkVsStatusDOE.Checked = dt.Rows[0]["status_alien_doe"].ToString().Equals("1") ? true : false; 
                clearTxtUser();
            }
        }
        private void GrfPttComp_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void initGrfRptData()
        {
            grfRptData = new C1FlexGrid();
            grfRptData.Font = fEdit;
            grfRptData.Dock = System.Windows.Forms.DockStyle.Fill;
            grfRptData.Location = new System.Drawing.Point(0, 0);
            grfRptData.Rows.Count = 1;
            pnRptData.Controls.Add(grfRptData);
            theme1.SetTheme(grfRptData, bc.iniC.themeApp);
        }
        private void initGrfRptReport()
        {
            grfRptReport = new C1FlexGrid();
            grfRptReport.Font = fEdit;
            grfRptReport.Dock = System.Windows.Forms.DockStyle.Fill;
            grfRptReport.Location = new System.Drawing.Point(0, 0);
            grfRptReport.Rows.Count = 4;
            grfRptReport.Cols.Count = 3;
            grfRptReport.Cols[colgrfRptReportCode].Width = 100;
            grfRptReport.Cols[colgrfRptReportName].Width = 400;
            //grfRptReport.Cols[colgrfPttCompid].Width = 150;
            grfRptReport.ShowCursor = true;
            grfRptReport.Cols[colgrfRptReportCode].Caption = "code";
            grfRptReport.Cols[colgrfRptReportName].Caption = "name";
            //grfRptReport.Cols[colgrfPttCompid].Caption = "id";

            grfRptReport.Cols[colgrfRptReportCode].Visible = false;
            grfRptReport.Cols[colgrfRptReportName].Visible = true;
            //grfRptReport.Cols[colgrfPttCompid].Visible = false;

            grfRptReport.Cols[colgrfRptReportCode].AllowEditing = false;
            grfRptReport.Cols[colgrfRptReportName].AllowEditing = false;
            grfRptReport.AllowSorting = AllowSortingEnum.None;

            grfRptReport.Rows[1][colgrfRptReportCode] = "reportdailydept";
            grfRptReport.Rows[1][colgrfPttCompNameT] = "รายงานยอดคนไข้ ตามแผนก";

            grfRptReport.Rows[2][colgrfRptReportCode] = "reportdailydeptHI";
            grfRptReport.Rows[2][colgrfPttCompNameT] = "รายงานยอดคนไข้ ตามแผนก Home Isolate";

            grfRptReport.Rows[3][colgrfRptReportCode] = "reportdailydeptHIATK";
            grfRptReport.Rows[3][colgrfPttCompNameT] = "รายงานยอดคนไข้ ตามแผนก Home Isolate ATK Screening";

            grfRptReport.Click += GrfRptReport_Click;

            pnRptReport.Controls.Add(grfRptReport);
            theme1.SetTheme(grfRptReport, bc.iniC.themeApp);
        }

        private void GrfRptReport_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfRptReport.Row <= 0) return;
            if (grfRptReport.Col <= 0) return;
            rptCode = grfRptReport[grfRptReport.Row, colgrfRptReportCode]!= null ? grfRptReport[grfRptReport.Row,colgrfRptReportCode].ToString():"";
            lfSbMessage.Text = "report "+rptCode ;
        }

        private void initGrfSrc()
        {
            grfSrc = new C1FlexGrid();
            grfSrc.Font = fEdit;
            grfSrc.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSrc.Location = new System.Drawing.Point(0, 0);
            grfSrc.Rows.Count = 1;
            grfSrc.Cols.Count = 10;
            grfSrc.Cols[colgrfSrcHn].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcFullNameT].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcPID].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcDOB].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].DataType = typeof(String);
            grfSrc.Cols[colgrfSrcHn].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcFullNameT].TextAlign = TextAlignEnum.LeftCenter;
            grfSrc.Cols[colgrfSrcPID].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcDOB].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcAge].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcVisitReleaseIPD].TextAlign = TextAlignEnum.CenterCenter;
            grfSrc.Cols[colgrfSrcHn].Width = 100;
            grfSrc.Cols[colgrfSrcFullNameT].Width = 300;
            grfSrc.Cols[colgrfSrcPID].Width = 150;
            grfSrc.Cols[colgrfSrcDOB].Width = 90;
            grfSrc.Cols[colgrfSrcPttid].Width = 60;
            grfSrc.Cols[colgrfSrcAge].Width = 90;
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].Width = 90;
            grfSrc.Cols[colgrfSrcVisitReleaseIPD].Width = 90;
            grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].Width = 90;
            grfSrc.ShowCursor = true;
            grfSrc.Cols[colgrfSrcHn].Caption = "hn";
            grfSrc.Cols[colgrfSrcFullNameT].Caption = "full name";
            grfSrc.Cols[colgrfSrcPID].Caption = "PID";
            grfSrc.Cols[colgrfSrcDOB].Caption = "DOB";
            grfSrc.Cols[colgrfSrcPttid].Caption = "";
            grfSrc.Cols[colgrfSrcAge].Caption = "AGE";
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].Caption = "มาล่าสุดOPD";
            grfSrc.Cols[colgrfSrcVisitReleaseIPD].Caption = "มาล่าสุดIPD";
            grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].Caption = "กลับบ้านIPD";

            grfSrc.Cols[colgrfSrcHn].Visible = true;
            grfSrc.Cols[colgrfSrcFullNameT].Visible = true;
            grfSrc.Cols[colgrfSrcPID].Visible = true;
            grfSrc.Cols[colgrfSrcDOB].Visible = true;
            grfSrc.Cols[colgrfSrcPttid].Visible = false;
            grfSrc.Cols[colgrfSrcAge].Visible = true;

            grfSrc.Cols[colgrfSrcHn].AllowEditing = false;
            grfSrc.Cols[colgrfSrcFullNameT].AllowEditing = false;
            grfSrc.Cols[colgrfSrcPID].AllowEditing = false;
            grfSrc.Cols[colgrfSrcDOB].AllowEditing = false;
            grfSrc.Cols[colgrfSrcAge].AllowEditing = false;
            grfSrc.Cols[colgrfSrcVisitReleaseOPD].AllowEditing = false;
            grfSrc.Cols[colgrfSrcVisitReleaseIPD].AllowEditing = false;
            grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].AllowEditing = false;
            grfSrc.AllowFiltering = true;

            grfSrc.DoubleClick += GrfSrc_DoubleClick;
            //grfSrc.AutoSizeCols();
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);
            
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnSrcGrf.Controls.Add(grfSrc);
            theme1.SetTheme(grfSrc, bc.iniC.themeApp);
        }
        private void ContextMenu_GrfPttVsRew(object sender, System.EventArgs e)
        {
            if (grfSrc.Row <= 0) return;
            if (grfSrc.Col <= 0) return;
            FrmReceptionStatusVisit frm = new FrmReceptionStatusVisit(bc, "rew", "", "", "", "","","","","","","","'");
        }
        private void GrfSrc_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfSrc.Row <= 0) return;
            if (grfSrc.Col <= 0) return;

            setControlPatientByGrf(grfSrc.Row);
        }
        private void setControlPatientByGrf(int row)
        {
            String hn = "";
            hn = grfSrc[row, colgrfSrcHn] != null ? grfSrc[row, colgrfSrcHn].ToString() : "";
            ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetByHn(hn);
            if (ptt.Hn.Length > 5)
            {
                setControlTabPateint(ptt);
                tC.SelectedTab = tabPtt;
            }
            else
            {
                lfSbStatus.Text = "";
                lfSbMessage.Text = "";
            }
        }
        private void setControlTabPateint(Patient ptt)
        {
            showLbLoading();
            wanttoSave = false;
            lfSbStatus.Text = "";            lfSbMessage.Text = "";            lbPttinWard.Text = "";            lbFindPaidSSO.Text = "";            QUENO = "";
            try
            {
                setLbLoading("patient tab01");
                clearControl();
                m_picPhoto.Image = null;
                DateTime dob = new DateTime();
                DateTime.TryParse(ptt.MNC_BDAY, out dob);
                dob = dob.AddYears(543);
                txtPttHn.Value = ptt.Hn;
                txtPttDOB.Value = dob;
                txtPttDOBDD.Value = dob.ToString("dd");
                txtPttDOBMM.Value = dob.ToString("MM");
                txtPttDOBYear.Value = (dob.Year+543).ToString();
                bc.setC1Combo(cboPttPrefixT, ptt.MNC_PFIX_CDT);
                txtPttNameT.Value = ptt.MNC_FNAME_T;
                txtPttSurNameT.Value = ptt.MNC_LNAME_T;
                txtPttNameE.Value = ptt.MNC_FNAME_E;
                txtPttSurNameE.Value = ptt.MNC_LNAME_E;

                txtPttPID.Value = ptt.MNC_ID_NO;
                txtPttSsn.Value = ptt.MNC_SS_NO;
                txtPttPassport.Value = ptt.passport;
                txtPttMobile1.Value = ptt.MNC_CUR_TEL;
                txtPttEmail.Value = "";
                txtPttAge.Value = ptt.AgeStringShort1();

                txtDocHn.Value = ptt.MNC_HN_NO;
                lbDocFullname.Text = ptt.Name;

                bc.setC1Combo(cboPttNat, ptt.MNC_NAT_CD);//สัญชาติ
                bc.setC1Combo(cboPttRel, ptt.MNC_REL_CD);
                bc.setC1Combo(cboPttEdu, ptt.MNC_EDU_CD);
                bc.setC1Combo(cboPttMarri, ptt.MNC_STATUS);
                bc.setC1Combo(cboPttRace, ptt.MNC_NATI_CD);//เชื้อชาติ
                bc.setC1Combo(cboPttSex, ptt.MNC_SEX);

                txtPttIDHomeNo.Value = ptt.MNC_DOM_ADD;
                txtPttIDMoo.Value = ptt.MNC_DOM_MOO;
                txtPttIDSoi.Value = ptt.MNC_DOM_SOI;
                txtPttIDRoad.Value = ptt.MNC_DOM_ROAD;
                txtPttIDPostcode.Value = ptt.MNC_DOM_POC;
                txtPttIdSearchTambon.Value = bc.bcDB.pm07DB.getTambonName( ptt.MNC_DOM_TUM);
                txtPttIdAmp.Value = bc.bcDB.pm07DB.getAmpName(ptt.MNC_DOM_AMP);
                txtPttIdChw.Value = bc.bcDB.pm07DB.getChwName(ptt.MNC_DOM_CHW);
                
                txtPttCurHomeNo.Value = ptt.MNC_CUR_ADD;
                txtPttCurMoo.Value = ptt.MNC_CUR_MOO;
                txtPttCurSoi.Value = ptt.MNC_CUR_SOI;
                txtPttCurRoad.Value = ptt.MNC_CUR_ROAD;
                txtPttCurPostcode.Value = ptt.MNC_CUR_POC;
                txtPttCurSearchTambon.Value = bc.bcDB.pm07DB.getTambonName(ptt.MNC_CUR_TUM);
                txtPttCurAmp.Value = bc.bcDB.pm07DB.getAmpName(ptt.MNC_CUR_AMP);
                txtPttCurChw.Value = bc.bcDB.pm07DB.getChwName(ptt.MNC_CUR_CHW);
                setLbLoading("patient tab02");
                txtPttRefHomeNo.Value = ptt.MNC_REF_ADD;
                txtPttRefMoo.Value = ptt.MNC_REF_MOO;
                txtPttRefSoi.Value = ptt.MNC_REF_SOI;
                txtPttRefRoad.Value = ptt.MNC_REF_ROAD;
                txtPttRefPostcode.Value = ptt.MNC_REF_POC;
                txtPttRefSearchTambon.Value = bc.bcDB.pm07DB.getTambonName(ptt.MNC_REF_TUM);
                txtPttRefAmp.Value = bc.bcDB.pm07DB.getAmpName(ptt.MNC_REF_AMP);
                txtPttRefChw.Value = bc.bcDB.pm07DB.getChwName(ptt.MNC_REF_CHW);
                txtPttRefContact1Mobile.Value = ptt.MNC_REF_TEL;
                txtPttRefContact1Rel.Value = ptt.MNC_REF_REL;

                txtPttCompCode.Value = bc.bcDB.pm24DB.getPaidName(ptt.MNC_COM_CD2);
                txtPttInsur.Value = bc.bcDB.pm24DB.getPaidName(ptt.MNC_COM_CD);
                //lbPttCompNameT.Text = ptt.comNameT;
                //lbPttInsurNameT.Text = ptt.insurNameT;
                txtPttRefContact1Name.Value = ptt.MNC_REF_NAME;
                txtPttRefContact2Name.Value = "";
                txtPttwp1.Value = ptt.WorkPermit1;
                txtPttwp2.Value = ptt.WorkPermit2;
                txtPttwp3.Value = ptt.WorkPermit3;
                txtPttRemark1.Value = ptt.remark1;
                txtPttRemark2.Value = ptt.remark2;
                txtPttRef1.Value = ptt.ref1;
                txtPttPassportOld.Value = ptt.passportold;
                //bc.bcDB.pm08DB.setCboAmphurByAmphurCode(cboPttIDAmphur, code, code);
                //bc.bcDB.pm09DB.setCboProvByProvCode(cboPttIDProv, code, code);
                setLbLoading("patient tab03");
                txtPttAttchNote.Value = ptt.MNC_ATT_NOTE;
                txtPttPaid.Value = bc.bcDB.finM02DB.getPaidName(ptt.MNC_FN_TYP_CD);
                setLbLoading("patient tab04");
                setPttnameTabVs("");
                setLbLoading("patient tab05");
                clearTxtUser();
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmReception setControlTabPateint " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "setControlTabPateint ", ex.Message);
                lfSbMessage.Text = ex.Message;
            }
            setLbLoading("patient tab06");
            setGrfPttVs();
            setLbLoading("patient tab07");
            setGrfPttApm();
            setLbLoading("patient tab08");
            checkPaidSSO(ptt.MNC_ID_NO);
            checkPttInWard();
            setLbLoading("patient tab09");
            hideLbLoading();
        }
        private void clearTxtUser()
        {
            txtVsUser.Value = "";            lbVsUser.Text = "";            lbPttUser.Text = "";            txtPttUser.Value = "";
        }
        private void GrfApm_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfApm.Row <= 0) return;
            if (grfApm.Col <= 0) return;

            PatientT07 apm = new PatientT07();
            apm.MNC_SEC_NO = grfApm[grfApm.Row, colgrfPttApmDeptR].ToString();
            apm.MNC_APP_DSC = grfApm[grfApm.Row, colgrfPttApmNote].ToString();
            apm.MNC_REM_MEMO = grfApm[grfApm.Row, colgrfPttApmOrder].ToString();
            apm.MNC_APP_DAT = grfApm[grfApm.Row, colgrfPttApmApmDateShow].ToString();
            apm.MNC_APP_TIM = grfApm[grfApm.Row, colgrfPttApmApmTime].ToString();
            apm.MNC_DOC_NO = grfApm[grfApm.Row, colgrfPttApmDocNo].ToString();
            apm.MNC_DOC_YR = grfApm[grfApm.Row, colgrfPttApmDocYear].ToString();
            apm.MNC_HN_NO = grfApm[grfApm.Row, colgrfPttApmHN].ToString();
            apm.MNC_SEC_NO = grfApm[grfApm.Row, colgrfPttApmDeptR].ToString();
            apm.MNC_SEC_NO = grfApm[grfApm.Row, colgrfPttApmDeptR].ToString();
            apm.remark_call = grfApm[grfApm.Row, colgrfPttApmRemarkCall].ToString();
            apm.status_remark_call = grfApm[grfApm.Row, colgrfPttApmStatusRemarkCall].ToString();

            ptt.Hn = grfApm[grfApm.Row, colgrfPttApmHN].ToString();
            ptt.Name = grfApm[grfApm.Row, colgrfPttApmPttName].ToString();
            ptt.MNC_CUR_TEL = grfApm[grfApm.Row, colgrfPttApmPhone].ToString();

            FrmApmCall frm = new FrmApmCall(bc, this.ptt, apm);
            frm.ShowDialog(this);
            setGrfApm();
        }
        private void setGrfApm()
        {
            if (pageLoad) return;
            DataTable dtvs = new DataTable();
            if(chkApmDate.Checked)
            {
                DateTime.TryParse(txtApmDate.Value.ToString(), out DateTime dtapm);
                if (dtapm.Year < 1900)
                {
                    dtapm.AddYears(543);
                }
                String paidcode = "";
                paidcode = cboApmPaid.SelectedItem == null ? "" : ((ComboBoxItem)cboApmPaid.SelectedItem).Value.ToString();
                dtvs = bc.bcDB.pt07DB.selectByDate(dtapm.Year.ToString() + "-" + dtapm.ToString("MM-dd"), paidcode,"");
            }
            else if (chkApmHn.Checked)
            {
                dtvs = bc.bcDB.pt07DB.selectByHnAll(txtApmHn.Text.Trim(),"desc");
            }
            grfApm.Rows.Count = 1; grfApm.Rows.Count = dtvs.Rows.Count + 1;
            int i = 1, j = 1;
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfApm.Rows[i];
                rowa[colgrfPttApmApmDateShow] = bc.datetoShowShort(row1["MNC_APP_DAT"].ToString());
                rowa[colgrfPttApmApmDate] = row1["MNC_APP_DAT"].ToString();
                rowa[colgrfPttApmApmTime] = bc.showTime(row1["MNC_APP_TIM"].ToString());
                rowa[colgrfPttApmDeptR] = row1["mnc_md_dep_dsc"].ToString();
                rowa[colgrfPttApmDeptMake] = bc.bcDB.pm32DB.getDeptNameOPD(row1["mnc_sec_no"].ToString());
                rowa[colgrfPttApmNote] = row1["MNC_APP_DSC"].ToString();
                rowa[colgrfPttApmOrder] = row1["MNC_REM_MEMO"].ToString();

                rowa[colgrfPttApmDocNo] = row1["MNC_DOC_NO"].ToString();
                rowa[colgrfPttApmDocYear] = row1["MNC_DOC_YR"].ToString();
                rowa[colgrfPttApmHN] = row1["MNC_HN_NO"].ToString();
                rowa[colgrfPttApmPttName] = row1["ptt_fullnamet"].ToString();
                rowa[colgrfPttApmDtrname] = row1["dtr_name"].ToString();
                rowa[colgrfPttApmPhone] = row1["MNC_CUR_TEL"].ToString();
                rowa[colgrfPttApmPaidName] = row1["MNC_FN_TYP_DSC"].ToString();

                rowa[colgrfPttApmRemarkCall] = row1["remark_call"].ToString();
                rowa[colgrfPttApmRemarkCallDate] = row1["remark_call_date"].ToString();
                if (row1["status_remark_call"].ToString().Equals("1")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรเรียบร้อย รับสาย บุคคลอื่นเป็นคนรับ"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);}
                else if (row1["status_remark_call"].ToString().Equals("2")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรเรียบร้อย ไม่รับสาย"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");}//#EBBDB6
                else if (row1["status_remark_call"].ToString().Equals("3")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรไม่ติด สายไม่ว่าง"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#CCCCFF");}
                else if (row1["status_remark_call"].ToString().Equals("4")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรเรียบร้อย รับสาย แจ้งคนไข้ ครบถ้วน"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#9FE2BF");}
                else if (row1["status_remark_call"].ToString().Equals("5")) { rowa[colgrfPttApmStatusRemarkCall] = "ไม่สามารถโทรได้ ไม่มีเบอร์โทร"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#FF7F50");}
                else rowa[colgrfPttApmStatusRemarkCall] = "";

                rowa[0] = i.ToString();
                i++;
            }
            lfSbMessage.Text = "พบ " + dtvs.Rows.Count+"รายการ";
        }
        private void setGrfPttApm()
        {
            DataTable dtvs = new DataTable();
            dtvs = bc.bcDB.pt07DB.selectByHnAll(txtPttHn.Text.Trim(),"desc");
            grfPttApm.Rows.Count = 1; grfPttApm.Rows.Count = dtvs.Rows.Count + 1;

            int i = 1, j = 1;
            String time = "";
            foreach (DataRow row1 in dtvs.Rows)
            {
                Row rowa = grfPttApm.Rows[i];
                rowa[colgrfPttApmApmDateShow] = bc.datetoShowShort(row1["MNC_APP_DAT"].ToString());
                rowa[colgrfPttApmApmDate] = row1["MNC_APP_DAT"].ToString();
                rowa[colgrfPttApmApmTime] = bc.showTime(row1["MNC_APP_TIM"].ToString());
                rowa[colgrfPttApmDeptR] = row1["mnc_md_dep_dsc"].ToString();
                rowa[colgrfPttApmDeptMake] = bc.bcDB.pm32DB.getDeptNameOPD(row1["mnc_sec_no"].ToString());
                rowa[colgrfPttApmNote] = row1["MNC_APP_DSC"].ToString();
                rowa[colgrfPttApmOrder] = row1["MNC_REM_MEMO"].ToString();

                rowa[colgrfPttApmDocNo] = row1["MNC_DOC_NO"].ToString();
                rowa[colgrfPttApmDocYear] = row1["MNC_DOC_YR"].ToString();

                rowa[colgrfPttApmDtrname] = row1["dtr_name"].ToString();
                rowa[colgrfPttApmPhone] = row1["MNC_CUR_TEL"].ToString();
                rowa[colgrfPttApmPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
                rowa[0] = i.ToString();
                i++;
            }
        }
        private void setGrfSrc()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.pttDB.selectPatinetBySearch(txtSrcHn.Text.Trim());
            int i = 1, j = 1;
            grfSrc.Rows.Count = 1;            grfSrc.Rows.Count = dt.Rows.Count+1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                try
                {
                    Row rowa = grfSrc.Rows[i];
                    ptt.patient_birthday = row1["MNC_bday"].ToString();
                    rowa[colgrfSrcHn] = row1["MNC_HN_NO"].ToString();
                    rowa[colgrfSrcFullNameT] = row1["pttfullname"].ToString();
                    rowa[colgrfSrcPID] = row1["mnc_id_no"].ToString();
                    rowa[colgrfSrcDOB] = bc.datetoShowShort(row1["MNC_bday"].ToString());
                    rowa[colgrfSrcAge] = ptt.AgeStringShort1();
                    rowa[colgrfSrcVisitReleaseOPD] = bc.datetoShowShort(row1["MNC_LAST_CONT"].ToString());
                    rowa[colgrfSrcVisitReleaseIPD] = bc.datetoShowShort(row1["MNC_LAST_CONT_I"].ToString());
                    rowa[colgrfSrcVisitReleaseIPDDischarge] = bc.datetoShowShort(row1["ipd_discharge_release"].ToString());
                    rowa[colgrfSrcPttid] = "";
                    rowa[0] = i.ToString();
                    i++;
                }
                catch(Exception ex)
                {
                    lfSbMessage.Text = ex.Message;
                    new LogWriter("e", "FrmReception setGrfSrc " + ex.Message);
                    bc.bcDB.insertLogPage(bc.userId, this.Name, "setGrfSrc ", ex.Message);
                }
            }
        }
        private void setLbLoading(String txt)
        {
            lbLoading.Text = txt;            Application.DoEvents();
        }
        private void showLbLoading()
        {
            lbLoading.Show();            lbLoading.BringToFront();            Application.DoEvents();
        }
        private void hideLbLoading()
        {
            lbLoading.Hide();            Application.DoEvents();
        }
        private void setTabIndex()
        {
            txtPttPID.TabIndex = 0;
            cboPttPrefixT.TabIndex = 1;
            txtPttNameT.TabIndex = 2;
            txtPttSurNameT.TabIndex=3;
            txtPttNameE.TabIndex=4;
            txtPttSurNameE.TabIndex = 5;
            txtPttPassport.TabIndex = 6;
            txtPttMobile1.TabIndex = 7;
            txtPttMobile2.TabIndex = 8;
            txtPttEmail.TabIndex = 9;
            txtPttwp1.TabIndex = 10;
            txtPttwp2.TabIndex = 11;
            txtPttwp3.TabIndex = 12;
            txtPttIDHomeNo.TabIndex = 13;
            txtPttIDMoo.TabIndex = 14;
            txtPttIDSoi.TabIndex = 15;
            txtPttIDRoad.TabIndex = 16;
            txtPttIdSearchTambon.TabIndex = 17;
            txtPttIdAmp.TabIndex = 18;
            txtPttIdChw.TabIndex = 19;
            txtPttIDPostcode.TabIndex = 20;
            txtPttCurHomeNo.TabIndex=21;
            txtPttCurMoo.TabIndex=22;
            txtPttCurSoi.TabIndex = 23;
            txtPttCurRoad.TabIndex=24;
            txtPttCurSearchTambon.TabIndex=25;
            txtPttCurAmp.TabIndex=26;
            txtPttCurChw.TabIndex=27;
            txtPttCurPostcode.TabIndex=28;
            txtPttRefHomeNo.TabIndex = 29;
            txtPttRefMoo.TabIndex = 30;
            txtPttRefSoi.TabIndex=31;
            txtPttRefRoad.TabIndex=32;
            txtPttRefSearchTambon.TabIndex = 33;
            txtPttRefAmp.TabIndex = 34;
            txtPttRefChw.TabIndex = 35;
            txtPttRefPostcode.TabIndex = 36;
            txtPttRefContact1Name.TabIndex = 37;
            txtPttRefContact1Mobile.TabIndex = 38;
            txtPttRefContact2Name.TabIndex = 39;
            txtPttRefContact2Mobile.TabIndex = 40;
            txtPttRefContact1Rel.TabIndex = 41;
            txtPttRefContact2Rel.TabIndex=42;
            txtPttPaid.TabIndex = 43;
            txtPttCompCode.TabIndex = 44;
            txtPttInsur.TabIndex = 45;
            cboPttNat.TabIndex = 46;
            cboPttMarri.TabIndex = 47;
            txtPttAttchNote.TabIndex = 48;
            txtPttRemark1.TabIndex = 49;
            txtPttRemark2.TabIndex = 50;
            txtPttUser.TabIndex = 51;
            btnPttCardRead.TabIndex = 52;
            btnPttSave.TabIndex = 53;
            btnPttVisit.TabIndex = 54;

            txtPttHn.TabStop = false;
        }
        private void printQueue()
        {
            try
            {
                PrintDocument document = new PrintDocument();
                document.PrinterSettings.PrinterName = bc.iniC.printerQueue;
                document.PrintPage += Document_PrintPageQue;
                document.DefaultPageSettings.Landscape = false;

                document.Print();
            }
            catch (Exception ex)
            {

            }
        }
        private void Document_PrintPageQue(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            String amt = "", line = null, date = "", price = "", qty = "", price1 = "", err="";
            Decimal amt1 = 0, voucamt = 0, discount = 0, total = 0, cash = 0;
            float yPos = 0, gap = 6, colName = 0, col2 = 5, col3 = 250, colPrice = 150, colPriceR2L = 180, colqty = 200, colqtyRtoL = 225, colamt = 230, colamtRtoL = 285, col4 = 820, col40 = 620;
            int count = 0, recx = 15, recy = 15, col2int = 0, yPosint = 0, col40int = 0;

            Graphics g = e.Graphics;
            SolidBrush Brush = new SolidBrush(Color.Black);
            try
            {
                date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                Pen blackPen = new Pen(Color.Black, 1);
                Size proposedSize = new Size(100, 100);
                err = "01";
                StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps
                Size textSize = TextRenderer.MeasureText(line, fPrnBil, proposedSize, TextFormatFlags.RightToLeft);
                StringFormat sfR2L = new StringFormat();
                sfR2L.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                Int32 xOffset = e.MarginBounds.Right - textSize.Width;  //pad?
                Int32 yOffset = e.MarginBounds.Bottom - textSize.Height;  //pad?
                float marginR = e.MarginBounds.Right;
                float avg = marginR / 2;
                Rectangle rec = new Rectangle(0, 0, 20, 20);
                col2int = int.Parse(col2.ToString());
                yPosint = int.Parse(yPos.ToString());
                col40int = int.Parse(col40.ToString());
                err = "02";
                e.Graphics.DrawString(QUEDEPT, fque, Brushes.Black, 0, yPos, flags);
                e.Graphics.DrawString(QUENO, ftotal, Brushes.Black, 200, yPos, flags);

                e.Graphics.DrawString("H.N. " + QUEHN, fqueB, Brushes.Black, 0, yPos + 25, flags);        //bc.pdfFontSize + 7        pdfFontName     FontStyle.Bold
                lfSbMessage.Text = QUENO;
                e.Graphics.DrawString(QUEFullname, fque, Brushes.Black, 0, yPos + 50, flags);
                err = "03";
                e.Graphics.DrawString("อายุ "+txtPttAge.Text, fque, Brushes.Black, 0, yPos + 70, flags);
                e.Graphics.DrawString(QUESymptoms, fque, Brushes.Black, 0, yPos + 100, flags);
                
                err = "04";
                
                e.Graphics.DrawString("V/S _______", fEdit, Brushes.Black, 5, yPos + 180, flags);
                e.Graphics.DrawString("T _______'C   BP______mmHg", fEdit, Brushes.Black, 5, yPos + 200, flags);
                e.Graphics.DrawString("P______/mm   R______/mm", fEdit, Brushes.Black, 5, yPos + 220, flags);
                e.Graphics.DrawString("BW _______kgs.   HT______cms", fEdit, Brushes.Black, 5, yPos + 240, flags);
                err = "05";
                e.Graphics.DrawString("ประวัติการแพ้ยา", fEdit2, Brushes.Black, 5, yPos + 260, flags);
                e.Graphics.DrawString("ประวัติโรคประจำตัว", fEdit2, Brushes.Black, 5, yPos + 280, flags);
                e.Graphics.DrawString("ขอใบรับรองแพทย์", fEdit, Brushes.Black, 5, yPos + 300, flags);
                e.Graphics.DrawString("___ไม่ขอ  ___ประกอบเบิก", fEdit, Brushes.Black, 5, yPos + 320, flags);
                e.Graphics.DrawString("___หยุดงาน  ___รับการตรวจจริง", fEdit, Brushes.Black, 5, yPos + 340, flags);
                err = "06";
                line = "print time  " + date;
                //textSize = TextRenderer.MeasureText(line, famtB, proposedSize, TextFormatFlags.Left);
                //e.Graphics.DrawString(line, famtB, Brushes.Black, 15, yPos + 185, flags);
                e.Graphics.DrawString(line, fEdit, Brushes.Black, 10, yPos + 360, flags);
            }
            catch(Exception ex)
            {
                lfSbStatus.Text = err+ " Document_PrintPageQue";
                lfSbMessage.Text =  ex.Message;
                new LogWriter("e", "FrmReception Document_PrintPageQue " + ex.Message);
                bc.bcDB.insertLogPage(bc.userId, this.Name, "Document_PrintPageQue", ex.Message);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // ...
            if (keyData == (Keys.Escape))
            {
                //appExit();
                if (MessageBox.Show("ต้องการออกจากโปรแกรม1", "ออกจากโปรแกรม", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    Close();                    return true;
                }
            }
            else if (keyData == (Keys.F1))
            {//ค้นหา
                tC.SelectedTab = tabSrc;
            }
            else if (keyData == (Keys.F3))
            {//คนไข้ใหม่
                tC.SelectedTab = tabPtt;
                //setControlPatientNew();   //+1 comment ไปเพราะ F3 ต้องการให้กลับไปหน้า patient
            }
            else if (keyData == (Keys.F7))
            {//ดูนัด
                tC.SelectedTab = tabApm;
            }
            else if (keyData == (Keys.F8))
            {//ส่งตัว ออกvisit
                setTabVisit();
                tC.SelectedTab = tabVs;
            }
            else
            {
                switch (keyData)
                {
                    case Keys.K | Keys.Control:
                        
                        return true;
                    case Keys.X | Keys.Control:
                        
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void FrmReception_Load(object sender, EventArgs e)
        {
            this.Text = "Update 2568-11-05  com_cd";
            tC.SelectedTab = tabSrc;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            CboTheme_SelectedIndexChanged(null, null);
            sCPtt.HeaderHeight = 0;
            scVs.HeaderHeight = 0;
            spSSO.HeaderHeight = 0;
            
            txtSrcHn.Focus();
            rgSbModule.Text = bc.iniC.hostDBMainHIS + " " + bc.iniC.nameDBMainHIS;
            rbSbPb.Visible = false;
            //groupBox5.Top = this.Height - groupBox5.Height;

            AutoCompleteStringCollection autoTambon = new AutoCompleteStringCollection();
            autoTambon = bc.bcDB.pm07DB.setAutoCompTumbonName_getlTambonAll();
            txtPttIdSearchTambon.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPttIdSearchTambon.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPttIdSearchTambon.AutoCompleteCustomSource = autoTambon;
            txtPttCurSearchTambon.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPttCurSearchTambon.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPttCurSearchTambon.AutoCompleteCustomSource = autoTambon;
            txtPttRefSearchTambon.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPttRefSearchTambon.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPttRefSearchTambon.AutoCompleteCustomSource = autoTambon;

            AutoCompleteStringCollection autoPaid = new AutoCompleteStringCollection();
            autoPaid = bc.bcDB.finM02DB.getlPaid();
            txtVsPaidCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtVsPaidCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtVsPaidCode.AutoCompleteCustomSource = autoPaid;
            txtPttPaid.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPttPaid.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPttPaid.AutoCompleteCustomSource = autoPaid;

            autoInsur = new AutoCompleteStringCollection();
            autoInsur = bc.bcDB.pm24DB.setAutoInsur(false);
            txtVsInsur.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtVsInsur.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtVsInsur.AutoCompleteCustomSource = autoInsur;

            txtPttInsur.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPttInsur.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPttInsur.AutoCompleteCustomSource = autoInsur;

            autoComp = new AutoCompleteStringCollection();
            autoComp = bc.bcDB.pm24DB.getlPaid1(false);
            txtVsComp.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtVsComp.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtVsComp.AutoCompleteCustomSource = autoComp;
            txtPttCompCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPttCompCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPttCompCode.AutoCompleteCustomSource = autoComp; //txtPttInsur 

            groupBox5.Height = 390;
            pnPttComp.Height = 220;
            pnPttPatient.SizeRatio = 50;
            rgSbUser.Text = bc.user.fullname;
            rbDateSearch.Value = DateTime.Now;
            groupBox3.Height = 53;
            cboTheme.Hide();
        }
    }
}
