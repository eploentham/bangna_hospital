using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PharmacyT04DB
    {
        public PharmacyT04 pharT04;
        ConnectDB conn;
        public PharmacyT04DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharT04 = new PharmacyT04();
            pharT04.MNC_DOC_CD = "MNC_DOC_CD";
            pharT04.MNC_REQ_YR = "MNC_REQ_YR";
            pharT04.MNC_REQ_NO = "MNC_REQ_NO";
            pharT04.MNC_PH_CD = "MNC_PH_CD";
            pharT04.MNC_PH_QTY = "MNC_PH_QTY";
            pharT04.MNC_PH_UNTF_QTY = "MNC_PH_UNTF_QTY";
            pharT04.MNC_PH_UNT_CD = "MNC_PH_UNT_CD";
            pharT04.MNC_PH_COS = "MNC_PH_COS";
            pharT04.MNC_SUP_STS = "MNC_SUP_STS";
            pharT04.MNC_DEPC_NO = "MNC_DEPC_NO";
            pharT04.MNC_SECC_NO = "MNC_SECC_NO";
            pharT04.MNC_PAY_STS = "MNC_PAY_STS";
            pharT04.MNC_PH_QTY_PAY = "MNC_PH_QTY_PAY";
            pharT04.MNC_PAY_CD = "MNC_PAY_CD";
            pharT04.MNC_PH_DSC = "MNC_PH_DSC";
            pharT04.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pharT04.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pharT04.MNC_USR_ADD = "MNC_USR_ADD";
            pharT04.MNC_USR_UPD = "MNC_USR_UPD";
        }
        public PharmacyT04 setPharmacyT04(DataTable dt)
        {
            PharmacyT04 pharT04 = new PharmacyT04();
            if (dt.Rows.Count > 0)
            {
                pharT04.MNC_DOC_CD = dt.Rows[0]["MNC_DOC_CD"].ToString();
                pharT04.MNC_REQ_YR = dt.Rows[0]["MNC_REQ_YR"].ToString();
                pharT04.MNC_REQ_NO = dt.Rows[0]["MNC_REQ_NO"].ToString();
                pharT04.MNC_PH_CD = dt.Rows[0]["MNC_PH_CD"].ToString();
                pharT04.MNC_PH_QTY = dt.Rows[0]["MNC_PH_QTY"].ToString();
                pharT04.MNC_PH_UNTF_QTY = dt.Rows[0]["MNC_PH_UNTF_QTY"].ToString();
                pharT04.MNC_PH_UNT_CD = dt.Rows[0]["MNC_PH_UNT_CD"].ToString();
                pharT04.MNC_PH_COS = dt.Rows[0]["MNC_PH_COS"].ToString();
                pharT04.MNC_SUP_STS = dt.Rows[0]["MNC_SUP_STS"].ToString();
                pharT04.MNC_DEPC_NO = dt.Rows[0]["MNC_DEPC_NO"].ToString();
                pharT04.MNC_SECC_NO = dt.Rows[0]["MNC_SECC_NO"].ToString();
                pharT04.MNC_PAY_STS = dt.Rows[0]["MNC_PAY_STS"].ToString();
                pharT04.MNC_PH_QTY_PAY = dt.Rows[0]["MNC_PH_QTY_PAY"].ToString();
                pharT04.MNC_PAY_CD = dt.Rows[0]["MNC_PAY_CD"].ToString();
                pharT04.MNC_PH_DSC = dt.Rows[0]["MNC_PH_DSC"].ToString();
                pharT04.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                pharT04.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                pharT04.MNC_USR_ADD = dt.Rows[0]["MNC_USR_ADD"].ToString();
                pharT04.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();

            }
            else
            {
                setPharmacyT04(pharT04);
            }
            return pharT04;
        }
        public PharmacyT04 setPharmacyT04(PharmacyT04 p)
        {
            p.MNC_DOC_CD = "";
            p.MNC_REQ_YR = "";
            p.MNC_REQ_NO = "";
            p.MNC_PH_CD = "";
            p.MNC_PH_QTY = "";
            p.MNC_PH_UNTF_QTY = "";
            p.MNC_PH_UNT_CD = "";
            p.MNC_PH_COS = "";
            p.MNC_SUP_STS = "";
            p.MNC_DEPC_NO = "";
            p.MNC_SECC_NO = "";
            p.MNC_PAY_STS = "";
            p.MNC_PH_QTY_PAY = "";
            p.MNC_PAY_CD = "";
            p.MNC_PH_DSC = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_ADD = "";
            p.MNC_USR_UPD = "";
            return p;
        }
    }

}
