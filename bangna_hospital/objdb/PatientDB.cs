using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PatientDB
    {
        public ConnectDB conn;
        Patient ptt;
        public PatientDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            ptt = new Patient();
            
            ptt.Age = "";
            ptt.Hn = "";
            ptt.Name = "";

            ptt.pkField = "";
            ptt.table = "";
        }
        public DataTable selectPatient(String hn, String name)
        {
            DataTable dt = new DataTable();
            String sql = "", wherehn="", wherename="";
            if (hn.Length > 0)
            {
                wherehn = " m01.MNC_HN_NO like '%"+hn+"%'";
            }
            if (name.Length > 0)
            {
                wherename = " or m01.MNC_FNAME_T like '%" + name + "%' or m01.MNC_LNAME_T like '%" + name + "%' ";
            }
            if(hn.Equals("") && name.Equals(""))
            {
                wherehn = "m01.MNC_HN_NO='999' ";
            }
            sql = "Select m01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE " +
                "From  patient_m01 m01 " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where  "+ wherehn + wherename;
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public Patient selectPatinet(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select m01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE " +
                "From  patient_m01 m01 " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where m01.MNC_HN_NO = '" + hn + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                ptt.Hn = dt.Rows[0]["MNC_HN_NO"].ToString();
                ptt.Age = dt.Rows[0]["MNC_AGE"].ToString();
                ptt.Name = dt.Rows[0]["prefix"].ToString() + " " + dt.Rows[0]["MNC_FNAME_T"].ToString() + " " + dt.Rows[0]["MNC_LNAME_T"].ToString();
            }
            return ptt;
        }
        public Patient selectPatinet1(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select m01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE " +
                "From  patient_m01 m01 " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where m01.MNC_HN_NO like '" + hn + "%' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                ptt.Hn = dt.Rows[0]["MNC_HN_NO"].ToString();
                ptt.Age = dt.Rows[0]["MNC_AGE"].ToString();
                ptt.Name = dt.Rows[0]["prefix"].ToString() + " " + dt.Rows[0]["MNC_FNAME_T"].ToString() + " " + dt.Rows[0]["MNC_LNAME_T"].ToString();
            }
            return ptt;
        }
    }
}
