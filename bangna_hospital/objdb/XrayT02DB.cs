using bangna_hospital.object1;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class XrayT02DB
    {
        public XrayT02 XrayT02;
        ConnectDB conn;
        public XrayT02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            XrayT02 = new XrayT02();
            XrayT02.MNC_REQ_YR = "MNC_REQ_YR";
            XrayT02.MNC_REQ_NO = "MNC_REQ_NO";
            XrayT02.MNC_REQ_DAT = "MNC_REQ_DAT";
            XrayT02.MNC_REQ_STS = "MNC_REQ_STS";
            XrayT02.MNC_XR_CD = "MNC_XR_CD";
            XrayT02.MNC_XR_RMK = "MNC_XR_RMK";
            XrayT02.MNC_XR_COS = "MNC_XR_COS";
            XrayT02.MNC_XR_PRI = "MNC_XR_PRI";
            XrayT02.MNC_XR_RFN = "MNC_XR_RFN";
            XrayT02.MNC_XR_PRI_R = "MNC_XR_PRI_R";
            XrayT02.MNC_FLG_K = "MNC_FLG_K";
            XrayT02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            XrayT02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            XrayT02.MNC_XR_COS_R = "MNC_XR_COS_R";
            XrayT02.MNC_DOT_CD_DF = "MNC_DOT_CD_DF";
            XrayT02.MNC_DOT_GRP_CD = "MNC_DOT_GRP_CD";
            XrayT02.MNC_ACT_DAT = "MNC_ACT_DAT";
            XrayT02.MNC_ACT_TIM = "MNC_ACT_TIM";
            XrayT02.MNC_CANCEL_STS = "MNC_CANCEL_STS";
            XrayT02.MNC_USR_UPD = "MNC_USR_UPD";
            XrayT02.MNC_SND_OUT_STS = "MNC_SND_OUT_STS";
            XrayT02.MNC_XR_STS = "MNC_XR_STS";
            XrayT02.status_pacs = "status_pacs";
        }
        public DataTable selectbyHNReqNo(String hn, String reqdate, String reqno)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "SELECT  XRAY_T02.MNC_XR_CD as order_code, XRAY_M01.MNC_XR_DSC as order_name, convert(varchar(20),XRAY_T02.MNC_REQ_DAT, 23) as req_date " +
                ", XRAY_T02.MNC_REQ_NO as req_no, 'xray' as flag, '1' as qty,XRAY_T02.MNC_REQ_YR,isnull(XRAY_T02.MNC_XR_PRI,0) as MNC_XR_PRI " +
                ",  isnull(pm02.MNC_PFIX_DSC,'') +' '+isnull(pm01.MNC_FNAME_T,'')+' '+isnull(pm01.MNC_LNAME_T,'') as pttfullname " +
                ",XRAY_T01.MNC_DOT_CD, isnull(pm02dtr.MNC_PFIX_DSC,'')  + ' ' + isnull(pm26.MNC_DOT_FNAME,'')+' '+isnull(pm26.MNC_DOT_LNAME,'') as dtr_name " +
                ",pt01.MNC_FN_TYP_CD,pt01.MNC_DEP_NO, pt01.MNC_SEC_NO,pt01.MNC_COM_CD, isnull(pt01.MNC_RES_MAS,'') as MNC_RES_MAS,XRAY_T01.MNC_HN_NO " +
                ", pt01.mnc_vn_seq, pt01.mnc_vn_sum, pt01.mnc_vn_no,pt01.MNC_TIME,XRAY_T01.MNC_REQ_TIM,convert(varchar(20),pt01.MNC_DATE,23) as MNC_DATE " +
                ", isnull(userm01_usr.MNC_USR_FULL,'') as MNC_USR_FULL_usr " +
                "FROM    XRAY_T01  " +
                "left join XRAY_T02 ON XRAY_T01.MNC_REQ_NO = XRAY_T02.MNC_REQ_NO AND XRAY_T01.MNC_REQ_DAT = XRAY_T02.MNC_REQ_DAT " +
                "left join XRAY_M01 ON XRAY_T02.MNC_XR_CD = XRAY_M01.MNC_XR_CD " +
                "inner join patient_m01 pm01 on XRAY_T01.MNC_HN_NO = pm01.MNC_HN_NO  " +
                "Left Join patient_m02 pm02 On pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "left join patient_m26 pm26 on XRAY_T01.MNC_ORD_DOT = pm26.MNC_DOT_CD " +
                "left join patient_m02 pm02dtr on pm26.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                "left Join USERLOG_M01 userm01_usr on XRAY_T01.MNC_EMPC_CD = userm01_usr.MNC_USR_NAME  " +
                "inner join PATIENT_T01 pt01 on XRAY_T01.MNC_HN_NO = pt01.MNC_HN_NO and  XRAY_T01.MNC_DATE = pt01.MNC_DATE and  XRAY_T01.MNC_PRE_NO = pt01.MNC_PRE_NO " +
                "where XRAY_T01.MNC_REQ_DAT = '" + reqdate + "' and XRAY_T01.MNC_REQ_NO = '" + reqno + "'  " +
                "and XRAY_T01.mnc_hn_no = '" + hn + "' " +
                "and XRAY_T02.mnc_req_sts <> 'C'  and XRAY_T01.mnc_req_sts <> 'C'  " +
                "Order By XRAY_T02.MNC_XR_CD ";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public DataTable selectbyHN(String hn, String vsdate, String preno)
        {
            DataTable dt = new DataTable();
            String sql = "", lccode = "", wherelccode = "";
            sql = "SELECT  XRAY_T02.MNC_XR_CD as order_code, XRAY_M01.MNC_XR_DSC as order_name, convert(varchar(20),XRAY_T02.MNC_REQ_DAT, 23) as req_date " +
                ", XRAY_T02.MNC_REQ_NO as req_no, 'xray' as flag, '1' as qty,XRAY_T02.MNC_REQ_YR " +
                "FROM    XRAY_T01  " +
                "left join XRAY_T02 ON XRAY_T01.MNC_REQ_NO = XRAY_T02.MNC_REQ_NO AND XRAY_T01.MNC_REQ_DAT = XRAY_T02.MNC_REQ_DAT " +
                "left join XRAY_M01 ON XRAY_T02.MNC_XR_CD = XRAY_M01.MNC_XR_CD " +
                "where XRAY_T01.MNC_DATE = '" + vsdate + "' and XRAY_T01.MNC_PRE_NO = '" + preno + "'  " +
                "and XRAY_T01.mnc_hn_no = '" + hn + "' " +
                "and XRAY_T02.mnc_req_sts <> 'C'  and XRAY_T01.mnc_req_sts <> 'C'  " +
                "Order By XRAY_T02.MNC_XR_CD ";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        private void chkNull(XrayT02 p)
        {
            long chk = 0;
            int chk1 = 0;
            decimal chk2 = 0;
        }
        public String insertXrayT02(XrayT02 p, String userId)
        {
            String sql = "", re = "";

            chkNull(p);
            sql = "Insert Into xray_t02 " +
                "(MNC_REQ_YR,MNC_REQ_NO,MNC_REQ_DAT,MNC_REQ_STS" +
                ",MNC_XR_CD,MNC_XR_RMK,MNC_XR_COS,MNC_XR_PRI" +
                ",MNC_XR_RFN, MNC_XR_PRI_R, MNC_FLG_K,MNC_STAMP_DAT" +
                ",MNC_STAMP_TIM,MNC_XR_COS_R,MNC_DOT_CD_DF,MNC_DOT_GRP_CD" +
                ",MNC_ACT_DAT,MNC_ACT_TIM,MNC_CANCEL_STS,MNC_USR_UPD" +
                ",MNC_SND_OUT_STS,MNC_XR_STS,status_pacs" +
                ")" +
                "Values ('" + p.MNC_REQ_YR + "','" + p.MNC_REQ_NO + "','" + p.MNC_REQ_DAT + "','" + p.MNC_REQ_STS + "'" +
                ",'" + p.MNC_XR_CD + "','" + p.MNC_XR_RMK + "','" + p.MNC_XR_COS + "','" + p.MNC_XR_PRI + "'" +
                ",'" + p.MNC_XR_RFN + "','" + p.MNC_XR_PRI_R + "','" + p.MNC_FLG_K + "',convert(varchar(10), GETDATE(),23)" +
                ",replace(left(convert(varchar(100),getdate(),108),5),':',''),'" + p.MNC_XR_COS_R + "','" + p.MNC_DOT_CD_DF + "','" + p.MNC_DOT_GRP_CD + "'" +
                ",'" + p.MNC_ACT_DAT + "','" + p.MNC_ACT_TIM + "','" + p.MNC_CANCEL_STS + "','" + p.MNC_USR_UPD + "'" +
                ",'" + p.MNC_SND_OUT_STS + "','" + p.MNC_XR_STS + "','" + p.status_pacs + "'" +
                ")";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //new LogWriter("d", "PharmacyT02 insertPharmacyT02 sql " + sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "XrayT02 insertXrayT02 " + ex.InnerException);
            }

            return re;
        }
        public String updateStatusPaca(String reqyr, String reqno, String req_date, String hn)
        {
            String sql = "", re = "";
            sql = "Update xray_t02 " +
                "Set status_pacs ='0' " +
                "Where MNC_REQ_YR = '" + reqyr + "' and MNC_REQ_NO = '" + reqno + "' and MNC_REQ_DAT = '" + req_date + "' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            return re;
        }
        public String updateAccessNumber(String reqyr, String reqno, String req_date, String xray_code, String accessnumber)
        {
            String sql = "", re = "";
            sql = "Update xray_t02 " +
                "Set access_number ='"+ accessnumber+"' " +
                "Where MNC_REQ_YR = '"+ reqyr + "' and MNC_REQ_NO = '" + reqno + "' and MNC_REQ_DAT = '" + req_date + "' and MNC_XR_CD = '" + xray_code + "'";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            return re;
        }
        public String deleteReqNo(String reqyr, String reqno)
        {
            String sql = "", re = "";
            sql = "Delete From xray_t02 Where mnc_req_yr = '" + reqyr + "' and mnc_req_no = '" + reqno + "' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            return re;
        }
        public String deleteReqNo(String reqyr, String reqno, String req_date)
        {
            String sql = "", re = "";
            sql = "Delete From xray_t02 Where mnc_req_yr = '" + reqyr + "' and mnc_req_no = '" + reqno + "' and MNC_REQ_DAT = '" + req_date + "'";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                sql = "Delete From xray_t01 Where mnc_req_yr = '" + reqyr + "' and mnc_req_no = '" + reqno + "' and MNC_REQ_DAT = '" + req_date + "'";
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            return re;
        }
        public XrayT02 setXrayT02(DataTable dt)
        {
            XrayT02 xrayT02 = new XrayT02();
            if (dt.Rows.Count > 0)
            {
                xrayT02.MNC_REQ_YR = dt.Rows[0]["MNC_REQ_YR"].ToString();
                xrayT02.MNC_REQ_NO = dt.Rows[0]["MNC_REQ_NO"].ToString(); ;
                xrayT02.MNC_REQ_DAT = dt.Rows[0]["MNC_REQ_DAT"].ToString();
                xrayT02.MNC_REQ_STS = dt.Rows[0]["MNC_REQ_STS"].ToString(); ;
                xrayT02.MNC_XR_CD = dt.Rows[0]["MNC_XR_CD"].ToString();
                xrayT02.MNC_XR_RMK = dt.Rows[0]["MNC_XR_RMK"].ToString();
                xrayT02.MNC_XR_COS = dt.Rows[0]["MNC_XR_COS"].ToString();
                xrayT02.MNC_XR_PRI = dt.Rows[0]["MNC_XR_PRI"].ToString();
                xrayT02.MNC_XR_RFN = dt.Rows[0]["MNC_XR_RFN"].ToString();
                xrayT02.MNC_XR_PRI_R = dt.Rows[0]["MNC_XR_PRI_R"].ToString();
                xrayT02.MNC_FLG_K = dt.Rows[0]["MNC_FLG_K"].ToString();
                xrayT02.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                xrayT02.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                xrayT02.MNC_XR_COS_R = dt.Rows[0]["MNC_XR_COS_R"].ToString();
                xrayT02.MNC_DOT_CD_DF = dt.Rows[0]["MNC_DOT_CD_DF"].ToString();
                xrayT02.MNC_DOT_GRP_CD = dt.Rows[0]["MNC_DOT_GRP_CD"].ToString();
                xrayT02.MNC_ACT_DAT = dt.Rows[0]["MNC_ACT_DAT"].ToString();
                xrayT02.MNC_ACT_TIM = dt.Rows[0]["MNC_ACT_TIM"].ToString();
                xrayT02.MNC_CANCEL_STS = dt.Rows[0]["MNC_CANCEL_STS"].ToString();
                xrayT02.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                xrayT02.MNC_SND_OUT_STS = dt.Rows[0]["MNC_SND_OUT_STS"].ToString();
                xrayT02.MNC_XR_STS = dt.Rows[0]["MNC_XR_STS"].ToString();
                xrayT02.status_pacs = dt.Rows[0]["status_pacs"].ToString();
            }
            else
            {
                setXrayT02(xrayT02);
            }
            return xrayT02;
        }
        public XrayT02 setXrayT02(XrayT02 p)
        {

            p.MNC_REQ_YR = "";
            p.MNC_REQ_NO = "";
            p.MNC_REQ_DAT = "";
            p.MNC_REQ_STS = "";
            p.MNC_XR_CD = "";
            p.MNC_XR_RMK = "";
            p.MNC_XR_COS = "";
            p.MNC_XR_PRI = "";
            p.MNC_XR_RFN = "";
            p.MNC_XR_PRI_R = "";
            p.MNC_FLG_K = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_XR_COS_R = "";
            p.MNC_DOT_CD_DF = "";
            p.MNC_DOT_GRP_CD = "";
            p.MNC_ACT_DAT = "";
            p.MNC_ACT_TIM = "";
            p.MNC_CANCEL_STS = "";
            p.MNC_USR_UPD = "";
            p.MNC_SND_OUT_STS = "";
            p.MNC_XR_STS = "";
            p.status_pacs = "";
            return p;
        }
    }
}