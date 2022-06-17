using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class OutPatient:Persistent
    {
        public String TransactionNumber { get; set; }
        public String MainHospitalCode { get; set; }
        public String ServiceHospitalCode { get; set; }
        public String IDCardNumber { get; set; }
        public String HN { get; set; }
        public String Birthdate { get; set; }
        public String Sex { get; set; }
        public String AdmitDate { get; set; }
        public String TransferToHospitalCode { get; set; }
        public String TotalSocialPaid { get; set; }
        public String TotalPatientPaid { get; set; }
        public String TotalOthersPaid { get; set; }

        public String GrandTotal { get; set; }
        public String DiagnosisCode1 { get; set; }
        public String CauseCode1 { get; set; }
        public String DoctorCode1 { get; set; }
        
        public String DiagnosisCode2 { get; set; }
        public String CauseCode2 { get; set; }
        public String DoctorCode2 { get; set; }
        public String DiagnosisCode3 { get; set; }
        public String CauseCode3 { get; set; }
        public String DoctorCode3 { get; set; }
        public String DiagnosisCode4 { get; set; }
        public String CauseCode4 { get; set; }
        public String DoctorCode4 { get; set; }
        public String DiagnosisCode5 { get; set; }
        public String CauseCode5 { get; set; }
        public String DoctorCode5 { get; set; }
        public String DiagnosisCode6 { get; set; }
        public String CauseCode6 { get; set; }
        public String DoctorCode6 { get; set; }
        public String DiagnosisCode7 { get; set; }
        public String CauseCode7 { get; set; }
        public String DoctorCode7 { get; set; }
        public String DiagnosisCode8 { get; set; }
        public String CauseCode8 { get; set; }
        public String DoctorCode8 { get; set; }
        public String DiagnosisCode9 { get; set; }
        public String CauseCode9 { get; set; }
        public String DoctorCode9 { get; set; }
        public String DiagnosisCode10 { get; set; }
        public String CauseCode10 { get; set; }
        public String DoctorCode10 { get; set; }
        public String ICD9Code1 { get; set; }
        public String OperatingDate1 { get; set; }
        public String OperatingDoctorCode1 { get; set; }
        public String ICD9Code2 { get; set; }
        public String OperatingDate2 { get; set; }
        public String OperatingDoctorCode2 { get; set; }
        public String ICD9Code3 { get; set; }
        public String OperatingDate3 { get; set; }
        public String OperatingDoctorCode3 { get; set; }
        public String ICD9Code4 { get; set; }
        public String OperatingDate4 { get; set; }
        public String OperatingDoctorCode4 { get; set; }
        public String ICD9Code5 { get; set; }
        public String OperatingDate5 { get; set; }
        public String OperatingDoctorCode5 { get; set; }
        public String ICD9Code6 { get; set; }
        public String OperatingDate6 { get; set; }
        public String OperatingDoctorCode6 { get; set; }
        public String ICD9Code7 { get; set; }
        public String OperatingDate7 { get; set; }
        public String OperatingDoctorCode7 { get; set; }
        public String ICD9Code8 { get; set; }
        public String OperatingDate8 { get; set; }
        public String OperatingDoctorCode8 { get; set; }
        public String ICD9Code9 { get; set; }
        public String OperatingDate9 { get; set; }
        public String OperatingDoctorCode9 { get; set; }
        public String ICD9Code10 { get; set; }
        public String OperatingDate10 { get; set; }
        public String OperatingDoctorCode10 { get; set; }

        public String SocialPaid1 { get; set; }
        public String PatientPaid1 { get; set; }
        public String OthersPaid1 { get; set; }
        public String SocialPaid2 { get; set; }
        public String PatientPaid2 { get; set; }
        public String OthersPaid2 { get; set; }
        public String SocialPaid3 { get; set; }
        public String PatientPaid3 { get; set; }
        public String OthersPaid3 { get; set; }

        public String SocialPaid4 { get; set; }
        public String PatientPaid4 { get; set; }
        public String OthersPaid4 { get; set; }
        public String SocialPaid5 { get; set; }
        public String PatientPaid5 { get; set; }
        public String OthersPaid5 { get; set; }
        public String SocialPaid6 { get; set; }
        public String PatientPaid6 { get; set; }
        public String OthersPaid6 { get; set; }
        public String SocialPaid7 { get; set; }
        public String PatientPaid7 { get; set; }
        public String OthersPaid7 { get; set; }
        public String SocialPaid8 { get; set; }
        public String PatientPaid8 { get; set; }
        public String OthersPaid8 { get; set; }
        public String SocialPaid9 { get; set; }
        public String PatientPaid9 { get; set; }
        public String OthersPaid9 { get; set; }
        public String SocialPaid10 { get; set; }
        public String PatientPaid10 { get; set; }
        public String OthersPaid10 { get; set; }

        public String SocialPaid11 { get; set; }
        public String PatientPaid11 { get; set; }
        public String OthersPaid11 { get; set; }
        public String SocialPaid12 { get; set; }
        public String PatientPaid12 { get; set; }
        public String OthersPaid12 { get; set; }
        public String SocialPaid13 { get; set; }
        public String PatientPaid13 { get; set; }
        public String OthersPaid13 { get; set; }
        public String SocialPaid14 { get; set; }
        public String PatientPaid14 { get; set; }
        public String OthersPaid14 { get; set; }
        public String SocialPaid15 { get; set; }
        public String PatientPaid15 { get; set; }
        public String OthersPaid15 { get; set; }
        public String SocialPaid16 { get; set; }
        public String PatientPaid16 { get; set; }
        public String OthersPaid16 { get; set; }
        public String SocialPaid17 { get; set; }
        public String PatientPaid17 { get; set; }
        public String OthersPaid17 { get; set; }
        public String SocialPaid18 { get; set; }
        public String PatientPaid18 { get; set; }
        public String OthersPaid18 { get; set; }

        public String preno { get; set; }
        public String visit_date { get; set; }
        public String visit_time { get; set; }
        public String aa { get; set; }
    }
}
