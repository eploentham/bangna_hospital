using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class ReportTabContents:Persistent
    {
        public String StudyKey { get; set; }
        public String HistNo { get; set; }
        public String RecNo { get; set; }
        public String Interpretation { get; set; }
        public String Conclusion { get; set; }
        public String HospitalID { get; set; }
        public String AccessNumber { get; set; }
    }
}
