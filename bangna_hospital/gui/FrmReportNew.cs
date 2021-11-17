using GrapeCity.ActiveReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmReportNew : Form
    {
        String filename = "";
        public FrmReportNew(String filename)
        {
            InitializeComponent();
            this.filename = filename;
        }

        private void FrmReportNew_Load(object sender, EventArgs e)
        {
            try
            {
                // Setup a new instance of the AnnualReport.
                //AnnualReport rpt = new AnnualReport();
                //Run the report, and set it to the viewer control on the Form.
                //String filename = "";
                arvMain.LoadDocument(filename);
            }
            catch (ReportException ex)
            {
                MessageBox.Show(ex.Message, Text);
            }
        }
    }
}
