using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Command;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmCapture:Form
    {
        BangnaControl bc;
        Font fEdit, fEditB;

        C1DockingTab tC1;
        C1.Win.C1Ribbon.C1StatusBar sb1;
        C1.Win.C1Themes.C1ThemeController theme1;
        C1Button btnCap1;

        Panel panel1, pnHead, pnBotton;
        FrmNurseScanView frm;
        public FrmCapture(BangnaControl bc, FrmNurseScanView frm)
        {
            this.bc = bc;
            this.frm = frm;
            initConfig();
        }
        private void initConfig()
        {
            initCompoment();
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);

            this.Load += FrmCapture_Load;
            this.Disposed += FrmCapture_Disposed;
            btnCap1.Click += BtnCap1_Click;
        }

        private void BtnCap1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!Directory.Exists(bc.iniC.pathScreenCaptureSend ))
            {
                Directory.CreateDirectory(bc.iniC.pathScreenCaptureSend);
            }
            this.Hide();
            Thread.Sleep(200);
            Application.DoEvents();
            ScreenCapture sc = new ScreenCapture();
            sc.CaptureScreenToFile(bc.iniC.pathScreenCaptureSend+"\\cap1.jpg", ImageFormat.Jpeg);
            Thread.Sleep(200);
            Application.DoEvents();
            C1DockingTabPage tabCap = new C1DockingTabPage();
            Panel pnCap = new Panel();
            pnCap.Dock = DockStyle.Fill;
            C1PictureBox pic = new C1PictureBox();
            pic.Dock = DockStyle.Fill;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            if(File.Exists(bc.iniC.pathScreenCaptureSend + "\\cap1.jpg"))
            {
                pic.Image = Image.FromFile(bc.iniC.pathScreenCaptureSend + "\\cap1.jpg");
            }
            tabCap.Name = "tabCap";
            tabCap.TabIndex = 0;
            tabCap.Text = "Capture 1";
            tabCap.Font = fEditB;
            tC1.Controls.Add(tabCap);
            tabCap.Controls.Add(pnCap);
            pnCap.Controls.Add(pic);

            this.Show();
        }

        private void FrmCapture_Disposed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            frm.Show();
        }

        private void initCompoment()
        {
            int gapLine = 20, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;

            this.sb1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.panel1 = new Panel();
            pnHead = new System.Windows.Forms.Panel();
            pnBotton = new System.Windows.Forms.Panel();
            this.theme1 = new C1.Win.C1Themes.C1ThemeController();
            tC1 = new C1DockingTab();

            panel1.SuspendLayout();
            pnHead.SuspendLayout();
            pnBotton.SuspendLayout();
            tC1.SuspendLayout();
            this.SuspendLayout();

            sb1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            sb1.Location = new System.Drawing.Point(0, 620);
            sb1.Name = "sb1";
            sb1.Size = new System.Drawing.Size(956, 22);
            sb1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Custom;

            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(956, 620);
            panel1.TabIndex = 0;
            pnHead.Size = new System.Drawing.Size(scrW, 50);
            pnHead.BorderStyle = BorderStyle.Fixed3D;
            pnHead.Dock = DockStyle.Top;
            pnBotton.Dock = DockStyle.Fill;
            pnBotton.BorderStyle = BorderStyle.FixedSingle;

            tC1.Dock = System.Windows.Forms.DockStyle.Fill;
            tC1.HotTrack = true;
            tC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tC1.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            tC1.TabsShowFocusCues = true;
            tC1.Alignment = TabAlignment.Bottom;
            tC1.SelectedTabBold = true;
            tC1.Name = "tC1";

            setControlComponent();

            this.Controls.Add(panel1);
            this.Controls.Add(this.sb1);
            panel1.Controls.Add(pnBotton);
            panel1.Controls.Add(pnHead);
            pnBotton.Controls.Add(tC1);
            pnHead.Controls.Add(btnCap1);
            this.WindowState = FormWindowState.Maximized;

            panel1.ResumeLayout(false);
            pnHead.ResumeLayout(false);
            pnBotton.ResumeLayout(false);
            tC1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void setControlComponent()
        {
            int gapLine = 10, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;

            btnCap1 = new C1Button();
            btnCap1.Name = "btnCap1";
            btnCap1.Text = "Capture 1";
            btnCap1.Font = fEdit;
            //size = bc.MeasureString(btnHnSearch);
            btnCap1.Location = new System.Drawing.Point(gapX, 10);
            size = bc.MeasureString(btnCap1);
            btnCap1.Size = new Size(size.Width+10, 30);
            btnCap1.Font = fEdit;
        }
        private void FrmCapture_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            
        }
    }
}
