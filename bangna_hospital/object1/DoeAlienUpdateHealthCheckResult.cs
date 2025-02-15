using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class DoeAlienUpdateHealthCheckResult:Persistent
    {
        public String token { get; set; }
        public String cert_id { get; set; }
        public String alcode { get; set; }
        public String alchkhos { get; set; }
        public String alchkstatus { get; set; }
        public String alchkdate { get; set; }
        public String alchkprovid { get; set; }
        public String licenseno { get; set; }
        public String chkname { get; set; }
        public String chkposition { get; set; }
        public String alchkdesc { get; set; }
        public String alchkdoc { get; set; }
    }
}
