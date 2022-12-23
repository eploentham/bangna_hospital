using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class EpidemAPI:Persistent
    {
        public String epidem_id { get; set; }
        public String hospital_code { get; set; }
        public String hospital_name { get; set; }
        public String his_identifier { get; set; }
        public String cid { get; set; }
        public String passport_no { get; set; }
        public String prefix { get; set; }
        public String first_name { get; set; }
        public String last_name { get; set; }
        public String nationality { get; set; }
        public String gender { get; set; }
        public String birth_date { get; set; }
        public String age_y { get; set; }
        public String age_m { get; set; }
        public String age_d { get; set; }
        public String marital_status_id { get; set; }
        public String address { get; set; }
        public String moo { get; set; }
        public String road { get; set; }
        public String chw_code { get; set; }
        public String amp_code { get; set; }
        public String tmb_code { get; set; }
        public String mobile_phone { get; set; }
        public String occupation { get; set; }
        public String epidem_report_guid { get; set; }
        public String epidem_report_group_id { get; set; }
        public String treated_hospital_code { get; set; }
        public String report_datetime { get; set; }
        public String onset_date { get; set; }
        public String treated_date { get; set; }
        public String diagnosis_date { get; set; }
        public String informer_name { get; set; }
        public String principal_diagnosis_icd10 { get; set; }
        public String diagnosis_icd10_list { get; set; }
        public String epidem_person_status_id { get; set; }
        public String epidem_symptom_type_id { get; set; }
        public String pregnant_status { get; set; }
        public String respirator_status { get; set; }
        public String epidem_accommodation_type_id { get; set; }
        public String vaccinated_status { get; set; }
        public String exposure_epidemic_area_status { get; set; }
        public String exposure_healthcare_worker_status { get; set; }
        public String exposure_closed_contact_status { get; set; }
        public String exposure_occupation_status { get; set; }
        public String exposure_travel_status { get; set; }
        public String rick_history_type_id { get; set; }
        public String epidem_address { get; set; }
        public String epidem_moo { get; set; }
        public String epidem_road { get; set; }
        public String epidem_chw_code { get; set; }
        public String epidem_amp_code { get; set; }
        public String epidem_tmb_code { get; set; }
        public String location_gis_latitude { get; set; }
        public String location_gis_longitude { get; set; }
        public String isolate_chw_code { get; set; }
        public String isolate_place_id { get; set; }
        public String patient_type { get; set; }
        public String epidem_covid_cluster_type_id { get; set; }
        public String cluster_latitude { get; set; }
        public String cluster_longitude { get; set; }
        public String epidem_lab_confirm_type_id { get; set; }
        public String lab_report_date { get; set; }
        public String lab_report_result { get; set; }
        public String specimen_date { get; set; }
        public String specimen_place_id { get; set; }
        public String tests_reason_type_id { get; set; }
        public String lab_his_ref_code { get; set; }
        public String lab_his_ref_name { get; set; }
        public String tmlt_code { get; set; }
        public String vaccine_hospital_code { get; set; }
        public String vaccine_date { get; set; }
        public String dose { get; set; }
        public String vaccine_manufacturer { get; set; }
        public String status_send { get; set; }//0=default;2=send
        public String branch_id { get; set; }
        public String visit_date { get; set; }
        public String pre_no { get; set; }
        public String an_no { get; set; }
        public String an_cnt { get; set; }
    }
}
