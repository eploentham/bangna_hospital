namespace bangna_hospital.gui
{
    partial class FrmScanView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIPD = new C1.Win.C1Input.C1CheckBox();
            this.txt = new C1.Win.C1Input.C1TextBox();
            this.btnRefresh = new C1.Win.C1Input.C1Button();
            this.btnOpen = new C1.Win.C1Input.C1Button();
            this.txtVisitDate = new C1.Win.C1Input.C1DateEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.btnVn = new C1.Win.C1Input.C1Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVN = new C1.Win.C1Input.C1TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDgs = new C1.Win.C1Input.C1ComboBox();
            this.txtName = new C1.Win.C1Input.C1TextBox();
            this.btnHn = new C1.Win.C1Input.C1Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVisitDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sb1
            // 
            this.sb1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.sb1.Location = new System.Drawing.Point(0, 647);
            this.sb1.Name = "sb1";
            this.sb1.Size = new System.Drawing.Size(1100, 22);
            this.theme1.SetTheme(this.sb1, "(default)");
            this.sb1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Custom;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.chkIPD);
            this.groupBox1.Controls.Add(this.txt);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.txtVisitDate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnVn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtVN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboDgs);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.btnHn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1100, 66);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patient";
            this.theme1.SetTheme(this.groupBox1, "(default)");
            // 
            // chkIPD
            // 
            this.chkIPD.BackColor = System.Drawing.Color.Transparent;
            this.chkIPD.BorderColor = System.Drawing.Color.Transparent;
            this.chkIPD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkIPD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkIPD.ForeColor = System.Drawing.Color.Black;
            this.chkIPD.Location = new System.Drawing.Point(505, 36);
            this.chkIPD.Name = "chkIPD";
            this.chkIPD.Padding = new System.Windows.Forms.Padding(1);
            this.chkIPD.Size = new System.Drawing.Size(61, 24);
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
            this.txt.Location = new System.Drawing.Point(572, 40);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(338, 20);
            this.txt.TabIndex = 551;
            this.txt.Tag = null;
            this.theme1.SetTheme(this.txt, "(default)");
            this.txt.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::bangna_hospital.Properties.Resources.refresh16;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(437, 36);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(62, 24);
            this.btnRefresh.TabIndex = 550;
            this.btnRefresh.Text = "refresh";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnRefresh, "(default)");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::bangna_hospital.Properties.Resources.Open_Large;
            this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpen.Location = new System.Drawing.Point(995, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(77, 43);
            this.btnOpen.TabIndex = 549;
            this.btnOpen.Text = "Open";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnOpen, "(default)");
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // txtVisitDate
            // 
            this.txtVisitDate.AllowSpinLoop = false;
            this.txtVisitDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.txtVisitDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtVisitDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtVisitDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtVisitDate.Culture = 1054;
            this.txtVisitDate.CurrentTimeZone = false;
            this.txtVisitDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtVisitDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)((((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat | C1.Win.C1Input.FormatInfoInheritFlags.NullText) 
            | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtVisitDate.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtVisitDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtVisitDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtVisitDate.GMTOffset = System.TimeSpan.Parse("00:00:00");
            this.txtVisitDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtVisitDate.Location = new System.Drawing.Point(321, 39);
            this.txtVisitDate.Name = "txtVisitDate";
            this.txtVisitDate.Size = new System.Drawing.Size(111, 18);
            this.txtVisitDate.TabIndex = 548;
            this.txtVisitDate.Tag = null;
            this.theme1.SetTheme(this.txtVisitDate, "(default)");
            this.txtVisitDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtVisitDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label6.Location = new System.Drawing.Point(242, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 547;
            this.label6.Text = "Visit Date :";
            this.theme1.SetTheme(this.label6, "(default)");
            // 
            // btnVn
            // 
            this.btnVn.Location = new System.Drawing.Point(204, 36);
            this.btnVn.Name = "btnVn";
            this.btnVn.Size = new System.Drawing.Size(28, 23);
            this.btnVn.TabIndex = 545;
            this.btnVn.Text = "...";
            this.theme1.SetTheme(this.btnVn, "(default)");
            this.btnVn.UseVisualStyleBackColor = true;
            this.btnVn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(10, 39);
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
            this.txtVN.Location = new System.Drawing.Point(48, 37);
            this.txtVN.Name = "txtVN";
            this.txtVN.Size = new System.Drawing.Size(150, 20);
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
            this.label1.Location = new System.Drawing.Point(534, 17);
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
            this.cboDgs.Location = new System.Drawing.Point(635, 13);
            this.cboDgs.Name = "cboDgs";
            this.cboDgs.Size = new System.Drawing.Size(275, 20);
            this.cboDgs.TabIndex = 3;
            this.cboDgs.Tag = null;
            this.theme1.SetTheme(this.cboDgs, "(default)");
            this.cboDgs.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.cboDgs.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtName.Location = new System.Drawing.Point(238, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(287, 20);
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
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHn.Location = new System.Drawing.Point(48, 13);
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(150, 20);
            this.txtHn.TabIndex = 538;
            this.txtHn.Tag = null;
            this.theme1.SetTheme(this.txtHn, "(default)");
            this.txtHn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtHn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel1.Location = new System.Drawing.Point(0, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 581);
            this.panel1.TabIndex = 5;
            this.theme1.SetTheme(this.panel1, "(default)");
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel3.Location = new System.Drawing.Point(314, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(786, 581);
            this.panel3.TabIndex = 1;
            this.theme1.SetTheme(this.panel3, "(default)");
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(314, 581);
            this.panel2.TabIndex = 0;
            this.theme1.SetTheme(this.panel2, "(default)");
            // 
            // FrmScanView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 669);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.sb1);
            this.Name = "FrmScanView";
            this.Text = "FrmScanView";
            this.Load += new System.EventHandler(this.FrmScanView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVisitDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController theme1;
        private C1.Win.C1Ribbon.C1StatusBar sb1;
        private System.Windows.Forms.GroupBox groupBox1;
        private C1.Win.C1Input.C1DateEdit txtVisitDate;
        private System.Windows.Forms.Label label6;
        private C1.Win.C1Input.C1Button btnVn;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1TextBox txtVN;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1ComboBox cboDgs;
        private C1.Win.C1Input.C1TextBox txtName;
        private C1.Win.C1Input.C1Button btnHn;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Input.C1TextBox txtHn;
        private C1.Win.C1Input.C1Button btnOpen;
        private C1.Win.C1Input.C1Button btnRefresh;
        private C1.Win.C1Input.C1TextBox txt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private C1.Win.C1Input.C1CheckBox chkIPD;
    }
}