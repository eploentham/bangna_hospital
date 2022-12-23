using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class VisitDB
    {
        //Config1 cf;
        public ConnectDB conn;
        public Visit vs;
        public String vnSearch = "";
        public VisitDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            //conn = new ConnectDB("mainhis");
            vs = new Visit();
            vs.HN = "";
            vs.PatientName = "";
            vs.vn = "";
            vs.VN = "";
            vs.vnseq = "";
            vs.vnsum = "";
            vs.VisitDate = "";

            vs.table = "";
            vs.pkField = "";
        }        
        public DataTable SelectHnLabOut(String reqid, String datereq)
        {
            DataTable dt = new DataTable();
            String year = "";
            int year2 = 0;
            year = datereq.Substring(0, 4);
            int.TryParse(year, out year2);
            year = (year2 + 543).ToString();
            String sql = "Select lt01.mnc_patname,lt01.mnc_pre_no,lt01.mnc_hn_no,lt01.mnc_req_no,convert(VARCHAR(20),lt01.mnc_req_dat,23) as mnc_req_dat,lt01.MNC_AN_NO, lt01.MNC_AN_YR" +
                ", ptt01.mnc_vn_seq, ptt01.mnc_vn_sum, ptt01.mnc_vn_no " +
                "From Lab_t01 lt01 " +
                "inner join patient_t01 ptt01 on ptt01.mnc_hn_no = lt01.mnc_hn_no and ptt01.mnc_pre_no = lt01.mnc_pre_no and ptt01.mnc_hn_yr = lt01.mnc_hn_yr " +
                "Where mnc_req_yr = '"+year+"' and mnc_req_dat = '"+datereq+"' and mnc_req_no = '"+ reqid + "'" ;
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable SelectHnLabOut1(String hn, String datereq)
        {
            DataTable dt = new DataTable();
            String year = "";
            int year2 = 0;
            year = datereq.Substring(0, 4);
            int.TryParse(year, out year2);
            year = (year2 + 543).ToString();
            String sql = "Select lt01.mnc_patname,lt01.mnc_pre_no,lt01.mnc_hn_no,lt01.mnc_req_no,convert(VARCHAR(20),lt01.mnc_req_dat,23) as mnc_req_dat,lt01.MNC_AN_NO, lt01.MNC_AN_YR" +
                ", ptt01.mnc_vn_seq, ptt01.mnc_vn_sum, ptt01.mnc_vn_no " +
                "From Lab_t01 lt01 " +
                "inner join patient_t01 ptt01 on ptt01.mnc_hn_no = lt01.mnc_hn_no and ptt01.mnc_pre_no = lt01.mnc_pre_no and ptt01.mnc_hn_yr = lt01.mnc_hn_yr " +
                "Where mnc_req_yr = '" + year + "' and mnc_req_dat = '" + datereq + "' and lt01.mnc_hn_no = '" + hn + "'";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectChronicByPID(String pid)
        {
            DataTable dt = new DataTable();
            
            String sql = "select ptt091.*, ptm184.MNC_CRO_DESC, um01.MNC_USR_FULL " +
                " " +
                "from PATIENT_T09_1 ptt091 " +
                "inner join PATIENT_M184 ptm184 on ptt091.CHRONICCODE = ptm184.MNC_CRO_CD " +
                "inner join USERLOG_M01 um01 on um01.MNC_USR_NAME = ptt091.MNC_DOC_CD " +
                "Where ptt091.mnc_idnum = '" + pid + "'" +
                "Order By ptt091.CHRONICCODE";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public Visit selectVisitCovid(String hn)
        {
            DataTable dt = new DataTable();
            Visit vs = new Visit();
            String sql = "", vsdate="";
            vsdate = DateTime.Now.ToString("yyyy-MM-dd");
            sql = "Select   m01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, FINANCE_M02.MNC_FN_TYP_DSC,convert(VARCHAR(20),m01.mnc_bday,23) as mnc_bday,m01.mnc_sex" +
                ",m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, t01.MNC_TIME" +
                ", t01.mnc_weight, t01.mnc_high, t01.MNC_TEMP, t01.mnc_ratios, t01.mnc_breath, t01.mnc_bp1_l, t01.mnc_bp1_r, t01.mnc_bp2_l, t01.mnc_bp2_r,t01.MNC_pre_no, t01.mnc_ref_dsc" +
                ", t01.mnc_id_nam, t01.mnc_shif_memo,t01.mnc_bld_l, t01.mnc_bld_h, t01.mnc_cir_head, t01.mnc_cc, t01.mnc_cc_in, t01.mnc_cc_ex " +
                "From  patient_m01 m01 " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT = m02.MNC_PFIX_CD " +
                " inner join patient_t01 t01 on m01.MNC_HN_NO =t01.MNC_HN_NO " +
                "INNER JOIN	FINANCE_M02 ON t01.MNC_FN_TYP_CD = FINANCE_M02.MNC_FN_TYP_CD " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                " Where t01.MNC_hn_NO = '" + hn + "' and t01.MNC_date = convert(varchar(20),GETDATE(),23) " +
                " ";


            return vs;
        }
        public Visit selectVisit(String hn, String vsdate, String preno)
        {
            DataTable dt = new DataTable();
            String sql = "";
            //String[] aa = vn.Split('/');
            sql = "Select   m01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, FINANCE_M02.MNC_FN_TYP_DSC,convert(VARCHAR(20),m01.mnc_bday,23) as mnc_bday,m01.mnc_sex" +
                ",m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, t01.MNC_TIME" +
                ", t01.mnc_weight, t01.mnc_high, t01.MNC_TEMP, t01.mnc_ratios, t01.mnc_breath, t01.mnc_bp1_l, t01.mnc_bp1_r, t01.mnc_bp2_l, t01.mnc_bp2_r,t01.MNC_pre_no, t01.mnc_ref_dsc" +
                ", t01.mnc_id_nam, t01.mnc_shif_memo,t01.mnc_bld_l, t01.mnc_bld_h, t01.mnc_cir_head, t01.mnc_cc, t01.mnc_cc_in, t01.mnc_cc_ex " +
                "From  patient_m01 m01 " +
                " left join patient_m02 m02 on m01.MNC_PFIX_CDT = m02.MNC_PFIX_CD " +
                " inner join patient_t01 t01 on m01.MNC_HN_NO =t01.MNC_HN_NO " +
                "left JOIN	FINANCE_M02 ON t01.MNC_FN_TYP_CD = FINANCE_M02.MNC_FN_TYP_CD " +
                " Where t01.MNC_hn_NO = '" + hn + "' and t01.MNC_date = '" + vsdate + "' and t01.MNC_pre_no='" + preno+"'";
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)
            {
                vs.HN = dt.Rows[0]["MNC_HN_NO"].ToString();
                vs.VN = dt.Rows[0]["MNC_VN_NO"].ToString() + "." + dt.Rows[0]["MNC_VN_SEQ"].ToString() + "." + dt.Rows[0]["MNC_VN_SUM"].ToString();
                vs.vn = dt.Rows[0]["MNC_VN_NO"].ToString();
                vs.vnseq = dt.Rows[0]["MNC_VN_SEQ"].ToString();
                vs.vnsum = dt.Rows[0]["MNC_VN_SUM"].ToString();
                vs.VisitTime = "0000"+dt.Rows[0]["mnc_time"].ToString();
                if (vs.VisitTime.Length >= 4)
                {
                    vs.VisitTime = vs.VisitTime.Substring(vs.VisitTime.Length - 4);
                    vs.VisitTime = vs.VisitTime.Substring(0, 2) + ":" + vs.VisitTime.Substring(vs.VisitTime.Length - 2);
                }
                vs.PatientName = dt.Rows[0]["prefix"].ToString() + " " + dt.Rows[0]["MNC_FNAME_T"].ToString() + " " + dt.Rows[0]["MNC_LNAME_T"].ToString();
                vs.PaidName = dt.Rows[0]["MNC_FN_TYP_DSC"].ToString();
                vs.ID = dt.Rows[0]["mnc_id_nam"].ToString();
                vs.weight = dt.Rows[0]["mnc_weight"].ToString();
                vs.high = dt.Rows[0]["mnc_high"].ToString();
                vs.temp = dt.Rows[0]["MNC_TEMP"].ToString();
                vs.ratios = dt.Rows[0]["mnc_ratios"].ToString();
                vs.breath = dt.Rows[0]["mnc_breath"].ToString();
                vs.bp1l = dt.Rows[0]["mnc_bp1_l"].ToString();
                vs.bp1r = dt.Rows[0]["mnc_bp1_r"].ToString();
                vs.bp2l = dt.Rows[0]["mnc_bp2_l"].ToString();
                vs.bp2r = dt.Rows[0]["mnc_bp2_r"].ToString();
                vs.symptom = dt.Rows[0]["mnc_shif_memo"].ToString();
                vs.preno = dt.Rows[0]["MNC_pre_no"].ToString();
            }
            return vs;
        }
        public Patient selectPatientName(String hn)
        {
            Patient ptt = new Patient();
            DataTable dt = new DataTable();
            String sql = "";
            String[] aa = hn.Split('/');
            sql = "Select   m01.*,m02.MNC_PFIX_DSC as prefix " +
                "From  patient_m01 m01  " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                //" inner join patient_t01 t01 on m01.MNC_HN_NO =t01.MNC_HN_NO " +
                " Where m01.MNC_HN_NO = '" + hn + "' ";
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)
            {
                ptt.Name = dt.Rows[0]["prefix"].ToString() + " " + dt.Rows[0]["MNC_FNAME_T"].ToString() + " " + dt.Rows[0]["MNC_LNAME_T"].ToString();
                ptt.patient_birthday = dt.Rows[0]["mnc_bday"].ToString();
            }
            return ptt;
        }
        public DataTable selectAppointmentByDtr(String dtrid, String date)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   t07.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t07.MNC_VN_NO,t07.MNC_VN_SEQ,t07.MNC_VN_SUM " +
                //"Case f02.MNC_FN_TYP_DSC " +
                //    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                //    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                //    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                //    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                //    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                //    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                //    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                //    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                //    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                //    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                //    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                //    "When '' Then '' " +
                //    "When '' Then '' " +
                //    "When '' Then '' " +
                //    "Else MNC_FN_TYP_DSC " +
                //    "End as MNC_FN_TYP_DSC, " +
                //" t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_vn_no, t01.mnc_vn_seq, t01.mnc_vn_sum, t01.mnc_doc_sts,m01.mnc_bday,m01.mnc_sex" +
                ", t07.mnc_app_dat, t07.mnc_app_tim, t07.mnc_app_dsc,m01.mnc_bday,m01.mnc_sex,t07.mnc_name,t07.MNC_REM_MEMO " +
                "From patient_t07 t07 " +
                " inner join patient_m01 m01 on t07.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                //" inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                " Where t07.MNC_DOT_CD = '" + dtrid + "' and t07.mnc_app_dat = '" + date + "'" +
                "and t07.MNC_STS <> 'C' " +
                " Order By t07.mnc_app_dat, t07.mnc_app_tim ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectAppointmentByDepNo(String depno, String date)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   t07.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t07.MNC_VN_NO,t07.MNC_VN_SEQ,t07.MNC_VN_SUM " +
                //"Case f02.MNC_FN_TYP_DSC " +
                //    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                //    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                //    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                //    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                //    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                //    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                //    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                //    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                //    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                //    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                //    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                //    "When '' Then '' " +
                //    "When '' Then '' " +
                //    "When '' Then '' " +
                //    "Else MNC_FN_TYP_DSC " +
                //    "End as MNC_FN_TYP_DSC, " +
                //" t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_vn_no, t01.mnc_vn_seq, t01.mnc_vn_sum, t01.mnc_doc_sts,m01.mnc_bday,m01.mnc_sex" +
                ", convert(VARCHAR(20),t07.mnc_app_dat,23) as mnc_app_dat, t07.mnc_app_tim, t07.mnc_app_dsc,convert(VARCHAR(20),m01.mnc_bday,23) as mnc_bday,m01.mnc_sex,t07.mnc_name,t07.MNC_REM_MEMO, convert(VARCHAR(20),t07.mnc_date,23) as mnc_date, t07.mnc_pre_no " +
                "From patient_t07 t07 " +
                " inner join patient_m01 m01 on t07.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                //" inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                " Where t07.MNC_dep_no = '" + depno + "' and t07.mnc_app_dat = '" + date + "'" +
                "and t07.MNC_STS <> 'C' " +
                " Order By t07.mnc_app_dat, t07.mnc_app_tim ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectAppointmentByHnDate(String hnno, String date)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   t07.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t07.MNC_VN_NO,t07.MNC_VN_SEQ,t07.MNC_VN_SUM " +
                //"Case f02.MNC_FN_TYP_DSC " +
                //    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                //    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                //    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                //    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                //    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                //    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                //    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                //    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                //    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                //    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                //    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                //    "When '' Then '' " +
                //    "When '' Then '' " +
                //    "When '' Then '' " +
                //    "Else MNC_FN_TYP_DSC " +
                //    "End as MNC_FN_TYP_DSC, " +
                //" t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_vn_no, t01.mnc_vn_seq, t01.mnc_vn_sum, t01.mnc_doc_sts,m01.mnc_bday,m01.mnc_sex" +
                ", convert(VARCHAR(20),t07.mnc_app_dat,23) as mnc_app_dat , t07.mnc_app_tim, t07.mnc_app_dsc,m01.mnc_bday,m01.mnc_sex,t07.mnc_name,t07.MNC_REM_MEMO" +
                ", convert(VARCHAR(20),t07.mnc_date,23) as mnc_date, t07.mnc_pre_no " +
                "From patient_t07 t07 " +
                " inner join patient_m01 m01 on t07.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                //" inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                " Where t07.MNC_hn_no = '" + hnno + "' and t07.mnc_date = '" + date + "'" +
                "and t07.MNC_STS <> 'C' " +
                " Order By t07.mnc_app_dat, t07.mnc_app_tim ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectPttinWard1(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select patient_t08.mnc_hn_no,patient_m02.MNC_PFIX_DSC as prefix,patient_m01.MNC_FNAME_T, " +
                "patient_m01.MNC_LNAME_T, convert(VARCHAR(20),patient_t08.MNC_AD_DATE,23) as MNC_AD_DATE,patient_t08.MNC_DS_DATE,patient_t08.MNC_AN_NO,patient_t08.MNC_AN_yr, " +
                "patient_m32.MNC_MD_DEP_DSC,patient_t08.MNC_RM_NAM,PATIENT_T08.MNC_BD_NO , " +
                "patient_t01.MNC_SHIF_MEMO,aa.MNC_PFIX_DSC,patient_m26.MNC_DOT_FNAME,patient_m26.MNC_DOT_LNAME,bb.MNC_MD_DEP_DSC as before, " +
                "DATEDIFF(DAY,MNC_AD_DATE,MNC_DS_DATE) as day, convert(VARCHAR(20),PATIENT_T01.mnc_date, 23) as mnc_date,PATIENT_T01.mnc_pre_no,PATIENT_M01.mnc_cur_tel,PATIENT_T08.MNC_DOT_CD_S, PATIENT_T08.MNC_DOT_CD_R, MNC_AD_time, " +
                "Case FINANCE_M02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC,PATIENT_T08.MNC_FN_TYP_CD " +
                "from PATIENT_T08 " +
                " inner join PATIENT_T01 on patient_t01.MNC_PRE_NO =PATIENT_T08.MNC_PRE_NO and patient_t01.MNC_date = PATIENT_T08.MNC_date " +
                " INNER JOIN dbo.PATIENT_M01 ON dbo.PATIENT_T08.MNC_HN_NO = dbo.PATIENT_M01.MNC_HN_NO  " +
                " INNER JOIN dbo.PATIENT_M02 ON dbo.PATIENT_M01.MNC_PFIX_CDT = dbo.PATIENT_M02.MNC_PFIX_CD " +
                "inner join	patient_m26 on PATIENT_T08.MNC_DOT_CD_S = patient_m26.MNC_DOT_CD " +
                "INNER JOIN	PATIENT_M32 ON PATIENT_T08.MNC_SEC_NO = PATIENT_M32.MNC_SEC_NO " +
                "INNER JOIN	FINANCE_M02 ON PATIENT_T08.MNC_FN_TYP_CD = FINANCE_M02.MNC_FN_TYP_CD " +
                "INNER JOIN	dbo.PATIENT_M02 as aa ON dbo.PATIENT_M26.MNC_DOT_PFIX = aa.MNC_PFIX_CD " +
                "inner join	PATIENT_M32 as bb ON bb.MNC_SEC_NO = PATIENT_M26.MNC_SEC_NO " +
                " WHERE   PATIENT_T08.MNC_AD_STS = 'A' and mnc_ds_lev = '1' and PATIENT_T08.mnc_hn_no = '" + hn + "' " +
                " Order By PATIENT_T08.mnc_ad_date, PATIENT_T08.mnc_ad_time ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectPttinDept(String deptid, string secid, String datestart, String dateend)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select PATIENT_T01.mnc_hn_no,patient_m02.MNC_PFIX_DSC as prefix,patient_m01.MNC_FNAME_T, " +
                "patient_m01.MNC_LNAME_T, convert(VARCHAR(20),PATIENT_T01.MNC_DATE,23) as MNC_DATE, " +
                " " +
                "patient_t01.MNC_SHIF_MEMO, " +
                "PATIENT_T01.mnc_pre_no,PATIENT_M01.mnc_cur_tel, MNC_time, " +
                "Case FINANCE_M02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC,PATIENT_T01.MNC_FN_TYP_CD " +
                "from  PATIENT_T01  " +
                " INNER JOIN dbo.PATIENT_M01 ON dbo.PATIENT_T01.MNC_HN_NO = dbo.PATIENT_M01.MNC_HN_NO and dbo.PATIENT_T01.MNC_HN_yr = dbo.PATIENT_M01.MNC_HN_yr  " +
                " INNER JOIN dbo.PATIENT_M02 ON dbo.PATIENT_M01.MNC_PFIX_CDT = dbo.PATIENT_M02.MNC_PFIX_CD " +
                "left join dbo.finance_m02 on finance_m02.MNC_FN_TYP_cd = patient_t01.MNC_FN_TYP_cd " +
                " WHERE   PATIENT_T01.MNC_STS <> 'C'  and patient_t01.MNC_SEC_NO ='" + secid + "' and patient_t01.MNC_DEP_NO = '"+ deptid + "' " +
                " and patient_t01.mnc_date >= '"+datestart+ "' and patient_t01.mnc_date <= '" + dateend + "' " +
                " Order By PATIENT_T01.mnc_date, PATIENT_T01.mnc_time ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectPttHiinDeptPaidCode(String deptid, string secid, String datestart, String dateend, String paidcode)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select PATIENT_T01.mnc_hn_no,patient_m02.MNC_PFIX_DSC as prefix,patient_m01.MNC_FNAME_T, " +
                "patient_m01.MNC_LNAME_T, convert(VARCHAR(20),PATIENT_T01.MNC_DATE,23) as MNC_DATE, " +
                " " +
                "patient_t01.MNC_SHIF_MEMO, " +
                "PATIENT_T01.mnc_pre_no,PATIENT_M01.mnc_cur_tel, MNC_time, " +
                "Case FINANCE_M02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC,PATIENT_T01.MNC_FN_TYP_CD " +
                    //", ptthi.status_drug,ptthi.status_xray,ptthi.status_authen,ptthi.status_pic_kyc,ptthi.queue_seq, ptthi.drug_set,ptthi.req_no_xray " +
                "from  PATIENT_T01  " +
                " INNER JOIN dbo.PATIENT_M01 ON dbo.PATIENT_T01.MNC_HN_NO = dbo.PATIENT_M01.MNC_HN_NO and dbo.PATIENT_T01.MNC_HN_yr = dbo.PATIENT_M01.MNC_HN_yr  " +
                " INNER JOIN dbo.PATIENT_M02 ON dbo.PATIENT_M01.MNC_PFIX_CDT = dbo.PATIENT_M02.MNC_PFIX_CD " +
                "left join dbo.finance_m02 on finance_m02.MNC_FN_TYP_cd = patient_t01.MNC_FN_TYP_cd " +
               // "inner join bn5_scan.dbo.t_patient_hi ptthi on PATIENT_T01.MNC_HN_NO = ptthi.hn and convert(VARCHAR(20),PATIENT_T01.mnc_date,23) = ptthi.visit_date and PATIENT_T01.mnc_pre_no = ptthi.pre_no " +
                " WHERE    " +
                "  patient_t01.mnc_date >= '" + datestart + "' and patient_t01.mnc_date <= '" + dateend + "' and PATIENT_T01.MNC_FN_TYP_CD = '" + paidcode+"' " +
                " Order By PATIENT_T01.mnc_date, PATIENT_T01.mnc_time ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectPttHiinDept(String deptid, string secid, String datestart, String dateend)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select PATIENT_T01.mnc_hn_no,patient_m02.MNC_PFIX_DSC as prefix,patient_m01.MNC_FNAME_T, " +
                "patient_m01.MNC_LNAME_T, convert(VARCHAR(20),PATIENT_T01.MNC_DATE,23) as MNC_DATE, " +
                " " +
                "patient_t01.MNC_SHIF_MEMO, " +
                "PATIENT_T01.mnc_pre_no,PATIENT_M01.mnc_cur_tel, MNC_time, " +
                "Case FINANCE_M02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC,PATIENT_T01.MNC_FN_TYP_CD , ptthi.status_drug,ptthi.status_xray,ptthi.status_authen,ptthi.status_pic_kyc,ptthi.queue_seq, ptthi.drug_set,ptthi.req_no_xray " +
                "from  PATIENT_T01  " +
                " INNER JOIN dbo.PATIENT_M01 ON dbo.PATIENT_T01.MNC_HN_NO = dbo.PATIENT_M01.MNC_HN_NO and dbo.PATIENT_T01.MNC_HN_yr = dbo.PATIENT_M01.MNC_HN_yr  " +
                " INNER JOIN dbo.PATIENT_M02 ON dbo.PATIENT_M01.MNC_PFIX_CDT = dbo.PATIENT_M02.MNC_PFIX_CD " +
                "left join dbo.finance_m02 on finance_m02.MNC_FN_TYP_cd = patient_t01.MNC_FN_TYP_cd " +
                "inner join bn5_scan.dbo.t_patient_hi ptthi on PATIENT_T01.MNC_HN_NO = ptthi.hn and convert(VARCHAR(20),PATIENT_T01.mnc_date,23) = ptthi.visit_date and PATIENT_T01.mnc_pre_no = ptthi.pre_no " +
                " WHERE   PATIENT_T01.MNC_STS <> 'C'  and patient_t01.MNC_SEC_NO ='" + secid + "' and patient_t01.MNC_DEP_NO = '" + deptid + "' " +
                " and patient_t01.mnc_date >= '" + datestart + "' and patient_t01.mnc_date <= '" + dateend + "' " +
                " Order By PATIENT_T01.mnc_date, PATIENT_T01.mnc_time ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectPttHiinDept1(String deptid, string secid, String datestart, String dateend)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select PATIENT_T01.mnc_hn_no,patient_m02.MNC_PFIX_DSC as prefix,patient_m01.MNC_FNAME_T, " +
                "patient_m01.MNC_LNAME_T, convert(VARCHAR(20),PATIENT_T01.MNC_DATE,23) as MNC_DATE, " +
                " " +
                "patient_t01.MNC_SHIF_MEMO, " +
                "PATIENT_T01.mnc_pre_no,PATIENT_M01.mnc_cur_tel, MNC_time, " +
                "Case FINANCE_M02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC,PATIENT_T01.MNC_FN_TYP_CD  " +
                "from  PATIENT_T01  " +
                " INNER JOIN dbo.PATIENT_M01 ON dbo.PATIENT_T01.MNC_HN_NO = dbo.PATIENT_M01.MNC_HN_NO and dbo.PATIENT_T01.MNC_HN_yr = dbo.PATIENT_M01.MNC_HN_yr  " +
                " INNER JOIN dbo.PATIENT_M02 ON dbo.PATIENT_M01.MNC_PFIX_CDT = dbo.PATIENT_M02.MNC_PFIX_CD " +
                "left join dbo.finance_m02 on finance_m02.MNC_FN_TYP_cd = patient_t01.MNC_FN_TYP_cd " +
                //"inner join bn5_scan.dbo.t_patient_hi ptthi on PATIENT_T01.MNC_HN_NO = ptthi.hn and convert(VARCHAR(20),PATIENT_T01.mnc_date,23) = ptthi.visit_date and PATIENT_T01.mnc_pre_no = ptthi.pre_no " +
                " WHERE   PATIENT_T01.MNC_STS <> 'C'  and patient_t01.MNC_SEC_NO ='" + secid + "' and patient_t01.MNC_DEP_NO = '" + deptid + "' " +
                " and patient_t01.mnc_date >= '" + datestart + "' and patient_t01.mnc_date <= '" + dateend + "' " +
                " Order By PATIENT_T01.mnc_date, PATIENT_T01.mnc_time ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectPttinWard2(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select top 1 patient_t08.mnc_hn_no,patient_m02.MNC_PFIX_DSC as prefix,patient_m01.MNC_FNAME_T, " +
                "patient_m01.MNC_LNAME_T, convert(VARCHAR(20),patient_t08.MNC_AD_DATE,23) as MNC_AD_DATE,patient_t08.MNC_DS_DATE,patient_t08.MNC_AN_NO,patient_t08.MNC_AN_yr, " +
                "patient_m32.MNC_MD_DEP_DSC,patient_t08.MNC_RM_NAM,PATIENT_T08.MNC_BD_NO , " +
                "patient_t01.MNC_SHIF_MEMO,aa.MNC_PFIX_DSC,patient_m26.MNC_DOT_FNAME,patient_m26.MNC_DOT_LNAME,bb.MNC_MD_DEP_DSC as before, " +
                "DATEDIFF(DAY,MNC_AD_DATE,MNC_DS_DATE) as day, convert(VARCHAR(20),PATIENT_T01.mnc_date, 23) as mnc_date,PATIENT_T01.mnc_pre_no,PATIENT_M01.mnc_cur_tel  " +
                "from PATIENT_T08 " +
                " inner join PATIENT_T01 on patient_t01.MNC_PRE_NO =PATIENT_T08.MNC_PRE_NO and patient_t01.MNC_date = PATIENT_T08.MNC_date " +
                " INNER JOIN dbo.PATIENT_M01 ON dbo.PATIENT_T08.MNC_HN_NO = dbo.PATIENT_M01.MNC_HN_NO  " +
                " INNER JOIN dbo.PATIENT_M02 ON dbo.PATIENT_M01.MNC_PFIX_CDT = dbo.PATIENT_M02.MNC_PFIX_CD " +
                "inner join	patient_m26 on PATIENT_T08.MNC_DOT_CD_S = patient_m26.MNC_DOT_CD " +
                "INNER JOIN	PATIENT_M32 ON PATIENT_T08.MNC_SEC_NO = PATIENT_M32.MNC_SEC_NO " +
                "INNER JOIN	FINANCE_M02 ON PATIENT_T08.MNC_FN_TYP_CD = FINANCE_M02.MNC_FN_TYP_CD " +
                "INNER JOIN	dbo.PATIENT_M02 as aa ON dbo.PATIENT_M26.MNC_DOT_PFIX = aa.MNC_PFIX_CD " +
                "inner join	PATIENT_M32 as bb ON bb.MNC_SEC_NO = PATIENT_M26.MNC_SEC_NO " +
                " WHERE    PATIENT_T08.mnc_hn_no = '" + hn + "' " +
                " Order By patient_t08.MNC_AN_yr desc, patient_t08.MNC_AN_NO desc, PATIENT_T08.mnc_ad_date, PATIENT_T08.mnc_ad_time ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectPttinWard(String wdno)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select patient_t08.mnc_hn_no,patient_m02.MNC_PFIX_DSC as prefix,patient_m01.MNC_FNAME_T, " +
                "patient_m01.MNC_LNAME_T,patient_t08.MNC_AD_DATE,patient_t08.MNC_DS_DATE,patient_t08.MNC_AN_NO, " +
                "patient_m32.MNC_MD_DEP_DSC,patient_t08.MNC_RM_NAM,PATIENT_T08.MNC_BD_NO , " +
                "patient_t01.MNC_SHIF_MEMO,aa.MNC_PFIX_DSC,patient_m26.MNC_DOT_FNAME,patient_m26.MNC_DOT_LNAME,bb.MNC_MD_DEP_DSC as before, " +
                "DATEDIFF(DAY,MNC_AD_DATE,MNC_DS_DATE) as day, convert(VARCHAR(20),PATIENT_T01.mnc_date, 23) as mnc_date,PATIENT_T01.mnc_pre_no " +
                "from PATIENT_T08 " +
                " inner join PATIENT_T01 on patient_t01.MNC_PRE_NO =PATIENT_T08.MNC_PRE_NO and patient_t01.MNC_date = PATIENT_T08.MNC_date " +
                " INNER JOIN dbo.PATIENT_M01 ON dbo.PATIENT_T08.MNC_HN_NO = dbo.PATIENT_M01.MNC_HN_NO  " +
                " INNER JOIN dbo.PATIENT_M02 ON dbo.PATIENT_M01.MNC_PFIX_CDT = dbo.PATIENT_M02.MNC_PFIX_CD " +
                "inner join	patient_m26 on PATIENT_T08.MNC_DOT_CD_S = patient_m26.MNC_DOT_CD " +
                "INNER JOIN	PATIENT_M32 ON PATIENT_T08.MNC_SEC_NO = PATIENT_M32.MNC_SEC_NO " +
                "INNER JOIN	FINANCE_M02 ON PATIENT_T08.MNC_FN_TYP_CD = FINANCE_M02.MNC_FN_TYP_CD " +
                "INNER JOIN	dbo.PATIENT_M02 as aa ON dbo.PATIENT_M26.MNC_DOT_PFIX = aa.MNC_PFIX_CD " +
                "inner join	PATIENT_M32 as bb ON bb.MNC_SEC_NO = PATIENT_M26.MNC_SEC_NO " +
                " WHERE   PATIENT_T08.MNC_AD_STS = 'A' and mnc_ds_lev = '1' and PATIENT_T08.mnc_wd_no = '" + wdno + "' " + 
                " Order By PATIENT_T08.mnc_ad_date, PATIENT_T08.mnc_ad_time ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectPttinWardByDate(String dateadmit)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select convert(VARCHAR(20),MNC_AD_DATE,23) as MNC_AD_DATE,PATIENT_T08.MNC_HN_NO ,patient_m02.MNC_PFIX_DSC,patient_m01.MNC_FNAME_T,PATIENT_T08.MNC_HN_yr, PATIENT_T08.mnc_pre_no ,PATIENT_T01.status_insert_pop, " +
                "patient_m01.MNC_LNAME_T,PATIENT_T08.MNC_AN_NO,MNC_AN_YR,PATIENT_M32.MNC_MD_DEP_DSC,MNC_RM_NAM ,MNC_BD_NO,MNC_SHIF_MEMO" +
                ",Case FINANCE_M02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC " +
                "from PATIENT_T08 " +
                " INNER JOIN dbo.PATIENT_M01 ON dbo.PATIENT_T08.MNC_HN_NO = dbo.PATIENT_M01.MNC_HN_NO " +
                " inner join PATIENT_M02 on patient_m01.MNC_PFIX_CDT = patient_m02.MNC_PFIX_CD  " +
                " INNER JOIN dbo.FINANCE_M02 ON dbo.PATIENT_T08.MNC_FN_TYP_CD= FINANCE_M02.MNC_FN_TYP_CD " +
                " INNER JOIN dbo.PATIENT_M32 ON dbo.PATIENT_T08.MNC_WD_NO= PATIENT_M32.MNC_MD_DEP_NO " +
                " left JOIN dbo.PATIENT_M24 on dbo.PATIENT_T08.MNC_COM_CD = dbo.PATIENT_M24.MNC_COM_CD " +
                " INNER JOIN dbo.PATIENT_T01 ON dbo.PATIENT_T08.MNC_HN_NO = dbo.PATIENT_T01.MNC_HN_NO and PATIENT_T08.MNC_PRE_NO = dbo.PATIENT_T01.MNC_PRE_NO and PATIENT_T08.MNC_DATE = dbo.PATIENT_T01.MNC_DATE " +

                "where PATIENT_T08.MNC_AD_STS  = 'A' and mnc_ds_lev = '1' and MNC_AD_DATE = '"+dateadmit+ "' and isnull(PATIENT_T01.status_insert_pop,'') <> '2' " +
                " Order By PATIENT_T08.mnc_ad_date, PATIENT_T08.mnc_ad_time ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectPttinWardByDate1(String dateadmit)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select convert(VARCHAR(20),pt01.MNC_DATE,23) as MNC_DATE,pt01.MNC_HN_NO ,patient_m02.MNC_PFIX_DSC,patient_m01.MNC_FNAME_T,pt01.MNC_HN_yr, pt01.mnc_pre_no ,pt01.status_insert_pop, " +
                "patient_m01.MNC_LNAME_T,pt01.MNC_SHIF_MEMO " +
                ",Case FINANCE_M02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC " +
                "from PATIENT_T01 pt01 " +
                " INNER JOIN dbo.PATIENT_M01 ON pt01.MNC_HN_NO = PATIENT_M01.MNC_HN_NO " +
                " inner join PATIENT_M02 on patient_m01.MNC_PFIX_CDT = patient_m02.MNC_PFIX_CD  " +
                " INNER JOIN dbo.FINANCE_M02 ON pt01.MNC_FN_TYP_CD= FINANCE_M02.MNC_FN_TYP_CD " +
                //" INNER JOIN dbo.PATIENT_M32 ON dbo.pt01.MNC_WD_NO= PATIENT_M32.MNC_MD_DEP_NO " +
                " left JOIN dbo.PATIENT_M24 on pt01.MNC_COM_CD = PATIENT_M24.MNC_COM_CD " +
                //" INNER JOIN dbo.PATIENT_T01 ON dbo.PATIENT_T08.MNC_HN_NO = dbo.PATIENT_T01.MNC_HN_NO and PATIENT_T08.MNC_PRE_NO = dbo.PATIENT_T01.MNC_PRE_NO and PATIENT_T08.MNC_DATE = dbo.PATIENT_T01.MNC_DATE " +

                "where pt01.MNC_STS  <> 'C' and pt01.MNC_DATE = '" + dateadmit + "' and isnull(pt01.status_insert_pop,'') <> '2' and pt01.MNC_SHIF_MEMO like 'โควิด-19%' " +
                " Order By pt01.mnc_date, pt01.mnc_time ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectPttinWardByDtr(String wdno)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select patient_t08.mnc_hn_no,patient_m02.MNC_PFIX_DSC as prefix,patient_m01.MNC_FNAME_T, " +
                "patient_m01.MNC_LNAME_T,convert(VARCHAR(20),patient_t08.MNC_AD_DATE, 23) as MNC_AD_DATE,patient_t08.MNC_DS_DATE,patient_t08.MNC_AN_NO, " +
                "patient_m32.MNC_MD_DEP_DSC,patient_t08.MNC_RM_NAM,PATIENT_T08.MNC_BD_NO , " +
                "patient_t01.MNC_SHIF_MEMO,aa.MNC_PFIX_DSC,patient_m26.MNC_DOT_FNAME,patient_m26.MNC_DOT_LNAME,bb.MNC_MD_DEP_DSC as before, " +
                "DATEDIFF(DAY,MNC_AD_DATE,MNC_DS_DATE) as day,PATIENT_M01.mnc_bday,PATIENT_T01.MNC_VN_NO,PATIENT_T01.MNC_VN_SEQ,PATIENT_T01.MNC_VN_SUM, " +
                "convert(VARCHAR(20),PATIENT_T01.mnc_date, 23) as mnc_date,PATIENT_T01.mnc_time,PATIENT_M01.mnc_sex,MNC_ref_dsc,MNC_SHIF_MEMO," +
                "bb.MNC_MD_DEP_DSC as MNC_MD_DEP_DSC1,PATIENT_T08.mnc_an_no, " +
                "Case FINANCE_M02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC " +
                "from PATIENT_T08 " +
                " inner join PATIENT_T01 on patient_t01.MNC_PRE_NO =PATIENT_T08.MNC_PRE_NO and patient_t01.MNC_date = PATIENT_T08.MNC_date " +
                " INNER JOIN dbo.PATIENT_M01 ON dbo.PATIENT_T08.MNC_HN_NO = dbo.PATIENT_M01.MNC_HN_NO  " +
                " INNER JOIN dbo.PATIENT_M02 ON dbo.PATIENT_M01.MNC_PFIX_CDT = dbo.PATIENT_M02.MNC_PFIX_CD " +
                "inner join	patient_m26 on PATIENT_T08.MNC_DOT_CD_S = patient_m26.MNC_DOT_CD " +
                "INNER JOIN	PATIENT_M32 ON PATIENT_T08.MNC_SEC_NO = PATIENT_M32.MNC_SEC_NO " +
                "INNER JOIN	FINANCE_M02 ON PATIENT_T08.MNC_FN_TYP_CD = FINANCE_M02.MNC_FN_TYP_CD " +
                "INNER JOIN	dbo.PATIENT_M02 as aa ON dbo.PATIENT_M26.MNC_DOT_PFIX = aa.MNC_PFIX_CD " +
                "inner join	PATIENT_M32 as bb ON bb.MNC_SEC_NO = PATIENT_M26.MNC_SEC_NO " +
                " WHERE   PATIENT_T08.MNC_AD_STS = 'A' and mnc_ds_lev = '1' and mnc_dot_cd_r = '" + wdno + "' " +
                "and PATIENT_T08.mnc_ds_cd is null " +
                " Order By PATIENT_T08.mnc_ad_date, PATIENT_T08.mnc_ad_time ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectPttinOPD(String wdno, String date)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select PATIENT_T01.mnc_hn_no,patient_m02.MNC_PFIX_DSC as prefix,patient_m01.MNC_FNAME_T, " +
                "patient_m01.MNC_LNAME_T,convert(VARCHAR(20),PATIENT_T01.mnc_date,23) as MNC_AD_DATE,'' as MNC_DS_DATE,'' as MNC_AN_NO, " +
                "patient_m32.MNC_MD_DEP_DSC,'' as MNC_RM_NAM,'' as MNC_BD_NO , " +
                "patient_t01.MNC_SHIF_MEMO,aa.MNC_PFIX_DSC,patient_m26.MNC_DOT_FNAME,patient_m26.MNC_DOT_LNAME,bb.MNC_MD_DEP_DSC as before, " +
                "'' as day,convert(VARCHAR(20),PATIENT_T01.mnc_date,23) as mnc_date,patient_t01.MNC_PRE_NO " +
                "from  PATIENT_T01  /*on patient_t01.MNC_PRE_NO =PATIENT_T08.MNC_PRE_NO and patient_t01.MNC_date = PATIENT_T08.MNC_date */" +
                " INNER JOIN dbo.PATIENT_M01 ON dbo.patient_t01.MNC_HN_NO = dbo.PATIENT_M01.MNC_HN_NO  " +
                " INNER JOIN dbo.PATIENT_M02 ON dbo.PATIENT_M01.MNC_PFIX_CDT = dbo.PATIENT_M02.MNC_PFIX_CD " +
                "inner join	patient_m26 on patient_t01.MNC_DOT_CD = patient_m26.MNC_DOT_CD " +
                "INNER JOIN	PATIENT_M32 ON patient_t01.MNC_SEC_NO = PATIENT_M32.MNC_SEC_NO " +
                "INNER JOIN	FINANCE_M02 ON PATIENT_T01.MNC_FN_TYP_CD = FINANCE_M02.MNC_FN_TYP_CD " +
                "INNER JOIN	dbo.PATIENT_M02 as aa ON dbo.PATIENT_M26.MNC_DOT_PFIX = aa.MNC_PFIX_CD " +
                "inner join	PATIENT_M32 as bb ON bb.MNC_SEC_NO = PATIENT_M26.MNC_SEC_NO " +
                " WHERE   PATIENT_T01.MNC_STS <> 'C'  and PATIENT_T01.MNC_DEP_NO = '" + wdno + "' and patient_t01.mnc_date ='"+date+"' " +
                " Order By PATIENT_T01.mnc_date desc, PATIENT_T01.mnc_time desc ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitByDtr1(String dtrid, String date)
        {
            String actno = "";
            //actno = flagVisit.Equals("finish") ? "610" : "110";
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_doc_sts,convert(VARCHAR(20),m01.mnc_bday,23) as mnc_bday,m01.mnc_sex" +
                ", convert(VARCHAR(20),t01.mnc_date,23) as mnc_date, t01.mnc_time, t01.mnc_weight, t01.mnc_high, t01.MNC_TEMP, t01.mnc_ratios, t01.mnc_breath, t01.mnc_bp1_l, t01.mnc_bp1_r, t01.mnc_bp2_l, t01.mnc_bp2_r,t01.MNC_pre_no, t01.mnc_ref_dsc" +
                ",bb.MNC_MD_DEP_DSC as MNC_MD_DEP_DSC1 " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                "inner join	patient_m26 on t01.MNC_DOT_CD = patient_m26.MNC_DOT_CD " +
                "inner join	PATIENT_M32 as bb ON bb.MNC_SEC_NO = PATIENT_M26.MNC_SEC_NO " +
                " Where t01.MNC_DOT_CD = '" + dtrid + "' and t01.mnc_date = '" + date + "'  " +
                "and t01.MNC_STS <> 'C' " +     // mnc_act_number 110 = ส่งพบแพทย์, 610 พยาบาล ปิดทำงาน ไปการเงิน
                " Order By t01.MNC_DATE, t01.MNC_TIME ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitByDtr(String dtrid, String date, String flagVisit)
        {
            String actno = "";
            actno = flagVisit.Equals("finish") ? "610" : "110";
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_doc_sts,convert(VARCHAR(20),m01.mnc_bday,23) as mnc_bday,m01.mnc_sex" +
                ", convert(VARCHAR(20),t01.mnc_date,23) as mnc_date, t01.mnc_time, t01.mnc_weight, t01.mnc_high, t01.MNC_TEMP, t01.mnc_ratios, t01.mnc_breath, t01.mnc_bp1_l, t01.mnc_bp1_r, t01.mnc_bp2_l, t01.mnc_bp2_r,t01.MNC_pre_no, t01.mnc_ref_dsc" +
                ",bb.MNC_MD_DEP_DSC as MNC_MD_DEP_DSC1 " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                "inner join	patient_m26 on t01.MNC_DOT_CD = patient_m26.MNC_DOT_CD " +
                "inner join	PATIENT_M32 as bb ON bb.MNC_SEC_NO = PATIENT_M26.MNC_SEC_NO " +
                " Where t01.MNC_DOT_CD = '" + dtrid + "' and t01.mnc_date = '" + date + "' and t01.mnc_act_no = '"+ actno + "' " +
                "and t01.MNC_STS <> 'C' " +     // mnc_act_number 110 = ส่งพบแพทย์, 610 พยาบาล ปิดทำงาน ไปการเงิน
                " Order By t01.MNC_DATE, t01.MNC_TIME ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitByHn(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +


                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD " +
                ", convert(VARCHAR(20),t01.mnc_date,23) as mnc_date,t01.mnc_pre_no, t01.mnc_sts_flg " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                " Where t01.MNC_HN_NO = '" + hn + "' " +
                "and t01.MNC_STS <> 'C' " +
                " Order by t01.MNC_HN_NO,t01.mnc_date ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitByHn(String hn, String datestart, String dateend)
        {
            String wherehn = "", wheredateend = "", wheredatestart = "", where = "";
            DataTable dt = new DataTable();
            if (hn.Length > 0)
            {
                wherehn = " t01.mnc_hn_no like '%" + hn + "%'";
            }
            if (dateend.Length > 0)
            {
                wheredateend = " t01.mnc_date <='" + dateend + "' ";
            }
            if (datestart.Length > 0)
            {
                wheredatestart = " t01.mnc_date >='" + datestart + "' ";
            }
            if ((datestart.Length >= 0) && (dateend.Length > 0) && (hn.Length > 0))
            {
                where = wheredatestart + " and " + wheredateend + " and " + wherehn;
            }
            else if ((datestart.Length > 0) && (dateend.Length <= 0) && (hn.Length > 0))
            {
                where = wheredatestart + " and " + wherehn;
            }
            else if ((datestart.Length <= 0) && (dateend.Length <= 0) && (hn.Length > 0))
            {
                where = wherehn;
            }
            String sql = "";
            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_doc_sts,m01.mnc_bday,m01.mnc_sex" +
                ", convert(VARCHAR(20),t01.mnc_date,23) as mnc_date, t01.mnc_time, t01.mnc_weight, t01.mnc_high, t01.MNC_TEMP, t01.mnc_ratios, t01.mnc_breath, t01.mnc_bp1_l, t01.mnc_bp1_r, t01.mnc_bp2_l, t01.mnc_bp2_r,t01.MNC_pre_no, t01.mnc_ref_dsc " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                " Where " + where +
                "and t01.MNC_STS <> 'C' " +
                " Order By t01.MNC_DATE, t01.MNC_TIME ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitByHn3(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +


                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_pre_no, convert(VARCHAR(20),t01.mnc_date,23) as mnc_date, t01.mnc_ref_dsc," +
                "t01_2.mnc_an_no, t01_2.MNC_PAT_FLAG, t01_2.mnc_an_yr, convert(VARCHAR(20),t01_2.mnc_ad_date,23) as mnc_ad_date, t01_2.mnc_an_yr, convert(VARCHAR(20),m01.MNC_bday,23) as MNC_bday " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                "Left Join patient_t01_2 t01_2 on t01.mnc_hn_no = t01_2.mnc_hn_no and t01.mnc_hn_yr = t01_2.mnc_hn_yr " +
                "and t01.mnc_date = t01_2.mnc_date and t01.mnc_pre_no = t01_2.mnc_pre_no " +
                " Where t01.MNC_HN_NO = '" + hn + "' " +
                "and t01.MNC_STS <> 'C' " +
                " Order by t01.MNC_HN_NO, t01.mnc_date desc ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitByHn4(String hn, String flag)
        {
            DataTable dt = new DataTable();
            String sql = "",whereflag="";
            if (flag.Equals("O"))
            {
                whereflag = " ";
            }
            else
            {
                whereflag = "and t01_2.MNC_PAT_FLAG = '" + flag + "' ";
            }
            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_pre_no, convert(VARCHAR(20),t01.mnc_date,23) as mnc_date, t01.mnc_ref_dsc," +
                "t01_2.mnc_an_no, t01_2.MNC_PAT_FLAG, t01_2.mnc_an_yr, convert(VARCHAR(20),t01_2.mnc_ad_date,23) as mnc_ad_date, t01_2.mnc_an_yr" +
                ", isnull(t01.mnc_high,'') as mnc_high, isnull(t01.mnc_weight,'') as mnc_weight, isnull(t01.mnc_temp,'') as mnc_temp, isnull(t01.mnc_cc,'') as mnc_cc, isnull(t01.mnc_cc_in,'') as mnc_cc_in" +
                ", isnull(t01.mnc_cc_ex,'') as mnc_cc_ex, isnull(t01.mnc_abc,'') as mnc_abc, isnull(t01.mnc_hc,'') as mnc_hc, isnull(t01.mnc_bp1_r,'') as mnc_bp1_r, isnull(t01.mnc_bp1_l,'') as mnc_bp1_l " +
                ", isnull(t01.mnc_bp2_r,'') as mnc_bp2_r, isnull(t01.mnc_bp2_l,'') as mnc_bp2_l, isnull(t01.mnc_breath,'') as mnc_breath, isnull(t01.mnc_cir_head,'') as mnc_cir_head, isnull(t01.mnc_ratios,'') as mnc_ratios, isnull(t01.mnc_bld_l,'') as mnc_bld_l, isnull(t01.mnc_bld_h,'') as mnc_bld_h " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                "Left Join patient_t01_2 t01_2 on t01.mnc_hn_no = t01_2.mnc_hn_no and t01.mnc_hn_yr = t01_2.mnc_hn_yr " +
                "and t01.mnc_date = t01_2.mnc_date and t01.mnc_pre_no = t01_2.mnc_pre_no " +
                " Where t01.MNC_HN_NO = '" + hn + "' " +
                "and t01.MNC_STS <> 'C'  " + whereflag +
                " Order by t01.MNC_HN_NO, t01.mnc_date desc ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitIPDByHn5(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "", whereflag = "";
            //if (flag.Equals("O"))
            //{
            //    whereflag = " ";
            //}
            //else
            //{
            //    whereflag = "and t01_2.MNC_PAT_FLAG = '" + flag + "' ";
            //}
            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_pre_no, convert(VARCHAR(20),t01.mnc_date,23) as mnc_date, t01.mnc_ref_dsc," +
                "t01_2.mnc_an_no, t01_2.mnc_an_yr, convert(VARCHAR(20),t01_2.mnc_ad_date,23) as mnc_ad_date, t01_2.mnc_an_yr " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO and t01.mnc_hn_yr = m01.mnc_hn_yr " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                "inner Join patient_t08 t01_2 on t01.mnc_hn_no = t01_2.mnc_hn_no and t01.mnc_hn_yr = t01_2.mnc_hn_yr " +
                "and t01.mnc_date = t01_2.mnc_date and t01.mnc_pre_no = t01_2.mnc_pre_no " +
                " Where t01.MNC_HN_NO = '" + hn + "' " +
                "and t01.MNC_STS <> 'C'  " + whereflag +
                " Order by t01.MNC_HN_NO, t01.mnc_date desc ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitByHn6(String hn, String flag)
        {
            DataTable dt = new DataTable();
            String sql = "", whereflag = "";
            if (flag.Equals("O"))
            {
                whereflag = " ";
            }
            else
            {
                whereflag = "and t01_2.MNC_PAT_FLAG = '" + flag + "' ";
            }
            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_pre_no, convert(VARCHAR(20),t01.mnc_date,23) as mnc_date, t01.mnc_ref_dsc," +
                //"t01_2.mnc_an_no, t01_2.MNC_PAT_FLAG, t01_2.mnc_an_yr, convert(VARCHAR(20),t01_2.mnc_ad_date,23) as mnc_ad_date, t01_2.mnc_an_yr" +
                "'' as mnc_an_no, '' as MNC_PAT_FLAG, '' as mnc_an_yr, '' as mnc_ad_date, ''as mnc_an_yr" +
                ", isnull(t01.mnc_high,'') as mnc_high, isnull(t01.mnc_weight,'') as mnc_weight, isnull(t01.mnc_temp,'') as mnc_temp, isnull(t01.mnc_cc,'') as mnc_cc, isnull(t01.mnc_cc_in,'') as mnc_cc_in" +
                ", isnull(t01.mnc_cc_ex,'') as mnc_cc_ex, isnull(t01.mnc_abc,'') as mnc_abc, isnull(t01.mnc_hc,'') as mnc_hc, isnull(t01.mnc_bp1_r,'') as mnc_bp1_r, isnull(t01.mnc_bp1_l,'') as mnc_bp1_l " +
                ", isnull(t01.mnc_bp2_r,'') as mnc_bp2_r, isnull(t01.mnc_bp2_l,'') as mnc_bp2_l, isnull(t01.mnc_breath,'') as mnc_breath, isnull(t01.mnc_cir_head,'') as mnc_cir_head, isnull(t01.mnc_ratios,'') as mnc_ratios" +
                ", isnull(t01.mnc_bld_l,'') as mnc_bld_l, isnull(t01.mnc_bld_h,'') as mnc_bld_h, t01.MNC_ID_nam " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                //"Left Join patient_t01_2 t01_2 on t01.mnc_hn_no = t01_2.mnc_hn_no and t01.mnc_hn_yr = t01_2.mnc_hn_yr " +
                //"and t01.mnc_date = t01_2.mnc_date and t01.mnc_pre_no = t01_2.mnc_pre_no " +
                " Where t01.MNC_HN_NO = '" + hn + "' " 
                //+"and t01.MNC_STS <> 'C'  " 
                + whereflag +
                " Order by t01.MNC_HN_NO, t01.mnc_date desc ";
            //new LogWriter("e", "VisitDB selectVisitByHn6  " + sql );
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitByLikeHn(String hn, String datestart, String dateend)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +


                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_pre_no, convert(VARCHAR(20),t01.mnc_date,23) as mnc_date, t01.mnc_ref_dsc," +
                "t01_2.mnc_an_no, t01_2.MNC_PAT_FLAG, t01_2.mnc_an_yr, convert(VARCHAR(20),t01_2.mnc_ad_date,23) as mnc_ad_date, t01_2.mnc_an_yr,m01.mnc_bday, t01.mnc_time,m01.mnc_sex " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                "Left Join patient_t01_2 t01_2 on t01.mnc_hn_no = t01_2.mnc_hn_no and t01.mnc_hn_yr = t01_2.mnc_hn_yr " +
                "and t01.mnc_date = t01_2.mnc_date and t01.mnc_pre_no = t01_2.mnc_pre_no " +
                " Where t01.MNC_HN_NO in (" + hn + ") " +
                "and t01.MNC_STS <> 'C' " +
                "and t01.mnc_date >= '" + datestart + "' and t01.mnc_date <= '" + dateend + "' " +
                //"and t01_2.MNC_PAT_FLAG = '" + flag + "' " +
                " Order by t01.MNC_HN_NO, t01.mnc_date desc ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitByDatePaidType(String hn,String paidtype, String datestart, String dateend)
        {
            DataTable dt = new DataTable();
            String sql = "", wherehn="", wherepaidtype="";
            if (hn.Length > 0)
            {
                wherehn = " and t01.mnc_hn_no = '"+hn+"' ";
            }
            if (paidtype.Length > 0 && !paidtype.Equals("''"))
            {
                wherepaidtype = " and t01.mnc_fn_typ_cd in (" + paidtype + ") ";
            }
            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +


                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_pre_no, convert(VARCHAR(20),t01.mnc_date,23) as mnc_date, t01.mnc_ref_dsc," +
                "t01_2.mnc_an_no, t01_2.MNC_PAT_FLAG, t01_2.mnc_an_yr, convert(VARCHAR(20),t01_2.mnc_ad_date,23) as mnc_ad_date, t01_2.mnc_an_yr,convert(VARCHAR(20),m01.mnc_bday,23) as mnc_bday, t01.mnc_time,m01.mnc_sex " +
                ",t01.mnc_pre_no,t01.MNC_HN_YR,m01.MNC_CUR_CHW,m01.MNC_CUR_TUM,m01.MNC_CUR_AMP,m01.mnc_pfix_cdt, m01.mnc_nat_cd, m01.mnc_id_no, t01.mnc_temp " +
                ", isnull(t01.mnc_high,'') as mnc_high, isnull(t01.mnc_weight,'') as mnc_weight, isnull(t01.mnc_temp,'') as mnc_temp, isnull(t01.mnc_cc,'') as mnc_cc, isnull(t01.mnc_cc_in,'') as mnc_cc_in" +
                ", isnull(t01.mnc_cc_ex,'') as mnc_cc_ex, isnull(t01.mnc_abc,'') as mnc_abc, isnull(t01.mnc_hc,'') as mnc_hc, isnull(t01.mnc_bp1_r,'') as mnc_bp1_r, isnull(t01.mnc_bp1_l,'') as mnc_bp1_l " +
                ", isnull(t01.mnc_bp2_r,'') as mnc_bp2_r, isnull(t01.mnc_bp2_l,'') as mnc_bp2_l, isnull(t01.mnc_breath,'') as mnc_breath, isnull(t01.mnc_cir_head,'') as mnc_cir_head, isnull(t01.mnc_ratios,'') as mnc_ratios" +
                ", isnull(t01.mnc_bld_l,'') as mnc_bld_l, isnull(t01.mnc_bld_h,'') as mnc_bld_h, t01.MNC_ID_nam,t01.mnc_dot_cd, f02.opbkk_inscl, m01.MNC_STATUS,t01_2.MNC_PAT_FLAG " +

                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO and t01.mnc_hn_yr = m01.mnc_hn_yr " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                "Left Join patient_t01_2 t01_2 on t01.mnc_hn_no = t01_2.mnc_hn_no and t01.mnc_hn_yr = t01_2.mnc_hn_yr " +
                "and t01.mnc_date = t01_2.mnc_date and t01.mnc_pre_no = t01_2.mnc_pre_no " +
                //" Where t01.mnc_fn_typ_cd in (" + paidtype + ") " +
                " Where  " +
                " t01.MNC_STS <> 'C' " +
                "and t01.mnc_date >= '" + datestart + "' and t01.mnc_date <= '" + dateend + "' " + wherehn+ wherepaidtype+
                //"and t01_2.MNC_PAT_FLAG = '" + flag + "' " +
                " Order by  t01.mnc_date asc, t01.mnc_pre_no ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitByHn5(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select top 1  t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +

                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_pre_no, t01.mnc_breath, t01.mnc_high,t01.mnc_bp1_l,t01.mnc_temp,t01.mnc_weight,m01.*, m07.mnc_tum_dsc, m08.mnc_amp_dsc, m09.mnc_chw_dsc, t01.mnc_ratios" +
                ", convert(VARCHAR(20),m01.mnc_bday,23) as mnc_bday, m01.MNC_NAT_CD,m04.MNC_NAT_DSC,m04.MNC_NAT_DSC_e, convert(VARCHAR(20),t01.mnc_date,23) as mnc_date, t01.mnc_time" +
                ", t01.MNC_DOT_CD as doctor_id " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                " left join patient_m07 m07 ON m01.MNC_cur_tum = m07.MNC_tum_cd " +
                " left join patient_m08 m08 ON m01.MNC_cur_amp = m08.MNC_amp_cd " +
                " left join patient_m09 m09 ON m01.MNC_cur_chw = m09.MNC_chw_cd " +
                " left join patient_m04 m04 ON m01.MNC_nat_cd = m04.MNC_nat_cd " +
                " Where t01.MNC_HN_NO = '" + hn + "' " +
                //"and t01.MNC_STS <> 'C' " +
                //"and t01.mnc_shif_memo like '%สุขภาพ%'" +
                " Order by t01.MNC_HN_NO, t01.mnc_date desc ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitByHn3(String hn, String visitDate)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +


                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_pre_no, t01.mnc_breath, t01.mnc_high,t01.mnc_bp1_l,t01.mnc_temp,t01.mnc_weight,m01.*, m07.mnc_tum_dsc, m08.mnc_amp_dsc, m09.mnc_chw_dsc, t01.mnc_ratios " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                " left join patient_m07 m07 ON m01.MNC_cur_tum = m07.MNC_tum_cd " +
                " left join patient_m08 m08 ON m01.MNC_cur_amp = m08.MNC_amp_cd " +
                " left join patient_m09 m09 ON m01.MNC_cur_chw = m09.MNC_chw_cd " +
                " Where t01.MNC_HN_NO = '" + hn + "' " +
                //"and t01.MNC_STS <> 'C' " +
                //"and t01.mnc_shif_memo like '%สุขภาพ%'" +
                " Order by t01.MNC_HN_NO ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitByHn2(String hn, String visitDate)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD, t01.mnc_pre_no, t01.mnc_breath, t01.mnc_high,t01.mnc_bp1_l,t01.mnc_temp,t01.mnc_weight,m01.*, m07.mnc_tum_dsc, m08.mnc_amp_dsc, m09.mnc_chw_dsc, t01.mnc_ratios " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                " left join patient_m07 m07 ON m01.MNC_cur_tum = m07.MNC_tum_cd " +
                " left join patient_m08 m08 ON m01.MNC_cur_amp = m08.MNC_amp_cd " +
                " left join patient_m09 m09 ON m01.MNC_cur_chw = m09.MNC_chw_cd " +
                " Where t01.MNC_HN_NO = '" + hn + "' " +
                "and t01.MNC_STS <> 'C' and t01.mnc_shif_memo like '%สุขภาพ%'" +
                " Order by t01.MNC_HN_NO ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectVisitByHn1(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            //sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
            //    "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, "+
            //    "Case f02.MNC_FN_TYP_DSC " +
            //        "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
            //        "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
            //        "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' "+
            //        "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
            //        "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
            //        "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' "+
            //        "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
            //        "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
            //        "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +
                    
                    
            //        "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
            //        "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
            //        "When '' Then '' " +
            //        "When '' Then '' " +
            //        "When '' Then '' " +
            //        "Else MNC_FN_TYP_DSC " +
            //        "End as MNC_FN_TYP_DSC, " +
            //    " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD,t01.MNC_DATE,t01.MNC_time " +
            //    "From patient_t01 t01 " +
            //    " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
            //    " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
            //    " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
            //    " Where t01.MNC_HN_NO like '" + hn + "%' " +
            //    "and t01.MNC_STS <> 'C' " +
            //    " Order by t01.MNC_HN_NO ";

            //sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
            //    "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
            //    "Case f02.MNC_FN_TYP_DSC " +
            //        "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
            //        "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
            //        "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
            //        "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
            //        "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
            //        "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
            //        "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
            //        "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
            //        "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +


            //        "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
            //        "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
            //        "When '' Then '' " +
            //        "When '' Then '' " +
            //        "When '' Then '' " +
            //        "Else MNC_FN_TYP_DSC " +
            //        "End as MNC_FN_TYP_DSC, " +
            //    " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD,t01.MNC_DATE,t01.MNC_time,LAB_T01.MNC_REQ_NO,LAB_T01.MNC_DOT_CD,LAB_T01.MNC_REQ_DAT,LAB_T01.MNC_REQ_TIM, "+
            //    "m26.MNC_DOT_FNAME,m26.MNC_DOT_LNAME,m021.MNC_PFIX_DSC as prefixdoc, " +
            //    "isnull(FLOOR((CAST (GetDate() AS INTEGER) - CAST( MNC_BDAY AS INTEGER)) / 365.25),0) AS Age, t01.mnc_pre_no " +
            //    "From patient_t01 t01 " +
            //    " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
            //    " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
            //    " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
            //    " inner join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE  " +
            //    " left join patient_m26 m26 ON LAB_T01.MNC_DOT_CD = m26.MNC_DOT_CD " +
            //    " inner join patient_m02 m021 on m26.MNC_DOT_PFIX =m021.MNC_PFIX_CD " +
            //    " Where t01.MNC_HN_NO like '" + hn + "%' " +
            //    "and t01.MNC_STS <> 'C' " +
            //    " Order by t01.MNC_HN_NO ";

            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM, " +
                "Case f02.MNC_FN_TYP_DSC " +
                    "When 'ประกันสังคม (บ.1)' Then 'ปกส(บ.1)' " +
                    "When 'ประกันสังคม (บ.2)' Then 'ปกส(บ.2)' " +
                    "When 'ประกันสังคม (บ.5)' Then 'ปกส(บ.5)' " +
                    "When 'ประกันสังคมอิสระ (บ.1)' Then 'ปกต(บ.1)' " +
                    "When 'ประกันสังคมอิสระ (บ.5)' Then 'ปกต(บ.5)' " +
                    "When 'ตรวจสุขภาพ (เงินสด)' Then 'ตส(เงินสด)' " +
                    "When 'ตรวจสุขภาพ (บริษัท)' Then 'ตส(บริษัท)' " +
                    "When 'ตรวจสุขภาพ (PACKAGE)' Then 'ตส(PACKAGE)' " +
                    "When 'ลูกหนี้ประกันสังคม รพ.เมืองสมุทรปากน้ำ' Then 'ลูกหนี้(ปากน้ำ)' " +


                    "When 'ลูกหนี้บางนา 1' Then 'ลูกหนี้(บ.1)' " +
                    "When 'บริษัทประกัน' Then 'บ.ประกัน' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "When '' Then '' " +
                    "Else MNC_FN_TYP_DSC " +
                    "End as MNC_FN_TYP_DSC, " +
                " t01.MNC_SHIF_MEMO,t01.MNC_FN_TYP_CD,t01.MNC_DATE,t01.MNC_time,LAB_T01.MNC_REQ_NO,LAB_T01.MNC_DOT_CD,LAB_T01.MNC_REQ_DAT,LAB_T01.MNC_REQ_TIM, " +
                "m26.MNC_DOT_FNAME,m26.MNC_DOT_LNAME,m021.MNC_PFIX_DSC as prefixdoc, " +
                "isnull(FLOOR((CAST (GetDate() AS INTEGER) - CAST( MNC_BDAY AS INTEGER)) / 365.25),0) AS Age, t01.mnc_pre_no, LAB_M01.MNC_LB_DSC" +
                ",t01.mnc_pre_no,t01.MNC_HN_YR, m02.MNC_PFIX_DSC + ' ' + m01.MNC_FNAME_T + ' ' + m01.MNC_LNAME_T as fullname " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                " inner join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE  " +
                " inner join patient_m26 m26 ON LAB_T01.MNC_DOT_CD = m26.MNC_DOT_CD " +
                " inner join patient_m02 m021 on m26.MNC_DOT_PFIX =m021.MNC_PFIX_CD " +
                " inner join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T05.MNC_REQ_YR = LAB_T01.MNC_REQ_YR "+
                " inner join LAB_M01 ON LAB_T05.MNC_LB_CD = LAB_M01.MNC_LB_CD "+
                " Where t01.MNC_HN_NO like '" + hn + "%' " +
                "and t01.MNC_STS <> 'C'  and LAB_M01.MNC_LB_DSC like '%Out lab%' and LAB_T05.MNC_LB_RES_CD = '01' " +
                " Order by t01.MNC_HN_NO ";

            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectDrugUcepByHn(String hn, String hnyr, String vsdate, String preno)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,sum(phart06.MNC_PH_QTY) as qty,phart06.MNC_PH_PRI    " +
                ", convert(VARCHAR(20),phart05.mnc_req_dat,23) as mnc_req_dat,phart06.MNC_PH_DIR_DSC,pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd " +
                ", phart05.mnc_hn_no, t01.MNC_ID_Nam, phart05.mnc_pre_no,pharmacy_m01.tmt_code, phart06.mnc_usr_add, fnm01.mnc_grp_ss1,phart05.MNC_REQ_TIM,PHARMACY_M01.MNC_fn_CD " +
                "From PHARMACY_T06 phart06  " +
                "left join PHARMACY_T05 phart05 on phart05.MNC_CFR_NO = phart06.MNC_CFR_NO and phart05.MNC_CFG_DAT = phart06.MNC_CFR_dat  " +
                "left join PHARMACY_M01 on phart06.MNC_PH_CD = pharmacy_m01.MNC_PH_CD  " +
                "Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD  " +
                "inner join patient_t01 t01 on phart05.MNC_hn_no = t01.mnc_hn_no and phart05.mnc_hn_yr = t01.mnc_hn_yr and phart05.mnc_pre_no = t01.mnc_pre_no and phart05.mnc_date = t01.mnc_date " +
                "Left join finance_m01 fnm01 on PHARMACY_M01.MNC_fn_CD = fnm01.MNC_fn_CD " +
                "where  " +
                " phart05.mnc_hn_no = '" + hn + "'  " +
                ////" and t01.mnc_vn_no = '" + vn + "' " +
                //"and phart05.MNC_PRE_NO = '" + preno + "' " +
                //"and phart05.MNC_DATE = '" + vsdate + "' " +
                " and phart05.MNC_CFR_STS = 'a' " +
                " and phart05.MNC_DATE = '" + vsdate + "' " +
                " and phart05.mnc_pre_no = " + preno + " " +
                //"and phart06.MNC_DOC_CD <> 'RIP' " +
                "Group By phart05.mnc_hn_no, t01.MNC_ID_Nam, phart05.mnc_pre_no, phart05.mnc_req_dat, phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,phart06.MNC_PH_DIR_DSC" +
                ", pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd,pharmacy_m01.tmt_code, phart06.mnc_usr_add, fnm01.mnc_grp_ss1,phart05.MNC_REQ_TIM,PHARMACY_M01.MNC_fn_CD ,phart06.MNC_PH_PRI " +
                "Order By phart05.mnc_hn_no, phart05.mnc_req_dat, phart06.MNC_PH_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectOrderDrugUcepIPDByHn(String hn, String hnyr, String anno, String anyear)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,sum(phart06.MNC_PH_QTY) as qty    " +
                ", convert(VARCHAR(20),phart05.mnc_req_dat,23) as mnc_req_dat,phart06.MNC_PH_DIR_DSC,pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd " +
                ", phart05.mnc_hn_no, t01.MNC_ID_Nam, phart05.mnc_pre_no,pharmacy_m01.tmt_code, phart06.mnc_usr_add, fnm01.mnc_grp_ss1,phart05.MNC_REQ_TIM,phart06.MNC_PH_PRI,PHARMACY_M01.MNC_fn_CD " +
                "From PHARMACY_T06 phart06  " +
                "left join PHARMACY_T05 phart05 on phart05.MNC_CFR_NO = phart06.MNC_CFR_NO and phart05.MNC_CFG_DAT = phart06.MNC_CFR_dat  " +
                "left join PHARMACY_M01 on phart06.MNC_PH_CD = pharmacy_m01.MNC_PH_CD  " +
                "Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD  " +
                "inner join patient_t01 t01 on phart05.MNC_hn_no = t01.mnc_hn_no and phart05.mnc_hn_yr = t01.mnc_hn_yr and phart05.mnc_pre_no = t01.mnc_pre_no and phart05.mnc_date = t01.mnc_date " +
                "Left join finance_m01 fnm01 on PHARMACY_M01.MNC_fn_CD = fnm01.MNC_fn_CD " +
                "where  " +
                " phart05.mnc_hn_no = '" + hn + "'  " +
                ////" and t01.mnc_vn_no = '" + vn + "' " +
                //"and phart05.MNC_PRE_NO = '" + preno + "' " +
                //"and phart05.MNC_DATE = '" + vsdate + "' " +
                " and phart05.MNC_CFR_STS = 'a' " +
                //" and phart05.MNC_DATE = '" + andate + "' " +
                " and phart05.mnc_an_no = '" + anno + "' " +
                " and phart05.mnc_an_yr = '" + anyear + "' " +
                "Group By phart05.mnc_hn_no, t01.MNC_ID_Nam, phart05.mnc_pre_no, phart05.mnc_req_dat, phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,phart06.MNC_PH_DIR_DSC" +
                ", pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd,pharmacy_m01.tmt_code, phart06.mnc_usr_add, fnm01.mnc_grp_ss1,phart05.MNC_REQ_TIM,phart06.MNC_PH_PRI,PHARMACY_M01.MNC_fn_CD " +
                "Order By phart05.mnc_hn_no, phart06.MNC_PH_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectOrderDrugUcepIPDByHnDOCcd(String hn, String hnyr, String anno, String anyear, String paidtype)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,sum(phart06.MNC_PH_QTY) as qty    " +
                ", convert(VARCHAR(20),phart05.mnc_req_dat,23) as mnc_req_dat,phart06.MNC_PH_DIR_DSC,pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd " +
                ", phart05.mnc_hn_no, t01.MNC_ID_Nam, phart05.mnc_pre_no,pharmacy_m01.tmt_code, phart06.mnc_usr_add, fnm01.mnc_grp_ss1,phart05.MNC_REQ_TIM,phart06.MNC_PH_PRI,PHARMACY_M01.MNC_fn_CD " +
                "From PHARMACY_T06 phart06  " +
                "left join PHARMACY_T05 phart05 on phart05.MNC_CFR_NO = phart06.MNC_CFR_NO and phart05.MNC_CFG_DAT = phart06.MNC_CFR_dat  " +
                "left join PHARMACY_M01 on phart06.MNC_PH_CD = pharmacy_m01.MNC_PH_CD  " +
                "Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD  " +
                "inner join patient_t01 t01 on phart05.MNC_hn_no = t01.mnc_hn_no and phart05.mnc_hn_yr = t01.mnc_hn_yr and phart05.mnc_pre_no = t01.mnc_pre_no and phart05.mnc_date = t01.mnc_date " +
                "Left join finance_m01 fnm01 on PHARMACY_M01.MNC_fn_CD = fnm01.MNC_fn_CD " +
                "where  " +
                " phart05.mnc_hn_no = '" + hn + "'  " +
                ////" and t01.mnc_vn_no = '" + vn + "' " +
                //"and phart05.MNC_PRE_NO = '" + preno + "' " +
                //"and phart05.MNC_DATE = '" + vsdate + "' " +
                " and phart05.MNC_CFR_STS = 'a' " +
                //" and phart05.MNC_DATE = '" + andate + "' " +
                " and phart05.mnc_an_no = '" + anno + "' " +
                //" and phart05.mnc_an_yr = '" + anyear + "' " +
                " and phart05.MNC_fn_typ_cd = '" + paidtype + "' " +
                "Group By phart05.mnc_hn_no, t01.MNC_ID_Nam, phart05.mnc_pre_no, phart05.mnc_req_dat, phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,phart06.MNC_PH_DIR_DSC" +
                ", pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd,pharmacy_m01.tmt_code, phart06.mnc_usr_add, fnm01.mnc_grp_ss1,phart05.MNC_REQ_TIM,phart06.MNC_PH_PRI,PHARMACY_M01.MNC_fn_CD " +
                "Order By phart05.mnc_hn_no, phart05.mnc_req_dat, phart06.MNC_PH_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectOrderDrugUcepIPDByHnDOCcd(String hn, String hnyr, String anno, String anyear, String doccd, String docyr, String docno, String docdate)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,sum(phart06.MNC_PH_QTY) as qty    " +
                ", convert(VARCHAR(20),phart05.mnc_req_dat,23) as mnc_req_dat,phart06.MNC_PH_DIR_DSC,pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd " +
                ", phart05.mnc_hn_no, t01.MNC_ID_Nam, phart05.mnc_pre_no,pharmacy_m01.tmt_code, phart06.mnc_usr_add, fnm01.mnc_grp_ss1,phart05.MNC_REQ_TIM,phart06.MNC_PH_PRI,PHARMACY_M01.MNC_fn_CD " +
                "From PHARMACY_T06 phart06  " +
                "left join PHARMACY_T05 phart05 on phart05.MNC_CFR_NO = phart06.MNC_CFR_NO and phart05.MNC_CFG_DAT = phart06.MNC_CFR_dat  " +
                "left join PHARMACY_M01 on phart06.MNC_PH_CD = pharmacy_m01.MNC_PH_CD  " +
                "Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD  " +
                "inner join patient_t01 t01 on phart05.MNC_hn_no = t01.mnc_hn_no and phart05.mnc_hn_yr = t01.mnc_hn_yr and phart05.mnc_pre_no = t01.mnc_pre_no and phart05.mnc_date = t01.mnc_date " +
                "Left join finance_m01 fnm01 on PHARMACY_M01.MNC_fn_CD = fnm01.MNC_fn_CD " +
                "where  " +
                " phart05.mnc_hn_no = '" + hn + "'  " +
                ////" and t01.mnc_vn_no = '" + vn + "' " +
                //"and phart05.MNC_PRE_NO = '" + preno + "' " +
                //"and phart05.MNC_DATE = '" + vsdate + "' " +
                " and phart05.MNC_CFR_STS = 'a' " +
                //" and phart05.MNC_DATE = '" + andate + "' " +
                " and phart05.mnc_an_no = '" + anno + "' " +
                " and phart05.mnc_an_yr = '" + anyear + "' " +
                " and phart05.MNC_DOC_CD = '" + doccd + "' " +
                " and phart05.MNC_CFR_YR = '" + docyr + "' " +
                " and phart05.MNC_CFR_NO = '" + docno + "' " +
                "Group By phart05.mnc_hn_no, t01.MNC_ID_Nam, phart05.mnc_pre_no, phart05.mnc_req_dat, phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,phart06.MNC_PH_DIR_DSC" +
                ", pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd,pharmacy_m01.tmt_code, phart06.mnc_usr_add, fnm01.mnc_grp_ss1,phart05.MNC_REQ_TIM,phart06.MNC_PH_PRI,PHARMACY_M01.MNC_fn_CD " +
                "Order By phart05.mnc_hn_no, phart05.mnc_req_dat, phart06.MNC_PH_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectOrderFinanceUcepIPDByHn(String hn, String hnyr, String anno, String anyear)
        {
            DataTable dt = new DataTable();
            String sql = "";

            //64-05-06  อ้วนแจ้งว่า ข้อมูล double ทำให้ ต้อง distinct
            //sql = "Select convert(VARCHAR(20),ft031.MNC_FN_DAT,23) as MNC_FN_DAT, ft031.MNC_FN_TIME,ft031.MNC_FN_CD, ft031.MNC_FN_AMT,fm01.MNC_FN_DSCT,fm01.mnc_charge_cd, fm01.mnc_sub_charge_cd, fm01.ucep_code,ft031.mnc_fn_cd    " +
            //    " " +

            //    "from FINANCE_T03_1 ft031  " +
            //    "inner join finance_m01 fm01 on ft031.mnc_fn_cd =  fm01.mnc_fn_cd and ft031.MNC_APP_CD2 = 'ADJ' " +
            //    "  " +

            //    "where  " +
            //    " ft031.MNC_HN_NO = '" + hn + "'  " +
            //    ////" and t01.mnc_vn_no = '" + vn + "' " +
            //    //"and phart05.MNC_PRE_NO = '" + preno + "' " +
            //    //"and phart05.MNC_DATE = '" + vsdate + "' " +
            //    //" and pt16.MNC_req_STS = 'a' " +
            //    //" and ft031.MNC_ad_DAT = '" + andate + "' " +
            //    " and ft031.MNC_AN_NO = '" + anno + "' " +
            //    " and ft031.MNC_AN_yr ='" + anyear + "' " +
            //    //"Group By pm30.MNC_SR_DSC, pt16.mnc_sr_cd,pt16.mnc_req_dat,pt16.mnc_stamp_tim,pt16.MNC_SR_PRI,pm30.ucep_code " +
            //    "order by ft031.MNC_FN_DAT, ft031.MNC_FN_TIME, ft031.MNC_FN_NO ";

            sql = "Select distinct convert(VARCHAR(20),ft031.MNC_FN_DAT,23) as MNC_FN_DAT, ft031.MNC_FN_TIME,ft031.MNC_FN_CD, ft031.MNC_FN_AMT,fm01.MNC_FN_DSCT,fm01.mnc_charge_cd, fm01.mnc_sub_charge_cd, fm01.ucep_code,ft031.mnc_fn_cd    " +
                " " +

                "from FINANCE_T03_1 ft031  " +
                "inner join finance_m01 fm01 on ft031.mnc_fn_cd =  fm01.mnc_fn_cd and ft031.MNC_APP_CD2 = 'ADJ' " +
                "  " +

                "where  " +
                " ft031.MNC_HN_NO = '" + hn + "'  " +
                ////" and t01.mnc_vn_no = '" + vn + "' " +
                //"and phart05.MNC_PRE_NO = '" + preno + "' " +
                //"and phart05.MNC_DATE = '" + vsdate + "' " +
                //" and pt16.MNC_req_STS = 'a' " +
                //" and ft031.MNC_ad_DAT = '" + andate + "' " +
                " and ft031.MNC_AN_NO = '" + anno + "' " +
                " and ft031.MNC_AN_yr ='" + anyear + "' " +
                //"Group By pm30.MNC_SR_DSC, pt16.mnc_sr_cd,pt16.mnc_req_dat,pt16.mnc_stamp_tim,pt16.MNC_SR_PRI,pm30.ucep_code " +
                "-- order by ft031.MNC_FN_DAT, ft031.MNC_FN_TIME, ft031.MNC_FN_NO ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectOrderFinanceUcepIPDByHnDOCcd(String hn, String hnyr, String anno, String anyear, String doccd, String docyr, String docno, String docdate)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select convert(VARCHAR(20),ft031.MNC_FN_DAT,23) as MNC_FN_DAT, ft031.MNC_FN_TIME,ft031.MNC_FN_CD, ft031.MNC_FN_AMT,fm01.MNC_FN_DSCT,fm01.mnc_charge_cd, fm01.mnc_sub_charge_cd, fm01.ucep_code,ft031.mnc_fn_cd    " +
                " " +

                "from FINANCE_T03_1 ft031  " +
                "inner join finance_m01 fm01 on ft031.mnc_fn_cd =  fm01.mnc_fn_cd and ft031.MNC_APP_CD2 = 'ADJ' " +
                "  " +

                "where  " +
                " ft031.MNC_HN_NO = '" + hn + "'  " +
               ////" and t01.mnc_vn_no = '" + vn + "' " +
               //"and phart05.MNC_PRE_NO = '" + preno + "' " +
               " and ft031.MNC_DOC_YR = '" + docyr + "' " +
                " and ft031.MNC_DOC_NO = '" + docno + "' " +
                " and ft031.MNC_DOC_CD = '" + doccd + "' " +
                " and ft031.MNC_AN_NO = '" + anno + "' " +
                " and ft031.MNC_AN_yr ='" + anyear + "' " +
                //"Group By pm30.MNC_SR_DSC, pt16.mnc_sr_cd,pt16.mnc_req_dat,pt16.mnc_stamp_tim,pt16.MNC_SR_PRI,pm30.ucep_code " +
                "order by ft031.MNC_FN_DAT, ft031.MNC_FN_TIME, ft031.MNC_FN_NO ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectOrderHotChargeUcepIPDByHn(String hn, String hnyr, String anno, String anyear)
        {
            DataTable dt = new DataTable();
            String sql = "";
            //อ้วนแจ้ง  ข้อมูล double ทำให้ต้อง distinct
            //sql = "Select pm30.MNC_SR_DSC, pt16.mnc_sr_cd, sum(pt16.mnc_sr_qty) as qty     " +
            //    ", convert(VARCHAR(20),pt16.mnc_req_dat,23) as mnc_req_dat,pt16.mnc_stamp_tim,pt16.MNC_SR_PRI,pm30.ucep_code,ft031.mnc_fn_cd " +
                
            //    "from FINANCE_T03_1 ft031 " +
            //    "inner join patient_t16  pt16 on pt16.MNC_REQ_YR = ft031.MNC_REQ_YR and pt16.MNC_REQ_DAT = ft031.MNC_REQ_DAT and pt16.MNC_REQ_NO = ft031.MNC_REQ_NO  " +
            //    "inner join PATIENT_M30 pm30 on pt16.MNC_SR_CD = pm30.MNC_SR_CD  " +
                
            //    "where  " +
            //    " ft031.MNC_HN_NO = '" + hn + "'  " +
            //    ////" and t01.mnc_vn_no = '" + vn + "' " +
            //    //"and phart05.MNC_PRE_NO = '" + preno + "' " +
            //    //"and phart05.MNC_DATE = '" + vsdate + "' " +
            //    " and pt16.MNC_req_STS = 'a' " +
            //    //" and ft031.MNC_ad_DAT = '" + andate + "' " +
            //    " and ft031.MNC_AN_NO = '" + anno + "' " +
            //    " and ft031.MNC_AN_yr ='" +anyear+"' " +
            //    "Group By pm30.MNC_SR_DSC, pt16.mnc_sr_cd,pt16.mnc_req_dat,pt16.mnc_stamp_tim,pt16.MNC_SR_PRI,pm30.ucep_code,ft031.mnc_fn_cd " +
            //    "Order By pt16.mnc_req_dat,pt16.mnc_stamp_tim, pt16.mnc_sr_cd ";

            sql = "Select distinct pm30.MNC_SR_DSC, pt16.mnc_sr_cd, sum(pt16.mnc_sr_qty) as qty     " +
                ", convert(VARCHAR(20),pt16.mnc_req_dat,23) as mnc_req_dat,pt16.mnc_stamp_tim,pt16.MNC_SR_PRI,pm30.ucep_code  " +

                "from FINANCE_T03_1 ft031 " +
                "inner join patient_t16  pt16 on pt16.MNC_REQ_YR = ft031.MNC_REQ_YR and pt16.MNC_REQ_DAT = ft031.MNC_REQ_DAT and pt16.MNC_REQ_NO = ft031.MNC_REQ_NO  " +
                "inner join PATIENT_M30 pm30 on pt16.MNC_SR_CD = pm30.MNC_SR_CD  " +

                "where  " +
                " ft031.MNC_HN_NO = '" + hn + "'  " +
                ////" and t01.mnc_vn_no = '" + vn + "' " +
                //"and phart05.MNC_PRE_NO = '" + preno + "' " +
                //"and phart05.MNC_DATE = '" + vsdate + "' " +
                " and pt16.MNC_req_STS = 'a' " +
                //" and ft031.MNC_ad_DAT = '" + andate + "' " +
                " and ft031.MNC_AN_NO = '" + anno + "' " +
                " and ft031.MNC_AN_yr ='" + anyear + "' " +
                "Group By pm30.MNC_SR_DSC, pt16.mnc_sr_cd,pt16.mnc_req_dat,pt16.mnc_stamp_tim,pt16.MNC_SR_PRI,pm30.ucep_code,ft031.mnc_fn_cd " +
                "-- Order By pt16.mnc_req_dat,pt16.mnc_stamp_tim, pt16.mnc_sr_cd ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectOrderHotChargeUcepOPDByHn(String hn, String hnyr, String visitdate, String preno)
        {
            DataTable dt = new DataTable();
            String sql = "";
            //อ้วนแจ้ง  ข้อมูล double ทำให้ต้อง distinct

            sql = "Select distinct pm30.MNC_SR_DSC, pt16.mnc_sr_cd, sum(pt16.mnc_sr_qty) as qty     " +
                ", convert(VARCHAR(20),pt16.mnc_req_dat,23) as mnc_req_dat,pt16.mnc_stamp_tim,pt16.MNC_SR_PRI,pm30.ucep_code  " +

                "from FINANCE_T03 ft031 " +
                "inner join patient_t16  pt16 on pt16.MNC_REQ_YR = ft031.MNC_REQ_YR and pt16.MNC_REQ_DAT = ft031.MNC_REQ_DAT and pt16.MNC_REQ_NO = ft031.MNC_REQ_NO  " +
                "inner join PATIENT_M30 pm30 on pt16.MNC_SR_CD = pm30.MNC_SR_CD  " +

                "where  " +
                " ft031.MNC_HN_NO = '" + hn + "'  " +
                ////" and t01.mnc_vn_no = '" + vn + "' " +
                //"and phart05.MNC_PRE_NO = '" + preno + "' " +
                //"and phart05.MNC_DATE = '" + vsdate + "' " +
                " and pt16.MNC_req_STS = 'a' " +
                //" and ft031.MNC_ad_DAT = '" + andate + "' " +
                " and ft031.MNC_PRE_NO = '" + preno + "' " +
                " and ft031.MNC_DATE ='" + visitdate + "' " +
                "Group By pm30.MNC_SR_DSC, pt16.mnc_sr_cd,pt16.mnc_req_dat,pt16.mnc_stamp_tim,pt16.MNC_SR_PRI,pm30.ucep_code,ft031.mnc_fn_cd " +
                "-- Order By pt16.mnc_req_dat,pt16.mnc_stamp_tim, pt16.mnc_sr_cd ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectOrderHotChargeUcepIPDByHnDOCcd(String hn, String hnyr, String anno, String anyear, String doccd, String docyr, String docno, String docdate)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select pm30.MNC_SR_DSC, pt16.mnc_sr_cd, sum(pt16.mnc_sr_qty) as qty     " +
                ", convert(VARCHAR(20),pt16.mnc_req_dat,23) as mnc_req_dat,pt16.mnc_stamp_tim,pt16.MNC_SR_PRI,pm30.ucep_code,ft031.mnc_fn_cd " +

                "from FINANCE_T03_1 ft031 " +
                "inner join patient_t16  pt16 on pt16.MNC_REQ_YR = ft031.MNC_REQ_YR and pt16.MNC_REQ_DAT = ft031.MNC_REQ_DAT and pt16.MNC_REQ_NO = ft031.MNC_REQ_NO  " +
                "inner join PATIENT_M30 pm30 on pt16.MNC_SR_CD = pm30.MNC_SR_CD  " +

                "where  " +
                " ft031.MNC_HN_NO = '" + hn + "'  " +
                " and ft031.MNC_DOC_YR = '" + docyr + "' " +
                " and ft031.MNC_DOC_NO = '" + docno + "' " +
                " and ft031.MNC_DOC_CD = '" + doccd + "' " +
                " and pt16.MNC_req_STS = 'a' " +
                //" and ft031.MNC_ad_DAT = '" + andate + "' " +
                " and ft031.MNC_AN_NO = '" + anno + "' " +
                " and ft031.MNC_AN_yr ='" + anyear + "' " +
                "Group By pm30.MNC_SR_DSC, pt16.mnc_sr_cd,pt16.mnc_req_dat,pt16.mnc_stamp_tim,pt16.MNC_SR_PRI,pm30.ucep_code,ft031.mnc_fn_cd " +
                "Order By pt16.mnc_req_dat,pt16.mnc_stamp_tim, pt16.mnc_sr_cd ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectDrugOPBKKByHn(String hn, String hnyr,String vsdate, String preno)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,sum(phart06.MNC_PH_QTY) as qty    " +
                ", convert(VARCHAR(20),phart05.mnc_req_dat,23) as mnc_req_dat,phart06.MNC_PH_DIR_DSC,pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd " +
                ", phart05.mnc_hn_no, t01.MNC_ID_Nam, phart05.mnc_pre_no,pharmacy_m01.tmt_code, phart06.mnc_usr_add " +
                "From PHARMACY_T06 phart06  " +
                "left join PHARMACY_T05 phart05 on phart05.MNC_CFR_NO = phart06.MNC_CFR_NO and phart05.MNC_CFG_DAT = phart06.MNC_CFR_dat  " +
                "left join PHARMACY_M01 on phart06.MNC_PH_CD = pharmacy_m01.MNC_PH_CD  " +
                "Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD  " +
                "inner join patient_t01 t01 on phart05.MNC_hn_no = t01.mnc_hn_no and phart05.mnc_hn_yr = t01.mnc_hn_yr and phart05.mnc_pre_no = t01.mnc_pre_no and phart05.mnc_date = t01.mnc_date " +
                //"Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD " +
                "where  " +
                " phart05.mnc_hn_no = '" + hn + "'  " +
                ////" and t01.mnc_vn_no = '" + vn + "' " +
                //"and phart05.MNC_PRE_NO = '" + preno + "' " +
                //"and phart05.MNC_DATE = '" + vsdate + "' " +
                " and phart05.MNC_CFR_STS = 'a' " +
                " and phart05.MNC_DATE = '" + vsdate + "' " +
                " and phart05.mnc_pre_no = " + preno + " " +
                //"and phart06.MNC_DOC_CD <> 'RIP' " +
                "Group By phart05.mnc_hn_no, t01.MNC_ID_Nam, phart05.mnc_pre_no, phart05.mnc_req_dat, phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,phart06.MNC_PH_DIR_DSC" +
                ", pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd,pharmacy_m01.tmt_code, phart06.mnc_usr_add " +
                "Order By phart05.mnc_hn_no, phart05.mnc_req_dat, phart06.MNC_PH_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectDrugOPBKK(String paidtype, String dateStart, String dateEnd)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,sum(phart06.MNC_PH_QTY) as qty    " +
                ", convert(VARCHAR(20),phart05.mnc_req_dat,23) as mnc_req_dat,phart06.MNC_PH_DIR_DSC,pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd " +
                ", phart05.mnc_hn_no, t01.MNC_ID_Nam, phart05.mnc_pre_no " +
                "From PHARMACY_T06 phart06  " +
                "left join PHARMACY_T05 phart05 on phart05.MNC_CFR_NO = phart06.MNC_CFR_NO and phart05.MNC_CFG_DAT = phart06.MNC_CFR_dat  " +
                "left join PHARMACY_M01 on phart06.MNC_PH_CD = pharmacy_m01.MNC_PH_CD  " +
                "Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD  " +
                "inner join patient_t01 t01 on phart05.MNC_hn_no = t01.mnc_hn_no and phart05.mnc_hn_yr = t01.mnc_hn_yr and phart05.mnc_pre_no = t01.mnc_pre_no and phart05.mnc_date = t01.mnc_date " +
                //"Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD " +
                "where  " +
                //" phart05.mnc_hn_no = '" + hn + "'  " +
                ////" and t01.mnc_vn_no = '" + vn + "' " +
                //"and phart05.MNC_PRE_NO = '" + preno + "' " +
                //"and phart05.MNC_DATE = '" + vsdate + "' " +
                " phart05.MNC_CFR_STS = 'a' " +
                " and phart05.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
                " and phart05.mnc_fn_typ_cd in (" + paidtype + ") " +
                //"and phart06.MNC_DOC_CD <> 'RIP' " +
                "Group By phart05.mnc_hn_no, t01.MNC_ID_Nam, phart05.mnc_pre_no, phart05.mnc_req_dat, phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,phart06.MNC_PH_DIR_DSC, pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd " +
                "Order By phart05.mnc_hn_no, phart05.mnc_req_dat, phart06.MNC_PH_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectDrugOPDPrint(String hn, String vn, String preno, String vsdate)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select phart06.MNC_PH_CD as phar_cd, pharmacy_m01.MNC_PH_TN as phar_name ,sum(phart06.MNC_PH_QTY) as phar_quantity   " +
                ", convert(VARCHAR(20),phart05.mnc_req_dat,23) as request_date,phart06.MNC_PH_DIR_DSC as phar_dir,pm11.MNC_PH_CAU_dsc ,PHARMACY_M01.mnc_ph_unt_cd , PHARMACY_M03.mnc_ph_unt_dsc as phar_unit, phart05.MNC_REQ_NO, phart05.MNC_REQ_YR, convert(VARCHAR(20),phart05.MNC_DATE,23) as patient_visitdate " +
                " " +
                "From PHARMACY_T06 phart06  " +
                "left join PHARMACY_T05 phart05 on phart05.MNC_CFR_NO = phart06.MNC_CFR_NO and phart05.MNC_CFG_DAT = phart06.MNC_CFR_dat  " +
                "left join PHARMACY_M01 on phart06.MNC_PH_CD = pharmacy_m01.MNC_PH_CD  " +
                "Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD  " +
                " left join PHARMACY_M03 on PHARMACY_M01.mnc_ph_unt_cd  = PHARMACY_M03.mnc_ph_unt_cd " +
                //" inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                //"left join PHARMACY_M05 on PHARMACY_M05.MNC_PH_CD=PHARMACY_M01.MNC_PH_CD " +
                //"Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD " +
                "where  " +
                " phart05.mnc_hn_no = '" + hn + "'  " +
                //" and t01.mnc_vn_no = '" + vn + "' " +
                "and phart05.MNC_PRE_NO = '" + preno + "' " +
                "and phart05.MNC_DATE = '" + vsdate + "' " +
                "and phart05.MNC_CFR_STS = 'a' " +
                "and phart06.MNC_DOC_CD <> 'RIP' " +
                "Group By phart05.mnc_req_dat, phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,phart06.MNC_PH_DIR_DSC, pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd, PHARMACY_M03.mnc_ph_unt_dsc, phart05.MNC_REQ_NO, phart05.MNC_REQ_YR, phart05.MNC_DATE " +
                "Order By phart05.mnc_req_dat, phart06.MNC_PH_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectDrugOPD(String hn, String vn, String preno, String vsdate)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,sum(phart06.MNC_PH_QTY) as qty    " +
                ", convert(VARCHAR(20),phart05.mnc_req_dat,23) as mnc_req_dat,phart06.MNC_PH_DIR_DSC,pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd " +
                " " +
                "From PHARMACY_T06 phart06  " +
                "left join PHARMACY_T05 phart05 on phart05.MNC_CFR_NO = phart06.MNC_CFR_NO and phart05.MNC_CFG_DAT = phart06.MNC_CFR_dat  " +
                "left join PHARMACY_M01 on phart06.MNC_PH_CD = pharmacy_m01.MNC_PH_CD  " +
                "Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD  " +
                //"left join PHARMACY_M05 on PHARMACY_M05.MNC_PH_CD=PHARMACY_M01.MNC_PH_CD " +
                //"Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD " +
                "where  "+
                " phart05.mnc_hn_no = '" + hn + "'  " +
                //" and t01.mnc_vn_no = '" + vn + "' " +
                "and phart05.MNC_PRE_NO = '" + preno + "' " +
                "and phart05.MNC_DATE = '" + vsdate + "' " +
                "and phart05.MNC_CFR_STS = 'a' " +
                "and phart06.MNC_DOC_CD <> 'RIP' " +
                "Group By phart05.mnc_req_dat, phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,phart06.MNC_PH_DIR_DSC, pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd " +
                "Order By phart05.mnc_req_dat, phart06.MNC_PH_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectDrugIPD(String hn, String an, String anyr)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,sum(phart06.MNC_PH_QTY) as qty  " +
                ", convert(VARCHAR(20),phart05.mnc_req_dat,23) as mnc_req_dat,phart06.MNC_PH_DIR_DSC,pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd   " +
                "From PHARMACY_T06 phart06  " +
                "left join PHARMACY_T05 phart05 on phart05.MNC_CFR_NO = phart06.MNC_CFR_NO and phart05.MNC_CFG_DAT = phart06.MNC_CFR_dat  " +
                "left join PHARMACY_M01 on phart06.MNC_PH_CD = pharmacy_m01.MNC_PH_CD  " +
                "Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD  " +
                //"left join PHARMACY_M03 on PHARMACY_M01.mnc_ph_unt_cd  = PHARMACY_M03.mnc_ph_unt_cd " +
                "where  " +
                " phart05.mnc_hn_no = '" + hn + "'  " +
                " and phart05.mnc_an_no = '" + an + "' and phart05.mnc_an_yr='" + anyr + "' " +
                "and phart05.MNC_CFR_STS = 'a' " +
                "and phart06.MNC_DOC_CD <> 'RIP' " +
                "Group By phart05.mnc_req_dat, phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,phart06.MNC_PH_DIR_DSC, pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd  " +
                "Order By phart05.mnc_req_dat, phart06.MNC_PH_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectDrugIPDPrint(String hn, String an, String anyr)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select phart06.MNC_PH_CD as phar_cd, pharmacy_m01.MNC_PH_TN  as phar_name,sum(phart06.MNC_PH_QTY)  as phar_quantity " +
                ", convert(VARCHAR(20),phart05.mnc_req_dat,23) as request_date,phart06.MNC_PH_DIR_DSC as phar_dir,pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd , PHARMACY_M03.mnc_ph_unt_dsc as phar_unit, phart05.MNC_REQ_NO, phart05.MNC_REQ_YR, convert(VARCHAR(20),phart05.MNC_DATE,23) as patient_visitdate  " +
                "From PHARMACY_T06 phart06  " +
                "left join PHARMACY_T05 phart05 on phart05.MNC_CFR_NO = phart06.MNC_CFR_NO and phart05.MNC_CFG_DAT = phart06.MNC_CFR_dat  " +
                "left join PHARMACY_M01 on phart06.MNC_PH_CD = pharmacy_m01.MNC_PH_CD  " +
                "Left join PHARMACY_M11 pm11 on PHARMACY_M01.MNC_PH_CAU_CD = pm11.MNC_PH_CAU_CD  " +
                "left join PHARMACY_M03 on PHARMACY_M01.mnc_ph_unt_cd  = PHARMACY_M03.mnc_ph_unt_cd " +
                "where  " +
                " phart05.mnc_hn_no = '" + hn + "'  " +
                " and phart05.mnc_an_no = '" + an + "' and phart05.mnc_an_yr='" + anyr + "' " +
                "and phart05.MNC_CFR_STS = 'a' " +
                "and phart06.MNC_DOC_CD <> 'RIP' " +
                "Group By phart05.mnc_req_dat, phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,phart06.MNC_PH_DIR_DSC, pm11.MNC_PH_CAU_dsc,PHARMACY_M01.mnc_ph_unt_cd , PHARMACY_M03.mnc_ph_unt_dsc, phart05.MNC_REQ_NO, phart05.MNC_REQ_YR, phart05.MNC_DATE " +
                "Order By phart05.mnc_req_dat, phart06.MNC_PH_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLAB(String hn, String vn, String preno)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "SELECT LAB_M02.MNC_LB_CD,LAB_M01.MNC_LB_DSC, LAB_M02.MNC_LB_PRI01, LAB_M02.MNC_LB_PRI02,LAB_T05.* "+
                "FROM     PATIENT_T01 t01 "+
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE "+
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT "+
                "left join LAB_M01 ON LAB_T05.MNC_LB_CD = LAB_M01.MNC_LB_CD "+
                "left join LAB_M02 ON LAB_T05.MNC_LB_CD = LAB_M02.MNC_LB_CD "+
                "where  t01.mnc_hn_no = '"+ hn + "' "+
                "and t01.mnc_vn_no = '"+ vn + "' "+
                "and t01.mnc_pre_no = '"+ preno + "' " +
                "Order By LAB_T05.MNC_REQ_DAT";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectDrugAllergy(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "SELECT t02.* ,pharm36.MNC_PH_ALG_DSC, pharm01.mnc_ph_tn " +
                "FROM     PATIENT_T02 t02 " +
                "left join pharmacy_m01 pharm01 ON t02.mnc_ph_cd = pharm01.mnc_ph_cd  " +
                "left join pharmacy_m36 pharm36 ON t02.MNC_PH_ALG_CD = pharm36.MNC_PH_ALG_CD  " +
                "where  t02.mnc_hn_no = '" + hn + "' " +
                "Order By t02.mnc_stamp_dat, t02.mnc_stamp_tim ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabbyVN(String dateStart, String dateEnd, String hn, String vn, String preNo)
        {
            //select PATIENT_T01.MNC_HN_NO,PATIENT_T01.MNC_PRE_NO,
            //PATIENT_T01.MNC_DATE,PATIENT_T01.MNC_TIME,
            //Patient_t09.MNC_DIA_CD,
            //PATIENT_T09_1.CHRONICCODE,
            //PHARMACY_T06.MNC_PH_CD,
            //Pharmacy_m01.MNC_PH_TN,
            //lab_t05.MNC_LB_CD,lab_m01.MNC_LB_DSC,
            //lab_t05.MNC_RES_VALUE,
            //lab_t05.MNC_STS
            //from PATIENT_T01
            //left join PATIENT_T09_1 on Patient_t01.MNC_ID_NAM = PATIENT_T09_1.MNC_IDNUM
            //left join PATIENT_T09 on Patient_t01.MNC_PRE_NO =Patient_t09.MNC_PRE_NO and
            //Patient_t01.MNC_date =Patient_t09.MNC_date
            //left join Patient_m18 on Patient_t09.MNC_DIA_CD = Patient_m18 .MNC_DIA_CD
            //left join PHARMACY_T05 on Patient_t01.MNC_PRE_NO = PHARMACY_T05.MNC_PRE_NO and
            //Patient_t01.MNC_date = PHARMACY_T05.mnc_date
            //left join PHARMACY_T06 on Pharmacy_t05.MNC_CFR_NO = Pharmacy_t06.MNC_CFR_NO and
            //Pharmacy_t05.MNC_CFG_DAT = Pharmacy_t06.MNC_CFR_dat
            //left join PHARMACY_M01 on Pharmacy_t06.MNC_PH_CD = Pharmacy_m01.MNC_PH_CD
            //left join LAB_T01 on  Patient_t01.MNC_PRE_NO = lab_t01.MNC_PRE_NO and
            //Patient_t01.MNC_date = LAB_T01.mnc_date
            //left join LAB_T05 on lab_t01.MNC_REQ_NO = lab_t05.MNC_REQ_NO and
            //lab_t01.MNC_REQ_DAT = lab_t05.MNC_REQ_DAT
            //left join LAB_M01 on lab_t05.MNC_LB_CD = lab_m01.MNC_LB_CD
            //where PATIENT_T01.MNC_DATE BETWEEN '03/02/2014' AND '03/02/2014'
            //and Patient_t01.MNC_FN_TYP_CD in ('44','45','46','47','48','49')
            //and lab_t05.MNC_LB_CD in ('ch002','ch250','ch003','ch004','ch040','ch037','ch039','ch036','ch038',
            //'se005','se038','se047','ch006','ch007','ch008','ch009','se165') 
            //order by PATIENT_T01.MNC_HN_NO


            //            SELECT LAB_T05.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_REQ_YR, LAB_T05.MNC_REQ_NO, LAB_T05.MNC_REQ_DAT, LAB_T05.MNC_LB_CD AS ExPr1, 
            //                  LAB_T05.MNC_LB_RES_CD, LAB_T05.MNC_RES, LAB_T05.MNC_RES_VALUE AS ExPr2, LAB_T05.MNC_LB_USR, LAB_T05.MNC_STS AS ExPr3, LAB_T05.MNC_RES_MIN, LAB_T05.MNC_RES_MAX, 
            //                  LAB_T05.MNC_LB_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_ACT, LAB_T05.MNC_STAMP_DAT, LAB_T05.MNC_STAMP_TIM, LAB_T05.MNC_LAB_PRN
            //FROM     PATIENT_T01 AS t01 LEFT OUTER JOIN
            //                  LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE LEFT OUTER JOIN
            //                  LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT LEFT OUTER JOIN
            //                  LAB_M01 ON LAB_T05.MNC_LB_CD = LAB_M01.MNC_LB_CD
            //WHERE  (t01.MNC_DATE BETWEEN '2014-03-31' AND '2014-03-31') AND (t01.MNC_FN_TYP_CD IN ('44', '45', '46', '47', '48', '49')) AND (t01.MNC_HN_NO = '5077727') AND (LAB_T05.MNC_LB_CD IN ('ch002', 'ch250', 'ch003', 'ch004', 
            //                  'ch040', 'ch037', 'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165'))

            String sql = "";
            DataTable dt = new DataTable();
            //sql = "SELECT distinct LAB_T05.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS " +
            //    "FROM     PATIENT_T01 t01 " +
            //    "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE " +
            //    "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT " +
            //    "left join LAB_M01 ON LAB_T05.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
            //    "where t01.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
            //    "and t01.MNC_FN_TYP_CD in ('44','45','46','47','48','49') " + " and t01.mnc_hn_no = '" + hn + "' " +
            //    "and t01.mnc_vn_no = '" + vn + "'  "+
            //    "and t01.mnc_Pre_no = '" + preNo + "'  " +
            //    "and  (LAB_T05.MNC_LB_CD IN ('ch002', 'ch250', 'ch003', 'ch004', 'ch040', 'ch037', "+
            //    "'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) "+
            //    "and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' ";

            sql = "SELECT distinct LAB_T05.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT " +
                "left join LAB_M01 ON LAB_T05.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "where t01.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
                "and t01.MNC_FN_TYP_CD in ('44','45','46','47','48','49') " + " and t01.mnc_hn_no = '" + hn + "' " +
                "and t01.mnc_vn_no = '" + vn + "'  " +
                "and t01.mnc_Pre_no = '" + preNo + "'  " +
                //"and  (LAB_T05.MNC_LB_CD IN ('ch002', 'ch250', 'ch003', 'ch004', 'ch040', 'ch037', " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                "and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultLabByDate(String paidtype, String dateStart, String dateEnd)
        {
            String sql = "";
            DataTable dt = new DataTable();

            //sql = "SELECT LAB_T05.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, t01.mnc_hn_no,LAB_T01.MNC_REQ_DAT, t01.MNC_ID_Nam, t01.mnc_pre_no " +
            //    "FROM PATIENT_T01 t01 " +
            //    "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_yr = lab_t01.mnc_hn_yr and t01.mnc_hn_no = lab_t01.mnc_hn_no " +
            //    "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and lab_t01.mnc_req_yr = lab_t05.mnc_req_yr " +
            //    "left join LAB_M01 ON LAB_T05.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
            //    "where t01.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
            //    " and t01.mnc_fn_typ_cd in (" + paidtype + ") " +
            //    //"and t01.MNC_FN_TYP_CD in ('44','45','46','47','48','49') " + " and t01.mnc_hn_no = '" + hn + "' " +
            //    //"and t01.mnc_vn_no = '" + vn + "'  " +
            //    //"and t01.mnc_Pre_no = '" + preNo + "'  " +
            //    //"and  (LAB_T05.MNC_LB_CD IN ('ch002', 'ch250', 'ch003', 'ch004', 'ch040', 'ch037', " +
            //    //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
            //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
            //"Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD ";
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX,usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve" +
                ", t01.mnc_hn_no, t01.MNC_ID_Nam, t01.mnc_pre_no " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "where t01.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
                " and t01.mnc_fn_typ_cd in (" + paidtype + ") " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                //"and  (LAB_T05.MNC_LB_CD IN ('ch002', 'ch250', 'ch003', 'ch004', 'ch040', 'ch037', " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By t01.mnc_hn_no,lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectPaidByHnGrpPaidTypeIPD(String hn, String hnyear, String vsdate, String preno)
        {
            String sql = "";
            DataTable dt = new DataTable();

            sql = "SELECT fnt01.mnc_sum_pri, fnt01.MNC_date, fnt01.MNC_doc_no, fnt01.MNC_doc_yr,convert(VARCHAR(20),fnt01.mnc_doc_dat,23) as mnc_doc_dat " +
                ", fnt01.MNC_DOC_CD, fnt01.MNC_DOC_YR, fnt01.MNC_DOC_NO, convert(VARCHAR(20),fnt01.MNC_DOC_DAT,23) as MNC_DOC_DAT, fnt01.mnc_an_yr, fnt01.mnc_an_no, fnt01.mnc_ad_dat " +
                "FROM      finance_t01 fnt01 " +
                //"inner join finance_t03 fnt03 ON t01.MNC_PRE_NO = fnt03.MNC_PRE_NO AND t01.MNC_DATE = fnt03.MNC_DATE and t01.mnc_hn_no = fnt03.mnc_hn_no  and t01.mnc_hn_yr = fnt03.mnc_hn_yr " +
                //"inner join finance_m01 fnm01 on fnt03.mnc_fn_no = fnm01.mnc_fn_cd " +
                //"inner join finance_m06 fnm06 on fnm01.MNC_FN_GRP = fnm06.mnc_fn_grp_cd " +
                "where fnt01.MNC_DATE = '" + vsdate + "' " +
                " and fnt01.mnc_hn_no = '" + hn + "' and fnt01.mnc_hn_yr = '" + hnyear + "' " +
                " and fnt01.mnc_pre_no = '" + preno + "' " +
                " and fnt01.mnc_doc_sts <> 'V'   " +
                "Order By fnt01.mnc_hn_no,fnt01.MNC_doc_DAT,fnt01.MNC_doc_no ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectPaidByHnGrpPaidTypeOPD(String hn, String hnyear, String vsdate, String preno)
        {
            String sql = "";
            DataTable dt = new DataTable();

            sql = "SELECT fnt01.mnc_sum_pri, fnt01.MNC_date, fnt01.MNC_doc_no, fnt01.MNC_doc_yr,convert(VARCHAR(20),fnt01.mnc_doc_dat,23) as mnc_doc_dat " +
                ", fnt01.MNC_DOC_CD, fnt01.MNC_DOC_YR, fnt01.MNC_DOC_NO, convert(VARCHAR(20),fnt01.MNC_DOC_DAT,23) as MNC_DOC_DAT, fnt01.mnc_an_yr, fnt01.mnc_an_no, fnt01.mnc_ad_dat " +
                "FROM      finance_t01 fnt01 " +
                //"inner join finance_t03 fnt03 ON t01.MNC_PRE_NO = fnt03.MNC_PRE_NO AND t01.MNC_DATE = fnt03.MNC_DATE and t01.mnc_hn_no = fnt03.mnc_hn_no  and t01.mnc_hn_yr = fnt03.mnc_hn_yr " +
                //"inner join finance_m01 fnm01 on fnt03.mnc_fn_no = fnm01.mnc_fn_cd " +
                //"inner join finance_m06 fnm06 on fnm01.MNC_FN_GRP = fnm06.mnc_fn_grp_cd " +
                "where fnt01.MNC_DATE = '" + vsdate + "' " +
                " and fnt01.mnc_hn_no = '" + hn + "' and fnt01.mnc_hn_yr = '" + hnyear + "' " +
                " and fnt01.mnc_pre_no = '" + preno + "' " +
                " and fnt01.mnc_doc_sts <> 'V'   " +
                "Order By fnt01.mnc_hn_no,fnt01.MNC_doc_DAT,fnt01.MNC_doc_no ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectPaidByHnGrpPaidTypeDetail(String hn, String hnyear, String vsdate, String preno, String doccd, String docyear, String docno, String docdate)
        {
            String sql = "";
            DataTable dt = new DataTable();

            sql = "SELECT fnt01.mnc_sum_pri, fnt01.MNC_date, fnt01.MNC_doc_no, fnt01.MNC_doc_yr,convert(VARCHAR(20),fnt01.mnc_doc_dat,23) as mnc_doc_dat " +
                " , fnt01.MNC_DOC_CD, fnt01.MNC_DOC_YR, fnt01.MNC_DOC_NO, convert(VARCHAR(20), fnt01.MNC_DOC_DAT, 23) as MNC_DOC_DAT,fm01.MNC_FN_DSCT,fm01.MNC_SIMB_CD, ft03.mnc_fn_amt, ft03.mnc_fn_dis " +
                ",fm01.MNC_CHARGE_CD,fm01.MNC_SUB_CHARGE_CD, ft03.mnc_fn_pad, ft03.mnc_fn_out " +
                "   FROM      finance_t01 fnt01 " +
                " inner join FINANCE_T03_1 ft03 on fnt01.MNC_HN_NO = ft03.MNC_HN_NO and fnt01.MNC_HN_YR = ft03.MNC_HN_YR " +
                " and fnt01.MNC_DOC_CD = ft03.MNC_DOC_CD " +
                " and fnt01.MNC_DOC_YR = ft03.MNC_DOC_YR " +
                " and fnt01.MNC_DOC_NO = ft03.MNC_DOC_NO " +
                " and fnt01.MNC_DOC_DAT = ft03.MNC_DOC_DAT " +
                " left join finance_m01 fm01 on ft03.MNC_FN_CD = fm01.MNC_FN_CD " +
                " where fnt01.MNC_DATE = '"+ vsdate + "'  and fnt01.mnc_hn_no = '"+ hn + "' and fnt01.mnc_hn_yr = '"+ hnyear + "'  and fnt01.mnc_doc_sts <> 'V' and fnt01.mnc_pre_no ='"+ preno + "' " +
                " and ft03.MNC_DOC_CD  = '"+ doccd + "' and ft03.MNC_DOC_YR  = '"+ docyear + "' and ft03.MNC_DOC_NO = '"+ docno + "' " +
                " Order By fnt01.mnc_hn_no,fnt01.MNC_doc_DAT,fnt01.MNC_doc_no , ft03.MNC_REQ_DAT, ft03.MNC_STAMP_TIM  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectPaidByHnOPBKKCHA(String hn, String hnyear, String date, String preno)
        {
            String sql = "";
            DataTable dt = new DataTable();

            sql = "SELECT fnt01.mnc_sum_pri, fnt01.MNC_date, fnt01.MNC_doc_no, fnt01.MNC_doc_yr,convert(VARCHAR(20),fnt01.mnc_doc_dat,23) as mnc_doc_dat" +
                ", t01.mnc_hn_no, t01.MNC_ID_Nam, t01.mnc_pre_no,fnm01.chrgitem, fnt03.mnc_fn_cd " +
                "FROM     PATIENT_T01 t01 " +
                "inner join finance_t01 fnt01 ON t01.MNC_PRE_NO = fnt01.MNC_PRE_NO AND t01.MNC_DATE = fnt01.MNC_DATE and t01.mnc_hn_no = fnt01.mnc_hn_no  and t01.mnc_hn_yr = fnt01.mnc_hn_yr " +
                "inner join finance_t03 fnt03 ON t01.MNC_PRE_NO = fnt03.MNC_PRE_NO AND t01.MNC_DATE = fnt03.MNC_DATE and t01.mnc_hn_no = fnt03.mnc_hn_no  and t01.mnc_hn_yr = fnt03.mnc_hn_yr " +
                "inner join finance_m01 fnm01 on fnt03.mnc_fn_no = fnm01.mnc_fn_cd " +
                "inner join finance_m06 fnm06 on fnm01.MNC_FN_GRP = fnm06.mnc_fn_grp_cd " +
                "where t01.MNC_DATE = '" + date + "' " +
                " and t01.mnc_hn_no = '" + hn + "' and t01.mnc_hn_yr = '" + hnyear + "' " +
                " and t01.mnc_pre_no = '" + preno + "' " +
                //"and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                "Order By t01.mnc_hn_no,fnt01.MNC_doc_DAT,fnt01.MNC_doc_no ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectPaidByHnOPBKK(String hn, String hnyear, String date, String preno)
        {
            String sql = "";
            DataTable dt = new DataTable();

            sql = "SELECT fnt01.mnc_sum_pri, fnt01.MNC_date, fnt01.MNC_doc_no, fnt01.MNC_doc_yr,convert(VARCHAR(20),fnt01.mnc_doc_dat,23) as mnc_doc_dat" +
                ", t01.mnc_hn_no, t01.MNC_ID_Nam, t01.mnc_pre_no " +
                "FROM     PATIENT_T01 t01 " +
                "left join finance_t01 fnt01 ON t01.MNC_PRE_NO = fnt01.MNC_PRE_NO AND t01.MNC_DATE = fnt01.MNC_DATE and t01.mnc_hn_no = fnt01.mnc_hn_no  and t01.mnc_hn_yr = fnt01.mnc_hn_yr " +
                "where t01.MNC_DATE = '" + date + "' " +
                " and t01.mnc_hn_no = '" + hn + "' and t01.mnc_hn_yr = '" + hnyear + "' " +
                " and t01.mnc_pre_no = '" + preno + "' " +
                //"and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                "Order By t01.mnc_hn_no,fnt01.MNC_doc_DAT,fnt01.MNC_doc_no ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultLabByHnOPBKK(String hn, String hnyear, String date, String preno)
        {
            String sql = "";
            DataTable dt = new DataTable();
            
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX,usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve" +
                ", t01.mnc_hn_no, t01.MNC_ID_Nam, t01.mnc_pre_no " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no  and t01.mnc_hn_yr = LAB_T01.mnc_hn_yr " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "where t01.MNC_DATE = '" + date + "' " +
                " and t01.mnc_hn_no = '" + hn + "' and t01.mnc_hn_yr = '" + hnyear+"' " +
                " and t01.mnc_pre_no = '" + preno + "' " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                "Order By t01.mnc_hn_no,lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectICD10ByHn(String hn, String hnyear, String date, String preno)
        {
            String sql = "";
            DataTable dt = new DataTable();

            sql = "SELECT t09.MNC_dia_cd, t09.MNC_dot_cd,convert(VARCHAR(20),t09.mnc_act_dat,23) as mnc_act_dat,t09.MNC_dia_flg " +
                ", t01.mnc_hn_no, t01.MNC_ID_Nam, t01.mnc_pre_no " +
                "FROM     PATIENT_T01 t01 " +
                "left join patient_t09 t09 ON t01.MNC_PRE_NO = t09.MNC_PRE_NO AND t01.MNC_DATE = t09.MNC_DATE and t01.mnc_hn_no = t09.mnc_hn_no and t01.mnc_hn_yr = t09.mnc_hn_yr " +
                "where t01.MNC_DATE = '" + date + "' " +
                " and t01.mnc_hn_no = '" + hn + "' and t01.mnc_hn_yr = '" + hnyear + "' " +
                " and t01.mnc_pre_no = '" + preno + "' " +
                "Order By t01.mnc_hn_no,t09.mnc_act_dat,t09.MNC_dot_cd ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectICD9ByHn(String hn, String hnyear, String date, String preno)
        {
            String sql = "";
            DataTable dt = new DataTable();

            sql = "SELECT t19.MNC_diaor_cd, t19.MNC_dot_cd,convert(VARCHAR(20),t19.mnc_act_dat,23) as mnc_act_dat,t19.MNC_diaor_flg " +
                ", t01.mnc_hn_no, t01.MNC_ID_Nam, t01.mnc_pre_no " +
                "FROM     PATIENT_T01 t01 " +
                "left join patient_t19 t19 ON t01.MNC_PRE_NO = t19.MNC_PRE_NO AND t01.MNC_DATE = t19.MNC_DATE and t01.mnc_hn_no = t19.mnc_hn_no and t01.mnc_hn_yr = t19.mnc_hn_yr " +
                "where t01.MNC_DATE = '" + date + "' " +
                " and t01.mnc_hn_no = '" + hn + "' and t01.mnc_hn_yr = '" + hnyear + "' " +
                " and t01.mnc_pre_no = '" + preno + "' " +
                "Order By t01.mnc_hn_no,t19.mnc_act_dat,t19.MNC_dot_cd ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabOutbyVN(String dateStart, String hn)
        {
            String sql = "";
            DataTable dt = new DataTable();


            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES" +
                ",convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat, lab_t05.mnc_res, lab_t05.mnc_req_no, lab_m01.mnc_sch_act " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "where t01.MNC_DATE = '" + dateStart + "' " +
                " and t01.mnc_hn_no = '" + hn + "' " +
                //"and t01.mnc_vn_no = '" + vn + "'  " +
                //"and t01.mnc_Pre_no = '" + preNo + "'" +
                "and (LAB_M01.mnc_lb_dsc like '%out lab%' or LAB_M01.mnc_lb_dsc like '%outlab%')  " +
                //"and  (LAB_T05.MNC_LB_CD IN ('ch002', 'ch250', 'ch003', 'ch004', 'ch040', 'ch037', " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabOutbyVN(String dateStart, String hn, String preNo)
        {
            String sql = "";
            DataTable dt = new DataTable();
            
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES" +
                ", LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no,LAB_M01.mnc_sch_act " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "where t01.MNC_DATE = '" + dateStart + "' " +
                " and t01.mnc_hn_no = '" + hn + "' " +
                //"and t01.mnc_vn_no = '" + vn + "'  " +
                "and t01.mnc_Pre_no = '" + preNo + "' " +
                "and (LAB_M01.mnc_lb_dsc like '%out lab%' or LAB_M01.mnc_lb_dsc like '%outlab%')  " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C'" +
                //"and  (LAB_T05.MNC_LB_CD IN ('ch002', 'ch250', 'ch003', 'ch004', 'ch040', 'ch037', " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabCOVIDbyHNpreno(String hn, String vsdate, String preno)
        {
            String sql = "";
            DataTable dt = new DataTable();
            //[MNC_LB_GRP_DSC]
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX,usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve" +
                ", convert(VARCHAR(20),lab_t02.MNC_STAMP_DAT,23) as MNC_STAMP_DAT, lab_t02.MNC_STAMP_TIM, convert(VARCHAR(20),lab_t02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT, lab_t02.MNC_RESULT_TIM " +
                ",t01.mnc_time " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "where t01.MNC_DATE = '" + vsdate + "' " +
                " and t01.mnc_hn_no = '" + hn + "' " +
                "and t01.mnc_pre_no = '" + preno + "'  " +
                //"and t01.mnc_Pre_no = '" + preNo + "'  " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                //"and  LAB_T05.MNC_LB_CD IN ('SE629') " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabCOVIDbyHN(String dateStart, String dateEnd, String hn)
        {
            String sql = "";
            DataTable dt = new DataTable();
            //[MNC_LB_GRP_DSC]
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX,usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve" +
                ", convert(VARCHAR(20),lab_t02.MNC_STAMP_DAT,23) as MNC_STAMP_DAT, lab_t02.MNC_STAMP_TIM, convert(VARCHAR(20),lab_t02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT, lab_t02.MNC_RESULT_TIM " +
                ",t01.mnc_time " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "where t01.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
                " and t01.mnc_hn_no = '" + hn + "' " +
                //"and t01.mnc_vn_no = '" + vn + "'  " +
                //"and t01.mnc_Pre_no = '" + preNo + "'  " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                //"and  LAB_T05.MNC_LB_CD IN ('SE629') "+
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabCOVIDSE184byHN(String dateStart, String dateEnd, String hn)
        {
            String sql = "", wherehn="";
            DataTable dt = new DataTable();
            //[MNC_LB_GRP_DSC]
            if (hn.Length > 0)
            {
                wherehn = " and t01.mnc_hn_no = '" + hn + "' ";
            }
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX,usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve" +
                ", convert(VARCHAR(20),lab_t02.MNC_STAMP_DAT,23) as MNC_STAMP_DAT, lab_t02.MNC_STAMP_TIM, convert(VARCHAR(20),lab_t02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT, lab_t02.MNC_RESULT_TIM " +
                ",t01.mnc_time,lab_t05.MNC_LB_RES_CD,LAB_T01.mnc_patname ,LAB_T01.mnc_hn_no " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                //"left join patient_m01 pm01 on t01.mnc_hn_no = pm01.mnc_hn_no and t01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "where t01.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
                wherehn +
                //"and t01.mnc_vn_no = '" + vn + "'  " +
                //"and t01.mnc_Pre_no = '" + preNo + "'  " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                "and  LAB_T05.MNC_LB_CD IN ('SE184') " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabCOVIDSE184byHNSE184(String dateStart, String dateEnd, String hn)
        {
            String sql = "", wherehn = "";
            DataTable dt = new DataTable();
            //[MNC_LB_GRP_DSC]
            if (hn.Length > 0)
            {
                wherehn = " and t01.mnc_hn_no = '" + hn + "' ";
            }
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX,usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve" +
                ", convert(VARCHAR(20),lab_t02.MNC_STAMP_DAT,23) as MNC_STAMP_DAT, lab_t02.MNC_STAMP_TIM, convert(VARCHAR(20),lab_t02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT, lab_t02.MNC_RESULT_TIM " +
                ",t01.mnc_time,lab_t05.MNC_LB_RES_CD,LAB_T01.mnc_patname ,LAB_T01.mnc_hn_no, pm01.mnc_cur_tel " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "left join patient_m01 pm01 on t01.mnc_hn_no = pm01.mnc_hn_no and t01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "where t01.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
                wherehn +
                " and LAB_T05.mnc_lb_res_cd = '02' " +
                //"and t01.mnc_Pre_no = '" + preNo + "'  " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                "and  LAB_T02.MNC_LB_CD IN ('SE184') " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabCOVIDSE184byHNSE184_1(String dateStart, String dateEnd, String hn)
        {
            String sql = "", wherehn = "";
            DataTable dt = new DataTable();
            //[MNC_LB_GRP_DSC]
            if (hn.Length > 0)
            {
                wherehn = " and t01.mnc_hn_no = '" + hn + "' ";
            }
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC " +
                ",usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve" +
                ", convert(VARCHAR(20),lab_t02.MNC_STAMP_DAT,23) as MNC_STAMP_DAT, lab_t02.MNC_STAMP_TIM, convert(VARCHAR(20),lab_t02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT, lab_t02.MNC_RESULT_TIM " +
                ",t01.mnc_time,LAB_T01.mnc_patname ,LAB_T01.mnc_hn_no, pm01.mnc_cur_tel " +
                ",LAB_T02.mnc_req_no,LAB_T02.mnc_req_yr,convert(VARCHAR(20),LAB_T02.mnc_req_dat,23) as mnc_req_date, convert(VARCHAR(20),t01.mnc_date,23) as mnc_date " +
                "FROM PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                //"left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "left join patient_m01 pm01 on t01.mnc_hn_no = pm01.mnc_hn_no and t01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "where t01.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
                wherehn +
                //" and LAB_T05.mnc_lb_res_cd = '02' " +
                //"and t01.mnc_Pre_no = '" + preNo + "'  " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                "and  LAB_T02.MNC_LB_CD IN ('SE184') " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By t01.mnc_date, t01.mnc_pre_no ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabCOVIDSE184byHNSE184_2(String dateStart)
        {
            String sql = "", wherehn = "";
            DataTable dt = new DataTable();
            //[MNC_LB_GRP_DSC]
            
            sql = "SELECT covidreq.lab_code, LAB_M01.MNC_LB_DSC,covidreq.mnc_hn_no " +
                ", convert(VARCHAR(20),covidreq.req_date,23) as req_date, covidreq.req_time, convert(VARCHAR(20),detected.date_create,8) as result_date " +
                ", pm01.mnc_cur_tel,covidreq.lab_code as MNC_LB_cd,LAB_M01.MNC_LB_DSC,detected.result_value " +
                ",covidreq.req_no ,covidreq.mnc_req_yr,convert(VARCHAR(20),covidreq.req_date,23) as req_date, convert(VARCHAR(20),covidreq.visit_date,23) as mnc_date " +
                ",isnull(pm02.MNC_PFIX_DSC,'')+' '+pm01.MNC_FNAME_T+' '+ pm01.MNC_LNAME_T as mnc_patname, '' as mnc_time, '' as mnc_req_sts,lab_covid_request_id " +
                "FROM t_lab_covid_request covidreq " +
                "left join LAB_M01 ON covidreq.lab_code = LAB_M01.MNC_LB_CD " +
                "left join patient_m01 pm01 on covidreq.mnc_hn_no = pm01.mnc_hn_no  " +
                "left join patient_m02 pm02 on pm01.MNC_PFIX_CDT =pm02.MNC_PFIX_CD " +
                "left join t_lab_covid_detected detected ON covidreq.mnc_hn_no = detected.mnc_hn_no and covidreq.req_no = detected.req_no and covidreq.req_date = detected.req_date and covidreq.mnc_req_yr = detected.mnc_req_yr " +
                "where covidreq.visit_date = '" + dateStart + "'  " +
                "Order By covidreq.visit_date, covidreq.pre_no ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabCOVIDSE184byHNSE629(String dateStart, String dateEnd, String hn)
        {
            String sql = "", wherehn = "";
            DataTable dt = new DataTable();
            //[MNC_LB_GRP_DSC]
            if (hn.Length > 0)
            {
                wherehn = " and t01.mnc_hn_no = '" + hn + "' ";
            }
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX,usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve" +
                ", convert(VARCHAR(20),lab_t02.MNC_STAMP_DAT,23) as MNC_STAMP_DAT, lab_t02.MNC_STAMP_TIM, convert(VARCHAR(20),lab_t02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT, lab_t02.MNC_RESULT_TIM " +
                ",t01.mnc_time,lab_t05.MNC_LB_RES_CD,LAB_T01.mnc_patname ,LAB_T01.mnc_hn_no, pm01.mnc_cur_tel,LAB_T01.mnc_req_sts " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "left join patient_m01 pm01 on t01.mnc_hn_no = pm01.mnc_hn_no and t01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "where t01.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
                wherehn +
                " and LAB_T05.mnc_lb_res_cd = '01' " +
                //"and t01.mnc_Pre_no = '" + preNo + "'  " +
                //"and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                "and  LAB_T02.MNC_LB_CD IN ('SE629') " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabCOVIDSE184byHNSE629_1(String dateStart, String dateEnd, String hn)
        {
            String sql = "", wherehn = "";
            DataTable dt = new DataTable();
            //[MNC_LB_GRP_DSC]
            if (hn.Length > 0)
            {
                wherehn = " and t01.mnc_hn_no = '" + hn + "' ";
            }
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC " +
                ",usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve" +
                ", convert(VARCHAR(20),lab_t02.MNC_STAMP_DAT,23) as MNC_STAMP_DAT, lab_t02.MNC_STAMP_TIM, convert(VARCHAR(20),lab_t02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT, lab_t02.MNC_RESULT_TIM " +
                ",t01.mnc_time,LAB_T01.mnc_patname ,LAB_T01.mnc_hn_no, pm01.mnc_cur_tel" +
                ",LAB_T02.mnc_req_no,LAB_T02.mnc_req_yr,convert(VARCHAR(20),LAB_T02.mnc_req_dat,23) as mnc_req_dat,LAB_T01.mnc_req_sts , convert(VARCHAR(20),t01.mnc_date,23) as mnc_date " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                //"left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "left join patient_m01 pm01 on t01.mnc_hn_no = pm01.mnc_hn_no and t01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "where t01.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
                wherehn +
                //" and LAB_T05.mnc_lb_res_cd = '01' " +
                //"and t01.mnc_Pre_no = '" + preNo + "'  " +
                //"and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                "and  LAB_T02.MNC_LB_CD IN ('SE629') " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By  t01.mnc_date, t01.mnc_pre_no";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabCOVIDRequltbyHN(String reqdate, String reqno, String reqyr, String labcode, String hn)
        {
            String sql = "", wherehn = "", wherelabcode="";
            DataTable dt = new DataTable();
            //[MNC_LB_GRP_DSC]
            if (hn.Length > 0)
            {
                wherehn = " and t01.mnc_hn_no = '" + hn + "' ";
            }
            if (labcode.Length > 0 && labcode.ToLower().Equals("se629"))
            {
                wherelabcode = " and LAB_T05.mnc_lb_cd = '" + labcode + "' and LAB_T05.mnc_lb_res_cd = '01' ";
            }
            else if (labcode.Length > 0 && labcode.ToLower().Equals("se184"))
            {
                wherelabcode = " and LAB_T05.mnc_lb_cd = '" + labcode + "' and LAB_T05.mnc_lb_res_cd = '02' ";
            }
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX,usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve" +
                ", convert(VARCHAR(20),lab_t02.MNC_STAMP_DAT,23) as MNC_STAMP_DAT, lab_t02.MNC_STAMP_TIM, convert(VARCHAR(20),lab_t02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT, lab_t02.MNC_RESULT_TIM " +
                ",t01.mnc_time,lab_t05.MNC_LB_RES_CD,LAB_T01.mnc_patname ,LAB_T01.mnc_hn_no, pm01.mnc_cur_tel " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "left join patient_m01 pm01 on t01.mnc_hn_no = pm01.mnc_hn_no and t01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "where t01.MNC_DATE = '" + reqdate + "' " +
                wherehn +
                wherelabcode +
                "and LAB_T05.mnc_req_no = '" + reqno + "'  " +
                "and LAB_T05.mnc_req_yr = '" + reqyr + "'  " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                //"and  LAB_T02.MNC_LB_CD IN ('SE629') " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabbyAN(String hn, String an, String anyr)
        {
            String sql = "";
            DataTable dt = new DataTable();
            //[MNC_LB_GRP_DSC]
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX,usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve" +
                ", lab_t05.mnc_lb_res_cd, pttm24.MNC_COM_DSC, LAB_T01.mnc_req_yr,lab_t01.MNC_REQ_DEP, lab_m07.MNC_LB_TYP_DSC " +
                ",patient_m02.MNC_PFIX_DSC + ' ' + patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME as dtr_name,fn02.MNC_FN_TYP_DSC ,LAB_T01.mnc_dot_cd, convert(VARCHAR(20),lab_t02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT, lab_t02.MNC_RESULT_TIM, convert(VARCHAR(20),lab_t02.MNC_STAMP_DAT,23) as MNC_STAMP_DAT " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +

                "left join patient_m24 pttm24  on LAB_T01.MNC_com_cd = pttm24.MNC_com_cd  " +
                "inner join lab_m07 on lab_m01.MNC_LB_GRP_CD =lab_m07.MNC_LB_GRP_CD  AND lab_m01.MNC_LB_TYP_CD = lab_m07.MNC_LB_TYP_CD   " +

                " inner join patient_m26 on LAB_T01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                 "Left Join finance_m02 fn02 on LAB_T01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +

                "where LAB_T01.mnc_an_no = '" + an + "'  " +
                "and LAB_T01.mnc_an_yr = '" + anyr + "'  " +
                "and LAB_T01.mnc_hn_no = '" + hn + "' " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                " and (LAB_T05.MNC_RES_VALUE <> null or LAB_T05.MNC_RES_VALUE <> 'NULL') " +
                //"and LAB_T05.mnc_lb_res_cd = '02' " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";
            new LogWriter("d", "selectLabbyVN1 " + sql);
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabbyVN1(String dateStart, String dateEnd, String hn, String vn, String preNo)
        {
            String sql = "";
            DataTable dt = new DataTable();
            //[MNC_LB_GRP_DSC]
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX,usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve" +
                ", lab_t05.mnc_lb_res_cd, pttm24.MNC_COM_DSC, LAB_T01.mnc_req_yr,lab_t01.MNC_REQ_DEP, lab_m07.MNC_LB_TYP_DSC " +
                ",patient_m02.MNC_PFIX_DSC + ' ' + patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME as dtr_name,fn02.MNC_FN_TYP_DSC ,LAB_T01.mnc_dot_cd, convert(VARCHAR(20),lab_t02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT, lab_t02.MNC_RESULT_TIM, convert(VARCHAR(20),lab_t02.MNC_STAMP_DAT,23) as MNC_STAMP_DAT " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +

                "left join patient_m24 pttm24  on LAB_T01.MNC_com_cd = pttm24.MNC_com_cd  " +
                "inner join lab_m07 on lab_m01.MNC_LB_GRP_CD =lab_m07.MNC_LB_GRP_CD  AND lab_m01.MNC_LB_TYP_CD = lab_m07.MNC_LB_TYP_CD   " +

                " inner join patient_m26 on LAB_T01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                 "Left Join finance_m02 fn02 on LAB_T01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +

                "where t01.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
                " and t01.mnc_hn_no = '" + hn + "' " +
                //"and t01.mnc_vn_no = '" + vn + "'  " +
                "and t01.mnc_Pre_no = '" + preNo + "'  " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                " and (LAB_T05.MNC_RES_VALUE <> null or LAB_T05.MNC_RES_VALUE <> 'NULL') " +
                //"and LAB_T05.mnc_lb_res_cd = '02' " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";
            new LogWriter("d", "selectLabbyVN1 "+ sql);
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectLabRequestbyVN1(String dateStart, String dateEnd, String hn, String preNo)
        {
            String sql = "";
            DataTable dt = new DataTable();

            //sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
            //    ", lab_t05.mnc_res, lab_t05.mnc_req_no,fn02.MNC_FN_TYP_DSC, pttm24.MNC_COM_DSC, LAB_T01.mnc_req_yr,patient_m02.MNC_PFIX_DSC + ' ' + patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME as dtr_name,convert(VARCHAR(20),lab_t05.MNC_STAMP_DAT) as MNC_STAMP_DAT, lab_t05.MNC_LB_USR,LAB_T01.mnc_dot_cd,lab_t01.MNC_REQ_DEP " +
            //    "FROM     PATIENT_T01 t01 " +
            //    "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
            //    "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
            //    "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
            //    "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
            //    "Left Join finance_m02 fn02 on LAB_T01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +
            //    "left join patient_m24 pttm24  on LAB_T01.MNC_com_cd = pttm24.MNC_com_cd  " +
            //    " inner join patient_m26 on LAB_T01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
            //    " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
            //    "where t01.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
            //    " and t01.mnc_hn_no = '" + hn + "' " +
            //    "and t01.mnc_vn_no = '" + vn + "'  " +
            //    "and t01.mnc_Pre_no = '" + preNo + "'  " +
            //    "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C'" +
            //    //"and  (LAB_T05.MNC_LB_CD IN ('ch002', 'ch250', 'ch003', 'ch004', 'ch040', 'ch037', " +
            //    //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
            //    //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
            //    "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat " +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no,fn02.MNC_FN_TYP_DSC, pttm24.MNC_COM_DSC, LAB_T01.mnc_req_yr" +
                ",patient_m02.MNC_PFIX_DSC + ' ' + patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME as dtr_name " +
                ",convert(VARCHAR(20),lab_t05.MNC_STAMP_DAT) as MNC_STAMP_DAT, lab_t05.MNC_LB_USR,LAB_T01.mnc_dot_cd,lab_t01.MNC_REQ_DEP,lab_m01.MNC_LB_DSC, lab_m07.MNC_LB_TYP_DSC, convert(VARCHAR(20),LAB_T02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT,LAB_T02.MNC_RESULT_TIM " +
                "FROM LAB_T01  " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "Left Join finance_m02 fn02 on LAB_T01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +
                "left join patient_m24 pttm24  on LAB_T01.MNC_com_cd = pttm24.MNC_com_cd  " +
                " inner join patient_m26 on LAB_T01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                //"inner join lab_m01 on lab_t02.MNC_LB_CD =lab_m01.MNC_LB_CD " +
                "inner join lab_m07 on lab_m01.MNC_LB_GRP_CD =lab_m07.MNC_LB_GRP_CD  AND lab_m01.MNC_LB_TYP_CD = lab_m07.MNC_LB_TYP_CD   " +
                "where LAB_T01.MNC_DATE BETWEEN '" + dateStart + "' AND '" + dateEnd + "' " +
                " and LAB_T01.mnc_hn_no = '" + hn + "' " +
                //"and LAB_T01.mnc_vn_no = '" + vn + "'  " +
                "and LAB_T01.mnc_Pre_no = '" + preNo + "'  " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C'" +
                //"and  (LAB_T05.MNC_LB_CD IN ('ch002', 'ch250', 'ch003', 'ch004', 'ch040', 'ch037', " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectRequestLabbyAN(String hn, String an, String anyr)
        {
            String sql = "";
            DataTable dt = new DataTable();
            //sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat, lab_t05.mnc_res" +
            //    ", lab_t05.mnc_req_no ,fn02.MNC_FN_TYP_DSC, pttm24.MNC_COM_DSC, LAB_T01.mnc_req_yr,patient_m02.MNC_PFIX_DSC + ' ' + patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME as dtr_name,convert(VARCHAR(20),lab_t05.MNC_STAMP_DAT) as MNC_STAMP_DAT, lab_t05.MNC_LB_USR, LAB_T01.mnc_dot_cd,lab_t01.MNC_REQ_DEP" +
            //    "FROM     PATIENT_T01 t01 " +
            //    "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
            //    "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
            //    "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
            //    "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
            //    "Left Join finance_m02 fn02 on LAB_T01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +
            //    "left join patient_m24 pttm24  on LAB_T01.MNC_com_cd = pttm24.MNC_com_cd  " +
            //    " inner join patient_m26 on LAB_T01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
            //    " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
            //    "where LAB_T01.mnc_an_no = '" + an + "'  " +
            //    "and LAB_T01.mnc_an_yr = '" + anyr + "'  " +
            //    "and t01.mnc_hn_no = '" + hn + "' " +
            //    "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C'" +
            //    "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat, lab_t05.mnc_res" +
                ", lab_t05.mnc_req_no ,fn02.MNC_FN_TYP_DSC, pttm24.MNC_COM_DSC, LAB_T01.mnc_req_yr,patient_m02.MNC_PFIX_DSC + ' ' + patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME as dtr_name" +
                ",convert(VARCHAR(20),lab_t05.MNC_STAMP_DAT) as MNC_STAMP_DAT, lab_t05.MNC_LB_USR, LAB_T01.mnc_dot_cd,lab_t01.MNC_REQ_DEP,lab_m01.MNC_LB_DSC, lab_m07.MNC_LB_TYP_DSC, convert(VARCHAR(20),LAB_T02.MNC_RESULT_DAT,23) as MNC_RESULT_DAT,LAB_T02.MNC_RESULT_TIM " +
                "FROM  LAB_T01  " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +

                "left join patient_m24 pttm24  on LAB_T01.MNC_com_cd = pttm24.MNC_com_cd  " +
                "inner join lab_m07 on lab_m01.MNC_LB_GRP_CD =lab_m07.MNC_LB_GRP_CD  AND lab_m01.MNC_LB_TYP_CD = lab_m07.MNC_LB_TYP_CD " +

                " inner join patient_m26 on LAB_T01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                "Left Join finance_m02 fn02 on LAB_T01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +

                "where LAB_T01.mnc_an_no = '" + an + "'  " +
                "and LAB_T01.mnc_an_yr = '" + anyr + "'  " +
                "and LAB_T01.mnc_hn_no = '" + hn + "' " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C'" +
                "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultLabbyAN(String hn, String an, String anyr)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat" +
                ", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX ,usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check" +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD,usr_result.MNC_USR_NAME as MNC_USR_NAME_result,usr_report.MNC_USR_NAME as MNC_USR_NAME_report,usr_approve.MNC_USR_NAME as MNC_USR_NAME_approve, lab_t05.MNC_STAMP_TIM, lab_t02.MNC_LB_PRI  " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "where LAB_T01.mnc_an_no = '" + an + "'  " +
                "and LAB_T01.mnc_an_yr = '" + anyr + "'  " +
                "and t01.mnc_hn_no = '" + hn + "' " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                "Order By lab_t05.MNC_REQ_DAT,lab_t05.MNC_REQ_NO,LAB_T05.MNC_LB_CD,lab_t05.MNC_LB_RES_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultLabUcepbyAN(String hn, String an, String anyr)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC,convert(VARCHAR(20),LAB_T02.mnc_req_dat,23) as mnc_req_dat" +
                //", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX  " +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD ,LAB_T02.MNC_STAMP_tim, lab_t02.MNC_LB_PRI,LAB_M01.ucep_code,LAB_M02.mnc_fn_cd " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                //"left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join LAB_M02 on LAB_M01.MNC_LB_CD = LAB_M02.MNC_LB_CD " +
                //"left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                //"left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "where LAB_T01.mnc_an_no = '" + an + "'  " +
                "and LAB_T01.mnc_an_yr = '" + anyr + "'  " +
                "and t01.mnc_hn_no = '" + hn + "' " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                "Order By LAB_T02.mnc_req_dat,LAB_T02.MNC_LB_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultLabUcepbyPreno(String hn, String visitdate, String preno)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC,convert(VARCHAR(20),LAB_T02.mnc_req_dat,23) as mnc_req_dat" +
                //", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX  " +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD ,LAB_T02.MNC_STAMP_tim, lab_t02.MNC_LB_PRI,LAB_M01.ucep_code,LAB_M02.mnc_fn_cd " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                //"left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join LAB_M02 on LAB_M01.MNC_LB_CD = LAB_M02.MNC_LB_CD " +
                //"left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                //"left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "where LAB_T01.MNC_DATE = '" + visitdate + "'  " +
                "and LAB_T01.MNC_PRE_NO = '" + preno + "'  " +
                "and t01.mnc_hn_no = '" + hn + "' " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                "Order By LAB_T02.mnc_req_dat,LAB_T02.MNC_LB_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultLabUcepbyANDOCcd(String hn, String an, String anyr, String doccd, String docyear, String docno, String docdate)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC,convert(VARCHAR(20),LAB_T02.mnc_req_dat,23) as mnc_req_dat" +
                //", lab_t05.mnc_res, lab_t05.mnc_req_no, lab_t05.MNC_RES_MIN, lab_t05.MNC_RES_MAX  " +
                ", lab_m06.MNC_LB_GRP_DSC,LAB_M01.MNC_LB_GRP_CD ,LAB_T02.MNC_STAMP_tim, lab_t02.MNC_LB_PRI,LAB_M01.ucep_code,LAB_M02.mnc_fn_cd " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                //"left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "left join LAB_M02 on LAB_M01.MNC_LB_CD = LAB_M02.MNC_LB_CD " +
                //"left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                //"left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                "left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +
                "where LAB_T01.mnc_an_no = '" + an + "'  " +
                "and LAB_T01.mnc_an_yr = '" + anyr + "'  " +
                "and t01.mnc_hn_no = '" + hn + "' " +
                "and LAB_T01.MNC_DOC_YR = '" + docyear + "'  " +
                "and LAB_T01.MNC_DOC_NO = '" + docno + "'  " +
                "and LAB_T01.MNC_DOC_CD = '" + doccd + "'  " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " +
                "Order By LAB_T02.mnc_req_dat,LAB_T02.MNC_LB_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultXraybyAN(String hn, String an, String anyr, String xraycode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select xray_t04.MNC_XR_DSC  " +
                "From xray_t01 xt01  " +
                "left join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01 .MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "left join xray_t04  on xray_t04 .MNC_REQ_NO = XRAY_T02.MNC_REQ_NO and xray_t04 .MNC_REQ_DAT = XRAY_T02.MNC_REQ_DAT and xray_t04 .MNC_REQ_YR = XRAY_T02.MNC_REQ_YR and xray_t04 .MNC_XR_CD = XRAY_T02.MNC_XR_CD " +
                "where xt01.mnc_an_no = '" + an + "'  " +
                "and xt01.mnc_an_yr = '" + anyr + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' " +
                "and xt04.mnc_xr_cd = '" + xraycode + "' " +
                "Order By XRAY_T02.MNC_XR_CD  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectRequestOutLabbyDateReq(String date_req)
        {
            String sql = "";
            DataTable dt = new DataTable();
            //sql = "SELECT lt02.MNC_LB_CD, LAB_M01.MNC_LB_DSC,convert(VARCHAR(20),lt02.mnc_req_dat,23) as mnc_req_dat, lt02.mnc_req_no,lt01.mnc_patname,lt01.mnc_hn_no,lt01.mnc_req_no,convert(VARCHAR(20),lt01.mnc_req_dat,23) as mnc_req_dat,lt01.MNC_AN_NO, lt01.MNC_AN_YR" +
            sql = "SELECT lt02.MNC_LB_CD, LAB_M01.MNC_LB_DSC,convert(VARCHAR(20),lt02.mnc_req_dat,23) as mnc_req_dat, lt02.mnc_req_no,lt01.mnc_patname,lt01.mnc_hn_no,lt01.mnc_req_no" +
                ",convert(VARCHAR(20),ptt01.MNC_DATE,23) as MNC_DATE,lt01.MNC_AN_NO, lt01.MNC_AN_YR" +
                ", ptt01.mnc_vn_seq, ptt01.mnc_vn_sum, ptt01.mnc_vn_no, ptt01.mnc_pre_no  " +
                "FROM     PATIENT_T01 ptt01 " +
                "inner join LAB_T01 lt01 ON ptt01.MNC_PRE_NO = lt01.MNC_PRE_NO AND ptt01.MNC_DATE = lt01.MNC_DATE and lt01.mnc_hn_no = ptt01.mnc_hn_no and lt01.mnc_hn_yr = ptt01.mnc_hn_yr " +
                "left join LAB_T02 lt02 ON lt01.MNC_REQ_NO = lt02.MNC_REQ_NO AND lt01.MNC_REQ_DAT = lt02.MNC_REQ_DAT AND lt01.MNC_REQ_YR = lt02.MNC_REQ_YR " +
                "left join LAB_M01 ON lt02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                
                "where lt01.mnc_req_dat = '" + date_req + "'  " +
                "and (LAB_M01.mnc_lb_dsc like '%out lab%' or LAB_M01.mnc_lb_dsc like '%outlab%')   " +
                "and lt02.mnc_req_sts <> 'C'  and lt01.mnc_req_sts <> 'C'" +
                "Order By lt01.mnc_req_dat,LAB_M01.MNC_LB_CD ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultXraybyVN(String hn, String vn, String vsdate)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select convert(VARCHAR(20),xt01.MNC_REQ_DAT,23) as MNC_REQ_DAT, xt04.MNC_XR_CD,xt04.MNC_GRP_NO,xt04.MNC_XR_TYP, xt04.MNC_XR_DSC as result, xm01.MNC_XR_DSC " +
                "From xray_t01 xt01  " +
                "left join xray_t04 xt04 on xt04.MNC_REQ_NO = xt01.MNC_REQ_NO and xt04.MNC_REQ_DAT = xt01.MNC_REQ_DAT and xt04.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "left join xray_m01 xm01 on xm01.MNC_XR_CD = xt04.MNC_XR_CD " +
                "where xt01.mnc_an_no = '" + vn + "'  " +
                "and xt01.mnc_date = '" + vsdate + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' " +
                "Order By xt04.MNC_XR_CD  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultXraybyVN1(String hn, String preno, String vsdate)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select convert(VARCHAR(20),xt01.MNC_REQ_DAT,23) as MNC_REQ_DAT, XRAY_T02.MNC_XR_CD,xray_m01.MNC_XR_DSC,xt01.MNC_REQ_NO,convert(VARCHAR(20),xt01.MNC_Date,23) as MNC_Date  " +
                "From xray_t01 xt01  " +
                "left join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01.MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "left join xray_m01 on xray_m01.MNC_XR_CD = XRAY_T02.MNC_XR_CD " +
                //"--left join xray_t04  on xray_t04 .MNC_REQ_NO = XRAY_T02.MNC_REQ_NO and xray_t04 .MNC_REQ_DAT = XRAY_T02.MNC_REQ_DAT and xray_t04 .MNC_REQ_YR = XRAY_T02.MNC_REQ_YR and xray_t04 .MNC_XR_CD = XRAY_T02.MNC_XR_CD " +
                "where xt01.mnc_pre_no = '" + preno + "'  " +
                "and xt01.mnc_date = '" + vsdate + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' and xt01.MNC_REQ_STS = 'A'" +
                "Order By xt01.MNC_REQ_NO, XRAY_T02.MNC_XR_CD  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultXrayXrDtrbyVN1(String hn, String preno, String vsdate, String xraycode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select xray_t04.MNC_XR_DSC,xray_t04.MNC_XR_DSC1,convert(VARCHAR(20),xt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,xt01.MNC_REQ_NO,convert(VARCHAR(20),xt01.MNC_Date,23) as MNC_Date,xt01.mnc_dot_cd " +
                ",patient_m02.MNC_PFIX_DSC + ' ' + patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME as dtr_name  " +
                "From xray_t01 xt01  " +
                "inner join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01.MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "inner join xray_t04  on xray_t04.MNC_REQ_NO = XRAY_T02.MNC_REQ_NO and xray_t04.MNC_REQ_DAT = XRAY_T02.MNC_REQ_DAT and xray_t04.MNC_REQ_YR = XRAY_T02.MNC_REQ_YR and xray_t04.MNC_XR_CD = XRAY_T02.MNC_XR_CD " +
                " inner join patient_m26 on xray_t04.mnc_dot_df_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                "where xt01.mnc_pre_no = '" + preno + "'  " +
                "and xt01.mnc_date = '" + vsdate + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' " +
                "and xray_t04.mnc_xr_cd = '" + xraycode + "' " +
                "Order By xt01.MNC_REQ_NO, xray_t04.MNC_XR_CD  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultXraybyVN1(String hn, String preno, String vsdate, String xraycode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select xray_t04.MNC_XR_DSC,xray_t04.MNC_XR_DSC1,convert(VARCHAR(20),xt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,xt01.MNC_REQ_NO,convert(VARCHAR(20),xt01.MNC_Date,23) as MNC_Date,xt01.mnc_dot_cd " +
                ",patient_m02.MNC_PFIX_DSC + ' ' + patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME as dtr_name,convert(VARCHAR(20),xray_t04.mnc_stamp_dat,23) as mnc_stamp_dat  " +
                "From xray_t01 xt01  " +
                "left join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01.MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "left join xray_t04  on xray_t04.MNC_REQ_NO = XRAY_T02.MNC_REQ_NO and xray_t04.MNC_REQ_DAT = XRAY_T02.MNC_REQ_DAT and xray_t04.MNC_REQ_YR = XRAY_T02.MNC_REQ_YR and xray_t04.MNC_XR_CD = XRAY_T02.MNC_XR_CD " +
                " inner join patient_m26 on xray_t04.MNC_DOT_DF_CD = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                "where xt01.mnc_pre_no = '" + preno + "'  " +
                "and xt01.mnc_date = '" + vsdate + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' " +
                "and xray_t04.mnc_xr_cd = '" + xraycode + "' " +
                "Order By xt01.MNC_REQ_NO, xray_t04.MNC_XR_CD  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectRequestXraybyVN1(String hn, String preno, String vsdate, String xraycode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select convert(VARCHAR(20),xt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,xt01.MNC_REQ_NO,xt01.MNC_REQ_YR,convert(VARCHAR(20),xt01.MNC_Date,23) as MNC_Date,xt01.mnc_dot_cd " +
                ",patient_m02.MNC_PFIX_DSC + ' ' + patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME as dtr_name, xm01.MNC_XR_DSC, xm05.mnc_xr_grp_dsc, pttm24.MNC_COM_DSC,fn02.MNC_FN_TYP_DSC,xt01.MNC_REQ_DEP  " +
                "From xray_t01 xt01  " +
                "left join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01.MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "left join patient_m24 pttm24  on xt01.MNC_com_cd = pttm24.MNC_com_cd  " +
                " inner join patient_m26 on xt01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                " Left Join xray_m01 xm01 on xm01.MNC_XR_CD = XRAY_T02.MNC_XR_CD " +
                "Left Join xray_m05 xm05 on xm01.mnc_xr_grp_cd = xm05.mnc_xr_grp_cd " +
                "Left Join finance_m02 fn02 on xt01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +
                //"Left Join patient_m32 pttm32 on xt01.MNC_REQ_DEP = pttm32.MNC_REQ_DEP " +
                "where xt01.mnc_pre_no = '" + preno + "'  " +
                "and xt01.mnc_date = '" + vsdate + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' " +
                "and xray_t02.mnc_xr_cd = '" + xraycode + "' " +
                "Order By xt01.MNC_REQ_NO  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectRequestXraybyVN2(String hn, String preno, String vsdate)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select convert(VARCHAR(20),xt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,xt01.MNC_REQ_NO,xt01.MNC_REQ_YR,convert(VARCHAR(20),xt01.MNC_Date,23) as MNC_Date,xt01.mnc_dot_cd " +
                ",patient_m02.MNC_PFIX_DSC + ' ' + patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME as dtr_name, xm01.MNC_XR_DSC, xm05.mnc_xr_grp_dsc, pttm24.MNC_COM_DSC,fn02.MNC_FN_TYP_DSC,xt01.MNC_REQ_DEP,xray_t02.mnc_xr_cd  " +
                "From xray_t01 xt01  " +
                "left join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01.MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "left join patient_m24 pttm24  on xt01.MNC_com_cd = pttm24.MNC_com_cd  " +
                " inner join patient_m26 on xt01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                " Left Join xray_m01 xm01 on xm01.MNC_XR_CD = XRAY_T02.MNC_XR_CD " +
                "Left Join xray_m05 xm05 on xm01.mnc_xr_grp_cd = xm05.mnc_xr_grp_cd " +
                "Left Join finance_m02 fn02 on xt01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +
                //"Left Join patient_m32 pttm32 on xt01.MNC_REQ_DEP = pttm32.MNC_REQ_DEP " +
                "where xt01.mnc_pre_no = '" + preno + "'  " +
                "and xt01.mnc_date = '" + vsdate + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' " +
                "and xray_t02.mnc_xr_cd <> 'CBR008' " +
                "Order By xt01.MNC_REQ_NO  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectRequestXraybyVN1(String hn, String vsdate, String xraycode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select convert(VARCHAR(20),xt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,xt01.MNC_REQ_NO,xt01.MNC_REQ_YR,convert(VARCHAR(20),xt01.MNC_Date,23) as MNC_Date,xt01.mnc_dot_cd " +
                ",patient_m02.MNC_PFIX_DSC + ' ' + patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME as dtr_name, xm01.MNC_XR_DSC, xm05.mnc_xr_grp_dsc, pttm24.MNC_COM_DSC,fn02.MNC_FN_TYP_DSC,xt01.MNC_REQ_DEP  " +
                "From xray_t01 xt01  " +
                "left join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01.MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "left join patient_m24 pttm24  on xt01.MNC_com_cd = pttm24.MNC_com_cd  " +
                " inner join patient_m26 on xt01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                " Left Join xray_m01 xm01 on xm01.MNC_XR_CD = XRAY_T02.MNC_XR_CD " +
                "Left Join xray_m05 xm05 on xm01.mnc_xr_grp_cd = xm05.mnc_xr_grp_cd " +
                "Left Join finance_m02 fn02 on xt01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +
                //"Left Join patient_m32 pttm32 on xt01.MNC_REQ_DEP = pttm32.MNC_REQ_DEP " +
                "where  xt01.mnc_date = '" + vsdate + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' " +
                "and xray_t02.mnc_xr_cd = '" + xraycode + "' " +
                "Order By xt01.MNC_REQ_NO  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectRequestXraybyAN1(String hn, String an, String anyr, String xraycode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select convert(VARCHAR(20),xt01.MNC_REQ_DAT,23) as MNC_REQ_DAT, XRAY_T02.MNC_XR_CD,xt01.MNC_REQ_NO,convert(VARCHAR(20),xt01.MNC_Date,23) as MNC_Date,xt01.mnc_dot_cd " +
                ",patient_m02.MNC_PFIX_DSC+' '+patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME  as dtr_name " +
                //",pm021.MNC_PFIX_DSC + ' ' + pm261.MNC_DOT_FNAME + ' ' + pm261.MNC_DOT_LNAME as dtr_name_result " +
                "From xray_t01 xt01  " +
                "inner join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01 .MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                //"left join XRAY_T04 on XRAY_T04.MNC_REQ_NO = XRAY_T02 .MNC_REQ_NO and XRAY_T04.MNC_REQ_DAT = XRAY_T02.MNC_REQ_DAT  and XRAY_T04.MNC_REQ_YR =XRAY_T02.MNC_REQ_YR  " +
                " inner join patient_m26 on xt01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                //" inner join patient_m26 as pm261 on xray_t04.mnc_dot_df_cd = pm261.MNC_DOT_CD " +
                //" inner join patient_m02 pm021 on pm261.MNC_DOT_PFIX =pm021.MNC_PFIX_CD " +
                "where xt01.mnc_an_no = '" + an + "'  " +
                "and xt01.mnc_an_yr = '" + anyr + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' " +
                "and XRAY_T02.mnc_xr_cd = '" + xraycode + "' " +
                "Order By XRAY_T02.MNC_XR_CD  ";


            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultXraybyAN1(String hn, String an, String anyr, String xraycode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select convert(VARCHAR(20),xt01.MNC_REQ_DAT,23) as MNC_REQ_DAT, XRAY_T02.MNC_XR_CD,xt01.MNC_REQ_NO,convert(VARCHAR(20),xt01.MNC_Date,23) as MNC_Date,xt01.mnc_dot_cd " +
                ",patient_m02.MNC_PFIX_DSC+' '+patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME  as dtr_name,xray_t04.mnc_xr_dsc " +
                ",pm021.MNC_PFIX_DSC + ' ' + pm261.MNC_DOT_FNAME + ' ' + pm261.MNC_DOT_LNAME as dtr_name_result, convert(VARCHAR(20),xray_t04.mnc_stamp_dat,23) as mnc_stamp_dat " +
                "From xray_t01 xt01  " +
                "inner join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01 .MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "left join XRAY_T04 on XRAY_T04.MNC_REQ_NO = XRAY_T02 .MNC_REQ_NO and XRAY_T04.MNC_REQ_DAT = XRAY_T02.MNC_REQ_DAT  and XRAY_T04.MNC_REQ_YR =XRAY_T02.MNC_REQ_YR  " +
                " inner join patient_m26 on xt01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                " inner join patient_m26 as pm261 on xray_t04.mnc_dot_df_cd = pm261.MNC_DOT_CD " +
                " inner join patient_m02 pm021 on pm261.MNC_DOT_PFIX =pm021.MNC_PFIX_CD " +
                "where xt01.mnc_an_no = '" + an + "'  " +
                "and xt01.mnc_an_yr = '" + anyr + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' " +
                "and XRAY_T02.mnc_xr_cd = '" + xraycode + "' " +
                "Order By XRAY_T02.MNC_XR_CD  ";


            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultXraybyANDOCcd(String hn, String an, String anyr, String doccd, String docyear, String docno, String docdate)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select convert(VARCHAR(20),xt01.MNC_REQ_DAT,23) as MNC_REQ_DAT, XRAY_T02.MNC_XR_CD,xray_m01.MNC_XR_DSC,xt01.MNC_REQ_NO,convert(VARCHAR(20),xt01.MNC_Date,23) as MNC_Date,xt01.mnc_dot_cd " +
                ",patient_m02.MNC_PFIX_DSC+' '+patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME  as dtr_name, XRAY_T02.MNC_STAMP_TIM,XRAY_T02.MNC_XR_PRI_R,xray_m01.ucep_code,xm02.mnc_fn_cd " +
                "From xray_t01 xt01  " +
                "left join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01 .MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "left join xray_m01 on xray_m01.MNC_XR_CD = XRAY_T02.MNC_XR_CD" +
                " inner join patient_m26 on xt01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                " Left Join xray_m02 xm02 on xray_m01.MNC_XR_CD = xm02.MNC_XR_CD " +
                //"Left Join xray_m05 xm05 on xm01.mnc_xr_grp_cd = xm05.mnc_xr_grp_cd " +
                "where xt01.mnc_an_no = '" + an + "'  " +
                "and xt01.mnc_an_yr = '" + anyr + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' " +
                "and xt01.MNC_DOC_YR = '" + docyear + "' " +
                "and xt01.MNC_DOC_NO = '" + docno + "' " +
                "and xt01.MNC_DOC_CD = '" + doccd + "' " +
                "Order By XRAY_T02.MNC_XR_CD  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultXraybyAN(String hn, String an, String anyr)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select convert(VARCHAR(20),xt01.MNC_REQ_DAT,23) as MNC_REQ_DAT, XRAY_T02.MNC_XR_CD,xray_m01.MNC_XR_DSC,xt01.MNC_REQ_NO,convert(VARCHAR(20),xt01.MNC_Date,23) as MNC_Date,xt01.mnc_dot_cd " +
                ",patient_m02.MNC_PFIX_DSC+' '+patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME  as dtr_name, XRAY_T02.MNC_STAMP_TIM,XRAY_T02.MNC_XR_PRI_R,xray_m01.ucep_code,xm02.mnc_fn_cd " +
                "From xray_t01 xt01  " +
                "left join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01 .MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "left join xray_m01 on xray_m01.MNC_XR_CD = XRAY_T02.MNC_XR_CD" +
                " inner join patient_m26 on xt01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                " Left Join xray_m02 xm02 on xray_m01.MNC_XR_CD = xm02.MNC_XR_CD " +
                //"Left Join xray_m05 xm05 on xm01.mnc_xr_grp_cd = xm05.mnc_xr_grp_cd " +
                "where xt01.mnc_an_no = '" + an + "'  " +
                "and xt01.mnc_an_yr = '" + anyr + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' " +
                "Order By XRAY_T02.MNC_XR_CD  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultXraybyPreno(String hn, String visitxdate, String preno)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select convert(VARCHAR(20),xt01.MNC_REQ_DAT,23) as MNC_REQ_DAT, XRAY_T02.MNC_XR_CD,xray_m01.MNC_XR_DSC,xt01.MNC_REQ_NO,convert(VARCHAR(20),xt01.MNC_Date,23) as MNC_Date,xt01.mnc_dot_cd " +
                ",patient_m02.MNC_PFIX_DSC+' '+patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME  as dtr_name, XRAY_T02.MNC_STAMP_TIM,XRAY_T02.MNC_XR_PRI_R,xray_m01.ucep_code,xm02.mnc_fn_cd,XRAY_T02.MNC_XR_PRI " +
                "From xray_t01 xt01  " +
                "left join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01 .MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "left join xray_m01 on xray_m01.MNC_XR_CD = XRAY_T02.MNC_XR_CD" +
                " inner join patient_m26 on xt01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                " Left Join xray_m02 xm02 on xray_m01.MNC_XR_CD = xm02.MNC_XR_CD " +
                //"Left Join xray_m05 xm05 on xm01.mnc_xr_grp_cd = xm05.mnc_xr_grp_cd " +
                "where xt01.MNC_DATE = '" + visitxdate + "'  " +
                "and xt01.MNC_PRE_NO = '" + preno + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "'  and xt01.MNC_REQ_STS != 'C'  and xm02.MNC_CHARGE_NO = '1' " +
                "Order By XRAY_T02.MNC_XR_CD  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectResultXraybyDate(String hn, String vsdate)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select convert(VARCHAR(20),xt01.MNC_REQ_DAT,23) as MNC_REQ_DAT, XRAY_T02.MNC_XR_CD,xray_m01.MNC_XR_DSC,xt01.MNC_REQ_NO,convert(VARCHAR(20),xt01.MNC_Date,23) as MNC_Date,xt01.mnc_dot_cd " +
                ",patient_m02.MNC_PFIX_DSC+' '+patient_m26.MNC_DOT_FNAME + ' ' + patient_m26.MNC_DOT_LNAME  as dtr_name " +
                "From xray_t01 xt01  " +
                "left join XRAY_T02 on XRAY_T02.MNC_REQ_NO = xt01 .MNC_REQ_NO and XRAY_T02.MNC_REQ_DAT = xt01.MNC_REQ_DAT and XRAY_T02.MNC_REQ_YR = xt01.MNC_REQ_YR " +
                "left join xray_m01 on xray_m01.MNC_XR_CD = XRAY_T02.MNC_XR_CD" +
                " inner join patient_m26 on xt01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                //" Left Join xray_m01 xm01 on xm01.MNC_XR_CD = XRAY_T02.MNC_XR_CD " +
                //"Left Join xray_m05 xm05 on xm01.mnc_xr_grp_cd = xm05.mnc_xr_grp_cd " +
                "where xt01.MNC_DATE = '" + vsdate + "'  " +
                //"and xt01.mnc_an_yr = '" + anyr + "'  " +
                "and xt01.mnc_hn_no = '" + hn + "' " +
                "Order By XRAY_T02.MNC_XR_CD  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectFinancePatient(String startdate, String enddate, String paidtypecode)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "select finance_t01.mnc_doc_cd,finance_t01.mnc_doc_yr,finance_t01.mnc_doc_no,convert(VARCHAR(20),finance_t01.mnc_doc_dat,23) as mnc_doc_dat,convert(VARCHAR(20),finance_t01.MNC_DATE,23) as MNC_DATE" +
                ",PATIENT_T01.mnc_time,finance_t01.MNC_HN_NO,finance_t01.MNC_HN_YR,finance_t01.MNC_AN_NO, " +
                "finance_t01.MNC_AN_YR, m02.MNC_PFIX_DSC,patient_m01.MNC_FNAME_T,patient_m01.MNC_LNAME_T,finance_t01.MNC_PRE_NO,finance_t01.MNC_FN_TYP_CD,PATIENT_T01.MNC_DOT_CD" +
                ",m02dtr.MNC_PFIX_DSC as MNC_PFIX_DSCdtr,patient_m26.MNC_DOT_FNAME,patient_m26.MNC_DOT_LNAME " +
                //" , ft02.MNC_FN_CD, ft02.MNC_NO, ft02.MNC_FN_AMT, fm01.MNC_FN_DSCT " +
                "From finance_t01   " +
                "left join  PATIENT_T01 on finance_t01.mnc_date = PATIENT_T01.mnc_date and finance_t01.mnc_pre_no = PATIENT_T01.MNC_PRE_NO and finance_t01.mnc_hn_no = PATIENT_T01.MNC_HN_NO " +
                "left join  PATIENT_M01 on PATIENT_T01.mnc_hn_no = PATIENT_M01.MNC_HN_NO " +
                "inner join patient_m26 on patient_t01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                "inner join patient_m02  as m02dtr on patient_m26.MNC_DOT_PFIX =m02dtr.MNC_PFIX_CD " +
                "left join PATIENT_M02 as m02 on PATIENT_M01.MNC_PFIX_CDT = M02.MNC_PFIX_CD " +
                //"inner join FINANCE_T02 ft02 on finance_t01.MNC_DOC_CD = ft02.MNC_DOC_CD and finance_t01.MNC_DOC_YR = ft02.MNC_DOC_YR and finance_t01.MNC_DOC_DAT = ft02.MNC_DOC_DAT and finance_t01.MNC_DOC_NO = ft02.MNC_DOC_NO " +
                //"inner join FINANCE_M01 fm01 on ft02.MNC_FN_CD = fm01.MNC_FN_CD " +
                "where  finance_t01.MNC_DATE >= '" + startdate + "' and finance_t01.MNC_DATE <= '" + enddate + "'  " +
                "and finance_t01.MNC_FN_TYP_CD  in (" + paidtypecode + ") " +
                "Order By finance_t01.MNC_DATE,finance_t01.MNC_HN_NO,finance_t01.MNC_PRE_NO  ";

            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectNHSOPrint(String startDate, String endDate, String fncd)
        {
            DataTable dt = new DataTable();
            String sql = "", whereAn = "";
            //sql = fncd.Equals("") ? "Select Distinct t01.mnc_hn_no, t01.MNC_PRE_NO ,t01.mnc_vn_no, m02.MNC_PFIX_DSC, m01.MNC_FNAME_T, m01.MNC_LNAME_T,t01.MNC_VN_SEQ,t01.MNC_VN_SUM,f02.MNC_FN_TYP_DSC, "+

            //    "t01.MNC_DATE,patient_m02.MNC_PFIX_DSC as prefix,patient_m26.MNC_DOT_FNAME as Fname,patient_m26.MNC_DOT_LNAME as Lname,t01.mnc_dot_cd, M01.MNC_BDAY, m01.mnc_id_no, t08.mnc_ds_date, t01.mnc_time, t01.mnc_weight " +
            //    "From PATIENT_T01 t01 "
            //    + "left JOIN PATIENT_M01 AS m01 ON t01.MNC_HN_NO = m01.MNC_HN_NO " +
            //    "left JOIN  PATIENT_M02 AS m02 ON m02.MNC_PFIX_CD = m01.MNC_PFIX_CDT  "+
            //    //"inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO and t01.mnc_date = t12.mnc_date and t12.mnc_pre_no = t01.mnc_pre_no " +
            //    "inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO  and t12.mnc_pre_no = t01.mnc_pre_no " +
            //    " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
            //    //"left join PATIENT_M18 on patient_t01.MNC_DIA_DEAD = PATIENT_M18.MNC_DIA_CD " +
            //    " inner join patient_m26 on t01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
            //    " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
            //    "inner join PATIENT_T08 t08 on t01.MNC_PRE_NO = t08.MNC_PRE_NO and t01.MNC_date = t08.MNC_date " +
            //    "where t08.MNC_ds_DATE >= '" + startDate + "'  and t08.MNC_ds_DATE <= '" + endDate + "'" 
            //    : "Select Distinct t01.mnc_hn_no, t01.MNC_PRE_NO ,t01.mnc_vn_no,m02.MNC_PFIX_DSC, m01.MNC_FNAME_T, m01.MNC_LNAME_T,t01.MNC_VN_SEQ,t01.MNC_VN_SUM,f02.MNC_FN_TYP_DSC "+
            //    "t01.MNC_DATE,patient_m02.MNC_PFIX_DSC as prefix,patient_m26.MNC_DOT_FNAME as Fname,patient_m26.MNC_DOT_LNAME as Lname,t01.mnc_dot_cd, M01.MNC_BDAY, m01.mnc_id_no, t08.mnc_ds_date, t01.mnc_time, t01.mnc_weight " +
            //    "From PATIENT_T01 t01 "
            //    + "left JOIN PATIENT_M01 AS m01 ON t01.MNC_HN_NO = m01.MNC_HN_NO " +
            //    "left JOIN  PATIENT_M02 AS m02 ON m02.MNC_PFIX_CD = m01.MNC_PFIX_CDT  "+
            //    //"inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO and t01.mnc_date = t12.mnc_date and t12.mnc_pre_no = t01.mnc_pre_no " +
            //    "inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO and t12.mnc_pre_no = t01.mnc_pre_no " +
            //    " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
            //    " inner join patient_m26 on t01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
            //    " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
            //    "inner join PATIENT_T08 t08 on t01.MNC_PRE_NO = t08.MNC_PRE_NO and t01.MNC_date = t08.MNC_date " +
            //    "where t01.MNC_FN_TYP_CD = '"
            //    + fncd + "' and t08.MNC_ds_DATE >= '" + startDate + "'  and t08.MNC_ds_DATE <= '" + endDate + "' ";
            //if (flagor == flagOR.setOR)
            //{
            //    sql = fncd.Equals("") ? "Select Distinct t01.mnc_hn_no, t01.MNC_PRE_NO ,t01.mnc_vn_no, m02.MNC_PFIX_DSC, m01.MNC_FNAME_T, m01.MNC_LNAME_T,t01.MNC_VN_SEQ,t01.MNC_VN_SUM,f02.MNC_FN_TYP_DSC, " +
            //    "t01.MNC_DATE,patient_m02.MNC_PFIX_DSC as prefix,patient_m26.MNC_DOT_FNAME as Fname,patient_m26.MNC_DOT_LNAME as Lname,t01.mnc_dot_cd, M01.MNC_BDAY " +
            //    ", m01.mnc_id_no, t08.mnc_ds_date, t01.mnc_time, t01.mnc_weight, t08.mnc_an_no, t08.mnc_an_yr " +
            //    "From PATIENT_T01 t01 "
            //    + "left JOIN PATIENT_M01 AS m01 ON t01.MNC_HN_NO = m01.MNC_HN_NO " +
            //    "left JOIN  PATIENT_M02 AS m02 ON m02.MNC_PFIX_CD = m01.MNC_PFIX_CDT  " +
            //    //"inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO and t01.mnc_date = t12.mnc_date and t12.mnc_pre_no = t01.mnc_pre_no " +
            //    "inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO  and t12.mnc_pre_no = t01.mnc_pre_no " +
            //    " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
            //    //"left join PATIENT_M18 on patient_t01.MNC_DIA_DEAD = PATIENT_M18.MNC_DIA_CD " +
            //    " inner join patient_m26 on t01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
            //    " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
            //    "inner join PATIENT_T08 t08 on t01.MNC_PRE_NO = t08.MNC_PRE_NO and t01.MNC_date = t08.MNC_date " +
            //    "where t08.MNC_ds_DATE >= '" + startDate + "'  and t08.MNC_ds_DATE <= '" + endDate + "'"
            //    : "Select Distinct t01.mnc_hn_no, t01.MNC_PRE_NO ,t01.mnc_vn_no,m02.MNC_PFIX_DSC, m01.MNC_FNAME_T, m01.MNC_LNAME_T,t01.MNC_VN_SEQ,t01.MNC_VN_SUM,f02.MNC_FN_TYP_DSC " +
            //    "t01.MNC_DATE,patient_m02.MNC_PFIX_DSC as prefix,patient_m26.MNC_DOT_FNAME as Fname,patient_m26.MNC_DOT_LNAME as Lname,t01.mnc_dot_cd, M01.MNC_BDAY " +
            //    ", m01.mnc_id_no, t08.mnc_ds_date, t01.mnc_time, t01.mnc_weight, t08.mnc_an_no, t08.mnc_an_yr " +
            //    "From PATIENT_T01 t01 "
            //    + "left JOIN PATIENT_M01 AS m01 ON t01.MNC_HN_NO = m01.MNC_HN_NO " +
            //    "left JOIN  PATIENT_M02 AS m02 ON m02.MNC_PFIX_CD = m01.MNC_PFIX_CDT  " +
            //    //"inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO and t01.mnc_date = t12.mnc_date and t12.mnc_pre_no = t01.mnc_pre_no " +
            //    "inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO and t12.mnc_pre_no = t01.mnc_pre_no " +
            //    " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
            //    " inner join patient_m26 on t01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
            //    " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
            //    "inner join PATIENT_T08 t08 on t01.MNC_PRE_NO = t08.MNC_PRE_NO and t01.MNC_date = t08.MNC_date " +
            //    "where t01.MNC_FN_TYP_CD = '"
            //    + fncd + "' and t08.MNC_ds_DATE >= '" + startDate + "'  and t08.MNC_ds_DATE <= '" + endDate + "' ";
            //}
            //else
            //{
                sql = fncd.Equals("") ? "Select Distinct t01.mnc_hn_no, t01.MNC_PRE_NO ,t01.mnc_vn_no, m02.MNC_PFIX_DSC, m01.MNC_FNAME_T, m01.MNC_LNAME_T,t01.MNC_VN_SEQ,t01.MNC_VN_SUM,f02.MNC_FN_TYP_DSC, " +
                "t01.MNC_DATE,patient_m02.MNC_PFIX_DSC as prefix,patient_m26.MNC_DOT_FNAME as Fname,patient_m26.MNC_DOT_LNAME as Lname,t01.mnc_dot_cd, M01.MNC_BDAY " +
                ", m01.mnc_id_no, t08.mnc_ds_date, t01.mnc_time, t01.mnc_weight, t08.mnc_an_no, t08.mnc_an_yr " +
                "From PATIENT_T01 t01 "
                + "left JOIN PATIENT_M01 AS m01 ON t01.MNC_HN_NO = m01.MNC_HN_NO " +
                "left JOIN  PATIENT_M02 AS m02 ON m02.MNC_PFIX_CD = m01.MNC_PFIX_CDT  " +
                //"inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO and t01.mnc_date = t12.mnc_date and t12.mnc_pre_no = t01.mnc_pre_no " +
                //"inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO  and t12.mnc_pre_no = t01.mnc_pre_no " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                //"left join PATIENT_M18 on patient_t01.MNC_DIA_DEAD = PATIENT_M18.MNC_DIA_CD " +
                " inner join patient_m26 on t01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                "inner join PATIENT_T08 t08 on t01.MNC_PRE_NO = t08.MNC_PRE_NO and t01.MNC_date = t08.MNC_date " +
                "where t08.MNC_ds_DATE >= '" + startDate + "'  and t08.MNC_ds_DATE <= '" + endDate + "'"
                : "Select Distinct t01.mnc_hn_no, t01.MNC_PRE_NO ,t01.mnc_vn_no,m02.MNC_PFIX_DSC, m01.MNC_FNAME_T, m01.MNC_LNAME_T,t01.MNC_VN_SEQ,t01.MNC_VN_SUM,f02.MNC_FN_TYP_DSC " +
                "t01.MNC_DATE,patient_m02.MNC_PFIX_DSC as prefix,patient_m26.MNC_DOT_FNAME as Fname,patient_m26.MNC_DOT_LNAME as Lname,t01.mnc_dot_cd, M01.MNC_BDAY " +
                ", m01.mnc_id_no, t08.mnc_ds_date, t01.mnc_time, t01.mnc_weight, t08.mnc_an_no, t08.mnc_an_yr " +
                "From PATIENT_T01 t01 "
                + "left JOIN PATIENT_M01 AS m01 ON t01.MNC_HN_NO = m01.MNC_HN_NO " +
                "left JOIN  PATIENT_M02 AS m02 ON m02.MNC_PFIX_CD = m01.MNC_PFIX_CDT  " +
                //"inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO and t01.mnc_date = t12.mnc_date and t12.mnc_pre_no = t01.mnc_pre_no " +
                //"inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO and t12.mnc_pre_no = t01.mnc_pre_no " +
                " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
                " inner join patient_m26 on t01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                "inner join PATIENT_T08 t08 on t01.MNC_PRE_NO = t08.MNC_PRE_NO and t01.MNC_date = t08.MNC_date " +
                "where t01.MNC_FN_TYP_CD = '"
                + fncd + "' and t08.MNC_ds_DATE >= '" + startDate + "'  and t08.MNC_ds_DATE <= '" + endDate + "' ";
            //}
            dt = conn.selectData(sql);
            return dt;
        }
        public int selectNHSOPrintHN2(String startDate, String hn, String preno, String vn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            String[] vn1 = vn.Split('.');
            int chk = 0;
            sql = "Select count(1) as cnt " +
                    " From PATIENT_T01 t01 " +
                    " left join PHARMACY_T05 phart05 on t01.MNC_PRE_NO = phart05.MNC_PRE_NO and t01.MNC_date = phart05.mnc_date " +
                    " left join PHARMACY_T06 phart06 on phart05.MNC_CFR_NO = phart06.MNC_CFR_NO and phart05.MNC_CFG_DAT = phart06.MNC_CFR_dat " +
                    " left join PHARMACY_M01 on phart06.MNC_PH_CD = pharmacy_m01.MNC_PH_CD " +
                    " left join PHARMACY_M05 on PHARMACY_M05.MNC_PH_CD = PHARMACY_M01.MNC_PH_CD " +
                    " where " +
                    //" --t01.MNC_DATE BETWEEN '' AND '' and " +
                    " phart05.mnc_hn_no = '" + hn + "' " +
                    //" t01.mnc_hn_no = '"+hn+"' "+
                    " and phart05.MNC_PRE_NO = '" + preno + "' " +
                    //" and t01.mnc_vn_no = '" + vn1[0] + "' " +
                    //" and t01.mnc_vn_seq = '" + vn1[1] + "' " +
                    //" and t01.mnc_vn_sum = '" + vn1[2] + "' " +
                    //" --PHARMACY_M01.mnc_ph_typ_flg = 'P' " +
                    " and pharmacy_m01.MNC_PH_TN like '%' " +
                    " and phart05.MNC_CFR_STS = 'a' ";
            //" Group By phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,PHARMACY_M05.MNC_PH_PRI01,PHARMACY_M05.MNC_PH_PRI02,phart05.MNC_CFG_DAT " +
            //" Order By phart05.MNC_CFG_DAT,phart06.MNC_PH_CD ";
            dt = conn.selectData(sql);
            return int.Parse(dt.Rows[0]["cnt"].ToString());
        }
        public DataTable selectNHSOPrint3(String an)
        {
            DataTable dt = new DataTable();
            String sql = "", whereAn = "";
            
            sql = "Select Distinct t01.mnc_hn_no, t01.MNC_PRE_NO ,t01.mnc_vn_no, m02.MNC_PFIX_DSC, m01.MNC_FNAME_T, m01.MNC_LNAME_T,t01.MNC_VN_SEQ,t01.MNC_VN_SUM,f02.MNC_FN_TYP_DSC, " +
            "t01.MNC_DATE,patient_m02.MNC_PFIX_DSC as prefix,patient_m26.MNC_DOT_FNAME as Fname,patient_m26.MNC_DOT_LNAME as Lname,t01.mnc_dot_cd, M01.MNC_BDAY " +
            ", m01.mnc_id_no, t08.mnc_ds_date, t01.mnc_time, t01.mnc_weight, t08.mnc_an_no, t08.mnc_an_yr " +
            "From PATIENT_T01 t01 "
            + "left JOIN PATIENT_M01 AS m01 ON t01.MNC_HN_NO = m01.MNC_HN_NO " +
            "left JOIN  PATIENT_M02 AS m02 ON m02.MNC_PFIX_CD = m01.MNC_PFIX_CDT  " +
            //"inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO and t01.mnc_date = t12.mnc_date and t12.mnc_pre_no = t01.mnc_pre_no " +
            //"inner JOIN  PATIENT_T12 AS t12 ON t12.MNC_HN_NO = t01.MNC_HN_NO  and t12.mnc_pre_no = t01.mnc_pre_no " +
            " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
            //"left join PATIENT_M18 on patient_t01.MNC_DIA_DEAD = PATIENT_M18.MNC_DIA_CD " +
            " inner join patient_m26 on t01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
            " inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
            "inner join PATIENT_T08 t08 on t01.MNC_PRE_NO = t08.MNC_PRE_NO and t01.MNC_date = t08.MNC_date " +
            "where t08.mnc_an_no = '" + an + "'";
            //}
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectImportMDB(String datestart, String dateend, String paidcode)
        {
            DataTable dt = new DataTable();
            String sql = "", whereAn = "";

            sql = "Select t01.MNC_HN_NO,t01.MNC_PRE_NO,t01.MNC_TIME, " +
            "m01.mnc_fname_t, m01.mnc_lname_t, m02.mnc_Pfix_dsc, t01.mnc_vn_no, t01.mnc_vn_seq, t01.mnc_vn_sum,convert(VARCHAR(20),m01.mnc_bday,23) as mnc_bday " +
            ", m01.mnc_sex ,convert(VARCHAR(20),t01.mnc_date,23) as mnc_date, fnt01.MNC_SUM_PRI, m01.mnc_id_no,t01.mnc_pre_no " +
            //", fnm01.mnc_grp_ss " +
            
            "From PATIENT_T01 t01 "
            + "inner JOIN PATIENT_M01 AS m01 ON t01.MNC_HN_NO = m01.MNC_HN_NO " +
            "inner JOIN  PATIENT_M02 AS m02 ON m02.MNC_PFIX_CD = m01.MNC_PFIX_CDT  " +
            " inner join FINANCE_M02 f02 ON t01.MNC_FN_TYP_CD = f02.MNC_FN_TYP_CD " +
            "left join finance_t01 fnt01 on t01.mnc_hn_no = fnt01.mnc_hn_no and t01.mnc_pre_no = fnt01.mnc_pre_no and t01.mnc_date = fnt01.mnc_date and t01.mnc_fn_typ_cd = fnt01.mnc_fn_typ_cd " +
            //" inner join finance_t02 fnt02 on fnt01.MNC_DOC_CD = fnt02.MNC_DOC_CD and fnt01.MNC_DOC_YR = fnt02.MNC_DOC_YR and fnt01.MNC_DOC_NO = fnt02.MNC_DOC_NO and fnt01.MNC_DOC_DAT = fnt02.MNC_DOC_DAT " +
            //" inner join finance_m01 fnm01 on  fnt02.mnc_fn_cd = fnm01.mnc_fn_cd " +
            
            //"inner join PATIENT_T08 t08 on t01.MNC_PRE_NO = t08.MNC_PRE_NO and t01.MNC_date = t08.MNC_date " +
            "where t01.MNC_DATE >= '" + datestart + "' and t01.MNC_DATE <= '" + dateend + "' and t01.mnc_sts <> 'C'  and fnt01.MNC_DOC_STS = 'F' " +
            " and t01.mnc_fn_typ_cd in ("+paidcode +") " +
            "Order By t01.mnc_date, t01.mnc_pre_no ";
            //}
            dt = conn.selectData(sql);
            return dt;
        }
        public String selectDSDateAN(String hn, String vn, String preNo)
        {
            String sql = "", an = "";
            DataTable dt = new DataTable();
            String[] vn1 = vn.Split('.');
            sql = "Select t08.MNC_ds_date, t08.mnc_ds_time, t08.mnc_an_no, t08.mnc_an_yr " +
                "From PATIENT_T01 t01 " +
                "left join PATIENT_T08 t08 on t01.MNC_PRE_NO = t08.MNC_PRE_NO and t01.MNC_date = t08.MNC_date " +
                "where  t01.mnc_hn_no = '" + hn + "' " +
                    " and t01.mnc_vn_no = '" + vn1[0] + "' " +
                     " and t01.mnc_vn_seq = '" + vn1[1] + "' " +
                      " and t01.mnc_vn_sum = '" + vn1[2] + "' " +
                "and t01.mnc_pre_no = '" + preNo + "'";
            //"Order By t01.mnc_date, t01.mnc_hn_no ";
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)
            {
                sql = dt.Rows[0]["MNC_ds_date"].ToString() + "*" + FormatTime(dt.Rows[0]["MNC_ds_time"].ToString()) + "," + dt.Rows[0]["mnc_an_no"].ToString() + "/" + dt.Rows[0]["mnc_an_yr"].ToString();
            }
            return sql;
        }
        public String FormatTime(String t)
        {
            String aa = "";
            aa = "0000" + t;
            if (aa.Length >= 4)
            {
                aa = aa.Substring(aa.Length - 4, 2) + ":" + aa.Substring(aa.Length - 2);
            }
            return aa;
        }
        public DataTable selectNHSOPrintHN(String startDate, String hn, String preno, String vn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            String[] vn1 = vn.Split('.');
            sql = "Select phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,PHARMACY_M05.MNC_PH_PRI01,PHARMACY_M05.MNC_PH_PRI02,sum(phart06.MNC_PH_QTY) as qty, PHARMACY_M05.MNC_PH_PRI01 * sum(phart06.MNC_PH_QTY) as amt,phart05.MNC_CFG_DAT " +
                    //", ptt12.MNC_OR_NO, ptt12.MNC_OR_YR, ptt12.MNC_OR_DATE_S, ptt12.MNC_OR_TIME_S, ptt12.MNC_OR_HOUR " +
                    " From PATIENT_T01 t01 " +
                    " left join PHARMACY_T05 phart05 on t01.MNC_PRE_NO = phart05.MNC_PRE_NO and t01.MNC_date = phart05.mnc_date and t01.mnc_hn_yr = phart05.mnc_hn_yr " +
                    " left join PHARMACY_T06 phart06 on phart05.MNC_CFR_NO = phart06.MNC_CFR_NO and phart05.MNC_CFG_DAT = phart06.MNC_CFR_dat  and phart05.MNC_CFR_YR = phart06.MNC_CFR_YR " +
                    " left join PHARMACY_M01 on phart06.MNC_PH_CD = pharmacy_m01.MNC_PH_CD " +
                    " left join PHARMACY_M05 on PHARMACY_M05.MNC_PH_CD = PHARMACY_M01.MNC_PH_CD " +
                    //"left join PATIENT_T12 ptt12 on t01.MNC_HN_NO = ptt12.MNC_HN_NO and t01.MNC_HN_YR = ptt12.MNC_HN_YR and t01.MNC_PRE_NO = ptt12.MNC_PRE_NO and t01.MNC_DATE = ptt12.MNC_DATE and ptt12.mnc_or_sts = 'L' " +
                    " where " +
                    //" --t01.MNC_DATE BETWEEN '' AND '' and " +
                    " t01.mnc_hn_no = '" + hn + "' " +
                    //" t01.mnc_hn_no = '"+hn+"' "+
                    " and t01.MNC_PRE_NO = '" + preno + "' " +
                    " and t01.mnc_vn_no = '" + vn1[0] + "' " +
                     " and t01.mnc_vn_seq = '" + vn1[1] + "' " +
                      " and t01.mnc_vn_sum = '" + vn1[2] + "' " +
                    " and pharmacy_m01.MNC_PH_TN like '(%' " +
                    //" and t01.mnc_vn_no = '58' and t01.MNC_PRE_NO = '61' " +
                    " and phart05.MNC_CFR_STS = 'a' " +
                    " Group By phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,PHARMACY_M05.MNC_PH_PRI01,PHARMACY_M05.MNC_PH_PRI02,phart05.MNC_CFG_DAT " +
                    //", ptt12.MNC_OR_NO, ptt12.MNC_OR_YR, ptt12.MNC_OR_DATE_S, ptt12.MNC_OR_TIME_S, ptt12.MNC_OR_HOUR " +
                    " Order By phart05.MNC_CFG_DAT,phart06.MNC_PH_CD ";
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectNHSOPrintHNAll(String startDate, String hn, String preno, String vn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            String[] vn1 = vn.Split('.');
            sql = "Select phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,PHARMACY_M05.MNC_PH_PRI01,PHARMACY_M05.MNC_PH_PRI02,sum(phart06.MNC_PH_QTY) as qty, PHARMACY_M05.MNC_PH_PRI01 * sum(phart06.MNC_PH_QTY) as amt,phart05.MNC_CFG_DAT " +
                    //", ptt12.MNC_OR_NO, ptt12.MNC_OR_YR, ptt12.MNC_OR_DATE_S, ptt12.MNC_OR_TIME_S, ptt12.MNC_OR_HOUR " +
                    " From PATIENT_T01 t01 " +
                    " left join PHARMACY_T05 phart05 on t01.MNC_PRE_NO = phart05.MNC_PRE_NO and t01.MNC_date = phart05.mnc_date  and t01.mnc_hn_yr = phart05.mnc_hn_yr " +
                    " left join PHARMACY_T06 phart06 on phart05.MNC_CFR_NO = phart06.MNC_CFR_NO and phart05.MNC_CFG_DAT = phart06.MNC_CFR_dat and phart05.MNC_CFR_YR = phart06.MNC_CFR_YR " +
                    " left join PHARMACY_M01 on phart06.MNC_PH_CD = pharmacy_m01.MNC_PH_CD " +
                    " left join PHARMACY_M05 on PHARMACY_M05.MNC_PH_CD = PHARMACY_M01.MNC_PH_CD " +
                    //"left join PATIENT_T12 ptt12 on t01.MNC_HN_NO = ptt12.MNC_HN_NO and t01.MNC_HN_YR = ptt12.MNC_HN_YR and t01.MNC_PRE_NO = ptt12.MNC_PRE_NO and t01.MNC_DATE = ptt12.MNC_DATE and ptt12.mnc_or_sts = 'L'  " +
                    " where " +
                    //" --t01.MNC_DATE BETWEEN '' AND '' and " +
                    " t01.mnc_hn_no = '" + hn + "' " +
                    //" t01.mnc_hn_no = '"+hn+"' "+
                    " and t01.MNC_PRE_NO = '" + preno + "' " +
                    " and t01.mnc_vn_no = '" + vn1[0] + "' " +
                     " and t01.mnc_vn_seq = '" + vn1[1] + "' " +
                      " and t01.mnc_vn_sum = '" + vn1[2] + "' " +
                    //" and pharmacy_m01.MNC_PH_TN like '(%' " +
                    //" and t01.mnc_vn_no = '58' and t01.MNC_PRE_NO = '61' " +
                    " and phart05.MNC_CFR_STS = 'a' " +
                    " Group By phart06.MNC_PH_CD, pharmacy_m01.MNC_PH_TN ,PHARMACY_M05.MNC_PH_PRI01,PHARMACY_M05.MNC_PH_PRI02,phart05.MNC_CFG_DAT" +
                    //", ptt12.MNC_OR_NO, ptt12.MNC_OR_YR, ptt12.MNC_OR_DATE_S, ptt12.MNC_OR_TIME_S, ptt12.MNC_OR_HOUR " +
                    " Order By phart05.MNC_CFG_DAT,phart06.MNC_PH_CD ";
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectPatientOR(String hn, String preno, String an)
        {
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select ptt12.* " +
                    "From PATIENT_T12 ptt12 " +
                    //"Left Join patient_m01 m01 on t08.mnc_hn_no = m01.mnc_hn_no " +
                    //"Left Join patient_m02 m02 on m01.MNC_PFIX_CDT = m02.MNC_PFIX_CD " +
                    "Where ptt12.MNC_HN_NO='" + hn + "'  " +
                    "and ptt12.MNC_OR_STS = 'L' " +
                    "and ptt12.MNC_HN_NO='" + hn + "' and ptt12.mnc_pre_no = '" + preno + "' and ptt12.mnc_an_no = '" + an + "'";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectFinance(String hn, String hnyr, String preno, String vsdate)
        {
            DataTable dt = new DataTable();
            String sql = "", whereAn = "";

            sql = "Select fnt01.*, convert(VARCHAR(20),fnt01.MNC_Date,23) as vs_date, convert(VARCHAR(20),fnt01.mnc_doc_dat,23) as doc_date " +
            "From finance_t01 fnt01 " +
            "where fnt01.mnc_hn_no = '" + hn + "' and fnt01.mnc_hn_yr = '"+hnyr+"' and fnt01.mnc_pre_no = '"+preno+"' and fnt01.mnc_date = '"+vsdate+"' and fnt01.mnc_doc_sts = 'F' ";
            //}
            dt = conn.selectData(sql);
            return dt;
        }
        public String insertVisit(String hn, String paid_id, String symptoms, String dept, String remark, String dtrid, String userId)
        {
            String re = "";
            String sql = "";
            try
            {
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "covid_gen_patient_t01_by_hn";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("hn_where", hn);
                conn.comStore.Parameters.AddWithValue("paid_id", paid_id);
                conn.comStore.Parameters.AddWithValue("symptoms", symptoms);
                conn.comStore.Parameters.AddWithValue("dept", dept);
                conn.comStore.Parameters.AddWithValue("remark", remark);
                conn.comStore.Parameters.AddWithValue("doctor_id", dtrid);
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;
                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;

            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insertVisit sql " + sql + " hn " + hn + " paid_id " + paid_id + " symptoms " + symptoms + " dept " + dept + " remark " + remark + " dtrid " + dtrid);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertVisitBack(String hn, String paid_id, String symptoms, String dept, String remark, String dtrid, String userI, String visitdate)
        {
            String re = "";
            String sql = "";
            try
            {
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "covid_gen_patient_t01_by_hn_hi_back";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("hn_where", hn);
                conn.comStore.Parameters.AddWithValue("paid_id", paid_id);
                conn.comStore.Parameters.AddWithValue("symptoms", symptoms);
                conn.comStore.Parameters.AddWithValue("dept", dept);
                conn.comStore.Parameters.AddWithValue("remark", remark);
                conn.comStore.Parameters.AddWithValue("doctor_id", dtrid);
                conn.comStore.Parameters.AddWithValue("visit_date", visitdate);
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;
                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;

            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insertVisit sql " + sql + " hn " + hn + " paid_id " + paid_id + " symptoms " + symptoms + " dept " + dept + " remark " + remark + " dtrid " + dtrid);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertVisitHI(String hn, String paid_id, String symptoms, String dept, String remark, String dtrid, String userI)
        {
            String re = "";
            String sql = "";
            try
            {
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "covid_gen_patient_t01_by_hn_hi";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("hn_where", hn);
                conn.comStore.Parameters.AddWithValue("paid_id", paid_id);
                conn.comStore.Parameters.AddWithValue("symptoms", symptoms);
                conn.comStore.Parameters.AddWithValue("dept", dept);
                conn.comStore.Parameters.AddWithValue("remark", remark);
                conn.comStore.Parameters.AddWithValue("doctor_id", dtrid);
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;
                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;

            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insertVisit sql " + sql + " hn " + hn + " paid_id " + paid_id + " symptoms " + symptoms + " dept " + dept + " remark " + remark + " dtrid " + dtrid);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertVisitOPSI(String hn, String paid_id, String symptoms, String dept, String remark, String dtrid, String userI)
        {
            String re = "";
            String sql = "";
            try
            {
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "covid_gen_patient_t01_by_hn_opsi";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("hn_where", hn);
                conn.comStore.Parameters.AddWithValue("paid_id", paid_id);
                conn.comStore.Parameters.AddWithValue("symptoms", symptoms);
                conn.comStore.Parameters.AddWithValue("dept", dept);
                conn.comStore.Parameters.AddWithValue("remark", remark);
                conn.comStore.Parameters.AddWithValue("doctor_id", dtrid);
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;
                conn.connMainHIS.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;

            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insertVisit sql " + sql + " hn " + hn + " paid_id " + paid_id + " symptoms " + symptoms + " dept " + dept + " remark " + remark + " dtrid " + dtrid);
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String updateStatusCloseVisit(String hn, String hnyr, String preno, String vsdate)
        {
            String sql = "", re = "";
            sql = "update patient_t01 set " +
                //"mnc_act_no = '610'" +
                "mnc_act_no = '131'" +
                //", mnc_sts = 'F' " +
                ", mnc_sts = 'O' " +
                ", mnc_lab_flg = 'A' " +
                "Where mnc_hn_no = '" +hn+"' and mnc_hn_yr = '" + hnyr + "' and mnc_date = '" + vsdate + "' and mnc_pre_no ='"+preno+"'";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateStatusCloseVisit sql " + sql + " hn " + hn );
            }
            return re;
        }
        public String updateStatusCloseVisitNoLAB(String hn, String hnyr, String preno, String vsdate)
        {
            String sql = "", re = "";
            sql = "update patient_t01 set " +
                //"mnc_act_no = '610'" +
                "mnc_act_no = '131'" +
                //", mnc_sts = 'F' " +
                ", mnc_sts = 'O' " +
                ", mnc_lab_flg = '' " +
                "Where mnc_hn_no = '" + hn + "' and mnc_hn_yr = '" + hnyr + "' and mnc_date = '" + vsdate + "' and mnc_pre_no ='" + preno + "'";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "updateStatusCloseVisit sql " + sql + " hn " + hn);
            }
            return re;
        }
    }
}
