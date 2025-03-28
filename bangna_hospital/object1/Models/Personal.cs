using System;

namespace bangna_hospital.Models
{
    public class Personal
    {
        public string CitizenID { get; set; }
        public PersonalInfo ThaiPersonalInfo { get; set; }
        public PersonalInfo EnglishPersonalInfo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public AddressInfo AddressInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Issuer { get; set; }
        public String dobYYYY { get; set; }
        public String dobMM { get; set; }
        public String dobDD { get; set; }
    }
}
