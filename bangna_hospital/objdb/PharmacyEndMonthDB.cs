using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PharmacyEndMonthDB
    {
        public PharmacyEndMonth endyear;        // แก้ให้เป็น endyear จะได้ทำ ปีละครั้ง
        ConnectDB conn;
        public PharmacyEndMonthDB(ConnectDB c)
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            endyear = new PharmacyEndMonth();
            endyear.MNC_YEAR = "MNC_YEAR";
            endyear.MNC_MONTH = "MNC_MONTH";
            endyear.MNC_PH_CD = "MNC_PH_CD";
            endyear.MNC_UNT_CD = "MNC_UNT_CD";
            endyear.MNC_DEP_NO = "MNC_DEP_NO";
            endyear.MNC_SEC_NO = "MNC_SEC_NO";
            endyear.MNC_PH_COST_AVG = "MNC_PH_COST_AVG";
            endyear.MNC_PH_QTY_REMAIN = "MNC_PH_QTY_REMAIN";
            endyear.MNC_PH_QTY_TRANS = "MNC_PH_QTY_TRANS";
            endyear.MNC_PH_QTY_USE = "MNC_PH_QTY_USE";

            endyear.MNC_PH_QTY = "MNC_PH_QTY";
            endyear.MNC_OLD_CD = "MNC_OLD_CD";
            endyear.MNC_PH_QTY_ADJ = "MNC_PH_QTY_ADJ";
            endyear.MNC_PH_COST_ADJ = "MNC_PH_COST_ADJ";
            endyear.MNC_PH_COST_BEFORE = "MNC_PH_COST_BEFORE";
            endyear.MNC_PH_QTY_BEFORE = "MNC_PH_QTY_BEFORE";
            endyear.adjust = "adjust";
            endyear.onhandnew = "onhandnew";
            endyear.table = "PHARMACY_ENDMONTH";
        }
        public DataTable SelectByYear(String year)
        {
            DataTable dt = new DataTable();

            String sql = "Select endyear.MNC_YEAR,endyear.MNC_MONTH,endyear.MNC_PH_CD,endyear.MNC_UNT_CD,endyear.MNC_DEP_NO, endyear.MNC_SEC_NO " +
                ", endyear.MNC_PH_COST_AVG, endyear.MNC_PH_QTY_USE, endyear.MNC_PH_QTY, endyear.MNC_OLD_CD,pharm01.MNC_PH_TN, endyear.adjust, endyear.onhandnew " +
                "From PHARMACY_ENDMONTH endyear " +
                "left join BNG5_DBMS_FRONT.dbo.PHARMACY_M01 pharm01 on endyear.MNC_PH_CD = pharm01.MNC_PH_CD  " +
                "Where endyear.MNC_YEAR = '" + year + "' Order By MNC_PH_CD";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public String updateOnhandNew(String itemcode, String year, float onhandnew)
        {
            String sql = "", re = "";
            sql = "update PHARMACY_ENDMONTH set " +
                "onhandnew = '" + onhandnew + "' " +
                "Where MNC_PH_CD = '" + itemcode + "' and MNC_YEAR = '" + year + "' and MNC_MONTH = '01' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateOnhandNew sql " + sql);
            }
            return re;
        }
        public String updatePHQTY(String itemcode, String year, float onhandnew)
        {
            String sql = "", re = "";
            sql = "update PHARMACY_ENDMONTH set " +
                "MNC_PH_QTY = '" + onhandnew + "' " +
                "Where MNC_PH_CD = '" + itemcode + "' and MNC_YEAR = '" + year + "' and MNC_MONTH = '01' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updatePHQTY sql " + sql);
            }
            return re;
        }
    }
}
