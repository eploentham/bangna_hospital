namespace bangna_hospital.gui
{
    partial class FrmPrintCri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintCri));
            this.theme1 = new C1.Win.C1Themes.C1ThemeController();
            this.sB1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.c1DockingTab1 = new C1.Win.C1Command.C1DockingTab();
            this.tabPrn1 = new C1.Win.C1Command.C1DockingTabPage();
            this.tabPrn2 = new C1.Win.C1Command.C1DockingTabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1SplitContainer1 = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.c1SplitterPanel1 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.c1SplitterPanel2 = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.txtHn = new C1.Win.C1Input.C1TextBox();
            this.c1Label1 = new C1.Win.C1Input.C1Label();
            this.c1Button1 = new C1.Win.C1Input.C1Button();
            this.c1Combo1 = new C1.Win.C1List.C1Combo();
            this.c1CheckBox1 = new C1.Win.C1Input.C1CheckBox();
            this.c1CheckBox2 = new C1.Win.C1Input.C1CheckBox();
            this.c1CheckBox3 = new C1.Win.C1Input.C1CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sB1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).BeginInit();
            this.c1DockingTab1.SuspendLayout();
            this.tabPrn1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).BeginInit();
            this.c1SplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Button1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Combo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CheckBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CheckBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CheckBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // sB1
            // 
            this.sB1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.sB1.Location = new System.Drawing.Point(0, 654);
            this.sB1.Name = "sB1";
            this.sB1.Size = new System.Drawing.Size(978, 22);
            this.theme1.SetTheme(this.sB1, "(default)");
            // 
            // c1DockingTab1
            // 
            this.c1DockingTab1.Controls.Add(this.tabPrn1);
            this.c1DockingTab1.Controls.Add(this.tabPrn2);
            this.c1DockingTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1DockingTab1.Location = new System.Drawing.Point(0, 0);
            this.c1DockingTab1.Name = "c1DockingTab1";
            this.c1DockingTab1.SelectedIndex = 1;
            this.c1DockingTab1.Size = new System.Drawing.Size(978, 654);
            this.c1DockingTab1.TabIndex = 1;
            this.c1DockingTab1.TabsSpacing = 5;
            this.theme1.SetTheme(this.c1DockingTab1, "(default)");
            // 
            // tabPrn1
            // 
            this.tabPrn1.Controls.Add(this.c1SplitContainer1);
            this.tabPrn1.Controls.Add(this.panel1);
            this.tabPrn1.Location = new System.Drawing.Point(1, 24);
            this.tabPrn1.Name = "tabPrn1";
            this.tabPrn1.Size = new System.Drawing.Size(976, 629);
            this.tabPrn1.TabIndex = 0;
            this.tabPrn1.Text = "Criteria Print 1";
            // 
            // tabPrn2
            // 
            this.tabPrn2.Location = new System.Drawing.Point(1, 24);
            this.tabPrn2.Name = "tabPrn2";
            this.tabPrn2.Size = new System.Drawing.Size(976, 629);
            this.tabPrn2.TabIndex = 1;
            this.tabPrn2.Text = "Criteria Print 2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.c1CheckBox3);
            this.panel1.Controls.Add(this.c1CheckBox2);
            this.panel1.Controls.Add(this.c1CheckBox1);
            this.panel1.Controls.Add(this.c1Combo1);
            this.panel1.Controls.Add(this.c1Button1);
            this.panel1.Controls.Add(this.c1Label1);
            this.panel1.Controls.Add(this.txtHn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(976, 213);
            this.panel1.TabIndex = 0;
            this.theme1.SetTheme(this.panel1, "(default)");
            // 
            // c1SplitContainer1
            // 
            this.c1SplitContainer1.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.c1SplitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1SplitContainer1.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.c1SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SplitContainer1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.c1SplitContainer1.Location = new System.Drawing.Point(0, 213);
            this.c1SplitContainer1.Name = "c1SplitContainer1";
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel1);
            this.c1SplitContainer1.Panels.Add(this.c1SplitterPanel2);
            this.c1SplitContainer1.Size = new System.Drawing.Size(976, 416);
            this.c1SplitContainer1.TabIndex = 1;
            this.theme1.SetTheme(this.c1SplitContainer1, "(default)");
            // 
            // c1SplitterPanel1
            // 
            this.c1SplitterPanel1.Collapsible = true;
            this.c1SplitterPanel1.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            this.c1SplitterPanel1.Location = new System.Drawing.Point(0, 21);
            this.c1SplitterPanel1.Name = "c1SplitterPanel1";
            this.c1SplitterPanel1.Size = new System.Drawing.Size(479, 395);
            this.c1SplitterPanel1.TabIndex = 0;
            this.c1SplitterPanel1.Text = "Panel 1";
            this.c1SplitterPanel1.Width = 486;
            // 
            // c1SplitterPanel2
            // 
            this.c1SplitterPanel2.Height = 416;
            this.c1SplitterPanel2.Location = new System.Drawing.Point(490, 21);
            this.c1SplitterPanel2.Name = "c1SplitterPanel2";
            this.c1SplitterPanel2.Size = new System.Drawing.Size(486, 395);
            this.c1SplitterPanel2.TabIndex = 1;
            this.c1SplitterPanel2.Text = "Panel 2";
            // 
            // txtHn
            // 
            this.txtHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHn.Location = new System.Drawing.Point(51, 7);
            this.txtHn.Multiline = true;
            this.txtHn.Name = "txtHn";
            this.txtHn.Size = new System.Drawing.Size(552, 115);
            this.txtHn.TabIndex = 0;
            this.txtHn.Tag = null;
            this.theme1.SetTheme(this.txtHn, "(default)");
            // 
            // c1Label1
            // 
            this.c1Label1.AutoSize = true;
            this.c1Label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Label1.Location = new System.Drawing.Point(11, 10);
            this.c1Label1.Name = "c1Label1";
            this.c1Label1.Size = new System.Drawing.Size(34, 16);
            this.c1Label1.TabIndex = 1;
            this.c1Label1.Tag = null;
            this.theme1.SetTheme(this.c1Label1, "(default)");
            // 
            // c1Button1
            // 
            this.c1Button1.Location = new System.Drawing.Point(356, 129);
            this.c1Button1.Name = "c1Button1";
            this.c1Button1.Size = new System.Drawing.Size(100, 40);
            this.c1Button1.TabIndex = 2;
            this.c1Button1.Text = "c1Button1";
            this.theme1.SetTheme(this.c1Button1, "(default)");
            this.c1Button1.UseVisualStyleBackColor = true;
            // 
            // c1Combo1
            // 
            this.c1Combo1.AddItemSeparator = ';';
            this.c1Combo1.Caption = "";
            this.c1Combo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.c1Combo1.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.c1Combo1.EditorBackColor = System.Drawing.SystemColors.Window;
            this.c1Combo1.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Combo1.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.c1Combo1.Images.Add(((System.Drawing.Image)(resources.GetObject("c1Combo1.Images"))));
            this.c1Combo1.Location = new System.Drawing.Point(499, 147);
            this.c1Combo1.MatchEntryTimeout = ((long)(2000));
            this.c1Combo1.MaxDropDownItems = ((short)(5));
            this.c1Combo1.MaxLength = 32767;
            this.c1Combo1.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.c1Combo1.Name = "c1Combo1";
            this.c1Combo1.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1Combo1.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1Combo1.Size = new System.Drawing.Size(121, 21);
            this.c1Combo1.TabIndex = 3;
            this.c1Combo1.Text = "c1Combo1";
            this.theme1.SetTheme(this.c1Combo1, "(default)");
            this.c1Combo1.PropBag = resources.GetString("c1Combo1.PropBag");
            // 
            // c1CheckBox1
            // 
            this.c1CheckBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1CheckBox1.Location = new System.Drawing.Point(51, 126);
            this.c1CheckBox1.Name = "c1CheckBox1";
            this.c1CheckBox1.Size = new System.Drawing.Size(104, 24);
            this.c1CheckBox1.TabIndex = 4;
            this.c1CheckBox1.Text = "พิมพ์ ใบยา";
            this.theme1.SetTheme(this.c1CheckBox1, "(default)");
            this.c1CheckBox1.UseVisualStyleBackColor = true;
            this.c1CheckBox1.Value = null;
            // 
            // c1CheckBox2
            // 
            this.c1CheckBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1CheckBox2.Location = new System.Drawing.Point(51, 155);
            this.c1CheckBox2.Name = "c1CheckBox2";
            this.c1CheckBox2.Size = new System.Drawing.Size(104, 24);
            this.c1CheckBox2.TabIndex = 5;
            this.c1CheckBox2.Text = "c1CheckBox2";
            this.theme1.SetTheme(this.c1CheckBox2, "(default)");
            this.c1CheckBox2.UseVisualStyleBackColor = true;
            this.c1CheckBox2.Value = null;
            // 
            // c1CheckBox3
            // 
            this.c1CheckBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1CheckBox3.Location = new System.Drawing.Point(51, 184);
            this.c1CheckBox3.Name = "c1CheckBox3";
            this.c1CheckBox3.Size = new System.Drawing.Size(104, 24);
            this.c1CheckBox3.TabIndex = 6;
            this.c1CheckBox3.Text = "c1CheckBox3";
            this.theme1.SetTheme(this.c1CheckBox3, "(default)");
            this.c1CheckBox3.UseVisualStyleBackColor = true;
            this.c1CheckBox3.Value = null;
            // 
            // FrmPrintCri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 676);
            this.Controls.Add(this.c1DockingTab1);
            this.Controls.Add(this.sB1);
            this.Name = "FrmPrintCri";
            this.Text = "FrmPrintCri";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPrintCri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.theme1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sB1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).EndInit();
            this.c1DockingTab1.ResumeLayout(false);
            this.tabPrn1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitContainer1)).EndInit();
            this.c1SplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtHn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Button1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Combo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CheckBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CheckBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CheckBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private C1.Win.C1Themes.C1ThemeController theme1;
        private C1.Win.C1Ribbon.C1StatusBar sB1;
        private C1.Win.C1Command.C1DockingTab c1DockingTab1;
        private C1.Win.C1Command.C1DockingTabPage tabPrn1;
        private C1.Win.C1Command.C1DockingTabPage tabPrn2;
        private C1.Win.C1SplitContainer.C1SplitContainer c1SplitContainer1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel1;
        private C1.Win.C1SplitContainer.C1SplitterPanel c1SplitterPanel2;
        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1Input.C1Label c1Label1;
        private C1.Win.C1Input.C1TextBox txtHn;
        private C1.Win.C1Input.C1CheckBox c1CheckBox1;
        private C1.Win.C1List.C1Combo c1Combo1;
        private C1.Win.C1Input.C1Button c1Button1;
        private C1.Win.C1Input.C1CheckBox c1CheckBox3;
        private C1.Win.C1Input.C1CheckBox c1CheckBox2;
    }
}