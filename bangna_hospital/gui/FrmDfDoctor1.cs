using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmDfDoctor1 : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B;

        C1FlexGrid grfImp;

        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        //C1PdfDocument pdfDoc;
        C1ThemeController theme1;
        Label lbLoading;
        
        Boolean pageLoad = false;
        int colImpchk = 1, colImpvsdate = 2, colImphnno = 3, colImppttname = 4, colImppreno = 5, colImppaidtype = 6, colImpdtrcode = 7, colImpdtrname = 8, colImpdoccd = 9, colImpdoc_yr = 10, colImpdocno = 11, colImpfncd = 12, colImpno = 13, colImpfnamt = 14, colImpfndesc = 15, colImpdocdat = 16, colImpvstime = 17, colImpanno = 18, colImpanyr = 19, colImphnyr = 20;
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

            btnImportDfSelect.Click += BtnImportDfSelect_Click;
            btnImportDfGen.Click += BtnImportDfGen_Click;

            setControlImp();
        }

        private void BtnImportDfGen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String dfdate = "";
            int i = 20000;
            DateTime date = new DateTime();
            DateTime.TryParse(DateTime.Now.ToString(), out date);
            if (date.Year > 2500)
            {
                date = date.AddYears(-543);
            }
            else if (date.Year < 2000)
            {
                date = date.AddYears(543);
            }
            dfdate = date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            foreach (Row row in grfImp.Rows)
            {
                String doccd = "", docyr = "", docno = "", docdat = "", fncd = "", fnno = "", fndat = "", chk = "", fndsc = "", dfamt = "", vsdate = "", vstime = "", hnno = "", hnyr = "", patname = "", preno = "", paidtypecode = "", dtrid = "", dtrname = "";
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

                fncd = "470";
                fndsc = "ค่าแพทย์ CHECKUP";
                dfamt = "10";

                DotDfDetail dotdfd = new DotDfDetail();
                dotdfd.MNC_DOC_CD = doccd;              //pk
                dotdfd.MNC_DOC_YR = docyr;              //pk
                dotdfd.MNC_DOC_NO = docno;              //pk
                dotdfd.MNC_DOC_DAT = docdat;            //pk
                dotdfd.MNC_FN_CD = fncd;                //pk
                dotdfd.MNC_FN_NO = i.ToString();        //pk
                dotdfd.MNC_FN_DAT = docdat;             //pk
                //dotdfd.MNC_DF_DATE = dfdate;
                dotdfd.MNC_DF_DATE = bc.datetoDB(vsdate);       // ต้องเป็น vsdate จากการดู data ของ วันต่างๆ
                dotdfd.MNC_FN_TYP_DESC = fndsc;
                dotdfd.MNC_DF_AMT = dfamt;
                dotdfd.MNC_FN_AMT = dfamt;
                dotdfd.MNC_DATE = bc.datetoDB(vsdate);
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

                String re = bc.bcDB.dotdfdDB.insert(dotdfd);
                i++;
            }
            MessageBox.Show("gen ข้อมูล ค่าแพทย์ checkup เรียบร้อย", "");
        }

        private void BtnImportDfSelect_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pageLoad = true;
            showLbLoading();
            DataTable dt = new DataTable();
            DateTime startdate1 = new DateTime();
            DateTime enddate1 = new DateTime();
            DateTime.TryParse(txtImpDateStart.Text, out startdate1);
            DateTime.TryParse(txtImpDateEnd.Text, out enddate1);
            String paidtype = "", startdate = "", enddate = "";

            if (startdate1.Year > 2500)
            {
                startdate1 = startdate1.AddYears(-543);
            }
            else if (startdate1.Year < 2000)
            {
                startdate1 = startdate1.AddYears(543);
            }
            startdate = startdate1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            if (enddate1.Year > 2500)
            {
                enddate1 = enddate1.AddYears(-543);
            }
            else if (enddate1.Year < 2000)
            {
                enddate1 = enddate1.AddYears(543);
            }
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
            dt = bc.bcDB.vsDB.selectFinancePatient(startdate, enddate, paidtype);

            Column colChk = grfImp.Cols[colImpchk];
            colChk.DataType = typeof(Boolean);
            grfImp.Cols.Count = 21;
            //grfSelect.Cols.Count = 12;
            grfImp.Rows.Count = 1;
            grfImp.Cols[colImpvsdate].Caption = "Date";
            grfImp.Cols[colImphnno].Caption = "HN";
            grfImp.Cols[colImppttname].Caption = "Patient Name";
            grfImp.Cols[colImppreno].Caption = "preno";
            grfImp.Cols[colImppaidtype].Caption = "สิทธิ";
            grfImp.Cols[colImpdtrcode].Caption = "แพทย์";
            grfImp.Cols[colImpanno].Caption = "AN NO";
            grfImp.Cols[colImpanyr].Caption = "AN YR";
            //grfSelect.Cols[coldtrname].Caption = "HN";
            grfImp.Cols[colImpchk].Width = 50;
            grfImp.Cols[colImpvsdate].Width = 100;
            grfImp.Cols[colImphnno].Width = 80;
            grfImp.Cols[colImppttname].Width = 250;
            grfImp.Cols[colImppreno].Width = 60;
            grfImp.Cols[colImppaidtype].Width = 60;
            grfImp.Cols[colImpdtrname].Width = 250;
            //grfSelect.Cols[colfndesc].Width = 250;
            //grfSelect.Cols[colfncd].Width = 50;
            //grfSelect.Cols[colno].Width = 50;
            //grfSelect.Cols[colfnamt].Width = 50;
            //grfSelect.Cols[coldtrname].Width = 60;
            grfImp.Rows.Count = dt.Rows.Count + 1;
            int i = 1;
            foreach (DataRow drow in dt.Rows)
            {
                grfImp[i, colImpvsdate] = bc.datetoShow(drow["MNC_DATE"].ToString());
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
                grfImp[i, colImpchk] = true;

                grfImp[i, 0] = i;
                i++;
            }
            grfImp.Cols[colImpdoccd].Visible = false;
            grfImp.Cols[colImpdoc_yr].Visible = false;
            grfImp.Cols[colImpdocno].Visible = false;
            grfImp.Cols[colImpdocdat].Visible = false;
            grfImp.Cols[colImpvstime].Visible = false;

            grfImp.Cols[colImpvsdate].AllowEditing = false;
            grfImp.Cols[colImphnno].AllowEditing = false;
            grfImp.Cols[colImppttname].AllowEditing = false;
            grfImp.Cols[colImppreno].AllowEditing = false;
            grfImp.Cols[colImppaidtype].AllowEditing = false;
            grfImp.Cols[colImpdtrname].AllowEditing = false;
            //grfSelect.Cols[colGrfUcepSelectHn].AllowEditing = false;
            //grfSelect.Cols[colGrfUcepSelectHn].AllowEditing = false;
            hideLbLoading();
            pageLoad = false;
        }

        private void setControlImp()
        {
            txtImpDateStart.Value = DateTime.Now;
            txtImpDateEnd.Value = DateTime.Now;
            txtImpPaidType.Value = "15,18";
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

            grfImp.Size = new Size(scrW - 20, scrH - btnImportDfSelect.Location.Y - 140);
            grfImp.Location = new Point(5, btnImportDfSelect.Location.Y + 40);

            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();

            this.Text = "Last Update 2024-02-17";
        }
    }
}
