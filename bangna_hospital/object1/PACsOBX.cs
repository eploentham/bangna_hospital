using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class PACsOBX
    {
        public String SetID { get; set; }
        public String ValueType { get; set; }
        public String ObservationIdentifier { get; set; }
        public String ObservationSubID { get; set; }
        public String ObservationValue { get; set; }
        public String Units { get; set; }
        public String ReferencesRange { get; set; }
        public String AbnormalFlags { get; set; }
        public String Probability { get; set; }
        public String NatureOfAbnormalTest { get; set; }
        public String ObservationResultStatus { get; set; }
        public String DateLastObsNormalValues { get; set; }
        public String UserDefinedAccessChecks { get; set; }
        public String DateTimeOfTheObservation { get; set; }
        public String ProducersID { get; set; }
        public String ResponsibleObserver { get; set; }
        public String ObservationMethod { get; set; }
    }
}
