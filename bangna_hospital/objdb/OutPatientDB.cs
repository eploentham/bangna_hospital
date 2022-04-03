using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
                "" + outp.DiagnosisCode1 + "," + outp.CauseCode1 + "," + outp.DoctorCode1 + "," +
                "" + outp.DiagnosisCode2 + "," + outp.CauseCode2 + "," + outp.DoctorCode2 + " " +
               ") " +
                "Values ('" + p.MainHospitalCode + "','" + p.ServiceHospitalCode + "','" + p.IDCardNumber + "'," +
                "'" + p.HN + "','" + p.Birthdate + "','" + p.Sex + "'," +
                "'" + p.AdmitDate + "',NULL,'" + p.TotalSocialPaid + "'," +
                "'" + p.TotalPatientPaid + "','" + p.TotalOthersPaid + "','" + p.GrandTotal + "'," +
                "'" + p.DiagnosisCode1 + "',NULL,'" + p.DoctorCode1 + "'," +
                "NULL,NULL,NULL " +
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
        public String deleteAll(String pathfilename, String userId)
        {
            String re = "";
            String sql = "";
            //p.ssdata_id = "";
            int chk = 0;
            
            OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
            conn.ConnectionString = "Provider = Microsoft.Jet.OLEDB.4.0;Data source= "+ pathfilename;
            conn.Open();

            sql = "Delete from " + outp.table + " ";
            try
            {
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                chk = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "OutPatientDB deleteAll error  " + ex.Message + " " + ex.InnerException + " " + sql);
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
    }
}
