using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class TokenDB
    {
        public ConnectDB conn;
        public Token token;
        public TokenDB(ConnectDB c) 
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            token = new Token();
            token.token_id = "token_id";
            token.token_module_name = "token_module_name";
            token.date_expire = "date_expire";
            token.user_name = "user_name";
            token.secret_key = "secret_key";
            token.token_text = "token_text";
            token.date_create = "date_create";
            token.date_cancel = "date_cancel";
            token.number_days_expire = "number_days_expire";
            token.token_type = "token_type";
            token.used = "used";
            token.used_by_table_id = "used_by_table_id";
            token.used_date = "used_date";

            token.active = "active";
            token.pkField = "token_id";
            token.table = "t_token";
        }
        public DataTable SelectUsedByUsername(String username)
        {
            DataTable dt = new DataTable();
            String year = "";
            String sql = "Select token.* " +
                "From t_token token " +
                "Where user_name = '" + username + "' and token.used = '1' and active = '1' Order By token.token_id ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable SelectUnUsedByUsername(String username)
        {
            DataTable dt = new DataTable();
            String year = "";
            String sql = "Select token.* " +
                "From t_token token " +
                "Where user_name = '" + username + "' and token.used != '1' and active = '1' Order By token.token_id ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable SelectUnUsedByUsername(String username, String tokentype)
        {
            DataTable dt = new DataTable();
            String year = "";
            String sql = "Select token.* " +
                "From t_token token " +
                "Where user_name = '" + username + "' and token.token_type = '"+ tokentype + "' and token.used != '1' and active = '1' Order By token.token_id ";
            dt = conn.selectData(sql);

            return dt;
        }
        public String updateUsed(String username, String tokentype, String tableid)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update "+token.table + " " +
                    " Set " + token.used + " = '1' " +
                    ","+token.used_by_table_id+" = '"+ tableid + "' " +
                    ","+token.used_date + " = convert(varchar(20), getdate(),23) " +
                    "Where "+token.token_id+" = (select  min("+token.token_id+") from "+token.table+" Where  active = '1' " +
                    "and " + token.user_name + "='" + username + "' and "+token.token_type+" = '"+tokentype+"') ";
                chk = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "updateUsed TokenDB");
            }
            return chk;
        }
        public String insert(Token p)
        {
            String sql = "", chk = "";
            try
            {
                p.active = "1";
                sql = "Insert Into " + token.table + "(" + token.token_module_name + "," + token.date_expire + " " +
                    ","+token.user_name + "," + token.secret_key + "," + token.token_text + " " +
                    ","+token.date_create + "," + token.number_days_expire + "," + token.active + "," + token.token_type + " " +
                    ","+token.used+"," + token.used_by_table_id+"," + token.used_date + ") " +
                    "Values('" + p.token_module_name + "','" + p.date_expire + "','" +
                    p.user_name + "','" + p.secret_key + "','" + p.token_text + "', convert(varchar(20), getdate(), 23)" +
                    ",'" + p.number_days_expire + "','" + p.active  + "','" + p.token_type + "'" +
                    ",'0','','') ";
                chk = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "insert TokenDB");
            }

            return chk;
        }
    }
}
