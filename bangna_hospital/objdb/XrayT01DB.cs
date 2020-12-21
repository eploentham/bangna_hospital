using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class XrayT01DB
    {
        public XrayT01 XrayT01;
        ConnectDB conn;
        public XrayT01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

            XrayT01 = new XrayT01();
            XrayT01.MNC_REQ_YR = "MNC_REQ_YR";
            XrayT01.MNC_REQ_NO = "MNC_REQ_NO";
            XrayT01.MNC_REQ_DAT = "MNC_REQ_DAT";
            XrayT01.MNC_REQ_DEP = "MNC_REQ_DEP";
            XrayT01.MNC_REQ_STS = "MNC_REQ_STS";
            XrayT01.MNC_REQ_TIM = "MNC_REQ_TIM";
            XrayT01.MNC_HN_YR = "MNC_HN_YR";
            XrayT01.MNC_HN_NO = "MNC_HN_NO";
            XrayT01.MNC_AN_YR = "MNC_AN_YR";
            XrayT01.MNC_AN_NO = "MNC_AN_NO";
            XrayT01.MNC_PRE_NO = "MNC_PRE_NO";
            XrayT01.MNC_DATE = "MNC_DATE";
            XrayT01.MNC_TIME = "MNC_TIME";
            XrayT01.MNC_DOT_CD = "MNC_DOT_CD";
            XrayT01.MNC_WD_NO = "MNC_WD_NO";
            XrayT01.MNC_RM_NAM = "MNC_RM_NAM";
            XrayT01.MNC_BD_NO = "MNC_BD_NO";
            XrayT01.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            XrayT01.MNC_COM_CD = "MNC_COM_CD";
            XrayT01.MNC_REM = "MNC_REM";
            XrayT01.MNC_XR_STS = "MNC_XR_STS";
            XrayT01.MNC_CAL_NO = "MNC_CAL_NO";
            XrayT01.MNC_EMPR_CD = "MNC_EMPR_CD";
            XrayT01.MNC_EMPC_CD = "MNC_EMPC_CD";
            XrayT01.MNC_ORD_DOT = "MNC_ORD_DOT";
            XrayT01.MNC_CFM_DOT = "MNC_CFM_DOT";
            XrayT01.MNC_DOC_YR = "MNC_DOC_YR";
            XrayT01.MNC_DOC_NO = "MNC_DOC_NO";
            XrayT01.MNC_DOC_DAT = "MNC_DOC_DAT";
            XrayT01.MNC_DOC_CD = "MNC_DOC_CD";
            XrayT01.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            XrayT01.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            XrayT01.MNC_CANCEL_STS = "MNC_CANCEL_STS";
            XrayT01.MNC_PAC_CD = "MNC_PAC_CD";
            XrayT01.MNC_PAC_TYP = "MNC_PAC_TYP";
            XrayT01.status_pacs = "status_pacs";



        }
    }
}
