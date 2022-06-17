using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PatientDB
    {
        public ConnectDB conn;
        Patient ptt;
        public List<PatientM32> lDeptOPD;
        public List<PatientM32> lDeptIPD;
        public PatientDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            ptt = new Patient();
            
            ptt.Age = "";
            ptt.Hn = "";
            ptt.Name = "";

            ptt.MNC_HN_NO  = "MNC_HN_NO";
            ptt.MNC_HN_YR = "MNC_HN_YR";
            ptt.MNC_PFIX_CDT = "MNC_PFIX_CDT";
            ptt.MNC_PFIX_CDE = "MNC_PFIX_CDE";
            ptt.MNC_FNAME_T  = "MNC_FNAME_T";
            ptt.MNC_LNAME_T = "MNC_LNAME_T";
            ptt.MNC_FNAME_E = "MNC_FNAME_E";
            ptt.MNC_LNAME_E = "MNC_LNAME_E";
            ptt.MNC_AGE  = "MNC_AGE";
            ptt.MNC_BDAY = "MNC_BDAY";
            ptt.MNC_ID_NO = "MNC_ID_NO";
            ptt.MNC_SS_NO  = "MNC_SS_NO";
            ptt.MNC_SEX = "MNC_SEX";
            ptt.MNC_FULL_ADD  = "MNC_FULL_ADD";
            ptt.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            ptt.MNC_STAMP_TIM  = "MNC_STAMP_TIM";

            ptt.MNC_NAT_CD = "MNC_NAT_CD";
            ptt.MNC_CUR_ADD = "MNC_CUR_ADD";
            ptt.MNC_CUR_TUM = "MNC_CUR_TUM";
            ptt.MNC_CUR_AMP = "MNC_CUR_AMP";
            ptt.MNC_CUR_CHW = "MNC_CUR_CHW";
            ptt.MNC_CUR_POC = "MNC_CUR_POC";
            ptt.MNC_CUR_TEL = "MNC_CUR_TEL";
            ptt.MNC_DOM_ADD = "MNC_DOM_ADD";
            ptt.MNC_DOM_TUM = "MNC_DOM_TUM";
            ptt.MNC_DOM_AMP = "MNC_DOM_AMP";
            ptt.MNC_DOM_CHW = "MNC_DOM_CHW";
            ptt.MNC_DOM_POC = "MNC_DOM_POC";
            ptt.MNC_DOM_TEL = "MNC_DOM_TEL";
            ptt.MNC_REF_NAME = "MNC_REF_NAME";
            ptt.MNC_REF_REL = "MNC_REF_REL";
            ptt.MNC_REF_ADD = "MNC_REF_ADD";
            ptt.MNC_REF_TUM = "MNC_REF_TUM";
            ptt.MNC_REF_AMP = "MNC_REF_AMP";
            ptt.MNC_REF_CHW = "MNC_REF_CHW";
            ptt.MNC_REF_POC = "MNC_REF_POC";
            ptt.MNC_REF_TEL = "MNC_REF_TEL";
            ptt.MNC_CUR_MOO = "MNC_CUR_MOO";
            ptt.MNC_DOM_MOO = "MNC_DOM_MOO";
            ptt.MNC_REF_MOO = "MNC_REF_MOO";
            ptt.MNC_REG_DAT = "MNC_REG_DAT";
            ptt.MNC_REG_TIM = "MNC_REG_TIM";
            ptt.MNC_CUR_SOI = "MNC_CUR_SOI";
            ptt.MNC_DOM_SOI = "MNC_DOM_SOI";
            ptt.MNC_REF_SOI = "MNC_REF_SOI";
            ptt.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            ptt.MNC_ATT_NOTE = "MNC_ATT_NOTE";
            //ptt.MNC_FULL_ADD = "MNC_FULL_SOI";
            ptt.MNC_OCC_CD = "MNC_OCC_CD";
            ptt.MNC_EDU_CD = "MNC_EDU_CD";
            ptt.MNC_REL_CD = "MNC_REL_CD";
            ptt.MNC_NATI_CD = "MNC_NATI_CD";
            ptt.MNC_CAR_CD = "MNC_CAR_CD";
            ptt.MNC_CUR_ROAD = "MNC_CUR_ROAD";
            ptt.MNC_COM_CD = "MNC_COM_CD";
            ptt.MNC_COM_CD2 = "MNC_COM_CD2";
            ptt.passport = "passport";

            ptt.pkField = "";
            ptt.table = "patient_m01";

            lDeptOPD = new List<PatientM32>();
            lDeptIPD = new List<PatientM32>();
        }
        public void getlDeptOPD()
        {
            //lDept = new List<Position>();

            lDeptOPD.Clear();
            DataTable dt = new DataTable();
            dt = selectDeptOPD();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                PatientM32 cus1 = new PatientM32();
                cus1.mnc_sec_no = row["mnc_sec_no"].ToString();
                cus1.mnc_md_dep_no = row["mnc_md_dep_no"].ToString();
                cus1.mnc_md_dep_dsc = row["mnc_md_dep_dsc"].ToString();
                lDeptOPD.Add(cus1);
            }
        }
        public void getlDeptIPD()
        {
            //lDept = new List<Position>();

            lDeptIPD.Clear();
            DataTable dt = new DataTable();
            dt = selectDeptIPD();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                PatientM32 cus1 = new PatientM32();
                cus1.mnc_sec_no = row["mnc_sec_no"].ToString();
                cus1.mnc_md_dep_no = row["mnc_md_dep_no"].ToString();
                cus1.mnc_md_dep_dsc = row["mnc_md_dep_dsc"].ToString();
                lDeptIPD.Add(cus1);
            }
        }
        public String selectHnMax()
        {
            DataTable dt = new DataTable();
            String sql = "",re="";
            sql = "Select max(m01.mnc_hn_no) as hn_no " +
                "From  patient_m01 m01 " ;
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["hn_no"].ToString();
            }
            return re;
        }
        public DataTable selectPatient(String hn, String name)
        {
            DataTable dt = new DataTable();
            String sql = "", wherehn="", wherename="";
            if (hn.Length > 0)
            {
                wherehn = " m01.MNC_HN_NO like '%"+hn+"%'";
            }
            if (name.Length > 0)
            {
                wherename = " or m01.MNC_FNAME_T like '%" + name + "%' or m01.MNC_LNAME_T like '%" + name + "%' ";
            }
            if(hn.Equals("") && name.Equals(""))
            {
                wherehn = "m01.MNC_HN_NO='999' ";
            }
            sql = "Select m01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE, m01.mnc_hn_yr " +
                "From  patient_m01 m01 " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where  "+ wherehn + wherename;
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public DataTable selectPatientPic(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "", wherehn = "", wherename = "";
            
            sql = "Select * " +
                "From  patient_img  " +
                " Where mnc_hn_no = '" +hn+"'";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public Patient selectPatient(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select m01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,m01.MNC_bday, m01.mnc_id_no, m01.mnc_hn_yr " +
                "From  patient_m01 m01 " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where m01.MNC_HN_NO = '" + hn + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                ptt.Hn = dt.Rows[0]["MNC_HN_NO"].ToString();
                ptt.Age = dt.Rows[0]["MNC_AGE"].ToString();
                ptt.patient_birthday = dt.Rows[0]["MNC_bday"].ToString();
                ptt.Name = dt.Rows[0]["prefix"].ToString() + " " + dt.Rows[0]["MNC_FNAME_T"].ToString() + " " + dt.Rows[0]["MNC_LNAME_T"].ToString();
                ptt.idcard = dt.Rows[0]["mnc_id_no"].ToString();
                ptt.hnyr = dt.Rows[0]["mnc_hn_yr"].ToString();
            }
            return ptt;
        }
        public DataTable selectPatinetBySearch(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            Patient ptt = new Patient();
            sql = "Select m01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix,m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_FNAME_E,m01.MNC_LNAME_E " +
                ",MNC_AGE,convert(VARCHAR(20),m01.MNC_bday,23) as MNC_bday, m01.mnc_id_no, m01.mnc_hn_yr" +
                ",m01.MNC_PFIX_CDT,m01.MNC_CUR_TEL,m01.MNC_PFIX_CDE,  m02.MNC_PFIX_DSC +' '+m01.MNC_FNAME_T+' '+m01.MNC_LNAME_T as pttfullname " +
                "From  patient_m01 m01 " +
                " left join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where m01.MNC_hn_NO like '" + hn + "%' or (m01.MNC_FNAME_T like '"+hn+ "%') or (m01.MNC_LNAME_T like '" + hn + "%') or (m01.mnc_id_no like '" + hn + "%') " +
                "Order By m01.mnc_hn_no desc";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public Patient selectPatinetByHn(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            Patient ptt = new Patient();
            sql = "Select m01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, m01.MNC_CUR_CHW, m01.MNC_CUR_AMP, m01.MNC_CUR_TUM, m01.MNC_CUR_ADD, m01.MNC_CUR_MOO, m01.MNC_CUR_SOI, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,MNC_AGE,convert(VARCHAR(20),m01.MNC_bday,23) as MNC_bday, m01.mnc_id_no, m01.mnc_hn_yr,m01.MNC_FNAME_E " +
                ",m01.MNC_LNAME_E,m01.MNC_PFIX_CDT,m01.MNC_CUR_TEL,m01.MNC_CUR_TEL,m01.MNC_PFIX_CDE,m01.MNC_ATT_NOTE " +
                ",m01.MNC_OCC_CD,m01.MNC_EDU_CD,m01.MNC_NAT_CD,m01.MNC_REL_CD,m01.MNC_NATI_CD,m01.MNC_OCC_CD,m01.MNC_CUR_ROAD, m01.passport,m01.MNC_SS_NO " +
                ", m01.MNC_dom_CHW, m01.MNC_dom_AMP, m01.MNC_dom_TUM, m01.MNC_dom_ADD, m01.MNC_dom_MOO, m01.MNC_dom_SOI,MNC_DOM_TEL " +
                "From  patient_m01 m01 " +
                " left join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where m01.MNC_hn_NO = '" + hn + "' " +
                "Order By m01.mnc_hn_no desc";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                ptt.Hn = dt.Rows[0]["MNC_HN_NO"].ToString();
                ptt.Age = dt.Rows[0]["MNC_AGE"].ToString();
                ptt.patient_birthday = dt.Rows[0]["MNC_bday"].ToString();
                ptt.Name = dt.Rows[0]["prefix"].ToString() + " " + dt.Rows[0]["MNC_FNAME_T"].ToString() + " " + dt.Rows[0]["MNC_LNAME_T"].ToString();
                ptt.idcard = dt.Rows[0]["mnc_id_no"].ToString();
                ptt.hnyr = dt.Rows[0]["mnc_hn_yr"].ToString();
                ptt.MNC_HN_NO = dt.Rows[0]["MNC_HN_NO"].ToString();
                ptt.MNC_HN_YR = dt.Rows[0]["mnc_hn_yr"].ToString();
                ptt.MNC_CUR_CHW = dt.Rows[0]["MNC_CUR_CHW"].ToString();
                ptt.MNC_CUR_AMP = dt.Rows[0]["MNC_CUR_AMP"].ToString();
                ptt.MNC_CUR_TUM = dt.Rows[0]["MNC_CUR_TUM"].ToString();
                ptt.MNC_CUR_ADD = dt.Rows[0]["MNC_CUR_ADD"].ToString();
                ptt.MNC_CUR_MOO = dt.Rows[0]["MNC_CUR_MOO"].ToString();
                ptt.MNC_CUR_SOI = dt.Rows[0]["MNC_CUR_SOI"].ToString();
                ptt.MNC_FNAME_T = dt.Rows[0]["MNC_FNAME_T"].ToString();
                ptt.MNC_LNAME_T = dt.Rows[0]["MNC_LNAME_T"].ToString();
                ptt.MNC_FNAME_E = dt.Rows[0]["MNC_FNAME_E"].ToString();
                ptt.MNC_LNAME_E = dt.Rows[0]["MNC_LNAME_E"].ToString();
                ptt.MNC_PFIX_CDT = dt.Rows[0]["MNC_PFIX_CDT"].ToString();
                ptt.MNC_CUR_TEL = dt.Rows[0]["MNC_CUR_TEL"].ToString();
                ptt.MNC_PFIX_CDE = dt.Rows[0]["MNC_PFIX_CDE"].ToString();
                ptt.MNC_ATT_NOTE = dt.Rows[0]["MNC_ATT_NOTE"].ToString();
                ptt.MNC_OCC_CD = dt.Rows[0]["MNC_OCC_CD"].ToString();
                ptt.MNC_EDU_CD = dt.Rows[0]["MNC_EDU_CD"].ToString();
                ptt.MNC_NAT_CD = dt.Rows[0]["MNC_NAT_CD"].ToString();
                ptt.MNC_REL_CD = dt.Rows[0]["MNC_REL_CD"].ToString();
                ptt.MNC_NATI_CD = dt.Rows[0]["MNC_NATI_CD"].ToString();
                ptt.MNC_CUR_ROAD = dt.Rows[0]["MNC_CUR_ROAD"].ToString();
                ptt.MNC_BDAY = dt.Rows[0]["MNC_BDAY"].ToString();
                ptt.MNC_ID_NO = dt.Rows[0]["mnc_id_no"].ToString();
                ptt.passport = dt.Rows[0]["passport"].ToString();
                ptt.MNC_SS_NO = dt.Rows[0]["MNC_SS_NO"].ToString();

                ptt.MNC_DOM_CHW = dt.Rows[0]["MNC_DOM_CHW"].ToString();
                ptt.MNC_DOM_AMP = dt.Rows[0]["MNC_DOM_AMP"].ToString();
                ptt.MNC_DOM_TUM = dt.Rows[0]["MNC_DOM_TUM"].ToString();
                ptt.MNC_DOM_ADD = dt.Rows[0]["MNC_DOM_ADD"].ToString();
                ptt.MNC_DOM_MOO = dt.Rows[0]["MNC_DOM_MOO"].ToString();
                ptt.MNC_DOM_SOI = dt.Rows[0]["MNC_DOM_SOI"].ToString();
                ptt.MNC_DOM_TEL = dt.Rows[0]["MNC_DOM_TEL"].ToString();

            }
            else
            {
                ptt.MNC_HN_NO = "";
                ptt.MNC_HN_YR = "";
                ptt.MNC_PFIX_CDT = "";
                ptt.MNC_PFIX_CDE = "";
                ptt.MNC_FNAME_T = "";
                ptt.MNC_LNAME_T = "";
                ptt.MNC_FNAME_E = "";
                ptt.MNC_LNAME_E = "";
                ptt.MNC_AGE = "";
                ptt.MNC_BDAY = "";
                ptt.MNC_ID_NO = "";
                ptt.MNC_SS_NO = "";
                ptt.MNC_SEX = "";
                ptt.MNC_FULL_ADD = "";
                ptt.MNC_STAMP_DAT = "";
                ptt.MNC_STAMP_TIM = "";
                ptt.MNC_FNAME_T = "";
                ptt.MNC_LNAME_T = "";
                ptt.MNC_FNAME_E = "";
                ptt.MNC_LNAME_E = "";
                ptt.MNC_CUR_TEL = "";
                ptt.MNC_ATT_NOTE = "";
                ptt.MNC_OCC_CD = "";
                ptt.MNC_EDU_CD = "";
                ptt.MNC_NAT_CD = "";
                ptt.MNC_REL_CD = "";
                ptt.MNC_NATI_CD = "";
                ptt.MNC_CAR_CD = "";
                ptt.MNC_CUR_ROAD = "";
                ptt.passport = "";
            }
            return ptt;
        }
        public Patient selectPatinetByID1(String pid)
        {
            DataTable dt = new DataTable();
            String sql = "";
            Patient ptt = new Patient();
            sql = "Select m01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, m01.MNC_CUR_CHW, m01.MNC_CUR_AMP, m01.MNC_CUR_TUM, m01.MNC_CUR_ADD, m01.MNC_CUR_MOO, m01.MNC_CUR_SOI, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,MNC_AGE,convert(VARCHAR(20),m01.MNC_bday,23) as MNC_bday, m01.mnc_id_no, m01.mnc_hn_yr,m01.MNC_FNAME_E" +
                ",m01.MNC_LNAME_E,m01.MNC_PFIX_CDT,m01.MNC_CUR_TEL,m01.MNC_CUR_TEL,m01.MNC_PFIX_CDE,m01.MNC_ATT_NOTE " +
                ",m01.MNC_OCC_CD,m01.MNC_EDU_CD,m01.MNC_NAT_CD,m01.MNC_REL_CD,m01.MNC_NATI_CD,m01.MNC_OCC_CD,m01.MNC_CUR_ROAD " +
                "From  patient_m01 m01 " +
                " left join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where m01.MNC_ID_NO = '" + pid + "' " +
                "Order By m01.mnc_hn_no desc";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                ptt.Hn = dt.Rows[0]["MNC_HN_NO"].ToString();
                ptt.Age = dt.Rows[0]["MNC_AGE"].ToString();
                ptt.patient_birthday = dt.Rows[0]["MNC_bday"].ToString();
                ptt.Name = dt.Rows[0]["prefix"].ToString() + " " + dt.Rows[0]["MNC_FNAME_T"].ToString() + " " + dt.Rows[0]["MNC_LNAME_T"].ToString();
                ptt.idcard = dt.Rows[0]["mnc_id_no"].ToString();
                ptt.hnyr = dt.Rows[0]["mnc_hn_yr"].ToString();
                ptt.MNC_HN_NO = dt.Rows[0]["MNC_HN_NO"].ToString();
                ptt.MNC_HN_YR = dt.Rows[0]["mnc_hn_yr"].ToString();
                ptt.MNC_CUR_CHW = dt.Rows[0]["MNC_CUR_CHW"].ToString();
                ptt.MNC_CUR_AMP = dt.Rows[0]["MNC_CUR_AMP"].ToString();
                ptt.MNC_CUR_TUM = dt.Rows[0]["MNC_CUR_TUM"].ToString();
                ptt.MNC_CUR_ADD = dt.Rows[0]["MNC_CUR_ADD"].ToString();
                ptt.MNC_CUR_MOO = dt.Rows[0]["MNC_CUR_MOO"].ToString();
                ptt.MNC_CUR_SOI = dt.Rows[0]["MNC_CUR_SOI"].ToString();
                ptt.MNC_FNAME_T = dt.Rows[0]["MNC_FNAME_T"].ToString();
                ptt.MNC_LNAME_T = dt.Rows[0]["MNC_LNAME_T"].ToString();
                ptt.MNC_FNAME_E = dt.Rows[0]["MNC_FNAME_E"].ToString();
                ptt.MNC_LNAME_E = dt.Rows[0]["MNC_LNAME_E"].ToString();
                ptt.MNC_PFIX_CDT = dt.Rows[0]["MNC_PFIX_CDT"].ToString();
                ptt.MNC_CUR_TEL = dt.Rows[0]["MNC_CUR_TEL"].ToString();
                ptt.MNC_PFIX_CDE = dt.Rows[0]["MNC_PFIX_CDE"].ToString();
                ptt.MNC_ATT_NOTE = dt.Rows[0]["MNC_ATT_NOTE"].ToString();
                ptt.MNC_OCC_CD = dt.Rows[0]["MNC_OCC_CD"].ToString();
                ptt.MNC_EDU_CD = dt.Rows[0]["MNC_EDU_CD"].ToString();
                ptt.MNC_NAT_CD = dt.Rows[0]["MNC_NAT_CD"].ToString();
                ptt.MNC_REL_CD = dt.Rows[0]["MNC_REL_CD"].ToString();
                ptt.MNC_NATI_CD = dt.Rows[0]["MNC_NATI_CD"].ToString();
                ptt.MNC_CUR_ROAD = dt.Rows[0]["MNC_CUR_ROAD"].ToString();
                ptt.MNC_BDAY = dt.Rows[0]["MNC_BDAY"].ToString();
                ptt.MNC_ID_NO = dt.Rows[0]["mnc_id_no"].ToString();

            }
            else
            {
                ptt.MNC_HN_NO = "";
                ptt.MNC_HN_YR = "";
                ptt.MNC_PFIX_CDT = "";
                ptt.MNC_PFIX_CDE = "";
                ptt.MNC_FNAME_T = "";
                ptt.MNC_LNAME_T = "";
                ptt.MNC_FNAME_E = "";
                ptt.MNC_LNAME_E = "";
                ptt.MNC_AGE = "";
                ptt.MNC_BDAY = "";
                ptt.MNC_ID_NO = "";
                ptt.MNC_SS_NO = "";
                ptt.MNC_SEX = "";
                ptt.MNC_FULL_ADD = "";
                ptt.MNC_STAMP_DAT = "";
                ptt.MNC_STAMP_TIM = "";
                ptt.MNC_FNAME_T = "";
                ptt.MNC_LNAME_T = "";
                ptt.MNC_FNAME_E = "";
                ptt.MNC_LNAME_E = "";
                ptt.MNC_CUR_TEL = "";
                ptt.MNC_ATT_NOTE = "";
                ptt.MNC_OCC_CD = "";
                ptt.MNC_EDU_CD = "";
                ptt.MNC_NAT_CD = "";
                ptt.MNC_REL_CD = "";
                ptt.MNC_NATI_CD = "";
                ptt.MNC_CAR_CD = "";
                ptt.MNC_CUR_ROAD = "";
            }
            return ptt;
        }
        public Patient selectPatinetByPID(String pid, String flag)
        {
            DataTable dt = new DataTable();
            String sql = "", wherepid="";
            Patient ptt = new Patient();
            if (flag.Equals("pid"))
            {
                wherepid = " m01.mnc_id_no = '" + pid + "' ";
            }
            else if (flag.Equals("passport"))
            {
                wherepid = " m01.passport = '" + pid + "' ";
            }
            else
            {
                wherepid = " m01.passport = '" + pid + "' ";
            }
            sql = "Select m01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, m01.MNC_CUR_CHW, m01.MNC_CUR_AMP, m01.MNC_CUR_TUM, m01.MNC_CUR_ADD, m01.MNC_CUR_MOO, m01.MNC_CUR_SOI, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,MNC_AGE,convert(VARCHAR(20),m01.MNC_bday,23) as MNC_bday, m01.mnc_id_no, m01.mnc_hn_yr,m01.MNC_FNAME_E,m01.MNC_LNAME_E, m01.MNC_PFIX_CDT, m01.MNC_PFIX_CDE, m01.passport,m01.MNC_CUR_TEL,m01.MNC_ATT_NOTE " +
                "From  patient_m01 m01 " +
                " left join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where "+ wherepid + " Order By m01.mnc_hn_no desc";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                ptt.Hn = dt.Rows[0]["MNC_HN_NO"].ToString();
                ptt.Age = dt.Rows[0]["MNC_AGE"].ToString();
                ptt.patient_birthday = dt.Rows[0]["MNC_bday"].ToString();
                ptt.Name = dt.Rows[0]["prefix"].ToString() + " " + dt.Rows[0]["MNC_FNAME_T"].ToString() + " " + dt.Rows[0]["MNC_LNAME_T"].ToString();
                ptt.idcard = dt.Rows[0]["mnc_id_no"].ToString();
                ptt.hnyr = dt.Rows[0]["mnc_hn_yr"].ToString();
                ptt.MNC_HN_NO = dt.Rows[0]["MNC_HN_NO"].ToString();
                ptt.MNC_HN_YR = dt.Rows[0]["mnc_hn_yr"].ToString();
                ptt.MNC_CUR_CHW = dt.Rows[0]["MNC_CUR_CHW"].ToString();
                ptt.MNC_CUR_AMP = dt.Rows[0]["MNC_CUR_AMP"].ToString();
                ptt.MNC_CUR_TUM = dt.Rows[0]["MNC_CUR_TUM"].ToString();
                ptt.MNC_CUR_ADD = dt.Rows[0]["MNC_CUR_ADD"].ToString();
                ptt.MNC_CUR_MOO = dt.Rows[0]["MNC_CUR_MOO"].ToString();
                ptt.MNC_CUR_SOI = dt.Rows[0]["MNC_CUR_SOI"].ToString();
                ptt.MNC_ID_NO = dt.Rows[0]["mnc_id_no"].ToString();
                ptt.MNC_BDAY = dt.Rows[0]["MNC_BDAY"].ToString();
                ptt.MNC_FNAME_T = dt.Rows[0]["MNC_FNAME_T"].ToString();
                ptt.MNC_LNAME_T = dt.Rows[0]["MNC_LNAME_T"].ToString();
                ptt.MNC_FNAME_E = dt.Rows[0]["MNC_FNAME_E"].ToString();
                ptt.MNC_LNAME_E = dt.Rows[0]["MNC_LNAME_E"].ToString();
                ptt.MNC_PFIX_CDT = dt.Rows[0]["MNC_PFIX_CDT"].ToString();
                ptt.MNC_PFIX_CDE = dt.Rows[0]["MNC_PFIX_CDE"].ToString();
                ptt.passport = dt.Rows[0]["passport"].ToString();
                ptt.MNC_CUR_TEL = dt.Rows[0]["MNC_CUR_TEL"].ToString();
            }
            else
            {
                ptt.MNC_HN_NO = "";
                ptt.MNC_HN_YR = "";
                ptt.MNC_PFIX_CDT = "";
                ptt.MNC_PFIX_CDE = "";
                ptt.MNC_FNAME_T = "";
                ptt.MNC_LNAME_T = "";
                ptt.MNC_FNAME_E = "";
                ptt.MNC_LNAME_E = "";
                ptt.MNC_AGE = "";
                ptt.MNC_BDAY = "";
                ptt.MNC_ID_NO = "";
                ptt.MNC_SS_NO = "";
                ptt.MNC_SEX = "";
                ptt.MNC_FULL_ADD = "";
                ptt.MNC_STAMP_DAT = "";
                ptt.MNC_STAMP_TIM = "";
                ptt.passport = "";
                ptt.hnyr = "";
                ptt.idcard = "";
                ptt.Name = "";
                ptt.patient_birthday = "";
            }
            return ptt;
        }
        public DataTable selectPatientCovidByDate(String date)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, m01.MNC_CUR_CHW, m01.MNC_CUR_AMP, m01.MNC_CUR_TUM, m01.MNC_CUR_ADD, m01.MNC_CUR_MOO, m01.MNC_CUR_SOI, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,MNC_AGE,convert(VARCHAR(20),m01.MNC_bday,23) as MNC_bday, m01.mnc_id_no, m01.mnc_hn_yr,m01.MNC_FNAME_E" +
                ",m01.MNC_LNAME_E,m01.MNC_PFIX_CDT,m01.MNC_CUR_TEL,m01.MNC_CUR_TEL,m01.MNC_PFIX_CDE,m01.MNC_ATT_NOTE " +
                ",m01.MNC_OCC_CD,m01.MNC_EDU_CD,m01.MNC_NAT_CD,m01.MNC_REL_CD,m01.MNC_NATI_CD,m01.MNC_OCC_CD,m01.MNC_CUR_ROAD " +
                "From  patient_m01 m01 " +
                " left join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                "left join patient_t01 pttt01 on m01.mnc_hn_no = pttt01.mnc_hn_no and m01.mnc_hn_yr = pttt01.mnc_hn_yr  " +
                "inner join patient_t15 pttt15 on pttt01.mnc_hn_no = pttt15.mnc_hn_no and pttt01.mnc_hn_yr = pttt15.mnc_hn_yr and pttt01.mnc_date = pttt15.mnc_date and pttt01.mnc_pre_no = pttt15.mnc_pre_no " +
                "left join patient_t16 pttt16 on pttt15.mnc_req_yr = pttt16.mnc_req_yr and pttt15.mnc_req_no = pttt16.mnc_req_no and pttt15.mnc_req_dat = pttt16.mnc_req_dat " +
                " Where pttt01.mnc_date = '" + date + "' and pttt16.mnc_sr_cd = 'H0696' " +
                "Order By pttt01.mnc_date, pttt01.mnc_pre_no ";
            dt = conn.selectData(conn.connMainHIS, sql);
            
            return dt;
        }
        public String selectProfixId(String provname)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m02.MNC_PFIX_CD " +
                "From  patient_m02 m02 " +
                " Where m02.MNC_PFIX_DSC like '" + provname + "%' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_PFIX_CD"].ToString();
            }
            return re;
        }
        public String selectProvinceName(String provid)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m09.MNC_CHW_DSC " +
                "From  patient_m09 m09 " +
                " Where m09.MNC_CHW_CD = '" + provid + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_CHW_DSC"].ToString();
            }
            return re;
        }
        public String selectProvinceId(String provname)
        {
            DataTable dt = new DataTable();
            String sql = "",re="";
            sql = "Select m09.MNC_CHW_CD " +
                "From  patient_m09 m09 " +
                " Where m09.MNC_CHW_DSC like '" + provname + "%' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_CHW_CD"].ToString();
            }
            return re;
        }
        public String selectAmphurId(String provid, String amprid)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m08.MNC_amp_CD " +
                "From  patient_m08 m08 " +
                " Where m08.MNC_CHW_cd = '" + provid + "' and m08.mnc_amp_dsc like '" + amprid + "%'";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_amp_CD"].ToString();
            }
            return re;
        }
        public String selectAmphurName(String provid, String amprid)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m08.mnc_amp_dsc " +
                "From  patient_m08 m08 " +
                " Where m08.MNC_CHW_cd = '" + provid + "' and m08.mnc_amp_cd = '"+ amprid + "'";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["mnc_amp_dsc"].ToString();
            }
            return re;
        }
        public String selectDistrictId(String provid, String amprid, String districtname)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m07.MNC_tum_CD " +
                "From  patient_m07 m07 " +
                " Where m07.MNC_CHW_cd = '" + provid + "' and m07.mnc_amp_cd = '" + amprid + "' and m07.mnc_tum_dsc like '" + districtname + "%'";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_tum_CD"].ToString();
            }
            return re;
        }
        public String selectDistrictName(String provid, String amprid, String districtid)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m07.mnc_tum_dsc " +
                "From  patient_m07 m07 " +
                " Where m07.MNC_CHW_cd = '" + provid + "' and m07.mnc_amp_cd = '" + amprid + "' and m07.mnc_tum_cd = '" + districtid + "'";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["mnc_tum_dsc"].ToString();
            }
            return re;
        }
        public String selectPOCId(String provid, String amprid, String districtid)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m11.MNC_poc " +
                "From  patient_m11 m11 " +
                " Where m11.MNC_CHW_cd = '" + provid + "' and m11.MNC_AMP_CD = '" + amprid + "' and m11.MNC_TUM_CD = '" + districtid + "'";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_poc"].ToString();
            }
            return re;
        }
        public Patient selectPatinet1(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select m01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE " +
                "From  patient_m01 m01 " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where m01.MNC_HN_NO like '" + hn + "%' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                ptt.Hn = dt.Rows[0]["MNC_HN_NO"].ToString();
                ptt.Age = dt.Rows[0]["MNC_AGE"].ToString();
                ptt.Name = dt.Rows[0]["prefix"].ToString() + " " + dt.Rows[0]["MNC_FNAME_T"].ToString() + " " + dt.Rows[0]["MNC_LNAME_T"].ToString();
            }
            return ptt;
        }
        public DataTable selectPatientinWardIPD(String wardid)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pt08.MNC_HN_NO,pm02.MNC_PFIX_DSC,pm01.MNC_FNAME_T, DATEDIFF(day,pt08.MNC_AD_DATE,getdate()) as day1, " +
                "pm01.MNC_LNAME_T,convert(varchar(20),pt08.MNC_AD_DATE,23) as MNC_AD_DATE,pt08.MNC_RM_NAM,pt08.MNC_BD_NO, '' as status_selected, pm02.MNC_PFIX_DSC +' ' + pm01.MNC_FNAME_T + ' ' + pm01.MNC_LNAME_T as patient_fullname " +
                "from PATIENT_T08 pt08 " +
                "inner join PATIENT_T01 pt01 on pt01.MNC_PRE_NO =pt08.MNC_PRE_NO and pt01.MNC_date = pt08.MNC_date " +
                "INNER JOIN dbo.PATIENT_M01 pm01 ON pt08.MNC_HN_NO = pm01.MNC_HN_NO " +
                "INNER JOIN dbo.PATIENT_M02 pm02 ON pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "WHERE   pt08.MNC_AD_STS = 'A' and mnc_ds_lev = '1' and pt08.mnc_wd_no = '" + wardid + "' " +
                " Order By pt08.mnc_ad_date, pt08.mnc_ad_time ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public String selectDeptOPD(String deptid)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m32.MNC_MD_DEP_DSC " +
                "From  patient_m32 m32 " +
                " Where m32.MNC_sec_no = '" + deptid + "' and m32.MNC_TYP_PT = 'O' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_MD_DEP_DSC"].ToString();
            }
            return re;
        }
        public String selectDeptIdOPDBySecId(String secid)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m32.MNC_MD_DEP_no " +
                "From  patient_m32 m32 " +
                " Where m32.MNC_sec_no = '" + secid + "' and m32.MNC_TYP_PT = 'O' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_MD_DEP_no"].ToString();
            }
            return re;
        }
        public DataTable selectDeptOPD()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m32.MNC_MD_DEP_DSC,m32.mnc_sec_no,m32.mnc_md_dep_no " +
                "From  patient_m32 m32 " +
                " Where  m32.MNC_TYP_PT = 'O' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            
            return dt;
        }
        public DataTable selectDeptIPD()
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select m32.MNC_MD_DEP_DSC,m32.mnc_sec_no,m32.MNC_MD_DEP_NO " +
                "From  patient_m32 m32 " +
                " Where m32.MNC_TYP_PT = 'I' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            
            return dt;
        }
        public void setCboDeptIPD(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            if (lDeptIPD.Count <= 0) getlDeptIPD();
            int i = 0;
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM32 row in lDeptIPD)
            {
                item = new ComboBoxItem();
                item.Value = row.mnc_sec_no;
                item.Text = row.mnc_md_dep_dsc;
                c.Items.Add(item);
                if (item.Value.Equals(selected))
                {
                    //c.SelectedItem = item.Value;
                    c.SelectedText = item.Text;
                    c.SelectedIndex = i + 1;
                }
                i++;
            }
            if (selected.Equals(""))
            {
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
        }
        public void setCboDeptIPDWdNo(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            if (lDeptIPD.Count <= 0) getlDeptIPD();
            int i = 0;
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM32 row in lDeptIPD)
            {
                item = new ComboBoxItem();
                item.Value = row.mnc_md_dep_no;
                item.Text = row.mnc_md_dep_dsc;
                c.Items.Add(item);
                if (item.Value.Equals(selected))
                {
                    //c.SelectedItem = item.Value;
                    c.SelectedText = item.Text;
                    c.SelectedIndex = i + 1;
                }
                i++;
            }
            if (selected.Equals(""))
            {
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
        }
        public void setCboDeptOPD(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            if (lDeptOPD.Count <= 0) getlDeptOPD();
            int i = 0;

            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientM32 row in lDeptOPD)
            {
                item = new ComboBoxItem();
                item.Value = row.mnc_sec_no;
                item.Text = row.mnc_md_dep_dsc;
                c.Items.Add(item);
                if (item.Value.Equals(selected))
                {
                    //c.SelectedItem = item.Value;
                    c.SelectedText = item.Text;
                    c.SelectedIndex = i + 1;
                }
                i++;
            }
            if (selected.Equals(""))
            {
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
        }
        private void chkNull(Patient p)
        {
            long chk = 0;
            decimal chk1 = 0;

            p.MNC_HN_NO = p.MNC_HN_NO == null ? "" : p.MNC_HN_NO;
            p.MNC_HN_YR = p.MNC_HN_YR == null ? "" : p.MNC_HN_YR;
            p.MNC_PFIX_CDT = p.MNC_PFIX_CDT == null ? "" : p.MNC_PFIX_CDT;
            p.MNC_PFIX_CDE = p.MNC_PFIX_CDE == null ? "" : p.MNC_PFIX_CDE;
            p.MNC_FNAME_T = p.MNC_FNAME_T == null ? "" : p.MNC_FNAME_T;
            p.MNC_LNAME_T = p.MNC_LNAME_T == null ? "" : p.MNC_LNAME_T;
            p.MNC_FNAME_E = p.MNC_FNAME_E == null ? "" : p.MNC_FNAME_E;
            p.MNC_LNAME_E = p.MNC_LNAME_E == null ? "" : p.MNC_LNAME_E;
            p.MNC_AGE = p.MNC_AGE == null ? "" : p.MNC_AGE;
            p.MNC_BDAY = p.MNC_BDAY == null ? "" : p.MNC_BDAY;
            p.MNC_ID_NO = p.MNC_ID_NO == null ? "" : p.MNC_ID_NO;
            p.MNC_SS_NO = p.MNC_SS_NO == null ? "" : p.MNC_SS_NO;
            p.MNC_SEX = p.MNC_SEX == null ? "" : p.MNC_SEX;
            p.MNC_FULL_ADD = p.MNC_FULL_ADD == null ? "" : p.MNC_FULL_ADD;
            p.MNC_STAMP_DAT = p.MNC_STAMP_DAT == null ? "" : p.MNC_STAMP_DAT;
            p.MNC_STAMP_TIM = p.MNC_STAMP_TIM == null ? "" : p.MNC_STAMP_TIM;

            p.MNC_CUR_ADD = p.MNC_CUR_ADD == null ? "" : p.MNC_CUR_ADD;
            p.MNC_CUR_TUM = p.MNC_CUR_TUM == null ? "" : p.MNC_CUR_TUM;
            p.MNC_CUR_AMP = p.MNC_CUR_AMP == null ? "" : p.MNC_CUR_AMP;
            p.MNC_CUR_CHW = p.MNC_CUR_CHW == null ? "" : p.MNC_CUR_CHW;
            p.MNC_CUR_POC = p.MNC_CUR_POC == null ? "" : p.MNC_CUR_POC;
            p.MNC_CUR_TEL = p.MNC_CUR_TEL == null ? "" : p.MNC_CUR_TEL;
            p.MNC_DOM_ADD = p.MNC_DOM_ADD == null ? "" : p.MNC_DOM_ADD;
            p.MNC_DOM_TUM = p.MNC_DOM_TUM == null ? "" : p.MNC_DOM_TUM;
            p.MNC_DOM_AMP = p.MNC_DOM_AMP == null ? "" : p.MNC_DOM_AMP;
            p.MNC_DOM_CHW = p.MNC_DOM_CHW == null ? "" : p.MNC_DOM_CHW;
            p.MNC_DOM_POC = p.MNC_DOM_POC == null ? "" : p.MNC_DOM_POC;
            p.MNC_DOM_TEL = p.MNC_DOM_TEL == null ? "" : p.MNC_DOM_TEL;

            p.MNC_REF_NAME = p.MNC_REF_NAME == null ? "" : p.MNC_REF_NAME;
            p.MNC_REF_ADD = p.MNC_REF_ADD == null ? "" : p.MNC_REF_ADD;
            p.MNC_REF_TUM = p.MNC_REF_TUM == null ? "" : p.MNC_REF_TUM;
            p.MNC_REF_AMP = p.MNC_REF_AMP == null ? "" : p.MNC_REF_AMP;
            p.MNC_REF_CHW = p.MNC_REF_CHW == null ? "" : p.MNC_REF_CHW;
            p.MNC_REF_POC = p.MNC_REF_POC == null ? "" : p.MNC_REF_POC;
            p.MNC_REF_TEL = p.MNC_REF_TEL == null ? "" : p.MNC_REF_TEL;

            p.MNC_CUR_MOO = p.MNC_CUR_MOO == null ? "" : p.MNC_CUR_MOO;
            p.MNC_DOM_MOO = p.MNC_DOM_MOO == null ? "" : p.MNC_DOM_MOO;
            p.MNC_REF_MOO = p.MNC_REF_MOO == null ? "" : p.MNC_REF_MOO;
            p.MNC_CUR_SOI = p.MNC_CUR_SOI == null ? "" : p.MNC_CUR_SOI;
            p.MNC_DOM_SOI = p.MNC_DOM_SOI == null ? "" : p.MNC_DOM_SOI;
            p.MNC_REF_SOI = p.MNC_REF_SOI == null ? "" : p.MNC_REF_SOI;
            p.MNC_FULL_ADD = p.MNC_FULL_ADD == null ? "" : p.MNC_FULL_ADD;
            p.MNC_COM_CD = p.MNC_COM_CD == null ? "" : p.MNC_COM_CD;
            p.MNC_COM_CD2 = p.MNC_COM_CD2 == null ? "" : p.MNC_COM_CD2;

            //p.MNC_DOC_YR = long.TryParse(p.MNC_DOC_YR, out chk) ? chk.ToString() : "0";

            //p.MNC_DF_AMT = decimal.TryParse(p.MNC_DF_AMT, out chk1) ? chk1.ToString() : "0";

        }
        public String insertPatient(Patient p)
        {
            String sql = "", chk = "", hn = "";
            long hn1 = 0;
            if (p.MNC_HN_NO.Length <= 0)
            {
                chk = insert(p);
            }
            else
            {
                chk = update(p);
            }
            return chk;
        }
        public String insertPatientStep1(Patient p)
        {
            String sql = "", chk = "", hn = "";
            long hn1 = 0;
            if (p.MNC_HN_NO.Length <= 0)
            {
                chk = insert(p);
            }
            else
            {
                chk = updateStept1(p);
            }
            return chk;
        }
        public String insertPatientTemp(Patient p, String statusInflue, String InflueGroup)
        {
            String sql = "", chk = "", hn = "";
            long hn1 = 0;
            //new LogWriter("d", "insert Patient p.MNC_HN_NO.Length " + p.MNC_ID_NO.Length);
            //if (p.MNC_ID_NO.Length <= 0)
            //{
                chk = insertTemp(p, statusInflue, InflueGroup);
            //}
            //else
            //{
            //    chk = updateTemp(p);
            //}
            return chk;
        }
        public String insert(Patient p)
        {
            String sql = "", chk = "", hn="";
            long hn1 = 0;
            try
            {
                chkNull(p);
                //hn = selectHnMax();
                //long.TryParse(hn, out hn1);
                //hn1++;
                //hn = hn1.ToString();
                if (p.MNC_HN_YR.Length <= 0)
                {
                    p.MNC_HN_YR = (DateTime.Now.Year + 543).ToString();
                }
                sql = "Insert Into " + ptt.table + "(" + ptt.MNC_HN_NO + "," + ptt.MNC_HN_YR + "," + ptt.MNC_PFIX_CDT + "," +
                    ptt.MNC_PFIX_CDE + "," + ptt.MNC_FNAME_T + "," + ptt.MNC_LNAME_T + "," +
                    ptt.MNC_FNAME_E + "," + ptt.MNC_LNAME_E + "," + ptt.MNC_AGE + "," +
                    ptt.MNC_BDAY + "," + ptt.MNC_ID_NO + "," + ptt.MNC_SS_NO + "," +

                    ptt.MNC_CUR_ADD + "," + ptt.MNC_CUR_TUM + "," + ptt.MNC_CUR_AMP + "," +
                    ptt.MNC_CUR_CHW + "," + ptt.MNC_CUR_POC + "," + ptt.MNC_CUR_TEL + "," +
                    ptt.MNC_DOM_ADD + "," + ptt.MNC_DOM_TUM + "," + ptt.MNC_DOM_AMP + "," +
                    ptt.MNC_DOM_CHW + "," + ptt.MNC_DOM_POC + "," + ptt.MNC_DOM_TEL + "," +
                    ptt.MNC_REF_NAME + "," + ptt.MNC_REF_ADD + "," + ptt.MNC_REF_TUM + "," +
                    ptt.MNC_REF_AMP + "," + ptt.MNC_REF_CHW + "," + ptt.MNC_REF_POC + "," +
                    ptt.MNC_REF_TEL + "," + ptt.MNC_CUR_MOO + "," + ptt.MNC_DOM_MOO + "," +
                    ptt.MNC_REF_MOO + "," + ptt.MNC_CUR_SOI + "," + ptt.MNC_DOM_SOI + "," +
                    ptt.MNC_REF_SOI + "," + ptt.MNC_FN_TYP_CD + "," + ptt.MNC_ATT_NOTE + "," +

                    ptt.MNC_SEX + "," + ptt.MNC_FULL_ADD + "," + ptt.MNC_STAMP_DAT + "," +
                    ptt.MNC_STAMP_TIM + "," + ptt.MNC_COM_CD + "," + ptt.MNC_COM_CD2+
                    ") " +
                    "Values((select max(mnc_hn_no) from patient_m01)+1,year(getdate())+543,'" + p.MNC_PFIX_CDT + "','" +
                    p.MNC_PFIX_CDE + "','" + p.MNC_FNAME_T.Replace("'", "''") + "','" + p.MNC_LNAME_T.Replace("'", "''") + "','" +
                    p.MNC_FNAME_E.Replace("'", "''") + "','" + p.MNC_LNAME_E.Replace("'", "''") + "','" + p.MNC_AGE + "','" +
                    p.MNC_BDAY + "','" + p.MNC_ID_NO + "','" + p.MNC_SS_NO + "','" +

                    p.MNC_CUR_ADD.Replace("'", "''") + "','" + p.MNC_CUR_TUM + "','" + p.MNC_CUR_AMP + "','" +
                    p.MNC_CUR_CHW + "','" + p.MNC_CUR_POC + "','" + p.MNC_CUR_TEL.Replace("'", "''") + "','" +
                    p.MNC_DOM_ADD.Replace("'", "''") + "','" + p.MNC_DOM_TUM + "','" + p.MNC_DOM_AMP + "','" +
                    p.MNC_DOM_CHW + "','" + p.MNC_DOM_POC + "','" + p.MNC_DOM_TEL + "','" +
                    p.MNC_REF_NAME.Replace("'", "''") + "','" + p.MNC_REF_ADD.Replace("'", "''") + "','" + p.MNC_REF_TUM + "','" +
                    p.MNC_REF_AMP + "','" + p.MNC_REF_CHW + "','" + p.MNC_REF_POC + "','" +
                    p.MNC_REF_TEL.Trim().Replace("'", "''") + "','" + p.MNC_CUR_MOO.Replace("หมู่ที่", "").Trim().Replace("'", "''") + "','" + p.MNC_DOM_MOO.Replace("หมู่ที่", "").Trim().Replace("'", "''") + "','" +
                    p.MNC_REF_MOO.Replace("หมู่ที่", "").Trim().Replace("'", "''") + "','" + p.MNC_CUR_SOI.Trim().Replace("'", "''") + "','" + p.MNC_DOM_SOI.Trim().Replace("'", "''") + "','" +
                    p.MNC_REF_SOI.Trim().Replace("'", "''") + "','" + p.MNC_FN_TYP_CD + "','" + p.MNC_ATT_NOTE.Replace("'", "''") + "','" +

                    p.MNC_SEX + "','" + p.MNC_FULL_ADD.Replace("'", "''") + "',convert(varchar(20), getdate(),23)," +
                    "REPLACE(convert(varchar(5),getdate(),108),':',''),'17352','17352'" +
                    ") ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                new LogWriter("d", "insert Patient chk " + chk);
            }
            catch (Exception ex)
            {
                chk = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert Patient sql " + sql + " ex " + chk);
            }
            return chk;
        }
        public String update(Patient p)
        {
            String sql = "", chk = "", hn = "";
            long hn1 = 0;
            try
            {
                chkNull(p);
                //hn = selectHnMax();
                //long.TryParse(hn, out hn1);
                //hn1++;
                //hn = hn1.ToString();
                sql = "Update " + ptt.table + " Set "
                    //+ ptt.MNC_HN_YR + " = '" + p.MNC_HN_YR + "' "
                    + " "+ptt.MNC_PFIX_CDT + " = '" +p.MNC_PFIX_CDT + "' " 
                    + "," + ptt.MNC_PFIX_CDE + " = '" + p.MNC_PFIX_CDE + "' "
                    + "," + ptt.MNC_FNAME_T + " = '" + p.MNC_FNAME_T.Replace("'", "''") + "' "
                    + "," + ptt.MNC_LNAME_T + " = '" + p.MNC_LNAME_T.Replace("'", "''") + "' "
                    + "," + ptt.MNC_FNAME_E + " = '" + p.MNC_FNAME_E.Replace("'", "''") + "' "
                    + "," + ptt.MNC_LNAME_E + " = '" + p.MNC_LNAME_E.Replace("'", "''") + "' "
                    //+ ptt.MNC_AGE + " = '" + p.MNC_AGE + "' "
                    + "," + ptt.MNC_BDAY + " = '" + p.MNC_BDAY + "' "
                    + "," + ptt.MNC_ID_NO + " = '" + p.MNC_ID_NO + "' "
                    + "," + ptt.MNC_SS_NO + " = '" + p.MNC_SS_NO + "' "
                    //+ ptt.MNC_CUR_ADD + " = '" + p.MNC_CUR_ADD + "' "
                    //+ ptt.MNC_CUR_TUM + " = '" + p.MNC_CUR_TUM + "' "
                    //+ ptt.MNC_CUR_AMP + " = '" + p.MNC_CUR_AMP + "' "
                    //+ ptt.MNC_CUR_CHW + " = '" + p.MNC_CUR_CHW + "' "
                    //+ ptt.MNC_CUR_POC + " = '" + p.MNC_CUR_POC + "' "
                    + "," + ptt.MNC_CUR_TEL + " = '" + p.MNC_CUR_TEL + "' "
                    //+ ptt.MNC_DOM_ADD + " = '" + p.MNC_DOM_ADD + "' "
                    //+ ptt.MNC_DOM_TUM + " = '" + p.MNC_DOM_TUM + "' "
                    //+ ptt.MNC_DOM_AMP + " = '" + p.MNC_DOM_AMP + "' "
                    //+ ptt.MNC_DOM_CHW + " = '" + p.MNC_DOM_CHW + "' "
                    //+ ptt.MNC_DOM_POC + " = '" + p.MNC_DOM_POC + "' "
                    + "," + ptt.MNC_DOM_TEL + " = '" + p.MNC_DOM_TEL + "' "
                    + "," + ptt.MNC_REF_NAME + " = '" + p.MNC_REF_NAME.Replace("'", "''") + "' "
                    //+ ptt.MNC_REF_ADD + " = '" + p.MNC_REF_ADD + "' "
                    //+ ptt.MNC_REF_TUM + " = '" + p.MNC_REF_TUM + "' "
                    //+ ptt.MNC_REF_AMP + " = '" + p.MNC_REF_AMP + "' "
                    //+ ptt.MNC_REF_CHW + " = '" + p.MNC_REF_CHW + "' "
                    //+ ptt.MNC_REF_POC + " = '" + p.MNC_REF_POC + "' "
                    + "," + ptt.MNC_REF_TEL + " = '" + p.MNC_REF_TEL + "' "
                    //+ ptt.MNC_CUR_MOO + " = '" + p.MNC_CUR_MOO.Replace("หมู่ที่", "").Replace("'", "''") + "' "
                    //+ ptt.MNC_DOM_MOO + " = '" + p.MNC_DOM_MOO.Replace("หมู่ที่", "").Replace("'", "''") + "' "
                    //+ ptt.MNC_REF_MOO + " = '" + p.MNC_REF_MOO.Replace("หมู่ที่", "").Replace("'", "''") + "' "
                    //+ ptt.MNC_CUR_SOI + " = '" + p.MNC_CUR_SOI.Replace("'", "''") + "' "
                    //+ ptt.MNC_DOM_SOI + " = '" + p.MNC_DOM_SOI.Replace("'", "''") + "' "
                    //+ ptt.MNC_REF_SOI + " = '" + p.MNC_REF_SOI.Replace("'", "''") + "' "
                    //+ ptt.MNC_FN_TYP_CD + " = '" + p.MNC_FN_TYP_CD + "' "
                    //+ ptt.MNC_ATT_NOTE + " = '" + p.MNC_ATT_NOTE.Replace("'", "''") + "' "
                    //+ ptt.MNC_SEX + " = '" + p.MNC_SEX + "' "
                    //+ ptt.MNC_FULL_ADD + " = '" + p.MNC_FULL_ADD.Replace("'", "''") + "' "
                    + "," + ptt.MNC_STAMP_DAT + " = convert(varchar(20), getdate(),23) "
                    + "," + ptt.MNC_STAMP_TIM + " = REPLACE(convert(varchar(5),getdate(),108),':','') "
                    //+ ptt.MNC_COM_CD + " = '17352' "
                    //+ ptt.MNC_COM_CD2 + " = '17352 "
                    +" " 
                    +"Where mnc_hn_no = '"+p.MNC_HN_NO+"' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                new LogWriter("d", "update Patient chk " + chk+ " MNC_FNAME_T " + p.MNC_FNAME_T + " MNC_LNAME_T " + p.MNC_LNAME_T);
            }
            catch (Exception ex)
            {
                chk = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "update Patient sql " + sql + " ex " + chk);
            }
            return chk;
        }
        public String updateStept1(Patient p)
        {
            String sql = "", chk = "", hn = "";
            long hn1 = 0;
            try
            {
                chkNull(p);
                //hn = selectHnMax();
                //long.TryParse(hn, out hn1);
                //hn1++;
                //hn = hn1.ToString();
                sql = "Update " + ptt.table + " Set "
                    //+ ptt.MNC_HN_YR + " = '" + p.MNC_HN_YR + "' "
                    + " " + ptt.MNC_PFIX_CDT + " = '" + p.MNC_PFIX_CDT + "' "
                    + "," + ptt.MNC_PFIX_CDE + " = '" + p.MNC_PFIX_CDE + "' "
                    + "," + ptt.MNC_FNAME_T + " = '" + p.MNC_FNAME_T.Replace("'", "''") + "' "
                    + "," + ptt.MNC_LNAME_T + " = '" + p.MNC_LNAME_T.Replace("'", "''") + "' "
                    + "," + ptt.MNC_FNAME_E + " = '" + p.MNC_FNAME_E.Replace("'", "''") + "' "
                    + "," + ptt.MNC_LNAME_E + " = '" + p.MNC_LNAME_E.Replace("'", "''") + "' "
                    + "," + ptt.passport + " = '" + p.passport + "' "
                    + "," + ptt.MNC_BDAY + " = '" + p.MNC_BDAY + "' "
                    + "," + ptt.MNC_ID_NO + " = '" + p.MNC_ID_NO + "' "
                    + "," + ptt.MNC_SS_NO + " = '" + p.MNC_SS_NO + "' "
                    //+ ptt.MNC_CUR_ADD + " = '" + p.MNC_CUR_ADD + "' "
                    //+ ptt.MNC_CUR_TUM + " = '" + p.MNC_CUR_TUM + "' "
                    //+ ptt.MNC_CUR_AMP + " = '" + p.MNC_CUR_AMP + "' "
                    //+ ptt.MNC_CUR_CHW + " = '" + p.MNC_CUR_CHW + "' "
                    //+ ptt.MNC_CUR_POC + " = '" + p.MNC_CUR_POC + "' "
                    + "," + ptt.MNC_CUR_TEL + " = '" + p.MNC_CUR_TEL + "' "
                    //+ ptt.MNC_DOM_ADD + " = '" + p.MNC_DOM_ADD + "' "
                    //+ ptt.MNC_DOM_TUM + " = '" + p.MNC_DOM_TUM + "' "
                    //+ ptt.MNC_DOM_AMP + " = '" + p.MNC_DOM_AMP + "' "
                    //+ ptt.MNC_DOM_CHW + " = '" + p.MNC_DOM_CHW + "' "
                    //+ ptt.MNC_DOM_POC + " = '" + p.MNC_DOM_POC + "' "
                    + "," + ptt.MNC_DOM_TEL + " = '" + p.MNC_DOM_TEL + "' "
                    //+ "," + ptt.MNC_REF_NAME + " = '" + p.MNC_REF_NAME.Replace("'", "''") + "' "
                    //+ ptt.MNC_REF_ADD + " = '" + p.MNC_REF_ADD + "' "
                    //+ ptt.MNC_REF_TUM + " = '" + p.MNC_REF_TUM + "' "
                    //+ ptt.MNC_REF_AMP + " = '" + p.MNC_REF_AMP + "' "
                    //+ ptt.MNC_REF_CHW + " = '" + p.MNC_REF_CHW + "' "
                    //+ ptt.MNC_REF_POC + " = '" + p.MNC_REF_POC + "' "
                    //+ "," + ptt.MNC_REF_TEL + " = '" + p.MNC_REF_TEL + "' "
                    //+ ptt.MNC_CUR_MOO + " = '" + p.MNC_CUR_MOO.Replace("หมู่ที่", "").Replace("'", "''") + "' "
                    //+ ptt.MNC_DOM_MOO + " = '" + p.MNC_DOM_MOO.Replace("หมู่ที่", "").Replace("'", "''") + "' "
                    //+ ptt.MNC_REF_MOO + " = '" + p.MNC_REF_MOO.Replace("หมู่ที่", "").Replace("'", "''") + "' "
                    //+ ptt.MNC_CUR_SOI + " = '" + p.MNC_CUR_SOI.Replace("'", "''") + "' "
                    //+ ptt.MNC_DOM_SOI + " = '" + p.MNC_DOM_SOI.Replace("'", "''") + "' "
                    //+ ptt.MNC_REF_SOI + " = '" + p.MNC_REF_SOI.Replace("'", "''") + "' "
                    //+ ptt.MNC_FN_TYP_CD + " = '" + p.MNC_FN_TYP_CD + "' "
                    //+ ptt.MNC_ATT_NOTE + " = '" + p.MNC_ATT_NOTE.Replace("'", "''") + "' "
                    //+ ptt.MNC_SEX + " = '" + p.MNC_SEX + "' "
                    //+ ptt.MNC_FULL_ADD + " = '" + p.MNC_FULL_ADD.Replace("'", "''") + "' "
                    + "," + ptt.MNC_STAMP_DAT + " = convert(varchar(20), getdate(),23) "
                    + "," + ptt.MNC_STAMP_TIM + " = REPLACE(convert(varchar(5),getdate(),108),':','') "
                    //+ ptt.MNC_COM_CD + " = '17352' "
                    //+ ptt.MNC_COM_CD2 + " = '17352 "
                    + " "
                    + "Where mnc_hn_no = '" + p.MNC_HN_NO + "' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                new LogWriter("d", "update Patient chk " + chk + " MNC_FNAME_T " + p.MNC_FNAME_T + " MNC_LNAME_T " + p.MNC_LNAME_T);
            }
            catch (Exception ex)
            {
                chk = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "update Patient sql " + sql + " ex " + chk);
            }
            return chk;
        }
        public String insertTemp(Patient p, String statusInflue, String InflueGroup)
        {
            String sql = "", chk = "", hn = "";
            long hn1 = 0;
            try
            {
                chkNull(p);
                //hn = selectHnMax();
                //long.TryParse(hn, out hn1);
                //hn1++;
                //hn = hn1.ToString();
                sql = "Insert Into patient_vaccine(" + ptt.MNC_PFIX_CDT + "," +
                    ptt.MNC_PFIX_CDE + "," + ptt.MNC_FNAME_T + "," + ptt.MNC_LNAME_T + "," +
                    ptt.MNC_FNAME_E + "," + ptt.MNC_LNAME_E + "," + ptt.MNC_AGE + "," +
                    ptt.MNC_BDAY + "," + ptt.MNC_ID_NO + "," + ptt.MNC_SS_NO + "," +

                    ptt.MNC_CUR_ADD + "," + ptt.MNC_CUR_TUM + "," + ptt.MNC_CUR_AMP + "," +
                    ptt.MNC_CUR_CHW + "," + ptt.MNC_CUR_POC + "," + ptt.MNC_CUR_TEL + "," +
                    ptt.MNC_DOM_ADD + "," + ptt.MNC_DOM_TUM + "," + ptt.MNC_DOM_AMP + "," +
                    ptt.MNC_DOM_CHW + "," + ptt.MNC_DOM_POC + "," + ptt.MNC_DOM_TEL + "," +
                    ptt.MNC_REF_NAME + "," + ptt.MNC_REF_ADD + "," + ptt.MNC_REF_TUM + "," +
                    ptt.MNC_REF_AMP + "," + ptt.MNC_REF_CHW + "," + ptt.MNC_REF_POC + "," +
                    ptt.MNC_REF_TEL + "," + ptt.MNC_CUR_MOO + "," + ptt.MNC_DOM_MOO + "," +
                    ptt.MNC_REF_MOO + "," + ptt.MNC_CUR_SOI + "," + ptt.MNC_DOM_SOI + "," +
                    ptt.MNC_REF_SOI + "," + ptt.MNC_FN_TYP_CD + "," + ptt.MNC_ATT_NOTE + "," +

                    ptt.MNC_SEX + "," + ptt.MNC_FULL_ADD + "," + ptt.MNC_STAMP_DAT + "," +
                    ptt.MNC_STAMP_TIM + "," + ptt.MNC_COM_CD + "," + ptt.MNC_COM_CD2 + "," + ptt.passport + ",status_vaccine_influenza_group,status_vaccine_influenza " +
                    ") " +
                    "Values('" + p.MNC_PFIX_CDT + "','" +
                    p.MNC_PFIX_CDE + "','" + p.MNC_FNAME_T.Replace("'", "''") + "','" + p.MNC_LNAME_T.Replace("'", "''") + "','" +
                    p.MNC_FNAME_E.Replace("'", "''") + "','" + p.MNC_LNAME_E.Replace("'", "''") + "','" + p.MNC_AGE + "','" +
                    p.MNC_BDAY + "','" + p.MNC_ID_NO + "','" + p.MNC_SS_NO + "','" +

                    p.MNC_CUR_ADD.Replace("'", "''") + "','" + p.MNC_CUR_TUM + "','" + p.MNC_CUR_AMP + "','" +
                    p.MNC_CUR_CHW + "','" + p.MNC_CUR_POC + "','" + p.MNC_CUR_TEL.Replace("'", "''") + "','" +
                    p.MNC_DOM_ADD.Replace("'", "''") + "','" + p.MNC_DOM_TUM + "','" + p.MNC_DOM_AMP + "','" +
                    p.MNC_DOM_CHW + "','" + p.MNC_DOM_POC + "','" + p.MNC_DOM_TEL + "','" +
                    p.MNC_REF_NAME.Replace("'", "''") + "','" + p.MNC_REF_ADD.Replace("'", "''") + "','" + p.MNC_REF_TUM + "','" +
                    p.MNC_REF_AMP + "','" + p.MNC_REF_CHW + "','" + p.MNC_REF_POC + "','" +
                    p.MNC_REF_TEL.Trim().Replace("'", "''") + "','" + p.MNC_CUR_MOO.Replace("หมู่ที่", "").Trim().Replace("'", "''") + "','" + p.MNC_DOM_MOO.Replace("หมู่ที่", "").Trim().Replace("'", "''") + "','" +
                    p.MNC_REF_MOO.Replace("หมู่ที่", "").Trim().Replace("'", "''") + "','" + p.MNC_CUR_SOI.Trim().Replace("'", "''") + "','" + p.MNC_DOM_SOI.Trim().Replace("'", "''") + "','" +
                    p.MNC_REF_SOI.Trim().Replace("'", "''") + "','" + p.MNC_FN_TYP_CD + "','" + p.MNC_ATT_NOTE.Replace("'", "''") + "','" +

                    p.MNC_SEX + "','" + p.MNC_FULL_ADD.Replace("'", "''") + "',convert(varchar(20), getdate(),23)," +
                    "REPLACE(convert(varchar(5),getdate(),108),':',''),'17352','17352','" +p.passport+"','"+ InflueGroup + "','" + statusInflue + "' " +
                    ") ";
                new LogWriter("e", "insert Patient sql " + sql );
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                new LogWriter("d", "insert Patient chk " + chk);
            }
            catch (Exception ex)
            {
                chk = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert Patient sql " + sql + " ex " + chk);
            }
            return chk;
        }
        public String updateTemp(Patient p)
        {
            String sql = "", chk = "", hn = "";
            long hn1 = 0;
            try
            {
                chkNull(p);
                sql = "Update patient_vaccine Set "
                    + " " + ptt.MNC_PFIX_CDT + " = '" + p.MNC_PFIX_CDT + "' "
                    + "," + ptt.MNC_PFIX_CDE + " = '" + p.MNC_PFIX_CDE + "' "
                    + "," + ptt.MNC_FNAME_T + " = '" + p.MNC_FNAME_T.Replace("'", "''") + "' "
                    + "," + ptt.MNC_LNAME_T + " = '" + p.MNC_LNAME_T.Replace("'", "''") + "' "
                    + "," + ptt.MNC_FNAME_E + " = '" + p.MNC_FNAME_E.Replace("'", "''") + "' "
                    + "," + ptt.MNC_LNAME_E + " = '" + p.MNC_LNAME_E.Replace("'", "''") + "' "
                    + "," + ptt.MNC_BDAY + " = '" + p.MNC_BDAY + "' "
                    + "," + ptt.MNC_SS_NO + " = '" + p.MNC_SS_NO + "' "
                    + "," + ptt.MNC_CUR_TEL + " = '" + p.MNC_CUR_TEL + "' "
                    + "," + ptt.MNC_DOM_TEL + " = '" + p.MNC_DOM_TEL + "' "
                    + "," + ptt.MNC_REF_NAME + " = '" + p.MNC_REF_NAME.Replace("'", "''") + "' "
                    + "," + ptt.MNC_REF_TEL + " = '" + p.MNC_REF_TEL + "' "
                    + "," + ptt.MNC_STAMP_DAT + " = convert(varchar(20), getdate(),23) "
                    + "," + ptt.MNC_STAMP_TIM + " = REPLACE(convert(varchar(5),getdate(),108),':','') "
                    + " "
                    + "Where MNC_ID_NO = '" + p.MNC_ID_NO + "' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                new LogWriter("d", "update Temp chk " + chk + " MNC_FNAME_T " + p.MNC_FNAME_T + " MNC_LNAME_T " + p.MNC_LNAME_T+ " MNC_ID_NO " + p.MNC_ID_NO);
            }
            catch (Exception ex)
            {
                chk = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateTemp sql " + sql + " ex " + chk);
            }
            return chk;
        }
        public String insertPatientImage(String hn, String hnyear, byte[] image)
        {
            String sql = "", re = "";
            try
            {
                string str = Encoding.Default.GetString(image);
                new LogWriter("d", "insertPatientImage hn " + hn+ " hnyear " + hnyear);
                sql = "delete from patient_img where mnc_hn_no = '"+hn+"' and mnc_hn_yr = '"+hnyear+"' ";
                conn.ExecuteNonQuery(conn.connMainHIS, sql);
                sql = "insert into patient_img (mnc_hn_no, mnc_hn_yr, mnc_pat_img) values(" +
                    "'"+hn+"','"+ hnyear+"',@photo"+
                    ")";
                re = conn.ExecuteNonQueryImage(conn.connMainHIS, sql, image);
            }
            catch (Exception ex)
            {
                re = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert Patient sql " + sql + " ex " + re);
            }
            return re;
        }
    }
}
