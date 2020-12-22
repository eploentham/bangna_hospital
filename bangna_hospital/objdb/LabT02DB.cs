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

    public LabT02 setLabT02(DataTable dt)
    {
        LabT02 labT02 = new LabT02();
        if (dt.Rows.Count > 0)
        {
            labT02.MNC_REQ_YR = dt.Rows[0]["MNC_REQ_YR"].ToString();
            labT02.MNC_REQ_NO = dt.Rows[0]["MNC_REQ_NO"].ToString();
            labT02.MNC_REQ_DAT = dt.Rows[0]["MNC_REQ_DAT"].ToString();
            labT02.MNC_LB_CD = dt.Rows[0]["MNC_LB_CD"].ToString();
            labT02.MNC_REQ_STS = dt.Rows[0]["MNC_REQ_STS"].ToString();
            labT02.MNC_LB_RMK = dt.Rows[0]["MNC_LB_RMK"].ToString();
            labT02.MNC_LB_COS = dt.Rows[0]["MNC_LB_COS"].ToString();
            labT02.MNC_LB_PRI = dt.Rows[0]["MNC_LB_PRI"].ToString();
            labT02.MNC_LB_RFN = dt.Rows[0]["MNC_LB_RFN"].ToString();
            labT02.MNC_SPC_SEND_DAT = dt.Rows[0]["MNC_SPC_SEND_DAT"].ToString();
            labT02.MNC_SPC_SEND_TM = dt.Rows[0]["MNC_SPC_SEND_TM"].ToString();
            labT02.MNC_SPC_TYP = dt.Rows[0]["MNC_SPC_TYP"].ToString();
            labT02.MNC_RESULT_DAT = dt.Rows[0]["MNC_RESULT_DAT"].ToString(); ;
            labT02.MNC_RESULT_TIM = dt.Rows[0]["MNC_RESULT_TIM"].ToString();
            labT02.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
            labT02.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
            labT02.MNC_USR_RESULT = dt.Rows[0]["MNC_USR_RESULT"].ToString();
            labT02.MNC_USR_RESULT_REPORT = dt.Rows[0]["MNC_USR_RESULT_REPORT"].ToString(); 
            labT02.MNC_USR_RESULT_APPROVE = dt.Rows[0]["MNC_USR_RESULT_APPROVE"].ToString();
            labT02.MNC_CANCEL_STS = dt.Rows[0]["MNC_CANCEL_STS"].ToString();
            labT02.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
            labT02.MNC_SND_OUT_STS = dt.Rows[0]["MNC_SND_OUT_STS"].ToString();
            labT02.MNC_LB_STS = dt.Rows[0]["MNC_LB_STS"].ToString();

        }
        else
        {
            setLabT02(labT02);
        }
        return labT02;

    }
    public LabT02 setLabT02(LabT02 p)
    {
        p.MNC_REQ_YR = "";
        p.MNC_REQ_NO = "";
        p.MNC_REQ_DAT = "";
        p.MNC_LB_CD = "";
        p.MNC_REQ_STS = "";
        p.MNC_LB_RMK = "";
        p.MNC_LB_COS = "";
        p.MNC_LB_PRI = "";
        p.MNC_SPC_SEND_DAT = "";
        p.MNC_SPC_SEND_TM = "";
        p.MNC_SPC_TYP = "";
        p.MNC_RESULT_DAT = "";
        p.MNC_RESULT_TIM = "";
        p.MNC_STAMP_DAT = "";
        p.MNC_STAMP_TIM = "";
        p.MNC_USR_RESULT = "";
        p.MNC_USR_RESULT_REPORT = "";
        p.MNC_USR_RESULT_APPROVE = "";
        p.MNC_CANCEL_STS = "";
        p.MNC_USR_UPD = "";
        p.MNC_SND_OUT_STS = "";
        p.MNC_LB_STS = "";

        return p;
    }

}
