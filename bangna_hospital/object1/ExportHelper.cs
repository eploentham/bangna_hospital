using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Export;
namespace bangna_hospital.object1
{
    class SectionExport
    {
        public string DisplayName { get; set; }
        public string Filter { get; set; }
        public string DefaultFileName { get; set; }
        public IDocumentExportEx Settings { get; set; }
        public void Export(SectionDocument sectionDocument,string filePath)
        {
            Settings.Export(sectionDocument, filePath);
        }
    }
}
