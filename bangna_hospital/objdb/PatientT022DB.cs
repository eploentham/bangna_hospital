using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    internal class PatientT022DB
    {
        public PatientT022 ptt022;
        ConnectDB conn;
        public PatientT022DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            ptt022 = new PatientT022();
            ptt022.MNC_HN_NO = "MNC_HN_NO";
            ptt022.MNC_HN_YR = "MNC_HN_YR";
            ptt022.MNC_PH_GRP_CD = "MNC_PH_GRP_CD";
            ptt022.MNC_PH_MEMO = "MNC_PH_MEMO";
            ptt022.MNC_PH_ALG_CD = "MNC_PH_ALG_CD";
            ptt022.MNC_EMP_CD = "MNC_EMP_CD";
            ptt022.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            ptt022.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            ptt022.table = "PATIENT_T022";
        }
        public DataTable selectByPk(String hn, String pharcode)
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + ptt022.table + " " +
                "Where " + ptt022.MNC_HN_NO + "='" + hn + "' and " + ptt022.MNC_PH_GRP_CD + "='" + pharcode + "' ";
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectByHn(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + ptt022.table + " " +
                "Where " + ptt022.MNC_HN_NO + "='" + hn + "' ";
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + ptt022.table + " ";
            dt = conn.selectData(sql);
            return dt;
        }
        public void chkNull(PatientT022 p)
        {
            long chk = 0;
            decimal chk1 = 0;
            DateTime chkdate = new DateTime();
            if (long.TryParse(p.MNC_HN_NO, out chk) == false) { p.MNC_HN_NO = "0"; }
            if (long.TryParse(p.MNC_HN_YR, out chk) == false) { p.MNC_HN_YR = "0"; }
            if (long.TryParse(p.MNC_PH_GRP_CD, out chk) == false) { p.MNC_PH_GRP_CD = "0"; }
            if (p.MNC_PH_MEMO == null) { p.MNC_PH_MEMO = ""; }
            if (p.MNC_PH_ALG_CD == null) { p.MNC_PH_ALG_CD = ""; }
            if (p.MNC_EMP_CD == null) { p.MNC_EMP_CD = ""; }
            if (DateTime.TryParse(p.MNC_STAMP_DAT, out chkdate) == false) { p.MNC_STAMP_DAT = System.DateTime.Now.ToString("yyyy-MM-dd"); }
            if (p.MNC_STAMP_TIM == null) { p.MNC_STAMP_TIM = ""; }
        }
        public String insert(PatientT022 p)
        {
            String sql = "";
            chkNull(p);
            sql = "Insert Into " + ptt022.table + " (" + ptt022.MNC_HN_NO + "," + ptt022.MNC_HN_YR + "," +
                ptt022.MNC_PH_GRP_CD + "," + ptt022.MNC_PH_MEMO + "," + ptt022.MNC_PH_ALG_CD + "," +
                ptt022.MNC_EMP_CD + "," + ptt022.MNC_STAMP_DAT + "," + ptt022.MNC_STAMP_TIM + ") " +
                "Values ('" + p.MNC_HN_NO + "','" + p.MNC_HN_YR + "','" +
                p.MNC_PH_GRP_CD + "','" + p.MNC_PH_MEMO.Replace("'", "''") + "','" + p.MNC_PH_ALG_CD.Replace("'", "''") + "','" +
                p.MNC_EMP_CD + "',convert(varchar(20), getdate(), 23),replace(left(convert(varchar(100),getdate(),108),5),':','')))";
            //new LogWriter("d", "insert PatientT02DB sql " + sql);
            return sql;
        }
        public String update(PatientT022 p)
        {
            String sql = "";
            chkNull(p);
            sql = "Update " + ptt022.table + " Set " +
                "" + ptt022.MNC_PH_MEMO + "='" + p.MNC_PH_MEMO.Replace("'", "''") + "'," +
                "" + ptt022.MNC_PH_ALG_CD + "='" + p.MNC_PH_ALG_CD.Replace("'", "''") + "'," +
                "" + ptt022.MNC_EMP_CD + "='" + p.MNC_EMP_CD + "'," +
                "" + ptt022.MNC_STAMP_DAT + "='" + p.MNC_STAMP_DAT + "'," +
                "" + ptt022.MNC_STAMP_TIM + "='" + p.MNC_STAMP_TIM + "' " +
                "Where " + ptt022.MNC_HN_NO + "='" + p.MNC_HN_NO + "' and " + ptt022.MNC_PH_GRP_CD + "='" + p.MNC_PH_GRP_CD + "' ";
            //new LogWriter("d", "update PatientT02DB sql " + sql);
            return sql;
        }
        public String insertPatientT022(PatientT022 p)
        {
            String re = "";
            DataTable dt = new DataTable();
            dt = selectByPk(p.MNC_HN_NO, p.MNC_PH_GRP_CD);
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
