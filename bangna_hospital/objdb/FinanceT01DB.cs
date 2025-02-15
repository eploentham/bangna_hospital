using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class FinanceT01DB
    {
        ConnectDB conn;
        public FinanceT01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

        }
        public DataTable SelectAllByHN(String hn)
        {
            DataTable dt = new DataTable();

            String sql = "select MNC_HN_NO,MNC_DOC_NO, MNC_DOC_DAT, mnc_DOC_CD, mnc_DOC_STS, mnc_SUM_PRI, MNC_JOB_NO, isnull(MNC_JOB_NOold,'') as MNC_JOB_NOold " +
                "From finance_t01  " +
                "Where MNC_HN_NO = '"+hn+"' " +
                "Order By mnc_DOC_DAT desc ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectPADByPreno1(String hn, String vsdate, String preno, String paidcode)
        {
            DataTable dt = new DataTable();
            String re = "0";
            String sql = "select MNC_HN_NO,MNC_DOC_NO, convert(varchar(20), MNC_DOC_DAT,23) as MNC_DOC_DAT,MNC_DOC_TIM, mnc_DOC_CD, mnc_DOC_STS, mnc_SUM_PRI, MNC_JOB_NO, isnull(MNC_JOB_NOold,'') as MNC_JOB_NOold " +
                "From finance_t01  " +
                "Where MNC_HN_NO = '" + hn + "' and MNC_PRE_NO = '"+ preno + "' and MNC_DATE = '"+ vsdate+ "' and MNC_FN_TYP_CD = '"+ paidcode + "' and MNC_DOC_STS <> 'V' " +
                "Order By mnc_DOC_DAT desc ";
            dt = conn.selectData(sql);

            return dt;
        }
        public String SelectPADByPreno(String hn, String vsdate, String preno, String paidcode)
        {
            DataTable dt = new DataTable();
            String re = "0";
            String sql = "select MNC_SUM_PRI " +
                "From finance_t01  " +
                "Where MNC_HN_NO = '" + hn + "' and MNC_PRE_NO = '" + preno + "' and MNC_DATE = '" + vsdate + "' and MNC_FN_TYP_CD = '" + paidcode + "' and MNC_DOC_STS <> 'V' " +
                "Order By mnc_DOC_DAT desc ";
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_SUM_PRI"].ToString();
            }
            if (float.TryParse(re, out float amt)) re = amt.ToString("#,###.00");
            return re;
        }
        public String SelectJOBNOCurrent()
        {
            DataTable dt = new DataTable();
            String year = "", jobno="";
            year = (DateTime.Now.Year+543).ToString();
            String sql = "Select top 1 MNC_DATE,MAX(MNC_JOB_NO) as MNC_JOB_NO " +
                "From FINANCE_T01  " +
                "Where MNC_HN_YR = '"+ year + "' and MNC_JOB_CD = 'fio' " +
                "Group by  MNC_DATE " +
                "Order By MNC_DATE desc ";
            dt = conn.selectData(sql);
            if(dt.Rows.Count > 0)
            {
                jobno = dt.Rows[0]["MNC_JOB_NO"].ToString();
            }
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return jobno;
        }
        public String setJobNo(String hn, String docno, String jobno)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update FINANCE_T01 " +
                    "Set MNC_JOB_NO = '" + jobno + "' " +
                    ",MNC_JOB_NOold = MNC_JOB_NO " +
                    "Where MNC_HN_NO = '" + hn + "' and MNC_DOC_NO = '"+ docno+"' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                new LogWriter("e", " FinanceM02DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
        public String revestJobNo(String hn, String docno, String jobnoold)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update FINANCE_T01 " +
                    "Set MNC_JOB_NO = '"+ jobnoold + "' " +
                    "Where MNC_HN_NO = '" + hn + "' and MNC_DOC_NO = '" + docno + "' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                new LogWriter("e", " FinanceM02DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
    }
}
