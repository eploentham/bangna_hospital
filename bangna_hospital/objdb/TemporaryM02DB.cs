using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class TemporaryM02DB
    {
        public ConnectDB conn;
        public TemporaryM02 tem02;

        public TemporaryM02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            tem02 = new TemporaryM02();
            tem02.MNC_DOC_CD = "MNC_DOC_CD";
            tem02.MNC_DOC_YR = "MNC_DOC_YR";
            tem02.MNC_DOC_NO = "MNC_DOC_NO";
            tem02.MNC_DOC_DAT = "MNC_DOC_DAT";
            tem02.MNC_DEF_CD = "MNC_DEF_CD";
            tem02.MNC_DEF_DSC = "MNC_DEF_DSC";
            tem02.MNC_ORD_BY = "MNC_ORD_BY";
            tem02.MNC_AMT = "MNC_AMT";
            tem02.MNC_DEF_LEV = "MNC_DEF_LEV";
            tem02.MNC_TOT_AMT = "MNC_TOT_AMT";
            tem02.MNC_DIS_AMT = "MNC_DIS_AMT";
            tem02.MNC_STS = "MNC_STS";
            tem02.MNC_QTY = "MNC_QTY";

            tem02.table = "TEMPORARY_M02";
        }
        public DataTable SelectByDocno(String docno, String docdate)
        {
            DataTable dt = new DataTable();
            String re = "0";
            String sql = "select MNC_DOC_CD, MNC_DOC_YR, MNC_DOC_NO, cponvert(varchar(20),MNC_DOC_DAT,23) as MNC_DOC_DAT, MNC_DEF_CD, MNC_DEF_DSC, MNC_ORD_BY, MNC_AMT, MNC_DEF_LEV, MNC_TOT_AMT, MNC_DIS_AMT, MNC_STS, MNC_QTY " +
                "From TEMPORARY_M02  " +
                "Where MNC_DOC_NO = '" + docno + "' and MNC_DOC_DAT = '" + docdate + "'  " +
                "Order By mnc_DOC_DAT desc ";
            dt = conn.selectData(sql);

            return dt;
        }
    }
}
