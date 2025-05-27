using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class SSOPOpdxDB
    {
        public ConnectDB conn;
        SSOPOpdx sSOPOpdx;

        public SSOPOpdxDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }

        private void initConfig()
        {
            sSOPOpdx = new SSOPOpdx();
            sSOPOpdx.opdx_id = "opdx_id";
            sSOPOpdx.class1 = "class1";
            sSOPOpdx.svid = "svid";
            sSOPOpdx.sl = "sl";
            sSOPOpdx.codeset = "codeset";
            sSOPOpdx.code = "code";
            sSOPOpdx.desc1 = "desc1";
            sSOPOpdx.active = "active";
            sSOPOpdx.billtrans_id = "billtrans_id";

            sSOPOpdx.pkField = "opdx_id";
            sSOPOpdx.table = "ssop_t_opdx";
        }

        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM " + sSOPOpdx.table + " WHERE active = '1'";
                dt = conn.selectData(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in selectAll: " + ex.Message);
            }
            return dt;
        }
        public DataTable selectByBilltransId(string billtrans_id)
        {
            string sql = "SELECT * FROM " + sSOPOpdx.table + " WHERE " + sSOPOpdx.billtrans_id + " = '" + billtrans_id + "'";
            DataTable dt = conn.selectData(conn.connSsnData, sql);
            
            return dt;
        }
        public SSOPOpdx selectByPk(string id)
        {
            SSOPOpdx sSOPOpdx1 = new SSOPOpdx();
            string sql = "SELECT * FROM " + sSOPOpdx.table + " WHERE " + sSOPOpdx.pkField + " = '" + id + "'";
            DataTable dt = conn.selectData(conn.connSsnData, sql);
            if (dt.Rows.Count > 0)  {             sSOPOpdx1 = setData(dt.Rows[0]);            }
            else            {                sSOPOpdx1 = setData();            }
            return sSOPOpdx1;
        }
        public List<SSOPOpdx> selectByBTransId(string billtrans_id)
        {
            List<SSOPOpdx> list = new List<SSOPOpdx>();
            string sql = "SELECT * FROM " + sSOPOpdx.table + " WHERE " + sSOPOpdx.billtrans_id + " = '" + billtrans_id + "'";
            DataTable dt = conn.selectData(conn.connSsnData, sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    SSOPOpdx sSOPOpdx1 = setData(row);
                    list.Add(sSOPOpdx1);
                }
            }
            return list;
        }
        public SSOPOpdx selectByOpdxId(string opdx_id)
        {
            SSOPOpdx sSOPOpdx1 = new SSOPOpdx();
            string sql = "SELECT * FROM " + sSOPOpdx.table + " WHERE " + sSOPOpdx.opdx_id + " = '" + opdx_id + "'";
            DataTable dt = conn.selectData(conn.connSsnData, sql);
            if (dt.Rows.Count > 0)
            {
                sSOPOpdx1 = setData(dt.Rows[0]);
            }
            else
            {
                sSOPOpdx1 = setData();
            }
            return sSOPOpdx1;
        }
        public string voidOPDx(String id)
        {
            string result = "";
            try
            {
                //sSOPOpdx = chknull(sSOPOpdx);
                string sql = "UPDATE " + sSOPOpdx.table + " SET " +
                    sSOPOpdx.active + " = '3' " +
                    "WHERE " + sSOPOpdx.opdx_id + " = '" + id + "'";
                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in update: " + ex.Message);
            }
            return result;
        }
        public SSOPOpdx chknull(SSOPOpdx sSOPOpdx)
        {
            if (sSOPOpdx == null) sSOPOpdx = new SSOPOpdx();

            sSOPOpdx.opdx_id = sSOPOpdx.opdx_id ?? "";
            sSOPOpdx.class1 = sSOPOpdx.class1 ?? "";
            sSOPOpdx.svid = sSOPOpdx.svid ?? "";
            sSOPOpdx.sl = sSOPOpdx.sl ?? "";
            sSOPOpdx.codeset = sSOPOpdx.codeset ?? "";
            sSOPOpdx.code = sSOPOpdx.code ?? "";
            sSOPOpdx.desc1 = sSOPOpdx.desc1 ?? "";
            sSOPOpdx.billtrans_id = sSOPOpdx.billtrans_id ?? "";
            return sSOPOpdx;
        }

        public string insertData(SSOPOpdx p)
        {
            string result = "";
            try
            {
                p = chknull(p);

                if (!string.IsNullOrEmpty(p.opdx_id))
                {
                    result = update(p);
                }
                else
                {
                    result = insert(p);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in insertData: " + ex.Message);
            }
            return result;
        }

        public string insert(SSOPOpdx p)
        {
            string result = "";
            try
            {
                //sSOPOpdx = chknull(sSOPOpdx);
                string sql = "INSERT INTO " + sSOPOpdx.table + " (" +
                    sSOPOpdx.class1 + ", " + sSOPOpdx.svid + ", " + sSOPOpdx.sl + ", " +
                    sSOPOpdx.codeset + ", " + sSOPOpdx.code + ", " + sSOPOpdx.desc1 +","+
                    sSOPOpdx.billtrans_id + " " +
                    ", active) " +
                    "VALUES (" +
                    "'" + p.class1 + "', '" + p.svid + "', '" + p.sl + "', " +
                    "'" + p.codeset + "', '" + p.code + "', '" + p.desc1 +"',"+
                    "'" + p.billtrans_id + "', " +
                    "'1')";

                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in insert: " + ex.Message);
            }
            return result;
        }

        public string update(SSOPOpdx p)
        {
            string result = "";
            try
            {
                //sSOPOpdx = chknull(sSOPOpdx);
                string sql = "UPDATE " + sSOPOpdx.table + " SET " +
                    sSOPOpdx.class1 + " = '" + p.class1 + "', " +
                    sSOPOpdx.svid + " = '" + p.svid + "', " +
                    sSOPOpdx.sl + " = '" + p.sl + "', " +
                    sSOPOpdx.codeset + " = '" + p.codeset + "', " +
                    sSOPOpdx.code + " = '" + p.code + "', " +
                    sSOPOpdx.desc1 + " = '" + p.desc1 + "' " +
                    "WHERE " + sSOPOpdx.opdx_id + " = '" + p.opdx_id + "'";

                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in update: " + ex.Message);
            }
            return result;
        }

        public SSOPOpdx setData(DataRow row)
        {
            SSOPOpdx sSOPOpdx1 = new SSOPOpdx();
            sSOPOpdx1.opdx_id = row[sSOPOpdx.opdx_id]?.ToString() ?? "";
            sSOPOpdx1.class1 = row[sSOPOpdx.class1]?.ToString() ?? "";
            sSOPOpdx1.svid = row[sSOPOpdx.svid]?.ToString() ?? "";
            sSOPOpdx1.sl = row[sSOPOpdx.sl]?.ToString() ?? "";
            sSOPOpdx1.codeset = row[sSOPOpdx.codeset]?.ToString() ?? "";
            sSOPOpdx1.code = row[sSOPOpdx.code]?.ToString() ?? "";
            sSOPOpdx1.desc1 = row[sSOPOpdx.desc1]?.ToString() ?? "";
            sSOPOpdx1.billtrans_id = row[sSOPOpdx.billtrans_id]?.ToString() ?? "";
            return sSOPOpdx1;
        }
        public SSOPOpdx setData()
        {
            SSOPOpdx sSOPOpdx = new SSOPOpdx();
            sSOPOpdx.opdx_id = "";
            sSOPOpdx.class1 = "";
            sSOPOpdx.svid = "";
            sSOPOpdx.sl = "";
            sSOPOpdx.codeset = "";
            sSOPOpdx.code = "";
            sSOPOpdx.desc1 = "";
            sSOPOpdx.billtrans_id = "";
            return sSOPOpdx;
        }
    }
}
