using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
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

        C1FlexGrid grfIPD, grfOPD;
        Font fEdit, fEditB, fEdit3B;
        C1DockingTab tcDtr, tcVs, tcHnLabOut, tcMac;
        C1DockingTabPage tabStfNote, tabOrder, tabScan, tabLab, tabXray, tablabOut, tabOPD, tabIPD, tabPrn, tabHn, tabHnLabOut;
        C1FlexGrid grfOrder, grfScan, grfLab, grfXray, grfPrn, grfHn;
        C1FlexViewer labOutView;
        List<C1DockingTabPage> tabHnLabOutR;

        int colVsVsDate = 1, colVsVn = 2, colVsStatus = 3, colVsDept = 4, colVsPreno = 5, colVsAn = 6, colVsAndate = 7;
        int colIPDDate = 1, colIPDAnShow = 2, colIPDStatus = 3, colIPDDept = 4, colIPDPreno = 5, colIPDVn = 6, colIPDAndate = 7, colIPDAnYr = 8, colIPDAn = 9;
        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
        int colOrderId = 1, colOrderName = 2, colOrderMed = 3, colOrderQty = 4;
        int colLabDate = 1, colLabName = 2, colLabNameSub = 3, colLabResult = 4, colInterpret = 5, colNormal = 6, colUnit = 7;
        int colXrayDate = 1, colXrayCode = 2, colXrayName = 3, colXrayResult = 4;
        int colLabOutDateReq = 1, colLabOutHN = 2, colLabOutFullName = 3, colLabOutVN = 4, colLabOutDateReceive = 5, colLabOutReqNo = 6, colLabOutId = 7;
        int colDrugAllCode = 1, colDrugAllName = 2, colDrugAllDsc = 3, colDrugAllAlg=4;
        int newHeight = 720;
        int mouseWheel = 0;
        int originalHeight = 0;
        ArrayList array1 = new ArrayList();
        List<listStream> lStream;
        listStream strm;
        Image resizedImage, img;
        C1PictureBox pic, picL, picR;
        //FlowLayoutPanel fpL, fpR;
        //SplitContainer sct;
        C1SplitContainer sct;
        C1SplitterPanel cspL, cspR;

        //VScrollBar vScroller;
        //int y = 0;
        Form frmImg;
        String dsc_id = "", hn = "", flagShowBtnSearch="";
        //Timer timer1;
        Patient ptt;
        Stream streamPrint, streamPrintL, streamPrintR, streamDownload;
        Form frmFlash;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        //[STAThread]
        //private void txtStatus(String msg)
        //{
        //    txt.Invoke(new EventHandler(delegate
        //    {
        //        txt.Value = msg;
        //    }));
        //}
        public FrmScanView1(BangnaControl bc,String flagShowBtnSearch)
        {
            InitializeComponent();
            this.bc = bc;
            this.flagShowBtnSearch = flagShowBtnSearch;
            initConfig();
        }
        public FrmScanView1(BangnaControl bc, String hn, String flagShowBtnSearch)
        {
            InitializeComponent();
            this.bc = bc;
            this.hn = hn;
            this.flagShowBtnSearch = flagShowBtnSearch;
            initConfig();
        }
        private void initConfig()
        {
            //this.FormBorderStyle = FormBorderStyle.None;

            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit3B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize+3, FontStyle.Bold);
            //bc.bcDB.dgsDB.setCboBsp(cboDgs, "");

            array1 = new ArrayList();
            lStream = new List<listStream>();
            strm = new listStream();
            tabHnLabOutR = new List<C1DockingTabPage>();

            ptt = new Patient();
            //timer1 = new Timer();
            //int chk = 0;
            //int.TryParse(bc.iniC.timerImgScanNew, out chk);
            //timer1.Interval = chk;
            //timer1.Enabled = true;
            //timer1.Tick += Timer1_Tick;
            //timer1.Stop();
            txtName.Font = fEdit3B;
            lbDrugAllergy.Font = fEdit3B;

            theme1.SetTheme(sb1, bc.iniC.themeApplication);
            theme1.SetTheme(gbPtt, bc.iniC.themeApplication);
            theme1.SetTheme(panel2, bc.iniC.themeApplication);
            theme1.SetTheme(panel3, bc.iniC.themeApplication);
            theme1.SetTheme(sC1, bc.iniC.themeApplication);
            foreach (Control con in gbPtt.Controls)
            {
                if (con is C1PictureBox) continue;
                //if (con.Name.Equals("lbAge")) continue;
                theme1.SetTheme(con, bc.iniC.themeApplication);
            }
            //theme1.SetTheme(lbAge, bc.iniC.themeApplication);
            //foreach (Control con in grfScan.Controls)
            //{
            //    theme1.SetTheme(con, "ExpressionDark");
            //}

            txtHn.Value = hn;
            ptt = bc.bcDB.pttDB.selectPatinet(txtHn.Text.Trim());
            txtName.Value = ptt.Name;
            String allergy = "";
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectDrugAllergy(txtHn.Text.Trim());
            foreach (DataRow row in dt.Rows)
            {
                allergy += row["MNC_ph_tn"].ToString() + " " + row["MNC_ph_memo"].ToString() + ", ";
            }
            lbDrugAllergy.Text = "";
            if (allergy.Length > 0)
            {
                lbDrugAllergy.Text = "แพ้ยา " + allergy;
            }
            else
            {
                lbDrugAllergy.Text = "ไม่มีข้อมูล การแพ้ยา ";
            }
            lbAge.Text = "อายุ "+ptt.AgeStringShort();
            btnHn.Click += BtnHn_Click;
            //btnRefresh.Click += BtnRefresh_Click;
            txtHn.KeyUp += TxtHn_KeyUp;
            //picExit.Click += PicExit_Click;
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
            tcDtr.SelectedTabChanged += TcDtr_SelectedTabChanged1;
            panel3.Controls.Add(tcDtr);
            theme1.SetTheme(tcDtr, bc.iniC.themeApplication);

            initTabVS();
            initGrfOPD();
            initGrfIPD();
            //setGrfVsIPD();

            initTabDtr();
            initGrf();

            setPicStaffNote();
            setControlHN();
            theme1.SetTheme(tcDtr, theme1.Theme);
            
            setTabMachineResult();
        }

        private void TcDtr_SelectedTabChanged1(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if(tcDtr.SelectedTab == tabOrder)
            {
                //setGrOrder();
            }
        }

        private void PicExit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Close();
        }

        private void initTabDtr()
        {
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

            tabPrn = new C1DockingTabPage();
            tabPrn.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabPrn.Size = new System.Drawing.Size(667, 175);
            tabPrn.TabIndex = 0;
            tabPrn.Text = "Print";
            tabPrn.Name = "tabPrn";
            tcDtr.Controls.Add(tabPrn);

            tabHn = new C1DockingTabPage();
            tabHn.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabHn.Size = new System.Drawing.Size(667, 175);
            tabHn.TabIndex = 0;
            //tabHn.Text = "Hn เวชระเบียน";
            tabHn.Text = "Machine Result";
            tabHn.Name = "tabHn";
            tcDtr.Controls.Add(tabHn);

            tabHnLabOut = new C1DockingTabPage();
            tabHnLabOut.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabHnLabOut.Size = new System.Drawing.Size(667, 175);
            tabHnLabOut.TabIndex = 0;
            tabHnLabOut.Text = "Hn Out Lab";
            tabHnLabOut.Name = "tabHnLabOut";
            tcDtr.Controls.Add(tabHnLabOut);

            //Panel pntabHnLabOut = new Panel();
            //pntabHnLabOut.Dock = DockStyle.Fill;
            //tabHnLabOut.Controls.Add(pntabHnLabOut);

            tcHnLabOut = new C1DockingTab();
            tcHnLabOut.Dock = System.Windows.Forms.DockStyle.Fill;
            tcHnLabOut.Location = new System.Drawing.Point(0, 266);
            tcHnLabOut.Name = "tcHnLabOut";
            tcHnLabOut.Size = new System.Drawing.Size(669, 200);
            tcHnLabOut.TabIndex = 0;
            tcHnLabOut.TabsSpacing = 5;
            tcHnLabOut.DoubleClick += TcHnLabOut_DoubleClick;
            tabHnLabOut.Controls.Add(tcHnLabOut);
            theme1.SetTheme(tcHnLabOut, bc.iniC.themeApplication);

            tcMac = new C1DockingTab();
            tcMac.Dock = System.Windows.Forms.DockStyle.Fill;
            tcMac.Location = new System.Drawing.Point(0, 266);
            tcMac.Name = "tcMac";
            tcMac.Size = new System.Drawing.Size(669, 200);
            tcMac.TabIndex = 0;
            tcMac.TabsSpacing = 5;
            tcMac.DoubleClick += TcMac_DoubleClick;
            tabHn.Controls.Add(tcMac);
            theme1.SetTheme(tcMac, bc.iniC.themeApplication);
        }

        private void TcMac_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void TcHnLabOut_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //if (tcHnLabOut.SelectedTab == tabImage)
            //{

            //}
            String filename = "";
            //((C1DockingTab)sender).SelectedTab.Name
            filename = ((C1DockingTab)sender).SelectedTab.Name;
            String[] txt1 = filename.Split('_');
            if (txt1.Length > 1)
            {
                filename = "report\\" + txt1[1] + ".pdf";
                if (File.Exists(filename))
                {
                    //bool isExists = System.IO.File.Exists(filename);
                    //if (isExists)
                    System.Diagnostics.Process.Start(filename);
                }
                else
                {
                    MessageBox.Show("ไม่พบ File Out Lab", "");
                }
            }
            
        }

        private void initTabVS()
        {
            tcVs = new C1DockingTab();
            tcVs.Dock = System.Windows.Forms.DockStyle.Fill;
            tcVs.Location = new System.Drawing.Point(0, 266);
            tcVs.Name = "c1DockingTab1";
            tcVs.Size = new System.Drawing.Size(669, 200);
            tcVs.TabIndex = 0;
            tcVs.TabsSpacing = 5;
            panel2.Controls.Add(tcVs);
            theme1.SetTheme(tcVs, bc.iniC.themeApplication);

            tabOPD = new C1DockingTabPage();
            tabOPD.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOPD.Size = new System.Drawing.Size(667, 175);
            tabOPD.TabIndex = 0;
            tabOPD.Text = "OPD";
            tabOPD.Name = "tabOPD";
            tcVs.Controls.Add(tabOPD);

            tabIPD = new C1DockingTabPage();
            tabIPD.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabIPD.Size = new System.Drawing.Size(667, 175);
            tabIPD.TabIndex = 0;
            tabIPD.Text = "IPD";
            tabIPD.Name = "tabIPD";
            tcVs.Controls.Add(tabIPD);

        }
        private void initGrf()
        {
            grfOrder = new C1FlexGrid();
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
            grfScan.Cols[colPic1].Width = bc.grfScanWidth;
            grfScan.Cols[colPic2].Width = bc.grfScanWidth;
            grfScan.Cols[colPic3].Width = bc.grfScanWidth;
            grfScan.Cols[colPic4].Width = bc.grfScanWidth;
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
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ Print ", new EventHandler(ContextMenu_print_lab));
            grfLab.ContextMenu = menuGw;
            grfLab.Rows.Count = 1;

            grfXray = new C1FlexGrid();
            grfXray.Font = fEdit;
            grfXray.Dock = System.Windows.Forms.DockStyle.Fill;
            grfXray.Location = new System.Drawing.Point(0, 0);
            //grfLab.Rows[0].Visible = false;
            //grfLab.Cols[0].Visible = false;
            grfXray.Name = "grfXray";
            grfXray.Rows.Count = 1;

            //theme1.SetTheme(grfOrder, "Office2016Black");
            theme1.SetTheme(grfOrder, bc.iniC.themeApp);
            theme1.SetTheme(grfLab, bc.iniC.themeApp);
            theme1.SetTheme(grfXray, bc.iniC.themeApp);
            //theme1.SetTheme(grfLab, "Office2016Black");
            //theme1.SetTheme(grfXray, "Office2016Black");
            tabOrder.Controls.Add(grfOrder);
            tabScan.Controls.Add(grfScan);
            tabLab.Controls.Add(grfLab);
            tabXray.Controls.Add(grfXray);

            initGrfPrn();
            //initGrfHn();
        }
        private void TcDtr_TabClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tcDtr.SelectedTab == tabScan)
            {
                grfScan.AutoSizeCols();
                grfScan.AutoSizeRows();
            }
            else if (tcDtr.SelectedTab == tabOrder)
            {
                if (grfIPD.Row == null) return;

                String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "", anyr = "", vn1 = "";
                DataTable dtOrder = new DataTable();
                if (chkIPD.Checked)
                {
                    statusOPD = grfIPD[grfIPD.Row, colIPDStatus] != null ? grfIPD[grfIPD.Row, colIPDStatus].ToString() : "";
                    preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
                    vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";

                    chkIPD.Checked = true;
                    an = grfIPD[grfIPD.Row, colIPDAn] != null ? grfIPD[grfIPD.Row, colIPDAn].ToString() : "";
                    anDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                    anyr = grfIPD[grfIPD.Row, colIPDAnYr] != null ? grfIPD[grfIPD.Row, colIPDAnYr].ToString() : "";
                    //txtVN.Value = an;
                    label2.Text = "AN :";
                    dtOrder = bc.bcDB.vsDB.selectDrugIPD(txtHn.Text, an, anyr);
                }
                else
                {
                    statusOPD = grfOPD[grfOPD.Row, colVsStatus] != null ? grfOPD[grfOPD.Row, colVsStatus].ToString() : "";
                    preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                    vsDate = grfOPD[grfOPD.Row, colVsVsDate] != null ? grfOPD[grfOPD.Row, colVsVsDate].ToString() : "";
                    //vsDate = bc.datetoDB(vsDate);

                    chkIPD.Checked = false;
                    vn = grfOPD[grfOPD.Row, colVsVn] != null ? grfOPD[grfOPD.Row, colVsVn].ToString() : "";
                    txtVN.Value = vn;
                    label2.Text = "VN :";
                    if (vn.IndexOf("(") > 0)
                    {
                        vn1 = vn.Substring(0, vn.IndexOf("("));
                    }
                    if (vn.IndexOf("/") > 0)
                    {
                        vn1 = vn.Substring(0, vn.IndexOf("/"));
                    }
                    dtOrder = bc.bcDB.vsDB.selectDrugOPD(txtHn.Text, vn1, preno);
                }
                setGrfOrder(dtOrder);
            }
            else if (tcDtr.SelectedTab == tabPrn)
            {

            }
        }

        private void TcDtr_SelectedTabChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tcDtr.SelectedTab == tabScan)
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
            foreach (listStream lstrmm in lStream)
            {
                if (lstrmm.id.Equals(id))
                {
                    strm = lstrmm.stream;
                    break;
                }
            }
            if (strm != null)
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
                resizedImage = img.GetThumbnailImage((newHeight * img.Width) / originalHeight, newHeight, null, IntPtr.Zero);
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
            if (bc.iniC.statusShowPrintDialog.Equals("1"))
            {
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.Document = pd;
                DialogResult result = printDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pd.Print();//this will trigger the Print Event handeler PrintPage
                }
            }
            else
            {
                pd.Print();
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
            catch (Exception ex)
            {
                new LogWriter("e", "FrmScanView1 Pd_PrintPageA4 error " + ex.Message);
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

                e.Graphics.DrawString("print date " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), fEdit, Brushes.Black, 30, 30);
                e.Graphics.DrawString("doc scan id " + dsc_id, fEdit, Brushes.Black, 30, 50);
                e.Graphics.DrawImage(img, m);
                //}
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmScanView1 Pd_PrintPage error " + ex.Message);
            }
        }
        private void ContextMenu_Delete(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("ต้องการ ลบข้อมูลนี้ ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                int chk = 0;
                String re = bc.bcDB.dscDB.voidDocScan(dsc_id, "");
                if (int.TryParse(re, out chk))
                {
                    frmImg.Dispose();
                    setGrfVsIPD();
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
                newHeight += SystemInformation.MouseWheelScrollLines * 10;
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
            if (e.KeyCode == Keys.Enter)
            {
                setControlHN();
            }
        }
        private void setControlHN()
        {
            ptt = bc.bcDB.pttDB.selectPatinet(txtHn.Text.Trim());
            txtName.Value = ptt.Name;
            //lbAge.Text = "อายุ " + ptt.AgeStringShort();
            String allergy = "";
            DataTable dt = new DataTable();
            dt = bc.bcDB.vsDB.selectDrugAllergy(txtHn.Text.Trim());
            foreach (DataRow row in dt.Rows)
            {
                allergy += row["MNC_ph_tn"].ToString() + " " + row["MNC_ph_memo"].ToString() + ", ";
            }
            lbDrugAllergy.Text = "";
            if (allergy.Length > 0)
            {
                lbDrugAllergy.Text = "แพ้ยา " + allergy;
            }
            else
            {
                lbDrugAllergy.Text = "ไม่มีข้อมูล การแพ้ยา ";
            }
            lbAge.Text = "อายุ " + ptt.AgeStringShort();

            picL.Image = null;
            picR.Image = null;
            setGrfVsIPD();
            setGrfVsOPD();
            setTabHnLabOut();
            setGrHn();
            grfOPD.Focus();
            if (grfOPD.Rows.Count > 1)
            {
                grfOPD.Row = 1;
                grfOPD.Col = 1;
                setGrfScan(grfOPD.Row, "OPD");
            }
        }
        private void setTabHnLabOut()
        {
            ProgressBar pB1 = new ProgressBar();
            pB1.Location = new System.Drawing.Point(20, 16);
            pB1.Name = "pB1";
            pB1.Size = new System.Drawing.Size(862, 23);
            gbPtt.Controls.Add(pB1);
            pB1.Left = txtHn.Left;
            pB1.Show();
            setHeaderDisable();

            if (txtHn.Text.Trim().Length <= 0)
            {
                setHeaderEnable(pB1);
                return;
            }
            tcHnLabOut.TabPages.Clear();
            DataTable dtlaboutOld = new DataTable();
            DataTable dtlaboutdsc = new DataTable();
            dtlaboutOld = bc.bcDB.labexDB.selectByHn(txtHn.Text.Trim());
            dtlaboutdsc = bc.bcDB.dscDB.selectLabOutByDateReq("","",txtHn.Text.Trim(), "daterequest");
            if (dtlaboutOld.Rows.Count > 0)
            {
                pB1.Maximum = dtlaboutOld.Rows.Count;
                //new LogWriter("w", "setTabHnLabOut");
                tcHnLabOut.TabPages.Clear();
                int k = 0;
                foreach(DataRow row in dtlaboutOld.Rows)
                {
                    try
                    {
                        k++;
                        pB1.Value = k;
                        String vn = "", preno = "", vsdate = "", an = "", labexid = "", yearid = "", filename = "", filename1 = "", datetick = "";
                        datetick = DateTime.Now.Ticks.ToString();
                        labexid = dtlaboutOld.Rows[0][bc.bcDB.labexDB.labex.Id].ToString();
                        filename = labexid + "_" + k.ToString() + ".pdf";
                        yearid = dtlaboutOld.Rows[0][bc.bcDB.labexDB.labex.YearId].ToString();
                        vn = dtlaboutOld.Rows[0][bc.bcDB.labexDB.labex.Vn].ToString();
                        vn = vn.Replace("/", ".").Replace("(", ".").Replace(")", "");

                        
                        //for (int i = 0; i < 6; i++)
                        //{
                        MemoryStream stream;

                        if (!Directory.Exists("report"))
                        {
                            Directory.CreateDirectory("report");
                        }

                        FtpClient ftpc = new FtpClient(bc.iniC.hostFTPLabOut, bc.iniC.userFTPLabOut, bc.iniC.passFTPLabOut, bc.ftpUsePassiveLabOut);
                        stream = ftpc.download(bc.iniC.folderFTPLabOut + "//" + yearid + "//" + filename);
                        if (stream == null)
                        {
                            setHeaderEnable(pB1);
                            continue;
                        }
                        if (stream.Length == 0)
                        {
                            setHeaderEnable(pB1);
                            continue;
                        }
                        C1DockingTabPage tabHnLabOut = new C1DockingTabPage();
                        tabHnLabOut.Location = new System.Drawing.Point(1, 24);
                        //tabScan.Name = "c1DockingTabPage1";
                        tabHnLabOut.Size = new System.Drawing.Size(667, 175);
                        tabHnLabOut.TabIndex = 0;
                        tabHnLabOut.Text = bc.datetoShow(row["labex_date"].ToString());
                        tabHnLabOut.Name = "tabHnLabOut_" + datetick;
                        //tabHnLabOut.DoubleClick += TabHnLabOut_DoubleClick;
                        tcHnLabOut.TabPages.Add(tabHnLabOut);

                        stream.Seek(0, SeekOrigin.Begin);
                        var fileStream = new FileStream("report\\" + datetick + ".pdf", FileMode.Create, FileAccess.Write);
                        stream.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Dispose();
                        Application.DoEvents();
                        Thread.Sleep(200);

                        //tcHnLabOut.Controls.Add(tablabOut);
                        if (bc.iniC.windows.Equals("windowsxp"))
                        {
                            string currentDirectory = Directory.GetCurrentDirectory();
                            WebBrowser webBrowser1;
                            webBrowser1 = new System.Windows.Forms.WebBrowser();
                            //webBrowser1.Enabled = true;
                            //webBrowser1.Location = new System.Drawing.Point(192, 0);
                            webBrowser1.Name = "webBrowser1";
                            //axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(Resources.GetObject("axAcroPDF1.OcxState")));
                            //webBrowser1.Size = new System.Drawing.Size(192, 192);
                            webBrowser1.Dock = DockStyle.Fill;
                            //axAcroPDF1.TabIndex = 7;
                            String file1 = "";
                            file1 = currentDirectory + "\\report\\" + datetick + ".pdf";
                            //new LogWriter("d", file1);
                            if (!File.Exists(file1))
                            {
                                MessageBox.Show("ไม่พบ File " + file1, "");
                            }
                            webBrowser1.Navigate(file1);
                            tabHnLabOut.Controls.Add(webBrowser1);
                        }
                        else
                        {
                            labOutView = new C1FlexViewer();
                            labOutView.AutoScrollMargin = new System.Drawing.Size(0, 0);
                            labOutView.AutoScrollMinSize = new System.Drawing.Size(0, 0);
                            labOutView.Dock = System.Windows.Forms.DockStyle.Fill;
                            labOutView.Location = new System.Drawing.Point(0, 0);
                            labOutView.Name = "c1FlexViewer1" + k.ToString();
                            labOutView.Size = new System.Drawing.Size(1065, 790);
                            labOutView.TabIndex = 0;

                            tabHnLabOut.Controls.Add(labOutView);

                            C1PdfDocumentSource pds = new C1PdfDocumentSource();

                            pds.LoadFromStream(stream);

                            //pds.LoadFromFile(filename1);

                            labOutView.DocumentSource = pds;
                        }
                    }
                    catch(Exception ex)
                    {
                        new LogWriter("e", "FrmScanView1 setTabHnLabOut " + ex.Message);
                    }
                    //}
                }
            }
            else
            {
                tabHnLabOutR.Clear();
            }
            if (dtlaboutdsc.Rows.Count > 0)
            {
                int k = 0;
                foreach (DataRow rowdsc in dtlaboutdsc.Rows)
                {
                    try
                    {
                        k++;
                        //pB1.Value = k;
                        String vn = "", preno = "", vsdate = "", an = "", labexid = "", yearid = "", filename = "", filename1 = "", datetick = "";
                        datetick = DateTime.Now.Ticks.ToString();
                        //labexid = dt.Rows[0][bc.bcDB.labexDB.labex.Id].ToString();
                        //filename = labexid + "_" + k.ToString() + ".pdf";
                        //yearid = dt.Rows[0][bc.bcDB.labexDB.labex.YearId].ToString();
                        //vn = dt.Rows[0][bc.bcDB.labexDB.labex.Vn].ToString();
                        //vn = vn.Replace("/", ".").Replace("(", ".").Replace(")", "");

                        C1DockingTabPage tabHnLabOut = new C1DockingTabPage();
                        //tabHnLabOut.Location = new System.Drawing.Point(1, 24);
                        //tabScan.Name = "c1DockingTabPage1";
                        tabHnLabOut.Size = new System.Drawing.Size(667, 175);
                        tabHnLabOut.TabIndex = 0;
                        //tabHnLabOut.Text = bc.datetoShow(rowdsc["date_create"].ToString());
                        if (rowdsc["ml_fm"].ToString().Equals("FM-LAB-997"))
                        {
                            tabHnLabOut.Text = "Patho " + bc.datetoShow(rowdsc["date_create"].ToString());
                        }
                        else if (rowdsc["ml_fm"].ToString().Equals("FM-LAB-998"))
                        {
                            tabHnLabOut.Text = "Clinical " + bc.datetoShow(rowdsc["date_create"].ToString());
                        }
                        else
                        {
                            tabHnLabOut.Text = rowdsc["ml_fm"].ToString();
                        }
                        tabHnLabOut.Name = "tabHnLabOut_" + datetick;
                        //tabHnLabOut.DoubleClick += TabHnLabOut_DoubleClick;
                        tcHnLabOut.TabPages.Add(tabHnLabOut);
                        //for (int i = 0; i < 6; i++)
                        //{
                        MemoryStream stream;

                        if (!Directory.Exists("report"))
                        {
                            Directory.CreateDirectory("report");
                        }

                        FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                        stream = ftpc.download(rowdsc[bc.bcDB.dscDB.dsc.folder_ftp] + "/" + rowdsc[bc.bcDB.dscDB.dsc.image_path].ToString());
                        if (stream == null)
                        {
                            setHeaderEnable(pB1);
                            continue;
                        }
                        if (stream.Length == 0)
                        {
                            setHeaderEnable(pB1);
                            continue;
                        }
                        stream.Seek(0, SeekOrigin.Begin);
                        var fileStream = new FileStream("report\\" + datetick + ".pdf", FileMode.Create, FileAccess.Write);
                        stream.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Dispose();
                        Application.DoEvents();
                        Thread.Sleep(200);

                        //tcHnLabOut.Controls.Add(tablabOut);
                        if (bc.iniC.windows.Equals("windowsxp"))
                        {
                            string currentDirectory = Directory.GetCurrentDirectory();
                            WebBrowser webBrowser1;
                            webBrowser1 = new System.Windows.Forms.WebBrowser();
                            //webBrowser1.Enabled = true;
                            //webBrowser1.Location = new System.Drawing.Point(192, 0);
                            webBrowser1.Name = "webBrowser1";
                            //axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(Resources.GetObject("axAcroPDF1.OcxState")));
                            //webBrowser1.Size = new System.Drawing.Size(192, 192);
                            webBrowser1.Dock = DockStyle.Fill;
                            //axAcroPDF1.TabIndex = 7;
                            String file1 = "";
                            file1 = currentDirectory + "\\report\\" + datetick + ".pdf";
                            //new LogWriter("d", file1);
                            if (!File.Exists(file1))
                            {
                                MessageBox.Show("ไม่พบ File " + file1, "");
                            }
                            webBrowser1.Navigate(file1);
                            tabHnLabOut.Controls.Add(webBrowser1);
                        }
                        else
                        {
                            labOutView = new C1FlexViewer();
                            labOutView.AutoScrollMargin = new System.Drawing.Size(0, 0);
                            labOutView.AutoScrollMinSize = new System.Drawing.Size(0, 0);
                            labOutView.Dock = System.Windows.Forms.DockStyle.Fill;
                            labOutView.Location = new System.Drawing.Point(0, 0);
                            labOutView.Name = "c1FlexViewer1" + k.ToString();
                            labOutView.Size = new System.Drawing.Size(1065, 790);
                            labOutView.TabIndex = 0;

                            tabHnLabOut.Controls.Add(labOutView);

                            C1PdfDocumentSource pds = new C1PdfDocumentSource();

                            pds.LoadFromStream(stream);

                            //pds.LoadFromFile(filename1);

                            labOutView.DocumentSource = pds;
                        }
                    }
                    catch (Exception ex)
                    {
                        new LogWriter("e", "FrmScanView1 setTabHnLabOut "+ex.Message);
                    }
                    
                }
            }
                setHeaderEnable(pB1);
        }
        private void setTabMachineResult()
        {
            if (txtHn.Text.Trim().Length <= 0)
            {
                return;
            }
            tcMac.TabPages.Clear();
            DataTable dtlaboutdsc = new DataTable();            
            dtlaboutdsc = bc.bcDB.dscDB.selectMedResultByDateReq("", "", txtHn.Text.Trim());
            if (dtlaboutdsc.Rows.Count > 0)
            {
                int k = 0;
                foreach (DataRow rowdsc in dtlaboutdsc.Rows)
                {
                    try
                    {
                        k++;
                        //pB1.Value = k;
                        String vn = "", preno = "", vsdate = "", an = "", labexid = "", yearid = "", filename = "", filename1 = "", datetick = "";
                        datetick = DateTime.Now.Ticks.ToString();                        

                        C1DockingTabPage tabHnLabOut = new C1DockingTabPage();                        
                        tabHnLabOut.Size = new System.Drawing.Size(667, 175);
                        tabHnLabOut.TabIndex = 0;
                        //tabHnLabOut.Text = bc.datetoShow(rowdsc["date_create"].ToString());
                        
                        tabHnLabOut.Text = rowdsc["ml_fm"].ToString();
                        
                        tabHnLabOut.Name = "tabHnLabOut_" + datetick;
                        //tabHnLabOut.DoubleClick += TabHnLabOut_DoubleClick;
                        tcMac.TabPages.Add(tabHnLabOut);
                        //for (int i = 0; i < 6; i++)
                        //{
                        MemoryStream stream;

                        if (!Directory.Exists("report"))
                        {
                            Directory.CreateDirectory("report");
                        }

                        FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
                        stream = ftpc.download(rowdsc[bc.bcDB.dscDB.dsc.folder_ftp] + "/" + rowdsc[bc.bcDB.dscDB.dsc.image_path].ToString());
                        if (stream == null)
                        {
                            continue;
                        }
                        if (stream.Length == 0)
                        {
                            continue;
                        }
                        stream.Seek(0, SeekOrigin.Begin);
                        var fileStream = new FileStream("report\\" + datetick + ".pdf", FileMode.Create, FileAccess.Write);
                        stream.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Dispose();
                        Application.DoEvents();
                        Thread.Sleep(200);

                        //tcHnLabOut.Controls.Add(tablabOut);
                        if (bc.iniC.windows.Equals("windowsxp"))
                        {
                            string currentDirectory = Directory.GetCurrentDirectory();
                            WebBrowser webBrowser1;
                            webBrowser1 = new System.Windows.Forms.WebBrowser();
                            //webBrowser1.Enabled = true;
                            //webBrowser1.Location = new System.Drawing.Point(192, 0);
                            webBrowser1.Name = "webBrowser1";
                            //axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(Resources.GetObject("axAcroPDF1.OcxState")));
                            //webBrowser1.Size = new System.Drawing.Size(192, 192);
                            webBrowser1.Dock = DockStyle.Fill;
                            //axAcroPDF1.TabIndex = 7;
                            String file1 = "";
                            file1 = currentDirectory + "\\report\\" + datetick + ".pdf";
                            //new LogWriter("d", file1);
                            if (!File.Exists(file1))
                            {
                                MessageBox.Show("ไม่พบ File " + file1, "");
                            }
                            webBrowser1.Navigate(file1);
                            tabHnLabOut.Controls.Add(webBrowser1);
                        }
                        else
                        {
                            labOutView = new C1FlexViewer();
                            labOutView.AutoScrollMargin = new System.Drawing.Size(0, 0);
                            labOutView.AutoScrollMinSize = new System.Drawing.Size(0, 0);
                            labOutView.Dock = System.Windows.Forms.DockStyle.Fill;
                            labOutView.Location = new System.Drawing.Point(0, 0);
                            labOutView.Name = "c1FlexViewer1" + k.ToString();
                            labOutView.Size = new System.Drawing.Size(1065, 790);
                            labOutView.TabIndex = 0;

                            tabHnLabOut.Controls.Add(labOutView);

                            C1PdfDocumentSource pds = new C1PdfDocumentSource();

                            pds.LoadFromStream(stream);

                            //pds.LoadFromFile(filename1);

                            labOutView.DocumentSource = pds;
                        }
                    }
                    catch (Exception ex)
                    {
                        new LogWriter("e", "FrmScanView1 setTabHnLabOut " + ex.Message);
                    }
                }
            }
        }
        private void setHeaderEnable(ProgressBar pB1)
        {
            pB1.Dispose();
            txtVN.Show();
            txtHn.Show();
            txtName.Show();
            //label1.Show();
            //cboDgs.Show();
            
            btnHn.Show();
            //txt.Show();
            //label6.Show();
            chkIPD.Show();
            //grf1.AutoSizeRows();
            //grf1.AutoSizeRows();
            panel2.Enabled = true;
        }
        private void setHeaderDisable()
        {
            txtVN.Hide();
            txtHn.Hide();
            txtName.Hide();
            //label1.Hide();
            //cboDgs.Hide();
            
            btnHn.Hide();
            //txt.Hide();
            //label6.Show();
            chkIPD.Hide();
            //grf1.AutoSizeRows();
            //grf1.AutoSizeRows();
            panel2.Enabled = false;
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            grfIPD.AutoSizeCols();
            grfIPD.AutoSizeRows();
        }

        private void BtnHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmSearchHn frm = new FrmSearchHn(bc, FrmSearchHn.StatusConnection.host);
            frm.ShowDialog(this);
            txtHn.Value = bc.sPtt.Hn;
            txtName.Value = bc.sPtt.Name;
            setGrfVsIPD();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

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
        private void initGrfOPD()
        {
            grfOPD = new C1FlexGrid();
            grfOPD.Font = fEdit;
            grfOPD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOPD.Location = new System.Drawing.Point(0, 0);
            grfOPD.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);

            grfOPD.AfterRowColChange += GrfOPD_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("&ต้องการลบข้อมูลมั้งหมด ของ รายการนี้", new EventHandler(ContextMenu_delete_opd_all));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            grfOPD.ContextMenu = menuGw;

            tabOPD.Controls.Add(grfOPD);

            theme1.SetTheme(grfOPD, "ExpressionDark");
        }
        private void ContextMenu_delete_opd_all(object sender, System.EventArgs e)
        {
            String id = "", vn="";
            vn = grfOPD[grfOPD.Row, colVsVn].ToString();
            if (MessageBox.Show("ต้องการลบข้อมูล ทั้งหมดของ VN "+ vn, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                int chk = 0;
                String re = bc.bcDB.dscDB.voidDocScanVN(vn, "");
                if (int.TryParse(re, out chk))
                {
                    setGrfLab(grfOPD.Row, "OPD");
                    setGrfXrayOPD(grfOPD.Row);
                    setGrfScan(grfOPD.Row, "OPD");
                    if (!bc.iniC.windows.Equals("windowsxp"))
                    {
                        //setTabLabOut(grfOPD.Row, "OPD", bc.iniC.windows);
                    }
                }
            }
        }
        private void GrfOPD_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 !=1) return;

            if (txtHn.Text.Equals("")) return;
            chkIPD.Checked = false;
            //txt.Value = "";
            //new LogWriter("d", "FrmScanView1 GrfOPD_AfterRowColChange 01 setGrfLab");
            try
            {
                showFormWaiting();
                setGrfLab(e.NewRange.r1, "OPD");
                //new LogWriter("d", "FrmScanView1 GrfOPD_AfterRowColChange 02 setGrfXrayOPD");
                setGrfXrayOPD(e.NewRange.r1);
                //new LogWriter("d", "FrmScanView1 GrfOPD_AfterRowColChange 03 setGrfScan");
                setGrfScan(e.NewRange.r1, "OPD");
                //new LogWriter("d", "FrmScanView1 GrfOPD_AfterRowColChange 04 setTabLabOut");
                if (!bc.iniC.windows.Equals("windowsxp"))
                {
                    //setTabLabOut(e.NewRange.r1, "OPD", bc.iniC.windows);
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmScanView1 GrfOPD_AfterRowColChange "+ex.Message);
            }
            finally
            {
                frmFlash.Dispose();
            }
            
            grfOPD.Focus();
        }

        private void initGrfIPD()
        {
            grfIPD = new C1FlexGrid();
            grfIPD.Font = fEdit;
            grfIPD.Dock = System.Windows.Forms.DockStyle.Fill;
            grfIPD.Location = new System.Drawing.Point(0, 0);
            grfIPD.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);

            grfIPD.AfterRowColChange += GrfIPD_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("&ต้องการลบข้อมูลมั้งหมด ของ รายการนี้", new EventHandler(ContextMenu_delete_ipd_all));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));
            grfIPD.ContextMenu = menuGw;

            tabIPD.Controls.Add(grfIPD);

            theme1.SetTheme(grfIPD, "ExpressionDark");

        }
        private void ContextMenu_delete_ipd_all(object sender, System.EventArgs e)
        {
            String id = "", vn = "";
            vn = grfIPD[grfIPD.Row, colIPDAnShow].ToString();
            if (MessageBox.Show("ต้องการลบข้อมูล ทั้งหมดของ AN " + vn, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                int chk = 0;
                String re = bc.bcDB.dscDB.voidDocScanAN(vn, "");
                if (int.TryParse(re, out chk))
                {
                    setGrfLab(grfIPD.Row, "IPD");
                    setGrfXray(grfIPD.Row);
                    setGrfScan(grfIPD.Row, "IPD");
                    //setTabLabOut(grfIPD.Row, "IPD", bc.iniC.windows);
                }
            }
        }
        private void initGrfPrn()
        {
            grfPrn = new C1FlexGrid();
            grfPrn.Font = fEdit;
            grfPrn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfPrn.Location = new System.Drawing.Point(0, 0);
            grfPrn.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);

            grfPrn.AfterRowColChange += GrfPrn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);
            
            tabPrn.Controls.Add(grfPrn);

            theme1.SetTheme(grfPrn, "ExpressionDark");

        }
        
        private void initGrfHn()
        {
            grfHn = new C1FlexGrid();
            grfHn.Font = fEdit;
            grfHn.Dock = System.Windows.Forms.DockStyle.Fill;
            grfHn.Location = new System.Drawing.Point(0, 0);
            grfHn.Rows.Count = 1;
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            tabHn.Controls.Add(grfHn);

            theme1.SetTheme(grfHn, bc.iniC.themeApp);

        }
        private void setGrHn()
        {
            //new Thread(() =>
            //{
            try
            {
                DataTable dt = new DataTable();
                String vn = "", preno = "", vsdate = "", an = "";

                Application.DoEvents();
                if (txtHn.Text.Length <= 0) return;
                dt = bc.bcDB.vsDB.selectDrugAllergy(txtHn.Text.Trim());
                //grfHn.Rows.Count = 1;
                //grfLab.Cols[colOrderId].Visible = false;
                grfHn.Rows.Count = 4;
                grfHn.Rows.Count = dt.Rows.Count + 1;
                grfHn.Cols.Count = 6;
                grfHn.Cols[colDrugAllCode].Caption = "Code";
                grfHn.Cols[colDrugAllName].Caption = "Drug Name";
                grfHn.Cols[colDrugAllDsc].Caption = "Drug Allergy";
                grfHn.Cols[colDrugAllAlg].Caption = "Desc";

                grfHn.Cols[colDrugAllCode].Width = 100;
                grfHn.Cols[colDrugAllName].Width = 250;
                grfHn.Cols[colDrugAllDsc].Width = 300;
                grfHn.Cols[colDrugAllAlg].Width = 200;

                int i = 0;
                decimal aaa = 0;
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    grfHn[i, colDrugAllCode] = row1["MNC_ph_cd"].ToString();
                    grfHn[i, colDrugAllName] = row1["MNC_ph_tn"].ToString();
                    grfHn[i, colDrugAllDsc] = row1["MNC_ph_memo"].ToString();
                    grfHn[i, colDrugAllAlg] = row1["MNC_PH_ALG_DSC"].ToString();

                    //row1[0] = (i - 2);
                }
                CellNoteManager mgr = new CellNoteManager(grfXray);
                grfHn.Cols[colDrugAllCode].AllowEditing = false;
                grfHn.Cols[colDrugAllName].AllowEditing = false;
                grfHn.Cols[colDrugAllDsc].AllowEditing = false;
                grfHn.Cols[colDrugAllAlg].AllowEditing = false;
            }
            catch(Exception ex)
            {
                new LogWriter("e", ex.Message);
            }
            
            //}).Start();
        }
        private void setGrfOrder(DataTable dtOrder)
        {
            //new Thread(() =>
            //{
            //DataTable dtOrder = new DataTable();
            String vsdate = "";

            Application.DoEvents();
            
            //if (txtHn.Text.Length <= 0) return;
            //dt = bc.bcDB.vsDB.selectDrugAllergy(txtHn.Text.Trim());
            //grfHn.Rows.Count = 1;
            //grfLab.Cols[colOrderId].Visible = false;
            grfOrder.Rows.Count = 1;
            grfOrder.Rows.Count = dtOrder.Rows.Count + 1;
            //grfOrder.Cols.Count = 6;
            grfOrder.Cols[colOrderName].Caption = "Drug Name";
            grfOrder.Cols[colOrderMed].Caption = "MED";
            grfOrder.Cols[colOrderQty].Caption = "QTY";
            //grfOrder.Cols[colDrugAllAlg].Caption = "Desc";

            grfOrder.Cols[colOrderName].Width = 300;
            grfOrder.Cols[colOrderMed].Width = 200;
            grfOrder.Cols[colOrderQty].Width = 80;
            //grfOrder.Cols[colDrugAllAlg].Width = 200;

            int i = 0;
            decimal aaa = 0;
            foreach (DataRow row1 in dtOrder.Rows)
            {
                i++;
                grfOrder[i, colOrderName] = row1["MNC_PH_TN"].ToString();
                grfOrder[i, colOrderMed] = "";
                grfOrder[i, colOrderQty] = row1["qty"].ToString();
                //grfOrder[i, colDrugAllAlg] = row1["MNC_PH_ALG_DSC"].ToString();

                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfOrder);
            grfOrder.Cols[colOrderName].AllowEditing = false;
            grfOrder.Cols[colOrderQty].AllowEditing = false;
            grfOrder.Cols[colOrderMed].AllowEditing = false;
            grfOrder.Cols[colOrderId].Visible = false;
            //}).Start();
        }
        private void setGrOrder(String vn, String preno, String an, String anyr, String flagOPD)
        {
            //new Thread(() =>
            //{
            DataTable dtOrder = new DataTable();
            String vsdate = "";
            String statusOPD = "", vsDate = "", anDate = "", hn = "", vn1 = "";

            Application.DoEvents();
            if (flagOPD.Equals("OPD"))
            {
                statusOPD = grfOPD[grfOPD.Row, colVsStatus] != null ? grfOPD[grfOPD.Row, colVsStatus].ToString() : "";
                preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
                vsDate = grfOPD[grfOPD.Row, colVsVsDate] != null ? grfOPD[grfOPD.Row, colVsVsDate].ToString() : "";
                //vsDate = bc.datetoDB(vsDate);

                chkIPD.Checked = false;
                vn = grfOPD[grfOPD.Row, colVsVn] != null ? grfOPD[grfOPD.Row, colVsVn].ToString() : "";
                txtVN.Value = vn;
                label2.Text = "VN :";
                if (vn.IndexOf("(") > 0)
                {
                    vn1 = vn.Substring(0, vn.IndexOf("("));
                }
                if (vn.IndexOf("/") > 0)
                {
                    vn1 = vn.Substring(0, vn.IndexOf("/"));
                }
                dtOrder = bc.bcDB.vsDB.selectDrugOPD(txtHn.Text, vn, preno);
            }
            else
            {
                statusOPD = grfIPD[grfIPD.Row, colIPDStatus] != null ? grfIPD[grfIPD.Row, colIPDStatus].ToString() : "";
                preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
                vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";

                chkIPD.Checked = true;
                an = grfIPD[grfIPD.Row, colIPDAn] != null ? grfIPD[grfIPD.Row, colIPDAn].ToString() : "";
                anDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
                anyr = grfIPD[grfIPD.Row, colIPDAnYr] != null ? grfIPD[grfIPD.Row, colIPDAnYr].ToString() : "";
                txtVN.Value = an;
                label2.Text = "AN :";
                dtOrder = bc.bcDB.vsDB.selectDrugIPD(txtHn.Text, an, anyr);
            }
                //if (txtHn.Text.Length <= 0) return;
                //dt = bc.bcDB.vsDB.selectDrugAllergy(txtHn.Text.Trim());
                //grfHn.Rows.Count = 1;
                //grfLab.Cols[colOrderId].Visible = false;
            grfOrder.Rows.Count = 1;
            grfOrder.Rows.Count = dtOrder.Rows.Count + 1;
            //grfOrder.Cols.Count = 6;
            grfOrder.Cols[colOrderName].Caption = "Drug Name";
            grfOrder.Cols[colOrderMed].Caption = "MED";
            grfOrder.Cols[colOrderQty].Caption = "QTY";
            //grfOrder.Cols[colDrugAllAlg].Caption = "Desc";

            grfOrder.Cols[colOrderName].Width = 300;
            grfOrder.Cols[colOrderMed].Width = 200;
            grfOrder.Cols[colOrderQty].Width = 80;
            //grfOrder.Cols[colDrugAllAlg].Width = 200;

            int i = 0;
            decimal aaa = 0;
            foreach (DataRow row1 in dtOrder.Rows)
            {
                i++;
                grfOrder[i, colOrderName] = row1["MNC_PH_TN"].ToString();
                grfOrder[i, colOrderMed] = "";
                grfOrder[i, colOrderQty] = row1["qty"].ToString();
                //grfOrder[i, colDrugAllAlg] = row1["MNC_PH_ALG_DSC"].ToString();

                //row1[0] = (i - 2);
            }
            CellNoteManager mgr = new CellNoteManager(grfOrder);
            grfOrder.Cols[colOrderName].AllowEditing = false;
            grfOrder.Cols[colOrderQty].AllowEditing = false;
            grfOrder.Cols[colOrderMed].AllowEditing = false;
            grfOrder.Cols[colOrderId].Visible = false;
            //}).Start();
        }
        private void GrfHn_AfterRowColChange(object sender, RangeEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GrfPrn_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void setStaffNote(String vsDate, String preno)
        {
            String file = "", dd = "", mm = "", yy = "";
            Image stffnoteR, stffnoteS;
            if (vsDate.Length > 8)
            {
                try
                {
                    picL.Image = null;
                    picR.Image = null;
                    String preno1 = preno;
                    int chk = 0;
                    dd = vsDate.Substring(vsDate.Length - 2);
                    mm = vsDate.Substring(5, 2);
                    yy = vsDate.Substring(0,4);
                    int.TryParse(yy, out chk);
                    if (chk > 2500)
                        chk -= 543;
                    file = "\\\\"+bc.iniC.pathScanStaffNote + chk + "\\" + mm + "\\" + dd + "\\";
                    preno1 = "000000" + preno1;
                    preno1 = preno1.Substring(preno1.Length - 6);
                    stffnoteR = Image.FromFile(file + preno1 + "R.JPG");
                    stffnoteS = Image.FromFile(file + preno1 + "S.JPG");
                    picL.Image = stffnoteS;
                    picR.Image = stffnoteR;
                    ContextMenu menuGwL = new ContextMenu();
                    menuGwL.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_print_staffnote_L));
                    ContextMenu menuGwR = new ContextMenu();
                    menuGwR.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_print_staffnote_R));
                    picL.ContextMenu = menuGwL;
                    picR.ContextMenu = menuGwR;

                }
                catch (Exception ex)
                {
                    //MessageBox.Show("ไม่พบ StaffNote ในระบบ " + ex.Message, "");
                }
            }
        }
        private void ContextMenu_print_staffnote_L(object sender, System.EventArgs e)
        {
            setGrfScanToPrintStaffNoteL();
        }
        private void ContextMenu_print_staffnote_R(object sender, System.EventArgs e)
        {
            setGrfScanToPrintStaffNoteR();
        }
        private void setGrfScanToPrintStaffNoteR()
        {
            SetDefaultPrinter(bc.iniC.printerA4);
            System.Threading.Thread.Sleep(500);

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += Pd_PrintPageA4_staffnote_R;
            //here to select the printer attached to user PC
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = pd;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                pd.Print();//this will trigger the Print Event handeler PrintPage
            }
        }
        private void setGrfScanToPrintStaffNoteL()
        {
            SetDefaultPrinter(bc.iniC.printerA4);
            System.Threading.Thread.Sleep(500);

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += Pd_PrintPageA4_staffnote_L;
            //here to select the printer attached to user PC
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = pd;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                pd.Print();//this will trigger the Print Event handeler PrintPage
            }
        }
        private void Pd_PrintPageA4_staffnote_R(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                float newWidth = picR.Image.Width * 100 / picR.Image.HorizontalResolution;
                float newHeight = picR.Image.Height * 100 / picR.Image.VerticalResolution;

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
                e.Graphics.DrawImage(picR.Image, 0, 0, (int)newWidth, (int)newHeight);
                //}
            }
            catch (Exception)
            {

            }
        }
        private void Pd_PrintPageA4_staffnote_L(object sender, PrintPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                float newWidth = picL.Image.Width * 100 / picL.Image.HorizontalResolution;
                float newHeight = picL.Image.Height * 100 / picL.Image.VerticalResolution;

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
                e.Graphics.DrawImage(picL.Image, 0, 0, (int)newWidth, (int)newHeight);
                //}
            }
            catch (Exception)
            {

            }
        }
        private void GrfIPD_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;
            String an = "";
            chkIPD.Checked = true;
            an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";
            //txt.Value = an;
            txtVN.Value = an;
            if (grfIPD.Row != 0)
            {
                if (e.NewRange.r1 == e.OldRange.r1) return;
            }
            
            if (txtHn.Text.Equals("")) return;
            
            setGrfLab(e.NewRange.r1, "IPD");
            setGrfXray(e.NewRange.r1);
            setGrfScan(e.NewRange.r1, "IPD");
            //setTabLabOut(e.NewRange.r1, "IPD", bc.iniC.windows);
        }
        private void setTabLabOut(int row, String flagOPD, String flagWindows)
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
            if (flagOPD.Equals("OPD"))
            {
                vn = grfOPD[row, colVsVn] != null ? grfOPD[row, colVsVn].ToString() : "";
                preno = grfOPD[row, colVsPreno] != null ? grfOPD[row, colVsPreno].ToString() : "";
                vsdate = grfOPD[row, colVsVsDate] != null ? grfOPD[row, colVsVsDate].ToString() : "";
                //an = grfOPD[row, colIPDAnShow] != null ? grfOPD[row, colIPDAnShow].ToString() : "";
            }
            else
            {
                vn = grfIPD[row, colIPDVn] != null ? grfIPD[row, colIPDVn].ToString() : "";
                preno = grfIPD[row, colIPDPreno] != null ? grfIPD[row, colIPDPreno].ToString() : "";
                vsdate = grfIPD[row, colIPDDate] != null ? grfIPD[row, colIPDDate].ToString() : "";
                an = grfIPD[row, colIPDAnShow] != null ? grfIPD[row, colIPDAnShow].ToString() : "";
            }
            
            vn = vn.Replace("/", ".").Replace("(", ".").Replace(")", "");
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt = bc.bcDB.labexDB.selectByVn(txtHn.Text.Trim(), vn);
            if (!flagWindows.Equals("windowsxp"))
            {
                if (dt.Rows.Count > 0)
                {
                    String labexid = "", yearid = "";
                    labexid = dt.Rows[0][bc.bcDB.labexDB.labex.Id].ToString();
                    yearid = dt.Rows[0][bc.bcDB.labexDB.labex.YearId].ToString();
                    for (int i = 0; i < 6; i++)
                    {
                        MemoryStream stream;
                        String filename = "", filename1 = "";
                        filename = labexid + "_" + (i + 1).ToString() + ".pdf";
                        FtpClient ftpc = new FtpClient(bc.iniC.hostFTPLabOut, bc.iniC.userFTPLabOut, bc.iniC.passFTPLabOut, bc.ftpUsePassiveLabOut);
                        stream = ftpc.download(bc.iniC.folderFTPLabOut + "//" + yearid + "//" + filename);
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
                        labOutView.Name = "c1FlexViewer1" + i.ToString();
                        labOutView.Size = new System.Drawing.Size(1065, 790);
                        labOutView.TabIndex = 0;

                        tablabOut.Controls.Add(labOutView);

                        C1PdfDocumentSource pds = new C1PdfDocumentSource();

                        pds.LoadFromStream(stream);
                        //pds.LoadFromFile(filename1);

                        labOutView.DocumentSource = pds;
                    }
                }
            }
            else
            {
                tablabOut = new C1DockingTabPage();
                tablabOut.Location = new System.Drawing.Point(1, 24);
                //tabScan.Name = "c1DockingTabPage1";
                tablabOut.Size = new System.Drawing.Size(667, 175);
                tablabOut.TabIndex = 0;
                tablabOut.Text = "OUT LAB";
                tablabOut.Name = "tablabOutOld";
                tcDtr.Controls.Add(tablabOut);
                Panel pnlabout = new Panel();
                pnlabout.Dock = DockStyle.Fill;
                tablabOut.Controls.Add(pnlabout);
                C1FlexGrid grflabout = new C1FlexGrid();
                grflabout.Dock = DockStyle.Fill;
                grflabout.Cols[colLabOutDateReq].Caption = "Date Req";
                grflabout.Cols[colLabOutHN].Caption = "HN";
                grflabout.Cols[colLabOutFullName].Caption = "Name";
                grflabout.Cols[colLabOutVN].Caption = "Date Rec";
                grflabout.Cols[colLabOutDateReceive].Caption = "Req No";
                grflabout.Cols[colLabOutReqNo].Caption = "VN";
                grflabout.Cols[colLabOutId].Caption = "id";
                grflabout.Cols[colLabOutDateReq].Width = 100;
                grflabout.Cols[colLabOutHN].Width = 80;
                grflabout.Cols[colLabOutFullName].Width = 300;
                grflabout.Cols[colLabOutVN].Width = 100;
                grflabout.Cols[colLabOutDateReceive].Width = 80;
                grflabout.Cols[colLabOutReqNo].Width = 80;

                pnlabout.Controls.Add(grflabout);
            }
            dt1 = bc.bcDB.labexDB.selectByVn(txtHn.Text.Trim(), vn);
        }
        private void setGrfXray(int row)
        {
            //new Thread(() =>
            //{
            DataTable dt = new DataTable();
            String vn = "", preno = "", vsdate = "", an = "";
            vn = grfIPD[row, colIPDVn] != null ? grfIPD[row, colIPDVn].ToString() : "";
            preno = grfIPD[row, colIPDPreno] != null ? grfIPD[row, colIPDPreno].ToString() : "";
            vsdate = grfIPD[row, colIPDDate] != null ? grfIPD[row, colIPDDate].ToString() : "";
            an = grfIPD[row, colIPDAnShow] != null ? grfIPD[row, colIPDAnShow].ToString() : "";
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
        private void setGrfXrayOPD(int row)
        {
            ProgressBar pB1 = new ProgressBar();
            pB1.Location = new System.Drawing.Point(20, 16);
            pB1.Name = "pB1";
            pB1.Size = new System.Drawing.Size(862, 23);
            gbPtt.Controls.Add(pB1);
            pB1.Left = txtHn.Left;
            pB1.Show();
            setHeaderDisable();
            DataTable dt = new DataTable();
            String vn = "", preno = "", vsdate = "", an = "";
            vn = grfOPD[row, colVsVn] != null ? grfOPD[row, colVsVn].ToString() : "";
            preno = grfOPD[row, colVsPreno] != null ? grfOPD[row, colVsPreno].ToString() : "";
            vsdate = grfOPD[row, colVsVsDate] != null ? grfOPD[row, colVsVsDate].ToString() : "";
            an = grfOPD[row, colVsAn] != null ? grfOPD[row, colVsAn].ToString() : "";
            //new LogWriter("e", "FrmScanView1 setGrfXrayOPD vsdate " + vsdate);
            vsdate = bc.datetoDB(vsdate);
            if (vn.IndexOf("(") > 0)
            {
                vn = vn.Substring(0, vn.IndexOf("("));
            }
            if (vn.IndexOf("/") > 0)
            {
                vn = vn.Substring(0, vn.IndexOf("/"));
            }
            Application.DoEvents();
            //if (an.Length > 0)
            //{
            //    String[] an1 = an.Split('/');
            //    if (an1.Length > 0)
            //    {
            //        dt = bc.bcDB.vsDB.selectResultXraybyAN(txtHn.Text, an1[0], an1[1]);
            //    }
            //}
            //else
            //{
                dt = bc.bcDB.vsDB.selectResultXraybyVN(txtHn.Text, vn, vsdate);
            //}
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
            pB1.Maximum = dt.Rows.Count;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    pB1.Value = i;
                    grfXray[i, colXrayDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
                    grfXray[i, colXrayName] = row1["MNC_XR_DSC"].ToString();
                    grfXray[i, colXrayCode] = row1["MNC_XR_CD"].ToString();
                    grfXray[i, colXrayResult] = row1["result"].ToString();

                    //row1[0] = (i - 2);
                }
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmScanView1 setGrfXrayOPD " + ex.Message);
            }
            
            CellNoteManager mgr = new CellNoteManager(grfXray);
            grfXray.Cols[colXrayDate].AllowEditing = false;
            grfXray.Cols[colXrayName].AllowEditing = false;
            grfXray.Cols[colXrayCode].AllowEditing = false;
            grfXray.Cols[colXrayResult].AllowEditing = false;
            //}).Start();

            setHeaderEnable(pB1);
        }
        private void setGrfLab(int row, String flagOPD)
        {
            ProgressBar pB1 = new ProgressBar();
            pB1.Location = new System.Drawing.Point(20, 16);
            pB1.Name = "pB1";
            pB1.Size = new System.Drawing.Size(862, 23);
            gbPtt.Controls.Add(pB1);
            pB1.Left = txtHn.Left;
            pB1.Show();
            setHeaderDisable();

            DataTable dt = new DataTable();
            String vn = "", preno = "", vsdate="", an="";
            DateTime dtt = new DateTime();
            if (flagOPD.Equals("OPD"))
            {
                vn = grfOPD[row, colVsVn] != null ? grfOPD[row, colVsVn].ToString() : "";
                preno = grfOPD[row, colVsPreno] != null ? grfOPD[row, colVsPreno].ToString() : "";
                vsdate = grfOPD[row, colVsVsDate] != null ? grfOPD[row, colVsVsDate].ToString() : "";
                an = grfOPD[row, colVsAn] != null ? grfOPD[row, colVsAn].ToString() : "";
                vsdate = bc.datetoDB(vsdate);
                //if (DateTime.TryParse(vsdate, out dtt))
                //{
                //    vsdate = dtt.Year.ToString() + "-" + dtt.ToString("MM-dd");
                //}
                if (vsdate.Length <= 0)
                {
                    return;
                }
                if (vn.IndexOf("(") > 0)
                {
                    vn = vn.Substring(0, vn.IndexOf("("));
                }
                if (vn.IndexOf("/") > 0)
                {
                    vn = vn.Substring(0, vn.IndexOf("/"));
                }
                dt = bc.bcDB.vsDB.selectLabbyVN1(vsdate, vsdate, txtHn.Text, vn, preno);
            }
            else
            {
                vn = grfIPD[row, colIPDVn] != null ? grfIPD[row, colIPDVn].ToString() : "";
                preno = grfIPD[row, colIPDPreno] != null ? grfIPD[row, colIPDPreno].ToString() : "";
                vsdate = grfIPD[row, colIPDDate] != null ? grfIPD[row, colIPDDate].ToString() : "";
                an = grfIPD[row, colIPDAnShow] != null ? grfIPD[row, colIPDAnShow].ToString() : "";
                if (an.Length > 0)
                {
                    String[] an1 = an.Split('/');
                    if (an1.Length > 0)
                    {
                        dt = bc.bcDB.vsDB.selectResultLabbyAN(txtHn.Text, an1[0], an1[1]);
                    }
                }
            }
            
            Application.DoEvents();
                
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
            pB1.Maximum = dt.Rows.Count;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    pB1.Value = i;
                    grfLab[i, colLabDate] = bc.datetoShow(row1["mnc_req_dat"].ToString());
                    grfLab[i, colLabName] = row1["MNC_LB_DSC"].ToString();
                    grfLab[i, colLabNameSub] = row1["mnc_res"].ToString();
                    grfLab[i, colLabResult] = row1["MNC_RES_VALUE"].ToString();
                    grfLab[i, colInterpret] = row1["MNC_STS"].ToString();
                    grfLab[i, colNormal] = row1["MNC_LB_RES"].ToString();
                    grfLab[i, colUnit] = row1["MNC_RES_UNT"].ToString();
                    //row1[0] = (i - 2);
                }
            }
            catch(Exception ex)
            {
                new LogWriter("e", "FrmScanView1 setGrfLab grfLab " + ex.Message);
            }
            
            CellNoteManager mgr = new CellNoteManager(grfLab);
            grfLab.Cols[colLabName].AllowEditing = false;
            grfLab.Cols[colInterpret].AllowEditing = false;
            grfLab.Cols[colNormal].AllowEditing = false;
            //}).Start();

            setHeaderEnable(pB1);
        }
        private void setGrfScan(int row, String flagOPD)
        {
            Application.DoEvents();
            ProgressBar pB1 = new ProgressBar();
            pB1.Location = new System.Drawing.Point(20, 16);
            pB1.Name = "pB1";
            pB1.Size = new System.Drawing.Size(862, 23);
            gbPtt.Controls.Add(pB1);
            pB1.Left = txtHn.Left;
            pB1.Show();
            setHeaderDisable();

            //clearGrf();
            String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "", anyr="", vn1="";
            DataTable dtOrder = new DataTable();
            if (flagOPD.Equals("OPD"))
            {
                statusOPD = grfOPD[row, colVsStatus] != null ? grfOPD[row, colVsStatus].ToString() : "";
                preno = grfOPD[row, colVsPreno] != null ? grfOPD[row, colVsPreno].ToString() : "";
                vsDate = grfOPD[row, colVsVsDate] != null ? grfOPD[row, colVsVsDate].ToString() : "";
                //vsDate = bc.datetoDB(vsDate);

                chkIPD.Checked = false;
                vn = grfOPD[row, colVsVn] != null ? grfOPD[row, colVsVn].ToString() : "";
                txtVN.Value = vn;
                label2.Text = "VN :";
                if (vn.IndexOf("(") > 0)
                {
                    vn1 = vn.Substring(0, vn.IndexOf("("));
                }
                if (vn.IndexOf("/") > 0)
                {
                    vn1 = vn.Substring(0, vn.IndexOf("/"));
                }
                //new LogWriter("e", "FrmScanView1 setGrfScan 1 " +DateTime.Now.ToString());
                dtOrder = bc.bcDB.vsDB.selectDrugOPD(txtHn.Text, vn1, preno);
                //new LogWriter("e", "FrmScanView1 setGrfScan 2 " + DateTime.Now.ToString());
            }
            else
            {
                statusOPD = grfIPD[row, colIPDStatus] != null ? grfIPD[row, colIPDStatus].ToString() : "";
                preno = grfIPD[row, colIPDPreno] != null ? grfIPD[row, colIPDPreno].ToString() : "";
                vsDate = grfIPD[row, colIPDDate] != null ? grfIPD[row, colIPDDate].ToString() : "";

                chkIPD.Checked = true;
                an = grfIPD[row, colIPDAn] != null ? grfIPD[row, colIPDAn].ToString() : "";
                anDate = grfIPD[row, colIPDDate] != null ? grfIPD[row, colIPDDate].ToString() : "";
                anyr = grfIPD[row, colIPDAnYr] != null ? grfIPD[row, colIPDAnYr].ToString() : "";
                //txtVN.Value = an;
                label2.Text = "AN :";
                //txtVisitDate.Value = anDate;
                //dtOrder = bc.bcDB.vsDB.selectDrugOPD(txtHn.Text, an, anDate);
                //new LogWriter("e", "FrmScanView1 setGrfScan 1 " + DateTime.Now.ToString());
                dtOrder = bc.bcDB.vsDB.selectDrugIPD(txtHn.Text, an, anyr);
                //new LogWriter("e", "FrmScanView1 setGrfScan 2 " + DateTime.Now.ToString());
            }

            //txtVisitDate.Value = vsDate;
            //if (vsDate.Length >= 10)
            //{
            //    MessageBox.Show("vsDate " + vsDate + " " + vsDate.Substring(6,4) + "-" + vsDate.Substring(3, 2) + "-" + vsDate.Substring(0, 2), "");
            //}
            //else
            //{
            //    MessageBox.Show("vsDate " + vsDate, "");
            //}
            //MessageBox.Show("vsDate " + vsDate, "");
            //new LogWriter("e", "FrmScanView1 setGrfScan 3 " + vsDate);
            vsDate = bc.datetoDB(vsDate);
            setStaffNote(vsDate, preno);
            
            //vn = grfIPD[row, colIPDAnShow] != null ? grfIPD[row, colIPDAnShow].ToString() : "";
            //if (vn.IndexOf("(") > 0)
            //{
            //    vn = vn.Substring(0, vn.IndexOf("("));
            //}
            //if (vn.IndexOf("/") > 0)
            //{
            //    vn = vn.Substring(0, vn.IndexOf("/"));
            //}
            Application.DoEvents();
            //new LogWriter("e", "FrmScanView1 setGrfScan 4 ");
            //setGrfOrder(dtOrder);
            //grfOrder.Rows.Count = 0;
            //grfOrder.Rows.Count = 1;
            //grfOrder.Cols[colOrderName].Caption = "รายการยา";
            //grfOrder.Cols[colOrderMed].Caption = "med";
            //grfOrder.Cols[colOrderQty].Caption = "qty";
            //grfOrder.Cols[colOrderName].Width = 250;
            //grfOrder.Cols[colOrderMed].Width = 250;
            //grfOrder.Cols[colOrderQty].Width = 80;
            //if (dtOrder.Rows.Count > 0)
            //{
            //    try
            //    {
            //        pB1.Value = 0;
            //        pB1.Minimum = 0;
            //        pB1.Maximum = dtOrder.Rows.Count;

            //        grfOrder.Rows.Count = dtOrder.Rows.Count+1;
            //        foreach (DataRow row1 in dtOrder.Rows)
            //        {
            //            Row rowg = grfOrder.Rows[pB1.Value+1];
            //            rowg[colOrderName] = row1["MNC_PH_TN"].ToString();
            //            rowg[colOrderMed] = "";
            //            rowg[colOrderQty] = row1["qty"].ToString();
            //            pB1.Value++;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("" + ex.Message, "");
            //    }
            //    grfOrder.Cols[colOrderName].AllowEditing = false;
            //    grfOrder.Cols[colOrderMed].AllowEditing = false;
            //    grfOrder.Cols[colOrderQty].AllowEditing = false;
            //    grfOrder.Cols[colOrderId].Visible = false;
            //}
            //new LogWriter("e", "FrmScanView1 setGrfScan 5 ");
            GC.Collect();
            Application.DoEvents();
            DataTable dt = new DataTable();
            if (flagOPD.Equals("OPD"))
            {
                dt = bc.bcDB.dscDB.selectByVn(txtHn.Text, vn, vsDate);
            }
            else
            {
                an = grfIPD[row, colIPDAnShow] != null ? grfIPD[row, colIPDAnShow].ToString() : "";
                dt = bc.bcDB.dscDB.selectByAn(txtHn.Text, an);
                //if (dt.Rows.Count == 0)
                //{
                //    vn = grfIPD[row, colIPDVn] != null ? grfIPD[row, colIPDVn].ToString() : "";
                //    dt = bc.bcDB.dscDB.selectByVn(txtHn.Text, vn, bc.datetoDB(vsDate));
                //}
            }
            
            ContextMenu menuGw = new ContextMenu();
            menuGw.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_grfscan_print));
            menuGw.MenuItems.Add("ต้องการ Print ภาพทั้งหมด", new EventHandler(ContextMenu_grfscan_print_all));
            menuGw.MenuItems.Add("ต้องการ Download ภาพนี้", new EventHandler(ContextMenu_grfscan_Download));
            //menuGw.MenuItems.Add("ต้องการ ลบข้อมูลนี้", new EventHandler(ContextMenu_Delete));
            grfScan.ContextMenu = menuGw;
            //new LogWriter("e", "FrmScanView1 setGrfScan 6 ");
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
                    //for(int k=0; k < grfScan.Rows.Count; k++)
                    //{
                    //    grfScan.Rows[k].Height = 200;
                    //}
                    pB1.Value = 0;
                    pB1.Minimum = 0;
                    pB1.Maximum = dt.Rows.Count;
                    //MemoryStream stream;
                    //Image loadedImage, resizedImage;
                    if (pB1.Value == 0)
                    {
                        lbCnt.Text = "["+dt.Rows[0][bc.bcDB.dscDB.dsc.pic_before_scan_cnt].ToString()+"]"+ "[" + dt.Rows[0][bc.bcDB.dscDB.dsc.row_cnt].ToString() + "]";
                    }
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
                                new LogWriter("e", "FrmScanView1 SetGrfScan try int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize); ex " + ex.Message + " " + err);
                            }
                            err = "03";

                            loadedImage = new Bitmap(stream);
                            err = "04";
                            int originalWidth = 0;
                            originalWidth = loadedImage.Width;
                            int newWidth = bc.imgScanWidth;
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
                            if(colcnt==50) GC.Collect();
                            if (colcnt == 100) GC.Collect();
                        }
                        catch (Exception ex)
                        {
                            String aaa = ex.Message + " " + err;
                            new LogWriter("e", "FrmScanView1 SetGrfScan ex " + ex.Message+" "+ err+ " colcnt " + colcnt+" HN "+ txtHn.Text+" "+ an);
                        }
                    //}).Start();

                    pB1.Value++;
                        Application.DoEvents();
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("" + ex.Message, "");
                    new LogWriter("e", "FrmScanView1 SetGrfScan if (dt.Rows.Count > 0) ex " + ex.Message);
                }
            }
            //new LogWriter("e", "FrmScanView1 setGrfScan 10 ");
            setHeaderEnable(pB1);
        }
        private void ContextMenu_grfscan_Download(object sender, System.EventArgs e)
        {
            String id = "", datetick="";
            if (grfScan.Col <= 0) return;
            if (grfScan.Row < 0) return;
            if (grfScan.Col == 1)
            {
                id = grfScan[grfScan.Row, colPic2].ToString();
            }
            else
            {
                id = grfScan[grfScan.Row, colPic4].ToString();
            }
            dsc_id = id;
            Stream streamDownload = null;
            MemoryStream strm = null;
            foreach (listStream lstrmm in lStream)
            {
                if (lstrmm.id.Equals(id))
                {
                    strm = lstrmm.stream;
                    streamDownload = lstrmm.stream;
                    break;
                }
            }
            if (!Directory.Exists(bc.iniC.pathDownloadFile))
            {
                Directory.CreateDirectory(bc.iniC.pathDownloadFile);
            }
            datetick = DateTime.Now.Ticks.ToString();
            Image img = Image.FromStream(streamDownload);
            img.Save(bc.iniC.pathDownloadFile+"\\"+txtHn.Text.Trim()+"_"+ datetick+".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            bc.ExploreFile(bc.iniC.pathDownloadFile + "\\" + txtHn.Text.Trim() + "_" + datetick + ".jpg");
        }
        private void ContextMenu_grfscan_print(object sender, System.EventArgs e)
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
        private void ContextMenu_grfscan_print_all(object sender, System.EventArgs e)
        {
            //FrmWaiting frmW = new FrmWaiting();
            //frmW.StartPosition = FormStartPosition.CenterScreen;
            //frmW.ShowDialog(this);

            int i = 0;
            foreach(Row row in grfScan.Rows)
            {
                String id = "";
                if (i==0)
                {
                    id = row[colPic2].ToString();
                    i = 1;
                }
                else
                {
                    id = row[colPic4].ToString();
                    i = 0;
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
            }

            //frmW.Dispose();
        }
        private void ContextMenu_print_lab(object sender, System.EventArgs e)
        {

        }
        //private void clearGrf()
        //{
        //    foreach (Control con in panel3.Controls)
        //    {
        //        if (con is C1DockingTab)
        //        {
        //            foreach (Control cond in con.Controls)
        //            {
        //                if (cond is C1DockingTabPage)
        //                {
        //                    foreach (Control cong in cond.Controls)
        //                    {
        //                        if (cong is C1DockingTab)
        //                        {
        //                            foreach (Control congd in cong.Controls)
        //                            {
        //                                if (congd is C1DockingTabPage)
        //                                {
        //                                    foreach (Control congd1 in congd.Controls)
        //                                    {
        //                                        if (congd1 is C1FlexGrid)
        //                                        {
        //                                            C1FlexGrid grf1;
        //                                            grf1 = (C1FlexGrid)congd1;
        //                                            //grf1.Clear();
        //                                            grf1.Rows.Count = 0;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        private void setGrfVsOPD()
        {
            ProgressBar pB1 = new ProgressBar();
            pB1.Location = new System.Drawing.Point(20, 16);
            pB1.Name = "pB1";
            pB1.Size = new System.Drawing.Size(862, 23);
            gbPtt.Controls.Add(pB1);
            pB1.Left = txtHn.Left;
            pB1.Show();
            setHeaderDisable();

            //if (cspL.Controls.Count > 0)
            //{
            //    cspL.Controls.Clear();
            //}
            //if (cspR.Controls.Count > 0)
            //{
            //    cspR.Controls.Clear();
            //}
            //cspL.Controls.Add(picL);
            //cspR.Controls.Add(picR);

            grfOPD.Clear();
            grfOPD.Rows.Count = 1;
            grfOPD.Cols.Count = 8;

            //C1TextBox text = new C1TextBox();
            //grfVs.Cols[colVsVsDate].Editor = text;
            //grfVs.Cols[colVsVn].Editor = text;
            //grfVs.Cols[colVsDept].Editor = text;
            //grfVs.Cols[colVsPreno].Editor = text;

            grfOPD.Cols[colVsVsDate].Width = 100;
            grfOPD.Cols[colVsVn].Width = 80;
            grfOPD.Cols[colVsDept].Width = 240;
            grfOPD.Cols[colVsPreno].Width = 100;
            grfOPD.Cols[colVsStatus].Width = 60;
            grfOPD.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfOPD.Cols[colVsVsDate].Caption = "Visit Date";
            grfOPD.Cols[colVsVn].Caption = "VN";
            grfOPD.Cols[colVsDept].Caption = "แผนก";
            grfOPD.Cols[colVsPreno].Caption = "";
            grfOPD.Cols[colVsPreno].Visible = false;
            grfOPD.Cols[colVsVn].Visible = true;
            grfOPD.Cols[colVsAn].Visible = true;
            grfOPD.Cols[colVsAndate].Visible = false;
            grfOPD.Rows[0].Visible = false;
            grfOPD.Cols[0].Visible = false;
            grfOPD.Cols[colVsVsDate].AllowEditing = false;
            grfOPD.Cols[colVsVn].AllowEditing = false;
            grfOPD.Cols[colVsDept].AllowEditing = false;
            grfOPD.Cols[colVsPreno].AllowEditing = false;

            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dt = bc.bcDB.vsDB.selectVisitByHn4(txtHn.Text,"O");
            int i = 1, j = 1, row = grfOPD.Rows.Count;
            //txtVN.Value = dt.Rows.Count;
            //txtName.Value = "";
            //txt.Value = "";
            pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                pB1.Value++;
                Row rowa = grfOPD.Rows.Add();
                String status = "", vn = "";

                status = row1["MNC_PAT_FLAG"] != null ? row1["MNC_PAT_FLAG"].ToString().Equals("O") ? "OPD" : "IPD" : "-";
                vn = row1["MNC_VN_NO"].ToString() + "/" + row1["MNC_VN_SEQ"].ToString() + "(" + row1["MNC_VN_SUM"].ToString() + ")";
                rowa[colVsVsDate] = bc.datetoShow1(row1["mnc_date"].ToString());
                rowa[colVsVn] = vn;
                rowa[colVsStatus] = status;
                rowa[colVsPreno] = row1["mnc_pre_no"].ToString();
                rowa[colVsDept] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colVsAn] = row1["mnc_an_no"].ToString() + "/" + row1["mnc_an_yr"].ToString();
                rowa[colVsAndate] = bc.datetoShow1(row1["mnc_ad_date"].ToString());
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
            setHeaderEnable(pB1);
        }
        private void setGrfVsIPD()
        {
            ProgressBar pB1 = new ProgressBar();
            pB1.Location = new System.Drawing.Point(20, 16);
            pB1.Name = "pB1";
            pB1.Size = new System.Drawing.Size(862, 23);
            gbPtt.Controls.Add(pB1);
            pB1.Left = txtHn.Left;
            pB1.Show();
            setHeaderDisable();

            grfIPD.Clear();
            grfIPD.Rows.Count = 1;
            grfIPD.Cols.Count = 10;
            
            //C1TextBox text = new C1TextBox();
            //grfVs.Cols[colVsVsDate].Editor = text;
            //grfVs.Cols[colVsVn].Editor = text;
            //grfVs.Cols[colVsDept].Editor = text;
            //grfVs.Cols[colVsPreno].Editor = text;

            grfIPD.Cols[colIPDDate].Width = 100;
            grfIPD.Cols[colIPDVn].Width = 80;
            grfIPD.Cols[colIPDDept].Width = 240;
            grfIPD.Cols[colIPDPreno].Width = 100;
            grfIPD.Cols[colIPDStatus].Width = 60;
            grfIPD.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfIPD.Cols[colIPDDate].Caption = "Visit Date";
            grfIPD.Cols[colIPDVn].Caption = "VN";
            grfIPD.Cols[colIPDDept].Caption = "แผนก";
            grfIPD.Cols[colIPDPreno].Caption = "";
            grfIPD.Cols[colIPDPreno].Visible = false;
            grfIPD.Cols[colIPDVn].Visible = true;
            grfIPD.Cols[colIPDAnShow].Visible = true;
            grfIPD.Cols[colIPDAndate].Visible = false;
            grfIPD.Rows[0].Visible = false;
            grfIPD.Cols[0].Visible = false;
            grfIPD.Cols[colIPDDate].AllowEditing = false;
            grfIPD.Cols[colIPDVn].AllowEditing = false;
            grfIPD.Cols[colIPDDept].AllowEditing = false;
            grfIPD.Cols[colIPDPreno].AllowEditing = false;

            DataTable dt = new DataTable();
            //MessageBox.Show("hn "+hn, "");
            dt = bc.bcDB.vsDB.selectVisitByHn4(txtHn.Text,"I");
            int i = 0, j = 1, row = grfIPD.Rows.Count;
            //txtVN.Value = dt.Rows.Count;
            //txtName.Value = "";
            //txt.Value = "";
            grfIPD.Rows.Count = 0;
            grfIPD.Rows.Count = dt.Rows.Count;
            pB1.Maximum = dt.Rows.Count;
            foreach (DataRow row1 in dt.Rows)
            {
                pB1.Value++;
                Row rowa = grfIPD.Rows[i];
                String status = "", vn = "";
                
                status = row1["MNC_PAT_FLAG"] != null ? row1["MNC_PAT_FLAG"].ToString().Equals("O") ? "OPD" : "IPD" : "-";
                vn = row1["MNC_VN_NO"].ToString() + "/" + row1["MNC_VN_SEQ"].ToString() + "(" + row1["MNC_VN_SUM"].ToString() + ")" ;
                rowa[colIPDDate] = bc.datetoShow1(row1["mnc_date"].ToString());
                rowa[colIPDVn] = vn;
                rowa[colIPDStatus] = status;
                rowa[colIPDPreno] = row1["mnc_pre_no"].ToString();
                rowa[colIPDDept] = row1["MNC_SHIF_MEMO"].ToString();
                rowa[colIPDAnShow] = row1["mnc_an_no"].ToString()+"/"+ row1["mnc_an_yr"].ToString();
                rowa[colIPDAndate] = bc.datetoShow1(row1["mnc_ad_date"].ToString());
                rowa[colIPDAnYr] = row1["mnc_an_yr"].ToString();
                rowa[colIPDAn] = row1["mnc_an_no"].ToString();
                i++;
            }
            //ContextMenu menuGw = new ContextMenu();
            //menuGw.MenuItems.Add("&ยกเลิก รูปภาพนี้", new EventHandler(ContextMenu_Void));
            //menuGw.MenuItems.Add("&Update ข้อมูล", new EventHandler(ContextMenu_Update));
            //foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
            //{
            //    menuGw.MenuItems.Add("&เลือกประเภทเอกสาร และUpload Image [" + dgs.doc_group_name + "]", new EventHandler(ContextMenu_upload));
            //}
            //grfVs.ContextMenu = menuGw;
            grfIPD.Cols[colIPDStatus].Visible = false;
            grfIPD.Cols[colIPDAnYr].Visible = false;
            grfIPD.Cols[colIPDAn].Visible = false;
            setHeaderEnable(pB1);
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
            Size size = new Size();
            widthL = tabStfNote.Width / 2;
            widthR = widthL + 5;
            //sct.SplitterDistance = fpL.Width;
            cspL.Width = widthL;
            sC1.HeaderHeight = 0;
            scVs.Width = 240;
            //sct.Panel1.Width = fpL.Width;
            //sct.Panel2.Width = fpR.Width;
            sct.HeaderHeight = 0;
            //Point poigtt = new Point();
            //poigtt.X = gbPtt.Width - picExit.Width - 10;
            //poigtt.Y = 10;
            //picExit.Location = poigtt;
            this.Text = "Last Update 2020-03-04";
            gbPtt.Height = 60;
            size = bc.MeasureString(txtName);
            txtName.Size = size;
            if (!flagShowBtnSearch.Equals("show"))
            {
                btnHn.Hide();
                txtName.Location = new Point(txtHn.Width + 50, txtHn.Location.Y);
                lbAge.Location = new Point(txtName.Location.X + txtName.Width + 5, txtName.Location.Y);
                size = bc.MeasureString(lbAge);
                //label1.Location = new Point(lbAge.Location.X + size.Width + 10, txtName.Location.Y);
                //size = bc.MeasureString(label1);
                //cboDgs.Location = new Point(label1.Location.X + size.Width + 10, txtName.Location.Y);
                label2.Location = new Point(lbAge.Location.X + size.Width + 10, txtName.Location.Y);
                size = bc.MeasureString(label2);
                txtVN.Location = new Point(label2.Location.X + size.Width + 10, txtName.Location.Y);
                size = bc.MeasureString(txtVN);
                chkIPD.Location = new Point(txtVN.Location.X + txtVN.Width + 10, txtName.Location.Y-5);
                //txt.Location = new Point(chkIPD.Location.X + chkIPD.Width + 10, txtName.Location.Y);
                lbCnt.Location = new Point(chkIPD.Location.X + chkIPD.Width + 10, txtName.Location.Y);
            }
            lbDrugAllergy.Location = new Point(txtName.Location.X + 10,  txtName.Height+6);
            
        }
    }
}
