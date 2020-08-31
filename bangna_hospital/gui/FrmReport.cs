using bangna_hospital.control;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmReport:Form
    {
        Form frmRpt;
        BangnaControl bc;
        ReportDocument rpt;
        CrystalReportViewer crv;
        String reportname = "";
        Form frmParent;
        DataTable dt;
        public FrmReport(BangnaControl bc, Form this1, String reportname, DataTable dt)
        {
            this.bc = bc;
            crv = new CrystalReportViewer();
            this.reportname = reportname;
            frmParent = this1;
            this.dt = dt;
            initConfig();
        }
        private void initConfig()
        {
            initCompoment();
            try
            {
                if (reportname.Equals("xray_result"))
                {
                    setRptXrayResult();
                }
            }
            catch(Exception ex)
            {

            }
        }
        private void initCompoment()
        {
            this.crv.ActiveViewIndex = -1;
            this.crv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crv.Cursor = System.Windows.Forms.Cursors.Default;
            this.crv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crv.Location = new System.Drawing.Point(0, 0);
            this.crv.Name = "crv";
            this.crv.Size = new System.Drawing.Size(800, 450);
            this.crv.TabIndex = 0;
        }
        private void setRptXrayResult()
        {
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.WindowState = FormWindowState.Maximized;

            //DataTable dt = new DataTable();
            rpt = new ReportDocument();
            rpt.Load(reportname+".rpt");
            rpt.SetDataSource(dt);
            rpt.SetParameterValue("line1", "");
            rpt.SetParameterValue("line2", "");
            crv.ReportSource = rpt;
            crv.Refresh();
            this.Controls.Add(frm);
            frm.ShowDialog(frmParent);
        }
    }
}
