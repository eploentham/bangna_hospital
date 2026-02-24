using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            sql = "Select pt16.MNC_REQ_YR,pt16.MNC_REQ_NO,pt16.MNC_REQ_DAT,pt16.MNC_SR_CD,pt16.MNC_SR_PRI,pt16.MNC_SR_QTY,pt16.MNC_FN_CD,pt16.MNC_STAMP_DAT,pm30.MNC_SR_DSC,pt16.MNC_REQ_STS " +
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
                ", pt16.MNC_REQ_NO as req_no, 'procedure' as flag, '1' as qty " +
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
        public String insertStoreProcedure(String hn, String vsdate, String preno, String useradd, String ordercode, String qty)
        {
            String sql = "", re = "";
            long hn1 = 0;
            try
            {
                conn.comStore = new SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "gen_procedure_opd_by_hn";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("hn_where", hn);
                conn.comStore.Parameters.AddWithValue("vsdate", vsdate);
                conn.comStore.Parameters.AddWithValue("preno", preno);
                conn.comStore.Parameters.AddWithValue("mnc_usr_add", useradd);
                conn.comStore.Parameters.AddWithValue("order_code", ordercode);
                conn.comStore.Parameters.AddWithValue("qty", qty);

                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;
                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;
                if (int.TryParse(re, out int chk))
                {
                    //vsdate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("MM") + "-" + DateTime.Now.Day.ToString("dd");
                    //sql = "SELECT MNC_VN_NO, MNC_VN_SEQ FROM PATIENT_T01 " +
                    //    "WHERE (MNC_HN_NO ='"+ hn + "') AND (MNC_DATE =@'"+ vsdate + "') AND (MNC_ACT_NO <= 600) ORDER BY MNC_VN_NO, MNC_VN_SEQ;";
                }
            }
            catch (Exception ex)
            {
                //conn.connLinkLIS.Close();
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert insertStoreProcedure  ex " + sql);
                re = sql;
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
    }
}
