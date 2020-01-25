namespace bangna_hospital.gui
{
    partial class FrmPrintCri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintCri));
            this.theme1 = new C1.Win.C1Themes.C1ThemeController();
            this.sB1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.tC1 = new C1.Win.C1Command.C1DockingTab();
            this.tabPrn1 = new C1.Win.C1Command.C1DockingTabPage();
            this.sC1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.pnHn = new System.Windows.Forms.Panel();
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnPrepare = new C1.Win.C1Input.C1Button();
            this.chkResXray = new C1.Win.C1Input.C1CheckBox();
            this.chkResLab = new C1.Win.C1Input.C1CheckBox();
            this.chkReqXray = new C1.Win.C1Input.C1CheckBox();
            this.btnSearch = new C1.Win.C1Input.C1Button();
            this.txtDateEnd = new C1.Win.C1Input.C1DateEdit();
            this.txtDateStart = new C1.Win.C1Input.C1DateEdit();
            this.c1Label4 = new C1.Win.C1Input.C1Label();
            this.c1Label3 = new C1.Win.C1Input.C1Label();
            this.c1Label2 = new C1.Win.C1Input.C1Label();
            this.chkReqLab = new C1.Win.C1Input.C1CheckBox();
            this.chkStaffNote = new C1.Win.C1Input.C1CheckBox();
            this.chkDrug = new C1.Win.C1Input.C1CheckBox();
            this.cboCriteria = new C1.Win.C1List.C1Combo();
            this.btnSel = new C1.Win.C1Input.C1Button();
            this.c1Label1 = new C1.Win.C1Input.C1Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.tabPrn2 = new C1.Win.C1Command.C1DockingTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sB1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tC1)).BeginInit();
            this.tC1.SuspendLayout();
            this.tabPrn1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sC1)).BeginInit();
            this.sC1.SuspendLayout();
            this.c1SplitterPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPrepare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkResXray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkResLab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReqXray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReqLab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStaffNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDrug)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCriteria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            this.SuspendLayout();
            // 
            // sB1
            // 
            this.sB1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.sB1.Location = new System.Drawing.Point(0, 727);
            this.sB1.Name = "sB1";
            this.sB1.Size = new System.Drawing.Size(1114, 22);
            this.theme1.SetTheme(this.sB1, "(default)");
            this.sB1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Custom;
            // 
            // tC1
            // 
            this.tC1.BackColor = System.Drawing.Color.White;
            this.tC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tC1.Controls.Add(this.tabPrn1);
            this.tC1.Controls.Add(this.tabPrn2);
            this.tC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tC1.HotTrack = true;
            this.tC1.Location = new System.Drawing.Point(0, 0);
            this.tC1.Name = "tC1";
            this.tC1.SelectedIndex = 1;
            this.tC1.Size = new System.Drawing.Size(1114, 727);
            this.tC1.TabIndex = 1;
            this.tC1.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.tC1.TabsShowFocusCues = false;
            this.tC1.TabsSpacing = 2;
            this.tC1.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007;
            this.theme1.SetTheme(this.tC1, "(default)");
            // 
            // tabPrn1
            // 
            this.tabPrn1.Controls.Add(this.sC1);
            this.tabPrn1.Controls.Add(this.panel1);
            this.tabPrn1.Location = new System.Drawing.Point(1, 24);
            this.tabPrn1.Name = "tabPrn1";
            this.tabPrn1.Size = new System.Drawing.Size(1112, 702);
            this.tabPrn1.TabIndex = 0;
            this.tabPrn1.Text = "Criteria Print 1";
            // 
            // sC1
            // 
            this.sC1.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.sC1.BackColor = System.Drawing.Color.White;
            this.sC1.CollapsingAreaColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.sC1.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.sC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sC1.FixedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.sC1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.sC1.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.sC1.HeaderLineWidth = 1;
            this.sC1.Location = new System.Drawing.Point(0, 188);
            this.sC1.Name = "sC1";
            this.sC1.Panels.Add(this.c1SplitterPanel1);
            this.sC1.Panels.Add(this.c1SplitterPanel2);
            this.sC1.Size = new System.Drawing.Size(1112, 514);
            this.sC1.SplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.sC1.SplitterMovingColor = System.Drawing.Color.Black;
            this.sC1.TabIndex = 1;
            this.theme1.SetTheme(this.sC1, "(default)");
            this.sC1.UseParentVisualStyle = false;
            // 
            // c1SplitterPanel1
            // 
            this.c1SplitterPanel1.Collapsible = true;
            this.c1SplitterPanel1.Controls.Add(this.pnHn);
            this.c1SplitterPanel1.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1SplitterPanel1.Location = new System.Drawing.Point(0, 21);
            this.c1SplitterPanel1.Name = "c1SplitterPanel1";
            this.c1SplitterPanel1.Size = new System.Drawing.Size(547, 493);
            this.c1SplitterPanel1.TabIndex = 0;
            this.c1SplitterPanel1.Text = "Panel 1";
            this.c1SplitterPanel1.Width = 554;
            // 
            // pnHn
            // 
            this.pnHn.BackColor = System.Drawing.Color.White;
            this.pnHn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnHn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.pnHn.Location = new System.Drawing.Point(0, 0);
            this.pnHn.Name = "pnHn";
            this.pnHn.Size = new System.Drawing.Size(547, 493);
            this.pnHn.TabIndex = 0;
            this.theme1.SetTheme(this.pnHn, "(default)");
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.Height = 514;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(558, 21);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(554, 493);
            this.c1SplitterPanel2.TabIndex = 1;
            this.c1SplitterPanel2.Text = "Panel 2";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.BtnPrepare);
            this.panel1.Controls.Add(this.chkResXray);
            this.panel1.Controls.Add(this.chkResLab);
            this.panel1.Controls.Add(this.chkReqXray);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtDateEnd);
            this.panel1.Controls.Add(this.txtDateStart);
            this.panel1.Controls.Add(this.c1Label4);
            this.panel1.Controls.Add(this.c1Label3);
            this.panel1.Controls.Add(this.c1Label2);
            this.panel1.Controls.Add(this.chkReqLab);
            this.panel1.Controls.Add(this.chkStaffNote);
            this.panel1.Controls.Add(this.chkDrug);
            this.panel1.Controls.Add(this.cboCriteria);
            this.panel1.Controls.Add(this.btnSel);
            this.panel1.Controls.Add(this.c1Label1);
            this.panel1.Controls.Add(this.txtHn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1112, 188);
            this.panel1.TabIndex = 0;
            this.theme1.SetTheme(this.panel1, "(default)");
            // 
            // BtnPrepare
            // 
            this.BtnPrepare.Image = global::bangna_hospital.Properties.Resources.database48;
            this.BtnPrepare.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnPrepare.Location = new System.Drawing.Point(806, 106);
            this.BtnPrepare.Name = "BtnPrepare";
            this.BtnPrepare.Size = new System.Drawing.Size(110, 63);
            this.BtnPrepare.TabIndex = 16;
            this.BtnPrepare.Text = "Prepare";
            this.BtnPrepare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.BtnPrepare, "(default)");
            this.BtnPrepare.UseVisualStyleBackColor = true;
            this.BtnPrepare.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkResXray
            // 
            this.chkResXray.BackColor = System.Drawing.Color.Transparent;
            this.chkResXray.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.chkResXray.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkResXray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkResXray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.chkResXray.Location = new System.Drawing.Point(262, 150);
            this.chkResXray.Name = "chkResXray";
            this.chkResXray.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.chkResXray.Size = new System.Drawing.Size(104, 24);
            this.chkResXray.TabIndex = 15;
            this.chkResXray.Text = "ใบผล X-ray";
            this.theme1.SetTheme(this.chkResXray, "(default)");
            this.chkResXray.UseVisualStyleBackColor = true;
            this.chkResXray.Value = null;
            this.chkResXray.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkResLab
            // 
            this.chkResLab.BackColor = System.Drawing.Color.Transparent;
            this.chkResLab.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.chkResLab.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkResLab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkResLab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.chkResLab.Location = new System.Drawing.Point(262, 124);
            this.chkResLab.Name = "chkResLab";
            this.chkResLab.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.chkResLab.Size = new System.Drawing.Size(104, 24);
            this.chkResLab.TabIndex = 14;
            this.chkResLab.Text = "ใบผล Lab";
            this.theme1.SetTheme(this.chkResLab, "(default)");
            this.chkResLab.UseVisualStyleBackColor = true;
            this.chkResLab.Value = null;
            this.chkResLab.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkReqXray
            // 
            this.chkReqXray.BackColor = System.Drawing.Color.Transparent;
            this.chkReqXray.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.chkReqXray.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkReqXray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkReqXray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.chkReqXray.Location = new System.Drawing.Point(132, 150);
            this.chkReqXray.Name = "chkReqXray";
            this.chkReqXray.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.chkReqXray.Size = new System.Drawing.Size(104, 24);
            this.chkReqXray.TabIndex = 13;
            this.chkReqXray.Text = "ใบReuest X-ray";
            this.theme1.SetTheme(this.chkReqXray, "(default)");
            this.chkReqXray.UseVisualStyleBackColor = true;
            this.chkReqXray.Value = null;
            this.chkReqXray.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnSearch
            // 
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(609, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(30, 29);
            this.btnSearch.TabIndex = 12;
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
            this.txtDateEnd.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
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
            this.txtDateEnd.Location = new System.Drawing.Point(511, 143);
            this.txtDateEnd.Name = "txtDateEnd";
            this.txtDateEnd.Size = new System.Drawing.Size(134, 20);
            this.txtDateEnd.TabIndex = 11;
            this.txtDateEnd.Tag = null;
            this.theme1.SetTheme(this.txtDateEnd, "(default)");
            this.txtDateEnd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
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
            this.txtDateStart.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
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
            this.txtDateStart.GMTOffset = System.TimeSpan.Parse("00:00:00");
            this.txtDateStart.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtDateStart.Location = new System.Drawing.Point(511, 122);
            this.txtDateStart.Name = "txtDateStart";
            this.txtDateStart.Size = new System.Drawing.Size(134, 18);
            this.txtDateStart.TabIndex = 10;
            this.txtDateStart.Tag = null;
            this.theme1.SetTheme(this.txtDateStart, "(default)");
            this.txtDateStart.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // c1Label4
            // 
            this.c1Label4.AutoSize = true;
            this.c1Label4.BackColor = System.Drawing.Color.Transparent;
            this.c1Label4.BorderColor = System.Drawing.Color.Transparent;
            this.c1Label4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.c1Label4.Location = new System.Drawing.Point(412, 143);
            this.c1Label4.Name = "c1Label4";
            this.c1Label4.Size = new System.Drawing.Size(51, 13);
            this.c1Label4.TabIndex = 9;
            this.c1Label4.Tag = null;
            this.theme1.SetTheme(this.c1Label4, "(default)");
            this.c1Label4.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom;
            // 
            // c1Label3
            // 
            this.c1Label3.AutoSize = true;
            this.c1Label3.BackColor = System.Drawing.Color.Transparent;
            this.c1Label3.BorderColor = System.Drawing.Color.Transparent;
            this.c1Label3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.c1Label3.Location = new System.Drawing.Point(412, 122);
            this.c1Label3.Name = "c1Label3";
            this.c1Label3.Size = new System.Drawing.Size(51, 13);
            this.c1Label3.TabIndex = 8;
            this.c1Label3.Tag = null;
            this.theme1.SetTheme(this.c1Label3, "(default)");
            this.c1Label3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom;
            // 
            // c1Label2
            // 
            this.c1Label2.AutoSize = true;
            this.c1Label2.BackColor = System.Drawing.Color.Transparent;
            this.c1Label2.BorderColor = System.Drawing.Color.Transparent;
            this.c1Label2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.c1Label2.Location = new System.Drawing.Point(11, 104);
            this.c1Label2.Name = "c1Label2";
            this.c1Label2.Size = new System.Drawing.Size(51, 13);
            this.c1Label2.TabIndex = 7;
            this.c1Label2.Tag = null;
            this.theme1.SetTheme(this.c1Label2, "(default)");
            this.c1Label2.Value = "Critetia :";
            this.c1Label2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom;
            // 
            // chkReqLab
            // 
            this.chkReqLab.BackColor = System.Drawing.Color.Transparent;
            this.chkReqLab.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.chkReqLab.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkReqLab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkReqLab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.chkReqLab.Location = new System.Drawing.Point(132, 126);
            this.chkReqLab.Name = "chkReqLab";
            this.chkReqLab.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.chkReqLab.Size = new System.Drawing.Size(104, 24);
            this.chkReqLab.TabIndex = 6;
            this.chkReqLab.Text = "ใบReuest Lab";
            this.theme1.SetTheme(this.chkReqLab, "(default)");
            this.chkReqLab.UseVisualStyleBackColor = true;
            this.chkReqLab.Value = null;
            this.chkReqLab.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkStaffNote
            // 
            this.chkStaffNote.BackColor = System.Drawing.Color.Transparent;
            this.chkStaffNote.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.chkStaffNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkStaffNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkStaffNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.chkStaffNote.Location = new System.Drawing.Point(13, 150);
            this.chkStaffNote.Name = "chkStaffNote";
            this.chkStaffNote.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.chkStaffNote.Size = new System.Drawing.Size(104, 24);
            this.chkStaffNote.TabIndex = 5;
            this.chkStaffNote.Text = "พิมพ์ Staff Note";
            this.theme1.SetTheme(this.chkStaffNote, "(default)");
            this.chkStaffNote.UseVisualStyleBackColor = true;
            this.chkStaffNote.Value = null;
            this.chkStaffNote.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkDrug
            // 
            this.chkDrug.BackColor = System.Drawing.Color.Transparent;
            this.chkDrug.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.chkDrug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkDrug.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.chkDrug.Location = new System.Drawing.Point(13, 126);
            this.chkDrug.Name = "chkDrug";
            this.chkDrug.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.chkDrug.Size = new System.Drawing.Size(104, 24);
            this.chkDrug.TabIndex = 4;
            this.chkDrug.Text = "พิมพ์ ใบยา";
            this.theme1.SetTheme(this.chkDrug, "(default)");
            this.chkDrug.UseVisualStyleBackColor = true;
            this.chkDrug.Value = null;
            this.chkDrug.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // cboCriteria
            // 
            this.cboCriteria.AddItemSeparator = ';';
            this.cboCriteria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboCriteria.Caption = "";
            this.cboCriteria.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCriteria.DeadAreaBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cboCriteria.EditorBackColor = System.Drawing.Color.White;
            this.cboCriteria.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCriteria.EditorForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.cboCriteria.FlatStyle = C1.Win.C1List.FlatModeEnum.Flat;
            this.cboCriteria.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCriteria.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCriteria.Images"))));
            this.cboCriteria.Location = new System.Drawing.Point(107, 99);
            this.cboCriteria.MatchEntryTimeout = ((long)(2000));
            this.cboCriteria.MaxDropDownItems = ((short)(5));
            this.cboCriteria.MaxLength = 32767;
            this.cboCriteria.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCriteria.Name = "cboCriteria";
            this.cboCriteria.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboCriteria.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCriteria.Size = new System.Drawing.Size(372, 19);
            this.cboCriteria.TabIndex = 3;
            this.cboCriteria.Text = "c1Combo1";
            this.theme1.SetTheme(this.cboCriteria, "(default)");
            this.cboCriteria.PropBag = resources.GetString("cboCriteria.PropBag");
            // 
            // btnSel
            // 
            this.btnSel.Image = global::bangna_hospital.Properties.Resources.database48;
            this.btnSel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSel.Location = new System.Drawing.Point(690, 106);
            this.btnSel.Name = "btnSel";
            this.btnSel.Size = new System.Drawing.Size(110, 63);
            this.btnSel.TabIndex = 2;
            this.btnSel.Text = "ดึงข้อมูล";
            this.btnSel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnSel, "(default)");
            this.btnSel.UseVisualStyleBackColor = true;
            this.btnSel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // c1Label1
            // 
            this.c1Label1.AutoSize = true;
            this.c1Label1.BackColor = System.Drawing.Color.Transparent;
            this.c1Label1.BorderColor = System.Drawing.Color.Transparent;
            this.c1Label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.c1Label1.Location = new System.Drawing.Point(11, 10);
            this.c1Label1.Name = "c1Label1";
            this.c1Label1.Size = new System.Drawing.Size(51, 13);
            this.c1Label1.TabIndex = 1;
            this.c1Label1.Tag = null;
            this.theme1.SetTheme(this.c1Label1, "(default)");
            this.c1Label1.Value = "HN :";
            this.c1Label1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom;
            // 
            // txtHn
            // 
            this.txtHn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.txtHn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHn.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHn.Location = new System.Drawing.Point(51, 7);
            this.txtHn.Multiline = true;
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(552, 86);
            this.txtHn.TabIndex = 0;
            this.txtHn.Tag = null;
            this.theme1.SetTheme(this.txtHn, "(default)");
            this.txtHn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // tabPrn2
            // 
            this.tabPrn2.Location = new System.Drawing.Point(1, 24);
            this.tabPrn2.Name = "tabPrn2";
            this.tabPrn2.Size = new System.Drawing.Size(1112, 702);
            this.tabPrn2.TabIndex = 1;
            this.tabPrn2.Text = "Criteria Print 2";
            // 
            // FrmPrintCri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 749);
            this.Controls.Add(this.tC1);
            this.Controls.Add(this.sB1);
            this.Name = "FrmPrintCri";
            this.Text = "FrmPrintCri";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPrintCri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sB1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tC1)).EndInit();
            this.tC1.ResumeLayout(false);
            this.tabPrn1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sC1)).EndInit();
            this.sC1.ResumeLayout(false);
            this.c1SplitterPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPrepare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkResXray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkResLab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReqXray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReqLab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStaffNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDrug)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCriteria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private C1.Win.C1Themes.C1ThemeController theme1;
        private C1.Win.C1Ribbon.C1StatusBar sB1;
        private C1.Win.C1Command.C1DockingTab tC1;
        private C1.Win.C1Command.C1DockingTabPage tabPrn1;
        private C1.Win.C1Command.C1DockingTabPage tabPrn2;
        private C1.Win.C1SplitContainer.C1SplitContainer sC1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1Input.C1Label c1Label1;
        private C1.Win.C1Input.C1TextBox txtHn;
        private C1.Win.C1Input.C1CheckBox chkDrug;
        private C1.Win.C1List.C1Combo cboCriteria;
        private C1.Win.C1Input.C1Button btnSel;
        private C1.Win.C1Input.C1CheckBox chkReqLab;
        private C1.Win.C1Input.C1CheckBox chkStaffNote;
        private C1.Win.C1Input.C1Label c1Label2;
        private C1.Win.C1Input.C1DateEdit txtDateEnd;
        private C1.Win.C1Input.C1DateEdit txtDateStart;
        private C1.Win.C1Input.C1Label c1Label4;
        private C1.Win.C1Input.C1Label c1Label3;
        private C1.Win.C1Input.C1Button btnSearch;
        private System.Windows.Forms.Panel pnHn;
        private C1.Win.C1Input.C1CheckBox chkReqXray;
        private C1.Win.C1Input.C1CheckBox chkResXray;
        private C1.Win.C1Input.C1CheckBox chkResLab;
        private C1.Win.C1Input.C1Button BtnPrepare;
    }
}