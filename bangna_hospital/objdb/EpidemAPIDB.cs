using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class EpidemAPIDB
    {
        EpidemAPI epid;
        ConnectDB conn;
        public EpidemAPIDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            epid = new EpidemAPI();
            epid.epidem_id = "epidem_id";
            epid.hospital_code = "hospital_code";
            epid.hospital_name = "hospital_name";
            epid.his_identifier = "his_identifier";
            epid.cid = "cid";
            epid.passport_no = "passport_no";
            epid.prefix = "prefix";
            epid.first_name = "first_name";
            epid.last_name = "last_name";
            epid.nationality = "nationality";
            epid.gender = "gender";
            epid.birth_date = "birth_date";
            epid.age_y = "age_y";
            epid.age_m = "age_m";
            epid.age_d = "age_d";
            epid.marital_status_id = "marital_status_id";
            epid.address = "address";
            epid.moo = "moo";
            epid.road = "road";
            epid.chw_code = "chw_code";
            epid.amp_code = "amp_code";
            epid.tmb_code = "tmb_code";
            epid.mobile_phone = "mobile_phone";
            epid.occupation = "occupation";
            epid.epidem_report_guid = "epidem_report_guid";
            epid.epidem_report_group_id = "epidem_report_group_id";
            epid.treated_hospital_code = "treated_hospital_code";
            epid.report_datetime = "report_datetime";
            epid.onset_date = "onset_date";
            epid.treated_date = "treated_date";
            epid.diagnosis_date = "diagnosis_date";
            epid.informer_name = "informer_name";
            epid.principal_diagnosis_icd10 = "principal_diagnosis_icd10";
            epid.diagnosis_icd10_list = "diagnosis_icd10_list";
            epid.epidem_person_status_id = "epidem_person_status_id";
            epid.epidem_symptom_type_id = "epidem_symptom_type_id";
            epid.pregnant_status = "pregnant_status";
            epid.respirator_status = "respirator_status";
            epid.epidem_accommodation_type_id = "epidem_accommodation_type_id";
            epid.vaccinated_status = "vaccinated_status";
            epid.exposure_epidemic_area_status = "exposure_epidemic_area_status";
            epid.exposure_healthcare_worker_status = "exposure_healthcare_worker_status";
            epid.exposure_closed_contact_status = "exposure_closed_contact_status";
            epid.exposure_occupation_status = "exposure_occupation_status";
            epid.exposure_travel_status = "exposure_travel_status";
            epid.rick_history_type_id = "rick_history_type_id";
            epid.epidem_address = "epidem_address";
            epid.epidem_moo = "epidem_moo";
            epid.epidem_road = "epidem_road";
            epid.epidem_chw_code = "epidem_chw_code";
            epid.epidem_amp_code = "epidem_amp_code";
            epid.epidem_tmb_code = "epidem_tmb_code";
            epid.location_gis_latitude = "location_gis_latitude";
            epid.location_gis_longitude = "location_gis_longitude";
            epid.isolate_chw_code = "isolate_chw_code";
            epid.isolate_place_id = "isolate_place_id";
            epid.patient_type = "patient_type";
            epid.epidem_covid_cluster_type_id = "epidem_covid_cluster_type_id";
            epid.cluster_latitude = "cluster_latitude";
            epid.cluster_longitude = "cluster_longitude";
            epid.epidem_lab_confirm_type_id = "epidem_lab_confirm_type_id";
            epid.lab_report_date = "lab_report_date";
            epid.lab_report_result = "lab_report_result";
            epid.specimen_date = "specimen_date";
            epid.specimen_place_id = "specimen_place_id";
            epid.tests_reason_type_id = "tests_reason_type_id";
            epid.lab_his_ref_code = "lab_his_ref_code";
            epid.lab_his_ref_name = "lab_his_ref_name";
            epid.tmlt_code = "tmlt_code";
            epid.vaccine_hospital_code = "vaccine_hospital_code";
            epid.vaccine_date = "vaccine_date";
            epid.dose = "dose";
            epid.vaccine_manufacturer = "vaccine_manufacturer";
            epid.status_send = "status_send";
            epid.branch_id = "branch_id";
            epid.active = "active";
            epid.visit_date = "visit_date";
            epid.pre_no = "pre_no";
            epid.an_no = "an_no";
            epid.an_cnt = "an_cnt";

            epid.table = "t_epidem";
            epid.pkField = "epidem_id";
        }
        public EpidemAPI selectByPk(String epidemid)
        {
            EpidemAPI cop1 = new EpidemAPI();
            DataTable dt = new DataTable();
            String sql = "select stf.mnc_usr_name, stf.mnc_usr_pw, stf.mnc_usr_full,stf.mnc_usr_lev " +
                "From "+ epid.table + " stf " +
                "Where stf." + epid.pkField + " ='" + epidemid + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            cop1 = setEpidemAPI(dt);
            return cop1;
        }
        public EpidemAPI selectByGUID(String guid)
        {
            EpidemAPI cop1 = new EpidemAPI();
            DataTable dt = new DataTable();
            String sql = "select stf.* " +
                "From "+ epid.table + " stf " +
                "Where stf.epidem_report_guid = '" + guid + "' " +
                "Order By stf.epidem_id ";
            dt = conn.selectData(conn.connMainHIS, sql);
            cop1 = setEpidemAPI(dt);
            return cop1;
        }
        public DataTable selectByBranchId(String branchid)
        {
            EpidemAPI cop1 = new EpidemAPI();
            DataTable dt = new DataTable();
            String sql = "select stf.* " +
                "From " + epid.table + " stf " +
                "Where stf.branch_id = '" + branchid + "' and status_send <> '2' and active = '1'  " +
                "Order By stf.epidem_id ";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public DataTable selectSendedByBranchId(String branchid)
        {
            EpidemAPI cop1 = new EpidemAPI();
            DataTable dt = new DataTable();
            String sql = "select stf.* " +
                "From " + epid.table + " stf " +
                "Where stf.branch_id = '" + branchid + "' and status_send = '2'  and active = '1' " +
                "Order By stf.epidem_id ";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        private void chkNull(EpidemAPI p)
        {
            long chk = 0;
            p.hospital_code = p.hospital_code == null ? "" : p.hospital_code;
            p.hospital_name = p.hospital_name == null ? "" : p.hospital_name;
            p.his_identifier = p.his_identifier == null ? "" : p.his_identifier;
            p.cid = p.cid == null ? "" : p.cid;
            p.passport_no = p.passport_no == null ? "" : p.passport_no;
            p.prefix = p.prefix == null ? "" : p.prefix;
            p.first_name = p.first_name == null ? "" : p.first_name;
            p.last_name = p.last_name == null ? "" : p.last_name;
            p.nationality = p.nationality == null ? "" : p.nationality;
            p.gender = p.gender == null ? "" : p.gender;
            p.birth_date = p.birth_date == null ? "" : p.birth_date;
            p.age_y = p.age_y == null ? "" : p.age_y;
            p.age_m = p.age_m == null ? "" : p.age_m;
            p.age_d = p.age_d == null ? "" : p.age_d;
            p.marital_status_id = p.marital_status_id == null ? "" : p.marital_status_id;
            p.address = p.address == null ? "" : p.address;
            p.moo = p.moo == null ? "" : p.moo;
            p.road = p.road == null ? "" : p.road;
            p.chw_code = p.chw_code == null ? "" : p.chw_code;
            p.amp_code = p.amp_code == null ? "" : p.amp_code;
            p.tmb_code = p.tmb_code == null ? "" : p.tmb_code;
            p.mobile_phone = p.mobile_phone == null ? "" : p.mobile_phone;
            p.occupation = p.occupation == null ? "" : p.occupation;
            p.epidem_report_guid = p.epidem_report_guid == null ? "" : p.epidem_report_guid;
            p.epidem_report_group_id = p.epidem_report_group_id == null ? "" : p.epidem_report_group_id;
            p.treated_hospital_code = p.treated_hospital_code == null ? "" : p.treated_hospital_code;
            p.report_datetime = p.report_datetime == null ? "" : p.report_datetime;
            p.onset_date = p.onset_date == null ? "" : p.onset_date;
            p.treated_date = p.treated_date == null ? "" : p.treated_date;
            p.diagnosis_date = p.diagnosis_date == null ? "" : p.diagnosis_date;
            p.informer_name = p.informer_name == null ? "" : p.informer_name;
            p.principal_diagnosis_icd10 = p.principal_diagnosis_icd10 == null ? "" : p.principal_diagnosis_icd10;
            p.diagnosis_icd10_list = p.diagnosis_icd10_list == null ? "" : p.diagnosis_icd10_list;
            p.epidem_person_status_id = p.epidem_person_status_id == null ? "" : p.epidem_person_status_id;
            p.epidem_symptom_type_id = p.epidem_symptom_type_id == null ? "" : p.epidem_symptom_type_id;
            p.pregnant_status = p.pregnant_status == null ? "" : p.pregnant_status;
            p.respirator_status = p.respirator_status == null ? "" : p.respirator_status;
            p.epidem_accommodation_type_id = p.epidem_accommodation_type_id == null ? "" : p.epidem_accommodation_type_id;
            p.vaccinated_status = p.vaccinated_status == null ? "" : p.vaccinated_status;
            p.exposure_epidemic_area_status = p.exposure_epidemic_area_status == null ? "" : p.exposure_epidemic_area_status;
            p.exposure_healthcare_worker_status = p.exposure_healthcare_worker_status == null ? "" : p.exposure_healthcare_worker_status;
            p.exposure_closed_contact_status = p.exposure_closed_contact_status == null ? "" : p.exposure_closed_contact_status;
            p.exposure_occupation_status = p.exposure_occupation_status == null ? "" : p.exposure_occupation_status;
            p.exposure_travel_status = p.exposure_travel_status == null ? "" : p.exposure_travel_status;
            p.rick_history_type_id = p.rick_history_type_id == null ? "" : p.rick_history_type_id;
            p.epidem_address = p.epidem_address == null ? "" : p.epidem_address;
            p.epidem_moo = p.epidem_moo == null ? "" : p.epidem_moo;
            p.epidem_road = p.epidem_road == null ? "" : p.epidem_road;
            p.epidem_chw_code = p.epidem_chw_code == null ? "" : p.epidem_chw_code;
            p.epidem_amp_code = p.epidem_amp_code == null ? "" : p.epidem_amp_code;
            p.epidem_tmb_code = p.epidem_tmb_code == null ? "" : p.epidem_tmb_code;
            p.location_gis_latitude = p.location_gis_latitude == null ? "" : p.location_gis_latitude;
            p.location_gis_longitude = p.location_gis_longitude == null ? "" : p.location_gis_longitude;
            p.isolate_chw_code = p.isolate_chw_code == null ? "" : p.isolate_chw_code;
            p.isolate_place_id = p.isolate_place_id == null ? "" : p.isolate_place_id;
            p.patient_type = p.patient_type == null ? "" : p.patient_type;
            p.epidem_covid_cluster_type_id = p.epidem_covid_cluster_type_id == null ? "" : p.epidem_covid_cluster_type_id;
            p.cluster_latitude = p.cluster_latitude == null ? "" : p.cluster_latitude;
            p.cluster_longitude = p.cluster_longitude == null ? "" : p.cluster_longitude;
            p.epidem_lab_confirm_type_id = p.epidem_lab_confirm_type_id == null ? "" : p.epidem_lab_confirm_type_id;
            p.lab_report_date = p.lab_report_date == null ? "" : p.lab_report_date;
            p.lab_report_result = p.lab_report_result == null ? "" : p.lab_report_result;
            p.specimen_date = p.specimen_date == null ? "" : p.specimen_date;
            p.specimen_place_id = p.specimen_place_id == null ? "" : p.specimen_place_id;
            p.tests_reason_type_id = p.tests_reason_type_id == null ? "" : p.tests_reason_type_id;
            p.lab_his_ref_code = p.lab_his_ref_code == null ? "" : p.lab_his_ref_code;
            p.lab_his_ref_name = p.lab_his_ref_name == null ? "" : p.lab_his_ref_name;
            p.tmlt_code = p.tmlt_code == null ? "" : p.tmlt_code;
            p.vaccine_hospital_code = p.vaccine_hospital_code == null ? "" : p.vaccine_hospital_code;
            p.vaccine_date = p.vaccine_date == null ? "" : p.vaccine_date;
            p.dose = p.dose == null ? "" : p.dose;
            p.vaccine_manufacturer = p.vaccine_manufacturer == null ? "" : p.vaccine_manufacturer;
            p.visit_date = p.visit_date == null ? "" : p.visit_date;
            p.pre_no = p.pre_no == null ? "" : p.pre_no;
            p.an_no = p.an_no == null ? "" : p.an_no;
            p.an_cnt = p.an_cnt == null ? "" : p.an_cnt;
        }
        public String insertEpidem(EpidemAPI p)
        {
            String sql = "", chk = "";
            if (p.epidem_id == "")
            {
                chk = insert(p);
            }
            else
            {
                chk = updatePerson(p);
                chk = updateReport(p);
                chk = updateLabVaccine(p);
            }
            return chk;
        }
        public String insert(EpidemAPI p)
        {
            String sql = "", chk = "";
            try
            {
                if (p.epidem_report_guid == "")
                {
                    p.epidem_report_guid = p.getGUIDID();
                }
                chkNull(p);
                p.active = "1";
                sql = "Insert Into " + epid.table + "(" + epid.hospital_code + "," + epid.active + "," +
                    epid.hospital_name + "," + epid.his_identifier + "," + epid.first_name + "," +
                    epid.passport_no + "," + epid.prefix + "," + epid.cid + "," +
                    epid.last_name + "," + epid.nationality + "," + epid.gender + "," +
                    epid.birth_date + "," + epid.age_y + "," + epid.age_m + "," +

                    epid.age_d + "," + epid.marital_status_id + "," + epid.address + "," +
                    epid.moo + "," + epid.road + "," + epid.chw_code + "," +
                    epid.amp_code + "," + epid.tmb_code + "," + epid.mobile_phone + "," +
                    epid.occupation + "," + epid.epidem_report_guid + "," + epid.epidem_report_group_id + "," +
                    epid.treated_hospital_code + "," + epid.report_datetime + "," + epid.onset_date + "," +

                    epid.treated_date + "," + epid.diagnosis_date + "," + epid.informer_name + "," +
                    epid.principal_diagnosis_icd10 + "," + epid.diagnosis_icd10_list + "," + epid.epidem_person_status_id + "," +
                    epid.epidem_symptom_type_id + "," + epid.pregnant_status + "," + epid.respirator_status + "," +
                    epid.epidem_accommodation_type_id + "," + epid.vaccinated_status + "," + epid.exposure_epidemic_area_status + "," +
                    epid.exposure_healthcare_worker_status + "," + epid.exposure_closed_contact_status + "," + epid.exposure_occupation_status + "," +

                    epid.exposure_travel_status + "," + epid.rick_history_type_id + "," + epid.epidem_address + "," +
                    epid.epidem_moo + "," + epid.epidem_road + "," + epid.epidem_chw_code + "," +
                    epid.epidem_amp_code + "," + epid.epidem_tmb_code + "," + epid.location_gis_latitude + "," +
                    epid.location_gis_longitude + "," + epid.isolate_chw_code + "," + epid.isolate_place_id + "," +
                    epid.patient_type + "," + epid.epidem_covid_cluster_type_id + "," + epid.cluster_latitude + "," +

                    epid.cluster_longitude + "," + epid.epidem_lab_confirm_type_id + "," + epid.lab_report_date + "," +
                    epid.lab_report_result + "," + epid.specimen_date + "," + epid.specimen_place_id + "," +
                    epid.tests_reason_type_id + "," + epid.lab_his_ref_code + "," + epid.lab_his_ref_name + "," +
                    epid.tmlt_code + "," + epid.vaccine_hospital_code + "," + epid.vaccine_date + "," +
                    epid.dose + "," + epid.vaccine_manufacturer + "," + epid.status_send + "," + epid.branch_id + ", " +
                    epid.visit_date + "," + epid.pre_no + "," + epid.an_no + "," + epid.an_cnt + ") " +
                    "Values('" + p.hospital_code + "','" + p.active + "','" +
                    p.hospital_name + "','" + p.his_identifier + "','" + p.first_name + "','" +
                    p.passport_no + "','" + p.prefix + "','" + p.cid + "','" +
                    p.last_name + "','" + p.nationality + "','" + p.gender + "','" +
                    p.birth_date + "','" + p.age_y + "','" + p.age_m + "','" +

                    p.age_d + "','" + p.marital_status_id + "','" + p.address + "','" +
                    p.moo + "','" + p.road + "','" + p.chw_code + "','" +
                    p.amp_code + "','" + p.tmb_code + "','" + p.mobile_phone + "','" +
                    p.occupation + "','" + p.epidem_report_guid + "','" + p.epidem_report_group_id + "','" +
                    p.treated_hospital_code + "','" + p.report_datetime + "','" + p.onset_date + "','" +

                    p.treated_date + "','" + p.diagnosis_date + "','" + p.informer_name + "','" +
                    p.principal_diagnosis_icd10 + "','" + p.diagnosis_icd10_list + "','" + p.epidem_person_status_id + "','" +
                    p.epidem_symptom_type_id + "','" + p.pregnant_status + "','" + p.respirator_status + "','" +
                    p.epidem_accommodation_type_id + "','" + p.vaccinated_status + "','" + p.exposure_epidemic_area_status + "','" +
                    p.exposure_healthcare_worker_status + "','" + p.exposure_closed_contact_status + "','" + p.exposure_occupation_status + "','" +

                    p.exposure_travel_status + "','" + p.rick_history_type_id + "','" + p.epidem_address + "','" +
                    p.epidem_moo + "','" + p.epidem_road + "','" + p.epidem_chw_code + "','" +
                    p.epidem_amp_code + "','" + p.epidem_tmb_code + "','" + p.location_gis_latitude + "','" +
                    p.location_gis_longitude + "','" + p.isolate_chw_code + "','" + p.isolate_place_id + "','" +
                    p.patient_type + "','" + p.epidem_covid_cluster_type_id + "','" + p.cluster_latitude + "','" +

                    p.cluster_longitude + "','" + p.epidem_lab_confirm_type_id + "','" + p.lab_report_date + "','" +
                    p.lab_report_result + "','" + p.specimen_date + "','" + p.specimen_place_id + "','" +
                    p.tests_reason_type_id + "','" + p.lab_his_ref_code + "','" + p.lab_his_ref_name + "','" +
                    p.tmlt_code + "','" + p.vaccine_hospital_code + "','" + p.vaccine_date + "','" +
                    p.dose + "','" + p.vaccine_manufacturer + "','0','"+ p.branch_id + "',' "+
                    p.visit_date + "','" + p.pre_no + "','"+p.an_no+"','" + p.an_cnt + "') ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
                chk = p.epidem_report_guid;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "insert OPDCheckUP");
            }

            return chk;
        }
        public String updatePerson(EpidemAPI p)
        {
            String sql = "", chk = "";
            long chk1 = 0;
            try
            {
                //p.dateModi = " CAST(year(GETDATE()) AS NVARCHAR)+'-'+ RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)+'-'+ RIGHT('00' + CAST(day(GETDATE()) AS NVARCHAR), 2)";
                sql = "Update " + epid.table + " Set " +// epid.AddrE + "='" + p.AddrE + "'," +
                    epid.cid + "='" + p.cid + "'," +
                    epid.passport_no + "='" + p.passport_no + "'," +
                    epid.prefix + "='" + p.prefix + "'," +
                    epid.first_name + "='" + p.first_name + "'," +
                    epid.last_name + "='" + p.last_name + "'," +
                    epid.nationality + "='" + p.nationality + "'," +
                    epid.gender + "='" + p.gender + "'," +
                    epid.birth_date + "='" + p.birth_date + "'," +
                    epid.age_y + "='" + p.age_y + "'," +
                    epid.age_m + "='" + p.age_m + "'," +
                    epid.age_d + "='" + p.age_d + "'," +
                    epid.marital_status_id + "='" + p.marital_status_id + "'," +
                    epid.address + "='" + p.address + "'," +
                    epid.moo + "='" + p.moo + "'," +
                    epid.road + "='" + p.road + "'," +
                    epid.chw_code + "='" + p.chw_code + "'," +
                    epid.amp_code + "='" + p.amp_code + "'," +
                    epid.tmb_code + "='" + p.tmb_code + "'," +
                    epid.mobile_phone + "='" + p.mobile_phone + "'," +
                    epid.occupation + "='" + p.occupation + "', " +
                    epid.status_send + "='1' " +

                "Where " + epid.epidem_report_guid + "='" + p.epidem_report_guid + "'";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                if(long.TryParse(chk,out chk1))
                {
                    chk = p.epidem_report_guid;
                }
                //chk = p.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "update OPDCheckUP");
            }
            return chk;
        }
        public String updateReport(EpidemAPI p)
        {
            String sql = "", chk = "";
            long chk1 = 0;
            try
            {
                //p.dateModi = " CAST(year(GETDATE()) AS NVARCHAR)+'-'+ RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)+'-'+ RIGHT('00' + CAST(day(GETDATE()) AS NVARCHAR), 2)";
                sql = "Update " + epid.table + " Set " +// epid.AddrE + "='" + p.AddrE + "'," +
                    epid.epidem_report_group_id + "='" + p.epidem_report_group_id + "'," +
                    epid.treated_hospital_code + "='" + p.treated_hospital_code + "'," +
                    epid.report_datetime + "='" + p.report_datetime + "'," +
                    epid.onset_date + "='" + p.onset_date + "'," +
                    epid.treated_date + "='" + p.treated_date + "'," +
                    epid.diagnosis_date + "='" + p.diagnosis_date + "'," +
                    epid.informer_name + "='" + p.informer_name + "'," +
                    epid.principal_diagnosis_icd10 + "='" + p.principal_diagnosis_icd10 + "'," +
                    epid.diagnosis_icd10_list + "='" + p.diagnosis_icd10_list + "'," +
                    epid.epidem_person_status_id + "='" + p.epidem_person_status_id + "'," +
                    epid.epidem_symptom_type_id + "='" + p.epidem_symptom_type_id + "'," +
                    epid.pregnant_status + "='" + p.pregnant_status + "'," +
                    epid.respirator_status + "='" + p.respirator_status + "'," +
                    epid.epidem_accommodation_type_id + "='" + p.epidem_accommodation_type_id + "'," +
                    epid.vaccinated_status + "='" + p.vaccinated_status + "'," +
                    epid.exposure_epidemic_area_status + "='" + p.exposure_epidemic_area_status + "'," +
                    epid.exposure_healthcare_worker_status + "='" + p.exposure_healthcare_worker_status + "'," +
                    epid.exposure_closed_contact_status + "='" + p.exposure_closed_contact_status + "'," +
                    epid.exposure_occupation_status + "='" + p.exposure_occupation_status + "'," +
                    epid.exposure_travel_status + "='" + p.exposure_travel_status + "'," +
                    epid.rick_history_type_id + "='" + p.rick_history_type_id + "'," +
                    epid.epidem_address + "='" + p.epidem_address + "'," +
                    epid.epidem_moo + "='" + p.epidem_moo + "'," +
                    epid.epidem_road + "='" + p.epidem_road + "'," +
                    epid.epidem_chw_code + "='" + p.epidem_chw_code + "'," +
                    epid.epidem_amp_code + "='" + p.epidem_amp_code + "'," +
                    epid.epidem_tmb_code + "='" + p.epidem_tmb_code + "'," +
                    epid.location_gis_latitude + "='" + p.location_gis_latitude + "'," +
                    epid.location_gis_longitude + "='" + p.location_gis_longitude + "'," +
                    epid.isolate_chw_code + "='" + p.isolate_chw_code + "'," +
                    epid.isolate_place_id + "='" + p.isolate_place_id + "'," +
                    epid.patient_type + "='" + p.patient_type + "'," +
                    epid.epidem_covid_cluster_type_id + "='" + p.epidem_covid_cluster_type_id + "'," +
                    epid.cluster_latitude + "='" + p.cluster_latitude + "'," +
                    epid.cluster_longitude + "='" + p.cluster_longitude + "' " +

                "Where " + epid.epidem_report_guid + "='" + p.epidem_report_guid + "'";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                if (long.TryParse(chk, out chk1))
                {
                    chk = p.epidem_report_guid;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "update OPDCheckUP");
            }
            return chk;
        }
        public String updateLabVaccine(EpidemAPI p)
        {
            String sql = "", chk = "";
            long chk1 = 0;
            try
            {
                //p.dateModi = " CAST(year(GETDATE()) AS NVARCHAR)+'-'+ RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)+'-'+ RIGHT('00' + CAST(day(GETDATE()) AS NVARCHAR), 2)";
                sql = "Update " + epid.table + " Set " +// epid.AddrE + "='" + p.AddrE + "'," +
                    epid.epidem_lab_confirm_type_id + "='" + p.epidem_lab_confirm_type_id + "'," +
                    epid.lab_report_date + "='" + p.lab_report_date + "'," +
                    epid.lab_report_result + "='" + p.lab_report_result + "'," +
                    epid.specimen_date + "='" + p.specimen_date + "'," +
                    epid.specimen_place_id + "='" + p.specimen_place_id + "'," +
                    epid.tests_reason_type_id + "='" + p.tests_reason_type_id + "'," +
                    epid.lab_his_ref_code + "='" + p.lab_his_ref_code + "'," +
                    epid.lab_his_ref_name + "='" + p.lab_his_ref_name + "'," +
                    epid.tmlt_code + "='" + p.tmlt_code + "'," +
                    epid.vaccine_hospital_code + "='" + p.vaccine_hospital_code + "'," +
                    epid.vaccine_date + "='" + p.vaccine_date + "'," +
                    epid.dose + "='" + p.dose + "'," +
                    epid.vaccine_manufacturer + "='" + p.vaccine_manufacturer + "' " +

                "Where " + epid.epidem_report_guid + "='" + p.epidem_report_guid + "'";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.Id;
                if (long.TryParse(chk, out chk1))
                {
                    chk = p.epidem_report_guid;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "update OPDCheckUP");
            }
            return chk;
        }
        public String updateStatusSendSucess(String epidem_id)
        {
            String sql = "", chk = "";
            try
            {
                //p.dateModi = " CAST(year(GETDATE()) AS NVARCHAR)+'-'+ RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)+'-'+ RIGHT('00' + CAST(day(GETDATE()) AS NVARCHAR), 2)";
                sql = "Update " + epid.table + " Set " +// epid.AddrE + "='" + p.AddrE + "'," +
                    epid.status_send + "='2' " +

                "Where " + epid.epidem_id + "='" + epidem_id + "'";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "update OPDCheckUP");
            }
            return chk;
        }
        public EpidemAPI setEpidemAPI(DataTable dt)
        {
            EpidemAPI dgs1 = new EpidemAPI();
            if (dt.Rows.Count > 0)
            {
                dgs1.epidem_id = dt.Rows[0][epid.epidem_id].ToString();
                dgs1.hospital_code = dt.Rows[0][epid.hospital_code].ToString();
                dgs1.hospital_name = dt.Rows[0][epid.hospital_name].ToString();
                dgs1.his_identifier = dt.Rows[0][epid.his_identifier].ToString();
                dgs1.cid = dt.Rows[0][epid.cid].ToString();
                dgs1.passport_no = dt.Rows[0][epid.passport_no].ToString();
                dgs1.prefix = dt.Rows[0][epid.prefix].ToString();
                dgs1.first_name = dt.Rows[0][epid.first_name].ToString();
                dgs1.last_name = dt.Rows[0][epid.last_name].ToString();
                dgs1.nationality = dt.Rows[0][epid.nationality].ToString();
                dgs1.gender = dt.Rows[0][epid.gender].ToString();
                dgs1.birth_date = dt.Rows[0][epid.birth_date].ToString();
                dgs1.age_y = dt.Rows[0][epid.age_y].ToString();
                dgs1.age_m = dt.Rows[0][epid.age_m].ToString();
                dgs1.age_d = dt.Rows[0][epid.age_d].ToString();
                dgs1.marital_status_id = dt.Rows[0][epid.marital_status_id].ToString();
                dgs1.address = dt.Rows[0][epid.address].ToString();
                dgs1.moo = dt.Rows[0][epid.moo].ToString();
                dgs1.road = dt.Rows[0][epid.road].ToString();
                dgs1.chw_code = dt.Rows[0][epid.chw_code].ToString();
                dgs1.amp_code = dt.Rows[0][epid.amp_code].ToString();
                dgs1.tmb_code = dt.Rows[0][epid.tmb_code].ToString();
                dgs1.mobile_phone = dt.Rows[0][epid.mobile_phone].ToString();
                dgs1.occupation = dt.Rows[0][epid.occupation].ToString();
                dgs1.epidem_report_guid = dt.Rows[0][epid.epidem_report_guid].ToString();
                dgs1.epidem_report_group_id = dt.Rows[0][epid.epidem_report_group_id].ToString();
                dgs1.treated_hospital_code = dt.Rows[0][epid.treated_hospital_code].ToString();
                dgs1.report_datetime = dt.Rows[0][epid.report_datetime].ToString();
                dgs1.onset_date = dt.Rows[0][epid.onset_date].ToString();
                dgs1.treated_date = dt.Rows[0][epid.treated_date].ToString();
                dgs1.diagnosis_date = dt.Rows[0][epid.diagnosis_date].ToString();
                dgs1.informer_name = dt.Rows[0][epid.informer_name].ToString();
                dgs1.principal_diagnosis_icd10 = dt.Rows[0][epid.principal_diagnosis_icd10].ToString();
                dgs1.diagnosis_icd10_list = dt.Rows[0][epid.diagnosis_icd10_list].ToString();
                dgs1.epidem_person_status_id = dt.Rows[0][epid.epidem_person_status_id].ToString();
                dgs1.epidem_symptom_type_id = dt.Rows[0][epid.epidem_symptom_type_id].ToString();
                dgs1.pregnant_status = dt.Rows[0][epid.pregnant_status].ToString();
                dgs1.respirator_status = dt.Rows[0][epid.respirator_status].ToString();
                dgs1.epidem_accommodation_type_id = dt.Rows[0][epid.epidem_accommodation_type_id].ToString();
                dgs1.vaccinated_status = dt.Rows[0][epid.vaccinated_status].ToString();
                dgs1.exposure_epidemic_area_status = dt.Rows[0][epid.exposure_epidemic_area_status].ToString();
                dgs1.exposure_healthcare_worker_status = dt.Rows[0][epid.exposure_healthcare_worker_status].ToString();
                dgs1.exposure_closed_contact_status = dt.Rows[0][epid.exposure_closed_contact_status].ToString();
                dgs1.exposure_occupation_status = dt.Rows[0][epid.exposure_occupation_status].ToString();
                dgs1.exposure_travel_status = dt.Rows[0][epid.exposure_travel_status].ToString();
                dgs1.rick_history_type_id = dt.Rows[0][epid.rick_history_type_id].ToString();
                dgs1.epidem_address = dt.Rows[0][epid.epidem_address].ToString();
                dgs1.epidem_moo = dt.Rows[0][epid.epidem_moo].ToString();
                dgs1.epidem_road = dt.Rows[0][epid.epidem_road].ToString();
                dgs1.epidem_chw_code = dt.Rows[0][epid.epidem_chw_code].ToString();
                dgs1.epidem_amp_code = dt.Rows[0][epid.epidem_amp_code].ToString();
                dgs1.epidem_tmb_code = dt.Rows[0][epid.epidem_tmb_code].ToString();
                dgs1.location_gis_latitude = dt.Rows[0][epid.location_gis_latitude].ToString();
                dgs1.location_gis_longitude = dt.Rows[0][epid.location_gis_longitude].ToString();
                dgs1.isolate_chw_code = dt.Rows[0][epid.isolate_chw_code].ToString();
                dgs1.isolate_place_id = dt.Rows[0][epid.isolate_place_id].ToString();
                dgs1.patient_type = dt.Rows[0][epid.patient_type].ToString();
                dgs1.epidem_covid_cluster_type_id = dt.Rows[0][epid.epidem_covid_cluster_type_id].ToString();
                dgs1.cluster_latitude = dt.Rows[0][epid.cluster_latitude].ToString();
                dgs1.cluster_longitude = dt.Rows[0][epid.cluster_longitude].ToString();
                dgs1.epidem_lab_confirm_type_id = dt.Rows[0][epid.epidem_lab_confirm_type_id].ToString();
                dgs1.lab_report_date = dt.Rows[0][epid.lab_report_date].ToString();
                dgs1.lab_report_result = dt.Rows[0][epid.lab_report_result].ToString();
                dgs1.specimen_date = dt.Rows[0][epid.specimen_date].ToString();
                dgs1.specimen_place_id = dt.Rows[0][epid.specimen_place_id].ToString();
                dgs1.tests_reason_type_id = dt.Rows[0][epid.tests_reason_type_id].ToString();
                dgs1.lab_his_ref_code = dt.Rows[0][epid.lab_his_ref_code].ToString();
                dgs1.lab_his_ref_name = dt.Rows[0][epid.lab_his_ref_name].ToString();
                dgs1.tmlt_code = dt.Rows[0][epid.tmlt_code].ToString();
                dgs1.vaccine_hospital_code = dt.Rows[0][epid.vaccine_hospital_code].ToString();
                dgs1.vaccine_date = dt.Rows[0][epid.vaccine_date].ToString();
                dgs1.dose = dt.Rows[0][epid.dose].ToString();
                dgs1.vaccine_manufacturer = dt.Rows[0][epid.vaccine_manufacturer].ToString();
                dgs1.status_send = dt.Rows[0][epid.status_send].ToString();
                dgs1.branch_id = dt.Rows[0][epid.branch_id].ToString();
                dgs1.visit_date = dt.Rows[0][epid.visit_date].ToString();
                dgs1.pre_no = dt.Rows[0][epid.pre_no].ToString();
                dgs1.an_no = dt.Rows[0][epid.an_no].ToString();
                dgs1.an_cnt = dt.Rows[0][epid.an_cnt].ToString();
            }
            else
            {
                setEpidemAPI(dgs1);
            }
            return dgs1;
        }
        public EpidemAPI setEpidemAPI(EpidemAPI dgs1)
        {
            dgs1.epidem_id = "";
            dgs1.hospital_code = "";
            dgs1.hospital_name = "";
            dgs1.his_identifier = "";
            dgs1.cid = "";
            dgs1.passport_no = "";
            dgs1.prefix = "";
            dgs1.first_name = "";
            dgs1.last_name = "";
            dgs1.nationality = "";
            dgs1.gender = "";
            dgs1.birth_date = "";
            dgs1.age_y = "";
            dgs1.age_m = "";
            dgs1.age_d = "";
            dgs1.marital_status_id = "";
            dgs1.address = "";
            dgs1.moo = "";
            dgs1.road = "";
            dgs1.chw_code = "";
            dgs1.amp_code = "";
            dgs1.tmb_code = "";
            dgs1.mobile_phone = "";
            dgs1.occupation = "";
            dgs1.epidem_report_guid = "";
            dgs1.epidem_report_group_id = "";
            dgs1.treated_hospital_code = "";
            dgs1.report_datetime = "";
            dgs1.onset_date = "";
            dgs1.treated_date = "";
            dgs1.diagnosis_date = "";
            dgs1.informer_name = "";
            dgs1.principal_diagnosis_icd10 = "";
            dgs1.diagnosis_icd10_list = "";
            dgs1.epidem_person_status_id = "";
            dgs1.epidem_symptom_type_id = "";
            dgs1.pregnant_status = "";
            dgs1.respirator_status = "";
            dgs1.epidem_accommodation_type_id = "";
            dgs1.vaccinated_status = "";
            dgs1.exposure_epidemic_area_status = "";
            dgs1.exposure_healthcare_worker_status = "";
            dgs1.exposure_closed_contact_status = "";
            dgs1.exposure_occupation_status = "";
            dgs1.exposure_travel_status = "";
            dgs1.rick_history_type_id = "";
            dgs1.epidem_address = "";
            dgs1.epidem_moo = "";
            dgs1.epidem_road = "";
            dgs1.epidem_chw_code = "";
            dgs1.epidem_amp_code = "";
            dgs1.epidem_tmb_code = "";
            dgs1.location_gis_latitude = "";
            dgs1.location_gis_longitude = "";
            dgs1.isolate_chw_code = "";
            dgs1.isolate_place_id = "";
            dgs1.patient_type = "";
            dgs1.epidem_covid_cluster_type_id = "";
            dgs1.cluster_latitude = "";
            dgs1.cluster_longitude = "";
            dgs1.epidem_lab_confirm_type_id = "";
            dgs1.lab_report_date = "";
            dgs1.lab_report_result = "";
            dgs1.specimen_date = "";
            dgs1.specimen_place_id = "";
            dgs1.tests_reason_type_id = "";
            dgs1.lab_his_ref_code = "";
            dgs1.lab_his_ref_name = "";
            dgs1.tmlt_code = "";
            dgs1.vaccine_hospital_code = "";
            dgs1.vaccine_date = "";
            dgs1.dose = "";
            dgs1.vaccine_manufacturer = "";
            dgs1.status_send = "";
            dgs1.branch_id = "";
            dgs1.visit_date = "";
            dgs1.pre_no = "";
            dgs1.an_no = "";
            dgs1.an_cnt = "";
            return dgs1;
        }
    }
}
