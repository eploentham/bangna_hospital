using System;

namespace bangna_hospital.object1
{
    public class Simb2BillingGroup
    {
        public int ID { get; set; }
        public string CodeBG { get; set; }
        public string BillingGroupTH { get; set; }
        public string BillingGroupEN { get; set; }
        public string CodeBSG { get; set; }
        public string BillingSubGroupTH { get; set; }
        public string BillingSubGroupEN { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Active { get; set; }
        public string status_master { get; set; }

        public Simb2BillingGroup()
        {
            ID = 0;
            CodeBG = string.Empty;
            BillingGroupTH = string.Empty;
            BillingGroupEN = string.Empty;
            CodeBSG = string.Empty;
            BillingSubGroupTH = string.Empty;
            BillingSubGroupEN = string.Empty;
            CreatedDate = null;
            Active = "Y";
            status_master = string.Empty;
        }
    }
}