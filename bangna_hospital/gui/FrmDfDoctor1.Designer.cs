namespace bangna_hospital.gui
{
    partial class FrmDfDoctor1
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
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.tCMain = new C1.Win.C1Command.C1DockingTab();
            this.tabImportDf = new C1.Win.C1Command.C1DockingTabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnImportDfGen = new C1.Win.C1Input.C1Button();
            this.txtImpPaidType = new C1.Win.C1Input.C1TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnImportDfSelect = new C1.Win.C1Input.C1Button();
            this.txtImpDateEnd = new C1.Win.Calendar.C1DateEdit();
            this.txtImpDateStart = new C1.Win.Calendar.C1DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.c1DockingTabPage2 = new C1.Win.C1Command.C1DockingTabPage();
            this.spReport = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCMain)).BeginInit();
            this.tCMain.SuspendLayout();
            this.tabImportDf.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnImportDfGen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImpPaidType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImportDfSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImpDateEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImpDateStart)).BeginInit();
            this.c1DockingTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spReport)).BeginInit();
            this.spReport.SuspendLayout();
            this.c1SplitterPanel1.SuspendLayout();
            this.c1SplitterPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 608);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.Size = new System.Drawing.Size(1035, 22);
            // 
            // tCMain
            // 
            this.tCMain.Controls.Add(this.tabImportDf);
            this.tCMain.Controls.Add(this.c1DockingTabPage2);
            this.tCMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tCMain.Location = new System.Drawing.Point(0, 0);
            this.tCMain.Name = "tCMain";
            this.tCMain.Size = new System.Drawing.Size(1035, 608);
            this.tCMain.TabIndex = 1;
            this.tCMain.TabsSpacing = 5;
            // 
            // tabImportDf
            // 
            this.tabImportDf.Controls.Add(this.panel2);
            this.tabImportDf.Controls.Add(this.panel1);
            this.tabImportDf.Location = new System.Drawing.Point(1, 24);
            this.tabImportDf.Name = "tabImportDf";
            this.tabImportDf.Size = new System.Drawing.Size(1033, 583);
            this.tabImportDf.TabIndex = 0;
            this.tabImportDf.Text = "Import Item DF";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1033, 533);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnImportDfGen);
            this.panel1.Controls.Add(this.txtImpPaidType);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnImportDfSelect);
            this.panel1.Controls.Add(this.txtImpDateEnd);
            this.panel1.Controls.Add(this.txtImpDateStart);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1033, 50);
            this.panel1.TabIndex = 0;
            // 
            // btnImportDfGen
            // 
            this.btnImportDfGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnImportDfGen.Location = new System.Drawing.Point(795, 3);
            this.btnImportDfGen.Name = "btnImportDfGen";
            this.btnImportDfGen.Size = new System.Drawing.Size(89, 33);
            this.btnImportDfGen.TabIndex = 276;
            this.btnImportDfGen.Text = "2. gen Text";
            this.btnImportDfGen.UseVisualStyleBackColor = true;
            // 
            // txtImpPaidType
            // 
            this.txtImpPaidType.Location = new System.Drawing.Point(532, 5);
            this.txtImpPaidType.Name = "txtImpPaidType";
            this.txtImpPaidType.Size = new System.Drawing.Size(154, 24);
            this.txtImpPaidType.TabIndex = 275;
            this.txtImpPaidType.Tag = null;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(441, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 274;
            this.label3.Text = "สิทธิ (xx,...) :";
            // 
            // btnImportDfSelect
            // 
            this.btnImportDfSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnImportDfSelect.Location = new System.Drawing.Point(700, 3);
            this.btnImportDfSelect.Name = "btnImportDfSelect";
            this.btnImportDfSelect.Size = new System.Drawing.Size(89, 33);
            this.btnImportDfSelect.TabIndex = 273;
            this.btnImportDfSelect.Text = "1. ดึงข้อมูล";
            this.btnImportDfSelect.UseVisualStyleBackColor = true;
            // 
            // txtImpDateEnd
            // 
            this.txtImpDateEnd.AllowSpinLoop = false;
            this.txtImpDateEnd.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.txtImpDateEnd.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtImpDateEnd.CurrentTimeZone = false;
            this.txtImpDateEnd.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            this.txtImpDateEnd.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtImpDateEnd.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtImpDateEnd.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtImpDateEnd.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtImpDateEnd.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtImpDateEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtImpDateEnd.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtImpDateEnd.GMTOffset = System.TimeSpan.Parse("07:00:00");
            this.txtImpDateEnd.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtImpDateEnd.Location = new System.Drawing.Point(311, 3);
            this.txtImpDateEnd.Name = "txtImpDateEnd";
            this.txtImpDateEnd.Size = new System.Drawing.Size(128, 27);
            this.txtImpDateEnd.TabIndex = 88;
            this.txtImpDateEnd.Tag = null;
            // 
            // txtImpDateStart
            // 
            this.txtImpDateStart.AllowSpinLoop = false;
            this.txtImpDateStart.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.txtImpDateStart.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtImpDateStart.CurrentTimeZone = false;
            this.txtImpDateStart.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            this.txtImpDateStart.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtImpDateStart.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtImpDateStart.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtImpDateStart.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtImpDateStart.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtImpDateStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtImpDateStart.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtImpDateStart.GMTOffset = System.TimeSpan.Parse("07:00:00");
            this.txtImpDateStart.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtImpDateStart.Location = new System.Drawing.Point(101, 3);
            this.txtImpDateStart.Name = "txtImpDateStart";
            this.txtImpDateStart.Size = new System.Drawing.Size(128, 27);
            this.txtImpDateStart.TabIndex = 87;
            this.txtImpDateStart.Tag = null;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "วันที่สิ้นสุด :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "วันที่เริ่มต้น :";
            // 
            // c1DockingTabPage2
            // 
            this.c1DockingTabPage2.Controls.Add(this.spReport);
            this.c1DockingTabPage2.Location = new System.Drawing.Point(1, 24);
            this.c1DockingTabPage2.Name = "c1DockingTabPage2";
            this.c1DockingTabPage2.Size = new System.Drawing.Size(1033, 583);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Page2";
            // 
            // spReport
            // 
            this.spReport.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.spReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.spReport.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.spReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.spReport.Location = new System.Drawing.Point(0, 0);
            this.spReport.Name = "spReport";
            this.spReport.Panels.Add(this.c1SplitterPanel1);
            this.spReport.Panels.Add(this.c1SplitterPanel2);
            this.spReport.Size = new System.Drawing.Size(1033, 583);
            this.spReport.TabIndex = 0;
            // 
            // c1SplitterPanel1
            // 
            this.c1SplitterPanel1.Collapsible = true;
            this.c1SplitterPanel1.Controls.Add(this.panel3);
            this.c1SplitterPanel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.c1SplitterPanel1.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1SplitterPanel1.Location = new System.Drawing.Point(0, 21);
            this.c1SplitterPanel1.Name = "c1SplitterPanel1";
            this.c1SplitterPanel1.Size = new System.Drawing.Size(371, 562);
            this.c1SplitterPanel1.SizeRatio = 36.735D;
            this.c1SplitterPanel1.TabIndex = 0;
            this.c1SplitterPanel1.Text = "Panel 1";
            this.c1SplitterPanel1.Width = 371;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(371, 562);
            this.panel3.TabIndex = 0;
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.Controls.Add(this.panel4);
            this.c1SplitterPanel2.Height = 583;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(382, 21);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(651, 562);
            this.c1SplitterPanel2.TabIndex = 1;
            this.c1SplitterPanel2.Text = "Panel 2";
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(651, 562);
            this.panel4.TabIndex = 0;
            // 
            // FrmDfDoctor1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 630);
            this.Controls.Add(this.tCMain);
            this.Controls.Add(this.c1StatusBar1);
            this.Name = "FrmDfDoctor1";
            this.Text = "FrmDfDoctor1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmDfDoctor1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCMain)).EndInit();
            this.tCMain.ResumeLayout(false);
            this.tabImportDf.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnImportDfGen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImpPaidType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImportDfSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImpDateEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImpDateStart)).EndInit();
            this.c1DockingTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spReport)).EndInit();
            this.spReport.ResumeLayout(false);
            this.c1SplitterPanel1.ResumeLayout(false);
            this.c1SplitterPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Command.C1DockingTab tCMain;
        private C1.Win.C1Command.C1DockingTabPage tabImportDf;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private C1.Win.Calendar.C1DateEdit txtImpDateEnd;
        private C1.Win.Calendar.C1DateEdit txtImpDateStart;
        private C1.Win.C1Input.C1Button btnImportDfSelect;
        private System.Windows.Forms.Label label3;
        private C1.Win.C1Input.C1TextBox txtImpPaidType;
        private C1.Win.C1Input.C1Button btnImportDfGen;
        private C1.Win.C1SplitContainer.C1SplitContainer spReport;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}