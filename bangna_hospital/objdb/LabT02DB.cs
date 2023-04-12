using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class LabT02DB
    {
        public LabT02 labT02;
        ConnectDB conn;
        public LabT02DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labT02 = new LabT02();
            labT02.MNC_REQ_YR = "MNC_REQ_YR";
            labT02.MNC_REQ_NO = "MNC_REQ_NO";
            labT02.MNC_REQ_DAT = "MNC_REQ_DAT";
            labT02.MNC_LB_CD = "MNC_LB_CD";
            labT02.MNC_REQ_STS = "MNC_REQ_STS";
            labT02.MNC_LB_RMK = "MNC_LB_RMK";
            labT02.MNC_LB_COS = "MNC_LB_COS";
            labT02.MNC_LB_PRI = "MNC_LB_PRI";
            labT02.MNC_LB_RFN = "MNC_LB_RFN";
            labT02.MNC_SPC_SEND_DAT = "MNC_SPC_SEND_DAT";
            labT02.MNC_SPC_SEND_TM = "MNC_SPC_SEND_TM";
            labT02.MNC_SPC_TYP = "MNC_SPC_TYP";
            labT02.MNC_RESULT_DAT = "MNC_RESULT_DAT";
            labT02.MNC_RESULT_TIM = "MNC_RESULT_TIM";
            labT02.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            labT02.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            labT02.MNC_USR_RESULT = "MNC_USR_RESULT";
            labT02.MNC_USR_RESULT_REPORT = "MNC_USR_RESULT_REPORT";
            labT02.MNC_USR_RESULT_APPROVE = "MNC_USR_RESULT_APPROVE";
            labT02.MNC_CANCEL_STS = "MNC_CANCEL_STS";
            labT02.MNC_USR_UPD = "MNC_USR_UPD";
            labT02.MNC_SND_OUT_STS = "MNC_SND_OUT_STS";
            labT02.MNC_LB_STS = "MNC_LB_STS";
            labT02.status_lis = "status_lis";
        }
        public DataTable selectLabReq(String hn, String reqDate, String reqNo, String lbcode, String lbsubcode)
        {
            DataTable dt = new DataTable();
            String sql = "Select isnull(labm04.MNC_LAB_PRN,'') as MNC_LAB_PRN, isnull(labm04.MNC_LB_RES_MIN,'') as MNC_LB_RES_MIN,isnull(labm04.MNC_LB_RES_MAX,'') as MNC_LB_RES_MAX,isnull(labm04.MNC_RES_UNT,'') as MNC_RES_UNT,isnuLL(labm04.MNC_LB_RES,'') as MNC_LB_RES " +
                "From lab_t02 labt02 " +
                "left join lab_m04 labm04 on labt02.mnc_lb_cd = labm04.mnc_lb_cd " +
                "Where labt02.mnc_req_dat = '" + reqDate+ "' and labt02.mnc_req_no = '" + reqNo+ "' and labt02.mnc_lb_cd = '"+ lbcode+ "' and labm04.MNC_LB_RES_CD = '" + lbsubcode + "'";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public DataTable selectItemNoSendByStatusLis(String reqDate, String reqNo)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select convert(varchar(20), labt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,labt01.MNC_REQ_TIM, labt01.MNC_REQ_NO, labt01.status_lis " +
                ", labt01.mnc_req_yr,labt02.MNC_LB_CD " +
                "From lab_t01 labt01 " +
                "inner join lab_t02 labt02 on labt01.MNC_REQ_YR = labt02.MNC_REQ_YR and labt01.MNC_REQ_NO = labt02.MNC_REQ_NO and labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                //"inner join patient_m01 pm01 on labt01.mnc_hn_no = pm01.mnc_hn_no and labt01.mnc_hn_yr = pm01.mnc_hn_yr " +
                //"inner join patient_t01 pt01 on pt01.mnc_hn_no = labt01.mnc_hn_no and pt01.mnc_hn_yr = labt01.mnc_hn_yr and pt01.MNC_PRE_NO = labt01.MNC_PRE_NO and pt01.MNC_DATE = labt01.MNC_DATE " +
                //"Left Join patient_m02 pm02 On pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Where labt02.MNC_REQ_DAT='" + reqDate + "' and labt02.MNC_REQ_NO='" + reqNo + "' " +
                "Order By labt02." + labT02.MNC_REQ_DAT + ",labt02." + labT02.MNC_REQ_NO;

            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        private void chkNull(LabT02 p)
        {
            long chk = 0;
            int chk1 = 0;
            decimal chk2 = 0;

        }
        public String insertLabT02(LabT02 p, String userId)
        {
            String sql = "", re = "";

            chkNull(p);
            sql = "Insert Into lab_t02 " +
                "(MNC_REQ_YR,MNC_REQ_NO,MNC_REQ_DAT,MNC_REQ_STS" +
                ",MNC_LB_CD,MNC_LB_RMK,MNC_LB_COS,MNC_LB_PRI" +
                ",MNC_LB_RFN, MNC_SPC_SEND_DAT, MNC_SPC_SEND_TM,MNC_STAMP_DAT" +
                ",MNC_STAMP_TIM,MNC_SPC_TYP,MNC_RESULT_DAT,MNC_RESULT_TIM" +
                ",MNC_USR_RESULT,MNC_USR_RESULT_REPORT,MNC_USR_RESULT_APPROVE,MNC_CANCEL_STS" +
                ",MNC_USR_UPD,MNC_SND_OUT_STS,MNC_LB_STS" +
                ")" +
                "Values ('" + p.MNC_REQ_YR + "','" + p.MNC_REQ_NO + "','" + p.MNC_REQ_DAT + "','" + p.MNC_REQ_STS + "'" +
                ",'" + p.MNC_LB_CD + "','" + p.MNC_LB_RMK + "','" + p.MNC_LB_COS + "','" + p.MNC_LB_PRI + "'" +
                ",'" + p.MNC_LB_RFN + "','" + p.MNC_SPC_SEND_DAT + "','" + p.MNC_SPC_SEND_TM + "',convert(varchar(10), GETDATE(),23)" +
                ",replace(left(convert(varchar(100),getdate(),108),5),':',''),'" + p.MNC_SPC_TYP + "','" + p.MNC_RESULT_DAT + "','" + p.MNC_RESULT_TIM + "'" +
                ",'" + p.MNC_USR_RESULT + "','" + p.MNC_USR_RESULT_REPORT + "','" + p.MNC_USR_RESULT_APPROVE + "','" + p.MNC_CANCEL_STS + "'" +
                ",'" + p.MNC_USR_UPD + "','" + p.MNC_SND_OUT_STS + "','" + p.MNC_LB_STS + "'" +
                ")";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //new LogWriter("d", "PharmacyT02 insertPharmacyT02 sql " + sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                new LogWriter("e", "LabT02 insertLabT02 " + ex.InnerException);
            }

            return re;
        }
        public String updateResultDateLinkLIS(String reqyr, String reqno, String reqdate, String labcode, String resultdate, String userresult, String userreport)
        {
            String sql = "", re = "";
            sql = "update lab_t02 " +
                //"Set MNC_USR_RESULT = '"+ userresult + "', MNC_RESULT_DAT = '" + resultdate + "' " +
                "Set MNC_REQ_STS = 'K' "       //tab รายการที่รออนุมัติผล
                + ",MNC_USR_RESULT = '"+ userresult+"' "
                + ",MNC_USR_RESULT_REPORT = '" + userreport + "' "
                + "Where mnc_req_yr = '" + reqyr + "' " +
                "and mnc_req_no = '" + reqno + "' and MNC_REQ_DAT = '" + reqdate + "' and MNC_LB_CD = '" + labcode + "' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                sql = "update lab_t01 " +
                    "Set MNC_REQ_STS = 'K' " +
                    "Where mnc_req_yr = '" + reqyr + "' " +
                        "and mnc_req_no = '" + reqno + "' and MNC_REQ_DAT = '" + reqdate + "'  ";
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            return re;
        }
        public String updateStatusLinkLIS(String reqyr, String reqno, String reqdate)
        {
            String sql = "", re = "";
            sql = "update lab_t02 " +
                "Set status_lis = '1' " +
                "Where mnc_req_yr = '" + reqyr + "' " +
                "and mnc_req_no = '" + reqno + "' and MNC_REQ_DAT = '" + reqdate + "'  ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            return re;
        }
        public String updateStatusLinkLIS(String reqyr, String reqno, String reqdate, String labcode)
        {
            String sql = "", re = "";
            sql = "update lab_t02 " +
                "Set status_lis = '1' " +
                "Where mnc_req_yr = '" + reqyr + "' " +
                "and mnc_req_no = '" + reqno + "' and MNC_REQ_DAT = '"+ reqdate + "' and MNC_LB_CD = '"+ labcode + "' ";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            return re;
        }
        public String deleteReqNo(String reqyr, String reqno)
        {
            String sql = "", re = "";
            sql = "Delete From lab_t02 Where mnc_doc_cd = 'ROS' and mnc_req_yr = '" + reqyr + "' and mnc_req_no = '" + reqno + "'";
            try
            {
                re = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
            }
            return re;
        }
        public LabT02 setLabT02(DataTable dt)
        {
            LabT02 labT02 = new LabT02();
            if (dt.Rows.Count > 0)
            {
                labT02.MNC_REQ_YR = dt.Rows[0]["MNC_REQ_YR"].ToString();
                labT02.MNC_REQ_NO = dt.Rows[0]["MNC_REQ_NO"].ToString();
                labT02.MNC_REQ_DAT = dt.Rows[0]["MNC_REQ_DAT"].ToString();
                labT02.MNC_LB_CD = dt.Rows[0]["MNC_LB_CD"].ToString();
                labT02.MNC_REQ_STS = dt.Rows[0]["MNC_REQ_STS"].ToString();
                labT02.MNC_LB_RMK = dt.Rows[0]["MNC_LB_RMK"].ToString();
                labT02.MNC_LB_COS = dt.Rows[0]["MNC_LB_COS"].ToString();
                labT02.MNC_LB_PRI = dt.Rows[0]["MNC_LB_PRI"].ToString();
                labT02.MNC_LB_RFN = dt.Rows[0]["MNC_LB_RFN"].ToString();
                labT02.MNC_SPC_SEND_DAT = dt.Rows[0]["MNC_SPC_SEND_DAT"].ToString();
                labT02.MNC_SPC_SEND_TM = dt.Rows[0]["MNC_SPC_SEND_TM"].ToString();
                labT02.MNC_SPC_TYP = dt.Rows[0]["MNC_SPC_TYP"].ToString();
                labT02.MNC_RESULT_DAT = dt.Rows[0]["MNC_RESULT_DAT"].ToString(); ;
                labT02.MNC_RESULT_TIM = dt.Rows[0]["MNC_RESULT_TIM"].ToString();
                labT02.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                labT02.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                labT02.MNC_USR_RESULT = dt.Rows[0]["MNC_USR_RESULT"].ToString();
                labT02.MNC_USR_RESULT_REPORT = dt.Rows[0]["MNC_USR_RESULT_REPORT"].ToString();
                labT02.MNC_USR_RESULT_APPROVE = dt.Rows[0]["MNC_USR_RESULT_APPROVE"].ToString();
                labT02.MNC_CANCEL_STS = dt.Rows[0]["MNC_CANCEL_STS"].ToString();
                labT02.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                labT02.MNC_SND_OUT_STS = dt.Rows[0]["MNC_SND_OUT_STS"].ToString();
                labT02.MNC_LB_STS = dt.Rows[0]["MNC_LB_STS"].ToString();
                labT02.status_lis = dt.Rows[0]["status_lis"].ToString();
            }
            else
            {
                setLabT02(labT02);
            }
            return labT02;
        }
        public LabT02 setLabT02(LabT02 p)
        {
            p.MNC_REQ_YR = "";
            p.MNC_REQ_NO = "";
            p.MNC_REQ_DAT = "";
            p.MNC_LB_CD = "";
            p.MNC_REQ_STS = "";
            p.MNC_LB_RMK = "";
            p.MNC_LB_COS = "";
            p.MNC_LB_PRI = "";
            p.MNC_SPC_SEND_DAT = "";
            p.MNC_SPC_SEND_TM = "";
            p.MNC_SPC_TYP = "";
            p.MNC_RESULT_DAT = "";
            p.MNC_RESULT_TIM = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_RESULT = "";
            p.MNC_USR_RESULT_REPORT = "";
            p.MNC_USR_RESULT_APPROVE = "";
            p.MNC_CANCEL_STS = "";
            p.MNC_USR_UPD = "";
            p.MNC_SND_OUT_STS = "";
            p.MNC_LB_STS = "";
            p.status_lis = "";
            return p;
        }
    }
}
