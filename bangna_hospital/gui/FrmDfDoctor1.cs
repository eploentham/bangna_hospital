using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C1.Win.C1Preview.Strings;
using C1.Win.C1SplitContainer;
using GrapeCity.Viewer.Common.Model;

namespace bangna_hospital.gui
{
    public partial class FrmDfDoctor1 : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B;

        C1FlexGrid grfImp, grfRpt;

        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        //C1PdfDocument pdfDoc;
        C1ThemeController theme1;
        Label lbLoading;
        DataTable DTRPT;
        String RPTNAME = "";
        Boolean pageLoad = false;
        int colImpchk = 1, colImpvsdateShow = 2, colImphnno = 3, colImppttname = 4, colImppreno = 5, colImppaidtype = 6, colImpdtrcode = 7, colImpdtrname = 8, colImpdoccd = 9, colImpdoc_yr = 10, colImpdocno = 11, colImpfncd = 12, colImpno = 13, colImpfnamt = 14, colImpfndesc = 15, colImpdocdat = 16, colImpvstime = 17, colImpanno = 18, colImpanyr = 19, colImphnyr = 20, colImpvsdate=21, colSymptoms=22;
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmDfDoctor1(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);
            theme1 = new C1.Win.C1Themes.C1ThemeController();
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);
            initGrfImp();
            initGrfRpt();
            bc.bcDB.pttDB.setCboDeptOPDNew(cboRpt2, "");

            btnImportDfSelect.Click += BtnImportDfSelect_Click;
            btnImportDfGen.Click += BtnImportDfGen_Click;
            btnRpt1.Click += BtnRpt1_Click;
            btnRptPrint.Click += BtnRptPrint_Click;
            txtSearch.KeyUp += TxtSearch_KeyUp;
            setControlTabImp();
            setControlRpt5();
            RPTNAME = "05";
        }
        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                grfImp.ApplySearch(txtSearch.Text.Trim(), true, true, false);
            }
        }
        private void setControlRpt5()
        {
            lbRptStartDate.Visible = true;
            lbRptEndDate.Visible = true;
            txtImpDateStart.Visible = true;
            txtImpDateEnd.Visible = true;
            btnRpt1.Visible = true;
            cboRpt1.Visible = true;
            lbRpt2.Visible = false;
            cboRpt2.Visible = false;
        }
        private void setControlTabImp()
        {
            txtImpDateStart.Value = DateTime.Now;
            txtImpDateEnd.Value = DateTime.Now;
            txtImpPaidType.Value = "15,18";
        }
        private void initGrfImp()
        {
            grfImp = new C1FlexGrid();
            grfImp.Font = fEdit;
            grfImp.Dock = System.Windows.Forms.DockStyle.Fill;
            grfImp.Location = new System.Drawing.Point(0, 0);
            grfImp.Rows.Count = 1;
            panel2.Controls.Add(grfImp);
            theme1.SetTheme(grfImp, "Office2010Red");
        }
        private void initGrfRpt()
        {
            grfRpt = new C1FlexGrid();
            grfRpt.Font = fEdit;
            grfRpt.Dock = System.Windows.Forms.DockStyle.Fill;
            grfRpt.Location = new System.Drawing.Point(0, 0);
            grfRpt.Rows.Count = 1;
            grfRpt.Cols.Count = 2;
            grfRpt.Cols[1].Width = 300;

            grfRpt.ShowCursor = true;
            grfRpt.Cols[1].Caption = "HN";

            grfRpt.Cols[1].DataType = typeof(String);
            grfRpt.Cols[1].TextAlign = TextAlignEnum.LeftCenter;
            grfRpt.Cols[1].Visible = true;
            grfRpt.Cols[1].AllowEditing = false;
            grfRpt.Click += GrfRpt_Click;
            //grfCheckUPList.AllowFiltering = true;
            grfRpt.Rows.Count = 2;
            Row rowa = grfRpt.Rows[1];
            rowa[1] = "รายงาน รายได้แพทย์ แบบรายละเอียด";

            pnRptName.Controls.Add(grfRpt);
            theme1.SetTheme(grfRpt, bc.iniC.themeApp);
        }

        private void GrfRpt_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(grfRpt.Rows.Count == 0) return;
            if (grfRpt.Col==1)//ให้เป็น รายงานตัวที่ 5 ไปก่อน  เพราะทำรายงานตัวนี้ก่อน
            {
                RPTNAME = "05";
            }
            setControlRpt5();
        }
        private void BtnRpt1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                showLbLoading();
                if (RPTNAME.Equals("05"))
                {
                    setControlReport05();
                }
            }
            catch(Exception ex)
            {

            }
            
            hideLbLoading();
        }
        private void setControlReport05()
        {
            DateTime.TryParse(txtRptStartDate.Value.ToString(), out DateTime dtapmstart);//txtApmDate
            if (dtapmstart.Year < 1900)
            {
                dtapmstart.AddYears(543);
            }
            DateTime.TryParse(txtRptEndDate.Value.ToString(), out DateTime dtapmend);//txtApmDate
            if (dtapmend.Year < 1900)
            {
                dtapmend.AddYears(543);
            }
            bc.bcDB.dfdDB.setCboTumbonName(cboRpt1, dtapmstart.Year.ToString() + "-" + dtapmstart.ToString("MM-dd"), dtapmend.Year.ToString() + "-" + dtapmend.ToString("MM-dd"), "");
        }
        private void BtnRptPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (RPTNAME.Equals("05"))
            {
                setReport05();
            }
        }
        class DTRgrp
        {
            public String dtrcode = "";
            public float amtIN02 = 0, amtIN45 = 0, amtINcredit = 0, cntIN02 = 0, cntIN45 = 0, cntINcredit = 0, amtOUT02 = 0, amtOUT45 = 0, amtOUTcredit = 0, cntOUT02 = 0, cntOUT45 = 0, cntOUTcredit = 0;
        }
        private void setReport05()
        {
            DateTime.TryParse(txtRptStartDate.Value.ToString(), out DateTime dtapmstart);//txtApmDate
            if (dtapmstart.Year < 1900)            {                dtapmstart.AddYears(543);            }
            DateTime.TryParse(txtRptEndDate.Value.ToString(), out DateTime dtapmend);//txtApmDate
            if (dtapmend.Year < 1900)            {                dtapmend.AddYears(543);            }
            String dtrcode = "";
            dtrcode = Chk1All.Checked ? "" : cboRpt1.SelectedItem == null ? "" : ((ComboBoxItem)cboRpt1.SelectedItem).Value.ToString();
            DTRPT = bc.bcDB.dfdDB.SelectByDFdate(dtapmstart.Year.ToString() + "-" + dtapmstart.ToString("MM-dd"), dtapmend.Year.ToString() + "-" + dtapmend.ToString("MM-dd"), dtrcode);
            int i = 0;
            float amtIN02 = 0, amtIN45 = 0, amtINcredit = 0, cntIN02 = 0, cntIN45 = 0, cntINcredit = 0;
            StringBuilder sbdtrcode = new StringBuilder();
            List<DTRgrp> ldtrgrp = new List<DTRgrp>();
            Boolean flagNewGrp=false;
            foreach (DataRow drow in DTRPT.Rows)
            {
                if (i == 0)
                {
                    sbdtrcode.Append(drow["MNC_DOT_CD_DF"].ToString());
                }
                String paidcode = "";
                drow["row_number"] = i++;
                drow["paid_name"] = drow["paid_name"].ToString().Replace("ประกันสังคม (บ.5)", "ปกส5").Replace("บริษัทประกัน", "ประกัน").Replace("ประกันสังคมอิสระ (บ.5)", "ปกต5").Replace("ประกันสังคม (บ.2)", "ปกส2")
                    .Replace("ประกันสังคมอิสระ (บ.1)", "ปกต1").Replace("ประกันสังคม (บ.1)", "ปกส1").Replace("ประกันสังคมอิสระ (บ.2)", "ปกต2").Replace("ตรวจสุขภาพ (เงินสด)", "ตส.เงินสด").Replace("ตรวจสุขภาพ (บริษัท)", "ตส.บริษัท")
                    .Replace("ลูกหนี้ตรวจสุขภาพประกันสังคม", "ลห.ตส.ปกส").Replace("ลูกหนี้ฝากครรภ์", "ลห.ฝากครรภ์");//
                drow["PAY_TYPE"] = drow["PAY_TYPE"].ToString().Replace("2", "บาท").Replace("1", "%");
                drow["DF_DATE_show"] = bc.datetoShow1(drow["DF_DATE"].ToString());
                drow["FN_DATE_show"] = bc.datetoShow1(drow["FN_DAT"].ToString());
                paidcode = drow["MNC_FN_TYP_CD"].ToString();
                if (drow["MNC_DOT_CD_DF"].ToString().Equals(sbdtrcode.ToString()))
                {
                    flagNewGrp = false;
                    if (drow["MNC_FN_TYP_CD"].ToString().Equals("02")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN02 += dfamt; cntIN02++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("44")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("45")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("46")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("47")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("48")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("49")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("72")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtINcredit += dfamt; cntINcredit++; }
                    flagNewGrp = false;
                }
                else
                {
                    //รายการสุดท้าย ก่อนขึ่นรายการใหม่
                    DTRgrp dtrgrp = new DTRgrp();
                    dtrgrp.dtrcode = sbdtrcode.ToString();
                    dtrgrp.amtIN02 = amtIN02; dtrgrp.amtIN45 = amtIN45; dtrgrp.amtINcredit = amtINcredit;
                    dtrgrp.cntIN02 = cntIN02; dtrgrp.cntIN45 = cntIN45; dtrgrp.cntINcredit = cntINcredit;
                    ldtrgrp.Add(dtrgrp);
                    //รายการสุดท้าย ก่อนขึ่นรายการใหม่

                    amtIN02 = 0; amtIN45 = 0; amtINcredit = 0; cntIN02 = 0; cntIN45 = 0; cntINcredit = 0;
                    sbdtrcode.Clear();                    sbdtrcode.Append(drow["MNC_DOT_CD_DF"].ToString());
                    flagNewGrp = true;
                    
                    if (drow["MNC_FN_TYP_CD"].ToString().Equals("02")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN02 += dfamt; cntIN02++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("44")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("45")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("46")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("47")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("48")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("49")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else if (drow["MNC_FN_TYP_CD"].ToString().Equals("72")) { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtIN45 += dfamt; cntIN45++; }
                    else { float.TryParse(drow["DF_AMT"].ToString(), out float dfamt); amtINcredit += dfamt; cntINcredit++; }
                    
                }
            }
            if(cboRpt1.Items.Count > 0)//ระบุแพทย์
            {
                if (ldtrgrp.Count == 0)
                {
                    DTRgrp dtrgrp = new DTRgrp();
                    dtrgrp.dtrcode = sbdtrcode.ToString();
                    dtrgrp.amtIN02 = amtIN02; dtrgrp.amtIN45 = amtIN45; dtrgrp.amtINcredit = amtINcredit;
                    dtrgrp.cntIN02 = cntIN02; dtrgrp.cntIN45 = cntIN45; dtrgrp.cntINcredit = cntINcredit;
                    ldtrgrp.Add(dtrgrp);
                }
            }
            i = 0;
            foreach (DataRow drow in DTRPT.Rows)
            {
                sbdtrcode.Clear(); sbdtrcode.Append(drow["MNC_DOT_CD_DF"].ToString());
                foreach (DTRgrp dtrg in ldtrgrp)
                {
                    if (dtrg.dtrcode.Equals(sbdtrcode.ToString()))
                {
                    drow["sum_grp_dtr_02"] = dtrg.amtIN02.ToString("#,###.00");
                        drow["sum_grp_dtr_44"] = dtrg.amtIN45.ToString("#,###.00");
                        drow["sum_grp_dtr_credit"] = dtrg.amtINcredit.ToString("#,###.00");
                        drow["cnt_grp_dtr_02"] = dtrg.cntIN02.ToString("#,###");
                        drow["cnt_grp_dtr_44"] = dtrg.cntIN45.ToString("#,###");
                        drow["cnt_grp_dtr_credit"] = dtrg.cntINcredit.ToString("#,###");
                }
                }
            }
            FileInfo rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\df_detail_doctor.rdlx");
            if (dtrcode.Length > 0)
            {
                //rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\appointment_date_doctor.rdlx");
                rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\df_detail_doctor.rdlx");
            }
            if (!File.Exists(rptPath.FullName))
            {
                lfSbMessage.Text = "File report not found";
                return;
            }
            PageReport definition = new PageReport(rptPath);
            PageDocument runtime = new GrapeCity.ActiveReports.Document.PageDocument(definition);
            
            runtime.Parameters["line1"].CurrentValue = bc.iniC.hostname;
            runtime.Parameters["line2"].CurrentValue = "รายงานรายได้แพทย์ ประจำวันที่ " + dtapmstart.ToString("dd-MM-yyyy") + " ถึงวันที่ " + dtapmend.ToString("dd-MM-yyyy");
            runtime.Parameters["line3"].CurrentValue = (dtrcode.Length > 0) ? "แพทย์ " + cboRpt1.Text : "";

            runtime.LocateDataSource += Runtime_LocateDataSource;
            arvMain.LoadDocument(runtime);
        }
        private void Runtime_LocateDataSource(object sender, LocateDataSourceEventArgs args)
        {
            //throw new NotImplementedException();
            args.Data = DTRPT;
        }
        private void BtnImportDfGen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String dfdate = "";
            int i = 20000;
            DateTime date = new DateTime();
            DateTime.TryParse(DateTime.Now.ToString(), out date);
            if (date.Year > 2500) {                date = date.AddYears(-543);            }
            else if (date.Year < 2000) {                date = date.AddYears(543);            }
            dfdate = date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            DateTime startdate1 = new DateTime();
            DateTime enddate1 = new DateTime();
            DateTime.TryParse(txtImpDateStart.Value.ToString(), out startdate1);
            DateTime.TryParse(txtImpDateEnd.Value.ToString(), out enddate1);
            String paidtype = "", startdate = "", enddate = "";

            if (startdate1.Year > 2500) { startdate1 = startdate1.AddYears(-543); }
            else if (startdate1.Year < 2000) { startdate1 = startdate1.AddYears(543); }
            startdate = startdate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            if (enddate1.Year > 2500) { enddate1 = enddate1.AddYears(-543); }
            else if (enddate1.Year < 2000) { enddate1 = enddate1.AddYears(543); }
            enddate = enddate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            String doccd = "", docyr = "", docdat="";
            if (grfImp.Rows.Count > 1)
            {
                docyr = grfImp.Rows[1][colImpdoc_yr] != null ? grfImp.Rows[1][colImpdoc_yr].ToString() : "";
                docdat = grfImp.Rows[1][colImpdocdat] != null ? grfImp.Rows[1][colImpdocdat].ToString() : "";
                String re1 = bc.bcDB.dfdDB.delete(startdate, enddate, docyr);
                foreach (Row row in grfImp.Rows)
                {
                    String docno = "", fncd = "", fnno = "", fndat = "", chk = "", fndsc = "", dfamt = "", vsdate = "", vstime = "", hnno = "", hnyr = "", patname = "", preno = "", paidtypecode = "", dtrid = "", dtrname = "";
                    String anno = "", anyr = "";
                    chk = row[colImpchk] != null ? row[colImpchk].ToString() : "";

                    if (!chk.ToLower().Equals("true")) continue;

                    doccd = row[colImpdoccd] != null ? row[colImpdoccd].ToString() : "";
                    docyr = row[colImpdoc_yr] != null ? row[colImpdoc_yr].ToString() : "";
                    docno = row[colImpdocno] != null ? row[colImpdocno].ToString() : "";
                    docdat = row[colImpdocdat] != null ? row[colImpdocdat].ToString() : "";

                    vsdate = row[colImpvsdate] != null ? row[colImpvsdate].ToString() : "";
                    vstime = row[colImpvstime] != null ? row[colImpvstime].ToString() : "";
                    hnno = row[colImphnno] != null ? row[colImphnno].ToString() : "";
                    hnyr = row[colImphnyr] != null ? row[colImphnyr].ToString() : "";
                    patname = row[colImppttname] != null ? row[colImppttname].ToString() : "";
                    preno = row[colImppreno] != null ? row[colImppreno].ToString() : "";
                    paidtypecode = row[colImppaidtype] != null ? row[colImppaidtype].ToString() : "";
                    dtrid = row[colImpdtrcode] != null ? row[colImpdtrcode].ToString() : "";
                    dtrname = row[colImpdtrname] != null ? row[colImpdtrname].ToString() : "";
                    anno = row[colImpanno] != null ? row[colImpanno].ToString() : "";
                    anyr = row[colImpanyr] != null ? row[colImpanyr].ToString() : "";

                    fncd = "470"; fndsc = "ค่าแพทย์ CHECKUP"; dfamt = "10";

                    DotDfDetail dotdfd = new DotDfDetail();
                    dotdfd.MNC_DOC_CD = doccd;              //pk
                    dotdfd.MNC_DOC_YR = docyr;              //pk
                    dotdfd.MNC_DOC_NO = docno;              //pk
                    dotdfd.MNC_DOC_DAT = docdat;            //pk
                    dotdfd.MNC_FN_CD = fncd;                //pk
                    dotdfd.MNC_FN_NO = i.ToString();        //pk
                    dotdfd.MNC_FN_DAT = docdat;             //pk
                                                            //dotdfd.MNC_DF_DATE = dfdate;
                    dotdfd.MNC_DF_DATE = vsdate;       // ต้องเป็น vsdate จากการดู data ของ วันต่างๆ
                    dotdfd.MNC_FN_TYP_DESC = fndsc;
                    dotdfd.MNC_DF_AMT = dfamt;
                    dotdfd.MNC_FN_AMT = dfamt;
                    dotdfd.MNC_DATE = vsdate;
                    dotdfd.MNC_TIME = vstime;
                    dotdfd.MNC_HN_NO = hnno;
                    dotdfd.MNC_HN_YR = hnyr;
                    dotdfd.MNC_AN_NO = anno;
                    dotdfd.MNC_AN_YR = anyr;
                    dotdfd.MNC_PAT_NAME = patname;
                    dotdfd.MNC_PRE_NO = preno;
                    dotdfd.MNC_FN_TYP_CD = paidtypecode;
                    dotdfd.MNC_DOT_CD_DF = dtrid;
                    dotdfd.MNC_DOT_GRP_CD = "0";
                    dotdfd.MNC_DOT_NAME = dtrid + "0" + dtrname;
                    dotdfd.MNC_PAY_FLAG = "Y";
                    dotdfd.MNC_PAY_DAT = "";            // null
                    dotdfd.MNC_PAY_NO = "";            // null
                    dotdfd.MNC_PAY_YR = "";            // null
                    dotdfd.MNC_REF_NO = "";            // null
                    dotdfd.MNC_REF_DAT = "";            // null
                    dotdfd.MNC_EMP_CD = "";            // null
                    dotdfd.MNC_DF_GROUP = "1";
                    dotdfd.MNC_PAY_TYP = "2";
                    dotdfd.MNC_PAY_RATE = dfamt;
                    dotdfd.MNC_DF_DET_TYPE = "";            // null
                    dotdfd.status_insert_manual = "1";

                    String re = bc.bcDB.dfdDB.insert(dotdfd);
                    i++;
                }
            }
            MessageBox.Show("gen ข้อมูล ค่าแพทย์ checkup เรียบร้อย " +i.ToString(), "");
        }
        private void BtnImportDfSelect_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pageLoad = true;
            showLbLoading();
            DataTable dt = new DataTable();
            DateTime startdate1 = new DateTime();
            DateTime enddate1 = new DateTime();
            DateTime.TryParse(txtImpDateStart.Value.ToString(), out startdate1);
            DateTime.TryParse(txtImpDateEnd.Value.ToString(), out enddate1);
            String paidtype = "", startdate = "", enddate = "";

            if (startdate1.Year > 2500)            {                startdate1 = startdate1.AddYears(-543);            }
            else if (startdate1.Year < 2000)            {                startdate1 = startdate1.AddYears(543);            }
            startdate = startdate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            if (enddate1.Year > 2500)            {                enddate1 = enddate1.AddYears(-543);            }
            else if (enddate1.Year < 2000)            {                enddate1 = enddate1.AddYears(543);            }
            enddate = enddate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            String[] paid = txtImpPaidType.Text.Trim().Split(',');
            if (paid.Length > 0)
            {
                foreach (String txt in paid)
                {
                    paidtype += "'" + txt + "',";
                }
                if (paidtype.Length > 1)
                {
                    paidtype = paidtype.Substring(0, paidtype.Length - 1);
                }
            }
            else
            {
                paidtype = txtImpPaidType.Text.Trim();
            }
            if(chkDriverLicense.Checked) dt = bc.bcDB.vsDB.selectFinancePatient02DriverLicense(startdate, enddate, paidtype);
            else dt = bc.bcDB.vsDB.selectFinancePatient(startdate, enddate, paidtype);

            Column colChk = grfImp.Cols[colImpchk];
            colChk.DataType = typeof(Boolean);
            grfImp.Cols.Count = 23;
            //grfSelect.Cols.Count = 12;
            grfImp.Rows.Count = 1;
            grfImp.Cols[colImpvsdateShow].Caption = "Date";
            grfImp.Cols[colImphnno].Caption = "HN";
            grfImp.Cols[colImppttname].Caption = "Patient Name";
            grfImp.Cols[colImppreno].Caption = "preno";
            grfImp.Cols[colImppaidtype].Caption = "สิทธิ";
            grfImp.Cols[colImpdtrcode].Caption = "แพทย์";
            grfImp.Cols[colImpdtrname].Caption = "ชื่อแพทย์";
            grfImp.Cols[colImpanno].Caption = "AN NO";
            grfImp.Cols[colImpanyr].Caption = "AN YR";
            //grfSelect.Cols[coldtrname].Caption = "HN";
            grfImp.Cols[colImpchk].Width = 50;
            grfImp.Cols[colImpvsdateShow].Width = 90;
            grfImp.Cols[colImphnno].Width = 80;
            grfImp.Cols[colImppttname].Width = 220;
            grfImp.Cols[colImppreno].Width = 60;
            grfImp.Cols[colImppaidtype].Width = 60;
            grfImp.Cols[colImpdtrname].Width = 220;
            grfImp.Cols[colSymptoms].Width = 250;
            grfImp.Cols[colImpanno].Width = 60;
            grfImp.Cols[colImpanyr].Width = 60;
            grfImp.Cols[colImphnyr].Width = 60;
            grfImp.Cols[colImpvsdate].Width = 80;
            grfImp.Cols[colImpvsdateShow].DataType = typeof(String);
            grfImp.Cols[colImpvsdate].DataType = typeof(String);
            
            //grfSelect.Cols[colfndesc].Width = 250;
            //grfSelect.Cols[colfncd].Width = 50;
            //grfSelect.Cols[colno].Width = 50;
            //grfSelect.Cols[colfnamt].Width = 50;
            //grfSelect.Cols[coldtrname].Width = 60;
            grfImp.Rows.Count = dt.Rows.Count + 1;
            int i = 1;
            foreach (DataRow drow in dt.Rows)
            {
                grfImp[i, colImpvsdateShow] = bc.datetoShow1(drow["MNC_DATE"].ToString());
                grfImp[i, colImpvsdate] = drow["MNC_DATE"].ToString();
                grfImp[i, colImphnno] = drow["MNC_HN_NO"].ToString();
                grfImp[i, colImppttname] = drow["MNC_PFIX_DSC"].ToString() + " " + drow["MNC_FNAME_T"].ToString() + " " + drow["MNC_LNAME_T"].ToString();
                grfImp[i, colImppreno] = drow["MNC_PRE_NO"].ToString();
                grfImp[i, colImppaidtype] = drow["MNC_FN_TYP_CD"].ToString();
                grfImp[i, colImpdtrcode] = drow["MNC_DOT_CD"].ToString();
                grfImp[i, colImpdtrname] = drow["MNC_PFIX_DSCdtr"].ToString() + " " + drow["MNC_DOT_FNAME"].ToString() + " " + drow["MNC_DOT_LNAME"].ToString();
                grfImp[i, colImpdoccd] = drow["MNC_DOC_CD"].ToString();
                grfImp[i, colImpdoc_yr] = drow["MNC_DOC_yr"].ToString();
                grfImp[i, colImpdocno] = drow["MNC_DOC_no"].ToString();
                grfImp[i, colImpdocdat] = drow["MNC_doc_dat"].ToString();
                grfImp[i, colImpvstime] = drow["MNC_time"].ToString();
                grfImp[i, colImpanno] = drow["MNC_AN_NO"].ToString();
                grfImp[i, colImpanyr] = drow["MNC_AN_YR"].ToString();
                grfImp[i, colImphnyr] = drow["MNC_hn_YR"].ToString();
                grfImp[i, colSymptoms] = drow["MNC_SHIF_MEMO"].ToString();
                grfImp[i, colImpchk] = true;

                grfImp[i, 0] = i;
                i++;
            }
            grfImp.Cols[colImpdoccd].Visible = false;
            grfImp.Cols[colImpdoc_yr].Visible = false;
            grfImp.Cols[colImpdocno].Visible = false;
            grfImp.Cols[colImpdocdat].Visible = false;
            grfImp.Cols[colImpvstime].Visible = false;
            grfImp.Cols[colImpfncd].Visible = false;
            grfImp.Cols[colImpno].Visible = false;
            grfImp.Cols[colImpfnamt].Visible = false;
            grfImp.Cols[colImpfndesc].Visible = false;
            //grfImp.Cols[colImpvsdate].Visible = false;

            grfImp.Cols[colImpvsdateShow].AllowEditing = false;
            grfImp.Cols[colImphnno].AllowEditing = false;
            grfImp.Cols[colImppttname].AllowEditing = false;
            grfImp.Cols[colImppreno].AllowEditing = false;
            grfImp.Cols[colImppaidtype].AllowEditing = false;
            grfImp.Cols[colImpdtrname].AllowEditing = false;
            grfImp.Cols[colSymptoms].AllowEditing = false;
            //grfSelect.Cols[colGrfUcepSelectHn].AllowEditing = false;
            hideLbLoading();
            pageLoad = false;
        }
        private void setLbLoading(String txt)
        {
            lbLoading.Text = txt;
            Application.DoEvents();
        }
        private void showLbLoading()
        {
            lbLoading.Show();
            lbLoading.BringToFront();
            Application.DoEvents();
        }
        private void hideLbLoading()
        {
            lbLoading.Hide();
            Application.DoEvents();
        }
        private void FrmDfDoctor1_Load(object sender, EventArgs e)
        {
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            int scrH = Screen.PrimaryScreen.Bounds.Height;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Maximized;
            txtRptStartDate.Value = DateTime.Now;
            txtRptEndDate.Value = DateTime.Now;
            grfImp.Size = new Size(scrW - 20, scrH - btnImportDfSelect.Location.Y - 140);
            grfImp.Location = new Point(5, btnImportDfSelect.Location.Y + 40);
            spReport.HeaderHeight = 0;

            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();
            rgSbModule.Text = bc.iniC.hostDBMainHIS + " " + bc.iniC.nameDBMainHIS;
            this.Text = "Last Update 2024-05-09-1";
        }
    }
}
