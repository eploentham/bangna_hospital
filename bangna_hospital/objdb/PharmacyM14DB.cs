using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PharmacyM14DB
    {
        public PharmacyM14 pharM14;
        ConnectDB conn;
        DataTable DTALL;
        public PharmacyM14DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharM14 = new PharmacyM14();
            pharM14.MNC_PH_GRP_CD = "MNC_PH_GRP_CD";
            pharM14.MNC_PH_GRP_DSC = "MNC_PH_GRP_DSC";
            pharM14.MNC_PH_GRP_STS = "MNC_PH_GRP_STS";
            pharM14.MNC_PH_STS = "MNC_PH_STS";
            pharM14.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pharM14.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pharM14.MNC_USR_ADD = "MNC_USR_ADD";
            pharM14.MNC_USR_UPD = "MNC_USR_UPD";

            pharM14.table = "PHARMACY_M14";
            DTALL = new DataTable();
        }
        public DataTable SelectAll()
        {
            if (DTALL.Rows.Count <= 0)
            {
                String sql = "select * " +
                "From PHARMACY_M14  ";
                DTALL = conn.selectData(sql);
                //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            }
            return DTALL;
        }
    }
}
