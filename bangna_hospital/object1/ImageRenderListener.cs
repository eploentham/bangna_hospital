using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Xobject;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace bangna_hospital.object1
{
    public class ImageRenderListener : IEventListener
    {
        public void EventOccurred(IEventData data, EventType type)
        {
            if (type == EventType.RENDER_IMAGE)
            {
                iText.Kernel.Pdf.Canvas.Parser.Data.ImageRenderInfo renderInfo = (iText.Kernel.Pdf.Canvas.Parser.Data.ImageRenderInfo)data;
                PdfImageXObject image = renderInfo.GetImage();
                byte[] imageBytes = image.GetImageBytes(true);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Bitmap bitmap = new Bitmap(ms);
                    DecodeQRCode(bitmap);
                }
            }
        }

        public ICollection<EventType> GetSupportedEvents()
        {
            return new HashSet<EventType> { EventType.RENDER_IMAGE };
        }

        private void DecodeQRCode(Bitmap bitmap)
        {
            IBarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            if (result != null)
            {
                Console.WriteLine("QR Code Text: " + result.Text);
            }
            else
            {
                Console.WriteLine("No QR code found in the image.");
            }
        }
    }
}
