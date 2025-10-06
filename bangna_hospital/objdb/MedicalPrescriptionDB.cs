using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    internal class MedicalPrescriptionDB
    {
        ConnectDB conn;
        MedicalPrescription mp;
        public MedicalPrescriptionDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            mp = new MedicalPrescription();
            mp.prescription_id = "prescription_id";
            mp.doc_scan_id = "doc_scan_id";
            mp.dtr_code = "dtr_code";
            mp.dtr_name_t = "dtr_name_t";
            mp.ptt_name_t = "ptt_name_t";
            mp.visit_date = "visit_date";
            mp.hn = "hn";
            mp.pre_no = "pre_no";
            mp.status_ipd = "status_ipd";
            mp.an = "an";
            mp.status_scan_upload = "status_scan_upload";
            mp.counter_name = "counter_name";
            mp.active = "active";
            mp.date_create = "date_create";
            mp.date_modi = "date_modi";
            mp.date_cancel = "date_cancel";
            mp.user_create = "user_create";
            mp.user_modi = "user_modi";
            mp.user_cancel = "user_cancel";
            mp.table = "t_medical_prescription";
            mp.pkField = "prescription_id";
        }
        public MedicalPrescription selectByHn(String hn, String vsdate, String preno)
        {
            MedicalPrescription p = new MedicalPrescription();
            DataTable dt = new DataTable();
            String sql = "select mp.* " +
                "From " + mp.table + " mp " +
                "Where mp." + mp.hn + "='" + hn + "' and mp." + mp.visit_date + "='" + vsdate + "' and mp." + mp.pre_no + "='" + preno + "' and mp."+mp.active+"='1' " +
                "Order By mp.visit_date DESC ";
            dt = conn.selectData(conn.conn,sql);
            if (dt.Rows.Count > 0)
            {
                setMedicalPrescription(p, dt);
            }
            else
            {
                setMedicalPrescription(p);
            }

            return p;
        }
        public DataTable selectByAn(String an)
        {
            DataTable dt = new DataTable();
            String sql = "Select mp.* " +
                "From " + mp.table + " mp " +
                "Where mp." + mp.an + "='" + an + "' and mp."+mp.active+"='1' " +
                "Order By mp.visit_date DESC ";
            dt = conn.selectData(conn.conn, sql);
            return dt;
        }
        public DataTable selectByPk(String pk)
        {
            DataTable dt = new DataTable();
            String sql = "Select mp.* " +
                "From " + mp.table + " mp " +
                "Where mp." + mp.pkField + "='" + pk + "' ";
            dt = conn.selectData(conn.conn, sql);
            return dt;
        }
        public void chkNull(MedicalPrescription p)
        {
            if (p.prescription_id == null) p.prescription_id = "";
            if (p.doc_scan_id == null) p.doc_scan_id = "";
            if (p.dtr_code == null) p.dtr_code = "";
            if (p.dtr_name_t == null) p.dtr_name_t = "";
            if (p.ptt_name_t == null) p.ptt_name_t = "";
            if (p.visit_date == null) p.visit_date = "";
            if (p.hn == null) p.hn = "";
            if (p.pre_no == null) p.pre_no = "";
            if (p.status_ipd == null) p.status_ipd = "";
            if (p.an == null) p.an = "";
            if (p.status_scan_upload == null) p.status_scan_upload = "";
            if (p.counter_name == null) p.counter_name = "";
        }
        public String insert(MedicalPrescription p)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Insert Into " + mp.table + " (" + mp.doc_scan_id + "," +         mp.dtr_code + "," +         mp.dtr_name_t + "," +
                    mp.ptt_name_t + "," +           mp.visit_date + "," +           mp.hn + "," +
                    mp.pre_no + "," +               mp.status_ipd + "," +           mp.an + "," +
                    mp.status_scan_upload + "," +                    mp.counter_name+","+mp.active + "," + mp.date_create + ") " +
                    "Values ('" + p.doc_scan_id + "','" +           p.dtr_code + "','" +        p.dtr_name_t.Replace("'","''") + "','" +
                    p.ptt_name_t.Replace("'","''") + "','" +        p.visit_date + "','" +      p.hn + "','" +
                    p.pre_no + "','" +                              p.status_ipd + "','" +      p.an + "','" +
                    p.status_scan_upload + "','" +                    p.counter_name +"','"+p.active+ "',convert(varchar(20),getdate(),121))";
                chk = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "MedicalPrescriptionDB insert error " + ex.Message);
            }
            return chk;
        }
        public String update(MedicalPrescription p)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update " + mp.table + " Set " +
                    mp.doc_scan_id + "='" + p.doc_scan_id + "'," +
                    mp.dtr_code + "='" + p.dtr_code + "'," +
                    mp.dtr_name_t + "='" + p.dtr_name_t + "'," +
                    mp.ptt_name_t + "='" + p.ptt_name_t + "'," +
                    mp.visit_date + "='" + p.visit_date + "'," +
                    mp.hn + "='" + p.hn + "'," +
                    mp.pre_no + "='" + p.pre_no + "'," +
                    mp.status_ipd + "='" + p.status_ipd + "'," +
                    mp.an + "='" + p.an + "'," +
                    mp.status_scan_upload + "='" + p.status_scan_upload + "'," +
                    mp.counter_name + "='" + p.counter_name + "' " +
                    "Where " + mp.pkField + "='" + p.prescription_id + "'";
                chk = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "MedicalPrescriptionDB update error " + ex.Message);
            }
            return chk;
        }
        public String insertMedicalPrescription(MedicalPrescription p)
        {
            chkNull(p);
            if (p.prescription_id.Length<=0)
            {
                return insert(p);
            }
            else
            {
                return update(p);
            }
        }
        public String voidMedicalPrescription(String id)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update " + mp.table + " Set " +
                    mp.active + "='3' " +
                    "Where " + mp.pkField + "='" + id + "'";
                chk = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "MedicalPrescriptionDB voidMedicalPrescription error " + ex.Message);
            }
            return chk;
        }
        private void setMedicalPrescription(MedicalPrescription p, DataTable dt)
        {
            p.prescription_id = dt.Rows[0][mp.prescription_id].ToString();
            p.doc_scan_id = dt.Rows[0][mp.doc_scan_id].ToString();
            p.dtr_code = dt.Rows[0][mp.dtr_code].ToString();
            p.dtr_name_t = dt.Rows[0][mp.dtr_name_t].ToString();
            p.ptt_name_t = dt.Rows[0][mp.ptt_name_t].ToString();
            p.visit_date = dt.Rows[0][mp.visit_date].ToString();
            p.hn = dt.Rows[0][mp.hn].ToString();
            p.pre_no = dt.Rows[0][mp.pre_no].ToString();
            p.status_ipd = dt.Rows[0][mp.status_ipd].ToString();
            p.an = dt.Rows[0][mp.an].ToString();
            p.status_scan_upload = dt.Rows[0][mp.status_scan_upload].ToString();
            p.counter_name = dt.Rows[0][mp.counter_name].ToString();
        }
        private void setMedicalPrescription(MedicalPrescription p)
        {
            p.prescription_id = "";
            p.doc_scan_id = "";
            p.dtr_code = "";
            p.dtr_name_t = "";
            p.ptt_name_t = "";
            p.visit_date = "";
            p.hn = "";
            p.pre_no = "";
            p.status_ipd = "";
            p.an = "";
            p.status_scan_upload = "";
            p.counter_name = "";
        }
    }
}
