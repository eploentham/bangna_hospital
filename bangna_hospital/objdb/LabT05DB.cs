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
        public DataTable selectRequestLabNotPrnbyDeptNo(String deptcode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "SELECT TOP 1 labt01.MNC_REQ_NO,convert(VARCHAR(20),labt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,labt01.MNC_HN_NO " +
                ",isnull(labt01.MNC_AN_NO,'') as MNC_AN_NO,isnull(labt01.MNC_AN_YR,'') as MNC_AN_YR " +
                ", ptt01.MNC_VN_SEQ, ptt01.MNC_VN_SUM, ptt01.MNC_VN_NO " +
                "FROM  LAB_T01 labt01 " +
                "left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                "left join PATIENT_T01 ptt01 on labt01.MNC_HN_NO = ptt01.MNC_HN_NO and labt01.MNC_PRE_NO = ptt01.MNC_HN_NO and labt01.MNC_DATE = ptt01.MNC_DATE " +
                "left join LAB_m06 labm06 ON labt02.MNC_MNC_LB_GRP_CD = labm06.MNC_LB_GRP_CD " +
                "where labt01.status_print_result_no is null  " +      //ยังไม่ได้พิมพ์
                "and labt02.MNC_RESULT_DAT is not null " +              //ผลพึ่งออก
                "and labt02.mnc_req_sts <> 'C'  and labt01.mnc_req_sts <> 'C' " +
                "and labt01.MNC_REQ_DEP = '" + deptcode+"' " +     //pt08.MNC_SEC_NO = PATIENT_M32.MNC_SEC_NO and pt08.MNC_WD_NO = PATIENT_M32.MNC_MD_DEP_NO
                "and labm06.MNC_LB_GRP_CD != 'BL' " +               // Blood Bank ไม่ต้องพิมพ์
                "Order By labt02.MNC_REQ_DAT,labt02.MNC_REQ_NO,labt02.MNC_LB_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultbyReqNo(String reqdate, String reqno)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "SELECT labt02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, labt05.MNC_RES_VALUE, labt05.MNC_STS, labt05.MNC_RES, labt05.MNC_RES_UNT, labt05.MNC_LB_RES,convert(VARCHAR(20),labt05.mnc_req_dat,23) as mnc_req_dat, labt05.mnc_res" +
                ", labt05.mnc_req_no ,fn02.MNC_FN_TYP_DSC, labt01.mnc_req_yr,patient_m02.MNC_PFIX_DSC + ' ' + patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME as dtr_name" +
                ",convert(VARCHAR(20),labt05.MNC_STAMP_DAT) as MNC_STAMP_DAT, labt05.MNC_LB_USR, labt01.mnc_dot_cd,labt01.MNC_REQ_DEP,lab_m01.MNC_LB_DSC, lab_m06.MNC_LB_GRP_DSC, lab_m07.MNC_LB_TYP_DSC " +
                ", convert(VARCHAR(20),labt02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT,labt02.MNC_RESULT_TIM " +
                ",usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_result.MNC_USR_FULL as user_lab" +
                ",usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_report.MNC_USR_FULL as user_report" +
                ",usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve,usr_approve.MNC_USR_FULL as user_check" +
                ", '' as hostname " +
                "FROM  LAB_T01 labt01  " +
                "left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                "left join LAB_T05 labt05 ON labt01.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt05.MNC_REQ_DAT and labt02.MNC_LB_CD = labt05.MNC_LB_CD " +
                "left join LAB_M01 ON labt02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "inner join lab_m07 on lab_m01.MNC_LB_GRP_CD =lab_m07.MNC_LB_GRP_CD  AND lab_m01.MNC_LB_TYP_CD = lab_m07.MNC_LB_TYP_CD " +
                " inner join patient_m26 on labt01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                "Left Join finance_m02 fn02 on labt01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = labt02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = labt02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = labt02.mnc_usr_result_approve " +
                
                "where labt01.MNC_REQ_NO = '" + reqno + "'  " +
                "and labt01.MNC_REQ_DAT = '" + reqdate + "' " +
                "and labt02.mnc_req_sts <> 'C'  and labt01.mnc_req_sts <> 'C'" +
                "Order By labt05.MNC_REQ_DAT,labt05.MNC_REQ_NO,labt05.MNC_LB_CD,labt05.MNC_LB_RES_CD ";

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
