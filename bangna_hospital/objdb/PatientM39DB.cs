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
    public class PatientM39DB
    {
        public PatientM39 pm39;
        ConnectDB conn;
        public List<PatientM39> lPm02;
        public PatientM39DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm39 = new PatientM39();
            pm39.MNC_PAC_CD = "MNC_PAC_CD";
            pm39.MNC_PAC_TYP = "MNC_PAC_TYP";
            pm39.MNC_PAC_DSC = "MNC_PAC_DSC";
            pm39.MNC_DEP_CD = "MNC_DEP_CD";
            pm39.MNC_SEC_CD = "MNC_SEC_CD";
            pm39.MNC_DIA_CD = "MNC_DIA_CD";
            pm39.MNC_DOT_CD = "MNC_DOT_CD";
            pm39.MNC_PAC_STS = "MNC_PAC_STS";
            pm39.MNC_ACT_CD = "MNC_ACT_CD";
            pm39.MNC_PAC_COS = "MNC_PAC_COS";
            pm39.MNC_PAC_PRI = "MNC_PAC_PRI";
            pm39.MNC_COM_CD = "MNC_COM_CD";
            pm39.MNC_FN_CD = "MNC_FN_CD";
            pm39.MNC_FN_STS = "MNC_FN_STS";
            pm39.MNC_COM_POS = "MNC_COM_POS";
            pm39.MNC_DIA_TYP = "MNC_DIA_TYP";
            pm39.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pm39.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pm39.MNC_USR_ADD = "MNC_USR_ADD";
            pm39.MNC_USR_UPD = "MNC_USR_UPD";
            pm39.MNC_PAC_REM = "MNC_PAC_REM";
            pm39.MNC_DET_FLG = "MNC_DET_FLG";
            pm39.MNC_ST_DAT = "MNC_ST_DAT";
            pm39.MNC_EN_DAT = "MNC_EN_DAT";
            pm39.MNC_CHK_DAT = "MNC_CHK_DAT";
            pm39.MNC_SEL_FLG = "MNC_SEL_FLG";
            pm39.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            pm39.MNC_PAT_TYP = "MNC_PAT_TYP";
            pm39.status_sex = "status_sex";

            pm39.table = "PATIENT_M39";
            pm39.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm39.* " +
                "From  "+pm39.table+" pm39 ";
            //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectByCompcode(String compcode)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm39.* " +
                "From  " + pm39.table + " pm39 " +
                " Where pm39.MNC_COM_CD = '"+ compcode + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectByCompcode(String compcode, String sex)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm39.* " +
                "From  " + pm39.table + " pm39 " +
                " Where pm39.MNC_COM_CD = '" + compcode + "' and pm39.status_sex = '"+sex+"' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
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
                PatientM39 cus1 = new PatientM39();
                cus1.MNC_PAC_CD = row[pm39.MNC_PAC_CD].ToString();
                cus1.MNC_PAC_DSC = row[pm39.MNC_PAC_DSC].ToString();
                cus1.status_sex = row[pm39.status_sex].ToString();

                lPm02.Add(cus1);
            }
        }
        public void setCboPrefixT(C1ComboBox c, String compcode, String selected)
        {
            if(compcode.Equals("3")) return;        // ถ้าเป็น 3 ไม่ต้องเอาค่าเข้าไป
            ComboBoxItem item = new ComboBoxItem();
            DataTable dt = selectByCompcode(compcode);
            int i = 0;
            
            foreach (DataRow row in dt.Rows)
            {
                item = new ComboBoxItem();
                item.Value = row["MNC_PAC_CD"].ToString();
                item.Text = row["MNC_PAC_DSC"].ToString()+" ["+ row["MNC_PAC_PRI"].ToString()+"บาท]";
                c.Items.Add(item);
                
                i++;
            }
        }
    }
}
