namespace bangna_hospital.gui
{
    partial class FrmToken
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
            this.tCMain = new C1.Win.C1Command.C1DockingTab();
            this.tabToken = new C1.Win.C1Command.C1DockingTabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTokenGen = new C1.Win.C1Input.C1Button();
            this.txtTokenQyt = new C1.Win.C1Input.C1NumericEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cboTokenType = new C1.Win.C1Input.C1ComboBox();
            this.lbTokenDtrName = new System.Windows.Forms.Label();
            this.tabView = new C1.Win.C1Command.C1DockingTabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cboTokenTypeView = new C1.Win.C1Input.C1ComboBox();
            this.lbViewDtrName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCMain)).BeginInit();
            this.tCMain.SuspendLayout();
            this.tabToken.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTokenGen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTokenQyt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTokenType)).BeginInit();
            this.tabView.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTokenTypeView)).BeginInit();
            this.SuspendLayout();
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 783);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.Size = new System.Drawing.Size(1150, 22);
            // 
            // tCMain
            // 
            this.tCMain.Controls.Add(this.tabToken);
            this.tCMain.Controls.Add(this.tabView);
            this.tCMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tCMain.Location = new System.Drawing.Point(0, 0);
            this.tCMain.Name = "tCMain";
            this.tCMain.Size = new System.Drawing.Size(1150, 783);
            this.tCMain.TabIndex = 1;
            this.tCMain.TabsSpacing = 5;
            // 
            // tabToken
            // 
            this.tabToken.Controls.Add(this.panel1);
            this.tabToken.Location = new System.Drawing.Point(1, 24);
            this.tabToken.Name = "tabToken";
            this.tabToken.Size = new System.Drawing.Size(1148, 758);
            this.tabToken.TabIndex = 0;
            this.tabToken.Text = "Page1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1148, 758);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 49);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1148, 709);
            this.panel3.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnTokenGen);
            this.panel2.Controls.Add(this.txtTokenQyt);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.cboTokenType);
            this.panel2.Controls.Add(this.lbTokenDtrName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1148, 49);
            this.panel2.TabIndex = 2;
            // 
            // btnTokenGen
            // 
            this.btnTokenGen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnTokenGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnTokenGen.Location = new System.Drawing.Point(1038, 6);
            this.btnTokenGen.Name = "btnTokenGen";
            this.btnTokenGen.Size = new System.Drawing.Size(99, 35);
            this.btnTokenGen.TabIndex = 292;
            this.btnTokenGen.Text = "gen token";
            this.btnTokenGen.UseVisualStyleBackColor = false;
            // 
            // txtTokenQyt
            // 
            this.txtTokenQyt.DataType = typeof(short);
            this.txtTokenQyt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtTokenQyt.ImagePadding = new System.Windows.Forms.Padding(0);
            this.txtTokenQyt.Location = new System.Drawing.Point(926, 10);
            this.txtTokenQyt.Name = "txtTokenQyt";
            this.txtTokenQyt.Size = new System.Drawing.Size(87, 24);
            this.txtTokenQyt.TabIndex = 291;
            this.txtTokenQyt.Tag = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(878, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 20);
            this.label1.TabIndex = 289;
            this.label1.Text = "qty";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label22.Location = new System.Drawing.Point(398, 11);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(101, 20);
            this.label22.TabIndex = 288;
            this.label22.Text = "ประเภท token";
            // 
            // cboTokenType
            // 
            this.cboTokenType.AllowSpinLoop = false;
            this.cboTokenType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboTokenType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboTokenType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboTokenType.GapHeight = 0;
            this.cboTokenType.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboTokenType.ItemsDisplayMember = "";
            this.cboTokenType.ItemsValueMember = "";
            this.cboTokenType.Location = new System.Drawing.Point(510, 6);
            this.cboTokenType.Name = "cboTokenType";
            this.cboTokenType.Size = new System.Drawing.Size(306, 29);
            this.cboTokenType.TabIndex = 287;
            this.cboTokenType.Tag = null;
            // 
            // lbTokenDtrName
            // 
            this.lbTokenDtrName.AutoSize = true;
            this.lbTokenDtrName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbTokenDtrName.Location = new System.Drawing.Point(11, 9);
            this.lbTokenDtrName.Name = "lbTokenDtrName";
            this.lbTokenDtrName.Size = new System.Drawing.Size(79, 24);
            this.lbTokenDtrName.TabIndex = 268;
            this.lbTokenDtrName.Text = "dtrname";
            // 
            // tabView
            // 
            this.tabView.Controls.Add(this.panel4);
            this.tabView.Controls.Add(this.panel5);
            this.tabView.Location = new System.Drawing.Point(1, 24);
            this.tabView.Name = "tabView";
            this.tabView.Size = new System.Drawing.Size(1148, 758);
            this.tabView.TabIndex = 1;
            this.tabView.Text = "Page2";
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 48);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1148, 710);
            this.panel4.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.cboTokenTypeView);
            this.panel5.Controls.Add(this.lbViewDtrName);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1148, 48);
            this.panel5.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(403, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 20);
            this.label3.TabIndex = 288;
            this.label3.Text = "ประเภท token";
            // 
            // cboTokenTypeView
            // 
            this.cboTokenTypeView.AllowSpinLoop = false;
            this.cboTokenTypeView.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboTokenTypeView.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboTokenTypeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboTokenTypeView.GapHeight = 0;
            this.cboTokenTypeView.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboTokenTypeView.ItemsDisplayMember = "";
            this.cboTokenTypeView.ItemsValueMember = "";
            this.cboTokenTypeView.Location = new System.Drawing.Point(510, 9);
            this.cboTokenTypeView.Name = "cboTokenTypeView";
            this.cboTokenTypeView.Size = new System.Drawing.Size(306, 29);
            this.cboTokenTypeView.TabIndex = 287;
            this.cboTokenTypeView.Tag = null;
            // 
            // lbViewDtrName
            // 
            this.lbViewDtrName.AutoSize = true;
            this.lbViewDtrName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbViewDtrName.Location = new System.Drawing.Point(11, 9);
            this.lbViewDtrName.Name = "lbViewDtrName";
            this.lbViewDtrName.Size = new System.Drawing.Size(79, 24);
            this.lbViewDtrName.TabIndex = 268;
            this.lbViewDtrName.Text = "dtrname";
            // 
            // FrmToken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 805);
            this.Controls.Add(this.tCMain);
            this.Controls.Add(this.c1StatusBar1);
            this.Name = "FrmToken";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmToken";
            this.Load += new System.EventHandler(this.FrmToken_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCMain)).EndInit();
            this.tCMain.ResumeLayout(false);
            this.tabToken.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTokenGen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTokenQyt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTokenType)).EndInit();
            this.tabView.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTokenTypeView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Command.C1DockingTab tCMain;
        private C1.Win.C1Command.C1DockingTabPage tabToken;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbTokenDtrName;
        private C1.Win.C1Input.C1ComboBox cboTokenType;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1NumericEdit txtTokenQyt;
        private C1.Win.C1Input.C1Button btnTokenGen;
        private C1.Win.C1Command.C1DockingTabPage tabView;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private C1.Win.C1Input.C1ComboBox cboTokenTypeView;
        private System.Windows.Forms.Label lbViewDtrName;
    }
}