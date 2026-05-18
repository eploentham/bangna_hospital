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

        public DataTable SelectPaidOPDByDate(String vsdate)
        {
            DataTable dt = new DataTable();

            String sql = "select ft01.MNC_HN_NO,ft01.MNC_DOC_NO, convert(varchar(20),ft01.MNC_DOC_DAT,23) as MNC_DOC_DAT,ft01.mnc_DOC_CD, ft01.mnc_DOC_STS, ft01.mnc_SUM_PRI " +
                ", ft01.MNC_JOB_NO, ft01.MNC_DOC_TIM, ft01.MNC_FN_TYP_CD, ft01.MNC_DIA_DSC,fn02.MNC_FN_TYP_DSC " +
                ",isnull(pm02ptt.MNC_PFIX_DSC,'') +' '+isnull(pm01.MNC_FNAME_T,'')+' '+isnull(pm01.MNC_LNAME_T,'') as pttfullname,ft01.MNC_DOC_TIM,ft01.MNC_PRE_NO " +
                ",isnull(ft01.MNC_AN_NO,'') as MNC_AN_NO, convert(varchar(20), ft01.MNC_DATE, 23) as MNC_DATE, pt01.MNC_TIME " +
                "From finance_t01 ft01  " +
                " Left Join finance_m02 fn02 on ft01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +
                " inner join patient_m01 pm01 on ft01.MNC_HN_NO = pm01.MNC_HN_NO " +
                " left join patient_m02 pm02ptt on pm01.MNC_PFIX_CDT = pm02ptt.MNC_PFIX_CD " +
                "Left Join Patient_t01 pt01 on ft01.mnc_hn_no = pt01.mnc_HN_NO and ft01.MNC_DATE = pt01.MNC_DATE and ft01.MNC_PRE_NO = pt01.MNC_PRE_NO " +
                "Where ft01.MNC_DOC_DAT = '" + vsdate + "' and ft01.MNC_DOC_STS = 'F'  " +
                //" and ft01.MNC_AN_NO is null " +      //การเงินแจ้ง ให้ดึงคนไข้ในมาด้วย and ft01.MNC_AN_NO is null
                "Order By ft01.MNC_DOC_TIM desc ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
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
        public DataTable SelectItemsByDocNo(String docno, String docdate)
        {
            DataTable dt = new DataTable();
            String sql = @" SELECT             'parent' AS service_type,            fm01.CodeBSG,ft01.MNC_USR_CD,
            -- fm01.MNC_SIMB_CD,
            CAST(simb2.BillingSubGroupTH AS VARCHAR(500)) AS item_name,
            CAST(simb2.BillingGroupTH AS VARCHAR(500)) AS BillingGroupTH,
            CAST(simb2.BillingGroupEN AS VARCHAR(500)) AS BillingGroupEN,
            CAST(simb2.BillingSubGroupTH AS VARCHAR(500)) AS BillingSubGroupTH,
            CAST(simb2.BillingSubGroupEN AS VARCHAR(500)) AS BillingSubGroupEN,
            '' AS MNC_DEF_DSC,
            SUM(ft02.MNC_FN_AMT) AS MNC_FN_AMT,     SUM(ft02.MNC_FN_PAD) AS MNC_FN_PAD,     SUM(ft02.MNC_FN_OUT) AS MNC_FN_OUT,
            SUM(ft02.MNC_FN_DIS) AS MNC_FN_DIS,
            SUM(ISNULL(ft02.MNC_FN_QTY, 0)) AS MNC_FN_QTY,
            CAST(ft01.MNC_DIA_DSC AS VARCHAR(500)) AS MNC_DIA_DSC,
            ft01.MNC_PAY_CASH,          ft01.MNC_PAY_CARD,          ft01.MNC_DIS_TOT,       ft01.MNC_DIS_PER,       ft01.MNC_SUM_DIS,
            CAST(pt01.MNC_SHIF_MEMO AS VARCHAR(500)) AS MNC_SHIF_MEMO,
            pt01.MNC_VN_NO,            pt01.MNC_VN_SEQ,            ft01.MNC_HN_NO,            ft01.MNC_PRE_NO,
            CONVERT(VARCHAR(20), ft01.MNC_DATE, 23) AS MNC_DATE,
            pt01.MNC_TIME,            ft01.MNC_FN_TYP_CD,
            CONVERT(VARCHAR(20), ft01.MNC_DOC_DAT, 23) AS MNC_DOC_DAT,
            ft01.MNC_DOC_NO,
            CAST(fm02.MNC_FN_TYP_DSC AS VARCHAR(200)) AS MNC_FN_TYP_DSC,
            (SELECT TOP 1 MNC_NO FROM finance_t02 
             WHERE MNC_DOC_CD = ft01.MNC_DOC_CD         AND MNC_DOC_YR = ft01.MNC_DOC_YR    AND MNC_DOC_NO = ft01.MNC_DOC_NO) AS MNC_NO
        FROM finance_t01 ft01
        INNER JOIN finance_t02 ft02 ON ft01.MNC_DOC_CD = ft02.MNC_DOC_CD    AND ft01.MNC_DOC_YR = ft02.MNC_DOC_YR   AND ft01.MNC_DOC_NO = ft02.MNC_DOC_NO
        LEFT JOIN finance_m01 fm01 ON ft02.MNC_FN_CD = fm01.MNC_FN_CD
        --LEFT JOIN FINANCE_M11 fm11 ON fm01.MNC_SIMB_CD = fm11.MNC_DEF_CD
        INNER JOIN PATIENT_T01 pt01 ON ft01.MNC_HN_NO = pt01.MNC_HN_NO  AND pt01.MNC_PRE_NO = ft01.MNC_PRE_NO AND pt01.MNC_DATE = ft01.MNC_DATE
        LEFT JOIN SIMB2_BillingGroup simb2 ON fm01.CodeBSG = simb2.CodeBSG
        LEFT JOIN FINANCE_M02 fm02 ON ft01.MNC_FN_TYP_CD = fm02.MNC_FN_TYP_CD
        WHERE ft01.MNC_DOC_DAT = '" + docdate + @"' 
          AND ft01.MNC_DOC_NO = '" + docno + @"'
        GROUP BY 
            fm01.CodeBSG,
            --fm01.MNC_SIMB_CD,
            CAST(simb2.BillingGroupTH AS VARCHAR(500)),
            CAST(simb2.BillingGroupEN AS VARCHAR(500)),
            CAST(simb2.BillingSubGroupTH AS VARCHAR(500)),
            CAST(simb2.BillingSubGroupEN AS VARCHAR(500)),
            CAST(ft01.MNC_DIA_DSC AS VARCHAR(500)),
            ft01.MNC_PAY_CASH,      ft01.MNC_PAY_CARD,      ft01.MNC_DIS_TOT,   ft01.MNC_DIS_PER,            ft01.MNC_SUM_DIS,
            CAST(pt01.MNC_SHIF_MEMO AS VARCHAR(500)),
            pt01.MNC_VN_NO,         pt01.MNC_VN_SEQ,        ft01.MNC_HN_NO,     ft01.MNC_PRE_NO,            ft01.MNC_DATE,
            pt01.MNC_TIME,          ft01.MNC_FN_TYP_CD,     ft01.MNC_DOC_DAT,   ft01.MNC_DOC_NO,
            CAST(fm02.MNC_FN_TYP_DSC AS VARCHAR(200)),
            ft01.MNC_DOC_CD,            ft01.MNC_DOC_YR,ft01.MNC_USR_CD
        ORDER BY fm01.CodeBSG, CAST(simb2.BillingSubGroupTH AS VARCHAR(500))";
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
        public DataTable SelectBillByDocno(String docno, String docdate)
        {
            DataTable dt = new DataTable();
            String re = "0";
            String sql = "select ft01.MNC_USR_CD,ft01.MNC_PAY_CASH  " +
                "from FINANCE_T01 ft01 " +
                //"left join FINANCE_T03 ft03 on ft01.MNC_DOC_CD = ft03.MNC_DOC_CD and ft01.MNC_DOC_NO = ft03.MNC_DOC_NO and ft01.MNC_DOC_YR = ft03.MNC_DOC_YR " +
                //"left join FINANCE_M01 fm01 on ft03.MNC_FN_CD = fm01.MNC_FN_CD " +
                "where  ft01.MNC_DOC_NO = '" + docno + "' and ft01.MNC_DOC_DAT = '" + docdate + "'; ";
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable SelectBillBypreno(String hn, String vsdate, String preno)
        {
            DataTable dt = new DataTable();
            String re = "0";
            String sql = "select ft03.MNC_APP_CD2,convert(varchar(9)20),ft03.MNC_FN_DAT,23) as MNC_FN_DAT, ft03.MNC_FN_TYP_CD, ft03.MNC_FN_CD, fm01.MNC_FN_DSCT, fm01.mnc_grp_ss1,ft03.MNC_FN_PAD  " +
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
        public DataTable SelectCardRemark(String docno, String docdate)
        {
            DataTable dt = new DataTable();
            String re = "0";
            String sql = "select ft05.*,fm13.MNC_CARD_DSC  " +
                "from FINANCE_T05  ft05 " +
                "left join FINANCE_M13 fm13 on ft05.MNC_CARD_ID = fm13.MNC_CARD_CD " +
                "Where ft05.MNC_DOC_NO  = '"+ docno + "' and ft05.MNC_DOC_DAT = '"+ docdate + "'; ";
            dt = conn.selectData(sql);

            return dt;
        }
    }
}
