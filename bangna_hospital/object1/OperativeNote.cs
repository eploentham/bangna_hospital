using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class OperativeNote:Persistent
    {
        public String operative_note_id { get; set; }
        public String dept_id { get; set; }
        public String dept_name { get; set; }
        public String ward_id { get; set; }
        public String ward_name { get; set; }
        public String attending_stf_id { get; set; }
        public String attending_stf_name { get; set; }
        public String patient_hn { get; set; }
        public String patient_fullname { get; set; }
        public String age { get; set; }
        public String hn { get; set; }
        public String an { get; set; }
        public String mnc_date { get; set; }
        public String pre_no { get; set; }
        public String doc_scan_id { get; set; }
        public String date_operation { get; set; }
        public String time_start { get; set; }
        public String time_finish { get; set; }
        public String total_time { get; set; }
        public String surgeon_id_1 { get; set; }
        public String surgeon_name_1 { get; set; }
        public String surgeon_id_2 { get; set; }
        public String surgeon_name_2 { get; set; }
        public String surgeon_id_3 { get; set; }
        public String surgeon_name_3 { get; set; }
        public String surgeon_id_4 { get; set; }
        public String surgeon_name_4 { get; set; }
        public String assistant_id_1 { get; set; }
        public String assistant_name_1 { get; set; }
        public String assistant_id_2 { get; set; }
        public String assistant_name_2 { get; set; }
        public String assistant_id_3 { get; set; }
        public String assistant_name_3 { get; set; }
        public String assistant_id_4 { get; set; }
        public String assistant_name_4 { get; set; }
        public String scrub_nurse_id_1 { get; set; }
        public String scrub_nurse_name_1 { get; set; }
        public String scrub_nurse_id_2 { get; set; }
        public String scrub_nurse_name_2 { get; set; }
        public String scrub_nurse_id_3 { get; set; }
        public String scrub_nurse_name_3 { get; set; }
        public String scrub_nurse_id_4 { get; set; }
        public String scrub_nurse_name_4 { get; set; }
        public String circulation_nurse_id_1 { get; set; }
        public String circulation_nurse_name_1 { get; set; }
        public String circulation_nurse_id_2 { get; set; }
        public String circulation_nurse_name_2 { get; set; }
        public String circulation_nurse_id_3 { get; set; }
        public String circulation_nurse_name_3 { get; set; }
        public String circulation_nurse_id_4 { get; set; }
        public String circulation_nurse_name_4 { get; set; }
        public String perfusionist_id_1 { get; set; }
        public String perfusionist_name_1 { get; set; }
        public String perfusionist_id_2 { get; set; }
        public String perfusionist_name_2 { get; set; }
        public String perfusionist_id_3 { get; set; }
        public String perfusionist_name_3 { get; set; }
        public String perfusionist_id_4 { get; set; }
        public String perfusionist_name_4 { get; set; }
        public String anesthetist_id_1 { get; set; }
        public String anesthetist_name_1 { get; set; }
        public String anesthetist_id_2 { get; set; }
        public String anesthetist_name_2 { get; set; }
        public String anesthetist_assistant_id_1 { get; set; }
        public String anesthetist_assistant_name_1 { get; set; }
        public String anesthetist_assistant_id_2 { get; set; }
        public String anesthetist_assistant_name_2 { get; set; }
        public String anesthesia_techique_id_1 { get; set; }
        public String anesthesia_techique_name_1 { get; set; }
        public String anesthesia_techique_id_2 { get; set; }
        public String anesthesia_techique_name_2 { get; set; }
        public String time_start_anesthesia_techique_1 { get; set; }
        public String time_finish_anesthesia_techique_1 { get; set; }
        public String time_start_anesthesia_techique_2 { get; set; }
        public String time_finish_anesthesia_techique_2 { get; set; }
        public String total_time_anesthesia_1 { get; set; }
        public String total_time_anesthesia_2 { get; set; }
        public String pre_operatation_diagnosis { get; set; }
        public String post_operation_diagnosis { get; set; }
        public String operation_1 { get; set; }
        public String operation_2 { get; set; }
        public String operation_3 { get; set; }
        public String operation_4 { get; set; }
        public String finding_1 { get; set; }
        public String finding_2 { get; set; }
        public String procidures_1 { get; set; }
        public String procidures_2 { get; set; }
        public String complication { get; set; }
        public String estimated_blood_loss { get; set; }
        public String tissue_biopsy { get; set; }
        public String tissue_biopsy_unit { get; set; }
        public String special_specimen { get; set; }
    }
}
