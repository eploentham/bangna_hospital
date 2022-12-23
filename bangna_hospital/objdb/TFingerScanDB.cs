using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class TFingerScanDB
    {
        public ConnectDB conn;
        public TFingerScan fscan;
        public TFingerScanDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            fscan = new TFingerScan();
            fscan.finger_image = "finger_image";
            fscan.finger_index = "finger_index";
            fscan.finger_scan = "finger_scan";
            fscan.hn = "hn";

            fscan.pkField = "finger_scan";
            fscan.table = "t_finger_scan";
        }
        public String insertFingerScan(String hn, String fingerindex, byte[] image)
        {
            String sql = "", re = "";
            try
            {
                string str = Encoding.Default.GetString(image);
                sql = "delete from "+ fscan.table + " where hn = '" + hn + "' and "+ fscan.finger_index + " = '" + fingerindex + "' ";
                conn.ExecuteNonQuery(conn.connMainHIS, sql);
                sql = "insert into " + fscan.table + " (" + fscan.hn + ", "+ fscan.finger_index + ", "+ fscan.finger_image + ") values(" +
                    "'" + hn + "','" + fingerindex + "',@photo" +
                    ")";
                re = conn.ExecuteNonQueryImage(conn.connMainHIS, sql, image);
            }
            catch (Exception ex)
            {
                re = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert Patient sql " + sql + " ex " + re);
            }
            return re;
        }
        public DataTable checkFingerScan(String fingerindex, byte[] image)
        {
            String re = "",sql="";
            DataTable dt = new DataTable();
            try
            {
                sql = "Select * From " + fscan.table + " where " + fscan.finger_index + " = '" + fingerindex + "' and "+fscan.finger_image+" = "+ image;
                dt = conn.selectData(conn.connMainHIS, sql);
            }
            catch(Exception ex)
            {
                re = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert Patient sql " + sql + " ex " + re);
            }
            return dt;
        }
        public DataTable selectAll()
        {
            String re = "", sql = "";
            DataTable dt = new DataTable();
            try
            {
                sql = "Select * From " + fscan.table + "  ";
                dt = conn.selectData(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                re = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert Patient sql " + sql + " ex " + re);
            }
            return dt;
        }
    }
}
