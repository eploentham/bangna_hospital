using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LabM06DB
    {
        public LabM06 labm06;
        ConnectDB conn;
        DataTable DTALL;
        public LabM06DB(ConnectDB c) 
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labm06 = new LabM06();
            labm06.MNC_LB_GRP_CD = "MNC_LB_GRP_CD";
            labm06.MNC_LB_GRP_DSC = "MNC_LB_GRP_DSC";
            labm06.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            labm06.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            labm06.MNC_USR_ADD = "MNC_USR_ADD";
            labm06.MNC_USR_UPD = "MNC_USR_UPD";
            labm06.MNC_ORDER = "MNC_ORDER";
            labm06.MNC_COLOR = "MNC_COLOR";

            labm06.table = "LAB_M06";
            DTALL = new DataTable();
        }
        public DataTable selectAll()
        {
            if (DTALL.Rows.Count <= 0)
            {
                String sql = "", re = "";
                sql = "Select labm06.* " +
                    "From  LAB_M06 labm06 ";
                //" Where pm08.MNC_TYP_PT = 'I' ";
                DTALL = conn.selectData(conn.connMainHIS, sql);
            }
            return DTALL;
        }
    }
}
