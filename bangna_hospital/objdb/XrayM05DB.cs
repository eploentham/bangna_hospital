using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class XrayM05DB
    {
        public XrayM05 xrayM05;
        ConnectDB conn;
        DataTable DTALL;
        public XrayM05DB(ConnectDB c) 
        { 
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xrayM05 = new XrayM05();
            xrayM05.MNC_XR_GRP_CD = "MNC_XR_GRP_CD";
            xrayM05.MNC_XR_GRP_DSC = "MNC_XR_GRP_DSC";
            xrayM05.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            xrayM05.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            xrayM05.MNC_USR_ADD = "MNC_USR_ADD";
            xrayM05.MNC_USR_UPD = "MNC_USR_UPD";
            xrayM05.MNC_XR_SEQ = "MNC_XR_SEQ";
            xrayM05.MNC_XR_COLOR = "MNC_XR_COLOR";
            xrayM05.table = "XRAY_M05";
            DTALL = new DataTable();
        }
        public DataTable SelectAll()
        {
            
            if (DTALL.Rows.Count <= 0)
            {
                String sql = "select * " +
                "From XRAY_M05  ";
                DTALL = conn.selectData(sql);
                //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            }
            return DTALL;
        }
    }
}
