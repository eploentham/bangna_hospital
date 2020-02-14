using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class ReportTabContentsDB
    {
        public ReportTabContents rpttc;
        ConnectDB conn;
        public ReportTabContentsDB(ConnectDB c)
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            rpttc = new ReportTabContents();
            rpttc.StudyKey = "StudyKey";
            rpttc.HistNo = "HistNo";
            rpttc.RecNo = "RecNo";
            rpttc.Interpretation = "Interpretation";
            rpttc.Conclusion = "Conclusion";
            rpttc.HospitalID = "HospitalID";

            rpttc.AccessNumber = "AccessNumber";

            rpttc.table = "ReportTab_Contents";
            rpttc.pkField = "StudyKey";
        }
        public DataTable selectResultByAccessNumber(String acc)
        {
            ResOrderTab cop1 = new ResOrderTab();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + rpttc.table + " rpttc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where rpttc." + rpttc.AccessNumber + " ='" + acc + "' " +
                " ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public DataTable selectResultByHn(String acc)
        {
            //ResOrderTab cop1 = new ResOrderTab();
            DataTable dt = new DataTable();
            String sql = "Select rpttc.*,ReportTab.pid,ReportTab.insertername, ReportTab.studydesc,ReportTab.pkname,ReportTab.studydate,ReportTab.confirmdate, reporttab.accessnum  " +
                "From " + rpttc.table + " rpttc " +
                "inner join ReportTab on rpttc.StudyKey = ReportTab.StudyKey and rpttc.HistNo = ReportTab.HistNo " +
                "Where ReportTab.PID in (" + acc + ") " +
                "Order By ReportTab.studydate, ReportTab.studytime ";
            dt = conn.selectData(conn.connPACs, sql);

            return dt;
        }
    }
}
