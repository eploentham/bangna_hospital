using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class BLabOut:Persistent
    {
        public String lab_id { get; set; }
        public String lab_code { get; set; }
        public String lab_name { get; set; }
        public String active { get; set; }
        public String period_result { get; set; }
        public String out_lab_comp_code { get; set; }
        public String out_lab_price { get; set; }
    }
}
