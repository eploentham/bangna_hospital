using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PharmacyM02DB
    {
        public PharmacyM02 pharM02;
        ConnectDB conn;
        public PharmacyM02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharM02 = new PharmacyM02();
            pharM02.MNC_PH_CD = "MNC_PH_CD";
            pharM02.MNC_DEP_NO = "MNC_DEP_NO";
            pharM02.MNC_SEC_NO = "MNC_SEC_NO";
            pharM02.MNC_PH_LOC = "MNC_PH_LOC";
            pharM02.MNC_PH_MIN = "MNC_PH_MIN";
            pharM02.MNC_PH_MAX = "MNC_PH_MAX";
            pharM02.MNC_PH_REC_QTY = "MNC_PH_REC_QTY";
            pharM02.MNC_PH_ISS_QTY = "MNC_PH_ISS_QTY";
            pharM02.MNC_PH_ADJ_QTY = "MNC_PH_ADJ_QTY";
            pharM02.MNC_PH_QTY = "MNC_PH_QTY";
            pharM02.MNC_PH_UNT_CD = "MNC_PH_UNT_CD";
            pharM02.MNC_FREEZ_QTY = "MNC_FREEZ_QTY";
            pharM02.MNC_FREEZ_DAT = "MNC_FREEZ_DAT";
            pharM02.MNC_FREEZ_ISS = "MNC_FREEZ_ISS";
            pharM02.MNC_LOT_NO = "MNC_LOT_NO";
            pharM02.MNC_EXP_DA = "MNC_EXP_DA";
            pharM02.MNC_PH_STS = "MNC_PH_STS";
            pharM02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pharM02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pharM02.MNC_USR_ADD = "MNC_USR_ADD";
            pharM02.MNC_USR_UPD = "MNC_USR_UPD";
            pharM02.MNC_MAC_ID = "MNC_MAC_ID";




         }

     }
}
