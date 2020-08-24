using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PharmacyT06DB
    {
        public PharmacyT06 pharT06;
        ConnectDB conn;
        public PharmacyT06DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
        pharT06 = new PharmacyT06();
        pharT06.MNC_DOC_CD  = "MNC_DOC_CD";
        pharT06.MNC_CFR_YR  = "MNC_CFR_YR";
        pharT06.MNC_CFR_NO  = "MNC_CFR_NO";
        pharT06.MNC_CFR_DAT  = "MNC_CFR_DAT";
        pharT06.MNC_PH_CD  = "MNC_PH_CD";
        pharT06.MNC_NO  = "MNC_NO";
        pharT06.MNC_LOT_YR  = "MNC_LOT_YR";
        pharT06.MNC_LOT_NO  = "MNC_LOT_NO";
        pharT06.MNC_PH_QTY  = "MNC_PH_QTY";
        pharT06.MNC_PH_UNTF_QTY  = "MNC_PH_UNTF_QTY";
        pharT06.MNC_PH_QTY_PAID  = "MNC_PH_QTY_PAID";
        pharT06.MNC_PH_UNT_CD  = "MNC_PH_UNT_CD";
        pharT06.MNC_PH_PRI  = "MNC_PH_PRI";
        pharT06.MNC_PH_COS  = "MNC_PH_COS";
        pharT06.MNC_PH_DIR_DSC  = "MNC_PH_DIR_DSC";
        pharT06.MNC_PAY_CD  = "MNC_PAY_CD";
        pharT06.MNC_ORD_NO  = "MNC_ORD_NO";
        pharT06.MNC_PH_RFN  = "MNC_PH_RFN";
        pharT06.MNC_PH_DIR_CD  = "MNC_PH_DIR_CD";
        pharT06.MNC_PH_FRE_CD  = "MNC_PH_FRE_CD";
        pharT06.MNC_PH_TIM_CD  = "MNC_PH_TIM_CD";
        pharT06.MNC_RUN_DAT  = "MNC_RUN_DAT";
        pharT06.MNC_RUN_NO  = "MNC_RUN_NO";
        pharT06.MNC_REQ_TYP  = "MNC_REQ_TYP";
        pharT06.MNC_PH_REM  = "MNC_PH_REM";
        pharT06.MNC_STAMP_DAT  = "MNC_STAMP_DAT";
        pharT06.MNC_STAMP_TIM  = "MNC_STAMP_TIM";
        pharT06.MNC_USR_ADD  = "MNC_USR_ADD";
        pharT06.MNC_USR_UPD  = "MNC_USR_UPD";
        pharT06.MNC_PH_DIR_TXT  = "MNC_PH_DIR_TXT";
        pharT06.MNC_CANCEL_STS  = "MNC_CANCEL_STS";
        pharT06.MNC_PH_QTY_PAY  = "MNC_PH_QTY_PAY";
        pharT06.MNC_PH_QTY_RFN  = "MNC_PH_QTY_RFN";
        pharT06.MNC_PAY_FLAG  = "MNC_PAY_FLAG";
    }
    }
}
