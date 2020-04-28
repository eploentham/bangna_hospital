using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class LabOutRIAResult
    {
        public LabOutRIAPatient patient { get; set; }
        public LabOutRIAOrderDetail orderdetail { get; set; }
        public List<LabOutRIALabs> labs { get; set; }
        public List<String> report { get; set; }
        public String report_original { get; set; }
    }
}
