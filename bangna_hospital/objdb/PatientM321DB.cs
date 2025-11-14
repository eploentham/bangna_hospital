using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    internal class PatientM321DB
    {
        public ConnectDB conn;
        public PatientM321 pm321;
        internal DataTable DTRoomUse;
        public PatientM321DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            pm321 = new PatientM321();
            pm321.MNC_DEP_NO = "MNC_DEP_NO";
            pm321.MNC_SEC_NO = "MNC_SEC_NO";
            pm321.MNC_MED_RM_CD = "MNC_MED_RM_CD";
            pm321.MNC_MED_RM_NAM = "MNC_MED_RM_NAM";
            pm321.MNC_MED_RM_STS = "MNC_MED_RM_STS";
            pm321.MNC_MED_RMUSE_STS = "MNC_MED_RMUSE_STS";
            pm321.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            pm321.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            pm321.MNC_USR_ADD = "MNC_USR_ADD";
            pm321.MNC_USR_UPD = "MNC_USR_UPD";
            pm321.MNC_DIV_NO = "MNC_DIV_NO";

            pm321.table = "patient_M32_1";

        }
        public String selectAll()
        {
            String sql = "select * " +
                "From " + pm321.table + " ";
            //DTRoomUse = conn.selectData(conn.connMainHIS, sql);
            return sql;
        }
        public PatientM321 selectByPk(String depNo, String secNo, String medRmCd)
        {
            PatientM321 p = new PatientM321();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + pm321.table + " " +
                "Where " + pm321.MNC_DEP_NO + "='" + depNo + "' " +
                "and " + pm321.MNC_SEC_NO + "='" + secNo + "' " +
                "and " + pm321.MNC_MED_RM_CD + "='" + medRmCd + "' ";
            dt = conn.selectData(conn.connMainHIS, sql);
            p = setPateintM321(dt);
            return p;
        }
        public void setCboRoomDoctor(C1.Win.C1Input.C1ComboBox c, String secid, String selected)
        {
            String sql = "select " + pm321.MNC_MED_RM_CD + ", " + pm321.MNC_MED_RM_NAM + " " +
                "From " + pm321.table + " " +
                "Where " + pm321.MNC_MED_RMUSE_STS + "='Y' and " + pm321.MNC_SEC_NO + "='"+ secid + "' " +
                "Order By " + pm321.MNC_MED_RM_NAM + " ";
            if(DTRoomUse==null || DTRoomUse.Rows.Count <= 0)
            {
                DTRoomUse = conn.selectData(conn.connMainHIS, sql);
            }
            ComboBoxItem item = new ComboBoxItem();
            int i = 0;
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (DataRow item1 in DTRoomUse.Rows)
            {
                item = new ComboBoxItem();
                item.Value = item1[pm321.MNC_MED_RM_CD].ToString();
                item.Text = item1[pm321.MNC_MED_RM_NAM].ToString();
                c.Items.Add(item);
                if (item.Value.Equals(selected))
                {
                    c.SelectedIndex = i + 1;
                }
                i++;

            }
        }
        public String insert(PatientM321 p)
        {
            String sql = "Insert Into " + pm321.table + " (" +
                "" + pm321.MNC_DEP_NO + ", " +
                "" + pm321.MNC_SEC_NO + ", " +
                "" + pm321.MNC_MED_RM_CD + ", " +
                "" + pm321.MNC_MED_RM_NAM + ", " +
                "" + pm321.MNC_MED_RM_STS + ", " +
                "" + pm321.MNC_MED_RMUSE_STS + ", " +
                "" + pm321.MNC_STAMP_DAT + ", " +
                "" + pm321.MNC_STAMP_TIM + ", " +
                "" + pm321.MNC_USR_ADD + ", " +
                "" + pm321.MNC_USR_UPD + ", " +
                "" + pm321.MNC_DIV_NO + ") " +
                "Values ('" + p.MNC_DEP_NO + "', '" +
                "" + p.MNC_SEC_NO + "', '" +
                "" + p.MNC_MED_RM_CD + "', '" +
                "" + p.MNC_MED_RM_NAM + "', '" +
                "" + p.MNC_MED_RM_STS + "', '" +
                "" + p.MNC_MED_RMUSE_STS + "', '" +
                "" + p.MNC_STAMP_DAT + "', '" +
                "" + p.MNC_STAMP_TIM + "', '" +
                "" + p.MNC_USR_ADD + "', '" +
                "" + p.MNC_USR_UPD + "', '" +
                "" + p.MNC_DIV_NO + "') ";
            return sql;
        }
        public String update(PatientM321 p)
        {
            String sql = "Update " + pm321.table + " Set " +
                "" + pm321.MNC_MED_RM_NAM + "='" + p.MNC_MED_RM_NAM + "', " +
                "" + pm321.MNC_MED_RM_STS + "='" + p.MNC_MED_RM_STS + "', " +
                "" + pm321.MNC_MED_RMUSE_STS + "='" + p.MNC_MED_RMUSE_STS + "', " +
                "" + pm321.MNC_STAMP_DAT + "='" + p.MNC_STAMP_DAT + "', " +
                "" + pm321.MNC_STAMP_TIM + "='" + p.MNC_STAMP_TIM + "', " +
                "" + pm321.MNC_USR_ADD + "='" + p.MNC_USR_ADD + "', " +
                "" + pm321.MNC_USR_UPD + "='" + p.MNC_USR_UPD + "', " +
                "" + pm321.MNC_DIV_NO + "='" + p.MNC_DIV_NO + "' " +
                "Where " + pm321.MNC_DEP_NO + "='" + p.MNC_DEP_NO + "' " +
                "and " + pm321.MNC_SEC_NO + "='" + p.MNC_SEC_NO + "' " +
                "and " + pm321.MNC_MED_RM_CD + "='" + p.MNC_MED_RM_CD + "' ";
            return sql;
        }
        public String insertOrUpdate(PatientM321 p)
        {
            String sql = "";
            PatientM321 chk = selectByPk(p.MNC_DEP_NO, p.MNC_SEC_NO, p.MNC_MED_RM_CD);
            if (chk.MNC_DEP_NO == null)
            {
                sql = insert(p);
            }
            else
            {
                sql = update(p);
            }
            return sql;
        }
        private PatientM321 setPateintM321(DataTable dt)
        {
            PatientM321 p = new PatientM321();
            if (dt.Rows.Count > 0)
            {
                p.MNC_DEP_NO = dt.Rows[0][pm321.MNC_DEP_NO].ToString();
                p.MNC_SEC_NO = dt.Rows[0][pm321.MNC_SEC_NO].ToString();
                p.MNC_MED_RM_CD = dt.Rows[0][pm321.MNC_MED_RM_CD].ToString();
                p.MNC_MED_RM_NAM = dt.Rows[0][pm321.MNC_MED_RM_NAM].ToString();
                p.MNC_MED_RM_STS = dt.Rows[0][pm321.MNC_MED_RM_STS].ToString();
                p.MNC_MED_RMUSE_STS = dt.Rows[0][pm321.MNC_MED_RMUSE_STS].ToString();
                p.MNC_STAMP_DAT = dt.Rows[0][pm321.MNC_STAMP_DAT].ToString();
                p.MNC_STAMP_TIM = dt.Rows[0][pm321.MNC_STAMP_TIM].ToString();
                p.MNC_USR_ADD = dt.Rows[0][pm321.MNC_USR_ADD].ToString();
                p.MNC_USR_UPD = dt.Rows[0][pm321.MNC_USR_UPD].ToString();
                p.MNC_DIV_NO = dt.Rows[0][pm321.MNC_DIV_NO].ToString();
            }
            else
            {
                SetPatientM321(p);
            }

            return p;
        }
        private PatientM321 SetPatientM321(PatientM321 p)
        {
            p.MNC_DEP_NO = "";
            p.MNC_SEC_NO = "";
            p.MNC_MED_RM_CD = "";
            p.MNC_MED_RM_NAM = "";
            p.MNC_MED_RM_STS = "";
            p.MNC_MED_RMUSE_STS = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_ADD = "";
            p.MNC_USR_UPD = "";
            p.MNC_DIV_NO = "";
            return p;
        }
    }
}
