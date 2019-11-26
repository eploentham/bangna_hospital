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
        }
    }
}
