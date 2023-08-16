using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class LabCovidDetectedDB
    {
        public LabCovidDetected lcovidd;
        ConnectDB conn;
        public LabCovidDetectedDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            lcovidd = new LabCovidDetected();
            lcovidd.lab_covid_detected_id = "lab_covid_detected_id";
            lcovidd.date_result = "date_result";
            lcovidd.hn = "hn";
            lcovidd.visit_date = "visit_date";
            lcovidd.pre_no = "pre_no";
            lcovidd.mnc_hn_no = "mnc_hn_no";
            lcovidd.active = "active";
            lcovidd.req_no = "req_no";
            lcovidd.mnc_req_yr = "mnc_req_yr";
            lcovidd.req_date = "req_date";
            lcovidd.lab_code = "lab_code";
            lcovidd.lab_sub_code = "lab_sub_code";
            lcovidd.result_value = "result_value";
            lcovidd.status_add_line = "status_add_line";
            lcovidd.status_report = "MNC_XRstatus_report_CD";
            lcovidd.status_novel = "status_novel";
            lcovidd.status_request_bed = "status_request_bed";
            lcovidd.status_admit = "status_admit";
            lcovidd.status_green_yellow_red_first = "status_green_yellow_red_first";
            lcovidd.result = "result";
            lcovidd.patient_smartcard_id = "patient_smartcard_id";
            lcovidd.date_create = "date_create";
            lcovidd.date_admit = "date_admit";
            lcovidd.ward_no = "ward_no";
            lcovidd.line_user_id = "line_user_id";
            lcovidd.pid = "pid";
            lcovidd.passport = "passport";
            lcovidd.novel_id = "novel_id";
            lcovidd.mobile = "mobile";

            lcovidd.hos_name = "hos_name";
            lcovidd.category = "category";
            lcovidd.sat_code = "sat_code";
            lcovidd.patient_fullname = "patient_fullname";
            lcovidd.sex = "sex";
            lcovidd.age_years = "age_years";
            lcovidd.nation_name = "nation_name";
            lcovidd.prov_name = "prov_name";
            lcovidd.amphur_name = "amphur_name";
            lcovidd.tumbon_name = "tumbon_name";
            lcovidd.addr_moo = "addr_moo";
            lcovidd.addr_home_no = "addr_home_no";
            lcovidd.disease = "disease";
            lcovidd.type_ptt = "type_ptt";
            lcovidd.cluster = "cluster";
            lcovidd.case_confirm = "case_confirm";
            lcovidd.place_doubt = "place_doubt";
            lcovidd.lab_date = "lab_date";
            lcovidd.lab_place = "lab_place";
            lcovidd.lab_result = "lab_result";
            lcovidd.e_gene = "e_gene";
            lcovidd.rdrp = "rdrp";
            lcovidd.n_gene = "n_gene";
            lcovidd.orf1ab = "orf1ab";
            lcovidd.ic = "ic";
            lcovidd.s_gene = "s_gene";
            lcovidd.rnp = "rnp";
            lcovidd.ns_gene = "ns_gene";

            lcovidd.table = "t_lab_covid_detected";
            lcovidd.pkField = "lab_covid_detected_id";
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select * " +
                "From t_lab_covid_detected  ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectByDateDetected(String dateresult, String hosname)
        {
            DataTable dt = new DataTable();
            String wherehosname = "";
            if (hosname.Length > 0)
            {
                hosname = " and hos_name = '" + hosname + "' ";
            }

            String sql = "select * " +
                "From t_lab_covid_detected  " +
                "Where visit_date >= '" + dateresult+ "' and visit_date <= '" + dateresult+ "'"+ hosname + " Order By date_create ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectByDateDetectedResultBangna5(String dateresult)
        {
            DataTable dt = new DataTable();

            String sql = "select * " +
                "From t_lab_covid_detected  " +
                "Where visit_date = dateadd(day,-1,'" + dateresult + "') and date_result = '" + dateresult + "' and hos_name = 'bangna5' Order By date_create ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectBystatusDEPIDEM()
        {
            DataTable dt = new DataTable();

            String sql = "Select lab_covid_detected_id, mnc_hn_no, hn, visit_date, pre_no, mnc_req_yr, req_date, lab_code, pid, passport, mobile "
                + ", hos_name, category, sat_code, patient_fullname, lab_date, lab_place, lab_result, e_gene, rdrp, n_gene, sex, nation_name, dob " +
                ", addr_home_no +' '+addr_moo as addr1 , tumbon_name, amphur_name, prov_name, first_name, last_name, prefix,MNC_LB_DSC "
                + "From t_lab_covid_detected " +
                "Left Join lab_m01 labm01 on t_lab_covid_detected.lab_code = labm01.MNC_LB_CD "
                + "Where t_lab_covid_detected.active = '1' and t_lab_covid_detected.status_epidem <> '1' ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public String updateStatusEpidemImported(String id)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update t_lab_covid_detected Set " +
                    "status_epidem ='1' " +
                    "Where lab_covid_detected_id ='" + id + "' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " XrayM01DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
        public String updateOPBKKCode(String xraycode, String opbkkcode)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update t_lab_covid_detected Set " +
                    "ucep_code ='" + opbkkcode + "' " +
                    "Where mnc_xr_cd ='" + xraycode + "' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " XrayM01DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
    }
}
