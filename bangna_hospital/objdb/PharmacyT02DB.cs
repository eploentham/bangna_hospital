using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PharmacyT02DB
    {
        public PharmacyT02 pharT02;
        ConnectDB conn;
        public PharmacyT02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharT02 = new PharmacyT02();
            pharT02.MNC_DOC_CD = "MNC_DOC_CD";
            pharT02.MNC_REQ_YR = "MNC_REQ_YR";
            pharT02.MNC_REQ_NO = "MNC_REQ_NO";
            pharT02.MNC_PH_CD = "MNC_PH_CD";
            pharT02.MNC_PH_QTY = "MNC_PH_QTY";
            pharT02.MNC_PH_UNTF_QTY = "MNC_PH_UNTF_QTY";
            pharT02.MNC_PH_UNT_CD = "MNC_PH_UNT_CD";
            pharT02.MNC_PH_DIR_DSC = "MNC_PH_DIR_DSC";
            pharT02.MNC_PH_PRI = "MNC_PH_PRI";
            pharT02.MNC_PH_COS = "MNC_PH_COS";
            pharT02.MNC_SUP_STS = "MNC_SUP_STS";
            pharT02.MNC_ORD_NO = "MNC_ORD_NO";
            pharT02.MNC_PH_RFN = "MNC_PH_RFN";
            pharT02.MNC_PH_DIR_CD = "MNC_PH_DIR_CD";
            pharT02.MNC_PH_FRE_CD = "MNC_PH_FRE_CD";
            pharT02.MNC_PH_TIM_CD = "MNC_PH_TIM_CD";
            pharT02.MNC_PH_CAU = "MNC_PH_CAU";
            pharT02.MNC_PH_IND = "MNC_PH_IND";
            pharT02.MNC_FN_CD = "MNC_FN_CD";
            pharT02.MNC_PHA_HID = "MNC_PHA_HID";
            pharT02.MNC_PH_FLG = "MNC_PH_FLG";
            pharT02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pharT02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pharT02.MNC_USR_ADD = "MNC_USR_ADD";
            pharT02.MNC_USR_UPD = "MNC_USR_UPD";
            pharT02.MNC_PH_DIR_TXT = "MNC_PH_DIR_TXT";
            pharT02.MNC_CANCEL_STS = "MNC_CANCEL_STS";
            pharT02.MNC_PH_REM = "MNC_PH_REM";
            pharT02.MNC_PAY_FLAG = "MNC_PAY_FLAG";
            pharT02.MNC_PH_STS = "MNC_PH_STS";

         }


    }
}
