using C1.C1Pdf;
using System.IO;
namespace bangna_hospital.object1
{
    public static class PdfHelper
    {
        public static C1PdfDocument ClonePdf(C1PdfDocument originalPdf)
        {
            // Create a new C1PdfDocument
            C1PdfDocument clonedPdf = new C1PdfDocument();

            // Save the original PDF to a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                originalPdf.Save(ms);
                ms.Position = 0;
                //clonedPdf.
            }

            return clonedPdf;
        }
    }
}
