using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class LabExDB
    {
        //Config1 cf;
        //public ConnectDB connBua;
        public ConnectDB conn;
        public LabEx labex;
        //private LogWriter lw;
        public LabExDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            //cf = new Config1();
            //lw = new LogWriter();
            //connBua = new ConnectDB("bangna");
            //conn = new ConnectDB("mainhis");
            labex = new LabEx();
            labex.Active = "labex_active";
            labex.Description = "description";
            labex.Hn = "hn";
            labex.Id = "labex_id";
            labex.LabDate = "lab_date";
            labex.LabExDate = "labex_date";
            labex.PatientName = "patient_name";
            labex.Remark = "remark";
            labex.RowNumber = "row_number";
            labex.VisitDate = "visit_date";
            labex.VisitTime = "visit_time";
            labex.Vn = "vn";
            labex.YearId = "year_id";
            labex.DoctorId = "doctor_id";
            labex.LabTime = "lab_time";
            labex.ReqNo = "req_no";
            labex.DoctorName = "doctor_name";

            labex.table = "labex";
            labex.pkField = "labex_id";
        }
        private LabEx setData(LabEx p, DataTable dt)
        {
            p.Active = dt.Rows[0][labex.Active].ToString();
            p.Description = dt.Rows[0][labex.Description].ToString();
            p.Hn = dt.Rows[0][labex.Hn].ToString();
            p.Id = dt.Rows[0][labex.Id].ToString();
            p.LabDate = dt.Rows[0][labex.LabDate].ToString();
            p.LabExDate = dt.Rows[0][labex.LabExDate].ToString();
            p.PatientName = dt.Rows[0][labex.PatientName].ToString();
            p.Remark = dt.Rows[0][labex.Remark].ToString();
            p.RowNumber = dt.Rows[0][labex.RowNumber].ToString();
            p.VisitDate = dt.Rows[0][labex.VisitDate].ToString();
            p.VisitTime = dt.Rows[0][labex.VisitTime].ToString();
            p.Vn = dt.Rows[0][labex.Vn].ToString();
            p.YearId = dt.Rows[0][labex.YearId].ToString();
            p.DoctorId = dt.Rows[0][labex.DoctorId].ToString();
            p.LabTime = dt.Rows[0][labex.LabTime].ToString();
            p.ReqNo = dt.Rows[0][labex.ReqNo].ToString();
            p.DoctorName = dt.Rows[0][labex.DoctorName].ToString();

            return p;
        }
        public LabEx selectByPk(String Id)
        {
            LabEx item = new LabEx();
            String sql = "";
            DataTable dt = new DataTable();
            
            sql = "Select * From " + labex.table + " Where " + labex.pkField + "='" + Id + "'";
            //lw.WriteLog("LabExDB.selectByPk  sql " + sql);
            //lw.WriteLog("LabExDB.selectByPk  connBua.hostNameBua " + connBua.hostNameBua + " connBua.databaseNameBua " + connBua.databaseNameBua);
            //MessageBox.Show("111111 connBua.databaseNameBua " + connBua.databaseNameBua, "1111111");
            //if (connBua.hostNameBua.Equals(""))
            //{
            //    lw.WriteLog("LabExDB.selectByPk  connBua.hostNameBua if (connBua.hostNameBua.Equals" );
            //    IniFile iniFile = new IniFile("reportbangna.ini");
            //    connBua.databaseNameBua = iniFile.Read("database_name_bua");
            //    connBua.hostNameBua = iniFile.Read("host_name_bua");
            //    connBua.userNameBua = iniFile.Read("user_name_bua");
            //    connBua.passwordBua = iniFile.Read("password_bua");
            //    //MessageBox.Show("connBua.databaseNameBua " + connBua.databaseNameBua, "");
            //    //MessageBox.Show("connBua.hostNameBua " + connBua.hostNameBua, "");
            //    //MessageBox.Show("connBua.hostNameMainHIS " + connBua.hostNameMainHIS, "");
            //    //MessageBox.Show("connBua.hostNameMainHIS " + , "");
            //}
            
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)
            {
                item = setData(item, dt);
            }
            //lw.WriteLog("LabExDB.selectByPk  End connBua.connMainHIS.State " + connBua.connMainHIS.State);
            return item;
        }
        public String selectMaxRowNumber(String yearId)
        {
            //Image1 item = new Image1();
            String sql = "";
            int cnt = 0;
            DataTable dt = new DataTable();
            sql = "Select Max(" + labex.RowNumber + ") as cnt From " + labex.table + " Where " + labex.YearId + "='" + yearId + "'";
            dt = conn.selectData(conn.conn,sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["cnt"] != null)
                {
                    if (dt.Rows[0]["cnt"].ToString().Equals(""))
                    {
                        cnt = 100000;
                    }
                    else
                    {
                        if (dt.Rows[0]["cnt"].ToString().Equals("0"))
                        {
                            cnt = 100000;
                        }
                        else
                        {
                            cnt = int.Parse(dt.Rows[0]["cnt"].ToString()) + 1;
                        }
                    }
                }
                else
                {
                    cnt = 100000;
                }
            }
            else
            {
                cnt = 100000;
            }
            return cnt.ToString();
        }
        private String insert(LabEx p)
        {
            String sql = "", chk = "";
            try
            {
                if (p.Id == "")
                {
                    p.Id = p.getGenID();
                }
                //p.monthId = "RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)";
                //p.yearId = "CAST(year(GETDATE()) AS NVARCHAR)";
                //p.dateCreate = " CAST(year(GETDATE()) AS NVARCHAR)+'-'+ RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)+'-'+ RIGHT('00' + CAST(day(GETDATE()) AS NVARCHAR), 2)"
                //    + "' '+ RIGHT('00' + CAST(hour(GETDATE()) AS NVARCHAR), 2)+':'+ RIGHT('00' + CAST(minute(GETDATE()) AS NVARCHAR), 2)+':'+ RIGHT('00' + CAST(second(GETDATE()) AS NVARCHAR), 2)";
                p.RowNumber = selectMaxRowNumber(p.YearId);
                p.Active = "1";
                sql = "Insert Into " + labex.table + "(" + labex.pkField + "," + labex.Active + "," + labex.Description + "," +
                    labex.Hn + "," + labex.LabDate + "," + labex.LabExDate + "," +
                    labex.PatientName + "," + labex.Remark + "," + labex.RowNumber + "," +
                    labex.VisitDate + "," + labex.VisitTime + "," + labex.Vn+","+
                    labex.YearId + "," + labex.DoctorId + "," + labex.LabTime + "," +
                    labex.ReqNo + "," + labex.DoctorName + ") " +
                    "Values('" + p.Id + "','" + p.Active + "','" + p.Description + "','" +
                    p.Hn + "','" + p.LabDate + "','" + p.LabExDate + "','" +
                    p.PatientName + "','" + p.Remark + "','" + p.RowNumber + "','" +
                    p.VisitDate + "','" + p.VisitTime + "','" + p.Vn+"','"+
                    p.YearId + "','" + p.DoctorId + "','" + p.LabTime + "','" +
                    p.ReqNo + "','" + p.DoctorName + "') ";
                chk = conn.ExecuteNonQuery(sql);
                //chk = p.RowNumber;
                chk = p.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "insert LabEx");
            }
            return chk;
        }
        public String update(LabEx p)
        {
            String sql = "", chk = "";
            try
            {
                //p.dateModi = " CAST(year(GETDATE()) AS NVARCHAR)+'-'+ RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)+'-'+ RIGHT('00' + CAST(day(GETDATE()) AS NVARCHAR), 2)";
                sql = "Update " + labex.table + " Set " + labex.Description + "='" + p.Description + "'," +
                    labex.Hn + "='" + p.Hn + "'," +
                    labex.LabDate + "='" + p.LabDate + "'," +
                    labex.LabExDate + "='" + p.LabExDate + "'," +
                    labex.PatientName + "='" + p.PatientName + "'," +
                    labex.Remark + "='" + p.Remark + "'," +
                    labex.RowNumber + "='" + p.RowNumber + "'," +
                    labex.VisitDate + "='" + p.VisitDate + "'," +
                    labex.VisitTime + "='" + p.VisitTime + "'," +
                    labex.Vn + "='" + p.Vn + "'," +
                    labex.YearId + "='" + p.YearId + "', " +
                    labex.DoctorId + "='" + p.DoctorId + "', " +
                    labex.LabTime + "='" + p.LabTime + "', " +
                    labex.ReqNo + "='" + p.ReqNo + "', " +
                    labex.DoctorName + "='" + p.DoctorName + "' " +

                    "Where " + labex.pkField + "='" + p.Id + "'";
                chk = conn.ExecuteNonQuery(sql);
                chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "update LabEx");
            }
            return chk;
        }
        public String insertLabEx(LabEx p)
        {
            //LabEx item = new LabEx();
            String chk = "";
            //item = selectByPk(p.Id);
            if (p.Id == "")
            {
                chk = insert(p);
            }
            else
            {
                chk = update(p);
            }
            return chk;
        }
        public String VoidLabEx(String Id)
        {
            String sql = "", chk = "";
            sql = "Update " + labex.table + " Set " + labex.Active + " = '3' " +
                "Where " + labex.pkField + "='" + Id + "'";
            try
            {
                chk = conn.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
            }

            return chk;
        }
        public String UpdateRowNumber(String Id, String rowNumber)
        {
            String sql = "", chk = "";
            sql = "Update " + labex.table + " Set " + labex.RowNumber + " = '" + rowNumber + "' " +
                "Where " + labex.pkField + "='" + Id + "'";
            try
            {
                chk = conn.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
            }

            return chk;
        }
        public DataTable selectVisitByHn(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   t01.MNC_HN_NO,m02.MNC_PFIX_DSC as prefix, " +
                "m01.MNC_FNAME_T,m01.MNC_LNAME_T,m01.MNC_AGE,t01.MNC_VN_NO,t01.MNC_VN_SEQ,t01.MNC_VN_SUM,t01.MNC_FN_TYP_CD " +
                "From patient_t01 t01 " +
                " inner join patient_m01 m01 on t01.MNC_HN_NO = m01.MNC_HN_NO " +
                " inner join patient_m02 m02 on m01.MNC_PFIX_CDT =m02.MNC_PFIX_CD " +
                " Where t01.MNC_HN_NO = '" + hn + "' " +
                "and t01.MNC_STS <> 'C' " +
                " Order by t01.mnc_dot_cd ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable selectByVn(String hn,String vn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   * " +
                "From " + labex.table + " " +
                " Where " + labex.Vn + " = '" + vn + "' " +
                "and " + labex.Active + " = '1' and " + labex.Hn + " = '" + hn + "' " +
                " Order by " + labex.RowNumber + " ";
            dt = conn.selectData(conn.connLabOut, sql);
            return dt;
        }
        public DataTable selectByHn(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   * " +
                "From " + labex.table + " " +
                " Where " + labex.Hn+ " = '" + hn + "' " +
                "and " + labex.Active+ " = '1' " +
                " Order by " + labex.RowNumber+ " ";
            dt = conn.selectData(conn.connLabOut,sql);
            return dt;
        }
        public DataTable selectByHn1(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select   * " +
                "From " + labex.table + " " +
                " Where " + labex.Hn + " like '" + hn + "%' " +
                "and " + labex.Active + " = '1' " +
                " Order by " + labex.RowNumber + " ";
            dt = conn.selectData(sql);
            return dt;
        }
    }
}
