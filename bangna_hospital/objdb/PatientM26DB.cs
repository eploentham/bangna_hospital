using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class PatientM26DB
    {
        DataTable DTDRUG; internal AutoCompleteStringCollection AUTODTR;
        ConnectDB conn;
        PatientM26 pm26;
        public PatientM26DB(ConnectDB c) {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm26 = new PatientM26();
            pm26.MNC_NO = "MNC_NO";
            pm26.MNC_DOT_CD = "MNC_DOT_CD";
            pm26.MNC_DOT_CD_E = "MNC_DOT_CD_E";
            pm26.MNC_DOT_PFIX = "MNC_DOT_PFIX";
            pm26.MNC_DOT_PFIX_E = "MNC_DOT_PFIX_E";
            pm26.MNC_DOT_FNAME = "MNC_DOT_FNAME";
            pm26.MNC_DOT_LNAME = "MNC_DOT_LNAME";
            pm26.MNC_DOT_FNAME_E = "MNC_DOT_FNAME_E";
            pm26.MNC_DOT_LNAME_E = "MNC_DOT_LNAME_E";
            pm26.MNC_DOT_BDAY = "MNC_DOT_BDAY";
            pm26.MNC_DOT_AGE = "MNC_DOT_AGE";
            pm26.MNC_DOT_LIC_NO = "MNC_DOT_LIC_NO";
            pm26.MNC_DOT_STS = "MNC_DOT_STS";
            pm26.MNC_DEP_NO = "MNC_DEP_NO";
            pm26.MNC_SEC_NO = "MNC_SEC_NO";
            pm26.MNC_DIA_TIME = "MNC_DIA_TIME";
            pm26.MNC_SPC_NO = "MNC_SPC_NO";
            pm26.MNC_MED_TYP = "MNC_MED_TYP";
            pm26.MNC_CUR_ADD = "MNC_CUR_ADD";
            pm26.MNC_CUR_TEL = "MNC_CUR_TEL";
            pm26.MNC_REF_ADD = "MNC_REF_ADD";
            pm26.MNC_REF_TEL = "MNC_REF_TEL";
            pm26.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pm26.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pm26.MNC_USR_ADD = "MNC_USR_ADD";
            pm26.MNC_USR_UPD = "MNC_USR_UPD";
            pm26.MNC_DOT_OLD = "MNC_DOT_OLD";
            pm26.MNC_MOB_TEL = "MNC_MOB_TEL";
            pm26.MNC_PAG_TEL = "MNC_PAG_TEL";
            pm26.active = "active";
            pm26.MNC_APP_NO = "MNC_APP_NO";

            pm26.table = "PATIENT_M26";
            pm26.pkField = "MNC_DOT_CD";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String re = "";
            String sql = "select pm26.* " +
                "From " + pm26.table + " pm26 "
                ;
            dt = conn.selectData(conn.conn, sql);
            
            return dt;
        }
        public PatientM26 selectByPk(String dtrcode)
        {
            PatientM26 cop1 = new PatientM26();
            DataTable dt = new DataTable();
            String sql = "select pm26.*,pm02.MNC_PFIX_DSC " +
                "From " + pm26.table + " pm26 " +
                "Left Join PATIENT_M02 pm02 On pm26.MNC_DOT_PFIX = pm02.MNC_PFIX_CD " +
                "Where pm26." + pm26.pkField + " ='" + dtrcode + "' " +
                " ";
            dt = conn.selectData(conn.connMainHIS, sql);
            cop1 = setMedicalCert(dt);
            return cop1;
        }
        public DataTable SelectDtrAll()
        {
            DataTable dt = new DataTable();

            String sql = "select pm26.*,pm02.MNC_PFIX_DSC " +
                "From " + pm26.table + " pm26 " +
                "Left Join PATIENT_M02 pm02 On pm26.MNC_DOT_PFIX = pm02.MNC_PFIX_CD " +
                "Where pm26." + pm26.MNC_DOT_STS + " ='Y' " +
                " ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);   select_drug_map
            return dt;
        }
        public AutoCompleteStringCollection setAUTODTR()
        {
            //lDept = new List<Position>();
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            //labM01.Clear();
            if (DTDRUG == null || DTDRUG.Rows.Count <= 0)
            {
                DTDRUG = SelectDtrAll(); AUTODTR = new AutoCompleteStringCollection();
                foreach (DataRow row in DTDRUG.Rows)
                {
                    autoSymptom.Add(row["MNC_PFIX_DSC"].ToString() + " " + row["MNC_DOT_FNAME"].ToString()+" "+ row["MNC_DOT_LNAME"].ToString() + "#" + row["MNC_DOT_CD"].ToString());
                    AUTODTR.Add(row["MNC_PFIX_DSC"].ToString() + " " + row["MNC_DOT_FNAME"].ToString() + " " + row["MNC_DOT_LNAME"].ToString() + "#" + row["MNC_DOT_CD"].ToString());
                }
            }
            return autoSymptom;
        }
        public String updateAppoint(String dtrcode, String appoint)
        {
            String sql = "", re = "";
            sql = "update " + pm26.table + "  set " +
                "MNC_APP_NO = '"+ appoint + "' " +
                "Where MNC_DOT_CD = '" + dtrcode + "' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateAppoint sql " + sql);
            }
            return re;
        }
        public PatientM26 setMedicalCert(DataTable dt)
        {
            PatientM26 cert = new PatientM26();
            if (dt.Rows.Count > 0)
            {
                cert.active = dt.Rows[0][pm26.active].ToString();
                cert.MNC_NO = dt.Rows[0][pm26.MNC_NO].ToString();
                cert.MNC_DOT_CD = dt.Rows[0][pm26.MNC_DOT_CD].ToString();
                cert.MNC_DOT_CD_E = dt.Rows[0][pm26.MNC_DOT_CD_E].ToString();
                cert.MNC_DOT_PFIX = dt.Rows[0][pm26.MNC_DOT_PFIX].ToString();
                cert.MNC_DOT_PFIX_E = dt.Rows[0][pm26.MNC_DOT_PFIX_E].ToString();
                cert.MNC_DOT_FNAME = dt.Rows[0][pm26.MNC_DOT_FNAME].ToString();
                cert.MNC_DOT_LNAME = dt.Rows[0][pm26.MNC_DOT_LNAME].ToString();
                cert.MNC_DOT_FNAME_E = dt.Rows[0][pm26.MNC_DOT_FNAME_E].ToString();
                cert.MNC_DOT_LNAME_E = dt.Rows[0][pm26.MNC_DOT_LNAME_E].ToString();
                cert.MNC_DOT_BDAY = dt.Rows[0][pm26.MNC_DOT_BDAY].ToString();
                cert.MNC_DOT_AGE = dt.Rows[0][pm26.MNC_DOT_AGE].ToString();
                cert.MNC_DOT_LIC_NO = dt.Rows[0][pm26.MNC_DOT_LIC_NO].ToString();
                cert.MNC_DOT_STS = dt.Rows[0][pm26.MNC_DOT_STS].ToString();
                cert.MNC_DEP_NO = dt.Rows[0][pm26.MNC_DEP_NO].ToString();
                cert.MNC_SEC_NO = dt.Rows[0][pm26.MNC_SEC_NO].ToString();
                cert.MNC_DIA_TIME = dt.Rows[0][pm26.MNC_DIA_TIME].ToString();
                cert.MNC_SPC_NO = dt.Rows[0][pm26.MNC_SPC_NO].ToString();
                cert.MNC_MED_TYP = dt.Rows[0][pm26.MNC_MED_TYP].ToString();
                cert.MNC_CUR_ADD = dt.Rows[0][pm26.MNC_CUR_ADD].ToString();
                cert.MNC_CUR_TEL = dt.Rows[0][pm26.MNC_CUR_TEL].ToString();
                cert.MNC_REF_ADD = dt.Rows[0][pm26.MNC_REF_ADD].ToString();
                cert.MNC_REF_TEL = dt.Rows[0][pm26.MNC_REF_TEL].ToString();
                cert.MNC_STAMP_DAT = dt.Rows[0][pm26.MNC_STAMP_DAT].ToString();
                cert.MNC_STAMP_TIM = dt.Rows[0][pm26.MNC_STAMP_TIM].ToString();
                cert.MNC_USR_ADD = dt.Rows[0][pm26.MNC_USR_ADD].ToString();
                cert.MNC_USR_UPD = dt.Rows[0][pm26.MNC_USR_UPD].ToString();
                cert.MNC_DOT_OLD = dt.Rows[0][pm26.MNC_DOT_OLD].ToString();
                cert.MNC_MOB_TEL = dt.Rows[0][pm26.MNC_MOB_TEL].ToString();
                cert.MNC_APP_NO = dt.Rows[0][pm26.MNC_APP_NO].ToString();
                cert.MNC_PAG_TEL = dt.Rows[0][pm26.MNC_PAG_TEL].ToString();
                cert.dtrname = dt.Rows[0]["MNC_PFIX_DSC"].ToString()+" "+ dt.Rows[0][pm26.MNC_DOT_FNAME].ToString()+" "+ dt.Rows[0][pm26.MNC_DOT_LNAME].ToString();
            }
            else
            {
                setMedicalCert(cert);
            }
            return cert;
        }
        public PatientM26 setMedicalCert(PatientM26 dgs1)
        {
            dgs1.MNC_NO = "";
            dgs1.MNC_DOT_CD = "";
            dgs1.MNC_DOT_CD_E = "";
            dgs1.MNC_DOT_PFIX = "";
            dgs1.MNC_DOT_PFIX_E = "";
            dgs1.MNC_DOT_FNAME = "";
            dgs1.MNC_DOT_LNAME = "";
            dgs1.MNC_DOT_FNAME_E = "";
            dgs1.MNC_DOT_LNAME_E = "";
            dgs1.MNC_DOT_BDAY = "";
            dgs1.MNC_DOT_AGE = "";
            dgs1.MNC_DOT_LIC_NO = "";
            dgs1.MNC_DOT_STS = "";
            dgs1.MNC_DEP_NO = "";
            dgs1.MNC_SEC_NO = "";
            dgs1.MNC_DIA_TIME = "";
            dgs1.MNC_SPC_NO = "";
            dgs1.MNC_MED_TYP = "";
            dgs1.MNC_CUR_ADD = "";
            dgs1.MNC_CUR_TEL = "";
            dgs1.MNC_REF_ADD = "";
            dgs1.MNC_REF_TEL = "";
            dgs1.MNC_STAMP_DAT = "";
            dgs1.MNC_STAMP_TIM = "";
            dgs1.MNC_USR_ADD = "";
            dgs1.MNC_USR_UPD = "";
            dgs1.MNC_DOT_OLD = "";
            dgs1.MNC_MOB_TEL = "";
            dgs1.MNC_PAG_TEL = "";
            dgs1.active = "";
            dgs1.dtrname = "";
            dgs1.MNC_APP_NO = "";
            return dgs1;
        }
        public AutoCompleteStringCollection getlACM()
        {
            //lDept = new List<Position>();

            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            //dt = selectDeptOPDNew();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                autoSymptom.Add(row["MNC_DOT_CD"].ToString()+" "+ row["MNC_DOT_PFIX"].ToString() + " " + row["MNC_DOT_FNAME"].ToString() + " " + row["MNC_DOT_LNAME"].ToString());
            }
            return autoSymptom;
        }
    }
}
