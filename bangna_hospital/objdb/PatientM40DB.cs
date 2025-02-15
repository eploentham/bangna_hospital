using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PatientM40DB
    {
        public PatientM40 pm40;
        ConnectDB conn;
        public List<PatientM40> lPm02;
        public PatientM40DB(ConnectDB c) 
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm40 = new PatientM40();
            pm40.MNC_PAC_CD = "MNC_PAC_CD";
            pm40.MNC_PAC_TYP = "MNC_PAC_TYP";
            pm40.MNC_OPR_FLAG = "MNC_OPR_FLAG";
            pm40.MNC_OPR_CD = "MNC_OPR_CD";
            pm40.MNC_OPR_QTY = "MNC_OPR_QTY";
            pm40.MNC_OPR_UNT = "MNC_OPR_UNT";
            pm40.MNC_COS = "MNC_COS";
            pm40.MNC_PRI = "MNC_PRI";
            pm40.MNC_PRI_I = "MNC_PRI_I";
            pm40.MNC_STS = "MNC_STS";
            pm40.MNC_COM_CD = "MNC_COM_CD";
            pm40.MNC_OPR_DIR_DSC = "MNC_OPR_DIR_DSC";
            pm40.MNC_OPR_DIR_CD = "MNC_OPR_DIR_CD";
            pm40.MNC_MIN_PT = "MNC_MIN_PT";
            pm40.MNC_MIN_RATE = "MNC_MIN_RATE";
            pm40.MNC_DOT_CD = "MNC_DOT_CD";
            pm40.MNC_PH_DIR_CD = "MNC_PH_DIR_CD";
            pm40.MNC_PH_DOT_CD = "MNC_PH_DOT_CD";
            pm40.MNC_PH_DIR_DOT = "MNC_PH_DIR_DOT";
            pm40.MNC_PH_FRE_CD = "MNC_PH_FRE_CD";
            pm40.MNC_PH_TIM_CD = "MNC_PH_TIM_CD";
            pm40.MNC_PH_DIR_QTY = "MNC_PH_DIR_QTY";
            pm40.MNC_PH_FRE_QTY = "MNC_PH_FRE_QTY";
            pm40.MNC_OPR_DOT_CD = "MNC_OPR_DOT_CD";
            pm40.MNC_OPR_FLAG_DOT = "MNC_OPR_FLAG_DOT";
            pm40.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pm40.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pm40.MNC_USR_ADD = "MNC_USR_ADD";
            pm40.MNC_USR_UPD = "MNC_USR_UPD";
            pm40.table = "PATIENT_M40";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm40.* " +
                "From  " + pm40.table + " pm40 ";
            //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectByPackCode(String packcode)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pm40.MNC_PAC_CD,pm40.MNC_OPR_CD,pm40.MNC_OPR_FLAG , isnull(labm01.MNC_LB_DSC,'') as MNC_LB_DSC, isnull(xraym01.MNC_XR_DSC,'') as MNC_XR_DSC,isnull(dtrm02.MNC_DF_DSC,'') as MNC_DF_DSC, isnull(pm30.MNC_SR_DSC,'') as MNC_SR_DSC " +
                "From  " + pm40.table + " pm40 " +
                "left join LAB_M01 labm01 on pm40.MNC_OPR_CD = labm01.MNC_LB_CD and pm40.MNC_OPR_FLAG = 'L' " +
                "left join XRAY_M01 xraym01 on pm40.MNC_OPR_CD = xraym01.MNC_XR_CD and pm40.MNC_OPR_FLAG = 'X' " +
                "left join DOCTOR_M02 dtrm02 on pm40.MNC_OPR_CD = dtrm02.MNC_DF_CD and pm40.MNC_OPR_FLAG = 'F' " +
                "left join patient_m30 pm30 on pm40.MNC_OPR_CD = pm30.MNC_SR_CD and pm40.MNC_OPR_FLAG = 'O' " +
                " Where pm40.MNC_PAC_CD = '"+ packcode + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
    }
}
