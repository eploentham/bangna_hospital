using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmScanView : Form
    {
        BangnaControl bc;

        C1FlexGrid grfVs;
        Font fEdit, fEditB;
        C1DockingTab tcDtr;
        C1DockingTabPage tabScan, tabOrder;
        C1FlexGrid grfOrder;

        int colVsVsDate=1, colVsVn = 2, colVsStatus=3, colVsDept = 4, colVsPreno=5, colVsAn=6, colVsAndate=7;
        int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
        int colOrderId = 1, colOrderName = 2, colOrderMed = 3, colOrderQty = 4;
        int newHeight = 720;
        int mouseWheel = 0;
        int originalHeight = 0;
        ArrayList array1 = new ArrayList();
        List<listStream> lStream;
        listStream strm;
        Image resizedImage, img;
        C1PictureBox pic, picL,picR;
        FlowLayoutPanel fpL, fpR;
        SplitContainer sct;
        //VScrollBar vScroller;
        //int y = 0;
        Form frmImg;
        String dsc_id = "";
        //Timer timer1;
        [STAThread]
        private void txtStatus(String msg)
        {
            txt.Invoke(new EventHandler(delegate
            {
                txt.Value = msg;
            }));
        }
        public FrmScanView(BangnaControl bc)
        {
            InitializeComponent();
            this.bc = bc;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            bc.bcDB.dgsDB.setCboBsp(cboDgs, "");

            array1 = new ArrayList();
            lStream = new List<listStream>();
            strm = new listStream();
            grfOrder = new C1FlexGrid();
            //timer1 = new Timer();
            //int chk = 0;
            //int.TryParse(bc.iniC.timerImgScanNew, out chk);
            //timer1.Interval = chk;
            //timer1.Enabled = true;
            //timer1.Tick += Timer1_Tick;
            //timer1.Stop();

            theme1.SetTheme(sb1, "ExpressionDark");
            theme1.SetTheme(groupBox1, "ExpressionDark");
            theme1.SetTheme(panel2, "ExpressionDark");
            theme1.SetTheme(panel3, "ExpressionDark");
            foreach (Control con in groupBox1.Controls)
            {
                theme1.SetTheme(con, "ExpressionDark");
            }
            //foreach (Control con in grfScan.Controls)
            //{
            //    theme1.SetTheme(con, "ExpressionDark");
            //}
            initGrf();
            setGrf();
            

            btnHn.Click += BtnHn_Click;
            btnOpen.Click += BtnOpen_Click;
            btnRefresh.Click += BtnRefresh_Click;
            txtHn.KeyUp += TxtHn_KeyUp;

            tcDtr = new C1DockingTab();
            tcDtr.Dock = System.Windows.Forms.DockStyle.Fill;
            tcDtr.Location = new System.Drawing.Point(0, 266);
            tcDtr.Name = "c1DockingTab1";
            tcDtr.Size = new System.Drawing.Size(669, 200);
            tcDtr.TabIndex = 0;
            tcDtr.TabsSpacing = 5;
            panel3.Controls.Add(tcDtr);
            tabScan = new C1DockingTabPage();
            tabScan.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabScan.Size = new System.Drawing.Size(667, 175);
            tabScan.TabIndex = 0;
            tabScan.Text = "ใบยา / Staff's Note";
            tabScan.Name = "tabPageScan";
            tcDtr.Controls.Add(tabScan);
            tabOrder = new C1DockingTabPage();
            tabOrder.Location = new System.Drawing.Point(1, 24);
            //tabScan.Name = "c1DockingTabPage1";
            tabOrder.Size = new System.Drawing.Size(667, 175);
            tabOrder.TabIndex = 0;
            tabOrder.Text = "ประวัติการสั่งการ";
            tabOrder.Name = "tabOrder";
            tcDtr.Controls.Add(tabOrder);
            grfOrder.Font = fEdit;
            grfOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOrder.Location = new System.Drawing.Point(0, 0);
            grfOrder.Rows[0].Visible = false;
            grfOrder.Cols[0].Visible = false;
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
            theme1.SetTheme(grfOrder, "Office2016Black");
            tabOrder.Controls.Add(grfOrder);
            setPicStaffNote();
            theme1.SetTheme(tcDtr, theme1.Theme);
            int i = 0;
            String idOld = "";
            if (bc.bcDB.dgssDB.lDgss.Count <= 0) bc.bcDB.dgssDB.getlBsp();
            foreach (DocGroupSubScan dgss in bc.bcDB.dgssDB.lDgss)
            {
                String dgsid = "";
                dgsid = bc.bcDB.dgssDB.getDgsIdDgss(dgss.doc_group_sub_name);
                if (!dgsid.Equals(idOld))
                {
                    idOld = dgsid;
                    String name = "";
                    name = bc.bcDB.dgsDB.getNameDgs(dgss.doc_group_id);
                    C1DockingTabPage tabPage = new C1DockingTabPage();
                    tabPage.Location = new System.Drawing.Point(1, 24);
                    tabPage.Size = new System.Drawing.Size(667, 175);

                    tabPage.TabIndex = 0;
                    tabPage.Text = " " + name + "  ";
                    tabPage.Name = dgsid;
                    tcDtr.Controls.Add(tabPage);
                    i++;
                    C1DockingTab tabDtr1 = new C1DockingTab();
                    tabDtr1.Dock = System.Windows.Forms.DockStyle.Fill;
                    tabDtr1.Location = new System.Drawing.Point(0, 266);
                    tabDtr1.Name = "c1DockingTab1";
                    tabDtr1.Size = new System.Drawing.Size(669, 200);
                    tabDtr1.TabIndex = 0;
                    tabDtr1.TabsSpacing = 5;
                    tabPage.Controls.Add(tabDtr1);
                    theme1.SetTheme(tabDtr1, "Office2010Red");
                    foreach (DocGroupSubScan dgsss in bc.bcDB.dgssDB.lDgss)
                    {
                        if (dgsss.doc_group_id.Equals(dgss.doc_group_id))
                        {
                            //addDevice.MenuItems.Add(new MenuItem(dgsss.doc_group_sub_name, new EventHandler(ContextMenu_upload)));

                            C1DockingTabPage tabPage2 = new C1DockingTabPage();
                            tabPage2.Location = new System.Drawing.Point(1, 24);
                            tabPage2.Size = new System.Drawing.Size(667, 175);
                            tabPage2.TabIndex = 0;
                            tabPage2.Text = " " + dgsss.doc_group_sub_name + "  ";
                            tabPage2.Name = "tab" + dgsss.doc_group_sub_id;
                            tabDtr1.Controls.Add(tabPage2);
                            C1FlexGrid grf = new C1FlexGrid();
                            grf.Font = fEdit;
                            grf.Dock = System.Windows.Forms.DockStyle.Fill;
                            grf.Location = new System.Drawing.Point(0, 0);
                            grf.Rows[0].Visible = false;
                            grf.Cols[0].Visible = false;
                            grf.Rows.Count = 1;
                            grf.Name = dgsss.doc_group_sub_id;
                            grf.Cols.Count = 5;
                            Column colpic1 = grf.Cols[colPic1];
                            colpic1.DataType = typeof(Image);
                            Column colpic2 = grf.Cols[colPic2];
                            colpic2.DataType = typeof(String);
                            Column colpic3 = grf.Cols[colPic3];
                            colpic3.DataType = typeof(Image);
                            Column colpic4 = grf.Cols[colPic4];
                            colpic4.DataType = typeof(Image);
                            grf.Cols[colPic1].Width = 310;
                            grf.Cols[colPic2].Width = 310;
                            grf.Cols[colPic3].Width = 310;
                            grf.Cols[colPic4].Width = 310;
                            grf.ShowCursor = true;
                            grf.Cols[colPic2].Visible = false;
                            grf.Cols[colPic3].Visible = false;
                            grf.Cols[colPic4].Visible = false;
                            grf.Cols[colPic1].AllowEditing = false;
                            grf.DoubleClick += Grf_DoubleClick;
                            tabPage2.Controls.Add(grf);
                        }
                    }
                }
                else
                {

                }
            }
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
            sct = new SplitContainer();
            sct.Dock = DockStyle.Fill;
            tabScan.Controls.Add(sct);

            fpL = new FlowLayoutPanel();
            fpL.Dock = DockStyle.Fill;
            fpL.AutoScroll = true;
            sct.Panel1.Controls.Add(fpL);
            //tabScan.Controls.Add(fpL);
            fpR = new FlowLayoutPanel();
            fpR.Dock = DockStyle.Fill;
            fpR.AutoScroll = true;
            //tabScan.Controls.Add(fpR);
            sct.Panel2.Controls.Add(fpR);

            picL = new C1PictureBox();
            picL = new C1PictureBox();
            picL.Dock = DockStyle.Fill;
            picL.SizeMode = PictureBoxSizeMode.AutoSize;
            fpL.Controls.Add(picL);

            picR = new C1PictureBox();
            picR = new C1PictureBox();
            picR.Dock = DockStyle.Fill;
            picR.SizeMode = PictureBoxSizeMode.AutoSize;
            fpR.Controls.Add(picR);
        }
        private void Grf_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //MessageBox.Show("Row " + ((C1FlexGrid)sender).Row+"\n grf name "+ ((C1FlexGrid)sender).Name, "Col "+((C1FlexGrid)sender).Col+" id "+ ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row,colPic2].ToString());
            if (((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colPic2] == null) return;
            if (((C1FlexGrid)sender).Row <= 0) return;
            String id = "";
            id = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colPic2].ToString();
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
        private void ContextMenu_Delete(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("ต้องการ ลบข้อมูลนี้ ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                int chk = 0;
                String re = bc.bcDB.dscDB.voidDocScan(dsc_id,"");
                if(int.TryParse(re, out chk))
                {
                    frmImg.Dispose();
                    setGrf();
                    clearGrf();
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
                setGrf();
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
            setGrf();
        }

        private void BtnHn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmSearchHn frm = new FrmSearchHn(bc, FrmSearchHn.StatusConnection.host);
            frm.ShowDialog(this);
            txtHn.Value = bc.sPtt.Hn;
            txtName.Value = bc.sPtt.Name;
            setGrf();
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
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            panel2.Controls.Add(grfVs);

            theme1.SetTheme(grfVs, "ExpressionDark");

        }

        private void GrfVs_AfterRowColChange(object sender, RangeEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.NewRange.r1 < 0) return;
            if (e.NewRange.Data == null) return;

            if (txtHn.Text.Equals("")) return;
            panel2.Enabled = false;

            ProgressBar pB1 = new ProgressBar();
            pB1.Location = new System.Drawing.Point(113, 36);
            pB1.Name = "pB1";
            pB1.Size = new System.Drawing.Size(862, 23);
            groupBox1.Controls.Add(pB1);
            pB1.Left = txtVN.Left;
            pB1.Show();
            txtVN.Hide();
            btnVn.Hide();
            btnRefresh.Hide();
            txt.Hide();
            label6.Hide();
            txtVisitDate.Hide();
            chkIPD.Hide();
            //txtPreNo.Hide();

            clearGrf();
            String statusOPD = "", vsDate="", vn="", an="", anDate="", hn="", preno="";
            statusOPD = grfVs[e.NewRange.r1, colVsStatus] != null ? grfVs[e.NewRange.r1, colVsStatus].ToString() : "";
            preno = grfVs[e.NewRange.r1, colVsPreno] != null ? grfVs[e.NewRange.r1, colVsPreno].ToString() : "";
            vsDate = grfVs[e.NewRange.r1, colVsVsDate] != null ? grfVs[e.NewRange.r1, colVsVsDate].ToString() : "";
            txtVisitDate.Value = vsDate;
            if (statusOPD.Equals("OPD"))
            {
                chkIPD.Checked = false;
                vn = grfVs[e.NewRange.r1, colVsVn] != null ? grfVs[e.NewRange.r1, colVsVn].ToString() : "";
                txtVN.Value = vn;
                label2.Text = "VN :";
            }
            else
            {
                chkIPD.Checked = true;
                an = grfVs[e.NewRange.r1, colVsAn] != null ? grfVs[e.NewRange.r1, colVsAn].ToString() : "";
                anDate = grfVs[e.NewRange.r1, colVsAndate] != null ? grfVs[e.NewRange.r1, colVsAndate].ToString() : "";
                txtVN.Value = an;
                label2.Text = "AN :";
                txtVisitDate.Value = anDate;
            }
            String file = "", dd="", mm="", yy="";
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
                catch(Exception ex)
                {
                    MessageBox.Show("error "+ex.Message, "");
                }
                
            }
            DataTable dtOrder = new DataTable();
            vn = grfVs[e.NewRange.r1, colVsVn] != null ? grfVs[e.NewRange.r1, colVsVn].ToString() : "";
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
                    foreach (DataRow row in dtOrder.Rows)
                    {
                        Row rowg = grfOrder.Rows.Add();
                        rowg[colOrderName] = row["MNC_PH_TN"].ToString();
                        rowg[colOrderMed] = "";
                        rowg[colOrderQty] = row["qty"].ToString();
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
            if (dt.Rows.Count > 0)
            {
                try
                {
                    pB1.Value = 0;
                    pB1.Minimum = 0;
                    pB1.Maximum = dt.Rows.Count;
                    //MemoryStream stream;
                    //Image loadedImage, resizedImage;
                    C1FlexGrid grf1;
                    FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
                    Boolean findTrue = false;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (findTrue) break;
                        String dgssid = "", filename = "", ftphost = "", id = "", folderftp="";
                        id = row[bc.bcDB.dscDB.dsc.doc_scan_id].ToString();
                        dgssid = row[bc.bcDB.dscDB.dsc.doc_group_sub_id].ToString();
                        filename = row[bc.bcDB.dscDB.dsc.image_path].ToString();
                        ftphost = row[bc.bcDB.dscDB.dsc.host_ftp].ToString();
                        folderftp = row[bc.bcDB.dscDB.dsc.folder_ftp].ToString();
                        foreach (Control con in panel3.Controls)
                        {
                            if (findTrue) break;
                            if (con is C1DockingTab)
                            {
                                foreach (Control cond in con.Controls)
                                {
                                    if (findTrue) break;
                                    if (cond is C1DockingTabPage)
                                    {
                                        foreach (Control cong in cond.Controls)
                                        {
                                            if (findTrue) break;
                                            if (cong is C1DockingTab)
                                            {
                                                foreach (Control congd in cong.Controls)
                                                {
                                                    if (findTrue) break;
                                                    if (congd is C1DockingTabPage)
                                                    {
                                                        foreach (Control congd1 in congd.Controls)
                                                        {
                                                            if (congd1 is C1FlexGrid)
                                                            {
                                                                if (congd1.Name.Equals(dgssid))
                                                                {
                                                                    try
                                                                    {
                                                                        grf1 = (C1FlexGrid)congd1;
                                                                        Row rowd = grf1.Rows.Add();
                                                                        MemoryStream stream;
                                                                        Image loadedImage, resizedImage;
                                                                        stream = new MemoryStream();
                                                                        stream = ftp.download(folderftp + "//" + filename);

                                                                        //loadedImage = Image.FromFile(filename);

                                                                        loadedImage = new Bitmap(stream);
                                                                        int originalWidth = 0;
                                                                        originalWidth = loadedImage.Width;
                                                                        int newWidth = 280;
                                                                        resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
                                                                        //
                                                                        rowd[colPic1] = resizedImage;
                                                                        rowd[colPic2] = id;
                                                                        strm = new listStream();
                                                                        strm.id = id;
                                                                        strm.stream = stream;
                                                                        lStream.Add(strm);

                                                                        grf1.AutoSizeRows();

                                                                        //loadedImage.Dispose();
                                                                        //resizedImage.Dispose();
                                                                        //stream.Dispose();
                                                                        Application.DoEvents();
                                                                        //findTrue = true;
                                                                        break;
                                                                        //GC.Collect();
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        String aaa = ex.Message;
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
                        }
                        pB1.Value++;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("" + ex.Message, "");
                }
                
            }
            pB1.Dispose();
            txtVN.Show();
            btnVn.Show();
            btnRefresh.Show();
            txt.Show();
            label6.Show();
            chkIPD.Show();
            //grf1.AutoSizeRows();
            panel2.Enabled = true;
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

        private void setGrf()
        {
            grfVs.Clear();
            grfVs.Rows.Count = 1;
            grfVs.Cols.Count = 8;
            
            C1TextBox text = new C1TextBox();
            grfVs.Cols[colVsVsDate].Editor = text;
            grfVs.Cols[colVsVn].Editor = text;
            grfVs.Cols[colVsDept].Editor = text;
            grfVs.Cols[colVsPreno].Editor = text;

            grfVs.Cols[colVsVsDate].Width = 100;
            grfVs.Cols[colVsVn].Width = 80;
            grfVs.Cols[colVsDept].Width = 100;
            grfVs.Cols[colVsPreno].Width = 100;
            grfVs.Cols[colVsStatus].Width = 60;
            grfVs.ShowCursor = true;
            //grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
            grfVs.Cols[colVsVsDate].Caption = "Visit Date";
            grfVs.Cols[colVsVn].Caption = "VN";
            grfVs.Cols[colVsDept].Caption = "แผนก";
            grfVs.Cols[colVsPreno].Caption = "";
            grfVs.Cols[colVsPreno].Visible = false;
            grfVs.Cols[colVsVn].Visible = false;
            grfVs.Cols[colVsAn].Visible = false;
            grfVs.Cols[colVsAndate].Visible = false;
            grfVs.Rows[0].Visible = false;
            grfVs.Cols[0].Visible = false;
            grfVs.Cols[colVsVsDate].AllowEditing = false;
            grfVs.Cols[colVsVn].AllowEditing = false;
            grfVs.Cols[colVsDept].AllowEditing = false;
            grfVs.Cols[colVsPreno].AllowEditing = false;

            DataTable dt = new DataTable();
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
        private void FrmScanView_Load(object sender, EventArgs e)
        {
            fpL.Width = tabScan.Width / 2;
            fpR.Width = fpL.Width+5;
            sct.SplitterDistance = fpL.Width;
            //sct.Panel1.Width = fpL.Width;
            //sct.Panel2.Width = fpR.Width;
            //pnR.Width = 0;
        }
    }
}
