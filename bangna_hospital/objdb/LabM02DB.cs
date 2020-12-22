using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LabM02DB
    {
        public LabM02 labM02;
        ConnectDB conn;
        public LabM02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labM02 = new LabM02();
            labM02.MNC_LB_CD = "MNC_LB_CD";
            labM02.MNC_FN_CD = "MNC_FN_CD";
            labM02.MNC_PRI_STS = "MNC_PRI_STS";
            labM02.MNC_LB_PRI = "MNC_LB_PRI";
            labM02.MNC_LB_COS = "MNC_LB_COS";
            labM02.MNC_LB_PRI01 = "MNC_LB_PRI01";
            labM02.MNC_LB_PRI02 = "MNC_LB_PRI02";
            labM02.MNC_LB_PRI03 = "MNC_LB_PRI03";
            labM02.MNC_LB_PRI04 = "MNC_LB_PRI04";
            labM02.MNC_LB_PRI05 = "MNC_LB_PRI05";
            labM02.MNC_LB_PRI06 = "MNC_LB_PRI06";
            labM02.MNC_LB_PRI07 = "MNC_LB_PRI07";
            labM02.MNC_LB_PRI08 = "MNC_LB_PRI08";
            labM02.MNC_LB_PRI09 = "MNC_LB_PRI09";
            labM02.MNC_LB_PRI10 = "MNC_LB_PRI10";
            labM02.MNC_LB_PRI11 = "MNC_LB_PRI11";
            labM02.MNC_LB_PRI12 = "MNC_LB_PRI12";
            labM02.MNC_LB_PRI13 = "MNC_LB_PRI13";
            labM02.MNC_LB_PRI14 = "MNC_LB_PRI14";
            labM02.MNC_LB_PRI15 = "MNC_LB_PRI15";
            labM02.MNC_LB_PRI16 = "MNC_LB_PRI16";
            labM02.MNC_LB_PRI17 = "MNC_LB_PRI17";
            labM02.MNC_LB_PRI18 = "MNC_LB_PRI18";
            labM02.MNC_LB_PRI19 = "MNC_LB_PRI19";
            labM02.MNC_LB_PRI20 = "MNC_LB_PRI20";
            labM02.MNC_LB_RFN = "MNC_LB_RFN";
            labM02.MNC_LBC_CD = "MNC_LBC_CD";
            labM02.MNC_LBC_PRI = "MNC_LBC_PRI";
            labM02.MNC_LBC_COS = "MNC_LBC_COS";
            labM02.MNC_CHANGE_PRI_STS = "MNC_CHANGE_PRI_STS";
            labM02.MNC_SHOW_PRI_STS = "MNC_SHOW_PRI_STS";
            labM02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            labM02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            labM02.MNC_USR_ADD = "MNC_USR_ADD";
            labM02.MNC_USR_UPD = "MNC_USR_UPD";
            labM02.MNC_CHARGE_NO = "MNC_CHARGE_NO";
        }
        public LabM02 setLabM02(DataTable dt)
        {
            LabM02 labM02 = new LabM02();
            if (dt.Rows.Count > 0)
            {
                labM02.MNC_LB_CD = dt.Rows[0]["MNC_LB_CD"].ToString();
                labM02.MNC_FN_CD = dt.Rows[0]["MNC_FN_CD"].ToString();
                labM02.MNC_PRI_STS = dt.Rows[0]["MNC_PRI_STS"].ToString();
                labM02.MNC_LB_PRI = dt.Rows[0]["MNC_LB_PRI"].ToString();
                labM02.MNC_LB_COS = dt.Rows[0]["MNC_LB_COS"].ToString();
                labM02.MNC_LB_PRI01 = dt.Rows[0]["MNC_LB_PRI01"].ToString();
                labM02.MNC_LB_PRI02 = dt.Rows[0]["MNC_LB_PRI02"].ToString();
                labM02.MNC_LB_PRI03 = dt.Rows[0]["MNC_LB_PRI03"].ToString();
                labM02.MNC_LB_PRI04 = dt.Rows[0]["MNC_LB_PRI04"].ToString();
                labM02.MNC_LB_PRI05 = dt.Rows[0]["MNC_LB_PRI05"].ToString();
                labM02.MNC_LB_PRI06 = dt.Rows[0]["MNC_LB_PRI06"].ToString();
                labM02.MNC_LB_PRI07 = dt.Rows[0]["MNC_LB_PRI07"].ToString();
                labM02.MNC_LB_PRI08 = dt.Rows[0]["MNC_LB_PRI08"].ToString();
                labM02.MNC_LB_PRI09 = dt.Rows[0]["MNC_LB_PRI09"].ToString();
                labM02.MNC_LB_PRI10 = dt.Rows[0]["MNC_LB_PRI10"].ToString();
                labM02.MNC_LB_PRI11 = dt.Rows[0]["MNC_LB_PRI11"].ToString();
                labM02.MNC_LB_PRI12 = dt.Rows[0]["MNC_LB_PRI12"].ToString();
                labM02.MNC_LB_PRI13 = dt.Rows[0]["MNC_LB_PRI13"].ToString();
                labM02.MNC_LB_PRI14 = dt.Rows[0]["MNC_LB_PRI14"].ToString();
                labM02.MNC_LB_PRI15 = dt.Rows[0]["MNC_LB_PRI15"].ToString();
                labM02.MNC_LB_PRI16 = dt.Rows[0]["MNC_LB_PRI16"].ToString();
                labM02.MNC_LB_PRI17 = dt.Rows[0]["MNC_LB_PRI17"].ToString();
                labM02.MNC_LB_PRI18 = dt.Rows[0]["MNC_LB_PRI18"].ToString();
                labM02.MNC_LB_PRI19 = dt.Rows[0]["MNC_LB_PRI19"].ToString();
                labM02.MNC_LB_PRI20 = dt.Rows[0]["MNC_LB_PRI20"].ToString();
                labM02.MNC_LB_RFN = dt.Rows[0]["MNC_LB_RFN"].ToString();
                labM02.MNC_LBC_CD = dt.Rows[0]["MNC_LBC_CD"].ToString();
                labM02.MNC_LBC_PRI = dt.Rows[0]["MNC_LBC_PRI"].ToString();
                labM02.MNC_LBC_COS = dt.Rows[0]["MNC_LBC_COS"].ToString();
                labM02.MNC_CHANGE_PRI_STS = dt.Rows[0]["MNC_CHANGE_PRI_STS"].ToString();
                labM02.MNC_SHOW_PRI_STS = dt.Rows[0]["MNC_SHOW_PRI_STS"].ToString();
                labM02.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                labM02.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                labM02.MNC_USR_ADD = dt.Rows[0]["MNC_USR_ADD"].ToString();
                labM02.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                labM02.MNC_CHARGE_NO = dt.Rows[0]["MNC_CHARGE_NO"].ToString();

            }
            else
            {
                setLabM02(labM02);
            }
            return labM02;
        }

        public LabM02 setLabM02(LabM02 p)
        {
            p.MNC_LB_CD = "";
            p.MNC_FN_CD = "";
            p.MNC_PRI_STS = "";
            p.MNC_LB_PRI = "";
            p.MNC_LB_COS = "";
            p.MNC_LB_PRI01 = "";
            p.MNC_LB_PRI02 = "";
            p.MNC_LB_PRI03 = "";
            p.MNC_LB_PRI04 = "";
            p.MNC_LB_PRI05 = "";
            p.MNC_LB_PRI06 = "";
            p.MNC_LB_PRI07 = "";
            p.MNC_LB_PRI08 = "";
            p.MNC_LB_PRI09 = "";
            p.MNC_LB_PRI10 = "";
            p.MNC_LB_PRI11 = "";
            p.MNC_LB_PRI12 = "";
            p.MNC_LB_PRI13 = "";
            p.MNC_LB_PRI14 = "";
            p.MNC_LB_PRI15 = "";
            p.MNC_LB_PRI16 = "";
            p.MNC_LB_PRI17 = "";
            p.MNC_LB_PRI18 = "";
            p.MNC_LB_PRI19 = "";
            p.MNC_LB_PRI20 = "";
            p.MNC_LB_RFN = "";
            p.MNC_LBC_CD = "";
            p.MNC_LBC_PRI = "";
            p.MNC_LBC_COS = "";
            p.MNC_CHANGE_PRI_STS = "";
            p.MNC_SHOW_PRI_STS = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_ADD = "";
            p.MNC_USR_UPD = "";
            p.MNC_CHARGE_NO = "";
            return p;
        }


    }



}
