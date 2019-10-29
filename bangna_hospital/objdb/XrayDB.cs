using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class XrayDB
    {
        public ConnectDB conn;

        public XrayDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

        }
        public DataTable selectVisitStatusPacsReqByDate(String Date)
        {
            DataTable dt = new DataTable();
            String sql = "";
            String[] aa = Date.Split('/');
            //sql = "Select  xr01.*, xr01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
            //    "ptt01.MNC_FNAME_T,ptt01.MNC_LNAME_T,ptt01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, t01.mnc_vn_no, t01.mnc_vn_seq, t01.mnc_vn_sum " +
            //    ",ptt01.mnc_bday, ptt01.mnc_sex, t01.mnc_shif_memo " +
            //    "From  xray_t01 xr01  " +
            //    "Left Join patient_m01 ptt01 on xr01.mnc_hn_no = ptt01.mnc_hn_no and xr01.mnc_hn_yr = ptt01.mnc_hn_yr " +
            //    "Left join patient_m02 m02 on ptt01.MNC_PFIX_CDT = m02.MNC_PFIX_CD " +
            //    " inner join patient_t01 t01 on xr01.MNC_HN_NO = t01.MNC_HN_NO " +
            //    " Where xr01.MNC_req_dat = '" + Date + "' and t01.mnc_sts <> 'C' and xr01.mnc_req_sts <> 'A' and xr01.status_pacs <> '1' ";
            //sql = "Select  xr01.*, xr01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
            //    "ptt01.MNC_FNAME_T,ptt01.MNC_LNAME_T,ptt01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, t01.mnc_vn_no, t01.mnc_vn_seq, t01.mnc_vn_sum " +
            //    ",ptt01.mnc_bday, ptt01.mnc_sex, t01.mnc_shif_memo " +
            //    "From  xray_t01 xr01  " +
            //    "Left Join patient_m01 ptt01 on xr01.mnc_hn_no = ptt01.mnc_hn_no and xr01.mnc_hn_yr = ptt01.mnc_hn_yr " +
            //    "Left join patient_m02 m02 on ptt01.MNC_PFIX_CDT = m02.MNC_PFIX_CD " +
            //    " inner join patient_t01 t01 on xr01.MNC_HN_NO = t01.MNC_HN_NO  and xr01.mnc_hn_yr = t01.mnc_hn_yr " +
            //    " Where t01.MNC_date = '" + Date + "' and t01.mnc_sts <> 'C' and xr01.status_pacs = '0' " +
            //    "Order By xr01.mnc_req_dat, xr01.mnc_req_tim";
            sql = "Select  xr01.*, xr01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,ptt01.MNC_VN_NO,ptt01.MNC_VN_SEQ,ptt01.MNC_VN_SUM, ptt01.mnc_vn_no, ptt01.mnc_vn_seq, m01.mnc_vn_sum " +
                ",m01.mnc_bday, m01.mnc_sex, ptt01.mnc_shif_memo,xt02.MNC_XR_CD, xm01.MNC_XR_DSC " +
                "From  xray_t01 xr01  " +
                "inner Join patient_m01 m01  on xr01 .mnc_hn_no = m01.mnc_hn_no  " +
                "inner join patient_m02 m02 on m01.MNC_PFIX_CDT = m02.MNC_PFIX_CD " +
                "inner join PATIENT_T01 ptt01 on xr01 .mnc_hn_no = ptt01.mnc_hn_no and xr01.MNC_DATE = ptt01.MNC_DATE and xr01 .MNC_PRE_NO = ptt01.MNC_PRE_NO  " +
                "Left Join xray_t02 xt02 on xr01.MNC_REQ_NO = xt02.MNC_REQ_NO and xr01.MNC_REQ_YR = xt02.MNC_REQ_YR and xr01.MNC_REQ_DAT = xt02.MNC_REQ_DAT " +
                "Left Join xray_m01 xm01 on xm01.MNC_XR_CD = xt02.MNC_XR_CD " +
                " Where xr01.MNC_REQ_DAT = '" + Date + "' and xr01.MNC_REQ_STS = 'A' and xr01.status_pacs = '0' " +
                "Order By xr01.mnc_req_dat, xr01.mnc_req_tim";
            dt = conn.selectData(sql);
            
            return dt;
        }
        public DataTable selectVisitStatusPacsProcessByDate(String Date)
        {
            DataTable dt = new DataTable();
            String sql = "";
            String[] aa = Date.Split('/');
            sql = "Select  xr01.*, xr01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "ptt01.MNC_FNAME_T,ptt01.MNC_LNAME_T,ptt01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, t01.mnc_vn_no, t01.mnc_vn_seq, t01.mnc_vn_sum " +
                "From  xray_t01 xr01  " +
                "Left Join patient_m01 ptt01 on xr01.mnc_hn_no = ptt01.mnc_hn_no and xr01.mnc_hn_yr = ptt01.mnc_hn_yr " +
                "Left join patient_m02 m02 on ptt01.MNC_PFIX_CDT = m02.MNC_PFIX_CD " +
                " inner join patient_t01 t01 on xr01.MNC_HN_NO = t01.MNC_HN_NO " +
                " Where xr01.MNC_req_dat = '" + Date + "' and t01.mnc_sts <> 'C' and xr01.mnc_req_sts <> 'C' and xr01.status_pacs = '1' ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitStatusPacsFinishByDate(String Date)
        {
            DataTable dt = new DataTable();
            String sql = "";
            String[] aa = Date.Split('/');
            sql = "Select  xr01.*, xr01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, t01.mnc_vn_no, t01.mnc_vn_seq, t01.mnc_vn_sum " +
                "From  xray_t01 xr01 on t01.mnc_dot_cd = m01.MNC_DOT_CD " +
                "Left Join patient_m01 ptt01 on xr01.mnc_hn_no = ptt01.mnc_hn_no and xr01.mnc_hn_yr = ptt01.mnc_hn_yr " +
                "Left join patient_m02 m02 on ptt01.MNC_DOT_PFIX = m02.MNC_PFIX_CD " +
                " inner join patient_t01 t01 on xr01.MNC_HN_NO = t01.MNC_HN_NO " +
                " Where t01.MNC_req_date = '" + Date + "' and t01.mnc_sts <> 'C' and xr01.mnc_req_sts <> 'C' and xr01.status_pacs = '2' ";
            dt = conn.selectData(sql);

            return dt;
        }
        public String updateStatusPACs(String reqno, String reqyr)
        {
            String sql = "", re="";
            sql = "Update xray_t01 Set status_pacs = '1' Where mnc_req_no = '" + reqno+"' and mnc_req_yr = '"+reqyr+"' ";
            re = conn.ExecuteNonQuery(sql);
            return re;
        }
    }
}
