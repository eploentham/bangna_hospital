using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LabM07DB
    {
        public LabM07 labm07;
        ConnectDB conn;
        public LabM07DB(ConnectDB c) 
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labm07 = new LabM07();
            labm07.MNC_LB_GRP_CD = "MNC_LB_GRP_CD";
            labm07.MNC_LB_TYP_CD = "MNC_LB_TYP_CD";
            labm07.MNC_LB_TYP_DSC = "MNC_LB_TYP_DSC";
            labm07.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            labm07.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            labm07.MNC_USR_ADD = "MNC_USR_ADD";
            labm07.MNC_USR_UPD = "MNC_USR_UPD";

            labm07.table = "LAB_M07";

        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select labm07.MNC_LB_GRP_CD,labm07.MNC_LB_TYP_CD,labm07.MNC_STAMP_DAT,labm07.MNC_STAMP_TIM,labm07.MNC_USR_ADD,labm07.MNC_USR_UPD,labm07.MNC_LB_TYP_DSC " +
                "From  LAB_M07 labm07 " +
            " Order By labm07.MNC_ORDER ";
            dt = conn.selectData(conn.connMainHIS, sql);
            
            return dt;
        }
        public DataTable selectByGrpcode(String grpcode)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select labm07.MNC_LB_GRP_CD,labm07.MNC_LB_TYP_CD,labm07.MNC_STAMP_DAT,labm07.MNC_STAMP_TIM,labm07.MNC_USR_ADD,labm07.MNC_USR_UPD,labm07.MNC_LB_TYP_DSC " +
                "From  LAB_M07 labm07 " +
                "Where labm07.MNC_LB_GRP_CD = '" +grpcode+"' " +
            " Order By labm07.MNC_LB_TYP_CD ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
    }
}
