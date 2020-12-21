using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LabM02DB
    {
        public LabM02 labM02;
        ConnectDB conn;
        public LabM02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labM02 = new LabM02();
            labM02.MNC_LB_CD = "MNC_LB_CD";
            labM02.MNC_FN_CD = "MNC_FN_CD";
            labM02.MNC_PRI_STS = "MNC_PRI_STS";
            labM02.MNC_LB_PRI = "MNC_LB_PRI";
            labM02.MNC_LB_COS = "MNC_LB_COS";
            labM02.MNC_LB_PRI01 = "MNC_LB_PRI01";
            labM02.MNC_LB_PRI02 = "MNC_LB_PRI02";
            labM02.MNC_LB_PRI03 = "MNC_LB_PRI03";
            labM02.MNC_LB_PRI04 = "MNC_LB_PRI04";
            labM02.MNC_LB_PRI05 = "MNC_LB_PRI05";
            labM02.MNC_LB_PRI06 = "MNC_LB_PRI06";
            labM02.MNC_LB_PRI07 = "MNC_LB_PRI07";
            labM02.MNC_LB_PRI08 = "MNC_LB_PRI08";
            labM02.MNC_LB_PRI09 = "MNC_LB_PRI09";
            labM02.MNC_LB_PRI10 = "MNC_LB_PRI10";
            labM02.MNC_LB_PRI11 = "MNC_LB_PRI11";
            labM02.MNC_LB_PRI12 = "MNC_LB_PRI12";
            labM02.MNC_LB_PRI13 = "MNC_LB_PRI13";
            labM02.MNC_LB_PRI14 = "MNC_LB_PRI14";
            labM02.MNC_LB_PRI15 = "MNC_LB_PRI15";
            labM02.MNC_LB_PRI16 = "MNC_LB_PRI16";
            labM02.MNC_LB_PRI17 = "MNC_LB_PRI17";
            labM02.MNC_LB_PRI18 = "MNC_LB_PRI18";
            labM02.MNC_LB_PRI19 = "MNC_LB_PRI19";
            labM02.MNC_LB_PRI20 = "MNC_LB_PRI20";
            labM02.MNC_LB_RFN = "MNC_LB_RFN";
            labM02.MNC_LBC_CD = "MNC_LBC_CD";
            labM02.MNC_LBC_PRI = "MNC_LBC_PRI";
            labM02.MNC_LBC_COS = "MNC_LBC_COS";
            labM02.MNC_CHANGE_PRI_STS = "MNC_CHANGE_PRI_STS";
            labM02.MNC_SHOW_PRI_STS = "MNC_SHOW_PRI_STS";
            labM02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            labM02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            labM02.MNC_USR_ADD = "MNC_USR_ADD";
            labM02.MNC_USR_UPD = "MNC_USR_UPD";
            labM02.MNC_CHARGE_NO = "MNC_CHARGE_NO";

        }

    }



}
