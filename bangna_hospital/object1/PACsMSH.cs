using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class PACsMSH
    {
        public String FieldSeparator { get; set; }
        public String EncodingCharacters { get; set; }
        public String SendingApplication { get; set; }
        public String SendingFacility { get; set; }
        public String ReceivingApplication { get; set; }
        public String ReceivingFacility { get; set; }
        public String DateTimeOfMessage { get; set; }
        public String Security { get; set; }
        public String MessageType { get; set; }
        public String MessageControlID { get; set; }
        public String ProcessingID { get; set; }
        public String VersionID { get; set; }
        public String SequenceNumber { get; set; }
        public String ContinuationPointer { get; set; }
        public String AcceptAcknowledgementType { get; set; }
        public String ApplicationAcknowledgementType { get; set; }
        public String CountryCode { get; set; }
        public String CharacterSet { get; set; }
        public String PrincipalLanguageOfMessage { get; set; }
        public String AlternateCharacterSetHandlingScheme { get; set; }
    }
}
