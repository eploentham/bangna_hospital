using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class BLabOutDB
    {
        public ConnectDB conn;
        public BLabOut labB;

        public BLabOutDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labB = new BLabOut();
            labB.active = "active";
            labB.lab_code = "lab_code";
            labB.lab_id = "lab_id";
            labB.lab_name = "lab_name";
            labB.period_result = "period_result";
            labB.out_lab_comp_code = "out_lab_comp_code";
            labB.out_lab_price = "out_lab_price";

            labB.table = "b_lab_out";
            labB.pkField = "lab_id";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select labB.* " +
                "From " + labB.table + " labB " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where labB." + labB.active + "='1'  " +
                "Order By labB. " + labB.lab_id;
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
    }
}
