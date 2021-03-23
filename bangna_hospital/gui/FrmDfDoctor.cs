using bangna_hospital.control;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmDfDoctor:Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B;

        C1DockingTab tcMain;
        C1DockingTabPage tabImportDf;
        C1FlexGrid grfSelect;
        C1Button btnImportDfSelect, btnImportDfGen;

        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        //C1PdfDocument pdfDoc;
        C1ThemeController theme1;
        Label lbDateStart, lbDateEnd, lbtxtPaidType, lbLoading;
        C1DateEdit txtDateStart, txtDateEnd;
        C1TextBox txtPaidType;
        Boolean pageLoad = false;
        int colchk = 1, colvsdate = 2, colhnno = 3, colpttname = 4, colpreno = 5, colpaidtype = 6, coldtrcode = 7, coldtrname = 8, coldoccd = 9, coldoc_yr = 10, coldocno = 11, colfncd = 12, colno = 13, colfnamt = 14, colfndesc = 15;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmDfDoctor(BangnaControl bc)
        {
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

            initCompoment();
            this.Load += FrmDfDoctor_Load;
            btnImportDfSelect.Click += BtnImportDfSelect_Click;
            btnImportDfGen.Click += BtnImportDfGen_Click;

            setControl();
        }

        private void BtnImportDfGen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
        }

        private void BtnImportDfSelect_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            pageLoad = true;
            showLbLoading();
            DataTable dt = new DataTable();
            DateTime startdate1 = new DateTime();
            DateTime enddate1 = new DateTime();
            DateTime.TryParse(txtDateStart.Text, out startdate1);
            DateTime.TryParse(txtDateEnd.Text, out enddate1);
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
            String[] paid = txtPaidType.Text.Trim().Split(',');
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
                paidtype = txtPaidType.Text.Trim();
            }
            dt = bc.bcDB.vsDB.selectFinancePatient(startdate, enddate, paidtype);

            Column colChk = grfSelect.Cols[colchk];
            colChk.DataType = typeof(Boolean);
            grfSelect.Cols.Count = 16;
            grfSelect.Cols.Count = 12;
            grfSelect.Rows.Count = 1;
            grfSelect.Cols[colvsdate].Caption = "Date";
            grfSelect.Cols[colhnno].Caption = "HN";
            grfSelect.Cols[colpttname].Caption = "Patient Name";
            grfSelect.Cols[colpreno].Caption = "preno";
            grfSelect.Cols[colpaidtype].Caption = "สิทธิ";
            grfSelect.Cols[coldtrcode].Caption = "แพทย์";
            //grfSelect.Cols[coldtrname].Caption = "HN";
            grfSelect.Cols[colchk].Width = 50;
            grfSelect.Cols[colvsdate].Width = 100;
            grfSelect.Cols[colhnno].Width = 80;
            grfSelect.Cols[colpttname].Width = 250;
            grfSelect.Cols[colpreno].Width = 60;
            grfSelect.Cols[colpaidtype].Width = 60;
            grfSelect.Cols[coldtrname].Width = 250;
            //grfSelect.Cols[colfndesc].Width = 250;
            //grfSelect.Cols[colfncd].Width = 50;
            //grfSelect.Cols[colno].Width = 50;
            //grfSelect.Cols[colfnamt].Width = 50;
            //grfSelect.Cols[coldtrname].Width = 60;
            grfSelect.Rows.Count = dt.Rows.Count + 1;
            int i = 1;
            foreach (DataRow drow in dt.Rows)
            {
                grfSelect[i, colvsdate] = bc.datetoShow(drow["MNC_DATE"].ToString());
                grfSelect[i, colhnno] = drow["MNC_HN_NO"].ToString();
                grfSelect[i, colpttname] = drow["MNC_PFIX_DSC"].ToString() + " " + drow["MNC_FNAME_T"].ToString() + " " + drow["MNC_LNAME_T"].ToString();
                grfSelect[i, colpreno] = drow["MNC_PRE_NO"].ToString();
                grfSelect[i, colpaidtype] = drow["MNC_FN_TYP_CD"].ToString();
                grfSelect[i, coldtrcode] = drow["MNC_DOT_CD"].ToString();
                grfSelect[i, coldtrname] = drow["MNC_PFIX_DSCdtr"].ToString() + " " + drow["MNC_DOT_FNAME"].ToString() + " " + drow["MNC_DOT_LNAME"].ToString();
                grfSelect[i, coldoccd] = drow["MNC_DOC_CD"].ToString();
                grfSelect[i, coldoc_yr] = drow["MNC_DOC_yr"].ToString();
                grfSelect[i, coldocno] = drow["MNC_DOC_no"].ToString();
                //grfSelect[i, colfncd] = drow["MNC_FN_CD"].ToString();
                //grfSelect[i, colno] = drow["MNC_NO"].ToString();
                //grfSelect[i, colfnamt] = drow["MNC_FN_AMT"].ToString();
                //grfSelect[i, colfndesc] = drow["MNC_FN_DSCT"].ToString();
                grfSelect[i, colchk] = true;

                grfSelect[i, 0] = i;
                i++;
            }
            grfSelect.Cols[coldoccd].Visible = false;
            grfSelect.Cols[coldoc_yr].Visible = false;
            grfSelect.Cols[coldocno].Visible = false;

            grfSelect.Cols[colvsdate].AllowEditing = false;
            grfSelect.Cols[colhnno].AllowEditing = false;
            grfSelect.Cols[colpttname].AllowEditing = false;
            grfSelect.Cols[colpreno].AllowEditing = false;
            grfSelect.Cols[colpaidtype].AllowEditing = false;
            grfSelect.Cols[coldtrname].AllowEditing = false;
            //grfSelect.Cols[colGrfUcepSelectHn].AllowEditing = false;
            //grfSelect.Cols[colGrfUcepSelectHn].AllowEditing = false;
            hideLbLoading();
            pageLoad = false;
        }

        

        private void initCompoment()
        {
            int gapLine = 25, gapX = 20, gapY = 20, xCol2 = 130, xCol1 = 80, xCol3 = 330, xCol4 = 640, xCol5 = 950;
            Size size = new Size();

            tcMain = new C1DockingTab();
            tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tcMain.Location = new System.Drawing.Point(0, 266);
            tcMain.Name = "tcMain";
            tcMain.Size = new System.Drawing.Size(669, 200);
            tcMain.TabIndex = 0;
            tcMain.TabsSpacing = 5;

            tabImportDf = new C1DockingTabPage();
            tabImportDf.Dock = System.Windows.Forms.DockStyle.Fill;
            tabImportDf.Name = "tabImportDf";
            tabImportDf.Text = "Import Item DF";

            grfSelect = new C1FlexGrid();
            grfSelect.Font = fEdit;
            grfSelect.Dock = System.Windows.Forms.DockStyle.Bottom;
            grfSelect.Location = new System.Drawing.Point(0, 0);
            grfSelect.Rows.Count = 1;

            lbDateStart = new Label();
            txtDateStart = new C1DateEdit();
            lbDateEnd = new Label();
            txtDateEnd = new C1DateEdit();
            lbtxtPaidType = new Label();
            txtPaidType = new C1TextBox();
            btnImportDfSelect = new C1Button();
            btnImportDfGen = new C1Button();

            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);

            bc.setControlLabel(ref lbDateStart, fEdit, "วันที่เริ่มต้น :", "lbDateStart", gapX, gapY);
            size = bc.MeasureString(lbDateStart);
            bc.setControlC1DateTimeEdit(ref txtDateStart, "txtDateStart", lbDateStart.Location.X + size.Width + 5, gapY);
            size = bc.MeasureString(lbDateStart);
            //bc.setControlC1DateTimeEdit(ref txtDateStart, "txtDateStart", lbDateStart.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbDateEnd, fEdit, "วันที่สิ้นสุด :", "lbDateEnd", txtDateStart.Location.X + txtDateStart.Width + 15, gapY);
            size = bc.MeasureString(lbDateEnd);
            bc.setControlC1DateTimeEdit(ref txtDateEnd, "txtDateEnd", lbDateEnd.Location.X + size.Width + 5, gapY);

            bc.setControlLabel(ref lbtxtPaidType, fEdit, "สิทธิ (xx,...) :", "lbtxtPaidType", txtDateEnd.Location.X + txtDateEnd.Width + 15, gapY);
            size = bc.MeasureString(lbtxtPaidType);
            bc.setControlC1TextBox(ref txtPaidType, fEdit, "txtPaidType", 120, lbtxtPaidType.Location.X + size.Width + 5, gapY);

            bc.setControlC1Button(ref btnImportDfSelect, fEdit, "ดึงข้อมูล", "btnSelect", txtPaidType.Location.X + txtPaidType.Width + 20, gapY);
            btnImportDfSelect.Width = 70;
            bc.setControlC1Button(ref btnImportDfGen, fEdit, "gen Text", "btnGen", btnImportDfSelect.Location.X + btnImportDfSelect.Width + 20, gapY);
            btnImportDfGen.Width = 80;

            tabImportDf.Controls.Add(grfSelect);
            theme1.SetTheme(grfSelect, "Office2010Red");

            tabImportDf.Controls.Add(lbDateStart);
            tabImportDf.Controls.Add(txtDateStart);
            tabImportDf.Controls.Add(lbDateEnd);
            tabImportDf.Controls.Add(txtDateEnd);
            tabImportDf.Controls.Add(lbtxtPaidType);
            tabImportDf.Controls.Add(txtPaidType);
            tabImportDf.Controls.Add(btnImportDfSelect);
            tabImportDf.Controls.Add(btnImportDfGen);

            tcMain.Controls.Add(tabImportDf);
            this.Controls.Add(lbLoading);
            this.Controls.Add(tcMain);
        }
        private void setControl()
        {
            txtDateStart.Value = DateTime.Now;
            txtDateEnd.Value = DateTime.Now;
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
        private void FrmDfDoctor_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            int scrH = Screen.PrimaryScreen.Bounds.Height;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Maximized;


            grfSelect.Size = new Size(scrW - 20, scrH - btnImportDfSelect.Location.Y - 140);
            grfSelect.Location = new Point(5, btnImportDfSelect.Location.Y + 40);

            Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();

            this.Text = "Last Update 2020-03-23";
        }
    }
}
