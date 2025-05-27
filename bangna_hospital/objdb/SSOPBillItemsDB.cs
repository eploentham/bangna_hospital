using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class SSOPBillItemsDB
    {
        public ConnectDB conn;
        SSOPBillItems sSOPBillItem;

        public SSOPBillItemsDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            sSOPBillItem = new SSOPBillItems();
            sSOPBillItem.billitems_id = "billitems_id";
            sSOPBillItem.billtrans_id = "billtrans_id";
            sSOPBillItem.invno = "invno";
            sSOPBillItem.svdate = "svdate";
            sSOPBillItem.billmuad = "billmuad";
            sSOPBillItem.lccode = "lccode";
            sSOPBillItem.stdcode = "stdcode";
            sSOPBillItem.desc1 = "desc1";
            sSOPBillItem.qty = "qty";
            sSOPBillItem.up1 = "up1";
            sSOPBillItem.chargeamt = "chargeamt";
            sSOPBillItem.claimup = "claimup";
            sSOPBillItem.claimamount = "claimamount";
            sSOPBillItem.svrefid = "svrefid";
            sSOPBillItem.claimcat = "claimcat";
            sSOPBillItem.active = "active";
            sSOPBillItem.pkField = "billitems_id";
            sSOPBillItem.table = "ssop_t_billitems";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM " + sSOPBillItem.table;
                dt = conn.selectData(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in selectAll: " + ex.Message);
            }
            return dt;
        }
        public SSOPBillItems selectByPk(string id)
        {
            DataTable dt = new DataTable();
            SSOPBillItems item = new SSOPBillItems();
            try
            {
                string sql = "SELECT * FROM " + sSOPBillItem.table + " WHERE " + sSOPBillItem.pkField + " = '" + id + "'";
                dt = conn.selectData(conn.connSsnData, sql);
                if (dt.Rows.Count > 0){                 item = setData(dt.Rows[0]);     }
                else                {                   item = setData();               }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in selectByBilltransId: " + ex.Message);
            }
            return item;
        }
        public DataTable selectByBilltransId(string billtrans_id)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM " + sSOPBillItem.table + " WHERE " + sSOPBillItem.billtrans_id + " = '" + billtrans_id + "'";
                dt = conn.selectData(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in selectByBilltransId: " + ex.Message);
            }
            return dt;
        }
        public List<SSOPBillItems> selectByBTransId(string billtrans_id)
        {
            List<SSOPBillItems> items = new List<SSOPBillItems>();
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM " + sSOPBillItem.table + " WHERE " + sSOPBillItem.billtrans_id + " = '" + billtrans_id + "'";
                dt = conn.selectData(conn.connSsnData, sql);
                foreach (DataRow row in dt.Rows)
                {
                    SSOPBillItems item = new SSOPBillItems();
                    item = setData(row);
                    items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in selectByBilltransId: " + ex.Message);
            }
            return items;
        }
        public string voidBillItem(String id)
        {
            string result = "";
            try
            {
                string sql = "UPDATE " + sSOPBillItem.table + " SET " +
                             sSOPBillItem.active + " = '3' " +                             
                             "WHERE " + sSOPBillItem.pkField + " = '" + id + "'";
                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in update: " + ex.Message);
            }
            return result;
        }
        public SSOPBillItems chknull(SSOPBillItems p)
        {
            if (p == null) p = new SSOPBillItems();

            p.billtrans_id = p.billtrans_id ?? "";
            p.invno = p.invno ?? "";
            p.svdate = p.svdate ?? "";
            p.billmuad = p.billmuad ?? "";
            p.lccode = p.lccode ?? "";
            p.stdcode = p.stdcode ?? "";
            p.desc1 = p.desc1 ?? "";
            p.qty = p.qty ?? "";
            p.up1 = p.up1 ?? "";
            p.chargeamt = p.chargeamt ?? "";
            p.claimup = p.claimup ?? "";
            p.claimamount = p.claimamount ?? "";
            p.svrefid = p.svrefid ?? "";
            p.claimcat = p.claimcat ?? "";
            p.billitems_id = p.billitems_id ?? "";

            return p;
        }
        public string insertData(SSOPBillItems p)
        {
            string result = "";
            try
            {
                // Ensure no null values in the object
                p = chknull(p);

                if (!string.IsNullOrEmpty(p.billitems_id))
                {
                    // If billtran_id has a value, call the update method
                    result = update(p);
                }
                else
                {
                    // If billtran_id is empty, call the insert method
                    result = insert(p);
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine("Error in insertData: " + ex.Message);
            }
            return result;
        }
        public string insert(SSOPBillItems p)
        {
            string result = "";
            try
            {
                p = chknull(p);
                string sql = "INSERT INTO " + sSOPBillItem.table + " (" +
                    sSOPBillItem.billtrans_id + ", " +                         sSOPBillItem.invno + ", " +                         sSOPBillItem.svdate + ", " +
                    sSOPBillItem.billmuad + ", " +                             sSOPBillItem.lccode + ", " +                        sSOPBillItem.stdcode + ", " +
                    sSOPBillItem.desc1 + ", " +                                sSOPBillItem.qty + ", " +                           sSOPBillItem.up1 + ", " +
                    sSOPBillItem.chargeamt + ", " +                            sSOPBillItem.claimup + ", " +                       sSOPBillItem.claimamount + ", " +
                    sSOPBillItem.svrefid + ", " +                              sSOPBillItem.claimcat + ", " + sSOPBillItem.active + "  " +
                    ") " +
                    "VALUES (" +
                    "'" + p.billtrans_id + "', " +                             "'" + p.invno + "', " +                             "'" + p.svdate + "', " +
                    "'" + p.billmuad + "', " +                             "'" + p.lccode + "', " +                             "'" + p.stdcode + "', " +
                    "'" + p.desc1 + "', " +                             "'" + p.qty + "', " +                             "'" + p.up1 + "', " +
                    "'" + p.chargeamt + "', " +                             "'" + p.claimup + "', " +                             "'" + p.claimamount + "', " +
                    "'" + p.svrefid + "', " +                             "'" + p.claimcat + "','1')";
                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in insert: " + ex.Message);
            }
            return result;
        }
        public string update(SSOPBillItems sSOPBillItems)
        {
            string result = "";
            try
            {
                sSOPBillItems = chknull(sSOPBillItems);
                string sql = "UPDATE " + sSOPBillItem.table + " SET " +
                             sSOPBillItem.invno + " = '" + sSOPBillItems.invno + "', " +
                             sSOPBillItem.svdate + " = '" + sSOPBillItems.svdate + "', " +
                             sSOPBillItem.billmuad + " = '" + sSOPBillItems.billmuad + "', " +
                             sSOPBillItem.lccode + " = '" + sSOPBillItems.lccode + "', " +
                             sSOPBillItem.stdcode + " = '" + sSOPBillItems.stdcode + "', " +
                             sSOPBillItem.desc1 + " = '" + sSOPBillItems.desc1 + "', " +
                             sSOPBillItem.qty + " = '" + sSOPBillItems.qty + "', " +
                             sSOPBillItem.up1 + " = '" + sSOPBillItems.up1 + "', " +
                             sSOPBillItem.chargeamt + " = '" + sSOPBillItems.chargeamt + "', " +
                             sSOPBillItem.claimup + " = '" + sSOPBillItems.claimup + "', " +
                             sSOPBillItem.claimamount + " = '" + sSOPBillItems.claimamount + "', " +
                             sSOPBillItem.svrefid + " = '" + sSOPBillItems.svrefid + "', " +
                             sSOPBillItem.claimcat + " = '" + sSOPBillItems.claimcat + "' " +
                             "WHERE " + sSOPBillItem.billitems_id + " = '" + sSOPBillItems.billitems_id + "'";
                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in update: " + ex.Message);
            }
            return result;
        }
        public SSOPBillItems setData(DataRow row)
        {
            SSOPBillItems item = new SSOPBillItems();
            item.billtrans_id = row[sSOPBillItem.billtrans_id]?.ToString() ?? "";
            item.invno = row[sSOPBillItem.invno]?.ToString() ?? "";
            item.svdate = row[sSOPBillItem.svdate]?.ToString() ?? "";
            item.billmuad = row[sSOPBillItem.billmuad]?.ToString() ?? "";
            item.lccode = row[sSOPBillItem.lccode]?.ToString() ?? "";
            item.stdcode = row[sSOPBillItem.stdcode]?.ToString() ?? "";
            item.desc1 = row[sSOPBillItem.desc1]?.ToString() ?? "";
            item.qty = row[sSOPBillItem.qty]?.ToString() ?? "";
            item.up1 = row[sSOPBillItem.up1]?.ToString() ?? "";
            item.chargeamt = row[sSOPBillItem.chargeamt]?.ToString() ?? "";
            item.claimup = row[sSOPBillItem.claimup]?.ToString() ?? "";
            item.claimamount = row[sSOPBillItem.claimamount]?.ToString() ?? "";
            item.svrefid = row[sSOPBillItem.svrefid]?.ToString() ?? "";
            item.claimcat = row[sSOPBillItem.claimcat]?.ToString() ?? "";
            item.billitems_id = row[sSOPBillItem.billitems_id]?.ToString() ?? "";
            return item;
        }
        public SSOPBillItems setData()
        {
            SSOPBillItems sSOPBillItems = new SSOPBillItems();
            sSOPBillItems.billtrans_id = "";
            sSOPBillItems.invno = "";
            sSOPBillItems.svdate = "";
            sSOPBillItems.billmuad = "";
            sSOPBillItems.lccode = "";
            sSOPBillItems.stdcode = "";
            sSOPBillItems.desc1 = "";
            sSOPBillItems.qty = "";
            sSOPBillItems.up1 = "";
            sSOPBillItems.chargeamt = "";
            sSOPBillItems.claimup = "";
            sSOPBillItems.claimamount = "";
            sSOPBillItems.svrefid = "";
            sSOPBillItems.claimcat = "";
            sSOPBillItems.billitems_id = "";
            return sSOPBillItems;
        }
    }
}
