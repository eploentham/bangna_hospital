namespace bangna_hospital.gui
{
    partial class FrmStock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStock));
            this.Sb1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.lfsbLastUpdate = new C1.Win.C1Ribbon.RibbonLabel();
            this.lfSbStation = new C1.Win.C1Ribbon.RibbonLabel();
            this.lfSbMessage = new C1.Win.C1Ribbon.RibbonLabel();
            this.lfSbStatus = new C1.Win.C1Ribbon.RibbonLabel();
            this.rb1 = new C1.Win.C1Ribbon.RibbonLabel();
            this.btnDrug = new C1.Win.C1Ribbon.RibbonButton();
            this.rb2 = new C1.Win.C1Ribbon.RibbonLabel();
            this.btnSupply = new C1.Win.C1Ribbon.RibbonButton();
            this.rgSbModule = new C1.Win.C1Ribbon.RibbonLabel();
            this.btnScanSaveImg = new C1.Win.C1Ribbon.RibbonButton();
            this.btnOperClose = new C1.Win.C1Ribbon.RibbonButton();
            this.tC = new C1.Win.C1Command.C1DockingTab();
            this.tabOnHand = new C1.Win.C1Command.C1DockingTabPage();
            this.scOnhand = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.pnOnhand = new System.Windows.Forms.Panel();
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.btnOnhandSearch = new System.Windows.Forms.Button();
            this.c1DateEdit1 = new C1.Win.C1Input.C1DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txtChk2DateStart = new C1.Win.C1Input.C1DateEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.lbOnhandItemName = new System.Windows.Forms.Label();
            this.txtOnhandItemcode = new C1.Win.C1Input.C1TextBox();
            this.c1SplitterPanel3 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.tabRec = new C1.Win.C1Command.C1DockingTabPage();
            this.scRec = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel4 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.c1SplitterPanel5 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.pnRecD = new System.Windows.Forms.Panel();
            this.c1SplitterPanel6 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.pnRecH = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboRecYear = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabDraw = new C1.Win.C1Command.C1DockingTabPage();
            this.tabEndYear = new C1.Win.C1Command.C1DockingTabPage();
            this.scEndYear = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel7 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.cboEndYearMonth = new System.Windows.Forms.ComboBox();
            this.btnEndYearUpdate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnEndStockSave = new System.Windows.Forms.Button();
            this.cboEndYearYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEndYearSearch = new C1.Win.C1Input.C1TextBox();
            this.c1SplitterPanel8 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.pnEndYear = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Sb1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tC)).BeginInit();
            this.tC.SuspendLayout();
            this.tabOnHand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scOnhand)).BeginInit();
            this.scOnhand.SuspendLayout();
            this.c1SplitterPanel1.SuspendLayout();
            this.c1SplitterPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChk2DateStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOnhandItemcode)).BeginInit();
            this.tabRec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scRec)).BeginInit();
            this.scRec.SuspendLayout();
            this.c1SplitterPanel5.SuspendLayout();
            this.c1SplitterPanel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabEndYear.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scEndYear)).BeginInit();
            this.scEndYear.SuspendLayout();
            this.c1SplitterPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndYearSearch)).BeginInit();
            this.c1SplitterPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // Sb1
            // 
            this.Sb1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.Sb1.LeftPaneItems.Add(this.lfsbLastUpdate);
            this.Sb1.LeftPaneItems.Add(this.lfSbStation);
            this.Sb1.LeftPaneItems.Add(this.lfSbMessage);
            this.Sb1.LeftPaneItems.Add(this.lfSbStatus);
            this.Sb1.Location = new System.Drawing.Point(0, 674);
            this.Sb1.Name = "Sb1";
            this.Sb1.RightPaneItems.Add(this.rb1);
            this.Sb1.RightPaneItems.Add(this.btnDrug);
            this.Sb1.RightPaneItems.Add(this.rb2);
            this.Sb1.RightPaneItems.Add(this.btnSupply);
            this.Sb1.RightPaneItems.Add(this.rgSbModule);
            this.Sb1.RightPaneItems.Add(this.btnScanSaveImg);
            this.Sb1.RightPaneItems.Add(this.btnOperClose);
            this.Sb1.Size = new System.Drawing.Size(1331, 22);
            // 
            // lfsbLastUpdate
            // 
            this.lfsbLastUpdate.Name = "lfsbLastUpdate";
            this.lfsbLastUpdate.Text = "Label";
            // 
            // lfSbStation
            // 
            this.lfSbStation.Name = "lfSbStation";
            this.lfSbStation.Text = "Label";
            // 
            // lfSbMessage
            // 
            this.lfSbMessage.Name = "lfSbMessage";
            this.lfSbMessage.Text = "Label";
            // 
            // lfSbStatus
            // 
            this.lfSbStatus.Name = "lfSbStatus";
            this.lfSbStatus.Text = "Label";
            // 
            // rb1
            // 
            this.rb1.Name = "rb1";
            this.rb1.Text = "Label";
            // 
            // btnDrug
            // 
            this.btnDrug.Name = "btnDrug";
            this.btnDrug.SmallImage = global::bangna_hospital.Properties.Resources.pngtree_pharmacy_logo_icon_vector_illustration_design_template_png_image_5655290;
            this.btnDrug.Text = "ดึงข้อมูลยาdrug";
            // 
            // rb2
            // 
            this.rb2.Name = "rb2";
            this.rb2.Text = "Label";
            // 
            // btnSupply
            // 
            this.btnSupply.Name = "btnSupply";
            this.btnSupply.SmallImage = global::bangna_hospital.Properties.Resources.Ticket_24;
            this.btnSupply.Text = "ดึงข้อมูลเวชภัณฑ์";
            // 
            // rgSbModule
            // 
            this.rgSbModule.Name = "rgSbModule";
            this.rgSbModule.Text = "Label";
            // 
            // btnScanSaveImg
            // 
            this.btnScanSaveImg.Name = "btnScanSaveImg";
            this.btnScanSaveImg.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnScanSaveImg.SmallImage")));
            this.btnScanSaveImg.Text = "save staffnote";
            // 
            // btnOperClose
            // 
            this.btnOperClose.Name = "btnOperClose";
            this.btnOperClose.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnOperClose.SmallImage")));
            this.btnOperClose.Text = "ปิดการรักษา";
            // 
            // tC
            // 
            this.tC.Controls.Add(this.tabOnHand);
            this.tC.Controls.Add(this.tabRec);
            this.tC.Controls.Add(this.tabDraw);
            this.tC.Controls.Add(this.tabEndYear);
            this.tC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tC.Location = new System.Drawing.Point(0, 0);
            this.tC.Name = "tC";
            this.tC.SelectedIndex = 4;
            this.tC.Size = new System.Drawing.Size(1331, 674);
            this.tC.TabIndex = 2;
            this.tC.TabsSpacing = 5;
            // 
            // tabOnHand
            // 
            this.tabOnHand.Controls.Add(this.scOnhand);
            this.tabOnHand.Location = new System.Drawing.Point(1, 26);
            this.tabOnHand.Name = "tabOnHand";
            this.tabOnHand.Size = new System.Drawing.Size(1329, 647);
            this.tabOnHand.TabIndex = 0;
            this.tabOnHand.Text = "On Hand";
            // 
            // scOnhand
            // 
            this.scOnhand.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.scOnhand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.scOnhand.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.scOnhand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scOnhand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.scOnhand.Location = new System.Drawing.Point(0, 0);
            this.scOnhand.Name = "scOnhand";
            this.scOnhand.Panels.Add(this.c1SplitterPanel1);
            this.scOnhand.Panels.Add(this.c1SplitterPanel2);
            this.scOnhand.Panels.Add(this.c1SplitterPanel3);
            this.scOnhand.Size = new System.Drawing.Size(1329, 647);
            this.scOnhand.TabIndex = 0;
            // 
            // c1SplitterPanel1
            // 
            this.c1SplitterPanel1.Controls.Add(this.pnOnhand);
            this.c1SplitterPanel1.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1SplitterPanel1.Location = new System.Drawing.Point(0, 21);
            this.c1SplitterPanel1.Name = "c1SplitterPanel1";
            this.c1SplitterPanel1.Size = new System.Drawing.Size(872, 626);
            this.c1SplitterPanel1.SizeRatio = 65.847D;
            this.c1SplitterPanel1.TabIndex = 0;
            this.c1SplitterPanel1.Text = "Panel 1";
            this.c1SplitterPanel1.Width = 872;
            // 
            // pnOnhand
            // 
            this.pnOnhand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnOnhand.Location = new System.Drawing.Point(0, 0);
            this.pnOnhand.Name = "pnOnhand";
            this.pnOnhand.Size = new System.Drawing.Size(872, 626);
            this.pnOnhand.TabIndex = 0;
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.Controls.Add(this.btnOnhandSearch);
            this.c1SplitterPanel2.Controls.Add(this.c1DateEdit1);
            this.c1SplitterPanel2.Controls.Add(this.label1);
            this.c1SplitterPanel2.Controls.Add(this.txtChk2DateStart);
            this.c1SplitterPanel2.Controls.Add(this.label6);
            this.c1SplitterPanel2.Controls.Add(this.lbOnhandItemName);
            this.c1SplitterPanel2.Controls.Add(this.txtOnhandItemcode);
            this.c1SplitterPanel2.Height = 101;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(876, 21);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(453, 80);
            this.c1SplitterPanel2.SizeRatio = 15.708D;
            this.c1SplitterPanel2.TabIndex = 1;
            this.c1SplitterPanel2.Text = "Panel 2";
            // 
            // btnOnhandSearch
            // 
            this.btnOnhandSearch.BackColor = System.Drawing.Color.PaleGreen;
            this.btnOnhandSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOnhandSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOnhandSearch.Location = new System.Drawing.Point(399, 29);
            this.btnOnhandSearch.Name = "btnOnhandSearch";
            this.btnOnhandSearch.Size = new System.Drawing.Size(45, 32);
            this.btnOnhandSearch.TabIndex = 137;
            this.btnOnhandSearch.Text = "...";
            this.btnOnhandSearch.UseVisualStyleBackColor = false;
            // 
            // c1DateEdit1
            // 
            this.c1DateEdit1.AllowSpinLoop = false;
            // 
            // 
            // 
            this.c1DateEdit1.Calendar.DayNameLength = 1;
            this.c1DateEdit1.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.c1DateEdit1.Culture = 1054;
            this.c1DateEdit1.CurrentTimeZone = false;
            this.c1DateEdit1.DisplayFormat.CalendarType = C1.Win.C1Input.CalendarType.ThaiBuddhistCalendar;
            this.c1DateEdit1.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            this.c1DateEdit1.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.c1DateEdit1.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)));
            this.c1DateEdit1.EditFormat.CalendarType = C1.Win.C1Input.CalendarType.ThaiBuddhistCalendar;
            this.c1DateEdit1.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.c1DateEdit1.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.c1DateEdit1.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)));
            this.c1DateEdit1.EmptyAsNull = true;
            this.c1DateEdit1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.c1DateEdit1.GMTOffset = System.TimeSpan.Parse("07:00:00");
            this.c1DateEdit1.ImagePadding = new System.Windows.Forms.Padding(0);
            this.c1DateEdit1.Location = new System.Drawing.Point(264, 35);
            this.c1DateEdit1.Name = "c1DateEdit1";
            this.c1DateEdit1.Size = new System.Drawing.Size(129, 22);
            this.c1DateEdit1.TabIndex = 136;
            this.c1DateEdit1.Tag = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(211, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 135;
            this.label1.Text = "ถึงวันที่";
            // 
            // txtChk2DateStart
            // 
            this.txtChk2DateStart.AllowSpinLoop = false;
            // 
            // 
            // 
            this.txtChk2DateStart.Calendar.DayNameLength = 1;
            this.txtChk2DateStart.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtChk2DateStart.Culture = 1054;
            this.txtChk2DateStart.CurrentTimeZone = false;
            this.txtChk2DateStart.DisplayFormat.CalendarType = C1.Win.C1Input.CalendarType.ThaiBuddhistCalendar;
            this.txtChk2DateStart.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            this.txtChk2DateStart.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtChk2DateStart.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)));
            this.txtChk2DateStart.EditFormat.CalendarType = C1.Win.C1Input.CalendarType.ThaiBuddhistCalendar;
            this.txtChk2DateStart.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtChk2DateStart.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtChk2DateStart.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)));
            this.txtChk2DateStart.EmptyAsNull = true;
            this.txtChk2DateStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtChk2DateStart.GMTOffset = System.TimeSpan.Parse("07:00:00");
            this.txtChk2DateStart.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtChk2DateStart.Location = new System.Drawing.Point(78, 33);
            this.txtChk2DateStart.Name = "txtChk2DateStart";
            this.txtChk2DateStart.Size = new System.Drawing.Size(129, 22);
            this.txtChk2DateStart.TabIndex = 134;
            this.txtChk2DateStart.Tag = null;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(4, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 133;
            this.label6.Text = "ตั้งแต่วันที่";
            // 
            // lbOnhandItemName
            // 
            this.lbOnhandItemName.AutoSize = true;
            this.lbOnhandItemName.BackColor = System.Drawing.Color.Transparent;
            this.lbOnhandItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOnhandItemName.ForeColor = System.Drawing.Color.Black;
            this.lbOnhandItemName.Location = new System.Drawing.Point(118, 5);
            this.lbOnhandItemName.Name = "lbOnhandItemName";
            this.lbOnhandItemName.Size = new System.Drawing.Size(40, 20);
            this.lbOnhandItemName.TabIndex = 6;
            this.lbOnhandItemName.Text = "HN :";
            // 
            // txtOnhandItemcode
            // 
            this.txtOnhandItemcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOnhandItemcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnhandItemcode.Location = new System.Drawing.Point(3, 3);
            this.txtOnhandItemcode.Name = "txtOnhandItemcode";
            this.txtOnhandItemcode.Size = new System.Drawing.Size(107, 24);
            this.txtOnhandItemcode.TabIndex = 5;
            this.txtOnhandItemcode.Tag = null;
            this.txtOnhandItemcode.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtOnhandItemcode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // c1SplitterPanel3
            // 
            this.c1SplitterPanel3.Height = 542;
            this.c1SplitterPanel3.Location = new System.Drawing.Point(876, 126);
            this.c1SplitterPanel3.Name = "c1SplitterPanel3";
            this.c1SplitterPanel3.Size = new System.Drawing.Size(453, 521);
            this.c1SplitterPanel3.TabIndex = 2;
            this.c1SplitterPanel3.Text = "Panel 3";
            // 
            // tabRec
            // 
            this.tabRec.Controls.Add(this.scRec);
            this.tabRec.Location = new System.Drawing.Point(1, 26);
            this.tabRec.Name = "tabRec";
            this.tabRec.Size = new System.Drawing.Size(1329, 647);
            this.tabRec.TabIndex = 1;
            this.tabRec.Text = "รับเข้า REC(เบิกบาง2)";
            // 
            // scRec
            // 
            this.scRec.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.scRec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.scRec.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.scRec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scRec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.scRec.Location = new System.Drawing.Point(0, 0);
            this.scRec.Name = "scRec";
            this.scRec.Panels.Add(this.c1SplitterPanel4);
            this.scRec.Panels.Add(this.c1SplitterPanel5);
            this.scRec.Panels.Add(this.c1SplitterPanel6);
            this.scRec.Size = new System.Drawing.Size(1329, 647);
            this.scRec.TabIndex = 0;
            // 
            // c1SplitterPanel4
            // 
            this.c1SplitterPanel4.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            this.c1SplitterPanel4.Location = new System.Drawing.Point(667, 21);
            this.c1SplitterPanel4.Name = "c1SplitterPanel4";
            this.c1SplitterPanel4.Size = new System.Drawing.Size(662, 626);
            this.c1SplitterPanel4.SizeRatio = 49.962D;
            this.c1SplitterPanel4.TabIndex = 0;
            this.c1SplitterPanel4.Text = "Panel 1";
            this.c1SplitterPanel4.Width = 662;
            // 
            // c1SplitterPanel5
            // 
            this.c1SplitterPanel5.Controls.Add(this.pnRecD);
            this.c1SplitterPanel5.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Bottom;
            this.c1SplitterPanel5.Height = 322;
            this.c1SplitterPanel5.Location = new System.Drawing.Point(0, 346);
            this.c1SplitterPanel5.Name = "c1SplitterPanel5";
            this.c1SplitterPanel5.Size = new System.Drawing.Size(663, 301);
            this.c1SplitterPanel5.TabIndex = 1;
            this.c1SplitterPanel5.Text = "Panel 2";
            // 
            // pnRecD
            // 
            this.pnRecD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnRecD.Location = new System.Drawing.Point(0, 0);
            this.pnRecD.Name = "pnRecD";
            this.pnRecD.Size = new System.Drawing.Size(663, 301);
            this.pnRecD.TabIndex = 0;
            // 
            // c1SplitterPanel6
            // 
            this.c1SplitterPanel6.Controls.Add(this.pnRecH);
            this.c1SplitterPanel6.Controls.Add(this.panel1);
            this.c1SplitterPanel6.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            this.c1SplitterPanel6.Location = new System.Drawing.Point(0, 21);
            this.c1SplitterPanel6.Name = "c1SplitterPanel6";
            this.c1SplitterPanel6.Size = new System.Drawing.Size(663, 300);
            this.c1SplitterPanel6.TabIndex = 2;
            this.c1SplitterPanel6.Text = "Panel 3";
            this.c1SplitterPanel6.Width = 663;
            // 
            // pnRecH
            // 
            this.pnRecH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnRecH.Location = new System.Drawing.Point(0, 44);
            this.pnRecH.Name = "pnRecH";
            this.pnRecH.Size = new System.Drawing.Size(663, 256);
            this.pnRecH.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboRecYear);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(663, 44);
            this.panel1.TabIndex = 0;
            // 
            // cboRecYear
            // 
            this.cboRecYear.FormattingEnabled = true;
            this.cboRecYear.Location = new System.Drawing.Point(59, 7);
            this.cboRecYear.Name = "cboRecYear";
            this.cboRecYear.Size = new System.Drawing.Size(121, 28);
            this.cboRecYear.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "ปี";
            // 
            // tabDraw
            // 
            this.tabDraw.Location = new System.Drawing.Point(1, 26);
            this.tabDraw.Name = "tabDraw";
            this.tabDraw.Size = new System.Drawing.Size(1329, 647);
            this.tabDraw.TabIndex = 2;
            this.tabDraw.Text = "ตัดจ่าย DRAW";
            // 
            // tabEndYear
            // 
            this.tabEndYear.Controls.Add(this.scEndYear);
            this.tabEndYear.Location = new System.Drawing.Point(1, 26);
            this.tabEndYear.Name = "tabEndYear";
            this.tabEndYear.Size = new System.Drawing.Size(1329, 647);
            this.tabEndYear.TabIndex = 3;
            this.tabEndYear.Text = "End Year";
            // 
            // scEndYear
            // 
            this.scEndYear.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.scEndYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.scEndYear.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.scEndYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scEndYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.scEndYear.Location = new System.Drawing.Point(0, 0);
            this.scEndYear.Name = "scEndYear";
            this.scEndYear.Panels.Add(this.c1SplitterPanel7);
            this.scEndYear.Panels.Add(this.c1SplitterPanel8);
            this.scEndYear.Size = new System.Drawing.Size(1329, 647);
            this.scEndYear.TabIndex = 4;
            // 
            // c1SplitterPanel7
            // 
            this.c1SplitterPanel7.Controls.Add(this.cboEndYearMonth);
            this.c1SplitterPanel7.Controls.Add(this.btnEndYearUpdate);
            this.c1SplitterPanel7.Controls.Add(this.label4);
            this.c1SplitterPanel7.Controls.Add(this.button2);
            this.c1SplitterPanel7.Controls.Add(this.btnEndStockSave);
            this.c1SplitterPanel7.Controls.Add(this.cboEndYearYear);
            this.c1SplitterPanel7.Controls.Add(this.label3);
            this.c1SplitterPanel7.Controls.Add(this.txtEndYearSearch);
            this.c1SplitterPanel7.Height = 64;
            this.c1SplitterPanel7.Location = new System.Drawing.Point(0, 21);
            this.c1SplitterPanel7.Name = "c1SplitterPanel7";
            this.c1SplitterPanel7.Size = new System.Drawing.Size(1329, 43);
            this.c1SplitterPanel7.SizeRatio = 9.953D;
            this.c1SplitterPanel7.TabIndex = 0;
            this.c1SplitterPanel7.Text = "Panel 1";
            // 
            // cboEndYearMonth
            // 
            this.cboEndYearMonth.FormattingEnabled = true;
            this.cboEndYearMonth.Location = new System.Drawing.Point(173, 9);
            this.cboEndYearMonth.Name = "cboEndYearMonth";
            this.cboEndYearMonth.Size = new System.Drawing.Size(121, 24);
            this.cboEndYearMonth.TabIndex = 273;
            // 
            // btnEndYearUpdate
            // 
            this.btnEndYearUpdate.BackColor = System.Drawing.Color.PaleGreen;
            this.btnEndYearUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnEndYearUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEndYearUpdate.Location = new System.Drawing.Point(533, 5);
            this.btnEndYearUpdate.Name = "btnEndYearUpdate";
            this.btnEndYearUpdate.Size = new System.Drawing.Size(197, 32);
            this.btnEndYearUpdate.TabIndex = 272;
            this.btnEndYearUpdate.Text = "update qty(onhandnew)";
            this.btnEndYearUpdate.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(736, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 24);
            this.label4.TabIndex = 270;
            this.label4.Text = "search";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PaleGreen;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(1124, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 32);
            this.button2.TabIndex = 139;
            this.button2.Text = "gen stock";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnEndStockSave
            // 
            this.btnEndStockSave.BackColor = System.Drawing.Color.PaleGreen;
            this.btnEndStockSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnEndStockSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEndStockSave.Location = new System.Drawing.Point(318, 3);
            this.btnEndStockSave.Name = "btnEndStockSave";
            this.btnEndStockSave.Size = new System.Drawing.Size(183, 32);
            this.btnEndStockSave.TabIndex = 138;
            this.btnEndStockSave.Text = "save (MNC_PH_QTY)";
            this.btnEndStockSave.UseVisualStyleBackColor = false;
            // 
            // cboEndYearYear
            // 
            this.cboEndYearYear.FormattingEnabled = true;
            this.cboEndYearYear.Location = new System.Drawing.Point(47, 9);
            this.cboEndYearYear.Name = "cboEndYearYear";
            this.cboEndYearYear.Size = new System.Drawing.Size(83, 24);
            this.cboEndYearYear.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(8, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "ปี";
            // 
            // txtEndYearSearch
            // 
            this.txtEndYearSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtEndYearSearch.Location = new System.Drawing.Point(809, 8);
            this.txtEndYearSearch.Name = "txtEndYearSearch";
            this.txtEndYearSearch.Size = new System.Drawing.Size(255, 27);
            this.txtEndYearSearch.TabIndex = 271;
            this.txtEndYearSearch.Tag = null;
            // 
            // c1SplitterPanel8
            // 
            this.c1SplitterPanel8.Controls.Add(this.pnEndYear);
            this.c1SplitterPanel8.Height = 579;
            this.c1SplitterPanel8.Location = new System.Drawing.Point(0, 89);
            this.c1SplitterPanel8.Name = "c1SplitterPanel8";
            this.c1SplitterPanel8.Size = new System.Drawing.Size(1329, 558);
            this.c1SplitterPanel8.TabIndex = 1;
            this.c1SplitterPanel8.Text = "Panel 2";
            // 
            // pnEndYear
            // 
            this.pnEndYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnEndYear.Location = new System.Drawing.Point(0, 0);
            this.pnEndYear.Name = "pnEndYear";
            this.pnEndYear.Size = new System.Drawing.Size(1329, 558);
            this.pnEndYear.TabIndex = 0;
            // 
            // FrmStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 696);
            this.Controls.Add(this.tC);
            this.Controls.Add(this.Sb1);
            this.Name = "FrmStock";
            this.Text = "FrmStock";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Sb1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tC)).EndInit();
            this.tC.ResumeLayout(false);
            this.tabOnHand.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scOnhand)).EndInit();
            this.scOnhand.ResumeLayout(false);
            this.c1SplitterPanel1.ResumeLayout(false);
            this.c1SplitterPanel2.ResumeLayout(false);
            this.c1SplitterPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChk2DateStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOnhandItemcode)).EndInit();
            this.tabRec.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scRec)).EndInit();
            this.scRec.ResumeLayout(false);
            this.c1SplitterPanel5.ResumeLayout(false);
            this.c1SplitterPanel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabEndYear.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scEndYear)).EndInit();
            this.scEndYear.ResumeLayout(false);
            this.c1SplitterPanel7.ResumeLayout(false);
            this.c1SplitterPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndYearSearch)).EndInit();
            this.c1SplitterPanel8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar Sb1;
        private C1.Win.C1Ribbon.RibbonLabel lfsbLastUpdate;
        private C1.Win.C1Ribbon.RibbonLabel lfSbStation;
        private C1.Win.C1Ribbon.RibbonLabel lfSbMessage;
        private C1.Win.C1Ribbon.RibbonLabel lfSbStatus;
        private C1.Win.C1Ribbon.RibbonLabel rb1;
        private C1.Win.C1Ribbon.RibbonButton btnDrug;
        private C1.Win.C1Ribbon.RibbonLabel rb2;
        private C1.Win.C1Ribbon.RibbonButton btnSupply;
        private C1.Win.C1Ribbon.RibbonLabel rgSbModule;
        private C1.Win.C1Ribbon.RibbonButton btnScanSaveImg;
        private C1.Win.C1Ribbon.RibbonButton btnOperClose;
        private C1.Win.C1Command.C1DockingTab tC;
        private C1.Win.C1Command.C1DockingTabPage tabOnHand;
        private C1.Win.C1Command.C1DockingTabPage tabRec;
        private C1.Win.C1Command.C1DockingTabPage tabDraw;
        private C1.Win.C1Command.C1DockingTabPage tabEndYear;
        private C1.Win.C1SplitContainer.C1SplitContainer scOnhand;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel3;
        private System.Windows.Forms.Panel pnOnhand;
        private C1.Win.C1SplitContainer.C1SplitContainer scRec;
        private C1.Win.C1Input.C1TextBox txtOnhandItemcode;
        private System.Windows.Forms.Label lbOnhandItemName;
        private C1.Win.C1Input.C1DateEdit c1DateEdit1;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1DateEdit txtChk2DateStart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOnhandSearch;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel4;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel5;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel6;
        private System.Windows.Forms.Panel pnRecH;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboRecYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnRecD;
        private C1.Win.C1SplitContainer.C1SplitContainer scEndYear;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel8;
        private System.Windows.Forms.Panel pnEndYear;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel7;
        private System.Windows.Forms.Button btnEndYearUpdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnEndStockSave;
        private System.Windows.Forms.ComboBox cboEndYearYear;
        private System.Windows.Forms.Label label3;
        private C1.Win.C1Input.C1TextBox txtEndYearSearch;
        private System.Windows.Forms.ComboBox cboEndYearMonth;
    }
}