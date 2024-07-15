using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LabM04DB
    {
        public LabM04 labM04;
        ConnectDB conn;
        public LabM04DB(ConnectDB c) {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labM04 = new LabM04();
            labM04.MNC_LB_CD = "MNC_LB_CD";
            labM04.MNC_LB_RES_CD = "MNC_LB_RES_CD";
            labM04.MNC_LB_RES = "MNC_LB_RES";
            labM04.MNC_LB_RES_MIN = "MNC_LB_RES_MIN";
            labM04.MNC_LB_RES_MAX = "MNC_LB_RES_MAX";
            labM04.MNC_RES_GRP_CD = "MNC_RES_GRP_CD";
            labM04.MNC_RES_UNT = "MNC_RES_UNT";
            labM04.MNC_ORD_NO = "MNC_ORD_NO";
            labM04.MNC_LAB_PRN = "MNC_LAB_PRN";

            labM04.table = "LAB_M04";
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select * " +
                "From LAB_M04  ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectAllBylabcode(String labcode)
        {
            DataTable dt = new DataTable();
            String sql = "Select labm04.MNC_LB_CD,labm01.MNC_LB_DSC,labm01.MNC_LB_TYP_CD,labm01.MNC_LB_GRP_CD,labm01.MNC_LB_DIS_STS" +
                ", labm04.MNC_LB_RES_CD, labm04.MNC_LB_RES, labm04.MNC_LB_RES_MIN,labm04.MNC_LB_RES_MAX,labm04.MNC_RES_GRP_CD,labm04.MNC_RES_UNT" +
                ", labm04.MNC_ORD_NO,labm04.MNC_LAB_PRN,labm13.MNC_RES_GRP_DSC,labm10.MNC_KW_DSC " +
                "From lab_m01 labm01 " +
                "Left join lab_m02 labm02 on labm01.MNC_LB_CD = labm02.MNC_LB_CD " +
                "Left join LAB_M04 labm04 on labm01.MNC_LB_CD = labm04.MNC_LB_CD " +
                "Left Join LAB_M13 labm13 on labm04.MNC_RES_GRP_CD = labm13.MNC_RES_GRP_CD " +
                //"Left Join LAB_M14 labm14 on labm04.MNC_RES_GRP_CD = labm14.MNC_RES_GRP_CD " +
                "Left Join LAB_M10 labm10 on labm04.mnc_kw_cd = labm10.MNC_KW_CD " +
                "Where labm04.MNC_LB_CD = '" + labcode + "' " +
                "Order By labm04.MNC_LB_RES_CD ";
            dt = conn.selectData(sql);

            return dt;
        }
    }
}
