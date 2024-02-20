using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PatientT16DB
    {
        public PatientT16 pt16;
        ConnectDB conn;
        public PatientT16DB(ConnectDB c) 
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pt16 = new PatientT16();
            pt16.MNC_REQ_YR = "MNC_REQ_YR";
            pt16.MNC_REQ_NO = "MNC_REQ_NO";
            pt16.MNC_REQ_DAT = "MNC_REQ_DAT";
            pt16.MNC_SR_CD = "MNC_SR_CD";
            pt16.MNC_REQ_STS = "MNC_REQ_STS";
            pt16.MNC_SR_PRI = "MNC_SR_PRI";
            pt16.MNC_SR_QTY = "MNC_SR_QTY";
            pt16.MNC_SR_COS = "MNC_SR_COS";
            pt16.MNC_STS = "MNC_STS";
            pt16.MNC_FN_CD = "MNC_FN_CD";
            pt16.MNC_USR_UPD = "MNC_USR_UPD";

            pt16.table = "Patient_T16";
        }
        public DataTable SelectProcedureByVisit(String hn, String preno, String vsdate)
        {
            String re = "", sql = "";
            DataTable dt = new DataTable();
            sql = "Select pt16.MNC_REQ_YR,pt16.MNC_REQ_NO,pt16.MNC_REQ_DAT,pt16.MNC_SR_CD,pt16.MNC_SR_PRI,pt16.MNC_SR_QTY,pt16.MNC_FN_CD,pt16.MNC_STAMP_DAT,pm30.MNC_SR_DSC " +
                "From Patient_t15 pt15 " +
                "Left Join "+ pt16.table+ " pt16 on pt16.MNC_REQ_YR = pt15.MNC_REQ_YR and pt16.MNC_REQ_NO = pt15.MNC_REQ_NO and pt16.MNC_REQ_DAT = pt15.MNC_REQ_DAT " +
                "Left Join PATIENT_M30 pm30 on pt16.MNC_SR_CD = pm30.MNC_SR_CD " +
                "Where pt15.MNC_HN_NO = '"+hn+ "' and pt15.MNC_PRE_NO = '" + preno + "' and pt15.MNC_DATE = '" + vsdate + "' and pt15.MNC_REQ_STS <> 'C' and pt16.MNC_REQ_STS <> 'C' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public DataTable selectbyHNReqNo(String hn, String reqdate, String reqno)
        {
            DataTable dt = new DataTable();
            String sql = "", lccode = "", wherelccode = "";
            sql = "SELECT  pt16.MNC_SR_CD as order_code, pm30.MNC_SR_DSC as order_name, convert(varchar(20),pt16.MNC_REQ_DAT, 23) as req_date " +
                ", pt16.MNC_REQ_NO as req_no, 'xray' as flag, '1' as qty " +
                "FROM    Patient_t15 pt15  " +
                "left join Patient_T16 pt16 ON pt15.MNC_REQ_NO = pt16.MNC_REQ_NO AND pt15.MNC_REQ_DAT = pt16.MNC_REQ_DAT " +
                "left join PATIENT_M30 pm30 ON pt16.MNC_SR_CD = pm30.MNC_SR_CD " +
                "where pt15.MNC_REQ_DAT = '" + reqdate + "' and pt15.MNC_REQ_NO = '" + reqno + "'  " +
                "and pt15.mnc_hn_no = '" + hn + "' " +
                "and pt15.mnc_req_sts <> 'C'  and pt15.mnc_req_sts <> 'C'  " +
                "Order By pt16.MNC_SR_CD ";

            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
    }
}
