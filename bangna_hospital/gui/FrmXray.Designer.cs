namespace bangna_hospital.gui
{
    partial class FrmXray
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
            this.tcMain = new C1.Win.C1Command.C1DockingTab();
            this.tabOper = new C1.Win.C1Command.C1DockingTabPage();
            this.tabFinish = new C1.Win.C1Command.C1DockingTabPage();
            this.spOper = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.spOperList = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.lfSbMessage = new C1.Win.C1Ribbon.RibbonLabel();
            this.lfSbStatus = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonLabel1 = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonLabel2 = new C1.Win.C1Ribbon.RibbonLabel();
            this.lfSbStation = new C1.Win.C1Ribbon.RibbonLabel();
            this.rgSbModule = new C1.Win.C1Ribbon.RibbonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcMain)).BeginInit();
            this.tcMain.SuspendLayout();
            this.tabOper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spOper)).BeginInit();
            this.spOper.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1StatusBar1.LeftPaneItems.Add(this.lfSbStation);
            this.c1StatusBar1.LeftPaneItems.Add(this.lfSbStatus);
            this.c1StatusBar1.LeftPaneItems.Add(this.lfSbMessage);
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 740);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.ribbonLabel1);
            this.c1StatusBar1.RightPaneItems.Add(this.ribbonLabel2);
            this.c1StatusBar1.RightPaneItems.Add(this.rgSbModule);
            this.c1StatusBar1.Size = new System.Drawing.Size(1105, 22);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabOper);
            this.tcMain.Controls.Add(this.tabFinish);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.Size = new System.Drawing.Size(1105, 740);
            this.tcMain.TabIndex = 1;
            this.tcMain.TabsSpacing = 5;
            // 
            // tabOper
            // 
            this.tabOper.Controls.Add(this.spOper);
            this.tabOper.Location = new System.Drawing.Point(1, 26);
            this.tabOper.Name = "tabOper";
            this.tabOper.Size = new System.Drawing.Size(1103, 713);
            this.tabOper.TabIndex = 0;
            this.tabOper.Text = "คนไข้ในแผนก";
            // 
            // tabFinish
            // 
            this.tabFinish.Location = new System.Drawing.Point(1, 26);
            this.tabFinish.Name = "tabFinish";
            this.tabFinish.Size = new System.Drawing.Size(1103, 713);
            this.tabFinish.TabIndex = 1;
            this.tabFinish.Text = "ตรวจเสร็จแล้ว close visit";
            // 
            // spOper
            // 
            this.spOper.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.spOper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.spOper.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.spOper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spOper.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.spOper.Location = new System.Drawing.Point(0, 0);
            this.spOper.Name = "spOper";
            this.spOper.Panels.Add(this.spOperList);
            this.spOper.Panels.Add(this.c1SplitterPanel2);
            this.spOper.Size = new System.Drawing.Size(1103, 713);
            this.spOper.TabIndex = 0;
            // 
            // spOperList
            // 
            this.spOperList.Height = 440;
            this.spOperList.Location = new System.Drawing.Point(0, 21);
            this.spOperList.Name = "spOperList";
            this.spOperList.Size = new System.Drawing.Size(1103, 419);
            this.spOperList.SizeRatio = 62.059D;
            this.spOperList.TabIndex = 0;
            this.spOperList.Text = "Panel 1";
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.Height = 269;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(0, 465);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(1103, 248);
            this.c1SplitterPanel2.TabIndex = 1;
            this.c1SplitterPanel2.Text = "Panel 2";
            // 
            // lfSbMessage
            // 
            this.lfSbMessage.Name = "lfSbMessage";
            this.lfSbMessage.Text = "Label";
            // 
            // lfSbStatus
            // 
            this.lfSbStatus.Name = "lfSbStatus";
            this.lfSbStatus.Text = "Label";
            // 
            // ribbonLabel1
            // 
            this.ribbonLabel1.Name = "ribbonLabel1";
            this.ribbonLabel1.Text = "Label";
            // 
            // ribbonLabel2
            // 
            this.ribbonLabel2.Name = "ribbonLabel2";
            this.ribbonLabel2.Text = "Label";
            // 
            // lfSbStation
            // 
            this.lfSbStation.Name = "lfSbStation";
            this.lfSbStation.Text = "Label";
            // 
            // rgSbModule
            // 
            this.rgSbModule.Name = "rgSbModule";
            this.rgSbModule.Text = "Label";
            // 
            // FrmXray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 762);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.c1StatusBar1);
            this.Name = "FrmXray";
            this.Text = "FrmXray";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmXray_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcMain)).EndInit();
            this.tcMain.ResumeLayout(false);
            this.tabOper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spOper)).EndInit();
            this.spOper.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Command.C1DockingTab tcMain;
        private C1.Win.C1Command.C1DockingTabPage tabOper;
        private C1.Win.C1Command.C1DockingTabPage tabFinish;
        private C1.Win.C1SplitContainer.C1SplitContainer spOper;
        private C1.Win.C1SplitContainer.C1SplitterPanel spOperList;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private C1.Win.C1Ribbon.RibbonLabel lfSbMessage;
        private C1.Win.C1Ribbon.RibbonLabel lfSbStatus;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabel1;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabel2;
        private C1.Win.C1Ribbon.RibbonLabel lfSbStation;
        private C1.Win.C1Ribbon.RibbonLabel rgSbModule;
    }
}