using bangna_hospital.control;
using bangna_hospital.FlexGrid;
using bangna_hospital.object1;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SplitContainer;
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
    public class FrmDoctorDrugSet:Form
    {
        BangnaControl bc;
        C1FlexGrid grfView, grfItem, grfDrug, grfSup, grfLab, grfXray, grfCopy, grfCopyItem, grfAddNewItem, grfReMedVs, grfReMedItem;
        C1DockingTab tC1, tcAddNewGrf;
        C1DockingTabPage tabView, tabCopy, tabAdd, tabReMed, tabDrug, tabSup, tabLab, tabXray;
        Label lbDoctor, lbItmId, lbItmName, lbItmQty, lbItmFre, lbItmIn1, lbItmIn2, lbItmNewId, lbItmNewName, lbItmNewQty, lbItmNewFre, lbItmNewIn1, lbItmNewIn2, lbDrugSetName;
        C1TextBox txtItmId, txtItmName, txtItmQty, txtItmFre, txtItmIn1, txtItmIn2, txtItmNewId, txtItmNewName, txtItmNewQty, txtItmNewFre, txtItmNewIn1, txtItmNewIn2, txtDrugSetName, txtDrugSetId;
        C1TextBox txtTabAddDrugSetName, txtTabAddDrugSetId, txtTabAddDrugSetRemark;
        Label lbTabAddDrugSetName, lbTabAddDrugSetRemark;
        C1Button btnReMedItemSen, btnTabAddDrugSetSave;
        C1ComboBox cboDoctor;
        Panel pnCopyAddView, pnCopyAddItem, pnCopyAdd, pnCopyCtl, pnView, pnCopy, pnAdd, pnViewLeft, pnViewItem, pnCopyCtlCtl, pnCopyCtlCtlItem, pnAddNew, pnAddNewItemGrf, pnAddNewItem, pnAddNewGrf;
        Panel pnscReMedVs, pnsCReMedItem, pnsCReMedItemSend;
        C1ThemeController theme1;
        C1SplitterPanel scView, scViewItem, scCopy, scCopyCtl, scCopyAddView, scCopyAddItem, scCopyCtlCtl, scCopyCtlCtlItem, scAddNew, scAddNewGrf, scAddNewItem, scAddNewItmGrf, scAddNewItm;
        C1SplitterPanel scReMedVs, sCReMedItem;
        C1SplitContainer sCView, sCCopy, sCCopyAdd, sCCopyCtlCtl, sCAddNew, sCAddNewItm, sCReMed;

        int colVsVsDate = 1, colVsVn = 2, colVsStatus = 3, colVsDept = 4, colVsPreno = 5, colVsAn = 6, colVsAndate = 7;
        int colOrdDrugChk=1, colOrdDrugId = 2, colOrdDrugDate = 3, colOrdDrugNameT = 4, colOrdDrugQty = 5, colOrdDrugtypcd = 6, colOrdAddDrugFr = 7, colOrdAddDrugIn = 8, colOrdDrugUnit = 9;
        int colOrdLabId = 1, colOrdLabName = 2, colOrdlabUnit = 3, colLabtypcd = 4, colLabgrpcd = 5, colLabgrpdsc = 6;
        int colOrdXrayId = 1, colOrdXrayName = 2, colOrdXrayUnit = 3, colXraytypcd = 4, colXraygrpcd = 5, colXraygrpdsc = 6;
        int colOrderId = 1, colOrderDate = 2, colOrderName = 3, colOrderMed = 4, colOrderQty = 5;

        int colOrdAddId = 1, colOrdAddNameT = 2, colOrdAddUnit = 3, colOrdAddQty = 4, colOrdAddDrugFr1 = 5, colOrdAddDrugIn1 = 6, colOrdDrugIn1 = 7, colOrdAddItemType = 10;

        Font fEdit, fEditB, fEditBig;
        Size size = new Size();

        String hn = "";
        public FrmDoctorDrugSet(BangnaControl bc, String hn)
        {
            this.bc = bc;
            this.hn = hn;
            initComponent();
            initComponentpnCopyCtlCtl();
            initComponentpnCopyCtlCtlItem();
            initComponentpnAddNewItem();
            initComponentTabAdd();
            initComponentTabRemed();
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
            tcAddNewGrf = new C1DockingTab();
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
            pnAddNewGrf = new Panel();
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
            scAddNewGrf = new C1SplitterPanel();
            scAddNewItem = new C1SplitterPanel();
            sCAddNew = new C1SplitContainer();
            scAddNewItmGrf = new C1SplitterPanel();
            scAddNewItm = new C1SplitterPanel();
            sCAddNewItm = new C1SplitContainer();

            tC1.SuspendLayout();
            tcAddNewGrf.SuspendLayout();
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
            pnAddNewGrf.SuspendLayout();
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
            scAddNewGrf.SuspendLayout();
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
            pnAddNewGrf.Dock = DockStyle.Fill;
            pnCopyAddView.BackColor = Color.Red;
            pnCopyAddItem.BackColor = Color.Green;
            pnCopyCtlCtl.BackColor = Color.Fuchsia;
            pnCopyCtlCtlItem.BackColor = Color.Peru;
            pnAddNew.BackColor = Color.Red;
            pnAddNewItemGrf.BackColor = Color.Green;
            pnAddNewItem.BackColor = Color.Peru;
            pnAddNewGrf.BackColor = Color.Orange;

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
            scAddNewGrf.Collapsible = true;
            scAddNewGrf.Dock = PanelDockStyle.Top;
            scAddNewGrf.Location = new Point(0, 21);
            scAddNewGrf.Name = "scAddNewGrf";
            scAddNewGrf.Controls.Add(pnAddNewGrf);
            scAddNewItem.Collapsible = true;
            scAddNewItem.Dock = PanelDockStyle.Bottom;
            scAddNewItem.Location = new Point(0, 21);
            scAddNewItem.Name = "scAddNewItem";
            scAddNewItem.Controls.Add(pnAddNewItem);
            sCAddNew.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCAddNew.Name = "sCAddNew";
            sCAddNew.Dock = DockStyle.Fill;
            sCAddNew.Panels.Add(scAddNew);
            sCAddNew.Panels.Add(scAddNewGrf);
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
            scAddNewItmGrf.Controls.Add(pnAddNewItemGrf);
            sCAddNewItm.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCAddNewItm.Name = "sCAddNewItm";
            sCAddNewItm.Dock = System.Windows.Forms.DockStyle.Fill;
            sCAddNewItm.Panels.Add(scAddNewItm);
            sCAddNewItm.Panels.Add(scAddNewItmGrf);
            sCAddNewItm.HeaderHeight = 20;
            scAddNewItem.Controls.Add(sCAddNewItm);

            tcAddNewGrf.Dock = DockStyle.Fill;
            tcAddNewGrf.HotTrack = true;
            tcAddNewGrf.BorderStyle = BorderStyle.FixedSingle;
            tcAddNewGrf.TabSizeMode = TabSizeModeEnum.Fit;
            tcAddNewGrf.TabsShowFocusCues = true;
            tcAddNewGrf.Alignment = TabAlignment.Left;
            tcAddNewGrf.SelectedTabBold = true;
            tcAddNewGrf.Name = "tcAddNewItemGrf";
            tcAddNewGrf.Font = fEdit;
            tcAddNewGrf.CanCloseTabs = true;
            tcAddNewGrf.CanAutoHide = false;
            tcAddNewGrf.SelectedTabBold = true;
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
            tcAddNewGrf.Controls.Add(tabDrug);
            tcAddNewGrf.Controls.Add(tabSup);
            tcAddNewGrf.Controls.Add(tabLab);
            tcAddNewGrf.Controls.Add(tabXray);
            pnAddNewGrf.Controls.Add(tcAddNewGrf);
            tcAddNewGrf.Dock = DockStyle.Fill;
            tcAddNewGrf.BackColor = Color.Indigo;

            this.Controls.Add(tC1);

            pnView.ResumeLayout(false);
            pnCopy.ResumeLayout(false);
            pnAdd.ResumeLayout(false);
            pnViewLeft.ResumeLayout(false);
            pnViewItem.ResumeLayout(false);
            pnAddNew.ResumeLayout(false);
            pnAddNewItemGrf.ResumeLayout(false);
            pnAddNewItem.ResumeLayout(false);
            pnAddNewGrf.ResumeLayout(false);

            tcAddNewGrf.ResumeLayout(false);
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
            scAddNewGrf.ResumeLayout(false);
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
            scAddNewGrf.PerformLayout();
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
            pnAddNewGrf.PerformLayout();
            tcAddNewGrf.PerformLayout();
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
        private void initComponentTabRemed()
        {
            int gapY = 30, gapX = 20, gapLine = 0, gapColName = 120;

            sCReMed = new C1SplitContainer();
            scReMedVs = new C1SplitterPanel();
            sCReMedItem = new C1SplitterPanel();
            pnscReMedVs = new Panel();
            pnsCReMedItem = new Panel();
            pnsCReMedItemSend = new Panel();

            sCReMed.SuspendLayout();
            scReMedVs.SuspendLayout();
            sCReMedItem.SuspendLayout();
            pnscReMedVs.SuspendLayout();
            pnsCReMedItem.SuspendLayout();
            pnsCReMedItemSend.SuspendLayout();

            pnscReMedVs.Dock = DockStyle.Fill;
            pnsCReMedItem.Dock = DockStyle.Fill;
            pnsCReMedItemSend.Dock = DockStyle.Bottom;

            scReMedVs.Collapsible = true;
            scReMedVs.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            scReMedVs.Location = new System.Drawing.Point(0, 21);
            scReMedVs.Name = "scReMedVs";
            scReMedVs.Controls.Add(pnscReMedVs);
            sCReMedItem.Collapsible = true;
            sCReMedItem.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            sCReMedItem.Location = new System.Drawing.Point(0, 21);
            sCReMedItem.Name = "sCReMedItem";
            sCReMedItem.Controls.Add(pnsCReMedItem);
            sCReMedItem.Controls.Add(pnsCReMedItemSend);
            sCReMed.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCReMed.Name = "sCReMed";
            sCReMed.Dock = System.Windows.Forms.DockStyle.Fill;
            sCReMed.Panels.Add(scReMedVs);
            sCReMed.Panels.Add(sCReMedItem);
            sCReMed.HeaderHeight = 20;

            grfReMedVs = new C1FlexGrid();
            grfReMedVs.Font = fEdit;
            grfReMedVs.Dock = DockStyle.Fill;
            grfReMedVs.Location = new Point(0, 0);
            grfReMedVs.Rows.Count = 1;
            grfReMedVs.Name = "grfReMedVs";
            grfReMedVs.DoubleClick += GrfVs_DoubleClick;
            pnscReMedVs.Controls.Add(grfReMedVs);
            theme1.SetTheme(grfReMedVs, "Office2010Red");
            grfReMedItem = new C1FlexGrid();
            grfReMedItem.Font = fEdit;
            grfReMedItem.Dock = DockStyle.Fill;
            grfReMedItem.Location = new Point(0, 0);
            grfReMedItem.Rows.Count = 1;
            grfReMedItem.Name = "grfReMedItem";
            grfReMedItem.DoubleClick += GrfReMedItem_DoubleClick;
            pnsCReMedItem.Controls.Add(grfReMedItem);
            theme1.SetTheme(grfReMedItem, "Office2010Red");

            btnReMedItemSen = new C1Button();
            btnReMedItemSen.Text = "ส่งไป หน้าป้อนยา";
            btnReMedItemSen.Name = "btnReMedItemSen";
            btnReMedItemSen.Location = new Point( 100, 20);
            btnReMedItemSen.Size = new Size(140, 40);
            pnsCReMedItemSend.Controls.Add(btnReMedItemSen);

            sCReMedItem.ResumeLayout(false);
            scReMedVs.ResumeLayout(false);
            sCReMed.ResumeLayout(false);
            pnscReMedVs.ResumeLayout(false);
            pnsCReMedItem.ResumeLayout(false);
            pnsCReMedItemSend.ResumeLayout(false);

            pnsCReMedItemSend.PerformLayout();
            pnscReMedVs.PerformLayout();
            pnsCReMedItem.PerformLayout();
            sCReMedItem.PerformLayout();
            scReMedVs.PerformLayout();
            sCReMed.PerformLayout();
            tabReMed.Controls.Add(sCReMed);
        }

        private void GrfReMedItem_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfReMedItem.Row <= 0) return;
            if (grfReMedItem.Col <= 0) return;
            grfReMedItem[grfReMedItem.Row, colOrdDrugChk] = !Boolean.Parse(grfReMedItem[grfReMedItem.Row, colOrdDrugChk].ToString());
            Boolean chhk = false;
        }

        private void initComponentTabAdd()
        {
            int gapY = 30, gapX = 20, gapLine = 0, gapColName = 120;

            txtTabAddDrugSetId = new C1TextBox();
            txtTabAddDrugSetId.Font = fEdit;
            txtTabAddDrugSetId.Name = "txtTabAddDrugSetId";
            txtTabAddDrugSetId.Location = new System.Drawing.Point(gapX, gapY);
            txtTabAddDrugSetId.Size = new Size(240, 20);
            txtTabAddDrugSetId.Hide();

            lbTabAddDrugSetName = new Label();
            lbTabAddDrugSetName.Font = fEdit;
            lbTabAddDrugSetName.Text = "ชื่อ ชุดรายการ : ";
            lbTabAddDrugSetName.Name = "lbTabAddDrugSetName";
            lbTabAddDrugSetName.Location = new System.Drawing.Point(gapX, gapY);
            lbTabAddDrugSetName.AutoSize = true;
            size = bc.MeasureString(lbTabAddDrugSetName);
            txtTabAddDrugSetName = new C1TextBox();
            txtTabAddDrugSetName.Font = fEdit;
            txtTabAddDrugSetName.Name = "txtTabAddDrugSetName";
            txtTabAddDrugSetName.Location = new System.Drawing.Point(lbTabAddDrugSetName.Location.X + size.Width+5, lbTabAddDrugSetName.Location.Y);
            txtTabAddDrugSetName.Size = new Size(240, 20);

            gapY += gapY;
            lbTabAddDrugSetRemark = new Label();
            lbTabAddDrugSetRemark.Font = fEdit;
            lbTabAddDrugSetRemark.Text = "หมายเหตุ : ";
            lbTabAddDrugSetRemark.Name = "lbTabAddDrugSetRemark";
            lbTabAddDrugSetRemark.Location = new System.Drawing.Point(gapX, gapY);
            lbTabAddDrugSetRemark.AutoSize = true;
            size = bc.MeasureString(lbTabAddDrugSetRemark);
            txtTabAddDrugSetRemark = new C1TextBox();
            txtTabAddDrugSetRemark.Font = fEdit;
            txtTabAddDrugSetRemark.Name = "txtTabAddDrugSetRemark";
            txtTabAddDrugSetRemark.Location = new System.Drawing.Point(txtTabAddDrugSetName.Location.X, lbTabAddDrugSetRemark.Location.Y);
            txtTabAddDrugSetRemark.Size = new Size(240, 20);

            btnTabAddDrugSetSave = new C1Button();
            btnTabAddDrugSetSave.Text = "save ชุดรายการ";
            btnTabAddDrugSetSave.Name = "btnTabAddDrugSetSave";
            btnTabAddDrugSetSave.Location = new Point(txtTabAddDrugSetRemark.Location.X + txtTabAddDrugSetRemark.Width + 40, lbTabAddDrugSetRemark.Location.Y);
            btnTabAddDrugSetSave.Size = new Size(140, 40);

            pnAddNew.Controls.Add(txtTabAddDrugSetId);
            pnAddNew.Controls.Add(lbTabAddDrugSetName);
            pnAddNew.Controls.Add(txtTabAddDrugSetName);
            pnAddNew.Controls.Add(lbTabAddDrugSetRemark);
            pnAddNew.Controls.Add(txtTabAddDrugSetRemark);
            pnAddNew.Controls.Add(btnTabAddDrugSetSave);
        }
        private void GrfVs_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfReMedVs.Row < 0) return;
            if (grfReMedVs.Col < 0) return;
            setGrfVsItem();

        }
        private void setGrfVsItem()
        {
            FrmWaiting frmW = new FrmWaiting();
            frmW.StartPosition = FormStartPosition.CenterScreen;
            frmW.Show(this);
            try
            {
                String vn = "", vn1 = "", preno = "", vsDate="";
                DataTable dtOrder = new DataTable();
                preno = grfReMedVs[grfReMedVs.Row, colVsPreno] != null ? grfReMedVs[grfReMedVs.Row, colVsPreno].ToString() : "";
                vn = grfReMedVs[grfReMedVs.Row, colVsVn] != null ? grfReMedVs[grfReMedVs.Row, colVsVn].ToString() : "";
                vsDate = grfReMedVs[grfReMedVs.Row, colVsVsDate] != null ? grfReMedVs[grfReMedVs.Row, colVsVsDate].ToString() : "";
                if (vn.IndexOf("(") > 0)
                {
                    vn1 = vn.Substring(0, vn.IndexOf("("));
                }
                if (vn.IndexOf("/") > 0)
                {
                    vn1 = vn.Substring(0, vn.IndexOf("/"));
                }
                vsDate = bc.datetoDB(vsDate);
                dtOrder = bc.bcDB.vsDB.selectDrugOPD(hn, vn1, preno, vsDate);

                CellStyle cs = grfReMedItem.Styles.Add("bool");
                cs.DataType = typeof(bool);
                cs.ImageAlign = ImageAlignEnum.LeftCenter;

                grfReMedItem.Cols.Count = 10;
                //grfReMedItem.Rows.Count = 0;
                grfReMedItem.Rows.Count = 1;
                grfReMedItem.Rows.Count = dtOrder.Rows.Count + 1;
                //grfReMedItem.Cols.Count = 6;
                grfReMedItem.Cols[colOrdDrugNameT].Caption = "Drug Name";
                grfReMedItem.Cols[colOrdDrugId].Caption = "CODE";
                grfReMedItem.Cols[colOrdDrugQty].Caption = "QTY";
                grfReMedItem.Cols[colOrdDrugDate].Caption = "Date";
                grfReMedItem.Cols[colOrdAddDrugFr].Caption = "วิธีใช้";
                grfReMedItem.Cols[colOrdAddDrugIn].Caption = "ข้อควรระวัง";
                grfReMedItem.Cols[colOrdDrugUnit].Caption = "หน่วย";

                grfReMedItem.Cols[colOrdDrugNameT].Width = 400;
                grfReMedItem.Cols[colOrdDrugId].Width = 80;
                grfReMedItem.Cols[colOrdDrugQty].Width = 60;
                grfReMedItem.Cols[colOrdDrugDate].Width = 100;
                grfReMedItem.Cols[colOrdAddDrugFr].Width = 300;
                grfReMedItem.Cols[colOrdAddDrugIn].Width = 300;
                grfReMedItem.Cols[colOrdDrugUnit].Width = 70;
                grfReMedItem.Cols[colOrdDrugChk].Width = 40;

                CellRange rg = grfReMedItem.GetCellRange(1, colOrdDrugChk, grfReMedItem.Rows.Count - 1, colOrdDrugChk);
                rg.Style = cs;
                rg.Style = grfReMedItem.Styles["bool"];
                int i = 0;
                decimal aaa = 0;
                foreach (DataRow row1 in dtOrder.Rows)
                {
                    i++;
                    grfReMedItem[i, colOrdDrugId] = row1["MNC_PH_CD"].ToString();
                    grfReMedItem[i, colOrdDrugNameT] = row1["MNC_PH_TN"].ToString();
                    //grfReMedItem[i, colOrderMed] = "";
                    grfReMedItem[i, colOrdDrugQty] = row1["qty"].ToString();
                    grfReMedItem[i, colOrdDrugDate] = bc.datetoShow(row1["mnc_req_dat"]);
                    grfReMedItem[i, colOrdAddDrugFr] = row1["MNC_PH_DIR_DSC"].ToString();
                    grfReMedItem[i, colOrdAddDrugIn] = row1["MNC_PH_CAU_dsc"].ToString();
                    grfReMedItem[i, colOrdDrugUnit] = row1["MNC_PH_unt_cd"].ToString();
                    grfReMedItem[i, colOrdDrugChk] = true;
                    //row1[0] = (i - 2);
                }
                CellNoteManager mgr = new CellNoteManager(grfReMedItem);
                grfReMedItem.Cols[colOrdDrugNameT].AllowEditing = false;
                grfReMedItem.Cols[colOrdDrugQty].AllowEditing = false;
                grfReMedItem.Cols[colOrdDrugDate].AllowEditing = false;
                grfReMedItem.Cols[colOrdDrugId].AllowEditing = false;
                grfReMedItem.Cols[colOrdAddDrugIn].AllowEditing = false;
                grfReMedItem.Cols[colOrdAddDrugFr].AllowEditing = false;
                grfReMedItem.Cols[colOrdDrugDate].AllowEditing = false;
                grfReMedItem.Cols[colOrdDrugNameT].AllowSorting = false;
                grfReMedItem.Cols[colOrdDrugQty].AllowSorting = false;
                grfReMedItem.Cols[colOrdDrugDate].AllowSorting = false;
                grfReMedItem.Cols[colOrdAddDrugIn].AllowSorting = false;
                grfReMedItem.Cols[colOrdAddDrugFr].AllowSorting = false;

                grfReMedItem.Cols[colOrdDrugUnit].Visible = true;
                grfReMedItem.Cols[colOrdDrugtypcd].Visible = false;
            }
            catch (Exception ex)
            {

            }
            frmW.Dispose();
        }
        private void setGrfVsOPD()
        {
            grfReMedVs.Clear();
            grfReMedVs.Rows.Count = 1;
            grfReMedVs.Cols.Count = 8;

            grfReMedVs.Cols[colVsVsDate].Width = 100;
            grfReMedVs.Cols[colVsVn].Width = 80;
            grfReMedVs.Cols[colVsDept].Width = 240;
            grfReMedVs.Cols[colVsPreno].Width = 100;
            grfReMedVs.Cols[colVsStatus].Width = 60;
            grfReMedVs.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfReMedVs.Cols[colVsVsDate].Caption = "Visit Date";
            grfReMedVs.Cols[colVsVn].Caption = "VN";
            grfReMedVs.Cols[colVsDept].Caption = "แผนก";
            grfReMedVs.Cols[colVsPreno].Caption = "";
            grfReMedVs.Cols[colVsPreno].Visible = false;
            grfReMedVs.Cols[colVsVn].Visible = true;
            grfReMedVs.Cols[colVsAn].Visible = true;
            grfReMedVs.Cols[colVsAndate].Visible = false;
            grfReMedVs.Rows[0].Visible = false;
            grfReMedVs.Cols[0].Visible = false;
            grfReMedVs.Cols[colVsVsDate].AllowEditing = false;
            grfReMedVs.Cols[colVsVn].AllowEditing = false;
            grfReMedVs.Cols[colVsDept].AllowEditing = false;
            grfReMedVs.Cols[colVsPreno].AllowEditing = false;

            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dt = bc.bcDB.vsDB.selectVisitByHn4(hn, "O");
            int i = 1, j = 1, row = grfReMedVs.Rows.Count;
            
            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfReMedVs.Rows.Add();
                String status = "", vn = "";

                status = row1["MNC_PAT_FLAG"] != null ? row1["MNC_PAT_FLAG"].ToString().Equals("O") ? "OPD" : "IPD" : "-";
                vn = row1["MNC_VN_NO"].ToString() + "/" + row1["MNC_VN_SEQ"].ToString() + "(" + row1["MNC_VN_SUM"].ToString() + ")";
                rowa[colVsVsDate] = bc.datetoShow1(row1["mnc_date"].ToString());
                rowa[colVsVn] = vn;
                rowa[colVsStatus] = status;
                rowa[colVsPreno] = row1["mnc_pre_no"].ToString();
                rowa[colVsDept] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colVsAn] = row1["mnc_an_no"].ToString() + "/" + row1["mnc_an_yr"].ToString();
                rowa[colVsAndate] = bc.datetoShow1(row1["mnc_ad_date"].ToString());
            }
            //theme1.SetTheme(grfVs, "ExpressionDark");
        }
        private void setGrfDrug()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.drugDB.selectDrugAll();

            grfDrug.Rows.Count = 1;
            grfDrug.Cols.Count = 9;
            //grfLab.Cols[colOrderId].Visible = false;
            grfDrug.Rows.Count = dt.Rows.Count + 1;
            grfDrug.Cols.Count = dt.Rows.Count + 1;
            grfDrug.Cols[colOrdDrugId].Caption = "code";
            grfDrug.Cols[colOrdDrugNameT].Caption = "ชื่อ ยา";
            grfDrug.Cols[colOrdDrugtypcd].Caption = " typ cd";
            grfDrug.Cols[colOrdDrugUnit].Caption = "unit";
            grfDrug.Cols[colOrdAddDrugFr].Caption = "วิธีใช้";
            grfDrug.Cols[colOrdAddDrugIn].Caption = "ข้อควรระวัง";
            grfDrug.Cols[colOrdDrugQty].Caption = "qty";

            grfDrug.Cols[colOrdDrugId].Width = 100;
            grfDrug.Cols[colOrdDrugNameT].Width = 350;
            grfDrug.Cols[colOrdDrugtypcd].Width = 100;
            grfDrug.Cols[colOrdDrugUnit].Width = 200;
            grfDrug.Cols[colOrdAddDrugFr].Width = 300;
            grfDrug.Cols[colOrdAddDrugIn].Width = 300;
            grfDrug.Cols[colOrdDrugQty].Width = 60;
            int i = 0;
            decimal aaa = 0;
            for (int col = 0; col < dt.Columns.Count; ++col)
            {
                grfDrug.Cols[col + 1].DataType = dt.Columns[col].DataType;
                //grfDrug.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
                grfDrug.Cols[col + 1].Name = dt.Columns[col].ColumnName;
            }
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                if (i == 1) continue;
                grfDrug[i, colOrdDrugId] = row1["MNC_ph_cd"].ToString();
                grfDrug[i, colOrdDrugNameT] = row1["MNC_ph_tn"].ToString();
                grfDrug[i, colOrdDrugtypcd] = row1["MNC_ph_typ_cd"].ToString();
                grfDrug[i, colOrdDrugUnit] = row1["mnc_ph_unt_cd"].ToString();
                grfDrug[i, colOrdAddDrugFr] = row1["MNC_ph_dir_dsc"].ToString();
                grfDrug[i, colOrdAddDrugIn] = row1["MNC_ph_cau_dsc"].ToString();
                grfDrug[i, colOrdDrugQty] = "";
                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfDrug);
            //grfDrug.Cols[colXrayResult].Visible = false;
            grfDrug.Cols[colOrdDrugId].AllowEditing = false;
            grfDrug.Cols[colOrdDrugNameT].AllowEditing = false;
            grfDrug.Cols[colOrdDrugtypcd].AllowEditing = false;
            grfDrug.Cols[colOrdDrugUnit].AllowEditing = false;
            grfDrug.Cols[colOrdDrugDate].Visible = false;
            grfDrug.Cols[colOrdDrugChk].Visible = false;
            FilterRow fr = new FilterRow(grfDrug);
            grfDrug.AllowFiltering = true;
            grfDrug.AfterFilter += GrfDrug_AfterFilter;
            //}).Start();
        }

        private void GrfDrug_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            for (int col = grfDrug.Cols.Fixed; col < grfDrug.Cols.Count; ++col)
            {
                var filter = grfDrug.Cols[col].ActiveFilter;
            }
        }
        private void setGrfOrdLab()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.labDB.selectLabAll();

            grfLab.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfLab.Rows.Count = dt.Rows.Count + 1;
            grfLab.Cols.Count = dt.Rows.Count + 1;
            grfLab.Cols[colOrdLabId].Caption = "code";

            grfLab.Cols[colOrdLabName].Caption = "ชื่อ LAB";
            grfLab.Cols[colLabtypcd].Caption = "ประเภท";
            grfLab.Cols[colOrdlabUnit].Caption = "หน่วย";
            grfLab.Cols[colLabgrpdsc].Caption = "กลุ่ม";
            //grfLab.Cols[colOrdDrugUnit].Caption = "หน่วย";

            grfLab.Cols[colOrdDrugId].Width = 100;
            grfLab.Cols[colOrdLabName].Width = 350;
            grfLab.Cols[colOrdDrugtypcd].Width = 100;
            grfLab.Cols[colOrdDrugUnit].Width = 200;
            grfLab.Cols[colLabgrpdsc].Width = 300;
            int i = 0;
            decimal aaa = 0;
            for (int col = 0; col < dt.Columns.Count; ++col)
            {
                grfLab.Cols[col + 1].DataType = dt.Columns[col].DataType;
                //grfLab.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
                grfLab.Cols[col + 1].Name = dt.Columns[col].ColumnName;
            }
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                if (i == 1) continue;
                grfLab[i, colOrdLabId] = row1["MNC_lb_cd"].ToString();
                grfLab[i, colOrdLabName] = row1["MNC_lb_dsc"].ToString();
                grfLab[i, colOrdDrugtypcd] = row1["MNC_LB_TYP_DSC"].ToString();
                grfLab[i, colLabgrpdsc] = row1["MNC_LB_GRP_DSC"].ToString();
                grfLab[i, colOrdlabUnit] = "";

                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfLab);
            //grfOrdDrug.Cols[colXrayResult].Visible = false;
            grfLab.Cols[colOrdLabId].AllowEditing = false;
            grfLab.Cols[colOrdLabName].AllowEditing = false;
            grfLab.Cols[colOrdDrugtypcd].AllowEditing = false;
            grfLab.Cols[colLabgrpdsc].AllowEditing = false;
            grfLab.Cols[colOrdlabUnit].AllowEditing = false;
            FilterRow fr = new FilterRow(grfLab);
            grfLab.AllowFiltering = true;
            grfLab.AfterFilter += GrfLab_AfterFilter;
            //}).Start();
        }

        private void GrfLab_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            for (int col = grfLab.Cols.Fixed; col < grfLab.Cols.Count; ++col)
            {
                var filter = grfLab.Cols[col].ActiveFilter;
            }
        }
        private void setGrfOrdXray()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.xrDB.selectXrayAll();

            grfXray.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfXray.Rows.Count = dt.Rows.Count + 1;
            grfXray.Cols.Count = dt.Rows.Count + 1;
            grfXray.Cols[colOrdXrayId].Caption = "Code";
            grfXray.Cols[colOrdXrayName].Caption = "Xray Description";
            grfXray.Cols[colXraytypcd].Caption = "typ cd";
            grfXray.Cols[colXraygrpcd].Caption = "grp cd";
            grfXray.Cols[colXraygrpdsc].Caption = "Grp Description";

            grfXray.Cols[colOrdXrayId].Width = 100;
            grfXray.Cols[colOrdXrayName].Width = 350;
            grfXray.Cols[colXraytypcd].Width = 100;
            grfXray.Cols[colXraygrpcd].Width = 100;
            grfXray.Cols[colXraygrpdsc].Width = 200;

            int i = 0;
            decimal aaa = 0;
            for (int col = 0; col < dt.Columns.Count; ++col)
            {
                grfXray.Cols[col + 1].DataType = dt.Columns[col].DataType;
                grfXray.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
                grfXray.Cols[col + 1].Name = dt.Columns[col].ColumnName;
            }
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                if (i == 1) continue;
                grfXray[i, colOrdXrayId] = row1["mnc_xr_cd"].ToString();
                grfXray[i, colOrdXrayName] = row1["mnc_xr_dsc"].ToString();
                grfXray[i, colXraytypcd] = row1["mnc_xr_typ_cd"].ToString();
                grfXray[i, colXraygrpcd] = row1["MNC_XR_GRP_CD"].ToString();
                grfXray[i, colXraygrpdsc] = row1["MNC_XR_GRP_DSC"].ToString();

                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfXray);
            //grfOrdDrug.Cols[colXrayResult].Visible = false;
            grfXray.Cols[colOrdXrayId].AllowEditing = false;
            grfXray.Cols[colOrdXrayName].AllowEditing = false;
            grfXray.Cols[colXraytypcd].AllowEditing = false;
            grfXray.Cols[colXraygrpcd].AllowEditing = false;
            grfXray.Cols[colXraygrpdsc].AllowEditing = false;
            FilterRow fr = new FilterRow(grfXray);
            grfXray.AllowFiltering = true;
            grfXray.AfterFilter += GrfXray_AfterFilter;
            //}).Start();
        }

        private void GrfXray_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            for (int col = grfXray.Cols.Fixed; col < grfXray.Cols.Count; ++col)
            {
                var filter = grfXray.Cols[col].ActiveFilter;
            }
        }
        private void setGrfOrdSup()
        {
            DataTable dt = new DataTable();
            dt = bc.bcDB.drugDB.selectSupplyAll();

            grfSup.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfSup.Rows.Count = dt.Rows.Count + 1;
            grfSup.Cols.Count = dt.Rows.Count + 1;
            grfSup.Cols[colOrdDrugId].Caption = "วันที่สั่ง";
            grfSup.Cols[colOrdDrugNameT].Caption = "ชื่อ";
            grfSup.Cols[colOrdDrugtypcd].Caption = "Code ";
            grfSup.Cols[colOrdDrugUnit].Caption = "หน่วย";

            grfSup.Cols[colOrdDrugId].Width = 100;
            grfSup.Cols[colOrdDrugNameT].Width = 350;
            grfSup.Cols[colOrdDrugtypcd].Width = 100;
            grfSup.Cols[colOrdDrugUnit].Width = 200;

            int i = 0;
            decimal aaa = 0;
            for (int col = 0; col < dt.Columns.Count; ++col)
            {
                grfSup.Cols[col + 1].DataType = dt.Columns[col].DataType;
                grfSup.Cols[col + 1].Caption = dt.Columns[col].ColumnName;
                grfSup.Cols[col + 1].Name = dt.Columns[col].ColumnName;
            }
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                if (i == 1) continue;
                grfSup[i, colOrdDrugId] = row1["MNC_ph_cd"].ToString();
                grfSup[i, colOrdDrugNameT] = row1["MNC_ph_tn"].ToString();
                grfSup[i, colOrdDrugtypcd] = row1["MNC_ph_gn"].ToString();
                grfSup[i, colOrdDrugUnit] = row1["mnc_ph_unt_cd"].ToString();

                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfSup);
            //grfOrdDrug.Cols[colXrayResult].Visible = false;
            grfSup.Cols[colOrdDrugId].AllowEditing = false;
            grfSup.Cols[colOrdDrugNameT].AllowEditing = false;
            grfSup.Cols[colOrdDrugtypcd].AllowEditing = false;
            grfSup.Cols[colOrdDrugUnit].AllowEditing = false;
            FilterRow fr = new FilterRow(grfSup);
            grfSup.AllowFiltering = true;
            grfSup.AfterFilter += GrfSup_AfterFilter;
            //}).Start();
        }

        private void GrfSup_AfterFilter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

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
            setGrfVsOPD();
            setGrfDrug();
            setGrfOrdLab();
            setGrfOrdXray();
            setGrfOrdSup();
            btnTabAddDrugSetSave.Click += BtnTabAddDrugSetSave_Click;
            this.Load += FrmDoctorDrugSet_Load;

            theme1.SetTheme(grfView, "Office2010Red");
        }

        private void BtnTabAddDrugSetSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void initGrfAddItem()
        {
            grfAddNewItem = new C1FlexGrid();
            grfAddNewItem.Font = fEdit;
            grfAddNewItem.Dock = DockStyle.Fill;
            grfAddNewItem.Location = new Point(0, 0);
            grfAddNewItem.Rows.Count = 1;
            grfAddNewItem.DoubleClick += GrfAddNewItem_DoubleClick;
            pnAddNewItemGrf.Controls.Add(grfAddNewItem);

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
            if (grfDrug == null) return;
            if (grfDrug.Row <= 1) return;
            if (grfDrug.Col <= 0) return;

            setOrdDrugItem();
        }
        private void setOrdDrugItem()
        {
            if (grfDrug == null) return;
            if (grfDrug.Row <= 1) return;
            if (grfDrug.Col <= 0) return;
            String code = "";
            grfAddNewItem.Cols.Count = 11;

            DataTable dt = new DataTable();
            code = grfDrug[grfDrug.Row, colOrdDrugId].ToString();
            dt = bc.bcDB.drugDB.selectDrugByCode(code);
            grfAddNewItem.Cols[colOrdAddId].Caption = "หน่วย";
            grfAddNewItem.Cols[colOrdAddNameT].Caption = "หน่วย";
            grfAddNewItem.Cols[colOrdAddItemType].Caption = "หน่วย";
            grfAddNewItem.Cols[colOrdAddUnit].Caption = "หน่วย";
            grfAddNewItem.Cols[colOrdAddDrugFr1].Caption = "หน่วย";
            grfAddNewItem.Cols[colOrdAddDrugIn1].Caption = "หน่วย";

            grfAddNewItem.Cols[colOrdAddId].Width = 100;
            grfAddNewItem.Cols[colOrdAddNameT].Width = 100;
            grfAddNewItem.Cols[colOrdAddItemType].Width = 100;
            grfAddNewItem.Cols[colOrdAddUnit].Width = 100;
            grfAddNewItem.Cols[colOrdAddDrugFr1].Width = 100;
            grfAddNewItem.Cols[colOrdAddDrugIn1].Width = 100;
            grfAddNewItem.Cols[colOrdAddDrugIn].Visible = false;

            Row rowdrug = grfAddNewItem.Rows.Add();
            rowdrug[colOrdAddId] = dt.Rows[0]["MNC_ph_cd"].ToString();
            rowdrug[colOrdAddNameT] = dt.Rows[0]["MNC_ph_tn"].ToString();
            rowdrug[colOrdAddItemType] = dt.Rows[0]["mnc_ph_typ_cd"].ToString();
            rowdrug[colOrdAddUnit] = dt.Rows[0]["mnc_ph_unt_cd"].ToString();
            rowdrug[colOrdAddDrugFr1] = dt.Rows[0]["MNC_ph_dir_dsc"].ToString();
            rowdrug[colOrdAddDrugIn1] = dt.Rows[0]["MNC_ph_cau_dsc"].ToString();
            //C1TextBox txtItmId = (C1TextBox)this.Controls["txtItmId"];
            //C1TextBox lbItmName = (C1TextBox)this.Controls["lbItmName"];
            txtItmNewId.Value = dt.Rows[0]["MNC_ph_cd"].ToString();
            txtItmNewName.Value = dt.Rows[0]["MNC_ph_tn"].ToString();
            txtItmNewFre.Value = dt.Rows[0]["MNC_ph_dir_dsc"].ToString();
            txtItmNewIn1.Value = dt.Rows[0]["MNC_ph_cau_dsc"].ToString();
            txtItmNewQty.Value = "1";
            //txtItmIn2.Value = dt.Rows[0]["MNC_ph_tn"].ToString();
            txtItmNewFre.Show();
            txtItmNewIn1.Show();
            txtItmNewQty.Show();
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
            btnReMedItemSen.Location = new Point(pnsCReMedItemSend.Width - 60, pnsCReMedItemSend.Height - btnReMedItemSen.Height - 10);
            scAddNew.SizeRatio = 17;
            scReMedVs.SizeRatio = 17;
        }
    }
}
