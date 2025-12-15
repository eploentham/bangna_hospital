using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class LabDB
    {
        public ConnectDB conn;
        public LabDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

        }
        public DataTable selectReqCovid(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "", wheredate="";
            DateTime dtreq = new DateTime();
            dtreq = DateTime.Now.AddDays(-5);
            wheredate = " and lcovidr.req_date >= '"+ dtreq.Year.ToString()+"-"+ dtreq.ToString("MM-dd")+ "' and lcovidr.req_date <= '"+ DateTime.Now.Year.ToString()+"-"+ DateTime.Now.ToString("MM-dd")+"' ";
            sql = "Select  lcovidr.mnc_hn_no,convert(VARCHAR(20),lcovidr.visit_date,23) visit_date,lcovidr.lab_code,convert(VARCHAR(20),lcovidr.req_date,23) as req_date,lcovidr.pre_no " +
                "From  t_lab_covid_request lcovidr  " +
                " Where lcovidr.mnc_hn_no = '"+ hn + "'  " + wheredate+" "+
                "Order By lcovidr.visit_date ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectResCovid(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "", wheredate = "";
            DateTime dtreq = new DateTime();
            dtreq = DateTime.Now.AddDays(-5);
            wheredate = " and lcovidd.req_date >= '" + dtreq.Year.ToString() + "-" + dtreq.ToString("MM-dd") + "' and lcovidd.req_date <= '" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd") + "' ";
            sql = "Select  lcovidd.* " +
                "From  t_lab_covid_detected lcovidd  " +
                " Where lcovidd.mnc_hn_no = '" + hn + "'  " + wheredate + " " +
                "Order By lcovidd.visit_date ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectLabAll()
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select  lab01.mnc_lb_cd, lab01.mnc_lb_dsc, lab01.mnc_lb_typ_cd, lab01.mnc_lb_grp_cd,lab_m06.MNC_LB_GRP_DSC " +
                ",lab01.MNC_LB_GRP_CD,lab_m07.MNC_LB_TYP_DSC " +
                "From  lab_m01 lab01  " +
                "inner join LAB_M06 on lab01.MNC_LB_GRP_CD = LAB_M06.MNC_LB_GRP_CD " +
                "inner join LAB_M07 on lab01.MNC_LB_TYP_CD= LAB_M07.MNC_LB_TYP_CD and lab01.MNC_LB_GRP_CD = LAB_M07.MNC_LB_GRP_CD " +

                " Where lab01.MNC_lb_STS = 'Y'  " +
                "Order By lab01.mnc_lb_cd";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectLabByCode(String code)
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select  lab01.mnc_lb_cd, lab01.mnc_lb_dsc, lab01.mnc_lb_typ_cd, lab01.mnc_lb_grp_cd " +
                "From  lab_m01 lab01  " +

                " Where lab01.MNC_lb_STS = 'Y' and lab01.mnc_lb_cd = '"+code+"' " +
                "Order By lab01.mnc_lb_cd";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable SelectHnLabOut(String datereq1, String datereq2, String labcode)
        {
            DataTable dt = new DataTable();
            String year = "";
            int year2 = 0;
            year = datereq1.Substring(0, 4);
            int.TryParse(year, out year2);
            year = (year2 + 543).ToString();
            String sql = "Select lt01.mnc_patname,lt01.mnc_pre_no,lt01.mnc_hn_no,lt01.mnc_req_no,convert(VARCHAR(20),lt01.mnc_req_dat,23) as mnc_req_dat,lt01.MNC_AN_NO, lt01.MNC_AN_YR " +
                ", ptt01.mnc_vn_seq, ptt01.mnc_vn_sum, ptt01.mnc_vn_no " +
                "From Lab_t01 lt01 " +
                "inner join patient_t01 ptt01 on ptt01.mnc_hn_no = lt01.mnc_hn_no and ptt01.mnc_pre_no = lt01.mnc_pre_no and ptt01.mnc_hn_yr = lt01.mnc_hn_yr " +
                "inner join LAB_T02 lt02 ON lt01.MNC_REQ_NO = lt02.MNC_REQ_NO AND lt01.MNC_REQ_DAT = lt02.MNC_REQ_DAT " +
                "Where lt01.mnc_req_yr = '" + year + "' and lt01.mnc_req_dat >= '" + datereq1 + "' and lt01.mnc_req_dat <= '" + datereq2 + "' and lt02.MNC_LB_CD in ("+ labcode + ")";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable selectResultLabByHnOPBKK(String date, String labcode)
        {
            String sql = "";
            DataTable dt = new DataTable();

            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX,usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve" +
                ", t01.mnc_hn_no, t01.MNC_ID_Nam, t01.mnc_pre_no,lt01.mnc_patname " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no  and t01.mnc_hn_yr = LAB_T01.mnc_hn_yr " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "where t01.MNC_DATE = '" + date + "' " +
                //" and t01.mnc_hn_no = '" + hn + "' and t01.mnc_hn_yr = '" + hnyear + "' " +
                //" and t01.mnc_pre_no = '" + preno + "' " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' and lt02.MNC_LB_CD in (" + labcode + ") " +
                "Order By t01.mnc_hn_no,lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        
    }
}
