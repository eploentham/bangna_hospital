using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PatientT09DB
    {
        ConnectDB conn;
        PatientT09 pt09;
        public PatientT09DB(ConnectDB c) 
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pt09 = new PatientT09();
        }
        public DataTable SelectBypreno(String hn, String preno, String vsdate)
        {
            DataTable dt = new DataTable();

            String sql = "select convert(varchar(20),MNC_ACT_DAT,23), MNC_DOT_CD, MNC_DIA_CD, (convert(int, isnull(MNC_DIA_FLG,'1'))+1) as MNC_DIA_FLG" +
                ", MNC_DIA_BOD, MNC_DIA_STS, MNC_KW_CD, isnull(MNC_CAUSE_CD,'') as MNC_CAUSE_CD " +
                "From PATIENT_T09 ptt09  " +
                "Where MNC_HN_NO  = '"+ hn + "' and MNC_PRE_NO = '"+ preno + "' and MNC_DATE = '"+ vsdate + "' " +
                "Order By MNC_DIA_FLG ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
    }
}