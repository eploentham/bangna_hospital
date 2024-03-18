using bangna_hospital.object1;
using C1.Util.DX.Direct3D11;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class DrugSetDB
    {
        public ConnectDB conn;
        public DrugSet drugs;
        public List<DrugSet> lDgs;
        public DrugSetDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            drugs = new DrugSet();

            drugs.drug_set_id = "drug_set_id";
            drugs.doctor_id = "doctor_id";
            drugs.drug_set_name = "drug_set_name";
            drugs.item_code = "item_code";
            drugs.item_name = "item_name";
            drugs.status_item = "status_item";
            drugs.qty = "qty";
            drugs.using1 = "using1";
            drugs.frequency = "frequency";
            drugs.precautions = "precautions";
            drugs.interaction = "interaction";
            drugs.indication = "indication";
            drugs.active = "active";
            drugs.user_cancel = "user_cancel";
            drugs.pkField = "drug_set_id";
            drugs.table = "b_drug_set";

            lDgs = new List<DrugSet>();
        }
        public DataTable selectDrugSet(String dtrcode)
        {
            DataTable dt = new DataTable();
            String sql = "", wherehn = "", wherename = "";

            sql = "Select * " +
                "From  b_drug_set  " +
                " Where doctor_id = '" + dtrcode + "'";
            dt = conn.selectData(conn.conn, sql);
            return dt;
        }
        public DataTable selectDrugSet(String dtrcode, String drugsetname)
        {
            DataTable dt = new DataTable();
            String sql = "", wherehn = "", wherename = "";

            sql = "Select * " +
                "From  b_drug_set  " +
                " Where doctor_id = '" + dtrcode + "' and drug_set_name = '"+drugsetname+"' and active = '1' ";
            dt = conn.selectData(conn.conn, sql);
            return dt;
        }
        public DataTable selectDrugSetGrougName(String dtrcode)
        {
            DataTable dt = new DataTable();
            String sql = "", wherehn = "", wherename = "";

            sql = "Select drug_set_name " +
                "From  b_drug_set  " +
                " Where doctor_id = '" + dtrcode + "' Group By drug_set_name ";
            dt = conn.selectData(conn.conn, sql);
            return dt;
        }
        private void chkNull(DrugSet p)
        {
            long chk = 0;

            p.doctor_id = p.doctor_id == null ? "" : p.doctor_id;
            p.drug_set_name = p.drug_set_name == null ? "" : p.drug_set_name;
            p.item_code = p.item_code == null ? "" : p.item_code;
            p.item_name = p.item_name == null ? "" : p.item_name;
            p.status_item = p.status_item == null ? "" : p.status_item;
            p.qty = p.qty == null ? "" : p.qty;
            p.frequency = p.frequency == null ? "" : p.frequency;
            p.precautions = p.precautions == null ? "" : p.precautions;
            p.interaction = p.interaction == null ? "" : p.interaction;
            p.indication = p.indication == null ? "" : p.indication;

            //p.doctor_id = long.TryParse(p.doctor_id, out chk) ? chk.ToString() : "0";
        }
        public String insert(DrugSet p, String userId)
        {
            String re = "";
            String sql = "";
            p.active = "1";
            //p.ssdata_id = "";
            int chk = 0;
            chkNull(p);
            sql = "Insert Into " + drugs.table + " (" + drugs.drug_set_name + "," + drugs.item_code + "," + drugs.item_name + "" +
                ", " + drugs.status_item + "," + drugs.qty + "," + drugs.frequency + "," + drugs.precautions + "" +
                ", " + drugs.interaction + "," + drugs.indication + ","+drugs.doctor_id+", "+ drugs .active+ ", date_create, using1 "+ 
               ") " +
                "Values ('" + p.drug_set_name.Replace("'", "''") + "','" + p.item_code + "','" + p.item_name.Replace("'", "''") + "'" +
                ",'" + p.status_item + "','" + p.qty + "','" + p.frequency.Replace("'", "''") + "','"+ p.precautions.Replace("'", "''") + "'" +
                ",'" + p.interaction.Replace("'", "''") + "','" + p.indication.Replace("'", "''") + "','" + p.doctor_id + "','1', convert(varchar(20),getdate(),121) ,'"+ p.indication.Replace("'", "''")+"' "+
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
        public String update(DrugSet p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            chkNull(p);
            sql = "Update " + drugs.table + " Set " +
                " " + drugs.drug_set_name + " = '" + p.drug_set_name + "' " +
                "," + drugs.item_code + " = '" + p.item_code + "' " +
                "," + drugs.item_name + " = '" + p.item_name + "' " +
                "," + drugs.status_item + " = '" + p.status_item + "' " +
                "," + drugs.qty + " = '" + p.qty + "' " +
                "," + drugs.frequency + " = '" + p.frequency + "' " +
                "," + drugs.precautions + " = '" + p.precautions + "' " +
                "," + drugs.interaction + " = '" + p.interaction + "' " +
                "," + drugs.indication + " = '" + p.indication + "' " +
                "Where " + drugs.pkField + "='" + p.drug_set_id + "'"
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
        public String updateVoid(String id, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            sql = "Update " + drugs.table + " Set " +
                " " + drugs.active + " = '3' " +
                ", " + drugs.user_cancel + " = '"+ userId + "' " +
                ",date_cancel = getdate() " +
                " " +
                "Where " + drugs.pkField + "='" + id + "'"
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
        public String insertDrugSet(DrugSet p, String userId)
        {
            String re = "";

            if (p.drug_set_id.Equals(""))
            {
                re = insert(p, "");
            }
            else
            {
                re = update(p, "");
            }
            return re;
        }
        public void getlDgs(String dtrcode)
        {
            //lDept = new List<Position>();

            lDgs.Clear();
            DataTable dt = new DataTable();
            //new LogWriter("d", "QueueTypeDB getlDgs  00");
            dt = selectDrugSetGrougName(dtrcode);
            foreach (DataRow row in dt.Rows)
            {
                DrugSet itm1 = new DrugSet();
                itm1.drug_set_id = row[drugs.drug_set_name].ToString();
                itm1.drug_set_name = row[drugs.drug_set_name].ToString();
                lDgs.Add(itm1);
            }
        }
        public void setCboDgs(C1ComboBox c, String dtrcode, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectAll();
            int i = 0;
            if (lDgs.Count <= 0) getlDgs(dtrcode);
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (DrugSet cus1 in lDgs)
            {
                item = new ComboBoxItem();
                item.Value = cus1.drug_set_id;
                item.Text = cus1.drug_set_name;
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
                    c.SelectedIndex = 0;
                }
            }
        }
        public DrugSet setDrugSet(DataTable dt)
        {
            DrugSet dgs1 = new DrugSet();
            if (dt.Rows.Count > 0)
            {
                dgs1.drug_set_id = dt.Rows[0][drugs.drug_set_id].ToString();
                dgs1.doctor_id = dt.Rows[0][drugs.doctor_id].ToString();
                dgs1.drug_set_name = dt.Rows[0][drugs.drug_set_name].ToString();
                dgs1.item_code = dt.Rows[0][drugs.item_code].ToString();
                dgs1.item_name = dt.Rows[0][drugs.item_name].ToString();
                dgs1.status_item = dt.Rows[0][drugs.status_item].ToString();
                dgs1.qty = dt.Rows[0][drugs.qty].ToString();
                dgs1.frequency = dt.Rows[0][drugs.frequency].ToString();
                dgs1.precautions = dt.Rows[0][drugs.precautions].ToString();
                dgs1.interaction = dt.Rows[0][drugs.interaction].ToString();
                dgs1.indication = dt.Rows[0][drugs.indication].ToString();
            }
            else
            {
                setDrugSet(dgs1);
            }
            return dgs1;
        }
        public DrugSet setDrugSet(DrugSet ptt1)
        {
            ptt1.drug_set_id = "";
            ptt1.doctor_id = "";
            ptt1.drug_set_name = "";
            ptt1.item_code = "";
            ptt1.item_name = "";
            ptt1.status_item = "";
            ptt1.qty = "";
            ptt1.frequency = "";
            ptt1.precautions = "";
            ptt1.interaction = "";
            ptt1.indication = "";

            return ptt1;
        }
    }
}
