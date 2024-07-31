using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class MedicalCertificateDB
    {
        ConnectDB conn;
        MedicalCertificate mcerti;

        public MedicalCertificateDB(ConnectDB c)
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            mcerti = new MedicalCertificate();
            mcerti.active = "active";
            mcerti.an = "an";
            mcerti.certi_code = "certi_code";
            mcerti.certi_id = "certi_id";
            
            mcerti.date_create = "date_create";
            
            mcerti.dtr_code = "dtr_code";
            mcerti.dtr_name_e = "dtr_name_e";
            mcerti.dtr_name_t = "dtr_name_t";
            mcerti.hn = "hn";
            mcerti.line1 = "line1";
            mcerti.line2 = "line2";
            mcerti.line3 = "line3";
            mcerti.line4 = "line4";
            mcerti.pre_no = "pre_no";
            mcerti.ptt_name_e = "ptt_name_e";
            mcerti.ptt_name_t = "ptt_name_t";
            mcerti.remark = "remark";
            mcerti.status_ipd = "status_ipd";
            mcerti.user_create = "user_create";
            mcerti.visit_date = "visit_date";
            mcerti.visit_time = "visit_time";
            mcerti.doc_scan_id = "doc_scan_id";
            mcerti.status_2nd_leaf = "status_2nd_leaf";
            mcerti.counter_name = "counter_name";

            mcerti.table = "t_medical_certificate";
            mcerti.pkField = "certi_id";
        }
        public String selectCertIDByHn(String hn, String preno, String vsdate)
        {
            DataTable dt = new DataTable();
            String re = "";
            String sql = "select certi_id " +
                "From " + mcerti.table + " dgs " +
                "Where hn = '" + hn + "' and visit_date = '" + vsdate + "' and pre_no ='" + preno + "' and active = '1'  and status_2nd_leaf = '1'";
            dt = conn.selectData(conn.conn, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["certi_id"].ToString();
            }
            return re;
        }
        public String selectCertIDByAn(String an)
        {
            DataTable dt = new DataTable();
            String re = "";
            String sql = "select certi_id " +
                "From " + mcerti.table + " dgs " +
                "Where an = '" + an + "'and active = '1'  and status_2nd_leaf = '1'";
            dt = conn.selectData(conn.conn, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["certi_id"].ToString();
            }
            return re;
        }
        public String selectCertIDByHn2ndLeaf(String hn, String preno, String vsdate)
        {
            DataTable dt = new DataTable();
            String re = "";
            String sql = "select certi_id " +
                "From " + mcerti.table + " dgs " +
                "Where hn = '" + hn + "' and visit_date = '" + vsdate + "' and pre_no ='" + preno + "' and active = '1' and status_2nd_leaf = '2' ";
            dt = conn.selectData(conn.conn, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["certi_id"].ToString();
            }
            return re;
        }
        public String selectCertIDByHn2ndLeafAN(String an)
        {
            DataTable dt = new DataTable();
            String re = "";
            String sql = "select certi_id " +
                "From " + mcerti.table + " dgs " +
                "Where an = '" + an + "' and active = '1' and status_2nd_leaf = '2' ";
            dt = conn.selectData(conn.conn, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["certi_id"].ToString();
            }
            return re;
        }
        public String selectDocScanIDByHn(String hn, String preno, String vsdate)
        {
            DataTable dt = new DataTable();
            String re = "";
            String sql = "select doc_scan_id " +
                "From " + mcerti.table + " dgs " +
                "Where hn = '" + hn + "' and visit_date = '" + vsdate + "' and pre_no ='" + preno + "' and active = '1' and status_2nd_leaf != '2' ";
            dt = conn.selectData(conn.conn, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["doc_scan_id"].ToString();
            }
            return re;
        }
        public MedicalCertificate selectByPk(String id)
        {
            MedicalCertificate cop1 = new MedicalCertificate();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + mcerti.table + " dgs " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dgs." + mcerti.pkField + " ='" + id + "' " +
                " ";
            dt = conn.selectData(conn.conn, sql);
            cop1 = setMedicalCert(dt);
            return cop1;
        }
        /*
         * Method นี้ ใช้เพราะ มี bug การinsert ไม่มี vsdate
         */
        public String updateVsDateByPk(String id, String vsdate)
        {
            String sql = "", re = "";
            sql = "update " + mcerti.table + " set " +
                "visit_date = '" + vsdate + "' " +
                "Where certi_id = '" + id + "' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateDocScanIdByPk sql " + sql + " id " + id);
            }
            return re;
        }
        public String updateDocScanIdByPk(String id, String dscid)
        {
            String sql = "", re = "";
            sql = "update " + mcerti.table + " set " +
                "doc_scan_id = '" + dscid + "' " +
                "Where certi_id = '" + id + "' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateDocScanIdByPk sql " + sql + " id " + id);
            }
            return re;
        }
        public String voidCertByHn(String hn, String preno, String vsdate, String status_2nd_leaf)
        {
            String sql = "", re = "";
            if (status_2nd_leaf.Equals("1"))
            {
                sql = "update " + mcerti.table + " set " +
                "active = '3' " +
                "Where hn = '" + hn + "' and visit_date = '" + vsdate + "' and pre_no ='" + preno + "' and status_2nd_leaf = '1' ";
            }
            else
            {
                sql = "update " + mcerti.table + " set " +
                "active = '3' " +
                "Where hn = '" + hn + "' and visit_date = '" + vsdate + "' and pre_no ='" + preno + "' and status_2nd_leaf = '2' ";
            }
            
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "voidCertByHn sql " + sql + " hn " + hn);
            }
            return re;
        }
        private void chkNull(MedicalCertificate p)
        {
            long chk = 0;
            int chk1 = 0;

            p.date_modi = p.date_modi == null ? "" : p.date_modi;
            p.date_cancel = p.date_cancel == null ? "" : p.date_cancel;
            p.user_create = p.user_create == null ? "" : p.user_create;
            p.user_modi = p.user_modi == null ? "" : p.user_modi;
            p.user_cancel = p.user_cancel == null ? "" : p.user_cancel;

            p.active = p.active == null ? "" : p.active;
            p.an = p.an == null ? "" : p.an;
            p.certi_code = p.certi_code == null ? "" : p.certi_code;
            p.dtr_code = p.dtr_code == null ? "" : p.dtr_code;
            p.dtr_name_e = p.dtr_name_e == null ? "" : p.dtr_name_e;
            p.dtr_name_t = p.dtr_name_t == null ? "" : p.dtr_name_t;
            p.hn = p.hn == null ? "" : p.hn;
            p.line1 = p.line1 == null ? "" : p.line1;
            p.line2 = p.line2 == null ? "" : p.line2;
            p.line3 = p.line3 == null ? "" : p.line3;
            p.line4 = p.line4 == null ? "" : p.line4;
            p.ptt_name_e = p.ptt_name_e == null ? "" : p.ptt_name_e;
            p.ptt_name_t = p.ptt_name_t == null ? "" : p.ptt_name_t;
            p.status_ipd = p.status_ipd == null ? "" : p.status_ipd;
            p.visit_date = p.visit_date == null ? "" : p.visit_date;
            p.visit_time = p.visit_time == null ? "" : p.visit_time;
            p.status_2nd_leaf = p.status_2nd_leaf == null ? "" : p.status_2nd_leaf;
            p.counter_name = p.counter_name == null ? "" : p.counter_name;

            p.doc_scan_id = long.TryParse(p.doc_scan_id, out chk) ? chk.ToString() : "0";
            p.pre_no = long.TryParse(p.pre_no, out chk) ? chk.ToString() : "0";
        }

        public String insertMedicalCertificate(MedicalCertificate p, String userId)
        {
            String re = "";
            if (p.certi_id.Equals(""))
            {
                voidCertByHn(p.hn, p.pre_no, p.visit_date, p.status_2nd_leaf);
                re = insert(p, "");
            }
            else
            {
                //re = update(p, "");
            }
            return re;
        }
        public String insert(MedicalCertificate p, String userId)
        {
            String re = "";
            String sql = "";
            p.active = "1";
            //p.ssdata_id = "";
            int chk = 0;
            chkNull(p);
            sql = "Insert Into " + mcerti.table + " (" + mcerti.hn + "," + mcerti.active + "," + mcerti.line1 + "" +
                " ," + mcerti.line2 + "," + mcerti.line3 + "," + mcerti.line4 + "" +
                " ," + mcerti.ptt_name_e + "," + mcerti.ptt_name_t + "," + mcerti.dtr_code + "" +
                " ," + mcerti.dtr_name_e + "," + mcerti.dtr_name_t + "," + mcerti.status_ipd + "" +
                " ," + mcerti.date_create + "," + mcerti.visit_date + "," + mcerti.visit_time + "" +
                " ," + mcerti.pre_no + "," + mcerti.certi_code + "," + mcerti.doc_scan_id + " " +
                " ," + mcerti.user_create + ","+ mcerti.status_2nd_leaf + "," + mcerti.an + "," + mcerti.counter_name + ") " +
                "Values ('" + p.hn.Replace("'", "''") + "','1','" + p.line1 + "'" +
                ",'" + p.line2.Replace("'", "''") + "','" + p.line3.Replace("'", "''") + "','" + p.line4 + "'" +
                ",'" + p.ptt_name_e.Replace("'", "''") + "','" + p.ptt_name_t.Replace("'", "''") + "','" + p.dtr_code + "'" +
                ",'" + p.dtr_name_e.Replace("'", "''") + "','" + p.dtr_name_t.Replace("'", "''") + "','" + p.status_ipd + "'" +
                ",convert(varchar(20),GETDATE(),23),'" + p.visit_date.Replace("'", "''") + "','" + p.visit_time + "'" +
                ",'" + p.pre_no + "','" + p.certi_code + "','" + p.doc_scan_id + "'" +
                ",'" + userId + "','"+p.status_2nd_leaf + "','" + p.an + "','" + p.counter_name + "')";
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insertScreenCapture sql " + sql);
            }
            return re;
        }
        public MedicalCertificate setMedicalCert(DataTable dt)
        {
            MedicalCertificate dgs1 = new MedicalCertificate();
            if (dt.Rows.Count > 0)
            {
                dgs1.active = dt.Rows[0][mcerti.active].ToString();
                dgs1.an = dt.Rows[0][mcerti.an].ToString();
                dgs1.certi_code = dt.Rows[0][mcerti.certi_code].ToString();
                dgs1.certi_id = dt.Rows[0][mcerti.certi_id].ToString();
                dgs1.date_create = dt.Rows[0][mcerti.date_create].ToString();
                dgs1.dtr_code = dt.Rows[0][mcerti.dtr_code].ToString();
                dgs1.dtr_name_e = dt.Rows[0][mcerti.dtr_name_e].ToString();
                dgs1.dtr_name_t = dt.Rows[0][mcerti.dtr_name_t].ToString();
                dgs1.hn = dt.Rows[0][mcerti.hn].ToString();
                dgs1.line1 = dt.Rows[0][mcerti.line1].ToString();
                dgs1.line2 = dt.Rows[0][mcerti.line2].ToString();
                dgs1.line3 = dt.Rows[0][mcerti.line3].ToString();
                dgs1.line4 = dt.Rows[0][mcerti.line4].ToString();
                dgs1.pre_no = dt.Rows[0][mcerti.pre_no].ToString();
                dgs1.ptt_name_e = dt.Rows[0][mcerti.ptt_name_e].ToString();
                dgs1.ptt_name_t = dt.Rows[0][mcerti.ptt_name_t].ToString();
                dgs1.remark = dt.Rows[0][mcerti.remark].ToString();
                dgs1.status_ipd = dt.Rows[0][mcerti.status_ipd].ToString();
                dgs1.user_create = dt.Rows[0][mcerti.user_create].ToString();
                dgs1.visit_date = dt.Rows[0][mcerti.visit_date].ToString();
                dgs1.visit_time = dt.Rows[0][mcerti.visit_time].ToString();
                dgs1.doc_scan_id = dt.Rows[0][mcerti.doc_scan_id].ToString();
                dgs1.status_2nd_leaf = dt.Rows[0][mcerti.status_2nd_leaf].ToString();
                dgs1.counter_name = dt.Rows[0][mcerti.counter_name].ToString();
            }
            else
            {
                setMedicalCert(dgs1);
            }
            return dgs1;
        }
        public MedicalCertificate setMedicalCert(MedicalCertificate dgs1)
        {
            dgs1.active = "";
            dgs1.an = "";
            dgs1.certi_code = "";
            dgs1.certi_id = "";
            dgs1.date_create = "";
            dgs1.dtr_code = "";
            dgs1.dtr_name_e = "";
            dgs1.dtr_name_t = "";
            dgs1.hn = "";
            dgs1.line1 = "";
            dgs1.line2 = "";
            dgs1.line3 = "";
            dgs1.line4 = "";
            dgs1.pre_no = "";
            dgs1.ptt_name_e = "";
            dgs1.ptt_name_t = "";
            dgs1.remark = "";
            dgs1.status_ipd = "";
            dgs1.user_create = "";
            dgs1.visit_date = "";
            dgs1.visit_time = "";
            dgs1.doc_scan_id = "";
            dgs1.status_2nd_leaf = "";
            dgs1.counter_name = "";
            return dgs1;
        }
        public String updateDocScanIdRemarkScreencaptureByPk(String id, String dscid)
        {
            String sql = "", re = "";
            sql = "update " + mcerti.table + " set " +
                "doc_scan_id = '" + dscid + "' "
                + ", remark = 'from screen capture', status_scan_upload = '1' "
                + "Where certi_id = '" + id + "' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateDocScanIdRemarkScreencaptureByPk sql " + sql + " id " + id);
            }
            return re;
        }
    }
}
