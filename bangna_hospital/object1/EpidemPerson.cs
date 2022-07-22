using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class EpidemPerson:Persistent
    {
        public String cid { get; set; }
        public String passport_no { get; set; }
        public String prefix { get; set; }
        public String first_name { get; set; }
        public String last_name { get; set; }
        public String nationality { get; set; }
        public String gender { get; set; }
        public String birth_date { get; set; }
        public String age_y { get; set; }
        public String age_m { get; set; }
        public String age_d { get; set; }
        public String marital_status_id { get; set; }
        public String address { get; set; }
        public String moo { get; set; }
        public String road { get; set; }
        public String chw_code { get; set; }
        public String amp_code { get; set; }
        public String tmb_code { get; set; }
        public String mobile_phone { get; set; }
        public String occupation { get; set; }
    }
}
