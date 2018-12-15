using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class DocGroupScanDB
    {
        public DocGroupScan dgs;
        ConnectDB conn;
        public DocGroupScanDB(ConnectDB c)
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            dgs = new DocGroupScan();
            dgs.active = "active";
            dgs.doc_group_name = "doc_group_name";
            dgs.doc_group_id = "doc_group_id";
            dgs.remark = "remark";

            dgs.table = "doc_group_scan";
            dgs.pkField = "doc_group_id";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From dgs."+ dgs.table +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dgs." + dgs.active + " ='1' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            
            return dt;
        }
        public DocGroupScan selectByPk(String id)
        {
            DocGroupScan cop1 = new DocGroupScan();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From dgs." + dgs.table +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dgs." + dgs.pkField + " ='" + id + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            cop1 = setDocGroupScan(dt);
            return cop1;
        }
        public DataTable selectByPk1(String id)
        {
            Staff cop1 = new Staff();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From dgs." + dgs.table +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dgs." + dgs.pkField + " ='"+id+"' ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DocGroupScan setDocGroupScan(DataTable dt)
        {
            DocGroupScan dgs1 = new DocGroupScan();
            if (dt.Rows.Count > 0)
            {
                dgs1.doc_group_id = dt.Rows[0][dgs.doc_group_id].ToString();
                dgs1.doc_group_name = dt.Rows[0][dgs.doc_group_name].ToString();
                dgs1.remark = dt.Rows[0][dgs.remark].ToString();
                dgs1.active = dt.Rows[0][dgs.active].ToString();
            }
            else
            {
                setDocGroupScan(dgs1);
            }
                return dgs1;
        }
        public DocGroupScan setDocGroupScan(DocGroupScan dgs1)
        {
            dgs1.active = "";
            dgs1.remark = "";
            dgs1.doc_group_name = "";
            dgs1.doc_group_id = "";
            return dgs1;
        }
    }
}
