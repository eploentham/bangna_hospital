using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LisLabRequestDB
    {
        public LisLabRequest labreq;
        ConnectDB conn;
        public LisLabRequestDB(ConnectDB c)
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labreq = new LisLabRequest();
            labreq.lab_request_id = "lab_request_id";
            labreq.lab_request_lon = "lab_request_lon";
            labreq.lab_request_msg_type = "lab_request_msg_type";
            labreq.lab_request_data = "lab_request_data";
            labreq.lab_request_datetime = "lab_request_datetime";
            labreq.lab_request_receive = "lab_request_receive";
            labreq.lab_request_receive_datetime = "lab_request_receive_datetime";
            labreq.lab_request_receive_data = "lab_request_receive_data";
            labreq.hn = "hn";
            labreq.req_date = "req_date";
            labreq.req_no = "req_no";

            labreq.table = "lab_request";
            labreq.pkField = "lab_request_id";
        }
        public DataTable selectByVnDocScan(String hn, String vn, String vsDate)
        {
            LisLabRequest cop1 = new LisLabRequest();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + labreq.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + labreq.lab_request_id + " ='" +
                "Order By sort1 ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        private void chkNull(LisLabRequest p)
        {
            long chk = 0;
            int chk1 = 0;

            p.lab_request_lon = p.lab_request_lon == null ? "" : p.lab_request_lon;
            p.lab_request_msg_type = p.lab_request_msg_type == null ? "" : p.lab_request_msg_type;
            p.lab_request_data = p.lab_request_data == null ? "" : p.lab_request_data;
            p.lab_request_datetime = p.lab_request_datetime == null ? "" : p.lab_request_datetime;
            p.lab_request_receive = p.lab_request_receive == null ? "" : p.lab_request_receive;
            p.lab_request_receive_datetime = p.lab_request_receive_datetime == null ? "" : p.lab_request_receive_datetime;
            p.lab_request_receive_data = p.lab_request_receive_data == null ? "" : p.lab_request_receive_data;
        }
        public String insert(LisLabRequest p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            chkNull(p);
            try
            {
                sql = "Insert Into " + labreq.table + " (" + labreq.lab_request_lon + "," + labreq.lab_request_msg_type + "," + labreq.lab_request_data + "" +
                    ","+ labreq.lab_request_datetime + ","+ labreq.lab_request_receive + ","+ labreq.lab_request_receive_datetime +
                    ","+ labreq.lab_request_receive_data + "," + labreq.hn+"," + labreq.req_date+"," + labreq.req_no+""+
                    ") " +
                "Values ('" + p.lab_request_lon + "','"+ p.lab_request_msg_type + "','" + p.lab_request_data + "'" +
                    ",'"+ p.lab_request_datetime + "','"+ p.lab_request_receive + "','"+ p.lab_request_receive_datetime + "'"+
                    ",'"+ p.lab_request_receive_data + "','"+ p.hn + "','" + p.req_date + "','" + p.req_no + "'" +
                    ")";
                re = conn.ExecuteNonQuery(conn.connLinkLIS, sql);
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
        public void updateLabMaster()
        {
            //String re = "";
            String sql = "";
            int chk = 0;
            DataTable dt = new DataTable();
            DataTable dtlis = new DataTable();
            try
            {
                sql = "Select isnull(labm04.MNC_LB_CD,'') as MNC_LB_CD, isnull(labm04.MNC_LB_RES_CD,'') as MNC_LB_RES_CD, labm04.MNC_LB_RES, isnull(labm01.MNC_LB_DSC,'') as MNC_LB_DSC " +
                    "From LAB_M01 labm01 " +
                    "inner join LAB_M04 labm04 on labm01.MNC_LB_CD = labm04.MNC_LB_CD Order By labm04.MNC_LB_CD ";
                dt = conn.selectData(conn.connMainHIS, sql);
                if (dt.Rows.Count > 0)
                {
                    foreach(DataRow drow in dt.Rows)
                    {
                        sql = "Select lab_code, lab_sub_code From lab_master Where lab_code = '" + drow["MNC_LB_CD"].ToString()+ "' and lab_sub_code = '"+ drow["MNC_LB_RES_CD"].ToString()+"' ";
                        dtlis = conn.selectData(conn.connLinkLIS, sql);
                        if (dtlis.Rows.Count <= 0)
                        {
                            String re = "";
                            sql = "insert into lab_master(lab_code, lab_name, lab_sub_code, lab_sub_name, status_new) " +
                                "values('"+ drow["MNC_LB_CD"].ToString() + "','" + drow["MNC_LB_DSC"].ToString().Replace("'","''") + "','" + drow["MNC_LB_RES_CD"].ToString() + "','" + drow["MNC_LB_RES"].ToString().Replace("'", "''") + "','1')";
                            re = conn.ExecuteNonQuery(conn.connLinkLIS, sql);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert LisLabRequest " + sql);
            }
            finally
            {

            }
            //return re;
        }
        public LisLabRequest setLisLabRequest(DataTable dt)
        {
            LisLabRequest dgs1 = new LisLabRequest();
            if (dt.Rows.Count > 0)
            {
                dgs1.lab_request_id = dt.Rows[0][labreq.lab_request_id].ToString();
                dgs1.lab_request_lon = dt.Rows[0][labreq.lab_request_lon].ToString();
                dgs1.lab_request_msg_type = dt.Rows[0][labreq.lab_request_msg_type].ToString();
                dgs1.lab_request_data = dt.Rows[0][labreq.lab_request_data].ToString();
                dgs1.lab_request_datetime = dt.Rows[0][labreq.lab_request_datetime].ToString();
                dgs1.lab_request_receive = dt.Rows[0][labreq.lab_request_receive].ToString();
                dgs1.lab_request_receive_datetime = dt.Rows[0][labreq.lab_request_receive_datetime].ToString();
                dgs1.lab_request_receive_data = dt.Rows[0][labreq.lab_request_receive_data].ToString();
            }
            else
            {
                setLisLabRequest(dgs1);
            }
            return dgs1;
        }
        public LisLabRequest setLisLabRequest(LisLabRequest dgs1)
        {
            dgs1.lab_request_id = "";
            dgs1.lab_request_lon = "";
            dgs1.lab_request_msg_type = "";
            dgs1.lab_request_data = "";
            dgs1.lab_request_datetime = "";
            dgs1.lab_request_receive = "";
            dgs1.lab_request_receive_datetime = "";
            dgs1.lab_request_receive_data = "";

            return dgs1;
        }
    }
}
