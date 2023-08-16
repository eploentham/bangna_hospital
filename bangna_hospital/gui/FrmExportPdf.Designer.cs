
namespace bangna_hospital.gui
{
    partial class FrmExportPdf
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPdfMergePdf = new System.Windows.Forms.Button();
            this.txtPdfMergePath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPdfBrow = new System.Windows.Forms.Button();
            this.txtPdfPath = new System.Windows.Forms.TextBox();
            this.btnPdfRead = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).BeginInit();
            this.c1DockingTab1.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1DockingTab1
            // 
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage1);
            this.c1DockingTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1DockingTab1.Location = new System.Drawing.Point(0, 0);
            this.c1DockingTab1.Name = "c1DockingTab1";
            this.c1DockingTab1.Size = new System.Drawing.Size(800, 450);
            this.c1DockingTab1.TabIndex = 0;
            this.c1DockingTab1.TabsSpacing = 5;
            // 
            // c1DockingTabPage1
            // 
            this.c1DockingTabPage1.Controls.Add(this.panel1);
            this.c1DockingTabPage1.Location = new System.Drawing.Point(1, 24);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(798, 425);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Page1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnPdfMergePdf);
            this.panel1.Controls.Add(this.txtPdfMergePath);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnPdfBrow);
            this.panel1.Controls.Add(this.txtPdfPath);
            this.panel1.Controls.Add(this.btnPdfRead);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 34);
            this.panel1.TabIndex = 1;
            // 
            // btnPdfMergePdf
            // 
            this.btnPdfMergePdf.Location = new System.Drawing.Point(1186, 4);
            this.btnPdfMergePdf.Name = "btnPdfMergePdf";
            this.btnPdfMergePdf.Size = new System.Drawing.Size(98, 23);
            this.btnPdfMergePdf.TabIndex = 13;
            this.btnPdfMergePdf.Text = "Merge pdf";
            this.btnPdfMergePdf.UseVisualStyleBackColor = true;
            // 
            // txtPdfMergePath
            // 
            this.txtPdfMergePath.Location = new System.Drawing.Point(898, 4);
            this.txtPdfMergePath.Name = "txtPdfMergePath";
            this.txtPdfMergePath.Size = new System.Drawing.Size(282, 26);
            this.txtPdfMergePath.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(850, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "pdf ...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnPdfBrow
            // 
            this.btnPdfBrow.Location = new System.Drawing.Point(103, 3);
            this.btnPdfBrow.Name = "btnPdfBrow";
            this.btnPdfBrow.Size = new System.Drawing.Size(42, 28);
            this.btnPdfBrow.TabIndex = 8;
            this.btnPdfBrow.Text = "...";
            this.btnPdfBrow.UseVisualStyleBackColor = true;
            // 
            // txtPdfPath
            // 
            this.txtPdfPath.Location = new System.Drawing.Point(11, 5);
            this.txtPdfPath.Name = "txtPdfPath";
            this.txtPdfPath.Size = new System.Drawing.Size(86, 26);
            this.txtPdfPath.TabIndex = 7;
            // 
            // btnPdfRead
            // 
            this.btnPdfRead.Location = new System.Drawing.Point(626, 3);
            this.btnPdfRead.Name = "btnPdfRead";
            this.btnPdfRead.Size = new System.Drawing.Size(75, 28);
            this.btnPdfRead.TabIndex = 6;
            this.btnPdfRead.Text = "Read";
            this.btnPdfRead.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "label1";
            // 
            // FrmExportPdf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.c1DockingTab1);
            this.Name = "FrmExportPdf";
            this.Text = "FrmExportPdf";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).EndInit();
            this.c1DockingTab1.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Command.C1DockingTab c1DockingTab1;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPdfMergePdf;
        private System.Windows.Forms.TextBox txtPdfMergePath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnPdfBrow;
        private System.Windows.Forms.TextBox txtPdfPath;
        private System.Windows.Forms.Button btnPdfRead;
        private System.Windows.Forms.Label label1;
    }
}