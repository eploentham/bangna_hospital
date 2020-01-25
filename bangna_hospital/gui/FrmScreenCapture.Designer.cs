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
            this.chkView = new System.Windows.Forms.RadioButton();
            this.chkUpload = new System.Windows.Forms.RadioButton();
            this.lbName = new System.Windows.Forms.Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lbVn = new System.Windows.Forms.Label();
            this.txtVN = new C1.Win.C1Input.C1TextBox();
            this.pnPic.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).BeginInit();
            this.SuspendLayout();
            // 
            // pnPic
            // 
            this.pnPic.Controls.Add(this.pnView);
            this.pnPic.Controls.Add(this.panel2);
            this.pnPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnPic.Location = new System.Drawing.Point(0, 0);
            this.pnPic.Name = "pnPic";
            this.pnPic.Size = new System.Drawing.Size(618, 567);
            this.pnPic.TabIndex = 0;
            // 
            // pnView
            // 
            this.pnView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnView.Location = new System.Drawing.Point(0, 35);
            this.pnView.Name = "pnView";
            this.pnView.Size = new System.Drawing.Size(618, 532);
            this.pnView.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbVn);
            this.panel2.Controls.Add(this.txtVN);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.chkView);
            this.panel2.Controls.Add(this.chkUpload);
            this.panel2.Controls.Add(this.lbName);
            this.panel2.Controls.Add(this.txtHn);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(618, 35);
            this.panel2.TabIndex = 0;
            // 
            // chkView
            // 
            this.chkView.AutoSize = true;
            this.chkView.Location = new System.Drawing.Point(564, 8);
            this.chkView.Name = "chkView";
            this.chkView.Size = new System.Drawing.Size(48, 17);
            this.chkView.TabIndex = 4;
            this.chkView.TabStop = true;
            this.chkView.Text = "View";
            this.chkView.UseVisualStyleBackColor = true;
            // 
            // chkUpload
            // 
            this.chkUpload.AutoSize = true;
            this.chkUpload.Location = new System.Drawing.Point(499, 8);
            this.chkUpload.Name = "chkUpload";
            this.chkUpload.Size = new System.Drawing.Size(59, 17);
            this.chkUpload.TabIndex = 3;
            this.chkUpload.TabStop = true;
            this.chkUpload.Text = "Upload";
            this.chkUpload.UseVisualStyleBackColor = true;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(149, 9);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(21, 20);
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
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(120, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(24, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "...";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // lbVn
            // 
            this.lbVn.AutoSize = true;
            this.lbVn.BackColor = System.Drawing.Color.White;
            this.lbVn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.lbVn.Location = new System.Drawing.Point(363, 8);
            this.lbVn.Name = "lbVn";
            this.lbVn.Size = new System.Drawing.Size(36, 16);
            this.lbVn.TabIndex = 546;
            this.lbVn.Text = "VN  :";
            // 
            // txtVN
            // 
            this.txtVN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtVN.Location = new System.Drawing.Point(402, 6);
            this.txtVN.Name = "txtVN";
            this.txtVN.Size = new System.Drawing.Size(94, 20);
            this.txtVN.TabIndex = 545;
            this.txtVN.Tag = null;
            this.txtVN.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtVN.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // FrmScreenCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 567);
            this.Controls.Add(this.pnPic);
            this.Name = "FrmScreenCapture";
            this.Text = "FrmScreenCapture";
            this.Load += new System.EventHandler(this.FrmScreenCapture_Load);
            this.pnPic.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).EndInit();
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
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lbVn;
        private C1.Win.C1Input.C1TextBox txtVN;
    }
}