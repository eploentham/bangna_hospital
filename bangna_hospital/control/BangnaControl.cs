using AForge.Video.DirectShow;
using bangna_hospital.gui;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using bangna_hospital.utility;
using C1.C1Excel;
using C1.C1Zip;
using C1.Win.C1Document;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SplitContainer;
using C1.Win.C1Themes;
using C1.Win.FlexViewer;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.Net;


//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using CrystalDecisions.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace bangna_hospital.control
{
    public class BangnaControl
    {
        public InitConfig iniC;
        private IniFile iniF;
        public ConnectDB conn;
        public System.Drawing.Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt2, famt2B, famt4B, famt2BL, famt5, famt5B, famt5BL, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2;
        public String theme = "", userId = "", hn = "", vn = "", preno = "", appName = "", vsdate = "", operative_note_precidures_1, operative_note_finding_1, operative_note_hn, operative_note_id, USERCONFIRMID = "";
        public String COMPNAME = "";
        public Color cTxtFocus;
        public Staff user;
        public Staff sStf, cStf;
        public int imggridscanwidth = 0, pdfFontSize = 0, pdfFontSizetitleFont = 0, pdfFontSizetxtFont = 0, pdfFontSizehdrFont = 0, pdfFontSizetxtFontB = 0, queFontSize = 0, padYCertMed = 0;
        public int grdViewFontSize = 0, grdQueFontSize = 0, grdQueTodayFontSize = 0, timerImgScanNew = 0, printerQueueFontSize = 0, grfRowHeight = 30, staffNoteFontSize = 0;

        public BangnaHospitalDB bcDB;

        public Patient sPtt;
        public Boolean ftpUsePassive = false, ftpUsePassiveLabOut = false, EnableSsl = false;
        public int grfScanWidth = 0, imgScanWidth = 0, txtSearchHnLenghtStart = 0, timerCheckLabOut = 0, tabLabOutImageHeight = 0, tabLabOutImageWidth = 0, grfImgWidth = 0, scVssizeradio = 0, imageCC_width = 0, imageME_width = 0, imageDiag_width = 0, imageCC_Height = 0, imageME_Height = 0, imageDiag_Height = 0;
        public String[] preoperation, postoperation, operation, fining, procidures;
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        public Dictionary<String, String> opBKKINSCL = new Dictionary<String, String>() { { "UCS", "สิทธิหลักประกันสุขภาพ" }, { "WEL", "สิทธิหลักประกันสุขภาพ (ยกเว้นการร่วมจ่าย)" }, { "OFC", "สิทธิข้าราชการ" }, { "SSS", "สิทธิประกันสังคม" }, { "LGO", "สิทธิ อปท" }, { "SSI", "สิทธิประกันสังคมทุพพลภาพ" } };
        public Dictionary<String, String> opBKKClinic = new Dictionary<String, String>();
        public Dictionary<String, String> opBKKCHRGITEM_CODEA = new Dictionary<String, String>();
        public Dictionary<String, String> ssopClaimCat = new Dictionary<String, String>() { { "OP1", "OPD ปกติ" }, { "OP...", "OPD อื่นๆ" }, { "RPT", "ไตวายเรื้อรัง" }, { "P01", "OCPA" }, { "P02", "RDPA" }, { "P03", "DDPA" }, { "REF", "ส่งต่อ" }, { "EM1", "ฉุกเฉิน" }, { "EM2", "ฉุกเฉินระยะทาง" }, { "OPF", "เบิกเพิ่มแบบเหมาจ่าย" }, { "OPR", "เบิกเพิ่มตามอัตรา" }, { "XX...", "บัญชี...ต่างๆ" } };
        public String _IPAddress = "";
        public List<Nation> lNat;
        public List<Province> lProv;
        public List<District> lDistrict;
        public List<SubDistrict> lSubDistrict;

        public EpidemPersonStatus epiPersS;
        public EpidemPersonType epiPersT;
        public EpidemMarital epiMari;
        public EpidemProvince epiProv;
        public EpidemNationality epiNati;
        public EpidemPrefixType epiPref;
        public EpidemTmlt epiTmlt;
        public EpidemVaccManu epiVaccManu;
        public EpidemGender epiGender;
        public EpidemSymptomType epiSympT;
        public EpidemAccommodation epiAccomT;
        public EpidemPersonRick epiPersR;
        public EpidemLabComfirmType epiLabConT;

        public EpidemCovidReasonType epiReasT;
        public EpidemCovidSpcmPlace epiSpcmP;
        public EpidemCovidIsolatePlace epiIsoP;
        public EpidemCluster epiClus;
        public List<Item> items;
        public C1ThemeController theme1;

        public FileTypeDetectorPDF chkPDF;

        Hashtable _styles;
        public VideoCaptureDevice video;
        
        public BangnaControl()
        {
            initConfig();
        }
        private void initConfig()
        {
            //MessageBox.Show("h1111n ", "");
            String err = "";
            try
            {
                err = "00";
                appName = System.AppDomain.CurrentDomain.FriendlyName;
                appName = appName.ToLower().Replace(".exe", "");
                if (System.IO.File.Exists(Environment.CurrentDirectory + "\\" + appName + ".ini"))
                {
                    appName = Environment.CurrentDirectory + "\\" + appName + ".ini";
                }
                else
                {
                    appName = Environment.CurrentDirectory + "\\" + Application.ProductName + ".ini";
                }
                if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("bangna_hospital_scan_capture"))
                {
                    appName = "c:\\bangna_hospital.ini";
                }
                //MessageBox.Show("h1111n ", "");
                err = "01";
                iniF = new IniFile(appName);
                iniC = new InitConfig();
                cTxtFocus = ColorTranslator.FromHtml(iniC.txtFocus);
                user = new Staff();
                sPtt = new Patient();
                sStf = new Staff();
                cStf = new Staff();
                lNat = new List<Nation>();
                lProv = new List<Province>();
                lDistrict = new List<District>();
                lSubDistrict = new List<SubDistrict>();
                err = "02";
                //new LogWriter("d", "BangnaControl initConfig GetConfig in " + err);
                GetConfig();
                //new LogWriter("d", "BangnaControl initConfig GetConfig out " + err);
                err = "03";
                conn = new ConnectDB(iniC);
                //new LogWriter("d", "BangnaControl initConfig new ConnectDB(iniC); out " + err);
                err = "04";
                bcDB = new BangnaHospitalDB(conn);
                //new LogWriter("d", "BangnaControl initConfig new BangnaHospitalDB(conn); out " + err);
                err = "05";
                initOPBKKClinic();
                //new LogWriter("d", "BangnaControl initConfig initOPBKKClinic(); out " + err);
                err = "06";
                initOPBKKCHRGITEM();
                //new LogWriter("d", "BangnaControl initConfig initOPBKKCHRGITEM(); out " + err);
                getInit();
                InitFonts();
                epiPersS = new EpidemPersonStatus();
                epiPersT = new EpidemPersonType();
                epiMari = new EpidemMarital();
                epiProv = new EpidemProvince();
                epiNati = new EpidemNationality();
                epiPref = new EpidemPrefixType();
                epiTmlt = new EpidemTmlt();
                epiVaccManu = new EpidemVaccManu();
                epiGender = new EpidemGender();
                epiSympT = new EpidemSymptomType();
                epiAccomT = new EpidemAccommodation();
                epiPersR = new EpidemPersonRick();
                epiLabConT = new EpidemLabComfirmType();
                epiReasT = new EpidemCovidReasonType();
                epiSpcmP = new EpidemCovidSpcmPlace();
                epiIsoP = new EpidemCovidIsolatePlace();
                epiClus = new EpidemCluster();

                theme1 = new C1ThemeController();
                theme1.Theme = iniC.themeApplication;
                chkPDF = new FileTypeDetectorPDF();
            }
            catch (Exception ex)
            {
                new LogWriter("e", "BangnaControl initConfig err " + err + " " + ex.Message);
                MessageBox.Show("error " + ex.Message + " err " + err, "");
            }
        }
        public void cloneComboBox(C1ComboBox c, ref C1ComboBox d)
        {
            d.Items.Clear();
            foreach (ComboBoxItem item in c.Items)
            {
                d.Items.Add(item);
            }
            d.SelectedIndex = c.SelectedIndex;
        }
        public void cloneComboBox(ComboBox c, ref ComboBox d)
        {
            d.Items.Clear();
            foreach (var item in c.Items)
            {
                d.Items.Add(item);
            }
            d.SelectedIndex = c.SelectedIndex;
        }
        public void getInit()
        {
            //bcDB.sexDB.getlSex();
            //cop = ivfDB.copDB.selectByCode1("001");
            _IPAddress = GetLocalIPAddress();
            conn._IPAddress = _IPAddress;
        }
        public String calBMI(String weight, String high)
        {
            String re = "";
            try
            {
                float bmi = 0;                float.TryParse(weight, out float wei);                float.TryParse(high, out float high1);
                if (high1 > 0)                {                    bmi = wei / ((high1 / 100) * (high1 / 100));                }
                else                {                    bmi = 0;                }
                re = bmi.ToString("##.##");
            }
            catch (Exception ex)            {            }
            return re;
        }
        public String calBMI(String weight, String high,ref Color backcolor,ref Color forecolor)
        {
            String re = "";
            try
            {
                re = calBMI(weight, high);
                //สีฟ้า / น้ำเงินอ่อน - น้ำหนักน้อย / ผอม(BMI < 18.5)   #3B82F6     Soft    93C5FD 
                //สีเขียว - น้ำหนักปกติ / ดี(BMI 18.5 - 22.9)         #10B981         6EE7B7 
                //สีเหลือง - น้ำหนักเกิน(BMI 23.0 - 24.9)            #F59E0B         FCD34D 
                //สีส้ม - อ้วนระดับ 1(BMI 25.0 - 29.9)              #F97316         FDBA74 
                //สีแดง - อ้วนระดับ 2 ขึ้นไป(BMI ≥ 30.0)             #EF4444         #FCA5A5 
                if (float.Parse(re) < 18.5) { backcolor = ColorTranslator.FromHtml("#3B82F6"); }
                else if ((float.Parse(re) >= 18.5) && (float.Parse(re) <= 22.9)) { backcolor = ColorTranslator.FromHtml("#10B981"); }
                else if ((float.Parse(re) > 22.9) && (float.Parse(re) <= 24.9)) { backcolor = ColorTranslator.FromHtml("#F59E0B"); }
                else if ((float.Parse(re) > 24.9) && (float.Parse(re) <= 29.9)) { backcolor = ColorTranslator.FromHtml("#FDBA74"); }
                else if (float.Parse(re) > 29.9) { backcolor = ColorTranslator.FromHtml("#FCA5A5"); }
                else { }
            }
            catch (Exception ex)
            {

            }
            return re;
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public C1ComboBox setCboSubDistrict(C1ComboBox c, String districtcode)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            //DataTable dt = selectC1();
            //lSubDistrict.Clear();
            getlSubDistrict(districtcode);
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "00";
            c.Items.Clear();
            c.Items.Add(item1);
            //for (int i = 0; i < dt.Rows.Count; i++)
            int i = 0;
            foreach (SubDistrict row in lSubDistrict)
            {
                item = new ComboBoxItem();
                item.Value = row.subdistrict_code;
                item.Text = row.subdistrict_name;
                c.Items.Add(item);
                i++;
            }
            c.SelectedText = select;
            c.SelectedIndex = row1;
            return c;
        }
        public void getlSubDistrict(String provcode)
        {
            //lDept = new List<Position>();
            lSubDistrict.Clear();
            DataTable dt = new DataTable();
            dt = selectSubDistrictByDistrictCode(provcode);
            foreach (DataRow row in dt.Rows)
            {
                SubDistrict dept1 = new SubDistrict();
                dept1.subdistrict_code = row["MNC_TUM_CD"].ToString();
                dept1.district_code = row["MNC_AMP_CD"].ToString();
                dept1.prov_code = row["MNC_CHW_CD"].ToString();
                dept1.subdistrict_name = row["MNC_TUM_DSC"].ToString();
                lSubDistrict.Add(dept1);
            }
        }
        public DataTable selectSubDistrictByDistrictCode(String districtcode)
        {
            DataTable dt = new DataTable();
            String sql = "select dept.*  " +
                "From patient_m07 dept " +
                "Where MNC_AMP_CD  = '" + districtcode + "'";
            //dt = conn.selectData(conn.conn, sql);
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {

            }
            return dt;
        }
        public C1ComboBox setCboDistrict(C1ComboBox c, String provcode)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            //DataTable dt = selectC1();
            //lDistrict.Clear();
            getlDistrict(provcode);
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "00";
            c.Items.Clear();
            c.Items.Add(item1);
            //for (int i = 0; i < dt.Rows.Count; i++)
            int i = 0;
            foreach (District row in lDistrict)
            {
                item = new ComboBoxItem();
                item.Value = row.district_code;
                item.Text = row.district_name;
                c.Items.Add(item);
                i++;
            }
            c.SelectedText = select;
            c.SelectedIndex = row1;
            return c;
        }
        public String fixLabUnit(String labcode, String labsubname, String unitold, String refold)
        {
            String unit = "";

            unit = unitold.Replace("0.00-0.00", "").Replace("0.00 - 0.00", "").Replace("0.00", "");

            unit = refold.Equals(unitold) ? "" : unitold;
            if (labcode.Equals("HE001") && labsubname.Equals("WBC  count")) unit = "cell/µL"; //CBC   WBC count 
            else if (labcode.Equals("HE001") && labsubname.Equals("RBC  count")) unit = "10^6/µL"; //CBC   RBC count 
            else if (labcode.Equals("HE001") && labsubname.Equals("Atp.Lymp")) unit = "cell/µL"; //CBC   Atp.Lymp
            else if (labcode.Equals("HE001") && labsubname.Equals("PLT. count")) unit = "cell/µL"; //Influenza A&B+COVID-19 Ag Combo   
            else if (labcode.Equals("SE001") && labsubname.Equals("VDRL")) unit = "";
            else if (labcode.Equals("SE038") && labsubname.Equals("HBsAg")) unit = "";
            else if (labcode.Equals("SE670") && labsubname.Equals("Influenza B")) unit = "";
            else if (labcode.Equals("SE670") && labsubname.Equals("Influenza A")) unit = "";
            else if (labcode.Equals("SE670") && labsubname.Equals("COVID-19 Antigen test")) unit = "";
            else if (labcode.Equals("MS100") && labsubname.Equals("Pregnancy test (Citest strip),UPT")) unit = "";
            else if (labcode.Equals("SE640") && labsubname.Equals("COVID-19 Antigen test")) unit = "";
            //else if (labcode.Equals("MS022") && labsubname.Equals("Pregnancy test (Citest strip)")) unit = "NEGATIVE";
            //else if (labcode.Equals("SE005") && labsubname.Equals("Anti HIV")) unit = "NEGATIVE";
            //else if (labcode.Equals("SE001") && labsubname.Equals("VDRL")) unit = "NON REACTIVE";
            else if (labcode.Equals("MS001") && labsubname.Equals("Renal Epi")) unit = "Cell/HPF";
            else if (labcode.Equals("CH035") && labsubname.Equals("HB A1C  (Glycosylated Hb)")) unit = "%";
            else if (labcode.Equals("MS043") && labsubname.Equals("WBC")) unit = "Cell/HPF";
            else if (labcode.Equals("MS043") && labsubname.Equals("RBC")) unit = "Cell/HPF";
            else if (labcode.Equals("CH008") && labsubname.Equals("HDL-C")) unit = "mg/dL";
            else if (labcode.Equals("MS001") && labsubname.Equals("WBC")) unit = "Cell/HPF";
            else if (labcode.Equals("MS001") && labsubname.Equals("RBC")) unit = "Cell/HPF";
            else if (labcode.Equals("MS001") && labsubname.Equals("RBC")) unit = "Cell/HPF";
            else if (labcode.Equals("MS001") && labsubname.Equals("Epith. Squamous")) unit = "Cell/HPF";
            else if (labcode.Equals("MS001") && labsubname.Equals("Crystal")) unit = "Cell/HPF";
            else if (labcode.Equals("MS001") && labsubname.Equals("Cast")) unit = "/LPF";
            else if (labcode.Equals("MS001") && labsubname.Equals("Bladder cell")) unit = "Cell/HPF";
            return unit;
        }
        public String fixLabRef(String labcode, String labsubname, String resunit, String refold)
        {
            String reref = "", reref1 = "";
            reref1 = resunit.Replace(refold, "").Replace("[", "").Replace("]", "").Trim();
            if (labcode.Equals("HE001") && labsubname.Equals("RBC  morpho.")) reref = "Normal";       //CBC   RBC  morpho.
            else if (labcode.Equals("HE001") && labsubname.Equals("Atp.Lymp")) reref = "";
            else if (labcode.Equals("HE001") && labsubname.Equals("PLT . smear")) reref = "Adequate";
            else if (labcode.Equals("MB014") && labsubname.Equals("AFB STAIN")) reref = "NEGATIVE";//AFB STAIN   ( Second Time) 
            else if (labcode.Equals("MB014") && labsubname.Equals("02")) reref = "NEGATIVE";//AFB STAIN   ( Second Time) 
            else if (labcode.Equals("MS001") && labsubname.Equals("Renal Epi")) reref = "";
            else if (labcode.Equals("MS001") && labsubname.Equals("Yeast")) reref = "";
            else if (labcode.Equals("MS001") && labsubname.Equals("WBC")) reref = "";
            else if (labcode.Equals("MS001") && labsubname.Equals("RBC")) reref = "";
            else if (labcode.Equals("MS001") && labsubname.Equals("Epith. Squamous")) reref = "";
            else if (labcode.Equals("MS001") && labsubname.Equals("Crystal")) reref = "";
            else if (labcode.Equals("MS001") && labsubname.Equals("Cast")) reref = "";
            else if (labcode.Equals("MS001") && labsubname.Equals("Bladder cell")) reref = "";//SE640

            else if (labcode.Equals("MS022") && labsubname.Equals("Pregnancy test (Citest strip)")) reref = "NEGATIVE";
            else if (labcode.Equals("MS043") && labsubname.Equals("RBC")) reref = "";
            else if (labcode.Equals("MS043") && labsubname.Equals("Ova & Parasite")) reref = "Not found";
            else if (labcode.Equals("MS043") && labsubname.Equals("WBC")) reref = "";
            else if (labcode.Equals("MS100") && labsubname.Equals("Pregnancy test (Citest strip),UPT")) reref = "NEGATIVE";

            else if (labcode.Equals("SE001") && labsubname.Equals("VDRL")) reref = "NON REACTIVE";
            else if (labcode.Equals("SE005") && labsubname.Equals("Anti HIV")) reref = "NEGATIVE";
            else if (labcode.Equals("SE005") && labsubname.Equals("Sen100% Spec99.72%")) reref = "";
            else if (labcode.Equals("SE005") && labsubname.Equals("Sen100% Spec99.96%")) reref = "";
            else if (labcode.Equals("SE005") && labsubname.Equals("Sen100% Spec99.40%")) reref = "";
            else if (labcode.Equals("SE038") && labsubname.Equals("HBsAg")) reref = "NEGATIVE";
            else if (labcode.Equals("SE047") && labsubname.Equals("Anti HCV")) reref = "NEGATIVE";
            else if (labcode.Equals("SE502") && labsubname.Equals("VDRL(Check up)")) reref = "NON REACTIVE";
            else if (labcode.Equals("SE640") && labsubname.Equals("COVID-19 Antigen test")) reref = "NEGATIVE";
            else if (labcode.Equals("SE670") && labsubname.Equals("Influenza A")) reref = "NEGATIVE"; //Influenza A&B+COVID-19 Ag Combo   
            else if (labcode.Equals("SE670") && labsubname.Equals("Influenza B")) reref = "NEGATIVE";
            else if (labcode.Equals("SE670") && labsubname.Equals("COVID-19 Antigen test")) reref = "NEGATIVE";

            else if (labcode.Equals("TG048") && labsubname.Equals("Methamphetamine test")) reref = "NEGATIVE";
            else if (labcode.Equals("TG048") && labsubname.Equals("Marijuana (URINE)")) reref = "NEGATIVE";
            else if (labcode.Equals("CH008") && labsubname.Equals("HDL-C")) reref = "Male > 35;Female > 44";

            else if (labcode.Equals("PA530") && labsubname.Equals("HPV genotype 16")) reref = "NEGATIVE";//PA530
            else if (labcode.Equals("PA530") && labsubname.Equals("HPV genotype 18")) reref = "NEGATIVE";//PA530
            else if (labcode.Equals("PA530") && labsubname.Equals("13 Other HPV -DNA Type")) reref = "NEGATIVE";//PA530
            else reref = reref1;
            return reref;
        }
        public String getPaidShortName(String paidname)
        {
            if (paidname.Equals("ประกันสังคม (บ.1)")) return "ปกส(บ.1)";
            else if (paidname.Equals("ประกันสังคม (บ.2)")) return "ปกส(บ.2)";
            else if (paidname.Equals("ประกันสังคม (บ.5)")) return "ปกส(บ.5)";
            else if (paidname.Equals("ประกันสังคมอิสระ (บ.1)")) return "ปกต(บ.1)";
            else if (paidname.Equals("ประกันสังคมอิสระ (บ.2)")) return "ปกต(บ.2)";
            else if (paidname.Equals("ประกันสังคมอิสระ (บ.5)")) return "ปกต(บ.5)";
            else if (paidname.Equals("ตรวจสุขภาพ (เงินสด)")) return "ตส(เงินสด)";
            else if (paidname.Equals("ตรวจสุขภาพ (บริษัท)")) return "ตส(บริษัท)";
            else if (paidname.Equals("ตรวจสุขภาพ (PACKAGE)")) return "ตส(PACKAGE)";
            else if (paidname.Equals("ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ")) return "ลูกหนี้(ปากน้ำ)";
            else if (paidname.Equals("ลูกหนี้บางนา 1")) return "ลูกหนี้(บ.1)";
            else if (paidname.Equals("บริษัทประกัน")) return "บ.ประกัน";
            else if (paidname.Equals("ลูกหนี้ UC บางนา 1")) return "UCบ.1";
            else if (paidname.Equals("ลูกหนี้ UC โครงการสวนหัวใจ")) return "UC(สวนหัวใจ)";
            else if (paidname.Equals("ลูกหนี้บางนา 2")) return "ลูกหนี้(บ.1)";
            else if (paidname.Equals("ลูกหนี้ UC โครงการต้อกระจก")) return "UC(ต้อกระจก)";
            else if (paidname.Equals("ลูกหนี้ตรวจสุขภาพประกันสังคม")) return "ลูกหนี้(ปกส)";
            else return paidname;
        }
        public void getlDistrict(String provcode)
        {
            //lDept = new List<Position>();
            lDistrict.Clear();
            DataTable dt = new DataTable();
            dt = selectDistrictByProvCode(provcode);
            foreach (DataRow row in dt.Rows)
            {
                District dept1 = new District();
                dept1.district_code = row["MNC_AMP_CD"].ToString();
                dept1.prov_code = row["MNC_CHW_CD"].ToString();
                dept1.district_name = row["MNC_AMP_DSC"].ToString();
                lDistrict.Add(dept1);
            }
        }
        public DataTable selectDistrictByProvCode(String provcode)
        {
            DataTable dt = new DataTable();
            String sql = "select dept.*  " +
                "From patient_m08 dept " +
                "Where MNC_CHW_CD  = '" + provcode + "'";
            //dt = conn.selectData(conn.conn, sql);
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {

            }
            return dt;
        }
        public C1ComboBox setCboProvince(C1ComboBox c)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            //DataTable dt = selectC1();
            if (lProv.Count <= 0) getlProvince();
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "00";
            c.Items.Clear();
            c.Items.Add(item1);
            //for (int i = 0; i < dt.Rows.Count; i++)
            int i = 0;
            foreach (Province row in lProv)
            {
                item = new ComboBoxItem();
                item.Value = row.prov_code;
                item.Text = row.prov_name;
                c.Items.Add(item);
                i++;
            }
            c.SelectedText = select;
            c.SelectedIndex = row1;
            return c;
        }
        public void getlProvince()
        {
            //lDept = new List<Position>();

            lProv.Clear();
            DataTable dt = new DataTable();
            dt = selectProvinceAll();
            foreach (DataRow row in dt.Rows)
            {
                Province dept1 = new Province();
                dept1.prov_code = row["MNC_CHW_CD"].ToString();
                dept1.prov_name = row["MNC_CHW_DSC"].ToString();
                lProv.Add(dept1);
            }
        }
        public DataTable selectProvinceAll()
        {
            DataTable dt = new DataTable();
            String sql = "select dept.*  " +
                "From patient_m09 dept ";
            //dt = conn.selectData(conn.conn, sql);
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {

            }
            return dt;
        }
        public C1ComboBox setCboAlienPosition(C1ComboBox c)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "";
            c.Items.Clear();
            c.Items.Add(item1);

            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "กรรมกร";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "2";
            item.Text = "ผู้รับใช้ในบ้าน";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "3";
            item.Text = "ช่างเครื่องยนต์ในเรือประมงทะเล";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "4";
            item.Text = "ผู้ประสานงานด้านภาษากัมพูชา ลาว เมียนมา หรือเวียดนาม";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "5";
            item.Text = "งานขายของหน้าร้าน";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "6";
            item.Text = "งานกสิกรรม";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "7";
            item.Text = "งานเลี้ยงสัตว์";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "8";
            item.Text = "งานป่าไม้";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "9";
            item.Text = "งานประมง";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "10";
            item.Text = "งานช่างก่ออิฐ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "11";
            item.Text = "งานช่างไม้";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "12";
            item.Text = "งานช่างก่อสร้างอาคาร";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "13";
            item.Text = "งานทำที่นอนหรือผ้าห่มนวม";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "14";
            item.Text = "งานทำมีด";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "15";
            item.Text = "งานทำรองเท้า";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "16";
            item.Text = "งานทำหมวก";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "17";
            item.Text = "งานประดิษฐ์เครื่องแต่งกาย";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "18";
            item.Text = "งานปั้นหรือทำเครื่องปั้นดินเผา";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "19";
            item.Text = "กิจการก่อสร้าง";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "20";
            item.Text = "จำหน่ายอาหารและเครื่องดื่ม";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "21";
            item.Text = "BT24 - การให้บริการต่างๆ";
            c.Items.Add(item);//
            item = new ComboBoxItem();
            item.Value = "22";
            item.Text = "ผลิตหรือจำหน่ายวัสดุก่อสร้าง";
            c.Items.Add(item);
            return c;
        }
        public C1ComboBox setCboVisitType(C1ComboBox c)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "";
            c.Items.Clear();
            c.Items.Add(item1);

            item = new ComboBoxItem();
            item.Value = "N";
            item.Text = "ตรวจปกติ Normal";
            c.Items.Add(item);
            c.SelectedItem = item;

            item = new ComboBoxItem();
            item.Value = "F";
            item.Text = "ตรวจต่อเนื่อง Follow Up";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "S";
            item.Text = "มารับบริการ General";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "R";
            item.Text = "รับรักษาต่อ Refer";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "P";
            item.Text = "ตรวจเหมาจ่ายต่างๆ Package";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "U";
            item.Text = "ตรวจสุขภาพบริษัท Check-Up";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "A";
            item.Text = "แพทย์นัด Appionment";
            c.Items.Add(item);

            return c;
        }
        public C1ComboBox setCboAlienCountry(C1ComboBox c)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "";
            c.Items.Clear();
            c.Items.Add(item1);

            item = new ComboBoxItem();
            item.Value = "M";
            item.Text = "เมียนมา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "M";
            item.Text = "พม่า";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "L";
            item.Text = "ลาว";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "C";
            item.Text = " กัมพูชา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "V";
            item.Text = "เวียดนาม";
            c.Items.Add(item);

            return c;
        }
        public C1ComboBox setCboNormal(C1ComboBox c)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "";
            c.Items.Clear();
            c.Items.Add(item1);

            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "Normal";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "0";
            item.Text = "AbNormal";
            c.Items.Add(item);

            return c;
        }
        public C1ComboBox setCboTokenType(C1ComboBox c)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "";
            c.Items.Clear();
            c.Items.Add(item1);

            item = new ComboBoxItem();
            item.Value = "cert_alien";
            item.Text = "ใบรับรองแพทย์ต่างด้าว";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "cert";
            item.Text = "ใบรับรองแพทย์";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "doctor_order";
            item.Text = "ใบสั่งยา";
            c.Items.Add(item);
            return c;
        }
        public C1ComboBox setCboSkintone(C1ComboBox c)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "";
            c.Items.Clear();
            c.Items.Add(item1);

            item = new ComboBoxItem();
            item.Value = "white1";
            item.Text = "ขาวที่สุด";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "white";
            item.Text = "ขาว";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "black";
            item.Text = "ดำ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "brown";
            item.Text = "น้ำตาล";

            item = new ComboBoxItem();
            item.Value = "brown1";
            item.Text = "น้ำตาลเข้ม";

            item = new ComboBoxItem();
            item.Value = "brown2";
            item.Text = "เหลือง";

            item = new ComboBoxItem();
            item.Value = "brown3";
            item.Text = "ขาวอมชมพู";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "brown4";
            item.Text = "ขาว-เหลือง";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "brown5";
            item.Text = "ผิวสองสี";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "brown6";
            item.Text = "ผิวเข้มที่สุด";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "brown7";
            item.Text = "ดำ-แดง";
            c.Items.Add(item);

            return c;
        }
        public C1ComboBox setCboChckUP(C1ComboBox c)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "";
            c.Items.Clear();
            c.Items.Add(item1);

            item = new ComboBoxItem();
            item.Value = "sso";
            item.Text = "ตรวจสุขภาพประกันสังคม";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "doe";
            item.Text = "ตรวจสุขภาพต่างด้าว";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "doeonline";
            item.Text = "ตรวจสุขภาพต่างด้าว On line";
            c.Items.Add(item);

            return c;
        }
        public C1ComboBox setCboSex(C1ComboBox c)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "";
            c.Items.Clear();
            c.Items.Add(item1);

            item = new ComboBoxItem();
            item.Value = "M";
            item.Text = "M";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "F";
            item.Text = "F";
            c.Items.Add(item);

            return c;
        }
        public C1ComboBox setCboNation(C1ComboBox c)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            //DataTable dt = selectC1();
            if (lNat.Count <= 0) getlNation();
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "00";
            c.Items.Clear();
            c.Items.Add(item1);
            //for (int i = 0; i < dt.Rows.Count; i++)
            int i = 0;
            foreach (Nation row in lNat)
            {
                item = new ComboBoxItem();
                item.Value = row.nat_id;
                item.Text = row.nat_name;
                c.Items.Add(item);
                i++;
            }
            c.SelectedText = select;
            c.SelectedIndex = row1;
            return c;
        }
        public void getlNation()
        {
            //lDept = new List<Position>();

            lNat.Clear();
            DataTable dt = new DataTable();
            dt = selectNationAll();
            foreach (DataRow row in dt.Rows)
            {
                Nation dept1 = new Nation();
                dept1.nat_id = row["mnc_nat_cd"].ToString();
                dept1.nat_name = row["MNC_NAT_DSC"].ToString();
                lNat.Add(dept1);
            }
        }
        public DataTable selectNationAll()
        {
            DataTable dt = new DataTable();
            String sql = "select dept.*  " +
                "From patient_m04 dept ";
            //dt = conn.selectData(conn.conn, sql);
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {

            }
            return dt;
        }
        public void GetConfig()
        {
            //MessageBox.Show("hn " , "");
            //new LogWriter("d", "BangnaControl initConfig connection  ");
            iniC.hostDB = iniF.getIni("connection", "hostDB");
            iniC.nameDB = iniF.getIni("connection", "nameDB");
            iniC.userDB = iniF.getIni("connection", "userDB");
            iniC.passDB = iniF.getIni("connection", "passDB");
            iniC.portDB = iniF.getIni("connection", "portDB");

            iniC.hostDBMainHIS = iniF.getIni("connection", "hostDBMainHIS");
            iniC.nameDBMainHIS = iniF.getIni("connection", "nameDBMainHIS");
            iniC.userDBMainHIS = iniF.getIni("connection", "userDBMainHIS");
            iniC.passDBMainHIS = iniF.getIni("connection", "passDBMainHIS");
            iniC.portDBMainHIS = iniF.getIni("connection", "portDBMainHIS");

            iniC.hostDBBACK = iniF.getIni("connection", "hostDBBACK");
            iniC.nameDBBACK = iniF.getIni("connection", "nameDBBACK");
            iniC.userDBBACK = iniF.getIni("connection", "userDBBACK");
            iniC.passDBBACK = iniF.getIni("connection", "passDBBACK");
            iniC.portDBBACK = iniF.getIni("connection", "portDBBACK");

            iniC.hostDBPACs = iniF.getIni("connection", "hostDBPACs");
            iniC.nameDBPACs = iniF.getIni("connection", "nameDBPACs");
            iniC.userDBPACs = iniF.getIni("connection", "userDBPACs");
            iniC.passDBPACs = iniF.getIni("connection", "passDBPACs");
            iniC.portDBPACs = iniF.getIni("connection", "portDBPACs");

            iniC.hostDBLabOut = iniF.getIni("connection", "hostDBLabOut");
            iniC.nameDBLabOut = iniF.getIni("connection", "nameDBLabOut");
            iniC.userDBLabOut = iniF.getIni("connection", "userDBLabOut");
            iniC.passDBLabOut = iniF.getIni("connection", "passDBLabOut");
            iniC.portDBLabOut = iniF.getIni("connection", "portDBLabOut");
            //new LogWriter("d", "BangnaControl initConfig connection hostDBOPBKK ");
            iniC.hostDBOPBKK = iniF.getIni("connection", "hostDBOPBKK");
            iniC.nameDBOPBKK = iniF.getIni("connection", "nameDBOPBKK");
            iniC.userDBOPBKK = iniF.getIni("connection", "userDBOPBKK");
            iniC.passDBOPBKK = iniF.getIni("connection", "passDBOPBKK");
            iniC.portDBOPBKK = iniF.getIni("connection", "portDBOPBKK");

            iniC.hostDBLogTask = iniF.getIni("connection", "hostDBLogTask");
            iniC.nameDBLogTask = iniF.getIni("connection", "nameDBLogTask");
            iniC.userDBLogTask = iniF.getIni("connection", "userDBLogTask");
            iniC.passDBLogTask = iniF.getIni("connection", "passDBLogTask");
            iniC.portDBLogTask = iniF.getIni("connection", "portDBLogTask");

            iniC.hostDBMySQL = iniF.getIni("connection", "hostDBMySQL");
            iniC.nameDBMySQL = iniF.getIni("connection", "nameDBMySQL");
            iniC.userDBMySQL = iniF.getIni("connection", "userDBMySQL");
            iniC.passDBMySQL = iniF.getIni("connection", "passDBMySQL");
            iniC.portDBMySQL = iniF.getIni("connection", "portDBMySQL");

            iniC.hostDBSsnData = iniF.getIni("connection", "hostDBSsnData");
            iniC.nameDBSsnData = iniF.getIni("connection", "nameDBSsnData");
            iniC.userDBSsnData = iniF.getIni("connection", "userDBSsnData");
            iniC.passDBSsnData = iniF.getIni("connection", "passDBSsnData");
            iniC.portDBSsnData = iniF.getIni("connection", "portDBSsnData");

            iniC.hostDBLinkLIS = iniF.getIni("connection", "hostDBLinkLIS");
            iniC.nameDBLinkLIS = iniF.getIni("connection", "nameDBLinkLIS");
            iniC.userDBLinkLIS = iniF.getIni("connection", "userDBLinkLIS");
            iniC.passDBLinkLIS = iniF.getIni("connection", "passDBLinkLIS");
            iniC.portDBLinkLIS = iniF.getIni("connection", "portDBLinkLIS");

            //new LogWriter("d", "BangnaControl initConfig ftp ");
            iniC.hostFTP = iniF.getIni("ftp", "hostFTP");
            iniC.userFTP = iniF.getIni("ftp", "userFTP");
            iniC.passFTP = iniF.getIni("ftp", "passFTP");
            iniC.portFTP = iniF.getIni("ftp", "portFTP");
            iniC.folderFTP = iniF.getIni("ftp", "folderFTP");
            iniC.usePassiveFTP = iniF.getIni("ftp", "usePassiveFTP");
            iniC.ProxyHost = iniF.getIni("ftp", "ProxyHost");
            iniC.ProxyPort = iniF.getIni("ftp", "ProxyPort");
            iniC.ProxyProxyType = iniF.getIni("ftp", "ProxyProxyType");
            iniC.EnableSsl = iniF.getIni("ftp", "EnableSsl");

            iniC.hostFTPLabOut = iniF.getIni("ftp", "hostFTPLabOut");
            iniC.userFTPLabOut = iniF.getIni("ftp", "userFTPLabOut");
            iniC.passFTPLabOut = iniF.getIni("ftp", "passFTPLabOut");
            iniC.portFTPLabOut = iniF.getIni("ftp", "portFTPLabOut");
            iniC.folderFTPLabOut = iniF.getIni("ftp", "folderFTPLabOut");
            iniC.usePassiveFTPLabOut = iniF.getIni("ftp", "usePassiveFTPLabOut");

            iniC.hostFTPLabOutMedica = iniF.getIni("ftp", "hostFTPLabOut_MADICA");
            iniC.userFTPLabOutMedica = iniF.getIni("ftp", "userFTPLabOut_MADICA");
            iniC.passFTPLabOutMedica = iniF.getIni("ftp", "passFTPLabOut_MADICA");
            iniC.portFTPLabOutMedica = iniF.getIni("ftp", "portFTPLabOut_MADICA");
            iniC.folderFTPLabOutMedica = iniF.getIni("ftp", "folderFTPLabOut_MADICA");
            iniC.usePassiveFTPLabOutMedica = iniF.getIni("ftp", "usePassiveFTPLabOut_MADICA");

            iniC.hostFTPCertMed = iniF.getIni("ftp", "hostFTPCertMed");
            iniC.userFTPCertMed = iniF.getIni("ftp", "userFTPCertMed");
            iniC.passFTPCertMed = iniF.getIni("ftp", "passFTPCertMed");
            iniC.portFTPCertMed = iniF.getIni("ftp", "portFTPCertMed");
            iniC.folderFTPCertMed = iniF.getIni("ftp", "folderFTPCertMed");
            iniC.usePassiveFTPCertMed = iniF.getIni("ftp", "usePassiveFTPCertMed");

            iniC.hostFTPDrugIn = iniF.getIni("ftp", "hostFTPDrugIn");
            iniC.userFTPDrugIn = iniF.getIni("ftp", "userFTPDrugIn");
            iniC.passFTPDrugIn = iniF.getIni("ftp", "passFTPDrugIn");
            iniC.portFTPDrugIn = iniF.getIni("ftp", "portFTPDrugIn");
            iniC.folderFTPDrugIn = iniF.getIni("ftp", "folderFTPDrugIn");
            iniC.usePassiveFTPDrugIn = iniF.getIni("ftp", "usePassiveFTPDrugIn");

            iniC.hostFTPbangnadoe = iniF.getIni("ftp", "hostFTPbangnadoe");
            iniC.userFTPbangnadoe = iniF.getIni("ftp", "userFTPbangnadoe");
            iniC.passFTPbangnadoe = iniF.getIni("ftp", "passFTPbangnadoe");
            iniC.portFTPbangnadoe = iniF.getIni("ftp", "portFTPbangnadoe");
            iniC.folderFTPbangnadoe = iniF.getIni("ftp", "folderFTPbangnadoe");
            iniC.usePassiveFTPbangnadoe = iniF.getIni("ftp", "usePassiveFTPbangnadoe");

            iniC.hostFTPCertMeddoe = iniF.getIni("ftp", "hostFTPCertMeddoe");
            iniC.userFTPCertMeddoe = iniF.getIni("ftp", "userFTPCertMeddoe");
            iniC.passFTPCertMeddoe = iniF.getIni("ftp", "passFTPCertMeddoe");
            iniC.portFTPCertMeddoe = iniF.getIni("ftp", "portFTPCertMeddoe");
            iniC.folderFTPCertMeddoe = iniF.getIni("ftp", "folderFTPCertMeddoe");
            iniC.usePassiveFTPCertMeddoe = iniF.getIni("ftp", "usePassiveFTPCertMeddoe");

            iniC.grdViewFontSize = iniF.getIni("app", "grdViewFontSize");
            iniC.grdViewFontName = iniF.getIni("app", "grdViewFontName");
            iniC.pdfFontSize = iniF.getIni("app", "pdfFontSize");
            iniC.pdfFontName = iniF.getIni("app", "pdfFontName");
            iniC.pdfFontSizetitleFont = iniF.getIni("app", "pdfFontSizetitleFont");
            iniC.pdfFontSizetxtFont = iniF.getIni("app", "pdfFontSizetxtFont");
            iniC.pdfFontSizehdrFont = iniF.getIni("app", "pdfFontSizehdrFont");
            iniC.pdfFontSizetxtFontB = iniF.getIni("app", "pdfFontSizetxtFontB");

            iniC.queFontName = iniF.getIni("app", "queFontName");
            iniC.queFontSize = iniF.getIni("app", "queFontSize");

            iniC.txtFocus = iniF.getIni("app", "txtFocus");
            iniC.grfRowColor = iniF.getIni("app", "grfRowColor");
            iniC.statusAppDonor = iniF.getIni("app", "statusAppDonor");
            iniC.themeApplication = iniF.getIni("app", "themeApplication");
            iniC.themeDonor = iniF.getIni("app", "themeDonor");
            iniC.printerSticker = iniF.getIni("app", "printerSticker");
            iniC.timerImgScanNew = iniF.getIni("app", "timerImgScanNew");
            iniC.pathImageScan = iniF.getIni("app", "pathImageScan");
            iniC.imggridscanwidth = iniF.getIni("app", "imggridscanwidth");
            iniC.hostname = iniF.getIni("app", "hostname");
            iniC.printerA4 = iniF.getIni("app", "printerA4");
            iniC.programLoad = iniF.getIni("app", "programLoad");
            iniC.labOutOpenFileDialog = iniF.getIni("app", "labOutOpenFileDialog");
            iniC.windows = iniF.getIni("app", "windows");
            iniC.grfScanWidth = iniF.getIni("app", "grfScanWidth");
            iniC.imgScanWidth = iniF.getIni("app", "imgScanWidth");
            iniC.pathScanStaffNote = iniF.getIni("app", "pathScanStaffNote");
            iniC.pathScreenCaptureUpload = iniF.getIni("app", "pathScreenCaptureUpload");
            iniC.pathIniFile = iniF.getIni("app", "pathIniFile");
            iniC.statusShowPrintDialog = iniF.getIni("app", "statusShowPrintDialog");
            iniC.txtSearchHnLenghtStart = iniF.getIni("app", "txtSearchHnLenghtStart");
            iniC.pathLabOutReceiveInnoTech = iniF.getIni("app", "pathLabOutReceiveInnoTech");
            iniC.pathLabOutBackupInnoTech = iniF.getIni("app", "pathLabOutBackupInnoTech");
            iniC.timerCheckLabOut = iniF.getIni("app", "timerCheckLabOut");
            iniC.pathTempScanAdd = iniF.getIni("app", "pathTempScanAdd");
            iniC.themeApp = iniF.getIni("app", "themeApp");
            iniC.station = iniF.getIni("app", "station");
            iniC.pathLabOutReceiveRIA = iniF.getIni("app", "pathLabOutReceiveRIA");
            iniC.pathLabOutBackupRIA = iniF.getIni("app", "pathLabOutBackupRIA");
            iniC.pathLabOutBackupRIAZipExtract = iniF.getIni("app", "pathLabOutBackupRIAZipExtract");
            iniC.pathLabOutBackupManual = iniF.getIni("app", "pathLabOutBackupManual");
            iniC.pathDownloadFile = iniF.getIni("app", "pathDownloadFile");
            iniC.pathScreenCaptureSend = iniF.getIni("app", "pathScreenCaptureSend");
            iniC.pacsServerIP = iniF.getIni("app", "pacsServerIP");
            iniC.pacsServerPort = iniF.getIni("app", "pacsServerPort");
            iniC.tabLabOutImageHeight = iniF.getIni("app", "tabLabOutImageHeight");
            iniC.tabLabOutImageWidth = iniF.getIni("app", "tabLabOutImageWidth");
            iniC.statusShowLabOutFrmLabOutReceiveView = iniF.getIni("app", "statusShowLabOutFrmLabOutReceiveView");
            iniC.pathLabOutReceiveMedica = iniF.getIni("app", "pathLabOutReceiveMedica");
            iniC.pathLabOutBackupMedica = iniF.getIni("app", "pathLabOutBackupMedica");
            iniC.statusLabOutReceiveOnline = iniF.getIni("app", "statusLabOutReceiveOnline");
            iniC.laboutMedicahosp_code = iniF.getIni("app", "laboutMedicahosp_code");
            iniC.statusLabOutAutoPrint = iniF.getIni("app", "statusLabOutAutoPrint");
            iniC.printerLabOut = iniF.getIni("app", "printerLabOut");
            iniC.statusLabOutReceiveTabShow = iniF.getIni("app", "statusLabOutReceiveTabShow");
            iniC.branchId = iniF.getIni("app", "branchId");
            iniC.pathline_bot_labout_urgent_bangna = iniF.getIni("app", "pathline_bot_labout_urgent_bangna");
            iniC.grfImgWidth = iniF.getIni("app", "grfImgWidth");
            iniC.scVssizeradio = iniF.getIni("app", "scVssizeradio");
            iniC.laboutdateMedica = iniF.getIni("app", "laboutdateMedica");
            iniC.medicalrecordexportpath = iniF.getIni("app", "medicalrecordexportpath");
            iniC.themegrfOpd = iniF.getIni("app", "themegrfOpd");
            iniC.themegrfIpd = iniF.getIni("app", "themegrfIpd");
            iniC.statusoutlabMedica = iniF.getIni("app", "statusoutlabMedica");
            iniC.ssoid = iniF.getIni("app", "ssoid");
            iniC.opbkkhcode = iniF.getIni("app", "opbkkhcode");
            iniC.lab_code = iniF.getIni("app", "lab_code");
            iniC.pathSaveExcelNovel = iniF.getIni("app", "pathSaveExcelNovel");

            iniC.hostnamee = iniF.getIni("app", "hostnamee");
            iniC.hostaddresst = iniF.getIni("app", "hostaddresst");
            iniC.hostaddresse = iniF.getIni("app", "hostaddresse");

            iniC.imageCC_width = iniF.getIni("app", "imageCC_width");
            iniC.imageME_width = iniF.getIni("app", "imageME_width");
            iniC.imageDiag_width = iniF.getIni("app", "imageDiag_width");
            iniC.imageCC_Height = iniF.getIni("app", "imageCC_Height");
            iniC.imageME_Height = iniF.getIni("app", "imageME_Height");
            iniC.imageDiag_Height = iniF.getIni("app", "imageDiag_Height");

            iniC.statusSmartCardNoDatabase = iniF.getIni("app", "statusSmartCardNoDatabase");
            iniC.printerStaffNote = iniF.getIni("app", "printerStaffNote");
            iniC.printerLeter = iniF.getIni("app", "printerLeter");
            iniC.printerA5 = iniF.getIni("app", "printerA5");
            iniC.printerQueue = iniF.getIni("app", "printerQueue");
            iniC.statusSmartCardvaccine = iniF.getIni("app", "statusSmartCardvaccine");
            iniC.statusPrintSticker = iniF.getIni("app", "statusPrintSticker");
            iniC.FrmSmartCardTabDefault = iniF.getIni("app", "FrmSmartCardTabDefault");
            iniC.stickerPrintNumber = iniF.getIni("app", "stickerPrintNumber");
            iniC.statusStation = iniF.getIni("app", "statusStation");
            iniC.paidcode = iniF.getIni("app", "paidcode");
            iniC.dtrcode = iniF.getIni("app", "dtrcode");
            iniC.hosttel = iniF.getIni("app", "hosttel");
            iniC.printerStickerDrug = iniF.getIni("app", "printerStickerDrug");
            iniC.printadjust = iniF.getIni("app", "printadjust");
            iniC.importMDBpaidcode = iniF.getIni("app", "importMDBpaidcode");
            iniC.statusVisitBack = iniF.getIni("app", "statusVisitBack");
            iniC.aipnXmlPath = iniF.getIni("app", "aipnXmlPath");
            iniC.aipnAuthorName = iniF.getIni("app", "aipnAuthorName");

            iniC.ssopXmlPath = iniF.getIni("app", "ssopXmlPath");
            iniC.padYCertMed = iniF.getIni("app", "padYCertMed");

            iniC.grdViewFontSize = iniF.getIni("app", "grdViewFontSize");
            iniC.grdViewFontName = iniF.getIni("app", "grdViewFontName");

            iniC.grdQueFontSize = iniF.getIni("app", "grdQueFontSize");
            iniC.grdQueFontName = iniF.getIni("app", "grdQueFontName");

            iniC.grdQueTodayFontSize = iniF.getIni("app", "grdQueTodayFontSize");
            iniC.grdQueTodayFontName = iniF.getIni("app", "grdQueTodayFontName");

            iniC.pathLabOutReceiveATTA = iniF.getIni("app", "pathLabOutReceiveATTA");
            iniC.pathLabOutBackupATTA = iniF.getIni("app", "pathLabOutBackupATTA");
            iniC.statusScreenCaptureUploadDoc = iniF.getIni("app", "statusScreenCaptureUploadDoc");
            iniC.statusScreenCaptureAutoSend = iniF.getIni("app", "statusScreenCaptureAutoSend");
            iniC.statusPrintPreview = iniF.getIni("app", "statusPrintPreview");
            iniC.grfRowHeight = iniF.getIni("app", "grfRowHeight");
            iniC.nightTime = iniF.getIni("app", "nightTime");
            iniC.nightTimeOn = iniF.getIni("app", "nightTimeOn");
            iniC.statusAutoPrintLabResult = iniF.getIni("app", "statusAutoPrintLabResult");
            iniC.statusdoctorold = iniF.getIni("app", "statusdoctorold");
            iniC.linkmedicalscan = iniF.getIni("app", "linkmedicalscan");
            iniC.applicationrunnextrecord = iniF.getIni("app", "applicationrunnextrecord");
            iniC.statusdruginon = iniF.getIni("app", "statusdruginon");
            iniC.doealientoken = iniF.getIni("app", "doealientoken");
            iniC.urlbangnadoe = iniF.getIni("app", "urlbangnadoe");
            iniC.provcode = iniF.getIni("app", "provcode");
            iniC.urlbangnadoeresult = iniF.getIni("app", "urlbangnadoeresult");
            iniC.pathdoealiencert = iniF.getIni("app", "pathdoealiencert");
            iniC.pathlocalStaffNote = iniF.getIni("app", "pathlocalStaffNote");
            iniC.compcodedoe = iniF.getIni("app", "compcodedoe");
            iniC.staffNoteFontName = iniF.getIni("app", "staffNoteFontName");
            iniC.staffNoteFontSize = iniF.getIni("app", "staffNoteFontSize");
            iniC.usersharepathstaffnote = iniF.getIni("app", "usersharepathstaffnote");
            iniC.passwordsharepathstaffnote = iniF.getIni("app", "passwordsharepathstaffnote");
            iniC.pathapp = iniF.getIni("app", "pathapp");
            iniC.pathlocalEKG = iniF.getIni("app", "pathlocalEKG");
            iniC.pathlocalDocOLD = iniF.getIni("app", "pathlocalDocOLD");
            iniC.pathlocalEST = iniF.getIni("app", "pathlocalEST");
            iniC.pathlocalECHO = iniF.getIni("app", "pathlocalECHO");
            iniC.pathlocalHolter = iniF.getIni("app", "pathlocalHolter");
            iniC.statusPasswordConfirm = iniF.getIni("app", "statusPasswordConfirm");
            iniC.statusShowMessageError = iniF.getIni("app", "statusShowMessageError");
            iniC.scannername = iniF.getIni("app", "scannername");
            iniC.deptphone = iniF.getIni("app", "deptphone");
            iniC.hostaddress1 = iniF.getIni("app", "hostaddress1");
            iniC.statusPrintStickerDrugAll = iniF.getIni("app", "statusPrintStickerDrugAll");
            iniC.CLAUDEAPI_KEY = iniF.getIni("app", "CLAUDEAPI_KEY");
            iniC.API_juristic = iniF.getIni("app", "API_juristic");
            iniC.printadjustY = iniF.getIni("app", "printadjustY");
            iniC.printYPOS = iniF.getIni("app", "printYPOS");
            iniC.statusPrintQue = iniF.getIni("app", "statusPrintQue");
            iniC.sectioncheckup = iniF.getIni("app", "sectioncheckup");
            iniC.tabdefault = iniF.getIni("app", "tabdefault");

            iniC.email_form = iniF.getIni("email", "email_form");
            iniC.email_auth_user = iniF.getIni("email", "email_auth_user");
            iniC.email_auth_pass = iniF.getIni("email", "email_auth_pass");
            iniC.email_port = iniF.getIni("email", "email_port");
            iniC.email_ssl = iniF.getIni("email", "email_ssl");
            iniC.EmailFromAIPN = iniF.getIni("email", "EmailFromAIPN");
            iniC.EmailToAIPN = iniF.getIni("email", "EmailToAIPN");
            iniC.EmailSubjectAIPN = iniF.getIni("email", "EmailSubjectAIPN");
            iniC.EmailPortAIPN = iniF.getIni("email", "EmailPortAIPN");
            iniC.EmailAuthUserAIPN = iniF.getIni("email", "EmailAuthUserAIPN");
            iniC.EmailAuthPassAIPN = iniF.getIni("email", "EmailAuthPassAIPN");

            iniC.OPD_BTEMP = iniF.getIni("OPBKKClaim", "OPD_BTEMP");
            iniC.OPD_SBP = iniF.getIni("OPBKKClaim", "OPD_SBP");//      ความดันโลหิตค่าตัวบน
            iniC.OPD_DBP = iniF.getIni("OPBKKClaim", "OPD_DBP");//      ความดันโลหิตค่าตัวล่าง
            iniC.OPD_PR = iniF.getIni("OPBKKClaim", "OPD_PR");//      อัตราการเต้นหัวใจ
            iniC.OPD_RR = iniF.getIni("OPBKKClaim", "OPD_RR");//      อัตราการหายใจ

            iniC.OPD_BTEMP = iniC.OPD_BTEMP == null ? "mnc_temp" : iniC.OPD_BTEMP.Equals("") ? "mnc_temp" : iniC.OPD_BTEMP;
            iniC.OPD_SBP = iniC.OPD_SBP == null ? "mnc_temp" : iniC.OPD_SBP.Equals("") ? "mnc_temp" : iniC.OPD_SBP;
            iniC.OPD_DBP = iniC.OPD_DBP == null ? "mnc_temp" : iniC.OPD_DBP.Equals("") ? "mnc_temp" : iniC.OPD_DBP;
            iniC.OPD_PR = iniC.OPD_PR == null ? "mnc_temp" : iniC.OPD_PR.Equals("") ? "mnc_temp" : iniC.OPD_PR;
            iniC.OPD_RR = iniC.OPD_RR == null ? "mnc_temp" : iniC.OPD_RR.Equals("") ? "mnc_temp" : iniC.OPD_RR;

            iniC.themeApplication = iniC.themeApplication == null ? "Office2007Blue" : iniC.themeApplication.Equals("") ? "Office2007Blue" : iniC.themeApplication;
            iniC.timerImgScanNew = iniC.timerImgScanNew == null ? "2" : iniC.timerImgScanNew.Equals("") ? "0" : iniC.timerImgScanNew;
            iniC.pathImageScan = iniC.pathImageScan == null ? "d:\\images" : iniC.pathImageScan.Equals("") ? "d:\\images" : iniC.pathImageScan;
            iniC.imggridscanwidth = iniC.imggridscanwidth == null ? "380" : iniC.imggridscanwidth.Equals("") ? "380" : iniC.imggridscanwidth;
            iniC.themeApp = iniC.themeApp == null ? "Office2007Blue" : iniC.themeApp.Equals("") ? "Office2007Blue" : iniC.themeApp;
            iniC.pathScreenCaptureSend = iniC.pathScreenCaptureSend == null ? "C:\\capture" : iniC.pathScreenCaptureSend.Equals("") ? "C:\\capture" : iniC.pathScreenCaptureSend;

            //iniC.pathImgScanNew = iniC.pathImgScanNew == null ? "d:\\images" : iniC.pathImgScanNew.Equals("") ? "d:\\images" : iniC.pathImgScanNew;
            iniC.folderFTP = iniC.folderFTP == null ? "images_medical_record" : iniC.folderFTP.Equals("") ? "images_medical_record" : iniC.folderFTP;
            iniC.grdViewFontName = iniC.grdViewFontName == null ? "Microsoft Sans Serif" : iniC.grdViewFontName;
            iniC.pdfFontName = iniC.pdfFontName == null ? iniC.grdViewFontName : iniC.pdfFontName;
            iniC.pdfFontSize = iniC.pdfFontSize == null ? iniC.grdViewFontSize : iniC.pdfFontSize;
            iniC.pdfFontSizetitleFont = iniC.pdfFontSizetitleFont == null ? iniC.pdfFontSize : iniC.pdfFontSizetitleFont;
            iniC.pdfFontSizetxtFont = iniC.pdfFontSizetxtFont == null ? iniC.pdfFontSize : iniC.pdfFontSizetxtFont;
            iniC.pdfFontSizehdrFont = iniC.pdfFontSizehdrFont == null ? iniC.pdfFontSize : iniC.pdfFontSizehdrFont;
            iniC.pdfFontSizetxtFontB = iniC.pdfFontSizetxtFontB == null ? iniC.pdfFontSize : iniC.pdfFontSizetxtFontB;

            iniC.hostname = iniC.hostname == null ? "โรงพยาบาล" : iniC.hostname.Equals("") ? "โรงพยาบาล" : iniC.hostname;
            iniC.usePassiveFTP = iniC.usePassiveFTP == null ? "false" : iniC.usePassiveFTP.Equals("") ? "false" : iniC.usePassiveFTP;
            iniC.usePassiveFTPLabOut = iniC.usePassiveFTPLabOut == null ? "false" : iniC.usePassiveFTPLabOut.Equals("") ? "false" : iniC.usePassiveFTPLabOut;
            iniC.labOutOpenFileDialog = iniC.labOutOpenFileDialog == null ? "" : iniC.labOutOpenFileDialog.Equals("") ? "" : iniC.labOutOpenFileDialog;
            iniC.windows = iniC.windows == null ? "" : iniC.windows.Equals("") ? "" : iniC.windows;
            iniC.imgScanWidth = iniC.imgScanWidth == null ? "380" : iniC.imgScanWidth.Equals("") ? "380" : iniC.imgScanWidth;
            iniC.pathScanStaffNote = iniC.pathScanStaffNote == null ? "172.25.10.5" : iniC.pathScanStaffNote.Equals("") ? "172.25.10.5" : iniC.pathScanStaffNote;
            iniC.ProxyProxyType = iniC.ProxyProxyType == null ? "0" : iniC.ProxyProxyType.Equals("") ? "0" : iniC.ProxyProxyType;
            iniC.ProxyPort = iniC.ProxyPort == null ? "0" : iniC.ProxyPort.Equals("") ? "0" : iniC.ProxyPort;
            iniC.pathIniFile = iniC.pathIniFile == null ? "" : iniC.pathIniFile.Equals("") ? "" : iniC.pathIniFile;
            iniC.statusShowPrintDialog = iniC.statusShowPrintDialog == null ? "0" : iniC.statusShowPrintDialog.Equals("") ? "0" : iniC.statusShowPrintDialog;
            iniC.timerCheckLabOut = iniC.timerCheckLabOut == null ? "0" : iniC.timerCheckLabOut.Equals("") ? "0" : iniC.timerCheckLabOut;
            iniC.station = iniC.station == null ? "0" : iniC.station.Equals("") ? "0" : iniC.station;
            iniC.pathDownloadFile = iniC.pathDownloadFile == null ? "" : iniC.pathDownloadFile.Equals("") ? "" : iniC.pathDownloadFile;
            iniC.pacsServerIP = iniC.pacsServerIP == null ? "" : iniC.pacsServerIP.Equals("") ? "" : iniC.pacsServerIP;
            iniC.pacsServerPort = iniC.pacsServerPort == null ? "" : iniC.pacsServerPort.Equals("") ? "" : iniC.pacsServerPort;
            iniC.tabLabOutImageHeight = iniC.tabLabOutImageHeight == null ? "842" : iniC.tabLabOutImageHeight.Equals("") ? "842" : iniC.tabLabOutImageHeight;
            iniC.tabLabOutImageWidth = iniC.tabLabOutImageWidth == null ? "595" : iniC.tabLabOutImageWidth.Equals("") ? "595" : iniC.tabLabOutImageWidth;
            iniC.statusShowLabOutFrmLabOutReceiveView = iniC.statusShowLabOutFrmLabOutReceiveView == null ? "windows10" : iniC.statusShowLabOutFrmLabOutReceiveView.Equals("") ? "windows10" : iniC.statusShowLabOutFrmLabOutReceiveView;
            iniC.pathLabOutReceiveMedica = iniC.pathLabOutReceiveMedica == null ? "c:\\medica\\result" : iniC.pathLabOutReceiveMedica.Equals("") ? "c:\\medica\\result" : iniC.pathLabOutReceiveMedica;
            iniC.pathLabOutBackupMedica = iniC.pathLabOutBackupMedica == null ? "c:\\medica\\backup" : iniC.pathLabOutBackupMedica.Equals("") ? "c:\\medica\\backup" : iniC.pathLabOutBackupMedica;
            iniC.statusLabOutReceiveOnline = iniC.statusLabOutReceiveOnline == null ? "0" : iniC.statusLabOutReceiveOnline.Equals("") ? "0" : iniC.statusLabOutReceiveOnline;
            iniC.statusLabOutAutoPrint = iniC.statusLabOutAutoPrint == null ? "0" : iniC.statusLabOutAutoPrint.Equals("") ? "0" : iniC.statusLabOutAutoPrint;
            iniC.printerLabOut = iniC.printerLabOut == null ? "" : iniC.printerLabOut.Equals("") ? "" : iniC.printerLabOut;
            iniC.statusLabOutReceiveTabShow = iniC.statusLabOutReceiveTabShow == null ? "1" : iniC.statusLabOutReceiveTabShow.Equals("") ? "1" : iniC.statusLabOutReceiveTabShow;
            iniC.branchId = iniC.branchId == null ? "005" : iniC.branchId.Equals("") ? "005" : iniC.branchId;
            iniC.pathline_bot_labout_urgent_bangna = iniC.pathline_bot_labout_urgent_bangna == null ? "c:\\python\\line_bot_labout_urgent_bangna.py" : iniC.pathline_bot_labout_urgent_bangna.Equals("") ? "c:\\python\\line_bot_labout_urgent_bangna.py" : iniC.pathline_bot_labout_urgent_bangna;
            iniC.grfImgWidth = iniC.grfImgWidth == null ? "500" : iniC.grfImgWidth.Equals("") ? "500" : iniC.grfImgWidth;
            iniC.scVssizeradio = iniC.scVssizeradio == null ? "20" : iniC.scVssizeradio.Equals("") ? "20" : iniC.scVssizeradio;
            iniC.laboutdateMedica = iniC.laboutdateMedica == null ? "" : iniC.laboutdateMedica.Equals("") ? "" : iniC.laboutdateMedica;
            iniC.medicalrecordexportpath = iniC.medicalrecordexportpath == null ? "c:\\exportpath" : iniC.medicalrecordexportpath.Equals("") ? "" : iniC.medicalrecordexportpath;
            iniC.themegrfOpd = iniC.themegrfOpd == null ? "Office2016Colorful" : iniC.themegrfOpd.Equals("") ? "Office2016Colorful" : iniC.themegrfOpd;
            iniC.themegrfIpd = iniC.themegrfIpd == null ? "Office2007Black" : iniC.themegrfIpd.Equals("") ? "Office2007Black" : iniC.themegrfIpd;
            iniC.statusoutlabMedica = iniC.statusoutlabMedica == null ? "1" : iniC.statusoutlabMedica.Equals("") ? "1" : iniC.statusoutlabMedica;
            iniC.pdfFontName = iniC.pdfFontName == null ? iniC.grdViewFontName : iniC.pdfFontName.Equals("") ? iniC.grdViewFontName : iniC.pdfFontName;
            iniC.hostnamee = iniC.hostnamee == null ? "" : iniC.hostnamee.Equals("") ? "" : iniC.hostnamee;
            iniC.email_auth_pass = iniC.email_auth_pass == null ? "" : iniC.email_auth_pass.Equals("") ? "" : iniC.email_auth_pass;
            iniC.email_auth_user = iniC.email_auth_user == null ? "" : iniC.email_auth_user.Equals("") ? "" : iniC.email_auth_user;
            iniC.email_form = iniC.email_form == null ? "" : iniC.email_form.Equals("") ? "" : iniC.email_form;
            iniC.email_port = iniC.email_port == null ? "" : iniC.email_port.Equals("") ? "" : iniC.email_port;
            iniC.email_ssl = iniC.email_ssl == null ? "" : iniC.email_ssl.Equals("") ? "" : iniC.email_ssl;
            iniC.imageCC_width = iniC.imageCC_width == null ? "800" : iniC.imageCC_width.Equals("") ? "800" : iniC.imageCC_width;
            iniC.imageME_width = iniC.imageME_width == null ? "800" : iniC.imageME_width.Equals("") ? "800" : iniC.imageME_width;
            iniC.imageDiag_width = iniC.imageDiag_width == null ? "800" : iniC.imageDiag_width.Equals("") ? "800" : iniC.imageDiag_width;
            iniC.imageCC_Height = iniC.imageCC_Height == null ? "400" : iniC.imageCC_Height.Equals("") ? "400" : iniC.imageCC_Height;
            iniC.imageME_Height = iniC.imageME_Height == null ? "400" : iniC.imageME_Height.Equals("") ? "400" : iniC.imageME_Height;
            iniC.imageDiag_Height = iniC.imageDiag_Height == null ? "400" : iniC.imageDiag_Height.Equals("") ? "400" : iniC.imageDiag_Height;
            iniC.ssoid = iniC.ssoid == null ? "0" : iniC.ssoid.Equals("") ? "0" : iniC.ssoid;
            iniC.opbkkhcode = iniC.opbkkhcode == null ? "0" : iniC.opbkkhcode.Equals("") ? "0" : iniC.opbkkhcode;
            iniC.statusSmartCardNoDatabase = iniC.statusSmartCardNoDatabase == null ? "0" : iniC.statusSmartCardNoDatabase.Equals("") ? "0" : iniC.statusSmartCardNoDatabase;
            iniC.statusSmartCardvaccine = iniC.statusSmartCardvaccine == null ? "0" : iniC.statusSmartCardvaccine.Equals("") ? "0" : iniC.statusSmartCardvaccine;
            iniC.FrmSmartCardTabDefault = iniC.FrmSmartCardTabDefault == null ? "1" : iniC.FrmSmartCardTabDefault.Equals("") ? "1" : iniC.FrmSmartCardTabDefault;
            iniC.stickerPrintNumber = iniC.stickerPrintNumber == null ? "1" : iniC.stickerPrintNumber.Equals("") ? "1" : iniC.stickerPrintNumber;
            iniC.statusStation = iniC.statusStation == null ? "OPD" : iniC.statusStation.Equals("") ? "OPD" : iniC.statusStation;
            iniC.statusVisitBack = iniC.statusVisitBack == null ? "0" : iniC.statusVisitBack.Equals("") ? "0" : iniC.statusVisitBack;
            iniC.statusScreenCaptureUploadDoc = iniC.statusScreenCaptureUploadDoc == null ? "0" : iniC.statusScreenCaptureUploadDoc.Equals("") ? "0" : iniC.statusScreenCaptureUploadDoc;
            iniC.padYCertMed = iniC.padYCertMed == null ? "820" : iniC.padYCertMed.Equals("") ? "820" : iniC.padYCertMed;
            iniC.statusScreenCaptureAutoSend = iniC.statusScreenCaptureAutoSend == null ? "0" : iniC.statusScreenCaptureAutoSend.Equals("") ? "0" : iniC.statusScreenCaptureAutoSend;
            iniC.statusPrintPreview = iniC.statusPrintPreview == null ? "0" : iniC.statusPrintPreview.Equals("") ? "0" : iniC.statusPrintPreview;
            iniC.nightTime = iniC.nightTime == null ? "1900:0600" : iniC.nightTime.Equals("") ? "1900:0600" : iniC.nightTime;
            iniC.nightTimeOn = iniC.nightTimeOn == null ? "0" : iniC.nightTimeOn.Equals("") ? "0" : iniC.nightTimeOn;
            iniC.statusAutoPrintLabResult = iniC.statusAutoPrintLabResult == null ? "0" : iniC.statusAutoPrintLabResult.Equals("") ? "0" : iniC.statusAutoPrintLabResult;
            iniC.statusdoctorold = iniC.statusdoctorold == null ? "old" : iniC.statusdoctorold.Equals("") ? "old" : iniC.statusdoctorold;
            iniC.linkmedicalscan = iniC.linkmedicalscan == null ? "" : iniC.linkmedicalscan.Equals("") ? "" : iniC.linkmedicalscan;
            iniC.applicationrunnextrecord = iniC.applicationrunnextrecord == null ? "" : iniC.applicationrunnextrecord.Equals("") ? "" : iniC.applicationrunnextrecord;
            iniC.statusdruginon = iniC.statusdruginon == null ? "" : iniC.statusdruginon.Equals("") ? "" : iniC.statusdruginon;
            iniC.pathlocalStaffNote = iniC.pathlocalStaffNote == null ? "D:\\image\\" : iniC.pathlocalStaffNote.Equals("") ? "D:\\image\\" : iniC.pathlocalStaffNote;
            iniC.staffNoteFontName = iniC.staffNoteFontName == null ? iniC.pdfFontName : iniC.staffNoteFontName.Equals("") ? iniC.pdfFontName : iniC.staffNoteFontName;
            iniC.staffNoteFontSize = iniC.staffNoteFontSize == null ? iniC.pdfFontSize : iniC.staffNoteFontSize.Equals("") ? iniC.pdfFontSize : iniC.staffNoteFontSize;
            iniC.pathapp = iniC.pathapp == null ? "C:\app" : iniC.pathapp.Equals("") ? "C:\app" : iniC.pathapp;
            iniC.pathlocalEKG = iniC.pathlocalEKG == null ? "D:\\EKG" : iniC.pathlocalEKG.Equals("") ? "D:\\EKG" : iniC.pathlocalEKG;
            iniC.pathlocalDocOLD = iniC.pathlocalDocOLD == null ? "D:\\docold" : iniC.pathlocalDocOLD.Equals("") ? "D:\\docold" : iniC.pathlocalDocOLD;
            iniC.pathlocalEST = iniC.pathlocalEST == null ? "D:\\EST" : iniC.pathlocalEST.Equals("") ? "D:\\EST" : iniC.pathlocalEST;
            iniC.pathlocalECHO = iniC.pathlocalECHO == null ? "D:\\ECHO" : iniC.pathlocalECHO.Equals("") ? "D:\\ECHO" : iniC.pathlocalECHO;
            iniC.pathlocalHolter = iniC.pathlocalHolter == null ? "D:\\Holter" : iniC.pathlocalHolter.Equals("") ? "D:\\Holter" : iniC.pathlocalHolter;
            iniC.statusPasswordConfirm = iniC.statusPasswordConfirm == null ? "0" : iniC.statusPasswordConfirm.Equals("") ? "0" : iniC.statusPasswordConfirm;
            iniC.statusShowMessageError = iniC.statusShowMessageError == null ? "0" : iniC.statusShowMessageError.Equals("") ? "0" : iniC.statusShowMessageError;
            iniC.scannername = iniC.scannername == null ? "" : iniC.scannername.Equals("") ? "" : iniC.scannername;
            iniC.deptphone = iniC.deptphone == null ? "" : iniC.deptphone.Equals("") ? "" : iniC.deptphone;
            iniC.hostaddress1 = iniC.hostaddress1 == null ? "" : iniC.hostaddress1.Equals("") ? "" : iniC.hostaddress1;
            iniC.statusPrintStickerDrugAll = iniC.statusPrintStickerDrugAll == null ? "" : iniC.statusPrintStickerDrugAll.Equals("") ? "" : iniC.statusPrintStickerDrugAll;
            iniC.CLAUDEAPI_KEY = iniC.CLAUDEAPI_KEY == null ? "" : iniC.CLAUDEAPI_KEY.Equals("") ? "" : iniC.CLAUDEAPI_KEY;
            iniC.API_juristic = iniC.API_juristic == null ? "" : iniC.API_juristic.Equals("") ? "" : iniC.API_juristic;
            iniC.printadjustY = iniC.printadjustY == null ? "0" : iniC.printadjustY.Equals("") ? "0" : iniC.printadjustY;
            iniC.printYPOS = iniC.printYPOS == null ? "0" : iniC.printYPOS.Equals("") ? "0" : iniC.printYPOS;
            iniC.statusPrintQue = iniC.statusPrintQue == null ? "0" : iniC.statusPrintQue.Equals("") ? "0" : iniC.statusPrintQue;
            iniC.sectioncheckup = iniC.sectioncheckup == null ? "302" : iniC.sectioncheckup.Equals("") ? "302" : iniC.sectioncheckup;
            iniC.tabdefault = iniC.tabdefault == null ? "0" : iniC.tabdefault.Equals("") ? "0" : iniC.tabdefault;

            int.TryParse(iniC.grdViewFontSize, out grdViewFontSize);
            int.TryParse(iniC.pdfFontSize, out pdfFontSize);
            int.TryParse(iniC.pdfFontSizehdrFont, out pdfFontSizehdrFont);
            int.TryParse(iniC.pdfFontSizetitleFont, out pdfFontSizetitleFont);
            int.TryParse(iniC.pdfFontSizetxtFont, out pdfFontSizetxtFont);
            int.TryParse(iniC.pdfFontSizetxtFontB, out pdfFontSizetxtFontB);
            int.TryParse(iniC.queFontSize, out queFontSize);
            int.TryParse(iniC.staffNoteFontSize, out staffNoteFontSize);

            int.TryParse(iniC.imggridscanwidth, out imggridscanwidth);
            Boolean.TryParse(iniC.usePassiveFTP, out ftpUsePassive);
            Boolean.TryParse(iniC.usePassiveFTPLabOut, out ftpUsePassiveLabOut);
            Boolean.TryParse(iniC.EnableSsl, out EnableSsl);

            int.TryParse(iniC.grfScanWidth, out grfScanWidth);
            int.TryParse(iniC.timerCheckLabOut, out timerCheckLabOut);

            int.TryParse(iniC.imggridscanwidth, out imggridscanwidth);
            int.TryParse(iniC.imgScanWidth, out imgScanWidth);
            int.TryParse(iniC.txtSearchHnLenghtStart, out txtSearchHnLenghtStart);
            int.TryParse(iniC.tabLabOutImageHeight, out tabLabOutImageHeight);
            int.TryParse(iniC.tabLabOutImageWidth, out tabLabOutImageWidth);
            int.TryParse(iniC.grfImgWidth, out grfImgWidth);
            int.TryParse(iniC.scVssizeradio, out scVssizeradio);
            int.TryParse(iniC.imageCC_width, out imageCC_width);
            int.TryParse(iniC.imageME_width, out imageME_width);
            int.TryParse(iniC.imageDiag_width, out imageDiag_width);
            int.TryParse(iniC.imageCC_Height, out imageCC_Height);
            int.TryParse(iniC.imageME_Height, out imageME_Height);
            int.TryParse(iniC.imageDiag_Height, out imageDiag_Height);
            int.TryParse(iniC.padYCertMed, out padYCertMed);
            int.TryParse(iniC.grdQueFontSize, out grdQueFontSize);
            int.TryParse(iniC.grdQueTodayFontSize, out grdQueTodayFontSize);
            int.TryParse(iniC.grfRowHeight, out grfRowHeight);
            int.TryParse(iniC.timerImgScanNew, out timerImgScanNew);
        }
        public String setC1Combo(C1ComboBox c, String data)
        {
            String chk = "";
            if (c.Items.Count == 0) return "";
            //if (c.SelectedIndex < 0) return;
            if (c.SelectedIndex > c.Items.Count)
            {
                c.SelectedIndex = 0;
            }
            c.SelectedIndex = c.SelectedItem == null ? 0 : c.SelectedIndex;
            c.SelectedIndex = 0;
            foreach (ComboBoxItem item in c.Items)
            {
                if (item.Value.Equals(data))
                {
                    c.SelectedItem = item;
                    chk = "ok";
                    break;
                }
            }
            return chk;
        }
        public String adjustACTNO(String secno)
        {
            String re = "";
            if (secno.Equals("101"))
            {
                re = "ส่งตัว";
            }
            else if (secno.Equals("110"))
            {
                re = "พบแพทย์";
            }
            else if (secno.Equals("114"))
            {
                re = "เข้าห้องตรวจ";
            }
            else if (secno.Equals("131"))
            {
                re = "จ่ายยา";
            }
            else if (secno.Equals("500"))
            {
                re = "ปิดทำการ";
            }
            else if (secno.Equals("600"))
            {
                re = "คิดคชจ";
            }
            else if (secno.Equals("610"))
            {
                re = "รับชำระ";
            }
            return re;
        }
        public String adjustSecNoOPD(String secno)
        {
            String re = "";
            if (secno.Equals("181"))
            {
                re = "302";
            }
            else if (secno.Equals("182"))
            {
                re = "144";
            }
            else if (secno.Equals("115"))
            {
                re = "147";
            }
            else if (secno.Equals("183"))
            {
                re = "302";
            }
            else if (secno.Equals("135"))
            {
                re = "131";
            }
            else if (secno.Equals("161"))
            {
                re = "302";//OPD3
            }
            else if (secno.Equals("171"))
            {
                re = "147";//OPD1
            }
            else
            {
                re = secno;
            }
            return re;
        }
        public void setC1ComboByName(C1ComboBox c, String data)
        {
            if (c.Items.Count == 0) return;
            if (data.Length == 0) c.SelectedIndex = 0;
            c.SelectedIndex = c.SelectedItem == null ? 0 : c.SelectedIndex;
            c.SelectedIndex = 0;
            foreach (ComboBoxItem item in c.Items)
            {
                if (item.Text.Equals(data))
                {
                    c.SelectedItem = item;
                    break;
                }
            }
        }
        public Image RotateImage(Image img)
        {
            var bmp = new Bitmap(img);

            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Color.White);
                gfx.DrawImage(img, 0, 0, img.Width, img.Height);
            }

            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            //bmp.Dispose();
            return bmp;
        }
        public Image RotateImage90(Image img)
        {
            var bmp = new Bitmap(img);

            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Color.White);
                gfx.DrawImage(img, 0, 0, img.Width, img.Height);
            }

            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            //bmp.Dispose();
            return bmp;
        }
        public Stream ToStream(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
        public String datetoShow(Object dt)
        {
            if (dt == null) return "";
            DateTime dt1 = new DateTime();
            //MySqlDateTime dtm = new MySqlDateTime();
            String re = "";
            if (iniC.windows.Equals("windowsxp"))
            {
                if (DateTime.TryParse(dt.ToString(), out dt1))
                {
                    if (dt1.Year > 2500)
                    {
                        re = dt1.ToString("dd-MM") + "-" + (dt1.Year - 543).ToString();
                    }
                    else if (dt1.Year < 1500)
                    {
                        re = dt1.ToString("dd-MM") + "-" + (dt1.Year + 543).ToString();
                    }
                    else
                    {
                        re = dt1.ToString("dd-MM") + "-" + dt1.Year.ToString();
                    }
                }
                //re = dt.ToString();
            }
            else
            {
                if (dt != null)
                {
                    if (DateTime.TryParse(dt.ToString(), out dt1))
                    {
                        if (dt1.Year < 2000)
                        {
                            dt1 = dt1.AddYears(543);
                        }
                        re = dt1.ToString("dd-MM-yyyy");
                    }
                }
            }
            return re;
        }
        public String datetoShow1(String dt)
        {
            DateTime dt1 = new DateTime();
            //MySqlDateTime dtm = new MySqlDateTime();
            String re = "", year1 = "", mm = "", dd = "";
            if (iniC.windows.Equals("windowsxp"))
            {
                if (dt.Length >= 10)
                {
                    year1 = dt.Substring(0, 4);
                    mm = dt.Substring(5, 2);
                    dd = dt.Substring(8, 2);
                    re = dd + "-" + mm + "-" + year1;
                }
                else
                {
                    re = "";
                }
                //re = dt.ToString();
            }
            else
            {
                if ((dt != null) && (dt.Length > 0))
                {
                    if (DateTime.TryParse(dt, out dt1))
                    {
                        if (dt1.Year < 2000)
                        {
                            dt1 = dt1.AddYears(543);
                        }
                        re = dt1.ToString("dd-MM" + "-" + dt1.Year.ToString());
                    }
                    else
                    {
                        year1 = dt.Substring(0, 4);
                        mm = dt.Substring(5, 2);
                        dd = dt.Substring(8, 2);
                        re = dd + "-" + mm + "-" + year1;
                    }
                }
            }
            return re;
        }
        public String datetoShowShort(String dt)
        {
            DateTime dt1 = new DateTime();
            //MySqlDateTime dtm = new MySqlDateTime();
            String re = "", year1 = "", mm = "", dd = "";
            if (iniC.windows.Equals("windowsxp"))
            {
                if (dt.Length >= 10)
                {
                    year1 = dt.Substring(0, 4);
                    mm = dt.Substring(5, 2);
                    dd = dt.Substring(8, 2);
                    re = dd + "-" + mm + "-" + year1;
                }
                else
                {
                    re = "";
                }
                //re = dt.ToString();
            }
            else
            {
                if (dt != null)
                {
                    if (DateTime.TryParse(dt.ToString(), out dt1))
                    {
                        re = dt1.ToString("dd-MM-yy");
                    }
                }
            }
            return re;
        }
        public String datetoShowEN(String dt)
        {
            DateTime dt1 = new DateTime();
            String re = "", year1 = "", mm = "", dd = "";
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            if (DateTime.TryParse(dt, currentCulture, DateTimeStyles.AdjustToUniversal, out dt1))
            {
                Console.WriteLine("Parsed date: " + dt1.ToString());
                if (dt1.Year < 1900) dt1 = dt1.AddYears(543);
            }
            else
            {
                Console.WriteLine("Unable to parse the date string.");
            }
            re = dt1.ToString("dd-MM") + "-" + dt1.Year.ToString();

            return re;
        }
        public String datetoShowTHMMM(String dt)
        {
            DateTime dt1 = new DateTime();
            String re = "", year1 = "", mm = "", dd = "";
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            if (DateTime.TryParse(dt, currentCulture, DateTimeStyles.AdjustToUniversal, out dt1))
            {
                Console.WriteLine("Parsed date: " + dt1.ToString());
                if (dt1.Year < 1900) dt1 = dt1.AddYears(543);
            }
            else
            {
                Console.WriteLine("Unable to parse the date string.");
            }
            re = dt1.ToString("dd") + " " + getMonthNameTHShort(dt1.ToString("MM")) + " " + (dt1.Year + 543).ToString();
            return re;
        }
        public String datetoDBCultureInfo(String dt)
        {
            DateTime dt1 = new DateTime();
            String re = "", year1 = "", mm = "", dd = "";
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            if (DateTime.TryParse(dt, currentCulture, DateTimeStyles.AdjustToUniversal, out dt1))
            {
                Console.WriteLine("Parsed date: " + dt1.ToString());
                if (dt1.Year < 1900) dt1 = dt1.AddYears(543);
            }
            else
            {
                Console.WriteLine("Unable to parse the date string.");
            }

            re = dt1.Year.ToString() + "-" + dt1.ToString("MM-dd");

            return re;
        }
        public String datetoDB(String dt)
        {
            DateTime dt1 = new DateTime();
            String re = "", year1 = "", mm = "", dd = "";
            int year = 0, mon = 0, day = 0;
            //new LogWriter("d", "datetoDB 01" );
            if (iniC.windows.Equals("windowsxp"))
            {
                //new LogWriter("d", "datetoDB 02 iniC.windowsxp ");
                //if (DateTime.TryParse(dt, out dt1))
                //{
                //    //new LogWriter("d", "datetoDB 02 iniC.windowsxp DateTime.TryParse(dt, out dt1) true dt1.Year"+ dt1.Year);
                //    if (dt1.Year > 2500)
                //    {
                //        re = (dt1.Year - 543).ToString() + "-" + dt1.ToString("MM-dd");
                //    }
                //    else if (dt1.Year < 1500)
                //    {
                //        re = (dt1.Year + 543).ToString() + "-" + dt1.ToString("MM-dd");
                //    }
                //    else
                //    {
                //        re = dt1.Year.ToString() + "-" + dt1.ToString("MM-dd");
                //    }                    
                //}
                //else
                //{
                //new LogWriter("d", "datetoDB 02 iniC.windowsxp DateTime.TryParse(dt, out dt1) false ");
                if (dt.Length >= 10)
                {
                    year1 = dt.Substring(6, 4);
                    mm = dt.Substring(3, 2);
                    dd = dt.Substring(0, 2);
                    int.TryParse(mm, out mon);
                    if (mon > 12)
                    {
                        mm = dt.Substring(0, 2);
                        dd = dt.Substring(3, 2);
                        int.TryParse(mm, out mon);
                    }
                    //new LogWriter("d", "datetoDB year1 " + year1);
                    if (int.TryParse(year1, out year))
                    {
                        //new LogWriter("d", "datetoDB year1 int.TryParse(year1, out year) " + year);
                        if (year <= 1500)
                        {
                            year = year + 543;
                        }
                        else if (year >= 2500)
                        {
                            year = year - 543;
                        }
                    }
                    else
                    {
                        //new LogWriter("d", "datetoDB year1 else int.TryParse(year1, out year) ");
                    }
                    re = year.ToString() + "-" + mm + "-" + dd;
                }
                else
                {

                }
                //}
                //re = dt1.ToString("yyyy-MM-dd");
            }
            else
            {
                //new LogWriter("d", "datetoDB 03 iniC.windows 10 ");
                if (dt != null)
                {
                    if (!dt.Equals(""))
                    {
                        // Thread แบบนี้ ทำให้ โปรแกรม ที่ไปลงที Xtrim ไม่เอา date ผิด
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us")
                        {
                            DateTimeFormat =
                            {
                                DateSeparator = "-"
                            }
                        };
                        if (DateTime.TryParse(dt, out dt1))
                        {

                            if (dt1.Year > 2500)
                            {
                                re = (dt1.Year - 543).ToString() + "-" + dt1.ToString("MM-dd");
                            }
                            else
                            {
                                re = dt1.Year.ToString() + "-" + dt1.ToString("MM-dd");
                            }
                        }
                        else
                        {
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH")
                            {
                                DateTimeFormat =
                            {
                                DateSeparator = "-"
                            }
                            };
                            if (DateTime.TryParse(dt, out dt1))
                            {
                                re = dt1.ToString("yyyy-MM-dd");
                            }
                        }
                        //dt1 = DateTime.Parse(dt.ToString());

                    }
                }
            }

            return re;
        }
        public String datetoDBWin10(String ddMM, String year1)
        {
            DateTime dt1 = new DateTime();
            String re = "", mm = "", dd = "";
            int year = 0, mon = 0, day = 0;
            //new LogWriter("d", "datetoDB 01" );
            //new LogWriter("d", "datetoDB 03 iniC.windows 10 ");

            dd = ddMM.Substring(0, 2);
            mm = ddMM.Substring(3, 2);
            dt1.AddDays(int.Parse(dd)).AddMonths(int.Parse(mm)).AddYears(int.Parse(year1));
            dt1 = new DateTime(int.Parse(year1), int.Parse(mm), int.Parse(dd));
            re = dt1.ToString("yyyy-MM-dd");

            //dt1 = DateTime.Parse(dt.ToString());
            return re;
        }
        public String datetoDBWin10EN(String ddMM, String year1)
        {
            DateTime dt1 = new DateTime();
            String re = "", mm = "", dd = "";
            int year = 0, mon = 0, day = 0;
            //new LogWriter("d", "datetoDB 01" );
            //new LogWriter("d", "datetoDB 03 iniC.windows 10 ");

            dd = ddMM.Substring(0, 2);
            mm = ddMM.Substring(3, 2);
            dt1.AddDays(int.Parse(dd)).AddMonths(int.Parse(mm)).AddYears(int.Parse(year1));
            dt1 = new DateTime(int.Parse(year1), int.Parse(mm), int.Parse(dd));
            re = dt1.Year + "-" + dt1.ToString("MM-dd");

            //dt1 = DateTime.Parse(dt.ToString());
            return re;
        }
        private void initOPBKKClinic()
        {
            opBKKClinic.Add("00", "หน่วยงานระดับสถานีอนามัย");
            opBKKClinic.Add("01", "อายุรกรรม");
            opBKKClinic.Add("02", "ศัลยกรรม");
            opBKKClinic.Add("03", "สูติกรรม");
            opBKKClinic.Add("04", "นรีเวชกรรม");
            opBKKClinic.Add("05", "กุมารเวชกรรม");
            opBKKClinic.Add("06", "โสต ศอ นาสิก");
            opBKKClinic.Add("07", "จักษุวิทยา");
            opBKKClinic.Add("08", "ศัลยกรรมออร์โธปิดิกส");
            opBKKClinic.Add("09", "จิตเวช");
            opBKKClinic.Add("10", "รังสีวิทยา");
            opBKKClinic.Add("11", "ทันตกรรม");
            opBKKClinic.Add("12", "เวชศาสตร์ฉุกเฉินและนิติเวช");
            opBKKClinic.Add("13", "เวชกรรมฟื้นฟู");
            opBKKClinic.Add("14", "แพทย์แผนไทย");
            opBKKClinic.Add("15", "PCU ใน รพ.");
            opBKKClinic.Add("16", "เวชกรรมปฎิบัติทั่วไป");
            opBKKClinic.Add("17", "เวชศาสสตร์ครอบครัวและชุมชน");
            opBKKClinic.Add("18", "อาชีวคลินิก");
            opBKKClinic.Add("19", "วิสัญญีวิทยา(คลินิกระงับปวด)");
            opBKKClinic.Add("20", "ศัลยกรรมประสาท");
            opBKKClinic.Add("21", "อาชีวเวชรกรรม");
            opBKKClinic.Add("22", "เวชกรรมสังคม");
            opBKKClinic.Add("23", "พยาธิวิทยากายวิภาค");
            opBKKClinic.Add("24", "พยาธิวิทยาคลินิค");
            opBKKClinic.Add("25", "แพทย์ทางเลือก");
            opBKKClinic.Add("99", "อื่นๆ");
            //opBKKClinic.Add("00", "111111");
        }
        private void initOPBKKCHRGITEM()
        {
            opBKKCHRGITEM_CODEA.Add("21", "ค่าอวัยวะเทียมและเครื่องช่วยผู้พิการ");
            opBKKCHRGITEM_CODEA.Add("31", "ค่ายาและสารอาหารทางเส้นเลือด");
            opBKKCHRGITEM_CODEA.Add("51", "เวชภัณฑ์ที่ไม่ใช่ยา");
            opBKKCHRGITEM_CODEA.Add("61", "บริการโลหิตและส่วนประกอบของโลหิต");
            opBKKCHRGITEM_CODEA.Add("71", "ตรวจวินิจฉัยทางเทคนิคการแพทย์และพยาธิวิทยา");
            opBKKCHRGITEM_CODEA.Add("81", "ตรวจวินิจฉัยและรักษาทางรังสีวิทยา");
            opBKKCHRGITEM_CODEA.Add("91", "ตรวจวินิจฉัยโดยวิธีพิเศษอื่น ๆ");
            opBKKCHRGITEM_CODEA.Add("A1", "อุปกรณ์ของใช้และเครื่องมือทางการแพทย");
            opBKKCHRGITEM_CODEA.Add("B1", "ทำหัตถการ และบริการวิสัญญ");
            opBKKCHRGITEM_CODEA.Add("C1", "ค่าบริการทางการพยาบาล");
            opBKKCHRGITEM_CODEA.Add("D1", "บริการทางทันตกรรม");
            opBKKCHRGITEM_CODEA.Add("E1", "บริการทางกายภาพบำบัด และเวชกรรมฟื้นฟู");
            opBKKCHRGITEM_CODEA.Add("J1", "ค่าบริการอื่นๆที่ไม่เกี่ยวกับการรักษาพยาบาลโดยตรง");
            opBKKCHRGITEM_CODEA.Add("H1", "ค่าธรรมเนียมบุคลากรทางการแพทย");
            //opBKKClinic.Add("14", "แพทย์แผนไทย");
            //opBKKClinic.Add("15", "PCU ใน รพ.");
            //opBKKClinic.Add("16", "เวชกรรมปฎิบัติทั่วไป");
            //opBKKClinic.Add("17", "เวชศาสสตร์ครอบครัวและชุมชน");
            //opBKKClinic.Add("18", "อาชีวคลินิก");
            //opBKKClinic.Add("19", "วิสัญญีวิทยา(คลินิกระงับปวด)");
            //opBKKClinic.Add("20", "ศัลยกรรมประสาท");
            //opBKKClinic.Add("21", "อาชีวเวชรกรรม");
            //opBKKClinic.Add("22", "เวชกรรมสังคม");
            //opBKKClinic.Add("23", "พยาธิวิทยากายวิภาค");
            //opBKKClinic.Add("24", "พยาธิวิทยาคลินิค");
            //opBKKClinic.Add("25", "แพทย์ทางเลือก");
            //opBKKClinic.Add("99", "อื่นๆ");
            //opBKKClinic.Add("00", "111111");
        }
        public void setCboEpidemGrp(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "92";
            item.Text = "92 = กลุ่มโรค Covid-19";
            c.Items.Add(item);
            c.SelectedIndex = 0;
        }
        public void setCboMarri(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();
            //สถานะภาพการสมรส,โสด,หย่า,หม้าย
            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "1 โสด";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "2";
            item.Text = "2 สมรส";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "3";
            item.Text = "3 แยกกันอยู่";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "4";
            item.Text = "4 หม้าย";
            c.Items.Add(item);
            c.SelectedItem = item;

            item = new ComboBoxItem();
            item.Value = "5";
            item.Text = "5 หย่า";
            c.Items.Add(item);
            c.SelectedItem = item;

            item = new ComboBoxItem();
            item.Value = "6";
            item.Text = "6 สมณะ";
            c.Items.Add(item);
            c.SelectedItem = item;

            item = new ComboBoxItem();
            item.Value = "7";
            item.Text = "7 ร้าง";
            c.Items.Add(item);
            c.SelectedItem = item;

            c.SelectedIndex = 0;
        }
        public void setCboPersonStatus(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "1 หาย";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "2";
            item.Text = "2 ตาย";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "3";
            item.Text = "3 ยังรักษา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "4";
            item.Text = "4 ไม่ทราบ";
            c.Items.Add(item);
            c.SelectedItem = item;
        }
        public void setCboPersonType(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "1 หาย";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "2";
            item.Text = "2 ตาย";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "3";
            item.Text = "3 ยังรักษา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "4";
            item.Text = "4 ไม่ทราบ";
            c.Items.Add(item);
        }
        public void setCboLabResult(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "negative";
            item.Text = "negative";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "positive";
            item.Text = "positive";
            c.Items.Add(item);
            c.SelectedItem = item;
        }
        public void setCboSex(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "M";
            item.Text = "Male";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "F";
            item.Text = "Female";
            c.Items.Add(item);
        }
        public void setCboPregnant(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "Y";
            item.Text = "ตั้งครรภ์";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "N";
            item.Text = "ไม่ตั้งครรภ์";
            c.Items.Add(item);
        }
        public void setCboRespirator(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "Y";
            item.Text = "ใส่";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "N";
            item.Text = "ไม่ใส่";
            c.Items.Add(item);
            c.SelectedItem = item;
        }
        public void setCboPatientType(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "OPD";
            item.Text = "OPD";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "IPD";
            item.Text = "IPD";
            c.Items.Add(item);
            c.SelectedItem = item;
        }
        public void setCboOccupation(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "Y";
            item.Text = "Y";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "N";
            item.Text = "N";
            c.Items.Add(item);
        }
        public void setCboRick(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "Y";
            item.Text = "Y";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "N";
            item.Text = "N";
            c.Items.Add(item);
        }
        public void setCboTravel(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "Y";
            item.Text = "Y";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "N";
            item.Text = "N";
            c.Items.Add(item);
        }
        public void setCboClosed(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "Y";
            item.Text = "Y";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "N";
            item.Text = "N";
            c.Items.Add(item);
        }
        public void setCboArea(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "Y";
            item.Text = "Y";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "N";
            item.Text = "N";
            c.Items.Add(item);
        }
        public void setCboWorker(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "Y";
            item.Text = "Y";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "N";
            item.Text = "N";
            c.Items.Add(item);
            c.SelectedItem = item;
        }
        public void setCboVacine(C1ComboBox c, String selected)
        {
            c.Items.Clear();
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "Y";
            item.Text = "Y";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "N";
            item.Text = "N";
            c.Items.Add(item);
            c.SelectedItem = item;
        }
        public void setCboOPBKKINSCL(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "UCS";
            item.Text = "สิทธิหลักประกันสุขภาพ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "WEL";
            item.Text = "สิทธิหลักประกันสุขภาพ (ยกเว้นการร่วมจ่าย)";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "OFC";
            item.Text = "สิทธิข้าราชการ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "SSS";
            item.Text = "สิทธิประกันสังคม";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "LGO";
            item.Text = "สิทธิ อปท";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "SSI";
            item.Text = "สิทธิประกันสังคมทุพพลภาพ";
            c.Items.Add(item);
        }
        public void setCboSSOPOPDxCodeSet(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "IT";
            item.Text = "ICD-10";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "SN";
            item.Text = "SNOMED";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "TT";
            item.Text = "ICD-10-TM";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "...";
            item.Text = "(ยังไม่ได้กำหนด)";
            c.Items.Add(item);
        }
        public void setCboSSOPSL(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "ลำดับ1";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "2";
            item.Text = "ลำดับ2";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "3";
            item.Text = "ลำดับ3";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "4";
            item.Text = "ลำดับ4";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "5";
            item.Text = "ลำดับ5";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "6";
            item.Text = "ลำดับ6";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "7";
            item.Text = "ลำดับ7";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "8";
            item.Text = "ลำดับ8";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "9";
            item.Text = "ลำดับ9";
            c.Items.Add(item);
        }
        public void setCboSSOPCompletion(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "Y";
            item.Text = "ผู้ป่วยได้รับบริการครบแล้ว";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "N";
            item.Text = "บริการนี้ยังไม่เสร็จสมบรูณ์ ต้องมารับบริการเพื่อภายหลัง";
            c.Items.Add(item);
        }
        public void setCboSSOPCodeSet(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "IN";
            item.Text = "ICD-9-CM";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "LC";
            item.Text = "Lonic";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "TT";
            item.Text = "ICD-10-TM";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "...";
            item.Text = "(ยังไม่ได้กำหนด)";
            c.Items.Add(item);
        }
        public void setCboSSOPClinic(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "01";
            item.Text = "อายุรกรรม";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "02";
            item.Text = "ศัลยกรรม";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "03";
            item.Text = "สูติกรรม";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "04";
            item.Text = "นรีเวชกรรม";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "05";
            item.Text = "กุมารเวช";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "06";
            item.Text = "โสต ศอ นาสิก";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "07";
            item.Text = "จักษุ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "08";
            item.Text = "ศัลยกรรมกระดูก";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "09";
            item.Text = "จิตเวช";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "10";
            item.Text = "รังสีวิทยา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "11";
            item.Text = "ทันตกรรม";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "12";
            item.Text = "ฉุกเฉิน";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "99";
            item.Text = "อื่นๆ";
            c.Items.Add(item);
        }
        public void setCboSSOPTypeOut(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "จำหน่ายกลับบ้าน";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "2";
            item.Text = "รับเข้าเป็นผู้ป่วยใน";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "3";
            item.Text = "ส่งต่อไปสถานพยาบาลอื่น";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "4";
            item.Text = "เสียชีวิต";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "5";
            item.Text = "หนีกลับ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "9";
            item.Text = "อื่นๆ";
            c.Items.Add(item);
        }
        public void setCboSSOPTypeIn(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "เข้ารับบริการเอง";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "2";
            item.Text = "เข้ารับบริการตามนัดหมาย";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "3";
            item.Text = "รับส่งต่อมาจากสถานพยาบาลอื่น";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "4";
            item.Text = "เข้ารับบริการแบบฉุกเฉิน";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "5";
            item.Text = "อื่นๆ";
            c.Items.Add(item);
        }
        public void setCboSSOPTypeServ(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "01";
            item.Text = "พบแพทย์เพื่อวินิจฉัยโรคครั้งแรก";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "02";
            item.Text = "พบแพทย์ตามนัดเพื่อติดตามการรักษาโรคทั่วไป";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "03";
            item.Text = "พบแพทย์ตามนัดเพื่อติดตามการรักษาโรครื้อรัง";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "04";
            item.Text = "ปรึกษาแพทย์(Consultation)";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "05";
            item.Text = "รับบริการกรณีฉุกเฉิน";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "06";
            item.Text = "ตรวจสุขภาพทั่วไป";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "07";
            item.Text = "ตรวจวินิจฉัยทางรังสีวิทยา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "08";
            item.Text = "รับรองอบรมความรู้ในการปฏิบัติตัวสำหรับผู้ป่วยโรคเรื้อรัง";
            c.Items.Add(item);
        }
        public void setCboSSOPCareAccount(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "สถานพยาบาลหลัก";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "2";
            item.Text = "สถานพยาบาล Supra";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "3";
            item.Text = "สถานพยาบาลเครือข่าย";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "9";
            item.Text = "สถานพยาบาลอื่นๆ";
            c.Items.Add(item);
        }
        public void setCboSSOPClass(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "OP";
            item.Text = "หัตถการ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "EC";
            item.Text = "การตรวจรักษา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "LB";
            item.Text = "LAB";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "XR";
            item.Text = "การตรวจวินิจฉัยและรักษาทางรังสีวิทยา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "IV";
            item.Text = "การตรวจวินิจฉัยด้วยวิธีพิเศษอื่นๆ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "ZZ";
            item.Text = "อื่นๆที่ยังไม่ได้กำหนด";
            c.Items.Add(item);
        }
        public void setCboSSOPClaimcont(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "OD";
            item.Text = "ไม่ได้กำหนดเงื่อนไข";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "NR";
            item.Text = "เบิกไม่ได้";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "PA";
            item.Text = "ขออนุมัติก่อนเบิก";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "AU";
            item.Text = "ตรวจหลังการเบิก";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "ST";
            item.Text = "ใช้ได้ตามลำดับ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "IN";
            item.Text = "มีข้อบ่งชี้กำกับ";
            c.Items.Add(item);
        }
        public void setCboSSOPPrdSeCode(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "0";
            item.Text = "ไม่ต้องจัดยาแทน";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "ผู้สั่งไม่ให้จัดยาแทน";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "2";
            item.Text = "ให้แทนได้, ผู้ป่วยขอเลือกยาต้นแบบ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "3";
            item.Text = "ให้แทนได้, เภสัชกรเป็นผู้เลือกยาต้นแบบ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "4";
            item.Text = "ให้แทนได้, แต่ยาแทนขาดตรวจจากห้องยา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "5";
            item.Text = "ให้แทนได้, ยาต้นแบบถูกใช้เป้นยาแทน";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "6";
            item.Text = "การแทนยาถูกกำหนดจากที่อื่น";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "7";
            item.Text = "ไม่ให้จัดแทน, มีกฎข้อบังคับให้ใช้ยาชื่อนี้";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "8";
            item.Text = "ให้แทนได้, แต่ยาแทนขาดตลาดไป";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "9";
            item.Text = "เหตุผลอื่น";
            c.Items.Add(item);
        }
        public void setCboSSOPPrdCat(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "ยาแผนปัจจุบันทางการค้า";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "2";
            item.Text = "ยาแผนปัจจุบันผลิตเอง";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "3";
            item.Text = "ยาแผนไทยทางการค้า";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "4";
            item.Text = "ยาแผนไทยผลิตเอง";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "5";
            item.Text = "ยาแผนการรักษาทางเลือกอื่นๆ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "6";
            item.Text = "เวชภัณฑ์";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "7";
            item.Text = "อื่นๆ";
            c.Items.Add(item);
        }
        public void setCboSSOPDispeStat(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "0";
            item.Text = "ยกเลิก,ไม่มีการจ่ายยา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "รับยาแล้ว";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "2";
            item.Text = "แก้ไข,คืนยาหรือเปลี่ยนยา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "3";
            item.Text = "แก้ไข,เบิกผิดพลาด";
            c.Items.Add(item);
        }
        public void setCboSSOPBenefitplan(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "NB";
            item.Text = "พรบ.ผู้ประสบภัยจากรถฯ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "CS";
            item.Text = "ประกันชีวิตส่วนบุคคล";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "UC";
            item.Text = "นายจ้าง/บริษัทเอกชน";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "SS";
            item.Text = "หน่วยบริการส่งต่อ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "GO";
            item.Text = "สถานภยาบาลยกเว้นค่ารักษาพยาบาล";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "GE";
            item.Text = "อื่นๆ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "RT";
            item.Text = "พ.ร.บ.ผู้ประสบภัยจากรถฯ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "PI";
            item.Text = "ประกันชีวิตส่วนบุคคล";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "บริษัท/นายจ้างเอกชน";
            item.Text = "อื่นๆ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "RF";
            item.Text = "หน่วยบริการส่งต่อ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "SO";
            item.Text = "หน่วยอื่นๆของสถานพยาบาล";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "ZZ";
            item.Text = "อื่นๆ";
            c.Items.Add(item);
        }
        public void setCboSSOPOtherPayPlan(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RT";
            item.Text = "พรบ.ผู้ประสบภัยจากรถฯ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "PI";
            item.Text = "ประกันชีวิตส่วนบุคคล";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "EM";
            item.Text = "นายจ้าง/บริษัทเอกชน";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "RF";
            item.Text = "หน่วยบริการส่งต่อ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "SH";
            item.Text = "สถานภยาบาลยกเว้นค่ารักษาพยาบาล";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "ZZ";
            item.Text = "อื่นๆ";
            c.Items.Add(item);
        }
        public void setCboSSOPPayPlan(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "00";
            item.Text = "ไม่ใช้สิทธิ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "10";
            item.Text = "ใช้สิทธิสวัสดิการข้าราชการ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "20";
            item.Text = "ใช้สิทธิ ขรก. ส่วนท้องถิ่น";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "30";
            item.Text = "ใช้สิทธิ องค์กรอื่นๆ ของรัฐ เบิกกสทช";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "31";
            item.Text = "ใช้มิทธิ องค์กรอื่นๆ ของรัฐ เบิกกกต";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "80";
            item.Text = "ใช้สิทธิประกันสังคม";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "81";
            item.Text = "ใช้สิทธิกองทุนเงินทดแทน";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "86";
            item.Text = "ทุพลภาพ";
            c.Items.Add(item);
        }
        public void setCboSSOPTFlag(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "A";
            item.Text = "เป็นรายการขอเบิก";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "E";
            item.Text = "แก้ไขรายการ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "D";
            item.Text = "ยกเลิกรายการ";
            c.Items.Add(item);
        }
        public void setCboSSOPBillmuad(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "1";
            item.Text = "ค่าห้อง และค่าอาหาร";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "2";
            item.Text = "ค่าอวัยวะเทียว และอุปกรณ์ในการบำบัด";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "3";
            item.Text = "ค่ายา และสารอาหาร";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "5";
            item.Text = "ค่าเวชภัณฑ์ที่มิใช่ยา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "6";
            item.Text = "ค่าบริการโลหิต และส่วนประกอบของโลหิต";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "7";
            item.Text = "ค่าตรวจวินิจฉัยทางเทคนิคการแพทย์";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "8";
            item.Text = "ค่าตรวจวินิจฉัย และรักษาทางรังสีวิทยา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "9";
            item.Text = "ค่าตรวจวินิจฉัยโดยวิธีพิเศษอื่นๆ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "A";
            item.Text = "ค่าอุปกรณ์ของใช้ และเครื่องมือฯ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "B";
            item.Text = "ค่าทำหัตถการ และวิสัญญี";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "C";
            item.Text = "ค่าบริการทางการพยาบาล";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "D";
            item.Text = "ค่าบริการทางทันตกรรม";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "E";
            item.Text = "ค่าบริการทางกายภาพบำบัด";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "F";
            item.Text = "ค่าบริการฝังเข็มและค่าบริการการให้การบำบัดของผู้ประกอบโรคศิลปะอื่นๆ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "G";
            item.Text = "ค่าบริการอื่นๆ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "H";
            item.Text = "ค่าห้องผ่าตัด/ห้องคลอด";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "I";
            item.Text = "ค่าธรรมเนียมบุคลากรทางการแพทย์";
            c.Items.Add(item);
        }
        public void setCboSSOPClaimCat(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "OP1";
            item.Text = "OPD ปกติ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "OP...";
            item.Text = "OPD อื่นๆ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "RPT";
            item.Text = "ไตวายเรื้อรัง";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "P01";
            item.Text = "OCPA";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "P02";
            item.Text = "RDPA";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "P03";
            item.Text = "DDPA";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "REF";
            item.Text = "ส่งต่อ";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "EM1";
            item.Text = "ฉุกเฉิน";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "EM2";
            item.Text = "ฉุกเฉินระยะทาง";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "OPF";
            item.Text = "เบิกเพิ่มแบบเหมาจ่าย";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "OPR";
            item.Text = "เบิกเพิ่มตามอัตรา";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "XX...";
            item.Text = "บัญชี...ต่างๆ";
            c.Items.Add(item);
        }
        public void setCboOPBKKCHRGITEM_CODEA(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();

            foreach (KeyValuePair<string, string> entry in opBKKCHRGITEM_CODEA)
            {
                item = new ComboBoxItem();
                item.Value = entry.Key;
                item.Text = entry.Value;
                c.Items.Add(item);
            }
        }
        public void setCboOPBKKClinic(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();

            foreach (KeyValuePair<string, string> entry in opBKKClinic)
            {
                item = new ComboBoxItem();
                item.Value = entry.Key;
                item.Text = entry.Value;
                c.Items.Add(item);
            }
        }
        public String convertExcelColName(String col)
        {
            int ret = 0;

            ret = Convert.ToChar(col);
            ret = ((int)ret) - 65;

            return ret.ToString();
        }
        public String getPrefix(String prefixname)
        {
            String re = "";
            if (prefixname.Trim().Equals("นาย"))
            {
                re = "01";
            }
            else if (prefixname.Trim().Equals("นาง"))
            {
                re = "02";
            }
            else if (prefixname.Trim().Equals("น.ส."))
            {
                re = "02";
            }
            return re;
        }
        public String getMonth1(String month)
        {
            if (month == "ม.ค.")
            {
                return "01";
            }
            else if (month == "ก.พ.")
            {
                return "02";
            }
            else if (month == "มี.ค.")
            {
                return "03";
            }
            else if (month == "เม.ย.")
            {
                return "04";
            }
            else if (month == "พ.ค.")
            {
                return "05";
            }
            else if (month == "มิ.ย.")
            {
                return "06";
            }
            else if (month == "ก.ค.")
            {
                return "07";
            }
            else if (month == "ส.ค.")
            {
                return "08";
            }
            else if (month == "ก.ย.")
            {
                return "09";
            }
            else if (month == "ต.ค.")
            {
                return "10";
            }
            else if (month == "พ.ย.")
            {
                return "11";
            }
            else if (month == "ธ.ค.")
            {
                return "12";
            }
            else
            {
                return "";
            }
        }
        public String getMonth(String monthId)
        {
            if (monthId == "01")
            {
                return "มกราคม";
            }
            else if (monthId == "02")
            {
                return "กุมภาพันธ์";
            }
            else if (monthId == "03")
            {
                return "มีนาคม";
            }
            else if (monthId == "04")
            {
                return "เมษายน";
            }
            else if (monthId == "05")
            {
                return "พฤษภาคม";
            }
            else if (monthId == "06")
            {
                return "มิถุนายน";
            }
            else if (monthId == "07")
            {
                return "กรกฎาคม";
            }
            else if (monthId == "08")
            {
                return "สิงหาคม";
            }
            else if (monthId == "09")
            {
                return "กันยายน";
            }
            else if (monthId == "10")
            {
                return "ตุลาคม";
            }
            else if (monthId == "11")
            {
                return "พฤศจิกายน";
            }
            else if (monthId == "12")
            {
                return "ธันวาคม";
            }
            else
            {
                return "";
            }
        }
        public String getMonthNameTHShort(String monthId)
        {
            if (monthId == "01")
            {
                return "ม.ค.";
            }
            else if (monthId == "02")
            {
                return "ก.พ.";
            }
            else if (monthId == "03")
            {
                return "ม.ค.";
            }
            else if (monthId == "04")
            {
                return "เม.ย";
            }
            else if (monthId == "05")
            {
                return "พ.ค.";
            }
            else if (monthId == "06")
            {
                return "มิ.ย.";
            }
            else if (monthId == "07")
            {
                return "ก.ค.";
            }
            else if (monthId == "08")
            {
                return "ส.ค.";
            }
            else if (monthId == "09")
            {
                return "ก.ย.";
            }
            else if (monthId == "10")
            {
                return "ต.ค.";
            }
            else if (monthId == "11")
            {
                return "พ.ย.";
            }
            else if (monthId == "12")
            {
                return "ธ.ค.";
            }
            else
            {
                return "";
            }
        }
        public C1ComboBox setCboMonth(C1ComboBox c)
        {
            c.Items.Clear();
            var items = new[]{
                new{Text = "มกราคม", Value="01"},
                new{Text = "กุมภาพันธ์", Value="02"},
                new{Text = "มีนาคม", Value="03"},
                new{Text = "เมษายน", Value="04"},
                new{Text = "พฤษภาคม", Value="05"},
                new{Text = "มิถุนายน", Value="06"},
                new{Text = "กรกฎาคม", Value="07"},
                new{Text = "สิงหาคม", Value="08"},
                new{Text = "กันยายน", Value="09"},
                new{Text = "ตุลาคม", Value="10"},
                new{Text = "พฤศจิกายน", Value="11"},
                new{Text = "ธันวาคม", Value="12"}
            };
            ComboBoxItem item = new ComboBoxItem();

            item = new ComboBoxItem();
            item.Value = "01";
            item.Text = "มกราคม";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "02";
            item.Text = "กุมภาพันธ์";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "03";
            item.Text = "มีนาคม";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "04";
            item.Text = "เมษายน";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "05";
            item.Text = "พฤษภาคม";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "06";
            item.Text = "มิถุนายน";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "07";
            item.Text = "กรกฎาคม";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "08";
            item.Text = "สิงหาคม";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "09";
            item.Text = "กันยายน";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "10";
            item.Text = "ตุลาคม";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "11";
            item.Text = "พฤศจิกายน";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "12";
            item.Text = "ธันวาคม";
            c.Items.Add(item);

            c.SelectedItem = System.DateTime.Now.Month.ToString("00");
            return c;
        }
        public ComboBox setCboMonth(ComboBox c)
        {
            c.Items.Clear();
            var items = new[]{
                new{Text = "มกราคม", Value="01"},
                new{Text = "กุมภาพันธ์", Value="02"},
                new{Text = "มีนาคม", Value="03"},
                new{Text = "เมษายน", Value="04"},
                new{Text = "พฤษภาคม", Value="05"},
                new{Text = "มิถุนายน", Value="06"},
                new{Text = "กรกฎาคม", Value="07"},
                new{Text = "สิงหาคม", Value="08"},
                new{Text = "กันยายน", Value="09"},
                new{Text = "ตุลาคม", Value="10"},
                new{Text = "พฤศจิกายน", Value="11"},
                new{Text = "ธันวาคม", Value="12"}
            };
            c.DataSource = items;
            c.DisplayMember = "Text";

            c.ValueMember = "Value";
            c.SelectedIndex = c.FindStringExact(getMonth(System.DateTime.Now.Month.ToString("00")));
            return c;
        }
        public void setCboMOdality(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectAll();
            int i = 0;

            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "CR";
            item.Text = "Computed Radiography";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "CT";
            item.Text = "Computed Tomography";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "MR";
            item.Text = "Magnetic Resonance";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "NR";
            item.Text = "Nuclear Medicine";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "UA";
            item.Text = "Ultrasound";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "OT";
            item.Text = "Other";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "BI";
            item.Text = "Biomagnetic Imaging";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "CD";
            item.Text = "Color Flow Doppler";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "DD";
            item.Text = "Duplex Doppler";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "DG";
            item.Text = "Diaphanography";
            c.Items.Add(item);

            item = new ComboBoxItem();
            item.Value = "ES";
            item.Text = "Endoscopy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "LS";
            item.Text = "Laser Surface Scan";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "PT";
            item.Text = "Positron Emission Tomography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RG";
            item.Text = "Radiographic Imaging";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "ST";
            item.Text = "Single-photon Emission Computed Tomography (SPECT)";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "TG";
            item.Text = "Thermography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "XA";
            item.Text = "X-Ray Angiography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RF";
            item.Text = "Radio Fluoroscopy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTIMAGE";
            item.Text = "Radiotherapy Image";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTDOSE";
            item.Text = "Radiotherapy Dose";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTSTRUCT";
            item.Text = "RadioTherapy Structure Set";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTPLAN";
            item.Text = "Radiotherapy Plan";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "RTRECORD";
            item.Text = "Radiotherapy Treatment Record";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "HC";
            item.Text = "Hard Copy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "DX";
            item.Text = "Digital Radiography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "MG";
            item.Text = "Mammography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "IO";
            item.Text = "Intra-oral Radiography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "PX";
            item.Text = "Panoramic X-Ray";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "GM";
            item.Text = "General Microscopy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SM";
            item.Text = "Slide Microscopy";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "XC";
            item.Text = "External-camera Photography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "PR";
            item.Text = "Presentation State";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "AU";
            item.Text = "Audio";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "ECG";
            item.Text = "Electrocardiography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "EPS";
            item.Text = "Cardiac Electrophysiology";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "HD";
            item.Text = "Hemodynamic Waveform";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SR";
            item.Text = "Structured Report Document";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "IVUS";
            item.Text = "Intravascular Ultrasound";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "OP";
            item.Text = "Ophthalmic Photography";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SMR";
            item.Text = "Stereometric Relationship";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SC";
            item.Text = "Secondary Capture";
            c.Items.Add(item);
            item = new ComboBoxItem();
            item.Value = "SD";
            item.Text = "Scanned Document";
            c.Items.Add(item);
        }
        public C1ComboBox setCboYear(C1ComboBox c)
        {
            c.Items.Clear();
            c.Items.Add(System.DateTime.Now.Year + 543);
            c.Items.Add(System.DateTime.Now.Year + 543 - 1);
            c.Items.Add(System.DateTime.Now.Year + 543 - 2);
            c.SelectedIndex = 0;
            return c;
        }
        public ComboBox setCboYear(ComboBox c)
        {
            c.Items.Clear();
            c.Items.Add(System.DateTime.Now.Year + 543);
            c.Items.Add(System.DateTime.Now.Year + 543 - 1);
            c.Items.Add(System.DateTime.Now.Year + 543 - 2);
            c.SelectedIndex = c.FindStringExact(String.Concat(System.DateTime.Now.Year + 543));
            return c;
        }
        public ComboBox setCboPeriod(ComboBox c)
        {
            c.Items.Clear();
            c.Items.Add(1);
            c.Items.Add(2);

            c.SelectedIndex = 0;
            return c;
        }
        public String shortPaidName(String name)
        {
            if (name == "ประกันสังคม (บ.1)")
            {
                return "ปกส(บ.1)";
            }
            else if (name == "ประกันสังคม (บ.2)")
            {
                return "ปกส(บ.2)";
            }
            else if (name == "ประกันสังคม (บ.5)")
            {
                return "ปกส(บ.5)";
            }
            else if (name == "ประกันสังคมอิสระ (บ.1)")
            {
                return "ปกต(บ.1)";
            }
            else if (name == "ประกันสังคมอิสระ (บ.5)")
            {
                return "ปกต(บ.5)";
            }
            else if (name == "ตรวจสุขภาพ (เงินสด)")
            {
                return "ตส(เงินสด)";
            }
            else if (name == "ตรวจสุขภาพ (บริษัท)")
            {
                return "ตส(บริษัท)";
            }
            else if (name == "ตรวจสุขภาพ (PACKAGE)")
            {
                return "ตส(PACKAGE)";
            }
            else if (name == "ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ")
            {
                return "ลูกหนี้(ปากน้ำ)";
            }
            else if (name == "ลูกหนี้บางนา 1")
            {
                return "ลูกหนี้(บ.1)";
            }
            else if (name == "บริษัทประกัน")
            {
                return "บ.ประกัน";
            }
            else if (name == "ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปู่เจ้า")
            {
                return "ลูกหนี้(ปู่เจ้า)";
            }
            else
            {
                return name;
            }
        }
        public String dateDBtoShowShort(String dt)
        {
            if (dt != "")
            {
                if (Int16.Parse(dt.Substring(0, 4)) < 1957)
                {
                    return dt.Substring(8, 2) + "-" + dt.Substring(5, 2) + "-" + String.Concat(Int16.Parse(dt.Substring(0, 4)) + 543);
                }
                else
                {
                    return dt.Substring(8, 2) + "-" + dt.Substring(5, 2) + "-" + String.Concat(Int16.Parse(dt.Substring(0, 4)) + 543).Substring(2);
                }

            }
            else
            {
                return dt;
            }
        }
        public String FormatTime(String t)
        {
            String aa = "";
            aa = "0000" + t;
            if (aa.Length >= 4)
            {
                aa = aa.Substring(aa.Length - 4, 2) + ":" + aa.Substring(aa.Length - 2);
            }
            return aa;
        }
        public String dateDBtoShowShort1(String dt)
        {
            if (dt != "")
            {
                if (Int16.Parse(dt.Substring(0, 4)) < 1957)
                {
                    return dt.Substring(8, 2) + "-" + dt.Substring(5, 2) + "-" + String.Concat(Int16.Parse(dt.Substring(0, 4)) + 543);
                }
                else
                {
                    return dt.Substring(8, 2) + "-" + dt.Substring(5, 2) + "-" + String.Concat(Int16.Parse(dt.Substring(0, 4)) + 543).Substring(2);
                }

            }
            else
            {
                return dt;
            }
        }
        public Size MeasureString(Control c)
        {
            return TextRenderer.MeasureText(c.Text, c.Font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.SingleLine | TextFormatFlags.NoClipping | TextFormatFlags.PreserveGraphicsClipping);
        }
        public Size MeasureString(String txt, Font font)
        {
            return TextRenderer.MeasureText(txt, font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.SingleLine | TextFormatFlags.NoClipping | TextFormatFlags.PreserveGraphicsClipping);
        }
        public Bitmap ResizeImage(Image image, int width, int height)
        {
            if (image == null) return null;
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
        public Bitmap ResizeImagetoA42(Bitmap image, int maxWidth, int maxHeight)
        {

            int quality = 100;
            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);


            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);


            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Jpeg);

            // Create an Encoder object for the Quality parameter.
            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);


            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;
            //newImage.Save(filePath, imageCodecInfo, encoderParameters);

            return newImage;
        }
        public Bitmap ResizeImagetoA41(Image image, int width)
        {
            /*
             * 
             */
            float plus = 3.8f;
            float plush = 1.4f;
            float tgtWidthMM = 210;  //A4 paper size
            float tgtHeightMM = 297;
            float tgtWidthInches = tgtWidthMM / 25.4f;
            float tgtHeightInches = tgtHeightMM / 25.4f;
            float srcWidthPx = image.Width;
            float srcHeightPx = image.Height;
            float dpiX = srcWidthPx / tgtWidthInches;
            float dpiY = srcHeightPx / tgtHeightInches;
            //int width = 900;
            double ratio = (double)image.Width / (double)image.Height;
            int height = Convert.ToInt32(width / ratio);

            if (height > width)
            {
                ratio = (double)image.Height / (double)image.Width;
                height = width;
                width = Convert.ToInt32(height / ratio);
            }

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public Bitmap ResizeImagetoA4(Image image)
        {
            /*
             * 
             */
            float tgtWidthMM = 210;  //A4 paper size
            float tgtHeightMM = 297;
            float tgtWidthInches = tgtWidthMM / 25.4f;
            float tgtHeightInches = tgtHeightMM / 25.4f;
            float srcWidthPx = image.Width;
            float srcHeightPx = image.Height;
            float dpiX = srcWidthPx / tgtWidthInches;
            float dpiY = srcHeightPx / tgtHeightInches;

            var destRect = new Rectangle(0, 0, 210, 297);
            var destImage = new Bitmap(210, 297);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public Bitmap ResizeImagetoA4Lan(Image image)
        {
            /*
             * 
             */
            int ww = 697, hh = 610;

            Image aaa = RotateImage(image);
            float tgtWidthMM = 297;  //A4 paper size
            float tgtHeightMM = 210;
            float tgtWidthInches = tgtWidthMM / 25.4f;
            float tgtHeightInches = tgtHeightMM / 25.4f;
            //float srcWidthPx = aaa.Width;
            //float srcHeightPx = aaa.Height;
            float srcWidthPx = aaa.Height;
            float srcHeightPx = aaa.Width;
            float dpiX = srcWidthPx / tgtWidthInches;
            float dpiY = srcHeightPx / tgtHeightInches;

            var destRect = new Rectangle(0, 0, ww, hh - 100);
            var destImage = new Bitmap(ww, hh - 100);
            destImage.SetResolution(aaa.HorizontalResolution, aaa.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(aaa, destRect, 0, 0, aaa.Width, aaa.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public bool ExploreFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }
            //Clean up file path so it can be navigated OK
            filePath = System.IO.Path.GetFullPath(filePath);
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            return true;
        }
        public void serverPACsInfinittStart(TcpListener tcpServerListener, String ipaddress, String port)
        {
            try
            {
                System.Net.IPAddress ipad = System.Net.IPAddress.Parse(ipaddress);
                tcpServerListener = new TcpListener(ipad, int.Parse(port));

                tcpServerListener.Start();

                Socket socket = tcpServerListener.AcceptSocket();
                byte[] b = new byte[100];
                int k = socket.Receive(b);

            }
            catch (Exception ex)
            {

            }
        }
        public PACsMSH genPAsMSH(String reqdept, String MessageType)
        {
            PACsMSH msh = new PACsMSH();
            msh.FieldSeparator = "";
            msh.EncodingCharacters = "^~\\&";
            msh.SendingApplication = "HIS";
            msh.SendingFacility = reqdept;
            msh.ReceivingApplication = "ThaiGL";
            msh.ReceivingFacility = "";
            msh.DateTimeOfMessage = DateTime.Now.Year + DateTime.Now.ToString("MMddHHmmss");
            msh.Security = "";
            //msh.MessageType = "ADT^A08";
            msh.MessageType = MessageType;
            msh.MessageControlID = "331950";
            msh.ProcessingID = "P";
            msh.VersionID = "2.3";
            msh.SequenceNumber = "";
            msh.ContinuationPointer = "";
            msh.AcceptAcknowledgementType = "";
            msh.ApplicationAcknowledgementType = "";
            msh.CountryCode = "";
            msh.CharacterSet = "";
            msh.PrincipalLanguageOfMessage = "";
            msh.AlternateCharacterSetHandlingScheme = "UNICODE UTF-8";

            return msh;
        }
        public PACsEVN genPACsEVN()
        {
            PACsEVN evn = new PACsEVN();
            evn.EventTypeCode = "";
            evn.RecordedDateTime = DateTime.Now.Year + DateTime.Now.ToString("MMddHHmmss");
            evn.DateTimePlannedEvent = "";
            evn.EventReasonCode = "";
            evn.OperatorID = "";
            evn.EventOccurred = DateTime.Now.Year + DateTime.Now.ToString("MMddHHmmss");

            return evn;
        }
        public PACsPID genPACsPID(String hn, String pttprefix, String pttfirstname, String pttlastame, String dob, String sex, String nation)
        {
            PACsPID pid = new PACsPID();
            pid = new PACsPID();
            pid.SetID = "";
            pid.PatientID = "";
            pid.PatientIdentifierList = hn;
            pid.AlternatePatientID = "";
            pid.PatientName = pttprefix + "^" + pttfirstname + "^^^" + pttlastame;
            pid.MothersMaidenName = "";
            pid.DateTimeOfBirth = dob;
            pid.Sex = sex;
            pid.PatientAlias = "";
            pid.Race = "";
            pid.PatientAddress = "";
            pid.CountyCode = "";
            pid.HomePhoneNumber = "";
            pid.BusinessPhoneNumber = "";
            pid.PrimaryLanguage = "";
            pid.MaritalStatus = "";
            pid.Religion = "";
            pid.PatientAccountNumber = "";
            pid.SSNNumber = "";
            pid.DriversLicenseNumber = "";
            pid.MothersIdentifier = "";
            pid.EthnicGroup = "";
            pid.BirthPlace = "";
            pid.MultipleBirthIndicator = "";
            pid.BirthOrder = "";
            pid.Citizenship = "";
            pid.VeteransMilitaryStatus = "";
            pid.Nationality = nation;
            pid.PatientDeathDateAndTime = "";
            pid.PatientDeathIndicator = "";

            return pid;
        }
        public PACsPV1 genPV1(String opdtype, String depcode, String depname)
        {
            PACsPV1 pv1 = new PACsPV1();
            pv1.SetID = "";
            pv1.PatientClass = opdtype;
            pv1.AssignedPatientLocation = depcode + "^" + depname;
            pv1.AdmissionType = "";
            pv1.PreadmitNumber = "";
            pv1.PriorPatientLocation = "";
            pv1.AttendingDoctor = "";
            pv1.ReferringDoctor = "";
            pv1.ConsultingDoctor = "";
            pv1.HospitalService = "";
            pv1.TemporaryLocation = "";
            pv1.PreadmitTestIndicator = "";
            pv1.ReadmissionIndicator = "";
            pv1.AdmitSource = "";
            pv1.AmbulatoryStatus = "";
            pv1.VIPIndicator = "";
            pv1.AdmittingDoctor = "";
            pv1.PatientType = "";
            pv1.VisitNumber = "";
            pv1.FinancialClass = "";
            pv1.ChargePriceIndicator = "";
            pv1.CourtesyCode = "";
            pv1.CreditRating = "";
            pv1.ContractCode = "";
            pv1.ContractEffectiveDate = "";
            pv1.ContractAmount = "";
            pv1.ContractPeriod = "";
            pv1.InterestCode = "";
            pv1.TransferToBadDebtCode = "";
            pv1.TransferToBadDebtDate = "";
            pv1.BadDebtAgencyCode = "";
            pv1.BadDebtTransferAmount = "";
            pv1.BadDebtRecoveryAmount = "";
            pv1.DeleteAccountIndicator = "";
            pv1.DeleteAccountDate = "";
            pv1.DischargeDisposition = "";
            pv1.DischargedToLocation = "";
            pv1.DietType = "";
            pv1.ServicingFacility = "";
            pv1.BedStatus = "";
            pv1.AccountStatus = "";
            pv1.PendingLocation = "";
            pv1.PriorTemporaryLocation = "";
            pv1.AdmitDateTime = "";
            pv1.DischargeDateTime = "";
            pv1.CurrentPatientBalance = "";
            pv1.TotalCharges = "";
            pv1.TotalAdjustments = "";
            pv1.TotalPayments = "";
            pv1.AlternateVisitID = "";
            pv1.VisitIndicator = "";
            pv1.OtherHealthcareProvider = "";

            return pv1;
        }
        public String PACsADT(PACsMSH msh, PACsEVN evn, PACsPID pid, PACsPV1 pv1)
        {
            String txt = "", MSH = "", separate = "|", EVN = "", PID = "", PV1 = "";
            txt = "\x0b";

            MSH = "MSH" + separate + msh.EncodingCharacters + separate + msh.SendingApplication + separate + msh.SendingFacility + separate + msh.ReceivingApplication
                + separate + msh.ReceivingFacility + separate + msh.DateTimeOfMessage + separate + msh.Security + separate + msh.MessageType
                + separate + msh.MessageControlID + separate + msh.ProcessingID + separate + msh.VersionID + separate + msh.SequenceNumber
                + separate + msh.ContinuationPointer + separate + msh.AcceptAcknowledgementType + separate + msh.ApplicationAcknowledgementType + separate + msh.CountryCode
                + separate + msh.CharacterSet + separate + msh.PrincipalLanguageOfMessage + separate + msh.AlternateCharacterSetHandlingScheme;

            EVN = "EVN" + separate + evn.EventTypeCode + separate + evn.RecordedDateTime + separate + evn.DateTimePlannedEvent + separate + evn.EventReasonCode
                + separate + evn.OperatorID + separate + evn.EventOccurred;

            PID = "PID" + separate + pid.SetID + separate + pid.PatientID + separate + pid.PatientIdentifierList + separate + pid.AlternatePatientID
                + pid.PatientName + separate + pid.MothersMaidenName + separate + pid.DateTimeOfBirth + separate + pid.Sex
                + pid.PatientAlias + separate + pid.Race + separate + pid.PatientAddress + separate + pid.CountyCode
                + pid.HomePhoneNumber + separate + pid.BusinessPhoneNumber + separate + pid.PrimaryLanguage + separate + pid.MaritalStatus
                + pid.Religion + separate + pid.PatientAccountNumber + separate + pid.SSNNumber + separate + pid.DriversLicenseNumber
                + pid.MothersIdentifier + separate + pid.EthnicGroup + separate + pid.BirthPlace + separate + pid.MultipleBirthIndicator
                + pid.BirthOrder + separate + pid.Citizenship + separate + pid.VeteransMilitaryStatus + separate + pid.Nationality
                + pid.PatientDeathDateAndTime + separate + pid.PatientDeathIndicator;

            PV1 = "PV1" + separate + pv1.SetID + separate + pv1.PatientClass + separate + pv1.AssignedPatientLocation + separate + pv1.AdmissionType
                + separate + pv1.PreadmitNumber + separate + pv1.PriorPatientLocation + separate + pv1.AttendingDoctor + separate + pv1.ReferringDoctor
                + separate + pv1.ConsultingDoctor + separate + pv1.HospitalService + separate + pv1.TemporaryLocation + separate + pv1.PreadmitTestIndicator
                + separate + pv1.ReadmissionIndicator + separate + pv1.AdmitSource + separate + pv1.AmbulatoryStatus + separate + pv1.VIPIndicator
                + separate + pv1.AdmittingDoctor + separate + pv1.PatientType + separate + pv1.VisitNumber + separate + pv1.FinancialClass
                + separate + pv1.ChargePriceIndicator + separate + pv1.CourtesyCode + separate + pv1.CreditRating + separate + pv1.ContractCode
                + separate + pv1.ContractEffectiveDate + separate + pv1.ContractAmount + separate + pv1.ContractPeriod + separate + pv1.InterestCode
                + separate + pv1.TransferToBadDebtCode + separate + pv1.TransferToBadDebtDate + separate + pv1.BadDebtAgencyCode + separate + pv1.BadDebtTransferAmount
                + separate + pv1.BadDebtRecoveryAmount + separate + pv1.DeleteAccountIndicator + separate + pv1.DeleteAccountDate + separate + pv1.DischargeDisposition
                + separate + pv1.DischargedToLocation + separate + pv1.DietType + separate + pv1.ServicingFacility + separate + pv1.BedStatus
                + separate + pv1.AccountStatus + separate + pv1.PendingLocation + separate + pv1.PriorTemporaryLocation + separate + pv1.AdmitDateTime
                + separate + pv1.DischargeDateTime + separate + pv1.CurrentPatientBalance + separate + pv1.TotalCharges + separate + pv1.TotalAdjustments
                + separate + pv1.TotalPayments + separate + pv1.AlternateVisitID + separate + pv1.VisitIndicator + separate + pv1.OtherHealthcareProvider;
            txt = txt + MSH + Environment.NewLine + EVN + Environment.NewLine + PID + Environment.NewLine + PV1;
            return txt;
        }
        public String genADT(String reqdept, String hn, String pttprefix, String pttfirstname, String pttlastame, String dob, String sex, String nation, String opdtype, String depcode, String depname)
        {
            String txt = "";
            PACsMSH msh = new PACsMSH();
            PACsPID pid = new PACsPID();
            PACsEVN evn = new PACsEVN();
            PACsPV1 pv1 = new PACsPV1();
            msh = genPAsMSH(reqdept, "ADT^A08");
            pid = genPACsPID(hn, pttprefix, pttfirstname, pttlastame, dob, sex, nation);
            pv1 = genPV1(opdtype, depcode, depname);
            evn = genPACsEVN();

            txt = PACsADT(msh, evn, pid, pv1);
            return txt;
        }
        public PACsORC genORC(String xrayyear, String reqno, String xraycode, String userid, String username)
        {
            String code = "";
            code = bcDB.xrDB.selectPACsInfinittCode(xraycode);
            PACsORC orc = new PACsORC();
            orc.OrderControl = "NW";
            orc.PlacerOrderNumber = xrayyear + reqno + code;
            orc.FillerOrderNumber = "";
            orc.PlacerGroupNumber = "";
            orc.OrderStatus = "SC";
            orc.ResponseFlag = "";
            orc.QuantityTiming = "";
            orc.Parent = "";

            orc.DateTimeOfTransaction = DateTime.Now.Year + DateTime.Now.ToString("MMddHHmmss");
            orc.EnteredBy = userid + "^" + username;
            orc.VerifiedBy = "";
            orc.OrderingProvider = userid + "^" + username;
            orc.EnterersLocation = "";
            orc.CallBackPhoneNumber = "";
            orc.OrderEffectiveDateTime = DateTime.Now.Year + DateTime.Now.ToString("MMddHHmmss");
            orc.OrderControlCodeReason = "";

            orc.EnteringOrganization = "";
            orc.EnteringDevice = "";
            orc.ActionBy = "";
            orc.AdvancedBeneficiaryNoticeCode = "";
            orc.OrderingFacilityName = "";
            orc.OrderingFacilityAddress = "";
            orc.OrderingFacilityPhoneNumber = "";
            orc.OrderingProviderAddress = "";
            return orc;
        }
        public PACsOBR genOBR(String xrayyear, String reqno, String xraycode, String xrayname, String xraytype, String userid, String username)
        {
            PACsOBR obr = new PACsOBR();
            obr.SetID = "";
            obr.PlacerOrderNumber = "";
            obr.FillerOrderNumber = "";
            String code = "";
            code = bcDB.xrDB.selectPACsInfinittCode(xraycode);
            obr.UniversalServiceID = code + "^" + xrayname;
            obr.Priority = "";
            obr.RequestedDateTime = "";
            obr.ObservationDateTime = "";
            obr.ObservationEndDateTime = "";

            obr.CollectionVolume = "";
            obr.CollectorIdentifier = "";
            obr.SpecimenActionCode = "";
            obr.DangerCode = "";
            obr.RelevantClinicalInfo = "";
            obr.SpecimenReceivedDateTime = "";
            obr.SpecimenSource = "";
            obr.OrderingProvider = userid + "^" + username;

            obr.OrderCallbackPhoneNumber = "";
            obr.PlacerField1 = xrayyear + reqno + code;
            obr.PlacerField2 = "RP" + xrayyear + reqno + code;
            obr.FillerField1 = "SS" + xrayyear + reqno + code;
            obr.FillerField2 = "";
            obr.ResultsRptStatusChngDateTime = "";
            obr.ChargeToPractice = "";
            obr.DiagnosticServSectID = xraytype;
            obr.ResultStatus = xraytype;
            obr.ParentResult = "";
            obr.QuantityTiming = "";
            obr.ResultCopiesTo = "";

            obr.ParentNumber = "";
            obr.TransportationMode = "";
            obr.ReasonForStudy = "";
            obr.PrincipalResultInterpreter = "";
            obr.AssistantResultInterpreter = "";
            obr.Technician = "";
            obr.Transcriptionist = "";
            obr.ScheduledDateTime = "";
            obr.NumberOfSampleContainers = "";
            obr.TransportLogisticsOfCollectedSample = "";
            obr.CollectorsComment = "";
            obr.TransportArrangementResponsibility = "";
            obr.TransportArranged = "";
            obr.EscortRequired = "A";
            obr.PlannedPatientTransportComment = "";
            obr.ProcedureCode = "";
            obr.ProcedureCodeModifier = code + "^" + xrayname;
            return obr;
        }
        public PACsZDS genZDS(String xrayyear, String reqno, String xraycode, String modality)
        {
            String code = "";
            code = bcDB.xrDB.selectPACsInfinittCode(xraycode);
            PACsZDS zds = new PACsZDS();
            zds.ZDS_Field1 = "1.2.410.2000010.66.101." + xrayyear + reqno + code;
            zds.ZDS_Field2 = "";
            zds.ZDS_Field3 = "";
            zds.ZDS_Field4 = "";
            zds.ZDS_Field5 = "";
            zds.ZDS_Field6 = "";
            zds.ZDS_Field7 = "";
            zds.ZDS_Field8 = "";
            zds.ZDS_Field9 = "";
            zds.ZDS_Field10 = "";

            return zds;
        }
        public String PACsORM(PACsMSH msh, PACsPID pid, PACsPV1 pv1, PACsORC orc, PACsOBR obr, PACsZDS zds, String comment)
        {
            String txt = "", MSH = "", separate = "|", PID = "", PV1 = "", ORC = "", OBR = "", ZDS = "", NTE = "";
            txt = "\x0b";

            MSH = "MSH" + separate + msh.EncodingCharacters + separate + msh.SendingApplication + separate + msh.SendingFacility + separate + msh.ReceivingApplication
                + separate + msh.ReceivingFacility + separate + msh.DateTimeOfMessage + separate + msh.Security + separate + msh.MessageType
                + separate + msh.MessageControlID + separate + msh.ProcessingID + separate + msh.VersionID + separate + msh.SequenceNumber
                + separate + msh.ContinuationPointer + separate + msh.AcceptAcknowledgementType + separate + msh.ApplicationAcknowledgementType + separate + msh.CountryCode
                + separate + msh.CharacterSet + separate + msh.PrincipalLanguageOfMessage + separate + msh.AlternateCharacterSetHandlingScheme;

            PID = "PID" + separate + pid.SetID + separate + pid.PatientID + separate + pid.PatientIdentifierList + separate + pid.AlternatePatientID
                + pid.PatientName + separate + pid.MothersMaidenName + separate + pid.DateTimeOfBirth + separate + pid.Sex
                + pid.PatientAlias + separate + pid.Race + separate + pid.PatientAddress + separate + pid.CountyCode
                + pid.HomePhoneNumber + separate + pid.BusinessPhoneNumber + separate + pid.PrimaryLanguage + separate + pid.MaritalStatus
                + pid.Religion + separate + pid.PatientAccountNumber + separate + pid.SSNNumber + separate + pid.DriversLicenseNumber
                + pid.MothersIdentifier + separate + pid.EthnicGroup + separate + pid.BirthPlace + separate + pid.MultipleBirthIndicator
                + pid.BirthOrder + separate + pid.Citizenship + separate + pid.VeteransMilitaryStatus + separate + pid.Nationality
                + pid.PatientDeathDateAndTime + separate + pid.PatientDeathIndicator;

            PV1 = "PV1" + separate + pv1.SetID + separate + pv1.PatientClass + separate + pv1.AssignedPatientLocation + separate + pv1.AdmissionType
                + separate + pv1.PreadmitNumber + separate + pv1.PriorPatientLocation + separate + pv1.AttendingDoctor + separate + pv1.ReferringDoctor
                + separate + pv1.ConsultingDoctor + separate + pv1.HospitalService + separate + pv1.TemporaryLocation + separate + pv1.PreadmitTestIndicator
                + separate + pv1.ReadmissionIndicator + separate + pv1.AdmitSource + separate + pv1.AmbulatoryStatus + separate + pv1.VIPIndicator
                + separate + pv1.AdmittingDoctor + separate + pv1.PatientType + separate + pv1.VisitNumber + separate + pv1.FinancialClass
                + separate + pv1.ChargePriceIndicator + separate + pv1.CourtesyCode + separate + pv1.CreditRating + separate + pv1.ContractCode
                + separate + pv1.ContractEffectiveDate + separate + pv1.ContractAmount + separate + pv1.ContractPeriod + separate + pv1.InterestCode
                + separate + pv1.TransferToBadDebtCode + separate + pv1.TransferToBadDebtDate + separate + pv1.BadDebtAgencyCode + separate + pv1.BadDebtTransferAmount
                + separate + pv1.BadDebtRecoveryAmount + separate + pv1.DeleteAccountIndicator + separate + pv1.DeleteAccountDate + separate + pv1.DischargeDisposition
                + separate + pv1.DischargedToLocation + separate + pv1.DietType + separate + pv1.ServicingFacility + separate + pv1.BedStatus
                + separate + pv1.AccountStatus + separate + pv1.PendingLocation + separate + pv1.PriorTemporaryLocation + separate + pv1.AdmitDateTime
                + separate + pv1.DischargeDateTime + separate + pv1.CurrentPatientBalance + separate + pv1.TotalCharges + separate + pv1.TotalAdjustments
                + separate + pv1.TotalPayments + separate + pv1.AlternateVisitID + separate + pv1.VisitIndicator + separate + pv1.OtherHealthcareProvider;

            ORC = "ORC" + separate + orc.OrderControl + separate + orc.PlacerOrderNumber + separate + orc.FillerOrderNumber + separate + orc.PlacerGroupNumber + separate
                + orc.OrderStatus + separate + orc.ResponseFlag + separate + orc.QuantityTiming + separate + orc.Parent + separate
                + orc.DateTimeOfTransaction + separate + orc.EnteredBy + separate + orc.VerifiedBy + separate + orc.OrderingProvider + separate
                + orc.EnterersLocation + separate + orc.CallBackPhoneNumber + separate + orc.OrderEffectiveDateTime + separate + orc.OrderControlCodeReason + separate
                + orc.EnteringOrganization + separate + orc.EnteringDevice + separate + orc.ActionBy + separate + orc.AdvancedBeneficiaryNoticeCode + separate
                + orc.OrderingFacilityName + separate + orc.OrderingFacilityAddress + separate + orc.OrderingFacilityPhoneNumber + separate + orc.OrderingProviderAddress;

            OBR = "OBR" + separate + obr.SetID + separate + obr.PlacerOrderNumber + separate + obr.FillerOrderNumber + separate + obr.UniversalServiceID + separate
                + obr.Priority + separate + obr.RequestedDateTime + separate + obr.ObservationDateTime + separate + obr.ObservationEndDateTime + separate
                + obr.CollectionVolume + separate + obr.CollectorIdentifier + separate + obr.SpecimenActionCode + separate + obr.DangerCode + separate
                + obr.RelevantClinicalInfo + separate + obr.SpecimenReceivedDateTime + separate + obr.SpecimenSource + separate + obr.OrderingProvider + separate
                + obr.OrderCallbackPhoneNumber + separate + obr.PlacerField1 + separate + obr.PlacerField2 + separate + obr.FillerField1 + separate
                + obr.FillerField2 + separate + obr.ResultsRptStatusChngDateTime + separate + obr.ChargeToPractice + separate + obr.DiagnosticServSectID + separate
                + obr.ResultStatus + separate + obr.ParentResult + separate + obr.QuantityTiming + separate + obr.ResultCopiesTo + separate
                + obr.ParentNumber + separate + obr.TransportationMode + separate + obr.ReasonForStudy + separate + obr.PrincipalResultInterpreter + separate
                + obr.AssistantResultInterpreter + separate + obr.Technician + separate + obr.Transcriptionist + separate + obr.ScheduledDateTime + separate
                + obr.NumberOfSampleContainers + separate + obr.TransportLogisticsOfCollectedSample + separate + obr.CollectorsComment + separate + obr.TransportArrangementResponsibility + separate
                + obr.TransportArranged + separate + obr.EscortRequired + separate + obr.PlannedPatientTransportComment + separate + obr.ProcedureCode
                + obr.ProcedureCodeModifier;

            ZDS = "ZDS" + separate + zds.ZDS_Field1 + separate + zds.ZDS_Field2 + separate + zds.ZDS_Field3 + separate + zds.ZDS_Field4 + separate
                + zds.ZDS_Field5 + separate + zds.ZDS_Field6 + separate + zds.ZDS_Field7 + separate + zds.ZDS_Field8 + separate
                + zds.ZDS_Field9 + separate + zds.ZDS_Field10;
            NTE = "NTE" + separate + "1" + separate + comment;
            txt = txt + MSH + Environment.NewLine + PID + Environment.NewLine + PV1 + Environment.NewLine + ORC + Environment.NewLine + OBR + Environment.NewLine + ZDS + Environment.NewLine + NTE + "\x0c";
            return txt;
        }
        public String genORM(String reqdept, String hn, String pttprefix, String pttfirstname, String pttlastame, String dob, String sex, String nation
            , String xrayyear, String reqno, String xraycode, String xrayname, String xraytype, String userid, String username, String modality
            , String opdtype, String depcode, String depname, String comment)
        {
            String txt = "";
            PACsMSH msh = new PACsMSH();
            PACsPID pid = new PACsPID();
            PACsPV1 pv1 = new PACsPV1();

            PACsORC orc = new PACsORC();
            PACsOBR obr = new PACsOBR();
            PACsZDS zds = new PACsZDS();
            msh = genPAsMSH(reqdept, "ORM^O01");
            pid = genPACsPID(hn, pttprefix, pttfirstname, pttlastame, dob, sex, nation);
            pv1 = genPV1(opdtype, depcode, depname);
            orc = genORC(xrayyear, reqno, xraycode, userid, username);
            obr = genOBR(xrayyear, reqno, xraycode, xrayname, xraytype, userid, username);
            zds = genZDS(xrayyear, reqno, xraycode, modality);
            txt = PACsORM(msh, pid, pv1, orc, obr, zds, comment);
            return txt;
        }
        public void setControlLabel(ref Label lb, Font fEdit, String text, String name, int x, int y)
        {
            lb.Text = text;
            lb.Font = fEdit;
            lb.Location = new System.Drawing.Point(x, y);
            lb.AutoSize = true;
            lb.Name = name;
        }
        public void setControlC1TextBox(ref C1TextBox txt, Font fEdit, String name, int width, int x, int y)
        {
            //txt = new C1TextBox();
            txt.Font = fEdit;
            txt.Location = new System.Drawing.Point(x, y);
            txt.Size = new Size(width, 30);
            txt.Name = name;
        }
        public void setControlRadioBox(ref RadioButton chk, Font fEdit, String text, String name, int x, int y)
        {
            chk.Checked = false;
            chk.Name = name;
            chk.Text = text;
            chk.Font = fEdit;
            Size size = MeasureString(chk);
            chk.Width = size.Width + 20;
            chk.Location = new Point(x, y);
        }
        public void setControlC1CheckBox(ref C1CheckBox chk, Font fEdit, String text, String name, int x, int y)
        {
            chk.BackColor = System.Drawing.Color.Transparent;
            chk.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            chk.BorderStyle = System.Windows.Forms.BorderStyle.None;
            chk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            chk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            chk.Location = new System.Drawing.Point(x, y);
            chk.Name = name;
            chk.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            chk.Text = text;
            chk.Value = text;
            chk.Font = fEdit;
            Size size = MeasureString(chk);
            chk.Width = size.Width + 30;
            chk.TabIndex = 0;
            //theme1.SetTheme(this.chkVoid, "(default)");
            chk.UseVisualStyleBackColor = true;
            chk.Value = null;
            chk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;

        }
        public void setControlCheckBox(ref CheckBox chk, Font fEdit, String text, String name, int x, int y)
        {
            chk.BackColor = System.Drawing.Color.Transparent;
            //chk.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            //chk.BorderStyle = System.Windows.Forms.BorderStyle.None;
            chk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            //chk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            chk.Location = new System.Drawing.Point(x, y);
            chk.Name = name;
            chk.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            chk.Text = text;
            //chk.Value = text;
            Size size = MeasureString(chk);
            chk.Width = size.Width;
            chk.TabIndex = 0;
            //theme1.SetTheme(this.chkVoid, "(default)");
            chk.UseVisualStyleBackColor = true;
            //chk.Value = null;
            //chk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            chk.Font = fEdit;
        }
        public void setControlC1Button(ref C1Button btn, Font fEdit, String text, String name, int x, int y)
        {
            btn = new C1Button();
            btn.Name = name;
            btn.Text = text;
            btn.Font = fEdit;
            btn.Location = new System.Drawing.Point(x, y);
            btn.Size = new Size(MeasureString(btn).Width + 50, 30);
            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextAlign = ContentAlignment.MiddleRight;
            btn.Font = fEdit;
        }
        public void setControlC1ComboBox(ref C1ComboBox cbo, String name, int width, int x, int y)
        {
            cbo.AllowSpinLoop = false;
            cbo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            cbo.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            cbo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            cbo.GapHeight = 0;
            cbo.ImagePadding = new System.Windows.Forms.Padding(0);
            cbo.ItemsDisplayMember = "";
            cbo.ItemsValueMember = "";
            cbo.Location = new System.Drawing.Point(x, y);
            cbo.Name = name;
            cbo.Size = new System.Drawing.Size(65, 20);
            cbo.Style.DropDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            cbo.Style.DropDownBorderColor = System.Drawing.Color.Gainsboro;
            cbo.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            cbo.TabIndex = 538;
            cbo.Tag = null;
            cbo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            cbo.Size = new Size(width, 30);
        }
        public void setControlComboBox(ref ComboBox cbo, String name, int width, int x, int y)
        {
            cbo = new System.Windows.Forms.ComboBox();
            cbo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            cbo.FormattingEnabled = true;
            cbo.Location = new System.Drawing.Point(x, y);
            cbo.Name = name;
            cbo.TabIndex = 9;
            cbo.Size = new Size(width, 30);
        }
        public void setControlC1DateTimeEdit(ref C1DateEdit txt, String name, int x, int y)
        {
            txt.AllowSpinLoop = false;
            txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txt.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            txt.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txt.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txt.CurrentTimeZone = false;
            txt.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            txt.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txt.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txt.EditFormat.CustomFormat = "dd/MM/yyyy";
            txt.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txt.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txt.GMTOffset = System.TimeSpan.Parse("00:00:00");
            txt.ImagePadding = new System.Windows.Forms.Padding(0);
            //txt.Culture = 1033;     // English US

            txt.Location = new System.Drawing.Point(x, y);
            txt.Name = name;
            txt.Size = new System.Drawing.Size(111, 20);
            txt.TabIndex = 12;
            txt.Tag = null;
            txt.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
        }
        public void setControlC1FlexViewer(ref C1FlexViewer fvPrnEmailSummary, String name)
        {
            fvPrnEmailSummary = new C1FlexViewer();
            fvPrnEmailSummary.AutoScrollMargin = new System.Drawing.Size(0, 0);
            fvPrnEmailSummary.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            fvPrnEmailSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            fvPrnEmailSummary.Location = new System.Drawing.Point(0, 0);
            fvPrnEmailSummary.Name = name;
            fvPrnEmailSummary.Size = new System.Drawing.Size(1065, 790);
            fvPrnEmailSummary.TabIndex = 0;
            fvPrnEmailSummary.Ribbon.Minimized = true;
        }
        public Boolean readOperation()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\opration.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                operation = File.ReadAllLines(filename);
            }
            return chk;
        }
        public Boolean writeOperation()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\opration.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                File.WriteAllLines(filename, operation, Encoding.UTF8);
            }
            else
            {
                File.CreateText(filename);
            }
            return chk;
        }
        public Boolean readPostOperation()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\postopration.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                postoperation = File.ReadAllLines(filename);
            }
            return chk;
        }
        public Boolean writePostOperation()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\postopration.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                File.WriteAllLines(filename, postoperation, Encoding.UTF8);
            }
            else
            {
                File.CreateText(filename);
            }
            return chk;
        }
        public Boolean readPreOperation()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\preopration.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                preoperation = File.ReadAllLines(filename);

            }
            return chk;
        }
        public Boolean writePreOperation()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\preopration.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                File.WriteAllLines(filename, preoperation, Encoding.UTF8);
            }
            else
            {
                File.CreateText(filename);
            }
            return chk;
        }
        public Boolean readFinding()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\finding.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                fining = File.ReadAllLines(filename);
            }
            return chk;
        }
        public Boolean readProcidures()
        {
            Boolean chk = true;
            String path = "", filename = "";
            path = "medical";
            filename = path + "\\procidures.txt";
            if (File.Exists(filename))
            {
                // Store each line in array of strings 
                procidures = File.ReadAllLines(filename);
            }
            return chk;
        }
        public Stream ToStream(String str)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public Stream ToStreamTxt(String[] str)
        {
            if (str == null) return null;
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            foreach (String txt in str)
            {
                writer.Write(txt + Environment.NewLine);
            }
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public String exportResultXray(DataTable dt, String hn, String vn, String flagOpenFile, String pathFolder)
        {
            //ReportDocument rpt;
            //CrystalReportViewer crv = new CrystalReportViewer();
            //rpt = new ReportDocument();
            //rpt.Load("xray_result.rpt");

            //crv.ReportSource = rpt;

            //crv.Refresh();
            ////rpt.Load(Application.StartupPath + "\\lab_opu_embryo_dev.rpt");
            ////rd.Load("StudentReg.rpt");
            //rpt.SetDataSource(dt);
            //rpt.SetParameterValue("line1", iniC.hostname);
            ////crv.ReportSource = rd;
            ////crv.Refresh();
            ////if (!Directory.Exists(iniC.medicalrecordexportpath))
            ////{
            ////    Directory.CreateDirectory(iniC.medicalrecordexportpath);
            ////}
            ////if (!Directory.Exists(iniC.medicalrecordexportpath + "\\" + hn))
            ////{
            ////    Directory.CreateDirectory(pathFolder);
            ////}
            //string filePath = pathFolder + "\\result_xray_" + hn + "_" + vn.Replace("/", "_") + ".pdf";
            //if (File.Exists(filePath))
            //    File.Delete(filePath);

            //ExportOptions CrExportOptions;
            //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            //CrDiskFileDestinationOptions.DiskFileName = filePath;
            //CrExportOptions = rpt.ExportOptions;
            //{
            //    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //    CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //}
            //rpt.Export();

            //if (!File.Exists(filePath))
            //{
            //    return "";
            //}

            //C1PdfDocumentSource pdf = new C1PdfDocumentSource();
            String filename = "", ext = "";

            //filename = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileNameWithoutExtension(filePath) + ".jpg";
            //var exporter = pdf.SupportedExportProviders[4].NewExporter();
            //exporter.ShowOptions = false;
            //exporter.FileName = filename;

            //pdf.LoadFromFile(filePath);
            //pdf.Export(exporter);

            //if (flagOpenFile.Equals("open"))
            //{
            //    // combine the arguments together
            //    // it doesn't matter if there is a space after ','
            //    string argument = "/select, \"" + filePath + "\"";

            //    System.Diagnostics.Process.Start("explorer.exe", argument);
            //}
            return filename;
        }
        public String exportResultLab(DataTable dt, String hn, String vn, String flagOpenFile, String pathFolder)
        {
            //ReportDocument rpt;
            //CrystalReportViewer crv = new CrystalReportViewer();
            //rpt = new ReportDocument();
            //rpt.Load("lab_result.rpt");
            //crv.ReportSource = rpt;

            //crv.Refresh();
            ////rpt.Load(Application.StartupPath + "\\lab_opu_embryo_dev.rpt");
            ////rd.Load("StudentReg.rpt");
            //rpt.SetDataSource(dt);
            //rpt.SetParameterValue("line1", iniC.hostname);
            ////crv.ReportSource = rd;
            ////crv.Refresh();
            //if (!Directory.Exists(pathFolder))
            //{
            //    Directory.CreateDirectory(pathFolder);
            //}
            ////if (!Directory.Exists(iniC.medicalrecordexportpath+"\\"+hn))
            ////{
            ////    Directory.CreateDirectory(iniC.medicalrecordexportpath + "\\" + hn);
            ////}
            //string filePath = pathFolder + "\\result_lab_" + hn + "_" + vn.Replace("/", "_") + ".pdf";
            //if (File.Exists(filePath))
            //    File.Delete(filePath);

            //ExportOptions CrExportOptions;
            //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            //CrDiskFileDestinationOptions.DiskFileName = filePath;
            //CrExportOptions = rpt.ExportOptions;
            //{
            //    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //    CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //}
            //rpt.Export();
            ////frmW.Dispose();

            //if (!File.Exists(filePath))
            //{
            //    return "";
            //}

            //C1PdfDocumentSource pdf = new C1PdfDocumentSource();
            String filename = "", ext = "";

            //filename = Path.GetDirectoryName(filePath)+"\\"+ Path.GetFileNameWithoutExtension(filePath)+".jpg";
            //var exporter = pdf.SupportedExportProviders[4].NewExporter();
            //exporter.ShowOptions = false;
            //exporter.FileName = filename;

            //pdf.LoadFromFile(filePath);
            //pdf.Export(exporter);

            //if (flagOpenFile.Equals("open"))
            //{
            //    // combine the arguments together
            //    // it doesn't matter if there is a space after ','
            //    string argument = "/select, \"" + filePath + "\"";

            //    System.Diagnostics.Process.Start("explorer.exe", argument);
            //}
            FrmReportNew frm = new FrmReportNew(this, "lab_result_3");
            frm.DT = dt;
            //frm.ShowDialog(this);
            frm.ExportReport(pathFolder + "_LAB.pdf");
            string argument = "/select, \"" + pathFolder + "_LAB.pdf" + "\"";
            Process.Start("explorer.exe", argument);
            return filename;
        }
        public String exportResultPharmacy(DataTable dt, String hn, String vn, String flagExpoler)
        {
            //ReportDocument rpt;
            //CrystalReportViewer crv = new CrystalReportViewer();
            //rpt = new ReportDocument();
            //rpt.Load("pharmacy_result.rpt");
            //crv.ReportSource = rpt;

            //crv.Refresh();
            ////rpt.Load(Application.StartupPath + "\\lab_opu_embryo_dev.rpt");
            ////rd.Load("StudentReg.rpt");
            //rpt.SetDataSource(dt);
            //rpt.SetParameterValue("line1", iniC.hostname);
            ////crv.ReportSource = rd;
            ////crv.Refresh();
            String filename = "";
            //filename = iniC.medicalrecordexportpath + "\\result_pharmacy_" + hn + "_" + vn.Replace("/", "_") + ".pdf";
            //if (!Directory.Exists(iniC.medicalrecordexportpath))
            //{
            //    Directory.CreateDirectory(iniC.medicalrecordexportpath);
            //}
            //if (File.Exists(filename))
            //    File.Delete(filename);

            //ExportOptions CrExportOptions;
            //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            //CrDiskFileDestinationOptions.DiskFileName = filename;
            //CrExportOptions = rpt.ExportOptions;
            //{
            //    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //    CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //}
            //rpt.Export();
            ////frmW.Dispose();

            string filePath = filename;
            //if (!File.Exists(filePath))
            //{
            //    return "";
            //}

            //// combine the arguments together
            //// it doesn't matter if there is a space after ','
            //if (flagExpoler.Equals("open"))
            //{
            //    string argument = "/select, \"" + filePath + "\"";
            //    System.Diagnostics.Process.Start("explorer.exe", argument);
            //}
            return filePath;
        }
        public string NumberToText(long number)
        {
            StringBuilder wordNumber = new StringBuilder();

            string[] powers = new string[] { "Thousand ", "Million ", "Billion " };
            string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string[] ones = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                                       "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

            if (number == 0) { return "Zero"; }
            if (number < 0)
            {
                wordNumber.Append("Negative ");
                number = -number;
            }

            long[] groupedNumber = new long[] { 0, 0, 0, 0 };
            int groupIndex = 0;

            while (number > 0)
            {
                groupedNumber[groupIndex++] = number % 1000;
                number /= 1000;
            }

            for (int i = 3; i >= 0; i--)
            {
                long group = groupedNumber[i];

                if (group >= 100)
                {
                    wordNumber.Append(ones[group / 100 - 1] + " Hundred ");
                    group %= 100;

                    if (group == 0 && i > 0)
                        wordNumber.Append(powers[i - 1]);
                }

                if (group >= 20)
                {
                    if ((group % 10) != 0)
                        wordNumber.Append(tens[group / 10 - 2] + " " + ones[group % 10 - 1] + " ");
                    else
                        wordNumber.Append(tens[group / 10 - 2] + " ");
                }
                else if (group > 0)
                    wordNumber.Append(ones[group - 1] + " ");

                if (group != 0 && i > 0)
                    wordNumber.Append(powers[i - 1]);
            }

            return wordNumber.ToString().Trim();
        }
        public string NumberToCurrencyTextThaiBaht(decimal number, MidpointRounding midpointRounding)
        {
            // Round the value just in case the decimal value is longer than two digits
            number = decimal.Round(number, 2, midpointRounding);

            string wordNumber = string.Empty;

            // Divide the number into the whole and fractional part strings
            string[] arrNumber = number.ToString().Split('.');

            // Get the whole number text
            long wholePart = long.Parse(arrNumber[0]);
            string strWholePart = NumberToText(wholePart);

            // For amounts of zero dollars show 'No Dollars...' instead of 'Zero Dollars...'
            //wordNumber = (wholePart == 0 ? "No" : strWholePart) + (wholePart == 1 ? " Dollar and " : " Dollars and ");
            wordNumber = (wholePart == 0 ? "No" : strWholePart) + (wholePart == 1 ? " Baht " : " Baht ");

            // If the array has more than one element then there is a fractional part otherwise there isn't
            // just add 'No Cents' to the end
            //if (arrNumber.Length > 1)
            //{
            //    // If the length of the fractional element is only 1, add a 0 so that the text returned isn't,
            //    // 'One', 'Two', etc but 'Ten', 'Twenty', etc.
            //    long fractionPart = long.Parse((arrNumber[1].Length == 1 ? arrNumber[1] + "0" : arrNumber[1]));
            //    string strFarctionPart = NumberToText(fractionPart);

            //    wordNumber += (fractionPart == 0 ? " No" : strFarctionPart) + (fractionPart == 1 ? " Cent" : " Cents");
            //}
            //else
            //    wordNumber += "No Cents";

            return wordNumber;
        }
        public string ThaiBahtText(string strNumber, bool IsTrillion = false)
        {
            string BahtText = "";
            string strTrillion = "";
            string[] strThaiNumber = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] strThaiPos = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };

            decimal decNumber = 0;
            decimal.TryParse(strNumber, out decNumber);

            if (decNumber == 0)
            {
                return "ศูนย์บาทถ้วน";
            }

            strNumber = decNumber.ToString("0.00");
            string strInteger = strNumber.Split('.')[0];
            string strSatang = strNumber.Split('.')[1];

            if (strInteger.Length > 13)
                throw new Exception("รองรับตัวเลขได้เพียง ล้านล้าน เท่านั้น!");

            bool _IsTrillion = strInteger.Length > 7;
            if (_IsTrillion)
            {
                strTrillion = strInteger.Substring(0, strInteger.Length - 6);
                BahtText = ThaiBahtText(strTrillion, _IsTrillion);
                strInteger = strInteger.Substring(strTrillion.Length);
            }

            int strLength = strInteger.Length;
            for (int i = 0; i < strInteger.Length; i++)
            {
                string number = strInteger.Substring(i, 1);
                if (number != "0")
                {
                    if (i == strLength - 1 && number == "1" && strLength != 1)
                    {
                        BahtText += "เอ็ด";
                    }
                    else if (i == strLength - 2 && number == "2" && strLength != 1)
                    {
                        BahtText += "ยี่";
                    }
                    else if (i != strLength - 2 || number != "1")
                    {
                        BahtText += strThaiNumber[int.Parse(number)];
                    }

                    BahtText += strThaiPos[(strLength - i) - 1];
                }
            }

            if (IsTrillion)
            {
                return BahtText + "ล้าน";
            }

            if (strInteger != "0")
            {
                BahtText += "บาท";
            }

            if (strSatang == "00")
            {
                BahtText += "ถ้วน";
            }
            else
            {
                strLength = strSatang.Length;
                for (int i = 0; i < strSatang.Length; i++)
                {
                    string number = strSatang.Substring(i, 1);
                    if (number != "0")
                    {
                        if (i == strLength - 1 && number == "1" && strSatang[0].ToString() != "0")
                        {
                            BahtText += "เอ็ด";
                        }
                        else if (i == strLength - 2 && number == "2" && strSatang[0].ToString() != "0")
                        {
                            BahtText += "ยี่";
                        }
                        else if (i != strLength - 2 || number != "1")
                        {
                            BahtText += strThaiNumber[int.Parse(number)];
                        }

                        BahtText += strThaiPos[(strLength - i) - 1];
                    }
                }
                BahtText += "สตางค์";
            }
            return BahtText;
        }
        public String selectNationName()
        {
            DataTable dt = new DataTable();
            String sql = "", chk = "-";
            sql = "Select  MNC_NAT_CD, MNC_NAT_DSC  " +
                "From  patient_m04  ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0]["MNC_NAT_CD"].ToString() + " " + dt.Rows[0]["FnameMNC_NAT_DSC"].ToString();
            }
            return chk;
        }
        public String selectDoctorName(String doctorId)
        {
            DataTable dt = new DataTable();
            String sql = "", chk = "-";
            sql = "Select  patient_m02.MNC_PFIX_DSC as prefix,patient_m26.MNC_DOT_FNAME as Fname,patient_m26.MNC_DOT_LNAME as Lname,patient_m26.MNC_DOT_FNAME_e,patient_m26.MNC_DOT_LNAME_e  " +
                "From  patient_m26  " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                "where patient_m26.MNC_DOT_CD = '" + doctorId + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0]["prefix"].ToString() + " " + dt.Rows[0]["Fname"].ToString() + " " + dt.Rows[0]["Lname"].ToString();
            }
            return chk;
        }
        public String selectDoctorNameE(String doctorId)
        {
            DataTable dt = new DataTable();
            String sql = "", chk = "-";
            sql = "Select  patient_m02.MNC_PFIX_DSC as prefix,patient_m26.MNC_DOT_FNAME as Fname,patient_m26.MNC_DOT_LNAME as Lname,patient_m26.MNC_DOT_FNAME_e,patient_m26.MNC_DOT_LNAME_e  " +
                "From  patient_m26  " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                "where patient_m26.MNC_DOT_CD = '" + doctorId + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                chk = "DR. " + dt.Rows[0]["MNC_DOT_FNAME_e"].ToString() + " " + dt.Rows[0]["MNC_DOT_LNAME_e"].ToString();
            }
            return chk;
        }
        public String datetoShow2(String date)
        {
            String dd = "", mm = "", yyyy = "", re = "";
            int dd1 = 0, mm1 = 0, yyyy1 = 0;
            re = date;
            if (date.Length >= 12)
            {
                dd = date.Substring(0, 2);
                mm = date.Substring(4, 2);
                yyyy = date.Substring(yyyy.Length - 4);
                int.TryParse(dd, out dd1);
                int.TryParse(mm, out mm1);
                int.TryParse(yyyy, out yyyy1);
                if (yyyy1 > 2500) yyyy1 = yyyy1 - 543;
                DateTime date1 = new DateTime(yyyy1, mm1, dd1);
                re = date1.ToString("dd") + "/" + date1.ToString("MMM") + "/" + date1.ToString("yyyy");
            }

            return re;
        }
        public void SaveSheet(C1FlexGrid flex, XLSheet sheet, C1XLBook _book, bool fixedCells)
        {
            // account for fixed cells
            //int frows = flex.Rows.Fixed;
            int frows = 0;// with header
            int fcols = flex.Cols.Fixed;
            if (fixedCells) frows = fcols = 0;

            // copy dimensions
            //int lastRow = flex.Rows.Count - frows - 1;
            int lastRow = flex.Rows.Count;// with header
            int lastCol = flex.Cols.Count - fcols - 1;
            if (lastRow < 0 || lastCol < 0) return;
            XLCell cell = sheet[lastRow, lastCol];

            // set default properties
            sheet.Book.DefaultFont = flex.Font;
            sheet.DefaultRowHeight = C1XLBook.PixelsToTwips(flex.Rows.DefaultSize);
            sheet.DefaultColumnWidth = C1XLBook.PixelsToTwips(flex.Cols.DefaultSize);

            // prepare to convert styles
            _styles = new Hashtable();

            // set row/column properties
            for (int r = frows; r < flex.Rows.Count; r++)
            {
                // size/visibility
                Row fr = flex.Rows[r];
                XLRow xr = sheet.Rows[r - frows];
                if (fr.Height >= 0)
                    xr.Height = C1XLBook.PixelsToTwips(fr.Height);
                xr.Visible = fr.Visible;

                // style
                //XLStyle xs = StyleFromFlex(_book,fr.Style, _styles);
                //if (xs != null)
                //    xr.Style = xs;
            }
            for (int c = fcols; c < flex.Cols.Count; c++)
            {
                // size/visibility
                Column fc = flex.Cols[c];
                XLColumn xc = sheet.Columns[c - fcols];
                if (fc.Width >= 0)
                    xc.Width = C1XLBook.PixelsToTwips(fc.Width);
                xc.Visible = fc.Visible;

                // style
                //XLStyle xs = StyleFromFlex(_book, fc.Style, _styles);
                //if (xs != null)
                //    xc.Style = xs;
            }

            // load cells
            for (int r = frows; r < flex.Rows.Count; r++)
            {
                for (int c = fcols; c < flex.Cols.Count; c++)
                {
                    // get cell
                    cell = sheet[r - frows, c - fcols];

                    // apply content
                    cell.Value = flex[r, c];

                    // apply style
                    //XLStyle xs = StyleFromFlex(_book,flex.GetCellStyle(r, c), _styles);
                    //if (xs != null)
                    //    cell.Style = xs;
                }
            }
        }
        public byte[] Encode(string text)
        {
            return Encoding.GetEncoding(1252).GetBytes(text);
        }
        public byte[] ToUTF8(byte[] tis620Bytes)
        {
            List<byte> buffer = new List<byte>();
            byte safeAscii = 126;
            for (var i = 0; i < tis620Bytes.Length; i++)
            {
                if (tis620Bytes[i] > safeAscii)
                {
                    if (((0xa1 <= tis620Bytes[i]) && (tis620Bytes[i] <= 0xda))
                        || ((0xdf <= tis620Bytes[i]) && (tis620Bytes[i] <= 0xfb)))
                    {
                        var utf8Char = 0x0e00 + tis620Bytes[i] - 0xa0;
                        byte utf8Byte1 = (byte)(0xe0 | (utf8Char >> 12));
                        buffer.Add(utf8Byte1);
                        byte utf8Byte2 = (byte)(0x80 | ((utf8Char >> 6) & 0x3f));
                        buffer.Add(utf8Byte2);
                        byte utf8Byte3 = (byte)(0x80 | (utf8Char & 0x3f));
                        buffer.Add(utf8Byte3);
                    }
                }
                else
                {
                    buffer.Add(tis620Bytes[i]);
                }
            }
            return buffer.ToArray();
        }
        public byte[] ToUTF8_1(byte[] tis620Bytes)
        {
            List<byte> buffer = new List<byte>();
            byte safeAscii = 126;
            for (var i = 0; i < tis620Bytes.Length; i++)
            {
                if (tis620Bytes[i] > safeAscii)
                {
                    if (((0xa1 <= tis620Bytes[i]) && (tis620Bytes[i] <= 0xda))
                        || ((0xdf <= tis620Bytes[i]) && (tis620Bytes[i] <= 0xfb)))
                    {
                        var utf8Char = 0x0e00 + tis620Bytes[i] - 0xa0;

                        byte utf8Byte1 = (byte)(0xe0 | (utf8Char >> 12));
                        buffer.Add(utf8Byte1);
                        byte utf8Byte2 = (byte)(0x80 | ((utf8Char >> 6) & 0x3f));
                        buffer.Add(utf8Byte2);
                        byte utf8Byte3 = (byte)(0x80 | (utf8Char & 0x3f));
                        buffer.Add(utf8Byte3);
                    }
                }
                else
                {
                    buffer.Add(tis620Bytes[i]);
                }
            }
            return buffer.ToArray();
        }
        public void SaveSheetDataTable(DataTable flex, XLSheet sheet, C1XLBook _book, bool fixedCells)
        {
            // account for fixed cells
            //int frows = flex.Rows.Fixed;
            int frows = 0;// with header
            //int fcols = flex.Cols.Fixed;
            //if (fixedCells) frows = fcols = 0;

            // copy dimensions
            //int lastRow = flex.Rows.Count - frows - 1;
            int lastRow = flex.Rows.Count;// with header
            int lastCol = flex.Columns.Count - 1;
            if (lastRow < 0 || lastCol < 0) return;
            XLCell cell = sheet[lastRow, lastCol];

            // set default properties
            sheet.Book.DefaultFont = new Font(iniC.grdViewFontName, grdViewFontSize, FontStyle.Regular);
            //sheet.DefaultRowHeight = C1XLBook.PixelsToTwips(flex.Rows.DefaultSize);
            //sheet.DefaultColumnWidth = C1XLBook.PixelsToTwips(flex.Cols.DefaultSize);

            // prepare to convert styles
            _styles = new Hashtable();

            // set row/column properties

            // load cells
            for (int r = frows; r < flex.Rows.Count; r++)
            {
                for (int c = 0; c < flex.Columns.Count; c++)
                {
                    // get cell
                    cell = sheet[r - frows, c];

                    // apply content
                    cell.Value = flex.Rows[r][c];

                    // apply style
                    //XLStyle xs = StyleFromFlex(_book,flex.GetCellStyle(r, c), _styles);
                    //if (xs != null)
                    //    cell.Style = xs;
                }
            }
        }
        public String genAipnFile(String authorName, String anno1, String submtype, Boolean statusSendMulti, Boolean statusNoAdd, Boolean flagAnNew)
        {
            DataTable dtaipn = new DataTable();
            DataTable dtclaimAuth = new DataTable();
            DataTable dtIPADT = new DataTable();
            DataTable dtIPOp = new DataTable();
            DataTable dtIPDx = new DataTable();
            DataTable dtBillItms = new DataTable();
            DataTable dtInv = new DataTable();
            DataTable dtCoin = new DataTable();
            DataTable dtAipn = new DataTable();

            String Hmain = "", sessionNo = "", pathFile = "", HeaderXML = "<?xml version=\"1.0\" encoding=\"windows-874\"?>", FooterXML = "";
            //aipnid = bcDB.aipnDB.selectAipnIdByStatusMakeText();

            dtAipn = bcDB.aipnDB.selectAipnIdByStatusMakeText(anno1, statusSendMulti, statusNoAdd);

            if (dtAipn.Rows.Count <= 0)
            {
                MessageBox.Show("ไม่พบข้อมูล ", "");
                return "";
            }
            Hmain = iniC.ssoid;
            sessionNo = bcDB.aipnDB.insertClaimZip(Hmain);
            pathFile = iniC.aipnXmlPath + "\\" + sessionNo;
            if (!Directory.Exists(pathFile))            {                Directory.CreateDirectory(pathFile);            }
            foreach (DataRow rowAipn in dtAipn.Rows)
            {
                String aipnXML = "", aipnid = "", anno = "", ancnt = "", an1 = "";
                aipnid = rowAipn["aipn_id"].ToString();
                anno = rowAipn["an_no"].ToString();
                ancnt = rowAipn["an_cnt"].ToString();
                an1 = rowAipn["an_1"].ToString();

                dtaipn = bcDB.aipnDB.selectAipn(aipnid);
                dtclaimAuth = bcDB.aipnDB.selectClaimAuth(aipnid);
                dtIPADT = bcDB.aipnDB.selectIPADT(aipnid);
                dtIPOp = bcDB.aipnDB.selectIPOp(aipnid);
                dtIPDx = bcDB.aipnDB.selectIPDx(aipnid);
                dtInv = bcDB.aipnDB.selectInvoice(aipnid);
                //dtBillItms = bcDB.aipnDB.selectBillItems(anno, ancnt);
                dtBillItms = bcDB.aipnDB.selectBillItemsByAipn(aipnid);
                dtCoin = bcDB.aipnDB.selectCoinsurance(aipnid);
                if (dtInv.Rows.Count <= 0)
                {
                    new LogWriter("d", "BangnaControl genAipnFile  invoice บิล ไม่พบข้อมูล aipnid "+ aipnid+ " anno " + anno+ " ancnt " + ancnt);
                    //MessageBox.Show("invoice บิล ไม่พบข้อมูล ", "");
                    continue;
                }
                AipnXmlFile aipnxmlF = new AipnXmlFile();
                String aipnHeader = "", aipnClaimAuth = "", aipnIPADT = "", aipnIPOp = "", aipnIPDx = "", aipnBillItms = "", aipnInv = "", aionCoin = "";
                String prefixAn = "", Hcare = "", hn = "", dtEffTime = "", SubmDT = "";
                dtEffTime = DateTime.Now.Year.ToString() + DateTime.Now.ToString("-MM-ddThh:mm:ss");

                foreach (DataRow drow in dtclaimAuth.Rows)
                {
                    Hcare = drow["hcare"].ToString();
                    Hmain = drow["hmain"].ToString();
                    aipnHeader = aipnxmlF.genHeader(iniC.ssoid, dtEffTime, authorName);
                    aipnClaimAuth = aipnxmlF.genClaimAuth(drow["upayplan"].ToString(), drow["servicetype"].ToString(), drow["projectcode"].ToString()
                        , drow["eventcode"].ToString(), drow["hmain"].ToString(), Hcare, drow["careas"].ToString(), drow["servicesubtype"].ToString());
                }
                if (dtaipn.Rows.Count > 0)
                {
                    new LogWriter("d", "genAipnFile an_1 " + dtaipn.Rows[0]["an_1"].ToString());
                    String servicetype = "";
                    DataTable dtclaima = bcDB.aipnDB.selectClaimAuth(aipnid);
                    if (dtclaima.Rows.Count > 0)
                    {
                        servicetype = dtclaima.Rows[0]["servicetype"].ToString();
                    }
                    new LogWriter("d", "genAipnFile servicetype " + servicetype);
                    if (servicetype.Equals("DS"))       //ถ้าเป็น ODS
                    {
                        String vsdate = dtaipn.Rows[0]["visit_date"].ToString().Replace("-", "").Replace("-", "");
                        vsdate = vsdate.Substring(1);
                        prefixAn = aipnxmlF.genPrefixAN(vsdate, dtaipn.Rows[0]["pre_no"].ToString());
                        new LogWriter("d", "genAipnFile prefixAn " + prefixAn);
                    }
                    else
                    {
                        if (flagAnNew)
                        {
                            if (dtaipn.Rows[0]["an_1"].ToString().Length <= 0) MessageBox.Show("AN new " + dtaipn.Rows[0]["an_1"].ToString(), "");
                            prefixAn = aipnxmlF.genPrefixAN(dtaipn.Rows[0]["an_1"].ToString());
                        }
                        else
                            prefixAn = aipnxmlF.genPrefixAN(dtaipn.Rows[0]["an_no"].ToString(), dtaipn.Rows[0]["an_cnt"].ToString());
                    }
                    hn = dtaipn.Rows[0]["hn"].ToString();
                }
                aipnIPADT = aipnxmlF.genIPADT(prefixAn, dtIPADT);
                aipnIPOp = aipnxmlF.genIPOp(dtIPOp);
                aipnIPDx = aipnxmlF.genIPDx(dtIPDx);

                aipnInv = aipnxmlF.genInvoice(dtInv.Rows[0]["invnumber"].ToString(), dtInv.Rows[0]["invdt"].ToString(), dtInv.Rows[0]["invadddiscount"].ToString()
                        , dtInv.Rows[0]["drgcharge"].ToString(), dtInv.Rows[0]["xdrgclaim"].ToString(), dtBillItms);

                if (dtCoin.Rows.Count > 0)
                {
                    aionCoin = aipnxmlF.genCoinsurance(dtCoin.Rows[0]["instypecode"].ToString(), dtCoin.Rows[0]["instotal"].ToString(), dtCoin.Rows[0]["insroomboard"].ToString(), dtCoin.Rows[0]["insproffee"].ToString(), dtCoin.Rows[0]["insother"].ToString());
                }
                else
                {
                    aionCoin = aipnxmlF.genCoinsurance("", "", "", "", "");
                }
                HeaderXML = "<?xml version=\"1.0\" encoding=\"windows-874\"?>" + Environment.NewLine;
                aipnXML += "<CIPN>" + Environment.NewLine;
                aipnXML += aipnHeader;
                aipnXML += aipnClaimAuth;
                aipnXML += aipnIPADT;
                aipnXML += aipnIPDx;
                aipnXML += aipnIPOp;
                aipnXML += aipnInv;
                aipnXML += aionCoin;
                aipnXML += "</CIPN>" + Environment.NewLine;

                String md5aipn = ComputeMD5(aipnXML);
                byte[] md5aipnBytes = Encoding.UTF8.GetBytes(md5aipn);

                byte[] wind874Bytes = Encoding.UTF8.GetBytes(aipnXML);

                String aaa = wind874Bytes.ToString();
                byte[] win874BytesAipn = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(874), wind874Bytes);
                String md5win874 = ComputeMD5Bytes(win874BytesAipn);
                byte[] md5aipnWin874Bytes = Encoding.GetEncoding(874).GetBytes(md5win874);
                byte[] win874BytesHeader = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(874), Encoding.UTF8.GetBytes(HeaderXML));

                //byte[] byteMD5 = MD5.Create().ComputeHash(win874BytesAipn);
                //String hex = BitConverter.ToString(byteMD5).Replace("-",String.Empty);
                //byte[] win874HexMD5 = Encoding.GetEncoding(874).GetBytes(hex);

                //string result = Encoding.UTF8.GetString(byteMD5);
                //StringBuilder sb = new StringBuilder();
                //for (int i = 0; i < byteMD5.Length; i++)
                //{
                //    sb.Append(byteMD5[i].ToString("X2"));
                //}

                //FooterXML = "<?EndNote HMAC=\""+ Encoding.Default.GetString(hash) + "\" ?>";
                md5aipnBytes = md5aipnWin874Bytes.ToArray();
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

                if (!Directory.Exists(iniC.aipnXmlPath))
                {
                    Directory.CreateDirectory(iniC.aipnXmlPath);
                }
                SubmDT = dtEffTime.Replace("-", "").Replace("-", "").Replace("T", "").Replace(":", "").Replace(":", "");
                String fileName = Hcare + "-AIPN-" + prefixAn + "-" + SubmDT;
                //แก้ไขส่งข้อมูลใหม่  กำหนดให้ ชื่อ file ต้องมี add, aud, adj
                if (anno.Length > 0)
                {
                    //fileName += "-"+submtype;
                }
                if ((fileName.Length > 0) && (fileName.Substring(fileName.Length - 1) == "-"))
                {
                    fileName = fileName.Substring(0, fileName.Length - 1);
                }
                Boolean chk = ByteArrayToFile(pathFile + "\\" + fileName + "-data.xml", win874BytesAipn);
                Boolean chk2 = ByteArrayToFile(pathFile + "\\" + fileName + "-utf8.xml", Encoding.UTF8.GetBytes(aipnXML));
                Boolean chk1 = ByteArrayToFile(pathFile + "\\" + fileName + ".xml", rv1);
                bcDB.aipnDB.updateSessionNoStatusMakeText(sessionNo, aipnid);
            }

            try
            {
                String fileZipName = "";
                C1ZipFile zip = new C1ZipFile(); //iniC.ssoid
                //fileZipName = Hmain+"AIPN"+ sessionNo;
                fileZipName = iniC.ssoid + "AIPN" + sessionNo;
                zip.Create(pathFile + "\\" + fileZipName + ".zip");
                foreach (String filename in Directory.GetFiles(pathFile))
                {
                    if (filename.IndexOf("-data") > 0)
                    {
                        continue;
                    }
                    if (filename.IndexOf("-utf8") > 0)
                    {
                        continue;
                    }
                    if (filename.IndexOf(".zip") > 0)
                    {
                        continue;
                    }
                    zip.Entries.Add(filename);
                }
                zip.Close();
                Process.Start("explorer.exe", pathFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);

            }

            return sessionNo;
        }
        public string CreateMD5Hash(string input)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.GetEncoding(874).GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public String CreateMD5Hash(byte[] inputBytes)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            //byte[] inputBytes = Encoding.GetEncoding(874).GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
        //public String CreateMD5(string input)
        //{
        //    // Use input string to calculate MD5 hash
        //    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        //    {
        //        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        //        byte[] hashBytes = md5.ComputeHash(inputBytes);

        //        return Convert.ToHexString(hashBytes); // .NET 5 +
        //    }
        //}
        public string GetMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public String ComputeMD5Bytes(byte[] input)
        {
            StringBuilder sb = new StringBuilder();
            // Initialize a MD5 hash object
            using (MD5 md5 = MD5.Create())
            {
                // Compute the hash of the given string
                byte[] hashValue = md5.ComputeHash(input);
                // Convert the byte array to string format
                foreach (byte b in hashValue)
                {
                    sb.Append($"{b:X2}");
                }
            }
            return sb.ToString();
        }
        public String ComputeMD5(String input)
        {
            StringBuilder sb = new StringBuilder();
            // Initialize a MD5 hash object
            using (MD5 md5 = MD5.Create())
            {
                // Compute the hash of the given string
                byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                // Convert the byte array to string format
                foreach (byte b in hashValue)
                {
                    sb.Append($"{b:X2}");
                }
            }
            return sb.ToString();
        }
        public bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }
        public String showTime(String time)
        {
            String txt = "";
            if (time == null) return txt;
            txt = "0000" + time;
            txt = txt.Substring(txt.Length - 4);
            txt = txt.Substring(0, 2) + ":" + txt.Substring(txt.Length - 2);
            return txt;
        }
        private DataTable setPrintLabAN(Patient ptt, String an)
        {
            DataTable dt = new DataTable();
            String anno = "", xraycode = "", txt1 = "", reqdate = "", reqno = "", dtrname = "", ordname = "", orddetail = "", dtrxrname = "", resdate = "", pttcompname = "", paidname = "", depname = "";

            dt = bcDB.vsDB.selectLabbyAN_ALL(ptt.MNC_HN_NO, vsdate);

            foreach (DataRow drow in dt.Rows)
            {
                Boolean chkname = false;
                chkname = hn.Any(c => !Char.IsLetterOrDigit(c));
                if (chkname)
                {
                    //drow["patient_age"] = ptt.AgeString().Replace("Year", "ปี").Replace("Month", "เดือน").Replace("Days", "วัน").Replace("s", "");
                    drow["patient_age"] = ptt.AgeStringShort().Replace("Year", "ปี").Replace("Month", "เดือน").Replace("Days", "วัน").Replace("s", "");
                }
                else
                {
                    drow["patient_age"] = ptt.AgeString();
                }

                drow["patient_name"] = ptt.Name;
                drow["patient_hn"] = hn;

                drow["patient_vn"] = vn;

                drow["patient_type"] = drow["MNC_FN_TYP_DSC"].ToString();
                drow["request_no"] = drow["MNC_REQ_NO"].ToString() + "/" + datetoShow(drow["mnc_req_dat"].ToString());
                drow["doctor"] = drow["dtr_name"].ToString() + "[" + drow["mnc_dot_cd"].ToString() + "]";
                //drow["result_date"] = bc.datetoShow(dtreq.Rows[0]["mnc_req_dat"].ToString());
                drow["result_date"] = datetoShow(drow["MNC_RESULT_DAT"].ToString()) + " " + drow["MNC_RESULT_TIM"].ToString();
                drow["print_date"] = datetoShow(drow["MNC_STAMP_DAT"].ToString());
                drow["user_lab"] = drow["user_lab"].ToString() + " [ทน." + drow["MNC_USR_NAME_result"].ToString() + "]";
                drow["user_report"] = drow["user_report"].ToString() + " [ทน." + drow["MNC_USR_NAME_report"].ToString() + "]";
                drow["user_check"] = drow["user_check"].ToString() + " [ทน." + drow["MNC_USR_NAME_approve"].ToString() + "]";
                if (iniC.branchId.Equals("005"))
                {
                    drow["patient_dep"] = drow["MNC_REQ_DEP"].ToString().Equals("101") ? "OPD1" : depname.Equals("107") ? "OPD2" : depname.Equals("103") ? "OPD3" :
                    depname.Equals("104") ? "ER" : depname.Equals("106") ? "WARD6" : depname.Equals("108") ? "WARD5W" : depname.Equals("109") ? "ล้างไต" :
                    depname.Equals("105") ? "WARD5M" : depname.Equals("113") ? "ICU" : depname.Equals("114") ? "NS/LR" : depname.Equals("115") ? "ทันตกรรม" : depname.Equals("116") ? "CCU" : depname;
                }
                else if (iniC.branchId.Equals("002"))
                {
                    drow["patient_dep"] = drow["MNC_REQ_DEP"].ToString();
                }
                else if (iniC.branchId.Equals("001"))
                {
                    drow["patient_dep"] = drow["MNC_REQ_DEP"].ToString();
                }

                //drow["mnc_lb_dsc"] = dtreq.Rows[0]["MNC_LB_DSC"].ToString();
                drow["mnc_lb_grp_cd"] = drow["MNC_LB_TYP_DSC"].ToString();
                if (drow["MNC_RES_VALUE"].ToString().Equals("-"))
                {
                    drow["MNC_RES_UNT"] = "";
                }
                drow["MNC_RES_UNT"] = drow["MNC_RES_UNT"].ToString().Replace("0.00-0.00", "").Replace("0.00 - 0.00", "").Replace("0.00", "");
                drow["sort1"] = drow["mnc_req_dat"].ToString().Replace("-", "").Replace("-", "") + drow["MNC_REQ_NO"].ToString();
            }
            foreach (DataRow drow in dt.Rows)
            {
                //MessageBox.Show(drow["sort1"].ToString(), "");
                if (drow["sort1"] == null)
                {
                    //MessageBox.Show("11", "");
                }

                if (drow["sort1"].ToString().Equals(""))
                {
                    //MessageBox.Show("22", "");
                }
            }
            return dt;
        }
        private String getDate(C1DateEdit object1)
        {
            String re = "";
            DateTime dtdate = new DateTime();
            dtdate = (DateTime)(object1.Value);
            if (dtdate.Year < 1900) dtdate = dtdate.AddYears(543);
            if (dtdate.Year > 2500) dtdate = dtdate.AddYears(-543);
            return re;
        }
        public XrayT01 setXrayT01(String hn, string hnyear, String vsDate, String preno, String dtrcode)
        {
            XrayT01 xrayT01 = new XrayT01();
            xrayT01.MNC_REQ_YR = (DateTime.Now.Year + 543).ToString();
            xrayT01.MNC_REQ_NO = "";
            xrayT01.MNC_REQ_DAT = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd");
            xrayT01.MNC_REQ_DEP = "";
            xrayT01.MNC_REQ_STS = "";
            xrayT01.MNC_REQ_TIM = "";
            xrayT01.MNC_HN_YR = hnyear;
            xrayT01.MNC_HN_NO = hn;
            xrayT01.MNC_AN_YR = "";
            xrayT01.MNC_AN_NO = "";
            xrayT01.MNC_PRE_NO = preno;
            xrayT01.MNC_DATE = vsDate;
            xrayT01.MNC_TIME = "";
            xrayT01.MNC_DOT_CD = dtrcode;
            xrayT01.MNC_WD_NO = "";
            xrayT01.MNC_RM_NAM = "";
            xrayT01.MNC_BD_NO = "";
            xrayT01.MNC_FN_TYP_CD = "";
            xrayT01.MNC_COM_CD = "";
            xrayT01.MNC_REM = "";
            xrayT01.MNC_XR_STS = "";
            xrayT01.MNC_CAL_NO = "";
            xrayT01.MNC_EMPR_CD = user.username;
            xrayT01.MNC_EMPC_CD = user.username;
            xrayT01.MNC_ORD_DOT = "";
            xrayT01.MNC_CFM_DOT = "";
            xrayT01.MNC_DOC_YR = "";
            xrayT01.MNC_DOC_NO = "";
            xrayT01.MNC_DOC_DAT = "";
            xrayT01.MNC_DOC_CD = "";
            xrayT01.MNC_STAMP_DAT = "";
            xrayT01.MNC_STAMP_TIM = "";
            xrayT01.MNC_CANCEL_STS = "";
            xrayT01.MNC_PAC_CD = "";
            xrayT01.MNC_PAC_TYP = "";
            xrayT01.status_pacs = "";
            return xrayT01;
        }
        public void genImgStaffNote(Patient ptt, Visit vs, Font fEdit, String chkcode, String chkname, string chiefcom)
        {
            String err = "";
            float mmpi = 25.4f;
            int dpi = 150, line1Len = 965, x2Right = 1740;
            Font fEditS = new Font(fEdit.FontFamily, fEdit.Size - 1);
            Font fEditB = new Font(fEdit.FontFamily, fEdit.Size + 1, FontStyle.Bold);
            Font famtB = new Font(fEdit.FontFamily, fEdit.Size + 4, FontStyle.Bold);        //fEdit5B
            Font fEdit5B = new Font(fEdit.FontFamily, fEdit.Size + 6, FontStyle.Bold);
            err = "00";
            Image imgLogo = Resources.LOGO_Green_Transparent;
            int newHeight = 50, yPos = 0, gapLine = 45, col2 = 470, col3 = 980, col31 = 460, recx = 15, recy = 15, col2int = 0, col4 = 0, col40 = 0, col40int = 0, yPosint = 0, col5 = 980, col6 = 1340;
            String line = "", date = "";
            StringFormat flags = new StringFormat(StringFormatFlags.LineLimit);  //wraps

            Image resizedImageLogo = imgLogo.GetThumbnailImage((newHeight * imgLogo.Width) / imgLogo.Height, newHeight, null, IntPtr.Zero);
            //Bitmap imgA4 = new Bitmap((int)(210 / mmpi * dpi), (int)(297 / mmpi * dpi));        //Port
            Bitmap imgA4 = new Bitmap((int)(297 / mmpi * dpi), (int)(210 / mmpi * dpi));        //lang

            Pen penGreen3 = new Pen(Color.FromArgb(26, 173, 79), 3);
            Pen penBlue3 = new Pen(Color.FromArgb(79, 111, 108), 3);
            Pen blackPen = new Pen(Color.Black, 1);
            Pen penBorder = penBlue3;
            SolidBrush BrushBlack = new SolidBrush(Color.Black);
            SolidBrush brushBule = new SolidBrush(Color.Blue);
            Rectangle rec = new Rectangle(0, 0, 20, 20);
            date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            col2 = 105; col3 = 300; col4 = 1000; col40 = 650; yPos = 15; col6 = 1340;
            col2int = int.Parse(col2.ToString()); yPosint = int.Parse(yPos.ToString()); col40int = int.Parse(col40.ToString());
            imgA4.SetResolution(dpi, dpi);
            err = "01";

            using (Graphics gfx = Graphics.FromImage(imgA4))
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                String line1 = "";
                gfx.FillRectangle(brush, 0, 0, imgA4.Width, imgA4.Height);
                gfx.DrawImage(resizedImageLogo, 120, 40);
                //gfx.DrawImage(resizedImageLogo, 1000, 40);
                line = "โรงพยาบาล บางนา5";
                gfx.DrawString(line, famtB, brushBule, 195, 35, flags);
                gfx.DrawString(line, famtB, brushBule, 1070, 35, flags);
                //gfx.DrawRectangle(penBorder, 3, 3, imgA4.Width - 9, imgA4.Height - 9);            // Border
                line = "H.N. " + ptt.MNC_HN_NO + "     " + vs.VN;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 35, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 35, flags);
                line = "ชื่อ " + ptt.Name;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 60, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 60, flags);
                line = "เลขที่บัตร " + ptt.MNC_ID_NO;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 85, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, 1340, 85, flags);
                line = vs.PaidName;
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 115, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 115, flags);

                line = ptt.comNameT;
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 140, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 140, flags);
                line = "โรคประจำตัว        ไม่มี";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 140, flags);
                rec = new Rectangle(col2int + 125, 145, recx, recy);
                gfx.DrawRectangle(blackPen, rec);
                line = "อายุ " + ptt.AgeStringShort1() + " [" + datetoShow(ptt.MNC_BDAY) + "]";
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 140, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 140, flags);

                line = "มีโรค ระบุ";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + 35, 170, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2int + 35 - recx - 10, 180, recx, recy));
                line = "วันที่เวลา " + date;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 170, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 170, flags);

                line = "โรคเรื้อรัง";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 200, flags);
                line = "ชื่อแพทย์ " + vs.DoctorId + " " + vs.DoctorName;
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 200, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 200, flags);

                line = "DR Time.                               ปิดใบยา";
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 230, flags);
                line = "อาการเบื้องต้น " + vs.symptom;
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 230, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col6, 230, flags);

                line = "Temp";
                gfx.DrawString(line, fEditS, Brushes.Black, col2, 260, flags);

                line = "H.Rate";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 80, 260, flags);
                line = "R.Rate";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 160, 260, flags);
                line = "BP1";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 240, 260, flags);
                line = "Time :";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 300, 260, flags);
                line = "BP2 ";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 380, 260, flags);
                line = "Time :";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 440, 260, flags);

                line = "Wt.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2, 290, flags);
                line = "Ht.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 80, 290, flags);
                line = "BMI.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 100, 290, flags);
                line = "CC.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 180, 290, flags);
                line = "CC.IN";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 240, 290, flags);
                line = "CC.EX";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 300, 290, flags);
                line = "Ab.C";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 400, 290, flags);
                line = "H.C.";
                gfx.DrawString(line, fEditS, Brushes.Black, col2 + 460, 290, flags);

                line = "Precaution (Med) _________________________________________ ";
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 290, flags);
                err = "02";
                line = "แพ้ยา/อาหาร/อื่นๆ         ไม่มี";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, 320, flags);
                gfx.DrawString(line, fEdit, Brushes.Black, col4, 320, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2int + 180, 330, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col4 - recx + 200, 330, recx, recy));
                line = "มี ระบุอาการ";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + recx + 10, 350, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2int + 20 - recx, 360, recx, recy));
                gfx.DrawString(line, fEdit, Brushes.Black, col4 + recx + 10, 350, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col4, 360, recx, recy));
                line = "O2 Sat __________        Pain Score __________";
                gfx.DrawString(line, fEdit, Brushes.Black, col31, 350, flags);

                gfx.DrawRectangle(penBorder, 100, 20, 865, 1200);// Border
                gfx.DrawRectangle(penBorder, col5, 20, 760, 1200);// Border

                yPos = 390;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);      //1
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawLine(penBorder, 1550, yPos, 1550, 1020);        //

                //gfx.DrawString("",)
                line = "Staff Note";
                gfx.DrawString(line, famtB, brushBule, 380, yPos, flags);
                line = "Rx";
                gfx.DrawString(line, famtB, brushBule, 980, yPos, flags);
                line = "ใบสั่งยา";
                gfx.DrawString(line, famtB, brushBule, 1280, yPos, flags);
                line = "จำนวนหน่วย";
                gfx.DrawString(line, famtB, brushBule, 1550, yPos, flags);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);

                gfx.DrawLine(penBorder, col3, yPos, x2Right, yPos);

                err = "03";
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("CHIEF COMPLIANT", fEditB, brushBule, 110, yPos, flags);

                gfx.DrawString(vs.DeptCode, fEdit, Brushes.Black, col4 + 100, yPos - 30, flags);
                line = "Medication                       No Medication";
                gfx.DrawString(line, fEdit, Brushes.Black, col4 + 100, yPos, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col4 + 80 - recx - 5, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col4 + 300 - recx, yPos + 10, recx, recy));
                gfx.DrawString(vs.symptom, fEdit, Brushes.Black, 110 + 100, yPos + 50, flags);
                gfx.DrawString(vs.symptom, fEditB, Brushes.Black, col40 + 460, yPos + 50 + 35, flags);
                //btnSavePic
                //line = "อาการ";
                //gfx.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 175, flags);
                //line = "เอกซเรย์ปอด";
                //gfx.DrawString(line, fEditB, Brushes.Black, col40 + 460, yPos + 175, flags);

                //line = "สัมผัสผู้ป่วย ชื่อ";
                //gfx.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 230, flags);
                //line = "Set ";
                //if (chkHI.Checked)
                //{
                //    line = "Set " + drugset.ToUpper();
                //}
                //gfx.DrawString(line, fEditB, Brushes.Black, col40 + 460, yPos + 230, flags);

                //line = "สัมผัสล่าสุด";
                //gfx.DrawString(line, fEditB, Brushes.Black, col2 + 20, yPos + 275, flags);
                //line = "เครื่องวัดออกซิเจน ปลายนิ้ว    1";
                //gfx.DrawString(line, fEditB, Brushes.Black, col40 + 460, yPos + 275, flags);
                //line = "ปรอทวัดอุณหภูมิ     1";
                //gfx.DrawString(line, fEditB, Brushes.Black, col40 + 460, yPos + 305, flags);

                gfx.DrawString("ผลตรวจ ATK screening HI", fEditB, Brushes.Black, col4 + 100, 615, flags);
                gfx.DrawString("ผลบวก พบว่า พบเชื้อ", fEdit, Brushes.Black, 1100, 665, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(1085, 670, recx, recy));
                gfx.DrawString("ผลลบ พบว่า ไม่พบเชื้อ", fEdit, Brushes.Black, 1100, 710, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(1085, 720, recx, recy));

                line = "คำแนะนำ       การออกกำลังกาย               การรับประทานอาหารที่ถูกสัดส่วน";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, yPos + 320, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 105, yPos + 325, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 310, yPos + 325, recx, recy));

                line = "การตรวจสุขภาพประจำปี          การพบแพทย์เฉพาะทาง       อื่นๆ";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos + 365, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 35 - recx, yPos + 375, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 280 - recx, yPos + 375, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 510 - recx, yPos + 375, recx, recy));

                err = "04";
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("PYSICAL EXAM", fEditB, brushBule, 110, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);      //10
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("DIAGNOSIS", fEditB, brushBule, 110, yPos, flags);
                gfx.DrawString("คำแนะนำ", fEditB, brushBule, 680, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);      //15
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("รหัสโรค", fEditB, brushBule, 110, yPos, flags);
                gfx.DrawString("FOLLOW UP", fEditB, brushBule, 680, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);
                gfx.DrawString("ATTENDIND PHYSICAN", fEditB, brushBule, 680, yPos, flags);

                yPos += gapLine;
                gfx.DrawLine(penBorder, 100, yPos, line1Len, yPos);
                gfx.DrawLine(penBorder, col5, yPos, x2Right, yPos);

                //yPos += gapLine;
                err = "05";
                line = "ใบรับรองแพทย์             ไม่มี      มี             Consult      ไม่มี      มี __________________";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + 40, yPos, flags);
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 240 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 305 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 410 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 515 - recx, yPos + 10, recx, recy));
                gfx.DrawRectangle(blackPen, new Rectangle(col2 + 590 - recx, yPos + 10, recx, recy));

                line = "Health Education :";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, imgA4.Height - 90, flags);
                line = "ชื่อผู้รับ _____________________________";
                gfx.DrawString(line, fEdit, Brushes.Black, col2 + 500, imgA4.Height - 90, flags);

                line = "ลงชื่อพยาบาล: _____________________________________";
                gfx.DrawString(line, fEdit, Brushes.Black, col2, imgA4.Height - 60, flags);
                line = "FM-REC-002 (00 10/09/53)(1/1)";
                gfx.DrawString(line, fEditS, Brushes.Black, col2, imgA4.Height - 30, flags);
                gfx.DrawString(line, fEditS, Brushes.Black, col6, imgA4.Height - 30, flags);
                //gfx.DrawString("",)
            }
            //imgA4.Save("aaaa.jpg");
            try
            {
                err = "06";
                Rectangle rectL = new Rectangle(0, 0, 980, imgA4.Height);
                //Bitmap imgL = new Bitmap(980, imgA4.Height);
                Bitmap imgL = new Bitmap(630, imgA4.Width - 960);
                Graphics gfxL = Graphics.FromImage(imgL);
                gfxL.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gfxL.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gfxL.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gfxL.DrawImage(imgA4, 0, 0, rectL, GraphicsUnit.Pixel);

                String filenameS = "";
                filenameS = "000000" + vs.preno;
                filenameS = filenameS.Substring(filenameS.Length - 6);

                Rectangle rectR = new Rectangle(975, 0, imgA4.Width - 960, imgA4.Height);
                //Bitmap imgR = new Bitmap(imgA4.Width - 975, imgA4.Height);
                Bitmap imgR = new Bitmap(502, imgA4.Width - 960);
                Graphics gfxR = Graphics.FromImage(imgR);
                gfxR.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gfxR.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gfxR.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gfxR.DrawImage(imgA4, 0, 0, rectR, GraphicsUnit.Pixel);
                String filenameR = "", path = "", year = "", mon = "", day = "";
                year = vs.VisitDate.Substring(0, 4);
                mon = vs.VisitDate.Substring(5, 2);
                day = vs.VisitDate.Substring(8, 2);
                path = "\\\\" + iniC.pathScanStaffNote + "" + year + "\\" + mon + "\\" + day + "\\";
                //path = iniC.pathlocalStaffNote + "" + year + "\\" + mon + "\\" + day + "\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filenameR = "000000" + vs.preno;
                filenameR = filenameR.Substring(filenameR.Length - 6);
                err = "07";
                if (File.Exists(filenameR))
                {
                    if (!IsFileLocked(filenameR))
                    {
                        File.Delete(filenameR);
                    }
                }
                if (File.Exists(filenameS))
                {
                    if (!IsFileLocked(filenameS))
                    {
                        File.Delete(filenameS);
                    }
                }
                //new LogWriter("e", "genImgStaffNote path filenameS " + path + filenameS);
                imgL.Save(path + filenameS + "S.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                //imgL.Save(filenameS + "R.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                err = "08";
                //new LogWriter("e", "genImgStaffNote path filenameS " + path + filenameR);
                imgR.Save(path + filenameR + "R.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                //imgR.Save(filenameR + "S.JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "genImgStaffNote err" + err + " error " + ex.Message);
                MessageBox.Show("" + ex.Message, "");
            }
        }
        private bool IsFileLocked(string filePath)
        {
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }
        public String checkPaidSSO(String pid, Label lbloading)
        {
            String re = "";
            try
            {
                lbloading.Text = "patient sso01";
                Application.DoEvents();
                PrakunM01 pkunM01 = new PrakunM01();
                pkunM01 = bcDB.prakM01DB.selectByPID(pid);
                if (pkunM01.Social_Card_no.Equals(""))
                {
                    pkunM01 = bcDB.prakM01DB.selectByCardNo(pid);
                    if (pkunM01.Social_Card_no.Equals(""))
                    {
                        pkunM01 = bcDB.prakM01DB.selectOne();
                        re = "ไม่พบ สิทธิ จาก database HIS->" + pkunM01.UploadDate;
                    }
                    else
                    {
                        String txt = pkunM01.PrakanCode.Equals("2210028") ? "สิทธิ บางนา 1" : pkunM01.PrakanCode.Equals("2211006") ? "สิทธิ บางนา 2" : pkunM01.PrakanCode.Equals("2211041") ? "สิทธิ บางนา 5" : "สิทธิ  ที่อื่น";
                        re = "พบ " + txt + " เริ่ม[" + pkunM01.StartDate + "] สิ้นสุด[" + pkunM01.EndDate + "] จาก database HIS->" + pkunM01.UploadDate;
                    }
                }
                else
                {
                    //abel1.Text = prak.PrakanCode.Equals("2210028") ? "สิทธิ บางนา 1" : prak.PrakanCode.Equals("2211006") ? "สิทธิ บางนา 2" : prak.PrakanCode.Equals("2211041") ? "สิทธิ บางนา 5" : "สิทธิ  ที่อื่น";
                    String txt = pkunM01.PrakanCode.Equals("2210028") ? "สิทธิ บางนา 1" : pkunM01.PrakanCode.Equals("2211006") ? "สิทธิ บางนา 2" : pkunM01.PrakanCode.Equals("2211041") ? "สิทธิ บางนา 5" : "สิทธิ  ที่อื่น";
                    re = "พบ " + txt + " เริ่ม[" + pkunM01.StartDate + "] สิ้นสุด[" + pkunM01.EndDate + "] จาก database HIS->" + pkunM01.UploadDate;
                }
                lbloading.Text = "patient sso02";
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmReception checkPaidSSO " + ex.Message);
                bcDB.insertLogPage(userId, "BangnaControl", "checkPaidSSO ", ex.Message);
            }
            return re;
        }
        public String checkPaidSSO(String pid)
        {
            String re = "";
            try
            {
                PrakunM01 pkunM01 = new PrakunM01();
                pkunM01 = bcDB.prakM01DB.selectByPID(pid);
                if (pkunM01.Social_Card_no.Equals(""))
                {
                    pkunM01 = bcDB.prakM01DB.selectByCardNo(pid);
                    if (pkunM01.Social_Card_no.Equals("")) { re = ""; }
                    else { re = pkunM01.PrakanCode.Equals("2210028") ? "44" : pkunM01.PrakanCode.Equals("2211006") ? "45" : pkunM01.PrakanCode.Equals("2211041") ? "46" : ""; }
                }
                else { re = pkunM01.PrakanCode.Equals("2210028") ? "44" : pkunM01.PrakanCode.Equals("2211006") ? "45" : pkunM01.PrakanCode.Equals("2211041") ? "46" : ""; }

            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmReception checkPaidSSO " + ex.Message);
                bcDB.insertLogPage(userId, "BangnaControl", "checkPaidSSO ", ex.Message);
            }
            return re;
        }
        public async Task<DoeAlienList> GetDoeAien(String alcode)
        {
            DoeAlienList doeAlienList = new DoeAlienList();
            DoeAlienRequest doea = new DoeAlienRequest();
            doea.reqcode = "";
            doea.alcode = alcode;
            if (alcode.Trim().Length <= 0)
            {
                return doeAlienList;
            }
            doea.token = iniC.doealientoken;
            var url = iniC.urlbangnadoe;
            String jsonEpi = JsonConvert.SerializeObject(doea, Formatting.Indented);
            jsonEpi = jsonEpi.Replace("[" + Environment.NewLine + "    null" + Environment.NewLine + "  ]", "[]");
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url + "?regcode=&alcode=" + alcode);
                    if (response.StatusCode.ToString().ToUpper().Equals("OK"))
                    {
                        String content1 = await response.Content.ReadAsStringAsync();
                        DoeAlientResponse doear = JsonConvert.DeserializeObject<DoeAlientResponse>(content1);
                        if (doear.statuscode.Equals("-100")) return doeAlienList;
                        doeAlienList.empname = doear.empname; doeAlienList.wkaddress = doear.wkaddress; doeAlienList.btname = doear.btname;
                        int i = 1, j = 1;
                        foreach (var alien in doear.alienlist)
                        {
                            doeAlienList.alcode = alien.alcode;
                            doeAlienList.altype = alien.altype;
                            doeAlienList.alprefix = alien.alprefix;
                            doeAlienList.alprefixen = alien.alprefixen;
                            doeAlienList.alnameen = alien.alnameen;
                            doeAlienList.alsnameen = alien.alsnameen;
                            doeAlienList.albdate = alien.albdate;
                            doeAlienList.algender = alien.algender;
                            doeAlienList.alnation = alien.alnation;
                            doeAlienList.alposid = alien.alposid;
                            i++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            return doeAlienList;
        }
        public static string GetFileTypeFormMemoryStream(MemoryStream stream)
        {
            if (stream == null || stream.Length < 4)
                return "Unknown";

            stream.Position = 0;
            byte[] header = new byte[8];
            stream.Read(header, 0, header.Length);
            stream.Position = 0; // Reset position

            // PDF
            if (header[0] == 0x25 && header[1] == 0x50 && header[2] == 0x44 && header[3] == 0x46)
                return "PDF";

            // JPEG
            if (header[0] == 0xFF && header[1] == 0xD8)
                return "JPEG";

            // PNG
            if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
                return "PNG";

            // GIF
            if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46)
                return "GIF";

            // BMP
            if (header[0] == 0x42 && header[1] == 0x4D)
                return "BMP";

            return "Unknown";
        }
        public bool IsSameImage(Image img1, Image img2)
        {
            if (img1 == null || img2 == null) return false;
            using (var ms1 = new MemoryStream())
            using (var ms2 = new MemoryStream())
            {
                img1.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                img2.Save(ms2, System.Drawing.Imaging.ImageFormat.Png);

                var hash1 = GetMD5Hash(ms1.ToArray());
                var hash2 = GetMD5Hash(ms2.ToArray());

                return hash1.SequenceEqual(hash2);
            }
        }

        private byte[] GetMD5Hash(byte[] data)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                return md5.ComputeHash(data);
            }
        }
        public String chkSpace(String txt)
        {
            if (txt == null) return "";
            return txt.Trim().Length>0 ? txt.Trim()+" " : "";
        }
        public void InitFonts()
        {
            fEdit = FontHelper.Create(iniC.grdViewFontName, (grdViewFontSize), FontStyle.Regular);
            fEditB = FontHelper.Create(iniC.grdViewFontName, (grdViewFontSize), FontStyle.Bold);
            fEditS = FontHelper.Create(iniC.grdViewFontName, (grdViewFontSize - 1), FontStyle.Regular);
            fEdit3B = FontHelper.Create(iniC.grdViewFontName, (grdViewFontSize + 3), FontStyle.Bold);
            fEdit5B = FontHelper.Create(iniC.grdViewFontName, (grdViewFontSize + 5), FontStyle.Bold);
            fEditS1 = FontHelper.Create(iniC.grdViewFontName, (grdViewFontSize -1), FontStyle.Regular);
            fEdit2 = FontHelper.Create(iniC.grdViewFontName, (grdViewFontSize + 2), FontStyle.Regular);
            fEdit2B = FontHelper.Create(iniC.grdViewFontName, (grdViewFontSize + 2), FontStyle.Regular);
            famtB14 = FontHelper.Create(iniC.grdViewFontName, (grdViewFontSize + 14), FontStyle.Regular);
            famtB30 = FontHelper.Create(iniC.grdViewFontName, (grdViewFontSize + 30), FontStyle.Regular);

            famt1 = FontHelper.Create(iniC.pdfFontName, (pdfFontSize + 1), FontStyle.Regular);
            famt2 = FontHelper.Create(iniC.pdfFontName, (pdfFontSize + 2), FontStyle.Regular);
            famt2B = FontHelper.Create(iniC.pdfFontName, (pdfFontSize + 2), FontStyle.Bold);
            famt4B = FontHelper.Create(iniC.pdfFontName, (pdfFontSize + 4), FontStyle.Bold);
            famt2BL = FontHelper.Create(iniC.pdfFontName, (pdfFontSize + 2), FontStyle.Underline);
            famt5 = FontHelper.Create(iniC.pdfFontName, (pdfFontSize + 5), FontStyle.Regular);
            famt5B = FontHelper.Create(iniC.pdfFontName, (pdfFontSize + 5), FontStyle.Bold);
            famt5BL = FontHelper.Create(iniC.pdfFontName, (pdfFontSize + 5), FontStyle.Bold);
            famt7 = FontHelper.Create(iniC.pdfFontName, (pdfFontSize + 7), FontStyle.Regular);

            famt7B = FontHelper.Create(iniC.pdfFontName, (pdfFontSize + 7), FontStyle.Regular);
            ftotal = FontHelper.Create(iniC.pdfFontName, (pdfFontSize + 7), FontStyle.Regular);
            fPrnBil = FontHelper.Create(iniC.pdfFontName, (pdfFontSize + 7), FontStyle.Regular);

            fque = FontHelper.Create(iniC.pdfFontName, (pdfFontSize), FontStyle.Regular);
            fqueB = FontHelper.Create(iniC.pdfFontName, (pdfFontSize), FontStyle.Bold);
            fPDF = FontHelper.Create(iniC.pdfFontName, (pdfFontSize), FontStyle.Regular);
            fPDFs2 = FontHelper.Create(iniC.pdfFontName, (pdfFontSize -2), FontStyle.Regular);
            fPDFs6 = FontHelper.Create(iniC.pdfFontName, (pdfFontSize -6), FontStyle.Regular);
            fPDFs8 = FontHelper.Create(iniC.pdfFontName, (pdfFontSize -8), FontStyle.Regular);
            fPDFl2 = FontHelper.Create(iniC.pdfFontName, (pdfFontSize + 2), FontStyle.Regular);

            // ... อื่น ๆ
        }
    }
}