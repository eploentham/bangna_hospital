using bangna_hospital.control;
using C1.C1Pdf;
using C1.Win.C1FlexGrid;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmOPBkkClaimPDF : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2;
        C1ThemeController theme1;
        C1FlexGrid grfUcepSelect;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1PdfDocument pdfDoc;
        int colGrfUcepSelectSelect = 1, colGrfUcepusedate = 2, colGrfUceptmtcode = 3, colGrfUcephospcode = 4, colGrfUcepcat116 = 5, colGrfUcepmean = 6, colGrfUcepunit = 7, colGrfUceppricetotal = 8, colGrfUcepprice = 9, colGrfhospcode1 = 10;
        public DataTable DTPDF;
        public String flagCreatePDF = "nocreatePDf";
        public FrmOPBkkClaimPDF(BangnaControl bc, DataTable dtpdf)
        {
            this.bc = bc;
            this.DTPDF = dtpdf;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            fPDF = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fPDFs2 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            fPDFl2 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize + 2, FontStyle.Regular);
            fPDFs6 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 6, FontStyle.Regular);
            fPDFs8 = new System.Drawing.Font(bc.iniC.pdfFontName, bc.pdfFontSize - 8, FontStyle.Regular);

            theme1 = new C1.Win.C1Themes.C1ThemeController();
            btnPDF.Click += BtnPDF_Click;
            initGrfUcepSelect();
        }

        private void BtnPDF_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            flagCreatePDF = "createPDf";
            int i = 0;
            foreach(Row row1 in grfUcepSelect.Rows)
            {
                if (row1[0] == null) continue;
                String aa = row1[0].ToString();
                DTPDF.Rows[i]["status_using"] = row1[colGrfUcepSelectSelect] != null ? (Boolean)row1[colGrfUcepSelectSelect] == true ? "1" : "0" : "0";
                i++;
            }
            this.Dispose();
        }

        private void initGrfUcepSelect()
        {
            grfUcepSelect = new C1FlexGrid();
            grfUcepSelect.Font = fEdit;
            grfUcepSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            grfUcepSelect.Location = new System.Drawing.Point(0, 0);
            grfUcepSelect.Rows.Count = 1;
            grfUcepSelect.Name = "grfUcepSelect";

            pnUcepSelect.Controls.Add(grfUcepSelect);
            Column colChk = grfUcepSelect.Cols[colGrfUcepSelectSelect];
            colChk.DataType = typeof(Boolean);
            grfUcepSelect.Cols.Count = 11;
            grfUcepSelect.Rows.Count = 1;
            grfUcepSelect.Cols[colGrfUcepusedate].Caption = "use_date";
            grfUcepSelect.Cols[colGrfUceptmtcode].Caption = "tmt_code";
            grfUcepSelect.Cols[colGrfUcephospcode].Caption = "hosp_code";
            grfUcepSelect.Cols[colGrfUcepcat116].Caption = "cat_1_16";
            grfUcepSelect.Cols[colGrfUcepmean].Caption = "mean";
            grfUcepSelect.Cols[colGrfUcepunit].Caption = "unit";
            grfUcepSelect.Cols[colGrfUceppricetotal].Caption = "price_total";
            
            grfUcepSelect.Cols[colGrfhospcode1].Caption = "price";
            grfUcepSelect.Cols[colGrfhospcode1].Caption = "hosp_code1";
            grfUcepSelect.Cols[colGrfUcepSelectSelect].Width = 60;
            grfUcepSelect.Cols[colGrfUcepusedate].Width = 90;
            grfUcepSelect.Cols[colGrfUceptmtcode].Width = 300;
            grfUcepSelect.Cols[colGrfUcephospcode].Width = 100;
            grfUcepSelect.Cols[colGrfUcepcat116].Width = 100;
            grfUcepSelect.Cols[colGrfUcepmean].Width = 300;
            grfUcepSelect.Cols[colGrfUcepunit].Width = 60;
            grfUcepSelect.Cols[colGrfUceppricetotal].Width = 60;
            
            grfUcepSelect.Cols[colGrfhospcode1].Width = 100;

            grfUcepSelect.Rows.Count = DTPDF.Rows.Count + 1;

            int i = 1;
            foreach (DataRow drow in DTPDF.Rows)
            {
                grfUcepSelect[i, colGrfUcepSelectSelect] = drow["status_using"].ToString();
                grfUcepSelect[i, colGrfUcepusedate] = drow["use_date"].ToString();
                grfUcepSelect[i, colGrfUceptmtcode] = drow["tmt_code"].ToString();
                grfUcepSelect[i, colGrfUcephospcode] = drow["hosp_code"].ToString();
                grfUcepSelect[i, colGrfUcepcat116] = drow["cat_1_16"].ToString();
                grfUcepSelect[i, colGrfUcepmean] = drow["mean"].ToString();
                grfUcepSelect[i, colGrfUcepunit] = drow["unit"].ToString();
                grfUcepSelect[i, colGrfUceppricetotal] = drow["price_total"].ToString();
                grfUcepSelect[i, colGrfUcepprice] = drow["price"].ToString();
                grfUcepSelect[i, colGrfhospcode1] = drow["hosp_code1"].ToString();
                grfUcepSelect[i, colGrfUcepprice] = drow["status_using"].ToString();

                grfUcepSelect[i, 0] = i;
                i++;

            }
            
            grfUcepSelect.Cols[colGrfUcepusedate].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUceptmtcode].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUcephospcode].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUcepcat116].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUcepmean].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUcepunit].AllowEditing = false;
            grfUcepSelect.Cols[colGrfUceppricetotal].AllowEditing = false;
            grfUcepSelect.Cols[colGrfhospcode1].AllowEditing = false;
            grfUcepSelect.SelectionMode = SelectionModeEnum.Row;

            grfUcepSelect.AllowFiltering = true;
        }
        private void FrmOPBkkClaimPDF_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 1400;
        }
    }
}
