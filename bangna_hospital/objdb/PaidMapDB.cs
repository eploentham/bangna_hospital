using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PaidMapDB
    {
        ConnectDB conn;
        public PaidMap paidMap;
        public PaidMapDB(ConnectDB c)
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            paidMap = new PaidMap();
            paidMap.paid_map_id = "paid_map_id";
            paidMap.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            paidMap.MNC_PH_CD = "MNC_PH_CD";
            paidMap.active = "active";
            paidMap.status_paid= "status_paid";
            paidMap.pkField = "paid_map_id";
            paidMap.table = "b_paid_map";
        }
        public String selectByPk(String pk)
        {
            String sql = "", re = "";
            sql = "Select " + paidMap.MNC_PH_CD + " From " + paidMap.table + " Where " + paidMap.pkField + "='" + pk + "'";
            return re;
        }
        public DataTable selectByFnTyp(String fnTyp)
        {
            String sql = "", re = "";
            sql = "Select " + paidMap.MNC_PH_CD + " From " + paidMap.table + " Where " + paidMap.MNC_FN_TYP_CD + "='" + fnTyp + "' and active = '1' ";
            DataTable dt = new DataTable();
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectByDrugCode(String drugcode)
        {
            String sql = "", re = "";
            sql = "Select " + paidMap.MNC_FN_TYP_CD + " From " + paidMap.table + " Where " + paidMap.MNC_PH_CD + "='" + drugcode + "' and active = '1' ";
            DataTable dt = new DataTable();
            dt = conn.selectData(sql);
            return dt;
        }
        public String insert(PaidMap p)
        {
            String sql = "", re = "";
            if (p.paid_map_id.Equals(""))
            {
                p.paid_map_id = p.getGenID();
            }
            sql = "Insert Into " + paidMap.table + " (" + paidMap.MNC_FN_TYP_CD + "," + paidMap.MNC_PH_CD + "," + paidMap.active + "," + paidMap.status_paid + ") " +
                "Values ('"  + p.MNC_FN_TYP_CD + "','" + p.MNC_PH_CD + "','1','" + p.status_paid + "')";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            return re;
        }
        public String update(PaidMap p)
        {
            String sql = "", re = "";
            sql = "Update " + paidMap.table + " Set " + paidMap.MNC_FN_TYP_CD + "='" + p.MNC_FN_TYP_CD + "'," +
                paidMap.MNC_PH_CD + "='" + p.MNC_PH_CD + "'," +
                paidMap.active + "='" + p.active + "', " +
                paidMap.status_paid + "='" + p.status_paid + "' " +
                "Where " + paidMap.pkField + "='" + p.paid_map_id + "'";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            return re;
        }
        public String Delete(String drugcode)
        {
            String sql = "", re = "";
            sql = "Delete From " + paidMap.table + "  " +
                "Where " + paidMap.MNC_PH_CD + "='" + drugcode + "'";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            return re;
        }
        public String insertPaidMap(PaidMap p)
        {
            String sql = "", re = "";
            Delete(p.MNC_PH_CD);
            
            if (p.MNC_PH_CD.Length > 0 && p.map1.Length > 0) { p.MNC_FN_TYP_CD = "1"; p.status_paid = p.map1; re = insert(p); }
            if (p.MNC_PH_CD.Length > 0 && p.map2.Length > 0) { p.MNC_FN_TYP_CD = "2"; p.status_paid = p.map2; re = insert(p); }
            if (p.MNC_PH_CD.Length > 0 && p.map3.Length > 0) { p.MNC_FN_TYP_CD = "3"; p.status_paid = p.map3; re = insert(p); }
            if (p.MNC_PH_CD.Length > 0 && p.map4.Length > 0) { p.MNC_FN_TYP_CD = "4"; p.status_paid = p.map4; re = insert(p); }
            
            
            return re;
        }
    }
}
