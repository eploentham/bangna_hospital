using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            dsc.patient_fullname = "patient_fullname";
            dsc.status_record = "status_record";
            dsc.sort1 = "sort1";
            dsc.comp_labout_id = "comp_labout_id";
            dsc.sort1 = "sort1";
            //dsc.row_cnt = "row_cnt";

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
                itm1.patient_fullname = row[dsc.patient_fullname].ToString();
                itm1.status_record = row[dsc.status_record].ToString();
                itm1.sort1 = row[dsc.sort1].ToString();
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
        public DataTable selectGroupByMlFM(String limit)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String limit1 = "";
            if (limit.Length > 0)
            {
                limit1 = " TOP ("+limit+")";
            }
            String sql = "select "+ limit1 + " dsc.ml_fm, count(1) as cnt " +
                "From " + dsc.table + " dsc " +
                "Where dsc." + dsc.active + "='1'  " +
                "Group By dsc.ml_fm " +
                "Order By dsc.ml_fm ";
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
        public DataTable selectOutLabByHn(String id)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                //"Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dsc." + dsc.hn + " ='" + id + "' and dsc." + dsc.active + "='1' and dsc." + dsc.status_record + "='2' " +
                "Order By dsc.doc_scan_id desc ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectStatus4ByHn(String hn)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                //"Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.active + "='1' and dsc.status_record = '4' " +
                "Order By dsc.doc_scan_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectPicByHn(string hn)
        {
            DocScan docScan = new DocScan();
            DataTable dataTable = new DataTable();
            return this.conn.selectData(this.conn.conn, "select dsc.* From " + this.dsc.table + " dsc Where dsc." + this.dsc.hn + " ='" + hn + "' and dsc." + this.dsc.active + "='1' and dsc.status_record = '4' Order By dsc.sort1,dsc.doc_scan_id ");
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
        public DataTable selectByAnSortID(string hn, string an)
        {
            DocScan docScan = new DocScan();
            DataTable dataTable = new DataTable();
            return this.conn.selectData(this.conn.conn, "select dsc.* From " + this.dsc.table + " dsc Where dsc." + this.dsc.hn + " ='" + hn + "' and dsc." + this.dsc.an + "='" + an + "' and dsc." + this.dsc.active + "='1' Order By dsc." + this.dsc.sort1);
        }
        public DataTable selectBySortID(string hn, string an, string sort1)
        {
            DocScan docScan = new DocScan();
            DataTable dataTable = new DataTable();
            return this.conn.selectData(this.conn.conn, "select dsc.* From " + this.dsc.table + " dsc Where dsc." + this.dsc.hn + " ='" + hn + "' and dsc." + this.dsc.an + "='" + an + "' and dsc." + this.dsc.active + "='1' and dsc." + this.dsc.sort1 + " in (" + sort1 + ") Order By dsc." + this.dsc.sort1);
        }
        public DataTable selectDistByDateCrate(String datestart, String dateend)
        {
            //DocScan cop1 = new DocScan();2022-10-12 11:00:47.050
            DataTable dt = new DataTable();
            String sql = "select dsc.hn, dsc.vn, dsc.an, dsc.visit_date, dsc.date_create, dsc.row_cnt, dsc.pic_before_scan_cnt" +
                ",pm02.MNC_PFIX_DSC as prefix, pm01.mnc_fname_t, pm01.mnc_lname_t, count(1) as cnt " +
                "From bn5_scan.dbo." + dsc.table + " dsc " +
                "Left Join bn5_scan.dbo.doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Left Join BNG5_DBMS_FRONT.dbo.patient_m01 pm01 on pm01.mnc_hn_no = dsc.hn " +
                " inner join BNG5_DBMS_FRONT.dbo.patient_m02 pm02 on pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Where dsc." + dsc.date_create + " >='" + datestart + "' and dsc." + dsc.date_create + " <='" + dateend + "' " +
                "and dsc." + dsc.active + "='1' and dsc.status_record = '1'  " +
                "Group By dsc.hn, dsc.vn, dsc.an, dsc.visit_date, dsc.date_create, dsc.row_cnt, dsc.pic_before_scan_cnt, pm02.MNC_PFIX_DSC , pm01.mnc_fname_t, pm01.mnc_lname_t " +
                "Order By dsc.date_create ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectLabOutByDateReq(String datestart, String dateend)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String wherehn = "";
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                "Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dsc." + dsc.date_req + " >='" + datestart + "' and dsc." + dsc.date_req + " <='" + dateend + "' " +
                "and dsc." + dsc.active + "='1'  /*and dgss.dept_us = '1'*/ and dsc." + dsc.status_record + "='2' " +
                "Order By dsc.doc_scan_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectLabOutByHnReqDateReqNo(String hn, String datestart, String preno, String datereq, String reqno)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String wherehn = "";
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                "Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' /*and dsc." + dsc.visit_date + " ='" + datestart + "'*/ " +
                "and dsc." + dsc.active + "='1'  and dsc."+dsc.pre_no +"= '"+ preno + "' and dsc." + dsc.status_record + "='2' and dsc."+ dsc.req_id+" = '"+ reqno+"' and dsc."+dsc.date_req+" = '"+datereq+"' " +
                "Order By dsc.doc_scan_id ";
            dt = conn.selectData(conn.conn, sql);
            //new LogWriter("d", "selectLabOutByHnReqDateReqNo sql " + sql);
            return dt;
        }
        public DataTable selectLabOutByHnReqDateReqNo(String hn, String datereq, String reqno)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String wherehn = "";
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                "Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' " +
                "and dsc." + dsc.active + "='1'  and dsc." + dsc.status_record + "='2' and dsc." + dsc.req_id + " = '" + reqno + "' and dsc." + dsc.date_req + " = '" + datereq + "' " +
                "Order By dsc.doc_scan_id ";
            dt = conn.selectData(conn.conn, sql);
            //new LogWriter("d", "selectLabOutByHnReqDateReqNo sql " + sql);
            return dt;
        }
        public DocScan selectLabOutByHnReqDateReqNo1(String hn, String datereq, String reqno)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String wherehn = "";
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                "Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' " +
                "and dsc." + dsc.active + "='1'  and dsc." + dsc.status_record + "='2' and dsc." + dsc.req_id + " = '" + reqno + "' and dsc." + dsc.date_req + " = '" + datereq + "' " +
                "Order By dsc.doc_scan_id ";
            dt = conn.selectData(conn.conn, sql);
            cop1 = setDocScan(dt);
            //new LogWriter("d", "selectLabOutByHnReqDateReqNo sql " + sql);
            return cop1;
        }
        public DocScan selectLabOutByHnReqDateReqNoUnActive(String hn, String datereq, String reqno)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String wherehn = "";
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                "Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' " +
                "and dsc." + dsc.active + "='3'  and dsc." + dsc.status_record + "='2' and dsc." + dsc.req_id + " = '" + reqno + "' and dsc." + dsc.date_req + " = '" + datereq + "' " +
                "Order By dsc.doc_scan_id ";
            dt = conn.selectData(conn.conn, sql);
            cop1 = setDocScan(dt);
            //new LogWriter("d", "selectLabOutByHnReqDateReqNo sql " + sql);
            return cop1;
        }
        public DataTable selectLabOutByAn( String hn, String an)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String wherehn = "", wherean = "", wheredatestart = "", where = "", orderby = "";
            if (hn.Length > 0)
            {
                wherehn = " dsc.hn like '%" + hn + "%' ";
            }
            wherean = " and dsc.an = '"+ an + "'";
            where = wherehn+ wherean;
            //MessageBox.Show("2222", "");
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                "Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where  " + where +
                "and dsc." + dsc.active + "='1'  /*and dgss.dept_us = '1'*/ and dsc." + dsc.status_record + " = '2' " +
                orderby;
            //new LogWriter("d", "sql " + sql);
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectLabOutByDateReq(String datestart, String dateend, String hn, String flagDate)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String wherehn = "", wheredateend="", wheredatestart="", where="", orderby="";
            if (hn.Length > 0)
            {
                wherehn = " dsc.hn like '%"+ hn + "%'";
            }
            if (flagDate.Equals("daterequest"))
            {
                if (dateend.Length > 0)
                {
                    wheredateend = " dsc." + dsc.date_req + " <='" + dateend + "' ";
                }
                if (datestart.Length > 0)
                {
                    wheredatestart = " dsc." + dsc.date_req + " >='" + datestart + "' ";
                }
                if ((datestart.Length >= 0) && (dateend.Length > 0) && (hn.Length > 0))
                {
                    where = wheredatestart + " and " + wheredateend + " and " + wherehn;
                }
                else if ((datestart.Length > 0) && (dateend.Length > 0) && (hn.Length <= 0))
                {
                    where = wheredatestart + " and " + wheredateend;
                }
                else if ((datestart.Length > 0) && (dateend.Length <= 0) && (hn.Length > 0))
                {
                    where = wheredatestart + " and " + wherehn;
                }
                else if ((datestart.Length <= 0) && (dateend.Length <= 0) && (hn.Length > 0))
                {
                    where = wherehn;
                }
                orderby = "Order By dsc.date_req, dsc.doc_scan_id ";
            }
            else
            {
                if (dateend.Length > 0)
                {
                    wheredateend = " dsc." + dsc.date_create + " <='" + dateend + " 23:59:59' ";
                }
                if (datestart.Length > 0)
                {
                    wheredatestart = " dsc." + dsc.date_create + " >='" + datestart + " 00:00:00' ";
                }
                if ((datestart.Length >= 0) && (dateend.Length > 0) && (hn.Length > 0))
                {
                    where = wheredatestart + " and " + wheredateend + " and " + wherehn;
                }
                else if ((datestart.Length > 0) && (dateend.Length > 0) && (hn.Length <= 0))
                {
                    where = wheredatestart + " and " + wheredateend;
                }
                else if ((datestart.Length > 0) && (dateend.Length <= 0) && (hn.Length > 0))
                {
                    where = wheredatestart + " and " + wherehn;
                }
                else if ((datestart.Length <= 0) && (dateend.Length <= 0) && (hn.Length > 0))
                {
                    where = wherehn;
                }
                orderby = "Order By dsc.doc_scan_id ";
            }
            //MessageBox.Show("2222", "");
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                "Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where  " + where +
                "and dsc." + dsc.active + "='1'  /*and dgss.dept_us = '1'*/ and dsc." + dsc.status_record + " = '2' " +
                orderby;
            //new LogWriter("d", "sql " + sql);
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectLabOutByDateReq2(String datestart, String dateend, String hn, String flagDate)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String wherehn = "", wheredateend = "", wheredatestart = "", where = "", orderby = "";
            if (hn.Length > 0)
            {
                wherehn = " dsc.hn like '%" + hn + "%'";
            }
            if (flagDate.Equals("daterequest"))
            {
                if (dateend.Length > 0)
                {
                    wheredateend = " dsc." + dsc.date_req + " <='" + dateend + "' ";
                }
                if (datestart.Length > 0)
                {
                    wheredatestart = " dsc." + dsc.date_req + " >='" + datestart + "' ";
                }
                if ((datestart.Length >= 0) && (dateend.Length > 0) && (hn.Length > 0))
                {
                    where = wheredatestart + " and " + wheredateend + " and " + wherehn;
                }
                else if ((datestart.Length > 0) && (dateend.Length > 0) && (hn.Length <= 0))
                {
                    where = wheredatestart + " and " + wheredateend;
                }
                else if ((datestart.Length > 0) && (dateend.Length <= 0) && (hn.Length > 0))
                {
                    where = wheredatestart + " and " + wherehn;
                }
                else if ((datestart.Length <= 0) && (dateend.Length <= 0) && (hn.Length > 0))
                {
                    where = wherehn;
                }
                orderby = "Order By dsc.date_req, dsc.doc_scan_id ";
            }
            else
            {
                if (dateend.Length > 0)
                {
                    wheredateend = " dsc." + dsc.date_create + " <='" + dateend + " 23:59:59' ";
                }
                if (datestart.Length > 0)
                {
                    wheredatestart = " dsc." + dsc.date_create + " >='" + datestart + " 00:00:00' ";
                }
                if ((datestart.Length >= 0) && (dateend.Length > 0) && (hn.Length > 0))
                {
                    where = wheredatestart + " and " + wheredateend + " and " + wherehn;
                }
                else if ((datestart.Length > 0) && (dateend.Length > 0) && (hn.Length <= 0))
                {
                    where = wheredatestart + " and " + wheredateend;
                }
                else if ((datestart.Length > 0) && (dateend.Length <= 0) && (hn.Length > 0))
                {
                    where = wheredatestart + " and " + wherehn;
                }
                else if ((datestart.Length <= 0) && (dateend.Length <= 0) && (hn.Length > 0))
                {
                    where = wherehn;
                }
                orderby = "Order By dsc.doc_scan_id ";
            }
            //MessageBox.Show("2222", "");
            String sql = "select dsc.*, pm01.mnc_id_nam " +
                "From "+ conn .connMainHIS.Database+ ".dbo." + dsc.table + " dsc " +
                "Left Join " + conn.connMainHIS.Database + ".dbo.doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Left Join " + conn.connMainHIS.Database + ".dbo.patient_m01 pm01 on dsc.hn = pm01.mnc_hn_ho " +
                "Where  " + where +
                "and dsc." + dsc.active + "='1'  /*and dgss.dept_us = '1'*/ and dsc." + dsc.status_record + " = '2' " +
                orderby;
            //new LogWriter("d", "sql " + sql);
            try
            {
                dt = conn.selectData(conn.conn, sql);
                new LogWriter("d", "DocScanDb selectLabOutByDateReq2 sql " + sql);
            }
            catch(Exception ex)
            {
                new LogWriter("e", "DocScanDb selectLabOutByDateReq2 sql " + sql);
            }

            return dt;
        }
        public DataTable selectMedResultByDateReq(String datestart, String dateend, String hn)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String wherehn = "", wheredateend = "", wheredatestart = "", where = "", orderby = "";
            if (hn.Length > 0)
            {
                wherehn = " dsc.hn like '%" + hn + "%'";
            }
            
            if (dateend.Length > 0)
            {
                wheredateend = " dsc." + dsc.date_req + " <='" + dateend + "' ";
            }
            if (datestart.Length > 0)
            {
                wheredatestart = " dsc." + dsc.date_req + " >='" + datestart + "' ";
            }
            if ((datestart.Length >= 0) && (dateend.Length > 0) && (hn.Length > 0))
            {
                where = wheredatestart + " and " + wheredateend + " and " + wherehn;
            }
            else if ((datestart.Length > 0) && (dateend.Length > 0) && (hn.Length <= 0))
            {
                where = wheredatestart + " and " + wheredateend;
            }
            else if ((datestart.Length > 0) && (dateend.Length <= 0) && (hn.Length > 0))
            {
                where = wheredatestart + " and " + wherehn;
            }
            else if ((datestart.Length <= 0) && (dateend.Length <= 0) && (hn.Length > 0))
            {
                where = wherehn;
            }
            orderby = "Order By dsc.date_req, dsc.doc_scan_id ";            
            //MessageBox.Show("2222", "");
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                "Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where  " + where +
                "and dsc." + dsc.active + "='1'  /*and dgss.dept_us = '1'*/ and dsc." + dsc.status_record + " = '3' " +
                orderby;
            //new LogWriter("d", "sql " + sql);
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectLabOutByDateReq1(String datestart, String dateend, String hn, String flagDate)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String wherehn = "", wheredateend = "", wheredatestart = "", where = "", orderby = "";
            if (hn.Length > 0)
            {
                wherehn = " dsc.hn like '%" + hn + "%'";
            }
            if (flagDate.Equals("daterequest"))
            {
                if (dateend.Length > 0)
                {
                    wheredateend = " dsc." + dsc.date_req + " <='" + dateend + "' ";
                }
                if (datestart.Length > 0)
                {
                    wheredatestart = " dsc." + dsc.date_req + " >='" + datestart + "' ";
                }
                if ((datestart.Length >= 0) && (dateend.Length > 0) && (hn.Length > 0))
                {
                    where = wheredatestart + " and " + wheredateend + " and " + wherehn;
                }
                else if ((datestart.Length > 0) && (dateend.Length > 0) && (hn.Length <= 0))
                {
                    where = wheredatestart + " and " + wheredateend;
                }
                else if ((datestart.Length > 0) && (dateend.Length <= 0) && (hn.Length > 0))
                {
                    where = wheredatestart + " and " + wherehn;
                }
                else if ((datestart.Length <= 0) && (dateend.Length <= 0) && (hn.Length > 0))
                {
                    where = wherehn;
                }
                orderby = "Order By dsc.date_req, dsc.doc_scan_id ";
            }
            else
            {
                if (dateend.Length > 0)
                {
                    wheredateend = " dsc." + dsc.date_create + " <='" + dateend + "' ";
                }
                if (datestart.Length > 0)
                {
                    wheredatestart = " dsc." + dsc.date_create + " >='" + datestart + "' ";
                }
                if ((datestart.Length >= 0) && (dateend.Length > 0) && (hn.Length > 0))
                {
                    where = wheredatestart + " and " + wheredateend + " and " + wherehn;
                }
                else if ((datestart.Length > 0) && (dateend.Length > 0) && (hn.Length <= 0))
                {
                    where = wheredatestart + " and " + wheredateend;
                }
                else if ((datestart.Length > 0) && (dateend.Length <= 0) && (hn.Length > 0))
                {
                    where = wheredatestart + " and " + wherehn;
                }
                else if ((datestart.Length <= 0) && (dateend.Length <= 0) && (hn.Length > 0))
                {
                    where = wherehn;
                }
                orderby = "Order By dsc.doc_scan_id ";
            }
            //MessageBox.Show("2222", "");
            String sql = "select dsc.doc_scan_id, dsc.hn, dsc.patient_fullname, convert(VARCHAR(20),dsc.date_create,23), convert(VARCHAR(20),dsc.date_req,23), dsc.req_id, dsc.vn, dsc.doc_scan_id " +
                "From " + dsc.table + " dsc " +
                "Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where  " + where +
                "and dsc." + dsc.active + "='1'  /*and dgss.dept_us = '1'*/ and dsc." + dsc.status_record + "='2' " +
                orderby;
            //new LogWriter("d", "sql " + sql);
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectLabOutByHn(String hn)
        {
            //DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String wherehn = "", wheredateend = "", wheredatestart = "", where = "";
            
            String sql = "Select dsc.* " +
                "From " + dsc.table + " dsc " +
                "Left Join doc_group_sub_scan dgss On dsc.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where  dsc." + dsc.hn + " in (" + hn + ") " +
                "and dsc." + dsc.active + "='1'  /*and dgss.dept_us = '1'*/ and dsc." + dsc.status_record + "='2' "+ 
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
                "and dsc." + dsc.active + "='1'  /*and dgss.dept_us = '1'*/ and dsc." + dsc.status_record+"='2' " +
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
        public DocScan selectByStatusMedicalExamination(String hn, String mlfm, String vsdate, String preno)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' " +
                "and status_record = '5' " +
                "and ml_fm = '"+ mlfm + "' " +
                "and visit_date = '" + vsdate + "' " +
                "and pre_no = '" + preno + "' and active = '1' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);
            cop1 = setDocScan(dt);
            return cop1;
        }
        public DataTable selectByDocOLD(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' " +
                " and status_record = '3' and active = '1' and doc_group_sub_id = '1200000039' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);
            return dt;
        }
        public DataTable selectByHolter(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' " +
                " and status_record = '3' and active = '1' and doc_group_sub_id = '1200000042' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);
            return dt;
        }
        public DataTable selectByCertMed(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' " +
                " and status_record = '4' and active = '1' and doc_group_sub_id = '1200000030' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);
            return dt;
        }
        public DataTable selectByECHO(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' " +
                " and status_record = '3' and active = '1' and doc_group_sub_id = '1200000041' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);
            return dt;
        }
        public DataTable selectByEST(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' " +
                " and status_record = '3' and active = '1' and doc_group_sub_id = '1200000040' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);
            return dt;
        }
        public DataTable selectByEKG(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' " +
                " and status_record = '3' and active = '1' and doc_group_sub_id = '1200000038' " +
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
                "Order By sort1 ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectByVnDocScan(String hn, String vn, String vsDate)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.vn + "='" + vn + "' and dsc." + dsc.visit_date + "='" + vsDate + "' and dsc." + dsc.active + "='1' and status_ipd = 'O' " +
                "Order By sort1 ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectByFmCode(String fmcode, String limit)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select TOP("+ limit + ") dsc.* " +
                "From " + dsc.table + " dsc " +
                "Where dsc." + dsc.ml_fm + " ='" + fmcode.Replace("'","''") + "' and dsc." + dsc.active + "='1' " +
                "Order By dsc.doc_scan_id ";
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
                "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.an + "='" + an + "' and dsc." + dsc.active + "='1'   " +
                "Order By sort1 ";
            //user อยากให้ sort แล้ว  67-12-19
            //sql = "select * " +
            //    "From " + dsc.table + " dsc " +
            //    //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
            //    "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.an + "='" + an + "' and dsc." + dsc.active + "='1'and dsc." + dsc.status_record + "='1' " +
            //    "Order By dsc.doc_scan_id,dsc.row_no ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectByAnScan(String hn, String an)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.an + "='" + an + "' and dsc." + dsc.active + "='1' and status_record = '1' " +
                "Order By sort1 ";
            //user อยากให้ sort แล้ว  67-12-19
            //sql = "select * " +
            //    "From " + dsc.table + " dsc " +
            //    //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
            //    "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.an + "='" + an + "' and dsc." + dsc.active + "='1'and dsc." + dsc.status_record + "='1' " +
            //    "Order By dsc.doc_scan_id,dsc.row_no ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectByAn(String hn, String an, String sort1)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sort2 = "";
            if ((sort1.Length > 0) && sort1.Equals("sort1"))
            {
                sort2 = "Order By sort1 ";
            }
            else{
                sort2 = "Order By dsc.doc_scan_id,dsc.row_no ";
            }
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.an + "='" + an + "' and dsc." + dsc.active + "='1' " +
                sort2;
            //user อยากให้ sort แล้ว  67-12-19
            //sql = "select * " +
            //    "From " + dsc.table + " dsc " +
            //    //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
            //    "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.an + "='" + an + "' and dsc." + dsc.active + "='1'and dsc." + dsc.status_record + "='1' " +
            //    "Order By dsc.doc_scan_id,dsc.row_no ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectOutLabByAn(String hn, String an)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.an + "='" + an + "' and dsc." + dsc.active + "='1' " +
                "Order By sort1 ";
            sql = "select * " +
                "From " + dsc.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.an + "='" + an + "' and dsc." + dsc.active + "='1'and dsc." + dsc.status_record + "='2' " +
                "Order By dsc.doc_scan_id,dsc.row_no ";
            dt = conn.selectData(conn.conn, sql);
            return dt;
        }
        public DataTable selectByAnDocGrp(String hn, String an, String docgrpid)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select dsc.* " +
                "From " + dsc.table + " dsc " +
                "left Join doc_group_fm fmcode On dsc.ml_fm = fmcode.fm_code " +
                "left join doc_group_sub_scan dgss on fmcode.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.an + "='" + an + "' and dsc." + dsc.active + "='1' and dsc.doc_group_id ='" + docgrpid + "' " +
                "Order By sort1 ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectByAn2(string hn, string an, string sort1)
        {
            DocScan docScan = new DocScan();
            DataTable dataTable = new DataTable();
            return this.conn.selectData(this.conn.conn, "select dsc.* From " + this.dsc.table + " dsc Where dsc." + this.dsc.hn + " ='" + hn + "' and dsc." + this.dsc.an + "='" + an + "' and dsc." + this.dsc.active + "='1' and dsc." + this.dsc.sort1 + " > " + sort1 + " Order By dsc.sort1 ");
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
        public DataTable selectByDoctorOrder(String hn, String vsdate, String preno)
        {
            DocScan cop1 = new DocScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + dsc.table + " dsc " +
                "Where dsc." + dsc.hn + " ='" + hn + "' and dsc." + dsc.visit_date + "='" + vsdate + "'and dsc." + dsc.pre_no + "='" + preno + "' and dsc." + dsc.active + "='1' and doc_group_sub_id = '1200000003' " +
                "Order By sort1 ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        private void chkNull(DocScan p)
        {
            long chk = 0;
            int chk1 = 0;

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
            p.patient_fullname = p.patient_fullname == null ? "" : p.patient_fullname;
            //p.status_record = p.status_record == null ? "" : p.status_record;

            p.doc_group_id = long.TryParse(p.doc_group_id, out chk) ? chk.ToString() : "0";
            p.row_no = long.TryParse(p.row_no, out chk) ? chk.ToString() : "0";
            p.doc_group_sub_id = long.TryParse(p.doc_group_sub_id, out chk) ? chk.ToString() : "0";
            p.pic_before_scan_cnt = long.TryParse(p.pic_before_scan_cnt, out chk) ? chk.ToString() : "0";
            
            p.status_record = int.TryParse(p.status_record, out chk1) ? chk1.ToString() : "0";
            p.sort1 = int.TryParse(p.sort1, out chk1) ? chk1.ToString() : "0";
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
                new LogWriter("e", "insertScreenCapture sql " + sql );
            }
            finally
            {
                conn.conn.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertEKG(DocScan p, String userId)
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
                conn.comStore.CommandText = "insert_doc_scan_v41";
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
                new LogWriter("e", "insertEKG sql " + sql);
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
                p.patient_fullname = p.patient_fullname.Replace("'", "''");
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.conn;
                conn.comStore.CommandText = "[insert_doc_scan_lab_out]";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("doc_group_id", p.doc_group_id);
                conn.comStore.Parameters.AddWithValue("host_ftp", p.host_ftp);
                conn.comStore.Parameters.AddWithValue("hn", p.hn);
                conn.comStore.Parameters.AddWithValue("vn", p.vn);
                conn.comStore.Parameters.AddWithValue("visit_date", p.visit_date);
                conn.comStore.Parameters.AddWithValue("remark", p.remark);
                conn.comStore.Parameters.AddWithValue("user_create", userId);
                conn.comStore.Parameters.AddWithValue("an", p.an);
                conn.comStore.Parameters.AddWithValue("doc_group_sub_id", p.doc_group_sub_id);
                conn.comStore.Parameters.AddWithValue("pre_no", p.pre_no);

                conn.comStore.Parameters.AddWithValue("an_date", p.an_date);
                conn.comStore.Parameters.AddWithValue("status_ipd", p.status_ipd);
                conn.comStore.Parameters.AddWithValue("ext", p.image_path);                
                conn.comStore.Parameters.AddWithValue("folder_ftp", p.folder_ftp);
                conn.comStore.Parameters.AddWithValue("row_no2", p.row_no);
                conn.comStore.Parameters.AddWithValue("row_cnt", p.row_cnt);
                conn.comStore.Parameters.AddWithValue("status_version", p.status_version);
                conn.comStore.Parameters.AddWithValue("pic_before_scan", p.pic_before_scan_cnt);
                conn.comStore.Parameters.AddWithValue("date_req", p.date_req);
                conn.comStore.Parameters.AddWithValue("req_id", p.req_id);

                conn.comStore.Parameters.AddWithValue("ml_fm", p.ml_fm);
                conn.comStore.Parameters.AddWithValue("patient_fullname", p.patient_fullname);
                conn.comStore.Parameters.AddWithValue("comp_labout_id", p.comp_labout_id);

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
                new LogWriter("e", "insertLabOut "+ sql);
            }
            finally
            {
                conn.conn.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertMed(DocScan p, String userId)
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
                p.patient_fullname = p.patient_fullname.Replace("'", "''");
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.conn;
                conn.comStore.CommandText = "[insert_doc_scan_med]";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("doc_group_id", p.doc_group_id);
                conn.comStore.Parameters.AddWithValue("host_ftp", p.host_ftp);
                conn.comStore.Parameters.AddWithValue("hn", p.hn);
                conn.comStore.Parameters.AddWithValue("vn", p.vn);
                conn.comStore.Parameters.AddWithValue("visit_date", p.visit_date);
                conn.comStore.Parameters.AddWithValue("remark", p.remark);
                conn.comStore.Parameters.AddWithValue("user_create", userId);
                conn.comStore.Parameters.AddWithValue("an", p.an);
                conn.comStore.Parameters.AddWithValue("doc_group_sub_id", p.doc_group_sub_id);
                conn.comStore.Parameters.AddWithValue("pre_no", p.pre_no);

                conn.comStore.Parameters.AddWithValue("an_date", p.an_date);
                conn.comStore.Parameters.AddWithValue("status_ipd", p.status_ipd);
                conn.comStore.Parameters.AddWithValue("ext", p.image_path);
                conn.comStore.Parameters.AddWithValue("folder_ftp", p.folder_ftp);
                conn.comStore.Parameters.AddWithValue("row_no2", p.row_no);
                conn.comStore.Parameters.AddWithValue("row_cnt", p.row_cnt);
                conn.comStore.Parameters.AddWithValue("status_version", p.status_version);
                conn.comStore.Parameters.AddWithValue("pic_before_scan", p.pic_before_scan_cnt);
                conn.comStore.Parameters.AddWithValue("date_req", p.date_req);
                conn.comStore.Parameters.AddWithValue("req_id", p.req_id);

                conn.comStore.Parameters.AddWithValue("ml_fm", p.ml_fm);
                conn.comStore.Parameters.AddWithValue("patient_fullname", p.patient_fullname);

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
                new LogWriter("e", "insertLabOut " + sql);
            }
            finally
            {
                conn.conn.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertMedicalExamination(DocScan p, String userId)
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
                p.patient_fullname = p.patient_fullname.Replace("'", "''");
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.conn;
                conn.comStore.CommandText = "[insert_doc_scan_medical_examination]";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("doc_group_id", p.doc_group_id);
                conn.comStore.Parameters.AddWithValue("host_ftp", p.host_ftp);
                conn.comStore.Parameters.AddWithValue("hn", p.hn);
                conn.comStore.Parameters.AddWithValue("vn", p.vn);
                conn.comStore.Parameters.AddWithValue("visit_date", p.visit_date);
                conn.comStore.Parameters.AddWithValue("remark", p.remark);
                conn.comStore.Parameters.AddWithValue("user_create", userId);
                conn.comStore.Parameters.AddWithValue("an", p.an);
                conn.comStore.Parameters.AddWithValue("doc_group_sub_id", p.doc_group_sub_id);
                conn.comStore.Parameters.AddWithValue("pre_no", p.pre_no);

                conn.comStore.Parameters.AddWithValue("an_date", p.an_date);
                conn.comStore.Parameters.AddWithValue("status_ipd", p.status_ipd);
                conn.comStore.Parameters.AddWithValue("ext", p.image_path);
                conn.comStore.Parameters.AddWithValue("folder_ftp", p.folder_ftp);
                conn.comStore.Parameters.AddWithValue("row_no2", p.row_no);
                conn.comStore.Parameters.AddWithValue("row_cnt", p.row_cnt);
                conn.comStore.Parameters.AddWithValue("status_version", p.status_version);
                conn.comStore.Parameters.AddWithValue("pic_before_scan", p.pic_before_scan_cnt);
                conn.comStore.Parameters.AddWithValue("date_req", p.date_req);
                conn.comStore.Parameters.AddWithValue("req_id", p.req_id);

                conn.comStore.Parameters.AddWithValue("ml_fm", p.ml_fm);
                conn.comStore.Parameters.AddWithValue("patient_fullname", p.patient_fullname);

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
                new LogWriter("e", "insertMedicalExamination " + sql);
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
                conn.comStore.CommandText = "insert_doc_scan_v31";
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
                new LogWriter("e", "insert " + sql);
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
        public String updateFmCodeByFmCodeLimit(String mlfm_new, String mlfm_old, String limit)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            //chkNull(p);
            sql = "Update " + dsc.table + " Set " +
                " " + dsc.ml_fm + " = '" + mlfm_new + "' " +
                "Where " + dsc.ml_fm + "='" + mlfm_old.Replace("'","''") + "' and active = '1' " +
                //"ORDER BY dsc."+dsc.pkField+" LIMIT "+ limit
                " "
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
        public String voidDocScanByStatusCertMedical(String hn, String mlfm, String vsdate, String preno, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = convert(varchar(20), getdate(),23) " +
                "," + dsc.user_cancel + " = '" + userId + "' " +
                "Where " + dsc.hn + " ='" + hn + "' " +
                "and status_record = '4' " +
                "and ml_fm = '" + mlfm + "' " +
                "and visit_date = '" + vsdate + "' " +
                "and pre_no = '" + preno + "' and sort1 = '1' "
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "voidDocScan " + sql);
            }

            return re;
        }
        public String voidDocScanByStatusCertMedical2NFLEAF(String hn, String mlfm, String vsdate, String preno, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = convert(varchar(20), getdate(),23) " +
                "," + dsc.user_cancel + " = '" + userId + "' " +
                "Where " + dsc.hn + " ='" + hn + "' " +
                "and status_record = '4' " +
                "and ml_fm = '" + mlfm + "' " +
                "and visit_date = '" + vsdate + "' " +
                "and pre_no = '" + preno + "' and sort1 = '2' "
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "voidDocScan " + sql);
            }
            return re;
        }
        public String voidDocScanByStatusMedicalExamination(String hn, String mlfm, String vsdate, String preno, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = convert(varchar(20), getdate(),23) " +
                "," + dsc.user_cancel + " = '" + userId + "' " +
                "Where " + dsc.hn + " ='" + hn + "' " +
                "and status_record = '5' " +
                "and ml_fm = '" + mlfm + "' " +
                "and visit_date = '" + vsdate + "' " +
                "and pre_no = '" + preno + "' "
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "voidDocScan " + sql);
            }
            return re;
        }
        public String voidDocScanByStatusDoctorOrder(String hn, String vsdate, String preno, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = convert(varchar(20), getdate(),23) " +
                "," + dsc.user_cancel + " = '" + userId + "' " +
                "Where " + dsc.hn + " ='" + hn + "' " +
                "and status_record = '5' " +
                "and doc_group_sub_id = '1200000003' " +
                "and visit_date = '" + vsdate + "' " +
                "and pre_no = '" + preno + "' "
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "voidDocScan " + sql);
            }
            return re;
        }
        public String voidDocScanOutLab(String id, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = convert(varchar(20), getdate(),23) " +
                "," + dsc.user_cancel + " = '" + userId + "'" +
                ", status_conv1 = 'outlab'" +
                "Where " + dsc.pkField + "='" + id + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "voidDocScan " + sql);
            }
            return re;
        }
        public String voidDocScanCertMed(String id, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = convert(varchar(20), getdate(),23) " +
                "," + dsc.user_cancel + " = '" + userId + "'" +
                ", status_conv1 = 'cert_med'" +
                "Where " + dsc.pkField + "='" + id + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "voidDocScan " + sql);
            }
            return re;
        }
        public String voidDocScan1(String id, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = convert(varchar(20), getdate(),23) " +
                "," + dsc.user_cancel + " = '" + userId + "'" +
                ", status_conv1 = 'frmscanview1'" +
                "Where " + dsc.pkField + "='" + id + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "voidDocScan " + sql);
            }

            return re;
        }
        public String voidDocScan2(String id, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = convert(varchar(20), getdate(),23) " +
                "," + dsc.user_cancel + " = '" + userId + "'" +
                ", status_conv1 = 'frmscanviewedit'" +
                "Where " + dsc.pkField + "='" + id + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "voidDocScan " + sql);
            }

            return re;
        }
        public String voidDocScan(String dscid, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            
            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = convert(varchar(20), getdate(),23) " +
                "," + dsc.user_cancel + " = '" + userId + "'" +
                "Where " + dsc.pkField + "='" + dscid + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "voidDocScan " + sql);
            }

            return re;
        }
        public String voidDocScanVNByScan(String vn, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = convert(varchar(20), getdate(),23) " +
                "," + dsc.user_cancel + " = '" + userId + "'" +
                "Where " + dsc.vn + "='" + vn + "' and status_record = '1' "
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "voidDocScanVN " + sql);
            }

            return re;
        }
        public String voidDocScanANByScan(String an, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = convert(varchar(20), getdate(),23) " +
                "," + dsc.user_cancel + " = '" + userId + "'" +
                "Where " + dsc.an + "='" + an + "' and status_record = '1'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "voidDocScanAN " + sql);
            }
            return re;
        }
        public String updateGrpChange(String doc_group_id, String doc_group_sub_id, String fm_code)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.doc_group_id + " = '" + doc_group_id + "' " +
                "," + dsc.doc_group_sub_id + " = '" + doc_group_sub_id + "' " +
                "Where " + dsc.ml_fm + "='" + fm_code + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "updateSort " + sql);
            }
            return re;
        }
        public String updateGrpSubGrp(String id, String doc_group_id, String doc_group_sub_id)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.doc_group_id + " = '" + doc_group_id + "' " +
                "," + dsc.doc_group_sub_id + " = '" + doc_group_sub_id + "' " +
                "Where " + dsc.pkField + "='" + id + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "updateSort " + sql);
            }
            return re;
        }
        public String updateFMCode(String id, String fmcode)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.ml_fm + " = '" + fmcode + "'" +
                "Where " + dsc.pkField + "='" + id + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "updateSort " + sql);
            }
            return re;
        }
        public String updateSort(String id, String sort1)
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Update " + dsc.table + " Set " +
                " " + dsc.sort1 + " = '"+ sort1 + "'" +
                "Where " + dsc.pkField + "='" + id + "'"
                ;
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "updateSort " + sql);
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
                dgs1.status_record = dt.Rows[0][dsc.status_record].ToString();
                dgs1.sort1 = dt.Rows[0][dsc.sort1].ToString();
                dgs1.ml_fm = dt.Rows[0][dsc.ml_fm].ToString();
                dgs1.row_cnt = dt.Rows[0][dsc.row_cnt].ToString();
                dgs1.req_id = dt.Rows[0][dsc.req_id].ToString();
                dgs1.comp_labout_id = dt.Rows[0][dsc.comp_labout_id] != null ? dt.Rows[0][dsc.comp_labout_id].ToString() : "";
                dgs1.patient_fullname = dt.Rows[0][dsc.patient_fullname].ToString();
                dgs1.ml_date_time_start = dt.Rows[0][dsc.ml_date_time_start].ToString();
                dgs1.ml_date_time_end = dt.Rows[0][dsc.ml_date_time_end].ToString();
                dgs1.status_ml = dt.Rows[0][dsc.status_ml].ToString();
                //dgs1.ml_date_time_end = dt.Rows[0][dsc.ml_date_time_end].ToString();
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
            dgs1.status_record = "";
            dgs1.sort1 = "";
            dgs1.ml_fm = "";
            dgs1.row_cnt = "";
            dgs1.status_ml = "";
            dgs1.ml_date_time_start = "";
            dgs1.ml_date_time_end = "";
            dgs1.date_req = "";
            dgs1.req_id = "";
            dgs1.patient_fullname = "";
            dgs1.comp_labout_id = "";
            
            return dgs1;
        }
        public DocScan castDocScan(DocScan dsc1)
        {
            DocScan dsc2 = new DocScan();
            dsc2.doc_scan_id = dsc1.doc_scan_id;
            dsc2.doc_group_id = dsc1.doc_group_id;
            dsc2.row_no = dsc1.row_no;
            dsc2.host_ftp = dsc1.host_ftp;
            dsc2.image_path = dsc1.image_path;
            dsc2.hn = dsc1.hn;
            dsc2.vn = dsc1.vn;
            dsc2.visit_date = dsc1.visit_date;
            dsc2.active = dsc1.active;
            dsc2.remark = dsc1.remark;
            dsc2.date_create = dsc1.date_create;
            dsc2.date_modi = dsc1.date_modi;
            dsc2.date_cancel = dsc1.date_cancel;
            dsc2.user_create = dsc1.user_create;
            dsc2.user_modi = dsc1.user_modi;
            dsc2.user_cancel = dsc1.user_cancel;
            dsc2.an = dsc1.an;
            dsc2.doc_group_sub_id = dsc1.doc_group_sub_id;
            dsc2.pre_no = dsc1.pre_no;
            dsc2.an_date = dsc1.an_date;
            dsc2.status_ipd = dsc1.status_ipd;
            dsc2.an_cnt = dsc1.an_cnt;
            dsc2.folder_ftp = dsc1.folder_ftp;
            dsc2.status_version = dsc1.status_version;
            dsc2.pic_before_scan_cnt = dsc1.pic_before_scan_cnt;
            dsc2.status_record = dsc1.status_record;
            dsc2.sort1 = dsc1.sort1;
            dsc2.ml_fm = dsc1.ml_fm;
            dsc2.row_cnt = dsc1.row_cnt;
            dsc2.status_ml = dsc1.status_ml;
            dsc2.ml_date_time_start = dsc1.ml_date_time_start;
            dsc2.ml_date_time_end = dsc1.ml_date_time_end;
            dsc2.date_req = dsc1.date_req;
            dsc2.req_id = dsc1.req_id;
            dsc2.patient_fullname = dsc1.patient_fullname;
            dsc2.comp_labout_id = dsc1.comp_labout_id;

            return dsc2;
        }
        public String voidDocScanPID(String hn, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            sql = "Update " + dsc.table + " Set " +
                " " + dsc.active + " = '3'" +
                "," + dsc.date_cancel + " = getdate()" +
                "," + dsc.user_cancel + " = '" + userId + "'" +
                "Where " + dsc.hn + "='" + hn + "' and " + dsc.status_record + "='4' and remark = 'บัตรปชช' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "voidDocScan " + sql);
            }
            return re;
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
