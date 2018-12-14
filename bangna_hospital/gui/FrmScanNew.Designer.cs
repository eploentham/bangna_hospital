namespace bangna_hospital.gui
{
    partial class FrmScanNew
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
            this.sb1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.c1ComboBox1 = new C1.Win.C1Input.C1ComboBox();
            this.txtNameFeMale = new C1.Win.C1Input.C1TextBox();
            this.c1Button1 = new C1.Win.C1Input.C1Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.grfScan = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVN = new C1.Win.C1Input.C1TextBox();
            this.c1Button2 = new C1.Win.C1Input.C1Button();
            this.btnOpen = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFeMale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Button1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Button2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).BeginInit();
            this.SuspendLayout();
            // 
            // sb1
            // 
            this.sb1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.sb1.Location = new System.Drawing.Point(0, 712);
            this.sb1.Name = "sb1";
            this.sb1.Size = new System.Drawing.Size(1090, 22);
            this.theme1.SetTheme(this.sb1, "(default)");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.c1Button2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtVN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.c1ComboBox1);
            this.groupBox1.Controls.Add(this.txtNameFeMale);
            this.groupBox1.Controls.Add(this.c1Button1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1090, 66);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patient";
            this.theme1.SetTheme(this.groupBox1, "(default)");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label1.Location = new System.Drawing.Point(599, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 542;
            this.label1.Text = "ประเภทเอกสาร  :";
            this.theme1.SetTheme(this.label1, "(default)");
            // 
            // c1ComboBox1
            // 
            this.c1ComboBox1.AllowSpinLoop = false;
            this.c1ComboBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1ComboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.c1ComboBox1.GapHeight = 0;
            this.c1ComboBox1.ImagePadding = new System.Windows.Forms.Padding(0);
            this.c1ComboBox1.ItemsDisplayMember = "";
            this.c1ComboBox1.ItemsValueMember = "";
            this.c1ComboBox1.Location = new System.Drawing.Point(700, 13);
            this.c1ComboBox1.Name = "c1ComboBox1";
            this.c1ComboBox1.Size = new System.Drawing.Size(275, 20);
            this.c1ComboBox1.TabIndex = 3;
            this.c1ComboBox1.Tag = null;
            this.theme1.SetTheme(this.c1ComboBox1, "(default)");
            this.c1ComboBox1.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.c1ComboBox1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // txtNameFeMale
            // 
            this.txtNameFeMale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNameFeMale.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtNameFeMale.Location = new System.Drawing.Point(303, 13);
            this.txtNameFeMale.Name = "txtNameFeMale";
            this.txtNameFeMale.Size = new System.Drawing.Size(287, 20);
            this.txtNameFeMale.TabIndex = 541;
            this.txtNameFeMale.Tag = null;
            this.theme1.SetTheme(this.txtNameFeMale, "(default)");
            this.txtNameFeMale.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtNameFeMale.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // c1Button1
            // 
            this.c1Button1.Location = new System.Drawing.Point(269, 12);
            this.c1Button1.Name = "c1Button1";
            this.c1Button1.Size = new System.Drawing.Size(28, 23);
            this.c1Button1.TabIndex = 540;
            this.c1Button1.Text = "...";
            this.theme1.SetTheme(this.c1Button1, "(default)");
            this.c1Button1.UseVisualStyleBackColor = true;
            this.c1Button1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
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
            this.txtHn.Location = new System.Drawing.Point(113, 13);
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(150, 20);
            this.txtHn.TabIndex = 538;
            this.txtHn.Tag = null;
            this.theme1.SetTheme(this.txtHn, "(default)");
            this.txtHn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtHn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // grfScan
            // 
            this.grfScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grfScan.Location = new System.Drawing.Point(0, 66);
            this.grfScan.Name = "grfScan";
            this.grfScan.Size = new System.Drawing.Size(1090, 646);
            this.grfScan.TabIndex = 2;
            this.grfScan.TabStop = false;
            this.grfScan.Text = "File Scan";
            this.theme1.SetTheme(this.grfScan, "(default)");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
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
            this.txtVN.Location = new System.Drawing.Point(113, 37);
            this.txtVN.Name = "txtVN";
            this.txtVN.Size = new System.Drawing.Size(150, 20);
            this.txtVN.TabIndex = 543;
            this.txtVN.Tag = null;
            this.theme1.SetTheme(this.txtVN, "(default)");
            this.txtVN.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtVN.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // c1Button2
            // 
            this.c1Button2.Location = new System.Drawing.Point(269, 36);
            this.c1Button2.Name = "c1Button2";
            this.c1Button2.Size = new System.Drawing.Size(28, 23);
            this.c1Button2.TabIndex = 545;
            this.c1Button2.Text = "...";
            this.theme1.SetTheme(this.c1Button2, "(default)");
            this.c1Button2.UseVisualStyleBackColor = true;
            this.c1Button2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::bangna_hospital.Properties.Resources.Open_Large;
            this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpen.Location = new System.Drawing.Point(981, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(77, 43);
            this.btnOpen.TabIndex = 546;
            this.btnOpen.Text = "Open";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnOpen, "(default)");
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // FrmScanNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 734);
            this.Controls.Add(this.grfScan);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.sb1);
            this.Name = "FrmScanNew";
            this.Text = "FrmScanNew";
            this.Load += new System.EventHandler(this.FrmScanNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFeMale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Button1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Button2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController theme1;
        private C1.Win.C1Ribbon.C1StatusBar sb1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grfScan;
        private C1.Win.C1Input.C1TextBox txtNameFeMale;
        private C1.Win.C1Input.C1Button c1Button1;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Input.C1TextBox txtHn;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1ComboBox c1ComboBox1;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1TextBox txtVN;
        private C1.Win.C1Input.C1Button c1Button2;
        private C1.Win.C1Input.C1Button btnOpen;
    }
}