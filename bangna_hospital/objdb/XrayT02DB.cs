using bangna_hospital.object1;
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
        public String deleteReqNo(String reqyr, String reqno)
        {
            String sql = "", re = "";
            sql = "Delete From xray_t02 Where mnc_doc_cd = 'ROS' and mnc_req_yr = '" + reqyr + "' and mnc_req_no = '" + reqno + "'";
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