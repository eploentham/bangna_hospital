using bangna_hospital.object1;
using System;
using System.Collections.Generic;
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
    }
}
