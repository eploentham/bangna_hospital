using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    internal class PatientT02DB
    {
        public PatientT02 patt02;
        ConnectDB conn;
        public List<PatientT02> lPatt02;
        public PatientT02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            patt02 = new PatientT02();
            patt02.MNC_HN_NO = "MNC_HN_NO";
            patt02.MNC_HN_YR = "MNC_HN_YR";
            patt02.MNC_PH_CD = "MNC_PH_CD";
            patt02.MNC_DATE = "MNC_DATE";
            patt02.MNC_PH_MEMO = "MNC_PH_MEMO";
            patt02.MNC_PH_ALG_CD = "MNC_PH_ALG_CD";
            patt02.MNC_EMP_CD = "MNC_EMP_CD";
            patt02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            patt02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            patt02.table = "PATIENT_T02";
            lPatt02 = new List<PatientT02>();
        }
        public DataTable selectByPk(String hn, String pharcode)
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + patt02.table + " " +
                "Where " + patt02.MNC_HN_NO + "='" + hn + "' and " + patt02.MNC_PH_CD + "='" + pharcode + "' ";
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectByHn(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + patt02.table + " " +
                "Where " + patt02.MNC_HN_NO + "='" + hn + "' ";
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + patt02.table + " ";
            dt = conn.selectData(sql);
            return dt;
        }
        public void chkNull(PatientT02 p)
        {
            if (p.MNC_HN_NO == null) p.MNC_HN_NO = "";
            if (p.MNC_HN_YR == null) p.MNC_HN_YR = "";
            if (p.MNC_PH_CD == null) p.MNC_PH_CD = "";
            if (p.MNC_DATE == null) p.MNC_DATE = "";
            if (p.MNC_PH_MEMO == null) p.MNC_PH_MEMO = "";
            if (p.MNC_PH_ALG_CD == null) p.MNC_PH_ALG_CD = "";
            if (p.MNC_EMP_CD == null) p.MNC_EMP_CD = "";
            if (p.MNC_STAMP_DAT == null) p.MNC_STAMP_DAT = "";
            if (p.MNC_STAMP_TIM == null) p.MNC_STAMP_TIM = "";
        }
        public String insert(PatientT02 p)
        {
            String re = "";
            String sql = "";
            sql = "Insert Into " + patt02.table + " (" +patt02.MNC_HN_NO + "," +patt02.MNC_HN_YR + "," +patt02.MNC_PH_CD + "," +patt02.MNC_DATE + "," +
                patt02.MNC_PH_MEMO + "," +      patt02.MNC_PH_ALG_CD + "," +    patt02.MNC_EMP_CD + "," +   patt02.MNC_STAMP_DAT + "" +
                ") " +
                "Values ('" + p.MNC_HN_NO + "','" + p.MNC_HN_YR + "','" + p.MNC_PH_CD + "','" + p.MNC_DATE + "','" +
                p.MNC_PH_MEMO + "','" + p.MNC_PH_ALG_CD + "','" + p.MNC_EMP_CD + "',convert(varchar(20), getdate(), 23),replace(left(convert(varchar(100),getdate(),108),5),':',''))";
            try
            {
                re = conn.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + sql;
                new LogWriter("e", "PatientT02DB insert() " + sql);
                re = ex.Message;
            }
            return re;
        }
        public String update(PatientT02 p)
        {
            String re = "";
            String sql = "";
            sql = "Update " + patt02.table + " Set " +
                "" + patt02.MNC_HN_YR + "='" + p.MNC_HN_YR + "'," +
                "" + patt02.MNC_DATE + "='" + p.MNC_DATE + "'," +
                "" + patt02.MNC_PH_MEMO + "='" + p.MNC_PH_MEMO + "'," +
                "" + patt02.MNC_PH_ALG_CD + "='" + p.MNC_PH_ALG_CD + "'," +
                "" + patt02.MNC_EMP_CD + "='" + p.MNC_EMP_CD + "'," +
                //"" + patt02.MNC_STAMP_DAT + "='" + p.MNC_STAMP_DAT + "'," +
                //"" + patt02.MNC_STAMP_TIM + "='" + p.MNC_STAMP_TIM + "' " +
                "Where " + patt02.MNC_HN_NO + "='" + p.MNC_HN_NO + "' and " +
                "" + patt02.MNC_PH_CD + "='" + p.MNC_PH_CD + "' ";
            try
            {
                re = conn.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + sql;
                new LogWriter("e", "PatientT02DB update() " + sql);
                re = ex.Message;
            }
            return re;
        }
        public String insertPatientT02(PatientT02 p)
        {
            String re = "";
            DataTable dt = new DataTable();
            chkNull(p);
            dt = selectByPk(p.MNC_HN_NO, p.MNC_PH_CD);
            if (dt.Rows.Count > 0)
            {
                re = update(p);
            }
            else
            {
                re = insert(p);
            }
            return re;
        }
    }
}
