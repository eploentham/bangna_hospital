using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PatientM02DB
    {
        public PatientM02 pm02;
        ConnectDB conn;
        public List<PatientM02> lPm02;

        public PatientM02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm02 = new PatientM02();
            pm02.MNC_PFIX_CD = "MNC_PFIX_CD";
            pm02.MNC_PFIX_DSC = "MNC_PFIX_DSC";
            pm02.MNC_SEX = "MNC_SEX";
            pm02.MNC_PFIX_DSC_e = "MNC_PFIX_DSC_e";

            pm02.table = "patient_M02";

            lPm02 = new List<PatientM02>();
        }
        public void getlCus()
        {
            //lDept = new List<Position>();

            lPm02.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                PatientM02 cus1 = new PatientM02();
                cus1.MNC_PFIX_CD = row[pm02.MNC_PFIX_CD].ToString();
                cus1.MNC_PFIX_DSC = row[pm02.MNC_PFIX_DSC].ToString();
                cus1.MNC_SEX = row[pm02.MNC_SEX].ToString();
                cus1.MNC_PFIX_DSC_e = row[pm02.MNC_PFIX_DSC_e].ToString();

                lPm02.Add(cus1);
            }
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm02.* " +
                "From  patient_M02 pm02 ";
            //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public void setCboPrefixT(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            if (lPm02.Count <= 0) getlCus();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM02 row in lPm02)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_PFIX_CD;
                item.Text = row.MNC_PFIX_DSC;
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
        public void setCboPrefixE(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            if (lPm02.Count <= 0) getlCus();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM02 row in lPm02)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_PFIX_CD;
                item.Text = row.MNC_PFIX_DSC_e;
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
        public PatientM02 setPatientM08(DataTable dt)
        {
            PatientM02 pm08 = new PatientM02();
            if (dt.Rows.Count > 0)
            {
                pm08.MNC_PFIX_CD = dt.Rows[0]["MNC_PFIX_CD"].ToString();
                pm08.MNC_PFIX_DSC = dt.Rows[0]["MNC_PFIX_DSC"].ToString();
                pm08.MNC_SEX = dt.Rows[0]["MNC_SEX"].ToString();
                pm08.MNC_PFIX_DSC_e = dt.Rows[0]["MNC_PFIX_DSC_e"].ToString();
            }
            else
            {
                setPatientM08(pm08);
            }
            return pm08;
        }
        public PatientM02 setPatientM08(PatientM02 p)
        {
            p.MNC_PFIX_CD = "";
            p.MNC_PFIX_DSC = "";
            p.MNC_SEX = "";
            p.MNC_PFIX_DSC_e = "";
            return p;
        }
    }
}
