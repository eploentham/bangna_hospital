using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class XrayT04DB
    {
        public XrayT04 xrt04;
        public ConnectDB conn;
        public XrayT04DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xrt04 = new XrayT04();
            xrt04.MNC_REQ_YR = "MNC_REQ_YR";
            xrt04.MNC_REQ_NO = "MNC_REQ_NO";
            xrt04.MNC_REQ_DAT = "MNC_REQ_DAT";
            xrt04.MNC_XR_CD = "MNC_XR_CD";
            xrt04.MNC_STS = "MNC_STS";
            xrt04.MNC_XR_TYP = "MNC_XR_TYP";
            xrt04.MNC_GRP_NO = "MNC_GRP_NO";
            xrt04.MNC_XR_DSC = "MNC_XR_DSC";
            xrt04.MNC_RES = "MNC_RES";
            xrt04.MNC_XR_USR = "MNC_XR_USR";
            xrt04.MNC_DOT_DF_CD = "MNC_DOT_DF_CD";
            xrt04.MNC_DOT_GRP_CD = "MNC_DOT_GRP_CD";
            xrt04.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            xrt04.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            xrt04.MNC_XR_DSC1 = "MNC_XR_DSC1";

            xrt04.table = "XRAY_T04";
            xrt04.pkField = "MNC_REQ_YR,MNC_REQ_NO,MNC_REQ_DAT,MNC_XR_CD";
        }
        private void chkNull(XrayT04 p)
        {
            long chk = 0;
            int chk1 = 0;

            p.date_modi = p.date_modi == null ? "" : p.date_modi;
            p.date_cancel = p.date_cancel == null ? "" : p.date_cancel;
            p.user_create = p.user_create == null ? "" : p.user_create;
            p.user_modi = p.user_modi == null ? "" : p.user_modi;
            p.user_cancel = p.user_cancel == null ? "" : p.user_cancel;

            p.MNC_REQ_YR = p.MNC_REQ_YR == null ? "" : p.MNC_REQ_YR;
            p.MNC_REQ_DAT = p.MNC_REQ_DAT == null ? "" : p.MNC_REQ_DAT;
            p.MNC_XR_CD = p.MNC_XR_CD == null ? "" : p.MNC_XR_CD;
            p.MNC_STS = p.MNC_STS == null ? "" : p.MNC_STS;
            p.MNC_XR_TYP = p.MNC_XR_TYP == null ? "" : p.MNC_XR_TYP;
            p.MNC_GRP_NO = p.MNC_GRP_NO == null ? "" : p.MNC_GRP_NO;
            p.MNC_XR_DSC = p.MNC_XR_DSC == null ? "" : p.MNC_XR_DSC;
            p.MNC_XR_USR = p.MNC_XR_USR == null ? "" : p.MNC_XR_USR;
            p.MNC_RES = p.MNC_RES == null ? "" : p.MNC_RES;
            p.MNC_XR_USR = p.MNC_XR_USR == null ? "" : p.MNC_XR_USR;
            p.MNC_DOT_DF_CD = p.MNC_DOT_DF_CD == null ? "" : p.MNC_DOT_DF_CD;
            p.MNC_DOT_GRP_CD = p.MNC_DOT_GRP_CD == null ? "" : p.MNC_DOT_GRP_CD;
            p.MNC_STAMP_DAT = p.MNC_STAMP_DAT == null ? "" : p.MNC_STAMP_DAT;
            //p.MNC_STAMP_TIM = p.MNC_STAMP_TIM == null ? "" : p.MNC_STAMP_TIM;
            p.MNC_XR_DSC1 = p.MNC_XR_DSC1 == null ? "" : p.MNC_XR_DSC1;

            p.MNC_REQ_NO = long.TryParse(p.MNC_REQ_NO, out chk) ? chk.ToString() : "0";
            p.MNC_STAMP_TIM = long.TryParse(p.MNC_STAMP_TIM, out chk) ? chk.ToString() : "0";
            p.MNC_GRP_NO = long.TryParse(p.MNC_GRP_NO, out chk) ? chk.ToString() : "0";
        }

        public String selectByPk(String reqyr, String reqdate, String reqno, String xrcode)
        {
            DataTable dt = new DataTable();
            String sql = "", re="insert";
            sql = "Select * from xray_t04 Where mnc_req_yr = '"+reqyr+"' and mnc_req_no = '"+reqno+"' and mnc_req_dat = '"+reqdate+"' and mnc_xr_cd = '"+xrcode+"'";
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)
            {
                re = "update";
            }
            return re;
        }
        public String insert(XrayT04 p)
        {
            String sql = "", chk = "";
            try
            {
                chkNull(p);
                sql = "Insert Into " + xrt04.table + "(" + xrt04.MNC_REQ_YR + "," + xrt04.MNC_REQ_NO + "," + xrt04.MNC_REQ_DAT + "," +
                    xrt04.MNC_XR_CD + "," + xrt04.MNC_STS + "," + xrt04.MNC_XR_TYP + "," +
                    xrt04.MNC_GRP_NO + "," + xrt04.MNC_XR_DSC + "," + xrt04.MNC_RES + "," +
                    xrt04.MNC_XR_USR + "," + xrt04.MNC_DOT_DF_CD + "," + xrt04.MNC_DOT_GRP_CD + "," +
                    xrt04.MNC_STAMP_DAT + "," + xrt04.MNC_STAMP_TIM + "" +
                    ") " +
                    "Values('" + p.MNC_REQ_YR + "','" + p.MNC_REQ_NO + "','" + p.MNC_REQ_DAT + "','" +
                    p.MNC_XR_CD + "','" + p.MNC_STS + "','" + p.MNC_XR_TYP + "','" +
                    p.MNC_GRP_NO + "','" + p.MNC_XR_DSC.Replace("'","''") + "','" + p.MNC_RES.Replace("'", "''") + "','" +
                    p.MNC_XR_USR + "','" + p.MNC_DOT_DF_CD + "','0'," +
                    "convert(VARCHAR(20),getdate(),20),'0'" +
                    ") ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS,sql);
            }
            catch (Exception ex)
            {
                chk = ex.Message+" "+ex.InnerException;
                new LogWriter("e", "insert Insert  sql " + sql +" ex "+chk);
            }
            return chk;
        }
        public String update(XrayT04 p)
        {
            String sql = "", chk = "";
            try
            {
                //p.dateModi = " CAST(year(GETDATE()) AS NVARCHAR)+'-'+ RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)+'-'+ RIGHT('00' + CAST(day(GETDATE()) AS NVARCHAR), 2)";
                sql = "Update " + xrt04.table + " Set " +
                    xrt04.MNC_XR_DSC + "= '" + p.MNC_XR_DSC + "'," +
                    xrt04.MNC_STAMP_DAT + "= convert(VARCHAR(20),getdate(),20)," +
                    "Where " + xrt04.MNC_REQ_YR + "='" + p.MNC_REQ_YR + "' and " + xrt04.MNC_REQ_NO + "='" + p.MNC_REQ_NO + "' and " + xrt04.MNC_REQ_DAT + "='" + p.MNC_REQ_DAT + "' and " + xrt04.MNC_XR_CD + "='" + p.MNC_XR_CD + "' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "update LabEx");
            }
            return chk;
        }
        //public String insertLabEx(XrayT04 p)
        //{
        //    //LabEx item = new LabEx();
        //    String chk = "";
        //    //item = selectByPk(p.Id);
        //    if (p.Id == "")
        //    {
        //        chk = insert(p);
        //    }
        //    else
        //    {
        //        chk = update(p);
        //    }
        //    return chk;
        //}
    }
}
