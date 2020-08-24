using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PharmacyT03DB
    {
        public PharmacyT03 pharT03;
        ConnectDB conn;
        public PharmacyT03DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        { 
            pharT03 = new PharmacyT03();
            pharT03.MNC_DOC_CD  = "MNC_DOC_CD";
            pharT03.MNC_REQ_YR  = "MNC_REQ_YR";
             pharT03.MNC_REQ_NO  = "MNC_REQ_NO";
             pharT03.MNC_REQ_DAT  = "MNC_REQ_DAT";
             pharT03.MNC_REQ_STS  = "MNC_REQ_STS";
              pharT03.MNC_SUM_COS  = "MNC_SUM_COS";
              pharT03.MNC_DEP_NO  = "MNC_DEP_NO";
             pharT03.MNC_SEC_NO  = "MNC_SEC_NO";
             pharT03.MNC_USR_LOG  = "MNC_USR_LOG";
             pharT03.MNC_DEPR_NO  = "MNC_DEPR_NO";
             pharT03.MNC_SECR_NO  = "MNC_SECR_NO";
             pharT03.MNC_STAMP_DAT  = "MNC_STAMP_DAT";
             pharT03.MNC_STAMP_TIM  = "MNC_STAMP_TIM";
             pharT03.MNC_USR_ADD  = "MNC_USR_ADD";
              pharT03.MNC_USR_UPD  = "MNC_USR_UPD";

        }
    }
}
