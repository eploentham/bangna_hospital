using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class DoeAlienList
    {
        public String alcode { get; set; }
        public String altype { get; set; }
        public String alprefix { get; set; } public String alprefixen { get; set; } 
        public String alnameen { get; set; } public String alsnameen { get; set; } public String albdate { get; set; } public String algender { get; set; } public String alnation { get; set; } public String alposid { get; set; }
        public String empname = "", wkaddress = "", btname = "";
        public String hn = "", vsdate = "", preno = "", height="", width="", skintone="", hrate="", bp1l="", nationname="", positionname="", countryname="";
        public MemoryStream mspdf = null;
    }
}
