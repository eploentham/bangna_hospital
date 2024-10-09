using bangna_hospital.object1;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class REQHEADDEPTDB
    {
        public REQHEADDEPT reqh;
        ConnectDB conn;
        public REQHEADDEPTDB(ConnectDB c) 
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            reqh = new REQHEADDEPT();
            reqh.REQNo = "REQNo";
            reqh.REQDate = "REQDate";
            reqh.strCode = "strCode";
            reqh.FlagREQ = "FlagREQ";
            reqh.DeptCode = "DeptCode";
            reqh.DueDate = "DueDate";
            reqh.REQUser = "REQUser";
            reqh.AppUser = "AppUser";
            reqh.REQSts = "REQSts";
            reqh.Remark = "Remark";

            reqh.CancelRemark = "CancelRemark";
            reqh.FlagPrint = "FlagPrint";
            reqh.UpDateUser = "UpDateUser";
            reqh.UpDateDate = "UpDateDate";
            reqh.RVNO = "RVNO";
            reqh.strsts = "strsts";
            reqh.DeptReqFlag = "DeptReqFlag";

            reqh.table = "REQHEAD_DEPT";
        }
        public DataTable SelectByReqno(String reqid)
        {
            DataTable dt = new DataTable();

            String sql = "Select reqh.REQNo,reqh.REQDate,reqh.strCode,reqh.FlagREQ,reqh.DeptCode, reqh.DueDate" +
                ", reqh.REQUser, reqh.AppUser, reqh.REQSts, reqh.Remark, reqh.CancelRemark, reqh.FlagPrint, reqh.UpDateUser, reqh.RVNO " +
                ", reqh.strsts, reqh.DeptReqFlag " +
                "From REQHEAD_DEPT reqh " +
                "Where reqh.REQNo = '" + reqid + "' ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable SelectBYYear(String reqid)
        {
            DataTable dt = new DataTable();

            String sql = "Select reqh.REQNo,convert(varchar(20),reqh.REQDate,23) as REQDate,reqh.strCode,reqh.FlagREQ,reqh.DeptCode, reqh.DueDate" +
                ", reqh.REQUser, reqh.AppUser, reqh.REQSts, reqh.Remark, reqh.CancelRemark, reqh.FlagPrint, reqh.UpDateUser, reqh.RVNO " +
                ", reqh.strsts, reqh.DeptReqFlag " +
                "From REQHEAD_DEPT reqh " +
                "Where year(reqh.REQDate) = '" + reqid + "' " +
                "Order By reqh.REQDate desc, reqh.REQNo";
            dt = conn.selectData(conn.connBACK,sql);

            return dt;
        }
    }
}
