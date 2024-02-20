using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace bangna_hospital.control
{
    public class PdfControl
    {
        public void MergeFiles(string destinationFile, string[] sourceFiles)
        {
            if (System.IO.File.Exists(destinationFile))
                System.IO.File.Delete(destinationFile);

            string[] sSrcFile;
            sSrcFile = new string[2];

            string[] arr = new string[2];
            for (int i = 0; i <= sourceFiles.Length - 1; i++)
            {
                if (sourceFiles[i] != null)
                {
                    if (sourceFiles[i].Trim() != "")
                        arr[i] = sourceFiles[i].ToString();
                }
            }

            if (arr != null)
            {
                sSrcFile = new string[2];

                for (int ic = 0; ic <= arr.Length - 1; ic++)
                {
                    sSrcFile[ic] = arr[ic].ToString();
                }
            }
            try
            {
                int f = 0;

                PdfReader reader = new PdfReader(sSrcFile[f]);
                int n = reader.NumberOfPages;
                //Response.Write("There are " + n + " pages in the original file.");
                Document document = new Document(PageSize.A4);

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));

                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;

                int rotation;
                while (f < sSrcFile.Length)
                {
                    int i = 0;
                    while (i < n)
                    {
                        i++;

                        document.SetPageSize(PageSize.A4);
                        document.NewPage();
                        page = writer.GetImportedPage(reader, i);

                        rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                        //Response.Write("\n Processed page " + i);
                    }

                    f++;
                    if (f < sSrcFile.Length)
                    {
                        reader = new PdfReader(sSrcFile[f]);
                        n = reader.NumberOfPages;
                        //Response.Write("There are " + n + " pages in the original file.");
                    }
                }
                //Response.Write("Success");
                reader.Close();
                writer.Close();
                document.Close();
            }
            catch (Exception e)
            {
                //Response.Write(e.Message);
            }
        }
        public void MergeFileslab2(string destinationFile, string[] sourceFiles)
        {
            if (System.IO.File.Exists(destinationFile))
                System.IO.File.Delete(destinationFile);
            try
            {
                Document doc = new Document();
                PdfCopy writer = new PdfCopy(doc, new FileStream(destinationFile, FileMode.Create));
                if (writer == null)
                {
                    return;
                }
                doc.Open();
                foreach (string filename in sourceFiles)
                {
                    PdfReader reader = new PdfReader(filename);
                    reader.ConsolidateNamedDestinations();
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        PdfImportedPage page = writer.GetImportedPage(reader, i);
                        writer.AddPage(page);
                    }
                    reader.Close();
                }
                writer.Close();
                doc.Close();
            }
            catch (Exception e)
            {
                //Response.Write(e.Message);
            }
        }
        public void MergeFileslab(string destinationFile, string[] sourceFiles)
        {
            if (System.IO.File.Exists(destinationFile))
                System.IO.File.Delete(destinationFile);

            string[] sSrcFile;
            sSrcFile = new string[2];

            string[] arr = new string[2];
            for (int i = 0; i <= sourceFiles.Length - 1; i++)
            {
                if (sourceFiles[i] != null)
                {
                    if (sourceFiles[i].Trim() != "")
                        arr[i] = sourceFiles[i].ToString();
                }
            }

            if (arr != null)
            {
                sSrcFile = new string[2];

                for (int ic = 0; ic <= arr.Length - 1; ic++)
                {
                    sSrcFile[ic] = arr[ic].ToString();
                }
            }
            try
            {
                int f = 0;

                PdfReader reader = new PdfReader(sSrcFile[f]);
                int n = reader.NumberOfPages;
                //Response.Write("There are " + n + " pages in the original file.");
                Document document = new Document(PageSize.A4);

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));

                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;

                int rotation;
                while (f < sSrcFile.Length)
                {
                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        if(f==0)
                            document.SetPageSize(PageSize.A4);
                        else if (f == 1)
                            document.SetPageSize(PageSize.A5.Rotate());

                        document.NewPage();
                        page = writer.GetImportedPage(reader, i);

                        rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                        //Response.Write("\n Processed page " + i);
                    }

                    f++;
                    if (f < sSrcFile.Length)
                    {
                        reader.Close();
                        reader = new PdfReader(sSrcFile[f]);
                        n = reader.NumberOfPages;
                        //Response.Write("There are " + n + " pages in the original file.");
                    }
                }
                //Response.Write("Success");
                
                reader.Close();
                writer.Close();
                document.Close();
            }
            catch (Exception e)
            {
                //Response.Write(e.Message);
            }


        }
        public void MergeFileslab1(string destinationFile, string[] sourceFiles)
        {
            if (System.IO.File.Exists(destinationFile))
                System.IO.File.Delete(destinationFile);

            string[] sSrcFile;
            sSrcFile = new string[2];

            string[] arr = new string[2];
            for (int i = 0; i <= sourceFiles.Length - 1; i++)
            {
                if (sourceFiles[i] != null)
                {
                    if (sourceFiles[i].Trim() != "")
                        arr[i] = sourceFiles[i].ToString();
                }
            }

            if (arr != null)
            {
                sSrcFile = new string[2];

                for (int ic = 0; ic <= arr.Length - 1; ic++)
                {
                    sSrcFile[ic] = arr[ic].ToString();
                }
            }
            try
            {
                int f = 0;

                PdfReader reader = new PdfReader(sSrcFile[f]);
                int n = reader.NumberOfPages;
                //Response.Write("There are " + n + " pages in the original file.");
                Document document = new Document(PageSize.A4);

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));

                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;

                int rotation;
                while (f < sSrcFile.Length)
                {
                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        //if (f == 0)
                            document.SetPageSize(PageSize.A4);
                        //else if (f == 1)
                        //    document.SetPageSize(PageSize.A5.Rotate());

                        document.NewPage();
                        page = writer.GetImportedPage(reader, i);

                        rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                        //Response.Write("\n Processed page " + i);
                    }

                    f++;
                    if (f < sSrcFile.Length)
                    {
                        reader = new PdfReader(sSrcFile[f]);
                        n = reader.NumberOfPages;
                        //Response.Write("There are " + n + " pages in the original file.");
                    }
                }
                //Response.Write("Success");
                reader.Close();
                writer.Close();
                document.Close();
            }
            catch (Exception e)
            {
                //Response.Write(e.Message);
            }


        }
        public void MergeFileslab(string destinationFile, string sourceFiles, Stream sourStream)
        {
            if (System.IO.File.Exists(destinationFile))
                System.IO.File.Delete(destinationFile);

            try
            {
                int f = 0;

                PdfReader reader = new PdfReader(sourceFiles);
                int n = reader.NumberOfPages;
                //Response.Write("There are " + n + " pages in the original file.");
                Document document = new Document(PageSize.A4);

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));

                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;
                int rotation;
                int i = 0;
                while (i < n)
                {
                    i++;
                    document.SetPageSize(PageSize.A4);
                    document.NewPage();
                    page = writer.GetImportedPage(reader, i);
                    cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                    //Response.Write("\n Processed page " + i);
                }

                //Response.Write("Success");
                reader = null;
                sourStream.Seek(0, SeekOrigin.Begin);
                reader = new PdfReader(sourStream);
                n = reader.NumberOfPages;
                i = 0;
                while (i < n)
                {
                    i++;
                    document.SetPageSize(PageSize.A4);
                    document.NewPage();
                    page = writer.GetImportedPage(reader, i);
                    cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                    //Response.Write("\n Processed page " + i);
                }
                reader.Close();
                writer.Close();
                document.Close();
            }
            catch (Exception e)
            {
                //Response.Write(e.Message);
            }


        }
    }
}
