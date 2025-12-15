
using System;
using System.Data;

namespace bangna_hospital.object1
{
    public class TSupra:Persistent
    {
        public string SupraId { get; set; }
        public string SupraDoc { get; set; }
        public string SupraDate { get; set; }
        public string InputDate { get; set; }
        public string BranchCode { get; set; }
        public string HospId { get; set; }
        public string Hn { get; set; }
        public string PatId { get; set; }
        public string PatName { get; set; }
        public string PatSurname { get; set; }
        public string Birthday { get; set; }
        public string PatAge { get; set; }
        public string DoctorName { get; set; }
        public string PaidTypeName { get; set; }
        public string Active { get; set; }
        public string Remark { get; set; }
        public string SupraTypeId { get; set; }
        public string ContactNameHosp { get; set; }
        public string ContactTeleHosp { get; set; }
        public string ContactNamePat { get; set; }
        public string ContactTelePat { get; set; }
        public decimal Forcast { get; set; } = 0.00m;
        public decimal Paid { get; set; } = 0.00m;
        public decimal Discount { get; set; } = 0.00m;
        public string Forcast1 { get; set; }
        public string Paid1 { get; set; }
        public string Discount1 { get; set; }
        public string PatSex { get; set; }
        public string PatStaff { get; set; }
        public string Reason { get; set; }
        public string ReasonRemark { get; set; }
        public string StatusCar { get; set; }
        public string Diseased1 { get; set; }
        public string Diseased2 { get; set; }
        public string Diseased3 { get; set; }
        public string Diseased4 { get; set; }
        public string Drg { get; set; }
        public string OnTop { get; set; }
        public string StatusTravel { get; set; }
        public string DateCreate { get; set; }
        public string DateModi { get; set; }
        public string DateCancel { get; set; }
        public string UserCreate { get; set; }
        public string UserModi { get; set; }
        public string UserCancel { get; set; }
        public string YearId { get; set; }
        public string MonthId { get; set; }
        public string doctor_remark1 { get; set; }
        public string doctor_remark2 { get; set; }
        public string doctor_remark3 { get; set; }

        public TSupra() { }

