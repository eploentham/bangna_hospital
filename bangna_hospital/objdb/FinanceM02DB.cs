using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class FinanceM02DB
    {
        public FinanceM02 finM02;
        ConnectDB conn;
        public List<FinanceM02> lPm02;
        public FinanceM02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            finM02 = new FinanceM02();
            finM02.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            finM02.MNC_FN_TYP_DSC = "MNC_FN_TYP_DSC";
            finM02.MNC_FN_SRP = "MNC_FN_SRP";
            finM02.MNC_FN_SRV = "MNC_FN_SRV";
            finM02.MNC_FN_DSP = "MNC_FN_DSP";
            finM02.MNC_FN_DSV = "MNC_FN_DSV";
            finM02.MNC_FN_STS = "MNC_FN_STS";
            finM02.MNC_COD_PRI_LB = "MNC_COD_PRI_LB";
            finM02.MNC_COD_PRI_PH = "MNC_COD_PRI_PH";
            finM02.MNC_COD_PRI_XR = "MNC_COD_PRI_XR";
            finM02.MNC_COD_PRI_PHY = "MNC_COD_PRI_PHY";
            finM02.MNC_COD_PRI_LBI = "MNC_COD_PRI_LBI";
            finM02.MNC_COD_PRI_PHI = "MNC_COD_PRI_PHI";
            finM02.MNC_COD_PRI_XRI = "MNC_COD_PRI_XRI";
            finM02.MNC_COD_PRI_PHYI = "MNC_COD_PRI_PHYI";
            finM02.MNC_COD_PRI_SUP = "MNC_COD_PRI_SUP";
            finM02.MNC_COD_PRI_SUPI = "MNC_COD_PRI_SUPI";
            finM02.MNC_COD_PRI_HOS = "MNC_COD_PRI_HOS";
            finM02.MNC_COD_PRI_HOSI = "MNC_COD_PRI_HOSI";
            finM02.MNC_FN_TYP_STS = "MNC_FN_TYP_STS";
            finM02.PTTYP = "PTTYP";
            finM02.MNC_TYP_REP_CD = "MNC_TYP_REP_CD";
            finM02.MNC_RGT_CD = "MNC_RGT_CD";
            finM02.MNC_REP_NO = "MNC_REP_NO";
            finM02.MNC_PCK_STS = "MNC_PCK_STS";
            finM02.MNC_PCK_FN_CD = "MNC_PCK_FN_CD";
            finM02.MNC_PCK_FN_AMT = "MNC_PCK_FN_AMT";
            finM02.MNC_INPUT_COM_CONT_STS = "MNC_INPUT_COM_CONT_STS";
            finM02.MNC_CHK_ID = "MNC_CHK_ID";
            finM02.MNC_REW_FLG = "MNC_REW_FLG";
            finM02.MNC_REW_FLG = "MNC_REW_PRI";
            finM02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            finM02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            finM02.MNC_USR_ADD = "MNC_USR_ADD";
            finM02.MNC_USR_UPD = "MNC_USR_UPD";
            finM02.MNC_ACCOUNT_NO = "MNC_ACCOUNT_NO";
            finM02.opbkk_inscl = "opbkk_inscl";

            lPm02 = new List<FinanceM02>();
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            
            String sql = "select * " +
                "From finance_m02  " +
                "Where MNC_FN_TYP_STS = 'Y' " +
                "Order By mnc_fn_typ_cd";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public String SelectpaidTypeName(String code)
        {
            DataTable dt = new DataTable();
            String re = "";
            String sql = "select * " +
                "From finance_m02  " +
                "Where MNC_FN_TYP_CD = '" + code + "' " +
                "Order By mnc_fn_typ_cd";
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)
            {
                re = dt.Rows[0]["MNC_FN_TYP_DSC"].ToString();
            }
            return re;
        }
        public AutoCompleteStringCollection getlPaid()
        {
            //lDept = new List<Position>();
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            lPm02.Clear();
            DataTable dt = new DataTable();
            dt = SelectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                autoSymptom.Add(row[finM02.MNC_FN_TYP_DSC].ToString());
                FinanceM02 cus1 = new FinanceM02();
                cus1.MNC_FN_TYP_CD = row[finM02.MNC_FN_TYP_CD].ToString();
                cus1.MNC_FN_TYP_DSC = row[finM02.MNC_FN_TYP_DSC].ToString();

                lPm02.Add(cus1);
            }
            return autoSymptom;
        }
        public String getPaidName(String paidcode)
        {
            String re = "";
            foreach (FinanceM02 row in lPm02)
            {
                if (row.MNC_FN_TYP_CD.Equals(paidcode))
                {
                    re = row.MNC_FN_TYP_DSC;
                    break;
                }
            }
            return re;
        }
        public String getPaidCode(String paidname)
        {
            String re = "";
            foreach (FinanceM02 row in lPm02)
            {
                if (row.MNC_FN_TYP_DSC.Equals(paidname))
                {
                    re = row.MNC_FN_TYP_CD;
                    break;
                }
            }
            return re;
        }
        public void setCboPaidName(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            if (lPm02.Count <= 0) getlPaid();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (FinanceM02 row in lPm02)
            {
                item = new ComboBoxItem();
                item.Value = row.MNC_FN_TYP_CD;
                item.Text = row.MNC_FN_TYP_DSC;
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
        public String updateOPBKKCode(String paidtypeid, String opbkkcode)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update finance_m02 Set " +
                    "opbkk_inscl ='" + opbkkcode + "' " +
                    "Where mnc_fn_typ_cd ='" + paidtypeid + "'";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " FinanceM02DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
        public String updateSSOPClaimCatCode(String paidtypeid, String opbkkcode)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update finance_m02 Set " +
                    "ssop_claimcat ='" + opbkkcode + "' " +
                    "Where mnc_fn_typ_cd ='" + paidtypeid + "'";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " FinanceM02DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
    }
}
