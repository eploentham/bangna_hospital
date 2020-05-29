using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class DrugDB
    {
        public ConnectDB conn;
        public DrugDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

        }
        public DataTable selectDrugAll()
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select  pm01.mnc_ph_cd, pm01.mnc_ph_id, pm01.mnc_ph_ctl_cd, pm01.mnc_ph_tn, pm01.mnc_ph_gn, pm01.mnc_ph_unt_cd, pm01.mnc_ph_grp_cd, pm01.mnc_ph_typ_cd " +
                "From  pharmacy_m01 pm01  " +

                " Where pm01.MNC_ph_STS = 'Y' and pm01.mnc_ph_typ_flg = 'P' " +
                "Order By pm01.mnc_ph_tn";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectSupplyAll()
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select  pm01.mnc_ph_cd, pm01.mnc_ph_id, pm01.mnc_ph_ctl_cd, pm01.mnc_ph_tn, pm01.mnc_ph_gn, pm01.mnc_ph_unt_cd, pm01.mnc_ph_grp_cd, pm01.mnc_ph_typ_cd " +
                "From  pharmacy_m01 pm01  " +

                " Where pm01.MNC_ph_STS = 'Y'  and pm01.mnc_ph_typ_flg = 'O' " +
                "Order By pm01.mnc_ph_tn";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectDrugByCode(String code)
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select  pm01.mnc_ph_cd, pm01.mnc_ph_id, pm01.mnc_ph_ctl_cd, pm01.mnc_ph_tn, pm01.mnc_ph_gn, pm01.mnc_ph_unt_cd, pm01.mnc_ph_grp_cd, pm01.mnc_ph_typ_cd " +
                "From  pharmacy_m01 pm01  " +

                " Where pm01.MNC_ph_STS = 'Y' and pm01.mnc_ph_cd = '"+ code+"' "+
                "Order By pm01.mnc_ph_tn";
            dt = conn.selectData(sql);

            return dt;
        }
    }
}
