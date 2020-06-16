using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,ptt01.MNC_VN_NO, " +
                " ptt01.MNC_VN_SEQ,ptt01.MNC_VN_SUM, ptt01.mnc_vn_no,ptt01.mnc_vn_seq, m01.mnc_vn_sum ,convert(VARCHAR(20),m01.mnc_bday,23) as mnc_bday, " +
                "m01.mnc_sex, ptt01.mnc_shif_memo, xt02.MNC_XR_CD, xm01.MNC_XR_DSC, xr01.mnc_empr_cd, um01.mnc_usr_full,ptt01.MNC_SEC_NO, pm32.MNC_MD_DEP_DSC, ptt01.mnc_sts " +
                "From  xray_t01 xr01  " +
                "inner Join patient_m01 m01  on xr01 .mnc_hn_no = m01.mnc_hn_no  " +
                "inner join patient_m02 m02 on m01.MNC_PFIX_CDT = m02.MNC_PFIX_CD " +
                "inner join PATIENT_T01 ptt01 on xr01 .mnc_hn_no = ptt01.mnc_hn_no and xr01.MNC_DATE = ptt01.MNC_DATE and xr01 .MNC_PRE_NO = ptt01.MNC_PRE_NO  " +
                "Left Join xray_t02 xt02 on xr01.MNC_REQ_NO = xt02.MNC_REQ_NO  and xr01.MNC_REQ_YR = xt02.MNC_REQ_YR and xr01.MNC_REQ_DAT = xt02.MNC_REQ_DAT " +
                " Left Join xray_m01 xm01 on xm01.MNC_XR_CD = xt02.MNC_XR_CD " +
                " Left Join userlog_m01 um01 on xr01.mnc_empr_cd = um01.mnc_usr_name " +
                " Left Join patient_m32 pm32 on ptt01.MNC_SEC_NO = pm32.MNC_SEC_NO " +
                " Where xr01.MNC_REQ_DAT = '" + Date + "' and xr01.MNC_REQ_STS = 'A' and xr01.status_pacs = '0' " +
                "Order By xr01.mnc_req_dat, xr01.mnc_req_tim";
            dt = conn.selectData(sql);
            
            return dt;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "";
            
            sql = "Select  xm01.* " +
                "From  xray_m01 xm01  " +

                " Where xm01.MNC_XR_STS = 'Y'  " +
                "Order By xm01.MNC_XR_CD";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectXrayAll()
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select  xm01.mnc_xr_cd, xm01.mnc_xr_dsc, xm01.mnc_xr_typ_cd, xm01.MNC_XR_GRP_CD, XRAY_M05.MNC_XR_GRP_DSC " +
                "From  xray_m01 xm01  " +
                " inner join XRAY_M05 on xm01.MNC_XR_GRP_CD = XRAY_M05.MNC_XR_GRP_CD" +

                " Where xm01.MNC_XR_STS = 'Y'  " +
                "Order By xm01.MNC_XR_CD";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectXrayByCode(String code)
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select  xm01.mnc_xr_cd, xm01.mnc_xr_dsc, xm01.mnc_typ_cd, xm01.grp_cd " +
                "From  xray_m01 xm01  " +

                " Where xm01.MNC_XR_STS = 'Y' and xm01.mnc_xr_cd = '"+code+"' " +
                "Order By xm01.MNC_XR_CD";
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
            re = conn.ExecuteNonQuery(conn.connMainHIS,sql);
            return re;
        }
        public String updateModality(String xrcode, String modalitycode)
        {
            String sql = "", re = "";
            sql = "Update xray_m01 Set modality_code = '"+ modalitycode + "' Where mnc_xr_cd = '" + xrcode + "' ";
            re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            return re;
        }
        public String selectPACsInfinittCode(String xraycode)
        {
            String pacscode = "",re="";
            //comMainhis.Parameters.Add("@xraycode", SqlDbType.NVarChar).Value = xraycode;
            //comMainhis.Parameters.Add("@pacinfinittcode", SqlDbType.NVarChar).Value = pacscode;
            conn.comStore = new System.Data.SqlClient.SqlCommand();
            conn.comStore.Connection = conn.connMainHIS;
            conn.comStore.CommandText = "select_gen_pac_infinitt_code";
            conn.comStore.CommandType = CommandType.StoredProcedure;
            conn.comStore.Parameters.Add("xraycode", SqlDbType.NVarChar).Value = xraycode;
            SqlParameter retval = conn.comStore.Parameters.Add("pacinfinittcode", SqlDbType.VarChar, 50);
            retval.Value = "";
            retval.Direction = ParameterDirection.Output;
            conn.connMainHIS.Open();
            conn.comStore.ExecuteNonQuery();
            re = (String)conn.comStore.Parameters["pacinfinittcode"].Value;
            conn.connMainHIS.Close();
            return re;
        }
        public DataTable selectResultXraybyVN1(String hn, String preno, String vsdate, String xraycode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select xray_t04.MNC_XR_DSC " +
                "From xray_t01 xt01  " +
                "left join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01.MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "left join xray_t04  on xray_t04.MNC_REQ_NO = XRAY_T02.MNC_REQ_NO and xray_t04.MNC_REQ_DAT = XRAY_T02.MNC_REQ_DAT and xray_t04.MNC_REQ_YR = XRAY_T02.MNC_REQ_YR and xray_t04.MNC_XR_CD = XRAY_T02.MNC_XR_CD " +
                "where xt01.mnc_pre_no = '" + preno + "'  " +
                "and xt01.mnc_date = '" + vsdate + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' " +
                "and xray_t04.mnc_xr_cd = '" + xraycode + "' " +
                "Order By xt01.MNC_REQ_NO, xray_t04.MNC_XR_CD  ";

            dt = conn.selectData(conn.connMainHIS,sql);
            return dt;
        }
        public DataTable selectResultXrayPACsPlusbyVN1(String hn, String preno, String vsdate, String xraycode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select rpttc.*, rptt.* " +
                "From ReportTab rptt   " +
                "left join ReportTab_Contents rpttc on rptt.StudyKey = rpttc.StudyKey and rptt.HistNo = rpttc.HistNo " +
                "where  rptt.studydate = '" + vsdate.Replace("-","").Replace("/", "") + "'  " +
                "and rptt.PID = '" + hn + "' " +
                "and len(rptt.readingdrid) > 0 " +
                "Order By rpttc.histno, rptt.StudyKey,rpttc.recno    ";

            dt = conn.selectData(conn.connPACs, sql);
            return dt;
        }
    }
}
