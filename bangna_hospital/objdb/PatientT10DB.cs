using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PatientT10DB
    {
        public PatientT10 pt10;
        ConnectDB conn;
        public PatientT10DB(ConnectDB c) {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pt10 = new PatientT10();
            pt10.MNC_HN_YR = "MNC_HN_YR";
            pt10.MNC_HN_NO = "MNC_HN_NO";
            pt10.MNC_RSV_NO = "MNC_RSV_NO";
            pt10.MNC_RSV_DATE = "MNC_RSV_DATE";
            pt10.MNC_RSV_TIME = "MNC_RSV_TIME";
            pt10.MNC_REQ_STS = "MNC_REQ_STS";
            pt10.MNC_HN_NAM = "MNC_HN_NAM";
            pt10.MNC_AD_DT_AMT = "MNC_AD_DT_AMT";
            pt10.MNC_PRE_NO = "MNC_PRE_NO";
            pt10.MNC_ARV_DATE = "MNC_ARV_DATE";
            pt10.MNC_ARV_TIME = "MNC_ARV_TIME";
            pt10.MNC_DEP_NO = "MNC_DEP_NO";
            pt10.MNC_RSV_STS = "MNC_RSV_STS";
            pt10.MNC_AD_STS = "MNC_AD_STS";
            pt10.MNC_WD_NO_O = "MNC_WD_NO_O";
            pt10.MNC_RM_NAM_O = "MNC_RM_NAM_O";
            pt10.MNC_BD_NO_O = "MNC_BD_NO_O";
            pt10.MNC_WD_NO = "MNC_WD_NO";
            pt10.MNC_RM_NAM = "MNC_RM_NAM";
            pt10.MNC_BD_NO = "MNC_BD_NO";
            pt10.MNC_DIA_CD = "MNC_DIA_CD";
            pt10.MNC_DIA_MEMO = "MNC_DIA_MEMO";
            pt10.MNC_DOT_CD = "MNC_DOT_CD";
            pt10.MNC_EMP_CD = "MNC_EMP_CD";
            pt10.MNC_DATE = "MNC_DATE";
            pt10.MNC_TIME = "MNC_TIME";
            pt10.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            pt10.MNC_EMPR_CD = "MNC_EMPR_CD";
            pt10.MNC_EMPC_CD = "MNC_EMPC_CD";
            pt10.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pt10.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pt10.MNC_WAD_CD = "MNC_WAD_CD";

            pt10.table = "PATIENT_T10";//Table Observe
        }
        private void chkNull(PatientT10 p)
        {
            long chk = 0;
            decimal chk1 = 0;

            p.MNC_HN_NO = p.MNC_HN_NO == null ? "" : p.MNC_HN_NO;
            p.MNC_HN_YR = p.MNC_HN_YR == null ? "" : p.MNC_HN_YR;
            p.MNC_DATE = p.MNC_DATE == null ? "" : p.MNC_DATE;
            p.MNC_TIME = p.MNC_TIME == null ? "" : p.MNC_TIME;

            p.MNC_RSV_NO = p.MNC_RSV_NO == null ? "" : p.MNC_RSV_NO;
            p.MNC_RSV_DATE = p.MNC_RSV_DATE == null ? "" : p.MNC_RSV_DATE;
            p.MNC_RSV_TIME = p.MNC_RSV_TIME == null ? "" : p.MNC_RSV_TIME;
            p.MNC_REQ_STS = p.MNC_REQ_STS == null ? "" : p.MNC_REQ_STS;
            p.MNC_HN_NAM = p.MNC_HN_NAM == null ? "" : p.MNC_HN_NAM;
            p.MNC_AD_DT_AMT = p.MNC_AD_DT_AMT == null ? "" : p.MNC_AD_DT_AMT;
            p.MNC_ARV_DATE = p.MNC_ARV_DATE == null ? "" : p.MNC_ARV_DATE;
            p.MNC_ARV_TIME = p.MNC_ARV_TIME == null ? "" : p.MNC_ARV_TIME;
            p.MNC_RSV_STS = p.MNC_RSV_STS == null ? "" : p.MNC_RSV_STS;

            p.MNC_AD_STS = p.MNC_AD_STS == null ? "" : p.MNC_AD_STS;
            p.MNC_WD_NO_O = p.MNC_WD_NO_O == null ? "" : p.MNC_WD_NO_O;
            p.MNC_RM_NAM_O = p.MNC_RM_NAM_O == null ? "" : p.MNC_RM_NAM_O;
            p.MNC_BD_NO_O = p.MNC_BD_NO_O == null ? "" : p.MNC_BD_NO_O;
            p.MNC_WD_NO = p.MNC_WD_NO == null ? "" : p.MNC_WD_NO;
            p.MNC_RM_NAM = p.MNC_RM_NAM == null ? "" : p.MNC_RM_NAM;
            p.MNC_BD_NO = p.MNC_BD_NO == null ? "" : p.MNC_BD_NO;
            p.MNC_DIA_CD = p.MNC_DIA_CD == null ? "" : p.MNC_DIA_CD;

            p.MNC_DIA_MEMO = p.MNC_DIA_MEMO == null ? "" : p.MNC_DIA_MEMO;
            p.MNC_DOT_CD = p.MNC_DOT_CD == null ? "" : p.MNC_DOT_CD;
            p.MNC_EMP_CD = p.MNC_EMP_CD == null ? "" : p.MNC_EMP_CD;
            p.MNC_FN_TYP_CD = p.MNC_FN_TYP_CD == null ? "" : p.MNC_FN_TYP_CD;
            p.MNC_EMPR_CD = p.MNC_EMPR_CD == null ? "" : p.MNC_EMPR_CD;
            p.MNC_EMPC_CD = p.MNC_EMPC_CD == null ? "" : p.MNC_EMPC_CD;
            p.MNC_STAMP_DAT = p.MNC_STAMP_DAT == null ? "" : p.MNC_STAMP_DAT;
            p.MNC_STAMP_TIM = p.MNC_STAMP_TIM == null ? "" : p.MNC_STAMP_TIM;
            p.MNC_WAD_CD = p.MNC_WAD_CD == null ? "" : p.MNC_WAD_CD;

            p.MNC_PRE_NO = long.TryParse(p.MNC_PRE_NO, out chk) ? chk.ToString() : "0";
            p.MNC_DEP_NO = long.TryParse(p.MNC_DEP_NO, out chk) ? chk.ToString() : "0";
            
        }
    }
}
