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
        public DataTable SelectByPreno(String hn, String vsdate, String preno)
        {
            DataTable dt = new DataTable();
            String re = "0";
            String sql = "select MNC_HN_NO,MNC_DOC_NO,MNC_SEC_NO, convert(varchar(20), MNC_DOC_DAT,23) as MNC_DOC_DAT,MNC_DOC_TIM, mnc_DOC_CD " +
                ", MNC_DOC_YR, mnc_DOC_STS, isnull(mnc_SUM_PRI,0) as mnc_SUM_PRI, isnull(MNC_PAY_CASH,0) as MNC_PAY_CASH, MNC_JOB_NO, isnull(MNC_JOB_NOold,'') as MNC_JOB_NOold" +
                ",MNC_FN_TYP_CD " +
                "From finance_t01  " +
                "Where MNC_HN_NO = '" + hn + "' and MNC_PRE_NO = '" + preno + "' and MNC_DATE = '" + vsdate + "' and MNC_DOC_STS <> 'V' " +
                "Order By mnc_DOC_DAT desc ";
            dt = conn.selectData(sql);

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
        public DataTable SelectBillByDocno(String docyear, String docno, String docdate)
        {
            DataTable dt = new DataTable();
            String re = "0";
            String sql = "select ft03.MNC_APP_CD2,ft03.MNC_FN_DAT,ft03.MNC_HN_NO, ft03.MNC_FN_TYP_CD, ft03.MNC_FN_CD, fm01.MNC_FN_DSCT, fm01.mnc_grp_ss1,ft03.MNC_FN_PAD  " +
                "from FINANCE_T01 ft01 "+
                "left join FINANCE_T03 ft03 on ft01.MNC_DOC_CD = ft03.MNC_DOC_CD and ft01.MNC_DOC_NO = ft03.MNC_DOC_NO and ft01.MNC_DOC_YR = ft03.MNC_DOC_YR "+
                "left join FINANCE_M01 fm01 on ft03.MNC_FN_CD = fm01.MNC_FN_CD " +
                "where ft01.MNC_DOC_YR = '"+ docyear + "' and ft01.MNC_DOC_NO = '"+ docno + "' and ft01.MNC_DOC_DAT = '"+ docdate + "'; ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable SelectBillBypreno(String hn, String vsdate, String preno)
        {
            DataTable dt = new DataTable();
            String re = "0";
            String sql = "select ft03.MNC_APP_CD2,convert(varchar920),ft03.MNC_FN_DAT,23) as MNC_FN_DAT, ft03.MNC_FN_TYP_CD, ft03.MNC_FN_CD, fm01.MNC_FN_DSCT, fm01.mnc_grp_ss1,ft03.MNC_FN_PAD  " +
                "From FINANCE_T01 ft01 " +
                "left join FINANCE_T03 ft03 on ft01.MNC_DOC_CD = ft03.MNC_DOC_CD and ft01.MNC_DOC_NO = ft03.MNC_DOC_NO and ft01.MNC_DOC_YR = ft03.MNC_DOC_YR " +
                "left join FINANCE_M01 fm01 on ft03.MNC_FN_CD = fm01.MNC_FN_CD " +
                "where ft01.MNC_HN_NO = '" + hn + "' and ft01.MNC_DATE = '" + vsdate + "' and ft01.MNC_PRE_NO = '" + preno + "'; ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable SelectBillGroupSS1Bypreno(String hn, String vsdate, String preno)
        {
            DataTable dt = new DataTable();
            String re = "0";
            String sql = "select convert(varchar(20),ft03.MNC_FN_DAT,23) as MNC_FN_DAT, ft03.MNC_FN_CD, fm01.MNC_FN_DSCT, fm01.mnc_grp_ss1,sum(ft03.MNC_FN_PAD) as MNC_FN_PAD,ft03.MNC_DOT_CD_DF,ft03.MNC_FN_NO " +
                "From FINANCE_T01 ft01 " +
                "left join FINANCE_T03 ft03 on ft01.MNC_DOC_CD = ft03.MNC_DOC_CD and ft01.MNC_DOC_NO = ft03.MNC_DOC_NO and ft01.MNC_DOC_YR = ft03.MNC_DOC_YR " +
                "left join FINANCE_M01 fm01 on ft03.MNC_FN_CD = fm01.MNC_FN_CD " +
                "where ft01.MNC_HN_NO = '" + hn + "' and ft01.MNC_DATE = '" + vsdate + "' and ft01.MNC_PRE_NO = '" + preno + "' " +
                "Group By ft03.MNC_FN_DAT, ft03.MNC_FN_CD, fm01.MNC_FN_DSCT, fm01.mnc_grp_ss1, ft03.MNC_FN_PAD,ft03.MNC_DOT_CD_DF,ft03.MNC_FN_NO; ";
            dt = conn.selectData(sql);

            return dt;
        }
    }
}
