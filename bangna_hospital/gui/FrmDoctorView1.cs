using bangna_hospital.control;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmDoctorView1:Form
    {
        BangnaControl bc;
        MainMenu menu;
        Login login;

        private Point _imageLocation = new Point(13, 5);
        private Point _imgHitArea = new Point(13, 2);
        Image CloseImage;
        C1FlexGrid grfQue, grfApm;
        private C1.Win.C1Ribbon.C1StatusBar sb1;
        private C1.Win.C1Themes.C1ThemeController theme1;
        C1DockingTab tC1;
        Label lbDtrName, lbTxtPttHn, lbTxtDate;
        C1TextBox txtPttHn;
        C1Button btnHnSearch;
        C1DateEdit txtDate;

        Panel panel1, pnHead, pnBotton;

        int colQueId = 1, colQueVnShow = 2, colQueHn = 3, colQuePttName = 4, colQueVsDate = 5, colQueVsTime = 6, colQueSex = 7, colQueAge = 8, colQuePaid = 9, colQueSymptom = 10, colQueHeight = 11, coolQueBw = 12, colQueBp = 13, colQuePulse = 14, colQyeTemp = 15, colQuePreNo = 16, colQueDsc = 17;
        int colApmId = 1, colApmHn = 2, colApmPttName = 3, colApmDate = 4, colApmTime = 5, colApmSex = 6, colApmAge = 7, colApmDsc = 8, colApmRemark = 9, colApmDept = 10;

        Boolean flagExit = false;
        Font fEdit, fEditB, fEditBig;
        System.Windows.Forms.Timer timer1;

        public FrmDoctorView1(BangnaControl bc, FrmSplash splash)
        {
            this.bc = bc;
            login = new Login(bc, splash);
            login.ShowDialog(this);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                bc.bcDB = new objdb.BangnaHospitalDB(bc.conn);
                bc.getInit();
            }).Start();
            splash.Dispose();
            if (login.LogonSuccessful.Equals("1"))
            {
                initConfig();
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    /* run your code here */

                }).Start();
            }
            else
            {
                Application.Exit();
            }
        }
        private void initConfig()
        {
            initCompoment();
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize+10, FontStyle.Regular);

            timer1 = new System.Windows.Forms.Timer();
            timer1.Enabled = true;
            timer1.Interval = bc.timerCheckLabOut * 1000;
            timer1.Tick += Timer1_Tick;

            this.Load += FrmDoctorView1_Load;
        }
        private void initCompoment()
        {
            int gapLine = 20, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;

            this.sb1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnHead = new System.Windows.Forms.Panel();
            this.pnBotton = new System.Windows.Forms.Panel();
            this.theme1 = new C1.Win.C1Themes.C1ThemeController();
            tC1 = new C1.Win.C1Command.C1DockingTab();

            panel1.SuspendLayout();
            pnHead.SuspendLayout();
            pnBotton.SuspendLayout();
            tC1.SuspendLayout();

            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.TabIndex = 0;
            this.sb1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.sb1.Location = new System.Drawing.Point(0, 620);
            this.sb1.Name = "sb1";
            this.sb1.Size = new System.Drawing.Size(956, 22);
            this.sb1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Custom;
            pnHead.Size = new System.Drawing.Size(scrW, 50);

            pnHead.SuspendLayout();
            pnBotton.SuspendLayout();
            pnHead.Dock = DockStyle.Top;
            pnBotton.Dock = DockStyle.Fill;

            tC1.Dock = System.Windows.Forms.DockStyle.Fill;
            tC1.HotTrack = true;
            tC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tC1.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            tC1.TabsShowFocusCues = true;
            tC1.Alignment = TabAlignment.Bottom;
            tC1.SelectedTabBold = true;
            tC1.Name = "tC1";

            setControl();

            this.Controls.Add(this.sb1);
            this.Controls.Add(panel1);
            panel1.Controls.Add(pnHead);
            panel1.Controls.Add(pnBotton);
            pnBotton.Controls.Add(tC1);
            pnHead.Controls.Add(lbDtrName);
            pnHead.Controls.Add(txtPttHn);

            panel1.ResumeLayout(false);
            pnHead.ResumeLayout(false);
            pnBotton.ResumeLayout(false);
            tC1.ResumeLayout(false);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void setControl()
        {
            int gapLine = 20, gapX = 20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            lbDtrName = new Label();
            lbDtrName.Text = "...";
            lbDtrName.Font = fEdit;
            lbDtrName.Location = new System.Drawing.Point(gapX, 10);
            lbDtrName.AutoSize = true;
            lbDtrName.Name = "lbDtrName";

            lbTxtPttHn = new Label();
            lbTxtPttHn.Text = "HN :";
            lbTxtPttHn.Font = fEditBig;
            size = bc.MeasureString(lbTxtPttHn);
            lbTxtPttHn.Location = new System.Drawing.Point(((scrW / 2) - size.Width), 10);
            lbTxtPttHn.AutoSize = true;
            lbTxtPttHn.Name = "lbTxtPttHn";

            txtPttHn = new C1TextBox();
            txtPttHn.Font = fEditBig;
            txtPttHn.Location = new System.Drawing.Point(lbTxtPttHn.Location.X + size.Width + 5, lbTxtPttHn.Location.Y);
            txtPttHn.Size = new Size(10, 10);

            btnHnSearch = new C1Button();
            btnHnSearch.Name = "btnHnSearch";
            btnHnSearch.Text = "...";
            btnHnSearch.Font = fEdit;
            //size = bc.MeasureString(btnHnSearch);
            btnHnSearch.Location = new System.Drawing.Point(txtPttHn.Location.X + txtPttHn .Width + 5, lbTxtPttHn.Location.Y);
            btnHnSearch.Size = new Size(30, lbTxtPttHn.Height);
            btnHnSearch.Font = fEdit;

            lbTxtDate = new Label();
            lbTxtDate.Text = "Date :";
            lbTxtDate.Font = fEdit;
            size = bc.MeasureString(lbTxtDate);
            lbTxtDate.Location = new System.Drawing.Point((scrW + txtDate.Width - size.Width), lbTxtPttHn.Location.Y);
            lbTxtDate.AutoSize = true;
            lbTxtDate.Name = "lbTxtPttHn";

            txtDate = new C1DateEdit();
            txtDate.AllowSpinLoop = false;
            txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            txtDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDate.CurrentTimeZone = false;
            txtDate.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            txtDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtDate.EditFormat.CustomFormat = "dd/MM/yyyy";
            txtDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtDate.GMTOffset = System.TimeSpan.Parse("00:00:00");
            txtDate.ImagePadding = new System.Windows.Forms.Padding(0);
            //size = bc.MeasureString(lbtxtDateStart);
            txtDate.Location = new System.Drawing.Point(scrW - txtDate.Width + 20, lbTxtPttHn.Location.Y);
            txtDate.Name = "txtDateStart";
            txtDate.Size = new System.Drawing.Size(111, 20);
            txtDate.TabIndex = 12;
            txtDate.Tag = null;
            //theme1.SetTheme(this.txtDate, "(default)");
            txtDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void FrmDoctorView1_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
    }
}
