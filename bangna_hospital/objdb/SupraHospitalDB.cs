using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class SupraHospitalDB
    {
        ConnectDB conn;
        SupraHospital suprahosp;
        public SupraHospitalDB(ConnectDB c) 
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            suprahosp = new SupraHospital();
            suprahosp.hosp_id = "hosp_id";
            suprahosp.hosp_code = "hosp_code";
            suprahosp.hosp_name_t = "hosp_name_t";
            suprahosp.addr1 = "addr1";
            suprahosp.addr2 = "addr2";
            suprahosp.tele = "tele";
            suprahosp.email = "email";
            suprahosp.tax_id = "tax_id";
            suprahosp.contact_name1 = "contact_name1";
            suprahosp.contact_name2 = "contact_name2";

            suprahosp.contact_tel1 = "contact_tel1";
            suprahosp.contact_tel2 = "contact_tel2";
            suprahosp.effectiveTime = "effectiveTime";
            suprahosp.active = "active";
            suprahosp.remark = "remark";
            suprahosp.date_cancel = "date_cancel";
            suprahosp.date_create = "date_create";
            suprahosp.date_modi = "date_modi";
        }
    }
}
