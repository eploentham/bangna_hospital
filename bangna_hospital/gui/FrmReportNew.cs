using bangna_hospital.control;
using bangna_hospital.object1;
using C1.C1Rdl.Rdl2008;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Data;

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
        public String reportfilename = "";
        public String HN="", VN="", preno="", reqDate="";
        public DataTable DT = new DataTable();
        public String LINE1 = "", LINE2 = "", LINE3 = "";
        BangnaControl bc;
        public FrmReportNew(BangnaControl bc, String filename)
        {
            InitializeComponent();
            this.bc = bc;
            this.reportfilename = filename;
        }
        public FrmReportNew(BangnaControl bc, String filename, String line1, String line2, String line3)
        {
            InitializeComponent();
            this.bc = bc;
            this.LINE1 = line1;
            this.LINE2 = line2;
            this.LINE3 = line3;
            this.reportfilename = filename;
        }
        private void FrmReportNew_Load(object sender, EventArgs e)
        {
            try
            {
                // Setup a new instance of the AnnualReport.
                //AnnualReport rpt = new AnnualReport();
                //Run the report, and set it to the viewer control on the Form.
                //String filename = "";
                //Parameter myParam1 = new Parameter();

                System.IO.FileInfo rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory()+"\\report\\" +reportfilename+ ".rdlx");
                PageReport definition = new PageReport(rptPath);
                GrapeCity.ActiveReports.Document.PageDocument runtime = new GrapeCity.ActiveReports.Document.PageDocument(definition);

                // With the following, using GrapeCity.ActiveReports.Expressions.ExpressionObjectModel.Parameter:
                runtime.Parameters["line1"].CurrentValue = LINE1.Length > 0 ? LINE1 : "";
                runtime.Parameters["line2"].CurrentValue = LINE2.Length > 0 ? LINE2 : "";
                runtime.Parameters["line3"].CurrentValue = LINE3.Length > 0 ? LINE3 : "";
                // If you need to set more parameters, use their names and assign values similarly:
                // runtime.Parameters["line2"].CurrentValue = "your_value_here";
                // runtime.Parameters["line3"].CurrentValue = "your_value_here";
                //runtime.Parameters.Add(line1);
                //runtime.Parameters.Add(line1);
                runtime.LocateDataSource += Runtime_LocateDataSource;
                //arvMain.LoadDocument(filename);
                arvMain.LoadDocument(runtime);
            }
            catch (ReportException ex)
            {
                MessageBox.Show(ex.Message, Text);
            }
        }

        private void Runtime_LocateDataSource(object sender, LocateDataSourceEventArgs args)
        {
            //throw new NotImplementedException();
            DataTable dt = new DataTable();
            if (reportfilename.Equals("lab_result_1"))
            {
                //dt = setPrintLabPrnSSO(vn, preno, chk + "-" + mm + "-" + dd);
                args.Data = DT;
            }
            else
            {
                args.Data = DT;
            }
        }
        private void LabReportExportDocument()
        {
            //ExportForm exportForm = new ExportForm(arvMain.Document);
            //exportForm.ShowDialog(this);
        }
        public Boolean PrintReportNoLINE()
        {
            Boolean chk = false;
            String err = "00";
            try
            {
                System.IO.FileInfo rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\" + reportfilename + ".rdlx");
                PageReport definition = new PageReport(rptPath);
                err = "01";
                GrapeCity.ActiveReports.Document.PageDocument runtime = new GrapeCity.ActiveReports.Document.PageDocument(definition);
                err = "02";
                //runtime.Parameters["line1"].CurrentValue = LINE1.Length > 0 ? LINE1 : "";
                //runtime.Parameters["line2"].CurrentValue = LINE2.Length > 0 ? LINE2 : "";
                //runtime.Parameters["line3"].CurrentValue = LINE3.Length > 0 ? LINE3 : "";
                err = "03";
                runtime.LocateDataSource += Runtime_LocateDataSource1;
                err = "04";
                runtime.Print(false);
                chk = true;
                definition.Dispose();
                runtime.Dispose();
            }
            catch (Exception ex)
            {
                chk = false;
                new LogWriter("e", this.Name + " PrintReport   " + err + " " + ex.Message);
            }
            return chk;
        }
        public Boolean PrintReport()
        {
            Boolean chk=false;
            String err = "00";
            try
            {
                System.IO.FileInfo rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\" + reportfilename + ".rdlx");
                PageReport definition = new PageReport(rptPath);
                err = "01";
                GrapeCity.ActiveReports.Document.PageDocument runtime = new GrapeCity.ActiveReports.Document.PageDocument(definition);
                err = "02";
                runtime.Parameters["line1"].CurrentValue = LINE1.Length > 0 ? LINE1 : "";
                runtime.Parameters["line2"].CurrentValue = LINE2.Length > 0 ? LINE2 : "";
                runtime.Parameters["line3"].CurrentValue = LINE3.Length > 0 ? LINE3 : "";
                err = "03";
                runtime.LocateDataSource += Runtime_LocateDataSource1;
                err = "04";
                runtime.Print(false);
                chk = true;
                definition.Dispose();
                runtime.Dispose();
            }
            catch (Exception ex)
            {
                chk=false;
                new LogWriter("e", this.Name + " PrintReport   "+ err +" "+ ex.Message);
            }
            return chk;
        }
        public void ExportReport(String filename)
        {
            try
            {
                System.IO.FileInfo rptPath = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\report\\" + reportfilename + ".rdlx");
                PageReport definition = new PageReport(rptPath);
                GrapeCity.ActiveReports.Document.PageDocument runtime = new GrapeCity.ActiveReports.Document.PageDocument(definition);

                //runtime.LocateDataSource += Runtime_LocateDataSource;
                runtime.LocateDataSource += Runtime_LocateDataSource1;
                //arvMain.LoadDocument(rptPath.FullName);

                GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport PdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                PdfExport1.Export(runtime, filename);

                //arvMain.LoadDocument(runtime);
                Application.DoEvents();

                //List<SectionExport> sectionExports = new List<SectionExport>();
                //sectionExports.Add(new SectionExport
                ////{
                ////    DisplayName = Properties.Resources.SectionHTMLString,
                ////    Filter = Properties.Resources.HTMLFilter,
                ////    DefaultFileName = Properties.Resources.HTMLFileName,
                ////    Settings = new Export.Html.Section.HtmlExport()
                ////});
                ////sectionExports.Add(new SectionExport
                //{
                //    DisplayName = Properties.Resources.SectionPDFString,
                //    Filter = Properties.Resources.PDFFilter,
                //    DefaultFileName = Properties.Resources.PDFFileName,
                //    Settings = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport()
                //});
                
                //this.Show();
                //Application.DoEvents();
                //((SectionExport)sectionExports[0]).Export(arvMain.Document, filename);
            }
            catch (ReportException ex)
            {
                MessageBox.Show(ex.Message, Text);
            }
            
            //sectionExports.Add(new SectionExport
            //{
            //    DisplayName = Properties.Resources.SectionRTFString,
            //    Filter = Properties.Resources.RtfFilter,
            //    DefaultFileName = Properties.Resources.RtfFileName,
            //    Settings = new Export.Word.Section.RtfExport()
            //});
            //sectionExports.Add(new SectionExport
            //{
            //    DisplayName = Properties.Resources.SectionTIFFString,
            //    Filter = Properties.Resources.TiffFilter,
            //    DefaultFileName = Properties.Resources.TiffFileName,
            //    Settings = new Export.Image.Tiff.Section.TiffExport()
            //});
            //sectionExports.Add(new SectionExport
            //{
            //    DisplayName = Properties.Resources.SectionPlainTextString,
            //    Filter = Properties.Resources.TextFilter,
            //    DefaultFileName = Properties.Resources.TextFileName,
            //    Settings = new Export.Xml.Section.TextExport()
            //});
            //sectionExports.Add(new SectionExport
            //{
            //    DisplayName = Properties.Resources.SectionExcelString,
            //    Filter = Properties.Resources.ExcelFilter,
            //    DefaultFileName = Properties.Resources.ExcelFileName,
            //    Settings = new XlsExport()
            //});
            //cmbExportFormat.DataSource = sectionExports;
            //cmbExportFormat.DisplayMember = "DisplayName";
            //cmbExportFormat.SelectedIndex = 0;
        }

        private void Runtime_LocateDataSource1(object sender, LocateDataSourceEventArgs args)
        {
            //throw new NotImplementedException();
            args.Data = DT;
        }
    }
}
