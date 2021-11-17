
namespace bangna_hospital.gui
{
    partial class FrmReportNew
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
            this.arvMain = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
            this.SuspendLayout();
            // 
            // arvMain
            // 
            this.arvMain.CurrentPage = 0;
            this.arvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arvMain.Location = new System.Drawing.Point(0, 0);
            this.arvMain.Name = "arvMain";
            this.arvMain.PreviewPages = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            this.arvMain.Sidebar.ParametersPanel.ContextMenu = null;
            this.arvMain.Sidebar.ParametersPanel.Text = "Parameters";
            this.arvMain.Sidebar.ParametersPanel.Width = 200;
            // 
            // 
            // 
            this.arvMain.Sidebar.SearchPanel.ContextMenu = null;
            this.arvMain.Sidebar.SearchPanel.Text = "Search results";
            this.arvMain.Sidebar.SearchPanel.Width = 200;
            // 
            // 
            // 
            this.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = null;
            this.arvMain.Sidebar.ThumbnailsPanel.Text = "Page thumbnails";
            this.arvMain.Sidebar.ThumbnailsPanel.Width = 200;
            this.arvMain.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
            // 
            // 
            // 
            this.arvMain.Sidebar.TocPanel.ContextMenu = null;
            this.arvMain.Sidebar.TocPanel.Expanded = true;
            this.arvMain.Sidebar.TocPanel.Text = "Document map";
            this.arvMain.Sidebar.TocPanel.Width = 200;
            this.arvMain.Sidebar.Width = 200;
            this.arvMain.Size = new System.Drawing.Size(1013, 761);
            this.arvMain.TabIndex = 0;
            // 
            // FrmReportNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 761);
            this.Controls.Add(this.arvMain);
            this.Name = "FrmReportNew";
            this.Text = "FrmReportNew";
            this.Load += new System.EventHandler(this.FrmReportNew_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private GrapeCity.ActiveReports.Viewer.Win.Viewer arvMain;
    }
}