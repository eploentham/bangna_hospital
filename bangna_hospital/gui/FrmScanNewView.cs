using bangna_hospital.control;

using bangna_hospital.object1;
using Leadtools;
using Leadtools.Codecs;
using Leadtools.Controls;
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
        private void Startup()
        {
            Properties.Settings settings = new Properties.Settings();

            // Show the OCR engine selection dialog to startup the OCR engine
            string engineType = settings.OcrEngineType;

            using (OcrEngineSelectDialog dlg = new OcrEngineSelectDialog(Messager.Caption, engineType, true))
            {
                // Use the same RasterCodecs instance in the OCR engine
                dlg.RasterCodecsInstance = _rasterCodecs;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _ocrEngine = dlg.OcrEngine;

                    if (_ocrEngine.SettingManager.IsSettingNameSupported("Recognition.SpaceIsValidCharacter"))
                        _ocrEngine.SettingManager.SetBooleanValue("Recognition.SpaceIsValidCharacter", false);

                    Text = string.Format("{0} [{1} Engine]", Messager.Caption, _ocrEngine.EngineType.ToString());

                    // Load the default document
                    string defaultDocumentFile;
                    if (_ocrEngine.EngineType == OcrEngineType.OmniPageArabic)
                        defaultDocumentFile = Path.Combine(DemosGlobal.ImagesFolder, "ArabicSample.tif");
                    else
                        defaultDocumentFile = Path.Combine(DemosGlobal.ImagesFolder, "ocr1.tif");

                    if (File.Exists(defaultDocumentFile))
                        OpenDocument(defaultDocumentFile);

                    UpdateUIState();
                }
                else
                {
                    // Close the demo
                    Close();
                }
            }
        }
        #region OCR open/recognize and save code
        private void OpenDocument(string documentFileName)
        {
            // Create a new document, add the page to it and recognize it
            // If all the above is OK, then use it

            // Setup the arguments for the callback
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("documentFileName", documentFileName);

            // Call the process dialog
            try
            {
                //bool allowProgress = _preferencesUseCallbacksToolStripMenuItem.Checked;
                bool allowProgress = true;
                using (OcrProgressDialog dlg = new OcrProgressDialog(allowProgress, "Loading and Recognizing Document", new OcrProgressDialog.ProcessDelegate(DoLoadAndRecognizeDocument), args))
                {
                    dlg.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                UpdateUIState();
            }
        }
        private void DoLoadAndRecognizeDocument(OcrProgressDialog dlg, Dictionary<string, object> args)
        {
            // Perform load and recognize here

            OcrProgressCallback callback = dlg.OcrProgressCallback;
            IOcrDocument ocrDocument = null;

            try
            {
                string documentFileName = args["documentFileName"] as string;

                ocrDocument = _ocrEngine.DocumentManager.CreateDocument("", OcrCreateDocumentOptions.InMemory);

                IOcrPage ocrPage = null;

                if (!dlg.IsCanceled)
                {
                    // If we are not using a progress bar, update the description text
                    if (callback == null)
                        dlg.UpdateDescription("Loading the document (first page only)...");

                    ocrPage = ocrDocument.Pages.AddPage(documentFileName, callback);
                }

                if (!dlg.IsCanceled)
                {
                    // If we are not using a progress bar, update the description text
                    if (callback == null)
                        dlg.UpdateDescription("Recognizing the page(s) of the document...");

                    ocrPage.Recognize(callback);
                }

                if (!dlg.IsCanceled)
                {
                    // We did not cancel, use this document
                    SetDocument(ocrDocument, documentFileName);
                    ocrDocument = null;
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                if (callback == null)
                    dlg.EndOperation();

                // Clean up
                if (ocrDocument != null)
                    ocrDocument.Dispose();
            }
        }
        private void SetDocument(IOcrDocument ocrDocument, string documentFileName)
        {
            // Delete the old document if it exists
            if (_ocrDocument != null)
                _ocrDocument.Dispose();

            _lastDocumentFile = documentFileName;
            _ocrDocument = ocrDocument;
            _ocrPage = _ocrDocument.Pages[0];

            BuildWordLists();

            _wordTextBox.Text = string.Empty;

            SetImage(_ocrPage.GetRasterImage());

            UpdateUIState();
        }
        #endregion OCR open/recognize and save code
        private void BuildWordLists()
        {
            _ocrPageCharacters = _ocrPage.GetRecognizedCharacters();
            _ocrZoneWords = new List<List<OcrWord>>();

            // Build the words
            foreach (IOcrZoneCharacters zoneCharacters in _ocrPageCharacters)
            {
                List<OcrWord> words = new List<OcrWord>();
                words.AddRange(zoneCharacters.GetWords());

                _ocrZoneWords.Add(words);
            }

            _selectedZoneIndex = -1;
            _selectedWordIndex = -1;
        }
        private void SetImage(RasterImage image)
        {
            _imageViewer.Image = image;
            _imageViewer.Show();
            pic1.Hide();

            UpdateUIState();
        }
        private void DoShowError(Exception ex)
        {
            BeginInvoke(new MethodInvoker(delegate ()
            {
                // Shows an error, check if the exception is an OCR, raster or general one
                OcrException oe = ex as OcrException;
                if (oe != null)
                    Messager.ShowError(this, string.Format("LEADTOOLS Error\n\nCode: {0}\n\n{1}", oe.Code, ex.Message));
                else
                {
                    RasterException re = ex as RasterException;
                    if (re != null)
                        Messager.ShowError(this, string.Format("OCR Error\n\nCode: {0}\n\n{1}", re.Code, ex.Message));
                    else
                        Messager.ShowError(this, ex);
                }
            }));
        }
        private delegate void ShowErrorDelegate(Exception ex);

        private void ShowError(Exception ex)
        {
            if (InvokeRequired)
                BeginInvoke(new ShowErrorDelegate(DoShowError), new object[] { ex });
            else
                DoShowError(ex);
        }
        private void UpdateUIState()
        {
            // Update the UI controls states

            //_fitPageWidthToolStripButton.Checked = _imageViewer.SizeMode == ControlSizeMode.FitWidth;
            //_fitPageToolStripButton.Checked = _imageViewer.SizeMode == ControlSizeMode.Fit;

            //bool imageOk = _imageViewer.Image != null;

            //_openToolStripButton.Enabled = true;
            //_saveToolStripButton.Enabled = imageOk;
            //_fitPageWidthToolStripButton.Enabled = imageOk;
            //_fitPageToolStripButton.Enabled = imageOk;
            //_zoomToolStripComboBox.Enabled = imageOk;
            //_zoomOutToolStripButton.Enabled = imageOk;
            //_zoomInToolStripButton.Enabled = imageOk;

            //_controlsPanel.Enabled = imageOk;

            //if (!imageOk)
            //    _zoomToolStripComboBox.Text = string.Empty;

            //_highlightWordsToolStripButton.Enabled = imageOk;

            //bool wordIsSelected = _selectedZoneIndex != -1 && _selectedWordIndex != -1;

            //_deleteButton.Enabled = imageOk && wordIsSelected;
            //_updateButton.Enabled = imageOk && wordIsSelected && _wordTextBox.Text.Trim().Length > 0;

            //_deleteWordToolStripButton.Enabled = _deleteButton.Enabled;
            //_updateWordToolStripButton.Enabled = _updateButton.Enabled;
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
