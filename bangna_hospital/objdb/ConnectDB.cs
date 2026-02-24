using bangna_hospital.object1;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class ConnectDB
    {
        public SqlConnection connMainHIS, conn, connPACs, connLabOut, connLog, connSsnData, connLinkLIS, connBACK;
        public MySqlConnection connMySQL;
        public Staff user;
        public long _rowsAffected = 0, _rowsAffectedMySQL=0;
        public SqlCommand comStore;
        public NpgsqlConnection connOPBKK;
        public String _IPAddress = "", connectionStringMainHIS="";
        public ConnectDB(InitConfig initc)
        {
            //new LogWriter("d", "ConnectDB ConnectDB  00");
            conn = new SqlConnection();
            connMainHIS = new SqlConnection();
            connPACs = new SqlConnection();
            connLabOut = new SqlConnection();
            connOPBKK = new NpgsqlConnection();
            connSsnData = new SqlConnection();
            connLinkLIS = new SqlConnection();
            connBACK = new SqlConnection();

            connLog = new SqlConnection();
            //new LogWriter("d", "ConnectDB ConnectDB  01" );
            connMySQL = new MySqlConnection();
            //new LogWriter("d", "ConnectDB ConnectDB  02");
            connectionStringMainHIS = "Server=" + initc.hostDBMainHIS + ";Database=" + initc.nameDBMainHIS + ";Uid=" + initc.userDBMainHIS + ";Pwd=" + initc.passDBMainHIS + ";";
            connMainHIS.ConnectionString = "Server=" + initc.hostDBMainHIS + ";Database=" + initc.nameDBMainHIS + ";Uid=" + initc.userDBMainHIS + ";Pwd=" + initc.passDBMainHIS + ";";
            connBACK.ConnectionString = "Server=" + initc.hostDBBACK + ";Database=" + initc.nameDBBACK + ";Uid=" + initc.userDBBACK + ";Pwd=" + initc.passDBBACK + ";";
            conn.ConnectionString = "Server=" + initc.hostDB + ";Database=" + initc.nameDB + ";Uid=" + initc.userDB + ";Pwd=" + initc.passDB + ";";
            connLabOut.ConnectionString = "Server=" + initc.hostDBLabOut + ";Database=" + initc.nameDBLabOut + ";Uid=" + initc.userDBLabOut + ";Pwd=" + initc.passDBLabOut + ";";
            connPACs.ConnectionString = "Server=" + initc.hostDBPACs + ";Database=" + initc.nameDBPACs + ";Uid=" + initc.userDBPACs + ";Pwd=" + initc.passDBPACs + ";";

            connOPBKK.ConnectionString = "Host=" + initc.hostDBOPBKK + ";Username=" + initc.userDBOPBKK + ";Password=" + initc.passDBOPBKK + ";Database=" + initc.nameDBOPBKK + ";Port=" + initc.portDBOPBKK + ";";
            connLog.ConnectionString = "Server=" + initc.hostDBLogTask + ";Database=" + initc.nameDBLogTask + ";Uid=" + initc.userDBLogTask + ";Pwd=" + initc.passDBLogTask + ";";
            connSsnData.ConnectionString = "Server=" + initc.hostDBSsnData + ";Database=" + initc.nameDBSsnData + ";Uid=" + initc.userDBSsnData + ";Pwd=" + initc.passDBSsnData + ";";
            connLinkLIS.ConnectionString = "Server=" + initc.hostDBLinkLIS + ";Database=" + initc.nameDBLinkLIS + ";Uid=" + initc.userDBLinkLIS + ";Pwd=" + initc.passDBLinkLIS + ";";

            //ของ VaccineApprove ต้องใช้ MySQL เพราะ node js ยังไม่สามารถพืมพ์ FORM ได้
            connMySQL.ConnectionString = "Server=" + initc.hostDBMySQL + ";Database=" + initc.nameDBMySQL + ";Uid=" + initc.userDBMySQL + ";Pwd=" + initc.passDBMySQL +
                ";port = " + initc.portDBMySQL + ";";
        }
        public String ExecuteNonQuery(MySqlConnection con, String sql)
        {
            String toReturn = "";

            MySqlCommand com = new MySqlCommand();
            com.CommandText = sql;
            com.CommandType = CommandType.Text;
            com.Connection = con;
            try
            {
                con.Open();
                _rowsAffected = com.ExecuteNonQuery();
                //_rowsAffected = (int)com.ExecuteScalar();
                toReturn = sql.Substring(0, 1).ToLower() == "i" ? com.LastInsertedId.ToString() : _rowsAffected.ToString();
                if (sql.IndexOf("Insert Into Visit") >= 0)        //old program
                {
                    toReturn = _rowsAffected.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteNonQuery::Error occured.", ex);
                toReturn = ex.Message;
            }
            finally
            {
                //_mainConnection.Close();
                con.Close();
                com.Dispose();
            }

            return toReturn;
        }
        public String ExecuteNonQuery(String sql)
        {
            String toReturn = "";
            toReturn = ExecuteNonQuery(conn,sql);
            return toReturn;
        }
        public String ExecuteNonQueryStore(SqlConnection con, String sql)
        {
            String toReturn = "";
                        
            try
            {
                con.Open();
                
                _rowsAffected = comStore.ExecuteNonQuery();
                //}
                toReturn = _rowsAffected.ToString();
                
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteNonQuery::Error occured.", ex);
                toReturn = ex.Message;
            }
            finally
            {
                //_mainConnection.Close();
                con.Close();
                comStore.Dispose();
            }

            return toReturn;
        }
        // ✅ เพิ่ม overload method นี้ใน ConnectDB class
        public String executeNonQuery(string sql, SqlParameter[] parameters)
        {
            String re = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringMainHIS))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // เพิ่ม parameters
                        if (parameters != null && parameters.Length > 0)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        int rowsAffected = command.ExecuteNonQuery();
                        re = rowsAffected.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                re = ex.Message;
            }
            return re;
        }
        public String ExecuteNonQuery(SqlConnection con, String sql)
        {
            String toReturn = "";

            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.CommandType = CommandType.Text;
            com.Connection = con;
            try
            {
                con.Open();
                //_rowsAffected = com.ExecuteNonQuery();
                //if (sql.Substring(0,2).ToLower().IndexOf("in")>=0)
                //{
                //    _rowsAffected = (int)com.ExecuteScalar();
                //    //var aaa = com.ExecuteScalar();

                //}
                //else
                //{
                    _rowsAffected = com.ExecuteNonQuery();
                //}
                toReturn = _rowsAffected.ToString();
                //toReturn = sql.Substring(0, 1).ToLower() == "i" ? com.LastInsertedId.ToString() : _rowsAffected.ToString();
                //if (sql.IndexOf("Insert Into Visit") >= 0)        //old program
                //{
                //    toReturn = _rowsAffected.ToString();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteNonQuery::Error occured.", ex);
                toReturn = ex.Message;
            }
            finally
            {
                //_mainConnection.Close();
                con.Close();
                com.Dispose();
            }
            return toReturn;
        }
        public String ExecuteScalarNonQuery(SqlConnection con, String sql)
        {
            String toReturn = "";

            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.CommandType = CommandType.Text;
            com.Connection = con;
            try
            {
                con.Open();
                //_rowsAffected = com.ExecuteNonQuery();
                //if (sql.Substring(0,2).ToLower().IndexOf("in")>=0)
                //{
                toReturn = com.ExecuteScalar().ToString();
                //    //var aaa = com.ExecuteScalar();

                //}
                //else
                //{
                //_rowsAffected = com.ExecuteNonQuery();
                //}
                //toReturn = _rowsAffected.ToString();
                //toReturn = sql.Substring(0, 1).ToLower() == "i" ? com.LastInsertedId.ToString() : _rowsAffected.ToString();
                //if (sql.IndexOf("Insert Into Visit") >= 0)        //old program
                //{
                //    toReturn = _rowsAffected.ToString();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteNonQuery::Error occured.", ex);
                toReturn = ex.Message;
            }
            finally
            {
                //_mainConnection.Close();
                con.Close();
                com.Dispose();
            }

            return toReturn;
        }
        public String ExecuteNonQueryImage(SqlConnection con, String sql, byte[] photo_aray)
        {
            String toReturn = "";

            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.CommandType = CommandType.Text;
            com.Connection = con;
            com.Parameters.AddWithValue("@photo", photo_aray);
            try
            {
                //ms.Position = 0;
                con.Open();
                //_rowsAffected = com.ExecuteNonQuery();
                //if (sql.Substring(0,2).ToLower().IndexOf("in")>=0)
                //{
                //    _rowsAffected = (int)com.ExecuteScalar();
                //    //var aaa = com.ExecuteScalar();

                //}
                //else
                //{
                _rowsAffected = com.ExecuteNonQuery();
                //}
                toReturn = _rowsAffected.ToString();
                //toReturn = sql.Substring(0, 1).ToLower() == "i" ? com.LastInsertedId.ToString() : _rowsAffected.ToString();
                //if (sql.IndexOf("Insert Into Visit") >= 0)        //old program
                //{
                //    toReturn = _rowsAffected.ToString();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteNonQuery::Error occured.", ex);
                toReturn = ex.Message;
            }
            finally
            {
                //_mainConnection.Close();
                con.Close();
                com.Dispose();
            }

            return toReturn;
        }
        public String ExecuteNonQueryLogPage(SqlConnection con, String sql)
        {
            String toReturn = "";

            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.CommandType = CommandType.Text;
            com.Connection = con;
            try
            {
                con.Open();
                _rowsAffected = com.ExecuteNonQuery();
                
                toReturn = _rowsAffected.ToString();
                
            }
            catch (Exception ex)
            {
                //throw new Exception("ExecuteNonQuery::Error occured.", ex);
                toReturn = ex.Message;
            }
            finally
            {
                //_mainConnection.Close();
                con.Close();
                com.Dispose();
            }

            return toReturn;
        }
        public DataTable selectStoreProcedure(SqlCommand comMainhis)
        {
            DataTable toReturn = new DataTable();
            comMainhis.Connection = connMainHIS;
            //SqlCommand comMainhis = new SqlCommand();
            //comMainhis.CommandText = storename;
            //comMainhis.CommandType = CommandType.StoredProcedure;
            //comMainhis.Connection = connMainHIS;
            //comMainhis.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId;
            //comMainhis.Parameters.Add("@Qid", SqlDbType.VarChar).Value = qid;
            SqlDataAdapter adapMainhis = new SqlDataAdapter(comMainhis);
            try
            {
                connMainHIS.Open();
                adapMainhis.Fill(toReturn);
                //return toReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                connMainHIS.Close();
                comMainhis.Dispose();
                adapMainhis.Dispose();
            }

            return toReturn;
        }
        public DataTable selectDataMySQL(MySqlConnection con, String sql)
        {
            DataTable toReturn = new DataTable();

            MySqlCommand comMainhis = new MySqlCommand();
            comMainhis.CommandText = sql;
            comMainhis.CommandType = CommandType.Text;
            comMainhis.Connection = con;
            MySqlDataAdapter adapMainhis = new MySqlDataAdapter(comMainhis);
            try
            {
                con.Open();
                adapMainhis.Fill(toReturn);
                //return toReturn;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("HResult " + ex.HResult + "\n" + "Message" + ex.Message + "\n" + sql, "Error");
                if (con.State == ConnectionState.Open)
                {
                    //con.Close();
                }
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
                comMainhis.Dispose();
                adapMainhis.Dispose();
            }
            return toReturn;
        }
        public DataTable selectData(String sql)
        {
            DataTable toReturn = new DataTable();
            
            SqlCommand comMainhis = new SqlCommand();
            comMainhis.CommandText = sql;
            comMainhis.CommandType = CommandType.Text;
            comMainhis.Connection = connMainHIS;
            comMainhis.CommandTimeout = 60;
            SqlDataAdapter adapMainhis = new SqlDataAdapter(comMainhis);
            try
            {
                if (connMainHIS.State == ConnectionState.Open)
                {
                    connMainHIS.Close();
                }
                connMainHIS.Open();
                adapMainhis.Fill(toReturn);
                //return toReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                connMainHIS.Close();
                comMainhis.Dispose();
                adapMainhis.Dispose();
            }

            return toReturn;
        }
        public DataTable selectData(SqlConnection con, String sql)
        {
            DataTable toReturn = new DataTable();

            SqlCommand comMainhis = new SqlCommand();
            comMainhis.CommandText = sql;
            comMainhis.CommandType = CommandType.Text;
            comMainhis.Connection = con;
            comMainhis.CommandTimeout = 60;
            SqlDataAdapter adapMainhis = new SqlDataAdapter(comMainhis);
            try
            {
                //new LogWriter("e", "ConnectDB selectData con.ConnectionString " + con.ConnectionString);

                con.Open();
                adapMainhis.Fill(toReturn);
                //return toReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
                comMainhis.Dispose();
                adapMainhis.Dispose();
            }
            return toReturn;
        }
        public DataTable selectData(NpgsqlConnection con, String sql)
        {
            DataTable toReturn = new DataTable();

            NpgsqlCommand comMainhis = new NpgsqlCommand();
            comMainhis.CommandText = sql;
            comMainhis.CommandType = CommandType.Text;
            comMainhis.Connection = con;
            NpgsqlDataAdapter adapMainhis = new NpgsqlDataAdapter(comMainhis);
            try
            {
                //new LogWriter("e", "ConnectDB selectData con.ConnectionString " + con.ConnectionString);

                con.Open();
                adapMainhis.Fill(toReturn);
                //return toReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
                comMainhis.Dispose();
                adapMainhis.Dispose();
            }
            return toReturn;
        }
        public object ExecuteScalar(string sql)
        {
            // forward to the connection-specific overload
            return ExecuteScalar(this.connMySQL, sql, null);
        }

        public object ExecuteScalar(MySqlConnection connection, string sql)
        {
            return ExecuteScalar(connection, sql, null);
        }
        public object ExecuteScalar(MySqlConnection connection, string sql, Dictionary<string, object> parameters)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrWhiteSpace(sql)) return null;
            try
            {
                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.Text;

                    if (parameters != null)
                    {
                        foreach (var kv in parameters)
                        {
                            var paramName = kv.Key.StartsWith("@") ? kv.Key : "@" + kv.Key;
                            cmd.Parameters.AddWithValue(paramName, kv.Value ?? DBNull.Value);
                        }
                    }
                    var mustClose = false;
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                        mustClose = true;
                    }
                    var result = cmd.ExecuteScalar();
                    if (mustClose) connection.Close();
                    return result == DBNull.Value ? null : result;
                }
            }
            catch (Exception ex)
            {
                // Use your project's logging helper if available:
                // new LogWriter("e", "ConnectDB.ExecuteScalar " + ex.Message);
                return null;
            }
        }
    }
}