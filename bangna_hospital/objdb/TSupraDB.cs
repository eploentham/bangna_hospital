
using bangna_hospital.object1;
using C1.Win.C1Input;
using System;
using System.Data;
using System.Text;

namespace bangna_hospital.objdb
{
    public class TSupraDB
    {
        public ConnectDB conn;
        public TSupra tsupra;
        private string table = "t_supra";

        public TSupraDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }

        private void initConfig()
        {
            tsupra = new TSupra();
            tsupra.SupraId = "supra_id";
            tsupra.SupraDoc = "supra_doc";
            tsupra.SupraDate = "supra_date";
            tsupra.InputDate = "input_date";
            tsupra.BranchCode = "branch_code";
            tsupra.HospId = "hosp_id";
            tsupra.Hn = "hn";
            tsupra.PatId = "pat_id";
            tsupra.PatName = "pat_name";
            tsupra.PatSurname = "pat_surname";
            tsupra.Birthday = "birthday";
            tsupra.PatAge = "pat_age";
            tsupra.DoctorName = "doctor_name";
            tsupra.PaidTypeName = "paid_type_name";
            tsupra.Active = "active";
            tsupra.Remark = "remark";
            tsupra.SupraTypeId = "supra_type_id";
            tsupra.ContactNameHosp = "contact_name_hosp";
            tsupra.ContactTeleHosp = "contact_tele_hosp";
            tsupra.ContactNamePat = "contact_name_pat";
            tsupra.ContactTelePat = "contact_tele_pat";
            tsupra.Forcast1 = "forcast";
            tsupra.Paid1 = "paid";
            tsupra.Discount1 = "discount";
            tsupra.PatSex = "pat_sex";
            tsupra.PatStaff = "pat_staff";
            tsupra.Reason = "reason";
            tsupra.ReasonRemark = "reason_remark";
            tsupra.StatusCar = "status_car";
            tsupra.Diseased1 = "diseased1";
            tsupra.Diseased2 = "diseased2";
            tsupra.Diseased3 = "diseased3";
            tsupra.Diseased4 = "diseased4";
            tsupra.Drg = "drg";
            tsupra.OnTop = "on_top";
            tsupra.StatusTravel = "status_travel";
            tsupra.DateCreate = "date_create";
            tsupra.DateModi = "date_modi";
            tsupra.DateCancel = "date_cancel";
            tsupra.UserCreate = "user_create";
            tsupra.UserModi = "user_modi";
            tsupra.UserCancel = "user_cancel";
            tsupra.YearId = "year_id";
            tsupra.MonthId = "month_id";
            tsupra.table = table;
            tsupra.pkField = tsupra.SupraId;

        }
        public DataTable selectTextImageById(String id)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select * From t_supra_image Where id = '"+ id + "'  ";
                dt = conn.selectDataMySQL(conn.connMySQL, sql);
            }
            catch (Exception ex)
            {
                // log if needed
            }
            return dt;
        }
        public DataTable selectImage()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select * From t_supra_image Where status_process != '1' Order By id ";
                dt = conn.selectDataMySQL(conn.connMySQL, sql);
            }
            catch (Exception ex)
            {
                // log if needed
            }
            return dt;
        }
        public DataTable selectImageByPk(String id)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select * From t_supra_image Where id = '"+id+"'  ";
                dt = conn.selectDataMySQL(conn.connMySQL, sql);
            }
            catch (Exception ex)
            {
                // log if needed
            }
            return dt;
        }
        public DataTable selectByHn(string hn)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select supra.*, hosp1.hosp_name_t as hosp_name1 , hosp2.hosp_name_t as hosp_name2 " +
                    "From t_supra supra " +
                    "Left join b_hospital hosp1 on supra.hosp_id = hosp1.hosp_id " +
                    "Left join b_hospital hosp2 on supra.hosp_id = hosp2.hosp_code  " +
                    "Where supra.hn = '" + hn.Replace("'", "''") + "' Order By supra.supra_date Desc";
                dt = conn.selectDataMySQL(conn.connMySQL, sql);
            }
            catch (Exception ex)
            {
                // log if needed
            }
            return dt;
        }
        public DataTable selectHospital()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select * From b_hospital Where active = '1'  Order By hosp_name_t ";
                dt = conn.selectDataMySQL(conn.connMySQL, sql);
            }
            catch (Exception ex)
            {
                // log if needed
            }
            return dt;
        }
        public DataTable selectHospitalByCode(String code)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select * From b_hospital Where hosp_code = '"+code+"'   ";
                dt = conn.selectDataMySQL(conn.connMySQL, sql);
            }
            catch (Exception ex)
            {
                // log if needed
            }
            return dt;
        }
        public DataTable selectBranch()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select * From b_branch Where active = '1'  Order By branch_name ";
                dt = conn.selectDataMySQL(conn.connMySQL, sql);
            }
            catch (Exception ex)
            {
                // log if needed
            }
            return dt;
        }
        public TSupra selectByPk(string supraId)
        {
            TSupra p = null;
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select * From " + table + " Where supra_id = '" + supraId.Replace("'", "''") + "'";
                dt = conn.selectData(sql);
                p = setTSupra(dt);
            }
            catch (Exception ex)
            {
                // log if needed

            }
            return p;
        }
        public String selectMaxSupraDoc()
        {
            String re = "0";
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select max(supra_doc)+1 From " + table + " Where year(supra_date) = year(now());";
                dt = conn.selectDataMySQL(conn.connMySQL, sql);
                if(dt.Rows.Count>0)
                {
                    re = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                // log if needed

            }
            return re;
        }
        public void setCboHospital(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            DataTable dt = selectHospital();
            int i = 0;
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (DataRow row in dt.Rows)
            {
                if(row["hosp_code"].ToString().Length<=0)                {                    continue;                }
                item = new ComboBoxItem();
                item.Value = row["hosp_code"].ToString();
                item.Text = row["hosp_name_t"].ToString();
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
        public void setCboBranch(C1ComboBox c, String selected)
        {
            ComboBoxItem item = new ComboBoxItem();
            DataTable dt = selectBranch();
            int i = 0;
            item = new ComboBoxItem();
            item.Value = "";
            item.Text = "";
            c.Items.Add(item);
            foreach (DataRow row in dt.Rows)
            {
                item = new ComboBoxItem();
                item.Value = row["branch_code"].ToString();
                item.Text = row["branch_name"].ToString();
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
        // New helper: insertOrUpdate - choose insert or update automatically
        public string insertOrUpdate(TSupra p)
        {
            if (p == null) return "";
            try
            {
                // If supra_id missing generate one (GUID)
                if (string.IsNullOrEmpty(p.SupraId))
                {
                    p.SupraId = Guid.NewGuid().ToString();
                    return insertData(p);
                }

                // If record exists -> update, otherwise insert
                TSupra existing = selectByPk(p.SupraId);
                if (existing == null)
                {
                    return insertData(p);
                }
                else
                {
                    return updateData(p);
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "TSupraDB.insertOrUpdate Exception supra_id=" + (p?.SupraId ?? "") + " message=" + ex.Message + " stack=" + ex.StackTrace);
                return ex.Message;
            }
        }
        public string insertData(TSupra p)
        {
            string re = "", doc="";
            if (p == null) return re;
            try
            {
                doc = selectMaxSupraDoc();
                p.SupraDoc = doc;
                var sb = new StringBuilder();
                sb.Append("Insert Into " + table + " (");
                sb.Append("supra_id,supra_doc,supra_date,input_date,branch_code,hosp_id,hn,pat_id,pat_name,pat_surname,birthday,pat_age,doctor_name,paid_type_name,active,remark,supra_type_id,contact_name_hosp,contact_tele_hosp,contact_name_pat,contact_tele_pat,forcast,paid,discount,pat_sex,pat_staff,reason,reason_remark,status_car,diseased1,diseased2,diseased3,diseased4,drg,on_top,status_travel,date_modi,date_cancel,user_create,user_modi,user_cancel,year_id,month_id, date_create, doctor_remark1, doctor_remark2, doctor_remark3");
                sb.Append(") Values (");
                sb.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}',{21},{22},{23},'{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}', now() ",
                    p.SupraId.Replace("'", "''"),
                    (p.SupraDoc ?? "").Replace("'", "''"),
                    (p.SupraDate ?? "").Replace("'", "''"),
                    (p.InputDate ?? "").Replace("'", "''"),
                    (p.BranchCode ?? "").Replace("'", "''"),
                    (p.HospId ?? "").Replace("'", "''"),
                    (p.Hn ?? "").Replace("'", "''"),
                    (p.PatId ?? "").Replace("'", "''"),
                    (p.PatName ?? "").Replace("'", "''"),
                    (p.PatSurname ?? "").Replace("'", "''"),
                    (p.Birthday ?? "").Replace("'", "''"),
                    (p.PatAge ?? "").Replace("'", "''"),
                    (p.DoctorName ?? "").Replace("'", "''"),
                    (p.PaidTypeName ?? "").Replace("'", "''"),
                    ("'1'"),
                    (p.Remark ?? "").Replace("'", "''"),
                    (p.SupraTypeId ?? "").Replace("'", "''"),
                    (p.ContactNameHosp ?? "").Replace("'", "''"),
                    (p.ContactTeleHosp ?? "").Replace("'", "''"),
                    (p.ContactNamePat ?? "").Replace("'", "''"),
                    (p.ContactTelePat ?? "").Replace("'", "''"),
                    p.Forcast.ToString(),
                    p.Paid.ToString(),
                    p.Discount.ToString(),
                    (p.PatSex ?? "").Replace("'", "''"),
                    (p.PatStaff ?? "").Replace("'", "''"),
                    (p.Reason ?? "").Replace("'", "''"),
                    (p.ReasonRemark ?? "").Replace("'", "''"),
                    (p.StatusCar ?? "").Replace("'", "''"),
                    (p.Diseased1 ?? "").Replace("'", "''"),
                    (p.Diseased2 ?? "").Replace("'", "''"),
                    (p.Diseased3 ?? "").Replace("'", "''"),
                    (p.Diseased4 ?? "").Replace("'", "''"),
                    (p.Drg ?? "").Replace("'", "''"),
                    (p.OnTop ?? "").Replace("'", "''"),
                    (p.StatusTravel ?? "").Replace("'", "''"),
                    (p.DateModi ?? "").Replace("'", "''"),
                    (p.DateCancel ?? "").Replace("'", "''"),
                    (p.UserCreate ?? "").Replace("'", "''"),
                    (p.UserModi ?? "").Replace("'", "''"),
                    (p.UserCancel ?? "").Replace("'", "''"),
                    (p.YearId ?? "").Replace("'", "''"),
                    (p.MonthId ?? "").Replace("'", "''"),
                    (p.doctor_remark1 ?? "").Replace("'", "''"),
                    (p.doctor_remark2 ?? "").Replace("'", "''"),
                    (p.doctor_remark3 ?? "").Replace("'", "''")
                );
                sb.Append(")");
                re = conn.ExecuteNonQuery(conn.connMySQL, sb.ToString());
                re = p.SupraDoc;
            }
            catch (Exception ex)
            {
                re = ex.Message;
            }
            return re;
        }
        public string updateData(TSupra p)
        {
            string re = "";
            if (p == null || string.IsNullOrEmpty(p.SupraId)) return re;
            try
            {
                var sb = new StringBuilder();
                sb.Append("Update " + table + " Set ");
                sb.AppendFormat("supra_doc = '{0}', supra_date = '{1}', input_date = '{2}', branch_code = '{3}', hosp_id = '{4}', hn = '{5}', pat_id = '{6}', pat_name = '{7}', pat_surname = '{8}', birthday = '{9}', pat_age = '{10}', doctor_name = '{11}', paid_type_name = '{12}', active = '{13}', remark = '{14}', supra_type_id = '{15}', contact_name_hosp = '{16}', contact_tele_hosp = '{17}', contact_name_pat = '{18}', contact_tele_pat = '{19}', forcast = {20}, paid = {21}, discount = {22}, pat_sex = '{23}', pat_staff = '{24}', reason = '{25}', reason_remark = '{26}', status_car = '{27}', diseased1 = '{28}', diseased2 = '{29}', diseased3 = '{30}', diseased4 = '{31}', drg = '{32}', on_top = '{33}', status_travel = '{34}', date_modi = '{35}', date_cancel = '{36}', user_modi = '{37}', user_cancel = '{38}', year_id = '{39}', month_id = '{40}'",
                    (p.SupraDoc ?? "").Replace("'", "''"),
                    (p.SupraDate ?? "").Replace("'", "''"),
                    (p.InputDate ?? "").Replace("'", "''"),
                    (p.BranchCode ?? "").Replace("'", "''"),
                    (p.HospId ?? "").Replace("'", "''"),
                    (p.Hn ?? "").Replace("'", "''"),
                    (p.PatId ?? "").Replace("'", "''"),
                    (p.PatName ?? "").Replace("'", "''"),
                    (p.PatSurname ?? "").Replace("'", "''"),
                    (p.Birthday ?? "").Replace("'", "''"),
                    (p.PatAge ?? "").Replace("'", "''"),
                    (p.DoctorName ?? "").Replace("'", "''"),
                    (p.PaidTypeName ?? "").Replace("'", "''"),
                    (p.Active ?? "").Replace("'", "''"),
                    (p.Remark ?? "").Replace("'", "''"),
                    (p.SupraTypeId ?? "").Replace("'", "''"),
                    (p.ContactNameHosp ?? "").Replace("'", "''"),
                    (p.ContactTeleHosp ?? "").Replace("'", "''"),
                    (p.ContactNamePat ?? "").Replace("'", "''"),
                    (p.ContactTelePat ?? "").Replace("'", "''"),
                    p.Forcast.ToString(),
                    p.Paid.ToString(),
                    p.Discount.ToString(),
                    (p.PatSex ?? "").Replace("'", "''"),
                    (p.PatStaff ?? "").Replace("'", "''"),
                    (p.Reason ?? "").Replace("'", "''"),
                    (p.ReasonRemark ?? "").Replace("'", "''"),
                    (p.StatusCar ?? "").Replace("'", "''"),
                    (p.Diseased1 ?? "").Replace("'", "''"),
                    (p.Diseased2 ?? "").Replace("'", "''"),
                    (p.Diseased3 ?? "").Replace("'", "''"),
                    (p.Diseased4 ?? "").Replace("'", "''"),
                    (p.Drg ?? "").Replace("'", "''"),
                    (p.OnTop ?? "").Replace("'", "''"),
                    (p.StatusTravel ?? "").Replace("'", "''"),
                    (p.DateModi ?? "").Replace("'", "''"),
                    (p.DateCancel ?? "").Replace("'", "''"),
                    (p.UserModi ?? "").Replace("'", "''"),
                    (p.UserCancel ?? "").Replace("'", "''"),
                    (p.YearId ?? "").Replace("'", "''"),
                    (p.MonthId ?? "").Replace("'", "''")
                );
                sb.AppendFormat(" Where supra_id = '{0}'", p.SupraId.Replace("'", "''"));
                re = conn.ExecuteNonQuery(conn.connMySQL, sb.ToString());
            }
            catch (Exception ex)
            {
                re = ex.Message;
            }
            return re;
        }
        public string deleteByPk(string supraId)
        {
            string re = "";
            try
            {
                string sql = "Delete From " + table + " Where supra_id = '" + supraId.Replace("'", "''") + "'";
                re = conn.ExecuteNonQuery(conn.connMySQL, sql);
            }
            catch (Exception ex)
            {
                re = ex.Message;
            }
            return re;
        }
        public string updateProcess(string id, String icd9, String icd10, String expense, String vsdate, String discdate, String hospcode, String hn, String userid)
        {
            string re = "";
            try
            {
                string sql = "update t_supra_image Set icd9_code = '"+ icd9 + "'" +
                    ", icd10_code = '"+ icd10.Replace("'", "''") + "'" +
                    ", expense = '"+ expense.Replace("'", "''") + "'" +
                    ", visit_date = '"+ vsdate.Replace("'", "''") + "'" +
                    ", discharge_date = '"+ discdate.Replace("'", "''") + "'" +
                    ", status_process = '1' " +
                    ", hosp_code = '"+ hospcode.Replace("'", "''") + "' " +
                    ", date_process = now() " +
                    ", user_process = '"+ userid.Replace("'", "''") + "' " +
                    ", hn = '" + hn.Replace("'", "''") + "' " +
                    "Where id = '" + id + "'";
                re = conn.ExecuteNonQuery(conn.connMySQL, sql);
            }
            catch (Exception ex)
            {
                re = ex.Message;
            }
            return re;
        }
        public TSupra setTSupra(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0) return null;
            return TSupra.FromDataRow(dt.Rows[0]);
        }
    }
}