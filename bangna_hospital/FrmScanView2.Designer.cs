namespace bangna_hospital
{
    partial class FrmScanView2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.c1SplitContainer1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.lbAge = new System.Windows.Forms.Label();
            this.txtName = new C1.Win.C1Input.C1TextBox();
            this.btnHn = new C1.Win.C1Input.C1Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVN = new C1.Win.C1Input.C1TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDgs = new C1.Win.C1Input.C1ComboBox();
            this.lbCnt = new System.Windows.Forms.Label();
            this.chkIPD = new C1.Win.C1Input.C1CheckBox();
            this.txt = new C1.Win.C1Input.C1TextBox();
            this.btnOpen = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).BeginInit();
            this.c1SplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).BeginInit();
            this.SuspendLayout();
            // 
            // sb1
            // 
            this.sb1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.sb1.Location = new System.Drawing.Point(0, 635);
            this.sb1.Name = "sb1";
            this.sb1.Size = new System.Drawing.Size(1363, 22);
            this.theme1.SetTheme(this.sb1, "(default)");
            this.sb1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Custom;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1363, 635);
            this.panel1.TabIndex = 2;
            this.theme1.SetTheme(this.panel1, "(default)");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbCnt);
            this.groupBox1.Controls.Add(this.chkIPD);
            this.groupBox1.Controls.Add(this.txt);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtVN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboDgs);
            this.groupBox1.Controls.Add(this.lbAge);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.btnHn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1363, 59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patient";
            this.theme1.SetTheme(this.groupBox1, "(default)");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.c1SplitContainer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1363, 576);
            this.panel2.TabIndex = 1;
            this.theme1.SetTheme(this.panel2, "(default)");
            // 
            // c1SplitContainer1
            // 
            this.c1SplitContainer1.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.c1SplitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1SplitContainer1.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.c1SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SplitContainer1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.c1SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.c1SplitContainer1.Name = "c1SplitContainer1";
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel1);
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel2);
            this.c1SplitContainer1.Size = new System.Drawing.Size(1363, 576);
            this.c1SplitContainer1.TabIndex = 0;
            this.theme1.SetTheme(this.c1SplitContainer1, "(default)");
            // 
            // c1SplitterPanel1
            // 
            this.c1SplitterPanel1.Collapsible = true;
            this.c1SplitterPanel1.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1SplitterPanel1.Location = new System.Drawing.Point(0, 21);
            this.c1SplitterPanel1.Name = "c1SplitterPanel1";
            this.c1SplitterPanel1.Size = new System.Drawing.Size(673, 555);
            this.c1SplitterPanel1.TabIndex = 0;
            this.c1SplitterPanel1.Text = "Panel 1";
            this.c1SplitterPanel1.Width = 680;
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.Height = 576;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(684, 21);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(679, 555);
            this.c1SplitterPanel2.TabIndex = 1;
            this.c1SplitterPanel2.Text = "Panel 2";
            // 
            // lbAge
            // 
            this.lbAge.AutoSize = true;
            this.lbAge.BackColor = System.Drawing.Color.White;
            this.lbAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.lbAge.Location = new System.Drawing.Point(531, 16);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(21, 20);
            this.lbAge.TabIndex = 561;
            this.lbAge.Text = "...";
            this.theme1.SetTheme(this.lbAge, "(default)");
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtName.Location = new System.Drawing.Point(238, 14);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(287, 27);
            this.txtName.TabIndex = 560;
            this.txtName.Tag = null;
            this.theme1.SetTheme(this.txtName, "(default)");
            this.txtName.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // btnHn
            // 
            this.btnHn.Location = new System.Drawing.Point(204, 13);
            this.btnHn.Name = "btnHn";
            this.btnHn.Size = new System.Drawing.Size(28, 23);
            this.btnHn.TabIndex = 559;
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
            this.label4.Location = new System.Drawing.Point(10, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 16);
            this.label4.TabIndex = 558;
            this.label4.Text = "HN  :";
            this.theme1.SetTheme(this.label4, "(default)");
            // 
            // txtHn
            // 
            this.txtHn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHn.Location = new System.Drawing.Point(48, 12);
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(150, 27);
            this.txtHn.TabIndex = 557;
            this.txtHn.Tag = null;
            this.theme1.SetTheme(this.txtHn, "(default)");
            this.txtHn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtHn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(849, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 565;
            this.label2.Text = "VN  :";
            this.theme1.SetTheme(this.label2, "(default)");
            // 
            // txtVN
            // 
            this.txtVN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtVN.Location = new System.Drawing.Point(887, 17);
            this.txtVN.Name = "txtVN";
            this.txtVN.Size = new System.Drawing.Size(107, 20);
            this.txtVN.TabIndex = 564;
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
            this.label1.Location = new System.Drawing.Point(561, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 563;
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
            this.cboDgs.Location = new System.Drawing.Point(662, 17);
            this.cboDgs.Name = "cboDgs";
            this.cboDgs.Size = new System.Drawing.Size(184, 20);
            this.cboDgs.TabIndex = 562;
            this.cboDgs.Tag = null;
            this.theme1.SetTheme(this.cboDgs, "(default)");
            this.cboDgs.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.cboDgs.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // lbCnt
            // 
            this.lbCnt.AutoSize = true;
            this.lbCnt.BackColor = System.Drawing.Color.White;
            this.lbCnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.lbCnt.Location = new System.Drawing.Point(1214, 19);
            this.lbCnt.Name = "lbCnt";
            this.lbCnt.Size = new System.Drawing.Size(17, 16);
            this.lbCnt.TabIndex = 569;
            this.lbCnt.Text = "...";
            this.theme1.SetTheme(this.lbCnt, "(default)");
            // 
            // chkIPD
            // 
            this.chkIPD.BackColor = System.Drawing.Color.Transparent;
            this.chkIPD.BorderColor = System.Drawing.Color.Transparent;
            this.chkIPD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkIPD.ForeColor = System.Drawing.Color.Black;
            this.chkIPD.Location = new System.Drawing.Point(1002, 14);
            this.chkIPD.Name = "chkIPD";
            this.chkIPD.Padding = new System.Windows.Forms.Padding(1);
            this.chkIPD.Size = new System.Drawing.Size(50, 24);
            this.chkIPD.TabIndex = 568;
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
            this.txt.Location = new System.Drawing.Point(1058, 15);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(67, 20);
            this.txt.TabIndex = 567;
            this.txt.Tag = null;
            this.theme1.SetTheme(this.txt, "(default)");
            this.txt.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::bangna_hospital.Properties.Resources.Open_Large;
            this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpen.Location = new System.Drawing.Point(1131, 11);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(77, 33);
            this.btnOpen.TabIndex = 566;
            this.btnOpen.Text = "Open";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnOpen, "(default)");
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // FrmScanView2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1363, 657);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sb1);
            this.Name = "FrmScanView2";
            this.Text = "FrmScanView2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmScanView2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).EndInit();
            this.c1SplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController theme1;
        private C1.Win.C1Ribbon.C1StatusBar sb1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private System.Windows.Forms.Label lbAge;
        private C1.Win.C1Input.C1TextBox txtName;
        private C1.Win.C1Input.C1Button btnHn;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Input.C1TextBox txtHn;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1TextBox txtVN;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1ComboBox cboDgs;
        private System.Windows.Forms.Label lbCnt;
        private C1.Win.C1Input.C1CheckBox chkIPD;
        private C1.Win.C1Input.C1TextBox txt;
        private C1.Win.C1Input.C1Button btnOpen;
    }
}