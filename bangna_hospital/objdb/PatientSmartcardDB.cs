using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PatientSmartcardDB
    {
        public PatientSmartcard pttsc;
        ConnectDB conn;

        public PatientSmartcardDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pttsc = new PatientSmartcard();
            pttsc.patient_smartcard_id = "patient_smartcard_id";
            pttsc.prefixname = "prefixname";
            pttsc.first_name = "first_name";
            pttsc.middle_name = "middle_name";
            pttsc.last_name = "last_name";
            pttsc.first_name_e = "first_name_e";
            pttsc.middle_name_e = "middle_name_e";
            pttsc.last_name_e = "last_name_e";
            pttsc.pid = "pid";
            pttsc.dob = "dob";
            pttsc.home_no = "home_no";
            pttsc.moo = "moo";
            pttsc.trok = "trok";
            pttsc.soi = "soi";
            pttsc.road = "road";
            pttsc.district_name = "district_name";
            pttsc.amphur_name = "amphur_name";
            pttsc.province_name = "province_name";
            pttsc.date_order = "date_order";
            pttsc.status_send = "status_send";
            pttsc.doc = "doc";
            pttsc.hn = "hn";
            pttsc.hn_year = "hn_year";
            pttsc.mobile = "mobile";
            pttsc.prefixname_e = "prefixname_e";
            pttsc.attach_note = "attach_note";
            pttsc.MNC_OCC_CD = "MNC_OCC_CD";
            pttsc.MNC_EDU_CD = "MNC_EDU_CD";
            pttsc.MNC_NAT_CD = "MNC_NAT_CD";
            pttsc.MNC_REL_CD = "MNC_REL_CD";
            pttsc.MNC_NATI_CD = "MNC_NATI_CD";
            pttsc.MNC_CAR_CD = "MNC_CAR_CD";
            pttsc.sex = "sex";
            pttsc.req_no = "req_no";
            pttsc.req_date = "req_date";
            pttsc.pre_no = "pre_no";
            pttsc.visit_date = "visit_date";

            pttsc.table = "b_patient_smartcard";
            pttsc.pkField = "patient_smartcard_id";
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select * " +
                "From b_patient_smartcard  " +
                //"Where MNC_FN_TYP_STS = 'Y' " +
                "Order By patient_smartcard_id desc";
            dt = conn.selectData(conn.conn, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);MNC_DEF_DSC
            return dt;
        }
        public DataTable SelectByDoc(String doc)
        {
            DataTable dt = new DataTable();

            String sql = "select * " +
                "From b_patient_smartcard  " +
                "Where doc = '"+ doc + "' " +
                "Order By patient_smartcard_id desc";
            dt = conn.selectData(conn.conn,sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);MNC_DEF_DSC
            return dt;
        }
        private void chkNull(PatientSmartcard p)
        {
            long chk = 0;
            int chk1 = 0;

            p.date_modi = p.date_modi == null ? "" : p.date_modi;
            p.date_cancel = p.date_cancel == null ? "" : p.date_cancel;
            p.user_create = p.user_create == null ? "" : p.user_create;
            p.user_modi = p.user_modi == null ? "" : p.user_modi;
            p.user_cancel = p.user_cancel == null ? "" : p.user_cancel;

            p.prefixname = p.prefixname == null ? "" : p.prefixname;
            p.first_name = p.first_name == null ? "" : p.first_name;
            p.middle_name = p.middle_name == null ? "" : p.middle_name;
            p.last_name = p.last_name == null ? "" : p.last_name;
            p.first_name_e = p.first_name_e == null ? "" : p.first_name_e;
            p.middle_name_e = p.middle_name_e == null ? "" : p.middle_name_e;
            p.last_name_e = p.last_name_e == null ? "" : p.last_name_e;
            p.pid = p.pid == null ? "" : p.pid;
            p.dob = p.dob == null ? "" : p.dob;
            p.home_no = p.home_no == null ? "" : p.home_no;
            p.moo = p.moo == null ? "" : p.moo;
            p.trok = p.trok == null ? "" : p.trok;
            p.soi = p.soi == null ? "" : p.soi;
            p.road = p.road == null ? "" : p.road;
            p.district_name = p.district_name == null ? "" : p.district_name;
            p.amphur_name = p.amphur_name == null ? "" : p.amphur_name;
            p.province_name = p.province_name == null ? "" : p.province_name;
            p.date_order = p.date_order == null ? "" : p.date_order;
            p.status_send = p.status_send == null ? "" : p.status_send;
            p.doc = p.doc == null ? "" : p.doc;
            p.MNC_OCC_CD = p.MNC_OCC_CD == null ? "" : p.MNC_OCC_CD;
            p.MNC_EDU_CD = p.MNC_EDU_CD == null ? "" : p.MNC_EDU_CD;
            p.MNC_NAT_CD = p.MNC_NAT_CD == null ? "" : p.MNC_NAT_CD;
            p.MNC_REL_CD = p.MNC_REL_CD == null ? "" : p.MNC_REL_CD;
            p.MNC_NATI_CD = p.MNC_NATI_CD == null ? "" : p.MNC_NATI_CD;
            p.MNC_CAR_CD = p.MNC_CAR_CD == null ? "" : p.MNC_CAR_CD;

            p.sex = p.sex == null ? "" : p.sex;
            p.req_no = p.req_no == null ? "" : p.req_no;
            p.req_date = p.req_date == null ? "" : p.req_date;
            p.pre_no = p.pre_no == null ? "" : p.pre_no;
            p.visit_date = p.visit_date == null ? "" : p.visit_date;

            p.hn = p.hn == null ? "" : p.hn;
            p.hn_year = p.hn_year == null ? "" : p.hn_year;
            p.mobile = p.mobile == null ? "" : p.mobile;
            p.prefixname_e = p.prefixname_e == null ? "" : p.prefixname_e;
            p.attach_note = p.attach_note == null ? "" : p.attach_note;

            //p.doc_group_sub_id = long.TryParse(p.doc_group_sub_id, out chk) ? chk.ToString() : "0";
            //p.pre_no = long.TryParse(p.pre_no, out chk) ? chk.ToString() : "0";
        }
        public String insert(PatientSmartcard p, String userId)
        {
            String re = "";
            String sql = "";
            p.active = "1";
            //p.ssdata_id = "";
            int chk = 0;
            chkNull(p);
            sql = "Insert Into " + pttsc.table + " (" + pttsc.prefixname + "," + pttsc.first_name + "," + pttsc.middle_name + "," + pttsc.last_name + "" +
                "," + pttsc.first_name_e + "," + pttsc.middle_name_e + "," + pttsc.last_name_e + "," + pttsc.pid + "" +
                "," + pttsc.dob + "," + pttsc.home_no + "," + pttsc.moo + "," + pttsc.trok + "" +
                "," + pttsc.soi + "," + pttsc.road + "," + pttsc.district_name + "," + pttsc.amphur_name + "" +
                "," + pttsc.province_name + "," + pttsc.date_order + "," + pttsc.status_send + "," + pttsc.doc + "" +
                "," + pttsc.hn + "," + pttsc.hn_year + "," + pttsc.mobile + "," + pttsc.prefixname_e + "" +
                "," + pttsc.MNC_OCC_CD + "," + pttsc.MNC_EDU_CD + "," + pttsc.MNC_NAT_CD + "," + pttsc.MNC_REL_CD + "" +
                "," + pttsc.MNC_NATI_CD + "" +
                "," + pttsc.attach_note + "" +
                "," + pttsc.sex + "," + pttsc.req_no + "," + pttsc.req_date + "," + pttsc.pre_no + "" +
                "," + pttsc.visit_date + "" +
                ") " +
                "Values ('" + p.prefixname.Replace("'", "''") + "','"+ p.first_name.Replace("'", "''") + "','" + p.middle_name.Replace("'", "''") + "','" + p.last_name.Replace("'", "''") + "' " +
                ",'" + p.first_name_e.Replace("'", "''") + "','" + p.middle_name_e.Replace("'", "''") + "','" + p.last_name_e.Replace("'", "''") + "','" + p.pid + "' " +
                ",'" + p.dob + "','" + p.home_no.Replace("'", "''") + "','" + p.moo.Replace("'", "''") + "','" + p.trok.Replace("'", "''") + "' " +
                ",'" + p.soi.Replace("'", "''") + "','" + p.road.Replace("'", "''") + "','" + p.district_name.Replace("'", "''") + "','" + p.amphur_name.Replace("'", "''") + "' " +
                ",'" + p.province_name.Replace("'", "''") + "','" + p.date_order + "','" + p.status_send + "','" + p.doc + "' " +
                ",'" + p.hn.Replace("'", "''") + "','" + p.hn_year + "','" + p.mobile + "','" + p.prefixname_e.Replace("'", "''") + "' " +
                ",'" + p.MNC_OCC_CD.Replace("'", "''") + "','" + p.MNC_EDU_CD + "','" + p.MNC_NAT_CD + "','" + p.MNC_REL_CD + "' " +
                ",'" + p.MNC_NATI_CD.Replace("'", "''") + "' " +
                ",'" + p.attach_note.Replace("'", "''") + "' " +
                ",'" + p.sex + "','" + p.req_no + "','" + p.req_date + "','" + p.pre_no + "' " +
                ",'" + p.visit_date + "' " +
                ")";
            try
            {
                re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                //sql = ex.Message + " " + ex.InnerException;
                new LogWriter("d", "PatientSmartcardDB insert ex.InnerException " + ex.InnerException+"\n"+sql);
            }

            return re;
        }
        public String update(PatientSmartcard p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            chkNull(p);
            sql = "Update " + pttsc.table + " Set " +
                " " + pttsc.prefixname + " = '" + p.prefixname.Replace("'", "''") + "'" +
                ", " + pttsc.first_name + " = '" + p.first_name.Replace("'", "''") + "'" +
                ", " + pttsc.middle_name + " = '" + p.middle_name + "'" +
                ", " + pttsc.last_name + " = '" + p.last_name + "'" +
                ", " + pttsc.first_name_e + " = '" + p.first_name_e + "'" +
                ", " + pttsc.middle_name_e + " = '" + p.middle_name_e + "'" +
                ", " + pttsc.last_name_e + " = '" + p.last_name_e + "'" +
                ", " + pttsc.pid + " = '" + p.pid + "'" +
                ", " + pttsc.dob + " = '" + p.dob + "'" +
                ", " + pttsc.home_no + " = '" + p.home_no + "'" +
                ", " + pttsc.moo + " = '" + p.moo + "'" +
                ", " + pttsc.trok + " = '" + p.trok + "'" +
                ", " + pttsc.soi + " = '" + p.soi + "'" +
                ", " + pttsc.road + " = '" + p.road + "'" +
                ", " + pttsc.district_name + " = '" + p.district_name + "'" +
                ", " + pttsc.amphur_name + " = '" + p.amphur_name + "'" +
                ", " + pttsc.province_name + " = '" + p.province_name + "'" +
                ", " + pttsc.date_order + " = '" + p.date_order + "'" +
                ", " + pttsc.status_send + " = '" + p.status_send + "'" +
                ", " + pttsc.doc + " = '" + p.doc + "'" +
                "Where " + pttsc.pkField + "='" + p.patient_smartcard_id + "'"
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
        public String delete(String pscid)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            
            sql = "Delete from " + pttsc.table + "  " +
                "Where " + pttsc.pkField + "='" + pscid + "'"
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
        public String deleteAll()
        {
            String re = "";
            String sql = "";
            int chk = 0;

            sql = "Delete from " + pttsc.table + "  "
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
        public String insertPatientSmartcard(PatientSmartcard p, String userId)
        {
            String re = "";

            if (p.patient_smartcard_id.Equals(""))
            {
                re = insert(p, "");
            }
            else
            {
                re = update(p, "");
            }

            return re;
        }
        public PatientSmartcard setLabM01(DataTable dt)
        {
            PatientSmartcard labM01 = new PatientSmartcard();
            if (dt.Rows.Count > 0)
            {
                labM01.patient_smartcard_id = dt.Rows[0]["patient_smartcard_id"].ToString();
                labM01.prefixname = dt.Rows[0]["prefixname"].ToString();
                labM01.first_name = dt.Rows[0]["first_name"].ToString();
                labM01.middle_name = dt.Rows[0]["middle_name"].ToString();
                labM01.last_name = dt.Rows[0]["last_name"].ToString();
                labM01.first_name_e = dt.Rows[0]["first_name_e"].ToString();
                labM01.middle_name_e = dt.Rows[0]["middle_name_e"].ToString();
                labM01.last_name_e = dt.Rows[0]["last_name_e"].ToString();
                labM01.pid = dt.Rows[0]["pid"].ToString();
                labM01.dob = dt.Rows[0]["dob"].ToString();
                labM01.home_no = dt.Rows[0]["home_no"].ToString();
                labM01.moo = dt.Rows[0]["moo"].ToString();
                labM01.trok = dt.Rows[0]["trok"].ToString();
                labM01.soi = dt.Rows[0]["soi"].ToString();
                labM01.road = dt.Rows[0]["road"].ToString();
                labM01.district_name = dt.Rows[0]["district_name"].ToString();
                labM01.amphur_name = dt.Rows[0]["amphur_name"].ToString();
                labM01.province_name = dt.Rows[0]["province_name"].ToString();
                labM01.date_order = dt.Rows[0]["date_order"].ToString();
                labM01.status_send = dt.Rows[0]["status_send"].ToString();
                labM01.doc = dt.Rows[0]["doc"].ToString();
                labM01.hn = dt.Rows[0]["hn"].ToString();
                labM01.hn_year = dt.Rows[0]["hn_year"].ToString();
                labM01.mobile = dt.Rows[0]["mobile"].ToString();
                labM01.prefixname_e = dt.Rows[0]["prefixname_e"].ToString();
                labM01.attach_note = dt.Rows[0]["attach_note"].ToString();

            }
            else
            {
                setLabM01(labM01);
            }
            return labM01;
        }

        public PatientSmartcard setLabM01(PatientSmartcard p)
        {
            p.patient_smartcard_id = "";
            p.prefixname = "";
            p.first_name = "";
            p.middle_name = "";
            p.last_name = "";
            p.first_name_e = "";
            p.middle_name_e = "";
            p.last_name_e = "";
            p.pid = "";
            p.dob = "";
            p.home_no = "";
            p.moo = "";
            p.trok = "";
            p.soi = "";
            p.road = "";
            p.district_name = "";
            p.amphur_name = "";
            p.province_name = "";
            p.date_order = "";
            p.status_send = "";
            p.doc = "";
            p.hn = "";
            p.hn_year = "";
            p.mobile = "";
            p.prefixname_e = "";
            p.attach_note = "";
            return p;
        }
    }
}
