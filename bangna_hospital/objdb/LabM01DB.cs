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
                "From lab_m01 labm01 " +
                "Left join lab_m02 labm02 on labm01.MNC_LB_CD = labm02.MNC_LB_CD " +
                "Left Join LAB_M06 labm06 on labm01.MNC_LB_GRP_CD = labm06.MNC_LB_GRP_CD " +
                "Left join LAB_M11 labm11 on labm01.MNC_SPC_CD = labm11.MNC_SPC_CD " +
                "Where labm01.MNC_LB_GRP_CD = '" + labgrpcode + "' " +
                "Order By  labm01.MNC_LB_CD ";
            dt = conn.selectData(sql);
            
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
            return p;
        }

    }
}
