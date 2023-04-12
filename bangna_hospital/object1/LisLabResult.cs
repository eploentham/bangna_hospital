using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class LisLabResult:Persistent
    {
        public String lab_result_id = "";
        public String lab_result_lon = "";
        public String lab_result_msg_type = "";
        public String lab_result_data = "";
        public String lab_result_datatype_id = "";
        public String lab_result_note = "";
        public String lab_result_datetime = "";
        public String lab_result_receive = "";
        public String lab_result_receive_datetime = "";
        public String lab_result_receive_data = "";
        public String hn = "";
        public String req_date = "";
        public String req_no = "";

        String split = "|", splitVal="^";
        public LisLabResult()
        {

        }
        public List<LinkLabOBX> genResult(DataRow  dtres)
        {
            String re = "", txt = "";
            //String OBX_setId = "", OBX_ValueTyp = "", OBX_ObsId = "", OBX_IdCode = "", OBX_ObsSubId = "", OBX_Obsvalue = "", OBX_Units = "", OBX_RefRanges = "", OBX_AbnorFlag = "", OBX_Probability = "", OBX_NatureofAbnor = "", OBX_ObsResultStatus = "";
            //String OBX_DateObsNorvalue = "", OBX_UserDefChk = "", OBX_DateTimeObs = "", OBX_ProdID = "", OBX_RespObs = "", OBX_IdNum = "", OBX_ObsMethod = "";

            
            String MSH_encoding = "", MSH_SendApp = "", MSH_SendFaci = "", MSH_RecApp = "", MSH_RecFaci = "", MSH_creaDT = "", MSH_secu = "", MSH_messTri = "", MSH_messCtlId = "", MSH_procId = "", MSH_verId = "";

            String PID_setId = "", PID_PID = "", PID_HN = "", PID_CID = "", PID_PttName = "", PID_PttLastName = "", PID_PttFirstName = "", PID_PttMidName = "", PID_PttSuffix = "", PID_PttPrefix = "", PID_PttDegree = "", PID_PttnameTypeCode = "", PID_NameRepreCode = "";
            String PID_MomName = "", PID_DOB = "", PID_Sex = "", PID_PttAlias = "", PID_Race = "", PID_PttAddr = "", PID_Street = "", PID_Addr2 = "", PID_City = "", PID_State = "", PID_ZipCode = "", PID_Country = "", PID_Phone = "";

            String PV1_setId = "", PV1_pttClass = "", PV1_pttLocation = "", PV1_admitTyp = "", PV1_An = "", PV1_prioLoca = "", PV1_dtrCodeName = "", PV1_dtrRefer = "", PV1_dtrConsu = "", PV1_HCode = "", PV1_TempLoca = "", PV1_TestIndi = "", PV1_readmIndi = "";
            String PV1_readmit = "", PV1_ambuStatus = "", PV1_vipIndi = "", PV1_dtrAdmit = "", PV1_IdNum = "", PV1_FamiName = "", PV1_pttTyp = "", PV1_vsNumber = "", PV1_FinaClass = "", PV1_CharPriceIndi = "", PV1_courCode = "", PV1_CredRate = "", PV1_ContCode = "";
            String PV1_ContEffDate = "", PV1_ContAmt = "", PV1_ContPeriod = "", PV1_IntersCode = "", PV1_TracfBadDebtCode = "",PV1_TranfBadDebtDate="", PV1_BadDebtAgencCode = "", PV1_BadDebtAmt = "", PV1_BadDebtRecoAmt = "", PV1_DelAccIndi = "", PV1_DelAccDate = "";
            String PV1_DiscDepo = "", PV1_DiscLoca = "", PV1_DietTyp = "", PV1_ServFaci = "", PV1_BedStatus = "", PV1_AccStatus = "", PV1_PendLoca = "", PV1_PriorLoca = "", PV1_AdtDateTime = "", PV1_DiscDateTime = "", PV1_CurrPttBal = "";
            String PV1_TotalChar = "", PV1_TotalAdj = "", PV1_TotalPay = "", PV1_AltVsId = "", PV1_VsIndi = "", PV1_OthHealtProv = "";

            String ORC_OrdCtl = "", ORC_PlcOrdNum = "", ORC_FillOrdNum = "", ORC_PlcGrpNum = "", ORC_OrdStatus = "", ORC_RespFlag = "", ORC_QtyTim = "", ORC_Paren = "", ORC_TransDateTime = "", ORC_EnterBy = "", ORC_VeriBy = "";
            String ORC_OrdProv = "", ORC_ProvCode = "", ORC_ProvName = "", ORC_EnterLoca = "", ORC_CallBPhone = "", ORC_OrdEffDateTime = "", ORC_OrdCtlReas = "", ORC_EnterOrg = "", ORC_EnterDevi = "";
            String ORC_ActBy = "", ORC_AdvCode = "", ORC_OrdFaciName = "", ORC_OrdFaciAddr = "", ORC_OrdFaciPhone = "";

            //String OBR_setId = "", OBR_PlaOrdNum = "", OBR_FillOrdNum = "", OBR_UnivServID = "", OBR_ID = "", OBR_Text = "", OBR_NameCode = "", OBR_AltId = "", OBR_AltText = "", OBR_nameAlt = "", OBR_Priority = "", OBR_ReqDateTime = "";
            //String OBR_ObsDateTime = "", OBR_ObsEndDateTime = "", OBR_ColleVol = "", OBR_ColleIden = "", OBR_SpeciActCode = "", OBR_DangerCode = "", OBR_ReleClinInfo = "", OBR_SpeciRece = "", OBR_DateTime = "", OBR_SpeciSour = "";
            //String OBR_OrdProv = "", OBR_OrdProvId = "", OBR_ProvLastName = "", OBR_OrdCallBPhone = "", OBR_PlacFiled1 = "", OBR_PlacField2 = "", OBR_FillerField1 = "", OBR_FillerField2 = "", OBR_ResRpt = "", OBR_CharPrac = "", OBR_DiagServ = "";
            //String OBR_ResStatus = "", OBR_Parent = "", OBR_QtyTim = "", OBR_ResCopTO = "", OBR_Parent1 = "", OBR_TranfMode = "", OBR_ReasStudy = "", OBR_PrincRes = "", OBR_AssisRes = "", OBR_Techno = "", OBR_Trascrip = "", OBR_scheDateTime = "";
            //String OBR_NumSamp = "", OBR_TransLog = "", OBR_ColleComm = "",OBR_TransArrR, OBR_TransArr = "", OBR_EscortReq = "", OBR_PlannPattTrans = "", OBR_ProcCode = "", OBR_ProcCodeModi = "";

            //Message MSH
            //Message PID
            //Message PV1
            //Message ORC
            //Message OBR
            List<LinkLabOBR> lOBR;
            List<LinkLabOBX> lOBX;
            lOBX = new List<LinkLabOBX>();
            
            //OBX_setId = "";        // Sequence number
            String mes = "";
            int linenum = 0;
            mes = dtres["lab_result_data"].ToString();
            if (mes.Length > 0)
            {
                String labcode = "", reqno = "",hn="", reqdate="";
                lOBR = new List<LinkLabOBR>();
                    
                String[] messplit = mes.Split(Environment.NewLine.ToCharArray());
                foreach(String mesline in messplit)
                {
                    //MessageMSH
                    linenum++;
                    if (linenum == 1)
                    {
                        String[] mesMSH = mesline.Split(split.ToCharArray());
                        if (mesMSH.Length > 0)
                        {
                            if (mesMSH[0].Equals("MSH"))
                            {
                                MSH_encoding = mesMSH[1];
                                MSH_SendApp = mesMSH[2];
                                MSH_SendFaci = mesMSH[3];
                                MSH_RecApp = mesMSH[4];
                                MSH_RecFaci = mesMSH[5];
                                MSH_creaDT = mesMSH[6];
                                MSH_secu = mesMSH[7];
                                MSH_messTri = mesMSH[8];
                                MSH_messCtlId = mesMSH[9];
                                MSH_procId = mesMSH[10];
                                MSH_verId = mesMSH[11];
                            }
                        }
                        continue;
                    }
                    //MessagePID
                    if (linenum == 2)
                    {
                        String[] mesPID = mesline.Split(split.ToCharArray());
                        if (mesPID.Length > 0)
                        {
                            //re = setId + split + PID + split + HN + split + CID + split + PttName + split + DOB + split + Sex + split + PttAlias + split + Race + split + PttAddr
                            //+ split + Country + split + Phone;
                            if (mesPID[0].Equals("PID"))
                            {
                                PID_setId = mesPID[1];
                                PID_PID = mesPID[2];
                                PID_HN = mesPID[3];
                                PID_CID = mesPID[4];
                                PID_PttName = mesPID[5];
                                PID_DOB = mesPID[6];
                                PID_Sex = mesPID[7];
                                PID_PttAlias = mesPID[8];
                                PID_Race = mesPID[9];
                                PID_PttAddr = mesPID[10];
                                PID_Country = mesPID[11];
                                PID_Phone = mesPID[12];
                            }
                        }
                        continue;
                    }
                    //MessagePV1
                    if (linenum == 3)
                    {
                        String[] mesPV1 = mesline.Split(split.ToCharArray());
                        if (mesPV1.Length > 0)
                        {
                            //            re = setId + split + pttClass + split + pttLocation + split + admitTyp + split + An + split + prioLoca + split + dtrCodeName + split + dtrRefer + split + dtrConsu
                            //+ split + HCode + split + TempLoca + split + TestIndi + split + readmIndi + split + readmit + split + ambuStatus + split + vipIndi + split + dtrAdmit
                            //+ split + IdNum + split + FamiName + split + pttTyp + split + vsNumber + split + FinaClass + split + CharPriceIndi + split + courCode + split + CredRate
                            //    + split + ContCode + split + ContEffDate + split + ContAmt + split + ContPeriod + split + IntersCode + split + TracfBadDebtCode + split + BadDebtAgencCode + split + BadDebtAmt
                            //    + split + BadDebtRecoAmt + split + DelAccIndi + split + DelAccDate + split + DiscDepo + split + DiscLoca + split + DietTyp + split + ServFaci + split + BedStatus
                            //    + split + AccStatus + split + PendLoca + split + PriorLoca + split + AdtDateTime + split + DiscDateTime + split + CurrPttBal + split + TotalChar + split + TotalAdj
                            //    + split + TotalPay + split + AltVsId + split + VsIndi + split + OthHealtProv;
                            if (mesPV1[0].Equals("PV1"))
                            {
                                PV1_setId = mesPV1[1];
                                PV1_pttClass = mesPV1[2];
                                PV1_pttLocation = mesPV1[3];
                                PV1_admitTyp = mesPV1[4];
                                PV1_An = mesPV1[5];
                                PV1_prioLoca = mesPV1[6];
                                PV1_dtrCodeName = mesPV1[7];
                                    
                                if (mesPV1.Length > 8)
                                {
                                    PV1_dtrRefer = mesPV1[8];
                                    PV1_dtrConsu = mesPV1[9];
                                    PV1_HCode = mesPV1[10];
                                    PV1_TempLoca = mesPV1[11];

                                    PV1_TestIndi = mesPV1[12];
                                    PV1_readmIndi = mesPV1[13];
                                    PV1_readmit = mesPV1[14];
                                    PV1_ambuStatus = mesPV1[15];
                                    PV1_vipIndi = mesPV1[16];
                                    PV1_dtrAdmit = mesPV1[17];
                                    //PV1_IdNum = mesPV1[18];
                                    //PV1_FamiName = mesPV1[19];
                                    PV1_pttTyp = mesPV1[18];
                                    PV1_vsNumber = mesPV1[19];

                                    PV1_FinaClass = mesPV1[20];
                                    PV1_CharPriceIndi = mesPV1[21];
                                    PV1_courCode = mesPV1[22];
                                    PV1_CredRate = mesPV1[23];
                                    PV1_ContCode = mesPV1[24];
                                    PV1_ContEffDate = mesPV1[25];
                                    PV1_ContAmt = mesPV1[26];
                                    PV1_ContPeriod = mesPV1[27];
                                    PV1_IntersCode = mesPV1[28];
                                    PV1_TracfBadDebtCode = mesPV1[29];

                                    PV1_TranfBadDebtDate = mesPV1[30];

                                    PV1_BadDebtAgencCode = mesPV1[31];
                                    PV1_BadDebtAmt = mesPV1[32];
                                    PV1_BadDebtRecoAmt = mesPV1[33];
                                    PV1_DelAccIndi = mesPV1[34];
                                    PV1_DelAccDate = mesPV1[35];
                                    PV1_DiscDepo = mesPV1[36];
                                    PV1_DiscLoca = mesPV1[37];
                                    PV1_DietTyp = mesPV1[38];
                                    PV1_ServFaci = mesPV1[39];
                                    PV1_BedStatus = mesPV1[40];

                                    PV1_AccStatus = mesPV1[41];
                                    PV1_PendLoca = mesPV1[42];
                                    PV1_PriorLoca = mesPV1[43];
                                    PV1_AdtDateTime = mesPV1[44];
                                    PV1_DiscDateTime = mesPV1[45];
                                    PV1_CurrPttBal = mesPV1[46];
                                    PV1_TotalChar = mesPV1[47];
                                    PV1_TotalAdj = mesPV1[48];
                                    PV1_TotalPay = mesPV1[49];
                                    PV1_AltVsId = mesPV1[50];

                                    PV1_VsIndi = mesPV1[51];
                                    PV1_OthHealtProv = mesPV1[52];
                                }
                            }
                        }
                        continue;
                    }
                    //MessageORC
                    if (linenum == 4)
                    {
                        String[] mesORC = mesline.Split(split.ToCharArray());
                        if (mesORC.Length > 0)
                        {
                            //            re = OrdCtl + split + PlcOrdNum + split + FillOrdNum + split + PlcGrpNum + split + OrdStatus + split + RespFlag + split + QtyTim + split + Paren
                            //+ split + TransDateTime + split + EnterBy + split + VeriBy + split + OrdProv + split + EnterLoca + split + CallBPhone
                            //+ split + OrdEffDateTime + split + OrdCtlReas + split + EnterOrg + split + EnterDevi + split + ActBy + split + AdvCode + split + OrdFaciName
                            //+ split + OrdFaciAddr + split + OrdFaciPhone;
                            if (mesORC[0].Equals("ORC"))
                            {
                                ORC_OrdCtl = mesORC[1];
                                ORC_PlcOrdNum = mesORC[2];
                                ORC_FillOrdNum = mesORC[3];
                                ORC_PlcGrpNum = mesORC[4];
                                ORC_OrdStatus = mesORC[5];
                                ORC_RespFlag = mesORC[6];
                                ORC_QtyTim = mesORC[7];
                                ORC_Paren = mesORC[8];
                                ORC_TransDateTime = mesORC[9];
                                ORC_EnterBy = mesORC[10];

                                ORC_VeriBy = mesORC[11];
                                ORC_OrdProv = mesORC[12];
                                //ORC_ProvCode = mesORC[13];
                                //ORC_ProvName = mesORC[12];
                                ORC_EnterLoca = mesORC[13];
                                ORC_CallBPhone = mesORC[14];
                                ORC_OrdEffDateTime = mesORC[15];
                                ORC_OrdCtlReas = mesORC[16];
                                ORC_EnterOrg = mesORC[17];
                                ORC_EnterDevi = mesORC[18];

                                ORC_ActBy = mesORC[19];
                                ORC_AdvCode = mesORC[20];
                                if (mesORC.Length > 21)
                                {
                                    ORC_OrdFaciName = mesORC[21];
                                    ORC_OrdFaciAddr = mesORC[22];
                                    ORC_OrdFaciPhone = mesORC[23];
                                }
                            }
                        }
                        continue;
                    }
                    if (linenum >= 5)
                    {
                        String[] mesOBROBX = mesline.Split(split.ToCharArray());
                        if (mesOBROBX.Length > 0)
                        {
                                
                            //            re = setId + split + PlaOrdNum + split + FillOrdNum + split + UnivServID + split + ID + split + Priority + split + ReqDateTime + split + ObsDateTime
                            //+ split + ObsEndDateTime + split + ColleVol + split + ColleIden + split + SpeciActCode + split + DangerCode + split + ReleClinInfo + split + SpeciRece
                            //+ split + SpeciSour + split + OrdProv + split + OrdCallBPhone + split + PlacFiled1 + split + PlacField2 + split + FillerField1 + split + FillerField2
                            //+ split + ResRpt + split + CharPrac + split + DiagServ + split + ResStatus + split + Parent + split + QtyTim + split + ResCopTO + split + Parent1
                            //+ split + TranfMode + split + ReasStudy + split + PrincRes + split + AssisRes + split + Techno + split + Trascrip + split + scheDateTime
                            //+ split + NumSamp + split + TransLog + split + ColleComm + split + TransArr + split + EscortReq + split + PlannPattTrans + split + ProcCode + split + ProcCodeModi;
                            if (mesOBROBX[0].Equals("OBR"))
                            {
                                LinkLabOBR obr = new LinkLabOBR();
                                    
                                obr.OBR_setId = mesOBROBX[1];
                                obr.OBR_PlaOrdNum = mesOBROBX[2];
                                obr.OBR_FillOrdNum = mesOBROBX[3];
                                obr.OBR_UnivServID = mesOBROBX[4];
                                    
                                reqno = obr.OBR_PlaOrdNum;
                                String[] chk1 = obr.OBR_UnivServID.Split(splitVal.ToCharArray());
                                if (chk1.Length > 0)
                                {
                                    labcode = chk1[0];
                                }

                                //obr.OBR_ID = mesOBROBX[1];
                                obr.OBR_Priority = mesOBROBX[5];
                                obr.OBR_ReqDateTime = mesOBROBX[6];
                                reqdate = obr.OBR_ReqDateTime;
                                obr.OBR_ObsDateTime = mesOBROBX[7];
                                obr.OBR_ObsEndDateTime = mesOBROBX[8];
                                obr.OBR_ColleVol = mesOBROBX[9];

                                obr.OBR_ColleIden = mesOBROBX[10];
                                obr.OBR_SpeciActCode = mesOBROBX[11];
                                obr.OBR_DangerCode = mesOBROBX[12];
                                obr.OBR_ReleClinInfo = mesOBROBX[13];
                                obr.OBR_SpeciRece = mesOBROBX[14];
                                obr.OBR_SpeciSour = mesOBROBX[15];
                                obr.OBR_OrdProv = mesOBROBX[16];
                                obr.OBR_OrdCallBPhone = mesOBROBX[17];
                                if (mesOBROBX.Length > 18)
                                {
                                    obr.OBR_PlacFiled1 = mesOBROBX[18];
                                    obr.OBR_PlacField2 = mesOBROBX[19];

                                    obr.OBR_FillerField1 = mesOBROBX[20];
                                    obr.OBR_FillerField2 = mesOBROBX[21];
                                    obr.OBR_ResRpt = mesOBROBX[22];
                                    obr.OBR_CharPrac = mesOBROBX[23];
                                    obr.OBR_DiagServ = mesOBROBX[24];
                                    obr.OBR_ResStatus = mesOBROBX[25];
                                    obr.OBR_Parent = mesOBROBX[26];
                                    obr.OBR_QtyTim = mesOBROBX[27];
                                    obr.OBR_ResCopTO = mesOBROBX[28];
                                    obr.OBR_Parent1 = mesOBROBX[29];

                                    obr.OBR_TranfMode = mesOBROBX[30];
                                    obr.OBR_ReasStudy = mesOBROBX[31];
                                    obr.OBR_PrincRes = mesOBROBX[32];
                                    obr.OBR_AssisRes = mesOBROBX[33];
                                    obr.OBR_Techno = mesOBROBX[34];
                                    obr.OBR_Trascrip = mesOBROBX[35];
                                    obr.OBR_scheDateTime = mesOBROBX[36];
                                    obr.OBR_NumSamp = mesOBROBX[37];
                                    obr.OBR_TransLog = mesOBROBX[38];
                                    obr.OBR_ColleComm = mesOBROBX[39];

                                    obr.OBR_TransArrR = mesOBROBX[40];
                                    obr.OBR_TransArr = mesOBROBX[41];
                                    obr.OBR_EscortReq = mesOBROBX[42];
                                    obr.OBR_PlannPattTrans = mesOBROBX[43];
                                    obr.OBR_ProcCode = mesOBROBX[44];
                                    obr.OBR_ProcCodeModi = mesOBROBX[45];
                                }
                                    
                                lOBR.Add(obr);
                            }
                            if (mesOBROBX[0].Equals("OBX"))
                            {
                                LinkLabOBX obx = new LinkLabOBX();
                                obx.OBX_setId = mesOBROBX[1];
                                obx.OBX_ValueTyp = mesOBROBX[2];
                                //obx.OBX_ObsId = mesOBROBX[3];     //แก้ใหม่ format เปลี่ยน
                                obx.reqno = mesOBROBX[3];       //แก้ใหม่ format เปลี่ยน
                                obx.lab_code = mesOBROBX[4];
                                obx.OBX_ObsId = mesOBROBX[5];
                                String[] chk = obx.OBX_ObsId.Split(splitVal.ToCharArray());
                                if (chk.Length > 0)
                                {
                                    //obx.lab_sub_code = chk[0];
                                    obx.running = chk[0];//   แก้เพราะ เปิดดู source code และ ข้อมูลใน table lab_t05.mnc_lb_res_cd เป็น running
                                    obx.lab_sub_code = chk[1];//   แก้เพราะ เปิดดู source code และ ข้อมูลใน table lab_t05.mnc_lb_res_cd เป็น running
                                }
                                obx.OBX_ObsSubId = "";
                                obx.OBX_Obsvalue = mesOBROBX[7];
                                
                                obx.OBX_Units = mesOBROBX[8];
                                obx.OBX_RefRanges = mesOBROBX[7];
                                obx.OBX_AbnorFlag = mesOBROBX[8];
                                obx.OBX_Probability = mesOBROBX[9];
                                obx.OBX_NatureofAbnor = mesOBROBX[10];

                                obx.OBX_ObsResultStatus = mesOBROBX[11];
                                obx.OBX_DateObsNorvalue = mesOBROBX[12];
                                obx.OBX_UserDefChk = mesOBROBX[13];
                                obx.OBX_DateTimeObs = mesOBROBX[16];
                                obx.OBX_ProdID = mesOBROBX[15];
                                obx.OBX_RespObs = mesOBROBX[16];
                                obx.OBX_IdNum = mesOBROBX[18];      //ส่งมาเป็น approce code, reporter code
                                obx.OBX_ObsMethod = mesOBROBX[19];
                                //obx.lab_code = labcode;
                                //obx.reqno = reqno;
                                obx.hn = PID_HN;
                                obx.reqdate = reqdate;
                                //obx.OBX_IdNum
                                lOBX.Add(obx);
                            }
                        }
                        continue;
                    }
                }
                if (lOBX.Count > 0)
                {
                    //foreach(LinkLabOBX obx in lOBX)
                    //{

                    //}
                }
            }
            return lOBX;
        }
    }
}
