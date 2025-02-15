using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class TokenDB
    {
        public ConnectDB conn;
        public object1.Token token;
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

            token.active = "active";
            token.pkField = "token_id";
            token.table = "t_token";
        }
        public String insert(Token p)
        {
            String sql = "", chk = "";
            try
            {
                p.active = "1";
                sql = "Insert Into " + token.table + "(" + token.token_module_name + "," + token.date_expire + "," +
                    token.user_name + "," + token.secret_key + "," + token.token_text + "," +
                    token.date_create + "," + token.number_days_expire + "," + token.active + "," +
                    ") " +
                    "Values('" + p.token_module_name + "','" + p.date_expire + "','" +
                    p.user_name + "','" + p.secret_key + "','" + p.token_text + "','" +
                    p.date_create + "','" + p.number_days_expire + "','" + p.active + "','" +
                    "') ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "insert TokenDB");
            }

            return chk;
        }
    }
}
