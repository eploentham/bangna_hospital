using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PharmacyT02DB
    {
        public PharmacyT02 pharT02;
        ConnectDB conn;
        public PharmacyT02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharT02 = new PharmacyT02();
            pharT02.MNC_DOC_CD = "MNC_DOC_CD";
            pharT02.MNC_REQ_YR = "MNC_REQ_YR";
            pharT02.MNC_REQ_NO = "MNC_REQ_NO";
            pharT02.MNC_PH_CD = "MNC_PH_CD";
            pharT02.MNC_PH_QTY = "MNC_PH_QTY";
            pharT02.MNC_PH_UNTF_QTY = "MNC_PH_UNTF_QTY";
            pharT02.MNC_PH_UNT_CD = "MNC_PH_UNT_CD";
            pharT02.MNC_PH_DIR_DSC = "MNC_PH_DIR_DSC";
            pharT02.MNC_PH_PRI = "MNC_PH_PRI";
            pharT02.MNC_PH_COS = "MNC_PH_COS";
            pharT02.MNC_SUP_STS = "MNC_SUP_STS";
            pharT02.MNC_ORD_NO = "MNC_ORD_NO";
            pharT02.MNC_PH_RFN = "MNC_PH_RFN";
            pharT02.MNC_PH_DIR_CD = "MNC_PH_DIR_CD";
            pharT02.MNC_PH_FRE_CD = "MNC_PH_FRE_CD";
            pharT02.MNC_PH_TIM_CD = "MNC_PH_TIM_CD";
            pharT02.MNC_PH_CAU = "MNC_PH_CAU";
            pharT02.MNC_PH_IND = "MNC_PH_IND";
            pharT02.MNC_FN_CD = "MNC_FN_CD";
            pharT02.MNC_PHA_HID = "MNC_PHA_HID";
            pharT02.MNC_PH_FLG = "MNC_PH_FLG";
            pharT02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pharT02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pharT02.MNC_USR_ADD = "MNC_USR_ADD";
            pharT02.MNC_USR_UPD = "MNC_USR_UPD";
            pharT02.MNC_PH_DIR_TXT = "MNC_PH_DIR_TXT";
            pharT02.MNC_CANCEL_STS = "MNC_CANCEL_STS";
            pharT02.MNC_PH_REM = "MNC_PH_REM";
            pharT02.MNC_PAY_FLAG = "MNC_PAY_FLAG";
            pharT02.MNC_PH_STS = "MNC_PH_STS";

            pharT02.table = "pharmacy_t02";
        }
        private void chkNull(PharmacyT02 p)
        {
            long chk = 0;
            int chk1 = 0;
            decimal chk2 = 0;

            p.MNC_DOC_CD = p.MNC_DOC_CD == null ? "" : p.MNC_DOC_CD;
            p.MNC_PH_CD = p.MNC_PH_CD == null ? "" : p.MNC_PH_CD;
            p.MNC_PH_UNT_CD = p.MNC_PH_UNT_CD == null ? "" : p.MNC_PH_UNT_CD;
            p.MNC_PH_DIR_DSC = p.MNC_PH_DIR_DSC == null ? "" : p.MNC_PH_DIR_DSC;
            p.MNC_SUP_STS = p.MNC_SUP_STS == null ? "" : p.MNC_SUP_STS;
            p.MNC_PH_DIR_CD = p.MNC_PH_DIR_CD == null ? "" : p.MNC_PH_DIR_CD;
            p.MNC_PH_FRE_CD = p.MNC_PH_FRE_CD == null ? "" : p.MNC_PH_FRE_CD;
            p.MNC_PH_TIM_CD = p.MNC_PH_TIM_CD == null ? "" : p.MNC_PH_TIM_CD;
            p.MNC_PH_CAU = p.MNC_PH_CAU == null ? "" : p.MNC_PH_CAU;
            p.MNC_PH_IND = p.MNC_PH_IND == null ? "" : p.MNC_PH_IND;
            p.MNC_FN_CD = p.MNC_FN_CD == null ? "" : p.MNC_FN_CD;
            p.MNC_PHA_HID = p.MNC_PHA_HID == null ? "" : p.MNC_PHA_HID;
            p.MNC_PH_FLG = p.MNC_PH_FLG == null ? "" : p.MNC_PH_FLG;
            p.MNC_STAMP_DAT = p.MNC_STAMP_DAT == null ? "" : p.MNC_STAMP_DAT;
            p.MNC_USR_ADD = p.MNC_USR_ADD == null ? "" : p.MNC_USR_ADD;
            p.MNC_USR_UPD = p.MNC_USR_UPD == null ? "" : p.MNC_USR_UPD;
            p.MNC_PH_DIR_TXT = p.MNC_PH_DIR_TXT == null ? "" : p.MNC_PH_DIR_TXT;
            p.MNC_CANCEL_STS = p.MNC_CANCEL_STS == null ? "" : p.MNC_CANCEL_STS;
            p.MNC_PH_REM = p.MNC_PH_REM == null ? "" : p.MNC_PH_REM;
            p.MNC_PAY_FLAG = p.MNC_PAY_FLAG == null ? "" : p.MNC_PAY_FLAG;
            p.MNC_PH_STS = p.MNC_PH_STS == null ? "" : p.MNC_PH_STS;
            //p.MNC_DOC_CD1 = p.MNC_DOC_CD1 == null ? "" : p.MNC_DOC_CD1;

            p.MNC_REQ_YR = long.TryParse(p.MNC_REQ_YR, out chk) ? chk.ToString() : "0";
            p.MNC_REQ_NO = long.TryParse(p.MNC_REQ_NO, out chk) ? chk.ToString() : "0";
            p.MNC_ORD_NO = long.TryParse(p.MNC_ORD_NO, out chk) ? chk.ToString() : "0";
            p.MNC_STAMP_TIM = long.TryParse(p.MNC_STAMP_TIM, out chk) ? chk.ToString() : "0";

            p.MNC_PH_QTY = decimal.TryParse(p.MNC_PH_QTY, out chk2) ? chk2.ToString() : "0";
            p.MNC_PH_UNTF_QTY = decimal.TryParse(p.MNC_PH_UNTF_QTY, out chk2) ? chk2.ToString() : "0";
            p.MNC_PH_PRI = decimal.TryParse(p.MNC_PH_PRI, out chk2) ? chk2.ToString() : "0";
            p.MNC_PH_COS = decimal.TryParse(p.MNC_PH_COS, out chk2) ? chk2.ToString() : "0";
            p.MNC_PH_RFN = decimal.TryParse(p.MNC_PH_RFN, out chk2) ? chk2.ToString() : "0";
            
        }
        public DataTable selectReq(String hnyr, String hn, String vsdate, String preno)
        {
            String sql = "";
            DataTable dt = new DataTable();

            sql = "Select phart02.*, pharm01.mnc_ph_tn, pharm01.mnc_ph_id, pharm01.mnc_ph_ctl_cd, pharm01.mnc_ph_gn " +
                ", pharm01.mnc_ph_unt_cd, pharm01.mnc_ph_grp_cd, pharm01.mnc_ph_typ_cd " +
                ", pharm01.mnc_ph_dir_cd, pm04.mnc_ph_dir_dsc, pharm01.MNC_PH_CAU_CD, pm11.MNC_PH_CAU_dsc " +
                "From " +pharT02.table+" phart02 " +
                "inner join pharmacy_t01 phart01 on phart02.mnc_doc_cd = phart01.mnc_doc_cd and phart02.mnc_req_no = phart01.mnc_req_no and phart02.mnc_req_yr = phart01.mnc_req_yr " +
                "Left Join pharmacy_m01 pharm01 on phart02.mnc_ph_cd = pharm01.mnc_ph_cd  " +
                "Left join pharmacy_m04 pm04 on pharm01.MNC_PH_DIR_CD = pm04.MNC_PH_DIR_CD " +
                "Left join PHARMACY_M11 pm11 on pharm01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD " +
                "Where phart01.mnc_hn_no = '" +hn+"' and phart01.mnc_hn_yr = '"+hnyr+"' and phart01.mnc_pre_no = '"+preno+"' and phart01.mnc_date = '"+vsdate+"' ";
            dt = conn.selectData(sql);
            return dt;
        }
        public String insertPharmacyT02(PharmacyT02 p, String userId)
        {
            String sql = "",re="";

            chkNull(p);
            sql = "Insert Into pharmacy_t02 " +
                "(MNC_DOC_CD,MNC_REQ_YR,MNC_REQ_NO,MNC_PH_CD" +
                ",MNC_PH_QTY,MNC_PH_UNTF_QTY,MNC_PH_UNT_CD,MNC_PH_DIR_DSC" +
                ",MNC_PH_PRI, MNC_PH_COS, MNC_SUP_STS,MNC_ORD_NO" +
                ",MNC_PH_RFN,MNC_PH_DIR_CD,MNC_PH_FRE_CD,MNC_PH_TIM_CD" +
                ",MNC_PH_CAU,MNC_PH_IND,MNC_FN_CD,MNC_PHA_HID" +
                ",MNC_PH_FLG,MNC_STAMP_DAT,MNC_STAMP_TIM,MNC_USR_ADD" +
                ",MNC_USR_UPD,MNC_PH_DIR_TXT,MNC_CANCEL_STS,MNC_PH_REM" +
                ",MNC_PAY_FLAG,MNC_PH_STS" +
                ")" +
                "Values ('"+p.MNC_DOC_CD+"','"+p.MNC_REQ_YR+"','"+p.MNC_REQ_NO+"','"+p.MNC_PH_CD+"'" +
                ",'" + p.MNC_PH_QTY + "','" + p.MNC_PH_UNTF_QTY + "','" + p.MNC_PH_UNT_CD + "','" + p.MNC_PH_DIR_DSC + "'" +
                ",'" + p.MNC_PH_PRI + "','" + p.MNC_PH_COS + "','" + p.MNC_SUP_STS + "','" + p.MNC_ORD_NO + "'" +
                ",'" + p.MNC_PH_RFN + "','" + p.MNC_PH_DIR_CD + "','" + p.MNC_PH_FRE_CD + "','" + p.MNC_PH_TIM_CD + "'" +
                ",'" + p.MNC_PH_CAU + "','" + p.MNC_PH_IND + "','" + p.MNC_FN_CD + "','" + p.MNC_PHA_HID + "'" +
                ",'" + p.MNC_PH_FLG + "','" + p.MNC_STAMP_DAT + "','" + p.MNC_STAMP_TIM + "','" + p.MNC_USR_ADD + "'" +
                ",'" + p.MNC_USR_UPD + "','" + p.MNC_PH_DIR_TXT + "','" + p.MNC_CANCEL_STS + "','" + p.MNC_PH_REM + "'" +
                ",'" + p.MNC_PAY_FLAG + "','" + p.MNC_PH_STS + "'" +
                ")";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                new LogWriter("d", "PharmacyT02 insertPharmacyT02 sql " + sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "PharmacyT02 insertPharmacyT02 " + ex.InnerException);
            }

            return re;
        }
        public String deleteReqNo(String reqyr, String reqno)
        {
            String sql = "", re = "";
            sql = "Delete From pharmacy_t02 Where mnc_doc_cd = 'ROS' and mnc_req_yr = '" + reqyr + "' and mnc_req_no = '" + reqno + "'";

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
        public PharmacyT02 setPharmacyT02(DataTable dt)
        {
            PharmacyT02 pharT02 = new PharmacyT02();
            if (dt.Rows.Count > 0)
            {
                pharT02.MNC_DOC_CD = dt.Rows[0]["MNC_DOC_CD"].ToString();
                pharT02.MNC_REQ_YR = dt.Rows[0]["MNC_REQ_YR"].ToString();
                pharT02.MNC_REQ_NO = dt.Rows[0]["MNC_REQ_NO"].ToString();
                pharT02.MNC_PH_CD = dt.Rows[0]["MNC_PH_CD"].ToString();
                pharT02.MNC_PH_QTY = dt.Rows[0]["MNC_PH_QTY"].ToString();
                pharT02.MNC_PH_UNTF_QTY = dt.Rows[0]["MNC_PH_UNTF_QTY"].ToString();
                pharT02.MNC_PH_UNT_CD = dt.Rows[0]["MNC_PH_UNT_CD"].ToString();
                pharT02.MNC_PH_DIR_DSC = dt.Rows[0]["MNC_PH_DIR_DSC"].ToString();
                pharT02.MNC_PH_PRI = dt.Rows[0]["MNC_PH_PRI"].ToString();
                pharT02.MNC_PH_COS = dt.Rows[0]["MNC_PH_COS"].ToString();
                pharT02.MNC_SUP_STS = dt.Rows[0]["MNC_SUP_STS"].ToString();
                pharT02.MNC_ORD_NO = dt.Rows[0]["MNC_ORD_NO"].ToString();
                pharT02.MNC_PH_RFN = dt.Rows[0]["MNC_PH_RFN"].ToString();
                pharT02.MNC_PH_DIR_CD = dt.Rows[0]["MNC_PH_DIR_CD"].ToString();
                pharT02.MNC_PH_FRE_CD = dt.Rows[0]["MNC_PH_FRE_CD"].ToString();
                pharT02.MNC_PH_TIM_CD = dt.Rows[0]["MNC_PH_TIM_CD"].ToString();
                pharT02.MNC_PH_CAU = dt.Rows[0]["MNC_PH_CAU"].ToString();
                pharT02.MNC_FN_CD = dt.Rows[0]["MNC_FN_CD"].ToString();
                pharT02.MNC_PHA_HID = dt.Rows[0]["MNC_PHA_HID"].ToString();
                pharT02.MNC_PH_FLG = dt.Rows[0]["MNC_PH_FLG"].ToString();
                pharT02.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                pharT02.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                pharT02.MNC_USR_ADD = dt.Rows[0]["MNC_USR_ADD"].ToString();
                pharT02.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                pharT02.MNC_PH_DIR_TXT = dt.Rows[0]["MNC_PH_DIR_TXT"].ToString();
                pharT02.MNC_CANCEL_STS = dt.Rows[0]["MNC_CANCEL_STS"].ToString();
                pharT02.MNC_PH_REM = dt.Rows[0]["MNC_PH_REM"].ToString();
                pharT02.MNC_PAY_FLAG = dt.Rows[0]["MNC_PAY_FLAG"].ToString();
                pharT02.MNC_PH_STS = dt.Rows[0]["MNC_PH_STS"].ToString();
                //pharT02.MNC_AN_NO = dt.Rows[0]["MNC_AN_NO"].ToString();
            }
            else
            {
                setPharmacyT02(pharT02);
            }
            return pharT02;
        }
        public PharmacyT02 setPharmacyT02(PharmacyT02 p)
        {
            p.MNC_DOC_CD = "";
            p.MNC_REQ_YR = "";
            p.MNC_REQ_NO = "";
            p.MNC_PH_CD = "";
            p.MNC_PH_QTY = "";
            p.MNC_PH_UNTF_QTY = "";
            p.MNC_PH_UNT_CD = "";
            p.MNC_PH_DIR_DSC = "";
            p.MNC_PH_PRI = "";
            p.MNC_PH_COS = "";
            p.MNC_SUP_STS = "";
            p.MNC_ORD_NO = "";
            p.MNC_PH_RFN = "";
            p.MNC_PH_DIR_CD = "";
            p.MNC_PH_FRE_CD = "";
            p.MNC_PH_TIM_CD = "";
            p.MNC_PH_CAU = "";
            p.MNC_PH_IND = "";
            p.MNC_FN_CD = "";
            p.MNC_PHA_HID = "";
            p.MNC_PH_FLG = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_ADD = "";
            p.MNC_USR_UPD = "";
            p.MNC_PH_DIR_TXT = "";
            p.MNC_CANCEL_STS = "";
            p.MNC_PH_REM = "";
            p.MNC_PAY_FLAG = "";
            p.MNC_PH_STS = "";
            //p.MNC_AN_NO = "";
            return p;
        }
    }
}
