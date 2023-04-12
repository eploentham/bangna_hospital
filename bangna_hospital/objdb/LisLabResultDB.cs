using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LisLabResultDB
    {
        public LisLabResult labres;
        ConnectDB conn;
        public LisLabResultDB(ConnectDB c)
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labres = new LisLabResult();
            labres.lab_result_id = "lab_result_id";
            labres.lab_result_lon = "lab_result_lon";
            labres.lab_result_msg_type = "lab_result_msg_type";
            labres.lab_result_data = "lab_result_data";
            labres.lab_result_datatype_id = "lab_result_datatype_id";
            labres.lab_result_note = "lab_result_note";
            labres.lab_result_datetime = "lab_result_datetime";
            labres.lab_result_receive = "lab_result_receive";
            labres.lab_result_receive_datetime = "lab_result_receive_datetime";
            labres.lab_result_receive_data = "lab_result_receive_data";
            labres.hn = "hn";
            labres.req_date = "req_date";
            labres.req_no = "req_no";

            labres.table = "lab_result";
            labres.pkField = "lab_result_id";
        }
        public DataTable selectNoSendByStatusLis()
        {
            LisLabResult cop1 = new LisLabResult();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + labres.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + labres.lab_result_receive + " ='N' " +
                "Order By lab_result_id ";
            dt = conn.selectData(conn.connLinkLIS, sql);

            return dt;
        }
        private void chkNull(LisLabResult p)
        {
            long chk = 0;
            int chk1 = 0;

            p.lab_result_lon = p.lab_result_lon == null ? "" : p.lab_result_lon;
            p.lab_result_msg_type = p.lab_result_msg_type == null ? "" : p.lab_result_msg_type;
            p.lab_result_data = p.lab_result_data == null ? "" : p.lab_result_data;
            p.lab_result_datatype_id = p.lab_result_datatype_id == null ? "" : p.lab_result_datatype_id;
            p.lab_result_note = p.lab_result_note == null ? "" : p.lab_result_note;
            p.lab_result_datetime = p.lab_result_datetime == null ? "" : p.lab_result_datetime;
            p.lab_result_receive = p.lab_result_receive == null ? "" : p.lab_result_receive;
            p.lab_result_receive_datetime = p.lab_result_receive_datetime == null ? "" : p.lab_result_receive_datetime;
            p.lab_result_receive_data = p.lab_result_receive_data == null ? "" : p.lab_result_receive_data;
            p.hn = p.hn == null ? "" : p.hn;
            p.req_date = p.req_date == null ? "" : p.req_date;
            p.req_no = p.req_no == null ? "" : p.req_no;
        }
        public String updateHnReqDate(String hn, String reqdate, String reqno, String resultid)
        {
            String re = "", sql="";
            try
            {
                sql = "Update " + labres.table + " Set hn = '" + hn + "' , req_date = '" + reqdate + "', req_no = '" + reqno + "' Where lab_result_id = '" + resultid + "' ";
                re = conn.ExecuteNonQuery(conn.connLinkLIS, sql);
            }
            catch(Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateHnReqDate LisLabResultDB " + sql);
            }
            
            return re;
        }
        public String updateStatusReceive(String hn, String reqdate, String reqno, String resultid)
        {
            String re = "", sql = "";
            try
            {
                sql = "Update " + labres.table + " Set hn = '" + hn + "' , req_date = '" + reqdate + "', req_no = '" + reqno + "', lab_result_receive = 'Y' Where lab_result_id = '" + resultid + "' ";
                re = conn.ExecuteNonQuery(conn.connLinkLIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateStatusReceive LisLabResultDB " + sql);
            }

            return re;
        }
        public String insert(LisLabResult p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            chkNull(p);
            try
            {
                sql = "Insert Into " + labres.table + " (" + labres.lab_result_lon + "," + labres.lab_result_msg_type + "," + labres.lab_result_data + "" +
                    "," + labres.lab_result_datatype_id + "," + labres.lab_result_note + "," + labres.lab_result_datetime +
                    "," + labres.lab_result_receive + "," + labres.lab_result_receive_datetime + ","+ labres.lab_result_receive_data+
                    "," + labres.hn + "," + labres.req_date + "," + labres.req_no +
                    ") " +
                "Values ('" + p.lab_result_lon + "','" + p.lab_result_msg_type + "','" + p.lab_result_data + "'" +
                    ",'" + p.lab_result_datatype_id + "','" + p.lab_result_note + "','" + p.lab_result_datetime + "'" +
                    ",'" + p.lab_result_receive + "','"+ p.lab_result_receive_datetime + "','"+ p.lab_result_receive_data + "'"+
                    ",'" + p.hn + "','" + p.req_date + "','" + p.req_no + "'" +
                    ")";
                re = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert LisLabRequest " + sql);
            }
            finally
            {

            }
            return re;
        }
        public LisLabResult setLisLabRequest(DataTable dt)
        {
            LisLabResult dgs1 = new LisLabResult();
            if (dt.Rows.Count > 0)
            {
                dgs1.lab_result_id = dt.Rows[0][labres.lab_result_id].ToString();
                dgs1.lab_result_lon = dt.Rows[0][labres.lab_result_lon].ToString();
                dgs1.lab_result_msg_type = dt.Rows[0][labres.lab_result_msg_type].ToString();
                dgs1.lab_result_data = dt.Rows[0][labres.lab_result_data].ToString();
                dgs1.lab_result_datatype_id = dt.Rows[0][labres.lab_result_datatype_id].ToString();
                dgs1.lab_result_note = dt.Rows[0][labres.lab_result_note].ToString();
                dgs1.lab_result_datetime.ToString();
                dgs1.lab_result_receive = dt.Rows[0][labres.lab_result_receive].ToString();
                dgs1.lab_result_receive_datetime = dt.Rows[0][labres.lab_result_receive_datetime].ToString();
                dgs1.lab_result_receive_data = dt.Rows[0][labres.lab_result_receive_data].ToString();
                dgs1.hn = dt.Rows[0][labres.hn].ToString();
                dgs1.req_date = dt.Rows[0][labres.req_date].ToString();
                dgs1.req_no = dt.Rows[0][labres.req_no].ToString();
            }
            else
            {
                setLisLabRequest(dgs1);
            }
            return dgs1;
        }
        public LisLabResult setLisLabRequest(LisLabResult dgs1)
        {
            dgs1.lab_result_id = "";
            dgs1.lab_result_lon = "";
            dgs1.lab_result_msg_type = "";
            dgs1.lab_result_data = "";
            dgs1.lab_result_datatype_id = "";
            dgs1.lab_result_note = "";
            dgs1.lab_result_datetime = "";
            dgs1.lab_result_receive = "";
            dgs1.lab_result_receive_datetime = "";
            dgs1.lab_result_receive_data = "";
            dgs1.hn = "";
            dgs1.req_no = "";
            dgs1.req_date = "";
            return dgs1;
        }
    }
}
