using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using bangna_hospital.control;
using bangna_hospital.objdb;
using bangna_hospital.object1;
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
    public partial class FrmSearchHn : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;
        Color bg, fc;
        Font ff, ffB;
        int colCuHn = 1, colCuName = 2;
        int colVsName = 1, colVsDate=2, colVsVn=3, colVsPreNo=4, colVsmeno=5, colVsDsc=6, coLVsAn=8, colVsStatus=7, colVsAnDate=9, colVsHn=10, colDOB=11;

        C1FlexGrid grfCu, grfVn, grfDay5, grfDay6;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        public enum StatusConnection {host, hostEx};
        StatusConnection statusconn;
        public FrmSearchHn(BangnaControl bc, StatusConnection statusconn)
        {
            InitializeComponent();
            this.bc = bc;
            this.statusconn = statusconn;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            //C1ThemeController.ApplicationTheme = ic.iniC.themeApplication;
            theme1.Theme = bc.iniC.themeApplication;
            theme1.SetTheme(sB, "BeigeOne");

            sB1.Text = "";
            bg = txtSearch.BackColor;
            fc = txtSearch.ForeColor;
            ff = txtSearch.Font;

            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
            
            initGrfCu();
            initGrfVn();
            setGrfCu();
            tC1.SelectedTab = tabCurrent;
            btnSearch.Click += BtnSearch_Click;
            txtSearch.KeyUp += TxtSearch_KeyUp;
            btnOk.Click += BtnOk_Click;

            txtHn.TabStop = false;
            txtName.TabStop = false;
            txtVn.TabStop = false;
        }

        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (txtSearch.Text.Length >= bc.txtSearchHnLenghtStart)
            {
                setGrfCu();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //bc.sVsOld = new VisitOld();
            //bc.sVsOld.PName = txtName.Text;
            //bc.sVsOld.VN = txtVn.Text;
            //bc.sVsOld.PIDS = txtHn.Text;
            Close();
            //return true;
        }

        private void initGrfCu()
        {
            grfCu = new C1FlexGrid();
            grfCu.Font = fEdit;
            grfCu.Dock = System.Windows.Forms.DockStyle.Fill;
            grfCu.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            grfCu.AfterRowColChange += GrfCu_AfterRowColChange;
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);
            ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            grfCu.ContextMenu = menuGw;
            panel2.Controls.Add(grfCu);

            theme1.SetTheme(grfCu, "Office2010Blue");
        }
        private void initGrfVn()
        {
            grfVn = new C1FlexGrid();
            grfVn.Font = fEdit;
            grfVn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfVn.Location = new System.Drawing.Point(0, 0);
            grfVn.Rows.Count = 1;
            grfVn.Cols.Count = 11;
            //FilterRow fr = new FilterRow(grfExpn);

            grfVn.AfterRowColChange += GrfVn_AfterRowColChange;
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);
            ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            grfVn.ContextMenu = menuGw;
            panel3.Controls.Add(grfVn);

            theme1.SetTheme(grfVn, "Office2010Red");
        }

        private void GrfVn_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            String vn = "", hn="";
            vn = grfVn[grfVn.Row, colVsVn] != null ? grfVn[grfVn.Row, colVsVn].ToString() : "";
            hn = grfVn[grfVn.Row, colVsHn] != null ? grfVn[grfVn.Row, colVsHn].ToString() : "";
            txtVn.Value = vn;
            Patient ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinet(hn);
            txtHn.Value = ptt.Hn;
            txtName.Value = ptt.Name;
            txtVisitDate.Value = grfVn[grfVn.Row, colVsDate] != null ? grfVn[grfVn.Row, colVsDate].ToString() : "";
            txtPreNo.Value = grfVn[grfVn.Row, colVsPreNo] != null ? grfVn[grfVn.Row, colVsPreNo].ToString() : "";
            chkIPD.Checked = grfVn[grfVn.Row, colVsStatus] != null ? grfVn[grfVn.Row, colVsStatus].ToString().Equals("I") ? true : false : false;
            txtAn.Value = grfVn[grfVn.Row, coLVsAn] != null ? grfVn[grfVn.Row, coLVsAn].ToString() : "";
            txtAnDate.Value = grfVn[grfVn.Row, colVsAnDate] != null ? grfVn[grfVn.Row, colVsAnDate].ToString() : "";

            bc.sPtt = new Patient();
            bc.sPtt.Hn = txtHn.Text;
            bc.sPtt.Name = txtName.Text;
            bc.sPtt.vn = txtVn.Text;
            bc.sPtt.visitDate = txtVisitDate.Text;
            bc.sPtt.preno = txtPreNo.Text;
            bc.sPtt.statusIPD = chkIPD.Checked ? "I" : "O";
            bc.sPtt.anDate = txtAnDate.Text;
            bc.sPtt.an = txtAn.Text;
            bc.sPtt.patient_birthday = grfVn[grfVn.Row, colDOB] != null ? grfVn[grfVn.Row, colDOB].ToString() : "";
            bc.sPtt.dob = bc.sPtt.patient_birthday;
        }

        private void GrfCu_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            String hn = "";
            hn = grfCu[e.NewRange.r1, colCuHn] != null ? grfCu[e.NewRange.r1, colCuHn].ToString() : "";
            if (!hn.Equals(""))
            {
                bc.sPtt = new Patient();
                bc.sPtt.Hn = grfCu[grfCu.Row, colCuHn] != null ? grfCu[grfCu.Row, colCuHn].ToString() : "";
                bc.sPtt.Name = grfCu[grfCu.Row, colCuName] != null ? grfCu[grfCu.Row, colCuName].ToString() : "";
                //bc.sVsOld.PIDS = grfCu[grfCu.Row, colCuHn] != null ? grfCu[grfCu.Row, colCuHn].ToString() : "";

                txtHn.Value = bc.sPtt.Hn;
                txtName.Value = bc.sPtt.Name;
                setGrfVn(bc.sPtt.Hn);
                //txtVn.Value = bc.sVsOld.VN;
            }
            //grfAddr.DataSource = xC.iniDB.addrDB.selectByTableId1(vn);
        }

        private void setGrfCu()
        {
            //grfDept.Rows.Count = 7;
            grfCu.Clear();
            DataTable dt = new DataTable();
            grfCu.DataSource = null;
            //ConnectDB con = new ConnectDB(bc.iniC);
            //con.OpenConnectionEx();
            dt = bc.bcDB.pttDB.selectPatient(txtSearch.Text, txtSearch.Text);
            //con.CloseConnectionEx();
            //grfExpn.Rows.Count = dt.Rows.Count + 1;
            grfCu.Rows.Count = 1;
            grfCu.Cols.Count = 3;
            C1TextBox txt = new C1TextBox();
            C1ComboBox cboproce = new C1ComboBox();
            //ic.ivfDB.itmDB.setCboItem(cboproce);
            grfCu.Cols[colCuHn].Editor = txt;            
            grfCu.Cols[colCuName].Editor = txt;

            grfCu.Cols[colCuHn].Width = 70;            
            grfCu.Cols[colCuName].Width = 240;            
            //grfCu.Cols[colCuTime].Width = 80;

            grfCu.ShowCursor = true;
            //grdFlex.Cols[colID].Caption = "no";
            //grfDept.Cols[colCode].Caption = "รหัส";

            grfCu.Cols[colCuHn].Caption = "HN";
            grfCu.Cols[colCuName].Caption = "Name";

            Color color = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            //CellRange rg1 = grfBank.GetCellRange(1, colE, grfBank.Rows.Count, colE);
            //rg1.Style = grfBank.Styles["date"];
            //grfCu.Cols[colID].Visible = false;
            for (int i = 0; i <= dt.Rows.Count-1; i++)
            {
                Row row = grfCu.Rows.Add();
                row[0] = (i+1);
                
                row[colCuHn] = dt.Rows[i]["MNC_HN_NO"].ToString();
                row[colCuName] = dt.Rows[i]["prefix"].ToString()+" "+ dt.Rows[i]["MNC_FNAME_T"].ToString()+" "+ dt.Rows[i]["MNC_LNAME_T"].ToString();
                //row[colCuDate] =  bc.datetoShow(dt.Rows[i]["VDate"].ToString());
                
            }
            grfCu.Cols[colCuHn].AllowEditing = false;            
            grfCu.Cols[colCuName].AllowEditing = false;
            if (dt.Rows.Count == 1)
            {
                if(grfCu[1, colCuHn]!=null)
                {
                    setGrfVn(grfCu[1, colCuHn].ToString());
                }
            }
        }
        private void setGrfVn(String hn)
        {
            //grfDept.Rows.Count = 7;
            grfVn.Clear();
            DataTable dt = new DataTable();
            grfVn.DataSource = null;
            //ConnectDB con = new ConnectDB(bc.iniC);
            //con.OpenConnectionEx();
            dt = bc.bcDB.vsDB.selectVisitByHn3(hn);
            //con.CloseConnectionEx();
            //grfExpn.Rows.Count = dt.Rows.Count + 1;
            grfVn.Rows.Count = 1;
            grfVn.Cols.Count = 12;
            C1TextBox txt = new C1TextBox();
            C1ComboBox cboproce = new C1ComboBox();
            //ic.ivfDB.itmDB.setCboItem(cboproce);
            grfVn.Cols[colVsName].Editor = txt;
            grfVn.Cols[colVsDate].Editor = txt;

            grfVn.Cols[colVsName].Width = 140;
            grfVn.Cols[colVsDate].Width = 120;
            grfVn.Cols[colVsVn].Width = 80;
            grfVn.Cols[colVsmeno].Width = 140;
            grfVn.Cols[colVsDsc].Width = 80;
            grfVn.Cols[coLVsAn].Width = 80;
            grfVn.Cols[colVsStatus].Width = 40;
            grfVn.Cols[colVsAnDate].Width = 120;

            grfVn.ShowCursor = true;
            //grdFlex.Cols[colID].Caption = "no";
            //grfDept.Cols[colCode].Caption = "รหัส";

            grfVn.Cols[colVsName].Caption = "HN";
            grfVn.Cols[colVsDate].Caption = "Visit Date";
            grfVn.Cols[colVsVn].Caption = "VN";
            grfVn.Cols[colVsmeno].Caption = "อาการ";
            grfVn.Cols[colVsDsc].Caption = "desc";
            grfVn.Cols[coLVsAn].Caption = "AN";
            grfVn.Cols[colVsStatus].Caption = " ";
            grfVn.Cols[colVsAnDate].Caption = "AN Date";

            Color color = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
            //CellRange rg1 = grfBank.GetCellRange(1, colE, grfBank.Rows.Count, colE);
            //rg1.Style = grfBank.Styles["date"];
            //grfCu.Cols[colID].Visible = false;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Row row = grfVn.Rows.Add();
                row[0] = i;

                row[colVsVn] = dt.Rows[i]["MNC_VN_NO"].ToString()+"/"+ dt.Rows[i]["MNC_VN_SEQ"].ToString() + "(" + dt.Rows[i]["MNC_VN_SUM"].ToString()+")";
                row[colVsName] = dt.Rows[i]["prefix"].ToString() + " " + dt.Rows[i]["MNC_FNAME_T"].ToString() + " " + dt.Rows[i]["MNC_LNAME_T"].ToString();
                row[colVsDate] =  bc.datetoShow(dt.Rows[i]["mnc_date"].ToString());
                row[colVsPreNo] = dt.Rows[i]["mnc_pre_no"].ToString();
                row[colVsmeno] = dt.Rows[i]["mnc_shif_memo"].ToString();
                row[colVsDsc] = dt.Rows[i]["mnc_ref_dsc"].ToString();
                row[coLVsAn] = dt.Rows[i]["mnc_an_no"].ToString()+"/"+ dt.Rows[i]["mnc_an_yr"].ToString();
                row[colVsStatus] = dt.Rows[i]["MNC_PAT_FLAG"].ToString();
                row[colVsAnDate] = bc.datetoShow(dt.Rows[i]["mnc_ad_date"].ToString());
                row[colVsHn] = dt.Rows[i]["mnc_hn_no"].ToString();
                row[colDOB] = dt.Rows[i]["MNC_bday"].ToString();

            }
            grfVn.Cols[colVsVn].AllowEditing = false;
            grfVn.Cols[colCuName].AllowEditing = false;
            grfVn.Cols[colVsPreNo].Visible = false;
            grfVn.Cols[colVsName].Visible = false;
            grfVn.Cols[colVsHn].Visible = false;
            grfVn.Cols[colDOB].Visible = false;
        }
        private void FrmSearchHn_Load(object sender, EventArgs e)
        {
            tC1.SelectedTab = tabSearch;
            txtSearch.Focus();
        }
    }
}
