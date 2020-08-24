using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PharmacyT04DB
    {
        public PharmacyT04 pharT04;
        ConnectDB conn;
        public PharmacyT04DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharT04 = new PharmacyT04();
            pharT04.MNC_DOC_CD = "MNC_DOC_CD";
            pharT04.MNC_REQ_YR = "MNC_REQ_YR";
            pharT04.MNC_REQ_NO = "MNC_REQ_NO";
            pharT04.MNC_PH_CD = "MNC_PH_CD";
            pharT04.MNC_PH_QTY = "MNC_PH_QTY";
            pharT04.MNC_PH_UNTF_QTY = "MNC_PH_UNTF_QTY";
            pharT04.MNC_PH_UNT_CD = "MNC_PH_UNT_CD";
            pharT04.MNC_PH_COS = "MNC_PH_COS";
            pharT04.MNC_SUP_STS = "MNC_SUP_STS";
            pharT04.MNC_DEPC_NO = "MNC_DEPC_NO";
            pharT04.MNC_SECC_NO = "MNC_SECC_NO";
            pharT04.MNC_PAY_STS = "MNC_PAY_STS";
            pharT04.MNC_PH_QTY_PAY = "MNC_PH_QTY_PAY";
            pharT04.MNC_PAY_CD = "MNC_PAY_CD";
            pharT04.MNC_PH_DSC = "MNC_PH_DSC";
            pharT04.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pharT04.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pharT04.MNC_USR_ADD = "MNC_USR_ADD";
            pharT04.MNC_USR_UPD = "MNC_USR_UPD";
        }

    }

}
