using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class SSOPDispenseditemDB
    {
        public ConnectDB conn;
        SSOPDispenseditem sSOPDispenseditem;

        public SSOPDispenseditemDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }

        private void initConfig()
        {
            sSOPDispenseditem = new SSOPDispenseditem();
            sSOPDispenseditem.dispenseditem_id = "dispenseditem_id";
            sSOPDispenseditem.dispid = "dispid";
            sSOPDispenseditem.prdcat = "prdcat";
            sSOPDispenseditem.hospdrgid = "hospdrgid";
            sSOPDispenseditem.drgid = "drgid";
            sSOPDispenseditem.dfscode = "dfscode";
            sSOPDispenseditem.dfstext = "dfstext";
            sSOPDispenseditem.packsize = "packsize";
            sSOPDispenseditem.sigcode = "sigcode";
            sSOPDispenseditem.sigtext = "sigtext";
            sSOPDispenseditem.quantity = "quantity";
            sSOPDispenseditem.unitprice = "unitprice";
            sSOPDispenseditem.chargeamt = "chargeamt";
            sSOPDispenseditem.reimbprice = "reimbprice";
            sSOPDispenseditem.reimbamt = "reimbamt";
            sSOPDispenseditem.prdsecode = "prdsecode";
            sSOPDispenseditem.claimcont = "claimcont";
            sSOPDispenseditem.claimcat = "claimcat";
            sSOPDispenseditem.multidisp = "multidisp";
            sSOPDispenseditem.supplyfor = "supplyfor";
            sSOPDispenseditem.active = "active";
            sSOPDispenseditem.billtrans_id = "billtrans_id";

            sSOPDispenseditem.pkField = "dispenseditem_id";
            sSOPDispenseditem.table = "ssop_t_dispenseditem";
        }

        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM " + sSOPDispenseditem.table;
                dt = conn.selectData(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in selectAll: " + ex.Message);
            }
            return dt;
        }
        public String selectMax()
        {
            String re = "";
            DataTable dt = new DataTable();
            try
            {
                // SQL query to select all data from the SSOPBillTran table
                String sql = "SELECT max(dispenseditem_id) as dispenseditem_id FROM " + sSOPDispenseditem.table + " ";
                dt = conn.selectData(conn.connSsnData, sql);
                re = dt.Rows[0]["dispenseditem_id"].ToString();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine("Error in selectAll: " + ex.Message);
            }
            return re;
        }
        public SSOPDispenseditem selectByPk(string id)
        {
            SSOPDispenseditem sSOPDispenseditem1 = new SSOPDispenseditem();
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM " + sSOPDispenseditem.table + " WHERE " + sSOPDispenseditem.dispenseditem_id + " = '" + id + "'";
                dt = conn.selectData(conn.connSsnData, sql);                
                if (dt.Rows.Count > 0)  {   sSOPDispenseditem1 = setData(dt.Rows[0]);                }
                else    {                    sSOPDispenseditem1 = setData();                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in selectByDispenseditemId: " + ex.Message);
            }
            return sSOPDispenseditem1;
        }
        public DataTable selectByBilltransId(string billtrans_id)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM " + sSOPDispenseditem.table + " WHERE " + sSOPDispenseditem.billtrans_id + " = '" + billtrans_id + "'";
                dt = conn.selectData(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in selectByDispenseditemId: " + ex.Message);
            }
            return dt;
        }
        public List<SSOPDispenseditem> selectByBTransId(string billtrans_id)
        {
            List<SSOPDispenseditem> list = new List<SSOPDispenseditem>();
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM " + sSOPDispenseditem.table + " WHERE " + sSOPDispenseditem.billtrans_id + " = '" + billtrans_id + "'";
                dt = conn.selectData(conn.connSsnData, sql);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(setData(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in selectByBTransId: " + ex.Message);
            }
            return list;
        }
        public SSOPDispenseditem selectByDispenseditemId(string dispenseditem_id)
        {
            SSOPDispenseditem sSOPDispenseditem1 = new SSOPDispenseditem();
            try
            {
                string sql = "SELECT * FROM " + sSOPDispenseditem.table + " WHERE " + sSOPDispenseditem.dispenseditem_id + " = '" + dispenseditem_id + "'";
                DataTable dt = conn.selectData(conn.connSsnData, sql);
                if (dt.Rows.Count > 0)
                {
                    sSOPDispenseditem1 = setData(dt.Rows[0]);
                }
                else
                {
                    sSOPDispenseditem1 = setData();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in selectByDispenseditemId: " + ex.Message);
            }
            return sSOPDispenseditem1;
        }

        public SSOPDispenseditem chknull(SSOPDispenseditem sSOPDispenseditem)
        {
            if (sSOPDispenseditem == null) sSOPDispenseditem = new SSOPDispenseditem();

            sSOPDispenseditem.dispenseditem_id = sSOPDispenseditem.dispenseditem_id ?? "";
            sSOPDispenseditem.dispid = sSOPDispenseditem.dispid ?? "";
            sSOPDispenseditem.prdcat = sSOPDispenseditem.prdcat ?? "";
            sSOPDispenseditem.hospdrgid = sSOPDispenseditem.hospdrgid ?? "";
            sSOPDispenseditem.drgid = sSOPDispenseditem.drgid ?? "";
            sSOPDispenseditem.dfscode = sSOPDispenseditem.dfscode ?? "";
            sSOPDispenseditem.dfstext = sSOPDispenseditem.dfstext ?? "";
            sSOPDispenseditem.packsize = sSOPDispenseditem.packsize ?? "";
            sSOPDispenseditem.sigcode = sSOPDispenseditem.sigcode ?? "";
            sSOPDispenseditem.sigtext = sSOPDispenseditem.sigtext ?? "";
            sSOPDispenseditem.quantity = sSOPDispenseditem.quantity ?? "";
            sSOPDispenseditem.unitprice = sSOPDispenseditem.unitprice ?? "";
            sSOPDispenseditem.chargeamt = sSOPDispenseditem.chargeamt ?? "";
            sSOPDispenseditem.reimbprice = sSOPDispenseditem.reimbprice ?? "";
            sSOPDispenseditem.reimbamt = sSOPDispenseditem.reimbamt ?? "";
            sSOPDispenseditem.prdsecode = sSOPDispenseditem.prdsecode ?? "";
            sSOPDispenseditem.claimcont = sSOPDispenseditem.claimcont ?? "";
            sSOPDispenseditem.claimcat = sSOPDispenseditem.claimcat ?? "";
            sSOPDispenseditem.multidisp = sSOPDispenseditem.multidisp ?? "";
            sSOPDispenseditem.supplyfor = sSOPDispenseditem.supplyfor ?? "";
            sSOPDispenseditem.billtrans_id = sSOPDispenseditem.billtrans_id ?? "";

            return sSOPDispenseditem;
        }
        public string insertData(SSOPDispenseditem p)
        {
            string result = "";
            try
            {
                // Ensure no null values in the object
                p = chknull(p);

                if (!string.IsNullOrEmpty(p.dispenseditem_id))
                {
                    // If billtran_id has a value, call the update method
                    result = update(p);
                }
                else
                {
                    // If billtran_id is empty, call the insert method
                    result = insert(p);
                    //result = selectMax();
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine("Error in insertData: " + ex.Message);
            }
            return result;
        }
        public string insert(SSOPDispenseditem p)
        {
            string result = "";
            try
            {
                //p = chknull(p);
                string sql = "INSERT INTO " + sSOPDispenseditem.table + " (" +
                    sSOPDispenseditem.dispid + ", " +                    sSOPDispenseditem.prdcat + ", " +
                    sSOPDispenseditem.hospdrgid + ", " +                    sSOPDispenseditem.drgid + ", " +                    sSOPDispenseditem.dfscode + ", " +
                    sSOPDispenseditem.dfstext + ", " +                    sSOPDispenseditem.packsize + ", " +                    sSOPDispenseditem.sigcode + ", " +
                    sSOPDispenseditem.sigtext + ", " +                    sSOPDispenseditem.quantity + ", " +                    sSOPDispenseditem.unitprice + ", " +
                    sSOPDispenseditem.chargeamt + ", " +                    sSOPDispenseditem.reimbprice + ", " +                    sSOPDispenseditem.reimbamt + ", " +
                    sSOPDispenseditem.prdsecode + ", " +                    sSOPDispenseditem.claimcont + ", " +                    sSOPDispenseditem.claimcat + ", " +
                    sSOPDispenseditem.multidisp + ", " +                    sSOPDispenseditem.supplyfor + ", " + sSOPDispenseditem.billtrans_id +
                    ") " +
                    "VALUES (" +
                    "'"+p.dispid + "', " +                    "'" + p.prdcat + "', " +
                    "'" + p.hospdrgid + "', " +                    "'" + p.drgid + "', " +                    "'" + p.dfscode + "', " +
                    "'" + p.dfstext + "', " +                    "'" + p.packsize + "', " +                    "'" + p.sigcode + "', " +
                    "'" + p.sigtext + "', " +                    "'" + p.quantity + "', " +                    "'" + p.unitprice + "', " +
                    "'" + p.chargeamt + "', " +                    "'" + p.reimbprice + "', " +                    "'" + p.reimbamt + "', " +
                    "'" + p.prdsecode + "', " +                    "'" + p.claimcont + "', " +                    "'" + p.claimcat + "', " +
                    "'" + p.multidisp + "', " +                    "'" + p.supplyfor + "', " + "'" + p.billtrans_id +
                    "')";
                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in insert: " + ex.Message);
            }
            return result;
        }

        public string update(SSOPDispenseditem p)
        {
            string result = "";
            try
            {
                //p = chknull(p);
                string sql = "UPDATE " + sSOPDispenseditem.table + " SET " +
                    sSOPDispenseditem.dispid + " = '" + p.dispid + "', " +
                    sSOPDispenseditem.prdcat + " = '" + p.prdcat + "', " +
                    sSOPDispenseditem.hospdrgid + " = '" + p.hospdrgid + "', " +
                    sSOPDispenseditem.drgid + " = '" + p.drgid + "', " +
                    sSOPDispenseditem.dfscode + " = '" + p.dfscode + "', " +
                    sSOPDispenseditem.dfstext + " = '" + p.dfstext + "', " +
                    sSOPDispenseditem.packsize + " = '" + p.packsize + "', " +
                    sSOPDispenseditem.sigcode + " = '" + p.sigcode + "', " +
                    sSOPDispenseditem.sigtext + " = '" + p.sigtext + "', " +
                    sSOPDispenseditem.quantity + " = '" + p.quantity + "', " +
                    sSOPDispenseditem.unitprice + " = '" + p.unitprice + "', " +
                    sSOPDispenseditem.chargeamt + " = '" + p.chargeamt + "', " +
                    sSOPDispenseditem.reimbprice + " = '" + p.reimbprice + "', " +
                    sSOPDispenseditem.reimbamt + " = '" + p.reimbamt + "', " +
                    sSOPDispenseditem.prdsecode + " = '" + p.prdsecode + "', " +
                    sSOPDispenseditem.claimcont + " = '" + p.claimcont + "', " +
                    sSOPDispenseditem.claimcat + " = '" + p.claimcat + "', " +
                    sSOPDispenseditem.multidisp + " = '" + p.multidisp + "', " +
                    sSOPDispenseditem.supplyfor + " = '" + p.supplyfor + "' " +
                    "WHERE " + sSOPDispenseditem.dispenseditem_id + " = '" + p.dispenseditem_id + "'";
                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in update: " + ex.Message);
            }
            return result;
        }

        public SSOPDispenseditem setData(DataRow row)
        {
            SSOPDispenseditem p = new SSOPDispenseditem();
            p.dispenseditem_id = row[sSOPDispenseditem.dispenseditem_id]?.ToString() ?? "";
            p.dispid = row[sSOPDispenseditem.dispid]?.ToString() ?? "";
            p.prdcat = row[sSOPDispenseditem.prdcat]?.ToString() ?? "";
            p.hospdrgid = row[sSOPDispenseditem.hospdrgid]?.ToString() ?? "";
            p.drgid = row[sSOPDispenseditem.drgid]?.ToString() ?? "";
            p.dfscode = row[sSOPDispenseditem.dfscode]?.ToString() ?? "";
            p.dfstext = row[sSOPDispenseditem.dfstext]?.ToString() ?? "";
            p.packsize = row[sSOPDispenseditem.packsize]?.ToString() ?? "";
            p.sigcode = row[sSOPDispenseditem.sigcode]?.ToString() ?? "";
            p.sigtext = row[sSOPDispenseditem.sigtext]?.ToString() ?? "";
            p.quantity = row[sSOPDispenseditem.quantity]?.ToString() ?? "";
            p.unitprice = row[sSOPDispenseditem.unitprice]?.ToString() ?? "";
            p.chargeamt = row[sSOPDispenseditem.chargeamt]?.ToString() ?? "";
            p.reimbprice = row[sSOPDispenseditem.reimbprice]?.ToString() ?? "";
            p.reimbamt = row[sSOPDispenseditem.reimbamt]?.ToString() ?? "";
            p.prdsecode = row[sSOPDispenseditem.prdsecode]?.ToString() ?? "";
            p.claimcont = row[sSOPDispenseditem.claimcont]?.ToString() ?? "";
            p.claimcat = row[sSOPDispenseditem.claimcat]?.ToString() ?? "";
            p.multidisp = row[sSOPDispenseditem.multidisp]?.ToString() ?? "";
            p.supplyfor = row[sSOPDispenseditem.supplyfor]?.ToString() ?? "";
            p.billtrans_id = row[sSOPDispenseditem.billtrans_id]?.ToString() ?? "";
            return p;
        }

        public SSOPDispenseditem setData()
        {
            SSOPDispenseditem sSOPDispenseditem = new SSOPDispenseditem();
            sSOPDispenseditem.dispenseditem_id = "";
            sSOPDispenseditem.dispid = "";
            sSOPDispenseditem.prdcat = "";
            sSOPDispenseditem.hospdrgid = "";
            sSOPDispenseditem.drgid = "";
            sSOPDispenseditem.dfscode = "";
            sSOPDispenseditem.dfstext = "";
            sSOPDispenseditem.packsize = "";
            sSOPDispenseditem.sigcode = "";
            sSOPDispenseditem.sigtext = "";
            sSOPDispenseditem.quantity = "";
            sSOPDispenseditem.unitprice = "";
            sSOPDispenseditem.chargeamt = "";
            sSOPDispenseditem.reimbprice = "";
            sSOPDispenseditem.reimbamt = "";
            sSOPDispenseditem.prdsecode = "";
            sSOPDispenseditem.claimcont = "";
            sSOPDispenseditem.claimcat = "";
            sSOPDispenseditem.multidisp = "";
            sSOPDispenseditem.supplyfor = "";
            sSOPDispenseditem.billtrans_id = "";
            return sSOPDispenseditem;
        }
    }
}
