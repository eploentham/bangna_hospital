using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class DocGroupSubScanDB
    {
        public DocGroupSubScan dgss;
        ConnectDB conn;
        public List<DocGroupSubScan> lDgss;

        public DocGroupSubScanDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            dgss = new DocGroupSubScan();
            lDgss = new List<DocGroupSubScan>();

            dgss.doc_group_sub_id = "doc_group_sub_id";
            dgss.active = "active";
            dgss.doc_group_sub_name = "doc_group_sub_name";
            dgss.doc_group_id = "doc_group_id";
            dgss.remark = "remark";

            dgss.table = "doc_group_sub_scan";
            dgss.pkField = "doc_group_sub_id";
        }
        public void getlBsp()
        {
            //lDept = new List<Position>();

            lDgss.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            foreach (DataRow row in dt.Rows)
            {
                DocGroupSubScan itm1 = new DocGroupSubScan();
                itm1.active = row[dgss.active].ToString();
                itm1.doc_group_sub_name = row[dgss.doc_group_sub_name].ToString();
                itm1.doc_group_id = row[dgss.doc_group_id].ToString();
                itm1.remark = row[dgss.remark].ToString();

                itm1.doc_group_sub_id = row[dgss.doc_group_sub_id].ToString();
                lDgss.Add(itm1);
            }
        }
        public String getDgsIdDgss(String name)
        {
            String re = "";
            foreach (DocGroupSubScan row in lDgss)
            {
                if (row.doc_group_sub_name.Trim().Equals(name.Trim()))
                {
                    re = row.doc_group_id;
                    break;
                }
            }
            return re;
        }
        public String getIdDgss(String name)
        {
            String re = "";
            foreach (DocGroupSubScan row in lDgss)
            {
                if (row.doc_group_sub_name.Trim().Equals(name.Trim()))
                {
                    re = row.doc_group_sub_id;
                    break;
                }
            }
            return re;
        }
        public String getNameDgss(String id)
        {
            String re = "";
            foreach (DocGroupSubScan row in lDgss)
            {
                if (row.doc_group_sub_id.Trim().Equals(id.Trim()))
                {
                    re = row.doc_group_sub_name;
                    break;
                }
            }
            return re;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select dgss.*, dgs.doc_group_name " +
                "From " + dgss.table + " dgss " +
                "Left Join doc_group_scan dgs On dgs.doc_group_id = dgss.doc_group_id " +
                " Where dgss." + dgss.active + " ='1' " +
                "Order By dgss.doc_group_id, dgss.doc_group_sub_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }

        public DocGroupSubScan selectByPk(String id)
        {
            DocGroupSubScan cop1 = new DocGroupSubScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dgss.table + " dgss " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dgss." + dgss.pkField + " ='" + id + "' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);
            cop1 = setDocGroupSubScan(dt);
            return cop1;
        }
        public DataTable selectByPk1(String id)
        {
            DocGroupSubScan cop1 = new DocGroupSubScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dgss.table + " dgss " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dgss." + dgss.pkField + " ='" + id + "' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public String insert(DocGroupSubScan p, String userId)
        {
            String re = "";
            String sql = "";
            p.active = "1";
            //p.ssdata_id = "";
            int chk = 0;

            sql = "Insert Into " + dgss.table + " (" + dgss.doc_group_sub_name + "," + dgss.active + ","+dgss.doc_group_id +
                ") " +
                "Values ('" + p.doc_group_sub_name.Replace("'","''") + "','1','" +p.doc_group_id+"' "+
                ")";
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }

            return re;
        }
        public String update(DocGroupSubScan p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dgss.table + " Set " +
                " " + dgss.doc_group_sub_name + " = '" + p.doc_group_sub_name.Replace("'", "''") + "'" +

                "Where " + dgss.pkField + "='" + p.doc_group_sub_id + "'"
                ;

            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }

            return re;
        }
        public String insertDocGroupSubScan(DocGroupSubScan p, String userId)
        {
            String re = "";

            if (p.doc_group_sub_id.Equals(""))
            {
                re = insert(p, "");
            }
            else
            {
                re = update(p, "");
            }

            return re;
        }
        public DocGroupSubScan setDocGroupSubScan(DataTable dt)
        {
            DocGroupSubScan dgs1 = new DocGroupSubScan();
            if (dt.Rows.Count > 0)
            {
                dgs1.doc_group_sub_id = dt.Rows[0][dgss.doc_group_sub_id].ToString();
                dgs1.doc_group_id = dt.Rows[0][dgss.doc_group_id].ToString();
                dgs1.doc_group_sub_name = dt.Rows[0][dgss.doc_group_sub_name].ToString();
                dgs1.remark = dt.Rows[0][dgss.remark].ToString();
                dgs1.active = dt.Rows[0][dgss.active].ToString();
            }
            else
            {
                setDocGroupSubScan(dgs1);
            }
            return dgs1;
        }
        public DocGroupSubScan setDocGroupSubScan(DocGroupSubScan dgs1)
        {
            dgs1.active = "";
            dgs1.remark = "";
            dgs1.doc_group_sub_name = "";
            dgs1.doc_group_id = "";
            dgs1.doc_group_sub_id = "";
            return dgs1;
        }
        public void setCboBsp(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectAll();
            int i = 0;
            if (lDgss.Count <= 0) getlBsp();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (DocGroupSubScan cus1 in lDgss)
            {
                item = new ComboBoxItem();
                item.Value = cus1.doc_group_sub_id;
                item.Text = cus1.doc_group_sub_name;
                c.Items.Add(item);
                if (item.Value.Equals(selected))
                {
                    //c.SelectedItem = item.Value;
                    c.SelectedText = item.Text;
                    c.SelectedIndex = i + 1;
                }
                i++;
            }
        }
    }
}
