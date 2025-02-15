namespace bangna_hospital.gui
{
    partial class FrmStaffNote
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
            this.c1SplitContainer1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.btnSave = new C1.Win.C1Input.C1Button();
            this.lbOperPttNameT = new System.Windows.Forms.Label();
            this.txtOperHN = new C1.Win.C1Input.C1TextBox();
            this.label68 = new System.Windows.Forms.Label();
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.picL = new C1.Win.C1Input.C1PictureBox();
            this.c1SplitterPanel3 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.picR = new C1.Win.C1Input.C1PictureBox();
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.lfSbMessage = new C1.Win.C1Ribbon.RibbonLabel();
            this.btnGet = new C1.Win.C1Input.C1Button();
            this.btnDel = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).BeginInit();
            this.c1SplitContainer1.SuspendLayout();
            this.c1SplitterPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperHN)).BeginInit();
            this.c1SplitterPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picL)).BeginInit();
            this.c1SplitterPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDel)).BeginInit();
            this.SuspendLayout();
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
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel1);
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel2);
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel3);
            this.c1SplitContainer1.Size = new System.Drawing.Size(1100, 757);
            this.c1SplitContainer1.TabIndex = 0;
            // 
            // c1SplitterPanel1
            // 
            this.c1SplitterPanel1.Controls.Add(this.btnDel);
            this.c1SplitterPanel1.Controls.Add(this.btnGet);
            this.c1SplitterPanel1.Controls.Add(this.btnSave);
            this.c1SplitterPanel1.Controls.Add(this.lbOperPttNameT);
            this.c1SplitterPanel1.Controls.Add(this.txtOperHN);
            this.c1SplitterPanel1.Controls.Add(this.label68);
            this.c1SplitterPanel1.Height = 62;
            this.c1SplitterPanel1.Location = new System.Drawing.Point(0, 21);
            this.c1SplitterPanel1.Name = "c1SplitterPanel1";
            this.c1SplitterPanel1.Size = new System.Drawing.Size(1100, 41);
            this.c1SplitterPanel1.SizeRatio = 8.258D;
            this.c1SplitterPanel1.TabIndex = 0;
            this.c1SplitterPanel1.Text = "Panel 1";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSave.Image = global::bangna_hospital.Properties.Resources.Save_large;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(986, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 33);
            this.btnSave.TabIndex = 281;
            this.btnSave.Text = "save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // lbOperPttNameT
            // 
            this.lbOperPttNameT.AutoSize = true;
            this.lbOperPttNameT.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbOperPttNameT.Location = new System.Drawing.Point(176, 5);
            this.lbOperPttNameT.Name = "lbOperPttNameT";
            this.lbOperPttNameT.Size = new System.Drawing.Size(35, 25);
            this.lbOperPttNameT.TabIndex = 271;
            this.lbOperPttNameT.Text = "ชื่อ";
            // 
            // txtOperHN
            // 
            this.txtOperHN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtOperHN.Location = new System.Drawing.Point(44, 3);
            this.txtOperHN.Name = "txtOperHN";
            this.txtOperHN.Size = new System.Drawing.Size(128, 27);
            this.txtOperHN.TabIndex = 270;
            this.txtOperHN.Tag = null;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label68.Location = new System.Drawing.Point(10, 6);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(32, 20);
            this.label68.TabIndex = 269;
            this.label68.Text = "HN";
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.Controls.Add(this.picL);
            this.c1SplitterPanel2.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(0, 87);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(548, 670);
            this.c1SplitterPanel2.TabIndex = 1;
            this.c1SplitterPanel2.Text = "Panel 2";
            this.c1SplitterPanel2.Width = 548;
            // 
            // picL
            // 
            this.picL.Location = new System.Drawing.Point(21, 20);
            this.picL.Name = "picL";
            this.picL.Size = new System.Drawing.Size(100, 50);
            this.picL.TabIndex = 0;
            this.picL.TabStop = false;
            // 
            // c1SplitterPanel3
            // 
            this.c1SplitterPanel3.Controls.Add(this.picR);
            this.c1SplitterPanel3.Height = 691;
            this.c1SplitterPanel3.Location = new System.Drawing.Point(552, 87);
            this.c1SplitterPanel3.Name = "c1SplitterPanel3";
            this.c1SplitterPanel3.Size = new System.Drawing.Size(548, 670);
            this.c1SplitterPanel3.TabIndex = 2;
            this.c1SplitterPanel3.Text = "Panel 3";
            // 
            // picR
            // 
            this.picR.Location = new System.Drawing.Point(22, 20);
            this.picR.Name = "picR";
            this.picR.Size = new System.Drawing.Size(100, 50);
            this.picR.TabIndex = 0;
            this.picR.TabStop = false;
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1StatusBar1.LeftPaneItems.Add(this.lfSbMessage);
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 757);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.Size = new System.Drawing.Size(1100, 22);
            // 
            // lfSbMessage
            // 
            this.lfSbMessage.Name = "lfSbMessage";
            this.lfSbMessage.Text = "Label";
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnGet.Image = global::bangna_hospital.Properties.Resources.fileopen;
            this.btnGet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGet.Location = new System.Drawing.Point(664, 3);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(102, 33);
            this.btnGet.TabIndex = 282;
            this.btnGet.Text = "ดึงรูปใหม่";
            this.btnGet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGet.UseVisualStyleBackColor = true;
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnDel.Image = global::bangna_hospital.Properties.Resources.DeleteRows_small;
            this.btnDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDel.Location = new System.Drawing.Point(878, 3);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(102, 33);
            this.btnDel.TabIndex = 283;
            this.btnDel.Text = "ลบรูป";
            this.btnDel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDel.UseVisualStyleBackColor = true;
            // 
            // FrmStaffNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 779);
            this.Controls.Add(this.c1SplitContainer1);
            this.Controls.Add(this.c1StatusBar1);
            this.Name = "FrmStaffNote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmStaffNote";
            this.Load += new System.EventHandler(this.FrmStaffNote_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).EndInit();
            this.c1SplitContainer1.ResumeLayout(false);
            this.c1SplitterPanel1.ResumeLayout(false);
            this.c1SplitterPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperHN)).EndInit();
            this.c1SplitterPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picL)).EndInit();
            this.c1SplitterPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel3;
        private C1.Win.C1Input.C1PictureBox picL;
        private C1.Win.C1Input.C1PictureBox picR;
        private System.Windows.Forms.Label lbOperPttNameT;
        private C1.Win.C1Input.C1TextBox txtOperHN;
        private System.Windows.Forms.Label label68;
        private C1.Win.C1Input.C1Button btnSave;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Ribbon.RibbonLabel lfSbMessage;
        private C1.Win.C1Input.C1Button btnGet;
        private C1.Win.C1Input.C1Button btnDel;
    }
}