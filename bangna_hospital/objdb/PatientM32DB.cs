using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PatientM32DB
    {
        public PatientM32 pttM32;
        ConnectDB conn;
        public PatientM32DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pttM32 = new PatientM32();
            pttM32.mnc_md_dep_dsc = "mnc_md_dep_dsc";
            pttM32.mnc_md_dep_no = "mnc_md_dep_no";
            pttM32.mnc_sec_no = "mnc_sec_no";
            pttM32.MNC_DIV_NO = "MNC_DIV_NO";
            pttM32.MNC_TYP_PT = "MNC_TYP_PT";
            pttM32.MNC_DP_NO = "MNC_DP_NO";
            pttM32.CLINIC = "CLINIC";
            pttM32.MNC_BUD_NO = "MNC_BUD_NO";
            pttM32.MNC_REP_GRP = "MNC_REP_GRP";
            pttM32.MNC_GRP_NO = "MNC_GRP_NO";
            pttM32.MNC_GRP_SS = "MNC_GRP_SS";
            pttM32.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pttM32.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pttM32.MNC_USR_ADD = "MNC_USR_ADD";
            pttM32.MNC_USR_UPD = "MNC_USR_UPD";
            pttM32.MNC_SUB_DP_NO = "MNC_SUB_DP_NO";
            pttM32.opbkk_clinic = "opbkk_clinic";

        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select * " +
                "From patient_m32  " ;
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public String updateOPBKKCode(String dep_no, String sec_no, String div_no, String typ_pt, String opbkkcode)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update patient_m32 Set " +
                    "opbkk_clinic ='" + opbkkcode + "' " +
                    "Where mnc_md_dep_no ='" + dep_no + "' and mnc_sec_no ='"+sec_no+"' and mnc_div_no ='"+div_no+"' and mnc_typ_pt = '"+typ_pt+"'";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " PatientM30DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
    }
}
