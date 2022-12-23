using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class FLocation:Persistent
    {
        public String location_code { get; set; }
        public String chw_code { get; set; }
        public String amp_code { get; set; }
        public String tmb_code { get; set; }
        public String chw_name_t { get; set; }
        public String amp_name_t { get; set; }
        public String tmb_name_t { get; set; }
    }
}
