using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class PatientM13DB
    {
        public ConnectDB conn;
        PatientT03 pm13;
        public List<PatientT03> lDeptOPD;
        public PatientM13DB(ConnectDB c) 
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm13 = new PatientT03();
            //pm13.MNC_APP_CD = "MNC_APP_CD";
            //pm13.MNC_APP_DSC = "MNC_APP_DSC";
            //pm13.MNC_DEP_NO = "MNC_DEP_NO";
            //pm13.MNC_SEC_NO = "MNC_SEC_NO";
            //pm13.MNC_DOT_CD = "MNC_DOT_CD";
            //pt03.MNC_HN_NO = "MNC_HN_NO";
            lDeptOPD = new List<PatientT03>();
        }
        public DataTable selectDeptOPDNew()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m13.MNC_APP_DSC,m13.MNC_APP_CD " +
                "From  patient_m13 m13 " +
                "  ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public AutoCompleteStringCollection getlApm()
        {
            //lDept = new List<Position>();
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            lDeptOPD.Clear();
            DataTable dt = new DataTable();
            dt = selectDeptOPDNew();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                autoSymptom.Add(row["MNC_APP_DSC"].ToString());
                //PatientM13 cus1 = new PatientM13();
                //cus1.MNC_APP_CD = row["MNC_APP_CD"].ToString();
                //cus1.MNC_APP_DSC = row["MNC_APP_DSC"].ToString();
                
                //lDeptOPD.Add(cus1);
            }
            return autoSymptom;
        }
    }
}
