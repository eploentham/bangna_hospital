using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class DocScanDB
    {
        public DocScan dsc;
        ConnectDB conn;
        public List<DocScan> lDgs;

        public DocScanDB(ConnectDB c)
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            dsc = new DocScan();
            lDgs = new List<DocScan>();

            dsc.doc_scan_id = "doc_scan_id";
            dsc.doc_group_id = "doc_group_id";
            dsc.row_no = "row_no";
            dsc.host_ftp = "host_ftp";
            dsc.image_path = "image_path";
            dsc.hn = "hn";
            dsc.vn = "vn";
            dsc.visit_date = "visit_date";
            dsc.active = "active";
            dsc.remark = "remark";
            dsc.date_create = "date_create";
            dsc.date_modi = "date_modi";
            dsc.date_cancel = "date_cancel";
            dsc.user_create = "user_create";
            dsc.user_modi = "user_modi";
            dsc.user_cancel = "user_cancel";
            dsc.an = "an";
            dsc.doc_group_sub_id = "doc_group_sub_id";
            dsc.pre_no = "pre_no";
            dsc.an_date = "an_date";
            dsc.status_ipd = "status_ipd";
            dsc.an_cnt = "an_cnt";
            dsc.folder_ftp = "folder_ftp";
            dsc.row_cnt = "row_cnt";
            dsc.status_ml = "status_ml";
            dsc.ml_date_time_start = "ml_date_time_start";
            dsc.ml_date_time_end = "ml_date_time_end";
            dsc.ml_fm = "ml_fm";
            dsc.status_version = "status_version";
            dsc.pic_before_scan_cnt = "pic_before_scan_cnt";
            dsc.date_req = "date_req";
            dsc.req_id = "req_id";

            dsc.table = "doc_scan";
            dsc.pkField = "doc_scan_id";
        }
        public void getlBsp()
        {
            //lDept = new List<Position>();

            lDgs.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            foreach (DataRow row in dt.Rows)
            {
                DocScan itm1 = new DocScan();
                itm1.doc_scan_id = row[dsc.doc_scan_id].ToString();
                itm1.doc_group_id = row[dsc.doc_group_id].ToString();
                itm1.row_no = row[dsc.row_no].ToString();
                itm1.host_ftp = row[dsc.host_ftp].ToString();
                itm1.image_path = row[dsc.image_path].ToString();
                itm1.hn = row[dsc.hn].ToString();
                itm1.vn = row[dsc.vn].ToString();
                itm1.visit_date = row[dsc.visit_date].ToString();
                itm1.active = row[dsc.active].ToString();
                itm1.remark = row[dsc.remark].ToString();
                itm1.date_create = row[dsc.date_create].ToString();
                itm1.date_modi = row[dsc.date_modi].ToString();
                itm1.date_cancel = row[dsc.date_cancel].ToString();
                itm1.user_create = row[dsc.user_create].ToString();
                itm1.user_modi = row[dsc.user_modi].ToString();
                itm1.user_cancel = row[dsc.user_cancel].ToString();
                itm1.an = row[dsc.an].ToString();
                itm1.doc_group_sub_id = row[dsc.doc_group_sub_id].ToString();
                itm1.pre_no = row[dsc.pre_no].ToString();
                itm1.an_date = row[dsc.an_date].ToString();
                itm1.status_ipd = row[dsc.status_ipd].ToString();
                itm1.an_cnt = row[dsc.an_cnt].ToString();
                itm1.status_version = row[dsc.status_version].ToString();
                itm1.pic_before_scan_cnt = row[dsc.pic_before_scan_cnt].ToString();
                itm1.date_req = row[dsc.date_req].ToString();
                itm1.req_id = row[dsc.req_id].ToString();
                lDgs.Add(itm1);
            }
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                " Where dsc." + dsc.active + " ='1' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectByHn(String id)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                //"Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dsc." + dsc.hn + " ='" + id + "' and dsc."+dsc.active+ "='1'  " +
                "Order By dsc.doc_scan_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectByHnDeptUS(String id)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                "Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dsc." + dsc.hn + " ='" + id + "' and dsc." + dsc.active + "='1' and dgss.dept_us = '1' " +
                "Order By dsc.doc_scan_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectLabOutByDateReceive(String datestart, String dateend)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                "Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dsc." + dsc.date_create + " >='" + datestart + "' and dsc." + dsc.date_create + " <='" + dateend + "' " +
                "and dsc." + dsc.active + "='1'  /*and dgss.dept_us = '1'*/ and dsc." + dsc.ml_fm+"='FM-LAB-999' " +
                "Order By dsc.doc_scan_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectByHnDeptUs(String id)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + id + "' and dsc." + dsc.active + "='1' and " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DocScan selectByPk(String id)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.pkField + " ='" + id + "' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);
            cop1 = setDocScan(dt);
            return cop1;
        }
        public DataTable selectByPk1(String id)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.pkField + " ='" + id + "' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectByVn(String hn, String vn, String vsDate)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' and dsc."+dsc.vn+"='"+vn+"' and dsc."+dsc.visit_date + "='"+vsDate+"' and dsc."+dsc.active+"='1' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectByAn(String hn, String an)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.an + "='" + an + "' and dsc." + dsc.active + "='1' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public String selectRowNoByHn(String hn, String docgid)
        {
            String re = "0", re1="";
            int chk = 0;
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select max("+dsc.row_no+") as "+ dsc.row_no+" " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' and dsc."+dsc.doc_group_id+"='"+docgid+"' " +
                "  ";
            dt = conn.selectData(conn.conn, sql);
            if (dt.Rows.Count > 0)
            {
                re1 = dt.Rows[0][dsc.row_no].ToString();
                int.TryParse(re1, out chk);
                chk++;
                re = chk.ToString();
            }
            return re;
        }
        public String selectRowNoByHnVn(String hn, String vn, String docgid)
        {
            String re = "0", re1 = "";
            int chk = 0;
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select max(" + dsc.row_no + ") as " + dsc.row_no + " " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.doc_group_id + "='" + docgid + "' and dsc."+dsc.vn+"='"+vn+"' " +
                "  ";
            dt = conn.selectData(conn.conn, sql);
            if (dt.Rows.Count > 0)
            {
                re1 = dt.Rows[0][dsc.row_no].ToString();
                int.TryParse(re1, out chk);
                chk++;
                re = chk.ToString();
            }
            return re;
        }
        private void chkNull(DocScan p)
        {
            long chk = 0;

            p.date_modi = p.date_modi == null ? "" : p.date_modi;
            p.date_cancel = p.date_cancel == null ? "" : p.date_cancel;
            p.user_create = p.user_create == null ? "" : p.user_create;
            p.user_modi = p.user_modi == null ? "" : p.user_modi;
            p.user_cancel = p.user_cancel == null ? "" : p.user_cancel;

            p.host_ftp = p.host_ftp == null ? "" : p.host_ftp;
            p.image_path = p.image_path == null ? "" : p.image_path;
            p.hn = p.hn == null ? "" : p.hn;
            p.vn = p.vn == null ? "" : p.vn;
            p.visit_date = p.visit_date == null ? "" : p.visit_date;
            p.remark = p.remark == null ? "" : p.remark;
            p.an = p.an == null ? "" : p.an;
            p.pre_no = p.pre_no == null ? "" : p.pre_no;
            p.an_date = p.an_date == null ? "" : p.an_date;
            p.status_ipd = p.status_ipd == null ? "" : p.status_ipd;
            p.an_cnt = p.an_cnt == null ? "" : p.an_cnt;
            p.folder_ftp = p.folder_ftp == null ? "" : p.folder_ftp;

            p.status_ml = p.status_ml == null ? "" : p.status_ml;
            p.row_cnt = p.row_cnt == null ? "" : p.row_cnt;
            p.ml_date_time_start = p.ml_date_time_start == null ? "" : p.ml_date_time_start;
            p.ml_date_time_end = p.ml_date_time_end == null ? "" : p.ml_date_time_end;
            p.ml_fm = p.ml_fm == null ? "" : p.ml_fm;
            p.status_version = p.status_version == null ? "" : p.status_version;
            p.date_req = p.date_req == null ? "" : p.date_req;
            p.req_id = p.req_id == null ? "" : p.req_id;
            //p.ml_fm = p.req_id == null ? "" : p.req_id;

            p.doc_group_id = long.TryParse(p.doc_group_id, out chk) ? chk.ToString() : "0";
            p.row_no = long.TryParse(p.row_no, out chk) ? chk.ToString() : "0";
            p.doc_group_sub_id = long.TryParse(p.doc_group_sub_id, out chk) ? chk.ToString() : "0";
            p.pic_before_scan_cnt = long.TryParse(p.pic_before_scan_cnt, out chk) ? chk.ToString() : "0";
            //p.doctor_id = int.TryParse(p.doctor_id, out chk) ? chk.ToString() : "0";
        }
        public String insertScreenCapture(DocScan p, String userId)
        {
            String re = "";
            String sql = "";
            DataTable dt = new DataTable();
            p.active = "1";
            //p.ssdata_id = "";
            int chk = 0;
            chkNull(p);

            try
            {
                //new LogWriter("d", "insertScreenCapture p.an " + p.an+ " p.vn "+ p.vn);
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.conn;
                conn.comStore.CommandText = "insert_doc_scan_v4";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("doc_group_id", p.doc_group_id);
                conn.comStore.Parameters.AddWithValue("host_ftp", p.host_ftp);
                conn.comStore.Parameters.AddWithValue("hn", p.hn);
                conn.comStore.Parameters.AddWithValue("vn", p.vn);
                conn.comStore.Parameters.AddWithValue("remark", p.remark);
                conn.comStore.Parameters.AddWithValue("user_create", userId);
                conn.comStore.Parameters.AddWithValue("an", p.an);
                conn.comStore.Parameters.AddWithValue("doc_group_sub_id", p.doc_group_sub_id);
                conn.comStore.Parameters.AddWithValue("pre_no", p.pre_no);
                conn.comStore.Parameters.AddWithValue("an_date", p.an_date);
                conn.comStore.Parameters.AddWithValue("status_ipd", p.status_ipd);
                conn.comStore.Parameters.AddWithValue("ext", p.image_path);
                conn.comStore.Parameters.AddWithValue("visit_date", p.visit_date);
                conn.comStore.Parameters.AddWithValue("folder_ftp", p.folder_ftp);
                conn.comStore.Parameters.AddWithValue("row_no2", p.row_no);
                conn.comStore.Parameters.AddWithValue("row_cnt", p.row_cnt);
                conn.comStore.Parameters.AddWithValue("status_version", p.status_version);
                conn.comStore.Parameters.AddWithValue("pic_before_scan", p.pic_before_scan_cnt);
                conn.comStore.Parameters.AddWithValue("ml_fm", p.ml_fm);
                conn.comStore.Parameters.AddWithValue("status_ml", p.status_ml);
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;

                conn.conn.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;
                //string retunvalue = (string)sqlcomm.Parameters["@b"].Value;
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            finally
            {
                conn.conn.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertLabOut(DocScan p, String userId)
        {
            String re = "";
            String sql = "";
            DataTable dt = new DataTable();
            p.active = "1";
            //p.ssdata_id = "";
            int chk = 0;
            chkNull(p);
            
            try
            {
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.conn;
                conn.comStore.CommandText = "insert_doc_scan_v3";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("doc_group_id", p.doc_group_id);
                conn.comStore.Parameters.AddWithValue("host_ftp", p.host_ftp);
                conn.comStore.Parameters.AddWithValue("hn", p.hn);
                conn.comStore.Parameters.AddWithValue("vn", p.vn);
                conn.comStore.Parameters.AddWithValue("remark", p.remark);
                conn.comStore.Parameters.AddWithValue("user_create", userId);
                conn.comStore.Parameters.AddWithValue("an", p.an);
                conn.comStore.Parameters.AddWithValue("doc_group_sub_id", p.doc_group_sub_id);
                conn.comStore.Parameters.AddWithValue("pre_no", p.pre_no);
                conn.comStore.Parameters.AddWithValue("an_date", p.an_date);
                conn.comStore.Parameters.AddWithValue("status_ipd", p.status_ipd);
                conn.comStore.Parameters.AddWithValue("ext", p.image_path);
                conn.comStore.Parameters.AddWithValue("visit_date", p.visit_date);
                conn.comStore.Parameters.AddWithValue("folder_ftp", p.folder_ftp);
                conn.comStore.Parameters.AddWithValue("row_no2", p.row_no);
                conn.comStore.Parameters.AddWithValue("row_cnt", p.row_cnt);
                conn.comStore.Parameters.AddWithValue("status_version", p.status_version);
                conn.comStore.Parameters.AddWithValue("pic_before_scan", p.pic_before_scan_cnt);
                conn.comStore.Parameters.AddWithValue("date_req", p.date_req);
                conn.comStore.Parameters.AddWithValue("req_id", p.req_id);
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;

                conn.conn.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;
                //string retunvalue = (string)sqlcomm.Parameters["@b"].Value;
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            finally
            {
                conn.conn.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insert(DocScan p, String userId)
        {
            String re = "";
            String sql = "";
            DataTable dt = new DataTable();
            p.active = "1";
            //p.ssdata_id = "";
            int chk = 0;
            chkNull(p);
            sql = "Insert Into " + dsc.table + " (" + dsc.doc_group_id + "," + dsc.active + "," + dsc.row_no + "," +
                dsc.host_ftp + "," + dsc.image_path + "," + dsc.hn + "," +
                dsc.vn + "," + dsc.visit_date + "," + dsc.remark + "," +
                dsc.date_create + "," + dsc.date_modi + "," + dsc.date_cancel + "," +
                dsc.user_create + "," + dsc.user_modi + "," + dsc.user_cancel + "," +
                dsc.an + "," + dsc.doc_group_sub_id + "," + dsc.pre_no + "," +
                dsc.an_date + "," + dsc.status_ipd + "," + dsc.an_cnt + "," +
                dsc.folder_ftp + "," + dsc.status_ml + "," + dsc.row_cnt + "," +
                dsc.ml_date_time_start + "," + dsc.ml_date_time_end + "," + dsc.ml_fm + "," + dsc.pic_before_scan_cnt + " " +
                ") " +
                "Values ('" + p.doc_group_id + "','1','" + p.row_no + "',"+
                "'"+ p.host_ftp + "','" + p.image_path + "','" + p.hn + "'," +
                "'" + p.vn + "','" + p.visit_date + "','" + p.remark + "'," +
                "convert(varchar, getdate(), 23),'" + p.date_modi + "','" + p.date_cancel + "'," +
                "'" + userId + "','" + p.user_modi + "','" + p.user_cancel + "'," +
                "'" + p.an + "','" + p.doc_group_sub_id + "','" + p.pre_no + "'," +
                "'" + p.an_date + "','" + p.status_ipd + "','" + p.an_cnt + "', " +
                "'" + p.folder_ftp + "','" + p.status_ml + "','" + p.row_cnt + "', " +
                "'" + p.ml_date_time_start + "','" + p.ml_date_time_end + "','" + p.ml_fm + "','" + p.pic_before_scan_cnt + "' " +
                ")";
            try
            {
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.conn;
                conn.comStore.CommandText = "insert_doc_scan_v3";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("doc_group_id", p.doc_group_id);
                conn.comStore.Parameters.AddWithValue("host_ftp", p.host_ftp);
                conn.comStore.Parameters.AddWithValue("hn", p.hn);
                conn.comStore.Parameters.AddWithValue("vn", p.vn);
                conn.comStore.Parameters.AddWithValue("remark", p.remark);
                conn.comStore.Parameters.AddWithValue("user_create", userId);
                conn.comStore.Parameters.AddWithValue("an", p.an);
                conn.comStore.Parameters.AddWithValue("doc_group_sub_id", p.doc_group_sub_id);
                conn.comStore.Parameters.AddWithValue("pre_no", p.pre_no);
                conn.comStore.Parameters.AddWithValue("an_date", p.an_date);
                conn.comStore.Parameters.AddWithValue("status_ipd", p.status_ipd);
                conn.comStore.Parameters.AddWithValue("ext", p.image_path);
                conn.comStore.Parameters.AddWithValue("visit_date", p.visit_date);
                conn.comStore.Parameters.AddWithValue("folder_ftp", p.folder_ftp);
                conn.comStore.Parameters.AddWithValue("row_no2", p.row_no);
                conn.comStore.Parameters.AddWithValue("row_cnt", p.row_cnt);
                conn.comStore.Parameters.AddWithValue("status_version", p.status_version);
                conn.comStore.Parameters.AddWithValue("pic_before_scan", p.pic_before_scan_cnt);
                SqlParameter retval =  conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;
                
                conn.conn.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;
                //string retunvalue = (string)sqlcomm.Parameters["@b"].Value;
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            finally
            {
                conn.conn.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String update(DocScan p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            chkNull(p);
            sql = "Update " + dsc.table + " Set " +
                " " + dsc.doc_group_id + " = '" + p.doc_group_id + "'" +
                "," + dsc.row_no + " = '" + p.row_no + "'" +
                "," + dsc.host_ftp + " = '" + p.host_ftp + "'" +
                "," + dsc.image_path + " = '" + p.image_path + "'" +
                "," + dsc.hn + " = '" + p.hn + "'" +
                "," + dsc.vn + " = '" + p.vn + "'" +
                "," + dsc.visit_date + " = '" + p.visit_date + "'" +
                "," + dsc.remark + " = '" + p.remark + "'" +
                "," + dsc.date_modi + " = convert(varchar, getdate(), 23)" +
                "," + dsc.user_modi + " = '" + userId + "'" +
                "," + dsc.an + " = '" + p.an + "'" +
                "," + dsc.doc_group_sub_id + " = '" + p.doc_group_sub_id + "'" +
                "," + dsc.pre_no + " = '" + p.pre_no + "'" +
                "," + dsc.an_date + " = '" + p.an_date + "'" +
                "," + dsc.status_ipd + " = '" + p.status_ipd + "'" +
                "," + dsc.an_cnt + " = '" + p.an_cnt + "'" +
                "," + dsc.folder_ftp + " = '" + p.folder_ftp + "'" +
                "Where " + dsc.pkField + "='" + p.doc_scan_id + "'"
                ;

            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }

            return re;
        }
        public String insertDocScan(DocScan p, String userId)
        {
            String re = "";

            if (p.doc_scan_id.Equals(""))
            {
                re = insert(p, "");
            }
            else
            {
                re = update(p, "");
            }

            return re;
        }
        public String updateImagepath(String image_path, String id)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            //chkNull(p);
            sql = "Update " + dsc.table + " Set " +
                " " + dsc.image_path + " = '" + image_path + "'" +
                "Where " + dsc.pkField + "='" + id + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }

            return re;
        }
        public String voidDocScan(String id, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            
            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = getdate()" +
                "," + dsc.user_cancel + " = '" + userId + "'" +
                "Where " + dsc.pkField + "='" + id + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }

            return re;
        }
        public String voidDocScanVN(String id, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = getdate()" +
                "," + dsc.user_cancel + " = '" + userId + "'" +
                "Where " + dsc.vn + "='" + id + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }

            return re;
        }
        public String voidDocScanAN(String id, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = getdate()" +
                "," + dsc.user_cancel + " = '" + userId + "'" +
                "Where " + dsc.an + "='" + id + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }

            return re;
        }
        public DocScan setDocScan(DataTable dt)
        {
            DocScan dgs1 = new DocScan();
            if (dt.Rows.Count > 0)
            {
                dgs1.doc_scan_id = dt.Rows[0][dsc.doc_scan_id].ToString();
                dgs1.doc_group_id = dt.Rows[0][dsc.doc_group_id].ToString();
                dgs1.row_no = dt.Rows[0][dsc.row_no].ToString();
                dgs1.host_ftp = dt.Rows[0][dsc.host_ftp].ToString();
                dgs1.image_path = dt.Rows[0][dsc.image_path].ToString();
                dgs1.hn = dt.Rows[0][dsc.hn].ToString();
                dgs1.vn = dt.Rows[0][dsc.vn].ToString();
                dgs1.visit_date = dt.Rows[0][dsc.visit_date].ToString();
                dgs1.active = dt.Rows[0][dsc.active].ToString();
                dgs1.remark = dt.Rows[0][dsc.remark].ToString();
                dgs1.date_create = dt.Rows[0][dsc.date_create].ToString();
                dgs1.date_modi = dt.Rows[0][dsc.date_modi].ToString();
                dgs1.date_cancel = dt.Rows[0][dsc.date_cancel].ToString();
                dgs1.user_create = dt.Rows[0][dsc.user_create].ToString();
                dgs1.user_modi = dt.Rows[0][dsc.user_modi].ToString();
                dgs1.user_cancel = dt.Rows[0][dsc.user_cancel].ToString();
                dgs1.an = dt.Rows[0][dsc.an].ToString();
                dgs1.doc_group_sub_id = dt.Rows[0][dsc.doc_group_sub_id].ToString();
                dgs1.pre_no = dt.Rows[0][dsc.pre_no].ToString();
                dgs1.an_date = dt.Rows[0][dsc.an_date].ToString();
                dgs1.status_ipd = dt.Rows[0][dsc.status_ipd].ToString();
                dgs1.an_cnt = dt.Rows[0][dsc.an_cnt].ToString();
                dgs1.folder_ftp = dt.Rows[0][dsc.folder_ftp].ToString();
                dgs1.status_version = dt.Rows[0][dsc.status_version].ToString();
                dgs1.pic_before_scan_cnt = dt.Rows[0][dsc.pic_before_scan_cnt].ToString();
            }
            else
            {
                setDocGroupScan(dgs1);
            }
            return dgs1;
        }
        public DocScan setDocGroupScan(DocScan dgs1)
        {
            dgs1.doc_scan_id = "";
            dgs1.doc_group_id = "";
            dgs1.row_no = "";
            dgs1.host_ftp = "";
            dgs1.image_path = "";
            dgs1.hn = "";
            dgs1.vn = "";
            dgs1.visit_date = "";
            dgs1.active = "";
            dgs1.remark = "";
            dgs1.date_create = "";
            dgs1.date_modi = "";
            dgs1.date_cancel = "";
            dgs1.user_create = "";
            dgs1.user_modi = "";
            dgs1.user_cancel = "";
            dgs1.an = "";
            dgs1.doc_group_sub_id = "";
            dgs1.pre_no = "";
            dgs1.an_date = "";
            dgs1.status_ipd = "";
            dgs1.an_cnt = "";
            dgs1.folder_ftp = "";
            dgs1.status_version = "";
            dgs1.pic_before_scan_cnt = "";
            return dgs1;
        }
        //public void setCboBsp(C1ComboBox c, String selected)
        //{
        //    ComboBoxItem item = new ComboBoxItem();
        //    //DataTable dt = selectAll();
        //    int i = 0;
        //    if (lDgs.Count <= 0) getlBsp();
        //    item = new ComboBoxItem();
        //    item.Value = "";
        //    item.Text = "";
        //    c.Items.Add(item);
        //    foreach (DocScan cus1 in lDgs)
        //    {
        //        item = new ComboBoxItem();
        //        item.Value = cus1.doc_group_id;
        //        item.Text = cus1.doc_group_name;
        //        c.Items.Add(item);
        //        if (item.Value.Equals(selected))
        //        {
        //            //c.SelectedItem = item.Value;
        //            c.SelectedText = item.Text;
        //            c.SelectedIndex = i + 1;
        //        }
        //        i++;
        //    }
        //}
    }
}
