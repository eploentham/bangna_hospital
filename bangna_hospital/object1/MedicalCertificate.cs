using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class MedicalCertificate:Persistent
    {
        public String certi_id { get; set; }
        public String certi_code { get; set; }
        public String dtr_code { get; set; }
        public String dtr_name_t { get; set; }
        public String dtr_name_e { get; set; }
        public String ptt_name_t { get; set; }
        public String ptt_name_e { get; set; }
        public String visit_date { get; set; }
        public String visit_time { get; set; }
        public String line1 { get; set; }
        public String line2 { get; set; }
        public String line3 { get; set; }
        public String line4 { get; set; }
        public String hn { get; set; }
        public String pre_no { get; set; }
        public String status_ipd { get; set; }
        public String an { get; set; }
        public String doc_scan_id { get; set; }
        public String status_2nd_leaf { get; set; }
        public String counter_name { get; set; }
    }
}
