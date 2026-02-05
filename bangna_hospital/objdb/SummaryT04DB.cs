using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    internal class SummaryT04DB
    {
        public ConnectDB conn;
        public SummaryT04 summT04;
        DataTable DTROOM;
        public SummaryT04DB(ConnectDB c)
        {
            conn = c;
            InitConfig();
        }
        private void InitConfig()
        {
            summT04 = new SummaryT04();
            summT04.MNC_SUM_DAT = "MNC_SUM_DAT";
            summT04.MNC_RM_GEN_NO = "MNC_RM_GEN_NO";
            summT04.MNC_DOT_CD = "MNC_DOT_CD";
            summT04.MNC_MED_RM_CD = "MNC_MED_RM_CD";
            summT04.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            summT04.MNC_MED_RM_STS = "MNC_MED_RM_STS";
            summT04.MNC_SUM_VN_ADD = "MNC_SUM_VN_ADD";
            summT04.MNC_SUM_VN_ERS = "MNC_SUM_VN_ERS";
            summT04.MNC_SUM_PAT_R_ADD = "MNC_SUM_PAT_R_ADD";
            summT04.MNC_SUM_PAT_R_ERS = "MNC_SUM_PAT_R_ERS";
            summT04.MNC_SUM_CLOSE_ADD = "MNC_SUM_CLOSE_ADD";
            summT04.MNC_SUM_CLOSE_ERS = "MNC_SUM_CLOSE_ERS";
            summT04.MNC_SUM_CLOSE_R_ADD = "MNC_SUM_CLOSMNC_SUM_CLOSE_R_ADDE_ERS";
            summT04.MNC_SUM_CLOSE_R_ERS = "MNC_SUM_CLOSE_R_ERS";
            summT04.MNC_SUM_CLOSE_O_ADD = "MNC_SUM_CLOSE_O_ADD";
            summT04.MNC_SUM_CLOSE_O_ERS = "MNC_SUM_CLOSE_O_ERS";
            summT04.MNC_SUM_STS = "MNC_SUM_STS";
            summT04.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            summT04.MNC_STAMP_TIM = "MNC_STAMP_TIM";

            summT04.table = "SUMMARY_T04";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + summT04.table + " ";
            dt = conn.selectData(conn.connMainHIS, sql);

            return dt;
        }
        public DataTable selectCurDateSec(String dtrcode)
        {
            DataTable dt = new DataTable();
            String sql = "select sumt04.MNC_SUM_DAT,sumt04.MNC_DOT_CD,sumt04.MNC_MED_RM_CD,isnull(pm321.MNC_MED_RM_NAM,'') as MNC_MED_RM_NAM  " +
                ", isnull(pm02.MNC_PFIX_DSC,'')+ ' ' +pm26.MNC_DOT_FNAME +' '+ pm26.MNC_DOT_LNAME as dtrname " +
                ",CONVERT(VARCHAR(8), sumt04.MNC_SUM_DAT, 112)+convert(varchar(10),sumt04.MNC_RM_GEN_NO) AS sum04key " +
                "From " + summT04.table + " sumt04 " +
                "Left Join PATIENT_M32_1 pm321 on pm321.MNC_MED_RM_CD = sumt04.MNC_MED_RM_CD " +
                "left join PATIENT_M26 pm26 on sumt04.MNC_DOT_CD = pm26.MNC_DOT_CD " +
                "left join PATIENT_M02 pm02 on pm26.MNC_DOT_PFIX = pm02.MNC_PFIX_CD " +
                "Where sumt04." + summT04.MNC_SUM_DAT + "=convert(varchar(10),getdate(),120) " +
                "and pm321.MNC_SEC_NO " + " in (" + dtrcode + ")  ";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public SummaryT04 selectByPk(String date, String rmGenNo)
        {
            SummaryT04 p = new SummaryT04();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + summT04.table + " " +
                "Where " + summT04.MNC_SUM_DAT + "='" + date + "' " +
                "and " + summT04.MNC_RM_GEN_NO + "='" + rmGenNo + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                p = setSummaryT04(dt.Rows[0]);
            }
            else { setSummaryT04(p); }
            return p;
        }
        public void setCboSummaryT04(C1.Win.C1Input.C1ComboBox c, String secid, String selected)
        {
            DTROOM = selectCurDateSec(secid);
            if (DTROOM.Rows.Count > 0)
            {
                try
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item = new ComboBoxItem();
                    item.Value = "";
                    item.Text = "";
                    c.Items.Add(item);
                    foreach (DataRow arow in DTROOM.Rows)
                    {
                        item = new ComboBoxItem();
                        item.Value = arow["sum04key"].ToString();
                        item.Text = arow["dtrname"].ToString()+"#"+ arow["MNC_DOT_CD"].ToString() + " ["+ arow["MNC_MED_RM_NAM"].ToString()+"]";//MNC_MED_RM_NAM
                        c.Items.Add(item);
                        if (item.Value.Equals(selected))
                        {
                            //c.SelectedItem = item.Value;
                            c.SelectedText = item.Text;
                            c.SelectedIndex = c.Items.Count - 1;
                        }
                    }
                }
                catch { c.SelectedIndex = 0; }
            }
        }
        private SummaryT04 setSummaryT04(DataRow dr)
        {
            SummaryT04 p = new SummaryT04();
            p.MNC_SUM_DAT = dr[summT04.MNC_SUM_DAT].ToString();
            p.MNC_RM_GEN_NO = dr[summT04.MNC_RM_GEN_NO].ToString();
            p.MNC_DOT_CD = dr[summT04.MNC_DOT_CD].ToString();
            p.MNC_MED_RM_CD = dr[summT04.MNC_MED_RM_CD].ToString();
            p.MNC_MED_RM_STS = dr[summT04.MNC_MED_RM_STS].ToString();
            p.MNC_SUM_VN_ADD = dr[summT04.MNC_SUM_VN_ADD].ToString();
            p.MNC_SUM_VN_ERS = dr[summT04.MNC_SUM_VN_ERS].ToString();
            p.MNC_SUM_PAT_R_ADD = dr[summT04.MNC_SUM_PAT_R_ADD].ToString();
            p.MNC_SUM_PAT_R_ERS = dr[summT04.MNC_SUM_PAT_R_ERS].ToString();
            p.MNC_SUM_CLOSE_ADD = dr[summT04.MNC_SUM_CLOSE_ADD].ToString();
            p.MNC_SUM_CLOSE_ERS = dr[summT04.MNC_SUM_CLOSE_ERS].ToString();
            p.MNC_SUM_CLOSE_R_ADD = dr[summT04.MNC_SUM_CLOSE_R_ADD].ToString();
            p.MNC_SUM_CLOSE_R_ERS = dr[summT04.MNC_SUM_CLOSE_R_ERS].ToString();
            p.MNC_SUM_CLOSE_O_ADD = dr[summT04.MNC_SUM_CLOSE_O_ADD].ToString();
            p.MNC_SUM_CLOSE_O_ERS = dr[summT04.MNC_SUM_CLOSE_O_ERS].ToString();
            p.MNC_SUM_STS = dr[summT04.MNC_SUM_STS].ToString();
            p.MNC_STAMP_DAT = dr[summT04.MNC_STAMP_DAT].ToString();
            p.MNC_STAMP_TIM = dr[summT04.MNC_STAMP_TIM].ToString();
            return p;
        }
        private void setSummaryT04(SummaryT04 p)
        {
            p.MNC_SUM_DAT = "";
            p.MNC_RM_GEN_NO = "";
            p.MNC_DOT_CD = "";
            p.MNC_MED_RM_CD = "";
            p.MNC_MED_RM_STS = "";
            p.MNC_SUM_VN_ADD = "";
            p.MNC_SUM_VN_ERS = "";
            p.MNC_SUM_PAT_R_ADD = "";
            p.MNC_SUM_PAT_R_ERS = "";
            p.MNC_SUM_CLOSE_ADD = "";
            p.MNC_SUM_CLOSE_ERS = "";
            p.MNC_SUM_CLOSE_R_ADD = "";
            p.MNC_SUM_CLOSE_R_ERS = "";
            p.MNC_SUM_CLOSE_O_ADD = "";
            p.MNC_SUM_CLOSE_O_ERS = "";
            p.MNC_SUM_STS = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
        }
        private void chknull(SummaryT04 p)
        {
            if (p.MNC_SUM_DAT == null) p.MNC_SUM_DAT = "";
            if (p.MNC_RM_GEN_NO == null) p.MNC_RM_GEN_NO = "";
            if (p.MNC_DOT_CD == null) p.MNC_DOT_CD = "";
            if (p.MNC_MED_RM_CD == null) p.MNC_MED_RM_CD = "";
            if (p.MNC_MED_RM_STS == null) p.MNC_MED_RM_STS = "";
            if (p.MNC_SUM_VN_ADD == null) p.MNC_SUM_VN_ADD = "";
            if (p.MNC_SUM_VN_ERS == null) p.MNC_SUM_VN_ERS = "";
            if (p.MNC_SUM_PAT_R_ADD == null) p.MNC_SUM_PAT_R_ADD = "";
            if (p.MNC_SUM_PAT_R_ERS == null) p.MNC_SUM_PAT_R_ERS = "";
            if (p.MNC_SUM_CLOSE_ADD == null) p.MNC_SUM_CLOSE_ADD = "";
            if (p.MNC_SUM_CLOSE_ERS == null) p.MNC_SUM_CLOSE_ERS = "";
            if (p.MNC_SUM_CLOSE_R_ADD == null) p.MNC_SUM_CLOSE_R_ADD = "";
            if (p.MNC_SUM_CLOSE_R_ERS == null) p.MNC_SUM_CLOSE_R_ERS = "";
            if (p.MNC_SUM_CLOSE_O_ADD == null) p.MNC_SUM_CLOSE_O_ADD = "";
            if (p.MNC_SUM_CLOSE_O_ERS == null) p.MNC_SUM_CLOSE_O_ERS = "";
            if (p.MNC_SUM_STS == null) p.MNC_SUM_STS = "";
            if (p.MNC_STAMP_DAT == null) p.MNC_STAMP_DAT = "";
            if (p.MNC_STAMP_TIM == null) p.MNC_STAMP_TIM = "";
        }
        public String insertSummaryT04(SummaryT04 p)
        {
            String re = "";
            String sql = "";
            chknull(p);
            sql = "Select * From " + summT04.table + " " +
                "Where " + summT04.MNC_SUM_DAT + "='" + p.MNC_SUM_DAT + "' " +
                "and " + summT04.MNC_RM_GEN_NO + "='" + p.MNC_RM_GEN_NO + "' ";
            DataTable dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count <= 0)
            {
                re = insert(p);
            }
            else
            {
                re = update(p);
            }
            return re;
        }
        public String insert(SummaryT04 p)
        {
            String sql = "",re="";
            sql = "Insert Into " + summT04.table + " (" +
                summT04.MNC_SUM_DAT + "," +
                summT04.MNC_RM_GEN_NO + "," +
                summT04.MNC_DOT_CD + "," +
                summT04.MNC_MED_RM_CD + "," +
                summT04.MNC_MED_RM_STS + "," +
                summT04.MNC_SUM_VN_ADD + "," +
                summT04.MNC_SUM_VN_ERS + "," +
                summT04.MNC_SUM_PAT_R_ADD + "," +
                summT04.MNC_SUM_PAT_R_ERS + "," +
                summT04.MNC_SUM_CLOSE_ADD + "," +
                summT04.MNC_SUM_CLOSE_ERS + "," +
                summT04.MNC_SUM_CLOSE_R_ADD + "," +
                summT04.MNC_SUM_CLOSE_R_ERS + "," +
                summT04.MNC_SUM_CLOSE_O_ADD + "," +
                summT04.MNC_SUM_CLOSE_O_ERS + "," +
                summT04.MNC_SUM_STS + "," +
                summT04.MNC_STAMP_DAT + "," +
                summT04.MNC_STAMP_TIM + ") " +
                "Values ('" + p.MNC_SUM_DAT + "','" + p.MNC_RM_GEN_NO + "','" + p.MNC_DOT_CD + "','" + p.MNC_MED_RM_CD + "','" + p.MNC_MED_RM_STS + "','" +
                p.MNC_SUM_VN_ADD + "','" + p.MNC_SUM_VN_ERS + "','" + p.MNC_SUM_PAT_R_ADD + "','" + p.MNC_SUM_PAT_R_ERS + "','" +
                p.MNC_SUM_CLOSE_ADD + "','" + p.MNC_SUM_CLOSE_ERS + "','" + p.MNC_SUM_CLOSE_R_ADD + "','" + p.MNC_SUM_CLOSE_R_ERS + "','" +
                p.MNC_SUM_CLOSE_O_ADD + "','" + p.MNC_SUM_CLOSE_O_ERS + "','" + p.MNC_SUM_STS + "','" +
                p.MNC_STAMP_DAT + "','" + p.MNC_STAMP_TIM + "') ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                re = ex.Message + " " + sql;
            }
            return sql;
        }
        public String update(SummaryT04 p)
        {
            String sql = "", re="";
            sql = "Update " + summT04.table + " Set " +
                "" + summT04.MNC_DOT_CD + "='" + p.MNC_DOT_CD + "', " +
                "" + summT04.MNC_MED_RM_CD + "='" + p.MNC_MED_RM_CD + "', " +
                "" + summT04.MNC_MED_RM_STS + "='" + p.MNC_MED_RM_STS + "', " +
                "" + summT04.MNC_SUM_VN_ADD + "='" + p.MNC_SUM_VN_ADD + "', " +
                "" + summT04.MNC_SUM_VN_ERS + "='" + p.MNC_SUM_VN_ERS + "', " +
                "" + summT04.MNC_SUM_PAT_R_ADD + "='" + p.MNC_SUM_PAT_R_ADD + "', " +
                "" + summT04.MNC_SUM_PAT_R_ERS + "='" + p.MNC_SUM_PAT_R_ERS + "', " +
                "" + summT04.MNC_SUM_CLOSE_ADD + "='" + p.MNC_SUM_CLOSE_ADD + "', " +
                "" + summT04.MNC_SUM_CLOSE_ERS + "='" + p.MNC_SUM_CLOSE_ERS + "', " +
                "" + summT04.MNC_SUM_CLOSE_R_ADD + "='" + p.MNC_SUM_CLOSE_R_ADD + "', " +
                "" + summT04.MNC_SUM_CLOSE_R_ERS + "='" + p.MNC_SUM_CLOSE_R_ERS + "', " +
                "" + summT04.MNC_SUM_CLOSE_O_ADD + "='" + p.MNC_SUM_CLOSE_O_ADD + "', " +
                "" + summT04.MNC_SUM_CLOSE_O_ERS + "='" + p.MNC_SUM_CLOSE_O_ERS + "', " +
                "" + summT04.MNC_SUM_STS + "='" + p.MNC_SUM_STS + "', " +
                "" + summT04.MNC_STAMP_DAT + "='" + p.MNC_STAMP_DAT + "', " +
                "" + summT04.MNC_STAMP_TIM + "='" + p.MNC_STAMP_TIM + "' " +
                "Where " + summT04.MNC_SUM_DAT + "='" + p.MNC_SUM_DAT + "' " +
                "and " + summT04.MNC_DOT_CD + "='" + p.MNC_DOT_CD + "' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                re = ex.Message + " " + sql;
                return sql;
            }
            return re;
        }
    }
}
