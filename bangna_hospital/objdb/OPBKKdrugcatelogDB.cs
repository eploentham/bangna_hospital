using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class OPBKKdrugcatelogDB
    {
        public OPBKKdrugcatelog opbkkDrugCat;
        ConnectDB conn;
        public List<OPBKKdrugcatelog> lopbkkDrugCat;

        public OPBKKdrugcatelogDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            opbkkDrugCat = new OPBKKdrugcatelog();
            lopbkkDrugCat = new List<OPBKKdrugcatelog>();
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select * " +
                "From drugcatalog  ";
            dt = conn.selectData(conn.connOPBKK, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public void getlopbkkDrugCat()
        {
            lopbkkDrugCat.Clear();
            DataTable dt = new DataTable();
            dt = SelectAll();
            foreach (DataRow row in dt.Rows)
            {
                OPBKKdrugcatelog itm1 = new OPBKKdrugcatelog();
                itm1.hospdrugcode = row["hospdrugcode"].ToString();
                itm1.genericname = row["genericname"].ToString();
                itm1.tmtid = row["tmtid"].ToString();

                //itm1.is_ipd = row[bsp.is_ipd].ToString();
                lopbkkDrugCat.Add(itm1);
            }
        }
        public String getTmtCodeBydrugcode(String drugcode)
        {
            String tmtcode = "";
            foreach(OPBKKdrugcatelog opbkkDrugcat in lopbkkDrugCat)
            {
                if (opbkkDrugcat.hospdrugcode.Equals(drugcode))
                {
                    tmtcode = opbkkDrugcat.tmtid;
                }
            }
            return tmtcode;
        }
    }
}
