using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Themes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmEpidem : Form
    {
        /*
         * ต้องเปลี่ยน dotnet เป็น 4.6 เพราะ ใช้ HttpClient;
         * 
         */ 
        BangnaControl bc;
        Font fEdit, fEditB, fEdit1, fEdit1B, fPrnBil, famtB14, fEditS, fEdit3B, fEdit5B;
        EpidemAPIDB epiApiDB;

        C1ThemeController theme1;
        C1FlexGrid grfData, grfBn5, grfBn2, grfSend;
        String authen = "", symptom="";
        List<EpidemSymptomType> lSympT;
        List<EpidemPersonStatus> lPersS;
        List<EpidemNationality> lNati;
        List<EpidemAccommodation> lAccom;
        List<EpidemMarital> lMari;
        List<EpidemPersonType> lPersT;
        List<EpidemProvince> lProv;
        List<EpidemPrefixType> lPref;
        List<EpidemTmlt> lTmlt;
        List<EpidemVaccManu> lVaccManu;
        List<EpidemGender> lGender;
        List<EpidemPersonRick> lPersR;
        List<EpidemLabComfirmType> lLabConT;
        List<EpidemCovidReasonType> lReasT;
        List<EpidemCovidSpcmPlace> lSpacmP;
        List<EpidemCovidIsolatePlace> lIsoP;
        List<EpidemCluster> lClus;

        int colId = 1, colhn=2, colVsDate=3, colpreno=4, colreqyear=5, colreqdate=6, collabcode=7, colcid=8, colpassport=9, colmobile=10, colhosname=11, colcate=12, colsatcode=13, colname=14, collabdate=15, collabplace=16, collabresult=17, colegene=18, colrdrp=19, colngene=20, colnation=21, coldob=22, colsex=23, coladdr1=24, coltambon=25, colamphur=26, colprov=27, colAge=28, colNationAPI=29, colStatusSingle=30, colfirstname=31, collastname=32, colprefix=33, colgender=34, colLabName = 35;
        int colPersCID = 1, colPersPassport = 2, colPersPrefix = 3, colPersFirstname = 4, colPersLastname = 5, colPersNati = 6, colPersGender = 7, colPersDOB = 8, colPersAgeY = 9, colPersAgeM = 10, colPersAgeD = 11, colPersMarital = 12, colPersAddr = 13, colPersMoo = 14, colPersRoad = 15, colPersChw = 16, colPersAmp = 17, colPersTmb = 18, colPersMobile = 19, colPersOccu = 20;
        int colRptGUID = 21, colRptGrpId = 22, colRptHospCode = 23, colRptRptDtm = 24, colRptOnDt=25, colRptTreatDt = 26, colRptDiagDt=27, colRptInfor = 28, colRptPrin = 29, colRptDiagICD10 = 30, colRptPers = 31, colRptSymp = 32, colRptPregn = 33, colRptRespi = 34, colRptAccom = 35, colRptVacci = 36, colRptArea = 37, colRptWorker = 38, colRptClosed = 39, colRptOccup = 40, colRptTravel = 41, colRptRick = 42, colRptAddr = 43, colRptMoo = 44, colRptRoad = 45, colRptChw = 46, colRptAmp = 47, colRptTmb = 48, colRptLat = 49, colRptLon = 50, colRptIsoChw = 51, colRptIsoPlace = 52, colRptPttt = 53, colRptClus = 54, colRptClusLat = 55, colRptClusLon = 56;
        int colLabCon = 55, colLabRptDt = 56, colLabRes = 57, colLabSpecDt = 58, colLabSpecPlac = 59, colLabReason = 60, colLabRefCode = 61, colLabRefName = 62, colLabTmlt = 63;
        int ColVaccHospCod = 64, colVaccDate = 65, colVaccDose = 66, colVaccManu = 67, colEpidemId=68;

        Boolean pageLoad = false;
        public FrmEpidem(BangnaControl bc)
        {
            //MessageBox.Show("FrmEpidem ", "");
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            theme1 = new C1ThemeController();
            lSympT = new List<EpidemSymptomType>();
            lPersS = new List<EpidemPersonStatus>();
            lNati = new List<EpidemNationality>();
            lAccom = new List<EpidemAccommodation>();
            lMari = new List<EpidemMarital>();
            lPersT = new List<EpidemPersonType>();
            lProv = new List<EpidemProvince>();
            lPref = new List<EpidemPrefixType>();
            lTmlt = new List<EpidemTmlt>();
            lVaccManu = new List<EpidemVaccManu>();
            lGender = new List<EpidemGender>();
            lPersR = new List<EpidemPersonRick>();
            lLabConT = new List<EpidemLabComfirmType>();
            lReasT = new List<EpidemCovidReasonType>();
            lSpacmP = new List<EpidemCovidSpcmPlace>();
            lIsoP = new List<EpidemCovidIsolatePlace>();
            lClus = new List<EpidemCluster>();

            epiApiDB = new EpidemAPIDB(bc.conn);

            initGrfData();
            initGrfBn5();
            initGrfBn2();
            initGrfSend();
            btnAuthen.Click += BtnAuthen_Click;
            btnGetData.Click += BtnGetData_Click;
            btnGenData.Click += BtnGenData_Click;
            btn5Save.Click += Btn5Save_Click;
            btnBn5SendAPI.Click += BtnBn5SendAPI_Click;
            btnBn5Search.Click += BtnBn5Search_Click;

            cboBn5Chw.SelectedItemChanged += CboBn5Chw_SelectedItemChanged;
            cboBn5Amp.SelectedItemChanged += CboBn5Amp_SelectedItemChanged;
            cboBn5EpidemChw.SelectedItemChanged += CboBn5EpidemChw_SelectedItemChanged;
            cboBn5EpidemAmp.SelectedItemChanged += CboBn5EpidemAmp_SelectedItemChanged;
            txtBn5Lat.Value = "0";
            txtBn5Lon.Value = "0";
            txtBn5ClusterLat.Value = "0";
            txtBn5ClusterLon.Value = "0";

            setGrdBn5();
            setGrdSended();
            setTheme();
            pageLoad = false;
            btnBn5SendAPI.Enabled = false;
        }
        private void setTheme()
        {
            theme1.SetTheme(panel5, bc.iniC.themeApp);
            theme1.SetTheme(panel7, bc.iniC.themeApp);
            theme1.SetTheme(panel8, bc.iniC.themeApp);
            theme1.SetTheme(panel9, bc.iniC.themeApp);
            foreach (Control c in panel7.Controls)
            {
                if(c.GetType().Name.Equals("Label"))
                {
                    theme1.SetTheme(c, bc.iniC.themeApp);
                }
            }
            foreach (Control c in panel8.Controls)
            {
                if (c.GetType().Name.Equals("Label"))
                {
                    theme1.SetTheme(c, bc.iniC.themeApp);
                }
            }
            foreach (Control c in panel9.Controls)
            {
                if (c.GetType().Name.Equals("Label"))
                {
                    theme1.SetTheme(c, bc.iniC.themeApp);
                }
            }
        }
        private void BtnBn5Search_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinetByID1(txtBn5CID.Text.Trim());
            label90.Text = ptt.TUMName;
            label89.Text = ptt.AMPName;
            label88.Text = ptt.CHWName;
        }

        private void CboBn5EpidemAmp_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!pageLoad)
            {
                pageLoad = true;
                String item = cboBn5EpidemAmp.SelectedItem != null ? ((ComboBoxItem)cboBn5EpidemAmp.SelectedItem).Value : "";
                if (item.Length > 0)
                {
                    String chwcode = cboBn5EpidemChw.SelectedItem != null ? ((ComboBoxItem)cboBn5EpidemChw.SelectedItem).Value : "";
                    bc.bcDB.flocaDB.setCboTumbon(cboBn5EpidemTmb, chwcode, item);
                }
                pageLoad = false;
            }
        }

        private void CboBn5EpidemChw_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!pageLoad)
            {
                pageLoad = true;
                String item = cboBn5EpidemChw.SelectedItem != null ? ((ComboBoxItem)cboBn5EpidemChw.SelectedItem).Value : "";
                if (item.Length > 0)
                {
                    cboBn5EpidemTmb.Clear();
                    bc.bcDB.flocaDB.setCboAmphur(cboBn5EpidemAmp, item);
                }
                pageLoad = false;
            }
        }

        private void CboBn5Amp_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!pageLoad)
            {
                pageLoad = true;
                String item = cboBn5Amp.SelectedItem != null ? ((ComboBoxItem)cboBn5Amp.SelectedItem).Value : "";
                if (item.Length > 0)
                {
                    String chwcode = cboBn5Chw.SelectedItem != null ? ((ComboBoxItem)cboBn5Chw.SelectedItem).Value : "";
                    bc.bcDB.flocaDB.setCboTumbon(cboBn5Tmb, chwcode, item);
                }
                pageLoad = false;
            }
        }

        private void CboBn5Chw_SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!pageLoad)
            {
                pageLoad = true;
                String item = cboBn5Chw.SelectedItem != null ? ((ComboBoxItem)cboBn5Chw.SelectedItem).Value : "";
                if (item.Length > 0)
                {
                    cboBn5Tmb.Clear();
                    bc.bcDB.flocaDB.setCboAmphur(cboBn5Amp, item);
                }
                pageLoad = false;
            }
        }

        private async void BtnBn5SendAPI_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            EpidemAPI epiapi = new EpidemAPI();
            //EpidemHospital epiHosp = new EpidemHospital();
            //EpidemPerson epiPers = new EpidemPerson();
            //EpidemReport epiRpt = new EpidemReport();
            //EpidemLabReport epiLabRpt = new EpidemLabReport();
            //EpidemVaccination epiVacc = new EpidemVaccination();
            EpidemSendEPIDEM epiSend = new EpidemSendEPIDEM();

            String guid = grfBn5[grfBn5.Row, colRptGUID].ToString();
            epiapi = epiApiDB.selectByGUID(guid);
            epiSend.hospital.hospital_code = txtHosp5Code.Text.Trim();
            epiSend.hospital.hospital_name = txtHosp5Name.Text.Trim();
            epiSend.hospital.his_identifier = txtHis5Iden.Text.Trim();
            //string jsonEpiHost = JsonConvert.SerializeObject(epiHosp, Formatting.Indented);

            epiSend.person.cid = txtBn5CID.Text.Trim();
            epiSend.person.passport_no = txtBn5Passport.Text.Trim();
            epiSend.person.prefix = cboBn5Prefix.SelectedItem != null ? ((ComboBoxItem)cboBn5Prefix.SelectedItem).Value : "";
            epiSend.person.first_name = txtbn5Firstname.Text.Trim();
            epiSend.person.last_name = txtBn5Lastname.Text.Trim();
            epiSend.person.nationality = cboBn5Nat.SelectedItem != null ? ((ComboBoxItem)cboBn5Nat.SelectedItem).Value : "";
            epiSend.person.gender = int.Parse(cboBn5Gender.SelectedItem != null ? ((ComboBoxItem)cboBn5Gender.SelectedItem).Value : "");
            epiSend.person.birth_date = txtBn5Birthdate.Text.Trim();
            epiSend.person.age_y = int.Parse(txtBn5AgeY.Text.Trim());
            epiSend.person.age_m = int.Parse(txtBn5AgeM.Text.Trim());
            epiSend.person.age_d = int.Parse(txtBn5AgeD.Text.Trim());
            if (!cboBn5Mari.SelectedItem.ToString().Equals(""))
            {
                epiSend.person.marital_status_id = int.Parse(cboBn5Mari.SelectedItem != null ? ((ComboBoxItem)cboBn5Mari.SelectedItem).Value : "");
            }
            else
            {
                epiSend.person.marital_status_id = 1;
            }
            epiSend.person.address = txtBn5Addr.Text.Trim();
            epiSend.person.moo = txtBn5Moo.Text.Trim();
            epiSend.person.road = txtBn5Road.Text.Trim();
            epiSend.person.chw_code = cboBn5Chw.SelectedItem != null ? ((ComboBoxItem)cboBn5Chw.SelectedItem).Value : "";
            epiSend.person.amp_code = cboBn5Amp.SelectedItem != null ? ((ComboBoxItem)cboBn5Amp.SelectedItem).Value : "";
            epiSend.person.tmb_code = cboBn5Tmb.SelectedItem != null ? ((ComboBoxItem)cboBn5Tmb.SelectedItem).Value : "";
            epiSend.person.mobile_phone = txtBn5Mobile.Text.Trim();
            epiSend.person.occupation = cbotxtBn5Occu.SelectedItem != null ? ((ComboBoxItem)cbotxtBn5Occu.SelectedItem).Value : "";
            //string jsonEpiPers = JsonConvert.SerializeObject(epiPers, Formatting.Indented);

            epiSend.epidem_report.epidem_report_guid = "{"+txtBn5Guid.Text.Trim()+"}";
            epiSend.epidem_report.epidem_report_group_id = cboBn5Grp.SelectedItem != null ? ((ComboBoxItem)cboBn5Grp.SelectedItem).Value : "";
            epiSend.epidem_report.treated_hospital_code = txtBn5HospCode.Text.Trim();
            epiSend.epidem_report.report_datetime = txtBn5RptDate.Text.Trim();
            epiSend.epidem_report.onset_date = txtBn5OnsetDate.Text.Trim();
            epiSend.epidem_report.treated_date = txtBn5TreatDate.Text.Trim();
            epiSend.epidem_report.diagnosis_date = txtBn5DiagDate.Text.Trim();
            epiSend.epidem_report.informer_name = txtBn5Informer.Text.Trim();
            epiSend.epidem_report.principal_diagnosis_icd10 = txtBn5ICD10.Text.Trim();
            epiSend.epidem_report.diagnosis_icd10_list = txtBn5ICD10List.Text.Trim();
            epiSend.epidem_report.epidem_person_status_id = int.Parse(cboBn5PersonStatus.SelectedItem != null ? ((ComboBoxItem)cboBn5PersonStatus.SelectedItem).Value : "");
            epiSend.epidem_report.epidem_symptom_type_id = int.Parse(cboBn5Symptom.SelectedItem != null ? ((ComboBoxItem)cboBn5Symptom.SelectedItem).Value : "");
            epiSend.epidem_report.pregnant_status = cboBn5Pregnant.SelectedItem != null ? ((ComboBoxItem)cboBn5Pregnant.SelectedItem).Value : "";
            epiSend.epidem_report.respirator_status = cboBn5Respi.SelectedItem != null ? ((ComboBoxItem)cboBn5Respi.SelectedItem).Value : "";
            epiSend.epidem_report.epidem_accommodation_type_id = int.Parse(cboBn5Accom.SelectedItem != null ? ((ComboBoxItem)cboBn5Accom.SelectedItem).Value : "");
            epiSend.epidem_report.vaccinated_status = cboBn5Vacc.SelectedItem != null ? ((ComboBoxItem)cboBn5Vacc.SelectedItem).Value : "";
            epiSend.epidem_report.exposure_epidemic_area_status = cboBn5RptArea.SelectedItem != null ? ((ComboBoxItem)cboBn5RptArea.SelectedItem).Value : "";
            epiSend.epidem_report.exposure_healthcare_worker_status = cboBn5RptWorker.SelectedItem != null ? ((ComboBoxItem)cboBn5RptWorker.SelectedItem).Value : "";
            epiSend.epidem_report.exposure_closed_contact_status = cboBn5RptClosed.SelectedItem != null ? ((ComboBoxItem)cboBn5RptClosed.SelectedItem).Value : "";
            epiSend.epidem_report.exposure_occupation_status = cboBn5Occu.SelectedItem != null ? ((ComboBoxItem)cboBn5Occu.SelectedItem).Value : "";
            epiSend.epidem_report.exposure_travel_status = cboBn5RptTravel.SelectedItem != null ? ((ComboBoxItem)cboBn5RptTravel.SelectedItem).Value : "";
            epiSend.epidem_report.risk_history_type_id = int.Parse(cboBn5RptRickHistoryType.SelectedItem != null ? ((ComboBoxItem)cboBn5RptRickHistoryType.SelectedItem).Value : "");
            epiSend.epidem_report.epidem_address = txtBn5EpidemAddr.Text.Trim();
            epiSend.epidem_report.epidem_moo = txtBn5EpidemMoo.Text.Trim();
            epiSend.epidem_report.epidem_road = txtBn5EpidemRoad.Text.Trim();
            epiSend.epidem_report.epidem_tmb_code = cboBn5EpidemTmb.SelectedItem != null ? ((ComboBoxItem)cboBn5EpidemTmb.SelectedItem).Value : "";
            epiSend.epidem_report.epidem_amp_code = cboBn5EpidemAmp.SelectedItem != null ? ((ComboBoxItem)cboBn5EpidemAmp.SelectedItem).Value : "";
            epiSend.epidem_report.epidem_chw_code = cboBn5EpidemChw.SelectedItem != null ? ((ComboBoxItem)cboBn5EpidemChw.SelectedItem).Value : "";
            //txtBn5Lat.Value = txtBn5Lat.Text.Length == 0 ? "0": txtBn5Lat.Text;
            epiSend.epidem_report.location_gis_latitude = int.Parse(txtBn5Lat.Text.Length == 0 ? "0" : txtBn5Lat.Text.Trim());
            //epiSend.epidem_report.location_gis_latitude = float.Parse("0");
            epiSend.epidem_report.location_gis_longitude = int.Parse(txtBn5Lon.Text.Trim());
            epiSend.epidem_report.isolate_chw_code = cboBn5IsoChw.SelectedItem != null ? ((ComboBoxItem)cboBn5IsoChw.SelectedItem).Value : "";
            epiSend.epidem_report.isolate_place_id = int.Parse(cboBn5IsoPlace.SelectedItem != null ? ((ComboBoxItem)cboBn5IsoPlace.SelectedItem).Value : "");
            epiSend.epidem_report.patient_type = cboBn5PttType.SelectedItem != null ? ((ComboBoxItem)cboBn5PttType.SelectedItem).Value : "";
            epiSend.epidem_report.epidem_covid_cluster_type_id = 1;
            epiSend.epidem_report.cluster_latitude = int.Parse(txtBn5ClusterLat.Text.Trim());
            epiSend.epidem_report.cluster_longitude = int.Parse(txtBn5ClusterLon.Text.Trim());
            //string jsonEpiRpt = JsonConvert.SerializeObject(epiRpt, Formatting.Indented);
            epiSend.epidem_report.risk_history_type_id = 14;

            epiSend.lab_report.epidem_lab_confirm_type_id = int.Parse(cboBn5LabConf.SelectedItem != null ? ((ComboBoxItem)cboBn5LabConf.SelectedItem).Value : "");
            epiSend.lab_report.lab_report_date = txtBn5LabRptDt.Text.Trim();
            epiSend.lab_report.lab_report_result = cboBn5LabRes.SelectedItem != null ? ((ComboBoxItem)cboBn5LabRes.SelectedItem).Value : "";
            epiSend.lab_report.specimen_date = txtBn5LabSpeciDt.Text.Trim();
            epiSend.lab_report.specimen_place_id = int.Parse(cboBn5LabPlace.SelectedItem != null ? ((ComboBoxItem)cboBn5LabPlace.SelectedItem).Value : "");
            epiSend.lab_report.tests_reason_type_id = int.Parse(cboBn5LabReas.SelectedItem != null ? ((ComboBoxItem)cboBn5LabReas.SelectedItem).Value : "");
            epiSend.lab_report.lab_his_ref_code = txtBn5LabCode.Text.Trim();
            epiSend.lab_report.lab_his_ref_name = txtBn5LabName.Text.Trim();
            epiSend.lab_report.tmlt_code = cboBn5LabTmlt.SelectedItem != null ? ((ComboBoxItem)cboBn5LabTmlt.SelectedItem).Value : "";
            //string jsonEpiLabRpt = JsonConvert.SerializeObject(epiLabRpt, Formatting.Indented);
            //EpidemVaccination vacc = new EpidemVaccination();
            //epiSend.vaccination[0] = vacc;
            //epiSend.vaccination.vaccine_hospital_code = "";
            //epiSend.vaccination.vaccine_date = "";
            //epiSend.vaccination.dose = "";
            //epiSend.vaccination.vaccine_manufacturer = "";
            //string jsonEpiVacc = JsonConvert.SerializeObject(epiVacc, Formatting.Indented);

            if (epiSend.epidem_report.vaccinated_status == null)
            {
                MessageBox.Show("vaccinated_status null", "");
                return;
            }
            if (epiSend.epidem_report.vaccinated_status.Equals(""))
            {
                MessageBox.Show("vaccinated_status", "");
                return;
            }
            if (epiSend.epidem_report.risk_history_type_id<=0)
            {
                MessageBox.Show("risk_history_type_id", "");
                return;
            }
            if (epiSend.epidem_report.exposure_healthcare_worker_status.Length<=0)
            {
                MessageBox.Show("exposure_healthcare_worker_status", "");
                return;
            }
            if (epiSend.epidem_report.epidem_report_group_id.Length <= 0)
            {
                MessageBox.Show("epidem_report_group_id", "");
                return;
            }

            String API_SECRET = txtAPISecret.Text.Trim();
            String api_key = txtPassword.Text.Trim();
            String user = txtUser.Text.Trim();
            String hospital_code = txtHospCode.Text.Trim();
            String password = "";
            password = HmacSHA256(API_SECRET, api_key);
            password = password.ToUpper();
            var url = "https://cvp1.moph.go.th/token?Action=get_moph_access_token&user=" + user + "&password_hash=" + password + "&hospital_code=" + hospital_code;
            using (HttpClient httpClient = new HttpClient())
            {
                //var result = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("user", user),
                    new KeyValuePair<string, string>("password_hash", password),
                    new KeyValuePair<string, string>("hospital_code", hospital_code)
                });
                HttpResponseMessage response = await httpClient.PostAsync(url, content);
                authen = response.Content.ReadAsStringAsync().Result;
            }
            url = "https://epidemcenter.moph.go.th/epidem/api/SendEPIDEM";
            String jsonEpi = JsonConvert.SerializeObject(epiSend, Formatting.Indented);
            jsonEpi = jsonEpi.Replace("["+Environment.NewLine+ "    null" + Environment.NewLine + "  ]", "[]");
            
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authen);
                var content = new StringContent(jsonEpi, Encoding.UTF8, "application/json");
                var result = httpClient.PostAsync(url, content).Result;
                if (result.StatusCode.ToString().ToUpper().Equals("OK"))
                {
                    epiApiDB.updateStatusSendSucess(txtID.Text.Trim());
                    setGrdBn5();
                    setGrdSended();
                }
                else
                {
                    MessageBox.Show("error send "+result.StatusCode, "");
                }
            }
        }

        private void Btn5Save_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            EpidemAPI epiapi1 = new EpidemAPI();
            epiapi1 = getEpidemApiBn5();
            String re = epiApiDB.insertEpidem(epiapi1);
            epiapi1 = new EpidemAPI();
            epiapi1 = epiApiDB.selectByGUID(re);
            setControlBn5ByEpi(epiapi1);
            setGrdBn5();
        }
        private void BtnGenData_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //grfBn5.Clear(ClearFlags.Content);
            //grfBn2.Clear(ClearFlags.Content);
            //grfBn5.Rows.Count = 1;
            //grfBn2.Rows.Count = 1;
            int i = 0;
            EpidemAPI epiapi = new EpidemAPI();
            String err = "";
            foreach (Row row in grfData.Rows)
            {
                i++;
                try
                {
                    err = "00";
                    EpidemPerson pers = new EpidemPerson();
                    EpidemLabReport lab = new EpidemLabReport();
                    EpidemVaccination vacc = new EpidemVaccination();
                    EpidemReport rpt = new EpidemReport();
                    if (row[colhosname] == null) continue;
                    if (row[colhosname].ToString().Equals("bangna2") || row[colhosname].ToString().Equals("bangna5"))
                    {
                        String[] age = row[colAge].ToString().Split('.');
                        err = "000 "+ row[colAge].ToString();
                        if (age.Length == 3)
                        {
                            pers.age_y = int.Parse(age[0].Equals("") ? "0" : age[0]);
                            pers.age_m = int.Parse(age[1].Equals("") ? "0" : age[1]);
                            pers.age_d = int.Parse(age[2].Equals("") ? "0":age[2]);
                        }
                        else if (age.Length == 2)
                        {
                            pers.age_y = 0;
                            pers.age_m = int.Parse(age[0].Equals("") ? "0" : age[0]);
                            pers.age_d = int.Parse(age[1].Equals("") ? "0" : age[1]);
                        }
                        else if (age.Length == 1)
                        {
                            pers.age_y = 0;
                            pers.age_m = 0;
                            pers.age_d = int.Parse(age[0].Equals("") ? "0" : age[0]);
                        }
                        pers.cid = row[colcid].ToString();
                        pers.passport_no = "";
                        pers.prefix = row[colprefix].ToString();
                        pers.first_name = row[colfirstname].ToString();
                        pers.last_name = row[collastname].ToString();
                        pers.nationality = "";
                        pers.gender = int.Parse(row[colgender].ToString());
                        pers.birth_date = row[coldob] != null ? row[coldob].ToString() : "";
                        err = "001";
                        pers.marital_status_id = 5;
                        pers.address = row[coladdr1].ToString();
                        pers.moo = "";
                        pers.road = "";
                        pers.chw_code = row[colprov].ToString();
                        pers.amp_code = row[colamphur].ToString();
                        pers.tmb_code = row[coltambon].ToString();
                        pers.mobile_phone = row[colmobile].ToString();
                        pers.occupation = "";
                        err = "01";
                        rpt.epidem_report_guid = epiapi.getGUIDID();
                        rpt.epidem_report_group_id = "92";
                        rpt.treated_hospital_code = txtHospCode.Text.Trim();
                        rpt.report_datetime = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + ".000";   //วันที่/เวลา ที่รายงานโรค "2021-12-12T09:00:00.000"                    **UTC + 7
                        rpt.onset_date = row[colVsDate].ToString();         //วันที่เริ่มมีอาการ (ค.ศ.) YYY-MM-DD
                        rpt.treated_date = row[collabdate].ToString();      //วันที่เริ่มรักษา (ค.ศ.)  YYYY-MM-DD
                        rpt.diagnosis_date = row[collabdate].ToString();    //วันที่วินิจฉัยโรค  (ค.ศ.) YYY-MM-DD
                        rpt.informer_name = "นายเอกภพ  เพลินธรรม";
                        rpt.principal_diagnosis_icd10 = "U071"; //รหัส ICD10 ที่เป็นโรค โควิด-19                    ***รหัสที่บ่งชี้ว่าเป็นโรค Covid - 19 ไม่ว่าจะเป็นโรคหลัก หรือโรคร่วม หากพบรหัสนี้จะส่งข้อมูลเข้าระบบ
                        rpt.diagnosis_icd10_list = "J149";      //รหัส ICD10 ผลการวินิจฉัยทั้งหมดของผู้ป่วยรายนี้ 
                        rpt.epidem_person_status_id = 3;      //สถานะผู้ป่วย ณ ตอนรายงาน                    (1 = หาย, 2 = ตาย, 3 = ยังรักษาอยู่, 4 = ไม่ทราบ)
                        rpt.epidem_symptom_type_id = 2;       //อาการที่แสดง     (1 = ไม่มีอาการ, 2 = มีอาการที่ ไม่เกี่ยวข้องกับระบบทางเดินหายใจ , 3 = มีอาการที่เกี่ยวกับระบบทาง เดินหายใจ เช่น ปอดบวม)
                        rpt.pregnant_status = "N";              //สถานะการตั้งครรภ์                     (Y = ตั้งครรภ์, N = ไม่ตั้งครรภ์)
                        rpt.respirator_status = "N";            //ใส่เครื่องช่วยหายใจ                     (Y = ใส่, N = ไม่ใส่)
                        rpt.epidem_accommodation_type_id = 5;         //ลักษณะที่อยู่อาศัย     (1 = บ้านเดี่ยว,     2 = ตึกแถว / ทาวน์เฮ้าส์,    3 = หอพัก / คอนโด / ห้องเช่า,   4 = พักห้องรวมกับคนจำนวนมาก,  5 = อื่นๆ)

                        rpt.vaccinated_status = "";             //มีประวัติการได้รับวัคซีน Y / N
                        rpt.exposure_epidemic_area_status = ""; //เคยอาศัยอยู่หรือเดินทางมาจากพื้นที่มีการระบาด Y / N
                        rpt.exposure_healthcare_worker_status = "";     //เป็นบุคลากรทางการแพทย์ ที่เกี่ยวข้องกับการรักษา                    ผู้ป่วย Y / N
                        rpt.exposure_closed_contact_status = "";        //ได้อยู่ใกล้ชิดกับผู้ที่ยืนยันว่าเป็นโรค Covid-19 หรือไม่ Y / N
                        rpt.exposure_occupation_status = "";        //ทำงานในอาชีพที่สัมผัสใกล้ชิดกับนัก ท่องเที่ยวต่างชาติหรือกลุ่มเสี่ยงสูง Y/ N
                        rpt.risk_history_type_id = 14;              //ได้เดินทางหรือทำกิจกรรม ในสถานที่ๆ มีคนอยู่หนา                    แน่น Y / N
                        rpt.exposure_travel_status = "";        //ประวัติเสี่ยง 14 วัน (ตัวเลือกจากตาราง epidem_risk_history_type)
                        rpt.epidem_address = "";            //ที่อยู่ บ้านเลขที่ขณะสำรวจว่าเป็นโรค
                        rpt.epidem_moo = "";
                        rpt.epidem_road = "";
                        rpt.epidem_chw_code = "";
                        rpt.epidem_amp_code = "";
                        rpt.epidem_tmb_code = "";
                        rpt.location_gis_latitude = 0;         //พิกัด Latitude GIS float value WGS84
                        rpt.location_gis_longitude = 0;        //พิกัด Longitude GIS float value WGS84
                        rpt.isolate_chw_code = "";              //รหัสจังหวัดที่ isolate
                        rpt.isolate_place_id = 0;              //สถานที่ isolate                     (ตัวเลือกจากตาราง epidem_covid_isolate_place)
                        rpt.patient_type = "";                  //ประเภทผู้ป่วย (OPD / IPD)
                        rpt.epidem_covid_cluster_type_id = 1;//รหัสของกลุ่ม cluster (ตัวเลือกจากตาราง epidem_cluster)
                        rpt.cluster_latitude = 0;      //พิกัด Latitude GIS float value WGS84 ของ cluster
                        rpt.cluster_longitude = 0;
                        err = "02";
                        lab.epidem_lab_confirm_type_id = 1;        //ผลการตรวจที่ยืนยันการติดเชื้อ                     (1 = RT - PCR, 2 = Antigen,                    3 = Antibody)
                        lab.lab_report_date = row[collabdate].ToString();                   //วันที่รายงานผล LAB                     (ค.ศ.) YYYY - MM - DD
                        lab.lab_report_result = "positive";                 //ผล lab “negative”, “positive”
                        lab.specimen_date = row[collabdate].ToString();                     //วันที่เก็บตัวอย่าง specimen                    (ค.ศ.) YYYY - MM - DD
                        lab.specimen_place_id = 1;                 //รหัสสถานที่เก็บตัวอย่าง (ตัวเลือกจากตาราง epidem_covid_spcm_place)
                        lab.tests_reason_type_id = 1;              //เหตุผลการตรวจ  (ตัวเลือกจากตาราง epidem_covid_reason_type)
                        lab.lab_his_ref_code = row[collabcode].ToString();                  //รหัสอ้างอิงฝั่ง HIS
                        lab.lab_his_ref_name = row[colLabName].ToString();                  //ชื่อรายการ Lab ฝั่ง HIS
                        lab.tmlt_code = "350501";

                        vacc.vaccine_hospital_code = "";            //รหัสหน่วยบริการ ที่รับวัคซีน
                        vacc.vaccine_date = "";                     //วันที่รับวัคซีน (ค.ศ.) YYYY-MM-DD
                        vacc.dose = "";                             //เข็ม ที่
                        vacc.vaccine_manufacturer = "";             //ชื่อผู้ผลิตวัคซีน

                        if (row[colhosname].ToString().Equals("bangna2") || row[colhosname].ToString().Equals("bangna5"))
                        {
                            EpidemAPI epiApi = new EpidemAPI();
                            epiApi.epidem_id = "";
                            epiApi.hospital_code = row[colhosname].ToString().Equals("bangna5") ? "24036" : row[colhosname].ToString().Equals("bangna2") ? "22222" : "";
                            epiApi.hospital_name = row[colhosname].ToString().Equals("bangna5") ? "โรงพยาบาล บางนา5" : row[colhosname].ToString().Equals("bangna2") ? "โรงพยาบาล บางนา2" : "";
                            epiApi.his_identifier = "Main HIS";
                            epiApi.cid = row[colcid].ToString();
                            epiApi.passport_no = "";
                            epiApi.prefix = row[colprefix].ToString().Equals("ด.ช.") ? "1" : row[colprefix].ToString().Equals("ด.ญ.") ? "2" : row[colprefix].ToString().Equals("น.ส.") ? "4" : row[colprefix].ToString().Equals("นาย") ? "3" : row[colprefix].ToString().Equals("นาง") ? "5" : row[colprefix].ToString();
                            epiApi.first_name = row[colfirstname].ToString();
                            epiApi.last_name = row[collastname].ToString();
                            epiApi.nationality = pers.nationality;
                            epiApi.gender = pers.gender.ToString();
                            epiApi.birth_date = pers.birth_date;
                            epiApi.age_y = pers.age_y.ToString();
                            epiApi.age_m = pers.age_m.ToString();
                            epiApi.age_d = pers.age_d.ToString();
                            epiApi.marital_status_id = pers.marital_status_id.ToString();
                            epiApi.address = pers.address;
                            epiApi.moo = pers.moo;
                            epiApi.road = pers.road;
                            epiApi.chw_code = pers.chw_code;
                            epiApi.amp_code = pers.amp_code;
                            epiApi.tmb_code = pers.tmb_code;
                            epiApi.mobile_phone = pers.mobile_phone;
                            epiApi.occupation = pers.occupation;
                            err = "03";
                            epiApi.epidem_report_guid = rpt.epidem_report_guid;

                            epiApi.epidem_report_group_id = rpt.epidem_report_group_id;
                            epiApi.treated_hospital_code = epiApi.hospital_code;
                            epiApi.report_datetime = rpt.report_datetime;
                            epiApi.onset_date = rpt.onset_date;
                            epiApi.treated_date = rpt.treated_date;
                            epiApi.diagnosis_date = rpt.diagnosis_date;
                            epiApi.informer_name = rpt.informer_name;
                            epiApi.principal_diagnosis_icd10 = rpt.principal_diagnosis_icd10;
                            epiApi.diagnosis_icd10_list = rpt.diagnosis_icd10_list;
                            epiApi.epidem_person_status_id = rpt.epidem_person_status_id.ToString();
                            epiApi.epidem_symptom_type_id = rpt.epidem_symptom_type_id.ToString();
                            epiApi.pregnant_status = rpt.pregnant_status;
                            epiApi.respirator_status = rpt.respirator_status;
                            epiApi.epidem_accommodation_type_id = rpt.epidem_accommodation_type_id.ToString();

                            epiApi.vaccinated_status = rpt.vaccinated_status;
                            epiApi.exposure_epidemic_area_status = rpt.exposure_epidemic_area_status;
                            epiApi.exposure_healthcare_worker_status = rpt.exposure_healthcare_worker_status;
                            epiApi.exposure_closed_contact_status = rpt.exposure_closed_contact_status;
                            epiApi.exposure_occupation_status = rpt.exposure_occupation_status;
                            epiApi.exposure_travel_status = rpt.exposure_travel_status;
                            epiApi.rick_history_type_id = rpt.risk_history_type_id.ToString();
                            epiApi.epidem_address = rpt.epidem_address;
                            epiApi.epidem_moo = rpt.epidem_moo;
                            epiApi.epidem_road = rpt.epidem_road;

                            epiApi.epidem_chw_code = rpt.epidem_chw_code;
                            epiApi.epidem_amp_code = rpt.epidem_amp_code;
                            epiApi.epidem_tmb_code = rpt.epidem_tmb_code;
                            epiApi.location_gis_latitude = rpt.location_gis_latitude.ToString();
                            epiApi.location_gis_longitude = rpt.location_gis_longitude.ToString();
                            epiApi.isolate_chw_code = rpt.isolate_chw_code;
                            epiApi.isolate_place_id = rpt.isolate_place_id.ToString();
                            epiApi.patient_type = rpt.patient_type;
                            epiApi.epidem_covid_cluster_type_id = rpt.epidem_covid_cluster_type_id.ToString();
                            epiApi.cluster_latitude = rpt.cluster_latitude.ToString();
                            epiApi.cluster_longitude = rpt.cluster_longitude.ToString();
                            err = "04";
                            epiApi.epidem_lab_confirm_type_id = lab.epidem_lab_confirm_type_id.ToString();
                            epiApi.lab_report_date = lab.lab_report_date;
                            epiApi.lab_report_result = lab.lab_report_result;
                            epiApi.specimen_date = lab.specimen_date;
                            epiApi.specimen_place_id = lab.specimen_place_id.ToString();
                            epiApi.tests_reason_type_id = lab.tests_reason_type_id.ToString();
                            epiApi.lab_his_ref_code = lab.lab_his_ref_code;
                            epiApi.lab_his_ref_name = lab.lab_his_ref_name;
                            epiApi.tmlt_code = lab.tmlt_code;

                            epiApi.vaccine_hospital_code = vacc.vaccine_hospital_code;
                            epiApi.vaccine_date = vacc.vaccine_date;
                            epiApi.dose = vacc.dose;
                            epiApi.vaccine_manufacturer = vacc.vaccine_manufacturer;
                            epiApi.status_send = "0";
                            epiApi.branch_id = row[colhosname].ToString().Equals("bangna5") ? "5" : row[colhosname].ToString().Equals("bangna2") ? "2" : "";
                            epiApi.visit_date = row[colVsDate].ToString();
                            epiApi.pre_no = row[colpreno].ToString();
                            epiApi.an_no = "";
                            epiApi.an_cnt = "";
                            String re = epiApiDB.insertEpidem(epiApi);
                            epiApi = new EpidemAPI();
                            epiApi = epiApiDB.selectByGUID(re);
                            if (epiApi.epidem_id.Length > 0)
                            {
                                bc.bcDB.lcoviddDB.updateStatusEpidemImported(row[colId].ToString());
                            }
                        }
                        //if (row[colhosname].ToString().Equals("bangna5"))
                        //{
                        //    Row rowbn5 = grfBn5.Rows.Add();
                        //    setRowGrfBn(rowbn5, pers, rpt, lab, vacc);
                        //}
                        //if (row[colhosname].ToString().Equals("bangna2"))
                        //{
                        //    Row rowbn2 = grfBn2.Rows.Add();
                        //    rowbn2[colPersCID] = pers.cid;
                        //    setRowGrfBn(rowbn2, pers, rpt, lab, vacc);
                        //}

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("error "+ex.Message, "err "+err);
                }

                
            }
            setGrfData();
            setGrdBn5();
        }
        private EpidemAPI getEpidemApiBn5()
        {
            EpidemAPI epiApi = new EpidemAPI();
            epiApi.epidem_id = txtID.Text.Trim();
            epiApi.hospital_code = txtHosp5Code.Text.Trim();
            epiApi.hospital_name = txtHosp5Name.Text.Trim();
            epiApi.his_identifier = txtHis5Iden.Text.Trim();
            epiApi.cid = txtBn5CID.Text.Trim();
            epiApi.passport_no = txtBn5Passport.Text.Trim();
            epiApi.prefix = cboBn5Prefix.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Prefix.SelectedItem).Value;
            epiApi.first_name = txtbn5Firstname.Text.Trim();
            epiApi.last_name = txtBn5Lastname.Text.Trim();
            epiApi.nationality = cboBn5Nat.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Nat.SelectedItem).Value;
            epiApi.gender = cboBn5Gender.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Gender.SelectedItem).Value;
            epiApi.birth_date = txtBn5Birthdate.Text.Trim();
            epiApi.age_y = txtBn5AgeY.Text.Trim();
            epiApi.age_m = txtBn5AgeM.Text.Trim();
            epiApi.age_d = txtBn5AgeD.Text.Trim();
            epiApi.marital_status_id = cboBn5Nat.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Nat.SelectedItem).Value;
            epiApi.address = txtBn5Addr.Text.Trim();
            epiApi.moo = txtBn5Moo.Text.Trim();
            epiApi.road = txtBn5Road.Text.Trim();
            epiApi.chw_code = cboBn5Chw.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Chw.SelectedItem).Value;
            epiApi.amp_code = cboBn5Amp.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Amp.SelectedItem).Value;
            epiApi.tmb_code = cboBn5Tmb.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Tmb.SelectedItem).Value;
            epiApi.mobile_phone = txtBn5Mobile.Text.Trim();
            epiApi.occupation = cbotxtBn5Occu.SelectedItem == null ? "" : ((ComboBoxItem)cbotxtBn5Occu.SelectedItem).Value;
            epiApi.epidem_report_guid = txtBn5Guid.Text.Trim();
            epiApi.epidem_report_group_id = cboBn5Grp.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Grp.SelectedItem).Value;
            epiApi.treated_hospital_code = txtBn5HospCode.Text.Trim();
            epiApi.report_datetime = txtBn5RptDate.Text.Trim();
            epiApi.onset_date = txtBn5OnsetDate.Text.Trim();
            epiApi.treated_date = txtBn5TreatDate.Text.Trim();
            epiApi.diagnosis_date = txtBn5DiagDate.Text.Trim();
            epiApi.informer_name = txtBn5Informer.Text.Trim();
            epiApi.principal_diagnosis_icd10 = txtBn5ICD10.Text.Trim();
            epiApi.diagnosis_icd10_list = txtBn5ICD10List.Text.Trim();
            epiApi.epidem_person_status_id = cboBn5PersonStatus.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5PersonStatus.SelectedItem).Value;
            epiApi.epidem_symptom_type_id = cboBn5Symptom.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Symptom.SelectedItem).Value;
            epiApi.pregnant_status = cboBn5Pregnant.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Pregnant.SelectedItem).Value;
            epiApi.respirator_status = cboBn5Respi.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Respi.SelectedItem).Value;
            epiApi.epidem_accommodation_type_id = cboBn5Accom.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Accom.SelectedItem).Value;
            epiApi.vaccinated_status = cboBn5Vacc.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Vacc.SelectedItem).Value;
            epiApi.exposure_epidemic_area_status = cboBn5RptArea.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5RptArea.SelectedItem).Value;
            epiApi.exposure_healthcare_worker_status = cboBn5RptWorker.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5RptWorker.SelectedItem).Value;
            epiApi.exposure_closed_contact_status = cboBn5RptClosed.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5RptClosed.SelectedItem).Value;
            epiApi.exposure_occupation_status = cboBn5Occu.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Occu.SelectedItem).Value;
            epiApi.exposure_travel_status = cboBn5RptTravel.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5RptTravel.SelectedItem).Value;
            epiApi.rick_history_type_id = cboBn5RptRickHistoryType.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5RptRickHistoryType.SelectedItem).Value;
            epiApi.epidem_address = txtBn5EpidemAddr.Text.Trim();
            epiApi.epidem_moo = txtBn5EpidemMoo.Text.Trim();
            epiApi.epidem_road = txtBn5EpidemRoad.Text.Trim();
            epiApi.epidem_chw_code = cboBn5EpidemChw.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5EpidemChw.SelectedItem).Value;
            epiApi.epidem_amp_code = cboBn5EpidemAmp.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5EpidemAmp.SelectedItem).Value;
            epiApi.epidem_tmb_code = cboBn5EpidemTmb.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5EpidemTmb.SelectedItem).Value;
            epiApi.location_gis_latitude = txtBn5Lat.Text.Trim();
            epiApi.location_gis_longitude = txtBn5Lon.Text.Trim();
            epiApi.isolate_chw_code = cboBn5IsoChw.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5IsoChw.SelectedItem).Value;
            epiApi.isolate_place_id = cboBn5IsoPlace.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5IsoPlace.SelectedItem).Value;
            epiApi.patient_type = cboBn5PttType.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5PttType.SelectedItem).Value;
            epiApi.epidem_covid_cluster_type_id = cboBn5Cluster.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Cluster.SelectedItem).Value;
            epiApi.cluster_latitude = txtBn5ClusterLat.Text.Trim();
            epiApi.cluster_longitude = txtBn5ClusterLon.Text.Trim();

            epiApi.epidem_lab_confirm_type_id = cboBn5LabConf.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5LabConf.SelectedItem).Value;
            epiApi.lab_report_date = txtBn5LabRptDt.Text.Trim();
            epiApi.lab_report_result = cboBn5LabRes.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5LabRes.SelectedItem).Value;
            epiApi.specimen_date = txtBn5LabSpeciDt.Text.Trim();
            epiApi.specimen_place_id = cboBn5LabPlace.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5LabPlace.SelectedItem).Value;
            epiApi.tests_reason_type_id = cboBn5LabReas.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5LabReas.SelectedItem).Value;
            epiApi.lab_his_ref_code = txtBn5LabCode.Text.Trim();
            epiApi.lab_his_ref_name = txtBn5LabName.Text.Trim();
            epiApi.tmlt_code = cboBn5LabTmlt.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5LabTmlt.SelectedItem).Value;
            epiApi.vaccine_hospital_code = txtBn5VaccHospCode.Text.Trim();
            epiApi.vaccine_date = txtBn5VaccDt.Text.Trim();
            epiApi.dose = txtBn5VaccDose.Text.Trim();
            epiApi.vaccine_manufacturer = cboBn5VaccManu.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5VaccManu.SelectedItem).Value;
            epiApi.status_send = txtBn5StatusSend.Text.Trim();
            epiApi.branch_id = txtBn5BranchId.Text.Trim();
            return epiApi;
        }
        private void setRowGrfBn(Row rowbn, EpidemPerson pers, EpidemReport rpt, EpidemLabReport lab, EpidemVaccination vacc)
        {
            rowbn[colPersCID] = pers.cid;
            rowbn[colPersPassport] = pers.passport_no;
            rowbn[colPersPrefix] = pers.prefix;
            rowbn[colPersFirstname] = pers.first_name;
            rowbn[colPersLastname] = pers.last_name;
            rowbn[colPersNati] = pers.nationality;
            rowbn[colPersGender] = pers.gender;
            rowbn[colPersDOB] = pers.birth_date;
            rowbn[colPersAgeY] = pers.age_y;
            rowbn[colPersAgeM] = pers.age_m;
            rowbn[colPersAgeD] = pers.age_d;
            rowbn[colPersMarital] = pers.marital_status_id;
            rowbn[colPersAddr] = pers.address;
            rowbn[colPersMoo] = pers.moo;
            rowbn[colPersRoad] = pers.road;
            rowbn[colPersChw] = pers.chw_code;
            rowbn[colPersAmp] = pers.amp_code;
            rowbn[colPersTmb] = pers.tmb_code;
            rowbn[colPersMobile] = pers.mobile_phone;
            rowbn[colPersOccu] = pers.occupation;

            rowbn[colRptGUID] = rpt.epidem_report_guid;
            rowbn[colRptGrpId] = rpt.epidem_report_group_id;
            rowbn[colRptHospCode] = rpt.treated_hospital_code;
            rowbn[colRptRptDtm] = rpt.report_datetime;
            rowbn[colRptOnDt] = rpt.onset_date;
            rowbn[colRptTreatDt] = rpt.treated_date;
            rowbn[colRptDiagDt] = rpt.diagnosis_date;
            rowbn[colRptInfor] = rpt.informer_name;
            rowbn[colRptPrin] = rpt.principal_diagnosis_icd10;
            rowbn[colRptDiagICD10] = rpt.diagnosis_icd10_list;
            rowbn[colRptPers] = rpt.epidem_person_status_id;
            rowbn[colRptSymp] = rpt.epidem_symptom_type_id;
            rowbn[colRptPregn] = rpt.pregnant_status;
            rowbn[colRptRespi] = rpt.respirator_status;
            rowbn[colRptAccom] = rpt.epidem_accommodation_type_id;
            rowbn[colRptVacci] = rpt.vaccinated_status;
            rowbn[colRptArea] = rpt.exposure_epidemic_area_status;
            rowbn[colRptWorker] = rpt.exposure_healthcare_worker_status;
            rowbn[colRptClosed] = rpt.exposure_closed_contact_status;
            rowbn[colRptOccup] = rpt.exposure_occupation_status;
            rowbn[colRptTravel] = rpt.risk_history_type_id;
            rowbn[colRptRick] = rpt.exposure_travel_status;
            rowbn[colRptAddr] = rpt.epidem_address;
            rowbn[colRptMoo] = rpt.epidem_moo;
            rowbn[colRptRoad] = rpt.epidem_road;
            rowbn[colRptChw] = rpt.epidem_chw_code;
            rowbn[colRptAmp] = rpt.epidem_amp_code;
            rowbn[colRptTmb] = rpt.epidem_tmb_code;
            rowbn[colRptLat] = rpt.location_gis_latitude;
            rowbn[colRptLon] = rpt.location_gis_longitude;
            rowbn[colRptIsoChw] = rpt.isolate_chw_code;
            rowbn[colRptIsoPlace] = rpt.isolate_place_id;
            rowbn[colRptPttt] = rpt.patient_type;
            rowbn[colRptClus] = rpt.epidem_covid_cluster_type_id;
            rowbn[colRptClusLat] = rpt.cluster_latitude;
            rowbn[colRptClusLon] = rpt.cluster_longitude;

            rowbn[colLabCon] = lab.epidem_lab_confirm_type_id;
            rowbn[colLabRptDt] = lab.lab_report_date;
            rowbn[colLabRes] = lab.lab_report_result;
            rowbn[colLabSpecDt] = lab.specimen_date;
            rowbn[colLabSpecPlac] = lab.specimen_place_id;
            rowbn[colLabReason] = lab.tests_reason_type_id;
            rowbn[colLabRefCode] = lab.lab_his_ref_code;
            rowbn[colLabRefName] = lab.lab_his_ref_name;
            rowbn[colLabTmlt] = lab.tmlt_code;

            rowbn[ColVaccHospCod] = vacc.vaccine_hospital_code;
            rowbn[colVaccDate] = vacc.vaccine_date;
            rowbn[colVaccDose] = vacc.dose;
            rowbn[colVaccManu] = vacc.vaccine_manufacturer;

        }
        private void initGrfBn2()
        {
            grfBn2 = new C1FlexGrid();
            grfBn2.Font = fEdit;
            grfBn2.Dock = System.Windows.Forms.DockStyle.Fill;
            grfBn2.Location = new System.Drawing.Point(0, 0);
            grfBn2.Rows.Count = 1;

            pnBn2View.Controls.Add(grfBn2);

            grfBn2.Rows.Count = 1;
            grfBn2.Cols.Count = 68;

            //grfBn2.Cols[colId].Visible = false;
            //grfBn2.Cols[colpreno].Visible = false;
            //grfBn2.Cols[colreqyear].Visible = false;
            //grfBn2.Cols[colpassport].Visible = false;
            ////grfAdmit.Cols[colhosname].Visible = false;
            //grfBn2.Cols[colcate].Visible = false;
            //grfBn2.Cols[colsatcode].Visible = false;
            grfBn2.Click += GrfBn2_Click;
            theme1.SetTheme(grfBn2, "Office2010Blue");
        }

        private void GrfBn2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void initGrfSend()
        {
            grfSend = new C1FlexGrid();
            grfSend.Font = fEdit;
            grfSend.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSend.Location = new System.Drawing.Point(0, 0);
            grfSend.Rows.Count = 1;

            tabAll.Controls.Add(grfSend);

            grfSend.Rows.Count = 1;
            grfSend.Cols.Count = 69;

            grfSend.Cols[colPersAgeY].Visible = false;
            grfSend.Cols[colPersAgeM].Visible = false;
            grfSend.Cols[colPersAgeD].Visible = false;
            grfSend.Cols[colPersMarital].Visible = false;
            //grfBn5.Cols[colPersMoo].Visible = false;
            grfSend.Cols[colPersNati].Visible = false;
            grfSend.Cols[colPersRoad].Visible = false;
            grfSend.Cols[colPersMoo].Visible = false;
            //grfBn5.Cols[colsatcode].Visible = false;
            ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("&ต้องการลบข้อมูลมั้งหมด ของ รายการนี้", new EventHandler(ContextMenu_delete_ipd_all));
            grfSend.Click += GrfSend_Click;
            theme1.SetTheme(grfSend, "Office2010Blue");
        }

        private void GrfSend_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void initGrfBn5()
        {
            grfBn5 = new C1FlexGrid();
            grfBn5.Font = fEdit;
            grfBn5.Dock = System.Windows.Forms.DockStyle.Fill;
            grfBn5.Location = new System.Drawing.Point(0, 0);
            grfBn5.Rows.Count = 1;

            pnBn5View.Controls.Add(grfBn5);

            grfBn5.Rows.Count = 1;
            grfBn5.Cols.Count = 69;

            grfBn5.Cols[colPersAgeY].Visible = false;
            grfBn5.Cols[colPersAgeM].Visible = false;
            grfBn5.Cols[colPersAgeD].Visible = false;
            grfBn5.Cols[colPersMarital].Visible = false;
            //grfBn5.Cols[colPersMoo].Visible = false;
            grfBn5.Cols[colPersNati].Visible = false;
            grfBn5.Cols[colPersRoad].Visible = false;
            grfBn5.Cols[colPersMoo].Visible = false;
            //grfBn5.Cols[colsatcode].Visible = false;
            grfBn5.Click += GrfBn5_Click;
            theme1.SetTheme(grfBn5, "Office2010Blue");
        }

        private void GrfBn5_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfBn5 == null) return;
            if (grfBn5.Rows.Count <= 1) return;
            //setControlBn5(grfBn5.Row);
            btnBn5SendAPI.Enabled = false;
            setControl(grfBn5.Row);
        }
        private void setControl(int rownum)
        {
            //if (grfBn5.Row == null) return;
            String guid = "", err = "";
            EpidemAPI epiapi = new EpidemAPI();
            guid = grfBn5[rownum, colRptGUID].ToString();
            epiapi = epiApiDB.selectByGUID(guid);
            if (epiapi.epidem_id.Length > 0)
            {
                setControlBn5ByEpi(epiapi);
            }
            else
            {
                setControlBn5New(rownum);
            }
            tcEpidem.SelectedTab = tabBn5Person;
        }
        private void setControlBn5ByEpi(EpidemAPI epiapi)
        {
            String err = "";
            try
            {
                pageLoad = true;
                err = "00";
                txtID.Value = epiapi.epidem_id;
                txtBn5Id.Value = epiapi.epidem_id;
                txtHosp5Code.Value = epiapi.hospital_code;
                txtHosp5Name.Value = epiapi.hospital_name;
                txtHis5Iden.Value = epiapi.his_identifier;
                txtBn5CID.Value = epiapi.cid;
                txtBn5Passport.Value = epiapi.passport_no;

                //epiapi.prefix = cboBn5Prefix.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Prefix.SelectedItem).Value;
                bc.setC1Combo(cboBn5Prefix, epiapi.prefix);

                txtbn5Firstname.Value = epiapi.first_name;
                txtBn5Lastname.Value = epiapi.last_name;
                //epiapi.nationality = cboBn5Nat.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Nat.SelectedItem).Value;
                //epiapi.gender = cboBn5Gender.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Gender.SelectedItem).Value;
                bc.setC1Combo(cboBn5Nat, epiapi.nationality);
                bc.setC1Combo(cboBn5Gender, epiapi.gender);

                txtBn5Birthdate.Value = epiapi.birth_date;
                txtBn5AgeY.Value = epiapi.age_y;
                txtBn5AgeM.Value = epiapi.age_m;
                txtBn5AgeD.Value = epiapi.age_d;

                epiapi.marital_status_id = cboBn5Nat.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Nat.SelectedItem).Value;
                bc.setC1Combo(cboBn5Mari, epiapi.marital_status_id);
                err = "01";
                txtBn5Addr.Value = epiapi.address;
                txtBn5Moo.Value = epiapi.moo;
                txtBn5Road.Value = epiapi.road;

                //epiapi.chw_code = cboBn5Chw.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Chw.SelectedItem).Value;
                String chk = bc.setC1Combo(cboBn5Chw, epiapi.chw_code.Replace("กรุงเทพ ฯ", "กรุงเทพมหานคร"));
                if(chk.Length<=0) bc.setC1ComboByName(cboBn5Chw, epiapi.chw_code.Replace("กรุงเทพ ฯ", "กรุงเทพมหานคร"));
                //epiapi.amp_code = cboBn5Amp.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Amp.SelectedItem).Value;
                //epiapi.tmb_code = cboBn5Tmb.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Tmb.SelectedItem).Value;
                bc.setC1Combo(cboBn5Chw, epiapi.chw_code);
                bc.bcDB.flocaDB.setCboAmphur(cboBn5Amp, epiapi.chw_code);
                bc.setC1Combo(cboBn5Amp, epiapi.amp_code);
                bc.bcDB.flocaDB.setCboTumbon(cboBn5Tmb, epiapi.chw_code, epiapi.amp_code);
                bc.setC1Combo(cboBn5Tmb, epiapi.tmb_code);
                //epiapi.mobile_phone = txtBn5Mobile.Text.Trim();
                txtBn5Mobile.Value = epiapi.mobile_phone;
                //epiapi.occupation = txtBn5Occu.SelectedItem == null ? "" : ((ComboBoxItem)txtBn5Occu.SelectedItem).Value;
                
                //bc.setC1Combo(cbotxtBn5Occu, epiapi.occupation);

                txtBn5Guid.Value = epiapi.epidem_report_guid;

                //epiapi.epidem_report_group_id = cboBn5Grp.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Grp.SelectedItem).Value;
                //bc.setC1Combo(cboBn5Grp, epiapi.epidem_report_group_id);

                txtBn5HospCode.Value = epiapi.treated_hospital_code;
                txtBn5RptDate.Value = epiapi.report_datetime;
                txtBn5OnsetDate.Value = epiapi.onset_date;
                txtBn5TreatDate.Value = epiapi.treated_date;
                txtBn5DiagDate.Value = epiapi.diagnosis_date;
                txtBn5Informer.Value = epiapi.informer_name;
                txtBn5ICD10.Value = epiapi.principal_diagnosis_icd10;
                txtBn5ICD10List.Value = epiapi.diagnosis_icd10_list;
                err = "02";
                //epiapi.epidem_person_status_id = cboBn5Status.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Status.SelectedItem).Value;
                //epiapi.epidem_symptom_type_id = cboBn5Symptom.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Symptom.SelectedItem).Value;
                //epiapi.pregnant_status = cboBn5Pregnant.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Pregnant.SelectedItem).Value;
                //epiapi.respirator_status = cboBn5Respi.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Respi.SelectedItem).Value;
                //epiapi.epidem_accommodation_type_id = cboBn5Accom.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Accom.SelectedItem).Value;
                //epiapi.vaccinated_status = cboBn5Vacc.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Vacc.SelectedItem).Value;
                //epiapi.exposure_epidemic_area_status = cboBn5RptArea.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5RptArea.SelectedItem).Value;
                //epiapi.exposure_healthcare_worker_status = cboBn5RptWorker.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5RptWorker.SelectedItem).Value;
                //epiapi.exposure_closed_contact_status = cboBn5RptClosed.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5RptClosed.SelectedItem).Value;
                //epiapi.exposure_occupation_status = cboBn5Occu.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Occu.SelectedItem).Value;
                //epiapi.exposure_travel_status = cboBn5RptTravel.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5RptTravel.SelectedItem).Value;
                //epiapi.rick_history_type_id = cboBn5RptRick.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5RptRick.SelectedItem).Value;
                bc.setC1Combo(cboBn5PersonStatus, epiapi.epidem_person_status_id);
                bc.setC1Combo(cboBn5Symptom, epiapi.epidem_symptom_type_id);
                bc.setC1Combo(cboBn5Pregnant, epiapi.pregnant_status);
                bc.setC1Combo(cboBn5Respi, epiapi.respirator_status);
                bc.setC1Combo(cboBn5Accom, epiapi.epidem_accommodation_type_id);
                bc.setC1Combo(cboBn5Vacc, epiapi.vaccinated_status);
                bc.setC1Combo(cboBn5RptArea, epiapi.exposure_epidemic_area_status);
                bc.setC1Combo(cboBn5RptWorker, epiapi.exposure_healthcare_worker_status);
                bc.setC1Combo(cboBn5RptClosed, epiapi.exposure_closed_contact_status);
                bc.setC1Combo(cboBn5Occu, epiapi.exposure_occupation_status);
                bc.setC1Combo(cboBn5RptTravel, epiapi.exposure_travel_status);
                bc.setC1Combo(cboBn5RptRickHistoryType, epiapi.rick_history_type_id);

                txtBn5EpidemAddr.Value = epiapi.epidem_address;
                txtBn5EpidemMoo.Value = epiapi.epidem_moo;
                txtBn5EpidemRoad.Value = epiapi.epidem_road;

                //epiapi.epidem_chw_code = cboBn5EpidemChw.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5EpidemChw.SelectedItem).Value;
                //epiapi.epidem_amp_code = cboBn5EpidemAmp.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5EpidemAmp.SelectedItem).Value;
                //epiapi.epidem_tmb_code = cboBn5EpidemTmb.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5EpidemTmb.SelectedItem).Value;
                bc.setC1Combo(cboBn5EpidemChw, epiapi.epidem_chw_code);

                txtBn5Lat.Value = epiapi.location_gis_latitude.Equals("") ? "0" : epiapi.location_gis_latitude;
                txtBn5Lon.Value = epiapi.location_gis_longitude.Equals("") ? "0" : epiapi.location_gis_longitude;
                err = "03";
                //epiapi.isolate_chw_code = cboBn5IsoChw.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5IsoChw.SelectedItem).Value;
                //epiapi.isolate_place_id = cboBn5IsoPlace.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5IsoPlace.SelectedItem).Value;
                //epiapi.patient_type = cboBn5PttType.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5PttType.SelectedItem).Value;
                //epiapi.epidem_covid_cluster_type_id = cboBn5Cluster.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5Cluster.SelectedItem).Value;
                bc.setC1Combo(cboBn5IsoChw, epiapi.isolate_chw_code);
                bc.setC1Combo(cboBn5IsoPlace, epiapi.isolate_place_id);
                //bc.setC1Combo(cboBn5PttType, epiapi.patient_type);
                bc.setC1Combo(cboBn5Cluster, epiapi.epidem_covid_cluster_type_id);

                txtBn5ClusterLat.Value = epiapi.cluster_latitude.Equals("") ? "0" : epiapi.cluster_latitude;
                txtBn5ClusterLon.Value = epiapi.cluster_longitude.Equals("") ? "0" : epiapi.cluster_longitude;

                //epiapi.epidem_lab_confirm_type_id = cboBn5LabConf.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5LabConf.SelectedItem).Value;
                bc.setC1Combo(cboBn5LabConf, epiapi.epidem_lab_confirm_type_id);

                txtBn5LabRptDt.Value = epiapi.lab_report_date;

                //epiapi.lab_report_result = cboBn5LabRes.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5LabRes.SelectedItem).Value;
                bc.setC1Combo(cboBn5LabRes, epiapi.lab_report_result);

                txtBn5LabSpeciDt.Value = epiapi.specimen_date;

                //epiapi.specimen_place_id = cboBn5LabPlace.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5LabPlace.SelectedItem).Value;
                //epiapi.tests_reason_type_id = cboBn5LabReas.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5LabReas.SelectedItem).Value;
                bc.setC1Combo(cboBn5LabPlace, epiapi.specimen_place_id);
                bc.setC1Combo(cboBn5LabReas, epiapi.tests_reason_type_id);

                txtBn5LabCode.Value = epiapi.lab_his_ref_code;
                txtBn5LabName.Value = epiapi.lab_his_ref_name;
                err = "04";
                //epiapi.tmlt_code = cboBn5LabTmlt.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5LabTmlt.SelectedItem).Value;
                bc.setC1Combo(cboBn5LabTmlt, epiapi.tmlt_code);

                txtBn5VaccHospCode.Value = epiapi.vaccine_hospital_code;
                txtBn5VaccDt.Value = epiapi.vaccine_date;
                txtBn5VaccDose.Value = epiapi.dose;

                //epiapi.vaccine_manufacturer = cboBn5VaccManu.SelectedItem == null ? "" : ((ComboBoxItem)cboBn5VaccManu.SelectedItem).Value;
                bc.setC1Combo(cboBn5VaccManu, epiapi.vaccine_manufacturer);

                txtBn5StatusSend.Value = epiapi.status_send;
                txtBn5BranchId.Value = epiapi.branch_id;
                if (epiapi.status_send.Equals("1"))
                {
                    btnBn5SendAPI.Enabled = true;
                }
                else
                {
                    btnBn5SendAPI.Enabled = false;
                }
                //tcEpidem.SelectedTab = tabBn5Person;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ได้กด ปุ่ม authen ยัง err " + err + " ");
            }
            pageLoad = false;
        }
        private void setControlBn5New(int rownum)
        {
            if (rownum < 1) return;
            String err = "";
            try
            {
                Row row = grfBn5.Rows[rownum];
                err = "00";
                txtID.Value = "";
                txtHosp5Code.Value = "24036";
                txtHosp5Name.Value = "โรงพยาบาล บางนา5";
                txtHis5Iden.Value = "HIS MainHIS";
                txtBn5CID.Value = row[colPersCID] != null ? row[colPersCID].ToString():"";
                txtBn5Passport.Value = row[colPersPassport] != null ? row[colPersPassport].ToString():"";
                txtBn5Addr.Value = row[colPersAddr] != null ? row[colPersAddr].ToString():"";
                txtbn5Firstname.Value = row[colPersFirstname] != null ? row[colPersFirstname].ToString():"";
                txtBn5Lastname.Value = row[colPersLastname] != null ? row[colPersLastname].ToString():"";
                txtBn5Birthdate.Value = row[colPersDOB] != null ? row[colPersDOB].ToString():"";
                txtBn5AgeY.Value = row[colPersAgeY] != null ? row[colPersAgeY].ToString():"";
                txtBn5AgeM.Value = row[colPersAgeM] != null ? row[colPersAgeM].ToString():"";
                txtBn5AgeD.Value = row[colPersAgeD] != null ? row[colPersAgeD].ToString():"";
                txtBn5Moo.Value = row[colPersMoo] != null ? row[colPersMoo].ToString():"";
                txtBn5Road.Value = row[colPersRoad] != null ? row[colPersRoad].ToString():"";
                txtBn5Mobile.Value = row[colPersMobile] != null ? row[colPersMobile].ToString():"";

                bc.setC1ComboByName(cboBn5Prefix, row[colPersPrefix].ToString());
                bc.setC1ComboByName(cboBn5Nat, row[colPersNati].ToString());
                bc.setC1ComboByName(cboBn5Gender, row[colPersGender].ToString());
                bc.setC1ComboByName(cboBn5Mari, row[colPersMarital].ToString());
                bc.setC1ComboByName(cboBn5Chw, row[colPersChw].ToString());
                err = "01";
                txtBn5Guid.Value = row[colRptGUID] != null ? row[colRptGUID].ToString() : "";
                txtBn5HospCode.Value = txtHosp5Code.Text.Trim();
                txtBn5RptDate.Value = !row[colRptRptDtm].ToString().Equals("") ? row[colRptRptDtm].ToString() : DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + ".000";
                txtBn5OnsetDate.Value = row[colRptOnDt] != null ? row[colRptOnDt].ToString() : "";
                txtBn5TreatDate.Value = row[colRptTreatDt] != null ? row[colRptTreatDt].ToString() : "";
                txtBn5DiagDate.Value = row[colRptDiagDt] != null ? row[colRptDiagDt].ToString() : "";
                txtBn5Informer.Value = row[colRptInfor] != null ? row[colRptInfor].ToString() : "";
                txtBn5ICD10.Value = row[colRptDiagICD10] != null ? row[colRptDiagICD10].ToString() : "";
                txtBn5ICD10List.Value = "";
                err = "012";
                bc.setC1ComboByName(cboBn5Symptom, row[colRptSymp].ToString());
                bc.setC1ComboByName(cboBn5Pregnant, row[colRptPregn].ToString());
                bc.setC1ComboByName(cboBn5Respi, row[colRptRespi].ToString());
                bc.setC1ComboByName(cboBn5Accom, row[colRptAccom].ToString());
                bc.setC1ComboByName(cboBn5Vacc, row[colRptVacci].ToString());
                bc.setC1ComboByName(cboBn5PersonStatus, row[colRptPers].ToString());
                err = "02";
                bc.setC1ComboByName(cboBn5RptWorker, row[colRptWorker].ToString());
                bc.setC1ComboByName(cboBn5RptArea, row[colRptArea].ToString());

                bc.setC1ComboByName(cboBn5RptClosed, row[colRptClosed].ToString());
                bc.setC1ComboByName(cboBn5Occu, row[colRptOccup].ToString());
                bc.setC1ComboByName(cboBn5RptTravel, row[colRptTravel].ToString());
                bc.setC1ComboByName(cboBn5RptRickHistoryType, row[colRptRick].ToString());
                bc.setC1ComboByName(cboBn5EpidemChw, row[colRptChw].ToString());
                bc.setC1ComboByName(cboBn5Cluster, row[colRptClus].ToString());
                bc.setC1ComboByName(cboBn5PttType, row[colRptPttt].ToString());
                bc.setC1ComboByName(cboBn5IsoPlace, row[colRptIsoPlace].ToString());
                bc.setC1ComboByName(cboBn5IsoChw, row[colRptIsoChw].ToString());

                txtBn5EpidemAddr.Value = row[colRptAddr] != null ? row[colRptAddr].ToString() : "";
                txtBn5EpidemMoo.Value = row[colRptMoo] != null ? row[colRptMoo].ToString() : "";
                txtBn5EpidemRoad.Value = row[colRptRoad] != null ? row[colRptRoad].ToString() : "";
                txtBn5ClusterLat.Value = row[colRptClusLat] != null ? row[colRptClusLat].ToString() : "0";
                txtBn5ClusterLon.Value = row[colRptClusLon] != null ? row[colRptClusLon].ToString() : "0";
                txtBn5Lat.Value = row[colRptLat] != null ? row[colRptLat].ToString() : "0";
                txtBn5Lon.Value = row[colRptLon] != null ? row[colRptLon].ToString() : "0";
                err = "03";
                txtBn5LabRptDt.Value = row[colLabRptDt] != null ? row[colLabRptDt].ToString() : "";
                txtBn5LabSpeciDt.Value = row[colLabSpecDt] != null ? row[colLabSpecDt].ToString() : "";
                txtBn5LabCode.Value = row[colLabRefCode] != null ? row[colLabRefCode].ToString() : "";
                txtBn5LabName.Value = row[colLabRefName] != null ? row[colLabRefName].ToString() : "";
                txtBn5VaccHospCode.Value = row[ColVaccHospCod] != null ? row[ColVaccHospCod].ToString() : "";
                txtBn5VaccDt.Value = row[colVaccDate] != null ? row[colVaccDate].ToString() : "";
                txtBn5VaccDose.Value = row[colVaccDose] != null ? row[colVaccDose].ToString():"";
                err = "04";
                bc.setC1ComboByName(cboBn5LabConf, row[colLabCon].ToString());
                bc.setC1ComboByName(cboBn5LabRes, row[colLabRes].ToString());
                bc.setC1ComboByName(cboBn5LabPlace, row[colLabSpecPlac].ToString());
                bc.setC1ComboByName(cboBn5LabReas, row[colLabReason].ToString());
                bc.setC1ComboByName(cboBn5LabTmlt, row[colLabTmlt].ToString());
                bc.setC1ComboByName(cboBn5VaccManu, row[colVaccManu].ToString());
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "err " + err + " ");
            }            
        }
        private void initGrfData()
        {
            grfData = new C1FlexGrid();
            grfData.Font = fEdit;
            grfData.Dock = System.Windows.Forms.DockStyle.Fill;
            grfData.Location = new System.Drawing.Point(0, 0);
            grfData.Rows.Count = 1;

            tabData.Controls.Add(grfData);

            grfData.Rows.Count = 1;
            grfData.Cols.Count = 36;
            //grfAdmit.Cols[colAdmitAn].Caption = "AN";
            //grfAdmit.Cols[colAdmitDate].Caption = "admit Date";
            //grfAdmit.Cols[colAdmitWard].Caption = "Ward";

            //grfAdmit.Cols[colAdmitAn].Width = 100;
            //grfAdmit.Cols[colAdmitDate].Width = 100;
            //grfAdmit.Cols[colAdmitWard].Width = 100;

            grfData.Cols[colId].Visible = false;
            grfData.Cols[colpreno].Visible = false;
            grfData.Cols[colreqyear].Visible = false;
            grfData.Cols[colpassport].Visible = false;
            //grfAdmit.Cols[colhosname].Visible = false;
            grfData.Cols[colcate].Visible = false;
            grfData.Cols[colsatcode].Visible = false;
            grfData.Click += GrfData_Click;
            theme1.SetTheme(grfData, "Office2010Blue");
            //theme1.SetTheme(grfAdmit, bc.iniC.themeApp);
        }

        private void GrfData_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void setGrdSended()
        {
            grfSend.Rows.Count = 1;
            DataTable dt = new DataTable();
            dt = epiApiDB.selectSendedByBranchId("5");
            int i = 0;
            grfSend.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                grfSend[i, colPersCID] = row1["cid"].ToString();
                grfSend[i, colPersPassport] = row1["passport_no"].ToString();
                grfSend[i, colPersPrefix] = row1["prefix"].ToString();
                grfSend[i, colPersFirstname] = row1["first_name"].ToString();
                grfSend[i, colPersLastname] = row1["last_name"].ToString();
                grfSend[i, colPersGender] = row1["gender"].ToString();
                grfSend[i, colPersDOB] = row1["birth_date"].ToString();
                grfSend[i, colRptGUID] = row1["epidem_report_guid"].ToString();
                grfSend[i, colEpidemId] = row1["epidem_id"].ToString();
                grfSend[i, colPersAddr] = row1["address"].ToString() + " " + row1["moo"].ToString() + " " + row1["road"].ToString();
                //grfBn5[i, colPersMoo] = row1["epidem_id"].ToString();
                //grfBn5[i, colPersRoad] = row1["epidem_id"].ToString();
                grfSend[i, colPersChw] = row1["chw_code"].ToString();
                grfSend[i, colPersAmp] = row1["amp_code"].ToString();
                grfSend[i, colPersTmb] = row1["tmb_code"].ToString();
                //grfBn5[i, colPersMobile] = row1["epidem_id"].ToString();
                //if (row1["status_send"].ToString().Equals("1"))
                //{
                //    grfSend.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                //}
            }
        }
        private void setGrdBn5()
        {
            grfBn5.Rows.Count = 1;
            DataTable dt = new DataTable();
            dt = epiApiDB.selectByBranchId("5");
            int i = 0;
            grfBn5.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                grfBn5[i, colPersCID] = row1["cid"].ToString();
                grfBn5[i, colPersPassport] = row1["passport_no"].ToString();
                grfBn5[i, colPersPrefix] = row1["prefix"].ToString();
                grfBn5[i, colPersFirstname] = row1["first_name"].ToString();
                grfBn5[i, colPersLastname] = row1["last_name"].ToString();
                grfBn5[i, colPersGender] = row1["gender"].ToString();
                grfBn5[i, colPersDOB] = row1["birth_date"].ToString();
                grfBn5[i, colRptGUID] = row1["epidem_report_guid"].ToString();
                grfBn5[i, colEpidemId] = row1["epidem_id"].ToString();
                grfBn5[i, colPersAddr] = row1["address"].ToString()+" "+ row1["moo"].ToString() + " " + row1["road"].ToString();
                //grfBn5[i, colPersMoo] = row1["epidem_id"].ToString();
                //grfBn5[i, colPersRoad] = row1["epidem_id"].ToString();
                grfBn5[i, colPersChw] = row1["chw_code"].ToString();
                grfBn5[i, colPersAmp] = row1["amp_code"].ToString();
                grfBn5[i, colPersTmb] = row1["tmb_code"].ToString();
                //grfBn5[i, colPersMobile] = row1["epidem_id"].ToString();
                if (row1["status_send"].ToString().Equals("1"))         //มีการ save
                {
                    grfBn5.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                }
            }
        }
        private void setGrfData()
        {
            String sql = "";
            DataTable dt = new DataTable();
            dt = bc.bcDB.lcoviddDB.SelectBystatusDEPIDEM();
            grfData.Rows.Count = 1;
            grfData.Rows.Count = dt.Rows.Count + 1;
            //grfBn2.Rows.Count = 1;
            //grfBn5.Rows.Count = 1;
            int i = 0;
            Patient ptt = new Patient();
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                //if (i == 1) continue;
                ptt.dob = row1["dob"].ToString();
                ptt.MNC_BDAY = row1["dob"].ToString();
                ptt.patient_birthday = row1["dob"].ToString();
                grfData[i, colId] = row1["lab_covid_detected_id"].ToString() ;
                grfData[i, colhn] = row1["hos_name"].ToString().Equals("bangna5") ? row1["mnc_hn_no"].ToString() : row1["hn"].ToString();
                grfData[i, colVsDate] = row1["visit_date"].ToString();
                grfData[i, colpreno] = row1["pre_no"].ToString();
                grfData[i, colreqyear] = row1["mnc_req_yr"].ToString();
                grfData[i, colreqdate] = row1["req_date"].ToString();
                grfData[i, collabcode] = row1["lab_code"].ToString();
                grfData[i, colcid] = row1["pid"].ToString();
                grfData[i, colpassport] = row1["passport"].ToString();
                grfData[i, colmobile] = row1["mobile"].ToString();
                grfData[i, colhosname] = row1["hos_name"].ToString();
                grfData[i, colcate] = row1["category"].ToString();
                grfData[i, colsatcode] = row1["sat_code"].ToString();
                grfData[i, colname] = row1["patient_fullname"].ToString();
                grfData[i, collabdate] = row1["lab_date"].ToString();
                grfData[i, collabplace] = row1["lab_place"].ToString();
                grfData[i, collabresult] = row1["lab_result"].ToString();
                grfData[i, colegene] = row1["e_gene"].ToString();
                grfData[i, colrdrp] = row1["rdrp"].ToString();
                grfData[i, colngene] = row1["n_gene"].ToString();
                grfData[i, colnation] = row1["nation_name"].ToString();
                grfData[i, coldob] = row1["dob"].ToString();
                grfData[i, colsex] = row1["sex"].ToString();
                grfData[i, coladdr1] = row1["addr1"].ToString();
                grfData[i, coltambon] = row1["tumbon_name"].ToString();
                grfData[i, colamphur] = row1["amphur_name"].ToString();
                grfData[i, colprov] = row1["prov_name"].ToString();
                grfData[i, colAge] = ptt.AgeStringOK1DOT();
                grfData[i, colNationAPI] = "";
                grfData[i, colStatusSingle] = "5";
                grfData[i, colfirstname] = row1["first_name"].ToString();
                grfData[i, collastname] = row1["last_name"].ToString();
                grfData[i, colprefix] = row1["prefix"].ToString();
                grfData[i, colgender] = row1["sex"].ToString().Equals("M") ? "1" : "2";
                grfData[i, colLabName] = row1["MNC_LB_DSC"].ToString();
                grfData[i, 0] = i;
                if (i % 2 == 0)
                {
                    //grfAdmit.Rows[i].StyleDisplay.BackColor = Color.FromArgb(143, 200, 127);
                }
            }
        }
        private void BtnGetData_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfData();
        }

        private void BtnAuthen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            btnAuthen.Enabled = false;
            getAuten();
            btnAuthen.Enabled = true;
        }
        private async void getAuten()
        {
            pageLoad = true;
            btnAuthen.Enabled = false;
            String API_SECRET = txtAPISecret.Text.Trim();
            String api_key = txtPassword.Text.Trim();
            String user = txtUser.Text.Trim();
            String hospital_code = txtHospCode.Text.Trim();
            String password = "5921CC2BDEDF43080011E8FE75CFEFA72BB8121D5E54058C0E19DE08658FFA0D";
            password = HmacSHA256(API_SECRET, api_key);
            password = password.ToUpper();
            //var url = "https://cvp1.moph.go.th/token?Action=get_moph_access_token&user=" + user + "&password_hash=" + password + "&hospital_code=" + hospital_code;
            var url = "https://cvp1.moph.go.th/token";
            using (HttpClient httpClient = new HttpClient())
            {
                //var result = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Action", "post_moph_access_token"),
                    new KeyValuePair<string, string>("user", user),
                    new KeyValuePair<string, string>("password_hash", password),
                    new KeyValuePair<string, string>("hospital_code", hospital_code)
                });
                HttpResponseMessage response = await httpClient.PostAsync(url, content);
                //HttpResponseMessage response = await httpClient.GetAsync(url);
                authen = response.Content.ReadAsStringAsync().Result;
                
            }
            lSympT.Clear();
            lPersS.Clear();
            lNati.Clear();
            lAccom.Clear();
            lMari.Clear();
            lPersT.Clear();
            lProv.Clear();
            lLabConT.Clear();
            lPref.Clear();
            lTmlt.Clear();
            using (HttpClient httpClient = new HttpClient())
            {
                url = "https://cvp1.moph.go.th/api/LookupTable?table_name=epidem_symptom_type";
                HttpResponseMessage response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        String id = bbb["epidem_symptom_type_id"].ToString();
                        String name = bbb["epidem_symptom_type_name"].ToString();
                        EpidemSymptomType sympt = new EpidemSymptomType();
                        sympt.epidem_symptom_type_id = id;
                        sympt.epidem_symptom_type_name = name;
                        lSympT.Add(sympt);
                    }
                }

                url = "https://cvp1.moph.go.th/api/LookupTable?table_name=nationality";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemNationality nati = new EpidemNationality();
                        nati.nationality_code = bbb["nationality_code"].ToString();
                        nati.nationality_name = bbb["nationality_name"].ToString();
                        lNati.Add(nati);
                    }
                }

                url = "https://cvp1.moph.go.th/api/LookupTable?table_name=epidem_person_status";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemPersonStatus perss = new EpidemPersonStatus();
                        perss.epidem_person_status_id = bbb["epidem_person_status_id"].ToString();
                        perss.epidem_person_status_name = bbb["epidem_person_status_name"].ToString();
                        lPersS.Add(perss);
                    }
                }

                url = "https://cvp1.moph.go.th/api/LookupTable?table_name=epidem_accommodation_type";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemAccommodation accom = new EpidemAccommodation();
                        accom.epidem_accommodation_type_id = bbb["epidem_accommodation_type_id"].ToString();
                        accom.epidem_accommodation_type_name = bbb["epidem_accommodation_type_name"].ToString();
                        lAccom.Add(accom);
                    }
                }

                url = "https://cvp1.moph.go.th/api/LookupTable?table_name=marital_status";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemMarital mari = new EpidemMarital();
                        mari.marital_status_id = bbb["marital_status_id"].ToString();
                        mari.marital_status_name = bbb["marital_status_name"].ToString();
                        lMari.Add(mari);
                    }
                }

                url = "https://cvp1.moph.go.th/api/LookupTable?table_name=province";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemProvince mari = new EpidemProvince();
                        mari.chw_code = bbb["chw_code"].ToString();
                        mari.province_name = bbb["province_name"].ToString();
                        lProv.Add(mari);
                    }
                }

                url = "https://cvp1.moph.go.th/api/LookupTable?table_name=person_type";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemPersonType mari = new EpidemPersonType();
                        mari.person_type_id = bbb["person_type_id"].ToString();
                        mari.person_type_name = bbb["person_type_name"].ToString();
                        lPersT.Add(mari);
                    }
                }

                url = "https://cvp1.moph.go.th/api/LookupTable?table_name=prefix_type";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemPrefixType mari = new EpidemPrefixType();
                        mari.prefix_type_id = bbb["prefix_type_id"].ToString();
                        mari.prefix_type_name = bbb["prefix_type_name"].ToString();
                        lPref.Add(mari);
                    }
                }

                url = "https://cvp1.moph.go.th/api/LookupTable?table_name=tmlt";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemTmlt mari = new EpidemTmlt();
                        mari.tmlt_code = bbb["tmlt_code"].ToString();
                        mari.tmlt_name = bbb["tmlt_name"].ToString();
                        lTmlt.Add(mari);
                    }
                }

                url = "https://cvp1.moph.go.th/api/LookupTable?table_name=vaccine_manufacturer";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemVaccManu mari = new EpidemVaccManu();
                        mari.vaccine_manufacturer_id = bbb["vaccine_manufacturer_id"].ToString();
                        mari.vaccine_manufacturer_name = bbb["vaccine_manufacturer_name"].ToString();
                        lVaccManu.Add(mari);
                    }
                }

                url = "https://cvp1.moph.go.th/api/LookupTable?table_name=gender";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemGender mari = new EpidemGender();
                        mari.gender = bbb["gender"].ToString();
                        mari.name = bbb["name"].ToString();
                        lGender.Add(mari);
                    }
                }

                url = "https://cvp1.moph.go.th/api/LookupTable?table_name=person_risk_type";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemPersonRick mari = new EpidemPersonRick();
                        mari.person_risk_type_id = bbb["person_risk_type_id"].ToString();
                        mari.person_risk_type_name = bbb["person_risk_type_name"].ToString();
                        lPersR.Add(mari);
                    }
                }

                url = "https://cvp1.moph.go.th/api/LookupTable?table_name=epidem_lab_confirm_type";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemLabComfirmType mari = new EpidemLabComfirmType();
                        mari.epidem_lab_confirm_type_id = bbb["epidem_lab_confirm_type_id"].ToString();
                        mari.epidem_lab_confirm_type_name = bbb["epidem_lab_confirm_type_name"].ToString();
                        lLabConT.Add(mari);
                    }
                }
                url = "https://epidemcenter.moph.go.th/epidem/api/LookupTable?table_name=epidem_covid_reason_type";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemCovidReasonType mari = new EpidemCovidReasonType();
                        mari.epidem_covid_reason_type_id = bbb["epidem_covid_reason_type_id"].ToString();
                        mari.epidem_covid_reason_type_name = bbb["epidem_covid_reason_type_name"].ToString();
                        lReasT.Add(mari);
                    }
                }
                url = "https://epidemcenter.moph.go.th/epidem/api/LookupTable?table_name=epidem_covid_spcm_place";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemCovidSpcmPlace mari = new EpidemCovidSpcmPlace();
                        mari.epidem_covid_spcm_place_id = bbb["epidem_covid_spcm_place_id"].ToString();
                        mari.epidem_covid_spcm_place_name = bbb["epidem_covid_spcm_place_name"].ToString();
                        lSpacmP.Add(mari);
                    }
                }
                url = "https://epidemcenter.moph.go.th/epidem/api/LookupTable?table_name=epidem_covid_isolate_place";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemCovidIsolatePlace mari = new EpidemCovidIsolatePlace();
                        mari.epidem_covid_isolate_place_id = bbb["epidem_covid_isolate_place_id"].ToString();
                        mari.epidem_covid_isolate_place_name = bbb["epidem_covid_isolate_place_name"].ToString();
                        lIsoP.Add(mari);
                    }
                }
                url = "https://epidemcenter.moph.go.th/epidem/api/LookupTable?table_name=epidem_cluster";
                response = await httpClient.GetAsync(url);
                symptom = response.Content.ReadAsStringAsync().Result;
                using (JsonTextReader reader = new JsonTextReader(new StringReader(symptom)))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);
                    dynamic objPtt = o2["result"];
                    List<JToken> results = o2["result"].Children().ToList();
                    for (int i = 0; i < results.Count; i++)
                    {
                        JToken bbb = (JToken)results[i];
                        EpidemCluster mari = new EpidemCluster();
                        mari.epidem_cluster_id = bbb["epidem_cluster_id"].ToString();
                        mari.epidem_cluster_name = bbb["epidem_cluster_name"].ToString();
                        lClus.Add(mari);
                    }
                }
            }
            //bc.epiPersS.setCboPersonStatus(cboBn5Status, lPersS, "");
            //.epiPersT.setCboPersonType(cboBn5PttType, lPersT, "");
            bc.epiSympT.setCboSymptomT(cboBn5Symptom, lSympT, "");
            bc.epiMari.setCboMarital(cboBn5Mari, lMari, "");
            //bc.epiProv.setCboProvince(cboBn5Chw, lProv, "");          //  ใช้ข้อมูลของ กรมการปกครอง ให้มาเป็น excel
            //bc.epiProv.setCboProvince(cboBn5EpidemChw, lProv, "");        //  ใช้ข้อมูลของ กรมการปกครอง ให้มาเป็น excel
            bc.epiProv.setCboProvince(cboBn5IsoChw, lProv, "");
            bc.epiNati.setCboNationality(cboBn5Nat, lNati, "");
            bc.epiPref.setCboPrefix(cboBn5Prefix, lPref, "");

            bc.epiGender.setCboGender(cboBn5Gender, lGender, "");
            bc.epiAccomT.setCboAccomType(cboBn5Accom, lAccom, "");
            bc.epiLabConT.setCboLabConfirmType(cboBn5LabConf, lLabConT, "");
            if (cboBn5LabConf.Items.Count > 3)
            {
                cboBn5LabConf.SelectedIndex = 2;
            }

            bc.epiReasT.setCboMarital(cboBn5LabReas, lReasT, "");
            bc.epiIsoP.setCboMarital(cboBn5IsoPlace, lIsoP, "");
            if (cboBn5IsoPlace.Items.Count > 1)
            {
                cboBn5IsoPlace.SelectedIndex = 1;
            }
            bc.epiSpcmP.setCboMarital(cboBn5LabPlace, lSpacmP, "");
            bc.epiClus.setCboMarital(cboBn5Cluster, lClus, "");

            bc.epiVaccManu.setCboVaccManu(cboBn5VaccManu, lVaccManu, "");
            bc.epiTmlt.setCboTmlt(cboBn5LabTmlt, lTmlt, "");
            bc.epiPersR.setCboAccomType(cboBn5RptRickHistoryType, lPersR, "");
            if (cboBn5RptRickHistoryType.Items.Count > 16)
            {
                cboBn5RptRickHistoryType.SelectedIndex = 16;
            }

            bc.setCboPregnant(cboBn5Pregnant, "");
            bc.setCboRespirator(cboBn5Respi, "");
            bc.setCboVacine(cboBn5Vacc, "");
            bc.setCboWorker(cboBn5RptWorker, "");
            bc.setCboArea(cboBn5RptArea, "");
            bc.setCboClosed(cboBn5RptClosed, "");
            bc.setCboTravel(cboBn5RptTravel, "");
            //bc.setCboRick(cboBn5RptRickHistoryType, "");
            bc.setCboOccupation(cboBn5Occu, "");
            bc.setCboPatientType(cboBn5PttType, "");
            bc.setCboLabResult(cboBn5LabRes, "");
            //bc.setCboPersonType(cboBn5LabRes, "");
            bc.setCboPersonStatus(cboBn5PersonStatus, "");
            bc.setCboEpidemGrp(cboBn5Grp, "");
            bc.bcDB.flocaDB.setCboProvince(cboBn5EpidemChw);
            bc.bcDB.flocaDB.setCboProvince(cboBn5Chw);
            cboBn5EpidemChw.SelectedIndex = 2;
            txtBn5LabRptDt.Value = txtBn5TreatDate.Text;
            txtBn5LabSpeciDt.Value = txtBn5TreatDate.Text;
            pageLoad = false;
            btnAuthen.Enabled = true;
        }
        public string HmacSHA256(string key, string data)
        {
            string hash;
            ASCIIEncoding encoder = new ASCIIEncoding();
            Byte[] code = encoder.GetBytes(key);
            using (HMACSHA256 hmac = new HMACSHA256(code))
            {
                Byte[] hmBytes = hmac.ComputeHash(encoder.GetBytes(data));
                hash = ToHexString(hmBytes);
            }
            return hash;
        }
        public static string ToHexString(byte[] array)
        {
            StringBuilder hex = new StringBuilder(array.Length * 2);
            foreach (byte b in array)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
        private void FrmEpidem_Load(object sender, EventArgs e)
        {
            this.Text = "Last Update 2022-09-22";
        }
    }
}
