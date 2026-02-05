using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class DoeAlienList11Nov
    {
        public String alien_id { get; set; }
        public String alien_type { get; set; }
        public String alien_prefix_id { get; set; } public String alien_prefix { get; set; } 
        public String alien_firstname_en { get; set; } public String alien_middlename_en { get; set; } public String alien_lastname_en { get; set; } public String birth_date { get; set; } public String gender_id { get; set; } public String gender { get; set; }
        public String empname = "", wkaddress = "", btname = "", nationnality_id="", nationality_name="", job_id="", job_name="";
        public String hn = "", vsdate = "", preno = "", height="", width="", skintone="", hrate="", bp1l="", nationname="", positionname="", countryname="";
        public MemoryStream mspdf = null;
    }
}
