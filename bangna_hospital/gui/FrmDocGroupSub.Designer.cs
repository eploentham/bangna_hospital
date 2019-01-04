namespace bangna_hospital.gui
{
    partial class FrmDocGroupSub
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
            this.sB = new System.Windows.Forms.StatusStrip();
            this.sB1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.theme1 = new C1.Win.C1Themes.C1ThemeController();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtPasswordVoid = new C1.Win.C1Input.C1TextBox();
            this.btnNew = new C1.Win.C1Input.C1Button();
            this.chkVoid = new C1.Win.C1Input.C1CheckBox();
            this.btnVoid = new C1.Win.C1Input.C1Button();
            this.btnEdit = new C1.Win.C1Input.C1Button();
            this.btnSave = new C1.Win.C1Input.C1Button();
            this.txtDocGroupSubName = new C1.Win.C1Input.C1TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID = new C1.Win.C1Input.C1TextBox();
            this.cboDocGroupName = new C1.Win.C1Input.C1ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordVoid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVoid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVoid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocGroupSubName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDocGroupName)).BeginInit();
            this.SuspendLayout();
            // 
            // sB
            // 
            this.sB.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sB1});
            this.sB.Location = new System.Drawing.Point(0, 674);
            this.sB.Name = "sB";
            this.sB.Size = new System.Drawing.Size(944, 22);
            this.sB.TabIndex = 4;
            this.sB.Text = "statusStrip1";
            // 
            // sB1
            // 
            this.sB1.Name = "sB1";
            this.sB1.Size = new System.Drawing.Size(118, 17);
            this.sB1.Text = "toolStripStatusLabel1";
            // 
            // theme1
            // 
            this.theme1.Theme = "Office2013Red";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(944, 674);
            this.panel1.TabIndex = 5;
            this.theme1.SetTheme(this.panel1, "(default)");
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.theme1.SetTheme(this.splitContainer1.Panel1, "(default)");
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.theme1.SetTheme(this.splitContainer1.Panel2, "(default)");
            this.splitContainer1.Size = new System.Drawing.Size(944, 674);
            this.splitContainer1.SplitterDistance = 362;
            this.splitContainer1.TabIndex = 0;
            this.theme1.SetTheme(this.splitContainer1, "(default)");
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(362, 674);
            this.panel2.TabIndex = 0;
            this.theme1.SetTheme(this.panel2, "(default)");
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cboDocGroupName);
            this.panel3.Controls.Add(this.txtPasswordVoid);
            this.panel3.Controls.Add(this.btnNew);
            this.panel3.Controls.Add(this.chkVoid);
            this.panel3.Controls.Add(this.btnVoid);
            this.panel3.Controls.Add(this.btnEdit);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.txtDocGroupSubName);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtID);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(578, 674);
            this.panel3.TabIndex = 0;
            this.theme1.SetTheme(this.panel3, "(default)");
            // 
            // txtPasswordVoid
            // 
            this.txtPasswordVoid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPasswordVoid.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtPasswordVoid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPasswordVoid.Location = new System.Drawing.Point(265, 286);
            this.txtPasswordVoid.Name = "txtPasswordVoid";
            this.txtPasswordVoid.PasswordChar = '*';
            this.txtPasswordVoid.Size = new System.Drawing.Size(78, 20);
            this.txtPasswordVoid.TabIndex = 259;
            this.txtPasswordVoid.Tag = null;
            this.theme1.SetTheme(this.txtPasswordVoid, "(default)");
            this.txtPasswordVoid.Visible = false;
            this.txtPasswordVoid.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnNew
            // 
            this.btnNew.Image = global::bangna_hospital.Properties.Resources.custom_reports24;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(426, 35);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(83, 39);
            this.btnNew.TabIndex = 245;
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
            this.chkVoid.Location = new System.Drawing.Point(265, 257);
            this.chkVoid.Name = "chkVoid";
            this.chkVoid.Padding = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.chkVoid.Size = new System.Drawing.Size(155, 24);
            this.chkVoid.TabIndex = 244;
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
            this.btnVoid.Location = new System.Drawing.Point(426, 268);
            this.btnVoid.Name = "btnVoid";
            this.btnVoid.Size = new System.Drawing.Size(83, 39);
            this.btnVoid.TabIndex = 243;
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
            this.btnEdit.Location = new System.Drawing.Point(426, 80);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(83, 39);
            this.btnEdit.TabIndex = 242;
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
            this.btnSave.Location = new System.Drawing.Point(426, 312);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 39);
            this.btnSave.TabIndex = 241;
            this.btnSave.Text = "บันทึกช้อมูล";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme1.SetTheme(this.btnSave, "(default)");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // txtDocGroupSubName
            // 
            this.txtDocGroupSubName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocGroupSubName.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtDocGroupSubName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDocGroupSubName.Location = new System.Drawing.Point(124, 89);
            this.txtDocGroupSubName.Name = "txtDocGroupSubName";
            this.txtDocGroupSubName.Size = new System.Drawing.Size(262, 20);
            this.txtDocGroupSubName.TabIndex = 238;
            this.txtDocGroupSubName.Tag = null;
            this.theme1.SetTheme(this.txtDocGroupSubName, "(default)");
            this.txtDocGroupSubName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label2.Location = new System.Drawing.Point(27, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 237;
            this.label2.Text = "กลุ่มย่อยเอกสาร :";
            this.theme1.SetTheme(this.label2, "(default)");
            // 
            // txtID
            // 
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtID.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtID.Location = new System.Drawing.Point(337, 29);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(30, 20);
            this.txtID.TabIndex = 236;
            this.txtID.Tag = null;
            this.theme1.SetTheme(this.txtID, "(default)");
            this.txtID.Visible = false;
            this.txtID.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
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
            this.cboDocGroupName.Location = new System.Drawing.Point(124, 63);
            this.cboDocGroupName.Name = "cboDocGroupName";
            this.cboDocGroupName.Size = new System.Drawing.Size(262, 20);
            this.cboDocGroupName.Style.DropDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cboDocGroupName.Style.DropDownBorderColor = System.Drawing.Color.Gainsboro;
            this.cboDocGroupName.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboDocGroupName.TabIndex = 548;
            this.cboDocGroupName.Tag = null;
            this.theme1.SetTheme(this.cboDocGroupName, "(default)");
            this.cboDocGroupName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label1.Location = new System.Drawing.Point(27, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 549;
            this.label1.Text = "กลุ่มเอกสาร :";
            this.theme1.SetTheme(this.label1, "(default)");
            // 
            // FrmDocGroupSub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 696);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sB);
            this.Name = "FrmDocGroupSub";
            this.Text = "FrmDocGroupSub";
            this.Load += new System.EventHandler(this.FrmDocGroupSub_Load);
            this.sB.ResumeLayout(false);
            this.sB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordVoid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVoid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVoid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocGroupSubName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDocGroupName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip sB;
        private System.Windows.Forms.ToolStripStatusLabel sB1;
        private C1.Win.C1Themes.C1ThemeController theme1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private C1.Win.C1Input.C1TextBox txtPasswordVoid;
        private C1.Win.C1Input.C1Button btnNew;
        private C1.Win.C1Input.C1CheckBox chkVoid;
        private C1.Win.C1Input.C1Button btnVoid;
        private C1.Win.C1Input.C1Button btnEdit;
        private C1.Win.C1Input.C1Button btnSave;
        private C1.Win.C1Input.C1TextBox txtDocGroupSubName;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1TextBox txtID;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1ComboBox cboDocGroupName;
    }
}