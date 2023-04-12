
namespace bangna_hospital.gui
{
    partial class FrmExcel
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
            this.c1DockingTab1 = new C1.Win.C1Command.C1DockingTab();
            this.c1DockingTabPage1 = new C1.Win.C1Command.C1DockingTabPage();
            this.btnOpenExcel = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnReadExcel = new System.Windows.Forms.Button();
            this.tabDrugCat = new C1.Win.C1Command.C1DockingTabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDrugCOpenExcel = new System.Windows.Forms.Button();
            this.txtDrugCPathExcel = new System.Windows.Forms.TextBox();
            this.btnDrugCRead = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).BeginInit();
            this.c1DockingTab1.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            this.tabDrugCat.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1DockingTab1
            // 
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage1);
            this.c1DockingTab1.Controls.Add(this.tabDrugCat);
            this.c1DockingTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1DockingTab1.Location = new System.Drawing.Point(0, 0);
            this.c1DockingTab1.Name = "c1DockingTab1";
            this.c1DockingTab1.SelectedIndex = 1;
            this.c1DockingTab1.Size = new System.Drawing.Size(972, 617);
            this.c1DockingTab1.TabIndex = 0;
            this.c1DockingTab1.TabsSpacing = 5;
            // 
            // c1DockingTabPage1
            // 
            this.c1DockingTabPage1.Controls.Add(this.btnOpenExcel);
            this.c1DockingTabPage1.Controls.Add(this.txtPath);
            this.c1DockingTabPage1.Controls.Add(this.btnReadExcel);
            this.c1DockingTabPage1.Location = new System.Drawing.Point(1, 24);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(970, 592);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Page1";
            // 
            // btnOpenExcel
            // 
            this.btnOpenExcel.Location = new System.Drawing.Point(133, 13);
            this.btnOpenExcel.Name = "btnOpenExcel";
            this.btnOpenExcel.Size = new System.Drawing.Size(42, 23);
            this.btnOpenExcel.TabIndex = 2;
            this.btnOpenExcel.Text = "...";
            this.btnOpenExcel.UseVisualStyleBackColor = true;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(181, 16);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(567, 20);
            this.txtPath.TabIndex = 1;
            // 
            // btnReadExcel
            // 
            this.btnReadExcel.Location = new System.Drawing.Point(754, 13);
            this.btnReadExcel.Name = "btnReadExcel";
            this.btnReadExcel.Size = new System.Drawing.Size(75, 23);
            this.btnReadExcel.TabIndex = 0;
            this.btnReadExcel.Text = "Read";
            this.btnReadExcel.UseVisualStyleBackColor = true;
            // 
            // tabDrugCat
            // 
            this.tabDrugCat.Controls.Add(this.label2);
            this.tabDrugCat.Controls.Add(this.label1);
            this.tabDrugCat.Controls.Add(this.btnDrugCOpenExcel);
            this.tabDrugCat.Controls.Add(this.txtDrugCPathExcel);
            this.tabDrugCat.Controls.Add(this.btnDrugCRead);
            this.tabDrugCat.Location = new System.Drawing.Point(1, 24);
            this.tabDrugCat.Name = "tabDrugCat";
            this.tabDrugCat.Size = new System.Drawing.Size(970, 592);
            this.tabDrugCat.TabIndex = 1;
            this.tabDrugCat.Text = "Drug Cat";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "0";
            // 
            // btnDrugCOpenExcel
            // 
            this.btnDrugCOpenExcel.Location = new System.Drawing.Point(12, 13);
            this.btnDrugCOpenExcel.Name = "btnDrugCOpenExcel";
            this.btnDrugCOpenExcel.Size = new System.Drawing.Size(42, 23);
            this.btnDrugCOpenExcel.TabIndex = 5;
            this.btnDrugCOpenExcel.Text = "...";
            this.btnDrugCOpenExcel.UseVisualStyleBackColor = true;
            // 
            // txtDrugCPathExcel
            // 
            this.txtDrugCPathExcel.Location = new System.Drawing.Point(60, 16);
            this.txtDrugCPathExcel.Name = "txtDrugCPathExcel";
            this.txtDrugCPathExcel.Size = new System.Drawing.Size(567, 20);
            this.txtDrugCPathExcel.TabIndex = 4;
            // 
            // btnDrugCRead
            // 
            this.btnDrugCRead.Location = new System.Drawing.Point(633, 13);
            this.btnDrugCRead.Name = "btnDrugCRead";
            this.btnDrugCRead.Size = new System.Drawing.Size(75, 23);
            this.btnDrugCRead.TabIndex = 3;
            this.btnDrugCRead.Text = "Read";
            this.btnDrugCRead.UseVisualStyleBackColor = true;
            // 
            // FrmExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 617);
            this.Controls.Add(this.c1DockingTab1);
            this.Name = "FrmExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmExcel";
            this.Load += new System.EventHandler(this.FrmExcel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).EndInit();
            this.c1DockingTab1.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            this.c1DockingTabPage1.PerformLayout();
            this.tabDrugCat.ResumeLayout(false);
            this.tabDrugCat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Command.C1DockingTab c1DockingTab1;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage1;
        private System.Windows.Forms.Button btnOpenExcel;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnReadExcel;
        private C1.Win.C1Command.C1DockingTabPage tabDrugCat;
        private System.Windows.Forms.Button btnDrugCOpenExcel;
        private System.Windows.Forms.TextBox txtDrugCPathExcel;
        private System.Windows.Forms.Button btnDrugCRead;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}