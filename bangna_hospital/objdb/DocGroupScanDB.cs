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
    public class DocGroupScanDB
    {
        public DocGroupScan dgs;
        ConnectDB conn;
        public List<DocGroupScan> lDgs;

        public DocGroupScanDB(ConnectDB c)
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            dgs = new DocGroupScan();
            lDgs = new List<DocGroupScan>();

            dgs.active = "active";
            dgs.doc_group_name = "doc_group_name";
            dgs.doc_group_id = "doc_group_id";
            dgs.remark = "remark";
            dgs.status_opd = "status_opd";

            dgs.table = "doc_group_scan";
            dgs.pkField = "doc_group_id";
        }
        public void getlDgs()
        {
            //lDept = new List<Position>();

            lDgs.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            foreach (DataRow row in dt.Rows)
            {
                DocGroupScan itm1 = new DocGroupScan();
                itm1.active = row[dgs.active].ToString();
                itm1.doc_group_name = row[dgs.doc_group_name].ToString();
                itm1.doc_group_id = row[dgs.doc_group_id].ToString();
                itm1.remark = row[dgs.remark].ToString();
                itm1.status_opd = row[dgs.status_opd].ToString();

                //itm1.is_ipd = row[bsp.is_ipd].ToString();
                lDgs.Add(itm1);
            }
        }
        public String getIdDgs(String name)
        {
            String re = "";
            foreach (DocGroupScan row in lDgs)
            {
                if (row.doc_group_name.Trim().Equals(name.Trim()))
                {
                    re = row.doc_group_id;
                    break;
                }
            }
            return re;
        }
        public String getNameDgs(String id)
        {
            String re = "";
            foreach (DocGroupScan row in lDgs)
            {
                if (row.doc_group_id.Trim().Equals(id.Trim()))
                {
                    re = row.doc_group_name;
                    break;
                }
            }
            return re;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From "+ dgs.table +" dgs "+
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                " Where dgs." + dgs.active + " ='1' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);
            
            return dt;
        }

        public DocGroupScan selectByPk(String id)
        {
            DocGroupScan cop1 = new DocGroupScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dgs.table +" dgs "+
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dgs." + dgs.pkField + " ='" + id + "' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);
            cop1 = setDocGroupScan(dt);
            return cop1;
        }
        public DataTable selectByPk1(String id)
        {
            DocGroupScan cop1 = new DocGroupScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dgs.table + " dgs " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dgs." + dgs.pkField + " ='"+id+"' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public String insert(DocGroupScan p, String userId)
        {
            String re = "";
            String sql = "";
            p.active = "1";
            //p.ssdata_id = "";
            int chk = 0;
            
            sql = "Insert Into " + dgs.table + " (" + dgs.doc_group_name + ","+dgs.active+","+dgs.status_opd + "" +
               ") " +
                "Values ('"+p.doc_group_name.Replace("'", "''") + "','1','" +p.status_opd+"'"+
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
        public String update(DocGroupScan p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
                        
            sql = "Update " + dgs.table + " Set " +
                " " + dgs.doc_group_name + " = '" + p.doc_group_name.Replace("'", "''") + "'" +
                "," + dgs.status_opd + " = '" + p.status_opd.Replace("'", "''") + "'" +

                "Where " + dgs.pkField + "='" + p.doc_group_id + "'"
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
        public String insertDocGroupScan(DocGroupScan p, String userId)
        {
            String re = "";

            if (p.doc_group_id.Equals(""))
            {
                re = insert(p, "");
            }
            else
            {
                re = update(p, "");
            }

            return re;
        }
        public DocGroupScan setDocGroupScan(DataTable dt)
        {
            DocGroupScan dgs1 = new DocGroupScan();
            if (dt.Rows.Count > 0)
            {
                dgs1.doc_group_id = dt.Rows[0][dgs.doc_group_id].ToString();
                dgs1.doc_group_name = dt.Rows[0][dgs.doc_group_name].ToString();
                dgs1.remark = dt.Rows[0][dgs.remark].ToString();
                dgs1.active = dt.Rows[0][dgs.active].ToString();
                dgs1.status_opd = dt.Rows[0][dgs.status_opd].ToString();
            }
            else
            {
                setDocGroupScan(dgs1);
            }
                return dgs1;
        }
        public DocGroupScan setDocGroupScan(DocGroupScan dgs1)
        {
            dgs1.active = "";
            dgs1.remark = "";
            dgs1.doc_group_name = "";
            dgs1.doc_group_id = "";
            dgs1.status_opd = "";
            return dgs1;
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
            foreach (DocGroupScan cus1 in lDgs)
            {
                item = new ComboBoxItem();
                item.Value = cus1.doc_group_id;
                item.Text = cus1.doc_group_name;
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
