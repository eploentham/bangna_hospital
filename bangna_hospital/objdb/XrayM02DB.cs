using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class XrayM02DB
    {
        public XrayM02 XrayM02;
        ConnectDB conn;
        public XrayM02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            XrayM02 = new XrayM02();
            XrayM02.MNC_XR_CD = "MNC_XR_CD";
            XrayM02.MNC_FN_CD = "MNC_FN_CD";
            XrayM02.MNC_PRI_STS = "MNC_PRI_STS";
            XrayM02.MNC_XR_PRI = "MNC_XR_PRI";
            XrayM02.MNC_XR_COS = "MNC_XR_COS";
            XrayM02.MNC_XR_PRI01 = "MNC_XR_PRI01";
            XrayM02.MNC_XR_PRI02 = "MNC_XR_PRI02";
            XrayM02.MNC_XR_PRI03 = "MNC_XR_PRI03";
            XrayM02.MNC_XR_PRI04 = "MNC_XR_PRI04";
            XrayM02.MNC_XR_PRI05 = "MNC_XR_PRI05";
            XrayM02.MNC_XR_PRI06 = "MNC_XR_PRI06";
            XrayM02.MNC_XR_PRI07 = "MNC_XR_PRI07";
            XrayM02.MNC_XR_PRI08 = "MNC_XR_PRI08";
            XrayM02.MNC_XR_PRI09 = "MNC_XR_PRI09";
            XrayM02.MNC_XR_PRI10 = "MNC_XR_PRI10";
            XrayM02.MNC_XR_PRI11 = "MNC_XR_PRI11";
            XrayM02.MNC_XR_PRI12 = "MNC_XR_PRI12";
            XrayM02.MNC_XR_PRI13 = "MNC_XR_PRI13";
            XrayM02.MNC_XR_PRI14 = "MNC_XR_PRI14";
            XrayM02.MNC_XR_PRI15 = "MNC_XR_PRI15";
            XrayM02.MNC_XR_PRI16 = "MNC_XR_PRI16";
            XrayM02.MNC_XR_PRI17 = "MNC_XR_PRI17";
            XrayM02.MNC_XR_PRI18 = "MNC_XR_PRI18";
            XrayM02.MNC_XR_PRI19 = "MNC_XR_PRI19";
            XrayM02.MNC_XR_PRI20 = "MNC_XR_PRI20";
            XrayM02.MNC_XR_RFN = "MNC_XR_RFN";
            XrayM02.MNC_CHANGE_PRI_STS = "MNC_CHANGE_PRI_STS";
            XrayM02.MNC_SHOW_PRI_STS = "MNC_SHOW_PRI_STS";
            XrayM02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            XrayM02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            XrayM02.MNC_USR_ADD = "MNC_USR_ADD";
            XrayM02.MNC_USR_UPD = "MNC_USR_UPD";
            XrayM02.MNC_CHARGE_NO = "MNC_CHARGE_NO";
            XrayM02.MNC_XR_DIS_STS = "MNC_XR_DIS_STS";
        }
        public XrayM02 setXrayM02(DataTable dt)
        {
            XrayM02 xrayM02 = new XrayM02();
            if (dt.Rows.Count > 0)
            {
                xrayM02.MNC_XR_CD = dt.Rows[0]["MNC_XR_CD"].ToString();
                xrayM02.MNC_FN_CD = dt.Rows[0]["MNC_FN_CD"].ToString();
                xrayM02.MNC_PRI_STS = dt.Rows[0]["MNC_PRI_STS"].ToString();
                xrayM02.MNC_XR_PRI = dt.Rows[0]["MNC_XR_PRI"].ToString();
                xrayM02.MNC_XR_COS = dt.Rows[0]["MNC_XR_COS"].ToString();
                xrayM02.MNC_XR_PRI01 = dt.Rows[0]["MNC_XR_PRI01"].ToString();
                xrayM02.MNC_XR_PRI02 = dt.Rows[0]["MNC_XR_PRI02"].ToString();
                xrayM02.MNC_XR_PRI03 = dt.Rows[0]["MNC_XR_PRI03"].ToString();
                xrayM02.MNC_XR_PRI04 = dt.Rows[0]["MNC_XR_PRI04"].ToString();
                xrayM02.MNC_XR_PRI05 = dt.Rows[0]["MNC_XR_PRI05"].ToString();
                xrayM02.MNC_XR_PRI06 = dt.Rows[0]["MNC_XR_PRI06"].ToString();
                xrayM02.MNC_XR_PRI07 = dt.Rows[0]["MNC_XR_PRI07"].ToString();
                xrayM02.MNC_XR_PRI08 = dt.Rows[0]["MNC_XR_PRI08"].ToString();
                xrayM02.MNC_XR_PRI09 = dt.Rows[0]["MNC_XR_PRI09"].ToString();
                xrayM02.MNC_XR_PRI10 = dt.Rows[0]["MNC_XR_PRI10"].ToString();
                xrayM02.MNC_XR_PRI11 = dt.Rows[0]["MNC_XR_PRI11"].ToString();
                xrayM02.MNC_XR_PRI12 = dt.Rows[0]["MNC_XR_PRI12"].ToString();
                xrayM02.MNC_XR_PRI13 = dt.Rows[0]["MNC_XR_PRI13"].ToString();
                xrayM02.MNC_XR_PRI14 = dt.Rows[0]["MNC_XR_PRI14"].ToString();
                xrayM02.MNC_XR_PRI15 = dt.Rows[0]["MNC_XR_PRI15"].ToString();
                xrayM02.MNC_XR_PRI16 = dt.Rows[0]["MNC_XR_PRI16"].ToString();
                xrayM02.MNC_XR_PRI17 = dt.Rows[0]["MNC_XR_PRI17"].ToString();
                xrayM02.MNC_XR_PRI18 = dt.Rows[0]["MNC_XR_PRI18"].ToString();
                xrayM02.MNC_XR_PRI19 = dt.Rows[0]["MNC_XR_PRI19"].ToString();
                xrayM02.MNC_XR_PRI20 = dt.Rows[0]["MNC_XR_PRI20"].ToString();
                xrayM02.MNC_XR_RFN = dt.Rows[0]["MNC_XR_RFN"].ToString();
                xrayM02.MNC_CHANGE_PRI_STS = dt.Rows[0]["MNC_CHANGE_PRI_STS"].ToString();
                xrayM02.MNC_SHOW_PRI_STS = dt.Rows[0]["MNC_SHOW_PRI_STS"].ToString();
                xrayM02.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                xrayM02.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                xrayM02.MNC_USR_ADD = dt.Rows[0]["MNC_USR_ADD"].ToString();
                xrayM02.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                xrayM02.MNC_CHARGE_NO = dt.Rows[0]["MNC_CHARGE_NO"].ToString();
                xrayM02.MNC_XR_DIS_STS = dt.Rows[0]["MNC_XR_DIS_STS"].ToString();
            }
            else
            {
                setXrayM02(xrayM02);
            }
            return xrayM02;
        }
        public XrayM02 setXrayM02(XrayM02 p)
        {
            p.MNC_XR_CD = "";
            p.MNC_FN_CD = "";
            p.MNC_PRI_STS = "";
            p.MNC_XR_PRI = "";
            p.MNC_XR_COS = "";
            p.MNC_XR_PRI01 = "";
            p.MNC_XR_PRI02 = "";
            p.MNC_XR_PRI03 = "";
            p.MNC_XR_PRI04 = "";
            p.MNC_XR_PRI05 = "";
            p.MNC_XR_PRI06 = "";
            p.MNC_XR_PRI07 = "";
            p.MNC_XR_PRI08 = "";
            p.MNC_XR_PRI09 = "";
            p.MNC_XR_PRI10 = "";
            p.MNC_XR_PRI11 = "";
            p.MNC_XR_PRI12 = "";
            p.MNC_XR_PRI13 = "";
            p.MNC_XR_PRI14 = "";
            p.MNC_XR_PRI15 = "";
            p.MNC_XR_PRI16 = "";
            p.MNC_XR_PRI17 = "";
            p.MNC_XR_PRI18 = "";
            p.MNC_XR_PRI19 = "";
            p.MNC_XR_PRI20 = "";
            p.MNC_XR_RFN = "";
            p.MNC_CHANGE_PRI_STS = "";
            p.MNC_SHOW_PRI_STS = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_ADD = "";
            p.MNC_USR_UPD = "";
            p.MNC_CHARGE_NO = "";
            p.MNC_XR_DIS_STS = "";

            return p;
        }
    }
}




