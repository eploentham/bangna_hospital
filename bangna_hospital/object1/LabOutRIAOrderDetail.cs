using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class LabOutRIAOrderDetail
    {
        public string order_number { get; set; }
        public string ln { get; set; }
        public string hn_customer { get; set; }
        public string ref_no { get; set; }
        public string ward_customer { get; set; }
        public string status { get; set; }
        public string doctor { get; set; }
        public string comment_order { get; set; }
        public string comment_patient { get; set; }
        public string time_register { get; set; }
        public int download { get; set; }
    }
}
