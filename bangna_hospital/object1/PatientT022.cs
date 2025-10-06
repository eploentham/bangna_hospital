using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    internal class PatientT022:Persistent
    {
        public String MNC_HN_NO { get; set; }
        public String MNC_HN_YR { get; set; }
        public String MNC_PH_GRP_CD { get; set; }
        public String MNC_PH_MEMO { get; set; }
        public String MNC_PH_ALG_CD { get; set; }
        public String MNC_EMP_CD { get; set; }
        public String MNC_STAMP_DAT { get; set; }
        public String MNC_STAMP_TIM { get; set; }
    }
}
