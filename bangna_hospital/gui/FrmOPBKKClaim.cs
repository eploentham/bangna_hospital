using bangna_hospital.control;
using C1.C1Pdf;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmOPBKKClaim:Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit3B, fEdit5B;

        C1DockingTab tcMain;
        C1DockingTabPage tabOPBkk;
        C1FlexGrid grfOrder;
        
        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1PdfDocument pdfDoc;
        C1ThemeController theme1;
        Label lbDateStart, lbDateEnd;
        C1DateEdit txtDateStart, txtDateEnd;
        C1Button btnOPBKKOk;
        C1ComboBox cboPaidType;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmOPBKKClaim(BangnaControl bc)
        {
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEdit5B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 5, FontStyle.Bold);
            theme1 = new C1.Win.C1Themes.C1ThemeController();
            this.Load += FrmOPBKKClaim_Load;

            initCompoment();
            setControl();
        }
        private void initCompoment()
        {
            int gapLine = 25, gapX = 20, gapY = 20, xCol2 = 130, xCol1 = 80, xCol3 = 330, xCol4 = 640, xCol5 = 950;
            Size size = new Size();

            tcMain = new C1DockingTab();
            tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tcMain.Location = new System.Drawing.Point(0, 266);
            tcMain.Name = "tcMain";
            tcMain.Size = new System.Drawing.Size(669, 200);
            tcMain.TabIndex = 0;
            tcMain.TabsSpacing = 5;

            tabOPBkk = new C1DockingTabPage();
            tabOPBkk.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOPBkk.Size = new System.Drawing.Size(667, 175);
            tabOPBkk.TabIndex = 0;
            tabOPBkk.Text = "OPBkk Claim";
            tabOPBkk.Name = "tabStfNote";
            tcMain.Controls.Add(tabOPBkk);

            lbDateStart = new Label();
            txtDateStart = new C1DateEdit();
            lbDateEnd = new Label();
            txtDateEnd = new C1DateEdit();
            btnOPBKKOk = new C1Button();

            //gapY += gapLine;
            bc.setControlLabel(ref lbDateStart, fEdit, "วันที่เริ่มต้น :", "lbDateStart", gapX , gapY);
            size = bc.MeasureString(lbDateStart);
            bc.setControlC1DateTimeEdit(ref txtDateStart, "txtDateStart", lbDateStart.Location.X + size.Width + 5, gapY);
            size = bc.MeasureString(lbDateStart);
            bc.setControlC1DateTimeEdit(ref txtDateStart, "txtDateStart", lbDateStart.Location.X + size.Width + 5, gapY);
            bc.setControlLabel(ref lbDateEnd, fEdit, "วันที่เริ่มต้น :", "lbDateEnd", txtDateStart.Location.X + txtDateStart.Width+20, gapY);
            size = bc.MeasureString(lbDateEnd);
            bc.setControlC1DateTimeEdit(ref txtDateEnd, "txtDateEnd", lbDateEnd.Location.X + size.Width + 5, gapY);

            bc.setControlC1Button(ref btnOPBKKOk, fEdit, "OK", "btnOPBKKOk", txtDateEnd.Location.X + txtDateEnd.Width + 20, gapY);

            tabOPBkk.Controls.Add(lbDateStart);
            tabOPBkk.Controls.Add(txtDateStart);
            tabOPBkk.Controls.Add(lbDateEnd);
            tabOPBkk.Controls.Add(txtDateEnd);
            tabOPBkk.Controls.Add(btnOPBKKOk);
            this.Controls.Add(tcMain);

            theme1.SetTheme(this, bc.iniC.themeApp);
            theme1.SetTheme(tcMain, bc.iniC.themeApp);
            theme1.SetTheme(lbDateStart, bc.iniC.themeApp);
            theme1.SetTheme(txtDateStart, bc.iniC.themeApp);
            theme1.SetTheme(lbDateEnd, bc.iniC.themeApp);
            theme1.SetTheme(txtDateEnd, bc.iniC.themeApp);
            theme1.SetTheme(btnOPBKKOk, bc.iniC.themeApp);
        }
        private void setControl()
        {
            txtDateStart.Value = DateTime.Now;
            txtDateEnd.Value = DateTime.Now;
        }
        private void FrmOPBKKClaim_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            int scrH = Screen.PrimaryScreen.Bounds.Height;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
