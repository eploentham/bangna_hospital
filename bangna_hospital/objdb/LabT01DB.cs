using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LabT01DB
    {
        public LabT01 labT01;
        ConnectDB conn;
        public LabT01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

            labT01 = new LabT01();
            labT01.MNC_REQ_YR = "MNC_REQ_YR";
            labT01.MNC_REQ_NO = "MNC_REQ_NO";
            labT01.MNC_REQ_DAT = "MNC_REQ_DAT";
            labT01.MNC_REQ_DEP = "MNC_REQ_DEP";
            labT01.MNC_REQ_STS = "MNC_REQ_STS";
            labT01.MNC_REQ_TIM = "MNC_REQ_TIM";
            labT01.MNC_HN_YR = "MNC_HN_YR";
            labT01.MNC_HN_NO = "MMNC_HN_NO"; 
            labT01.MNC_AN_YR = "MNC_AN_YR";
            labT01.MNC_AN_NO = "MNC_AN_NO";
            labT01.MNC_PRE_NO = "MNC_PRE_NO";
            labT01.MNC_DATE = "MNC_DATE";
            labT01.MNC_TIME = "MNC_TIME";
            labT01.MNC_DOT_CD = "MNC_DOT_CD";
            labT01.MNC_WD_NO = "MNC_WD_NO";
            labT01.MNC_RM_NAM = "MNC_RM_NAM";
            labT01.MNC_BD_NO = "MNC_BD_NO";
            labT01.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            labT01.MNC_COM_CD = "MNC_COM_CD";
            labT01.MNC_REM = "MNC_REM";
            labT01.MNC_LB_STS = "MNC_LB_STS";
            labT01.MNC_CAL_NO = "MNC_CAL_NO";
            labT01.MNC_EMPR_CD = "MNC_EMPR_CD";
            labT01.MNC_EMPC_CD = "MNC_EMPC_CD";
            labT01.MNC_ORD_DOT = "MNC_ORD_DOT";
            labT01.MNC_CFM_DOT = "MNC_CFM_DOT";
            labT01.MNC_DOC_YR = "MNC_DOC_YR";
            labT01.MNC_DOC_NO = "MNC_DOC_NO";
            labT01.MNC_DOC_DAT = "MNC_DOC_DAT";
            labT01.MNC_DOC_CD = "MNC_DOC_CD";
            labT01.MNC_SPC_SEND_DAT = "MNC_SPC_SEND_DAT";
            labT01.MNC_SPC_SEND_TM = "MNC_SPC_SEND_TM";
            labT01.MNC_SPC_TYP = "MNC_SPC_TYP";
            labT01.MNC_REMARK = "MNC_REMARK";
            labT01.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            labT01.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            labT01.MNC_CANCEL_STS = "MNC_CANCEL_STS";
            labT01.MNC_PAC_CD = "MNC_PAC_CD";
            labT01.MNC_PAC_TYP = "MNC_PAC_TYP";
            labT01.MNC_DANGER_FLG = "MNC_DANGER_FLG";
            labT01.MNC_DIET_FLG = "MNC_DIET_FLG";
            labT01.MNC_MED_FLG = "MNC_MED_FLG";
            labT01.MNC_LAB_FN_TYP_CD = "MNC_LAB_FN_TYP_CD";
            labT01.MNC_IP_ADD1 = "MNC_IP_ADD1";
            labT01.MNC_IP_ADD2 = "MNC_IP_ADD2";
            labT01.MNC_IP_ADD3 = "MNC_IP_ADD3";
            labT01.MNC_IP_ADD4 = "MNC_IP_ADD4";
            labT01.MNC_PATNAME = "MNC_PATNAME";
            labT01.MNC_LOAD_STS = "MNC_LOAD_STS";
            labT01.MNC_IP_REC = "MNC_IP_REC";
           


        }
    }
}