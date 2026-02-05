using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class XrayM01 : Persistent
    {
        public String MNC_XR_CD { get; set; }
        public String MNC_XR_CTL_CD { get; set; }
        public String MNC_XR_DSC { get; set; }
        public String MNC_XR_TYP_CD { get; set; }
        public String MNC_XR_GRP_CD { get; set; }
        public String MNC_XR_DIS_STS { get; set; }
        public String MNC_XR_STS { get; set; }
        public String MNC_DEC_CD { get; set; }
        public String MNC_DEC_NO { get; set; }
        public String MNC_STAMP_DAT { get; set; }
        public String MNC_STAMP_TIM { get; set; }
        public String MNC_USR_UPD { get; set; }
        public String MNC_USR_ADD { get; set; }
        public String MNC_OLD_CD { get; set; }
        public String MNC_SUP_STS { get; set; }
        public String MNC_XR_PRI { get; set; }
        public String MNC_XR_DSC_STS { get; set; }
        public String MNC_XR_AUTO { get; set; }
        public String pacs_infinitt_code { get; set; }
        public String modality_code { get; set; }
        public String ucep_code { get; set; }
        public String status_control { get; set; }
        public String control_supervisor { get; set; }
        public String control_remark { get; set; }
        public String control_paid_code { get; set; }
        public String passsupervisor { get; set; }
        public String txtremark { get; set; }

    }
}
