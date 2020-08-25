using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PharmacyT06DB
    {
        public PharmacyT06 pharT06;
        ConnectDB conn;
        public PharmacyT06DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharT06 = new PharmacyT06();
            pharT06.MNC_DOC_CD  = "MNC_DOC_CD";
            pharT06.MNC_CFR_YR  = "MNC_CFR_YR";
            pharT06.MNC_CFR_NO  = "MNC_CFR_NO";
            pharT06.MNC_CFR_DAT  = "MNC_CFR_DAT";
            pharT06.MNC_PH_CD  = "MNC_PH_CD";
            pharT06.MNC_NO  = "MNC_NO";
            pharT06.MNC_LOT_YR  = "MNC_LOT_YR";
            pharT06.MNC_LOT_NO  = "MNC_LOT_NO";
            pharT06.MNC_PH_QTY  = "MNC_PH_QTY";
            pharT06.MNC_PH_UNTF_QTY  = "MNC_PH_UNTF_QTY";
            pharT06.MNC_PH_QTY_PAID  = "MNC_PH_QTY_PAID";
            pharT06.MNC_PH_UNT_CD  = "MNC_PH_UNT_CD";
            pharT06.MNC_PH_PRI  = "MNC_PH_PRI";
            pharT06.MNC_PH_COS  = "MNC_PH_COS";
            pharT06.MNC_PH_DIR_DSC  = "MNC_PH_DIR_DSC";
            pharT06.MNC_PAY_CD  = "MNC_PAY_CD";
            pharT06.MNC_ORD_NO  = "MNC_ORD_NO";
            pharT06.MNC_PH_RFN  = "MNC_PH_RFN";
            pharT06.MNC_PH_DIR_CD  = "MNC_PH_DIR_CD";
            pharT06.MNC_PH_FRE_CD  = "MNC_PH_FRE_CD";
            pharT06.MNC_PH_TIM_CD  = "MNC_PH_TIM_CD";
            pharT06.MNC_RUN_DAT  = "MNC_RUN_DAT";
            pharT06.MNC_RUN_NO  = "MNC_RUN_NO";
            pharT06.MNC_REQ_TYP  = "MNC_REQ_TYP";
            pharT06.MNC_PH_REM  = "MNC_PH_REM";
            pharT06.MNC_STAMP_DAT  = "MNC_STAMP_DAT";
            pharT06.MNC_STAMP_TIM  = "MNC_STAMP_TIM";
            pharT06.MNC_USR_ADD  = "MNC_USR_ADD";
            pharT06.MNC_USR_UPD  = "MNC_USR_UPD";
            pharT06.MNC_PH_DIR_TXT  = "MNC_PH_DIR_TXT";
            pharT06.MNC_CANCEL_STS  = "MNC_CANCEL_STS";
            pharT06.MNC_PH_QTY_PAY  = "MNC_PH_QTY_PAY";
            pharT06.MNC_PH_QTY_RFN  = "MNC_PH_QTY_RFN";
            pharT06.MNC_PAY_FLAG  = "MNC_PAY_FLAG";
        }
        public PharmacyT06 setPharmacyT06(DataTable dt)
        {
            PharmacyT06 pharT06 = new PharmacyT06();
            if (dt.Rows.Count > 0)
            {
                pharT06.MNC_DOC_CD = dt.Rows[0]["MNC_DOC_CD"].ToString();
                pharT06.MNC_CFR_YR = dt.Rows[0]["MNC_CFR_YR"].ToString();
                pharT06.MNC_CFR_NO = dt.Rows[0]["MNC_CFR_NO"].ToString();
                pharT06.MNC_CFR_DAT = dt.Rows[0]["MNC_CFR_DAT"].ToString();
                pharT06.MNC_PH_CD = dt.Rows[0]["MNC_PH_CD"].ToString();
                pharT06.MNC_NO = dt.Rows[0]["MNC_NO"].ToString();
                pharT06.MNC_LOT_YR = dt.Rows[0]["MNC_LOT_YR"].ToString();
                pharT06.MNC_LOT_NO = dt.Rows[0]["MNC_LOT_NO"].ToString();
                pharT06.MNC_PH_QTY = dt.Rows[0]["MNC_PH_QTY"].ToString();
                pharT06.MNC_PH_UNTF_QTY = dt.Rows[0]["MNC_PH_UNTF_QTY"].ToString();
                pharT06.MNC_PH_QTY_PAID = dt.Rows[0]["MNC_PH_QTY_PAID"].ToString();
                pharT06.MNC_PH_UNT_CD = dt.Rows[0]["MNC_PH_UNT_CD"].ToString();
                pharT06.MNC_PH_PRI = dt.Rows[0]["MNC_PH_PRI"].ToString();
                pharT06.MNC_PH_COS = dt.Rows[0]["MNC_PH_COS"].ToString();
                pharT06.MNC_PH_DIR_DSC = dt.Rows[0]["MNC_PH_DIR_DSC"].ToString();
                pharT06.MNC_PAY_CD = dt.Rows[0]["MNC_PAY_CD"].ToString();
                pharT06.MNC_ORD_NO = dt.Rows[0]["MNC_ORD_NO"].ToString();
                pharT06.MNC_PH_RFN = dt.Rows[0]["MNC_PH_RFN"].ToString();
                pharT06.MNC_PH_DIR_CD = dt.Rows[0]["MNC_PH_DIR_CD"].ToString();
                pharT06.MNC_PH_FRE_CD = dt.Rows[0]["MNC_PH_FRE_CD"].ToString();
                pharT06.MNC_PH_TIM_CD = dt.Rows[0]["MNC_PH_TIM_CD"].ToString();
                pharT06.MNC_RUN_DAT = dt.Rows[0]["MNC_RUN_DAT"].ToString();
                pharT06.MNC_RUN_NO = dt.Rows[0]["MNC_RUN_NO"].ToString();
                pharT06.MNC_REQ_TYP = dt.Rows[0]["MNC_REQ_TYP"].ToString();
                pharT06.MNC_PH_REM = dt.Rows[0]["MNC_PH_REM"].ToString();
                pharT06.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                pharT06.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                pharT06.MNC_USR_ADD = dt.Rows[0]["MNC_USR_ADD"].ToString();
                pharT06.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                pharT06.MNC_PH_DIR_TXT = dt.Rows[0]["MNC_PH_DIR_TXT"].ToString();
                pharT06.MNC_CANCEL_STS = dt.Rows[0]["MNC_CANCEL_STS"].ToString();
                pharT06.MNC_PH_QTY_PAY = dt.Rows[0]["MNC_PH_QTY_PAY"].ToString();
                pharT06.MNC_PH_QTY_RFN = dt.Rows[0]["MNC_PH_QTY_RFN"].ToString();
                pharT06.MNC_PAY_FLAG = dt.Rows[0]["MNC_PAY_FLAG"].ToString();
            }
            else
            {
                setPharmacyT06(pharT06);
            }
            return pharT06;
        }
        public PharmacyT06 setPharmacyT06(PharmacyT06 p)
        {
            p.MNC_DOC_CD  = "";
            p.MNC_CFR_YR  = "";
            p.MNC_CFR_NO  = "";
            p.MNC_CFR_DAT  = "";
            p.MNC_PH_CD  = "";
            p.MNC_NO  = "";
            p.MNC_LOT_YR  = "";
            p.MNC_LOT_NO  = "";
            p.MNC_PH_QTY  = "";
            p.MNC_PH_UNTF_QTY  = "";
            p.MNC_PH_QTY_PAID  = "";
            p.MNC_PH_UNT_CD  = "";
            p.MNC_PH_PRI  = "";
            p.MNC_PH_COS  = "";
            p.MNC_PH_DIR_DSC  = "";
            p.MNC_PAY_CD  = "";
            p.MNC_ORD_NO  = "";
            p.MNC_PH_RFN  = "";
            p.MNC_PH_DIR_CD  = "";
            p.MNC_PH_FRE_CD  = "";
            p.MNC_PH_TIM_CD  = "";
            p.MNC_RUN_DAT  = "";
            p.MNC_RUN_NO  = "";
            p.MNC_REQ_TYP  = "";
            p.MNC_PH_REM  = "";
            p.MNC_STAMP_DAT  = "";
            p.MNC_STAMP_TIM  = "";
            p.MNC_USR_ADD  = "";
            p.MNC_USR_UPD  = "";
            p.MNC_PH_DIR_TXT  = "";
            p.MNC_CANCEL_STS  = "";
            p.MNC_PH_QTY_PAY  = "";
            p.MNC_PH_QTY_RFN  = "";
            p.MNC_PAY_FLAG  = "";
            return p;
        }
    }
}
