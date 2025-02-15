using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class Visit:Persistent
    {
        public String VN = "", HN = "", PatientName = "", VisitDate = "", vn = "", vnno = "", vnseq = "", vnsum = "", symptom = "", VisitTime = "", LabDate = "", LabTime = "", DoctorId = "", DoctorName = "", LabReqNo = "", Age = "", preno = "";
        public String PaidName = "", ID="",weight="", high="", temp="", ratios="", breath="", bp1l="", bp1r = "", bp2l="", bp2r="", cc="", ccin="", ccex="";
        public String PaidCode = "", DeptCode="", VisitType="", VisitNote = "", DoctorOwn="", abc="", hc="",MNC_STS="", hrate="", rrate="", MNC_DOT_CD="", CompName="";
        public String MNC_DIA_CD = "", MNC_DOT_MEMO="", MNC_DIA_MEMO="", MNC_LAB_FLG="", MNC_PHA_FLG="", MNC_XRA_FLG="", MNC_PHY_FLG="", MNC_CAR_MEMO="", MNC_HN_YR="", compcode="", insurcode="";
        public String status_doe = "", status_supra = "", certi_id="";
    }
}
