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
        String reportfilename = "";
        public String HN="", VN="", preno="", reqDate="";
        public DataTable DT = new DataTable();
        BangnaControl bc;
        public FrmReportNew(BangnaControl bc, String filename)
        {
            InitializeComponent();
            this.bc = bc;
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
                Parameter line1 = new Parameter();
                Parameter line2 = new Parameter();
                Parameter line3 = new Parameter();
                //runtime.Parameters.Add(line1);
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
