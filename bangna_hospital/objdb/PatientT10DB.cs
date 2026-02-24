using System;
using System.Collections.Generic;
using System.Data;
using bangna_hospital.object1;

namespace bangna_hospital.objdb
{
    public class PatientT10DB : BangnaHospitalDB
    {
        public PatientT10DB(ConnectDB c) : base(c)
        {
        }

        public List<PatientT10> GetList()
        {
            List<PatientT10> list = new List<PatientT10>();
            string sql = @"SELECT TOP 100 * FROM PATIENT_T10 
                          ORDER BY MNC_RSV_DATE DESC, MNC_RSV_NO DESC";

            DataTable dt = conn.selectData(conn.connMainHIS, sql);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(GetObjectFromDataRow(dr));
            }
            return list;
        }

        public List<PatientT10> GetListByHN(string hnYear, string hnNo)
        {
            List<PatientT10> list = new List<PatientT10>();
            string sql = string.Format(@"SELECT * FROM PATIENT_T10 
                          WHERE MNC_HN_YR = '{0}' AND MNC_HN_NO = '{1}' 
                          ORDER BY MNC_RSV_DATE DESC, MNC_RSV_NO DESC",
                          hnYear.Replace("'", "''"),
                          hnNo.Replace("'", "''"));

            DataTable dt = conn.selectData(conn.connMainHIS, sql);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(GetObjectFromDataRow(dr));
            }
            return list;
        }

        public List<PatientT10> GetListByDate(DateTime startDate, DateTime endDate)
        {
            List<PatientT10> list = new List<PatientT10>();
            string sql = string.Format(@"SELECT * FROM PATIENT_T10 
                          WHERE MNC_RSV_DATE BETWEEN '{0}' AND '{1}' 
                          ORDER BY MNC_RSV_DATE DESC, MNC_RSV_TIME DESC",
                          startDate.ToString("yyyy-MM-dd"),
                          endDate.ToString("yyyy-MM-dd"));

            DataTable dt = conn.selectData(conn.connMainHIS, sql);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(GetObjectFromDataRow(dr));
            }
            return list;
        }

        public List<PatientT10> GetListByStatus(string status)
        {
            List<PatientT10> list = new List<PatientT10>();
            string sql = string.Format(@"SELECT * FROM PATIENT_T10 
                          WHERE MNC_RSV_STS = '{0}' 
                          ORDER BY MNC_RSV_DATE DESC",
                          status.Replace("'", "''"));

            DataTable dt = conn.selectData(conn.connMainHIS, sql);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(GetObjectFromDataRow(dr));
            }
            return list;
        }

        public PatientT10 GetByKey(string hnYear, string hnNo, int rsvNo, DateTime rsvDate)
        {
            string sql = string.Format(@"SELECT * FROM PATIENT_T10 
                          WHERE MNC_HN_YR = '{0}' 
                          AND MNC_HN_NO = '{1}' 
                          AND MNC_RSV_NO = {2} 
                          AND MNC_RSV_DATE = '{3}'",
                          hnYear.Replace("'", "''"),
                          hnNo.Replace("'", "''"),
                          rsvNo,
                          rsvDate.ToString("yyyy-MM-dd"));

            DataTable dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                return GetObjectFromDataRow(dt.Rows[0]);
            }
            return null;
        }

        public string Insert(PatientT10 obj)
        {
            string sql = string.Format(@"INSERT INTO PATIENT_T10 
                (MNC_HN_YR, MNC_HN_NO, MNC_RSV_NO, MNC_RSV_DATE, MNC_RSV_TIME, 
                 MNC_REQ_STS, MNC_HN_NAM, MNC_AD_DT_AMT, MNC_PRE_NO, 
                 MNC_ARV_DATE, MNC_ARV_TIME, MNC_DEP_NO, MNC_RSV_STS, MNC_AD_STS,
                 MNC_WD_NO_O, MNC_RM_NAM_O, MNC_BD_NO_O, 
                 MNC_WD_NO, MNC_RM_NAM, MNC_BD_NO,
                 MNC_DIA_CD, MNC_DIA_MEMO, MNC_DOT_CD, MNC_EMP_CD,
                 MNC_DATE, MNC_TIME, MNC_FN_TYP_CD, MNC_EMPR_CD, MNC_EMPC_CD,
                 MNC_STAMP_DAT, MNC_STAMP_TIM, MNC_WAD_CD) 
                VALUES 
                ('{0}', '{1}', {2}, '{3}', {4}, 
                 '{5}', N'{6}', {7}, {8}, 
                 {9}, {10}, '{11}', '{12}', '{13}',
                 {14}, '{15}', {16}, 
                 {17}, '{18}', {19},
                 '{20}', N'{21}', '{22}', '{23}',
                 {24}, {25}, '{26}', '{27}', '{28}',
                 {29}, {30}, '{31}')",
                obj.MNC_HN_YR?.Replace("'", "''") ?? "",
                obj.MNC_HN_NO?.Replace("'", "''") ?? "",
                obj.MNC_RSV_NO,
                obj.MNC_RSV_DATE.ToString("yyyy-MM-dd"),
                obj.MNC_RSV_TIME?.ToString() ?? "NULL",
                obj.MNC_REQ_STS?.Replace("'", "''") ?? "0",
                obj.MNC_HN_NAM?.Replace("'", "''") ?? "",
                obj.MNC_AD_DT_AMT?.ToString() ?? "NULL",
                obj.MNC_PRE_NO?.ToString() ?? "NULL",
                obj.MNC_ARV_DATE.HasValue ? "'" + obj.MNC_ARV_DATE.Value.ToString("yyyy-MM-dd") + "'" : "NULL",
                obj.MNC_ARV_TIME?.ToString() ?? "NULL",
                obj.MNC_DEP_NO?.Replace("'", "''") ?? "",
                obj.MNC_RSV_STS?.Replace("'", "''") ?? "0",
                obj.MNC_AD_STS?.Replace("'", "''") ?? "0",
                obj.MNC_WD_NO_O?.ToString() ?? "NULL",
                obj.MNC_RM_NAM_O?.Replace("'", "''") ?? "",
                obj.MNC_BD_NO_O?.ToString() ?? "NULL",
                obj.MNC_WD_NO?.ToString() ?? "NULL",
                obj.MNC_RM_NAM?.Replace("'", "''") ?? "",
                obj.MNC_BD_NO?.ToString() ?? "NULL",
                obj.MNC_DIA_CD?.Replace("'", "''") ?? "",
                obj.MNC_DIA_MEMO?.Replace("'", "''") ?? "",
                obj.MNC_DOT_CD?.Replace("'", "''") ?? "",
                obj.MNC_EMP_CD?.Replace("'", "''") ?? "",
                obj.MNC_DATE.HasValue ? "'" + obj.MNC_DATE.Value.ToString("yyyy-MM-dd") + "'" : "NULL",
                obj.MNC_TIME?.ToString() ?? "NULL",
                obj.MNC_FN_TYP_CD?.Replace("'", "''") ?? "",
                obj.MNC_EMPR_CD?.Replace("'", "''") ?? "",
                obj.MNC_EMPC_CD?.Replace("'", "''") ?? "",
                obj.MNC_STAMP_DAT.HasValue ? "'" + obj.MNC_STAMP_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" : "GETDATE()",
                obj.MNC_STAMP_TIM?.ToString() ?? "NULL",
                obj.MNC_WAD_CD?.Replace("'", "''") ?? "");

            string result = conn.ExecuteNonQueryLogPage(conn.connMainHIS, sql);
            return result;
        }

        public bool Update(PatientT10 obj)
        {
            string sql = string.Format(@"UPDATE PATIENT_T10 SET 
                MNC_RSV_TIME = {0}, MNC_REQ_STS = '{1}', MNC_HN_NAM = N'{2}', 
                MNC_AD_DT_AMT = {3}, MNC_PRE_NO = {4}, 
                MNC_ARV_DATE = {5}, MNC_ARV_TIME = {6}, MNC_DEP_NO = '{7}', 
                MNC_RSV_STS = '{8}', MNC_AD_STS = '{9}',
                MNC_WD_NO_O = {10}, MNC_RM_NAM_O = '{11}', MNC_BD_NO_O = {12}, 
                MNC_WD_NO = {13}, MNC_RM_NAM = '{14}', MNC_BD_NO = {15},
                MNC_DIA_CD = '{16}', MNC_DIA_MEMO = N'{17}', MNC_DOT_CD = '{18}', 
                MNC_EMP_CD = '{19}', MNC_DATE = {20}, MNC_TIME = {21}, 
                MNC_FN_TYP_CD = '{22}', MNC_EMPR_CD = '{23}', MNC_EMPC_CD = '{24}',
                MNC_STAMP_DAT = {25}, MNC_STAMP_TIM = {26}, MNC_WAD_CD = '{27}'
                WHERE MNC_HN_YR = '{28}' AND MNC_HN_NO = '{29}' 
                AND MNC_RSV_NO = {30} AND MNC_RSV_DATE = '{31}'",
                obj.MNC_RSV_TIME?.ToString() ?? "NULL",
                obj.MNC_REQ_STS?.Replace("'", "''") ?? "0",
                obj.MNC_HN_NAM?.Replace("'", "''") ?? "",
                obj.MNC_AD_DT_AMT?.ToString() ?? "NULL",
                obj.MNC_PRE_NO?.ToString() ?? "NULL",
                obj.MNC_ARV_DATE.HasValue ? "'" + obj.MNC_ARV_DATE.Value.ToString("yyyy-MM-dd") + "'" : "NULL",
                obj.MNC_ARV_TIME?.ToString() ?? "NULL",
                obj.MNC_DEP_NO?.Replace("'", "''") ?? "",
                obj.MNC_RSV_STS?.Replace("'", "''") ?? "0",
                obj.MNC_AD_STS?.Replace("'", "''") ?? "0",
                obj.MNC_WD_NO_O?.ToString() ?? "NULL",
                obj.MNC_RM_NAM_O?.Replace("'", "''") ?? "",
                obj.MNC_BD_NO_O?.ToString() ?? "NULL",
                obj.MNC_WD_NO?.ToString() ?? "NULL",
                obj.MNC_RM_NAM?.Replace("'", "''") ?? "",
                obj.MNC_BD_NO?.ToString() ?? "NULL",
                obj.MNC_DIA_CD?.Replace("'", "''") ?? "",
                obj.MNC_DIA_MEMO?.Replace("'", "''") ?? "",
                obj.MNC_DOT_CD?.Replace("'", "''") ?? "",
                obj.MNC_EMP_CD?.Replace("'", "''") ?? "",
                obj.MNC_DATE.HasValue ? "'" + obj.MNC_DATE.Value.ToString("yyyy-MM-dd") + "'" : "NULL",
                obj.MNC_TIME?.ToString() ?? "NULL",
                obj.MNC_FN_TYP_CD?.Replace("'", "''") ?? "",
                obj.MNC_EMPR_CD?.Replace("'", "''") ?? "",
                obj.MNC_EMPC_CD?.Replace("'", "''") ?? "",
                obj.MNC_STAMP_DAT.HasValue ? "'" + obj.MNC_STAMP_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" : "GETDATE()",
                obj.MNC_STAMP_TIM?.ToString() ?? "NULL",
                obj.MNC_WAD_CD?.Replace("'", "''") ?? "",
                obj.MNC_HN_YR?.Replace("'", "''") ?? "",
                obj.MNC_HN_NO?.Replace("'", "''") ?? "",
                obj.MNC_RSV_NO,
                obj.MNC_RSV_DATE.ToString("yyyy-MM-dd"));

            string result = conn.ExecuteNonQueryLogPage(conn.connMainHIS, sql);
            return result == "1";
        }

        public bool Delete(string hnYear, string hnNo, int rsvNo, DateTime rsvDate)
        {
            string sql = string.Format(@"DELETE FROM PATIENT_T10 
                WHERE MNC_HN_YR = '{0}' AND MNC_HN_NO = '{1}' 
                AND MNC_RSV_NO = {2} AND MNC_RSV_DATE = '{3}'",
                hnYear.Replace("'", "''"),
                hnNo.Replace("'", "''"),
                rsvNo,
                rsvDate.ToString("yyyy-MM-dd"));

            string result = conn.ExecuteNonQueryLogPage(conn.connMainHIS, sql);
            return result == "1";
        }

        public bool UpdateStatus(string hnYear, string hnNo, int rsvNo, DateTime rsvDate, string status)
        {
            string sql = string.Format(@"UPDATE PATIENT_T10 SET MNC_RSV_STS = '{0}' 
                WHERE MNC_HN_YR = '{1}' AND MNC_HN_NO = '{2}' 
                AND MNC_RSV_NO = {3} AND MNC_RSV_DATE = '{4}'",
                status.Replace("'", "''"),
                hnYear.Replace("'", "''"),
                hnNo.Replace("'", "''"),
                rsvNo,
                rsvDate.ToString("yyyy-MM-dd"));

            string result = conn.ExecuteNonQueryLogPage(conn.connMainHIS, sql);
            return result == "1";
        }

        private PatientT10 GetObjectFromDataRow(DataRow dr)
        {
            PatientT10 obj = new PatientT10();
            obj.MNC_HN_YR = dr["MNC_HN_YR"].ToString();
            obj.MNC_HN_NO = dr["MNC_HN_NO"].ToString();
            obj.MNC_RSV_NO = Convert.ToInt32(dr["MNC_RSV_NO"]);
            obj.MNC_RSV_DATE = Convert.ToDateTime(dr["MNC_RSV_DATE"]);
            obj.MNC_RSV_TIME = dr["MNC_RSV_TIME"] != DBNull.Value ? (int?)Convert.ToInt32(dr["MNC_RSV_TIME"]) : null;
            obj.MNC_REQ_STS = dr["MNC_REQ_STS"].ToString();
            obj.MNC_HN_NAM = dr["MNC_HN_NAM"].ToString();
            obj.MNC_AD_DT_AMT = dr["MNC_AD_DT_AMT"] != DBNull.Value ? (int?)Convert.ToInt32(dr["MNC_AD_DT_AMT"]) : null;
            obj.MNC_PRE_NO = dr["MNC_PRE_NO"] != DBNull.Value ? (int?)Convert.ToInt32(dr["MNC_PRE_NO"]) : null;
            obj.MNC_ARV_DATE = dr["MNC_ARV_DATE"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["MNC_ARV_DATE"]) : null;
            obj.MNC_ARV_TIME = dr["MNC_ARV_TIME"] != DBNull.Value ? (int?)Convert.ToInt32(dr["MNC_ARV_TIME"]) : null;
            obj.MNC_DEP_NO = dr["MNC_DEP_NO"].ToString();
            obj.MNC_RSV_STS = dr["MNC_RSV_STS"].ToString();
            obj.MNC_AD_STS = dr["MNC_AD_STS"].ToString();
            obj.MNC_WD_NO_O = dr["MNC_WD_NO_O"] != DBNull.Value ? (int?)Convert.ToInt32(dr["MNC_WD_NO_O"]) : null;
            obj.MNC_RM_NAM_O = dr["MNC_RM_NAM_O"].ToString();
            obj.MNC_BD_NO_O = dr["MNC_BD_NO_O"] != DBNull.Value ? (int?)Convert.ToInt32(dr["MNC_BD_NO_O"]) : null;
            obj.MNC_WD_NO = dr["MNC_WD_NO"] != DBNull.Value ? (int?)Convert.ToInt32(dr["MNC_WD_NO"]) : null;
            obj.MNC_RM_NAM = dr["MNC_RM_NAM"].ToString();
            obj.MNC_BD_NO = dr["MNC_BD_NO"] != DBNull.Value ? (int?)Convert.ToInt32(dr["MNC_BD_NO"]) : null;
            obj.MNC_DIA_CD = dr["MNC_DIA_CD"].ToString();
            obj.MNC_DIA_MEMO = dr["MNC_DIA_MEMO"].ToString();
            obj.MNC_DOT_CD = dr["MNC_DOT_CD"].ToString();
            obj.MNC_EMP_CD = dr["MNC_EMP_CD"].ToString();
            obj.MNC_DATE = dr["MNC_DATE"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["MNC_DATE"]) : null;
            obj.MNC_TIME = dr["MNC_TIME"] != DBNull.Value ? (int?)Convert.ToInt32(dr["MNC_TIME"]) : null;
            obj.MNC_FN_TYP_CD = dr["MNC_FN_TYP_CD"].ToString();
            obj.MNC_EMPR_CD = dr["MNC_EMPR_CD"].ToString();
            obj.MNC_EMPC_CD = dr["MNC_EMPC_CD"].ToString();
            obj.MNC_STAMP_DAT = dr["MNC_STAMP_DAT"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["MNC_STAMP_DAT"]) : null;
            obj.MNC_STAMP_TIM = dr["MNC_STAMP_TIM"] != DBNull.Value ? (int?)Convert.ToInt32(dr["MNC_STAMP_TIM"]) : null;
            obj.MNC_WAD_CD = dr["MNC_WAD_CD"].ToString();
            return obj;
        }
    }
}
