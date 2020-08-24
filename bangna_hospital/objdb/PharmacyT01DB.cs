using bangna_hospital.objdb;
using System;
using System.Collections.Generic;
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

    }
}
