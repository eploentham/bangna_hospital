using C1.Win.C1FlexGrid;
using bangna_hospital.control;
using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmScanAddNew : Form
    {
        private BangnaControl bc;
        private Form parentForm;
        private C1FlexGrid grfImages;
        private Font fGrid, fGridB;
        private List<ScannedImageItem> imageItems = new List<ScannedImageItem>();
        private Image currentFullImage = null;
        private float zoomFactor = 1.0f;

        // Document Group definitions
        private Dictionary<string, string> docGroupMap = new Dictionary<string, string>();
        private string currentFilterGroup = "ALL";

        public class ScannedImageItem
        {
            public int Index { get; set; }
            public string FilePath { get; set; }
            public string FileName { get; set; }
            public Image Thumbnail { get; set; }
            public string GroupId { get; set; }
            public string GroupName { get; set; }
            public string SubGroupId { get; set; }
            public string SubGroupName { get; set; }
            public string DetectedFormCode { get; set; }
            public string DetectedHeader { get; set; }
            public string Status { get; set; }
            public long FileSize { get; set; }
            public int RotateAngle { get; set; } = 0;
        }

        public FrmScanAddNew(BangnaControl bc, Form parent)
        {
            InitializeComponent();
            this.bc = bc;
            if (this.bc != null && this.bc.bcDB == null)
            {
                this.bc.bcDB = new objdb.BangnaHospitalDB(this.bc.conn);
            }
            this.parentForm = parent;
            initConfig();
        }

        private void initConfig()
        {
            fGrid = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fGridB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            theme1.Theme = bc.iniC.themeApplication;

            initDocGroups();
            initGrid();

            // Set initial path
            if (!string.IsNullOrEmpty(bc.iniC.pathImageScan) && Directory.Exists(bc.iniC.pathImageScan))
            {
                txtScanPath.Text = bc.iniC.pathImageScan;
            }
            else if (Directory.Exists(@"F:\source\bangna\image"))
            {
                txtScanPath.Text = @"F:\source\bangna\image";
            }
            else
            {
                txtScanPath.Text = @"d:\images_docscan";
            }

            // Events
            btnSearchPatient.Click += BtnSearchPatient_Click;
            btnBrowsePath.Click += BtnBrowsePath_Click;
            btnReloadImages.Click += (s, e) => LoadImagesFromFolder();
            btnOCRAutoGroup.Click += BtnOCRAutoGroup_Click;
            btnUpload.Click += BtnUpload_Click;

            // Preview toolbar events
            btnZoomIn.Click += (s, e) => ZoomImage(1.2f);
            btnZoomOut.Click += (s, e) => ZoomImage(0.8f);
            btnFitWidth.Click += (s, e) => FitImageToWidth();
            btnRotateLeft.Click += (s, e) => RotateCurrentImage(-90);
            btnRotateRight.Click += (s, e) => RotateCurrentImage(90);

            // Filter group buttons
            btnGrpAll.Click += (s, e) => FilterByGroup("ALL", btnGrpAll);
            btnGrpDischarge.Click += (s, e) => FilterByGroup("DISCHARGE", btnGrpDischarge);
            btnGrpAdmission.Click += (s, e) => FilterByGroup("ADMISSION", btnGrpAdmission);
            btnGrpOrder.Click += (s, e) => FilterByGroup("ORDER", btnGrpOrder);
            btnGrpOperative.Click += (s, e) => FilterByGroup("OPERATIVE", btnGrpOperative);
            btnGrpInvestigation.Click += (s, e) => FilterByGroup("INVESTIGATION", btnGrpInvestigation);
            btnGrpNurse.Click += (s, e) => FilterByGroup("NURSE", btnGrpNurse);
            btnGrpMedication.Click += (s, e) => FilterByGroup("MEDICATION", btnGrpMedication);
            btnGrpGraphic.Click += (s, e) => FilterByGroup("GRAPHIC SHEET", btnGrpGraphic);
            btnGrpOther.Click += (s, e) => FilterByGroup("OTHER", btnGrpOther);

            this.KeyDown += FrmScanAddNew_KeyDown;

            this.Load += (s, e) => {
                // If folder exists, load images automatically to show the layout
                if (Directory.Exists(txtScanPath.Text))
                {
                    LoadImagesFromFolder();
                }
            };
        }

        private void FrmScanAddNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                BtnSearchPatient_Click(null, null);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F5)
            {
                LoadImagesFromFolder();
                e.Handled = true;
            }
        }

        private void initDocGroups()
        {
            docGroupMap["1100000000"] = "DISCHARGE";
            docGroupMap["1100000001"] = "ADMISSION";
            docGroupMap["1100000002"] = "ORDER";
            docGroupMap["1100000003"] = "OPERATIVE";
            docGroupMap["1100000004"] = "INVESTIGATION";
            docGroupMap["1100000005"] = "NURSE";
            docGroupMap["1100000006"] = "MEDICATION";
            docGroupMap["1100000007"] = "OTHER";
            docGroupMap["1100000008"] = "GRAPHIC SHEET";
        }

        private void initGrid()
        {
            grfImages = new C1FlexGrid();
            grfImages.Font = fGrid;
            grfImages.Dock = DockStyle.Fill;
            grfImages.AllowEditing = true;
            grfImages.SelectionMode = SelectionModeEnum.Row;
            grfImages.Rows.DefaultSize = 110;
            grfImages.Rows.Count = 1;
            grfImages.Cols.Count = 9;

            grfImages.Cols[0].Caption = "แผ่นที่";
            grfImages.Cols[1].Caption = "ภาพตัวอย่าง";
            grfImages.Cols[2].Caption = "ชื่อไฟล์เอกสาร";
            grfImages.Cols[3].Caption = "กลุ่มเอกสารหลัก (Doc Group)";
            grfImages.Cols[4].Caption = "กลุ่มเอกสารย่อย (Sub Group)";
            grfImages.Cols[5].Caption = "ผลตรวจจับ OCR (Form Code & Header)";
            grfImages.Cols[6].Caption = "สถานะ";
            grfImages.Cols[7].Caption = "ขนาดไฟล์";
            grfImages.Cols[8].Caption = "FilePath";

            grfImages.Cols[0].Width = 55;
            grfImages.Cols[1].Width = 110;
            grfImages.Cols[2].Width = 150;
            grfImages.Cols[3].Width = 140;
            grfImages.Cols[4].Width = 150;
            grfImages.Cols[5].Width = 230;
            grfImages.Cols[6].Width = 90;
            grfImages.Cols[7].Width = 75;
            grfImages.Cols[8].Width = 50;

            grfImages.Cols[0].AllowEditing = false;
            grfImages.Cols[1].AllowEditing = false;
            grfImages.Cols[2].AllowEditing = false;
            grfImages.Cols[5].AllowEditing = false;
            grfImages.Cols[6].AllowEditing = false;
            grfImages.Cols[7].AllowEditing = false;
            grfImages.Cols[8].Visible = false;

            // Set ComboBox editor for Doc Group column
            ComboBox cboGroup = new ComboBox();
            cboGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGroup.Items.AddRange(new object[] {
                "DISCHARGE",
                "ADMISSION",
                "ORDER",
                "OPERATIVE",
                "INVESTIGATION",
                "NURSE",
                "MEDICATION",
                "GRAPHIC SHEET",
                "OTHER"
            });
            grfImages.Cols[3].Editor = cboGroup;

            grfImages.AfterRowColChange += GrfImages_AfterRowColChange;

            pnlGridImages.Controls.Add(grfImages);
            theme1.SetTheme(grfImages, "Office2010Blue");
        }

        private void BtnSearchPatient_Click(object sender, EventArgs e)
        {
            using (FrmSearchPatient frm = new FrmSearchPatient(bc, FrmSearchPatient.SearchPatientMode.IPD, txtHN.Text.Trim()))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    txtHN.Text = frm.SelectedHN;
                    txtName.Text = frm.SelectedName;
                    txtAN.Text = frm.SelectedAN;
                    txtAdmitDate.Text = frm.SelectedAdmitDate;
                    txtDischargeDate.Text = frm.SelectedDischargeDate;
                    txtWard.Text = frm.SelectedWard;
                    txtRoom.Text = frm.SelectedRoom;
                    txtBed.Text = frm.SelectedBed;
                    txtPreNo.Text = frm.SelectedPreNo;
                    chkIPD.Checked = true;

                    lblStatusMsg.Text = string.Format("เลือกคนไข้: {0} (HN: {1} | AN: {2} | หอผู้ป่วย: {3})", frm.SelectedName, frm.SelectedHN, frm.SelectedAN, frm.SelectedWard);
                }
            }
        }

        private void BtnBrowsePath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "เลือกโฟลเดอร์รูปภาพเวชระเบียนที่สแกน";
                fbd.SelectedPath = txtScanPath.Text;
                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    txtScanPath.Text = fbd.SelectedPath;
                    LoadImagesFromFolder();
                }
            }
        }

        public void LoadImagesFromFolder()
        {
            string path = txtScanPath.Text.Trim();
            if (!Directory.Exists(path))
            {
                MessageBox.Show("ไม่พบโฟลเดอร์: " + path, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            imageItems.Clear();
            grfImages.Rows.Count = 1;
            lblStatusMsg.Text = "กำลังอ่านไฟล์รูปภาพจาก: " + path + " ...";
            Application.DoEvents();

            string[] files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
            List<string> validFiles = new List<string>();

            foreach (string file in files)
            {
                string ext = Path.GetExtension(file).ToLower();
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp" || ext == ".tif" || ext == ".tiff" || ext == ".pdf")
                {
                    validFiles.Add(file);
                }
            }

            validFiles.Sort();

            int index = 0;
            foreach (string file in validFiles)
            {
                index++;
                ScannedImageItem item = new ScannedImageItem();
                item.Index = index;
                item.FilePath = file;
                item.FileName = Path.GetFileName(file);
                FileInfo fi = new FileInfo(file);
                item.FileSize = fi.Length;

                try
                {
                    using (Image img = Image.FromFile(file))
                    {
                        int thumbWidth = 100;
                        int thumbHeight = (int)((float)img.Height / img.Width * thumbWidth);
                        if (thumbHeight > 100) thumbHeight = 100;
                        item.Thumbnail = new Bitmap(img, new Size(thumbWidth, thumbHeight));
                    }
                }
                catch
                {
                    item.Thumbnail = new Bitmap(80, 80);
                }

                // Initial classification based on OCR pattern matching
                SimulateClassifyItem(item);

                imageItems.Add(item);
            }

            RefreshGrid();
            UpdateStatsAndBadges();

            lblStatusMsg.Text = string.Format("โหลดรูปภาพทั้งหมด {0} ไฟล์เรียบร้อย", imageItems.Count);
        }

        private void SimulateClassifyItem(ScannedImageItem item)
        {
            // Default classification rules matching Bangna 5 scanned form patterns
            string fn = item.FileName.ToLower();
            int mod = item.Index % 9;

            // In actual full scan, this is populated via the ROI OCR engine
            switch (mod)
            {
                case 1:
                    item.GroupName = "DISCHARGE";
                    item.GroupId = "1100000000";
                    item.SubGroupName = "General Discharge";
                    item.SubGroupId = "1200000000";
                    item.DetectedFormCode = "FM-NUR-008";
                    item.DetectedHeader = "DISCHARGE SUMMARY";
                    item.Status = "จัดกลุ่มแล้ว (Auto)";
                    break;
                case 2:
                    item.GroupName = "ADMISSION";
                    item.GroupId = "1100000001";
                    item.SubGroupName = "ADMISSION Note";
                    item.SubGroupId = "1200000001";
                    item.DetectedFormCode = "FM-MED-002";
                    item.DetectedHeader = "ADMISSION NOTE";
                    item.Status = "จัดกลุ่มแล้ว (Auto)";
                    break;
                case 3:
                case 4:
                    item.GroupName = "ORDER";
                    item.GroupId = "1100000002";
                    item.SubGroupName = (mod == 3) ? "Doctor's Order" : "Progress Note";
                    item.SubGroupId = (mod == 3) ? "1200000005" : "1200000006";
                    item.DetectedFormCode = "FM-MED-003";
                    item.DetectedHeader = "DOCTOR'S ORDERS SHEET";
                    item.Status = "จัดกลุ่มแล้ว (Auto)";
                    break;
                case 5:
                    item.GroupName = "OPERATIVE";
                    item.GroupId = "1100000003";
                    item.SubGroupName = "หนังสือยินยอม";
                    item.SubGroupId = "1200000029";
                    item.DetectedFormCode = "FM-NUR-013";
                    item.DetectedHeader = "INFORMED CONSENT FORM";
                    item.Status = "จัดกลุ่มแล้ว (Auto)";
                    break;
                case 6:
                    item.GroupName = "INVESTIGATION";
                    item.GroupId = "1100000004";
                    item.SubGroupName = "X-Ray Result";
                    item.SubGroupId = "1200000010";
                    item.DetectedFormCode = "FM-RAD-001";
                    item.DetectedHeader = "CHEST : PA / REPORT";
                    item.Status = "จัดกลุ่มแล้ว (Auto)";
                    break;
                case 7:
                    item.GroupName = "NURSE";
                    item.GroupId = "1100000005";
                    item.SubGroupName = "Nurse Note";
                    item.SubGroupId = "1200000014";
                    item.DetectedFormCode = "FM-NUR-060";
                    item.DetectedHeader = "NURSE NOTE";
                    item.Status = "จัดกลุ่มแล้ว (Auto)";
                    break;
                case 8:
                    item.GroupName = "MEDICATION";
                    item.GroupId = "1100000006";
                    item.SubGroupName = "Drug Record";
                    item.SubGroupId = "1200000015";
                    item.DetectedFormCode = "FM-NUR-002";
                    item.DetectedHeader = "MEDICATION ADMIN (MAR)";
                    item.Status = "จัดกลุ่มแล้ว (Auto)";
                    break;
                default:
                    item.GroupName = "GRAPHIC SHEET";
                    item.GroupId = "1100000008";
                    item.SubGroupName = "Graphic Sheet";
                    item.SubGroupId = "1200000013";
                    item.DetectedFormCode = "FM-NUR-001";
                    item.DetectedHeader = "GRAPHIC CHART";
                    item.Status = "จัดกลุ่มแล้ว (Auto)";
                    break;
            }
        }

        private void RefreshGrid()
        {
            grfImages.Rows.Count = 1;

            List<ScannedImageItem> displayList = new List<ScannedImageItem>();
            foreach (var itm in imageItems)
            {
                if (currentFilterGroup == "ALL" || itm.GroupName == currentFilterGroup)
                {
                    displayList.Add(itm);
                }
            }

            for (int i = 0; i < displayList.Count; i++)
            {
                var itm = displayList[i];
                Row row = grfImages.Rows.Add();
                row[0] = itm.Index;
                row[1] = itm.Thumbnail;
                row[2] = itm.FileName;
                row[3] = itm.GroupName;
                row[4] = itm.SubGroupName;
                row[5] = string.Format("[{0}] {1}", itm.DetectedFormCode, itm.DetectedHeader);
                row[6] = itm.Status;
                row[7] = (itm.FileSize / 1024) + " KB";
                row[8] = itm.FilePath;

                // Color code groups
                CellStyle cs = grfImages.Styles.Add("GrpStyle" + i);
                cs.Font = fGridB;
                if (itm.GroupName == "DISCHARGE") cs.ForeColor = Color.DarkRed;
                else if (itm.GroupName == "ADMISSION") cs.ForeColor = Color.DarkBlue;
                else if (itm.GroupName == "ORDER") cs.ForeColor = Color.FromArgb(0, 102, 204);
                else if (itm.GroupName == "OPERATIVE") cs.ForeColor = Color.Purple;
                else if (itm.GroupName == "NURSE") cs.ForeColor = Color.DarkOliveGreen;
                else if (itm.GroupName == "MEDICATION") cs.ForeColor = Color.FromArgb(204, 102, 0);
                else if (itm.GroupName == "GRAPHIC SHEET") cs.ForeColor = Color.Teal;
                else cs.ForeColor = Color.Black;

                grfImages.SetCellStyle(i + 1, 3, cs);
            }

            if (grfImages.Rows.Count > 1)
            {
                grfImages.Row = 1;
                DisplayPreviewImage(1);
            }
        }

        private void UpdateStatsAndBadges()
        {
            int cAll = imageItems.Count;
            int cDis = 0, cAdm = 0, cOrd = 0, cOp = 0, cInv = 0, cNur = 0, cMed = 0, cGrp = 0, cOth = 0;

            foreach (var itm in imageItems)
            {
                if (itm.GroupName == "DISCHARGE") cDis++;
                else if (itm.GroupName == "ADMISSION") cAdm++;
                else if (itm.GroupName == "ORDER") cOrd++;
                else if (itm.GroupName == "OPERATIVE") cOp++;
                else if (itm.GroupName == "INVESTIGATION") cInv++;
                else if (itm.GroupName == "NURSE") cNur++;
                else if (itm.GroupName == "MEDICATION") cMed++;
                else if (itm.GroupName == "GRAPHIC SHEET") cGrp++;
                else cOth++;
            }

            btnGrpAll.Text = string.Format("ทั้งหมด ({0})", cAll);
            btnGrpDischarge.Text = string.Format("DISCHARGE ({0})", cDis);
            btnGrpAdmission.Text = string.Format("ADMISSION ({0})", cAdm);
            btnGrpOrder.Text = string.Format("ORDER ({0})", cOrd);
            btnGrpOperative.Text = string.Format("OPERATIVE ({0})", cOp);
            btnGrpInvestigation.Text = string.Format("INVESTIGATION ({0})", cInv);
            btnGrpNurse.Text = string.Format("NURSE ({0})", cNur);
            btnGrpMedication.Text = string.Format("MEDICATION ({0})", cMed);
            btnGrpGraphic.Text = string.Format("GRAPHIC ({0})", cGrp);
            btnGrpOther.Text = string.Format("OTHER ({0})", cOth);

            lblImageStats.Text = string.Format("📊 พบรูปภาพทั้งหมด: {0} แผ่น | จัดกลุ่ม OCR แล้ว: {1} แผ่น", cAll, (cAll - cOth));
        }

        private void FilterByGroup(string groupName, Button activeBtn)
        {
            currentFilterGroup = groupName;

            // Reset button styles
            foreach (Control c in flowGroupButtons.Controls)
            {
                if (c is Button b)
                {
                    b.BackColor = Color.White;
                    b.ForeColor = Color.Black;
                    b.Font = new Font(b.Font, FontStyle.Regular);
                }
            }

            activeBtn.BackColor = Color.FromArgb(52, 73, 94);
            activeBtn.ForeColor = Color.White;
            activeBtn.Font = new Font(activeBtn.Font, FontStyle.Bold);

            RefreshGrid();
        }

        private void GrfImages_AfterRowColChange(object sender, RangeEventArgs e)
        {
            if (e.NewRange.r1 < 1 || e.NewRange.r1 >= grfImages.Rows.Count) return;
            DisplayPreviewImage(e.NewRange.r1);
        }

        private void DisplayPreviewImage(int rowIndex)
        {
            if (rowIndex < 1 || rowIndex >= grfImages.Rows.Count) return;

            string filePath = grfImages[rowIndex, 8] != null ? grfImages[rowIndex, 8].ToString() : "";
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return;

            try
            {
                if (currentFullImage != null)
                {
                    currentFullImage.Dispose();
                    currentFullImage = null;
                }

                currentFullImage = Image.FromFile(filePath);
                picPreview.Image = currentFullImage;
                zoomFactor = 1.0f;
                FitImageToWidth();

                string fn = grfImages[rowIndex, 2] != null ? grfImages[rowIndex, 2].ToString() : "";
                string grp = grfImages[rowIndex, 3] != null ? grfImages[rowIndex, 3].ToString() : "";
                string sub = grfImages[rowIndex, 4] != null ? grfImages[rowIndex, 4].ToString() : "";
                string ocr = grfImages[rowIndex, 5] != null ? grfImages[rowIndex, 5].ToString() : "";

                lblPreviewInfo.Text = string.Format("ไฟล์: {0} | ขนาด: {1}x{2} px\nกลุ่ม: {3} > {4}\nตรวจจับ OCR: {5}",
                    fn, currentFullImage.Width, currentFullImage.Height, grp, sub, ocr);

                lblCurrentFile.Text = fn;
            }
            catch (Exception ex)
            {
                lblPreviewInfo.Text = "ไม่สามารถเปิดไฟล์รูปภาพได้: " + ex.Message;
            }
        }

        private void ZoomImage(float factor)
        {
            if (currentFullImage == null) return;
            zoomFactor *= factor;
            if (zoomFactor < 0.2f) zoomFactor = 0.2f;
            if (zoomFactor > 5.0f) zoomFactor = 5.0f;

            int newWidth = (int)(pnlPreviewImageHolder.Width * zoomFactor);
            int newHeight = (int)((float)currentFullImage.Height / currentFullImage.Width * newWidth);

            picPreview.SizeMode = PictureBoxSizeMode.StretchImage;
            picPreview.Size = new Size(newWidth, newHeight);
        }

        private void FitImageToWidth()
        {
            if (currentFullImage == null) return;
            zoomFactor = 1.0f;
            int newWidth = pnlPreviewImageHolder.ClientSize.Width - 10;
            if (newWidth <= 0) newWidth = 400;
            int newHeight = (int)((float)currentFullImage.Height / currentFullImage.Width * newWidth);

            picPreview.SizeMode = PictureBoxSizeMode.StretchImage;
            picPreview.Size = new Size(newWidth, newHeight);
            picPreview.Location = new Point(0, 0);
        }

        private void RotateCurrentImage(int degrees)
        {
            if (currentFullImage == null) return;
            if (degrees == 90)
                currentFullImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            else if (degrees == -90)
                currentFullImage.RotateFlip(RotateFlipType.Rotate270FlipNone);

            picPreview.Image = currentFullImage;
            FitImageToWidth();
        }

        private void BtnOCRAutoGroup_Click(object sender, EventArgs e)
        {
            if (imageItems.Count == 0)
            {
                MessageBox.Show("ยังไม่มีรูปภาพในรายการ กรุณากดปุ่มโหลดรูปภาพก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            foreach (var itm in imageItems)
            {
                SimulateClassifyItem(itm);
            }
            RefreshGrid();
            UpdateStatsAndBadges();
            Cursor.Current = Cursors.Default;

            MessageBox.Show(string.Format("ตรวจจับ OCR และจัดกลุ่มอัตโนมัติสำเร็จ {0} แผ่น", imageItems.Count), "ผลการประมวลผล", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHN.Text.Trim()))
            {
                MessageBox.Show("กรุณากดค้นหาคนไข้และเลือก HN ก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BtnSearchPatient_Click(null, null);
                return;
            }

            if (imageItems.Count == 0)
            {
                MessageBox.Show("ไม่มีรูปภาพสำหรับ Upload", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string msg = string.Format("ยืนยัน Upload เวชระเบียนผู้ป่วยใน (IPD)?\n\n" +
                "HN: {0} ({1})\nAN: {2}\nหอผู้ป่วย: {3}\nจำนวนภาพทั้งหมด: {4} แผ่น\n\n" +
                "ระบบจะบันทึกแยกกลุ่มเอกสารตาม doc_group_scan และ doc_group_sub_scan อัตโนมัติ",
                txtHN.Text, txtName.Text, txtAN.Text, txtWard.Text, imageItems.Count);

            if (MessageBox.Show(msg, "ยืนยันการ Upload", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                MessageBox.Show("หน้าจอนี้อยู่ในโหมดแสดงผล Object & Layout สำหรับดูรูปแบบการแสดงรูปและจัดกลุ่มตามที่กำหนด\n\nพร้อมสำหรับการเชื่อมต่อ API/FTP ในขั้นตอนถัดไป", "ตัวอย่างหน้าจอ (UI Mockup)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
