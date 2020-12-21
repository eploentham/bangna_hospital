using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class XrayM02DB
    {
        public XrayM02 XrayM02;
        ConnectDB conn;
        public XrayM02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            XrayM02 = new XrayM02();
            XrayM02.MNC_XR_CD = "MNC_XR_CD";
            XrayM02.MNC_FN_CD = "MNC_FN_CD";
            XrayM02.MNC_PRI_STS = "MNC_PRI_STS";
            XrayM02.MNC_XR_PRI = "MNC_XR_PRI";
            XrayM02.MNC_XR_COS = "MNC_XR_COS";
            XrayM02.MNC_XR_PRI01 = "MNC_XR_PRI01";
            XrayM02.MNC_XR_PRI02 = "MNC_XR_PRI02";
            XrayM02.MNC_XR_PRI03 = "MNC_XR_PRI03";
            XrayM02.MNC_XR_PRI04 = "MNC_XR_PRI04";
            XrayM02.MNC_XR_PRI05 = "MNC_XR_PRI05";
            XrayM02.MNC_XR_PRI06 = "MNC_XR_PRI06";
            XrayM02.MNC_XR_PRI07 = "MNC_XR_PRI07";
            XrayM02.MNC_XR_PRI08 = "MNC_XR_PRI08";
            XrayM02.MNC_XR_PRI09 = "MNC_XR_PRI09";
            XrayM02.MNC_XR_PRI10 = "MNC_XR_PRI10";
            XrayM02.MNC_XR_PRI11 = "MNC_XR_PRI11";
            XrayM02.MNC_XR_PRI12 = "MNC_XR_PRI12";
            XrayM02.MNC_XR_PRI13 = "MNC_XR_PRI13";
            XrayM02.MNC_XR_PRI14 = "MNC_XR_PRI14";
            XrayM02.MNC_XR_PRI15 = "MNC_XR_PRI15";
            XrayM02.MNC_XR_PRI16 = "MNC_XR_PRI16";
            XrayM02.MNC_XR_PRI17 = "MNC_XR_PRI17";
            XrayM02.MNC_XR_PRI18 = "MNC_XR_PRI18";
            XrayM02.MNC_XR_PRI19 = "MNC_XR_PRI19";
            XrayM02.MNC_XR_PRI20 = "MNC_XR_PRI20";
            XrayM02.MNC_XR_RFN = "MNC_XR_RFN";
            XrayM02.MNC_CHANGE_PRI_STS = "MNC_CHANGE_PRI_STS";
            XrayM02.MNC_SHOW_PRI_STS = "MNC_SHOW_PRI_STS";
            XrayM02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            XrayM02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            XrayM02.MNC_USR_ADD = "MNC_USR_ADD";
            XrayM02.MNC_USR_UPD = "MNC_USR_UPD";
            XrayM02.MNC_CHARGE_NO = "MNC_CHARGE_NO";
            XrayM02.MNC_XR_DIS_STS = "MNC_XR_DIS_STS";



        }
    }
}
