
namespace bangna_hospital.gui
{
    partial class FrmLisLink
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
            this.lb1 = new C1.Win.C1Ribbon.RibbonLabel();
            this.lbYear = new C1.Win.C1Ribbon.RibbonLabel();
            this.rb1 = new C1.Win.C1Ribbon.RibbonLabel();
            this.c1DockingTab1 = new C1.Win.C1Command.C1DockingTab();
            this.tabReq = new C1.Win.C1Command.C1DockingTabPage();
            this.pnReq = new System.Windows.Forms.Panel();
            this.c1SplitContainer1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.pnRequest = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.pnItems = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.tabRes = new C1.Win.C1Command.C1DockingTabPage();
            this.pnRes = new System.Windows.Forms.Panel();
            this.lb3 = new C1.Win.C1Ribbon.RibbonLabel();
            this.rb2 = new C1.Win.C1Ribbon.RibbonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).BeginInit();
            this.c1DockingTab1.SuspendLayout();
            this.tabReq.SuspendLayout();
            this.pnReq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).BeginInit();
            this.c1SplitContainer1.SuspendLayout();
            this.tabRes.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1StatusBar1.LeftPaneItems.Add(this.lb1);
            this.c1StatusBar1.LeftPaneItems.Add(this.lbYear);
            this.c1StatusBar1.LeftPaneItems.Add(this.lb3);
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 711);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.rb1);
            this.c1StatusBar1.RightPaneItems.Add(this.rb2);
            this.c1StatusBar1.Size = new System.Drawing.Size(1143, 22);
            // 
            // lb1
            // 
            this.lb1.Name = "lb1";
            this.lb1.Text = "Label";
            // 
            // lbYear
            // 
            this.lbYear.Name = "lbYear";
            this.lbYear.Text = "Label";
            // 
            // rb1
            // 
            this.rb1.Name = "rb1";
            this.rb1.Text = "Label";
            // 
            // c1DockingTab1
            // 
            this.c1DockingTab1.Controls.Add(this.tabReq);
            this.c1DockingTab1.Controls.Add(this.tabRes);
            this.c1DockingTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1DockingTab1.Location = new System.Drawing.Point(0, 0);
            this.c1DockingTab1.Name = "c1DockingTab1";
            this.c1DockingTab1.Size = new System.Drawing.Size(1143, 711);
            this.c1DockingTab1.TabIndex = 1;
            this.c1DockingTab1.TabsSpacing = 5;
            // 
            // tabReq
            // 
            this.tabReq.Controls.Add(this.pnReq);
            this.tabReq.Location = new System.Drawing.Point(1, 24);
            this.tabReq.Name = "tabReq";
            this.tabReq.Size = new System.Drawing.Size(1141, 686);
            this.tabReq.TabIndex = 0;
            this.tabReq.Text = "Request";
            // 
            // pnReq
            // 
            this.pnReq.Controls.Add(this.c1SplitContainer1);
            this.pnReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnReq.Location = new System.Drawing.Point(0, 0);
            this.pnReq.Name = "pnReq";
            this.pnReq.Size = new System.Drawing.Size(1141, 686);
            this.pnReq.TabIndex = 0;
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
            this.c1SplitContainer1.Panels.Add(this.pnRequest);
            this.c1SplitContainer1.Panels.Add(this.pnItems);
            this.c1SplitContainer1.Size = new System.Drawing.Size(1141, 686);
            this.c1SplitContainer1.TabIndex = 0;
            // 
            // pnRequest
            // 
            this.pnRequest.Collapsible = true;
            this.pnRequest.Height = 477;
            this.pnRequest.Location = new System.Drawing.Point(0, 21);
            this.pnRequest.Name = "pnRequest";
            this.pnRequest.Size = new System.Drawing.Size(1141, 449);
            this.pnRequest.SizeRatio = 70D;
            this.pnRequest.TabIndex = 0;
            this.pnRequest.Text = "Request";
            // 
            // pnItems
            // 
            this.pnItems.Height = 205;
            this.pnItems.Location = new System.Drawing.Point(0, 502);
            this.pnItems.Name = "pnItems";
            this.pnItems.Size = new System.Drawing.Size(1141, 184);
            this.pnItems.TabIndex = 1;
            this.pnItems.Text = "Lab Items";
            // 
            // tabRes
            // 
            this.tabRes.Controls.Add(this.pnRes);
            this.tabRes.Location = new System.Drawing.Point(1, 24);
            this.tabRes.Name = "tabRes";
            this.tabRes.Size = new System.Drawing.Size(1141, 686);
            this.tabRes.TabIndex = 1;
            this.tabRes.Text = "Result";
            // 
            // pnRes
            // 
            this.pnRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnRes.Location = new System.Drawing.Point(0, 0);
            this.pnRes.Name = "pnRes";
            this.pnRes.Size = new System.Drawing.Size(1141, 686);
            this.pnRes.TabIndex = 0;
            // 
            // lb3
            // 
            this.lb3.Name = "lb3";
            this.lb3.Text = "Label";
            // 
            // rb2
            // 
            this.rb2.Name = "rb2";
            this.rb2.Text = "Label";
            // 
            // FrmLisLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 733);
            this.Controls.Add(this.c1DockingTab1);
            this.Controls.Add(this.c1StatusBar1);
            this.Name = "FrmLisLink";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLisLink";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLisLink_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).EndInit();
            this.c1DockingTab1.ResumeLayout(false);
            this.tabReq.ResumeLayout(false);
            this.pnReq.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).EndInit();
            this.c1SplitContainer1.ResumeLayout(false);
            this.tabRes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Command.C1DockingTab c1DockingTab1;
        private C1.Win.C1Command.C1DockingTabPage tabReq;
        private C1.Win.C1Command.C1DockingTabPage tabRes;
        private System.Windows.Forms.Panel pnReq;
        private System.Windows.Forms.Panel pnRes;
        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer1;
        private C1.Win.C1SplitContainer.C1SplitterPanel pnRequest;
        private C1.Win.C1SplitContainer.C1SplitterPanel pnItems;
        private C1.Win.C1Ribbon.RibbonLabel lb1;
        private C1.Win.C1Ribbon.RibbonLabel rb1;
        private C1.Win.C1Ribbon.RibbonLabel lbYear;
        private C1.Win.C1Ribbon.RibbonLabel lb3;
        private C1.Win.C1Ribbon.RibbonLabel rb2;
    }
}