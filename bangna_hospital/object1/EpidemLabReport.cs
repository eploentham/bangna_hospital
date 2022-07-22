using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class EpidemLabReport:Persistent
    {
        public String epidem_lab_confirm_type_id { get; set; }
        public String lab_report_date { get; set; }
        public String lab_report_result { get; set; }
        public String specimen_date { get; set; }
        public String specimen_place_id { get; set; }
        public String tests_reason_type_id { get; set; }
        public String lab_his_ref_code { get; set; }
        public String lab_his_ref_name { get; set; }
        public String tmlt_code { get; set; }
    }
}
