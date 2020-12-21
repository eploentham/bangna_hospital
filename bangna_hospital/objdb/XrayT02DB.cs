using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class XrayT02DB
    {
        public XrayT02 XrayT02;
        ConnectDB conn;
        public XrayT02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

            XrayT02 = new XrayT02();
            XrayT02.MNC_REQ_YR = "MNC_REQ_YR";
            XrayT02.MNC_REQ_NO = "MNC_REQ_NO";
            XrayT02.MNC_REQ_DAT = "MNC_REQ_DAT";
            XrayT02.MNC_REQ_STS = "MNC_REQ_STS";
            XrayT02.MNC_XR_CD = "MNC_XR_CD";
            XrayT02.MNC_XR_RMK = "MNC_XR_RMK";
            XrayT02.MNC_XR_COS = "MNC_XR_COS";
            XrayT02.MNC_XR_PRI = "MNC_XR_PRI";
            XrayT02.MNC_XR_RFN = "MNC_XR_RFN";
            XrayT02.MNC_XR_PRI_R = "MNC_XR_PRI_R";
            XrayT02.MNC_FLG_K = "MNC_FLG_K";
            XrayT02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            XrayT02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            XrayT02.MNC_XR_COS_R = "MNC_XR_COS_R";
            XrayT02.MNC_DOT_CD_DF = "MNC_DOT_CD_DF";
            XrayT02.MNC_DOT_GRP_CD = "MNC_DOT_GRP_CD";
            XrayT02.MNC_ACT_DAT = "MNC_ACT_DAT";
            XrayT02.MNC_ACT_TIM = "MNC_ACT_TIM";
            XrayT02.MNC_CANCEL_STS = "MNC_CANCEL_STS";
            XrayT02.MNC_USR_UPD = "MNC_USR_UPD";
            XrayT02.MNC_SND_OUT_STS = "MNC_SND_OUT_STS";
            XrayT02.MNC_XR_STS = "MNC_XR_STS";
            XrayT02.status_pacs = "status_pacs";
         

        }
    }
}
