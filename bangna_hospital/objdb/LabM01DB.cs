using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class LabM01DB
    {
        public LabM01 labM01;
        ConnectDB conn;
        DataTable DTlab;
        internal AutoCompleteStringCollection AUTOLab;
        public LabM01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            labM01 = new LabM01();
            labM01.MNC_LB_CD = "MNC_LB_CD";
            labM01.MNC_LB_DSC = "MNC_LB_DSC";
            labM01.MNC_LB_TYP_CD = "MNC_LB_TYP_CD";
            labM01.MNC_LB_GRP_CD = "MNC_LB_GRP_CD";
            labM01.MNC_LB_DIS_STS = "MNC_LB_DIS_STS";
            labM01.MNC_SCH_ACT = "MNC_SCH_ACT";
            labM01.MNC_STAMP_DAT = "MNC_DOC_CD";
            labM01.MNC_STAMP_TIM = "MNC_DOC_CD";
            labM01.MNC_SPC_CD = "MNC_DOC_CD";
            labM01.MNC_LB_STS = "MNC_DOC_CD";
            labM01.MNC_USR_ADD = "MNC_DOC_CD";
            labM01.MNC_USR_UPD = "MNC_DOC_CD";
            labM01.MNC_LB_CTL_CD = "MNC_DOC_CD";
            labM01.MNC_LB_OLD_CD = "MNC_DOC_CD";
            labM01.MNC_DEC_CD = "MNC_DOC_CD";
            labM01.MNC_DEC_NO = "MNC_DOC_CD";
            labM01.MNC_LB_PRI = "MNC_DOC_CD";
            labM01.mnc_res_flg = "MNC_DOC_CD";
            labM01.MNC_HL7_CODE = "MNC_DOC_CD";
            labM01.ucep_code = "ucep_code";
            labM01.price = "mnc_lb_pri01";
            labM01.status_control = "status_control";
            labM01.control_supervisor = "control_supervisor";
            labM01.control_year = "control_year";
        }
        public AutoCompleteStringCollection setAUTOLab()
        {
            //lDept = new List<Position>();
            AutoCompleteStringCollection autoSymptom = new AutoCompleteStringCollection();
            //labM01.Clear();
            if (DTlab == null || DTlab.Rows.Count <= 0)
            {
                DTlab = SelectAll(); AUTOLab = new AutoCompleteStringCollection();
                foreach (DataRow row in DTlab.Rows)
                {
                    //autoSymptom.Add(row["MNC_PH_TN"].ToString() + "#" + row["MNC_PH_CD"].ToString());
                    AUTOLab.Add(row["MNC_LB_DSC"].ToString() + "#" + row["MNC_LB_CD"].ToString());
                }
            }
            return autoSymptom;
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select * " +
                "From lab_m01  ";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
        public LabM01 SelectByName(String labcode)
        {
            DataTable dt = new DataTable();
            LabM01 labM01 = new LabM01();
            String sql = "select lab_m01.*, lab_m02.mnc_lb_pri01 " +
                "From lab_m01  " +
                "Left join lab_m02 on lab_m01.mnc_lb_cd = lab_m02.mnc_lb_cd " +
                "Where lab_m01.mnc_lb_dsc = '" + labcode + "' " +
                " ";
            dt = conn.selectData(sql);
            labM01 = setLabM01(dt);
            return labM01;
        }
        public DataTable SelectControlByPk(String labcode)
        {
            DataTable dt = new DataTable();
            LabM01 labM01 = new LabM01();
            String sql = "select lab_m01.status_control,lab_m01.control_year,lab_m01.control_supervisor,lab_m01.control_paid_code,lab_m01.control_remark  " +
                "From lab_m01  Where lab_m01.mnc_lb_cd = '" + labcode + "' " +
                " ";
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable SelectAllSearch()
        {
            DataTable dt = new DataTable();
            LabM01 labM01 = new LabM01();
            String sql = "select lab_m01.MNC_LB_CD,isnull(lab_m01.status_control,'') as status_control,lab_m01.MNC_LB_DSC,isnull(lab_m01.control_year,0) as control_year,isnull(lab_m01.control_supervisor,'') as control_supervisor,isnull(lab_m01.control_paid_code,'') as control_paid_code,isnull(lab_m01.control_remark,'') as control_remark  " +
                "From lab_m01  Where lab_m01.active = '1' " +
                " ";
            dt = conn.selectData(sql);
            return dt;
        }
        public LabM01 SelectByPk(String labcode)
        {
            DataTable dt = new DataTable();
            LabM01 labM01 = new LabM01();
            String sql = "select lab_m01.*, lab_m02.mnc_lb_pri01 " +
                "From lab_m01  " +
                "Left join lab_m02 on lab_m01.mnc_lb_cd = lab_m02.mnc_lb_cd " +
                "Where lab_m01.mnc_lb_cd = '" + labcode + "' " +
                " ";
            dt = conn.selectData(sql);
            labM01 = setLabM01(dt);
            return labM01;
        }
        public DataTable SelectAllBySearch(String labgrpcode)
        {
            DataTable dt = new DataTable();
            String wheresearch = "";
            if (labgrpcode == "")
            {
                return dt;
            }
            wheresearch = " (labm01.MNC_LB_DSC like '" + labgrpcode + "%') ";
            if (labgrpcode != "")
            {
                wheresearch += " or (labm01.MNC_LB_CD like '" + labgrpcode + "%') ";
            }
            String sql = "Select labm01.MNC_LB_CD as code,labm01.MNC_LB_DSC as name ,labm01.MNC_LB_TYP_CD,labm01.MNC_LB_GRP_CD as grp_name,labm01.MNC_LB_DIS_STS" +
                ", labm02.mnc_lb_pri01, labm02.mnc_lb_pri02, labm02.mnc_lb_pri03,labm06.MNC_LB_GRP_DSC,labm01.MNC_SPC_CD,labm11.MNC_SPC_DSC,labm01.MNC_SCH_ACT " +
                "From lab_m01 labm01 " +
                "Left join lab_m02 labm02 on labm01.MNC_LB_CD = labm02.MNC_LB_CD " +
                "Left Join LAB_M06 labm06 on labm01.MNC_LB_GRP_CD = labm06.MNC_LB_GRP_CD " +
                "Left join LAB_M11 labm11 on labm01.MNC_SPC_CD = labm11.MNC_SPC_CD " +
                "Where " + wheresearch + " " +
                "Order By  labm01.MNC_LB_CD ";
            dt = conn.selectData(sql);

            return dt;
        }
        public DataTable SelectAllByGroup(String labgrpcode)
        {
            DataTable dt = new DataTable();
            String sql = "Select labm01.MNC_LB_CD,labm01.MNC_LB_DSC,labm01.MNC_LB_TYP_CD,labm01.MNC_LB_GRP_CD,labm01.MNC_LB_DIS_STS" +
                ", labm02.mnc_lb_pri01, labm02.mnc_lb_pri02, labm02.mnc_lb_pri03,labm06.MNC_LB_GRP_DSC,labm01.MNC_SPC_CD,labm11.MNC_SPC_DSC,labm01.MNC_SCH_ACT " +
                ", isnull(labm01.control_year,'') as control_year,isnull(labm01.control_supervisor,'') as control_supervisor " +
                "From lab_m01 labm01 " +
                "Left join lab_m02 labm02 on labm01.MNC_LB_CD = labm02.MNC_LB_CD " +
                "Left Join LAB_M06 labm06 on labm01.MNC_LB_GRP_CD = labm06.MNC_LB_GRP_CD " +
                "Left join LAB_M11 labm11 on labm01.MNC_SPC_CD = labm11.MNC_SPC_CD " +
                "Where labm01.MNC_LB_GRP_CD = '" + labgrpcode + "' " +
                "Order By  labm01.MNC_LB_CD ";
            dt = conn.selectData(sql);
            
            return dt;
        }
        public DataTable SelectLabMap()
        {
            DataTable dt = new DataTable();
            String sql = "select lab01.MNC_LB_CD, lab01.MNC_LB_DSC, lab01.MNC_LB_DSC,isnull(lab02.MNC_FN_CD,'') as MNC_FN_CD, isnull(fm11.MNC_DEF_DSC,'') as MNC_DEF_DSC " +
                ",simb2.CodeBSG,simb2.BillingSubGroupTH,simb2.BillingSubGroupEN,fm01.MNC_SIMB_CD,fm01.MNC_CHARGE_CD,fm01.MNC_SUB_CHARGE_CD, lab02.MNC_CHARGE_NO  " +
                "From LAB_M01 lab01 " +
                "left join LAB_M02 lab02 on lab01.MNC_LB_CD = lab02.MNC_LB_CD  " +
                "left join FINANCE_M01 fm01 on lab02.MNC_FN_CD = fm01.MNC_FN_CD " +
                "Left join Finance_m11 fm11 on fm01.MNC_SIMB_CD = fm11.MNC_DEF_CD " +
                "Left Join SIMB2_BillingGroup simb2 on lab02.CodeBSG = simb2.CodeBSG  " +
                "Where lab01.active = '1' " +
                "Order By lab01.MNC_LB_CD,lab02.MNC_CHARGE_NO";
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
                autoSymptom.Add(row["MNC_LB_DSC"].ToString()+"#"+ row["MNC_LB_CD"].ToString());
                //PatientM13 cus1 = new PatientM13();
                //cus1.MNC_APP_CD = row["MNC_APP_CD"].ToString();
                //cus1.MNC_APP_DSC = row["MNC_APP_DSC"].ToString();

                //labM01.Add(cus1);
            }
            return autoSymptom;
        }
        public String UpdateCodeBSG(String drugcode, String codebsg)
        {
            String sql = "", chk = "";
            sql = "Update lab_m02 Set CodeBSG = '" + codebsg + "' " +
                "Where mnc_lb_cd ='" + drugcode + "'";
            try
            {
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "LabM01DB UpdateCodeBSG Exception " + ex.Message);
            }
            return chk;
        }
        public String updateControlLab(String labcode, String controlyear, String controlsupervisor)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update lab_m01 Set " +
                    "control_year = '" + controlyear + "' " +
                    ", control_supervisor = '" + controlsupervisor+"' " +
                    "Where mnc_lb_cd = '" + labcode + "'";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " LabM01DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
        public String updateOPBKKCode(String labcode, String opbkkcode)
        {
            String sql = "", chk = "";
            try
            {
                sql = "Update lab_m01 Set " +
                    "ucep_code = '" + opbkkcode + "' " +
                    "Where mnc_lb_cd = '" + labcode + "'";
                chk = conn.ExecuteNonQuery(conn.connMainHIS, sql);
                //chk = p.RowNumber;
            }
            catch (Exception ex)
            {
                new LogWriter("e", " LabM01DB updateOPBKKCode error " + ex.InnerException);
            }
            return chk;
        }
        public LabM01 setLabM01(DataTable dt)
        {
            LabM01 labM01 = new LabM01();
            if (dt.Rows.Count > 0)
            {
                labM01.MNC_LB_CD = dt.Rows[0]["MNC_LB_CD"].ToString();
                labM01.MNC_LB_DSC = dt.Rows[0]["MNC_LB_DSC"].ToString();
                labM01.MNC_LB_TYP_CD = dt.Rows[0]["MNC_LB_TYP_CD"].ToString();
                labM01.MNC_LB_GRP_CD = dt.Rows[0]["MNC_LB_GRP_CD"].ToString();
                labM01.MNC_LB_DIS_STS = dt.Rows[0]["MNC_LB_DIS_STS"].ToString();
                labM01.MNC_SCH_ACT = dt.Rows[0]["MNC_SCH_ACT"].ToString();
                labM01.MNC_STAMP_DAT = dt.Rows[0]["MNC_STAMP_DAT"].ToString();
                labM01.MNC_STAMP_TIM = dt.Rows[0]["MNC_STAMP_TIM"].ToString();
                labM01.MNC_SPC_CD = dt.Rows[0]["MNC_SPC_CD"].ToString();
                labM01.MNC_LB_STS = dt.Rows[0]["MNC_LB_STS"].ToString();
                labM01.MNC_USR_ADD = dt.Rows[0]["MNC_USR_ADD"].ToString();
                labM01.MNC_USR_UPD = dt.Rows[0]["MNC_USR_UPD"].ToString();
                labM01.MNC_LB_CTL_CD = dt.Rows[0]["MNC_LB_CTL_CD"].ToString();
                labM01.MNC_LB_OLD_CD = dt.Rows[0]["MNC_LB_OLD_CD"].ToString();
                labM01.MNC_DEC_CD = dt.Rows[0]["MNC_DEC_CD"].ToString();
                labM01.MNC_DEC_NO = dt.Rows[0]["MNC_DEC_NO"].ToString();
                labM01.MNC_LB_PRI = dt.Rows[0]["MNC_LB_PRI"].ToString();
                labM01.mnc_res_flg = dt.Rows[0]["mnc_res_flg"].ToString();
                labM01.MNC_HL7_CODE = dt.Rows[0]["mnc_res_flg"].ToString();
                labM01.ucep_code = dt.Rows[0]["ucep_code"].ToString();
                labM01.price = dt.Rows[0]["mnc_lb_pri01"].ToString();
                labM01.status_control = dt.Rows[0]["status_control"] != null ? dt.Rows[0]["status_control"].ToString():"";
                labM01.control_supervisor = dt.Rows[0]["control_supervisor"] != null ? dt.Rows[0]["control_supervisor"].ToString() : "";
                labM01.control_year = dt.Rows[0]["control_year"] != null ? dt.Rows[0]["control_year"].ToString() : "";
                labM01.control_paid_code = dt.Rows[0]["control_paid_code"] != null ? dt.Rows[0]["control_paid_code"].ToString() : "";
                labM01.control_remark = dt.Rows[0]["control_remark"] != null ? dt.Rows[0]["control_remark"].ToString() : "";
            }
            else
            {
                setLabM01(labM01);
            }
            return labM01;
        }

        public LabM01 setLabM01(LabM01 p)
        {
            p.MNC_LB_CD = "";
            p.MNC_LB_DSC = "";
            p.MNC_LB_TYP_CD = "";
            p.MNC_LB_GRP_CD = "";
            p.MNC_LB_DIS_STS = "";
            p.MNC_SCH_ACT = "";
            p.MNC_STAMP_DAT = "";
            p.MNC_STAMP_TIM = "";
            p.MNC_SPC_CD = "";
            p.MNC_LB_STS = "";
            p.MNC_USR_ADD = "";
            p.MNC_USR_UPD = "";
            p.MNC_LB_CTL_CD = "";
            p.MNC_LB_OLD_CD = "";
            p.MNC_DEC_CD = "";
            p.MNC_DEC_NO = "";
            p.MNC_LB_PRI = "";
            p.mnc_res_flg = "";
            p.MNC_HL7_CODE = "";
            p.ucep_code = "";
            p.price = "0";
            p.status_control = "";
            p.control_supervisor = "";
            p.control_year = "";
            p.control_paid_code = "";
            p.control_remark = "";
            return p;
        }

    }
}
