using bangna_hospital.object1;
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
        public SqlConnection connMainHIS, conn, connPACs, connLabOut;
        public Staff user;
        public long _rowsAffected = 0;
        public SqlCommand comStore;
        public ConnectDB(InitConfig initc)
        {
            conn = new SqlConnection();
            connMainHIS = new SqlConnection();
            connPACs = new SqlConnection();
            connLabOut = new SqlConnection();
            connMainHIS.ConnectionString = "Server=" + initc.hostDBMainHIS + ";Database=" + initc.nameDBMainHIS + ";Uid=" + initc.userDBMainHIS + ";Pwd=" + initc.passDBMainHIS + ";";
            conn.ConnectionString = "Server=" + initc.hostDB + ";Database=" + initc.nameDB + ";Uid=" + initc.userDB + ";Pwd=" + initc.passDB + ";";
            connLabOut.ConnectionString = "Server=" + initc.hostDBLabOut + ";Database=" + initc.nameDBLabOut + ";Uid=" + initc.userDBLabOut + ";Pwd=" + initc.passDBLabOut + ";";
            connPACs.ConnectionString = "Server=" + initc.hostDBPACs + ";Database=" + initc.nameDBPACs + ";Uid=" + initc.userDBPACs + ";Pwd=" + initc.passDBPACs + ";";
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
        public DataTable selectData(String sql)
        {
            DataTable toReturn = new DataTable();
            
            SqlCommand comMainhis = new SqlCommand();
            comMainhis.CommandText = sql;
            comMainhis.CommandType = CommandType.Text;
            comMainhis.Connection = connMainHIS;
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
        public DataTable selectData(SqlConnection con, String sql)
        {
            DataTable toReturn = new DataTable();

            SqlCommand comMainhis = new SqlCommand();
            comMainhis.CommandText = sql;
            comMainhis.CommandType = CommandType.Text;
            comMainhis.Connection = con;
            SqlDataAdapter adapMainhis = new SqlDataAdapter(comMainhis);
            try
            {
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
    }
}
