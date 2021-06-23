using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class PatientSmartcard:Persistent
    {
        public String patient_smartcard_id { get; set; }
        public String prefixname { get; set; }
        public String first_name { get; set; }
        public String middle_name { get; set; }
        public String last_name { get; set; }
        public String first_name_e { get; set; }
        public String middle_name_e { get; set; }
        public String last_name_e { get; set; }
        public String pid { get; set; }
        public String dob { get; set; }
        public String home_no { get; set; }
        public String moo { get; set; }
        public String trok { get; set; }
        public String soi { get; set; }
        public String road { get; set; }
        public String district_name { get; set; }
        public String amphur_name { get; set; }
        public String province_name { get; set; }
        public String date_order { get; set; }
        public String status_send { get; set; }
        public String doc { get; set; }
        public String hn { get; set; }
        public String hn_year { get; set; }
        public String mobile { get; set; }
        public String prefixname_e { get; set; }
        public String attach_note { get; set; }
        public String MNC_OCC_CD { get; set; }
        public String MNC_EDU_CD { get; set; }
        public String MNC_NAT_CD { get; set; }
        public String MNC_REL_CD { get; set; }
        public String MNC_NATI_CD { get; set; }
        public String MNC_CAR_CD { get; set; }
        public String sex { get; set; }
        public String req_no { get; set; }
        public String req_date { get; set; }
        public String pre_no { get; set; }
        public String visit_date { get; set; }
    }
}
