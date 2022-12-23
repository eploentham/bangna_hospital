namespace bangna_hospital.gui
{
    partial class FrmDocGroupFm
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
            this.sB = new C1.Win.C1Ribbon.C1StatusBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1SplitContainer1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkStatusDocOffice = new C1.Win.C1Input.C1CheckBox();
            this.ChkStatusDocAdmision = new C1.Win.C1Input.C1CheckBox();
            this.ChkStatusDocNurse = new C1.Win.C1Input.C1CheckBox();
            this.chkStatusDocMedical = new C1.Win.C1Input.C1CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDocGroupName = new C1.Win.C1Input.C1ComboBox();
            this.txtFmName = new C1.Win.C1Input.C1TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDocGroupSubName = new C1.Win.C1Input.C1ComboBox();
            this.txtPasswordVoid = new C1.Win.C1Input.C1TextBox();
            this.btnNew = new C1.Win.C1Input.C1Button();
            this.chkVoid = new C1.Win.C1Input.C1CheckBox();
            this.btnVoid = new C1.Win.C1Input.C1Button();
            this.btnEdit = new C1.Win.C1Input.C1Button();
            this.btnSave = new C1.Win.C1Input.C1Button();
            this.txtFmCode = new C1.Win.C1Input.C1TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID = new C1.Win.C1Input.C1TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sB)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).BeginInit();
            this.c1SplitContainer1.SuspendLayout();
            this.c1SplitterPanel1.SuspendLayout();
            this.c1SplitterPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkStatusDocOffice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkStatusDocAdmision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkStatusDocNurse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStatusDocMedical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDocGroupName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFmName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDocGroupSubName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordVoid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVoid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVoid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFmCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID)).BeginInit();
            this.SuspendLayout();
            // 
            // theme1
            // 
            this.theme1.Theme = "Office2013Red";
            // 
            // sB
            // 
            this.sB.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.sB.Location = new System.Drawing.Point(0, 638);
            this.sB.Name = "sB";
            this.sB.Size = new System.Drawing.Size(1214, 22);
            this.theme1.SetTheme(this.sB, "(default)");
            this.sB.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Custom;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.c1SplitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1214, 638);
            this.panel1.TabIndex = 1;
            this.theme1.SetTheme(this.panel1, "(default)");
            // 
            // c1SplitContainer1
            // 
            this.c1SplitContainer1.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.c1SplitContainer1.BackColor = System.Drawing.Color.White;
            this.c1SplitContainer1.CollapsingAreaColor = System.Drawing.Color.White;
            this.c1SplitContainer1.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.c1SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SplitContainer1.FixedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(189)))), ((int)(((byte)(182)))));
            this.c1SplitContainer1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.c1SplitContainer1.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.c1SplitContainer1.HeaderLineWidth = 1;
            this.c1SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.c1SplitContainer1.Name = "c1SplitContainer1";
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel1);
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel2);
            this.c1SplitContainer1.Size = new System.Drawing.Size(1214, 638);
            this.c1SplitContainer1.SplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(189)))), ((int)(((byte)(182)))));
            this.c1SplitContainer1.SplitterMovingColor = System.Drawing.Color.Black;
            this.c1SplitContainer1.TabIndex = 0;
            this.theme1.SetTheme(this.c1SplitContainer1, "(default)");
            this.c1SplitContainer1.UseParentVisualStyle = false;
            // 
            // c1SplitterPanel1
            // 
            this.c1SplitterPanel1.Collapsible = true;
            this.c1SplitterPanel1.Controls.Add(this.panel2);
            this.c1SplitterPanel1.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1SplitterPanel1.Location = new System.Drawing.Point(0, 21);
            this.c1SplitterPanel1.Name = "c1SplitterPanel1";
            this.c1SplitterPanel1.Size = new System.Drawing.Size(481, 617);
            this.c1SplitterPanel1.SizeRatio = 40.331D;
            this.c1SplitterPanel1.TabIndex = 0;
            this.c1SplitterPanel1.Text = "Panel 1";
            this.c1SplitterPanel1.Width = 488;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(481, 617);
            this.panel2.TabIndex = 0;
            this.theme1.SetTheme(this.panel2, "(default)");
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.Controls.Add(this.panel3);
            this.c1SplitterPanel2.Height = 638;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(492, 21);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(722, 617);
            this.c1SplitterPanel2.TabIndex = 1;
            this.c1SplitterPanel2.Text = "Panel 2";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.cboDocGroupName);
            this.panel3.Controls.Add(this.txtFmName);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cboDocGroupSubName);
            this.panel3.Controls.Add(this.txtPasswordVoid);
            this.panel3.Controls.Add(this.btnNew);
            this.panel3.Controls.Add(this.chkVoid);
            this.panel3.Controls.Add(this.btnVoid);
            this.panel3.Controls.Add(this.btnEdit);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.txtFmCode);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtID);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(722, 617);
            this.panel3.TabIndex = 0;
            this.theme1.SetTheme(this.panel3, "(default)");
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox1.Controls.Add(this.chkStatusDocOffice);
            this.groupBox1.Controls.Add(this.ChkStatusDocAdmision);
            this.groupBox1.Controls.Add(this.ChkStatusDocNurse);
            this.groupBox1.Controls.Add(this.chkStatusDocMedical);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.groupBox1.Location = new System.Drawing.Point(130, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 147);
            this.groupBox1.TabIndex = 565;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ประเภทเอกสาร";
            this.theme1.SetTheme(this.groupBox1, "(default)");
            // 
            // chkStatusDocOffice
            // 
            this.chkStatusDocOffice.BackColor = System.Drawing.Color.Transparent;
            this.chkStatusDocOffice.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.chkStatusDocOffice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkStatusDocOffice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkStatusDocOffice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.chkStatusDocOffice.Location = new System.Drawing.Point(6, 109);
            this.chkStatusDocOffice.Name = "chkStatusDocOffice";
            this.chkStatusDocOffice.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.chkStatusDocOffice.Size = new System.Drawing.Size(155, 24);
            this.chkStatusDocOffice.TabIndex = 560;
            this.chkStatusDocOffice.Text = "เอกสารทางการ ธุรการ";
            this.theme1.SetTheme(this.chkStatusDocOffice, "(default)");
            this.chkStatusDocOffice.UseVisualStyleBackColor = true;
            this.chkStatusDocOffice.Value = null;
            this.chkStatusDocOffice.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // ChkStatusDocAdmision
            // 
            this.ChkStatusDocAdmision.BackColor = System.Drawing.Color.Transparent;
            this.ChkStatusDocAdmision.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.ChkStatusDocAdmision.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChkStatusDocAdmision.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkStatusDocAdmision.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ChkStatusDocAdmision.Location = new System.Drawing.Point(6, 79);
            this.ChkStatusDocAdmision.Name = "ChkStatusDocAdmision";
            this.ChkStatusDocAdmision.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.ChkStatusDocAdmision.Size = new System.Drawing.Size(155, 24);
            this.ChkStatusDocAdmision.TabIndex = 559;
            this.ChkStatusDocAdmision.Text = "เอกสาร adminsion";
            this.theme1.SetTheme(this.ChkStatusDocAdmision, "(default)");
            this.ChkStatusDocAdmision.UseVisualStyleBackColor = true;
            this.ChkStatusDocAdmision.Value = null;
            this.ChkStatusDocAdmision.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // ChkStatusDocNurse
            // 
            this.ChkStatusDocNurse.BackColor = System.Drawing.Color.Transparent;
            this.ChkStatusDocNurse.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.ChkStatusDocNurse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChkStatusDocNurse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkStatusDocNurse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ChkStatusDocNurse.Location = new System.Drawing.Point(6, 49);
            this.ChkStatusDocNurse.Name = "ChkStatusDocNurse";
            this.ChkStatusDocNurse.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.ChkStatusDocNurse.Size = new System.Drawing.Size(155, 24);
            this.ChkStatusDocNurse.TabIndex = 558;
            this.ChkStatusDocNurse.Text = "เอกสารทางการ พยาบาล";
            this.theme1.SetTheme(this.ChkStatusDocNurse, "(default)");
            this.ChkStatusDocNurse.UseVisualStyleBackColor = true;
            this.ChkStatusDocNurse.Value = null;
            this.ChkStatusDocNurse.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkStatusDocMedical
            // 
            this.chkStatusDocMedical.BackColor = System.Drawing.Color.Transparent;
            this.chkStatusDocMedical.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.chkStatusDocMedical.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkStatusDocMedical.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkStatusDocMedical.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.chkStatusDocMedical.Location = new System.Drawing.Point(6, 19);
            this.chkStatusDocMedical.Name = "chkStatusDocMedical";
            this.chkStatusDocMedical.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.chkStatusDocMedical.Size = new System.Drawing.Size(155, 24);
            this.chkStatusDocMedical.TabIndex = 557;
            this.chkStatusDocMedical.Text = "เอกสารทางการ แพทย์ ";
            this.theme1.SetTheme(this.chkStatusDocMedical, "(default)");
            this.chkStatusDocMedical.UseVisualStyleBackColor = true;
            this.chkStatusDocMedical.Value = null;
            this.chkStatusDocMedical.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label4.Location = new System.Drawing.Point(33, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 564;
            this.label4.Text = "กลุ่มเอกสาร :";
            this.theme1.SetTheme(this.label4, "(default)");
            // 
            // cboDocGroupName
            // 
            this.cboDocGroupName.AllowSpinLoop = false;
            this.cboDocGroupName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDocGroupName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboDocGroupName.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.cboDocGroupName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboDocGroupName.GapHeight = 0;
            this.cboDocGroupName.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboDocGroupName.ItemsDisplayMember = "";
            this.cboDocGroupName.ItemsValueMember = "";
            this.cboDocGroupName.Location = new System.Drawing.Point(130, 23);
            this.cboDocGroupName.Name = "cboDocGroupName";
            this.cboDocGroupName.Size = new System.Drawing.Size(262, 20);
            this.cboDocGroupName.Style.DropDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cboDocGroupName.Style.DropDownBorderColor = System.Drawing.Color.Gainsboro;
            this.cboDocGroupName.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboDocGroupName.TabIndex = 563;
            this.cboDocGroupName.Tag = null;
            this.theme1.SetTheme(this.cboDocGroupName, "(default)");
            this.cboDocGroupName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // txtFmName
            // 
            this.txtFmName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFmName.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtFmName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtFmName.Location = new System.Drawing.Point(130, 101);
            this.txtFmName.Name = "txtFmName";
            this.txtFmName.Size = new System.Drawing.Size(262, 20);
            this.txtFmName.TabIndex = 562;
            this.txtFmName.Tag = null;
            this.theme1.SetTheme(this.txtFmName, "(default)");
            this.txtFmName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label3.Location = new System.Drawing.Point(33, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 561;
            this.label3.Text = "FM Name";
            this.theme1.SetTheme(this.label3, "(default)");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label1.Location = new System.Drawing.Point(33, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 560;
            this.label1.Text = "กลุ่มย่อยเอกสาร :";
            this.theme1.SetTheme(this.label1, "(default)");
            // 
            // cboDocGroupSubName
            // 
            this.cboDocGroupSubName.AllowSpinLoop = false;
            this.cboDocGroupSubName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDocGroupSubName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboDocGroupSubName.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.cboDocGroupSubName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboDocGroupSubName.GapHeight = 0;
            this.cboDocGroupSubName.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboDocGroupSubName.ItemsDisplayMember = "";
            this.cboDocGroupSubName.ItemsValueMember = "";
            this.cboDocGroupSubName.Location = new System.Drawing.Point(130, 49);
            this.cboDocGroupSubName.Name = "cboDocGroupSubName";
            this.cboDocGroupSubName.Size = new System.Drawing.Size(262, 20);
            this.cboDocGroupSubName.Style.DropDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cboDocGroupSubName.Style.DropDownBorderColor = System.Drawing.Color.Gainsboro;
            this.cboDocGroupSubName.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboDocGroupSubName.TabIndex = 559;
            this.cboDocGroupSubName.Tag = null;
            this.theme1.SetTheme(this.cboDocGroupSubName, "(default)");
            this.cboDocGroupSubName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // txtPasswordVoid
            // 
            this.txtPasswordVoid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPasswordVoid.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtPasswordVoid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPasswordVoid.Location = new System.Drawing.Point(271, 358);
            this.txtPasswordVoid.Name = "txtPasswordVoid";
            this.txtPasswordVoid.PasswordChar = '*';
            this.txtPasswordVoid.Size = new System.Drawing.Size(78, 20);
            this.txtPasswordVoid.TabIndex = 558;
            this.txtPasswordVoid.Tag = null;
            this.theme1.SetTheme(this.txtPasswordVoid, "(default)");
            this.txtPasswordVoid.Visible = false;
            this.txtPasswordVoid.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnNew
            // 
            this.btnNew.Image = global::bangna_hospital.Properties.Resources.custom_reports24;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(467, 21);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(123, 39);
            this.btnNew.TabIndex = 557;
            this.btnNew.Text = "เพิ่มช้อมูล";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnNew, "(default)");
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkVoid
            // 
            this.chkVoid.BackColor = System.Drawing.Color.Transparent;
            this.chkVoid.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.chkVoid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkVoid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkVoid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.chkVoid.Location = new System.Drawing.Point(271, 329);
            this.chkVoid.Name = "chkVoid";
            this.chkVoid.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.chkVoid.Size = new System.Drawing.Size(155, 24);
            this.chkVoid.TabIndex = 556;
            this.chkVoid.Text = "ต้องการยกเลิกรายการ";
            this.theme1.SetTheme(this.chkVoid, "(default)");
            this.chkVoid.UseVisualStyleBackColor = true;
            this.chkVoid.Value = null;
            this.chkVoid.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnVoid
            // 
            this.btnVoid.Image = global::bangna_hospital.Properties.Resources.trash24;
            this.btnVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVoid.Location = new System.Drawing.Point(432, 340);
            this.btnVoid.Name = "btnVoid";
            this.btnVoid.Size = new System.Drawing.Size(123, 39);
            this.btnVoid.TabIndex = 555;
            this.btnVoid.Text = "ยกเลิกช้อมูล";
            this.btnVoid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnVoid, "(default)");
            this.btnVoid.UseVisualStyleBackColor = true;
            this.btnVoid.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::bangna_hospital.Properties.Resources.lock24;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(467, 115);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(123, 39);
            this.btnEdit.TabIndex = 554;
            this.btnEdit.Text = "แก้ไขช้อมูล";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnEdit, "(default)");
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::bangna_hospital.Properties.Resources.accept_database24;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(432, 384);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(123, 39);
            this.btnSave.TabIndex = 553;
            this.btnSave.Text = "บันทึกช้อมูล";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnSave, "(default)");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // txtFmCode
            // 
            this.txtFmCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFmCode.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtFmCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtFmCode.Location = new System.Drawing.Point(130, 75);
            this.txtFmCode.Name = "txtFmCode";
            this.txtFmCode.Size = new System.Drawing.Size(262, 20);
            this.txtFmCode.TabIndex = 552;
            this.txtFmCode.Tag = null;
            this.theme1.SetTheme(this.txtFmCode, "(default)");
            this.txtFmCode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label2.Location = new System.Drawing.Point(33, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 551;
            this.label2.Text = "FM Code";
            this.theme1.SetTheme(this.label2, "(default)");
            // 
            // txtID
            // 
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtID.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtID.Location = new System.Drawing.Point(343, 15);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(30, 20);
            this.txtID.TabIndex = 550;
            this.txtID.Tag = null;
            this.theme1.SetTheme(this.txtID, "(default)");
            this.txtID.Visible = false;
            this.txtID.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label5.Location = new System.Drawing.Point(398, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 16);
            this.label5.TabIndex = 566;
            this.label5.Text = "enter เพื่อcheck รหัส";
            this.theme1.SetTheme(this.label5, "(default)");
            // 
            // FrmDocGroupFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 660);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sB);
            this.Name = "FrmDocGroupFm";
            this.Text = "FrmDocGroupFm";
            this.Load += new System.EventHandler(this.FrmDocGroupFm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sB)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).EndInit();
            this.c1SplitContainer1.ResumeLayout(false);
            this.c1SplitterPanel1.ResumeLayout(false);
            this.c1SplitterPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkStatusDocOffice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkStatusDocAdmision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkStatusDocNurse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStatusDocMedical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDocGroupName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFmName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDocGroupSubName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordVoid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVoid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVoid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFmCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController theme1;
        private C1.Win.C1Ribbon.C1StatusBar sB;
        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel1;
        private System.Windows.Forms.Panel panel2;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1ComboBox cboDocGroupSubName;
        private C1.Win.C1Input.C1TextBox txtPasswordVoid;
        private C1.Win.C1Input.C1Button btnNew;
        private C1.Win.C1Input.C1CheckBox chkVoid;
        private C1.Win.C1Input.C1Button btnVoid;
        private C1.Win.C1Input.C1Button btnEdit;
        private C1.Win.C1Input.C1Button btnSave;
        private C1.Win.C1Input.C1TextBox txtFmCode;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1TextBox txtID;
        private C1.Win.C1Input.C1TextBox txtFmName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Input.C1ComboBox cboDocGroupName;
        private System.Windows.Forms.GroupBox groupBox1;
        private C1.Win.C1Input.C1CheckBox chkStatusDocOffice;
        private C1.Win.C1Input.C1CheckBox ChkStatusDocAdmision;
        private C1.Win.C1Input.C1CheckBox ChkStatusDocNurse;
        private C1.Win.C1Input.C1CheckBox chkStatusDocMedical;
        private System.Windows.Forms.Label label5;
    }
}