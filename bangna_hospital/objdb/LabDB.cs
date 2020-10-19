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
    }
}
