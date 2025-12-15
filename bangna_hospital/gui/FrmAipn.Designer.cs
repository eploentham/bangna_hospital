
namespace bangna_hospital.gui
{
    partial class FrmAipn
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
            this.btnGenXML = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAuthorName = new System.Windows.Forms.TextBox();
            this.btnOpenXML = new System.Windows.Forms.Button();
            this.txtSessionNO = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAnNew = new System.Windows.Forms.CheckBox();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTO = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.lb1 = new C1.Win.C1Ribbon.RibbonLabel();
            this.lb2 = new C1.Win.C1Ribbon.RibbonLabel();
            this.rb1 = new C1.Win.C1Ribbon.RibbonLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAn = new System.Windows.Forms.TextBox();
            this.cboSubMType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lbPttName = new System.Windows.Forms.Label();
            this.ChkAn = new System.Windows.Forms.CheckBox();
            this.chkNoAdd = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkNew = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenXML
            // 
            this.btnGenXML.Location = new System.Drawing.Point(437, 16);
            this.btnGenXML.Name = "btnGenXML";
            this.btnGenXML.Size = new System.Drawing.Size(111, 51);
            this.btnGenXML.TabIndex = 0;
            this.btnGenXML.Text = "gen XML";
            this.btnGenXML.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "authorName";
            // 
            // txtAuthorName
            // 
            this.txtAuthorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAuthorName.Location = new System.Drawing.Point(126, 12);
            this.txtAuthorName.Name = "txtAuthorName";
            this.txtAuthorName.Size = new System.Drawing.Size(269, 26);
            this.txtAuthorName.TabIndex = 2;
            // 
            // btnOpenXML
            // 
            this.btnOpenXML.Location = new System.Drawing.Point(437, 73);
            this.btnOpenXML.Name = "btnOpenXML";
            this.btnOpenXML.Size = new System.Drawing.Size(111, 51);
            this.btnOpenXML.TabIndex = 3;
            this.btnOpenXML.Text = "open XML";
            this.btnOpenXML.UseVisualStyleBackColor = true;
            // 
            // txtSessionNO
            // 
            this.txtSessionNO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSessionNO.Location = new System.Drawing.Point(126, 44);
            this.txtSessionNO.Name = "txtSessionNO";
            this.txtSessionNO.Size = new System.Drawing.Size(269, 26);
            this.txtSessionNO.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "session NO";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAnNew);
            this.groupBox1.Controls.Add(this.btnSendEmail);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtSubject);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTO);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtFrom);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(17, 196);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 194);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Email";
            // 
            // chkAnNew
            // 
            this.chkAnNew.AutoSize = true;
            this.chkAnNew.Checked = true;
            this.chkAnNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkAnNew.Location = new System.Drawing.Point(359, 164);
            this.chkAnNew.Name = "chkAnNew";
            this.chkAnNew.Size = new System.Drawing.Size(114, 24);
            this.chkAnNew.TabIndex = 16;
            this.chkAnNew.Text = "AN new bn1";
            this.chkAnNew.UseVisualStyleBackColor = true;
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Location = new System.Drawing.Point(19, 121);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(111, 51);
            this.btnSendEmail.TabIndex = 12;
            this.btnSendEmail.Text = "Send Email";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(15, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Subject";
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSubject.Location = new System.Drawing.Point(85, 89);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(418, 26);
            this.txtSubject.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(15, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "TO";
            // 
            // txtTO
            // 
            this.txtTO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtTO.Location = new System.Drawing.Point(85, 57);
            this.txtTO.Name = "txtTO";
            this.txtTO.Size = new System.Drawing.Size(269, 26);
            this.txtTO.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(15, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "From";
            // 
            // txtFrom
            // 
            this.txtFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtFrom.Location = new System.Drawing.Point(85, 25);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(269, 26);
            this.txtFrom.TabIndex = 6;
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1StatusBar1.LeftPaneItems.Add(this.lb1);
            this.c1StatusBar1.LeftPaneItems.Add(this.lb2);
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 400);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.rb1);
            this.c1StatusBar1.Size = new System.Drawing.Size(557, 22);
            // 
            // lb1
            // 
            this.lb1.Name = "lb1";
            this.lb1.Text = "Label";
            // 
            // lb2
            // 
            this.lb2.Name = "lb2";
            this.lb2.Text = "Label";
            // 
            // rb1
            // 
            this.rb1.Name = "rb1";
            this.rb1.Text = "Label";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(6, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "ส่ง AN แก้ไขส่งใหม่";
            // 
            // txtAn
            // 
            this.txtAn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAn.Location = new System.Drawing.Point(158, 74);
            this.txtAn.Name = "txtAn";
            this.txtAn.Size = new System.Drawing.Size(93, 26);
            this.txtAn.TabIndex = 7;
            // 
            // cboSubMType
            // 
            this.cboSubMType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboSubMType.FormattingEnabled = true;
            this.cboSubMType.Items.AddRange(new object[] {
            "ADD",
            "AUD",
            "ADJ"});
            this.cboSubMType.Location = new System.Drawing.Point(108, 135);
            this.cboSubMType.Name = "cboSubMType";
            this.cboSubMType.Size = new System.Drawing.Size(82, 28);
            this.cboSubMType.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(6, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "SumMType";
            // 
            // lbPttName
            // 
            this.lbPttName.AutoSize = true;
            this.lbPttName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbPttName.Location = new System.Drawing.Point(196, 138);
            this.lbPttName.Name = "lbPttName";
            this.lbPttName.Size = new System.Drawing.Size(21, 20);
            this.lbPttName.TabIndex = 11;
            this.lbPttName.Text = "...";
            // 
            // ChkAn
            // 
            this.ChkAn.AutoSize = true;
            this.ChkAn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ChkAn.Location = new System.Drawing.Point(259, 78);
            this.ChkAn.Name = "ChkAn";
            this.ChkAn.Size = new System.Drawing.Size(170, 24);
            this.ChkAn.TabIndex = 12;
            this.ChkAn.Text = "ส่งข้อมูลแก้ไข หลายAN";
            this.ChkAn.UseVisualStyleBackColor = true;
            // 
            // chkNoAdd
            // 
            this.chkNoAdd.AutoSize = true;
            this.chkNoAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNoAdd.Location = new System.Drawing.Point(259, 108);
            this.chkNoAdd.Name = "chkNoAdd";
            this.chkNoAdd.Size = new System.Drawing.Size(144, 24);
            this.chkNoAdd.TabIndex = 14;
            this.chkNoAdd.Text = "ส่งใหม่ ไม่ใส่ ADD";
            this.chkNoAdd.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBox1.Location = new System.Drawing.Point(108, 167);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(269, 26);
            this.textBox1.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(6, 170);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "authorName";
            // 
            // chkNew
            // 
            this.chkNew.AutoSize = true;
            this.chkNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNew.Location = new System.Drawing.Point(259, 134);
            this.chkNew.Name = "chkNew";
            this.chkNew.Size = new System.Drawing.Size(129, 24);
            this.chkNew.TabIndex = 19;
            this.chkNew.Text = "เฉพาะข้อมูลใหม่";
            this.chkNew.UseVisualStyleBackColor = true;
            // 
            // FrmAipn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 422);
            this.Controls.Add(this.chkNew);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chkNoAdd);
            this.Controls.Add(this.ChkAn);
            this.Controls.Add(this.lbPttName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboSubMType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAn);
            this.Controls.Add(this.c1StatusBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSessionNO);
            this.Controls.Add(this.btnOpenXML);
            this.Controls.Add(this.txtAuthorName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenXML);
            this.Name = "FrmAipn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAipn";
            this.Load += new System.EventHandler(this.FrmAipn_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenXML;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAuthorName;
        private System.Windows.Forms.Button btnOpenXML;
        private System.Windows.Forms.TextBox txtSessionNO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Button btnSendEmail;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Ribbon.RibbonLabel lb1;
        private C1.Win.C1Ribbon.RibbonLabel rb1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAn;
        private System.Windows.Forms.ComboBox cboSubMType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbPttName;
        private System.Windows.Forms.CheckBox ChkAn;
        private System.Windows.Forms.CheckBox chkNoAdd;
        private System.Windows.Forms.CheckBox chkAnNew;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private C1.Win.C1Ribbon.RibbonLabel lb2;
        private System.Windows.Forms.CheckBox chkNew;
    }
}