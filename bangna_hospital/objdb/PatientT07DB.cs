using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PatientT07DB
    {
        public PatientT07 pt07;
        ConnectDB conn;
        public PatientT07DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pt07 = new PatientT07();
            pt07.MNC_DOC_YR = "MNC_DOC_YR";
            pt07.MNC_DOC_NO = "MNC_DOC_NO";
            pt07.MNC_HN_YR = "MNC_HN_YR";
            pt07.MNC_HN_NO = "MNC_HN_NO";
            pt07.MNC_DATE = "MNC_DATE";
            pt07.MNC_TIME = "MNC_TIME";
            pt07.MNC_PRE_NO = "MNC_PRE_NO";
            pt07.MNC_DEP_NO = "MNC_DEP_NO";
            pt07.MNC_SEC_NO = "MNC_SEC_NO";
            pt07.MNC_DEPR_NO = "MNC_DEPR_NO";
            pt07.MNC_SECR_NO = "MNC_SECR_NO";
            pt07.MNC_APP_DAT = "MNC_APP_DAT";
            pt07.MNC_APP_TIM = "MNC_APP_TIM";
            pt07.MNC_APP_DSC = "MNC_APP_DSC";
            pt07.MNC_APP_STS = "MNC_APP_STS";
            pt07.MNC_APP_ADD = "MNC_APP_ADD";
            pt07.MNC_APP_TEL = "MNC_APP_TEL";
            pt07.MNC_APP_OR_FLG = "MNC_APP_OR_FLG";
            pt07.MNC_APP_ADM_FLG = "MNC_APP_ADM_FLG";
            pt07.MNC_APP_NO = "MNC_APP_NO";
            pt07.MNC_DOT_CD = "MNC_DOT_CD";
            pt07.MNC_SEX = "MNC_SEX";
            pt07.MNC_NAME = "MNC_NAME";
            pt07.MNC_AGE = "MNC_AGE";
            pt07.MNC_APP_BY = "MNC_APP_BY";
            pt07.MNC_AN_YR = "MNC_AN_YR";
            pt07.MNC_AN_NO = "MNC_AN_NO";
            pt07.MNC_STS = "MNC_STS";
            pt07.MNC_REM_MEMO = "MNC_REM_MEMO";
            pt07.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            pt07.MNC_EMPR_CD = "MNC_EMPR_CD";
            pt07.MNC_SEND_CARD = "MNC_SEND_CARD";
            pt07.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pt07.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pt07.MNC_SEND_DAT = "MNC_SEND_DAT";
            pt07.MNC_SEND_TIM = "MNC_SEND_TIM";
            pt07.MNC_SEND_VN = "MNC_SEND_VN";
            pt07.MNC_USR_UPD = "MNC_USR_UPD";
            pt07.MNC_VN_NO = "MNC_VN_NO";
            pt07.MNC_VN_SEQ = "MNC_VN_SEQ";
            pt07.MNC_VN_SUM = "MNC_VN_SUM";
            pt07.MNC_APP_TIM_E = "MNC_APP_TIM_E";
            pt07.MNC_APP_TYP = "MNC_APP_TYP";
            pt07.remark_call = "remark_call";
            pt07.status_remark_call = "status_remark_call";
            pt07.remark_call_date = "remark_call_date";
            pt07.apm_time = "apm_time";
            pt07.table = "PATIENT_T07";//Table Appointment
        }
        public DataTable selectByHnAll(String hn, String sort1)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select pt07.MNC_DOC_YR, pt07.MNC_DOC_NO, pt07.MNC_HN_YR, pt07.MNC_HN_NO, pt07.MNC_DATE, pt07.MNC_TIME,m01.MNC_CUR_TEL,fm02.MNC_FN_TYP_DSC, " +
                "pt07.MNC_PRE_NO, pt07.MNC_DEP_NO, pt07.MNC_SEC_NO, pt07.MNC_DEPR_NO, pt07.MNC_SECR_NO,isnull(pt07.remark_call,'') as remark_call,isnull(pt07.status_remark_call, '') as status_remark_call, pt07.remark_call_date, " +
                "convert(varchar(20), pt07.MNC_APP_DAT, 23 ) as MNC_APP_DAT, pt07.MNC_APP_TIM, pt07.MNC_APP_DSC, pt07.MNC_APP_STS, pt07.MNC_APP_ADD, " +
                "pt07.MNC_APP_TEL, pt07.MNC_APP_OR_FLG, pt07.MNC_APP_ADM_FLG, pt07.MNC_APP_NO, pt07.MNC_DOT_CD, " +
                "pt07.MNC_SEX, pt07.MNC_NAME, pt07.MNC_AGE, pt07.MNC_APP_BY, pt07.MNC_AN_YR, " +
                "pt07.MNC_AN_NO, pt07.MNC_STS, pt07.MNC_REM_MEMO, pt07.MNC_FN_TYP_CD, pt07.MNC_EMPR_CD, pt07.MNC_SEND_CARD,  " +
                " convert(varchar(20), pt07.MNC_STAMP_DAT, 20) as MNC_STAMP_DAT, pt07.MNC_STAMP_TIM, convert(varchar(20), pt07.MNC_SEND_DAT,23) as MNC_SEND_DAT, pt07.MNC_SEND_TIM, pt07.MNC_SEND_VN, pt07.MNC_VN_SEQ,  " +
                "pt07.MNC_VN_SUM, pt07.MNC_APP_TIM_E, pt07.MNC_APP_TYP, pm32.mnc_md_dep_dsc  " +//นัดตรวจที่แผนก
                ",m02.MNC_PFIX_DSC as prefix, m01.MNC_FNAME_T,m01.MNC_LNAME_T, isnull(m02.MNC_PFIX_DSC,'') +' '+ isnull(m01.MNC_FNAME_T,'')+' ' + isnull(m01.MNC_LNAME_T,'') as ptt_fullnamet " +
                ",(isnull(pm02dtr.MNC_PFIX_DSC,'') +' ' +isnull(pm26.MNC_DOT_FNAME,'') + ' ' + isnull(pm26.MNC_DOT_LNAME,'')) as dtr_name " +
                "From  " + pt07.table + " pt07 " +
                " inner join patient_m01 m01 on pt07.MNC_HN_NO = m01.MNC_HN_NO " +
                " left join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                "left join patient_m32 pm32 on pt07.MNC_SECR_NO = pm32.mnc_sec_no and pt07.MNC_DEPR_NO = pm32.mnc_md_dep_no and pm32.mnc_typ_pt = 'O' " +//ต้อง MNC_SECR_NO แผนกที่นัด  นัดตรวจที่แผนก
                "left join	patient_m26 pm26 on pt07.MNC_DOT_CD = pm26.MNC_DOT_CD " +
                "left JOIN	dbo.PATIENT_M02 as pm02dtr ON pm26.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                "left join Finance_m02 fm02 on pt07.MNC_FN_TYP_CD = fm02.MNC_FN_TYP_CD " +
            " Where pt07.MNC_HN_NO = '" +hn+"' "+
            "Order By pt07.MNC_APP_DAT "+ sort1 + ", pt07.MNC_APP_TIM "+ sort1+" ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectByDate(String date, String paidcode, String sort1)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "", wherepaid="";
            if (paidcode.Length > 0)
            {
                wherepaid = " and pt07.MNC_FN_TYP_CD = '"+paidcode+"' ";
            }
            sql = "Select pt07.MNC_DOC_YR, pt07.MNC_DOC_NO, pt07.MNC_HN_YR, pt07.MNC_HN_NO, pt07.MNC_DATE, pt07.MNC_TIME,m01.MNC_CUR_TEL,fm02.MNC_FN_TYP_DSC, " +
                "pt07.MNC_PRE_NO, pt07.MNC_DEP_NO, pt07.MNC_SEC_NO, pt07.MNC_DEPR_NO, pt07.MNC_SECR_NO,isnull(pt07.remark_call,'') as remark_call,isnull(pt07.status_remark_call,'') as status_remark_call, pt07.remark_call_date, " +
                "convert(varchar(20), pt07.MNC_APP_DAT, 23 ) as MNC_APP_DAT, pt07.MNC_APP_TIM, pt07.MNC_APP_DSC, pt07.MNC_APP_STS, pt07.MNC_APP_ADD, " +
                "pt07.MNC_APP_TEL, pt07.MNC_APP_OR_FLG, pt07.MNC_APP_ADM_FLG, pt07.MNC_APP_NO, pt07.MNC_DOT_CD, " +
                "pt07.MNC_SEX, pt07.MNC_NAME, pt07.MNC_AGE, pt07.MNC_APP_BY, pt07.MNC_AN_YR, " +
                "pt07.MNC_AN_NO, pt07.MNC_STS, pt07.MNC_REM_MEMO, pt07.MNC_FN_TYP_CD, pt07.MNC_EMPR_CD, pt07.MNC_SEND_CARD,  " +
                " convert(varchar(20), pt07.MNC_STAMP_DAT, 20) as MNC_STAMP_DAT, pt07.MNC_STAMP_TIM, convert(varchar(20), pt07.MNC_SEND_DAT,23) as MNC_SEND_DAT, pt07.MNC_SEND_TIM, pt07.MNC_SEND_VN, pt07.MNC_VN_SEQ,  " +
                "pt07.MNC_VN_SUM, pt07.MNC_APP_TIM_E, pt07.MNC_APP_TYP, pm32.mnc_md_dep_dsc  " +
                ",m02.MNC_PFIX_DSC as prefix, m01.MNC_FNAME_T,m01.MNC_LNAME_T, isnull(m02.MNC_PFIX_DSC,'') +' '+ isnull(m01.MNC_FNAME_T,'')+' ' + isnull(m01.MNC_LNAME_T,'') as ptt_fullnamet " +
                ",(isnull(pm02dtr.MNC_PFIX_DSC,'') +' ' +isnull(pm26.MNC_DOT_FNAME,'') + ' ' + isnull(pm26.MNC_DOT_LNAME,'')) as dtr_name " +
                "From  " + pt07.table + " pt07 " +
                " inner join patient_m01 m01 on pt07.MNC_HN_NO = m01.MNC_HN_NO " +
                " left join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                "left join patient_m32 pm32 on pt07.mnc_sec_no = pm32.mnc_sec_no and pt07.mnc_dep_no = pm32.mnc_md_dep_no and pm32.mnc_typ_pt = 'O' "+
                "left join	patient_m26 pm26 on pt07.MNC_DOT_CD = pm26.MNC_DOT_CD " +
                "left JOIN	dbo.PATIENT_M02 as pm02dtr ON pm26.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                "left join Finance_m02 fm02 on pt07.MNC_FN_TYP_CD = fm02.MNC_FN_TYP_CD " +
            " Where pt07.MNC_APP_DAT = '" + date + "' " + wherepaid+
            "Order By pt07.MNC_APP_DAT " +sort1+", pt07.MNC_APP_TIM "+sort1+" ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectByDate1(String datestart, String dateend, String dtrcode, String deptcode, String secno, String sort1)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "", wheredtr = "", wheredept="";
            if (dtrcode.Length > 0)
            {
                wheredtr = " and pt07.MNC_DOT_CD = '" + dtrcode + "' ";
            }
            if (secno.Length > 0)
            {
                //wheredept = " and pt07.MNC_SECR_NO = '" + secno + "' and pt07.MNC_DEPR_NO = '"+ deptcode+"' ";
                wheredept = " and pt07.MNC_NAME = '" + secno + "'  ";
            }
            sql = "Select ''  AS row_number, pt07.MNC_DOC_YR, pt07.MNC_DOC_NO, pt07.MNC_HN_YR, pt07.MNC_HN_NO as hn, pt07.MNC_DATE, pt07.MNC_TIME,m01.MNC_CUR_TEL as phone,fm02.MNC_FN_TYP_DSC as paidname, " +
                "pt07.MNC_PRE_NO, pt07.MNC_DEP_NO, pt07.MNC_SEC_NO, pt07.MNC_DEPR_NO, pt07.MNC_SECR_NO,isnull(pt07.remark_call,'') as remark_call,isnull(pt07.status_remark_call,'') as status_remark_call, pt07.remark_call_date, " +
                "convert(varchar(20), pt07.MNC_APP_DAT, 23) as apm_date, convert(varchar(20),pt07.MNC_APP_TIM) as apm_time, pt07.MNC_APP_DSC as desc1, pt07.MNC_APP_STS, pt07.MNC_APP_ADD, " +
                "pt07.MNC_APP_TEL, pt07.MNC_APP_OR_FLG, pt07.MNC_APP_ADM_FLG, pt07.MNC_APP_NO, pt07.MNC_DOT_CD, " +
                "pt07.MNC_SEX, pt07.MNC_NAME as apmdept, pt07.MNC_AGE, pt07.MNC_APP_BY, pt07.MNC_AN_YR, " +
                "pt07.MNC_AN_NO, pt07.MNC_STS, pt07.MNC_REM_MEMO, pt07.MNC_FN_TYP_CD, pt07.MNC_EMPR_CD, pt07.MNC_SEND_CARD,  " +
                " convert(varchar(20), pt07.MNC_STAMP_DAT, 20) as MNC_STAMP_DAT, pt07.MNC_STAMP_TIM, convert(varchar(20), pt07.MNC_SEND_DAT,23) as MNC_SEND_DAT, pt07.MNC_SEND_TIM, pt07.MNC_SEND_VN, pt07.MNC_VN_SEQ,  " +
                "pt07.MNC_VN_SUM, pt07.MNC_APP_TIM_E, pt07.MNC_APP_TYP, pm32make.mnc_md_dep_dsc as apmmake  " +
                ",m02.MNC_PFIX_DSC as prefix, m01.MNC_FNAME_T,m01.MNC_LNAME_T, isnull(m02.MNC_PFIX_DSC,'') +' '+ isnull(m01.MNC_FNAME_T,'')+' ' + isnull(m01.MNC_LNAME_T,'') as pttname " +
                ",(isnull(pm02dtr.MNC_PFIX_DSC,'') +' ' +isnull(pm26.MNC_DOT_FNAME,'') + ' ' + isnull(pm26.MNC_DOT_LNAME,'')) as dtrname " +
                "From  " + pt07.table + " pt07 " +
                " inner join patient_m01 m01 on pt07.MNC_HN_NO = m01.MNC_HN_NO " +
                " left join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                //"left join patient_m32 pm32 on pt07.MNC_SECR_NO = pm32.mnc_sec_no and pt07.MNC_DEPR_NO = pm32.mnc_md_dep_no  " +
                "left join patient_m32 pm32make on pt07.MNC_SEC_NO = pm32make.mnc_sec_no and pt07.mnc_dep_no = pm32make.mnc_md_dep_no  " +
                "left join	patient_m26 pm26 on pt07.MNC_DOT_CD = pm26.MNC_DOT_CD " +
                "left JOIN	dbo.PATIENT_M02 as pm02dtr ON pm26.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                "left join Finance_m02 fm02 on pt07.MNC_FN_TYP_CD = fm02.MNC_FN_TYP_CD " +
            " Where pt07.MNC_APP_DAT >= '" + datestart + "' and pt07.MNC_APP_DAT <= '" + dateend + "' " + wheredtr + wheredept+
            "Order By pt07.MNC_APP_DAT " + sort1 + ", pt07.MNC_APP_TIM " + sort1 + " ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectByDateDept(String date, String deptcode, String sort1)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "", wherepaid = "";
            if (deptcode.Length > 0)
            {
                wherepaid = " and pt07.MNC_SECR_NO = '" + deptcode + "' ";
            }
            sql = "Select pt07.MNC_DOC_YR, pt07.MNC_DOC_NO, pt07.MNC_HN_YR, pt07.MNC_HN_NO, pt07.MNC_DATE, pt07.MNC_TIME,m01.MNC_CUR_TEL,fm02.MNC_FN_TYP_DSC, " +
                "pt07.MNC_PRE_NO, pt07.MNC_DEP_NO, pt07.MNC_SEC_NO, pt07.MNC_DEPR_NO, pt07.MNC_SECR_NO,isnull(pt07.remark_call,'') as remark_call,isnull(pt07.status_remark_call,'') as status_remark_call, pt07.remark_call_date, " +
                "convert(varchar(20), pt07.MNC_APP_DAT, 23 ) as MNC_APP_DAT, pt07.MNC_APP_TIM, pt07.MNC_APP_DSC, pt07.MNC_APP_STS, pt07.MNC_APP_ADD, " +
                "pt07.MNC_APP_TEL, pt07.MNC_APP_OR_FLG, pt07.MNC_APP_ADM_FLG, pt07.MNC_APP_NO, pt07.MNC_DOT_CD, " +
                "pt07.MNC_SEX, pt07.MNC_NAME, pt07.MNC_AGE, pt07.MNC_APP_BY, pt07.MNC_AN_YR, " +
                "pt07.MNC_AN_NO, pt07.MNC_STS, pt07.MNC_REM_MEMO, pt07.MNC_FN_TYP_CD, pt07.MNC_EMPR_CD, pt07.MNC_SEND_CARD,  " +
                " convert(varchar(20), pt07.MNC_STAMP_DAT, 20) as MNC_STAMP_DAT, pt07.MNC_STAMP_TIM, convert(varchar(20), pt07.MNC_SEND_DAT,23) as MNC_SEND_DAT, pt07.MNC_SEND_TIM, pt07.MNC_SEND_VN, pt07.MNC_VN_SEQ,  " +
                "pt07.MNC_VN_SUM, pt07.MNC_APP_TIM_E, pt07.MNC_APP_TYP, pm32.mnc_md_dep_dsc  " +
                ",m02.MNC_PFIX_DSC as prefix, m01.MNC_FNAME_T,m01.MNC_LNAME_T, isnull(m02.MNC_PFIX_DSC,'') +' '+ isnull(m01.MNC_FNAME_T,'')+' ' + isnull(m01.MNC_LNAME_T,'') as ptt_fullnamet " +
                ",(isnull(pm02dtr.MNC_PFIX_DSC,'') +' ' +isnull(pm26.MNC_DOT_FNAME,'') + ' ' + isnull(pm26.MNC_DOT_LNAME,'')) as dtr_name " +
                "From  " + pt07.table + " pt07 " +
                " inner join patient_m01 m01 on pt07.MNC_HN_NO = m01.MNC_HN_NO " +
                " left join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                "left join patient_m32 pm32 on pt07.mnc_sec_no = pm32.mnc_sec_no and pt07.mnc_dep_no = pm32.mnc_md_dep_no and pm32.mnc_typ_pt = 'O' " +
                "left join	patient_m26 pm26 on pt07.MNC_DOT_CD = pm26.MNC_DOT_CD " +
                "left JOIN	dbo.PATIENT_M02 as pm02dtr ON pm26.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                "left join Finance_m02 fm02 on pt07.MNC_FN_TYP_CD = fm02.MNC_FN_TYP_CD " +
            " Where pt07.MNC_APP_DAT = '" + date + "' " + wherepaid +
            "Order By pt07.MNC_APP_DAT " + sort1 + ", pt07.MNC_APP_TIM " + sort1 + " ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public PatientT07 selectAppointmentDate(String hn, String startdate)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select pt07.MNC_DOC_YR, pt07.MNC_DOC_NO, pt07.MNC_HN_YR, pt07.MNC_HN_NO, pt07.MNC_DATE, pt07.MNC_TIME,m01.MNC_CUR_TEL,fm02.MNC_FN_TYP_DSC, " +
                "pt07.MNC_PRE_NO, pt07.MNC_DEP_NO, pt07.MNC_SEC_NO, pt07.MNC_DEPR_NO, pt07.MNC_SECR_NO,isnull(pt07.remark_call,'') as remark_call,isnull(pt07.status_remark_call, '') as status_remark_call, pt07.remark_call_date, " +
                "convert(varchar(20), pt07.MNC_APP_DAT, 23 ) as MNC_APP_DAT, pt07.MNC_APP_TIM, pt07.MNC_APP_DSC, pt07.MNC_APP_STS, pt07.MNC_APP_ADD, " +
                "pt07.MNC_APP_TEL, pt07.MNC_APP_OR_FLG, pt07.MNC_APP_ADM_FLG, pt07.MNC_APP_NO, pt07.MNC_DOT_CD,pt07.MNC_USR_UPD,pt07.MNC_VN_NO,pt07.MNC_VN_SEQ,pt07.MNC_VN_SUM, " +
                "pt07.MNC_SEX, pt07.MNC_NAME, pt07.MNC_AGE, pt07.MNC_APP_BY, pt07.MNC_AN_YR,pt07.apm_time, " +
                "pt07.MNC_AN_NO, pt07.MNC_STS, pt07.MNC_REM_MEMO, pt07.MNC_FN_TYP_CD, pt07.MNC_EMPR_CD, pt07.MNC_SEND_CARD,  " +
                " convert(varchar(20), pt07.MNC_STAMP_DAT, 20) as MNC_STAMP_DAT, pt07.MNC_STAMP_TIM, convert(varchar(20), pt07.MNC_SEND_DAT,23) as MNC_SEND_DAT, pt07.MNC_SEND_TIM, pt07.MNC_SEND_VN, pt07.MNC_VN_SEQ,  " +
                "pt07.MNC_VN_SUM, pt07.MNC_APP_TIM_E, pt07.MNC_APP_TYP, pm32.mnc_md_dep_dsc  " +
                ",m02.MNC_PFIX_DSC as prefix, m01.MNC_FNAME_T,m01.MNC_LNAME_T, isnull(m02.MNC_PFIX_DSC,'') +' '+ isnull(m01.MNC_FNAME_T,'')+' ' + isnull(m01.MNC_LNAME_T,'') as ptt_fullnamet " +
                ",(isnull(pm02dtr.MNC_PFIX_DSC,'') +' ' +isnull(pm26.MNC_DOT_FNAME,'') + ' ' + isnull(pm26.MNC_DOT_LNAME,'')) as dtr_name " +
                "From  " + pt07.table + " pt07 " +
                " inner join patient_m01 m01 on pt07.MNC_HN_NO = m01.MNC_HN_NO " +
                " left join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                "left join patient_m32 pm32 on pt07.mnc_sec_no = pm32.mnc_sec_no and pt07.mnc_dep_no = pm32.mnc_md_dep_no and pm32.mnc_typ_pt = 'O' " +
                "left join	patient_m26 pm26 on pt07.MNC_DOT_CD = pm26.MNC_DOT_CD " +
                "left JOIN	dbo.PATIENT_M02 as pm02dtr ON pm26.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                "left join Finance_m02 fm02 on pt07.MNC_FN_TYP_CD = fm02.MNC_FN_TYP_CD " +
            " Where pt07.MNC_HN_NO = '" + hn + "' and pt07.MNC_APP_DAT = '" + startdate + "' " +
            "Order By pt07.MNC_APP_DAT , pt07.MNC_APP_TIM  ";
            dt = conn.selectData(conn.connMainHIS, sql);
            PatientT07 pt071 = setPatientT07(dt);
            return pt071;
        }
        public PatientT07 selectAppointment(String apmyear, String apmno)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select pt07.MNC_DOC_YR, pt07.MNC_DOC_NO, pt07.MNC_HN_YR, pt07.MNC_HN_NO, pt07.MNC_DATE, pt07.MNC_TIME,m01.MNC_CUR_TEL,fm02.MNC_FN_TYP_DSC, " +
                "pt07.MNC_PRE_NO, pt07.MNC_DEP_NO, pt07.MNC_SEC_NO, pt07.MNC_DEPR_NO, pt07.MNC_SECR_NO,isnull(pt07.remark_call,'') as remark_call,isnull(pt07.status_remark_call, '') as status_remark_call, pt07.remark_call_date, " +
                "convert(varchar(20), pt07.MNC_APP_DAT, 23 ) as MNC_APP_DAT, pt07.MNC_APP_TIM, pt07.MNC_APP_DSC, pt07.MNC_APP_STS, pt07.MNC_APP_ADD, " +
                "pt07.MNC_APP_TEL, pt07.MNC_APP_OR_FLG, pt07.MNC_APP_ADM_FLG, pt07.MNC_APP_NO, pt07.MNC_DOT_CD,pt07.MNC_USR_UPD,pt07.MNC_VN_NO,pt07.MNC_VN_SEQ,pt07.MNC_VN_SUM, " +
                "pt07.MNC_SEX, pt07.MNC_NAME, pt07.MNC_AGE, pt07.MNC_APP_BY, pt07.MNC_AN_YR,pt07.apm_time, " +
                "pt07.MNC_AN_NO, pt07.MNC_STS, pt07.MNC_REM_MEMO, pt07.MNC_FN_TYP_CD, pt07.MNC_EMPR_CD, pt07.MNC_SEND_CARD,  " +
                " convert(varchar(20), pt07.MNC_STAMP_DAT, 20) as MNC_STAMP_DAT, pt07.MNC_STAMP_TIM, convert(varchar(20), pt07.MNC_SEND_DAT,23) as MNC_SEND_DAT, pt07.MNC_SEND_TIM, pt07.MNC_SEND_VN, pt07.MNC_VN_SEQ,  " +
                "pt07.MNC_VN_SUM, pt07.MNC_APP_TIM_E, pt07.MNC_APP_TYP, pm32.mnc_md_dep_dsc  " +
                ",m02.MNC_PFIX_DSC as prefix, m01.MNC_FNAME_T,m01.MNC_LNAME_T, isnull(m02.MNC_PFIX_DSC,'') +' '+ isnull(m01.MNC_FNAME_T,'')+' ' + isnull(m01.MNC_LNAME_T,'') as ptt_fullnamet " +
                ",(isnull(pm02dtr.MNC_PFIX_DSC,'') +' ' +isnull(pm26.MNC_DOT_FNAME,'') + ' ' + isnull(pm26.MNC_DOT_LNAME,'')) as dtr_name " +
                "From  " + pt07.table + " pt07 " +
                " inner join patient_m01 m01 on pt07.MNC_HN_NO = m01.MNC_HN_NO " +
                " left join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                "left join patient_m32 pm32 on pt07.mnc_sec_no = pm32.mnc_sec_no and pt07.mnc_dep_no = pm32.mnc_md_dep_no and pm32.mnc_typ_pt = 'O' " +
                "left join	patient_m26 pm26 on pt07.MNC_DOT_CD = pm26.MNC_DOT_CD " +
                "left JOIN	PATIENT_M02 as pm02dtr ON pm26.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                "left join Finance_m02 fm02 on pt07.MNC_FN_TYP_CD = fm02.MNC_FN_TYP_CD " +
            " Where pt07.MNC_DOC_YR = '" + apmyear + "' and pt07.MNC_DOC_NO = '" + apmno + "' " +
            "Order By pt07.MNC_APP_DAT , pt07.MNC_APP_TIM  ";
            dt = conn.selectData(conn.connMainHIS, sql);
            PatientT07 pt071 = setPatientT07(dt);
            return pt071;
        }
        public PatientT07 selectAppointmentToday(String hn)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select pt07.MNC_DOC_YR, pt07.MNC_DOC_NO, pt07.MNC_HN_YR, pt07.MNC_HN_NO, pt07.MNC_DATE, pt07.MNC_TIME,m01.MNC_CUR_TEL,fm02.MNC_FN_TYP_DSC, " +
                "pt07.MNC_PRE_NO, pt07.MNC_DEP_NO, pt07.MNC_SEC_NO, pt07.MNC_DEPR_NO, pt07.MNC_SECR_NO,isnull(pt07.remark_call,'') as remark_call,isnull(pt07.status_remark_call, '') as status_remark_call, pt07.remark_call_date, " +
                "convert(varchar(20), pt07.MNC_APP_DAT, 23 ) as MNC_APP_DAT, pt07.MNC_APP_TIM, pt07.MNC_APP_DSC, pt07.MNC_APP_STS, pt07.MNC_APP_ADD, " +
                "pt07.MNC_APP_TEL, pt07.MNC_APP_OR_FLG, pt07.MNC_APP_ADM_FLG, pt07.MNC_APP_NO, pt07.MNC_DOT_CD,pt07.MNC_USR_UPD,pt07.MNC_VN_NO,pt07.MNC_VN_SEQ,pt07.MNC_VN_SUM, " +
                "pt07.MNC_SEX, pt07.MNC_NAME, pt07.MNC_AGE, pt07.MNC_APP_BY, pt07.MNC_AN_YR,pt07.apm_time, " +
                "pt07.MNC_AN_NO, pt07.MNC_STS, pt07.MNC_REM_MEMO, pt07.MNC_FN_TYP_CD, pt07.MNC_EMPR_CD, pt07.MNC_SEND_CARD,  " +
                " convert(varchar(20), pt07.MNC_STAMP_DAT, 20) as MNC_STAMP_DAT, pt07.MNC_STAMP_TIM, convert(varchar(20), pt07.MNC_SEND_DAT,23) as MNC_SEND_DAT, pt07.MNC_SEND_TIM, pt07.MNC_SEND_VN, pt07.MNC_VN_SEQ,  " +
                "pt07.MNC_VN_SUM, pt07.MNC_APP_TIM_E, pt07.MNC_APP_TYP, pm32.mnc_md_dep_dsc  " +
                ",m02.MNC_PFIX_DSC as prefix, m01.MNC_FNAME_T,m01.MNC_LNAME_T, isnull(m02.MNC_PFIX_DSC,'') +' '+ isnull(m01.MNC_FNAME_T,'')+' ' + isnull(m01.MNC_LNAME_T,'') as ptt_fullnamet " +
                ",(isnull(pm02dtr.MNC_PFIX_DSC,'') +' ' +isnull(pm26.MNC_DOT_FNAME,'') + ' ' + isnull(pm26.MNC_DOT_LNAME,'')) as dtr_name " +
                "From  " + pt07.table + " pt07 " +
                " inner join patient_m01 m01 on pt07.MNC_HN_NO = m01.MNC_HN_NO " +
                " left join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                "left join patient_m32 pm32 on pt07.mnc_sec_no = pm32.mnc_sec_no and pt07.mnc_dep_no = pm32.mnc_md_dep_no and pm32.mnc_typ_pt = 'O' " +
                "left join	patient_m26 pm26 on pt07.MNC_DOT_CD = pm26.MNC_DOT_CD " +
                "left JOIN	dbo.PATIENT_M02 as pm02dtr ON pm26.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                "left join Finance_m02 fm02 on pt07.MNC_FN_TYP_CD = fm02.MNC_FN_TYP_CD " +
            " Where pt07.MNC_HN_NO = '" + hn + "' and pt07.MNC_APP_DAT = convert(varchar(20), getdate(), 23) " +
            "Order By pt07.MNC_APP_DAT , pt07.MNC_APP_TIM  ";
            dt = conn.selectData(conn.connMainHIS, sql);
            PatientT07 pt071 = setPatientT07(dt);
            return pt071;
        }
        public DataTable selectAppointmentOrder(String apmyear, String apmno)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select pt073.MNC_DOC_YR, pt073.MNC_DOC_NO, pt073.MNC_OPR_CD, pt073.MNC_OPR_FLAG, pt073.MNC_FN_CD, pt073.MNC_OPR_PRI,pt073.MNC_OPR_COS,pt073.MNC_OPR_RFN  " +
                "From  PATIENT_T073 pt073  " +
            " Where pt073.MNC_DOC_YR = '" + apmyear + "' and pt073.MNC_DOC_NO = '" + apmno + "' " +
            "Order By pt073.MNC_STAMP_DAT , pt073.MNC_STAMP_TIM  ";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        private void chkNull(PatientT07 p)
        {
            long chk = 0;
            decimal chk1 = 0;

            p.MNC_HN_NO = p.MNC_HN_NO == null ? "" : p.MNC_HN_NO;
            p.MNC_HN_YR = p.MNC_HN_YR == null ? "" : p.MNC_HN_YR;
            p.MNC_DATE = p.MNC_DATE == null ? "" : p.MNC_DATE;
            p.MNC_TIME = p.MNC_TIME == null ? "" : p.MNC_TIME;
            p.MNC_APP_DSC = p.MNC_APP_DSC == null ? "" : p.MNC_APP_DSC;
            p.MNC_APP_STS = p.MNC_APP_STS == null ? "" : p.MNC_APP_STS;
            p.MNC_APP_ADD = p.MNC_APP_ADD == null ? "" : p.MNC_APP_ADD;
            p.MNC_APP_TEL = p.MNC_APP_TEL == null ? "" : p.MNC_APP_TEL;
            p.MNC_APP_OR_FLG = p.MNC_APP_OR_FLG == null ? "" : p.MNC_APP_OR_FLG;
            p.MNC_APP_ADM_FLG = p.MNC_APP_ADM_FLG == null ? "" : p.MNC_APP_ADM_FLG;
            p.MNC_SEX = p.MNC_SEX == null ? "" : p.MNC_SEX;
            p.MNC_NAME = p.MNC_NAME == null ? "" : p.MNC_NAME;
            p.MNC_APP_BY = p.MNC_APP_BY == null ? "" : p.MNC_APP_BY;
            p.MNC_STS = p.MNC_STS == null ? "" : p.MNC_STS;
            p.MNC_REM_MEMO = p.MNC_REM_MEMO == null ? "" : p.MNC_REM_MEMO;
            p.MNC_FN_TYP_CD = p.MNC_FN_TYP_CD == null ? "" : p.MNC_FN_TYP_CD;
            p.MNC_EMPR_CD = p.MNC_EMPR_CD == null ? "" : p.MNC_EMPR_CD;
            p.MNC_SEND_CARD = p.MNC_SEND_CARD == null ? "" : p.MNC_SEND_CARD;
            p.MNC_SEND_DAT = p.MNC_SEND_DAT == null ? "" : p.MNC_SEND_DAT;
            p.MNC_SEND_TIM = p.MNC_SEND_TIM == null ? "" : p.MNC_SEND_TIM;
            p.MNC_APP_TYP = p.MNC_APP_TYP == null ? "" : p.MNC_APP_TYP;
            p.apm_time = p.apm_time == null ? "" : p.apm_time;
            p.MNC_APP_TIM = p.MNC_APP_TIM == null ? "" : p.MNC_APP_TIM;

            p.MNC_DEP_NO = long.TryParse(p.MNC_DEP_NO, out chk) ? chk.ToString() : "0";
            p.MNC_SEC_NO = long.TryParse(p.MNC_SEC_NO, out chk) ? chk.ToString() : "0";
            //p.MNC_AN_YR = long.TryParse(p.MNC_AN_YR, out chk) ? chk.ToString() : "0";
            //p.MNC_AN_NO = long.TryParse(p.MNC_AN_NO, out chk) ? chk.ToString() : "0";
            p.MNC_PRE_NO = long.TryParse(p.MNC_PRE_NO, out chk) ? chk.ToString() : "0";

            p.MNC_DOC_YR = long.TryParse(p.MNC_DOC_YR, out chk) ? chk.ToString() : "0";
            p.MNC_DOC_NO = long.TryParse(p.MNC_DOC_NO, out chk) ? chk.ToString() : "0";
            p.MNC_PRE_NO = long.TryParse(p.MNC_PRE_NO, out chk) ? chk.ToString() : "0";
            p.MNC_DEPR_NO = long.TryParse(p.MNC_DEPR_NO, out chk) ? chk.ToString() : "0";
            p.MNC_SECR_NO = long.TryParse(p.MNC_SECR_NO, out chk) ? chk.ToString() : "0";
            p.MNC_APP_NO = long.TryParse(p.MNC_APP_NO, out chk) ? chk.ToString() : "0";
            p.MNC_AGE = long.TryParse(p.MNC_AGE, out chk) ? chk.ToString() : "0";
            p.MNC_AN_YR = long.TryParse(p.MNC_AN_YR, out chk) ? chk.ToString() : "0";
            p.MNC_AN_NO = long.TryParse(p.MNC_AN_NO, out chk) ? chk.ToString() : "0";
            p.MNC_VN_NO = long.TryParse(p.MNC_VN_NO, out chk) ? chk.ToString() : "0";
            p.MNC_VN_SEQ = long.TryParse(p.MNC_VN_SEQ, out chk) ? chk.ToString() : "0";
            p.MNC_VN_SUM = long.TryParse(p.MNC_VN_SUM, out chk) ? chk.ToString() : "0";
            p.MNC_APP_TIM_E = long.TryParse(p.MNC_APP_TIM_E, out chk) ? chk.ToString() : "0";
            //p.apm_time = long.TryParse(p.MNC_AN_NO, out chk) ? chk.ToString() : "0";
        }
        public String setAppTime(String app_tim)
        {
            String re = "";
            if (app_tim.Length > 0)
            {
                String[] txt = app_tim.Split('-');
                if (txt.Length > 1)
                {
                    int.TryParse(txt[0].Replace(":", ""), out int chk);
                    re = chk.ToString();
                }
            }
            return re;
        }
        public void setCboTumbonName(C1ComboBox c, String datestart, String dateend, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();

            DataTable dt = new DataTable();
            
            String sql = "", re = "";
            sql = "Select distinct (isnull(pm02dtr.MNC_PFIX_DSC,'') +' ' +isnull(pm26.MNC_DOT_FNAME,'') + ' ' + isnull(pm26.MNC_DOT_LNAME,'')) as dtr_name,pt07.MNC_DOT_CD " +
                "From  " + pt07.table + " pt07 " +
                "left join patient_m26 pm26 on pt07.MNC_DOT_CD = pm26.MNC_DOT_CD " +
                "left JOIN	PATIENT_M02 as pm02dtr ON pm26.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                " Where pt07.MNC_APP_DAT >= '" + datestart + "' and pt07.MNC_APP_DAT <= '" + dateend + "' " +
                "Order By dtr_name ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                c.Items.Clear();
                item = new ComboBoxItem();
                item.Value ="";
                item.Text = "";
                c.Items.Add(item);
                foreach (DataRow row in dt.Rows)
                {
                    item = new ComboBoxItem();
                    item.Value = row["MNC_DOT_CD"].ToString();
                    item.Text = row["dtr_name"].ToString() ;
                    c.Items.Add(item);
                    if (item.Value.Equals(selected))
                    {
                        //c.SelectedItem = item.Value;
                        c.SelectedText = item.Text;
                        c.SelectedIndex = i;
                    }
                    i++;
                }
            }
            if (selected.Equals(""))
            {
                if (c.Items.Count > 0)
                {
                    c.SelectedIndex = 0;
                }
            }
        }
        public String insertPatientT073(String docno, String docyear,String ordercode,String fncode, String flag )
        {
            String sql = "", chk = "";
            try
            {
                sql = "Insert into PATIENT_T073 (" +
                    "MNC_DOC_YR, MNC_DOC_NO, MNC_OPR_CD, MNC_OPR_FLAG, MNC_FN_CD " +
                    ") " +
                    "Values(" +
                    "'"+docyear+"','" + docno + "','" + ordercode + "','" + flag + "','" + fncode + "')";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", " PATIENT07DB insertPatientT073 error " + ex.InnerException);
                chk = sql;
            }
            return chk;
        }
        public String insertPatientT07(PatientT07 p)
        {
            String sql = "", chk = "", hn = "";
            long hn1 = 0;
            chkNull(p);
            if (p.MNC_DOC_NO.Equals("0"))
            {
                p.MNC_DOC_YR = (DateTime.Now.Year+543).ToString();
                chk = insertStoreProcedure(p);
            }
            else
            {
                chk = update(p.MNC_DOC_YR, p.MNC_DOC_NO, p.MNC_SECR_NO, p.MNC_DEPR_NO, p.MNC_APP_DSC, p.MNC_APP_TEL, p.MNC_APP_DAT, p.MNC_APP_TIM, p.apm_time);
            }

            return chk;
        }
        public String insertStoreProcedure(PatientT07 p)
        {
            String sql = "", hn = "",re = "";
            long hn1 = 0;
            try
            {
                chkNull(p);
                conn.comStore = new SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "gen_patient_t07_by_hn";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("hn_where", p.MNC_HN_NO);
                conn.comStore.Parameters.AddWithValue("vsdate", p.MNC_DATE);
                conn.comStore.Parameters.AddWithValue("vstime", p.MNC_TIME);
                conn.comStore.Parameters.AddWithValue("secno", p.MNC_SEC_NO);
                conn.comStore.Parameters.AddWithValue("secnomake", p.MNC_SECR_NO);
                conn.comStore.Parameters.AddWithValue("deptnomake", p.MNC_DEPR_NO);
                conn.comStore.Parameters.AddWithValue("preno", p.MNC_PRE_NO);
                conn.comStore.Parameters.AddWithValue("doctor_id", p.MNC_DOT_CD);
                conn.comStore.Parameters.AddWithValue("apm_date", p.MNC_APP_DAT);
                conn.comStore.Parameters.AddWithValue("apm_time", p.MNC_APP_TIM);
                conn.comStore.Parameters.AddWithValue("apm_tel", p.MNC_APP_TEL);
                conn.comStore.Parameters.AddWithValue("apm_desc", p.MNC_APP_DSC);
                conn.comStore.Parameters.AddWithValue("apm_remark", p.MNC_REM_MEMO);
                conn.comStore.Parameters.AddWithValue("apm_time1", p.apm_time);

                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;
                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;
                if (int.TryParse(re, out int chk))
                {
                    //vsdate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("MM") + "-" + DateTime.Now.Day.ToString("dd");
                    //sql = "SELECT MNC_VN_NO, MNC_VN_SEQ FROM PATIENT_T01 " +
                    //    "WHERE (MNC_HN_NO ='"+ hn + "') AND (MNC_DATE =@'"+ vsdate + "') AND (MNC_ACT_NO <= 600) ORDER BY MNC_VN_NO, MNC_VN_SEQ;";
                }
            }
            catch (Exception ex)
            {
                //conn.connLinkLIS.Close();
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert PatientT07 sql " + sql + " ex " + sql);
                re = sql;
            }finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insert(PatientT07 p)
        {
            String sql = "", chk = "", hn = "";
            long hn1 = 0;
            try
            {
                chkNull(p);
                sql = "Insert Into " + pt07.table + "(" + pt07.MNC_HN_NO + "," + pt07.MNC_HN_YR + "," + pt07.MNC_DATE + "," +
                    pt07.MNC_PRE_NO + "," + pt07.MNC_DOC_YR + "," + pt07.MNC_TIME + "," +
                    pt07.MNC_DEP_NO + "," + pt07.MNC_SEC_NO + "," + pt07.MNC_DEPR_NO + "," +
                    pt07.MNC_SECR_NO + "," + pt07.MNC_APP_DAT + "," + pt07.MNC_APP_TIM + "," +
                    pt07.MNC_APP_DSC + "," + pt07.MNC_APP_STS + "," + pt07.MNC_APP_ADD + "," +
                    pt07.MNC_APP_TEL + "," + pt07.MNC_APP_OR_FLG + "," + pt07.MNC_APP_ADM_FLG + "," +
                    pt07.MNC_APP_NO + "," + pt07.MNC_DOT_CD + "," + pt07.MNC_SEX + "," +
                    pt07.MNC_NAME + "," + pt07.MNC_AGE + "," + pt07.MNC_APP_BY + "," +
                    pt07.MNC_AN_YR + "," + pt07.MNC_AN_NO + "," + pt07.MNC_STS + "," +
                    pt07.MNC_REM_MEMO + "," + pt07.MNC_FN_TYP_CD + "," + pt07.MNC_EMPR_CD + "," +
                    pt07.MNC_SEND_CARD + "," + pt07.MNC_STAMP_DAT + "," + pt07.MNC_STAMP_TIM + "," +
                    pt07.MNC_SEND_DAT + "," + pt07.MNC_SEND_TIM + "," + pt07.MNC_SEND_VN + "," +
                    pt07.MNC_USR_UPD + "," + pt07.MNC_VN_NO + "," + pt07.MNC_VN_SEQ + "," +
                    pt07.MNC_VN_SUM + "," + pt07.MNC_APP_TIM_E + "," + pt07.MNC_APP_TYP + "," +
                    pt07.remark_call + "," + pt07.status_remark_call + "," + pt07.remark_call_date + "," +
                    pt07.apm_time + " " +
                    ") " +
                    "Values( '" +
                    p.MNC_HN_NO + "','" + p.MNC_HN_YR + "','" + p.MNC_DATE + "','" +
                    p.MNC_PRE_NO + "','" + p.MNC_DOC_YR + "','" + p.MNC_TIME + "','" +
                    p.MNC_DEP_NO + "','" + p.MNC_SEC_NO + "','" + p.MNC_DEPR_NO + "','" +
                    p.MNC_SECR_NO + "','" + p.MNC_APP_DAT + "','" + p.MNC_APP_TIM + "','" +
                    p.MNC_APP_DSC.Replace("'","''") + "','" + p.MNC_APP_STS + "','" + p.MNC_APP_ADD + "','" +
                    p.MNC_APP_TEL + "','" + p.MNC_APP_OR_FLG + "','" + p.MNC_APP_ADM_FLG + "','" +
                    p.MNC_APP_NO + "','" + p.MNC_DOT_CD + "','" + p.MNC_SEX + "','" +
                    p.MNC_NAME + "','" + p.MNC_AGE + "','" + p.MNC_APP_BY + "','" +
                    p.MNC_AN_YR + "','" + p.MNC_AN_NO + "','" + p.MNC_STS + "','" +
                    p.MNC_REM_MEMO + "','" + p.MNC_FN_TYP_CD + "','" + p.MNC_EMPR_CD + "','" +
                    p.MNC_SEND_CARD + "','" + p.MNC_STAMP_DAT + "','" + p.MNC_STAMP_TIM + "','" +
                    p.MNC_SEND_DAT + "','" + p.MNC_SEND_TIM + "','" + p.MNC_SEND_VN + "','" +
                    p.MNC_USR_UPD + "','" + p.MNC_VN_NO + "','" + p.MNC_VN_SEQ + "','" +
                    p.MNC_VN_SUM + "','" + p.MNC_APP_TIM_E + "','" + p.MNC_APP_TYP + "','" +
                    p.remark_call + "','" + p.status_remark_call + "','" + p.remark_call_date + "','" +
                    p.apm_time + "' " +
                    ") ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //new LogWriter("d", "insert Patient chk " + chk);
            }
            catch (Exception ex)
            {
                chk = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert PatientT07 sql " + sql + " ex " + chk);
            }
            return chk;
        }
        public String update(String docyear, String docno, String secno, String deptno, String apmdesc, String ptttel, String apmdate, String apmtime, String apmtime1)
        {
            String sql = "", chk = "", hn = "";
            long hn1 = 0;
            try
            {
                sql = "Update " + pt07.table + " Set "
                    + " " + pt07.MNC_SECR_NO + " = '" + secno + "' "
                    + "," + pt07.MNC_DEPR_NO + " = '" + deptno + "' "
                    + "," + pt07.MNC_APP_DSC + " = '" + apmdesc + "' "
                    + "," + pt07.MNC_APP_TEL + " = '" + ptttel + "' "
                    + "," + pt07.MNC_APP_DAT + " = '" + apmdate + "' "
                    + "," + pt07.MNC_APP_TIM + " = '" + apmtime + "' "
                    + "," + pt07.apm_time + " = '" + apmtime1 + "' "
                    + "Where MNC_DOC_YR = '" + docyear + "' and MNC_DOC_NO = '" + docno + "' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //new LogWriter("d", "update Temp chk " + chk + " docyear " + docyear + " docno " + docno);
            }
            catch (Exception ex)
            {
                chk = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateRemarkCall sql " + sql + " ex " + chk);
            }
            return chk;
        }
        public String updateRemarkCall(String docyear, String docno, String remarkcall, String status)
        {
            String sql = "", chk = "", hn = "";
            long hn1 = 0;
            try
            {
                sql = "Update "+pt07.table+" Set "
                    + " " + pt07.remark_call + " = '" + remarkcall.Replace("'","''") + "' "
                    + "," + pt07.status_remark_call + " = '" + status + "' "
                    + "," + pt07.remark_call_date + " = convert(varchar(20), getdate(),20) "
                    + " "
                    + "Where MNC_DOC_YR = '" + docyear + "' and MNC_DOC_NO = '"+docno+"' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //new LogWriter("d", "update Temp chk " + chk + " docyear " + docyear + " docno " + docno);
            }
            catch (Exception ex)
            {
                chk = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateRemarkCall sql " + sql + " ex " + chk);
            }
            return chk;
        }
        public PatientT07 setPatientT07(DataTable dt)
        {
            PatientT07 pt07 = new PatientT07();
            if (dt.Rows.Count > 0)
            {
                pt07.MNC_DOC_YR = dt.Rows[0]["MNC_DOC_YR"].ToString();
                pt07.MNC_DOC_NO = dt.Rows[0]["MNC_DOC_NO"].ToString();
                pt07.MNC_HN_YR = dt.Rows[0]["MNC_HN_YR"].ToString();
                pt07.MNC_HN_NO = dt.Rows[0]["MNC_HN_NO"].ToString();
                pt07.MNC_DATE = dt.Rows[0]["MNC_DATE"].ToString();
                pt07.MNC_TIME = dt.Rows[0]["MNC_TIME"].ToString();
                pt07.MNC_PRE_NO = dt.Rows[0]["MNC_PRE_NO"].ToString();
                pt07.MNC_DEP_NO = dt.Rows[0]["MNC_DEP_NO"].ToString();
                pt07.MNC_SEC_NO = dt.Rows[0]["MNC_SEC_NO"].ToString();
                pt07.MNC_DEPR_NO = dt.Rows[0]["MNC_DEPR_NO"].ToString();
                pt07.MNC_SECR_NO = dt.Rows[0]["MNC_SECR_NO"].ToString();
                pt07.MNC_APP_DAT = dt.Rows[0]["MNC_APP_DAT"].ToString();
                pt07.MNC_APP_TIM = dt.Rows[0]["MNC_APP_TIM"].ToString();
                pt07.MNC_APP_DSC = dt.Rows[0]["MNC_APP_DSC"].ToString();
                pt07.MNC_APP_STS = dt.Rows[0]["MNC_APP_STS"].ToString();
                pt07.MNC_APP_ADD = dt.Rows[0]["MNC_APP_ADD"].ToString();
                pt07.MNC_APP_TEL = dt.Rows[0]["MNC_APP_TEL"].ToString();
                pt07.MNC_APP_OR_FLG = dt.Rows[0]["MNC_APP_OR_FLG"].ToString();
                pt07.MNC_APP_ADM_FLG = dt.Rows[0]["MNC_APP_ADM_FLG"].ToString();
                pt07.MNC_APP_NO = dt.Rows[0]["MNC_APP_NO"].ToString();
                pt07.MNC_DOT_CD = dt.Rows[0]["MNC_DOT_CD"].ToString();
                pt07.MNC_SEX = dt.Rows[0]["MNC_SEX"].ToString();
                pt07.MNC_NAME = dt.Rows[0]["MNC_NAME"].ToString();
                pt07.MNC_AGE = dt.Rows[0]["MNC_AGE"].ToString();
                pt07.MNC_APP_BY = dt.Rows[0]["MNC_APP_BY"].ToString();
                pt07.MNC_AN_YR = dt.Rows[0]["MNC_AN_YR"].ToString();
                pt07.MNC_AN_NO = dt.Rows[0]["MNC_AN_NO"].ToString();
                pt07.MNC_STS = dt.Rows[0]["MNC_STS"].ToString();
                pt07.MNC_REM_MEMO = dt.Rows[0]["MNC_REM_MEMO"].ToString();
                pt07.MNC_FN_TYP_CD = dt.Rows[0]["MNC_FN_TYP_CD"].ToString();
                pt07.MNC_EMPR_CD = dt.Rows[0]["MNC_EMPR_CD"].ToString();
                pt07.MNC_SEND_CARD = dt.Rows[0]["MNC_SEND_CARD"].ToString();
                pt07.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                pt07.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                pt07.MNC_SEND_DAT = dt.Rows[0]["MNC_SEND_DAT"].ToString();
                pt07.MNC_SEND_TIM = dt.Rows[0]["MNC_SEND_TIM"].ToString();
                pt07.MNC_SEND_VN = dt.Rows[0]["MNC_SEND_VN"].ToString();
                pt07.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                pt07.MNC_VN_NO = dt.Rows[0]["MNC_VN_NO"].ToString();
                pt07.MNC_VN_SEQ = dt.Rows[0]["MNC_VN_SEQ"].ToString();
                pt07.MNC_VN_SUM = dt.Rows[0]["MNC_VN_SUM"].ToString();
                pt07.MNC_APP_TIM_E = dt.Rows[0]["MNC_APP_TIM_E"].ToString();
                pt07.MNC_APP_TYP = dt.Rows[0]["MNC_APP_TYP"].ToString();
                pt07.MNC_APP_TYP = dt.Rows[0]["MNC_APP_TYP"].ToString();
                pt07.apm_time = dt.Rows[0]["apm_time"].ToString();
                pt07.doctor_name = dt.Rows[0]["dtr_name"].ToString();
                pt07.patient_name = dt.Rows[0]["ptt_fullnamet"].ToString();
                pt07.deptname = dt.Rows[0]["mnc_md_dep_dsc"].ToString();
                pt07.apm_cnt_inday = dt.Rows.Count.ToString();
            }
            else
            {
                setPatientT07(pt07);
            }
            return pt07;
        }
        public PatientT07 setPatientT07(PatientT07 p)
        {
            p.MNC_DOC_YR = "";
            p.MNC_DOC_NO = "";
            p.MNC_HN_YR = "";
            p.MNC_HN_NO = "";
            p.MNC_DATE = "";
            p.MNC_TIME = "";
            p.MNC_PRE_NO = "";
            p.MNC_DEP_NO = "";
            p.MNC_SEC_NO = "";
            p.MNC_DEPR_NO = "";
            p.MNC_SECR_NO = "";
            p.MNC_APP_DAT = "";
            p.MNC_APP_TIM = "";
            p.MNC_APP_DSC = "";
            p.MNC_APP_STS = "";
            p.MNC_APP_ADD = "";
            p.MNC_APP_TEL = "";
            p.MNC_APP_OR_FLG = "";
            p.MNC_APP_ADM_FLG = "";
            p.MNC_APP_NO = "";
            p.MNC_DOT_CD = "";
            p.MNC_SEX = "";
            p.MNC_NAME = "";
            p.MNC_AGE = "";
            p.MNC_APP_BY = "";
            p.MNC_AN_YR = "";
            p.MNC_AN_NO = "";
            p.MNC_STS = "";
            p.MNC_REM_MEMO = "";
            p.MNC_FN_TYP_CD = "";
            p.MNC_EMPR_CD = "";
            p.MNC_SEND_CARD = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_SEND_DAT = "";
            p.MNC_SEND_TIM = "";
            p.MNC_SEND_VN = "";
            p.MNC_USR_UPD = "";
            p.MNC_VN_NO = "";
            p.MNC_VN_SEQ = "";
            p.MNC_VN_SUM = "";
            p.MNC_APP_TIM_E = "";
            p.MNC_APP_TYP = "";
            p.apm_time = "";
            p.doctor_name = "";
            pt07.patient_name = "";
            pt07.deptname = "";
            pt07.apm_cnt_inday = "";
            return p;
        }
    }
}
