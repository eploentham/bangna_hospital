using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class ResOrderTabDB
    {
        public ResOrderTab reso;
        ConnectDB conn;
        public ResOrderTabDB(ConnectDB c)
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {
            reso = new ResOrderTab();
            //lDgs = new List<ResOrderTab>();

            reso.ResOrderKey = "ResOrderKey";
            reso.OrderClass = "OrderClass";
            reso.PatientID = "PatientID";
            reso.AccessNumber = "AccessNumber";
            reso.KPatientName = "KPatientName";
            reso.EPatientName = "EPatientName";
            reso.DateOfBirth = "DateOfBirth";
            reso.PatientSex = "PatientSex";
            reso.PatientClass = "PatientClass";
            reso.Modality = "Modality";
            reso.StudyDate = "StudyDate";
            reso.SicknessName = "SicknessName";
            reso.SicknessCode = "SicknessCode";
            reso.ProcDesc = "ProcDesc";
            reso.ResStatus = "ResStatus";
            reso.ResStatusPolling = "ResStatusPolling";
            reso.InsertDate = "InsertDate";
            reso.OrderDate = "OrderDate";
            reso.PhysicianID = "PhysicianID";
            reso.PhysicianName = "PhysicianName";
            reso.OrderDept = "OrderDept";
            reso.ModalityCode = "ModalityCode";
            reso.ExamCode = "ExamCode";
            reso.ExanDescription = "ExanDescription";
            reso.ReadingPriority = "ReadingPriority";
            reso.ReqPhysicianID = "ReqPhysicianID";
            reso.ReqPhysicianName = "ReqPhysicianName";
            reso.StationAE = "StationAE";

            reso.table = "ResOrderTab";
            reso.pkField = "ResOrderKey";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + reso.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                " Where dsc." + reso.ResStatus + " ='1' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        public ResOrderTab selectByPk(String id)
        {
            ResOrderTab cop1 = new ResOrderTab();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + reso.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + reso.pkField + " ='" + id + "' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);
            cop1 = setDocScan(dt);
            return cop1;
        }
        public DataTable selectByPk1(String id)
        {
            ResOrderTab cop1 = new ResOrderTab();
            DataTable dt = new DataTable();
            String sql = "select * " +
                "From " + reso.table + " dsc " +
                //"Left Join f_patient_prefix pfx On stf.prefix_id = pfx.f_patient_prefix_id " +
                "Where dsc." + reso.pkField + " ='" + id + "' " +
                "Order By doc_group_id ";
            dt = conn.selectData(conn.conn, sql);

            return dt;
        }
        private void chkNull(ResOrderTab p)
        {
            long chk = 0;

            p.OrderClass = p.OrderClass == null ? "" : p.OrderClass;
            p.PatientID = p.PatientID == null ? "" : p.PatientID;
            p.AccessNumber = p.AccessNumber == null ? "" : p.AccessNumber;
            p.KPatientName = p.KPatientName == null ? "" : p.KPatientName;
            p.EPatientName = p.EPatientName == null ? "" : p.EPatientName;

            p.DateOfBirth = p.DateOfBirth == null ? "" : p.DateOfBirth;
            p.PatientSex = p.PatientSex == null ? "" : p.PatientSex;
            p.PatientClass = p.PatientClass == null ? "" : p.PatientClass;
            p.Modality = p.Modality == null ? "" : p.Modality;
            p.StudyDate = p.StudyDate == null ? "" : p.StudyDate;
            p.SicknessName = p.SicknessName == null ? "" : p.SicknessName;
            p.SicknessCode = p.SicknessCode == null ? "" : p.SicknessCode;
            p.ProcDesc = p.ProcDesc == null ? "" : p.ProcDesc;
            p.ResStatus = p.ResStatus == null ? "" : p.ResStatus;
            p.ResStatusPolling = p.ResStatusPolling == null ? "" : p.ResStatusPolling;
            p.InsertDate = p.InsertDate == null ? "" : p.InsertDate;
            p.OrderDate = p.OrderDate == null ? "" : p.OrderDate;
            p.PhysicianID = p.PhysicianID == null ? "" : p.PhysicianID;
            p.PhysicianName = p.PhysicianName == null ? "" : p.PhysicianName;
            p.OrderDept = p.OrderDept == null ? "" : p.OrderDept;
            p.ModalityCode = p.ModalityCode == null ? "" : p.ModalityCode;
            p.ExamCode = p.ExamCode == null ? "" : p.ExamCode;
            p.ExanDescription = p.ExanDescription == null ? "" : p.ExanDescription;
            p.ReadingPriority = p.ReadingPriority == null ? "" : p.ReadingPriority;
            p.ReqPhysicianID = p.ReqPhysicianID == null ? "" : p.ReqPhysicianID;
            p.ReqPhysicianName = p.ReqPhysicianName == null ? "" : p.ReqPhysicianName;
            p.StationAE = p.StationAE == null ? "" : p.StationAE;
            //p.folder_ftp = p.folder_ftp == null ? "" : p.folder_ftp;
            //p.folder_ftp = p.folder_ftp == null ? "" : p.folder_ftp;

            //p.doc_group_id = long.TryParse(p.doc_group_id, out chk) ? chk.ToString() : "0";
            //p.row_no = long.TryParse(p.row_no, out chk) ? chk.ToString() : "0";
            //p.doc_group_sub_id = long.TryParse(p.doc_group_sub_id, out chk) ? chk.ToString() : "0";
            //p.pre_no = int.TryParse(p.pre_no, out chk) ? chk.ToString() : "0";
            //p.doctor_id = int.TryParse(p.doctor_id, out chk) ? chk.ToString() : "0";
        }
        public ResOrderTab setResOrderTab(String hn, String name, String vn, String preno, String hnyear, String reqno, String dob, String sex, String sickness, String xraydesc)
        {
            String date = "";
            date = System.DateTime.Now.Year + System.DateTime.Now.ToString("MMddHHmmss");
            ResOrderTab reso1 = new ResOrderTab();
            reso1.ResOrderKey = "";
            reso1.OrderClass = "NEW";
            reso1.PatientID = hn;
            reso1.AccessNumber = vn + "@" + preno + "@" + hnyear + "@" + reqno;
            reso1.KPatientName = name;
            reso1.EPatientName = name;
            reso1.DateOfBirth = dob.Replace("-","");
            reso1.PatientSex = sex;
            reso1.PatientClass = "CR";
            reso1.Modality = "CR";
            reso1.StudyDate = date;
            reso1.SicknessName = sickness;
            reso1.SicknessCode = "100";
            reso1.ProcDesc = xraydesc;      // ท่า
            reso1.ResStatus = "";
            reso1.ResStatusPolling = "";
            reso1.InsertDate = date;
            reso1.OrderDate = date;
            reso1.PhysicianID = "admin";
            reso1.PhysicianName = "administrator";
            reso1.OrderDept = "IT";
            reso1.ModalityCode = "";
            //reso1.ProcDesc = "E017";
            //reso1.ExanDescription = "NEck";
            reso1.ExamCode = "";
            reso1.ExanDescription = xraydesc;
            reso1.ReadingPriority = "0";
            reso1.ReqPhysicianID = "";
            reso1.ReqPhysicianName = "";
            reso1.StationAE = "";
            reso1.HisAddedInfo1 = vn + "@" + preno + "@" + hnyear + "@" + reqno;
            //reso1.HisAddedInfo1 = vn + "@" + preno + "@" + hnyear + "@" + reqno;

            return reso1;
        }
        public String insert(ResOrderTab p, String userId)
        {
            String re = "";
            String sql = "";
            DataTable dt = new DataTable();
            p.Modality = "CR";      //fix code
            p.OrderClass = "NEW";      //fix code
            p.ResStatus = "U";      //fix code
            //p.ssdata_id = "";
            long chk = 0;
            chkNull(p);
            //sql = "Insert Into " + reso.table + " (" + reso.doc_group_id + "," + reso.active + "," + reso.row_no + "," +
            //    reso.host_ftp + "," + reso.image_path + "," + reso.hn + "," +
            //    reso.vn + "," + reso.visit_date + "," + reso.remark + "," +
            //    reso.date_create + "," + reso.date_modi + "," + reso.date_cancel + "," +
            //    reso.user_create + "," + reso.user_modi + "," + reso.user_cancel + "," +
            //    reso.an + "," + reso.doc_group_sub_id + "," + reso.pre_no + "," +
            //    reso.an_date + "," + reso.status_ipd + "," + reso.an_cnt + "," +
            //    reso.folder_ftp + " " +
            //    ") " +
            //    "Values ('" + p.doc_group_id + "','1','" + p.row_no + "'," +
            //    "'" + p.host_ftp + "','" + p.image_path + "','" + p.hn + "'," +
            //    "'" + p.vn + "','" + p.visit_date + "','" + p.remark + "'," +
            //    "convert(varchar, getdate(), 23),'" + p.date_modi + "','" + p.date_cancel + "'," +
            //    "'" + userId + "','" + p.user_modi + "','" + p.user_cancel + "'," +
            //    "'" + p.an + "','" + p.doc_group_sub_id + "','" + p.pre_no + "'," +
            //    "'" + p.an_date + "','" + p.status_ipd + "','" + p.an_cnt + "' " +
            //    "'" + p.folder_ftp + "' " +
            //    ")";
            try
            {
                conn.comStore = new System.Data.SqlClient.SqlCommand();
                conn.comStore.Connection = conn.connPACs;
                conn.comStore.CommandText = "insert_resordertab";
                conn.comStore.CommandType = CommandType.StoredProcedure;
                conn.comStore.Parameters.AddWithValue("OrderClass", p.OrderClass);
                conn.comStore.Parameters.AddWithValue("PatientID", p.PatientID);
                conn.comStore.Parameters.AddWithValue("AccessNumber", p.AccessNumber);
                conn.comStore.Parameters.AddWithValue("KPatientName", p.KPatientName);
                conn.comStore.Parameters.AddWithValue("EPatientName", p.EPatientName);
                conn.comStore.Parameters.AddWithValue("DateOfBirth", p.DateOfBirth);
                conn.comStore.Parameters.AddWithValue("PatientSex", p.PatientSex);
                conn.comStore.Parameters.AddWithValue("PatientClass", p.PatientClass);
                conn.comStore.Parameters.AddWithValue("Modality", p.Modality);
                conn.comStore.Parameters.AddWithValue("StudyDate", p.StudyDate);
                conn.comStore.Parameters.AddWithValue("SicknessName", p.SicknessName);
                conn.comStore.Parameters.AddWithValue("SicknessCode", p.SicknessCode);
                conn.comStore.Parameters.AddWithValue("ProcDesc", p.ProcDesc);
                conn.comStore.Parameters.AddWithValue("ResStatus", p.ResStatus);
                conn.comStore.Parameters.AddWithValue("ResStatusPolling", p.ResStatusPolling);
                conn.comStore.Parameters.AddWithValue("InsertDate", p.InsertDate);
                conn.comStore.Parameters.AddWithValue("OrderDate", p.OrderDate);
                conn.comStore.Parameters.AddWithValue("PhysicianID", p.PhysicianID);
                conn.comStore.Parameters.AddWithValue("PhysicianName", p.PhysicianName);
                conn.comStore.Parameters.AddWithValue("OrderDept", p.OrderDept);
                conn.comStore.Parameters.AddWithValue("ModalityCode", p.ModalityCode);
                conn.comStore.Parameters.AddWithValue("ExamCode", p.ExamCode);
                conn.comStore.Parameters.AddWithValue("ExanDescription", p.ExanDescription);
                conn.comStore.Parameters.AddWithValue("ReadingPriority", p.ReadingPriority);
                conn.comStore.Parameters.AddWithValue("ReqPhysicianID", p.ReqPhysicianID);
                conn.comStore.Parameters.AddWithValue("ReqPhysicianName", p.ReqPhysicianName);
                conn.comStore.Parameters.AddWithValue("StationAE", p.StationAE);
                conn.comStore.Parameters.AddWithValue("HisAddedInfo1", p.HisAddedInfo1);
                //conn.comStore.Parameters.AddWithValue("folder_ftp", p.folder_ftp);
                SqlParameter retval = conn.comStore.Parameters.Add("row_no1", SqlDbType.VarChar, 50);
                retval.Value = "";
                retval.Direction = ParameterDirection.Output;

                conn.connPACs.Open();
                conn.comStore.ExecuteNonQuery();
                re = (String)conn.comStore.Parameters["row_no1"].Value;
                if(long.TryParse(re, out chk))
                {

                }
                //string retunvalue = (string)sqlcomm.Parameters["@b"].Value;
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                re = ex.Message;
            }
            finally
            {
                conn.connPACs.Close();
                conn.comStore.Dispose();
            }
            return re;
        }
        public String update(ResOrderTab p, String userId)
        {
            String re = "";
            String sql = "";
            int chk = 0;
            chkNull(p);
            //sql = "Update " + dsc.table + " Set " +
            //    " " + dsc.doc_group_id + " = '" + p.doc_group_id + "'" +
            //    "," + dsc.row_no + " = '" + p.row_no + "'" +
            //    "," + dsc.host_ftp + " = '" + p.host_ftp + "'" +
            //    "," + dsc.image_path + " = '" + p.image_path + "'" +
            //    "," + dsc.hn + " = '" + p.hn + "'" +
            //    "," + dsc.vn + " = '" + p.vn + "'" +
            //    "," + dsc.visit_date + " = '" + p.visit_date + "'" +
            //    "," + dsc.remark + " = '" + p.remark + "'" +
            //    "," + dsc.date_modi + " = convert(varchar, getdate(), 23)" +
            //    "," + dsc.user_modi + " = '" + userId + "'" +
            //    "," + dsc.an + " = '" + p.an + "'" +
            //    "," + dsc.doc_group_sub_id + " = '" + p.doc_group_sub_id + "'" +
            //    "," + dsc.pre_no + " = '" + p.pre_no + "'" +
            //    "," + dsc.an_date + " = '" + p.an_date + "'" +
            //    "," + dsc.status_ipd + " = '" + p.status_ipd + "'" +
            //    "," + dsc.an_cnt + " = '" + p.an_cnt + "'" +
            //    "," + dsc.folder_ftp + " = '" + p.folder_ftp + "'" +
            //    "Where " + dsc.pkField + "='" + p.doc_scan_id + "'"
            //    ;

            try
            {
                //re = conn.ExecuteNonQuery(conn.conn, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }

            return re;
        }
        public String insertResOrderTab(ResOrderTab p, String userId)
        {
            String re = "";

            if (p.ResOrderKey.Equals(""))
            {
                re = insert(p, "");
            }
            else
            {
                re = update(p, "");
            }

            return re;
        }
        public ResOrderTab setDocScan(DataTable dt)
        {
            ResOrderTab dgs1 = new ResOrderTab();
            if (dt.Rows.Count > 0)
            {
                dgs1.ResOrderKey = dt.Rows[0][reso.ResOrderKey].ToString();
                dgs1.OrderClass = dt.Rows[0][reso.OrderClass].ToString();
                dgs1.PatientID = dt.Rows[0][reso.PatientID].ToString();
                dgs1.AccessNumber = dt.Rows[0][reso.AccessNumber].ToString();
                dgs1.KPatientName = dt.Rows[0][reso.KPatientName].ToString();
                dgs1.EPatientName = dt.Rows[0][reso.EPatientName].ToString();
                dgs1.DateOfBirth = dt.Rows[0][reso.DateOfBirth].ToString();
                dgs1.PatientSex = dt.Rows[0][reso.PatientSex].ToString();
                dgs1.PatientClass = dt.Rows[0][reso.PatientClass].ToString();
                dgs1.Modality = dt.Rows[0][reso.Modality].ToString();
                dgs1.StudyDate = dt.Rows[0][reso.StudyDate].ToString();
                dgs1.SicknessName = dt.Rows[0][reso.SicknessName].ToString();
                dgs1.SicknessCode = dt.Rows[0][reso.SicknessCode].ToString();
                dgs1.ProcDesc = dt.Rows[0][reso.ProcDesc].ToString();
                dgs1.ResStatus = dt.Rows[0][reso.ResStatus].ToString();
                dgs1.ResStatusPolling = dt.Rows[0][reso.ResStatusPolling].ToString();
                dgs1.InsertDate = dt.Rows[0][reso.InsertDate].ToString();
                dgs1.OrderDate = dt.Rows[0][reso.OrderDate].ToString();
                dgs1.PhysicianID = dt.Rows[0][reso.PhysicianID].ToString();
                dgs1.PhysicianName = dt.Rows[0][reso.PhysicianName].ToString();
                dgs1.OrderDept = dt.Rows[0][reso.OrderDept].ToString();
                dgs1.ModalityCode = dt.Rows[0][reso.ModalityCode].ToString();
                dgs1.ExamCode = dt.Rows[0][reso.ExamCode].ToString();
                dgs1.ExanDescription = dt.Rows[0][reso.ExanDescription].ToString();
                dgs1.ReadingPriority = dt.Rows[0][reso.ReadingPriority].ToString();
                dgs1.ReqPhysicianID = dt.Rows[0][reso.ReqPhysicianID].ToString();
                dgs1.ReqPhysicianName = dt.Rows[0][reso.ReqPhysicianName].ToString();
                dgs1.StationAE = dt.Rows[0][reso.StationAE].ToString();
                //dgs1.folder_ftp = dt.Rows[0][reso.folder_ftp].ToString();
            }
            else
            {
                setDocGroupScan(dgs1);
            }
            return dgs1;
        }
        public ResOrderTab setDocGroupScan(ResOrderTab dgs1)
        {
            dgs1.ResOrderKey = "";
            dgs1.OrderClass = "";
            dgs1.PatientID = "";
            dgs1.AccessNumber = "";
            dgs1.KPatientName = "";
            dgs1.EPatientName = "";
            dgs1.DateOfBirth = "";
            dgs1.PatientSex = "";
            dgs1.PatientClass = "";
            dgs1.Modality = "";
            dgs1.StudyDate = "";
            dgs1.SicknessName = "";
            dgs1.SicknessCode = "";
            dgs1.ProcDesc = "";
            dgs1.ResStatus = "";
            dgs1.ResStatusPolling = "";
            dgs1.InsertDate = "";
            dgs1.OrderDate = "";
            dgs1.PhysicianID = "";
            dgs1.PhysicianName = "";
            dgs1.OrderDept = "";
            dgs1.ModalityCode = "";
            dgs1.ExamCode = "";
            dgs1.ExanDescription = "";
            dgs1.ReadingPriority = "";
            dgs1.ReqPhysicianID = "";
            dgs1.ReqPhysicianName = "";
            dgs1.StationAE = "";
            //dgs1.folder_ftp = "";
            return dgs1;
        }
    }
}
