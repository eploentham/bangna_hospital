using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class PatientM07DB
    {
        public PatientM07 pm07;
        ConnectDB conn;
        public List<PatientM07> lPm07;
        public List<PatientM07> lTaminAmphur;
        public List<PatientM07> ltambonAll;
        public PatientM07DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm07 = new PatientM07();
            pm07.MNC_COU_CD = "MNC_COU_CD";
            pm07.MNC_CHW_CD = "MNC_CHW_CD";
            pm07.MNC_AMP_CD = "MNC_AMP_CD";
            pm07.MNC_TUM_CD = "MNC_TUM_CD";
            pm07.MNC_TUM_DSC = "MNC_TUM_DSC";
            pm07.postcode = "MNC_POC";
            pm07.table = "patient_M07";

            lPm07 = new List<PatientM07>();
            lTaminAmphur = new List<PatientM07>();
            ltambonAll = new List<PatientM07>();
        }
        public void getlTambonAll()
        {
            //lDept = new List<Position>();
            if (ltambonAll.Count <= 0)
            {
                DataTable dt = new DataTable();
                dt = selectAll();
                //dtCus = dt;
                foreach (DataRow row in dt.Rows)
                {
                    PatientM07 cus1 = new PatientM07();
                    cus1.MNC_COU_CD = row[pm07.MNC_COU_CD].ToString();
                    cus1.MNC_CHW_CD = row[pm07.MNC_CHW_CD].ToString();
                    cus1.MNC_AMP_CD = row[pm07.MNC_AMP_CD].ToString();
                    cus1.MNC_TUM_CD = row[pm07.MNC_TUM_CD].ToString();
                    cus1.MNC_TUM_DSC = row[pm07.MNC_TUM_DSC].ToString();
                    cus1.postcode = row[pm07.postcode].ToString();
                    ltambonAll.Add(cus1);
                }
            }
        }
        public void getlTaminAmpur(String amphrcode)
        {
            //lDept = new List<Position>();
            lTaminAmphur.Clear();
            //DataTable dt = new DataTable();            
            foreach (PatientM07 row in ltambonAll)
            {
                if (row.MNC_AMP_CD.Equals(amphrcode))
                {
                    PatientM07 cus1 = new PatientM07();
                    cus1.MNC_COU_CD = row.MNC_COU_CD;
                    cus1.MNC_CHW_CD = row.MNC_CHW_CD;
                    cus1.MNC_AMP_CD = row.MNC_AMP_CD;
                    cus1.MNC_TUM_CD = row.MNC_TUM_CD;
                    cus1.MNC_TUM_DSC = row.MNC_TUM_DSC;
                    lTaminAmphur.Add(cus1);
                }
            }
        }
        public String getPostCode(String tambonname)
        {
            String re = "";
            foreach (PatientM07 row in ltambonAll)
            {
                if (row.chwname.Equals(tambonname))
                {
                    re = row.postcode;
                    break;
                }
            }
            return re;
        }
        public String getChwCode(String chwname)
        {
            String re = "";
            foreach(PatientM07 row in ltambonAll)
            {
                if (row.chwname.Equals(chwname))
                {
                    re = row.MNC_CHW_CD;
                    break;
                }
            }
            return re;
        }
        public String getChwName(String chwcode)
        {
            String re = "";
            foreach (PatientM07 row in ltambonAll)
            {
                if (row.MNC_CHW_CD.Equals(chwcode))
                {
                    re = row.chwname;
                    break;
                }
            }
            return re;
        }
        public String getChwNameByPostCode(String postcode)
        {
            String re = "";
            foreach (PatientM07 row in ltambonAll)
            {
                if (row.postcode.Equals(postcode))
                {
                    re = row.chwname;
                    break;
                }
            }
            return re;
        }
        public String getAmpCode(String ampname)
        {
            String re = "";
            foreach (PatientM07 row in ltambonAll)
            {
                if (row.ampname.Equals(ampname))
                {
                    re = row.MNC_AMP_CD;
                    break;
                }
            }
            return re;
        }
        public String getAmpName(String ampcode)
        {
            String re = "";
            foreach (PatientM07 row in ltambonAll)
            {
                if (row.MNC_AMP_CD.Equals(ampcode))
                {
                    re = row.ampname;
                    break;
                }
            }
            return re;
        }
        public String getAmpNameByPostCode(String postcode)
        {
            String re = "";
            foreach (PatientM07 row in ltambonAll)
            {
                if (row.postcode.Equals(postcode))
                {
                    re = row.ampname;
                    break;
                }
            }
            return re;
        }
        public String getTambonCode(String tambonname)
        {
            String re = "";
            foreach (PatientM07 row in ltambonAll)
            {
                if (row.MNC_TUM_DSC.Equals(tambonname))
                {
                    re = row.MNC_TUM_CD;
                    break;
                }
            }
            return re;
        }
        public String getTambonName(String tamboncode)
        {
            String re = "";
            if (ltambonAll.Count <= 0) getlTambonAll();
            foreach (PatientM07 row in ltambonAll)
            {
                if (row.MNC_TUM_CD.Equals(tamboncode))
                {
                    re = row.MNC_TUM_DSC;
                    break;
                }
            }
            return re;
        }
        public String getTambonNameByPostCode(String postcode)
        {
            String re = "";
            foreach (PatientM07 row in ltambonAll)
            {
                if (row.postcode.Equals(postcode))
                {
                    re = row.MNC_TUM_DSC;
                    break;
                }
            }
            return re;
        }
        public void setCboTambonByAmphrCode(C1ComboBox c, String amphurcode, String tamboncode)
        {
            lTaminAmphur.Clear();
            ComboBoxItem item = new ComboBoxItem();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Clear();
            c.Items.Add(item);
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm07.* " +
                "From  patient_M07 pm07 " +
                "Where MNC_AMP_CD = '" + amphurcode + "'";

            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    PatientM07 cus1 = new PatientM07();
                    cus1.MNC_COU_CD = row["MNC_COU_CD"].ToString();
                    cus1.MNC_CHW_CD = row["MNC_CHW_CD"].ToString();
                    cus1.MNC_AMP_CD = row["MNC_AMP_CD"].ToString();
                    cus1.MNC_TUM_CD = row["MNC_TUM_cd"].ToString();
                    cus1.MNC_TUM_DSC = row["MNC_TUM_DSC"].ToString();
                    lTaminAmphur.Add(cus1);
                    item = new ComboBoxItem();
                    item.Value = row["MNC_TUM_CD"].ToString();
                    item.Text = row["MNC_TUM_dsc"].ToString();
                    c.Items.Add(item);
                    if (row["MNC_TUM_CD"].ToString().Equals(tamboncode))
                    {
                        //c.SelectedItem = item.Value;
                        c.SelectedText = cus1.MNC_TUM_DSC;
                        c.SelectedIndex = i + 1;
                    }
                    i++;
                }
            }
            if (tamboncode == null || tamboncode.Equals(""))
            {
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
        }
        public DataTable selectByAmphur(String amphrcode)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm07.* " +
                "From  patient_M07 pm07 "+
            " Where pm07.MNC_AMP_CD = '"+ amphrcode + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectByTambonName(String tambonname)
        {
            DataTable dt = new DataTable();
            if (tambonname.Length <= 2) return dt;
            String sql = "", re = "";
            sql = "Select pm07.* " +
                "From  patient_M07 pm07 " +
            " Where pm07.MNC_AMP_CD like '%" + tambonname + "%' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public String selectPostcodeByTambon(String tambon)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm11.* " +
                "From  patient_M11 pm11 " +
            " Where pm11.MNC_TUM_CD = '" + tambon + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_POC"].ToString();
            }
            return re;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm07.*,pm11.MNC_POC " +
                "From  patient_M07 pm07 "+
                "inner join patient_m11 pm11 on pm07.MNC_TUM_CD = pm11.MNC_TUM_CD " ;
            //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public String getTumbonAmphurProvName(String tamboncode)
        {
            DataTable dt = new DataTable();
            if (tamboncode.Length < 0) return "";
            String sql = "",re="";
            sql = "Select pm07.*,pm08.MNC_AMP_DSC,pm09.MNC_CHW_DSC " +
                "From  patient_M07 pm07 " +
                "inner join patient_M08 pm08 on pm08.MNC_AMP_CD = pm07.MNC_AMP_CD " +
                "inner join patient_m09 pm09 on pm09.MNC_CHW_CD = pm07.MNC_CHW_CD " +
                //"inner join patient_m11 pm11 on pm07.MNC_TUM_CD = pm11.MNC_TUM_CD " +
                " Where pm07.MNC_TUM_CD = '" + tamboncode + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = " ตำบล " + dt.Rows[0]["MNC_TUM_DSC"].ToString() + " อำเภอ " + dt.Rows[0]["MNC_AMP_DSC"].ToString() + " จังหวัด " + dt.Rows[0]["MNC_CHW_DSC"].ToString();
            }
            return re;
        }
        public AutoCompleteStringCollection setAutoCompTumbonName(String tambonname)
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            if (tambonname.Length < 0) return null;
            String sql = "";
            sql = "Select pm07.*,pm08.MNC_AMP_DSC,pm09.MNC_CHW_DSC " +
                "From  patient_M07 pm07 " +
                "inner join patient_M08 pm08 on pm08.MNC_AMP_CD = pm07.MNC_AMP_CD " +
                "inner join patient_m09 pm09 on pm09.MNC_CHW_CD = pm07.MNC_CHW_CD " +
                //"inner join patient_m11 pm11 on pm07.MNC_TUM_CD = pm11.MNC_TUM_CD " +
                " Where pm07.MNC_TUM_DSC like '" + tambonname + "%' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    autoSymptom.Add(row["MNC_TUM_DSC"].ToString() + "," + row["MNC_AMP_DSC"].ToString() + "," + row["MNC_CHW_DSC"].ToString());
                }
            }
            return autoSymptom;
        }
        public AutoCompleteStringCollection setAutoCompTumbonName_getlTambonAll()
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            ltambonAll.Clear();
            String sql = "";
            sql = "Select pm07.*,pm08.MNC_AMP_DSC,pm09.MNC_CHW_DSC,pm11.MNC_POC " +
                "From  patient_M07 pm07 " +
                "inner join patient_M08 pm08 on pm08.MNC_AMP_CD = pm07.MNC_AMP_CD " +
                "inner join patient_m09 pm09 on pm09.MNC_CHW_CD = pm07.MNC_CHW_CD " +
                "inner join patient_m11 pm11 on pm07.MNC_TUM_CD = pm11.MNC_TUM_CD " +
                "  ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    autoSymptom.Add(row["MNC_TUM_DSC"].ToString() + "," + row["MNC_AMP_DSC"].ToString() + "," + row["MNC_CHW_DSC"].ToString() + "," + row["MNC_POC"].ToString());
                    PatientM07 cus1 = new PatientM07();
                    cus1.MNC_COU_CD = row[pm07.MNC_COU_CD].ToString();
                    cus1.MNC_CHW_CD = row[pm07.MNC_CHW_CD].ToString();
                    cus1.MNC_AMP_CD = row[pm07.MNC_AMP_CD].ToString();
                    cus1.MNC_TUM_CD = row[pm07.MNC_TUM_CD].ToString();
                    cus1.MNC_TUM_DSC = row[pm07.MNC_TUM_DSC].ToString();
                    cus1.postcode = row["MNC_POC"].ToString();
                    cus1.chwname = row["MNC_CHW_DSC"].ToString();
                    cus1.ampname = row["MNC_AMP_DSC"].ToString();
                    ltambonAll.Add(cus1);
                }
            }
            return autoSymptom;
        }
        public void setCboTumbonName(C1ComboBox c, String tambonname)
        {
            ComboBoxItem item = new ComboBoxItem();
            
            DataTable dt = new DataTable();
            if (tambonname.Length < 3) return;
            String sql = "", re = "";
            sql = "Select pm07.*,pm08.MNC_AMP_DSC,pm09.MNC_CHW_DSC " +
                "From  patient_M07 pm07 " +
                "inner join patient_M08 pm08 on pm08.MNC_AMP_CD = pm07.MNC_AMP_CD " +
                "inner join patient_m09 pm09 on pm09.MNC_CHW_CD = pm07.MNC_CHW_CD " +
                " Where pm07.MNC_TUM_DSC like '" + tambonname + "%' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                c.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    item = new ComboBoxItem();
                    item.Value = row["MNC_TUM_CD"].ToString();
                    item.Text = row["MNC_TUM_DSC"].ToString()+" "+ row["MNC_AMP_DSC"].ToString()+" "+ row["MNC_CHW_DSC"].ToString();
                    c.Items.Add(item);
                }
            }
        }
        public void setCboTumbonName(C1ComboBox c, String tambonname, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();

            DataTable dt = new DataTable();
            if (tambonname.Length < 3) return;
            String sql = "", re = "";
            sql = "Select pm07.*,pm08.MNC_AMP_DSC,pm09.MNC_CHW_DSC " +
                "From  patient_M07 pm07 " +
                "inner join patient_M08 pm08 on pm08.MNC_AMP_CD = pm07.MNC_AMP_CD " +
                "inner join patient_m09 pm09 on pm09.MNC_CHW_CD = pm07.MNC_CHW_CD " +
                " Where pm07.MNC_TUM_DSC like '" + tambonname + "%' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                c.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    item = new ComboBoxItem();
                    item.Value = row["MNC_TUM_CD"].ToString();
                    item.Text = row["MNC_TUM_DSC"].ToString() + " " + row["MNC_AMP_DSC"].ToString() + " " + row["MNC_CHW_DSC"].ToString();
                    c.Items.Add(item);
                    if (item.Value.Equals(selected))
                    {
                        //c.SelectedItem = item.Value;
                        c.SelectedText = item.Text;
                        c.SelectedIndex = i;
                    }
                    i++;
                }
            }
            if (selected.Equals(""))
            {
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
        }
        public void setCboTumbon(C1ComboBox c, String amphurcode, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            getlTaminAmpur(amphurcode);//tambon ต้องดึงใหม่ 
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Clear();
            c.Items.Add(item);
            
            foreach (PatientM07 row in lTaminAmphur)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_TUM_CD;
                item.Text = row.MNC_TUM_DSC;
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
        public PatientM07 setPatientM07(DataTable dt)
        {
            PatientM07 pm09 = new PatientM07();
            if (dt.Rows.Count > 0)
            {
                pm07.MNC_COU_CD = dt.Rows[0]["MNC_COU_CD"].ToString();
                pm07.MNC_CHW_CD = dt.Rows[0]["MNC_CHW_CD"].ToString();
                pm07.MNC_AMP_CD = dt.Rows[0]["MNC_AMP_CD"].ToString();
                pm07.MNC_TUM_CD = dt.Rows[0]["MNC_TUM_CD"].ToString();
                pm07.MNC_TUM_DSC = dt.Rows[0]["MNC_TUM_DSC"].ToString();
            }
            else
            {
                setPatientM07(pm09);
            }
            return pm09;
        }
        public PatientM07 setPatientM07(PatientM07 p)
        {
            p.MNC_COU_CD = "";
            p.MNC_CHW_CD = "";
            p.MNC_AMP_CD = "";
            p.MNC_TUM_CD = "";
            p.MNC_TUM_DSC = "";
            return p;
        }
    }
}
