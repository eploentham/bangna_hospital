namespace bangna_hospital.gui
{
    partial class FrmDoctor
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
            this.lfSbLastUpdate = new C1.Win.C1Ribbon.RibbonLabel();
            this.lfSbMessage = new C1.Win.C1Ribbon.RibbonLabel();
            this.lfSbStation = new C1.Win.C1Ribbon.RibbonLabel();
            this.rgSbModule = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonLabel4 = new C1.Win.C1Ribbon.RibbonLabel();
            this.rbSbDrugSet = new C1.Win.C1Ribbon.RibbonButton();
            this.rpDrugSetNew = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonSeparator1 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.rbToken = new C1.Win.C1Ribbon.RibbonButton();
            this.spDoctor = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.spTop = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.pnTop = new System.Windows.Forms.Panel();
            this.btnDfDate = new C1.Win.C1Input.C1Button();
            this.txtHN = new C1.Win.C1Input.C1TextBox();
            this.txtDate = new C1.Win.Calendar.C1DateEdit();
            this.lbDtrName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.spMain = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.tC1 = new C1.Win.C1Command.C1DockingTab();
            this.tabQue = new C1.Win.C1Command.C1DockingTabPage();
            this.pnQue = new System.Windows.Forms.Panel();
            this.tabApm = new C1.Win.C1Command.C1DockingTabPage();
            this.pnApm = new System.Windows.Forms.Panel();
            this.tabIPD = new C1.Win.C1Command.C1DockingTabPage();
            this.pnIPD = new System.Windows.Forms.Panel();
            this.tabRpt = new C1.Win.C1Command.C1DockingTabPage();
            this.spRpt = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel8 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.pnRpt = new System.Windows.Forms.Panel();
            this.arvMain = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
            this.c1SplitterPanel5 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.pnRptName = new System.Windows.Forms.Panel();
            this.c1SplitterPanel18 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.pnRptCriDate = new System.Windows.Forms.Panel();
            this.label85 = new System.Windows.Forms.Label();
            this.cboRpt2 = new C1.Win.C1Input.C1ComboBox();
            this.cboRpt1 = new C1.Win.C1Input.C1ComboBox();
            this.btnRpt1 = new C1.Win.C1Input.C1Button();
            this.btnRptPrint = new C1.Win.C1Input.C1Button();
            this.txtRptEndDate = new C1.Win.Calendar.C1DateEdit();
            this.label84 = new System.Windows.Forms.Label();
            this.txtRptStartDate = new C1.Win.Calendar.C1DateEdit();
            this.label78 = new System.Windows.Forms.Label();
            this.pnRptCri1 = new System.Windows.Forms.Panel();
            this.tabFinish = new C1.Win.C1Command.C1DockingTabPage();
            this.pnFinish = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDoctor)).BeginInit();
            this.spDoctor.SuspendLayout();
            this.spTop.SuspendLayout();
            this.pnTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDfDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate)).BeginInit();
            this.spMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tC1)).BeginInit();
            this.tC1.SuspendLayout();
            this.tabQue.SuspendLayout();
            this.tabApm.SuspendLayout();
            this.tabIPD.SuspendLayout();
            this.tabRpt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spRpt)).BeginInit();
            this.spRpt.SuspendLayout();
            this.c1SplitterPanel8.SuspendLayout();
            this.pnRpt.SuspendLayout();
            this.c1SplitterPanel5.SuspendLayout();
            this.c1SplitterPanel18.SuspendLayout();
            this.pnRptCriDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboRpt2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRpt1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRpt1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRptPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRptEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRptStartDate)).BeginInit();
            this.tabFinish.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1StatusBar1.LeftPaneItems.Add(this.lfSbLastUpdate);
            this.c1StatusBar1.LeftPaneItems.Add(this.lfSbMessage);
            this.c1StatusBar1.LeftPaneItems.Add(this.lfSbStation);
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 758);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.rgSbModule);
            this.c1StatusBar1.RightPaneItems.Add(this.ribbonLabel4);
            this.c1StatusBar1.RightPaneItems.Add(this.rbSbDrugSet);
            this.c1StatusBar1.RightPaneItems.Add(this.rpDrugSetNew);
            this.c1StatusBar1.RightPaneItems.Add(this.ribbonSeparator1);
            this.c1StatusBar1.RightPaneItems.Add(this.rbToken);
            this.c1StatusBar1.Size = new System.Drawing.Size(1194, 22);
            // 
            // lfSbLastUpdate
            // 
            this.lfSbLastUpdate.Name = "lfSbLastUpdate";
            this.lfSbLastUpdate.Text = "Label";
            // 
            // lfSbMessage
            // 
            this.lfSbMessage.Name = "lfSbMessage";
            this.lfSbMessage.Text = "Label";
            // 
            // lfSbStation
            // 
            this.lfSbStation.Name = "lfSbStation";
            this.lfSbStation.Text = "Label";
            // 
            // rgSbModule
            // 
            this.rgSbModule.Name = "rgSbModule";
            this.rgSbModule.Text = "Label";
            // 
            // ribbonLabel4
            // 
            this.ribbonLabel4.Name = "ribbonLabel4";
            this.ribbonLabel4.Text = "Label";
            // 
            // rbSbDrugSet
            // 
            this.rbSbDrugSet.Name = "rbSbDrugSet";
            this.rbSbDrugSet.SmallImage = global::bangna_hospital.Properties.Resources.TableRowProperties_small;
            this.rbSbDrugSet.Text = "drug set";
            // 
            // rpDrugSetNew
            // 
            this.rpDrugSetNew.Name = "rpDrugSetNew";
            this.rpDrugSetNew.SmallImage = global::bangna_hospital.Properties.Resources.custom_reports24;
            this.rpDrugSetNew.Text = "Drug Set [Chief complian] [Physical exam] ";
            // 
            // ribbonSeparator1
            // 
            this.ribbonSeparator1.Name = "ribbonSeparator1";
            // 
            // rbToken
            // 
            this.rbToken.Name = "rbToken";
            this.rbToken.SmallImage = global::bangna_hospital.Properties.Resources.Add_ticket_24;
            this.rbToken.Text = "Token";
            // 
            // spDoctor
            // 
            this.spDoctor.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.spDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.spDoctor.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.spDoctor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spDoctor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.spDoctor.Location = new System.Drawing.Point(0, 0);
            this.spDoctor.Name = "spDoctor";
            this.spDoctor.Panels.Add(this.spTop);
            this.spDoctor.Panels.Add(this.spMain);
            this.spDoctor.Size = new System.Drawing.Size(1194, 758);
            this.spDoctor.TabIndex = 2;
            // 
            // spTop
            // 
            this.spTop.Collapsible = true;
            this.spTop.Controls.Add(this.pnTop);
            this.spTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.spTop.Height = 71;
            this.spTop.Location = new System.Drawing.Point(0, 21);
            this.spTop.Name = "spTop";
            this.spTop.Size = new System.Drawing.Size(1194, 43);
            this.spTop.SizeRatio = 9.416D;
            this.spTop.TabIndex = 0;
            this.spTop.Text = "Panel 1";
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.btnDfDate);
            this.pnTop.Controls.Add(this.txtHN);
            this.pnTop.Controls.Add(this.txtDate);
            this.pnTop.Controls.Add(this.lbDtrName);
            this.pnTop.Controls.Add(this.label1);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(1194, 43);
            this.pnTop.TabIndex = 272;
            // 
            // btnDfDate
            // 
            this.btnDfDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnDfDate.Image = global::bangna_hospital.Properties.Resources.Female_user_edit_24;
            this.btnDfDate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDfDate.Location = new System.Drawing.Point(955, 3);
            this.btnDfDate.Name = "btnDfDate";
            this.btnDfDate.Size = new System.Drawing.Size(86, 33);
            this.btnDfDate.TabIndex = 272;
            this.btnDfDate.Text = "วันที่:";
            this.btnDfDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDfDate.UseVisualStyleBackColor = true;
            // 
            // txtHN
            // 
            this.txtHN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHN.Location = new System.Drawing.Point(517, 8);
            this.txtHN.Name = "txtHN";
            this.txtHN.Size = new System.Drawing.Size(91, 27);
            this.txtHN.TabIndex = 269;
            this.txtHN.Tag = null;
            // 
            // txtDate
            // 
            this.txtDate.AllowSpinLoop = false;
            this.txtDate.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.txtDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtDate.CurrentTimeZone = false;
            this.txtDate.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            this.txtDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtDate.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtDate.GMTOffset = System.TimeSpan.Parse("07:00:00");
            this.txtDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtDate.Location = new System.Drawing.Point(1047, 6);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(128, 27);
            this.txtDate.TabIndex = 271;
            this.txtDate.Tag = null;
            // 
            // lbDtrName
            // 
            this.lbDtrName.AutoSize = true;
            this.lbDtrName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbDtrName.Location = new System.Drawing.Point(7, 8);
            this.lbDtrName.Name = "lbDtrName";
            this.lbDtrName.Size = new System.Drawing.Size(79, 24);
            this.lbDtrName.TabIndex = 267;
            this.lbDtrName.Text = "dtrname";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(473, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 24);
            this.label1.TabIndex = 268;
            this.label1.Text = "HN";
            // 
            // spMain
            // 
            this.spMain.Controls.Add(this.tC1);
            this.spMain.Height = 683;
            this.spMain.Location = new System.Drawing.Point(0, 96);
            this.spMain.Name = "spMain";
            this.spMain.Size = new System.Drawing.Size(1194, 662);
            this.spMain.TabIndex = 1;
            this.spMain.Text = "Panel 2";
            // 
            // tC1
            // 
            this.tC1.Controls.Add(this.tabQue);
            this.tC1.Controls.Add(this.tabApm);
            this.tC1.Controls.Add(this.tabIPD);
            this.tC1.Controls.Add(this.tabRpt);
            this.tC1.Controls.Add(this.tabFinish);
            this.tC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tC1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tC1.Location = new System.Drawing.Point(0, 0);
            this.tC1.Name = "tC1";
            this.tC1.SelectedIndex = 5;
            this.tC1.Size = new System.Drawing.Size(1194, 662);
            this.tC1.TabIndex = 0;
            this.tC1.TabsSpacing = 5;
            this.tC1.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            // 
            // tabQue
            // 
            this.tabQue.Controls.Add(this.pnQue);
            this.tabQue.Location = new System.Drawing.Point(1, 26);
            this.tabQue.Name = "tabQue";
            this.tabQue.Size = new System.Drawing.Size(1192, 635);
            this.tabQue.TabIndex = 0;
            this.tabQue.Text = "Queue";
            // 
            // pnQue
            // 
            this.pnQue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnQue.Location = new System.Drawing.Point(0, 0);
            this.pnQue.Name = "pnQue";
            this.pnQue.Size = new System.Drawing.Size(1192, 635);
            this.pnQue.TabIndex = 0;
            // 
            // tabApm
            // 
            this.tabApm.Controls.Add(this.pnApm);
            this.tabApm.Location = new System.Drawing.Point(1, 26);
            this.tabApm.Name = "tabApm";
            this.tabApm.Size = new System.Drawing.Size(1192, 635);
            this.tabApm.TabIndex = 1;
            this.tabApm.Text = "Appointment";
            // 
            // pnApm
            // 
            this.pnApm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnApm.Location = new System.Drawing.Point(0, 0);
            this.pnApm.Name = "pnApm";
            this.pnApm.Size = new System.Drawing.Size(1192, 635);
            this.pnApm.TabIndex = 0;
            // 
            // tabIPD
            // 
            this.tabIPD.Controls.Add(this.pnIPD);
            this.tabIPD.Location = new System.Drawing.Point(1, 26);
            this.tabIPD.Name = "tabIPD";
            this.tabIPD.Size = new System.Drawing.Size(1192, 635);
            this.tabIPD.TabIndex = 2;
            this.tabIPD.Text = "IPD";
            // 
            // pnIPD
            // 
            this.pnIPD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnIPD.Location = new System.Drawing.Point(0, 0);
            this.pnIPD.Name = "pnIPD";
            this.pnIPD.Size = new System.Drawing.Size(1192, 635);
            this.pnIPD.TabIndex = 0;
            // 
            // tabRpt
            // 
            this.tabRpt.Controls.Add(this.spRpt);
            this.tabRpt.Location = new System.Drawing.Point(1, 26);
            this.tabRpt.Name = "tabRpt";
            this.tabRpt.Size = new System.Drawing.Size(1192, 635);
            this.tabRpt.TabIndex = 3;
            this.tabRpt.Text = "Report";
            // 
            // spRpt
            // 
            this.spRpt.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.spRpt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.spRpt.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.spRpt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spRpt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.spRpt.Location = new System.Drawing.Point(0, 0);
            this.spRpt.Name = "spRpt";
            this.spRpt.Panels.Add(this.c1SplitterPanel8);
            this.spRpt.Panels.Add(this.c1SplitterPanel5);
            this.spRpt.Panels.Add(this.c1SplitterPanel18);
            this.spRpt.Size = new System.Drawing.Size(1192, 635);
            this.spRpt.TabIndex = 1;
            // 
            // c1SplitterPanel8
            // 
            this.c1SplitterPanel8.Controls.Add(this.pnRpt);
            this.c1SplitterPanel8.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            this.c1SplitterPanel8.Location = new System.Drawing.Point(364, 21);
            this.c1SplitterPanel8.Name = "c1SplitterPanel8";
            this.c1SplitterPanel8.Size = new System.Drawing.Size(828, 614);
            this.c1SplitterPanel8.SizeRatio = 69.715D;
            this.c1SplitterPanel8.TabIndex = 1;
            this.c1SplitterPanel8.Text = "Panel 2";
            this.c1SplitterPanel8.Width = 828;
            // 
            // pnRpt
            // 
            this.pnRpt.Controls.Add(this.arvMain);
            this.pnRpt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnRpt.Location = new System.Drawing.Point(0, 0);
            this.pnRpt.Name = "pnRpt";
            this.pnRpt.Size = new System.Drawing.Size(828, 614);
            this.pnRpt.TabIndex = 0;
            // 
            // arvMain
            // 
            this.arvMain.CurrentPage = 0;
            this.arvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arvMain.Location = new System.Drawing.Point(0, 0);
            this.arvMain.Name = "arvMain";
            this.arvMain.PreviewPages = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            this.arvMain.Sidebar.ParametersPanel.ContextMenu = null;
            this.arvMain.Sidebar.ParametersPanel.Text = "Parameters";
            this.arvMain.Sidebar.ParametersPanel.Width = 200;
            // 
            // 
            // 
            this.arvMain.Sidebar.SearchPanel.ContextMenu = null;
            this.arvMain.Sidebar.SearchPanel.Text = "Search results";
            this.arvMain.Sidebar.SearchPanel.Width = 200;
            // 
            // 
            // 
            this.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = null;
            this.arvMain.Sidebar.ThumbnailsPanel.Text = "Page thumbnails";
            this.arvMain.Sidebar.ThumbnailsPanel.Width = 200;
            this.arvMain.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
            // 
            // 
            // 
            this.arvMain.Sidebar.TocPanel.ContextMenu = null;
            this.arvMain.Sidebar.TocPanel.Expanded = true;
            this.arvMain.Sidebar.TocPanel.Text = "Document map";
            this.arvMain.Sidebar.TocPanel.Width = 200;
            this.arvMain.Sidebar.Width = 200;
            this.arvMain.Size = new System.Drawing.Size(828, 614);
            this.arvMain.TabIndex = 1;
            // 
            // c1SplitterPanel5
            // 
            this.c1SplitterPanel5.Collapsible = true;
            this.c1SplitterPanel5.Controls.Add(this.pnRptName);
            this.c1SplitterPanel5.Cursor = System.Windows.Forms.Cursors.Default;
            this.c1SplitterPanel5.Height = 230;
            this.c1SplitterPanel5.Location = new System.Drawing.Point(0, 21);
            this.c1SplitterPanel5.Name = "c1SplitterPanel5";
            this.c1SplitterPanel5.Size = new System.Drawing.Size(360, 209);
            this.c1SplitterPanel5.SizeRatio = 37.638D;
            this.c1SplitterPanel5.TabIndex = 0;
            this.c1SplitterPanel5.Text = "Panel 1";
            this.c1SplitterPanel5.Width = 360;
            // 
            // pnRptName
            // 
            this.pnRptName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnRptName.Location = new System.Drawing.Point(0, 0);
            this.pnRptName.Name = "pnRptName";
            this.pnRptName.Size = new System.Drawing.Size(360, 209);
            this.pnRptName.TabIndex = 0;
            // 
            // c1SplitterPanel18
            // 
            this.c1SplitterPanel18.Controls.Add(this.pnRptCriDate);
            this.c1SplitterPanel18.Controls.Add(this.pnRptCri1);
            this.c1SplitterPanel18.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Bottom;
            this.c1SplitterPanel18.Height = 394;
            this.c1SplitterPanel18.Location = new System.Drawing.Point(0, 262);
            this.c1SplitterPanel18.Name = "c1SplitterPanel18";
            this.c1SplitterPanel18.Size = new System.Drawing.Size(360, 373);
            this.c1SplitterPanel18.TabIndex = 2;
            this.c1SplitterPanel18.Text = "Panel 3";
            // 
            // pnRptCriDate
            // 
            this.pnRptCriDate.Controls.Add(this.label85);
            this.pnRptCriDate.Controls.Add(this.cboRpt2);
            this.pnRptCriDate.Controls.Add(this.cboRpt1);
            this.pnRptCriDate.Controls.Add(this.btnRpt1);
            this.pnRptCriDate.Controls.Add(this.btnRptPrint);
            this.pnRptCriDate.Controls.Add(this.txtRptEndDate);
            this.pnRptCriDate.Controls.Add(this.label84);
            this.pnRptCriDate.Controls.Add(this.txtRptStartDate);
            this.pnRptCriDate.Controls.Add(this.label78);
            this.pnRptCriDate.Location = new System.Drawing.Point(3, 3);
            this.pnRptCriDate.Name = "pnRptCriDate";
            this.pnRptCriDate.Size = new System.Drawing.Size(420, 191);
            this.pnRptCriDate.TabIndex = 0;
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label85.Location = new System.Drawing.Point(8, 106);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(45, 20);
            this.label85.TabIndex = 265;
            this.label85.Text = "แผนก";
            // 
            // cboRpt2
            // 
            this.cboRpt2.AllowSpinLoop = false;
            this.cboRpt2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboRpt2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboRpt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboRpt2.GapHeight = 0;
            this.cboRpt2.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboRpt2.ItemsDisplayMember = "";
            this.cboRpt2.ItemsValueMember = "";
            this.cboRpt2.Location = new System.Drawing.Point(77, 102);
            this.cboRpt2.Name = "cboRpt2";
            this.cboRpt2.Size = new System.Drawing.Size(220, 24);
            this.cboRpt2.TabIndex = 264;
            this.cboRpt2.Tag = null;
            // 
            // cboRpt1
            // 
            this.cboRpt1.AllowSpinLoop = false;
            this.cboRpt1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboRpt1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboRpt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboRpt1.GapHeight = 0;
            this.cboRpt1.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboRpt1.ItemsDisplayMember = "";
            this.cboRpt1.ItemsValueMember = "";
            this.cboRpt1.Location = new System.Drawing.Point(77, 72);
            this.cboRpt1.Name = "cboRpt1";
            this.cboRpt1.Size = new System.Drawing.Size(220, 24);
            this.cboRpt1.TabIndex = 263;
            this.cboRpt1.Tag = null;
            // 
            // btnRpt1
            // 
            this.btnRpt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnRpt1.Location = new System.Drawing.Point(8, 71);
            this.btnRpt1.Name = "btnRpt1";
            this.btnRpt1.Size = new System.Drawing.Size(62, 28);
            this.btnRpt1.TabIndex = 262;
            this.btnRpt1.Text = "แพทย์:";
            this.btnRpt1.UseVisualStyleBackColor = true;
            // 
            // btnRptPrint
            // 
            this.btnRptPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnRptPrint.Location = new System.Drawing.Point(337, 65);
            this.btnRptPrint.Name = "btnRptPrint";
            this.btnRptPrint.Size = new System.Drawing.Size(80, 31);
            this.btnRptPrint.TabIndex = 113;
            this.btnRptPrint.Text = "ดึงข้อมูล";
            this.btnRptPrint.UseVisualStyleBackColor = true;
            // 
            // txtRptEndDate
            // 
            this.txtRptEndDate.AllowSpinLoop = false;
            this.txtRptEndDate.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.txtRptEndDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtRptEndDate.CurrentTimeZone = false;
            this.txtRptEndDate.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            this.txtRptEndDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtRptEndDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtRptEndDate.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtRptEndDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtRptEndDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtRptEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtRptEndDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtRptEndDate.GMTOffset = System.TimeSpan.Parse("07:00:00");
            this.txtRptEndDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtRptEndDate.Location = new System.Drawing.Point(77, 42);
            this.txtRptEndDate.Name = "txtRptEndDate";
            this.txtRptEndDate.Size = new System.Drawing.Size(128, 27);
            this.txtRptEndDate.TabIndex = 90;
            this.txtRptEndDate.Tag = null;
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label84.Location = new System.Drawing.Point(8, 48);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(51, 20);
            this.label84.TabIndex = 89;
            this.label84.Text = "ถึงวันที่";
            // 
            // txtRptStartDate
            // 
            this.txtRptStartDate.AllowSpinLoop = false;
            this.txtRptStartDate.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.txtRptStartDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtRptStartDate.CurrentTimeZone = false;
            this.txtRptStartDate.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            this.txtRptStartDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtRptStartDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtRptStartDate.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtRptStartDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtRptStartDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtRptStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtRptStartDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtRptStartDate.GMTOffset = System.TimeSpan.Parse("07:00:00");
            this.txtRptStartDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtRptStartDate.Location = new System.Drawing.Point(77, 9);
            this.txtRptStartDate.Name = "txtRptStartDate";
            this.txtRptStartDate.Size = new System.Drawing.Size(128, 27);
            this.txtRptStartDate.TabIndex = 88;
            this.txtRptStartDate.Tag = null;
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label78.Location = new System.Drawing.Point(8, 15);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(35, 20);
            this.label78.TabIndex = 87;
            this.label78.Text = "วันที่";
            // 
            // pnRptCri1
            // 
            this.pnRptCri1.Location = new System.Drawing.Point(89, 200);
            this.pnRptCri1.Name = "pnRptCri1";
            this.pnRptCri1.Size = new System.Drawing.Size(200, 100);
            this.pnRptCri1.TabIndex = 1;
            // 
            // tabFinish
            // 
            this.tabFinish.Controls.Add(this.pnFinish);
            this.tabFinish.Location = new System.Drawing.Point(1, 26);
            this.tabFinish.Name = "tabFinish";
            this.tabFinish.Size = new System.Drawing.Size(1192, 635);
            this.tabFinish.TabIndex = 4;
            this.tabFinish.Text = "Finish";
            this.tabFinish.ToolTipText = "ดึงข้อมูลตาม ว.แพทย์ของวันนี้??";
            // 
            // pnFinish
            // 
            this.pnFinish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnFinish.Location = new System.Drawing.Point(0, 0);
            this.pnFinish.Name = "pnFinish";
            this.pnFinish.Size = new System.Drawing.Size(1192, 635);
            this.pnFinish.TabIndex = 0;
            // 
            // FrmDoctor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 780);
            this.Controls.Add(this.spDoctor);
            this.Controls.Add(this.c1StatusBar1);
            this.Name = "FrmDoctor";
            this.Text = "FrmDoctor2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmDoctor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDoctor)).EndInit();
            this.spDoctor.ResumeLayout(false);
            this.spTop.ResumeLayout(false);
            this.pnTop.ResumeLayout(false);
            this.pnTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDfDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate)).EndInit();
            this.spMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tC1)).EndInit();
            this.tC1.ResumeLayout(false);
            this.tabQue.ResumeLayout(false);
            this.tabApm.ResumeLayout(false);
            this.tabIPD.ResumeLayout(false);
            this.tabRpt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spRpt)).EndInit();
            this.spRpt.ResumeLayout(false);
            this.c1SplitterPanel8.ResumeLayout(false);
            this.pnRpt.ResumeLayout(false);
            this.c1SplitterPanel5.ResumeLayout(false);
            this.c1SplitterPanel18.ResumeLayout(false);
            this.pnRptCriDate.ResumeLayout(false);
            this.pnRptCriDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboRpt2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRpt1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRpt1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRptPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRptEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRptStartDate)).EndInit();
            this.tabFinish.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Ribbon.RibbonLabel lfSbMessage;
        private C1.Win.C1Ribbon.RibbonLabel lfSbStation;
        private C1.Win.C1Ribbon.RibbonLabel rgSbModule;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabel4;
        private C1.Win.C1SplitContainer.C1SplitContainer spDoctor;
        private C1.Win.C1SplitContainer.C1SplitterPanel spTop;
        private System.Windows.Forms.Panel pnTop;
        private C1.Win.C1Input.C1TextBox txtHN;
        private C1.Win.Calendar.C1DateEdit txtDate;
        private System.Windows.Forms.Label lbDtrName;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1SplitContainer.C1SplitterPanel spMain;
        private C1.Win.C1Command.C1DockingTab tC1;
        private C1.Win.C1Command.C1DockingTabPage tabQue;
        private System.Windows.Forms.Panel pnQue;
        private C1.Win.C1Command.C1DockingTabPage tabApm;
        private System.Windows.Forms.Panel pnApm;
        private C1.Win.C1Command.C1DockingTabPage tabIPD;
        private System.Windows.Forms.Panel pnIPD;
        private C1.Win.C1Command.C1DockingTabPage tabRpt;
        private C1.Win.C1SplitContainer.C1SplitContainer spRpt;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel8;
        private System.Windows.Forms.Panel pnRpt;
        private GrapeCity.ActiveReports.Viewer.Win.Viewer arvMain;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel5;
        private System.Windows.Forms.Panel pnRptName;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel18;
        private System.Windows.Forms.Panel pnRptCriDate;
        private System.Windows.Forms.Label label85;
        private C1.Win.C1Input.C1ComboBox cboRpt2;
        private C1.Win.C1Input.C1ComboBox cboRpt1;
        private C1.Win.C1Input.C1Button btnRpt1;
        private C1.Win.C1Input.C1Button btnRptPrint;
        private C1.Win.Calendar.C1DateEdit txtRptEndDate;
        private System.Windows.Forms.Label label84;
        private C1.Win.Calendar.C1DateEdit txtRptStartDate;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Panel pnRptCri1;
        private C1.Win.C1Ribbon.RibbonButton rbSbDrugSet;
        private C1.Win.C1Ribbon.RibbonButton rpDrugSetNew;
        private C1.Win.C1Command.C1DockingTabPage tabFinish;
        private System.Windows.Forms.Panel pnFinish;
        private C1.Win.C1Input.C1Button btnDfDate;
        private C1.Win.C1Ribbon.RibbonLabel lfSbLastUpdate;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator1;
        private C1.Win.C1Ribbon.RibbonButton rbToken;
    }
}