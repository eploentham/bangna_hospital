using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class FinanceM02DB
    {
        public FinanceM02 finM02;
        ConnectDB conn;
        public List<FinanceM02> lPm02;
        private readonly object _paidIndexLock = new object();
        private Dictionary<string, string> _paidNameToCode; // normalized name -> code
        private readonly object _paidCodeIndexLock = new object();
        private Dictionary<string, string> _paidCodeToName = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
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
        public void getlCus()
        {
            //lDept = new List<Position>();

            lPm02.Clear();
            DataTable dt = new DataTable();
            dt = SelectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                FinanceM02 cus1 = new FinanceM02();
                cus1.MNC_FN_TYP_CD = row[finM02.MNC_FN_TYP_CD].ToString();
                cus1.MNC_FN_TYP_DSC = row[finM02.MNC_FN_TYP_DSC].ToString();
                lPm02.Add(cus1);
            }
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
        public AutoCompleteStringCollection getlPaid1()
        {
            if (lPm02.Count <= 0) getlCus();
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            foreach (FinanceM02 rowa in lPm02)
            {
                autoSymptom.Add(rowa.MNC_FN_TYP_DSC);
            }
            return autoSymptom;
        }
        public string getPaidNameCopilot(string paidcode)
        {
            if (string.IsNullOrWhiteSpace(paidcode)) return string.Empty;
            var key = paidcode.Trim();

            var snapshot = _paidCodeToName; // local snapshot
            string name;
            return snapshot != null && snapshot.TryGetValue(key, out name) ? name ?? string.Empty : string.Empty;
        }
        public string getPaidCodeCopilot(string paidname)
        {
            if (string.IsNullOrWhiteSpace(paidname))
                return string.Empty;

            EnsurePaidIndex();
            var key = NormalizeKey(paidname);

            string code;
            return _paidNameToCode != null && _paidNameToCode.TryGetValue(key, out code)
                ? code
                : string.Empty;
        }
        // Rebuild after lPm24 changes (bulk refresh)
        public void RebuildPaidCodeIndex()
        {
            var source = lPm02 ?? new List<FinanceM02>();
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var row in source)
            {
                if (row == null || string.IsNullOrWhiteSpace(row.MNC_FN_TYP_CD)) continue;
                var key = row.MNC_FN_TYP_CD.Trim();

                // Keep first mapping if duplicates exist
                if (!dict.ContainsKey(key))
                    dict[key] = row.MNC_FN_TYP_DSC ?? string.Empty;
            }

            Interlocked.Exchange(ref _paidCodeToName, dict);
        }
        // If you support incremental changes, wrap lPm24 mutations and update cache accordingly:
        public void ReplacePaidList(IEnumerable<FinanceM02> rows)
        {
            lPm02 = rows?.Where(r => r != null).ToList() ?? new List<FinanceM02>();
            RebuildPaidCodeIndex();
        }

        public void AddOrUpdatePaid(FinanceM02 row)
        {
            if (row == null || string.IsNullOrWhiteSpace(row.MNC_FN_TYP_CD)) return;

            lock (_paidCodeIndexLock)
            {
                var key = row.MNC_FN_TYP_CD.Trim();
                var exist = lPm02.FirstOrDefault(r => r != null &&
                                                      !string.IsNullOrWhiteSpace(r.MNC_FN_TYP_CD) &&
                                                      string.Equals(r.MNC_FN_TYP_CD.Trim(), key, StringComparison.OrdinalIgnoreCase));
                if (exist == null) lPm02.Add(row);
                else { exist.MNC_FN_TYP_DSC = row.MNC_FN_TYP_DSC; }
            }

            // clone-and-swap dictionary
            var k = row.MNC_FN_TYP_CD.Trim();
            var clone = new Dictionary<string, string>(_paidCodeToName, StringComparer.OrdinalIgnoreCase)
            {
                [k] = row.MNC_FN_TYP_DSC ?? string.Empty
            };
            Interlocked.Exchange(ref _paidCodeToName, clone);
        }

        public void RemovePaidByCode(string paidcode)
        {
            if (string.IsNullOrWhiteSpace(paidcode)) return;
            var key = paidcode.Trim();

            lock (_paidCodeIndexLock)
            {
                lPm02.RemoveAll(r => r != null &&
                                     !string.IsNullOrWhiteSpace(r.MNC_FN_TYP_CD) &&
                                     string.Equals(r.MNC_FN_TYP_CD.Trim(), key, StringComparison.OrdinalIgnoreCase));
            }

            var clone = new Dictionary<string, string>(_paidCodeToName, StringComparer.OrdinalIgnoreCase);
            clone.Remove(key);
            Interlocked.Exchange(ref _paidCodeToName, clone);
        }
        private void EnsurePaidIndex()
        {
            if (_paidNameToCode != null) return;

            lock (_paidIndexLock)
            {
                if (_paidNameToCode != null) return;

                var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                if (lPm02 != null)
                {
                    foreach (var row in lPm02)
                    {
                        if (row == null || string.IsNullOrWhiteSpace(row.MNC_FN_TYP_DSC)) continue;
                        var key = NormalizeKey(row.MNC_FN_TYP_DSC);
                        if (key.Length == 0) continue;

                        // Preserve first seen code if duplicates exist
                        if (!dict.ContainsKey(key))
                            dict[key] = row.MNC_FN_TYP_CD;
                    }
                }
                _paidNameToCode = dict;
            }
        }

        public void RebuildPaidIndex()
        {
            lock (_paidIndexLock)
            {
                _paidNameToCode = null;
            }
        }
        private static string NormalizeKey(string s)
        {
            if (s == null) return string.Empty;
            s = Regex.Replace(s.Trim(), @"\s+", " ");
            s = s.Normalize(NormalizationForm.FormKC);
            s = RemoveDiacritics(s);
            return s;
        }
        private static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder(normalized.Length);
            foreach (var c in normalized)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
        public String getPaidName(String paidcode)
        {
            String re = "";
            if (lPm02.Count <= 0) getlPaid();
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
            if (paidname.Equals("บ.ประกัน"))
            {
                paidname = "บริษัทประกัน";
                //return re;
            }
            else if (paidname.Equals("ปกส(บ.5)"))
            {
                paidname = "ประกันสังคม (บ.5)";
                //return re;
            }
            else if (paidname.Equals("ปกต(บ.5)"))
            {
                paidname = "ประกันสังคมอิสระ (บ.5)";
                //return re;
            }
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
