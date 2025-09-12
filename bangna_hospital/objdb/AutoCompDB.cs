using AutocompleteMenuNS;
using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class AutoCompDB
    {
        ConnectDB conn;
        public AutoComp autoComp;
        AutoCompleteSource acs;
        public AutoCompDB(ConnectDB c)
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            autoComp = new AutoComp();
            autoComp.auto_com_id = "auto_com_id";
            autoComp.auto_com_line1 = "auto_com_line1";
            autoComp.auto_com_line2 = "auto_com_line2";
            autoComp.auto_com_line3 = "auto_com_line3";
            autoComp.modu1 = "modu1";
            autoComp.active = "active";
            autoComp.user_id = "user_id";
            autoComp.pkField = "auto_com_id";
            autoComp.table = "b_auto_comp";
        }
        public String selectByPk(String pk)
        {
            String sql = "", re = "";
            sql = "Select " + autoComp.auto_com_line1 + " From " + autoComp.table + " Where " + autoComp.pkField + "='" + pk + "'";
            DataTable dt = new DataTable();
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0][autoComp.auto_com_line1].ToString();
            }
            return re;
        }
        public DataTable selectByLine1(String userid)
        {
            String sql = "", re = "";
            sql = "Select " + autoComp.auto_com_line1 + " From " + autoComp.table + " Where " + autoComp.user_id + "='" + userid + "' and active = '1' ";
            DataTable dt = new DataTable();
            dt = conn.selectData(sql);
            
            return dt;
        }
        public DataTable selectLine1ByModu1(String modu1,String userid)
        {
            String sql = "", re = "";
            sql = "Select " + autoComp.auto_com_id + ", " + autoComp.auto_com_line1 + " From " + autoComp.table + " Where " + autoComp.user_id + "='" + userid + "' and " + autoComp.modu1 + "='" + modu1 + "' and active = '1' Order By " + autoComp.auto_com_line1;
            DataTable dt = new DataTable();
            dt = conn.selectData(sql);

            return dt;
        }
        public String insertAutoComp(AutoComp p)
        {
            String sql = "", re = "";
            if (p.auto_com_id.Equals(""))            {                re = insert(p);            }
            else            {       re = update(p);            }
            return re;
        }
        public String insert(AutoComp p)
        {
            String sql = "", re = "";
            sql = "Insert Into " + autoComp.table + " (" + autoComp.auto_com_line1 + "," + autoComp.auto_com_line2 + "," +
                autoComp.auto_com_line3 + "," + autoComp.modu1 + "," + autoComp.user_id + "," + autoComp.active + ") " +
                "Values ('" + p.auto_com_line1.Replace("'","''") + "','" + p.auto_com_line2.Replace("'", "''") + "','" +
                p.auto_com_line3.Replace("'", "''") + "','" + p.modu1 + "','" + p.user_id + "','1')";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "insert AutoComp");
                new LogWriter("e", "AutoCompDB insert ex " + ex.InnerException+" "+ ex.Message);
                re = ex.Message;
            }
            return re;
        }
        public String update(AutoComp p)
        {
            String sql = "", re = "";
            sql = "Update " + autoComp.table + " Set " + autoComp.auto_com_line1 + "='" + p.auto_com_line1.Replace("'", "''") + "'," +
                "" + autoComp.auto_com_line2 + "='" + p.auto_com_line2.Replace("'", "''") + "'," +
                "" + autoComp.auto_com_line3 + "='" + p.auto_com_line3.Replace("'", "''") + "'," +
                "" + autoComp.modu1 + "='" + p.modu1 + "'," +
                "" + autoComp.user_id + "='" + p.user_id + "' " +
                "Where " + autoComp.pkField + "='" + p.auto_com_id + "'";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "update AutoComp");
                new LogWriter("e", "AutoCompDB update ex " + ex.InnerException + " " + ex.Message);
            }
            return re;
        }
        public String voidAuto(String id, String userid)
        {
            String sql = "", re = "";
            sql = "Update " + autoComp.table + " Set " + autoComp.active + "='3' " +
                "Where " + autoComp.pkField + "='" + id + "'";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "update AutoComp");
                new LogWriter("e", "AutoCompDB update ex " + ex.InnerException + " " + ex.Message);
            }
            return re;
        }
        public AutoCompleteStringCollection getlAutoLine1(String userid)
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            dt = selectByLine1(userid);
            foreach (DataRow row in dt.Rows)            {                autoSymptom.Add(row["auto_com_line1"].ToString());            }
            return autoSymptom;
        }
        public AutoCompleteStringCollection getlAutoLine1(String modu1, String userid)
        {
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            dt = selectLine1ByModu1(modu1,userid);
            foreach (DataRow row in dt.Rows) { autoSymptom.Add(row["auto_com_line1"].ToString()); }
            return autoSymptom;
        }
        public List<AutocompleteItem> BuildAutoCompleteLine1(String modu1, String userid)
        {
            List<AutocompleteItem> items = new List<AutocompleteItem>();
            DataTable dt = new DataTable();
            dt = selectLine1ByModu1(modu1, userid);
            foreach (DataRow row in dt.Rows)
                items.Add(new AutocompleteItem(row["auto_com_line1"].ToString()));

            return items;
        }
    }
}
