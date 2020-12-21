using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LabM01DB
    {
        public LabM01 labM01;
        ConnectDB conn;
        public LabM01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labM01 = new LabM01();
            labM01.MNC_LB_CD = "MNC_LB_CD";
            labM01.MNC_LB_DSC = "MNC_LB_DSC";
            labM01.MNC_LB_TYP_CD = "MNC_LB_TYP_CD";
            labM01.MNC_LB_GRP_CD = "MNC_LB_GRP_CD";
            labM01.MNC_LB_DIS_STS = "MNC_LB_DIS_STS";
            labM01.MNC_SCH_ACT = "MNC_SCH_ACT";
            labM01.MNC_STAMP_DAT = "MNC_DOC_CD";
            labM01.MNC_STAMP_TIM = "MNC_DOC_CD";
            labM01.MNC_SPC_CD = "MNC_DOC_CD";
            labM01.MNC_LB_STS = "MNC_DOC_CD";
            labM01.MNC_USR_ADD = "MNC_DOC_CD";
            labM01.MNC_USR_UPD = "MNC_DOC_CD";
            labM01.MNC_LB_CTL_CD = "MNC_DOC_CD";
            labM01.MNC_LB_OLD_CD = "MNC_DOC_CD";
            labM01.MNC_DEC_CD = "MNC_DOC_CD";
            labM01.MNC_DEC_NO = "MNC_DOC_CD";
            labM01.MNC_LB_PRI = "MNC_DOC_CD";
            labM01.mnc_res_flg = "MNC_DOC_CD";
            labM01.MNC_HL7_CODE = "MNC_DOC_CD"

        }
    }
}
