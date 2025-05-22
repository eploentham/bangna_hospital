using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class SSOPDispensing:Persistent
    {
        public String billdisp_id { get; set; }
        public String providerid { get; set; }
        public String dispid { get; set; }
        public String invno { get; set; }
        public String hn { get; set; }
        public String pid { get; set; }
        public String prescdt { get; set; }
        public String dispdt { get; set; }
        public String prescb { get; set; }
        public String itemcnt { get; set; }
        public String chargeamt { get; set; }
        public String claimamt { get; set; }
        public String paid { get; set; }
        public String otherpay { get; set; }
        public String reimburser { get; set; }
        public String benefitplan { get; set; }
        public String dispestat { get; set; }
        public String svid { get; set; }
        public String daycover { get; set; }
        public String billtrans_id { get; set; }

    }
}
