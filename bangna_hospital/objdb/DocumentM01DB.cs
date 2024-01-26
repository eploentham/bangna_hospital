using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class DocumentM01DB
    {
        public DocumentM01 docM01;
        ConnectDB conn;
        public DocumentM01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            docM01 = new DocumentM01();
            docM01.MNC_MOD_CD = "MNC_MOD_CD";
            docM01.MNC_DOC_CD = "MNC_DOC_CD";
            docM01.MNC_DOC_YR = "MNC_DOC_YR";
            docM01.MNC_DOC_DSC = "MNC_DOC_DSC";
            docM01.MNC_DOC_NO = "MNC_DOC_NO";
            docM01.MNC_DOC_STS = "MNC_DOC_STS";

            docM01.table = "Document_M01";
        }
        public String genDocAppointment()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "", year="", chk = "";
            year = (DateTime.Now.Year + 543).ToString();
            sql = "Insert into "+ docM01.table+"( "+
                ""+ docM01.MNC_MOD_CD+","+ docM01.MNC_DOC_CD+","+ docM01.MNC_DOC_YR+
                ","+ docM01.MNC_DOC_DSC+","+ docM01.MNC_DOC_NO+" "+
                "Values('OPD','APP', '" + year+ "','ใบนัด','1'"+
                ")"
                ;
            sql = "Update " + docM01.table + " Set " +
                " " + docM01.MNC_DOC_NO + "" +
                " Where " + docM01.MNC_MOD_CD + " and " + docM01.MNC_DOC_CD + " and " + docM01.MNC_DOC_YR + "='" + year + "' ";
            chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            sql = "Select " + docM01.MNC_DOC_NO + " From " + docM01.table + " Where " + docM01.MNC_MOD_CD + " and " + docM01.MNC_DOC_CD + " and " + docM01.MNC_DOC_YR + "='" + year + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if(dt.Rows.Count > 0)
            {
                re = dt.Rows[0][docM01.MNC_DOC_NO].ToString();
            }

            return re;
        }
    }
}
