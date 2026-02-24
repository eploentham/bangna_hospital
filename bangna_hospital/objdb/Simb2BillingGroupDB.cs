using System;
using System.Collections.Generic;
using System.Data;
using bangna_hospital.object1;

namespace bangna_hospital.objdb
{
    public class Simb2BillingGroupDB : BangnaHospitalDB
    {
        public Simb2BillingGroupDB(ConnectDB c) : base(c)
        {
        }

        public List<Simb2BillingGroup> GetList()
        {
            List<Simb2BillingGroup> list = new List<Simb2BillingGroup>();
            string sql = @"SELECT ID, CodeBG, BillingGroupTH, BillingGroupEN, 
                          CodeBSG, BillingSubGroupTH, BillingSubGroupEN, 
                          CreatedDate, active, status_master
                          FROM SIMB2_BillingGroup 
                          WHERE active = '1' 
                          ORDER BY CodeBG";

            DataTable dt = conn.selectData(conn.connMainHIS, sql);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(GetObjectFromDataRow(dr));
            }
            return list;
        }

        public List<Simb2BillingGroup> GetListByCodeBG(string codeBG)
        {
            List<Simb2BillingGroup> list = new List<Simb2BillingGroup>();
            string sql = string.Format(@"SELECT ID, CodeBG, BillingGroupTH, BillingGroupEN, 
                          CodeBSG, BillingSubGroupTH, BillingSubGroupEN, 
                          CreatedDate, active, status_master
                          FROM SIMB2_BillingGroup 
                          WHERE CodeBG = '{0}' AND active = '1'",
                          codeBG.Replace("'", "''"));

            DataTable dt = conn.selectData(conn.connMainHIS, sql);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(GetObjectFromDataRow(dr));
            }
            return list;
        }
        public Simb2BillingGroup GetByCodeBSG(string codeBSG)
        {
            string sql = string.Format(@"SELECT ID, CodeBG, BillingGroupTH, BillingGroupEN, 
                          CodeBSG, BillingSubGroupTH, BillingSubGroupEN, 
                          CreatedDate, active, status_master
                          FROM SIMB2_BillingGroup 
                          WHERE CodeBSG = '{0}'", codeBSG.Replace("'", "''"));

            DataTable dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                return GetObjectFromDataRow(dt.Rows[0]);
            }
            return null;
        }
        public Simb2BillingGroup GetByCodeBG(string codeBG)
        {
            string sql = string.Format(@"SELECT ID, CodeBG, BillingGroupTH, BillingGroupEN, 
                          CodeBSG, BillingSubGroupTH, BillingSubGroupEN, 
                          CreatedDate, active, status_master
                          FROM SIMB2_BillingGroup 
                          WHERE CodeBG = '{0}'", codeBG.Replace("'", "''"));

            DataTable dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                return GetObjectFromDataRow(dt.Rows[0]);
            }
            return null;
        }
        public Simb2BillingGroup GetByID(int id)
        {
            string sql = string.Format(@"SELECT ID, CodeBG, BillingGroupTH, BillingGroupEN, 
                          CodeBSG, BillingSubGroupTH, BillingSubGroupEN, 
                          CreatedDate, active, status_master
                          FROM SIMB2_BillingGroup 
                          WHERE ID = {0}", id);

            DataTable dt = conn.selectData(conn.connMainHIS, sql);
            if (dt.Rows.Count > 0)
            {
                return GetObjectFromDataRow(dt.Rows[0]);
            }
            return null;
        }
        public String InsertOrUpdate(Simb2BillingGroup obj)
        {
            Simb2BillingGroup existingObj = GetByID(obj.ID);
            if (obj.ID == 0)
            {
                return Insert(obj);
            }
            else
            {
                bool updated = Update(obj);
                return updated ? obj.ID.ToString() : "0";
            }
        }
        public String Insert(Simb2BillingGroup obj)
        {
            string createdDateStr = obj.CreatedDate.HasValue ? 
                "'" + obj.CreatedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" : "GETDATE()";

            string sql = string.Format(@"INSERT INTO SIMB2_BillingGroup 
                          (CodeBG, BillingGroupTH, BillingGroupEN, CodeBSG, 
                           BillingSubGroupTH, BillingSubGroupEN, CreatedDate, active) 
                          VALUES 
                          ('{0}', N'{1}', N'{2}', '{3}', N'{4}', N'{5}', {6}, '{7}');
                          SELECT CAST(SCOPE_IDENTITY() as int)",
                obj.CodeBG?.Replace("'", "''") ?? "",
                obj.BillingGroupTH?.Replace("'", "''") ?? "",
                obj.BillingGroupEN?.Replace("'", "''") ?? "",
                obj.CodeBSG?.Replace("'", "''") ?? "",
                obj.BillingSubGroupTH?.Replace("'", "''") ?? "",
                obj.BillingSubGroupEN?.Replace("'", "''") ?? "",
                createdDateStr,
                obj.Active ?? "1");

            string result = conn.ExecuteNonQueryLogPage(conn.connMainHIS, sql);
            return result;
        }

        public bool Update(Simb2BillingGroup obj)
        {
            string createdDateStr = obj.CreatedDate.HasValue ? 
                "'" + obj.CreatedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" : "GETDATE()";

            string sql = string.Format(@"UPDATE SIMB2_BillingGroup SET 
                          CodeBG = '{0}', 
                          BillingGroupTH = N'{1}', 
                          BillingGroupEN = N'{2}', 
                          CodeBSG = '{3}', 
                          BillingSubGroupTH = N'{4}', 
                          BillingSubGroupEN = N'{5}', 
                          CreatedDate = {6},                          
                          WHERE ID = {8}",
                obj.CodeBG?.Replace("'", "''") ?? "",
                obj.BillingGroupTH?.Replace("'", "''") ?? "",
                obj.BillingGroupEN?.Replace("'", "''") ?? "",
                obj.CodeBSG?.Replace("'", "''") ?? "",
                obj.BillingSubGroupTH?.Replace("'", "''") ?? "",
                obj.BillingSubGroupEN?.Replace("'", "''") ?? "",
                createdDateStr,                
                obj.ID);

            string result = conn.ExecuteNonQueryLogPage(conn.connMainHIS, sql);
            return result == "1";
        }

        public bool Delete(int id)
        {
            string sql = string.Format("UPDATE SIMB2_BillingGroup SET active = '3' WHERE ID = {0}", id);
            string result = conn.ExecuteNonQueryLogPage(conn.connMainHIS, sql);
            return result == "1";
        }

        private Simb2BillingGroup GetObjectFromDataRow(DataRow dr)
        {
            Simb2BillingGroup obj = new Simb2BillingGroup();
            obj.ID = Convert.ToInt32(dr["ID"]);
            obj.CodeBG = dr["CodeBG"].ToString();
            obj.BillingGroupTH = dr["BillingGroupTH"].ToString();
            obj.BillingGroupEN = dr["BillingGroupEN"].ToString();
            obj.CodeBSG = dr["CodeBSG"].ToString();
            obj.BillingSubGroupTH = dr["BillingSubGroupTH"].ToString();
            obj.BillingSubGroupEN = dr["BillingSubGroupEN"].ToString();
            obj.CreatedDate = dr["CreatedDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["CreatedDate"]) : null;
            obj.Active = dr["active"].ToString();
            obj.status_master = dr["status_master"].ToString();
            return obj;
        }
    }
}