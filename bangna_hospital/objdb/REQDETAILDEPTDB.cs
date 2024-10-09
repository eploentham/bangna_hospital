using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class REQDETAILDEPTDB
    {
        public REQDETAILDEPT reqd;
        ConnectDB conn;
        public REQDETAILDEPTDB(ConnectDB c) 
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            reqd = new REQDETAILDEPT();
            reqd.REQNo = "REQNo";
            reqd.Line = "Line";
            reqd.InvCode = "InvCode";
            reqd.InvUnitCode = "InvUnitCode";
            reqd.Qty = "Qty";
            reqd.Cost = "Cost";
            reqd.LotNo = "LotNo";
            reqd.Remain = "Remain";
            reqd.UpDateUser = "UpDateUser";
            reqd.UpDateDate = "UpDateDate";

            reqd.InvUnitCode2 = "InvUnitCode2";
            reqd.table = "REQDETAIL_DEPT";
        }
        public DataTable SelectByReqno(String reqid)
        {
            DataTable dt = new DataTable();
            
            String sql = "Select reqd.REQNo,reqd.Line,reqd.InvCode,reqd.InvUnitCode,reqd.Qty, reqd.Cost" +
                ", reqd.LotNo, reqd.Remain, reqd.UpDateUser, reqd.UpDateDate,pharm01.MNC_PH_TN " +
                "From REQDETAIL_DEPT reqd " +
                "left join BNG5_DBMS_FRONT.dbo.PHARMACY_M01 pharm01 on reqd.InvCode = pharm01.MNC_PH_CD  " +
                "Where reqd.REQNo = '" + reqid + "' Order By Line";
            dt = conn.selectData(conn.connBACK,sql);

            return dt;
        }
    }
}
