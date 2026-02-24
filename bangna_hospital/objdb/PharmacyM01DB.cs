using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class PharmacyM01DB
    {
        DataTable DTDRUG, DTUSING, DTFRE, DTCAU, DTPROPER, DTINDICA;
        public PharmacyM01 pharM01;
        ConnectDB conn;
        internal AutoCompleteStringCollection AUTODrugTR, AUTODrugGN, AUTOUSING, AUTOUSING1, AUTOFRE, AUTOFRE1, AUTOINDICA, AUTOINDICA1, AUTOPROPER, AUTOPROPER1, AUTOCAU, AUTOCAU1;
        public PharmacyM01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharM01 = new PharmacyM01();
            pharM01.MNC_PH_CD = "MNC_PH_CD";
            pharM01.MNC_PH_ID = "MNC_PH_ID";
            pharM01.MNC_PH_CTL_CD = "MNC_PH_CTL_CD";
            pharM01.MNC_PH_TN = "MNC_PH_TN";
            pharM01.MNC_PH_GN = "MNC_PH_GN";
            pharM01.MNC_PH_UNT_CD = "MNC_PH_UNT_CD";
            pharM01.MNC_PH_GRP_CD = "MNC_PH_GRP_CD";
            pharM01.MNC_PH_TYP_CD = "MNC_PH_TYP_CD";
            pharM01.MNC_PH_DIR_CD = "MNC_PH_DIR_CD";
            pharM01.MNC_PH_CAU_CD = "MNC_PH_CAU_CD";
            pharM01.MNC_PH_MIN = "MNC_PH_MIN";
            pharM01.MNC_PH_MAX = "MNC_PH_MAX";
            pharM01.MNC_PH_DAY = "MNC_PH_DAY";
            pharM01.MNC_PR_STD = "MNC_PR_STD";
            pharM01.MNC_SUP_STS = "MNC_SUP_STS";
            pharM01.MNC_PRO_LOC = "MNC_PRO_LOC";
            pharM01.MNC_PH_AT = "MNC_PH_AT";
            pharM01.MNC_FN_CD = "MNC_FN_CD";
            pharM01.MNC_PH_STS = "MNC_PH_STS";
            pharM01.MNC_PH_LQ = "MNC_PH_LQ";
            pharM01.MNC_PH_LV = "MNC_PH_LV";
            pharM01.MNC_PH_LI = "MNC_PH_LI";
            pharM01.MNC_PH_LC = "MNC_PH_LC";
            pharM01.MNC_PH_LD = "MNC_PH_LD";
            pharM01.MNC_PH_DIS_STS = "MNC_PH_DIS_STS";
            pharM01.MNC_PH_DIV_CD = "MNC_PH_DIV_CD";
            pharM01.MNC_PH_FRE_CD = "MNC_PH_FRE_CD";
            pharM01.MNC_PH_TIM_CD = "MNC_PH_TIM_CD";
            pharM01.MNC_PH_IND_CD = "MNC_PH_IND_CD";
            pharM01.MNC_GRP_PH_CD = "MNC_GRP_PH_CD";
            pharM01.MNC_PH_GRP_SUB_CD = "MNC_PH_GRP_SUB_CD";
            pharM01.MNC_PH_DIR_TXT = "MNC_PH_DIR_TXT";
            pharM01.MNC_DEC_CD = "MNC_DEC_CD";
            pharM01.MNC_DEC_NO = "MNC_DEC_NO";
            pharM01.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pharM01.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pharM01.MNC_USR_ADD = "MNC_USR_ADD";
            pharM01.MNC_USR_UPD = "MNC_USR_UPD";
            pharM01.MNC_PH_PRI = "MNC_PH_PRI";
            pharM01.MNC_PH_PRO_STS = "MNC_PH_PRO_STS";
            pharM01.MNC_DOSAGE_FORM = "MNC_DOSAGE_FORM";
            pharM01.MNC_PH_STRENGTH = "MNC_PH_STRENGTH";
            pharM01.MNC_PH_COMM_THAI = "MNC_PH_COMM_THAI";
            pharM01.MNC_PH_COMM_ENG = "MNC_PH_COMM_ENG";
            pharM01.MNC_PH_DIV_CD_I = "MNC_PH_DIV_CD_I";
            pharM01.MNC_PH_MAT_FLG = "MNC_PH_MAT_FLG";
            pharM01.MNC_PH_TYP_FLG = "MNC_PH_TYP_FLG";
            pharM01.MNC_VEN_CD = "MNC_VEN_CD";
            pharM01.MNC_OLD_CD = "MNC_OLD_CD";
            pharM01.MNC_PH_WRN_CD = "MNC_PH_WRN_CD";
            pharM01.MNC_PH_AUTO = "MNC_PH_AUTO";
            pharM01.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            pharM01.MNC_PRINT_FLG = "MNC_PRINT_FLG";
            pharM01.MNC_PH_NEW_SS = "MNC_PH_NEW_SS";
            pharM01.tmt_code = "tmt_code";
            pharM01.CodeBSG = "CodeBSG";
        }
        public AutoCompleteStringCollection getlDrugAllTherd()
        {
            //lDept = new List<Position>();
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            //labM01.Clear();
            DataTable dt = new DataTable();
            String connstring= conn.connMainHIS.ConnectionString;
            String sql = "select pm01.*, pm05.mnc_ph_pri01, pm05.mnc_ph_pri02, pm05.mnc_ph_pri03 " +
                "From pharmacy_m01 pm01 " +
                "inner join pharmacy_m05 pm05 on pm01.mnc_ph_cd = pm05.mnc_ph_cd " +
                "Where mnc_ph_typ_flg = 'P' " +
                "Order By pm01.MNC_PH_CD";
            using (SqlConnection conn1 = new SqlConnection(connstring))
            {
                conn1.Open();
                // query
                SqlCommand comMainhis = new SqlCommand();
                comMainhis.CommandText = sql;
                comMainhis.CommandType = CommandType.Text;
                comMainhis.Connection = conn1;
                comMainhis.CommandTimeout = 60;
                SqlDataAdapter adapMainhis = new SqlDataAdapter(comMainhis);
                try
                {
                    //new LogWriter("e", "ConnectDB selectData con.ConnectionString " + con.ConnectionString);

                    adapMainhis.Fill(dt);
                    //return toReturn;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    conn1.Close();
                    comMainhis.Dispose();
                    adapMainhis.Dispose();
                }
            }
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                autoSymptom.Add(row["MNC_PH_TN"].ToString() + "#" + row["MNC_PH_CD"].ToString());
                //PatientM13 cus1 = new PatientM13();
                //cus1.MNC_APP_CD = row["MNC_APP_CD"].ToString();
                //cus1.MNC_APP_DSC = row["MNC_APP_DSC"].ToString();

                //labM01.Add(cus1);
            }
            return autoSymptom;
        }
        public AutoCompleteStringCollection setAUTODrug()
        {
            //lDept = new List<Position>();
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            //labM01.Clear();
            if (DTDRUG == null || DTDRUG.Rows.Count <= 0)
            {
                DTDRUG = SelectDrugAll(); AUTODrugTR = new AutoCompleteStringCollection(); AUTODrugGN = new AutoCompleteStringCollection();
                foreach (DataRow row in DTDRUG.Rows)
                {
                    autoSymptom.Add(row["MNC_PH_TN"].ToString() + "#" + row["MNC_PH_CD"].ToString());
                    AUTODrugTR.Add(row["MNC_PH_TN"].ToString() + "#" + row["MNC_PH_CD"].ToString());
                    AUTODrugGN.Add(row["MNC_PH_GN"].ToString() + "#" + row["MNC_PH_CD"].ToString());
                }
            }
            return autoSymptom;
        }
        public AutoCompleteStringCollection getAUTOUsing()
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            dt = SelectUsingAllThai();
            foreach (DataRow row in dt.Rows)
            {
                autoSymptom.Add(row["MNC_PH_DIR_CD"].ToString() + "#" + row["MNC_PH_DIR_DSC"].ToString());
            }
            return autoSymptom;
        }
        public AutoCompleteStringCollection setAUTOUsingDesc()
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            if(DTUSING == null || DTUSING.Rows.Count <= 0)
            {
                AUTOUSING = new AutoCompleteStringCollection();
                AUTOUSING1 = new AutoCompleteStringCollection();
                DTUSING = SelectUsingAllThai();
                foreach (DataRow row in DTUSING.Rows)
                {
                    autoSymptom.Add(row["MNC_PH_DIR_DSC"].ToString());
                    AUTOUSING.Add(row["MNC_PH_DIR_DSC"].ToString());
                    AUTOUSING1.Add(row["MNC_PH_DIR_CD"].ToString() + "#" + row["MNC_PH_DIR_DSC"].ToString());
                }
            }
            return autoSymptom;
        }
        public AutoCompleteStringCollection setAUTOFrequency()
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            if(DTFRE == null || DTFRE.Rows.Count <= 0)
            {
                AUTOFRE = new AutoCompleteStringCollection(); AUTOFRE1 = new AutoCompleteStringCollection();
                DTFRE = SelectFrequencyAllThai();
                foreach (DataRow row in DTFRE.Rows)
                {
                    autoSymptom.Add(row["MNC_PH_FRE_CD"].ToString() + "#" + row["MNC_PH_FRE_DSC"].ToString());
                    AUTOFRE1.Add(row["MNC_PH_FRE_CD"].ToString() + "#" + row["MNC_PH_FRE_DSC"].ToString());
                    AUTOFRE.Add(row["MNC_PH_FRE_DSC"].ToString());
                }
            }
            return autoSymptom;
        }
        public AutoCompleteStringCollection getAUTOFrequencyDesc()
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            dt = SelectFrequencyAllThai();
            foreach (DataRow row in dt.Rows)
            {
                autoSymptom.Add(row["MNC_PH_FRE_DSC"].ToString());
            }
            return autoSymptom;
        }
        public AutoCompleteStringCollection getAUTOPrecaution()
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            dt = SelectPrecautionAllThai();
            foreach (DataRow row in dt.Rows)
            {
                autoSymptom.Add(row["MNC_PH_CAU_CD"].ToString() + "#" + row["MNC_PH_CAU_DSC"].ToString());
            }
            return autoSymptom;
        }
        public AutoCompleteStringCollection getAUTOPrecautionDesc()
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            //dt = SelectPrecautionAllThai();
            if((DTCAU==null) || DTCAU.Rows.Count == 0)
            {
                DTCAU = SelectPrecautionAllThai(); AUTOCAU = new AutoCompleteStringCollection(); AUTOCAU1 = new AutoCompleteStringCollection();
                foreach (DataRow row in DTCAU.Rows)
                {
                    autoSymptom.Add(row["MNC_PH_CAU_DSC"].ToString());
                    AUTOCAU.Add(row["MNC_PH_CAU_DSC"].ToString());
                    AUTOCAU1.Add(row["MNC_PH_CAU_CD"].ToString() + "#" + row["MNC_PH_CAU_DSC"].ToString());
                }
            }
            
            return autoSymptom;
        }
        public AutoCompleteStringCollection getAUTOIndication()
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            dt = SelectIndicationAllThai();
            foreach (DataRow row in dt.Rows)
            {
                autoSymptom.Add(row["MNC_PH_TIM_CD"].ToString() + "#" + row["MNC_PH_TIM_DSC"].ToString());
            }
            return autoSymptom;
        }
        public AutoCompleteStringCollection setAUTOIndicationDesc()
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            //dt = SelectIndicationAllThai();
            if((DTINDICA==null) || (DTINDICA.Rows.Count <= 0))
            {
                DTINDICA = SelectIndicationAllThai(); AUTOINDICA = new AutoCompleteStringCollection(); AUTOINDICA1 = new AutoCompleteStringCollection();
                foreach (DataRow row in DTINDICA.Rows)
                {
                    autoSymptom.Add(row["MNC_PH_TIM_DSC"].ToString());
                    AUTOINDICA.Add(row["MNC_PH_TIM_DSC"].ToString());
                    AUTOINDICA1.Add(row["MNC_PH_TIM_CD"].ToString() + "#" + row["MNC_PH_TIM_DSC"].ToString());
                }
            }
            return autoSymptom;
        }
        public AutoCompleteStringCollection setAUTOProperties()
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            dt = SelectPropertiesAllThai();
            foreach (DataRow row in dt.Rows)
            {
                autoSymptom.Add(row["MNC_PH_IND_CD"].ToString() + "#" + row["MNC_PH_IND_DSC"].ToString());
                AUTOPROPER.Add(row["MNC_PH_IND_DSC"].ToString());
                AUTOPROPER1.Add(row["MNC_PH_IND_CD"].ToString() + "#" + row["MNC_PH_IND_DSC"].ToString());
            }
            return autoSymptom;
        }
        public AutoCompleteStringCollection getAUTOPropertiesDesc()
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            //dt = SelectPropertiesAllThai();
            if((DTPROPER==null) || (DTPROPER.Rows.Count <= 0))
            {
                DTPROPER = SelectPropertiesAllThai();
                foreach (DataRow row in DTPROPER.Rows)
                {
                    autoSymptom.Add(row["MNC_PH_IND_DSC"].ToString());
                    AUTOPROPER.Add(row["MNC_PH_IND_DSC"].ToString());
                    AUTOPROPER1.Add(row["MNC_PH_IND_CD"].ToString() + "#" + row["MNC_PH_IND_DSC"].ToString());
                }
            }
            return autoSymptom;
        }
        public DataTable SelectPropertiesAllThai()
        {
            DataTable dt = new DataTable();

            String sql = "select phar24.MNC_PH_IND_DSC, phar24.MNC_PH_IND_CD " +
                "From PHARMACY_M24 phar24 " +
                " " +
                "Order By phar24.MNC_PH_IND_DSC ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectIndicationAllThai()
        {
            DataTable dt = new DataTable();

            String sql = "select phar22.MNC_PH_TIM_DSC, phar22.MNC_PH_TIM_CD " +
                "From PHARMACY_M22 phar22 " +
                " " +
                "Order By phar22.MNC_PH_TIM_DSC ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectPrecautionAllThai()
        {
            DataTable dt = new DataTable();

            String sql = "select phar11.MNC_PH_CAU_DSC, phar11.MNC_PH_CAU_CD " +
                "From PHARMACY_M11 phar11 " +
                " " +
                "Order By phar11.MNC_PH_CAU_DSC ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectFrequencyAllThai()
        {
            DataTable dt = new DataTable();

            String sql = "select phar21.MNC_PH_FRE_CD, phar21.MNC_PH_FRE_DSC " +
                "From PHARMACY_M21 phar21 " +
                " " +
                "Order By phar21.MNC_PH_FRE_DSC ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectUsingAllThai()
        {
            DataTable dt = new DataTable();

            String sql = "select phar04.MNC_PH_DIR_CD, phar04.MNC_PH_DIR_DSC " +
                "From pharmacy_m04 phar04 " +
                " " +
                "Order By phar04.MNC_PH_DIR_DSC ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectDrugMap(String drug)
        {
            DataTable dt = new DataTable();
            String wheredrug = "";
            if(drug.Equals("drug"))             {                wheredrug = "P";            }
            else if(drug.Equals("supply"))      {                wheredrug = "O"; }
            String sql = "select pm01.MNC_PH_CD, pm01.MNC_PH_GN, pm01.MNC_PH_TN, pm01.MNC_PH_GN,isnull(pm01.MNC_FN_CD,'') as MNC_FN_CD, isnull(fm11.MNC_DEF_DSC,'') as MNC_DEF_DSC " +
                ",simb2.CodeBSG,simb2.BillingSubGroupTH,simb2.BillingSubGroupEN,fm01.MNC_SIMB_CD,fm01.MNC_CHARGE_CD,fm01.MNC_SUB_CHARGE_CD, '' as MNC_CHARGE_NO  " +
                "From pharmacy_m01 pm01 " +
                "left join FINANCE_M01 fm01 on pm01.MNC_FN_CD = fm01.MNC_FN_CD " +
                "Left join Finance_m11 fm11 on fm01.MNC_SIMB_CD = fm11.MNC_DEF_CD " +
                "Left Join SIMB2_BillingGroup simb2 on pm01.CodeBSG = simb2.CodeBSG  " +
                "Where mnc_ph_typ_flg = '" + wheredrug + "' " +
                "Order By pm01.MNC_PH_CD";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectSimbG2()
        {
            DataTable dt = new DataTable();

            String sql = "select simb.* " +
                "From SIMB2_BillingGroup simb " +
                " " +
                "Order By simb.ID ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectDrugMapFromSP()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn1 = new SqlConnection(conn.connMainHIS.ConnectionString))
            {
                //using (SqlCommand cmd = new SqlCommand("select_drug_map_claude", conn1))  //ใช้ PIVOT ไม่เข้าใจ แก้ไขไม่ได้
                using (SqlCommand cmd = new SqlCommand("select_drug_map", conn1))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // If your stored procedure requires parameters, add them here:
                    // cmd.Parameters.AddWithValue("@paramName", value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            conn1.Open();
                            da.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            // Handle exception as needed
                            throw new Exception("Error executing select_drug_map_claude: " + ex.Message, ex);
                        }
                    }
                }
            }
            return dt;
        }
        public DataTable SelectDrugAll()
        {
            DataTable dt = new DataTable();

            String sql = "select pm01.*, pm05.mnc_ph_pri01, pm05.mnc_ph_pri02, pm05.mnc_ph_pri03 " +
                "From pharmacy_m01 pm01 " +
                "inner join pharmacy_m05 pm05 on pm01.mnc_ph_cd = pm05.mnc_ph_cd " +
                "Where mnc_ph_typ_flg = 'P' " + 
                "Order By pm01.MNC_PH_CD";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);   select_drug_map
            return dt;
        }
        public DataTable SelectDrugAll1()
        {
            DataTable dt = new DataTable();

            String sql = "select pm01.MNC_PH_CD,pm01.MNC_PH_ID,pm01.MNC_PH_TN,pm01.MNC_PH_UNT_CD,pm01.MNC_PH_GRP_CD,pm01.MNC_PH_TYP_CD,pm01.MNC_PH_MIN,pm01.MNC_PH_MAX,pm01.MNC_PH_DAY " +
                ", pm05.mnc_ph_pri01, pm05.mnc_ph_pri02, pm05.mnc_ph_pri03, isnull(pm01.onhand,'0') as onhand " +
                "From pharmacy_m01 pm01 " +
                "inner join pharmacy_m05 pm05 on pm01.mnc_ph_cd = pm05.mnc_ph_cd " +
                "left join PHARMACY_M03 pm03 on pm01.MNC_PH_UNT_CD = pm03.MNC_PH_UNT_CD " +
                "Where mnc_ph_typ_flg = 'P' " +
                "Order By pm01.MNC_PH_CD";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select pm01.*, pm05.mnc_ph_pri01, pm05.mnc_ph_pri02, pm05.mnc_ph_pri03 " +
                "From pharmacy_m01 pm01 " +
                "inner join pharmacy_m05 pm05 on pm01.mnc_ph_cd = pm05.mnc_ph_cd " +
                //"Where MNC_FN_TYP_STS = 'Y' " +and pm01.mnc_ph_typ_flg = 'P'
                "Order By pm01.MNC_PH_CD";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectAllByGroup(String labgrpcode)
        {
            DataTable dt = new DataTable();

            String sql = "select pm01.*, pm05.mnc_ph_pri01, pm05.mnc_ph_pri02, pm05.mnc_ph_pri03,pm14.MNC_PH_GRP_DSC " +
                "From pharmacy_m01 pm01 " +
                "inner join pharmacy_m05 pm05 on pm01.mnc_ph_cd = pm05.mnc_ph_cd " +
                "left join pharmacy_m14 pm14 on pm01.MNC_PH_GRP_CD = pm14.MNC_PH_GRP_CD " +
                "Where pm01.MNC_PH_GRP_CD = '" + labgrpcode + "' " +
                "Order By pm01.MNC_PH_CD";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public String SelectNameByPk(String labgrpcode)
        {
            DataTable dt = new DataTable();
            String re = "";
            String sql = "select pm01.* " +
                "From pharmacy_m01 pm01 " +
                "Where pm01.mnc_ph_cd = '" + labgrpcode + "' " +
                " ";
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_PH_TN"].ToString();
            }
            return re;
        }
        public PharmacyM01 SelectNameByPk1(String labgrpcode)
        {
            PharmacyM01 pharm01 = new PharmacyM01();
            DataTable dt = new DataTable();
            String re = "";
            String sql = "select pm01.*" +//ใช้ set object ก็ใช้เป็น *
                ",pm04.mnc_ph_dir_dsc,pm11.MNC_PH_CAU_dsc,pm22.MNC_PH_TIM_DSC,pm24.MNC_PH_IND_DSC, pm21.MNC_PH_FRE_DSC " +
                "From pharmacy_m01 pm01 " +//PHARMACY_M21
                "Left join pharmacy_m04 pm04 on pm01.MNC_PH_DIR_CD = pm04.MNC_PH_DIR_CD " +//จริงๆคือ Using วิธีใช้
                "Left join PHARMACY_M11 pm11 on pm01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD " +//Precautions จริงๆคือ คำเตือน
                "Left join PHARMACY_M22 pm22 on pm01.MNC_PH_TIM_CD = pm22.MNC_PH_TIM_CD " +//Indication ข้อบ่งชี้
                "Left join PHARMACY_M24 pm24 on pm01.MNC_PH_IND_CD = pm24.MNC_PH_IND_CD " +//สรรพคุณ
                "Left join PHARMACY_M21 pm21 on pm01.MNC_PH_FRE_CD = pm21.MNC_PH_FRE_CD " +//ความถี่
                //"Left Join b_paid_map pmap on pm01.MNC_PH_CD = pmap.MNC_PH_CD " +     //join ไม่ได้ เพราะ มีหลายrecord
                "Where pm01.mnc_ph_cd = '" + labgrpcode + "' " +
                " ";
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)
            {
                pharm01 = setPharmacyM01(dt);
            }
            return pharm01;
        }
        public String UpdateTMTCodeOPBKK(String drugcode, String tmtcode)
        {
            String sql = "", chk = "";
            sql = "Update pharmacy_m01 Set " +
                " tmt_code_opbkk = '" + tmtcode + "' " +
                "Where mnc_ph_cd ='" + drugcode + "'";
            try
            {
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
            }

            return chk;
        }
        /*
         * for UCEP
         */ 
        public String UpdateTMTCode(String drugcode, String tmtcode)
        {
            String sql = "", chk = "";
            sql = "Update pharmacy_m01 Set " +
                " tmt_code = '" + tmtcode + "' " +
                //", tmt_code_opbkk = '" + tmtcode + "' " +
                "Where mnc_ph_cd ='" + drugcode + "'";
            try
            {
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
            }

            return chk;
        }
        public String UpdateTMTCodeCIPN(String drugcode, String tmtcode)
        {
            String sql = "", chk = "";
            sql = "Update pharmacy_m01 Set " +
                " tmt_code_cipn = '" + tmtcode + "' " +
                //", tmt_code_opbkk = '" + tmtcode + "' " +
                "Where mnc_ph_cd ='" + drugcode + "'";
            try
            {
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
            }

            return chk;
        }
        public String UpdateOPBKKCode(String drugcode, String tmtcode)
        {
            String sql = "", chk = "";
            sql = "Update pharmacy_m01 Set opbkk_code = '" + tmtcode + "' " +
                "Where mnc_ph_cd ='" + drugcode + "'";
            try
            {
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
            }

            return chk;
        }
        public String UpdateCodeBSG(String drugcode, String codebsg)
        {
            String sql = "", chk = "";
            sql = "Update pharmacy_m01 Set CodeBSG = '" + codebsg + "' " +
                "Where mnc_ph_cd ='" + drugcode + "'";
            try
            {
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "PharmacyM01DB UpdateCodeBSG Exception " + ex.Message);
            }
            return chk;
        }
        public String SelectPriceByTmtCode(String tmtcode)
        {
            DataTable dt = new DataTable();
            String sql = "", chk = "";
            sql = "Select pm05.mnc_ph_pri01,pm05.mnc_ph_pri02,pm05.mnc_ph_cos, pm01.tmt_code, pm01.mnc_ph_cd " +
                "From pharmacy_m01 pm01 " +
                "inner join pharmacy_m05 pm05 on pm01.mnc_ph_cd = pm05.mnc_ph_cd " +
                "Where tmt_code ='" + tmtcode + "'";
            try
            {
                dt = conn.selectData(conn.connMainHIS, sql);
                if (dt.Rows.Count > 0)
                {
                    chk = dt.Rows[0]["mnc_ph_pri01"].ToString()+"#"+ dt.Rows[0]["mnc_ph_pri02"].ToString();
                }
                
            }
            catch (Exception ex)
            {
            }

            return chk;
        }
        public String SelectHosCodeByTmtCode(String tmtcode)
        {
            DataTable dt = new DataTable();
            String sql = "", chk = "";
            sql = "Select pm01.mnc_ph_cd,pm01.MNC_OLD_CD " +
                "From pharmacy_m01 pm01 " +
                "inner join pharmacy_m05 pm05 on pm01.mnc_ph_cd = pm05.mnc_ph_cd " +
                "Where tmt_code ='" + tmtcode + "'";
            try
            {
                dt = conn.selectData(conn.connMainHIS, sql);
                if (dt.Rows.Count > 0)
                {
                    chk = dt.Rows[0]["mnc_ph_cd"].ToString() + "#" + dt.Rows[0]["MNC_OLD_CD"].ToString();
                }

            }
            catch (Exception ex)
            {
            }

            return chk;
        }
        public PharmacyM01 setPharmacyM01(DataTable dt)
        {
            PharmacyM01 pharM01 = new PharmacyM01();
            if (dt.Rows.Count > 0)
            {
                pharM01.MNC_PH_CD = dt.Rows[0]["MNC_PH_CD"].ToString();
                pharM01.MNC_PH_ID = dt.Rows[0]["MNC_PH_ID"].ToString();
                pharM01.MNC_PH_CTL_CD = dt.Rows[0]["MNC_PH_CTL_CD"].ToString();
                pharM01.MNC_PH_TN = dt.Rows[0]["MNC_PH_TN"].ToString();
                pharM01.MNC_PH_GN = dt.Rows[0]["MNC_PH_GN"].ToString();
                pharM01.MNC_PH_UNT_CD = dt.Rows[0]["MNC_PH_UNT_CD"].ToString();
                pharM01.MNC_PH_GRP_CD = dt.Rows[0]["MNC_PH_GRP_CD"].ToString();
                pharM01.MNC_PH_TYP_CD = dt.Rows[0]["MNC_PH_TYP_CD"].ToString();
                pharM01.MNC_PH_DIR_CD = dt.Rows[0]["MNC_PH_DIR_CD"].ToString();
                pharM01.MNC_PH_CAU_CD = dt.Rows[0]["MNC_PH_CAU_CD"].ToString();
                pharM01.MNC_PH_MIN = dt.Rows[0]["MNC_PH_MIN"].ToString();
                pharM01.MNC_PH_MAX = dt.Rows[0]["MNC_PH_MAX"].ToString();
                pharM01.MNC_PH_DAY = dt.Rows[0]["MNC_PH_DAY"].ToString();
                pharM01.MNC_PR_STD = dt.Rows[0]["MNC_PR_STD"].ToString();
                pharM01.MNC_SUP_STS = dt.Rows[0]["MNC_SUP_STS"].ToString();
                pharM01.MNC_PRO_LOC = dt.Rows[0]["MNC_PRO_LOC"].ToString();
                pharM01.MNC_PH_AT = dt.Rows[0]["MNC_PH_AT"].ToString();
                pharM01.MNC_FN_CD = dt.Rows[0]["MNC_FN_CD"].ToString();
                pharM01.MNC_PH_STS = dt.Rows[0]["MNC_PH_STS"].ToString();
                pharM01.MNC_PH_LQ = dt.Rows[0]["MNC_PH_LQ"].ToString();
                pharM01.MNC_PH_LV = dt.Rows[0]["MNC_PH_LV"].ToString();
                pharM01.MNC_PH_LI = dt.Rows[0]["MNC_PH_LI"].ToString();
                pharM01.MNC_PH_LC = dt.Rows[0]["MNC_PH_LC"].ToString();
                pharM01.MNC_PH_LD = dt.Rows[0]["MNC_PH_LD"].ToString();
                pharM01.MNC_PH_DIS_STS = dt.Rows[0]["MNC_PH_DIS_STS"].ToString();
                pharM01.MNC_PH_DIV_CD = dt.Rows[0]["MNC_PH_DIV_CD"].ToString();
                pharM01.MNC_PH_FRE_CD = dt.Rows[0]["MNC_PH_FRE_CD"].ToString();
                pharM01.MNC_PH_TIM_CD = dt.Rows[0]["MNC_PH_TIM_CD"].ToString();
                pharM01.MNC_PH_IND_CD = dt.Rows[0]["MNC_PH_IND_CD"].ToString();
                pharM01.MNC_GRP_PH_CD = dt.Rows[0]["MNC_GRP_PH_CD"].ToString();
                pharM01.MNC_PH_GRP_SUB_CD = dt.Rows[0]["MNC_PH_GRP_SUB_CD"].ToString();
                pharM01.MNC_PH_DIR_TXT = dt.Rows[0]["MNC_PH_DIR_TXT"].ToString();
                pharM01.MNC_DEC_CD = dt.Rows[0]["MNC_DEC_CD"].ToString();
                pharM01.MNC_DEC_NO = dt.Rows[0]["MNC_DEC_NO"].ToString();
                pharM01.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                pharM01.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                pharM01.MNC_USR_ADD = dt.Rows[0]["MNC_USR_ADD"].ToString();
                pharM01.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                pharM01.MNC_PH_PRI = dt.Rows[0]["MNC_PH_PRI"].ToString();
                pharM01.MNC_PH_PRO_STS = dt.Rows[0]["MNC_PH_PRO_STS"].ToString();
                pharM01.MNC_DOSAGE_FORM = dt.Rows[0]["MNC_DOSAGE_FORM"].ToString();
                pharM01.MNC_PH_STRENGTH = dt.Rows[0]["MNC_PH_STRENGTH"].ToString();
                pharM01.MNC_PH_COMM_THAI = dt.Rows[0]["MNC_PH_COMM_THAI"].ToString();
                pharM01.MNC_PH_COMM_ENG = dt.Rows[0]["MNC_PH_COMM_ENG"].ToString();
                pharM01.MNC_PH_DIV_CD_I = dt.Rows[0]["MNC_PH_DIV_CD_I"].ToString();
                pharM01.MNC_PH_MAT_FLG = dt.Rows[0]["MNC_PH_MAT_FLG"].ToString();
                pharM01.MNC_PH_TYP_FLG = dt.Rows[0]["MNC_PH_TYP_FLG"].ToString();
                pharM01.MNC_VEN_CD = dt.Rows[0]["MNC_VEN_CD"].ToString();
                pharM01.MNC_OLD_CD = dt.Rows[0]["MNC_OLD_CD"].ToString();
                pharM01.MNC_PH_WRN_CD = dt.Rows[0]["MNC_PH_WRN_CD"].ToString();
                pharM01.MNC_PH_AUTO = dt.Rows[0]["MNC_PH_AUTO"].ToString();
                pharM01.MNC_FN_TYP_CD = dt.Rows[0]["MNC_FN_TYP_CD"].ToString();
                pharM01.MNC_PRINT_FLG = dt.Rows[0]["MNC_PRINT_FLG"].ToString();
                pharM01.MNC_PH_NEW_SS = dt.Rows[0]["MNC_PH_NEW_SS"].ToString();
                pharM01.tmt_code = dt.Rows[0]["tmt_code"].ToString();
                pharM01.MNC_PH_THAI = dt.Rows[0]["MNC_PH_THAI"].ToString();
                //pharmacy_m04.MNC_PH_DIR_DSC วิธีใช้
                //PHARMACY_M21.MNC_PH_FRE_DSC ความถี่
                //PHARMACY_M22.MNC_PH_TIM_DSC ข้อบ่งชี้
                //PHARMACY_M11.MNC_PH_CAU_DSC คำเตือน
                //PHARMACY_M24.MNC_PH_IND_DSC สรรพคุณ
                pharM01.using1 = dt.Rows[0]["MNC_PH_DIR_DSC"].ToString().Replace("/", "").Trim();   //using1 วิธีใช้
                pharM01.frequency = dt.Rows[0]["MNC_ph_FRE_dsc"].ToString().Replace("/", "").Trim();//Frequency  ความถี่
                pharM01.indication = dt.Rows[0]["MNC_PH_TIM_DSC"].ToString().Replace("/", "").Trim();//Indication ข้อบ่งชี้
                pharM01.precautions = dt.Rows[0]["MNC_ph_cau_dsc"].ToString().Replace("/","").Trim();//Precautions  คำเตือน
                pharM01.properties = dt.Rows[0]["MNC_PH_IND_DSC"].ToString().Replace("/", "").Trim();//Precautions  สรรพคุณ
                pharM01.interaction = dt.Rows[0]["drug_interaction"].ToString();
                pharM01.tmt_code_opbkk = dt.Rows[0]["tmt_code_opbkk"].ToString().Replace("/", "").Trim();
                pharM01.CodeBSG = dt.Rows[0]["CodeBSG"].ToString().Trim();
            }
            else
            {
                setPharmacyM01(pharM01);
            }
            return pharM01;
        }
        public PharmacyM01 setPharmacyM01(PharmacyM01 p)
        {
            p.MNC_PH_CD = "";
            p.MNC_PH_ID = "";
            p.MNC_PH_CTL_CD = "";
            p.MNC_PH_TN = "";
            p.MNC_PH_GN = "";
            p.MNC_PH_UNT_CD = "";
            p.MNC_PH_GRP_CD = "";
            p.MNC_PH_TYP_CD = "";
            p.MNC_PH_DIR_CD = "";
            p.MNC_PH_CAU_CD = "";
            p.MNC_PH_MIN = "";
            p.MNC_PH_MAX = "";
            p.MNC_PH_DAY = "";
            p.MNC_PR_STD = "";
            p.MNC_SUP_STS = "";
            p.MNC_PRO_LOC = "";
            p.MNC_PH_AT = "";
            p.MNC_FN_CD = "";
            p.MNC_PH_STS = "";
            p.MNC_PH_LQ = "";
            p.MNC_PH_LV = "";
            p.MNC_PH_LI = "";
            p.MNC_PH_LC = "";
            p.MNC_PH_LD = "";
            p.MNC_PH_DIS_STS = "";
            p.MNC_PH_DIV_CD = "";
            p.MNC_PH_FRE_CD = "";
            p.MNC_PH_TIM_CD = "";
            p.MNC_PH_IND_CD = "";
            p.MNC_GRP_PH_CD = "";
            p.MNC_PH_GRP_SUB_CD = "";
            p.MNC_PH_DIR_TXT = "";
            p.MNC_DEC_CD = "";
            p.MNC_DEC_NO = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_ADD = "";
            p.MNC_USR_UPD = "";
            p.MNC_PH_PRI = "";
            p.MNC_PH_PRO_STS = "";
            p.MNC_DOSAGE_FORM = "";
            p.MNC_PH_STRENGTH = "";
            p.MNC_PH_COMM_THAI = "";
            p.MNC_PH_COMM_ENG = "";
            p.MNC_PH_DIV_CD_I = "";
            p.MNC_PH_MAT_FLG = "";
            p.MNC_PH_TYP_FLG = "";
            p.MNC_VEN_CD = "";
            p.MNC_OLD_CD = "";
            p.MNC_PH_WRN_CD = "";
            p.MNC_PH_AUTO = "";
            p.MNC_FN_TYP_CD = "";
            p.MNC_PRINT_FLG = "";
            p.MNC_PH_NEW_SS = "";
            p.tmt_code = "";
            p.MNC_PH_THAI = "";
            p.frequency = "";//Frequency
            p.precautions = "";//Precautions
            p.indication = "";//Indication คำเตือน
            p.interaction = "";
            p.using1 = "";//วิธีใช้
            p.tmt_code_opbkk = "";
            p.CodeBSG = "";
            return p;
        }
    }
}
