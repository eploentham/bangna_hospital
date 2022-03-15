using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PatientM03DB
    {
        public PatientM03 pm03;
        ConnectDB conn;
        public List<PatientM03> lPm03;

        public PatientM03DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm03 = new PatientM03();
            pm03.MNC_EDU_CD = "MNC_EDU_CD";
            pm03.MNC_EDU_DSC = "MNC_EDU_DSC";
            //pm08.MNC_SEX = "MNC_SEX";
            //pm08.MNC_PFIX_DSC_e = "MNC_PFIX_DSC_e";

            pm03.table = "patient_M03";

            lPm03 = new List<PatientM03>();
        }
        public void getlCus()
        {
            //lDept = new List<Position>();

            lPm03.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                PatientM03 cus1 = new PatientM03();
                cus1.MNC_EDU_CD = row[pm03.MNC_EDU_CD].ToString();
                cus1.MNC_EDU_DSC = row[pm03.MNC_EDU_DSC].ToString();
                //cus1.MNC_SEX = row[pm08.MNC_SEX].ToString();
                //cus1.MNC_PFIX_DSC_e = row[pm08.MNC_PFIX_DSC_e].ToString();

                lPm03.Add(cus1);
            }
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm03.* " +
                "From  patient_M03 pm03 ";
            //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public void setCboEdu(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            if (lPm03.Count <= 0) getlCus();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM03 row in lPm03)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_EDU_CD;
                item.Text = row.MNC_EDU_DSC;
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
        public PatientM03 setPatientM08(DataTable dt)
        {
            PatientM03 pm08 = new PatientM03();
            if (dt.Rows.Count > 0)
            {
                pm08.MNC_EDU_CD = dt.Rows[0]["MNC_EDU_CD"].ToString();
                pm08.MNC_EDU_DSC = dt.Rows[0]["MNC_EDU_DSC"].ToString();
                //pm08.MNC_SEX = dt.Rows[0]["MNC_SEX"].ToString();
                //pm08.MNC_PFIX_DSC_e = dt.Rows[0]["MNC_PFIX_DSC_e"].ToString();
            }
            else
            {
                setPatientM08(pm08);
            }
            return pm08;
        }
        public PatientM03 setPatientM08(PatientM03 p)
        {
            p.MNC_EDU_CD = "";
            p.MNC_EDU_DSC = "";
            //p.MNC_SEX = "";
            //p.MNC_PFIX_DSC_e = "";
            return p;
        }
    }
}
