using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class DoeAlientResponse11Nov
    {
        public String statuscode = "", statusdesc = "", empname = "", wkaddress = "", reqcode = "", btname = "";
        public String code = "", message = "", emp_name = "", work_address = "", namelist_id = "", business_type_name_id="", business_type_name = "";
        
        public List<DoeAlienList11Nov> alien_list { get; set; }
    }
}
