using bangna_hospital.control;
using bangna_hospital.Properties;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Themes;
using GrapeCity.ActiveReports.Document.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Row = C1.Win.C1FlexGrid.Row;

namespace bangna_hospital.gui
{
    public partial class FrmStock : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt, famt1, famt5, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        C1FlexGrid grfItem, grfOnhand, grfRecH, grfRecD, grfEndYear;
        C1ThemeController theme1;
        int colItemCode=1, colItemName=2, colItemOnhand=3, colItemUnit=4, colItemGrp=5, colItemTyp=6, colItemMin=7, colItemMax=8;
        int colOnhandDate = 1,colOnhandTime = 2, colOnhandStatus = 3, colOnhandQTY = 4, colOnhandRemark = 5;
        int colRecHRecDate = 1, colRecHReqNo = 2, colRecHStrCode = 3, colRecHFlagReq = 4, colRecHDeptCode = 5;
        int colRecDReqNo = 1, colRecDLine = 2, colRecDInvCode = 3, colRecDItemName=4, colRecDUnit = 5, colRecDQTY = 6;
        int colEndYearItemCode=1, colEndYearItemName=2,colEndYearItemOnhand=3, colEndYearItemAdjust = 4, colEndYearItemOnhandNew = 5, colEndYearItemUnit=6,colEndYearRemark=7, colEndYearItemEdit=8;
        String FlagDRUG = "", TABACTIVE="";
        Label lbLoading;
        Boolean pageLoad=false;
        public FrmStock(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            theme1 = new C1ThemeController();
            initGrfItem();
            initLoading();
            initGrfRecH();
            initGrfRecD();
            initGrfEndYear();
            bc.setCboYear(cboRecYear);
            bc.setCboYear(cboEndYearYear);
            FlagDRUG = "drug";
            if (FlagDRUG.Equals("drug")) { btnDrug.SmallImage = Resources.pngtree_pharmacy_logo_icon_vector_illustration_design_template_png_image_5655290; btnSupply.SmallImage = null; }
            if (FlagDRUG.Equals("drug")) { btnDrug.SmallImage = null; btnSupply.SmallImage = Resources.Ticket_24; }

            btnOnhandSearch.Click += BtnOnhandSearch_Click;
            tC.SelectedTabChanged += TC_SelectedTabChanged;
            cboRecYear.SelectedValueChanged += CboRecYear_SelectedValueChanged;
            cboEndYearYear.SelectedValueChanged += CboEndYearYear_SelectedValueChanged;
            btnEndStockSave.Click += BtnEndStockSave_Click;
            txtEndYearSearch.KeyPress += TxtEndYearSearch_KeyPress;
            btnEndYearUpdate.Click += BtnEndYearUpdate_Click;

            pageLoad = false;
        }

