namespace bangna_hospital.gui
{
    partial class FrmScanAddNew
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.theme1 = new C1.Win.C1Themes.C1ThemeController();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlPatientBanner = new System.Windows.Forms.Panel();
            this.txtPreNo = new C1.Win.C1Input.C1TextBox();
            this.txtBed = new C1.Win.C1Input.C1TextBox();
            this.lblBed = new System.Windows.Forms.Label();
            this.txtRoom = new C1.Win.C1Input.C1TextBox();
            this.lblRoom = new System.Windows.Forms.Label();
            this.txtWard = new C1.Win.C1Input.C1TextBox();
            this.lblWard = new System.Windows.Forms.Label();
            this.txtDischargeDate = new C1.Win.C1Input.C1TextBox();
            this.lblDischargeDate = new System.Windows.Forms.Label();
            this.txtAdmitDate = new C1.Win.C1Input.C1TextBox();
            this.lblAdmitDate = new System.Windows.Forms.Label();
            this.chkIPD = new C1.Win.C1Input.C1CheckBox();
            this.txtAN = new C1.Win.C1Input.C1TextBox();
            this.lblAN = new System.Windows.Forms.Label();
            this.txtName = new C1.Win.C1Input.C1TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtHN = new C1.Win.C1Input.C1TextBox();
            this.lblHN = new System.Windows.Forms.Label();
            this.btnSearchPatient = new C1.Win.C1Input.C1Button();
            this.pnlActionBar = new System.Windows.Forms.Panel();
            this.lblImageStats = new System.Windows.Forms.Label();
            this.btnUpload = new C1.Win.C1Input.C1Button();
            this.btnOCRAutoGroup = new C1.Win.C1Input.C1Button();
            this.chkAutoGroupOnLoad = new System.Windows.Forms.CheckBox();
            this.btnReloadImages = new C1.Win.C1Input.C1Button();
            this.btnBrowsePath = new C1.Win.C1Input.C1Button();
            this.txtScanPath = new C1.Win.C1Input.C1TextBox();
            this.lblScanPath = new System.Windows.Forms.Label();
            this.pnlDocGroups = new System.Windows.Forms.Panel();
            this.flowGroupButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGrpAll = new System.Windows.Forms.Button();
            this.btnGrpDischarge = new System.Windows.Forms.Button();
            this.btnGrpAdmission = new System.Windows.Forms.Button();
            this.btnGrpOrder = new System.Windows.Forms.Button();
            this.btnGrpOperative = new System.Windows.Forms.Button();
            this.btnGrpInvestigation = new System.Windows.Forms.Button();
            this.btnGrpNurse = new System.Windows.Forms.Button();
            this.btnGrpMedication = new System.Windows.Forms.Button();
            this.btnGrpGraphic = new System.Windows.Forms.Button();
            this.btnGrpOther = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlGridImages = new System.Windows.Forms.Panel();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.pnlPreviewImageHolder = new System.Windows.Forms.Panel();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.pnlPreviewInfo = new System.Windows.Forms.Panel();
            this.lblPreviewInfo = new System.Windows.Forms.Label();
            this.pnlPreviewHeader = new System.Windows.Forms.Panel();
            this.btnFitWidth = new System.Windows.Forms.Button();
            this.btnRotateRight = new System.Windows.Forms.Button();
            this.btnRotateLeft = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.lblPreviewTitle = new System.Windows.Forms.Label();
            this.sbStatus = new System.Windows.Forms.StatusStrip();
            this.lblStatusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCurrentFile = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlPatientBanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDischargeDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdmitDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearchPatient)).BeginInit();
            this.pnlActionBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOCRAutoGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReloadImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBrowsePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtScanPath)).BeginInit();
            this.pnlDocGroups.SuspendLayout();
            this.flowGroupButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlPreview.SuspendLayout();
            this.pnlPreviewImageHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.pnlPreviewInfo.SuspendLayout();
            this.pnlPreviewHeader.SuspendLayout();
            this.sbStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.pnlActionBar);
            this.pnlTop.Controls.Add(this.pnlPatientBanner);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 116);
            this.pnlTop.TabIndex = 0;
            // 
            // pnlPatientBanner
            // 
            this.pnlPatientBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.pnlPatientBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPatientBanner.Controls.Add(this.txtPreNo);
            this.pnlPatientBanner.Controls.Add(this.txtBed);
            this.pnlPatientBanner.Controls.Add(this.lblBed);
            this.pnlPatientBanner.Controls.Add(this.txtRoom);
            this.pnlPatientBanner.Controls.Add(this.lblRoom);
            this.pnlPatientBanner.Controls.Add(this.txtWard);
            this.pnlPatientBanner.Controls.Add(this.lblWard);
            this.pnlPatientBanner.Controls.Add(this.txtDischargeDate);
            this.pnlPatientBanner.Controls.Add(this.lblDischargeDate);
            this.pnlPatientBanner.Controls.Add(this.txtAdmitDate);
            this.pnlPatientBanner.Controls.Add(this.lblAdmitDate);
            this.pnlPatientBanner.Controls.Add(this.chkIPD);
            this.pnlPatientBanner.Controls.Add(this.txtAN);
            this.pnlPatientBanner.Controls.Add(this.lblAN);
            this.pnlPatientBanner.Controls.Add(this.txtName);
            this.pnlPatientBanner.Controls.Add(this.lblName);
            this.pnlPatientBanner.Controls.Add(this.txtHN);
            this.pnlPatientBanner.Controls.Add(this.lblHN);
            this.pnlPatientBanner.Controls.Add(this.btnSearchPatient);
            this.pnlPatientBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatientBanner.Location = new System.Drawing.Point(0, 0);
            this.pnlPatientBanner.Name = "pnlPatientBanner";
            this.pnlPatientBanner.Size = new System.Drawing.Size(1200, 68);
            this.pnlPatientBanner.TabIndex = 0;
            // 
            // txtPreNo
            // 
            this.txtPreNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPreNo.Location = new System.Drawing.Point(1115, 36);
            this.txtPreNo.Name = "txtPreNo";
            this.txtPreNo.ReadOnly = true;
            this.txtPreNo.Size = new System.Drawing.Size(65, 23);
            this.txtPreNo.TabIndex = 18;
            this.txtPreNo.Visible = false;
            // 
            // txtBed
            // 
            this.txtBed.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtBed.Location = new System.Drawing.Point(920, 36);
            this.txtBed.Name = "txtBed";
            this.txtBed.ReadOnly = true;
            this.txtBed.Size = new System.Drawing.Size(55, 23);
            this.txtBed.TabIndex = 17;
            // 
            // lblBed
            // 
            this.lblBed.AutoSize = true;
            this.lblBed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblBed.Location = new System.Drawing.Point(882, 40);
            this.lblBed.Name = "lblBed";
            this.lblBed.Size = new System.Drawing.Size(35, 14);
            this.lblBed.TabIndex = 16;
            this.lblBed.Text = "เตียง:";
            // 
            // txtRoom
            // 
            this.txtRoom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtRoom.Location = new System.Drawing.Point(805, 36);
            this.txtRoom.Name = "txtRoom";
            this.txtRoom.ReadOnly = true;
            this.txtRoom.Size = new System.Drawing.Size(65, 23);
            this.txtRoom.TabIndex = 15;
            // 
            // lblRoom
            // 
            this.lblRoom.AutoSize = true;
            this.lblRoom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblRoom.Location = new System.Drawing.Point(768, 40);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(34, 14);
            this.lblRoom.TabIndex = 14;
            this.lblRoom.Text = "ห้อง:";
            // 
            // txtWard
            // 
            this.txtWard.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtWard.Location = new System.Drawing.Point(620, 36);
            this.txtWard.Name = "txtWard";
            this.txtWard.ReadOnly = true;
            this.txtWard.Size = new System.Drawing.Size(138, 23);
            this.txtWard.TabIndex = 13;
            // 
            // lblWard
            // 
            this.lblWard.AutoSize = true;
            this.lblWard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblWard.Location = new System.Drawing.Point(549, 40);
            this.lblWard.Name = "lblWard";
            this.lblWard.Size = new System.Drawing.Size(68, 14);
            this.lblWard.TabIndex = 12;
            this.lblWard.Text = "หอผู้ป่วย:";
            // 
            // txtDischargeDate
            // 
            this.txtDischargeDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDischargeDate.Location = new System.Drawing.Point(420, 36);
            this.txtDischargeDate.Name = "txtDischargeDate";
            this.txtDischargeDate.ReadOnly = true;
            this.txtDischargeDate.Size = new System.Drawing.Size(115, 23);
            this.txtDischargeDate.TabIndex = 11;
            // 
            // lblDischargeDate
            // 
            this.lblDischargeDate.AutoSize = true;
            this.lblDischargeDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDischargeDate.Location = new System.Drawing.Point(365, 40);
            this.lblDischargeDate.Name = "lblDischargeDate";
            this.lblDischargeDate.Size = new System.Drawing.Size(52, 14);
            this.lblDischargeDate.TabIndex = 10;
            this.lblDischargeDate.Text = "วัน D/C:";
            // 
            // txtAdmitDate
            // 
            this.txtAdmitDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAdmitDate.Location = new System.Drawing.Point(240, 36);
            this.txtAdmitDate.Name = "txtAdmitDate";
            this.txtAdmitDate.ReadOnly = true;
            this.txtAdmitDate.Size = new System.Drawing.Size(115, 23);
            this.txtAdmitDate.TabIndex = 9;
            // 
            // lblAdmitDate
            // 
            this.lblAdmitDate.AutoSize = true;
            this.lblAdmitDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblAdmitDate.Location = new System.Drawing.Point(177, 40);
            this.lblAdmitDate.Name = "lblAdmitDate";
            this.lblAdmitDate.Size = new System.Drawing.Size(60, 14);
            this.lblAdmitDate.TabIndex = 8;
            this.lblAdmitDate.Text = "วัน Admit:";
            // 
            // chkIPD
            // 
            this.chkIPD.Checked = true;
            this.chkIPD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIPD.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkIPD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.chkIPD.Location = new System.Drawing.Point(1000, 8);
            this.chkIPD.Name = "chkIPD";
            this.chkIPD.Size = new System.Drawing.Size(100, 24);
            this.chkIPD.TabIndex = 7;
            this.chkIPD.Text = "ผู้ป่วยใน IPD";
            this.chkIPD.UseVisualStyleBackColor = true;
            // 
            // txtAN
            // 
            this.txtAN.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAN.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtAN.Location = new System.Drawing.Point(620, 7);
            this.txtAN.Name = "txtAN";
            this.txtAN.ReadOnly = true;
            this.txtAN.Size = new System.Drawing.Size(120, 26);
            this.txtAN.TabIndex = 6;
            // 
            // lblAN
            // 
            this.lblAN.AutoSize = true;
            this.lblAN.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblAN.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblAN.Location = new System.Drawing.Point(585, 12);
            this.lblAN.Name = "lblAN";
            this.lblAN.Size = new System.Drawing.Size(32, 16);
            this.lblAN.TabIndex = 5;
            this.lblAN.Text = "AN:";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtName.Location = new System.Drawing.Point(340, 7);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(235, 26);
            this.txtName.TabIndex = 4;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblName.Location = new System.Drawing.Point(280, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 16);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "ชื่อ-สกุล:";
            // 
            // txtHN
            // 
            this.txtHN.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHN.ForeColor = System.Drawing.Color.DarkGreen;
            this.txtHN.Location = new System.Drawing.Point(177, 7);
            this.txtHN.Name = "txtHN";
            this.txtHN.ReadOnly = true;
            this.txtHN.Size = new System.Drawing.Size(95, 26);
            this.txtHN.TabIndex = 2;
            // 
            // lblHN
            // 
            this.lblHN.AutoSize = true;
            this.lblHN.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblHN.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblHN.Location = new System.Drawing.Point(145, 12);
            this.lblHN.Name = "lblHN";
            this.lblHN.Size = new System.Drawing.Size(31, 16);
            this.lblHN.TabIndex = 1;
            this.lblHN.Text = "HN:";
            // 
            // btnSearchPatient
            // 
            this.btnSearchPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSearchPatient.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSearchPatient.ForeColor = System.Drawing.Color.White;
            this.btnSearchPatient.Location = new System.Drawing.Point(8, 8);
            this.btnSearchPatient.Name = "btnSearchPatient";
            this.btnSearchPatient.Size = new System.Drawing.Size(125, 52);
            this.btnSearchPatient.TabIndex = 0;
            this.btnSearchPatient.Text = "🔍 ค้นหาคนไข้\r\n[F3 / Search]";
            this.btnSearchPatient.UseVisualStyleBackColor = false;
            // 
            // pnlActionBar
            // 
            this.pnlActionBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlActionBar.Controls.Add(this.lblImageStats);
            this.pnlActionBar.Controls.Add(this.btnUpload);
            this.pnlActionBar.Controls.Add(this.btnOCRAutoGroup);
            this.pnlActionBar.Controls.Add(this.chkAutoGroupOnLoad);
            this.pnlActionBar.Controls.Add(this.btnReloadImages);
            this.pnlActionBar.Controls.Add(this.btnBrowsePath);
            this.pnlActionBar.Controls.Add(this.txtScanPath);
            this.pnlActionBar.Controls.Add(this.lblScanPath);
            this.pnlActionBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActionBar.Location = new System.Drawing.Point(0, 68);
            this.pnlActionBar.Name = "pnlActionBar";
            this.pnlActionBar.Size = new System.Drawing.Size(1200, 48);
            this.pnlActionBar.TabIndex = 1;
            // 
            // lblImageStats
            // 
            this.lblImageStats.AutoSize = true;
            this.lblImageStats.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblImageStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblImageStats.Location = new System.Drawing.Point(740, 16);
            this.lblImageStats.Name = "lblImageStats";
            this.lblImageStats.Size = new System.Drawing.Size(107, 16);
            this.lblImageStats.TabIndex = 7;
            this.lblImageStats.Text = "จำนวน: 0 รายการ";
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnUpload.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnUpload.ForeColor = System.Drawing.Color.White;
            this.btnUpload.Location = new System.Drawing.Point(1030, 8);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(160, 32);
            this.btnUpload.TabIndex = 6;
            this.btnUpload.Text = "☁️ Upload เวชระเบียน";
            this.btnUpload.UseVisualStyleBackColor = false;
            // 
            // btnOCRAutoGroup
            // 
            this.btnOCRAutoGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOCRAutoGroup.Location = new System.Drawing.Point(580, 9);
            this.btnOCRAutoGroup.Name = "btnOCRAutoGroup";
            this.btnOCRAutoGroup.Size = new System.Drawing.Size(145, 30);
            this.btnOCRAutoGroup.TabIndex = 5;
            this.btnOCRAutoGroup.Text = "⚡ ตรวจจับ OCR จัดกลุ่ม";
            this.btnOCRAutoGroup.UseVisualStyleBackColor = true;
            // 
            // chkAutoGroupOnLoad
            // 
            this.chkAutoGroupOnLoad.AutoSize = true;
            this.chkAutoGroupOnLoad.Checked = true;
            this.chkAutoGroupOnLoad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoGroupOnLoad.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkAutoGroupOnLoad.Location = new System.Drawing.Point(470, 16);
            this.chkAutoGroupOnLoad.Name = "chkAutoGroupOnLoad";
            this.chkAutoGroupOnLoad.Size = new System.Drawing.Size(102, 17);
            this.chkAutoGroupOnLoad.TabIndex = 4;
            this.chkAutoGroupOnLoad.Text = "จัดกลุ่ม Auto เมื่อโหลด";
            this.chkAutoGroupOnLoad.UseVisualStyleBackColor = true;
            // 
            // btnReloadImages
            // 
            this.btnReloadImages.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnReloadImages.Location = new System.Drawing.Point(375, 9);
            this.btnReloadImages.Name = "btnReloadImages";
            this.btnReloadImages.Size = new System.Drawing.Size(85, 30);
            this.btnReloadImages.TabIndex = 3;
            this.btnReloadImages.Text = "🔄 โหลดรูป";
            this.btnReloadImages.UseVisualStyleBackColor = true;
            // 
            // btnBrowsePath
            // 
            this.btnBrowsePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnBrowsePath.Location = new System.Drawing.Point(335, 9);
            this.btnBrowsePath.Name = "btnBrowsePath";
            this.btnBrowsePath.Size = new System.Drawing.Size(35, 30);
            this.btnBrowsePath.TabIndex = 2;
            this.btnBrowsePath.Text = "...";
            this.btnBrowsePath.UseVisualStyleBackColor = true;
            // 
            // txtScanPath
            // 
            this.txtScanPath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtScanPath.Location = new System.Drawing.Point(90, 13);
            this.txtScanPath.Name = "txtScanPath";
            this.txtScanPath.Size = new System.Drawing.Size(240, 22);
            this.txtScanPath.TabIndex = 1;
            // 
            // lblScanPath
            // 
            this.lblScanPath.AutoSize = true;
            this.lblScanPath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblScanPath.Location = new System.Drawing.Point(10, 16);
            this.lblScanPath.Name = "lblScanPath";
            this.lblScanPath.Size = new System.Drawing.Size(77, 14);
            this.lblScanPath.TabIndex = 0;
            this.lblScanPath.Text = "โฟลเดอร์รูปภาพ:";
            // 
            // pnlDocGroups
            // 
            this.pnlDocGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(240)))), ((int)(((byte)(242)))));
            this.pnlDocGroups.Controls.Add(this.flowGroupButtons);
            this.pnlDocGroups.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDocGroups.Location = new System.Drawing.Point(0, 116);
            this.pnlDocGroups.Name = "pnlDocGroups";
            this.pnlDocGroups.Size = new System.Drawing.Size(1200, 42);
            this.pnlDocGroups.TabIndex = 1;
            // 
            // flowGroupButtons
            // 
            this.flowGroupButtons.Controls.Add(this.btnGrpAll);
            this.flowGroupButtons.Controls.Add(this.btnGrpDischarge);
            this.flowGroupButtons.Controls.Add(this.btnGrpAdmission);
            this.flowGroupButtons.Controls.Add(this.btnGrpOrder);
            this.flowGroupButtons.Controls.Add(this.btnGrpOperative);
            this.flowGroupButtons.Controls.Add(this.btnGrpInvestigation);
            this.flowGroupButtons.Controls.Add(this.btnGrpNurse);
            this.flowGroupButtons.Controls.Add(this.btnGrpMedication);
            this.flowGroupButtons.Controls.Add(this.btnGrpGraphic);
            this.flowGroupButtons.Controls.Add(this.btnGrpOther);
            this.flowGroupButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowGroupButtons.Location = new System.Drawing.Point(0, 0);
            this.flowGroupButtons.Name = "flowGroupButtons";
            this.flowGroupButtons.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.flowGroupButtons.Size = new System.Drawing.Size(1200, 42);
            this.flowGroupButtons.TabIndex = 0;
            // 
            // btnGrpAll
            // 
            this.btnGrpAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnGrpAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrpAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnGrpAll.ForeColor = System.Drawing.Color.White;
            this.btnGrpAll.Location = new System.Drawing.Point(9, 7);
            this.btnGrpAll.Name = "btnGrpAll";
            this.btnGrpAll.Size = new System.Drawing.Size(95, 28);
            this.btnGrpAll.TabIndex = 0;
            this.btnGrpAll.Text = "ทั้งหมด (0)";
            this.btnGrpAll.UseVisualStyleBackColor = false;
            // 
            // btnGrpDischarge
            // 
            this.btnGrpDischarge.BackColor = System.Drawing.Color.White;
            this.btnGrpDischarge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrpDischarge.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnGrpDischarge.Location = new System.Drawing.Point(110, 7);
            this.btnGrpDischarge.Name = "btnGrpDischarge";
            this.btnGrpDischarge.Size = new System.Drawing.Size(105, 28);
            this.btnGrpDischarge.TabIndex = 1;
            this.btnGrpDischarge.Text = "DISCHARGE (0)";
            this.btnGrpDischarge.UseVisualStyleBackColor = false;
            // 
            // btnGrpAdmission
            // 
            this.btnGrpAdmission.BackColor = System.Drawing.Color.White;
            this.btnGrpAdmission.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrpAdmission.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnGrpAdmission.Location = new System.Drawing.Point(221, 7);
            this.btnGrpAdmission.Name = "btnGrpAdmission";
            this.btnGrpAdmission.Size = new System.Drawing.Size(105, 28);
            this.btnGrpAdmission.TabIndex = 2;
            this.btnGrpAdmission.Text = "ADMISSION (0)";
            this.btnGrpAdmission.UseVisualStyleBackColor = false;
            // 
            // btnGrpOrder
            // 
            this.btnGrpOrder.BackColor = System.Drawing.Color.White;
            this.btnGrpOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrpOrder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnGrpOrder.Location = new System.Drawing.Point(332, 7);
            this.btnGrpOrder.Name = "btnGrpOrder";
            this.btnGrpOrder.Size = new System.Drawing.Size(95, 28);
            this.btnGrpOrder.TabIndex = 3;
            this.btnGrpOrder.Text = "ORDER (0)";
            this.btnGrpOrder.UseVisualStyleBackColor = false;
            // 
            // btnGrpOperative
            // 
            this.btnGrpOperative.BackColor = System.Drawing.Color.White;
            this.btnGrpOperative.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrpOperative.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnGrpOperative.Location = new System.Drawing.Point(433, 7);
            this.btnGrpOperative.Name = "btnGrpOperative";
            this.btnGrpOperative.Size = new System.Drawing.Size(105, 28);
            this.btnGrpOperative.TabIndex = 4;
            this.btnGrpOperative.Text = "OPERATIVE (0)";
            this.btnGrpOperative.UseVisualStyleBackColor = false;
            // 
            // btnGrpInvestigation
            // 
            this.btnGrpInvestigation.BackColor = System.Drawing.Color.White;
            this.btnGrpInvestigation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrpInvestigation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnGrpInvestigation.Location = new System.Drawing.Point(544, 7);
            this.btnGrpInvestigation.Name = "btnGrpInvestigation";
            this.btnGrpInvestigation.Size = new System.Drawing.Size(125, 28);
            this.btnGrpInvestigation.TabIndex = 5;
            this.btnGrpInvestigation.Text = "INVESTIGATION (0)";
            this.btnGrpInvestigation.UseVisualStyleBackColor = false;
            // 
            // btnGrpNurse
            // 
            this.btnGrpNurse.BackColor = System.Drawing.Color.White;
            this.btnGrpNurse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrpNurse.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnGrpNurse.Location = new System.Drawing.Point(675, 7);
            this.btnGrpNurse.Name = "btnGrpNurse";
            this.btnGrpNurse.Size = new System.Drawing.Size(95, 28);
            this.btnGrpNurse.TabIndex = 6;
            this.btnGrpNurse.Text = "NURSE (0)";
            this.btnGrpNurse.UseVisualStyleBackColor = false;
            // 
            // btnGrpMedication
            // 
            this.btnGrpMedication.BackColor = System.Drawing.Color.White;
            this.btnGrpMedication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrpMedication.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnGrpMedication.Location = new System.Drawing.Point(776, 7);
            this.btnGrpMedication.Name = "btnGrpMedication";
            this.btnGrpMedication.Size = new System.Drawing.Size(120, 28);
            this.btnGrpMedication.TabIndex = 7;
            this.btnGrpMedication.Text = "MEDICATION (0)";
            this.btnGrpMedication.UseVisualStyleBackColor = false;
            // 
            // btnGrpGraphic
            // 
            this.btnGrpGraphic.BackColor = System.Drawing.Color.White;
            this.btnGrpGraphic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrpGraphic.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnGrpGraphic.Location = new System.Drawing.Point(902, 7);
            this.btnGrpGraphic.Name = "btnGrpGraphic";
            this.btnGrpGraphic.Size = new System.Drawing.Size(110, 28);
            this.btnGrpGraphic.TabIndex = 8;
            this.btnGrpGraphic.Text = "GRAPHIC (0)";
            this.btnGrpGraphic.UseVisualStyleBackColor = false;
            // 
            // btnGrpOther
            // 
            this.btnGrpOther.BackColor = System.Drawing.Color.White;
            this.btnGrpOther.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrpOther.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnGrpOther.Location = new System.Drawing.Point(1018, 7);
            this.btnGrpOther.Name = "btnGrpOther";
            this.btnGrpOther.Size = new System.Drawing.Size(90, 28);
            this.btnGrpOther.TabIndex = 9;
            this.btnGrpOther.Text = "OTHER (0)";
            this.btnGrpOther.UseVisualStyleBackColor = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 158);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlGridImages);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlPreview);
            this.splitContainer1.Size = new System.Drawing.Size(1200, 520);
            this.splitContainer1.SplitterDistance = 720;
            this.splitContainer1.TabIndex = 2;
            // 
            // pnlGridImages
            // 
            this.pnlGridImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridImages.Location = new System.Drawing.Point(0, 0);
            this.pnlGridImages.Name = "pnlGridImages";
            this.pnlGridImages.Size = new System.Drawing.Size(720, 520);
            this.pnlGridImages.TabIndex = 0;
            // 
            // pnlPreview
            // 
            this.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreview.Controls.Add(this.pnlPreviewImageHolder);
            this.pnlPreview.Controls.Add(this.pnlPreviewInfo);
            this.pnlPreview.Controls.Add(this.pnlPreviewHeader);
            this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreview.Location = new System.Drawing.Point(0, 0);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(476, 520);
            this.pnlPreview.TabIndex = 0;
            // 
            // pnlPreviewImageHolder
            // 
            this.pnlPreviewImageHolder.AutoScroll = true;
            this.pnlPreviewImageHolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlPreviewImageHolder.Controls.Add(this.picPreview);
            this.pnlPreviewImageHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreviewImageHolder.Location = new System.Drawing.Point(0, 36);
            this.pnlPreviewImageHolder.Name = "pnlPreviewImageHolder";
            this.pnlPreviewImageHolder.Size = new System.Drawing.Size(474, 432);
            this.pnlPreviewImageHolder.TabIndex = 2;
            // 
            // picPreview
            // 
            this.picPreview.Location = new System.Drawing.Point(0, 0);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(450, 400);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            // 
            // pnlPreviewInfo
            // 
            this.pnlPreviewInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlPreviewInfo.Controls.Add(this.lblPreviewInfo);
            this.pnlPreviewInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPreviewInfo.Location = new System.Drawing.Point(0, 468);
            this.pnlPreviewInfo.Name = "pnlPreviewInfo";
            this.pnlPreviewInfo.Size = new System.Drawing.Size(474, 50);
            this.pnlPreviewInfo.TabIndex = 1;
            // 
            // lblPreviewInfo
            // 
            this.lblPreviewInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPreviewInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblPreviewInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblPreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.lblPreviewInfo.Name = "lblPreviewInfo";
            this.lblPreviewInfo.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.lblPreviewInfo.Size = new System.Drawing.Size(474, 50);
            this.lblPreviewInfo.TabIndex = 0;
            this.lblPreviewInfo.Text = "ข้อมูลเอกสาร: คลิกเลือกภาพในตารางเพื่อดูภาพตัวอย่างขนาดใหญ่";
            // 
            // pnlPreviewHeader
            // 
            this.pnlPreviewHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.pnlPreviewHeader.Controls.Add(this.btnFitWidth);
            this.pnlPreviewHeader.Controls.Add(this.btnRotateRight);
            this.pnlPreviewHeader.Controls.Add(this.btnRotateLeft);
            this.pnlPreviewHeader.Controls.Add(this.btnZoomOut);
            this.pnlPreviewHeader.Controls.Add(this.btnZoomIn);
            this.pnlPreviewHeader.Controls.Add(this.lblPreviewTitle);
            this.pnlPreviewHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPreviewHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlPreviewHeader.Name = "pnlPreviewHeader";
            this.pnlPreviewHeader.Size = new System.Drawing.Size(474, 36);
            this.pnlPreviewHeader.TabIndex = 0;
            // 
            // btnFitWidth
            // 
            this.btnFitWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFitWidth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnFitWidth.Location = new System.Drawing.Point(405, 5);
            this.btnFitWidth.Name = "btnFitWidth";
            this.btnFitWidth.Size = new System.Drawing.Size(60, 26);
            this.btnFitWidth.TabIndex = 5;
            this.btnFitWidth.Text = "พอดีจอ";
            this.btnFitWidth.UseVisualStyleBackColor = true;
            // 
            // btnRotateRight
            // 
            this.btnRotateRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRotateRight.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnRotateRight.Location = new System.Drawing.Point(365, 5);
            this.btnRotateRight.Name = "btnRotateRight";
            this.btnRotateRight.Size = new System.Drawing.Size(35, 26);
            this.btnRotateRight.TabIndex = 4;
            this.btnRotateRight.Text = "↷";
            this.btnRotateRight.UseVisualStyleBackColor = true;
            // 
            // btnRotateLeft
            // 
            this.btnRotateLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRotateLeft.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnRotateLeft.Location = new System.Drawing.Point(325, 5);
            this.btnRotateLeft.Name = "btnRotateLeft";
            this.btnRotateLeft.Size = new System.Drawing.Size(35, 26);
            this.btnRotateLeft.TabIndex = 3;
            this.btnRotateLeft.Text = "↶";
            this.btnRotateLeft.UseVisualStyleBackColor = true;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomOut.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnZoomOut.Location = new System.Drawing.Point(285, 5);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(35, 26);
            this.btnZoomOut.TabIndex = 2;
            this.btnZoomOut.Text = "－";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomIn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnZoomIn.Location = new System.Drawing.Point(245, 5);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(35, 26);
            this.btnZoomIn.TabIndex = 1;
            this.btnZoomIn.Text = "＋";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            // 
            // lblPreviewTitle
            // 
            this.lblPreviewTitle.AutoSize = true;
            this.lblPreviewTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblPreviewTitle.Location = new System.Drawing.Point(10, 10);
            this.lblPreviewTitle.Name = "lblPreviewTitle";
            this.lblPreviewTitle.Size = new System.Drawing.Size(126, 14);
            this.lblPreviewTitle.TabIndex = 0;
            this.lblPreviewTitle.Text = "🔍 ดูภาพขยายตัวอย่าง";
            // 
            // sbStatus
            // 
            this.sbStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusMsg,
            this.lblCurrentFile});
            this.sbStatus.Location = new System.Drawing.Point(0, 678);
            this.sbStatus.Name = "sbStatus";
            this.sbStatus.Size = new System.Drawing.Size(1200, 22);
            this.sbStatus.TabIndex = 3;
            // 
            // lblStatusMsg
            // 
            this.lblStatusMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblStatusMsg.Name = "lblStatusMsg";
            this.lblStatusMsg.Size = new System.Drawing.Size(155, 17);
            this.lblStatusMsg.Text = "พร้อมทำงาน - กด F3 ค้นหาคนไข้";
            // 
            // lblCurrentFile
            // 
            this.lblCurrentFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCurrentFile.ForeColor = System.Drawing.Color.Gray;
            this.lblCurrentFile.Name = "lblCurrentFile";
            this.lblCurrentFile.Size = new System.Drawing.Size(0, 17);
            // 
            // FrmScanAddNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlDocGroups);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.sbStatus);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "FrmScanAddNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "จัดกลุ่มและสแกนเวชระเบียนผู้ป่วยใน (IPD Scan & Document Classification)";
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlPatientBanner.ResumeLayout(false);
            this.pnlPatientBanner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDischargeDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdmitDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearchPatient)).EndInit();
            this.pnlActionBar.ResumeLayout(false);
            this.pnlActionBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOCRAutoGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReloadImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBrowsePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtScanPath)).EndInit();
            this.pnlDocGroups.ResumeLayout(false);
            this.flowGroupButtons.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlPreview.ResumeLayout(false);
            this.pnlPreviewImageHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.pnlPreviewInfo.ResumeLayout(false);
            this.pnlPreviewHeader.ResumeLayout(false);
            this.pnlPreviewHeader.PerformLayout();
            this.sbStatus.ResumeLayout(false);
            this.sbStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController theme1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlPatientBanner;
        private C1.Win.C1Input.C1Button btnSearchPatient;
        private C1.Win.C1Input.C1TextBox txtHN;
        private System.Windows.Forms.Label lblHN;
        private C1.Win.C1Input.C1TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private C1.Win.C1Input.C1TextBox txtAN;
        private System.Windows.Forms.Label lblAN;
        private C1.Win.C1Input.C1CheckBox chkIPD;
        private C1.Win.C1Input.C1TextBox txtAdmitDate;
        private System.Windows.Forms.Label lblAdmitDate;
        private C1.Win.C1Input.C1TextBox txtDischargeDate;
        private System.Windows.Forms.Label lblDischargeDate;
        private C1.Win.C1Input.C1TextBox txtWard;
        private System.Windows.Forms.Label lblWard;
        private C1.Win.C1Input.C1TextBox txtRoom;
        private System.Windows.Forms.Label lblRoom;
        private C1.Win.C1Input.C1TextBox txtBed;
        private System.Windows.Forms.Label lblBed;
        private C1.Win.C1Input.C1TextBox txtPreNo;
        private System.Windows.Forms.Panel pnlActionBar;
        private System.Windows.Forms.Label lblScanPath;
        private C1.Win.C1Input.C1TextBox txtScanPath;
        private C1.Win.C1Input.C1Button btnBrowsePath;
        private C1.Win.C1Input.C1Button btnReloadImages;
        private System.Windows.Forms.CheckBox chkAutoGroupOnLoad;
        private C1.Win.C1Input.C1Button btnOCRAutoGroup;
        private C1.Win.C1Input.C1Button btnUpload;
        private System.Windows.Forms.Label lblImageStats;
        private System.Windows.Forms.Panel pnlDocGroups;
        private System.Windows.Forms.FlowLayoutPanel flowGroupButtons;
        private System.Windows.Forms.Button btnGrpAll;
        private System.Windows.Forms.Button btnGrpDischarge;
        private System.Windows.Forms.Button btnGrpAdmission;
        private System.Windows.Forms.Button btnGrpOrder;
        private System.Windows.Forms.Button btnGrpOperative;
        private System.Windows.Forms.Button btnGrpInvestigation;
        private System.Windows.Forms.Button btnGrpNurse;
        private System.Windows.Forms.Button btnGrpMedication;
        private System.Windows.Forms.Button btnGrpGraphic;
        private System.Windows.Forms.Button btnGrpOther;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlGridImages;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.Panel pnlPreviewHeader;
        private System.Windows.Forms.Label lblPreviewTitle;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnRotateLeft;
        private System.Windows.Forms.Button btnRotateRight;
        private System.Windows.Forms.Button btnFitWidth;
        private System.Windows.Forms.Panel pnlPreviewImageHolder;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Panel pnlPreviewInfo;
        private System.Windows.Forms.Label lblPreviewInfo;
        private System.Windows.Forms.StatusStrip sbStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusMsg;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentFile;
    }
}
