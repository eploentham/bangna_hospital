using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PatientM44DB
    {
        public PatientM44 pm44;
        ConnectDB conn;
        DataTable DTALL;
        public PatientM44DB(ConnectDB c) 
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm44 = new PatientM44();
            pm44.MNC_SR_GRP_CD = "MNC_SR_GRP_CD";
            pm44.MNC_SR_GRP_DSC = "MNC_SR_GRP_DSC";
            pm44.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pm44.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pm44.MNC_USR_ADD = "MNC_USR_ADD";
            pm44.MNC_USR_UPD = "MNC_USR_UPD";

            pm44.table = "PATIENT_M44";
            DTALL = new DataTable();
        }
        public DataTable SelectAll()
        {
            if (DTALL.Rows.Count <= 0)
            {
                String sql = "select * " +
                "From PATIENT_M44  ";
                DTALL = conn.selectData(sql);
                //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            }
            return DTALL;
        }
    }
}
