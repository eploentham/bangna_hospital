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

            sql = "Select  lab01.mnc_lb_cd, lab01.mnc_lb_dsc, lab01.mnc_lb_typ_cd, lab01.mnc_lb_grp_cd " +
                "From  lab_m01 lab01  " +

                " Where lab01.MNC_lb_STS = 'Y'  " +
                "Order By lab01.mnc_lb_cd";
            dt = conn.selectData(sql);

            return dt;
        }
    }
}
