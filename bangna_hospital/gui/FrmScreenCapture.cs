using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1List;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmScreenCapture : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;
        C1PictureBox picScr;
        C1FlexGrid grfView, grfUpload;
        C1List listView;
        private System.IO.FileSystemWatcher m_Watcher;
        List<String> lFile;
        Patient ptt;

        int colUploadId=1, colUploadName=2, colUpload
        public FrmScreenCapture(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            lFile = new List<string>();

            picScr = new C1PictureBox();
            picScr.Location = new System.Drawing.Point(0, 0);
            picScr.Dock = DockStyle.Fill;
            picScr.Name = "picScr";
            //picScr.Size = new System.Drawing.Size(screenWidth / 2, screenHeight);
            //picScr.Image = Resources.screen_first_l;
            picScr.SizeMode = PictureBoxSizeMode.StretchImage;
            pnPic.Controls.Add(picScr);
            
            this.Activated += FrmScreenCapture_Activated;
            this.FormClosed += FrmScreenCapture_FormClosed;
            txtHn.KeyUp += TxtHn_KeyUp;
            chkView.Click += ChkView_Click;
            chkUpload.Click += ChkUpload_Click;

            chkUpload.Checked = true;
            initGrfView();
        }

        private void ChkUpload_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            initGrfView();
            getListFile();
        }

        private void ChkView_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            initGrfUpload();
        }

        private void TxtHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                ptt = new Patient();
                ptt = bc.bcDB.pttDB.selectPatinet(txtHn.Text.Trim());
                lbName.Text = ptt.Name;
            }
        }

        private void FrmScreenCapture_Activated(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            getListFile();
        }

        private void FrmScreenCapture_FormClosed(object sender, FormClosedEventArgs e)
        {
            //throw new NotImplementedException();
            //m_Watcher.Dispose();
        }
        private void initGrfUpload()
        {
            if(grfView != null)
            {
                grfView.Dispose();
            }
            grfUpload = new C1FlexGrid();
            grfUpload.Font = fEdit;
            grfUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            grfUpload.Location = new System.Drawing.Point(0, 0);

            grfUpload.Rows[0].Visible = false;
            grfUpload.Cols[0].Visible = false;
            pnView.Controls.Add(grfUpload);

            grfUpload.Rows.Count = 1;
            grfUpload.Cols.Count = 3;
            grfUpload.Cols[1].Width = this.Width - 50;

            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_View_Print));
            menuGw.MenuItems.Add("ต้องการ ลบข้อมูลนี้", new EventHandler(ContextMenu_View_Delete));
            //mouseWheel = 0;
            //pic.MouseWheel += Pic_MouseWheel;
            grfUpload.ContextMenu = menuGw;

            grfUpload.Cols[2].Visible = false;
        }
        private void ContextMenu_View_Print(object sender, System.EventArgs e)
        {

        }
        private void ContextMenu_View_Delete(object sender, System.EventArgs e)
        {

        }
        private void setGrfUpload()
        {

        }
        private void initGrfView()
        {
            if (grfUpload != null)
            {
                grfUpload.Dispose();
            }
            grfView = new C1FlexGrid();
            grfView.Font = fEdit;
            grfView.Dock = System.Windows.Forms.DockStyle.Fill;
            grfView.Location = new System.Drawing.Point(0, 0);
            
            grfView.Rows[0].Visible = false;
            grfView.Cols[0].Visible = false;
            pnView.Controls.Add(grfView);

            grfView.Rows.Count = 1;
            grfView.Cols.Count = 2;
            grfView.Cols[1].Width = this.Width-50;

            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ Upload ภาพนี้", new EventHandler(ContextMenu_UpLoad));
            menuGw.MenuItems.Add("ต้องการ ลบข้อมูลนี้", new EventHandler(ContextMenu_Delete));
            //mouseWheel = 0;
            //pic.MouseWheel += Pic_MouseWheel;
            grfView.ContextMenu = menuGw;
        }
        private void ContextMenu_UpLoad(object sender, System.EventArgs e)
        {
            String filename = "";
            if (txtHn.Text.Length == 0)
            {
                MessageBox.Show("ไม่พบ HN", "");
                return;
            }
            if (lbName.Text.Length == 0)
            {
                MessageBox.Show("ไม่พบ ชื่อ คนไข้", "");
                return;
            }
            filename = grfView[grfView.Row, 1].ToString();
            if (File.Exists(filename))
            {
                FrmScreenCaptureUpload frm = new FrmScreenCaptureUpload(bc, filename, txtHn.Text.Trim(), lbName.Text);
                frm.ShowDialog(this);
                getListFile();
            }
            else
            {
                MessageBox.Show("ไม่พบ File Upload", "");
            }
        }
        private void ContextMenu_Delete(object sender, System.EventArgs e)
        {
            
        }
        private void getListFile()
        {
            DirectoryInfo dir = new DirectoryInfo(bc.iniC.pathScreenCaptureUpload);
            FileInfo[] Files = dir.GetFiles("*.*");
            string str = "";
            //grfView.Rows.Count = 4;
            //int i = 0;
            lFile.Clear();
            foreach (FileInfo file in Files)
            {
                lFile.Add(file.FullName);
            }
            setListView();
        }
        private void setListView()
        {
            grfView.Rows.Count = 0;
            foreach (String file in lFile)
            {
                Row row = grfView.Rows.Add();
                row[1] = file;
            }
        }

        private void BtnCapture_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.Hide();
            System.Threading.Thread.Sleep(500);
            Application.DoEvents();
            FullScreenshot(Application.StartupPath, "capture.jpg", ImageFormat.Jpeg);
            Application.DoEvents();
            this.Show();
            //WindowScreenshotWithoutClass(Application.StartupPath, "capture1.jpg", ImageFormat.Jpeg);
            //CaptureScreenToFile("capture2.jpg", ImageFormat.Jpeg);
        }
        private void WindowScreenshotWithoutClass(String filepath, String filename, ImageFormat format)
        {
            Rectangle bounds = this.Bounds;

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }

                string fullpath = filepath + "\\" + filename;

                bitmap.Save(fullpath, format);
            }
        }
        private void FullScreenshot(String filepath, String filename, ImageFormat format)
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }

                string fullpath = filepath + "\\" + filename;

                bitmap.Save(fullpath, format);
            }
        }
        public Image CaptureScreen()
        {
            return CaptureWindow(User32.GetDesktopWindow());
        }
        public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);
        }
        public void CaptureScreenToFile(string filename, ImageFormat format)
        {
            Image img = CaptureScreen();
            img.Save(filename, format);
        }
        public Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up 
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
            return img;
        }
        private void FrmScreenCapture_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.Manual;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new System.Drawing.Point(5, screenHeight - this.Height - 40);
        }
    }
}
