using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class SSOPDispenseditem:Persistent
    {
        public String dispenseditem_id { get; set; }
        public String dispid { get; set; }
        public String prdcat { get; set; }
        public String hospdrgid { get; set; }
        public String drgid { get; set; }
        public String dfscode { get; set; }
        public String dfstext { get; set; }
        public String packsize { get; set; }
        public String sigcode { get; set; }
        public String sigtext { get; set; }
        public String quantity { get; set; }
        public String unitprice { get; set; }
        public String chargeamt { get; set; }
        public String reimbprice { get; set; }
        public String reimbamt { get; set; }
        public String prdsecode { get; set; }
        public String claimcont { get; set; }
        public String claimcat { get; set; }
        public String multidisp { get; set; }
        public String supplyfor { get; set; }
        public String billtrans_id { get; set; }
    }
}
