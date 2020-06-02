using bangna_hospital.control;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SplitContainer;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmDoctorDrugSet:Form
    {
        BangnaControl bc;
        C1FlexGrid grfView, grfItem, grfDrug, grfSup, grfLab, grfXray, grfCopy, grfCopyItem, grfAddNewItem;
        C1DockingTab tC1, tcAddNewItemGrf;
        C1DockingTabPage tabView, tabCopy, tabAdd, tabReMed, tabDrug, tabSup, tabLab, tabXray;
        Label lbDoctor, lbItmId, lbItmName, lbItmQty, lbItmFre, lbItmIn1, lbItmIn2, lbItmNewId, lbItmNewName, lbItmNewQty, lbItmNewFre, lbItmNewIn1, lbItmNewIn2, lbDrugSetName;
        C1TextBox txtItmId, txtItmName, txtItmQty, txtItmFre, txtItmIn1, txtItmIn2, txtItmNewId, txtItmNewName, txtItmNewQty, txtItmNewFre, txtItmNewIn1, txtItmNewIn2, txtDrugSetName, txtDrugSetId;
        C1Button btnHnSearch;
        C1ComboBox cboDoctor;
        Panel pnCopyAddView, pnCopyAddItem, pnCopyAdd, pnCopyCtl, pnView, pnCopy, pnAdd, pnViewLeft, pnViewItem, pnCopyCtlCtl, pnCopyCtlCtlItem, pnAddNew, pnAddNewItemGrf, pnAddNewItem;
        C1ThemeController theme1;
        C1SplitterPanel scView, scViewItem, scCopy, scCopyCtl, scCopyAddView, scCopyAddItem, scCopyCtlCtl, scCopyCtlCtlItem, scAddNew, scAddNewItemGrf, scAddNewItem, scAddNewItmGrf, scAddNewItm;
        C1SplitContainer sCView, sCCopy, sCCopyAdd, sCCopyCtlCtl, sCAddNew, sCAddNewItm;

        Font fEdit, fEditB, fEditBig;
        Size size = new Size();
        public FrmDoctorDrugSet(BangnaControl bc)
        {
            this.bc = bc;
            initComponent();
            initComponentpnCopyCtlCtl();
            initComponentpnCopyCtlCtlItem();
            initComponentpnAddNewItem();
            initGrfCopy();
            initGrfCopyItem();
            initConfig();
        }
        private void initComponent()
        {
            int gapLine = 20, gapX = 20;
            
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 7, FontStyle.Regular);

            theme1 = new C1ThemeController();
            theme1.Theme = bc.iniC.themeApplication;

            tC1 = new C1DockingTab();
            tcAddNewItemGrf = new C1DockingTab();
            tabView = new C1DockingTabPage();
            tabCopy = new C1DockingTabPage();
            tabAdd = new C1DockingTabPage();
            tabDrug = new C1DockingTabPage();
            tabSup = new C1DockingTabPage();
            tabLab = new C1DockingTabPage();
            tabXray = new C1DockingTabPage();
            tabReMed = new C1DockingTabPage();
            pnView = new Panel();
            pnCopy = new Panel();
            pnAdd = new Panel();
            pnCopyCtl = new Panel();
            pnCopyAdd = new Panel();
            pnViewLeft = new Panel();
            pnViewItem = new Panel();
            pnCopyAddItem = new Panel();
            pnCopyAddView = new Panel();
            pnCopyCtlCtl = new Panel();
            pnCopyCtlCtlItem = new Panel();
            pnAddNew = new Panel();
            pnAddNewItemGrf = new Panel();
            pnAddNewItem = new Panel();
            scView = new C1SplitterPanel();
            scViewItem = new C1SplitterPanel();
            sCView = new C1SplitContainer();
            scCopy = new C1SplitterPanel();
            scCopyCtl = new C1SplitterPanel();
            sCCopy = new C1SplitContainer();
            scCopyAddView = new C1SplitterPanel();
            scCopyAddItem = new C1SplitterPanel();
            sCCopyAdd = new C1SplitContainer();
            scCopyCtlCtlItem = new C1SplitterPanel();
            scCopyCtlCtl = new C1SplitterPanel();
            sCCopyCtlCtl = new C1SplitContainer();
            scAddNew = new C1SplitterPanel();
            scAddNewItemGrf = new C1SplitterPanel();
            scAddNewItem = new C1SplitterPanel();
            sCAddNew = new C1SplitContainer();
            scAddNewItmGrf = new C1SplitterPanel();
            scAddNewItm = new C1SplitterPanel();
            sCAddNewItm = new C1SplitContainer();

            tC1.SuspendLayout();
            tcAddNewItemGrf.SuspendLayout();
            tabDrug.SuspendLayout();
            tabSup.SuspendLayout();
            tabLab.SuspendLayout();
            tabXray.SuspendLayout();
            tabView.SuspendLayout();
            tabCopy.SuspendLayout();
            tabAdd.SuspendLayout();
            tabReMed.SuspendLayout();
            pnView.SuspendLayout();
            pnCopy.SuspendLayout();
            pnAdd.SuspendLayout();
            pnCopyCtl.SuspendLayout();
            pnCopyAdd.SuspendLayout();
            pnViewLeft.SuspendLayout();
            pnViewItem.SuspendLayout();
            pnCopyCtlCtl.SuspendLayout();
            pnCopyCtlCtlItem.SuspendLayout();
            pnAddNew.SuspendLayout();
            pnAddNewItemGrf.SuspendLayout();
            pnAddNewItem.SuspendLayout();
            sCView.SuspendLayout();
            scView.SuspendLayout();
            scViewItem.SuspendLayout();
            sCCopy.SuspendLayout();
            scCopy.SuspendLayout();
            scCopyCtl.SuspendLayout();
            sCCopyAdd.SuspendLayout();
            scCopyAddView.SuspendLayout();
            scCopyAddItem.SuspendLayout();
            sCCopyCtlCtl.SuspendLayout();
            scCopyCtlCtl.SuspendLayout();
            scCopyCtlCtlItem.SuspendLayout();
            sCAddNew.SuspendLayout();
            scAddNew.SuspendLayout();
            scAddNewItemGrf.SuspendLayout();
            scAddNewItem.SuspendLayout();
            sCAddNewItm.SuspendLayout();
            scAddNewItm.SuspendLayout();
            scAddNewItmGrf.SuspendLayout();

            pnView.Dock = DockStyle.Fill;
            pnCopy.Dock = DockStyle.Fill;
            pnAdd.Dock = DockStyle.Fill;
            pnViewLeft.Dock = DockStyle.Fill;
            pnViewItem.Dock = DockStyle.Fill;
            pnCopyCtl.Dock = DockStyle.Fill;
            pnCopyAdd.Dock = DockStyle.Fill;
            pnCopyAddItem.Dock = DockStyle.Fill;
            pnCopyAddView.Dock = DockStyle.Fill;
            pnCopyCtlCtl.Dock = DockStyle.Fill;
            pnCopyCtlCtlItem.Dock = DockStyle.Fill;
            pnAddNew.Dock = DockStyle.Fill;
            pnAddNewItemGrf.Dock = DockStyle.Fill;
            pnAddNewItem.Dock = DockStyle.Fill;
            pnCopyAddView.BackColor = Color.Red;
            pnCopyAddItem.BackColor = Color.Green;
            pnCopyCtlCtl.BackColor = Color.Fuchsia;
            pnCopyCtlCtlItem.BackColor = Color.Peru;
            pnAddNew.BackColor = Color.Red;
            pnAddNewItemGrf.BackColor = Color.Green;
            pnAddNewItem.BackColor = Color.Peru;

            tC1.Dock = DockStyle.Fill;
            tC1.HotTrack = true;
            tC1.BorderStyle = BorderStyle.FixedSingle;
            tC1.TabSizeMode = TabSizeModeEnum.Fit;
            tC1.TabsShowFocusCues = true;
            tC1.Alignment = TabAlignment.Top;
            tC1.SelectedTabBold = true;
            tC1.Name = "tC1";
            tC1.Font = fEdit;
            tC1.CanCloseTabs = true;
            tC1.CanAutoHide = false;
            tC1.SelectedTabBold = true;
            tabView.Name = "tabView";
            tabView.TabIndex = 0;
            tabView.Text = "เลือกรายการ";
            tabView.Font = fEditB;
            tabCopy.Name = "tabAdd";
            tabCopy.TabIndex = 0;
            tabCopy.Text = "Copy รายการ";
            tabCopy.Font = fEditB;
            tabAdd.Name = "tabAdd";
            tabAdd.TabIndex = 0;
            tabAdd.Text = "สร้างรายการใหม่";
            tabAdd.Font = fEditB;
            tabReMed.Name = "tabReMed";
            tabReMed.TabIndex = 0;
            tabReMed.Text = "re medical";
            tabReMed.Font = fEditB;
            tabView.Controls.Add(pnView);
            tabCopy.Controls.Add(pnCopy);
            tabAdd.Controls.Add(pnAdd);
            tC1.Controls.Add(tabView);
            tC1.Controls.Add(tabCopy);
            tC1.Controls.Add(tabAdd);
            tC1.Controls.Add(tabReMed);
            pnView.Controls.Add(sCView);
            pnCopy.Controls.Add(sCCopy);
            pnAdd.Controls.Add(sCAddNew);

            grfView = new C1FlexGrid();
            grfView.Font = fEdit;
            grfView.Dock = DockStyle.Fill;
            grfView.Location = new Point(0, 0);
            grfView.Rows.Count = 1;
            grfView.Name = "grfView";
            grfItem = new C1FlexGrid();
            grfItem.Font = fEdit;
            grfItem.Dock = DockStyle.Fill;
            grfItem.Location = new Point(0, 0);
            grfItem.Rows.Count = 1;
            grfItem.Name = "grfItem";
            pnViewLeft.Controls.Add(grfView);
            pnViewItem.Controls.Add(grfItem);

            scView.Collapsible = true;
            scView.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            scView.Location = new System.Drawing.Point(0, 21);
            scView.Name = "scView";
            scView.Controls.Add(pnViewLeft);
            scViewItem.Collapsible = true;
            scViewItem.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            scViewItem.Location = new System.Drawing.Point(0, 21);
            scViewItem.Name = "scViewItem";
            scViewItem.Controls.Add(pnViewItem);
            sCView.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCView.Name = "sCView";
            sCView.Dock = System.Windows.Forms.DockStyle.Fill;
            sCView.Panels.Add(scView);
            sCView.Panels.Add(scViewItem);
            sCView.HeaderHeight = 20;

            scCopy.Collapsible = true;
            scCopy.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            scCopy.Location = new System.Drawing.Point(0, 21);
            scCopy.Name = "scCopy";
            scCopy.Controls.Add(pnCopyAdd);
            scCopyCtl.Collapsible = true;
            scCopyCtl.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            scCopyCtl.Location = new System.Drawing.Point(0, 21);
            scCopyCtl.Name = "scAddCtl";
            scCopyCtl.Controls.Add(pnCopyCtl);
            sCCopy.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCCopy.Name = "sCAdd";
            sCCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            sCCopy.Panels.Add(scCopy);
            sCCopy.Panels.Add(scCopyCtl);
            sCCopy.HeaderHeight = 20;

            scCopyAddItem.Collapsible = true;
            scCopyAddItem.Dock = PanelDockStyle.Bottom;
            scCopyAddItem.Location = new Point(0, 21);
            scCopyAddItem.Name = "scCopyAddItem";
            scCopyAddItem.Controls.Add(pnCopyAddItem);
            scCopyAddView.Collapsible = true;
            scCopyAddView.Dock = PanelDockStyle.Top;
            scCopyAddView.Location = new Point(0, 21);
            scCopyAddView.Name = "scCopyAddView";
            scCopyAddView.Controls.Add(pnCopyAddView);
            sCCopyAdd.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCCopyAdd.Name = "sCCopyAdd";
            sCCopyAdd.Dock = DockStyle.Fill;
            sCCopyAdd.Panels.Add(scCopyAddView);
            sCCopyAdd.Panels.Add(scCopyAddItem);
            sCCopyAdd.HeaderHeight = 20;
            pnCopyAdd.Controls.Add(sCCopyAdd);

            scCopyCtlCtlItem.Collapsible = true;
            scCopyCtlCtlItem.Dock = PanelDockStyle.Bottom;
            scCopyCtlCtlItem.Location = new Point(0, 21);
            scCopyCtlCtlItem.Name = "scCopyCtlCtlItem";
            scCopyCtlCtlItem.Controls.Add(pnCopyCtlCtlItem);
            scCopyCtlCtl.Collapsible = true;
            scCopyCtlCtl.Dock = PanelDockStyle.Top;
            scCopyCtlCtl.Location = new Point(0, 21);
            scCopyCtlCtl.Name = "scCopyCtlCtl";
            scCopyCtlCtl.Controls.Add(pnCopyCtlCtl);
            sCCopyCtlCtl.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCCopyCtlCtl.Name = "sCCopyCtlCtl";
            sCCopyCtlCtl.Dock = DockStyle.Fill;
            sCCopyCtlCtl.Panels.Add(scCopyCtlCtl);
            sCCopyCtlCtl.Panels.Add(scCopyCtlCtlItem);
            sCCopyCtlCtl.HeaderHeight = 20;
            pnCopyCtl.Controls.Add(sCCopyCtlCtl);

            scAddNew.Collapsible = true;
            scAddNew.Dock = PanelDockStyle.Top;
            scAddNew.Location = new Point(0, 21);
            scAddNew.Name = "scAddNew";
            scAddNew.Controls.Add(pnAddNew);
            scAddNewItemGrf.Collapsible = true;
            scAddNewItemGrf.Dock = PanelDockStyle.Top;
            scAddNewItemGrf.Location = new Point(0, 21);
            scAddNewItemGrf.Name = "scAddNewItemGrf";
            scAddNewItemGrf.Controls.Add(pnAddNewItemGrf);
            scAddNewItem.Collapsible = true;
            scAddNewItem.Dock = PanelDockStyle.Bottom;
            scAddNewItem.Location = new Point(0, 21);
            scAddNewItem.Name = "scAddNewItem";
            //scAddNewItem.Controls.Add(pnAddNewItem);
            sCAddNew.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCAddNew.Name = "sCAddNew";
            sCAddNew.Dock = DockStyle.Fill;
            sCAddNew.Panels.Add(scAddNew);
            sCAddNew.Panels.Add(scAddNewItemGrf);
            sCAddNew.Panels.Add(scAddNewItem);
            sCAddNew.HeaderHeight = 20;

            scAddNewItm.Collapsible = true;
            scAddNewItm.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            scAddNewItm.Location = new System.Drawing.Point(0, 21);
            scAddNewItm.Name = "scAddNewItm";
            scAddNewItm.Controls.Add(pnAddNewItem);
            scAddNewItmGrf.Collapsible = true;
            scAddNewItmGrf.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            scAddNewItmGrf.Location = new System.Drawing.Point(0, 21);
            scAddNewItmGrf.Name = "scAddNewItmGrf";
            scAddNewItmGrf.Controls.Add(pnViewItem);
            sCAddNewItm.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCAddNewItm.Name = "sCAddNewItm";
            sCAddNewItm.Dock = System.Windows.Forms.DockStyle.Fill;
            sCAddNewItm.Panels.Add(scAddNewItm);
            sCAddNewItm.Panels.Add(scAddNewItmGrf);
            sCAddNewItm.HeaderHeight = 20;
            scAddNewItem.Controls.Add(sCAddNewItm);

            tcAddNewItemGrf.Dock = DockStyle.Fill;
            tcAddNewItemGrf.HotTrack = true;
            tcAddNewItemGrf.BorderStyle = BorderStyle.FixedSingle;
            tcAddNewItemGrf.TabSizeMode = TabSizeModeEnum.Fit;
            tcAddNewItemGrf.TabsShowFocusCues = true;
            tcAddNewItemGrf.Alignment = TabAlignment.Left;
            tcAddNewItemGrf.SelectedTabBold = true;
            tcAddNewItemGrf.Name = "tcAddNewItemGrf";
            tcAddNewItemGrf.Font = fEdit;
            tcAddNewItemGrf.CanCloseTabs = true;
            tcAddNewItemGrf.CanAutoHide = false;
            tcAddNewItemGrf.SelectedTabBold = true;
            tabDrug.Name = "tabDrug";
            tabDrug.TabIndex = 0;
            tabDrug.Text = "Drug List";
            tabDrug.Font = fEditB;
            tabSup.Name = "tabSup";
            tabSup.TabIndex = 0;
            tabSup.Text = "Supply List";
            tabSup.Font = fEditB;
            tabLab.Name = "tabLab";
            tabLab.TabIndex = 0;
            tabLab.Text = "Lab List";
            tabLab.Font = fEditB;
            tabXray.Name = "tabXray";
            tabXray.TabIndex = 0;
            tabXray.Text = "X-Ray List";
            tabXray.Font = fEditB;
            tcAddNewItemGrf.Controls.Add(tabDrug);
            tcAddNewItemGrf.Controls.Add(tabSup);
            tcAddNewItemGrf.Controls.Add(tabLab);
            tcAddNewItemGrf.Controls.Add(tabXray);
            pnAddNewItemGrf.Controls.Add(tcAddNewItemGrf);

            this.Controls.Add(tC1);

            pnView.ResumeLayout(false);
            pnCopy.ResumeLayout(false);
            pnAdd.ResumeLayout(false);
            pnViewLeft.ResumeLayout(false);
            pnViewItem.ResumeLayout(false);
            pnAddNew.ResumeLayout(false);
            pnAddNewItemGrf.ResumeLayout(false);
            pnAddNewItem.ResumeLayout(false);

            tcAddNewItemGrf.ResumeLayout(false);
            tabDrug.ResumeLayout(false);
            tabSup.ResumeLayout(false);
            tabLab.ResumeLayout(false);
            tabXray.ResumeLayout(false);
            tabReMed.ResumeLayout(false);
            tC1.ResumeLayout(false);
            tabView.ResumeLayout(false);
            tabCopy.ResumeLayout(false);
            tabAdd.ResumeLayout(false);
            scViewItem.ResumeLayout(false);
            scView.ResumeLayout(false);
            sCView.ResumeLayout(false);
            scCopyCtl.ResumeLayout(false);
            scCopy.ResumeLayout(false);
            sCCopy.ResumeLayout(false);
            scAddNewItem.ResumeLayout(false);
            scAddNewItemGrf.ResumeLayout(false);
            scAddNew.ResumeLayout(false);
            sCAddNew.ResumeLayout(false);
            sCAddNewItm.ResumeLayout(false);
            scAddNewItm.ResumeLayout(false);
            scAddNewItmGrf.ResumeLayout(false);
            pnView.ResumeLayout(false);
            pnCopy.ResumeLayout(false);
            pnCopyCtl.ResumeLayout(false);
            pnCopyAdd.ResumeLayout(false);
            pnViewLeft.ResumeLayout(false);
            pnViewItem.ResumeLayout(false);
            sCCopyAdd.ResumeLayout(false);
            scCopyAddView.ResumeLayout(false);
            scCopyAddItem.ResumeLayout(false);
            sCCopyCtlCtl.ResumeLayout(false);
            scCopyCtlCtl.ResumeLayout(false);
            scCopyCtlCtlItem.ResumeLayout(false);
            pnCopyAddItem.ResumeLayout(false);
            pnCopyAddView.ResumeLayout(false);
            pnCopyCtlCtl.ResumeLayout(false);
            pnCopyCtlCtlItem.ResumeLayout(false);

            sCCopyAdd.PerformLayout();
            scCopyAddView.PerformLayout();
            scCopyAddItem.PerformLayout();
            scCopyCtl.PerformLayout();
            scCopy.PerformLayout();
            sCCopy.PerformLayout();
            scViewItem.PerformLayout();
            scView.PerformLayout();
            sCView.PerformLayout();
            sCCopyCtlCtl.PerformLayout();
            scCopyCtlCtl.PerformLayout();
            scCopyCtlCtlItem.PerformLayout();
            scAddNewItem.PerformLayout();
            scAddNewItemGrf.PerformLayout();
            scAddNew.PerformLayout();
            sCAddNew.PerformLayout();
            sCAddNewItm.PerformLayout();
            scAddNewItm.PerformLayout();
            scAddNewItmGrf.PerformLayout();
            pnCopyCtl.PerformLayout();
            pnCopyAdd.PerformLayout();
            pnView.PerformLayout();
            pnCopy.PerformLayout();
            pnAdd.PerformLayout();
            pnViewLeft.PerformLayout();
            pnViewItem.PerformLayout();
            pnCopyAddItem.PerformLayout();
            pnCopyAddView.PerformLayout();
            pnCopyCtlCtl.PerformLayout();
            pnCopyCtlCtlItem.PerformLayout();
            pnAddNew.PerformLayout();
            pnAddNewItemGrf.PerformLayout();
            pnAddNewItem.PerformLayout();
            tcAddNewItemGrf.PerformLayout();
            tabDrug.PerformLayout();
            tabSup.PerformLayout();
            tabLab.PerformLayout();
            tabXray.PerformLayout();
            tabReMed.PerformLayout();
            tC1.PerformLayout();
            tabView.PerformLayout();
            tabCopy.PerformLayout();
            tabAdd.PerformLayout();
            
        }
        private void initComponentpnAddNewItem()
        {
            int gapY = 30, gapX = 20, gapLine = 0, gapColName = 120;

            lbItmNewId = new Label();
            lbItmNewId.Text = "รหัส";
            lbItmNewId.Font = fEdit;
            lbItmNewId.Location = new System.Drawing.Point(gapX, 5);
            lbItmNewId.AutoSize = true;
            lbItmNewId.Name = "lbItmNewId";
            txtItmNewId = new C1TextBox();
            txtItmNewId.Font = fEdit;
            txtItmNewId.Name = "txtItmNewId";
            txtItmNewId.Location = new System.Drawing.Point(gapColName, lbItmId.Location.Y);
            txtItmNewId.Size = new Size(120, 20);
            gapLine += gapY;
            lbItmNewName = new Label();
            lbItmNewName.Text = "ชื่อ";
            lbItmNewName.Font = fEdit;
            lbItmNewName.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmNewName.AutoSize = true;
            lbItmNewName.Name = "lbItmNewName";
            txtItmNewName = new C1TextBox();
            txtItmNewName.Font = fEdit;
            txtItmNewName.Name = "txtItmNewName";
            txtItmNewName.Location = new System.Drawing.Point(gapColName, lbItmName.Location.Y);
            txtItmNewName.Size = new Size(300, 20);
            gapLine += gapY;
            lbItmNewQty = new Label();
            lbItmNewQty.Text = "QTY";
            lbItmNewQty.Font = fEdit;
            lbItmNewQty.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmNewQty.AutoSize = true;
            lbItmNewQty.Name = "lbItmNewQty";
            txtItmNewQty = new C1TextBox();
            txtItmNewQty.Font = fEdit;
            txtItmNewQty.Name = "txtItmNewQty";
            txtItmNewQty.Location = new System.Drawing.Point(gapColName, lbItmQty.Location.Y);
            txtItmNewQty.Size = new Size(120, 20);
            gapLine += gapY;
            lbItmNewFre = new Label();
            lbItmNewFre.Text = "วิธีใช้";
            lbItmNewFre.Font = fEdit;
            lbItmNewFre.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmNewFre.AutoSize = true;
            lbItmNewFre.Name = "lbItmNewFre";
            txtItmNewFre = new C1TextBox();
            txtItmNewFre.Font = fEdit;
            txtItmNewFre.Name = "txtItmNewFre";
            txtItmNewFre.Location = new System.Drawing.Point(gapColName, lbItmFre.Location.Y);
            txtItmNewFre.Size = new Size(300, 20);
            gapLine += gapY;
            lbItmNewIn1 = new Label();
            lbItmNewIn1.Text = "ข้อควรระวัง1";
            lbItmNewIn1.Font = fEdit;
            lbItmNewIn1.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmNewIn1.AutoSize = true;
            lbItmNewIn1.Name = "lbItmNewIn1";
            txtItmNewIn1 = new C1TextBox();
            txtItmNewIn1.Font = fEdit;
            txtItmNewIn1.Name = "txtItmNewIn1";
            txtItmNewIn1.Location = new System.Drawing.Point(gapColName, lbItmIn1.Location.Y);
            txtItmNewIn1.Size = new Size(300, 20);
            gapLine += gapY;
            lbItmNewIn2 = new Label();
            lbItmNewIn2.Text = "ข้อควรระวัง2";
            lbItmNewIn2.Font = fEdit;
            lbItmNewIn2.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmNewIn2.AutoSize = true;
            lbItmNewIn2.Name = "lbItmNewIn2";
            txtItmNewIn2 = new C1TextBox();
            txtItmNewIn2.Font = fEdit;
            txtItmNewIn2.Name = "txtItNewmIn2";
            txtItmNewIn2.Location = new System.Drawing.Point(gapColName, lbItmIn2.Location.Y);
            txtItmNewIn2.Size = new Size(300, 20);

            pnAddNewItem.Controls.Add(lbItmNewId);
            pnAddNewItem.Controls.Add(txtItmNewId);
            pnAddNewItem.Controls.Add(lbItmNewName);
            pnAddNewItem.Controls.Add(txtItmNewName);
            pnAddNewItem.Controls.Add(lbItmNewQty);
            pnAddNewItem.Controls.Add(txtItmNewQty);
            pnAddNewItem.Controls.Add(lbItmNewFre);
            pnAddNewItem.Controls.Add(txtItmNewFre);
            pnAddNewItem.Controls.Add(lbItmNewIn1);
            pnAddNewItem.Controls.Add(txtItmNewIn1);
            pnAddNewItem.Controls.Add(lbItmNewIn2);
            pnAddNewItem.Controls.Add(txtItmNewIn2);
        }
        private void initComponentpnCopyCtlCtl()
        {
            int gapLine = 20, gapX = 20;

            lbDoctor = new Label();
            lbDoctor.Text = "ชื่อแพทย์ : ";
            lbDoctor.Font = fEditBig;
            lbDoctor.Location = new System.Drawing.Point(gapX, gapLine);
            lbDoctor.AutoSize = true;
            lbDoctor.Name = "lbDoctor";
            cboDoctor = new C1ComboBox();
            cboDoctor.Font = fEdit;
            size = bc.MeasureString(lbDoctor);
            cboDoctor.Location = new System.Drawing.Point(lbDoctor.Location.X + lbDoctor.Width + 20, lbDoctor.Location.Y);

            pnCopyCtlCtl.Controls.Add(lbDoctor);
            pnCopyCtlCtl.Controls.Add(cboDoctor);
        }
        private void initComponentpnCopyCtlCtlItem()
        {
            int gapY = 30, gapX = 20, gapLine = 0, gapColName = 120;

            lbItmId = new Label();
            lbItmId.Text = "รหัส";
            lbItmId.Font = fEdit;
            lbItmId.Location = new System.Drawing.Point(gapX, 5);
            lbItmId.AutoSize = true;
            lbItmId.Name = "lbItmId";
            txtItmId = new C1TextBox();
            txtItmId.Font = fEdit;
            txtItmId.Name = "txtItmId";
            txtItmId.Location = new System.Drawing.Point(gapColName, lbItmId.Location.Y);
            txtItmId.Size = new Size(120, 20);
            gapLine += gapY;
            lbItmName = new Label();
            lbItmName.Text = "ชื่อ";
            lbItmName.Font = fEdit;
            lbItmName.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmName.AutoSize = true;
            lbItmName.Name = "lbItmName";
            txtItmName = new C1TextBox();
            txtItmName.Font = fEdit;
            txtItmName.Name = "txtItmName";
            txtItmName.Location = new System.Drawing.Point(gapColName, lbItmName.Location.Y);
            txtItmName.Size = new Size(300, 20);
            gapLine += gapY;
            lbItmQty = new Label();
            lbItmQty.Text = "QTY";
            lbItmQty.Font = fEdit;
            lbItmQty.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmQty.AutoSize = true;
            lbItmQty.Name = "lbItmQty";
            txtItmQty = new C1TextBox();
            txtItmQty.Font = fEdit;
            txtItmQty.Name = "txtItmQty";
            txtItmQty.Location = new System.Drawing.Point(gapColName, lbItmQty.Location.Y);
            txtItmQty.Size = new Size(120, 20);
            gapLine += gapY;
            lbItmFre = new Label();
            lbItmFre.Text = "วิธีใช้";
            lbItmFre.Font = fEdit;
            lbItmFre.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmFre.AutoSize = true;
            lbItmFre.Name = "lbItmFre";
            txtItmFre = new C1TextBox();
            txtItmFre.Font = fEdit;
            txtItmFre.Name = "txtItmFre";
            txtItmFre.Location = new System.Drawing.Point(gapColName, lbItmFre.Location.Y);
            txtItmFre.Size = new Size(300, 20);
            gapLine += gapY;
            lbItmIn1 = new Label();
            lbItmIn1.Text = "ข้อควรระวัง1";
            lbItmIn1.Font = fEdit;
            lbItmIn1.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmIn1.AutoSize = true;
            lbItmIn1.Name = "lbItmIn1";
            txtItmIn1 = new C1TextBox();
            txtItmIn1.Font = fEdit;
            txtItmIn1.Name = "txtItmIn1";
            txtItmIn1.Location = new System.Drawing.Point(gapColName, lbItmIn1.Location.Y);
            txtItmIn1.Size = new Size(300, 20);
            gapLine += gapY;
            lbItmIn2 = new Label();
            lbItmIn2.Text = "ข้อควรระวัง2";
            lbItmIn2.Font = fEdit;
            lbItmIn2.Location = new System.Drawing.Point(gapX, gapLine);
            lbItmIn2.AutoSize = true;
            lbItmIn2.Name = "lbItmIn2";
            txtItmIn2 = new C1TextBox();
            txtItmIn2.Font = fEdit;
            txtItmIn2.Name = "txtItmIn2";
            txtItmIn2.Location = new System.Drawing.Point(gapColName, lbItmIn2.Location.Y);
            txtItmIn2.Size = new Size(300, 20);

            pnCopyCtlCtlItem.Controls.Add(lbItmId);
            pnCopyCtlCtlItem.Controls.Add(txtItmId);
            pnCopyCtlCtlItem.Controls.Add(lbItmName);
            pnCopyCtlCtlItem.Controls.Add(txtItmName);
            pnCopyCtlCtlItem.Controls.Add(lbItmQty);
            pnCopyCtlCtlItem.Controls.Add(txtItmQty);
            pnCopyCtlCtlItem.Controls.Add(lbItmFre);
            pnCopyCtlCtlItem.Controls.Add(txtItmFre);
            pnCopyCtlCtlItem.Controls.Add(lbItmIn1);
            pnCopyCtlCtlItem.Controls.Add(txtItmIn1);
            pnCopyCtlCtlItem.Controls.Add(lbItmIn2);
            pnCopyCtlCtlItem.Controls.Add(txtItmIn2);
        }
        private void initConfig()
        {
            initGrfDrug();
            initGrfSup();
            initGrfLab();
            initGrfXray();
            initGrfAddItem();
            this.Load += FrmDoctorDrugSet_Load;
        }
        private void initGrfAddItem()
        {
            grfAddNewItem = new C1FlexGrid();
            grfAddNewItem.Font = fEdit;
            grfAddNewItem.Dock = DockStyle.Fill;
            grfAddNewItem.Location = new Point(0, 0);
            grfAddNewItem.Rows.Count = 1;
            grfAddNewItem.DoubleClick += GrfAddNewItem_DoubleClick;
            scAddNewItmGrf.Controls.Add(grfAddNewItem);

            theme1.SetTheme(grfAddNewItem, "Office2010Red");

        }

        private void GrfAddNewItem_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void initGrfCopy()
        {
            grfCopy = new C1FlexGrid();
            grfCopy.Font = fEdit;
            grfCopy.Dock = DockStyle.Fill;
            grfCopy.Location = new Point(0, 0);
            grfCopy.Rows.Count = 1;
            grfCopy.DoubleClick += GrfCopy_DoubleClick;
            pnCopyAddView.Controls.Add(grfCopy);

            theme1.SetTheme(grfCopy, "Office2010Red");

        }
        private void initGrfCopyItem()
        {
            grfCopyItem = new C1FlexGrid();
            grfCopyItem.Font = fEdit;
            grfCopyItem.Dock = System.Windows.Forms.DockStyle.Fill;
            grfCopyItem.Location = new System.Drawing.Point(0, 0);
            grfCopyItem.Rows.Count = 1;
            grfCopyItem.DoubleClick += GrfCopyItem_DoubleClick;
            pnCopyAddItem.Controls.Add(grfCopyItem);

            theme1.SetTheme(grfCopyItem, "Office2010Red");

        }

        private void GrfCopyItem_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void GrfCopy_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void initGrfDrug()
        {
            grfDrug = new C1FlexGrid();
            grfDrug.Font = fEdit;
            grfDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            grfDrug.Location = new System.Drawing.Point(0, 0);
            grfDrug.Rows.Count = 1;
            grfDrug.DoubleClick += GrfDrug_DoubleClick;
            tabDrug.Controls.Add(grfDrug);

            theme1.SetTheme(grfDrug, "Office2010Red");

        }

        private void GrfDrug_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void initGrfSup()
        {
            grfSup = new C1FlexGrid();
            grfSup.Font = fEdit;
            grfSup.Dock = System.Windows.Forms.DockStyle.Fill;
            grfSup.Location = new System.Drawing.Point(0, 0);
            grfSup.Rows.Count = 1;
            grfSup.DoubleClick += GrfSup_DoubleClick;
            tabSup.Controls.Add(grfSup);

            theme1.SetTheme(grfSup, "ShinyBlue");

        }

        private void GrfSup_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void initGrfLab()
        {
            grfLab = new C1FlexGrid();
            grfLab.Font = fEdit;
            grfLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLab.Location = new System.Drawing.Point(0, 0);
            grfLab.Rows.Count = 1;
            grfLab.DoubleClick += GrfLab_DoubleClick;
            tabLab.Controls.Add(grfLab);

            theme1.SetTheme(grfLab, "RainerOrange");

        }

        private void GrfLab_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void initGrfXray()
        {
            grfXray = new C1FlexGrid();
            grfXray.Font = fEdit;
            grfXray.Dock = System.Windows.Forms.DockStyle.Fill;
            grfXray.Location = new System.Drawing.Point(0, 0);
            grfXray.Rows.Count = 1;
            grfXray.DoubleClick += GrfXray_DoubleClick;
            tabXray.Controls.Add(grfXray);

            theme1.SetTheme(grfXray, "Office2016Colorful");

        }

        private void GrfXray_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void FrmDoctorDrugSet_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
    }
}
