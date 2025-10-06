namespace bangna_hospital.gui
{
    partial class UCPatient
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbAttachNote = new System.Windows.Forms.Label();
            this.lbHN = new System.Windows.Forms.Label();
            this.lboperAge = new System.Windows.Forms.Label();
            this.m_picPhoto = new C1.Win.C1Input.C1PictureBox();
            this.lbPttFinNote = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_picPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // lbAttachNote
            // 
            this.lbAttachNote.AutoSize = true;
            this.lbAttachNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbAttachNote.Location = new System.Drawing.Point(4, 33);
            this.lbAttachNote.Name = "lbAttachNote";
            this.lbAttachNote.Size = new System.Drawing.Size(0, 20);
            this.lbAttachNote.TabIndex = 280;
            // 
            // lbHN
            // 
            this.lbHN.AutoSize = true;
            this.lbHN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbHN.Location = new System.Drawing.Point(4, 6);
            this.lbHN.Name = "lbHN";
            this.lbHN.Size = new System.Drawing.Size(0, 20);
            this.lbHN.TabIndex = 277;
            // 
            // lboperAge
            // 
            this.lboperAge.AutoSize = true;
            this.lboperAge.BackColor = System.Drawing.Color.White;
            this.lboperAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lboperAge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.lboperAge.Location = new System.Drawing.Point(433, 178);
            this.lboperAge.Name = "lboperAge";
            this.lboperAge.Size = new System.Drawing.Size(0, 20);
            this.lboperAge.TabIndex = 559;
            // 
            // m_picPhoto
            // 
            this.m_picPhoto.Location = new System.Drawing.Point(437, 3);
            this.m_picPhoto.Name = "m_picPhoto";
            this.m_picPhoto.Size = new System.Drawing.Size(143, 172);
            this.m_picPhoto.TabIndex = 560;
            this.m_picPhoto.TabStop = false;
            // 
            // lbPttFinNote
            // 
            this.lbPttFinNote.AutoSize = true;
            this.lbPttFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbPttFinNote.Location = new System.Drawing.Point(4, 74);
            this.lbPttFinNote.Name = "lbPttFinNote";
            this.lbPttFinNote.Size = new System.Drawing.Size(0, 20);
            this.lbPttFinNote.TabIndex = 564;
            // 
            // UCPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbPttFinNote);
            this.Controls.Add(this.m_picPhoto);
            this.Controls.Add(this.lboperAge);
            this.Controls.Add(this.lbAttachNote);
            this.Controls.Add(this.lbHN);
            this.Name = "UCPatient";
            this.Size = new System.Drawing.Size(585, 204);
            ((System.ComponentModel.ISupportInitialize)(this.m_picPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbAttachNote;
        private System.Windows.Forms.Label lbHN;
        private System.Windows.Forms.Label lboperAge;
        private C1.Win.C1Input.C1PictureBox m_picPhoto;
        private System.Windows.Forms.Label lbPttFinNote;
    }
}
