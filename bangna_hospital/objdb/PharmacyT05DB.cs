using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
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
        public PharmacyT05 setPharmacyT05(DataTable dt)
        {
            PharmacyT05 pharT05 = new PharmacyT05();
            if (dt.Rows.Count > 0)
            {
                pharT05.MNC_DOC_CD = dt.Rows[0]["MNC_DOC_CD"].ToString();
                pharT05.MNC_CFR_YR = dt.Rows[0]["MNC_CFR_YR"].ToString();
                pharT05.MNC_CFR_NO = dt.Rows[0]["MNC_CFR_NO"].ToString();
                pharT05.MNC_CFG_DAT = dt.Rows[0]["MNC_CFG_DAT"].ToString();
                pharT05.MNC_CFR_STS = dt.Rows[0]["MNC_CFR_STS"].ToString();
                pharT05.MNC_DEPC_NO = dt.Rows[0]["MNC_DEPC_NO"].ToString();
                pharT05.MNC_SECC_NO = dt.Rows[0]["MNC_SECC_NO"].ToString();
                pharT05.MNC_REQ_YR = dt.Rows[0]["MNC_REQ_YR"].ToString();
                pharT05.MNC_REQ_NO = dt.Rows[0]["MNC_REQ_NO"].ToString();
                pharT05.MNC_REQ_DAT = dt.Rows[0]["MNC_REQ_DAT"].ToString();
                pharT05.MNC_REQ_TIM = dt.Rows[0]["MNC_REQ_TIM"].ToString();
                pharT05.MNC_DEPR_NO = dt.Rows[0]["MNC_DEPR_NO"].ToString();
                pharT05.MNC_SECR_NO = dt.Rows[0]["MNC_SECR_NO"].ToString();
                pharT05.MNC_SUM_PRI = dt.Rows[0]["MNC_SUM_PRI"].ToString();
                pharT05.MNC_SUM_COS = dt.Rows[0]["MNC_SUM_COS"].ToString();
                pharT05.MNC_HN_YR = dt.Rows[0]["MNC_HN_YR"].ToString();
                pharT05.MNC_HN_NO = dt.Rows[0]["MNC_HN_NO"].ToString();
                pharT05.MNC_AN_YR = dt.Rows[0]["MNC_AN_YR"].ToString();
                pharT05.MNC_AN_NO = dt.Rows[0]["MNC_AN_NO"].ToString();
                pharT05.MNC_PRE_NO = dt.Rows[0]["MNC_PRE_NO"].ToString();
                pharT05.MNC_DATE = dt.Rows[0]["MNC_DATE"].ToString();
                pharT05.MNC_TIME = dt.Rows[0]["MNC_TIME"].ToString();
                pharT05.MNC_USE_LOG = dt.Rows[0]["MNC_USE_LOG"].ToString();
                pharT05.MNC_FN_TYP_CD = dt.Rows[0]["MNC_FN_TYP_CD"].ToString();
                pharT05.MNC_COM_CD = dt.Rows[0]["MNC_COM_CD"].ToString();
                pharT05.MNC_FLAGAR = dt.Rows[0]["MNC_FLAGAR"].ToString();
                pharT05.MNC_REQ_REF = dt.Rows[0]["MNC_REQ_REF"].ToString();
                pharT05.MNC_CFR_TIME = dt.Rows[0]["MNC_CFR_TIME"].ToString();
                pharT05.MNC_EMPR_CD = dt.Rows[0]["MNC_EMPR_CD"].ToString();
                pharT05.MNC_EMPC_CD = dt.Rows[0]["MNC_EMPC_CD"].ToString();
                pharT05.MNC_ORD_DOT = dt.Rows[0]["MNC_ORD_DOT"].ToString();
                pharT05.MNC_QUE_NO = dt.Rows[0]["MNC_QUE_NO"].ToString();
                pharT05.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                pharT05.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                pharT05.MNC_PAC_CD = dt.Rows[0]["MNC_PAC_CD"].ToString();
                pharT05.MNC_PAC_TYP = dt.Rows[0]["MNC_PAC_TYP"].ToString();
                pharT05.MNC_PHA_STS = dt.Rows[0]["MNC_PHA_STS"].ToString();
                pharT05.MNC_REQ_COUNT = dt.Rows[0]["MNC_REQ_COUNT"].ToString();
                pharT05.MNC_REQ_TYP = dt.Rows[0]["MNC_REQ_TYP"].ToString();

            }
            else
            {
                setPharmacyT05(pharT05);
            }
            return pharT05;
        }
        public PharmacyT05 setPharmacyT05(PharmacyT05 p)
        {
            
            p.MNC_DOC_CD = "";
            p.MNC_CFR_YR = "";
            p.MNC_CFR_NO = "";
            p.MNC_CFG_DAT = "";
            p.MNC_CFR_STS = "";
            p.MNC_DEPC_NO = "";
            p.MNC_SECC_NO = "";
            p.MNC_REQ_YR = "";
            p.MNC_REQ_NO = "";
            p.MNC_REQ_DAT = "";
            p.MNC_REQ_TIM = "";
            p.MNC_DEPR_NO = "";
            p.MNC_SECR_NO = "";
            p.MNC_SUM_PRI = "";
            p.MNC_SUM_COS = "";
            p.MNC_HN_YR = "";
            p.MNC_HN_NO = "";
            p.MNC_AN_YR = "";
            p.MNC_AN_NO = "";
            p.MNC_PRE_NO = "";
            p.MNC_DATE = "";
            p.MNC_TIME = "";
            p.MNC_USE_LOG = "";
            p.MNC_FN_TYP_CD = "";
            p.MNC_COM_CD = "";
            p.MNC_FLAGAR = "";
            p.MNC_REQ_REF = "";
            p.MNC_CFR_TIME = "";
            p.MNC_EMPR_CD = "";
            p.MNC_EMPC_CD = "";
            p.MNC_ORD_DOT = "";
            p.MNC_QUE_NO = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_PAC_CD = "";
            p.MNC_PAC_TYP = "";
            p.MNC_PHA_STS = "";
            p.MNC_REQ_COUNT = "";
            p.MNC_REQ_TYP = "";
            return p;
        }


    }
}
