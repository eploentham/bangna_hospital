using bangna_hospital.object1;
using iText.StyledXmlParser.Jsoup.Select;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PharmacyT05DB
    {
        public PharmacyT05 pharT05;
        ConnectDB conn;
        public PharmacyT05DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharT05 = new PharmacyT05();
            pharT05.MNC_DOC_CD  = "MNC_DOC_CD";
            pharT05.MNC_CFR_YR  = "MNC_CFR_YR";
            pharT05.MNC_CFR_NO  = "MNC_CFR_NO";
            pharT05.MNC_CFG_DAT  = "MNC_CFG_DAT";
            pharT05.MNC_CFR_STS  = "MNC_CFR_STS";
            pharT05.MNC_DEPC_NO  = "MNC_DEPC_NO";
            pharT05.MNC_SECC_NO  = "MNC_SECC_NO";
            pharT05.MNC_REQ_YR  = "MNC_REQ_YR";
            pharT05.MNC_REQ_NO  = "MNC_REQ_NO";
            pharT05.MNC_REQ_DAT  = "MNC_REQ_DAT";
            pharT05.MNC_REQ_TIM  = "MNC_REQ_TIM";
            pharT05.MNC_DEPR_NO  = "MNC_DEPR_NO";
            pharT05.MNC_SECR_NO  = "MNC_SECR_NO";
            pharT05.MNC_SUM_PRI  = "MNC_SUM_PRI";
            pharT05.MNC_SUM_COS  = "MNC_SUM_COS";
            pharT05.MNC_HN_YR  = "MNC_HN_YR";
            pharT05.MNC_HN_NO  = "MNC_HN_NO";
            pharT05.MNC_AN_YR  = "MNC_AN_YR";
            pharT05.MNC_AN_NO  = "MNC_AN_NO";
            pharT05.MNC_PRE_NO  = "MNC_PRE_NO";
            pharT05.MNC_DATE  = "MNC_DATE";
            pharT05.MNC_TIME  = "MNC_TIME";
            pharT05.MNC_USE_LOG  = "MNC_USE_LOG";
            pharT05.MNC_FN_TYP_CD  = "MNC_FN_TYP_CD";
            pharT05.MNC_COM_CD  = "MNC_COM_CD";
            pharT05.MNC_FLAGAR  = "MNC_FLAGAR";
            pharT05.MNC_REQ_REF  = "MNC_REQ_REF";
            pharT05.MNC_CFR_TIME  = "MNC_CFR_TIME";
            pharT05.MNC_EMPR_CD  = "MNC_EMPR_CD";
            pharT05.MNC_EMPC_CD  = "MNC_EMPC_CD";
            pharT05.MNC_ORD_DOT  = "MNC_ORD_DOT";
            pharT05.MNC_QUE_NO  = "MNC_QUE_NO";
            pharT05.MNC_STAMP_DAT  = "MNC_STAMP_DAT";
            pharT05.MNC_STAMP_TIM  = "MNC_STAMP_TIM";
            pharT05.MNC_PAC_CD  = "MNC_PAC_CD";
            pharT05.MNC_PAC_TYP  = "MNC_PAC_TYP";
            pharT05.MNC_PHA_STS  = "MNC_PHA_STS";
            pharT05.MNC_REQ_COUNT  = "MNC_REQ_COUNT";
            pharT05.MNC_REQ_TYP  = "MNC_REQ_TYP";

            pharT05.table = "pharmacy_t05";
            pharT05.pkField = "";

        }
        public DataTable SelectByCFGCurrentDateSum(String flagall)
        {
            DataTable dt = new DataTable();
            String wherepha = "";
            if(flagall.Equals("wait"))
            {
                wherepha = "  and  pht05.MNC_PHA_STS != 'P' and pt01.MNC_PHA_FLG = 'A' ";
            }
            else if (flagall.Equals("finish"))
            {
                wherepha = "  and  pht05.MNC_PHA_STS = 'P' and pt01.MNC_PHA_FLG = 'A' ";
            }
            String sql = "SELECT  pht05.MNC_DOC_CD, pht05.MNC_CFR_YR, pht05.MNC_CFR_NO, convert(varchar(20),pht05.MNC_CFG_DAT,23) as MNC_CFG_DAT, isnull(pht05.MNC_ORD_DOT,'') as MNC_ORD_DOT " +
                    ", convert(varchar(20),pht05.MNC_REQ_DAT,23) as MNC_REQ_DAT, pht05.MNC_REQ_TIM  " +
                    ",  pht05.MNC_HN_NO, pht05.MNC_PRE_NO, convert(varchar(20),pht05.MNC_DATE,23) as MNC_DATE  " +
                    ", pht05.MNC_TIME, pht05.MNC_USE_LOG, pht05.MNC_FN_TYP_CD " +
                    ", pht05.MNC_QUE_NO, isnull(pt01.MNC_DOT_CD,'') as MNC_DOT_CD  " +
                    ", pm02.MNC_PFIX_DSC + ' ' + pm01.MNC_FNAME_T + ' ' + pm01.MNC_LNAME_T AS PATIENTNAME " +
                    ", convert(varchar(20), pm01.MNC_BDAY,23) as MNC_BDAY, pm01.MNC_SEX, finm02.MNC_FN_TYP_DSC " +
                    ", pt01.MNC_VN_NO, pt01.MNC_VN_SEQ, pt01.MNC_VN_SUM, pt01.MNC_DEP_NO, pt01.MNC_SEC_NO  " +
                    ", pm02dtr.MNC_PFIX_DSC + ' ' + pm26dtr.MNC_DOT_FNAME + ' ' + pm26dtr.MNC_DOT_LNAME AS ORDDOTNAME " +
                    ", isnull(userm01r.MNC_USR_FULL,'') AS MNC_USR_FULL_R, isnull(userm01c.MNC_USR_FULL,'') AS MNC_USR_FULL_C, pm16.MNC_SEC_DSC,isnull(pt011.status_doctor_order,'') as status_doctor_order " +
                    ",pt01.MNC_SHIF_MEMO, isnull(pm04.MNC_NAT_DSC,'') as nation,pht05.MNC_PHA_STS,pt01.MNC_PHA_TIM " +
                    " From PATIENT_T01 pt01 " +
                    "Left join PHARMACY_M16 pm16 on pm16.MNC_DEP_NO = pt01.MNC_DEP_NO AND pm16.MNC_SEC_NO = pt01.MNC_SEC_NO " +
                    "inner join PATIENT_M01 pm01 ON pt01.MNC_HN_YR = pm01.MNC_HN_YR AND pt01.MNC_HN_NO = pm01.MNC_HN_NO " +
                    "Left join PATIENT_M02 pm02 ON pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                    "Inner join PHARMACY_T05 pht05 on  pt01.MNC_HN_NO = pht05.MNC_HN_NO AND pt01.MNC_PRE_NO = pht05.MNC_PRE_NO AND pt01.MNC_DATE = pht05.MNC_DATE " +
                    "Left join PATIENT_M26 pm26dtr ON  pm26dtr.MNC_DOT_CD = pt01.MNC_DOT_CD   " +
                    "Left JOIN  PATIENT_M02 pm02dtr ON pm26dtr.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                    "Left join FINANCE_M02 finm02 ON pt01.MNC_FN_TYP_CD = finm02.MNC_FN_TYP_CD " +
                    "Left join USERLOG_M01 userm01c ON pht05.MNC_EMPC_CD = userm01c.MNC_USR_NAME " +
                    "Left join USERLOG_M01 userm01r ON pht05.MNC_EMPR_CD = userm01r.MNC_USR_NAME " +
                    "Left join PATIENT_T01_1 pt011 on pt01.MNC_HN_NO = pt011.MNC_HN_NO AND pt01.MNC_PRE_NO = pt011.MNC_PRE_NO AND pt01.MNC_DATE = pt011.MNC_DATE " +
                    "Left Join Patient_m04 pm04 on pm01.MNC_NAT_CD = pm04.MNC_NAT_CD " +
                    "Where pht05.MNC_CFG_DAT = convert(varchar(20),getdate(),23) and pht05.MNC_DOC_CD = 'CRO' and pt01.MNC_STS != 'C' " +
                    " " + wherepha +  //pht05.MNC_DOC_CD = 'CRO' 
                    "Order By pht05.MNC_REQ_DAT, pht05.MNC_REQ_TIM desc, pht05.MNC_CFR_NO desc ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        /*
         * tab 0 จาก source เดิม tab ข้อมูลผู้ป่วยนอก
         * SELECT     dbo.PATIENT_T01.MNC_ADM_FLG, dbo.PATIENT_T01.MNC_STAFF_STS, dbo.PATIENT_T01.MNC_HN_YR, dbo.PATIENT_T01.MNC_HN_NO, 
                      dbo.PATIENT_T01.MNC_DATE, dbo.PATIENT_T01.MNC_PRE_NO, dbo.PATIENT_T01.MNC_VN_NO, dbo.PATIENT_T01.MNC_VN_SEQ, 
                      dbo.PATIENT_T01.MNC_VN_SUM, dbo.PATIENT_M01.MNC_BDAY, dbo.PATIENT_T01.MNC_STAMP_TIM, dbo.PATIENT_M01.MNC_AGE, 
                      PATIENT_M02_1.MNC_PFIX_DSC + ' ' + dbo.PATIENT_M01.MNC_FNAME_T + ' ' + dbo.PATIENT_M01.MNC_LNAME_T AS PATIENTNAME, 
                      dbo.PATIENT_M01.MNC_SEX, 
                      PATIENT_M02_2.MNC_PFIX_DSC + ' ' + dbo.PATIENT_M26.MNC_DOT_FNAME + ' ' + dbo.PATIENT_M26.MNC_DOT_LNAME AS DOCTORNAME, 
                      dbo.PATIENT_T01.MNC_DOT_CD, dbo.FINANCE_M02.MNC_FN_TYP_DSC, dbo.PATIENT_M32.MNC_MD_DEP_DSC
FROM         dbo.PATIENT_T01 INNER JOIN
                      dbo.PATIENT_M01 ON dbo.PATIENT_T01.MNC_HN_NO = dbo.PATIENT_M01.MNC_HN_NO INNER JOIN
                      dbo.PATIENT_M26 ON dbo.PATIENT_T01.MNC_DOT_CD = dbo.PATIENT_M26.MNC_DOT_CD INNER JOIN
                      dbo.FINANCE_M02 ON dbo.PATIENT_T01.MNC_FN_TYP_CD = dbo.FINANCE_M02.MNC_FN_TYP_CD INNER JOIN
                      dbo.PATIENT_M32 ON dbo.PATIENT_T01.MNC_DEP_NO = dbo.PATIENT_M32.MNC_MD_DEP_NO AND 
                      dbo.PATIENT_T01.MNC_SEC_NO = dbo.PATIENT_M32.MNC_SEC_NO LEFT OUTER JOIN
                      dbo.PATIENT_M02 PATIENT_M02_1 ON dbo.PATIENT_M01.MNC_PFIX_CDT = PATIENT_M02_1.MNC_PFIX_CD LEFT OUTER JOIN
                      dbo.PATIENT_M02 PATIENT_M02_2 ON dbo.PATIENT_M26.MNC_DOT_PFIX = PATIENT_M02_2.MNC_PFIX_CD
WHERE     (dbo.PATIENT_T01.MNC_DATE = '07/18/2006') AND (dbo.PATIENT_T01.MNC_ACT_NO >= 130) AND (dbo.PATIENT_T01.MNC_STAFF_STS NOT IN ('Y', 
                      'C'))
        tab 2 จาก source เดิม tab การสั่งการที่รับทำการ
         
        SELECT     Pharmacy_t05.MNC_DOC_CD, Pharmacy_t05.MNC_CFR_YR, Pharmacy_t05.MNC_CFR_NO, Pharmacy_t05.MNC_CFG_DAT, Pharmacy_t05.MNC_ORD_DOT,
                      Pharmacy_t05.MNC_CFR_STS, Pharmacy_t05.MNC_DEPC_NO, Pharmacy_t05.MNC_SECC_NO, Pharmacy_t05.MNC_REQ_YR, 
                      Pharmacy_t05.MNC_REQ_NO, Pharmacy_t05.MNC_REQ_DAT, Pharmacy_t05.MNC_REQ_TIM, Pharmacy_t05.MNC_DEPR_NO, 
                      Pharmacy_t05.MNC_SECR_NO, Pharmacy_t05.MNC_SUM_PRI, Pharmacy_t05.MNC_SUM_COS, Pharmacy_t05.MNC_HN_YR, 
                      Pharmacy_t05.MNC_HN_NO, Pharmacy_t05.MNC_AN_YR, Pharmacy_t05.MNC_AN_NO, Pharmacy_t05.MNC_PRE_NO, Pharmacy_t05.MNC_DATE, 
                      Pharmacy_t05.MNC_TIME, Pharmacy_t05.MNC_USE_LOG, Pharmacy_t05.MNC_FN_TYP_CD, Pharmacy_t05.MNC_COM_CD, 
                      Pharmacy_t05.MNC_FLAGAR, Pharmacy_t05.MNC_REQ_REF, Pharmacy_t05.MNC_CFR_TIME, Pharmacy_t05.MNC_EMPR_CD, 
                      Pharmacy_t05.MNC_EMPC_CD, Pharmacy_t05.MNC_ORD_DOT, Pharmacy_t05.MNC_QUE_NO, Pharmacy_t05.MNC_STAMP_DAT, PATIENT_T01.MNC_DOT_CD,
                      Pharmacy_t05.MNC_STAMP_TIM, Pharmacy_t05.MNC_PAC_CD, Pharmacy_t05.MNC_PAC_TYP, 
                      Patient_m02.MNC_PFIX_DSC + ' ' + Patient_m01.MNC_FNAME_T + ' ' + Patient_m01.MNC_LNAME_T AS PATIENTNAME, Patient_m01.MNC_BDAY, 
                      Patient_m01.MNC_SEX, Finance_m02.MNC_FN_TYP_DSC, Patient_m24.MNC_COM_DSC, Finance_m02.MNC_COD_PRI_PH, 
                      Finance_m02.MNC_COD_PRI_PHI, Finance_m02.MNC_FN_STS, Patient_m01.MNC_ATT_NOTE, Patient_m01.MNC_FIN_NOTE, 
                      Pharmacy_t05.MNC_REQ_COUNT, Pharmacy_t05.MNC_REQ_TYP, Pharmacy_t05.MNC_PHA_STS, dbo.PATIENT_T01.MNC_VN_NO, 
                      dbo.PATIENT_T01.MNC_VN_SEQ, dbo.PATIENT_T01.MNC_VN_SUM, dbo.PATIENT_T08.MNC_AN_NO AS Expr1, 
                      dbo.PATIENT_T08.MNC_AN_YR AS Expr2, dbo.PATIENT_T01.MNC_DEP_NO, dbo.PATIENT_T01.MNC_SEC_NO, 
                      PATIENT_M02_1.MNC_PFIX_DSC + ' ' + dbo.PATIENT_M26.MNC_DOT_FNAME + ' ' + dbo.PATIENT_M26.MNC_DOT_LNAME AS ORDDOTNAME, 
                      USERLOG_M01_1.MNC_USR_FULL AS MNC_USR_FULL_C, Userlog_m01.MNC_USR_FULL, dbo.PHARMACY_M16.MNC_SEC_DSC
FROM         dbo.PATIENT_M26 INNER JOIN
                      dbo.PATIENT_M02 PATIENT_M02_1 ON dbo.PATIENT_M26.MNC_DOT_PFIX = PATIENT_M02_1.MNC_PFIX_CD RIGHT OUTER JOIN
                      dbo.PHARMACY_M16 INNER JOIN
                      dbo.PATIENT_T01 ON dbo.PHARMACY_M16.MNC_DEP_NO = dbo.PATIENT_T01.MNC_DEP_NO AND 
                      dbo.PHARMACY_M16.MNC_SEC_NO = dbo.PATIENT_T01.MNC_SEC_NO ON 
                      dbo.PATIENT_M26.MNC_DOT_CD = dbo.PATIENT_T01.MNC_DOT_CD RIGHT OUTER JOIN
                      dbo.PATIENT_T08 RIGHT OUTER JOIN
                      dbo.PHARMACY_T05 Pharmacy_t05 LEFT OUTER JOIN
                      dbo.USERLOG_M01 USERLOG_M01_1 ON Pharmacy_t05.MNC_EMPC_CD = USERLOG_M01_1.MNC_USR_NAME ON 
                      dbo.PATIENT_T08.MNC_AN_YR = Pharmacy_t05.MNC_AN_YR AND dbo.PATIENT_T08.MNC_AN_NO = Pharmacy_t05.MNC_AN_NO AND 
                      dbo.PATIENT_T08.MNC_HN_NO = Pharmacy_t05.MNC_HN_NO AND dbo.PATIENT_T08.MNC_HN_YR = Pharmacy_t05.MNC_HN_YR ON 
                      dbo.PATIENT_T01.MNC_HN_YR = Pharmacy_t05.MNC_HN_YR AND dbo.PATIENT_T01.MNC_HN_NO = Pharmacy_t05.MNC_HN_NO AND 
                      dbo.PATIENT_T01.MNC_PRE_NO = Pharmacy_t05.MNC_PRE_NO AND dbo.PATIENT_T01.MNC_DATE = Pharmacy_t05.MNC_DATE LEFT OUTER JOIN
                      dbo.PATIENT_M01 Patient_m01 ON Pharmacy_t05.MNC_HN_YR = Patient_m01.MNC_HN_YR AND 
                      Pharmacy_t05.MNC_HN_NO = Patient_m01.MNC_HN_NO LEFT OUTER JOIN
                      dbo.FINANCE_M02 Finance_m02 ON Pharmacy_t05.MNC_FN_TYP_CD = Finance_m02.MNC_FN_TYP_CD LEFT OUTER JOIN
                      dbo.USERLOG_M01 Userlog_m01 ON Pharmacy_t05.MNC_EMPR_CD = Userlog_m01.MNC_USR_NAME LEFT OUTER JOIN
                      dbo.PATIENT_M24 Patient_m24 ON Pharmacy_t05.MNC_COM_CD = Patient_m24.MNC_COM_CD LEFT OUTER JOIN
                      dbo.PATIENT_M02 Patient_m02 ON Patient_m01.MNC_PFIX_CDT = Patient_m02.MNC_PFIX_CD
WHERE     (Pharmacy_t05.MNC_CFG_DAT = '07/18/2006')


         * //จาก source เดิม tab การสั่งการที่รับทำการ
            //SELECT Pharmacy_t05.MNC_DOC_CD, Pharmacy_t05.MNC_CFR_YR, Pharmacy_t05.MNC_CFR_NO, Pharmacy_t05.MNC_CFG_DAT, Pharmacy_t05.MNC_ORD_DOT,
            //          Pharmacy_t05.MNC_CFR_STS, Pharmacy_t05.MNC_DEPC_NO, Pharmacy_t05.MNC_SECC_NO, Pharmacy_t05.MNC_REQ_YR, 
            //          Pharmacy_t05.MNC_REQ_NO, Pharmacy_t05.MNC_REQ_DAT, Pharmacy_t05.MNC_REQ_TIM, Pharmacy_t05.MNC_DEPR_NO, 
            //          Pharmacy_t05.MNC_SECR_NO, Pharmacy_t05.MNC_SUM_PRI, Pharmacy_t05.MNC_SUM_COS, Pharmacy_t05.MNC_HN_YR, 
            //          Pharmacy_t05.MNC_HN_NO, Pharmacy_t05.MNC_AN_YR, Pharmacy_t05.MNC_AN_NO, Pharmacy_t05.MNC_PRE_NO, Pharmacy_t05.MNC_DATE, 
            //          Pharmacy_t05.MNC_TIME, Pharmacy_t05.MNC_USE_LOG, Pharmacy_t05.MNC_FN_TYP_CD, Pharmacy_t05.MNC_COM_CD, 
            //          Pharmacy_t05.MNC_FLAGAR, Pharmacy_t05.MNC_REQ_REF, Pharmacy_t05.MNC_CFR_TIME, Pharmacy_t05.MNC_EMPR_CD, 
            //          Pharmacy_t05.MNC_EMPC_CD, Pharmacy_t05.MNC_ORD_DOT, Pharmacy_t05.MNC_QUE_NO, Pharmacy_t05.MNC_STAMP_DAT, PATIENT_T01.MNC_DOT_CD,
            //          Pharmacy_t05.MNC_STAMP_TIM, Pharmacy_t05.MNC_PAC_CD, Pharmacy_t05.MNC_PAC_TYP, 
            //          Patient_m02.MNC_PFIX_DSC + ' ' + Patient_m01.MNC_FNAME_T + ' ' + Patient_m01.MNC_LNAME_T AS PATIENTNAME, Patient_m01.MNC_BDAY, 
            //          Patient_m01.MNC_SEX, Finance_m02.MNC_FN_TYP_DSC, Patient_m24.MNC_COM_DSC, Finance_m02.MNC_COD_PRI_PH, 
            //          Finance_m02.MNC_COD_PRI_PHI, Finance_m02.MNC_FN_STS, Patient_m01.MNC_ATT_NOTE, Patient_m01.MNC_FIN_NOTE, 
            //          Pharmacy_t05.MNC_REQ_COUNT, Pharmacy_t05.MNC_REQ_TYP, Pharmacy_t05.MNC_PHA_STS, dbo.PATIENT_T01.MNC_VN_NO, 
            //          dbo.PATIENT_T01.MNC_VN_SEQ, dbo.PATIENT_T01.MNC_VN_SUM, dbo.PATIENT_T08.MNC_AN_NO AS Expr1, 
            //          dbo.PATIENT_T08.MNC_AN_YR AS Expr2, dbo.PATIENT_T01.MNC_DEP_NO, dbo.PATIENT_T01.MNC_SEC_NO, 
            //          PATIENT_M02_1.MNC_PFIX_DSC + ' ' + dbo.PATIENT_M26.MNC_DOT_FNAME + ' ' + dbo.PATIENT_M26.MNC_DOT_LNAME AS ORDDOTNAME,
            //          USERLOG_M01_1.MNC_USR_FULL AS MNC_USR_FULL_C, Userlog_m01.MNC_USR_FULL, dbo.PHARMACY_M16.MNC_SEC_DSC
            //    FROM         dbo.PATIENT_M26 INNER JOIN
            //          dbo.PATIENT_M02 PATIENT_M02_1 ON dbo.PATIENT_M26.MNC_DOT_PFIX = PATIENT_M02_1.MNC_PFIX_CD RIGHT OUTER JOIN
            //          dbo.PHARMACY_M16 INNER JOIN
            //          dbo.PATIENT_T01 ON dbo.PHARMACY_M16.MNC_DEP_NO = dbo.PATIENT_T01.MNC_DEP_NO AND
            //          dbo.PHARMACY_M16.MNC_SEC_NO = dbo.PATIENT_T01.MNC_SEC_NO ON
            //          dbo.PATIENT_M26.MNC_DOT_CD = dbo.PATIENT_T01.MNC_DOT_CD RIGHT OUTER JOIN
            //          dbo.PATIENT_T08 RIGHT OUTER JOIN
            //          dbo.PHARMACY_T05 Pharmacy_t05 LEFT OUTER JOIN
            //          dbo.USERLOG_M01 USERLOG_M01_1 ON Pharmacy_t05.MNC_EMPC_CD = USERLOG_M01_1.MNC_USR_NAME ON
            //          dbo.PATIENT_T08.MNC_AN_YR = Pharmacy_t05.MNC_AN_YR AND dbo.PATIENT_T08.MNC_AN_NO = Pharmacy_t05.MNC_AN_NO AND
            //          dbo.PATIENT_T08.MNC_HN_NO = Pharmacy_t05.MNC_HN_NO AND dbo.PATIENT_T08.MNC_HN_YR = Pharmacy_t05.MNC_HN_YR ON
            //          dbo.PATIENT_T01.MNC_HN_YR = Pharmacy_t05.MNC_HN_YR AND dbo.PATIENT_T01.MNC_HN_NO = Pharmacy_t05.MNC_HN_NO AND
            //          dbo.PATIENT_T01.MNC_PRE_NO = Pharmacy_t05.MNC_PRE_NO AND dbo.PATIENT_T01.MNC_DATE = Pharmacy_t05.MNC_DATE LEFT OUTER JOIN
            //          dbo.PATIENT_M01 Patient_m01 ON Pharmacy_t05.MNC_HN_YR = Patient_m01.MNC_HN_YR AND
            //          Pharmacy_t05.MNC_HN_NO = Patient_m01.MNC_HN_NO LEFT OUTER JOIN
            //          dbo.FINANCE_M02 Finance_m02 ON Pharmacy_t05.MNC_FN_TYP_CD = Finance_m02.MNC_FN_TYP_CD LEFT OUTER JOIN
            //          dbo.USERLOG_M01 Userlog_m01 ON Pharmacy_t05.MNC_EMPR_CD = Userlog_m01.MNC_USR_NAME LEFT OUTER JOIN
            //          dbo.PATIENT_M24 Patient_m24 ON Pharmacy_t05.MNC_COM_CD = Patient_m24.MNC_COM_CD LEFT OUTER JOIN
            //          dbo.PATIENT_M02 Patient_m02 ON Patient_m01.MNC_PFIX_CDT = Patient_m02.MNC_PFIX_CD
            //    WHERE(Pharmacy_t05.MNC_CFG_DAT = '07/18/2006')
         */
        public DataTable SelectByCFGCurrentDate()
        {
            DataTable dt = new DataTable();
            //ใน source MNC_PHA_STS = 'P' จ่ายยาแล้ว  ใน table ไม่มี null มี ค่า "" กับ 'P'
            String sql = "SELECT pht05.MNC_DOC_CD, pht05.MNC_CFR_YR, pht05.MNC_CFR_NO, convert(varchar(20),pht05.MNC_CFG_DAT,23) as MNC_CFG_DAT, pht05.MNC_ORD_DOT  " +
                ", pht05.MNC_CFR_STS, pht05.MNC_DEPC_NO, pht05.MNC_SECC_NO, pht05.MNC_REQ_YR, pht05.MNC_REQ_NO, convert(varchar(20),pht05.MNC_REQ_DAT,23) as MNC_REQ_DAT, pht05.MNC_REQ_TIM, pht05.MNC_DEPR_NO  " +
                ", pht05.MNC_SECR_NO, pht05.MNC_SUM_PRI, pht05.MNC_SUM_COS, pht05.MNC_HN_YR,  pht05.MNC_HN_NO, pht05.MNC_AN_YR, pht05.MNC_AN_NO, pht05.MNC_PRE_NO, convert(varchar(20),pht05.MNC_DATE,23) as MNC_DATE  " +
                ", pht05.MNC_TIME, pht05.MNC_USE_LOG, pht05.MNC_FN_TYP_CD, pht05.MNC_COM_CD, pht05.MNC_FLAGAR, pht05.MNC_REQ_REF, pht05.MNC_CFR_TIME, pht05.MNC_EMPR_CD " +
                ", pht05.MNC_EMPC_CD, pht05.MNC_ORD_DOT, pht05.MNC_QUE_NO, pht05.MNC_STAMP_DAT, pt01.MNC_DOT_CD, pht05.MNC_STAMP_TIM, pht05.MNC_PAC_CD, pht05.MNC_PAC_TYP  " +
                ", pm02.MNC_PFIX_DSC + ' ' + pm01.MNC_FNAME_T + ' ' + pm01.MNC_LNAME_T AS PATIENTNAME " +
                ", pm01.MNC_BDAY, pm01.MNC_SEX, finm02.MNC_FN_TYP_DSC, pm24.MNC_COM_DSC, finm02.MNC_COD_PRI_PH, finm02.MNC_COD_PRI_PHI, finm02.MNC_FN_STS, pm01.MNC_ATT_NOTE, pm01.MNC_FIN_NOTE " +
                ", pht05.MNC_REQ_COUNT, pht05.MNC_REQ_TYP, pht05.MNC_PHA_STS, pt01.MNC_VN_NO, pt01.MNC_VN_SEQ, pt01.MNC_VN_SUM, pt01.MNC_DEP_NO, pt01.MNC_SEC_NO  " +
                ", pm02dtr.MNC_PFIX_DSC + ' ' + pm26dtr.MNC_DOT_FNAME + ' ' + pm26dtr.MNC_DOT_LNAME AS ORDDOTNAME " +
                ", userm01.MNC_USR_FULL AS MNC_USR_FULL_C, userm01.MNC_USR_FULL, pm16.MNC_SEC_DSC,isnull(pt011.status_doctor_order,'') as status_doctor_order " +
                "From PATIENT_T01 pt01 " +
                "Left join PHARMACY_M16 pm16 on pm16.MNC_DEP_NO = pt01.MNC_DEP_NO AND pm16.MNC_SEC_NO = pt01.MNC_SEC_NO " +
                "inner join PATIENT_M01 pm01 ON pt01.MNC_HN_YR = pm01.MNC_HN_YR AND pt01.MNC_HN_NO = pm01.MNC_HN_NO " +
                "Left join PATIENT_M02 pm02 ON pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Inner join PHARMACY_T05 pht05 on pt01.MNC_HN_YR = pht05.MNC_HN_YR AND pt01.MNC_HN_NO = pht05.MNC_HN_NO AND pt01.MNC_PRE_NO = pht05.MNC_PRE_NO AND pt01.MNC_DATE = pht05.MNC_DATE " +
                "Left join PATIENT_M26 pm26dtr ON  pm26dtr.MNC_DOT_CD = pt01.MNC_DOT_CD   " +
                "Left JOIN  PATIENT_M02 pm02dtr ON pm26dtr.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                "Left join FINANCE_M02 finm02 ON pt01.MNC_FN_TYP_CD = finm02.MNC_FN_TYP_CD " +
                "Left join USERLOG_M01 userm01 ON pht05.MNC_EMPR_CD = userm01.MNC_USR_NAME " +
                "Left join PATIENT_M24 pm24 ON pt01.MNC_COM_CD = pm24.MNC_COM_CD " +
                "Inner join PATIENT_T01_1 pt011 on pt01.MNC_HN_NO = pt011.MNC_HN_NO AND pt01.MNC_PRE_NO = pt011.MNC_PRE_NO AND pt01.MNC_DATE = pt011.MNC_DATE " +
                "Where pht05.MNC_CFG_DAT = convert(varchar(20),getdate(),23) and  pht05.MNC_PHA_STS = 'P' " +
                "Order By pht05.MNC_REQ_DAT, pht05.MNC_REQ_TIM desc, pht05.MNC_CFR_NO desc ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectByCFGdate(String startdate, String enddate)
        {
            DataTable dt = new DataTable();
            
            String sql = "SELECT pht05.MNC_DOC_CD, pht05.MNC_CFR_YR, pht05.MNC_CFR_NO, pht05.MNC_CFG_DAT, pht05.MNC_ORD_DOT  " +
                ", pht05.MNC_CFR_STS, pht05.MNC_DEPC_NO, pht05.MNC_SECC_NO, pht05.MNC_REQ_YR, pht05.MNC_REQ_NO, pht05.MNC_REQ_DAT, pht05.MNC_REQ_TIM, pht05.MNC_DEPR_NO  " +
                ", pht05.MNC_SECR_NO, pht05.MNC_SUM_PRI, pht05.MNC_SUM_COS, pht05.MNC_HN_YR,  pht05.MNC_HN_NO, pht05.MNC_AN_YR, pht05.MNC_AN_NO, pht05.MNC_PRE_NO, pht05.MNC_DATE  " +
                ", pht05.MNC_TIME, pht05.MNC_USE_LOG, pht05.MNC_FN_TYP_CD, pht05.MNC_COM_CD, pht05.MNC_FLAGAR, pht05.MNC_REQ_REF, pht05.MNC_CFR_TIME, pht05.MNC_EMPR_CD " +
                ", pht05.MNC_EMPC_CD, pht05.MNC_ORD_DOT, pht05.MNC_QUE_NO, pht05.MNC_STAMP_DAT, pt01.MNC_DOT_CD, pht05.MNC_STAMP_TIM, pht05.MNC_PAC_CD, pht05.MNC_PAC_TYP  " +
                ", pm02.MNC_PFIX_DSC + ' ' + pm01.MNC_FNAME_T + ' ' + pm01.MNC_LNAME_T AS PATIENTNAME " +
                ", pm01.MNC_BDAY, pm01.MNC_SEX, finm02.MNC_FN_TYP_DSC, pm24.MNC_COM_DSC, finm02.MNC_COD_PRI_PH, finm02.MNC_COD_PRI_PHI, finm02.MNC_FN_STS, pm01.MNC_ATT_NOTE, pm01.MNC_FIN_NOTE " +
                ", pht05.MNC_REQ_COUNT, pht05.MNC_REQ_TYP, pht05.MNC_PHA_STS, pt01.MNC_VN_NO, pt01.MNC_VN_SEQ, pt01.MNC_VN_SUM, pt01.MNC_DEP_NO, pt01.MNC_SEC_NO  " +
                ", pm02dtr.MNC_PFIX_DSC + ' ' + pm26dtr.MNC_DOT_FNAME + ' ' + pm26dtr.MNC_DOT_LNAME AS ORDDOTNAME " +
                ", userm01.MNC_USR_FULL AS MNC_USR_FULL_C, userm01.MNC_USR_FULL, pm16.MNC_SEC_DSC " +
                "From PATIENT_T01 pt01 " +
                "Left join PHARMACY_M16 pm16 on pm16.MNC_DEP_NO = pt01.MNC_DEP_NO AND pm16.MNC_SEC_NO = pt01.MNC_SEC_NO " +
                "Left join PATIENT_M26 pm26dtr ON  pm26dtr.MNC_DOT_CD = pt01.MNC_DOT_CD   " +
                "Left JOIN  PATIENT_M02 pm02dtr ON pm26dtr.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                "inner join PATIENT_M01 pm01 ON pt01.MNC_HN_YR = pm01.MNC_HN_YR AND pt01.MNC_HN_NO = pm01.MNC_HN_NO " +
                "Left join PATIENT_M02 pm02 ON pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Inner join PHARMACY_T05 pht05 on pt01.MNC_HN_YR = pht05.MNC_HN_YR AND pt01.MNC_HN_NO = pht05.MNC_HN_NO AND pt01.MNC_PRE_NO = pht05.MNC_PRE_NO AND pt01.MNC_DATE = pht05.MNC_DATE " +
                "Left join FINANCE_M02 finm02 ON pt01.MNC_FN_TYP_CD = finm02.MNC_FN_TYP_CD " +
                "Left join USERLOG_M01 userm01 ON pht05.MNC_EMPR_CD = userm01.MNC_USR_NAME " +
                "Left join PATIENT_M24 pm24 ON pt01.MNC_COM_CD = pm24.MNC_COM_CD " +
                "Where pht05.MNC_CFG_DAT >= '" + startdate + "' and pht05.MNC_CFG_DAT <= '" + enddate + "'   " +
                "Order By dfd.MNC_DOT_CD_DF, dfd.MNC_DOT_GRP_CD, dfd.MNC_FN_DAT, dfd.MNC_DATE, dfd.MNC_PRE_NO, dfd.MNC_DF_GROUP ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectOPDByCFGdate(String startdate, String enddate)
        {
            DataTable dt = new DataTable();
            //จาก source เดิม tab การสั่งการที่รับทำการ
            //SELECT Pharmacy_t05.MNC_DOC_CD, Pharmacy_t05.MNC_CFR_YR, Pharmacy_t05.MNC_CFR_NO, Pharmacy_t05.MNC_CFG_DAT, Pharmacy_t05.MNC_ORD_DOT,
            //          Pharmacy_t05.MNC_CFR_STS, Pharmacy_t05.MNC_DEPC_NO, Pharmacy_t05.MNC_SECC_NO, Pharmacy_t05.MNC_REQ_YR, 
            //          Pharmacy_t05.MNC_REQ_NO, Pharmacy_t05.MNC_REQ_DAT, Pharmacy_t05.MNC_REQ_TIM, Pharmacy_t05.MNC_DEPR_NO, 
            //          Pharmacy_t05.MNC_SECR_NO, Pharmacy_t05.MNC_SUM_PRI, Pharmacy_t05.MNC_SUM_COS, Pharmacy_t05.MNC_HN_YR, 
            //          Pharmacy_t05.MNC_HN_NO, Pharmacy_t05.MNC_AN_YR, Pharmacy_t05.MNC_AN_NO, Pharmacy_t05.MNC_PRE_NO, Pharmacy_t05.MNC_DATE, 
            //          Pharmacy_t05.MNC_TIME, Pharmacy_t05.MNC_USE_LOG, Pharmacy_t05.MNC_FN_TYP_CD, Pharmacy_t05.MNC_COM_CD, 
            //          Pharmacy_t05.MNC_FLAGAR, Pharmacy_t05.MNC_REQ_REF, Pharmacy_t05.MNC_CFR_TIME, Pharmacy_t05.MNC_EMPR_CD, 
            //          Pharmacy_t05.MNC_EMPC_CD, Pharmacy_t05.MNC_ORD_DOT, Pharmacy_t05.MNC_QUE_NO, Pharmacy_t05.MNC_STAMP_DAT, PATIENT_T01.MNC_DOT_CD,
            //          Pharmacy_t05.MNC_STAMP_TIM, Pharmacy_t05.MNC_PAC_CD, Pharmacy_t05.MNC_PAC_TYP, 
            //          Patient_m02.MNC_PFIX_DSC + ' ' + Patient_m01.MNC_FNAME_T + ' ' + Patient_m01.MNC_LNAME_T AS PATIENTNAME, Patient_m01.MNC_BDAY, 
            //          Patient_m01.MNC_SEX, Finance_m02.MNC_FN_TYP_DSC, Patient_m24.MNC_COM_DSC, Finance_m02.MNC_COD_PRI_PH, 
            //          Finance_m02.MNC_COD_PRI_PHI, Finance_m02.MNC_FN_STS, Patient_m01.MNC_ATT_NOTE, Patient_m01.MNC_FIN_NOTE, 
            //          Pharmacy_t05.MNC_REQ_COUNT, Pharmacy_t05.MNC_REQ_TYP, Pharmacy_t05.MNC_PHA_STS, dbo.PATIENT_T01.MNC_VN_NO, 
            //          dbo.PATIENT_T01.MNC_VN_SEQ, dbo.PATIENT_T01.MNC_VN_SUM, dbo.PATIENT_T08.MNC_AN_NO AS Expr1, 
            //          dbo.PATIENT_T08.MNC_AN_YR AS Expr2, dbo.PATIENT_T01.MNC_DEP_NO, dbo.PATIENT_T01.MNC_SEC_NO, 
            //          PATIENT_M02_1.MNC_PFIX_DSC + ' ' + dbo.PATIENT_M26.MNC_DOT_FNAME + ' ' + dbo.PATIENT_M26.MNC_DOT_LNAME AS ORDDOTNAME,
            //          USERLOG_M01_1.MNC_USR_FULL AS MNC_USR_FULL_C, Userlog_m01.MNC_USR_FULL, dbo.PHARMACY_M16.MNC_SEC_DSC
            //    FROM         dbo.PATIENT_M26 INNER JOIN
            //          dbo.PATIENT_M02 PATIENT_M02_1 ON dbo.PATIENT_M26.MNC_DOT_PFIX = PATIENT_M02_1.MNC_PFIX_CD RIGHT OUTER JOIN
            //          dbo.PHARMACY_M16 INNER JOIN
            //          dbo.PATIENT_T01 ON dbo.PHARMACY_M16.MNC_DEP_NO = dbo.PATIENT_T01.MNC_DEP_NO AND
            //          dbo.PHARMACY_M16.MNC_SEC_NO = dbo.PATIENT_T01.MNC_SEC_NO ON
            //          dbo.PATIENT_M26.MNC_DOT_CD = dbo.PATIENT_T01.MNC_DOT_CD RIGHT OUTER JOIN
            //          dbo.PATIENT_T08 RIGHT OUTER JOIN
            //          dbo.PHARMACY_T05 Pharmacy_t05 LEFT OUTER JOIN
            //          dbo.USERLOG_M01 USERLOG_M01_1 ON Pharmacy_t05.MNC_EMPC_CD = USERLOG_M01_1.MNC_USR_NAME ON
            //          dbo.PATIENT_T08.MNC_AN_YR = Pharmacy_t05.MNC_AN_YR AND dbo.PATIENT_T08.MNC_AN_NO = Pharmacy_t05.MNC_AN_NO AND
            //          dbo.PATIENT_T08.MNC_HN_NO = Pharmacy_t05.MNC_HN_NO AND dbo.PATIENT_T08.MNC_HN_YR = Pharmacy_t05.MNC_HN_YR ON
            //          dbo.PATIENT_T01.MNC_HN_YR = Pharmacy_t05.MNC_HN_YR AND dbo.PATIENT_T01.MNC_HN_NO = Pharmacy_t05.MNC_HN_NO AND
            //          dbo.PATIENT_T01.MNC_PRE_NO = Pharmacy_t05.MNC_PRE_NO AND dbo.PATIENT_T01.MNC_DATE = Pharmacy_t05.MNC_DATE LEFT OUTER JOIN
            //          dbo.PATIENT_M01 Patient_m01 ON Pharmacy_t05.MNC_HN_YR = Patient_m01.MNC_HN_YR AND
            //          Pharmacy_t05.MNC_HN_NO = Patient_m01.MNC_HN_NO LEFT OUTER JOIN
            //          dbo.FINANCE_M02 Finance_m02 ON Pharmacy_t05.MNC_FN_TYP_CD = Finance_m02.MNC_FN_TYP_CD LEFT OUTER JOIN
            //          dbo.USERLOG_M01 Userlog_m01 ON Pharmacy_t05.MNC_EMPR_CD = Userlog_m01.MNC_USR_NAME LEFT OUTER JOIN
            //          dbo.PATIENT_M24 Patient_m24 ON Pharmacy_t05.MNC_COM_CD = Patient_m24.MNC_COM_CD LEFT OUTER JOIN
            //          dbo.PATIENT_M02 Patient_m02 ON Patient_m01.MNC_PFIX_CDT = Patient_m02.MNC_PFIX_CD
            //    WHERE(Pharmacy_t05.MNC_CFG_DAT = '07/18/2006')
            String sql = "select phart05.MNC_DOC_CD, phart05.MNC_CFR_YR, phart05.MNC_CFR_NO, phart05.MNC_CFG_DAT, phart05.MNC_ORD_DOT  " +
                ", phart05.MNC_CFR_STS, phart05.MNC_DEPC_NO, phart05.MNC_SECC_NO, phart05.MNC_REQ_YR  " +
                ", Pharmacy_t05.MNC_REQ_NO, Pharmacy_t05.MNC_REQ_DAT, Pharmacy_t05.MNC_REQ_TIM, Pharmacy_t05.MNC_DEPR_NO  " +
                ", Pharmacy_t05.MNC_SECR_NO, Pharmacy_t05.MNC_SUM_PRI, Pharmacy_t05.MNC_SUM_COS, Pharmacy_t05.MNC_HN_YR " +
                ", Pharmacy_t05.MNC_HN_NO, Pharmacy_t05.MNC_AN_YR, Pharmacy_t05.MNC_AN_NO, Pharmacy_t05.MNC_PRE_NO, Pharmacy_t05.MNC_DATE  " +
                ", Pharmacy_t05.MNC_TIME, Pharmacy_t05.MNC_USE_LOG, Pharmacy_t05.MNC_FN_TYP_CD, Pharmacy_t05.MNC_COM_CD " +
                ", Pharmacy_t05.MNC_FLAGAR, Pharmacy_t05.MNC_REQ_REF, Pharmacy_t05.MNC_CFR_TIME, Pharmacy_t05.MNC_EMPR_CD " +
                ", Pharmacy_t05.MNC_EMPC_CD, Pharmacy_t05.MNC_ORD_DOT, Pharmacy_t05.MNC_QUE_NO, Pharmacy_t05.MNC_STAMP_DAT, PATIENT_T01.MNC_DOT_CD  " +
                ", Pharmacy_t05.MNC_STAMP_TIM, Pharmacy_t05.MNC_PAC_CD, Pharmacy_t05.MNC_PAC_TYP " +
                ", Patient_m02.MNC_PFIX_DSC + ' ' + Patient_m01.MNC_FNAME_T + ' ' + Patient_m01.MNC_LNAME_T AS PATIENTNAME, Patient_m01.MNC_BDAY " +
                ", Patient_m01.MNC_SEX, Finance_m02.MNC_FN_TYP_DSC, Patient_m24.MNC_COM_DSC, Finance_m02.MNC_COD_PRI_PH " +
                ", Finance_m02.MNC_COD_PRI_PHI, Finance_m02.MNC_FN_STS, Patient_m01.MNC_ATT_NOTE, Patient_m01.MNC_FIN_NOTE " +
                ", Pharmacy_t05.MNC_REQ_COUNT, Pharmacy_t05.MNC_REQ_TYP, Pharmacy_t05.MNC_PHA_STS, dbo.PATIENT_T01.MNC_VN_NO " +
                ", dbo.PATIENT_T01.MNC_VN_SEQ, dbo.PATIENT_T01.MNC_VN_SUM, dbo.PATIENT_T08.MNC_AN_NO AS Expr1 " +
                ", PATIENT_M02_1.MNC_PFIX_DSC + ' ' + dbo.PATIENT_M26.MNC_DOT_FNAME + ' ' + dbo.PATIENT_M26.MNC_DOT_LNAME AS ORDDOTNAME " +
                ", USERLOG_M01_1.MNC_USR_FULL AS MNC_USR_FULL_C, Userlog_m01.MNC_USR_FULL, dbo.PHARMACY_M16.MNC_SEC_DSC " +
                "From PATIENT_T01 pt01 " +
                "Left join PHARMACY_M16 pm16 on pm16.MNC_DEP_NO = pt01.MNC_DEP_NO AND pm16.MNC_SEC_NO = pt01.MNC_SEC_NO " +
                "Left join PATIENT_M26 pm26dtr ON  pm26dtr.MNC_DOT_CD = pt01.MNC_DOT_CD   " +
                "Left JOIN  PATIENT_M02 pm02dtr ON pm26dtr.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                "inner join PATIENT_M01 pm01 ON pt01.MNC_HN_YR = pm01.MNC_HN_YR AND pt01.MNC_HN_NO = pm01.MNC_HN_NO " +
                "Left join PATIENT_M02 pm02 ON pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Inner join PHARMACY_T05 phart05 on  pt01.MNC_HN_NO = phart05.MNC_HN_NO AND pt01.MNC_PRE_NO = phart05.MNC_PRE_NO AND pt01.MNC_DATE = phart05.MNC_DATE " +
                "Left join FINANCE_M02 finm02 ON pt01.MNC_FN_TYP_CD = finm02.MNC_FN_TYP_CD " +
                "Left join USERLOG_M01 userm01 ON pht05.MNC_EMPR_CD = userm01.MNC_USR_NAME " +
                "Left join PATIENT_M24 pm24 ON pt01.MNC_COM_CD = pm24.MNC_COM_CD " +
                "Where pht05.MNC_CFG_DAT >= '" + startdate + "' and pht05.MNC_CFG_DAT <= '" + enddate + "'   " +
                "Order By dfd.MNC_DOT_CD_DF, dfd.MNC_DOT_GRP_CD, dfd.MNC_FN_DAT, dfd.MNC_DATE, dfd.MNC_PRE_NO, dfd.MNC_DF_GROUP ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public String updateStatusPrintStrickered(String doccd, String cfryear, String cfrno, String cfrdate)
        {
            String sql = "", re = "";
            try
            {
                sql = "Update PHARMACY_T05 Set status_print_stricker_new = '1' where MNC_DOC_CD = '" + doccd + "' and MNC_CFR_YR = '" + cfryear + "' and MNC_CFR_NO = '"+ cfrno+ "' and MNC_CFG_DAT = '"+ cfrdate+"' ";
                conn.ExecuteNonQuery(conn.connMainHIS, sql);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                re = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateStatusPrintStrickered sql " + sql + " ex " + re);
            }
            return re;
        }
        public String clearStatusPrintStrickered()
        {
            String sql = "", re = "";
            try
            {
                sql = "Update PHARMACY_T05 Set status_print_stricker_new = '1' where status_print_stricker_new is null   ";
                conn.ExecuteNonQuery(conn.connMainHIS, sql);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                re = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateStatusPrintStrickered sql " + sql + " ex " + re);
            }
            return re;
        }
        public String updatePttReceiveMed(String usercode, String doccd, String cfrno, String cfryear, String cfgdate, String hn, String visit_date, String pre_no)
        {
            String sql = "", re = "";
            //PhamacyMas.Phar_T05MNC_PHA_STS.Value := 'P';
            //PhamacyMas.Phar_T05MNC_EMPR_CD.Value := FCheckUsrmod.Edit3.Text;
            //PhamacyMas.Phar_T05MNC_EMPC_CD.Value := FCheckUsrmod.Edit3.Text;
            //Phar_t05.ParamByName('MNC_Doc_cd').Value := MNC_Doc_cd;
            //Phar_t05.ParamByName('MNC_CFR_no').Value := MNC_CFR_no;
            //Phar_t05.ParamByName('MNC_CFR_YR').Value := MNC_CFR_YR;
            //Phar_t05.ParamByName('MNC_CFG_DAT').Value := MNC_CFR_DAT;
            try
            {
                sql = "Update PHARMACY_T05 Set MNC_PHA_STS = 'P', MNC_EMPC_CD = '"+ usercode + "' where MNC_DOC_CD = '"+ doccd + "' and  MNC_CFR_NO = '"+ cfrno + "' and MNC_CFR_YR = '"+ cfryear + "' and MNC_CFG_DAT = '"+ cfgdate + "'";
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                sql = "Update PATIENT_T01 set MNC_PHA_FLG = 'A', MNC_PHA_USR = '"+ usercode + "', MNC_PHA_TIM = replace(left(convert(varchar(100),getdate(),108),5),':','') Where mnc_hn_no = '" + hn + "' and mnc_date = '"+ visit_date + "' and mnc_pre_no = '"+ pre_no + "' ";
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                re = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updatePttreceiveMed sql " + sql + " ex " + re);
            }
            return re;
        }
        public String insertPharmacyT0506jjj(String hnyear, String hn, String visit_date, String pre_no, String req_year, String req_no, String req_date, String drug_set, String userI)
        {
            String re = "";
            String sql = "";
            try
            {
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "insert_pharmacy_t05_t06_cro_jjj";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("mnc_hn_yr", hnyear);
                conn.comStore.Parameters.AddWithValue("mnc_hn_no", hn);
                conn.comStore.Parameters.AddWithValue("mnc_date", visit_date);
                conn.comStore.Parameters.AddWithValue("mnc_pre_no", pre_no);
                conn.comStore.Parameters.AddWithValue("mnc_usr_add", userI);
                conn.comStore.Parameters.AddWithValue("mnc_usr_upd", userI);
                conn.comStore.Parameters.AddWithValue("mnc_req_yr", "2565");
                conn.comStore.Parameters.AddWithValue("mnc_req_no", req_no);
                conn.comStore.Parameters.AddWithValue("mnc_req_dat", req_date);
                conn.comStore.Parameters.AddWithValue("flag_drug", drug_set);
                //conn.comStore.Parameters.AddWithValue("qty", "50");
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;
                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;

            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insertPharmacyT0506 sql " + sql + " hn " + hn + " hnyear " + hnyear + " visit_date " + visit_date + " pre_no " + pre_no + " req_year " + req_year + " req_no " + req_no);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertPharmacyT0506(String hnyear,String hn, String visit_date, String pre_no, String req_year, String req_no, String req_date, String drug_set, String userI)
        {
            String re = "";
            String sql = "";
            try
            {
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "insert_pharmacy_t05_t06_cro";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("mnc_hn_yr", hnyear);
                conn.comStore.Parameters.AddWithValue("mnc_hn_no", hn);
                conn.comStore.Parameters.AddWithValue("mnc_date", visit_date);
                conn.comStore.Parameters.AddWithValue("mnc_pre_no", pre_no);
                conn.comStore.Parameters.AddWithValue("mnc_usr_add", userI);
                conn.comStore.Parameters.AddWithValue("mnc_usr_upd", userI);
                conn.comStore.Parameters.AddWithValue("mnc_req_yr", "2565");
                conn.comStore.Parameters.AddWithValue("mnc_req_no", req_no);
                conn.comStore.Parameters.AddWithValue("mnc_req_dat", req_date);
                conn.comStore.Parameters.AddWithValue("flag_drug", drug_set);
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;
                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;

            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insertPharmacyT0506 sql " + sql + " hn " + hn + " hnyear " + hnyear + " visit_date " + visit_date + " pre_no " + pre_no + " req_year " + req_year + " req_no " + req_no);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public PharmacyT05 setPharmacyT05(DataTable dt)
        {
            PharmacyT05 pharT05 = new PharmacyT05();
            if (dt.Rows.Count > 0)
            {
                pharT05.MNC_DOC_CD = dt.Rows[0]["MNC_DOC_CD"].ToString();
                pharT05.MNC_CFR_YR = dt.Rows[0]["MNC_CFR_YR"].ToString();
                pharT05.MNC_CFR_NO = dt.Rows[0]["MNC_CFR_NO"].ToString();
                pharT05.MNC_CFG_DAT = dt.Rows[0]["MNC_CFG_DAT"].ToString();
                pharT05.MNC_CFR_STS = dt.Rows[0]["MNC_CFR_STS"].ToString();
                pharT05.MNC_DEPC_NO = dt.Rows[0]["MNC_DEPC_NO"].ToString();
                pharT05.MNC_SECC_NO = dt.Rows[0]["MNC_SECC_NO"].ToString();
                pharT05.MNC_REQ_YR = dt.Rows[0]["MNC_REQ_YR"].ToString();
                pharT05.MNC_REQ_NO = dt.Rows[0]["MNC_REQ_NO"].ToString();
                pharT05.MNC_REQ_DAT = dt.Rows[0]["MNC_REQ_DAT"].ToString();
                pharT05.MNC_REQ_TIM = dt.Rows[0]["MNC_REQ_TIM"].ToString();
                pharT05.MNC_DEPR_NO = dt.Rows[0]["MNC_DEPR_NO"].ToString();
                pharT05.MNC_SECR_NO = dt.Rows[0]["MNC_SECR_NO"].ToString();
                pharT05.MNC_SUM_PRI = dt.Rows[0]["MNC_SUM_PRI"].ToString();
                pharT05.MNC_SUM_COS = dt.Rows[0]["MNC_SUM_COS"].ToString();
                pharT05.MNC_HN_YR = dt.Rows[0]["MNC_HN_YR"].ToString();
                pharT05.MNC_HN_NO = dt.Rows[0]["MNC_HN_NO"].ToString();
                pharT05.MNC_AN_YR = dt.Rows[0]["MNC_AN_YR"].ToString();
                pharT05.MNC_AN_NO = dt.Rows[0]["MNC_AN_NO"].ToString();
                pharT05.MNC_PRE_NO = dt.Rows[0]["MNC_PRE_NO"].ToString();
                pharT05.MNC_DATE = dt.Rows[0]["MNC_DATE"].ToString();
                pharT05.MNC_TIME = dt.Rows[0]["MNC_TIME"].ToString();
                pharT05.MNC_USE_LOG = dt.Rows[0]["MNC_USE_LOG"].ToString();
                pharT05.MNC_FN_TYP_CD = dt.Rows[0]["MNC_FN_TYP_CD"].ToString();
                pharT05.MNC_COM_CD = dt.Rows[0]["MNC_COM_CD"].ToString();
                pharT05.MNC_FLAGAR = dt.Rows[0]["MNC_FLAGAR"].ToString();
                pharT05.MNC_REQ_REF = dt.Rows[0]["MNC_REQ_REF"].ToString();
                pharT05.MNC_CFR_TIME = dt.Rows[0]["MNC_CFR_TIME"].ToString();
                pharT05.MNC_EMPR_CD = dt.Rows[0]["MNC_EMPR_CD"].ToString();
                pharT05.MNC_EMPC_CD = dt.Rows[0]["MNC_EMPC_CD"].ToString();
                pharT05.MNC_ORD_DOT = dt.Rows[0]["MNC_ORD_DOT"].ToString();
                pharT05.MNC_QUE_NO = dt.Rows[0]["MNC_QUE_NO"].ToString();
                pharT05.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                pharT05.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                pharT05.MNC_PAC_CD = dt.Rows[0]["MNC_PAC_CD"].ToString();
                pharT05.MNC_PAC_TYP = dt.Rows[0]["MNC_PAC_TYP"].ToString();
                pharT05.MNC_PHA_STS = dt.Rows[0]["MNC_PHA_STS"].ToString();
                pharT05.MNC_REQ_COUNT = dt.Rows[0]["MNC_REQ_COUNT"].ToString();
                pharT05.MNC_REQ_TYP = dt.Rows[0]["MNC_REQ_TYP"].ToString();
            }
            else
            {
                setPharmacyT05(pharT05);
            }
            return pharT05;
        }
        public PharmacyT05 setPharmacyT05(PharmacyT05 p)
        {
            p.MNC_DOC_CD = "";
            p.MNC_CFR_YR = "";
            p.MNC_CFR_NO = "";
            p.MNC_CFG_DAT = "";
            p.MNC_CFR_STS = "";
            p.MNC_DEPC_NO = "";
            p.MNC_SECC_NO = "";
            p.MNC_REQ_YR = "";
            p.MNC_REQ_NO = "";
            p.MNC_REQ_DAT = "";
            p.MNC_REQ_TIM = "";
            p.MNC_DEPR_NO = "";
            p.MNC_SECR_NO = "";
            p.MNC_SUM_PRI = "";
            p.MNC_SUM_COS = "";
            p.MNC_HN_YR = "";
            p.MNC_HN_NO = "";
            p.MNC_AN_YR = "";
            p.MNC_AN_NO = "";
            p.MNC_PRE_NO = "";
            p.MNC_DATE = "";
            p.MNC_TIME = "";
            p.MNC_USE_LOG = "";
            p.MNC_FN_TYP_CD = "";
            p.MNC_COM_CD = "";
            p.MNC_FLAGAR = "";
            p.MNC_REQ_REF = "";
            p.MNC_CFR_TIME = "";
            p.MNC_EMPR_CD = "";
            p.MNC_EMPC_CD = "";
            p.MNC_ORD_DOT = "";
            p.MNC_QUE_NO = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_PAC_CD = "";
            p.MNC_PAC_TYP = "";
            p.MNC_PHA_STS = "";
            p.MNC_REQ_COUNT = "";
            p.MNC_REQ_TYP = "";
            return p;
        }
    }
}
