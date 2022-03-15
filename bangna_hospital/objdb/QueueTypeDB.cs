using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class QueueTypeDB
    {
        public ConnectDB conn;
        public QueueType queueType;
        public List<QueueType> lDgs;
        public QueueTypeDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            lDgs = new List<QueueType>();
            queueType = new QueueType();
            queueType.queue_type_id = "queue_type_id";
            queueType.queue_type_name = "queue_type_name";

            queueType.table = "b_queue_type";
            queueType.pkField = "queue_type_id";
        }
        public void getlDgs()
        {
            //lDept = new List<Position>();

            lDgs.Clear();
            DataTable dt = new DataTable();
            //new LogWriter("d", "QueueTypeDB getlDgs  00");
            dt = selectAll();
            foreach (DataRow row in dt.Rows)
            {
                QueueType itm1 = new QueueType();
                itm1.queue_type_id = row[queueType.queue_type_id].ToString();
                itm1.queue_type_name = row[queueType.queue_type_name].ToString();
                lDgs.Add(itm1);
            }
        }
        public String getIdDgs(String name)
        {
            String re = "";
            foreach (QueueType row in lDgs)
            {
                if (row.queue_type_name.Trim().Equals(name.Trim()))
                {
                    re = row.queue_type_id;
                    break;
                }
            }
            return re;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select labB.* " +
                "From " + queueType.table + " labB " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                //"Where labB." + queueType.active + "='1'  " +
                "Order By labB." + queueType.queue_type_name;
            //new LogWriter("d", "QueueTypeDB selectAll  "+sql+ " conn.conn.ConnectionString " + conn.conn.ConnectionString);
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public void setCboDgs(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectAll();
            int i = 0;
            if (lDgs.Count <= 0) getlDgs();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (QueueType cus1 in lDgs)
            {
                item = new ComboBoxItem();
                item.Value = cus1.queue_type_id;
                item.Text = cus1.queue_type_name;
                c.Items.Add(item);
                if (item.Value.Equals(selected))
                {
                    //c.SelectedItem = item.Value;
                    c.SelectedText = item.Text;
                    c.SelectedIndex = i + 1;
                }
                i++;
            }
            if (c.SelectedIndex <= 0)
            {
                if (c.Items.Count >= 1)
                {
                    c.SelectedIndex = 1;
                }
            }
        }
    }
}
