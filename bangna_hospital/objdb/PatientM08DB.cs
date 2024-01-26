using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PatientM08DB
    {
        public PatientM08 pm08;
        ConnectDB conn;
        public List<PatientM08> lPm08;
        public List<PatientM08> lAmphurinProv;
        public PatientM08DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm08 = new PatientM08();
            pm08.MNC_COU_CD = "MNC_COU_CD";
            pm08.MNC_CHW_CD = "MNC_CHW_CD";
            pm08.MNC_AMP_CD = "MNC_AMP_CD";
            pm08.MNC_AMP_DSC = "MNC_AMP_DSC";

            pm08.table = "patient_M08";

            lPm08 = new List<PatientM08>();
            lAmphurinProv = new List<PatientM08>();
        }
        public void getlCus()
        {
            //lDept = new List<Position>();

            lPm08.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                PatientM08 cus1 = new PatientM08();
                cus1.MNC_COU_CD = row[pm08.MNC_COU_CD].ToString();
                cus1.MNC_CHW_CD = row[pm08.MNC_CHW_CD].ToString();
                cus1.MNC_AMP_CD = row[pm08.MNC_AMP_CD].ToString();
                cus1.MNC_AMP_DSC = row[pm08.MNC_AMP_DSC].ToString();
                
                lPm08.Add(cus1);
            }
        }
        public void getlAmpurinProv(String provcode)
        {
            //lDept = new List<Position>();
            lAmphurinProv.Clear();
            DataTable dt = new DataTable();
            foreach (PatientM08 row in lPm08)
            {
                if (row.MNC_CHW_CD.Equals(provcode))
                {
                    PatientM08 cus1 = new PatientM08();
                    cus1.MNC_COU_CD = row.MNC_COU_CD;
                    cus1.MNC_CHW_CD = row.MNC_CHW_CD;
                    cus1.MNC_AMP_CD = row.MNC_AMP_CD;
                    cus1.MNC_AMP_DSC = row.MNC_AMP_DSC;
                    //cus1.MNC_TUM_DSC = row.MNC_TUM_DSC;
                    lAmphurinProv.Add(cus1);
                }
            }
        }
        public void getlAmpurinAmphurCode(String amphurcode)
        {
            //lDept = new List<Position>();
            lAmphurinProv.Clear();
            DataTable dt = new DataTable();
            if (lPm08.Count == 0) getlCus();
            foreach (PatientM08 row in lPm08)
            {
                if (row.MNC_AMP_CD.Equals(amphurcode))
                {
                    PatientM08 cus1 = new PatientM08();
                    cus1.MNC_COU_CD = row.MNC_COU_CD;
                    cus1.MNC_CHW_CD = row.MNC_CHW_CD;
                    cus1.MNC_AMP_CD = row.MNC_AMP_CD;
                    cus1.MNC_AMP_DSC = row.MNC_AMP_DSC;
                    //cus1.MNC_TUM_DSC = row.MNC_TUM_DSC;
                    lAmphurinProv.Add(cus1);
                }
            }
        }
        public void setCboAmphurByProvCode(C1ComboBox c, String provcode, String amphurcode)
        {
            lAmphurinProv.Clear();
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Clear();
            c.Items.Add(item);
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm08.* " +
                "From  patient_M08 pm08 " +
                "Where MNC_CHW_CD = '" + provcode + "'";
            
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count>0)
            {
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    PatientM08 cus1 = new PatientM08();
                    cus1.MNC_COU_CD = row["MNC_COU_CD"].ToString();
                    cus1.MNC_CHW_CD = row["MNC_CHW_CD"].ToString();
                    cus1.MNC_AMP_CD = row["MNC_AMP_CD"].ToString();
                    cus1.MNC_AMP_DSC = row["MNC_AMP_DSC"].ToString();
                    //cus1.MNC_TUM_DSC = row.MNC_TUM_DSC;
                    lAmphurinProv.Add(cus1);
                    item = new ComboBoxItem();
                    item.Value = row["MNC_AMP_CD"].ToString();
                    item.Text = row["MNC_AMP_DSC"].ToString();
                    c.Items.Add(item);
                    if (row["MNC_AMP_CD"].ToString().Equals(amphurcode))
                    {
                        //c.SelectedItem = item.Value;
                        c.SelectedText = cus1.MNC_AMP_DSC;
                        c.SelectedIndex = i + 1;
                    }
                    i++;
                }
            }
            if (amphurcode.Equals(""))
            {
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm08.* " +
                "From  patient_M08 pm08 ";
                //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectByProvcode(String provcode)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm08.* " +
                "From  patient_M08 pm08 " +
                "Where MNC_CHW_CD = '"+provcode+"'";
            //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectDeptIPD()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m32.MNC_MD_DEP_DSC,m32.MNC_MD_DEP_NO " +
                "From  patient_m32 m32 " +
                " Where m32.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public void setCboAmphurByAmphurCode(C1ComboBox c, String ampcode, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            c.Items.Clear();
            int i = 0;
            getlAmpurinAmphurCode(ampcode);
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM08 row in lAmphurinProv)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_AMP_CD;
                item.Text = row.MNC_AMP_DSC;
                c.Items.Add(item);
                if (item.Value.Equals(selected))
                {
                    //c.SelectedItem = item.Value;
                    c.SelectedText = item.Text;
                    c.SelectedIndex = i + 1;
                }
                i++;
            }
            if (selected == null || selected.Equals(""))
            {
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
        }
        public void setCboAmphur(C1ComboBox c, String provcode, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            getlAmpurinProv(provcode);
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM08 row in lAmphurinProv)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_AMP_CD;
                item.Text = row.MNC_AMP_DSC;
                c.Items.Add(item);
                if (item.Value.Equals(selected))
                {
                    //c.SelectedItem = item.Value;
                    c.SelectedText = item.Text;
                    c.SelectedIndex = i + 1;
                }
                i++;
            }
            if (selected.Equals(""))
            {
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
        }
        public PatientM08 setPatientM08(DataTable dt)
        {
            PatientM08 pm08 = new PatientM08();
            if (dt.Rows.Count > 0)
            {
                pm08.MNC_COU_CD = dt.Rows[0]["MNC_COU_CD"].ToString();
                pm08.MNC_CHW_CD = dt.Rows[0]["MNC_CHW_CD"].ToString();
                pm08.MNC_AMP_CD = dt.Rows[0]["MNC_AMP_CD"].ToString();
                pm08.MNC_AMP_DSC = dt.Rows[0]["MNC_AMP_DSC"].ToString();
            }
            else
            {
                setPatientM08(pm08);
            }
            return pm08;
        }
        public PatientM08 setPatientM08(PatientM08 p)
        {
            p.MNC_COU_CD = "";
            p.MNC_CHW_CD = "";
            p.MNC_AMP_CD = "";
            p.MNC_AMP_DSC = "";
            return p;
        }
    }
}
