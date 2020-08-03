using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.Util.DX.Direct2D;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmDoctorDiag1:Form
    {
        BangnaControl bc;
        String title = "", filename = "", hn = "", vsdate = "", preno = "", status = "";
        Font fEdit, fEditB, fEditBig;
        Patient ptt;
        private int indent = 10;

        C1SuperTooltip stt;
        C1SuperErrorProvider sep;

        ToolStrip toolStrip1;
        ToolStripButton tsbtnSave;
        ToolStripSeparator toolStripSeparator1;
        ToolStripComboBox tscmbFont;
        ToolStripComboBox tscmbFontSize;
        ToolStripButton tsbtnChooseFont;
        ToolStripSeparator toolStripSeparator2;
        ToolStripButton tsbtnBold;
        ToolStripButton tsbtnItalic;
        ToolStripButton tsbtnUnderline;
        ToolStripSeparator toolStripSeparator3;
        ToolStripButton tsbtnAlignLeft;
        ToolStripButton tsbtnAlignCenter;
        ToolStripButton tsbtnAlignRight;
        ToolStripSeparator toolStripSeparator4;
        ToolStripButton tsbtnFontColor;
        ToolStripButton tsbtnIndent;
        ToolStripButton tsbtnOutdent;
        ToolStripButton tsbtnBullets;
        ToolStripButton tsbtnInsertPicture;
        ToolStripSeparator toolStripSeparator5;
        ToolStripButton tsbtnUndo;
        ToolStripButton tsbtnRedo;
        ToolStripLabel tslbTitle;
        private C1ThemeController theme1;
        RichTextBox rtbDocument;
        public int INDENT
        {
            get { return indent; }
            set { indent = value; }
        }
        public FrmDoctorDiag1(BangnaControl bc, String title, String hn)
        {
            this.bc = bc;
            this.title = title;
            this.hn = hn;

            status = title;

            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            ptt = new Patient();
            ptt = bc.bcDB.pttDB.selectPatinet(hn);

            InitComponent();
            this.Load += FrmDoctorDiag1_Load;
        }
        private void InitComponent()
        {
            toolStrip1 = new ToolStrip();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripSeparator4 = new ToolStripSeparator();
            toolStripSeparator5 = new ToolStripSeparator();
            rtbDocument = new System.Windows.Forms.RichTextBox();

            tsbtnSave = new System.Windows.Forms.ToolStripButton();
            tscmbFont = new System.Windows.Forms.ToolStripComboBox();
            tscmbFontSize = new System.Windows.Forms.ToolStripComboBox();
            tsbtnChooseFont = new System.Windows.Forms.ToolStripButton();
            tsbtnBold = new System.Windows.Forms.ToolStripButton();
            tsbtnItalic = new System.Windows.Forms.ToolStripButton();
            tsbtnUnderline = new System.Windows.Forms.ToolStripButton();
            tsbtnAlignLeft = new System.Windows.Forms.ToolStripButton();
            tsbtnAlignCenter = new System.Windows.Forms.ToolStripButton();
            tsbtnAlignRight = new System.Windows.Forms.ToolStripButton();
            tsbtnFontColor = new System.Windows.Forms.ToolStripButton();
            tsbtnIndent = new System.Windows.Forms.ToolStripButton();
            tsbtnOutdent = new System.Windows.Forms.ToolStripButton();
            tsbtnBullets = new System.Windows.Forms.ToolStripButton();
            tsbtnInsertPicture = new System.Windows.Forms.ToolStripButton();
            tsbtnUndo = new System.Windows.Forms.ToolStripButton();
            tsbtnRedo = new System.Windows.Forms.ToolStripButton();
            tslbTitle = new System.Windows.Forms.ToolStripLabel();

            this.rtbDocument.AcceptsTab = true;
            //this.rtbDocument.ContextMenuStrip = this.contextMenu;
            this.rtbDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDocument.EnableAutoDragDrop = true;
            this.rtbDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbDocument.Location = new System.Drawing.Point(0, 51);
            this.rtbDocument.Name = "rtbDocument";
            this.rtbDocument.Size = new System.Drawing.Size(667, 262);
            this.rtbDocument.TabIndex = 0;
            this.rtbDocument.Text = "";
            this.rtbDocument.SelectionChanged += RtbDocument_SelectionChanged;

            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSave,            
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
            
            this.toolStripSeparator4,
            this.tsbtnIndent,
            this.tsbtnOutdent,
            this.tsbtnBullets,
            this.toolStripSeparator5,
            this.tsbtnInsertPicture,
            tslbTitle
            });
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(667, 26);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 26);

            this.tscmbFont.Name = "tscmbFont";
            this.tscmbFont.Size = new System.Drawing.Size(121, 26);
            this.tscmbFont.SelectedIndexChanged += TscmbFont_SelectedIndexChanged;

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

            tslbTitle.Name = "tslbTitle";
            if (title.Equals("cc"))
            {
                tslbTitle.Text = "Cheif Compliant";
            }
            else if (title.Equals("me"))
            {
                tslbTitle.Text = "Medical Examination";
            }
            else if (title.Equals("diag"))
            {
                tslbTitle.Text = "Diagnose";
            }
            

            this.tscmbFontSize.Name = "tscmbFontSize";
            this.tscmbFontSize.Size = new System.Drawing.Size(40, 21);
            this.tscmbFontSize.SelectedIndexChanged += TscmbFontSize_SelectedIndexChanged;
            this.tscmbFontSize.TextChanged += TscmbFontSize_TextChanged;

            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = Resources.Save_small;
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(23, 23);
            this.tsbtnSave.Text = "toolStripButton1";
            this.tsbtnSave.ToolTipText = "Save Document";
            this.tsbtnSave.Click += TsbtnSave_Click;

            this.tsbtnBold.CheckOnClick = true;
            this.tsbtnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnBold.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tsbtnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnBold.Name = "tsbtnBold";
            this.tsbtnBold.Size = new System.Drawing.Size(24, 23);
            this.tsbtnBold.Text = "B";
            this.tsbtnBold.ToolTipText = "Toggle Bold";
            this.tsbtnBold.Click += TsbtnBIU_Click;
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
            this.tsbtnItalic.Click += TsbtnBIU_Click;
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
            this.tsbtnUnderline.Click += TsbtnBIU_Click;

            this.tsbtnChooseFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnChooseFont.Image = Resources.GrowFont_small;
            this.tsbtnChooseFont.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnChooseFont.Name = "tsbtnChooseFont";
            this.tsbtnChooseFont.Size = new System.Drawing.Size(23, 23);
            this.tsbtnChooseFont.Text = "toolStripButton1";
            this.tsbtnChooseFont.ToolTipText = "Select Font";
            this.tsbtnChooseFont.Click += TsbtnChooseFont_Click;

            this.tsbtnAlignLeft.CheckOnClick = true;
            this.tsbtnAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAlignLeft.Image = Resources.AlignTextLeft_small;
            this.tsbtnAlignLeft.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnAlignLeft.Name = "tsbtnAlignLeft";
            this.tsbtnAlignLeft.Size = new System.Drawing.Size(23, 23);
            this.tsbtnAlignLeft.Text = "toolStripButton1";
            this.tsbtnAlignLeft.ToolTipText = "Align Left";
            this.tsbtnAlignLeft.Click += TsbtnAlignment_Click;
            // 
            // tsbtnAlignCenter
            // 
            this.tsbtnAlignCenter.CheckOnClick = true;
            this.tsbtnAlignCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAlignCenter.Image = Resources.AlignTextCenter_small;
            this.tsbtnAlignCenter.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnAlignCenter.Name = "tsbtnAlignCenter";
            this.tsbtnAlignCenter.Size = new System.Drawing.Size(23, 23);
            this.tsbtnAlignCenter.Text = "toolStripButton2";
            this.tsbtnAlignCenter.ToolTipText = "Align Center";
            this.tsbtnAlignCenter.Click += TsbtnAlignment_Click;
            // 
            // tsbtnAlignRight
            // 
            this.tsbtnAlignRight.CheckOnClick = true;
            this.tsbtnAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAlignRight.Image = Resources.AlignTextRight_small;
            this.tsbtnAlignRight.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnAlignRight.Name = "tsbtnAlignRight";
            this.tsbtnAlignRight.Size = new System.Drawing.Size(23, 23);
            this.tsbtnAlignRight.Text = "toolStripButton3";
            this.tsbtnAlignRight.ToolTipText = "Align Right";
            this.tsbtnAlignRight.Click += TsbtnAlignment_Click;
            // 
            // tsbtnFontColor
            // 
            this.tsbtnFontColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnFontColor.Image = Resources.FontColor_small;
            this.tsbtnFontColor.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnFontColor.Name = "tsbtnFontColor";
            this.tsbtnFontColor.Size = new System.Drawing.Size(23, 23);
            this.tsbtnFontColor.Text = "toolStripButton4";
            this.tsbtnFontColor.ToolTipText = "Pick Font Color";
            this.tsbtnFontColor.Click += TsbtnFontColor_Click;

            this.tsbtnIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnIndent.Image = Resources.DecreaseIndent_small;
            this.tsbtnIndent.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnIndent.Name = "tsbtnIndent";
            this.tsbtnIndent.Size = new System.Drawing.Size(23, 23);
            this.tsbtnIndent.Text = "toolStripButton1";
            this.tsbtnIndent.ToolTipText = "Indent";
            this.tsbtnIndent.Click += TsbtnBulletsAndNumbering_Click;
            // 
            // tsbtnOutdent
            // 
            this.tsbtnOutdent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOutdent.Image = Resources.IncreaseIndent_small;
            this.tsbtnOutdent.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnOutdent.Name = "tsbtnOutdent";
            this.tsbtnOutdent.Size = new System.Drawing.Size(23, 23);
            this.tsbtnOutdent.Text = "toolStripButton3";
            this.tsbtnOutdent.ToolTipText = "Outdent";
            this.tsbtnOutdent.Click += TsbtnBulletsAndNumbering_Click;
            // 
            // tsbtnBullets
            // 
            this.tsbtnBullets.CheckOnClick = true;
            this.tsbtnBullets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnBullets.Image = Resources.Bullets_small;
            this.tsbtnBullets.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnBullets.Name = "tsbtnBullets";
            this.tsbtnBullets.Size = new System.Drawing.Size(23, 23);
            this.tsbtnBullets.Text = "toolStripButton2";
            this.tsbtnBullets.ToolTipText = "Toggle Bullets";
            this.tsbtnBullets.Click += TsbtnBulletsAndNumbering_Click;
            // 
            // tsbtnInsertPicture
            // 
            this.tsbtnInsertPicture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnInsertPicture.Image = Resources.Image_small;
            this.tsbtnInsertPicture.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnInsertPicture.Name = "tsbtnInsertPicture";
            this.tsbtnInsertPicture.Size = new System.Drawing.Size(23, 23);
            this.tsbtnInsertPicture.Text = "toolStripButton1";
            this.tsbtnInsertPicture.ToolTipText = "Insert Picture";
            this.tsbtnInsertPicture.Click += TsbtnInsertPicture_Click;

            this.Controls.Add(this.rtbDocument);
            this.Controls.Add(this.toolStrip1);

            toolStrip1.SuspendLayout();
            this.SuspendLayout();

            toolStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            toolStrip1.PerformLayout();
            this.PerformLayout();
        }

        private void TsbtnSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (status.Equals("cc"))
            {
                
            }
            else if (status.Equals("me"))
            {
                
            }
            else if (status.Equals("diag"))
            {
                
            }
            else if (status.Equals("operative_note"))
            {
                if (bc.hn.Length <= 0)
                {
                    MessageBox.Show("ไม่พบ HN บันทึก ไม่สำเร็จ ", "");
                    return;
                }
            }
            try
            {
                bc.cStf.staff_id = "";
                FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
                frm.ShowDialog(this);
                if (!bc.cStf.staff_id.Equals(""))
                {
                    String filename = "", ext = "";
                    if (!Directory.Exists("medical"))
                    {
                        Directory.CreateDirectory("medical");
                    }
                    filename = "medical\\" + hn + "_" + bc.vsdate + "_" + bc.preno + "_" + status + ".rtf";
                    SaveDocumentMedicalExamination(filename);
                    Application.DoEvents();
                    Thread.Sleep(200);
                    if (File.Exists(filename))
                    {
                        ext = Path.GetExtension(filename);
                        FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                        DocScan dsc = new DocScan();
                        dsc.active = "1";
                        dsc.doc_scan_id = "";
                        dsc.doc_group_id = "";
                        dsc.hn = bc.hn;
                        //dsc.vn = dt.Rows[0]["MNC_VN_NO"].ToString() + "/" + dt.Rows[0]["MNC_VN_SEQ"].ToString() + "(" + dt.Rows[0]["MNC_VN_SUM"].ToString() + ")";
                        dsc.an = "";

                        dsc.visit_date = bc.vsdate;
                        dsc.host_ftp = bc.iniC.hostFTP;
                        //dsc.image_path = txtHn.Text + "//" + txtHn.Text + "_" + dgssid + "_" + dsc.row_no + "." + ext[ext.Length - 1];
                        dsc.image_path = "";
                        dsc.doc_group_sub_id = "";
                        dsc.pre_no = bc.preno;

                        dsc.folder_ftp = bc.iniC.folderFTP;
                        //    dsc.status_ipd = chkIPD.Checked ? "I" : "O";
                        dsc.row_no = "1";
                        dsc.row_cnt = "1";
                        dsc.status_version = "2";
                        dsc.req_id = "";
                        dsc.date_req = "";
                        dsc.status_ipd = "0";
                        //dsc.ml_fm = "FM-LAB-999";
                        if (status.Equals("cc"))
                        {
                            dsc.ml_fm = "FM-MED-900";       //
                        }
                        else if (status.Equals("me"))
                        {
                            dsc.ml_fm = "FM-MED-901";       //
                        }
                        else if (status.Equals("diag"))
                        {
                            dsc.ml_fm = "FM-MED-902";       //
                        }
                        else if (status.Equals("operative_note"))
                        {
                            dsc.ml_fm = "FM-MED-903";       //
                        }
                        bc.bcDB.dscDB.voidDocScanByStatusMedicalExamination(hn, dsc.ml_fm, bc.vsdate, bc.preno, bc.cStf.staff_id);
                        dsc.patient_fullname = ptt.Name;
                        dsc.status_record = "5";        // status medical diagnose
                        dsc.comp_labout_id = "";
                        String re = bc.bcDB.dscDB.insertMedicalExamination(dsc, bc.userId);
                        dsc.image_path = hn + "//" + status + "_" + bc.hn + "_" + bc.vsdate + "_" + bc.preno + "_" + re + ext;
                        String re1 = bc.bcDB.dscDB.updateImagepath(dsc.image_path, re);
                        ftp.createDirectory(bc.iniC.folderFTP + "//" + hn.Replace("/", "-"));       // สร้าง Folder HN
                        ftp.delete(bc.iniC.folderFTP + "//" + dsc.image_path);
                        Thread.Sleep(100);
                        if (ftp.upload(bc.iniC.folderFTP + "//" + dsc.image_path, filename))
                        {
                            if (status.Equals("cc"))
                            {
                                
                            }
                            else if (status.Equals("me"))
                            {
                                
                            }
                            else if (status.Equals("diag"))
                            {
                                
                            }
                            else if (status.Equals("operative_note_precidures_1"))
                            {
                                bc.operative_note_precidures_1 = re;
                            }
                        }
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
        private void SaveDocumentMedicalExamination(String filename)
        {
            //string filePathName = this.documentPath + '\\' + this.documentName;
            try
            {
                String PathName = "medical";
                if (File.Exists(PathName + "" + filename))
                {
                    File.Delete(PathName + "" + filename);
                    System.Threading.Thread.Sleep(200);
                }
                //foreach(String rthline in richTextBox1.Lines)
                //{

                //}
                rtbDocument.SaveFile(filename, RichTextBoxStreamType.RichText);
                //bc.savePicOPUtoServer(txtIdOld.Text, documentName, filePathName);
            }
            catch (Exception ex)
            {

            }
        }
        private void RtbDocument_SelectionChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (rtbDocument.SelectionFont != null)
            {
                tsbtnBold.Checked = rtbDocument.SelectionFont.Bold;
                tsbtnItalic.Checked = rtbDocument.SelectionFont.Italic;
                tsbtnUnderline.Checked = rtbDocument.SelectionFont.Underline;

                //boldToolStripMenuItem.Checked = rtbDocument.SelectionFont.Bold;
                //italicToolStripMenuItem.Checked = rtbDocument.SelectionFont.Italic;
                //underlineToolStripMenuItem.Checked = rtbDocument.SelectionFont.Underline;

                switch (rtbDocument.SelectionAlignment)
                {
                    case HorizontalAlignment.Left:
                        tsbtnAlignLeft.Checked = true;
                        tsbtnAlignCenter.Checked = false;
                        tsbtnAlignRight.Checked = false;

                        //leftToolStripMenuItem.Checked = true;
                        //centerToolStripMenuItem.Checked = false;
                        //rightToolStripMenuItem.Checked = false;
                        break;

                    case HorizontalAlignment.Center:
                        tsbtnAlignLeft.Checked = false;
                        tsbtnAlignCenter.Checked = true;
                        tsbtnAlignRight.Checked = false;

                        //leftToolStripMenuItem.Checked = false;
                        //centerToolStripMenuItem.Checked = true;
                        //rightToolStripMenuItem.Checked = false;
                        break;

                    case HorizontalAlignment.Right:
                        tsbtnAlignLeft.Checked = false;
                        tsbtnAlignCenter.Checked = false;
                        tsbtnAlignRight.Checked = true;

                        //leftToolStripMenuItem.Checked = false;
                        //centerToolStripMenuItem.Checked = false;
                        //rightToolStripMenuItem.Checked = true;
                        break;
                }

                tsbtnBullets.Checked = rtbDocument.SelectionBullet;
                //bulletsToolStripMenuItem.Checked = rtbDocument.SelectionBullet;

                tscmbFont.SelectedItem = rtbDocument.SelectionFont.FontFamily.Name;
                tscmbFontSize.SelectedItem = rtbDocument.SelectionFont.Size.ToString();
            }
        }

        private void TscmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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

        private void TscmbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void TscmbFontSize_TextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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
        
        private void TsbtnBIU_Click(object sender, EventArgs e)
        {
            // bold, italic, underline
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

        private void TsbtnChooseFont_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            using (FontDialog dlg = new FontDialog())
            {
                if (rtbDocument.SelectionFont != null) dlg.Font = rtbDocument.SelectionFont;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    rtbDocument.SelectionFont = dlg.Font;
                }
            }
        }
        private void TsbtnAlignment_Click(object sender, EventArgs e)
        {
            // alignment: left, center, right
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
        private void TsbtnFontColor_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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
        private void TsbtnBulletsAndNumbering_Click(object sender, EventArgs e)
        {
            // bullets, indentation
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
        private void TsbtnInsertPicture_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (File.Exists(@"white_300_200.jpg"))
            {
                if (!Directory.Exists("temp_med"))
                {
                    Directory.CreateDirectory("temp_med");
                }
                String datetick = "", filename = "";
                datetick = DateTime.Now.Ticks.ToString();
                filename = "temp_med\\" + datetick + ".jpg";
                File.Copy(@"white_300_200.jpg", filename);
                var filePath = @filename;
                ProcessStartInfo Info = new ProcessStartInfo()
                {
                    FileName = "mspaint.exe",
                    WindowStyle = ProcessWindowStyle.Maximized,
                    Arguments = filePath
                };
                Process procPaint = Process.Start(Info);
                procPaint.WaitForExit();
                setImagePaint(filename);
                this.filename = filename;
            }
        }
        private void setImagePaint(String filename)
        {
            Clipboard.SetImage(System.Drawing.Image.FromFile(filename));
            rtbDocument.Paste();
        }
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
        private void loadDoctorDiag()
        {
            String mlfm = "";
            DocScan docCC = new DocScan();
            //DocScan docME = new DocScan();
            //DocScan docDiag = new DocScan();
            if (status.Equals("cc"))
            {
                mlfm = "FM-MED-900";
            }
            else if (title.Equals("me"))
            {
                mlfm = "FM-MED-901";
            }
            else if (title.Equals("diag"))
            {
                mlfm = "FM-MED-902";
            }
            docCC = bc.bcDB.dscDB.selectByStatusMedicalExamination(hn, mlfm, bc.vsdate, bc.preno);
            //docME = bc.bcDB.dscDB.selectByStatusMedicalExamination(hn, "FM-MED-901", vsdate, preno);
            //docDiag = bc.bcDB.dscDB.selectByStatusMedicalExamination(hn, "FM-MED-902", vsdate, preno);
            MemoryStream streamCC, streamME, streamDiag;
            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);

            streamCC = ftp.download(bc.iniC.folderFTP + "//" + docCC.image_path);
            Thread.Sleep(200);
            //streamME = ftp.download(bc.iniC.folderFTP + "//" + docME.image_path);
            //Thread.Sleep(200);
            //streamDiag = ftp.download(bc.iniC.folderFTP + "//" + docDiag.image_path);
            //Thread.Sleep(200);
            rtbDocument.Text = "";
            streamCC.Position = 0;
            if (streamCC.Length > 0)
            {
                rtbDocument.LoadFile(streamCC, RichTextBoxStreamType.RichText);
            }
        }
        private void FrmDoctorDiag1_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            foreach (FontFamily family in FontFamily.Families)
            {
                tscmbFont.Items.Add(family.Name);
            }
            tscmbFont.SelectedItem = "Microsoft Sans Serif";

            tscmbFontSize.SelectedItem = "9";
            loadDoctorDiag();
            theme1 = new C1.Win.C1Themes.C1ThemeController();
            theme1.SetTheme(toolStrip1, bc.iniC.themeApplication);
            //tstxtZoomFactor.Text = Convert.ToString(rtbDocument.ZoomFactor * 100);
        }
    }
}
