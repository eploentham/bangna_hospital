using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PatientT03DB
    {
        public ConnectDB conn;
        PatientT03 pt03;
        public PatientT03DB(ConnectDB c) {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pt03 = new PatientT03();
            pt03.MNC_HN_YR = "MNC_HN_YR";
            pt03.MNC_HN_NO = "MNC_HN_NO";
            pt03.MNC_DATE = "MNC_DATE";
            pt03.MNC_PRE_NO = "MNC_PRE_NO";
            pt03.MNC_ACT_NO = "MNC_ACT_NO";
            pt03.MNC_TIME = "MNC_TIME";
            pt03.MNC_AN_NO = "MNC_AN_NO";
            pt03.MNC_AN_YR = "MNC_AN_YR";
            pt03.MNC_DEP_NO = "MNC_DEP_NO";
            pt03.MNC_SEC_NO = "MNC_SEC_NO";
            pt03.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pt03.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pt03.pkField = "";
            pt03.table = "patient_T03";
        }
        public DataTable SelectByVsDate(String hn, String preno, String vsdate)
        {
            DataTable dt = new DataTable();

            String sql = "select pt03.MNC_ACT_NO,pt03.MNC_STP_TIME,pt03.MNC_STP_DATE,pt03.MNC_SEC_NO,pt03.MNC_DEP_NO, am01.MNC_ACT_DSC " +
                ",um01.MNC_USR_FULL " +
                "From patient_T03 pt03 " +
                "Inner join ACTION_M01 am01 on pt03.MNC_ACT_NO = am01.MNC_ACT_NO " +
                "Left Join USERLOG_M01 um01 on pt03.MNC_EMP_CD = um01.MNC_USR_NAME " +
                "Where pt03.MNC_HN_NO = '" + hn + "' and pt03.MNC_PRE_NO = '" + preno + "' and pt03.MNC_DATE = '" + vsdate + "'" +
                "Order By pt03.MNC_STP_TIME ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        private void chkNull(PatientT03 p)
        {
            long chk = 0;
            decimal chk1 = 0;

            p.MNC_HN_NO = p.MNC_HN_NO == null ? "" : p.MNC_HN_NO;
            p.MNC_HN_YR = p.MNC_HN_YR == null ? "" : p.MNC_HN_YR;
            p.MNC_DATE = p.MNC_DATE == null ? "" : p.MNC_DATE;
            p.MNC_TIME = p.MNC_TIME == null ? "" : p.MNC_TIME;

            p.MNC_DEP_NO = long.TryParse(p.MNC_DEP_NO, out chk) ? chk.ToString() : "0";
            p.MNC_SEC_NO = long.TryParse(p.MNC_SEC_NO, out chk) ? chk.ToString() : "0";
            p.MNC_AN_YR = long.TryParse(p.MNC_AN_YR, out chk) ? chk.ToString() : "0";
            p.MNC_AN_NO = long.TryParse(p.MNC_AN_NO, out chk) ? chk.ToString() : "0";
            p.MNC_PRE_NO = long.TryParse(p.MNC_PRE_NO, out chk) ? chk.ToString() : "0";
        }
        public String insertPatientT03(PatientT03 p)
        {
            String sql = "", chk = "", hn = "";
            long hn1 = 0;
            
            chk = insert(p);
            
            return chk;
        }
        public String insert(PatientT03 p)
        {
            String sql = "", chk = "", hn = "";
            long hn1 = 0;
            try
            {
                chkNull(p);
                //hn = selectHnMax();
                //long.TryParse(hn, out hn1);
                //hn1++;
                //hn = hn1.ToString();
                if (p.MNC_HN_YR.Length <= 0)
                {
                    p.MNC_HN_YR = (DateTime.Now.Year + 543).ToString();
                }
                //ptt.MNC_FIN_NOTE = "MNC_FIN_NOTE";
                sql = "Insert Into " + pt03.table + "(" + pt03.MNC_HN_NO + "," + pt03.MNC_HN_YR + "," + pt03.MNC_DATE + "," +
                    pt03.MNC_PRE_NO + "," + pt03.MNC_ACT_NO + "," + pt03.MNC_TIME + "," +
                    pt03.MNC_DEP_NO + "," + pt03.MNC_SEC_NO + "," + pt03.MNC_AN_NO + "," +
                    pt03.MNC_AN_YR + ","  + pt03.MNC_STAMP_DAT + "," + pt03.MNC_STAMP_TIM + " " +

                    ") " +
                    "Values( '" +
                    p.MNC_HN_NO + "','" + p.MNC_HN_YR + "','" + p.MNC_DATE + "','" +
                    p.MNC_PRE_NO + "','" + p.MNC_ACT_NO + "','" + p.MNC_TIME + "','" +
                    p.MNC_DEP_NO + "','" + p.MNC_SEC_NO + "','" + p.MNC_AN_NO + "','" +

                    p.MNC_AN_YR + "',convert(varchar(20), getdate(),23),REPLACE(convert(varchar(5),getdate(),108),':','') " +
                    ") ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //new LogWriter("d", "insert Patient chk " + chk);
            }
            catch (Exception ex)
            {
                chk = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert PatientT07 sql " + sql + " ex " + chk);
            }
            return chk;
        }
    }
}
