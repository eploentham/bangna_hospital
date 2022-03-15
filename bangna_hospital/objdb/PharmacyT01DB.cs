using bangna_hospital.objdb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class PharmacyT01DB
    {
        public PharmacyT01 pharT01;
        ConnectDB conn;
        public PharmacyT01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharT01 = new PharmacyT01();
            pharT01.MNC_DOC_CD = "MNC_DOC_CD";
            pharT01.MNC_REQ_YR = "MNC_REQ_YR";
            pharT01.MNC_REQ_NO = "MNC_REQ_NO";
            pharT01.MNC_REQ_DAT = "MNC_REQ_DAT";
            pharT01.MNC_REQ_STS = "MNC_REQ_STS";
            pharT01.MNC_REQ_TIM = "MNC_REQ_TIM";
            pharT01.MNC_SUM_PRI = "MNC_SUM_PRI";
            pharT01.MNC_SUM_COS = "MNC_SUM_COS";
            pharT01.MNC_DEP_NO = "MNC_DEP_NO";
            pharT01.MNC_HN_YR = "MNC_HN_YR";
            pharT01.MNC_HN_NO = "MNC_HN_NO";
            pharT01.MNC_AN_YR = "MNC_AN_YR";
            pharT01.MNC_AN_NO = "MNC_AN_NO";
            pharT01.MNC_DATE = "MNC_DATE";
            pharT01.MNC_PRE_NO = "MNC_PRE_NO";
            pharT01.MNC_PRE_SEQ = "MNC_PRE_SEQ";
            pharT01.MNC_TIME = "MNC_TIME";
            pharT01.MNC_WD_NO = "MNC_WD_NO";
            pharT01.MNC_RM_NAM = "MNC_RM_NAM";
            pharT01.MNC_BD_NO = "MNC_BD_NO";
            pharT01.MNC_DOT_CD = "MNC_DOT_CD";
            pharT01.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            pharT01.MNC_COM_CD = "MNC_COM_CD";
            pharT01.MNC_USE_LOG = "MNC_USE_LOG";
            pharT01.MNC_PHA_STS = "MNC_PHA_STS";
            pharT01.MNC_CAL_NO = "MNC_CAL_NO";
            pharT01.MNC_EMPR_CD = "MNC_EMPR_CD";
            pharT01.MNC_EMPC_CD = "MNC_EMPC_CD";
            pharT01.MNC_ORD_DOT = "MNC_ORD_DOT";
            pharT01.MNC_CFM_DOT = "MNC_CFM_DOT";
            pharT01.MNC_SEC_NO = "MNC_SEC_NO";
            pharT01.MNC_DEPC_NO = "MNC_DEPC_NO";
            pharT01.MNC_SECC_NO = "MNC_SECC_NO";
            pharT01.MNC_CANCEL_STS = "MNC_CANCEL_STS";
            pharT01.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pharT01.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pharT01.MNC_USR_ADD = "MNC_USR_ADD";
            pharT01.MNC_USR_UPD = "MNC_USR_UPD";
            pharT01.MNC_PH_REM = "MNC_PH_REM";
            pharT01.MNC_PAC_CD = "MNC_PAC_CD";
            pharT01.MNC_PAC_TYP = "MNC_PAC_TYP";
            pharT01.MNC_REQ_COUNT = "MNC_REQ_COUNT";
            pharT01.MNC_REQ_TYP = "MNC_REQ_TYP";
        }
        private void chkNull(PharmacyT01 p)
        {
            long chk = 0;
            int chk1 = 0;
            decimal chk2 = 0;

            p.MNC_DOC_CD = p.MNC_DOC_CD == null ? "" : p.MNC_DOC_CD;
            p.MNC_REQ_STS = p.MNC_REQ_STS == null ? "" : p.MNC_REQ_STS;
            p.MNC_RM_NAM = p.MNC_RM_NAM == null ? "" : p.MNC_RM_NAM;
            p.MNC_DOT_CD = p.MNC_DOT_CD == null ? "" : p.MNC_DOT_CD;
            p.MNC_FN_TYP_CD = p.MNC_FN_TYP_CD == null ? "" : p.MNC_FN_TYP_CD;
            p.MNC_COM_CD = p.MNC_COM_CD == null ? "" : p.MNC_COM_CD;
            p.MNC_USE_LOG = p.MNC_USE_LOG == null ? "" : p.MNC_USE_LOG;
            p.MNC_PHA_STS = p.MNC_PHA_STS == null ? "" : p.MNC_PHA_STS;
            p.MNC_EMPR_CD = p.MNC_EMPR_CD == null ? "" : p.MNC_EMPR_CD;
            p.MNC_EMPC_CD = p.MNC_EMPC_CD == null ? "" : p.MNC_EMPC_CD;
            p.MNC_ORD_DOT = p.MNC_ORD_DOT == null ? "" : p.MNC_ORD_DOT;
            p.MNC_CFM_DOT = p.MNC_CFM_DOT == null ? "" : p.MNC_CFM_DOT;
            p.MNC_CANCEL_STS = p.MNC_CANCEL_STS == null ? "" : p.MNC_CANCEL_STS;
            p.MNC_USR_ADD = p.MNC_USR_ADD == null ? "" : p.MNC_USR_ADD;
            p.MNC_USR_UPD = p.MNC_USR_UPD == null ? "" : p.MNC_USR_UPD;
            p.MNC_PH_REM = p.MNC_PH_REM == null ? "" : p.MNC_PH_REM;
            p.MNC_PAC_CD = p.MNC_PAC_CD == null ? "" : p.MNC_PAC_CD;
            p.MNC_PAC_TYP = p.MNC_PAC_TYP == null ? "" : p.MNC_PAC_TYP;
            p.MNC_REQ_TYP = p.MNC_REQ_TYP == null ? "" : p.MNC_REQ_TYP;

            p.MNC_REQ_YR = long.TryParse(p.MNC_REQ_YR, out chk) ? chk.ToString() : "0";
            p.MNC_REQ_NO = long.TryParse(p.MNC_REQ_NO, out chk) ? chk.ToString() : "0";
            p.MNC_REQ_TIM = long.TryParse(p.MNC_REQ_TIM, out chk) ? chk.ToString() : "0";
            p.MNC_DEP_NO = long.TryParse(p.MNC_DEP_NO, out chk) ? chk.ToString() : "0";
            p.MNC_HN_YR = long.TryParse(p.MNC_HN_YR, out chk) ? chk.ToString() : "0";
            p.MNC_HN_NO = long.TryParse(p.MNC_HN_NO, out chk) ? chk.ToString() : "0";
            p.MNC_AN_YR = long.TryParse(p.MNC_AN_YR, out chk) ? chk.ToString() : "0";
            p.MNC_AN_NO = long.TryParse(p.MNC_AN_NO, out chk) ? chk.ToString() : "0";
            p.MNC_PRE_NO = long.TryParse(p.MNC_PRE_NO, out chk) ? chk.ToString() : "0";
            p.MNC_TIME = long.TryParse(p.MNC_TIME, out chk) ? chk.ToString() : "0";
            p.MNC_WD_NO = long.TryParse(p.MNC_WD_NO, out chk) ? chk.ToString() : "0";
            p.MNC_BD_NO = long.TryParse(p.MNC_BD_NO, out chk) ? chk.ToString() : "0";
            p.MNC_CAL_NO = long.TryParse(p.MNC_CAL_NO, out chk) ? chk.ToString() : "0";
            p.MNC_SEC_NO = long.TryParse(p.MNC_SEC_NO, out chk) ? chk.ToString() : "0";
            p.MNC_DEPC_NO = long.TryParse(p.MNC_DEPC_NO, out chk) ? chk.ToString() : "0";
            p.MNC_SECC_NO = long.TryParse(p.MNC_SECC_NO, out chk) ? chk.ToString() : "0";
            p.MNC_STAMP_TIM = long.TryParse(p.MNC_STAMP_TIM, out chk) ? chk.ToString() : "0";
            p.MNC_REQ_COUNT = long.TryParse(p.MNC_REQ_COUNT, out chk) ? chk.ToString() : "0";

            p.MNC_SUM_PRI = decimal.TryParse(p.MNC_SUM_PRI, out chk2) ? chk2.ToString() : "0";
            p.MNC_SUM_COS = decimal.TryParse(p.MNC_SUM_COS, out chk2) ? chk2.ToString() : "0";
            
        }
        public DataTable selectTop200()
        {
            PharmacyT01 pharT01 = new PharmacyT01();
            DataTable dt = new DataTable();
            String reqno = "", sql = "";
            sql = "Select top 200 sum(phart02.MNC_PH_QTY) as qty, phart02.MNC_PH_CD, pharm01.tmt_code " +
                "from PHARMACY_T01 phart01 " +
                "inner join pharmacy_t02 phart02 on phart01.MNC_REQ_YR = phart02.MNC_REQ_YR and phart01.MNC_REQ_NO = phart02.MNC_REQ_NO and phart01.MNC_DOC_CD = phart02.MNC_DOC_CD " +
                "inner join PHARMACY_M01 pharm01 on phart02.MNC_PH_CD = pharm01.MNC_PH_CD " +
                "where pharm01.tmt_code is not null and phart01.MNC_REQ_DAT >= '2020-01-01' and phart01.MNC_REQ_DAT <= '2020-12-31' " +
                "group by phart02.MNC_PH_CD, pharm01.tmt_code ";
            dt = conn.selectData(sql);
            
            return dt;
        }
        public PharmacyT01 selectCheckReqNoFromPreNO(String hn, String hnyr, String preno)
        {
            PharmacyT01 pharT01 = new PharmacyT01();
            DataTable dt = new DataTable();
            String reqno = "", sql="";
            sql = "Select * " +
                "From pharmacy_t01 " +
                "Where mnc_hn_no = '"+hn+"' and mnc_hn_yr = '"+hnyr +"' and mnc_pre_no = '"+preno+"'";
            dt = conn.selectData(sql);
            pharT01 = setPharmacyT01(dt);
            return pharT01;
        }
        public String insertPharmacyT01(PharmacyT01 p)
        {
            String sql = "", chk = "", re = "";
            
            chkNull(p);
            if (p.MNC_REQ_NO.Equals("0"))
            {
                //re = insert(p, "");
            }
            else
            {
                    
            }
            return re;
        }
        public String insertPharmacyOPDDrugB(String hnyear, String hn, String vsdate, String preno, String dtrcode, String userid)
        {
            String sql = "", chk = "", re = "";
            try
            {
                //new LogWriter("e", "PharmacyT01 insert " );
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "insert_pharmacy_t01_t02_ros_drug_b";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("mnc_hn_yr", hnyear);
                conn.comStore.Parameters.AddWithValue("mnc_hn_no", hn);
                conn.comStore.Parameters.AddWithValue("mnc_date", vsdate);
                conn.comStore.Parameters.AddWithValue("mnc_pre_no", preno);
                conn.comStore.Parameters.AddWithValue("mnc_dot_cd", dtrcode);
                conn.comStore.Parameters.AddWithValue("mnc_usr_add", userid);
                conn.comStore.Parameters.AddWithValue("mnc_usr_upd", userid);

                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;

                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;
            }
            catch (Exception ex)
            {
                new LogWriter("e", "PharmacyT01DB.insertPharmacyOPDDrugB " + ex.Message + " " + sql);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertPharmacyOPDDrugK(String hnyear, String hn, String vsdate, String preno, String dtrcode, String userid)
        {
            String sql = "", chk = "", re = "";
            try
            {
                //new LogWriter("e", "PharmacyT01 insert " );
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "insert_pharmacy_t01_t02_ros_drug_k";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("mnc_hn_yr", hnyear);
                conn.comStore.Parameters.AddWithValue("mnc_hn_no", hn);
                conn.comStore.Parameters.AddWithValue("mnc_date", vsdate);
                conn.comStore.Parameters.AddWithValue("mnc_pre_no", preno);
                conn.comStore.Parameters.AddWithValue("mnc_dot_cd", dtrcode);
                conn.comStore.Parameters.AddWithValue("mnc_usr_add", userid);
                conn.comStore.Parameters.AddWithValue("mnc_usr_upd", userid);

                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;

                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;
            }
            catch (Exception ex)
            {
                new LogWriter("e", "PharmacyT01DB.insertPharmacyOPDDrugB " + ex.Message + " " + sql);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertPharmacyOPDDrugD(String hnyear, String hn, String vsdate, String preno, String dtrcode, String userid)
        {
            String sql = "", chk = "", re = "";
            try
            {
                //new LogWriter("e", "PharmacyT01 insert " );
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "insert_pharmacy_t01_t02_ros_drug_d";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("mnc_hn_yr", hnyear);
                conn.comStore.Parameters.AddWithValue("mnc_hn_no", hn);
                conn.comStore.Parameters.AddWithValue("mnc_date", vsdate);
                conn.comStore.Parameters.AddWithValue("mnc_pre_no", preno);
                conn.comStore.Parameters.AddWithValue("mnc_dot_cd", dtrcode);
                conn.comStore.Parameters.AddWithValue("mnc_usr_add", userid);
                conn.comStore.Parameters.AddWithValue("mnc_usr_upd", userid);

                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;

                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;
            }
            catch (Exception ex)
            {
                new LogWriter("e", "PharmacyT01DB.insertPharmacyOPDDrugB " + ex.Message + " " + sql);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public PharmacyT01 setPharmacyT01(DataTable dt)
        {
            PharmacyT01 pharT01 = new PharmacyT01();
            if (dt.Rows.Count > 0)
            {
                pharT01.MNC_DOC_CD = dt.Rows[0]["MNC_DOC_CD"].ToString();
                pharT01.MNC_REQ_YR = dt.Rows[0]["MNC_REQ_YR"].ToString();
                pharT01.MNC_REQ_NO = dt.Rows[0]["MNC_REQ_NO"].ToString();

                pharT01.MNC_REQ_DAT = dt.Rows[0]["MNC_REQ_DAT"].ToString();
                pharT01.MNC_REQ_STS = dt.Rows[0]["MNC_REQ_STS"].ToString();
                pharT01.MNC_REQ_TIM = dt.Rows[0]["MNC_REQ_TIM"].ToString();
                pharT01.MNC_SUM_PRI = dt.Rows[0]["MNC_SUM_PRI"].ToString();
                pharT01.MNC_SUM_COS = dt.Rows[0]["MNC_SUM_COS"].ToString();
                pharT01.MNC_DEP_NO = dt.Rows[0]["MNC_DEP_NO"].ToString();
                pharT01.MNC_HN_YR = dt.Rows[0]["MNC_HN_YR"].ToString();
                pharT01.MNC_HN_NO = dt.Rows[0]["MNC_HN_NO"].ToString();
                pharT01.MNC_AN_YR = dt.Rows[0]["MNC_AN_YR"].ToString();
                pharT01.MNC_AN_NO = dt.Rows[0]["MNC_AN_NO"].ToString();
                pharT01.MNC_DATE = dt.Rows[0]["MNC_DATE"].ToString();
                pharT01.MNC_PRE_NO = dt.Rows[0]["MNC_PRE_NO"].ToString();
                pharT01.MNC_PRE_SEQ = dt.Rows[0]["MNC_PRE_SEQ"].ToString();
                pharT01.MNC_TIME = dt.Rows[0]["MNC_TIME"].ToString();
                pharT01.MNC_WD_NO = dt.Rows[0]["MNC_WD_NO"].ToString();
                pharT01.MNC_RM_NAM = dt.Rows[0]["MNC_RM_NAM"].ToString();
                pharT01.MNC_BD_NO = dt.Rows[0]["MNC_BD_NO"].ToString();
                pharT01.MNC_DOT_CD = dt.Rows[0]["MNC_DOT_CD"].ToString();
                pharT01.MNC_FN_TYP_CD = dt.Rows[0]["MNC_FN_TYP_CD"].ToString();
                pharT01.MNC_COM_CD = dt.Rows[0]["MNC_COM_CD"].ToString();
                pharT01.MNC_USE_LOG = dt.Rows[0]["MNC_USE_LOG"].ToString();
                pharT01.MNC_PHA_STS = dt.Rows[0]["MNC_PHA_STS"].ToString();
                pharT01.MNC_CAL_NO = dt.Rows[0]["MNC_CAL_NO"].ToString();
                pharT01.MNC_EMPR_CD = dt.Rows[0]["MNC_EMPR_CD"].ToString();
                pharT01.MNC_EMPC_CD = dt.Rows[0]["MNC_EMPC_CD"].ToString();
                pharT01.MNC_ORD_DOT = dt.Rows[0]["MNC_ORD_DOT"].ToString();
                pharT01.MNC_CFM_DOT = dt.Rows[0]["MNC_CFM_DOT"].ToString();
                pharT01.MNC_SEC_NO = dt.Rows[0]["MNC_SEC_NO"].ToString();
                pharT01.MNC_DEPC_NO = dt.Rows[0]["MNC_DEPC_NO"].ToString();
                pharT01.MNC_SECC_NO = dt.Rows[0]["MNC_SECC_NO"].ToString();
                pharT01.MNC_CANCEL_STS = dt.Rows[0]["MNC_CANCEL_STS"].ToString();
                pharT01.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                pharT01.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                pharT01.MNC_USR_ADD = dt.Rows[0]["MNC_USR_ADD"].ToString();
                pharT01.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                pharT01.MNC_PH_REM = dt.Rows[0]["MNC_PH_REM"].ToString();
                pharT01.MNC_PAC_CD = dt.Rows[0]["MNC_PAC_CD"].ToString();
                pharT01.MNC_PAC_TYP = dt.Rows[0]["MNC_PAC_TYP"].ToString();
                pharT01.MNC_REQ_COUNT = dt.Rows[0]["MNC_REQ_COUNT"].ToString();
                pharT01.MNC_REQ_TYP = dt.Rows[0]["MNC_REQ_TYP"].ToString();
            }
            else
            {
                setPharmacyT01(pharT01);
            }
            return pharT01;
        }
        public PharmacyT01 setPharmacyT01(PharmacyT01 p)
        {
            p.MNC_DOC_CD = "";
            p.MNC_REQ_YR = "";
            p.MNC_REQ_NO = "";
            p.MNC_REQ_DAT = "";
            p.MNC_REQ_STS = "";
            p.MNC_REQ_TIM = "";
            p.MNC_SUM_PRI = "";
            p.MNC_SUM_COS = "";
            p.MNC_DEP_NO = "";
            p.MNC_HN_YR = "";
            p.MNC_HN_NO = "";
            p.MNC_AN_YR = "";
            p.MNC_AN_NO = "";
            p.MNC_DATE = "";
            p.MNC_PRE_NO = "";
            p.MNC_PRE_SEQ = "";
            p.MNC_TIME = "";
            p.MNC_WD_NO = "";
            p.MNC_RM_NAM = "";
            p.MNC_BD_NO = "";
            p.MNC_DOT_CD = "";
            p.MNC_FN_TYP_CD = "";
            p.MNC_COM_CD = "";
            p.MNC_USE_LOG = "";
            p.MNC_PHA_STS = "";
            p.MNC_CAL_NO = "";
            p.MNC_EMPR_CD = "";
            p.MNC_EMPC_CD = "";
            p.MNC_ORD_DOT = "";
            p.MNC_CFM_DOT = "";
            p.MNC_SEC_NO = "";
            p.MNC_DEPC_NO = "";
            p.MNC_SECC_NO = "";
            p.MNC_CANCEL_STS = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_ADD = "";
            p.MNC_USR_UPD = "";
            p.MNC_PH_REM = "";
            p.MNC_PAC_CD = "";
            p.MNC_PAC_TYP = "";
            p.MNC_REQ_COUNT = "";
            p.MNC_REQ_TYP = "";
            return p;
        }
    }
}
