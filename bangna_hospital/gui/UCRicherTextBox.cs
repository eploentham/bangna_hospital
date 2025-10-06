using AutocompleteMenuNS;
using bangna_hospital.control;
using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public enum RicherTextBoxToolStripGroups
    {
        SaveAndLoad = 0x1,
        FontNameAndSize = 0x2,
        BoldUnderlineItalic = 0x4,
        Alignment = 0x8,
        FontColor = 0x10,
        IndentationAndBullets = 0x20,
        Insert = 0x40,
        Zoom = 0x80
    }

    public partial class UCRicherTextBox : UserControl
    {
        BangnaControl BC;
        String PRENO = "", VSDATE = "", HN = "", DTRCODE = "", StatusFormUs="", TXT="";
        AutocompleteMenu AUTOMENU;
        Patient PTT;
        Boolean isLoaded = false;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbtnSave;
        private ToolStripButton tsbtnOpen;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripComboBox tscmbFont;
        private ToolStripComboBox tscmbFontSize;
        private ToolStripButton tsbtnChooseFont;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton tsbtnBold;
        private ToolStripButton tsbtnItalic;
        private ToolStripButton tsbtnUnderline;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsbtnAlignLeft;
        private ToolStripButton tsbtnAlignCenter;
        private ToolStripButton tsbtnAlignRight;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton tsbtnFontColor;
        private ToolStripButton tsbtnWordWrap;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton tsbtnIndent;
        private ToolStripButton tsbtnOutdent;
        private ToolStripButton tsbtnBullets;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton tsbtnInsertPicture;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton tsbtnZoomIn;
        private ToolStripButton tsbtnZoomOut;
        private ToolStripTextBox tstxtZoomFactor;
        private ToolStrip toolStripFindReplace;
        private ToolStripTextBox tstxtSearchText;
        private ToolStripButton tsbtnFind;
        private ToolStripButton tsbtnReplace;
        private RichTextBox rtbDocument;
        private ContextMenuStrip contextMenu;
        private IContainer components;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem alignmentToolStripMenuItem;
        private ToolStripMenuItem leftToolStripMenuItem;
        private ToolStripMenuItem centerToolStripMenuItem;
        private ToolStripMenuItem rightToolStripMenuItem;
        private ToolStripMenuItem styleToolStripMenuItem;
        private ToolStripMenuItem boldToolStripMenuItem;
        private ToolStripMenuItem italicToolStripMenuItem;
        private ToolStripMenuItem underlineToolStripMenuItem;
        private ToolStripMenuItem indentationToolStripMenuItem;
        private ToolStripMenuItem increaseToolStripMenuItem;
        private ToolStripMenuItem decreaseToolStripMenuItem;
        private ToolStripMenuItem bulletsToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem insertPictureToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem zoomInToolStripMenuItem;
        private ToolStripMenuItem zoomOuToolStripMenuItem;
        #region Settings
        private int indent = 10;
        [Category("Settings")]
        [Description("Value indicating the number of characters used for indentation")]
        public int INDENT
        {
            get { return indent; }
            set { indent = value; }
        }
        #endregion

        #region Properties for toolstrip items visibility
        [Category("Toolstip items visibility")]
        public bool GroupSaveAndLoadVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.SaveAndLoad); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.SaveAndLoad, value); }
        }
        [Category("Toolstip items visibility")]
        public bool GroupFontNameAndSizeVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.FontNameAndSize); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.FontNameAndSize, value); }
        }
        [Category("Toolstip items visibility")]
        public bool GroupBoldUnderlineItalicVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.BoldUnderlineItalic); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.BoldUnderlineItalic, value); }
        }
        [Category("Toolstip items visibility")]
        public bool GroupAlignmentVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.Alignment); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.Alignment, value); }
        }
        [Category("Toolstip items visibility")]
        public bool GroupFontColorVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.FontColor); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.FontColor, value); }
        }
        [Category("Toolstip items visibility")]
        public bool GroupIndentationAndBulletsVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.IndentationAndBullets); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.IndentationAndBullets, value); }
        }
        [Category("Toolstip items visibility")]
        public bool GroupInsertVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.Insert); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.Insert, value); }
        }
        [Category("Toolstip items visibility")]
        public bool GroupZoomVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.Zoom); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.Zoom, value); }
        }
        [Category("Toolstip items visibility")]
        public bool ToolStripVisible
        {
            get { return toolStrip1.Visible; }
            set { toolStrip1.Visible = value; }
        }
        [Category("Toolstip items visibility")]
        public bool FindReplaceVisible
        {
            get { return toolStripFindReplace.Visible; }
            set { toolStripFindReplace.Visible = value; }
        }

        [Category("Toolstrip single items visibility")]
        public bool SaveVisible
        {
            get { return tsbtnSave.Visible; }
            set { tsbtnSave.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool LoadVisible
        {
            get { return tsbtnOpen.Visible; }
            set { tsbtnOpen.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool SeparatorSaveLoadVisible
        {
            get { return toolStripSeparator6.Visible; }
            set { toolStripSeparator6.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool FontFamilyVisible
        {
            get { return tscmbFont.Visible; }
            set { tscmbFont.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool FontSizeVisible
        {
            get { return tscmbFontSize.Visible; }
            set { tscmbFontSize.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool ChooseFontVisible
        {
            get { return tsbtnChooseFont.Visible; }
            set { tsbtnChooseFont.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool SeparatorFontVisible
        {
            get { return toolStripSeparator1.Visible; }
            set { toolStripSeparator1.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool BoldVisible
        {
            get { return tsbtnBold.Visible; }
            set { tsbtnBold.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool ItalicVisible
        {
            get { return tsbtnItalic.Visible; }
            set { tsbtnItalic.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool UnderlineVisible
        {
            get { return tsbtnUnderline.Visible; }
            set { tsbtnUnderline.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool SeparatorBoldUnderlineItalicVisible
        {
            get { return toolStripSeparator2.Visible; }
            set { toolStripSeparator2.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool AlignLeftVisible
        {
            get { return tsbtnAlignLeft.Visible; }
            set { tsbtnAlignLeft.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool AlignRightVisible
        {
            get { return tsbtnAlignRight.Visible; }
            set { tsbtnAlignRight.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool AlignCenterVisible
        {
            get { return tsbtnAlignCenter.Visible; }
            set { tsbtnAlignCenter.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool SeparatorAlignVisible
        {
            get { return toolStripSeparator3.Visible; }
            set { toolStripSeparator3.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool FontColorVisible
        {
            get { return tsbtnFontColor.Visible; }
            set { tsbtnFontColor.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool WordWrapVisible
        {
            get { return tsbtnWordWrap.Visible; }
            set { tsbtnWordWrap.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool SeparatorFontColorVisible
        {
            get { return toolStripSeparator4.Visible; }
            set { toolStripSeparator4.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool IndentVisible
        {
            get { return tsbtnIndent.Visible; }
            set { tsbtnIndent.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool OutdentVisible
        {
            get { return tsbtnOutdent.Visible; }
            set { tsbtnOutdent.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool BulletsVisible
        {
            get { return tsbtnBullets.Visible; }
            set { tsbtnBullets.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool SeparatorIndentAndBulletsVisible
        {
            get { return toolStripSeparator5.Visible; }
            set { toolStripSeparator5.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool InsertPictureVisible
        {
            get { return tsbtnInsertPicture.Visible; }
            set { tsbtnInsertPicture.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool SeparatorInsertVisible
        {
            get { return toolStripSeparator7.Visible; }
            set { toolStripSeparator7.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool ZoomInVisible
        {
            get { return tsbtnZoomIn.Visible; }
            set { tsbtnZoomIn.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool ZoomOutVisible
        {
            get { return tsbtnZoomOut.Visible; }
            set { tsbtnZoomOut.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        public bool ZoomFactorTextVisible
        {
            get { return tstxtZoomFactor.Visible; }
            set { tstxtZoomFactor.Visible = value; }
        }

        #endregion

        #region data properties
        [Category("Document data")]
        [Description("RicherTextBox content in plain text")]
        [Browsable(true)]
        public override string Text
        {
            get { return rtbDocument.Text; }
            set { rtbDocument.Text = value; }
        }
        [Category("Document data")]
        [Description("RicherTextBox content in rich-text format")]
        public string Rtf
        {
            get { return rtbDocument.Rtf; }
            set { try { rtbDocument.Rtf = value; } catch (ArgumentException) { rtbDocument.Text = value; } }
        }
        #endregion

        #region Construction and initial loading
        public UCRicherTextBox(BangnaControl bc, String dtrcode, String hn, String vsdate, String preno, Patient ptt, String statusformus, String txt)
        {
            isLoaded = false;
            BC = bc;
            DTRCODE = dtrcode;
            HN = hn;
            VSDATE = vsdate;
            PRENO = preno;
            PTT = ptt;
            StatusFormUs = statusformus;
            TXT = txt;
            //AUTOMENU = aUTOCom;
            InitializeComponent();
            this.Load += new System.EventHandler(this.RicherTextBox_Load);
            rtbDocument.Font = new Font("Microsoft Sans Serif", 12);
            tscmbFont.SelectedIndexChanged += new EventHandler(tscmbFont_Click);
            tscmbFontSize.SelectedIndexChanged += new EventHandler(tscmbFontSize_Click);
            tsbtnChooseFont.Click += new EventHandler(btnChooseFont_Click);
            tsbtnBold.Click += new EventHandler(tsbtnBIU_Click);
            tsbtnItalic.Click += new EventHandler(tsbtnBIU_Click);
            tsbtnUnderline.Click += new EventHandler(tsbtnBIU_Click);
            rtbDocument.SelectionChanged += new EventHandler(rtbDocument_SelectionChanged);
            tsbtnAlignLeft.Click += new EventHandler(tsbtnAlignment_Click);
            tsbtnAlignCenter.Click += new EventHandler(tsbtnAlignment_Click);
            tsbtnAlignRight.Click += new EventHandler(tsbtnAlignment_Click);
            tsbtnFontColor.Click += new EventHandler(tsbtnFontColor_Click);
            tsbtnBullets.Click += new EventHandler(tsbtnBulletsAndNumbering_Click);
            tsbtnIndent.Click += new EventHandler(tsbtnBulletsAndNumbering_Click);
            tsbtnOutdent.Click += new EventHandler(tsbtnBulletsAndNumbering_Click);
            tsbtnSave.Click += new EventHandler(tsbtnSave_Click);
            tsbtnInsertPicture.Visible = false;
            tsbtnZoomIn.Visible = false;
            tsbtnZoomOut.Visible = false; tsbtnZoomIn.Visible = false;
            tstxtZoomFactor.Visible = false;
            toolStripSeparator7.Visible = false; toolStripSeparator5.Visible = false;
            isLoaded = true;
            AUTOMENU = new AutocompleteMenuNS.AutocompleteMenu();
            AUTOMENU.AllowsTabKey = true;
            AUTOMENU.Font = new System.Drawing.Font(BC.iniC.grdViewFontName, 12F);
            AUTOMENU.Items = new string[0];
            AUTOMENU.SearchPattern = "[\\w\\.:=!<>]";
            AUTOMENU.TargetControlWrapper = null;
            if(statusformus.Equals("doctor_order_physical_exam"))
            {
                AUTOMENU.SetAutocompleteItems(BC.bcDB.autoCompDB.BuildAutoCompleteLine1("doctor_physical", DTRCODE));
            }
            AUTOMENU.SetAutocompleteMenu(rtbDocument, AUTOMENU);
        }

        private void RicherTextBox_Load(object sender, EventArgs e)
        {
            // load system fonts
            isLoaded = true;
            foreach (FontFamily family in FontFamily.Families)
            {
                tscmbFont.Items.Add(family.Name);
            }
            tscmbFont.SelectedItem = "Microsoft Sans Serif";

            tscmbFontSize.SelectedItem = "9";

            tstxtZoomFactor.Text = Convert.ToString(rtbDocument.ZoomFactor * 100);
            tsbtnWordWrap.Checked = rtbDocument.WordWrap;
            RichTextBoxLoad();
            isLoaded = false;
        }
        #endregion
        public void RichTextBoxLoad()
        {
            // Custom painting logic if needed
            //loadDoctorOrderPhysicalExamFTP();
            rtbDocument.Text = TXT;
        }
        private void loadDoctorOrderPhysicalExam()
        {

        }
        private void loadDoctorOrderPhysicalExamFTP()
        {
            DataTable dt = BC.bcDB.dscDB.selectByDoctorOrderPhysicalExam(HN, VSDATE, PRENO);
            if (dt.Rows.Count <= 0) return;
            FtpClient ftp = new FtpClient(BC.iniC.hostFTP, BC.iniC.userFTP, BC.iniC.passFTP, BC.ftpUsePassive);
            MemoryStream stream = ftp.download(dt.Rows[0][BC.bcDB.dscDB.dsc.folder_ftp] + "/" + dt.Rows[0][BC.bcDB.dscDB.dsc.image_path].ToString());
            stream.Position = 0;
            var fileType = FileTypeDetectorPDF.DetectFileType(stream);
            switch (fileType)
            {
                case FileTypeDetectorPDF.FileTypePDF.PDF:
                    rtbDocument.LoadFile(stream, RichTextBoxStreamType.RichText);
                    break;
                case FileTypeDetectorPDF.FileTypePDF.JPEG:
                    Console.WriteLine("ไฟล์นี้เป็น JPEG");
                    break;
            }
        }
        #region Toolstrip items handling
        private void tsbtnBIU_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDocument.SelectionFont == null))
                {
                    Font currentFont = rtbDocument.SelectionFont;
                    FontStyle newFontStyle = rtbDocument.SelectionFont.Style;
                    string txt = (sender as ToolStripButton).Name;
                    if (txt.IndexOf("Bold") >= 0)
                        newFontStyle = rtbDocument.SelectionFont.Style ^ FontStyle.Bold;
                    else if (txt.IndexOf("Italic") >= 0)
                        newFontStyle = rtbDocument.SelectionFont.Style ^ FontStyle.Italic;
                    else if (txt.IndexOf("Underline") >= 0)
                        newFontStyle = rtbDocument.SelectionFont.Style ^ FontStyle.Underline;

                    rtbDocument.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void rtbDocument_SelectionChanged(object sender, EventArgs e)
        {
            if (rtbDocument.SelectionFont != null)
            {
                tsbtnBold.Checked = rtbDocument.SelectionFont.Bold;
                tsbtnItalic.Checked = rtbDocument.SelectionFont.Italic;
                tsbtnUnderline.Checked = rtbDocument.SelectionFont.Underline;

                boldToolStripMenuItem.Checked = rtbDocument.SelectionFont.Bold;
                italicToolStripMenuItem.Checked = rtbDocument.SelectionFont.Italic;
                underlineToolStripMenuItem.Checked = rtbDocument.SelectionFont.Underline;

                switch (rtbDocument.SelectionAlignment)
                {
                    case HorizontalAlignment.Left:
                        tsbtnAlignLeft.Checked = true;
                        tsbtnAlignCenter.Checked = false;
                        tsbtnAlignRight.Checked = false;

                        leftToolStripMenuItem.Checked = true;
                        centerToolStripMenuItem.Checked = false;
                        rightToolStripMenuItem.Checked = false;
                        break;

                    case HorizontalAlignment.Center:
                        tsbtnAlignLeft.Checked = false;
                        tsbtnAlignCenter.Checked = true;
                        tsbtnAlignRight.Checked = false;

                        leftToolStripMenuItem.Checked = false;
                        centerToolStripMenuItem.Checked = true;
                        rightToolStripMenuItem.Checked = false;
                        break;

                    case HorizontalAlignment.Right:
                        tsbtnAlignLeft.Checked = false;
                        tsbtnAlignCenter.Checked = false;
                        tsbtnAlignRight.Checked = true;

                        leftToolStripMenuItem.Checked = false;
                        centerToolStripMenuItem.Checked = false;
                        rightToolStripMenuItem.Checked = true;
                        break;
                }

                tsbtnBullets.Checked = rtbDocument.SelectionBullet;
                bulletsToolStripMenuItem.Checked = rtbDocument.SelectionBullet;

                tscmbFont.SelectedItem = rtbDocument.SelectionFont.FontFamily.Name;
                tscmbFontSize.SelectedItem = rtbDocument.SelectionFont.Size.ToString();
            }
        }

        private void tsbtnAlignment_Click(object sender, EventArgs e)
        {
            try
            {
                string txt = (sender as ToolStripButton).Name;
                if (txt.IndexOf("Left") >= 0)
                {
                    rtbDocument.SelectionAlignment = HorizontalAlignment.Left;
                    tsbtnAlignLeft.Checked = true;
                    tsbtnAlignCenter.Checked = false;
                    tsbtnAlignRight.Checked = false;
                }
                else if (txt.IndexOf("Center") >= 0)
                {
                    rtbDocument.SelectionAlignment = HorizontalAlignment.Center;
                    tsbtnAlignLeft.Checked = false;
                    tsbtnAlignCenter.Checked = true;
                    tsbtnAlignRight.Checked = false;
                }
                else if (txt.IndexOf("Right") >= 0)
                {
                    rtbDocument.SelectionAlignment = HorizontalAlignment.Right;
                    tsbtnAlignLeft.Checked = false;
                    tsbtnAlignCenter.Checked = false;
                    tsbtnAlignRight.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbtnFontColor_Click(object sender, EventArgs e)
        {
            try
            {
                using (ColorDialog dlg = new ColorDialog())
                {
                    dlg.Color = rtbDocument.SelectionColor;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        rtbDocument.SelectionColor = dlg.Color;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbtnBulletsAndNumbering_Click(object sender, EventArgs e)
        {
            try
            {
                string name = (sender as ToolStripButton).Name;
                if (name.IndexOf("Bullets") >= 0)
                    rtbDocument.SelectionBullet = tsbtnBullets.Checked;
                else if (name.IndexOf("Indent") >= 0)
                    rtbDocument.SelectionIndent += INDENT;
                else if (name.IndexOf("Outdent") >= 0)
                    rtbDocument.SelectionIndent -= INDENT;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tscmbFontSize_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDocument.SelectionFont == null))
                {
                    Font currentFont = rtbDocument.SelectionFont;
                    float newSize = Convert.ToSingle(tscmbFontSize.SelectedItem.ToString());
                    rtbDocument.SelectionFont = new Font(currentFont.FontFamily, newSize, currentFont.Style);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }


        private void tscmbFontSize_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDocument.SelectionFont == null))
                {
                    Font currentFont = rtbDocument.SelectionFont;
                    float newSize = Convert.ToSingle(tscmbFontSize.Text);
                    rtbDocument.SelectionFont = new Font(currentFont.FontFamily, newSize, currentFont.Style);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tscmbFont_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDocument.SelectionFont == null))
                {
                    Font currentFont = rtbDocument.SelectionFont;
                    FontFamily newFamily = new FontFamily(tscmbFont.SelectedItem.ToString());
                    rtbDocument.SelectionFont = new Font(newFamily, currentFont.Size, currentFont.Style);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void btnChooseFont_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new FontDialog())
            {
                if (rtbDocument.SelectionFont != null) dlg.Font = rtbDocument.SelectionFont;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    rtbDocument.SelectionFont = dlg.Font;
                }
            }
        }

        private void tsbtnInsertPicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Insert picture";
                dlg.DefaultExt = "jpg";
                dlg.Filter = "Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif|All files|*.*";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string strImagePath = dlg.FileName;
                        Image img = Image.FromFile(strImagePath);
                        Clipboard.SetDataObject(img);
                        DataFormats.Format df;
                        df = DataFormats.GetFormat(DataFormats.Bitmap);
                        if (this.rtbDocument.CanPaste(df))
                        {
                            this.rtbDocument.Paste(df);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Unable to insert image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            if(StatusFormUs.Equals("VIEW"))
            {
                return;
            }
            else if (StatusFormUs.Equals("doctor_order_physical_exam"))
            {
                //saveDoctorOrderPhysicalExamFTP();
                saveDoctorOrderPhysicalExam();
            }
            else
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "Rich text format|*.rtf";
                    dlg.FilterIndex = 0;
                    dlg.OverwritePrompt = true;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            rtbDocument.SaveFile(dlg.FileName, RichTextBoxStreamType.RichText);
                        }
                        catch (IOException exc)
                        {
                            MessageBox.Show("Error writing file: \n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (ArgumentException exc_a)
                        {
                            MessageBox.Show("Error writing file: \n" + exc_a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void saveDoctorOrderPhysicalExam()
        {
            String re = "";
            re = BC.bcDB.vsDB.updatePhysicalExam(HN, VSDATE, PRENO, rtbDocument.Rtf.Trim(), DTRCODE);
            if (re.Equals("1"))
            {
                //lfSbMessage.Text = "Save Physical Exam Complete";
            }
            else
            {
                //lfSbMessage.Text = "Save Physical Exam not complete";
            }
        }
        private void saveDoctorOrderPhysicalExamFTP()
        {
            try
            {
                FtpClient ftp = new FtpClient(BC.iniC.hostFTP, BC.iniC.userFTP, BC.iniC.passFTP, BC.ftpUsePassive);
                DocScan dsc = new DocScan();
                dsc.active = "1";
                dsc.doc_scan_id = "";
                dsc.doc_group_id = "1100000001";
                dsc.hn = HN;
                dsc.an = "";
                dsc.visit_date = VSDATE;
                dsc.host_ftp = BC.iniC.hostFTP;
                dsc.image_path = "";
                dsc.doc_group_sub_id = "1200000003";
                dsc.pre_no = PRENO;
                dsc.folder_ftp = BC.iniC.folderFTP;
                dsc.row_no = "1";
                dsc.row_cnt = "1";
                dsc.status_version = "2";
                dsc.req_id = "";
                dsc.date_req = "";
                dsc.status_ipd = "O";
                dsc.ml_fm = "FM-MED-902";
                dsc.remark = "RTF";
                dsc.sort1 = "1";

                BC.bcDB.dscDB.voidDocScanByStatusDoctorOrder(HN, VSDATE, PRENO, DTRCODE);
                dsc.patient_fullname = PTT.Name;
                dsc.status_record = "5";
                dsc.comp_labout_id = "";
                String re = BC.bcDB.dscDB.insertMedicalExamination(dsc, BC.userId);
                dsc.image_path = BC.hn + "//1200000003_" + BC.hn + "_" + BC.vsdate + "_" + BC.preno + "_" + re + ".RTF";
                String re1 = BC.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                ftp.createDirectory(BC.iniC.folderFTP + "//" + BC.hn.Replace("/", "-"));
                ftp.delete(BC.iniC.folderFTP + "//" + dsc.image_path);

                // Convert drawingSurface to MemoryStream
                using (var ms = new System.IO.MemoryStream())
                {
                    rtbDocument.SaveFile(ms, RichTextBoxStreamType.RichText);
                    ms.Position = 0;
                    if (ftp.upload(BC.iniC.folderFTP + "//" + dsc.image_path, ms))
                    {
                        // Optionally handle success
                    }
                }
            }
            catch (IOException exc)
            {
                MessageBox.Show("Error writing file: \n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException exc_a)
            {
                MessageBox.Show("Error writing file: \n" + exc_a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsbtnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Rich text format|*.rtf";
                dlg.FilterIndex = 0;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        rtbDocument.LoadFile(dlg.FileName, RichTextBoxStreamType.RichText);
                    }
                    catch (IOException exc)
                    {
                        MessageBox.Show("Error reading file: \n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (ArgumentException exc_a)
                    {
                        MessageBox.Show("Error reading file: \n" + exc_a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (rtbDocument.ZoomFactor < 64.0f - 0.20f)
            {
                rtbDocument.ZoomFactor += 0.20f;
                tstxtZoomFactor.Text = String.Format("{0:F0}", rtbDocument.ZoomFactor * 100);
            }
        }

        private void tsbtnZoomOut_Click(object sender, EventArgs e)
        {
            if (rtbDocument.ZoomFactor > 0.16f + 0.20f)
            {
                rtbDocument.ZoomFactor -= 0.20f;
                tstxtZoomFactor.Text = String.Format("{0:F0}", rtbDocument.ZoomFactor * 100);
            }
        }

        private void tstxtZoomFactor_Leave(object sender, EventArgs e)
        {
            try
            {
                rtbDocument.ZoomFactor = Convert.ToSingle(tstxtZoomFactor.Text) / 100;
            }
            catch (FormatException)
            {
                MessageBox.Show("Enter valid number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tstxtZoomFactor.Focus();
                tstxtZoomFactor.SelectAll();
            }
            catch (OverflowException)
            {
                MessageBox.Show("Enter valid number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tstxtZoomFactor.Focus();
                tstxtZoomFactor.SelectAll();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Zoom factor should be between 20% and 6400%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tstxtZoomFactor.Focus();
                tstxtZoomFactor.SelectAll();
            }
        }


        private void tsbtnWordWrap_Click(object sender, EventArgs e)
        {
            rtbDocument.WordWrap = tsbtnWordWrap.Checked;
        }

        #endregion

        #region Changing visibility of toolstrip items

        public void HideToolstripItemsByGroup(RicherTextBoxToolStripGroups group, bool visible)
        {
            if ((group & RicherTextBoxToolStripGroups.SaveAndLoad) != 0)
            {
                tsbtnSave.Visible = visible;
                tsbtnOpen.Visible = visible;
                toolStripSeparator6.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.FontNameAndSize) != 0)
            {
                tscmbFont.Visible = visible;
                tscmbFontSize.Visible = visible;
                tsbtnChooseFont.Visible = visible;
                toolStripSeparator1.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.BoldUnderlineItalic) != 0)
            {
                tsbtnBold.Visible = visible;
                tsbtnItalic.Visible = visible;
                tsbtnUnderline.Visible = visible;
                toolStripSeparator2.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.Alignment) != 0)
            {
                tsbtnAlignLeft.Visible = visible;
                tsbtnAlignRight.Visible = visible;
                tsbtnAlignCenter.Visible = visible;
                toolStripSeparator3.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.FontColor) != 0)
            {
                tsbtnFontColor.Visible = visible;
                tsbtnWordWrap.Visible = visible;
                toolStripSeparator4.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.IndentationAndBullets) != 0)
            {
                tsbtnIndent.Visible = visible;
                tsbtnOutdent.Visible = visible;
                tsbtnBullets.Visible = visible;
                toolStripSeparator5.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.Insert) != 0)
            {
                tsbtnInsertPicture.Visible = visible;
                toolStripSeparator7.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.Zoom) != 0)
            {
                tsbtnZoomOut.Visible = visible;
                tsbtnZoomIn.Visible = visible;
                tstxtZoomFactor.Visible = visible;
            }
        }

        public bool IsGroupVisible(RicherTextBoxToolStripGroups group)
        {
            switch (group)
            {
                case RicherTextBoxToolStripGroups.SaveAndLoad:
                    return tsbtnSave.Visible && tsbtnOpen.Visible && toolStripSeparator6.Visible;

                case RicherTextBoxToolStripGroups.FontNameAndSize:
                    return tscmbFont.Visible && tscmbFontSize.Visible && tsbtnChooseFont.Visible && toolStripSeparator1.Visible;

                case RicherTextBoxToolStripGroups.BoldUnderlineItalic:
                    return tsbtnBold.Visible && tsbtnItalic.Visible && tsbtnUnderline.Visible && toolStripSeparator2.Visible;

                case RicherTextBoxToolStripGroups.Alignment:
                    return tsbtnAlignLeft.Visible && tsbtnAlignRight.Visible && tsbtnAlignCenter.Visible && toolStripSeparator3.Visible;

                case RicherTextBoxToolStripGroups.FontColor:
                    return tsbtnFontColor.Visible && tsbtnWordWrap.Visible && toolStripSeparator4.Visible;

                case RicherTextBoxToolStripGroups.IndentationAndBullets:
                    return tsbtnIndent.Visible && tsbtnOutdent.Visible && tsbtnBullets.Visible && toolStripSeparator5.Visible;

                case RicherTextBoxToolStripGroups.Insert:
                    return tsbtnInsertPicture.Visible && toolStripSeparator7.Visible;

                case RicherTextBoxToolStripGroups.Zoom:
                    return tsbtnZoomOut.Visible && tsbtnZoomIn.Visible && tstxtZoomFactor.Visible;

                default:
                    return false;
            }
        }
        #endregion

        #region Public methods for accessing the functionality of the RicherTextBox

        public void SetFontFamily(FontFamily family)
        {
            if (family != null)
            {
                tscmbFont.SelectedItem = family.Name;
            }
        }

        public void SetFontSize(float newSize)
        {
            tscmbFontSize.Text = newSize.ToString();
        }

        public void ToggleBold()
        {
            tsbtnBold.PerformClick();
        }

        public void ToggleItalic()
        {
            tsbtnItalic.PerformClick();
        }

        public void ToggleUnderline()
        {
            tsbtnUnderline.PerformClick();
        }

        public void SetAlign(HorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case HorizontalAlignment.Center:
                    tsbtnAlignCenter.PerformClick();
                    break;

                case HorizontalAlignment.Left:
                    tsbtnAlignLeft.PerformClick();
                    break;

                case HorizontalAlignment.Right:
                    tsbtnAlignRight.PerformClick();
                    break;
            }
        }

        public void Indent()
        {
            tsbtnIndent.PerformClick();
        }

        public void Outdent()
        {
            tsbtnOutdent.PerformClick();
        }

        public void ToggleBullets()
        {
            tsbtnBullets.PerformClick();
        }

        public void ZoomIn()
        {
            tsbtnZoomIn.PerformClick();
        }

        public void ZoomOut()
        {
            tsbtnZoomOut.PerformClick();
        }

        public void ZoomTo(float factor)
        {
            rtbDocument.ZoomFactor = factor;
        }

        public void SetWordWrap(bool activated)
        {
            rtbDocument.WordWrap = activated;
        }
        #endregion


        #region Context menu handlers
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.Clear();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.SelectAll();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.Redo();
        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnBold.PerformClick();
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnItalic.PerformClick();
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnUnderline.PerformClick();
        }

        private void increaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnIndent.PerformClick();
        }

        private void decreaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnOutdent.PerformClick();
        }

        private void bulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnBullets.PerformClick();
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnZoomIn.PerformClick();
        }

        private void zoomOuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnZoomOut.PerformClick();
        }

        private void insertPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnInsertPicture.PerformClick();
        }

        #endregion

        #region Find and Replace
        private void tsbtnFind_Click(object sender, EventArgs e)
        {

        }

        private void tsbtnReplace_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCRicherTextBox));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tscmbFont = new System.Windows.Forms.ToolStripComboBox();
            this.tscmbFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnChooseFont = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnBold = new System.Windows.Forms.ToolStripButton();
            this.tsbtnItalic = new System.Windows.Forms.ToolStripButton();
            this.tsbtnUnderline = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnAlignLeft = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAlignCenter = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAlignRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnFontColor = new System.Windows.Forms.ToolStripButton();
            this.tsbtnWordWrap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnIndent = new System.Windows.Forms.ToolStripButton();
            this.tsbtnOutdent = new System.Windows.Forms.ToolStripButton();
            this.tsbtnBullets = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnInsertPicture = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsbtnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tstxtZoomFactor = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripFindReplace = new System.Windows.Forms.ToolStrip();
            this.tstxtSearchText = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtnFind = new System.Windows.Forms.ToolStripButton();
            this.tsbtnReplace = new System.Windows.Forms.ToolStripButton();
            this.rtbDocument = new System.Windows.Forms.RichTextBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.alignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.styleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.italicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.underlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.increaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bulletsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.insertPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.toolStripFindReplace.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSave,
            this.tsbtnOpen,
            this.toolStripSeparator6,
            this.tscmbFont,
            this.tscmbFontSize,
            this.tsbtnChooseFont,
            this.toolStripSeparator1,
            this.tsbtnBold,
            this.tsbtnItalic,
            this.tsbtnUnderline,
            this.toolStripSeparator2,
            this.tsbtnAlignLeft,
            this.tsbtnAlignCenter,
            this.tsbtnAlignRight,
            this.toolStripSeparator3,
            this.tsbtnFontColor,
            this.tsbtnWordWrap,
            this.toolStripSeparator4,
            this.tsbtnIndent,
            this.tsbtnOutdent,
            this.tsbtnBullets,
            this.toolStripSeparator5,
            this.tsbtnInsertPicture,
            this.toolStripSeparator7,
            this.tsbtnZoomIn,
            this.tsbtnZoomOut,
            this.tstxtZoomFactor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(695, 26);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSave.Image")));
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(23, 23);
            this.tsbtnSave.Text = "toolStripButton1";
            this.tsbtnSave.ToolTipText = "Save Document";
            // 
            // tsbtnOpen
            // 
            this.tsbtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOpen.Image")));
            this.tsbtnOpen.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnOpen.Name = "tsbtnOpen";
            this.tsbtnOpen.Size = new System.Drawing.Size(23, 23);
            this.tsbtnOpen.Text = "toolStripButton2";
            this.tsbtnOpen.ToolTipText = "Load Document";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 26);
            // 
            // tscmbFont
            // 
            this.tscmbFont.Name = "tscmbFont";
            this.tscmbFont.Size = new System.Drawing.Size(141, 26);
            // 
            // tscmbFontSize
            // 
            this.tscmbFontSize.AutoSize = false;
            this.tscmbFontSize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72"});
            this.tscmbFontSize.Name = "tscmbFontSize";
            this.tscmbFontSize.Size = new System.Drawing.Size(40, 23);
            // 
            // tsbtnChooseFont
            // 
            this.tsbtnChooseFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnChooseFont.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnChooseFont.Image")));
            this.tsbtnChooseFont.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnChooseFont.Name = "tsbtnChooseFont";
            this.tsbtnChooseFont.Size = new System.Drawing.Size(23, 23);
            this.tsbtnChooseFont.Text = "toolStripButton1";
            this.tsbtnChooseFont.ToolTipText = "Select Font";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbtnBold
            // 
            this.tsbtnBold.CheckOnClick = true;
            this.tsbtnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnBold.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tsbtnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnBold.Name = "tsbtnBold";
            this.tsbtnBold.Size = new System.Drawing.Size(24, 23);
            this.tsbtnBold.Text = "B";
            this.tsbtnBold.ToolTipText = "Toggle Bold";
            // 
            // tsbtnItalic
            // 
            this.tsbtnItalic.CheckOnClick = true;
            this.tsbtnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnItalic.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.tsbtnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnItalic.Name = "tsbtnItalic";
            this.tsbtnItalic.Size = new System.Drawing.Size(23, 23);
            this.tsbtnItalic.Text = "I";
            this.tsbtnItalic.ToolTipText = "Toggle Italic";
            // 
            // tsbtnUnderline
            // 
            this.tsbtnUnderline.CheckOnClick = true;
            this.tsbtnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnUnderline.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Underline);
            this.tsbtnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUnderline.Name = "tsbtnUnderline";
            this.tsbtnUnderline.Size = new System.Drawing.Size(24, 23);
            this.tsbtnUnderline.Text = "U";
            this.tsbtnUnderline.ToolTipText = "Toggle Underline";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbtnAlignLeft
            // 
            this.tsbtnAlignLeft.CheckOnClick = true;
            this.tsbtnAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAlignLeft.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAlignLeft.Image")));
            this.tsbtnAlignLeft.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnAlignLeft.Name = "tsbtnAlignLeft";
            this.tsbtnAlignLeft.Size = new System.Drawing.Size(23, 23);
            this.tsbtnAlignLeft.Text = "toolStripButton1";
            this.tsbtnAlignLeft.ToolTipText = "Align Left";
            // 
            // tsbtnAlignCenter
            // 
            this.tsbtnAlignCenter.CheckOnClick = true;
            this.tsbtnAlignCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAlignCenter.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAlignCenter.Image")));
            this.tsbtnAlignCenter.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnAlignCenter.Name = "tsbtnAlignCenter";
            this.tsbtnAlignCenter.Size = new System.Drawing.Size(23, 23);
            this.tsbtnAlignCenter.Text = "toolStripButton2";
            this.tsbtnAlignCenter.ToolTipText = "Align Center";
            // 
            // tsbtnAlignRight
            // 
            this.tsbtnAlignRight.CheckOnClick = true;
            this.tsbtnAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAlignRight.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAlignRight.Image")));
            this.tsbtnAlignRight.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnAlignRight.Name = "tsbtnAlignRight";
            this.tsbtnAlignRight.Size = new System.Drawing.Size(23, 23);
            this.tsbtnAlignRight.Text = "toolStripButton3";
            this.tsbtnAlignRight.ToolTipText = "Align Right";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbtnFontColor
            // 
            this.tsbtnFontColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnFontColor.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFontColor.Image")));
            this.tsbtnFontColor.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnFontColor.Name = "tsbtnFontColor";
            this.tsbtnFontColor.Size = new System.Drawing.Size(23, 23);
            this.tsbtnFontColor.Text = "toolStripButton4";
            this.tsbtnFontColor.ToolTipText = "Pick Font Color";
            // 
            // tsbtnWordWrap
            // 
            this.tsbtnWordWrap.CheckOnClick = true;
            this.tsbtnWordWrap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnWordWrap.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnWordWrap.Image")));
            this.tsbtnWordWrap.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnWordWrap.Name = "tsbtnWordWrap";
            this.tsbtnWordWrap.Size = new System.Drawing.Size(23, 23);
            this.tsbtnWordWrap.Text = "toolStripButton1";
            this.tsbtnWordWrap.ToolTipText = "Word Wrap";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbtnIndent
            // 
            this.tsbtnIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnIndent.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnIndent.Image")));
            this.tsbtnIndent.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnIndent.Name = "tsbtnIndent";
            this.tsbtnIndent.Size = new System.Drawing.Size(23, 23);
            this.tsbtnIndent.Text = "toolStripButton1";
            this.tsbtnIndent.ToolTipText = "Indent";
            // 
            // tsbtnOutdent
            // 
            this.tsbtnOutdent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOutdent.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOutdent.Image")));
            this.tsbtnOutdent.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnOutdent.Name = "tsbtnOutdent";
            this.tsbtnOutdent.Size = new System.Drawing.Size(23, 23);
            this.tsbtnOutdent.Text = "toolStripButton3";
            this.tsbtnOutdent.ToolTipText = "Outdent";
            // 
            // tsbtnBullets
            // 
            this.tsbtnBullets.CheckOnClick = true;
            this.tsbtnBullets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnBullets.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnBullets.Image")));
            this.tsbtnBullets.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnBullets.Name = "tsbtnBullets";
            this.tsbtnBullets.Size = new System.Drawing.Size(23, 23);
            this.tsbtnBullets.Text = "toolStripButton2";
            this.tsbtnBullets.ToolTipText = "Toggle Bullets";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbtnInsertPicture
            // 
            this.tsbtnInsertPicture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnInsertPicture.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnInsertPicture.Image")));
            this.tsbtnInsertPicture.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnInsertPicture.Name = "tsbtnInsertPicture";
            this.tsbtnInsertPicture.Size = new System.Drawing.Size(23, 23);
            this.tsbtnInsertPicture.Text = "toolStripButton1";
            this.tsbtnInsertPicture.ToolTipText = "Insert Picture";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbtnZoomIn
            // 
            this.tsbtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnZoomIn.Image")));
            this.tsbtnZoomIn.ImageTransparentColor = System.Drawing.Color.White;
            this.tsbtnZoomIn.Name = "tsbtnZoomIn";
            this.tsbtnZoomIn.Size = new System.Drawing.Size(23, 23);
            this.tsbtnZoomIn.Text = "toolStripButton1";
            this.tsbtnZoomIn.ToolTipText = "Zoom In";
            // 
            // tsbtnZoomOut
            // 
            this.tsbtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnZoomOut.Image")));
            this.tsbtnZoomOut.ImageTransparentColor = System.Drawing.Color.White;
            this.tsbtnZoomOut.Name = "tsbtnZoomOut";
            this.tsbtnZoomOut.Size = new System.Drawing.Size(23, 23);
            this.tsbtnZoomOut.Text = "toolStripButton2";
            this.tsbtnZoomOut.ToolTipText = "Zoom Out";
            // 
            // tstxtZoomFactor
            // 
            this.tstxtZoomFactor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstxtZoomFactor.Name = "tstxtZoomFactor";
            this.tstxtZoomFactor.Size = new System.Drawing.Size(30, 26);
            // 
            // toolStripFindReplace
            // 
            this.toolStripFindReplace.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripFindReplace.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstxtSearchText,
            this.tsbtnFind,
            this.tsbtnReplace});
            this.toolStripFindReplace.Location = new System.Drawing.Point(0, 26);
            this.toolStripFindReplace.Name = "toolStripFindReplace";
            this.toolStripFindReplace.Size = new System.Drawing.Size(695, 25);
            this.toolStripFindReplace.TabIndex = 4;
            this.toolStripFindReplace.Text = "toolStrip2";
            // 
            // tstxtSearchText
            // 
            this.tstxtSearchText.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstxtSearchText.Name = "tstxtSearchText";
            this.tstxtSearchText.Size = new System.Drawing.Size(100, 25);
            // 
            // tsbtnFind
            // 
            this.tsbtnFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnFind.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnFind.Name = "tsbtnFind";
            this.tsbtnFind.Size = new System.Drawing.Size(23, 22);
            this.tsbtnFind.Text = "toolStripButton1";
            this.tsbtnFind.ToolTipText = "Find";
            // 
            // tsbtnReplace
            // 
            this.tsbtnReplace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnReplace.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnReplace.Name = "tsbtnReplace";
            this.tsbtnReplace.Size = new System.Drawing.Size(23, 22);
            this.tsbtnReplace.Text = "toolStripButton2";
            this.tsbtnReplace.ToolTipText = "Replace";
            // 
            // rtbDocument
            // 
            this.rtbDocument.AcceptsTab = true;
            this.rtbDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDocument.EnableAutoDragDrop = true;
            this.rtbDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbDocument.Location = new System.Drawing.Point(0, 51);
            this.rtbDocument.Name = "rtbDocument";
            this.rtbDocument.Size = new System.Drawing.Size(695, 352);
            this.rtbDocument.TabIndex = 5;
            this.rtbDocument.Text = "";
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.toolStripMenuItem1,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripMenuItem2,
            this.alignmentToolStripMenuItem,
            this.styleToolStripMenuItem,
            this.indentationToolStripMenuItem,
            this.toolStripMenuItem3,
            this.insertPictureToolStripMenuItem,
            this.toolStripMenuItem4,
            this.zoomInToolStripMenuItem,
            this.zoomOuToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(144, 314);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "Clear";
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("selectAllToolStripMenuItem.Image")));
            this.selectAllToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
            this.undoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
            this.redoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // alignmentToolStripMenuItem
            // 
            this.alignmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leftToolStripMenuItem,
            this.centerToolStripMenuItem,
            this.rightToolStripMenuItem});
            this.alignmentToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.alignmentToolStripMenuItem.Name = "alignmentToolStripMenuItem";
            this.alignmentToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.alignmentToolStripMenuItem.Text = "Alignment";
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.CheckOnClick = true;
            this.leftToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.leftToolStripMenuItem.Text = "Left";
            // 
            // centerToolStripMenuItem
            // 
            this.centerToolStripMenuItem.CheckOnClick = true;
            this.centerToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.centerToolStripMenuItem.Name = "centerToolStripMenuItem";
            this.centerToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.centerToolStripMenuItem.Text = "Center";
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.CheckOnClick = true;
            this.rightToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.rightToolStripMenuItem.Text = "Right";
            // 
            // styleToolStripMenuItem
            // 
            this.styleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.boldToolStripMenuItem,
            this.italicToolStripMenuItem,
            this.underlineToolStripMenuItem});
            this.styleToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.styleToolStripMenuItem.Name = "styleToolStripMenuItem";
            this.styleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.styleToolStripMenuItem.Text = "Style";
            // 
            // boldToolStripMenuItem
            // 
            this.boldToolStripMenuItem.CheckOnClick = true;
            this.boldToolStripMenuItem.Name = "boldToolStripMenuItem";
            this.boldToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.boldToolStripMenuItem.Text = "Bold";
            // 
            // italicToolStripMenuItem
            // 
            this.italicToolStripMenuItem.CheckOnClick = true;
            this.italicToolStripMenuItem.Name = "italicToolStripMenuItem";
            this.italicToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.italicToolStripMenuItem.Text = "Italic";
            // 
            // underlineToolStripMenuItem
            // 
            this.underlineToolStripMenuItem.CheckOnClick = true;
            this.underlineToolStripMenuItem.Name = "underlineToolStripMenuItem";
            this.underlineToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.underlineToolStripMenuItem.Text = "Underline";
            // 
            // indentationToolStripMenuItem
            // 
            this.indentationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.increaseToolStripMenuItem,
            this.decreaseToolStripMenuItem,
            this.bulletsToolStripMenuItem});
            this.indentationToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.indentationToolStripMenuItem.Name = "indentationToolStripMenuItem";
            this.indentationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.indentationToolStripMenuItem.Text = "Indentation";
            // 
            // increaseToolStripMenuItem
            // 
            this.increaseToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.increaseToolStripMenuItem.Name = "increaseToolStripMenuItem";
            this.increaseToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.increaseToolStripMenuItem.Text = "Increase";
            // 
            // decreaseToolStripMenuItem
            // 
            this.decreaseToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.decreaseToolStripMenuItem.Name = "decreaseToolStripMenuItem";
            this.decreaseToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.decreaseToolStripMenuItem.Text = "Decrease";
            // 
            // bulletsToolStripMenuItem
            // 
            this.bulletsToolStripMenuItem.CheckOnClick = true;
            this.bulletsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.bulletsToolStripMenuItem.Name = "bulletsToolStripMenuItem";
            this.bulletsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.bulletsToolStripMenuItem.Text = "Bullets";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 6);
            // 
            // insertPictureToolStripMenuItem
            // 
            this.insertPictureToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.insertPictureToolStripMenuItem.Name = "insertPictureToolStripMenuItem";
            this.insertPictureToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.insertPictureToolStripMenuItem.Text = "Insert Picture";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(177, 6);
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.zoomInToolStripMenuItem.Text = "Zoom In";
            // 
            // zoomOuToolStripMenuItem
            // 
            this.zoomOuToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.zoomOuToolStripMenuItem.Name = "zoomOuToolStripMenuItem";
            this.zoomOuToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.zoomOuToolStripMenuItem.Text = "Zoom Out";
            // 
            // RicherTextBox
            // 
            this.Controls.Add(this.rtbDocument);
            //this.Controls.Add(this.toolStripFindReplace);
            this.Controls.Add(this.toolStrip1);
            this.Name = "RicherTextBox";
            this.Size = new System.Drawing.Size(695, 403);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripFindReplace.ResumeLayout(false);
            this.toolStripFindReplace.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
