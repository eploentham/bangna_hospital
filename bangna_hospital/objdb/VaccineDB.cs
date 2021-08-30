using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class VaccineDB
    {
        public Vaccine vaccine;
        ConnectDB conn;

        public VaccineDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            vaccine = new Vaccine();
            vaccine.reserve_vaccine_id= "reserve_vaccine_id";
            vaccine.pid= "pid";
            vaccine.firstname= "firstname";
            vaccine.lastname= "lastname";
            vaccine.address= "address";
            vaccine.email= "email";
            vaccine.mobile= "mobile";
            vaccine.disease= "disease";
            vaccine.vaccine= "vaccine";
            vaccine.dose= "dose";
            vaccine.payment= "payment";
            vaccine.active= "active";
            vaccine.date_create= "date_create";
            vaccine.drug_allergy= "drug_allergy";
            vaccine.status_payment= "status_payment";

            vaccine.table = "t_reserve_vaccine";
            vaccine.pkField = "reserve_vaccine_id";
        }

        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select * " +
                "From t_reserve_vaccine " +
                "Where active = '1' ";
            dt = conn.selectDataMySQL(conn.connMySQL, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public Vaccine SelectByPk(String id)
        {
            DataTable dt = new DataTable();
            Vaccine vacc = new Vaccine();
            String sql = "select * " +
                "From t_reserve_vaccine " +
                "Where reserve_vaccine_id ='"+id+"'";
            dt = conn.selectDataMySQL(conn.connMySQL, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            vacc = setVaccine(dt);
            return vacc;
        }
        public String updateStatusPayment(String labcode)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update t_reserve_vaccine Set " +
                    "status_payment = '1' " +
                    "Where reserve_vaccine_id = '" + labcode + "'";
                chk = conn.ExecuteNonQuery(conn.connMySQL, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " LabM01DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
        public String updateStatusPaymentVoid(String labcode)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update t_reserve_vaccine Set " +
                    "status_payment = '0' " +
                    "Where reserve_vaccine_id = '" + labcode + "'";
                chk = conn.ExecuteNonQuery(conn.connMySQL, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " LabM01DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
        public Vaccine setVaccine(DataTable dt)
        {
            Vaccine vacc = new Vaccine();
            if (dt.Rows.Count > 0)
            {
                vacc.reserve_vaccine_id = dt.Rows[0]["reserve_vaccine_id"].ToString();
                vacc.pid = dt.Rows[0]["pid"].ToString();
                vacc.firstname = dt.Rows[0]["firstname"].ToString();
                vacc.lastname = dt.Rows[0]["lastname"].ToString();
                vacc.address = dt.Rows[0]["address"].ToString();
                vacc.email = dt.Rows[0]["email"].ToString();
                vacc.mobile = dt.Rows[0]["mobile"].ToString();
                vacc.vaccine = dt.Rows[0]["vaccine"].ToString();
                vacc.dose = dt.Rows[0]["dose"].ToString();
                vacc.payment = dt.Rows[0]["payment"].ToString();
                vacc.active = dt.Rows[0]["active"].ToString();
                vacc.date_create = dt.Rows[0]["date_create"].ToString();
                vacc.drug_allergy = dt.Rows[0]["drug_allergy"].ToString();
                vacc.status_payment = dt.Rows[0]["status_payment"].ToString();
                vacc.disease = dt.Rows[0]["disease"].ToString();
            }
            else
            {
                setVaccine(vacc);
            }
            return vacc;
        }

        public Vaccine setVaccine(Vaccine p)
        {
            p.reserve_vaccine_id = "";
            p.pid = "";
            p.firstname = "";
            p.lastname = "";
            p.address = "";
            p.email = "";
            p.mobile = "";
            p.vaccine = "";
            p.dose = "";
            p.payment = "";
            p.active = "";
            p.date_create = "";
            p.drug_allergy = "";
            p.status_payment = "";
            p.disease = "";
            return p;
        }
    }
}
