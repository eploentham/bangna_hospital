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
    }
}
