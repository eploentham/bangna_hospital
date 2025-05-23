﻿using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.OneD;

namespace bangna_hospital.objdb
{
    public class BangnaHospitalDB
    {
        ConnectDB conn;
        public StaffDB stfDB;
        public DocGroupScanDB dgsDB;
        
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
        public PharmacyT05DB pharT05DB;
        public PharmacyT06DB pharT06DB;
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
        public OPDCheckUPDB opdcDB;
        public PharmacyM01DB pharM01DB;
        public PharmacyM02DB pharM02DB;
        
        public OPBKKdrugcatelogDB opbkkDrugCatDB;
        public DotDfDetailDB dfdDB;
        public PatientSmartcardDB pttscDB;
        public VaccineDB vaccDB;
        public QueueTypeDB queueTypeDB;
        public QueueDB queueDB;
        public LabCovidDetectedDB lcoviddDB;
        public PatientDB pttDB;         //PatientM01DB
        public PatientM02DB pm02DB;
        public PatientM03DB pm03DB;
        public PatientM04DB pm04DB;
        public PatientM05DB pm05DB;
        public PatientM06DB pm06DB;
        public PatientM07DB pm07DB;
        public PatientM08DB pm08DB;
        public PatientM09DB pm09DB;
        public PatientM13DB pm13DB;
        public PatientM24DB pm24DB;
        public PatientM26DB pm26DB;     //Doctor
        public PatientM32DB pm32DB;
        public PatientM30DB pm30DB;
        public PatientM30DB pttM30DB;
        public PatientM39DB pm39DB;
        public PatientM40DB pm40DB;
        public PatientM44DB pm44DB;
        public PatientHIDB ptthiDB;
        public PatientT16DB pt16DB;
        public PatientT013DB pt013DB;
        public PrakunM01DB prakM01DB;
        public FLocationDB flocaDB;
        public AipnDB aipnDB;
        public LisLabResultDB labresDB;
        public LisLabRequestDB labreqDB;
        public LabT05DB labT05DB;
        public MedicalCertificateDB mcertiDB;
        public FinanceT01DB fint01DB;
        public PatientT07DB pt07DB;
        public PatientT08DB pt08DB;
        public PatientT09DB pt09DB;
        public PatientT03DB pt03DB;
        
        public SummaryT03DB sumt03DB;
        
        public LabM04DB labM04DB;
        public LabM06DB labm06DB;
        public LabM07DB labm07DB;
        public XrayM05DB xraym05DB;
        
        public PharmacyM14DB pharm14DB;
        public DrugSetDB drugSetDB;
        public REQDETAILDEPTDB reqdDB;
        public REQHEADDEPTDB reqhDB;
        public PharmacyEndMonthDB endyearDB;        // แก้ให้เป็น endyear จะได้ทำ ปีละครั้ง
        public SupraHospitalDB supraHospDB;
        public SupraSupraDB supraDB;
        public TokenDB tokenDB;
        
        public DoctorM02DB dtrM02DB;
        public SSOPBillItemsDB ssopBillItemsDB;
        public SSOPBillTranDB sSOPBillTranDB;
        public SSOPDispenseditemDB sSOPDIDB;
        public SSOPDispensingDB sSOPDDB;
        public SSOPOpdxDB sSOPODDB;
        public SSOPOpservicesDB sSOPOSDB;

