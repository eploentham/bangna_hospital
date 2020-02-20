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
            this.sb1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.gbPtt = new System.Windows.Forms.GroupBox();
            this.lbDrugAllergy = new System.Windows.Forms.Label();
            this.lbAge = new System.Windows.Forms.Label();
            this.lbCnt = new System.Windows.Forms.Label();
            this.chkIPD = new C1.Win.C1Input.C1CheckBox();
            this.txt = new C1.Win.C1Input.C1TextBox();
            this.btnOpen = new C1.Win.C1Input.C1Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVN = new C1.Win.C1Input.C1TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDgs = new C1.Win.C1Input.C1ComboBox();
            this.txtName = new C1.Win.C1Input.C1TextBox();
            this.btnHn = new C1.Win.C1Input.C1Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sC1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.scVs = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.scScan = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).BeginInit();
            this.gbPtt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDgs)).BeginInit();
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
            // sb1
            // 
            this.sb1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.sb1.Location = new System.Drawing.Point(0, 647);
            this.sb1.Name = "sb1";
            this.sb1.Size = new System.Drawing.Size(1410, 22);
            this.theme1.SetTheme(this.sb1, "(default)");
            this.sb1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Custom;
            // 
            // gbPtt
            // 
            this.gbPtt.BackColor = System.Drawing.Color.White;
            this.gbPtt.Controls.Add(this.lbDrugAllergy);
            this.gbPtt.Controls.Add(this.lbAge);
            this.gbPtt.Controls.Add(this.lbCnt);
            this.gbPtt.Controls.Add(this.chkIPD);
            this.gbPtt.Controls.Add(this.txt);
            this.gbPtt.Controls.Add(this.btnOpen);
            this.gbPtt.Controls.Add(this.label2);
            this.gbPtt.Controls.Add(this.txtVN);
            this.gbPtt.Controls.Add(this.label1);
            this.gbPtt.Controls.Add(this.cboDgs);
            this.gbPtt.Controls.Add(this.txtName);
            this.gbPtt.Controls.Add(this.btnHn);
            this.gbPtt.Controls.Add(this.label4);
            this.gbPtt.Controls.Add(this.txtHn);
            this.gbPtt.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPtt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.gbPtt.Location = new System.Drawing.Point(0, 0);
            this.gbPtt.Name = "gbPtt";
            this.gbPtt.Size = new System.Drawing.Size(1410, 69);
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
            this.lbDrugAllergy.Location = new System.Drawing.Point(531, 39);
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
            this.lbAge.Location = new System.Drawing.Point(531, 10);
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
            this.lbCnt.Location = new System.Drawing.Point(1277, 17);
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
            this.chkIPD.Location = new System.Drawing.Point(1065, 12);
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
            // txt
            // 
            this.txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txt.Location = new System.Drawing.Point(1121, 13);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(67, 20);
            this.txt.TabIndex = 551;
            this.txt.Tag = null;
            this.theme1.SetTheme(this.txt, "(default)");
            this.txt.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::bangna_hospital.Properties.Resources.Open_Large;
            this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpen.Location = new System.Drawing.Point(1194, 9);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(77, 33);
            this.btnOpen.TabIndex = 549;
            this.btnOpen.Text = "Open";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnOpen, "(default)");
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(914, 15);
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
            this.txtVN.Location = new System.Drawing.Point(952, 13);
            this.txtVN.Name = "txtVN";
            this.txtVN.Size = new System.Drawing.Size(107, 20);
            this.txtVN.TabIndex = 543;
            this.txtVN.Tag = null;
            this.theme1.SetTheme(this.txtVN, "(default)");
            this.txtVN.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtVN.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label1.Location = new System.Drawing.Point(626, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 542;
            this.label1.Text = "ประเภทเอกสาร  :";
            this.theme1.SetTheme(this.label1, "(default)");
            // 
            // cboDgs
            // 
            this.cboDgs.AllowSpinLoop = false;
            this.cboDgs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboDgs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboDgs.GapHeight = 0;
            this.cboDgs.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboDgs.ItemsDisplayMember = "";
            this.cboDgs.ItemsValueMember = "";
            this.cboDgs.Location = new System.Drawing.Point(727, 13);
            this.cboDgs.Name = "cboDgs";
            this.cboDgs.Size = new System.Drawing.Size(184, 20);
            this.cboDgs.TabIndex = 3;
            this.cboDgs.Tag = null;
            this.theme1.SetTheme(this.cboDgs, "(default)");
            this.cboDgs.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.cboDgs.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // txtName
            // 
            this.txtName.AutoSize = false;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtName.Location = new System.Drawing.Point(238, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(287, 49);
            this.txtName.TabIndex = 541;
            this.txtName.Tag = null;
            this.theme1.SetTheme(this.txtName, "(default)");
            this.txtName.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // btnHn
            // 
            this.btnHn.Location = new System.Drawing.Point(204, 12);
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
            this.txtHn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHn.Location = new System.Drawing.Point(48, 11);
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(150, 27);
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
            this.panel1.Location = new System.Drawing.Point(0, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1410, 578);
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
            this.sC1.Size = new System.Drawing.Size(1410, 578);
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
            this.scVs.Size = new System.Drawing.Size(265, 557);
            this.scVs.SizeRatio = 19.346D;
            this.scVs.TabIndex = 0;
            this.scVs.Text = "Panel 1";
            this.scVs.Width = 272;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(265, 557);
            this.panel2.TabIndex = 0;
            this.theme1.SetTheme(this.panel2, "(default)");
            // 
            // scScan
            // 
            this.scScan.Controls.Add(this.panel3);
            this.scScan.Height = 578;
            this.scScan.Location = new System.Drawing.Point(276, 21);
            this.scScan.Name = "scScan";
            this.scScan.Size = new System.Drawing.Size(1134, 557);
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
            this.panel3.Size = new System.Drawing.Size(1134, 557);
            this.panel3.TabIndex = 1;
            this.theme1.SetTheme(this.panel3, "(default)");
            // 
            // FrmScanView1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 669);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbPtt);
            this.Controls.Add(this.sb1);
            this.Name = "FrmScanView1";
            this.Text = "FrmScanView1";
            this.Load += new System.EventHandler(this.FrmScanView1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).EndInit();
            this.gbPtt.ResumeLayout(false);
            this.gbPtt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sC1)).EndInit();
            this.sC1.ResumeLayout(false);
            this.scVs.ResumeLayout(false);
            this.scScan.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController theme1;
        private C1.Win.C1Ribbon.C1StatusBar sb1;
        private System.Windows.Forms.GroupBox gbPtt;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1TextBox txtVN;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1ComboBox cboDgs;
        private C1.Win.C1Input.C1TextBox txtName;
        private C1.Win.C1Input.C1Button btnHn;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Input.C1TextBox txtHn;
        private C1.Win.C1Input.C1Button btnOpen;
        private C1.Win.C1Input.C1TextBox txt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private C1.Win.C1Input.C1CheckBox chkIPD;
        private C1.Win.C1SplitContainer.C1SplitContainer sC1;
        private C1.Win.C1SplitContainer.C1SplitterPanel scVs;
        private C1.Win.C1SplitContainer.C1SplitterPanel scScan;
        private System.Windows.Forms.Label lbCnt;
        private System.Windows.Forms.Label lbAge;
        private System.Windows.Forms.Label lbDrugAllergy;
    }
}