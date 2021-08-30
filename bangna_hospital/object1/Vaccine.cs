using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class Vaccine:Persistent
    {
        public String reserve_vaccine_id { get; set; }
        public String pid { get; set; }
        public String firstname { get; set; }
        public String lastname { get; set; }
        public String address { get; set; }
        public String email { get; set; }
        public String mobile { get; set; }
        public String vaccine { get; set; }
        public String dose { get; set; }
        public String payment { get; set; }
        public String active { get; set; }
        public String date_create { get; set; }
        public String drug_allergy { get; set; }
        public String status_payment { get; set; }
        public String disease { get; set; }
    }
}
