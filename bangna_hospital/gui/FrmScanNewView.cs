using bangna_hospital.control;

using bangna_hospital.object1;
using Leadtools;
using Leadtools.Codecs;
using Leadtools.Controls;
using Leadtools.Forms.Auto;
using Leadtools.Forms.Processing;
using Leadtools.Forms.Recognition;
using Leadtools.Forms.Recognition.Ocr;
using Leadtools.Ocr;
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
    public partial class FrmScanNewView : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;
        String hn = "", vn = "", name = "", filename = "", visitDate="", dgs="";

        // The RasterCodecs instance used to load/save images
        private RasterCodecs _rasterCodecs;
        // The OCR engine instance used in this demo
        private IOcrEngine _ocrEngine;
        // The current OCR document
        private IOcrDocument _ocrDocument;
        // The current OCR page in the viewer
        private IOcrPage _ocrPage;
        // The current recognized characters
        private IOcrPageCharacters _ocrPageCharacters;
        // The current recognized words
        private List<List<OcrWord>> _ocrZoneWords;
        // Selected word index into _ocrZoneWords
        private int _selectedZoneIndex;
        // Selected word index into the _ocrZoneWords[_selectedZoneIndex];
        private int _selectedWordIndex;

        // Last document we opened correctly
        private string _lastDocumentFile;
        // Minimum and maximum scale percentages allowed
        private const double _minimumViewerScalePercentage = 1;
        private const double _maximumViewerScalePercentage = 6400;
        // Extra pixels to edge around the word when clicking/highlighting
        private const int _wordEdge = 2;

        private string _openInitialPath = string.Empty;

        ImageViewer _imageViewer;

        MemoryStream stream;
        Image img1=null;
        string MY_LICENSE_FILE = @"LEADTOOLS.lic";
        string MY_DEVELOPER_KEY = "gMxMXs9T3paebVPDRdEyk4CRX8BNLmMIvN383qJp6jProMPYamOe136YzHr+CmFEOZzOcuiabiSFpOJGrOHJlx8jHKErnx/u";

        //Create the recognition engine 
        
        //Create the OCR Engine to use in the recognition 
        IOcrEngine formsOCREngine;
        public FrmScanNewView(BangnaControl bc, String hn, String vn, String name, String filename, String dsg, String visitdate)
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
            //// Initialize the RasterCodecs object
            //_rasterCodecs = new RasterCodecs();
            //_imageViewer = new Leadtools.Controls.ImageViewer();
            //this._imageViewer.BackColor = System.Drawing.SystemColors.AppWorkspace;
            //this._imageViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            //this._imageViewer.Cursor = System.Windows.Forms.Cursors.Cross;
            //this._imageViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            //this._imageViewer.ViewHorizontalAlignment = Leadtools.Controls.ControlAlignment.Center;
            //this._imageViewer.Location = new System.Drawing.Point(0, 195);
            //this._imageViewer.Name = "_rasterImageViewer";
            //this._imageViewer.Size = new System.Drawing.Size(894, 333);
            //this._imageViewer.TabIndex = 3;
            //this._imageViewer.UseDpi = true;
            //this._imageViewer.ViewVerticalAlignment = Leadtools.Controls.ControlAlignment.Center;
            ////this._imageViewer.TransformChanged += new System.EventHandler(this._imageViewer_TransformChanged);
            ////this._imageViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this._imageViewer_MouseMove);
            ////this._imageViewer.MouseClick += new System.Windows.Forms.MouseEventHandler(this._imageViewer_MouseClick);
            ////this._imageViewer.PostRender += new System.EventHandler<Leadtools.Controls.ImageViewerRenderEventArgs>(this._imageViewer_PostRender);
            //pnImg.Controls.Add(_imageViewer);
            //_imageViewer.Hide();

            ////  Use the new RasterizeDocumentOptions to default loading document files at 300 DPI
            //_rasterCodecs.Options.RasterizeDocument.Load.XResolution = 300;
            //_rasterCodecs.Options.RasterizeDocument.Load.YResolution = 300;
            //_rasterCodecs.Options.Pdf.Load.EnableInterpolate = true;
            //_rasterCodecs.Options.Load.AutoFixImageResolution = true;

            theme1.Theme = bc.iniC.themeApplication;
            btnRotate.Click += BtnRotate_Click;
            bc.bcDB.dgsDB.setCboBsp(cboDgs, "");
            btnSave.Click += BtnSave_Click;
            btnAnalyze.Click += BtnAnalyze_Click;
            this.FormClosing += FrmScanNewView_FormClosing;
            //theme1.SetTheme(sb1, "BeigeOne");

            //sb1.Text = "aaaaaaaaaa";

            txtHn.Value = hn;
            txtVN.Value = vn;
            txtNameFeMale.Value = name;
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
            if (formsOCREngine != null && formsOCREngine.IsStarted)
                formsOCREngine.Shutdown();
        }

        private void Startup()
        {
            try
            {
                string MY_LICENSE_FILE = @"C:\LEADTOOLS 20\Common\License\LEADTOOLS.LIC";

                // Unlock support 
                string MY_DEVELOPER_KEY = "gMxMXs9T3paebVPDRdEyk4CRX8BNLmMIvN383qJp6jProMPYamOe136YzHr+CmFEOZzOcuiabiSFpOJGrOHJlx8jHKErnx/u";
                RasterSupport.SetLicense(MY_LICENSE_FILE, MY_DEVELOPER_KEY);
                FormRecognitionEngine recognitionEngine = new FormRecognitionEngine();
                RasterCodecs codecs;

                codecs = new RasterCodecs();
                //Create a LEADTOOLS OCR Module - LEAD Engine and start it 
                formsOCREngine = OcrEngineManager.CreateEngine(OcrEngineType.LEAD, false);
                formsOCREngine.Startup(codecs, null, null, @"C:\LEADTOOLS 20\Bin\Common\OcrLEADRuntime");
                //Add an OCRObjectManager to the recognition engines 
                //ObjectManager collection 
                OcrObjectsManager ocrObjectsManager = new OcrObjectsManager(formsOCREngine);
                ocrObjectsManager.Engine = formsOCREngine;
                recognitionEngine.ObjectsManagers.Add(ocrObjectsManager);

                //Get master form filenames 
                //You may need to update the below path to point to the "Leadtools Images\Forms\MasterForm Sets\OCR" directory. 
                string[] masterFileNames = Directory.GetFiles(@"C:\Users\Public\Documents\LEADTOOLS Images\Forms\MasterForm Sets",
                                                                   "StaffNote.tif",
                                                                   SearchOption.AllDirectories);
                foreach (string masterFileName in masterFileNames)
                {
                    string formName = Path.GetFileNameWithoutExtension(masterFileName);
                    //Load the master form image 
                    RasterImage image = codecs.Load(masterFileName, 0, CodecsLoadByteOrder.BgrOrGray, 1, -1);
                    //Create a new master form 
                    FormRecognitionAttributes masterFormAttributes = recognitionEngine.CreateMasterForm(formName, Guid.Empty, null);
                    for (int i = 0; i < image.PageCount; i++)
                    {
                        image.Page = i + 1;
                        //Add the master form page to the recognition engine 
                        recognitionEngine.AddMasterFormPage(masterFormAttributes, image, null);
                    }
                    //Close the master form and save it's attributes 
                    recognitionEngine.CloseMasterForm(masterFormAttributes);
                    //File.WriteAllBytes(formName + ".bin", masterFormAttributes.GetData());
                }
                MessageBox.Show("Master Form Processing Complete", "Complete");
                
                //For this tutorial, we will use the sample W9 filled form. 
                //You may need to update the below path to point to "\LEADTOOLS Images\Forms\Forms to be Recognized\OCR\W9_OCR_Filled.tif". 
                string formToRecognize = @"C:\Users\ekapop-pc\Desktop\bangna_hospital\20181222-00800002.tif";
                RasterImage image1 = codecs.Load(formToRecognize, 0, CodecsLoadByteOrder.BgrOrGray, 1, -1);
                //Load the image to recognize 
                FormRecognitionAttributes filledFormAttributes = recognitionEngine.CreateForm(null);
                for (int i = 0; i < image1.PageCount; i++)
                {
                    image1.Page = i + 1;
                    //Add each page of the filled form to the recognition engine 
                    recognitionEngine.AddFormPage(filledFormAttributes, image1, null);
                }
                recognitionEngine.CloseForm(filledFormAttributes);
                string resultMessage = "The form could not be recognized";
                //Compare the attributes of each master form to the attributes of the filled form 
                string[] masterFileNames1 = Directory.GetFiles(Application.StartupPath, "*.bin");
                foreach (string masterFileName in masterFileNames1)
                {
                    FormRecognitionAttributes masterFormAttributes = new FormRecognitionAttributes();
                    masterFormAttributes.SetData(File.ReadAllBytes(masterFileName));
                    FormRecognitionResult recognitionResult = recognitionEngine.CompareForm(masterFormAttributes, filledFormAttributes, null);
                    //In this example, we consider a confidence equal to or greater 
                    //than 90 to be a match 
                    if (recognitionResult.Confidence >= 90)
                    {
                        resultMessage = String.Format("This form has been recognized as a {0}", Path.GetFileNameWithoutExtension(masterFileName));
                        break;
                    }
                }
                MessageBox.Show(resultMessage, "Recognition Results");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnAnalyze_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Startup();
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