        // Populate from a DataRow safely (null-tolerant)
        public static TSupra FromDataRow(DataRow row)
        {
            if (row == null) return null;
            var t = new TSupra();
            t.SupraId = row.Table.Columns.Contains("supra_id") ? row["supra_id"]?.ToString() : null;
            t.SupraDoc = row.Table.Columns.Contains("supra_doc") ? row["supra_doc"]?.ToString() : null;
            t.SupraDate = row.Table.Columns.Contains("supra_date") ? row["supra_date"]?.ToString() : null;
            t.InputDate = row.Table.Columns.Contains("input_date") ? row["input_date"]?.ToString() : null;
            t.BranchCode = row.Table.Columns.Contains("branch_code") ? row["branch_code"]?.ToString() : null;
            t.HospId = row.Table.Columns.Contains("hosp_id") ? row["hosp_id"]?.ToString() : null;
            t.Hn = row.Table.Columns.Contains("hn") ? row["hn"]?.ToString() : null;
            t.PatId = row.Table.Columns.Contains("pat_id") ? row["pat_id"]?.ToString() : null;
            t.PatName = row.Table.Columns.Contains("pat_name") ? row["pat_name"]?.ToString() : null;
            t.PatSurname = row.Table.Columns.Contains("pat_surname") ? row["pat_surname"]?.ToString() : null;
            t.Birthday = row.Table.Columns.Contains("birthday") ? row["birthday"]?.ToString() : null;
            t.PatAge = row.Table.Columns.Contains("pat_age") ? row["pat_age"]?.ToString() : null;
            t.DoctorName = row.Table.Columns.Contains("doctor_name") ? row["doctor_name"]?.ToString() : null;
            t.PaidTypeName = row.Table.Columns.Contains("paid_type_name") ? row["paid_type_name"]?.ToString() : null;
            t.Active = row.Table.Columns.Contains("active") ? row["active"]?.ToString() : null;
            t.Remark = row.Table.Columns.Contains("remark") ? row["remark"]?.ToString() : null;
            t.SupraTypeId = row.Table.Columns.Contains("supra_type_id") ? row["supra_type_id"]?.ToString() : null;
            t.ContactNameHosp = row.Table.Columns.Contains("contact_name_hosp") ? row["contact_name_hosp"]?.ToString() : null;
            t.ContactTeleHosp = row.Table.Columns.Contains("contact_tele_hosp") ? row["contact_tele_hosp"]?.ToString() : null;
            t.ContactNamePat = row.Table.Columns.Contains("contact_name_pat") ? row["contact_name_pat"]?.ToString() : null;
            t.ContactTelePat = row.Table.Columns.Contains("contact_tele_pat") ? row["contact_tele_pat"]?.ToString() : null;

            if (row.Table.Columns.Contains("forcast") && decimal.TryParse(row["forcast"]?.ToString(), out decimal forcast)) t.Forcast = forcast;
            if (row.Table.Columns.Contains("paid") && decimal.TryParse(row["paid"]?.ToString(), out decimal paid)) t.Paid = paid;
            if (row.Table.Columns.Contains("discount") && decimal.TryParse(row["discount"]?.ToString(), out decimal discount)) t.Discount = discount;

            t.PatSex = row.Table.Columns.Contains("pat_sex") ? row["pat_sex"]?.ToString() : null;
            t.PatStaff = row.Table.Columns.Contains("pat_staff") ? row["pat_staff"]?.ToString() : null;
            t.Reason = row.Table.Columns.Contains("reason") ? row["reason"]?.ToString() : null;
            t.ReasonRemark = row.Table.Columns.Contains("reason_remark") ? row["reason_remark"]?.ToString() : null;
            t.StatusCar = row.Table.Columns.Contains("status_car") ? row["status_car"]?.ToString() : null;
            t.Diseased1 = row.Table.Columns.Contains("diseased1") ? row["diseased1"]?.ToString() : null;
            t.Diseased2 = row.Table.Columns.Contains("diseased2") ? row["diseased2"]?.ToString() : null;
            t.Diseased3 = row.Table.Columns.Contains("diseased3") ? row["diseased3"]?.ToString() : null;
            t.Diseased4 = row.Table.Columns.Contains("diseased4") ? row["diseased4"]?.ToString() : null;
            t.Drg = row.Table.Columns.Contains("drg") ? row["drg"]?.ToString() : null;
            t.OnTop = row.Table.Columns.Contains("on_top") ? row["on_top"]?.ToString() : null;
            t.StatusTravel = row.Table.Columns.Contains("status_travel") ? row["status_travel"]?.ToString() : null;
            t.DateCreate = row.Table.Columns.Contains("date_create") ? row["date_create"]?.ToString() : null;
            t.DateModi = row.Table.Columns.Contains("date_modi") ? row["date_modi"]?.ToString() : null;
            t.DateCancel = row.Table.Columns.Contains("date_cancel") ? row["date_cancel"]?.ToString() : null;
            t.UserCreate = row.Table.Columns.Contains("user_create") ? row["user_create"]?.ToString() : null;
            t.UserModi = row.Table.Columns.Contains("user_modi") ? row["user_modi"]?.ToString() : null;
            t.UserCancel = row.Table.Columns.Contains("user_cancel") ? row["user_cancel"]?.ToString() : null;
            t.YearId = row.Table.Columns.Contains("year_id") ? row["year_id"]?.ToString() : null;
            t.MonthId = row.Table.Columns.Contains("month_id") ? row["month_id"]?.ToString() : null;
            t.doctor_remark1 = row.Table.Columns.Contains("doctor_remark1") ? row["doctor_remark1"]?.ToString() : null;
            t.doctor_remark2 = row.Table.Columns.Contains("doctor_remark2") ? row["doctor_remark2"]?.ToString() : null;
            t.doctor_remark3 = row.Table.Columns.Contains("doctor_remark3") ? row["doctor_remark3"]?.ToString() : null;
            return t;
        }
    }
}