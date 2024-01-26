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
    public class PatientM06DB
    {
        public PatientM06 pm06;
        ConnectDB conn;
        public List<PatientM06> lPm06;
        public PatientM06DB(ConnectDB c) 
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm06 = new PatientM06();
            pm06.MNC_NATI_CD = "MNC_NATI_CD";
            pm06.MNC_NATI_DSC = "MNC_NATI_DSC";
            //pm08.MNC_SEX = "MNC_SEX";
            //pm08.MNC_PFIX_DSC_e = "MNC_PFIX_DSC_e";

            pm06.table = "patient_M06";

            lPm06 = new List<PatientM06>();
        }
        public void getlCus()
        {
            //lDept = new List<Position>();

            lPm06.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                PatientM06 cus1 = new PatientM06();
                cus1.MNC_NATI_CD = row[pm06.MNC_NATI_CD].ToString();
                cus1.MNC_NATI_DSC = row[pm06.MNC_NATI_DSC].ToString();
                //cus1.MNC_SEX = row[pm08.MNC_SEX].ToString();
                //cus1.MNC_PFIX_DSC_e = row[pm08.MNC_PFIX_DSC_e].ToString();

                lPm06.Add(cus1);
            }
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm06.* " +
                "From  patient_M06 pm06 ";
            //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public void setCboRace(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            if (lPm06.Count <= 0) getlCus();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM06 row in lPm06)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_NATI_CD;
                item.Text = row.MNC_NATI_DSC;
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
        public PatientM06 setPatientM06(DataTable dt)
        {
            PatientM06 pm08 = new PatientM06();
            if (dt.Rows.Count > 0)
            {
                pm08.MNC_NATI_CD = dt.Rows[0]["MNC_NATI_CD"].ToString();
                pm08.MNC_NATI_DSC = dt.Rows[0]["MNC_NATI_DSC"].ToString();
                //pm08.MNC_SEX = dt.Rows[0]["MNC_SEX"].ToString();
                //pm08.MNC_PFIX_DSC_e = dt.Rows[0]["MNC_PFIX_DSC_e"].ToString();
            }
            else
            {
                setPatientM06(pm08);
            }
            return pm08;
        }
        public PatientM06 setPatientM06(PatientM06 p)
        {
            p.MNC_NATI_CD = "";
            p.MNC_NATI_DSC = "";
            //p.MNC_SEX = "";
            //p.MNC_PFIX_DSC_e = "";
            return p;
        }
    }
}
