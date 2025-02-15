namespace bangna_hospital.gui
{
    partial class FrmOPBkkClaimPDF
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnUcepSelect = new System.Windows.Forms.Panel();
            this.btnPDF = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPDF)).BeginInit();
            this.SuspendLayout();
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 795);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.Size = new System.Drawing.Size(1031, 22);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPDF);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1031, 55);
            this.panel1.TabIndex = 1;
            // 
            // pnUcepSelect
            // 
            this.pnUcepSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnUcepSelect.Location = new System.Drawing.Point(0, 55);
            this.pnUcepSelect.Name = "pnUcepSelect";
            this.pnUcepSelect.Size = new System.Drawing.Size(1031, 740);
            this.pnUcepSelect.TabIndex = 0;
            // 
            // btnPDF
            // 
            this.btnPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPDF.Location = new System.Drawing.Point(649, 7);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(203, 40);
            this.btnPDF.TabIndex = 113;
            this.btnPDF.Text = "แก้ไขเรียบร้อย genPDF";
            this.btnPDF.UseVisualStyleBackColor = true;
            // 
            // FrmOPBkkClaimPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 817);
            this.Controls.Add(this.pnUcepSelect);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.c1StatusBar1);
            this.Name = "FrmOPBkkClaimPDF";
            this.Text = "FrmOPBkkClaimPDF";
            this.Load += new System.EventHandler(this.FrmOPBkkClaimPDF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnPDF)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnUcepSelect;
        private C1.Win.C1Input.C1Button btnPDF;
    }
}