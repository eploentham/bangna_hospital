using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LabT05DB
    {
        public LabT05 labT05;
        ConnectDB conn;
        public LabT05DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labT05 = new LabT05();
            labT05.MNC_REQ_YR = "MNC_REQ_YR";
            labT05.MNC_REQ_NO = "MNC_REQ_NO";
            labT05.MNC_REQ_DAT = "MNC_REQ_DAT";
            labT05.MNC_LB_CD = "MNC_LB_CD";
            labT05.MNC_LB_RES_CD = "MNC_LB_RES_CD";
            labT05.MNC_LB_USR = "MNC_LB_USR";
            labT05.MNC_RES = "MNC_RES";
            labT05.MNC_RES_MAX = "MNC_RES_MAX";
            labT05.MNC_RES_MIN = "MNC_RES_MIN";
            labT05.MNC_RES_VALUE = "MNC_RES_VALUE";
            labT05.MNC_LB_RES = "MNC_LB_RES";
            labT05.MNC_LB_ACT = "MNC_LB_ACT";
            labT05.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            labT05.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            labT05.MNC_STS = "MNC_STS";
            labT05.MNC_LAB_PRN = "MNC_LAB_PRN";
            labT05.MNC_RES_UNT = "MNC_RES_UNT";

            labT05.table = "lab_t05";

        }
        public DataTable selectResultDate(String reqdate)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "SELECT labt01.MNC_REQ_NO,convert(VARCHAR(20),labt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,labt01.MNC_HN_NO,labt01.MNC_REQ_TIM,labt01.MNC_REQ_DEP " +
                ",isnull(labt01.MNC_AN_NO,'') as MNC_AN_NO,isnull(labt01.MNC_AN_YR,'') as MNC_AN_YR " +
                ", ptt01.MNC_VN_SEQ, ptt01.MNC_VN_SUM, ptt01.MNC_VN_NO " +
                ", pm02.MNC_PFIX_DSC + ' '+ pm01.MNC_FNAME_T + ' ' + pm01.MNC_LNAME_T as pttfullnamet,pm01.MNC_SEX,labt01.MNC_FN_TYP_CD " +
                ", userm01dtr.MNC_USR_FULL as dtrname,isnull(labt01.MNC_EMPC_CD,'') as MNC_EMPC_CD " +
                "FROM  LAB_T01 labt01 " +
                "left join PATIENT_T01 ptt01 on labt01.MNC_HN_NO = ptt01.MNC_HN_NO and labt01.MNC_PRE_NO = ptt01.MNC_PRE_NO and labt01.MNC_DATE = ptt01.MNC_DATE " +
                
                "inner join patient_m01 pm01 on labt01.mnc_hn_no = pm01.mnc_hn_no and labt01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "Left Join patient_m02 pm02 On pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Left Join USERLOG_M01 userm01dtr on labt01.MNC_ORD_DOT = userm01dtr.MNC_USR_NAME " +
                "where labt02.MNC_RESULT_DAT is not null " +              //ผลพึ่งออก
                "and labt02.mnc_req_sts = 'O'  " +                      //O  comfirm เรียบร้อย ยังไม่พิมพ์  mnc_req_sts เป็นค่าว่าง = request นั้น รอ รับทำการ,A=รับทำการแล้ว,O=confirm เรียบร้อย, Q=comfirm เรียบร้อย พิมพ์เรียบร้อย
                "and labt01.mnc_req_sts <> 'C' " +                          //Q
                "and labt01.MNC_REQ_DAT = '" + reqdate + "' " +     //pt08.MNC_SEC_NO = PATIENT_M32.MNC_SEC_NO and pt08.MNC_WD_NO = PATIENT_M32.MNC_MD_DEP_NO
                "and labm01.MNC_LB_GRP_CD != 'BL' " +               // Blood Bank ไม่ต้องพิมพ์
                "Order By labt02.MNC_REQ_DAT,labt02.MNC_REQ_NO,labt02.MNC_LB_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectRequestLabNotPrnbyDeptNo(String deptcode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "SELECT TOP 1 labt01.MNC_REQ_NO,convert(VARCHAR(20),labt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,labt01.MNC_HN_NO " +
                ",isnull(labt01.MNC_AN_NO,'') as MNC_AN_NO,isnull(labt01.MNC_AN_YR,'') as MNC_AN_YR " +
                ", ptt01.MNC_VN_SEQ, ptt01.MNC_VN_SUM, ptt01.MNC_VN_NO " +
                "FROM  LAB_T01 labt01 " +
                "left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                "left join PATIENT_T01 ptt01 on labt01.MNC_HN_NO = ptt01.MNC_HN_NO and labt01.MNC_PRE_NO = ptt01.MNC_PRE_NO and labt01.MNC_DATE = ptt01.MNC_DATE " +
                "left join LAB_M01 labm01 on labt02.MNC_LB_CD = labm01.MNC_LB_CD " +
                //"left join LAB_m06 labm06 ON labm01.MNC_MNC_LB_GRP_CD = labm06.MNC_LB_GRP_CD " +
                "where labt01.status_print_result_no is null  " +      //ยังไม่ได้พิมพ์
                "and labt02.MNC_RESULT_DAT is not null " +              //ผลพึ่งออก
                "and labt02.mnc_req_sts = 'O'  " +                      //O  comfirm เรียบร้อย ยังไม่พิมพ์  mnc_req_sts เป็นค่าว่าง = request นั้น รอ รับทำการ,A=รับทำการแล้ว,O=confirm เรียบร้อย, Q=comfirm เรียบร้อย พิมพ์เรียบร้อย
                "and labt01.mnc_req_sts <> 'C' " +                       // ให้ดึงที่เป็น O
                "and labt01.MNC_REQ_DEP = '" + deptcode+"' " +     //pt08.MNC_SEC_NO = PATIENT_M32.MNC_SEC_NO and pt08.MNC_WD_NO = PATIENT_M32.MNC_MD_DEP_NO
                "and labm01.MNC_LB_GRP_CD != 'BL' " +               // Blood Bank ไม่ต้องพิมพ์
                "Order By labt02.MNC_REQ_DAT,labt02.MNC_REQ_NO,labt02.MNC_LB_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultbyReqNo(String reqdate, String reqno)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "SELECT labt02.MNC_LB_CD, labm01.MNC_LB_DSC as mnc_lb_dsc, labt05.MNC_RES_VALUE, labt05.MNC_STS, labt05.MNC_RES, labt05.MNC_RES_UNT, labt05.MNC_LB_RES as mnc_lb_res,convert(VARCHAR(20),labt05.mnc_req_dat,23) as mnc_req_dat " +
                ", labt05.mnc_req_no ,fn02.MNC_FN_TYP_DSC, labt01.mnc_req_yr,isnull(patient_m02.MNC_PFIX_DSC,'') + ' ' + isnull(patient_m26.MNC_DOT_FNAME,'') + ' ' + isnull(patient_m26.MNC_DOT_LNAME,'') as dtr_name" +
                ",convert(VARCHAR(20),labt05.MNC_STAMP_DAT) as MNC_STAMP_DAT,labt05.MNC_STAMP_TIM" +
                ", labt05.MNC_LB_USR, isnull(labt01.mnc_dot_cd,'') as mnc_dot_cd,isnull(labt01.MNC_REQ_DEP,'') as MNC_REQ_DEP, labm06.MNC_LB_GRP_DSC, lab_m07.MNC_LB_TYP_DSC " +
                ", convert(VARCHAR(20),labt02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT,labt02.MNC_RESULT_TIM " +
                ",usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_result.MNC_USR_FULL as user_lab" +
                ",usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_report.MNC_USR_FULL as user_report" +
                ",usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve,usr_approve.MNC_USR_FULL as user_check" +
                ", '' as hostname,'' as mnc_lb_grp_cd " +
                ",isnull(labt01.MNC_AN_NO,'') as MNC_AN_NO,isnull(labt01.MNC_AN_YR,'') as MNC_AN_YR,labt05.MNC_LB_RES_CD " +
                "FROM  LAB_T01 labt01  " +
                "left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                "left join LAB_T05 labt05 ON labt01.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt05.MNC_REQ_DAT and labt02.MNC_LB_CD = labt05.MNC_LB_CD " +
                "left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD " +
                "left join lab_m06 labm06 on labm01.MNC_LB_GRP_CD = labm06.MNC_LB_GRP_CD " +
                "inner join lab_m07 on labm01.MNC_LB_GRP_CD =lab_m07.MNC_LB_GRP_CD  AND labm01.MNC_LB_TYP_CD = lab_m07.MNC_LB_TYP_CD " +
                " inner join patient_m26 on labt01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                "Left Join finance_m02 fn02 on labt01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = labt02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = labt02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = labt02.mnc_usr_result_approve " +
                "where labt01.MNC_REQ_NO = '" + reqno + "'  " +
                "and labt01.MNC_REQ_DAT = '" + reqdate + "' " +
                "and labt02.mnc_req_sts <> 'C'  and labt01.mnc_req_sts <> 'C' " +
                "and labm01.status_outlab <> '1' " + //outlab ไม่ต้องพิมพ์
                "Order By labt05.MNC_REQ_DAT,labt05.MNC_REQ_NO,labt05.MNC_LB_CD,convert(int,labt05.MNC_LB_RES_CD) ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultbyLabCode(String reqdate, String reqno, String labcode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "SELECT labt02.MNC_LB_CD, labm01.MNC_LB_DSC as mnc_lb_dsc, labt05.MNC_RES_VALUE, labt05.MNC_STS, labt05.MNC_RES, labt05.MNC_RES_UNT, labt05.MNC_LB_RES as mnc_lb_res" +
                ", convert(VARCHAR(20),labt05.mnc_req_dat,23) as mnc_req_dat " +
                ", labt05.mnc_req_no ,fn02.MNC_FN_TYP_DSC, labt01.mnc_req_yr" +
                ", isnull(patient_m02.MNC_PFIX_DSC,'') + ' ' + isnull(patient_m26.MNC_DOT_FNAME,'') + ' ' + isnull(patient_m26.MNC_DOT_LNAME,'') as dtr_name" +
                ", convert(VARCHAR(20), labt05.MNC_STAMP_DAT) as MNC_STAMP_DAT, labt05.MNC_STAMP_TIM" +
                ", labt05.MNC_LB_USR, isnull(labt01.mnc_dot_cd,'') as mnc_dot_cd, isnull(labt01.MNC_REQ_DEP,'') as MNC_REQ_DEP " +
                ", convert(VARCHAR(20), labt02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT, labt02.MNC_RESULT_TIM " +
                //",usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_result.MNC_USR_FULL as user_lab" +
                //",usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_report.MNC_USR_FULL as user_report" +
                ///",usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve,usr_approve.MNC_USR_FULL as user_check" +
                ", '' as hostname,'' as mnc_lb_grp_cd " +
                ",isnull(labt01.MNC_AN_NO,'') as MNC_AN_NO,isnull(labt01.MNC_AN_YR,'') as MNC_AN_YR,labt05.MNC_LB_RES_CD " +
                "FROM  LAB_T01 labt01  " +
                "left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                "left join LAB_T05 labt05 ON labt01.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt05.MNC_REQ_DAT and labt02.MNC_LB_CD = labt05.MNC_LB_CD " +
                "left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD " +
                //"left join lab_m06 labm06 on labm01.MNC_LB_GRP_CD = labm06.MNC_LB_GRP_CD " +
                //"inner join lab_m07 on labm01.MNC_LB_GRP_CD =lab_m07.MNC_LB_GRP_CD  AND labm01.MNC_LB_TYP_CD = lab_m07.MNC_LB_TYP_CD " +
                //" inner join patient_m26 on labt01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                //"Left Join finance_m02 fn02 on labt01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +
                //"left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = labt02.mnc_usr_result " +
                //"left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = labt02.mnc_usr_result_report " +
                //"left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = labt02.mnc_usr_result_approve " +
                "where labt01.MNC_REQ_NO = '" + reqno + "'  " +
                "and labt01.MNC_REQ_DAT = '" + reqdate + "' " +
                "and labt02.mnc_req_sts <> 'C'  and labt01.mnc_req_sts <> 'C' " +
                "and labm01.status_outlab <> '1' " + //outlab ไม่ต้องพิมพ์
                "and labt05.MNC_LB_CD = '" + labcode + "' " +
                "Order By labt05.MNC_REQ_DAT,labt05.MNC_REQ_NO,labt05.MNC_LB_CD,convert(int,labt05.MNC_LB_RES_CD) ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultCheckSSObyLabCode(String hn, String labcode)
        {
            String sql = "", reqdate="";
            DataTable dt = new DataTable();
            reqdate = DateTime.Now.Year.ToString()+DateTime.Now.ToString("-MM-dd");
            sql = "SELECT labt02.MNC_LB_CD, labm01.MNC_LB_DSC as mnc_lb_dsc, labt05.MNC_RES_VALUE, labt05.MNC_STS, labt05.MNC_RES, labt05.MNC_RES_UNT, labt05.MNC_LB_RES as mnc_lb_res" +
                ", convert(VARCHAR(20),labt05.mnc_req_dat,23) as mnc_req_dat " +
                ", labt05.mnc_req_no , labt01.mnc_req_yr" +
                //", isnull(patient_m02.MNC_PFIX_DSC,'') + ' ' + isnull(patient_m26.MNC_DOT_FNAME,'') + ' ' + isnull(patient_m26.MNC_DOT_LNAME,'') as dtr_name" +
                ", convert(VARCHAR(20), labt05.MNC_STAMP_DAT) as MNC_STAMP_DAT, labt05.MNC_STAMP_TIM" +
                ", labt05.MNC_LB_USR, isnull(labt01.mnc_dot_cd,'') as mnc_dot_cd, isnull(labt01.MNC_REQ_DEP,'') as MNC_REQ_DEP " +
                ", convert(VARCHAR(20), labt02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT, labt02.MNC_RESULT_TIM " +
                ", '' as hostname,'' as mnc_lb_grp_cd " +
                ",isnull(labt01.MNC_AN_NO,'') as MNC_AN_NO,isnull(labt01.MNC_AN_YR,'') as MNC_AN_YR,labt05.MNC_LB_RES_CD " +
                "FROM  LAB_T01 labt01  " +
                "left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                "left join LAB_T05 labt05 ON labt01.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt05.MNC_REQ_DAT and labt02.MNC_LB_CD = labt05.MNC_LB_CD " +
                "left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD " +
                //" inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                "where labt01.MNC_HN_NO = '" + hn + "'  " +
                "and labt01.MNC_REQ_DAT = '" + reqdate + "' " +
                "and labt02.mnc_req_sts <> 'C'  and labt01.mnc_req_sts <> 'C' " +
                "and labm01.status_outlab <> '1' " + //outlab ไม่ต้องพิมพ์
                "and labt05.MNC_LB_CD = '" + labcode + "' " +
                "Order By labt05.MNC_REQ_DAT,labt05.MNC_REQ_NO,labt05.MNC_LB_CD,convert(int,labt05.MNC_LB_RES_CD) ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectRequestByHn(String hn)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "SELECT convert(varchar(20), labt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,labt01.MNC_REQ_TIM, labt01.MNC_REQ_NO, labt01.status_lis, labt01.status_request_lab,labt02.MNC_LB_CD  " +
                ", labt01.MNC_REQ_YR,labt01.MNC_HN_NO,isnull(labt01.MNC_AN_NO,0) as MNC_AN_NO,isnull(labt01.MNC_AN_yr,0) as MNC_AN_yr,labt01.MNC_REQ_DEP  " +
                ", pm02.MNC_PFIX_DSC, pm01.MNC_FNAME_T, pm01.MNC_LNAME_T, convert(varchar(20), pm01.MNC_BDAY,23) as MNC_BDAY, pm01.MNC_SEX " +
                ", pt01.MNC_VN_NO,pt01.MNC_VN_SEQ,pt01.MNC_VN_SUM,isnull(labt01.MNC_ORD_DOT,'') as MNC_ORD_DOT,isnull(pt01.mnc_sec_no,'') as mnc_sec_no, userm01.MNC_USR_FULL " +
                ",isnuLL(labt01.MNC_WD_NO,'') as MNC_WD_NO,isnull(labt01.MNC_EMPC_CD,'') as MNC_EMPC_CD, isnull(userm01_usr.MNC_USR_FULL,'') as MNC_USR_FULL_usr " +
                ",labt01.status_request_lab,labt01.date_request_lab, labt01.api_database,labt02.status_request_lab,labt02.date_request_lab, labt02.api_database, labm01.MNC_LB_DSC " +
                "From lab_t01 labt01  " +
                "inner join lab_t02 labt02 on labt01.MNC_REQ_YR = labt02.MNC_REQ_YR and labt01.MNC_REQ_NO = labt02.MNC_REQ_NO and labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                "inner join patient_m01 pm01 on labt01.mnc_hn_no = pm01.mnc_hn_no and labt01.mnc_hn_yr = pm01.mnc_hn_yr  " +
                "inner join patient_t01 pt01 on pt01.mnc_hn_no = labt01.mnc_hn_no and pt01.mnc_hn_yr = labt01.mnc_hn_yr and pt01.MNC_PRE_NO = labt01.MNC_PRE_NO and pt01.MNC_DATE = labt01.MNC_DATE  " +
                "Left Join patient_m02 pm02 On pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD  " +
                "Left Join USERLOG_M01 userm01 on labt01.MNC_ORD_DOT = userm01.MNC_USR_NAME  " +
                "left Join USERLOG_M01 userm01_usr on labt01.MNC_EMPC_CD = userm01_usr.MNC_USR_NAME  " +
                "left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD " +
                "where labt01.MNC_HN_NO = '" + hn + "'  " +
                "Order By labt01.MNC_REQ_DAT,labt02.MNC_REQ_NO,labt02.MNC_LB_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectbyLabCode(String reqdate, String reqdate1, String hn, String labcode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            //reqdate = DateTime.Now.Year.ToString() + DateTime.Now.ToString("-MM-dd");
            sql = "SELECT labt02.MNC_LB_CD, labm01.MNC_LB_DSC as mnc_lb_dsc " +
                ", convert(VARCHAR(20),labt01.mnc_req_dat,23) as mnc_req_dat " +
                " , labt01.mnc_req_yr" +
                ", isnull(labt01.mnc_dot_cd,'') as mnc_dot_cd, isnull(labt01.MNC_REQ_DEP,'') as MNC_REQ_DEP " +
                ", convert(VARCHAR(20), labt02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT, labt02.MNC_RESULT_TIM " +
                //", '' as hostname,'' as mnc_lb_grp_cd " +
                ",isnull(labt01.MNC_AN_NO,'') as MNC_AN_NO,isnull(labt01.MNC_AN_YR,'') as MNC_AN_YR, isnull(userm01.MNC_USR_FULL,'') as MNC_USR_FULL_usr " +
                "FROM  LAB_T01 labt01  " +
                "left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                "left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD " +
                 "Left Join USERLOG_M01 userm01 on labt01.MNC_ORD_DOT = userm01.MNC_USR_NAME  " +
                "where labt01.MNC_HN_NO = '" + hn + "'  " +
                "and labt01.MNC_REQ_DAT >= '" + reqdate + "' and labt01.MNC_REQ_DAT <= '" + reqdate1 + "' and labt02.MNC_LB_CD = '" +labcode+ "' " +
                "and labt02.mnc_req_sts <> 'C'  and labt01.mnc_req_sts <> 'C' " +

                "Order By labt01.MNC_REQ_DAT,labt01.MNC_REQ_NO ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selecCnttbyHn(String reqdate, String reqdate1, String hn, String labcode)
        {
            String sql = "", wherelabcode="";
            if(labcode.Length > 0)
            {
                wherelabcode = " and labt02.MNC_LB_CD = '" + labcode + "' ";
            }
            DataTable dt = new DataTable();
            //reqdate = DateTime.Now.Year.ToString() + DateTime.Now.ToString("-MM-dd");
            sql = "SELECT labt02.MNC_LB_CD, labm01.MNC_LB_DSC  " +
                ", count(1) as cnt, isnull(labm02.MNC_LB_PRI01,0) as MNC_LB_PRI01 " +
                " " +
                "FROM  LAB_T01 labt01  " +
                "left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                "left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD " +
                "left join LAB_M02 labm02 ON labM02.MNC_LB_CD = labm01.MNC_LB_CD " +
                "where labt01.MNC_HN_NO = '" + hn + "'  " +
                "and labt01.MNC_REQ_DAT >= '" + reqdate + "' and labt01.MNC_REQ_DAT <= '" + reqdate1 + "' " +
                "and labt02.mnc_req_sts <> 'C'  and labt01.mnc_req_sts <> 'C' and labt02.MNC_RESULT_DAT is not null " + wherelabcode+
                "Group By labt02.MNC_LB_CD, labm01.MNC_LB_DSC,labm02.MNC_LB_PRI01 " +
                "Order By labm01.MNC_LB_DSC ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selecControltbyHn(String reqdate, String reqdate1, String hn, String labcode)
        {
            String sql = "", wherelabcode = "";
            if (labcode.Length > 0)
            {
                wherelabcode = " and labt02.MNC_LB_CD = '" + labcode + "' ";
            }
            DataTable dt = new DataTable();
            //reqdate = DateTime.Now.Year.ToString() + DateTime.Now.ToString("-MM-dd");
            sql = "SELECT labt02.MNC_LB_CD, labm01.MNC_LB_DSC  " +
                ", isnull(labm02.MNC_LB_PRI01,0) as MNC_LB_PRI01,convert(varchar(20),labt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,labt02.MNC_REQ_NO " +
                " " +
                "FROM  LAB_T01 labt01  " +
                "left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                "left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD " +
                "left join LAB_M02 labm02 ON labM02.MNC_LB_CD = labm01.MNC_LB_CD " +
                "where labt01.MNC_HN_NO = '" + hn + "'  " +
                "and labt01.MNC_REQ_DAT >= '" + reqdate + "' and labt01.MNC_REQ_DAT <= '" + reqdate1 + "' " +
                "and labt02.mnc_req_sts <> 'C'  and labt01.mnc_req_sts <> 'C' and labt02.MNC_RESULT_DAT is not null " + wherelabcode +
                //"Group By labt02.MNC_LB_CD, labm01.MNC_LB_DSC,labm02.MNC_LB_PRI01 " +
                "Order By labm01.MNC_LB_DSC ";

            dt = conn.selectData(sql);
            return dt;
        }
        private void chkNull(LabT05 p)
        {
            long chk = 0;
            int chk1 = 0;

            p.MNC_REQ_YR = p.MNC_REQ_YR == null ? "" : p.MNC_REQ_YR;
            p.MNC_REQ_NO = p.MNC_REQ_NO == null ? "" : p.MNC_REQ_NO;
            p.MNC_REQ_DAT = p.MNC_REQ_DAT == null ? "" : p.MNC_REQ_DAT;
            p.MNC_LB_CD = p.MNC_LB_CD == null ? "" : p.MNC_LB_CD;
            p.MNC_LB_RES_CD = p.MNC_LB_RES_CD == null ? "" : p.MNC_LB_RES_CD;
            p.MNC_LB_USR = p.MNC_LB_USR == null ? "" : p.MNC_LB_USR;
            p.MNC_RES = p.MNC_RES == null ? "" : p.MNC_RES;
            p.MNC_RES_MAX = p.MNC_RES_MAX == null ? "" : p.MNC_RES_MAX;
            p.MNC_RES_MIN = p.MNC_RES_MIN == null ? "" : p.MNC_RES_MIN;
            p.MNC_RES_VALUE = p.MNC_RES_VALUE == null ? "" : p.MNC_RES_VALUE;
            p.MNC_LB_RES = p.MNC_LB_RES == null ? "" : p.MNC_LB_RES;
            p.MNC_STAMP_DAT = p.MNC_STAMP_DAT == null ? "" : p.MNC_STAMP_DAT;
            p.MNC_STAMP_TIM = p.MNC_STAMP_TIM == null ? "" : p.MNC_STAMP_TIM;
            p.MNC_STS = p.MNC_STS == null ? "" : p.MNC_STS;
            p.MNC_LB_ACT = p.MNC_LB_ACT == null ? "" : p.MNC_LB_ACT;
        }
        public String insertLabT05(LabT05 p)
        {
            String sql = "", chk = "", re = "";

            chkNull(p);
            if (p.MNC_REQ_NO.Equals("0"))
            {
                re = insertLabT05(p, "");
            }
            else
            {

            }
            return re;
        }
        public String deleteLabT05(String reqyr, String reqno, String reqdate, String lab_code, String lab_sub_code)
        {
            String sql = "", chk = "", re = "";
            try
            {
                sql = "Delete From lab_t05  "
                    + "Where MNC_REQ_YR = '" + reqyr + "' and MNC_REQ_DAT = '" + reqdate + "' and MNC_REQ_NO = '"+ reqno+ "' and MNC_LB_RES_CD = '"+ lab_sub_code + "' and MNC_LB_CD = '"+ lab_code+"' ";
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "insert_lab_t05 " + ex.Message + " " + ex.InnerException);
            }
            return re;
        }
        public String insertLabT05(LabT05 p, String userid)
        {
            String sql = "", chk = "", re = "";
            try
            {
                sql = "insert into lab_t05 ("+labT05.MNC_REQ_YR+","+labT05.MNC_REQ_NO+","+labT05.MNC_REQ_DAT+","+labT05.MNC_LB_CD+"," 
                    +labT05.MNC_LB_RES_CD+","+labT05.MNC_RES+","+labT05.MNC_RES_VALUE+","+labT05.MNC_RES_UNT+","
                    +labT05.MNC_RES_MAX+","+labT05.MNC_RES_MIN+","+labT05.MNC_LB_USR+","+labT05.MNC_LB_ACT+","
                    +labT05.MNC_LAB_PRN+","+labT05.MNC_STAMP_DAT+","+labT05.MNC_STAMP_TIM+","+labT05.MNC_STS+",status_insert_pop"+
                    "," + labT05.MNC_LB_RES + ") "
                    +"values('"+p.MNC_REQ_YR+"','"+p.MNC_REQ_NO+"','"+p.MNC_REQ_DAT+"','"+p.MNC_LB_CD+"','"
                    +p.MNC_LB_RES_CD+"','"+p.MNC_RES+"','"+p.MNC_RES_VALUE+"','"+p.MNC_RES_UNT+"','"
                    +p.MNC_RES_MAX+"','"+p.MNC_RES_MIN+"','"+p.MNC_LB_USR+"','"+p.MNC_LB_ACT+"','"
                    +p.MNC_LAB_PRN+"','"+p.MNC_STAMP_DAT+"','"+p.MNC_STAMP_TIM+"','"+p.MNC_STS+"','1'" +
                    ",'" + p.MNC_LB_RES+"')";
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch(Exception ex)
            {
                new LogWriter("e", "insert_lab_t05 " + ex.Message+" "+ ex.InnerException);
            }
            return re;
        }
    }
}
