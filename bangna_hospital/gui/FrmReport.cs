using bangna_hospital.control;
using bangna_hospital.object1;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmReport:Form
    {
        Form frmRpt;
        BangnaControl bc;
        //ReportDocument rpt;
        //CrystalReportViewer crv;
        String reportname = "";
        Form frmParent;
        DataTable dt;
        public FrmReport(BangnaControl bc, Form this1, String reportname, DataTable dt)
        {
            this.bc = bc;
            //crv = new CrystalReportViewer();
            this.reportname = reportname;
            frmParent = this1;
            this.dt = dt;
            //MessageBox.Show("40 ", "");
            initConfig();
        }
        private void initConfig()
        {
            initCompoment();
            //MessageBox.Show("50 ", "");
            try
            {
                new LogWriter("d", "FrmReport initConfig reportname " + reportname);
                if (reportname.Equals("xray_result"))
                {
                    setRptXrayResult();
                }
                else if (reportname.Equals("lab_result"))
                {
                    setRptLabResult();
                }
                else if (reportname.Equals("pharmacy_result"))
                {
                    setRptLabResult();
                }
            }
            catch(Exception ex)
            {

            }
        }
        private void initCompoment()
        {
            //crv = new CrystalReportViewer();
            //this.crv.ActiveViewIndex = -1;
            //this.crv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.crv.Cursor = System.Windows.Forms.Cursors.Default;
            //this.crv.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.crv.Location = new System.Drawing.Point(0, 0);
            //this.crv.Name = "crv";
            //this.crv.Size = new System.Drawing.Size(800, 450);
            //this.crv.TabIndex = 0;
        }
        private void setRptXrayResult()
        {
            //Form frm = new Form();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            //MessageBox.Show("60 ", "");
            try
            {
                //DataTable dt = new DataTable();
                //rpt = new ReportDocument();
                //if (!File.Exists(reportname+".rpt"))
                //{
                //    MessageBox.Show("File not Found " + reportname + ".rpt", "");
                //}
                //rpt.Load(reportname + ".rpt");
                //rpt.SetDataSource(dt);
                //rpt.SetParameterValue("line1", bc.iniC.hostname);
                ////rpt.SetParameterValue("line2", "");
                ////rpt.SetParameterValue("line3", "");
                //crv.ReportSource = rpt;
                //crv.Refresh();
                //this.Controls.Add(crv);
                this.ShowDialog(frmParent);
            }
            catch(Exception ex)
            {
                String chk = ex.Message.ToString();
                MessageBox.Show("error " + ex.Message, "");
            }
        }
        private void setRptLabResult()
        {
            //Form frm = new Form();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            //MessageBox.Show("60 ", "");
            try
            {
                //DataTable dt = new DataTable();
                //rpt = new ReportDocument();
                //if (!File.Exists(reportname + ".rpt"))
                //{
                //    MessageBox.Show("File not Found " + reportname + ".rpt", "");
                //}
                //rpt.Load(reportname + ".rpt");
                //rpt.SetDataSource(dt);
                //rpt.SetParameterValue("line1", bc.iniC.hostname);
                ////rpt.SetParameterValue("line2", "");
                ////rpt.SetParameterValue("line3", "");
                //crv.ReportSource = rpt;
                //crv.Refresh();
                //this.Controls.Add(crv);
                this.ShowDialog(frmParent);
            }
            catch (Exception ex)
            {
                String chk = ex.Message.ToString();
                MessageBox.Show("error " + ex.Message, "");
            }
        }
        private void setRptPharmacyResult()
        {
            //Form frm = new Form();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            //MessageBox.Show("60 ", "");
            try
            {
                //DataTable dt = new DataTable();
                //rpt = new ReportDocument();
                //if (!File.Exists(reportname + ".rpt"))
                //{
                //    MessageBox.Show("File not Found " + reportname + ".rpt", "");
                //}
                //rpt.Load(reportname + ".rpt");
                //rpt.SetDataSource(dt);
                ////rpt.SetParameterValue("line1", "");
                ////rpt.SetParameterValue("line2", "");
                ////rpt.SetParameterValue("line3", "");
                //crv.ReportSource = rpt;
                //crv.Refresh();
                //this.Controls.Add(crv);
                this.ShowDialog(frmParent);
            }
            catch (Exception ex)
            {
                String chk = ex.Message.ToString();
                MessageBox.Show("error " + ex.Message, "");
            }
        }
    }
}
