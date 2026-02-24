using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace bangna_hospital.objdb
{
    public class PatientM30DB
    {//Table HotCharge  ค่าหัตถการ
        public PatientM30 pttM32;
        ConnectDB conn;
        DataTable DTProcedure;
        internal AutoCompleteStringCollection AUTOProcedure;
        public PatientM30DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pttM32 = new PatientM30();
            pttM32.mnc_sr_cd = "mnc_sr_cd";
            pttM32.mnc_sr_dsc = "mnc_sr_dsc";
            pttM32.mnc_sr_qty = "mnc_sr_qty";
            pttM32.mnc_vd_cd = "mnc_vd_cd";
            pttM32.mnc_sn_dat = "mnc_sn_dat";
            pttM32.mnc_rv_dat = "mnc_rv_dat";
            pttM32.mnc_sr_grp_cd = "mnc_sr_grp_cd";
            pttM32.mnc_diagor_cd = "mnc_diagor_cd";
            pttM32.mnc_sr_sts = "mnc_sr_sts";
            pttM32.mnc_stamp_dat = "mnc_stamp_dat";
            pttM32.mnc_stamp_tim = "mnc_stamp_tim";
            pttM32.mnc_usr_add = "mnc_usr_add";
            pttM32.mnc_usr_upd = "mnc_usr_upd";
            pttM32.mnc_dep_sts = "mnc_dep_sts";
            pttM32.mnc_sr_ctl_cd = "mnc_sr_ctl_cd";
            pttM32.mnc_dec_cd = "mnc_dec_cd";
            pttM32.mnc_dec_no = "mnc_dec_no";
            pttM32.mnc_use_all = "mnc_use_all";
            pttM32.mnc_sr_pri = "mnc_sr_pri";
            pttM32.mnc_sr_min = "mnc_sr_min";
            pttM32.mnc_sr_max = "mnc_sr_max";
            pttM32.ucep_code = "ucep_code";

        }
        public AutoCompleteStringCollection setAUTOProcedure()
        {
            //lDept = new List<Position>();
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            //labM01.Clear();
            if (DTProcedure == null || DTProcedure.Rows.Count <= 0)
            {
                DTProcedure = SelectAll(); AUTOProcedure = new AutoCompleteStringCollection();
                foreach (DataRow row in DTProcedure.Rows)
                {
                    //autoSymptom.Add(row["MNC_PH_TN"].ToString() + "#" + row["MNC_PH_CD"].ToString());
                    AUTOProcedure.Add(row["MNC_SR_DSC"].ToString() + "#" + row["MNC_SR_CD"].ToString());
                }
            }
            return autoSymptom;
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select * " +
                "From patient_m30  ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public String SelectNameByPk(String labgrpcode)
        {
            DataTable dt = new DataTable();
            String re = "";
            String sql = "select pm30.* " +
                "From patient_m30 pm30 " +
                "Where pm30.MNC_SR_CD = '" + labgrpcode + "' " +
                " ";
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_SR_DSC"].ToString();
            }
            return re;
        }
        public DataTable SelectAllBySearch(String labgrpcode)
        {
            DataTable dt = new DataTable();
            String wheresearch = "";
            if (labgrpcode == "")
            {
                return dt;
            }
            wheresearch = " (pm30.MNC_SR_DSC like '" + labgrpcode + "%') ";
            if (labgrpcode != "")
            {
                wheresearch += " or (pm30.MNC_SR_CD like '" + labgrpcode + "%') ";
            }
            String sql = "select pm30.MNC_SR_DSC as name, pm30.MNC_SR_CD as code, pm301.MNC_RESULT01 " +
                "From patient_m30 pm30 " +
                "Left join PATIENT_M301 pm301 on pm30.mnc_sr_cd = pm301.mnc_sr_cd " +
                "Where " + wheresearch + " " +
                " ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable SelectAllByGroup(String labgrpcode)
        {
            DataTable dt = new DataTable();
            String sql = "select pm30.*, pm301.MNC_RESULT01 " +
                "From patient_m30 pm30 " +
                "Left join PATIENT_M301 pm301 on pm30.mnc_sr_cd = pm301.mnc_sr_cd " +
                "Where pm30.MNC_SR_GRP_CD = '" + labgrpcode + "' " +
                " ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable SelectProcedureMap()
        {
            DataTable dt = new DataTable();
            String sql = "select pm30.MNC_SR_CD, pm30.MNC_SR_DSC, pm30.MNC_SR_GRP_CD, isnull(fm01.MNC_FN_CD,'') as MNC_FN_CD, isnull(fm11.MNC_DEF_DSC,'') as MNC_DEF_DSC " +
                ",simb2.CodeBSG,simb2.BillingSubGroupTH,simb2.BillingSubGroupEN,fm01.MNC_SIMB_CD,fm01.MNC_CHARGE_CD,fm01.MNC_SUB_CHARGE_CD, pm303.MNC_CHARGE_NO  " +
                "From PATIENT_M30 pm30 " +
                "left join PATIENT_M303 pm303 on pm30.MNC_SR_CD = pm303.MNC_SR_CD  " +
                "left join FINANCE_M01 fm01 on pm303.MNC_FN_CD = fm01.MNC_FN_CD " +
                "Left join Finance_m11 fm11 on fm01.MNC_SIMB_CD = fm11.MNC_DEF_CD " +
                "Left Join SIMB2_BillingGroup simb2 on pm303.CodeBSG = simb2.CodeBSG  " +
                //"Where xray01.MNC_XR_TYP_FLG = '1' " +
                "Order By pm30.MNC_SR_CD,pm303.MNC_CHARGE_NO ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public AutoCompleteStringCollection getlProcedureAll()
        {
            //lDept = new List<Position>();
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            //labM01.Clear();
            DataTable dt = new DataTable();
            dt = SelectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                autoSymptom.Add(row["MNC_SR_DSC"].ToString() + "#" + row["MNC_SR_CD"].ToString());
                //PatientM13 cus1 = new PatientM13();
                //cus1.MNC_APP_CD = row["MNC_APP_CD"].ToString();
                //cus1.MNC_APP_DSC = row["MNC_APP_DSC"].ToString();

                //labM01.Add(cus1);
            }
            return autoSymptom;
        }
        public String updateOPBKKCode(String srcode, String opbkkcode, String ditcode)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update patient_m30 Set " +
                    "ucep_code ='" + opbkkcode + "' " +
                    ",dit_code ='" + ditcode + "' " +
                    "Where mnc_sr_cd ='" + srcode + "' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " PatientM30DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
        public String UpdateCodeBSG(String drugcode, String codebsg)
        {
            String sql = "", chk = "";
            sql = "Update PATIENT_M303 Set CodeBSG = '" + codebsg + "' " +
                "Where MNC_SR_CD ='" + drugcode + "' and ";
            try
            {
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "XrayM02DB UpdateCodeBSG Exception " + ex.Message);
            }
            return chk;
        }
    }
}
