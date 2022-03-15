using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PatientM04DB
    {
        public PatientM04 pm04;
        ConnectDB conn;
        public List<PatientM04> lPm04;

        public PatientM04DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm04 = new PatientM04();
            pm04.MNC_NAT_CD = "MNC_NAT_CD";
            pm04.MNC_NAT_DSC = "MNC_NAT_DSC";
            pm04.NATION = "NATION";
            pm04.MNC_NAT_DSC_e = "MNC_NAT_DSC_e";

            pm04.table = "patient_M04";

            lPm04 = new List<PatientM04>();
        }
        public void getlCus()
        {
            //lDept = new List<Position>();

            lPm04.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                PatientM04 cus1 = new PatientM04();
                cus1.MNC_NAT_CD = row[pm04.MNC_NAT_CD].ToString();
                cus1.MNC_NAT_DSC = row[pm04.MNC_NAT_DSC].ToString();
                cus1.NATION = row[pm04.NATION].ToString();
                cus1.MNC_NAT_DSC_e = row[pm04.MNC_NAT_DSC_e].ToString();

                lPm04.Add(cus1);
            }
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm04.* " +
                "From  patient_M04 pm04 ";
            //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public void setCboNation(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            if (lPm04.Count <= 0) getlCus();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM04 row in lPm04)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_NAT_CD;
                item.Text = row.MNC_NAT_DSC;
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
        public PatientM04 setPatientM08(DataTable dt)
        {
            PatientM04 pm08 = new PatientM04();
            if (dt.Rows.Count > 0)
            {
                pm08.MNC_NAT_CD = dt.Rows[0]["MNC_NAT_CD"].ToString();
                pm08.MNC_NAT_DSC = dt.Rows[0]["MNC_NAT_DSC"].ToString();
                pm08.NATION = dt.Rows[0]["NATION"].ToString();
                pm08.MNC_NAT_DSC_e = dt.Rows[0]["MNC_NAT_DSC_e"].ToString();
            }
            else
            {
                setPatientM08(pm08);
            }
            return pm08;
        }
        public PatientM04 setPatientM08(PatientM04 p)
        {
            p.MNC_NAT_CD = "";
            p.MNC_NAT_DSC = "";
            p.NATION = "";
            p.MNC_NAT_DSC_e = "";
            return p;
        }
    }
}
