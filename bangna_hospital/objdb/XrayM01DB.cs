using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class XrayM01DB
    {

        public XrayM01 XrayM01;
        ConnectDB conn;
        public XrayM01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

            XrayM01 = new XrayM01();
            XrayM01.MNC_XR_CD = "MNC_XR_CD";
            XrayM01.MNC_XR_CTL_CD = "MNC_XR_CTL_CD";
            XrayM01.MNC_XR_DSC = "MNC_XR_DSC";
            XrayM01.MNC_XR_TYP_CD = "MNC_XR_TYP_CD";
            XrayM01.MNC_XR_GRP_CD = "MNC_XR_GRP_CD";
            XrayM01.MNC_XR_DIS_STS = "MNC_XR_DIS_STS";
            XrayM01.MNC_XR_STS = "MNC_XR_STS";
            XrayM01.MNC_DEC_CD = "MNC_DEC_CD";
            XrayM01.MNC_DEC_NO = "MNC_DEC_NO";
            XrayM01.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            XrayM01.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            XrayM01.MNC_USR_UPD = "MNC_USR_UPD";
            XrayM01.MNC_USR_ADD = "MNC_USR_ADD";
            XrayM01.MNC_OLD_CD = "MNC_OLD_CD";
            XrayM01.MNC_SUP_STS = "MNC_SUP_STS";
            XrayM01.MNC_XR_PRI = "MNC_XR_PRI";
            XrayM01.MNC_XR_DSC_STS = "MNC_XR_DSC_STS";
            XrayM01.MNC_XR_AUTO = "MNC_XR_AUTO";
            XrayM01.pacs_infinitt_code = "pacs_infinitt_code";
            XrayM01.modality_code = "modality_code";
            XrayM01.ucep_code = "ucep_code";
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select * " +
                "From xray_m01  ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public String updateOPBKKCode(String xraycode, String opbkkcode)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update xray_m01 Set " +
                    "ucep_code ='" + opbkkcode + "' " +
                    "Where mnc_xr_cd ='" + xraycode + "' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " XrayM01DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
        public XrayM01 setXrayM01(DataTable dt)
        {
            XrayM01 xrayM01 = new XrayM01();
            if (dt.Rows.Count > 0)
            {

                xrayM01.MNC_XR_CD = dt.Rows[0]["MNC_XR_CD"].ToString();
                xrayM01.MNC_XR_CTL_CD = dt.Rows[0]["MNC_XR_CTL_CD"].ToString();
                xrayM01.MNC_XR_DSC = dt.Rows[0]["MNC_XR_DSC"].ToString();
                xrayM01.MNC_XR_TYP_CD = dt.Rows[0]["MNC_XR_TYP_CD"].ToString();
                xrayM01.MNC_XR_GRP_CD = dt.Rows[0]["MNC_XR_GRP_CD"].ToString();
                xrayM01.MNC_XR_DIS_STS = dt.Rows[0]["MNC_XR_DIS_STS"].ToString();
                xrayM01.MNC_XR_STS = dt.Rows[0]["MNC_XR_STS"].ToString();
                xrayM01.MNC_DEC_CD = dt.Rows[0]["MNC_DEC_CD"].ToString();
                xrayM01.MNC_DEC_NO = dt.Rows[0]["MNC_DEC_NO"].ToString();
                xrayM01.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                xrayM01.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                xrayM01.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                xrayM01.MNC_USR_ADD = dt.Rows[0]["MNC_USR_ADD"].ToString();
                xrayM01.MNC_OLD_CD = dt.Rows[0]["MNC_OLD_CD"].ToString();
                xrayM01.MNC_SUP_STS = dt.Rows[0]["MNC_SUP_STS"].ToString();
                xrayM01.MNC_XR_PRI = dt.Rows[0]["MNC_XR_PRI"].ToString();
                xrayM01.MNC_XR_DSC_STS = dt.Rows[0]["MNC_XR_DSC_STS"].ToString();
                xrayM01.MNC_XR_AUTO = dt.Rows[0]["MNC_XR_AUTO"].ToString();
                xrayM01.pacs_infinitt_code = dt.Rows[0]["pacs_infinitt_code"].ToString();
                xrayM01.modality_code = dt.Rows[0]["modality_code"].ToString();
                xrayM01.ucep_code = dt.Rows[0]["ucep_code"].ToString();
            }
            else
            {
                setXrayM01(xrayM01);
            }
            return xrayM01;
        }
        public XrayM01 setXrayM01(XrayM01 p)
        {
            p.MNC_XR_CD = "";
            p.MNC_XR_CTL_CD = "";
            p.MNC_XR_DSC = "";
            p.MNC_XR_TYP_CD = "";
            p.MNC_XR_GRP_CD = "";
            p.MNC_XR_DIS_STS = "";
            p.MNC_XR_STS = "";
            p.MNC_DEC_CD = "";
            p.MNC_DEC_NO = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_UPD = "";
            p.MNC_USR_ADD = "";
            p.MNC_OLD_CD = "";
            p.MNC_SUP_STS = "";
            p.MNC_XR_PRI = "";
            p.MNC_XR_DSC_STS = "";
            p.MNC_XR_AUTO = "";
            p.pacs_infinitt_code = "";
            p.modality_code = "";
            p.ucep_code = "";
            return p;
        }
    }
}





