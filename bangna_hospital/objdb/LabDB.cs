using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class LabDB
    {
        public ConnectDB conn;
        public LabDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

        }
        public DataTable selectLabAll()
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select  lab01.mnc_lb_cd, lab01.mnc_lb_dsc, lab01.mnc_lb_typ_cd, lab01.mnc_lb_grp_cd,lab_m06.MNC_LB_GRP_DSC " +
                ",lab01.MNC_LB_GRP_CD,lab_m07.MNC_LB_TYP_DSC " +
                "From  lab_m01 lab01  " +
                "inner join LAB_M06 on lab01.MNC_LB_GRP_CD = LAB_M06.MNC_LB_GRP_CD " +
                "inner join LAB_M07 on lab01.MNC_LB_TYP_CD= LAB_M07.MNC_LB_TYP_CD and lab01.MNC_LB_GRP_CD = LAB_M07.MNC_LB_GRP_CD " +

                " Where lab01.MNC_lb_STS = 'Y'  " +
                "Order By lab01.mnc_lb_cd";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectLabByCode(String code)
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select  lab01.mnc_lb_cd, lab01.mnc_lb_dsc, lab01.mnc_lb_typ_cd, lab01.mnc_lb_grp_cd " +
                "From  lab_m01 lab01  " +

                " Where lab01.MNC_lb_STS = 'Y' and lab01.mnc_lb_cd = '"+code+"' " +
                "Order By lab01.mnc_lb_cd";
            dt = conn.selectData(sql);

            return dt;
        }
    }
}
