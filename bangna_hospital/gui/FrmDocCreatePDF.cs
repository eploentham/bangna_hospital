using bangna_hospital.control;
using bangna_hospital.Properties;
using C1.Win.C1Input;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmDocCreatePDF:Form
    {
        BangnaControl bc;

        Font fEdit, fEditB, fEdit3B, fEdit5B;
        Panel pnTop;

        Label lbTxtFilename;
        C1TextBox txtFilename;
        C1Button btnGenPdf;
        public FrmDocCreatePDF(BangnaControl bc, String hn, String pathFolder)
        {
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            initCompoment();
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            //fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 7, FontStyle.Regular);

            this.Load += FrmDocCreatePDF_Load;
        }

        private void initCompoment()
        {
            int gapLine = 20, gapX = 20, gapY=20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;

            pnTop = new Panel();
            pnTop.Dock = DockStyle.Fill;

            lbTxtFilename = new Label();
            bc.setControlLabel(ref lbTxtFilename, fEdit, "File Name", "lbTxtFilename", gapX, gapY);

            txtFilename = new C1TextBox();
            bc.setControlC1TextBox(ref txtFilename, fEdit, "txtFilename", 250, lbTxtFilename.Location.X + lbTxtFilename.Width, gapY);

            gapLine += 40;
            btnGenPdf = new C1Button();
            btnGenPdf.Name = "btnGenPdf";
            btnGenPdf.Text = "Save";
            btnGenPdf.Font = fEdit;
            //size = bc.MeasureString(btnHnSearch);
            btnGenPdf.Location = new System.Drawing.Point(gapX, gapLine);
            btnGenPdf.Size = new Size(200, 140);
            btnGenPdf.Font = fEdit;
            btnGenPdf.Image = Resources.Save_small;
            btnGenPdf.TextAlign = ContentAlignment.MiddleRight;
            btnGenPdf.ImageAlign = ContentAlignment.MiddleLeft;
            btnGenPdf.Click += BtnGenPdf_Click;

            pnTop.Controls.Add(lbTxtFilename);
            pnTop.Controls.Add(txtFilename);
            this.Controls.Add(pnTop);
        }

        private void BtnGenPdf_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            string filename;
            ArrayList selected = new ArrayList();
            


            MemoryStream[] streams = null;
            byte[] bytes;
            using (MemoryStream finalStream = new MemoryStream())
            {
                PdfCopyFields copy = new PdfCopyFields(finalStream);

                foreach (MemoryStream ms in streams)
                {
                    ms.Position = 0;
                    //Add it to the copy object
                    copy.AddDocument(new PdfReader(ms));
                    //Clean up
                    ms.Dispose();
                }

                copy.Close();
                bytes = finalStream.ToArray();
            }
            string outputFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Combined.pdf");
            using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fs.Write(bytes, 0, bytes.Length);
            }


        }

        private void FrmDocCreatePDF_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(600, 400);
        }
    }
}
