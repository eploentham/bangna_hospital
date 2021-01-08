namespace bangna_hospital.gui
{
    partial class FrmOPD2CheckUPMain
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
            this.btnPrintOPD2 = new System.Windows.Forms.Button();
            this.btnPrintOPD21 = new System.Windows.Forms.Button();
            this.btnPrintOPD21TrueStar = new System.Windows.Forms.Button();
            this.btnPrintLicenseDriver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPrintOPD2
            // 
            this.btnPrintOPD2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrintOPD2.Location = new System.Drawing.Point(117, 60);
            this.btnPrintOPD2.Name = "btnPrintOPD2";
            this.btnPrintOPD2.Size = new System.Drawing.Size(284, 41);
            this.btnPrintOPD2.TabIndex = 130;
            this.btnPrintOPD2.Text = "Print ใบรับรองแพทย์อาชีวะ";
            this.btnPrintOPD2.UseVisualStyleBackColor = true;
            this.btnPrintOPD2.Click += new System.EventHandler(this.btnPrintOPD2_Click);
            // 
            // btnPrintOPD21
            // 
            this.btnPrintOPD21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrintOPD21.Location = new System.Drawing.Point(117, 126);
            this.btnPrintOPD21.Name = "btnPrintOPD21";
            this.btnPrintOPD21.Size = new System.Drawing.Size(284, 41);
            this.btnPrintOPD21.TabIndex = 131;
            this.btnPrintOPD21.Text = "Print ใบรับรองแพทย์ การตรวจสุขภาพแรงงานต่างด้าว";
            this.btnPrintOPD21.UseVisualStyleBackColor = true;
            this.btnPrintOPD21.Click += new System.EventHandler(this.btnPrintOPD21_Click);
            // 
            // btnPrintOPD21TrueStar
            // 
            this.btnPrintOPD21TrueStar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrintOPD21TrueStar.Location = new System.Drawing.Point(117, 199);
            this.btnPrintOPD21TrueStar.Name = "btnPrintOPD21TrueStar";
            this.btnPrintOPD21TrueStar.Size = new System.Drawing.Size(284, 54);
            this.btnPrintOPD21TrueStar.TabIndex = 132;
            this.btnPrintOPD21TrueStar.Text = "Print ใบรับรองแพทย์ การตรวจสุขภาพแรงงานต่างด้าว\r\nTrue Star";
            this.btnPrintOPD21TrueStar.UseVisualStyleBackColor = true;
            // 
            // btnPrintLicenseDriver
            // 
            this.btnPrintLicenseDriver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPrintLicenseDriver.Location = new System.Drawing.Point(117, 275);
            this.btnPrintLicenseDriver.Name = "btnPrintLicenseDriver";
            this.btnPrintLicenseDriver.Size = new System.Drawing.Size(284, 41);
            this.btnPrintLicenseDriver.TabIndex = 133;
            this.btnPrintLicenseDriver.Text = "Print ใบรับรองแพทย์ ใบขับขี่";
            this.btnPrintLicenseDriver.UseVisualStyleBackColor = true;
            // 
            // FrmOPD2CheckUPMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 465);
            this.Controls.Add(this.btnPrintLicenseDriver);
            this.Controls.Add(this.btnPrintOPD21TrueStar);
            this.Controls.Add(this.btnPrintOPD21);
            this.Controls.Add(this.btnPrintOPD2);
            this.Name = "FrmOPD2CheckUPMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmOPD2CheckUPMain";
            this.Load += new System.EventHandler(this.FrmOPD2CheckUPMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrintOPD2;
        private System.Windows.Forms.Button btnPrintOPD21;
        private System.Windows.Forms.Button btnPrintOPD21TrueStar;
        private System.Windows.Forms.Button btnPrintLicenseDriver;
    }
}