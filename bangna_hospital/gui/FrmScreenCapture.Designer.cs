namespace bangna_hospital.gui
{
    partial class FrmScreenCapture
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
            this.pnPic = new System.Windows.Forms.Panel();
            this.pnView = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbName = new System.Windows.Forms.Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkUpload = new System.Windows.Forms.RadioButton();
            this.chkView = new System.Windows.Forms.RadioButton();
            this.pnPic.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            this.SuspendLayout();
            // 
            // pnPic
            // 
            this.pnPic.Controls.Add(this.pnView);
            this.pnPic.Controls.Add(this.panel2);
            this.pnPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnPic.Location = new System.Drawing.Point(0, 0);
            this.pnPic.Name = "pnPic";
            this.pnPic.Size = new System.Drawing.Size(482, 567);
            this.pnPic.TabIndex = 0;
            // 
            // pnView
            // 
            this.pnView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnView.Location = new System.Drawing.Point(0, 35);
            this.pnView.Name = "pnView";
            this.pnView.Size = new System.Drawing.Size(482, 532);
            this.pnView.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkView);
            this.panel2.Controls.Add(this.chkUpload);
            this.panel2.Controls.Add(this.lbName);
            this.panel2.Controls.Add(this.txtHn);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(482, 35);
            this.panel2.TabIndex = 0;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(125, 9);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(17, 16);
            this.lbName.TabIndex = 2;
            this.lbName.Text = "...";
            // 
            // txtHn
            // 
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHn.Location = new System.Drawing.Point(43, 6);
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(76, 24);
            this.txtHn.TabIndex = 1;
            this.txtHn.Tag = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "HN :";
            // 
            // chkUpload
            // 
            this.chkUpload.AutoSize = true;
            this.chkUpload.Location = new System.Drawing.Point(357, 8);
            this.chkUpload.Name = "chkUpload";
            this.chkUpload.Size = new System.Drawing.Size(59, 17);
            this.chkUpload.TabIndex = 3;
            this.chkUpload.TabStop = true;
            this.chkUpload.Text = "Upload";
            this.chkUpload.UseVisualStyleBackColor = true;
            // 
            // chkView
            // 
            this.chkView.AutoSize = true;
            this.chkView.Location = new System.Drawing.Point(422, 8);
            this.chkView.Name = "chkView";
            this.chkView.Size = new System.Drawing.Size(48, 17);
            this.chkView.TabIndex = 4;
            this.chkView.TabStop = true;
            this.chkView.Text = "View";
            this.chkView.UseVisualStyleBackColor = true;
            // 
            // FrmScreenCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 567);
            this.Controls.Add(this.pnPic);
            this.Name = "FrmScreenCapture";
            this.Text = "FrmScreenCapture";
            this.Load += new System.EventHandler(this.FrmScreenCapture_Load);
            this.pnPic.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnPic;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbName;
        private C1.Win.C1Input.C1TextBox txtHn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnView;
        private System.Windows.Forms.RadioButton chkView;
        private System.Windows.Forms.RadioButton chkUpload;
    }
}