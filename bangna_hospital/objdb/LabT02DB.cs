using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LabT02DB
    {
        public LabT02 labT02;
        ConnectDB conn;
        public LabT02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labT02 = new LabT02();
            labT02.MNC_REQ_YR = "MNC_REQ_YR";
            labT02.MNC_REQ_NO = "MNC_REQ_NO";
            labT02.MNC_REQ_DAT = "MNC_REQ_DAT";
            labT02.MNC_LB_CD = "MNC_LB_CD";
            labT02.MNC_REQ_STS = "MNC_REQ_STS";
            labT02.MNC_LB_RMK = "MNC_LB_RMK";
            labT02.MNC_LB_COS = "MNC_LB_COS";
            labT02.MNC_LB_PRI = "MNC_LB_PRI";
            labT02.MNC_LB_RFN = "MNC_LB_RFN";
            labT02.MNC_SPC_SEND_DAT = "MNC_SPC_SEND_DAT";
            labT02.MNC_SPC_SEND_TM = "MNC_SPC_SEND_TM";
            labT02.MNC_SPC_TYP = "MNC_SPC_TYP";
            labT02.MNC_RESULT_DAT = "MNC_RESULT_DAT";
            labT02.MNC_RESULT_TIM = "MNC_RESULT_TIM";
            labT02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            labT02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            labT02.MNC_USR_RESULT = "MNC_USR_RESULT";
            labT02.MNC_USR_RESULT_REPORT = "MNC_USR_RESULT_REPORT";
            labT02.MNC_USR_RESULT_APPROVE = "MNC_USR_RESULT_APPROVE";
            labT02.MNC_CANCEL_STS = "MNC_CANCEL_STS";
            labT02.MNC_USR_UPD = "MNC_USR_UPD";
            labT02.MNC_SND_OUT_STS = "MNC_SND_OUT_STS";
            labT02.MNC_LB_STS = "MNC_LB_STS";



        }


    }

}
