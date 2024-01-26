using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class OutPatientDB
    {
        public ConnectDB conn;
        public OutPatient outp;
        public OutPatientDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        public OutPatientDB()
        {
            initConfig();
        }
        private void initConfig()
        {
            outp = new OutPatient();
            outp.TransactionNumber = "TransactionNumber";
            outp.MainHospitalCode = "MainHospitalCode";
            outp.ServiceHospitalCode = "ServiceHospitalCode";
            outp.IDCardNumber = "IDCardNumber";
            outp.HN = "HN";
            outp.Birthdate = "Birthdate";
            outp.Sex = "Sex";
            outp.AdmitDate = "AdmitDate";
            outp.TransferToHospitalCode = "TransferToHospitalCode";
            outp.TotalSocialPaid = "TotalSocialPaid";
            outp.TotalPatientPaid = "TotalPatientPaid";
            outp.TotalOthersPaid = "TotalOthersPaid";
            outp.GrandTotal = "GrandTotal";
            outp.DiagnosisCode1 = "DiagnosisCode1";
            outp.CauseCode1 = "CauseCode1";
            outp.DoctorCode1 = "DoctorCode1";

            outp.DiagnosisCode2 = "DiagnosisCode2";
            outp.CauseCode2 = "CauseCode2";
            outp.DoctorCode2 = "DoctorCode2";
            outp.DiagnosisCode3 = "DiagnosisCode3";
            outp.CauseCode3 = "CauseCode3";
            outp.DoctorCode3 = "DoctorCode3";
            outp.DiagnosisCode4 = "DiagnosisCode4";
            outp.CauseCode4 = "CauseCode4";
            outp.DoctorCode4 = "DoctorCode4";
            outp.DiagnosisCode5 ="";
            outp.CauseCode5 ="";
            outp.DoctorCode5 ="";
            outp.DiagnosisCode6 ="";
            outp.CauseCode6 ="";
            outp.DoctorCode6 ="";
            outp.DiagnosisCode7 ="";
            outp.CauseCode7 ="";
            outp.DoctorCode7 ="";
            outp.DiagnosisCode8 ="";
            outp.CauseCode8 ="";
            outp.DoctorCode8 ="";
            outp.DiagnosisCode9 ="";
            outp.CauseCode9 ="";
            outp.DoctorCode9 ="";
            outp.DiagnosisCode10 ="";
            outp.CauseCode10 ="";
            outp.DoctorCode10 ="";
            outp.ICD9Code1 = "ICD9Code1";
            outp.OperatingDate1 = "OperatingDate1";
            outp.OperatingDoctorCode1 = "OperatingDoctorCode1";
            outp.ICD9Code2 = "ICD9Code2";
            outp.OperatingDate2 = "OperatingDate2";
            outp.OperatingDoctorCode2 = "OperatingDoctorCode2";
            outp.ICD9Code3 = "ICD9Code3";
            outp.OperatingDate3 = "OperatingDate3";
            outp.OperatingDoctorCode3 = "OperatingDoctorCode3";
            outp.ICD9Code4 = "ICD9Code4";
            outp.OperatingDate4 = "OperatingDate4";
            outp.OperatingDoctorCode4 = "OperatingDoctorCode4";
            outp.ICD9Code5 ="";
            outp.OperatingDate5 ="";
            outp.OperatingDoctorCode5 ="";
            outp.ICD9Code6 ="";
            outp.OperatingDate6 ="";
            outp.OperatingDoctorCode6 ="";
            outp.ICD9Code7 ="";
            outp.OperatingDate7 ="";
            outp.OperatingDoctorCode7 ="";
            outp.ICD9Code8 ="";
            outp.OperatingDate8 ="";
            outp.OperatingDoctorCode8 ="";
            outp.ICD9Code9 ="";
            outp.OperatingDate9 ="";
            outp.OperatingDoctorCode9 ="";
            outp.ICD9Code10 ="";
            outp.OperatingDate10 ="";
            outp.OperatingDoctorCode10 ="";

            outp.SocialPaid1 = "SocialPaid1";
            outp.PatientPaid1 = "PatientPaid1";
            outp.OthersPaid1 = "OthersPaid1";

            outp.SocialPaid2 = "SocialPaid2";
            outp.PatientPaid2 = "PatientPaid2";
            outp.OthersPaid2 = "OthersPaid2";

            outp.SocialPaid3 = "SocialPaid3";
            outp.PatientPaid3 = "PatientPaid3";
            outp.OthersPaid3 = "OthersPaid3";

            outp.SocialPaid4 = "SocialPaid4";
            outp.PatientPaid4 = "PatientPaid4";
            outp.OthersPaid4 = "OthersPaid4";

            outp.SocialPaid5 = "SocialPaid5";
            outp.PatientPaid5 = "PatientPaid5";
            outp.OthersPaid5 = "OthersPaid5";

            outp.SocialPaid6 = "SocialPaid6";
            outp.PatientPaid6 = "PatientPaid6";
            outp.OthersPaid6 = "OthersPaid6";

            outp.SocialPaid7 = "SocialPaid7";
            outp.PatientPaid7 = "PatientPaid7";
            outp.OthersPaid7 = "OthersPaid7";

            outp.SocialPaid8 = "SocialPaid8";
            outp.PatientPaid8 = "PatientPaid8";
            outp.OthersPaid8 = "OthersPaid8";

            outp.SocialPaid9 = "SocialPaid9";
            outp.PatientPaid9 = "PatientPaid9";
            outp.OthersPaid9 = "OthersPaid9";

            outp.SocialPaid10 = "SocialPaid10";
            outp.PatientPaid10 = "PatientPaid10";
            outp.OthersPaid10 = "OthersPaid10";

            outp.SocialPaid11 = "SocialPaid11";
            outp.PatientPaid11 = "PatientPaid11";
            outp.OthersPaid11 = "OthersPaid11";

            outp.SocialPaid12 = "SocialPaid12";
            outp.PatientPaid12 = "PatientPaid12";
            outp.OthersPaid12 = "OthersPaid12";

            outp.SocialPaid13 = "SocialPaid13";
            outp.PatientPaid13 = "PatientPaid13";
            outp.OthersPaid13 = "OthersPaid13";

            outp.SocialPaid14 = "SocialPaid14";
            outp.PatientPaid14 = "PatientPaid14";
            outp.OthersPaid14 = "OthersPaid14";

            outp.SocialPaid15 = "SocialPaid15";
            outp.PatientPaid15 = "PatientPaid15";
            outp.OthersPaid15 = "OthersPaid15";

            outp.SocialPaid16 = "SocialPaid16";
            outp.PatientPaid16 = "PatientPaid16";
            outp.OthersPaid16 = "OthersPaid16";

            outp.SocialPaid17 = "SocialPaid17";
            outp.PatientPaid17 = "PatientPaid17";
            outp.OthersPaid17 = "OthersPaid17";

            outp.SocialPaid18 = "SocialPaid18";
            outp.PatientPaid18 = "PatientPaid18";
            outp.OthersPaid18 = "OthersPaid18";

            

            outp.preno = "pre_no";
            outp.visit_date = "visit_date";
            outp.visit_time = "visit_time";
            outp.aa = "aa";

            outp.table = "Outpatient";
            outp.pkField = "";
        }
        private void chkNull(OutPatient p)
        {
            long chk = 0;
            int chk1 = 0;
            decimal chk2 = 0;

            p.MainHospitalCode = p.MainHospitalCode == null ? "" : p.MainHospitalCode;
            p.ServiceHospitalCode = p.ServiceHospitalCode == null ? "" : p.ServiceHospitalCode;
            p.IDCardNumber = p.IDCardNumber == null ? "" : p.IDCardNumber;
            p.HN = p.HN == null ? "" : p.HN;
            p.Birthdate = p.Birthdate == null ? "" : p.Birthdate;
            p.AdmitDate = p.AdmitDate == null ? "" : p.AdmitDate;
            p.DiagnosisCode1 = p.DiagnosisCode1 == null ? "" : p.DiagnosisCode1;
            p.CauseCode1 = p.CauseCode1 == null ? "" : p.CauseCode1;
            p.DoctorCode1 = p.DoctorCode1 == null ? "" : p.DoctorCode1;
            p.DiagnosisCode2 = p.DiagnosisCode2 == null ? "" : p.DiagnosisCode2;
            p.CauseCode2 = p.CauseCode2 == null ? "" : p.CauseCode2;
            p.DoctorCode2 = p.DoctorCode2 == null ? "" : p.DoctorCode2;
            p.DiagnosisCode3 = p.DiagnosisCode3 == null ? "" : p.DiagnosisCode3;
            p.CauseCode3 = p.CauseCode3 == null ? "" : p.CauseCode3;
            p.DoctorCode3 = p.DoctorCode3 == null ? "" : p.DoctorCode3;
            p.DiagnosisCode4 = p.DiagnosisCode4 == null ? "" : p.DiagnosisCode4;
            p.CauseCode4 = p.CauseCode4 == null ? "" : p.CauseCode4;
            p.DoctorCode4 = p.DoctorCode4 == null ? "" : p.DoctorCode4;
            p.DiagnosisCode5 = p.DiagnosisCode5 == null ? "" : p.DiagnosisCode5;
            p.CauseCode5 = p.CauseCode5 == null ? "" : p.CauseCode5;
            p.DoctorCode5 = p.DoctorCode5 == null ? "" : p.DoctorCode5;
            p.DiagnosisCode6 = p.DiagnosisCode6 == null ? "" : p.DiagnosisCode6;
            p.CauseCode6 = p.CauseCode6 == null ? "" : p.CauseCode6;
            p.DoctorCode6 = p.DoctorCode6 == null ? "" : p.DoctorCode6;
            p.DiagnosisCode7 = p.DiagnosisCode7 == null ? "" : p.DiagnosisCode7;
            p.CauseCode7 = p.CauseCode7 == null ? "" : p.CauseCode7;
            p.DoctorCode7 = p.DoctorCode7 == null ? "" : p.DoctorCode7;
            p.DiagnosisCode8 = p.DiagnosisCode8 == null ? "" : p.DiagnosisCode8;
            p.CauseCode8 = p.CauseCode8 == null ? "" : p.CauseCode8;
            p.DoctorCode8 = p.DoctorCode8 == null ? "" : p.DoctorCode8;
            p.DiagnosisCode9 = p.DiagnosisCode9 == null ? "" : p.DiagnosisCode9;
            p.CauseCode9 = p.CauseCode9 == null ? "" : p.CauseCode9;
            p.DoctorCode9 = p.DoctorCode9 == null ? "" : p.DoctorCode9;
            p.DiagnosisCode10 = p.DiagnosisCode10 == null ? "" : p.DiagnosisCode10;
            p.CauseCode10 = p.CauseCode10 == null ? "" : p.CauseCode10;
            p.DoctorCode10 = p.DoctorCode10 == null ? "" : p.DoctorCode10;
            p.ICD9Code1 = p.ICD9Code1 == null ? "" : p.ICD9Code1;
            p.OperatingDate1 = p.OperatingDate1 == null ? "" : p.OperatingDate1;
            p.OperatingDoctorCode1 = p.OperatingDoctorCode1 == null ? "" : p.OperatingDoctorCode1;
            p.ICD9Code2 = p.ICD9Code2 == null ? "" : p.ICD9Code2;
            p.OperatingDate2 = p.OperatingDate2 == null ? "" : p.OperatingDate2;
            p.OperatingDoctorCode2 = p.OperatingDoctorCode2 == null ? "" : p.OperatingDoctorCode2;
            p.ICD9Code3 = p.ICD9Code3 == null ? "" : p.ICD9Code3;
            p.OperatingDate3 = p.OperatingDate3 == null ? "" : p.OperatingDate3;
            p.OperatingDoctorCode3 = p.OperatingDoctorCode3 == null ? "" : p.OperatingDoctorCode3;
            p.ICD9Code4 = p.ICD9Code4 == null ? "" : p.ICD9Code4;
            p.OperatingDate4 = p.OperatingDate4 == null ? "" : p.OperatingDate4;
            p.OperatingDoctorCode4 = p.OperatingDoctorCode4 == null ? "" : p.OperatingDoctorCode4;
            p.ICD9Code5 = p.ICD9Code5 == null ? "" : p.ICD9Code5;
            p.OperatingDate5 = p.OperatingDate5 == null ? "" : p.OperatingDate5;
            p.OperatingDoctorCode5 = p.OperatingDoctorCode5 == null ? "" : p.OperatingDoctorCode5;
            p.TransferToHospitalCode = p.TransferToHospitalCode == null ? "" : p.TransferToHospitalCode;
            p.aa = p.aa == null ? "" : p.aa;

            p.preno = p.preno == null ? "" : p.preno;
            p.visit_date = p.visit_date == null ? "" : p.visit_date;
            p.visit_time = p.visit_time == null ? "" : p.visit_time;

            p.Sex = long.TryParse(p.Sex, out chk) ? chk.ToString() : "1";
            //p.MNC_REQ_NO = long.TryParse(p.MNC_REQ_NO, out chk) ? chk.ToString() : "0";
            

            p.TotalSocialPaid = decimal.TryParse(p.TotalSocialPaid, out chk2) ? chk2.ToString() : "0";
            p.TotalPatientPaid = decimal.TryParse(p.TotalPatientPaid, out chk2) ? chk2.ToString() : "0";
            p.TotalOthersPaid = decimal.TryParse(p.TotalOthersPaid, out chk2) ? chk2.ToString() : "0";
            p.GrandTotal = decimal.TryParse(p.GrandTotal, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid1 = decimal.TryParse(p.SocialPaid1, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid1 = decimal.TryParse(p.PatientPaid1, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid1 = decimal.TryParse(p.OthersPaid1, out chk2) ? chk2.ToString() : "0";
            p.SocialPaid2 = decimal.TryParse(p.SocialPaid2, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid2 = decimal.TryParse(p.PatientPaid2, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid2 = decimal.TryParse(p.OthersPaid2, out chk2) ? chk2.ToString() : "0";
            p.SocialPaid3 = decimal.TryParse(p.SocialPaid3, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid3 = decimal.TryParse(p.PatientPaid3, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid3 = decimal.TryParse(p.OthersPaid3, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid4 = decimal.TryParse(p.SocialPaid4, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid4 = decimal.TryParse(p.PatientPaid4, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid4 = decimal.TryParse(p.OthersPaid4, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid5 = decimal.TryParse(p.SocialPaid5, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid5 = decimal.TryParse(p.PatientPaid5, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid5 = decimal.TryParse(p.OthersPaid5, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid6 = decimal.TryParse(p.SocialPaid6, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid6 = decimal.TryParse(p.PatientPaid6, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid6 = decimal.TryParse(p.OthersPaid6, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid7 = decimal.TryParse(p.SocialPaid7, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid7 = decimal.TryParse(p.PatientPaid7, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid7 = decimal.TryParse(p.OthersPaid7, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid8 = decimal.TryParse(p.SocialPaid8, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid8 = decimal.TryParse(p.PatientPaid8, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid8 = decimal.TryParse(p.OthersPaid8, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid9 = decimal.TryParse(p.SocialPaid9, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid9 = decimal.TryParse(p.PatientPaid9, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid9 = decimal.TryParse(p.OthersPaid9, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid10 = decimal.TryParse(p.SocialPaid10, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid10 = decimal.TryParse(p.PatientPaid10, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid10 = decimal.TryParse(p.OthersPaid10, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid11 = decimal.TryParse(p.SocialPaid11, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid11 = decimal.TryParse(p.PatientPaid11, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid11 = decimal.TryParse(p.OthersPaid11, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid12 = decimal.TryParse(p.SocialPaid12, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid12 = decimal.TryParse(p.PatientPaid12, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid12 = decimal.TryParse(p.OthersPaid12, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid13 = decimal.TryParse(p.SocialPaid13, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid13 = decimal.TryParse(p.PatientPaid13, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid13 = decimal.TryParse(p.OthersPaid13, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid14 = decimal.TryParse(p.SocialPaid14, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid14 = decimal.TryParse(p.PatientPaid14, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid14 = decimal.TryParse(p.OthersPaid14, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid15 = decimal.TryParse(p.SocialPaid15, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid15 = decimal.TryParse(p.PatientPaid15, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid15 = decimal.TryParse(p.OthersPaid15, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid16 = decimal.TryParse(p.SocialPaid16, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid16 = decimal.TryParse(p.PatientPaid16, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid16 = decimal.TryParse(p.OthersPaid16, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid17 = decimal.TryParse(p.SocialPaid17, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid17 = decimal.TryParse(p.PatientPaid17, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid17 = decimal.TryParse(p.OthersPaid17, out chk2) ? chk2.ToString() : "0";

            p.SocialPaid18 = decimal.TryParse(p.SocialPaid18, out chk2) ? chk2.ToString() : "0";
            p.PatientPaid18 = decimal.TryParse(p.PatientPaid18, out chk2) ? chk2.ToString() : "0";
            p.OthersPaid18 = decimal.TryParse(p.OthersPaid18, out chk2) ? chk2.ToString() : "0";
        }
        public String insertNoClose(OleDbCommand cmd, OutPatient p, String userId)
        {
            String re = "";
            String sql = "";
            p.active = "1";
            //p.ssdata_id = "";
            int chk = 0;
            chkNull(p);
            sql = "Insert Into " + outp.table + " (" + outp.MainHospitalCode + "," + outp.ServiceHospitalCode + "," + outp.IDCardNumber + "," +
                "" + outp.HN + "," + outp.Birthdate + "," + outp.Sex + "," +
                "" + outp.AdmitDate + "," + outp.TransferToHospitalCode + "," + outp.TotalSocialPaid + "," +
                "" + outp.TotalPatientPaid + "," + outp.TotalOthersPaid + "," + outp.GrandTotal + "," +
                "" + outp.SocialPaid1 + "," + outp.PatientPaid1 + "," + outp.OthersPaid1 + "," +
                "" + outp.SocialPaid2 + "," + outp.PatientPaid2 + "," + outp.OthersPaid2 + "," +

                "" + outp.SocialPaid3 + "," + outp.PatientPaid3 + "," + outp.OthersPaid3 + "," +
                "" + outp.SocialPaid4 + "," + outp.PatientPaid4 + "," + outp.OthersPaid4 + "," +
                "" + outp.SocialPaid5 + "," + outp.PatientPaid5 + "," + outp.OthersPaid5 + "," +
                "" + outp.SocialPaid6 + "," + outp.PatientPaid6 + "," + outp.OthersPaid6 + "," +

                "" + outp.SocialPaid7 + "," + outp.PatientPaid7 + "," + outp.OthersPaid7 + "," +
                "" + outp.SocialPaid8 + "," + outp.PatientPaid8 + "," + outp.OthersPaid8 + "," +
                "" + outp.SocialPaid9 + "," + outp.PatientPaid9 + "," + outp.OthersPaid9 + "," +
                "" + outp.SocialPaid10 + "," + outp.PatientPaid10 + "," + outp.OthersPaid10 + "," +

                "" + outp.SocialPaid11 + "," + outp.PatientPaid11 + "," + outp.OthersPaid11 + "," +
                "" + outp.SocialPaid12 + "," + outp.PatientPaid12 + "," + outp.OthersPaid12 + "," +
                "" + outp.SocialPaid13 + "," + outp.PatientPaid13 + "," + outp.OthersPaid13 + "," +
                "" + outp.SocialPaid14 + "," + outp.PatientPaid14 + "," + outp.OthersPaid14 + "," +

                "" + outp.SocialPaid15 + "," + outp.PatientPaid15 + "," + outp.OthersPaid15 + "," +
                "" + outp.SocialPaid16 + "," + outp.PatientPaid16 + "," + outp.OthersPaid16 + "," +
                "" + outp.SocialPaid17 + "," + outp.PatientPaid17 + "," + outp.OthersPaid17 + "," +
                "" + outp.SocialPaid18 + "," + outp.PatientPaid18 + "," + outp.OthersPaid18 + "" + 
                "," + outp.aa + "" +
               ") " +
                "Values ('" + p.MainHospitalCode + "','" + p.ServiceHospitalCode + "','" + p.IDCardNumber + "'," +
                "'" + p.HN + "','" + p.Birthdate + "','" + p.Sex + "'," +
                "'" + p.AdmitDate + "',NULL,'" + p.TotalSocialPaid + "'," +
                "'" + p.TotalPatientPaid + "','" + p.TotalOthersPaid + "','" + p.GrandTotal + "'," +
                "'" + p.SocialPaid1 + "','" + p.PatientPaid1 + "','" + p.OthersPaid1 + "'," +
                "'" + p.SocialPaid2 + "','" + p.PatientPaid2 + "','" + p.OthersPaid2 + "'," +


                "'" + p.SocialPaid3 + "','" + p.PatientPaid3 + "','" + p.OthersPaid3 + "'," +
                "'" + p.SocialPaid4 + "','" + p.PatientPaid4 + "','" + p.OthersPaid4 + "'," +
                "'" + p.SocialPaid5 + "','" + p.PatientPaid5 + "','" + p.OthersPaid5 + "'," +
                "'" + p.SocialPaid6 + "','" + p.PatientPaid6 + "','" + p.OthersPaid6 + "'," +

                "'" + p.SocialPaid7 + "','" + p.PatientPaid7 + "','" + p.OthersPaid7 + "'," +
                "'" + p.SocialPaid8 + "','" + p.PatientPaid8 + "','" + p.OthersPaid8 + "'," +
                "'" + p.SocialPaid9 + "','" + p.PatientPaid9 + "','" + p.OthersPaid9 + "'," +
                "'" + p.SocialPaid10 + "','" + p.PatientPaid10 + "','" + p.OthersPaid10 + "'," +

                "'" + p.SocialPaid11 + "','" + p.PatientPaid11 + "','" + p.OthersPaid11 + "'," +
                "'" + p.SocialPaid12 + "','" + p.PatientPaid12 + "','" + p.OthersPaid12 + "'," +
                "'" + p.SocialPaid13 + "','" + p.PatientPaid13 + "','" + p.OthersPaid13 + "'," +
                "'" + p.SocialPaid14 + "','" + p.PatientPaid14 + "','" + p.OthersPaid14 + "'," +

                "'" + p.SocialPaid15 + "','" + p.PatientPaid15 + "','" + p.OthersPaid15 + "'," +
                "'" + p.SocialPaid16 + "','" + p.PatientPaid16 + "','" + p.OthersPaid16 + "'," +
                "'" + p.SocialPaid17 + "','" + p.PatientPaid17 + "','" + p.OthersPaid17 + "'," +
                "'" + p.SocialPaid18 + "','" + p.PatientPaid18 + "','" + p.OthersPaid18 + "'," + 
                "'" + p.aa + "'" +
                ")";
            try
            {
                cmd.CommandText = sql;
                chk = cmd.ExecuteNonQuery();
                re = chk.ToString();
            }
            catch (Exception ex)
            {
                //sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "OutPatientDB insert error  " + ex.Message + " " + ex.InnerException + " " + sql);
                re = ex.Message;
            }

            return re;
        }
        public String insertNoClose1(SqlCommand cmd, OutPatient p, String userId)
        {
            String re = "";
            String sql = "";
            p.active = "1";
            //p.ssdata_id = "";
            int chk = 0;
            chkNull(p);
            sql = "Insert Into " + outp.table + "1 (" + outp.MainHospitalCode + "," + outp.ServiceHospitalCode + "," + outp.IDCardNumber + "," +
                "" + outp.HN + "," + outp.Birthdate + "," + outp.Sex + "," +
                "" + outp.AdmitDate + ","  + outp.TotalSocialPaid + "," +
                "" + outp.TotalPatientPaid + "," + outp.TotalOthersPaid + "," + outp.GrandTotal + "," +
                "" + outp.DiagnosisCode1 + ","  + outp.DoctorCode1 + "," +
                "" + outp.preno + "," + outp.visit_date + "," + outp.visit_time + " " +
               ") " +
                "Values ('" + p.MainHospitalCode + "','" + p.ServiceHospitalCode + "','" + p.IDCardNumber + "'," +
                "'" + p.HN + "','" + p.Birthdate + "','" + p.Sex + "'," +
                "'" + p.AdmitDate + "','" + p.TotalSocialPaid + "'," +
                "'" + p.TotalPatientPaid + "','" + p.TotalOthersPaid + "','" + p.GrandTotal + "'," +
                "'" + p.DiagnosisCode1 + "','" + p.DoctorCode1 + "', " +
                //"NULL,NULL,NULL " +
                "'" + p.preno + "','" + p.visit_date + "','" + p.visit_time + "' " +
                ")";
            try
            {
                cmd.CommandText = sql;
                chk = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "OutPatientDB insert error  " + ex.Message + " " + ex.InnerException + " " + sql);
            }

            return chk.ToString();
        }
        public String insert(OutPatient p, String userId)
        {
            String re = "";
            String sql = "";
            p.active = "1";
            //p.ssdata_id = "";
            int chk = 0;
            chkNull(p);
            OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data source= C:\Users\pir fahim shah\Documents\TravelAgency.accdb";
            conn.Open();
            
            sql = "Insert Into " + outp.table + " (" + outp.MainHospitalCode + "," + outp.ServiceHospitalCode + "," + outp.IDCardNumber + "," +
                "" + outp.HN + "," + outp.Birthdate + "," + outp.Sex + "," +
                "" + outp.AdmitDate + "," + outp.TransferToHospitalCode + "," + outp.TotalSocialPaid + "," +
                "" + outp.TotalPatientPaid + "," + outp.TotalOthersPaid + "," + outp.GrandTotal + "," +
                "" + outp.DiagnosisCode1 + "," + outp.CauseCode1 + "," + outp.DoctorCode1 + "," +
                "" + outp.DiagnosisCode2 + "," + outp.CauseCode2 + "," + outp.DoctorCode2 + "," +
               ") " +
                "Values ('" + p.MainHospitalCode + "','" + p.ServiceHospitalCode + "','" + p.IDCardNumber + "'," +
                "'" + p.HN + "','" + p.Birthdate + "','" + p.Sex + "'," +
                "'" + p.AdmitDate + "','" + p.TransferToHospitalCode + "','" + p.TotalSocialPaid + "'," +
                "'" + p.TotalPatientPaid + "','" + p.TotalOthersPaid + "','" + p.GrandTotal + "'," +
                "'" + p.DiagnosisCode1 + "','" + p.CauseCode1 + "','" + p.DoctorCode1 + "'," +
                "'" + p.DiagnosisCode2 + "','" + p.CauseCode2 + "','" + p.DoctorCode2 + "'," +
                ")";
            try
            {
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                chk = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "OutPatientDB insert error  " + ex.Message + " " + ex.InnerException+" "+ sql);
            }

            return re;
        }
        public String deleteAll(String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Delete from " + outp.table + "1 ";

            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "deleteAll error  " + ex.Message + " " + ex.InnerException);
            }

            return re;
        }
        public String deleteAll(String pathfilename, String userId)
        {
            String re = "";
            String sql = "";
            //p.ssdata_id = "";
            int chk = 0;
            
            OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
            conn.ConnectionString = "Provider = Microsoft.Jet.OLEDB.4.0;Data source= "+ pathfilename;

            sql = "Delete from " + outp.table + " ";
            try
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                chk = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "OutPatientDB deleteAll error  " + ex.Message + " " + ex.InnerException + " " + sql);
            }
            finally
            {
                conn.Close();
            }
            return re;
        }
        public String insertQueue(OutPatient p, String userId)
        {
            String re = "";

            if (p.TransactionNumber.Equals(""))
            {
                re = insert(p, "");
            }
            else
            {
                //re = update(p, "");
            }

            return re;
        }
        public String updateMDBNew(String datestart, String dateend, String paid_id, String statusdelete)
        {
            String re = "";
            String sql = "";
            try
            {
                //conn.comStore = new System.Data.SqlClient.SqlCommand();
                //conn.comStore.Connection = conn.connMainHIS;
                //conn.comStore.CommandText = "importMDB_new";
                //conn.comStore.CommandType = CommandType.StoredProcedure;
                //conn.comStore.Parameters.AddWithValue("date_start", datestart);
                //conn.comStore.Parameters.AddWithValue("date_end", dateend);
                //conn.comStore.Parameters.AddWithValue("paid_code", paid_id);
                //SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                //retval.Value = "";
                //retval.Direction = ParameterDirection.Output;
                //conn.connMainHIS.Open();
                //conn.comStore.ExecuteNonQuery();
                //re = (String)conn.comStore.Parameters["row_no1"].Value;

                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "importMDB_new1";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("date_start", datestart);
                conn.comStore.Parameters.AddWithValue("date_end", dateend);
                conn.comStore.Parameters.AddWithValue("paid_code", paid_id);
                conn.comStore.Parameters.AddWithValue("status_delete", statusdelete);
                conn.comStore.CommandTimeout = 0;
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;

                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;

            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateMDBNew  " + sql);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String updateMDB()
        {
            String re = "";
            String sql = "";
            try
            {
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "importMDB";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.CommandTimeout = 0;
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;
                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;

            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateMDB  " + sql );
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public DataTable selectAll(OleDbCommand cmdMDB)
        {
            DataTable dtmdb = new DataTable();
            String re = "", sql = "";
            sql = "Select MainHospitalCode,ServiceHospitalCode,IDCardNumber,hn,Birthdate,Sex,AdmitDate " +
                    ", TransferToHospitalCode,TotalSocialPaid,TotalPatientPaid,TotalOthersPaid,GrandTotal " +
                    ",SocialPaid1, PatientPaid1, OthersPaid1,SocialPaid2, PatientPaid2, OthersPaid2,SocialPaid3, PatientPaid3, OthersPaid3 " +
                    ",SocialPaid4, PatientPaid4, OthersPaid4,SocialPaid5, PatientPaid5, OthersPaid5,SocialPaid6, PatientPaid6, OthersPaid6 " +
                    ",SocialPaid7, PatientPaid7, OthersPaid7,SocialPaid8, PatientPaid8, OthersPaid8,SocialPaid9, PatientPaid9, OthersPaid9 " +
                    ",SocialPaid10, PatientPaid10, OthersPaid10,SocialPaid11, PatientPaid11, OthersPaid11,SocialPaid12, PatientPaid12, OthersPaid12 " +
                    ",SocialPaid13, PatientPaid13, OthersPaid13,SocialPaid14, PatientPaid14, OthersPaid14,SocialPaid15, PatientPaid15, OthersPaid15 " +
                    ",SocialPaid16, PatientPaid16, OthersPaid16,SocialPaid17, PatientPaid17, OthersPaid17,SocialPaid18, PatientPaid18, OthersPaid18 " +
                    ",isnull(aa) as aa " +
                    "from " + outp.table + " ";
            cmdMDB.CommandText = sql;
            OleDbDataAdapter adapMainhis = new OleDbDataAdapter(cmdMDB);
            adapMainhis.Fill(dtmdb);
            return dtmdb;
        }
        public String insertMDBNoClose(OleDbCommand cmdMDB)
        {
            String re = "", sql = "", err="";
            int rowi = 0, rowe = 0;
            try
            {
                DataTable dtmdb = new DataTable();
                sql = "Select MainHospitalCode,ServiceHospitalCode,IDCardNumber,hn,convert(VARCHAR(20),Birthdate,23) as Birthdate,Sex,convert(VARCHAR(20),AdmitDate,23) as AdmitDate " +
                    ", TransferToHospitalCode,TotalSocialPaid,TotalPatientPaid,TotalOthersPaid,GrandTotal " +
                    ",SocialPaid1, PatientPaid1, OthersPaid1,SocialPaid2, PatientPaid2, OthersPaid2,SocialPaid3, PatientPaid3, OthersPaid3 " +
                    ",SocialPaid4, PatientPaid4, OthersPaid4,SocialPaid5, PatientPaid5, OthersPaid5,SocialPaid6, PatientPaid6, OthersPaid6 " +
                    ",SocialPaid7, PatientPaid7, OthersPaid7,SocialPaid8, PatientPaid8, OthersPaid8,SocialPaid9, PatientPaid9, OthersPaid9 " +
                    ",SocialPaid10, PatientPaid10, OthersPaid10,SocialPaid11, PatientPaid11, OthersPaid11,SocialPaid12, PatientPaid12, OthersPaid12 " +
                    ",SocialPaid13, PatientPaid13, OthersPaid13,SocialPaid14, PatientPaid14, OthersPaid14,SocialPaid15, PatientPaid15, OthersPaid15 " +
                    ",SocialPaid16, PatientPaid16, OthersPaid16,SocialPaid17, PatientPaid17, OthersPaid17,SocialPaid18, PatientPaid18, OthersPaid18 " +
                    ",visit_date, aa, visit_time " +
                    "from " + outp.table+"1 ";
                dtmdb = conn.selectData(sql);
                if (dtmdb.Rows.Count > 0)
                {
                    foreach(DataRow drow in dtmdb.Rows)
                    {
                        OutPatient outp = new OutPatient();
                        outp.MainHospitalCode = "2210028";
                        outp.ServiceHospitalCode = "2210028";
                        outp.IDCardNumber = drow["IDCardNumber"] != null ? drow["IDCardNumber"].ToString() : "";
                        outp.HN = drow["hn"] != null ? drow["hn"].ToString() : "";
                        outp.Birthdate = drow["Birthdate"] != null ? drow["Birthdate"].ToString() : "";
                        outp.Sex = drow["Sex"] != null ? drow["Sex"].ToString() : "";
                        //outp.AdmitDate = drow["AdmitDate"] != null ? drow["AdmitDate"].ToString() : "";
                        outp.AdmitDate = drow["visit_date"] != null ? drow["visit_date"].ToString() : "";
                        if (outp.HN.Equals("5004225"))
                        {
                            String aaa = "";
                        }
                        if (drow["visit_time"] != null)
                        {
                            String time = drow["visit_time"].ToString();
                            if (time.Length >= 2)
                            {
                                time = "0000"+drow["visit_time"].ToString();
                                time = time.Substring(time.Length - 4);
                                time = time.Substring(0, 2) + ":" + time.Substring(time.Length - 2);
                                outp.AdmitDate += " " + time;
                            }
                        }
                        outp.TransferToHospitalCode = drow["TransferToHospitalCode"] != null ? drow["TransferToHospitalCode"].ToString() : "";
                        outp.TotalSocialPaid = drow["TotalSocialPaid"] != null ? drow["TotalSocialPaid"].ToString() : "0";
                        outp.TotalPatientPaid = drow["TotalPatientPaid"] != null ? drow["TotalPatientPaid"].ToString() : "";
                        outp.TotalOthersPaid = drow["TotalOthersPaid"] != null ? drow["TotalOthersPaid"].ToString() : "";
                        outp.GrandTotal = drow["GrandTotal"] != null ? drow["GrandTotal"].ToString() : "0";

                        outp.SocialPaid1 = drow["SocialPaid1"] != null ? drow["SocialPaid1"].ToString() : "0";
                        outp.PatientPaid1 = drow["PatientPaid1"] != null ? drow["PatientPaid1"].ToString() : "0";
                        outp.OthersPaid1 = drow["OthersPaid1"] != null ? drow["OthersPaid1"].ToString() : "0";

                        outp.SocialPaid2 = drow["SocialPaid2"] != null ? drow["SocialPaid2"].ToString() : "0";
                        outp.PatientPaid2 = drow["PatientPaid2"] != null ? drow["PatientPaid2"].ToString() : "0";
                        outp.OthersPaid2 = drow["OthersPaid2"] != null ? drow["OthersPaid2"].ToString() : "0";

                        outp.SocialPaid3 = drow["SocialPaid3"] != null ? drow["SocialPaid3"].ToString() : "0";
                        outp.PatientPaid3 = drow["PatientPaid3"] != null ? drow["PatientPaid3"].ToString() : "0";
                        outp.OthersPaid3 = drow["OthersPaid3"] != null ? drow["OthersPaid3"].ToString() : "0";

                        outp.SocialPaid4 = drow["SocialPaid4"] != null ? drow["SocialPaid4"].ToString() : "0";
                        outp.PatientPaid4 = drow["PatientPaid4"] != null ? drow["PatientPaid4"].ToString() : "0";
                        outp.OthersPaid4 = drow["OthersPaid4"] != null ? drow["OthersPaid4"].ToString() : "0";

                        outp.SocialPaid5 = drow["SocialPaid5"] != null ? drow["SocialPaid5"].ToString() : "0";
                        outp.PatientPaid5 = drow["PatientPaid5"] != null ? drow["PatientPaid5"].ToString() : "0";
                        outp.OthersPaid5 = drow["OthersPaid5"] != null ? drow["OthersPaid5"].ToString() : "0";

                        outp.SocialPaid6 = drow["SocialPaid6"] != null ? drow["SocialPaid6"].ToString() : "0";
                        outp.PatientPaid6 = drow["PatientPaid6"] != null ? drow["PatientPaid6"].ToString() : "0";
                        outp.OthersPaid6 = drow["OthersPaid6"] != null ? drow["OthersPaid6"].ToString() : "0";

                        outp.SocialPaid7 = drow["SocialPaid7"] != null ? drow["SocialPaid7"].ToString() : "0";
                        outp.PatientPaid7 = drow["PatientPaid7"] != null ? drow["PatientPaid7"].ToString() : "0";
                        outp.OthersPaid7 = drow["OthersPaid7"] != null ? drow["OthersPaid7"].ToString() : "0";

                        outp.SocialPaid8 = drow["SocialPaid8"] != null ? drow["SocialPaid8"].ToString() : "0";
                        outp.PatientPaid8 = drow["PatientPaid8"] != null ? drow["PatientPaid8"].ToString() : "0";
                        outp.OthersPaid8 = drow["OthersPaid8"] != null ? drow["OthersPaid8"].ToString() : "0";

                        outp.SocialPaid9 = drow["SocialPaid9"] != null ? drow["SocialPaid9"].ToString() : "0";
                        outp.PatientPaid9 = drow["PatientPaid9"] != null ? drow["PatientPaid9"].ToString() : "0";
                        outp.OthersPaid9 = drow["OthersPaid9"] != null ? drow["OthersPaid9"].ToString() : "0";

                        outp.SocialPaid10 = drow["SocialPaid10"] != null ? drow["SocialPaid10"].ToString() : "0";
                        outp.PatientPaid10 = drow["PatientPaid10"] != null ? drow["PatientPaid10"].ToString() : "0";
                        outp.OthersPaid10 = drow["OthersPaid10"] != null ? drow["OthersPaid10"].ToString() : "0";

                        outp.SocialPaid11 = drow["SocialPaid11"] != null ? drow["SocialPaid11"].ToString() : "0";
                        outp.PatientPaid11 = drow["PatientPaid11"] != null ? drow["PatientPaid11"].ToString() : "0";
                        outp.OthersPaid11 = drow["OthersPaid11"] != null ? drow["OthersPaid11"].ToString() : "0";

                        outp.SocialPaid12 = drow["SocialPaid12"] != null ? drow["SocialPaid12"].ToString() : "0";
                        outp.PatientPaid12 = drow["PatientPaid12"] != null ? drow["PatientPaid12"].ToString() : "0";
                        outp.OthersPaid12 = drow["OthersPaid12"] != null ? drow["OthersPaid12"].ToString() : "0";

                        outp.SocialPaid13 = drow["SocialPaid13"] != null ? drow["SocialPaid13"].ToString() : "0";
                        outp.PatientPaid13 = drow["PatientPaid13"] != null ? drow["PatientPaid13"].ToString() : "0";
                        outp.OthersPaid13 = drow["OthersPaid13"] != null ? drow["OthersPaid13"].ToString() : "0";

                        outp.SocialPaid14 = drow["SocialPaid14"] != null ? drow["SocialPaid14"].ToString() : "0";
                        outp.PatientPaid14 = drow["PatientPaid14"] != null ? drow["PatientPaid14"].ToString() : "0";
                        outp.OthersPaid14 = drow["OthersPaid14"] != null ? drow["OthersPaid14"].ToString() : "0";

                        outp.SocialPaid15 = drow["SocialPaid15"] != null ? drow["SocialPaid15"].ToString() : "0";
                        outp.PatientPaid15 = drow["PatientPaid15"] != null ? drow["PatientPaid15"].ToString() : "0";
                        outp.OthersPaid15 = drow["OthersPaid15"] != null ? drow["OthersPaid15"].ToString() : "0";

                        outp.SocialPaid16 = drow["SocialPaid16"] != null ? drow["SocialPaid16"].ToString() : "0";
                        outp.PatientPaid16 = drow["PatientPaid16"] != null ? drow["PatientPaid16"].ToString() : "0";
                        outp.OthersPaid16 = drow["OthersPaid16"] != null ? drow["OthersPaid16"].ToString() : "0";

                        outp.SocialPaid17 = drow["SocialPaid17"] != null ? drow["SocialPaid17"].ToString() : "0";
                        outp.PatientPaid17 = drow["PatientPaid17"] != null ? drow["PatientPaid17"].ToString() : "0";
                        outp.OthersPaid17 = drow["OthersPaid17"] != null ? drow["OthersPaid17"].ToString() : "0";

                        outp.SocialPaid18 = drow["SocialPaid18"] != null ? drow["SocialPaid18"].ToString() : "0";
                        outp.PatientPaid18 = drow["PatientPaid18"] != null ? drow["PatientPaid18"].ToString() : "0";
                        outp.OthersPaid18 = drow["OthersPaid18"] != null ? drow["OthersPaid18"].ToString() : "0";

                        outp.aa = drow["aa"] != null ? drow["aa"].ToString() : "";
                        outp.visit_time = drow["visit_time"] != null ? drow["visit_time"].ToString() : "";

                        re = insertNoClose(cmdMDB, outp, "");
                        
                        if (re.Length > 2)// ถ้ามี return len มากกว่า 2 น่าจะมี error
                        {
                            err += re + Environment.NewLine;
                        }
                        else
                        {
                            rowi++;
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                rowe++;
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insertMDB sql " + sql );
            }
            finally
            {
                re = "access "+rowi.ToString() + ";error " + rowe.ToString();
                if (err.Length > 0)
                {
                    re += Environment.NewLine + err;
                }
            }
            return re;
        }
    }
}
