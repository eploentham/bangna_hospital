using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class XrayT01DB
    {
        public XrayT01 XrayT01;
        ConnectDB conn;
        public XrayT01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

            XrayT01 = new XrayT01();
            XrayT01.MNC_REQ_YR = "MNC_REQ_YR";
            XrayT01.MNC_REQ_NO = "MNC_REQ_NO";
            XrayT01.MNC_REQ_DAT = "MNC_REQ_DAT";
            XrayT01.MNC_REQ_DEP = "MNC_REQ_DEP";
            XrayT01.MNC_REQ_STS = "MNC_REQ_STS";
            XrayT01.MNC_REQ_TIM = "MNC_REQ_TIM";
            XrayT01.MNC_HN_YR = "MNC_HN_YR";
            XrayT01.MNC_HN_NO = "MNC_HN_NO";
            XrayT01.MNC_AN_YR = "MNC_AN_YR";
            XrayT01.MNC_AN_NO = "MNC_AN_NO";
            XrayT01.MNC_PRE_NO = "MNC_PRE_NO";
            XrayT01.MNC_DATE = "MNC_DATE";
            XrayT01.MNC_TIME = "MNC_TIME";
            XrayT01.MNC_DOT_CD = "MNC_DOT_CD";
            XrayT01.MNC_WD_NO = "MNC_WD_NO";
            XrayT01.MNC_RM_NAM = "MNC_RM_NAM";
            XrayT01.MNC_BD_NO = "MNC_BD_NO";
            XrayT01.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            XrayT01.MNC_COM_CD = "MNC_COM_CD";
            XrayT01.MNC_REM = "MNC_REM";
            XrayT01.MNC_XR_STS = "MNC_XR_STS";
            XrayT01.MNC_CAL_NO = "MNC_CAL_NO";
            XrayT01.MNC_EMPR_CD = "MNC_EMPR_CD";
            XrayT01.MNC_EMPC_CD = "MNC_EMPC_CD";
            XrayT01.MNC_ORD_DOT = "MNC_ORD_DOT";
            XrayT01.MNC_CFM_DOT = "MNC_CFM_DOT";
            XrayT01.MNC_DOC_YR = "MNC_DOC_YR";
            XrayT01.MNC_DOC_NO = "MNC_DOC_NO";
            XrayT01.MNC_DOC_DAT = "MNC_DOC_DAT";
            XrayT01.MNC_DOC_CD = "MNC_DOC_CD";
            XrayT01.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            XrayT01.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            XrayT01.MNC_CANCEL_STS = "MNC_CANCEL_STS";
            XrayT01.MNC_PAC_CD = "MNC_PAC_CD";
            XrayT01.MNC_PAC_TYP = "MNC_PAC_TYP";
            XrayT01.status_pacs = "status_pacs";
        }
        private void chkNull(XrayT01 p)
        {
            long chk = 0;
            int chk1 = 0;
            decimal chk2 = 0;

        }
        public String insertPharmacyT01(XrayT01 p)
        {
            String sql = "", chk = "", re = "";

            chkNull(p);
            if (p.MNC_REQ_NO.Equals("0"))
            {
                re = insertOPD(p, "");
            }
            else
            {

            }
            return re;
        }
        public String insertOPD(XrayT01 p, String userid)
        {
            String sql = "", chk = "", re = "";
            try
            {
                //new LogWriter("e", "PharmacyT01 insert " );
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "insert_xray_t01_opd";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                //conn.comStore.Parameters.AddWithValue("mnc_req_yr", p.MNC_REQ_YR);
                //conn.comStore.Parameters.AddWithValue("mnc_req_dat", p.MNC_REQ_DAT);
                //conn.comStore.Parameters.AddWithValue("mnc_req_tim", p.MNC_REQ_TIM);
                //conn.comStore.Parameters.AddWithValue("mnc_sum_pri", p.MNC_SUM_PRI);
                //conn.comStore.Parameters.AddWithValue("mnc_sum_cos", p.MNC_SUM_COS);
                //conn.comStore.Parameters.AddWithValue("mnc_dep_no", p.MNC_DEP_NO);
                conn.comStore.Parameters.AddWithValue("mnc_hn_yr", p.MNC_HN_YR);
                conn.comStore.Parameters.AddWithValue("mnc_hn_no", p.MNC_HN_NO);
                conn.comStore.Parameters.AddWithValue("mnc_date", p.MNC_DATE);
                conn.comStore.Parameters.AddWithValue("mnc_pre_no", p.MNC_PRE_NO);

                //conn.comStore.Parameters.AddWithValue("mnc_pre_seq", p.MNC_PRE_SEQ);
                //conn.comStore.Parameters.AddWithValue("mnc_time", p.MNC_TIME);
                conn.comStore.Parameters.AddWithValue("mnc_dot_cd", p.MNC_DOT_CD);
                //conn.comStore.Parameters.AddWithValue("mnc_fn_typ_cd", p.MNC_FN_TYP_CD);
                //conn.comStore.Parameters.AddWithValue("mnc_com_cd", p.MNC_COM_CD);
                //conn.comStore.Parameters.AddWithValue("mnc_sec_no", p.MNC_SEC_NO);
                //conn.comStore.Parameters.AddWithValue("mnc_depc_no", p.MNC_DEPC_NO);
                //conn.comStore.Parameters.AddWithValue("mnc_secc_no", p.MNC_SECC_NO);
                //conn.comStore.Parameters.AddWithValue("mnc_cancel_sts", p.MNC_CANCEL_STS);
                //conn.comStore.Parameters.AddWithValue("mnc_usr_add", p.MNC_USR_ADD);

                //conn.comStore.Parameters.AddWithValue("mnc_usr_upd", p.MNC_USR_UPD);

                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;

                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;
            }
            catch (Exception ex)
            {
                new LogWriter("e", "PharmacyT01DB.XrayT01 " + ex.Message + " " + sql);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
    }
}
