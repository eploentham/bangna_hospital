using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PatientM05DB
    {
        public PatientM05 pm05;
        ConnectDB conn;
        public List<PatientM05> lPm05;

        public PatientM05DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm05 = new PatientM05();
            pm05.MNC_REL_CD = "MNC_REL_CD";
            pm05.MNC_REL_DSC = "MNC_REL_DSC";
            //pm08.MNC_SEX = "MNC_SEX";
            //pm08.MNC_PFIX_DSC_e = "MNC_PFIX_DSC_e";

            pm05.table = "patient_M05";

            lPm05 = new List<PatientM05>();
        }
        public void getlCus()
        {
            //lDept = new List<Position>();

            lPm05.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                PatientM05 cus1 = new PatientM05();
                cus1.MNC_REL_CD = row[pm05.MNC_REL_CD].ToString();
                cus1.MNC_REL_DSC = row[pm05.MNC_REL_DSC].ToString();
                //cus1.MNC_SEX = row[pm08.MNC_SEX].ToString();
                //cus1.MNC_PFIX_DSC_e = row[pm08.MNC_PFIX_DSC_e].ToString();

                lPm05.Add(cus1);
            }
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm05.* " +
                "From  patient_M05 pm05 ";
            //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public void setCboRel(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            if (lPm05.Count <= 0) getlCus();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM05 row in lPm05)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_REL_CD;
                item.Text = row.MNC_REL_DSC;
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
        public PatientM05 setPatientM05(DataTable dt)
        {
            PatientM05 pm08 = new PatientM05();
            if (dt.Rows.Count > 0)
            {
                pm08.MNC_REL_CD = dt.Rows[0]["MNC_REL_CD"].ToString();
                pm08.MNC_REL_DSC = dt.Rows[0]["MNC_REL_DSC"].ToString();
                //pm08.MNC_SEX = dt.Rows[0]["MNC_SEX"].ToString();
                //pm08.MNC_PFIX_DSC_e = dt.Rows[0]["MNC_PFIX_DSC_e"].ToString();
            }
            else
            {
                setPatientM05(pm08);
            }
            return pm08;
        }
        public PatientM05 setPatientM05(PatientM05 p)
        {
            p.MNC_REL_CD = "";
            p.MNC_REL_DSC = "";
            //p.MNC_SEX = "";
            //p.MNC_PFIX_DSC_e = "";
            return p;
        }
    }
}
