using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmScanCheck : Form
    {
        BangnaControl bc;
        MainMenu menu;

        C1FlexGrid grfHn;
        Font fEdit, fEditB, fEditBig;
        int colHN = 1, colFullName = 2, colVN = 3, colAN = 4, colVsDate = 5, colCrDate = 6, colRowCnt = 7, colPicBfS = 8, colCnt=9;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmScanCheck(BangnaControl bc, MainMenu m)
        {
            InitializeComponent();
            this.bc = bc;
            this.menu = m;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);

            btnOk.Click += BtnOk_Click;
            theme1.SetTheme(sb1, bc.iniC.themeApplication);
            theme1.SetTheme(panel1, bc.iniC.themeApplication);
            theme1.SetTheme(panel2, bc.iniC.themeApplication);
            
            foreach (Control con in panel1.Controls)
            {
                theme1.SetTheme(con, bc.iniC.themeApplication);
            }
            foreach (Control con in panel2.Controls)
            {
                theme1.SetTheme(con, bc.iniC.themeApplication);
            }
            foreach (Control con in panel3.Controls)
            {
                theme1.SetTheme(con, bc.iniC.themeApplication);
            }
            initGrf();
            setGrf();
        }
        private void BtnOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrf();
        }
        private void initGrf()
        {
            grfHn = new C1FlexGrid();
            grfHn.Font = fEdit;
            grfHn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHn.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            grfHn.DoubleClick += GrfHn_DoubleClick;
            //grfHn.Click += GrfHn_Click;

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            panel2.Controls.Add(grfHn);
            grfHn.Rows[0].Visible = false;
            grfHn.Cols[0].Visible = false;
            theme1.SetTheme(grfHn, bc.iniC.themeDonor);

        }

        private void GrfHn_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfHn.Row <= 0) return;
            if (grfHn.Col <= 0) return;
            String vn = "", hn = "", vsdate = "", txt = "";
            vn = grfHn[grfHn.Row, colVN] != null ? grfHn[grfHn.Row, colVN].ToString() : "";
            hn = grfHn[grfHn.Row, colHN] != null ? grfHn[grfHn.Row, colHN].ToString() : "";
            txt = grfHn[grfHn.Row, colFullName] != null ? grfHn[grfHn.Row, colFullName].ToString() : "";
            openNewForm(hn, txt);
        }
        private void openNewForm(String hn, String txt)
        {
            FrmScanView1 frm = new FrmScanView1(bc, hn);
            frm.FormBorderStyle = FormBorderStyle.None;
            
            frm.FormBorderStyle = FormBorderStyle.None;
            menu.AddNewTab(frm, txt);
        }
        private void setGrf()
        {
            String datestart = "", dateend = "", hn = "", txt = "";
            DataTable dt = new DataTable();
            datestart = bc.datetoDB(txtDateStart.Text);
            dateend = bc.datetoDB(txtDateEnd.Text);
            dt = bc.bcDB.dscDB.selectDistByDateCrate(datestart, dateend);
            grfHn.Clear();
            grfHn.Rows.Count = 1;
            //grfQue.Rows.Count = 1;
            grfHn.Cols.Count = 10;
            grfHn.Cols[colHN].Caption = "HN";
            grfHn.Cols[colFullName].Caption = "Patient FullName";
            grfHn.Cols[colVN].Caption = "VN";
            grfHn.Cols[colAN].Caption = "AN";
            grfHn.Cols[colVsDate].Caption = "vs Date";
            grfHn.Cols[colCrDate].Caption = "scan Date";
            grfHn.Cols[colRowCnt].Caption = "row cnt";
            grfHn.Cols[colPicBfS].Caption = "pic BfS";
            grfHn.Cols[colCnt].Caption = "CNT";
            grfHn.Cols[colHN].Width = 100;
            grfHn.Cols[colFullName].Width = 300;
            grfHn.Cols[colVN].Width = 80;
            grfHn.Cols[colAN].Width = 80;
            grfHn.Cols[colVsDate].Width = 100;
            grfHn.Cols[colCrDate].Width = 100;
            grfHn.Cols[colRowCnt].Width = 80;
            grfHn.Cols[colPicBfS].Width = 80;
            grfHn.ShowCursor = true;
            ContextMenu menuGw = new ContextMenu();
            grfHn.ContextMenu = menuGw;
            int i = 1;
            grfHn.Rows.Count = dt.Rows.Count + 1;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    grfHn[i, 0] = (i);
                    grfHn[i, colHN] = row["hn"].ToString();
                    grfHn[i, colFullName] = row["prefix"].ToString() + " " + row["MNC_FNAME_T"].ToString() + " " + row["MNC_LNAME_T"].ToString();
                    grfHn[i, colVN] = row["vn"].ToString();
                    grfHn[i, colAN] = row["an"].ToString();
                    grfHn[i, colVsDate] = bc.datetoShow(row["visit_date"].ToString());
                    grfHn[i, colCrDate] = bc.datetoShow(row["date_create"].ToString());
                    grfHn[i, colRowCnt] = row["row_cnt"].ToString();
                    grfHn[i, colPicBfS] = row["pic_before_scan_cnt"].ToString();
                    grfHn[i, colCnt] = row["cnt"].ToString();
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "FrmLabOutReceiveView setGrf ex " + ex.Message);
                }
                i++;
            }
            grfHn.Cols[colHN].AllowEditing = false;
            grfHn.Cols[colFullName].AllowEditing = false;
            grfHn.Cols[colVN].AllowEditing = false;
            grfHn.Cols[colAN].AllowEditing = false;
            grfHn.Cols[colVsDate].AllowEditing = false;
            grfHn.Cols[colCrDate].AllowEditing = false;
            grfHn.Cols[colRowCnt].AllowEditing = false;
            grfHn.Cols[colPicBfS].AllowEditing = false;
        }
        private void FrmScanCheck_Load(object sender, EventArgs e)
        {

        }
    }
}
