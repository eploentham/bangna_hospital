using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PatientM24DB
    {
        public PatientM24 pm24;
        ConnectDB conn;
        public List<PatientM24> lPm24;
        public PatientM24DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm24 = new PatientM24();
            pm24.MNC_COM_CD = "MNC_COM_CD";
            pm24.MNC_COM_PRF_CD = "MNC_COM_PRF_CD";
            pm24.MNC_COM_DSC = "MNC_COM_DSC";
            pm24.MNC_COM_TUM_CD = "MNC_COM_TUM_CD";
            pm24.MNC_COM_AMP_CD = "MNC_COM_AMP_CD";
            pm24.MNC_COM_CHW_CD = "MNC_COM_CHW_CD";
            pm24.MNC_COM_POC = "MNC_COM_POC";
            pm24.MNC_COM_TEL = "MNC_COM_TEL";
            pm24.MNC_COM_TYP_CD = "MNC_COM_TYP_CD";
            pm24.MNC_COM_STS = "MNC_COM_STS";

            pm24.table = "patient_M24";

            lPm24 = new List<PatientM24>();
        }
        public void getlCus()
        {
            //lDept = new List<Position>();

            lPm24.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                PatientM24 cus1 = new PatientM24();
                cus1.MNC_COM_CD = row[pm24.MNC_COM_CD].ToString();
                cus1.MNC_COM_DSC = row[pm24.MNC_COM_DSC].ToString();
                cus1.MNC_COM_TEL = row[pm24.MNC_COM_TEL].ToString();
                cus1.MNC_COM_PRF_CD = row[pm24.MNC_COM_PRF_CD].ToString();
                cus1.MNC_COM_ADD = row[pm24.MNC_COM_ADD].ToString();
                cus1.MNC_COM_TUM_CD = row[pm24.MNC_COM_TUM_CD].ToString();
                cus1.MNC_COM_AMP_CD = row[pm24.MNC_COM_AMP_CD].ToString();
                cus1.MNC_COM_CHW_CD = row[pm24.MNC_COM_CHW_CD].ToString();

                lPm24.Add(cus1);
            }
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm24.* " +
                "From  patient_M24 pm24 ";
            //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public PatientM24 setPatientM08(DataTable dt)
        {
            PatientM24 pm08 = new PatientM24();
            if (dt.Rows.Count > 0)
            {
                pm08.MNC_COM_CD = dt.Rows[0]["MNC_COM_CD"].ToString();
                pm08.MNC_COM_DSC = dt.Rows[0]["MNC_COM_DSC"].ToString();
                pm08.MNC_COM_TEL = dt.Rows[0]["MNC_COM_TEL"].ToString();
                pm08.MNC_COM_PRF_CD = dt.Rows[0]["MNC_COM_PRF_CD"].ToString();
                pm08.MNC_COM_ADD = dt.Rows[0]["MNC_COM_ADD"].ToString();
                pm08.MNC_COM_TUM_CD = dt.Rows[0]["MNC_COM_TUM_CD"].ToString();
                pm08.MNC_COM_AMP_CD = dt.Rows[0]["MNC_COM_AMP_CD"].ToString();
                pm08.MNC_COM_CHW_CD = dt.Rows[0]["MNC_COM_CHW_CD"].ToString();
                pm08.MNC_COM_GRP_CD = dt.Rows[0]["MNC_COM_GRP_CD"].ToString();
                pm08.MNC_COM_TYP_CD = dt.Rows[0]["MNC_COM_TYP_CD"].ToString();
            }
            else
            {
                setPatientM08(pm08);
            }
            return pm08;
        }
        public PatientM24 setPatientM08(PatientM24 p)
        {
            p.MNC_COM_CD = "";
            p.MNC_COM_DSC = "";
            p.MNC_COM_TEL = "";
            p.MNC_COM_PRF_CD = "";
            p.MNC_COM_ADD = "";
            p.MNC_COM_TUM_CD = "";
            p.MNC_COM_AMP_CD = "";
            p.MNC_COM_CHW_CD = "";
            p.MNC_COM_GRP_CD = "";
            p.MNC_COM_TYP_CD = "";
            return p;
        }
    }
}
