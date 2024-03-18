using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class DotDfDetailDB
    {
        public DotDfDetail dotdfd;
        ConnectDB conn;

        public DotDfDetailDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            dotdfd = new DotDfDetail();
            dotdfd.MNC_DOC_CD = "MNC_DOC_CD";
            dotdfd.MNC_DOC_YR = "MNC_DOC_YR";
            dotdfd.MNC_DOC_NO = "MNC_DOC_NO";
            dotdfd.MNC_DOC_DAT = "MNC_DOC_DAT";
            dotdfd.MNC_FN_CD = "MNC_FN_CD";
            dotdfd.MNC_FN_NO = "MNC_FN_NO";
            dotdfd.MNC_FN_DAT = "MNC_FN_DAT";
            dotdfd.MNC_DF_DATE = "MNC_DF_DATE";
            dotdfd.MNC_FN_TYP_DESC = "MNC_FN_TYP_DESC";
            dotdfd.MNC_DF_AMT = "MNC_DF_AMT";
            dotdfd.MNC_FN_AMT = "MNC_FN_AMT";
            dotdfd.MNC_DATE = "MNC_DATE";
            dotdfd.MNC_TIME = "MNC_TIME";
            dotdfd.MNC_HN_NO = "MNC_HN_NO";
            dotdfd.MNC_HN_YR = "MNC_HN_YR";
            dotdfd.MNC_AN_NO = "MNC_AN_NO";
            dotdfd.MNC_AN_YR = "MNC_AN_YR";
            dotdfd.MNC_PAT_NAME = "MNC_PAT_NAME";
            dotdfd.MNC_PRE_NO = "MNC_PRE_NO";
            dotdfd.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            dotdfd.MNC_DOT_CD_DF = "MNC_DOT_CD_DF";
            dotdfd.MNC_DOT_GRP_CD = "MNC_DOT_GRP_CD";
            dotdfd.MNC_DOT_NAME = "MNC_DOT_NAME";
            dotdfd.MNC_PAY_FLAG = "MNC_PAY_FLAG";
            dotdfd.MNC_PAY_DAT = "MNC_PAY_DAT";
            dotdfd.MNC_PAY_NO = "MNC_PAY_NO";
            dotdfd.MNC_PAY_YR = "MNC_PAY_YR";
            dotdfd.MNC_REF_NO = "MNC_REF_NO";
            dotdfd.MNC_REF_DAT = "MNC_REF_DAT";
            dotdfd.MNC_EMP_CD = "MNC_EMP_CD";
            dotdfd.MNC_DF_GROUP = "MNC_DF_GROUP";
            dotdfd.MNC_PAY_TYP = "MNC_PAY_TYP";
            dotdfd.MNC_PAY_RATE = "MNC_PAY_RATE";
            dotdfd.MNC_DF_DET_TYPE = "MNC_DF_DET_TYPE";
            dotdfd.status_insert_manual = "status_insert_manual";

            dotdfd.table = "dotdf_detail";
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select pm01.*, pm05.mnc_ph_pri01, pm05.mnc_ph_pri02, pm05.mnc_ph_pri03 " +
                "From pharmacy_m01 pm01 " +
                "inner join pharmacy_m05 pm05 on pm01.mnc_ph_cd = pm05.mnc_ph_cd " +
                //"Where MNC_FN_TYP_STS = 'Y' " +
                "Order By pm01.MNC_PH_CD";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectDtrDFByPreno(String hn, String vsdate, String preno)
        {
            DataTable dt = new DataTable();

            String sql = "select convert(varchar(20),dfd.MNC_DF_DATE,23) as DF_DATE, convert(varchar(20),dfd.MNC_FN_DAT,23) as FN_DAT,dfd.MNC_DOT_CD_DF " +
                "From DOTDF_DETAIL dfd " +
                " " +
                "Where dfd.MNC_HN_NO = '" + hn + "' and dfd.MNC_PRE_NO = '" + preno + "' and dfd.MNC_DATE = '" + vsdate + "' and dfd.MNC_DOT_CD_DF != '20799' " +
                "Order By dfd.MNC_DOC_NO ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectByDFdate(String startdate, String enddate, String dtrcode)
        {
            DataTable dt = new DataTable();
            String wheredtrcode = "";
            if (dtrcode.Length > 0)
            {
                wheredtrcode = " and dfd.MNC_DOT_CD_DF = '" + dtrcode + "'";
            }
            String sql = "SELECT '' as cnt_grp_dtr_02,'' as cnt_grp_dtr_44,'' as cnt_grp_dtr_credit,'' as sum_grp_dtr_02,'' as sum_grp_dtr_44,'' as sum_grp_dtr_credit " +
                ",'' as cnt_grp_dtr_02_out,'' as cnt_grp_dtr_44_out,'' as cnt_grp_dtr_credit_out,'' as sum_grp_dtr_02_out,'' as sum_grp_dtr_44_out,'' as sum_grp_dtr_credit_out " +
                ",'' as DF_DATE_show,'' as FN_DATE_show,'' as row_number, dfd.MNC_DOC_CD, dfd.MNC_DOC_YR, dfd.MNC_DOC_NO  " +
                ", convert(varchar(20),dfd.MNC_DOC_DAT,23) as MNC_DOC_DAT, convert(varchar(20),dfd.MNC_DF_DATE,23) as DF_DATE,dfd.MNC_FN_CD, dfd.MNC_FN_NO, convert(varchar(20),dfd.MNC_FN_DAT,23) as FN_DAT " +
                ", dfd.MNC_FN_TYP_DESC as item_name, dfd.MNC_DF_AMT as DF_AMT, dfd.MNC_FN_AMT as FN_AMT, convert(varchar(20),dfd.MNC_DATE,23) as vsdate " +
                ", dfd.MNC_HN_NO as hn, dfd.MNC_HN_YR, dfd.MNC_AN_NO, dfd.MNC_AN_YR " +
                ", dfd.MNC_PAT_NAME as PAT_NAME, dfd.MNC_PRE_NO as preno, dfd.MNC_FN_TYP_CD " +
                ", dfd.MNC_DOT_CD_DF, dfd.MNC_DOT_GRP_CD, dfd.MNC_DOT_NAME as dtr_name " +
                ", dfd.MNC_PAY_FLAG, dfd.MNC_DF_DET_TYPE, convert(varchar(20),dfd.MNC_PAY_DAT,23) as MNC_PAY_DAT, dfd.MNC_REF_NO, convert(varchar(20),dfd.MNC_REF_DAT,23) as MNC_REF_DAT " +
                ", finm02.MNC_FN_TYP_DSC as paid_name, finm02.MNC_FN_STS, pt01.MNC_VN_NO, pt01.MNC_VN_SEQ,convert(varchar(20),pt01.MNC_VN_NO) + '.'+ convert(varchar(20),pt01.MNC_VN_SEQ)+'.'+convert(varchar(20),pt01.MNC_VN_SUM) as vn " +
                ", pt01.MNC_VN_SUM, isnull(dfdg.MNC_DF_GRP,'') as DF_GRP, isnull(dfdg.MNC_DF_GRP_DSC,'') as DF_GRP_DSC,convert(varchar(20),dfd.MNC_PAY_TYP) as PAY_TYPE,dfd.MNC_PAY_RATE as PAY_RATE " +
                "From DOTDF_DETAIL dfd " +
                "Left JOIN  FINANCE_M02 finm02 ON dfd.MNC_FN_TYP_CD = finm02.MNC_FN_TYP_CD " +
                "INNER JOIN PATIENT_T01 pt01 ON dfd.MNC_HN_YR = pt01.MNC_HN_YR AND dfd.MNC_HN_NO = pt01.MNC_HN_NO AND dfd.MNC_DATE = pt01.MNC_DATE AND dfd.MNC_PRE_NO = pt01.MNC_PRE_NO " +
                "Left JOIN DOTDF_GROUP dfdg ON dfd.MNC_DF_GROUP = dfdg.MNC_DF_GRP " +
                "Where dfd.MNC_DF_DATE >= '" + startdate + "' and dfd.MNC_DF_DATE <= '" + enddate + "'  and dfd.MNC_DF_AMT <> '0' " + wheredtrcode +
                "Order By dfd.MNC_DOT_CD_DF, dfd.MNC_DOT_GRP_CD, dfd.MNC_FN_DAT, dfd.MNC_DATE, dfd.MNC_PRE_NO, dfd.MNC_DF_GROUP ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public void setCboTumbonName(C1ComboBox c, String datestart, String dateend, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();

            DataTable dt = new DataTable();

            String sql = "", re = "";
            sql = "Select distinct isnull(dfd.MNC_DOT_CD_DF,'') as MNC_DOT_CD_DF ,(isnull(pm02dtr.MNC_PFIX_DSC,'') +' ' +isnull(pm26.MNC_DOT_FNAME,'') + ' ' + isnull(pm26.MNC_DOT_LNAME,'')) as dtr_name " +
                "From  DOTDF_DETAIL dfd " +
                "Left join PATIENT_M26 pm26 on dfd.MNC_DOT_CD_DF = pm26.MNC_DOT_CD " +
                "left JOIN PATIENT_M02 as pm02dtr ON pm26.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                " Where dfd.MNC_DF_DATE >= '" + datestart + "' and dfd.MNC_DF_DATE <= '" + dateend + "' Order By dtr_name ";
            dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                c.Items.Clear();
                //item = new ComboBoxItem();
                //item.Value = "";
                //item.Text = "";
                //c.Items.Add(item);
                foreach (DataRow row in dt.Rows)
                {
                    if (row["MNC_DOT_CD_DF"].ToString().Equals("")) continue;
                    item = new ComboBoxItem();
                    item.Value = row["MNC_DOT_CD_DF"].ToString();
                    item.Text = row["dtr_name"].ToString();
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
        private void chkNull(DotDfDetail p)
        {
            long chk = 0;
            decimal chk1 = 0;

            p.MNC_DOC_CD = p.MNC_DOC_CD == null ? "" : p.MNC_DOC_CD;
            p.MNC_DOC_DAT = p.MNC_DOC_DAT == null ? "" : p.MNC_DOC_DAT;
            p.MNC_FN_CD = p.MNC_FN_CD == null ? "" : p.MNC_FN_CD;
            p.MNC_FN_DAT = p.MNC_FN_DAT == null ? "" : p.MNC_FN_DAT;
            p.MNC_DF_DATE = p.MNC_DF_DATE == null ? "" : p.MNC_DF_DATE;
            p.MNC_FN_TYP_DESC = p.MNC_FN_TYP_DESC == null ? "" : p.MNC_FN_TYP_DESC;
            p.MNC_DATE = p.MNC_DATE == null ? "" : p.MNC_DATE;
            p.MNC_PAT_NAME = p.MNC_PAT_NAME == null ? "" : p.MNC_PAT_NAME;
            p.MNC_FN_TYP_CD = p.MNC_FN_TYP_CD == null ? "" : p.MNC_FN_TYP_CD;
            p.MNC_DOT_CD_DF = p.MNC_DOT_CD_DF == null ? "" : p.MNC_DOT_CD_DF;
            p.MNC_DOT_GRP_CD = p.MNC_DOT_GRP_CD == null ? "" : p.MNC_DOT_GRP_CD;
            p.MNC_DOT_NAME = p.MNC_DOT_NAME == null ? "" : p.MNC_DOT_NAME;
            p.MNC_PAY_FLAG = p.MNC_PAY_FLAG == null ? "" : p.MNC_PAY_FLAG;
            p.MNC_PAY_DAT = p.MNC_PAY_DAT == null ? "" : p.MNC_PAY_DAT;
            p.MNC_REF_NO = p.MNC_REF_NO == null ? "" : p.MNC_REF_NO;
            p.MNC_REF_DAT = p.MNC_REF_DAT == null ? "" : p.MNC_REF_DAT;
            p.MNC_EMP_CD = p.MNC_EMP_CD == null ? "" : p.MNC_EMP_CD;
            p.MNC_DF_DET_TYPE = p.MNC_DF_DET_TYPE == null ? "" : p.MNC_DF_DET_TYPE;
            p.status_insert_manual = p.status_insert_manual == null ? "" : p.status_insert_manual;

            p.MNC_DOC_YR = long.TryParse(p.MNC_DOC_YR, out chk) ? chk.ToString() : "0";
            p.MNC_DOC_NO = long.TryParse(p.MNC_DOC_NO, out chk) ? chk.ToString() : "0";
            p.MNC_FN_NO = long.TryParse(p.MNC_FN_NO, out chk) ? chk.ToString() : "0";
            p.MNC_TIME = long.TryParse(p.MNC_TIME, out chk) ? chk.ToString() : "0";
            p.MNC_HN_NO = long.TryParse(p.MNC_HN_NO, out chk) ? chk.ToString() : "0";
            p.MNC_HN_YR = long.TryParse(p.MNC_HN_YR, out chk) ? chk.ToString() : "0";
            p.MNC_AN_NO = long.TryParse(p.MNC_AN_NO, out chk) ? chk.ToString() : "0";
            p.MNC_AN_YR = long.TryParse(p.MNC_AN_YR, out chk) ? chk.ToString() : "0";
            p.MNC_PRE_NO = long.TryParse(p.MNC_PRE_NO, out chk) ? chk.ToString() : "0";
            p.MNC_PAY_NO = long.TryParse(p.MNC_PAY_NO, out chk) ? chk.ToString() : "0";
            p.MNC_PAY_YR = long.TryParse(p.MNC_PAY_YR, out chk) ? chk.ToString() : "0";
            p.MNC_DF_GROUP = long.TryParse(p.MNC_DF_GROUP, out chk) ? chk.ToString() : "0";
            p.MNC_PAY_TYP = long.TryParse(p.MNC_PAY_TYP, out chk) ? chk.ToString() : "0";

            p.MNC_DF_AMT = decimal.TryParse(p.MNC_DF_AMT, out chk1) ? chk1.ToString() : "0";
            p.MNC_FN_AMT = decimal.TryParse(p.MNC_FN_AMT, out chk1) ? chk1.ToString() : "0";
            p.MNC_PAY_RATE = decimal.TryParse(p.MNC_PAY_RATE, out chk1) ? chk1.ToString() : "0";
        }
        public String insert(DotDfDetail p)
        {
            String sql = "", chk = "";
            try
            {
                chkNull(p);
                sql = "select * from " + dotdfd.table
                    + " Where " + dotdfd.MNC_DOC_CD + "= '" + p.MNC_DOC_CD + "'"
                    + " and " + dotdfd.MNC_DOC_YR + "= '" + p.MNC_DOC_YR + "'"
                    + " and " + dotdfd.MNC_DOC_NO + "= '" + p.MNC_DOC_NO + "'"
                    + " and " + dotdfd.MNC_DOC_DAT + "= '" + p.MNC_DOC_DAT + "'"
                    + " and  " + dotdfd.MNC_FN_CD + "= '" + p.MNC_FN_CD + "'"
                    + " and " + dotdfd.MNC_FN_DAT + "= '" + p.MNC_FN_DAT + "'"
                    + " and " + dotdfd.MNC_HN_NO + "= '" + p.MNC_HN_NO + "'"
                    + " and " + dotdfd.MNC_HN_YR + "= '" + p.MNC_HN_YR + "'"
                    + " and  " + dotdfd.MNC_PRE_NO + "= '" + p.MNC_PRE_NO + "'"
                    + " and " + dotdfd.MNC_DATE + "= '" + p.MNC_DATE + "'";
                DataTable dt = conn.selectData(conn.connMainHIS, sql);
                if (dt.Rows.Count > 0) return "";

                sql = "Delete from " + dotdfd.table 
                    +" Where " + dotdfd.MNC_DOC_CD + "= '" + p.MNC_DOC_CD + "'"
                    + " and " + dotdfd.MNC_DOC_YR + "= '" + p.MNC_DOC_YR + "'"
                    + " and " + dotdfd.MNC_DOC_NO + "= '" + p.MNC_DOC_NO + "'"
                    + " and " + dotdfd.MNC_DOC_DAT + "= '" + p.MNC_DOC_DAT + "'"
                    + " and  " + dotdfd.MNC_FN_CD + "= '" + p.MNC_FN_CD + "'"
                    + " and " + dotdfd.MNC_FN_DAT + "= '" + p.MNC_FN_DAT + "'"
                    + " and " + dotdfd.MNC_HN_NO + "= '" + p.MNC_HN_NO + "'"
                    + " and " + dotdfd.MNC_HN_YR + "= '" + p.MNC_HN_YR + "'"
                    + " and  " + dotdfd.MNC_PRE_NO + "= '" + p.MNC_PRE_NO + "'"
                    + " and " + dotdfd.MNC_DATE + "= '" + p.MNC_DATE + "'"
                    + " and " + dotdfd.status_insert_manual + "= '1' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);

                sql = "Insert Into " + dotdfd.table + "(" + dotdfd.MNC_DOC_CD + "," + dotdfd.MNC_DOC_YR + "," + dotdfd.MNC_DOC_NO + "," +
                    dotdfd.MNC_DOC_DAT + "," + dotdfd.MNC_FN_CD + "," + dotdfd.MNC_FN_NO + "," +
                    dotdfd.MNC_FN_DAT + "," + dotdfd.MNC_DF_DATE + "," + dotdfd.MNC_FN_TYP_DESC + "," +
                    dotdfd.MNC_DF_AMT + "," + dotdfd.MNC_FN_AMT + "," + dotdfd.MNC_DATE + "," +
                    dotdfd.MNC_TIME + "," + dotdfd.MNC_HN_NO + "," + dotdfd.MNC_HN_YR + "," +
                    dotdfd.MNC_AN_NO + "," + dotdfd.MNC_AN_YR + "," + dotdfd.MNC_PAT_NAME + "," +
                    dotdfd.MNC_PRE_NO + "," + dotdfd.MNC_FN_TYP_CD + "," + dotdfd.MNC_DOT_CD_DF + "," +
                    dotdfd.MNC_DOT_GRP_CD + "," + dotdfd.MNC_DOT_NAME + "," + dotdfd.MNC_PAY_FLAG + "," +
                    dotdfd.MNC_DF_GROUP + "," + dotdfd.MNC_PAY_TYP + "," + dotdfd.MNC_PAY_RATE + "," +
                    dotdfd.status_insert_manual +
                    ") " +
                    "Values('" + p.MNC_DOC_CD + "','" + p.MNC_DOC_YR + "','" + p.MNC_DOC_NO + "','" +
                    p.MNC_DOC_DAT + "','" + p.MNC_FN_CD + "','" + p.MNC_FN_NO + "','" +
                    p.MNC_FN_DAT + "','" + p.MNC_DF_DATE.Replace("'", "''") + "','" + p.MNC_FN_TYP_DESC.Replace("'", "''") + "','" +
                    p.MNC_DF_AMT + "','" + p.MNC_FN_AMT + "','" + p.MNC_DATE + "','" +
                    p.MNC_TIME + "','" + p.MNC_HN_NO + "','" + p.MNC_HN_YR + "','" +
                    p.MNC_AN_NO + "','" + p.MNC_AN_YR + "','" + p.MNC_PAT_NAME + "','" +
                    p.MNC_PRE_NO + "','" + p.MNC_FN_TYP_CD + "','" + p.MNC_DOT_CD_DF + "','" +
                    p.MNC_DOT_GRP_CD + "','" + p.MNC_DOT_NAME + "','" + p.MNC_PAY_FLAG + "','" +
                    p.MNC_DF_GROUP + "','" + p.MNC_PAY_TYP + "','" + p.MNC_PAY_RATE + "','1'" +
                    ") ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                chk = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "insert Insert  sql " + sql + " ex " + chk);
            }
            return chk;
        }
    }
}
