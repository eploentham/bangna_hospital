using bangna_hospital.object1;
using System;
using System.Collections.Generic;
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
        }
    }
}
