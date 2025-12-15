using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class StaffDB
    {
        public Staff stf;
        public List<Staff> lStf;
        ConnectDB conn;

        public StaffDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            stf = new Staff();
            stf.username = "mnc_usr_name";
            stf.password1 = "mnc_usr_pw";
            stf.fullname = "mnc_usr_full";

            stf.table = "userlog_m01";
        }
        public Staff selectByUsernameLevelDoctor(String username)
        {
            Staff cop1 = new Staff();
            DataTable dt = new DataTable();
            String sql = "select stf.mnc_usr_name, stf.mnc_usr_pw, stf.mnc_usr_full,stf.mnc_usr_lev " +
                "From userlog_m01 stf " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where stf." + stf.username + " ='" + username + " and mnc_usr_lev = '5'  ";
            dt = conn.selectData(conn.connMainHIS, sql);
            cop1 = setStaff(dt);
            return cop1;
        }
        public Staff selectByUsernameLevelDoctor1(String username)
        {
            Staff cop1 = new Staff();
            DataTable dt = new DataTable();
            String sql = "select stf.mnc_usr_name, stf.mnc_usr_pw, stf.mnc_usr_full,stf.mnc_usr_lev " +
                "From userlog_m01 stf " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where (stf." + stf.username + " ='" + username + "' or stf." + stf.fullname + " like '" + username + "%') and mnc_usr_lev = '5'  ";
            dt = conn.selectData(conn.connMainHIS, sql);
            cop1 = setStaff(dt);
            return cop1;
        }
        public Staff selectByUsername(String username)
        {
            Staff cop1 = new Staff();
            DataTable dt = new DataTable();
            String sql = "select stf.mnc_usr_name, stf.mnc_usr_pw, stf.mnc_usr_full,stf.mnc_usr_lev " +
                "From userlog_m01 stf " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where stf." + stf.username + " ='" + username + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            cop1 = setStaff(dt);
            return cop1;
        }
        public Staff selectByUsername1(String username)
        {
            Staff cop1 = new Staff();
            DataTable dt = new DataTable();
            String sql = "select stf.mnc_usr_name, stf.mnc_usr_pw, stf.mnc_usr_full,stf.mnc_usr_lev " +
                "From userlog_m01 stf " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where stf." + stf.username + " ='" + username + "' or stf." + stf.fullname + " like '" + username + "%'";
            dt = conn.selectData(conn.connMainHIS, sql);
            cop1 = setStaff(dt);
            return cop1;
        }
        public String selectByPassword(String username)
        {
            Staff cop1 = new Staff();
            DataTable dt = new DataTable();
            String re = "";
            String sql = "select stf.mnc_usr_name, stf.mnc_usr_pw, stf.mnc_usr_full,stf.mnc_usr_lev " +
                "From userlog_m01 stf " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where stf." + stf.password1 + " ='" + username + "'";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["mnc_usr_full"].ToString();
            }
            return re;
        }
        public DataTable selectByLevel(String username)
        {
            Staff cop1 = new Staff();
            DataTable dt = new DataTable();
            String sql = "select stf.mnc_usr_name, stf.mnc_usr_full " +
                "From userlog_m01 stf " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where stf.mnc_usr_lev in (" + username + ") " +
                "Order By stf.mnc_usr_name ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //cop1 = setStaff(dt);
            return dt;
        }
        public Staff selectByLogin(String username)
        {
            Staff cop1 = new Staff();
            DataTable dt = new DataTable();
            try
            {
                cop1 = setStaff1(cop1);
                if (username == null)
                {
                    return cop1;
                }
                //if (password1 == null)
                //{
                //    return cop1;
                //}
                if (username.Equals(""))
                {
                    return cop1;
                }
                //if (password1.Equals(""))
                //{
                //    return cop1;
                //}
                String sql = "select stf.mnc_usr_name, stf.mnc_usr_pw, stf.mnc_usr_full,stf.mnc_usr_lev " +
                    "From " + stf.table + " stf " +
                    //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                    "Where stf." + stf.username + " ='" + username + "'  ";
                dt = conn.selectData(conn.connMainHIS, sql);
                cop1 = setStaff(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "error");
            }

            return cop1;
        }
        public Staff selectByLogin(String username, String password1)
        {
            Staff cop1 = new Staff();
            DataTable dt = new DataTable();
            try
            {
                cop1 = setStaff1(cop1);
                if (username == null)
                {
                    return cop1;
                }
                if (password1 == null)
                {
                    return cop1;
                }
                if (username.Equals(""))
                {
                    return cop1;
                }
                if (password1.Equals(""))
                {
                    return cop1;
                }
                String sql = "Select stf.mnc_usr_name, stf.mnc_usr_pw, stf.mnc_usr_full,stf.mnc_usr_lev " +
                    "From " + stf.table + " stf " +
                    //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                    "Where stf." + stf.username + " ='" + username + "' and stf." + stf.password1 + "='" + password1 + "' ";
                dt = conn.selectData(conn.connMainHIS, sql);
                cop1 = setStaff(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "error");
            }

            return cop1;
        }
        public Staff selectByPasswordConfirm1(String pass)
        {
            Staff cop1 = new Staff();
            DataTable dt = new DataTable();
            String sql = "select stf.mnc_usr_name, stf.mnc_usr_pw, stf.mnc_usr_full,stf.mnc_usr_lev  " +
                "From " + stf.table + " stf " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where stf." + stf.password1 + " ='" + pass + "' and MNC_USR_STS = 'Y' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                cop1 = setStaff(dt);
                return cop1;
            }
            else
            {
                return setStaff1(cop1);
            }
        }
        public Staff setStaff1(Staff stf1)
        {
            stf1.username = "";
            stf1.password1 = "";
            stf1.fullname = "";

            return stf1;
        }
        public Staff setStaff(DataTable dt)
        {
            Staff stf1 = new Staff();
            if (dt.Rows.Count > 0)
            {
                //MessageBox.Show("setStaff if (dt.Rows.Count > 0)", "");
                stf1.staff_id = dt.Rows[0]["mnc_usr_name"].ToString();
                stf1.username = dt.Rows[0]["mnc_usr_name"].ToString();
                stf1.password1 = dt.Rows[0]["mnc_usr_pw"].ToString();
                stf1.fullname = dt.Rows[0]["mnc_usr_full"].ToString();
                stf1.priority = dt.Rows[0]["mnc_usr_lev"].ToString();
                if (stf1.priority.Equals("5"))
                {
                    stf1.status_module_doctor = "1";
                }
            }
            else
            {
                //MessageBox.Show("setStaff else", "");
                stf1.username = "";
                stf1.password1 = "";
                stf1.fullname = "";
            }
            return stf1;
        }
    }
}
