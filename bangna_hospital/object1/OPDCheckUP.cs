using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bangna_hospital.object1
{
    public class OPDCheckUP : Persistent
    {
        public String Id = "", Name = "", CheckDate = "", HN = "", VN = "", Sex = "", Age = "", Addr1 = "", Addr2 = "", Phone = "", ABOGroup = "", RhGroup = "", Tempu = "", Weight = "", Height = "";
        public String BloodPressure = "", Pulse = "", Breath = "",Active, Result="", Suggest="", PreNo="";
        public String CBCWBCCntN = "", CBCWBCCntR = "", CBCWBCCntV = "";
        public String CBCRBCCntN = "", CBCRBCCntR = "", CBCRBCCntV = "";
        public String CBCHbN = "", CBCHbR = "", CBCHbV = "";
        public String CBCHctN = "", CBCHctR = "", CBCHctV = "";
        public String CBCMcvN = "", CBCMcvR = "", CBCMcvV = "";
        public String CBCMchN = "", CBCMchR = "", CBCMchV = "";
        public String CBCMchcN = "", CBCMchcR = "", CBCMchcV = "";
        public String CBCPmnN = "", CBCPmnR = "", CBCPmnV = "";
        public String CBCLyN = "", CBCLyR = "", CBCLyV = "";
        public String CBCMonoN = "", CBCMonoR = "", CBCMonoV = "";
        public String CBCEosN = "", CBCEosR = "", CBCEosV = "";
        public String CBCBasN = "", CBCBasR = "", CBCBasV = "";
        public String CBCAtrLyN = "", CBCAtrLyR = "", CBCAtrLyV = "";
        public String CBCPlaCntN = "", CBCPlaCntR = "", CBCPlaCntV = "";
        public String CBCPLaSmeN = "", CBCPlaSmeR = "", CBCPlaSmeV = "";
        public String CBCRBCMorN = "", CBCRBCMorR = "", CBCRBCMorV = "";
        public String CBCOtherCellN = "", CBCOtherCellR = "", CBCOtherCellV = "";
        public String CBCResult = "", CBCSuggest = "";
        public String HistStatus = "", HistOther = "";
        public String AllergicStatus = "", AllergicOther = "";
        public String ORStatus = "", OROther = "";
        public String FBSN = "", FBSR = "", FBSV = "", FBSResult="", FBSSuggest="";
        public String CholN = "", CholR = "", CholV = "", CholResult = "", CholSuggest = "";
        public String CreatiN = "", CreatiR = "", CreatiV = "", CreatiResult = "", CreatiSuggest = "";
        public String SgotN = "", SgotR = "", SgotV = "", SgotResult = "", SgotSuggest = "", SgptN = "", SgptR = "", SgptV = "";
    }
}
