namespace bangna_hospital.gui
{
    partial class FrmAttachNote
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
            this.lbPttNameT = new System.Windows.Forms.Label();
            this.txtHN = new C1.Win.C1Input.C1TextBox();
            this.label68 = new System.Windows.Forms.Label();
            this.txtAttachNote = new C1.Win.C1Input.C1TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtHN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAttachNote)).BeginInit();
            this.SuspendLayout();
            // 
            // lbPttNameT
            // 
            this.lbPttNameT.AutoSize = true;
            this.lbPttNameT.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbPttNameT.Location = new System.Drawing.Point(170, 14);
            this.lbPttNameT.Name = "lbPttNameT";
            this.lbPttNameT.Size = new System.Drawing.Size(35, 25);
            this.lbPttNameT.TabIndex = 271;
            this.lbPttNameT.Text = "ชื่อ";
            // 
            // txtHN
            // 
            this.txtHN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHN.Location = new System.Drawing.Point(38, 12);
            this.txtHN.Name = "txtHN";
            this.txtHN.Size = new System.Drawing.Size(128, 27);
            this.txtHN.TabIndex = 270;
            this.txtHN.Tag = null;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label68.Location = new System.Drawing.Point(4, 15);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(32, 20);
            this.label68.TabIndex = 269;
            this.label68.Text = "HN";
            // 
            // txtAttachNote
            // 
            this.txtAttachNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAttachNote.Location = new System.Drawing.Point(108, 56);
            this.txtAttachNote.Multiline = true;
            this.txtAttachNote.Name = "txtAttachNote";
            this.txtAttachNote.Size = new System.Drawing.Size(610, 343);
            this.txtAttachNote.TabIndex = 277;
            this.txtAttachNote.Tag = null;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label74.Location = new System.Drawing.Point(6, 56);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(96, 20);
            this.label74.TabIndex = 276;
            this.label74.Text = "Attachnote :";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.PaleGreen;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(8, 354);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(93, 45);
            this.btnOk.TabIndex = 278;
            this.btnOk.Text = "save";
            this.btnOk.UseVisualStyleBackColor = false;
            // 
            // FrmAttachNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 405);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtAttachNote);
            this.Controls.Add(this.label74);
            this.Controls.Add(this.lbPttNameT);
            this.Controls.Add(this.txtHN);
            this.Controls.Add(this.label68);
            this.Name = "FrmAttachNote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAttachNote";
            this.Load += new System.EventHandler(this.FrmAttachNote_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtHN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAttachNote)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPttNameT;
        private C1.Win.C1Input.C1TextBox txtHN;
        private System.Windows.Forms.Label label68;
        private C1.Win.C1Input.C1TextBox txtAttachNote;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Button btnOk;
    }
}