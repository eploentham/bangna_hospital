using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class DotDfDetailDB
    {
        public DotDfDetail dotdfd;
        ConnectDB conn;

        public DotDfDetailDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            dotdfd = new DotDfDetail();
            dotdfd.MNC_DOC_CD = "MNC_DOC_CD";
            dotdfd.MNC_DOC_YR = "MNC_DOC_YR";
            dotdfd.MNC_DOC_NO = "MNC_DOC_NO";
            dotdfd.MNC_DOC_DAT = "MNC_DOC_DAT";
            dotdfd.MNC_FN_CD = "MNC_FN_CD";
            dotdfd.MNC_FN_NO = "MNC_FN_NO";
            dotdfd.MNC_FN_DAT = "MNC_FN_DAT";
            dotdfd.MNC_DF_DATE = "MNC_DF_DATE";
            dotdfd.MNC_FN_TYP_DESC = "MNC_FN_TYP_DESC";
            dotdfd.MNC_DF_AMT = "MNC_DF_AMT";
            dotdfd.MNC_FN_AMT = "MNC_FN_AMT";
            dotdfd.MNC_DATE = "MNC_DATE";
            dotdfd.MNC_TIME = "MNC_TIME";
            dotdfd.MNC_HN_NO = "MNC_HN_NO";
            dotdfd.MNC_HN_YR = "MNC_HN_YR";
            dotdfd.MNC_AN_NO = "MNC_AN_NO";
            dotdfd.MNC_AN_YR = "MNC_AN_YR";
            dotdfd.MNC_PAT_NAME = "MNC_PAT_NAME";
            dotdfd.MNC_PRE_NO = "MNC_PRE_NO";
            dotdfd.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            dotdfd.MNC_DOT_CD_DF = "MNC_DOT_CD_DF";
            dotdfd.MNC_DOT_GRP_CD = "MNC_DOT_GRP_CD";
            dotdfd.MNC_DOT_NAME = "MNC_DOT_NAME";
            dotdfd.MNC_PAY_FLAG = "MNC_PAY_FLAG";
            dotdfd.MNC_PAY_DAT = "MNC_PAY_DAT";
            dotdfd.MNC_PAY_NO = "MNC_PAY_NO";
            dotdfd.MNC_PAY_YR = "MNC_PAY_YR";
            dotdfd.MNC_REF_NO = "MNC_REF_NO";
            dotdfd.MNC_REF_DAT = "MNC_REF_DAT";
            dotdfd.MNC_EMP_CD = "MNC_EMP_CD";
            dotdfd.MNC_DF_GROUP = "MNC_DF_GROUP";
            dotdfd.MNC_PAY_TYP = "MNC_PAY_TYP";
            dotdfd.MNC_PAY_RATE = "MNC_PAY_RATE";
            dotdfd.MNC_DF_DET_TYPE = "MNC_DF_DET_TYPE";
            dotdfd.status_insert_manual = "status_insert_manual";
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select pm01.*, pm05.mnc_ph_pri01, pm05.mnc_ph_pri02, pm05.mnc_ph_pri03 " +
                "From pharmacy_m01 pm01 " +
                "inner join pharmacy_m05 pm05 on pm01.mnc_ph_cd = pm05.mnc_ph_cd " +
                //"Where MNC_FN_TYP_STS = 'Y' " +
                "Order By pm01.MNC_PH_CD";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        private void chkNull(DotDfDetail p)
        {
            long chk = 0;
            decimal chk1 = 0;

            p.MNC_DOC_CD = p.MNC_DOC_CD == null ? "" : p.MNC_DOC_CD;
            p.MNC_DOC_DAT = p.MNC_DOC_DAT == null ? "" : p.MNC_DOC_DAT;
            p.MNC_FN_CD = p.MNC_FN_CD == null ? "" : p.MNC_FN_CD;
            p.MNC_FN_DAT = p.MNC_FN_DAT == null ? "" : p.MNC_FN_DAT;
            p.MNC_DF_DATE = p.MNC_DF_DATE == null ? "" : p.MNC_DF_DATE;
            p.MNC_FN_TYP_DESC = p.MNC_FN_TYP_DESC == null ? "" : p.MNC_FN_TYP_DESC;
            p.MNC_DATE = p.MNC_DATE == null ? "" : p.MNC_DATE;
            p.MNC_PAT_NAME = p.MNC_PAT_NAME == null ? "" : p.MNC_PAT_NAME;
            p.MNC_FN_TYP_CD = p.MNC_FN_TYP_CD == null ? "" : p.MNC_FN_TYP_CD;
            p.MNC_DOT_CD_DF = p.MNC_DOT_CD_DF == null ? "" : p.MNC_DOT_CD_DF;
            p.MNC_DOT_GRP_CD = p.MNC_DOT_GRP_CD == null ? "" : p.MNC_DOT_GRP_CD;
            p.MNC_DOT_NAME = p.MNC_DOT_NAME == null ? "" : p.MNC_DOT_NAME;
            p.MNC_PAY_FLAG = p.MNC_PAY_FLAG == null ? "" : p.MNC_PAY_FLAG;
            p.MNC_PAY_DAT = p.MNC_PAY_DAT == null ? "" : p.MNC_PAY_DAT;
            p.MNC_REF_NO = p.MNC_REF_NO == null ? "" : p.MNC_REF_NO;
            p.MNC_REF_DAT = p.MNC_REF_DAT == null ? "" : p.MNC_REF_DAT;
            p.MNC_EMP_CD = p.MNC_EMP_CD == null ? "" : p.MNC_EMP_CD;
            p.MNC_DF_DET_TYPE = p.MNC_DF_DET_TYPE == null ? "" : p.MNC_DF_DET_TYPE;
            p.status_insert_manual = p.status_insert_manual == null ? "" : p.status_insert_manual;

            p.MNC_DOC_YR = long.TryParse(p.MNC_DOC_YR, out chk) ? chk.ToString() : "0";
            p.MNC_DOC_NO = long.TryParse(p.MNC_DOC_NO, out chk) ? chk.ToString() : "0";
            p.MNC_FN_NO = long.TryParse(p.MNC_FN_NO, out chk) ? chk.ToString() : "0";
            p.MNC_TIME = long.TryParse(p.MNC_TIME, out chk) ? chk.ToString() : "0";
            p.MNC_HN_NO = long.TryParse(p.MNC_HN_NO, out chk) ? chk.ToString() : "0";
            p.MNC_HN_YR = long.TryParse(p.MNC_HN_YR, out chk) ? chk.ToString() : "0";
            p.MNC_AN_NO = long.TryParse(p.MNC_AN_NO, out chk) ? chk.ToString() : "0";
            p.MNC_AN_YR = long.TryParse(p.MNC_AN_YR, out chk) ? chk.ToString() : "0";
            p.MNC_PRE_NO = long.TryParse(p.MNC_PRE_NO, out chk) ? chk.ToString() : "0";
            p.MNC_PAY_NO = long.TryParse(p.MNC_PAY_NO, out chk) ? chk.ToString() : "0";
            p.MNC_PAY_YR = long.TryParse(p.MNC_PAY_YR, out chk) ? chk.ToString() : "0";
            p.MNC_DF_GROUP = long.TryParse(p.MNC_DF_GROUP, out chk) ? chk.ToString() : "0";
            p.MNC_PAY_TYP = long.TryParse(p.MNC_PAY_TYP, out chk) ? chk.ToString() : "0";

            p.MNC_DF_AMT = decimal.TryParse(p.MNC_DF_AMT, out chk1) ? chk1.ToString() : "0";
            p.MNC_FN_AMT = decimal.TryParse(p.MNC_FN_AMT, out chk1) ? chk1.ToString() : "0";
            p.MNC_PAY_RATE = decimal.TryParse(p.MNC_PAY_RATE, out chk1) ? chk1.ToString() : "0";
        }
        public String insert(DotDfDetail p)
        {
            String sql = "", chk = "";
            try
            {
                chkNull(p);
                sql = "Insert Into " + dotdfd.table + "(" + dotdfd.MNC_DOC_CD + "," + dotdfd.MNC_DOC_YR + "," + dotdfd.MNC_DOC_NO + "," +
                    dotdfd.MNC_DOC_DAT + "," + dotdfd.MNC_FN_CD + "," + dotdfd.MNC_FN_NO + "," +
                    dotdfd.MNC_FN_DAT + "," + dotdfd.MNC_DF_DATE + "," + dotdfd.MNC_FN_TYP_DESC + "," +
                    dotdfd.MNC_DF_AMT + "," + dotdfd.MNC_FN_AMT + "," + dotdfd.MNC_DATE + "," +
                    dotdfd.MNC_TIME + "," + dotdfd.MNC_HN_NO + "," + dotdfd.MNC_HN_YR + "," +
                    dotdfd.MNC_AN_NO + "," + dotdfd.MNC_AN_YR + "," + dotdfd.MNC_PAT_NAME + "," +
                    dotdfd.MNC_PRE_NO + "," + dotdfd.MNC_FN_TYP_CD + "," + dotdfd.MNC_DOT_CD_DF + "," +
                    dotdfd.MNC_DOT_GRP_CD + "," + dotdfd.MNC_DOT_NAME + "," + dotdfd.MNC_PAY_FLAG + "," +
                    dotdfd.MNC_DF_GROUP + "," + dotdfd.MNC_PAY_TYP + "," + dotdfd.MNC_PAY_RATE + "," +
                    dotdfd.status_insert_manual +
                    ") " +
                    "Values('" + p.MNC_DOC_CD + "','" + p.MNC_DOC_YR + "','" + p.MNC_DOC_NO + "','" +
                    p.MNC_DOC_DAT + "','" + p.MNC_FN_CD + "','" + p.MNC_FN_NO + "','" +
                    p.MNC_FN_DAT + "','" + p.MNC_DF_DATE.Replace("'", "''") + "','" + p.MNC_FN_TYP_DESC.Replace("'", "''") + "','" +
                    p.MNC_DF_AMT + "','" + p.MNC_FN_AMT + "','" + p.MNC_DATE + "','" +
                    p.MNC_TIME + "','" + p.MNC_HN_NO + "','" + p.MNC_HN_YR + "','" +
                    p.MNC_AN_NO + "','" + p.MNC_AN_YR + "','" + p.MNC_PAT_NAME + "','" +
                    p.MNC_PRE_NO + "','" + p.MNC_FN_TYP_CD + "','" + p.MNC_DOT_CD_DF + "','" +
                    p.MNC_DOT_GRP_CD + "','" + p.MNC_DOT_NAME + "','" + p.MNC_PAY_FLAG + "','" +
                    p.MNC_DF_GROUP + "','" + p.MNC_PAY_TYP + "','" + p.MNC_PAY_RATE + "','1'" +
                    ") ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                chk = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert Insert  sql " + sql + " ex " + chk);
            }
            return chk;
        }
    }
}
