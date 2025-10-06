using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace bangna_hospital.objdb
{
    public class AttachNoteDB
    {
        ConnectDB conn;
        public AttachNote attachNote;
        public AttachNoteDB(ConnectDB conn)
        {
            attachNote = new AttachNote();
            this.conn = conn;
            initConfig();
        }
        public void initConfig()
        {
            attachNote.attach_note_id = "attach_note_id";
            attachNote.attach_note = "attach_note";
            attachNote.active = "active";
            attachNote.remark = "remark";
            attachNote.date_create = "date_create";
            attachNote.user_create = "user_create";
            attachNote.user_modi = "user_modi";
            attachNote.date_modi = "date_modi";
            attachNote.hn = "hn";

            attachNote.pkField = "attach_note_id";
            attachNote.table = "t_attach_note";
        }
        public String chknull(AttachNote p)
        {
            if (p.attach_note_id == null) p.attach_note_id = "";
            if (p.attach_note == null) p.attach_note = "";
            if (p.active == null) p.active = "";
            if (p.remark == null) p.remark = "";
            if (p.date_create == null) p.date_create = "";
            if (p.user_create == null) p.user_create = "";
            if (p.date_modi == null) p.date_modi = "";
            if (p.user_modi == null) p.user_modi = "";
            return p.attach_note_id;
        }
        public String insert(AttachNote p)
        {
            String sql = "", chk = "";
            try
            {
                //p.attach_note_id = p.getGenID();
                sql = "Insert Into " + attachNote.table + " (" + attachNote.attach_note + "," +
                    attachNote.active + "," + attachNote.remark + "," +
                    attachNote.date_create + "," + attachNote.user_create+","+p.hn + ") " +
                    "Values ('" + p.attach_note.Replace("'","''") + "','" +
                    p.active + "','" + p.remark.Replace("'", "''") + "'," +
                    "convert(varchar(20), pt01.MNC_DATE,23),'" + p.user_create+"','"+p.hn + "')";
                chk = conn.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert sql " + sql);
                //insertLogPage(BC.userId, this.Name, " insert  ", ex.Message);
            }
            return chk;
        }
        public String update(AttachNote p)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update " + p.table + " Set " +
                    " " + p.attach_note + " = '" + p.attach_note.Replace("'", "''") + "'" +
                    "," + p.active + " = '" + p.active + "'" +
                    "," + p.remark + " = '" + p.remark.Replace("'", "''") + "'" +
                    "," + p.date_modi + " = convert(varchar(20), pt01.MNC_DATE,23)" +
                    "," + p.user_modi + " = '" + p.user_modi + "'" +
                    "Where " + p.pkField + "='" + p.attach_note_id + "'"
                    ;
                chk = conn.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "update sql " + sql);
                //insertLogPage(BC.userId, this.Name, "update  ", ex.Message);
            }
            return chk;
        }
        public String insertAttachNote(AttachNote p)
        {
            //AttachNote chk = selectByPk(p.attach_note_id);
            chknull(p);
            if (p.attach_note_id == "")
            {
                return insert(p);
            }
            else
            {
                return update(p);
            }
        }    }
}
