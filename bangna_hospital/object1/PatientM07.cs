using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class PatientM07:Persistent 
    {
        public String MNC_COU_CD { get; set; }
        public String MNC_CHW_CD { get; set; }
        public String MNC_AMP_CD { get; set; }
        public String MNC_TUM_CD { get; set; }
        public String MNC_TUM_DSC { get; set; }
        public String postcode { get; set; }
        public String chwname { get; set; }
        public String ampname { get; set; }
    }
}
