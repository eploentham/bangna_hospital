using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class XrayM01DB
    {
        public XrayM01 xrayM01;
        ConnectDB conn;
        DataTable DTXray;
        internal AutoCompleteStringCollection AUTOXray;
        public XrayM01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {

            xrayM01 = new XrayM01();
            xrayM01.MNC_XR_CD = "MNC_XR_CD";
            xrayM01.MNC_XR_CTL_CD = "MNC_XR_CTL_CD";
            xrayM01.MNC_XR_DSC = "MNC_XR_DSC";
            xrayM01.MNC_XR_TYP_CD = "MNC_XR_TYP_CD";
            xrayM01.MNC_XR_GRP_CD = "MNC_XR_GRP_CD";
            xrayM01.MNC_XR_DIS_STS = "MNC_XR_DIS_STS";
            xrayM01.MNC_XR_STS = "MNC_XR_STS";
            xrayM01.MNC_DEC_CD = "MNC_DEC_CD";
            xrayM01.MNC_DEC_NO = "MNC_DEC_NO";
            xrayM01.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            xrayM01.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            xrayM01.MNC_USR_UPD = "MNC_USR_UPD";
            xrayM01.MNC_USR_ADD = "MNC_USR_ADD";
            xrayM01.MNC_OLD_CD = "MNC_OLD_CD";
            xrayM01.MNC_SUP_STS = "MNC_SUP_STS";
            xrayM01.MNC_XR_PRI = "MNC_XR_PRI";
            xrayM01.MNC_XR_DSC_STS = "MNC_XR_DSC_STS";
            xrayM01.MNC_XR_AUTO = "MNC_XR_AUTO";
            xrayM01.pacs_infinitt_code = "pacs_infinitt_code";
            xrayM01.modality_code = "modality_code";
            xrayM01.ucep_code = "ucep_code";
        }
        public AutoCompleteStringCollection setAUTOXray()
        {
            //lDept = new List<Position>();
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            //labM01.Clear();
            if (DTXray == null || DTXray.Rows.Count <= 0)
            {
                DTXray = SelectAll(); AUTOXray = new AutoCompleteStringCollection();
                foreach (DataRow row in DTXray.Rows)
                {
                    //autoSymptom.Add(row["MNC_PH_TN"].ToString() + "#" + row["MNC_PH_CD"].ToString());
                    AUTOXray.Add(row["MNC_XR_DSC"].ToString() + "#" + row["MNC_XR_CD"].ToString());
                }
            }
            return autoSymptom;
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select * From xray_m01  ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public DataTable SelectAllByGroup(String labgrpcode)
        {
            DataTable dt = new DataTable();
            //String sql = "select xray_m01.*, xray_m02.mnc_xr_pri01 " +
            //    "From xray_m01  " +
            //    "Left join xray_m02 on xray_m01.mnc_xr_cd = xray_m02.mnc_xr_cd and  " +
            //    "Where xray_m01.MNC_XR_GRP_CD = '" + labgrpcode + "' " +
            //    " ";
            String sql = "select xray_m01.* " +
                "From xray_m01  " +
                //"Left join xray_m02 on xray_m01.mnc_xr_cd = xray_m02.mnc_xr_cd and  " +
                "Where xray_m01.MNC_XR_GRP_CD = '" + labgrpcode + "' " +
                " ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable SelectXrayMap()
        {
            DataTable dt = new DataTable();
            String sql = "select xray01.MNC_XR_CD, xray01.MNC_XR_DSC, xray01.MNC_XR_GRP_CD, isnull(fm01.MNC_FN_CD,'') as MNC_FN_CD, isnull(fm11.MNC_DEF_DSC,'') as MNC_DEF_DSC " +
                ",simb2.CodeBSG,simb2.BillingSubGroupTH,simb2.BillingSubGroupEN,fm01.MNC_SIMB_CD,fm01.MNC_CHARGE_CD,fm01.MNC_SUB_CHARGE_CD, xray02.MNC_CHARGE_NO  " +
                "From XRAY_M01 xray01 " +
                "left join XRAY_M02 xray02 on xray01.MNC_XR_CD = xray02.MNC_XR_CD  " +
                "left join FINANCE_M01 fm01 on xray02.MNC_FN_CD = fm01.MNC_FN_CD " +
                "Left join Finance_m11 fm11 on fm01.MNC_SIMB_CD = fm11.MNC_DEF_CD " +
                "Left Join SIMB2_BillingGroup simb2 on xray02.CodeBSG = simb2.CodeBSG  " +
                //"Where xray01.MNC_XR_TYP_FLG = '1' " +
                "Order By xray01.MNC_XR_CD,xray02.MNC_CHARGE_NO ";
            dt = conn.selectData(conn.connMainHIS, sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public AutoCompleteStringCollection getlLabAll()
        {
            //lDept = new List<Position>();
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            //labM01.Clear();
            DataTable dt = new DataTable();
            dt = SelectAll();
            //dtCus = dt;
            foreach (DataRow row in dt.Rows)
            {
                autoSymptom.Add(row["MNC_XR_DSC"].ToString() + "#" + row["MNC_XR_CD"].ToString());
                //PatientM13 cus1 = new PatientM13();
                //cus1.MNC_APP_CD = row["MNC_APP_CD"].ToString();
                //cus1.MNC_APP_DSC = row["MNC_APP_DSC"].ToString();

                //labM01.Add(cus1);
            }
            return autoSymptom;
        }
        public XrayM01 SelectByPk(String xraycode)
        {
            DataTable dt = new DataTable();
            XrayM01 xrayM01 = new XrayM01();
            String sql = "select xray_m01.*, xray_m02.mnc_xr_pri01 " +
                "From xray_m01  " +
                "Left join xray_m02 on xray_m01.mnc_xr_cd = xray_m02.mnc_xr_cd " +
                "Where xray_m01.mnc_xr_cd = '" + xraycode + "' " +
                " ";
            dt = conn.selectData(sql);
            xrayM01 = setXrayM01(dt);
            return xrayM01;
        }
        public DataTable SelectAllBySearch(String xraycode)
        {
            DataTable dt = new DataTable();
            String wheresearch = "";
            if (xraycode == "")
            {
                return dt;
            }
            wheresearch = " (xray_m01.mnc_xr_dsc like '" + xraycode + "%') ";
            if (xraycode != "")
            {
                wheresearch += " or (xray_m01.mnc_xr_cd like '" + xraycode + "%') ";
            }
            String sql = "select xray_m01.MNC_XR_CD as code,xray_m01.MNC_XR_DSC as name,xray_m01.MNC_XR_GRP_CD as grp_name, xray_m02.mnc_xr_pri01 " +
                "From xray_m01  " +
                "Left join xray_m02 on xray_m01.mnc_xr_cd = xray_m02.mnc_xr_cd " +
                "Where  "+ wheresearch + " " +
                " ";
            dt = conn.selectData(sql);
            //xrayM01 = setXrayM01(dt);
            return dt;
        }
        public String updateOPBKKCode(String xraycode, String opbkkcode)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update xray_m01 Set " +
                    "ucep_code ='" + opbkkcode + "' " +
                    "Where mnc_xr_cd ='" + xraycode + "' ";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " XrayM01DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
        public String UpdateCodeBSG(String drugcode, String codebsg)
        {
            String sql = "", chk = "";
            sql = "Update xray_m02 Set CodeBSG = '" + codebsg + "' " +
                "Where mnc_xr_cd ='" + drugcode + "'";
            try
            {
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "XrayM02DB UpdateCodeBSG Exception " + ex.Message);
            }
            return chk;
        }
        public String insertXrayAccessNumber(String hn, String reqdate, String reqno,String xraycode)
        {
            String sql = "", chk = "";
            try
            {
                //new LogWriter("d", " XrayM01DB insertXrayAccessNumber hn " + hn + " reqdate " + reqdate+ " reqno " + reqno + " xraycode " + xraycode);
                //using (SqlCommand cmd = new SqlCommand("INSERT INTO xray_accessnumber(hn,req_date, req_no, xray_code) output INSERTED.accessnumber_id VALUES(@hn,@reqdate,@req_no,@xray_code)", conn.connMainHIS))
                //{
                //    new LogWriter("d", " XrayM01DB insertXrayAccessNumber sql " + cmd.CommandText);
                //    cmd.Parameters.AddWithValue("@hn", hn);
                //    cmd.Parameters.AddWithValue("@reqdate", reqdate);
                //    cmd.Parameters.AddWithValue("@req_no", reqno);
                //    cmd.Parameters.AddWithValue("@xray_code", xraycode);
                //    conn.connMainHIS.Open();

                //    int modified = (int)cmd.ExecuteScalar();

                //    if (conn.connMainHIS.State == System.Data.ConnectionState.Open)
                //        conn.connMainHIS.Close();

                //    return modified.ToString();
                //}
                sql = "INSERT INTO xray_accessnumber(hn,req_date, req_no, xray_code) Values('"+hn+"','"+reqdate+"','"+reqno+"','"+xraycode+ "');Select Scope_Identity();";
                chk = conn.ExecuteScalarNonQuery(conn.connMainHIS, sql);
                //new LogWriter("d", " XrayM01DB insertXrayAccessNumber chk " + chk);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " XrayM01DB insertXrayAccessNumber error " + ex.InnerException +" "+ex.Message);
            }
            return chk;
        }
        public XrayM01 setXrayM01(DataTable dt)
        {
            XrayM01 xrayM01 = new XrayM01();
            if (dt.Rows.Count > 0)
            {

                xrayM01.MNC_XR_CD = dt.Rows[0]["MNC_XR_CD"].ToString();
                xrayM01.MNC_XR_CTL_CD = dt.Rows[0]["MNC_XR_CTL_CD"].ToString();
                xrayM01.MNC_XR_DSC = dt.Rows[0]["MNC_XR_DSC"].ToString();
                xrayM01.MNC_XR_TYP_CD = dt.Rows[0]["MNC_XR_TYP_CD"].ToString();
                xrayM01.MNC_XR_GRP_CD = dt.Rows[0]["MNC_XR_GRP_CD"].ToString();
                xrayM01.MNC_XR_DIS_STS = dt.Rows[0]["MNC_XR_DIS_STS"].ToString();
                xrayM01.MNC_XR_STS = dt.Rows[0]["MNC_XR_STS"].ToString();
                xrayM01.MNC_DEC_CD = dt.Rows[0]["MNC_DEC_CD"].ToString();
                xrayM01.MNC_DEC_NO = dt.Rows[0]["MNC_DEC_NO"].ToString();
                xrayM01.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                xrayM01.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                xrayM01.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                xrayM01.MNC_USR_ADD = dt.Rows[0]["MNC_USR_ADD"].ToString();
                xrayM01.MNC_OLD_CD = dt.Rows[0]["MNC_OLD_CD"].ToString();
                xrayM01.MNC_SUP_STS = dt.Rows[0]["MNC_SUP_STS"].ToString();
                xrayM01.MNC_XR_PRI = dt.Rows[0]["MNC_XR_PRI"].ToString();
                xrayM01.MNC_XR_DSC_STS = dt.Rows[0]["MNC_XR_DSC_STS"].ToString();
                xrayM01.MNC_XR_AUTO = dt.Rows[0]["MNC_XR_AUTO"].ToString();
                xrayM01.pacs_infinitt_code = dt.Rows[0]["pacs_infinitt_code"].ToString();
                xrayM01.modality_code = dt.Rows[0]["modality_code"].ToString();
                xrayM01.ucep_code = dt.Rows[0]["ucep_code"].ToString();
            }
            else
            {
                setXrayM01(xrayM01);
            }
            return xrayM01;
        }
        public XrayM01 setXrayM01(XrayM01 p)
        {
            p.MNC_XR_CD = "";
            p.MNC_XR_CTL_CD = "";
            p.MNC_XR_DSC = "";
            p.MNC_XR_TYP_CD = "";
            p.MNC_XR_GRP_CD = "";
            p.MNC_XR_DIS_STS = "";
            p.MNC_XR_STS = "";
            p.MNC_DEC_CD = "";
            p.MNC_DEC_NO = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_USR_UPD = "";
            p.MNC_USR_ADD = "";
            p.MNC_OLD_CD = "";
            p.MNC_SUP_STS = "";
            p.MNC_XR_PRI = "";
            p.MNC_XR_DSC_STS = "";
            p.MNC_XR_AUTO = "";
            p.pacs_infinitt_code = "";
            p.modality_code = "";
            p.ucep_code = "";
            return p;
        }
    }
}





