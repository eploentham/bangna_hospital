namespace bangna_hospital.gui
{
    partial class FrmLabOutReceiveView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.c1Label1 = new C1.Win.C1Input.C1Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkDateLabOut = new System.Windows.Forms.RadioButton();
            this.chkDateReq = new System.Windows.Forms.RadioButton();
            this.btnOk = new C1.Win.C1Input.C1Button();
            this.txtDateEnd = new C1.Win.C1Input.C1DateEdit();
            this.c1Label4 = new C1.Win.C1Input.C1Label();
            this.txtDateStart = new C1.Win.C1Input.C1DateEdit();
            this.c1Label3 = new C1.Win.C1Input.C1Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkDateReqHIS = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).BeginInit();
            this.SuspendLayout();
            // 
            // sb1
            // 
            this.sb1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.sb1.Location = new System.Drawing.Point(0, 428);
            this.sb1.Name = "sb1";
            this.sb1.Size = new System.Drawing.Size(990, 22);
            this.theme1.SetTheme(this.sb1, "(default)");
            this.sb1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Custom;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtHn);
            this.panel1.Controls.Add(this.c1Label1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.txtDateEnd);
            this.panel1.Controls.Add(this.c1Label4);
            this.panel1.Controls.Add(this.txtDateStart);
            this.panel1.Controls.Add(this.c1Label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(990, 55);
            this.panel1.TabIndex = 2;
            this.theme1.SetTheme(this.panel1, "(default)");
            // 
            // txtHn
            // 
            this.txtHn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHn.Location = new System.Drawing.Point(416, 10);
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(100, 24);
            this.txtHn.TabIndex = 17;
            this.txtHn.Tag = null;
            this.theme1.SetTheme(this.txtHn, "(default)");
            this.txtHn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtHn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // c1Label1
            // 
            this.c1Label1.AutoSize = true;
            this.c1Label1.BorderColor = System.Drawing.Color.Transparent;
            this.c1Label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1Label1.Location = new System.Drawing.Point(379, 16);
            this.c1Label1.Name = "c1Label1";
            this.c1Label1.Size = new System.Drawing.Size(32, 13);
            this.c1Label1.TabIndex = 16;
            this.c1Label1.Tag = null;
            this.theme1.SetTheme(this.c1Label1, "(default)");
            this.c1Label1.Value = "HN:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.chkDateReqHIS);
            this.panel3.Controls.Add(this.chkDateLabOut);
            this.panel3.Controls.Add(this.chkDateReq);
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel3.Location = new System.Drawing.Point(522, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(390, 37);
            this.panel3.TabIndex = 5;
            this.theme1.SetTheme(this.panel3, "(default)");
            // 
            // chkDateLabOut
            // 
            this.chkDateLabOut.AutoSize = true;
            this.chkDateLabOut.BackColor = System.Drawing.Color.Transparent;
            this.chkDateLabOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDateLabOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.chkDateLabOut.Location = new System.Drawing.Point(119, 8);
            this.chkDateLabOut.Name = "chkDateLabOut";
            this.chkDateLabOut.Size = new System.Drawing.Size(136, 20);
            this.chkDateLabOut.TabIndex = 1;
            this.chkDateLabOut.TabStop = true;
            this.chkDateLabOut.Text = "วันที่ รับผลจาก out lab";
            this.theme1.SetTheme(this.chkDateLabOut, "(default)");
            this.chkDateLabOut.UseVisualStyleBackColor = false;
            // 
            // chkDateReq
            // 
            this.chkDateReq.AutoSize = true;
            this.chkDateReq.BackColor = System.Drawing.Color.Transparent;
            this.chkDateReq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDateReq.ForeColor = System.Drawing.Color.Black;
            this.chkDateReq.Location = new System.Drawing.Point(3, 8);
            this.chkDateReq.Name = "chkDateReq";
            this.chkDateReq.Size = new System.Drawing.Size(101, 20);
            this.chkDateReq.TabIndex = 0;
            this.chkDateReq.TabStop = true;
            this.chkDateReq.Text = "วันที่ Request";
            this.theme1.SetTheme(this.chkDateReq, "(default)");
            this.chkDateReq.UseVisualStyleBackColor = false;
            // 
            // btnOk
            // 
            this.btnOk.Image = global::bangna_hospital.Properties.Resources.custom_reports24;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(914, 6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(73, 37);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "ดึงข้อมูล";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnOk, "(default)");
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // txtDateEnd
            // 
            this.txtDateEnd.AllowSpinLoop = false;
            this.txtDateEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.txtDateEnd.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateEnd.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtDateEnd.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtDateEnd.Culture = 1054;
            this.txtDateEnd.CurrentTimeZone = false;
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
            this.txtDateEnd.Location = new System.Drawing.Point(262, 12);
            this.txtDateEnd.Name = "txtDateEnd";
            this.txtDateEnd.Size = new System.Drawing.Size(111, 20);
            this.txtDateEnd.TabIndex = 14;
            this.txtDateEnd.Tag = null;
            this.theme1.SetTheme(this.txtDateEnd, "(default)");
            this.txtDateEnd.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtDateEnd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // c1Label4
            // 
            this.c1Label4.AutoSize = true;
            this.c1Label4.BorderColor = System.Drawing.Color.Transparent;
            this.c1Label4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1Label4.Location = new System.Drawing.Point(200, 16);
            this.c1Label4.Name = "c1Label4";
            this.c1Label4.Size = new System.Drawing.Size(51, 13);
            this.c1Label4.TabIndex = 13;
            this.c1Label4.Tag = null;
            this.theme1.SetTheme(this.c1Label4, "(default)");
            this.c1Label4.Value = "วันที่สิ้นสุด:";
            // 
            // txtDateStart
            // 
            this.txtDateStart.AllowSpinLoop = false;
            this.txtDateStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.txtDateStart.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtDateStart.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtDateStart.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtDateStart.CurrentTimeZone = false;
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
            this.txtDateStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateStart.GMTOffset = System.TimeSpan.Parse("00:00:00");
            this.txtDateStart.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtDateStart.Location = new System.Drawing.Point(80, 12);
            this.txtDateStart.Name = "txtDateStart";
            this.txtDateStart.Size = new System.Drawing.Size(111, 20);
            this.txtDateStart.TabIndex = 12;
            this.txtDateStart.Tag = null;
            this.theme1.SetTheme(this.txtDateStart, "(default)");
            this.txtDateStart.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.txtDateStart.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // c1Label3
            // 
            this.c1Label3.AutoSize = true;
            this.c1Label3.BorderColor = System.Drawing.Color.Transparent;
            this.c1Label3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1Label3.Location = new System.Drawing.Point(15, 16);
            this.c1Label3.Name = "c1Label3";
            this.c1Label3.Size = new System.Drawing.Size(51, 13);
            this.c1Label3.TabIndex = 11;
            this.c1Label3.Tag = null;
            this.theme1.SetTheme(this.c1Label3, "(default)");
            this.c1Label3.Value = "วันที่เริ่มต้น:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.panel2.Location = new System.Drawing.Point(0, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(990, 373);
            this.panel2.TabIndex = 3;
            this.theme1.SetTheme(this.panel2, "(default)");
            // 
            // chkDateReqHIS
            // 
            this.chkDateReqHIS.AutoSize = true;
            this.chkDateReqHIS.BackColor = System.Drawing.Color.Transparent;
            this.chkDateReqHIS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDateReqHIS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.chkDateReqHIS.Location = new System.Drawing.Point(261, 8);
            this.chkDateReqHIS.Name = "chkDateReqHIS";
            this.chkDateReqHIS.Size = new System.Drawing.Size(126, 20);
            this.chkDateReqHIS.TabIndex = 2;
            this.chkDateReqHIS.TabStop = true;
            this.chkDateReqHIS.Text = "วันที่ Request HIS";
            this.theme1.SetTheme(this.chkDateReqHIS, "(default)");
            this.chkDateReqHIS.UseVisualStyleBackColor = false;
            // 
            // FrmLabOutReceiveView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sb1);
            this.Name = "FrmLabOutReceiveView";
            this.Text = "FrmLabOutReceiveView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLabOutReceiveView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController theme1;
        private C1.Win.C1Ribbon.C1StatusBar sb1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private C1.Win.C1Input.C1DateEdit txtDateStart;
        private C1.Win.C1Input.C1Label c1Label3;
        private C1.Win.C1Input.C1DateEdit txtDateEnd;
        private C1.Win.C1Input.C1Label c1Label4;
        private C1.Win.C1Input.C1Button btnOk;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton chkDateLabOut;
        private System.Windows.Forms.RadioButton chkDateReq;
        private C1.Win.C1Input.C1Label c1Label1;
        private C1.Win.C1Input.C1TextBox txtHn;
        private System.Windows.Forms.RadioButton chkDateReqHIS;
    }
}