using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LabT01DB
    {
        public LabT01 labT01;
        ConnectDB conn;
        public LabT01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

            labT01 = new LabT01();
            labT01.MNC_REQ_YR = "MNC_REQ_YR";
            labT01.MNC_REQ_NO = "MNC_REQ_NO";
            labT01.MNC_REQ_DAT = "MNC_REQ_DAT";
            labT01.MNC_REQ_DEP = "MNC_REQ_DEP";
            labT01.MNC_REQ_STS = "MNC_REQ_STS";
            labT01.MNC_REQ_TIM = "MNC_REQ_TIM";
            labT01.MNC_HN_YR = "MNC_HN_YR";
            labT01.MNC_HN_NO = "MMNC_HN_NO";
            labT01.MNC_AN_YR = "MNC_AN_YR";
            labT01.MNC_AN_NO = "MNC_AN_NO";
            labT01.MNC_PRE_NO = "MNC_PRE_NO";
            labT01.MNC_DATE = "MNC_DATE";
            labT01.MNC_TIME = "MNC_TIME";
            labT01.MNC_DOT_CD = "MNC_DOT_CD";
            labT01.MNC_WD_NO = "MNC_WD_NO";
            labT01.MNC_RM_NAM = "MNC_RM_NAM";
            labT01.MNC_BD_NO = "MNC_BD_NO";
            labT01.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            labT01.MNC_COM_CD = "MNC_COM_CD";
            labT01.MNC_REM = "MNC_REM";
            labT01.MNC_LB_STS = "MNC_LB_STS";
            labT01.MNC_CAL_NO = "MNC_CAL_NO";
            labT01.MNC_EMPR_CD = "MNC_EMPR_CD";
            labT01.MNC_EMPC_CD = "MNC_EMPC_CD";
            labT01.MNC_ORD_DOT = "MNC_ORD_DOT";
            labT01.MNC_CFM_DOT = "MNC_CFM_DOT";
            labT01.MNC_DOC_YR = "MNC_DOC_YR";
            labT01.MNC_DOC_NO = "MNC_DOC_NO";
            labT01.MNC_DOC_DAT = "MNC_DOC_DAT";
            labT01.MNC_DOC_CD = "MNC_DOC_CD";
            labT01.MNC_SPC_SEND_DAT = "MNC_SPC_SEND_DAT";
            labT01.MNC_SPC_SEND_TM = "MNC_SPC_SEND_TM";
            labT01.MNC_SPC_TYP = "MNC_SPC_TYP";
            labT01.MNC_REMARK = "MNC_REMARK";
            labT01.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            labT01.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            labT01.MNC_CANCEL_STS = "MNC_CANCEL_STS";
            labT01.MNC_PAC_CD = "MNC_PAC_CD";
            labT01.MNC_PAC_TYP = "MNC_PAC_TYP";
            labT01.MNC_DANGER_FLG = "MNC_DANGER_FLG";
            labT01.MNC_DIET_FLG = "MNC_DIET_FLG";
            labT01.MNC_MED_FLG = "MNC_MED_FLG";
            labT01.MNC_LAB_FN_TYP_CD = "MNC_LAB_FN_TYP_CD";
            labT01.MNC_IP_ADD1 = "MNC_IP_ADD1";
            labT01.MNC_IP_ADD2 = "MNC_IP_ADD2";
            labT01.MNC_IP_ADD3 = "MNC_IP_ADD3";
            labT01.MNC_IP_ADD4 = "MNC_IP_ADD4";
            labT01.MNC_PATNAME = "MNC_PATNAME";
            labT01.MNC_LOAD_STS = "MNC_LOAD_STS";
            labT01.MNC_IP_REC = "MNC_IP_REC";



        }
    }


    public LabT01 setLabT01(DataTable dt)
    {
        LabT01 labT01 = new LabT01();
        if (dt.Rows.Count > 0)
        {
            labT01.MNC_REQ_YR = dt.Rows[0]["MNC_REQ_YR"].ToString();
            labT01.MNC_REQ_NO = dt.Rows[0]["MNC_REQ_NO"].ToString();
            labT01.MNC_REQ_DAT = dt.Rows[0]["MNC_REQ_DAT"].ToString();
            labT01.MNC_REQ_DEP = dt.Rows[0]["MNC_REQ_DEP"].ToString();
            labT01.MNC_REQ_STS = dt.Rows[0]["MNC_REQ_STS"].ToString();
            labT01.MNC_REQ_TIM = dt.Rows[0]["MNC_REQ_TIM"].ToString();
            labT01.MNC_HN_YR = dt.Rows[0]["MNC_HN_YR"].ToString();
            labT01.MNC_HN_NO = dt.Rows[0]["MNC_HN_NO"].ToString();
            labT01.MNC_AN_YR = dt.Rows[0]["MNC_AN_YR"].ToString();
            labT01.MNC_AN_NO = dt.Rows[0]["MNC_AN_NO"].ToString();
            labT01.MNC_PRE_NO = dt.Rows[0]["MNC_PRE_NO"].ToString();
            labT01.MNC_DATE = dt.Rows[0]["MNC_DATE"].ToString();
            labT01.MNC_TIME = dt.Rows[0]["MNC_TIME"].ToString();
            labT01.MNC_DOT_CD = dt.Rows[0]["MNC_DOT_CD"].ToString();
            labT01.MNC_WD_NO = dt.Rows[0]["MNC_WD_NO"].ToString();
            labT01.MNC_RM_NAM = dt.Rows[0]["MNC_RM_NAM"].ToString();
            labT01.MNC_BD_NO = dt.Rows[0]["MNC_BD_NO"].ToString();
            labT01.MNC_FN_TYP_CD = dt.Rows[0]["MNC_FN_TYP_CD"].ToString();
            labT01.MNC_COM_CD = dt.Rows[0]["MNC_COM_CD"].ToString();
            labT01.MNC_REM = dt.Rows[0]["MNC_REM"].ToString();
            labT01.MNC_LB_STS = dt.Rows[0]["MNC_LB_STS"].ToString();
            labT01.MNC_CAL_NO = dt.Rows[0]["MNC_CAL_NO"].ToString();
            labT01.MNC_EMPR_CD = dt.Rows[0]["MNC_EMPR_CD"].ToString();
            labT01.MNC_EMPC_CD = dt.Rows[0]["MNC_EMPC_CD"].ToString();
            labT01.MNC_ORD_DOT = dt.Rows[0]["MNC_ORD_DOT"].ToString();
            labT01.MNC_CFM_DOT = dt.Rows[0]["MNC_CFM_DOT"].ToString(); ;
            labT01.MNC_DOC_YR = dt.Rows[0]["MNC_DOC_YR"].ToString();
            labT01.MNC_DOC_NO = dt.Rows[0]["MNC_DOC_NO"].ToString();
            labT01.MNC_DOC_DAT = dt.Rows[0]["MNC_DOC_DAT"].ToString();
            labT01.MNC_DOC_CD = dt.Rows[0]["MNC_DOC_CD"].ToString();
            labT01.MNC_SPC_SEND_DAT = dt.Rows[0]["MNC_SPC_SEND_DAT"].ToString();
            labT01.MNC_SPC_SEND_TM = dt.Rows[0]["MNC_SPC_SEND_TM"].ToString();
            labT01.MNC_SPC_TYP = dt.Rows[0]["MNC_SPC_TYP"].ToString();
            labT01.MNC_REMARK = dt.Rows[0]["MNC_REMARK"].ToString();
            labT01.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
            labT01.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
            labT01.MNC_CANCEL_STS = dt.Rows[0]["MNC_CANCEL_STS"].ToString();
            labT01.MNC_PAC_CD = dt.Rows[0]["MNC_PAC_CD"].ToString();
            labT01.MNC_PAC_TYP = dt.Rows[0]["MNC_PAC_TYP"].ToString();
            labT01.MNC_DANGER_FLG = dt.Rows[0]["MNC_DANGER_FLG"].ToString();
            labT01.MNC_DIET_FLG = dt.Rows[0]["MNC_DIET_FLG"].ToString();
            labT01.MNC_MED_FLG = dt.Rows[0]["MNC_MED_FLG"].ToString();
            labT01.MNC_LAB_FN_TYP_CD = dt.Rows[0]["MNC_LAB_FN_TYP_CD"].ToString();
            labT01.MNC_IP_ADD1 = dt.Rows[0]["MNC_IP_ADD1"].ToString();
            labT01.MNC_IP_ADD2 = dt.Rows[0]["MNC_IP_ADD2"].ToString();
            labT01.MNC_IP_ADD3 = dt.Rows[0]["MNC_IP_ADD3"].ToString();
            labT01.MNC_IP_ADD4 = dt.Rows[0]["MNC_IP_ADD4"].ToString();
            labT01.MNC_PATNAME = dt.Rows[0]["MNC_PATNAME"].ToString();
            labT01.MNC_LOAD_STS = dt.Rows[0]["MNC_LOAD_STS"].ToString();
            labT01.MNC_IP_REC = dt.Rows[0]["MNC_IP_REC"].ToString();
        }
        else
        {
            setLabT01(labT01);
        }
        return labT01;
    }
    public LabT01 setLabT01(LabT01 p)
    {
        p.MNC_REQ_YR = "";
        p.MNC_REQ_NO = "";
        p.MNC_REQ_DAT = "";
        p.MNC_REQ_DEP = "";
        p.MNC_REQ_STS = "";
        p.MNC_REQ_TIM = "";
        p.MNC_HN_YR = "";
        p.MNC_AN_YR = "";
        p.MNC_AN_NO = "";
        p.MNC_PRE_NO = "";
        p.MNC_DATE = "";
        p.MNC_TIME = "";
        p.MNC_DOT_CD = "";
        p.MNC_WD_NO = "";
        p.MNC_RM_NAM = "";
        p.MNC_BD_NO = "";
        p.MNC_FN_TYP_CD = "";
        p.MNC_COM_CD = "";
        p.MNC_REM = "";
        p.MNC_LB_STS = "";
        p.MNC_CAL_NO = "";
        p.MNC_EMPR_CD = "";
        p.MNC_EMPC_CD = "";
        p.MNC_ORD_DOT = "";
        p.MNC_CFM_DOT = "";
        p.MNC_DOC_YR = "";
        p.MNC_DOC_NO = "";
        p.MNC_DOC_DAT = "";
        p.MNC_DOC_CD = "";
        p.MNC_SPC_SEND_DAT = "";
        p.MNC_SPC_SEND_TM = "";
        p.MNC_SPC_TYP = "";
        p.MNC_REMARK = "";
        p.MNC_STAMP_DAT = "";
        p.MNC_STAMP_TIM = "";
        p.MNC_CANCEL_STS = "";
        p.MNC_PAC_CD = "";
        p.MNC_PAC_TYP = "";
        p.MNC_DANGER_FLG = "";
        p.MNC_DIET_FLG = "";
        p.MNC_MED_FLG = "";
        p.MNC_LAB_FN_TYP_CD = "";
        p.MNC_IP_ADD1 = "";
        p.MNC_IP_ADD2 = "";
        p.MNC_IP_ADD3 = "";
        p.MNC_IP_ADD4 = "";
        p.MNC_PATNAME = "";
        p.MNC_LOAD_STS = "";
        p.MNC_IP_REC = "";

        return p;
    }
}