namespace bangna_hospital.gui
{
    partial class FrmScanAddView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAn = new C1.Win.C1Input.C1TextBox();
            this._wordTextBox = new System.Windows.Forms.TextBox();
            this.btnAnalyze = new C1.Win.C1Input.C1Button();
            this.btnRotate = new C1.Win.C1Input.C1Button();
            this.txtVisitDate = new C1.Win.C1Input.C1DateEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVN = new C1.Win.C1Input.C1TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDgs = new C1.Win.C1Input.C1ComboBox();
            this.txtNameFeMale = new C1.Win.C1Input.C1TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.pnImg = new System.Windows.Forms.Panel();
            this.pic1 = new System.Windows.Forms.PictureBox();
            this.pn = new System.Windows.Forms.Panel();
            this.btnSave = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAnalyze)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRotate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVisitDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFeMale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            this.pnImg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            this.pn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtAn);
            this.groupBox1.Controls.Add(this._wordTextBox);
            this.groupBox1.Controls.Add(this.btnAnalyze);
            this.groupBox1.Controls.Add(this.btnRotate);
            this.groupBox1.Controls.Add(this.txtVisitDate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtVN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboDgs);
            this.groupBox1.Controls.Add(this.txtNameFeMale);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1089, 66);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patient";
            this.theme1.SetTheme(this.groupBox1, "(default)");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label3.Location = new System.Drawing.Point(206, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 16);
            this.label3.TabIndex = 554;
            this.label3.Text = "AN  :";
            this.theme1.SetTheme(this.label3, "(default)");
            // 
            // txtAn
            // 
            this.txtAn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAn.Location = new System.Drawing.Point(248, 37);
            this.txtAn.Name = "txtAn";
            this.txtAn.Size = new System.Drawing.Size(150, 20);
            this.txtAn.TabIndex = 553;
            this.txtAn.Tag = null;
            this.theme1.SetTheme(this.txtAn, "(default)");
            this.txtAn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtAn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // _wordTextBox
            // 
            this._wordTextBox.BackColor = System.Drawing.Color.White;
            this._wordTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._wordTextBox.Location = new System.Drawing.Point(607, 38);
            this._wordTextBox.Name = "_wordTextBox";
            this._wordTextBox.Size = new System.Drawing.Size(266, 20);
            this._wordTextBox.TabIndex = 4;
            this.theme1.SetTheme(this._wordTextBox, "(default)");
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Image = global::bangna_hospital.Properties.Resources.rules24;
            this.btnAnalyze.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnalyze.Location = new System.Drawing.Point(974, 12);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(76, 30);
            this.btnAnalyze.TabIndex = 552;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnAnalyze, "(default)");
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnRotate
            // 
            this.btnRotate.Image = global::bangna_hospital.Properties.Resources.rules24;
            this.btnRotate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRotate.Location = new System.Drawing.Point(879, 12);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(76, 30);
            this.btnRotate.TabIndex = 551;
            this.btnRotate.Text = "Rotate";
            this.btnRotate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnRotate, "(default)");
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // txtVisitDate
            // 
            this.txtVisitDate.AllowSpinLoop = false;
            this.txtVisitDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.txtVisitDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtVisitDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtVisitDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtVisitDate.Culture = 1054;
            this.txtVisitDate.CurrentTimeZone = false;
            this.txtVisitDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtVisitDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)((((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat | C1.Win.C1Input.FormatInfoInheritFlags.NullText) 
            | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtVisitDate.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.txtVisitDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate;
            this.txtVisitDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.txtVisitDate.GMTOffset = System.TimeSpan.Parse("00:00:00");
            this.txtVisitDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtVisitDate.Location = new System.Drawing.Point(490, 39);
            this.txtVisitDate.Name = "txtVisitDate";
            this.txtVisitDate.Size = new System.Drawing.Size(111, 18);
            this.txtVisitDate.TabIndex = 550;
            this.txtVisitDate.Tag = null;
            this.theme1.SetTheme(this.txtVisitDate, "(default)");
            this.txtVisitDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtVisitDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label6.Location = new System.Drawing.Point(411, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 549;
            this.label6.Text = "Visit Date :";
            this.theme1.SetTheme(this.label6, "(default)");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label2.Location = new System.Drawing.Point(10, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 544;
            this.label2.Text = "VN  :";
            this.theme1.SetTheme(this.label2, "(default)");
            // 
            // txtVN
            // 
            this.txtVN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtVN.Location = new System.Drawing.Point(48, 37);
            this.txtVN.Name = "txtVN";
            this.txtVN.Size = new System.Drawing.Size(150, 20);
            this.txtVN.TabIndex = 543;
            this.txtVN.Tag = null;
            this.theme1.SetTheme(this.txtVN, "(default)");
            this.txtVN.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtVN.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label1.Location = new System.Drawing.Point(497, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 542;
            this.label1.Text = "ประเภทเอกสาร  :";
            this.theme1.SetTheme(this.label1, "(default)");
            // 
            // cboDgs
            // 
            this.cboDgs.AllowSpinLoop = false;
            this.cboDgs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboDgs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboDgs.GapHeight = 0;
            this.cboDgs.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboDgs.ItemsDisplayMember = "";
            this.cboDgs.ItemsValueMember = "";
            this.cboDgs.Location = new System.Drawing.Point(598, 13);
            this.cboDgs.Name = "cboDgs";
            this.cboDgs.Size = new System.Drawing.Size(275, 20);
            this.cboDgs.TabIndex = 3;
            this.cboDgs.Tag = null;
            this.theme1.SetTheme(this.cboDgs, "(default)");
            this.cboDgs.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.cboDgs.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // txtNameFeMale
            // 
            this.txtNameFeMale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNameFeMale.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtNameFeMale.Location = new System.Drawing.Point(201, 13);
            this.txtNameFeMale.Name = "txtNameFeMale";
            this.txtNameFeMale.Size = new System.Drawing.Size(287, 20);
            this.txtNameFeMale.TabIndex = 541;
            this.txtNameFeMale.Tag = null;
            this.theme1.SetTheme(this.txtNameFeMale, "(default)");
            this.txtNameFeMale.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtNameFeMale.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.label4.Location = new System.Drawing.Point(10, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 16);
            this.label4.TabIndex = 539;
            this.label4.Text = "HN  :";
            this.theme1.SetTheme(this.label4, "(default)");
            // 
            // txtHn
            // 
            this.txtHn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHn.Location = new System.Drawing.Point(48, 13);
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(150, 20);
            this.txtHn.TabIndex = 538;
            this.txtHn.Tag = null;
            this.theme1.SetTheme(this.txtHn, "(default)");
            this.txtHn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtHn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // pnImg
            // 
            this.pnImg.BackColor = System.Drawing.Color.White;
            this.pnImg.Controls.Add(this.pic1);
            this.pnImg.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnImg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.pnImg.Location = new System.Drawing.Point(0, 66);
            this.pnImg.Name = "pnImg";
            this.pnImg.Size = new System.Drawing.Size(832, 663);
            this.pnImg.TabIndex = 3;
            this.theme1.SetTheme(this.pnImg, "(default)");
            // 
            // pic1
            // 
            this.pic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic1.Location = new System.Drawing.Point(0, 0);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(832, 663);
            this.pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic1.TabIndex = 0;
            this.pic1.TabStop = false;
            // 
            // pn
            // 
            this.pn.BackColor = System.Drawing.Color.White;
            this.pn.Controls.Add(this.btnSave);
            this.pn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.pn.Location = new System.Drawing.Point(832, 66);
            this.pn.Name = "pn";
            this.pn.Size = new System.Drawing.Size(257, 663);
            this.pn.TabIndex = 0;
            this.theme1.SetTheme(this.pn, "(default)");
            // 
            // btnSave
            // 
            this.btnSave.Image = global::bangna_hospital.Properties.Resources.download_database24;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(162, 612);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 39);
            this.btnSave.TabIndex = 537;
            this.btnSave.Text = "บันทึกช้อมูล";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnSave, "(default)");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // FrmScanAddView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 729);
            this.Controls.Add(this.pn);
            this.Controls.Add(this.pnImg);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmScanAddView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmScanView";
            this.Load += new System.EventHandler(this.FrmScanView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAnalyze)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRotate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVisitDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFeMale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            this.pnImg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            this.pn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController theme1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1TextBox txtVN;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1ComboBox cboDgs;
        private C1.Win.C1Input.C1TextBox txtNameFeMale;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Input.C1TextBox txtHn;
        private System.Windows.Forms.Panel pnImg;
        private System.Windows.Forms.Panel pn;
        private C1.Win.C1Input.C1Button btnSave;
        private C1.Win.C1Input.C1DateEdit txtVisitDate;
        private System.Windows.Forms.Label label6;
        private C1.Win.C1Input.C1Button btnRotate;
        private C1.Win.C1Input.C1Button btnAnalyze;
        private System.Windows.Forms.PictureBox pic1;
        private System.Windows.Forms.TextBox _wordTextBox;
        private System.Windows.Forms.Label label3;
        private C1.Win.C1Input.C1TextBox txtAn;
    }
}