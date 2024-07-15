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
    public class LabT01DB
    {
        public LabT01 labT01;
        ConnectDB conn;
        public LabT01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labT01 = new LabT01();
            labT01.MNC_REQ_YR = "MNC_REQ_YR";
            labT01.MNC_REQ_NO = "MNC_REQ_NO";
            labT01.MNC_REQ_DAT = "MNC_REQ_DAT";
            labT01.MNC_REQ_DEP = "MNC_REQ_DEP";
            labT01.MNC_REQ_STS = "MNC_REQ_STS";
            labT01.MNC_REQ_TIM = "MNC_REQ_TIM";
            labT01.MNC_HN_YR = "MNC_HN_YR";
            labT01.MNC_HN_NO = "MMNC_HN_NO";
            labT01.MNC_AN_YR = "MNC_AN_YR";
            labT01.MNC_AN_NO = "MNC_AN_NO";
            labT01.MNC_PRE_NO = "MNC_PRE_NO";
            labT01.MNC_DATE = "MNC_DATE";
            labT01.MNC_TIME = "MNC_TIME";
            labT01.MNC_DOT_CD = "MNC_DOT_CD";
            labT01.MNC_WD_NO = "MNC_WD_NO";
            labT01.MNC_RM_NAM = "MNC_RM_NAM";
            labT01.MNC_BD_NO = "MNC_BD_NO";
            labT01.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            labT01.MNC_COM_CD = "MNC_COM_CD";
            labT01.MNC_REM = "MNC_REM";
            labT01.MNC_LB_STS = "MNC_LB_STS";
            labT01.MNC_CAL_NO = "MNC_CAL_NO";
            labT01.MNC_EMPR_CD = "MNC_EMPR_CD";
            labT01.MNC_EMPC_CD = "MNC_EMPC_CD";
            labT01.MNC_ORD_DOT = "MNC_ORD_DOT";
            labT01.MNC_CFM_DOT = "MNC_CFM_DOT";
            labT01.MNC_DOC_YR = "MNC_DOC_YR";
            labT01.MNC_DOC_NO = "MNC_DOC_NO";
            labT01.MNC_DOC_DAT = "MNC_DOC_DAT";
            labT01.MNC_DOC_CD = "MNC_DOC_CD";
            labT01.MNC_SPC_SEND_DAT = "MNC_SPC_SEND_DAT";
            labT01.MNC_SPC_SEND_TM = "MNC_SPC_SEND_TM";
            labT01.MNC_SPC_TYP = "MNC_SPC_TYP";
            labT01.MNC_REMARK = "MNC_REMARK";
            labT01.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            labT01.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            labT01.MNC_CANCEL_STS = "MNC_CANCEL_STS";
            labT01.MNC_PAC_CD = "MNC_PAC_CD";
            labT01.MNC_PAC_TYP = "MNC_PAC_TYP";
            labT01.MNC_DANGER_FLG = "MNC_DANGER_FLG";
            labT01.MNC_DIET_FLG = "MNC_DIET_FLG";
            labT01.MNC_MED_FLG = "MNC_MED_FLG";
            labT01.MNC_LAB_FN_TYP_CD = "MNC_LAB_FN_TYP_CD";
            labT01.MNC_IP_ADD1 = "MNC_IP_ADD1";
            labT01.MNC_IP_ADD2 = "MNC_IP_ADD2";
            labT01.MNC_IP_ADD3 = "MNC_IP_ADD3";
            labT01.MNC_IP_ADD4 = "MNC_IP_ADD4";
            labT01.MNC_PATNAME = "MNC_PATNAME";
            labT01.MNC_LOAD_STS = "MNC_LOAD_STS";
            labT01.MNC_IP_REC = "MNC_IP_REC";
            labT01.status_lis = "status_lis";

            labT01.table = "lab_t01";
        }
        public DataTable selectReqIPDByDate(String hn, String anno, String ancnt)
        {
            DataTable dt = new DataTable();
            String sql = "select labt01.MNC_REQ_NO,convert(varchar(20),labt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,labt01.MNC_REQ_DEP,labt01.MNC_REQ_STS,labt01.MNC_REQ_TIM  " +
                ",labt01.MNC_HN_NO,isnull(labt01.MNC_AN_YR,'') as MNC_AN_YR,isnull(labt01.MNC_AN_NO,'') as MNC_AN_NO,labt01.MNC_PRE_NO,labt01.MNC_WD_NO " +
                ",isnull(labt01.MNC_ORD_DOT,'') as MNC_ORD_DOT ,labt01.MNC_FN_TYP_CD, convert(varchar(20),labt01.mnc_date,23) as mnc_date " +
                ", userm01.MNC_USR_FULL, pm02.MNC_PFIX_DSC +' '+pm01.MNC_FNAME_T+' '+pm01.MNC_LNAME_T as pttfullnamet " +
                "From " + labT01.table + " labt01 " +
                "inner join patient_m01 pm01 on labt01.mnc_hn_no = pm01.mnc_hn_no and labt01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "Left Join patient_m02 pm02 On pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Left Join USERLOG_M01 userm01 on labt01.MNC_DOT_CD = userm01.MNC_USR_NAME " +
                "inner join PATIENT_T08 pt08 on labt01.mnc_hn_no = pt08.mnc_hn_no and labt01.MNC_PRE_NO = pt08.MNC_PRE_NO and labt01.MNC_DATE = pt08.MNC_DATE  "+
                "Where labt01.MNC_HN_NO ='" + hn + "' and pt08.MNC_AN_NO ='" + anno + "' and pt08.MNC_AN_YR ='" + ancnt + "' and labt01.MNC_REQ_STS <> 'C'  " +
                "Order By labt01.MNC_REQ_TIM";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectReqOPDByDate(String hn,String vsdate, String preno)
        {
            DataTable dt = new DataTable();
            String sql = "select labt01.MNC_REQ_NO,convert(varchar(20),labt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,labt01.MNC_REQ_DEP,labt01.MNC_REQ_STS,labt01.MNC_REQ_TIM  " +
                ",labt01.MNC_HN_NO,isnull(labt01.MNC_AN_YR,'') as MNC_AN_YR,isnull(labt01.MNC_AN_NO,'') as MNC_AN_NO,labt01.MNC_PRE_NO,labt01.MNC_WD_NO " +
                ",isnull(labt01.MNC_ORD_DOT,'') as MNC_ORD_DOT ,labt01.MNC_FN_TYP_CD, convert(varchar(20),labt01.mnc_date,23) as mnc_date " +
                ", userm01.MNC_USR_FULL, pm02.MNC_PFIX_DSC +' '+pm01.MNC_FNAME_T+' '+pm01.MNC_LNAME_T as pttfullnamet " +
                "From " + labT01.table + " labt01 " +
                "inner join patient_m01 pm01 on labt01.mnc_hn_no = pm01.mnc_hn_no and labt01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "Left Join patient_m02 pm02 On pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Left Join USERLOG_M01 userm01 on labt01.MNC_DOT_CD = userm01.MNC_USR_NAME " +
                "Where labt01.MNC_HN_NO ='" + hn + "' and labt01.MNC_DATE ='" + vsdate + "' and labt01.MNC_PRE_NO ='" + preno + "' and labt01.MNC_REQ_STS <> 'C'  " +
                "Order By labt01.MNC_REQ_TIM";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectVisitReqLabByDate(String datereq)
        {
            DataTable dt = new DataTable();
            String sql = "select  pt01.MNC_HN_NO,pt01.MNC_PRE_NO,pt01.MNC_FN_TYP_CD, convert(varchar(20),pt01.MNC_DATE,23) as MNC_DATE, userm01.MNC_USR_FULL as dtrnamet " +
                ", pm02.MNC_PFIX_DSC, pm01.MNC_FNAME_T, pm01.MNC_LNAME_T, pm02.MNC_PFIX_DSC +' '+pm01.MNC_FNAME_T+' '+pm01.MNC_LNAME_T as pttfullnamet " +
                ", pt01.MNC_TIME, pm01.MNC_SEX, pt01.MNC_DEP_NO, pt01.MNC_SEC_NO,pm01.MNC_BDAY " +
                ", pt01.MNC_VN_NO,pt01.MNC_VN_SEQ,pt01.MNC_VN_SUM, isnull(pt08.MNC_AN_NO,'0') as MNC_AN_NO, isnull(pt08.MNC_AN_YR,'') as MNC_AN_YR " +
                ", pt012.MNC_PAT_FLAG, isnull(pt012.MNC_WD_NO,'') as MNC_WD_NO, pm32d.MNC_MD_DEP_DSC as dept_opd,pm32an.MNC_MD_DEP_DSC as dept_ipd " +
                "From PATIENT_T01 pt01 " +
                "inner join patient_m01 pm01 on pt01.mnc_hn_no = pm01.mnc_hn_no and pt01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "Left Join patient_m02 pm02 On pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Left Join USERLOG_M01 userm01 on pt01.MNC_DOT_CD = userm01.MNC_USR_NAME " +
                " Left Join PATIENT_T01_2 pt012 on  pt01.mnc_hn_no = pt012.mnc_hn_no and pt01.mnc_pre_no = pt012.mnc_pre_no and pt01.mnc_date = pt012.mnc_date " +
                "left join PATIENT_M32 pm32d on pt01.MNC_SEC_NO = pm32d.MNC_SEC_NO and pt01.MNC_DEP_NO = pm32d.MNC_MD_DEP_NO " +
                "left join PATIENT_T08 pt08 on pt01.MNC_HN_NO = pt08.MNC_HN_NO and pt01.MNC_DATE = pt08.MNC_DATE and pt01.MNC_PRE_NO = pt08.MNC_PRE_NO " +
                "left join PATIENT_M32 pm32an on pt08.MNC_SEC_NO = pm32an.MNC_SEC_NO and pt08.MNC_WD_NO = pm32an.MNC_MD_DEP_NO " +
                "Where pt01.MNC_DATE ='" + datereq + "' and pt01.MNC_STS <> 'C' and pt01.MNC_LAB_FLG = 'A' " +
                "Order By pt01.MNC_TIME";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectNoSendByStatusLis(String datereq)
        {
            DataTable dt = new DataTable();
            String sql = "select labt01.MNC_REQ_NO,convert(varchar(20),labt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,labt01.MNC_REQ_DEP,labt01.MNC_REQ_STS,labt01.MNC_REQ_TIM " +
                ",labt01.MNC_HN_NO,isnull(labt01.MNC_AN_YR,'') as MNC_AN_YR,isnull(labt01.MNC_AN_NO,'') as MNC_AN_NO,labt01.MNC_PRE_NO,labt01.MNC_WD_NO " +
                ",isnull(labt01.MNC_ORD_DOT,'') as MNC_ORD_DOT ,labt01.MNC_FN_TYP_CD, convert(varchar(20),labt01.mnc_date,23) as mnc_date " +
                ", pm02.MNC_PFIX_DSC, pm01.MNC_FNAME_T, pm01.MNC_LNAME_T, userm01.MNC_USR_FULL, labt01.MNC_REQ_STS, isnull(labt01.status_lis,'') as status_lis " +
                //", pm32.MNC_MD_DEP_DSC " +
                "From " + labT01.table + " labt01 " +
                "inner join patient_m01 pm01 on labt01.mnc_hn_no = pm01.mnc_hn_no and labt01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "Left Join patient_m02 pm02 On pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Left Join USERLOG_M01 userm01 on labt01.MNC_ORD_DOT = userm01.MNC_USR_NAME " +
                //"Left Join patient_m32 pm32 on labt01.MNC_REQ_DEP = pm32.MNC_MD_DEP_NO " +
                "Where labt01." + labT01.MNC_REQ_DAT + " ='"+ datereq + "' " +
                "Order By "+labT01.MNC_REQ_DAT+","+labT01.MNC_REQ_NO;
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectNoSendByStatusLis()
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select convert(varchar(20), labt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,labt01.MNC_REQ_TIM, labt01.MNC_REQ_NO, labt01.status_lis " +
                ", labt01.mnc_req_yr,labt01.MNC_HN_NO,isnull(labt01.MNC_AN_NO,0) as MNC_AN_NO,isnull(labt01.MNC_AN_yr,0) as MNC_AN_yr,labt01.MNC_REQ_DEP " +
                ", pm02.MNC_PFIX_DSC, pm01.MNC_FNAME_T, pm01.MNC_LNAME_T, convert(varchar(20), pm01.MNC_BDAY,23) as MNC_BDAY, pm01.MNC_SEX " +
                ", pt01.MNC_VN_NO,pt01.MNC_VN_SEQ,pt01.MNC_VN_SUM,isnull(labt01.MNC_ORD_DOT,'') as MNC_ORD_DOT,isnull(pt01.mnc_sec_no,'') as mnc_sec_no, userm01.MNC_USR_FULL,labt01.MNC_WD_NO,isnull(labt01.MNC_EMPC_CD,'') as MNC_EMPC_CD, isnull(userm01_usr.MNC_USR_FULL,'') as MNC_USR_FULL_usr " +
                "From " + labT01.table + " labt01 " +
                //"inner join lab_t02 labt02 on labt01.MNC_REQ_YR = labt02.MNC_REQ_YR and labt01.MNC_REQ_NO = labt02.MNC_REQ_NO and labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                "inner join patient_m01 pm01 on labt01.mnc_hn_no = pm01.mnc_hn_no and labt01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "inner join patient_t01 pt01 on pt01.mnc_hn_no = labt01.mnc_hn_no and pt01.mnc_hn_yr = labt01.mnc_hn_yr and pt01.MNC_PRE_NO = labt01.MNC_PRE_NO and pt01.MNC_DATE = labt01.MNC_DATE " +
                "Left Join patient_m02 pm02 On pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Left Join USERLOG_M01 userm01 on labt01.MNC_ORD_DOT = userm01.MNC_USR_NAME " +
                "Left Join USERLOG_M01 userm01_usr on labt01.MNC_EMPC_CD = userm01_usr.MNC_USR_NAME " +
                "Where labt01." + labT01.status_lis+ "='0' " +
                "Order By labt01." + labT01.MNC_REQ_DAT + ",labt01." + labT01.MNC_REQ_NO ;

            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public DataTable selectReqOPDByNoStatus()
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select labt01.MNC_REQ_YR,labt01.MNC_REQ_NO,convert(varchar(20),labt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,labt01.MNC_REQ_DEP,labt01.MNC_REQ_TIM,labt01.MNC_DOT_CD " +
                ",pm01.MNC_HN_NO,isnull(labt01.MNC_AN_YR,'') as MNC_AN_YR,isnull(labt01.MNC_AN_NO,'') as MNC_AN_NO,labt01.MNC_PRE_NO,convert(varchar(20),labt01.MNC_DATE,23 ) as MNC_DATE " +
                ", pm02.MNC_PFIX_DSC + ' '+ pm01.MNC_FNAME_T + ' ' + pm01.MNC_LNAME_T as pttfullnamet,pm01.MNC_SEX,labt01.MNC_FN_TYP_CD,fm02.MNC_FN_TYP_DSC " +
                ", userm01dtr.MNC_USR_FULL as dtrname,isnull(labt01.MNC_EMPC_CD,'') as MNC_EMPC_CD, isnull(userm01_usr.MNC_USR_FULL,'') as MNC_USR_FULL_usr " +
                ", convert(varchar(20),pm01.MNC_BDAY,23) as MNC_BDAY " +
                "From LAB_T01 labt01 " +
                "inner join patient_m01 pm01 on labt01.mnc_hn_no = pm01.mnc_hn_no and labt01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "Left Join patient_m02 pm02 On pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Left Join USERLOG_M01 userm01dtr on labt01.MNC_ORD_DOT = userm01dtr.MNC_USR_NAME " +
                "Left Join USERLOG_M01 userm01_usr on labt01.MNC_EMPC_CD = userm01_usr.MNC_USR_NAME " +
                "Left Join FINANCE_M02 fm02 on labt01.MNC_FN_TYP_CD = fm02.MNC_FN_TYP_CD " +
                "Where labt01.MNC_REQ_STS = '' and labt01.MNC_AN_NO is null " +
                "Order By labt01.MNC_REQ_DAT, labt01.MNC_REQ_TIM,labt01.MNC_REQ_NO ";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        private void chkNull(LabT01 p)
        {
            long chk = 0;
            int chk1 = 0;
            decimal chk2 = 0;

        }
        public String insertLabT01(LabT01 p)
        {
            String sql = "", chk = "", re = "";

            chkNull(p);
            if (p.MNC_REQ_NO.Equals("0"))
            {
                re = insertOPD(p, "");
            }
            else
            {

            }
            return re;
        }
        public String updateStatusPrintResult(String reqno, String reqdate)
        {
            String sql = "", re = "";
            sql = "update lab_t01 " +
                "Set status_print_result_no = '1' " +
                //", MNC_REQ_STS = 'Q' " +  // ต้อง comment ไว้ก่อน ใช้งานจริง ค่อยเอาออก
                "Where  mnc_req_no = '" + reqno + "' and MNC_REQ_DAT = '" + reqdate + "' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            return re;
        }
        public String updateStatusLinkLIS(String reqyr, String reqno, String reqdate)
        {
            String sql = "", re = "";
            sql = "update lab_t01 " +
                "Set status_lis = '1' " +
                "Where mnc_req_yr = '" + reqyr + "' " +
                "and mnc_req_no = '" + reqno + "' and MNC_REQ_DAT = '" + reqdate + "' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            return re;
        }
        public String insertCOVID1500(String hn, String vsdate, String preno, String labcode, String userid)
        {
            String sql = "", chk = "", re = "";
            try
            {
                //new LogWriter("d", "insertCOVID 1");
                //new LogWriter("e", "PharmacyT01 insert " );       covid_insert_lab_covid_hn
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;

                conn.comStore.CommandText = "covid_insert_lab_covid_hn_new";
                conn.comStore.CommandType = CommandType.StoredProcedure;

                conn.comStore.Parameters.AddWithValue("hn", hn);
                conn.comStore.Parameters.AddWithValue("vs_date", vsdate);
                conn.comStore.Parameters.AddWithValue("pre_no", preno);
                conn.comStore.Parameters.AddWithValue("lab_code", labcode);
                conn.comStore.Parameters.AddWithValue("mnc_usr_upd", userid);
                conn.comStore.Parameters.AddWithValue("mnc_usr_add", userid);
                conn.comStore.Parameters.AddWithValue("status1500", "1");

                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;

                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;
            }
            catch (Exception ex)
            {
                new LogWriter("e", "LabT01 " + ex.Message + " " + sql);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertCOVID(String hn, String vsdate, String preno, String labcode, String userid)
        {
            String sql = "", chk = "", re = "";
            try
            {
                //new LogWriter("d", "insertCOVID 1");
                //new LogWriter("e", "PharmacyT01 insert " );       covid_insert_lab_covid_hn
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;

                conn.comStore.CommandText = "covid_insert_lab_covid_hn";
                conn.comStore.CommandType = CommandType.StoredProcedure;

                conn.comStore.Parameters.AddWithValue("hn", hn);
                conn.comStore.Parameters.AddWithValue("vs_date", vsdate);
                conn.comStore.Parameters.AddWithValue("pre_no", preno);
                conn.comStore.Parameters.AddWithValue("lab_code", labcode);
                conn.comStore.Parameters.AddWithValue("mnc_usr_upd", userid);
                conn.comStore.Parameters.AddWithValue("mnc_usr_add", userid);

                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;

                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;
            }
            catch (Exception ex)
            {
                new LogWriter("e", "LabT01 " + ex.Message + " " + sql);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertOPD(LabT01 p, String userid)
        {
            String sql = "", chk = "", re = "";
            try
            {
                //new LogWriter("e", "PharmacyT01 insert " );       covid_insert_lab_covid_hn
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "insert_lab_t01_opd";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                //conn.comStore.Parameters.AddWithValue("mnc_req_yr", p.MNC_REQ_YR);
                //conn.comStore.Parameters.AddWithValue("mnc_req_dat", p.MNC_REQ_DAT);
                //conn.comStore.Parameters.AddWithValue("mnc_req_tim", p.MNC_REQ_TIM);
                //conn.comStore.Parameters.AddWithValue("mnc_sum_pri", p.MNC_SUM_PRI);
                //conn.comStore.Parameters.AddWithValue("mnc_sum_cos", p.MNC_SUM_COS);
                //conn.comStore.Parameters.AddWithValue("mnc_dep_no", p.MNC_DEP_NO);
                conn.comStore.Parameters.AddWithValue("mnc_hn_yr", p.MNC_HN_YR);
                conn.comStore.Parameters.AddWithValue("mnc_hn_no", p.MNC_HN_NO);
                conn.comStore.Parameters.AddWithValue("mnc_date", p.MNC_DATE);
                conn.comStore.Parameters.AddWithValue("mnc_pre_no", p.MNC_PRE_NO);

                //conn.comStore.Parameters.AddWithValue("mnc_pre_seq", p.MNC_PRE_SEQ);
                //conn.comStore.Parameters.AddWithValue("mnc_time", p.MNC_TIME);
                conn.comStore.Parameters.AddWithValue("mnc_dot_cd", p.MNC_DOT_CD);
                

                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;

                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;
            }
            catch (Exception ex)
            {
                new LogWriter("e", "insert_lab_t01_opd " + ex.Message + " MNC_HN_YR " + p.MNC_HN_YR+ " MNC_HN_NO " + p.MNC_HN_NO + " MNC_DATE " + p.MNC_DATE + " MNC_PRE_NO " + p.MNC_PRE_NO);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public LabT01 setLabT01(DataTable dt)
        {
            LabT01 labT01 = new LabT01();
            if (dt.Rows.Count > 0)
            {
                labT01.MNC_REQ_YR = dt.Rows[0]["MNC_REQ_YR"].ToString();
                labT01.MNC_REQ_NO = dt.Rows[0]["MNC_REQ_NO"].ToString();
                labT01.MNC_REQ_DAT = dt.Rows[0]["MNC_REQ_DAT"].ToString();
                labT01.MNC_REQ_DEP = dt.Rows[0]["MNC_REQ_DEP"].ToString();
                labT01.MNC_REQ_STS = dt.Rows[0]["MNC_REQ_STS"].ToString();
                labT01.MNC_REQ_TIM = dt.Rows[0]["MNC_REQ_TIM"].ToString();
                labT01.MNC_HN_YR = dt.Rows[0]["MNC_HN_YR"].ToString();
                labT01.MNC_HN_NO = dt.Rows[0]["MNC_HN_NO"].ToString();
                labT01.MNC_AN_YR = dt.Rows[0]["MNC_AN_YR"].ToString();
                labT01.MNC_AN_NO = dt.Rows[0]["MNC_AN_NO"].ToString();
                labT01.MNC_PRE_NO = dt.Rows[0]["MNC_PRE_NO"].ToString();
                labT01.MNC_DATE = dt.Rows[0]["MNC_DATE"].ToString();
                labT01.MNC_TIME = dt.Rows[0]["MNC_TIME"].ToString();
                labT01.MNC_DOT_CD = dt.Rows[0]["MNC_DOT_CD"].ToString();
                labT01.MNC_WD_NO = dt.Rows[0]["MNC_WD_NO"].ToString();
                labT01.MNC_RM_NAM = dt.Rows[0]["MNC_RM_NAM"].ToString();
                labT01.MNC_BD_NO = dt.Rows[0]["MNC_BD_NO"].ToString();
                labT01.MNC_FN_TYP_CD = dt.Rows[0]["MNC_FN_TYP_CD"].ToString();
                labT01.MNC_COM_CD = dt.Rows[0]["MNC_COM_CD"].ToString();
                labT01.MNC_REM = dt.Rows[0]["MNC_REM"].ToString();
                labT01.MNC_LB_STS = dt.Rows[0]["MNC_LB_STS"].ToString();
                labT01.MNC_CAL_NO = dt.Rows[0]["MNC_CAL_NO"].ToString();
                labT01.MNC_EMPR_CD = dt.Rows[0]["MNC_EMPR_CD"].ToString();
                labT01.MNC_EMPC_CD = dt.Rows[0]["MNC_EMPC_CD"].ToString();
                labT01.MNC_ORD_DOT = dt.Rows[0]["MNC_ORD_DOT"].ToString();
                labT01.MNC_CFM_DOT = dt.Rows[0]["MNC_CFM_DOT"].ToString(); ;
                labT01.MNC_DOC_YR = dt.Rows[0]["MNC_DOC_YR"].ToString();
                labT01.MNC_DOC_NO = dt.Rows[0]["MNC_DOC_NO"].ToString();
                labT01.MNC_DOC_DAT = dt.Rows[0]["MNC_DOC_DAT"].ToString();
                labT01.MNC_DOC_CD = dt.Rows[0]["MNC_DOC_CD"].ToString();
                labT01.MNC_SPC_SEND_DAT = dt.Rows[0]["MNC_SPC_SEND_DAT"].ToString();
                labT01.MNC_SPC_SEND_TM = dt.Rows[0]["MNC_SPC_SEND_TM"].ToString();
                labT01.MNC_SPC_TYP = dt.Rows[0]["MNC_SPC_TYP"].ToString();
                labT01.MNC_REMARK = dt.Rows[0]["MNC_REMARK"].ToString();
                labT01.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                labT01.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                labT01.MNC_CANCEL_STS = dt.Rows[0]["MNC_CANCEL_STS"].ToString();
                labT01.MNC_PAC_CD = dt.Rows[0]["MNC_PAC_CD"].ToString();
                labT01.MNC_PAC_TYP = dt.Rows[0]["MNC_PAC_TYP"].ToString();
                labT01.MNC_DANGER_FLG = dt.Rows[0]["MNC_DANGER_FLG"].ToString();
                labT01.MNC_DIET_FLG = dt.Rows[0]["MNC_DIET_FLG"].ToString();
                labT01.MNC_MED_FLG = dt.Rows[0]["MNC_MED_FLG"].ToString();
                labT01.MNC_LAB_FN_TYP_CD = dt.Rows[0]["MNC_LAB_FN_TYP_CD"].ToString();
                labT01.MNC_IP_ADD1 = dt.Rows[0]["MNC_IP_ADD1"].ToString();
                labT01.MNC_IP_ADD2 = dt.Rows[0]["MNC_IP_ADD2"].ToString();
                labT01.MNC_IP_ADD3 = dt.Rows[0]["MNC_IP_ADD3"].ToString();
                labT01.MNC_IP_ADD4 = dt.Rows[0]["MNC_IP_ADD4"].ToString();
                labT01.MNC_PATNAME = dt.Rows[0]["MNC_PATNAME"].ToString();
                labT01.MNC_LOAD_STS = dt.Rows[0]["MNC_LOAD_STS"].ToString();
                labT01.MNC_IP_REC = dt.Rows[0]["MNC_IP_REC"].ToString();
                labT01.status_lis = dt.Rows[0]["status_lis"].ToString();
            }
            else
            {
                setLabT011(labT01);
            }
            return labT01;
        }
        public LabT01 setLabT011(LabT01 p)
        {
            p.MNC_REQ_YR = "";
            p.MNC_REQ_NO = "";
            p.MNC_REQ_DAT = "";
            p.MNC_REQ_DEP = "";
            p.MNC_REQ_STS = "";
            p.MNC_REQ_TIM = "";
            p.MNC_HN_YR = "";
            p.MNC_AN_YR = "";
            p.MNC_AN_NO = "";
            p.MNC_PRE_NO = "";
            p.MNC_DATE = "";
            p.MNC_TIME = "";
            p.MNC_DOT_CD = "";
            p.MNC_WD_NO = "";
            p.MNC_RM_NAM = "";
            p.MNC_BD_NO = "";
            p.MNC_FN_TYP_CD = "";
            p.MNC_COM_CD = "";
            p.MNC_REM = "";
            p.MNC_LB_STS = "";
            p.MNC_CAL_NO = "";
            p.MNC_EMPR_CD = "";
            p.MNC_EMPC_CD = "";
            p.MNC_ORD_DOT = "";
            p.MNC_CFM_DOT = "";
            p.MNC_DOC_YR = "";
            p.MNC_DOC_NO = "";
            p.MNC_DOC_DAT = "";
            p.MNC_DOC_CD = "";
            p.MNC_SPC_SEND_DAT = "";
            p.MNC_SPC_SEND_TM = "";
            p.MNC_SPC_TYP = "";
            p.MNC_REMARK = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_CANCEL_STS = "";
            p.MNC_PAC_CD = "";
            p.MNC_PAC_TYP = "";
            p.MNC_DANGER_FLG = "";
            p.MNC_DIET_FLG = "";
            p.MNC_MED_FLG = "";
            p.MNC_LAB_FN_TYP_CD = "";
            p.MNC_IP_ADD1 = "";
            p.MNC_IP_ADD2 = "";
            p.MNC_IP_ADD3 = "";
            p.MNC_IP_ADD4 = "";
            p.MNC_PATNAME = "";
            p.MNC_LOAD_STS = "";
            p.MNC_IP_REC = "";
            p.status_lis = "";
            return p;
        }
    }
}