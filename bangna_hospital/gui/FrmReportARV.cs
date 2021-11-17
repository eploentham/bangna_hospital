using GrapeCity.ActiveReports.Viewer.Win;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmReportARV:Form
    {
        Viewer arvMain;
        public FrmReportARV()
        {
            initConfig();
        }
        private void initConfig()
        {
            initCompoment();
            arvMain.LoadDocument("");
        }
        private void initCompoment()
        {
            arvMain = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
            this.arvMain.BackColor = System.Drawing.SystemColors.Control;
            this.arvMain.CurrentPage = 0;
            this.arvMain.Name = "arvMain";
            this.arvMain.PreviewPages = 0;
            this.arvMain.Sidebar.ParametersPanel.ContextMenu = null;
            this.arvMain.Sidebar.ParametersPanel.Width = 180;
            this.arvMain.Sidebar.SearchPanel.ContextMenu = null;
            this.arvMain.Sidebar.SearchPanel.Width = 180;
            this.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = null;
            this.arvMain.Sidebar.ThumbnailsPanel.Width = 180;
            this.arvMain.Sidebar.TocPanel.ContextMenu = null;
            this.arvMain.Sidebar.TocPanel.Width = 180;
            this.arvMain.Sidebar.Width = 180;
            arvMain.Dock = DockStyle.Fill;
            this.Controls.Add(this.arvMain);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrmReportARV
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "FrmReportARV";
            this.Load += new System.EventHandler(this.FrmReportARV_Load);
            this.ResumeLayout(false);

        }

        private void FrmReportARV_Load(object sender, EventArgs e)
        {

        }
    }
}
