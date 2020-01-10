namespace bangna_hospital.gui
{
    partial class FrmDoctorView
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
            this.tC1 = new C1.Win.C1Command.C1DockingTab();
            this.tabQue = new C1.Win.C1Command.C1DockingTabPage();
            this.pnQue = new System.Windows.Forms.Panel();
            this.tabApm = new C1.Win.C1Command.C1DockingTabPage();
            this.pnApm = new System.Windows.Forms.Panel();
            this.tabRpt = new C1.Win.C1Command.C1DockingTabPage();
            this.lbDtrName = new C1.Win.C1Input.C1Label();
            this.c1Label2 = new C1.Win.C1Input.C1Label();
            this.c1Label3 = new C1.Win.C1Input.C1Label();
            this.txtDate = new C1.Win.C1Input.C1DateEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDtrId = new C1.Win.C1Input.C1Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.c1Label1 = new C1.Win.C1Input.C1Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.btnHnSearch = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tC1)).BeginInit();
            this.tC1.SuspendLayout();
            this.tabQue.SuspendLayout();
            this.tabApm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbDtrName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtrId)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHnSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // theme1
            // 
            this.theme1.Theme = "BeigeOne";
            // 
            // tC1
            // 
            this.tC1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tC1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(161)))), ((int)(((byte)(106)))));
            this.tC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tC1.CanCloseTabs = true;
            this.tC1.CanMoveTabs = true;
            this.tC1.Controls.Add(this.tabQue);
            this.tC1.Controls.Add(this.tabApm);
            this.tC1.Controls.Add(this.tabRpt);
            this.tC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tC1.HotTrack = true;
            this.tC1.Location = new System.Drawing.Point(0, 0);
            this.tC1.Name = "tC1";
            this.tC1.SelectedTabBold = true;
            this.tC1.ShowCaption = true;
            this.tC1.Size = new System.Drawing.Size(1012, 600);
            this.tC1.TabIndex = 0;
            this.tC1.TabLayout = C1.Win.C1Command.ButtonLayoutEnum.TextOnLeft;
            this.tC1.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.tC1.TabsShowFocusCues = false;
            this.tC1.TabsSpacing = 2;
            this.tC1.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007;
            this.theme1.SetTheme(this.tC1, "(default)");
            // 
            // tabQue
            // 
            this.tabQue.CaptionVisible = true;
            this.tabQue.Controls.Add(this.pnQue);
            this.tabQue.Location = new System.Drawing.Point(1, 1);
            this.tabQue.Name = "tabQue";
            this.tabQue.Size = new System.Drawing.Size(1010, 575);
            this.tabQue.TabIndex = 0;
            this.tabQue.Text = "Queue";
            // 
            // pnQue
            // 
            this.pnQue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(161)))), ((int)(((byte)(106)))));
            this.pnQue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnQue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pnQue.Location = new System.Drawing.Point(0, 22);
            this.pnQue.Name = "pnQue";
            this.pnQue.Size = new System.Drawing.Size(1010, 553);
            this.pnQue.TabIndex = 1;
            this.theme1.SetTheme(this.pnQue, "(default)");
            // 
            // tabApm
            // 
            this.tabApm.CaptionVisible = true;
            this.tabApm.Controls.Add(this.pnApm);
            this.tabApm.Location = new System.Drawing.Point(1, 1);
            this.tabApm.Name = "tabApm";
            this.tabApm.Size = new System.Drawing.Size(1010, 575);
            this.tabApm.TabIndex = 1;
            this.tabApm.Text = "Appointment";
            // 
            // pnApm
            // 
            this.pnApm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(161)))), ((int)(((byte)(106)))));
            this.pnApm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnApm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pnApm.Location = new System.Drawing.Point(0, 22);
            this.pnApm.Name = "pnApm";
            this.pnApm.Size = new System.Drawing.Size(1010, 553);
            this.pnApm.TabIndex = 0;
            this.theme1.SetTheme(this.pnApm, "(default)");
            // 
            // tabRpt
            // 
            this.tabRpt.CaptionVisible = true;
            this.tabRpt.Location = new System.Drawing.Point(1, 1);
            this.tabRpt.Name = "tabRpt";
            this.tabRpt.Size = new System.Drawing.Size(1010, 575);
            this.tabRpt.TabIndex = 2;
            this.tabRpt.Text = "Report";
            // 
            // lbDtrName
            // 
            this.lbDtrName.AutoSize = true;
            this.lbDtrName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbDtrName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbDtrName.Location = new System.Drawing.Point(13, 12);
            this.lbDtrName.Name = "lbDtrName";
            this.lbDtrName.Size = new System.Drawing.Size(57, 13);
            this.lbDtrName.TabIndex = 0;
            this.lbDtrName.Tag = null;
            this.theme1.SetTheme(this.lbDtrName, "(default)");
            this.lbDtrName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom;
            // 
            // c1Label2
            // 
            this.c1Label2.AutoSize = true;
            this.c1Label2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.c1Label2.Location = new System.Drawing.Point(462, 12);
            this.c1Label2.Name = "c1Label2";
            this.c1Label2.Size = new System.Drawing.Size(51, 13);
            this.c1Label2.TabIndex = 1;
            this.c1Label2.Tag = null;
            this.theme1.SetTheme(this.c1Label2, "(default)");
            this.c1Label2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom;
            // 
            // c1Label3
            // 
            this.c1Label3.AutoSize = true;
            this.c1Label3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.c1Label3.Location = new System.Drawing.Point(7, 8);
            this.c1Label3.Name = "c1Label3";
            this.c1Label3.Size = new System.Drawing.Size(51, 13);
            this.c1Label3.TabIndex = 2;
            this.c1Label3.Tag = null;
            this.theme1.SetTheme(this.c1Label3, "(default)");
            this.c1Label3.Value = "Date :";
            this.c1Label3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom;
            // 
            // txtDate
            // 
            this.txtDate.AllowSpinLoop = false;
            this.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.txtDate.Calendar.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.txtDate.Calendar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(161)))), ((int)(((byte)(106)))));
            this.txtDate.Calendar.DayNamesColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.txtDate.Calendar.DayNamesFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtDate.Calendar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(81)))), ((int)(((byte)(0)))));
            this.txtDate.Calendar.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(155)))), ((int)(((byte)(118)))));
            this.txtDate.Calendar.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(36)))), ((int)(((byte)(0)))));
            this.txtDate.Calendar.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(161)))), ((int)(((byte)(106)))));
            this.txtDate.Calendar.TitleFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtDate.Calendar.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.txtDate.Calendar.TodayBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(51)))), ((int)(((byte)(0)))));
            this.txtDate.Calendar.TrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(101)))), ((int)(((byte)(101)))));
            this.txtDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtDate.CalendarType = C1.Win.C1Input.CalendarType.GregorianCalendar;
            this.txtDate.Culture = 1054;
            this.txtDate.CurrentTimeZone = false;
            this.txtDate.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(101)))), ((int)(((byte)(101)))));
            this.txtDate.DisplayFormat.CalendarType = C1.Win.C1Input.CalendarType.GregorianCalendar;
            this.txtDate.DisplayFormat.CustomFormat = "dd-MM-yyyy";
            this.txtDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)));
            this.txtDate.EditFormat.CustomFormat = "dd-MM-yyyy";
            this.txtDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtDate.GMTOffset = System.TimeSpan.Parse("00:00:00");
            this.txtDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtDate.Location = new System.Drawing.Point(76, 8);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(110, 18);
            this.txtDate.TabIndex = 3;
            this.txtDate.Tag = null;
            this.theme1.SetTheme(this.txtDate, "(default)");
            this.txtDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(161)))), ((int)(((byte)(106)))));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.txtDtrId);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.lbDtrName);
            this.panel1.Controls.Add(this.c1Label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1012, 44);
            this.panel1.TabIndex = 1;
            this.theme1.SetTheme(this.panel1, "(default)");
            // 
            // txtDtrId
            // 
            this.txtDtrId.AutoSize = true;
            this.txtDtrId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDtrId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.txtDtrId.Location = new System.Drawing.Point(346, 28);
            this.txtDtrId.Name = "txtDtrId";
            this.txtDtrId.Size = new System.Drawing.Size(41, 13);
            this.txtDtrId.TabIndex = 2;
            this.txtDtrId.Tag = null;
            this.theme1.SetTheme(this.txtDtrId, "(default)");
            this.txtDtrId.Visible = false;
            this.txtDtrId.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(161)))), ((int)(((byte)(106)))));
            this.panel3.Controls.Add(this.c1Label3);
            this.panel3.Controls.Add(this.txtDate);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel3.Location = new System.Drawing.Point(814, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(198, 44);
            this.panel3.TabIndex = 0;
            this.theme1.SetTheme(this.panel3, "(default)");
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(161)))), ((int)(((byte)(106)))));
            this.panel2.Controls.Add(this.tC1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel2.Location = new System.Drawing.Point(0, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1012, 600);
            this.panel2.TabIndex = 2;
            this.theme1.SetTheme(this.panel2, "(default)");
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(161)))), ((int)(((byte)(106)))));
            this.panel4.Controls.Add(this.btnHnSearch);
            this.panel4.Controls.Add(this.txtHn);
            this.panel4.Controls.Add(this.c1Label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel4.Location = new System.Drawing.Point(602, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(212, 44);
            this.panel4.TabIndex = 3;
            this.theme1.SetTheme(this.panel4, "(default)");
            // 
            // c1Label1
            // 
            this.c1Label1.AutoSize = true;
            this.c1Label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.c1Label1.Location = new System.Drawing.Point(10, 16);
            this.c1Label1.Name = "c1Label1";
            this.c1Label1.Size = new System.Drawing.Size(51, 13);
            this.c1Label1.TabIndex = 2;
            this.c1Label1.Tag = null;
            this.theme1.SetTheme(this.c1Label1, "(default)");
            this.c1Label1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom;
            // 
            // txtHn
            // 
            this.txtHn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHn.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(101)))), ((int)(((byte)(101)))));
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHn.Location = new System.Drawing.Point(67, 12);
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(100, 20);
            this.txtHn.TabIndex = 3;
            this.txtHn.Tag = null;
            this.theme1.SetTheme(this.txtHn, "(default)");
            this.txtHn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnHnSearch
            // 
            this.btnHnSearch.Location = new System.Drawing.Point(173, 11);
            this.btnHnSearch.Name = "btnHnSearch";
            this.btnHnSearch.Size = new System.Drawing.Size(32, 23);
            this.btnHnSearch.TabIndex = 4;
            this.btnHnSearch.Text = "...";
            this.theme1.SetTheme(this.btnHnSearch, "(default)");
            this.btnHnSearch.UseVisualStyleBackColor = true;
            this.btnHnSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // FrmDoctorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 644);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmDoctorView";
            this.Text = "FrmDoctorView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmDoctorView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tC1)).EndInit();
            this.tC1.ResumeLayout(false);
            this.tabQue.ResumeLayout(false);
            this.tabApm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbDtrName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtrId)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHnSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController theme1;
        private C1.Win.C1Command.C1DockingTab tC1;
        private C1.Win.C1Command.C1DockingTabPage tabQue;
        private C1.Win.C1Command.C1DockingTabPage tabApm;
        private System.Windows.Forms.Panel pnApm;
        private C1.Win.C1Command.C1DockingTabPage tabRpt;
        private System.Windows.Forms.Panel pnQue;
        private C1.Win.C1Input.C1Label lbDtrName;
        private C1.Win.C1Input.C1Label c1Label3;
        private C1.Win.C1Input.C1Label c1Label2;
        private C1.Win.C1Input.C1DateEdit txtDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private C1.Win.C1Input.C1Label txtDtrId;
        private System.Windows.Forms.Panel panel4;
        private C1.Win.C1Input.C1TextBox txtHn;
        private C1.Win.C1Input.C1Label c1Label1;
        private C1.Win.C1Input.C1Button btnHnSearch;
    }
}