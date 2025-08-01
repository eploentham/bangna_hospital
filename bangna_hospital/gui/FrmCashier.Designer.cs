namespace bangna_hospital.gui
{
    partial class FrmCashier
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.lfSbMessage = new C1.Win.C1Ribbon.RibbonLabel();
            this.lfSbStation = new C1.Win.C1Ribbon.RibbonLabel();
            this.lfSbComp = new C1.Win.C1Ribbon.RibbonLabel();
            this.lfSbInsur = new C1.Win.C1Ribbon.RibbonLabel();
            this.rgSearch = new C1.Win.C1Ribbon.RibbonTextBox();
            this.rgSbModule = new C1.Win.C1Ribbon.RibbonLabel();
            this.rgSb1 = new C1.Win.C1Ribbon.RibbonLabel();
            this.rgSbHIV = new C1.Win.C1Ribbon.RibbonLabel();
            this.rgSbAFB = new C1.Win.C1Ribbon.RibbonLabel();
            this.rgPrn = new C1.Win.C1Ribbon.RibbonButton();
            this.tabMain = new C1.Win.C1Command.C1DockingTab();
            this.tabOper = new C1.Win.C1Command.C1DockingTabPage();
            this.tabFinish = new C1.Win.C1Command.C1DockingTabPage();
            this.scFinish = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.pnGrfFinishView = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.pnGrfFinishInv = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.c1DockingTabPage3 = new C1.Win.C1Command.C1DockingTabPage();
            this.tabReport = new C1.Win.C1Command.C1DockingTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabFinish.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scFinish)).BeginInit();
            this.scFinish.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1StatusBar1.LeftPaneItems.Add(this.lfSbMessage);
            this.c1StatusBar1.LeftPaneItems.Add(this.lfSbStation);
            this.c1StatusBar1.LeftPaneItems.Add(this.lfSbComp);
            this.c1StatusBar1.LeftPaneItems.Add(this.lfSbInsur);
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 762);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.rgSearch);
            this.c1StatusBar1.RightPaneItems.Add(this.rgSbModule);
            this.c1StatusBar1.RightPaneItems.Add(this.rgSb1);
            this.c1StatusBar1.RightPaneItems.Add(this.rgSbHIV);
            this.c1StatusBar1.RightPaneItems.Add(this.rgSbAFB);
            this.c1StatusBar1.RightPaneItems.Add(this.rgPrn);
            this.c1StatusBar1.Size = new System.Drawing.Size(1303, 22);
            // 
            // lfSbMessage
            // 
            this.lfSbMessage.Name = "lfSbMessage";
            this.lfSbMessage.Text = "Label";
            // 
            // lfSbStation
            // 
            this.lfSbStation.Name = "lfSbStation";
            this.lfSbStation.Text = "Label";
            // 
            // lfSbComp
            // 
            this.lfSbComp.Name = "lfSbComp";
            this.lfSbComp.Text = "Label";
            // 
            // lfSbInsur
            // 
            this.lfSbInsur.Name = "lfSbInsur";
            this.lfSbInsur.Text = "Label";
            // 
            // rgSearch
            // 
            this.rgSearch.Label = "ค้นหา:";
            this.rgSearch.Name = "rgSearch";
            // 
            // rgSbModule
            // 
            this.rgSbModule.Name = "rgSbModule";
            this.rgSbModule.Text = "Label";
            // 
            // rgSb1
            // 
            this.rgSb1.Name = "rgSb1";
            this.rgSb1.Text = "Label";
            // 
            // rgSbHIV
            // 
            this.rgSbHIV.Name = "rgSbHIV";
            this.rgSbHIV.Text = "Label";
            // 
            // rgSbAFB
            // 
            this.rgSbAFB.Name = "rgSbAFB";
            this.rgSbAFB.Text = "Label";
            // 
            // rgPrn
            // 
            this.rgPrn.Name = "rgPrn";
            this.rgPrn.SmallImage = global::bangna_hospital.Properties.Resources.printer_blue16;
            this.rgPrn.Text = "ใบเสร็จ";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabOper);
            this.tabMain.Controls.Add(this.tabFinish);
            this.tabMain.Controls.Add(this.c1DockingTabPage3);
            this.tabMain.Controls.Add(this.tabReport);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.Size = new System.Drawing.Size(1303, 762);
            this.tabMain.TabIndex = 2;
            this.tabMain.TabsSpacing = 5;
            // 
            // tabOper
            // 
            this.tabOper.Location = new System.Drawing.Point(1, 24);
            this.tabOper.Name = "tabOper";
            this.tabOper.Size = new System.Drawing.Size(1301, 737);
            this.tabOper.TabIndex = 0;
            this.tabOper.Text = "รอรับชำระ";
            // 
            // tabFinish
            // 
            this.tabFinish.Controls.Add(this.scFinish);
            this.tabFinish.Location = new System.Drawing.Point(1, 24);
            this.tabFinish.Name = "tabFinish";
            this.tabFinish.Size = new System.Drawing.Size(1301, 737);
            this.tabFinish.TabIndex = 1;
            this.tabFinish.Text = "รับชำระเรียบร้อย";
            // 
            // scFinish
            // 
            this.scFinish.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.scFinish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.scFinish.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.scFinish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scFinish.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.scFinish.Location = new System.Drawing.Point(0, 0);
            this.scFinish.Name = "scFinish";
            this.scFinish.Panels.Add(this.pnGrfFinishView);
            this.scFinish.Panels.Add(this.pnGrfFinishInv);
            this.scFinish.Size = new System.Drawing.Size(1301, 737);
            this.scFinish.TabIndex = 0;
            // 
            // pnGrfFinishView
            // 
            this.pnGrfFinishView.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.pnGrfFinishView.Location = new System.Drawing.Point(0, 21);
            this.pnGrfFinishView.Name = "pnGrfFinishView";
            this.pnGrfFinishView.Size = new System.Drawing.Size(648, 716);
            this.pnGrfFinishView.TabIndex = 0;
            this.pnGrfFinishView.Text = "Panel 1";
            this.pnGrfFinishView.Width = 648;
            // 
            // pnGrfFinishInv
            // 
            this.pnGrfFinishInv.Height = 737;
            this.pnGrfFinishInv.Location = new System.Drawing.Point(652, 21);
            this.pnGrfFinishInv.Name = "pnGrfFinishInv";
            this.pnGrfFinishInv.Size = new System.Drawing.Size(649, 716);
            this.pnGrfFinishInv.TabIndex = 1;
            this.pnGrfFinishInv.Text = "Panel 2";
            // 
            // c1DockingTabPage3
            // 
            this.c1DockingTabPage3.Location = new System.Drawing.Point(1, 24);
            this.c1DockingTabPage3.Name = "c1DockingTabPage3";
            this.c1DockingTabPage3.Size = new System.Drawing.Size(1301, 737);
            this.c1DockingTabPage3.TabIndex = 2;
            this.c1DockingTabPage3.Text = "Page3";
            // 
            // tabReport
            // 
            this.tabReport.Location = new System.Drawing.Point(1, 24);
            this.tabReport.Name = "tabReport";
            this.tabReport.Size = new System.Drawing.Size(1301, 737);
            this.tabReport.TabIndex = 3;
            this.tabReport.Text = "Report";
            // 
            // FrmCashier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 784);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.c1StatusBar1);
            this.Name = "FrmCashier";
            this.Text = "FrmCashier";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmCashier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabFinish.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scFinish)).EndInit();
            this.scFinish.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Ribbon.RibbonLabel lfSbMessage;
        private C1.Win.C1Ribbon.RibbonLabel lfSbStation;
        private C1.Win.C1Ribbon.RibbonLabel lfSbComp;
        private C1.Win.C1Ribbon.RibbonLabel lfSbInsur;
        private C1.Win.C1Ribbon.RibbonLabel rgSbModule;
        private C1.Win.C1Ribbon.RibbonLabel rgSb1;
        private C1.Win.C1Ribbon.RibbonLabel rgSbHIV;
        private C1.Win.C1Ribbon.RibbonLabel rgSbAFB;
        private C1.Win.C1Ribbon.RibbonButton rgPrn;
        private C1.Win.C1Command.C1DockingTab tabMain;
        private C1.Win.C1Command.C1DockingTabPage tabOper;
        private C1.Win.C1Command.C1DockingTabPage tabFinish;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage3;
        private C1.Win.C1Command.C1DockingTabPage tabReport;
        private C1.Win.C1SplitContainer.C1SplitContainer scFinish;
        private C1.Win.C1SplitContainer.C1SplitterPanel pnGrfFinishView;
        private C1.Win.C1SplitContainer.C1SplitterPanel pnGrfFinishInv;
        private C1.Win.C1Ribbon.RibbonTextBox rgSearch;
    }
}