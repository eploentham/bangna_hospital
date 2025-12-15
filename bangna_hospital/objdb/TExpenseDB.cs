using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    internal class TExpenseDB
    {
        private ConnectDB conn;
        private string table = "t_expense";
        public TExpenseDB(ConnectDB c)
        {
            conn = c ?? throw new ArgumentNullException(nameof(c));
        }
        public string Insert(TExpense p)
        {
            try
            {
                // sanitize simple string inputs (basic) to avoid breaking SQL when ConnectDB doesn't support parameters.
                string desc = (p.expense_desc ?? "").Replace("'", "''");
                string module = (p.module_id ?? "").Replace("'", "''");
                string tcol = (p.t_expensecol ?? "").Replace("'", "''");
                string tbl = (p.table_name ?? "").Replace("'", "''");
                string req = (p.user_req_id ?? "").Replace("'", "''");
                string app = (p.user_approve_id ?? "").Replace("'", "''");
                string cash = (p.user_cashier_id ?? "").Replace("'", "''");

                string sql = $"INSERT INTO {table} (expense_date, expense_desc, expense, date_create, active, module_id, row_id, table_name, user_req_id, user_approve_id, user_cashier_id, date_req, date_approve, date_cashier,status_draw) " +
                    "VALUES (" +
                    $"'{(p.expense_date ?? "")}', " +
                    $"'{desc}', " +
                    $"{p.expense.ToString("0.00")}, " +
                    $"now(), " +
                    $"'{(p.active ?? "1")}', " +
                    $"'{module}', " +
                    $"{(p.row_id.HasValue ? p.row_id.Value.ToString() : "NULL")}, " +
                    $"'{tbl}', " +
                    $"'{req}', " +
                    $"'{app}', " +
                    $"'{cash}', " +
                    $"now(), " +
                    $"'{(p.date_approve ?? "")}', " +
                    $"'{(p.date_cashier ?? "")}'" +
                    $",'1'" +
                    ")";

                // ExecuteNonQuery expected to return rows affected; adjust if your ConnectDB has different semantics.
                conn.ExecuteNonQuery(conn.connMySQL, sql);

                // get last id (MySQL style). If your DB differs, change this.
                object o = conn.ExecuteScalar(conn.connMySQL, "SELECT LAST_INSERT_ID()");
                return o?.ToString() ?? "0";
            }
            catch (Exception ex)
            {
                // log or rethrow as appropriate in your project
                return "0";
            }
        }
        public String Update(TExpense p)
        {
            try
            {
                string desc = (p.expense_desc ?? "").Replace("'", "''");
                string module = (p.module_id ?? "").Replace("'", "''");
                string tcol = (p.t_expensecol ?? "").Replace("'", "''");
                string tbl = (p.table_name ?? "").Replace("'", "''");
                string req = (p.user_req_id ?? "").Replace("'", "''");
                string app = (p.user_approve_id ?? "").Replace("'", "''");
                string cash = (p.user_cashier_id ?? "").Replace("'", "''");

                string sql = $"UPDATE {table} SET " +
                    $"expense_date = '{(p.expense_date ?? "")}', " +
                    $"expense_desc = '{desc}', " +
                    $"expense = {p.expense.ToString("0.00")}, " +
                    $"date_modi = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', " +
                    $"module_id = '{module}', " +
                    $"t_expensecol = '{tcol}', " +
                    $"row_id = {(p.row_id.HasValue ? p.row_id.Value.ToString() : "NULL")}, " +
                    $"table_name = '{tbl}', " +
                    $"user_req_id = '{req}', " +
                    $"user_approve_id = '{app}', " +
                    $"user_cashier_id = '{cash}', " +
                    $"date_req = '{(p.date_req ?? "")}', " +
                    $"date_approve = '{(p.date_approve ?? "")}', " +
                    $"date_cashier = '{(p.date_cashier ?? "")}' " +
                    $"WHERE expense_id = {p.expense_id}";

                String re = conn.ExecuteNonQuery(conn.connMySQL, sql);
                return re;
            }
            catch
            {
                return "";
            }
        }
        public DataTable SelectByDraw()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = $"SELECT * FROM {table} WHERE status_draw =  '1' ";
                dt = conn.selectDataMySQL(conn.connMySQL, sql); // adjust method name if ConnectDB uses a different name
                if (dt == null || dt.Rows.Count == 0) return dt;
                return dt;
            }
            catch
            {
                return dt;
            }
        }
        public TExpense SelectByPk(long id)
        {
            try
            {
                string sql = $"SELECT * FROM {table} WHERE expense_id = {id}";
                DataTable dt = conn.selectDataMySQL(conn.connMySQL, sql); // adjust method name if ConnectDB uses a different name
                if (dt == null || dt.Rows.Count == 0) return null;
                return ToObject(dt.Rows[0]);
            }
            catch
            {
                return null;
            }
        }

        public List<TExpense> SelectAll(string where = "")
        {
            var list = new List<TExpense>();
            try
            {
                string sql = $"SELECT * FROM {table}";
                if (!string.IsNullOrWhiteSpace(where)) sql += " WHERE " + where;
                DataTable dt = conn.selectDataMySQL(conn.connMySQL, sql); // adjust if needed
                if (dt == null) return list;
                foreach (DataRow r in dt.Rows) list.Add(ToObject(r));
            }
            catch { }
            return list;
        }
        public String updateAmountDraw(long id, String amount, string userId = "")
        {
            try
            {
                String amt = amount.Replace(",", "");
                float  famt = 0;
                if(float.TryParse(amt, out famt))
                {
                    string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string sql = $"UPDATE {table} SET status_draw = '2', date_cashier = now(), user_cashier_id = '{(userId ?? "")}',amount_approve ={famt} WHERE expense_id = {id}";
                    String re = conn.ExecuteNonQuery(conn.connMySQL, sql);
                    return re;
                }
                else { return "";                }
            }
            catch
            {
                return "";
            }
        }
        public String Void(long id, string userId = "")
        {
            try
            {
                string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = $"UPDATE {table} SET active = '0', date_cancel = '{now}', user_cashier_id = '{(userId ?? "")}' WHERE expense_id = {id}";
                String re = conn.ExecuteNonQuery(conn.connMySQL, sql);
                return re;
            }
            catch
            {
                return "";
            }
        }

        #region helpers
        private TExpense ToObject(DataRow r)
        {
            var p = new TExpense();
            p.expense_id = r["expense_id"] == DBNull.Value ? 0 : Convert.ToInt64(r["expense_id"]);
            p.expense_date = r["expense_date"] == DBNull.Value ? "" : r["expense_date"].ToString();
            p.expense_desc = r["expense_desc"] == DBNull.Value ? "" : r["expense_desc"].ToString();
            p.expense = r["expense"] == DBNull.Value ? 0m : Convert.ToDecimal(r["expense"]);
            p.module_id = r.Table.Columns.Contains("module_id") && r["module_id"] != DBNull.Value ? r["module_id"].ToString() : "";
            p.t_expensecol = r.Table.Columns.Contains("t_expensecol") && r["t_expensecol"] != DBNull.Value ? r["t_expensecol"].ToString() : "";
            p.row_id = r.Table.Columns.Contains("row_id") && r["row_id"] != DBNull.Value ? (long?)Convert.ToInt64(r["row_id"]) : null;
            p.table_name = r.Table.Columns.Contains("table_name") && r["table_name"] != DBNull.Value ? r["table_name"].ToString() : "";
            p.user_req_id = r.Table.Columns.Contains("user_req_id") && r["user_req_id"] != DBNull.Value ? r["user_req_id"].ToString() : "";
            p.user_approve_id = r.Table.Columns.Contains("user_approve_id") && r["user_approve_id"] != DBNull.Value ? r["user_approve_id"].ToString() : "";
            p.user_cashier_id = r.Table.Columns.Contains("user_cashier_id") && r["user_cashier_id"] != DBNull.Value ? r["user_cashier_id"].ToString() : "";
            p.date_req = r.Table.Columns.Contains("date_req") && r["date_req"] != DBNull.Value ? r["date_req"].ToString() : "";
            p.date_approve = r.Table.Columns.Contains("date_approve") && r["date_approve"] != DBNull.Value ? r["date_approve"].ToString() : "";
            p.date_cashier = r.Table.Columns.Contains("date_cashier") && r["date_cashier"] != DBNull.Value ? r["date_cashier"].ToString() : "";
            return p;
        }
        #endregion
    }
}
