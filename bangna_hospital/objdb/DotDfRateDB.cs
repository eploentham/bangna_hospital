using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class DotDfRateDB
    {
        public DotDfRate dotdfrate;
        ConnectDB conn;
        DataTable DTRATE;
        public DotDfRateDB(ConnectDB c) {            conn = c;            initConfig();        }
        private void initConfig()
        {
            dotdfrate = new DotDfRate();
            dotdfrate.MNC_DOT_CD = "MNC_DOT_CD";
            dotdfrate.MNC_FN_CD = "MNC_FN_CD";
            dotdfrate.MNC_FN_STS = "MNC_FN_STS";
            dotdfrate.MNC_DF_GRP = "MNC_DF_GRP";
            dotdfrate.MNC_PAY_RATE = "MNC_PAY_RATE";
            dotdfrate.MNC_TYP = "MNC_TYP";
            dotdfrate.table = "dot_dfrate";
            dotdfrate.pkField = "MNC_DOT_CD";
        }
        public String getRate(String dtrcode)
        {
            String rate = "";
            if(DTRATE == null)            {                DTRATE = selectRate(dtrcode);            }
            foreach (DataRow row in DTRATE.Rows)
            {
                if ((row[dotdfrate.MNC_DOT_CD].ToString() == dtrcode) && (row[dotdfrate.MNC_FN_STS].ToString().Equals("1")))
                {
                    rate = row[dotdfrate.MNC_PAY_RATE].ToString();
                    break;
                }
            }
            return rate;
        }
        public DataTable selectRate(String dtrcode)
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dotdfrate.table + " " +
                "Where " + dotdfrate.MNC_DOT_CD + " = '" + dtrcode + "' Order By "+ dotdfrate.MNC_DOT_CD+"," + dotdfrate.MNC_FN_CD + "," + dotdfrate.MNC_FN_STS;
            dt = conn.selectData(sql);
            return dt;
        }
        public DotDfRate setData(DotDfRate p, System.Data.DataRow row)
        {
            p.MNC_DOT_CD = "";
            p.MNC_FN_CD = "";
            p.MNC_FN_STS = "";
            p.MNC_DF_GRP = "";
            p.MNC_PAY_RATE = "";
            p.MNC_TYP = "";
            return p;
        }
        public DotDfRate setData(DotDfRate p, System.Data.DataTable dt)
        {
            p.MNC_DOT_CD = dt.Rows[0][dotdfrate.MNC_DOT_CD].ToString();
            p.MNC_FN_CD = dt.Rows[0][dotdfrate.MNC_FN_CD].ToString();
            p.MNC_FN_STS = dt.Rows[0][dotdfrate.MNC_FN_STS].ToString();
            p.MNC_DF_GRP = dt.Rows[0][dotdfrate.MNC_DF_GRP].ToString();
            p.MNC_PAY_RATE = dt.Rows[0][dotdfrate.MNC_PAY_RATE].ToString();
            p.MNC_TYP = dt.Rows[0][dotdfrate.MNC_TYP].ToString();
            return p;
        }
    }
}