        private void BtnEndYearUpdate_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            foreach (Row rowa in grfEndYear.Rows)
            {
                if (rowa[colEndYearItemCode].ToString().Equals("code")) continue;
                if (rowa[colEndYearItemCode].ToString().Equals("")) continue;
                if (rowa[colEndYearItemEdit].ToString().Equals("1"))
                {
                    float onhandnew = 0;
                    onhandnew = (float)rowa[colEndYearItemOnhandNew];
                    String itemcode = (String)rowa[colEndYearItemCode];
                    String re = bc.bcDB.endyearDB.updateOnhandNew(itemcode, cboEndYearYear.Text, onhandnew);
                }
            }
        }

        private void TxtEndYearSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            grfEndYear.ApplySearch(txtEndYearSearch.Text.Trim(), true, true, false);
        }

        private void BtnEndStockSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            foreach(Row rowa in grfEndYear.Rows)
            {
                if (rowa[colEndYearItemCode].ToString().Equals("code")) continue;
                if (rowa[colEndYearItemCode].ToString().Equals("")) continue;
                if (rowa[colEndYearItemEdit].ToString().Equals("1"))
                {
                    float onhandnew = 0;
                    onhandnew = (float)rowa[colEndYearItemOnhandNew];
                    String itemcode = (String)rowa[colEndYearItemCode];
                    String re = bc.bcDB.endyearDB.updatePHQTY(itemcode, cboEndYearYear.Text, onhandnew);
                }
            }
        }

        private void CboEndYearYear_SelectedValueChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfEndYear(cboEndYearYear.Text);
        }

        private void CboRecYear_SelectedValueChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void initGrfEndYear()
        {
            grfEndYear = new C1FlexGrid();
            grfEndYear.Font = fEdit;
            grfEndYear.Dock = System.Windows.Forms.DockStyle.Fill;
            grfEndYear.Location = new System.Drawing.Point(0, 0);
            grfEndYear.Rows.Count = 1;
            grfEndYear.Cols.Count = 9;

            grfEndYear.Cols[colEndYearItemCode].Width = 90;
            grfEndYear.Cols[colEndYearItemName].Width = 400;
            grfEndYear.Cols[colEndYearItemOnhand].Width = 100;
            grfEndYear.Cols[colEndYearItemUnit].Width = 60;
            grfEndYear.Cols[colEndYearRemark].Width = 180;

            grfEndYear.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfEndYear.Cols[colEndYearItemCode].Caption = "code";
            grfEndYear.Cols[colEndYearItemName].Caption = "Name";
            grfEndYear.Cols[colEndYearItemOnhand].Caption = "Onhand";
            grfEndYear.Cols[colEndYearItemAdjust].Caption = "ปรับยอด";
            grfEndYear.Cols[colEndYearItemOnhandNew].Caption = "ยอดใหม่ที่นับได้";
            grfEndYear.Cols[colEndYearItemUnit].Caption = "unit";
            grfEndYear.Cols[colEndYearRemark].Caption = "remark";

            grfEndYear.Cols[colEndYearItemOnhand].DataType = typeof(float);
            grfEndYear.Cols[colEndYearItemAdjust].DataType = typeof(float);
            grfEndYear.Cols[colEndYearItemOnhandNew].DataType = typeof(float);
            grfEndYear.Cols[colEndYearItemUnit].DataType = typeof(String);
            grfEndYear.Cols[colEndYearItemCode].DataType = typeof(String);
            grfEndYear.Cols[colEndYearItemName].DataType = typeof(String);
            grfEndYear.Cols[colEndYearRemark].DataType = typeof(String);

            grfEndYear.Cols[colEndYearItemCode].AllowEditing = false;
            grfEndYear.Cols[colEndYearItemName].AllowEditing = false;
            grfEndYear.Cols[colEndYearItemOnhand].AllowEditing = false;
            grfEndYear.Cols[colEndYearItemUnit].AllowEditing = false;
            grfEndYear.Cols[colEndYearRemark].AllowEditing = false;
            grfEndYear.Cols[colEndYearItemOnhandNew].StyleNew.BackColor = ColorTranslator.FromHtml("#FFC5C5");

            grfEndYear.Cols[colEndYearItemEdit].Visible = false;
            grfEndYear.SelectionMode = SelectionModeEnum.Row;

            grfEndYear.AfterEdit += GrfEndYear_AfterEdit;

            pnEndYear.Controls.Add(grfEndYear);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfEndYear, bc.iniC.themegrfIpd);
        }

        private void GrfEndYear_AfterEdit(object sender, RowColEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfEndYear[grfEndYear.Row, colEndYearItemCode] == null) return;
            try
            {
                float onhand = 0, adj = 0, onhandnew = 0;
                if(grfEndYear.Col== colEndYearItemOnhandNew)
                {
                    onhand = grfEndYear[grfEndYear.Row, colEndYearItemOnhand]!=null?(float)grfEndYear[grfEndYear.Row, colEndYearItemOnhand]:0;
                    onhandnew = (float)grfEndYear[grfEndYear.Row, colEndYearItemOnhandNew];
                    if(onhandnew > onhand)
                    {
                        adj = onhandnew - onhand;
                    }
                    else
                    {
                        adj = onhandnew - onhand;
                    }
                    grfEndYear[grfEndYear.Row, colEndYearItemEdit] = "1";
                    grfEndYear[grfEndYear.Row, colEndYearItemAdjust] = adj;
                    //C1.Win.C1FlexGrid.Row rowa = grfEndYear.Rows[grfEndYear.Row];
                    grfEndYear.Rows[grfEndYear.Row].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                }
            }
            catch(Exception ex) 
            { 

            }

        }

        private void setGrfEndYear(String year)
        {
            showLbLoading();
            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dt = bc.bcDB.endyearDB.SelectByYear(year);
            int i = 1, j = 1, row = grfEndYear.Rows.Count;
            
            grfEndYear.Rows.Count = 1;
            grfEndYear.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfEndYear.Rows[i];

                rowa[colEndYearItemCode] = row1["MNC_PH_CD"].ToString();
                rowa[colEndYearItemName] = row1["MNC_PH_TN"].ToString();
                rowa[colEndYearItemOnhand] = row1["MNC_PH_QTY"].ToString();
                rowa[colEndYearItemUnit] = row1["MNC_UNT_CD"].ToString();
                rowa[colEndYearItemAdjust] = row1["adjust"].ToString();

                rowa[colEndYearItemOnhandNew] = row1["onhandnew"].ToString();

                rowa[colEndYearItemEdit] = "0";

                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initGrfRecH()
        {
            grfRecH = new C1FlexGrid();
            grfRecH.Font = fEdit;
            grfRecH.Dock = System.Windows.Forms.DockStyle.Fill;
            grfRecH.Location = new System.Drawing.Point(0, 0);
            grfRecH.Rows.Count = 1;
            grfRecH.Cols.Count = 6;

            grfRecH.Cols[colRecHRecDate].Width = 90;
            grfRecH.Cols[colRecHReqNo].Width = 80;
            grfRecH.Cols[colRecHStrCode].Width = 100;
            grfRecH.Cols[colRecHFlagReq].Width = 60;
            grfRecH.Cols[colRecHDeptCode].Width = 180;
            
            grfRecH.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfRecH.Cols[colRecHRecDate].Caption = "rec date";
            grfRecH.Cols[colRecHReqNo].Caption = "NO";
            grfRecH.Cols[colRecHStrCode].Caption = "STRCODE";
            grfRecH.Cols[colRecHFlagReq].Caption = "FLAGREQ";
            grfRecH.Cols[colRecHDeptCode].Caption = "dept";

            grfRecH.Cols[colRecHRecDate].AllowEditing = false;
            grfRecH.Cols[colRecHReqNo].AllowEditing = false;
            grfRecH.Cols[colRecHStrCode].AllowEditing = false;
            grfRecH.Cols[colRecHFlagReq].AllowEditing = false;
            grfRecH.Cols[colRecHDeptCode].AllowEditing = false;

            grfRecH.AfterRowColChange += GrfRecH_AfterRowColChange;

            pnRecH.Controls.Add(grfRecH);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfRecH, bc.iniC.themegrfIpd);
        }

        private void GrfRecH_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfRecH.Row <= 0) return;
            if (grfRecH[grfRecH.Row, colRecHReqNo] == null) return;
            setGrfRecD(grfRecH[grfRecH.Row, colRecHReqNo].ToString());
        }
        private void setGrfRecH()
        {
            showLbLoading();
            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dt = bc.bcDB.reqhDB.SelectBYYear(DateTime.Now.Year.ToString());
            int i = 1, j = 1, row = grfRecH.Rows.Count;
            //txtVN.Value = dt.Rows.Count;
            //txtName.Value = "";
            //txt.Value = "";
            grfRecH.Rows.Count = 1;
            grfRecH.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfRecH.Rows[i];

                rowa[colRecHRecDate] = row1["REQDate"].ToString();
                rowa[colRecHReqNo] = row1["REQNo"].ToString();
                rowa[colRecHStrCode] = row1["strCode"].ToString();
                rowa[colRecHFlagReq] = row1["FlagREQ"].ToString();
                rowa[colRecHDeptCode] = row1["DeptCode"].ToString();
                
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initGrfRecD()
        {
            grfRecD = new C1FlexGrid();
            grfRecD.Font = fEdit;
            grfRecD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfRecD.Location = new System.Drawing.Point(0, 0);
            grfRecD.Rows.Count = 1;
            grfRecD.Cols.Count = 7;

            grfRecD.Cols[colRecDReqNo].Width = 90;
            grfRecD.Cols[colRecDItemName].Width = 300;
            grfRecD.Cols[colRecDLine].Width = 80;
            grfRecD.Cols[colRecDInvCode].Width = 100;
            grfRecD.Cols[colRecDUnit].Width = 60;
            grfRecD.Cols[colRecDQTY].Width = 180;

            grfRecD.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfRecD.Cols[colRecDReqNo].Caption = "NO";
            grfRecD.Cols[colRecDLine].Caption = "line";
            grfRecD.Cols[colRecDItemName].Caption = "name";
            grfRecD.Cols[colRecDInvCode].Caption = "code";
            grfRecD.Cols[colRecDUnit].Caption = "unit";
            grfRecD.Cols[colRecDQTY].Caption = "QTY";

            grfRecD.Cols[colRecDReqNo].Visible = false;

            grfRecD.Cols[colRecDReqNo].AllowEditing = false;
            grfRecD.Cols[colRecDLine].AllowEditing = false;
            grfRecD.Cols[colRecDItemName].AllowEditing = false;
            grfRecD.Cols[colRecDInvCode].AllowEditing = false;
            grfRecD.Cols[colRecDUnit].AllowEditing = false;
            grfRecD.Cols[colRecDQTY].AllowEditing = false;

            pnRecD.Controls.Add(grfRecD);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfRecD, bc.iniC.themegrfIpd);
        }
        private void setGrfRecD(String recid)
        {
            showLbLoading();
            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dt = bc.bcDB.reqdDB.SelectByReqno(recid);
            int i = 1, j = 1, row = grfRecD.Rows.Count;
            //txtVN.Value = dt.Rows.Count;
            //txtName.Value = "";
            //txt.Value = "";
            grfRecD.Rows.Count = 1;
            grfRecD.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfRecD.Rows[i];

                rowa[colRecDReqNo] = row1["REQNo"].ToString();
                rowa[colRecDLine] = row1["Line"].ToString();
                rowa[colRecDInvCode] = row1["InvCode"].ToString();
                rowa[colRecDItemName] = row1["MNC_PH_TN"].ToString();
                rowa[colRecDUnit] = row1["InvUnitCode"].ToString();
                rowa[colRecDQTY] = row1["Qty"].ToString();

                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void TC_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TABACTIVE = ((C1DockingTab)sender).Name;
            if (tC.SelectedTab == tabOnHand)
            {
                
            }
            else if (tC.SelectedTab == tabRec)
            {
                setGrfRecH();
            }
            else if (tC.SelectedTab == tabEndYear)
            {
                setGrfEndYear(cboEndYearYear.Text);
            }
        }

        private void BtnOnhandSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void initGrfItem()
        {
            grfItem = new C1FlexGrid();
            grfItem.Font = fEdit;
            grfItem.Dock = System.Windows.Forms.DockStyle.Fill;
            grfItem.Location = new System.Drawing.Point(0, 0);
            grfItem.Rows.Count = 1;
            grfItem.Cols.Count = 11;

            grfItem.Cols[colItemCode].Width = 90;
            grfItem.Cols[colItemName].Width = 80;
            grfItem.Cols[colItemOnhand].Width = 170;
            grfItem.Cols[colItemUnit].Width = 100;
            grfItem.Cols[colItemGrp].Width = 60;
            grfItem.Cols[colItemTyp].Width = 180;
            grfItem.Cols[colItemMin].Width = 180;
            grfItem.Cols[colItemMax].Width = 180;
            grfItem.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfItem.Cols[colItemCode].Caption = "code";
            grfItem.Cols[colItemName].Caption = "name";
            grfItem.Cols[colItemOnhand].Caption = "onhand";
            grfItem.Cols[colItemUnit].Caption = "unit";
            grfItem.Cols[colItemGrp].Caption = "group";
            grfItem.Cols[colItemTyp].Caption = "type";
            grfItem.Cols[colItemMin].Caption = "min";
            grfItem.Cols[colItemMax].Caption = "max";

            grfItem.Cols[colItemCode].AllowEditing = false;
            grfItem.Cols[colItemName].AllowEditing = false;
            grfItem.Cols[colItemOnhand].AllowEditing = false;
            grfItem.Cols[colItemUnit].AllowEditing = false;
            grfItem.Cols[colItemGrp].AllowEditing = false;
            grfItem.Cols[colItemTyp].AllowEditing = false;
            grfItem.Cols[colItemMin].AllowEditing = false;
            grfItem.Cols[colItemMax].AllowEditing = false;

            grfItem.AfterRowColChange += GrfItem_AfterRowColChange;

            pnOnhand.Controls.Add(grfItem);

            //theme1.SetTheme(grfIPD, "ExpressionDark");
            theme1.SetTheme(grfItem, bc.iniC.themegrfIpd);
        }
        private void GrfItem_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (grfItem[grfItem.Row, colItemCode] == null) return;
            txtOnhandItemcode.Value = grfItem[grfItem.Row, colItemCode].ToString();
            lbOnhandItemName.Text = grfItem[grfItem.Row, colItemName].ToString();
        }
        private void setGrfItem()
        {
            showLbLoading();
            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dt = bc.bcDB.pharM01DB.SelectDrugAll1();
            int i = 1, j = 1, row = grfItem.Rows.Count;
            //txtVN.Value = dt.Rows.Count;
            //txtName.Value = "";
            //txt.Value = "";
            grfItem.Rows.Count = 1;
            grfItem.Rows.Count = dt.Rows.Count + 1;
            //pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                //pB1.Value++;
                Row rowa = grfItem.Rows[i];

                rowa[colItemCode] = row1["MNC_PH_CD"].ToString();
                rowa[colItemName] = row1["MNC_PH_TN"].ToString();
                rowa[colItemOnhand] = row1["onhand"].ToString();
                rowa[colItemUnit] = row1["MNC_PH_UNT_CD"].ToString();
                rowa[colItemGrp] = row1["MNC_PH_GRP_CD"].ToString();
                rowa[colItemTyp] = row1["MNC_PH_TYP_CD"].ToString();
                rowa[colItemMin] = row1["MNC_PH_MIN"].ToString();
                rowa[colItemMax] = row1["MNC_PH_MAX"].ToString();
                rowa[0] = i;
                i++;
            }
            hideLbLoading();
        }
        private void initLoading()
        {
            lbLoading = new Label();
            lbLoading.Font = fEdit5B;
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            this.Controls.Add(lbLoading);
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
        private void FrmStock_Load(object sender, EventArgs e)
        {
            System.Drawing.Rectangle screenRect = Screen.GetBounds(Bounds);
            lbLoading.Location = new Point((screenRect.Width / 2) - 100, (screenRect.Height / 2) - 300);
            lbLoading.Text = "กรุณารอซักครู่ ...";
            lbLoading.Hide();

            scOnhand.HeaderHeight = 0;
            scRec.HeaderHeight = 0;
            setGrfItem();
            setGrfEndYear(cboEndYearYear.Text);

            this.Text = "Last Update 2024-09-03";
        }
    }
}
