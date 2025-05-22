using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class SSOPBillTran:Persistent
    {
        public String billtran_id { get; set; }
        public String station { get; set; }
        public String authcode { get; set; }
        public String dtran { get; set; }
        public String hcode { get; set; }
        public String invno { get; set; }
        public String billno { get; set; }
        public String hn { get; set; }
        public String memberno { get; set; }
        public String amount { get; set; }
        public String paid { get; set; }
        public String vercode { get; set; }
        public String tflag { get; set; }
        public String pid { get; set; }
        public String name { get; set; }
        public String hmain { get; set; }
        public String payplan { get; set; }
        public String claimamt { get; set; }
        public String otherpayplan { get; set; }
        public String otherpay { get; set; }
        public String preno { get; set; }
        public String status_process { get; set; }
    }
}
