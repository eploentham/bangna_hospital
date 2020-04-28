using bangna_hospital.control;

using bangna_hospital.object1;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmScanAddView : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;
        String hn = "", vn = "", name = "", filename = "", visitDate="", dgs="";
        
        MemoryStream stream;
        Image img1=null;
        public FrmScanAddView(BangnaControl bc, String hn, String vn, String name, String filename, String dsg, String visitdate)
        {
            InitializeComponent();
            this.bc = bc;
            this.hn = hn;
            this.vn = vn;
            this.name = name;
            this.filename = filename;
            this.dgs = dsg;
            visitDate = visitdate;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            theme1.Theme = bc.iniC.themeApplication;
            btnRotate.Click += BtnRotate_Click;
            bc.bcDB.dgsDB.setCboDgs(cboDgs, "");
            btnSave.Click += BtnSave_Click;
            btnAnalyze.Click += BtnAnalyze_Click;
            this.FormClosing += FrmScanNewView_FormClosing;
            //theme1.SetTheme(sb1, "BeigeOne");

            //sb1.Text = "aaaaaaaaaa";

            txtHn.Value = hn;
            txtVN.Value = vn;
            txtPttName.Value = name;
            txtVisitDate.Value = visitDate;
            bc.setC1Combo(cboDgs, dgs);
            if (File.Exists(@filename))
            {
                Image img = Image.FromFile(filename);
                stream = new MemoryStream();
                img.Save(stream, ImageFormat.Jpeg);
                img.Dispose();
                img = Image.FromStream(stream);
                img1 = img;
                pic1.Image = img;
            }            
        }

        private void FrmScanNewView_FormClosing(object sender, FormClosingEventArgs e)
        {
            //throw new NotImplementedException();
            //if (formsOCREngine != null && formsOCREngine.IsStarted)
            //    formsOCREngine.Shutdown();
        }
        private void BtnAnalyze_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //Startup();
        }

        private void BtnRotate_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            String dgs = "", id = "";            
            try
            {
                filename = filename.Substring(filename.IndexOf('*') + 1);
                //Image img=null;
                //img.Save(stream, ImageFormat.Jpeg);
                //resizedImage = bc.RotateImage(img);
                img1 = bc.RotateImage(img1);
                //img.Dispose();
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                img1.Save(filename);
                //Bitmap bmp;
                //bmp = (Bitmap)img1;
                //bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                Image img2 = Image.FromFile(filename);
                pic1.Image = img2;
                
            }
            catch (Exception ex)
            {
                dgs = ex.Message;
            }

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void FrmScanView_Load(object sender, EventArgs e)
        {

        }
    }
}
