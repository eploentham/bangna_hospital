namespace bangna_hospital.gui
{
    partial class UCStaffNote
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
            this.c1SplitContainer1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.spLeft = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.spRight = new C1.Win.C1SplitContainer.C1SplitterPanel();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).BeginInit();
            this.c1SplitContainer1.SuspendLayout();
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
            this.c1SplitContainer1.Panels.Add(this.spLeft);
            this.c1SplitContainer1.Panels.Add(this.spRight);
            this.c1SplitContainer1.Size = new System.Drawing.Size(963, 689);
            this.c1SplitContainer1.TabIndex = 0;
            // 
            // spLeft
            // 
            this.spLeft.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.spLeft.Height = 689;
            this.spLeft.Location = new System.Drawing.Point(0, 21);
            this.spLeft.Name = "spLeft";
            this.spLeft.Size = new System.Drawing.Size(521, 668);
            this.spLeft.SizeRatio = 54.327D;
            this.spLeft.TabIndex = 0;
            this.spLeft.Text = "Panel 1";
            this.spLeft.Width = 521;
            // 
            // spRight
            // 
            this.spRight.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.spRight.Height = 689;
            this.spRight.Location = new System.Drawing.Point(525, 21);
            this.spRight.Name = "spRight";
            this.spRight.Size = new System.Drawing.Size(438, 668);
            this.spRight.TabIndex = 1;
            this.spRight.Text = "Panel 2";
            this.spRight.Width = 438;
            // 
            // UCStaffNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.c1SplitContainer1);
            this.Name = "UCStaffNote";
            this.Size = new System.Drawing.Size(963, 689);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).EndInit();
            this.c1SplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer1;
        private C1.Win.C1SplitContainer.C1SplitterPanel spLeft;
        private C1.Win.C1SplitContainer.C1SplitterPanel spRight;
    }
}
