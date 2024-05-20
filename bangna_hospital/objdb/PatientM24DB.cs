using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class PatientM24DB
    {
        public PatientM24 pm24;
        ConnectDB conn;
        public List<PatientM24> lPm24;
        public List<PatientM24> lPm24Search;
        DataTable dtInsur ;
        DataTable dtComp ;
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
            pm24.MNC_COM_START = "MNC_COM_START";
            pm24.MNC_COM_DSC_E = "MNC_COM_DSC_E";
            pm24.status_insur = "status_insur";
            pm24.insur1_code = "insur1_code";
            pm24.insur2_code = "insur2_code";
            pm24.MNC_COM_ADD = "MNC_COM_ADD";
            pm24.phone2 = "phone2";
            pm24.email = "email";
            pm24.MNC_COM_NAM = "MNC_COM_NAM";
            pm24.name_old = "name_old";

            pm24.table = "patient_M24";

            lPm24 = new List<PatientM24>();
            lPm24Search = new List<PatientM24>();
            dtInsur = new DataTable();
            dtComp = new DataTable();
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
                cus1.phone2 = row[pm24.phone2].ToString();
                cus1.email = row[pm24.email].ToString();
                cus1.MNC_COM_NAM = row[pm24.MNC_COM_NAM].ToString();

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
        public String selectCustByName1(String name)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm24.MNC_COM_CD,pm24.MNC_COM_DSC " +
                "From  patient_M24 pm24 " +
            " Where pm24." + pm24.MNC_COM_DSC + " = '" + name + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_COM_CD"].ToString();
            }
            return re;
        }
        public String selectCust(String code)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm24.MNC_COM_CD,pm24.MNC_COM_DSC " +
                "From  patient_M24 pm24 " +
            " Where pm24." + pm24.MNC_COM_CD + " = '" + code + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_COM_DSC"].ToString();
            }
            return re;
        }
        public DataTable selectCustByName(String name)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm24.MNC_COM_CD,isnull(pm24.MNC_COM_DSC,'') as MNC_COM_DSC, isnull(pm24.MNC_COM_TEL,'') as MNC_COM_TEL,isnull(pm24.phone2,'') as phone2,isnull(pm24.email,'') as email " +
                ",isnull(pm24.insur1_code,'') as insur1_code ,isnull(pm24.insur2_code,'') as insur2_code ,isnull(pm24.status_insur,'') as status_insur,isnull(pm24.MNC_COM_ADD,'') as MNC_COM_ADD " +
                ",isnull(pm24.MNC_COM_NAM,'') as MNC_COM_NAM " +
                "From  patient_M24 pm24 "+
            " Where (pm24."+ pm24.MNC_COM_DSC + " like '"+ name + "%') or (pm24."+ pm24.MNC_COM_CD + " like '"+ name + "%') or (pm24."+ pm24.MNC_COM_DSC_E + " like '"+ name + "%') ";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public AutoCompleteStringCollection setAutoComp()
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm24.MNC_COM_CD,pm24.MNC_COM_DSC " +
                "From  patient_M24 pm24 "
            ;
            dt = conn.selectData(conn.connMainHIS, sql);
            if(dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    autoSymptom.Add(row["MNC_COM_DSC"].ToString());
                }
            }
            return autoSymptom;
        }
        public String getPaidCode(String paidname)
        {
            String re = "";
            foreach (PatientM24 row in lPm24)
            {
                //if (row.MNC_COM_DSC.Equals("AIA"))        //เอาไว้ test debug
                //{
                //    String aaa = "";
                //}
                //bool areEqual = row.MNC_COM_DSC.Equals(paidname, StringComparison.OrdinalIgnoreCase);     //มีแจ้ง error ว่า save แล้ว บริษัทหาย ได้ลอง debug เช่น aIa ค้นไม่เจอ
                if (row.MNC_COM_DSC.Equals(paidname, StringComparison.OrdinalIgnoreCase))
                {
                    re = row.MNC_COM_CD;
                    break;
                }
            }
            return re;
        }
        public String getPaidName(String paidcode)
        {
            String re = "";
            foreach (PatientM24 row in lPm24)
            {
                if (row.MNC_COM_CD.Equals(paidcode))
                {
                    re = row.MNC_COM_DSC;
                    break;
                }
            }
            return re;
        }
        public AutoCompleteStringCollection getlPaid1(Boolean refresh)
        {
            if (refresh) lPm24.Clear();
            if (lPm24.Count <= 0) getlCus();
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            foreach (PatientM24 rowa in lPm24)
            {
                autoSymptom.Add(rowa.MNC_COM_DSC);
            }
            return autoSymptom;
        }
        public AutoCompleteStringCollection setAutoInsur(Boolean refresh)
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            String sql = "", re = "";
            if(refresh) dtInsur.Clear();
            if (dtInsur.Rows.Count <= 0)
            {//check เพื่อจะได้ไม่ต้อง select ใหม่
                sql = "Select pm24.MNC_COM_CD,pm24.MNC_COM_DSC " +
                "From  patient_M24 pm24 Where status_insur = '1' ";
                dtInsur = conn.selectData(conn.connMainHIS, sql);
            }
            if (dtInsur.Rows.Count > 0)
            {
                foreach (DataRow row in dtInsur.Rows)
                {
                    autoSymptom.Add(row["MNC_COM_DSC"].ToString());
                }
            }
            return autoSymptom;
        }
        private void chkNull(PatientM24 p)
        {
            long chk = 0;
            int chk1 = 0;
            decimal chk2 = 0;

            p.MNC_COM_CD = p.MNC_COM_CD == null ? "" : p.MNC_COM_CD;
            p.MNC_COM_PRF_CD = p.MNC_COM_PRF_CD == null ? "" : p.MNC_COM_PRF_CD;
            p.MNC_COM_DSC = p.MNC_COM_DSC == null ? "" : p.MNC_COM_DSC;
            p.MNC_COM_TUM_CD = p.MNC_COM_TUM_CD == null ? "" : p.MNC_COM_TUM_CD;
            p.MNC_COM_AMP_CD = p.MNC_COM_AMP_CD == null ? "" : p.MNC_COM_AMP_CD;
            p.MNC_COM_CHW_CD = p.MNC_COM_CHW_CD == null ? "" : p.MNC_COM_CHW_CD;
            p.MNC_COM_POC = p.MNC_COM_POC == null ? "" : p.MNC_COM_POC;
            p.MNC_COM_TEL = p.MNC_COM_TEL == null ? "" : p.MNC_COM_TEL;
            p.MNC_COM_TYP_CD = p.MNC_COM_TYP_CD == null ? "" : p.MNC_COM_TYP_CD;
            p.MNC_COM_STS = p.MNC_COM_STS == null ? "" : p.MNC_COM_STS;
            p.MNC_COM_START = p.MNC_COM_START == null ? "" : p.MNC_COM_START;
            p.MNC_COM_DSC_E = p.MNC_COM_DSC_E == null ? "" : p.MNC_COM_DSC_E;
            p.status_insur = p.status_insur == null ? "" : p.status_insur;
            p.insur1_code = p.insur1_code == null ? "" : p.insur1_code;
            p.insur2_code = p.insur2_code == null ? "" : p.insur2_code;
            p.phone2 = p.phone2 == null ? "" : p.phone2;
            p.email = p.email == null ? "" : p.email;
            p.MNC_COM_NAM = p.MNC_COM_NAM == null ? "" : p.MNC_COM_NAM;
        }
        public String insertCompany(PatientM24 p, String userId)
        {
            String re = "";

            if (p.MNC_COM_CD.Equals(""))
            {
                re = insert(p, "");
            }
            else
            {
                re = update(p, "");
            }

            return re;
        }
        public String insert(PatientM24 p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            chkNull(p);
            sql = "Insert Into " + pm24.table + " (" + pm24.MNC_COM_CD + "," + pm24.MNC_COM_PRF_CD + "," + pm24.MNC_COM_DSC + "," +
                "" + pm24.MNC_COM_TUM_CD + "," + pm24.MNC_COM_AMP_CD + "," + pm24.MNC_COM_CHW_CD + "," +
                "" + pm24.MNC_COM_POC + "," + pm24.MNC_COM_TEL + "," + pm24.MNC_COM_TYP_CD + "," +
                "" + pm24.MNC_COM_STS + "," + pm24.MNC_COM_START + "," + pm24.MNC_COM_DSC_E + ", " +
                "" + pm24.status_insur + "," + pm24.insur1_code + "," + pm24.insur2_code + ", " +
                "" + pm24.phone2 + "," + pm24.email + "," + pm24.MNC_COM_NAM + " " +
               ") " +
                "Values ((select max(MNC_COM_CD) from "+ pm24.table + " Where mnc_com_cd not in ('DUMMY','SA001','SA002','SA003'))+1,'" + p.MNC_COM_PRF_CD + "','" + p.MNC_COM_DSC.Replace("'", "''") + "'," +
                "'" + p.MNC_COM_TUM_CD + "','" + p.MNC_COM_AMP_CD + "','" + p.MNC_COM_CHW_CD + "'," +
                "'" + p.MNC_COM_POC + "','" + p.MNC_COM_TEL + "','" + p.MNC_COM_TYP_CD + "'," +
                "'" + p.MNC_COM_STS + "',convert(varchar(20), getdate(),20),'" + p.MNC_COM_DSC_E.Replace("'", "''") + "'," +
                "'" + p.status_insur + "','" + p.insur1_code + "','" + p.insur2_code + "'," +
                "'" + p.phone2 + "','" + p.email + "','" + p.MNC_COM_NAM.Replace("'", "''") + "' " +
                ")";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                re = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "PatientM24DB insert error  " + ex.Message + " " + ex.InnerException);
                //bc.bcDB.insertLogPage(bc.userId, this.Name, "BtnPttSave_Click save image ", ex.Message);
            }

            return re;
        }
        public String update(PatientM24 p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            chkNull(p);
            sql = "Update " + pm24.table + " Set " + pm24.MNC_COM_PRF_CD + " = '" + p.MNC_COM_PRF_CD + "' " +
                "," + pm24.MNC_COM_DSC + " = '" + p.MNC_COM_DSC.Replace("'", "''") + "' " +
                "," + pm24.MNC_COM_TUM_CD + " = '" + p.MNC_COM_TUM_CD + "' " +
                "," + pm24.MNC_COM_AMP_CD + " = '" + p.MNC_COM_AMP_CD + "' " +
                "," + pm24.MNC_COM_CHW_CD + " = '" + p.MNC_COM_CHW_CD + "' " +
                "," + pm24.MNC_COM_POC + " = '" + p.MNC_COM_POC + "' " +
                "," + pm24.MNC_COM_TEL + " = '" + p.MNC_COM_TEL.Replace("'", "''") + "' " +
                "," + pm24.MNC_COM_TYP_CD + " = '" + p.MNC_COM_TYP_CD + "' " +
                "," + pm24.MNC_COM_STS + " = '" + p.MNC_COM_STS + "' " +
                "," + pm24.phone2 + " = '" + p.phone2 + "' " +
                "," + pm24.email + " = '" + p.email.Replace("'", "''") + "' " +
                "," + pm24.MNC_COM_NAM + " = '" + p.MNC_COM_NAM.Replace("'","''") + "' " +
                "," + pm24.status_insur + " = '" + p.status_insur + "' " +
                "," + pm24.name_old + " = "+ pm24.MNC_COM_DSC + " " +
               " " +
                "Where " + pm24.MNC_COM_CD + " = '" + p.MNC_COM_CD + "' " +
                " ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "PatientM24DB update error  " + ex.Message + " " + ex.InnerException);
            }

            return re;
        }
        public PatientM24 setPatientM24(DataTable dt)
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
                pm08.MNC_COM_START = dt.Rows[0]["MNC_COM_START"].ToString();
                pm08.MNC_COM_DSC_E = dt.Rows[0]["MNC_COM_DSC_E"].ToString();
                pm08.email = dt.Rows[0]["email"].ToString();
                pm08.phone2 = dt.Rows[0]["phone"].ToString();
                pm08.MNC_COM_NAM = dt.Rows[0]["MNC_COM_NAM"].ToString();
                pm08.name_old = dt.Rows[0]["name_old"].ToString();
            }
            else
            {
                setPatientM24(pm08);
            }
            return pm08;
        }
        public PatientM24 setPatientM24(PatientM24 p)
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
            p.MNC_COM_START = "";
            p.MNC_COM_DSC_E = "";
            p.email = "";
            p.phone2 = "";
            p.MNC_COM_NAM = "";
            p.name_old = "";
            return p;
        }
    }
}
