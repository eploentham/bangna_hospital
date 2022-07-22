using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class EpidemReport:Persistent
    {
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
        public String risk_history_type_id { get; set; }
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
    }
}
