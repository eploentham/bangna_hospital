using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Command;
using C1.Win.C1Document;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SplitContainer;
using C1.Win.FlexViewer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    /*
     * 62-11-18     0001    รูปใน scanView เรียงไม่ถูกต้อง
     */
    public partial class FrmScanView1 : Form
    {
        BangnaControl bc;

        C1FlexGrid grfVs;
        Font fEdit, fEditB;
        C1DockingTab tcDtr;
        C1DockingTabPage tabStfNote, tabOrder,  tabScan, tabLab, tabXray, tablabOut;
        C1FlexGrid grfOrder, grfScan, grfLab, grfXray;
        C1FlexViewer labOutView;

        int colVsVsDate=1, colVsVn = 2, colVsStatus=3, colVsDept = 4, colVsPreno=5, colVsAn=6, colVsAndate=7;
        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
        int colOrderId = 1, colOrderName = 2, colOrderMed = 3, colOrderQty = 4;
        int colLabDate=1,colLabName = 2, colLabNameSub=3, colLabResult = 4, colInterpret = 5, colNormal = 6, colUnit=7;
        int colXrayDate = 1, colXrayCode = 2, colXrayName = 3, colXrayResult = 4;
        int newHeight = 720;
        int mouseWheel = 0;
        int originalHeight = 0;
        ArrayList array1 = new ArrayList();
        List<listStream> lStream;
        listStream strm;
        Image resizedImage, img;
        C1PictureBox pic, picL,picR;
        //FlowLayoutPanel fpL, fpR;
        //SplitContainer sct;
        C1SplitContainer sct;
        C1SplitterPanel cspL, cspR;
        
        //VScrollBar vScroller;
        //int y = 0;
        Form frmImg;
        String dsc_id = "", hn="";
        //Timer timer1;
        Patient ptt;
        Stream streamPrint;
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        [STAThread]
        private void txtStatus(String msg)
        {
            txt.Invoke(new EventHandler(delegate
            {
                txt.Value = msg;
            }));
        }
        public FrmScanView1(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        public FrmScanView1(BangnaControl bc, String hn)
        {
            InitializeComponent();
            this.bc = bc;
            this.hn = hn;
            initConfig();
        }
        private void initConfig()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            bc.bcDB.dgsDB.setCboBsp(cboDgs, "");

            array1 = new ArrayList();
            lStream = new List<listStream>();
            strm = new listStream();
            grfOrder = new C1FlexGrid();
            ptt = new Patient();
            //timer1 = new Timer();
            //int chk = 0;
            //int.TryParse(bc.iniC.timerImgScanNew, out chk);
            //timer1.Interval = chk;
            //timer1.Enabled = true;
            //timer1.Tick += Timer1_Tick;
            //timer1.Stop();

            theme1.SetTheme(sb1, bc.iniC.themeApplication);
            theme1.SetTheme(groupBox1, bc.iniC.themeApplication);
            theme1.SetTheme(panel2, bc.iniC.themeApplication);
            theme1.SetTheme(panel3, bc.iniC.themeApplication);
            theme1.SetTheme(sC1, bc.iniC.themeApplication);
            foreach (Control con in groupBox1.Controls)
            {
                theme1.SetTheme(con, bc.iniC.themeApplication);
            }
            //foreach (Control con in grfScan.Controls)
            //{
            //    theme1.SetTheme(con, "ExpressionDark");
            //}
            initGrf();
            txtHn.Value = hn;
            ptt = bc.bcDB.pttDB.selectPatinet(txtHn.Text.Trim());
            txtName.Value = ptt.Name;
            setGrfVs();
            
            btnHn.Click += BtnHn_Click;
            btnOpen.Click += BtnOpen_Click;
            //btnRefresh.Click += BtnRefresh_Click;
            txtHn.KeyUp += TxtHn_KeyUp;
            //tcDtr.SelectedTabChanged += TcDtr_SelectedTabChanged;
            //sC1.TabIndexChanged += SC1_TabIndexChanged;
            //tcDtr.TabClick += TcDtr_TabClick;


            tcDtr = new C1DockingTab();
            tcDtr.Dock = System.Windows.Forms.DockStyle.Fill;
            tcDtr.Location = new System.Drawing.Point(0, 266);
            tcDtr.Name = "c1DockingTab1";
            tcDtr.Size = new System.Drawing.Size(669, 200);
            tcDtr.TabIndex = 0;
            tcDtr.TabsSpacing = 5;
            panel3.Controls.Add(tcDtr);
            theme1.SetTheme(tcDtr, bc.iniC.themeApplication);
            tabStfNote = new C1DockingTabPage();
            tabStfNote.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabStfNote.Size = new System.Drawing.Size(667, 175);
            tabStfNote.TabIndex = 0;
            tabStfNote.Text = "ใบยา / Staff's Note";
            tabStfNote.Name = "tabStfNote";
            tcDtr.Controls.Add(tabStfNote);
            tcDtr.TabClick += TcDtr_TabClick;

            tabOrder = new C1DockingTabPage();
            tabOrder.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOrder.Size = new System.Drawing.Size(667, 175);
            tabOrder.TabIndex = 0;
            tabOrder.Text = "ประวัติการสั่งการ";
            tabOrder.Name = "tabOrder";
            tcDtr.Controls.Add(tabOrder);

            tabScan = new C1DockingTabPage();
            tabScan.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabScan.Size = new System.Drawing.Size(667, 175);
            tabScan.TabIndex = 0;
            tabScan.Text = "เวชระเบียน Scan";
            tabScan.Name = "tabPageScan";
            
            tcDtr.Controls.Add(tabScan);

            tabLab = new C1DockingTabPage();
            tabLab.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabLab.Size = new System.Drawing.Size(667, 175);
            tabLab.TabIndex = 0;
            tabLab.Text = "ผล LAB";
            tabLab.Name = "tabPageLab";

            tcDtr.Controls.Add(tabLab);

            tabXray = new C1DockingTabPage();
            tabXray.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabXray.Size = new System.Drawing.Size(667, 175);
            tabXray.TabIndex = 0;
            tabXray.Text = "ผล x-Ray";
            tabXray.Name = "tabPageXray";

            tcDtr.Controls.Add(tabXray);

           

            grfOrder.Font = fEdit;
            grfOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOrder.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grfOrder.Cols[colOrderId].Visible = false;
            grfOrder.Rows.Count = 1;
            grfOrder.Cols.Count = 5;
            grfOrder.Cols[colOrderName].Caption = "ชื่อ";
            grfOrder.Cols[colOrderMed].Caption = "-";
            grfOrder.Cols[colOrderQty].Caption = "QTY";
            grfOrder.Cols[colOrderName].Width = 300;
            grfOrder.Cols[colOrderMed].Width = 200;
            grfOrder.Cols[colOrderQty].Width = 60;
            grfOrder.Name = "grfOrder";

            grfScan = new C1FlexGrid();
            grfScan.Font = fEdit;
            grfScan.Dock = System.Windows.Forms.DockStyle.Fill;
            grfScan.Location = new System.Drawing.Point(0, 0);
            grfScan.Rows[0].Visible = false;
            grfScan.Cols[0].Visible = false;
            grfScan.Rows.Count = 1;
            grfScan.Name = "grfScan";
            grfScan.Cols.Count = 5;
            Column colpic1 = grfScan.Cols[colPic1];
            colpic1.DataType = typeof(Image);
            Column colpic2 = grfScan.Cols[colPic2];
            colpic2.DataType = typeof(String);
            Column colpic3 = grfScan.Cols[colPic3];
            colpic3.DataType = typeof(Image);
            Column colpic4 = grfScan.Cols[colPic4];
            colpic4.DataType = typeof(String);
            grfScan.Cols[colPic1].Width = bc.grfScanHeight;
            grfScan.Cols[colPic2].Width = bc.grfScanHeight;
            grfScan.Cols[colPic3].Width = bc.grfScanHeight;
            grfScan.Cols[colPic4].Width = bc.grfScanHeight;
            grfScan.ShowCursor = true;
            grfScan.Cols[colPic2].Visible = false;
            grfScan.Cols[colPic3].Visible = true;
            grfScan.Cols[colPic4].Visible = false;
            grfScan.Cols[colPic1].AllowEditing = false;
            grfScan.Cols[colPic3].AllowEditing = false;
            grfScan.DoubleClick += Grf_DoubleClick;
            //grfScan.AutoSizeRows();
            //grfScan.AutoSizeCols();
            //tabScan.Controls.Add(grfScan);
            grfLab = new C1FlexGrid();
            grfLab.Font = fEdit;
            grfLab.Dock = System.Windows.Forms.DockStyle.Fill;
            grfLab.Location = new System.Drawing.Point(0, 0);
            //grfLab.Rows[0].Visible = false;
            //grfLab.Cols[0].Visible = false;
            grfLab.Name = "grfLab";

            grfXray = new C1FlexGrid();
            grfXray.Font = fEdit;
            grfXray.Dock = System.Windows.Forms.DockStyle.Fill;
            grfXray.Location = new System.Drawing.Point(0, 0);
            //grfLab.Rows[0].Visible = false;
            //grfLab.Cols[0].Visible = false;
            grfXray.Name = "grfXray";
            
            theme1.SetTheme(grfOrder, "Office2016Black");
            theme1.SetTheme(grfLab, "Office2016Black");
            theme1.SetTheme(grfXray, "Office2016Black");
            tabOrder.Controls.Add(grfOrder);
            tabScan.Controls.Add(grfScan);
            tabLab.Controls.Add(grfLab);
            tabXray.Controls.Add(grfXray);
            
            setPicStaffNote();
            theme1.SetTheme(tcDtr, theme1.Theme);
            //int i = 0;
            //String idOld = "";
            //if (bc.bcDB.dgssDB.lDgss.Count <= 0) bc.bcDB.dgssDB.getlBsp();
            //foreach (DocGroupSubScan dgss in bc.bcDB.dgssDB.lDgss)
            //{
            //    String dgsid = "";
            //    dgsid = bc.bcDB.dgssDB.getDgsIdDgss(dgss.doc_group_sub_name);
            //    if (!dgsid.Equals(idOld))
            //    {
            //        idOld = dgsid;
            //        String name = "";
            //        name = bc.bcDB.dgsDB.getNameDgs(dgss.doc_group_id);
            //        C1DockingTabPage tabPage = new C1DockingTabPage();
            //        tabPage.Location = new System.Drawing.Point(1, 24);
            //        tabPage.Size = new System.Drawing.Size(667, 175);

            //        tabPage.TabIndex = 0;
            //        tabPage.Text = " " + name + "  ";
            //        tabPage.Name = dgsid;
            //        tcDtr.Controls.Add(tabPage);
            //        i++;
            //        C1DockingTab tabDtr1 = new C1DockingTab();
            //        tabDtr1.Dock = System.Windows.Forms.DockStyle.Fill;
            //        tabDtr1.Location = new System.Drawing.Point(0, 266);
            //        tabDtr1.Name = "c1DockingTab1";
            //        tabDtr1.Size = new System.Drawing.Size(669, 200);
            //        tabDtr1.TabIndex = 0;
            //        tabDtr1.TabsSpacing = 5;
            //        tabPage.Controls.Add(tabDtr1);
            //        theme1.SetTheme(tabDtr1, "Office2010Red");
            //        foreach (DocGroupSubScan dgsss in bc.bcDB.dgssDB.lDgss)
            //        {
            //            if (dgsss.doc_group_id.Equals(dgss.doc_group_id))
            //            {
            //                //addDevice.MenuItems.Add(new MenuItem(dgsss.doc_group_sub_name, new EventHandler(ContextMenu_upload)));

            //                C1DockingTabPage tabPage2 = new C1DockingTabPage();
            //                tabPage2.Location = new System.Drawing.Point(1, 24);
            //                tabPage2.Size = new System.Drawing.Size(667, 175);
            //                tabPage2.TabIndex = 0;
            //                tabPage2.Text = " " + dgsss.doc_group_sub_name + "  ";
            //                tabPage2.Name = "tab" + dgsss.doc_group_sub_id;
            //                tabDtr1.Controls.Add(tabPage2);
            //                C1FlexGrid grf = new C1FlexGrid();
            //                grf.Font = fEdit;
            //                grf.Dock = System.Windows.Forms.DockStyle.Fill;
            //                grf.Location = new System.Drawing.Point(0, 0);
            //                grf.Rows[0].Visible = false;
            //                grf.Cols[0].Visible = false;
            //                grf.Rows.Count = 1;
            //                grf.Name = dgsss.doc_group_sub_id;
            //                grf.Cols.Count = 5;
            //                Column colpic1 = grf.Cols[colPic1];
            //                colpic1.DataType = typeof(Image);
            //                Column colpic2 = grf.Cols[colPic2];
            //                colpic2.DataType = typeof(String);
            //                Column colpic3 = grf.Cols[colPic3];
            //                colpic3.DataType = typeof(Image);
            //                Column colpic4 = grf.Cols[colPic4];
            //                colpic4.DataType = typeof(Image);
            //                grf.Cols[colPic1].Width = 310;
            //                grf.Cols[colPic2].Width = 310;
            //                grf.Cols[colPic3].Width = 310;
            //                grf.Cols[colPic4].Width = 310;
            //                grf.ShowCursor = true;
            //                grf.Cols[colPic2].Visible = false;
            //                grf.Cols[colPic3].Visible = true;
            //                grf.Cols[colPic4].Visible = false;
            //                grf.Cols[colPic1].AllowEditing = false;
            //                grf.Cols[colPic3].AllowEditing = false;
            //                grf.DoubleClick += Grf_DoubleClick;
            //                tabPage2.Controls.Add(grf);
            //            }
            //        }
            //    }
            //    else
            //    {

            //    }
            //}
        }

        private void TcDtr_TabClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tcDtr.SelectedTab == tabScan)
            {
                grfScan.AutoSizeCols();
                grfScan.AutoSizeRows();
            }
        }

        private void TcDtr_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(tcDtr.SelectedTab == tabScan)
            {
                grfScan.AutoSizeCols();
                grfScan.AutoSizeRows();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // ...
            if (keyData == (Keys.Escape))
            {
                //appExit();
                //if (MessageBox.Show("ต้องการออกจากโปรแกรม1", "ออกจากโปรแกรม", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                //{
                //frmmain.Show();
                Close();
                //    return true;
                //}
            }
            //else
            //{
            //    switch (keyData)
            //    {
            //        case Keys.K | Keys.Control:
            //            if (flagShowTitle)
            //                flagShowTitle = false;
            //            else
            //                flagShowTitle = true;
            //            setTitle(flagShowTitle);
            //            return true;
            //        case Keys.X | Keys.Control:
            //            //frmmain.Show();
            //            Close();
            //            return true;
            //    }
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void setPicStaffNote()
        {

            //pnL = new Panel();
            //pnL.Dock = DockStyle.Left;
            //pnL.Width = tabScan.Width / 2;
            //tabScan.Controls.Add(pnL);
            //pnR = new Panel();
            //pnR.Dock = DockStyle.Fill;
            //pnR.Width = tabScan.Width / 2;
            //tabScan.Controls.Add(pnR);
            //sct = new SplitContainer();
            sct = new C1SplitContainer();
            sct.Dock = DockStyle.Fill;
            //sct.Dock = System.Windows.Forms.DockStyle.Fill;
            sct.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            theme1.SetTheme(sct, bc.iniC.themeApplication);
            tabStfNote.Controls.Add(sct);

            //fpL = new FlowLayoutPanel();
            //fpL.Dock = DockStyle.Fill;
            //fpL.AutoScroll = true;
            //sct.Panels.Controls.Add(fpL);
            //tabScan.Controls.Add(fpL);
            //fpR = new FlowLayoutPanel();
            //fpR.Dock = DockStyle.Fill;
            //fpR.AutoScroll = true;
            //tabScan.Controls.Add(fpR);
            //sct.Panel2.Controls.Add(fpR);
            cspL = new C1SplitterPanel();
            cspL.Collapsible = true;
            //cspL.Controls.Add(this.panel1);
            cspL.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            cspL.Height = 629;
            cspL.Location = new System.Drawing.Point(0, 21);
            cspL.Name = "cspL";
            cspL.Size = new System.Drawing.Size(507, 608);
            cspL.SizeRatio = 49.855D;
            cspL.TabIndex = 0;
            cspL.Text = "Panel 1";
            cspL.Width = 514;

            cspR = new C1SplitterPanel();
            //cspR.Controls.Add(this.panel2);
            cspR.Height = 629;
            cspR.Location = new System.Drawing.Point(494, 21);
            cspR.Name = "cspR";
            cspR.Size = new System.Drawing.Size(541, 608);
            cspR.TabIndex = 1;
            cspR.Text = "Panel 2";

            sct.Panels.Add(cspL);
            sct.Panels.Add(cspR);

            picL = new C1PictureBox();
            picL = new C1PictureBox();
            picL.Dock = DockStyle.Fill;
            picL.SizeMode = PictureBoxSizeMode.StretchImage;
            //picL.SizeMode = PictureBoxSizeMode.StretchImage;

            

            picR = new C1PictureBox();
            picR = new C1PictureBox();
            picR.Dock = DockStyle.Fill;
            picR.SizeMode = PictureBoxSizeMode.StretchImage;
            //picR.SizeMode = PictureBoxSizeMode.StretchImage;
            cspL.Controls.Add(picL);
            cspR.Controls.Add(picR);
            //fpR.Controls.Add(picR);
        }
        private void Grf_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //MessageBox.Show("Row " + ((C1FlexGrid)sender).Row+"\n grf name "+ ((C1FlexGrid)sender).Name, "Col "+((C1FlexGrid)sender).Col+" id "+ ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row,colPic2].ToString());
            if (((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colPic2] == null) return;
            if (((C1FlexGrid)sender).Row < 0) return;
            String id = "";
            ((C1FlexGrid)sender).AutoSizeCols();
            ((C1FlexGrid)sender).AutoSizeRows();
            if (((C1FlexGrid)sender).Col == 1)
            {
                id = grfScan[grfScan.Row, colPic2].ToString();
            }
            else
            {
                id = grfScan[grfScan.Row, colPic4].ToString();
            }
            //id = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colPic2].ToString();
            dsc_id = id;
            MemoryStream strm = null;
            foreach(listStream lstrmm in lStream)
            {
                if (lstrmm.id.Equals(id))
                {
                    strm = lstrmm.stream;
                    break;
                }
            }
            if(strm != null)
            {
                streamPrint = strm;
                img = Image.FromStream(strm);
                frmImg = new Form();
                FlowLayoutPanel pn = new FlowLayoutPanel();
                //vScroller = new VScrollBar();
                //vScroller.Height = frmImg.Height;
                //vScroller.Width = 15;
                //vScroller.Dock = DockStyle.Right;
                frmImg.WindowState = FormWindowState.Normal;
                frmImg.StartPosition = FormStartPosition.CenterScreen;
                frmImg.Size = new Size(1024, 764);
                frmImg.AutoScroll = true;
                pn.Dock = DockStyle.Fill;
                pn.AutoScroll = true;
                pic = new C1PictureBox();
                pic.Dock = DockStyle.Fill;
                pic.SizeMode = PictureBoxSizeMode.AutoSize;
                //int newWidth = 440;
                int originalWidth = 0;
                
                originalHeight = 0;
                originalWidth = img.Width;
                originalHeight = img.Height;
                //resizedImage = img.GetThumbnailImage(newWidth, (newWidth * img.Height) / originalWidth, null, IntPtr.Zero);
                resizedImage = img.GetThumbnailImage((newHeight* img.Width) / originalHeight, newHeight, null, IntPtr.Zero);
                pic.Image = resizedImage;
                frmImg.Controls.Add(pn);
                pn.Controls.Add(pic);
                //pn.Controls.Add(vScroller);
                ContextMenu menuGw = new ContextMenu();
                menuGw.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_print));
                menuGw.MenuItems.Add("ต้องการ ลบข้อมูลนี้", new EventHandler(ContextMenu_Delete));
                mouseWheel = 0;
                pic.MouseWheel += Pic_MouseWheel;
                pic.ContextMenu = menuGw;
                //vScroller.Scroll += VScroller_Scroll;
                //pic.Paint += Pic_Paint;
                //vScroller.Hide();
                frmImg.ShowDialog(this);
            }
        }
        private void ContextMenu_print(object sender, System.EventArgs e)
        {
            setGrfScanToPrint();
        }
        private void setGrfScanToPrint()
        {
            SetDefaultPrinter(bc.iniC.printerA4);
            System.Threading.Thread.Sleep(500);

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += Pd_PrintPageA4;
            //here to select the printer attached to user PC
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = pd;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                pd.Print();//this will trigger the Print Event handeler PrintPage
            }
        }
        private void Pd_PrintPageA4(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                
                System.Drawing.Image img = Image.FromStream(streamPrint);
                
                float newWidth = img.Width * 100 / img.HorizontalResolution;
                float newHeight = img.Height * 100 / img.VerticalResolution;

                float widthFactor = newWidth / e.MarginBounds.Width;
                float heightFactor = newHeight / e.MarginBounds.Height;

                if (widthFactor > 1 | heightFactor > 1)
                {
                    if (widthFactor > heightFactor)
                    {
                        widthFactor = 1;
                        newWidth = newWidth / widthFactor;
                        newHeight = newHeight / widthFactor;
                        //newWidth = newWidth / 1.2;
                        //newHeight = newHeight / 1.2;
                    }
                    else
                    {
                        newWidth = newWidth / heightFactor;
                        newHeight = newHeight / heightFactor;
                    }
                }
                e.Graphics.DrawImage(img, 0, 0, (int)newWidth, (int)newHeight);
                //}
            }
            catch (Exception)
            {

            }
        }
        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                //if (File.Exists(this.ImagePath))
                //{
                //Load the image from the file
                //Stream streamPrint = null;
                System.Drawing.Image img = Image.FromStream(streamPrint);
                //Adjust the size of the image to the page to print the full image without loosing any part of it
                Rectangle m = e.MarginBounds;
                if ((double)img.Width / (double)img.Height > (double)m.Width / (double)m.Height) // image is wider
                {
                    m.Height = (int)((double)img.Height / (double)img.Width * (double)m.Width);
                }
                else
                {
                    m.Width = (int)((double)img.Width / (double)img.Height * (double)m.Height);
                }
                //pd.DefaultPageSettings.Landscape = m.Width > m.Height;
                //Putting image in center of page.
                
                e.Graphics.DrawString("print date "+DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), fEdit, Brushes.Black, 30, 30);
                e.Graphics.DrawString("doc scan id " + dsc_id, fEdit, Brushes.Black, 30, 50);
                e.Graphics.DrawImage(img, m);
                //}
            }
            catch (Exception)
            {

            }
        }
        private void ContextMenu_Delete(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("ต้องการ ลบข้อมูลนี้ ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                int chk = 0;
                String re = bc.bcDB.dscDB.voidDocScan(dsc_id,"");
                if(int.TryParse(re, out chk))
                {
                    frmImg.Dispose();
                    setGrfVs();
                    grfScan.Rows.Count = 0;
                    //clearGrf();
                }
            }
        }
        //private void Pic_Paint(object sender, PaintEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    //pBox = sender as PictureBox;
        //    e.Graphics.DrawImage(pic.Image, e.ClipRectangle, pic.Image.Height, y, e.ClipRectangle.Width,
        //      e.ClipRectangle.Height, GraphicsUnit.Pixel);
        //}

        //private void VScroller_Scroll(object sender, ScrollEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    //Graphics g = pic.CreateGraphics();
        //    //g.DrawImage(pic.Image, newRectangle(0, 0, pic.Height, vScroller.Value));
        //    y = (sender as VScrollBar).Value;
        //    pic.Refresh();
        //}

        private void Pic_MouseWheel(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            
            int numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            if (e.Delta < 0)
            {
                newHeight += SystemInformation.MouseWheelScrollLines*10;
                this.Text = e.Y.ToString();
            }
            else
            {
                newHeight -= SystemInformation.MouseWheelScrollLines * 10;
            }
            resizedImage = img.GetThumbnailImage((newHeight * img.Width) / originalHeight, newHeight, null, IntPtr.Zero);
            pic.Image = resizedImage;
            //if(resizedImage.Height > frmImg.Height)
            //{
            //    vScroller.Show();
            //}
            //else
            //{
            //    vScroller.Hide();
            //    //Graphics g = pictureBox1.CreateGraphics();
            //    //g.DrawImage(pictureBox1.Image, newRectangle(0, 0, pictureBox1.Height, vScroller.Value));
            //}
        }

        private void TxtHn_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                ptt = bc.bcDB.pttDB.selectPatinet(txtHn.Text.Trim());
                txtName.Value = ptt.Name;
                setGrfVs();
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            grfVs.AutoSizeCols();
            grfVs.AutoSizeRows();
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setGrfVs();
        }

        private void BtnHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmSearchHn frm = new FrmSearchHn(bc, FrmSearchHn.StatusConnection.host);
            frm.ShowDialog(this);
            txtHn.Value = bc.sPtt.Hn;
            txtName.Value = bc.sPtt.Name;
            setGrfVs();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }
        private void initGrf()
        {
            grfVs = new C1FlexGrid();
            grfVs.Font = fEdit;
            grfVs.Dock = System.Windows.Forms.DockStyle.Fill;
            grfVs.Location = new System.Drawing.Point(0, 0);

            //FilterRow fr = new FilterRow(grfExpn);

            grfVs.AfterRowColChange += GrfVs_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            panel2.Controls.Add(grfVs);

            theme1.SetTheme(grfVs, "ExpressionDark");

        }
        private void setStaffNote(String vsDate, String preno)
        {
            String file = "", dd = "", mm = "", yy = "";
            Image stffnoteL, stffnoteR;
            if (vsDate.Length > 8)
            {
                try
                {
                    String preno1 = preno;
                    int chk = 0;
                    dd = vsDate.Substring(0, 2);
                    mm = vsDate.Substring(3, 2);
                    yy = vsDate.Substring(vsDate.Length - 4);
                    int.TryParse(yy, out chk);
                    if (chk > 2500)
                        chk -= 543;
                    file = "\\\\172.25.10.5\\image\\OPD\\" + chk + "\\" + mm + "\\" + dd + "\\";
                    preno1 = "000000" + preno1;
                    preno1 = preno1.Substring(preno1.Length - 6);
                    stffnoteL = Image.FromFile(file + preno1 + "R.JPG");
                    stffnoteR = Image.FromFile(file + preno1 + "S.JPG");
                    picL.Image = stffnoteL;
                    picR.Image = stffnoteR;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error " + ex.Message, "");
                }
            }
        }
        private void GrfVs_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;

            if (txtHn.Text.Equals("")) return;
            setGrfLab(e.NewRange.r1);
            setGrfXray(e.NewRange.r1);
            setGrfScan(e.NewRange.r1);
            setTabLabOut(e.NewRange.r1);
        }
        private void setTabLabOut(int row)
        {
            //int i1 = 0;
            foreach(Control obj in tcDtr.Controls)
            {
                if(obj is C1DockingTabPage)
                {
                    if (obj.Name.Equals("tablabOutOld0")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld1")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld2")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld3")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld4")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld5")) tcDtr.Controls.Remove(obj);
                }
            }
            foreach (Control obj in tcDtr.Controls)
            {
                if (obj is C1DockingTabPage)
                {
                    if (obj.Name.Equals("tablabOutOld0")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld1")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld2")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld3")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld4")) tcDtr.Controls.Remove(obj);
                    if (obj.Name.Equals("tablabOutOld5")) tcDtr.Controls.Remove(obj);
                }
            }
            String vn = "", preno = "", vsdate = "", an = "";
            vn = grfVs[row, colVsVn] != null ? grfVs[row, colVsVn].ToString() : "";
            preno = grfVs[row, colVsPreno] != null ? grfVs[row, colVsPreno].ToString() : "";
            vsdate = grfVs[row, colVsVsDate] != null ? grfVs[row, colVsVsDate].ToString() : "";
            an = grfVs[row, colVsAn] != null ? grfVs[row, colVsAn].ToString() : "";
            vn = vn.Replace("/", ".").Replace("(", ".").Replace(")", "");
            DataTable dt = new DataTable();
            dt = bc.bcDB.labexDB.selectByVn(txtHn.Text.Trim(), vn);
            if (dt.Rows.Count > 0)
            {
                String labexid = "", yearid="";
                labexid = dt.Rows[0][bc.bcDB.labexDB.labex.Id].ToString();
                yearid = dt.Rows[0][bc.bcDB.labexDB.labex.YearId].ToString();
                for (int i = 0; i < 6; i++)
                {
                    MemoryStream stream;
                    String filename = "", filename1="";
                    filename = labexid + "_" + (i+1).ToString() + ".pdf";
                    FtpClient ftpc = new FtpClient(bc.iniC.hostFTPLabOut, bc.iniC.userFTPLabOut, bc.iniC.passFTPLabOut, bc.ftpUsePassiveLabOut);
                    stream = ftpc.download(bc.iniC.folderFTPLabOut+"//" +yearid +"//"+ filename);
                    if (stream == null) return;
                    if (stream.Length == 0) return;

                    tablabOut = new C1DockingTabPage();
                    tablabOut.Location = new System.Drawing.Point(1, 24);
                    //tabScan.Name = "c1DockingTabPage1";
                    tablabOut.Size = new System.Drawing.Size(667, 175);
                    tablabOut.TabIndex = 0;
                    tablabOut.Text = "OUT LAB";
                    tablabOut.Name = "tablabOutOld" + i.ToString();

                    tcDtr.Controls.Add(tablabOut);

                    labOutView = new C1FlexViewer();
                    labOutView.AutoScrollMargin = new System.Drawing.Size(0, 0);
                    labOutView.AutoScrollMinSize = new System.Drawing.Size(0, 0);
                    labOutView.Dock = System.Windows.Forms.DockStyle.Fill;
                    labOutView.Location = new System.Drawing.Point(0, 0);
                    labOutView.Name = "c1FlexViewer1"+i.ToString();
                    labOutView.Size = new System.Drawing.Size(1065, 790);
                    labOutView.TabIndex = 0;

                    tablabOut.Controls.Add(labOutView);

                    C1PdfDocumentSource pds = new C1PdfDocumentSource();
                    //C1.C1Pdf.C1PdfDocument _pdf = new C1.C1Pdf.C1PdfDocument();
                    //_pdf.
                    //if (!Directory.Exists("report"))
                    //{
                    //    Directory.CreateDirectory("report");
                    //}
                    //filename1 = System.IO.Directory.GetCurrentDirectory()+"\\report\\" +filename;
                    //if (File.Exists(filename1))
                    //{
                    //    File.Delete(filename1);
                    //    System.Threading.Thread.Sleep(200);
                    //}
                    //var fileStream = new FileStream(filename1, FileMode.Create, FileAccess.Write);
                    //File file = new File();
                    
                    //stream.WriteTo(fileStream);
                    //fileStream.Dispose();

                    pds.LoadFromStream(stream);
                    //pds.LoadFromFile(filename1);

                    labOutView.DocumentSource = pds;
                }
            }
        }
        private void setGrfXray(int row)
        {
            //new Thread(() =>
            //{
            DataTable dt = new DataTable();
            String vn = "", preno = "", vsdate = "", an = "";
            vn = grfVs[row, colVsVn] != null ? grfVs[row, colVsVn].ToString() : "";
            preno = grfVs[row, colVsPreno] != null ? grfVs[row, colVsPreno].ToString() : "";
            vsdate = grfVs[row, colVsVsDate] != null ? grfVs[row, colVsVsDate].ToString() : "";
            an = grfVs[row, colVsAn] != null ? grfVs[row, colVsAn].ToString() : "";
            if (vn.IndexOf("(") > 0)
            {
                vn = vn.Substring(0, vn.IndexOf("("));
            }
            if (vn.IndexOf("/") > 0)
            {
                vn = vn.Substring(0, vn.IndexOf("/"));
            }
            Application.DoEvents();
            if (an.Length > 0)
            {
                String[] an1 = an.Split('/');
                if (an1.Length > 0)
                {
                    dt = bc.bcDB.vsDB.selectResultXraybyAN(txtHn.Text, an1[0], an1[1]);
                }
            }
            else
            {
                dt = bc.bcDB.vsDB.selectLabbyVN(vsdate, vsdate, txtHn.Text, vn, preno);
            }
            grfXray.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfXray.Rows.Count = dt.Rows.Count + 1;
            grfXray.Cols.Count = 5;
            grfXray.Cols[colXrayDate].Caption = "วันที่สั่ง";
            grfXray.Cols[colXrayName].Caption = "ชื่อX-Ray";
            grfXray.Cols[colXrayCode].Caption = "Code X-Ray";
            grfXray.Cols[colXrayResult].Caption = "ผล X-Ray";

            grfXray.Cols[colXrayDate].Width = 100;
            grfXray.Cols[colXrayName].Width = 250;
            grfXray.Cols[colXrayCode].Width = 100;
            grfXray.Cols[colXrayResult].Width = 200;
            
            int i = 0;
            decimal aaa = 0;
            foreach (DataRow row1 in dt.Rows)
            {
                i++;
                grfXray[i, colXrayDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
                grfXray[i, colXrayName] = row1["MNC_XR_DSC"].ToString();
                grfXray[i, colXrayCode] = row1["MNC_XR_CD"].ToString();
                grfXray[i, colXrayResult] = row1["result"].ToString();
                
                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfXray);
            grfXray.Cols[colXrayDate].AllowEditing = false;
            grfXray.Cols[colXrayName].AllowEditing = false;
            grfXray.Cols[colXrayCode].AllowEditing = false;
            grfXray.Cols[colXrayResult].AllowEditing = false;
            //}).Start();
        }
        private void setGrfLab(int row)
        {
            //new Thread(() =>
            //{
                DataTable dt = new DataTable();
                String vn = "", preno = "", vsdate="", an="";
                vn = grfVs[row, colVsVn] != null ? grfVs[row, colVsVn].ToString() : "";
                preno = grfVs[row, colVsPreno] != null ? grfVs[row, colVsPreno].ToString() : "";
                vsdate = grfVs[row, colVsVsDate] != null ? grfVs[row, colVsVsDate].ToString() : "";
                an = grfVs[row, colVsAn] != null ? grfVs[row, colVsAn].ToString() : "";
                if (vn.IndexOf("(") > 0)
                {
                    vn = vn.Substring(0, vn.IndexOf("("));
                }
                if (vn.IndexOf("/") > 0)
                {
                    vn = vn.Substring(0, vn.IndexOf("/"));
                }
                Application.DoEvents();
                if (an.Length > 0)
                {
                    String[] an1 = an.Split('/');
                    if (an1.Length > 0)
                    {
                        dt = bc.bcDB.vsDB.selectResultLabbyAN(txtHn.Text, an1[0], an1[1]);
                    }
                }
                else
                {
                    dt = bc.bcDB.vsDB.selectLabbyVN(vsdate, vsdate, txtHn.Text, vn, preno);
                }
                grfLab.Rows.Count = 1;
                //grfLab.Cols[colOrderId].Visible = false;
                grfLab.Rows.Count = dt.Rows.Count + 1;
                grfLab.Cols.Count = 8;
                grfLab.Cols[colLabDate].Caption = "วันที่สั่ง";
                grfLab.Cols[colLabName].Caption = "ชื่อLAB";
                grfLab.Cols[colLabNameSub].Caption = "ชื่อLABย่อย";
                grfLab.Cols[colLabResult].Caption = "ผลLAB";
                grfLab.Cols[colInterpret].Caption = "แปรผล";
                grfLab.Cols[colNormal].Caption = "Normal";
                grfLab.Cols[colUnit].Caption = "Unit";
                grfLab.Cols[colLabDate].Width = 100;
                grfLab.Cols[colLabName].Width = 250;
                grfLab.Cols[colLabNameSub].Width = 100;
                grfLab.Cols[colInterpret].Width = 200;
                grfLab.Cols[colNormal].Width = 200;
                grfLab.Cols[colUnit].Width = 150;
                grfLab.Cols[colLabResult].Width = 200;
            int i = 0;
                decimal aaa = 0;
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    grfLab[i, colLabDate] = bc.datetoShow(row1["mnc_req_dat"].ToString());
                    grfLab[i, colLabName] = row1["MNC_LB_DSC"].ToString();
                    grfLab[i, colLabNameSub] = row1["mnc_res"].ToString();
                    grfLab[i, colLabResult] = row1["MNC_RES_VALUE"].ToString();
                    grfLab[i, colInterpret] = row1["MNC_STS"].ToString();
                    grfLab[i, colNormal] = row1["MNC_LB_RES"].ToString();
                    grfLab[i, colUnit] = row1["MNC_RES_UNT"].ToString();
                    //row1[0] = (i - 2);
                }
                CellNoteManager mgr = new CellNoteManager(grfLab);
                grfLab.Cols[colLabName].AllowEditing = false;
                grfLab.Cols[colInterpret].AllowEditing = false;
                grfLab.Cols[colNormal].AllowEditing = false;
            //}).Start();
        }
        private void setGrfScan(int row)
        {
            panel2.Enabled = false;

            ProgressBar pB1 = new ProgressBar();
            pB1.Location = new System.Drawing.Point(20, 16);
            pB1.Name = "pB1";
            pB1.Size = new System.Drawing.Size(862, 23);
            groupBox1.Controls.Add(pB1);
            pB1.Left = txtHn.Left;
            pB1.Show();
            txtVN.Hide();
            txtHn.Hide();
            txtName.Hide();
            label1.Hide();
            cboDgs.Hide();
            btnOpen.Hide();
            btnHn.Hide();

            txt.Hide();
            //label6.Hide();
            //txtVisitDate.Hide();
            chkIPD.Hide();
            //txtPreNo.Hide();

            //clearGrf();
            String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "";
            statusOPD = grfVs[row, colVsStatus] != null ? grfVs[row, colVsStatus].ToString() : "";
            preno = grfVs[row, colVsPreno] != null ? grfVs[row, colVsPreno].ToString() : "";
            vsDate = grfVs[row, colVsVsDate] != null ? grfVs[row, colVsVsDate].ToString() : "";
            //txtVisitDate.Value = vsDate;
            if (statusOPD.Equals("OPD"))
            {
                chkIPD.Checked = false;
                vn = grfVs[row, colVsVn] != null ? grfVs[row, colVsVn].ToString() : "";
                txtVN.Value = vn;
                label2.Text = "VN :";
            }
            else
            {
                chkIPD.Checked = true;
                an = grfVs[row, colVsAn] != null ? grfVs[row, colVsAn].ToString() : "";
                anDate = grfVs[row, colVsAndate] != null ? grfVs[row, colVsAndate].ToString() : "";
                txtVN.Value = an;
                label2.Text = "AN :";
                //txtVisitDate.Value = anDate;
            }
            setStaffNote(vsDate, preno);

            DataTable dtOrder = new DataTable();
            vn = grfVs[row, colVsVn] != null ? grfVs[row, colVsVn].ToString() : "";
            if (vn.IndexOf("(") > 0)
            {
                vn = vn.Substring(0, vn.IndexOf("("));
            }
            if (vn.IndexOf("/") > 0)
            {
                vn = vn.Substring(0, vn.IndexOf("/"));
            }
            Application.DoEvents();
            dtOrder = bc.bcDB.vsDB.selectDrug(txtHn.Text, vn, preno);
            grfOrder.Rows.Count = 1;
            if (dtOrder.Rows.Count > 0)
            {
                try
                {
                    pB1.Value = 0;
                    pB1.Minimum = 0;
                    pB1.Maximum = dtOrder.Rows.Count;
                    foreach (DataRow row1 in dtOrder.Rows)
                    {
                        Row rowg = grfOrder.Rows.Add();
                        rowg[colOrderName] = row1["MNC_PH_TN"].ToString();
                        rowg[colOrderMed] = "";
                        rowg[colOrderQty] = row1["qty"].ToString();
                        pB1.Value++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex.Message, "");
                }
            }
            GC.Collect();
            DataTable dt = new DataTable();
            dt = bc.bcDB.dscDB.selectByAn(txtHn.Text, an);
            if (dt.Rows.Count == 0)
            {
                vn = grfVs[row, colVsVn] != null ? grfVs[row, colVsVn].ToString() : "";
                dt = bc.bcDB.dscDB.selectByVn(txtHn.Text, vn, bc.datetoDB(vsDate));
            }
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_grfscan__print));
            //menuGw.MenuItems.Add("ต้องการ ลบข้อมูลนี้", new EventHandler(ContextMenu_Delete));
            grfScan.ContextMenu = menuGw;

            grfScan.Rows.Count = 0;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    int cnt = 0;
                    cnt = dt.Rows.Count / 2;

                    grfScan.Rows.Count = cnt + 1;
                    //foreach (Row row1 in grfScan.Rows)
                    //{
                    //    row1.Height = 100;
                    //}
                    for(int k=0; k < grfScan.Rows.Count; k++)
                    {
                        grfScan.Rows[k].Height = 200;
                    }
                    pB1.Value = 0;
                    pB1.Minimum = 0;
                    pB1.Maximum = dt.Rows.Count;
                    //MemoryStream stream;
                    //Image loadedImage, resizedImage;

                    FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                    Boolean findTrue = false;
                    int colcnt = 0, rowrun = -1;
                    foreach (DataRow row1 in dt.Rows)
                    {
                        if (findTrue) break;
                        colcnt++;
                        String dgssid = "", filename = "", ftphost = "", id = "", folderftp = "";
                        id = row1[bc.bcDB.dscDB.dsc.doc_scan_id].ToString();
                        dgssid = row1[bc.bcDB.dscDB.dsc.doc_group_sub_id].ToString();
                        filename = row1[bc.bcDB.dscDB.dsc.image_path].ToString();
                        ftphost = row1[bc.bcDB.dscDB.dsc.host_ftp].ToString();
                        folderftp = row1[bc.bcDB.dscDB.dsc.folder_ftp].ToString();

                        //new Thread(() =>
                        //{
                            String err = "";
                        try
                        {
                            FtpWebRequest ftpRequest = null;
                            FtpWebResponse ftpResponse = null;
                            Stream ftpStream = null;
                            int bufferSize = 2048;
                            err = "00";
                            Row rowd;
                            if ((colcnt % 2) == 0)
                            {
                                rowd = grfScan.Rows[rowrun];
                            }
                            else
                            {
                                rowrun++;
                                rowd = grfScan.Rows[rowrun];
                            }

                            MemoryStream stream;
                            Image loadedImage, resizedImage;
                            stream = new MemoryStream();
                            //stream = ftp.download(folderftp + "//" + filename);

                            //loadedImage = Image.FromFile(filename);
                            err = "01";
                            Application.DoEvents();
                            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(ftphost + "/" + folderftp + "/" + filename);
                            ftpRequest.Credentials = new NetworkCredential(bc.iniC.userFTP, bc.iniC.passFTP);
                            ftpRequest.UseBinary = true;
                            ftpRequest.UsePassive = bc.ftpUsePassive;
                            ftpRequest.KeepAlive = true;
                            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                            ftpStream = ftpResponse.GetResponseStream();
                            err = "02";
                            byte[] byteBuffer = new byte[bufferSize];
                            int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                            try
                            {
                                while (bytesRead > 0)
                                {
                                    stream.Write(byteBuffer, 0, bytesRead);
                                    bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                            err = "03";


                            loadedImage = new Bitmap(stream);
                            err = "04";
                            int originalWidth = 0;
                            originalWidth = loadedImage.Width;
                            int newWidth = 640;
                            resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
                            //
                            err = "05";
                            if ((colcnt % 2) == 0)
                            {
                                //err = "051";                      //  621118  -0001
                                //rowd[colPic1] = resizedImage;     //  621118  -0001
                                //err = "06";               //  621118  -0001
                                //rowd[colPic2] = id;           //  621118  -0001
                                //err = "07";//  621118  -0001
                                rowd[colPic3] = resizedImage;       // + 0001
                                err = "061";       // + 0001
                                rowd[colPic4] = id;       // + 0001
                                err = "071";       // + 0001
                            }
                            else
                            {
                                //err = "052 " + colPic3 + " cnt " + grfScan.Cols.Count;            //  621118  -0001
                                //rowd[colPic3] = resizedImage;         //  621118  -0001
                                //err = "061";          //  621118  -0001
                                //rowd[colPic4] = id;           //  621118  -0001
                                //err = "071";          //  621118  -0001
                                err = "051";       // + 0001
                                rowd[colPic1] = resizedImage;       // + 0001
                                err = "06";       // + 0001
                                rowd[colPic2] = id;       // + 0001
                                err = "07";       // + 0001
                            }

                            strm = new listStream();
                            strm.id = id;
                            err = "08";
                            strm.stream = stream;
                            err = "09";
                            lStream.Add(strm);

                            //grf1.AutoSizeRows();
                            //err = "10";
                            //grf1.AutoSizeCols();
                            //err = "11";
                            //loadedImage.Dispose();
                            //resizedImage.Dispose();
                            //stream.Dispose();
                            Application.DoEvents();
                            err = "12";
                            //findTrue = true;
                            //break;
                            //GC.Collect();
                        }
                        catch (Exception ex)
                        {
                            String aaa = ex.Message + " " + err;
                        }
                    //}).Start();

                    pB1.Value++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex.Message, "");
                }

            }
            pB1.Dispose();
            txtVN.Show();
            txtHn.Show();
            txtName.Show();
            label1.Show();
            cboDgs.Show();
            btnOpen.Show();
            btnHn.Show();
            txt.Show();
            //label6.Show();
            chkIPD.Show();
            //grf1.AutoSizeRows();
            //grf1.AutoSizeRows();
            panel2.Enabled = true;
        }
        private void ContextMenu_grfscan__print(object sender, System.EventArgs e)
        {
            String id = "";
            if (grfScan.Col <= 0) return;
            if (grfScan.Row < 0) return;
            if(grfScan.Col == 1)
            {
                id = grfScan[grfScan.Row, colPic2].ToString();
            }
            else
            {
                id = grfScan[grfScan.Row, colPic4].ToString();
            }
            dsc_id = id;
            MemoryStream strm = null;
            foreach (listStream lstrmm in lStream)
            {
                if (lstrmm.id.Equals(id))
                {
                    strm = lstrmm.stream;
                    streamPrint = lstrmm.stream;
                    break;
                }
            }
            setGrfScanToPrint();
            //MessageBox.Show("row "+ grfScan.Row+"\n"+"col "+grfScan.Col+"\n ", "");

        }
        private void clearGrf()
        {
            foreach (Control con in panel3.Controls)
            {
                if (con is C1DockingTab)
                {
                    foreach (Control cond in con.Controls)
                    {
                        if (cond is C1DockingTabPage)
                        {
                            foreach (Control cong in cond.Controls)
                            {
                                if (cong is C1DockingTab)
                                {
                                    foreach (Control congd in cong.Controls)
                                    {
                                        if (congd is C1DockingTabPage)
                                        {
                                            foreach (Control congd1 in congd.Controls)
                                            {
                                                if (congd1 is C1FlexGrid)
                                                {
                                                    C1FlexGrid grf1;
                                                    grf1 = (C1FlexGrid)congd1;
                                                    //grf1.Clear();
                                                    grf1.Rows.Count = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void setGrfVs()
        {
            grfVs.Clear();
            grfVs.Rows.Count = 1;
            grfVs.Cols.Count = 8;
            
            //C1TextBox text = new C1TextBox();
            //grfVs.Cols[colVsVsDate].Editor = text;
            //grfVs.Cols[colVsVn].Editor = text;
            //grfVs.Cols[colVsDept].Editor = text;
            //grfVs.Cols[colVsPreno].Editor = text;

            grfVs.Cols[colVsVsDate].Width = 100;
            grfVs.Cols[colVsVn].Width = 80;
            grfVs.Cols[colVsDept].Width = 240;
            grfVs.Cols[colVsPreno].Width = 100;
            grfVs.Cols[colVsStatus].Width = 60;
            grfVs.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfVs.Cols[colVsVsDate].Caption = "Visit Date";
            grfVs.Cols[colVsVn].Caption = "VN";
            grfVs.Cols[colVsDept].Caption = "แผนก";
            grfVs.Cols[colVsPreno].Caption = "";
            grfVs.Cols[colVsPreno].Visible = false;
            grfVs.Cols[colVsVn].Visible = true;
            grfVs.Cols[colVsAn].Visible = true;
            grfVs.Cols[colVsAndate].Visible = false;
            grfVs.Rows[0].Visible = false;
            grfVs.Cols[0].Visible = false;
            grfVs.Cols[colVsVsDate].AllowEditing = false;
            grfVs.Cols[colVsVn].AllowEditing = false;
            grfVs.Cols[colVsDept].AllowEditing = false;
            grfVs.Cols[colVsPreno].AllowEditing = false;

            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dt = bc.bcDB.vsDB.selectVisitByHn3(txtHn.Text);
            int i = 1, j = 1, row = grfVs.Rows.Count;
            //txtVN.Value = dt.Rows.Count;
            //txtName.Value = "";
            //txt.Value = "";
            foreach (DataRow row1 in dt.Rows)
            {
                Row rowa = grfVs.Rows.Add();
                String status = "", vn = "";
                
                status = row1["MNC_PAT_FLAG"] != null ? row1["MNC_PAT_FLAG"].ToString().Equals("O") ? "OPD" : "IPD" : "-";
                vn = row1["MNC_VN_NO"].ToString() + "/" + row1["MNC_VN_SEQ"].ToString() + "(" + row1["MNC_VN_SUM"].ToString() + ")" ;
                rowa[colVsVsDate] = bc.datetoShow(row1["mnc_date"]);
                rowa[colVsVn] = vn;
                rowa[colVsStatus] = status;
                rowa[colVsPreno] = row1["mnc_pre_no"].ToString();
                rowa[colVsDept] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colVsAn] = row1["mnc_an_no"].ToString()+"/"+ row1["mnc_an_yr"].ToString();
                rowa[colVsAndate] = bc.datetoShow(row1["mnc_ad_date"].ToString());
            }
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("&ยกเลิก รูปภาพนี้", new EventHandler(ContextMenu_Void));
            //menuGw.MenuItems.Add("&Update ข้อมูล", new EventHandler(ContextMenu_Update));
            //foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            //{
            //    menuGw.MenuItems.Add("&เลือกประเภทเอกสาร และUpload Image [" + dgs.doc_group_name + "]", new EventHandler(ContextMenu_upload));
            //}
            //grfVs.ContextMenu = menuGw;
            //grfVs.Cols[colVsVsDate].Visible = false;
            //grfVs.Cols[colImagePath].Visible = false;
            //row1[colVSE2] = row[ic.ivfDB.pApmDB.pApm.e2].ToString().Equals("1") ? imgCorr : imgTran;
            //grfVs.AutoSizeCols();
            //grfVs.AutoSizeRows();
            //grfVs.Refresh();
            //theme1.SetTheme(grfVs, "ExpressionDark");
        }
        private void ContextMenu_Void(object sender, System.EventArgs e)
        {
            
        }
        class listStream
        {
            public String id = "";
            public MemoryStream stream;
        }
        private void FrmScanView1_Load(object sender, EventArgs e)
        {
            int widthL = 0, widthR = 0;
            widthL = tabStfNote.Width / 2;
            widthR = widthL + 5;
            //sct.SplitterDistance = fpL.Width;
            cspL.Width = widthL;
            sC1.HeaderHeight = 0;
            scVs.Width = 240;
            //sct.Panel1.Width = fpL.Width;
            //sct.Panel2.Width = fpR.Width;
            sct.HeaderHeight = 0;
        }
    }
}
