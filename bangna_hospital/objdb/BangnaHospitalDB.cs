using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class BangnaHospitalDB
    {
        ConnectDB conn;
        public StaffDB stfDB;
        public DocGroupScanDB dgsDB;
        public PatientDB pttDB;
        public DocScanDB dscDB;
        public DocGroupSubScanDB dgssDB;
        public VisitDB vsDB;
        public XrayDB xrDB;
        public ResOrderTabDB resoDB;
        public LabExDB labexDB;
        public ReportTabContentsDB rpttcDB;
        public LabOutDB laboDB;
        public BLabOutDB labbDB;
        public DocGroupFMDB dfmDB;
        public DrugDB drugDB;
        public LabDB labDB;
        public XrayT04DB xrt04DB;
        public OrDB orDB;
        public OperativeNoteDB operNoteDB;
        public TemporaryM01DB tem01DB;
        public TemporaryM02DB tem02DB;
        public PharmacyT01DB pharT01DB;
        public PharmacyT02DB pharT02DB;
        public XrayM01DB xrayM01DB;
        public XrayM02DB xrayM02DB;
        public XrayT01DB xrayT01DB;
        public XrayT02DB xrayT02DB;
        public LabM01DB labM01DB;
        public LabM02DB labM02DB;
        public LabT01DB labT01DB;
        public LabT02DB labT02DB;
        public FinanceM01DB finM01DB;
        public FinanceM02DB finM02DB;
        public PatientM32DB pttM32DB;
        public OPDCheckUPDB opdcDB;
        public PharmacyM01DB pharM01DB;
        public PharmacyM02DB pharM02DB;
        public PatientM30DB pttM30DB;
        public OPBKKdrugcatelogDB opbkkDrugCatDB;
        public DotDfDetailDB dotdfdDB;
        public PatientSmartcardDB pttscDB;
        public VaccineDB vaccDB;
        public QueueTypeDB queueTypeDB;
        public QueueDB queueDB;
        public LabCovidDetectedDB lcoviddDB;
        public BangnaHospitalDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            stfDB = new StaffDB(conn);
            dgsDB = new DocGroupScanDB(conn);
            pttDB = new PatientDB(conn);
            dscDB = new DocScanDB(conn);
            dgssDB = new DocGroupSubScanDB(conn);
            vsDB = new VisitDB(conn);
            xrDB = new XrayDB(conn);
            resoDB = new ResOrderTabDB(conn);
            labexDB = new LabExDB(conn);
            rpttcDB = new ReportTabContentsDB(conn);
            laboDB = new LabOutDB(conn);
            labbDB = new BLabOutDB(conn);
            dfmDB = new DocGroupFMDB(conn);
            drugDB = new DrugDB(conn);
            labDB = new LabDB(conn);
            xrt04DB = new XrayT04DB(conn);
            orDB = new OrDB(conn);
            operNoteDB = new OperativeNoteDB(conn);
            tem01DB = new TemporaryM01DB(conn);
            tem02DB = new TemporaryM02DB(conn);
            pharT01DB = new PharmacyT01DB(conn);
            pharT02DB = new PharmacyT02DB(conn);
            xrayM01DB = new XrayM01DB(conn);
            xrayM02DB = new XrayM02DB(conn);
            xrayT01DB = new XrayT01DB(conn);
            xrayT02DB = new XrayT02DB(conn);
            labT01DB = new LabT01DB(conn);
            labT02DB = new LabT02DB(conn);
            finM01DB = new FinanceM01DB(conn);
            finM02DB = new FinanceM02DB(conn);
            pttM32DB = new PatientM32DB(conn);
            opdcDB = new OPDCheckUPDB(conn);
            pharM01DB = new PharmacyM01DB(conn);
            pharM02DB = new PharmacyM02DB(conn);
            pttM30DB = new PatientM30DB(conn);
            labM01DB = new LabM01DB(conn);
            labM02DB = new LabM02DB(conn);
            opbkkDrugCatDB = new OPBKKdrugcatelogDB(conn);
            dotdfdDB = new DotDfDetailDB(conn);
            pttscDB = new PatientSmartcardDB(conn);
            vaccDB = new VaccineDB(conn);
            queueTypeDB = new QueueTypeDB(conn);
            queueDB = new QueueDB(conn);
            lcoviddDB = new LabCovidDetectedDB(conn);
        }
        public String insertLogPage(String userid, String form, String method, String desc)
        {
            String re = "", sql = "";
            //sql = "Insert into log_page set " +
            //    "log_page_user_id = '" + userid + "'" +
            //    ",log_page_desc = '" + desc + "'" +
            //    ",log_page_id_address = '" + conn._IPAddress + "'" +
            //    ",page_form = '" + form + "'" +
            //    ",page_form_method = '" + method + "'" +
            //    ",date_create = now()" +
            //    ",user_create = CURRENT_USER()" +
            //    " " +
            //    "";
            sql = "Insert into t_log_page (" +
                "log_page_user_id, log_page_desc,log_page_id_address,page_form,page_form_method,date_create,user_create) " +
                "Values(" +
                "'"+userid+"','"+desc+"','"+conn._IPAddress+"','"+form+"','"+method+ "',convert(varchar(20),getdate(),20),'')";
            conn.ExecuteNonQueryLogPage(conn.connLog, sql);
            return re;
        }
        public String InsertPrakunM01()
        {
            String sql = "", cnt="";
            DataTable dt = new DataTable();
            sql = "Delete from prakun_m01_pop_temp;";
            conn.ExecuteNonQueryLogPage(conn.connMainHIS, sql);
            sql = "insert into prakun_m01_pop_temp (SocialID, Social_Card_no,TitleName,FirstName, LastName,FullName, PrakanCode, Prangnant, StartDate,EndDate,BirthDay,UploadDate,FLAG   ) " +
                "Select SocialID, Social_Card_no,TitleName,FirstName, LastName,FullName, PrakanCode, Prangnant, StartDate,EndDate,BirthDay,UploadDate,FLAG From prakun_m01_pop ";
            conn.ExecuteNonQueryLogPage(conn.connMainHIS, sql);
            sql = "Select count(1) as cnt from prakun_m01_pop_temp";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                cnt = dt.Rows[0]["cnt"].ToString();
            }
            return cnt;
        }
        public void BulkInsert(DataTable dt, String flag)
        {
            String sql = "";
            if (dt == null) return;
            if (dt.Rows.Count <= 0) return;
            using (SqlConnection connection = new SqlConnection(conn.connMainHIS.ConnectionString))
            {
                // make sure to enable triggers
                // more on triggers in next post
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = string.Format("SET DATEFORMAT YMD; {0}", cmd.CommandText);
                SqlBulkCopy bulkCopy =
                    new SqlBulkCopy
                    (
                    connection,
                    SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction,
                    null
                    );
                // set the destination table name
                if (flag.Equals("1"))
                {
                    //sql = "Delete from b_ssn_data_1";
                    sql = "Delete from prakun_m01_pop";
                    conn.ExecuteNonQueryLogPage(conn.connMainHIS, sql);
                    //bulkCopy.DestinationTableName = "b_ssn_data_1";
                    bulkCopy.DestinationTableName = "prakun_m01_pop";
                }
                else if (flag.Equals("2"))
                {
                    bulkCopy.DestinationTableName = "prakun_m01_pop";
                }
                else if (flag.Equals("5"))
                {
                    bulkCopy.DestinationTableName = "prakun_m01_pop";
                }
                connection.Open();
                // write the data in the "dataTable"
                bulkCopy.WriteToServer(dt);
                connection.Close();
            }
            // reset
            //this.dataTable.Clear();
        }
    }
}
