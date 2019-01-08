namespace bangna_hospital.gui
{
    partial class FrmSearchHn
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOk = new C1.Win.C1Input.C1Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVn = new C1.Win.C1Input.C1TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new C1.Win.C1Input.C1TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.tC1 = new C1.Win.C1Command.C1DockingTab();
            this.tabCurrent = new C1.Win.C1Command.C1DockingTabPage();
            this.gbCu = new System.Windows.Forms.GroupBox();
            this.tabSearch = new C1.Win.C1Command.C1DockingTabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbHn = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtVisitDate = new C1.Win.C1Input.C1DateEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearch = new C1.Win.C1Input.C1TextBox();
            this.btnSearch = new C1.Win.C1Input.C1Button();
            this.txtDateEnd = new C1.Win.C1Input.C1DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDateStart = new C1.Win.C1Input.C1DateEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.sB = new System.Windows.Forms.StatusStrip();
            this.sB1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtPreNo = new C1.Win.C1Input.C1TextBox();
            this.chkIPD = new C1.Win.C1Input.C1CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAn = new C1.Win.C1Input.C1TextBox();
            this.txtAnDate = new C1.Win.C1Input.C1DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tC1)).BeginInit();
            this.tC1.SuspendLayout();
            this.tabCurrent.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbHn.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVisitDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStart)).BeginInit();
            this.sB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnDate)).BeginInit();
            this.SuspendLayout();
            // 
            // theme1
            // 
            this.theme1.Theme = "Office2013Red";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox2.Controls.Add(this.txtAnDate);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtAn);
            this.groupBox2.Controls.Add(this.txtPreNo);
            this.groupBox2.Controls.Add(this.btnOk);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtVn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtHn);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(944, 63);
            this.groupBox2.TabIndex = 526;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Patient";
            this.theme1.SetTheme(this.groupBox2, "(default)");
            // 
            // btnOk
            // 
            this.btnOk.Image = global::bangna_hospital.Properties.Resources.Female_user_search_24;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(837, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(95, 43);
            this.btnOk.TabIndex = 526;
            this.btnOk.Text = "กลับหน้าหลัก";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnOk, "(default)");
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label5.Location = new System.Drawing.Point(538, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 16);
            this.label5.TabIndex = 525;
            this.label5.Text = "VN :";
            this.theme1.SetTheme(this.label5, "(default)");
            // 
            // txtVn
            // 
            this.txtVn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtVn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVn.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtVn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtVn.Location = new System.Drawing.Point(577, 19);
            this.txtVn.Name = "txtVn";
            this.txtVn.ReadOnly = true;
            this.txtVn.Size = new System.Drawing.Size(82, 20);
            this.txtVn.TabIndex = 524;
            this.txtVn.Tag = null;
            this.theme1.SetTheme(this.txtVn, "(default)");
            this.txtVn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label2.Location = new System.Drawing.Point(205, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 523;
            this.label2.Text = "name :";
            this.theme1.SetTheme(this.label2, "(default)");
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtName.Location = new System.Drawing.Point(259, 19);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(268, 20);
            this.txtName.TabIndex = 522;
            this.txtName.Tag = null;
            this.theme1.SetTheme(this.txtName, "(default)");
            this.txtName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label3.Location = new System.Drawing.Point(11, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 16);
            this.label3.TabIndex = 521;
            this.label3.Text = "HN  :";
            this.theme1.SetTheme(this.label3, "(default)");
            // 
            // txtHn
            // 
            this.txtHn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtHn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHn.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHn.Location = new System.Drawing.Point(62, 19);
            this.txtHn.Name = "txtHn";
            this.txtHn.ReadOnly = true;
            this.txtHn.Size = new System.Drawing.Size(124, 20);
            this.txtHn.TabIndex = 520;
            this.txtHn.Tag = null;
            this.theme1.SetTheme(this.txtHn, "(default)");
            this.txtHn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // tC1
            // 
            this.tC1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tC1.Controls.Add(this.tabCurrent);
            this.tC1.Controls.Add(this.tabSearch);
            this.tC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tC1.HotTrack = true;
            this.tC1.Location = new System.Drawing.Point(0, 63);
            this.tC1.Name = "tC1";
            this.tC1.SelectedIndex = 1;
            this.tC1.Size = new System.Drawing.Size(944, 451);
            this.tC1.TabIndex = 527;
            this.tC1.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.tC1.TabsShowFocusCues = false;
            this.tC1.TabsSpacing = 2;
            this.tC1.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007;
            this.theme1.SetTheme(this.tC1, "(default)");
            // 
            // tabCurrent
            // 
            this.tabCurrent.Controls.Add(this.gbCu);
            this.tabCurrent.Location = new System.Drawing.Point(1, 24);
            this.tabCurrent.Name = "tabCurrent";
            this.tabCurrent.Size = new System.Drawing.Size(942, 426);
            this.tabCurrent.TabIndex = 0;
            this.tabCurrent.Text = "current visit";
            // 
            // gbCu
            // 
            this.gbCu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gbCu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.gbCu.Location = new System.Drawing.Point(0, 0);
            this.gbCu.Name = "gbCu";
            this.gbCu.Size = new System.Drawing.Size(942, 426);
            this.gbCu.TabIndex = 0;
            this.gbCu.TabStop = false;
            this.gbCu.Text = "groupBox2";
            this.theme1.SetTheme(this.gbCu, "(default)");
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this.panel1);
            this.tabSearch.Location = new System.Drawing.Point(1, 24);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Size = new System.Drawing.Size(942, 426);
            this.tabSearch.TabIndex = 1;
            this.tabSearch.Text = "search hn";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.gbHn);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(942, 426);
            this.panel1.TabIndex = 0;
            this.theme1.SetTheme(this.panel1, "(default)");
            // 
            // gbHn
            // 
            this.gbHn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gbHn.Controls.Add(this.panel3);
            this.gbHn.Controls.Add(this.panel2);
            this.gbHn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbHn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.gbHn.Location = new System.Drawing.Point(0, 45);
            this.gbHn.Name = "gbHn";
            this.gbHn.Size = new System.Drawing.Size(942, 381);
            this.gbHn.TabIndex = 528;
            this.gbHn.TabStop = false;
            this.gbHn.Text = "Patient Search";
            this.theme1.SetTheme(this.gbHn, "(default)");
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panel3.Location = new System.Drawing.Point(439, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(500, 362);
            this.panel3.TabIndex = 1;
            this.theme1.SetTheme(this.panel3, "(default)");
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panel2.Location = new System.Drawing.Point(3, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(436, 362);
            this.panel2.TabIndex = 0;
            this.theme1.SetTheme(this.panel2, "(default)");
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox1.Controls.Add(this.chkIPD);
            this.groupBox1.Controls.Add(this.txtVisitDate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtDateEnd);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDateStart);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(942, 45);
            this.groupBox1.TabIndex = 527;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            this.theme1.SetTheme(this.groupBox1, "(default)");
            // 
            // txtVisitDate
            // 
            this.txtVisitDate.AllowSpinLoop = false;
            this.txtVisitDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.txtVisitDate.Calendar.ArrowColor = System.Drawing.Color.Black;
            this.txtVisitDate.Calendar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtVisitDate.Calendar.DayNamesFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtVisitDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtVisitDate.Calendar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtVisitDate.Calendar.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(71)))), ((int)(((byte)(47)))));
            this.txtVisitDate.Calendar.SelectionForeColor = System.Drawing.Color.White;
            this.txtVisitDate.Calendar.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtVisitDate.Calendar.TitleFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtVisitDate.Calendar.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtVisitDate.Calendar.TodayBorderColor = System.Drawing.Color.White;
            this.txtVisitDate.Calendar.TrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtVisitDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtVisitDate.Culture = 1054;
            this.txtVisitDate.CurrentTimeZone = false;
            this.txtVisitDate.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
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
            this.txtVisitDate.Location = new System.Drawing.Point(706, 13);
            this.txtVisitDate.Name = "txtVisitDate";
            this.txtVisitDate.Size = new System.Drawing.Size(111, 18);
            this.txtVisitDate.TabIndex = 521;
            this.txtVisitDate.Tag = null;
            this.theme1.SetTheme(this.txtVisitDate, "(default)");
            this.txtVisitDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label6.Location = new System.Drawing.Point(627, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 520;
            this.label6.Text = "Visit Date :";
            this.theme1.SetTheme(this.label6, "(default)");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label4.Location = new System.Drawing.Point(435, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 16);
            this.label4.TabIndex = 519;
            this.label4.Text = "HN  :";
            this.theme1.SetTheme(this.label4, "(default)");
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSearch.Location = new System.Drawing.Point(475, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(110, 20);
            this.txtSearch.TabIndex = 518;
            this.txtSearch.Tag = null;
            this.theme1.SetTheme(this.txtSearch, "(default)");
            this.txtSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(591, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(28, 23);
            this.btnSearch.TabIndex = 517;
            this.btnSearch.Text = "...";
            this.theme1.SetTheme(this.btnSearch, "(default)");
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // txtDateEnd
            // 
            this.txtDateEnd.AllowSpinLoop = false;
            this.txtDateEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.txtDateEnd.Calendar.ArrowColor = System.Drawing.Color.Black;
            this.txtDateEnd.Calendar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtDateEnd.Calendar.DayNamesFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateEnd.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateEnd.Calendar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtDateEnd.Calendar.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(71)))), ((int)(((byte)(47)))));
            this.txtDateEnd.Calendar.SelectionForeColor = System.Drawing.Color.White;
            this.txtDateEnd.Calendar.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtDateEnd.Calendar.TitleFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateEnd.Calendar.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtDateEnd.Calendar.TodayBorderColor = System.Drawing.Color.White;
            this.txtDateEnd.Calendar.TrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtDateEnd.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtDateEnd.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtDateEnd.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtDateEnd.Location = new System.Drawing.Point(287, 13);
            this.txtDateEnd.Name = "txtDateEnd";
            this.txtDateEnd.Size = new System.Drawing.Size(129, 18);
            this.txtDateEnd.TabIndex = 514;
            this.txtDateEnd.Tag = null;
            this.theme1.SetTheme(this.txtDateEnd, "(default)");
            this.txtDateEnd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label1.Location = new System.Drawing.Point(211, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 513;
            this.label1.Text = "Date End :";
            this.theme1.SetTheme(this.label1, "(default)");
            // 
            // txtDateStart
            // 
            this.txtDateStart.AllowSpinLoop = false;
            this.txtDateStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.txtDateStart.Calendar.ArrowColor = System.Drawing.Color.Black;
            this.txtDateStart.Calendar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtDateStart.Calendar.DayNamesFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateStart.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateStart.Calendar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtDateStart.Calendar.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(71)))), ((int)(((byte)(47)))));
            this.txtDateStart.Calendar.SelectionForeColor = System.Drawing.Color.White;
            this.txtDateStart.Calendar.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtDateStart.Calendar.TitleFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateStart.Calendar.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtDateStart.Calendar.TodayBorderColor = System.Drawing.Color.White;
            this.txtDateStart.Calendar.TrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtDateStart.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtDateStart.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtDateStart.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtDateStart.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtDateStart.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtDateStart.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtDateStart.Location = new System.Drawing.Point(90, 13);
            this.txtDateStart.Name = "txtDateStart";
            this.txtDateStart.Size = new System.Drawing.Size(111, 18);
            this.txtDateStart.TabIndex = 512;
            this.txtDateStart.Tag = null;
            this.theme1.SetTheme(this.txtDateStart, "(default)");
            this.txtDateStart.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label11.Location = new System.Drawing.Point(11, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 16);
            this.label11.TabIndex = 511;
            this.label11.Text = "Date Start :";
            this.theme1.SetTheme(this.label11, "(default)");
            // 
            // sB
            // 
            this.sB.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sB1});
            this.sB.Location = new System.Drawing.Point(0, 514);
            this.sB.Name = "sB";
            this.sB.Size = new System.Drawing.Size(944, 22);
            this.sB.TabIndex = 525;
            this.sB.Text = "statusStrip1";
            // 
            // sB1
            // 
            this.sB1.Name = "sB1";
            this.sB1.Size = new System.Drawing.Size(118, 17);
            this.sB1.Text = "toolStripStatusLabel1";
            // 
            // txtPreNo
            // 
            this.txtPreNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPreNo.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtPreNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPreNo.Location = new System.Drawing.Point(665, 19);
            this.txtPreNo.Name = "txtPreNo";
            this.txtPreNo.ReadOnly = true;
            this.txtPreNo.Size = new System.Drawing.Size(82, 20);
            this.txtPreNo.TabIndex = 527;
            this.txtPreNo.Tag = null;
            this.theme1.SetTheme(this.txtPreNo, "(default)");
            this.txtPreNo.Visible = false;
            this.txtPreNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkIPD
            // 
            this.chkIPD.BackColor = System.Drawing.Color.Transparent;
            this.chkIPD.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.chkIPD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkIPD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkIPD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.chkIPD.Location = new System.Drawing.Point(823, 10);
            this.chkIPD.Name = "chkIPD";
            this.chkIPD.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.chkIPD.Size = new System.Drawing.Size(104, 24);
            this.chkIPD.TabIndex = 524;
            this.chkIPD.Text = "IPD";
            this.theme1.SetTheme(this.chkIPD, "(default)");
            this.chkIPD.UseVisualStyleBackColor = true;
            this.chkIPD.Value = null;
            this.chkIPD.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label7.Location = new System.Drawing.Point(697, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 16);
            this.label7.TabIndex = 529;
            this.label7.Text = "AN :";
            this.theme1.SetTheme(this.label7, "(default)");
            // 
            // txtAn
            // 
            this.txtAn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAn.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtAn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAn.Location = new System.Drawing.Point(736, 21);
            this.txtAn.Name = "txtAn";
            this.txtAn.ReadOnly = true;
            this.txtAn.Size = new System.Drawing.Size(82, 20);
            this.txtAn.TabIndex = 528;
            this.txtAn.Tag = null;
            this.theme1.SetTheme(this.txtAn, "(default)");
            this.txtAn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // txtAnDate
            // 
            this.txtAnDate.AllowSpinLoop = false;
            this.txtAnDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.txtAnDate.Calendar.ArrowColor = System.Drawing.Color.Black;
            this.txtAnDate.Calendar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtAnDate.Calendar.DayNamesFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtAnDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtAnDate.Calendar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtAnDate.Calendar.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(71)))), ((int)(((byte)(47)))));
            this.txtAnDate.Calendar.SelectionForeColor = System.Drawing.Color.White;
            this.txtAnDate.Calendar.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtAnDate.Calendar.TitleFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtAnDate.Calendar.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtAnDate.Calendar.TodayBorderColor = System.Drawing.Color.White;
            this.txtAnDate.Calendar.TrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtAnDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtAnDate.Culture = 1054;
            this.txtAnDate.CurrentTimeZone = false;
            this.txtAnDate.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
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
            this.txtAnDate.Location = new System.Drawing.Point(577, 37);
            this.txtAnDate.Name = "txtAnDate";
            this.txtAnDate.Size = new System.Drawing.Size(111, 18);
            this.txtAnDate.TabIndex = 530;
            this.txtAnDate.Tag = null;
            this.theme1.SetTheme(this.txtAnDate, "(default)");
            this.txtAnDate.Visible = false;
            this.txtAnDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // FrmSearchHn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 536);
            this.Controls.Add(this.tC1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.sB);
            this.Name = "FrmSearchHn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSearchHn";
            this.Load += new System.EventHandler(this.FrmSearchHn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tC1)).EndInit();
            this.tC1.ResumeLayout(false);
            this.tabCurrent.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gbHn.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVisitDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStart)).EndInit();
            this.sB.ResumeLayout(false);
            this.sB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController theme1;
        private System.Windows.Forms.StatusStrip sB;
        private System.Windows.Forms.ToolStripStatusLabel sB1;
        private System.Windows.Forms.GroupBox groupBox2;
        private C1.Win.C1Command.C1DockingTab tC1;
        private C1.Win.C1Command.C1DockingTabPage tabCurrent;
        private System.Windows.Forms.GroupBox gbCu;
        private C1.Win.C1Command.C1DockingTabPage tabSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbHn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Input.C1TextBox txtSearch;
        private C1.Win.C1Input.C1Button btnSearch;
        private C1.Win.C1Input.C1DateEdit txtDateEnd;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1DateEdit txtDateStart;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1TextBox txtName;
        private System.Windows.Forms.Label label3;
        private C1.Win.C1Input.C1TextBox txtHn;
        private System.Windows.Forms.Label label5;
        private C1.Win.C1Input.C1TextBox txtVn;
        private C1.Win.C1Input.C1Button btnOk;
        private C1.Win.C1Input.C1DateEdit txtVisitDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private C1.Win.C1Input.C1TextBox txtPreNo;
        private C1.Win.C1Input.C1CheckBox chkIPD;
        private System.Windows.Forms.Label label7;
        private C1.Win.C1Input.C1TextBox txtAn;
        private C1.Win.C1Input.C1DateEdit txtAnDate;
    }
}