namespace bangna_hospital.gui
{
    partial class FrmScreenCaptureUpload
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
            this.theme1 = new C1.Win.C1Themes.C1ThemeController();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDgs = new C1.Win.C1Input.C1ComboBox();
            this.btnUpload = new C1.Win.C1Input.C1Button();
            this.sB11 = new System.Windows.Forms.StatusStrip();
            this.picWait = new C1.Win.C1Input.C1PictureBox();
            this.txtFM = new C1.Win.C1Input.C1TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVn = new C1.Win.C1Input.C1TextBox();
            this.lbVn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWait)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVn)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "HN :";
            this.theme1.SetTheme(this.label1, "(default)");
            // 
            // txtHn
            // 
            this.txtHn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.txtHn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHn.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHn.Location = new System.Drawing.Point(52, 7);
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(77, 24);
            this.txtHn.TabIndex = 3;
            this.txtHn.Tag = null;
            this.theme1.SetTheme(this.txtHn, "(default)");
            this.txtHn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.BackColor = System.Drawing.Color.White;
            this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.lbName.Location = new System.Drawing.Point(135, 12);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(17, 16);
            this.lbName.TabIndex = 4;
            this.lbName.Text = "...";
            this.theme1.SetTheme(this.lbName, "(default)");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "ประเถทเอกสาร :";
            this.theme1.SetTheme(this.label2, "(default)");
            // 
            // cboDgs
            // 
            this.cboDgs.AllowSpinLoop = false;
            this.cboDgs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.cboDgs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboDgs.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.cboDgs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboDgs.GapHeight = 0;
            this.cboDgs.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboDgs.ItemsDisplayMember = "";
            this.cboDgs.ItemsValueMember = "";
            this.cboDgs.Location = new System.Drawing.Point(96, 37);
            this.cboDgs.Name = "cboDgs";
            this.cboDgs.Size = new System.Drawing.Size(246, 24);
            this.cboDgs.Style.DropDownBackColor = System.Drawing.Color.White;
            this.cboDgs.Style.DropDownBorderColor = System.Drawing.Color.Gainsboro;
            this.cboDgs.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboDgs.TabIndex = 7;
            this.cboDgs.Tag = null;
            this.theme1.SetTheme(this.cboDgs, "(default)");
            this.cboDgs.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnUpload
            // 
            this.btnUpload.Image = global::bangna_hospital.Properties.Resources.refresh48;
            this.btnUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpload.Location = new System.Drawing.Point(242, 167);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(88, 60);
            this.btnUpload.TabIndex = 8;
            this.btnUpload.Text = "Upload";
            this.btnUpload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnUpload, "(default)");
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // sB11
            // 
            this.sB11.BackColor = System.Drawing.Color.White;
            this.sB11.Location = new System.Drawing.Point(0, 233);
            this.sB11.Name = "sB11";
            this.sB11.Size = new System.Drawing.Size(342, 22);
            this.sB11.TabIndex = 9;
            this.sB11.Text = "statusStrip1";
            this.theme1.SetTheme(this.sB11, "(default)");
            // 
            // picWait
            // 
            this.picWait.Image = global::bangna_hospital.Properties.Resources.loading_transparent;
            this.picWait.Location = new System.Drawing.Point(97, 102);
            this.picWait.Name = "picWait";
            this.picWait.Size = new System.Drawing.Size(135, 125);
            this.picWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWait.TabIndex = 5;
            this.picWait.TabStop = false;
            // 
            // txtFM
            // 
            this.txtFM.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.txtFM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFM.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtFM.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFM.Location = new System.Drawing.Point(96, 67);
            this.txtFM.Name = "txtFM";
            this.txtFM.Size = new System.Drawing.Size(111, 24);
            this.txtFM.TabIndex = 11;
            this.txtFM.Tag = null;
            this.theme1.SetTheme(this.txtFM, "(default)");
            this.txtFM.Value = "FM-IMG-999";
            this.txtFM.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label3.Location = new System.Drawing.Point(6, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "FM code :";
            this.theme1.SetTheme(this.label3, "(default)");
            // 
            // txtVn
            // 
            this.txtVn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.txtVn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVn.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtVn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVn.Location = new System.Drawing.Point(251, 67);
            this.txtVn.Name = "txtVn";
            this.txtVn.Size = new System.Drawing.Size(79, 24);
            this.txtVn.TabIndex = 13;
            this.txtVn.Tag = null;
            this.theme1.SetTheme(this.txtVn, "(default)");
            this.txtVn.Value = "FM-IMG-999";
            this.txtVn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lbVn
            // 
            this.lbVn.AutoSize = true;
            this.lbVn.BackColor = System.Drawing.Color.White;
            this.lbVn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.lbVn.Location = new System.Drawing.Point(211, 72);
            this.lbVn.Name = "lbVn";
            this.lbVn.Size = new System.Drawing.Size(28, 16);
            this.lbVn.TabIndex = 12;
            this.lbVn.Text = "vn :";
            this.theme1.SetTheme(this.lbVn, "(default)");
            // 
            // FrmScreenCaptureUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 255);
            this.Controls.Add(this.txtVn);
            this.Controls.Add(this.lbVn);
            this.Controls.Add(this.txtFM);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sB11);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.cboDgs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picWait);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.txtHn);
            this.Controls.Add(this.label1);
            this.Name = "FrmScreenCaptureUpload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmScreenCaptureUpload";
            this.Load += new System.EventHandler(this.FrmScreenCaptureUpload_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWait)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private C1.Win.C1Themes.C1ThemeController theme1;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1TextBox txtHn;
        private System.Windows.Forms.Label lbName;
        private C1.Win.C1Input.C1PictureBox picWait;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1ComboBox cboDgs;
        private C1.Win.C1Input.C1Button btnUpload;
        private System.Windows.Forms.StatusStrip sB11;
        private C1.Win.C1Input.C1TextBox txtFM;
        private System.Windows.Forms.Label label3;
        private C1.Win.C1Input.C1TextBox txtVn;
        private System.Windows.Forms.Label lbVn;
    }
}