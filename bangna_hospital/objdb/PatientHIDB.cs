using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PatientHIDB
    {
        public PatientHI ptthi;
        ConnectDB conn;

        public PatientHIDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            ptthi = new PatientHI();
            ptthi.hi_id = "hi_id";
            ptthi.hn = "hn";
            ptthi.visit_date = "visit_date";
            ptthi.pre_no = "pre_no";
            ptthi.date_order_drug = "date_order_drug";
            ptthi.status_drug = "status_drug";
            ptthi.status_xray = "status_xray";
            ptthi.status_lab = "status_lab";
            ptthi.status_authen = "status_authen";
            ptthi.date_order_xray = "date_order_xray";
            ptthi.date_order_lab = "date_order_lab";
            ptthi.drug_set = "drug_set";
            ptthi.status_ordered_drug = "status_ordered_drug";
            ptthi.status_ordered_xray = "status_ordered_xray";
            ptthi.status_ordered_lab = "status_ordered_lab";
            ptthi.active = "active";
            ptthi.date_create = "date_create";
            ptthi.status_pic_kyc = "status_pic_kyc";
            ptthi.status_order = "status_order";
            ptthi.doc_scan_id_authen = "doc_scan_id_authen";
            ptthi.doc_scan_id_kyc = "doc_scan_id_kyc";
            ptthi.queue_seq = "queue_seq";
            ptthi.req_no_drug = "req_no_drug";
            ptthi.req_no_xray = "req_no_xray";
            ptthi.vn = "vn";
            ptthi.paid_code = "paid_code";

            ptthi.table = "t_patient_hi";
            ptthi.pkField = "hi_id";
        }
        private void chkNull(PatientHI p)
        {
            long chk = 0;
            int chk1 = 0;
            decimal chk2 = 0;

            p.hn = p.hn == null ? "" : p.hn;
            p.visit_date = p.visit_date == null ? "" : p.visit_date;
            p.pre_no = p.pre_no == null ? "" : p.pre_no;
            p.date_order_drug = p.date_order_drug == null ? "" : p.date_order_drug;
            p.status_drug = p.status_drug == null ? "" : p.status_drug;
            p.status_xray = p.status_xray == null ? "" : p.status_xray;
            p.status_lab = p.status_lab == null ? "" : p.status_lab;
            p.status_authen = p.status_authen == null ? "" : p.status_authen;
            p.date_order_xray = p.date_order_xray == null ? "" : p.date_order_xray;
            p.date_order_lab = p.date_order_lab == null ? "" : p.date_order_lab;
            p.status_pic_kyc = p.status_pic_kyc == null ? "" : p.status_pic_kyc;
            p.status_order = p.status_order == null ? "" : p.status_order;
            p.req_no_drug = p.req_no_drug == null ? "" : p.req_no_drug;
            p.req_no_xray = p.req_no_xray == null ? "" : p.req_no_xray;

            p.vn = p.vn == null ? "" : p.vn;
            p.drug_set = p.drug_set == null ? "" : p.drug_set;
            p.paid_code = p.paid_code == null ? "" : p.paid_code;

            p.status_ordered_drug = p.status_ordered_drug == null ? "" : p.status_ordered_drug;
            p.status_ordered_xray = p.status_ordered_xray == null ? "" : p.status_ordered_xray;
            p.status_ordered_lab = p.status_ordered_lab == null ? "" : p.status_ordered_lab;
            p.active = p.active == null ? "" : p.active;
            p.date_create = p.date_create == null ? "" : p.date_create;

            p.doc_scan_id_authen = long.TryParse(p.doc_scan_id_authen, out chk) ? chk.ToString() : "0";
            p.doc_scan_id_kyc = long.TryParse(p.doc_scan_id_kyc, out chk) ? chk.ToString() : "0";

            //p.MNC_SUM_PRI = decimal.TryParse(p.MNC_SUM_PRI, out chk2) ? chk2.ToString() : "0";
        }
        public DataTable SelectByVisitDate( String datereq)
        {
            DataTable dt = new DataTable();
            String year = "";
            int year2 = 0;
            year = datereq.Substring(0, 4);
            int.TryParse(year, out year2);
            year = (year2 + 543).ToString();
            String sql = "Select ptthi.*,pm02.MNC_PFIX_DSC as prefix,pm01.MNC_FNAME_T,pm01.MNC_LNAME_T " +
                "From "+ ptthi .table+ " ptthi " +
                " inner join bng5_dbms_front.dbo.patient_m01 pm01 on ptthi.hn = pm01.MNC_HN_NO " +
                " inner join bng5_dbms_front.dbo.patient_m02 pm02 on pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Where ptthi.visit_date = '" + datereq + "'";
            dt = conn.selectData(conn.conn, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectByDateOrder(String datereq)
        {
            DataTable dt = new DataTable();
            String year = "";
            int year2 = 0;
            year = datereq.Substring(0, 4);
            int.TryParse(year, out year2);
            year = (year2 + 543).ToString();
            String sql = "Select ptthi.*,pm02.MNC_PFIX_DSC as prefix,pm01.MNC_FNAME_T,pm01.MNC_LNAME_T " +
                "From " + ptthi.table + " ptthi " +
                " inner join bng5_dbms_front.dbo.patient_m01 pm01 on ptthi.hn = pm01.MNC_HN_NO " +
                " inner join bng5_dbms_front.dbo.patient_m02 pm02 on pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Where ptthi.date_order_drug = '" + datereq + "' and  ptthi.status_order = '1' and ptthi.active = '1' " +
                "Order By ptthi.hi_id ";
            dt = conn.selectData(conn.conn, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public String SelectMax(String hn)
        {
            DataTable dt = new DataTable();
            String year = "0";
            
            String sql = "Select max(ptthi.hi_id) as cnt " +
                "From " + ptthi.table + " ptthi " +
                "Where ptthi.hn = '" + hn + "'";
            dt = conn.selectData(conn.conn, sql);
            if (dt.Rows.Count > 0)
            {
                year = dt.Rows[0]["cnt"].ToString();
            }
            return year;
        }
        public String insert(PatientHI p, String userId)
        {
            String re = "";
            String sql = "";
            p.active = "1";
            //p.ssdata_id = "";
            long chk = 0;
            chkNull(p);
            sql = "Insert Into " + ptthi.table + " (" + ptthi.hn + "," + ptthi.visit_date + "," + ptthi.pre_no + "," +
                ptthi.date_order_drug + "," + ptthi.status_drug + "," + ptthi.status_xray + "," +
                ptthi.status_lab + "," + ptthi.status_authen + "," + ptthi.date_order_xray + "," +
                ptthi.drug_set + "," + ptthi.status_ordered_drug + "," + ptthi.status_ordered_xray + "," + ptthi.status_ordered_lab + "," +
                ptthi.date_order_lab + "," + ptthi.active + "," + ptthi.date_create + "," +
                ptthi.status_pic_kyc + "," + ptthi.status_order + "," + ptthi.doc_scan_id_kyc + ", " + 
                ptthi.doc_scan_id_authen + "," + ptthi.queue_seq + "," + ptthi.req_no_drug + "," + 
                ptthi.req_no_xray + "," + ptthi.vn + "," + ptthi.paid_code + " " +
               ") " +
                "Values ('" + p.hn + "','" + p.visit_date + "','" + p.pre_no + "'," +
                "'" + p.date_order_drug + "','" + p.status_drug + "','" + p.status_xray + "'," +
                "'" + p.status_lab + "','" + p.status_authen + "','" + p.date_order_xray + "'," +
                "'"+ p.drug_set + "','0','0','0'," +
                "'" + p.date_order_lab + "','1', convert(varchar, getdate(), 20)," +
                "'0','" + p.status_order + "','0'," +
                "'0','" + p.queue_seq + "','" + p.req_no_drug + "'," +
                "'" + p.req_no_xray + "','" + p.vn + "','" + p.paid_code + "')";
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
                if(long.TryParse(re, out chk))
                {
                    re = SelectMax(p.hn);
                }
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "PatientHIDB insert error  " + ex.Message + " " + ex.InnerException+" sql "+ sql);
                new LogWriter("e", "PatientHIDB insert error  sql " + sql);
            }

            return re;
        }
        public String update(PatientHI p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            chkNull(p);
            sql = "Update " + ptthi.table + " Set " +
                " " + ptthi.visit_date + " = " + p.visit_date + " " +
                "Where " + ptthi.pkField + "='" + p.hi_id + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "PatientHIDB update error  " + ex.Message + " " + ex.InnerException);
            }

            return re;
        }
        public String updateStatusDrugOrder(String ptthiid, String statusdrug, String datedrug, String reqnodrug, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            //chkNull(p);
            sql = "Update " + ptthi.table + " Set " +
                " " + ptthi.status_drug + " = '1' " +
                ", " + ptthi.date_order_drug + " = '" + datedrug.Trim() + "' " +
                ", " + ptthi.drug_set + " ='" + statusdrug.Trim() + "' " +
                ", " + ptthi.req_no_drug + " ='" + reqnodrug + "' " +
                "Where " + ptthi.pkField + "='" + ptthiid + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "PatientHIDB update error  " + ex.Message + " " + ex.InnerException);
            }
            return re;
        }
        public String updateStatusXrayOrder(String ptthiid,String datexray, String reqnoxray, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            //chkNull(p);
            sql = "Update " + ptthi.table + " Set " +
                " " + ptthi.status_xray + " = '1' " +
                ", " + ptthi.date_order_xray + " = '" + datexray + "' " +
                ", " + ptthi.req_no_xray + " = '" + reqnoxray + "' " +
                "Where " + ptthi.pkField + "='" + ptthiid + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "PatientHIDB update error  " + ex.Message + " " + ex.InnerException);
            }

            return re;
        }
        public String updateStatusPicAuthen(String ptthiid, String docscanid, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            //chkNull(p);
            sql = "Update " + ptthi.table + " Set " +
                " " + ptthi.status_authen + " = '1' " +
                ", " + ptthi.doc_scan_id_authen + " = '" + docscanid + "' " +
                "Where " + ptthi.pkField + "='" + ptthiid + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "PatientHIDB updateStatusPicAuthen error  " + ex.Message + " " + ex.InnerException);
            }
            return re;
        }
        public String updateStatusPicKYC(String ptthiid, String docscanid, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            //chkNull(p);
            sql = "Update " + ptthi.table + " Set " +
                " " + ptthi.status_pic_kyc + " = '1' " +
                ", " + ptthi.doc_scan_id_kyc + " = '"+ docscanid + "' " +
                "Where " + ptthi.pkField + "='" + ptthiid + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "PatientHIDB updateStatusPicKYC error  " + ex.Message + " " + ex.InnerException);
            }
            return re;
        }
        public String insertPatientHI(PatientHI p, String userId)
        {
            String re = "";

            if (p.hi_id.Equals(""))
            {
                re = insert(p, "");
            }
            else
            {
                re = update(p, "");
            }

            return re;
        }
        public String reserveHI(String hi_id, String userI)
        {
            String re = "";
            String sql = "";
            try
            {
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "covid_gen_patient_t01_by_hn_hi_reserve";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("hi_id", hi_id);
                
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
                new LogWriter("e", "reserveHI  " );
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public PatientHI setLabM02(DataTable dt)
        {
            PatientHI labM02 = new PatientHI();
            if (dt.Rows.Count > 0)
            {
                labM02.hi_id = dt.Rows[0]["hi_id"].ToString();
                labM02.hn = dt.Rows[0]["hn"].ToString();
                labM02.visit_date = dt.Rows[0]["visit_date"].ToString();
                labM02.pre_no = dt.Rows[0]["pre_no"].ToString();
                labM02.date_order_drug = dt.Rows[0]["date_order_drug"].ToString();
                labM02.status_drug = dt.Rows[0]["status_drug"].ToString();
                labM02.status_xray = dt.Rows[0]["status_xray"].ToString();
                labM02.status_lab = dt.Rows[0]["status_lab"].ToString();
                labM02.status_authen = dt.Rows[0]["status_authen"].ToString();
                labM02.date_order_xray = dt.Rows[0]["date_order_xray"].ToString();
                labM02.date_order_lab = dt.Rows[0]["date_order_lab"].ToString();
                labM02.status_order = dt.Rows[0]["status_order"].ToString();
                labM02.req_no_drug = dt.Rows[0]["req_no_drug"].ToString();
                labM02.req_no_xray = dt.Rows[0]["req_no_xray"].ToString();
                labM02.vn = dt.Rows[0]["vn"].ToString();
                labM02.paid_code = dt.Rows[0]["paid_code"].ToString();
            }
            else
            {
                setLabM02(labM02);
            }
            return labM02;
        }
        public PatientHI setLabM02(PatientHI p)
        {
            p.hi_id = "";
            p.hn = "";
            p.visit_date = "";
            p.pre_no = "";
            p.date_order_drug = "";
            p.status_drug = "";
            p.status_xray = "";
            p.status_lab = "";
            p.status_authen = "";
            p.date_order_xray = "";
            p.date_order_lab = "";
            p.status_order = "";
            p.req_no_xray = "";
            p.req_no_drug = "";
            p.vn = "";
            p.paid_code = "";
            return p;
        }
    }
}
