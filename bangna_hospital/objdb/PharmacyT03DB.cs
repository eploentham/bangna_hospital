using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PharmacyT03DB
    {
        public PharmacyT03 pharT03;
        ConnectDB conn;
        public PharmacyT03DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        { 
            pharT03 = new PharmacyT03();
            pharT03.MNC_DOC_CD  = "MNC_DOC_CD";
            pharT03.MNC_REQ_YR  = "MNC_REQ_YR";
            pharT03.MNC_REQ_NO  = "MNC_REQ_NO";
            pharT03.MNC_REQ_DAT  = "MNC_REQ_DAT";
            pharT03.MNC_REQ_STS  = "MNC_REQ_STS";
            pharT03.MNC_SUM_COS  = "MNC_SUM_COS";
            pharT03.MNC_DEP_NO  = "MNC_DEP_NO";
            pharT03.MNC_SEC_NO  = "MNC_SEC_NO";
            pharT03.MNC_USR_LOG  = "MNC_USR_LOG";
            pharT03.MNC_DEPR_NO  = "MNC_DEPR_NO";
            pharT03.MNC_SECR_NO  = "MNC_SECR_NO";
            pharT03.MNC_STAMP_DAT  = "MNC_STAMP_DAT";
            pharT03.MNC_STAMP_TIM  = "MNC_STAMP_TIM";
            pharT03.MNC_USR_ADD  = "MNC_USR_ADD";
            pharT03.MNC_USR_UPD  = "MNC_USR_UPD";
        }

        public PharmacyT03 setPharmacyT03(DataTable dt)
        {
            PharmacyT03 pharT03 = new PharmacyT03();
            if (dt.Rows.Count > 0)
            {
                pharT03.MNC_DOC_CD = dt.Rows[0]["MNC_DOC_CD"].ToString();
                pharT03.MNC_REQ_YR = dt.Rows[0]["MNC_REQ_YR"].ToString();
                pharT03.MNC_REQ_NO = dt.Rows[0]["MNC_REQ_NO"].ToString();
                pharT03.MNC_REQ_DAT = dt.Rows[0]["MNC_REQ_DAT"].ToString();
                pharT03.MNC_REQ_STS = dt.Rows[0]["MNC_REQ_STS"].ToString();
                pharT03.MNC_SUM_COS = dt.Rows[0]["MNC_SUM_COS"].ToString();
                pharT03.MNC_DEP_NO = dt.Rows[0]["MNC_DEP_NO"].ToString();
                pharT03.MNC_SEC_NO = dt.Rows[0]["MNC_SEC_NO"].ToString();
                pharT03.MNC_USR_LOG = dt.Rows[0]["MNC_USR_LOG"].ToString();
                pharT03.MNC_DEPR_NO = dt.Rows[0]["MNC_DEPR_NO"].ToString();
                pharT03.MNC_SECR_NO = dt.Rows[0]["MNC_SECR_NO"].ToString();
                pharT03.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                pharT03.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                pharT03.MNC_USR_ADD = dt.Rows[0]["MNC_USR_ADD"].ToString();
                pharT03.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
            }
            else
            {
                setPharmacyT03(pharT03);
            }
            return pharT03;
        }
        public PharmacyT03 setPharmacyT03(PharmacyT03 p)
        {
            p.MNC_DOC_CD = "";
            p.MNC_REQ_YR = "";
            p.MNC_REQ_NO = "";
            p.MNC_REQ_DAT = "";
            p.MNC_REQ_STS = "";
            p.MNC_SUM_COS = "";
            p.MNC_DEP_NO = "";
            p.MNC_SEC_NO = "";
            p.MNC_USR_LOG = "";
            p.MNC_DEPR_NO = "";
            p.MNC_SECR_NO = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_ADD = "";
            p.MNC_USR_UPD = "";
            return p;
        }
    }
}
