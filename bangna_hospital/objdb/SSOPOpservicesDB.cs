using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class SSOPOpservicesDB
    {
        public ConnectDB conn;
        SSOPOpservices sSOPOpservices;

        public SSOPOpservicesDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }

        private void initConfig()
        {
            sSOPOpservices = new SSOPOpservices();
            sSOPOpservices.opservices_id = "opservices_id";
            sSOPOpservices.invno = "invno";
            sSOPOpservices.svid = "svid";
            sSOPOpservices.class1 = "class1";
            sSOPOpservices.hcode = "hcode";
            sSOPOpservices.hn = "hn";
            sSOPOpservices.pid = "pid";
            sSOPOpservices.careaccount = "careaccount";
            sSOPOpservices.typeserv = "typeserv";
            sSOPOpservices.typein = "typein";
            sSOPOpservices.typeout = "typeout";
            sSOPOpservices.dtappoint = "dtappoint";
            sSOPOpservices.svpid = "svpid";
            sSOPOpservices.clinic = "clinic";
            sSOPOpservices.begdt = "begdt";
            sSOPOpservices.enddt = "enddt";
            sSOPOpservices.lccode = "lccode";
            sSOPOpservices.codeset = "codeset";
            sSOPOpservices.stdcode = "stdcode";
            sSOPOpservices.svcharge = "svcharge";
            sSOPOpservices.completion = "completion";
            sSOPOpservices.svtxcode = "svtxcode";
            sSOPOpservices.claimcat = "claimcat";
            sSOPOpservices.active = "active";
            sSOPOpservices.billtrans_id = "billtrans_id";

            sSOPOpservices.pkField = "opservices_id";
            sSOPOpservices.table = "ssop_t_opservices";
        }

        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM " + sSOPOpservices.table + " WHERE active = '1'";
                dt = conn.selectData(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in selectAll: " + ex.Message);
            }
            return dt;
        }
        public DataTable selectByBilltransId(string billtrans_id)
        {
            string sql = "SELECT * FROM " + sSOPOpservices.table + " WHERE " + sSOPOpservices.billtrans_id + " = '" + billtrans_id + "'";
            DataTable dt = conn.selectData(conn.connSsnData, sql);
            
            return dt;
        }
        public SSOPOpservices selectByPk(string id)
        {
            SSOPOpservices sSOPOpservices1 = new SSOPOpservices();
            string sql = "SELECT * FROM " + sSOPOpservices.table + " WHERE " + sSOPOpservices.pkField + " = '" + id + "'";
            DataTable dt = conn.selectData(conn.connSsnData, sql);
            if (dt.Rows.Count > 0)
            {
                sSOPOpservices1 = setData(dt.Rows[0]);
            }
            else
            {
                sSOPOpservices1 = setData();
            }
            return sSOPOpservices1;
        }
        public List<SSOPOpservices> selectByBTransId(string billtrans_id)
        {
            List<SSOPOpservices> list = new List<SSOPOpservices>();
            string sql = "SELECT * FROM " + sSOPOpservices.table + " WHERE " + sSOPOpservices.billtrans_id + " = '" + billtrans_id + "'";
            DataTable dt = conn.selectData(conn.connSsnData, sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(setData(row));
                }
            }
            return list;
        }
        public SSOPOpservices selectByOpservicesId(string opservices_id)
        {
            SSOPOpservices sSOPOpservices1 = new SSOPOpservices();
            string sql = "SELECT * FROM " + sSOPOpservices.table + " WHERE " + sSOPOpservices.opservices_id + " = '" + opservices_id + "'";
            DataTable dt = conn.selectData(conn.connSsnData, sql);
            if (dt.Rows.Count > 0)
            {
                sSOPOpservices1 = setData(dt.Rows[0]);
            }
            else
            {
                sSOPOpservices1 = setData();
            }
            return sSOPOpservices1;
        }
        public string voidOPService(String id)
        {
            string result = "";
            try
            {
                //sSOPOpservices = chknull(sSOPOpservices);
                string sql = "UPDATE " + sSOPOpservices.table + " SET " +
                    sSOPOpservices.active + " = '3' " +
                    "WHERE " + sSOPOpservices.pkField + " = '" + id + "'";

                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in update: " + ex.Message);
            }
            return result;
        }
        public SSOPOpservices chknull(SSOPOpservices sSOPOpservices)
        {
            if (sSOPOpservices == null) sSOPOpservices = new SSOPOpservices();

            sSOPOpservices.opservices_id = sSOPOpservices.opservices_id ?? "";
            sSOPOpservices.invno = sSOPOpservices.invno ?? "";
            sSOPOpservices.svid = sSOPOpservices.svid ?? "";
            sSOPOpservices.class1 = sSOPOpservices.class1 ?? "";
            sSOPOpservices.hcode = sSOPOpservices.hcode ?? "";
            sSOPOpservices.hn = sSOPOpservices.hn ?? "";
            sSOPOpservices.pid = sSOPOpservices.pid ?? "";
            sSOPOpservices.careaccount = sSOPOpservices.careaccount ?? "";
            sSOPOpservices.typeserv = sSOPOpservices.typeserv ?? "";
            sSOPOpservices.typein = sSOPOpservices.typein ?? "";
            sSOPOpservices.typeout = sSOPOpservices.typeout ?? "";
            sSOPOpservices.dtappoint = sSOPOpservices.dtappoint ?? "";
            sSOPOpservices.svpid = sSOPOpservices.svpid ?? "";
            sSOPOpservices.clinic = sSOPOpservices.clinic ?? "";
            sSOPOpservices.begdt = sSOPOpservices.begdt ?? "";
            sSOPOpservices.enddt = sSOPOpservices.enddt ?? "";
            sSOPOpservices.lccode = sSOPOpservices.lccode ?? "";
            sSOPOpservices.codeset = sSOPOpservices.codeset ?? "";
            sSOPOpservices.stdcode = sSOPOpservices.stdcode ?? "";
            sSOPOpservices.svcharge = sSOPOpservices.svcharge ?? "";
            sSOPOpservices.completion = sSOPOpservices.completion ?? "";
            sSOPOpservices.svtxcode = sSOPOpservices.svtxcode ?? "";
            sSOPOpservices.claimcat = sSOPOpservices.claimcat ?? "";
            sSOPOpservices.billtrans_id = sSOPOpservices.billtrans_id ?? "";

            return sSOPOpservices;
        }

        public string insertData(SSOPOpservices p)
        {
            string result = "";
            try
            {
                p = chknull(p);

                if (!string.IsNullOrEmpty(p.opservices_id))
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

        public string insert(SSOPOpservices p)
        {
            string result = "";
            try
            {
                //sSOPOpservices = chknull(sSOPOpservices);
                string sql = "INSERT INTO " + sSOPOpservices.table + " (" +
                    sSOPOpservices.invno + ", " + sSOPOpservices.svid + ", " + sSOPOpservices.class1 + ", " +
                    sSOPOpservices.hcode + ", " + sSOPOpservices.hn + ", " + sSOPOpservices.pid + ", " +
                    sSOPOpservices.careaccount + ", " + sSOPOpservices.typeserv + ", " + sSOPOpservices.typein + ", " +
                    sSOPOpservices.typeout + ", " + sSOPOpservices.dtappoint + ", " + sSOPOpservices.svpid + ", " +
                    sSOPOpservices.clinic + ", " + sSOPOpservices.begdt + ", " + sSOPOpservices.enddt + ", " +
                    sSOPOpservices.lccode + ", " + sSOPOpservices.codeset + ", " + sSOPOpservices.stdcode + ", " +
                    sSOPOpservices.svcharge + ", " + sSOPOpservices.completion + ", " + sSOPOpservices.svtxcode + ", " +
                    sSOPOpservices.claimcat +","+ sSOPOpservices.active+","+sSOPOpservices.billtrans_id + ") " +
                    "VALUES (" +
                    "'" + p.invno + "', '" + p.svid + "', '" + p.class1 + "', " +
                    "'" + p.hcode + "', '" + p.hn + "', '" + p.pid + "', " +
                    "'" + p.careaccount + "', '" + p.typeserv + "', '" + p.typein + "', " +
                    "'" + p.typeout + "', '" + p.dtappoint + "', '" + p.svpid + "', " +
                    "'" + p.clinic + "', '" + p.begdt + "', '" + p.enddt + "', " +
                    "'" + p.lccode + "', '" + p.codeset + "', '" + p.stdcode + "', " +
                    "'" + p.svcharge + "', '" + p.completion + "', '" + p.svtxcode + "', " +
                    "'" + p.claimcat + "','1','"+p.billtrans_id+"')";

                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in insert: " + ex.Message);
            }
            return result;
        }

        public string update(SSOPOpservices p)
        {
            string result = "";
            try
            {
                //sSOPOpservices = chknull(sSOPOpservices);
                string sql = "UPDATE " + sSOPOpservices.table + " SET " +
                    sSOPOpservices.invno + " = '" + p.invno + "', " +
                    sSOPOpservices.svid + " = '" + p.svid + "', " +
                    sSOPOpservices.class1 + " = '" + p.class1 + "', " +
                    sSOPOpservices.hcode + " = '" + p.hcode + "', " +
                    sSOPOpservices.hn + " = '" + p.hn + "', " +
                    sSOPOpservices.pid + " = '" + p.pid + "', " +
                    sSOPOpservices.careaccount + " = '" + p.careaccount + "', " +
                    sSOPOpservices.typeserv + " = '" + p.typeserv + "', " +
                    sSOPOpservices.typein + " = '" + p.typein + "', " +
                    sSOPOpservices.typeout + " = '" + p.typeout + "', " +
                    sSOPOpservices.dtappoint + " = '" + p.dtappoint + "', " +
                    sSOPOpservices.svpid + " = '" + p.svpid + "', " +
                    sSOPOpservices.clinic + " = '" + p.clinic + "', " +
                    sSOPOpservices.begdt + " = '" + p.begdt + "', " +
                    sSOPOpservices.enddt + " = '" + p.enddt + "', " +
                    sSOPOpservices.lccode + " = '" + p.lccode + "', " +
                    sSOPOpservices.codeset + " = '" + p.codeset + "', " +
                    sSOPOpservices.stdcode + " = '" + p.stdcode + "', " +
                    sSOPOpservices.svcharge + " = '" + p.svcharge + "', " +
                    sSOPOpservices.completion + " = '" + p.completion + "', " +
                    sSOPOpservices.svtxcode + " = '" + p.svtxcode + "', " +
                    sSOPOpservices.claimcat + " = '" + p.claimcat + "' " +
                    "WHERE " + sSOPOpservices.opservices_id + " = '" + p.opservices_id + "'";

                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in update: " + ex.Message);
            }
            return result;
        }

        public SSOPOpservices setData(DataRow row)
        {
            SSOPOpservices p = new SSOPOpservices();
            p.opservices_id = row[sSOPOpservices.opservices_id]?.ToString() ?? "";
            p.invno = row[sSOPOpservices.invno]?.ToString() ?? "";
            p.svid = row[sSOPOpservices.svid]?.ToString() ?? "";
            p.class1 = row[sSOPOpservices.class1]?.ToString() ?? "";
            p.hcode = row[sSOPOpservices.hcode]?.ToString() ?? "";
            p.hn = row[sSOPOpservices.hn]?.ToString() ?? "";
            p.pid = row[sSOPOpservices.pid]?.ToString() ?? "";
            p.careaccount = row[sSOPOpservices.careaccount]?.ToString() ?? "";
            p.typeserv = row[sSOPOpservices.typeserv]?.ToString() ?? "";
            p.typein = row[sSOPOpservices.typein]?.ToString() ?? "";
            p.typeout = row[sSOPOpservices.typeout]?.ToString() ?? "";
            p.dtappoint = row[sSOPOpservices.dtappoint]?.ToString() ?? "";
            p.svpid = row[sSOPOpservices.svpid]?.ToString() ?? "";
            p.clinic = row[sSOPOpservices.clinic]?.ToString() ?? "";
            p.begdt = row[sSOPOpservices.begdt]?.ToString() ?? "";
            p.enddt = row[sSOPOpservices.enddt]?.ToString() ?? "";
            p.lccode = row[sSOPOpservices.lccode]?.ToString() ?? "";
            p.codeset = row[sSOPOpservices.codeset]?.ToString() ?? "";
            p.stdcode = row[sSOPOpservices.stdcode]?.ToString() ?? "";
            p.svcharge = row[sSOPOpservices.svcharge]?.ToString() ?? "";
            p.completion = row[sSOPOpservices.completion]?.ToString() ?? "Y";
            p.svtxcode = row[sSOPOpservices.svtxcode]?.ToString() ?? "";
            p.claimcat = row[sSOPOpservices.claimcat]?.ToString() ?? "";
            p.billtrans_id = row[sSOPOpservices.billtrans_id]?.ToString() ?? "";
            p.completion = p.completion.Equals("") ? "Y" : p.completion;
            return p;
        }

        public SSOPOpservices setData()
        {
            SSOPOpservices sSOPOpservices = new SSOPOpservices();
            sSOPOpservices.opservices_id = "";
            sSOPOpservices.invno = "";
            sSOPOpservices.svid = "";
            sSOPOpservices.class1 = "";
            sSOPOpservices.hcode = "";
            sSOPOpservices.hn = "";
            sSOPOpservices.pid = "";
            sSOPOpservices.careaccount = "";
            sSOPOpservices.typeserv = "";
            sSOPOpservices.typein = "";
            sSOPOpservices.typeout = "";
            sSOPOpservices.dtappoint = "";
            sSOPOpservices.svpid = "";
            sSOPOpservices.clinic = "";
            sSOPOpservices.begdt = "";
            sSOPOpservices.enddt = "";
            sSOPOpservices.lccode = "";
            sSOPOpservices.codeset = "";
            sSOPOpservices.stdcode = "";
            sSOPOpservices.svcharge = "";
            sSOPOpservices.completion = "";
            sSOPOpservices.svtxcode = "";
            sSOPOpservices.claimcat = "";
            sSOPOpservices.billtrans_id = "";
            return sSOPOpservices;
        }
    }
}
