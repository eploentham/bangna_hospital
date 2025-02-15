using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class DoctorM02DB
    {
        public DoctorM02 dtrM02;
        ConnectDB conn;
        public List<DoctorM02> ldtrM02;
        public DoctorM02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            dtrM02 = new DoctorM02();
            dtrM02.MNC_DF_CD = "MNC_DF_CD";
            dtrM02.MNC_DF_CTL_CD = "MNC_DF_CTL_CD";
            dtrM02.MNC_DF_DSC = "MNC_DF_DSC";
            dtrM02.MNC_DF_AMT = "MNC_DF_AMT";
            dtrM02.MNC_DF_AMT_H = "MNC_DF_AMT_H";
            dtrM02.MNC_DF_AMT_L = "MNC_DF_AMT_L";
            dtrM02.MNC_SHRP_I = "MNC_SHRP_I";
            dtrM02.MNC_SHRP_O = "MNC_SHRP_O";
            dtrM02.MNC_SHRC_I = "MNC_SHRC_I";
            dtrM02.MNC_SHRC_O = "MNC_SHRC_O";
            dtrM02.MNC_FN_CD = "MNC_FN_CD";
            dtrM02.MNC_DF_COS = "MNC_DF_COS";
            dtrM02.MNC_DF_FLG = "MNC_DF_FLG";
            dtrM02.MNC_DF_STS = "MNC_DF_STS";
            dtrM02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            dtrM02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            dtrM02.MNC_USR_ADD = "MNC_USR_ADD";
            dtrM02.MNC_USR_UPD = "MNC_USR_UPD";
            

            dtrM02.table = "DOCTOR_m02";
            dtrM02.pkField = "";
            ldtrM02 = new List<DoctorM02>();
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select dtrM02.* " +
                "From  " + dtrM02.table + " dtrM02 ";
            //" Where pm08.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public void getlCus()
        {
            //lDept = new List<Position>();

            ldtrM02.Clear();
            DataTable dt = new DataTable();
            dt = selectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                DoctorM02 cus1 = new DoctorM02();
                cus1.MNC_DF_CD = row[dtrM02.MNC_DF_CD].ToString();
                cus1.MNC_DF_CTL_CD = row[dtrM02.MNC_DF_CTL_CD].ToString();
                cus1.MNC_DF_DSC = row[dtrM02.MNC_DF_DSC].ToString();
                cus1.MNC_DF_AMT = row[dtrM02.MNC_DF_AMT].ToString();
                cus1.MNC_FN_CD = row[dtrM02.MNC_FN_CD].ToString();
                cus1.MNC_DF_FLG = row[dtrM02.MNC_DF_FLG].ToString();
                cus1.MNC_DF_STS = row[dtrM02.MNC_DF_STS].ToString();

                ldtrM02.Add(cus1);
            }
        }
    }
}
