using bangna_hospital.object1;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class OperativeNoteDB
    {
        public OperativeNote operNote;
        ConnectDB conn;
        public OperativeNoteDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            operNote = new OperativeNote();
            operNote.operative_note_id = "operative_note_id";
            operNote.dept_id = "dept_id";
            operNote.dept_name = "dept_name";
            operNote.ward_id = "ward_id";
            operNote.ward_name = "ward_name";
            operNote.attending_stf_id = "attending_stf_id";
            operNote.attending_stf_name = "attending_stf_name";
            operNote.patient_hn = "patient_hn";
            operNote.patient_fullname = "patient_fullname";
            operNote.age = "age";
            operNote.hn = "hn";
            operNote.an = "an";
            operNote.mnc_date = "mnc_date";
            operNote.pre_no = "pre_no";
            operNote.doc_scan_id = "doc_scan_id";
            operNote.date_operation = "date_operation";
            operNote.time_start = "time_start";
            operNote.time_finish = "time_finish";
            operNote.total_time = "total_time";
            operNote.surgeon_id_1 = "surgeon_id_1";
            operNote.surgeon_name_1 = "surgeon_name_1";
            operNote.surgeon_id_2 = "surgeon_id_2";
            operNote.surgeon_name_2 = "surgeon_name_2";
            operNote.surgeon_id_3 = "surgeon_id_3";
            operNote.surgeon_name_3 = "surgeon_name_3";
            operNote.surgeon_id_4 = "surgeon_id_4";
            operNote.surgeon_name_4 = "surgeon_name_4";
            operNote.assistant_id_1 = "assistant_id_1";
            operNote.assistant_name_1 = "assistant_name_1";
            operNote.assistant_id_2 = "assistant_id_2";
            operNote.assistant_name_2 = "assistant_name_2";
            operNote.assistant_id_3 = "assistant_id_3";
            operNote.assistant_name_3 = "assistant_name_3";
            operNote.assistant_id_4 = "assistant_id_4";
            operNote.assistant_name_4 = "assistant_name_4";
            operNote.scrub_nurse_id_1 = "scrub_nurse_id_1";
            operNote.scrub_nurse_name_1 = "scrub_nurse_name_1";
            operNote.scrub_nurse_id_2 = "scrub_nurse_id_2";
            operNote.scrub_nurse_name_2 = "scrub_nurse_name_2";
            operNote.scrub_nurse_id_3 = "scrub_nurse_id_3";
            operNote.scrub_nurse_name_3 = "scrub_nurse_name_3";
            operNote.scrub_nurse_id_4 = "scrub_nurse_id_4";
            operNote.scrub_nurse_name_4 = "scrub_nurse_name_4";
            operNote.circulation_nurse_id_1 = "circulation_nurse_id_1";
            operNote.circulation_nurse_name_1 = "circulation_nurse_name_1";
            operNote.circulation_nurse_id_2 = "circulation_nurse_id_2";
            operNote.circulation_nurse_name_2 = "circulation_nurse_name_2";
            operNote.circulation_nurse_id_3 = "circulation_nurse_id_3";
            operNote.circulation_nurse_name_3 = "circulation_nurse_name_3";
            operNote.circulation_nurse_id_4 = "circulation_nurse_id_4";
            operNote.circulation_nurse_name_4 = "circulation_nurse_name_4";
            operNote.perfusionist_id_1 = "perfusionist_id_1";
            operNote.perfusionist_name_1 = "perfusionist_name_1";
            operNote.perfusionist_id_2 = "perfusionist_id_2";
            operNote.perfusionist_name_2 = "perfusionist_name_2";
            operNote.perfusionist_id_3 = "perfusionist_id_3";
            operNote.perfusionist_name_3 = "perfusionist_name_3";
            operNote.perfusionist_id_4 = "perfusionist_id_4";
            operNote.perfusionist_name_4 = "perfusionist_name_4";
            operNote.anesthetist_id_1 = "anesthetist_id_1";
            operNote.anesthetist_name_1 = "anesthetist_name_1";
            operNote.anesthetist_id_2 = "anesthetist_id_2";
            operNote.anesthetist_name_2 = "anesthetist_name_2";
            operNote.anesthetist_assistant_id_1 = "anesthetist_assistant_id_1";
            operNote.anesthetist_assistant_name_1 = "anesthetist_assistant_name_1";
            operNote.anesthetist_assistant_id_2 = "anesthetist_assistant_id_2";
            operNote.anesthetist_assistant_name_2 = "anesthetist_assistant_name_2";
            operNote.anesthesia_techique_id_1 = "anesthesia_techique_id_1";
            operNote.anesthesia_techique_name_1 = "anesthesia_techique_name_1";
            operNote.anesthesia_techique_id_2 = "anesthesia_techique_id_2";
            operNote.anesthesia_techique_name_2 = "anesthesia_techique_name_2";
            operNote.time_start_anesthesia_techique_1 = "time_start_anesthesia_techique_1";
            operNote.time_finish_anesthesia_techique_1 = "time_finish_anesthesia_techique_1";
            operNote.time_start_anesthesia_techique_2 = "time_start_anesthesia_techique_2";
            operNote.time_finish_anesthesia_techique_2 = "time_finish_anesthesia_techique_2";
            operNote.total_time_anesthesia_1 = "total_time_anesthesia_1";
            operNote.total_time_anesthesia_2 = "total_time_anesthesia_2";
            operNote.pre_operatation_diagnosis = "pre_operatation_diagnosis";
            operNote.post_operation_diagnosis = "post_operation_diagnosis";
            operNote.operation_1 = "operation_1";
            operNote.operation_2 = "operation_2";
            operNote.operation_3 = "operation_3";
            operNote.operation_4 = "operation_4";
            operNote.finding_1 = "finding_1";
            operNote.finding_2 = "finding_2";
            operNote.procidures_1 = "procidures_1";
            operNote.procidures_2 = "procidures_2";
            operNote.complication = "complication";
            operNote.estimated_blood_loss = "estimated_blood_loss";
            operNote.tissue_biopsy = "tissue_biopsy";
            operNote.tissue_biopsy_unit = "tissue_biopsy_unit";
            operNote.special_specimen = "special_specimen";
            operNote.date_modi = "date_modi";
            operNote.complication_other = "complication_other";

            operNote.table = "t_operative_note";
            operNote.pkField = "operative_note_id";
        }
        public OperativeNote selectByPk(String opernoteid)
        {
            OperativeNote oper = new OperativeNote();
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select * " +
                "From  t_operative_note " +
                " " +
                " Where "+operNote.operative_note_id+" = '" + opernoteid + "' ";
            dt = conn.selectData(conn.conn, sql);
            oper = setOperativeNote(dt);
            return oper;
        }
        public DataTable selectByHn(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select opernote.operative_note_id,opernote.dept_name,opernote.ward_name,convert(VARCHAR(20),opernote.date_operation,23) as date_operation,opernote.attending_stf_name,opernote.time_start" +
                ",opernote.patient_fullname,opernote.patient_hn,opernote.an,convert(VARCHAR(20),opernote.mnc_date,23) as mnc_date,opernote.pre_no " +
                "From  t_operative_note opernote " +
                " " +
                " Where opernote." + operNote.patient_hn + " like '" + hn + "%' " +
                "Order By opernote.date_operation ";
            dt = conn.selectData(conn.conn, sql);
            
            return dt;
        }
        private void chkNull(OperativeNote p)
        {
            long chk = 0;
            int chk1 = 0;

            p.date_modi = p.date_modi == null ? "" : p.date_modi;
            p.date_cancel = p.date_cancel == null ? "" : p.date_cancel;
            p.user_create = p.user_create == null ? "" : p.user_create;
            p.user_modi = p.user_modi == null ? "" : p.user_modi;
            p.user_cancel = p.user_cancel == null ? "" : p.user_cancel;

            p.dept_name = p.dept_name == null ? "" : p.dept_name;
            p.dept_id = p.dept_id == null ? "" : p.dept_id;
            p.patient_fullname = p.patient_fullname == null ? "" : p.patient_fullname;
            p.hn = p.hn == null ? "" : p.hn;
            p.an = p.an == null ? "" : p.an;
            p.ward_id = p.ward_id == null ? "" : p.ward_id;
            p.ward_name = p.ward_name == null ? "" : p.ward_name;
            p.attending_stf_id = p.attending_stf_id.Length == 0 ? "" : p.attending_stf_id;
            p.attending_stf_name = p.attending_stf_name == null ? "" : p.attending_stf_name;
            p.patient_hn = p.patient_hn == null ? "" : p.patient_hn;
            p.patient_fullname = p.patient_fullname == null ? "" : p.patient_fullname;
            p.age = p.age == null ? "" : p.age;
            p.mnc_date = p.mnc_date == null ? "" : p.mnc_date;
            p.date_operation = p.date_operation == null ? "" : p.date_operation;
            p.time_start = p.time_start == null ? "" : p.time_start;
            p.time_finish = p.time_finish == null ? "" : p.time_finish;
            p.total_time = p.total_time == null ? "" : p.total_time;
            p.surgeon_id_1 = p.surgeon_id_1 == null ? "" : p.surgeon_id_1;
            p.surgeon_name_1 = p.surgeon_name_1 == null ? "" : p.surgeon_name_1;
            p.surgeon_id_2 = p.surgeon_id_2 == null ? "" : p.surgeon_id_2;
            p.surgeon_name_2 = p.surgeon_name_2 == null ? "" : p.surgeon_name_2;
            p.surgeon_id_3 = p.surgeon_id_3 == null ? "" : p.surgeon_id_3;
            p.surgeon_name_3 = p.surgeon_name_3 == null ? "" : p.surgeon_name_3;
            p.surgeon_id_4 = p.surgeon_id_4 == null ? "" : p.surgeon_id_4;
            p.surgeon_name_4 = p.surgeon_name_4 == null ? "" : p.surgeon_name_4;
            p.assistant_id_1 = p.assistant_id_1 == null ? "" : p.assistant_id_1;
            p.assistant_name_1 = p.assistant_name_1 == null ? "" : p.assistant_name_1;
            p.assistant_id_2 = p.assistant_id_2 == null ? "" : p.assistant_id_2;
            p.assistant_name_2 = p.assistant_name_2 == null ? "" : p.assistant_name_2;
            p.assistant_id_3 = p.assistant_id_3 == null ? "" : p.assistant_id_3;
            p.assistant_id_4 = p.assistant_id_4 == null ? "" : p.assistant_id_4;
            p.assistant_name_4 = p.assistant_name_4 == null ? "" : p.assistant_name_4;
            p.scrub_nurse_id_1 = p.scrub_nurse_id_1 == null ? "" : p.scrub_nurse_id_1;
            p.scrub_nurse_name_1 = p.scrub_nurse_name_1 == null ? "" : p.scrub_nurse_name_1;
            p.scrub_nurse_id_2 = p.scrub_nurse_id_2 == null ? "" : p.scrub_nurse_id_2;
            p.scrub_nurse_name_2 = p.scrub_nurse_name_2 == null ? "" : p.scrub_nurse_name_2;
            p.scrub_nurse_id_3 = p.scrub_nurse_id_3 == null ? "" : p.scrub_nurse_id_3;
            p.scrub_nurse_name_3 = p.scrub_nurse_name_3 == null ? "" : p.scrub_nurse_name_3;
            p.scrub_nurse_id_4 = p.scrub_nurse_id_4 == null ? "" : p.scrub_nurse_id_4;
            p.scrub_nurse_name_4 = p.scrub_nurse_name_4 == null ? "" : p.scrub_nurse_name_4;
            p.circulation_nurse_id_1 = p.circulation_nurse_id_1 == null ? "" : p.circulation_nurse_id_1;
            p.circulation_nurse_name_1 = p.circulation_nurse_name_1 == null ? "" : p.circulation_nurse_name_1;
            p.circulation_nurse_id_2 = p.circulation_nurse_id_2 == null ? "" : p.circulation_nurse_id_2;
            p.circulation_nurse_name_2 = p.circulation_nurse_name_2 == null ? "" : p.circulation_nurse_name_2;
            p.circulation_nurse_id_3 = p.circulation_nurse_id_3 == null ? "" : p.circulation_nurse_id_3;
            p.circulation_nurse_name_3 = p.circulation_nurse_name_3 == null ? "" : p.circulation_nurse_name_3;
            p.circulation_nurse_id_4 = p.circulation_nurse_id_4 == null ? "" : p.circulation_nurse_id_4;
            p.circulation_nurse_name_4 = p.circulation_nurse_name_4 == null ? "" : p.circulation_nurse_name_4;
            p.perfusionist_id_1 = p.perfusionist_id_1 == null ? "" : p.perfusionist_id_1;
            p.perfusionist_name_1 = p.perfusionist_name_1 == null ? "" : p.perfusionist_name_1;
            p.perfusionist_id_2 = p.perfusionist_id_2 == null ? "" : p.perfusionist_id_2;
            p.perfusionist_name_2 = p.perfusionist_name_2 == null ? "" : p.perfusionist_name_2;
            p.perfusionist_id_3 = p.perfusionist_id_3 == null ? "" : p.perfusionist_id_3;
            p.perfusionist_name_3 = p.perfusionist_name_3 == null ? "" : p.perfusionist_name_3;
            p.perfusionist_id_4 = p.perfusionist_id_4 == null ? "" : p.perfusionist_id_4;
            p.perfusionist_name_4 = p.perfusionist_name_4 == null ? "" : p.perfusionist_name_4;
            p.anesthetist_id_1 = p.anesthetist_id_1 == null ? "" : p.anesthetist_id_1;
            p.anesthetist_name_1 = p.anesthetist_name_1 == null ? "" : p.anesthetist_name_1;
            p.anesthetist_id_2 = p.anesthetist_id_2 == null ? "" : p.anesthetist_id_2;
            p.anesthetist_name_2 = p.anesthetist_name_2 == null ? "" : p.anesthetist_name_2;
            p.anesthetist_assistant_id_1 = p.anesthetist_assistant_id_1 == null ? "" : p.anesthetist_assistant_id_1;
            p.anesthetist_assistant_name_1 = p.anesthetist_assistant_name_1 == null ? "" : p.anesthetist_assistant_name_1;
            p.anesthetist_assistant_id_2 = p.anesthetist_assistant_id_2 == null ? "" : p.anesthetist_assistant_id_2;
            p.anesthetist_assistant_name_2 = p.anesthetist_assistant_name_2 == null ? "" : p.anesthetist_assistant_name_2;
            p.anesthesia_techique_id_1 = p.anesthesia_techique_id_1 == null ? "" : p.anesthesia_techique_id_1;
            p.anesthesia_techique_name_1 = p.anesthesia_techique_name_1 == null ? "" : p.anesthesia_techique_name_1;
            p.anesthesia_techique_id_2 = p.anesthesia_techique_id_2 == null ? "" : p.anesthesia_techique_id_2;
            p.anesthesia_techique_name_2 = p.anesthesia_techique_name_2 == null ? "" : p.anesthesia_techique_name_2;
            p.time_start_anesthesia_techique_1 = p.time_start_anesthesia_techique_1 == null ? "" : p.time_start_anesthesia_techique_1;
            p.time_finish_anesthesia_techique_1 = p.time_finish_anesthesia_techique_1 == null ? "" : p.time_finish_anesthesia_techique_1;
            p.time_start_anesthesia_techique_2 = p.time_start_anesthesia_techique_2 == null ? "" : p.time_start_anesthesia_techique_2;
            p.time_finish_anesthesia_techique_2 = p.time_finish_anesthesia_techique_2 == null ? "" : p.time_finish_anesthesia_techique_2;
            p.total_time_anesthesia_1 = p.total_time_anesthesia_1 == null ? "" : p.total_time_anesthesia_1;
            p.total_time_anesthesia_2 = p.total_time_anesthesia_2 == null ? "" : p.total_time_anesthesia_2;
            p.pre_operatation_diagnosis = p.pre_operatation_diagnosis == null ? "" : p.pre_operatation_diagnosis;
            p.post_operation_diagnosis = p.post_operation_diagnosis == null ? "" : p.post_operation_diagnosis;
            p.operation_1 = p.operation_1 == null ? "" : p.operation_1;
            p.operation_2 = p.operation_2 == null ? "" : p.operation_2;
            p.operation_3 = p.operation_3 == null ? "" : p.operation_3;
            p.operation_4 = p.operation_4 == null ? "" : p.operation_4;
            p.finding_1 = p.finding_1 == null ? "" : p.finding_1;
            p.finding_2 = p.finding_2 == null ? "" : p.finding_2;
            p.procidures_1 = p.procidures_1 == null ? "" : p.procidures_1;
            p.procidures_2 = p.procidures_2 == null ? "" : p.procidures_2;
            p.complication = p.complication == null ? "" : p.complication;
            p.estimated_blood_loss = p.estimated_blood_loss == null ? "" : p.estimated_blood_loss;
            p.tissue_biopsy = p.tissue_biopsy == null ? "" : p.tissue_biopsy;
            p.tissue_biopsy_unit = p.tissue_biopsy_unit == null ? "" : p.tissue_biopsy_unit;
            p.special_specimen = p.special_specimen == null ? "" : p.special_specimen;

            p.dept_id = long.TryParse(p.dept_id, out chk) ? chk.ToString() : "0";
            p.pre_no = long.TryParse(p.pre_no, out chk) ? chk.ToString() : "0";
            p.doc_scan_id = long.TryParse(p.doc_scan_id, out chk) ? chk.ToString() : "0";
            p.finding_1 = long.TryParse(p.finding_1, out chk) ? chk.ToString() : "0";
            p.finding_2 = long.TryParse(p.finding_2, out chk) ? chk.ToString() : "0";
            p.procidures_1 = long.TryParse(p.procidures_1, out chk) ? chk.ToString() : "0";
            p.procidures_2 = long.TryParse(p.procidures_2, out chk) ? chk.ToString() : "0";
            //p.pre_no = long.TryParse(p.pre_no, out chk) ? chk.ToString() : "0";
        }
        public String insert(OperativeNote p, String userid)
        {
            String sql = "", chk = "", re = "";
            try
            {
                chkNull(p);
                //p.dateModi = " CAST(year(GETDATE()) AS NVARCHAR)+'-'+ RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)+'-'+ RIGHT('00' + CAST(day(GETDATE()) AS NVARCHAR), 2)";
                p.patient_fullname = p.patient_fullname.Replace("'", "''");
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.conn;
                conn.comStore.CommandText = "[insert_t_operative_note]";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("dept_id", p.dept_id);
                conn.comStore.Parameters.AddWithValue("dept_name", p.dept_name.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("ward_id", p.ward_id);
                conn.comStore.Parameters.AddWithValue("ward_name", p.ward_name.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("attending_stf_id", p.attending_stf_id);
                conn.comStore.Parameters.AddWithValue("attending_stf_name", p.attending_stf_name.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("patient_hn", p.patient_hn.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("patient_fullname", p.patient_fullname.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("hn", p.hn);
                conn.comStore.Parameters.AddWithValue("age", p.age);
                conn.comStore.Parameters.AddWithValue("an", p.an);
                conn.comStore.Parameters.AddWithValue("date_operation", p.date_operation);
                conn.comStore.Parameters.AddWithValue("mnc_date", p.mnc_date);
                conn.comStore.Parameters.AddWithValue("pre_no", p.pre_no);
                conn.comStore.Parameters.AddWithValue("doc_scan_id", p.doc_scan_id);
                conn.comStore.Parameters.AddWithValue("time_start", p.time_start);
                conn.comStore.Parameters.AddWithValue("time_finish", p.time_finish);
                conn.comStore.Parameters.AddWithValue("total_time", p.total_time);
                conn.comStore.Parameters.AddWithValue("surgeon_id_1", p.surgeon_id_1);
                conn.comStore.Parameters.AddWithValue("surgeon_name_1", p.surgeon_name_1.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("surgeon_id_2", p.surgeon_id_2);
                conn.comStore.Parameters.AddWithValue("surgeon_name_2", p.surgeon_name_2.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("surgeon_id_3", p.surgeon_id_3);
                conn.comStore.Parameters.AddWithValue("surgeon_name_3", p.surgeon_name_3.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("surgeon_id_4", p.surgeon_id_4);
                conn.comStore.Parameters.AddWithValue("surgeon_name_4", p.surgeon_name_4.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("assistant_id_1", p.assistant_id_1);
                conn.comStore.Parameters.AddWithValue("assistant_name_1", p.assistant_name_1.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("assistant_id_2", p.assistant_id_2);
                conn.comStore.Parameters.AddWithValue("assistant_name_2", p.assistant_name_2.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("assistant_id_3", p.assistant_id_3);
                conn.comStore.Parameters.AddWithValue("assistant_name_3", p.assistant_name_3.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("assistant_id_4", p.assistant_id_4);
                conn.comStore.Parameters.AddWithValue("assistant_name_4", p.assistant_name_4.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("scrub_nurse_id_1", p.scrub_nurse_id_1);
                conn.comStore.Parameters.AddWithValue("scrub_nurse_name_1", p.scrub_nurse_name_1.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("scrub_nurse_id_2", p.scrub_nurse_id_2);
                conn.comStore.Parameters.AddWithValue("scrub_nurse_name_2", p.scrub_nurse_name_2.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("scrub_nurse_id_3", p.scrub_nurse_id_3);
                conn.comStore.Parameters.AddWithValue("scrub_nurse_name_3", p.scrub_nurse_name_3.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("scrub_nurse_id_4", p.scrub_nurse_id_4);
                conn.comStore.Parameters.AddWithValue("scrub_nurse_name_4", p.scrub_nurse_name_4.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("circulation_nurse_id_1", p.circulation_nurse_id_1);
                conn.comStore.Parameters.AddWithValue("circulation_nurse_name_1", p.circulation_nurse_name_1.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("circulation_nurse_id_2", p.circulation_nurse_id_2);
                conn.comStore.Parameters.AddWithValue("circulation_nurse_name_2", p.circulation_nurse_name_2.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("circulation_nurse_id_3", p.circulation_nurse_id_3);
                conn.comStore.Parameters.AddWithValue("circulation_nurse_name_3", p.circulation_nurse_name_3.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("circulation_nurse_id_4", p.circulation_nurse_id_4);
                conn.comStore.Parameters.AddWithValue("circulation_nurse_name_4", p.circulation_nurse_name_4.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("perfusionist_id_1", p.perfusionist_id_1);
                conn.comStore.Parameters.AddWithValue("perfusionist_name_1", p.perfusionist_name_1.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("perfusionist_id_2", p.perfusionist_id_2);
                conn.comStore.Parameters.AddWithValue("perfusionist_name_2", p.perfusionist_name_2.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("perfusionist_id_3", p.perfusionist_id_3);
                conn.comStore.Parameters.AddWithValue("perfusionist_name_3", p.perfusionist_name_3.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("perfusionist_id_4", p.perfusionist_id_4);
                conn.comStore.Parameters.AddWithValue("perfusionist_name_4", p.perfusionist_name_4.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("anesthetist_id_1", p.anesthetist_id_1);
                conn.comStore.Parameters.AddWithValue("anesthetist_name_1", p.anesthetist_name_1.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("anesthetist_id_2", p.anesthetist_id_2);
                conn.comStore.Parameters.AddWithValue("anesthetist_name_2", p.anesthetist_name_2.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("anesthetist_assistant_id_1", p.anesthetist_assistant_id_1);
                conn.comStore.Parameters.AddWithValue("anesthetist_assistant_name_1", p.anesthetist_assistant_name_1.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("anesthetist_assistant_id_2", p.anesthetist_assistant_id_2);
                conn.comStore.Parameters.AddWithValue("anesthetist_assistant_name_2", p.anesthetist_assistant_name_2.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("anesthesia_techique_id_1", p.anesthesia_techique_id_1);
                conn.comStore.Parameters.AddWithValue("anesthesia_techique_name_1", p.anesthesia_techique_name_1.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("anesthesia_techique_id_2", p.anesthesia_techique_id_2);
                conn.comStore.Parameters.AddWithValue("anesthesia_techique_name_2", p.anesthesia_techique_name_2.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("time_start_anesthesia_techique_1", p.time_start_anesthesia_techique_1);
                conn.comStore.Parameters.AddWithValue("time_finish_anesthesia_techique_1", p.time_finish_anesthesia_techique_1.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("time_start_anesthesia_techique_2", p.time_start_anesthesia_techique_2);
                conn.comStore.Parameters.AddWithValue("time_finish_anesthesia_techique_2", p.time_finish_anesthesia_techique_2.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("total_time_anesthesia_1", p.total_time_anesthesia_1);
                conn.comStore.Parameters.AddWithValue("total_time_anesthesia_2", p.total_time_anesthesia_2.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("pre_operatation_diagnosis", p.pre_operatation_diagnosis);
                conn.comStore.Parameters.AddWithValue("post_operation_diagnosis", p.post_operation_diagnosis.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("operation_1", p.operation_1);
                conn.comStore.Parameters.AddWithValue("operation_2", p.operation_2.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("operation_3", p.operation_3);
                conn.comStore.Parameters.AddWithValue("operation_4", p.operation_4.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("finding_1", p.finding_1);
                conn.comStore.Parameters.AddWithValue("finding_2", p.finding_2.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("procidures_1", p.procidures_1);
                conn.comStore.Parameters.AddWithValue("procidures_2", p.procidures_2.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("complication", p.complication);
                conn.comStore.Parameters.AddWithValue("estimated_blood_loss", p.estimated_blood_loss.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("tissue_biopsy", p.tissue_biopsy);
                conn.comStore.Parameters.AddWithValue("tissue_biopsy_unit", p.tissue_biopsy_unit.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("special_specimen", p.special_specimen.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("user_create", userid.Replace("'", "''"));
                conn.comStore.Parameters.AddWithValue("complication_other", p.complication_other.Replace("'", "''"));
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;

                conn.conn.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;

            }
            catch (Exception ex)
            {
                new LogWriter("e", "insertOperative Note "+ex.Message+" " + sql);
            }
            finally
            {
                conn.conn.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String update(OperativeNote p)
        {
            String sql = "", chk = "";
            try
            {
                chkNull(p);
                //p.dateModi = " CAST(year(GETDATE()) AS NVARCHAR)+'-'+ RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)+'-'+ RIGHT('00' + CAST(day(GETDATE()) AS NVARCHAR), 2)";
                sql = "Update " + operNote.table + " Set " + operNote.dept_id + "='" + p.dept_id + "'," +
                    operNote.dept_name + "='" + p.dept_name + "'," +
                    operNote.ward_id + "='" + p.ward_id + "'," +
                    operNote.ward_name + "='" + p.ward_name + "'," +
                    operNote.attending_stf_id + "='" + p.attending_stf_id + "'," +
                    operNote.attending_stf_name + "='" + p.attending_stf_name + "'," +
                    operNote.patient_hn + "='" + p.patient_hn + "'," +
                    operNote.patient_fullname + "='" + p.patient_fullname + "'," +
                    operNote.age + "='" + p.age + "'," +
                    operNote.hn + "='" + p.hn + "'," +
                    operNote.an + "='" + p.an + "', " +
                    operNote.mnc_date + "='" + p.mnc_date + "', " +
                    operNote.pre_no + "='" + p.pre_no + "', " +
                    operNote.doc_scan_id + "='" + p.doc_scan_id + "', " +
                    operNote.date_operation + "='" + p.date_operation + "', " +
                    operNote.time_start + "='" + p.time_start + "', " +
                    operNote.time_finish + "='" + p.time_finish + "', " +
                    operNote.total_time + "='" + p.total_time + "', " +
                    operNote.surgeon_id_1 + "='" + p.surgeon_id_1 + "', " +
                    operNote.surgeon_name_1 + "='" + p.surgeon_name_1 + "', " +
                    operNote.surgeon_id_2 + "='" + p.surgeon_id_2 + "', " +
                    operNote.surgeon_name_2 + "='" + p.surgeon_name_2 + "', " +
                    operNote.surgeon_id_3 + "='" + p.surgeon_id_3 + "', " +
                    operNote.surgeon_name_3 + "='" + p.surgeon_name_3 + "', " +
                    operNote.surgeon_id_4 + "='" + p.surgeon_id_4 + "', " +
                    operNote.surgeon_name_4 + "='" + p.surgeon_name_4 + "', " +
                    operNote.assistant_id_1 + "='" + p.assistant_id_1 + "', " +
                    operNote.assistant_name_1 + "='" + p.assistant_name_1 + "', " +
                    operNote.assistant_id_2 + "='" + p.assistant_id_2 + "', " +
                    operNote.assistant_name_2 + "='" + p.assistant_name_2 + "', " +
                    operNote.assistant_id_3 + "='" + p.assistant_id_3 + "', " +
                    operNote.assistant_name_3 + "='" + p.assistant_name_3 + "', " +
                    operNote.assistant_id_4 + "='" + p.assistant_id_4 + "', " +
                    operNote.assistant_name_4 + "='" + p.assistant_name_4 + "', " +
                    operNote.scrub_nurse_id_1 + "='" + p.scrub_nurse_id_1 + "', " +
                    operNote.scrub_nurse_name_1 + "='" + p.scrub_nurse_name_1 + "', " +
                    operNote.scrub_nurse_id_2 + "='" + p.scrub_nurse_id_2 + "', " +
                    operNote.scrub_nurse_name_2 + "='" + p.scrub_nurse_name_2 + "', " +
                    operNote.scrub_nurse_id_3 + "='" + p.scrub_nurse_id_3 + "', " +
                    operNote.scrub_nurse_name_3 + "='" + p.scrub_nurse_name_3 + "', " +
                    operNote.scrub_nurse_id_4 + "='" + p.scrub_nurse_id_4 + "', " +
                    operNote.scrub_nurse_name_4 + "='" + p.scrub_nurse_name_4 + "', " +
                    operNote.circulation_nurse_id_1 + "='" + p.circulation_nurse_id_1 + "', " +
                    operNote.circulation_nurse_name_1 + "='" + p.circulation_nurse_name_1 + "', " +
                    operNote.circulation_nurse_id_2 + "='" + p.circulation_nurse_id_2 + "', " +
                    operNote.circulation_nurse_name_2 + "='" + p.circulation_nurse_name_2 + "', " +
                    operNote.circulation_nurse_id_3 + "='" + p.circulation_nurse_id_3 + "', " +
                    operNote.circulation_nurse_name_3 + "='" + p.circulation_nurse_name_3 + "', " +
                    operNote.circulation_nurse_id_4 + "='" + p.circulation_nurse_id_4 + "', " +
                    operNote.circulation_nurse_name_4 + "='" + p.circulation_nurse_name_4 + "', " +
                    operNote.perfusionist_id_1 + "='" + p.perfusionist_id_1 + "', " +
                    operNote.perfusionist_name_1 + "='" + p.perfusionist_name_1 + "', " +
                    operNote.perfusionist_id_2 + "='" + p.perfusionist_id_2 + "', " +
                    operNote.perfusionist_name_2 + "='" + p.perfusionist_name_2 + "', " +
                    operNote.perfusionist_id_3 + "='" + p.perfusionist_id_3 + "', " +
                    operNote.perfusionist_name_3 + "='" + p.perfusionist_name_3 + "'," +
                    operNote.perfusionist_id_4 + "='" + p.perfusionist_id_4 + "', " +
                    operNote.perfusionist_name_4 + "='" + p.perfusionist_name_4 + "', " +
                    operNote.anesthetist_id_1 + "='" + p.anesthetist_id_1 + "', " +
                    operNote.anesthetist_name_1 + "='" + p.anesthetist_name_1 + "', " +
                    operNote.anesthetist_id_2 + "='" + p.anesthetist_id_2 + "', " +
                    operNote.anesthetist_name_2 + "='" + p.anesthetist_name_2 + "', " +
                    operNote.anesthetist_assistant_id_1 + "='" + p.anesthetist_assistant_id_1 + "', " +
                    operNote.anesthetist_assistant_name_1 + "='" + p.anesthetist_assistant_name_1 + "', " +
                    operNote.anesthetist_assistant_id_2 + "='" + p.anesthetist_assistant_id_2 + "', " +
                    operNote.anesthetist_assistant_name_2 + "='" + p.anesthetist_assistant_name_2 + "', " +
                    operNote.anesthesia_techique_id_1 + "='" + p.anesthesia_techique_id_1 + "', " +
                    operNote.anesthesia_techique_name_1 + "='" + p.anesthesia_techique_name_1 + "', " +
                    operNote.anesthesia_techique_id_2 + "='" + p.anesthesia_techique_id_2 + "', " +
                    operNote.anesthesia_techique_name_2 + "='" + p.anesthesia_techique_name_2 + "', " +
                    operNote.time_start_anesthesia_techique_1 + "='" + p.time_start_anesthesia_techique_1 + "', " +
                    operNote.time_finish_anesthesia_techique_1 + "='" + p.time_finish_anesthesia_techique_1 + "', " +
                    operNote.time_start_anesthesia_techique_2 + "='" + p.time_start_anesthesia_techique_2 + "', " +
                    operNote.time_finish_anesthesia_techique_2 + "='" + p.time_finish_anesthesia_techique_2 + "', " +
                    operNote.total_time_anesthesia_1 + "='" + p.total_time_anesthesia_1 + "', " +
                    operNote.total_time_anesthesia_2 + "='" + p.total_time_anesthesia_2 + "', " +
                    operNote.pre_operatation_diagnosis + "='" + p.pre_operatation_diagnosis + "', " +
                    operNote.post_operation_diagnosis + "='" + p.post_operation_diagnosis + "', " +
                    operNote.operation_1 + "='" + p.operation_1 + "', " +
                    operNote.operation_2 + "='" + p.operation_2 + "', " +
                    operNote.operation_3 + "='" + p.operation_3 + "', " +
                    operNote.operation_4 + "='" + p.operation_4 + "', " +
                    operNote.finding_1 + "='" + p.finding_1 + "', " +
                    operNote.finding_2 + "='" + p.finding_2 + "', " +
                    //operNote.procidures_1 + "='" + p.procidures_1 + "', " +
                    //operNote.procidures_2 + "='" + p.procidures_2 + "', " +
                    operNote.complication + "='" + p.complication + "', " +
                    operNote.estimated_blood_loss + "='" + p.estimated_blood_loss + "', " +
                    operNote.tissue_biopsy + "='" + p.tissue_biopsy + "', " +
                    operNote.tissue_biopsy_unit + "='" + p.tissue_biopsy_unit + "', " +
                    operNote.special_specimen + "='" + p.special_specimen + "', " +
                    operNote.complication_other + "='" + p.complication_other + "', " +
                    operNote.date_modi + " = convert(varchar, getdate(), 121) " +
                    "Where " + operNote.pkField + "='" + p.operative_note_id + "'";
                chk = conn.ExecuteNonQuery(sql);
                chk = p.operative_note_id;
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "update OperativeNote");
            }
            return chk;
        }
        public String updateProcidures1(String opernoteid, String docscanid)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update " + operNote.table + " Set " + 
                    operNote.procidures_1 + "='" + docscanid + "' " +
                    "Where " + operNote.pkField + "='" + opernoteid + "'";
                chk = conn.ExecuteNonQuery(sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "update OperativeNote");
            }
            return chk;
        }
        public String updateFinding1(String opernoteid, String docscanid)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update " + operNote.table + " Set " +
                    operNote.finding_1 + "='" + docscanid + "' " +
                    "Where " + operNote.pkField + "='" + opernoteid + "'";
                chk = conn.ExecuteNonQuery(sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "update OperativeNote");
            }
            return chk;
        }
        public String updateProcidures2(String opernoteid, String docscanid)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update " + operNote.table + " Set " +
                    operNote.procidures_2 + "='" + docscanid + "' " +
                    "Where " + operNote.pkField + "='" + opernoteid + "'";
                chk = conn.ExecuteNonQuery(sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "update OperativeNote");
            }
            return chk;
        }
        public String insertOperativeNote(OperativeNote p, String userid)
        {
            //LabEx item = new LabEx();
            String chk = "";
            //item = selectByPk(p.Id);
            if (p.operative_note_id.Equals(""))
            {
                chk = insert(p, userid);
            }
            else
            {
                chk = update(p);
            }
            return chk;
        }
        public OperativeNote setOperativeNote(DataTable dt)
        {
            OperativeNote operNote1 = new OperativeNote();
            if (dt.Rows.Count > 0)
            {
                operNote1.operative_note_id = dt.Rows[0]["operative_note_id"].ToString();
                operNote1.dept_id = dt.Rows[0]["dept_id"].ToString();
                operNote1.dept_name = dt.Rows[0]["dept_name"].ToString();
                operNote1.ward_id = dt.Rows[0]["ward_id"].ToString();
                operNote1.ward_name = dt.Rows[0]["ward_name"].ToString();
                operNote1.attending_stf_id = dt.Rows[0]["attending_stf_id"].ToString();
                operNote1.attending_stf_name = dt.Rows[0]["attending_stf_name"].ToString();
                operNote1.patient_hn = dt.Rows[0]["patient_hn"].ToString();
                operNote1.patient_fullname = dt.Rows[0]["patient_fullname"].ToString();
                operNote1.age = dt.Rows[0]["age"].ToString();
                operNote1.hn = dt.Rows[0]["hn"].ToString();
                operNote1.an = dt.Rows[0]["an"].ToString();
                operNote1.mnc_date = dt.Rows[0]["mnc_date"].ToString();
                operNote1.pre_no = dt.Rows[0]["pre_no"].ToString();
                operNote1.doc_scan_id = dt.Rows[0]["doc_scan_id"].ToString();
                operNote1.date_operation = dt.Rows[0]["date_operation"].ToString();
                operNote1.time_start = dt.Rows[0]["time_start"].ToString();
                operNote1.time_finish = dt.Rows[0]["time_finish"].ToString();
                operNote1.total_time = dt.Rows[0]["total_time"].ToString();
                operNote1.surgeon_id_1 = dt.Rows[0]["surgeon_id_1"].ToString();
                operNote1.surgeon_name_1 = dt.Rows[0]["surgeon_name_1"].ToString();
                operNote1.surgeon_id_2 = dt.Rows[0]["surgeon_id_2"].ToString();
                operNote1.surgeon_name_2 = dt.Rows[0]["surgeon_name_2"].ToString();
                operNote1.surgeon_id_3 = dt.Rows[0]["surgeon_id_3"].ToString();
                operNote1.surgeon_name_3 = dt.Rows[0]["surgeon_name_3"].ToString();
                operNote1.surgeon_id_4 = dt.Rows[0]["surgeon_id_4"].ToString();
                operNote1.surgeon_name_4 = dt.Rows[0]["surgeon_name_4"].ToString();
                operNote1.assistant_id_1 = dt.Rows[0]["assistant_id_1"].ToString();
                operNote1.assistant_name_1 = dt.Rows[0]["assistant_name_1"].ToString();
                operNote1.assistant_id_2 = dt.Rows[0]["assistant_id_2"].ToString();
                operNote1.assistant_name_2 = dt.Rows[0]["assistant_name_2"].ToString();
                operNote1.assistant_id_3 = dt.Rows[0]["assistant_id_3"].ToString();
                operNote1.assistant_name_3 = dt.Rows[0]["assistant_name_3"].ToString();
                operNote1.assistant_id_4 = dt.Rows[0]["assistant_id_4"].ToString();
                operNote1.assistant_name_4 = dt.Rows[0]["assistant_name_4"].ToString();
                operNote1.scrub_nurse_id_1 = dt.Rows[0]["scrub_nurse_id_1"].ToString();
                operNote1.scrub_nurse_name_1 = dt.Rows[0]["scrub_nurse_name_1"].ToString();
                operNote1.scrub_nurse_id_2 = dt.Rows[0]["scrub_nurse_id_2"].ToString();
                operNote1.scrub_nurse_name_2 = dt.Rows[0]["scrub_nurse_name_2"].ToString();
                operNote1.scrub_nurse_id_3 = dt.Rows[0]["scrub_nurse_id_3"].ToString();
                operNote1.scrub_nurse_name_3 = dt.Rows[0]["scrub_nurse_name_3"].ToString();
                operNote1.scrub_nurse_id_4 = dt.Rows[0]["scrub_nurse_id_4"].ToString();
                operNote1.scrub_nurse_name_4 = dt.Rows[0]["scrub_nurse_name_4"].ToString();
                operNote1.circulation_nurse_id_1 = dt.Rows[0]["circulation_nurse_id_1"].ToString();
                operNote1.circulation_nurse_name_1 = dt.Rows[0]["circulation_nurse_name_1"].ToString();
                operNote1.circulation_nurse_id_2 = dt.Rows[0]["circulation_nurse_id_2"].ToString();
                operNote1.circulation_nurse_name_2 = dt.Rows[0]["circulation_nurse_name_2"].ToString();
                operNote1.circulation_nurse_id_3 = dt.Rows[0]["circulation_nurse_id_3"].ToString();
                operNote1.circulation_nurse_name_3 = dt.Rows[0]["circulation_nurse_name_3"].ToString();
                operNote1.circulation_nurse_id_4 = dt.Rows[0]["circulation_nurse_id_4"].ToString();
                operNote1.circulation_nurse_name_4 = dt.Rows[0]["circulation_nurse_name_4"].ToString();
                operNote1.perfusionist_id_1 = dt.Rows[0]["perfusionist_id_1"].ToString();
                operNote1.perfusionist_name_1 = dt.Rows[0]["perfusionist_name_1"].ToString();
                operNote1.perfusionist_id_2 = dt.Rows[0]["perfusionist_id_2"].ToString();
                operNote1.perfusionist_name_2 = dt.Rows[0]["perfusionist_name_2"].ToString();
                operNote1.perfusionist_id_3 = dt.Rows[0]["perfusionist_id_3"].ToString();
                operNote1.perfusionist_name_3 = dt.Rows[0]["perfusionist_name_3"].ToString();
                operNote1.perfusionist_id_4 = dt.Rows[0]["perfusionist_id_4"].ToString();
                operNote1.perfusionist_name_4 = dt.Rows[0]["perfusionist_name_4"].ToString();
                operNote1.anesthetist_id_1 = dt.Rows[0]["anesthetist_id_1"].ToString();
                operNote1.anesthetist_name_1 = dt.Rows[0]["anesthetist_name_1"].ToString();
                operNote1.anesthetist_id_2 = dt.Rows[0]["anesthetist_id_2"].ToString();
                operNote1.anesthetist_name_2 = dt.Rows[0]["anesthetist_name_2"].ToString();
                operNote1.anesthetist_assistant_id_1 = dt.Rows[0]["anesthetist_assistant_id_1"].ToString();
                operNote1.anesthetist_assistant_name_1 = dt.Rows[0]["anesthetist_assistant_name_1"].ToString();
                operNote1.anesthetist_assistant_id_2 = dt.Rows[0]["anesthetist_assistant_id_2"].ToString();
                operNote1.anesthetist_assistant_name_2 = dt.Rows[0]["anesthetist_assistant_name_2"].ToString();
                operNote1.anesthesia_techique_id_1 = dt.Rows[0]["anesthesia_techique_id_1"].ToString();
                operNote1.anesthesia_techique_name_1 = dt.Rows[0]["anesthesia_techique_name_1"].ToString();
                operNote1.anesthesia_techique_id_2 = dt.Rows[0]["anesthesia_techique_id_2"].ToString();
                operNote1.anesthesia_techique_name_2 = dt.Rows[0]["anesthesia_techique_name_2"].ToString();
                operNote1.time_start_anesthesia_techique_1 = dt.Rows[0]["time_start_anesthesia_techique_1"].ToString();
                operNote1.time_finish_anesthesia_techique_1 = dt.Rows[0]["time_finish_anesthesia_techique_1"].ToString();
                operNote1.time_start_anesthesia_techique_2 = dt.Rows[0]["time_start_anesthesia_techique_2"].ToString();
                operNote1.time_finish_anesthesia_techique_2 = dt.Rows[0]["time_finish_anesthesia_techique_2"].ToString();
                operNote1.total_time_anesthesia_1 = dt.Rows[0]["total_time_anesthesia_1"].ToString();
                operNote1.total_time_anesthesia_2 = dt.Rows[0]["total_time_anesthesia_2"].ToString();
                operNote1.pre_operatation_diagnosis = dt.Rows[0]["pre_operatation_diagnosis"].ToString();
                operNote1.post_operation_diagnosis = dt.Rows[0]["post_operation_diagnosis"].ToString();
                operNote1.operation_1 = dt.Rows[0]["operation_1"].ToString();
                operNote1.operation_2 = dt.Rows[0]["operation_2"].ToString();
                operNote1.operation_3 = dt.Rows[0]["operation_3"].ToString();
                operNote1.operation_4 = dt.Rows[0]["operation_4"].ToString();
                operNote1.finding_1 = dt.Rows[0]["finding_1"].ToString();
                operNote1.finding_2 = dt.Rows[0]["finding_2"].ToString();
                operNote1.procidures_1 = dt.Rows[0]["procidures_1"].ToString();
                operNote1.procidures_2 = dt.Rows[0]["procidures_2"].ToString();
                operNote1.complication = dt.Rows[0]["complication"].ToString();
                operNote1.estimated_blood_loss = dt.Rows[0]["estimated_blood_loss"].ToString();
                operNote1.tissue_biopsy = dt.Rows[0]["tissue_biopsy"].ToString();
                operNote1.tissue_biopsy_unit = dt.Rows[0]["tissue_biopsy_unit"].ToString();
                operNote1.special_specimen = dt.Rows[0]["special_specimen"].ToString();
                operNote1.complication_other = dt.Rows[0]["complication_other"].ToString();
            }
            else
            {
                setOperativeNote(operNote1);
            }
            return operNote1;
        }
        public OperativeNote setOperativeNote(OperativeNote p)
        {
            //OperativeNote operNote1 = new OperativeNote();
            p.operative_note_id = "";
            p.dept_id = "";
            p.dept_name = "";
            p.ward_id = "";
            p.ward_name = "";
            p.attending_stf_id = "";
            p.attending_stf_name = "";
            p.patient_hn = "";
            p.patient_fullname = "";
            p.age = "";
            p.hn = "";
            p.an = "";
            p.mnc_date = "";
            p.pre_no = "";
            p.doc_scan_id = "";
            p.date_operation = "";
            p.time_start = "";
            p.time_finish = "";
            p.total_time = "";
            p.surgeon_id_1 = "";
            p.surgeon_name_1 = "";
            p.surgeon_id_2 = "";
            p.surgeon_name_2 = "";
            p.surgeon_id_3 = "";
            p.surgeon_name_3 = "";
            p.surgeon_id_4 = "";
            p.surgeon_name_4 = "";
            p.assistant_id_1 = "";
            p.assistant_name_1 = "";
            p.assistant_id_2 = "";
            p.assistant_name_2 = "";
            p.assistant_id_3 = "";
            p.assistant_name_3 = "";
            p.assistant_id_4 = "";
            p.assistant_name_4 = "";
            p.scrub_nurse_id_1 = "";
            p.scrub_nurse_name_1 = "";
            p.scrub_nurse_id_2 = "";
            p.scrub_nurse_name_2 = "";
            p.scrub_nurse_id_3 = "";
            p.scrub_nurse_name_3 = "";
            p.scrub_nurse_id_4 = "";
            p.scrub_nurse_name_4 = "";
            p.circulation_nurse_id_1 = "";
            p.circulation_nurse_name_1 = "";
            p.circulation_nurse_id_2 = "";
            p.circulation_nurse_name_2 = "";
            p.circulation_nurse_id_3 = "";
            p.circulation_nurse_name_3 = "";
            p.circulation_nurse_id_4 = "";
            p.circulation_nurse_name_4 = "";
            p.perfusionist_id_1 = "";
            p.perfusionist_name_1 = "";
            p.perfusionist_id_2 = "";
            p.perfusionist_name_2 = "";
            p.perfusionist_id_3 = "";
            p.perfusionist_name_3 = "";
            p.perfusionist_id_4 = "";
            p.perfusionist_name_4 = "";
            p.anesthetist_id_1 = "";
            p.anesthetist_name_1 = "";
            p.anesthetist_id_2 = "";
            p.anesthetist_name_2 = "";
            p.anesthetist_assistant_id_1 = "";
            p.anesthetist_assistant_name_1 = "";
            p.anesthetist_assistant_id_2 = "";
            p.anesthetist_assistant_name_2 = "";
            p.anesthesia_techique_id_1 = "";
            p.anesthesia_techique_name_1 = "";
            p.anesthesia_techique_id_2 = "";
            p.anesthesia_techique_name_2 = "";
            p.time_start_anesthesia_techique_1 = "";
            p.time_finish_anesthesia_techique_1 = "";
            p.time_start_anesthesia_techique_2 = "";
            p.time_finish_anesthesia_techique_2 = "";
            p.total_time_anesthesia_1 = "";
            p.total_time_anesthesia_2 = "";
            p.pre_operatation_diagnosis = "";
            p.post_operation_diagnosis = "";
            p.operation_1 = "";
            p.operation_2 = "";
            p.operation_3 = "";
            p.operation_4 = "";
            p.finding_1 = "";
            p.finding_2 = "";
            p.procidures_1 = "";
            p.procidures_2 = "";
            p.complication = "";
            p.estimated_blood_loss = "";
            p.tissue_biopsy = "";
            p.tissue_biopsy_unit = "";
            p.special_specimen = "";
            p.complication_other = "";
            return p;
        }
    }
}
