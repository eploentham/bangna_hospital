using bangna_hospital.objdb;
using System;
using System.Collections.Generic;
using System.Data;
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
