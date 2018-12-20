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

        C1FlexGrid grfCu, grfDay3, grfDay5, grfDay6;
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
            if (txtSearch.Text.Length >= 4)
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
            gbHn.Controls.Add(grfCu);

            theme1.SetTheme(grfCu, "Office2010Blue");
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
            ConnectDB con = new ConnectDB(bc.iniC);
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

            grfCu.Cols[colCuHn].Width = 100;            
            grfCu.Cols[colCuName].Width = 280;            
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
                row[0] = i;
                
                row[colCuHn] = dt.Rows[i]["MNC_HN_NO"].ToString();
                row[colCuName] = dt.Rows[i]["prefix"].ToString()+" "+ dt.Rows[i]["MNC_FNAME_T"].ToString()+" "+ dt.Rows[i]["MNC_LNAME_T"].ToString();
                //row[colCuDate] =  bc.datetoShow(dt.Rows[i]["VDate"].ToString());
                
            }
            grfCu.Cols[colCuHn].AllowEditing = false;            
            grfCu.Cols[colCuName].AllowEditing = false;            
        }
        private void FrmSearchHn_Load(object sender, EventArgs e)
        {
            tC1.SelectedTab = tabSearch;
            txtSearch.Focus();
        }
    }
}
