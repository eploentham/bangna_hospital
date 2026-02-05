namespace bangna_hospital.gui
{
    partial class FrmOpdRoomDoctor
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
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnDoctor = new System.Windows.Forms.Panel();
            this.pnRoom = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbDtrName = new System.Windows.Forms.Label();
            this.txtDtrSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbRoom = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 589);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.Size = new System.Drawing.Size(800, 22);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 589);
            this.panel2.TabIndex = 2;
            // 
            // pnDoctor
            // 
            this.pnDoctor.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnDoctor.Location = new System.Drawing.Point(0, 0);
            this.pnDoctor.Name = "pnDoctor";
            this.pnDoctor.Size = new System.Drawing.Size(296, 541);
            this.pnDoctor.TabIndex = 2;
            // 
            // pnRoom
            // 
            this.pnRoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnRoom.Location = new System.Drawing.Point(296, 0);
            this.pnRoom.Name = "pnRoom";
            this.pnRoom.Size = new System.Drawing.Size(504, 541);
            this.pnRoom.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbRoom);
            this.panel4.Controls.Add(this.lbDtrName);
            this.panel4.Controls.Add(this.txtDtrSearch);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(800, 48);
            this.panel4.TabIndex = 0;
            // 
            // lbDtrName
            // 
            this.lbDtrName.AutoSize = true;
            this.lbDtrName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbDtrName.Location = new System.Drawing.Point(307, 15);
            this.lbDtrName.Name = "lbDtrName";
            this.lbDtrName.Size = new System.Drawing.Size(21, 20);
            this.lbDtrName.TabIndex = 598;
            this.lbDtrName.Text = "...";
            // 
            // txtDtrSearch
            // 
            this.txtDtrSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDtrSearch.Location = new System.Drawing.Point(54, 11);
            this.txtDtrSearch.Name = "txtDtrSearch";
            this.txtDtrSearch.Size = new System.Drawing.Size(248, 26);
            this.txtDtrSearch.TabIndex = 597;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 20);
            this.label1.TabIndex = 596;
            this.label1.Text = "...";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnRoom);
            this.panel1.Controls.Add(this.pnDoctor);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 541);
            this.panel1.TabIndex = 3;
            // 
            // lbRoom
            // 
            this.lbRoom.AutoSize = true;
            this.lbRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbRoom.Location = new System.Drawing.Point(669, 15);
            this.lbRoom.Name = "lbRoom";
            this.lbRoom.Size = new System.Drawing.Size(21, 20);
            this.lbRoom.TabIndex = 599;
            this.lbRoom.Text = "...";
            // 
            // FrmOpdRoomDoctor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 611);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.c1StatusBar1);
            this.Name = "FrmOpdRoomDoctor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmOpdRoomDoctor";
            this.Load += new System.EventHandler(this.FrmOpdRoomDoctor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnRoom;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnDoctor;
        private System.Windows.Forms.TextBox txtDtrSearch;
        private System.Windows.Forms.Label lbDtrName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbRoom;
    }
}