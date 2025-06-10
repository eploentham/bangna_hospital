using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class SSOPBillTranDB
    {
        public ConnectDB conn;
        SSOPBillTran sSOPBillTran;
        public SSOPBillTranDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            sSOPBillTran = new SSOPBillTran();
            sSOPBillTran.billtran_id = "billtran_id";
            sSOPBillTran.station = "station";
            sSOPBillTran.authcode = "authcode";
            sSOPBillTran.dtran = "dtran";
            sSOPBillTran.hcode = "hcode";
            sSOPBillTran.invno = "invno";
            sSOPBillTran.billno = "billno";
            sSOPBillTran.hn = "hn";
            sSOPBillTran.memberno = "memberno";
            sSOPBillTran.amount = "amount";
            sSOPBillTran.paid = "paid";
            sSOPBillTran.vercode = "vercode";
            sSOPBillTran.tflag = "tflag";
            sSOPBillTran.pid = "pid";
            sSOPBillTran.name = "name";
            sSOPBillTran.hmain = "hmain";
            sSOPBillTran.payplan = "payplan";
            sSOPBillTran.claimamt = "claimamt";
            sSOPBillTran.otherpayplan = "otherpayplan";
            sSOPBillTran.otherpay = "otherpay";
            sSOPBillTran.active = "active";
            sSOPBillTran.preno = "preno";
            sSOPBillTran.status_process = "status_process";
            sSOPBillTran.pkField = "billtran_id";
            sSOPBillTran.table = "ssop_t_billtran";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                // SQL query to select all data from the SSOPBillTran table
                String sql = "SELECT * FROM " + sSOPBillTran.table + " WHERE " + sSOPBillTran.active + " = '1'";
                dt = conn.selectData(sql);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine("Error in selectAll: " + ex.Message);
            }
            return dt;
        }
        public DataTable selectBySearch(String search)
        {
            DataTable dt = new DataTable();
            String where = "";
            try
            {
                where = " and ((hn like '%"+ search + "%')  or (name like '%"+ search + "%'))";
                // SQL query to select all data from the SSOPBillTran table
                String sql = "SELECT * FROM " + sSOPBillTran.table + " WHERE " + sSOPBillTran.active + " = '1' "+ where;
                dt = conn.selectData(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
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
                String sql = "SELECT max(billtran_id) as billtran_id FROM " + sSOPBillTran.table + " ";
                dt = conn.selectData(conn.connSsnData, sql);
                re = dt.Rows[0]["billtran_id"].ToString();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine("Error in selectAll: " + ex.Message);
            }
            return re;
        }
        public SSOPBillTran selectByPk(String billtran_id)
        {
            SSOPBillTran sSOPBillTran1 = new SSOPBillTran();
            String sql = "Select * From " + sSOPBillTran.table + " Where " + sSOPBillTran.billtran_id + "='" + billtran_id + "'";
            DataTable dt = conn.selectData(conn.connSsnData, sql);
            if (dt.Rows.Count > 0)            {                sSOPBillTran1 = setData(dt.Rows[0]);            }
            else { sSOPBillTran1 = setData(); }
            return sSOPBillTran1;
        }
        public DataTable selectBystatusprocess(String statusprocess)
        {
            SSOPBillTran sSOPBillTran1 = new SSOPBillTran();
            String sql = "Select * From " + sSOPBillTran.table + " Where " + sSOPBillTran.status_process + "='" + statusprocess + "' and active = '1' Order By "+ sSOPBillTran.billtran_id+" desc";
            DataTable dt = conn.selectData(conn.connSsnData, sql);
            return dt;
        }
        public String selectMaxSessionID()
        {
            String sql = "", re="";
            sql = "SELECT MAX(sessionid)+1 FROM ssop ";
            DataTable dt = conn.selectData(conn.connSsnData, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0][0].ToString();
                sql = "update ssop set sessionid = "+ re + " ";
                conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            else
            {
                re = "1";
            }
            return re;
        }
        public string updateStatusProcessMakeTextPrepare(String id)
        {
            string result = "";
            try
            {
                string sql = "UPDATE " + sSOPBillTran.table + " SET " +
                sSOPBillTran.status_process + " = '0' " +
                "WHERE " + sSOPBillTran.pkField + " = '" + id + "'";
                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine("Error in update: " + ex.Message);
            }
            return result;
        }
        public string updateStatusProcessMakeText(String id)
        {
            string result = "";
            try
            {
                string sql = "UPDATE " + sSOPBillTran.table + " SET " +
                sSOPBillTran.status_process + " = '1' " +
                "WHERE " + sSOPBillTran.pkField + " = '" + id + "'";
                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine("Error in update: " + ex.Message);
            }
            return result;
        }
        public string updateStatusProcessMakeTextSend(String id)
        {
            string result = "";
            try
            {
                string sql = "UPDATE " + sSOPBillTran.table + " SET " +
                sSOPBillTran.status_process + " = '2' " +
                "WHERE " + sSOPBillTran.pkField + " = '" + id + "'";
                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine("Error in update: " + ex.Message);
            }
            return result;
        }
        public string updateStatusVoid(String id)
        {
            string result = "";
            try
            {
                string sql = "UPDATE " + sSOPBillTran.table + " SET " +
                sSOPBillTran.active + " = '3' " +
                "WHERE " + sSOPBillTran.pkField + " = '" + id + "'";
                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine("Error in update: " + ex.Message);
            }
            return result;
        }
        public SSOPBillTran chknull(SSOPBillTran sSOPBillTran)
        {
            if (sSOPBillTran == null)            {                sSOPBillTran = new SSOPBillTran();            }

            // Replace null values with empty strings
            sSOPBillTran.billtran_id = sSOPBillTran.billtran_id ?? "";
            sSOPBillTran.station = sSOPBillTran.station ?? "";
            sSOPBillTran.authcode = sSOPBillTran.authcode ?? "";
            sSOPBillTran.dtran = sSOPBillTran.dtran ?? "";
            sSOPBillTran.hcode = sSOPBillTran.hcode ?? "";
            sSOPBillTran.invno = sSOPBillTran.invno ?? "";
            sSOPBillTran.billno = sSOPBillTran.billno ?? "";
            sSOPBillTran.hn = sSOPBillTran.hn ?? "";
            sSOPBillTran.memberno = sSOPBillTran.memberno ?? "";
            sSOPBillTran.amount = sSOPBillTran.amount ?? "";
            sSOPBillTran.paid = sSOPBillTran.paid ?? "";
            sSOPBillTran.vercode = sSOPBillTran.vercode ?? "";
            sSOPBillTran.tflag = sSOPBillTran.tflag ?? "";
            sSOPBillTran.pid = sSOPBillTran.pid ?? "";
            sSOPBillTran.name = sSOPBillTran.name ?? "";
            sSOPBillTran.hmain = sSOPBillTran.hmain ?? "";
            sSOPBillTran.payplan = sSOPBillTran.payplan ?? "";
            sSOPBillTran.claimamt = sSOPBillTran.claimamt ?? "";
            sSOPBillTran.otherpayplan = sSOPBillTran.otherpayplan ?? "";
            sSOPBillTran.otherpay = sSOPBillTran.otherpay ?? "";
            sSOPBillTran.active = sSOPBillTran.active ?? "";
            sSOPBillTran.preno = sSOPBillTran.preno ?? "";
            sSOPBillTran.status_process = sSOPBillTran.status_process ?? "";
            return sSOPBillTran;
        }
        public string insertData(SSOPBillTran sSOPBillTran)
        {
            string result = "";
            try
            {
                // Ensure no null values in the object
                sSOPBillTran = chknull(sSOPBillTran);

                if (!string.IsNullOrEmpty(sSOPBillTran.billtran_id))
                {
                    // If billtran_id has a value, call the update method
                    result = update(sSOPBillTran);
                }
                else
                {
                    // If billtran_id is empty, call the insert method
                    result = insert(sSOPBillTran);
                    result = selectMax();
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine("Error in insertData: " + ex.Message);
            }
            return result;
        }
        public string insert(SSOPBillTran p)
        {
            string result = "";
            try
            {
                //sSOPBillTran = chknull(sSOPBillTran);
                // Construct the SQL INSERT statement
                string sql = "INSERT INTO " + sSOPBillTran.table + " (" +
                    sSOPBillTran.station + ", " +                       sSOPBillTran.authcode + ", " +                          sSOPBillTran.dtran + ", " +
                    sSOPBillTran.hcode + ", " +                         sSOPBillTran.invno + ", " +                             sSOPBillTran.billno + ", " +
                    sSOPBillTran.hn + ", " +                            sSOPBillTran.memberno + ", " +                          sSOPBillTran.amount + ", " +
                    sSOPBillTran.paid + ", " +                          sSOPBillTran.vercode + ", " +                        sSOPBillTran.tflag + ", " +
                    sSOPBillTran.pid + ", " +                           sSOPBillTran.name + ", " +                            sSOPBillTran.hmain + ", " +
                    sSOPBillTran.payplan + ", " +                       sSOPBillTran.claimamt + ", " +                    sSOPBillTran.otherpayplan + ", " +
                    sSOPBillTran.otherpay + ", " +                      sSOPBillTran.active + ", " +                     sSOPBillTran.preno + ", date_create, status_process) " +
                    "VALUES (" +
                    "'" + p.station + "', " +                             "'" + p.authcode + "', " +                        "'" + p.dtran + "', " +
                    "'" + p.hcode + "', " +                             "'" + p.invno + "', " +                             "'" + p.billno + "', " +
                    "'" + p.hn + "', " +                             "'" + p.memberno + "', " +                             "'" + p.amount + "', " +
                    "'" + p.paid + "', " +                             "'" + p.vercode + "', " +                            "'" + p.tflag + "', " +
                    "'" + p.pid + "', " +                             "'" + p.name + "', " +                                "'" + p.hmain + "', " +
                    "'" + p.payplan + "', " +                             "'" + p.claimamt + "', " +                        "'" + p.otherpayplan + "', " +
                    "'" + p.otherpay + "', " +                             "'" + p.active + "', " +                         "'" + p.preno + "',convert(varchar(20), getdate(),23),'0')";

                // Execute the SQL command
                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine("Error in insert: " + ex.Message);
            }
            return result;
        }
        public string update(SSOPBillTran p)
        {
            string result = "";
            try
            {
                // Ensure no null values in the object
                //sSOPBillTran = chknull(sSOPBillTran);

                // Construct the SQL UPDATE statement
                string sql = "UPDATE " + sSOPBillTran.table + " SET " +
                sSOPBillTran.station + " = '" + p.station + "', " +
                sSOPBillTran.authcode + " = '" + p.authcode + "', " +
                sSOPBillTran.dtran + " = '" + p.dtran + "', " +
                sSOPBillTran.hcode + " = '" + p.hcode + "', " +
                sSOPBillTran.invno + " = '" + p.invno + "', " +
                sSOPBillTran.billno + " = '" + p.billno + "', " +
                //sSOPBillTran.hn + " = '" + p.hn + "', " +
                sSOPBillTran.memberno + " = '" + p.memberno + "', " +
                sSOPBillTran.amount + " = '" + p.amount + "', " +
                sSOPBillTran.paid + " = '" + p.paid + "', " +
                sSOPBillTran.vercode + " = '" + p.vercode + "', " +
                sSOPBillTran.tflag + " = '" + p.tflag + "', " +
                sSOPBillTran.pid + " = '" + p.pid + "', " +
                //sSOPBillTran.name + " = '" + p.name + "', " +
                sSOPBillTran.hmain + " = '" + p.hmain + "', " +
                sSOPBillTran.payplan + " = '" + p.payplan + "', " +
                sSOPBillTran.claimamt + " = '" + p.claimamt + "', " +
                sSOPBillTran.otherpayplan + " = '" + p.otherpayplan + "', " +
                sSOPBillTran.otherpay + " = '" + p.otherpay + "' " +
                //sSOPBillTran.active + " = '" + p.active + "' " +
                "WHERE " + sSOPBillTran.billtran_id + " = '" + p.billtran_id + "'";

                // Execute the SQL command
                result = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine("Error in update: " + ex.Message);
            }
            return result;
        }
        public SSOPBillTran setData(DataRow row)
        {
            SSOPBillTran btran = new SSOPBillTran();
            btran.billtran_id = row[sSOPBillTran.billtran_id]?.ToString() ?? "";
            btran.station = row[sSOPBillTran.station]?.ToString() ?? "";
            btran.authcode = row[sSOPBillTran.authcode]?.ToString() ?? "";
            btran.dtran = row[sSOPBillTran.dtran]?.ToString() ?? "";
            btran.hcode = row[sSOPBillTran.hcode]?.ToString() ?? "";
            btran.invno = row[sSOPBillTran.invno]?.ToString() ?? "";
            btran.billno = row[sSOPBillTran.billno]?.ToString() ?? "";
            btran.hn = row[sSOPBillTran.hn]?.ToString() ?? "";
            btran.memberno = row[sSOPBillTran.memberno]?.ToString() ?? "";
            btran.amount = row[sSOPBillTran.amount]?.ToString() ?? "";
            btran.paid = row[sSOPBillTran.paid]?.ToString() ?? "";
            btran.vercode = row[sSOPBillTran.vercode]?.ToString() ?? "";
            btran.tflag = row[sSOPBillTran.tflag]?.ToString() ?? "";
            btran.pid = row[sSOPBillTran.pid]?.ToString() ?? "";
            btran.name = row[sSOPBillTran.name]?.ToString() ?? "";
            btran.hmain = row[sSOPBillTran.hmain]?.ToString() ?? "";
            btran.payplan = row[sSOPBillTran.payplan]?.ToString() ?? "";
            btran.claimamt = row[sSOPBillTran.claimamt]?.ToString() ?? "";
            btran.otherpayplan = row[sSOPBillTran.otherpayplan]?.ToString() ?? "";
            btran.otherpay = row[sSOPBillTran.otherpay]?.ToString() ?? "";
            btran.active = row[sSOPBillTran.active]?.ToString() ?? "";
            btran.preno = row[sSOPBillTran.preno]?.ToString() ?? "";
            btran.status_process = row[sSOPBillTran.status_process]?.ToString() ?? "";
            return btran;
        }
        public SSOPBillTran setData()
        {
            SSOPBillTran sSOPBillTran = new SSOPBillTran();
            sSOPBillTran.billtran_id = "";
            sSOPBillTran.station = "";
            sSOPBillTran.authcode = "";
            sSOPBillTran.dtran = "";
            sSOPBillTran.hcode = "";
            sSOPBillTran.invno = "";
            sSOPBillTran.billno = "";
            sSOPBillTran.hn = "";
            sSOPBillTran.memberno = "";
            sSOPBillTran.amount = "";
            sSOPBillTran.paid = "";
            sSOPBillTran.vercode = "";
            sSOPBillTran.tflag = "";
            sSOPBillTran.pid = "";
            sSOPBillTran.name = "";
            sSOPBillTran.hmain = "";
            sSOPBillTran.payplan = "";
            sSOPBillTran.claimamt = "";
            sSOPBillTran.otherpayplan = "";
            sSOPBillTran.otherpay = "";
            sSOPBillTran.active = "";
            sSOPBillTran.preno = "";
            sSOPBillTran.status_process = "";
            return sSOPBillTran;
        }
    }
}
