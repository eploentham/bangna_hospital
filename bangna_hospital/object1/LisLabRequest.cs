using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class LisLabRequest:Persistent
    {
        public String lab_request_id = "";
        public String lab_request_lon = "";
        public String lab_request_msg_type = "";
        public String lab_request_data = "";
        public String lab_request_datetime = "";
        public String lab_request_receive = "";
        public String lab_request_receive_datetime = "";
        public String lab_request_receive_data = "";
        public String hn = "";
        public String req_date = "";
        public String req_no = "";
        String split = "|";
        public LisLabRequest()
        {

        }
        public String genReq(String createdateyyyymmsshhMMss, String hn, String pttprefix, String pttfirstname, String pttlastname, String pttmidname, string dobyyyymmdd, String sexMFU
            , String opdtype, String dept, string an, String vsid, String pttType, String AdtDateTimeyyyymmsddhhmmss, String DiscDateTimeyyyymmddhhMMss
            , String nwrpca,String reqno, String appectreqdatetime, String dtrcode, String deptcode, DataTable dReq, String userreceivedatetime)
        {
            String MSH = "", PID = "", PV1="", req="", OCR="", OBR="", err="";
            int i = 0;
            try
            {
                err = "00";
                MSH = "MSH" + split + genMSH(createdateyyyymmsshhMMss) + Environment.NewLine;
                err = "01";
                PID = "PID" + split + genPID(hn, pttprefix, pttfirstname, pttlastname, pttmidname, dobyyyymmdd, sexMFU) + Environment.NewLine;
                err = "02";
                PV1 = "PV1" + split + genPV1(opdtype, dept, an, vsid, pttType, AdtDateTimeyyyymmsddhhmmss, DiscDateTimeyyyymmddhhMMss) + Environment.NewLine;
                err = "03";
                OCR = "ORC" + split + genORC(nwrpca, reqno, appectreqdatetime, userreceivedatetime) + Environment.NewLine;
                err = "04";
                foreach (DataRow drow in dReq.Rows)
                {
                    i++;
                    //OBR += "OBR" + split+ genOBR(i.ToString(),drow["MNC_LB_CD"].ToString(),"S", drow["MNC_REQ_DAT"].ToString(), dtrcode, deptcode) +Environment.NewLine;
                    String reqtime = "";
                    reqtime = "0000" + drow["MNC_REQ_TIM"].ToString();
                    reqtime = reqtime.Substring(reqtime.Length - 4) + "00";
                    OBR += "OBR" + split + genOBR(i.ToString(), drow["MNC_LB_CD"].ToString(), "S", drow["MNC_REQ_DAT"].ToString() + reqtime, dtrcode, deptcode) + Environment.NewLine;
                }
                req = MSH + PID + PV1 + OCR + OBR;
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmLisLink setLinkLIS  " + err + " " + ex.Message);
            }
            
            return req;
        }
        public String genMSH(String createdateyyyymmsshhMMss)
        {
            String re = "", txt="";
            String encoding = "", SendApp = "", SendFaci = "", RecApp = "", RecFaci = "", creaDT = "", secu = "", messTri = "" ,messCtlId="", procId = "", verId = "";
            encoding = "^~\\&";             //Encoding characters       ^~\&
            SendApp = "HIS";                //Sending application       HIS
            SendFaci = "MainHIS";           //Sending facility      BMS-HOSxP
            RecApp = "LIS";                 //Receiving application LIS
            RecFaci = "";                   //Receiving facility    Company Name
            creaDT = createdateyyyymmsshhMMss;      //Date/Time of message      R
            secu = "";              //Security      **Not Used
            messTri = "ORM^001";//Message type/Trigger Event            R
                        messCtlId = "";     //Message control ID        R
            procId = "P";       //Version ID        R
            verId = "2.x";      //Version ID        R
            re = encoding + split + SendApp + split + SendFaci + split + RecApp + split + RecFaci + split + creaDT + split + secu + split + messTri + split + messCtlId + split + procId + split + verId;
            return re;
        }
        public String genPID(String hn, String pttprefix, String pttfirstname, String pttlastname, String pttmidname, String dobyyyymmdd, String sexMFU)
        {
            String re = "", txt = "",split1="^";
            String setId = "", PID = "", HN = "", CID = "", PttName="", PttLastName = "", PttFirstName = "", PttMidName = "", PttSuffix = "", PttPrefix = "", PttDegree = "", PttnameTypeCode = "", NameRepreCode = "";
            String MomName = "", DOB = "", Sex = "", PttAlias = "", Race = "", PttAddr = "", Street = "", Addr2 = "", City = "", State = "", ZipCode = "", Country = "", Phone = "";

            setId = "1";    //Set to ‘1'
            PID = "";       //**Not Used  **Not Used
            HN = hn;        //Patient identifier list HN
            CID = "";       //Alternate patient ID – PID    CID

            //Patient name -> Patient Last Name + Patient First Name + Patient Middle Initial + Patient Name Suffix(**Not Used) 
            //PttName = pttmidname + " " + pttlastname + split + pttfirstname + split +" "+ PttSuffix+" "+ pttprefix+" "+ PttDegree+" "+ PttnameTypeCode+" "+ NameRepreCode+" "+ MomName;
            PttName =pttmidname.Length>0 ? pttmidname + " " + pttlastname + split1 + pttfirstname + split1 + pttprefix : pttlastname + split1 + pttfirstname + split1 + pttprefix;
            DOB = dobyyyymmdd;
            Sex = sexMFU;           //Sex Supported M/F/U
            PttAlias = "";          //**Not Used
            Race = "2028-9";        //Set to ‘2028-9’
            Country = "";           //**Not Used
            PttAddr = Street+" "+ Addr2+" "+ City+" "+ State+" "+ ZipCode;

            re = setId + split + PID + split + HN + split + CID + split + PttName + split + DOB + split + Sex + split + PttAlias + split + Race + split + PttAddr + split + Country + split + Phone;
            return re;
        }
        public String genPV1(String opdtype, String dept, string an, String vsid, String pttType, String AdtDateTimeyyyymmsddhhmmss, String DiscDateTimeyyyymmddhhMMss)
        {
            String re = "", txt = "";
            String setId = "", pttClass="", pttLocation="", admitTyp="", An="", prioLoca="", dtrCodeName="", dtrRefer="", dtrConsu="", HCode="", TempLoca="", TestIndi="", readmIndi="";
            String readmit="", ambuStatus = "", vipIndi = "", dtrAdmit = "", IdNum = "", FamiName = "", pttTyp = "", vsNumber = "", FinaClass = "", CharPriceIndi = "", courCode = "", CredRate = "", ContCode = "";
            String ContEffDate = "", ContAmt = "", ContPeriod = "", IntersCode = "", TracfBadDebtCode = "", BadDebtAgencCode = "", BadDebtAmt = "", BadDebtRecoAmt = "", DelAccIndi = "", DelAccDate = "";
            String DiscDepo = "", DiscLoca = "", DietTyp = "", ServFaci = "", BedStatus = "", AccStatus = "", PendLoca = "", PriorLoca = "", AdtDateTime = "", DiscDateTime = "", CurrPttBal = "";
            String TotalChar = "", TotalAdj = "", TotalPay = "", AltVsId = "", VsIndi = "", OthHealtProv = "";

            setId = "1";        // Always 1
            pttClass = opdtype;     //I – Inpatient, O – Outpatient
            pttLocation = dept;       //Assigned Patient Location
            admitTyp = "";          //Admission Type **Not Use
            An = an;                //Preadmit Number IPD – AN
            prioLoca = "";          //PriorPatientLocation **Not Used
            dtrCodeName = "";           //Attending Doctor   Visit Doctor Code  Visit Doctor Name
            dtrRefer = "";          // Referring Doctor  **Not Used
            dtrConsu = "";          //Consulting Doctor  **Not Used
            HCode = "";             //Hospital Service  Hospital code
            TempLoca = "";          //Temporary Location
            TestIndi = "";          //Preadmit Test Indicator
            readmIndi = "";         //Readmission Indicator
            IdNum = "01";
            pttTyp = pttType;
            vsNumber = vsid;
            AdtDateTime = AdtDateTimeyyyymmsddhhmmss;
            DiscDateTime = DiscDateTimeyyyymmddhhMMss;

            re = setId + split + pttClass + split + pttLocation + split + admitTyp + split + An + split + prioLoca + split + dtrCodeName + split + dtrRefer + split + dtrConsu
                + split + HCode + split + TempLoca + split + TestIndi + split + readmIndi + split + readmit + split + ambuStatus + split + vipIndi + split + dtrAdmit
                + split + IdNum + split + FamiName + split + pttTyp + split + vsNumber + split + FinaClass + split + CharPriceIndi + split + courCode + split + CredRate
                 + split + ContCode + split + ContEffDate + split + ContAmt + split + ContPeriod + split + IntersCode + split + TracfBadDebtCode + split + BadDebtAgencCode + split + BadDebtAmt
                 + split + BadDebtRecoAmt + split + DelAccIndi + split + DelAccDate + split + DiscDepo + split + DiscLoca + split + DietTyp + split + ServFaci + split + BedStatus
                 + split + AccStatus + split + PendLoca + split + PriorLoca + split + AdtDateTime + split + DiscDateTime + split + CurrPttBal + split + TotalChar + split + TotalAdj
                 + split + TotalPay + split + AltVsId + split + VsIndi + split + OthHealtProv;

            return re;
        }
        public String genORC(String nwrpcacr, String reqno, String receivedatetime, String userreceivedatetime)
        {
            String re = "", txt = "";
            String OrdCtl = "", PlcOrdNum = "", FillOrdNum = "", PlcGrpNum = "", OrdStatus = "", RespFlag = "", QtyTim = "", Paren = "", TransDateTime = "", EnterBy = "", VeriBy = "";
            String OrdProv = "", ProvCode = "", ProvName = "", EnterLoca = "", CallBPhone = "", OrdEffDateTime = "", OrdCtlReas = "", EnterOrg = "", EnterDevi = "";
            String ActBy = "", AdvCode = "", OrdFaciName = "", OrdFaciAddr = "", OrdFaciPhone = "";

            OrdCtl = nwrpcacr;
            PlcOrdNum = reqno;
            FillOrdNum = reqno;
            TransDateTime = receivedatetime.Replace("-","").Replace(" ", "").Replace(":", "")+"00";
            OrdFaciName = TransDateTime;
            OrdFaciAddr = userreceivedatetime;

            re = OrdCtl + split + PlcOrdNum + split + FillOrdNum + split + PlcGrpNum + split + OrdStatus + split + RespFlag + split + QtyTim + split + Paren
                + split + TransDateTime + split + EnterBy + split + VeriBy + split + OrdProv + split + EnterLoca + split + CallBPhone
                + split + OrdEffDateTime + split + OrdCtlReas + split + EnterOrg + split + EnterDevi + split + ActBy + split + AdvCode + split + OrdFaciName
                + split + OrdFaciAddr + split + OrdFaciPhone;

            return re;
        }
        public String genOBR(string seq,String itemCode, String priority, String reqdate, String dtrcode, String deptcode)
        {
            String re = "", txt = "";
            String setId = "", PlaOrdNum = "", FillOrdNum = "", UnivServID = "", ID = "", Text = "", NameCode = "", AltId = "", AltText = "", nameAlt = "", Priority = "", ReqDateTime = "";
            String ObsDateTime = "", ObsEndDateTime = "", ColleVol = "", ColleIden = "", SpeciActCode = "", DangerCode = "", ReleClinInfo="", SpeciRece="", DateTime="", SpeciSour="";
            String OrdProv = "", OrdProvId = "", ProvLastName = "", OrdCallBPhone = "", PlacFiled1 = "", PlacField2 = "", FillerField1 = "", FillerField2 = "", ResRpt = "", CharPrac = "", DiagServ = "";
            String ResStatus = "", Parent = "", QtyTim = "", ResCopTO = "", Parent1 = "", TranfMode = "", ReasStudy = "", PrincRes = "", AssisRes = "", Techno = "", Trascrip = "", scheDateTime = "";
            String NumSamp = "", TransLog = "", ColleComm = "", TransArr = "", EscortReq = "", PlannPattTrans = "", ProcCode = "", ProcCodeModi = "";

            setId = seq;        // Sequence number
            ID = itemCode;      //itemCode
            Priority = priority;        //S = Stat, R = Routine, A= Abrupt
            ReqDateTime = reqdate;      //Request Datetime            (yyyymmddhhnnss)
            ObsDateTime = "";        //  ให้แก้  เอารหัสหมอ
            ObsEndDateTime = "";    //   ให้แก้ dept code
            OrdProv = dtrcode;
            PlacFiled1 = deptcode;
            re = setId + split + PlaOrdNum + split + FillOrdNum + split + UnivServID + split + ID + split + Priority + split + ReqDateTime + split + ObsDateTime
                + split + ObsEndDateTime + split + ColleVol + split + ColleIden + split + SpeciActCode + split + DangerCode + split + ReleClinInfo + split + SpeciRece
                + split + SpeciSour + split + OrdProv + split + OrdCallBPhone + split + PlacFiled1 + split + PlacField2 + split + FillerField1 + split + FillerField2
                + split + ResRpt + split + CharPrac + split + DiagServ + split + ResStatus + split + Parent + split + QtyTim + split + ResCopTO + split + Parent1
                + split + TranfMode + split + ReasStudy + split + PrincRes + split + AssisRes + split + Techno + split + Trascrip + split + scheDateTime
                + split + NumSamp + split + TransLog + split + ColleComm + split + TransArr + split + EscortReq + split + PlannPattTrans + split + ProcCode + split + ProcCodeModi;
            return re;
        }
        
        public String genMSA(String ackAcceptError)
        {
            String re = "", txt = "";
            String Ack = "";
            Ack = "";

            return re;
        }
    }
    
}
