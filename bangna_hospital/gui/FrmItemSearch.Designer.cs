namespace bangna_hospital.gui
{
    partial class FrmItemSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmItemSearch));
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.lfsb1 = new C1.Win.C1Ribbon.RibbonLabel();
            this.lfsb2 = new C1.Win.C1Ribbon.RibbonLabel();
            this.btnClose = new C1.Win.C1Ribbon.RibbonButton();
            this.c1SplitContainer1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.c1DockingTab1 = new C1.Win.C1Command.C1DockingTab();
            this.c1DockingTabPage1 = new C1.Win.C1Command.C1DockingTabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1DockingTabPage2 = new C1.Win.C1Command.C1DockingTabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnItemLab = new System.Windows.Forms.Panel();
            this.pnTopLab = new System.Windows.Forms.Panel();
            this.c1DockingTabPage3 = new C1.Win.C1Command.C1DockingTabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnItemXray = new System.Windows.Forms.Panel();
            this.pnTopXray = new System.Windows.Forms.Panel();
            this.c1DockingTabPage4 = new C1.Win.C1Command.C1DockingTabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnItemProcedure = new System.Windows.Forms.Panel();
            this.pnTopProcedure = new System.Windows.Forms.Panel();
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.pnItems = new System.Windows.Forms.Panel();
            this.pnItemDrug = new System.Windows.Forms.Panel();
            this.pnTopDrug = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).BeginInit();
            this.c1SplitContainer1.SuspendLayout();
            this.c1SplitterPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).BeginInit();
            this.c1DockingTab1.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.c1DockingTabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.c1DockingTabPage3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.c1DockingTabPage4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.c1SplitterPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1StatusBar1.LeftPaneItems.Add(this.lfsb1);
            this.c1StatusBar1.LeftPaneItems.Add(this.lfsb2);
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 717);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.btnClose);
            this.c1StatusBar1.Size = new System.Drawing.Size(963, 22);
            // 
            // lfsb1
            // 
            this.lfsb1.Name = "lfsb1";
            this.lfsb1.Text = "Label";
            // 
            // lfsb2
            // 
            this.lfsb2.Name = "lfsb2";
            this.lfsb2.Text = "Label";
            // 
            // btnClose
            // 
            this.btnClose.Name = "btnClose";
            this.btnClose.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnClose.SmallImage")));
            this.btnClose.Text = "กลับหน้าหลัก";
            // 
            // c1SplitContainer1
            // 
            this.c1SplitContainer1.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.c1SplitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1SplitContainer1.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.c1SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SplitContainer1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.c1SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.c1SplitContainer1.Name = "c1SplitContainer1";
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel1);
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel2);
            this.c1SplitContainer1.Size = new System.Drawing.Size(963, 717);
            this.c1SplitContainer1.TabIndex = 1;
            // 
            // c1SplitterPanel1
            // 
            this.c1SplitterPanel1.Controls.Add(this.c1DockingTab1);
            this.c1SplitterPanel1.Height = 402;
            this.c1SplitterPanel1.Location = new System.Drawing.Point(0, 21);
            this.c1SplitterPanel1.Name = "c1SplitterPanel1";
            this.c1SplitterPanel1.Size = new System.Drawing.Size(963, 381);
            this.c1SplitterPanel1.SizeRatio = 56.418D;
            this.c1SplitterPanel1.TabIndex = 0;
            this.c1SplitterPanel1.Text = "Panel 1";
            // 
            // c1DockingTab1
            // 
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage1);
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage2);
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage3);
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage4);
            this.c1DockingTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1DockingTab1.Location = new System.Drawing.Point(0, 0);
            this.c1DockingTab1.Name = "c1DockingTab1";
            this.c1DockingTab1.Size = new System.Drawing.Size(963, 381);
            this.c1DockingTab1.TabIndex = 0;
            this.c1DockingTab1.TabsSpacing = 5;
            this.c1DockingTab1.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            // 
            // c1DockingTabPage1
            // 
            this.c1DockingTabPage1.Controls.Add(this.panel1);
            this.c1DockingTabPage1.Location = new System.Drawing.Point(1, 24);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(961, 356);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Drug";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnItemDrug);
            this.panel1.Controls.Add(this.pnTopDrug);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(961, 356);
            this.panel1.TabIndex = 0;
            // 
            // c1DockingTabPage2
            // 
            this.c1DockingTabPage2.Controls.Add(this.panel3);
            this.c1DockingTabPage2.Location = new System.Drawing.Point(1, 24);
            this.c1DockingTabPage2.Name = "c1DockingTabPage2";
            this.c1DockingTabPage2.Size = new System.Drawing.Size(961, 356);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Lab";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnItemLab);
            this.panel3.Controls.Add(this.pnTopLab);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(961, 356);
            this.panel3.TabIndex = 0;
            // 
            // pnItemLab
            // 
            this.pnItemLab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnItemLab.Location = new System.Drawing.Point(0, 119);
            this.pnItemLab.Name = "pnItemLab";
            this.pnItemLab.Size = new System.Drawing.Size(961, 237);
            this.pnItemLab.TabIndex = 1;
            // 
            // pnTopLab
            // 
            this.pnTopLab.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTopLab.Location = new System.Drawing.Point(0, 0);
            this.pnTopLab.Name = "pnTopLab";
            this.pnTopLab.Size = new System.Drawing.Size(961, 119);
            this.pnTopLab.TabIndex = 0;
            // 
            // c1DockingTabPage3
            // 
            this.c1DockingTabPage3.Controls.Add(this.panel4);
            this.c1DockingTabPage3.Location = new System.Drawing.Point(1, 24);
            this.c1DockingTabPage3.Name = "c1DockingTabPage3";
            this.c1DockingTabPage3.Size = new System.Drawing.Size(961, 356);
            this.c1DockingTabPage3.TabIndex = 2;
            this.c1DockingTabPage3.Text = "Xray";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pnItemXray);
            this.panel4.Controls.Add(this.pnTopXray);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(961, 356);
            this.panel4.TabIndex = 0;
            // 
            // pnItemXray
            // 
            this.pnItemXray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnItemXray.Location = new System.Drawing.Point(0, 119);
            this.pnItemXray.Name = "pnItemXray";
            this.pnItemXray.Size = new System.Drawing.Size(961, 237);
            this.pnItemXray.TabIndex = 3;
            // 
            // pnTopXray
            // 
            this.pnTopXray.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTopXray.Location = new System.Drawing.Point(0, 0);
            this.pnTopXray.Name = "pnTopXray";
            this.pnTopXray.Size = new System.Drawing.Size(961, 119);
            this.pnTopXray.TabIndex = 2;
            // 
            // c1DockingTabPage4
            // 
            this.c1DockingTabPage4.Controls.Add(this.panel5);
            this.c1DockingTabPage4.Location = new System.Drawing.Point(1, 24);
            this.c1DockingTabPage4.Name = "c1DockingTabPage4";
            this.c1DockingTabPage4.Size = new System.Drawing.Size(961, 356);
            this.c1DockingTabPage4.TabIndex = 3;
            this.c1DockingTabPage4.Text = "Procedure";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pnItemProcedure);
            this.panel5.Controls.Add(this.pnTopProcedure);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(961, 356);
            this.panel5.TabIndex = 0;
            // 
            // pnItemProcedure
            // 
            this.pnItemProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnItemProcedure.Location = new System.Drawing.Point(0, 119);
            this.pnItemProcedure.Name = "pnItemProcedure";
            this.pnItemProcedure.Size = new System.Drawing.Size(961, 237);
            this.pnItemProcedure.TabIndex = 3;
            // 
            // pnTopProcedure
            // 
            this.pnTopProcedure.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTopProcedure.Location = new System.Drawing.Point(0, 0);
            this.pnTopProcedure.Name = "pnTopProcedure";
            this.pnTopProcedure.Size = new System.Drawing.Size(961, 119);
            this.pnTopProcedure.TabIndex = 2;
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.Controls.Add(this.pnItems);
            this.c1SplitterPanel2.Height = 311;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(0, 427);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(963, 290);
            this.c1SplitterPanel2.TabIndex = 1;
            this.c1SplitterPanel2.Text = "Panel 2";
            // 
            // pnItems
            // 
            this.pnItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnItems.Location = new System.Drawing.Point(0, 0);
            this.pnItems.Name = "pnItems";
            this.pnItems.Size = new System.Drawing.Size(963, 290);
            this.pnItems.TabIndex = 0;
            // 
            // pnItemDrug
            // 
            this.pnItemDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnItemDrug.Location = new System.Drawing.Point(0, 119);
            this.pnItemDrug.Name = "pnItemDrug";
            this.pnItemDrug.Size = new System.Drawing.Size(961, 237);
            this.pnItemDrug.TabIndex = 3;
            // 
            // pnTopDrug
            // 
            this.pnTopDrug.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTopDrug.Location = new System.Drawing.Point(0, 0);
            this.pnTopDrug.Name = "pnTopDrug";
            this.pnTopDrug.Size = new System.Drawing.Size(961, 119);
            this.pnTopDrug.TabIndex = 2;
            // 
            // FrmItemSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 739);
            this.Controls.Add(this.c1SplitContainer1);
            this.Controls.Add(this.c1StatusBar1);
            this.Name = "FrmItemSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmItemSearch";
            this.Load += new System.EventHandler(this.FrmItemSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).EndInit();
            this.c1SplitContainer1.ResumeLayout(false);
            this.c1SplitterPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).EndInit();
            this.c1DockingTab1.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.c1DockingTabPage2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.c1DockingTabPage3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.c1DockingTabPage4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.c1SplitterPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private C1.Win.C1Command.C1DockingTab c1DockingTab1;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage1;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage2;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage3;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel pnItems;
        private System.Windows.Forms.Panel pnItemLab;
        private System.Windows.Forms.Panel pnTopLab;
        private C1.Win.C1Ribbon.RibbonLabel lfsb1;
        private C1.Win.C1Ribbon.RibbonLabel lfsb2;
        private C1.Win.C1Ribbon.RibbonButton btnClose;
        private System.Windows.Forms.Panel pnItemXray;
        private System.Windows.Forms.Panel pnTopXray;
        private System.Windows.Forms.Panel pnItemProcedure;
        private System.Windows.Forms.Panel pnTopProcedure;
        private System.Windows.Forms.Panel pnItemDrug;
        private System.Windows.Forms.Panel pnTopDrug;
    }
}