using bangna_hospital.control;
using bangna_hospital.Properties;
using C1.Win.C1Input;
using C1.Win.C1Ribbon;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmScanEdit:Form
    {
        BangnaControl bc;
        Form frmFlash;
        Font fEdit, fEditB, fEditBig;
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1StatusBar sb1;
        C1ThemeController theme1;
        Panel panel1, pnHead, pnBotton;
        Label lbTxtPttHn;
        C1TextBox txtPttHn;
        C1Button btnSave;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrmScanEdit
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "FrmScanEdit";
            this.Load += new System.EventHandler(this.FrmScanEdit_Load);
            this.ResumeLayout(false);

        }
        public FrmScanEdit(BangnaControl bc)
        {
            showFormWaiting();
            this.bc = bc;
            initConfig();
            frmFlash.Dispose();
        }
        private void initConfig()
        {
            initCompoment();
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
        }
        private void initCompoment()
        {
            int gapLine = 20, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;

            sb1 = new C1.Win.C1Ribbon.C1StatusBar();
            panel1 = new Panel();
            pnHead = new Panel();
            pnBotton = new Panel();
            theme1 = new C1.Win.C1Themes.C1ThemeController();

            panel1.SuspendLayout();
            pnHead.SuspendLayout();
            pnBotton.SuspendLayout();

            panel1.Controls.Add(pnHead);
            panel1.Controls.Add(pnBotton);
            this.Controls.Add(panel1);
            this.Controls.Add(sb1);

            panel1.ResumeLayout(false);
            pnHead.ResumeLayout(false);
            pnBotton.ResumeLayout(false);
            panel1.PerformLayout();
            pnHead.PerformLayout();
            pnBotton.PerformLayout();
            this.PerformLayout();
        }
        private void showFormWaiting()
        {
            frmFlash = new Form();
            frmFlash.Size = new Size(300, 300);
            frmFlash.StartPosition = FormStartPosition.CenterScreen;
            C1PictureBox picFlash = new C1PictureBox();
            //Image img = new Image();
            picFlash.SuspendLayout();
            picFlash.Image = Resources.loading_transparent;
            picFlash.Width = 230;
            picFlash.Height = 230;
            picFlash.Location = new Point(30, 10);
            picFlash.SizeMode = PictureBoxSizeMode.StretchImage;
            frmFlash.Controls.Add(picFlash);
            picFlash.ResumeLayout();
            frmFlash.Show();
            Application.DoEvents();
        }
        private void FrmScanEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
