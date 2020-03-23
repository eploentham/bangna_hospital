using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class LabOutDB
    {
        public ConnectDB conn;
        public LabOut labo;

        public LabOutDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labo = new LabOut();
            labo.active = "active";
            labo.lab_out_id = "lab_out_id";
            labo.lab_code = "lab_code";
            labo.lab_name = "lab_name";
            labo.patient_fullname = "patient_fullname";
            labo.hn = "hn";
            labo.an = "an";
            labo.visit_date = "visit_date";
            labo.pre_no = "pre_no";
            labo.date_create = "date_create";
            labo.date_modi = "date_modi";
            labo.date_cancel = "date_cancel";
            labo.user__create = "user__create";
            labo.user_modi = "user_modi";
            labo.user_cancel = "user_cancel";
            labo.req_no = "req_no";
            labo.status_result = "status_result";
            labo.date_result = "date_result";
            labo.date_req = "date_req";

            labo.table = "t_lab_out";
            labo.pkField = "lab_out_id";
        }
        public DataTable selectByDateReq(String vsDate)
        {
            DataTable dt = new DataTable();
            String sql = "select labo.* " +
                "From " + labo.table + " labo " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where labo." + labo.visit_date + "='" + vsDate + "' and labo." + labo.active + "='1' " +
                "Order By labo. "+ labo.lab_out_id;
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        private void chkNull(LabOut p)
        {
            long chk = 0;
            int chk1 = 0;

            p.date_modi = p.date_modi == null ? "" : p.date_modi;
            p.date_cancel = p.date_cancel == null ? "" : p.date_cancel;
            p.user_create = p.user_create == null ? "" : p.user_create;
            p.user_modi = p.user_modi == null ? "" : p.user_modi;
            p.user_cancel = p.user_cancel == null ? "" : p.user_cancel;

            p.lab_code = p.lab_code == null ? "" : p.lab_code;
            p.lab_name = p.lab_name == null ? "" : p.lab_name;
            p.patient_fullname = p.patient_fullname == null ? "" : p.patient_fullname;
            p.hn = p.hn == null ? "" : p.hn;
            p.an = p.an == null ? "" : p.an;
            p.visit_date = p.visit_date == null ? "" : p.visit_date;
            p.status_result = p.status_result == null ? "0" : p.status_result;
            p.status_result = p.status_result.Length == 0 ? "0" : p.status_result;
            p.date_result = p.date_result == null ? "" : p.date_result;
            p.date_result = p.date_req == null ? "" : p.date_req;

            p.req_no = long.TryParse(p.req_no, out chk) ? chk.ToString() : "0";
            p.pre_no = long.TryParse(p.pre_no, out chk) ? chk.ToString() : "0";
        }
        public String insertLabOut(LabOut p)
        {
            String sql = "", chk = "",re="";
            try
            {
                chkNull(p);
                //p.dateModi = " CAST(year(GETDATE()) AS NVARCHAR)+'-'+ RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)+'-'+ RIGHT('00' + CAST(day(GETDATE()) AS NVARCHAR), 2)";
                p.patient_fullname = p.patient_fullname.Replace("'", "''");
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.conn;
                conn.comStore.CommandText = "[insert_t_lab_out]";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("lab_code", p.lab_code);
                conn.comStore.Parameters.AddWithValue("lab_name", p.lab_name.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("patient_fullname", p.patient_fullname.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("hn", p.hn);
                conn.comStore.Parameters.AddWithValue("an", p.an);
                conn.comStore.Parameters.AddWithValue("vn", p.vn);
                conn.comStore.Parameters.AddWithValue("visit_date", p.visit_date);
                conn.comStore.Parameters.AddWithValue("pre_no", p.pre_no);
                conn.comStore.Parameters.AddWithValue("req_no", p.req_no);
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;

                conn.conn.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;

            }
            catch (Exception ex)
            {
                new LogWriter("e", "insertLabOut " + sql);
            }
            finally
            {
                conn.conn.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String update(LabOut p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            chkNull(p);
            sql = "Update " + labo.table + " Set " +
                " " + labo.lab_code + " = '" + p.lab_code + "'" +
                "," + labo.lab_name + " = '" + p.lab_name + "'" +
                "," + labo.patient_fullname + " = '" + p.patient_fullname + "'" +
                "," + labo.pre_no + " = '" + p.pre_no + "'" +
                "," + labo.hn + " = '" + p.hn + "'" +
                "," + labo.vn + " = '" + p.vn + "'" +
                "," + labo.visit_date + " = '" + p.visit_date + "'" +
                "," + labo.req_no + " = '" + p.req_no + "'" +
                "," + labo.date_modi + " = convert(varchar, getdate(), 23)" +
                "," + labo.user_modi + " = '" + userId + "'" +
                "Where " + labo.pkField + "='" + p.lab_out_id + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }

            return re;
        }
        public String voidByDate(String req_date, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            //chkNull(p);
            sql = "Update " + labo.table + " Set " +
                " " + labo.active + " = '3'" +
                "," + labo.date_cancel + " = convert(varchar, getdate(), 23)" +
                "," + labo.user_cancel + " = '" + userId + "'" +
                "Where " + labo.visit_date + "='" + req_date + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }

            return re;
        }
        public String updateStatusResult(String hn, String req_date, String req_id, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            //chkNull(p);
            sql = "Update " + labo.table + " Set " +
                " " + labo.status_result + " = '1'" +
                "," + labo.date_result + " = convert(varchar, getdate(), 23)" +
                "," + labo.user_cancel + " = '" + userId + "'" +
                "Where " + labo.visit_date + "='" + req_date + "' and "+labo.hn +"='"+hn+"' and "+labo.req_no+"='"+req_id+"'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateStatusResult " + sql);
            }

            return re;
        }
    }
}