        public BangnaHospitalDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            String err = "00";
            try
            {
                err = "00";
                //new LogWriter("e", "BangnaHospitalDB initConfig err " + err);
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
                err = "01";
                //new LogWriter("e", "BangnaHospitalDB initConfig err 01");
                operNoteDB = new OperativeNoteDB(conn);
                tem01DB = new TemporaryM01DB(conn);
                tem02DB = new TemporaryM02DB(conn);
                pharT01DB = new PharmacyT01DB(conn);
                pharT02DB = new PharmacyT02DB(conn);
                pharT05DB = new PharmacyT05DB(conn);
                pharT06DB = new PharmacyT06DB(conn);
                xrayM01DB = new XrayM01DB(conn);
                xrayM02DB = new XrayM02DB(conn);
                xrayT01DB = new XrayT01DB(conn);
                xrayT02DB = new XrayT02DB(conn);
                labT01DB = new LabT01DB(conn);
                labT02DB = new LabT02DB(conn);
                finM01DB = new FinanceM01DB(conn);
                finM02DB = new FinanceM02DB(conn);
                //pttM32DB = new PatientM32DB(conn);
                opdcDB = new OPDCheckUPDB(conn);
                pharM01DB = new PharmacyM01DB(conn);
                pharM02DB = new PharmacyM02DB(conn);
                pttM30DB = new PatientM30DB(conn);
                labM01DB = new LabM01DB(conn);
                labM02DB = new LabM02DB(conn);
                opbkkDrugCatDB = new OPBKKdrugcatelogDB(conn);
                dfdDB = new DotDfDetailDB(conn);
                err = "02";
                //new LogWriter("e", "BangnaHospitalDB initConfig err 02");
                pttscDB = new PatientSmartcardDB(conn);
                vaccDB = new VaccineDB(conn);
                queueTypeDB = new QueueTypeDB(conn);
                queueDB = new QueueDB(conn);
                err = "03";
                //new LogWriter("e", "BangnaHospitalDB initConfig err 03");
                lcoviddDB = new LabCovidDetectedDB(conn);
                pm09DB = new PatientM09DB(conn);
                pm08DB = new PatientM08DB(conn);
                pm07DB = new PatientM07DB(conn);
                pm07DB = new PatientM07DB(conn);
                pm05DB = new PatientM05DB(conn);
                pm06DB = new PatientM06DB(conn);
                pm04DB = new PatientM04DB(conn);
                err = "04";
                //new LogWriter("e", "BangnaHospitalDB initConfig err 04");
                pm03DB = new PatientM03DB(conn);
                pm02DB = new PatientM02DB(conn);
                err = "041";
                //new LogWriter("e", "BangnaHospitalDB initConfig err 041");
                pm24DB = new PatientM24DB(conn);
                pm30DB = new PatientM30DB(conn);
                err = "042";
                //new LogWriter("e", "BangnaHospitalDB initConfig err 042");
                pm32DB = new PatientM32DB(conn);
                ptthiDB = new PatientHIDB(conn);
                err = "043";
                //new LogWriter("e", "BangnaHospitalDB initConfig err 043");
                prakM01DB = new PrakunM01DB(conn);
                err = "044";
                flocaDB = new FLocationDB(conn);
                err = "05";
                aipnDB = new AipnDB(conn);
                //new LogWriter("e", "BangnaHospitalDB initConfig err 05");
                labresDB = new LisLabResultDB(conn);
                labreqDB = new LisLabRequestDB(conn);
                labT05DB = new LabT05DB(conn);
                mcertiDB = new MedicalCertificateDB(conn);
                fint01DB = new FinanceT01DB(conn);
                pt07DB = new PatientT07DB(conn);
                pt03DB = new PatientT03DB(conn);
                pm13DB = new PatientM13DB(conn);
                sumt03DB = new SummaryT03DB(conn);
                pt16DB = new PatientT16DB(conn);
                pt013DB = new PatientT013DB(conn);
                labm06DB = new LabM06DB(conn);
                xraym05DB = new XrayM05DB(conn);
                pm44DB = new PatientM44DB(conn);
                pharm14DB = new PharmacyM14DB(conn);
                drugSetDB = new DrugSetDB(conn);
                labm07DB = new LabM07DB(conn);
                labM04DB = new LabM04DB(conn);
                pt08DB = new PatientT08DB(conn);
                reqdDB = new REQDETAILDEPTDB(conn);
                reqhDB = new REQHEADDEPTDB(conn);
                endyearDB = new PharmacyEndMonthDB(conn);
                supraHospDB = new SupraHospitalDB(conn);
                supraDB = new SupraSupraDB(conn);
                tokenDB = new TokenDB(conn);
                pm39DB = new PatientM39DB(conn);
                pm40DB = new PatientM40DB(conn);
                dtrM02DB = new DoctorM02DB(conn);
                pm26DB = new PatientM26DB(conn);
                ssopBillItemsDB = new SSOPBillItemsDB(conn);
                sSOPBillTranDB = new SSOPBillTranDB(conn);
                sSOPDIDB = new SSOPDispenseditemDB(conn);
                sSOPDDB = new SSOPDispensingDB(conn);
                sSOPODDB = new SSOPOpdxDB(conn);
                sSOPOSDB = new SSOPOpservicesDB(conn);
                pt09DB = new PatientT09DB(conn);
            }
            catch(Exception ex)
            {
                new LogWriter("e", "BangnaHospitalDB initConfig err "+err+" "+ex.Message);
            }
        }
        public String insertLogPage(String userid, String form, String method, String desc)
        {
            String re = "", sql = "", desc1="";
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
            desc1 = desc.Length>500 ? desc1 = desc.Substring(0,500):desc;
            sql = "Insert into t_log_page (" +
                "log_page_user_id, log_page_desc,log_page_id_address,page_form,page_form_method,date_create,user_create) " +
                "Values(" +
                "'"+userid+"','"+desc1.Replace("'","''")+"','"+conn._IPAddress+"','"+form+"','"+method+ "',convert(varchar(20),getdate(),20),'')";
            re = conn.ExecuteNonQueryLogPage(conn.connLog, sql);
            return re;
        }
        public String InsertPrakunM01()
        {
            String sql = "", cnt="";
            DataTable dt = new DataTable();
            //sql = "Delete from prakun_m01_pop_temp;";
            sql = "Delete from prakun_m01;";
            conn.ExecuteNonQueryLogPage(conn.connMainHIS, sql);
            //sql = "insert into prakun_m01_pop_temp (SocialID, Social_Card_no,TitleName,FirstName, LastName,FullName, PrakanCode, Prangnant, StartDate,EndDate,BirthDay,UploadDate,FLAG   ) " +
            //    "Select SocialID, Social_Card_no,TitleName,FirstName, LastName,FullName, PrakanCode, Prangnant, StartDate,EndDate,BirthDay,UploadDate,FLAG From prakun_m01_pop ";
            sql = "insert into prakun_m01 (SocialID, Social_Card_no,TitleName,FirstName, LastName,FullName, PrakanCode, Prangnant, StartDate,EndDate,BirthDay,UploadDate,FLAG   ) " +
                "Select SocialID, Social_Card_no,TitleName,FirstName, LastName,FullName, PrakanCode, Prangnant, StartDate,EndDate,BirthDay,UploadDate,FLAG From prakun_m01_pop ";
            conn.ExecuteNonQueryLogPage(conn.connMainHIS, sql);
            //sql = "Select count(1) as cnt from prakun_m01_pop_temp";
            sql = "Select count(1) as cnt from prakun_m01";
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
        public void BulkInsertSsnData(DataTable dt, String flag, String yearid, String monthid, String periodid)
        {
            String sql = "";
            if (dt == null) return;
            if (dt.Rows.Count <= 0) return;
            dt.Columns.Add("year_id", typeof(System.String));
            dt.Columns.Add("month_id", typeof(System.String));
            dt.Columns.Add("period_id", typeof(System.String));
            dt.Columns.Add("status_ssn_data", typeof(System.String));
            foreach (DataRow drow in dt.Rows)
            {
                drow["year_id"] = yearid;
                drow["month_id"] = monthid;
                drow["period_id"] = periodid;
                drow["status_ssn_data"] = "0";
            }
            using (SqlConnection connection = new SqlConnection(conn.connSsnData.ConnectionString))
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
                    sql = "Delete from ssn_data_period Where year_id = '"+yearid+"' and month_id = '"+monthid+"' and period_id = '"+periodid+"' ";
                    conn.ExecuteNonQueryLogPage(conn.connSsnData, sql);
                    //bulkCopy.DestinationTableName = "b_ssn_data_1";
                    bulkCopy.DestinationTableName = "ssn_data_period";
                }
                else if (flag.Equals("2"))
                {
                    bulkCopy.DestinationTableName = "ssn_data_period";
                }
                else if (flag.Equals("5"))
                {
                    bulkCopy.DestinationTableName = "ssn_data_period";
                }
                connection.Open();
                // write the data in the "dataTable"
                bulkCopy.WriteToServer(dt);
                connection.Close();
            }
            // reset
            //this.dataTable.Clear();
        }
        public String MergeAN(String annosource, String annodesc, String remark)
        {
            String sql = "",re="", tablename="", tablenameold="";
            String[] annos = annosource.Split('.');
            String[] annod = annodesc.Split('.');
            if (annod.Length <=1) return "(annodesc.Length <=1)";
            if (annos.Length <= 1) return "(annosource.Length <= 1";
            tablename = "Patient_t01_2";
            sql = "Update "+ tablename + " Set " +
                "an_no_old = "+ annos[0] + "."+annos[1]+" " +
                ",remark = 'merge  " + annosource + "->"+ annodesc + " "+ remark.Replace("'","''")+"'" +
                ",date_modi = convert(varchar(20), getdate(),23) " +
                ",MNC_AN_YR = '" + annod[1] +"'"+
                ",MNC_AN_NO = '" + annod[0] +"' "+
                "Where MNC_AN_YR = '" + annos[1] + "' and MNC_AN_NO = '"+ annos[0] + "' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "Patient_t01_2";
                tablename = "Patient_t011";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "Patient_t011";
                tablename = "Patient_t03";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "Patient_t03";
                tablename = "Patient_t08";
                sql = sql.Replace(tablenameold, tablename);
                //re = conn.ExecuteNonQuery(conn.connMainHIS, sql);     ไม่ต้องแก้ไข เพราะ AN เป็น PK
                tablenameold = "Patient_t08";
                tablename = "Patient_t08_1";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "Patient_t08_1";
                tablename = "Patient_t08_2";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "Patient_t08_2";
                tablename = "Patient_t08_30";
                sql = sql.Replace(tablenameold, tablename);
                sql = sql.Replace("MNC_AN_YR", "MNC_YEAR");
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);

                tablenameold = "Patient_t08_30";
                tablename = "Patient_t09";
                sql = sql.Replace(tablenameold, tablename);
                sql = sql.Replace("MNC_YEAR", "MNC_AN_YR");
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "Patient_t09";
                tablename = "Patient_t12";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "Patient_t12";
                tablename = "Patient_t15";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "Patient_t15";
                tablename = "Patient_t18";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "Patient_t18";
                tablename = "Patient_t19";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "Patient_t19";
                tablename = "Patient_t28";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "Patient_t28";
                tablename = "Patient_t31";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "Patient_t31";
                tablename = "PHARMACY_T01";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "PHARMACY_T01";
                tablename = "PHARMACY_T05";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "PHARMACY_T05";
                tablename = "LAB_T01";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "LAB_T01";
                tablename = "FINANCE_T01";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "FINANCE_T01";
                tablename = "FINANCE_T03";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "FINANCE_T03";
                tablename = "FINANCE_T03_1";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "FINANCE_T03_1";
                tablename = "FINANCE_T06_DOT";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                tablenameold = "FINANCE_T06_DOT";
                tablename = "FINANCE_T14";
                sql = sql.Replace(tablenameold, tablename);
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                re = "update เรียบร้อย";
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                //new LogWriter("e", "voidDocScan " + sql);
                re = sql;
            }
            return re;
        }
    }
}
