using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PharmacyT05DB
    {
        public PharmacyT05 pharT05;
        ConnectDB conn;
        public PharmacyT05DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharT05 = new PharmacyT05();
            pharT05.MNC_DOC_CD  = "MNC_DOC_CD";
            pharT05.MNC_CFR_YR  = "MNC_CFR_YR";
            pharT05.MNC_CFR_NO  = "MNC_CFR_NO";
            pharT05.MNC_CFG_DAT  = "MNC_CFG_DAT";
            pharT05.MNC_CFR_STS  = "MNC_CFR_STS";
            pharT05.MNC_DEPC_NO  = "MNC_DEPC_NO";
            pharT05.MNC_SECC_NO  = "MNC_SECC_NO";
            pharT05.MNC_REQ_YR  = "MNC_REQ_YR";
            pharT05.MNC_REQ_NO  = "MNC_REQ_NO";
            pharT05.MNC_REQ_DAT  = "MNC_REQ_DAT";
            pharT05.MNC_REQ_TIM  = "MNC_REQ_TIM";
            pharT05.MNC_DEPR_NO  = "MNC_DEPR_NO";
            pharT05.MNC_SECR_NO  = "MNC_SECR_NO";
            pharT05.MNC_SUM_PRI  = "MNC_SUM_PRI";
            pharT05.MNC_SUM_COS  = "MNC_SUM_COS";
            pharT05.MNC_HN_YR  = "MNC_HN_YR";
            pharT05.MNC_HN_NO  = "MNC_HN_NO";
            pharT05.MNC_AN_YR  = "MNC_AN_YR";
            pharT05.MNC_AN_NO  = "MNC_AN_NO";
            pharT05.MNC_PRE_NO  = "MNC_PRE_NO";
            pharT05.MNC_DATE  = "MNC_DATE";
            pharT05.MNC_TIME  = "MNC_TIME";
            pharT05.MNC_USE_LOG  = "MNC_USE_LOG";
            pharT05.MNC_FN_TYP_CD  = "MNC_FN_TYP_CD";
            pharT05.MNC_COM_CD  = "MNC_COM_CD";
            pharT05.MNC_FLAGAR  = "MNC_FLAGAR";
            pharT05.MNC_REQ_REF  = "MNC_REQ_REF";
            pharT05.MNC_CFR_TIME  = "MNC_CFR_TIME";
            pharT05.MNC_EMPR_CD  = "MNC_EMPR_CD";
            pharT05.MNC_EMPC_CD  = "MNC_EMPC_CD";
            pharT05.MNC_ORD_DOT  = "MNC_ORD_DOT";
            pharT05.MNC_QUE_NO  = "MNC_QUE_NO";
            pharT05.MNC_STAMP_DAT  = "MNC_STAMP_DAT";
            pharT05.MNC_STAMP_TIM  = "MNC_STAMP_TIM";
            pharT05.MNC_PAC_CD  = "MNC_PAC_CD";
            pharT05.MNC_PAC_TYP  = "MNC_PAC_TYP";
            pharT05.MNC_PHA_STS  = "MNC_PHA_STS";
            pharT05.MNC_REQ_COUNT  = "MNC_REQ_COUNT";
            pharT05.MNC_REQ_TYP  = "MNC_REQ_TYP";

        }
    }
}
