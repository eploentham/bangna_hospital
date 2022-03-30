using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class PatientHI:Persistent
    {
        public String hi_id { get; set; }
        public String hn { get; set; }
        public String visit_date { get; set; }
        public String pre_no { get; set; }
        public String date_order_drug { get; set; }
        public String status_drug { get; set; }
        public String status_xray { get; set; }
        public String status_lab { get; set; }
        public String status_authen { get; set; }
        public String date_order_xray { get; set; }
        public String date_order_lab { get; set; }
        public String drug_set { get; set; }
        public String status_ordered_drug { get; set; }
        public String status_ordered_xray { get; set; }
        public String status_ordered_lab { get; set; }
        public String status_pic_kyc { get; set; }
        public String status_order { get; set; }
        public String doc_scan_id_kyc { get; set; }
        public String doc_scan_id_authen { get; set; }
        public String queue_seq { get; set; }
        public String req_no_drug { get; set; }
        public String req_no_xray { get; set; }
        public String vn { get; set; }
        public String paid_code { get; set; }
    }
}
