using bangna_hospital.objdb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class QueueDB
    {
        public Queue dgs;
        ConnectDB conn;
        public List<Queue> lDgs;

        public QueueDB(ConnectDB c)
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            dgs = new Queue();
            lDgs = new List<Queue>();

            dgs.queue_id = "queue_id";
            dgs.queue_type_id = "queue_type_id";
            dgs.queue_seq = "queue_seq";
            dgs.queue_date = "queue_date";

            dgs.table = "b_queue";
            dgs.pkField = "queue_id";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dgs.table + " dgs " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                //" Where dgs." + dgs.active + " ='1' " +
                "Order By queue_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectQueue(String quedate, String quetypeid)
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dgs.table + " dgs " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                " Where dgs." + dgs.queue_type_id + " ='"+ quetypeid + "' and dgs." + dgs.queue_date + " ='" + quedate + "' " +
                "Order By queue_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public Queue selectQueueNext(String quedate, String quetypeid)
        {
            DataTable dt = new DataTable();
            Queue queue = new Queue();
            int cnt = 0;
            String re = "";
            String sql = "select * " +
                "From " + dgs.table + " dgs " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                " Where dgs." + dgs.queue_type_id + " ='" + quetypeid + "' and dgs." + dgs.queue_date + " ='" + quedate + "' " +
                "Order By queue_id ";
            dt = conn.selectData(conn.conn, sql);
            if (dt.Rows.Count > 0)
            {
                //re = dt.Rows[0]["cnt"].ToString();

                queue = setDocGroupScan(dt);
            }
            else
            {
                queue.queue_id = "";
                queue.queue_type_id = quetypeid;
                queue.queue_seq = "0";
                queue.queue_date = quedate;
                insertQueue(queue, "");
                dt = selectQueue(quedate, quetypeid);
                queue = setDocGroupScan(dt);
                re = "1";
            }
            int.TryParse(queue.queue_seq, out cnt);
            queue.queue_seq = (cnt+1).ToString();
            return queue;
        }
        public String insert(Queue p, String userId)
        {
            String re = "";
            String sql = "";
            p.active = "1";
            //p.ssdata_id = "";
            int chk = 0;

            sql = "Insert Into " + dgs.table + " (" + dgs.queue_type_id + "," + dgs.queue_seq + "," + dgs.queue_date + "" +
               ") " +
                "Values ('" + p.queue_type_id + "',0,'" + p.queue_date + "'" +
                ")";
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert error  " + ex.Message + " " + ex.InnerException);
            }

            return re;
        }
        public String update(Queue p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dgs.table + " Set " +
                " " + dgs.queue_seq + " = " + p.queue_seq + " " +
                "Where " + dgs.pkField + "='" + p.queue_id + "'"
                ;

            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "update error  " + ex.Message + " " + ex.InnerException);
            }

            return re;
        }
        public String insertQueue(Queue p, String userId)
        {
            String re = "";

            if (p.queue_id.Equals(""))
            {
                re = insert(p, "");
            }
            else
            {
                re = update(p, "");
            }

            return re;
        }
        public Queue setDocGroupScan(DataTable dt)
        {
            Queue dgs1 = new Queue();
            if (dt.Rows.Count > 0)
            {
                dgs1.queue_id = dt.Rows[0][dgs.queue_id].ToString();
                dgs1.queue_type_id = dt.Rows[0][dgs.queue_type_id].ToString();
                dgs1.queue_seq = dt.Rows[0][dgs.queue_seq].ToString();
                dgs1.queue_date = dt.Rows[0][dgs.queue_date].ToString();
                
            }
            else
            {
                setDocGroupScan(dgs1);
            }
            return dgs1;
        }
        public Queue setDocGroupScan(Queue dgs1)
        {
            dgs1.queue_id = "";
            dgs1.queue_type_id = "";
            dgs1.queue_seq = "";
            dgs1.queue_date = "";
            
            return dgs1;
        }
    }
}
