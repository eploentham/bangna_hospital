using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class SupraSupraDB
    {
        ConnectDB conn;
        SupraSupra supra;
        public SupraSupraDB(ConnectDB c) 
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            supra = new SupraSupra();
            supra.supra_id = "supra_id";
            supra.supra_doc = "supra_doc";
            supra.date_supra = "date_supra";
            supra.date_input = "date_input";
            supra.branch_code = "branch_code";
            supra.hosp_id = "hosp_id";
            supra.hn = "hn";
            supra.pid = "pid";
            supra.patient_fullname = "patient_fullname";
            supra.dob = "dob";

            supra.dtr_name = "dtr_name";
            supra.dtr_code = "dtr_code";
            supra.supra_type_id = "supra_type_id";
            supra.contact_name_hosp = "contact_name_hosp";
            supra.contact_tele_hosp = "contact_tele_hosp";
            supra.patient_contact_name = "patient_contact_name";
            supra.patient_contact_tele = "patient_contact_tele";
            supra.forcast = "forcast";
            supra.paid = "paid";
            supra.discount = "discount";

            supra.status_car = "status_car";
            supra.diseased1 = "diseased1";
            supra.diseased2 = "diseased2";
            supra.diseased3 = "diseased3";
            supra.diseased4 = "diseased4";
            supra.drg = "drg";
            supra.on_top = "on_top";
            supra.status_travel = "status_travel";
            supra.year_id = "year_id";
            supra.month_id = "month_id";

        }
    }
}
