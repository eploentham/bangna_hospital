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
    public class SummaryT03DB
    {
        public ConnectDB conn;
        public SummaryT03 sumt03;
        public SummaryT03DB(ConnectDB c) 
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            sumt03 = new SummaryT03();
            sumt03.MNC_SUM_DAT = "MNC_SUM_DAT";
            sumt03.MNC_DOT_CD = "MNC_DOT_CD";
            sumt03.MNC_SUM_VN_ADD = "MNC_SUM_VN_ADD";
            sumt03.MNC_SUM_VN_ERS = "MNC_SUM_VN_ERS";
            sumt03.MNC_SUM_PAT_R_ADD = "MNC_SUM_PAT_R_ADD";
            sumt03.MNC_SUM_PAT_R_ERS = "MNC_SUM_PAT_R_ERS";
            sumt03.MNC_SUM_PAT_O_ADD = "MNC_SUM_PAT_O_ADD";
            sumt03.MNC_SUM_PAT_O_ERS = "MNC_SUM_PAT_O_ERS";
            sumt03.MNC_SUM_CLOSE_ADD = "MNC_SUM_CLOSE_ADD";
            sumt03.MNC_SUM_CLOSE_ERS = "MNC_SUM_CLOSE_ERS";
            sumt03.MNC_SUM_CLOSE_R_ADD = "MNC_SUM_CLOSE_R_ADD";
            sumt03.MNC_SUM_CLOSE_R_ERS = "MNC_SUM_CLOSE_R_ERS";
            sumt03.MNC_SUM_CLOSE_O_ADD = "MNC_SUM_CLOSE_O_ADD";
            sumt03.MNC_SUM_CLOSE_O_ERS = "MNC_SUM_CLOSE_O_ERS";
            sumt03.MNC_SUM_STS = "MNC_SUM_STS";
            sumt03.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            sumt03.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            sumt03.MNC_USR_ADD = "MNC_USR_ADD";
            sumt03.MNC_USR_UPD = "MNC_USR_UPD";
            sumt03.table = "SUMMARY_T03";
        }
        public DataTable selectQueDoctorToday()
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select sumt03.MNC_SUM_VN_ADD,sumt03.queue_current " +
                ", isnull(pm02dtr.MNC_PFIX_DSC,'') +' '+isnull(pm26.MNC_DOT_FNAME,'') +' '+isnull(pm26.MNC_DOT_LNAME,'') as dtr_name " +
                    "From SUMMARY_T03 sumt03 " +
                    "Left join	patient_m26 pm26 on sumt03.MNC_DOT_CD = pm26.MNC_DOT_CD " +
                    "Left JOIN	PATIENT_M02 as pm02dtr ON pm26.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                    "Where sumt03.MNC_SUM_DAT = convert(varchar(20), getdate(),23)  " ;
            dt = conn.selectData(sql);

            return dt;
        }
        public String updateSumVnAddold(String dtrcode)
        {
            String sql = "", re="";
            
            try
            {
                sql = "Update SUMMARY_T03 Set MNC_SUM_VN_ADD = MNC_SUM_VN_ADD - 1 Where MNC_DOT_CD = '" + dtrcode + "' and MNC_SUM_DAT = convert(varchar(20), getdate(),23) ";
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                if (int.TryParse(re, out int chk) && (chk == 0))
                {
                    
                }
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateSumVnAdd sql " + sql);
            }
            return re;
        }
        public String updateSumVnAdd(String dtrcode, String hn, String preno, String vsdate)
        {
            String re = "";
            String sql = "";
            try
            {
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "que_doctor_opd";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("dtr_code", dtrcode);
                conn.comStore.Parameters.AddWithValue("hn", hn);
                conn.comStore.Parameters.AddWithValue("pre_no", preno);
                conn.comStore.Parameters.AddWithValue("vs_date", vsdate);
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;
                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;

            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateSumVnAdd re " + re);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertSummaryT03(String dtrcode)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "", vsdate="";
            try
            {
                sql = "Update " + sumt03.table + " Set " +
                    ""+ sumt03.MNC_SUM_VN_ADD+"="+sumt03.MNC_SUM_VN_ADD+"+1 " +
                    "Where " + sumt03.MNC_SUM_DAT + " = convert(varchar(20), getdate(),23) and " + sumt03.MNC_DOT_CD + "='" + dtrcode + "' ";
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                if(int.TryParse(re, out int chk) && (chk==0))
                {
                    sql = "Insert into " + sumt03.table + "(" +
                        "" +sumt03.MNC_SUM_DAT+","+sumt03.MNC_DOT_CD+
                        ","+sumt03.MNC_SUM_VN_ADD + "," + sumt03.MNC_STAMP_DAT + ") " +
                        "Values( convert(varchar(20), getdate(),23)," + dtrcode +
                        ",1,convert(varchar(20), getdate(),23)" +
                        ")";
                    re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                }
            }
            catch(Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insertSummaryT03 sql " + sql);
            }
            return re;
        }
    }
}
