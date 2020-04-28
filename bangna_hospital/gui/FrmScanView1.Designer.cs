namespace bangna_hospital.gui
{
    partial class FrmScanView1
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
            this.gbPtt = new System.Windows.Forms.GroupBox();
            this.lbDrugAllergy = new System.Windows.Forms.Label();
            this.lbAge = new System.Windows.Forms.Label();
            this.lbCnt = new System.Windows.Forms.Label();
            this.chkIPD = new C1.Win.C1Input.C1CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVN = new C1.Win.C1Input.C1TextBox();
            this.txtName = new C1.Win.C1Input.C1TextBox();
            this.btnHn = new C1.Win.C1Input.C1Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sC1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.scVs = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.scScan = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            this.gbPtt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sC1)).BeginInit();
            this.sC1.SuspendLayout();
            this.scVs.SuspendLayout();
            this.scScan.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPtt
            // 
            this.gbPtt.BackColor = System.Drawing.Color.White;
            this.gbPtt.Controls.Add(this.lbDrugAllergy);
            this.gbPtt.Controls.Add(this.lbAge);
            this.gbPtt.Controls.Add(this.lbCnt);
            this.gbPtt.Controls.Add(this.chkIPD);
            this.gbPtt.Controls.Add(this.label2);
            this.gbPtt.Controls.Add(this.txtVN);
            this.gbPtt.Controls.Add(this.txtName);
            this.gbPtt.Controls.Add(this.btnHn);
            this.gbPtt.Controls.Add(this.label4);
            this.gbPtt.Controls.Add(this.txtHn);
            this.gbPtt.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPtt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.gbPtt.Location = new System.Drawing.Point(0, 0);
            this.gbPtt.Name = "gbPtt";
            this.gbPtt.Size = new System.Drawing.Size(1209, 45);
            this.gbPtt.TabIndex = 2;
            this.gbPtt.TabStop = false;
            this.gbPtt.Text = "Patient";
            this.theme1.SetTheme(this.gbPtt, "(default)");
            // 
            // lbDrugAllergy
            // 
            this.lbDrugAllergy.BackColor = System.Drawing.Color.White;
            this.lbDrugAllergy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDrugAllergy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.lbDrugAllergy.Location = new System.Drawing.Point(1109, 13);
            this.lbDrugAllergy.Name = "lbDrugAllergy";
            this.lbDrugAllergy.Size = new System.Drawing.Size(380, 20);
            this.lbDrugAllergy.TabIndex = 557;
            this.lbDrugAllergy.Text = "...";
            this.theme1.SetTheme(this.lbDrugAllergy, "(default)");
            // 
            // lbAge
            // 
            this.lbAge.AutoSize = true;
            this.lbAge.BackColor = System.Drawing.Color.White;
            this.lbAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.lbAge.Location = new System.Drawing.Point(500, 10);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(21, 20);
            this.lbAge.TabIndex = 556;
            this.lbAge.Text = "...";
            this.theme1.SetTheme(this.lbAge, "(default)");
            // 
            // lbCnt
            // 
            this.lbCnt.AutoSize = true;
            this.lbCnt.BackColor = System.Drawing.Color.White;
            this.lbCnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.lbCnt.Location = new System.Drawing.Point(1086, 16);
            this.lbCnt.Name = "lbCnt";
            this.lbCnt.Size = new System.Drawing.Size(17, 16);
            this.lbCnt.TabIndex = 555;
            this.lbCnt.Text = "...";
            this.theme1.SetTheme(this.lbCnt, "(default)");
            // 
            // chkIPD
            // 
            this.chkIPD.BackColor = System.Drawing.Color.Transparent;
            this.chkIPD.BorderColor = System.Drawing.Color.Transparent;
            this.chkIPD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkIPD.ForeColor = System.Drawing.Color.Black;
            this.chkIPD.Location = new System.Drawing.Point(957, 11);
            this.chkIPD.Name = "chkIPD";
            this.chkIPD.Padding = new System.Windows.Forms.Padding(1);
            this.chkIPD.Size = new System.Drawing.Size(50, 24);
            this.chkIPD.TabIndex = 554;
            this.chkIPD.Text = "IPD";
            this.theme1.SetTheme(this.chkIPD, "(default)");
            this.chkIPD.UseVisualStyleBackColor = true;
            this.chkIPD.Value = null;
            this.chkIPD.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkIPD.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(836, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 544;
            this.label2.Text = "VN  :";
            this.theme1.SetTheme(this.label2, "(default)");
            // 
            // txtVN
            // 
            this.txtVN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtVN.Location = new System.Drawing.Point(874, 13);
            this.txtVN.Name = "txtVN";
            this.txtVN.Size = new System.Drawing.Size(71, 20);
            this.txtVN.TabIndex = 543;
            this.txtVN.Tag = null;
            this.theme1.SetTheme(this.txtVN, "(default)");
            this.txtVN.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtVN.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // txtName
            // 
            this.txtName.AutoSize = false;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtName.Location = new System.Drawing.Point(146, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(287, 27);
            this.txtName.TabIndex = 541;
            this.txtName.Tag = null;
            this.theme1.SetTheme(this.txtName, "(default)");
            this.txtName.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // btnHn
            // 
            this.btnHn.Location = new System.Drawing.Point(174, 12);
            this.btnHn.Name = "btnHn";
            this.btnHn.Size = new System.Drawing.Size(28, 23);
            this.btnHn.TabIndex = 540;
            this.btnHn.Text = "...";
            this.theme1.SetTheme(this.btnHn, "(default)");
            this.btnHn.UseVisualStyleBackColor = true;
            this.btnHn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label4.Location = new System.Drawing.Point(10, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 16);
            this.label4.TabIndex = 539;
            this.label4.Text = "HN  :";
            this.theme1.SetTheme(this.label4, "(default)");
            // 
            // txtHn
            // 
            this.txtHn.AutoSize = false;
            this.txtHn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHn.Location = new System.Drawing.Point(48, 11);
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(92, 27);
            this.txtHn.TabIndex = 538;
            this.txtHn.Tag = null;
            this.theme1.SetTheme(this.txtHn, "(default)");
            this.txtHn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtHn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.sC1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel1.Location = new System.Drawing.Point(0, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1209, 624);
            this.panel1.TabIndex = 5;
            this.theme1.SetTheme(this.panel1, "(default)");
            // 
            // sC1
            // 
            this.sC1.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.sC1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.sC1.CollapsingAreaColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.sC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sC1.FixedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(166)))), ((int)(((byte)(194)))));
            this.sC1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.sC1.Location = new System.Drawing.Point(0, 0);
            this.sC1.Name = "sC1";
            this.sC1.Panels.Add(this.scVs);
            this.sC1.Panels.Add(this.scScan);
            this.sC1.Size = new System.Drawing.Size(1209, 624);
            this.sC1.SplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(166)))), ((int)(((byte)(194)))));
            this.sC1.TabIndex = 2;
            this.theme1.SetTheme(this.sC1, "(default)");
            this.sC1.ToolTipGradient = C1.Win.C1SplitContainer.ToolTipGradient.Blue;
            this.sC1.UseParentVisualStyle = false;
            // 
            // scVs
            // 
            this.scVs.Collapsible = true;
            this.scVs.Controls.Add(this.panel2);
            this.scVs.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.scVs.Location = new System.Drawing.Point(0, 21);
            this.scVs.Name = "scVs";
            this.scVs.Size = new System.Drawing.Size(226, 603);
            this.scVs.SizeRatio = 19.336D;
            this.scVs.TabIndex = 0;
            this.scVs.Text = "Panel 1";
            this.scVs.Width = 233;
            // 
            // scScan
            // 
            this.scScan.Controls.Add(this.panel3);
            this.scScan.Height = 624;
            this.scScan.Location = new System.Drawing.Point(237, 21);
            this.scScan.Name = "scScan";
            this.scScan.Size = new System.Drawing.Size(972, 603);
            this.scScan.TabIndex = 1;
            this.scScan.Text = "Panel 2";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(972, 603);
            this.panel3.TabIndex = 1;
            this.theme1.SetTheme(this.panel3, "(default)");
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(226, 603);
            this.panel2.TabIndex = 0;
            this.theme1.SetTheme(this.panel2, "(default)");
            // 
            // FrmScanView1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 669);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbPtt);
            this.Name = "FrmScanView1";
            this.Text = "FrmScanView1";
            this.Load += new System.EventHandler(this.FrmScanView1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            this.gbPtt.ResumeLayout(false);
            this.gbPtt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sC1)).EndInit();
            this.sC1.ResumeLayout(false);
            this.scVs.ResumeLayout(false);
            this.scScan.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController theme1;
        private System.Windows.Forms.GroupBox gbPtt;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1TextBox txtVN;
        private C1.Win.C1Input.C1TextBox txtName;
        private C1.Win.C1Input.C1Button btnHn;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Input.C1TextBox txtHn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private C1.Win.C1Input.C1CheckBox chkIPD;
        private C1.Win.C1SplitContainer.C1SplitContainer sC1;
        private C1.Win.C1SplitContainer.C1SplitterPanel scVs;
        private C1.Win.C1SplitContainer.C1SplitterPanel scScan;
        private System.Windows.Forms.Label lbCnt;
        private System.Windows.Forms.Label lbAge;
        private System.Windows.Forms.Label lbDrugAllergy;
        private System.Windows.Forms.Panel panel2;
    }
}