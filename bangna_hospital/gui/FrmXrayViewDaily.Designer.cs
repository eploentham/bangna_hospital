namespace bangna_hospital.gui
{
    partial class FrmXrayViewDaily
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
            this.sB = new C1.Win.C1Ribbon.C1StatusBar();
            this.tC1 = new C1.Win.C1Command.C1DockingTab();
            this.tabRequest = new C1.Win.C1Command.C1DockingTabPage();
            this.pnRequest = new System.Windows.Forms.Panel();
            this.tabProcess = new C1.Win.C1Command.C1DockingTabPage();
            this.pnProcess = new System.Windows.Forms.Panel();
            this.tabFinish = new C1.Win.C1Command.C1DockingTabPage();
            this.pnfinishD = new System.Windows.Forms.Panel();
            this.pnFinishH = new System.Windows.Forms.Panel();
            this.tabSearch = new C1.Win.C1Command.C1DockingTabPage();
            this.pnSearchD = new System.Windows.Forms.Panel();
            this.pnSearchH = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.sB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tC1)).BeginInit();
            this.tC1.SuspendLayout();
            this.tabRequest.SuspendLayout();
            this.tabProcess.SuspendLayout();
            this.tabFinish.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // sB
            // 
            this.sB.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.sB.Location = new System.Drawing.Point(0, 678);
            this.sB.Name = "sB";
            this.sB.Size = new System.Drawing.Size(1066, 22);
            // 
            // tC1
            // 
            this.tC1.Controls.Add(this.tabRequest);
            this.tC1.Controls.Add(this.tabProcess);
            this.tC1.Controls.Add(this.tabFinish);
            this.tC1.Controls.Add(this.tabSearch);
            this.tC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tC1.Location = new System.Drawing.Point(0, 0);
            this.tC1.Name = "tC1";
            this.tC1.SelectedIndex = 2;
            this.tC1.Size = new System.Drawing.Size(1066, 678);
            this.tC1.TabIndex = 2;
            this.tC1.TabsSpacing = 5;
            // 
            // tabRequest
            // 
            this.tabRequest.Controls.Add(this.pnRequest);
            this.tabRequest.Location = new System.Drawing.Point(1, 24);
            this.tabRequest.Name = "tabRequest";
            this.tabRequest.Size = new System.Drawing.Size(1064, 653);
            this.tabRequest.TabIndex = 0;
            this.tabRequest.Text = "Request";
            // 
            // pnRequest
            // 
            this.pnRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnRequest.Location = new System.Drawing.Point(0, 0);
            this.pnRequest.Name = "pnRequest";
            this.pnRequest.Size = new System.Drawing.Size(1064, 653);
            this.pnRequest.TabIndex = 0;
            // 
            // tabProcess
            // 
            this.tabProcess.Controls.Add(this.pnProcess);
            this.tabProcess.Location = new System.Drawing.Point(1, 24);
            this.tabProcess.Name = "tabProcess";
            this.tabProcess.Size = new System.Drawing.Size(1064, 653);
            this.tabProcess.TabIndex = 1;
            this.tabProcess.Text = "Process";
            // 
            // pnProcess
            // 
            this.pnProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnProcess.Location = new System.Drawing.Point(0, 0);
            this.pnProcess.Name = "pnProcess";
            this.pnProcess.Size = new System.Drawing.Size(1064, 653);
            this.pnProcess.TabIndex = 0;
            // 
            // tabFinish
            // 
            this.tabFinish.Controls.Add(this.pnfinishD);
            this.tabFinish.Controls.Add(this.pnFinishH);
            this.tabFinish.Location = new System.Drawing.Point(1, 24);
            this.tabFinish.Name = "tabFinish";
            this.tabFinish.Size = new System.Drawing.Size(1064, 653);
            this.tabFinish.TabIndex = 2;
            this.tabFinish.Text = "Finish";
            // 
            // pnfinishD
            // 
            this.pnfinishD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnfinishD.Location = new System.Drawing.Point(0, 78);
            this.pnfinishD.Name = "pnfinishD";
            this.pnfinishD.Size = new System.Drawing.Size(1064, 575);
            this.pnfinishD.TabIndex = 1;
            // 
            // pnFinishH
            // 
            this.pnFinishH.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnFinishH.Location = new System.Drawing.Point(0, 0);
            this.pnFinishH.Name = "pnFinishH";
            this.pnFinishH.Size = new System.Drawing.Size(1064, 78);
            this.pnFinishH.TabIndex = 0;
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this.pnSearchD);
            this.tabSearch.Controls.Add(this.pnSearchH);
            this.tabSearch.Location = new System.Drawing.Point(1, 24);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Size = new System.Drawing.Size(1064, 653);
            this.tabSearch.TabIndex = 3;
            this.tabSearch.Text = "Search";
            // 
            // pnSearchD
            // 
            this.pnSearchD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnSearchD.Location = new System.Drawing.Point(0, 78);
            this.pnSearchD.Name = "pnSearchD";
            this.pnSearchD.Size = new System.Drawing.Size(1064, 575);
            this.pnSearchD.TabIndex = 3;
            // 
            // pnSearchH
            // 
            this.pnSearchH.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnSearchH.Location = new System.Drawing.Point(0, 0);
            this.pnSearchH.Name = "pnSearchH";
            this.pnSearchH.Size = new System.Drawing.Size(1064, 78);
            this.pnSearchH.TabIndex = 2;
            // 
            // FrmXrayViewDaily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 700);
            this.Controls.Add(this.tC1);
            this.Controls.Add(this.sB);
            this.Name = "FrmXrayViewDaily";
            this.Text = "FrmXrayViewDaily";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmXrayViewDaily_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tC1)).EndInit();
            this.tC1.ResumeLayout(false);
            this.tabRequest.ResumeLayout(false);
            this.tabProcess.ResumeLayout(false);
            this.tabFinish.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar sB;
        private C1.Win.C1Command.C1DockingTab tC1;
        private C1.Win.C1Command.C1DockingTabPage tabRequest;
        private System.Windows.Forms.Panel pnRequest;
        private C1.Win.C1Command.C1DockingTabPage tabProcess;
        private System.Windows.Forms.Panel pnProcess;
        private C1.Win.C1Command.C1DockingTabPage tabFinish;
        private System.Windows.Forms.Panel pnFinishH;
        private C1.Win.C1Command.C1DockingTabPage tabSearch;
        private System.Windows.Forms.Panel pnfinishD;
        private System.Windows.Forms.Panel pnSearchD;
        private System.Windows.Forms.Panel pnSearchH;
    }
}