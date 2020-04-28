using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class DocGroupFMDB
    {
        ConnectDB conn;
        DocGroupFM dfm;
        public List<DocGroupFM> lDgss;
        public List<DocGroupFM> lDgssDeptUS;
        public DocGroupFMDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            dfm = new DocGroupFM();

            dfm.active = "active";
            dfm.fm_code = "fm_code";
            dfm.fm_id = "fm_id";
            dfm.fm_name = "fm_name";
            dfm.doc_group_sub_id = "doc_group_sub_id";
            dfm.doc_group_id = "doc_group_id";
            dfm.status_doc_adminsion = "status_doc_adminsion";
            dfm.status_doc_medical = "status_doc_medical";
            dfm.status_doc_nurse = "status_doc_nurse";
            dfm.status_doc_office = "status_doc_office";

            dfm.table = "doc_group_fm";
            dfm.pkField = "fm_id";
        }
        public void getlBspDeptUS()
        {
            //lDept = new List<Position>();

            lDgssDeptUS.Clear();
            DataTable dt = new DataTable();
            dt = selectAllDeptUS();
            foreach (DataRow row in dt.Rows)
            {
                DocGroupFM itm1 = new DocGroupFM();
                itm1.active = row[dfm.active].ToString();
                itm1.doc_group_sub_id = row[dfm.doc_group_sub_id].ToString();
                itm1.fm_code = row[dfm.fm_code].ToString();
                itm1.fm_name = row[dfm.fm_name].ToString();
                itm1.active = row[dfm.active].ToString();
                itm1.status_doc_adminsion = row[dfm.status_doc_adminsion].ToString();
                itm1.status_doc_medical = row[dfm.status_doc_medical].ToString();
                itm1.status_doc_nurse = row[dfm.status_doc_nurse].ToString();
                itm1.status_doc_office = row[dfm.status_doc_office].ToString();
                lDgssDeptUS.Add(itm1);
            }
        }
        public void getlBsp()
        {
            //lDept = new List<Position>();

            lDgss.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            foreach (DataRow row in dt.Rows)
            {
                DocGroupFM itm1 = new DocGroupFM();
                itm1.fm_id = row[dfm.fm_id].ToString();
                itm1.doc_group_sub_id = row[dfm.doc_group_sub_id].ToString();
                itm1.fm_code = row[dfm.fm_code].ToString();
                itm1.fm_name = row[dfm.fm_name].ToString();
                itm1.active = row[dfm.active].ToString();
                itm1.status_doc_adminsion = row[dfm.status_doc_adminsion].ToString();
                itm1.status_doc_medical = row[dfm.status_doc_medical].ToString();
                itm1.status_doc_nurse = row[dfm.status_doc_nurse].ToString();
                itm1.status_doc_office = row[dfm.status_doc_office].ToString();
                lDgss.Add(itm1);
            }
        }
        public DocGroupFM setDocFM(DataTable dt)
        {
            DocGroupFM dfm = new DocGroupFM();
            if (dt.Rows.Count > 0)
            {
                dfm.fm_id = dt.Rows[0][this.dfm.fm_id].ToString();
                dfm.doc_group_sub_id = dt.Rows[0][this.dfm.doc_group_sub_id].ToString();
                dfm.fm_code = dt.Rows[0][this.dfm.fm_code].ToString();
                dfm.fm_name = dt.Rows[0][this.dfm.fm_name].ToString();
                dfm.active = dt.Rows[0][this.dfm.active].ToString();
                dfm.doc_group_id =  dt.Rows[0][this.dfm.doc_group_id].ToString();
                dfm.status_doc_adminsion = dt.Rows[0][this.dfm.status_doc_adminsion].ToString() ;
                dfm.status_doc_medical = dt.Rows[0][this.dfm.status_doc_medical].ToString() ;
                dfm.status_doc_nurse =dt.Rows[0][this.dfm.status_doc_nurse].ToString();
                dfm.status_doc_office = dt.Rows[0][this.dfm.status_doc_office].ToString();
            }
            else
            {
                setDocGroupFM(dfm);
            }
            return dfm;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select dfm.*, dgss.doc_group_sub_name " +
                "From " + dfm.table + " dfm " +
                "Left Join doc_group_sub_scan dgss On dfm.doc_group_sub_id = dgss.doc_group_sub_id " +
                " Where dfm." + dfm.active + " ='1' " +
                "Order By dfm.fm_code ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DocGroupFM selectByPk(String id)
        {
            DocGroupFM cop1 = new DocGroupFM();
            DataTable dt = new DataTable();
            String sql = "select dfm.*, dgss.doc_group_id " +
                "From " + dfm.table + " dfm " +
                "Left Join doc_group_sub_scan dgss On dfm.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dfm." + dfm.pkField + " ='" + id + "' " +
                "Order By dfm.fm_id ";
            dt = conn.selectData(conn.conn, sql);
            cop1 = setDocFM(dt);
            return cop1;
        }
        public DocGroupFM selectByFMCode(String id)
        {
            DocGroupFM cop1 = new DocGroupFM();
            DataTable dt = new DataTable();
            String sql = "select dfm.*, dgss.doc_group_id " +
                "From " + dfm.table + " dfm " +
                "Left Join doc_group_sub_scan dgss On dfm.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dfm." + dfm.fm_code + " ='" + id + "' " +
                "Order By dfm.fm_id ";
            dt = conn.selectData(conn.conn, sql);
            cop1 = setDocFM(dt);
            return cop1;
        }
        public DataTable selectByPk1(String id)
        {
            DocGroupFM cop1 = new DocGroupFM();
            DataTable dt = new DataTable();
            String sql = "select dfm.*, dgss.doc_group_id " +
                "From " + dfm.table + " dfm " +
                "Left Join doc_group_sub_scan dgss On dfm.doc_group_sub_id = dgss.doc_group_sub_id " +
                "Where dfm." + dfm.pkField + " ='" + id + "' " +
                "Order By dfm.fm_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectAllDeptUS()
        {
            DataTable dt = new DataTable();
            String sql = "select dgss.*, dgs.doc_group_name " +
                "From " + dfm.table + " dgss " +
                "Left Join doc_group_scan dgs On dgs.doc_group_id = dgss.doc_group_id " +
                " Where dgss." + dfm.active + " ='1' and dept_us = '1' " +
                "Order By dgss.doc_group_id, dgss.doc_group_sub_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DocGroupFM setDocGroupFM(DocGroupFM dgs1)
        {
            dgs1.active = "";
            dgs1.remark = "";
            dgs1.fm_id = "";
            dgs1.doc_group_sub_id = "";
            dgs1.fm_code = "";
            dgs1.fm_name = "";
            dgs1.status_doc_adminsion = "";
            dgs1.status_doc_medical = "";
            dgs1.status_doc_nurse = "";
            dgs1.status_doc_office = "";
            return dgs1;
        }
        private void chkNull(DocGroupFM p)
        {
            long chk = 0;
            int chk1 = 0;

            p.date_modi = p.date_modi == null ? "" : p.date_modi;
            p.date_cancel = p.date_cancel == null ? "" : p.date_cancel;
            p.user_create = p.user_create == null ? "" : p.user_create;
            p.user_modi = p.user_modi == null ? "" : p.user_modi;
            p.user_cancel = p.user_cancel == null ? "" : p.user_cancel;

            p.fm_code = p.fm_code == null ? "" : p.fm_code;
            p.fm_name = p.fm_name == null ? "" : p.fm_name;
            p.active = p.active == null ? "" : p.active;
            p.status_doc_adminsion = p.status_doc_adminsion == null ? "" : p.status_doc_adminsion;
            p.status_doc_medical = p.status_doc_medical == null ? "" : p.status_doc_medical;
            p.status_doc_nurse = p.status_doc_nurse == null ? "" : p.status_doc_nurse;
            p.status_doc_office = p.status_doc_office == null ? "0" : p.status_doc_office;

            p.doc_group_sub_id = long.TryParse(p.doc_group_sub_id, out chk) ? chk.ToString() : "0";
            //p.pre_no = long.TryParse(p.pre_no, out chk) ? chk.ToString() : "0";
        }
        public String insert(DocGroupFM p, String userId)
        {
            String re = "";
            String sql = "";
            p.active = "1";
            //p.ssdata_id = "";
            int chk = 0;
            chkNull(p);
            sql = "Insert Into " + dfm.table + " (" + dfm.fm_code + "," + dfm.active + "," + dfm.fm_name + "," + dfm.doc_group_sub_id +"'"+
                ",'" + dfm.status_doc_adminsion + "','" + dfm.status_doc_medical + "','" + dfm.status_doc_nurse + "','" + dfm.status_doc_office + "'"+
                ") " +
                "Values ('" + p.fm_code.Replace("'", "''") + "','1','" + p.fm_name.Replace("'", "''") + "','" + p.doc_group_sub_id + "' " +
                ",'" + p.status_doc_adminsion + "','" + p.status_doc_medical + "','" + p.status_doc_nurse + "','" + p.status_doc_office + "' " +
                ")";
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
        public String update(DocGroupFM p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            chkNull(p);
            sql = "Update " + dfm.table + " Set " +
                " " + dfm.fm_code + " = '" + p.fm_code.Replace("'", "''") + "'" +
                ", " + dfm.fm_name + " = '" + p.fm_name.Replace("'", "''") + "'" +
                ", " + dfm.doc_group_sub_id + " = '" + p.doc_group_sub_id + "'" +
                ", " + dfm.status_doc_adminsion + " = '" + p.status_doc_adminsion + "'" +
                ", " + dfm.status_doc_medical + " = '" + p.status_doc_medical + "'" +
                ", " + dfm.status_doc_nurse + " = '" + p.status_doc_nurse + "'" +
                ", " + dfm.status_doc_office + " = '" + p.status_doc_office + "'" +
                "Where " + dfm.pkField + "='" + p.fm_id + "'"
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
        public String insertDocGroupFM(DocGroupFM p, String userId)
        {
            String re = "";

            if (p.fm_id.Equals(""))
            {
                re = insert(p, "");
            }
            else
            {
                re = update(p, "");
            }

            return re;
        }
    }
}
