using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PatientM09DB
    {
        public PatientM09 pm09;
        ConnectDB conn;
        public List<PatientM09> lPm09;
        public List<PatientM09> lProv;
        public PatientM09DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm09 = new PatientM09();
            pm09.MNC_COU_CD = "MNC_COU_CD";
            pm09.MNC_CHW_CD = "MNC_CHW_CD";
            pm09.MNC_CHW_DSC = "MNC_CHW_DSC";
            pm09.table = "patient_M09";

            lPm09 = new List<PatientM09>();
            lProv = new List<PatientM09>();
        }
        public void getlCus()
        {
            //lDept = new List<Position>();

            lPm09.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                PatientM09 cus1 = new PatientM09();
                cus1.MNC_COU_CD = row[pm09.MNC_COU_CD].ToString();
                cus1.MNC_CHW_CD = row[pm09.MNC_CHW_CD].ToString();
                cus1.MNC_CHW_DSC = row[pm09.MNC_CHW_DSC].ToString();
                //cus1.MNC_AMP_DSC = row[pm09.MNC_AMP_DSC].ToString();

                lPm09.Add(cus1);
            }
        }
        public void getlProvByProvCode(String provcode)
        {
            //lDept = new List<Position>();
            lProv.Clear();
            DataTable dt = new DataTable();
            if (lPm09.Count == 0) getlCus();
            foreach (PatientM09 row in lPm09)
            {
                if (row.MNC_CHW_CD.Equals(provcode))
                {
                    PatientM09 cus1 = new PatientM09();
                    cus1.MNC_COU_CD = row.MNC_COU_CD;
                    cus1.MNC_CHW_CD = row.MNC_CHW_CD;
                    cus1.MNC_CHW_DSC = row.MNC_CHW_DSC;
                    //cus1.MNC_AMP_DSC = row.MNC_AMP_DSC;
                    //cus1.MNC_TUM_DSC = row.MNC_TUM_DSC;
                    lProv.Add(cus1);
                }
            }
        }
        public String getProvName(String provcode)
        {
            String re = "";
            if (lPm09.Count <= 0) getlCus();
            foreach (PatientM09 row in lPm09)
            {
                if (row.MNC_CHW_CD.Equals(provcode))
                {
                    re = row.MNC_CHW_DSC;
                    break;
                }
            }
            return re;
        }
        public void setCboProvByProvCode(C1ComboBox c, String provcode, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            c.Items.Clear();
            int i = 0;
            if(lProv.Count<=0) getlProvByProvCode(provcode);
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM09 row in lProv)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_CHW_CD;
                item.Text = row.MNC_CHW_DSC;
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
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm09.* " +
                "From  patient_M09 pm09 ";
            //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public void setCboProvince(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            if (lPm09.Count <= 0) getlCus();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM09 row in lPm09)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_CHW_CD;
                item.Text = row.MNC_CHW_DSC;
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
        public PatientM09 setPatientM09(DataTable dt)
        {
            PatientM09 pm09 = new PatientM09();
            if (dt.Rows.Count > 0)
            {
                pm09.MNC_COU_CD = dt.Rows[0]["MNC_COU_CD"].ToString();
                pm09.MNC_CHW_CD = dt.Rows[0]["MNC_CHW_CD"].ToString();
                pm09.MNC_CHW_DSC = dt.Rows[0]["MNC_CHW_DSC"].ToString();
            }
            else
            {
                setPatientM09(pm09);
            }
            return pm09;
        }
        public PatientM09 setPatientM09(PatientM09 p)
        {
            p.MNC_COU_CD = "";
            p.MNC_CHW_CD = "";
            p.MNC_CHW_DSC = "";
            return p;
        }
    }
}
