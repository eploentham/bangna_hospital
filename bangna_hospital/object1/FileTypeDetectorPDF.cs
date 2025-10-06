using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class FileTypeDetectorPDF
    {
        public enum FileTypePDF
        {
            Unknown,
            PDF,
            JPEG,
            PNG,
            GIF,
            BMP
        }

        public static FileTypePDF DetectFileType(MemoryStream stream)
        {
            if (stream == null || stream.Length < 4)
                return FileTypePDF.Unknown;

            var fileType = GetFileTypeFromHeader(stream);

            if (fileType == FileTypePDF.Unknown)
            {
                // ลองตรวจสอบเพิ่มเติมด้วยวิธีอื่น
                if (IsValidImage(stream))
                {
                    // ถ้าเป็นรูปภาพแต่ไม่รู้ประเภท ให้ลองดูจาก content
                    return GuessImageType(stream);
                }

                if (IsValidPdf(stream))
                    return FileTypePDF.PDF;
            }
            return fileType;
        }

        private static FileTypePDF GetFileTypeFromHeader(MemoryStream stream)
        {
            stream.Position = 0;
            byte[] header = new byte[8];
            stream.Read(header, 0, header.Length);
            stream.Position = 0;

            // PDF: %PDF
            if (header[0] == 0x25 && header[1] == 0x50 && header[2] == 0x44 && header[3] == 0x46)
                return FileTypePDF.PDF;

            // JPEG: FF D8
            if (header[0] == 0xFF && header[1] == 0xD8)
                return FileTypePDF.JPEG;

            // PNG: 89 50 4E 47
            if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
                return FileTypePDF.PNG;

            // GIF: GIF
            if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46)
                return FileTypePDF.GIF;

            // BMP: BM
            if (header[0] == 0x42 && header[1] == 0x4D)
                return FileTypePDF.BMP;

            return FileTypePDF.Unknown;
        }

        // ฟังก์ชันที่หายไป - ตรวจสอบว่าเป็นรูปภาพหรือไม่
        private static bool IsValidImage(MemoryStream stream)
        {
            try
            {
                stream.Position = 0;
                using (var image = System.Drawing.Image.FromStream(stream))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                stream.Position = 0;
            }
        }

        // ฟังก์ชันที่หายไป - ตรวจสอบว่าเป็น PDF หรือไม่
        private static bool IsValidPdf(MemoryStream stream)
        {
            try
            {
                stream.Position = 0;
                byte[] buffer = new byte[4];
                stream.Read(buffer, 0, 4);
                stream.Position = 0;

                // ตรวจสอบ PDF signature
                return buffer[0] == 0x25 && buffer[1] == 0x50 && buffer[2] == 0x44 && buffer[3] == 0x46;
            }
            catch
            {
                return false;
            }
        }

        // ฟังก์ชันเสริม - เดาประเภทรูปภาพ
        private static FileTypePDF GuessImageType(MemoryStream stream)
        {
            try
            {
                stream.Position = 0;
                using (var image = System.Drawing.Image.FromStream(stream))
                {
                    if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                        return FileTypePDF.JPEG;
                    else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                        return FileTypePDF.PNG;
                    else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                        return FileTypePDF.GIF;
                    else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                        return FileTypePDF.BMP;
                }
            }
            catch
            {
                // ถ้าไม่สามารถเดาได้ ให้ return JPEG เป็น default
            }
            finally
            {
                stream.Position = 0;
            }

            return FileTypePDF.JPEG; // default สำหรับรูปภาพที่ไม่รู้ประเภท
        }
    }
}
