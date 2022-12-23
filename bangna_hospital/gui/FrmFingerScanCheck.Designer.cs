
namespace bangna_hospital.gui
{
    partial class FrmFingerScanCheck
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
            this.lbFullName = new System.Windows.Forms.Label();
            this.txtHn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OpenDeviceBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDeviceName = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxSelFinger = new System.Windows.Forms.ComboBox();
            this.progressBar_R1 = new System.Windows.Forms.ProgressBar();
            this.pictureBoxR1 = new System.Windows.Forms.PictureBox();
            this.BtnCapture1 = new System.Windows.Forms.Button();
            this.StatusBar = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbFullName
            // 
            this.lbFullName.AutoSize = true;
            this.lbFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbFullName.Location = new System.Drawing.Point(159, 8);
            this.lbFullName.Name = "lbFullName";
            this.lbFullName.Size = new System.Drawing.Size(30, 25);
            this.lbFullName.TabIndex = 45;
            this.lbFullName.Text = "...";
            // 
            // txtHn
            // 
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHn.Location = new System.Drawing.Point(53, 5);
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(100, 26);
            this.txtHn.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(5, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 20);
            this.label3.TabIndex = 43;
            this.label3.Text = "HN";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OpenDeviceBtn);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBoxDeviceName);
            this.groupBox2.Location = new System.Drawing.Point(4, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 48);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Initialization";
            // 
            // OpenDeviceBtn
            // 
            this.OpenDeviceBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.OpenDeviceBtn.Location = new System.Drawing.Point(241, 18);
            this.OpenDeviceBtn.Name = "OpenDeviceBtn";
            this.OpenDeviceBtn.Size = new System.Drawing.Size(54, 24);
            this.OpenDeviceBtn.TabIndex = 8;
            this.OpenDeviceBtn.Text = "Init";
            this.OpenDeviceBtn.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "Device Name";
            // 
            // comboBoxDeviceName
            // 
            this.comboBoxDeviceName.Items.AddRange(new object[] {
            "Auto Selection",
            "USB FDU08P(U20AP)",
            "USB FDU08(U20A)",
            "USB FDU07A(U10A)",
            "USB FDU07(U10)",
            "USB FDU05",
            "USB FDU04",
            "USB FDU03",
            "USB FDU02",
            "No device"});
            this.comboBoxDeviceName.Location = new System.Drawing.Point(82, 18);
            this.comboBoxDeviceName.Name = "comboBoxDeviceName";
            this.comboBoxDeviceName.Size = new System.Drawing.Size(152, 21);
            this.comboBoxDeviceName.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.comboBoxSelFinger);
            this.groupBox3.Controls.Add(this.progressBar_R1);
            this.groupBox3.Controls.Add(this.pictureBoxR1);
            this.groupBox3.Controls.Add(this.BtnCapture1);
            this.groupBox3.Location = new System.Drawing.Point(9, 91);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(309, 202);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Check";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(6, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 25);
            this.label4.TabIndex = 51;
            this.label4.Text = "...";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 18);
            this.label2.TabIndex = 50;
            this.label2.Text = "Select Finger";
            // 
            // comboBoxSelFinger
            // 
            this.comboBoxSelFinger.Items.AddRange(new object[] {
            "Unknown finger",
            "Right thumb",
            "Right index finger",
            "Right middle finger",
            "Right ring finger",
            "Right little finger",
            "Left thumb",
            "Left index finger",
            "Left middle finger",
            "Left ring finger",
            "Left little finger"});
            this.comboBoxSelFinger.Location = new System.Drawing.Point(6, 34);
            this.comboBoxSelFinger.Name = "comboBoxSelFinger";
            this.comboBoxSelFinger.Size = new System.Drawing.Size(128, 21);
            this.comboBoxSelFinger.TabIndex = 49;
            // 
            // progressBar_R1
            // 
            this.progressBar_R1.Location = new System.Drawing.Point(155, 139);
            this.progressBar_R1.Name = "progressBar_R1";
            this.progressBar_R1.Size = new System.Drawing.Size(104, 12);
            this.progressBar_R1.TabIndex = 43;
            // 
            // pictureBoxR1
            // 
            this.pictureBoxR1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxR1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxR1.Location = new System.Drawing.Point(155, 16);
            this.pictureBoxR1.Name = "pictureBoxR1";
            this.pictureBoxR1.Size = new System.Drawing.Size(104, 120);
            this.pictureBoxR1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxR1.TabIndex = 26;
            this.pictureBoxR1.TabStop = false;
            // 
            // BtnCapture1
            // 
            this.BtnCapture1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BtnCapture1.Location = new System.Drawing.Point(155, 153);
            this.BtnCapture1.Name = "BtnCapture1";
            this.BtnCapture1.Size = new System.Drawing.Size(104, 42);
            this.BtnCapture1.TabIndex = 23;
            this.BtnCapture1.Text = "Capture R1";
            this.BtnCapture1.UseVisualStyleBackColor = false;
            // 
            // StatusBar
            // 
            this.StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusBar.ForeColor = System.Drawing.SystemColors.Highlight;
            this.StatusBar.Location = new System.Drawing.Point(0, 298);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(415, 24);
            this.StatusBar.TabIndex = 48;
            // 
            // FrmFingerScanCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 322);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lbFullName);
            this.Controls.Add(this.txtHn);
            this.Controls.Add(this.label3);
            this.Name = "FrmFingerScanCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmFingerScanCheck";
            this.Load += new System.EventHandler(this.FrmFingerScanCheck_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbFullName;
        private System.Windows.Forms.TextBox txtHn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button OpenDeviceBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDeviceName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxSelFinger;
        private System.Windows.Forms.ProgressBar progressBar_R1;
        private System.Windows.Forms.PictureBox pictureBoxR1;
        private System.Windows.Forms.Button BtnCapture1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label StatusBar;
    }
}