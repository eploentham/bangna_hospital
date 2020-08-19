using bangna_hospital.control;
using bangna_hospital.FlexGrid;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmSearchStaff:Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEditBig;

        C1FlexGrid grfStf;

        Panel pnOrdSearchDrug;

        C1ThemeController theme1;
        int colStfId = 1, colStfName = 2;
        public FrmSearchStaff(BangnaControl bc)
        {
            this.bc = bc;

            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            theme1 = new C1ThemeController();

            initConfig();
            
        }
        private void initConfig()
        {
            InitComponent();
        }
        private void InitComponent()
        {
            pnOrdSearchDrug = new Panel();
            pnOrdSearchDrug.Dock = DockStyle.Fill;

            this.Size = new Size(550, 600);

            initGrfStf();
            setGrfStf();
            this.Controls.Add(pnOrdSearchDrug);
        }
        private void initGrfStf()
        {
            grfStf = new C1FlexGrid();
            grfStf.Font = fEdit;
            grfStf.Dock = System.Windows.Forms.DockStyle.Fill;
            grfStf.Location = new System.Drawing.Point(0, 0);
            grfStf.Rows.Count = 1;
            grfStf.DoubleClick += GrfStf_DoubleClick;
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnOrdSearchDrug.Controls.Add(grfStf);

            theme1.SetTheme(grfStf, "Office2010Red");
        }

        private void GrfStf_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfStf.Row == null) return;
            if (grfStf.Row <= 0) return;
            if (grfStf.Col <= 0) return;
            bc.sStf.staff_id = grfStf[grfStf.Row,colStfId].ToString();
            bc.sStf.fullname = grfStf[grfStf.Row, colStfName].ToString();
            bc.sStf.username = grfStf[grfStf.Row, colStfId].ToString();
            Close();
            this.Hide();
            this.Dispose();
        }

        private void setGrfStf()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.stfDB.selectByLevel("'5','6'");

            grfStf.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfStf.Rows.Count = dt.Rows.Count + 1;
            grfStf.Cols.Count = dt.Columns.Count + 1;
            grfStf.Cols[colStfId].Caption = "ID";
            grfStf.Cols[colStfName].Caption = "Name";
            
            grfStf.Cols[colStfName].Width = 350;
            grfStf.Cols[colStfId].Width = 100;
            
            int i = 0;
            decimal aaa = 0;
            for (int col = 0; col < dt.Columns.Count; ++col)
            {
                grfStf.Cols[col + 1].DataType = dt.Columns[col].DataType;
                //grfOrdDrug.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
                grfStf.Cols[col + 1].Name = dt.Columns[col].ColumnName;
            }
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                if (i == 1) continue;
                grfStf[i, colStfId] = row1["MNC_USR_NAME"].ToString();
                grfStf[i, colStfName] = row1["MNC_USR_FULL"].ToString();
                
                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfStf);
            //grfOrdDrug.Cols[colXrayResult].Visible = false;
            grfStf.Cols[colStfId].AllowEditing = false;
            grfStf.Cols[colStfName].AllowEditing = false;
            
            FilterRow fr = new FilterRow(grfStf);
            grfStf.AllowFiltering = true;
            grfStf.AfterFilter += GrfStf_AfterFilter;
            //}).Start();
        }
        private void GrfStf_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            for (int col = grfStf.Cols.Fixed; col < grfStf.Cols.Count; ++col)
            {
                var filter = grfStf.Cols[col].ActiveFilter;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // ...
            if (keyData == (Keys.Escape))
            {
                //appExit();
                //if (MessageBox.Show("ต้องการออกจากโปรแกรม1", "ออกจากโปรแกรม", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                //{
                //frmmain.Show();
                Close();
                //    return true;
                //}
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
