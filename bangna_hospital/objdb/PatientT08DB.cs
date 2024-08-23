using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PatientT08DB
    {
        public PatientT08 ptt08;
        ConnectDB conn;
        public PatientT08DB(ConnectDB c) 
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            ptt08 = new PatientT08();
            ptt08.MNC_AN_YR = "MNC_AN_YR";
            ptt08.MNC_AN_NO = "MNC_AN_NO";
            ptt08.MNC_HN_YR = "MNC_HN_YR";
            ptt08.MNC_HN_NO = "MNC_HN_NO";
            ptt08.MNC_AD_TIME = "MNC_AD_TIME";
            ptt08.MNC_AD_DATE = "MNC_AD_DATE";
            ptt08.MNC_AD_DT_AMT = "MNC_AD_DT_AMT";
            ptt08.MNC_PRE_NO = "MNC_PRE_NO";
            ptt08.MNC_DATE = "MNC_DATE";
            ptt08.MNC_TIME = "MNC_TIME";

            ptt08.MNC_DEP_NO = "MNC_DEP_NO";
            ptt08.MNC_DOT_CD_S = "MNC_DOT_CD_S";
            ptt08.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            ptt08.MNC_COM_CD = "MNC_COM_CD";
            ptt08.MNC_CRD_LIM_CD = "MNC_CRD_LIM_CD";
            ptt08.MNC_AD_STS = "MNC_AD_STS";
            ptt08.MNC_WD_NO = "MNC_WD_NO";
            ptt08.MNC_RM_NAM = "MNC_RM_NAM";
            ptt08.MNC_BD_NO = "MNC_BD_NO";
            ptt08.MNC_DOT_CD_R = "MNC_DOT_CD_R";

            ptt08.MNC_DN_FLG = "MNC_DN_FLG";
            ptt08.MNC_DIA_CD = "MNC_DIA_CD";
            ptt08.MNC_DIA_MEMO = "MNC_DIA_MEMO";
            ptt08.MNC_DOT_MEMO = "MNC_DOT_MEMO";
            ptt08.MNC_DS_TIME = "MNC_DS_TIME";
            ptt08.MNC_DS_CD = "MNC_DS_CD";
            ptt08.MNC_AMP_FLG = "MNC_AMP_FLG";
            ptt08.MNC_PHA_FLG = "MNC_PHA_FLG";
            ptt08.MNC_LAB_FLG = "MNC_LAB_FLG";
            ptt08.MNC_XRA_FLG = "MNC_XRA_FLG";

            ptt08.MNC_DFF_FLG = "MNC_DFF_FLG";
            ptt08.MNC_ADM_FLG = "MNC_ADM_FLG";
            ptt08.MNC_PHY_FLG = "MNC_PHY_FLG";
            ptt08.MNC_CAR_MEMO = "MNC_CAR_MEMO";
            ptt08.MNC_RM_RAT = "MNC_RM_RAT";
            ptt08.MNC_SEC_NO = "MNC_SEC_NO";
            ptt08.MNC_DS_DATE = "MNC_DS_DATE";
            ptt08.MNC_DS_TYP_CD = "MNC_DS_TYP_CD";
            ptt08.MNC_RP_NO = "MNC_RP_NO";
            ptt08.MNC_ANM_NO = "MNC_ANM_NO";

            ptt08.MNC_ANM_YR = "MNC_ANM_YR";
            ptt08.MNC_STS_FLG = "MNC_STS_FLG";
            ptt08.MNC_REF_CD = "MNC_REF_CD";
            ptt08.MNC_REF_RM = "MNC_REF_RM";
            ptt08.MNC_DEAD_FLG = "MNC_DEAD_FLG";
            ptt08.MNC_DIA_DEAD = "MNC_DIA_DEAD";
            ptt08.MNC_REF_TYP = "MNC_REF_TYP";
            ptt08.MNC_CLN_NO = "MNC_CLN_NO";
            ptt08.MNC_TURN_STS = "MNC_TURN_STS";
            ptt08.MNC_USR_ADM = "MNC_USR_ADM";

            ptt08.MNC_USR_DIS = "MNC_USR_DIS";
            ptt08.MNC_NUT_FLG = "MNC_NUT_FLG";
            ptt08.MNC_PHA_CFR_DAT = "MNC_PHA_CFR_DAT";
            ptt08.MNC_PHA_CFR_TIM = "MNC_PHA_CFR_TIM";
            ptt08.MNC_PHA_CFR_STS = "MNC_PHA_CFR_STS";
            ptt08.MNC_ID_NAM = "MNC_ID_NAM";
            ptt08.MNC_ID_STR = "MNC_ID_STR";
            ptt08.MNC_ID_EXP = "MNC_ID_EXP";
            ptt08.MNC_RES_MAS = "MNC_RES_MAS";
            ptt08.MNC_RES_SEC = "MNC_RES_SEC";

            ptt08.MNC_PAC_CD = "MNC_PAC_CD";
            ptt08.MNC_PT_DEP = "MNC_PT_DEP";
            ptt08.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            ptt08.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            ptt08.MNC_DS_LEV = "MNC_DS_LEV";
            ptt08.MNC_PAC_TYP = "MNC_PAC_TYP";
            ptt08.MNC_DIV_NO = "MNC_DIV_NO";
            ptt08.MNC_SEC_GRP_NO = "MNC_SEC_GRP_NO";
            ptt08.MNC_VN_NO = "MNC_VN_NO";
            ptt08.MNC_VN_SEQ = "MNC_VN_SEQ";

            ptt08.MNC_VN_SUM = "MNC_VN_SUM";
            ptt08.MNC_USR_DIS_2 = "MNC_USR_DIS_2";
            ptt08.MNC_USR_DIS_3 = "MNC_USR_DIS_3";
            ptt08.MNC_USR_DIS_4 = "MNC_USR_DIS_4";
            ptt08.MNC_ACT_NO = "MNC_ACT_NO";
            ptt08.MNC_REQ_MED_COUNT = "MNC_REQ_MED_COUNT";
            ptt08.MNC_REQ_INV_COUNT = "MNC_REQ_INV_COUNT";
            ptt08.MNC_OR_FLG = "MNC_OR_FLG";
            ptt08.MNC_LR_FLG = "MNC_LR_FLG";
            ptt08.MNC_HEM_FLG = "MNC_HEM_FLG";

            ptt08.MNC_ENDO_FLG = "MNC_ENDO_FLG";
            ptt08.MNC_LAB_RES_FLG = "MNC_LAB_RES_FLG";
            ptt08.MNC_XRA_RES_FLG = "MNC_XRA_RES_FLG";
            ptt08.MNC_DIAG_STS = "MNC_DIAG_STS";
            ptt08.MNC_READM_FLG = "MNC_READM_FLG";
            ptt08.MNC_READM_TYPE = "MNC_READM_TYPE";
            ptt08.MNC_DEAD_TYP = "MNC_DEAD_TYP";
            ptt08.status_import_aipn = "status_import_aipn";
            ptt08.certi_id = "certi_id";
            ptt08.certi_id_2nfleaf = "certi_id_2nfleaf";

            ptt08.certi_id_3nfleaf = "certi_id_3nfleaf";
            ptt08.certi_id_4nfleaf = "certi_id_4nfleaf";
            ptt08.table = "PATIENT_T08";
        }
        public DataTable SelectByPk(String an, String ancnt)
        {
            DataTable dt = new DataTable();
            String sql = "select ptt08.* " +
                "From PATIENT_T08 ptt08  " +
                "Where ptt08.MNC_AN_NO = '" + an + "' and  ptt08.MNC_AN_YR = '" + ancnt + "' " +
                " ";
            dt = conn.selectData(sql);
            
            return dt;
        }
        public DataTable SelectByHN(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "select ptt08.* " +
                "From PATIENT_T08 ptt08  " +
                "Where ptt08.MNC_HN_NO = '" + hn + "'  " +
                " ";
            dt = conn.selectData(sql);

            return dt;
        }
    }
}
