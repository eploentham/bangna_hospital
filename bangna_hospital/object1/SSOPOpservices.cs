using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class SSOPOpservices:Persistent
    {
        public String opservices_id { get; set; }
        public String invno { get; set; }
        public String svid { get; set; }
        public String class1 { get; set; }
        public String hcode { get; set; }
        public String hn { get; set; }
        public String pid { get; set; }
        public String careaccount { get; set; }
        public String typeserv { get; set; }
        public String typein { get; set; }
        public String typeout { get; set; }
        public String dtappoint { get; set; }
        public String svpid { get; set; }
        public String clinic { get; set; }
        public String begdt { get; set; }
        public String enddt { get; set; }
        public String lccode { get; set; }
        public String codeset { get; set; }
        public String stdcode { get; set; }
        public String svcharge { get; set; }
        public String completion { get; set; }
        public String svtxcode { get; set; }
        public String claimcat { get; set; }
        public String billtrans_id { get; set; }
    }
}
