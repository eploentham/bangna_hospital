using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class PharmacyM14DB
    {
        public PharmacyM14 pharM14;
        ConnectDB conn;
        DataTable DTALL;
        public PharmacyM14DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pharM14 = new PharmacyM14();
            pharM14.MNC_PH_GRP_CD = "MNC_PH_GRP_CD";
            pharM14.MNC_PH_GRP_DSC = "MNC_PH_GRP_DSC";
            pharM14.MNC_PH_GRP_STS = "MNC_PH_GRP_STS";
            pharM14.MNC_PH_STS = "MNC_PH_STS";
            pharM14.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pharM14.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pharM14.MNC_USR_ADD = "MNC_USR_ADD";
            pharM14.MNC_USR_UPD = "MNC_USR_UPD";

            pharM14.table = "PHARMACY_M14";
            DTALL = new DataTable();
        }
        public DataTable SelectAll()
        {
            if (DTALL.Rows.Count <= 0)
            {
                String sql = "select * " +
                "From PHARMACY_M14  ";
                DTALL = conn.selectData(sql);
                //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            }
            return DTALL;
        }
        public PharmacyM14 selectByPk(String code)
        {
            PharmacyM14 cop1 = new PharmacyM14();
            String sql = "select * " +
                "From PHARMACY_M14 " +
                "Where MNC_PH_GRP_CD='" + code + "' ";
            DataTable dt = new DataTable();
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)            {                setData(cop1, dt.Rows[0]);            }
            else            {                setData(cop1);            }
            return cop1;
        }
        public void setCboDrugGroup(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            //DataTable dt = selectDeptIPD();
            int i = 0;
            DataTable dt = SelectAll();
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (DataRow arow in dt.Rows)
            {
                item = new ComboBoxItem();
                item.Value = arow["MNC_PH_GRP_CD"].ToString();
                item.Text = arow["MNC_PH_GRP_DSC"].ToString();
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
        public void chkNull(PharmacyM14 p)
        {
            if (p.MNC_PH_GRP_CD == null) p.MNC_PH_GRP_CD = "";
            if (p.MNC_PH_GRP_DSC == null) p.MNC_PH_GRP_DSC = "";
            if (p.MNC_PH_GRP_STS == null) p.MNC_PH_GRP_STS = "";
            if (p.MNC_PH_STS == null) p.MNC_PH_STS = "";
            if (p.MNC_STAMP_DAT == null) p.MNC_STAMP_DAT = "";
            if (p.MNC_STAMP_TIM == null) p.MNC_STAMP_TIM = "";
            if (p.MNC_USR_ADD == null) p.MNC_USR_ADD = "";
            if (p.MNC_USR_UPD == null) p.MNC_USR_UPD = "";
        }
        public String insert(PharmacyM14 p)
        {
            String re = "", sql = "";
            sql = "Insert Into " + pharM14.table + " (" +
                pharM14.MNC_PH_GRP_CD + "," +
                pharM14.MNC_PH_GRP_DSC + "," +
                pharM14.MNC_PH_GRP_STS + "," +
                pharM14.MNC_PH_STS + "," +
                pharM14.MNC_STAMP_DAT + "," +
                pharM14.MNC_STAMP_TIM + "," +
                pharM14.MNC_USR_ADD + "," +
                pharM14.MNC_USR_UPD + ") " +
                "Values ('" + p.MNC_PH_GRP_CD + "','" + p.MNC_PH_GRP_DSC + "','" + p.MNC_PH_GRP_STS + "','" +
                p.MNC_PH_STS + "','" + p.MNC_STAMP_DAT + "','" + p.MNC_STAMP_TIM + "','" +
                p.MNC_USR_ADD + "','" + p.MNC_USR_UPD + "')";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "PharmacyM14DB insert error " + ex.Message);
            }
            return re;
        }
        public String update(PharmacyM14 p)
        {
            String re = "", sql = "";
            sql = "Update " + pharM14.table + " Set " +
                " " + pharM14.MNC_PH_GRP_DSC + "='" + p.MNC_PH_GRP_DSC + "'" +
                "," + pharM14.MNC_PH_GRP_STS + "='" + p.MNC_PH_GRP_STS + "'" +
                "," + pharM14.MNC_PH_STS + "='" + p.MNC_PH_STS + "'" +
                "," + pharM14.MNC_STAMP_DAT + "='" + p.MNC_STAMP_DAT + "'" +
                "," + pharM14.MNC_STAMP_TIM + "='" + p.MNC_STAMP_TIM + "'" +
                "," + pharM14.MNC_USR_ADD + "='" + p.MNC_USR_ADD + "'" +
                "," + pharM14.MNC_USR_UPD + "='" + p.MNC_USR_UPD + "'" +
                "Where " + pharM14.MNC_PH_GRP_CD + "='" + p.MNC_PH_GRP_CD + "'";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "PharmacyM14DB update error " + ex.Message);
            }
            return re;
        }
        public String insertDrugAllergy(PharmacyM14 p)
        {
            String re = "";
            if (p.MNC_PH_GRP_CD.Equals(""))
            {
                re = "PharmacyM14DB insertDrugAllergy fail, No Code";
            }
            else
            {
                chkNull(p);
                PharmacyM14 chk = selectByPk(p.MNC_PH_GRP_CD);
                if (chk.MNC_PH_GRP_CD == null)
                {
                    re = insert(p);
                }
                else
                {
                    re = update(p);
                }
            }
            return re;
        }
        private void setData(PharmacyM14 p, DataRow row)
        {
            p.MNC_PH_GRP_CD = row[pharM14.MNC_PH_GRP_CD].ToString();
            p.MNC_PH_GRP_DSC = row[pharM14.MNC_PH_GRP_DSC].ToString();
            p.MNC_PH_GRP_STS = row[pharM14.MNC_PH_GRP_STS].ToString();
            p.MNC_PH_STS = row[pharM14.MNC_PH_STS].ToString();
            p.MNC_STAMP_DAT = row[pharM14.MNC_STAMP_DAT].ToString();
            p.MNC_STAMP_TIM = row[pharM14.MNC_STAMP_TIM].ToString();
            p.MNC_USR_ADD = row[pharM14.MNC_USR_ADD].ToString();
            p.MNC_USR_UPD = row[pharM14.MNC_USR_UPD].ToString();
        }
        private void setData(PharmacyM14 p)
        {
            p.MNC_PH_GRP_CD = "";
            p.MNC_PH_GRP_DSC = "";
            p.MNC_PH_GRP_STS = "";
            p.MNC_PH_STS = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_ADD = "";
            p.MNC_USR_UPD = "";
        }
    }
}
