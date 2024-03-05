namespace bangna_hospital.gui
{
    partial class FrmOrder
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
            this.lfSbMessage = new C1.Win.C1Ribbon.RibbonLabel();
            this.lfSbStation = new C1.Win.C1Ribbon.RibbonLabel();
            this.rgSbModule = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonLabel4 = new C1.Win.C1Ribbon.RibbonLabel();
            this.c1DockingTab1 = new C1.Win.C1Command.C1DockingTab();
            this.c1DockingTabPage1 = new C1.Win.C1Command.C1DockingTabPage();
            this.c1SplitContainer1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.spPtt = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.spDrugAllergy = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.spChronic = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.spItems = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.lbPttAttachNote = new System.Windows.Forms.Label();
            this.txtPttHN = new C1.Win.C1Input.C1TextBox();
            this.label68 = new System.Windows.Forms.Label();
            this.lbPttNameT = new System.Windows.Forms.Label();
            this.pnDrugAllergy = new System.Windows.Forms.Panel();
            this.pnChronic = new System.Windows.Forms.Panel();
            this.spOrders = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.c1TextBox2 = new C1.Win.C1Input.C1TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.c1TextBox1 = new C1.Win.C1Input.C1TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOrderSubmit = new C1.Win.C1Input.C1Button();
            this.btnOrderSave = new C1.Win.C1Input.C1Button();
            this.btnItemAdd = new C1.Win.C1Input.C1Button();
            this.txtItemRemark = new C1.Win.C1Input.C1TextBox();
            this.label82 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.txtItemQTY = new C1.Win.C1Input.C1TextBox();
            this.lbItemName = new System.Windows.Forms.Label();
            this.txtItemCode = new C1.Win.C1Input.C1TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.btnOperItemSearch = new C1.Win.C1Input.C1Button();
            this.txtSearchItem = new C1.Win.C1Input.C1TextBox();
            this.chkItemProcedure = new System.Windows.Forms.RadioButton();
            this.chkItemDrug = new System.Windows.Forms.RadioButton();
            this.chkItemXray = new System.Windows.Forms.RadioButton();
            this.chkItemLab = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).BeginInit();
            this.c1DockingTab1.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).BeginInit();
            this.c1SplitContainer1.SuspendLayout();
            this.spPtt.SuspendLayout();
            this.spDrugAllergy.SuspendLayout();
            this.spChronic.SuspendLayout();
            this.spItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPttHN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TextBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOrderSubmit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOrderSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnItemAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemQTY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOperItemSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchItem)).BeginInit();
            this.SuspendLayout();
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1StatusBar1.LeftPaneItems.Add(this.lfSbMessage);
            this.c1StatusBar1.LeftPaneItems.Add(this.lfSbStation);
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 801);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.rgSbModule);
            this.c1StatusBar1.RightPaneItems.Add(this.ribbonLabel4);
            this.c1StatusBar1.Size = new System.Drawing.Size(1023, 22);
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
            // c1DockingTab1
            // 
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage1);
            this.c1DockingTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1DockingTab1.Location = new System.Drawing.Point(0, 0);
            this.c1DockingTab1.Name = "c1DockingTab1";
            this.c1DockingTab1.Size = new System.Drawing.Size(1023, 801);
            this.c1DockingTab1.TabIndex = 2;
            this.c1DockingTab1.TabsSpacing = 5;
            // 
            // c1DockingTabPage1
            // 
            this.c1DockingTabPage1.Controls.Add(this.c1SplitContainer1);
            this.c1DockingTabPage1.Location = new System.Drawing.Point(1, 24);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(1021, 776);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Order";
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
            this.c1SplitContainer1.Panels.Add(this.spOrders);
            this.c1SplitContainer1.Panels.Add(this.spItems);
            this.c1SplitContainer1.Panels.Add(this.spPtt);
            this.c1SplitContainer1.Panels.Add(this.spDrugAllergy);
            this.c1SplitContainer1.Panels.Add(this.spChronic);
            this.c1SplitContainer1.Size = new System.Drawing.Size(1021, 776);
            this.c1SplitContainer1.TabIndex = 0;
            // 
            // spPtt
            // 
            this.spPtt.Controls.Add(this.lbPttAttachNote);
            this.spPtt.Controls.Add(this.txtPttHN);
            this.spPtt.Controls.Add(this.label68);
            this.spPtt.Controls.Add(this.lbPttNameT);
            this.spPtt.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.spPtt.Height = 115;
            this.spPtt.Location = new System.Drawing.Point(0, 21);
            this.spPtt.Name = "spPtt";
            this.spPtt.Size = new System.Drawing.Size(293, 94);
            this.spPtt.SizeRatio = 28.81D;
            this.spPtt.TabIndex = 0;
            this.spPtt.Text = "Patient";
            this.spPtt.Width = 293;
            // 
            // spDrugAllergy
            // 
            this.spDrugAllergy.Controls.Add(this.pnDrugAllergy);
            this.spDrugAllergy.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.spDrugAllergy.Location = new System.Drawing.Point(297, 21);
            this.spDrugAllergy.Name = "spDrugAllergy";
            this.spDrugAllergy.Size = new System.Drawing.Size(317, 94);
            this.spDrugAllergy.SizeRatio = 44.028D;
            this.spDrugAllergy.TabIndex = 1;
            this.spDrugAllergy.Text = "Drug Allergy";
            this.spDrugAllergy.Width = 317;
            // 
            // spChronic
            // 
            this.spChronic.Collapsible = true;
            this.spChronic.Controls.Add(this.pnChronic);
            this.spChronic.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            this.spChronic.Location = new System.Drawing.Point(618, 21);
            this.spChronic.Name = "spChronic";
            this.spChronic.Size = new System.Drawing.Size(403, 94);
            this.spChronic.TabIndex = 2;
            this.spChronic.Text = "Chronic";
            this.spChronic.Width = 403;
            // 
            // spItems
            // 
            this.spItems.Controls.Add(this.chkItemProcedure);
            this.spItems.Controls.Add(this.chkItemDrug);
            this.spItems.Controls.Add(this.chkItemXray);
            this.spItems.Controls.Add(this.chkItemLab);
            this.spItems.Controls.Add(this.c1TextBox2);
            this.spItems.Controls.Add(this.label2);
            this.spItems.Controls.Add(this.c1TextBox1);
            this.spItems.Controls.Add(this.label1);
            this.spItems.Controls.Add(this.btnOrderSubmit);
            this.spItems.Controls.Add(this.btnOrderSave);
            this.spItems.Controls.Add(this.btnItemAdd);
            this.spItems.Controls.Add(this.txtItemRemark);
            this.spItems.Controls.Add(this.label82);
            this.spItems.Controls.Add(this.label81);
            this.spItems.Controls.Add(this.txtItemQTY);
            this.spItems.Controls.Add(this.lbItemName);
            this.spItems.Controls.Add(this.txtItemCode);
            this.spItems.Controls.Add(this.label79);
            this.spItems.Controls.Add(this.btnOperItemSearch);
            this.spItems.Controls.Add(this.txtSearchItem);
            this.spItems.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Bottom;
            this.spItems.Height = 267;
            this.spItems.Location = new System.Drawing.Point(0, 140);
            this.spItems.Name = "spItems";
            this.spItems.Size = new System.Drawing.Size(1021, 246);
            this.spItems.SizeRatio = 69.819D;
            this.spItems.TabIndex = 3;
            this.spItems.Text = "Items";
            // 
            // lbPttAttachNote
            // 
            this.lbPttAttachNote.AutoSize = true;
            this.lbPttAttachNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbPttAttachNote.Location = new System.Drawing.Point(3, 66);
            this.lbPttAttachNote.Name = "lbPttAttachNote";
            this.lbPttAttachNote.Size = new System.Drawing.Size(106, 20);
            this.lbPttAttachNote.TabIndex = 281;
            this.lbPttAttachNote.Text = "attach note ...";
            // 
            // txtPttHN
            // 
            this.txtPttHN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPttHN.Location = new System.Drawing.Point(37, 3);
            this.txtPttHN.Name = "txtPttHN";
            this.txtPttHN.ReadOnly = true;
            this.txtPttHN.Size = new System.Drawing.Size(128, 27);
            this.txtPttHN.TabIndex = 279;
            this.txtPttHN.TabStop = false;
            this.txtPttHN.Tag = null;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label68.Location = new System.Drawing.Point(3, 6);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(32, 20);
            this.label68.TabIndex = 278;
            this.label68.Text = "HN";
            // 
            // lbPttNameT
            // 
            this.lbPttNameT.AutoSize = true;
            this.lbPttNameT.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbPttNameT.Location = new System.Drawing.Point(2, 33);
            this.lbPttNameT.Name = "lbPttNameT";
            this.lbPttNameT.Size = new System.Drawing.Size(35, 25);
            this.lbPttNameT.TabIndex = 280;
            this.lbPttNameT.Text = "ชื่อ";
            // 
            // pnDrugAllergy
            // 
            this.pnDrugAllergy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDrugAllergy.Location = new System.Drawing.Point(0, 0);
            this.pnDrugAllergy.Name = "pnDrugAllergy";
            this.pnDrugAllergy.Size = new System.Drawing.Size(317, 94);
            this.pnDrugAllergy.TabIndex = 1;
            // 
            // pnChronic
            // 
            this.pnChronic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnChronic.Location = new System.Drawing.Point(0, 0);
            this.pnChronic.Name = "pnChronic";
            this.pnChronic.Size = new System.Drawing.Size(403, 94);
            this.pnChronic.TabIndex = 1;
            // 
            // spOrders
            // 
            this.spOrders.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Bottom;
            this.spOrders.Height = 386;
            this.spOrders.Location = new System.Drawing.Point(0, 411);
            this.spOrders.Name = "spOrders";
            this.spOrders.Size = new System.Drawing.Size(1021, 365);
            this.spOrders.TabIndex = 4;
            this.spOrders.Text = "Orders";
            // 
            // c1TextBox2
            // 
            this.c1TextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.c1TextBox2.Location = new System.Drawing.Point(62, 150);
            this.c1TextBox2.Name = "c1TextBox2";
            this.c1TextBox2.Size = new System.Drawing.Size(522, 24);
            this.c1TextBox2.TabIndex = 297;
            this.c1TextBox2.Tag = null;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(5, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 296;
            this.label2.Text = "ข้อบ่งชี้";
            // 
            // c1TextBox1
            // 
            this.c1TextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.c1TextBox1.Location = new System.Drawing.Point(62, 120);
            this.c1TextBox1.Name = "c1TextBox1";
            this.c1TextBox1.Size = new System.Drawing.Size(522, 24);
            this.c1TextBox1.TabIndex = 295;
            this.c1TextBox1.Tag = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(5, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 294;
            this.label1.Text = "วิธีใช้";
            // 
            // btnOrderSubmit
            // 
            this.btnOrderSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOrderSubmit.Location = new System.Drawing.Point(191, 175);
            this.btnOrderSubmit.Name = "btnOrderSubmit";
            this.btnOrderSubmit.Size = new System.Drawing.Size(72, 33);
            this.btnOrderSubmit.TabIndex = 293;
            this.btnOrderSubmit.Text = "submit";
            this.btnOrderSubmit.UseVisualStyleBackColor = true;
            // 
            // btnOrderSave
            // 
            this.btnOrderSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOrderSave.Location = new System.Drawing.Point(62, 175);
            this.btnOrderSave.Name = "btnOrderSave";
            this.btnOrderSave.Size = new System.Drawing.Size(72, 33);
            this.btnOrderSave.TabIndex = 292;
            this.btnOrderSave.Text = "save";
            this.btnOrderSave.UseVisualStyleBackColor = true;
            // 
            // btnItemAdd
            // 
            this.btnItemAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnItemAdd.Location = new System.Drawing.Point(553, 88);
            this.btnItemAdd.Name = "btnItemAdd";
            this.btnItemAdd.Size = new System.Drawing.Size(30, 28);
            this.btnItemAdd.TabIndex = 291;
            this.btnItemAdd.Text = "+";
            this.btnItemAdd.UseVisualStyleBackColor = true;
            // 
            // txtItemRemark
            // 
            this.txtItemRemark.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtItemRemark.Location = new System.Drawing.Point(84, 90);
            this.txtItemRemark.Name = "txtItemRemark";
            this.txtItemRemark.Size = new System.Drawing.Size(467, 24);
            this.txtItemRemark.TabIndex = 290;
            this.txtItemRemark.Tag = null;
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label82.Location = new System.Drawing.Point(5, 91);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(75, 20);
            this.label82.TabIndex = 289;
            this.label82.Text = "หมายเหตุ :";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label81.Location = new System.Drawing.Point(504, 183);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(30, 20);
            this.label81.TabIndex = 288;
            this.label81.Text = "จน.";
            // 
            // txtItemQTY
            // 
            this.txtItemQTY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtItemQTY.Location = new System.Drawing.Point(536, 180);
            this.txtItemQTY.Name = "txtItemQTY";
            this.txtItemQTY.Size = new System.Drawing.Size(47, 24);
            this.txtItemQTY.TabIndex = 287;
            this.txtItemQTY.Tag = null;
            // 
            // lbItemName
            // 
            this.lbItemName.AutoSize = true;
            this.lbItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbItemName.Location = new System.Drawing.Point(149, 61);
            this.lbItemName.Name = "lbItemName";
            this.lbItemName.Size = new System.Drawing.Size(30, 25);
            this.lbItemName.TabIndex = 286;
            this.lbItemName.Text = "...";
            // 
            // txtItemCode
            // 
            this.txtItemCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtItemCode.Location = new System.Drawing.Point(62, 61);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(83, 24);
            this.txtItemCode.TabIndex = 285;
            this.txtItemCode.Tag = null;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label79.Location = new System.Drawing.Point(5, 62);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(47, 20);
            this.label79.TabIndex = 284;
            this.label79.Text = "item :";
            // 
            // btnOperItemSearch
            // 
            this.btnOperItemSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOperItemSearch.Location = new System.Drawing.Point(3, 31);
            this.btnOperItemSearch.Name = "btnOperItemSearch";
            this.btnOperItemSearch.Size = new System.Drawing.Size(55, 28);
            this.btnOperItemSearch.TabIndex = 283;
            this.btnOperItemSearch.Text = "ค้นหา:";
            this.btnOperItemSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearchItem
            // 
            this.txtSearchItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSearchItem.Location = new System.Drawing.Point(62, 34);
            this.txtSearchItem.Name = "txtSearchItem";
            this.txtSearchItem.Size = new System.Drawing.Size(455, 24);
            this.txtSearchItem.TabIndex = 282;
            this.txtSearchItem.Tag = null;
            // 
            // chkItemProcedure
            // 
            this.chkItemProcedure.AutoSize = true;
            this.chkItemProcedure.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItemProcedure.Location = new System.Drawing.Point(281, 4);
            this.chkItemProcedure.Name = "chkItemProcedure";
            this.chkItemProcedure.Size = new System.Drawing.Size(77, 24);
            this.chkItemProcedure.TabIndex = 301;
            this.chkItemProcedure.TabStop = true;
            this.chkItemProcedure.Text = "หัตถการ";
            this.chkItemProcedure.UseVisualStyleBackColor = true;
            // 
            // chkItemDrug
            // 
            this.chkItemDrug.AutoSize = true;
            this.chkItemDrug.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItemDrug.Location = new System.Drawing.Point(440, 4);
            this.chkItemDrug.Name = "chkItemDrug";
            this.chkItemDrug.Size = new System.Drawing.Size(76, 24);
            this.chkItemDrug.TabIndex = 300;
            this.chkItemDrug.TabStop = true;
            this.chkItemDrug.Text = "DRUG";
            this.chkItemDrug.UseVisualStyleBackColor = true;
            // 
            // chkItemXray
            // 
            this.chkItemXray.AutoSize = true;
            this.chkItemXray.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItemXray.Location = new System.Drawing.Point(148, 4);
            this.chkItemXray.Name = "chkItemXray";
            this.chkItemXray.Size = new System.Drawing.Size(72, 24);
            this.chkItemXray.TabIndex = 299;
            this.chkItemXray.TabStop = true;
            this.chkItemXray.Text = "XRAY";
            this.chkItemXray.UseVisualStyleBackColor = true;
            // 
            // chkItemLab
            // 
            this.chkItemLab.AutoSize = true;
            this.chkItemLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkItemLab.Location = new System.Drawing.Point(25, 4);
            this.chkItemLab.Name = "chkItemLab";
            this.chkItemLab.Size = new System.Drawing.Size(58, 24);
            this.chkItemLab.TabIndex = 298;
            this.chkItemLab.TabStop = true;
            this.chkItemLab.Text = "LAB";
            this.chkItemLab.UseVisualStyleBackColor = true;
            // 
            // FrmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 823);
            this.Controls.Add(this.c1DockingTab1);
            this.Controls.Add(this.c1StatusBar1);
            this.Name = "FrmOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmOrder";
            this.Load += new System.EventHandler(this.FrmOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).EndInit();
            this.c1DockingTab1.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).EndInit();
            this.c1SplitContainer1.ResumeLayout(false);
            this.spPtt.ResumeLayout(false);
            this.spPtt.PerformLayout();
            this.spDrugAllergy.ResumeLayout(false);
            this.spChronic.ResumeLayout(false);
            this.spItems.ResumeLayout(false);
            this.spItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPttHN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TextBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOrderSubmit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOrderSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnItemAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemQTY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOperItemSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Ribbon.RibbonLabel lfSbMessage;
        private C1.Win.C1Ribbon.RibbonLabel lfSbStation;
        private C1.Win.C1Ribbon.RibbonLabel rgSbModule;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabel4;
        private C1.Win.C1Command.C1DockingTab c1DockingTab1;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage1;
        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer1;
        private C1.Win.C1SplitContainer.C1SplitterPanel spPtt;
        private C1.Win.C1SplitContainer.C1SplitterPanel spDrugAllergy;
        private C1.Win.C1SplitContainer.C1SplitterPanel spChronic;
        private C1.Win.C1SplitContainer.C1SplitterPanel spItems;
        private System.Windows.Forms.Label lbPttAttachNote;
        private C1.Win.C1Input.C1TextBox txtPttHN;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label lbPttNameT;
        private System.Windows.Forms.Panel pnDrugAllergy;
        private System.Windows.Forms.Panel pnChronic;
        private C1.Win.C1SplitContainer.C1SplitterPanel spOrders;
        private C1.Win.C1Input.C1TextBox c1TextBox2;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1TextBox c1TextBox1;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1Button btnOrderSubmit;
        private C1.Win.C1Input.C1Button btnOrderSave;
        private C1.Win.C1Input.C1Button btnItemAdd;
        private C1.Win.C1Input.C1TextBox txtItemRemark;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label81;
        private C1.Win.C1Input.C1TextBox txtItemQTY;
        private System.Windows.Forms.Label lbItemName;
        private C1.Win.C1Input.C1TextBox txtItemCode;
        private System.Windows.Forms.Label label79;
        private C1.Win.C1Input.C1Button btnOperItemSearch;
        private C1.Win.C1Input.C1TextBox txtSearchItem;
        private System.Windows.Forms.RadioButton chkItemProcedure;
        private System.Windows.Forms.RadioButton chkItemDrug;
        private System.Windows.Forms.RadioButton chkItemXray;
        private System.Windows.Forms.RadioButton chkItemLab;
    }
}