using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PatientT013DB
    {// Table วงเงิน Patient_t01_3 น่าจะเป็น table detail Table Patient_t01_4 น่าจะเป็น master
        public ConnectDB conn;
        PatientT013 pt013;
        PatientT014 pt014;
        public List<PatientT013> lPm24;
        public PatientT013DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pt013 = new PatientT013();
            pt013.MNC_RUN_NO = "MNC_RUN_NO";
            pt013.MNC_RUN_DAT = "MNC_RUN_DAT";
            pt013.MNC_HN_NO = "MNC_HN_NO";
            pt013.MNC_HN_YR = "MNC_HN_YR";
            pt013.MNC_DATE = "MNC_DATE";
            pt013.MNC_PRE_NO = "MNC_PRE_NO";
            pt013.MNC_DOC_CD = "MNC_DOC_CD";
            pt013.MNC_DOC_NO = "MNC_DOC_NO";
            pt013.MNC_DOC_YR = "MNC_DOC_YR";
            pt013.MNC_DOC_DAT = "MNC_DOC_DAT";
            pt013.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            pt013.MNC_REW_PRI = "MNC_REW_PRI";
            pt013.MNC_SUM_PAD = "MNC_SUM_PAD";
            pt013.MNC_CUR_PAD = "MNC_CUR_PAD";
            pt013.MNC_DOC_STS = "MNC_DOC_STS";
            pt013.MNC_EMP_CD = "MNC_EMP_CD";
            pt013.MNC_FRM_RUN_NO = "MNC_FRM_RUN_NO";
            pt013.MNC_FRM_RUN_DAT = "MNC_FRM_RUN_DAT";
            pt013.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pt013.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pt013.MNC_FN_DOC_CD = "MNC_FN_DOC_CD";
            pt013.MNC_FN_DOC_YR = "MNC_FN_DOC_YR";
            pt013.MNC_FN_DOC_NO = "MNC_FN_DOC_NO";
            pt013.MNC_FN_DOC_DAT = "MNC_FN_DOC_DAT";

            pt013.pkField = "";
            pt013.table = "PATIENT_T01_3";
            lPm24 = new List<PatientT013>();
        }
        public PatientT014 selectByRunNo(String hn, String docDate, String docno)
        {
            DataTable dt = new DataTable();
            PatientT014 queue = new PatientT014();
            String sql = "";
            sql = "Select pt014.MNC_DOC_CD,pt014.MNC_DOC_NO,pt014.MNC_DOC_YR,convert(varchar(20),pt014.MNC_DOC_DAT,23) as MNC_DOC_DAT,pt014.MNC_HN_NO,pt014.MNC_HN_YR,pt014.MNC_FN_TYP_CD " +
                ",pt014.MNC_DIA_DSC,pt014.MNC_COM_CD,pt014.MNC_RES_MAS,pt014.MNC_REW_PRI ,pt014.MNC_SUM_PAD " +
                ",pt014.MNC_DOC_STS,pt014.MNC_EMP_CD,convert(varchar(20),pt014.MNC_STAMP_DAT, 23) as MNC_STAMP_DAT,pt014.MNC_STAMP_TIM " +
                "From PATIENT_T01_4 pt014  " +
                " Where pt014.MNC_HN_NO = '" + hn + "' " +
                "and pt014.MNC_DOC_DAT = '" + docDate + "' " +
                "and pt014.MNC_DOC_NO = '" + docno + "'" +
                " Order by pt014.MNC_DOC_NO ";
            dt = conn.selectData(sql);

            queue = setPatientT014(dt);

            return queue;
        }
        public PatientT013 selectByPreno(String hn, String visitDate, String preno)
        {
            DataTable dt = new DataTable();
            PatientT013 queue = new PatientT013();
            String sql = "";
            sql = "Select pt013.MNC_RUN_NO,pt013.MNC_RUN_DAT,pt013.MNC_HN_NO,pt013.MNC_HN_YR,pt013.MNC_DATE,pt013.MNC_PRE_NO " +
                ",pt013.MNC_DOC_CD,pt013.MNC_DOC_NO,pt013.MNC_DOC_YR,convert(varchar(20),pt013.MNC_DOC_DAT,23) as MNC_DOC_DAT,pt013.MNC_FN_TYP_CD " +
                ",pt013.MNC_REW_PRI,pt013.MNC_SUM_PAD,pt013.MNC_CUR_PAD,pt013.MNC_DOC_STS,pt013.MNC_EMP_CD " +
                ",pt013.MNC_FRM_RUN_NO,pt013.MNC_FRM_RUN_DAT,pt013.MNC_STAMP_DAT,pt013.MNC_STAMP_TIM,pt013.MNC_FN_DOC_CD " +
                ",pt013.MNC_FN_DOC_YR,pt013.MNC_FN_DOC_NO,pt013.MNC_FN_DOC_DAT " +
                ",pt014.MNC_DIA_DSC,pt014.MNC_COM_CD,pt014.MNC_RES_MAS " +
                "From PATIENT_T01_3 pt013 " +
                "Left Join PATIENT_T01_4 pt014 on pt013.MNC_DOC_DAT = pt014.MNC_DOC_DAT and pt013.MNC_DOC_NO = pt014.MNC_DOC_NO and pt013.MNC_DOC_YR = pt014.MNC_DOC_YR " +
                " Where pt013.MNC_HN_NO = '" + hn + "' " +
                "and pt013.MNC_DATE = '" + visitDate + "' " +
                "and pt013.MNC_PRE_NO = '" + preno + "'" +
                " Order by pt013.MNC_RUN_NO ";
            dt = conn.selectData(sql);
            
            queue = setPatientT013(dt);
            
            return queue;
        }
        public DataTable SelectByLimitNo(String hn, String docno, String docdate)
        {
            DataTable dt = new DataTable();

            String sql = "";
            sql = "Select pt013.MNC_RUN_NO,pt013.MNC_RUN_DAT,pt013.MNC_HN_NO,pt013.MNC_HN_YR,pt013.MNC_DATE,pt013.MNC_PRE_NO " +
                ",pt013.MNC_DOC_CD,pt013.MNC_DOC_NO,pt013.MNC_DOC_YR,convert(varchar(20),pt013.MNC_DOC_DAT,23) as MNC_DOC_DAT,pt013.MNC_FN_TYP_CD " +
                ",pt013.MNC_REW_PRI,pt013.MNC_SUM_PAD,pt013.MNC_CUR_PAD,pt013.MNC_DOC_STS,pt013.MNC_EMP_CD " +
                ",pt013.MNC_FRM_RUN_NO,pt013.MNC_FRM_RUN_DAT,pt013.MNC_STAMP_DAT,pt013.MNC_STAMP_TIM,pt013.MNC_FN_DOC_CD " +
                ",pt013.MNC_FN_DOC_YR,pt013.MNC_FN_DOC_NO,pt013.MNC_FN_DOC_DAT " +
                ",pt014.MNC_DIA_DSC,pt014.MNC_COM_CD,pt014.MNC_RES_MAS " +
                "From PATIENT_T01_3 pt013 " +
                "Left Join PATIENT_T01_4 pt014 on pt013.MNC_DOC_DAT = pt014.MNC_DOC_DAT and pt013.MNC_DOC_NO = pt014.MNC_DOC_NO and pt013.MNC_DOC_YR = pt014.MNC_DOC_YR " +
                " Where pt013.MNC_HN_NO = '" + hn + "' " +
                "and pt013.MNC_DOC_NO = '" + docno + "' " +
                "and pt013.MNC_DOC_DAT = '" + docdate + "'" +
                " Order by pt013.MNC_RUN_NO ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public void getlCus(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "", re = "";
            sql = "Select MNC_DIA_DSC, MNC_REW_PRI, MNC_SUM_PAD,convert(varchar(20),MNC_DOC_DAT,23) as MNC_DOC_DAT,MNC_DOC_NO " +
                "From PATIENT_T01_4 pt014 " +
                "Where pt014.MNC_HN_NO = '" + hn + "' and MNC_DOC_STS = 'Y' ";
            lPm24.Clear();
            dt = conn.selectData(sql);
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                PatientT013 cus1 = new PatientT013();
                cus1.DIA_DSC = row["MNC_DIA_DSC"].ToString();
                cus1.MNC_REW_PRI = row["MNC_REW_PRI"].ToString();
                cus1.MNC_SUM_PAD = row["MNC_SUM_PAD"].ToString();
                cus1.MNC_DOC_CD = row["MNC_SUM_PAD"].ToString();
                cus1.MNC_DOC_NO = row["MNC_DOC_NO"].ToString();
                cus1.MNC_DOC_DAT = row["MNC_DOC_DAT"].ToString();
                lPm24.Add(cus1);
            }
        }
        public void setCboPaidName(C1ComboBox c, String selected, String hn)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            getlCus(hn);
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (PatientT013 row in lPm24)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_DOC_NO+"@"+ row.MNC_DOC_DAT;
                item.Text = row.DIA_DSC+" "+ row.MNC_REW_PRI +" "+row.MNC_SUM_PAD;
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
        public String insert(PatientT013 p)
        {
            String re = "";
            String sql = "";
            try
            {
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connMainHIS;
                conn.comStore.CommandText = "gen_patient_t01_3by_hn";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("hn_where", p.MNC_HN_NO);
                conn.comStore.Parameters.AddWithValue("paid_id", p.MNC_FN_TYP_CD);
                conn.comStore.Parameters.AddWithValue("preno", p.MNC_PRE_NO);
                conn.comStore.Parameters.AddWithValue("vsdate", p.MNC_DATE);
                conn.comStore.Parameters.AddWithValue("limitcredit", p.MNC_REW_PRI);
                conn.comStore.Parameters.AddWithValue("vsamount", p.MNC_CUR_PAD);
                conn.comStore.Parameters.AddWithValue("userid", "");
                conn.comStore.Parameters.AddWithValue("descinsur", p.DIA_DSC);
                conn.comStore.Parameters.AddWithValue("compcode", p.RES_MAS);
                conn.comStore.Parameters.AddWithValue("insurcode", p.COM_CD);
                conn.comStore.Parameters.AddWithValue("docno", p.MNC_DOC_NO);
                conn.comStore.Parameters.AddWithValue("docdate", p.MNC_DOC_DAT);
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
                new LogWriter("e", "insertLimitCredit sql " + sql);
                re = sql;
            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String insertLimitCredit(PatientT013 p)
        {
            String re = "";
            if (p.MNC_RUN_NO.Length <= 0)
            {
                re = insert(p);
            }
            else
            {
                re = update(p);
            }
            return re;
        }
        public String update(PatientT013 p)
        {
            String re = "";
            String sql = "";
            try
            {
                sql = "Update PATIENT_T01_3 Set " +
                    ""+pt013.MNC_CUR_PAD+"="+p.MNC_CUR_PAD+""+
                    ","+pt013.MNC_SUM_PAD + "="+p.MNC_CUR_PAD + ""+
                    " Where MNC_HN_NO = '"+ p.MNC_HN_NO+ "' and MNC_RUN_NO = '"+p.MNC_RUN_NO+ "' and MNC_DATE = '"+p.MNC_DATE+ "' and MNC_PRE_NO = '"+p.MNC_PRE_NO+ "' and MNC_DOC_NO = '"+p.MNC_DOC_NO + "' "
                    ;
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                sql = "Update PATIENT_T01_4 Set " +
                    " MNC_DIA_DSC ='" + p.DIA_DSC + "' " +
                    ",MNC_COM_CD='" + p.COM_CD + "' " +
                    ",MNC_RES_MAS='" + p.RES_MAS + "' " +
                    " Where MNC_HN_NO = '" + p.MNC_HN_NO + "' and MNC_DOC_DAT = '" + p.MNC_DOC_DAT + "' and MNC_DOC_NO = '" + p.MNC_DOC_NO + "' "
                    ;
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.connMainHIS.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public PatientT013 setPatientT013(DataTable dt)
        {
            PatientT013 vs1 = new PatientT013();
            if (dt.Rows.Count > 0)
            {
                vs1.MNC_RUN_NO = dt.Rows[0]["MNC_RUN_NO"].ToString();
                vs1.MNC_RUN_DAT = dt.Rows[0]["MNC_RUN_DAT"].ToString();
                vs1.MNC_HN_NO = dt.Rows[0]["MNC_HN_NO"].ToString();
                vs1.MNC_HN_YR = dt.Rows[0]["MNC_HN_YR"].ToString();
                vs1.MNC_DATE = dt.Rows[0]["MNC_DATE"].ToString();
                vs1.MNC_PRE_NO = dt.Rows[0]["MNC_PRE_NO"].ToString();
                vs1.MNC_DOC_CD = dt.Rows[0]["MNC_DOC_CD"].ToString();
                vs1.MNC_DOC_NO = dt.Rows[0]["MNC_DOC_NO"].ToString();
                vs1.MNC_DOC_YR = dt.Rows[0]["MNC_DOC_YR"].ToString();
                vs1.MNC_DOC_DAT = dt.Rows[0]["MNC_DOC_DAT"].ToString();
                vs1.MNC_FN_TYP_CD = dt.Rows[0]["MNC_FN_TYP_CD"].ToString();
                vs1.MNC_REW_PRI = dt.Rows[0]["MNC_REW_PRI"].ToString();
                vs1.MNC_SUM_PAD = dt.Rows[0]["MNC_SUM_PAD"].ToString();
                vs1.MNC_CUR_PAD = dt.Rows[0]["MNC_CUR_PAD"].ToString();
                vs1.MNC_DOC_STS = dt.Rows[0]["MNC_DOC_STS"].ToString();
                vs1.MNC_EMP_CD = dt.Rows[0]["MNC_EMP_CD"].ToString();
                vs1.MNC_FRM_RUN_NO = dt.Rows[0]["MNC_FRM_RUN_NO"].ToString();
                vs1.MNC_FRM_RUN_DAT = dt.Rows[0]["MNC_FRM_RUN_DAT"].ToString();
                vs1.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                vs1.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                vs1.MNC_FN_DOC_CD = dt.Rows[0]["MNC_FN_DOC_CD"].ToString();
                vs1.MNC_FN_DOC_YR = dt.Rows[0]["MNC_FN_DOC_YR"].ToString();
                vs1.MNC_FN_DOC_NO = dt.Rows[0]["MNC_FN_DOC_NO"].ToString();
                vs1.MNC_FN_DOC_DAT = dt.Rows[0]["MNC_FN_DOC_DAT"].ToString();
                vs1.DIA_DSC = dt.Rows[0]["MNC_DIA_DSC"].ToString();
                vs1.COM_CD = dt.Rows[0]["MNC_COM_CD"].ToString();
                vs1.RES_MAS = dt.Rows[0]["MNC_RES_MAS"].ToString();
            }
            else
            {
                setPatientT013(vs1);
            }
            return vs1;
        }
        public PatientT013 setPatientT013(PatientT013 vs1)
        {
            vs1.MNC_RUN_NO = "";
            vs1.MNC_RUN_DAT = "";
            vs1.MNC_HN_NO = "";
            vs1.MNC_HN_YR = "";
            vs1.MNC_DATE = "";
            vs1.MNC_PRE_NO = "";
            vs1.MNC_DOC_CD = "";
            vs1.MNC_DOC_NO = "";
            vs1.MNC_DOC_YR = "";
            vs1.MNC_DOC_DAT = "";
            vs1.MNC_FN_TYP_CD = "";
            vs1.MNC_REW_PRI = "";
            vs1.MNC_SUM_PAD = "";
            vs1.MNC_CUR_PAD = "";
            vs1.MNC_DOC_STS = "";
            vs1.MNC_EMP_CD = "";
            vs1.MNC_FRM_RUN_NO = "";
            vs1.MNC_FRM_RUN_DAT = "";
            vs1.MNC_STAMP_DAT = "";
            vs1.MNC_STAMP_TIM = "";
            vs1.MNC_FN_DOC_CD = "";
            vs1.MNC_FN_DOC_YR = "";
            vs1.MNC_FN_DOC_NO = "";
            vs1.MNC_FN_DOC_DAT = "";
            vs1.DIA_DSC = "";
            vs1.COM_CD = "";
            vs1.RES_MAS = "";
            return vs1;
        }
        public PatientT014 setPatientT014(DataTable dt)
        {
            PatientT014 vs1 = new PatientT014();
            if (dt.Rows.Count > 0)
            {
                vs1.MNC_DOC_CD = dt.Rows[0]["MNC_DOC_CD"].ToString();
                vs1.MNC_DOC_NO = dt.Rows[0]["MNC_DOC_NO"].ToString();
                vs1.MNC_DOC_YR = dt.Rows[0]["MNC_DOC_YR"].ToString();
                vs1.MNC_DOC_DAT = dt.Rows[0]["MNC_DOC_DAT"].ToString();
                vs1.MNC_HN_NO = dt.Rows[0]["MNC_HN_NO"].ToString();
                vs1.MNC_HN_YR = dt.Rows[0]["MNC_HN_YR"].ToString();
                vs1.MNC_FN_TYP_CD = dt.Rows[0]["MNC_FN_TYP_CD"].ToString();
                vs1.MNC_DIA_DSC = dt.Rows[0]["MNC_DIA_DSC"].ToString();
                vs1.MNC_COM_CD = dt.Rows[0]["MNC_COM_CD"].ToString();
                vs1.MNC_RES_MAS = dt.Rows[0]["MNC_RES_MAS"].ToString();
                vs1.MNC_REW_PRI = dt.Rows[0]["MNC_REW_PRI"].ToString();
                vs1.MNC_SUM_PAD = dt.Rows[0]["MNC_SUM_PAD"].ToString();
                vs1.MNC_DOC_STS = dt.Rows[0]["MNC_DOC_STS"].ToString();
                vs1.MNC_EMP_CD = dt.Rows[0]["MNC_EMP_CD"].ToString();
                vs1.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                vs1.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                
            }
            else
            {
                setPatientT014(vs1);
            }
            return vs1;
        }
        public PatientT014 setPatientT014(PatientT014 vs1)
        {
            vs1.MNC_DOC_CD = "";
            vs1.MNC_DOC_NO = "";
            vs1.MNC_DOC_YR = "";
            vs1.MNC_DOC_DAT = "";
            vs1.MNC_HN_NO = "";
            vs1.MNC_HN_YR = "";
            vs1.MNC_FN_TYP_CD = "";
            vs1.MNC_DIA_DSC = "";
            vs1.MNC_COM_CD = "";
            vs1.MNC_RES_MAS = "";
            vs1.MNC_REW_PRI = "";
            vs1.MNC_SUM_PAD = "";
            vs1.MNC_DOC_STS = "";
            vs1.MNC_EMP_CD = "";
            vs1.MNC_STAMP_DAT = "";
            vs1.MNC_STAMP_TIM = "";
            
            return vs1;
        }
    }
}
