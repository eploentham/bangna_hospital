using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class XrayM01DB
    {

        public XrayM01 XrayM01;
        ConnectDB conn;
        public XrayM01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

            XrayM01 = new XrayM01();
            XrayM01.MNC_XR_CD = "MNC_XR_CD";
            XrayM01.MNC_XR_CTL_CD = "MNC_XR_CTL_CD";
            XrayM01.MNC_XR_DSC = "MNC_XR_DSC";
            XrayM01.MNC_XR_TYP_CD = "MNC_XR_TYP_CD";
            XrayM01.MNC_XR_GRP_CD = "MNC_XR_GRP_CD";
            XrayM01.MNC_XR_DIS_STS = "MNC_XR_DIS_STS";
            XrayM01.MNC_XR_STS = "MNC_XR_STS";
            XrayM01.MNC_DEC_CD = "MNC_DEC_CD";
            XrayM01.MNC_DEC_NO = "MNC_DEC_NO";
            XrayM01.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            XrayM01.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            XrayM01.MNC_USR_UPD = "MNC_USR_UPD";
            XrayM01.MNC_USR_ADD = "MNC_USR_ADD";
            XrayM01.MNC_OLD_CD = "MNC_OLD_CD";
            XrayM01.MNC_SUP_STS = "MNC_SUP_STS";
            XrayM01.MNC_XR_PRI = "MNC_XR_PRI";
            XrayM01.MNC_XR_DSC_STS = "MNC_XR_DSC_STS";
            XrayM01.MNC_XR_AUTO = "MNC_XR_AUTO";
            XrayM01.pacs_infinitt_code = "pacs_infinitt_code";
            XrayM01.modality_code = "modality_code";
         

        }
    }
}