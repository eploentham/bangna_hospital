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
        public SqlConnection connMainHIS, conn;
        public Staff user;

        public ConnectDB(InitConfig initc)
        {
            conn = new SqlConnection();
            connMainHIS = new SqlConnection();
            connMainHIS.ConnectionString = "Server=" + initc.hostDBMainHIS + ";Database=" + initc.nameDBMainHIS + ";Uid=" + initc.userDBMainHIS + ";Pwd=" + initc.passDBMainHIS + ";";
            conn.ConnectionString = "Server=" + initc.hostDB + ";Database=" + initc.nameDB + ";Uid=" + initc.userDB + ";Pwd=" + initc.passDB + ";";
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
