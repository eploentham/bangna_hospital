using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PharmacyM01DB
    {
        public PharmacyM01 pharM01;
        ConnectDB conn;
        public PharmacyM01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharM01 = new PharmacyM01();
            pharM01.MNC_PH_CD = "MNC_PH_CD";
            pharM01.MNC_PH_ID = "MNC_PH_ID";
            pharM01.MNC_PH_CTL_CD = "MNC_PH_CTL_CD";
            pharM01.MNC_PH_TN = "MNC_PH_TN";
            pharM01.MNC_PH_GN = "MNC_PH_GN";
            pharM01.MNC_PH_UNT_CD = "MNC_PH_UNT_CD";
            pharM01.MNC_PH_GRP_CD = "MNC_PH_GRP_CD";
            pharM01.MNC_PH_TYP_CD = "MNC_PH_TYP_CD";
            pharM01.MNC_PH_DIR_CD = "MNC_PH_DIR_CD";
            pharM01.MNC_PH_CAU_CD = "MNC_PH_CAU_CD";
            pharM01.MNC_PH_MIN = "MNC_PH_MIN";
            pharM01.MNC_PH_MAX = "MNC_PH_MAX";
            pharM01.MNC_PH_DAY = "MNC_PH_DAY";
            pharM01.MNC_PR_STD = "MNC_PR_STD";
            pharM01.MNC_SUP_STS = "MNC_SUP_STS";
            pharM01.MNC_PRO_LOC = "MNC_PRO_LOC";
            pharM01.MNC_PH_AT = "MNC_PH_AT";
            pharM01.MNC_FN_CD = "MNC_FN_CD";
            pharM01.MNC_PH_STS = "MNC_PH_STS";
            pharM01.MNC_PH_LQ = "MNC_PH_LQ";
            pharM01.MNC_PH_LV = "MNC_PH_LV";
            pharM01.MNC_PH_LI = "MNC_PH_LI";
            pharM01.MNC_PH_LC = "MNC_PH_LC";
            pharM01.MNC_PH_LD = "MNC_PH_LD";
            pharM01.MNC_PH_DIS_STS = "MNC_PH_DIS_STS";
            pharM01.MNC_PH_DIV_CD = "MNC_PH_DIV_CD";
            pharM01.MNC_PH_FRE_CD = "MNC_PH_FRE_CD";
            pharM01.MNC_PH_TIM_CD = "MNC_PH_TIM_CD";
            pharM01.MNC_PH_IND_CD = "MNC_PH_IND_CD";
            pharM01.MNC_GRP_PH_CD = "MNC_GRP_PH_CD";
            pharM01.MNC_PH_GRP_SUB_CD = "MNC_PH_GRP_SUB_CD";
            pharM01.MNC_PH_DIR_TXT = "MNC_PH_DIR_TXT";
            pharM01.MNC_DEC_CD = "MNC_DEC_CD";
            pharM01.MNC_DEC_NO = "MNC_DEC_NO";
            pharM01.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pharM01.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pharM01.MNC_USR_ADD = "MNC_USR_ADD";
            pharM01.MNC_USR_UPD = "MNC_USR_UPD";
            pharM01.MNC_PH_PRI = "MNC_PH_PRI";
            pharM01.MNC_PH_PRO_STS = "MNC_PH_PRO_STS";
            pharM01.MNC_DOSAGE_FORM = "MNC_DOSAGE_FORM";
            pharM01.MNC_PH_STRENGTH = "MNC_PH_STRENGTH";
            pharM01.MNC_PH_COMM_THAI = "MNC_PH_COMM_THAI";
            pharM01.MNC_PH_COMM_ENG = "MNC_PH_COMM_ENG";
            pharM01.MNC_PH_DIV_CD_I = "MNC_PH_DIV_CD_I";
            pharM01.MNC_PH_MAT_FLG = "MNC_PH_MAT_FLG";
            pharM01.MNC_PH_TYP_FLG = "MNC_PH_TYP_FLG";
            pharM01.MNC_VEN_CD = "MNC_VEN_CD";
            pharM01.MNC_OLD_CD = "MNC_OLD_CD";
            pharM01.MNC_PH_WRN_CD = "MNC_PH_WRN_CD";
            pharM01.MNC_PH_AUTO = "MNC_PH_AUTO";
            pharM01.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            pharM01.MNC_PRINT_FLG = "MNC_PRINT_FLG";
            pharM01.MNC_PH_NEW_SS = "MNC_PH_NEW_SS";
            pharM01.tmt_code = "tmt_code";


        }

        public PharmacyM01 setPharmacyM01(DataTable dt)
        {
            PharmacyM01 pharM01 = new PharmacyM01();
            if (dt.Rows.Count > 0)
            {
                pharM01.MNC_PH_CD = dt.Rows[0]["MNC_PH_CD"].ToString();
                pharM01.MNC_PH_ID = dt.Rows[0]["MNC_PH_ID"].ToString();
                pharM01.MNC_PH_CTL_CD = dt.Rows[0]["MNC_PH_CTL_CD"].ToString();
                pharM01.MNC_PH_TN = dt.Rows[0]["MNC_PH_TN"].ToString();
                pharM01.MNC_PH_GN = dt.Rows[0]["MNC_PH_GN"].ToString();
                pharM01.MNC_PH_UNT_CD = dt.Rows[0]["MNC_PH_UNT_CD"].ToString();
                pharM01.MNC_PH_GRP_CD = dt.Rows[0]["MNC_PH_GRP_CD"].ToString();
                pharM01.MNC_PH_TYP_CD = dt.Rows[0]["MNC_PH_TYP_CD"].ToString();
                pharM01.MNC_PH_DIR_CD = dt.Rows[0]["MNC_PH_DIR_CD"].ToString();
                pharM01.MNC_PH_CAU_CD = dt.Rows[0]["MNC_PH_CAU_CD"].ToString();
                pharM01.MNC_PH_MIN = dt.Rows[0]["MNC_PH_MIN"].ToString();
                pharM01.MNC_PH_MAX = dt.Rows[0]["MNC_PH_MAX"].ToString();
                pharM01.MNC_PH_DAY = dt.Rows[0]["MNC_PH_DAY"].ToString();
                pharM01.MNC_PR_STD = dt.Rows[0]["MNC_PR_STD"].ToString();
                pharM01.MNC_SUP_STS = dt.Rows[0]["MNC_SUP_STS"].ToString();
                pharM01.MNC_PRO_LOC = dt.Rows[0]["MNC_PRO_LOC"].ToString();
                pharM01.MNC_PH_AT = dt.Rows[0]["MNC_PH_AT"].ToString();
                pharM01.MNC_FN_CD = dt.Rows[0]["MNC_FN_CD"].ToString();
                pharM01.MNC_PH_STS = dt.Rows[0]["MNC_PH_STS"].ToString();
                pharM01.MNC_PH_LQ = dt.Rows[0]["MNC_PH_LQ"].ToString();
                pharM01.MNC_PH_LV = dt.Rows[0]["MNC_PH_LV"].ToString();
                pharM01.MNC_PH_LI = dt.Rows[0]["MNC_PH_LI"].ToString();
                pharM01.MNC_PH_LC = dt.Rows[0]["MNC_PH_LC"].ToString();
                pharM01.MNC_PH_LD = dt.Rows[0]["MNC_PH_LD"].ToString();
                pharM01.MNC_PH_DIS_STS = dt.Rows[0]["MNC_PH_DIS_STS"].ToString();
                pharM01.MNC_PH_DIV_CD = dt.Rows[0]["MNC_PH_DIV_CD"].ToString();
                pharM01.MNC_PH_FRE_CD = dt.Rows[0]["MNC_PH_FRE_CD"].ToString();
                pharM01.MNC_PH_TIM_CD = dt.Rows[0]["MNC_PH_TIM_CD"].ToString();
                pharM01.MNC_PH_IND_CD = dt.Rows[0]["MNC_PH_IND_CD"].ToString();
                pharM01.MNC_GRP_PH_CD = dt.Rows[0]["MNC_GRP_PH_CD"].ToString();
                pharM01.MNC_PH_GRP_SUB_CD = dt.Rows[0]["MNC_PH_GRP_SUB_CD"].ToString();
                pharM01.MNC_PH_DIR_TXT = dt.Rows[0]["MNC_PH_DIR_TXT"].ToString();
                pharM01.MNC_DEC_CD = dt.Rows[0]["MNC_DEC_CD"].ToString();
                pharM01.MNC_DEC_NO = dt.Rows[0]["MNC_DEC_NO"].ToString();
                pharM01.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                pharM01.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                pharM01.MNC_USR_ADD = dt.Rows[0]["MNC_USR_ADD"].ToString();
                pharM01.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                pharM01.MNC_PH_PRI = dt.Rows[0]["MNC_PH_PRI"].ToString();
                pharM01.MNC_PH_PRO_STS = dt.Rows[0]["MNC_PH_PRO_STS"].ToString();
                pharM01.MNC_DOSAGE_FORM = dt.Rows[0]["MNC_DOSAGE_FORM"].ToString();
                pharM01.MNC_PH_STRENGTH = dt.Rows[0]["MNC_PH_STRENGTH"].ToString();
                pharM01.MNC_PH_COMM_THAI = dt.Rows[0]["MNC_PH_COMM_THAI"].ToString();
                pharM01.MNC_PH_COMM_ENG = dt.Rows[0]["MNC_PH_COMM_ENG"].ToString();
                pharM01.MNC_PH_DIV_CD_I = dt.Rows[0]["MNC_PH_DIV_CD_I"].ToString();
                pharM01.MNC_PH_MAT_FLG = dt.Rows[0]["MNC_PH_MAT_FLG"].ToString();
                pharM01.MNC_PH_TYP_FLG = dt.Rows[0]["MNC_PH_TYP_FLG"].ToString();
                pharM01.MNC_VEN_CD = dt.Rows[0]["MNC_VEN_CD"].ToString();
                pharM01.MNC_OLD_CD = dt.Rows[0]["MNC_OLD_CD"].ToString();
                pharM01.MNC_PH_WRN_CD = dt.Rows[0]["MNC_PH_WRN_CD"].ToString();
                pharM01.MNC_PH_AUTO = dt.Rows[0]["MNC_PH_AUTO"].ToString();
                pharM01.MNC_FN_TYP_CD = dt.Rows[0]["MNC_FN_TYP_CD"].ToString();
                pharM01.MNC_PRINT_FLG = dt.Rows[0]["MNC_PRINT_FLG"].ToString();
                pharM01.MNC_PH_NEW_SS = dt.Rows[0]["MNC_PH_NEW_SS"].ToString();
                pharM01.tmt_code = dt.Rows[0]["tmt_code"].ToString();
                


            }
            else
            {
                setPharmacyM01(pharM01);
            }
            return pharM01;
        }
        public PharmacyM01 setPharmacyM01(PharmacyM01 p)
        {

            p.MNC_PH_CD = "";
            p.MNC_PH_ID = "";
            p.MNC_PH_CTL_CD = "";
            p.MNC_PH_TN = "";
            p.MNC_PH_GN = "";
            p.MNC_PH_UNT_CD = "";
            p.MNC_PH_GRP_CD = "";
            p.MNC_PH_TYP_CD = "";
            p.MNC_PH_DIR_CD = "";
            p.MNC_PH_CAU_CD = "";
            p.MNC_PH_MIN = "";
            p.MNC_PH_MAX = "";
            p.MNC_PH_DAY = "";
            p.MNC_PR_STD = "";
            p.MNC_SUP_STS = "";
            p.MNC_PRO_LOC = "";
            p.MNC_PH_AT = "";
            p.MNC_FN_CD = "";
            p.MNC_PH_STS = "";
            p.MNC_PH_LQ = "";
            p.MNC_PH_LV = "";
            p.MNC_PH_LI = "";
            p.MNC_PH_LC = "";
            p.MNC_PH_LD = "";
            p.MNC_PH_DIS_STS = "";
            p.MNC_PH_DIV_CD = "";
            p.MNC_PH_FRE_CD = "";
            p.MNC_PH_TIM_CD = "";
            p.MNC_PH_IND_CD = "";
            p.MNC_GRP_PH_CD = "";
            p.MNC_PH_GRP_SUB_CD = "";
            p.MNC_PH_DIR_TXT = "";
            p.MNC_DEC_CD = "";
            p.MNC_DEC_NO = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_ADD = "";
            p.MNC_USR_UPD = "";
            p.MNC_PH_PRI = "";
            p.MNC_PH_PRO_STS = "";
            p.MNC_DOSAGE_FORM = "";
            p.MNC_PH_STRENGTH = "";
            p.MNC_PH_COMM_THAI = "";
            p.MNC_PH_COMM_ENG = "";
            p.MNC_PH_DIV_CD_I = "";
            p.MNC_PH_MAT_FLG = "";
            p.MNC_PH_TYP_FLG = "";
            p.MNC_VEN_CD = "";
            p.MNC_OLD_CD = "";
            p.MNC_PH_WRN_CD = "";
            p.MNC_PH_AUTO = "";
            p.MNC_FN_TYP_CD = "";
            p.MNC_PRINT_FLG = "";
            p.MNC_PH_NEW_SS = "";
            p.tmt_code = "";


            return p;
        }


    }
}
