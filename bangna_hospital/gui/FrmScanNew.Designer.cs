namespace bangna_hospital.gui
{
    partial class FrmScanNew
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
            this.txtAnCnt = new C1.Win.C1Input.C1TextBox();
            this.chkIPD = new C1.Win.C1Input.C1CheckBox();
            this.txtAnDate = new C1.Win.C1Input.C1DateEdit();
            this.txtPreNo = new C1.Win.C1Input.C1TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAN = new C1.Win.C1Input.C1TextBox();
            this.txtVisitDate = new C1.Win.C1Input.C1DateEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOpen = new C1.Win.C1Input.C1Button();
            this.btnVn = new C1.Win.C1Input.C1Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVN = new C1.Win.C1Input.C1TextBox();
            this.txtName = new C1.Win.C1Input.C1TextBox();
            this.btnHn = new C1.Win.C1Input.C1Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDel = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVisitDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDel)).BeginInit();
            this.SuspendLayout();
            // 
            // sb1
            // 
            this.sb1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.sb1.Location = new System.Drawing.Point(0, 712);
            this.sb1.Name = "sb1";
            this.sb1.Size = new System.Drawing.Size(1090, 22);
            this.theme1.SetTheme(this.sb1, "(default)");
            this.sb1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Custom;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.btnDel);
            this.groupBox1.Controls.Add(this.txtAnCnt);
            this.groupBox1.Controls.Add(this.chkIPD);
            this.groupBox1.Controls.Add(this.txtAnDate);
            this.groupBox1.Controls.Add(this.txtPreNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtAN);
            this.groupBox1.Controls.Add(this.txtVisitDate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.btnVn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtVN);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.btnHn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1090, 66);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patient";
            this.theme1.SetTheme(this.groupBox1, "(default)");
            // 
            // txtAnCnt
            // 
            this.txtAnCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAnCnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAnCnt.Location = new System.Drawing.Point(424, 37);
            this.txtAnCnt.Name = "txtAnCnt";
            this.txtAnCnt.Size = new System.Drawing.Size(35, 20);
            this.txtAnCnt.TabIndex = 554;
            this.txtAnCnt.Tag = null;
            this.theme1.SetTheme(this.txtAnCnt, "(default)");
            this.txtAnCnt.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtAnCnt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // chkIPD
            // 
            this.chkIPD.BackColor = System.Drawing.Color.Transparent;
            this.chkIPD.BorderColor = System.Drawing.Color.Transparent;
            this.chkIPD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkIPD.ForeColor = System.Drawing.Color.Black;
            this.chkIPD.Location = new System.Drawing.Point(465, 36);
            this.chkIPD.Name = "chkIPD";
            this.chkIPD.Padding = new System.Windows.Forms.Padding(1);
            this.chkIPD.Size = new System.Drawing.Size(61, 24);
            this.chkIPD.TabIndex = 553;
            this.chkIPD.Text = "IPD";
            this.theme1.SetTheme(this.chkIPD, "(default)");
            this.chkIPD.UseVisualStyleBackColor = true;
            this.chkIPD.Value = null;
            this.chkIPD.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkIPD.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // txtAnDate
            // 
            this.txtAnDate.AllowSpinLoop = false;
            this.txtAnDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.txtAnDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtAnDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtAnDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtAnDate.Culture = 1054;
            this.txtAnDate.CurrentTimeZone = false;
            this.txtAnDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtAnDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)((((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat | C1.Win.C1Input.FormatInfoInheritFlags.NullText) 
            | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtAnDate.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtAnDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtAnDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtAnDate.GMTOffset = System.TimeSpan.Parse("00:00:00");
            this.txtAnDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtAnDate.Location = new System.Drawing.Point(656, 39);
            this.txtAnDate.Name = "txtAnDate";
            this.txtAnDate.Size = new System.Drawing.Size(111, 18);
            this.txtAnDate.TabIndex = 552;
            this.txtAnDate.Tag = null;
            this.theme1.SetTheme(this.txtAnDate, "(default)");
            this.txtAnDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtAnDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // txtPreNo
            // 
            this.txtPreNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPreNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPreNo.Location = new System.Drawing.Point(761, 37);
            this.txtPreNo.Name = "txtPreNo";
            this.txtPreNo.Size = new System.Drawing.Size(75, 20);
            this.txtPreNo.TabIndex = 551;
            this.txtPreNo.Tag = null;
            this.theme1.SetTheme(this.txtPreNo, "(default)");
            this.txtPreNo.Visible = false;
            this.txtPreNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtPreNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label3.Location = new System.Drawing.Point(244, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 16);
            this.label3.TabIndex = 550;
            this.label3.Text = "AN  :";
            this.theme1.SetTheme(this.label3, "(default)");
            // 
            // txtAN
            // 
            this.txtAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAN.Location = new System.Drawing.Point(286, 37);
            this.txtAN.Name = "txtAN";
            this.txtAN.Size = new System.Drawing.Size(132, 20);
            this.txtAN.TabIndex = 549;
            this.txtAN.Tag = null;
            this.theme1.SetTheme(this.txtAN, "(default)");
            this.txtAN.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtAN.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
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
            this.txtVisitDate.Location = new System.Drawing.Point(636, 39);
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
            this.label6.Location = new System.Drawing.Point(535, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 547;
            this.label6.Text = "Visit Date :";
            this.theme1.SetTheme(this.label6, "(default)");
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::bangna_hospital.Properties.Resources.Open_Large;
            this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpen.Location = new System.Drawing.Point(981, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(71, 30);
            this.btnOpen.TabIndex = 546;
            this.btnOpen.Text = "Open";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnOpen, "(default)");
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnVn
            // 
            this.btnVn.Location = new System.Drawing.Point(205, 36);
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
            this.txtVN.Location = new System.Drawing.Point(49, 37);
            this.txtVN.Name = "txtVN";
            this.txtVN.Size = new System.Drawing.Size(150, 20);
            this.txtVN.TabIndex = 543;
            this.txtVN.Tag = null;
            this.theme1.SetTheme(this.txtVN, "(default)");
            this.txtVN.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtVN.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtName.Location = new System.Drawing.Point(239, 13);
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
            this.btnHn.Location = new System.Drawing.Point(205, 12);
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
            this.txtHn.Location = new System.Drawing.Point(49, 13);
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
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel1.Location = new System.Drawing.Point(0, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1090, 646);
            this.panel1.TabIndex = 4;
            this.theme1.SetTheme(this.panel1, "(default)");
            // 
            // btnDel
            // 
            this.btnDel.Image = global::bangna_hospital.Properties.Resources.trash24;
            this.btnDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDel.Location = new System.Drawing.Point(884, 13);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(71, 30);
            this.btnDel.TabIndex = 555;
            this.btnDel.Text = "Delete";
            this.btnDel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnDel, "(default)");
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // FrmScanNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 734);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.sb1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.Name = "FrmScanNew";
            this.Text = "FrmScanNew";
            this.theme1.SetTheme(this, "(default)");
            this.Load += new System.EventHandler(this.FrmScanNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVisitDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController theme1;
        private C1.Win.C1Ribbon.C1StatusBar sb1;
        private System.Windows.Forms.GroupBox groupBox1;
        private C1.Win.C1Input.C1TextBox txtName;
        private C1.Win.C1Input.C1Button btnHn;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Input.C1TextBox txtHn;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1TextBox txtVN;
        private C1.Win.C1Input.C1Button btnVn;
        private C1.Win.C1Input.C1Button btnOpen;
        private C1.Win.C1Input.C1DateEdit txtVisitDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private C1.Win.C1Input.C1TextBox txtAN;
        private C1.Win.C1Input.C1TextBox txtPreNo;
        private C1.Win.C1Input.C1DateEdit txtAnDate;
        private C1.Win.C1Input.C1CheckBox chkIPD;
        private C1.Win.C1Input.C1TextBox txtAnCnt;
        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1Input.C1Button btnDel;
    }
}