
namespace bangna_hospital.gui
{
    partial class FrmImportMDB
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
            this.c1SplitContainer1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.chkDeleteAll = new C1.Win.C1Input.C1CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPaidType = new C1.Win.C1Input.C1TextBox();
            this.btnMDBOk = new C1.Win.C1Input.C1Button();
            this.txtMDBdateend = new C1.Win.C1Input.C1DateEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMDBdatestart = new C1.Win.C1Input.C1DateEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.btnMDBimport = new C1.Win.C1Input.C1Button();
            this.cboMDBhostname = new C1.Win.C1Input.C1ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMDBbrow = new C1.Win.C1Input.C1Button();
            this.txtMDBpathfile = new C1.Win.C1Input.C1TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMDBappend = new System.Windows.Forms.RadioButton();
            this.chkMDBloadNew = new System.Windows.Forms.RadioButton();
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.lsbStatus = new C1.Win.C1Ribbon.RibbonLabel();
            this.lsbMessage = new C1.Win.C1Ribbon.RibbonLabel();
            this.lsbMessage1 = new C1.Win.C1Ribbon.RibbonLabel();
            this.lsbMessage2 = new C1.Win.C1Ribbon.RibbonLabel();
            this.rsbStatus = new C1.Win.C1Ribbon.RibbonLabel();
            this.rsbPb = new C1.Win.C1Ribbon.RibbonProgressBar();
            this.tC = new C1.Win.C1Command.C1DockingTab();
            this.tabMDB = new C1.Win.C1Command.C1DockingTabPage();
            this.btnChk = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).BeginInit();
            this.c1SplitContainer1.SuspendLayout();
            this.c1SplitterPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDeleteAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaidType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMDBOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMDBdateend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMDBdatestart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMDBimport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMDBhostname)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMDBbrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMDBpathfile)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tC)).BeginInit();
            this.tC.SuspendLayout();
            this.tabMDB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnChk)).BeginInit();
            this.SuspendLayout();
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
            this.c1SplitContainer1.Size = new System.Drawing.Size(1087, 403);
            this.c1SplitContainer1.TabIndex = 0;
            // 
            // c1SplitterPanel1
            // 
            this.c1SplitterPanel1.Collapsible = true;
            this.c1SplitterPanel1.Controls.Add(this.btnChk);
            this.c1SplitterPanel1.Controls.Add(this.chkDeleteAll);
            this.c1SplitterPanel1.Controls.Add(this.label5);
            this.c1SplitterPanel1.Controls.Add(this.txtPaidType);
            this.c1SplitterPanel1.Controls.Add(this.btnMDBOk);
            this.c1SplitterPanel1.Controls.Add(this.txtMDBdateend);
            this.c1SplitterPanel1.Controls.Add(this.label4);
            this.c1SplitterPanel1.Controls.Add(this.txtMDBdatestart);
            this.c1SplitterPanel1.Controls.Add(this.label3);
            this.c1SplitterPanel1.Controls.Add(this.btnMDBimport);
            this.c1SplitterPanel1.Controls.Add(this.cboMDBhostname);
            this.c1SplitterPanel1.Controls.Add(this.label2);
            this.c1SplitterPanel1.Controls.Add(this.btnMDBbrow);
            this.c1SplitterPanel1.Controls.Add(this.txtMDBpathfile);
            this.c1SplitterPanel1.Controls.Add(this.label1);
            this.c1SplitterPanel1.Controls.Add(this.groupBox1);
            this.c1SplitterPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.c1SplitterPanel1.Height = 102;
            this.c1SplitterPanel1.Location = new System.Drawing.Point(0, 21);
            this.c1SplitterPanel1.Name = "c1SplitterPanel1";
            this.c1SplitterPanel1.Size = new System.Drawing.Size(1087, 74);
            this.c1SplitterPanel1.SizeRatio = 25.564D;
            this.c1SplitterPanel1.TabIndex = 0;
            this.c1SplitterPanel1.Text = "Panel 1";
            // 
            // chkDeleteAll
            // 
            this.chkDeleteAll.BackColor = System.Drawing.SystemColors.Control;
            this.chkDeleteAll.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkDeleteAll.Checked = true;
            this.chkDeleteAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeleteAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkDeleteAll.Location = new System.Drawing.Point(549, 39);
            this.chkDeleteAll.Name = "chkDeleteAll";
            this.chkDeleteAll.Size = new System.Drawing.Size(96, 24);
            this.chkDeleteAll.TabIndex = 14;
            this.chkDeleteAll.Text = "ลบข้อมูล";
            this.chkDeleteAll.UseVisualStyleBackColor = true;
            this.chkDeleteAll.Value = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(817, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "สิทธิ";
            // 
            // txtPaidType
            // 
            this.txtPaidType.Location = new System.Drawing.Point(858, 7);
            this.txtPaidType.Name = "txtPaidType";
            this.txtPaidType.Size = new System.Drawing.Size(150, 24);
            this.txtPaidType.TabIndex = 12;
            this.txtPaidType.Tag = null;
            // 
            // btnMDBOk
            // 
            this.btnMDBOk.Location = new System.Drawing.Point(1014, 3);
            this.btnMDBOk.Name = "btnMDBOk";
            this.btnMDBOk.Size = new System.Drawing.Size(70, 34);
            this.btnMDBOk.TabIndex = 11;
            this.btnMDBOk.Text = "ดึงข้อมูล";
            this.btnMDBOk.UseVisualStyleBackColor = true;
            // 
            // txtMDBdateend
            // 
            this.txtMDBdateend.AllowSpinLoop = false;
            // 
            // 
            // 
            this.txtMDBdateend.Calendar.DayNameLength = 1;
            this.txtMDBdateend.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtMDBdateend.CurrentTimeZone = false;
            this.txtMDBdateend.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            this.txtMDBdateend.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtMDBdateend.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtMDBdateend.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtMDBdateend.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtMDBdateend.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtMDBdateend.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtMDBdateend.GMTOffset = System.TimeSpan.Parse("00:00:00");
            this.txtMDBdateend.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtMDBdateend.Location = new System.Drawing.Point(951, 39);
            this.txtMDBdateend.Name = "txtMDBdateend";
            this.txtMDBdateend.Size = new System.Drawing.Size(124, 24);
            this.txtMDBdateend.TabIndex = 10;
            this.txtMDBdateend.Tag = null;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(896, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "ถึงวันที่";
            // 
            // txtMDBdatestart
            // 
            this.txtMDBdatestart.AllowSpinLoop = false;
            // 
            // 
            // 
            this.txtMDBdatestart.Calendar.DayNameLength = 1;
            this.txtMDBdatestart.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtMDBdatestart.CurrentTimeZone = false;
            this.txtMDBdatestart.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            this.txtMDBdatestart.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtMDBdatestart.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtMDBdatestart.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtMDBdatestart.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtMDBdatestart.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtMDBdatestart.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtMDBdatestart.GMTOffset = System.TimeSpan.Parse("00:00:00");
            this.txtMDBdatestart.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtMDBdatestart.Location = new System.Drawing.Point(768, 39);
            this.txtMDBdatestart.Name = "txtMDBdatestart";
            this.txtMDBdatestart.Size = new System.Drawing.Size(124, 24);
            this.txtMDBdatestart.TabIndex = 8;
            this.txtMDBdatestart.Tag = null;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(727, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "วันที่";
            // 
            // btnMDBimport
            // 
            this.btnMDBimport.Location = new System.Drawing.Point(651, 37);
            this.btnMDBimport.Name = "btnMDBimport";
            this.btnMDBimport.Size = new System.Drawing.Size(70, 34);
            this.btnMDBimport.TabIndex = 6;
            this.btnMDBimport.Text = "Start";
            this.btnMDBimport.UseVisualStyleBackColor = true;
            // 
            // cboMDBhostname
            // 
            this.cboMDBhostname.AllowSpinLoop = false;
            this.cboMDBhostname.GapHeight = 0;
            this.cboMDBhostname.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboMDBhostname.Items.Add("bangna1");
            this.cboMDBhostname.Items.Add("bangna2");
            this.cboMDBhostname.Items.Add("bangna5");
            this.cboMDBhostname.ItemsDisplayMember = "";
            this.cboMDBhostname.ItemsValueMember = "";
            this.cboMDBhostname.Location = new System.Drawing.Point(342, 37);
            this.cboMDBhostname.Name = "cboMDBhostname";
            this.cboMDBhostname.Size = new System.Drawing.Size(200, 24);
            this.cboMDBhostname.TabIndex = 5;
            this.cboMDBhostname.Tag = null;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "hostname";
            // 
            // btnMDBbrow
            // 
            this.btnMDBbrow.Location = new System.Drawing.Point(756, 7);
            this.btnMDBbrow.Name = "btnMDBbrow";
            this.btnMDBbrow.Size = new System.Drawing.Size(39, 24);
            this.btnMDBbrow.TabIndex = 3;
            this.btnMDBbrow.Text = "...";
            this.btnMDBbrow.UseVisualStyleBackColor = true;
            // 
            // txtMDBpathfile
            // 
            this.txtMDBpathfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtMDBpathfile.Location = new System.Drawing.Point(342, 7);
            this.txtMDBpathfile.Name = "txtMDBpathfile";
            this.txtMDBpathfile.Size = new System.Drawing.Size(408, 20);
            this.txtMDBpathfile.TabIndex = 2;
            this.txtMDBpathfile.Tag = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(254, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "path file";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMDBappend);
            this.groupBox1.Controls.Add(this.chkMDBloadNew);
            this.groupBox1.Location = new System.Drawing.Point(8, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(171, 68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ประเภทการโหลด";
            // 
            // chkMDBappend
            // 
            this.chkMDBappend.AutoSize = true;
            this.chkMDBappend.Location = new System.Drawing.Point(8, 42);
            this.chkMDBappend.Name = "chkMDBappend";
            this.chkMDBappend.Size = new System.Drawing.Size(159, 24);
            this.chkMDBappend.TabIndex = 1;
            this.chkMDBappend.TabStop = true;
            this.chkMDBappend.Text = "โหลดเฉพาะส่วนที่ขาด";
            this.chkMDBappend.UseVisualStyleBackColor = true;
            // 
            // chkMDBloadNew
            // 
            this.chkMDBloadNew.AutoSize = true;
            this.chkMDBloadNew.Location = new System.Drawing.Point(8, 19);
            this.chkMDBloadNew.Name = "chkMDBloadNew";
            this.chkMDBloadNew.Size = new System.Drawing.Size(131, 24);
            this.chkMDBloadNew.TabIndex = 0;
            this.chkMDBloadNew.TabStop = true;
            this.chkMDBloadNew.Text = "โหลดใหม่ทั้งหมด";
            this.chkMDBloadNew.UseVisualStyleBackColor = true;
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.Height = 297;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(0, 127);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(1087, 276);
            this.c1SplitterPanel2.TabIndex = 1;
            this.c1SplitterPanel2.Text = "Panel 2";
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1StatusBar1.LeftPaneItems.Add(this.lsbStatus);
            this.c1StatusBar1.LeftPaneItems.Add(this.lsbMessage);
            this.c1StatusBar1.LeftPaneItems.Add(this.lsbMessage1);
            this.c1StatusBar1.LeftPaneItems.Add(this.lsbMessage2);
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 428);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.rsbStatus);
            this.c1StatusBar1.RightPaneItems.Add(this.rsbPb);
            this.c1StatusBar1.Size = new System.Drawing.Size(1089, 22);
            // 
            // lsbStatus
            // 
            this.lsbStatus.Name = "lsbStatus";
            this.lsbStatus.Text = "Label";
            // 
            // lsbMessage
            // 
            this.lsbMessage.Name = "lsbMessage";
            this.lsbMessage.Text = "Label";
            // 
            // lsbMessage1
            // 
            this.lsbMessage1.Name = "lsbMessage1";
            this.lsbMessage1.Text = "Label";
            // 
            // lsbMessage2
            // 
            this.lsbMessage2.Name = "lsbMessage2";
            this.lsbMessage2.Text = "Label";
            // 
            // rsbStatus
            // 
            this.rsbStatus.Name = "rsbStatus";
            // 
            // rsbPb
            // 
            this.rsbPb.Name = "rsbPb";
            this.rsbPb.Width = 200;
            // 
            // tC
            // 
            this.tC.Controls.Add(this.tabMDB);
            this.tC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tC.Location = new System.Drawing.Point(0, 0);
            this.tC.Name = "tC";
            this.tC.Size = new System.Drawing.Size(1089, 428);
            this.tC.TabIndex = 2;
            this.tC.TabsSpacing = 5;
            // 
            // tabMDB
            // 
            this.tabMDB.Controls.Add(this.c1SplitContainer1);
            this.tabMDB.Location = new System.Drawing.Point(1, 24);
            this.tabMDB.Name = "tabMDB";
            this.tabMDB.Size = new System.Drawing.Size(1087, 403);
            this.tabMDB.TabIndex = 0;
            this.tabMDB.Text = "Import to MDB";
            // 
            // btnChk
            // 
            this.btnChk.Location = new System.Drawing.Point(185, 7);
            this.btnChk.Name = "btnChk";
            this.btnChk.Size = new System.Drawing.Size(63, 34);
            this.btnChk.TabIndex = 15;
            this.btnChk.Text = "check";
            this.btnChk.UseVisualStyleBackColor = true;
            // 
            // FrmImportMDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 450);
            this.Controls.Add(this.tC);
            this.Controls.Add(this.c1StatusBar1);
            this.Name = "FrmImportMDB";
            this.Text = "FrmImportMDB";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmImportMDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).EndInit();
            this.c1SplitContainer1.ResumeLayout(false);
            this.c1SplitterPanel1.ResumeLayout(false);
            this.c1SplitterPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDeleteAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaidType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMDBOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMDBdateend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMDBdatestart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMDBimport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMDBhostname)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMDBbrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMDBpathfile)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tC)).EndInit();
            this.tC.ResumeLayout(false);
            this.tabMDB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnChk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel1;
        private C1.Win.C1Input.C1Button btnMDBbrow;
        private C1.Win.C1Input.C1TextBox txtMDBpathfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton chkMDBappend;
        private System.Windows.Forms.RadioButton chkMDBloadNew;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private C1.Win.C1Input.C1ComboBox cboMDBhostname;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1Button btnMDBimport;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Command.C1DockingTab tC;
        private C1.Win.C1Command.C1DockingTabPage tabMDB;
        private C1.Win.C1Input.C1DateEdit txtMDBdateend;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Input.C1DateEdit txtMDBdatestart;
        private System.Windows.Forms.Label label3;
        private C1.Win.C1Input.C1Button btnMDBOk;
        private C1.Win.C1Ribbon.RibbonLabel lsbStatus;
        private C1.Win.C1Ribbon.RibbonLabel lsbMessage;
        private C1.Win.C1Ribbon.RibbonLabel lsbMessage1;
        private C1.Win.C1Ribbon.RibbonLabel rsbStatus;
        private C1.Win.C1Ribbon.RibbonProgressBar rsbPb;
        private C1.Win.C1Input.C1TextBox txtPaidType;
        private System.Windows.Forms.Label label5;
        private C1.Win.C1Input.C1CheckBox chkDeleteAll;
        private C1.Win.C1Ribbon.RibbonLabel lsbMessage2;
        private C1.Win.C1Input.C1Button btnChk;
    }
}