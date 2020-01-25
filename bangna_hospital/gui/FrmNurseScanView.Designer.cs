namespace bangna_hospital.gui
{
    partial class FrmNurseScanView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtDateEnd = new C1.Win.C1Input.C1DateEdit();
            this.c1Label4 = new C1.Win.C1Input.C1Label();
            this.txtDateStart = new C1.Win.C1Input.C1DateEdit();
            this.c1Label3 = new C1.Win.C1Input.C1Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkDoctor = new System.Windows.Forms.RadioButton();
            this.chkHn = new System.Windows.Forms.RadioButton();
            this.btnOk = new C1.Win.C1Input.C1Button();
            this.lbName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1001, 75);
            this.panel1.TabIndex = 0;
            this.theme1.SetTheme(this.panel1, "(default)");
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel2.Location = new System.Drawing.Point(0, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1001, 375);
            this.panel2.TabIndex = 1;
            this.theme1.SetTheme(this.panel2, "(default)");
            // 
            // txtDateEnd
            // 
            this.txtDateEnd.AllowSpinLoop = false;
            this.txtDateEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.txtDateEnd.Calendar.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.txtDateEnd.Calendar.BackColor = System.Drawing.Color.White;
            this.txtDateEnd.Calendar.DayNamesColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.txtDateEnd.Calendar.DayNamesFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateEnd.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateEnd.Calendar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.txtDateEnd.Calendar.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.txtDateEnd.Calendar.SelectionForeColor = System.Drawing.Color.White;
            this.txtDateEnd.Calendar.TitleBackColor = System.Drawing.Color.White;
            this.txtDateEnd.Calendar.TitleFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateEnd.Calendar.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.txtDateEnd.Calendar.TodayBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.txtDateEnd.Calendar.TrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtDateEnd.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtDateEnd.Culture = 1054;
            this.txtDateEnd.CurrentTimeZone = false;
            this.txtDateEnd.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            this.txtDateEnd.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtDateEnd.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtDateEnd.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtDateEnd.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtDateEnd.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtDateEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateEnd.GMTOffset = System.TimeSpan.Parse("00:00:00");
            this.txtDateEnd.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtDateEnd.Location = new System.Drawing.Point(635, 9);
            this.txtDateEnd.Name = "txtDateEnd";
            this.txtDateEnd.Size = new System.Drawing.Size(134, 20);
            this.txtDateEnd.TabIndex = 18;
            this.txtDateEnd.Tag = null;
            this.theme1.SetTheme(this.txtDateEnd, "(default)");
            this.txtDateEnd.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtDateEnd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // c1Label4
            // 
            this.c1Label4.AutoSize = true;
            this.c1Label4.BorderColor = System.Drawing.Color.Transparent;
            this.c1Label4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1Label4.Location = new System.Drawing.Point(563, 10);
            this.c1Label4.Name = "c1Label4";
            this.c1Label4.Size = new System.Drawing.Size(51, 13);
            this.c1Label4.TabIndex = 17;
            this.c1Label4.Tag = null;
            this.theme1.SetTheme(this.c1Label4, "(default)");
            this.c1Label4.Value = "วันที่สิ้นสุด:";
            // 
            // txtDateStart
            // 
            this.txtDateStart.AllowSpinLoop = false;
            this.txtDateStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.txtDateStart.Calendar.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.txtDateStart.Calendar.BackColor = System.Drawing.Color.White;
            this.txtDateStart.Calendar.DayNamesColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.txtDateStart.Calendar.DayNamesFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateStart.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateStart.Calendar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.txtDateStart.Calendar.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.txtDateStart.Calendar.SelectionForeColor = System.Drawing.Color.White;
            this.txtDateStart.Calendar.TitleBackColor = System.Drawing.Color.White;
            this.txtDateStart.Calendar.TitleFont = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateStart.Calendar.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.txtDateStart.Calendar.TodayBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.txtDateStart.Calendar.TrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtDateStart.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtDateStart.CurrentTimeZone = false;
            this.txtDateStart.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            this.txtDateStart.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtDateStart.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtDateStart.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtDateStart.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtDateStart.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtDateStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateStart.GMTOffset = System.TimeSpan.Parse("00:00:00");
            this.txtDateStart.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtDateStart.Location = new System.Drawing.Point(420, 9);
            this.txtDateStart.Name = "txtDateStart";
            this.txtDateStart.Size = new System.Drawing.Size(134, 20);
            this.txtDateStart.TabIndex = 16;
            this.txtDateStart.Tag = null;
            this.theme1.SetTheme(this.txtDateStart, "(default)");
            this.txtDateStart.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtDateStart.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // c1Label3
            // 
            this.c1Label3.AutoSize = true;
            this.c1Label3.BorderColor = System.Drawing.Color.Transparent;
            this.c1Label3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1Label3.Location = new System.Drawing.Point(359, 10);
            this.c1Label3.Name = "c1Label3";
            this.c1Label3.Size = new System.Drawing.Size(51, 13);
            this.c1Label3.TabIndex = 15;
            this.c1Label3.Tag = null;
            this.theme1.SetTheme(this.c1Label3, "(default)");
            this.c1Label3.Value = "วันที่เริ่มต้น:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label4.Location = new System.Drawing.Point(153, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 541;
            this.label4.Text = "ค้นหา  :";
            this.theme1.SetTheme(this.label4, "(default)");
            // 
            // txtHn
            // 
            this.txtHn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHn.Location = new System.Drawing.Point(203, 3);
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(150, 27);
            this.txtHn.TabIndex = 540;
            this.txtHn.Tag = null;
            this.theme1.SetTheme(this.txtHn, "(default)");
            this.txtHn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtHn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.lbName);
            this.panel3.Controls.Add(this.txtDateEnd);
            this.panel3.Controls.Add(this.c1Label4);
            this.panel3.Controls.Add(this.btnOk);
            this.panel3.Controls.Add(this.chkDoctor);
            this.panel3.Controls.Add(this.txtDateStart);
            this.panel3.Controls.Add(this.c1Label3);
            this.panel3.Controls.Add(this.chkHn);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtHn);
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel3.Location = new System.Drawing.Point(12, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(813, 68);
            this.panel3.TabIndex = 542;
            this.theme1.SetTheme(this.panel3, "(default)");
            // 
            // chkDoctor
            // 
            this.chkDoctor.AutoSize = true;
            this.chkDoctor.BackColor = System.Drawing.Color.Transparent;
            this.chkDoctor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDoctor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.chkDoctor.Location = new System.Drawing.Point(79, 8);
            this.chkDoctor.Name = "chkDoctor";
            this.chkDoctor.Size = new System.Drawing.Size(63, 20);
            this.chkDoctor.TabIndex = 1;
            this.chkDoctor.TabStop = true;
            this.chkDoctor.Text = "ว.แพทย์";
            this.theme1.SetTheme(this.chkDoctor, "(default)");
            this.chkDoctor.UseVisualStyleBackColor = false;
            // 
            // chkHn
            // 
            this.chkHn.AutoSize = true;
            this.chkHn.BackColor = System.Drawing.Color.Transparent;
            this.chkHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.chkHn.Location = new System.Drawing.Point(3, 8);
            this.chkHn.Name = "chkHn";
            this.chkHn.Size = new System.Drawing.Size(46, 20);
            this.chkHn.TabIndex = 0;
            this.chkHn.TabStop = true;
            this.chkHn.Text = "HN";
            this.theme1.SetTheme(this.chkHn, "(default)");
            this.chkHn.UseVisualStyleBackColor = false;
            // 
            // btnOk
            // 
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(782, 7);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(26, 24);
            this.btnOk.TabIndex = 543;
            this.btnOk.Text = "...";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnOk, "(default)");
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.BackColor = System.Drawing.Color.White;
            this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.lbName.Location = new System.Drawing.Point(12, 41);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(21, 20);
            this.lbName.TabIndex = 542;
            this.lbName.Text = "...";
            this.theme1.SetTheme(this.lbName, "(default)");
            // 
            // FrmNurseScanView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmNurseScanView";
            this.Text = "FrmNurseScanView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmNurseScanView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDateEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController theme1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private C1.Win.C1Input.C1DateEdit txtDateEnd;
        private C1.Win.C1Input.C1Label c1Label4;
        private C1.Win.C1Input.C1DateEdit txtDateStart;
        private C1.Win.C1Input.C1Label c1Label3;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Input.C1TextBox txtHn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton chkDoctor;
        private System.Windows.Forms.RadioButton chkHn;
        private C1.Win.C1Input.C1Button btnOk;
        private System.Windows.Forms.Label lbName;
    }
}