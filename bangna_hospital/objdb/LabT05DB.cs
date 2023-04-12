using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LabT05DB
    {
        public LabT05 labT05;
        ConnectDB conn;
        public LabT05DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labT05 = new LabT05();
            labT05.MNC_REQ_YR = "MNC_REQ_YR";
            labT05.MNC_REQ_NO = "MNC_REQ_NO";
            labT05.MNC_REQ_DAT = "MNC_REQ_DAT";
            labT05.MNC_LB_CD = "MNC_LB_CD";
            labT05.MNC_LB_RES_CD = "MNC_LB_RES_CD";
            labT05.MNC_LB_USR = "MNC_LB_USR";
            labT05.MNC_RES = "MNC_RES";
            labT05.MNC_RES_MAX = "MNC_RES_MAX";
            labT05.MNC_RES_MIN = "MNC_RES_MIN";
            labT05.MNC_RES_VALUE = "MNC_RES_VALUE";
            labT05.MNC_LB_RES = "MNC_LB_RES";
            labT05.MNC_LB_ACT = "MNC_LB_ACT";
            labT05.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            labT05.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            labT05.MNC_STS = "MNC_STS";
            labT05.MNC_LAB_PRN = "MNC_LAB_PRN";
            labT05.MNC_RES_UNT = "MNC_RES_UNT";

            labT05.table = "lab_t05";

        }

        private void chkNull(LabT05 p)
        {
            long chk = 0;
            int chk1 = 0;

            p.MNC_REQ_YR = p.MNC_REQ_YR == null ? "" : p.MNC_REQ_YR;
            p.MNC_REQ_NO = p.MNC_REQ_NO == null ? "" : p.MNC_REQ_NO;
            p.MNC_REQ_DAT = p.MNC_REQ_DAT == null ? "" : p.MNC_REQ_DAT;
            p.MNC_LB_CD = p.MNC_LB_CD == null ? "" : p.MNC_LB_CD;
            p.MNC_LB_RES_CD = p.MNC_LB_RES_CD == null ? "" : p.MNC_LB_RES_CD;
            p.MNC_LB_USR = p.MNC_LB_USR == null ? "" : p.MNC_LB_USR;
            p.MNC_RES = p.MNC_RES == null ? "" : p.MNC_RES;
            p.MNC_RES_MAX = p.MNC_RES_MAX == null ? "" : p.MNC_RES_MAX;
            p.MNC_RES_MIN = p.MNC_RES_MIN == null ? "" : p.MNC_RES_MIN;
            p.MNC_RES_VALUE = p.MNC_RES_VALUE == null ? "" : p.MNC_RES_VALUE;
            p.MNC_LB_RES = p.MNC_LB_RES == null ? "" : p.MNC_LB_RES;
            p.MNC_STAMP_DAT = p.MNC_STAMP_DAT == null ? "" : p.MNC_STAMP_DAT;
            p.MNC_STAMP_TIM = p.MNC_STAMP_TIM == null ? "" : p.MNC_STAMP_TIM;
            p.MNC_STS = p.MNC_STS == null ? "" : p.MNC_STS;
            p.MNC_LB_ACT = p.MNC_LB_ACT == null ? "" : p.MNC_LB_ACT;
        }
        public String insertLabT05(LabT05 p)
        {
            String sql = "", chk = "", re = "";

            chkNull(p);
            if (p.MNC_REQ_NO.Equals("0"))
            {
                re = insertLabT05(p, "");
            }
            else
            {

            }
            return re;
        }
        public String deleteLabT05(String reqyr, String reqno, String reqdate, String lab_code, String lab_sub_code)
        {
            String sql = "", chk = "", re = "";
            try
            {
                sql = "Delete From lab_t05  "
                    + "Where MNC_REQ_YR = '" + reqyr + "' and MNC_REQ_DAT = '" + reqdate + "' and MNC_REQ_NO = '"+ reqno+ "' and MNC_LB_RES_CD = '"+ lab_sub_code + "' and MNC_LB_CD = '"+ lab_code+"' ";
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "insert_lab_t05 " + ex.Message + " " + ex.InnerException);
            }
            return re;
        }
        public String insertLabT05(LabT05 p, String userid)
        {
            String sql = "", chk = "", re = "";
            try
            {
                sql = "insert into lab_t05 ("+labT05.MNC_REQ_YR+","+labT05.MNC_REQ_NO+","+labT05.MNC_REQ_DAT+","+labT05.MNC_LB_CD+"," 
                    +labT05.MNC_LB_RES_CD+","+labT05.MNC_RES+","+labT05.MNC_RES_VALUE+","+labT05.MNC_RES_UNT+","
                    +labT05.MNC_RES_MAX+","+labT05.MNC_RES_MIN+","+labT05.MNC_LB_USR+","+labT05.MNC_LB_ACT+","
                    +labT05.MNC_LAB_PRN+","+labT05.MNC_STAMP_DAT+","+labT05.MNC_STAMP_TIM+","+labT05.MNC_STS+",status_insert_pop"+
                    "," + labT05.MNC_LB_RES + ") "
                    +"values('"+p.MNC_REQ_YR+"','"+p.MNC_REQ_NO+"','"+p.MNC_REQ_DAT+"','"+p.MNC_LB_CD+"','"
                    +p.MNC_LB_RES_CD+"','"+p.MNC_RES+"','"+p.MNC_RES_VALUE+"','"+p.MNC_RES_UNT+"','"
                    +p.MNC_RES_MAX+"','"+p.MNC_RES_MIN+"','"+p.MNC_LB_USR+"','"+p.MNC_LB_ACT+"','"
                    +p.MNC_LAB_PRN+"','"+p.MNC_STAMP_DAT+"','"+p.MNC_STAMP_TIM+"','"+p.MNC_STS+"','1'" +
                    ",'" + p.MNC_LB_RES+"')";
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch(Exception ex)
            {
                new LogWriter("e", "insert_lab_t05 " + ex.Message+" "+ ex.InnerException);
            }
            return re;
        }
    }
}
