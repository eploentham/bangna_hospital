using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PharmacyM02DB
    {
        public PharmacyM02 pharM02;
        ConnectDB conn;
        public PharmacyM02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharM02 = new PharmacyM02();
            pharM02.MNC_PH_CD = "MNC_PH_CD";
            pharM02.MNC_DEP_NO = "MNC_DEP_NO";
            pharM02.MNC_SEC_NO = "MNC_SEC_NO";
            pharM02.MNC_PH_LOC = "MNC_PH_LOC";
            pharM02.MNC_PH_MIN = "MNC_PH_MIN";
            pharM02.MNC_PH_MAX = "MNC_PH_MAX";
            pharM02.MNC_PH_REC_QTY = "MNC_PH_REC_QTY";
            pharM02.MNC_PH_ISS_QTY = "MNC_PH_ISS_QTY";
            pharM02.MNC_PH_ADJ_QTY = "MNC_PH_ADJ_QTY";
            pharM02.MNC_PH_QTY = "MNC_PH_QTY";
            pharM02.MNC_PH_UNT_CD = "MNC_PH_UNT_CD";
            pharM02.MNC_FREEZ_QTY = "MNC_FREEZ_QTY";
            pharM02.MNC_FREEZ_DAT = "MNC_FREEZ_DAT";
            pharM02.MNC_FREEZ_ISS = "MNC_FREEZ_ISS";
            pharM02.MNC_LOT_NO = "MNC_LOT_NO";
            pharM02.MNC_EXP_DAT = "MNC_EXP_DA";
            pharM02.MNC_PH_STS = "MNC_PH_STS";
            pharM02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pharM02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pharM02.MNC_USR_ADD = "MNC_USR_ADD";
            pharM02.MNC_USR_UPD = "MNC_USR_UPD";
            pharM02.MNC_MAC_ID = "MNC_MAC_ID";


         }

        public PharmacyM02 setPharmacyM02(DataTable dt)
        {
            PharmacyM02 pharM02 = new PharmacyM02();
            if (dt.Rows.Count > 0)
            {
                pharM02.MNC_PH_CD = dt.Rows[0]["MNC_PH_CD"].ToString();
                pharM02.MNC_DEP_NO = dt.Rows[0]["MNC_DEP_NO"].ToString();
                pharM02.MNC_SEC_NO = dt.Rows[0]["MNC_SEC_NO"].ToString();
                pharM02.MNC_PH_LOC = dt.Rows[0]["MNC_PH_LOC"].ToString();
                pharM02.MNC_PH_MIN = dt.Rows[0]["MNC_PH_MIN"].ToString();
                pharM02.MNC_PH_MAX = dt.Rows[0]["MNC_PH_MAX"].ToString();
                pharM02.MNC_PH_REC_QTY = dt.Rows[0]["MNC_PH_REC_QTY"].ToString();
                pharM02.MNC_PH_ISS_QTY = dt.Rows[0]["MNC_PH_ISS_QTY"].ToString();
                pharM02.MNC_PH_ADJ_QTY = dt.Rows[0]["MNC_PH_ADJ_QTY"].ToString();
                pharM02.MNC_PH_QTY = dt.Rows[0]["MNC_PH_QTY"].ToString();
                pharM02.MNC_PH_UNT_CD = dt.Rows[0]["MNC_PH_UNT_CD"].ToString();
                pharM02.MNC_FREEZ_QTY = dt.Rows[0]["MNC_FREEZ_QTY"].ToString();
                pharM02.MNC_FREEZ_DAT = dt.Rows[0]["MNC_FREEZ_DAT"].ToString();
                pharM02.MNC_FREEZ_ISS = dt.Rows[0]["MNC_FREEZ_ISS"].ToString();
                pharM02.MNC_LOT_NO = dt.Rows[0]["MNC_LOT_NO"].ToString();
                pharM02.MNC_EXP_DAT = dt.Rows[0]["MNC_EXP_DAT"].ToString();
                pharM02.MNC_PH_STS = dt.Rows[0]["MNC_PH_STS"].ToString();
                pharM02.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                pharM02.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                pharM02.MNC_USR_ADD = dt.Rows[0]["MNC_USR_ADD"].ToString();
                pharM02.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                pharM02.MNC_MAC_ID = dt.Rows[0]["MNC_MAC_ID"].ToString();


            }
            else
            {
                setPharmacyM02(pharM02);
            }
            return pharM02;
        }
        public PharmacyM02 setPharmacyM02(PharmacyM02 p)
        {

            p.MNC_PH_CD = "";
            p.MNC_DEP_NO = "";
            p.MNC_SEC_NO = "";
            p.MNC_PH_LOC = "";
            p.MNC_PH_MIN = "";
            p.MNC_PH_MAX = "";
            p.MNC_PH_REC_QTY = "";
            p.MNC_PH_ISS_QTY = "";
            p.MNC_PH_ADJ_QTY = "";
            p.MNC_PH_QTY = "";
            p.MNC_PH_UNT_CD = "";
            p.MNC_FREEZ_QTY = "";
            p.MNC_FREEZ_DAT = "";
            p.MNC_FREEZ_ISS = "";
            p.MNC_LOT_NO = "";
            p.MNC_EXP_DAT = "";
            p.MNC_PH_STS = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_ADD = "";
            p.MNC_USR_UPD = "";
            p.MNC_MAC_ID = "";

            return p;
        }


    }
}
