using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Framework;
using C1.Win.C1FlexGrid;
using C1.Win.C1Themes;
using C1.Win.C1Tile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C1.Win.C1Preview.Strings;
using ImageElement = C1.Win.C1Tile.ImageElement;
using TextElement = C1.Win.C1Tile.TextElement;

namespace bangna_hospital.gui
{
    public partial class FrmItemSearch : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt5, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB;
        C1ThemeController theme1;
        Image imgCorr, imgTran;
        Group grpDrug, grpDrugItems, grpLab, grpLabItems, grpXray, grpXrayItems, grpProcedure, grpProcedureItems;
        C1FlexGrid grfItems, grfSearch, grfSearchItems;

        C1TileControl topTilesDrug, itemTilesDrug,topTilesLab, itemTilesLab, topTilesXray, itemTilesXray, topTilesProcedure, itemTilesProcedure;
        PanelElement pnTopTilesLab, pnItemsTilesLab, pnTopTilesXray, pnTopTilesProcedure;
        ImageElement imgTopTilesLab, imgTopTilesXray, imgTopTilesProcedure;
        TextElement txtTopTilesLab, txtTopTilesXray, txtTopTilesProcedure;
        Template tempFolder;
        int colGrfItemsCode = 1, colGrfItemsName = 2, colGrfItemStatus = 3;
        int colGrfSearchItemsCode = 1, colGrfSearchItemsName = 2, colGrfSearchItemStatus = 3, colGrfSearchItemGrp = 3;
        String DEFAULTTAB = "";
        public DataTable DTLABGRPSEARCH, DTLABSEARCH, DTXRAYGRPSEARCH, DTXRAYSEARCH, DTPROCEDUREGRPSEARCH, DTPROCEDURESEARCH, DTDRUGGRPSEARCH, DTDRUGSEARCH;
        public FrmItemSearch(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        public FrmItemSearch(BangnaControl bc, String defaulttab)
        {
            this.bc = bc;
            this.DEFAULTTAB = defaulttab??"";
            InitializeComponent();
            initConfig();
        }
        public FrmItemSearch(BangnaControl bc, String defaulttab, DataTable dtlabsearch)
        {
            this.bc = bc;
            this.DEFAULTTAB = defaulttab ?? "";
            this.DTLABGRPSEARCH = dtlabsearch;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit2 = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit2B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);

            famt1 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 1, FontStyle.Regular);
            famt5 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 5, FontStyle.Regular);
            famt7B = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 7, FontStyle.Bold);
            famtB14 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 14, FontStyle.Bold);
            famtB30 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 30, FontStyle.Bold);
            ftotal = new Font(bc.iniC.pdfFontName, bc.pdfFontSize + 60, FontStyle.Bold);
            fPrnBil = new Font(bc.iniC.pdfFontName, bc.pdfFontSize, FontStyle.Regular);
            fque = new Font(bc.iniC.queFontName, bc.queFontSize + 3, FontStyle.Bold);
            fqueB = new Font(bc.iniC.queFontName, bc.queFontSize + 7, FontStyle.Bold);
            fEditS = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 2, FontStyle.Regular);
            fEditS1 = new Font(bc.iniC.pdfFontName, bc.pdfFontSize - 1, FontStyle.Regular);

            initControl();
            initGrfItems();
            initGrfSearch();
            initGrfSearchItems();

            btnClose.Click += BtnClose_Click;
            txtSearchItem.KeyUp += TxtSearchItem_KeyUp;
            txtSearchItem.KeyPress += TxtSearchItem_KeyPress;
            getGroupDrug();
            getGroupLab();
            getGroupXray();
            getGroupProcedure();
        }

        private void TxtSearchItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            //throw new NotImplementedException();
            
        }
        private void TxtSearchItem_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearchItem.Text.Trim().Length <= 0) return;
                setOrderItem();
                txtItemCode.Focus();
            }
            else if (txtSearchItem.Text.Trim().Length > 2)
            {
                DataTable dt = new DataTable();
                //dt = bc.bcDB.pharM01DB.SelectAllByName(txtSearch.Text);
                if (chkItemLab.Checked)
                {
                    dt = bc.bcDB.labM01DB.SelectAllBySearch(txtSearchItem.Text.Trim());
                }
                else if (chkItemXray.Checked)
                {
                    dt = bc.bcDB.xrayM01DB.SelectAllBySearch(txtSearchItem.Text.Trim());
                }
                else if (chkItemProcedure.Checked)
                {
                    dt = bc.bcDB.pm30DB.SelectAllBySearch(txtSearchItem.Text.Trim());
                }
                else if (chkItemDrug.Checked)
                {
                    //dt = bc.bcDB.pharM01DB.SelectNameByPk(txtSearchItem.Text.Trim());
                }
                grfSearchItems.Rows.Count = 1; grfSearchItems.Rows.Count = dt.Rows.Count + 1;
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    Row rowa = grfSearchItems.Rows[i]; rowa[0] = i;
                    //int i = grfSearch.Rows.Count;
                    rowa[colGrfItemsCode] = dr["code"].ToString();
                    rowa[colGrfItemsName] = dr["name"].ToString();
                    rowa[colGrfItemStatus] = (chkItemLab.Checked) ? "lab" : (chkItemXray.Checked) ? "xray" : (chkItemProcedure.Checked) ? "procedure" : (chkItemDrug.Checked) ? "drug" : "";
                    i++;
                }
            }
            else if (txtSearchItem.Text.Trim().Length <= 0)
            {
                grfSearchItems.Rows.Count = 1;
            }
        }
        private void setOrderItem()
        {
            String[] txt = txtSearchItem.Text.Split('#');
            if (txt.Length <= 1)
            {
                lfSbMessage.Text = "no item";
                txtItemCode.Value = "";
                lbItemName.Text = "";
                //txtItemQTY.Value = "1";
                return;
            }
            String name = txt[0].Trim();
            String code = txt[1].Trim();
            if (chkItemLab.Checked)
            {
                LabM01 lab = new LabM01();
                lab = bc.bcDB.labM01DB.SelectByPk(code);
                txtItemCode.Value = lab.MNC_LB_CD;
                lbItemName.Text = lab.MNC_LB_DSC;
                //txtItemQTY.Visible = false;
                //txtItemQTY.Value = "1";
            }
            else if (chkItemXray.Checked)
            {
                XrayM01 xray = new XrayM01();
                xray = bc.bcDB.xrayM01DB.SelectByPk(code);
                txtItemCode.Value = xray.MNC_XR_CD;
                lbItemName.Text = xray.MNC_XR_DSC;
                //txtItemQTY.Visible = false;
                //txtItemQTY.Value = "1";
            }
            else if (chkItemProcedure.Checked)
            {
                PatientM30 pm30 = new PatientM30();
                String name1 = bc.bcDB.pm30DB.SelectNameByPk(code);
                txtItemCode.Value = code;
                lbItemName.Text = name1;
                //txtItemQTY.Visible = false;
                //txtItemQTY.Value = "1";
            }
            else if (chkItemDrug.Checked)
            {
                PharmacyM01 drug = new PharmacyM01();
                String name1 = bc.bcDB.pharM01DB.SelectNameByPk(code);
                txtItemCode.Value = code;
                lbItemName.Text = name1;
                //txtItemQTY.Visible = false;
                //txtItemQTY.Value = "1";
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            List<Item> items = new List<Item>();
            if (grfItems.Rows.Count > 0)
            {
                foreach(Row rowa in grfItems.Rows)
                {
                    if (rowa.Index == 0) continue;
                    Item item = new Item();
                    item.code = rowa[colGrfItemsCode]?.ToString() ?? "";
                    item.name = rowa[colGrfItemsName]?.ToString() ?? "";
                    item.flag = rowa[colGrfItemStatus]?.ToString() ?? "";
                    item.qty = "1";
                    items.Add(item);
                }
            }
            bc.items = items;
            this.Dispose();
        }
        // ✅ สร้าง helper method
        private string GetGridCellValue(Row row, int columnIndex)
        {
            if (row == null || columnIndex < 0)
                return "";

            return row[columnIndex]?.ToString() ?? "";
        }
        private void initControl()
        {
            theme1 = new C1ThemeController();
            imgCorr = Resources.red_checkmark_png_16;
            imgTran = Resources.DeleteTable_small;

            topTilesDrug = new C1.Win.C1Tile.C1TileControl();
            itemTilesDrug = new C1.Win.C1Tile.C1TileControl();
            topTilesLab = new C1.Win.C1Tile.C1TileControl();
            itemTilesLab = new C1.Win.C1Tile.C1TileControl();
            pnTopTilesLab = new C1.Win.C1Tile.PanelElement();
            imgTopTilesLab = new C1.Win.C1Tile.ImageElement();
            txtTopTilesLab = new C1.Win.C1Tile.TextElement();

            topTilesXray = new C1.Win.C1Tile.C1TileControl();
            itemTilesXray = new C1.Win.C1Tile.C1TileControl();
            pnTopTilesXray = new C1.Win.C1Tile.PanelElement();
            imgTopTilesXray = new C1.Win.C1Tile.ImageElement();
            txtTopTilesXray = new C1.Win.C1Tile.TextElement();

            topTilesProcedure = new C1.Win.C1Tile.C1TileControl();
            itemTilesProcedure = new C1.Win.C1Tile.C1TileControl();
            pnTopTilesProcedure = new C1.Win.C1Tile.PanelElement();
            imgTopTilesProcedure = new C1.Win.C1Tile.ImageElement();
            txtTopTilesProcedure = new C1.Win.C1Tile.TextElement();

            tempFolder = new C1.Win.C1Tile.Template();
            grpDrug = new Group();
            grpDrugItems = new Group();
            grpLab = new Group();
            grpLabItems = new Group();
            grpXray = new Group();
            grpXrayItems = new Group();
            grpProcedure = new Group();
            grpProcedureItems = new Group();
            imgTopTilesLab.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            imgTopTilesXray.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            imgTopTilesProcedure.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);

            pnTopTilesLab.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            pnTopTilesLab.Children.Add(imgTopTilesLab);
            pnTopTilesLab.Children.Add(txtTopTilesLab);
            pnTopTilesLab.ChildSpacing = 2;
            pnTopTilesLab.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);

            pnTopTilesXray.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            pnTopTilesXray.Children.Add(imgTopTilesLab);
            pnTopTilesXray.Children.Add(txtTopTilesLab);
            pnTopTilesXray.ChildSpacing = 2;
            pnTopTilesXray.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);

            pnTopTilesProcedure.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            pnTopTilesProcedure.Children.Add(imgTopTilesLab);
            pnTopTilesProcedure.Children.Add(txtTopTilesLab);
            pnTopTilesProcedure.ChildSpacing = 2;
            pnTopTilesProcedure.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);

            pnTopDrug.Controls.Add(this.topTilesDrug);
            pnItemDrug.Controls.Add(this.itemTilesDrug);

            pnTopLab.Controls.Add(this.topTilesLab);
            pnItemLab.Controls.Add(this.itemTilesLab);

            pnTopXray.Controls.Add(this.topTilesXray);
            pnItemXray.Controls.Add(this.itemTilesXray);

            pnTopProcedure.Controls.Add(this.topTilesProcedure);
            pnItemProcedure.Controls.Add(this.itemTilesProcedure);

            this.topTilesDrug.AllowPanningFeedback = false;
            this.topTilesDrug.BackColor = System.Drawing.Color.Silver;
            this.topTilesDrug.CellHeight = 30;
            this.topTilesDrug.CellSpacing = 3;
            this.topTilesDrug.CellWidth = 96;
            this.topTilesDrug.DefaultTemplate.Elements.Add(pnTopTilesLab);
            this.topTilesDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topTilesDrug.FocusedBorderColor = System.Drawing.Color.ForestGreen;
            this.topTilesDrug.GroupPadding = new System.Windows.Forms.Padding(4);
            this.topTilesDrug.Groups.Add(grpDrug);
            this.topTilesDrug.HotBorderColor = System.Drawing.Color.White;
            this.topTilesDrug.Location = new System.Drawing.Point(0, 0);
            this.topTilesDrug.Name = "topTilesDrug";
            this.topTilesDrug.Padding = new System.Windows.Forms.Padding(0);
            this.topTilesDrug.Size = new System.Drawing.Size(1856, 48);
            this.topTilesDrug.SurfacePadding = new System.Windows.Forms.Padding(0);
            this.topTilesDrug.TabIndex = 1;
            this.topTilesDrug.TabStop = false;
            this.topTilesDrug.TileBackColor = System.Drawing.Color.DimGray;

            this.itemTilesDrug.AllowPanningFeedback = false;
            this.itemTilesDrug.BackColor = System.Drawing.Color.Silver;
            this.itemTilesDrug.CellHeight = 20;
            this.itemTilesDrug.CellSpacing = 1;
            this.itemTilesDrug.CellWidth = 120;
            this.itemTilesDrug.DefaultTemplate.Elements.Add(pnTopTilesLab);
            this.itemTilesDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemTilesDrug.FocusedBorderColor = System.Drawing.Color.ForestGreen;
            this.itemTilesDrug.GroupPadding = new System.Windows.Forms.Padding(4);
            this.itemTilesDrug.Groups.Add(grpDrugItems);
            this.itemTilesDrug.HotBorderColor = System.Drawing.Color.White;
            this.itemTilesDrug.Location = new System.Drawing.Point(0, 0);
            this.itemTilesDrug.Name = "itemTilesDrug";
            this.itemTilesDrug.Padding = new System.Windows.Forms.Padding(0);
            this.itemTilesDrug.Size = new System.Drawing.Size(1856, 48);
            this.itemTilesDrug.SurfacePadding = new System.Windows.Forms.Padding(0);
            this.itemTilesDrug.TabIndex = 1;
            this.itemTilesDrug.TabStop = false;
            this.itemTilesDrug.TileBackColor = System.Drawing.Color.DimGray;

            this.topTilesLab.AllowPanningFeedback = false;
            this.topTilesLab.BackColor = System.Drawing.Color.Silver;
            this.topTilesLab.CellHeight = 30;
            this.topTilesLab.CellSpacing = 6;
            this.topTilesLab.CellWidth = 96;
            this.topTilesLab.DefaultTemplate.Elements.Add(pnTopTilesLab);
            this.topTilesLab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topTilesLab.FocusedBorderColor = System.Drawing.Color.ForestGreen;
            this.topTilesLab.GroupPadding = new System.Windows.Forms.Padding(4);
            this.topTilesLab.Groups.Add(grpLab);
            this.topTilesLab.HotBorderColor = System.Drawing.Color.White;
            this.topTilesLab.Location = new System.Drawing.Point(0, 0);
            this.topTilesLab.Name = "topTilesLab";
            this.topTilesLab.Padding = new System.Windows.Forms.Padding(0);
            this.topTilesLab.Size = new System.Drawing.Size(1856, 48);
            this.topTilesLab.SurfacePadding = new System.Windows.Forms.Padding(0);
            this.topTilesLab.TabIndex = 1;
            this.topTilesLab.TabStop = false;
            this.topTilesLab.TileBackColor = System.Drawing.Color.DimGray;

            this.itemTilesLab.AllowPanningFeedback = false;
            this.itemTilesLab.BackColor = System.Drawing.Color.Silver;
            this.itemTilesLab.CellHeight = 20;
            this.itemTilesLab.CellSpacing = 2;
            this.itemTilesLab.CellWidth = 120;
            this.itemTilesLab.DefaultTemplate.Elements.Add(pnTopTilesLab);
            this.itemTilesLab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemTilesLab.FocusedBorderColor = System.Drawing.Color.ForestGreen;
            this.itemTilesLab.GroupPadding = new System.Windows.Forms.Padding(4);
            this.itemTilesLab.Groups.Add(grpLabItems);
            this.itemTilesLab.HotBorderColor = System.Drawing.Color.White;
            this.itemTilesLab.Location = new System.Drawing.Point(0, 0);
            this.itemTilesLab.Name = "itemTilesLab";
            this.itemTilesLab.Padding = new System.Windows.Forms.Padding(0);
            this.itemTilesLab.Size = new System.Drawing.Size(1856, 48);
            this.itemTilesLab.SurfacePadding = new System.Windows.Forms.Padding(0);
            this.itemTilesLab.TabIndex = 1;
            this.itemTilesLab.TabStop = false;
            this.itemTilesLab.TileBackColor = System.Drawing.Color.DimGray;

            this.topTilesXray.AllowPanningFeedback = false;
            this.topTilesXray.BackColor = System.Drawing.Color.Silver;
            this.topTilesXray.CellHeight = 30;
            this.topTilesXray.CellSpacing = 4;
            this.topTilesXray.CellWidth = 96;
            this.topTilesXray.DefaultTemplate.Elements.Add(pnTopTilesXray);
            this.topTilesXray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topTilesXray.FocusedBorderColor = System.Drawing.Color.ForestGreen;
            this.topTilesXray.GroupPadding = new System.Windows.Forms.Padding(4);
            this.topTilesXray.Groups.Add(grpXray);
            this.topTilesXray.HotBorderColor = System.Drawing.Color.White;
            this.topTilesXray.Location = new System.Drawing.Point(0, 0);
            this.topTilesXray.Name = "topTilesXray";
            this.topTilesXray.Padding = new System.Windows.Forms.Padding(0);
            this.topTilesXray.Size = new System.Drawing.Size(1856, 48);
            this.topTilesXray.SurfacePadding = new System.Windows.Forms.Padding(0);
            this.topTilesXray.TabIndex = 1;
            this.topTilesXray.TabStop = false;
            this.topTilesXray.TileBackColor = System.Drawing.Color.DimGray;

            this.itemTilesXray.AllowPanningFeedback = false;
            this.itemTilesXray.BackColor = System.Drawing.Color.Silver;
            this.itemTilesXray.CellHeight = 30;
            this.itemTilesXray.CellSpacing = 1;
            this.itemTilesXray.CellWidth = 120;
            this.itemTilesXray.DefaultTemplate.Elements.Add(pnTopTilesXray);
            this.itemTilesXray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemTilesXray.FocusedBorderColor = System.Drawing.Color.ForestGreen;
            this.itemTilesXray.GroupPadding = new System.Windows.Forms.Padding(4);
            this.itemTilesXray.Groups.Add(grpXrayItems);
            this.itemTilesXray.HotBorderColor = System.Drawing.Color.White;
            this.itemTilesXray.Location = new System.Drawing.Point(0, 0);
            this.itemTilesXray.Name = "itemTilesXray";
            this.itemTilesXray.Padding = new System.Windows.Forms.Padding(0);
            this.itemTilesXray.Size = new System.Drawing.Size(1856, 48);
            this.itemTilesXray.SurfacePadding = new System.Windows.Forms.Padding(0);
            this.itemTilesXray.TabIndex = 1;
            this.itemTilesXray.TabStop = false;
            this.itemTilesXray.TileBackColor = System.Drawing.Color.DimGray;

            this.topTilesProcedure.AllowPanningFeedback = false;
            this.topTilesProcedure.BackColor = System.Drawing.Color.Silver;
            this.topTilesProcedure.CellHeight = 20;
            this.topTilesProcedure.CellSpacing = 3;
            this.topTilesProcedure.CellWidth = 96;
            this.topTilesProcedure.DefaultTemplate.Elements.Add(pnTopTilesProcedure);
            this.topTilesProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topTilesProcedure.FocusedBorderColor = System.Drawing.Color.ForestGreen;
            this.topTilesProcedure.GroupPadding = new System.Windows.Forms.Padding(4);
            this.topTilesProcedure.Groups.Add(grpProcedure);
            this.topTilesProcedure.HotBorderColor = System.Drawing.Color.White;
            this.topTilesProcedure.Location = new System.Drawing.Point(0, 0);
            this.topTilesProcedure.Name = "topTilesProcedure";
            this.topTilesProcedure.Padding = new System.Windows.Forms.Padding(0);
            this.topTilesProcedure.Size = new System.Drawing.Size(1856, 48);
            this.topTilesProcedure.SurfacePadding = new System.Windows.Forms.Padding(0);
            this.topTilesProcedure.TabIndex = 1;
            this.topTilesProcedure.TabStop = false;
            this.topTilesProcedure.TileBackColor = System.Drawing.Color.DimGray;

            this.itemTilesProcedure.AllowPanningFeedback = false;
            this.itemTilesProcedure.BackColor = System.Drawing.Color.Silver;
            this.itemTilesProcedure.CellHeight = 30;
            this.itemTilesProcedure.CellSpacing = 1;
            this.itemTilesProcedure.CellWidth = 120;
            this.itemTilesProcedure.DefaultTemplate.Elements.Add(pnTopTilesProcedure);
            this.itemTilesProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemTilesProcedure.FocusedBorderColor = System.Drawing.Color.ForestGreen;
            this.itemTilesProcedure.GroupPadding = new System.Windows.Forms.Padding(4);
            this.itemTilesProcedure.Groups.Add(grpProcedureItems);
            this.itemTilesProcedure.HotBorderColor = System.Drawing.Color.White;
            this.itemTilesProcedure.Location = new System.Drawing.Point(0, 0);
            this.itemTilesProcedure.Name = "itemTilesProcedure";
            this.itemTilesProcedure.Padding = new System.Windows.Forms.Padding(0);
            this.itemTilesProcedure.Size = new System.Drawing.Size(1856, 48);
            this.itemTilesProcedure.SurfacePadding = new System.Windows.Forms.Padding(0);
            this.itemTilesProcedure.TabIndex = 1;
            this.itemTilesProcedure.TabStop = false;
            this.itemTilesProcedure.TileBackColor = System.Drawing.Color.DimGray;
        }
        private void initGrfSearchItems()
        {
            grfSearchItems = new C1FlexGrid();
            grfSearchItems.Font = fEdit;
            grfSearchItems.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSearchItems.Location = new System.Drawing.Point(0, 0);
            grfSearchItems.Rows.Count = 1;
            grfSearchItems.Cols.Count = 4;

            //grfitems.Cols.Count = 8;
            grfSearchItems.Cols[colGrfItemsCode].Caption = "code";
            grfSearchItems.Cols[colGrfItemsName].Caption = "name";
            grfSearchItems.Cols[colGrfItemStatus].Caption = "สถานะ";
            grfSearchItems.Cols[colGrfSearchItemGrp].Caption = "กลุ่ม";

            grfSearchItems.Cols[colGrfItemsCode].Width = 100;
            grfSearchItems.Cols[colGrfItemsName].Width = 500;
            grfSearchItems.Cols[colGrfItemStatus].Width = 70;
            grfSearchItems.Cols[colGrfSearchItemGrp].Width = 100;

            grfSearchItems.Cols[colGrfItemStatus].Visible = false;
            //grfitems.Cols[colGrfItemsCode].Visible = false;
            //grfitems.Cols[colGrfItemsCode].Visible = false;

            grfSearchItems.Cols[colGrfItemsCode].AllowEditing = false;
            grfSearchItems.Cols[colGrfItemsName].AllowEditing = false;
            grfSearchItems.Cols[colGrfItemStatus].AllowEditing = false;
            grfSearchItems.Cols[colGrfSearchItemGrp].AllowEditing = false;

            grfSearchItems.DoubleClick += GrfSearchItems_DoubleClick;

            pnSearchItems.Controls.Add(grfSearchItems);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfSearchItems, bc.iniC.themegrfOpd);
        }

        private void GrfSearchItems_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (((C1FlexGrid)sender).Row <= 0) return;
                if (((C1FlexGrid)sender).Col <= 0) return;
                String code = ((C1FlexGrid)sender).GetData(((C1FlexGrid)sender).Row, colGrfSearchItemsCode)?.ToString()??"";
                String name = ((C1FlexGrid)sender).GetData(((C1FlexGrid)sender).Row, colGrfSearchItemsName)?.ToString()??"";
                setGrfItem(code, name,chkItemLab.Checked ? "lab" : chkItemXray.Checked ? "xray" : chkItemProcedure.Checked ? "procedure":"drug");
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmScanView1 GrfSearchItems_DoubleClick " + ex.Message);
            }
        }

        private void initGrfSearch()
        {
            grfSearch = new C1FlexGrid();
            grfSearch.Font = fEdit;
            grfSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSearch.Location = new System.Drawing.Point(0, 0);
            grfSearch.Rows.Count = 1;
            grfSearch.Cols.Count = 4;

            //grfitems.Cols.Count = 8;
            grfSearch.Cols[colGrfItemsCode].Caption = "code";
            grfSearch.Cols[colGrfItemsName].Caption = "name";
            grfSearch.Cols[colGrfItemStatus].Caption = "สถานะ";

            grfSearch.Cols[colGrfItemsCode].Width = 100;
            grfSearch.Cols[colGrfItemsName].Width = 500;
            grfSearch.Cols[colGrfItemStatus].Width = 70;

            //grfitems.Cols[colGrfItemsCode].Visible = false;
            //grfitems.Cols[colGrfItemsCode].Visible = false;
            //grfitems.Cols[colGrfItemsCode].Visible = false;

            grfSearch.Cols[colGrfItemsCode].AllowEditing = false;
            grfSearch.Cols[colGrfItemsName].AllowEditing = false;
            grfSearch.Cols[colGrfItemStatus].AllowEditing = false;

            grfSearch.DoubleClick += GrfSearch_DoubleClick;

            pnItems.Controls.Add(grfSearch);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfSearch, bc.iniC.themegrfOpd);
        }

        private void GrfSearch_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void initGrfItems()
        {
            grfItems = new C1FlexGrid();
            grfItems.Font = fEdit;
            grfItems.Dock = System.Windows.Forms.DockStyle.Fill;
            grfItems.Location = new System.Drawing.Point(0, 0);
            grfItems.Rows.Count = 1;
            grfItems.Cols.Count = 4;

            //grfitems.Cols.Count = 8;
            grfItems.Cols[colGrfItemsCode].Caption = "code";
            grfItems.Cols[colGrfItemsName].Caption = "name";
            grfItems.Cols[colGrfItemStatus].Caption = "สถานะ";
            
            grfItems.Cols[colGrfItemsCode].Width = 100;
            grfItems.Cols[colGrfItemsName].Width = 500;
            grfItems.Cols[colGrfItemStatus].Width = 70;

            //grfitems.Cols[colGrfItemsCode].Visible = false;
            //grfitems.Cols[colGrfItemsCode].Visible = false;
            //grfitems.Cols[colGrfItemsCode].Visible = false;

            grfItems.Cols[colGrfItemsCode].AllowEditing = false;
            grfItems.Cols[colGrfItemsName].AllowEditing = false;
            grfItems.Cols[colGrfItemStatus].AllowEditing = false;

            grfItems.DoubleClick += Grfitems_DoubleClick;

            pnItems.Controls.Add(grfItems);

            //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grfItems, bc.iniC.themegrfOpd);
        }

        private void Grfitems_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((C1FlexGrid)sender).Row <= 0) return;
            if (((C1FlexGrid)sender).Col <= 0) return;
            grfItems.Rows.Remove(((C1FlexGrid)sender).Row);
        }

        private void setGrfItem(String code, String name, String flag)
        {
            //DataTable dt = new DataTable();
            try
            {
                Row rowa = grfItems.Rows.Add();
                int i = grfItems.Rows.Count;
                rowa[colGrfItemsCode] = code??"";
                rowa[colGrfItemsName] = name ?? "";
                rowa[colGrfItemStatus] = flag ?? "";
                rowa[0] = (i-1);
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmScanView1 setGrfLab grfLab " + ex.Message);
            }
        }
        private void getGroupProcedure()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.pm44DB.SelectAll();
            foreach (DataRow dr in dt.Rows)
            {
                Tile tilegrpproc = new Tile();
                tilegrpproc.HorizontalSize = 2;
                grpProcedure.Tiles.Add(tilegrpproc);
                //tilegrplab.Template = tempFolder;
                //tilegrplab.Image = imgCorr;
                tilegrpproc.Text = dr["MNC_SR_GRP_DSC"]?.ToString() ?? "";
                tilegrpproc.Text1 = dr["MNC_SR_GRP_CD"]?.ToString() ?? "";
                tilegrpproc.BackColor = Color.Coral;
                tilegrpproc.Tag = dr;
                tilegrpproc.Click += Tilegrpproc_Click;
            }
        }

        private void Tilegrpproc_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            lfSbMessage.Text = ((Tile)sender).Text + " " + ((Tile)sender).Text1;
            clearImageGrf(grpProcedure);
            ((Tile)sender).Image = imgCorr;
            getItemsProcedure(((Tile)sender).Text1);
        }
        private void getItemsProcedure(String grpcode)
        {
            grpProcedureItems.Tiles.Clear();
            DataTable dt = new DataTable();
            dt = bc.bcDB.pm30DB.SelectAllByGroup(grpcode);
            foreach (DataRow dr in dt.Rows)
            {
                Tile tileitemproc = new Tile();
                tileitemproc.HorizontalSize = 2;
                grpProcedureItems.Tiles.Add(tileitemproc);
                //tilegrplab.Template = tempFolder;
                tileitemproc.Image = null;
                tileitemproc.Text = dr["MNC_SR_DSC"]?.ToString() ?? "";
                tileitemproc.Text1 = dr["MNC_SR_CD"]?.ToString() ?? "";
                //tileitemproc.Text3 = dr["MNC_SR_TYP_CD"].ToString();
                tileitemproc.BackColor = Color.LightCoral;
                tileitemproc.Tag = dr;
                tileitemproc.Click += Tileitemproc_Click;
            }
        }

        private void Tileitemproc_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((Tile)sender).Image == null)
            {
                ((Tile)sender).Image = imgCorr;
            }
            else
            {
                ((Tile)sender).Image = null;
            }
            setGrfItem(((Tile)sender).Text1, ((Tile)sender).Text, "procedure");
        }

        private void getGroupXray()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.xraym05DB.SelectAll();
            foreach (DataRow dr in dt.Rows)
            {
                Tile tilegrpXray = new Tile();
                tilegrpXray.HorizontalSize = 2;
                grpXray.Tiles.Add(tilegrpXray);
                //tilegrplab.Template = tempFolder;
                //tilegrplab.Image = imgCorr;
                tilegrpXray.Text = dr["MNC_XR_GRP_DSC"]?.ToString() ?? "";
                tilegrpXray.Text1 = dr["MNC_XR_GRP_CD"]?.ToString() ?? "";
                tilegrpXray.BackColor = Color.SeaGreen;
                tilegrpXray.Tag = dr;
                tilegrpXray.Click += TilegrpXray_Click;
            }
        }

        private void TilegrpXray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            lfSbMessage.Text = ((Tile)sender).Text + " " + ((Tile)sender).Text1;
            clearImageGrf(grpXray);
            ((Tile)sender).Image = imgCorr;
            getItemsXray(((Tile)sender).Text1);
        }
        private void getItemsXray(String grpcode)
        {
            grpXrayItems.Tiles.Clear();
            DataTable dt = new DataTable();
            dt = bc.bcDB.xrayM01DB.SelectAllByGroup(grpcode);
            foreach (DataRow dr in dt.Rows)
            {
                Tile tileitemxray = new Tile();
                tileitemxray.HorizontalSize = 2;
                grpXrayItems.Tiles.Add(tileitemxray);
                //tilegrplab.Template = tempFolder;
                tileitemxray.Image = null;
                tileitemxray.Text = dr["MNC_XR_DSC"]?.ToString() ?? "";
                tileitemxray.Text1 = dr["MNC_XR_CD"]?.ToString() ?? "";
                tileitemxray.Text3 = dr["MNC_XR_TYP_CD"]?.ToString() ?? "";
                tileitemxray.BackColor = Color.SeaGreen;
                tileitemxray.Tag = dr;
                tileitemxray.Click += Tileitemxray_Click;
            }
        }
        private void Tileitemxray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((Tile)sender).Image == null)
            {
                ((Tile)sender).Image = imgCorr;
            }
            else
            {
                ((Tile)sender).Image = null;
            }
            setGrfItem(((Tile)sender).Text1, ((Tile)sender).Text, "xray");
        }
        private void getGroupDrug()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.pharM14DB.SelectAll();
            foreach (DataRow dr in dt.Rows)
            {
                Tile tilegrpdrug = new Tile();
                tilegrpdrug.HorizontalSize = 2;
                grpDrug.Tiles.Add(tilegrpdrug);
                //tilegrplab.Template = tempFolder;
                //tilegrplab.Image = imgCorr;
                tilegrpdrug.Text = dr["MNC_PH_GRP_DSC"]?.ToString() ?? "";
                tilegrpdrug.Text1 = dr["MNC_PH_GRP_CD"]?.ToString() ?? "";
                tilegrpdrug.BackColor = Color.Goldenrod;
                tilegrpdrug.Tag = dr;
                tilegrpdrug.Click += Tilegrpdrug_Click;
            }
        }

        private void Tilegrpdrug_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            lfSbMessage.Text = ((Tile)sender).Text + " " + ((Tile)sender).Text1;
            clearImageGrf(grpDrug);
            ((Tile)sender).Image = imgCorr;
            getItemsDrug(((Tile)sender).Text1);
        }

        private void getItemsDrug(String grpcode)
        {
            grpDrugItems.Tiles.Clear();
            DataTable dt = new DataTable();
            dt = bc.bcDB.pharM01DB.SelectAllByGroup(grpcode);
            foreach (DataRow dr in dt.Rows)
            {
                Tile tileitemdrug = new Tile();
                tileitemdrug.HorizontalSize = 2;
                grpDrugItems.Tiles.Add(tileitemdrug);
                //tilegrplab.Template = tempFolder;
                tileitemdrug.Image = null;
                tileitemdrug.Text = dr["MNC_PH_TN"]?.ToString() ?? "";
                tileitemdrug.Text1 = dr["MNC_PH_CD"]?.ToString() ?? "";
                //tileitemdrug.Text3 = dr["MNC_LB_TYP_CD"].ToString();
                tileitemdrug.BackColor = Color.IndianRed;
                tileitemdrug.Tag = dr;
                tileitemdrug.Click += Tileitemdrug_Click;
            }
        }

        private void Tileitemdrug_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (((Tile)sender).Image == null)
            {
                ((Tile)sender).Image = imgCorr;
            }
            else
            {
                ((Tile)sender).Image = null;
            }
            setGrfItem(((Tile)sender).Text1, ((Tile)sender).Text, "drug");
        }

        private void getGroupLab()
        {
            if((DTLABGRPSEARCH==null) || (DTLABGRPSEARCH.Rows.Count<=0))
            {
                DTLABGRPSEARCH = bc.bcDB.labm06DB.selectAll();
            }
            foreach (DataRow dr in DTLABGRPSEARCH.Rows)
            {
                Tile tilegrplab = new Tile();
                tilegrplab.HorizontalSize = 2;
                grpLab.Tiles.Add(tilegrplab);
                //tilegrplab.Template = tempFolder;
                //tilegrplab.Image = imgCorr;
                tilegrplab.Text = dr["MNC_LB_GRP_DSC"]?.ToString() ?? "";
                tilegrplab.Text1 = dr["MNC_LB_GRP_CD"]?.ToString() ?? "";
                tilegrplab.BackColor = Color.SteelBlue;
                tilegrplab.Tag = dr;
                tilegrplab.Click += Tilegrplab_Click;
            }
        }
        // ✅ Filter ใน memory แทนการ query
        private void getItemsLab(String grpcode)
        {
            grpLabItems.Tiles.Clear();
            //DataTable dt = new DataTable();
            if(DTLABSEARCH==null || DTLABSEARCH.Rows.Count<=0)
                DTLABSEARCH = bc.bcDB.labM01DB.SelectAll();
            // ✅ Filter ใน memory (ไม่ query DB)
            DataRow[] filteredRows = DTLABSEARCH.Select($"MNC_LB_GRP_CD = '{grpcode}'");
            foreach (DataRow dr in filteredRows)
            {
                Tile tileitemlab = new Tile();
                tileitemlab.HorizontalSize = 2;
                grpLabItems.Tiles.Add(tileitemlab);
                //tilegrplab.Template = tempFolder;
                tileitemlab.Image = null;
                tileitemlab.Text = dr["MNC_LB_DSC"]?.ToString() ?? "";
                tileitemlab.Text1 = dr["MNC_LB_CD"]?.ToString() ?? "";
                tileitemlab.Text3 = dr["MNC_LB_TYP_CD"]?.ToString() ?? "";
                tileitemlab.BackColor = Color.SteelBlue;
                tileitemlab.Tag = dr;
                tileitemlab.Click  += (sender, e) => 
                {
                    if (((Tile)sender).Image == null)   {       ((Tile)sender).Image = imgCorr;         }
                    else                {                        ((Tile)sender).Image = null;           }
                    setGrfItem(((Tile)sender).Text1, ((Tile)sender).Text, "lab");
                };
                //tileitemlab.Click += Tileitemlab_Click;
            }
        }
        private void Tilegrplab_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            lfSbMessage.Text = ((Tile)sender).Text+" "+ ((Tile)sender).Text1;
            clearImageGrf(grpLab);
            ((Tile)sender).Image = imgCorr;
            getItemsLab(((Tile)sender).Text1);
        }
        private void clearImageGrf(Group grp)
        {
            if (grp == null) return;  // ✅ เพิ่ม null check
            foreach (Tile tile in grp.Tiles)
            {
                tile.Image = null;
            }
        }
        private void FrmItemSearch_Load(object sender, EventArgs e)
        {
            if (DEFAULTTAB == "DRUG")
            {
                TC1.SelectedTab = tabDrug;
            }
            else if (DEFAULTTAB == "LAB")
            {
                TC1.SelectedTab = tabLab;
            }
            else if (DEFAULTTAB == "XRAY")
            {
                TC1.SelectedTab = tabXray;
            }
            else if (DEFAULTTAB == "PROCEDURE")
            {
                TC1.SelectedTab = tabProcedure;
            }
        }
    }
}
