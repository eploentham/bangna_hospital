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
    public class PatientM32DB
    {
        public PatientM32 pttM32;
        ConnectDB conn;
        public List<PatientM32> lPm08;
        public PatientM32DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pttM32 = new PatientM32();
            pttM32.mnc_md_dep_dsc = "mnc_md_dep_dsc";
            pttM32.mnc_md_dep_no = "mnc_md_dep_no";
            pttM32.mnc_sec_no = "mnc_sec_no";
            pttM32.MNC_DIV_NO = "MNC_DIV_NO";
            pttM32.MNC_TYP_PT = "MNC_TYP_PT";
            pttM32.MNC_DP_NO = "MNC_DP_NO";
            pttM32.CLINIC = "CLINIC";
            pttM32.MNC_BUD_NO = "MNC_BUD_NO";
            pttM32.MNC_REP_GRP = "MNC_REP_GRP";
            pttM32.MNC_GRP_NO = "MNC_GRP_NO";
            pttM32.MNC_GRP_SS = "MNC_GRP_SS";
            pttM32.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pttM32.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pttM32.MNC_USR_ADD = "MNC_USR_ADD";
            pttM32.MNC_USR_UPD = "MNC_USR_UPD";
            pttM32.MNC_SUB_DP_NO = "MNC_SUB_DP_NO";
            pttM32.opbkk_clinic = "opbkk_clinic";

            pttM32.table = "patient_M32";

            lPm08 = new List<PatientM32>();

        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select * " +
                "From patient_m32  " ;
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable selectDeptOPDAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m32.MNC_MD_DEP_DSC, m32.SEC_NO " +
                "From  patient_m32 m32 " +
                " Where m32.MNC_TYP_PT = 'O' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            
            return dt;
        }
        public String selectDeptOPD(String deptid)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m32.MNC_MD_DEP_DSC " +
                "From  patient_m32 m32 " +
                " Where m32.MNC_md_dep_no = '" + deptid + "' and m32.MNC_TYP_PT = 'O' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_MD_DEP_DSC"].ToString();
            }
            return re;
        }
        public String selectDeptOPDBySecNO(String secno)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m32.MNC_MD_DEP_NO " +
                "From  patient_m32 m32 " +
                " Where m32.MNC_SEC_NO = '" + secno + "' and m32.MNC_TYP_PT = 'O' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_MD_DEP_NO"].ToString();
            }
            return re;
        }
        public DataTable selectDeptIPDAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m32.MNC_MD_DEP_DSC, m32.SEC_NO " +
                "From  patient_m32 m32 " +
                " Where  m32.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            
            return dt;
        }
        public String selectDeptIPD(String deptid)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m32.MNC_MD_DEP_DSC " +
                "From  patient_m32 m32 " +
                " Where m32.MNC_sec_no = '" + deptid + "' and m32.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_MD_DEP_DSC"].ToString();
            }
            return re;
        }
        public void getlCus()
        {
            //lDept = new List<Position>();

            lPm08.Clear();
            DataTable dt = new DataTable();
            dt = SelectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                PatientM32 cus1 = new PatientM32();
                cus1.mnc_sec_no = row[pttM32.mnc_sec_no].ToString();
                cus1.mnc_md_dep_no = row[pttM32.mnc_md_dep_no].ToString();
                cus1.mnc_md_dep_dsc = row[pttM32.mnc_md_dep_dsc].ToString();
                cus1.MNC_DP_NO = row[pttM32.MNC_DP_NO].ToString();
                cus1.MNC_TYP_PT = row[pttM32.MNC_TYP_PT].ToString();
                cus1.opbkk_clinic = row[pttM32.opbkk_clinic].ToString();
                lPm08.Add(cus1);
            }
        }
        public String getDeptNoOPD(String seccode)
        {
            String re = "";
            if (lPm08.Count <= 0) getlCus();
            foreach (PatientM32 row in lPm08)
            {
                if (row.MNC_TYP_PT.Equals("O"))
                {
                    if (row.mnc_sec_no.Equals(seccode))
                    {
                        re = row.mnc_md_dep_no;
                        break;
                    }
                }
            }
            return re;
        }
        public String getDeptName(String seccode)
        {
            String re = "";
            if (lPm08.Count <= 0) getlCus();
            foreach (PatientM32 row in lPm08)
            {
                if (row.mnc_sec_no.Equals(seccode))
                {
                    re = row.mnc_md_dep_dsc;
                    break;
                }
            }
            return re;
        }
        public String getDeptNameOPD(String seccode)
        {
            String re = "";
            if (lPm08.Count <= 0) getlCus();
            foreach (PatientM32 row in lPm08)
            {
                if (row.MNC_TYP_PT.Equals("O"))
                {
                    if (row.mnc_sec_no.Equals(seccode))
                    {
                        re = row.mnc_md_dep_dsc;
                        break;
                    }
                }
            }
            return re;
        }
        public void setCboAmphur(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            if (lPm08.Count <= 0) getlCus();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM32 row in lPm08)
            {
                item = new ComboBoxItem();
                item.Value = row.mnc_md_dep_no;
                item.Text = row.mnc_md_dep_dsc;
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
        public String updateSSOPCode(String dep_no, String sec_no, String div_no, String typ_pt, String ssopcode)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update patient_m32 Set " +
                    "ssop_clinic ='" + ssopcode + "' " +
                    "Where mnc_md_dep_no ='" + dep_no + "' and mnc_sec_no ='" + sec_no + "' and mnc_div_no ='" + div_no + "' and mnc_typ_pt = '" + typ_pt + "'";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " PatientM30DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
        public String updateOPBKKCode(String dep_no, String sec_no, String div_no, String typ_pt, String opbkkcode)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update patient_m32 Set " +
                    "opbkk_clinic ='" + opbkkcode + "' " +
                    "Where mnc_md_dep_no ='" + dep_no + "' and mnc_sec_no ='"+sec_no+"' and mnc_div_no ='"+div_no+"' and mnc_typ_pt = '"+typ_pt+"'";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " PatientM30DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
        public PatientM32 setPatientM08(DataTable dt)
        {
            PatientM32 pm08 = new PatientM32();
            if (dt.Rows.Count > 0)
            {
                pm08.mnc_sec_no = dt.Rows[0]["mnc_sec_no"].ToString();
                pm08.mnc_md_dep_no = dt.Rows[0]["mnc_md_dep_no"].ToString();
                pm08.mnc_md_dep_dsc = dt.Rows[0]["mnc_md_dep_dsc"].ToString();
                pm08.MNC_DIV_NO = dt.Rows[0]["MNC_DIV_NO"].ToString();
                pm08.MNC_TYP_PT = dt.Rows[0]["MNC_TYP_PT"].ToString();
                pm08.MNC_DP_NO = dt.Rows[0]["MNC_DP_NO"].ToString();
                pm08.opbkk_clinic = dt.Rows[0]["opbkk_clinic"].ToString();
            }
            else
            {
                setPatientM08(pm08);
            }
            return pm08;
        }
        public PatientM32 setPatientM08(PatientM32 p)
        {
            p.mnc_sec_no = "";
            p.mnc_md_dep_no = "";
            p.mnc_md_dep_dsc = "";
            p.MNC_DIV_NO = "";
            p.MNC_TYP_PT = "";
            p.MNC_DP_NO = "";
            p.opbkk_clinic = "";
            return p;
        }
    }
}
