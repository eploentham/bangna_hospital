using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class OrDB
    {
        public ConnectDB conn;
        public OrDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

        }
        public DataTable SelectDepartment()
        {
            DataTable dt = new DataTable();
            
            String sql = "Select pttm32.mnc_md_dep_no, pttm32.mnc_md_dep_dsc, pttm32.mnc_sec_no " +
                "From Patient_m32 pttm32 " +
                " " +
                "where MNC_DP_NO in ('111','302','OPE','ERI','XRY','W7A','CCU','W5W','ICU','WNS','W6A','W5M') " +

                "Order By mnc_md_dep_dsc ";
            dt = conn.selectData(sql);
            
            return dt;
        }
        public PatientM22 SelectAnesthesis(String code)
        {
            DataTable dt = new DataTable();
            String re = "";
            PatientM22 pttm22 = new PatientM22();
            String sql = "Select pttm22.mnc_oruse_cd, pttm22.mnc_oruse_dsc " +
                "From Patient_m22 pttm22 " +
                " " +
                "Where pttm22.mnc_oruse_cd = '" + code+"' "+
                "and mnc_oruse_sts = 'Y' " +
                "Order By mnc_oruse_cd ";
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)
            {
                pttm22.mnc_oruse_cd = dt.Rows[0]["mnc_oruse_cd"].ToString();
                pttm22.mnc_oruse_dsc = dt.Rows[0]["mnc_oruse_dsc"].ToString();
            }
            return pttm22;
        }
        public DataTable SelectAnesthesisAll()
        {
            DataTable dt = new DataTable();
            String re = "";
            
            String sql = "Select pttm22.mnc_oruse_cd, pttm22.mnc_oruse_dsc " +
                "From Patient_m22 pttm22 " +
                " " +
                "Where mnc_oruse_sts = 'Y' " +
                "Order By mnc_oruse_cd ";
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable SelectOrDepatmentAll()
        {
            DataTable dt = new DataTable();
            String re = "";

            String sql = "Select pttm29.mnc_diaor_grp_cd, pttm29.mnc_diaor_grp_dsc " +
                "From Patient_m29 pttm29 " +
                " " +
                //"Where mnc_oruse_sts = 'Y' " +
                "Order By mnc_diaor_grp_cd ";
            dt = conn.selectData(sql);
            return dt;
        }
    }
}
