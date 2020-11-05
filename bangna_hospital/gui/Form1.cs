using C1.C1Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital
{
    public partial class Form1 : Form
    {
		private Font _fontBody = new Font("Tahoma", 10);
		private Font _fontTitle = new Font("Tahoma", 15, FontStyle.Bold);
		private Font _fontHeader = new Font("Tahoma", 11, FontStyle.Bold);
		private StringFormat _sfRight;
		private StringFormat _sfRightCenter;
		public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //c1PdfDocumentSource1.
        }

        private void c1Button1_Click(object sender, EventArgs e)
        {
			String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "", anyr = "", vn1 = "", pathFolder = "", datetick = "";
			C1PdfDocument pdf = new C1PdfDocument();

			pdf.Clear();

			//get page rectangle, discount margins
			RectangleF rcPage = pdf.PageRectangle;
			rcPage.Inflate(-72, -92);

			//loop through selected categories
			int page = 0;

			//add page break, update page counter
			if (page > 0) pdf.NewPage();
			page++;

			pdf.DrawString("ใบงบหน้าสรุป", _fontTitle, Brushes.Blue, rcPage);


			datetick = DateTime.Now.Ticks.ToString();
			pathFolder = "D:\\" + datetick;
			if (!Directory.Exists(pathFolder))
			{
				Directory.CreateDirectory(pathFolder);
			}
			pdf.Save(pathFolder + "\\_summary.pdf");




			System.Diagnostics.Process.Start("explorer.exe", pathFolder);
		}
    }
}
