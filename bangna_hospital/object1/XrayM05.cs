using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class XrayM05:Persistent
    {
        public String MNC_XR_GRP_CD { get; set; }
        public String MNC_XR_GRP_DSC { get; set; }
        public String MNC_STAMP_DAT { get; set; }
        public String MNC_STAMP_TIM { get; set; }
        public String MNC_USR_ADD { get; set; }
        public String MNC_USR_UPD { get; set; }
        public String MNC_XR_SEQ { get; set; }
        public String MNC_XR_COLOR { get; set; }
    }
}
