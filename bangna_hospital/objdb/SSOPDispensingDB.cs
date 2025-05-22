using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class SSOPDispensingDB
    {
        public ConnectDB conn;
        SSOPDispensing sSOPDispensing;

        public SSOPDispensingDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }

        private void initConfig()
        {
            sSOPDispensing = new SSOPDispensing();
            sSOPDispensing.billdisp_id = "billdisp_id";
            sSOPDispensing.providerid = "providerid";
            sSOPDispensing.dispid = "dispid";
            sSOPDispensing.invno = "invno";
            sSOPDispensing.hn = "hn";
            sSOPDispensing.pid = "pid";
            sSOPDispensing.prescdt = "prescdt";
            sSOPDispensing.dispdt = "dispdt";
            sSOPDispensing.prescb = "prescb";
            sSOPDispensing.itemcnt = "itemcnt";
            sSOPDispensing.chargeamt = "chargeamt";
            sSOPDispensing.claimamt = "claimamt";
            sSOPDispensing.paid = "paid";
            sSOPDispensing.otherpay = "otherpay";
            sSOPDispensing.reimburser = "reimburser";
            sSOPDispensing.benefitplan = "benefitplan";
            sSOPDispensing.dispestat = "dispestat";
            sSOPDispensing.svid = "svid";
            sSOPDispensing.daycover = "daycover";
            sSOPDispensing.active = "active";
            sSOPDispensing.billtrans_id = "billtrans_id";

            sSOPDispensing.pkField = "billdisp_id";
            sSOPDispensing.table = "ssop_t_dispensing";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM " + sSOPDispensing.table + " WHERE active = '1'";
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
            SSOPDispensing sSOPDispensing1 = new SSOPDispensing();
            string sql = "SELECT * FROM " + sSOPDispensing.table + " WHERE " + sSOPDispensing.billtrans_id + " = '" + billtrans_id + "'";
            DataTable dt = conn.selectData(conn.connSsnData,sql);
            
            return dt;
        }
        public List<SSOPDispensing> selectByBTransId(string billtrans_id)
        {
            List<SSOPDispensing> ssopDispensings = new List<SSOPDispensing>();
            string sql = "SELECT * FROM " + sSOPDispensing.table + " WHERE " + sSOPDispensing.billtrans_id + " = '" + billtrans_id + "'";
            DataTable dt = conn.selectData(conn.connSsnData, sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ssopDispensings.Add(setData(row));
                }
            }
            return ssopDispensings;
        }
        public SSOPDispensing selectByPk(string id)
        {
            SSOPDispensing sSOPDispensing1 = new SSOPDispensing();
            string sql = "SELECT * FROM " + sSOPDispensing.table + " WHERE " + sSOPDispensing.pkField + " = '" + id + "'";
            DataTable dt = conn.selectData(conn.connSsnData, sql);
            if (dt.Rows.Count > 0)
            {
                sSOPDispensing1 = setData(dt.Rows[0]);
            }
            else
            {
                sSOPDispensing1 = setData();
            }
            return sSOPDispensing1;
        }

        public SSOPDispensing chknull(SSOPDispensing sSOPDispensing)
        {
            if (sSOPDispensing == null) sSOPDispensing = new SSOPDispensing();

            sSOPDispensing.billdisp_id = sSOPDispensing.billdisp_id ?? "";
            sSOPDispensing.providerid = sSOPDispensing.providerid ?? "";
            sSOPDispensing.dispid = sSOPDispensing.dispid ?? "";
            sSOPDispensing.invno = sSOPDispensing.invno ?? "";
            sSOPDispensing.hn = sSOPDispensing.hn ?? "";
            sSOPDispensing.pid = sSOPDispensing.pid ?? "";
            sSOPDispensing.prescdt = sSOPDispensing.prescdt ?? "";
            sSOPDispensing.dispdt = sSOPDispensing.dispdt ?? "";
            sSOPDispensing.prescb = sSOPDispensing.prescb ?? "";
            sSOPDispensing.itemcnt = sSOPDispensing.itemcnt ?? "";
            sSOPDispensing.chargeamt = sSOPDispensing.chargeamt ?? "";
            sSOPDispensing.claimamt = sSOPDispensing.claimamt ?? "";
            sSOPDispensing.paid = sSOPDispensing.paid ?? "";
            sSOPDispensing.otherpay = sSOPDispensing.otherpay ?? "";
            sSOPDispensing.reimburser = sSOPDispensing.reimburser ?? "";
            sSOPDispensing.benefitplan = sSOPDispensing.benefitplan ?? "";
            sSOPDispensing.dispestat = sSOPDispensing.dispestat ?? "";
            sSOPDispensing.svid = sSOPDispensing.svid ?? "";
            sSOPDispensing.daycover = sSOPDispensing.daycover ?? "";

            return sSOPDispensing;
        }

        public string insertData(SSOPDispensing p)
        {
            string result = "";
            try
            {
                p = chknull(p);

                if (!string.IsNullOrEmpty(p.billdisp_id))
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
        public string insert(SSOPDispensing p)
        {
            string result = "";
            try
            {
                //p = chknull(p);
                string sql = "INSERT INTO " + sSOPDispensing.table + " (" +
                    sSOPDispensing.providerid + ", " + sSOPDispensing.dispid + ", " + sSOPDispensing.invno + ", " +
                    sSOPDispensing.hn + ", " + sSOPDispensing.pid + ", " + sSOPDispensing.prescdt + ", " +
                    sSOPDispensing.dispdt + ", " + sSOPDispensing.prescb + ", " + sSOPDispensing.itemcnt + ", " +
                    sSOPDispensing.chargeamt + ", " + sSOPDispensing.claimamt + ", " + sSOPDispensing.paid + ", " +
                    sSOPDispensing.otherpay + ", " + sSOPDispensing.reimburser + ", " + sSOPDispensing.benefitplan + ", " +
                    sSOPDispensing.dispestat + ", " + sSOPDispensing.svid + ", " + sSOPDispensing.daycover + ", " +
                    sSOPDispensing.billtrans_id + " "
                    + ") " +
                    "VALUES (" +
                    "'" + p.providerid + "', '" + p.dispid + "', '" + p.invno + "', " +
                    "'" + p.hn + "', '" + p.pid + "', '" + p.prescdt + "', " +
                    "'" + p.dispdt + "', '" + p.prescb + "', '" + p.itemcnt + "', " +
                    "'" + p.chargeamt + "', '" + p.claimamt + "', '" + p.paid + "', " +
                    "'" + p.otherpay + "', '" + p.reimburser + "', '" + p.benefitplan + "', " +
                    "'" + p.dispestat + "', '" + p.svid + "', '" + p.daycover +
                    "', '" + p.billtrans_id + "' " +
                    ")";

                result = conn.ExecuteNonQuery(conn.connSsnData,sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in insert: " + ex.Message);
            }
            return result;
        }

        public string update(SSOPDispensing p)
        {
            string result = "";
            try
            {
                //sSOPDispensing = chknull(sSOPDispensing);
                string sql = "UPDATE " + sSOPDispensing.table + " SET " +
                    sSOPDispensing.providerid + " = '" + p.providerid + "', " +
                    sSOPDispensing.dispid + " = '" + p.dispid + "', " +
                    sSOPDispensing.invno + " = '" + p.invno + "', " +
                    sSOPDispensing.hn + " = '" + p.hn + "', " +
                    sSOPDispensing.pid + " = '" + p.pid + "', " +
                    sSOPDispensing.prescdt + " = '" + p.prescdt + "', " +
                    sSOPDispensing.dispdt + " = '" + p.dispdt + "', " +
                    sSOPDispensing.prescb + " = '" + p.prescb + "', " +
                    sSOPDispensing.itemcnt + " = '" + p.itemcnt + "', " +
                    sSOPDispensing.chargeamt + " = '" + p.chargeamt + "', " +
                    sSOPDispensing.claimamt + " = '" + p.claimamt + "', " +
                    sSOPDispensing.paid + " = '" + p.paid + "', " +
                    sSOPDispensing.otherpay + " = '" + p.otherpay + "', " +
                    sSOPDispensing.reimburser + " = '" + p.reimburser + "', " +
                    sSOPDispensing.benefitplan + " = '" + p.benefitplan + "', " +
                    sSOPDispensing.dispestat + " = '" + p.dispestat + "', " +
                    sSOPDispensing.svid + " = '" + p.svid + "', " +
                    sSOPDispensing.daycover + " = '" + p.daycover + "' " +
                    "WHERE " + sSOPDispensing.billdisp_id + " = '" + p.billdisp_id + "'";

                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in update: " + ex.Message);
            }
            return result;
        }

        public SSOPDispensing setData(DataRow row)
        {
            SSOPDispensing p = new SSOPDispensing();
            p.billdisp_id = row[sSOPDispensing.billdisp_id]?.ToString() ?? "";
            p.providerid = row[sSOPDispensing.providerid]?.ToString() ?? "";
            p.dispid = row[sSOPDispensing.dispid]?.ToString() ?? "";
            p.invno = row[sSOPDispensing.invno]?.ToString() ?? "";
            p.hn = row[sSOPDispensing.hn]?.ToString() ?? "";
            p.pid = row[sSOPDispensing.pid]?.ToString() ?? "";
            p.prescdt = row[sSOPDispensing.prescdt]?.ToString() ?? "";
            p.dispdt = row[sSOPDispensing.dispdt]?.ToString() ?? "";
            p.prescb = row[sSOPDispensing.prescb]?.ToString() ?? "";
            p.itemcnt = row[sSOPDispensing.itemcnt]?.ToString() ?? "";
            p.chargeamt = row[sSOPDispensing.chargeamt]?.ToString() ?? "";
            p.claimamt = row[sSOPDispensing.claimamt]?.ToString() ?? "";
            p.paid = row[sSOPDispensing.paid]?.ToString() ?? "";
            p.otherpay = row[sSOPDispensing.otherpay]?.ToString() ?? "";
            p.reimburser = row[sSOPDispensing.reimburser]?.ToString() ?? "";
            p.benefitplan = row[sSOPDispensing.benefitplan]?.ToString() ?? "";
            p.dispestat = row[sSOPDispensing.dispestat]?.ToString() ?? "";
            p.svid = row[sSOPDispensing.svid]?.ToString() ?? "";
            p.daycover = row[sSOPDispensing.daycover]?.ToString() ?? "";
            p.billtrans_id = row[sSOPDispensing.billtrans_id]?.ToString() ?? "";
            return p;
        }

        public SSOPDispensing setData()
        {
            SSOPDispensing sSOPDispensing = new SSOPDispensing();
            sSOPDispensing.billdisp_id = "";
            sSOPDispensing.providerid = "";
            sSOPDispensing.dispid = "";
            sSOPDispensing.invno = "";
            sSOPDispensing.hn = "";
            sSOPDispensing.pid = "";
            sSOPDispensing.prescdt = "";
            sSOPDispensing.dispdt = "";
            sSOPDispensing.prescb = "";
            sSOPDispensing.itemcnt = "";
            sSOPDispensing.chargeamt = "";
            sSOPDispensing.claimamt = "";
            sSOPDispensing.paid = "";
            sSOPDispensing.otherpay = "";
            sSOPDispensing.reimburser = "";
            sSOPDispensing.benefitplan = "";
            sSOPDispensing.dispestat = "";
            sSOPDispensing.svid = "";
            sSOPDispensing.daycover = "";
            sSOPDispensing.billtrans_id = "";
            return sSOPDispensing;
        }
    }
}
