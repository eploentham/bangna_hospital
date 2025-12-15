using bangna_hospital.object1;
using C1.Win.C1Input;
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
        public DataTable selectByTodayOutLab(String reqDate)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select convert(varchar(20), labt01.MNC_REQ_DAT,23) as MNC_REQ_DAT,labt01.MNC_REQ_TIM, labt01.MNC_REQ_NO, labt01.status_lis " +
                ", labt01.mnc_req_yr,labt02.MNC_LB_CD,labm01.MNC_LB_DSC,labt01.MNC_HN_NO,convert(varchar(20), pt07.MNC_APP_DAT, 23) as MNC_APP_DAT, pt07.MNC_APP_DSC " +
                ",  isnull(pm02.MNC_PFIX_DSC,'') +' '+isnull(pm01.MNC_FNAME_T,'')+' '+isnull(pm01.MNC_LNAME_T,'') as pttfullname " +
                ",pt07.MNC_DOT_CD, pm02dtr.MNC_PFIX_DSC + ' ' +pm26.MNC_DOT_FNAME+' '+pm26.MNC_DOT_LNAME as dtr_name " +
                "From PATIENT_T07 pt07 " +
                "inner join lab_t01 labt01 on labt01.MNC_HN_NO = pt07.MNC_HN_NO and labt01.MNC_DATE = pt07.MNC_DATE and labt01.MNC_PRE_NO = pt07.MNC_PRE_NO " +
                "inner join lab_t02 labt02 on labt01.MNC_REQ_YR = labt02.MNC_REQ_YR and labt01.MNC_REQ_NO = labt02.MNC_REQ_NO and labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                "inner join LAB_M01 labm01 on labt02.MNC_LB_CD = labm01.MNC_LB_CD  " +
                "inner join patient_m01 pm01 on labt01.mnc_hn_no = pm01.mnc_hn_no and labt01.mnc_hn_yr = pm01.mnc_hn_yr " +
                "Left Join patient_m02 pm02 On pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "left join patient_m26 pm26 on pt07.MNC_DOT_CD = pm26.MNC_DOT_CD " +
                "left join patient_m02 pm02dtr on pm26.MNC_DOT_PFIX = pm02dtr.MNC_PFIX_CD " +
                "Where pt07.MNC_APP_DAT='" + reqDate + "' and labt02.mnc_req_sts <> 'C' and labt01.mnc_req_sts <> 'C' and labm01.status_outlab = '1' " +
                "Order By labt02." + labT02.MNC_REQ_DAT + ",labt02." + labT02.MNC_REQ_NO;

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
        public DataTable selectLabNamebyHN(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "select  labm01.MNC_LB_DSC, labt02.MNC_LB_CD " +
                "From lab_t01 labt01 " +
                "inner join lab_t02 labt02 on labt01.MNC_REQ_YR = labt02.MNC_REQ_YR and labt01.MNC_REQ_NO = labt02.MNC_REQ_NO and labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT " +
                "inner join lab_m01 labm01 on labt02.MNC_LB_CD = labm01.MNC_LB_CD  " +
                //"inner join patient_t01 pt01 on pt01.mnc_hn_no = labt01.mnc_hn_no and pt01.mnc_hn_yr = labt01.mnc_hn_yr and pt01.MNC_PRE_NO = labt01.MNC_PRE_NO and pt01.MNC_DATE = labt01.MNC_DATE " +
                //"Left Join patient_m02 pm02 On pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD " +
                "Where labt01.MNC_HN_NO='" + hn + "' and labt02.mnc_req_sts <> 'C'  and labt01.mnc_req_sts <> 'C' " +
                "group by labm01.MNC_LB_DSC, labt02.MNC_LB_CD  " +
                "Order By labm01.MNC_LB_DSC ";

            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public DataTable selectLabSubNamebyHN(String hn, List<String> llcode, String datestart, String dateend)
        {
            DataTable dt = new DataTable();
            String sql = "", lccode="", wherelccode="";
            foreach(String txt in llcode)
            {
                if (!txt.Equals("00"))
                {
                    lccode += "'" + txt + "',";
                }
                //lccode += "'"+txt+"',";
            }
            lccode = lccode.Length>0 ? lccode.Substring(0,lccode.Length - 1) : "";
            if (lccode.Length>0)
            {
                wherelccode = " and LAB_T02.MNC_LB_CD in (" + lccode + ")";
            }
            sql = "SELECT  LAB_M01.MNC_LB_CD, LAB_M01.MNC_LB_DSC, isnull(labm24.MNC_RES_UNT, '') as MNC_RES_UNT " +
                ", isnull(labm24.MNC_LB_RES_MIN,'') as MNC_LB_RES_MIN, isnull(labm24.MNC_LB_RES_MAX,'') as MNC_LB_RES_MAX " +
                ", labm24.MNC_LB_RES_CD, labm24.MNC_LB_RES  " +
                "FROM     PATIENT_T01 t01 " +
                "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                //"left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                //"left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " +
                //"left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report " +
                //"left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " +
                //"left join lab_m06 on LAB_M01.MNC_LB_GRP_CD = lab_m06.MNC_LB_GRP_CD " +

                "inner join lab_m04 labm24  on LAB_T02.MNC_LB_CD = labm24.MNC_LB_CD and labm24.MNC_ORD_NO = '0'  " +
                //"inner join lab_m07 on lab_m01.MNC_LB_GRP_CD =lab_m07.MNC_LB_GRP_CD  AND lab_m01.MNC_LB_TYP_CD = lab_m07.MNC_LB_TYP_CD   " +

                //" inner join patient_m26 on LAB_T01.mnc_dot_cd = patient_m26.MNC_DOT_CD " +
                //" inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD " +
                 //"Left Join finance_m02 fn02 on LAB_T01.MNC_FN_TYP_CD = fn02.MNC_FN_TYP_CD " +

                "where LAB_T01.MNC_REQ_DAT >= '" + datestart + "' and LAB_T01.MNC_REQ_DAT <= '" + dateend + "'  " +

                "and LAB_T01.mnc_hn_no = '" + hn + "' " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " + wherelccode + " "+
                //" and (LAB_T05.MNC_RES_VALUE <> null or LAB_T05.MNC_RES_VALUE <> 'NULL') " +
                //"and LAB_T05.mnc_lb_res_cd = '02' " +
                //"'ch039', 'ch036', 'ch038', 'se005', 'se038', 'se047', 'ch006', 'ch007', 'ch008', 'ch009', 'se165')) " +
                //"and lab_t05.mnc_res <> '' and LAB_T05.MNC_LAB_PRN = '1' " +
                "Group By LAB_M01.MNC_LB_CD, LAB_M01.MNC_LB_DSC,labm24.MNC_RES_UNT,labm24.MNC_LB_RES_MIN,labm24.MNC_LB_RES_MAX, labm24.MNC_LB_RES_CD, labm24.MNC_LB_RES " +
                "Order By LAB_M01.MNC_LB_CD,labm24.MNC_LB_RES_CD ";

            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public DataTable selectLabSubNameDatebyHN(String hn, List<String> llcode, String datestart, String dateend)
        {
            DataTable dt = new DataTable();
            String sql = "", lccode = "", wherelccode = "";
            foreach (String txt in llcode)
            {
                if (!txt.Equals("00"))
                {
                    lccode += "'" + txt + "',";
                }
            }
            lccode = lccode.Length>0 ? lccode.Substring(0,lccode.Length - 1) : "";
            if (lccode.Length > 0)
            {
                wherelccode = " and LAB_T02.MNC_LB_CD in (" + lccode + ")";
                sql = "SELECT LAB_T02.MNC_REQ_NO, convert(varchar(20),LAB_T02.MNC_REQ_DAT,23) as MNC_REQ_DAT  " +
                    "FROM     PATIENT_T01 t01 " +
                    "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                    "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                    "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                    "inner join lab_m04 labm24  on LAB_T02.MNC_LB_CD = labm24.MNC_LB_CD and labm24.MNC_ORD_NO = '0'  " +
                    "where LAB_T01.MNC_REQ_DAT >= '" + datestart + "' and LAB_T01.MNC_REQ_DAT <= '" + dateend + "'  " +

                    "and LAB_T01.mnc_hn_no = '" + hn + "' " +
                    "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " + wherelccode + " " +
                    "Group By LAB_T02.MNC_REQ_DAT,LAB_T02.MNC_REQ_NO  " +
                    "Order By LAB_T02.MNC_REQ_DAT desc,LAB_T02.MNC_REQ_NO  desc ";

                dt = conn.selectData(conn.connMainHIS, sql);
            }
            
            return dt;
        }
        public DataTable selectLabResultbyHNReqNo(String hn, List<String> llcode, String date, String reqno)
        {
            DataTable dt = new DataTable();
            String sql = "", lccode = "", wherelccode = "";
            foreach (String txt in llcode)
            {
                if (!txt.Equals("00"))
                {
                    lccode += "'" + txt + "',";
                }
                //lccode += "'" + txt + "',";
            }
            lccode = lccode.Length >0? lccode.Substring(0, lccode.Length - 1):"";
            if (lccode.Length > 0)
            {
                wherelccode = " and LAB_T02.MNC_LB_CD in (" + lccode + ")";
                sql = "SELECT  LAB_M01.MNC_LB_CD, LAB_M01.MNC_LB_DSC, isnull(LAB_T05.MNC_RES_UNT, '') as MNC_RES_UNT " +
                    ", isnull(LAB_T05.MNC_RES_MIN,'') as MNC_RES_MIN, isnull(LAB_T05.MNC_RES_MAX,'') as MNC_RES_MAX " +
                    ", LAB_T05.MNC_LB_RES_CD, LAB_T05.MNC_RES,isnull(LAB_T05.MNC_RES_VALUE,'') as MNC_RES_VALUE ,LAB_T05.MNC_LB_RES,LAB_T01.MNC_REQ_NO,LAB_T05.MNC_STS " +
                    "FROM     PATIENT_T01 t01 " +
                    "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE and t01.mnc_hn_no = LAB_T01.mnc_hn_no " +
                    "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                    "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT and LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO and LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD " +
                    "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                    "where LAB_T01.MNC_REQ_DAT = '" + date + "' and LAB_T01.MNC_REQ_NO = '" + reqno + "'  " +
                    "and LAB_T01.mnc_hn_no = '" + hn + "' " +
                    "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' " + wherelccode + " " +
                    "and (LAB_T05.MNC_RES_VALUE <> null or LAB_T05.MNC_RES_VALUE <> 'NULL') " +
                    "   " +
                    "Order By LAB_M01.MNC_LB_CD,LAB_T05.MNC_LB_RES_CD ";

                dt = conn.selectData(conn.connMainHIS, sql);
            }
            return dt;
        }
        public DataTable selectbyHNReqNo(String hn,String reqdate, String reqno)
        {
            DataTable dt = new DataTable();
            String sql = "", lccode = "", wherelccode = "";
            sql = "SELECT  LAB_T02.MNC_LB_CD as order_code, LAB_M01.MNC_LB_DSC as order_name, convert(varchar(20),LAB_T02.MNC_REQ_DAT, 23) as req_date " +
                ", LAB_T02.MNC_REQ_NO as req_no, 'lab' as flag, '1' as qty " +
                "FROM    LAB_T01  " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "where LAB_T01.MNC_REQ_DAT = '" + reqdate + "' and LAB_T01.MNC_REQ_NO = '" + reqno + "'  " +
                "and LAB_T01.mnc_hn_no = '" + hn + "' " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C'  " +
                "Order By LAB_T02.MNC_LB_CD ";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public DataTable selectLabByHnLabcodeinYear(String hn, String labcode)
        {
            DataTable dt = new DataTable();
            String sql = "", lccode = "", wherelccode = "";
            sql = "SELECT  LAB_T02.MNC_LB_CD as order_code, LAB_M01.MNC_LB_DSC as order_name, convert(varchar(20),LAB_T02.MNC_REQ_DAT, 23) as req_date " +
                ", LAB_T02.MNC_REQ_NO as req_no, 'lab' as flag, '1' as qty " +
                "FROM    LAB_T01  " +
                "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT " +
                "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD " +
                "where LAB_T01.MNC_HN_NO = '" + hn + "' and LAB_T02.MNC_LB_CD = '" + labcode + "'  " +
                "and LAB_T02.mnc_req_sts <> 'C'  and LAB_T01.mnc_req_sts <> 'C' AND YEAR(LAB_T01.MNC_REQ_DAT) = year(getdate())  " +
                "Order By LAB_T02.MNC_REQ_DAT desc ";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public C1ComboBox setCboLabNamebyHN(C1ComboBox c, String hn)
        {
            ComboBoxItem item = new ComboBoxItem();
            String select = "";
            int row1 = 0;
            //DataTable dt = selectC1();
            //lDistrict.Clear();
            DataTable dtamp = selectLabNamebyHN(hn);
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Text = "";
            item1.Value = "00";
            c.Items.Clear();
            c.Items.Add(item1);
            //for (int i = 0; i < dt.Rows.Count; i++)
            int i = 0;
            foreach (DataRow drow in dtamp.Rows)
            {
                item = new ComboBoxItem();
                item.Value = drow["MNC_LB_CD"].ToString();
                item.Text = drow["MNC_LB_DSC"].ToString();
                c.Items.Add(item);
                i++;
            }
            c.SelectedText = select;
            c.SelectedIndex = row1;
            return c;
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
        public String updateStatusRequestLabUnsend(String reqyr, String reqno, String reqdate)
        {
            String sql = "", re = "";
            sql = "update lab_t02 " +
                "Set status_request_lab = '0' " +
                "Where mnc_req_yr = '" + reqyr + "' and mnc_req_no = '" + reqno + "' and MNC_REQ_DAT = '" + reqdate + "'  ";
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
