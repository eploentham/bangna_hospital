using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class LabOut:Persistent
    {
        public String lab_out_id { get; set; }
        public String lab_code { get; set; }
        public String lab_name { get; set; }
        public String patient_fullname { get; set; }
        public String hn { get; set; }
        public String vn { get; set; }
        public String an { get; set; }
        public String visit_date { get; set; }
        public String pre_no { get; set; }
        public String active { get; set; }
        public String date_create { get; set; }
        public String date_modi { get; set; }
        public String date_cancel { get; set; }
        public String user__create { get; set; }
        public String user_modi { get; set; }
        public String user_cancel { get; set; }
        public String req_no { get; set; }
    }
}
