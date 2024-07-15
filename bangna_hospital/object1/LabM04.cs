using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class LabM04:Persistent
    {
        public String MNC_LB_CD { get; set; }
        public String MNC_LB_RES_CD { get; set; }
        public String MNC_LB_RES { get; set; }
        public String MNC_LB_RES_MIN { get; set; }
        public String MNC_LB_RES_MAX { get; set; }
        public String MNC_RES_GRP_CD { get; set; }
        public String MNC_RES_UNT { get; set; }
        public String MNC_ORD_NO { get; set; }
        public String MNC_LAB_PRN { get; set; }
    }
}
